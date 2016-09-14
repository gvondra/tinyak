Imports System.Collections.ObjectModel
Imports System.ComponentModel
Imports System.Runtime.CompilerServices

Public Class clsFeatureListItemVM
    Implements INotifyPropertyChanged
    Implements IFeatureObserver

    Private m_objInnerFeatureListItem As clsFeatureListItem
    Private m_objAddWorkItem As clsAddWorkItemVM
    Private m_objProject As clsProject
    Private m_blnIsExpanded As Boolean
    Private m_strExpandText As String
    Private m_intContentVisibility As Visibility
    Private m_colWorkListItem As ObservableCollection(Of clsWorkListItemVM)

    Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged

    Public Sub New(ByVal objFeatureListItem As clsFeatureListItem)
        m_colWorkListItem = New ObservableCollection(Of clsWorkListItemVM)
        m_objAddWorkItem = New clsAddWorkItemVM(objFeatureListItem)
        m_objInnerFeatureListItem = objFeatureListItem
        m_blnIsExpanded = True
        m_strExpandText = "-"
        m_intContentVisibility = Visibility.Visible
    End Sub

    Public ReadOnly Property Id As Integer
        Get
            Return m_objInnerFeatureListItem.Id
        End Get
    End Property

    Public Property WorkListItems As ObservableCollection(Of clsWorkListItemVM)
        Get
            Return m_colWorkListItem
        End Get
        Set
            m_colWorkListItem = Value
            OnPropertyChanged()
        End Set
    End Property

    Public Property ContentVisibility As Visibility
        Get
            Return m_intContentVisibility
        End Get
        Set(value As Visibility)
            m_intContentVisibility = value
            OnPropertyChanged()
        End Set
    End Property

    Public Property Project As clsProject
        Get
            Return m_objProject
        End Get
        Set(value As clsProject)
            m_objProject = value
            m_objAddWorkItem.Project = value
        End Set
    End Property

    Public Property AddWorkItem As clsAddWorkItemVM
        Get
            Return m_objAddWorkItem
        End Get
        Set(value As clsAddWorkItemVM)
            m_objAddWorkItem = value
        End Set
    End Property

    Public Property Title As String
        Get
            Return m_objInnerFeatureListItem.Title
        End Get
        Set(value As String)
            m_objInnerFeatureListItem.Title = value
            OnPropertyChanged()
        End Set
    End Property

    Public Property ExpandText As String
        Get
            Return m_strExpandText
        End Get
        Set(value As String)
            m_strExpandText = value
            OnPropertyChanged()
        End Set
    End Property

    Public Sub ToggleIsExpanded()
        If m_blnIsExpanded Then
            ExpandText = "+"
            ContentVisibility = Visibility.Collapsed
        Else
            ExpandText = "-"
            ContentVisibility = Visibility.Visible
        End If
        m_blnIsExpanded = Not m_blnIsExpanded
    End Sub

    Public ReadOnly Property IsExpanded As Boolean
        Get
            Return m_blnIsExpanded
        End Get
    End Property

    Private Sub OnPropertyChanged(<CallerMemberName> Optional ByVal strName As String = Nothing)
        RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(strName))
    End Sub

    Public Sub ShowAddWorkItem()
        m_objAddWorkItem.Visibility = Visibility.Visible
    End Sub

    Public Sub HideAddWorkItem()
        m_objAddWorkItem.Visibility = Visibility.Collapsed
    End Sub

    Public Delegate Sub LoadWorkItemsDeleage(ByVal objSessionId As Guid, ByVal objDispatcher As System.Windows.Threading.Dispatcher, ByVal intItterationId As Nullable(Of Integer))
    Public Sub LoadWorkItems(ByVal objSessionId As Guid, ByVal objDispatcher As System.Windows.Threading.Dispatcher, ByVal intItterationId As Nullable(Of Integer))
        Dim colWorkItem As List(Of clsWorkListItem)
        Dim objLoad As LoadWorkItemsDelegate
        Dim i As Integer
        Try
            colWorkItem = clsWorkListItem.GetByFeatureId(New clsSettings, objSessionId, m_objInnerFeatureListItem.Id)
            If colWorkItem IsNot Nothing Then
                If intItterationId.HasValue Then
                    For i = colWorkItem.Count - 1 To 0 Step -1
                        If colWorkItem(i).Itteration Is Nothing OrElse colWorkItem(i).Itteration.Id.Value <> intItterationId.Value Then
                            colWorkItem.RemoveAt(i)
                        End If
                    Next
                End If
                objLoad = New LoadWorkItemsDelegate(AddressOf LoadWorkItems)
                objDispatcher.Invoke(objLoad, colWorkItem, objDispatcher)
            End If
        Catch ex As Exception
            winException.BeginProcessException(ex, objDispatcher)
        End Try
    End Sub

    Private Delegate Sub LoadWorkItemsDelegate(ByVal colWorkItem As List(Of clsWorkListItem), ByVal objDispatcher As System.Windows.Threading.Dispatcher)
    Private Sub LoadWorkItems(ByVal colWorkItem As List(Of clsWorkListItem), ByVal objDispatcher As System.Windows.Threading.Dispatcher)
        Dim objWorkItem As clsWorkListItem

        Try
            m_colWorkListItem.Clear()
            For Each objWorkItem In colWorkItem
                m_colWorkListItem.Add(New clsWorkListItemVM(objWorkItem))
            Next objWorkItem
        Catch ex As Exception
            winException.BeginProcessException(ex, objDispatcher)
        End Try
    End Sub

    Public Sub DeleselectAllWorkItems()
        Dim objWorkItem As clsWorkListItemVM
        For Each objWorkItem In m_colWorkListItem
            objWorkItem.Deselected()
        Next objWorkItem
    End Sub

    Public Sub OnSaveFeature(objFeature As clsFeatureVM) Implements IFeatureObserver.OnSaveFeature
        Title = objFeature.Title
    End Sub
End Class
