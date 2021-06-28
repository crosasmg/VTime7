if (navigator.geolocation) {
    navigator.geolocation.getCurrentPosition(success);
} else {
    alert("There is Some Problem on your current browser to get Geo Location!");
}

function OnBirthdayValidation(s, e) {
    var birthday = e.value;
    if (!birthday)
        return;
    var today = new Date();
    var msecPerYear = 1000 * 60 * 60 * 24 * 365;
    var years = (today.getTime() - birthday.getTime()) / msecPerYear;
    if (years < 14) {
        e.isValid = false;
        e.errorText = "You should be at least 14 years old.";
    }
}

function success(position) {
    //var lat = position.coords.latitude;
    //var long = position.coords.longitude;
    //var city = position.coords.locality;
    //var LatLng = new google.maps.LatLng(lat, long);
    //var mapOptions = {
    //    mapTypeControl: false,
    //    center: LatLng,
    //    zoom: 12,
    //    mapTypeId: google.maps.MapTypeId.ROADMAP
    //};

    //var map = new google.maps.Map(document.getElementById("mapa"), mapOptions);
    //var marker = new google.maps.Marker({
    //    position: LatLng,
    //    title: "<div style = 'height:60px;width:200px'><b>Ubicación actual aproximada</b></div>"
    //});

    //marker.setMap(map);
    //var getInfoWindow = new google.maps.InfoWindow({ content: "<b>Ubicación actual aproximada</b>"
    //});
    //getInfoWindow.open(map, marker);
}

var lastCountry = null;

//select of the zipcode
function SelectedIndexChangedPostalCodeChanged(ddlPostalCode) {
    ddlPostalCode.PerformCallback(ddlPostalCode.GetValue().toString());
}

function TextChangedddlPostalCodeChanged(ddlPostalCode) {
    ddlPostalCode.PerformCallback(ddlPostalCode.GetText().toString());
}

function ValidationTypePhyTicalAddress(s, e) {
    if (s.GetItemCount() > 0) {
        if (s.GetSelectedItem().value == -1) {
            e.isValid = false;
            e.errorText = "Este campo es obligatorio.";
        }
    }
}

function ValidationZipCode(s, e) {
    var value = s.GetSelectedItem().value;
    if (value == "" || value == null) {
        e.isValid = false;
        e.errorText = 'Este campo es obligatorio.';
    }
}

function ValidationLastContact(s, e) {
    var value = s.GetValue();
    if (value == "" || value == null) {
        e.isValid = false;
        e.errorText = 'Este campo es obligatorio.';
    }
}

function ValidationInitialYear(s, e) {
    var value = s.GetValue();
    if (value == "" || value == null) {
        e.isValid = false;
        e.errorText = 'Este campo es obligatorio.';
    }
}

function ValidationTypeRoute(s, e) {
    if (s.GetItemCount() > 0) {
        if (s.GetSelectedItem().value == -1) {
            e.isValid = false;
            e.errorText = "Este campo es obligatorio.";
        }
    }
}

function ValidationTimeZone(s, e) {
    if (s.GetItemCount() > 0) {
        if (s.GetSelectedItem().value == -1) {
            e.isValid = false;
            e.errorText = "Este campo es obligatorio.";
        }
    }
}

function ValidationGeneric(s, e) {
    var value = s.GetValue();
    if (value == "" || value == null) {
        e.isValid = false;
        e.errorText = 'Este campo es obligatorio.';
    }
}

function GetValue(value) {
    ddlPostalCode.PerformCallback(value);
    $('#hfTransactionValue').val()
}

function OnTextBoxInit(s, e) {
    $(s.GetInputElement()).autocomplete({
        source: function (request, response) {
            var value = s.GetValue();
            return GetValue(value);
        },
        position:
                {
                    my: "left top",
                    at: "left bottom",
                    of: s.GetMainElement()
                },
        select: function (event, ui) {
            s.SetValue(ui.item.value);
        }
    });
}

