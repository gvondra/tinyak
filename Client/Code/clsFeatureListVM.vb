Imports System.Collections.ObjectModel
Imports System.ComponentModel
Imports System.Runtime.CompilerServices
Public Class clsFeatureListVM
    Implements INotifyPropertyChanged

    Private m_objAddFeature As clsAddFeatureVM
    Private m_objProject As clsProject
    Private m_colFeatureListItem As ObservableCollection(Of clsFeatureListItemVM)
    Private m_intSelectedItterationId As Nullable(Of Integer)
    Private m_intItterationDateVisibility As Visibility
    Private m_datItterationStartDate As Date?
    Private m_datItterationEndDate As Date?

    Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged

    Public Sub New()
        m_objAddFeature = New clsAddFeatureVM
        m_colFeatureListItem = New ObservableCollection(Of clsFeatureListItemVM)
        m_intItterationDateVisibility = Visibility.Collapsed
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

    Public Property SelectedItterationId As Nullable(Of Integer)
        Get
            Return m_intSelectedItterationId
        End Get
        Set(value As Nullable(Of Integer))
            m_intSelectedItterationId = value
        End Set
    End Property

    Public Property ItterationDateVisibility As Visibility
        Get
            Return m_intItterationDateVisibility
        End Get
        Set(value As Visibility)
            m_intItterationDateVisibility = value
            OnPropertyChanged()
        End Set
    End Property

    Public Property ItterationStartDate As Date?
        Get
            Return m_datItterationStartDate
        End Get
        Set(value As Date?)
            m_datItterationStartDate = value
            OnPropertyChanged()
        End Set
    End Property

    Public Property ItterationEndDate As Date?
        Get
            Return m_datItterationEndDate
        End Get
        Set(value As Date?)
            m_datItterationEndDate = value
            OnPropertyChanged()
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

    Public Overloads Sub LoadBacklog(ByVal objSessionId As Guid, ByVal objDispatcher As System.Windows.Threading.Dispatcher)
        LoadBacklog(objSessionId, objDispatcher, Nothing)
    End Sub

    Public Overloads Sub LoadBacklog(ByVal objSessionId As Guid, ByVal objDispatcher As System.Windows.Threading.Dispatcher, ByVal intItterationId As Nullable(Of Integer))
        Dim objGet As GetByProjectIdDelegate

        m_colFeatureListItem.Clear()
        objGet = New GetByProjectIdDelegate(AddressOf GetByProjectId)
        objGet.BeginInvoke(objSessionId, objDispatcher, intItterationId, Nothing, objGet)
    End Sub

    Private Delegate Sub GetByProjectIdDelegate(ByVal objSessionId As Guid, ByVal objDispatcher As System.Windows.Threading.Dispatcher, ByVal intItterationId As Nullable(Of Integer))
    Private Sub GetByProjectId(ByVal objSessionId As Guid, ByVal objDispatcher As System.Windows.Threading.Dispatcher, ByVal intItterationId As Nullable(Of Integer))
        Dim colFeature As List(Of clsFeatureListItem)
        Dim objLoad As LoadFeaturesDelegate

        Try
            colFeature = clsFeatureListItem.GetByProjectId(New clsSettings, objSessionId, m_objProject.Id)
            If colFeature IsNot Nothing Then
                objLoad = New LoadFeaturesDelegate(AddressOf LoadFeatures)
                objDispatcher.Invoke(objLoad, colFeature, objSessionId, objDispatcher, intItterationId)
            End If
        Catch ex As Exception
            winException.BeginProcessException(ex, objDispatcher)
        End Try
    End Sub

    Private Delegate Sub LoadFeaturesDelegate(ByVal colFeature As List(Of clsFeatureListItem), ByVal objSessionId As Guid, ByVal objDispatcher As System.Windows.Threading.Dispatcher, ByVal intItterationId As Nullable(Of Integer))
    Private Sub LoadFeatures(ByVal colFeature As List(Of clsFeatureListItem), ByVal objSessionId As Guid, ByVal objDispatcher As System.Windows.Threading.Dispatcher, ByVal intItterationId As Nullable(Of Integer))
        Dim objFeature As clsFeatureListItem
        Dim objVM As clsFeatureListItemVM
        Dim objDelegate As clsFeatureListItemVM.LoadWorkItemsDeleage

        For Each objFeature In colFeature
            objVM = New clsFeatureListItemVM(objFeature) With {.Project = Project, .SelectedItterationId = intItterationId}
            m_colFeatureListItem.Add(objVM)

            objDelegate = New clsFeatureListItemVM.LoadWorkItemsDeleage(AddressOf objVM.LoadWorkItems)
            objDelegate.BeginInvoke(objSessionId, objDispatcher, intItterationId, Nothing, objDelegate)
        Next
    End Sub

    Public Sub DeselectAllWorkItems()
        Dim featureListItem As clsFeatureListItemVM
        If m_colFeatureListItem IsNot Nothing Then
            For Each featureListItem In m_colFeatureListItem
                featureListItem.DeleselectAllWorkItems()
            Next featureListItem
        End If
    End Sub
End Class
