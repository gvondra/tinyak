Imports tinyak.Interface.tinyak
Imports System.Configuration
Public Class clsSettings
    Implements ISettings

    Public ReadOnly Property BaseAddress As String Implements ISettings.BaseAddress
        Get
            Return ConfigurationManager.AppSettings("BaseAddress")
        End Get
    End Property
End Class
