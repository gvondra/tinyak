Imports System.IO
Imports tas = tinyak.API.Shared
Public Class clsException
    Private m_objInnerException As tas.clsException

    Private Sub New(ByVal objException As tas.clsException)
        m_objInnerException = objException
        If objException.InnerException IsNot Nothing Then
            InnerException = New clsException(objException.InnerException)
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

    Public ReadOnly Property Data As Dictionary(Of String, String)
        Get
            Return m_objInnerException.Data
        End Get
    End Property

    Public Property InnerException As clsException

    Public Shared Function [Get](ByVal objSettings As ISettings, ByVal objSession As Guid, ByVal datMinimumTimestamp As Date) As List(Of clsException)
        Dim objRequest As HttpWebRequest
        Dim objResponse As HttpWebResponse
        Dim objUri As Uri
        Dim objQuery As NameValueCollection
        Dim objSerializer As DataContractSerializer
        Dim arrInnerException() As tas.clsException
        Dim colResult As List(Of clsException)
        Dim i As Integer

        objQuery = New NameValueCollection
        Date.SpecifyKind(datMinimumTimestamp, DateTimeKind.Local)
        objQuery.Add("minTimestamp", datMinimumTimestamp.ToUniversalTime.ToString("yyyy-MM-ddTHH:mm:ss.fffZ"))
        objUri = GetUri(objSettings, "Exception", objQuery)
        objRequest = CreateGetRequest(objSession, objUri)

        objResponse = DirectCast(objRequest.GetResponse, HttpWebResponse)
        objSerializer = New DataContractSerializer(GetType(tas.clsException()))
        arrInnerException = CType(objSerializer.ReadObject(objResponse.GetResponseStream), tas.clsException())
        If arrInnerException IsNot Nothing Then
            colResult = New List(Of clsException)
            For i = 0 To arrInnerException.Length - 1
                If arrInnerException(i) IsNot Nothing Then
                    colResult.Add(New clsException(arrInnerException(i)))
                End If
            Next i
        Else
            colResult = Nothing
        End If
        Return colResult
    End Function
End Class
