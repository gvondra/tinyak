Imports System.ComponentModel
Imports System.Data
Imports System.Runtime.CompilerServices
Public Class clsWebMetricsVM
    Implements INotifyPropertyChanged

    Private m_dtData As DataTable

    Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged

    Public Property Data As DataTable
        Get
            Return m_dtData
        End Get
        Set(value As DataTable)
            m_dtData = value
            OnPropertyChanged()
            OnPropertyChanged("WebMetrics")
        End Set
    End Property

    Public ReadOnly Property WebMetrics As DataView
        Get
            If m_dtData IsNot Nothing Then
                Return m_dtData.DefaultView
            Else
                Return Nothing
            End If
        End Get
    End Property

    Private Sub OnPropertyChanged(<CallerMemberName> Optional ByVal strName As String = Nothing)
        RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(strName))
    End Sub
End Class
