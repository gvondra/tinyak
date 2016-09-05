Imports System.ComponentModel
Imports System.Runtime.CompilerServices
Public Class clsAddFeatureVM
    Implements INotifyPropertyChanged

    Private m_strTitle As String
    Private m_intVisibility As Visibility

    Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged

    Public Property Project As clsProject

    Public Property Title As String
        Get
            Return m_strTitle
        End Get
        Set(value As String)
            m_strTitle = value
            OnPropertyChanged()
        End Set
    End Property

    Public Property Visibility As Visibility
        Get
            Return m_intVisibility
        End Get
        Set(value As Visibility)
            m_intVisibility = value
            OnPropertyChanged()
        End Set
    End Property

    Private Sub OnPropertyChanged(<CallerMemberName> Optional ByVal strName As String = Nothing)
        RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(strName))
    End Sub
End Class
