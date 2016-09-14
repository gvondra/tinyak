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

    Public Sub Update(ByVal objSettings As ISettings)
        Dim objInnerSettings As clsSettings

        objInnerSettings = New clsSettings(objSettings)
        Try
            m_objProjectData.Update(objInnerSettings)
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

    Public Function GetNewFeature() As clsFeature
        Return clsFeature.GetNew(Me)
    End Function

    Public Function GetFeatures(ByVal objSettings As ISettings) As List(Of clsFeature)
        If Id.HasValue Then
            Return clsFeature.GetByProject(New clsSettings(objSettings), Me)
        Else
            Return Nothing
        End If
    End Function

    Public Shared Function [Get](ByVal objSettings As ISettings, ByVal intId As Integer) As clsProject
        Dim objData As clsProjectData

        objData = clsProjectData.Get(New clsSettings(objSettings), intId)
        If objData IsNot Nothing Then
            Return New clsProject(objData)
        Else
            Return Nothing
        End If
    End Function

    Public Function CanUserUpdate(ByVal objUser As clsUser) As Boolean
        Return objUser.Id.Value = OwnerId
    End Function

    Public Shared Function GetByOwnerId(ByVal objSettings As ISettings, ByVal intOwnerId As Integer) As List(Of clsProject)
        Dim colData As List(Of clsProjectData)
        Dim objData As clsProjectData
        Dim colResult As List(Of clsProject)

        colData = clsProjectData.GetByOwner(New clsSettings(objSettings), intOwnerId)
        colResult = New List(Of clsProject)
        For Each objData In colData
            colResult.Add(New clsProject(objData))
        Next objData
        Return colResult
    End Function

    Public Sub AddMember(ByVal objSettings As ISettings, ByVal strEmailAddress As String)
        Dim objInnerSettings As clsSettings

        If Id.HasValue AndAlso IsProjectMember(strEmailAddress) = False Then
            objInnerSettings = New clsSettings(objSettings)
            Try
                clsProjectMemberData.Create(objInnerSettings, Id.Value, strEmailAddress)
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
            If ProjectMembers Is Nothing Then
                ProjectMembers = New List(Of String)
            End If
            ProjectMembers.Add(strEmailAddress)
        End If
    End Sub

    Public Sub RemoveMember(ByVal objSettings As ISettings, ByVal strEmailAddress As String)
        Dim objInnerSettings As clsSettings
        Dim i As Integer
        If Id.HasValue AndAlso IsProjectMember(strEmailAddress) Then
            objInnerSettings = New clsSettings(objSettings)
            Try
                clsProjectMemberData.Remove(objInnerSettings, Id.Value, strEmailAddress)
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
            For i = ProjectMembers.Count - 1 To 0 Step -1
                If String.Compare(strEmailAddress, ProjectMembers(i), True) = 0 Then
                    ProjectMembers.RemoveAt(i)
                End If
            Next i
        End If
    End Sub

    Public Function IsProjectMember(ByVal strEmailAddress As String) As Boolean
        Dim blnFound As Boolean
        Dim objEnumerator As IEnumerator(Of String)

        blnFound = False
        If ProjectMembers IsNot Nothing Then
            objEnumerator = ProjectMembers.GetEnumerator
            While blnFound = False AndAlso objEnumerator.MoveNext
                If String.Compare(strEmailAddress, objEnumerator.Current, True) = 0 Then
                    blnFound = True
                End If
            End While
        End If
        Return blnFound
    End Function

    Public Shared Function GetByEmailAddress(ByVal objSettings As ISettings, ByVal strEmailAddress As String) As List(Of clsProject)
        Dim colData As List(Of clsProjectData)
        Dim objData As clsProjectData
        Dim colResult As List(Of clsProject)

        colData = clsProjectData.GetByEmailAddress(New clsSettings(objSettings), strEmailAddress)
        If colData IsNot Nothing Then
            colResult = New List(Of clsProject)
            For Each objData In colData
                colResult.Add(New clsProject(objData))
            Next objData
        Else
            colResult = Nothing
        End If
        Return colResult
    End Function

    Public Function IncludesMember(ByVal strEmailAddress As String) As Boolean
        Dim blnResult As Boolean
        Dim objEnumerator As IEnumerator(Of String)

        blnResult = False
        If ProjectMembers IsNot Nothing Then
            objEnumerator = ProjectMembers.GetEnumerator
            While blnResult = False AndAlso objEnumerator.MoveNext
                If String.Compare(objEnumerator.Current, strEmailAddress, True) = 0 Then
                    blnResult = True
                End If
            End While
        End If
        Return blnResult
    End Function

    Public Function GetItterations(ByVal objSettings As ISettings) As List(Of clsItteration)
        If Id.HasValue Then
            Return clsItteration.GetByProject(New clsSettings(objSettings), Me)
        Else
            Return Nothing
        End If
    End Function

    Public Function GetNewItteration() As clsItteration
        Return clsItteration.GetNew(Me)
    End Function

    Public Sub LoadWorkItemItteration(ByVal objSettings As ISettings, ByVal colWorkItem As List(Of clsWorkItem))
        Dim objItterations As SortedDictionary(Of Integer, clsItteration)
        Dim objWorkItem As clsWorkItem

        If colWorkItem IsNot Nothing AndAlso colWorkItem.Count > 0 Then
            objItterations = clsItteration.GetItterationDictionary(clsItteration.GetByProject(New clsSettings(objSettings), Me))
            For Each objWorkItem In colWorkItem
                objWorkItem.SetItteration(objItterations)
            Next
        End If
    End Sub
End Class
