Imports System.ComponentModel
Imports System.Runtime.CompilerServices
Public Class clsItterationVM
    Implements INotifyPropertyChanged

    Private m_objInnerItteration As clsItteration
    Private m_intFontWeight As FontWeight

    Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged

    Public Sub New(ByVal objItteration As clsItteration)
        m_objInnerItteration = objItteration

        m_intFontWeight = FontWeights.Normal
        If objItteration.StartDate.HasValue AndAlso objItteration.EndDate.HasValue Then
            If objItteration.StartDate.Value <= Date.Today AndAlso Date.Today <= objItteration.EndDate.Value Then
                m_intFontWeight = FontWeights.Bold
            End If
        ElseIf objItteration.StartDate.HasValue Then
            If objItteration.StartDate.Value <= Date.Today Then
                m_intFontWeight = FontWeights.Bold
            End If
        ElseIf objItteration.EndDate.HasValue Then
            If Date.Today <= objItteration.EndDate.Value Then
                m_intFontWeight = FontWeights.Bold
            End If
        End If

    End Sub

    Public ReadOnly Property Id As Integer
        Get
            Return m_objInnerItteration.Id
        End Get
    End Property

    Public Property Name As String
        Get
            Return m_objInnerItteration.Name
        End Get
        Set(value As String)
            m_objInnerItteration.Name = value
            OnPropertyChanged()
        End Set
    End Property

    Public Property FontWeight As FontWeight
        Get
            Return m_intFontWeight
        End Get
        Set(value As FontWeight)
            m_intFontWeight = value
            OnPropertyChanged()
        End Set
    End Property

    Public Property StartDate As Nullable(Of Date)
        Get
            Return m_objInnerItteration.StartDate
        End Get
        Set(value As Nullable(Of Date))
            m_objInnerItteration.StartDate = value
            OnPropertyChanged()
        End Set
    End Property

    Public Property EndDate As Nullable(Of Date)
        Get
            Return m_objInnerItteration.EndDate
        End Get
        Set(value As Nullable(Of Date))
            m_objInnerItteration.EndDate = value
            OnPropertyChanged()
        End Set
    End Property

    Public Property IsActive As Boolean
        Get
            Return m_objInnerItteration.IsActive
        End Get
        Set(value As Boolean)
            m_objInnerItteration.IsActive = value
            OnPropertyChanged()
        End Set
    End Property

    Private Sub OnPropertyChanged(<CallerMemberName> Optional ByVal strName As String = Nothing)
        RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(strName))
    End Sub
End Class
