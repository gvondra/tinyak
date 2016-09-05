Public Class uctFeaturesViewer
    Private m_objFeatureViewer As clsFeaturesViewerVM

    Private Function SessionId() As Guid
        Return DirectCast(Window.GetWindow(Me), winMain).SessionId
    End Function

    Public Sub LoadBacklog(ByVal objProject As clsProject)
        m_objFeatureViewer.Project = objProject
        LoadBacklog()
    End Sub

    Private Sub LoadBacklog()
        m_objFeatureViewer.FeatureList.LoadBacklog(SessionId)
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
End Class
