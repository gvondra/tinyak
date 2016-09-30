Imports System.Data
Public Class clsExceptionListVM
    Private m_objInnerException As clsException
    Private m_dtData As DataTable
    Private m_intDataVisibility As Visibility

    Public Sub New(ByVal objException As clsException)
        Dim objEnumerator As Dictionary(Of String, String).Enumerator

        m_objInnerException = objException
        If objException.InnerException IsNot Nothing Then
            InnerException = New clsExceptionListVM(objException.InnerException)
        End If

        m_dtData = New DataTable
        m_dtData.Columns.Add("Name", GetType(String))
        m_dtData.Columns.Add("Value", GetType(String))

        If objException.Data IsNot Nothing AndAlso objException.Data.Count > 0 Then
            m_intDataVisibility = Visibility.Visible

            objEnumerator = objException.Data.GetEnumerator
            While objEnumerator.MoveNext
                m_dtData.Rows.Add(objEnumerator.Current.Key, objEnumerator.Current.Value)
            End While
        Else
            m_intDataVisibility = Visibility.Collapsed
        End If
    End Sub

    Public ReadOnly Property TypeName As String
        Get
            Return m_objInnerException.TypeName
        End Get
    End Property

    Public ReadOnly Property Message As String
        Get
            Return m_objInnerException.Message
        End Get
    End Property

    Public ReadOnly Property Source As String
        Get
            Return m_objInnerException.Source
        End Get
    End Property

    Public ReadOnly Property Target As String
        Get
            Return m_objInnerException.Target
        End Get
    End Property

    Public ReadOnly Property StackTrace As String
        Get
            Return m_objInnerException.StackTrace
        End Get
    End Property

    Public ReadOnly Property HResult As Integer
        Get
            Return m_objInnerException.HResult
        End Get
    End Property

    Public ReadOnly Property Timestamp As Date
        Get
            Return m_objInnerException.Timestamp
        End Get
    End Property

    Public ReadOnly Property Data As DataView
        Get
            Return m_dtData.DefaultView
        End Get
    End Property

    Public ReadOnly Property DataVisibility As Visibility
        Get
            Return m_intDataVisibility
        End Get
    End Property

    Public Property InnerException As clsExceptionListVM
End Class
