Imports System.IO
Imports tas = tinyak.API.Shared
Public Class clsWebMetrics
    Private m_objInnerWebMetrics As tas.clsWebMetrics

    Private Sub New(ByVal objWebMetrics As tas.clsWebMetrics)
        m_objInnerWebMetrics = objWebMetrics
    End Sub

    Public Property Url As String
        Get
            Return m_objInnerWebMetrics.Url
        End Get
        Set
            m_objInnerWebMetrics.Url = Value
        End Set
    End Property

    Public Property Controller As String
        Get
            Return m_objInnerWebMetrics.Controller
        End Get
        Set
            m_objInnerWebMetrics.Controller = Value
        End Set
    End Property

    Public Property Action As String
        Get
            Return m_objInnerWebMetrics.Action
        End Get
        Set
            m_objInnerWebMetrics.Action = Value
        End Set
    End Property

    Public Property Timestamp As Date
        Get
            Return m_objInnerWebMetrics.Timestamp
        End Get
        Set
            m_objInnerWebMetrics.Timestamp = Value
        End Set
    End Property

    Public Property Duration As Nullable(Of Double)
        Get
            Return m_objInnerWebMetrics.Duration
        End Get
        Set
            m_objInnerWebMetrics.Duration = Value
        End Set
    End Property

    Public Property ContentEncoding As String
        Get
            Return m_objInnerWebMetrics.ContentEncoding
        End Get
        Set
            m_objInnerWebMetrics.ContentEncoding = Value
        End Set
    End Property

    Public Property ContentLength As Nullable(Of Integer)
        Get
            Return m_objInnerWebMetrics.ContentLength
        End Get
        Set
            m_objInnerWebMetrics.ContentLength = Value
        End Set
    End Property

    Public Property ContentType As String
        Get
            Return m_objInnerWebMetrics.ContentType
        End Get
        Set
            m_objInnerWebMetrics.ContentType = Value
        End Set
    End Property

    Public Property Method As String
        Get
            Return m_objInnerWebMetrics.Method
        End Get
        Set
            m_objInnerWebMetrics.Method = Value
        End Set
    End Property

    Public Property RequestType As String
        Get
            Return m_objInnerWebMetrics.RequestType
        End Get
        Set
            m_objInnerWebMetrics.RequestType = Value
        End Set
    End Property

    Public Property TotalBytes As Nullable(Of Integer)
        Get
            Return m_objInnerWebMetrics.TotalBytes
        End Get
        Set
            m_objInnerWebMetrics.TotalBytes = Value
        End Set
    End Property

    Public Property UrlReferrer As String
        Get
            Return m_objInnerWebMetrics.UrlReferrer
        End Get
        Set
            m_objInnerWebMetrics.UrlReferrer = Value
        End Set
    End Property

    Public Property UserAgent As String
        Get
            Return m_objInnerWebMetrics.UserAgent
        End Get
        Set
            m_objInnerWebMetrics.UserAgent = Value
        End Set
    End Property

    Public Property Parameters As String
        Get
            Return m_objInnerWebMetrics.Parameters
        End Get
        Set
            m_objInnerWebMetrics.Parameters = Value
        End Set
    End Property

    Public Property StatusCode As Nullable(Of Integer)
        Get
            Return m_objInnerWebMetrics.StatusCode
        End Get
        Set(value As Nullable(Of Integer))
            m_objInnerWebMetrics.StatusCode = value
        End Set
    End Property

    Public Property StatusDescription As String
        Get
            Return m_objInnerWebMetrics.StatusDescription
        End Get
        Set(value As String)
            m_objInnerWebMetrics.StatusDescription = value
        End Set
    End Property

    Public Shared Function [Get](ByVal objSettings As ISettings, ByVal objSession As Guid, ByVal datMinimumTimestamp As Date) As List(Of clsWebMetrics)
        Dim objRequest As HttpWebRequest
        Dim objResponse As HttpWebResponse
        Dim objUri As Uri
        Dim objQuery As NameValueCollection
        Dim objSerializer As DataContractSerializer
        Dim arrInnerWebMetrics() As tas.clsWebMetrics
        Dim colResult As List(Of clsWebMetrics)
        Dim i As Integer

        objQuery = New NameValueCollection
        Date.SpecifyKind(datMinimumTimestamp, DateTimeKind.Local)
        objQuery.Add("minTimestamp", datMinimumTimestamp.ToUniversalTime.ToString("yyyy-MM-ddTHH:mm:ss.fffZ"))
        objUri = GetUri(objSettings, "WebMetrics", objQuery)
        objRequest = CreateGetRequest(objSession, objUri)

        objResponse = DirectCast(objRequest.GetResponse, HttpWebResponse)
        objSerializer = New DataContractSerializer(GetType(tas.clsWebMetrics()))
        arrInnerWebMetrics = CType(objSerializer.ReadObject(objResponse.GetResponseStream), tas.clsWebMetrics())
        If arrInnerWebMetrics IsNot Nothing Then
            colResult = New List(Of clsWebMetrics)
            For i = 0 To arrInnerWebMetrics.Length - 1
                colResult.Add(New clsWebMetrics(arrInnerWebMetrics(i)))
            Next i
        Else
            colResult = Nothing
        End If
        Return colResult
    End Function

End Class
