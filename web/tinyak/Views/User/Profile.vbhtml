@ModelType tinyak.clsUserModel
@Code
    ViewData("Title") = "Profile"
    Html.BeginForm()
End Code
@Html.AntiForgeryToken()
@Html.Hidden("EmailAddress", Model.EmailAddress)
<table>
    <tr>
        <td>Email Address:</td>
        <td>@Html.Encode(Model.EmailAddress)</td>
    </tr>
    <tr>
        <td>Name:</td>
        <td>@Html.TextBox("Name", Model.Name) @Html.ValidationMessage("Name")</td>
    </tr>
    <tr>
        <td></td>
        <td><input type="submit" value="Save"/></td>
    </tr>
</table>
@Code
    Html.EndForm()
End Code