var month = "Jan";
function OnClick(s, e) {
    s.Focus();
    month = s.GetText();
}
function OnPrevClick(s, e) {
    lblYear.SetText(parseInt(lblYear.GetText()) - 1);
    month = "Jan";
}
function OnNextClick(s, e) {
    lblYear.SetText(parseInt(lblYear.GetText()) + 1);
    month = "Jan";
}
function OnOkClick(s, e) {
    dde.SetText(lblYear.GetText() + "-" + month);
    dde.HideDropDown();
}

function generic_SelectedIndexChanged(s, e, name) {
    CallbackPanel.PerformCallback(ddlCountry.GetSelectedItem().value + ',' + s.GetSelectedItem().value + ',' + name + "," + s.name);
}

function ddlTypeRoute_SelectedIndex(s, e) {
    var defaultIndex = s.GetSelectedIndex();
    var itemFound = s.FindItemByValue(s.GetSelectedItem().value);
    if (itemFound != null) {
        defaultIndex = itemFound.index;
    }
    s.SetSelectedIndex(defaultIndex);
    CallbackPanelTypeRoutePart.PerformCallback(ddlCountry.GetSelectedItem().value + ',' + s.GetSelectedItem().value);
}

function genericTextBox_SelectedUpdateValueIndexChanged(s, e, prefix, nameSource) {
    var textSource = s.GetText();
    var param = { nameSource: nameSource, textSource: textSource };
    var paramVector = JSON.stringify(param);
    var urlBase = window.location.protocol + '//' + window.location.host + '/Controls/AddressMethodWeb.aspx/UpdateValuesCacheTextBox';
    $.ajax({
        url: urlBase,
        data: paramVector,
        dataType: "json",
        type: "POST",
        contentType: "application/json; charset=utf-8",
        dataFilter: function (data) { return data; },
        success: function (data) {
            if (data.d == true) {
                var da = data.d;
            } else {
            }
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            alert(textStatus);
        }
    });
}

function genericComboBox_SelectedUpdateValueIndexChanged(s, e, prefix, nameSource) {
    var valueSource = s.GetSelectedItem().value;
    var textSource = s.GetSelectedItem().text;
    var indexSource = s.GetSelectedIndex();
    var param = { nameSource: nameSource, valueSource: valueSource, textSource: textSource, indexSource: indexSource, prefix: prefix };
    var paramVector = JSON.stringify(param);
    var urlBase = window.location.protocol + '//' + window.location.host + '/Controls/AddressMethodWeb.aspx/UpdateValuesCacheComboBox';
    $.ajax({
        url: urlBase,
        data: paramVector,
        dataType: "json",
        type: "POST",
        contentType: "application/json; charset=utf-8",
        dataFilter: function (data) { return data; },
        success: function (data) {
            if (data.d == true) {
                var da = data.d;
            } else {
            }
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            alert(textStatus);
        }
    });
}

function generic_Init_Combobox(s, e, prefix, nameSource, index) {
    //    ASPxClientUtils.AttachEventToElement(s, 'Validation', function (evt) {
    //        ValidationGeneric(s, e);
    //    });
    s.customValidationEnabled = true;
    if (s.GetItemCount() > 0) {
        if (index != -1) {
            if (s.GetItemCount() >= index) {
                s.SetSelectedIndex(index);
            }
        }
    }
}

function generic_Init_Textbox(s, e, prefix, nameSource, description) {
    if (description != null && description != "") {
        s.SetText(description);
        genericTextBox_SelectedUpdateValueIndexChanged(s, e, prefix, nameSource)
    }
}

function generic_Init(s, e, prefix, nameSource, level, count, children, code, description) {
    if (s.GetItemCount() > 0) {
        if (s.GetSelectedItem().value != -1) {
            var defaultIndex = -1;
            if (code != null && code != "") {
                geographicZoneIdField = code;
                var itemFound = s.FindItemByValue(code);
                if (itemFound != null) {
                    defaultIndex = itemFound.index;
                }
            }
            else {
                code = "";
                description = "";
            }
            if (defaultIndex == -1) {
                s.SetSelectedIndex(0);
            }

            else {
                s.SetSelectedIndex(defaultIndex);
            }

            if (s != null) {
                generic_SelectedIndexChangedQuery(s, e, prefix, nameSource, level, count, children, true, code, description);
            }
        }
    }
}

