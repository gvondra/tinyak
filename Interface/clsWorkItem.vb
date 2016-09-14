Imports System.IO
Imports tas = tinyak.API.Shared
Public Class clsWorkItem
    Private m_objInnerWorkItem As tas.clsWorkItem
    Private m_objItteration As clsItteration
    Private Sub New(ByVal objWorkItem As tas.clsWorkItem)
        m_objInnerWorkItem = objWorkItem
        If objWorkItem.Itteration IsNot Nothing Then
            m_objItteration = New clsItteration(objWorkItem.Itteration)
        Else
            m_objItteration = Nothing
        End If
    End Sub

    Public ReadOnly Property Id As Nullable(Of Integer)
        Get
            Return m_objInnerWorkItem.Id
        End Get
    End Property

    Public Property Title As String
        Get
            Return m_objInnerWorkItem.Title
        End Get
        Set(value As String)
            m_objInnerWorkItem.Title = value
        End Set
    End Property

    Public Property Itteration As clsItteration
        Get
            Return m_objItteration
        End Get
        Set(value As clsItteration)
            m_objItteration = value
        End Set
    End Property

    Public Property State As enumWorkItemState
        Get
            Return CType(CType(m_objInnerWorkItem.State, Int16), enumWorkItemState)
        End Get
        Set(value As enumWorkItemState)
            m_objInnerWorkItem.State = CType(CType(value, Int16), tas.enumWorkItemState)
        End Set
    End Property

    Public Property AssignedTo As String
        Get
            Return m_objInnerWorkItem.AssignedTo
        End Get
        Set
            m_objInnerWorkItem.AssignedTo = Value
        End Set
    End Property

    Public Property Effort As Nullable(Of Int16)
        Get
            Return m_objInnerWorkItem.Effort
        End Get
        Set(value As Nullable(Of Int16))
            m_objInnerWorkItem.Effort = value
        End Set
    End Property

    Public Property Description As String
        Get
            Return m_objInnerWorkItem.Description
        End Get
        Set(value As String)
            m_objInnerWorkItem.Description = value
        End Set
    End Property

    Public Property AcceptanceCriteria As String
        Get
            Return m_objInnerWorkItem.AcceptanceCriteria
        End Get
        Set(value As String)
            m_objInnerWorkItem.AcceptanceCriteria = value
        End Set
    End Property

    Public Shared Function [Get](ByVal objSettings As ISettings, ByVal objSession As Guid, ByVal intWorkItemId As Integer) As clsWorkItem
        Dim objRequest As HttpWebRequest
        Dim objResponse As HttpWebResponse
        Dim objUri As Uri
        Dim objSerializer As DataContractSerializer
        Dim objInner As tas.clsWorkItem
        Dim objResult As clsWorkItem

        objUri = GetUri(objSettings, String.Format("WorkItem/{0}", intWorkItemId))
        objRequest = CreateGetRequest(objSession, objUri)

        objResponse = DirectCast(objRequest.GetResponse, HttpWebResponse)
        objSerializer = New DataContractSerializer(GetType(tas.clsWorkItem))
        objInner = CType(objSerializer.ReadObject(objResponse.GetResponseStream), tas.clsWorkItem)
        If objInner IsNot Nothing Then
            objResult = New clsWorkItem(objInner)
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
        Dim objInner As tas.clsWorkItem
        Dim objStream As Stream

        If m_objItteration Is Nothing Then
            m_objInnerWorkItem.Itteration = Nothing
        Else
            m_objItteration.Update(m_objInnerWorkItem)
        End If

        objUri = GetUri(objSettings, String.Format("WorkItem/{0}", Id))
        objRequest = CreatePutRequest(objSessionId, objUri)

        objSerializer = New DataContractSerializer(GetType(tas.clsWorkItem))
        objStream = objRequest.GetRequestStream
        objSerializer.WriteObject(objStream, m_objInnerWorkItem)
        objStream.Flush()
        objStream.Close()

        objResponse = DirectCast(objRequest.GetResponse, HttpWebResponse)
        objSerializer = New DataContractSerializer(GetType(tas.clsWorkItem))
        objInner = CType(objSerializer.ReadObject(objResponse.GetResponseStream), tas.clsWorkItem)
        If objInner IsNot Nothing Then
            Me.AcceptanceCriteria = objInner.AcceptanceCriteria
            Me.AssignedTo = objInner.AssignedTo
            Me.Description = objInner.Description
            Me.Effort = objInner.Effort
            Me.State = CType(CType(objInner.State, Int16), enumWorkItemState)
            Me.Title = objInner.Title
        End If
    End Sub
End Class
