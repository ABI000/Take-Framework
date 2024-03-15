

param name string
param location string = resourceGroup().location
param roleAssignments array 
param rbacPolicies array
param keyVaultCreate bool= true

resource key 'Microsoft.KeyVault/vaults@2023-07-01' existing = if(!keyVaultCreate){
  name:name
}
//output keyVaultName string=name//key.name
module keyVault 'br/public:security/keyvault:1.0.2' = if(keyVaultCreate){
  name: '${name}-keyVault-key'
  params: {
    roleAssignments:roleAssignments
    rbacPolicies:rbacPolicies
    name:name
    location: location
  }
}

output keyVaultName string=keyVaultCreate?keyVault.outputs.name:key.name
