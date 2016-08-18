@ModelType tinyak.clsProjectMembersModel
@Code
    Layout = Nothing
End Code
<h3>Members</h3>
<table>
    @Code
        For Each strEmail As String In Model.Members
    End Code
    <tr>
        <td>@strEmail</td>
        <td><a onclick="ProjectUpdate.RemoveMember(this, @Model.ProjectId, '@strEmail');" href="#">Remove</a></td>
    </tr>
    @Code
        Next strEmail
    End Code
</table>

