Imports System.IO
Imports tas = tinyak.API.Shared
Public Class clsFeatureListItem
    Implements IComparable(Of clsFeatureListItem)

    Private m_objInnerFeatureListItem As tas.clsFeatureListItem
    Private Sub New(ByVal objFeatureListItem As tas.clsFeatureListItem)
        m_objInnerFeatureListItem = objFeatureListItem
    End Sub

    Public Property Id As Integer
        Get
            Return m_objInnerFeatureListItem.Id
        End Get
        Set(value As Integer)
            m_objInnerFeatureListItem.Id = value
        End Set
    End Property

    Public Property Title As String
        Get
            Return m_objInnerFeatureListItem.Title
        End Get
        Set(value As String)
            m_objInnerFeatureListItem.Title = value
        End Set
    End Property

    Public Shared Function Create(ByVal objSettings As ISettings, ByVal objSession As Guid, ByVal intProjectId As Integer, ByVal strTitle As String) As clsFeatureListItem
        Dim objRequest As HttpWebRequest
        Dim objResponse As HttpWebResponse
        Dim objUri As Uri
        Dim objQuery As NameValueCollection
        Dim objSerializer As DataContractSerializer
        Dim objCreateResponse As tas.clsCreateFeatureResponse
        Dim objCreateRequest As tas.clsCreateFeatureRequest
        Dim objStream As Stream

        objQuery = New NameValueCollection
        objQuery.Add("projectId", intProjectId.ToString)
        objUri = GetUri(objSettings, "Feature", objQuery)
        objRequest = CreatePostRequest(objSession, objUri)

        objCreateRequest = New tas.clsCreateFeatureRequest
        objCreateRequest.Title = strTitle
        objSerializer = New DataContractSerializer(GetType(tas.clsCreateFeatureRequest))
        objStream = objRequest.GetRequestStream
        objSerializer.WriteObject(objStream, objCreateRequest)
        objStream.Flush()
        objStream.Close()

        objResponse = DirectCast(objRequest.GetResponse, HttpWebResponse)
        objSerializer = New DataContractSerializer(GetType(tas.clsCreateFeatureResponse))
        objCreateResponse = CType(objSerializer.ReadObject(objResponse.GetResponseStream), tas.clsCreateFeatureResponse)
        If objCreateResponse IsNot Nothing Then
            If String.IsNullOrEmpty(objCreateResponse.ErrorMessage) Then
                Return New clsFeatureListItem(objCreateResponse.Feature)
            Else
                Throw New ApplicationException(objCreateResponse.ErrorMessage)
            End If
        Else
            Return Nothing
        End If
    End Function

    Public Shared Function GetByProjectId(ByVal objSettings As ISettings, ByVal objSession As Guid, ByVal intProjectId As Integer) As List(Of clsFeatureListItem)
        Dim objRequest As HttpWebRequest
        Dim objResponse As HttpWebResponse
        Dim objUri As Uri
        Dim objQuery As NameValueCollection
        Dim objSerializer As DataContractSerializer
        Dim arrInner() As tas.clsFeatureListItem
        Dim colResult As List(Of clsFeatureListItem)
        Dim i As Integer

        objQuery = New NameValueCollection
        objQuery.Add("projectId", intProjectId.ToString)
        objUri = GetUri(objSettings, "Features", objQuery)
        objRequest = CreateGetRequest(objSession, objUri)

        objResponse = DirectCast(objRequest.GetResponse, HttpWebResponse)
        objSerializer = New DataContractSerializer(GetType(tas.clsFeatureListItem()))
        arrInner = CType(objSerializer.ReadObject(objResponse.GetResponseStream), tas.clsFeatureListItem())
        If arrInner IsNot Nothing Then
            colResult = New List(Of clsFeatureListItem)
            For i = 0 To arrInner.Length - 1
                colResult.Add(New clsFeatureListItem(arrInner(i)))
            Next i
            colResult.Sort()
        Else
            colResult = Nothing
        End If
        Return colResult
    End Function

    Public Function CompareTo(other As clsFeatureListItem) As Int32 Implements IComparable(Of clsFeatureListItem).CompareTo
        Return String.Compare(Title, other.Title, True)
    End Function
End Class
