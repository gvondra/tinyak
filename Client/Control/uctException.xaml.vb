Public Class uctException
    Private m_objExcpeptionVm As clsExceptionListVM

    Private Sub uctException_DataContextChanged(sender As Object, e As DependencyPropertyChangedEventArgs) Handles Me.DataContextChanged
        If TypeOf e.NewValue Is clsExceptionListVM Then
            m_objExcpeptionVm = DirectCast(e.NewValue, clsExceptionListVM)
        End If
    End Sub

    Private Sub uctException_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        Dim objControl As uctException
        If m_objExcpeptionVm IsNot Nothing AndAlso m_objExcpeptionVm.InnerException IsNot Nothing Then
            objControl = New uctException
            objControl.DataContext = m_objExcpeptionVm.InnerException
            pnlInnerExcpetion.Children.Add(objControl)
            lblInnerException.Visibility = Visibility.Visible
            pnlInnerExcpetion.Visibility = Visibility.Visible
        End If
    End Sub
End Class
