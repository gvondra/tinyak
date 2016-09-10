Public Class winFeature

    Private ReadOnly Property FeatureTitle As String
        Get
            Return txtTitle.Text.Trim
        End Get
    End Property

    Private ReadOnly Property Feature As clsFeatureVM
        Get
            Return DirectCast(DataContext, clsFeatureVM)
        End Get
    End Property

    Private Sub btnSave_Click(sender As Object, e As RoutedEventArgs) Handles btnSave.Click
        Try
            If ValidateData() Then
                txtTitle.GetBindingExpression(TextBox.TextProperty).UpdateSource()
                Feature.Save(winMain.SessionId)
            End If
        Catch ex As Exception
            winException.BeginProcessException(ex, Dispatcher)
        End Try
    End Sub

    Private Function ValidateData() As Boolean
        Dim intErrorCount As Integer

        intErrorCount = 0
        If String.IsNullOrEmpty(FeatureTitle) Then
            Feature.TitleMessage = "Title is required"
            Feature.TitleMessageVisiblity = Visibility.Visible
            intErrorCount += 1
        Else
            Feature.TitleMessageVisiblity = Visibility.Collapsed
        End If
        Return (intErrorCount = 0)
    End Function
End Class
