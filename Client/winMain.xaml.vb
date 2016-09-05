Imports System.Collections.ObjectModel
Public Class winMain
    Private m_objFeatureViewer As uctFeaturesViewer
    Private m_objCurrentProject As clsProject

    Public Property SessionId As Guid
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
End Class
