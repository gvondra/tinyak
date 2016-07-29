Imports System.Collections.Specialized
Public Class clsSessionData

    Private Const TABLE_NAME As String = "Session"

    Private m_objId As Guid
    Private m_intUserId As Nullable(Of Integer)
    Private m_datExpiration As Nullable(Of Date)
    Private m_objData As NameValueCollection

    Public Sub New()
        m_objId = Guid.Empty
        m_objData = New NameValueCollection
    End Sub

    Public Property Id As Guid
        Get
            Return m_objId
        End Get
        Set
            m_objId = Value
        End Set
    End Property

    Public Property UserId As Nullable(Of Integer)
        Get
            Return m_intUserId
        End Get
        Set
            m_intUserId = Value
        End Set
    End Property

    Public Property ExpirationDate As Nullable(Of Date)
        Get
            Return m_datExpiration
        End Get
        Set
            m_datExpiration = Value
        End Set
    End Property

    Public Property Data As NameValueCollection
        Get
            Return m_objData
        End Get
        Set
            m_objData = Value
        End Set
    End Property

    Private Shared Sub Initialize(ByVal objReader As IDataReader, ByVal objSession As clsSessionData)
        Dim bytValue() As Byte
        With objSession
            bytValue = CType(objReader.GetValue(objReader.GetOrdinal("")), Byte())
            .m_objId = New Guid(bytValue)
            If objReader.IsDBNull(objReader.GetOrdinal("UserId")) = False Then
                .m_intUserId = objReader.GetInt32(objReader.GetOrdinal("UserId"))
            Else
                .m_intUserId = Nothing
            End If
            If objReader.IsDBNull(objReader.GetOrdinal("ExpirationDate")) = False Then
                .m_datExpiration = objReader.GetDateTime(objReader.GetOrdinal("ExpirationDate"))
            Else
                .m_datExpiration = Nothing
            End If
        End With
    End Sub
End Class
