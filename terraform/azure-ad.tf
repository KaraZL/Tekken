#create azuread group
resource "azuread_group" "aks-administrators" {
    display_name     = "${azurerm_resource_group.tekken-rg.name}-admins"
    security_enabled = true
}