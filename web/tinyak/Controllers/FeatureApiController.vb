Imports System.Net
Imports System.Web.Http
Imports tas = tinyak.API.Shared
Imports tc = tinyak.Core
Namespace Controllers.Api
    Public Class FeatureController
        Inherits clsApiControllerBase

        <HttpPost>
        Public Function Create(ByVal projectId As Integer, ByVal objRequest As tas.clsCreateFeatureRequest) As HttpResponseMessage
            Dim objSettings As clsSettings
            Dim objSession As tc.clsSession
            Dim objProject As tc.clsProject
            Dim objInnerUser As tc.clsUser
            Dim objFeature As tc.clsFeature
            Dim objResponse As tas.clsCreateFeatureResponse

            objSettings = New clsSettings
            objSession = GetSession(objSettings)
            If objSession IsNot Nothing Then
                objInnerUser = objSession.GetUser(objSettings)
                objProject = tc.clsProject.Get(objSettings, projectId)
                If objInnerUser IsNot Nothing AndAlso objProject IsNot Nothing AndAlso objProject.IncludesMember(objInnerUser.EmailAddress) Then
                    objResponse = New tas.clsCreateFeatureResponse
                    If String.IsNullOrEmpty(objRequest.Title) = False Then
                        objFeature = objProject.GetNewFeature()
                        objFeature.Title = objRequest.Title.TrimEnd
                        objFeature.Create(objSettings)
                        objResponse.Feature = New tas.clsFeatureListItem() With {.Id = objFeature.Id.Value, .Title = objFeature.Title}
                        Return Request.CreateResponse(HttpStatusCode.OK, objResponse)
                    Else
                        objResponse.ErrorMessage = "Title is required"
                        Return Request.CreateResponse(HttpStatusCode.BadRequest, objResponse)
                    End If

                Else
                    Return Request.CreateResponse(HttpStatusCode.Unauthorized)
                End If
            Else
                Return Request.CreateResponse(HttpStatusCode.Unauthorized)
            End If
        End Function
    End Class
End Namespace