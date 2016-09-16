Imports System.Net
Imports System.Web.Http
Imports tas = tinyak.API.Shared
Imports tc = tinyak.Core
Namespace Controllers.Api
    ' don't as this will log passwords <clsApiActionFilter>
    Public Class UserController
        Inherits clsApiControllerBase

        <HttpGet>
        Public Function GetUser(ByVal emailAddress As String, ByVal password As String) As HttpResponseMessage
            Dim objSession As tc.clsSession
            Dim objSettings As clsSettings
            Dim objInnerUser As tc.clsUser
            Dim objResult As tas.clsUser

            objSettings = New clsSettings
            objSession = GetSession(objSettings)
            If objSession IsNot Nothing Then
                objInnerUser = tc.clsUser.GetByEmailAddress(objSettings, emailAddress, password)
                If objInnerUser IsNot Nothing Then
                    objSession.UserId = objInnerUser.Id
                    objSession.Save(objSettings)
                    objResult = New tas.clsUser() With {.Name = objInnerUser.Name}
                    Return Request.CreateResponse(HttpStatusCode.OK, objResult)
                Else
                    Return Request.CreateResponse(HttpStatusCode.Unauthorized)
                End If
            Else
                Return Request.CreateResponse(HttpStatusCode.Unauthorized)
            End If
        End Function
    End Class
End Namespace