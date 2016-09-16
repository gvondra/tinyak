Imports tinyak.Data
Public Class clsException
    Private m_objExceptionData As clsExceptionData
    Private m_objInnerException As clsException

    Private Sub New(ByVal objExceptionData As clsExceptionData)
        m_objExceptionData = objExceptionData
    End Sub

    Public ReadOnly Property Id As Nullable(Of Integer)
        Get
            Return m_objExceptionData.Id
        End Get
    End Property

    Public Property TypeName As String
        Get
            Return m_objExceptionData.TypeName
        End Get
        Set(value As String)
            m_objExceptionData.TypeName = value
        End Set
    End Property

    Public Property Message As String
        Get
            Return m_objExceptionData.Message
        End Get
        Set(value As String)
            m_objExceptionData.Message = value
        End Set
    End Property

    Public Property Source As String
        Get
            Return m_objExceptionData.Source
        End Get
        Set(value As String)
            m_objExceptionData.Source = value
        End Set
    End Property

    Public Property Target As String
        Get
            Return m_objExceptionData.Target
        End Get
        Set(value As String)
            m_objExceptionData.Target = value
        End Set
    End Property

    Public Property StackTrace As String
        Get
            Return m_objExceptionData.StackTrace
        End Get
        Set(value As String)
            m_objExceptionData.StackTrace = value
        End Set
    End Property

    Public Property HResult As Integer
        Get
            Return m_objExceptionData.HResult
        End Get
        Set(value As Integer)
            m_objExceptionData.HResult = value
        End Set
    End Property

    Public Property Timestamp As Date
        Get
            Return m_objExceptionData.Timestamp
        End Get
        Set(value As Date)
            m_objExceptionData.Timestamp = value
        End Set
    End Property

    Public Property Data As Dictionary(Of String, String)
        Get
            Return m_objExceptionData.Data
        End Get
        Set(value As Dictionary(Of String, String))
            m_objExceptionData.Data = value
        End Set
    End Property

    Public Property InnerException As clsException
        Get
            Return m_objInnerException
        End Get
        Set(value As clsException)
            m_objInnerException = value
        End Set
    End Property

    Public Shared Function GetNew(ByVal objException As Exception) As clsException
        Dim objResult As clsException
        Dim objEnumerator As IDictionaryEnumerator

        objResult = New clsException(New clsExceptionData)
        With objResult
            .HResult = objException.HResult
            .Message = objException.Message
            .Source = objException.Source
            .StackTrace = objException.StackTrace
            .Target = objException.TargetSite.ToString
            .TypeName = objException.GetType.FullName
            .Timestamp = Date.UtcNow
        End With
        If objException.Data IsNot Nothing AndAlso objException.Data.Count > 0 Then
            objResult.Data = New Dictionary(Of String, String)
            objEnumerator = objException.Data.GetEnumerator
            While objEnumerator.MoveNext
                objResult.Data.Add(objEnumerator.Key.ToString, objEnumerator.Value.ToString)
            End While
        End If
        If objException.InnerException IsNot Nothing Then
            objResult.InnerException = GetNew(objException.InnerException)
        End If
        Return objResult
    End Function

    Private Sub Update(ByVal objException As clsExceptionData)
        objException.InnerException = m_objExceptionData
    End Sub

    Public Sub Create(ByVal objSettings As ISettings)
        Dim objInnerSettings As clsSettings

        If InnerException IsNot Nothing Then
            InnerException.Update(m_objExceptionData)
        End If

        objInnerSettings = New clsSettings(objSettings)
        Try
            m_objExceptionData.Create(objInnerSettings)
            objInnerSettings.DatabaseTransaction.Commit()
            objInnerSettings.DatabaseConnection.Close()
        Catch
            objInnerSettings.DatabaseTransaction.Rollback()
            Throw
        Finally
            If objInnerSettings.DatabaseTransaction IsNot Nothing Then
                objInnerSettings.DatabaseTransaction.Dispose()
                objInnerSettings.DatabaseTransaction = Nothing
            End If
            If objInnerSettings.DatabaseConnection IsNot Nothing Then
                objInnerSettings.DatabaseConnection.Dispose()
                objInnerSettings.DatabaseConnection = Nothing
            End If
        End Try
    End Sub
End Class
