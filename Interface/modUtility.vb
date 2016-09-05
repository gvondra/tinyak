Imports System.Text
Imports System.Web
Imports tas = tinyak.API.Shared
Friend Module modUtility

    Friend Function GetUri(ByVal objSettings As ISettings, ByVal strController As String) As Uri
        Return GetUri(objSettings, strController, Nothing)
    End Function

    Friend Function GetUri(ByVal objSettings As ISettings, ByVal strController As String, ByVal objQuery As NameValueCollection) As Uri
        Dim objUriBuilder As UriBuilder
        Dim objRoot As Uri
        Dim colPath As List(Of String)
        Dim strSegment As String
        Dim i As Integer
        Dim objEnumerator As IEnumerator
        Dim sbQuery As StringBuilder

        objRoot = New Uri(objSettings.BaseUrl)
        objUriBuilder = New UriBuilder(objRoot.Scheme, objRoot.Host, objRoot.Port)
        colPath = New List(Of String)
        If objRoot.Segments IsNot Nothing AndAlso objRoot.Segments.Length > 0 Then
            For i = 0 To objRoot.Segments.Length - 1
                strSegment = objRoot.Segments(i).Replace("/", String.Empty)
                If String.IsNullOrEmpty(objRoot.Segments(i)) = False Then
                    colPath.Add(strSegment)
                End If
            Next i
        End If
        colPath.Add(strController)
        objUriBuilder.Path = String.Join("/", colPath)

        If objQuery IsNot Nothing AndAlso objQuery.Count > 0 Then
            objEnumerator = objQuery.GetEnumerator
            sbQuery = New StringBuilder
            While objEnumerator.MoveNext
                If sbQuery.Length > 0 Then
                    sbQuery.Append("&")
                End If
                sbQuery.AppendFormat("{0}={1}", HttpUtility.UrlEncode(DirectCast(objEnumerator.Current, String)), HttpUtility.UrlEncode(objQuery(DirectCast(objEnumerator.Current, String))))
            End While
            objUriBuilder.Query = sbQuery.ToString
        End If

        Return objUriBuilder.Uri
    End Function

    Private Function CreateRequest(ByVal objSession As Guid, ByVal objUri As Uri) As HttpWebRequest
        Dim objRequest As HttpWebRequest
        objRequest = HttpWebRequest.CreateHttp(objUri)
        objRequest.UseDefaultCredentials = True
        objRequest.Headers.Add(tas.clsConstant.HEADER_SESSION_ID, objSession.ToString("N"))
        objRequest.Accept = "application/xml"
        Return objRequest
    End Function

    Public Function CreateGetRequest(ByVal objSession As Guid, ByVal objUri As Uri) As HttpWebRequest
        Dim objRequest As HttpWebRequest
        objRequest = CreateRequest(objSession, objUri)
        objRequest.Method = "GET"
        Return objRequest
    End Function

    Public Function CreatePostRequest(ByVal objSession As Guid, ByVal objUri As Uri) As HttpWebRequest
        Dim objRequest As HttpWebRequest
        objRequest = CreateRequest(objSession, objUri)
        objRequest.Method = "POST"
        'objRequest.ContentType = "application/x-www-form-urlencoded"
        objRequest.ContentType = "application/xml"
        Return objRequest
    End Function

End Module
