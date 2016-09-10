Imports System.Net
Imports System.Web.Http
Imports tas = tinyak.API.Shared
Imports tc = tinyak.Core
Namespace Controllers.Api
    Public Class FeatureController
        Inherits clsApiControllerBase

        <HttpGet>
        Public Function [Get](ByVal id As Integer) As HttpResponseMessage
            Dim objSettings As clsSettings
            Dim objSession As tc.clsSession
            Dim objInnerUser As tc.clsUser
            Dim objFeature As tc.clsFeature
            Dim objProject As tc.clsProject
            Dim objResponse As tas.clsFeature

            objSettings = New clsSettings
            objSession = GetSession(objSettings)
            If objSession IsNot Nothing Then
                objInnerUser = objSession.GetUser(objSettings)
                objFeature = tc.clsFeature.Get(objSettings, id)
                If objFeature IsNot Nothing Then
                    objProject = objFeature.GetProject(objSettings)
                Else
                    objProject = Nothing
                End If
                If objInnerUser IsNot Nothing AndAlso objProject IsNot Nothing AndAlso objProject.IncludesMember(objInnerUser.EmailAddress) AndAlso objFeature IsNot Nothing Then
                    objResponse = New tas.clsFeature
                    With objResponse
                        .Id = objFeature.Id.Value
                        .ProjectId = objProject.Id.Value
                        .Title = objFeature.Title
                    End With
                    Return Request.CreateResponse(HttpStatusCode.OK, objResponse)
                Else
                    Return Request.CreateResponse(HttpStatusCode.Unauthorized)
                End If
            Else
                Return Request.CreateResponse(HttpStatusCode.Unauthorized)
            End If
        End Function

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

        <HttpPut>
        Public Function Update(ByVal id As Integer, ByVal objRequest As tas.clsFeature) As HttpResponseMessage
            Dim objSettings As clsSettings
            Dim objSession As tc.clsSession
            Dim objProject As tc.clsProject
            Dim objInnerUser As tc.clsUser
            Dim objFeature As tc.clsFeature
            Dim objResponse As tas.clsFeature

            objSettings = New clsSettings
            objSession = GetSession(objSettings)
            If objSession IsNot Nothing AndAlso objRequest IsNot Nothing Then
                objInnerUser = objSession.GetUser(objSettings)
                objFeature = tc.clsFeature.Get(objSettings, id)
                If objFeature IsNot Nothing Then
                    objProject = objFeature.GetProject(objSettings)
                Else
                    objProject = Nothing
                End If
                If objInnerUser IsNot Nothing AndAlso objProject IsNot Nothing AndAlso objProject.IncludesMember(objInnerUser.EmailAddress) AndAlso objFeature IsNot Nothing Then
                    If String.IsNullOrEmpty(objRequest.Title) = False Then
                        objFeature.Title = objRequest.Title.TrimEnd
                        objFeature.Update(objSettings)
                        objResponse = New tas.clsFeature() With {.Id = objFeature.Id.Value, .ProjectId = objProject.Id.Value, .Title = objFeature.Title}
                        Return Request.CreateResponse(HttpStatusCode.OK, objResponse)
                    Else
                        Return Request.CreateResponse(HttpStatusCode.BadRequest, "Title is required")
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