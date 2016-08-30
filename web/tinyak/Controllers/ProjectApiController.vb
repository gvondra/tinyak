Imports System.Net
Imports System.Web.Http
Imports tas = tinyak.API.Shared
Imports tc = tinyak.Core
Namespace Controllers.Api
    Public Class ProjectController
        Inherits clsApiControllerBase

        <HttpGet>
        Public Function GetProjects() As HttpResponseMessage
            Dim objSession As tc.clsSession
            Dim objSettings As clsSettings
            Dim objInnerUser As tc.clsUser
            Dim colProject As List(Of tc.clsProject)
            Dim i As Integer
            Dim arrResult() As tas.clsProject

            objSettings = New clsSettings
            objSession = GetSession(objSettings)
            If objSession IsNot Nothing Then
                objInnerUser = objSession.GetUser(objSettings)
                If objInnerUser IsNot Nothing Then
                    colProject = tc.clsProject.GetByEmailAddress(objSettings, objInnerUser.EmailAddress)
                    If colProject IsNot Nothing AndAlso colProject.Count > 0 Then
                        ReDim arrResult(colProject.Count - 1)
                        For i = 0 To colProject.Count - 1
                            arrResult(i) = New tas.clsProject
                            arrResult(i).Title = colProject(i).Title
                        Next i
                    Else
                        arrResult = Nothing
                    End If
                    Return Request.CreateResponse(HttpStatusCode.OK, arrResult)
                Else
                    Return Request.CreateResponse(HttpStatusCode.Unauthorized)
                End If
            Else
                Return Request.CreateResponse(HttpStatusCode.Unauthorized)
            End If
        End Function
    End Class
End Namespace