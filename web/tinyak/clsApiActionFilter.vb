Imports System.IO
Imports System.Runtime.Serialization.Json
Imports System.Text.RegularExpressions
Imports System.Web.Http.Controllers
Imports System.Web.Http.Filters
Imports System.Xml
Imports tinyak.Core
Public Class clsApiActionFilter
    Inherits System.Web.Http.Filters.ActionFilterAttribute

    Public Overrides Sub OnActionExecuting(actionContext As HttpActionContext)
        actionContext.Request.Properties("ActionStartTimestamp") = Date.UtcNow
        MyBase.OnActionExecuting(actionContext)
    End Sub

    Public Overrides Sub OnActionExecuted(actionExecutedContext As HttpActionExecutedContext)
        Dim objWebMetrics As clsWebMetrics = clsWebMetrics.GetNew
        Dim objRequest As HttpRequestBase
        With objWebMetrics
            .Action = actionExecutedContext.ActionContext.ActionDescriptor.ActionName
            .Controller = actionExecutedContext.ActionContext.ActionDescriptor.ControllerDescriptor.ControllerName

            objRequest = CType(actionExecutedContext.Request.Properties("MS_HttpContext"), HttpContextWrapper).Request
            .ContentEncoding = objRequest.ContentEncoding.WebName
            .ContentLength = objRequest.ContentLength
            .ContentType = objRequest.ContentType
            .Timestamp = DirectCast(actionExecutedContext.Request.Properties("ActionStartTimestamp"), Date)
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
        End With
        SetParameters(objRequest, objWebMetrics)
        objWebMetrics.Create(New clsSettings)
        MyBase.OnActionExecuted(actionExecutedContext)
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