function generic_SelectedIndexChangedQuery(s, e, prefix, nameSource, level, count, children, auto, code, description) {
    if (s != null) {
        var isFirst = $('#hfReload').val();
        if (level <= count) {
            var defaultIndex = -1;
            var defaultIndexGrandChildren = -1;

            var countryCodeField = ddlCountry.GetSelectedItem().value;
            var geographicZoneIdField = s.GetSelectedItem().value;
            var geographicZoneLevelIdField = level;

            //            if (isFirst != '1') {
            //                code = "";
            //            }

            if (code != null && code != "") {
                geographicZoneIdField = code;
                var itemFound = s.FindItemByValue(code);
                if (itemFound != null) {
                    defaultIndex = itemFound.index;
                }
            }
            else {
                code = "";
                description = "";
            }

            var valueSource = s.GetSelectedItem().value;
            var textSource = s.GetSelectedItem().text;
            var indexSource = s.GetSelectedIndex();

            var param = { countryCode: countryCodeField, geographicZoneLevelId: geographicZoneLevelIdField, geographicZoneId: geographicZoneIdField, nameSource: nameSource, valueSource: valueSource, textSource: textSource, indexSource: level };
            var paramVector = JSON.stringify(param);
            var urlBase = window.location.protocol + '//' + window.location.host + '/Controls/AddressMethodWeb.aspx/GetListLookUpPossibleValuesAllowedForGeographicZone';
            $.ajax({
                url: urlBase,
                data: paramVector,
                dataType: "json",
                type: "POST",
                contentType: "application/json; charset=utf-8",
                dataFilter: function (data) { return data; },
                success: function (data) {
                    if (data.d.length > 0) {
                        var length = data.d.length;
                        if (level != count) {
                            children.ClearItems();
                            children.BeginUpdate();
                            for (var i = 0; i < length; i++) {
                                children.AddItem(data.d[i].Description, data.d[i].Code);
                                if (data.d[i].ShortDescription.split(',').length > 1) {
                                    defaultIndexGrandChildren = i;
                                }
                            }
                            children.EndUpdate();
                            if (defaultIndex == -1) {
                                children.SetSelectedIndex(0);
                            }

                            else {
                                var defaultValues = data.d[defaultIndexGrandChildren].ShortDescription;
                                if (defaultValues != null || defaultValues != "") {
                                    code = defaultValues.split(',')[0];
                                    description = defaultValues.split(',')[1];
                                }
                                children.SetSelectedIndex(defaultIndexGrandChildren);
                            }
                        }
                        if (auto == true) {
                            var grandChildren = ASPxClientControl.GetControlCollection().GetByName(prefix + (level + 2))
                            children = ASPxClientControl.GetControlCollection().GetByName(prefix + (level + 1))
                            generic_SelectedIndexChangedQuery(children, e, prefix, (prefix + (level + 1)), (level + 1), count, grandChildren, auto, code, description);
                        }
                        if (level == (count - 1)) {
                            $('#hfReload').val('0');
                        }
                    } else {
                        if (children != null) {
                            children.ClearItems();
                            children.AddItem('No records found', -1);
                        }
                    }
                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    if (children != null) {
                        children.ClearItems();
                    }
                    alert(textStatus);
                }
            });
        }
    }
}

function ddlCountry_SelectedIndexChanged(s, e) {
    var value = s.GetSelectedItem().value;
    $('#hfCountryCodeDefault').val(value);
    InitialPartAddress();
    CallbackPanel.PerformCallback(value);
}

