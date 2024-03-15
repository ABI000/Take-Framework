@description('Location for all resources.')
param location string = resourceGroup().location
//web
@description('The name of the App Service Plan.')
param appServicePlanName string
param registryName string
@description('The Name of the Image.')
param imageName string
@description('The Tag of the Image.')
param imageTag string = 'latest'
@description('The name of the Web.')
param webAppName string
param alwaysOn bool = false
param Create bool = true

resource acrResource 'Microsoft.ContainerRegistry/registries@2021-09-01' existing = {
  name: registryName
}
resource appServicePlan 'Microsoft.Web/serverfarms@2020-12-01' existing = {
  name: appServicePlanName
}
resource webAppexisting 'Microsoft.Web/sites@2022-09-01' existing = if (!Create) {
  name: webAppName
}

resource webApp 'Microsoft.Web/sites@2022-09-01' = if (Create) {
  name: webAppName
  kind: 'app,linux,container'
  location: location
  identity: {
    type: 'SystemAssigned'
  }
  properties: {

    siteConfig: {
      alwaysOn: alwaysOn
      linuxFxVersion: 'DOCKER|${acrResource.properties.loginServer}/${imageName}:${imageTag}'
      appSettings: [ {
          name: 'DOCKER_REGISTRY_SERVER_PASSWORD'
          value: acrResource.listCredentials().passwords[0].value
        }
        {
          name: 'DOCKER_REGISTRY_SERVER_URL'
          value: '${registryName}.azurecr.io'
        }
        {
          name: 'DOCKER_REGISTRY_SERVER_USERNAME'
          value: acrResource.listCredentials().username
        }
        {
          name: 'DOCKER_ENABLE_CI'
          value: 'true'
        } ]
    }
    serverFarmId: appServicePlan.id
    keyVaultReferenceIdentity: 'SystemAssigned'
  }
}
resource publishingcreds 'Microsoft.Web/sites/config@2022-03-01' existing = if (Create) {
  name: '${!Create ? webAppexisting.name : webApp.name}/publishingcredentials'
}

param webHookName string

resource webappBrideWell 'Microsoft.ContainerRegistry/registries/webhooks@2023-08-01-preview' = if (Create) {
  name: webHookName
  location: location
  parent: acrResource
  properties: {
    status: 'enabled'
    scope: '${imageName}:${imageTag}'
    actions: [
      'push'
    ]
    serviceUri: publishingcreds.list().properties.scmUri
  }
}
output webAppobject object = Create ? webApp : webAppexisting
