(function () {
    $.fn.checkList = function (options) {
        var self = this,
			items = options.items ? options.items.slice() : [],
			checkedItems = options.checkedItems ? options.checkedItems.slice() : [],
			valuePath = options.valuePath,
			textPath = options.textPath;

        this.addClass('list-group');
        
        $.each(items, function (key, item) {
            if (checkedItems.length > 0)
                item.selected = $.grep(checkedItems, function (p) { return p[valuePath].toString() === item[valuePath].toString() }).length > 0;

            self.append(createListItem(item, item.selected, valuePath, textPath));
        });

        this.addItem = function (item) {
            items.push(item);
            self.append(createListItem(item, item.selected, valuePath, textPath));
        };

        this.getCheckedItems = function () {
            var selectedItems = new Array();

            $.each(items, function (index, item) {
                if (item.selected) {
                    var selectedItem = {};

                    selectedItem[textPath] = item[textPath];
                    selectedItem[valuePath] = item[valuePath];

                    selectedItems.push(selectedItem);
                }
            });

            return selectedItems;
        };

        this.getUncheckedItems = function () {
            var notSelectedItems = new Array();

            $.each(items, function (index, item) {
                if (!item.selected) {
                    var notSelectedItem = {};
                    notSelectedItem[textPath] = item[textPath];
                    notSelectedItem[valuePath] = item[valuePath];

                    notSelectedItems.push(notSelectedItem);
                }
            });

            return notSelectedItems;
        };

        this.updateCheckedItems = function (checkedItems) {            
            this.checkedItems = checkedItems;
            this.html('');

            $.each(items, function (key, item) {                
                item.selected = checkedItems !== undefined && checkedItems !== null
                    && ($.grep(checkedItems, function (p) { return p[valuePath].toString() === item[valuePath].toString() }) !== undefined);

                self.append(createListItem(item, item.selected, valuePath, textPath));
            });            
        }

        return this;

        function createListItem(item, isChecked, valuePath, textPath) {
            return $(
				'<li class="list-group-item ' + (isChecked ? 'active' : '') + '">' +
				'	<label>' +
				'		<input type="checkbox" ' + (isChecked ? 'checked="checked" ' : '') +
				'value="' + item[valuePath] + '" ' +
				'		/> ' + item[textPath] +
				'	</label>' +
				'</li>')
				.change(function () {
				    var $el = $(this).closest('li').toggleClass('active');
				    var item = $el.data('item');

				    $.grep(items, function (p) { return p[valuePath].toString() === item[valuePath].toString() })[0].selected = $el.context.className.indexOf('active') !== -1;

				}).data('item', item);
        }
    }
})();