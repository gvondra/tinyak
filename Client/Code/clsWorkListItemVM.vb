﻿Imports System.Collections.ObjectModel
Imports System.ComponentModel
Imports System.Runtime.CompilerServices
Public Class clsWorkListItemVM
    Implements INotifyPropertyChanged

    Private m_objInnerWorkListItem As clsWorkListItem
    Private m_objBackground As Brush

    Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged

    Public Sub New(ByVal objWorkListItem As clsWorkListItem)
        m_objInnerWorkListItem = objWorkListItem
        m_objBackground = Brushes.Black
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

    Public Property Effort As Nullable(Of Int16)
        Get
            Return m_objInnerWorkListItem.Effort
        End Get
        Set(value As Nullable(Of Int16))
            m_objInnerWorkListItem.Effort = value
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
End Class
