Imports tinyak.Data
Public Class clsSession
    Private m_objSessionData As clsSessionData

    Private Sub New(ByVal objSessionData As clsSessionData)
        m_objSessionData = objSessionData
    End Sub

    Public Property Id As Guid
        Get
            Return m_objSessionData.Id
        End Get
        Private Set
            m_objSessionData.Id = Value
        End Set
    End Property

    Public Property UserId As Nullable(Of Integer)
        Get
            Return m_objSessionData.UserId
        End Get
        Set
            m_objSessionData.UserId = Value
        End Set
    End Property

    Public Property ExpirationDate As Nullable(Of Date)
        Get
            Return m_objSessionData.ExpirationDate
        End Get
        Set
            m_objSessionData.ExpirationDate = Value
        End Set
    End Property

    Public Property Data As Dictionary(Of String, String)
        Get
            Return m_objSessionData.Data
        End Get
        Set
            m_objSessionData.Data = Value
        End Set
    End Property

    Public Shared Function [Get](ByVal objSettings As ISettings, ByVal objGuid As Guid) As clsSession
        Dim objSessionData As clsSessionData
        Dim objResult As clsSession

        objSessionData = clsSessionData.Get(New clsSettings(objSettings), objGuid)
        If objSessionData IsNot Nothing Then
            objResult = New clsSession(objSessionData)
        Else
            objResult = Nothing
        End If
        Return objResult
    End Function

    Public Shared Function CreateNew(ByVal objSettings As ISettings) As clsSession
        Dim objSession As clsSession
        Dim objInnerSettings As clsSettings

        objInnerSettings = New clsSettings(objSettings)
        objSession = New clsSession(New clsSessionData)
        objSession.Id = Guid.NewGuid
        objSession.ExpirationDate = Date.UtcNow.AddHours(6)
        objSession.Save(objSettings)

        Return objSession
    End Function

    Public Sub Save(ByVal objSettings As ISettings)
        Dim objInnerSettings As clsSettings

        objInnerSettings = New clsSettings(objSettings)
        Try
            m_objSessionData.Save(objInnerSettings)
            objInnerSettings.DatabaseTransaction.Commit()
            objInnerSettings.DatabaseConnection.Close()
        Catch
            If objInnerSettings.DatabaseTransaction IsNot Nothing Then
                objInnerSettings.DatabaseTransaction.Rollback()
            End If
            Throw
        Finally
            If objInnerSettings.DatabaseTransaction IsNot Nothing Then
                objInnerSettings.DatabaseTransaction.Dispose()
            End If
            If objInnerSettings.DatabaseConnection IsNot Nothing Then
                objInnerSettings.DatabaseConnection.Dispose()
            End If
        End Try

    End Sub

    Public Function GetUser(ByVal objSettings As ISettings) As clsUser
        Dim objUser As clsUser

        If UserId.HasValue Then
            objUser = clsUser.Get(objSettings, UserId.Value)
        Else
            objUser = Nothing
        End If
        Return objUser
    End Function
End Class
