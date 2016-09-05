Imports System.Net
Imports System.Web.Http
Imports tas = tinyak.API.Shared
Imports tc = tinyak.Core
Namespace Controllers.Api
    Public Class FeaturesController
        Inherits clsApiControllerBase

        <HttpGet>
        Public Function GetFeatures(ByVal projectId As Integer) As HttpResponseMessage
            Dim objSettings As clsSettings
            Dim objSession As tc.clsSession
            Dim objProject As tc.clsProject
            Dim objInnerUser As tc.clsUser
            Dim colFeature As List(Of tc.clsFeature)
            Dim arrResult() As tas.clsFeatureListItem
            Dim i As Integer

            objSettings = New clsSettings
            objSession = GetSession(objSettings)
            If objSession IsNot Nothing Then
                objInnerUser = objSession.GetUser(objSettings)
                objProject = tc.clsProject.Get(objSettings, projectId)
                If objInnerUser IsNot Nothing AndAlso objProject IsNot Nothing AndAlso objProject.IncludesMember(objInnerUser.EmailAddress) Then
                    colFeature = objProject.GetFeatures(objSettings)
                    If colFeature IsNot Nothing Then
                        ReDim arrResult(colFeature.Count - 1)
                        For i = 0 To colFeature.Count - 1
                            arrResult(i) = New tas.clsFeatureListItem() With {.Id = colFeature(i).Id.Value, .Title = colFeature(i).Title}
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