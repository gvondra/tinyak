Imports System.IO
Imports System.Text
Public Class clsSession
    Private Sub New()
    End Sub

    Public Shared Function GetSessionId(ByVal objSettings As ISettings) As Guid
        Dim objRequest As HttpWebRequest
        Dim objResponse As HttpWebResponse
        Dim objUri As Uri
        Dim objReader As StreamReader
        Dim strGuid As String

        objUri = GetUri(objSettings, "Session")
        objRequest = HttpWebRequest.CreateHttp(objUri)
        objRequest.UseDefaultCredentials = True
        objResponse = DirectCast(objRequest.GetResponse, HttpWebResponse)
        objReader = New StreamReader(objResponse.GetResponseStream, Encoding.GetEncoding(objResponse.CharacterSet))
        Try
            strGuid = objReader.ReadToEnd.Replace("""", String.Empty)
        Finally
            objReader.Close()
            objReader.Dispose()
        End Try
        Return Guid.Parse(strGuid)
    End Function
End Class
