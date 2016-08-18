"use strict";
var ProjectUpdate = (function () {

    function LoadMembers(projectId) {
        var request = $.ajax({
            url: "../ProjectMembers",
            data: { id: projectId},
            dataType: "html",
        });

        request.done(OnLoadMembersDone);
        request.fail(OnLoadMembersFail);
    }

    function OnLoadMembersDone(data, textStatus, jqXHR) {
        $("#pnlMember").html(data);
    }

    function OnLoadMembersFail(jqXHR, textStatus, errorThrown) {
        $("#pnlMember").text(textStatus);
    }

    function AddMember(projectId, emailAddress) {
        var request = $.ajax({
            url: "../ProjectMember",
            data: { id: projectId, emailAddress: emailAddress },
            dataType: "json",
            method: "POST"
        });
        
        request.done(OnAddMemberDone);
        request.fail(OnAddMemberFail);
    }

    function OnAddMemberDone(data, textStatus, jqXHR) {
        LoadMembers(data.ProjectId);
        $("#lblAddMessage").text(data.EmailAddress);
        $("#txtEmailAddress").val('');
    }

    function OnAddMemberFail(jqXHR, textStatus, errorThrown) {
        $("#lblAddMessage").text(textStatus);
    }

    function RemoveMember(target, projectId, emailAddress) {
        var cell = $(target).closest("td");
        var request = $.ajax({
            url: "../ProjectMember",
            data: { id: projectId, emailAddress: emailAddress },
            context: cell,
            dataType: "html",
            method: "DELETE"
        });
                
        request.done(OnRemoveMemberDone);
        request.fail(OnRemoveMemberFail);
    }

    function OnRemoveMemberDone(data, textStatus, jqXHR) {
        $(this).text(data);
    }

    function OnRemoveMemberFail(jqXHR, textStatus, errorThrown) {
        $(this).text(textStatus);
    }

    return {
        RemoveMember: RemoveMember,
        AddMember: AddMember
    }
})();