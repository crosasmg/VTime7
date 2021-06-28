var localAddressesDTO = [];
var localPhonesDTO = [];

var AddressSupport = new function () {
    var urlService = "";
	var zipCodeNumeric = undefined;
	// var autoNumericOptions = {
		// decimalCharacter: generalSupport.DecimalCharacter(),
		// digitGroupSeparator: '',
		// maximumValue: 9999999999,
		// decimalPlaces: 0,
		// minimumValue: -9999999999
	// };
	
    this.localBestTimeToCall = "", this.localPhoneTypes = "", this.postBackCounter = 0, this.defaultAddressGets = {};
    
    // Gets the address enpoint.
    this.GetEndpoint = function () {
        return $.ajax({
            type: "POST",
            url: "/fasi/wmethods/General.aspx/SettingValue",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
			data: JSON.stringify({ "name": "Address.URL" }),
            success: function (data) {
                urlService = data.d;
            }
        });
    }

    // Gets the address enpoint.
    this.GetDefaultCountrySetting = function () {
        return $.ajax({
            type: "POST",
            url: "/fasi/wmethods/General.aspx/SettingValue",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
			data: JSON.stringify({ "name": "CountryCode" })
        });
    }
    
    // Gets the address enpoint.
    this.GetShowMapOnControlSetting = function () {
        return $.ajax({
            type: "POST",
            url: "/fasi/wmethods/General.aspx/SettingValue",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
			data: JSON.stringify({ "name": "ShowMapOnAddressService" })
        });
    }

    // Gets all the phone types.
    this.GetPhoneTypes = function () {
        return $.ajax({
            type: "GET",
            url: urlService + "phonetypes/get",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            crossDomain: true,
            headers: {
                'Accept-Language': generalSupport.LanguageName()
            },
            beforeSend: function (xhr) {
                xhr.setRequestHeader('Authorization', 'Bearer ' + generalSupport.user.token);
            },
            success: function (data) {
                AddressSupport.localPhoneTypes = AddressSupport.CreateOptionsForSelect(data);
            }
        });
    }

    // Gets all the best time to call.
    this.GetBestTimeToCall = function () {
        return $.ajax({
            type: "GET",
            url: urlService + "besttimetocall/get",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            crossDomain: true,
            headers: {
                'Accept-Language': generalSupport.LanguageName()
            },
            beforeSend: function (xhr) {
                xhr.setRequestHeader('Authorization', 'Bearer ' + generalSupport.user.token);
            },
            success: function (data) {
                AddressSupport.localBestTimeToCall = AddressSupport.CreateOptionsForSelect(data);
            }
        });
    }

    // Gets all the phones.
    this.GetPhones = function (addressKey, phoneKey) {
        return $.ajax({
            type: "GET",
            url: urlService + "phones/get?addressKey=" + addressKey + "&phoneKey=" + phoneKey,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            crossDomain: true,
            headers: {
                'Accept-Language': generalSupport.LanguageName()
            },
            beforeSend: function (xhr) {
                xhr.setRequestHeader('Authorization', 'Bearer ' + generalSupport.user.token);
            },
            error: function (qXHR, textStatus, errorThrown) {
                generalSupport.ErrorHandler(qXHR, textStatus, errorThrown);
            }
        });
    }

    // Gets the address from the service.
    this.GetAddress = function (addressKey) {
        return $.ajax({
            type: "GET",
            url: urlService + "addresses/get?addresskey=" + addressKey,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            crossDomain: true,
            headers: {
                'Accept-Language': generalSupport.LanguageName()
            },
            beforeSend: function (xhr) {
                xhr.setRequestHeader('Authorization', 'Bearer ' + generalSupport.user.token);
            },
            error: function (qXHR, textStatus, errorThrown) {
                generalSupport.ErrorHandler(qXHR, textStatus, errorThrown);
            }
        });
    }

    // Gets all the countries.
    this.GetCountries = function (localSelector) {
        return $.ajax({
            type: "GET",
            url: urlService + "countries/get",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            crossDomain: true,
            headers: {
                'Accept-Language': generalSupport.LanguageName()
            },
            beforeSend: function (xhr) {
                xhr.setRequestHeader('Authorization', 'Bearer ' + generalSupport.user.token);
            },
            success: function (data) {
                AddressSupport.RenderSelector(data, "Country", localSelector);
            },
			error: function (qXHR, textStatus, errorThrown) {
                generalSupport.ErrorHandler(qXHR, textStatus, errorThrown);
            }
        });
    }

    // Gets all the provinces.
    this.GetProvinces = function (localSelector) {
        return $.ajax({
            type: "GET",
            url: urlService + "provinces/get",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            crossDomain: true,
            headers: {
                'Accept-Language': generalSupport.LanguageName()
            },
            beforeSend: function (xhr) {
                xhr.setRequestHeader('Authorization', 'Bearer ' + generalSupport.user.token);
            },
            success: function (data) {
                AddressSupport.RenderSelector(data, "Province", localSelector);
            },
			error: function (qXHR, textStatus, errorThrown) {
                generalSupport.ErrorHandler(qXHR, textStatus, errorThrown);
            }
        });
    }

    // Gets the municipalities.
    this.GetMunicipalities = function (locality, localSelector) {
        return $.ajax({
            type: "GET",
            url: urlService + "municipalities/get?idLocal=" + ((locality === null || locality === undefined) ? "0" : locality),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            crossDomain: true,
            headers: {
                'Accept-Language': generalSupport.LanguageName()
            },
            beforeSend: function (xhr) {
                xhr.setRequestHeader('Authorization', 'Bearer ' + generalSupport.user.token);
            },
            success: function (data) {
                AddressSupport.RenderSelector(data, "Municipality", localSelector);
            },
			error: function (qXHR, textStatus, errorThrown) {
                generalSupport.ErrorHandler(qXHR, textStatus, errorThrown);
            }
        });
    }
	
	// Gets the delegations for MX.
    this.GetDelegations = function (locality, localSelector) {
        return $.ajax({
            type: "GET",
            url: urlService + "delegations/get?id=" + ((locality === null || locality === undefined) ? "0" : locality),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            crossDomain: true,
            headers: {
                'Accept-Language': generalSupport.LanguageName()
            },
            beforeSend: function (xhr) {
                xhr.setRequestHeader('Authorization', 'Bearer ' + generalSupport.user.token);
            },
            success: function (data) {
                AddressSupport.RenderSelector(data, "DelegationMX", localSelector);
            },
			error: function (qXHR, textStatus, errorThrown) {
                generalSupport.ErrorHandler(qXHR, textStatus, errorThrown);
            }
        });
    }

    // Gets the localities.
    this.GetLocalities = function (province, localSelector) {
        return $.ajax({
            type: "GET",
            url: urlService + "localities/get?idProvince=" + ((province === null || province === undefined) ? "0" : province),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            crossDomain: true,
            headers: {
                'Accept-Language': generalSupport.LanguageName()
            },
            beforeSend: function (xhr) {
                xhr.setRequestHeader('Authorization', 'Bearer ' + generalSupport.user.token);
            },
            success: function (data) {
                AddressSupport.RenderSelector(data, "Locality", localSelector);
            },
			error: function (qXHR, textStatus, errorThrown) {
                generalSupport.ErrorHandler(qXHR, textStatus, errorThrown);
            }
        });
    }

    // Gets the address config settings.
    this.GetConfigSettings = function (country) {
        return $.ajax({
            type: "GET",
            url: urlService + "addressConfig/get?country=" + country,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            crossDomain: true,
            headers: {
                'Accept-Language': generalSupport.LanguageName()
            },
            beforeSend: function (xhr) {
                xhr.setRequestHeader('Authorization', 'Bearer ' + generalSupport.user.token);
            },
            error: function (qXHR, textStatus, errorThrown) {
                generalSupport.ErrorHandler(qXHR, textStatus, errorThrown);
            }
        });
    }

    // Distributes the config settings between address and phones.
    this.SetConfigSettings = function (localSelector, configSettings) {
        AddressSupport.SetAddressSettings(localSelector, JSON.parse(configSettings.AddressConfig));
        //AddressSupport.SetPhoneSettings(localSelector, JSON.parse(configSettings.PhoneConfig));
    }

    // Shows / Hide address controls.
    this.SetAddressSettings = function (localSelector, addressConfig) {
        var localObject = {};
        // Each setting is checked.
        for (var key in addressConfig) {
            if (addressConfig.hasOwnProperty(key)) {
                // Selector is built.
                localObject = $('#' + localSelector + key + 'Container');
                if (localObject.length)
                    (addressConfig[key].Visibility) ? localObject.show() : localObject.hide();
                //console.log(key, " -> ", addressConfig[key].Visibility, " -> ", addressConfig[key].Required);
            }
        }
    }

    // Shows / Hide phone controls.
    this.SetPhoneSettings = function (localSelector, phoneConfig) {
        // TODO para teléfonos se va a hacer lo mismo que en direcciones? por país?
        //for (var key in phoneConfig) {
        //    if (phoneConfig.hasOwnProperty(key)) {
        //        console.log(key, " -> ", phoneConfig[key].Visibility, " -> ", phoneConfig[key].Required);
        //    }
        //}
    }

    // Renders the full object on the screen.
    this.RenderSelector = function (data, selectSelector, localSelector) {
        $('#' + localSelector + selectSelector).empty().append(AddressSupport.CreateOptionsForSelect(data));
    }

    // Creates <option> for the content of a <select>.
    this.CreateOptionsForSelect = function (data) {
        var htmlContent = '<option value=""></option>';
        $.each(data, function (index, value) {
            htmlContent += '<option value="' + value.Code + '">' + value.Description + '</option>';
        });
        return htmlContent;
    }

    // Loads the information of the address on the form.
    this.LoadAddressOnForm = function (localSelector, addressDTO) {
		// If the country loaded from the server is somehow null or empty, the default code is set.
        $('#' + localSelector + 'Country').val(((addressDTO.Country === null || addressDTO.Country === "" || addressDTO.Country === 0) ? $('#' + localSelector + 'DefaultCountryCodeValue').val() : addressDTO.Country));
        $('#' + localSelector + 'Municipality').val(addressDTO.MunicipalityCode);
        $('#' + localSelector + 'Locality').val(addressDTO.CityCode);
        $('#' + localSelector + 'Province').val(addressDTO.StateOrProvince);
        $('#' + localSelector + 'Email').val(addressDTO.Email);
        $('#' + localSelector + 'StreetOrUrl').val(addressDTO.StreetOrUrl);
        $('#' + localSelector + 'Street').val(addressDTO.Street);
        $('#' + localSelector + 'ZipCode').val(addressDTO.ZipCode);
        $('#' + localSelector + 'ZipCodeARG').val(addressDTO.ZipCodeARG);
        $('#' + localSelector + 'Latitude').val(addressDTO.LatitudeCardinale);
        $('#' + localSelector + 'Longitude').val(addressDTO.LongitudeCardinale);
    }

	// Handles the event when the province is changed.
	this.HandlesProvinceEvent = function (localSelector) {
		AddressSupport.GetLocalities($('#' + localSelector + 'Province').val(), localSelector);
	}
	
	// Handles the event when the province is changed.
	this.HandlesLocalityEvent = function (localSelector) {
		AddressSupport.GetMunicipalities($('#' + localSelector + 'Locality').val(), localSelector);
	}
	
	// Handles the event when countries is changed.
	this.HandlesCountryEvent = function (localSelector) {
		AddressSupport.ClearAddressOnForm(localSelector);
		var newCountry = $('#' + localSelector + 'Country').val();
		
		if (newCountry !== null) {
			AddressSupport.GetConfigSettings(newCountry).done(function (v1) {
				AddressSupport.AddressConfigSuccess(localSelector, v1);
			});
		}
	}
	
	this.AddressConfigSuccess = function(localSelector, v1) {
		// Not all countries have configuration settings.
		if (v1 === undefined) {
			$('#' + localSelector + 'ZipCodeARGContainer').hide();
			$('#' + localSelector + 'DelegationMX').val(0).parent().parent().hide();
			$('#' + localSelector + 'Municipality').val(0).parent().parent().hide();
			$('#' + localSelector + 'Locality').val(0).parent().parent().hide();
			$('#' + localSelector + 'Province').val(0).parent().parent().hide();
		} else {
			AddressSupport.SetConfigSettings(localSelector, v1);
			$('#' + localSelector + 'Municipality').parent().parent().show();
			$('#' + localSelector + 'Locality').parent().parent().show();
			$('#' + localSelector + 'Province').parent().parent().show();
			
			// $.when(AddressSupport.GetProvinces(localSelector), 
				   // AddressSupport.GetLocalities(null, localSelector), 
				   // AddressSupport.GetMunicipalities(null, localSelector)).done(function (v1, v2, v3) {
				// console.log("geographic zone levels loaded.");
			// });
		}
	}
	
	// Clears the form.
	this.ClearAddressOnForm = function (localSelector) {
        // $('#' + localSelector + 'Municipality').val("");
        // $('#' + localSelector + 'Locality').val("");
        // $('#' + localSelector + 'Province').val("");
        $('#' + localSelector + 'Email').val("");
        $('#' + localSelector + 'StreetOrUrl').val("");
        $('#' + localSelector + 'Street').val("");
        $('#' + localSelector + 'ZipCode').val("0");
        $('#' + localSelector + 'ZipCodeARG').val("");
        $('#' + localSelector + 'Latitude').val("0");
        $('#' + localSelector + 'Longitude').val("0");
	}

    // If the rectype was change, a new get must be done.
    this.ChangeRecTypeEvent = function () {
        var localSelector = $(this).attr('id').substring(0, $(this).attr('id').indexOf('sRecType'));
        var addressDTO = AddressSupport.GetLocalAddressBySelector(localSelector);

        if (!$('#' + localSelector + 'TypeOfAddressForClient').hasClass('hidden')) {
            // If it's a form for a client address, the key has to be updated. 
            addressDTO.KeyToAddressRecord = $('#' + localSelector + 'sRecType').val() + addressDTO.KeyToAddressRecord.substr(1, addressDTO.KeyToAddressRecord.length);
            $('#' + localSelector + 'KeyToAddressRecord').val(addressDTO.KeyToAddressRecord);
        }

        AddressSupport.GetAddress(addressDTO.KeyToAddressRecord).done(function (data) {
            AddressSupport.SuccessGetAddress(localSelector, data, $('#' + localSelector + 'IsPhoneAvailable').val());
        });
    }

	/**
	 * In case no address was found.
	 **/
	this.LoadDefaultAddress = function (localSelector) {
		defaultAddress.KeyToAddressRecord = $('#' + localSelector + 'NRecOwner').val();
		defaultAddress.RecordOwner = $('#' + localSelector + 'NRecOwner').val();
		defaultAddress.RecordEffectiveDate = $('#' + localSelector + 'EffectiveDate').val();
		defaultAddress.Country = $('#' + localSelector + 'Country').val();
		AddressSupport.LoadAddressOnForm(localSelector, defaultAddress);
		AddressSupport.UpdateLocalAddress(localSelector, defaultAddress);
		AddressSupport.LoadPhonesOnForm(localSelector, undefined);
	}
	
    this.SuccessGetAddress = function (selector, data, isPhoneAvailable) {
        if (data !== undefined) {
            AddressSupport.UpdateLocalAddress(selector, data);
            AddressSupport.LoadAddressOnForm(selector, data);

            // Adds the event of updating address.
            if ($('#' + selector + 'IsCalledFromBackOffice').val())
                $('#' + selector + 'UpdateAddressBtn').hide().show().off().click(AddressSupport.UpdateAddressModalEvent);
            else
                $('#' + selector + 'UpdateAddressBtn').hide().off();
            $('#' + selector + 'AddAddressBtn').hide().off();

            // Setting to show phones.
            if (isPhoneAvailable) {
				// // Renders the phone(s) on the form.
				AddressSupport.LoadPhonesOnForm(selector, data.Phones);

				// Add a new phone event.
				$('#' + selector + 'AddPhoneBtn').off().click(AddressSupport.AddNewPhoneRow);
			
				// AddressSupport.GetPhones(data.KeyToAddressRecord, '').done(function (data) {
					// AddressSupport.LoadPhonesOnForm(selector, data);
				// });
            } else {
                $('#' + selector + 'PhoneWrapper').hide();
            }
        } else {
            // No address was found with the address key.
			AddressSupport.LoadDefaultAddress(selector);

            // Adds the event of adding the new address.
            if ($('#' + selector + 'IsCalledFromBackOffice').val())
                $('#' + selector + 'AddAddressBtn').show().off().click(AddressSupport.AddAddressModalEvent);
            else
                $('#' + selector + 'AddAddressBtn').hide().off();
			
            $('#' + selector + 'UpdateAddressBtn').hide().off();
        }
    }

    // Should be called from the backoffice.
    this.Init = function(selector, isPhoneAvailable, addressKey, sClient, country, isClientAddress, sRecType, nRecOwner, sCertype, nBranch, nProduct, nPolicy, nCertif, nClaim, nBank_code, nBk_agency, effectiveDate) {
        // Owner type cannot be empty.
        if ((nRecOwner === "") || (nRecOwner === null) || (nRecOwner === undefined)) {
            toastr.error($.i18n.t("addressControl.form.RecordOwnerRequired"));
        } else {
            // AddressKey should be constructed if empty.
            if ((addressKey === null) || (addressKey === undefined) || (addressKey === 0) || (addressKey === "0") || (addressKey === "")) {
                switch (nRecOwner) {
                    case 1 || "1":
                        // Póliza/cotización/propuesta (SCA102).
                        addressKey = sRecType.toString() + sCertype.toString() + nBranch.toString() + nProduct.toString() + nPolicy.toString() + nCertif.toString();
                        break;
                    case 2 || "2":
                        // Cliente, Compañías, Intermediario, Beneficiario (SCA101, SCA103, SCA106, SCA107).
                        addressKey = sRecType.toString() + sClient.toString();
                        break;
                    case 7 || "7":
                        // Dirección de la agencia bancaria (SCA104).
                        addressKey = sRecType.toString() + nBank_code.toString() + nBk_agency.toString();
                        break;
                    case 8 || "8":
                        // Dirección del riesgo (SCA108).
                        addressKey = sRecType.toString() + sCertype.toString() + nBranch.toString() + nProduct.toString() + nPolicy.toString() + nCertif.toString();
                        break;
                    case 11 || "11":
                        // Ocurrencia del siniestro (SCA110).
                        addressKey = sRecType.toString() + sCertype.toString() + nBranch.toString() + nProduct.toString() + nPolicy.toString() + nCertif.toString() + nClaim.toString();
                        break;
                    case 12 || "12":
                        // Reclamante del siniestro (SCA778).
                        addressKey = sRecType.toString() + sCertype.toString() + nBranch.toString() + nProduct.toString() + nPolicy.toString() + nCertif.toString() + nClaim.toString();
                        break;
                    case 13 || "13":
                        // Envío de correspondencia en el siniestro (SCA735).
                        addressKey = sRecType.toString() + sCertype.toString() + nBranch.toString() + nProduct.toString() + nPolicy.toString() + nCertif.toString() + nClaim.toString() + sClient.toString();
                        break;
                    default:
                        toastr.error($.i18n.t("addressControl.form.RecordOwnerValid"));
                        break;
                }
            }
            defaultAddress.KeyToAddressRecord = addressKey;
            defaultAddress.RecordEffectiveDate = effectiveDate;
            defaultAddress.RecordOwner = nRecOwner;
            defaultAddress.Country = country;
            AddressSupport.Initialization(selector, defaultAddress, isPhoneAvailable, true);
        }
    }
    
    /**
     * Loads the language text on the address control.
     */
	this.LoadTextOnForm = function(localSelector) {
		$('#' + localSelector + 'CountryLabel').html($.i18n.t("addressControl.form.Country"));
		$('#' + localSelector + 'EmailLabel').html($.i18n.t("addressControl.form.Email"));
        $('#' + localSelector + 'StreetLabel').html($.i18n.t("addressControl.form.StreetOrUrl"));
        $('#' + localSelector + 'ZipCodeLabel').html($.i18n.t("addressControl.form.ZipCode"));
        $('#' + localSelector + 'ZipCodeARGLabel').html($.i18n.t("addressControl.form.ZipCodeARG"));
		$('#' + localSelector + 'DelegationMXLabel').html($.i18n.t("addressControl.form.DelegationMX"));
		$('#' + localSelector + 'MunicipalityLabel').html($.i18n.t("addressControl.form.Municipality"));
		$('#' + localSelector + 'LocalityLabel').html($.i18n.t("addressControl.form.Locality"));
		$('#' + localSelector + 'ProvinceLabel').html($.i18n.t("addressControl.form.Province"));
        
        $('#' + localSelector + 'AddAddressBtn').append(' ' + $.i18n.t("addressControl.form.AddAddress"));
        $('#' + localSelector + 'UpdateAddressBtn').append(' ' + $.i18n.t("addressControl.form.UpdateAddress"));
        $('#' + localSelector + 'DeleteAddressBtn').append(' ' + $.i18n.t("addressControl.form.DeleteAddress"));
        $('#' + localSelector + 'AddPhoneBtn').append(' ' + $.i18n.t("addressControl.form.AddPhone"));

        $('#' + localSelector + 'DeleteAdddressModal').find("h4.modal-title").html($.i18n.t("addressControl.form.DeleteMessageTitle"));
        $('#' + localSelector + 'DeleteAdddressModal').find("p.error-text").html($.i18n.t("addressControl.form.DeleteMessageBody"));
        $('#' + localSelector + 'DeleteAddressModalBtn').append(' ' + $.i18n.t("addressControl.form.Delete"));
        document.getElementById(localSelector + 'DeleteAddressModalBtn').title = $.i18n.t("addressControl.form.Delete");
        $('#' + localSelector + 'DeleteAdddressModal').find("button.btn.btn-default").append(' ' + $.i18n.t("addressControl.form.Cancel"));

        $('#' + localSelector + 'AddAddressModal').find("h4.modal-title").html($.i18n.t("addressControl.form.AddMessageTitle"));
        $('#' + localSelector + 'AddAddressModal').find("p.error-text").html($.i18n.t("addressControl.form.AddMessageBody"));
        $('#' + localSelector + 'AddAddressModalBtn').html($.i18n.t("addressControl.form.Add"));
        document.getElementById(localSelector + 'AddAddressModalBtn').title = $.i18n.t("addressControl.form.Add");
        $('#' + localSelector + 'AddAddressModal').find("button.btn.btn-default").html($.i18n.t("addressControl.form.Cancel"));

        $('#' + localSelector + 'UpdateAddressModal').find("h4.modal-title").html($.i18n.t("addressControl.form.UpdateMessageTitle"));
        $('#' + localSelector + 'UpdateAddressModal').find("p.error-text").html($.i18n.t("addressControl.form.UpdateAddressMessageBody"));
        $('#' + localSelector + 'UpdateAddressModalBtn').html($.i18n.t("addressControl.form.UpdateAddress"));
        document.getElementById(localSelector + 'UpdateAddressModalBtn').title = $.i18n.t("addressControl.form.UpdateAddress");
        $('#' + localSelector + 'UpdateAddressModal').find("button.btn.btn-default").html($.i18n.t("addressControl.form.Cancel"));
        
        $('#' + localSelector + 'DeletePhoneModal').find("h4.modal-title").html($.i18n.t("addressControl.form.UpdateMessageTitle"));
        $('#' + localSelector + 'DeletePhoneModal').find("p.error-text").html($.i18n.t("addressControl.form.DeleteMessageTitle"));
        $('#' + localSelector + 'DeletePhoneModalBtn').html($.i18n.t("addressControl.form.DeleteMessageBody"));
        document.getElementById(localSelector + 'DeletePhoneModalBtn').title = $.i18n.t("addressControl.form.DeletePhone");
        $('#' + localSelector + 'DeletePhoneModal').find("button.btn.btn-default").html($.i18n.t("addressControl.form.Cancel"));

        $('#' + localSelector + 'AddPhoneModal').find("h4.modal-title").html($.i18n.t("addressControl.form.AddMessageTitle"));
        $('#' + localSelector + 'AddPhoneModal').find("p.error-text").html($.i18n.t("addressControl.form.AddMessageBody"));
        $('#' + localSelector + 'AddPhoneModalBtn').html($.i18n.t("addressControl.form.Add"));
        document.getElementById(localSelector + 'AddPhoneModalBtn').title = $.i18n.t("addressControl.form.Add");
        $('#' + localSelector + 'AddPhoneModal').find("button.btn.btn-default").html($.i18n.t("addressControl.form.Cancel"));

        $('#' + localSelector + 'UpdatePhoneModal').find("h4.modal-title").html($.i18n.t("addressControl.form.UpdateMessageTitle"));
        $('#' + localSelector + 'UpdatePhoneModal').find("p.error-text").html($.i18n.t("addressControl.form.UpdatePhoneMessageBody"));
        $('#' + localSelector + 'UpdatePhoneModalBtn').html($.i18n.t("addressControl.form.UpdatePhone"));
        document.getElementById(localSelector + 'UpdatePhoneModalBtn').title = $.i18n.t("addressControl.form.UpdatePhone");
        $('#' + localSelector + 'UpdatePhoneModal').find("button.btn.btn-default").append(' ' + $.i18n.t("addressControl.form.Cancel"));
        
        $('#' + localSelector + 'MapModal').find("h4.modal-title").html($.i18n.t("addressControl.form.MapTitle"));
        $('#' + localSelector + 'MapModal').find("button.btn.btn-default").html('<i class="fa fa-times"></i> ' + $.i18n.t("addressControl.form.Close"));
		
		document.getElementById(localSelector + 'ShowMapBtn').title = $.i18n.t("addressControl.form.ShowMap");
		document.getElementById(localSelector + 'sRecTypeLabel').title = $.i18n.t("addressControl.form.AddressType");
		document.getElementById(localSelector + 'sRecType').title = $.i18n.t("addressControl.form.AddressType");
		document.getElementById(localSelector + 'CountryLabel').title = $.i18n.t("addressControl.form.Country");
		document.getElementById(localSelector + 'EmailLabel').title = $.i18n.t("addressControl.form.Email");
		document.getElementById(localSelector + 'StreetLabel').title = $.i18n.t("addressControl.form.StreetOrUrl");
		document.getElementById(localSelector + 'ZipCodeLabel').title = $.i18n.t("addressControl.form.ZipCode");
		document.getElementById(localSelector + 'ZipCodeARGLabel').title = $.i18n.t("addressControl.form.ZipCodeARG");
		document.getElementById(localSelector + 'DelegationMXLabel').title = $.i18n.t("addressControl.form.DelegationMX");
		document.getElementById(localSelector + 'MunicipalityLabel').title = $.i18n.t("addressControl.form.Municipality");
		document.getElementById(localSelector + 'LocalityLabel').title = $.i18n.t("addressControl.form.Locality");
        document.getElementById(localSelector + 'ProvinceLabel').title = $.i18n.t("addressControl.form.Province");

        document.getElementById(localSelector + 'Country').title = $.i18n.t("addressControl.form.Country");
		document.getElementById(localSelector + 'Email').title = $.i18n.t("addressControl.form.Email");
		document.getElementById(localSelector + 'Street').title = $.i18n.t("addressControl.form.Street");
		document.getElementById(localSelector + 'StreetOrUrl').title = $.i18n.t("addressControl.form.StreetOrUrl");
		document.getElementById(localSelector + 'ZipCode').title = $.i18n.t("addressControl.form.ZipCode");
		document.getElementById(localSelector + 'ZipCodeARG').title = $.i18n.t("addressControl.form.ZipCodeARG");
		document.getElementById(localSelector + 'DelegationMX').title = $.i18n.t("addressControl.form.DelegationMX");
		document.getElementById(localSelector + 'Municipality').title = $.i18n.t("addressControl.form.Municipality");
		document.getElementById(localSelector + 'Locality').title = $.i18n.t("addressControl.form.Locality");
        document.getElementById(localSelector + 'Province').title = $.i18n.t("addressControl.form.Province");
        document.getElementById(localSelector + 'Autocomplete').title = $.i18n.t("addressControl.form.SearchAddress");
        
        document.getElementById(localSelector + 'AddAddressBtn').title = $.i18n.t("addressControl.form.AddAddress");
		document.getElementById(localSelector + 'UpdateAddressBtn').title = $.i18n.t("addressControl.form.UpdateAddress");
		document.getElementById(localSelector + 'DeleteAddressBtn').title = $.i18n.t("addressControl.form.DeleteAddress");
        document.getElementById(localSelector + 'AddPhoneBtn').title = $.i18n.t("addressControl.form.AddPhone");
        
        $('#' + localSelector + 'Email').attr("placeholder", $.i18n.t("addressControl.form.EmailPlaceholder"));
        $('#' + localSelector + 'Autocomplete').attr("placeholder", $.i18n.t("addressControl.form.SearchAddress"));
        $('#' + localSelector + 'StreetOrUrl').attr("placeholder", $.i18n.t("addressControl.form.SearchAddress"));
		
		$('#' + localSelector + 'sRecType').append('<option value="1">' + $.i18n.t("addressControl.form.AddressTypeComercial") + '</option>');
		$('#' + localSelector + 'sRecType').append('<option value="2">' + $.i18n.t("addressControl.form.AddressTypeParticular") + '</option>');
		$('#' + localSelector + 'sRecType').append('<option value="3">' + $.i18n.t("addressControl.form.AddressTypePOBox") + '</option>');
	}
	
	/**
     * Gets the languages for the address control.
     */
     this.LoadLanguage = function(localSelector) {
		//  Load the JSON File
		$.ajax("/fasi/locales/AddressControlBase." + generalSupport.LanguageName() + ".json").done(function(addressResource){
			$.i18n.addResourceBundle(generalSupport.LanguageName(), 'translation', addressResource, true, true);
			AddressSupport.LoadTextOnForm(localSelector);
		});
	}
	
    /**
     * Principal function of the control.
     */
    this.Initialization = function (selector, addressDTO, isPhoneAvailable, isCalledFromBackOffice, showMap, showEmail, showZipCode, isEmailRequired, typeOfAddress, isNormalizedAddress, isCountryStatic, isVisible, isEnabled) {
		// Hides the container of zip codes for Arg.
		$('#' + selector + 'ZipCodeARGContainer').hide();
		// Hides the container of delegation geographic zone for Mx.
		$('#' + selector + 'DelegationMX').val(0).parent().parent().hide();
		// POBOX is only shown if the type of address is 3. This is handle after the load off the lookups.
		$('#' + selector + 'POBOXContainer').hide();
		
		// Handles the zip code (numeric values)
		// if (zipCodeNumeric === undefined)
			//zipCodeNumeric = new AutoNumeric('#' + selector + 'ZipCode', autoNumericOptions);
		
		// Event for changing country.
		$('#' + selector + 'Country').change(function (e) { AddressSupport.HandlesCountryEvent(selector) });
		
		// Event for changing the province.
		$('#' + selector + 'Province').change(function (e) { AddressSupport.HandlesProvinceEvent(selector) });
		
		// Event for changing the locality.
		$('#' + selector + 'Locality').change(function (e) { AddressSupport.HandlesLocalityEvent(selector) });
		
		// Normalize the properties of the address object.
		addressDTO = generalSupport.NormalizeDatesInObject(addressDTO);
		
		// Assigns the value from the form designer to the object.
		addressDTO.TypeOfAddress = typeOfAddress;
		
		if (AddressSupport.GetLocalAddressBySelector(selector) === undefined) {
			// Loads the languages for the control.
			AddressSupport.LoadLanguage(selector);
			
			// Retrieves address endpoint.
			AddressSupport.GetEndpoint().done(function () {
				// Before loading address of phones, lookups are invoked.
				$.when(
					AddressSupport.GetCountries(selector), 
					AddressSupport.GetDefaultCountrySetting(),
					AddressSupport.GetShowMapOnControlSetting(),
					AddressSupport.GetLocalities(null, selector), 
					AddressSupport.GetMunicipalities(null, selector), 
					AddressSupport.GetProvinces(selector), 
					AddressSupport.GetBestTimeToCall(), 
					AddressSupport.GetPhoneTypes()).done(function (v1, v2, v3, v4, v5, v6, v7, v8) {
						// Loads the info on the form.
						AddressSupport.SuccessfulDefaultGets(selector, addressDTO, isPhoneAvailable, isCalledFromBackOffice, showMap, isEnabled);

						// Sets the default country especified in the configuration settings.
                        var defaultCountryCode = v2[0].d;
                        $('#' + selector + 'DefaultCountryCodeValue').val(defaultCountryCode);
                        $('#' + selector + 'Country').val(defaultCountryCode);
                        
                        try {
                            if (v3[0].d) {
                                $('#' + selector + 'ShowMapBtn').show();
                                $('#' + selector + 'NoMapBtn').addClass('hidden');
                            } else {
                                $('#' + selector + 'ShowMapBtn').addClass('hidden');
                                $('#' + selector + 'NoMapBtn').show();
                            }
                        } catch (e) {
                            console.log(e);
                        }
                        
						// Loads the controls by the configuration settings.
						AddressSupport.GetConfigSettings(defaultCountryCode).done(function (v9) {
							AddressSupport.AddressConfigSuccess(selector, v9);
						});
					});
			});
		} else {
			AddressSupport.SuccessfulDefaultGets(selector, addressDTO, isPhoneAvailable, isCalledFromBackOffice, showMap, isEnabled);
		}

		// If the setting to show the email is true.
		var showEmailSelector = $('#' + selector + 'Email').parent().parent();
		(showEmail) ? showEmailSelector.show() : showEmailSelector.hide();
		
		// If the setting to show the zipCode is true.
		var showZipCodeSelector = $('#' + selector + 'ZipCode').parent().parent();
		(showZipCode) ? showZipCodeSelector.show() : showZipCodeSelector.hide();
		
		// If the email is required in the designer, the event must be created.
		if ((isEmailRequired !== undefined) && (isEmailRequired))
			AddressSupport.EmailIsRequired(selector);

		// If the country is static, the user should not be able to change it.
		if ((isCountryStatic !== undefined) && (isCountryStatic))
			$('#' + selector + 'Country').attr('disabled', true);
		
		// If normalized is activated, the service operation is called to normalize the given address.
		if (isNormalizedAddress)
			$('#' + selector + 'StreetOrUrl, #' + selector + 'Street').change(function (e) { AddressSupport.NormalizeStreetAddress(selector) });
        
        // Shows or hides the control with every request to the server.
        if (isVisible)
            AddressSupport.Visible(selector, isVisible);

		AddressSupport.postBackCounter++;
    }

	/**
	 * Once the default gets are called
	 **/
	this.SuccessfulDefaultGets = function(selector, addressDTO, isPhoneAvailable, isCalledFromBackOffice, showMap, isEnabled) {
		// Once the requests from all the sources are done:						
		// Shows the control when all the base calls are done.
		$('#' + selector + 'AddressControl').show();
		
		if ((addressDTO !== null) && (addressDTO !== undefined)) {
			// This behavior is no longer available, the type of address is selected on the form designer.
			$('#' + selector + 'TypeOfAddressForClient').addClass('hidden');
			if ((addressDTO.TypeOfAddress !== undefined) && (addressDTO.TypeOfAddress.toString() === "3")) {
				// PO BOX
				AddressSupport.ShowOnlyPOBox(selector);
				addressDTO.TypeOfAddress = 3;
			}

			// Renders the address on the form.
			if (isCalledFromBackOffice) {
				AddressSupport.GetAddress(addressDTO.KeyToAddressRecord).done(function (data) {
					AddressSupport.SuccessGetAddress(selector, data, isPhoneAvailable);
				});
			} else {
				//if (AddressSupport.GetLocalAddressBySelector(selector) === undefined) {
					AddressSupport.SuccessGetAddress(selector, addressDTO, isPhoneAvailable);
				//}
			}
		} else {
			// Default HTML content is added.
			AddressSupport.LoadDefaultAddress(selector);
		}
			
		// If the setting to show the map is true, this event supports the modal of the map.
		if (showMap) {
			AddressSupport.ShowMap(selector);
		} else {
			$('#' + selector + 'ShowMapBtn').find('i').hide();
        }
        
        // Enables or disbles the control with every request to the server.
        if ((isEnabled !== null) && (isEnabled !== undefined))
            AddressSupport.Enable(selector, isEnabled);
	}
	
	this.NormalizeStreetAddress = function(selector) {
		try {
			$.ajax({
				type: "GET",
				url: urlService + "normalization/get?StreetAddress1=" + $('#' + selector + 'StreetOrUrl').val() + "&StreetAddress2=" + $('#' + selector + 'Street').val() + "&simple=false",
				contentType: "application/json; charset=utf-8",
				dataType: "json",
				crossDomain: true,
				headers: {
					'Accept-Language': generalSupport.LanguageName()
				},
				beforeSend: function (xhr) {
					xhr.setRequestHeader('Authorization', 'Bearer ' + generalSupport.user.token);
				},
				success: function (data) {
					$('#' + selector + 'StreetOrUrl').val(data.StreetAddress1),
					$('#' + selector + 'Street').val(data.StreetAddress2)
				},
				error: function (qXHR, textStatus, errorThrown) {
					generalSupport.ErrorHandler(qXHR, textStatus, errorThrown);
				}
			});
		} catch (e) {
			console.log(e);
		}
	}
	
	/**
	 * If the type of address is 3, then all other controls must be hidden.
	 **/
	this.ShowOnlyPOBox = function(selector) {
		$('#' + selector + 'StreetOrUrl').parent().parent().parent().hide();
		$('#' + selector + 'ZipCodeContainer').hide();
		$('#' + selector + 'ZipCodeARGContainer').hide();
		$('#' + selector + 'POBOXContainer').show();
		$('#' + selector + 'DelegationMX').val(0).parent().parent().hide();
		$('#' + selector + 'Municipality').val(0).parent().parent().hide();
		$('#' + selector + 'Locality').val(0).parent().parent().hide();
		$('#' + selector + 'Province').val(0).parent().parent().hide();
		$('#' + selector + 'Email').parent().parent().hide();		
	}	
	
    /**
	 * Shows the modal containing the map.
	 **/
    this.ShowMap = function (selector) {
		$('#' + selector + 'ShowMapBtn').off().click(function() {
			//var localSelector = $(this).attr('id').substring(0, $(this).attr('id').indexOf('ShowMapBtn'));
			$('#' + selector + 'MapModal').modal('show').draggable({
				handle: ".modal-header"
			});
			
			var addressDTO = AddressSupport.GetLocalAddressBySelector(selector);
			
			if ((addressDTO !== undefined) && ((addressDTO.LatitudeCardinale !== null) && (addressDTO.LongitudeCardinale !== null)) && ((addressDTO.LatitudeCardinale !== "") && (addressDTO.LongitudeCardinale !== "")) && ((addressDTO.LatitudeCardinale.toString() !== "0") && (addressDTO.LongitudeCardinale.toString() !== "0"))) {
				$('#' + selector + 'Autocomplete').val(addressDTO.StreetOrUrl);
				loadMap(addressDTO.LatitudeCardinale, addressDTO.LongitudeCardinale, selector);
			} else {
				loadMapUsingLocation(selector);
			}
		});
		
		defaultMapBehaviorOnForm(selector);
    }

	/**
	 * Handles the event if the email is required and adds the rule.
	 **/
	this.EmailIsRequired = function(selector) {
		var emailSelector = $("#" + selector + "Email");
		emailSelector.rules("remove");
		emailSelector.rules("add", { 
			required: true, 
			messages: { 
				required: $.i18n.t("addressControl.form.EmailRequired")
			} 
		});
	}
    
    // Loads the information of the phones on the form.
    this.LoadPhonesOnForm = function (localSelector, phoneDTO) {
        var phoneContent = '<h3>' + $.i18n.t("addressControl.form.Phones") + '</h3>';
        var phoneContentSelector = $('#' + localSelector + 'PhonesContainer');

        if ((phoneDTO !== undefined) && (phoneDTO !== null) && (phoneDTO.length > 0)) {
            for (var i = 0; i < phoneDTO.length; i++) {
                phoneContent +=
                    '<div class="row">' +
                    '<div class="col-xs-12 col-sm-6 col-lg-3" id="' + localSelector + 'TypeOfPhoneContainer-' + phoneDTO[i].KeyPhone + '">' +
                        '<label class="phone-label" style="height: auto;">' + $.i18n.t("addressControl.form.TypeOfPhone") + '</label>' +
                        '<select class="form-control input-sm" id="' + localSelector + 'TypeOfPhone-' + phoneDTO[i].KeyPhone + '" title="' +
                        $.i18n.t("addressControl.form.TypeOfPhone") + '">' + AddressSupport.localPhoneTypes + '</select>' +
                    '</div>' +
                    '<div class="col-xs-12 col-sm-6 col-lg-3" id="' + localSelector + 'BestTimeToCallContainer-' + phoneDTO[i].KeyPhone + '">' +
                        '<label class="phone-label" style="height: auto;">' + $.i18n.t("addressControl.form.BestTimeToCall") + '</label>' +
                        '<select class="form-control input-sm" id="' + localSelector + 'BestTimeToCall-' + phoneDTO[i].KeyPhone + '" title="' +
                        $.i18n.t("addressControl.form.BestTimeToCall") + '">' + AddressSupport.localBestTimeToCall + '</select>' +
                    '</div>' +
                    '<div class="col-xs-12 col-sm-6 col-lg-4" id="' + localSelector + 'PhoneNumberContainer-' + phoneDTO[i].KeyPhone + '">' +
                        '<label class="phone-label" style="height: auto;">' + $.i18n.t("addressControl.form.PhoneNumber") + '</label>' +
                        '<input type="number" placeholder="12345678" maxlength="11" max="99999999999" class="form-control input-sm" id="' + localSelector + 'PhoneNumber-' +
                            phoneDTO[i].KeyPhone + '" value="' + phoneDTO[i].PhoneNumber + '" title="' + $.i18n.t("addressControl.form.PhoneNumber") + '">' +
                    '</div>' +
                    '<div class="col-xs-12 col-sm-6 col-lg-2" id="' + localSelector + 'Ext1Container-' + phoneDTO[i].KeyPhone + '">' +
                        '<label class="phone-label" style="height: auto;">' + $.i18n.t("addressControl.form.Extension") + '</label>' + 
                        '<input type="number" placeholder="Ext." maxlength="5" max="99999" class="form-control input-sm" id="' + localSelector + 'Ext1-' +
                            phoneDTO[i].KeyPhone + '" value="' + ((phoneDTO[i].Extension1 === null) ? '' : phoneDTO[i].Extension1) + '" title="' + $.i18n.t("addressControl.form.Extension") + '">' +
                    '</div>' +
                    '<div class="col-xs-6 col-sm-6 col-lg-2 hidden" id="' + localSelector + 'Ext2Container-' + phoneDTO[i].KeyPhone + '">' +
                        '<label class="phone-label" style="height: auto;">' + $.i18n.t("addressControl.form.ExtensionTwo") + '</label>' +
                        '<input type="number" placeholder="Ext. 2" maxlength="5" max="99999" class="form-control input-sm" id="' + localSelector + 'Ext2-' +
                            phoneDTO[i].KeyPhone + '" value="' + ((phoneDTO[i].Extension2 === null) ? '' : phoneDTO[i].Extension2) + '" title="' + $.i18n.t("addressControl.form.ExtensionTwo") + '">' +
                    '</div>' +
                    '<div class="col-xs-12" id="' + localSelector + 'ActionButtons-' + phoneDTO[i].KeyPhone + '">' +
                        '<div style="margin-top: 10px; text-align: right;">' + 
                            '<button type="button" id="' + localSelector + 'EditAction-' + phoneDTO[i].KeyPhone + '" class="btn btn-primary btn-xs" title="' +
                            $.i18n.t("addressControl.form.Edit") + '"><i class="fa fa-pencil"></i> ' + $.i18n.t("addressControl.form.Edit") + '</button>' +
                            '<button type="button" id="' + localSelector + 'DeleteAction-' + phoneDTO[i].KeyPhone + '" class="btn btn-danger btn-xs" title="' +
                            $.i18n.t("addressControl.form.Delete") + '"><i class="fa fa-trash"></i> ' + $.i18n.t("addressControl.form.Delete") + '</button>' + 
                        '</div>' +
                    '</div>' +
                    '</div>';
            }
        } else {
            phoneContent = AddressSupport.EmptyPhonesTemplate(localSelector);
        }

        // HTML content is added.
        phoneContentSelector.empty().append(phoneContent);

        // Events are added.
        if ((phoneDTO !== undefined) && (phoneDTO !== null) && (phoneDTO.length > 0)) {
            for (var i = 0; i < phoneDTO.length; i++) {
                $('#' + localSelector + 'EditAction-' + phoneDTO[i].KeyPhone).click(AddressSupport.EditPhoneEvent);
                $('#' + localSelector + 'DeleteAction-' + phoneDTO[i].KeyPhone).click(AddressSupport.DeletePhoneEvent);

                // Values of the selectors are added.
                phoneContentSelector.find('#' + localSelector + 'TypeOfPhone-' + phoneDTO[i].KeyPhone).val(phoneDTO[i].TelephoneType);
                phoneContentSelector.find('#' + localSelector + 'BestTimeToCall-' + phoneDTO[i].KeyPhone).val(phoneDTO[i].BestTimeToCall);
            }
        }
    }

    this.EmptyPhonesTemplate = function (localSelector) {
        return '<h3>' + $.i18n.t("addressControl.form.Phones") + '</h3><p id="' + localSelector + 'EmptyPhones">' + $.i18n.t("addressControl.form.NoDataFound") + '</p>';
    }

    // Creates the default template for the phones.
    this.CreatePhoneTemplate = function (localSelector, phoneId) {
        return '<div class="row">' + 
			'<div class="col-xs-12 col-sm-6 col-lg-3" id="' + localSelector + 'TypeOfPhoneContainer-' + phoneId +'">' +
	            '<label class="phone-label" style="height: auto;">' + $.i18n.t("addressControl.form.TypeOfPhone") + '</label>' +
                '<select class="form-control input-sm" id="' + localSelector + 'TypeOfPhone-' + phoneId +
                    '" title="' + $.i18n.t("addressControl.form.TypeOfPhone") + '">' + AddressSupport.localPhoneTypes + '</select>' +
            '</div>' +
            '<div class="col-xs-12 col-sm-6 col-lg-3" id="' + localSelector + 'BestTimeToCallContainer-' + phoneId +'">' +
	            '<label class="phone-label" style="height: auto;">' + $.i18n.t("addressControl.form.BestTimeToCall") + '</label>' +
                '<select class="form-control input-sm" id="' + localSelector + 'BestTimeToCall-' + phoneId + 
                    '" title="' + $.i18n.t("addressControl.form.BestTimeToCall") + '">' + AddressSupport.localBestTimeToCall + '</select>' +
            '</div>' +
            '<div class="col-xs-12 col-sm-6 col-lg-4" id="' + localSelector + 'PhoneNumberContainer-' + phoneId +'">' +
	            '<label class="phone-label" style="height: auto;">' + $.i18n.t("addressControl.form.PhoneNumber") + '</label>' +
                '<input type="number" placeholder="12345678" maxlength="11" max="99999999999" class="form-control input-sm" id="' + localSelector + 'PhoneNumber-' + phoneId +
                    '" title="' + $.i18n.t("addressControl.validation.PhoneNumber") + '">' +
            '</div>' +
            '<div class="col-xs-12 col-sm-6 col-lg-2" id="' + localSelector + 'Ext1Container-' + phoneId +'">' +
	            '<label class="phone-label" style="height: auto;">' + $.i18n.t("addressControl.form.Extension") + '</label>' +
                '<input type="number" placeholder="333" maxlength="3" max="999" class="form-control input-sm" id="' + localSelector + 'Ext1-' + phoneId +
                    '" title="' + $.i18n.t("addressControl.validation.Extension") + '">' +
            '</div>' +
            '<div class="col-xs-6 col-sm-6 col-lg-2 hidden" id="' + localSelector + 'Ext2Container-' + phoneId +'">' +
	            '<label class="phone-label" style="height: auto;">' + $.i18n.t("addressControl.form.ExtensionTwo") + '</label>' +
                '<input type="number" placeholder="333" maxlength="3" max="999" class="form-control input-sm" id="' + localSelector + 'Ext2-' + phoneId +
                    '" title="' + $.i18n.t("addressControl.validation.ExtensionTwo") + '">' +
            '</div>' +
            '<div class="col-xs-12" id="' + localSelector + 'ActionButtons-' + phoneId +'">' +
	            '<div style="margin-top: 10px; text-align: right;">' +
		            '<button type="button" id="' + localSelector + 'AddAction-' + phoneId +'" class="btn btn-primary btn-xs" title="' + $.i18n.t("addressControl.form.Save") + '">' +
                        '<i class="fa fa-plus"></i> ' + $.i18n.t("addressControl.form.Save") + '</button>' +
                    '<button type="button" id="' + localSelector + 'DeleteAction-' + phoneId +'" class="btn btn-danger btn-xs" ' + 
                        'title="' + $.i18n.t("addressControl.form.Delete") + '" style="margin-left: 5px;">' +
                        '<i class="fa fa-trash"></i> ' + $.i18n.t("addressControl.form.Delete") + '</button>' +
	            '</div>' +
            '</div>' +
            '</div>';
    }

	// Default events for the container of phones.
	this.PhoneEventForContainer = function (localSelector, id) {
		// Handles the phone number (numeric values)
		// new AutoNumeric('#' + localSelector + 'PhoneNumber-' + id, autoNumericOptions);
		
		// // Handles the first extension (numeric values)
		// new AutoNumeric('#' + localSelector + 'Ext1-' + id, autoNumericOptions);
		
		// // Handles the second extension (numeric values)
		// new AutoNumeric('#' + localSelector + 'Ext2-' + id, autoNumericOptions);
		
		$('#' + localSelector + 'AddAction-' + id).click(AddressSupport.AddPhoneEvent);
		$('#' + localSelector + 'DeleteAction-' + id).click(AddressSupport.DeletePhoneEvent);
	}
	
    // Handles a new row for the phones.
    this.AddNewPhoneRow = function () {
        var localSelector = $(this).attr('id').substring(0, $(this).attr('id').indexOf('AddPhoneBtn'));
        var phonesContainer = $('#' + localSelector + 'PhonesContainer');
        $(this).hide();
        
        if ($('#' + localSelector + 'EmptyPhones').length) {
            // Creates a full template.
            phonesContainer.empty().append('<h3>' + $.i18n.t("addressControl.form.Phones") + '</h3>' + AddressSupport.CreatePhoneTemplate(localSelector, 1));
			AddressSupport.PhoneEventForContainer(localSelector, 1);
        } else {
            // Adds input for a new phone.
            if (phonesContainer.find('.fa-plus').length) {
                // If the previous phone hasn't been added, an alert is shown.
                toastr.error($.i18n.t("addressControl.form.PreviousPhoneHasNotBeenAdded"));
            } else {
                // Last id of the container plus 1.
                var phoneId = Number(phonesContainer.find('.row').last().find('input').last().attr('id').replace(localSelector + 'Ext2-', '')) + 1;

                var newPhoneRow = AddressSupport.CreatePhoneTemplate(localSelector, phoneId);

                $('#'+ localSelector + 'PhonesContainer').append(newPhoneRow);
				AddressSupport.PhoneEventForContainer(localSelector, phoneId);
            }
        }
    }

    // Deletes the row or empties the container of phones.
    this.DeletePhoneEvent = function () {
        var localSelector = $(this).attr('id').substring(0, $(this).attr('id').indexOf('DeleteAction'));
        var deleteButton = $(this);
        var phoneContainer = $('#' + localSelector + 'PhonesContainer');

        if (phoneContainer.find('.fa-plus').length) {
            if (phoneContainer.find('.row').length === 1) {
                // There's only one row and it hasn't been added to the database / database. Container is deleted.
                $('#' + localSelector + 'PhonesContainer').empty().append(AddressSupport.EmptyPhonesTemplate(localSelector));
            } else {
                // There's more than one row. Last row doesn't exist in the database / database. Last row is deleted.
                phoneContainer.find('.row').last().remove();
            }
            // "Agregar teléfono" is shown again.
            $('#' + localSelector + 'AddPhoneBtn').show();
        } else {
            // Phone exists in the object / database.
            // Unbinds the on click event of the save on close.
            $('#' + localSelector + 'DeletePhoneModal').modal('show').on('hidden.bs.modal', function (e) {
                $('#' + localSelector + 'DeletePhoneModalBtn').off();
            });

            $('#' + localSelector + 'DeletePhoneModalBtn').click(function (e) {
                e.preventDefault();
                // Gets the key of the button clicked (contains the key).
                var phoneKey = deleteButton.attr("id").replace(localSelector + 'DeleteAction-', '');
                var localAddress = AddressSupport.GetLocalAddressBySelector(localSelector);

				if (phoneContainer.find('.row').length === 1) {
					// There's only one row and it hasn't been added to the database / database. Container is deleted.
					$('#' + localSelector + 'PhonesContainer').empty().append(AddressSupport.EmptyPhonesTemplate(localSelector));
				} else {
					// There's more than one row. Row is deleted.
					deleteButton.parent().parent().parent().remove();
				}

				// Retrieves the array of phones by its selector.
				var localPhones = AddressSupport.GetLocalPhoneBySelector(localSelector);
				
				if (localPhones !== undefined) {
					// Retrieves the phone deleted from the database by its key.
					var phoneDTO = AddressSupport.GetLocalPhoneByPhoneKey(localPhones, phoneKey);
					// Deletes the phone from the local array.
					AddressSupport.UpdateLocalPhone(localSelector, phoneDTO, true, false); // true, false because its a delete.
					
					// Updates the phones on the address object.
					// localAddress.Phones = AddressSupport.GetLocalPhoneBySelector(localSelector);
					// AddressSupport.UpdateLocalAddress(localSelector, localAddress);
				}	
			
                // AddressSupport.DeletePhone(localAddress.KeyToAddressRecord, phoneKey).done(function () {
                    // if ($('#' + localSelector + 'PhoneNumberContainer').children('input').length === 1) {
                        // // There's only one row and it hasn't been added to the database. Container is deleted.
                        // $('#' + localSelector + 'PhonesContainer').empty().append(AddressSupport.EmptyPhonesTemplate(localSelector));
                    // } else {
                        // // There's more than one row. Last row doesn't exist in the database. Last row is deleted.
                        // $('#' + localSelector + 'BestTimeToCall-' + phoneKey).remove();
                        // $('#' + localSelector + 'TypeOfPhone-' + phoneKey).remove();
                        // $('#' + localSelector + 'PhoneNumber-' + phoneKey).remove();
                        // $('#' + localSelector + 'Ext1-' + phoneKey).remove();
                        // $('#' + localSelector + 'Ext2-' + phoneKey).remove();
                        // deleteButton.parent().remove();
                    // }

                    // // Retrieves the array of phones by its selector.
                    // var localPhones = AddressSupport.GetLocalPhoneBySelector(localSelector);
                    // // Retrieves the phone deleted from the database by its key.
                    // var phoneDTO = AddressSupport.GetLocalPhoneByPhoneKey(localPhones, phoneKey);
                    // // Deletes the phone from the local array.
                    // AddressSupport.UpdateLocalPhone(localSelector, phoneDTO, true);
                // });

                // "Agregar teléfono" is shown again.
                $('#' + localSelector + 'AddPhoneBtn').show();
            });
        }        
    }

    // Adds the new phone on the database and local array.
    this.AddPhoneEvent = function () {
        var localSelector = $(this).attr('id').substring(0, $(this).attr('id').indexOf('AddAction'));
        var phoneKey = $(this).attr('id').replace(localSelector + 'AddAction-', '');
        
        if ($('#' + localSelector + 'PhoneNumber-' + phoneKey).val() === '') {
            toastr.error($.i18n.t("addressControl.validation.PhoneNumberRequired"));
        } else {
            var localAddressDTO = AddressSupport.GetLocalAddressBySelector(localSelector);
			var localPhoneDTO = JSON.parse(JSON.stringify(defaultPhone));
            localPhoneDTO.KeyToPhoneRecord = phoneKey + localAddressDTO.KeyToAddressRecord;
            localPhoneDTO.RelatedAddress = localAddressDTO.KeyToAddressRecord;
            localPhoneDTO.CountryCode = localAddressDTO.Country;
            localPhoneDTO.RecordEffectiveDate = localAddressDTO.RecordEffectiveDate;
            localPhoneDTO.RecordOwner = localAddressDTO.RecordOwner;
            localPhoneDTO.BestTimeToCall = $('#' + localSelector + 'BestTimeToCall-' + phoneKey).val()
            localPhoneDTO.Extension1 = $('#' + localSelector + 'Ext1-' + phoneKey).val()
            localPhoneDTO.Extension2 = $('#' + localSelector + 'Ext2-' + phoneKey).val()
            localPhoneDTO.PhoneNumber = $('#' + localSelector + 'PhoneNumber-' + phoneKey).val();
            localPhoneDTO.TelephoneType = $('#' + localSelector + 'TypeOfPhone-' + phoneKey).val()
            localPhoneDTO.KeyPhone = phoneKey;
            localPhoneDTO.Order = phoneKey;
			
			$('#' + localSelector + 'AddAction-' + phoneKey).off().parent().prepend('<button type="button" id="' + localSelector + 'EditAction-' + phoneKey +
				'" class="btn btn-primary btn-xs" title="' + $.i18n.t("addressControl.form.Edit") + '"><i class="fa fa-pencil"></i> ' + $.i18n.t("addressControl.form.Edit") + '</button>');
			$('#' + localSelector + 'AddAction-' + phoneKey).remove();
			$('#' + localSelector + 'EditAction-' + phoneKey).click(AddressSupport.EditPhoneEvent);
						
			AddressSupport.UpdateLocalPhone(localSelector, localPhoneDTO, false, true); // false, true because its new phone.
			
			// localAddressDTO.Phones = AddressSupport.GetLocalPhoneBySelector(localSelector);
            // AddressSupport.UpdateLocalAddress(localSelector, localAddressDTO);
            
            // "Agregar teléfono" is shown again.
            $('#' + localSelector + 'AddPhoneBtn').show();
        }
    }

    // Edits the phone.
    this.EditPhoneEvent = function () {
        var localSelector = $(this).attr('id').substring(0, $(this).attr('id').indexOf('EditAction'));
        var phoneKey = $(this).attr('id').replace(localSelector + 'EditAction-', '');

        // Unbinds the on click event of the save on close.
        $('#' + localSelector + 'UpdatePhoneModal').modal('show').on('hidden.bs.modal', function (e) {
            $('#' + localSelector + 'UpdatePhoneModalBtn').off();
        });

        $('#' + localSelector + 'UpdatePhoneModalBtn').click(function (e) {
            e.preventDefault();
            var localPhones = AddressSupport.GetLocalPhoneBySelector(localSelector);
				
            if (localPhones !== undefined) {
                // Retrieves the phone deleted from the database by its key.
                var phoneDTO = AddressSupport.GetLocalPhoneByPhoneKey(localPhones, phoneKey);
                phoneDTO.BestTimeToCall = $('#' + localSelector + 'BestTimeToCall-' + phoneKey).val();
                phoneDTO.TelephoneType = $('#' + localSelector + 'TypeOfPhone-' + phoneKey).val();
                phoneDTO.PhoneNumber = $('#' + localSelector + 'PhoneNumber-' + phoneKey).val();
                phoneDTO.Extension1 = $('#' + localSelector + 'Ext1-' + phoneKey).val();
                phoneDTO.Extension2 = $('#' + localSelector + 'Ext2-' + phoneKey).val();

                //var localAddress = AddressSupport.GetLocalAddressBySelector(localSelector);
                
                // Updates the phone on the local address array.
                AddressSupport.UpdateLocalPhone(localSelector, phoneDTO, false, false); // false, false because its an update.
                        
                // Updates the phones on the address object.
                // localAddress.Phones = AddressSupport.GetLocalPhoneBySelector(localSelector);
                // AddressSupport.UpdateLocalAddress(localSelector, localAddress);
                        
                // AddressSupport.PutPhone(phoneDTO).done(function (v1) {
                    // // Updates the phone on the local array.
                    // AddressSupport.UpdateLocalPhone(localSelector, phoneDTO, false);
                // });

                // "Agregar teléfono" is shown again.
                $('#' + localSelector + 'AddPhoneBtn').show();
            }
        });
    }

    // Push the addressDTO to the array of addresses.
    this.PushLocalAddress = function (localSelector, addressDTO) {
        localAddressesDTO.push({
            "selector": localSelector,
            "addressDTO": addressDTO
        });
    }

    // Updates the local array of addresses.
    this.UpdateLocalAddress = function (localSelector, addressDTO) {
        // Old address is removed from local array.
        localAddressesDTO = localAddressesDTO.filter(function (address) { return (address.selector !== localSelector) });

        // Edited address is addded.
        AddressSupport.PushLocalAddress(localSelector, addressDTO);
    }

    /**
     * Updates the local array of phones. 
     * Phones are contained in the array of addresses.
     *  */ 
    this.UpdateLocalPhone = function (localSelector, phoneDTO, isPhoneDeleted, isNew) {
        // Gets the array of phones by the selector.
        var localAddress = AddressSupport.GetLocalAddressBySelector(localSelector);
        var newPhoneArray = (localAddress === undefined) ? null : localAddress.Phones;
 
		if ((newPhoneArray !== null) && (newPhoneArray !== undefined)) {	
            // Turns the object into an array of phones.
            newPhoneArray = Object.keys(newPhoneArray).map(function(key) { return newPhoneArray[key]; });

            // If it's new, the previous one is not removed.
            // Phone is removed if update or delete.
            if (isNew === false) {
                newPhoneArray = newPhoneArray.filter(function (phone) { return phone.KeyPhone != phoneDTO.KeyPhone });
            }
        }
		
        // If the phone is deleted, it's not pushed in the array. It's only done when the phone is updated.
        if (!isPhoneDeleted) {
            if (newPhoneArray === null) 
				newPhoneArray = [];
			
			newPhoneArray.push(phoneDTO);
        }
        
        localAddress.Phones = newPhoneArray;
        AddressSupport.UpdateLocalAddress(localSelector, localAddress);

        // Old phone is removed from local array.
        //localPhonesDTO = localPhonesDTO.filter(function (phone) { return (phone.selector !== localSelector) });
    }

    // All address objects are stored in a global variable (in case the control is used multiple times).
    // This function retrieves the address by the given selector.
    this.GetLocalAddressBySelector = function (localSelector) {
        //var returnedLocalAddressBySelector = localAddressesDTO.map(function (address) { return (address.selector == localSelector) ? address.addressDTO : undefined; })[0];
		var localAddressBySelector = localAddressesDTO.filter(function (address) { return (address.selector == localSelector) ? address.addressDTO : undefined})[0];
		
		var returnedLocalAddressBySelector = (localAddressBySelector !== undefined) ? localAddressBySelector.addressDTO : undefined;
		if(returnedLocalAddressBySelector === undefined){
			return returnedLocalAddressBySelector;
		}
		returnedLocalAddressBySelector = AddressSupport.SetAddressDTO(localSelector, returnedLocalAddressBySelector);
		
		if ((returnedLocalAddressBySelector.Phones !== undefined) && (returnedLocalAddressBySelector.Phones !== null)) {
            for (var i = 0; i < returnedLocalAddressBySelector.Phones.length; i++) {
                returnedLocalAddressBySelector.Phones[i] = generalSupport.NormalizeDatesInObject(returnedLocalAddressBySelector.Phones[i]);
                returnedLocalAddressBySelector.Phones[i].ExtensionData = null;
            }
            returnedLocalAddressBySelector.Phones = generalSupport.OrderKeysOnObject(generalSupport.NormalizeDatesInObject(returnedLocalAddressBySelector.Phones));
        }
        
		// Server expects it as null
		returnedLocalAddressBySelector.ExtensionData = null;
		return generalSupport.OrderKeysOnObject(generalSupport.NormalizeDatesInObject(returnedLocalAddressBySelector));
    }

    // All phone objects are stored in a global variable (in case the control is used multiple times).
    // This function retrieves the phone by the given selector.
    this.GetLocalPhoneBySelector = function (localSelector) {
        var localAddress = AddressSupport.GetLocalAddressBySelector(localSelector);
        if (localAddress === undefined) 
            return undefined;
        else 
            return Object.keys(localAddress.Phones).map(function(key) { return localAddress.Phones[key]; });
        
        //return localPhonesDTO.map(function (phone) { return (phone.selector == localSelector) ? phone.phoneDTO : undefined; })[0];
    }

    // Given an array of phones, return the phone by its key.
    this.GetLocalPhoneByPhoneKey = function (phones, key) {
        return phones.filter(function (phone) { return (phone.KeyPhone == key) ? phone : undefined; })[0];
    }

    // Inserts the new phone on the database.
    this.PostPhone = function (phoneDTO) {
        return $.ajax({
            type: "POST",
            url: urlService + "phones/post",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            crossDomain: true,
            headers: {
                'Accept-Language': generalSupport.LanguageName()
            },
            data: JSON.stringify( phoneDTO ),
            beforeSend: function (xhr) {
                xhr.setRequestHeader('Authorization', 'Bearer ' + generalSupport.user.token);
            },
            success: function (data) { },
            error: function (qXHR, textStatus, errorThrown) {
                generalSupport.ErrorHandler(qXHR, textStatus, errorThrown);
            }
        });
    }

    // Updates a phone of the database.
    this.PutPhone = function (phoneDTO) {
        return $.ajax({
            type: "PUT",
            url: urlService + "phones/put",
            contentType: "application/json; charset=utf-8",
            dataType: "text json",
            crossDomain: true,
            headers: {
                'Accept-Language': generalSupport.LanguageName()
            },
            data: JSON.stringify(phoneDTO),
            beforeSend: function (xhr) {
                xhr.setRequestHeader('Authorization', 'Bearer ' + generalSupport.user.token);
            },
            success: function (data) { },
            error: function (qXHR, textStatus, errorThrown) {
                generalSupport.ErrorHandler(qXHR, textStatus, errorThrown);
            }
        });
    }

    // Deletes the phone from the database.
    this.DeletePhone = function (addressKey, phoneKey) {
        return $.ajax({
            type: "DELETE",
            url: urlService + "phones/delete?addressKey=" + addressKey + "&phoneKey=" + phoneKey,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            crossDomain: true,
            headers: {
                'Accept-Language': generalSupport.LanguageName()
            },
            beforeSend: function (xhr) {
                xhr.setRequestHeader('Authorization', 'Bearer ' + generalSupport.user.token);
            },
            success: function (data) { },
            error: function (qXHR, textStatus, errorThrown) {
                generalSupport.ErrorHandler(qXHR, textStatus, errorThrown);
            }
        });
    }

    // Inserts the new address on the database.
    this.PostAddress = function (addressDTO) {
        return $.ajax({
            type: "POST",
            url: urlService + "addresses/post",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            crossDomain: true,
            headers: {
                'Accept-Language': generalSupport.LanguageName()
            },
            data: JSON.stringify( daddressDTO ),
            beforeSend: function (xhr) {
                xhr.setRequestHeader('Authorization', 'Bearer ' + generalSupport.user.token);
            },
            success: function (data) { },
            error: function (qXHR, textStatus, errorThrown) {
                generalSupport.ErrorHandler(qXHR, textStatus, errorThrown);
            }
        });
    }

    // Updates an address of the database.
    this.PutAddress = function (addressDTO) {
        return $.ajax({
            type: "PUT",
            url: urlService + "addresses/put",
            contentType: "application/json; charset=utf-8",
            dataType: "text json",
            crossDomain: true,
            headers: {
                'Accept-Language': generalSupport.LanguageName()
            },
            data: JSON.stringify( addressDTO ),
            beforeSend: function (xhr) {
                xhr.setRequestHeader('Authorization', 'Bearer ' + generalSupport.user.token);
            },
            success: function (data) { },
            error: function (qXHR, textStatus, errorThrown) {
                generalSupport.ErrorHandler(qXHR, textStatus, errorThrown);
            }
        });
    }

    // Deletes an address from the database.
    this.DeleteAddress = function (addressKey) {
        return $.ajax({
            type: "DELETE",
            url: urlService + "addresses/delete?addressKey=" + addressKey,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            crossDomain: true,
            headers: {
                'Accept-Language': generalSupport.LanguageName()
            },
            beforeSend: function (xhr) {
                xhr.setRequestHeader('Authorization', 'Bearer ' + generalSupport.user.token);
            },
            success: function (data) { },
            error: function (qXHR, textStatus, errorThrown) {
                generalSupport.ErrorHandler(qXHR, textStatus, errorThrown);
            }
        });
    }

    // Sets all the properties of the address DTO object.
    this.SetAddressDTO = function (localSelector, addressDTO) {
		// If it's a form for a client address, the key has to be updated.
        if ((!$('#' + localSelector + 'TypeOfAddressForClient').hasClass('hidden')) && (addressDTO.KeyToAddressRecord !== null))
            addressDTO.KeyToAddressRecord = $('#' + localSelector + 'sRecType').val() + addressDTO.KeyToAddressRecord.substr(1, addressDTO.KeyToAddressRecord.length);

		var municipality = $('#' + localSelector + 'Municipality').val();
		var delegation = $('#' + localSelector + 'DelegationMX').val();
		var cityCode = $('#' + localSelector + 'Locality').val();
		var stateOrProvince = $('#' + localSelector + 'Province').val();
		var country = $('#' + localSelector + 'Country').val();
		var zipCode = $('#' + localSelector + 'ZipCode').val();
		var latitude = $('#' + localSelector + 'Latitude').val();
		var longitude = $('#' + localSelector + 'Longitude').val();
		
        addressDTO.MunicipalityCode = ((municipality === null || municipality === "") ? 0 : municipality);
        addressDTO.DelegationCode = ((delegation === null || delegation === undefined || delegation === "") ? 0 : delegation);
        addressDTO.CityCode = ((cityCode === null || cityCode === "") ? 0 : cityCode);
        addressDTO.StateOrProvince = ((stateOrProvince === null || stateOrProvince === "") ? 0 : stateOrProvince);
        addressDTO.Country = ((country === null || country === "") ? 0 : country);
        addressDTO.Email = $('#' + localSelector + 'Email').val();
        addressDTO.StreetOrUrl = $('#' + localSelector + 'StreetOrUrl').val();
        addressDTO.Street = $('#' + localSelector + 'Street').val();
        addressDTO.ZipCode = ((zipCode === null || zipCode === "") ? 0 : zipCode);
        addressDTO.LatitudeCardinale = ((latitude === null || latitude === "") ? 0 : latitude);
        addressDTO.LongitudeCardinale = ((longitude === null || longitude === "") ? 0 : longitude);
        addressDTO.CompleteAddress = $('#' + localSelector + 'StreetOrUrl').val() + ', ' + $('#' + localSelector + 'Street').val();
        addressDTO.ZipCodeARG = $('#' + localSelector + 'ZipCodeARG').val();

        return addressDTO;
    }

    // Evento para agregado de direcciones.
    this.AddAddressModalEvent = function () {
        var localSelector = $(this).attr('id').substring(0, $(this).attr('id').indexOf('AddAddressBtn'));
        var addModal = $('#' + localSelector + 'AddModal');

        // Unbinds the on click event of the save on close.
        addModal.modal('show').on('hidden.bs.modal', function (e) {
            $('#' + localSelector + 'AddAddressModalBtn').off();
        });

        $('#' + localSelector + 'AddAddressModalBtn').click(function (e) {
            var addressDTO = AddressSupport.GetLocalAddressBySelector(localSelector);

            AddressSupport.PostAddress(addressDTO).always(function () {
                addModal.modal('hide');
            });
        });
    }

    // Evento para actualización de direcciones.
    this.UpdateAddressModalEvent = function () {
        var localSelector = $(this).attr('id').substring(0, $(this).attr('id').indexOf('UpdateAddressBtn'));
        var updateModal = $('#' + localSelector + 'UpdateAddressModal');

        // Unbinds the on click event of the save on close.
        updateModal.modal('show').on('hidden.bs.modal', function (e) {
            $('#' + localSelector + 'UpdateAddressModalBtn').off();
        });

        $('#' + localSelector + 'UpdateAddressModalBtn').click(function (e) {
            var addressDTO = AddressSupport.GetLocalAddressBySelector(localSelector);

            AddressSupport.PutAddress(addressDTO).always(function () {
                AddressSupport.UpdateLocalAddress(localSelector, addressDTO);
                updateModal.modal('hide');
            });
        });
    }

    // Evento para actualización de direcciones.
    this.DeleteAddressModalEvent = function () {
        var localSelector = $(this).attr('id').substring(0, $(this).attr('id').indexOf('DeleteAddressBtn'));
        var updateModal = $('#' + localSelector + 'DeleteAddressModal');

        // Unbinds the on click event of the save on close.
        updateModal.modal('show').on('hidden.bs.modal', function (e) {
            $('#' + localSelector + 'DeleteAddressModalBtn').off();
        });

        $('#' + localSelector + 'DeleteAddressModalBtn').click(function (e) {
            var addressDTO = AddressSupport.GetLocalAddressBySelector(localSelector);

            AddressSupport.DeleteAddress(addressDTO.KeyToAddressRecord).always(function () {
                updateModal.modal('hide');
            });
        });
    }

    // En true, la dirección es requerida.
    this.Required = function (selector, validate) {
        if (validate) {
            $('#' + selector + 'StreetOrUrl').attr("required", true);
        } else {
			$('#' + selector + 'StreetOrUrl').attr("required", false);
        }
    }

    // En true, mustra la dirección.
    this.Visible = function (selector, visible) {
        if (visible) {
            $('#' + selector + 'AddressControl').parent().show();
        } else {
            $('#' + selector + 'AddressControl').parent().hide();
        }
    }

    // En false, deshabilita los botones.
    this.Enable = function (selector, enable) {
        if (enable) {
			$('#' + selector + 'AddressControl').parent().find(".btn, a, input .input-group-addon, select").attr('disabled', false);
        } else {
			$('#' + selector + 'AddressControl').parent().find(".btn, a, input .input-group-addon, select").attr('disabled', true);
        }
    }
}

