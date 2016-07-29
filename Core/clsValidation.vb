Imports System.Text.RegularExpressions
Public Class clsValidation
    Private Sub New()
    End Sub

    Public Shared Function IsValidEmailAddress(ByVal strEmailAddress As String) As Boolean
        If String.IsNullOrEmpty(strEmailAddress) Then
            Return False
        Else
            Return Regex.IsMatch(strEmailAddress, "^\S@\S$")
        End If
    End Function
End Class
