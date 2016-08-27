Imports System.Web.Mvc
Imports tinyak.Core
Namespace Controllers
    <Authorize()>
    Public Class ProjectController
        Inherits clsControllerBase

        Public Sub New(ByVal objSettings As clsSettings, ByVal objSession As clsSession)
            MyBase.New(objSettings, objSession)
        End Sub

        Public Function List() As ActionResult
            Dim objUser As clsUser
            Dim colProject As List(Of clsProject)
            Dim objProject As clsProject
            Dim objModel As clsProjectListModel
            Dim objItem As clsProjectListItemModel

            objUser = Session.GetUser(Settings)
            colProject = clsProject.GetByOwnerId(Settings, objUser.Id.Value)
            objModel = New clsProjectListModel
            objModel.Items = New List(Of clsProjectListItemModel)
            For Each objProject In colProject
                objItem = New clsProjectListItemModel
                objItem.Id = objProject.Id.Value
                objItem.Title = objProject.Title
                objModel.Items.Add(objItem)
            Next objProject

            Return View(objModel)
        End Function

        Public Function Create() As ActionResult
            Return View(New clsProjectCreateModel)
        End Function

        <ValidateAntiForgeryToken, HttpPost>
        Public Function Create(<FromBody> ByVal objModel As clsProjectCreateModel) As ActionResult
            Dim objProject As clsProject
            Dim objUser As clsUser

            Validate(objModel)
            If ModelState.IsValid Then
                objUser = Session.GetUser(Settings)
                objProject = clsProject.GetNew(objUser, objModel.ProjectTitle)
                objProject.Create(Settings)
                Return RedirectToAction("Update", New With {.id = objProject.Id.Value})
            Else
                Return View(objModel)
            End If
        End Function

        Private Sub Validate(ByVal objModel As clsProjectCreateModel)
            If String.IsNullOrEmpty(objModel.ProjectTitle) Then
                ModelState.AddModelError("ProjectTitle", "Title is required")
            End If
        End Sub

        Public Function Update(ByVal id As Integer) As ActionResult
            Dim objModel As clsProjectUpdateModel
            Dim objProject As clsProject

            objProject = clsProject.Get(Settings, id)
            If objProject IsNot Nothing AndAlso objProject.CanUserUpdate(Session.GetUser(Settings)) Then
                objModel = New clsProjectUpdateModel
                objModel.Id = objProject.Id.Value
                objModel.ProjectTitle = objProject.Title
                objModel.ProjectMembers = New clsProjectMembersModel
                objModel.ProjectMembers.ProjectId = id
                objModel.ProjectMembers.Members = objProject.ProjectMembers
                Return View("Update", objModel)
            Else
                Return New HttpStatusCodeResult(HttpStatusCode.Unauthorized)
            End If
        End Function

        <ValidateAntiForgeryToken, HttpPost>
        Public Function Update(ByVal id As Integer, ByVal objModel As clsProjectUpdateModel) As ActionResult
            Dim objProject As clsProject

            objProject = clsProject.Get(Settings, id)
            If objProject IsNot Nothing AndAlso objProject.CanUserUpdate(Session.GetUser(Settings)) Then
                objModel.Id = objProject.Id.Value
                objModel.ProjectMembers = New clsProjectMembersModel
                objModel.ProjectMembers.ProjectId = id
                objModel.ProjectMembers.Members = objProject.ProjectMembers
                Validate(objModel)
                If ModelState.IsValid Then
                    objProject.Title = objModel.ProjectTitle
                    objProject.Update(Settings)
                End If
                Return View(objModel)
            Else
                Return New HttpStatusCodeResult(HttpStatusCode.Unauthorized)
            End If
        End Function

        Public Function AddProjectMember(ByVal id As Integer) As ActionResult
            Return Update(id)
        End Function

        Private Sub Validate(ByVal objModel As clsProjectUpdateModel)
            If String.IsNullOrEmpty(objModel.ProjectTitle) Then
                ModelState.AddModelError("ProjectTitle", "Title is required")
            End If
        End Sub

        <HttpPost, ActionName("ProjectMember")>
        Public Function PostProjectMember(ByVal id As Integer, ByVal emailAddress As String) As ActionResult
            Dim objProject As clsProject
            Dim objModel As clsProjectCreateProjectMemberModel

            objProject = clsProject.Get(Settings, id)
            If objProject IsNot Nothing AndAlso objProject.CanUserUpdate(Session.GetUser(Settings)) Then
                If String.IsNullOrEmpty(emailAddress) = False Then
                    objProject.AddMember(Settings, emailAddress)
                End If
                objModel = New clsProjectCreateProjectMemberModel() With {.ProjectId = id, .EmailAddress = emailAddress}
                Return New JsonResult() With {.Data = objModel}
            Else
                Return New HttpStatusCodeResult(HttpStatusCode.Unauthorized)
            End If
        End Function

        <HttpDelete, ActionName("ProjectMember")>
        Public Function DeleteProjectMember(ByVal id As Integer, ByVal emailAddress As String) As ActionResult
            Dim objProject As clsProject

            objProject = clsProject.Get(Settings, id)
            If objProject IsNot Nothing AndAlso objProject.CanUserUpdate(Session.GetUser(Settings)) Then
                If String.IsNullOrEmpty(emailAddress) = False Then
                    objProject.RemoveMember(Settings, emailAddress)
                End If
                Return New ContentResult() With {.Content = "Gone"}
            Else
                Return New HttpStatusCodeResult(HttpStatusCode.Unauthorized)
            End If
        End Function

        <HttpGet>
        Public Function ProjectMembers(ByVal id As Integer) As ActionResult
            Dim objProject As clsProject
            Dim objModel As clsProjectMembersModel

            objProject = clsProject.Get(Settings, id)
            If objProject IsNot Nothing AndAlso objProject.CanUserUpdate(Session.GetUser(Settings)) Then
                objModel = New clsProjectMembersModel
                objModel.ProjectId = id
                objModel.Members = objProject.ProjectMembers
                Return View(objModel)
            Else
                Return New HttpStatusCodeResult(HttpStatusCode.Unauthorized)
            End If

        End Function
    End Class
End Namespace