Imports System.Net
Imports System.Web.Http
Imports tas = tinyak.API.Shared
Imports tc = tinyak.Core
Namespace Controllers.Api
    Public Class WorkItemController
        Inherits clsApiControllerBase

        <HttpPost>
        Public Function Create(ByVal projectId As Integer, ByVal objRequest As tas.clsCreateWorkItemRequest) As HttpResponseMessage
            Dim objSettings As clsSettings
            Dim objSession As tc.clsSession
            Dim objProject As tc.clsProject
            Dim objInnerUser As tc.clsUser
            Dim objFeature As tc.clsFeature
            Dim objWorkItem As tc.clsWorkItem
            Dim objResponse As tas.clsCreateWorkItemResponse

            objSettings = New clsSettings
            objSession = GetSession(objSettings)
            If objSession IsNot Nothing Then
                objInnerUser = objSession.GetUser(objSettings)
                objProject = tc.clsProject.Get(objSettings, projectId)
                If objInnerUser IsNot Nothing AndAlso objProject IsNot Nothing AndAlso objProject.IncludesMember(objInnerUser.EmailAddress) Then
                    objFeature = tc.clsFeature.Get(objSettings, objRequest.FeatureId)
                    objResponse = New tas.clsCreateWorkItemResponse
                    If objFeature IsNot Nothing Then
                        If String.IsNullOrEmpty(objRequest.Title) = False Then
                            objWorkItem = objFeature.GetNewWorkItem
                            objWorkItem.Title = objRequest.Title.TrimEnd
                            objWorkItem.Create(objSettings)
                            objResponse.WorkItem = New tas.clsWorkListItem()
                            With objResponse.WorkItem
                                .AssignedTo = objWorkItem.AssignedTo
                                .Effort = objWorkItem.Effort
                                .Id = objWorkItem.Id.Value
                                .State = CType(CType(objWorkItem.State, Int16), tas.enumWorkItemState)
                                .Title = objWorkItem.Title
                            End With
                            Return Request.CreateResponse(HttpStatusCode.OK, objResponse)
                        Else
                            objResponse.ErrorMessage = "Title is required"
                            Return Request.CreateResponse(HttpStatusCode.BadRequest, objResponse)
                        End If
                    Else
                        objResponse.ErrorMessage = "Feature not found for id " & objRequest.FeatureId.ToString
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