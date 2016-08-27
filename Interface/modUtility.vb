Friend Module modUtility

    Friend Function GetUri(ByVal objSettings As ISettings, ByVal strController As String) As Uri
        Dim objUriBuilder As UriBuilder
        Dim objRoot As Uri
        Dim colPath As List(Of String)
        Dim strSegment As String
        Dim i As Integer

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

        Return objUriBuilder.Uri
    End Function

End Module
