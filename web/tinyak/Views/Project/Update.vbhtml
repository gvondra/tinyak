@ModelType tinyak.clsProjectUpdateModel
@Code
    ViewData("PageTitle") = "Update Project"
End Code
@Section Scripts
    <script src="@Url.Content("~/Scripts/projectUpdate.js")" type="text/javascript"></script>
End Section

<h2>Update Project</h2>
@Code
    Html.BeginForm("Update", "Project", routeValues:=New With {.id = Model.Id})
End Code
@Html.AntiForgeryToken
<table>
    <tr>
        <td>Title:</td>
        <td>@Html.TextBox("ProjectTitle", Model.ProjectTitle, New With {.maxlength = 500}) @Html.ValidationMessage("ProjectTitle")</td>
    </tr>
    <tr>
        <td></td>
        <td><input type="submit" value="Update" /></td>
    </tr>
</table>
@Code
    Html.EndForm

    If Model.ProjectMembers IsNot Nothing AndAlso Model.ProjectMembers.Count > 0 Then
End Code
    <h3>Members</h3>
    <table>
        @Code
            For Each strEmail As String In Model.ProjectMembers
        End Code
        <tr>
            <td>@strEmail</td>
            <td><a onclick="ProjectUpdate.HideRow(this);" href="#">Remove</a></td>
        </tr>
        @Code
            Next strEmail
        End Code
    </table>
@Code
    End If
End Code
@Code
    Html.BeginForm("AddProjectMember", "Project", routeValues:=New With {.id = Model.Id})
End Code
@Html.AntiForgeryToken
<h4>Add Member</h4>
<table>
    <tr>
        <td>Email Address:</td>
        <td>@Html.TextBox("AddEmailAddress", value:=Nothing, htmlAttributes:=New With {.maxLength = 250})</td>
        <td><input type="submit" value="Add"/></td>
    </tr>
</table>
@Code
    Html.EndForm
End Code