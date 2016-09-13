@ModelType tinyak.clsItterationListModel
@Code
    ViewData("PageTitle") = "Project Itterations"
End Code
@Section Styles
    <style type="text/css">
        input[type='text']
        {
            border: 0px;
            margin: 0px;
        }
        
        table.dataTable tr td
        {
            padding: 0px;
        }
    </style>
End Section

@Section Scripts
    <script src="@Url.Content("~/Scripts/itteration.js")" type="text/javascript"></script>
End Section
<h2>Project Itterations</h2>
<h3>@Html.ActionLink(Model.ProejctTitle, "Update", "Project", routeValues:=New With {.id = Model.ProjectId}, htmlAttributes:=Nothing)</h3>
<table>
    <tr>
        <td style="vertical-align: top;">
            <table class="dataTable">
                <tr>
                    <th></th>
                    <th>Name</th>
                    <th>Begin</th>
                    <th>End</th>
                    <th>Is Active</th>
                </tr>
                @Code
                    If Model.Itterations IsNot Nothing AndAlso Model.Itterations.Count > 0 Then
                        For Each objItteration As clsItterationModel In Model.Itterations
                End Code
                <tr>
                    <td><input type="button" value="Delete" onclick="itteration.DeleteItteration(@objItteration.Id, this)" /></td>
                    <td><a onclick="itteration.LoadItteration(@objItteration.Id)" href="#">@objItteration.Name</a></td>
                    <td>@String.Format("{0:M-dd-yyyy}", objItteration.StartDate)</td>
                    <td>@String.Format("{0:M-dd-yyyy}", objItteration.EndDate)</td>
                    <td>@Code 
                        If objItteration.IsActive Then
                            @("Yes")
                        Else
                            @("No")
                        End If
                        End Code
                    </td>
                </tr>
                @Code
                        Next objItteration
                    End If
                    Html.BeginForm("Create", "Itteration", New With {.projectId = Model.ProjectId})
                End Code
                @Html.AntiForgeryToken
                <tr>
                    <td><input type="submit" value="Add" /></td>
                    <td>
                        @Html.TextBox("NewName", Model.NewName) @Html.ValidationMessage("NewName")
                    </td>
                    <td>@Html.TextBox("NewBeginDate", Model.NewBeginDate, "{0:M-dd-yyyy}", New With {.maxlength = 10}) @Html.ValidationMessage("NewBeginDate")</td>
                    <td>@Html.TextBox("NewEndDate", Model.NewEndDate, "{0:M-dd-yyyy}", New With {.maxlength = 10}) @Html.ValidationMessage("NewEndDate")</td>
                    <td>@Html.CheckBox("NewIsActive", Model.NewIsActive)</td>
                </tr>
                @Code
                    Html.EndForm
                End Code
            </table>
        </td>
        <td id="tblcItteration" style="vertical-align: top;">
            @Code
                If Model.UpdateItteration IsNot Nothing Then
                    Html.RenderPartial("GetItteration", Model.UpdateItteration)
                End If
            End Code
        </td>
    </tr>
</table>