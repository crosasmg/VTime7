var defaultPage = new function () {
    this.itemNotFound = ' <div class="middle-box-error text-center animated fadeInDown">' +
        '   <h1>404</h1>' +
        '   <h3 id="ResourceNotFound" class="font-bold trn" >{{ResourceNotFound}}</h3>' +
        '   <div id="ResourceNotFoundDetail" class="error-desc trn" >{{ResourceNotFoundDetail}}</div>' +
        ' </div>';

    this.editWidget = ' <a class="" href="javascript:defaultPage.editInstanceWidget(\'{id}\')" >' +
        '                <i class="fa fa-pencil"></i>' +
        '               </a>';

    this.saveWidgetButton = ' <div id="saveWidgetEdit{id}"  class="saveWidgetEdit{id} ibox-tools  col-md-4 collapse" style="display:none; margin-bottom: -2px; margin-top: -10px;">' +
        '                               <div style="width:80%;float:left">' +
        '                                   <input type="hidden" class="form-control" id="widgetTitleOld{id}" value="{title}" class=" d-inline-flex"/> ' +
        '                                   <input type="text" class="form-control" id="widgetTitle{id}" value="{title}" maxlength="200" class=" d-inline-flex"/> ' +
        '                               </div>' +
        '                               <div style="Text-align:left;width:20%;float:right;cursor:pointer">' +
        '                                   <a href="javascript:defaultPage.updateWidgetInstanceTitle(\'{id}\')" class="nav-link d-inline-flex">' +
        '                                       <i class="fa fa-save"></i>' +
        '                                   </a>' +
        '                               </div>' +
        '                   </div>';

    this.widgetHtml = '     <div class="grid-stack-item" id="{id}" ' +
        '          data-gs-x="{x}"' +
        '          data-gs-y="{y}"' +
        '          data-gs-width="{width}"' +
        '          data-gs-height="{height}"' +
        '          data-gs-min-width="3"' +
        '          data-gs-min-height="{minHeight}"' +
        '          data-original-height="{originalHeight}"' +
        '          {autoPosition} {noResize}>' +
        '         <div class="grid-stack-item-content">' +
        '             <div class="ibox">' +
        '                 <div class="ibox-title draggable-active">' +
        '                     <div id="titleWidget{id}" class="saveWidgetEdit{id}">' +
        '                       <h5>{title}</h5>' +
        '                     </div>' +
        '                     {saveWidgetButton} ' +
        '                     <div class="ibox-tools" style="width:50%;float:right">' +
        '                         {editWidget}' +
        '                         <a class="expand-link" style="{expand-link-style}">' +
        '                             <i class="fa fa-expand"></i>' +
        '                         </a>' +
        '                         <a class="collapse-link">' +
        '                             <i class="{collapse-link-icon}"></i>' +
        '                         </a>' +
        '                         {close}' +
        '                     </div>' +
        '                 </div>' +
        '                 <div class="ibox-content">' +
        '                 </div>' +
        '             </div>' +
        '         </div>' +
        '     </div>';

    this.buttonClose = '                         <a class="close-link" href="javascript:defaultPage.removeWidget(\'{id}\')"> ' +
        '                             <i class="fa fa-times"></i>' +
        '                         </a>';

    this.editInstanceWidget = function (id) {
        $("#widgetTitle" + id).val($("#widgetTitleOld" + id).val());
        if ($("#saveWidgetEdit" + id).is(":visible")) {
            $("#saveWidgetEdit" + id).css('display', 'none');
            $("#titleWidget" + id).css('display', 'inline-block');
        } else {
            $("#saveWidgetEdit" + id).css('display', 'inline-block');
            $("#titleWidget" + id).css('display', 'none');
        }
    }

    this.ItemNotFound = function () {
        var ResourceNotFound = dict.ResourceNotFound[generalSupport.LanguageName()];
        var ResourceNotFoundDetail = dict.ResourceNotFoundDetail[generalSupport.LanguageName()];
        var result = this.itemNotFound.replace('{{ResourceNotFound}}', ResourceNotFound).replace('{{ResourceNotFoundDetail}}', ResourceNotFoundDetail);
        return result;
    };

    this.createGridStack2 = function (layoutType, items) {
        // Se monta el html de cada widget
        $.each(items, function (index, widgetData) {
            defaultPage.createGridStackWidget(widgetData, layoutType);
        });

        // Llama el componente que crea efectivamente la grid
        $('.grid-stack').gridstack({
            alwaysShowResizeHandle: true,
            disableDrag: true,
            verticalMargin: 10,
            cellHeight: 50
        });

        // Se llama al cambiar cualquier widget de posición
        $('.grid-stack').on('change', defaultPage.onChange);

        // Agrega los eventos a los widgets
        defaultPage.bindingEvents(layoutType);

        // Código temporario hasta que la grid de todos los clientes entén actualizadas
        var widgetsForUpdatePosition = new Array();
        $.each(items, function (index, widgetData) {
            if (!widgetData.X || !widgetData.Height) {
                var widget = $('#' + widgetData.Id);
                widgetsForUpdatePosition.push({
                    id: widgetData.Id,
                    x: widget.data('gs-x'),
                    y: widget.data('gs-y'),
                    width: widget.data('gs-width'),
                    height: widget.data('gs-height')
                });
            }
        });

        // Actualiza las posiciones de los widgets en la página
        if (widgetsForUpdatePosition.length > 0)
            defaultPage.updateWidget(widgetsForUpdatePosition);
    };

    // Consulta los widgets de la página y inicia la creación de la grid de widgets
    this.createGridStack = function (pageId) {
        var layoutType = defaultPage.getLayoutType();
        var items = null;
        if (!pageId || pageId !== "") {
            pageId = $('.metismenu li.active').prop('id');
            if (!pageId) {
                var node = $('.metismenu li').filter(function () { return $(this).attr('id') !== undefined; }).first();
                node.addClass('active');
                pageId = node.attr('id');
            }
        }

        // Si hay una página del menú seleccionada carga los widgets,
        //      eso evita que una url con parámetro de página que no esté seleccionada se cargue los widgets
        if ($('#side-menu li.active').length > 0) {
            var languageId = generalSupport.LanguageId();
            var key = 'Page_' + $('.metismenu li.active').prop('id') + '_' + languageId;
            if (!localStorage.getItem(key)) {
                app.core.Get(constants.fasiApi.fasi + 'WidgetsInPage?pageId=' + pageId + '&languageId=' + languageId,
                    true,
                    true,
                    undefined,
                    null,
                    function (data) {
                        items = data.Data;
                        localStorage.setItem(key, JSON.stringify(data.Data));
                        defaultPage.createGridStack2(layoutType, items);
                    },
                    null
                );
            } else {
                items = JSON.parse(localStorage.getItem(key));
                defaultPage.createGridStack2(layoutType, items);
            }
        }
    };

    this.refreshWidgetInformation = function () {
        pageId = $('.metismenu li.active').prop('id');
        if (!pageId) {
            var node = $('.metismenu li').filter(function () { return $(this).attr('id') !== undefined; }).first();
            node.addClass('active');
            pageId = node.attr('id');
        }
        var languageId = generalSupport.LanguageId();
        var key = 'Page_' + $('.metismenu li.active').prop('id') + '_' + languageId;
        app.core.Get(constants.fasiApi.fasi + 'WidgetsInPage?pageId=' + pageId + '&languageId=' + languageId,
            true,
            true,
            undefined,
            null,
            function (data) {
                items = data.Data;
                localStorage.setItem(key, JSON.stringify(data.Data));
            },
            null
        );
    }


    // Crea el html de un widget con base en el objeto recibido del servidor
    this.createGridStackWidget = function (widgetData, layoutType) {
        // Guarda el original height para el caso del widget no estar expandido
        var originalHeight = widgetData.Height;
        if (!widgetData.Expanded)
            widgetData.Height = 1;

        var item = defaultPage.widgetHtml.replace('{expand-link-style}', layoutType === 1 ? 'display: none;' : '')
            .replace('{x}', widgetData.X ? widgetData.X : 0)
            .replace('{y}', widgetData.Y ? widgetData.Y : 0)
            .replace('{width}', widgetData.Width ? widgetData.Width : constants.gridStackWidth / layoutType)
            .replace('{height}', widgetData.Height ? widgetData.Height : constants.gridStackNodeHeight)
            .replace('{originalHeight}', originalHeight)
            .replace('{autoPosition}', widgetData.X && widgetData.Y ? '' : 'data-gs-auto-position="1"')
            .replace('{collapse-link-icon}', widgetData.Height && widgetData.Height === 1 ? 'fa fa-chevron-down' : 'fa fa-chevron-up')
            .replace('{noResize}', widgetData.Height && widgetData.Height === 1 ? 'data-gs-no-resize="1"' : '')
            .replace('{minHeight}', widgetData.Height && widgetData.Height === 1 ? 1 : 3)
            .replace('{editWidget}', app.user.isAnonymous == false && widgetData.IsAllowedToEditTheTitle == true ? this.editWidget : '')
            .replace('{saveWidgetButton}', app.user.isAnonymous == false && widgetData.IsAllowedToEditTheTitle == true ? this.saveWidgetButton : '')
            .replace(/{id}/gi, widgetData.Id)
            .replace(/{title}/gi, widgetData.Title);
        if (app.user.isAnonymous) {
            item = item.replace("{close}", "");
        } else {
            item = item.replace("{close}", defaultPage.buttonClose.replace('{id}', widgetData.Id));
        }

        $('.grid-stack').append(item);

        // Se carga el contenido dinámico del widget con base en la url
        $.ajax({
            type: "GET",
            url: widgetData.Url,
            contentType: "text/html; charset=utf-8"
        }).done(function (data) {
            try {
                if (data.toLowerCase().indexOf("<!--MarkAsWidget-->".toLocaleLowerCase()) !== -1) {
                    $('#' + widgetData.Id + ' .ibox-content').html(defaultPage.ItemNotFound());
                } else {
                    $('#' + widgetData.Id + ' .ibox-content').html(data);
                    // Si el widget contiene su proprio js con un método llamado "load", este será llamado por acá
                    if (widgetData.Name && window[widgetData.Name + 'Support'] && window[widgetData.Name + 'Support']['load'])
                        window[widgetData.Name + 'Support']['load'](widgetData);
                }
            } catch (e) {
                console.warn('Error loading widget in URL "' + widgetData.Url + '" , error :' + e);
            }
        }).fail(function (jqXHR, textStatus, errorThrown) {
            if (jqXHR.status === 404) {
                $('#' + widgetData.Id + ' .ibox-content').html(defaultPage.ItemNotFound());
            } else {
                console.error("Error al cargar la widget, URL " + widgetData.Url);
            }
        });
    };

    // Agrega los eventos que necesita un widget
    this.bindingEvents = function (layoutType) {
        // Collapse ibox function
        $('.collapse-link').on('click', function () {
            $(this).find('i').toggleClass('fa-chevron-up').toggleClass('fa-chevron-down');

            var gridStack = $('.grid-stack').data('gridstack');
            var isExpanded = this.offsetParent.offsetParent.offsetParent.dataset.gsHeight > 1;

            // Se saca la opción de resize
            gridStack.resizable(this.offsetParent.offsetParent.offsetParent, !isExpanded);
            // Se cambia la altura minima
            gridStack.minHeight(this.offsetParent.offsetParent.offsetParent, !isExpanded ? 3 : 1);

            if (!isExpanded) {
                var height = parseInt($('#' + this.offsetParent.offsetParent.offsetParent.id).data('original-height'));
                gridStack.resize(this.offsetParent.offsetParent.offsetParent, null, height);
            }
            else
                gridStack.resize(this.offsetParent.offsetParent.offsetParent, null, 1);
        });

        // Expand/compress widget
        $('.grid-stack .expand-link').on('click', function () {
            var gridStack = $('.grid-stack').data('gridstack');

            if (this.offsetParent.offsetParent.offsetParent.dataset.gsWidth !== constants.gridStackWidth) {
                gridStack.move(this.offsetParent.offsetParent.offsetParent, 0, null);
                gridStack.resize(this.offsetParent.offsetParent.offsetParent, constants.gridStackWidth, null);
                this.firstChild.className = 'fa fa-compress';
            }
            else {
                gridStack.resize(this.offsetParent.offsetParent.offsetParent, constants.gridStackWidth / layoutType, null);
                this.firstChild.className = 'fa fa-expand';
            }
        });

        // Habilita el evento de mover un widget cuando el mouse ESTÁ sobre su encabezado
        $('.draggable-active').on('mouseover', function () {
            var gridStack = $('.grid-stack').data('gridstack');
            if (gridStack) {
                gridStack.movable(this.offsetParent.offsetParent, true);
            }
        });

        // Inhabilita el evento de mover un widget cuando el mouse NO está sobre su encabezado
        $('.draggable-active').on('mouseout', function () {
            var gridStack = $('.grid-stack').data('gridstack');
            if (gridStack) {
                gridStack.movable(this.offsetParent.offsetParent, false);
            }
        });
    };

    // Se llama al cambiar cualquier widget de posición
    this.onChange = function (event, items) {
        if (typeof items !== 'undefined') {
            var widgetsForUpdate = new Array();
            $.each(items, function (index, widget) {
                var widgetId = widget.el.prop('id');
                var isExpanded = widget.height > 1;

                if (isExpanded)
                    $('#' + widgetId).data('original-height', widget.height);

                var param = {
                    id: widgetId,
                    x: widget.x,
                    y: widget.y,
                    expanded: isExpanded,
                    width: widget.width,
                    height: $('#' + widgetId).data('original-height')
                };
                widgetsForUpdate.push(param);
            });
            defaultPage.updateWidget(widgetsForUpdate);
        }
    };

    this.updateWidgetInstanceTitle = function (id) {
        title = $("#widgetTitle" + id).val();
        if ($.trim(title) !== "") {
            ajaxJsonHelper.post(constants.fasiApi.fasi + 'UpdateWidgetInstanceTitle?id=' + id + "&title=" + title + "&languageId=" + generalSupport.LanguageId(), "", null,
                function (qXHR, textStatus, errorThrown) {
                    generalSupport.ErrorHandler(qXHR, textStatus, errorThrown);
                }, null, false);
            $("#widgetTitleOld" + id).val(title);
            $("#titleWidget" + id).html("<h5>" + title + "</h5>");
            this.refreshWidgetInformation();
        } else {
            notification.swal.warning('', generalSupport.ResourceByKey("UndefinedTitleWidget"));
        }
        $("#saveWidgetEdit" + id).css('display', 'none');
        $("#titleWidget" + id).css('display', 'inline-block');
    }

    // Restablece los widgets por tipo de layout
    this.resetGridStackColumns = function (layoutType) {
        var gridStack = $('.grid-stack').data('gridstack');
        var nodes = gridStack.grid.nodes;

        var x = 0;
        var y = 0;
        var widgetsForUpdate = new Array();
        $.each(nodes, function (index, widget) {
            var widgetId = widget.el.prop('id');

            var param = {
                id: widgetId,
                x: constants.gridStackWidth / layoutType * x,
                y: y,
                expanded: true,
                width: constants.gridStackWidth / layoutType,
                height: constants.gridStackNodeHeight
            };
            widgetsForUpdate.push(param);

            x += 1;
            if (constants.gridStackWidth / layoutType * x >= constants.gridStackWidth) {
                x = 0;
                y += constants.gridStackNodeHeight;
            }
        });
        defaultPage.updateWidget(widgetsForUpdate);
    };

    // Agrega un widget a la stack de widgets
    this.addWidget = function (widgetData) {
        var gridStack = $('.grid-stack').data('gridstack');
        var layoutType = defaultPage.getLayoutType();

        defaultPage.createGridStackWidget(widgetData, layoutType);
        gridStack.addWidget($('#' + widgetData.Id), 0, 0, constants.gridStackWidth / layoutType, constants.gridStackNodeHeight, true);

        defaultPage.bindingEvents(layoutType);
    };

    // Actualiza las posiciones de los widgets
    this.updateWidget = function (widgetsForUpdate) {
        ajaxJsonHelper.post(constants.fasiApi.fasi + 'WidgetsInstanceChange', JSON.stringify(widgetsForUpdate), null,
            function (qXHR, textStatus, errorThrown) {
                generalSupport.ErrorHandler(qXHR, textStatus, errorThrown);
            });
    };

    // Elimina un widget de la stack de widgets y del servidor
    this.removeWidget = function (idWidget) {
        $.LoadingOverlay("show");
        ajaxJsonHelper.delete(constants.fasiApi.fasi + 'WidgetInstanceRemove?id=' + idWidget, null,
            function (data) {
                $.LoadingOverlay("hide");

                var gridStack = $('.grid-stack').data('gridstack');
                var key = 'Page_' + $('.metismenu li.active').prop('id') + '_' + generalSupport.LanguageId();
                localStorage.removeItem(key);
                gridStack.removeWidget($('#' + idWidget));

                toastr.success(dict.DeleteWidgetSuccess[generalSupport.LanguageName()], '', { positionClass: "toast-bottom-right" });
            },
            function (qXHR, textStatus, errorThrown) {
                $.LoadingOverlay("hide");
                generalSupport.ErrorHandler(qXHR, textStatus, errorThrown);
            });
    };

    // Obtiene el tipo de layout que está selecciona para la página actual
    this.getLayoutType = function () {
        var layoutType = parseInt($('.metismenu li.active').data('layout'));

        if (layoutType < 1 || layoutType > 3 || !layoutType)
            layoutType = 3;

        return layoutType;
    };

    // Inicia el modal con la lista de widgets disponibles
    this.Init = function () {
        var widgetsModal = $('#widgetsModal');
        widgetsModal.on('shown.bs.modal', function () {
            var AddWidgetsTitle = generalSupport.ResourceByKey("AddWidgetsTitle");
            $('#AddWidgetsTitle').html(AddWidgetsTitle);
            var modalBody = $(this).find('.modal-body');
            if (modalBody.html().trim() === "")
                modalBody.load(constants.availableWidgetsPopup);
        });
    };
};

$(document).ready(function () {
    masterSupport.Init();
    defaultPage.Init();
});