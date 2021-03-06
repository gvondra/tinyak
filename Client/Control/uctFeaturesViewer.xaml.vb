﻿Public Class uctFeaturesViewer
    Private m_objFeatureViewer As clsFeaturesViewerVM
    Private m_objSelectedItteration As clsItterationVM

    Public Event FeatureDoubleClick(ByVal objSender As Object, ByVal objFeature As clsFeatureListItemVM)
    Public Event WorkItemDoubleClick(ByVal objSender As Object, ByVal objWorkItem As clsWorkListItemVM)

    Private Function SessionId() As Guid
        Return winMain.SessionId
    End Function

    Public Sub LoadBacklog(ByVal objProject As clsProject)
        m_objFeatureViewer.Project = objProject
        LoadBacklog()
    End Sub

    Private Sub LoadBacklog()
        Dim objLoadItterations As GetItterationsDelegate

        m_objFeatureViewer.SelectedItterationId = Nothing
        m_objFeatureViewer.FeatureList.LoadBacklog(SessionId, Dispatcher)
        m_objFeatureViewer.FeatureList.ItterationDateVisibility = Visibility.Collapsed

        objLoadItterations = New GetItterationsDelegate(AddressOf clsItteration.GetByProjectId)
        objLoadItterations.BeginInvoke(New clsSettings, winMain.SessionId, m_objFeatureViewer.Project.Id, AddressOf GetItterationsCallback, objLoadItterations)
    End Sub

    Private Sub uctFeaturesViewer_Initialized(sender As Object, e As EventArgs) Handles Me.Initialized
        m_objFeatureViewer = New clsFeaturesViewerVM
        DataContext = m_objFeatureViewer
    End Sub

    Private Sub lnkBacklog_Click(sender As Object, e As RoutedEventArgs) Handles lnkBacklog.Click
        Try
            LoadBacklog()
        Catch ex As Exception
            winException.BeginProcessException(ex, Dispatcher)
        End Try
    End Sub

    Private Sub uctFeatureList_FeatureDoubleClick(objSender As Object, objFeature As clsFeatureListItemVM)
        RaiseEvent FeatureDoubleClick(objSender, objFeature)
    End Sub

    Private Sub uctFeatureList_WorkItemDoubleClick(objSender As Object, objWorkItem As clsWorkListItemVM)
        RaiseEvent WorkItemDoubleClick(objSender, objWorkItem)
    End Sub

    Private Delegate Function GetItterationsDelegate(ByVal objSettings As ISettings, ByVal objSessionId As Guid, ByVal intProjectId As Integer) As List(Of clsItteration)
    Private Sub GetItterationsCallback(ByVal objResult As IAsyncResult)
        Dim colItteration As List(Of clsItteration)
        Dim i As Integer
        Dim objLoad As LoadItterationsDelegate
        Try
            colItteration = CType(objResult.AsyncState, GetItterationsDelegate).EndInvoke(objResult)
            If colItteration IsNot Nothing Then
                For i = colItteration.Count - 1 To 0 Step -1
                    If colItteration(i).IsActive = False Then
                        colItteration.RemoveAt(i)
                    End If
                Next
            End If
            objLoad = New LoadItterationsDelegate(AddressOf LoadItterations)
            Dispatcher.Invoke(objLoad, colItteration)
        Catch ex As Exception
            winException.BeginProcessException(ex, Dispatcher)
        End Try
    End Sub

    Private Delegate Sub LoadItterationsDelegate(ByVal colItteration As List(Of clsItteration))
    Private Sub LoadItterations(ByVal colItteration As List(Of clsItteration))
        Dim objItteration As clsItteration

        m_objFeatureViewer.Itterations.Clear()

        For Each objItteration In colItteration
            m_objFeatureViewer.Itterations.Add(New clsItterationVM(objItteration))
        Next
    End Sub

    Private Sub Hyperlink_Click(sender As Object, e As RoutedEventArgs)
        Try
            m_objSelectedItteration = DirectCast(CType(sender, Hyperlink).DataContext, clsItterationVM)
            m_objFeatureViewer.SelectedItterationId = m_objSelectedItteration.Id
            m_objFeatureViewer.FeatureList.LoadBacklog(SessionId, Dispatcher, m_objSelectedItteration.Id.Value)
            m_objFeatureViewer.FeatureList.ItterationStartDate = m_objSelectedItteration.StartDate
            m_objFeatureViewer.FeatureList.ItterationEndDate = m_objSelectedItteration.EndDate
            If m_objSelectedItteration.StartDate.HasValue OrElse m_objSelectedItteration.EndDate.HasValue Then
                m_objFeatureViewer.FeatureList.ItterationDateVisibility = Visibility.Visible
            Else
                m_objFeatureViewer.FeatureList.ItterationDateVisibility = Visibility.Collapsed
            End If
        Catch ex As Exception
            winException.ProcessException(ex)
        End Try
    End Sub

    Private Sub uctFeatureList_WorkItemDelete(objSender As Object, objWorkItem As clsWorkListItemVM)
        Dim intItterationId As Nullable(Of Integer)
        Try
            If m_objSelectedItteration IsNot Nothing Then
                intItterationId = m_objSelectedItteration.Id
            Else
                intItterationId = Nothing
            End If
            m_objFeatureViewer.FeatureList.LoadBacklog(SessionId, Dispatcher, intItterationId)
        Catch ex As Exception
            winException.ProcessException(ex)
        End Try
    End Sub
End Class
