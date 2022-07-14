resource "azurerm_resource_group" "tekken-rg" {
  name     = "${var.resource_group_name}-${var.environment}"
  location = var.location
}

