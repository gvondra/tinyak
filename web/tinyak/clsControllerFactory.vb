﻿
Public Class clsControllerFactory
    Inherits DefaultControllerFactory

    Public Overrides Function CreateController(requestContext As RequestContext, controllerName As String) As IController
        Dim objType As Type
        Dim objSettings As clsSettings

        objType = GetControllerType(requestContext, controllerName)
        If objType.IsSubclassOf(GetType(clsControllerBase)) Then
            objSettings = New clsSettings()
            Return CType(Activator.CreateInstance(objType, New Object() {objSettings, GetSession(objSettings, requestContext)}), IController)
        Else
            Return MyBase.CreateController(requestContext, controllerName)
        End If
    End Function

    Private Function GetSession(ByVal objSetings As clsSettings, ByVal requestContext As RequestContext) As clsSession
        Dim objCookie As HttpCookie
        Dim objSession As clsSession
        Dim objService As clsSessionService

        objService = New clsSessionService(objSetings)
        objCookie = requestContext.HttpContext.Request.Cookies.Get("SID")
        If objCookie Is Nothing Then
            objSession = objService.Create
            objCookie = New HttpCookie("SID")
            objCookie.Value = objSession.Id.ToString("N")
            objCookie.Expires = objSession.ExpirationDate
            objCookie.HttpOnly = True
            requestContext.HttpContext.Response.Cookies.Add(objCookie)
        Else
            objSession = objService.Get(Guid.Parse(objCookie.Value))
        End If
        Return objSession
    End Function
End Class