var map_options = {};
var map;
var defaultBounds;
var infowindow;

/*----------------------------------------------------- Google Maps ------------------------------------------------ */
// Sets the animation effect on the marker.
function toggleBounce(marker) {
	if (marker.getAnimation() !== null)
		marker.setAnimation(null);
	else
		marker.setAnimation(google.maps.Animation.BOUNCE);
}

// Gets and sets the elevation of the point marked on the map
// This method now needs the API key in order to work charges may apply
// https://developers.google.com/maps/documentation/elevation/usage-limits
function getElevation(center) {
    var locations = [];

    // push the center location of the map on the array                
    locations.push(center);

    // Create a LocationElevationRequest object using the array's one value
    var positionalRequest = {
        'locations': locations
    };

    // Initiate the location request
    var elevator = new google.maps.ElevationService();
    elevator.getElevationForLocations(positionalRequest, function (results, status) {
        if (status == google.maps.ElevationStatus.OK) {
            // Retrieve the first result
            if (results[0]) {
                // Sets the elevation to the input
                $("#txtElevationEdit").val(results[0].elevation);
            } else {
                console.log('No se han encontrado resultados.');
                $("#txtElevationEdit").val("0");
            }
        } else {
            console.log('El servicio de elevación ha fallado. Error: ' + status);
            $("#txtElevationEdit").val("0");
        }
    });
}

// Sets the info of the place into the InfoWindow of the marker.
function setMarkerAndInfoWindow(event, selector, map, marker, infowindow) {
    var centerMap = event.latLng;
    getElevation(centerMap);
    map.setCenter(centerMap);   
    marker.setPosition(centerMap);

    var geocoder = new google.maps.Geocoder();
    geocoder.geocode({
        'latLng': centerMap
    }, function (results, status) {
        if (status == google.maps.GeocoderStatus.OK) {
            var detailAddress = "";
            var addressMap = results[0].formatted_address;

            if (results[0]) {
                for (var ix = 0; ix < results[0].address_components.length; ix++) {
                    if (results[0].address_components[ix].types[0] == "administrative_area_level_1") {
                        detailAddress += results[0].address_components[ix].short_name;
                    }
                }

                setValuesFromMap(selector, results[0].address_components, centerMap);
                
                // Sets the content of the address on the info window.
                if (addressMap !== "") {
                    infowindow.setContent('<div><strong>' + addressMap + '</strong><br></div>');
                } else {
                    infowindow.setContent('<div><strong>' + detailAddress + '</strong><br></div>');
                }

                infowindow.open(map, marker);
            } else {
                alert('No results found');
            }
        } else {
            alert('Geocoder failed due to: ' + status);
        }
    });
}

// Loads the default behavior of the map.
function defaultMapBehaviorOnForm(selector) {
	infowindow = new google.maps.InfoWindow();
	map_options = {
        zoom: 16,
        mapTypeId: google.maps.MapTypeId.ROADMAP,
        scrollwheel: true,
        disableDefaultUI: false
    };

    map = new google.maps.Map(document.getElementById(selector + 'Map'), map_options);
    marker = new google.maps.Marker({
        map: map,
        draggable: false,
        animation: google.maps.Animation.DROP,
        position: map.getCenter()
    });
	
	var input = document.getElementById(selector + 'StreetOrUrl');
    var autocomplete = new google.maps.places.Autocomplete(input);
    autocomplete.bindTo('bounds', map);

    // A delay is needed because the map has to be loaded first
    window.setTimeout(function () {
		mapAutocompleteEvent(selector, autocomplete, map, infowindow, marker);
    }, 500);
}

// Loads the default behavior of the map.
function defaultMapBehavior(map, selector, infowindow, marker) {
    var input = document.getElementById(selector + 'Autocomplete');
    var autocomplete = new google.maps.places.Autocomplete(input);
    autocomplete.bindTo('bounds', map);

    // A delay is needed because the map has to be loaded first
    window.setTimeout(function () {
		mapAutocompleteEvent(selector, autocomplete, map, infowindow, marker);
		mapClickEvent(selector, map, marker, infowindow);
		mapDragAndDropEvent(selector, map, marker, infowindow);
    }, 500);
}

