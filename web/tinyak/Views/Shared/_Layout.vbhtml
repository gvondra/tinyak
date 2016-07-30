﻿<!DOCTYPE html>
<html>
<head>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>@ViewBag.Title - tinyak</title>
    @Styles.Render("~/Content/css")
</head>
<body>
    <div>
        <div style="display: inline; float:right">
            @Code
                If ViewData.ContainsKey("User") = False Then
                    @Html.Partial("LoginHeader")
                Else
                    @Html.Partial("ProfileHeader", ViewData("User"))
                End If
            End Code
        </div>
        <h1>@Html.ActionLink("tinyak", "Index", "Home", Nothing, New With {.Style = "text-decoration: none"})</h1>
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
