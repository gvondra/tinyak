Imports tas = tinyak.API.Shared
Public Class clsProject
    Private m_objInnerProject As tas.clsProject
    Private Sub New(ByVal objProject As tas.clsProject)
        m_objInnerProject = objProject
    End Sub

    Public ReadOnly Property Id As Integer
        Get
            Return m_objInnerProject.Id
        End Get
    End Property

    Public ReadOnly Property Title As String
        Get
            Return m_objInnerProject.Title
        End Get
    End Property

    Public Shared Function [Get](ByVal objSettings As ISettings, ByVal objSession As Guid) As List(Of clsProject)
        Dim objRequest As HttpWebRequest
        Dim objResponse As HttpWebResponse
        Dim objUri As Uri
        Dim objSerializer As DataContractSerializer
        Dim arrInnerProject() As tas.clsProject
        Dim colResult As List(Of clsProject)
        Dim i As Integer

        objUri = GetUri(objSettings, "Project")
        objRequest = CreateGetRequest(objSession, objUri)

        objResponse = DirectCast(objRequest.GetResponse, HttpWebResponse)
        objSerializer = New DataContractSerializer(GetType(tas.clsProject()))
        arrInnerProject = CType(objSerializer.ReadObject(objResponse.GetResponseStream), tas.clsProject())
        If arrInnerProject IsNot Nothing Then
            colResult = New List(Of clsProject)
            For i = 0 To arrInnerProject.Length - 1
                colResult.Add(New clsProject(arrInnerProject(i)))
            Next i
        Else
            colResult = Nothing
        End If
        Return colResult
    End Function
End Class
