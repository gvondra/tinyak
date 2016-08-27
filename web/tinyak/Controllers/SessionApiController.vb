Imports System.Net
Imports System.Web.Http

Namespace Controllers.Api
    Public Class SessionController
        Inherits ApiController

        <HttpGet>
        Public Function Session() As String
            Dim objSession As clsSession
            objSession = clsSession.CreateNew(New clsSettings)
            Return objSession.Id.ToString("N")
        End Function

    End Class
End Namespace