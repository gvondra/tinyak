Imports System.Net
Imports System.Web.Http
Imports tas = tinyak.API.Shared
Imports tc = tinyak.Core
Namespace Controllers.Api
    <clsApiActionFilter, clsApiExceptionFilterAttribute>
    Public Class WorkItemController
        Inherits clsApiControllerBase

        <HttpGet>
        Public Function [Get](ByVal id As Integer) As HttpResponseMessage
            Dim objSettings As clsSettings
            Dim objSession As tc.clsSession
            Dim objInnerUser As tc.clsUser
            Dim objWorkItem As tc.clsWorkItem
            Dim objProject As tc.clsProject
            Dim objResponse As tas.clsWorkItem
            Dim objItteration As tc.clsItteration

            objSettings = New clsSettings
            objSession = GetSession(objSettings)
            If objSession IsNot Nothing Then
                objInnerUser = objSession.GetUser(objSettings)
                objWorkItem = tc.clsWorkItem.Get(objSettings, id)
                If objWorkItem IsNot Nothing Then
                    objProject = objWorkItem.GetProject(objSettings)
                Else
                    objProject = Nothing
                End If
                If objInnerUser IsNot Nothing AndAlso objProject IsNot Nothing AndAlso objProject.IncludesMember(objInnerUser.EmailAddress) AndAlso objWorkItem IsNot Nothing Then
                    objItteration = objWorkItem.GetItteration(objSettings)
                    objResponse = New tas.clsWorkItem
                    With objResponse
                        .AcceptanceCriteria = objWorkItem.AcceptanceCriteria
                        .AssignedTo = objWorkItem.AssignedTo
                        .Description = objWorkItem.Description
                        .Effort = objWorkItem.Effort
                        .Id = objWorkItem.Id
                        .State = CType(CType(objWorkItem.State, Int16), tas.enumWorkItemState)
                        .Title = objWorkItem.Title
                    End With
                    If objItteration IsNot Nothing Then
                        objResponse.Itteration = New tas.clsItteration With {.EndDate = objItteration.EndDate, .Id = objItteration.Id.Value, .IsActive = objItteration.IsActive, .Name = objItteration.Name, .StartDate = objItteration.StartDate}
                    Else
                        objResponse.Itteration = Nothing
                    End If
                    Return Request.CreateResponse(HttpStatusCode.OK, objResponse)
                Else
                    Return Request.CreateResponse(HttpStatusCode.Unauthorized)
                End If
            Else
                Return Request.CreateResponse(HttpStatusCode.Unauthorized)
            End If
        End Function

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

        <HttpPut>
        Public Function Update(ByVal id As Integer, ByVal objRequest As tas.clsWorkItem) As HttpResponseMessage
            Dim objSettings As clsSettings
            Dim objSession As tc.clsSession
            Dim objProject As tc.clsProject
            Dim objInnerUser As tc.clsUser
            Dim objWorkItem As tc.clsWorkItem
            Dim objResponse As tas.clsWorkItem
            Dim objItteration As tc.clsItteration

            objSettings = New clsSettings
            objSession = GetSession(objSettings)
            If objSession IsNot Nothing AndAlso objRequest IsNot Nothing Then
                objInnerUser = objSession.GetUser(objSettings)
                objWorkItem = tc.clsWorkItem.Get(objSettings, id)
                If objWorkItem IsNot Nothing Then
                    objProject = objWorkItem.GetProject(objSettings)
                Else
                    objProject = Nothing
                End If
                If objInnerUser IsNot Nothing AndAlso objProject IsNot Nothing AndAlso objProject.IncludesMember(objInnerUser.EmailAddress) AndAlso objWorkItem IsNot Nothing Then
                    If String.IsNullOrEmpty(objRequest.Title) = False Then
                        With objRequest
                            objWorkItem.AcceptanceCriteria = .AcceptanceCriteria
                            objWorkItem.AssignedTo = .AssignedTo
                            objWorkItem.Description = .Description
                            objWorkItem.Effort = .Effort
                            objWorkItem.State = CType(CType(.State, Int16), tc.clsWorkItem.enumState)
                            objWorkItem.Title = .Title
                        End With

                        If objRequest.Itteration IsNot Nothing AndAlso objRequest.Itteration.Id.HasValue Then
                            objItteration = tc.clsItteration.Get(objSettings, objRequest.Itteration.Id.Value)
                        Else
                            objItteration = Nothing
                        End If
                        objWorkItem.SetItteration(objItteration)

                        objWorkItem.Update(objSettings)
                        objResponse = New tas.clsWorkItem
                        With objResponse
                            .AcceptanceCriteria = objWorkItem.AcceptanceCriteria
                            .AssignedTo = objWorkItem.AssignedTo
                            .Description = objWorkItem.Description
                            .Effort = objWorkItem.Effort
                            .State = CType(CType(objWorkItem.State, Int16), tas.enumWorkItemState)
                            .Title = objWorkItem.Title
                        End With
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