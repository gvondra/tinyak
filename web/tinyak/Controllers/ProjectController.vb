Imports System.Web.Mvc

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

        Public Function Update(ByVal id As Integer) As ActionResult
            Dim objModel As clsProjectUpdateModel
            Dim objProject As clsProject

            objProject = clsProject.Get(Settings, id)
            If objProject IsNot Nothing AndAlso objProject.CanUserUpdate(Session.GetUser(Settings)) Then
                objModel = New clsProjectUpdateModel
                objModel.ProjectTitle = objProject.Title
                Return View(objModel)
            Else
                Return New HttpStatusCodeResult(HttpStatusCode.Unauthorized)
            End If
        End Function

        Private Sub Validate(ByVal objModel As clsProjectCreateModel)
            If String.IsNullOrEmpty(objModel.ProjectTitle) Then
                ModelState.AddModelError("ProjectTitle", "Title is required")
            End If
        End Sub
    End Class
End Namespace