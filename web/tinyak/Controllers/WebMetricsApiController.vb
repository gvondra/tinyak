Imports System.Net
Imports System.Web.Http
Imports tas = tinyak.API.Shared
Imports tc = tinyak.Core
Namespace Controllers.Api
    <clsApiActionFilter, clsApiExceptionFilterAttribute>
    Public Class WebMetricsController
        Inherits clsApiControllerBase

        <HttpGet>
        Public Function [Get](minTimestamp As DateTime) As HttpResponseMessage
            Dim objSettings As clsSettings
            Dim objSession As tc.clsSession
            Dim objInnerUser As tc.clsUser
            Dim colWebMetrics As List(Of tc.clsWebMetrics)
            Dim objWebMetrics As tc.clsWebMetrics
            Dim arrResult() As tas.clsWebMetrics
            Dim i As Integer

            objSettings = New clsSettings
            objSession = GetSession(objSettings)
            If objSession IsNot Nothing Then
                objInnerUser = objSession.GetUser(objSettings)
                If objInnerUser IsNot Nothing AndAlso objInnerUser.IsAdministrator Then
                    colWebMetrics = tc.clsWebMetrics.GetByMinimumTimestamp(objSettings, minTimestamp)
                    If colWebMetrics IsNot Nothing Then
                        ReDim arrResult(colWebMetrics.Count - 1)
                        For i = 0 To colWebMetrics.Count - 1
                            objWebMetrics = colWebMetrics(i)
                            arrResult(i) = New tas.clsWebMetrics
                            With arrResult(i)
                                .Action = objWebMetrics.Action
                                .ContentEncoding = objWebMetrics.ContentEncoding
                                .ContentLength = objWebMetrics.ContentLength
                                .ContentType = objWebMetrics.ContentType
                                .Controller = objWebMetrics.Controller
                                .Duration = objWebMetrics.Duration
                                .Method = objWebMetrics.Method
                                .Parameters = objWebMetrics.Parameters
                                .RequestType = objWebMetrics.RequestType
                                .Timestamp = objWebMetrics.Timestamp
                                .TotalBytes = objWebMetrics.TotalBytes
                                .Url = objWebMetrics.Url
                                .UrlReferrer = objWebMetrics.UrlReferrer
                                .UserAgent = objWebMetrics.UserAgent
                                .StatusCode = objWebMetrics.StatusCode
                                .StatusDescription = objWebMetrics.StatusDescription
                            End With
                        Next
                    Else
                        arrResult = Nothing
                    End If
                    Return Request.CreateResponse(HttpStatusCode.OK, arrResult)
                Else
                    Return Request.CreateResponse(HttpStatusCode.Unauthorized)
                End If
            Else
                Return Request.CreateResponse(HttpStatusCode.Unauthorized)
            End If
        End Function
    End Class
End Namespace