Imports tinyak.Data
Public Class clsUser
    Private m_objUserData As clsUserData

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
End Class
