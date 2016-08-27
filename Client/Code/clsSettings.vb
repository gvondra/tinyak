Public Class clsSettings
    Implements ISettings

    Public ReadOnly Property BaseUrl As String Implements ISettings.BaseUrl
        Get
            Return clsAppSettings.BaseUrl
        End Get
    End Property
End Class
