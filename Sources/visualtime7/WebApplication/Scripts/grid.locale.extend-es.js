$(function () {
    var dateextension = $.extend({}, $.jgrid.locales["es"].formatter.date, {
        customvalidationdate: {
            msgvaliddate: "Fecha válida - dd/mm/aaaa",
            msginvaliddate: "Fecha inválida - dd/mm/aaaa"
        }
    });
    $.jgrid.locales["es"].formatter.date = dateextension;
});