function InitialPartAddress() {
    cbxGeographicZone1.SetSelectedIndex(-1);
}
function OnTextBoxInitCountry(s, e) {
    var countryCodeDefault = $("#hfCountryCodeDefault").val();
    var urlBase = window.location.protocol + '//' + window.location.host + '/Controls/AddressMethodWeb.aspx/GetListLookUpPossibleValuesOfCountryTable';
    var index;
    $.ajax({
        url: urlBase,
        dataType: "json",
        type: "POST",
        contentType: "application/json; charset=utf-8",
        dataFilter: function (data) { return data; },
        success: function (data) {
            if (data.d.length > 0) {
                var count = data.d.length;
                s.ClearItems();
                s.BeginUpdate();
                for (var i = 0; i < count; i++) {
                    s.AddItem(data.d[i].Description, data.d[i].Code);
                    if (data.d[i].Code == countryCodeDefault) {
                        index = i;
                    }
                }
                s.EndUpdate();
                s.SetSelectedIndex(index);
                CallbackPanel.PerformCallback(countryCodeDefault);
            } else {
                response([{ label: 'No records found.', id: -1}]);
            }
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            alert(textStatus);
        }
    });
}

function OnTextBoxInitTypePhyTicalAddress(s, e) {
    var countryCodeDefault = $("#hfCountryCodeDefault").val();
    var urlBase = window.location.protocol + '//' + window.location.host + '/Controls/AddressMethodWeb.aspx/GetListLookUpPossibleValuesOfTypeOfPhysicalAddressTable';
    $.ajax({
        url: urlBase,
        dataType: "json",
        type: "POST",
        contentType: "application/json; charset=utf-8",
        dataFilter: function (data) { return data; },
        success: function (data) {
            if (data.d.length > 0) {
                var count = data.d.length;
                s.ClearItems();
                s.BeginUpdate();
                for (var i = 0; i < count; i++) {
                    s.AddItem(data.d[i].Description, data.d[i].Code);
                }

                s.EndUpdate();
                s.SetSelectedIndex(0);
            } else {
                response([{ label: 'No results found.', id: -1}]);
            }
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            alert(textStatus);
        }
    });
}

function OnTextBoxInitTypeRoute(s, e) {
    var countryCodeDefault = $("#hfCountryCodeDefault").val();
    var urlBase = window.location.protocol + '//' + window.location.host + '/Controls/AddressMethodWeb.aspx/GetListLookUpPossibleValuesOfTypeOfRouteTable';
    $.ajax({
        url: urlBase,
        dataType: "json",
        type: "POST",
        contentType: "application/json; charset=utf-8",
        dataFilter: function (data) { return data; },
        success: function (data) {
            if (data.d.length > 0) {
                var count = data.d.length;
                s.ClearItems();
                s.BeginUpdate();
                for (var i = 0; i < count; i++) {
                    s.AddItem(data.d[i].Description, data.d[i].Code);
                }
                s.SetSelectedIndex(0);
                s.EndUpdate();
                CallbackPanelTypeRoutePart.PerformCallback(countryCodeDefault + ',' + s.GetSelectedItem().value);
            } else {
                response([{ label: 'No results found.', id: -1}]);
            }
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            alert(textStatus);
        }
    });
}

function OnTextBoxInitTimeZone(s, e) {
    var countryCodeDefault = $("#hfCountryCodeDefault").val();
    var urlBase = window.location.protocol + '//' + window.location.host + '/Controls/AddressMethodWeb.aspx/GetLookUpPossibleValuesOfTimeZoneTable';
    $.ajax({
        url: urlBase,
        dataType: "json",
        type: "POST",
        contentType: "application/json; charset=utf-8",
        dataFilter: function (data) { return data; },
        success: function (data) {
            if (data.d.length > 0) {
                var count = data.d.length;
                s.ClearItems();
                s.BeginUpdate();
                for (var i = 0; i < count; i++) {
                    s.AddItem(data.d[i].Description, data.d[i].Code);
                }
                s.SetSelectedIndex(0);
                s.EndUpdate();
            } else {
                response([{ label: 'No results found.', id: -1}]);
            }
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            alert(textStatus);
        }
    });
}

