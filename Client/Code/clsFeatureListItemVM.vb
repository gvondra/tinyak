Imports System.Collections.ObjectModel
Imports System.ComponentModel
Imports System.Runtime.CompilerServices
Public Class clsFeatureListItemVM
    Implements INotifyPropertyChanged

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
        m_blnIsExpanded = False
        m_strExpandText = "+"
        m_intContentVisibility = Visibility.Collapsed
    End Sub

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

    Private Sub OnPropertyChanged(<CallerMemberName> Optional ByVal strName As String = Nothing)
        RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(strName))
    End Sub

    Public Sub ShowAddWorkItem()
        m_objAddWorkItem.Visibility = Visibility.Visible
    End Sub

    Public Sub HideAddWorkItem()
        m_objAddWorkItem.Visibility = Visibility.Collapsed
    End Sub

    Public Delegate Sub LoadWorkItemsDeleage(ByVal objSessionId As Guid)
    Public Sub LoadWorkItems(ByVal objSessionId As Guid)
        Dim colWorkItem As List(Of clsWorkListItem)
        Dim objWorkItem As clsWorkListItem

        m_colWorkListItem.Clear()
        colWorkItem = clsWorkListItem.GetByFeatureId(New clsSettings, objSessionId, m_objInnerFeatureListItem.Id)
        If colWorkItem IsNot Nothing Then
            For Each objWorkItem In colWorkItem
                m_colWorkListItem.Add(New clsWorkListItemVM(objWorkItem))
            Next objworkitem
        End If
    End Sub
End Class
