"use strict";
var itteration = (function () {

    function LoadItteration(itterationId) {
        $("#tblcItteration").text("loading…")

        var request = $.ajax({
            url: "/Itteration/Get",
            data: { id: itterationId },
            dataType: "html"
        });

        request.done(OnLoadItterationDone);
        request.fail(OnLoadItterationFail);
    }

    function OnLoadItterationDone(data, textStatus, jqXHR) {
        $("#tblcItteration").html(data);
    }

    function OnLoadItterationFail(jqXHR, textStatus, errorThrown) {
        $("#tblcItteration").text(textStatus);
    }

    function DeleteItteration(itterationId, sender) {
        var row = $(sender).closest("tr");
        row.css("background", "pink");
        var request = $.ajax({
            url: "/Itteration/Delete",
            data: { id: itterationId },
            dataType: "html",
            method: "POST",
            context: row
        });

        request.done(OnDeleteItterationDone);
        request.fail(OnDeleteItterationFail);
    }

    function OnDeleteItterationDone(data, textStatus, jqXHR) {
        $(this).css("visibility", "hidden");
        $(this).css("display", "none");
        $("#tblcItteration").text("");
    }

    function OnDeleteItterationFail(jqXHR, textStatus, errorThrown) {
        $("#tblcItteration").text(textStatus);
    }

    return {
        LoadItteration: LoadItteration,
        DeleteItteration: DeleteItteration
    }
})();