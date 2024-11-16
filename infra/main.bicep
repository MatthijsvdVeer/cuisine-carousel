@description('Resource Location')
param location string = resourceGroup().location

@description('OpenAI Location')
param openAiLocation string

@description('Workload Name')
param workloadName string = 'cuisine'

var locationShorthand = {
  eastus: 'eus'
  northeurope: 'neu'
  westeurope: 'weu'
  swedencentral: 'swe'
}

var shorthand = locationShorthand[location]

resource logAnalyticsWorkspace 'Microsoft.OperationalInsights/workspaces@2021-06-01' = {
  name: 'law-${workloadName}-${shorthand}'
  location: location
  properties: {
    sku: {
      name: 'PerGB2018'
    }
  }
}

resource appInsights 'Microsoft.Insights/components@2020-02-02' = {
  name: 'appi-${workloadName}-${shorthand}'
  kind: 'web'
  location: location
  properties: {
    Application_Type: 'web'
    WorkspaceResourceId: logAnalyticsWorkspace.id
  }
}

resource appServicePlan 'Microsoft.Web/serverfarms@2023-12-01' = {
  name: 'asp-${workloadName}-${shorthand}'
  location: location
  sku: {
    name: 'B1'
    tier: 'Basic'
  }
  properties: {
    reserved: true
  }
}

resource appService 'Microsoft.Web/sites@2023-12-01' = {
  name: 'app-${workloadName}-${shorthand}'
  location: location
  identity: {
    type: 'SystemAssigned'
  }
  properties: {
    serverFarmId: appServicePlan.id
    siteConfig: {
      linuxFxVersion: 'DOTNETCORE|8.0'
      alwaysOn: true
      appSettings: [
        {
          name: 'APPINSIGHTS_INSTRUMENTATIONKEY'
          value: appInsights.properties.InstrumentationKey
        }
        {
          name: 'APPLICATIONINSIGHTS_CONNECTION_STRING'
          value: 'InstrumentationKey=${appInsights.properties.InstrumentationKey}'
        }
      ]
    }
  }
}

module openAiModule 'openai.bicep' = {
  name: 'openAiModule'
  params: {
    openAiLocation: openAiLocation
    workloadName: workloadName
    locationShorthand: locationShorthand
    principalId: appService.identity.principalId
  }
}

module storageModule 'storage.bicep' = {
  name: 'storageModule'
  params: {
    location: location
    workloadName: workloadName
    principalId: appService.identity.principalId
  }
}

output openAiEndpoint string = openAiModule.outputs.openai.endpoint
output appServiceName string = appService.name
