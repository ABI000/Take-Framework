
//ACR
@description('The name of the ACR')
param registryName string 

@description('Location for all resources.')
param location string = resourceGroup().location

param Create bool =true
@allowed([
  'Basic'
  'Standard'
  'Premium'
])
param skuName string='Basic'

resource acr 'Microsoft.ContainerRegistry/registries@2023-07-01' existing=if(!Create){
name: registryName
}

module containerRegistry 'br/public:compute/container-registry:1.1.1' =if(Create) {
  name: '${registryName}-container-registry'
  params: {
    name: registryName
    location: location
    adminUserEnabled:true
    skuName:skuName
  }
}
output containerRegistryObj object =Create?{
name:containerRegistry.outputs.name
resourceId:containerRegistry.outputs.resourceId
loginServer:containerRegistry.outputs.loginServer
} :{

  name:registryName
  resourceId:acr.id
  loginServer:acr.properties.loginServer
}


