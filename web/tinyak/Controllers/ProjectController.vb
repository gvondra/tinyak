Imports System.Web.Mvc

Namespace Controllers
    <Authorize()>
    Public Class ProjectController
        Inherits clsControllerBase

        Public Sub New(ByVal objSettings As clsSettings, ByVal objSession As clsSession)
            MyBase.New(objSettings, objSession)
        End Sub

        Public Function List() As ActionResult
            Return View()
        End Function

        Public Function Create() As ActionResult
            Return View(New clsProjectCreateModel)
        End Function

        <ValidateAntiForgeryToken, HttpPost>
        Public Function Create(ByVal objModel As clsProjectCreateModel) As ActionResult
            Validate(objModel)
            Return View(objModel)
        End Function

        Private Sub Validate(ByVal objModel As clsProjectCreateModel)
            If String.IsNullOrEmpty(objModel.ProjectTitle) Then
                ModelState.AddModelError("ProjectTitle", "Title is required")
            End If
        End Sub
    End Class
End Namespace