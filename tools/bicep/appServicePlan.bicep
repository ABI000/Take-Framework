param Create bool=true
@description('The Name of the App Service Plan.')
param appServicePlanName string
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
param skuname string ='F1'
@description('The Tier of the Sku.')
@allowed([
'Shared'   
'Free'
'Dedicated'     
'Basic'   
'Standard'
'Premium' 
'PremiumV2'
'Isolated'
'Dynamic'
])  
param skutier string = 'Free'

@description('Location for all resources.')
param location string = resourceGroup().location

resource saExisting 'Microsoft.Web/serverfarms@2020-12-01' existing = if (!Create) {
  name: appServicePlanName
}
//output appServicePlan object=saExisting
resource appServicePlan 'Microsoft.Web/serverfarms@2020-12-01' = if (Create){
  name: appServicePlanName
  location: location
  kind: 'linux'
  properties: {
    reserved: true
  }	
  sku:  {
  	name: skuname
    tier: skutier
  }
}
output appServicePlan object=Create?appServicePlan:saExisting
