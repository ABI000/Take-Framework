param webname string
param properties object
resource webapp 'Microsoft.Web/sites@2022-09-01' existing = {
  name: webname
}

resource addwebConfig 'Microsoft.Web/sites/config@2022-09-01' = {
  name: 'appsettings'
  parent: webapp
  properties: properties
}
