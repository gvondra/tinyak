Imports tinyak.Data
Imports System.Security.Cryptography
Imports System.Text
Public Class clsUser
    Private m_objUserData As clsUserData

    Private Sub New(ByVal objUserData As clsUserData)
        m_objUserData = objUserData
    End Sub

    Public Property Id As Nullable(Of Integer)
        Get
            Return m_objUserData.Id
        End Get
        Set
            m_objUserData.Id = Value
        End Set
    End Property

    Public Property Name As String
        Get
            Return m_objUserData.Name
        End Get
        Set
            m_objUserData.Name = Value
        End Set
    End Property

    Public Property EmailAddress As String
        Get
            Return m_objUserData.EmailAddress
        End Get
        Set
            m_objUserData.EmailAddress = Value
        End Set
    End Property

    Public Property IsAdministrator As Boolean
        Get
            Return m_objUserData.IsAdministrator
        End Get
        Set
            m_objUserData.IsAdministrator = Value
        End Set
    End Property

    Public Shared Function IsEmailAddressAvailable(ByVal objSettings As ISettings, ByVal strEmailAddress As String) As Boolean
        Return clsUserData.IsEmailAddressAvailable(New clsSettings(objSettings), strEmailAddress)
    End Function

    Public Shared Function CreateNew(ByVal objSettings As ISettings, ByVal strName As String, ByVal strEmailAddress As String, ByVal strPassword As String) As clsUser
        Dim objUser As clsUser
        Dim objUserData As clsUserData
        Dim objInnerSettings As clsSettings
        Dim bytSalt As Byte()

        objInnerSettings = New clsSettings(objSettings)
        objUserData = New clsUserData
        objUser = New clsUser(objUserData)
        With objUser
            .Name = strName
            .EmailAddress = strEmailAddress
        End With

        bytSalt = GenerateSalt(strEmailAddress)

        Try
            objUserData.SaveNew(objInnerSettings, Tokenize(strPassword, bytSalt))
            objInnerSettings.DatabaseTransaction.Commit()
        Catch
            If objInnerSettings.DatabaseTransaction IsNot Nothing Then
                objInnerSettings.DatabaseTransaction.Rollback()
                objInnerSettings.DatabaseTransaction.Dispose()
                objInnerSettings.DatabaseTransaction = Nothing
            End If
        Finally
            If objInnerSettings.DatabaseConnection IsNot Nothing Then
                objInnerSettings.DatabaseConnection.Close()
                objInnerSettings.DatabaseConnection.Dispose()
                objInnerSettings.DatabaseConnection = Nothing
            End If
        End Try

        Return objUser
    End Function

    Private Shared Function GenerateSalt(ByVal strEmailAddress As String) As Byte()
        Dim bytSalt() As Byte
        Dim strValue As String
        Dim objHash As HashAlgorithm

        strValue = "tinyak_" & strEmailAddress.ToLower

        objHash = New SHA256Managed
        bytSalt = objHash.ComputeHash(Encoding.UTF8.GetBytes(strValue))

        Return bytSalt
    End Function

    Private Shared Function Tokenize(ByVal strValue As String, ByVal bytSalt As Byte()) As Byte()
        Dim objHash As HashAlgorithm
        Dim bytValue() As Byte
        Dim bytSaltedValue() As Byte
        Dim bytToken() As Byte

        If String.IsNullOrEmpty(strValue) = False Then
            bytValue = Encoding.UTF8.GetBytes(strValue)

            ReDim bytSaltedValue(bytValue.Length + bytSalt.Length - 1)
            Array.Copy(bytSalt, 0, bytSaltedValue, 0, bytSalt.Length)
            Array.Copy(bytValue, 0, bytSaltedValue, bytSalt.Length, bytValue.Length)
            objHash = New SHA512Managed()
            Try
                bytToken = objHash.ComputeHash(bytSaltedValue)
            Finally
                objHash.Dispose()
            End Try
        Else
            bytToken = Nothing
        End If
        Return bytToken
    End Function

    Public Shared Function GetByEmailAddress(ByVal objSettings As ISettings, ByVal strEmailAddress As String, ByVal strPassword As String) As clsUser
        Dim objData As clsUserData

        objData = clsUserData.GetByEmailAddress(New clsSettings(objSettings), strEmailAddress, Tokenize(strPassword, GenerateSalt(strEmailAddress)))
        If objData IsNot Nothing Then
            Return New clsUser(objData)
        Else
            Return Nothing
        End If
    End Function

    Public Shared Function [Get](ByVal objSettings As ISettings, ByVal intId As Integer) As clsUser
        Dim objData As clsUserData

        objData = clsUserData.Get(New clsSettings(objSettings), intId)
        If objData IsNot Nothing Then
            Return New clsUser(objData)
        Else
            Return Nothing
        End If
    End Function

    Public Sub Update(ByVal objSettings As ISettings)
        Dim objInnerSettings As clsSettings

        objInnerSettings = New clsSettings(objSettings)
        Try
            m_objUserData.Update(objInnerSettings)
            objInnerSettings.DatabaseTransaction.Commit()
        Catch
            If objInnerSettings.DatabaseTransaction IsNot Nothing Then
                objInnerSettings.DatabaseTransaction.Rollback()
                objInnerSettings.DatabaseTransaction.Dispose()
                objInnerSettings.DatabaseTransaction = Nothing
            End If
        Finally
            If objInnerSettings.DatabaseConnection IsNot Nothing Then
                objInnerSettings.DatabaseConnection.Close()
                objInnerSettings.DatabaseConnection.Dispose()
                objInnerSettings.DatabaseConnection = Nothing
            End If
        End Try
    End Sub
End Class