//new event

function Init_PartRoute(s, e, value) {
    if (s.GetItemCount() > 0) {
        if (index != -1) {
            if (s.GetItemCount() >= index) {
                s.SetSelectedIndex(index);
            }
        }
    }
}

function Init_GeographicZoneFirst(s, e, defaultValue, children, defaultValueChildren, level, count) {
    if (level < count) {
        var countryCodeField = ddlCountry.GetSelectedItem().value;
        var geographicZoneIdField = defaultValue;
        var geographicZoneLevelIdField = level;
        var defaultIndex = -1;
        var param = { countryCode: countryCodeField };
        var paramVector = JSON.stringify(param);
        var urlBase = window.location.protocol + '//' + window.location.host + '/Controls/AddressMethodWeb.aspx/GetListLookUpPossibleValuesOfGeographicZoneTableByLevel';

        $.ajax({
            url: urlBase,
            data: paramVector,
            dataType: "json",
            type: "POST",
            contentType: "application/json; charset=utf-8",
            dataFilter: function (data) { return data; },
            success: function (data) {
                if (data.d.length > 0) {
                    var length = data.d.length;
                    if (level != count) {
                        s.ClearItems();
                        s.BeginUpdate();
                        for (var i = 0; i < length; i++) {
                            s.AddItem(data.d[i].Description, data.d[i].Code);
                        }
                        s.EndUpdate();
                        var itemFound = s.FindItemByValue(defaultValue);
                        if (itemFound != null) {
                            defaultIndex = itemFound.index;
                        }

                        if (defaultIndex == -1) {
                            s.SetSelectedIndex(0);
                        }

                        else {
                            s.SetSelectedIndex(defaultIndex);
                        }
                    }
                } else {
                    if (children != null) {
                        s.ClearItems();
                        s.AddItem('No records found', -1);
                    }
                }
            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                if (s != null) {
                    s.ClearItems();
                }
                alert(textStatus);
            }
        });
    }
}

function Init_GeographicZone(s, e, defaultValue, children, defaultValueChildren, level, count) {
    if (level < count) {
        var countryCodeField = ddlCountry.GetSelectedItem().value;
        var geographicZoneIdField = defaultValue;
        var geographicZoneLevelIdField = level;
        var defaultIndex = -1;
        var param = { countryCode: countryCodeField, geographicZoneLevelId: geographicZoneLevelIdField, geographicZoneId: geographicZoneIdField };
        var paramVector = JSON.stringify(param);
        var urlBase = window.location.protocol + '//' + window.location.host + '/Controls/AddressMethodWeb.aspx/GetListLookUpPossibleValuesAllowedForGeographicZone';

        $.ajax({
            url: urlBase,
            data: paramVector,
            dataType: "json",
            type: "POST",
            contentType: "application/json; charset=utf-8",
            dataFilter: function (data) { return data; },
            success: function (data) {
                if (data.d.length > 0) {
                    var length = data.d.length;
                    if (level != count) {
                        s.ClearItems();
                        s.BeginUpdate();
                        for (var i = 0; i < length; i++) {
                            s.AddItem(data.d[i].Description, data.d[i].Code);
                        }
                        s.EndUpdate();
                        var itemFound = s.FindItemByValue(defaultValueChildren);
                        if (itemFound != null) {
                            defaultIndex = itemFound.index;
                        }

                        if (defaultIndex == -1) {
                            s.SetSelectedIndex(0);
                        }

                        else {
                            s.SetSelectedIndex(defaultIndex);
                        }
                    }
                } else {
                    if (children != null) {
                        s.ClearItems();
                        s.AddItem('No records found', -1);
                    }
                }
            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                if (s != null) {
                    s.ClearItems();
                }
                alert(textStatus);
            }
        });
    }
}

