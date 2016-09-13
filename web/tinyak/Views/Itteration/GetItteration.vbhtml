@ModelType tinyak.clsItterationModel
@Code
    Layout = Nothing
    Html.BeginForm("Update", "Itteration", routeValues:=New With {.id = Model.Id})
End Code
@Html.AntiForgeryToken
<table style="border: 1px solid silver; margin: 2px; padding: 2px;">
    <tr>
        <td>Name:</td>
        <td>@Html.TextBox("Name", Model.Name) @Html.ValidationMessage("Name")</td>
    </tr>
    <tr>
        <td>Begin Date:</td>
        <td>@Html.TextBox("StartDate", Model.StartDate, "{0:M-dd-yyyy}", New With {.maxlength = 10}) @Html.ValidationMessage("StartDate")</td>
    </tr>
    <tr>
        <td>End Date:</td>
        <td>@Html.TextBox("EndDate", Model.EndDate, "{0:M-dd-yyyy}", New With {.maxlength = 10}) @Html.ValidationMessage("EndDate")</td>
    </tr>
    <tr>
        <td>Is Active:</td>
        <td>@Html.CheckBox("IsActive", Model.IsActive)</td>
    </tr>
    <tr>
        <td></td>
        <td><input type="submit" value="Update"/></td>
    </tr>
</table>
@code
    Html.EndForm
End Code