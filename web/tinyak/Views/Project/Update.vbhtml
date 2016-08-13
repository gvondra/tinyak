@ModelType tinyak.clsProjectUpdateModel
@Code
    ViewData("Title") = "Update Project"
End Code

<h2>Update Project</h2>
@Code
    Html.BeginForm
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