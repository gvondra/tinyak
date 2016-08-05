Imports tinyak.Interface.tinyak
Imports System.Web.Mvc

Namespace Controllers
    Public Class UserController
        Inherits clsControllerBase

        Public Sub New(ByVal objSettings As clsSettings, ByVal objSession As clsSession)
            MyBase.New(objSettings, objSession)
        End Sub

        Public Function Login() As ActionResult
            Return View(New clsUserLoginModel)
        End Function

        <HttpPost(), ValidateAntiForgeryToken()>
        Public Function Login(ByVal objModel As clsUserLoginModel) As ActionResult
            Dim objService As clsUserService

            ValidateLogin(objModel)
            If ModelState.IsValid Then
                objService = New clsUserService(Settings)
                If objService.Login(Session.Id, objModel.EmailAddress, objModel.Password) Then
                    FormsAuthentication.SetAuthCookie(objModel.EmailAddress, False)
                    Return RedirectToAction("Index", "Home")
                Else
                    ModelState.AddModelError("EmailAddress", "Login failed")
                    Return View(objModel)
                End If
            Else
                Return View(objModel)
            End If
        End Function

        Private Sub ValidateLogin(ByVal objModel As clsUserLoginModel)
            If String.IsNullOrEmpty(objModel.EmailAddress) Then
                ModelState.AddModelError("EmailAddress", "Email Address is required")
            End If
        End Sub

        Public Function Logout() As ActionResult
            Dim objCookie As HttpCookie

            FormsAuthentication.SignOut()

            objCookie = Response.Cookies("SID")
            If objCookie IsNot Nothing Then
                objCookie.Expires = #1/1/2000#
            End If

            Return RedirectToAction("Index", "Home")
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
                objUserData = objService.Create(Session.Id, objCreateUser.Name, objCreateUser.EmailAddress, objCreateUser.Password)
                FormsAuthentication.SetAuthCookie(objUserData.EmailAddress, False)
                Return RedirectToAction("Profile")
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

        Public Shadows Function Profile(ByVal id As Nullable(Of Integer)) As ActionResult
            Dim objUserData As tinyak.Interface.tinyak.clsUser
            Dim objModel As clsUserModel

            If id.HasValue = False Then
                id = Session.UserId
            End If
            If id.HasValue Then
                objUserData = GetUser(id.Value)
                objModel = New clsUserModel
                If objUserData IsNot Nothing Then
                    objModel.EmailAddress = objUserData.EmailAddress
                    objModel.Name = objUserData.Name
                End If
                Return View(objModel)
            Else
                Return New HttpStatusCodeResult(Net.HttpStatusCode.Forbidden)
            End If
        End Function

        <HttpPost, ValidateAntiForgeryToken()>
        Public Shadows Function Profile(ByVal id As Nullable(Of Integer), ByVal objModel As clsUserModel) As ActionResult
            Dim objUserData As tinyak.Interface.tinyak.clsUser
            Dim objService As clsUserService

            ValidateUsesr(objModel)
            If ModelState.IsValid Then
                If id.HasValue = False Then
                    id = Session.UserId
                End If
                If id.HasValue Then
                    objService = New clsUserService(Settings)
                    objUserData = New tinyak.Interface.tinyak.clsUser
                    objUserData.Id = id.Value
                    objUserData.EmailAddress = objModel.EmailAddress
                    objUserData.Name = objModel.Name
                    objUserData = objService.SaveUser(Session.Id, objUserData)
                    If objUserData IsNot Nothing Then
                        objModel.EmailAddress = objUserData.EmailAddress
                        objModel.Name = objUserData.Name
                    End If
                    Return View(objModel)
                Else
                    Return New HttpStatusCodeResult(Net.HttpStatusCode.Forbidden)
                End If
            End If
            Return View(objModel)
        End Function

        Private Sub ValidateUsesr(ByVal objModel As clsUserModel)
            If String.IsNullOrEmpty(objModel.Name) Then
                ModelState.AddModelError("Name", "Name is required")
            End If
        End Sub

        Private Function GetUser(ByVal intId As Integer) As tinyak.Interface.tinyak.clsUser
            Dim objService As clsUserService
            objService = New clsUserService(Settings)
            Return objService.GetUser(Session.Id, intId)
        End Function
    End Class
End Namespace