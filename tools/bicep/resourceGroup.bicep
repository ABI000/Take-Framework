targetScope='subscription'
param Create bool=true
param resourceGroupName string
param resourceGroupLocation string

resource existingRg 'Microsoft.Resources/resourceGroups@2022-09-01' existing =if(!Create) {
  name: resourceGroupName
}
resource newRG 'Microsoft.Resources/resourceGroups@2022-09-01' = if(Create) {
  name: resourceGroupName
  location: resourceGroupLocation
}
output  resourceGroupLocation string = Create?newRG.location:existingRg.location
output  resourceGroupName string = Create?newRG.name:existingRg.name

