Public Class winException
    Public Shared Sub BeginProcessException(ByVal objException As Exception, ByVal objDispatcher As System.Windows.Threading.Dispatcher)
        Dim objProcessException As ProcessExceptionDelegate
        objProcessException = New ProcessExceptionDelegate(AddressOf ProcessException)
        objDispatcher.Invoke(objProcessException)
    End Sub

    Public Delegate Sub ProcessExceptionDelegate(ByVal objException As Exception)
    Public Shared Sub ProcessException(ByVal objException As Exception)
        Dim objWindow As winException
        objWindow = New winException
        objWindow.Show()
    End Sub
End Class
