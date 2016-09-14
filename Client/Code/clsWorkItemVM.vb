Imports System.ComponentModel
Imports System.Runtime.CompilerServices
Public Class clsWorkItemVM
    Implements INotifyPropertyChanged

    Private m_objInnerWorkItem As clsWorkItem
    Private m_strTitleMessage As String
    Private m_intTitleMessageVisibility As Visibility
    Private m_strEffortMessage As String
    Private m_intEffortMessageVisibility As Visibility
    Private m_colObserver As List(Of IWorkItemObserver)
    Private m_colItteration As List(Of clsItteration)
    Private m_objItteration As clsItteration

    Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged

    Public Sub New(ByVal objWorkItem As clsWorkItem)
        m_objInnerWorkItem = objWorkItem
        m_objItteration = objWorkItem.Itteration
        m_colObserver = New List(Of IWorkItemObserver)
        m_intTitleMessageVisibility = Visibility.Collapsed
        m_intEffortMessageVisibility = Visibility.Collapsed
    End Sub

    Public ReadOnly Property Id As Nullable(Of Integer)
        Get
            Return m_objInnerWorkItem.Id
        End Get
    End Property

    Public Property Title As String
        Get
            Return m_objInnerWorkItem.Title
        End Get
        Set(value As String)
            m_objInnerWorkItem.Title = value.Trim
            OnPropertyChanged()
        End Set
    End Property

    Public Property TitleMessage As String
        Get
            Return m_strTitleMessage
        End Get
        Set(value As String)
            m_strTitleMessage = value
            OnPropertyChanged()
        End Set
    End Property

    Public Property TitleMessageVisibility As Visibility
        Get
            Return m_intTitleMessageVisibility
        End Get
        Set(value As Visibility)
            m_intTitleMessageVisibility = value
            OnPropertyChanged()
        End Set
    End Property

    Public Property State As enumWorkItemState
        Get
            Return m_objInnerWorkItem.State
        End Get
        Set(value As enumWorkItemState)
            m_objInnerWorkItem.State = value
            OnPropertyChanged()
        End Set
    End Property

    Public Property StateValue As Int16
        Get
            Return CType(State, Int16)
        End Get
        Set(value As Int16)
            State = CType(value, tinyak.Interface.enumWorkItemState)
            OnPropertyChanged()
        End Set
    End Property

    Public Property AssignedTo As String
        Get
            Return m_objInnerWorkItem.AssignedTo
        End Get
        Set
            m_objInnerWorkItem.AssignedTo = Value.Trim
            OnPropertyChanged()
        End Set
    End Property

    Public Property Effort As Nullable(Of Int16)
        Get
            Return m_objInnerWorkItem.Effort
        End Get
        Set(value As Nullable(Of Int16))
            m_objInnerWorkItem.Effort = value
            OnPropertyChanged()
        End Set
    End Property

    Public Property EffortMessage As String
        Get
            Return m_strEffortMessage
        End Get
        Set(value As String)
            m_strEffortMessage = value
            OnPropertyChanged()
        End Set
    End Property

    Public Property EffortMessageVisibility As Visibility
        Get
            Return m_intEffortMessageVisibility
        End Get
        Set(value As Visibility)
            m_intEffortMessageVisibility = value
            OnPropertyChanged()
        End Set
    End Property

    Public Property Description As String
        Get
            Return m_objInnerWorkItem.Description
        End Get
        Set(value As String)
            m_objInnerWorkItem.Description = value
            OnPropertyChanged()
        End Set
    End Property

    Public Property AcceptanceCriteria As String
        Get
            Return m_objInnerWorkItem.AcceptanceCriteria
        End Get
        Set(value As String)
            m_objInnerWorkItem.AcceptanceCriteria = value
            OnPropertyChanged()
        End Set
    End Property

    Public ReadOnly Property ItterationName As String
        Get
            If m_objInnerWorkItem.Itteration IsNot Nothing Then
                Return m_objInnerWorkItem.Itteration.Name
            Else
                Return String.Empty
            End If
        End Get
    End Property

    Public ReadOnly Property Itterations As List(Of clsItteration)
        Get
            Return m_colItteration
        End Get
    End Property

    Public Property Itteration As clsItteration
        Get
            Return m_objItteration
        End Get
        Set(value As clsItteration)
            m_objItteration = value
            OnPropertyChanged()
        End Set
    End Property

    Public ReadOnly Property StateItems As List(Of clsComboBoxItem)
        Get
            Dim arrState As Array
            Dim i As Integer
            Dim colResult As List(Of clsComboBoxItem)
            Dim strText As String

            arrState = [Enum].GetValues(GetType(tinyak.Interface.enumWorkItemState))
            colResult = New List(Of clsComboBoxItem)
            For i = 0 To arrState.Length - 1
                Select Case CType(arrState.GetValue(i), tinyak.Interface.enumWorkItemState)
                    Case enumWorkItemState.Approved
                        strText = "Approved"
                    Case enumWorkItemState.Committed
                        strText = "Committed"
                    Case enumWorkItemState.Complete
                        strText = "Complete"
                    Case enumWorkItemState.New
                        strText = "New"
                    Case enumWorkItemState.Rejected
                        strText = "Rejected"
                    Case Else
                        strText = "Unkown"
                End Select
                colResult.Add(New clsComboBoxItem With {.Value = CType(arrState.GetValue(i), Int16), .Text = strText})
            Next
            Return colResult
        End Get
    End Property

    Private Sub OnPropertyChanged(<CallerMemberName> Optional ByVal strName As String = Nothing)
        RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(strName))
    End Sub

    Public Sub Update(ByVal objSettings As clsSettings, ByVal objSessionId As Guid)
        Dim i As Integer

        If m_objItteration Is Nothing OrElse m_objItteration.Id.HasValue = False Then
            m_objInnerWorkItem.Itteration = Nothing
        Else
            m_objInnerWorkItem.Itteration = m_objItteration
        End If

        m_objInnerWorkItem.Update(objSettings, objSessionId)

        For i = 0 To m_colObserver.Count - 1
            m_colObserver(i).OnSave(Me)
        Next
    End Sub

    Public Sub RegisterObserver(ByVal objObserver As IWorkItemObserver)
        If m_colObserver.Contains(objObserver) = False Then
            m_colObserver.Add(objObserver)
        End If
    End Sub

    Public Sub LoadItterations(ByVal intProjectId As Integer)
        Dim colItteration As List(Of clsItteration)
        Dim i As Integer

        colItteration = clsItteration.GetByProjectId(New clsSettings, winMain.SessionId, intProjectId)
        If colItteration Is Nothing Then
            colItteration = New List(Of clsItteration)
        ElseIf m_objItteration IsNot Nothing Then
            For i = 0 To colItteration.Count - 1
                If m_objItteration.Id.Value = colItteration(i).Id.Value Then
                    colItteration(i) = m_objItteration
                End If
            Next i
        End If
        colItteration.Insert(0, clsItteration.GetNew)
        m_colItteration = colItteration
    End Sub
End Class
