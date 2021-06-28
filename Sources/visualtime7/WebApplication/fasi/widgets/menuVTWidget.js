
var menuvtSupport = new function () {
    this.load = function (widgetData) {
        if (widgetData.State && widgetData.State != null) {
            var treeViewName = widgetData.Id + 'TreeView';
            $('#' + widgetData.Id + ' .ibox-content #menuVtTw').append('<div id="' + treeViewName + '"></div>');

            var state = JSON.parse(widgetData.State);

            // carga la información del menú
            ajaxJsonHelper.get(constants.fasiApi.backoffice + 'FullMenuInformation',
                {
                    menuCode: state.module,
                    schemaCode: masterSupport.user.schemeCode,
                    companyId: masterSupport.user.companyId
                },
                function (response) {
                    if (response && response.Successfully) {
                        items = menuvtSupport.createTreeItems(response.Data.Items);

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

                                if (node.data.WindowType !== 8) {
                                    if (node.data.locked) {
                                        notification.swal.warning('Oops!', dict.TransactionNotAllowed[localStorage.getItem("languageName")]);
                                    }
                                    else {
                                        if (node.data.url && node.data.url != null && node.data.url != '') {
                                            var win = open(node.data.url, node.text, 'toolbar=no,resizable=no,location=no,directories=no,status=yes,menubar=no,copyhistory=no,width=750,height=450,left=20,top=20');
                                            if (win != null) {
                                                win.moveTo(0, 0);
                                                win.resizeTo(window.screen.availWidth, window.screen.availHeight);
                                            }
                                        }
                                    }
                                }
                            }
                        });
                    }
                });
        }
    };

    // Crea los artículos del arból del menú
    this.createTreeItems = function (data) {
        var items = new Array();
        $.each(data, function (index, item) {
            var treeNode = {
                data: { url: item.URLAccessLink, locked: item.Icon == 'fa-lock', WindowType: item.WindowType },
                text: item.Description,
                icon: item.Icon && item.Icon != null && item.Icon != '' ? 'fa ' + item.Icon : 'fa fa-folder'
            };

            // Si un artículo contiene artículos hijos, entonces se llama el método de forma recursiva
            if (item.Items && item.Items.length > 0)
                treeNode.children = menuvtSupport.createTreeItems(item.Items);

            items.push(treeNode);
        });
        return items;
    };
};