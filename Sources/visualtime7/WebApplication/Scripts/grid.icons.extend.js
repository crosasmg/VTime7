$(function () {
	var iconsExtend = $.extend({}, $.jgrid.icons.glyph, {
		form: {
			undo: "glyphicon-remove-circle",
			save: "fa fa-pencil-square-o fa-lg"
		},
		subgrid: {
			plus: "glyphicon-triangle-right",
			minus: "glyphicon-triangle-bottom"
		}
	});
	$.jgrid.icons.glyph = iconsExtend;
});