// Loads a map by the given coordinates.
function loadMap(latValue, lngValue, selector) {
	infowindow = new google.maps.InfoWindow();
    map_options = {
        center: new google.maps.LatLng(latValue, lngValue),
        zoom: 16,
        mapTypeId: google.maps.MapTypeId.ROADMAP,
        scrollwheel: true,
        disableDefaultUI: false
    };

    map = new google.maps.Map(document.getElementById(selector + 'Map'), map_options);
	marker = new google.maps.Marker({
        map: map,
        draggable: true,
        animation: google.maps.Animation.DROP,
        position: map.getCenter()
    });
	
    defaultMapBehavior(map, selector, infowindow, marker);
}

// Loads a default address on the map.
function loadMapUsingLocation(selector) {
	infowindow = new google.maps.InfoWindow();
    map_options = {
        zoom: 16,
        mapTypeId: google.maps.MapTypeId.ROADMAP,
        scrollwheel: true,
        disableDefaultUI: false
    };

    map = new google.maps.Map(document.getElementById(selector + 'Map'), map_options);
	marker = new google.maps.Marker({
        map: map,
        draggable: true,
        animation: google.maps.Animation.DROP,
        position: map.getCenter()
    });

    // Try HTML5 geolocation.
    if (navigator.geolocation) {
        navigator.geolocation.getCurrentPosition(function (position) {
            var pos = new google.maps.LatLng(position.coords.latitude,
                                             position.coords.longitude);
            map.setCenter(pos);
            defaultMapBehavior(map, selector, infowindow, marker);

            marker.setPosition(pos);
			// marker = new google.maps.Marker({
			// 	map: map,
			// 	draggable: true,
			// 	animation: google.maps.Animation.DROP,
			// 	position: map.getCenter()
			// });
	
            infowindow.setContent('<div><strong>Ubicación actual aproximada.</strong></div>');
            infowindow.open(map, marker);

            // Sets the latitude and longitude on the input variables.
            setLatitudeAndLongitude(selector, pos);

            // Gets the elevation of the center of the map.
            getElevation(pos);
        }, function () {
            handleNoGeolocation(true, infowindow);
        });
    } else {
        // Browser doesn't support Geolocation.
        handleNoGeolocation(false, infowindow);
    }
}

// Handles no geolocation on browser.
function handleNoGeolocation(errorFlag, infowindow) {
    var content = "";
    if (errorFlag)
        content = 'Error: El servicio de Geolocation ha fallado.';
    else
        content = 'Error: Su navegador no soporta geolocation.';

    // Default address.
    var options = {
        map: map,
        zoom: 16,
        mapTypeId: google.maps.MapTypeId.ROADMAP,
        scrollwheel: false,
        disableDefaultUI: false,
        position: new google.maps.LatLng(-33.4136972, -70.5838192),
        content: content
    };

    infowindow = new google.maps.InfoWindow(options);
    map.setCenter(options.position);
}

// Autocomplete event on map.
function mapAutocompleteEvent(selector, autocomplete, map, infowindow, marker) {
    google.maps.event.addListener(autocomplete, "place_changed", function (event) {
		try {
			var place = autocomplete.getPlace();

			if (!place.geometry) {
				console.log("Autocomplete's returned place contains no geometry");
				return;
			}

			if (place.geometry.viewport) {
				map.fitBounds(place.geometry.viewport);
			} else {
				map.setCenter(place.geometry.location);
				map.setZoom(16);
			}

			//console.log(place.geometry.location);

			// Gets the elevation of the center of the map.
			getElevation(place.geometry.location);

			marker.setPosition(place.geometry.location);
			
			setValuesFromMap(selector, place.address_components, map.center);

			infowindow.setContent('<div><strong>' + place.formatted_address + '</strong></div>');
			infowindow.open(map, marker);
			toastr.info("Address updated.");
		} catch (e){
			toastr.error("Invalid Address."); //dict.RecordOwnerValid[localStorage.getItem('languageName')]);
			console.log("Invalid Address.");
		}
    });
}

// Click event on map.
function mapClickEvent(selector, map, marker, infowindow) {
    google.maps.event.addListener(map, 'click', function (event) {
        setMarkerAndInfoWindow(event, selector, map, marker, infowindow);

        toggleBounce(marker);
        setTimeout(toggleBounce(marker), 100);
    });
}

// Drag and Drop Event on marker.
function mapDragAndDropEvent(selector, map, marker, infowindow) {
    google.maps.event.addListener(marker, 'dragend', function (event) {
        setMarkerAndInfoWindow(event, selector, map, marker, infowindow);
    });
}

