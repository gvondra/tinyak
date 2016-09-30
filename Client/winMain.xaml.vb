Imports System.Collections.ObjectModel
Imports tinyak.Client

Public Class winMain
    Private WithEvents m_objFeatureViewer As uctFeaturesViewer
    Private m_objCurrentProject As clsProject
    Private m_objFeatureWindows As Dictionary(Of Integer, winFeature)
    Private m_objWorkItemWindows As Dictionary(Of Integer, winWorkItem)
    Private m_blnIsAdministrator As Boolean

    Public Shared Property SessionId As Guid
    Public Property Projects As ObservableCollection(Of clsProject)


    Private Sub mnuExit_Click(sender As Object, e As RoutedEventArgs) Handles mnuExit.Click
        Me.Close()
    End Sub

    Private Sub LoadSessionId()
        Dim objSettings As clsSettings
        Try
            objSettings = New clsSettings
            SessionId = clsSession.GetSessionId(objSettings)
        Catch ex As Exception
            winException.BeginProcessException(ex, Dispatcher)
        End Try
    End Sub

    Private Sub winMain_Initialized(sender As Object, e As EventArgs) Handles Me.Initialized
        Dim objLoadSessionId As Action

        Projects = New ObservableCollection(Of clsProject)
        m_objFeatureWindows = New Dictionary(Of Integer, winFeature)
        m_objWorkItemWindows = New Dictionary(Of Integer, winWorkItem)
        ctlProjectList.DataContext = Me
        objLoadSessionId = New Action(AddressOf LoadSessionId)
        objLoadSessionId.BeginInvoke(Nothing, objLoadSessionId)
    End Sub

    Friend Sub OnLoggedOn(ByVal objSender As Control, ByVal objUser As clsUser)
        Dim objSettings As clsSettings
        Dim colProject As List(Of clsProject)
        Dim objProject As clsProject
        Try
            objSettings = New clsSettings
            m_blnIsAdministrator = objUser.IsAdministrator
            If m_blnIsAdministrator Then
                mnuAdministration.Visibility = Visibility.Visible
            Else
                mnuAdministration.Visibility = Visibility.Collapsed
            End If
            lblUser.DataContext = New clsUserVM(objUser)
            pnlControl.Children.Clear()
            colProject = clsProject.Get(objSettings, SessionId)
            Projects.Clear()
            For Each objProject In colProject
                Projects.Add(objProject)
            Next
            ctlProjectList.Visibility = Visibility.Visible
        Catch ex As Exception
            winException.BeginProcessException(ex, Dispatcher)
        End Try
    End Sub

    Private Sub ctlProjectList_OnProjectSelected(objSender As Control, objProject As clsProject) Handles ctlProjectList.OnProjectSelected
        Try
            m_objCurrentProject = objProject
            m_objFeatureViewer = New uctFeaturesViewer
            pnlControl.Children.Clear()
            pnlControl.Children.Add(m_objFeatureViewer)
            ctlProjectList.Visibility = Visibility.Collapsed
            m_objFeatureViewer.LoadBacklog(objProject)
        Catch ex As Exception
            winException.BeginProcessException(ex, Dispatcher)
        End Try
    End Sub

    Private Sub mnuAddFeature_Click(sender As Object, e As RoutedEventArgs) Handles mnuAddFeature.Click
        If m_objFeatureViewer IsNot Nothing Then
            DirectCast(m_objFeatureViewer.DataContext, clsFeaturesViewerVM).ShowAddFeature()
        End If
    End Sub

    Private Sub m_objFeatureViewer_FeatureDoubleClick(objSender As Object, objFeature As clsFeatureListItemVM) Handles m_objFeatureViewer.FeatureDoubleClick
        Dim objWindow As winFeature
        Dim objShowFeature As ShowFeatureDelegate

        Try
            If m_objFeatureWindows.ContainsKey(objFeature.Id) Then
                objWindow = m_objFeatureWindows(objFeature.Id)
            Else
                objWindow = New winFeature
                objWindow.Show()
                AddHandler objWindow.Closing, AddressOf OnFeatureWindowClosing
                m_objFeatureWindows(objFeature.Id) = objWindow

                objShowFeature = New ShowFeatureDelegate(AddressOf ShowFeature)
                objShowFeature.BeginInvoke(New clsSettings, SessionId, objFeature.Id, objFeature, Nothing, objShowFeature)
            End If
            objWindow.Activate()
        Catch ex As Exception
            winException.BeginProcessException(ex, Dispatcher)
        End Try
    End Sub

    Private Delegate Sub ShowFeatureDelegate(ByVal objSettings As ISettings, ByVal objSessionId As Guid, ByVal intFeatureId As Integer, ByVal objListItem As clsFeatureListItemVM)
    Private Sub ShowFeature(ByVal objSettings As ISettings, ByVal objSessionId As Guid, ByVal intFeatureId As Integer, ByVal objListItem As clsFeatureListItemVM)
        Dim objFeature As clsFeature
        Dim objWindow As winFeature
        Dim objSetDataContext As SetDataContextDelegate
        Dim objVm As clsFeatureVM
        Try
            objFeature = clsFeature.Get(objSettings, objSessionId, intFeatureId)
            If m_objFeatureWindows.ContainsKey(objFeature.Id) Then
                objVm = New clsFeatureVM(objFeature)
                objWindow = m_objFeatureWindows(objFeature.Id)
                objSetDataContext = New SetDataContextDelegate(AddressOf SetDataContext)
                Dispatcher.Invoke(objSetDataContext, objWindow, objVm)
                objVm.RegisterObserver(objListItem)
            End If
        Catch ex As Exception
            winException.BeginProcessException(ex, Dispatcher)
        End Try
    End Sub

    Private Delegate Sub SetDataContextDelegate(ByVal objWindow As Window, ByVal obj As Object)
    Private Sub SetDataContext(ByVal objWindow As Window, ByVal obj As Object)
        objWindow.DataContext = obj
    End Sub

    Private Sub OnFeatureWindowClosing(ByVal objSender As Object, ByVal e As System.ComponentModel.CancelEventArgs)
        Dim objEnumerator As Dictionary(Of Integer, winFeature).Enumerator
        Dim intFeatureId As Nullable(Of Integer)

        If objSender.GetType.Equals(GetType(winFeature)) Then
            intFeatureId = Nothing
            If DirectCast(objSender, winFeature).DataContext IsNot Nothing Then
                intFeatureId = DirectCast(DirectCast(objSender, winFeature).DataContext, clsFeatureVM).Id
            Else
                objEnumerator = m_objFeatureWindows.GetEnumerator
                While intFeatureId.HasValue = False AndAlso objEnumerator.MoveNext
                    If objEnumerator.Current.Value Is objSender Then
                        intFeatureId = objEnumerator.Current.Key
                    End If
                End While
            End If

            If intFeatureId.HasValue AndAlso m_objFeatureWindows.ContainsKey(intFeatureId.Value) Then
                m_objFeatureWindows.Remove(intFeatureId.Value)
            End If
        End If
    End Sub

    Private Sub winMain_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        ctlLogin.Focus()
    End Sub

    Private Sub m_objFeatureViewer_WorkItemDoubleClick(objSender As Object, objWorkItem As clsWorkListItemVM) Handles m_objFeatureViewer.WorkItemDoubleClick
        Dim objWindow As winWorkItem
        Dim objShowWorkItem As ShowWorkItemDelegate

        Try
            If m_objWorkItemWindows.ContainsKey(objWorkItem.Id) Then
                objWindow = m_objWorkItemWindows(objWorkItem.Id)
            Else
                objWindow = New winWorkItem
                objWindow.Show()
                AddHandler objWindow.Closing, AddressOf OnWorkItemWindowClosing
                m_objWorkItemWindows(objWorkItem.Id) = objWindow

                objShowWorkItem = New ShowWorkItemDelegate(AddressOf ShowWorkItem)
                objShowWorkItem.BeginInvoke(New clsSettings, SessionId, objWorkItem, Nothing, objShowWorkItem)
            End If
            objWindow.Activate()
        Catch ex As Exception
            winException.BeginProcessException(ex, Dispatcher)
        End Try
    End Sub

    Private Delegate Sub ShowWorkItemDelegate(ByVal objSettings As ISettings, ByVal objSessionId As Guid, ByVal objWorkItem As clsWorkListItemVM)
    Private Sub ShowWorkItem(ByVal objSettings As ISettings, ByVal objSessionId As Guid, ByVal objListItem As clsWorkListItemVM)
        Dim objWorkItem As clsWorkItem
        Dim objWindow As winWorkItem
        Dim objSetDataContext As SetDataContextDelegate
        Dim objVm As clsWorkItemVM
        Try
            objWorkItem = clsWorkItem.Get(objSettings, objSessionId, objListItem.Id)
            If objWorkItem IsNot Nothing AndAlso m_objWorkItemWindows.ContainsKey(objWorkItem.Id.Value) Then
                objVm = New clsWorkItemVM(objWorkItem)
                objVm.LoadItterations(m_objCurrentProject.Id)
                objWindow = m_objWorkItemWindows(objWorkItem.Id.Value)
                objSetDataContext = New SetDataContextDelegate(AddressOf SetDataContext)
                Dispatcher.Invoke(objSetDataContext, objWindow, objVm)
                objVm.RegisterObserver(objListItem)
            End If
        Catch ex As Exception
            winException.BeginProcessException(ex, Dispatcher)
        End Try
    End Sub

    Private Sub OnWorkItemWindowClosing(ByVal objSender As Object, ByVal e As System.ComponentModel.CancelEventArgs)
        Dim objEnumerator As Dictionary(Of Integer, winWorkItem).Enumerator
        Dim intWorkItemId As Nullable(Of Integer)

        If objSender.GetType.Equals(GetType(winWorkItem)) Then
            intWorkItemId = Nothing
            If DirectCast(objSender, winWorkItem).DataContext IsNot Nothing Then
                intWorkItemId = DirectCast(DirectCast(objSender, winWorkItem).DataContext, clsWorkItemVM).Id
            Else
                objEnumerator = m_objWorkItemWindows.GetEnumerator
                While intWorkItemId.HasValue = False AndAlso objEnumerator.MoveNext
                    If objEnumerator.Current.Value Is objSender Then
                        intWorkItemId = objEnumerator.Current.Key
                    End If
                End While
            End If

            If intWorkItemId.HasValue AndAlso m_objWorkItemWindows.ContainsKey(intWorkItemId.Value) Then
                m_objWorkItemWindows.Remove(intWorkItemId.Value)
            End If
        End If
    End Sub

    Private Sub mnuWebMetrics_Click(sender As Object, e As RoutedEventArgs) Handles mnuWebMetrics.Click
        Dim objControl As uctWebMetrics
        Try
            ctlProjectList.Visibility = Visibility.Visible
            pnlControl.Children.Clear()
            objControl = New uctWebMetrics
            objControl.Load()
            pnlControl.Children.Add(objControl)
        Catch ex As Exception
            winException.ProcessException(ex)
        End Try
    End Sub

    Private Sub mnuExceptionLog_Click(sender As Object, e As RoutedEventArgs) Handles mnuExceptionLog.Click
        Dim objControl As uctExceptions
        Try
            ctlProjectList.Visibility = Visibility.Visible
            pnlControl.Children.Clear()
            objControl = New uctExceptions
            objControl.Load()
            pnlControl.Children.Add(objControl)
        Catch ex As Exception
            winException.ProcessException(ex)
        End Try
    End Sub
End Class
