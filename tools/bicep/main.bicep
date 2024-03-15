//resource-naming:https://learn.microsoft.com/zh-cn/azure/cloud-adoption-framework/ready/azure-best-practices/resource-naming
//bicep-registry-modules:https://github.com/Azure/bicep-registry-modules
targetScope = 'subscription'
@description('purpose')
@allowed([
  'prod'
  'shared'
  'client'
])
param purpose string
@description('The Tier of the Sku.')
@allowed([
  'prod'
  'dev'
  'qa'
  'stage'
  'test'
])
param environment string
@description('The name of the project.')
@minLength(4)
@maxLength(30)
param projectName string
param subscriptionId string = subscription().subscriptionId
param location string
var webAppNamePrefix = 'app'
param guidstring string = newGuid()
var suffix = '${toLower(replace(trim(location), ' ', ''))}${uniqueString(guidstring)}'
var deployObj = {
  subscriptionId: subscriptionId
  location: location
  resourceGroupName: 'rg-${projectName}-${purpose}-${suffix}'
  acrName: 'acr${projectName}${environment}${suffix}'
  storageAccountName: take('st${projectName}${suffix}', 24)
  webAppName: '${webAppNamePrefix}-${projectName}-${environment}-${suffix}'
  webHookName: '${webAppNamePrefix}${projectName}${environment}${suffix}'
  //sql
  azuresqlserversName: 'sql-${projectName}-${environment}-${suffix}'
  dbName: 'sqldb-${projectName}-${environment}-${suffix}'
  textTranslationName: 'cog-texttranslation-${projectName}-${environment}-${suffix}'
  keyVaultName: take('kv-${projectName}-${environment}-${suffix}', 24)
  appServicePlanName: 'asp-${projectName}-${environment}-${suffix}'

  //email
  communicationServiceName: 'acs-${projectName}-${environment}-${suffix}'
  emailServiceName: 'acs-email-${projectName}-${environment}-${suffix}'

}
//resourceGroup
param rgCreate bool = true
module newrg './resourceGroup.bicep' = {
  name: deployObj.resourceGroupName
  scope: subscription(deployObj.subscriptionId)
  params: {
    resourceGroupName: deployObj.resourceGroupName
    resourceGroupLocation: location
    Create: rgCreate
  }
}

//TextTranslation
@allowed([
  'F0'
])
param textTranslationskuName string = 'F0'
param textTranslationCreate bool = true
module textTranslation 'br/public:ai/cognitiveservices:1.1.1' = if (textTranslationCreate) {
  name: deployObj.textTranslationName
  scope: resourceGroup(deployObj.resourceGroupName)
  params: {
    kind: 'TextTranslation'
    skuName: textTranslationskuName
    name: deployObj.textTranslationName
    location: newrg.outputs.resourceGroupLocation
  }
}
//storageAccount
@allowed([ 'Premium_LRS', 'Premium_ZRS', 'Standard_GRS', 'Standard_GZRS', 'Standard_LRS', 'Standard_RAGRS', 'Standard_RAGZRS', 'Standard_ZRS' ])
param storageAccountsku string = 'Standard_ZRS'
param storageAccountCreate bool = true

param azureBlobDefaultContainer string = 'DefaultContainer'
module storageAccount 'br/public:storage/storage-account:3.0.1' = if (storageAccountCreate) {
  name: deployObj.storageAccountName
  scope: resourceGroup(deployObj.resourceGroupName)
  params: {
    location: newrg.outputs.resourceGroupLocation
    name: deployObj.storageAccountName
    sku: storageAccountsku
    blobContainers: [
      {
        name: toLower(azureBlobDefaultContainer)
        properties: {
          publicAccess: 'None'
        }
      }
    ]
  }
}
//azuresql
@description('The LoginName of the azuresql')
param sqlAdministratorLogin string
@description('The Password of the azuresql')
@secure()
param sqlAdministratorLoginPassword string
param DbCreate bool = true

param serverCreate bool = true

param dbskuname string = 'Standard'
param dbskutier string = 'Standard'
module azuresql './azuresql.bicep' = {
  name: deployObj.azuresqlserversName
  scope: resourceGroup(deployObj.resourceGroupName)
  params: {
    location: newrg.outputs.resourceGroupLocation
    sqlDBName: deployObj.dbName
    serverName: deployObj.azuresqlserversName
    sqlAdministratorLoginPassword: sqlAdministratorLoginPassword
    sqlAdministratorLogin: sqlAdministratorLogin
    DbCreate: DbCreate
    serverCreate: serverCreate
    skuname: dbskuname
    skutier: dbskutier
  }
}
//output sqlServerexistingObjout object = azuresql.outputs.sqlServerexistingObj
//output sqlServerDbexistingObjout string = azuresql.outputs.sqlDBexistingName
//App Service Plan
@description('The Name of the Sku.')
@allowed([
  'D1'
  'F1'
  'B1'
  'B2'
  'B3'
  'S1'
  'S2'
  'S3'
  'P1'
  'P2'
  'P3'
  'P1V2'
  'P2V2'
  'P3V2'
  'I1'
  'I2'
  'I3'
  'Y1'
])
param appServicePlanSkuname string = 'F1'
@description('The Tier of the Sku.')
@allowed([
  'Shared'
  'Free'
  'Basic'
  'Standard'
  'Dedicated'
  'Premium'
  'PremiumV2'
  'Isolated'
  'Dynamic'
])
param appServicePlanSkutier string = 'Free'
param appServicePlanCreate bool = true
module appServicePlan './appServicePlan.bicep' = {
  name: deployObj.appServicePlanName
  scope: resourceGroup(deployObj.resourceGroupName)
  params: {
    location: newrg.outputs.resourceGroupLocation
    appServicePlanName: deployObj.appServicePlanName
    skuname: appServicePlanSkuname
    skutier: appServicePlanSkutier
    Create: appServicePlanCreate
  }
}

