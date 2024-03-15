
param secrets array
param keyVaultName string
resource vault 'Microsoft.KeyVault/vaults@2022-11-01' existing = {
  name: keyVaultName
}
resource addsecret 'Microsoft.KeyVault/vaults/secrets@2022-11-01' =[for secret in secrets: {
  parent: vault
  name: '${secret.Name}'
  properties: {
    value: secret.Value
  }
}]
