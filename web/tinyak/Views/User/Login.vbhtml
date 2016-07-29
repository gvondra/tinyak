@Code
    Layout = "~/Views/Shared/_Layout.vbhtml"
    ViewData("Title") = "Login"
    Html.BeginForm()
End Code
    @Html.AntiForgeryToken()
    <table>
        <tr>
            <td>Email Address:</td>
            <td>
                <input type="text" maxlength="250"/>
            </td>
        </tr>
        <tr>
            <td>Password:</td>
            <td>
                <input type="password" maxlength="1000"/>
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