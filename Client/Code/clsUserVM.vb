Imports System.ComponentModel
Imports System.Runtime.CompilerServices
Public Class clsUserVM
    Implements INotifyPropertyChanged

    Private m_objInnerUser As clsUser

    Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged

    Public Sub New(ByVal objUser As clsUser)
        m_objInnerUser = objUser
    End Sub

    Public ReadOnly Property WelcomeMessage As String
        Get
            Return "Welcome " & m_objInnerUser.Name
        End Get
    End Property

    Private Sub OnPropertyChange(<CallerMemberName> ByVal strName As String)
        RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(strName))
    End Sub
End Class
