@ModelType tinyak.clsProjectListModel
@Code
    ViewData("PageTitle") = "Projects"
End Code
<div>
    <div style="display: inline-block; float: right">
        @Html.ActionLink("Create New Project", "Create")
    </div>
    <h2>Projects</h2>
    <p>This is you're project list.</p>
    @Code
        If Model.Items IsNot Nothing AndAlso Model.Items.Count > 0 Then
    End Code
        <ul>
            @Code 
                For each objItem As clsProjectListItemModel In Model.Items
            End Code
                    <li>@Html.ActionLink(objItem.Title, "Update", New With {.id = objItem.Id})</li>
            @Code
                Next objItem
            End Code
        </ul>
    @Code 
        End If
    End Code
</div>