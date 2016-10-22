Public Class uctAddWorkItem
    Public Event AfterAddWorkItem(ByVal objSender As Control, ByVal objWorkItem As clsWorkListItem)
    Public Event OnHide(ByVal objSender As Control)

    Private ReadOnly Property ProjectId As Integer
        Get
            Return DirectCast(DataContext, clsAddWorkItemVM).Project.Id
        End Get
    End Property

    Private Property Title As String
        Get
            Return DirectCast(DataContext, clsAddWorkItemVM).Title
        End Get
        Set(value As String)
            DirectCast(DataContext, clsAddWorkItemVM).Title = value
        End Set
    End Property

    Private ReadOnly Property Feature As clsFeatureListItem
        Get
            Return DirectCast(DataContext, clsAddWorkItemVM).Feature
        End Get
    End Property

    Private Function SessionId() As Guid
        Return winMain.SessionId
    End Function

    Private Sub btnAdd_Click(sender As Object, e As RoutedEventArgs) Handles btnAdd.Click
        Dim objWorkItem As clsWorkListItem
        Try
            If String.IsNullOrEmpty(Title) = False Then
                objWorkItem = clsWorkListItem.Create(New clsSettings, SessionId, ProjectId, Feature.Id, Title, DirectCast(DataContext, clsAddWorkItemVM).SelectedItterationId)
                RaiseEvent AfterAddWorkItem(Me, objWorkItem)
                Title = String.Empty
                txtTitle.Focus()
            End If
        Catch ex As Exception
            winException.BeginProcessException(ex, Dispatcher)
        End Try
    End Sub

    Private Sub btnHide_Click(sender As Object, e As RoutedEventArgs) Handles btnHide.Click
        RaiseEvent OnHide(Me)
    End Sub
End Class
