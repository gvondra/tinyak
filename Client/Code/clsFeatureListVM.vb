Imports System.Collections.ObjectModel
Imports System.ComponentModel
Imports System.Runtime.CompilerServices
Public Class clsFeatureListVM
    Implements INotifyPropertyChanged

    Private m_objAddFeature As clsAddFeatureVM
    Private m_objProject As clsProject
    Private m_colFeatureListItem As ObservableCollection(Of clsFeatureListItemVM)

    Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged

    Public Sub New()
        m_objAddFeature = New clsAddFeatureVM
        m_colFeatureListItem = New ObservableCollection(Of clsFeatureListItemVM)
    End Sub

    Public Property Project As clsProject
        Get
            Return m_objProject
        End Get
        Set(value As clsProject)
            m_objProject = value
            m_objAddFeature.Project = value
        End Set
    End Property

    Public Property AddFeature As clsAddFeatureVM
        Get
            Return m_objAddFeature
        End Get
        Set(value As clsAddFeatureVM)
            m_objAddFeature = value
        End Set
    End Property

    Public Property FeatureListItems As ObservableCollection(Of clsFeatureListItemVM)
        Get
            Return m_colFeatureListItem
        End Get
        Set(value As ObservableCollection(Of clsFeatureListItemVM))
            m_colFeatureListItem = value
            OnPropertyChanged()
        End Set
    End Property

    Private Sub OnPropertyChanged(<CallerMemberName> Optional ByVal strName As String = Nothing)
        RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(strName))
    End Sub

    Public Sub ShowAddFeature()
        m_objAddFeature.Visibility = Visibility.Visible
    End Sub

    Public Sub HideAddFeature()
        m_objAddFeature.Visibility = Visibility.Collapsed
    End Sub

    Public Sub LoadBacklog(ByVal objSessionId As Guid)
        Dim colFeature As List(Of clsFeatureListItem)
        Dim objFeature As clsFeatureListItem
        Dim objVM As clsFeatureListItemVM
        Dim objDelegate As clsFeatureListItemVM.LoadWorkItemsDeleage

        m_colFeatureListItem.Clear()
        colFeature = clsFeatureListItem.GetByProjectId(New clsSettings, objSessionId, m_objProject.Id)
        If colFeature IsNot Nothing Then
            For Each objFeature In colFeature
                objVM = New clsFeatureListItemVM(objFeature) With {.Project = Project}
                m_colFeatureListItem.Add(objVM)

                objDelegate = New clsFeatureListItemVM.LoadWorkItemsDeleage(AddressOf objVM.LoadWorkItems)
                objDelegate.BeginInvoke(objSessionId, AddressOf LoadWorkItemsCallback, objDelegate)
            Next
        End If
    End Sub

    Private Sub LoadWorkItemsCallback(ByVal objResult As IAsyncResult)
        Try
            CType(objResult.AsyncState, clsFeatureListItemVM.LoadWorkItemsDeleage).EndInvoke(objResult)
        Catch ex As Exception

        End Try
    End Sub
End Class
