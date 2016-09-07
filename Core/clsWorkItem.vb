Imports tinyak.Data
Public Class clsWorkItem

    Public Enum enumState As Int16
        [New] = 0
        [Approved] = 1
        [Committed] = 2
        [Complete] = 3
        [Rejected] = 4
    End Enum

    Private m_objWorkItemData As clsWorkItemData
    Private m_objFeature As clsFeature

    Private Sub New(ByVal objWorkItemData As clsWorkItemData, ByVal objFeature As clsFeature)
        m_objWorkItemData = objWorkItemData
        m_objFeature = objFeature
    End Sub

    Public ReadOnly Property Id As Nullable(Of Integer)
        Get
            Return m_objWorkItemData.Id
        End Get
    End Property

    Private Property ProjectId As Nullable(Of Integer)
        Get
            Return m_objWorkItemData.ProjectId
        End Get
        Set(value As Nullable(Of Integer))
            m_objWorkItemData.ProjectId = value
        End Set
    End Property

    Private Property FeatureId As Nullable(Of Integer)
        Get
            Return m_objWorkItemData.FeatureId
        End Get
        Set(value As Nullable(Of Integer))
            m_objWorkItemData.FeatureId = value
        End Set
    End Property

    Public Property Title As String
        Get
            Return m_objWorkItemData.Title
        End Get
        Set
            m_objWorkItemData.Title = Value
        End Set
    End Property

    Public Property State As enumState
        Get
            Return CType(m_objWorkItemData.State, enumState)
        End Get
        Set
            m_objWorkItemData.State = CType(Value, Int16)
        End Set
    End Property

    Public Property AssignedTo As Nullable(Of Integer)
        Get
            Return m_objWorkItemData.AssignedTo
        End Get
        Set
            m_objWorkItemData.AssignedTo = Value
        End Set
    End Property

    Public Property Effort As Nullable(Of Int16)
        Get
            Return m_objWorkItemData.Effort
        End Get
        Set
            m_objWorkItemData.Effort = Value
        End Set
    End Property

    Public Property Description As String
        Get
            Return m_objWorkItemData.Description
        End Get
        Set
            m_objWorkItemData.Description = Value
        End Set
    End Property

    Public Property AcceptanceCriteria As String
        Get
            Return m_objWorkItemData.AcceptanceCriteria
        End Get
        Set
            m_objWorkItemData.AcceptanceCriteria = Value
        End Set
    End Property

    Friend Shared Function GetNew(ByVal objFeature As clsFeature) As clsWorkItem
        Return New clsWorkItem(New clsWorkItemData, objFeature) With {.AcceptanceCriteria = String.Empty, .Description = String.Empty}
    End Function

    Public Sub Create(ByVal objSettings As ISettings)
        Dim objInnerSettings As clsSettings

        If ProjectId.HasValue = False Then
            If m_objFeature IsNot Nothing AndAlso m_objFeature.ProjectId.HasValue Then
                ProjectId = m_objFeature.ProjectId.Value
            Else
                Throw New ApplicationException("Cannot create work item without project")
            End If
        End If

        If FeatureId.HasValue = False Then
            If m_objFeature IsNot Nothing AndAlso m_objFeature.Id.HasValue Then
                FeatureId = m_objFeature.Id.Value
            Else
                Throw New ApplicationException("Cannot create work item without feature")
            End If
        End If

        objInnerSettings = New clsSettings(objSettings)
        Try
            m_objWorkItemData.Create(objInnerSettings)
            objInnerSettings.DatabaseTransaction.Commit()
            objInnerSettings.DatabaseConnection.Close()
        Catch
            objInnerSettings.DatabaseTransaction.Rollback()
            Throw
        Finally
            If objInnerSettings.DatabaseTransaction IsNot Nothing Then
                objInnerSettings.DatabaseTransaction.Dispose()
                objInnerSettings.DatabaseTransaction = Nothing
            End If
            If objInnerSettings.DatabaseConnection IsNot Nothing Then
                objInnerSettings.DatabaseConnection.Dispose()
                objInnerSettings.DatabaseConnection = Nothing
            End If
        End Try
    End Sub

    Public Sub Update(ByVal objSettings As ISettings)
        Dim objInnerSettings As clsSettings

        objInnerSettings = New clsSettings(objSettings)
        Try
            m_objWorkItemData.Update(objInnerSettings)
            objInnerSettings.DatabaseTransaction.Commit()
            objInnerSettings.DatabaseConnection.Close()
        Catch
            objInnerSettings.DatabaseTransaction.Rollback()
            Throw
        Finally
            If objInnerSettings.DatabaseTransaction IsNot Nothing Then
                objInnerSettings.DatabaseTransaction.Dispose()
                objInnerSettings.DatabaseTransaction = Nothing
            End If
            If objInnerSettings.DatabaseConnection IsNot Nothing Then
                objInnerSettings.DatabaseConnection.Dispose()
                objInnerSettings.DatabaseConnection = Nothing
            End If
        End Try
    End Sub

    Public Shared Sub Delete(ByVal objSettings As ISettings, ByVal intId As Integer)
        Dim objInnerSettings As clsSettings

        objInnerSettings = New clsSettings(objSettings)
        Try
            clsWorkItemData.Delete(objInnerSettings, intId)
            objInnerSettings.DatabaseTransaction.Commit()
            objInnerSettings.DatabaseConnection.Close()
        Catch
            objInnerSettings.DatabaseTransaction.Rollback()
            Throw
        Finally
            If objInnerSettings.DatabaseTransaction IsNot Nothing Then
                objInnerSettings.DatabaseTransaction.Dispose()
                objInnerSettings.DatabaseTransaction = Nothing
            End If
            If objInnerSettings.DatabaseConnection IsNot Nothing Then
                objInnerSettings.DatabaseConnection.Dispose()
                objInnerSettings.DatabaseConnection = Nothing
            End If
        End Try
    End Sub
End Class
