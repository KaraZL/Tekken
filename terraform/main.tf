terraform {
  required_providers{
      azurerm = {
      source  = "hashicorp/azurerm"
      version = "=3.11.0"
    }

    azuread = {
      source = "hashicorp/azuread"
      version = "=2.25.0"
    }

    random = {
      source = "hashicorp/random"
      version = "=3.3.2"
    }
  }

  backend "azurerm" {
      resource_group_name = "terraform-storage-rg"
      storage_account_name = "tekkenterraformstorage"
      container_name = "tfstatefiles"
      key = "terraform.tfstate"
      # #key = "terraform.tfstate"
      # #key = "dev.terraform.tfstate"
  }
  
}



provider "azurerm" {
  features {
    
  }
}