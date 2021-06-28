var tableHelperSupport = new function () {
    this.EditCommandFormatter = function (value, row) {
        return '<a href="javascript:void(0)" class="update edit" data-modal-title="Editar" title="Haga click para editar">' + value + '</a>';
    };

    this.EditCommandOnlyDateFormatter = function (value, row, index) {
        var newValue = "";

        if (value === 0 || value === "") {
            newValue = '';
        } else {
            newValue = generalSupport.ToJavaScriptDateCustom(value, generalSupport.DateFormat());
        }

        return '<a href="javascript:void(0)" class="update edit" data-modal-title="Editar" title="Haga click para editar">' + newValue + '</a>';
    };

    this.EditCommandOnlyDateFormatterWithHoursMinutesSeconds = function (value, row, index) {
        var newValue = "";

        if (value === 0 || value === "") {
            newValue = '';
        } else {
            newValue = generalSupport.ToJavaScriptDateCustom(value, generalSupport.DateFormat() + ' HH:mm:ss');
        }

        return '<a href="javascript:void(0)" class="update edit" data-modal-title="Editar" title="Haga click para editar">' + newValue + '</a>';
    };

    this.DateFormatter = function (value, row, index) {
        if (value === 0 || value === "") {
            return value = '';
        } else {
            return generalSupport.ToJavaScriptDateCustom(value, generalSupport.DateFormat() + ' hh:mma');
        }
    };

    this.OnlyDateFormatter = function (value, row, index) {
        if (value === 0 || value === "") {
            return value = '';
        } else {
            return generalSupport.ToJavaScriptDateCustom(value, generalSupport.DateFormat());
        }
    };

    this.OnlyDateFormatterWithHoursMinutesSeconds = function (value, row, index) {
        if (value === 0 || value === "") {
            return value = '';
        } else {
            return generalSupport.ToJavaScriptDateCustom(value, generalSupport.DateFormat() + ' HH:mm:ss');
        }
    };

    this.IsCheck = function (value, row, index) {
        var icon = "";

        if (typeof value === "string") {
            if (value === "2") {
                icon = "glyphicon glyphicon-unchecked";
            } else {
                icon = "glyphicon glyphicon-check";
            }
        } else {
            if (typeof value === "boolean") {
                if (value === false) {
                    icon = "glyphicon glyphicon-unchecked";
                } else {
                    icon = "glyphicon glyphicon-check";
                }
            }
        }

        return '<div class="text-center" >' +
            '<i class="' + icon + '"></i>' +
            '</div>';
    };

    this.IsContextMenu = function (value, row, index) {
        var valueHtml = '<div class="row-fluid"> ' +
            '<label class="control-label">' + value + ' </label> ' +
            '<span class="caret folders"></span> ' +
            '</div>';

        return valueHtml;
    };

    this.DownloadIcon = function (value, row) {
        return '<i class="fa fa-download"></i>';
    };

    this.IsContextMenu = function (value, row, index, field) {
        return '<div class="row-fluid">' +
            '<label class="control-label">' + value + ' </label> ' +
            '<span class="caret menu-' + field + '"></span>' +
            '</div>';
    };

    /**
     * Realiza una traducción de la columnas del grid.
     * @param {String} nameGrid Es el nombre del grid a traducir las columnas.
     * @param {String} keyTranstale Es el nombre para buscar en el json.
     */
    this.Translate = function (nameGrid, keyTranstale) {
        tableColumn = $(nameGrid).find('thead > tr > th').get();
        var gridCaption = $.i18n.t('app.form.' + keyTranstale.replace('#', "").replace("Tbl", "") + '_Title');
        if (gridCaption !== "") {
            //if ($(nameGrid).find('Caption').get().length == 0) {
            //    $('a[data-toggle="tab"]').on('shown.bs.tab', { nameGrid: nameGrid, key: keyTranstale }, function (e) {
            //        var gridCaption = $.i18n.t('app.form.' + e.data.key.replace('#', "").replace("Tbl", "") + '_Title');
            //        $(e.data.nameGrid).find('Caption').get()[0].innerHTML = gridCaption;
            //    });
            //} else {
                $(nameGrid).find('Caption').get()[0].innerHTML = gridCaption;
            //}
        }
        $.each(tableColumn, function (key, val) {
            var field = val.dataset.field;
            if (field !== 'selected') {
                $.each(val.children, function (key, valTitle) {
                    if (valTitle.classList.contains("th-inner")) {
                        valTitle.innerHTML = $.i18n.t('app.form.' + keyTranstale.replace('#',"") + '_' + field + '_Title');
                    }
                });
            }
        });
    };

    /**
     * Función que permite realizar la validación de varios campos que componen una regla.
     * @param {any} array Arreglo que representa los campos de una tabla.
     * @param {any} fields Campos unidimensionales a contrastar con los valores almacenados en la tabla.
     * @param {any} rowKey Identificación única de la fila a validar.
     */
    this.ObjectFindByKey = function (array, fields, rowKey) {
        for (var i = 0; i < array.length; i++) {
            var exists = false;
            if (array[i]['Unique_Key'] != rowKey) {
                for (var y = 0; y < fields.length; y++) {
                    if (array[i][fields[y].field] == fields[y].value) {
                        exists = true;
                        break;
                    }
                    else
                        exists = false;
                    if (exists && (y == fields.length - 1) && array[i][fields[y - 1].field] != fields[y - 1].value)
                        exists = false;
                }
            }
            if (exists)
                return true;
        }
        return false;
    }

    /**
     * Función que permite validar campos únicos compuestos.
     * @param {any} grid Identificador del control de tipo Table principal.
     * @param {any} gridEditForm Identificador del control de tipo Table de edición.
     * @param {any} rowKey Identificación única de la fila a validar.
     */
    this.MultipleKeyColumnValidate = function (grid, gridEditForm, rowKey) {

        var fields = [];
        gridEditForm.children().find('.multi-validate').each(function (index, el) {
            if (el.nodeName.toLowerCase() === 'select')
                fields.push({ field: el.id, value: $(el).find("option:selected").val() })
            else
                fields.push({ field: el.id, value: el.value })
        });
        var data = grid.bootstrapTable('getData');
        var result = false;
        result = this.ObjectFindByKey(data, fields, rowKey);

        return !result;
    };

    /**
     * Función que permite validar los campos únicos en los grid's.
     * @param {any} grid Identificador del control de tipo table.
     * @param {number} index Valor de la columna del grid a ser evaluada comenzando desde 0.
     * @param {any} value Valor a buscar en cada una de las celdas de la columna del grid.
     * @param {any} element Elemento del popup que posee la validación.
     * @param {any} popup Identificador del popup para agregar o editar.
     * @returns {boolean} Si el valor es encontrado la función retorna falso, en caso contrario verdadero.
     */
    this.UniqueColumnValidate = function (grid, index, value, element, popup) {
        var result = true;
        if (element.nodeName.toLowerCase() === 'select') {
            value = $(element).find("option:selected").text();
        }
        if (popup.data('id')) {
            var tableTr = $('table' + grid.selector + ' tbody tr').filter(function (idx, el) {
                if (popup.data('id') != $(el).attr("data-uniqueid"))
                    return $(this);
            });
            $(tableTr).find('td:eq(' + index + ')').each(function () {
                if ($(this).text().toLowerCase() == value.toLowerCase())
                    result = false;
            });
        } else {
            $('table' + grid.selector + ' tbody tr').find('td:eq(' + index + ')').each(function () {
                if ($(this).text().toLowerCase() == value.toLowerCase())
                    result = false;
            });
        }
        return result;
    };
};