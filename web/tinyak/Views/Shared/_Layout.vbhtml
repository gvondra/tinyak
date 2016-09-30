<!DOCTYPE html>
<html>
<head>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>@ViewBag.PageTitle - tinyak</title>
    @Styles.Render("~/Content/css")
    @RenderSection("styles", required:=False)
</head>
<body>
    <div>
        <div style="display: inline; float:right">
            @Code
                If ViewData.ContainsKey("UserId") = False Then
                    @Html.Partial("LoginHeader")
                Else
                    @Html.Partial("ProfileHeader")
                End If
            End Code
        </div>
        <h1>@Html.ActionLink("tinyak", "Index", "Home", Nothing, New With {.Style = "text-decoration: none"})</h1>
    </div>
    <div class="tabContainer">
        <div style="display: inline-block; margin: 0px; vertical-align: middle;">
            <nav class="tabRow">
                <ul>
                    <li>
                        @Html.ActionLink("Projects", "List", "Project")
                    </li>
                    <li>
                        <a href="http://tinyak.net/client/tinyak.application">Launch It!</a>
                    </li>
                    <li>
                        <a href="http://tinyak.net/client/setup.exe">Install It!</a>
                    </li>
                </ul>
            </nav>
        </div>
    </div>
    <div>
        @RenderBody()
        <hr />
        <footer>
            <p>&copy; @DateTime.Now.Year - tinyak</p>
        </footer>
    </div>
    
    @Scripts.Render("~/bundles/jquery")
    @RenderSection("scripts", required:=False)
</body>
</html>
