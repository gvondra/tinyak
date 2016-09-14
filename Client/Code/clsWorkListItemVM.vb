Imports System.Collections.ObjectModel
Imports System.ComponentModel
Imports System.Runtime.CompilerServices

Public Class clsWorkListItemVM
    Implements INotifyPropertyChanged
    Implements IWorkItemObserver

    Private m_objInnerWorkListItem As clsWorkListItem
    Private m_strItteration As String
    Private m_objBackground As Brush

    Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged

    Public Sub New(ByVal objWorkListItem As clsWorkListItem)
        m_objInnerWorkListItem = objWorkListItem
        If objWorkListItem.Itteration IsNot Nothing Then
            m_strItteration = objWorkListItem.Itteration.Name
        Else
            m_strItteration = String.Empty
        End If
        m_objBackground = Brushes.Black
    End Sub

    Public ReadOnly Property Id As Integer
        Get
            Return m_objInnerWorkListItem.Id.Value
        End Get
    End Property

    Public Property Title As String
        Get
            Return m_objInnerWorkListItem.Title
        End Get
        Set(value As String)
            m_objInnerWorkListItem.Title = value
            OnPropertyChanged()
        End Set
    End Property

    Public Property Effort As Nullable(Of Int16)
        Get
            Return m_objInnerWorkListItem.Effort
        End Get
        Set(value As Nullable(Of Int16))
            m_objInnerWorkListItem.Effort = value
            OnPropertyChanged()
        End Set
    End Property

    Public Property AssignedTo As String
        Get
            Return m_objInnerWorkListItem.AssignedTo
        End Get
        Set(value As String)
            m_objInnerWorkListItem.AssignedTo = value
            OnPropertyChanged()
        End Set
    End Property

    Public Property Itteration As String
        Get
            Return m_strItteration
        End Get
        Set(value As String)
            m_strItteration = value
            OnPropertyChanged()
        End Set
    End Property

    Public Property Background As Brush
        Get
            Return m_objBackground
        End Get
        Set(value As Brush)
            m_objBackground = value
            OnPropertyChanged()
        End Set
    End Property

    Public Property State As enumWorkItemState
        Get
            Return m_objInnerWorkListItem.State
        End Get
        Set(value As enumWorkItemState)
            m_objInnerWorkListItem.State = value
            OnPropertyChanged()
            OnPropertyChanged("StatusDescription")
        End Set
    End Property

    Public ReadOnly Property StatusDescription As String
        Get
            Select Case m_objInnerWorkListItem.State
                Case enumWorkItemState.Approved
                    Return "Approved"
                Case enumWorkItemState.Committed
                    Return "Committed"
                Case enumWorkItemState.Complete
                    Return "Complete"
                Case enumWorkItemState.New
                    Return "New"
                Case enumWorkItemState.Rejected
                    Return "Rejected"
                Case Else
                    Return "Unkown"
            End Select
        End Get
    End Property

    Private Sub OnPropertyChanged(<CallerMemberName> Optional ByVal strName As String = Nothing)
        RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(strName))
    End Sub

    Public Sub Selected()
        Background = Brushes.DarkViolet
    End Sub

    Public Sub Deselected()
        Background = Brushes.Black
    End Sub

    Public Sub OnSave(objWorkItem As clsWorkItemVM) Implements IWorkItemObserver.OnSave
        Me.AssignedTo = objWorkItem.AssignedTo
        Me.Effort = objWorkItem.Effort
        Me.State = objWorkItem.State
        Me.Title = objWorkItem.Title
        Me.Itteration = objWorkItem.ItterationName
    End Sub
End Class
