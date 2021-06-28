app.menu = (function () {

    Setup = function () {

        $('#mainmenu').on('change', function () {
            LoadMenu($('#mainmenu').val(), $('#mainmenu option:selected').text());
        });

        $('#vt').jstree({
            "plugins": ["wholerow"],
            'core': {
                'themes': { 'name': 'default' },
                "check_callback": true,
                'data': null
            }
        }).on("changed.jstree", function (e, data) {
            if (data.selected.length) {
                var node = data.instance.get_node(data.selected[0]);

                if (node.data.WindowType !== 8) {
                    if (node.data.locked) {
                        notification.swal.warning('Oops!', dict.TransactionNotAllowed[localStorage.getItem("languageName")]);
                    }
                    else {
                        if (node.data.url && node.data.url !== null && node.data.url !== '') {
                            var win = open(node.data.url, node.text, 'toolbar=no,resizable=no,location=no,directories=no,status=yes,menubar=no,copyhistory=no,width=750,height=450,left=20,top=20');
                            if (win !== null) {
                                win.moveTo(0, 0);
                                win.resizeTo(window.screen.availWidth, window.screen.availHeight);
                            }
                        }
                    }
                }
            }
        });


    };

    LoadMainMenu = function () {
        app.core.AsyncWebMethod("/fasi/core/menu.aspx/MainMenu", false,
            JSON.stringify({}),
            function (data) {
                var ctrol = $('#mainmenu');
                var firstCode = '';
                var firstDesc = '';

                $.each(data.d, function () {
                    $("#buttonsPlace").append('<li><a name="MainMenuOption" href="#" class="dropdown-item" data-code="' + this['code'] + '">' + this['description'] + '</a></li>');

                    //Opcion1 con combo
                    //Opcion1 ctrol.append($('<option />').val(this['code']).text(this['description']));

                    if (firstCode === '') {
                        firstCode = this['code'];
                        firstDesc = this['description'];
                    }
                });
                //Opcion1 ctrol.val(firstCode);
                //Opcion1 ctrol.change();

                LoadMenu(firstCode, firstDesc);

                $('a[name=MainMenuOption]').click(function (e) {
                    e.preventDefault();
                    LoadMenu($(this).data('code'), $(this).text());
                });

            });
    };
    
    LoadMenu = function (code, title) {

      
        $('#MainMenuTitle').html('Cargando...');
        $('#vt').jstree(true).settings.core.data = null;
        $('#vt').jstree(true).refresh();

        masterSupport.user.schemeCode = 'EASE1';
        masterSupport.user.companyId = 1;

        app.core.Get(constants.fasiApi.backoffice + 'FullMenuInformation', false, true, true,
            {
                menuCode: code,
                schemaCode: masterSupport.user.schemeCode,
                companyId: masterSupport.user.companyId
            }, function (data) {
                if (data && data.Successfully) {
                    $('#MainMenuTitle').html(title);
                    $('#vt').jstree(true).settings.core.data = ItemTreeMapper(data.Data.Items);
                    $('#vt').jstree(true).refresh();
                }
            }
        );
    };

    ItemTreeMapper = function (data) {
        var items = new Array();
        $.each(data, function (index, item) {
            var treeNode = {
                data: { url: item.URLAccessLink, locked: item.Icon === 'fa-lock', WindowType: item.WindowType },
                text: item.Description,
                icon: item.Icon && item.Icon !== null && item.Icon !== '' ? 'fa ' + item.Icon : 'fa fa-folder'
            };
            if (item.Items && item.Items.length > 0)
                treeNode.children = ItemTreeMapper(item.Items);
            items.push(treeNode);
        });
        return items;
    };

    return {
        Init: function () {
            masterSupport.setPageTitle('Core - Panel de acceso');
            Setup();
            LoadMainMenu();
        }
    };
})();

$(document).ready(function () {
    app.menu.Init();
});
