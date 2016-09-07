Imports System.Collections.ObjectModel
Imports System.ComponentModel
Imports System.Runtime.CompilerServices
Public Class clsWorkListItemVM
    Implements INotifyPropertyChanged

    Private m_objInnerWorkListItem As clsWorkListItem

    Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged

    Public Sub New(ByVal objWorkListItem As clsWorkListItem)
        m_objInnerWorkListItem = objWorkListItem
    End Sub

    Public Property Title As String
        Get
            Return m_objInnerWorkListItem.Title
        End Get
        Set(value As String)
            m_objInnerWorkListItem.Title = value
            OnPropertyChanged()
        End Set
    End Property

    Private Sub OnPropertyChanged(<CallerMemberName> Optional ByVal strName As String = Nothing)
        RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(strName))
    End Sub
End Class
