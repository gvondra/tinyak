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
            Dim objUser As clsUser

            ValidateLogin(objModel)
            If ModelState.IsValid Then
                objUser = clsUser.GetByEmailAddress(Settings, objModel.EmailAddress, objModel.Password)
                If objUser IsNot Nothing Then
                    Session.UserId = objUser.Id.Value
                    Session.Save(Settings)
                    FormsAuthentication.SetAuthCookie(objUser.EmailAddress, False)
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
            Dim objUser As clsUser

            ValidateCreate(objCreateUser)
            If ModelState.IsValid Then
                objUser = clsUser.CreateNew(Settings, objCreateUser.Name, objCreateUser.EmailAddress, objCreateUser.Password)
                FormsAuthentication.SetAuthCookie(objUser.EmailAddress, False)
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
            Return clsUser.IsEmailAddressAvailable(Settings, strEmailAddress)
        End Function

        <Authorize>
        Public Shadows Function Profile(ByVal id As Nullable(Of Integer)) As ActionResult
            Dim objUser As clsUser
            Dim objModel As clsUserModel

            If id.HasValue = False Then
                id = Session.UserId
            End If
            If id.HasValue Then
                objUser = clsUser.Get(Settings, id.Value)
                objModel = New clsUserModel
                If objUser IsNot Nothing Then
                    objModel.EmailAddress = objUser.EmailAddress
                    objModel.Name = objUser.Name
                End If
                Return View(objModel)
            Else
                Return New HttpStatusCodeResult(Net.HttpStatusCode.Forbidden)
            End If
        End Function

        <Authorize, HttpPost, ValidateAntiForgeryToken()>
        Public Shadows Function Profile(ByVal id As Nullable(Of Integer), ByVal objModel As clsUserModel) As ActionResult
            Dim objUser As clsUser

            ValidateUsesr(objModel)
            If ModelState.IsValid Then
                If id.HasValue = False Then
                    id = Session.UserId
                End If
                If id.HasValue Then
                    objUser = clsUser.Get(Settings, id.Value)
                    objUser.Id = id.Value
                    objUser.EmailAddress = objModel.EmailAddress
                    objUser.Name = objModel.Name
                    objUser.Update(Settings)
                    objModel.EmailAddress = objUser.EmailAddress
                    objModel.Name = objUser.Name

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
    End Class
End Namespace