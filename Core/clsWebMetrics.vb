Imports tinyak.Data
Public Class clsWebMetrics
    Private m_objWebMetricsData As clsWebMetricsData

    Private Sub New(ByVal objWebMetricsData As clsWebMetricsData)
        m_objWebMetricsData = objWebMetricsData
    End Sub

    Public Property Url As String
        Get
            Return m_objWebMetricsData.Url
        End Get
        Set
            m_objWebMetricsData.Url = Value
        End Set
    End Property

    Public Property Controller As String
        Get
            Return m_objWebMetricsData.Controller
        End Get
        Set
            m_objWebMetricsData.Controller = Value
        End Set
    End Property

    Public Property Action As String
        Get
            Return m_objWebMetricsData.Action
        End Get
        Set
            m_objWebMetricsData.Action = Value
        End Set
    End Property

    Public Property Timestamp As Date
        Get
            Return m_objWebMetricsData.Timestamp
        End Get
        Set
            m_objWebMetricsData.Timestamp = Value
        End Set
    End Property

    Public Property Duration As Nullable(Of Double)
        Get
            Return m_objWebMetricsData.Duration
        End Get
        Set
            m_objWebMetricsData.Duration = Value
        End Set
    End Property

    Public Property ContentEncoding As String
        Get
            Return m_objWebMetricsData.ContentEncoding
        End Get
        Set
            m_objWebMetricsData.ContentEncoding = Value
        End Set
    End Property

    Public Property ContentLength As Nullable(Of Integer)
        Get
            Return m_objWebMetricsData.ContentLength
        End Get
        Set
            m_objWebMetricsData.ContentLength = Value
        End Set
    End Property

    Public Property ContentType As String
        Get
            Return m_objWebMetricsData.ContentType
        End Get
        Set
            m_objWebMetricsData.ContentType = Value
        End Set
    End Property

    Public Property Method As String
        Get
            Return m_objWebMetricsData.Method
        End Get
        Set
            m_objWebMetricsData.Method = Value
        End Set
    End Property

    Public Property RequestType As String
        Get
            Return m_objWebMetricsData.RequestType
        End Get
        Set
            m_objWebMetricsData.RequestType = Value
        End Set
    End Property

    Public Property TotalBytes As Nullable(Of Integer)
        Get
            Return m_objWebMetricsData.TotalBytes
        End Get
        Set
            m_objWebMetricsData.TotalBytes = Value
        End Set
    End Property

    Public Property UrlReferrer As String
        Get
            Return m_objWebMetricsData.UrlReferrer
        End Get
        Set
            m_objWebMetricsData.UrlReferrer = Value
        End Set
    End Property

    Public Property UserAgent As String
        Get
            Return m_objWebMetricsData.UserAgent
        End Get
        Set
            m_objWebMetricsData.UserAgent = Value
        End Set
    End Property

    Public Property Parameters As String
        Get
            Return m_objWebMetricsData.Parameters
        End Get
        Set
            m_objWebMetricsData.Parameters = Value
        End Set
    End Property

    Public Shared Function GetNew() As clsWebMetrics
        Return New clsWebMetrics(New clsWebMetricsData) With {.Timestamp = Date.UtcNow}
    End Function

    Public Sub Create(ByVal objSettings As ISettings)
        Dim objInnerSettings As clsSettings

        objInnerSettings = New clsSettings(objSettings)
        Try
            m_objWebMetricsData.Create(objInnerSettings)
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

    Public Shared Function GetByMinimumTimestamp(ByVal objSettings As ISettings, ByVal datMinimumTimestamp As Date) As List(Of clsWebMetrics)
        Dim colData As List(Of clsWebMetricsData)
        Dim objData As clsWebMetricsData
        Dim colResult As List(Of clsWebMetrics)

        colData = clsWebMetricsData.GetByMinimumTimestamp(New clsSettings(objSettings), datMinimumTimestamp)
        colResult = New List(Of clsWebMetrics)
        If colData IsNot Nothing Then
            For Each objData In colData
                colResult.Add(New clsWebMetrics(objData))
            Next
        End If
        Return colResult
    End Function
End Class