function Init_GeographicZoneNew(s, e, defaultParent, defaultChildren, level) {
    var countryCodeField = $("#hfCountryCodeDefault").val();
    var geographicZoneIdField = defaultParent;
    var geographicZoneLevelIdField = level;
    var defaultIndex = -1;
    var param = { countryCode: countryCodeField, geographicZoneLevelId: geographicZoneLevelIdField, geographicZoneId: geographicZoneIdField };
    var paramVector = JSON.stringify(param);
    var urlBase = window.location.protocol + '//' + window.location.host + '/Controls/AddressMethodWeb.aspx/GetListLookUpPossibleValuesAllowedForGeographicZone';

    $.ajax({
        url: urlBase,
        data: paramVector,
        dataType: "json",
        type: "POST",
        contentType: "application/json; charset=utf-8",
        dataFilter: function (data) { return data; },
        success: function (data) {
            if (data.d.length > 0) {
                var length = data.d.length;
                s.ClearItems();
                s.BeginUpdate();
                for (var i = 0; i < length; i++) {
                    s.AddItem(data.d[i].Description, data.d[i].Code);
                }
                s.EndUpdate();
                var itemFound = s.FindItemByValue(defaultChildren);
                if (itemFound != null) {
                    defaultIndex = itemFound.index;
                }

                if (defaultIndex == -1) {
                    s.SetSelectedIndex(0);
                }

                else {
                    s.SetSelectedIndex(defaultIndex);
                }
            } else {
                if (children != null) {
                    s.ClearItems();
                    s.AddItem('No records found', -1);
                }
            }
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            if (s != null) {
                s.ClearItems();
            }
            alert(textStatus);
        }
    });
}

//Validations

function ValidationcbxPartRoute(s, e) {
    if (s.GetItemCount() > 0) {
        if (s.GetSelectedItem().value == -1) {
            e.isValid = false;
            e.errorText = "Este campo es obligatorio.";
        }
    }
}

function ValidationcbxGeographicZone(s, e) {
    if (s.GetItemCount() > 0) {
        if (s.GetSelectedItem().value == -1) {
            e.isValid = false;
            e.errorText = "Este campo es obligatorio.";
        }
    }
}

function ValidationtxtPartRoute(s, e) {
    if (s.GetText() == "") {
        e.isValid = false;
        e.errorText = "Este campo es obligatorio.";
    }
}

function GeographicZone_SelectedIndexChanged(s, e, children, level) {
    if (s != null) {
        if (s.GetSelectedItem().value != -1) {
            var valueSource = s.GetSelectedItem().value;
            var countryCodeField = ddlCountry.GetSelectedItem().value;

            var param = { countryCode: countryCodeField, geographicZoneLevelId: level, geographicZoneId: valueSource };
            var paramVector = JSON.stringify(param);
            var urlBase = window.location.protocol + '//' + window.location.host + '/Controls/AddressMethodWeb.aspx/GetListLookUpPossibleValuesAllowedForGeographicZone';

            $.ajax({
                url: urlBase,
                data: paramVector,
                dataType: "json",
                type: "POST",
                contentType: "application/json; charset=utf-8",
                dataFilter: function (data) { return data; },
                success: function (data) {
                    if (data.d.length > 0) {
                        var length = data.d.length;
                        children.ClearItems();
                        children.BeginUpdate();
                        for (var i = 0; i < length; i++) {
                            children.AddItem(data.d[i].Description, data.d[i].Code);
                        }
                        children.EndUpdate();
                        children.SetSelectedIndex(0);
                    } else {
                        if (children != null) {
                            children.ClearItems();
                            children.AddItem('No records found', -1);
                        }
                    }
                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    if (children != null) {
                        children.ClearItems();
                    }
                    alert(textStatus);
                }
            });
        }
    }
}

function PostCallBackback() {
    CallbackPanelTypeRoutePart.PerformCallback(ddlCountry.GetSelectedItem().value + ',' + s.GetSelectedItem().value);
    CallbackPanel.PerformCallback(ddlCountry.GetSelectedItem().value + ',' + s.GetSelectedItem().value + ',' + name + "," + s.name);
}