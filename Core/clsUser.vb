Imports tinyak.Data
Imports System.Security.Cryptography
Imports System.Text
Public Class clsUser
    Private m_objUserData As clsUserData
    Private m_objSettings As clsSettings

    Private Sub New(ByVal objSettings As clsSettings, ByVal objUserData As clsUserData)
        m_objSettings = objSettings
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
        Dim lngSalt As Long

        objInnerSettings = New clsSettings(objSettings)
        objUserData = New clsUserData
        objUser = New clsUser(objInnerSettings, objUserData)
        With objUser
            .Name = strName
            .EmailAddress = strEmailAddress
        End With

        lngSalt = GenerateSalt()

        Try
            objUserData.SaveNew(objInnerSettings, Tokenize(strPassword, lngSalt), lngSalt)
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

    Private Shared Function GenerateSalt() As Long
        Dim objRandom As Random
        Dim bytSeed(3) As Byte
        Dim bytSalt(7) As Byte

        Array.Copy(BitConverter.GetBytes(Date.Now.Ticks), 3, bytSeed, 0, 2)
        Array.Copy(BitConverter.GetBytes(Date.Now.Millisecond), 0, bytSeed, 2, 2)
        objRandom = New Random(BitConverter.ToInt32(bytSeed, 0))

        Array.Copy(BitConverter.GetBytes(objRandom.Next), 0, bytSalt, 0, 4)
        Array.Copy(BitConverter.GetBytes(objRandom.Next), 0, bytSalt, 4, 4)
        Return BitConverter.ToInt64(bytSalt, 0)
    End Function

    Private Shared Function Tokenize(ByVal strValue As String, ByVal lngSalt As Int64) As Byte()
        Dim objHash As HashAlgorithm
        Dim bytValue() As Byte
        Dim bytSaltedValue() As Byte
        Dim bytToken() As Byte

        bytValue = Encoding.UTF8.GetBytes(strValue)

        ReDim bytSaltedValue(bytValue.Length + 7)
        Array.Copy(BitConverter.GetBytes(lngSalt), 0, bytSaltedValue, 0, 8)
        Array.Copy(bytValue, 0, bytSaltedValue, 8, bytValue.Length)
        objHash = New SHA512Managed()
        Try
            bytToken = objHash.ComputeHash(bytSaltedValue)
        Finally
            objHash.Dispose()
        End Try
        Return bytToken
    End Function
End Class
