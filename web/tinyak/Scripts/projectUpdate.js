"use strict";
var ProjectUpdate = (function () {
    function HideRow(target)
    {
        $(target).closest("tr").css("visibility", "hidden");
        $(target).closest("tr").css("display", "none");
    }
    return {
        HideRow: HideRow
    }
})();