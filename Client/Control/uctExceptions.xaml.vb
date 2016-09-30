Imports System.Collections.ObjectModel
Public Class uctExceptions
    Private m_colExceptionVm As ObservableCollection(Of clsExceptionListVM)

    Public Sub Load()
        Dim objLoad As LoadDataDelegate

        m_colExceptionVm = New ObservableCollection(Of clsExceptionListVM)
        DataContext = m_colExceptionVm

        objLoad = New LoadDataDelegate(AddressOf LoadData)
        objLoad.BeginInvoke(Nothing, objLoad)
    End Sub

    Private Delegate Sub LoadDataDelegate()
    Private Sub LoadData()
        Dim colException As List(Of clsException)
        Dim objException As clsException
        Dim colVm As List(Of clsExceptionListVM)
        Dim objSetData As SetDataDelegate
        Try
            colException = clsException.Get(New clsSettings, winMain.SessionId, Date.Now.AddMonths(-3))
            colVm = New List(Of clsExceptionListVM)
            If colException IsNot Nothing Then
                For Each objException In colException
                    colVm.Add(New clsExceptionListVM(objException))
                Next
            End If
            objSetData = New SetDataDelegate(AddressOf SetData)
            Dispatcher.Invoke(objSetData, colVm)
        Catch ex As Exception
            winException.BeginProcessException(ex, Dispatcher)
        End Try
    End Sub

    Private Delegate Sub SetDataDelegate(ByVal colVm As List(Of clsExceptionListVM))
    Private Sub SetData(ByVal colVm As List(Of clsExceptionListVM))
        Dim objException As clsExceptionListVM

        m_colExceptionVm.Clear()
        If colVm IsNot Nothing Then
            For Each objException In colVm
                m_colExceptionVm.Add(objException)
            Next
        End If
    End Sub
End Class
