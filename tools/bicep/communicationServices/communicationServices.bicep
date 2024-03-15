@secure()
param dataLocation string
param location string
param emailServiceName string
param communicationServiceName string
param emailServiceCreate bool
param emaildomainCreate bool
param communicationServiceCreate bool
//add email service
resource emailService 'Microsoft.Communication/emailServices@2023-04-01-preview' = if (emailServiceCreate) {
  name: emailServiceName
  location: 'Global'
  properties: {
    dataLocation: dataLocation
  }
}
resource emailServiceExisting 'Microsoft.Communication/emailServices@2023-04-01-preview' existing = if (!emailServiceCreate) {
  name: emailServiceName
}
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
//add email domains
resource emailServiceobj 'Microsoft.Communication/emailServices@2023-04-01-preview' existing = {
  name: emailServiceCreate ? emailService.name : emailServiceExisting.name
}
resource emaildomain 'Microsoft.Communication/emailServices/domains@2023-04-01-preview' = if (emaildomainCreate) {
  parent: emailServiceobj
  name: domainManagement == 'AzureManaged' ? 'AzureManagedDomain' : customDomain
  location: 'Global'
  properties: {
    domainManagement: domainManagement
    userEngagementTracking: userEngagementTracking
  }
}
// SenderUsername (Azure Managed)
resource senderUserNameAzureManaged 'Microsoft.Communication/emailServices/domains/senderUsernames@2023-03-31' = if (emaildomainCreate) {
  parent: emaildomain
  name: 'donotreply'
  properties: {
    username: username
    displayName: displayName
  }
}

output fromSenderDomain string = emaildomainCreate ? emaildomain.properties.mailFromSenderDomain : ''

// // Email Communication Services Domain (Customer Managed)
// resource emailServiceCustomDomain 'Microsoft.Communication/emailServices/domains@2023-04-01-preview' = if (emaildomainCreate && domainManagement != 'AzureManaged') {
//   parent: emailServiceobj
//   name: customDomain
//   location: 'Global'
//   properties: {
//     domainManagement: domainManagement
//     userEngagementTracking: userEngagementTracking
//   }
// }

// // SenderUsername (Customer Managed Domain)
// resource senderUserNameCustomDomain 'Microsoft.Communication/emailServices/domains/senderUsernames@2023-03-31' = if (emaildomainCreate && domainManagement != 'AzureManaged') {
//   parent: emailServiceCustomDomain
//   name: 'donotreply'
//   properties: {
//     username: username
//     displayName: displayName
//   }
// }
var linkedDomains = !emaildomainCreate ? [] : [ emaildomain.id ]
//
resource communicationService 'Microsoft.Communication/communicationServices@2023-04-01' = if (communicationServiceCreate) {
  name: communicationServiceName
  location: 'Global'
  identity: {
    type: 'SystemAssigned'
  }
  properties: {
    dataLocation: dataLocation
    linkedDomains: linkedDomains
  }
}
resource communicationServicEexisting 'Microsoft.Communication/communicationServices@2023-04-01' existing = if (!communicationServiceCreate) {
  name: communicationServiceName
}
output communicationServiceName string = communicationServiceCreate ? communicationService.name : communicationServicEexisting.name
