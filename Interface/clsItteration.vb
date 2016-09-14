Imports System.IO
Imports tinyak.Interface
Imports tas = tinyak.API.Shared
Public Class clsItteration
    Implements IComparable(Of clsItteration)

    Private m_objInnerItteration As tas.clsItteration

    Private Sub New(ByVal objItteration As tas.clsItteration)
        m_objInnerItteration = objItteration
    End Sub

    Public ReadOnly Property Id As Integer
        Get
            Return m_objInnerItteration.Id
        End Get
    End Property

    Public Property Name As String
        Get
            Return m_objInnerItteration.Name
        End Get
        Set(value As String)
            m_objInnerItteration.Name = value
        End Set
    End Property

    Public Property StartDate As Nullable(Of Date)
        Get
            Return m_objInnerItteration.StartDate
        End Get
        Set(value As Nullable(Of Date))
            m_objInnerItteration.StartDate = value
        End Set
    End Property

    Public Property EndDate As Nullable(Of Date)
        Get
            Return m_objInnerItteration.EndDate
        End Get
        Set(value As Nullable(Of Date))
            m_objInnerItteration.EndDate = value
        End Set
    End Property

    Public Property IsActive As Boolean
        Get
            Return m_objInnerItteration.IsActive
        End Get
        Set(value As Boolean)
            m_objInnerItteration.IsActive = value
        End Set
    End Property

    Public Shared Function GetByProjectId(ByVal objSettings As ISettings, ByVal objSession As Guid, ByVal intProjectId As Integer) As List(Of clsItteration)
        Dim objRequest As HttpWebRequest
        Dim objResponse As HttpWebResponse
        Dim objUri As Uri
        Dim objQuery As NameValueCollection
        Dim objSerializer As DataContractSerializer
        Dim arrInner() As tas.clsItteration
        Dim colResult As List(Of clsItteration)
        Dim i As Integer

        objQuery = New NameValueCollection
        objQuery.Add("projectId", intProjectId.ToString)
        objUri = GetUri(objSettings, "Itterations", objQuery)
        objRequest = CreateGetRequest(objSession, objUri)

        objResponse = DirectCast(objRequest.GetResponse, HttpWebResponse)
        objSerializer = New DataContractSerializer(GetType(tas.clsItteration()))
        arrInner = CType(objSerializer.ReadObject(objResponse.GetResponseStream), tas.clsItteration())
        If arrInner IsNot Nothing Then
            colResult = New List(Of clsItteration)
            For i = 0 To arrInner.Length - 1
                colResult.Add(New clsItteration(arrInner(i)))
            Next i
            colResult.Sort()
        Else
            colResult = Nothing
        End If
        Return colResult
    End Function

    Public Function CompareTo(other As clsItteration) As Int32 Implements IComparable(Of clsItteration).CompareTo
        Dim intResult As Integer
        Dim datValue(1) As Date

        If StartDate.HasValue Then
            datValue(0) = StartDate.Value
        Else
            datValue(0) = Date.MinValue
        End If
        If other.StartDate.HasValue Then
            datValue(1) = other.StartDate.Value
        Else
            datValue(1) = Date.MinValue
        End If
        intResult = Date.Compare(datValue(0), datValue(1))

        If intResult = 0 Then
            If EndDate.HasValue Then
                datValue(0) = EndDate.Value
            Else
                datValue(0) = Date.MinValue
            End If
            If other.EndDate.HasValue Then
                datValue(1) = other.EndDate.Value
            Else
                datValue(1) = Date.MinValue
            End If
            intResult = Date.Compare(datValue(0), datValue(1))
        End If

        If intResult = 0 Then
            intResult = String.Compare(Name, other.Name, True)
        End If

        Return intResult
    End Function
End Class
