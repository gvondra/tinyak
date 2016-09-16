Imports System.Web.Http.Filters
Imports tinyak.Core
Public Class clsApiExceptionFilterAttribute
    Inherits System.Web.Http.Filters.ExceptionFilterAttribute

    Public Overrides Sub OnException(actionExecutedContext As HttpActionExecutedContext)
        Dim objException As clsException

        objException = clsException.GetNew(actionExecutedContext.Exception)
        objException.Create(New clsSettings)
    End Sub
End Class
