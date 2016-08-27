Imports System.Web.Optimization

Public Class MvcApplication
    Inherits System.Web.HttpApplication

    Protected Sub Application_Start()
        ControllerBuilder.Current.SetControllerFactory(GetType(clsControllerFactory))
        AreaRegistration.RegisterAllAreas()
        FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters)
        WebApiConfig.Register(GlobalConfiguration.Configuration)
        RouteConfig.RegisterRoutes(RouteTable.Routes)
        BundleConfig.RegisterBundles(BundleTable.Bundles)
        GlobalConfiguration.Configuration.EnsureInitialized()
    End Sub
End Class
