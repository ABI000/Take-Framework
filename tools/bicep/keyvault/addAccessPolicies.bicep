

param keyVaultName string
param objectId string
resource vault 'Microsoft.KeyVault/vaults@2022-11-01' existing = {
  name: keyVaultName
}
resource symbolicname 'Microsoft.KeyVault/vaults/accessPolicies@2022-07-01' = {
  name: 'add'
  parent: vault
  properties: {
    accessPolicies: [
      {
        objectId: objectId
        permissions: {        
          secrets: [
            'get'
            'list'
          ]
        }
        tenantId: subscription().tenantId
      }
    ]
  }
}
