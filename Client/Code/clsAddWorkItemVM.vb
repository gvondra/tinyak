Imports System.ComponentModel
Imports System.Runtime.CompilerServices
Public Class clsAddWorkItemVM
    Implements INotifyPropertyChanged

    Private m_strTitle As String
    Private m_intVisibility As Visibility
    Private m_objFeature As clsFeatureListItem

    Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged

    Public Sub New(ByVal objFeatureListItem As clsFeatureListItem)
        m_objFeature = objFeatureListItem
    End Sub

    Public Property Project As clsProject

    Public Property SelectedItterationId As Nullable(Of Integer)

    Public Property Title As String
        Get
            Return m_strTitle
        End Get
        Set(value As String)
            m_strTitle = value
            OnPropertyChanged()
        End Set
    End Property

    Public Property Feature As clsFeatureListItem
        Get
            Return m_objFeature
        End Get
        Set(value As clsFeatureListItem)
            m_objFeature = value
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
