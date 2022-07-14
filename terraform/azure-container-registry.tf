resource "azurerm_container_registry" "tekken-acr" {
  name                = "tekkenacr"
  resource_group_name = azurerm_resource_group.tekken-rg.name
  location            = azurerm_resource_group.tekken-rg.location
  sku                 = "Basic"
  admin_enabled       = false
}