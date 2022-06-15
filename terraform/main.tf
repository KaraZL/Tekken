terraform {
  required_providers{
      azurerm = {
      source  = "hashicorp/azurerm"
      version = "=3.0.0"
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