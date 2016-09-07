Imports System.IO
Imports tas = tinyak.API.Shared
Public Class clsWorkListItem
    Implements IComparable(Of clsWorkListItem)

    Private m_objInnerWorkListItem As tas.clsWorkListItem

    Private Sub New(ByVal objWorkListItem As tas.clsWorkListItem)
        m_objInnerWorkListItem = objWorkListItem
    End Sub

    Public ReadOnly Property Id As Nullable(Of Integer)
        Get
            Return m_objInnerWorkListItem.Id
        End Get
    End Property

    Public Property Title As String
        Get
            Return m_objInnerWorkListItem.Title
        End Get
        Set(value As String)
            m_objInnerWorkListItem.Title = value
        End Set
    End Property

    Public Property State As enumWorkItemState
        Get
            Return CType(CType(m_objInnerWorkListItem.State, Int16), enumWorkItemState)
        End Get
        Set(value As enumWorkItemState)
            m_objInnerWorkListItem.State = CType(CType(value, Int16), tas.enumWorkItemState)
        End Set
    End Property

    Public Property AssignedTo As Nullable(Of Integer)
        Get
            Return m_objInnerWorkListItem.AssignedTo
        End Get
        Set(value As Nullable(Of Integer))
            m_objInnerWorkListItem.AssignedTo = value
        End Set
    End Property

    Public Property Effort As Nullable(Of Int16)
        Get
            Return m_objInnerWorkListItem.Effort
        End Get
        Set(value As Nullable(Of Int16))
            m_objInnerWorkListItem.Effort = value
        End Set
    End Property

    Public Shared Function Create(ByVal objSettings As ISettings, ByVal objSession As Guid, ByVal intProjectId As Integer, ByVal intFeatureId As Integer, ByVal strTitle As String) As clsWorkListItem
        Dim objRequest As HttpWebRequest
        Dim objResponse As HttpWebResponse
        Dim objUri As Uri
        Dim objQuery As NameValueCollection
        Dim objSerializer As DataContractSerializer
        Dim objCreateResponse As tas.clsCreateWorkItemResponse
        Dim objCreateRequest As tas.clsCreateWorkItemRequest
        Dim objStream As Stream

        objQuery = New NameValueCollection
        objQuery.Add("projectId", intProjectId.ToString)
        objUri = GetUri(objSettings, "WorkItem", objQuery)
        objRequest = CreatePostRequest(objSession, objUri)

        objCreateRequest = New tas.clsCreateWorkItemRequest
        objCreateRequest.FeatureId = intFeatureId
        objCreateRequest.Title = strTitle
        objSerializer = New DataContractSerializer(GetType(tas.clsCreateWorkItemRequest))
        objStream = objRequest.GetRequestStream
        objSerializer.WriteObject(objStream, objCreateRequest)
        objStream.Flush()
        objStream.Close()

        objResponse = DirectCast(objRequest.GetResponse, HttpWebResponse)
        objSerializer = New DataContractSerializer(GetType(tas.clsCreateWorkItemResponse))
        objCreateResponse = CType(objSerializer.ReadObject(objResponse.GetResponseStream), tas.clsCreateWorkItemResponse)
        If objCreateResponse IsNot Nothing Then
            If String.IsNullOrEmpty(objCreateResponse.ErrorMessage) Then
                Return New clsWorkListItem(objCreateResponse.WorkItem)
            Else
                Throw New ApplicationException(objCreateResponse.ErrorMessage)
            End If
        Else
            Return Nothing
        End If
    End Function

    Public Shared Function GetByFeatureId(ByVal objSettings As ISettings, ByVal objSession As Guid, ByVal intFeatureId As Integer) As List(Of clsWorkListItem)
        Dim objRequest As HttpWebRequest
        Dim objResponse As HttpWebResponse
        Dim objUri As Uri
        Dim objQuery As NameValueCollection
        Dim objSerializer As DataContractSerializer
        Dim arrInner() As tas.clsWorkListItem
        Dim colResult As List(Of clsWorkListItem)
        Dim i As Integer

        objQuery = New NameValueCollection
        objQuery.Add("featureId", intFeatureId.ToString)
        objUri = GetUri(objSettings, "WorkItems", objQuery)
        objRequest = CreateGetRequest(objSession, objUri)

        objResponse = DirectCast(objRequest.GetResponse, HttpWebResponse)
        objSerializer = New DataContractSerializer(GetType(tas.clsWorkListItem()))
        arrInner = CType(objSerializer.ReadObject(objResponse.GetResponseStream), tas.clsWorkListItem())
        If arrInner IsNot Nothing Then
            colResult = New List(Of clsWorkListItem)
            For i = 0 To arrInner.Length - 1
                colResult.Add(New clsWorkListItem(arrInner(i)))
            Next i
            colResult.Sort()
        Else
            colResult = Nothing
        End If
        Return colResult
    End Function

    Public Function CompareTo(other As clsWorkListItem) As Int32 Implements IComparable(Of clsWorkListItem).CompareTo
        Return String.Compare(Title, other.Title, True)
    End Function
End Class
