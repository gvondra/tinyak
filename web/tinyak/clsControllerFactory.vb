Imports tinyak.Core
Public Class clsControllerFactory
    Inherits DefaultControllerFactory

    Public Overrides Function CreateController(requestContext As RequestContext, controllerName As String) As IController
        Dim objType As Type
        Dim objSettings As clsSettings
        Dim objSession As clsSession
        Dim objController As Controller

        objType = GetControllerType(requestContext, controllerName)
        If objType IsNot Nothing AndAlso objType.IsSubclassOf(GetType(clsControllerBase)) Then
            objSettings = New clsSettings()
            objSession = GetSession(objSettings, requestContext)
            objController = CType(Activator.CreateInstance(objType, New Object() {objSettings, objSession}), Controller)
            If objSession IsNot Nothing AndAlso objSession.UserId.HasValue Then
                objController.ViewData.Add("UserId", objSession.UserId.Value)
            End If
        Else
            objController = CType(MyBase.CreateController(requestContext, controllerName), Controller)
        End If
        Return objController
    End Function

    Private Function GetSession(ByVal objSettings As clsSettings, ByVal requestContext As RequestContext) As clsSession
        Dim objCookie As HttpCookie
        Dim objSession As clsSession

        objSession = Nothing
        objCookie = requestContext.HttpContext.Request.Cookies.Get("SID")
        If objCookie IsNot Nothing Then
            objSession = clsSession.Get(objSettings, Guid.Parse(objCookie.Value))
        End If
        If objSession Is Nothing Then
            objSession = clsSession.CreateNew(objSettings)
            objCookie = New HttpCookie("SID")
            objCookie.Value = objSession.Id.ToString("N")
            objCookie.Expires = Date.MinValue
            objCookie.HttpOnly = True
            requestContext.HttpContext.Response.Cookies.Add(objCookie)
        End If
        Return objSession
    End Function
End Class
