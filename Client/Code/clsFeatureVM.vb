Imports System.ComponentModel
Imports System.Runtime.CompilerServices
Public Class clsFeatureVM
    Implements INotifyPropertyChanged

    Private m_objInnerFeature As clsFeature

    Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged

    Public Sub New(ByVal objInnerFeature As clsFeature)
        m_objInnerFeature = objInnerFeature
    End Sub

    Public ReadOnly Property Id As Integer
        Get
            Return m_objInnerFeature.Id
        End Get
    End Property

    Public Property Title As String
        Get
            Return m_objInnerFeature.Title
        End Get
        Set(value As String)
            m_objInnerFeature.Title = value
            OnPropertyChanged()
        End Set
    End Property

    Private Sub OnPropertyChanged(<CallerMemberName> Optional ByVal strName As String = Nothing)
        RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(strName))
    End Sub
End Class
