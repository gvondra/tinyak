Public Class uctAddFeature
    Public Event AfterAddFeature(ByVal objSender As Control, ByVal objFeature As clsFeatureListItem)
    Public Event OnHide(ByVal objSender As Control)

    Private ReadOnly Property ProjectId As Integer
        Get
            Return DirectCast(DataContext, clsAddFeatureVM).Project.Id
        End Get
    End Property

    Private Property Title As String
        Get
            Return DirectCast(DataContext, clsAddFeatureVM).Title
        End Get
        Set(value As String)
            DirectCast(DataContext, clsAddFeatureVM).Title = value
        End Set
    End Property

    Private Function SessionId() As Guid
        Return DirectCast(Window.GetWindow(Me), winMain).SessionId
    End Function

    Private Sub btnAdd_Click(sender As Object, e As RoutedEventArgs) Handles btnAdd.Click
        Dim objFeature As clsFeatureListItem
        Try
            If String.IsNullOrEmpty(Title) = False Then
                objFeature = clsFeatureListItem.Create(New clsSettings, SessionId, ProjectId, Title)
                RaiseEvent AfterAddFeature(Me, objFeature)
                Title = String.Empty
            End If
        Catch ex As Exception
            winException.BeginProcessException(ex, Dispatcher)
        End Try
    End Sub

    Private Sub btnHide_Click(sender As Object, e As RoutedEventArgs) Handles btnHide.Click
        RaiseEvent OnHide(Me)
    End Sub
End Class
