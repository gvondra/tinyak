Imports System.Collections.ObjectModel
Imports tinyak.Client

Public Class winMain
    Private WithEvents m_objFeatureViewer As uctFeaturesViewer
    Private m_objCurrentProject As clsProject
    Private m_objFeatureWindows As Dictionary(Of Integer, winFeature)

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
End Class
