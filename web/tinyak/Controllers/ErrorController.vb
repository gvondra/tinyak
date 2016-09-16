Imports System.Web.Mvc

Namespace Controllers
    <clsMvcActionFilter>
    Public Class ErrorController
        Inherits Controller

        Public Function Index() As ActionResult
            Return View()
        End Function

        Public Function Index(ByVal aspxerrorpath As String) As ActionResult
            Return View()
        End Function
    End Class
End Namespace