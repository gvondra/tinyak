Imports System.Net
Imports System.Web.Http
Imports tas = tinyak.API.Shared
Imports tc = tinyak.Core
Namespace Controllers.Api
    <clsApiActionFilter, clsApiExceptionFilterAttribute>
    Public Class ExceptionController
        Inherits clsApiControllerBase

        <HttpGet>
        Public Function [Get](minTimestamp As DateTime) As HttpResponseMessage
            Dim objSettings As clsSettings
            Dim objSession As tc.clsSession
            Dim objInnerUser As tc.clsUser
            Dim colException As List(Of tc.clsException)
            Dim arrResult() As tas.clsException
            Dim i As Integer

            objSettings = New clsSettings
            objSession = GetSession(objSettings)
            If objSession IsNot Nothing Then
                objInnerUser = objSession.GetUser(objSettings)
                If objInnerUser IsNot Nothing AndAlso objInnerUser.IsAdministrator Then
                    colException = tc.clsException.GetByMinimumTimestamp(objSettings, minTimestamp)
                    If colException IsNot Nothing Then
                        ReDim arrResult(colException.Count - 1)
                        For i = 0 To colException.Count - 1
                            arrResult(i) = GetException(colException(i))
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

        Private Function GetException(ByVal objException As tc.clsException) As tas.clsException
            Dim objResult As tas.clsException
            objResult = New tas.clsException
            With objResult
                .Data = objException.Data
                .HResult = objException.HResult
                .Message = objException.Message
                .Source = objException.Source
                .StackTrace = objException.StackTrace
                .Target = objException.Target
                .Timestamp = objException.Timestamp
                .TypeName = objException.TypeName
            End With
            If objException.InnerException IsNot Nothing Then
                objResult.InnerException = GetException(objException.InnerException)
            Else
                objResult.InnerException = Nothing
            End If
            Return objResult
        End Function
    End Class
End Namespace