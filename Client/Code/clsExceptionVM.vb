Imports System.Text
Public Class clsExceptionVM
    Private m_objInnerException As Exception
    Private m_sbText As StringBuilder

    Public Sub New(ByVal objException As Exception)
        m_objInnerException = objException
        m_sbText = New StringBuilder
        m_sbText.AppendLine(String.Format("Timestamp :   {0:yyyy-MM-dd HH:mm:ss}Z", Date.UtcNow))
        SetExceptionText(objException)
    End Sub

    Public ReadOnly Property Text As String
        Get
            Return m_sbText.ToString
        End Get
    End Property

    Private Sub SetExceptionText(ByVal objException As Exception)
        Dim objEnumerator As IDictionaryEnumerator

        m_sbText.AppendLine(String.Format("Message :     {0}", objException.Message))
        m_sbText.AppendLine(String.Format("HResult :     H{0:X4}", objException.HResult))
        m_sbText.AppendLine(String.Format("Source :      {0}", objException.Source))
        m_sbText.AppendLine(String.Format("Target Site : {0}", objException.TargetSite.ToString))
        m_sbText.AppendLine("Stack Trace")
        m_sbText.AppendLine(objException.StackTrace)

        If objException.Data IsNot Nothing AndAlso objException.Data.Count > 0 Then
            m_sbText.AppendLine()
            m_sbText.AppendLine("Data")
            objEnumerator = objException.Data.GetEnumerator()
            While objEnumerator.MoveNext
                m_sbText.AppendLine(String.Format("{0} =  {1}", objEnumerator.Key, objEnumerator.Value))
            End While
        End If

        If objException.InnerException IsNot Nothing Then
            m_sbText.AppendLine()
            m_sbText.AppendLine("Inner Exception")
            SetExceptionText(objException.InnerException)
        End If
    End Sub
End Class
