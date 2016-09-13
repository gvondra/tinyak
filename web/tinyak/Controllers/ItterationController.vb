Imports System.Web.Mvc
Imports tinyak.Core
Namespace Controllers
    <Authorize()>
    Public Class ItterationController
        Inherits clsControllerBase

        Public Sub New(ByVal objSettings As clsSettings, ByVal objSession As clsSession)
            MyBase.New(objSettings, objSession)
        End Sub

        Public Function [Get](ByVal id As Integer) As ActionResult
            Dim objUser As clsUser
            Dim objModel As clsItterationModel
            Dim objItteration As clsItteration
            Dim objProject As clsProject


            objUser = Session.GetUser(Settings)
            objItteration = clsItteration.Get(Settings, id)
            If objItteration IsNot Nothing Then
                objProject = objItteration.GetProject(Settings)
            Else
                objProject = Nothing
            End If
            If objUser IsNot Nothing AndAlso objProject IsNot Nothing AndAlso objProject.CanUserUpdate(Session.GetUser(Settings)) AndAlso objItteration IsNot Nothing Then
                objModel = New clsItterationModel
                objModel.Load(objItteration)
                Return View("GetItteration", objModel)
            Else
                Return New HttpStatusCodeResult(HttpStatusCode.Unauthorized)
            End If
        End Function

        Public Function List(ByVal projectId As Integer) As ActionResult
            Dim objUser As clsUser
            Dim objProject As clsProject
            Dim colItteration As List(Of clsItteration)
            Dim objItteration As clsItteration
            Dim objModel As clsItterationListModel
            Dim objItterationModel As clsItterationModel

            objUser = Session.GetUser(Settings)
            objProject = clsProject.Get(Settings, projectId)
            If objUser IsNot Nothing AndAlso objProject IsNot Nothing AndAlso objProject.CanUserUpdate(Session.GetUser(Settings)) Then
                objModel = New clsItterationListModel
                objModel.ProjectId = objProject.Id.Value
                objModel.ProejctTitle = objProject.Title

                colItteration = objProject.GetItterations(Settings)
                If colItteration IsNot Nothing Then
                    objModel.Itterations = New List(Of clsItterationModel)
                    For Each objItteration In colItteration
                        objItterationModel = New clsItterationModel
                        objItterationModel.Load(objItteration)
                        objModel.Itterations.Add(objItterationModel)
                    Next
                End If

                Return View("List", objModel)
            Else
                Return New HttpStatusCodeResult(HttpStatusCode.Unauthorized)
            End If
        End Function

        <HttpPost, ValidateAntiForgeryToken>
        Public Function Create(ByVal projectId As Integer, <FromBody> objForm As FormCollection) As ActionResult
            Dim objUser As clsUser
            Dim objProject As clsProject
            Dim colItteration As List(Of clsItteration)
            Dim objItteration As clsItteration
            Dim objModel As clsItterationListModel
            Dim objItterationModel As clsItterationModel

            objUser = Session.GetUser(Settings)
            objProject = clsProject.Get(Settings, projectId)
            If objUser IsNot Nothing AndAlso objProject IsNot Nothing AndAlso objProject.CanUserUpdate(Session.GetUser(Settings)) Then
                objItteration = objProject.GetNewItteration

                objModel = New clsItterationListModel
                objModel.ProjectId = objProject.Id.Value
                objModel.ProejctTitle = objProject.Title

                ValidateCreate(objForm, objModel)
                If ModelState.IsValid Then
                    objItteration.Name = objForm("NewName")
                    If String.IsNullOrEmpty(objForm("NewBeginDate")) = False Then
                        objItteration.StartDate = Date.Parse(objForm("NewBeginDate"))
                    Else
                        objItteration.StartDate = Nothing
                    End If
                    If String.IsNullOrEmpty(objForm("NewEndDate")) = False Then
                        objItteration.EndDate = Date.Parse(objForm("NewEndDate"))
                    Else
                        objItteration.EndDate = Nothing
                    End If
                    objItteration.IsActive = String.IsNullOrEmpty(objForm("NewIsActive")) = False AndAlso objForm("NewIsActive").ToLower.StartsWith("true")
                    objItteration.Create(Settings)
                    objModel.NewName = String.Empty
                    If objModel.NewEndDate.HasValue Then
                        objModel.NewBeginDate = objModel.NewEndDate.Value.AddDays(1)
                    Else
                        objModel.NewBeginDate = Nothing
                    End If
                    objModel.NewEndDate = Nothing
                End If
                colItteration = objProject.GetItterations(Settings)
                If colItteration IsNot Nothing Then
                    objModel.Itterations = New List(Of clsItterationModel)
                    For Each objItteration In colItteration
                        objItterationModel = New clsItterationModel
                        objItterationModel.Load(objItteration)
                        objModel.Itterations.Add(objItterationModel)
                    Next
                End If

                Return View("List", objModel)
            Else
                Return New HttpStatusCodeResult(HttpStatusCode.Unauthorized)
            End If
        End Function

        Private Sub ValidateCreate(ByVal objForm As FormCollection, ByVal objModel As clsItterationListModel)
            Dim datValue As Date
            If String.IsNullOrEmpty(objForm("NewName")) Then
                ModelState.AddModelError("NewName", "Name is required")
            Else
                objModel.NewName = objForm("NewName")
            End If
            If String.IsNullOrEmpty(objForm("NewBeginDate")) = False Then
                If Date.TryParse(objForm("NewBeginDate"), datValue) = False Then
                    ModelState.AddModelError("NewBeginDate", "Invalid begin date")
                Else
                    objModel.NewBeginDate = datValue
                End If
            End If
            If String.IsNullOrEmpty(objForm("NewEndDate")) = False Then
                If Date.TryParse(objForm("NewEndDate"), datValue) = False Then
                    ModelState.AddModelError("NewEndDate", "Invalid end date")
                Else
                    objModel.NewEndDate = datValue
                End If
            End If
            objModel.NewIsActive = String.IsNullOrEmpty(objForm("NewIsActive")) = False AndAlso objForm("NewIsActive").ToLower.StartsWith("true")
        End Sub

        <HttpPost, ValidateAntiForgeryToken>
        Public Function Update(ByVal id As Integer, <FromBody> objForm As FormCollection) As ActionResult
            Dim objUser As clsUser
            Dim objProject As clsProject
            Dim colItteration As List(Of clsItteration)
            Dim objItteration As clsItteration
            Dim objModel As clsItterationListModel
            Dim objItterationModel As clsItterationModel

            objUser = Session.GetUser(Settings)
            objItteration = clsItteration.Get(Settings, id)
            If objItteration IsNot Nothing Then
                objProject = objItteration.GetProject(Settings)
            Else
                objProject = Nothing
            End If
            If objUser IsNot Nothing AndAlso objProject IsNot Nothing AndAlso objProject.CanUserUpdate(Session.GetUser(Settings)) AndAlso objItteration IsNot Nothing Then
                objModel = New clsItterationListModel
                objModel.ProjectId = objProject.Id.Value
                objModel.ProejctTitle = objProject.Title
                objModel.UpdateItteration = New clsItterationModel
                objModel.UpdateItteration.Load(objItteration)

                ValidateUpdate(objForm, objModel.UpdateItteration)
                If ModelState.IsValid Then
                    objItteration.Name = objForm("Name")
                    If String.IsNullOrEmpty(objForm("StartDate")) = False Then
                        objItteration.StartDate = Date.Parse(objForm("StartDate"))
                    Else
                        objItteration.StartDate = Nothing
                    End If
                    If String.IsNullOrEmpty(objForm("EndDate")) = False Then
                        objItteration.EndDate = Date.Parse(objForm("EndDate"))
                    Else
                        objItteration.EndDate = Nothing
                    End If
                    objItteration.IsActive = String.IsNullOrEmpty(objForm("IsActive")) = False AndAlso objForm("IsActive").ToLower.StartsWith("true")
                    objItteration.Update(Settings)
                    objModel.UpdateItteration.Load(objItteration)
                End If
                colItteration = objProject.GetItterations(Settings)
                If colItteration IsNot Nothing Then
                    objModel.Itterations = New List(Of clsItterationModel)
                    For Each objItteration In colItteration
                        objItterationModel = New clsItterationModel
                        objItterationModel.Load(objItteration)
                        objModel.Itterations.Add(objItterationModel)
                    Next
                End If

                Return View("List", objModel)
            Else
                Return New HttpStatusCodeResult(HttpStatusCode.Unauthorized)
            End If
        End Function

        Private Sub ValidateUpdate(ByVal objForm As FormCollection, ByVal objModel As clsItterationModel)
            Dim datValue As Date
            If String.IsNullOrEmpty(objForm("Name")) Then
                ModelState.AddModelError("Name", "Name is required")
            Else
                objModel.Name = objForm("Name")
            End If
            If String.IsNullOrEmpty(objForm("BeginDate")) = False Then
                If Date.TryParse(objForm("StartDate"), datValue) = False Then
                    ModelState.AddModelError("StartDate", "Invalid begin date")
                Else
                    objModel.StartDate = datValue
                End If
            End If
            If String.IsNullOrEmpty(objForm("EndDate")) = False Then
                If Date.TryParse(objForm("EndDate"), datValue) = False Then
                    ModelState.AddModelError("EndDate", "Invalid end date")
                Else
                    objModel.EndDate = datValue
                End If
            End If
        End Sub

        <HttpPost>
        Public Function Delete(ByVal id As Integer) As ActionResult
            Dim objUser As clsUser
            Dim objProject As clsProject
            Dim objItteration As clsItteration

            objUser = Session.GetUser(Settings)
            objItteration = clsItteration.Get(Settings, id)
            If objItteration IsNot Nothing Then
                objProject = objItteration.GetProject(Settings)
            Else
                objProject = Nothing
            End If
            If objUser IsNot Nothing AndAlso objProject IsNot Nothing AndAlso objProject.CanUserUpdate(Session.GetUser(Settings)) AndAlso objItteration IsNot Nothing Then
                clsItteration.Delete(Settings, id)
                Return Content("Deleted")
            Else
                Return New HttpStatusCodeResult(HttpStatusCode.Unauthorized)
            End If
        End Function
    End Class
End Namespace