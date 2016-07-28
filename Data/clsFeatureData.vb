﻿Public Class clsFeatureDAta

    Friend Const TABLE_NAME As String = "Feature"

    Private m_intId As Nullable(Of Integer)
    Private m_strTitle As String

    Public Property Id As Nullable(Of Integer)
        Get
            Return m_intId
        End Get
        Set
            m_intId = Value
        End Set
    End Property

    Public Property Title As String
        Get
            Return m_strTitle
        End Get
        Set
            m_strTitle = Value
        End Set
    End Property

End Class
