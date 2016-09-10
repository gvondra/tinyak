Imports System.IO
Imports tas = tinyak.API.Shared
Public Class clsFeature
    Private m_objInnerFeature As tas.clsFeature

    Private Sub New(ByVal objFeature As tas.clsFeature)
        m_objInnerFeature = objFeature
    End Sub

    Public ReadOnly Property Id As Integer
        Get
            Return m_objInnerFeature.Id
        End Get
    End Property

    Public ReadOnly Property ProjectId As Integer
        Get
            Return m_objInnerFeature.ProjectId
        End Get
    End Property

    Public Property Title As String
        Get
            Return m_objInnerFeature.Title
        End Get
        Set(value As String)
            m_objInnerFeature.Title = value
        End Set
    End Property

    Public Shared Function [Get](ByVal objSettings As ISettings, ByVal objSession As Guid, ByVal intFeatureId As Integer) As clsFeature
        Dim objRequest As HttpWebRequest
        Dim objResponse As HttpWebResponse
        Dim objUri As Uri
        Dim objSerializer As DataContractSerializer
        Dim objInner As tas.clsFeature
        Dim objResult As clsFeature

        objUri = GetUri(objSettings, String.Format("Feature/{0}", intFeatureId))
        objRequest = CreateGetRequest(objSession, objUri)

        objResponse = DirectCast(objRequest.GetResponse, HttpWebResponse)
        objSerializer = New DataContractSerializer(GetType(tas.clsFeature))
        objInner = CType(objSerializer.ReadObject(objResponse.GetResponseStream), tas.clsFeature)
        If objInner IsNot Nothing Then
            objResult = New clsFeature(objInner)
        Else
            objResult = Nothing
        End If
        Return objResult
    End Function

    Public Sub Update(ByVal objSettings As ISettings, ByVal objSessionId As Guid)
        Dim objRequest As HttpWebRequest
        Dim objResponse As HttpWebResponse
        Dim objUri As Uri
        Dim objSerializer As DataContractSerializer
        Dim objInner As tas.clsFeature
        Dim objStream As Stream

        objUri = GetUri(objSettings, String.Format("Feature/{0}", Id))
        objRequest = CreatePutRequest(objSessionId, objUri)

        objSerializer = New DataContractSerializer(GetType(tas.clsFeature))
        objStream = objRequest.GetRequestStream
        objSerializer.WriteObject(objStream, m_objInnerFeature)
        objStream.Flush()
        objStream.Close()

        objResponse = DirectCast(objRequest.GetResponse, HttpWebResponse)
        objSerializer = New DataContractSerializer(GetType(tas.clsFeature))
        objInner = CType(objSerializer.ReadObject(objResponse.GetResponseStream), tas.clsFeature)
        If objInner IsNot Nothing Then
            Title = objInner.Title
        End If
    End Sub
End Class
