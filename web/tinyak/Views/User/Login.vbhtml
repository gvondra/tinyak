@ModelType tinyak.clsUserLoginModel
@Code
    ViewData("Title") = "Login"
    Html.BeginForm()
End Code
    @Html.AntiForgeryToken()
    <table>
        <tr>
            <td>Email Address:</td>
            <td>
                @Html.TextBox("EmailAddress", Model.EmailAddress, htmlAttributes:=New With {.maxLength = 240})
                @Html.ValidationMessage("EmailAddress")
            </td>
        </tr>
        <tr>
            <td>Password:</td>
            <td>
                @Html.Password("Password", Model.EmailAddress, htmlAttributes:=New With {.maxLength = 1000})
            </td>
        </tr>
        <tr>
            <td></td>
            <td>
                <input type="submit" value="Login" />
            </td>
        </tr>
    </table>
@Code
    Html.EndForm()
End Code