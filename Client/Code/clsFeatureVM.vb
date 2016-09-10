Imports System.ComponentModel
Imports System.Runtime.CompilerServices
Public Class clsFeatureVM
    Implements INotifyPropertyChanged

    Private m_objInnerFeature As clsFeature
    Private m_strTitleMessage As String
    Private m_intTitleMessageVisibility As Visibility

    Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged

    Public Sub New(ByVal objInnerFeature As clsFeature)
        m_objInnerFeature = objInnerFeature
        m_strTitleMessage = String.Empty
        m_intTitleMessageVisibility = Visibility.Collapsed
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

    Public Property TitleMessage As String
        Get
            Return m_strTitleMessage
        End Get
        Set(value As String)
            m_strTitleMessage = value
            OnPropertyChanged()
        End Set
    End Property

    Public Property TitleMessageVisiblity As Visibility
        Get
            Return m_intTitleMessageVisibility
        End Get
        Set(value As Visibility)
            m_intTitleMessageVisibility = value
            OnPropertyChanged()
        End Set
    End Property

    Private Sub OnPropertyChanged(<CallerMemberName> Optional ByVal strName As String = Nothing)
        RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(strName))
    End Sub

    Public Sub Save(ByVal objSessionId As Guid)
        m_objInnerFeature.Update(New clsSettings, objSessionId)
        OnPropertyChanged("Title")
    End Sub
End Class
