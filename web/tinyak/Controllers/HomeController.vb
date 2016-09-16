Imports tinyak.Core
Namespace Controllers
    <clsMvcActionFilter, clsMvcHandleError>
    Public Class HomeController
        Inherits clsControllerBase

        Public Sub New(ByVal objSettings As clsSettings, ByVal objSession As clsSession)
            MyBase.New(objSettings, objSession)
        End Sub

        Function Index() As ActionResult
            Return View()
        End Function

    End Class
End Namespace