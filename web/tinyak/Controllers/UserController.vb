Imports tinyak.Interface.tinyak
Imports System.Web.Mvc

Namespace Controllers
    Public Class UserController
        Inherits clsControllerBase

        Public Function Login() As ActionResult
            Return View()
        End Function

        <HttpPost(), ValidateAntiForgeryToken()>
        Public Function Login(ByVal emailAddress As String, ByVal password As String) As ActionResult
            Return View()
        End Function

        Public Function Create() As ActionResult
            Return View()
        End Function

        <HttpPost(), ValidateAntiForgeryToken()>
        Public Function Create(ByVal objCreateUser As clsCreateUserModel) As ActionResult
            Dim objService As clsUserService
            Dim objUserData As clsUser

            ValidateCreate(objCreateUser)
            If ModelState.IsValid Then
                objService = New clsUserService(New clsSettings)
                objUserData = objService.Create(objCreateUser.Name, objCreateUser.EmailAddress, objCreateUser.Password)
                FormsAuthentication.SetAuthCookie(objUserData.EmailAddress, False)
                Return RedirectToAction("Index", "Home")
            Else
                Return View(objCreateUser)
            End If
        End Function

        Private Sub ValidateCreate(ByVal objCreateUser As clsCreateUserModel)
            If String.IsNullOrEmpty(objCreateUser.Name) Then
                ModelState.AddModelError("Name", "Name is required")
            End If
            If String.IsNullOrEmpty(objCreateUser.EmailAddress) Then
                ModelState.AddModelError("EmailAddress", "Email Address is required")
            ElseIf IsEmailAddressAvailable(objCreateUser.EmailAddress) = False Then
                ModelState.AddModelError("EmailAddress", "Email Address is not available")
            End If
            If String.IsNullOrEmpty(objCreateUser.Password) Then
                ModelState.AddModelError("Password", "Password is required")
            End If
        End Sub

        Private Function IsEmailAddressAvailable(ByVal strEmailAddress As String) As Boolean
            Dim objUserService As tinyak.Interface.tinyak.clsUserService

            objUserService = New tinyak.Interface.tinyak.clsUserService(New clsSettings)
            Return objUserService.IsEmailAddressAvailable(strEmailAddress)
        End Function
    End Class
End Namespace