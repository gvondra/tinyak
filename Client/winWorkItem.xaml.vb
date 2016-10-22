Imports System.Timers

Public Class winWorkItem
    Private WithEvents tmrHideStatus As Timers.Timer
    Private ReadOnly Property WorkItem As clsWorkItemVM
        Get
            Return DirectCast(DataContext, clsWorkItemVM)
        End Get
    End Property

    Private Sub btnSave_Click(sender As Object, e As RoutedEventArgs) Handles btnSave.Click
        Try
            If ValidateData() Then
                txtTitle.GetBindingExpression(TextBox.TextProperty).UpdateSource()
                txtAssignedTo.GetBindingExpression(TextBox.TextProperty).UpdateSource()
                txtEffort.GetBindingExpression(TextBox.TextProperty).UpdateSource()
                txtDescription.GetBindingExpression(TextBox.TextProperty).UpdateSource()
                txtAcceptanceCriteria.GetBindingExpression(TextBox.TextProperty).UpdateSource()
                cboState.GetBindingExpression(ComboBox.SelectedValueProperty).UpdateSource()
                cboItteration.GetBindingExpression(ComboBox.SelectedItemProperty).UpdateSource()
                WorkItem.Update(New clsSettings, winMain.SessionId)
                lblStatus.Text = "Item Saved"
                tmrHideStatus.Enabled = True
            End If
        Catch ex As Exception
            winException.BeginProcessException(ex, Dispatcher)
        End Try
    End Sub

    Private Sub tmrHideStatus_Elapsed(sender As Object, e As ElapsedEventArgs) Handles tmrHideStatus.Elapsed
        Dim objSetStatus As SetStatusDelegate

        tmrHideStatus.Enabled = False
        objSetStatus = New SetStatusDelegate(AddressOf SetStatus)
        Dispatcher.Invoke(objSetStatus, String.Empty)
    End Sub

    Private Delegate Sub SetStatusDelegate(ByVal strStatus As String)
    Private Sub SetStatus(ByVal strStatus As String)
        lblStatus.Text = strStatus
    End Sub

    Private Sub winWorkItem_Initialized(sender As Object, e As EventArgs) Handles Me.Initialized
        tmrHideStatus = New Timers.Timer()
        tmrHideStatus.Interval = 5000
    End Sub

    Private Function ValidateData() As Boolean
        Dim intErrorCount As Integer
        Dim intValue As Int16

        intErrorCount = 0
        If String.IsNullOrEmpty(txtTitle.Text.Trim) Then
            WorkItem.TitleMessage = "Title is required"
            WorkItem.TitleMessageVisibility = Visibility.Visible
            intErrorCount += 1
        Else
            WorkItem.TitleMessageVisibility = Visibility.Collapsed
        End If

        WorkItem.EffortMessageVisibility = Visibility.Collapsed
        If txtEffort.Text.Trim.Length > 0 Then
            If Int16.TryParse(txtEffort.Text.Trim, intValue) = False Then
                WorkItem.EffortMessage = "Invalid effort"
                WorkItem.EffortMessageVisibility = Visibility.Visible
                intErrorCount += 1
            End If
        End If
        Return (intErrorCount = 0)
    End Function
End Class