// Sets location by the given place of the map.
function setValuesFromMap(selector, place, centerMap) {
    setLatitudeAndLongitude(selector, centerMap);
    setStreetOrUrl(selector, place);
    setPostalCode(selector, place);

    // Geographic levels.
    setCountry(selector, place);
    setMunicipality(selector, place);
    setProvince(selector, place);
    setLocality(selector, place);
}

// Try sets the street or url on the html element.
function setStreetOrUrl(selector, place) {
    // Address information, first line. Street or url.
    try {
        var placeDesc = place[1].short_name + ' ' + place[0].short_name
        if (placeDesc.length > 49) {
            console.log("La dirección introducida es muy extensa, se reduce a 50 caracteres.");
        }
		var streetOrUrl = placeDesc.substring(0, 49);
        $('#' + selector + 'StreetOrUrl').first().val(streetOrUrl);
		$('#' + selector + 'Autocomplete').val(streetOrUrl);
		console.log("streetOrUrl:", streetOrUrl);
        console.log("street_number:", place.find(function (place) { return place.types[0] === "street_number"; }).long_name);
    } catch (e) {
        console.log("error setting street_number");
    }    
}

// Try sets the country on the html element.
function setCountry(selector, place) {
    try {
        $('#' + selector + 'Country').val($('#' + selector + 'Country option').filter(function () {
            return $(this).html().toLowerCase() == place.find(function (place) {
                return place.types[0] === "country";
            }).long_name.toLowerCase();
        }).val());
        console.log('country:', place.find(function (place) { return place.types[0] === "country"; }).long_name);
    } catch (e) {
        console.log("error setting country");
    }
}

// Try sets the municipality on the html element.
function setMunicipality(selector, place) {
    try {
        $('#' + selector + 'Municipality').val($('#' + selector + 'Municipality option').filter(function () {
            return $(this).html().toLowerCase() == place.find(function (place) {
                return place.types[0] === "locality";
            }).long_name.toLowerCase();
        }).val());
        console.log('locality:', place.find(function (place) { return place.types[0] === "locality"; }).long_name);
        console.log("administrative_area_level_3", place.find(function (place) { return place.types[0] === "administrative_area_level_3"; }).long_name);
    } catch (e) {
        console.log("error setting locality");
    }
}

// Try sets the province on the html element.
function setProvince(selector, place) {
    try {
        $('#' + selector + 'Province').val($('#' + selector + 'Province option').filter(function () {
            return (place.find(function (place) {
					return place.types[0] === "administrative_area_level_1";
				}).long_name.toLowerCase().includes($(this).html().toLowerCase())) ? $(this).html().toLowerCase() : "";
			}).val());
        console.log('administrative_area_level_1:', place.find(function (place) { return place.types[0] === "administrative_area_level_1"; }).long_name);
    } catch (e) {
        console.log("error setting administrative_area_level_1");
    }
}

// Try sets the locality on the html element.
function setLocality(selector, place) {
    try {
        $('#' + selector + 'Locality').val($('#' + selector + 'Locality option').filter(function () {
            return $(this).html().toLowerCase() == place.find(function (place) {
                return place.types[0] === "administrative_area_level_2";
            }).long_name.toLowerCase();
        }).val());
        console.log('administrative_area_level_2:', place.find(function (place) { return place.types[0] === "administrative_area_level_2"; }).long_name);
    } catch (e) {
        console.log("error setting administrative_area_level_2");
    }
}

// Try sets the postal code on the html element.
function setPostalCode(selector, place) {
    try {
        $('#' + selector + 'ZipCode').val(place.find(function (place) { return place.types[0] === "postal_code"; }).long_name);
        console.log('zip_code:', place.find(function (place) { return place.types[0] === "postal_code"; }).long_name);
    } catch (e) {
		$('#' + selector + 'ZipCode').val(0);
        console.log("error setting postal_code");
    }
}

// Sets the latitude and longitude values on inputs.
function setLatitudeAndLongitude(selector, centerMap) {
	var addressDTO = AddressSupport.GetLocalAddressBySelector(selector);
	addressDTO.LongitudeCardinale = centerMap.lng();
	addressDTO.LatitudeCardinale = centerMap.lat();
	AddressSupport.UpdateLocalAddress(selector, addressDTO);
	
    $('#' + selector + 'Latitude').val(centerMap.lat());
    $('#' + selector + 'Longitude').val(centerMap.lng());
}

/*----------------------------------------------------- Google Maps ------------------------------------------------ */