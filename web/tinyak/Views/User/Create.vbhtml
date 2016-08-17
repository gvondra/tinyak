@Code
    Layout = "~/Views/Shared/_Layout.vbhtml"
    ViewData("PageTitle") = "Create Login"
    Html.BeginForm()
End Code
    @Html.AntiForgeryToken
    <table>
        <tr>
            <td>Name:</td>
            <td>
                @Html.TextBox("Name", ViewData("Name"))
                @Html.ValidationMessage("Name")
            </td>
        </tr>
        <tr>
            <td>Email Address:</td>
            <td>
                @Html.TextBox("EmailAddress", ViewData("EmailAddress"))
                @Html.ValidationMessage("EmailAddress")
            </td>
        </tr>
        <tr>
            <td>Password:</td>
            <td>
                @Html.Password("Password", ViewData("Password"))
                @Html.ValidationMessage("Password")
            </td>
        </tr>
        <tr>
            <td></td>
            <td>
                <input type="submit" value="Create"/>
            </td>
        </tr>
    </table>
@Code
    Html.EndForm()
End Code