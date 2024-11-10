param openAiLocation string
param workloadName string = 'cuisine'
param locationShorthand object
param principalId string // Add this parameter to receive the principalId

var openAiShortHand = locationShorthand[openAiLocation]

resource openai 'Microsoft.CognitiveServices/accounts@2024-06-01-preview' = {
  name: 'oai-${workloadName}-${openAiShortHand}'
  location: openAiLocation
  identity: {
    type: 'SystemAssigned'
  }
  sku: {
    name: 'S0'
  }
  kind: 'OpenAI'
  properties: {
    publicNetworkAccess: 'Enabled'
    networkAcls: {
      defaultAction: 'Allow'
    }
    disableLocalAuth: true
    customSubDomainName: 'oai-${workloadName}-${openAiShortHand}'
  }
}

resource model 'Microsoft.CognitiveServices/accounts/deployments@2024-06-01-preview' = {
  parent: openai
  name: 'gpt-4o'
  sku: {
    name: 'Standard'
    capacity: 10
  }
  properties: {
    model: {
      format: 'OpenAI'
      name: 'gpt-4o'
      version: '2024-08-06'
    }
    versionUpgradeOption: 'OnceNewDefaultVersionAvailable'
    currentCapacity: 10
    raiPolicyName: 'Microsoft.DefaultV2'
  }
}

resource contentFilter 'Microsoft.CognitiveServices/accounts/raiPolicies@2024-06-01-preview' = {
  parent: openai
  name: 'cuisine-filter'
  properties: {
    basePolicyName: 'Microsoft.DefaultV2'
    mode: 'Blocking'
    contentFilters: [
      {
        name: 'Hate'
        severityThreshold: 'Medium'
        blocking: true
        enabled: true
        source: 'Prompt'
      }
      {
        name: 'Hate'
        severityThreshold: 'Medium'
        blocking: true
        enabled: true
        source: 'Completion'
      }
      {
        name: 'Sexual'
        severityThreshold: 'Medium'
        blocking: true
        enabled: true
        source: 'Prompt'
      }
      {
        name: 'Sexual'
        severityThreshold: 'Medium'
        blocking: true
        enabled: true
        source: 'Completion'
      }
      {
        name: 'Violence'
        severityThreshold: 'Medium'
        blocking: true
        enabled: true
        source: 'Prompt'
      }
      {
        name: 'Violence'
        severityThreshold: 'Medium'
        blocking: true
        enabled: true
        source: 'Completion'
      }
      {
        name: 'Selfharm'
        severityThreshold: 'Medium'
        blocking: true
        enabled: true
        source: 'Prompt'
      }
      {
        name: 'Selfharm'
        severityThreshold: 'Medium'
        blocking: true
        enabled: true
        source: 'Completion'
      }
      {
        name: 'Jailbreak'
        blocking: true
        enabled: true
        source: 'Prompt'
      }
      {
        name: 'Protected Material Text'
        blocking: true
        enabled: true
        source: 'Completion'
      }
      {
        name: 'Protected Material Code'
        blocking: true
        enabled: true
        source: 'Completion'
      }
    ]
  }
}

resource dalle 'Microsoft.CognitiveServices/accounts/deployments@2024-06-01-preview' = {
  parent: openai
  name: 'DALL-E3'
  sku: {
    name: 'Standard'
    capacity: 1
  }
  properties: {
    model: {
      format: 'OpenAI'
      name: 'dall-e-3'
      version: ' 3.0' 
    }
    versionUpgradeOption: 'OnceNewDefaultVersionAvailable'
    currentCapacity: 1
    raiPolicyName: 'Microsoft.DefaultV2'
  }
}

resource monitoringReaderRoleDefinition 'Microsoft.Authorization/roleDefinitions@2022-04-01' existing = {
  scope: subscription()
  name: '5e0bd9bd-7b93-4f28-af87-19fc36ad61bd' // Cognitive Services OpenAI User
}

resource roleAssignment 'Microsoft.Authorization/roleAssignments@2020-04-01-preview' = {
  name: guid(openai.id, principalId, monitoringReaderRoleDefinition.id)
  scope: openai
  properties: {
    roleDefinitionId: monitoringReaderRoleDefinition.id
    principalId: principalId
  }
}

output openai object = {
  id: openai.id
  endpoint: openai.properties.endpoint
}
