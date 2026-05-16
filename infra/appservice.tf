resource "azurerm_linux_web_app" "backend" {
  name                = var.backend_app_name
  location            = azurerm_resource_group.rg.location
  resource_group_name = azurerm_resource_group.rg.name
  service_plan_id     = azurerm_service_plan.plan.id

  site_config {
    application_stack {
      dotnet_version = "8.0"
    }
  }

  app_settings = {
    "ASPNETCORE_ENVIRONMENT" = "Production"
  }

  https_only = true
}