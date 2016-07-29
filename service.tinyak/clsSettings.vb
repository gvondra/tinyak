Imports tinyak.Core
Imports System.Configuration
Public Class clsSettings
    Implements ISettings

    Public ReadOnly Property ConnectionString As String Implements ISettings.ConnectionString
        Get
            Return ConfigurationManager.AppSettings("ConnectionString")
        End Get
    End Property
End Class
