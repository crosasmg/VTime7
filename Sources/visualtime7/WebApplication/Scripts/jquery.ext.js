(function($) {
    jQuery.fn.visible = function() {
		return this.css('visibility', 'visible');
	};

	jQuery.fn.invisible = function() {
		return this.css('visibility', 'hidden');
	};
}(jQuery));