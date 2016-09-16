Imports System.Net
Imports System.Web.Http
Imports tas = tinyak.API.Shared
Imports tc = tinyak.Core
Namespace Controllers.Api
    <clsApiActionFilter, clsApiExceptionFilterAttribute>
    Public Class WorkItemsController
        Inherits clsApiControllerBase

        <HttpGet>
        Public Function GetFeatures(ByVal featureId As Integer) As HttpResponseMessage
            Dim objSettings As clsSettings
            Dim objSession As tc.clsSession
            Dim objProject As tc.clsProject
            Dim objFeature As tc.clsFeature
            Dim objInnerUser As tc.clsUser
            Dim colWorkItem As List(Of tc.clsWorkItem)
            Dim arrResult() As tas.clsWorkListItem
            Dim i As Integer
            Dim objItteration As tc.clsItteration

            objSettings = New clsSettings
            objSession = GetSession(objSettings)
            If objSession IsNot Nothing Then
                objInnerUser = objSession.GetUser(objSettings)
                objFeature = tc.clsFeature.Get(objSettings, featureId)
                If objFeature IsNot Nothing Then
                    objProject = objFeature.GetProject(objSettings)
                Else
                    objProject = Nothing
                End If
                If objInnerUser IsNot Nothing AndAlso objProject IsNot Nothing AndAlso objProject.IncludesMember(objInnerUser.EmailAddress) AndAlso objFeature IsNot Nothing Then
                    colWorkItem = objFeature.GetWorkItems(objSettings)
                    objProject.LoadWorkItemItteration(objSettings, colWorkItem)
                    If colWorkItem IsNot Nothing Then
                        ReDim arrResult(colWorkItem.Count - 1)
                        For i = 0 To colWorkItem.Count - 1
                            arrResult(i) = New tas.clsWorkListItem() With {.AssignedTo = colWorkItem(i).AssignedTo, .Effort = colWorkItem(i).Effort, .Id = colWorkItem(i).Id.Value, .State = CType(CType(colWorkItem(i).State, Int16), tas.enumWorkItemState), .Title = colWorkItem(i).Title}
                            objItteration = colWorkItem(i).GetItteration(objSettings)
                            If objItteration IsNot Nothing Then
                                arrResult(i).Itteration = New tas.clsItteration() With {.EndDate = objItteration.EndDate, .Id = objItteration.Id.Value, .IsActive = objItteration.IsActive, .Name = objItteration.Name, .StartDate = objItteration.StartDate}
                            Else
                                arrResult(i).Itteration = Nothing
                            End If
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