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
End Code 
<div id="pnlMember">
    @Code
        If Model.ProjectMembers IsNot Nothing AndAlso Model.ProjectMembers.Members IsNot Nothing AndAlso Model.ProjectMembers.Members.Count > 0 Then
            Html.RenderPartial("ProjectMembers", Model.ProjectMembers)
        End If
    End Code
</div>
<h4>Add Member</h4>
<table>
    <tr>
        <td>Email Address:</td>
        <td><input type="text" id="txtEmailAddress" maxlength="250"/></td>
        <td><input type="button" value="Add" onclick="ProjectUpdate.AddMember(@Model.Id, $('#txtEmailAddress').val())"/><span id="lblAddMessage" style="margin-left: 5px;" /></td>
    </tr>
</table>