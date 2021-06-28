$(function () {
    var dateextension = $.extend({}, $.jgrid.locales["en"].formatter.date, {
        customvalidationdate: {
            msgvaliddate: "Valid date - dd/mm/yyyy",
            msginvaliddate: "Invalid date - dd/mm/yyyy"
        }
    });
    $.jgrid.locales["en"].formatter.date = dateextension;
});