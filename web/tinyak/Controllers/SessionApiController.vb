Imports System.Net
Imports System.Web.Http
Imports tinyak.Core
Namespace Controllers.Api
    <clsApiActionFilter>
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