Imports tc = tinyak.Core
Public Class clsItterationListModel
    Public Property ProjectId As Integer
    Public Property ProejctTitle As String
    Public Property NewName As String
    Public Property NewBeginDate As Nullable(Of Date)
    Public Property NewEndDate As Nullable(Of Date)
    Public Property NewIsActive As Boolean
    Public Property Itterations As List(Of clsItterationModel)
    Public Property UpdateItteration As clsItterationModel

    Public Sub New()
        NewIsActive = True
    End Sub
End Class

Public Class clsItterationModel
    Public Property Id As Integer
    Public Property Name As String
    Public Property StartDate As Nullable(Of Date)
    Public Property EndDate As Nullable(Of Date)
    Public Property IsActive As Boolean

    Friend Sub Load(ByVal objInner As tc.clsItteration)
        With objInner
            Id = .Id.Value
            Name = .Name
            StartDate = .StartDate
            EndDate = .EndDate
            IsActive = .IsActive
        End With
    End Sub
End Class