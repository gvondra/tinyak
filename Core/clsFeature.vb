Imports tinyak.Data
Public Class clsFeature
    Private m_objFeatureData As clsFeatureData
    Private m_objProject As clsProject

    Private Sub New(ByVal objFeatureData As clsFeatureData, ByVal objProject As clsProject)
        m_objFeatureData = objFeatureData
        m_objProject = objProject
    End Sub

    Public ReadOnly Property Id As Nullable(Of Integer)
        Get
            Return m_objFeatureData.Id
        End Get
    End Property

    Private Property ProjectId As Nullable(Of Integer)
        Get
            Return m_objFeatureData.ProjectId
        End Get
        Set
            m_objFeatureData.ProjectId = Value
        End Set
    End Property

    Public Property Title As String
        Get
            Return m_objFeatureData.Title
        End Get
        Set
            m_objFeatureData.Title = Value
        End Set
    End Property

    Friend Shared Function GetNew(ByVal objProject As clsProject) As clsFeature
        Dim objFeature As clsFeature

        objFeature = New clsFeature(New clsFeatureData, objProject)
        Return objFeature
    End Function

    Public Sub Create(ByVal objSettings As ISettings)
        Dim objInnerSettings As clsSettings

        If ProjectId.HasValue = False Then
            If m_objProject IsNot Nothing AndAlso m_objProject.Id.HasValue Then
                ProjectId = m_objProject.Id.Value
            Else
                Throw New ApplicationException("Cannot create feature with project")
            End If
        End If

        objInnerSettings = New clsSettings(objSettings)
        Try
            m_objFeatureData.Create(objInnerSettings)
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
            m_objFeatureData.Update(objInnerSettings)
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
            clsFeatureData.Delete(objInnerSettings, intId)
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

    Friend Shared Function GetByProject(ByVal objSettings As clsSettings, ByVal objProject As clsProject) As List(Of clsFeature)
        Dim colData As List(Of clsFeatureData)
        Dim objData As clsFeatureData
        Dim colResult As List(Of clsFeature)

        colResult = Nothing
        If objProject IsNot Nothing AndAlso objProject.Id.HasValue Then
            colData = clsFeatureData.GetByProjectId(objSettings, objProject.Id.Value)
            If colData IsNot Nothing Then
                colResult = New List(Of clsFeature)
                For Each objData In colData
                    colResult.Add(New clsFeature(objData, objProject))
                Next objData
            End If
        End If
        Return colResult
    End Function
End Class
