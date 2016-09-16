Imports tinyak.Core
Public Class clsMvcHandleErrorAttribute
    Inherits HandleErrorAttribute

    Public Overrides Sub OnException(filterContext As ExceptionContext)
        Dim objException As clsException

        objException = clsException.GetNew(filterContext.Exception)
        objException.Create(New clsSettings)
    End Sub
End Class
