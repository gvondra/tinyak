@ModelType tinyak.clsProjectListModel
@Code
    ViewData("Title") = "Projects"
End Code
<div>
    <div style="display: inline-block; float: right">
        @Html.ActionLink("Create New Project", "Create")
    </div>
    <h2>Projects</h2>
    <p>This is you're project list.</p>
</div>