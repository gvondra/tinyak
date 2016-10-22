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
    Private m_objProject As clsProject
    Private m_objItteration As clsItteration

    Private Sub New(ByVal objWorkItemData As clsWorkItemData, ByVal objFeature As clsFeature)
        m_objWorkItemData = objWorkItemData
        m_objFeature = objFeature
    End Sub

    Private Sub New(ByVal objWorkItemData As clsWorkItemData)
        m_objWorkItemData = objWorkItemData
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

    Public Property AssignedTo As String
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

    Private Property ItterationId As Nullable(Of Integer)
        Get
            Return m_objWorkItemData.ItterationId
        End Get
        Set(value As Nullable(Of Integer))
            m_objWorkItemData.ItterationId = value
        End Set
    End Property

    Friend Shared Function GetNew(ByVal objFeature As clsFeature) As clsWorkItem
        Return New clsWorkItem(New clsWorkItemData, objFeature) With {.AcceptanceCriteria = String.Empty, .Description = String.Empty, .AssignedTo = String.Empty}
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
            If m_objItteration IsNot Nothing Then
                ItterationId = m_objItteration.Id
            Else
                ItterationId = Nothing
            End If

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

    Public Sub Delete(ByVal objSettings As ISettings)
        Dim objInnerSettings As clsSettings

        If Id.HasValue Then
            objInnerSettings = New clsSettings(objSettings)
            Try
                clsWorkItemData.Delete(objInnerSettings, Id.Value)
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
        End If
    End Sub

    Friend Shared Function GetByFeature(ByVal objSettings As clsSettings, ByVal objFeature As clsFeature) As List(Of clsWorkItem)
        Dim colData As List(Of clsWorkItemData)
        Dim objData As clsWorkItemData
        Dim colResult As List(Of clsWorkItem)

        colData = clsWorkItemData.GetByFeatureId(objSettings, objFeature.Id.Value)
        If colData IsNot Nothing Then
            colResult = New List(Of clsWorkItem)
            For Each objData In colData
                colResult.Add(New clsWorkItem(objData, objFeature))
            Next
        Else
            colResult = Nothing
        End If
        Return colResult
    End Function

    Public Shared Function [Get](ByVal objSettings As ISettings, ByVal intWorkItemId As Integer) As clsWorkItem
        Dim objData As clsWorkItemData
        objData = clsWorkItemData.Get(New clsSettings(objSettings), intWorkItemId)
        If objData IsNot Nothing Then
            Return New clsWorkItem(objData)
        Else
            Return Nothing
        End If
    End Function

    Public Function GetProject(ByVal objSettings As ISettings) As clsProject
        If m_objProject Is Nothing AndAlso ProjectId.HasValue Then
            m_objProject = clsProject.Get(objSettings, ProjectId.Value)
        End If
        Return m_objProject
    End Function

    Friend Sub SetItteration(ByVal objItterationDictionary As SortedDictionary(Of Integer, clsItteration))
        If ItterationId.HasValue AndAlso objItterationDictionary.ContainsKey(ItterationId.Value) Then
            m_objItteration = objItterationDictionary(ItterationId.Value)
        Else
            m_objItteration = Nothing
        End If
    End Sub

    Public Sub SetItteration(ByVal objItteration As clsItteration)
        If objItteration Is Nothing Then
            m_objItteration = Nothing
        ElseIf ProjectId.HasValue AndAlso objItteration.IsSameProject(ProjectId.Value) Then
            m_objItteration = objItteration
        Else
            Throw New ApplicationException("Project mismatch")
        End If
    End Sub

    Public Function GetItteration(ByVal objSettings As ISettings) As clsItteration
        If m_objItteration Is Nothing AndAlso ItterationId.HasValue Then
            m_objItteration = clsItteration.Get(objSettings, ItterationId.Value)
        End If
        Return m_objItteration
    End Function
End Class