//acr
@allowed([
  'Basic'
  'Standard'
  'Premium'
])
param containerRegistryskuname string = 'Basic'
param containerRegistryCreate bool = true
module containerRegistry './ACR.bicep' = {
  name: deployObj.acrName
  scope: resourceGroup(deployObj.resourceGroupName)
  params: {
    location: newrg.outputs.resourceGroupLocation
    registryName: deployObj.acrName
    Create: containerRegistryCreate
    skuName: containerRegistryskuname
  }
}

//web app
param webAppCreate bool = true
module webApp './webapp/webapp.bicep' = {
  name: '${deployObj.webAppName}-webapp'
  scope: resourceGroup(deployObj.resourceGroupName)
  params: {
    location: newrg.outputs.resourceGroupLocation
    webAppName: deployObj.webAppName
    registryName: containerRegistry.outputs.containerRegistryObj.name
    appServicePlanName: appServicePlan.outputs.appServicePlan.properties.name
    imageName: projectName
    imageTag: environment
    alwaysOn: false
    Create: webAppCreate
    webHookName: deployObj.webHookName
  }
}
//output webappobj object = webApp.outputs.webAppobject
//keyVault
param keyVaultCreate bool = true
module keyVault './keyvault/keyVault.bicep' = {
  name: '${deployObj.keyVaultName}-keyVault'
  scope: resourceGroup(deployObj.resourceGroupName)
  params: {
    name: deployObj.keyVaultName
    location: newrg.outputs.resourceGroupLocation
    roleAssignments: [ '4633458b-17de-408a-b874-0445c86b69e6' ]
    keyVaultCreate: keyVaultCreate
    rbacPolicies: [ {
        objectId: webApp.outputs.webAppobject.identity.principalId
      } ]
  }
}
//output sd string=keyVault.outputs.keyVaultName

//email
param emailDataLocation string
@allowed([ 'Disabled'
  'Enabled' ])
param userEngagementTracking string = 'Disabled'
@allowed([ 'AzureManaged'
  'CustomerManaged'
  'CustomerManagedInExchangeOnline' ])
param domainManagement string = 'AzureManaged'

@minLength(1)
@maxLength(253)
param customDomain string = 'pentenrieder.dev'

param username string = 'DoNotReply'
param displayName string = 'DoNotReply'
param emailServiceCreate bool = true
param emaildomainCreate bool = true
param communicationServiceCreate bool = true
module addEmailSevice './communicationServices/communicationServices.bicep' = {
  name: '${deployObj.communicationServiceName}-addEmailSevice'
  scope: resourceGroup(deployObj.resourceGroupName)
  params: {
    location: newrg.outputs.resourceGroupLocation
    dataLocation: emailDataLocation
    communicationServiceName: deployObj.communicationServiceName
    emailServiceName: deployObj.emailServiceName
    domainManagement: domainManagement
    userEngagementTracking: userEngagementTracking
    customDomain: customDomain
    username: username
    displayName: displayName
    emailServiceCreate: emailServiceCreate
    communicationServiceCreate: communicationServiceCreate
    emaildomainCreate: emaildomainCreate
  }
}

@secure()
param jwtSecretKey string
param tgphone string
param tgappid string
param tgApiHash string

param TGScreenshotQueue string = 'ScreenshotQueue'
param TGScreenshotErrorQueue string = 'ScreenshotErrorQueue'
param ErrorLogRecipient string

param addProjectsecretsCreate bool = true
param sqlConnectionString string = ''
param jwtExpires int = 1440
param azureQueueDefaultQueueName string = 'DefaultQueue'
param jwtAlgorithm string = 'HS256'
module addprojectsecrets './projectsecrets.bicep' = if (addProjectsecretsCreate) {
  name: '${deployObj.keyVaultName}-addprojectsecrets'
  scope: resourceGroup(deployObj.resourceGroupName)
  params: {
    TGScreenshotQueue: TGScreenshotQueue
    TGScreenshotErrorQueue: TGScreenshotErrorQueue
    keyVaultName: keyVault.outputs.keyVaultName
    storageAccountName: storageAccount.outputs.name
    dbname: azuresql.outputs.sqlDBexistingName
    jwtSecretKey: jwtSecretKey
    sqlpwd: sqlAdministratorLoginPassword
    sqlurl: azuresql.outputs.sqlServerexistingObj.properties.fullyQualifiedDomainName
    sqluser: sqlAdministratorLogin
    tgApiHash: tgApiHash
    tgappid: tgappid
    tgphone: tgphone
    weburi: webApp.outputs.webAppobject.properties.defaultHostName
    TranslationName: textTranslation.outputs.name
    ErrorLogRecipient: ErrorLogRecipient
    AzureMailSender: '${username}@{addEmailSevice.outputs.fromSenderDomain}'
    communicationServiceName: addEmailSevice.outputs.communicationServiceName
    jwtAlgorithm: jwtAlgorithm
    AzureBlobDefaultContainer: azureBlobDefaultContainer
    AzureQueueDefaultQueueName: azureQueueDefaultQueueName
    jwtExpires: jwtExpires
    sqlConnectionString: sqlConnectionString
    webappName: webApp.outputs.webAppobject.properties.name

  }
}
