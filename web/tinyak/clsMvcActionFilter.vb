Imports System.IO
Imports System.Runtime.Serialization.Json
Imports System.Text.RegularExpressions
Imports System.Xml
Imports tinyak.Core
Public Class clsMvcActionFilter
    Inherits ActionFilterAttribute

    Public Overrides Sub OnActionExecuting(filterContext As ActionExecutingContext)
        filterContext.Controller.ViewData("ActionStartTimestamp") = Date.UtcNow
        MyBase.OnActionExecuting(filterContext)
    End Sub

    Public Overrides Sub OnActionExecuted(filterContext As ActionExecutedContext)
        Dim objWebMetrics As clsWebMetrics = clsWebMetrics.GetNew
        Dim objRequest As HttpRequestBase
        Dim objResponse As HttpResponseBase

        With objWebMetrics
            .Action = filterContext.ActionDescriptor.ActionName
            .Controller = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName

            objRequest = filterContext.HttpContext.Request
            .ContentEncoding = objRequest.ContentEncoding.WebName
            .ContentLength = objRequest.ContentLength
            .ContentType = objRequest.ContentType
            .Timestamp = DirectCast(filterContext.Controller.ViewData("ActionStartTimestamp"), Date)
            .Duration = Date.UtcNow.Subtract(.Timestamp).TotalSeconds
            .Method = objRequest.HttpMethod
            .RequestType = objRequest.RequestType
            .TotalBytes = objRequest.TotalBytes
            .Url = objRequest.Url.ToString
            If objRequest.UrlReferrer IsNot Nothing Then
                .UrlReferrer = objRequest.UrlReferrer.ToString
            Else
                .UrlReferrer = String.Empty
            End If
            If objRequest.UserAgent IsNot Nothing Then
                .UserAgent = objRequest.UserAgent
            Else
                .UserAgent = String.Empty
            End If

            objResponse = filterContext.HttpContext.Response
            .StatusCode = objResponse.StatusCode
            .StatusDescription = objResponse.StatusDescription
        End With
        SetParameters(objRequest, objWebMetrics)
        objWebMetrics.Create(New clsSettings)

        MyBase.OnActionExecuted(filterContext)
    End Sub

    Private Sub SetParameters(objRequest As HttpRequestBase, ByVal objWebMetrics As clsWebMetrics)
        Dim objWriter As XmlWriter
        Dim objStream As MemoryStream
        Dim i As Integer
        Dim strKey As String

        objStream = New MemoryStream
        Try
            objWriter = JsonReaderWriterFactory.CreateJsonWriter(objStream, Encoding.UTF8)
            With objWriter
                .WriteStartElement("root")
                .WriteAttributeString("type", "object")

                If objRequest.Params IsNot Nothing AndAlso objRequest.Params.Count > 0 Then
                    For i = 0 To objRequest.Params.Count - 1
                        strKey = objRequest.Params.Keys.Item(i)
                        If Regex.IsMatch(strKey, "^ALL_HTTP$", RegexOptions.IgnoreCase) = False _
                                AndAlso Regex.IsMatch(strKey, "^ALL_RAW$", RegexOptions.IgnoreCase) = False _
                                AndAlso Regex.IsMatch(strKey, "PASSWORD", RegexOptions.IgnoreCase) = False _
                                AndAlso Regex.IsMatch(strKey, "^CERT_", RegexOptions.IgnoreCase) = False _
                                AndAlso Regex.IsMatch(strKey, "^.ASPXAUTH$", RegexOptions.IgnoreCase) = False _
                                AndAlso Regex.IsMatch(strKey, "^__RequestVerificationToken$", RegexOptions.IgnoreCase) = False Then

                            .WriteStartElement(strKey)
                            .WriteAttributeString("type", "string")
                            .WriteValue(objRequest.Params.Item(i))
                            .WriteEndElement()
                        End If
                    Next
                End If

                .WriteEndElement()
            End With

            objWriter.Flush()
            objWriter.Close()
            objWebMetrics.Parameters = Encoding.UTF8.GetString(objStream.ToArray)
        Finally
            objStream.Dispose()
        End Try
    End Sub
End Class
