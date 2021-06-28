﻿var navdirectorySupport = new function () {
    this.load = function (widgetData) { 
        if (widgetData.State && widgetData.State !== null) {
            var treeViewName = widgetData.Id + 'TreeView';
            $('#' + widgetData.Id + ' .ibox-content #menuVtTw').append('<div id="' + treeViewName + '"></div>');

            var state = JSON.parse(widgetData.State);

            //carga la información del menú 
            app.core.Get(constants.fasiApi.widgetSupport + 'NavigationDirectory?category=' + state.Category, false, true, true, null,
                function (data) {
                    if (data && data.Successfully) {
                        items = navdirectorySupport.BuildTreeItems(data.Data);
                        $('#' + treeViewName).jstree({
                            "plugins": ["wholerow"],
                            'core': {
                                'themes': { 'name': 'default' },
                                "check_callback": true,
                                'data': items
                            }
                        }).on("changed.jstree", function (e, data) {
                            if (data.selected.length) {
                                var node = data.instance.get_node(data.selected[0]);

                                window.location.href = node.data.url;
                            }
                        });
                    }
                });
        }
    };
    // Crea los artículos del arból del menú
    this.BuildTreeItems = function (data) {
        var items = new Array();
        $.each(data, function (index, item) {
            var treeNode = {
                data: { url: item.Path },
                text: item.Description,
                icon: 'fa fa-database',
                a_attr: item.Path
            };

            // Si un artículo contiene artículos hijos, entonces se llama el método de forma recursiva
            if (item.Items && item.Items.length > 0)
                treeNode.children = navdirectorySupport.BuildTreeItems(item.Items);

            items.push(treeNode);
        });
        return items;
    };
};