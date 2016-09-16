Imports System.Net
Imports System.Web.Http
Imports tas = tinyak.API.Shared
Imports tc = tinyak.Core
Namespace Controllers.Api
    <clsApiActionFilter, clsApiExceptionFilterAttribute>
    Public Class ItterationsController
        Inherits clsApiControllerBase

        <HttpGet>
        Public Function [Get](ByVal projectId As Integer) As HttpResponseMessage
            Dim objSettings As clsSettings
            Dim objSession As tc.clsSession
            Dim objInnerUser As tc.clsUser
            Dim objProject As tc.clsProject
            Dim colItteration As List(Of tc.clsItteration)
            Dim arrResult() As tas.clsItteration
            Dim i As Integer

            objSettings = New clsSettings
            objSession = GetSession(objSettings)
            If objSession IsNot Nothing Then
                objInnerUser = objSession.GetUser(objSettings)
                objProject = tc.clsProject.Get(objSettings, projectId)
                If objInnerUser IsNot Nothing AndAlso objProject IsNot Nothing AndAlso objProject.IncludesMember(objInnerUser.EmailAddress) Then
                    colItteration = objProject.GetItterations(objSettings)
                    If colItteration IsNot Nothing AndAlso colItteration.Count > 0 Then
                        ReDim arrResult(colItteration.Count - 1)
                        For i = 0 To colItteration.Count - 1
                            arrResult(i) = New tas.clsItteration()
                            With arrResult(i)
                                .EndDate = colItteration(i).EndDate
                                .Id = colItteration(i).Id.Value
                                .IsActive = colItteration(i).IsActive
                                .Name = colItteration(i).Name
                                .StartDate = colItteration(i).StartDate
                            End With
                        Next
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