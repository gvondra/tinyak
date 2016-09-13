Imports tinyak.Data
Public Class clsItteration
    Private m_objItterationData As clsItterationData
    Private m_objProject As clsProject

    Private Sub New(ByVal objItterationData As clsItterationData)
        m_objItterationData = objItterationData
    End Sub

    Public ReadOnly Property Id As Nullable(Of Integer)
        Get
            Return m_objItterationData.Id
        End Get
    End Property

    Private Property ProjectId As Nullable(Of Integer)
        Get
            Return m_objItterationData.ProjectId
        End Get
        Set(value As Nullable(Of Integer))
            m_objItterationData.ProjectId = value
        End Set
    End Property

    Public Property Name As String
        Get
            Return m_objItterationData.Name
        End Get
        Set(value As String)
            m_objItterationData.Name = value
        End Set
    End Property

    Public Property StartDate As Nullable(Of Date)
        Get
            Return m_objItterationData.StartDate
        End Get
        Set(value As Nullable(Of Date))
            m_objItterationData.StartDate = value
        End Set
    End Property

    Public Property EndDate As Nullable(Of Date)
        Get
            Return m_objItterationData.EndDate
        End Get
        Set(value As Nullable(Of Date))
            m_objItterationData.EndDate = value
        End Set
    End Property

    Public Property IsActive As Boolean
        Get
            Return m_objItterationData.IsActive
        End Get
        Set(value As Boolean)
            m_objItterationData.IsActive = value
        End Set
    End Property

    Friend Shared Function GetByProject(ByVal objSettings As clsSettings, ByVal objProject As clsProject) As List(Of clsItteration)
        Dim colData As List(Of clsItterationData)
        Dim objData As clsItterationData
        Dim colResult As List(Of clsItteration)
        Dim objResult As clsItteration

        colData = clsItterationData.GetByProjectId(objSettings, objProject.Id.Value)
        If colData IsNot Nothing Then
            colResult = New List(Of clsItteration)
            For Each objData In colData
                objResult = New clsItteration(objData)
                objResult.m_objProject = objProject
                colResult.Add(objResult)
            Next
        Else
            colResult = Nothing
        End If
        Return colResult
    End Function

    Friend Shared Function GetNew(ByVal objProject As clsProject) As clsItteration
        Dim objItteration As clsItteration
        objItteration = New clsItteration(New clsItterationData)
        objItteration.m_objProject = objProject
        Return objItteration
    End Function

    Public Sub Create(ByVal objSettings As ISettings)
        Dim objInnerSettings As clsSettings

        If ProjectId.HasValue = False Then
            If m_objProject IsNot Nothing AndAlso m_objProject.Id.HasValue Then
                ProjectId = m_objProject.Id.Value
            Else
                Throw New ApplicationException("Cannot create itteration without project")
            End If
        End If

        objInnerSettings = New clsSettings(objSettings)
        Try
            m_objItterationData.Create(objInnerSettings)
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
            m_objItterationData.Update(objInnerSettings)
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
            clsItterationData.Delete(objInnerSettings, intId)
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

    Public Shared Function [Get](ByVal objSettings As ISettings, ByVal intItterationId As Integer) As clsItteration
        Dim objData As clsItterationData
        objData = clsItterationData.Get(New clsSettings(objSettings), intItterationId)
        If objData IsNot Nothing Then
            Return New clsItteration(objData)
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
End Class
