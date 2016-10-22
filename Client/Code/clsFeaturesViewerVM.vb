Imports System.ComponentModel
Imports System.Collections.ObjectModel
Imports System.Runtime.CompilerServices
Public Class clsFeaturesViewerVM
    Implements INotifyPropertyChanged

    Private m_objProject As clsProject
    Private m_objFeatureListVM As clsFeatureListVM
    Private m_colItteration As ObservableCollection(Of clsItterationVM)
    Private m_intSelectedItterationId As Nullable(Of Integer)
    Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged

    Public Sub New()
        m_objFeatureListVM = New clsFeatureListVM
        m_colItteration = New ObservableCollection(Of clsItterationVM)
    End Sub

    Public Property Project As clsProject
        Get
            Return m_objProject
        End Get
        Set(value As clsProject)
            m_objProject = value
            m_objFeatureListVM.Project = value
        End Set
    End Property

    Public Property SelectedItterationId As Nullable(Of Integer)
        Get
            Return m_intSelectedItterationId
        End Get
        Set(value As Nullable(Of Integer))
            m_intSelectedItterationId = value
            m_objFeatureListVM.SelectedItterationId = value
        End Set
    End Property

    Public Property FeatureList As clsFeatureListVM
        Get
            Return m_objFeatureListVM
        End Get
        Set(value As clsFeatureListVM)
            m_objFeatureListVM = value
        End Set
    End Property

    Public Property Itterations As ObservableCollection(Of clsItterationVM)
        Get
            Return m_colItteration
        End Get
        Set(value As ObservableCollection(Of clsItterationVM))
            m_colItteration = value
            OnPropertyChanged()
        End Set
    End Property

    Public Sub ShowAddFeature()
        m_objFeatureListVM.ShowAddFeature()
    End Sub

    Private Sub OnPropertyChanged(<CallerMemberName> Optional ByVal strName As String = Nothing)
        RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(strName))
    End Sub
End Class
