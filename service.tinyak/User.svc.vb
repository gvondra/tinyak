Imports tinyak.Core
Imports System.ServiceModel
Public Class User
    Implements IUser

    Public Function IsEmailAddressAvailable(strEmailAddress As String) As Boolean Implements IUser.IsEmailAddressAvailable
        Try
            strEmailAddress = strEmailAddress.Trim
            If clsValidation.IsValidEmailAddress(strEmailAddress) Then
                Return clsUser.IsEmailAddressAvailable(New clsSettings, strEmailAddress)
            Else
                Return False
            End If
        Catch ex As Exception
            Throw New FaultException("Unexpected error has occured")
        End Try
    End Function
End Class