var defaultAddress = {  
    "Agree":0,
	"ApartmentNumber":null,
	"BankAgency":0,
	"BankInternalCode":0,
	"BranchOffice":0,
	"BuildingNumber":null,
	"CancellationDate":new Date(parseFloat(-62135582400000)),
	"CertificateID":0,
	"CityCode":0,
	"CityName":null,
	"ClaimID":0,
	"ClientID":null,
	"CompleteAddress":null,
	"CostCenter":null,
	"Country":0,
	"CountryDescription":null,
	"CustomBoolean":false,
	"CustomBooleanEx":false,
	"CustomDate":new Date(parseFloat(-62135582400000)),
	"CustomDateEx":new Date(parseFloat(-62135582400000)),
	"CustomNumeric":0,
	"CustomNumericEx":0,
	"CustomString":null,
	"CustomStringEx":null,
	"DelegationCode":0,
	"EffectiveDateNormalization":new Date(parseFloat(-62135582400000)),
	"Email":null,
	"ExtensionData":null,
	"FloorNumber":0,
	"FullAddress":"",
	"IndicatorOfCollectionAddress":null,
	"IndicatorOfCorrespondenceAddress":null,
	"IndicatorSendingCorrespondenceByEmail":null,
	"IsDeletedMark":false,
	"IsDirty":true,
	"IsNew":true,
	"KeyToAddressRecord":null,
	"Latitude":0,
	"LatitudeCardinale":0,
	"LatitudeCoordinateGrades":0,
	"LatitudeGrades":0,
	"LatitudeMinutes":0,
	"LatitudeSeconds":0,
	"LedgerProcessIndicator":null,
	"LineOfBusiness":0,
	"Location":null,
	"Longitude":0,
	"LongitudeCardinale":0,
	"LongitudeGrades":0,
	"LongitudeMinutes":0,
	"LongitudeSeconds":0,
	"LonigitudeCoordinateGrades":0,
	"MailingAddressIndicator":false,
	"MunicipalityCode":0,
	"NTypeOfAddress":0,
	"NormalizationStatus":0,
	"NotInformeEMailCause":0,
	"POBox":null,
	"Phones":null,
	"PolicyID":0,
	"PremiumFinancialAgreement":0,
	"ProductCode":0,
	"RecordEffectiveDate":new Date(parseFloat(-62135582400000)),
	"RecordEffectiveDateOld":new Date(parseFloat(-62135582400000)),
	"RecordOwner":0,
	"RecordType":null,
	"RecordTypeEnum":0,
	"RecordTypeEnumText":"None",
	"SendIndicatorOfCollectionNoticeByEmail":null,
	"StateInstance":null,
	"StateOrProvince":0,
	"Street":null,
	"StreetOrUrl":null,
	"Tag":null,
	"TypeOfAddress":"2",
	"TypeOfAddressEnum":2,
	"TypeOfAddressEnumText":"Home",
	"UpdateTimeStamp":new Date(parseFloat(-62135582400000)),
	"UserCode":0,
	"ValidAddressIndicator":null,
	"ZipCode":0,
	"ZipCodeARG":null,
	"SElevation": null
}

