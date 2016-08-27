Imports System.Configuration
Public Class clsAppSettings
    Private Shared m_strBaseUrl As String

    Shared Sub New()
        m_strBaseUrl = ConfigurationManager.AppSettings("BaseUrl")
    End Sub

    Private Sub New()
    End Sub

    Public Shared ReadOnly Property BaseUrl As String
        Get
            Return m_strBaseUrl
        End Get
    End Property
End Class
