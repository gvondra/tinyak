Imports tinyak.Data
Public Class clsProject
    Private m_objProjectData As clsProjectData

    Private Sub New(ByVal objProjectData As clsProjectData)
        m_objProjectData = objProjectData
    End Sub

    Public Property Id As Nullable(Of Integer)
        Get
            Return m_objProjectData.Id
        End Get
        Set
            m_objProjectData.Id = Value
        End Set
    End Property

    Public Property Title As String
        Get
            Return m_objProjectData.Title
        End Get
        Set
            m_objProjectData.Title = Value
        End Set
    End Property

    Private Property OwnerId As Integer
        Get
            Return m_objProjectData.OwnerId
        End Get
        Set
            m_objProjectData.OwnerId = Value
        End Set
    End Property

    Public Property ProjectMembers As List(Of String)
        Get
            Return m_objProjectData.ProjectMembers
        End Get
        Set
            m_objProjectData.ProjectMembers = Value
        End Set
    End Property

    Public Shared Function GetNew(ByVal objUser As clsUser, ByVal strTitle As String) As clsProject
        Dim objResult As clsProject

        objResult = New clsProject(New clsProjectData)
        objResult.OwnerId = objUser.Id.Value
        objResult.Title = strTitle
        Return objResult
    End Function

    Public Sub Create(ByVal objSettings As ISettings)
        Dim objInnerSettings As clsSettings

        objInnerSettings = New clsSettings(objSettings)
        Try
            m_objProjectData.Create(objInnerSettings)
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