var defaultPhone = {
    "AreaCode": 0,
    "BestTimeToCall": "04",
    "BestTimeToCallDescription": null,
    "CancellationDate": new Date(parseFloat(-62135582400000)),
    "CountryCode": 0,
    "Extension1": "0",
    "Extension2": "0",
    "ExtensionData": null,
    "IsDeletedMark": false,
    "IsDirty": true,
    "IsNew": true,
    "KeyPhone": 1,
    "KeyToPhoneRecord": "",
    "NPhoneClas": 0,
    "Order": 1,
    "PhoneNumber": "",
    "RecordEffectiveDate": new Date(parseFloat(-62135582400000)),
    "RecordEffectiveDateOld": new Date(parseFloat(-62135582400000)),
    "RecordOwner": 2,
    "RelatedAddress": null,
    "StateInstance": null,
    "Tag": null,
    "TelephoneType": "2",
    "TelephoneTypeDescription": null,
    "TelephoneTypeEnum": 2,
    "TelephoneTypeEnumText": "Mobile",
    "UpdateTimeStamp": new Date(parseFloat(-62135582400000)),
    "UserCode": 0
}

var addressControlSettings = {
    "Country": 56,
    "AddressConfig": {
        "Agree": { "Visibility": true, "Required": false },
        "ApartmentNumber": { "Visibility": true, "Required": false },
        "BankAgency": { "Visibility": true, "Required": false },
        "BankInternalCode": { "Visibility": true, "Required": false },
        "BranchOffice": { "Visibility": true, "Required": false },
        "BuildingNumber": { "Visibility": true, "Required": false },
        "CancellationDate": { "Visibility": true, "Required": false },
        "CityCode": { "Visibility": true, "Required": false },
        "CityName": { "Visibility": true, "Required": false },
        "CompleteAddress": { "Visibility": true, "Required": false },
        "CostCenter": { "Visibility": true, "Required": false },
        "Country": { "Visibility": true, "Required": false },
        "DelegationCode": { "Visibility": true, "Required": false },
        "EffectiveDateNormalization": { "Visibility": true, "Required": false },
        "Email": { "Visibility": true, "Required": false },
        "FloorNumber": { "Visibility": true, "Required": false },
        "IndicatorOfCollectionAddress": { "Visibility": true, "Required": false },
        "IndicatorOfCorrespondenceAddress": { "Visibility": true, "Required": false },
        "IndicatorSendingCorrespondenceByEmail": { "Visibility": true, "Required": false },
        "LatitudeCardinale": { "Visibility": true, "Required": false },
        "LatitudeCoordinateGrades": { "Visibility": true, "Required": false },
        "LatitudeGrades": { "Visibility": true, "Required": false },
        "LatitudeMinutes": { "Visibility": true, "Required": false },
        "LatitudeSeconds": { "Visibility": true, "Required": false },
        "LedgerProcessIndicator": { "Visibility": true, "Required": false },
        "Location": { "Visibility": true, "Required": false },
        "LongitudeCardinale": { "Visibility": true, "Required": false },
        "LongitudeGrades": { "Visibility": true, "Required": false },
        "LongitudeMinutes": { "Visibility": true, "Required": false },
        "LongitudeSeconds": { "Visibility": true, "Required": false },
        "LonigitudeCoordinateGrades": { "Visibility": true, "Required": false },
        "MailingAddressIndicator": { "Visibility": true, "Required": false },
        "MunicipalityCode": { "Visibility": true, "Required": false },
        "NTypeOfAddress": { "Visibility": true, "Required": false },
        "NormalizationStatus": { "Visibility": true, "Required": false },
        "NotInformeEMailCause": { "Visibility": true, "Required": false },
        "POBox": { "Visibility": true, "Required": false },
        "RecordEffectiveDate": { "Visibility": true, "Required": false },
        "SendIndicatorOfCollectionNoticeByEmail": { "Visibility": true, "Required": false },
        "StateOrProvince": { "Visibility": true, "Required": false },
        "Street": { "Visibility": true, "Required": false },
        "StreetOrUrl": { "Visibility": true, "Required": false },
        "TypeOfAddress": { "Visibility": true, "Required": false },
        "ValidAddressIndicator": { "Visibility": true, "Required": false },
        "ZipCode": { "Visibility": true, "Required": false },
        "ZipCodeARG": { "Visibility": false, "Required": false }
    },
    "PhoneSettings": {
        "AreaCode": { "Visibility": true, "Required": false },
        "BestTimeToCall": { "Visibility": true, "Required": false },
        "CancellationDate": { "Visibility": true, "Required": false },
        "CountryCode": { "Visibility": true, "Required": false },
        "Extension1": { "Visibility": true, "Required": false },
        "Extension2": { "Visibility": false, "Required": false },
        "Order": { "Visibility": true, "Required": false },
        "PhoneNumber": { "Visibility": true, "Required": false },
        "RecordEffectiveDate": { "Visibility": true, "Required": false },
        "TelephoneType": { "Visibility": true, "Required": false }
    }
}