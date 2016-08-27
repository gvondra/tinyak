Imports System.Net
Imports System.Web.Http
Imports tas = tinyak.API.Shared
Imports tc = tinyak.Core
Namespace Controllers.Api
    Public Class UserController
        Inherits ApiController

        <HttpGet>
        Public Function GetUser(ByVal emailAddress As String, ByVal password As String) As HttpResponseMessage
            Dim colSession As List(Of String)
            Dim objSessionId As Guid
            Dim objSession As tc.clsSession
            Dim objSettings As clsSettings
            Dim objInnerUser As tc.clsUser
            Dim objResult As tas.clsUser

            objSettings = New clsSettings
            colSession = New List(Of String)(Request.Headers.GetValues(tas.clsConstant.HEADER_SESSION_ID))
            If colSession.Count > 0 Then
                objSessionId = Guid.Parse(colSession(0))
                objSession = tc.clsSession.Get(objSettings, objSessionId)
                objInnerUser = tc.clsUser.GetByEmailAddress(objSettings, emailAddress, password)
                If objInnerUser IsNot Nothing Then
                    objSession.UserId = objInnerUser.Id
                    objSession.Save(objSettings)
                    objResult = New tas.clsUser() With {.Name = objInnerUser.Name}
                    Return Request.CreateResponse(HttpStatusCode.OK, objResult)
                Else
                    Return Request.CreateResponse(HttpStatusCode.Unauthorized)
                End If
            End If
            Return Request.CreateResponse(HttpStatusCode.Unauthorized)
        End Function
    End Class
End Namespace