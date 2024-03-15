@description('The name of the SQL logical server.')
param serverName string = uniqueString('sql', resourceGroup().id)

@description('The name of the SQL Database.')
param sqlDBName string = 'SampleDB'

@description('Location for all resources.')
param location string = resourceGroup().location

@description('The administrator username of the SQL logical server.')
param sqlAdministratorLogin string

@description('The administrator password of the SQL logical server.')
@secure()
param sqlAdministratorLoginPassword string

param skuname string = 'Standard'
param skutier string = 'Standard'

param DbCreate bool = true

param serverCreate bool = true
resource sqlServerexisting 'Microsoft.Sql/servers@2022-05-01-preview' existing = if (!serverCreate) {
  name: serverName

}
// resource sqlDBexisting 'Microsoft.Sql/servers/databases@2022-05-01-preview' existing = if (!DbCreate) {
//   parent: sqlServerexisting
//   name: sqlDBName

// }

resource addfirewallRules 'Microsoft.Sql/servers/firewallRules@2022-05-01-preview' =  {
  name: '${DbCreate?sqlServer.name:sqlServerexisting.name}/addfirewallRules'
  properties: {
    endIpAddress: '0.0.0.0'//Must be IPv4 format. Use value '0.0.0.0' for all Azure-internal IP addresses.
    startIpAddress: '0.0.0.0'//Must be IPv4 format. Use value '0.0.0.0' for all Azure-internal IP addresses.
  }
}
resource sqlServer 'Microsoft.Sql/servers@2022-05-01-preview' = if (serverCreate) {
  name: serverName
  location: location
  properties: {
    administratorLogin: sqlAdministratorLogin
    administratorLoginPassword: sqlAdministratorLoginPassword
  }
}

resource sqlDB 'Microsoft.Sql/servers/databases@2022-05-01-preview' = if (DbCreate) {
  //parent: sqlServer
  name: '${serverCreate ? sqlServer.name : sqlServerexisting.name}/${sqlDBName}'
  location: location
  sku: {
    name: skuname
    tier: skutier
  }
}

output sqlServerexistingObj object = serverCreate ? sqlServer : sqlServerexisting
output sqlDBexistingName string =  sqlDBName
