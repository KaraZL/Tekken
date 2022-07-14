data "azurerm_kubernetes_service_versions" "current" {
  location = azurerm_resource_group.tekken-rg.location
  include_preview = false
}

