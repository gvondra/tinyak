Imports tas = tinyak.API.Shared
Imports tc = tinyak.Core
Public MustInherit Class clsApiControllerBase
    Inherits ApiController

    Protected Function GetSession(ByVal objSettings As clsSettings) As tc.clsSession
        Dim colSession As List(Of String)
        Dim objSessionId As Guid
        Dim objSession As tc.clsSession

        colSession = New List(Of String)(Request.Headers.GetValues(tas.clsConstant.HEADER_SESSION_ID))
        If colSession.Count > 0 Then
            objSessionId = Guid.Parse(colSession(0))
            objSession = tc.clsSession.Get(objSettings, objSessionId)
        Else
            objSession = Nothing
        End If
        Return objSession
    End Function
End Class
