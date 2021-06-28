var mapSupport = new function () {
    var Configurations = new Array();
    var elementInternal;
    var geocoder = new google.maps.Geocoder();

    function place_changed(lat, long, geo_options) {
        try {
            mapSupport.CreateLocation(geo_options.RootControl, lat, long);
        } catch (e) {
            alert('Ocurrió un error al tratar de ubicar la posición en sus controles');
        }
    }

    //#region Events

    function keyUp(e, geo_options) {
        try {
            var lat = parseFloat(AutoNumeric.getNumber('#' + geo_options.ControlLatitude));
            var long = parseFloat(AutoNumeric.getNumber('#' + geo_options.ControlLongitude));

            if (geo_options.Latitude != lat || geo_options.Longitude !== long) {
                mapSupport.CreateLocation(geo_options.RootControl, lat, long, null);

                mapSupport.GeocodeLatLng(geocoder, lat, long, geo_options.ControlAddress)

                geo_options.Latitude = lat;
                geo_options.Longitude = long;
            }
        } catch (e) {
            alert('Ocurrió un error al tratar de ubicar la posición en sus controles');
        }
        e.preventDefault();
    }

    function Address_keyUp(e, geo_options) {
        var geocoder = new google.maps.Geocoder();
        if (geo_options.Address != $('#' + geo_options.ControlAddress).val()) {
            mapSupport.GeocodeAddress(geocoder, geo_options, $('#' + geo_options.ControlAddress).val());
        }
    };

    function EventDragend(event, geo_options) {
        var lat = event.latLng.lat();
        var lng = event.latLng.lng();

        SetLatAndLog(geo_options, lat, lng);

        mapSupport.GeocodeLatLng(geocoder, lat, lng, geo_options.ControlAddress)
        mapSupport.GeocodeLatLngInTitle(geocoder, geo_options, lat, lng);

        geo_options.Latitude = lat;
        geo_options.Longitude = lng;

        mapSupport.UpdateAddressByLocation(geocoder, geo_options, lat, lng)

        geo_options.Map.setCenter(event.latLng);
    };

    function EventZoomChanged(event, geo_options) {
        var rr = event;

        /// $('#lblCoordenate').text(geo_options.Map.getZoom());
    };

    function EventCenterChanged(event, geo_options) {
        var rr = event;
    };

    //#end-region

    this.Initialization = function (element, optionsMap) {
        elementInternal = element;
        var optionsMapLocal = {
            center: { lat: -34.397, lng: 150.644 },
            zoom: 8,
            enableHighAccuracy: true,
            maximumAge: 30000,
            timeout: 27000
        };

        var found = false;
        if (Configurations.length != 0) {
            Configurations.forEach(function (item) {
                if (item.RootControl === element) {
                    found = true;
                }
            });
        }

        if (optionsMap !== null) {
            for (var propertie in optionsMap) {
                optionsMapLocal[propertie] = optionsMap[propertie];
            }
            optionsMapLocal['RootControl'] = element;
        }

        map = new google.maps.Map(document.getElementById(element), optionsMapLocal);

        optionsMapLocal.Map = map;
        optionsMapLocal.Marker = null;
        optionsMapLocal.Address = null;

        if (found == false) {
            Configurations.push(optionsMapLocal);
        }

        if (optionsMapLocal.ControlAutocomplete != null) {
            var input = (document.getElementById(optionsMapLocal.ControlAutocomplete));

            var autocomplete = new google.maps.places.Autocomplete(input);

            autocomplete.bindTo('bounds', optionsMapLocal.Map);

            autocomplete.addListener('place_changed', function () {
                var place = autocomplete.getPlace();
                if (place.geometry != null) {
                    var lat = place.geometry.location.lat();
                    var long = place.geometry.location.lng();
                    place_changed.call(this, lat, long, optionsMapLocal);
                }
            });
        }

        if (optionsMapLocal.ControlBindCoordinates == true) {
            if (optionsMapLocal.ControlLatitude !== null) {
                $('#' + optionsMapLocal.ControlLatitude).data("EventThrow", true);
                $(document).on('change', '#' + optionsMapLocal.ControlLatitude, function (e) {
                    $('#' + optionsMapLocal.ControlAddress).data("EventThrow", false);
                    if ($('#' + optionsMapLocal.ControlLatitude).data("EventThrow") === true) {
                        keyUp.call(this,
                            e,
                            optionsMapLocal
                        );
                    }
                    $('#' + optionsMapLocal.ControlLatitude).data("EventThrow", true);
                });
            }

            if (optionsMapLocal.ControlLongitude !== null) {
                $('#' + optionsMapLocal.ControlLongitude).data("EventThrow", true);
                $(document).on('change', '#' + optionsMapLocal.ControlLongitude, function (e) {
                    $('#' + optionsMapLocal.ControlAddress).data("EventThrow", false);
                    if ($('#' + optionsMapLocal.ControlLongitude).data("EventThrow") === true) {
                        keyUp.call(this,
                            e,
                            optionsMapLocal
                        );
                    }
                    $('#' + optionsMapLocal.ControlLongitude).data("EventThrow", true);
                });
            }
        }

        if (optionsMapLocal.ControlBindAdress == true && optionsMapLocal.ControlAddress != null) {
            $('#' + optionsMapLocal.ControlAddress).data("EventThrow", true);
            $(document).on('change', '#' + optionsMapLocal.ControlAddress, function (e) {
                $('#' + optionsMapLocal.ControlLatitude).data("EventThrow", false);
                $('#' + optionsMapLocal.ControlLongitude).data("EventThrow", false);
                if ($('#' + optionsMapLocal.ControlAddress).data("EventThrow") === true) {
                    Address_keyUp.call(this,
                        e,
                        optionsMapLocal
                    );
                }
                $('#' + optionsMapLocal.ControlAddress).data("EventThrow", true);
            });
        }
    };

    function Configuration(element) {
        var result = null;
        if (Configurations.length != 0) {
            Configurations.forEach(function (item) {
                if (item.RootControl === element) {
                    result = item;
                }
            });
        }
        return result;
    };

    this.LocationByIP = function (response, geo_options) {
        var loc = response.loc.split(',');
        AutoNumeric.set('#' + geo_options.ControlLatitude, loc[0]);
        AutoNumeric.set('#' + geo_options.ControlLongitude, loc[1]);

        mapSupport.GeocodeLatLng(geocoder, loc[0], loc[1], geo_options.ControlAddress);

        mapSupport.CreateLocation(geo_options.RootControl, loc[0], loc[1], null);
    };

    /**
     * Método que permite asignar la coordenada desde un dirección física.
     * @param {any} element El nombre de control.
     * @param {any} address El address a asignar.
     */
    this.CreateLocationByAddress = function (element, address) {
        mapSupport.CreateLocation(element, null, null, address);
    };

   
    /**
     * Método permite realizar un render de un arreglo de direcciones naturales en el mapa
     * @param {any} options Conjunto de paramentos que permite el render:
     *                      1. element El nombre de control.
     *                      2. data Array address a asignar.
     *                      3. property Propiedad a buscar.
     *                      4. template es el template que se desea asignar al content de market.
     */
    this.CreateLocationByAddressByArray = function (options) {
        var element = options.element;
        var data = options.data;
        var property = options.property;
        var template = options.template;
        var click = options.click;
        var marker, count;
        var item = Configuration(element);
        var map = item.Map;

        var infowindow = new google.maps.InfoWindow({});
        var bounds = new google.maps.LatLngBounds();

        data.forEach(function (valorAdress, indiceAddress, data) {
            geocoder.geocode({ 'address': valorAdress[property] }, function (results, status) {
                if (status === 'OK') {
                    var lat = results[0].geometry.location.lat();
                    var lng = results[0].geometry.location.lng();

                    var LatLng = new google.maps.LatLng(lat, lng);

                    bounds.extend(LatLng);

                    marker = new google.maps.Marker({
                        position: LatLng,
                        map: map,
                        title: valorAdress[property],
                        item: valorAdress,
                        property: property,
                        template: template,
                        click: click
                    });

                    var address = valorAdress[property];
                    var isLast = indiceAddress === data.length - 1;
                    if (isLast) {
                        //# auto - zoom
                        map.fitBounds(bounds);
                        //# auto-center
                        //map.panToBounds(bounds);
                    }
                    google.maps.event.addListener(marker, 'click', (function (marker) {
                        return function () {
                            var item = marker.item;
                            var property = marker.property;
                            if (marker.template) {
                                var content = generalSupport.RenderBody({
                                    name: "Template7",
                                    template: marker.template,
                                    context: marker.item
                                });
                                infowindow.setContent(content);
                            } else {
                                infowindow.setContent(marker.item[marker.property]);
                            }
                            infowindow.open(map, marker);
                            if (marker.click) {
                                marker.click(this, marker.item);
                            }
                        };
                    })(marker));
                }
            });
        });
    };

    /**
     *  Método que permite asignar la coordenada desde un dirección física.
     * @param {any} element El nombre de control.
     * @param {any} coordinates Las coordenadas separadas por coma a asignar.
     */
    this.CreateLocationByMergedCoordinates = function (element, coordinates) {
        var vector = coordinates.split(',');
        mapSupport.CreateLocation(element, vector[0], vector[1], null);
    };

    this.CreateLocationByLatitude = function (element, callback) {
        mapSupport.AutoLocation(element, callback, null);
    };

    this.CreateLocationByLongitude = function (element, callback) {
        mapSupport.AutoLocation(element, null, callback);
    };

    this.AutoLocation = function (element, callbackLatitude, callbackLongitude) {
        var geo_options = Configuration(element);

        if (navigator.geolocation) {
            navigator.geolocation.getCurrentPosition(function (position) {
                var lat = position.coords.latitude;
                var long = position.coords.longitude;
                var city = position.coords.locality;

                if (geo_options) {
                    if (geo_options.ControlLatitude) {
                        AutoNumeric.set('#' + geo_options.ControlLatitude, lat);
                    }
                    if (geo_options.ControlLongitude) {
                        AutoNumeric.set('#' + geo_options.ControlLongitude, long);
                    }
                }

                if (callbackLatitude) {
                    lat = callbackLatitude.call();
                }

                if (callbackLongitude) {
                    long = callbackLongitude.call();
                }

                mapSupport.GeocodeLatLng(geocoder, lat, long, geo_options.ControlAddress);

                mapSupport.CreateLocation(geo_options.RootControl, lat, long, null);
            }, function (failure) {
                console.log(failure);
                $.getJSON('https://ipinfo.io/geo', function (response) {
                    mapSupport.LocationByIP(response, geo_options);
                });
            }, geo_options);
        } else {
            alert("There is Some Problem on your current browser to get Geo Location!");
        }
    };

    this.CreateLocation = function (element, latitude, longitude, address) {
        var geo_options = Configuration(element);
        if (address !== null || (longitude != null && longitude != null)) {
            if (latitude !== null && latitude !== '' && longitude !== null && longitude !== '') {
                var LatLng = new google.maps.LatLng(latitude, longitude);

                geo_options.Latitude = latitude;
                geo_options.Longitude = longitude;
                geo_options.center = LatLng;

                var map = geo_options.Map;

                if (geo_options.Marker == null) {
                    geo_options.Marker = new google.maps.Marker({
                        position: LatLng,
                        map: map,
                        draggable: geo_options.draggable,
                        title: 'Ubicación actual aproximada'
                    });
                    if (geo_options.draggable == true) {
                        geo_options.Marker.addListener('dragend', function (e) {
                            EventDragend.call(this,
                                e,
                                geo_options
                            );
                        });
                    }
                }

                InicializacionMarker(geo_options, LatLng);

                geo_options.Marker.setPosition(LatLng);

                marker = geo_options.Marker;

                marker.setMap(map);

                var getInfoWindow = null;

                if (geo_options.InfoWindow == null) {
                    geo_options.InfoWindow = new google.maps.InfoWindow({
                        content: "<b>Ubicación actual aproximada</b>"
                    });
                }

                getInfoWindow = geo_options.InfoWindow;

                var geocoder = new google.maps.Geocoder();
                getInfoWindow.open(map, marker);

                mapSupport.GeocodeLatLngInTitle(geocoder, geo_options, latitude, longitude);

                if (geo_options.draggable === true) {
                    geo_options.Marker = marker;
                }
                geo_options.InfoWindow = getInfoWindow;
                map.setCenter(marker.getPosition());
            } else {
                var geocoder = new google.maps.Geocoder();
                geo_options.Address = address;
                mapSupport.GeocodeAddress(geocoder, geo_options, address);
            }
        }
        else {
            mapSupport.AutoLocation(element);
        }
    };

    function InicializacionMarker(geo_options, LatLng) {
        if (geo_options.Marker === null) {
            geo_options.Marker = new google.maps.Marker({
                draggable: geo_options.draggable,
                position: LatLng,
                title: "<div style = 'height:60px;width:200px'><b>Ubicación actual aproximada</b></div>"
            });

            if (geo_options.draggable == true) {
                geo_options.Marker.addListener('dragend', function (e) {
                    EventDragend.call(this,
                        e,
                        geo_options
                    );
                });
            }
        } else {
            geo_options.Marker.position = LatLng;
        }
    };

    function InicializationEvent(geo_options) {
        if (geo_options.draggable == true) {
            geo_options.Marker.addListener('dragend', function (e) {
                EventDragend.call(this,
                    e,
                    geo_options
                );
            });
        }
    };

    this.GeocodeAddress = function (geocoder, geo_options, address) {
        geocoder.geocode({ 'address': address }, function (results, status) {
            if (status === 'OK') {
                var lat = results[0].geometry.location.lat();
                var lng = results[0].geometry.location.lng();

                var LatLng = new google.maps.LatLng(lat, lng);

                geo_options.Latitude = lat;
                geo_options.Longitude = lng;
                geo_options.center = LatLng;
                geo_options.Address = address;

                var map = geo_options.Map;

                var marker;

                if (geo_options.Marker == null) {
                    geo_options.Marker = new google.maps.Marker({
                        position: LatLng,
                        draggable: geo_options.draggable,
                        map: map,
                        title: geo_options.Address
                    });

                    if (geo_options.draggable === true) {
                        geo_options.Marker.addListener('dragend', function (e) {
                            EventDragend.call(this,
                                e,
                                geo_options
                            );
                        });
                    }
                }

                InicializacionMarker(geo_options, LatLng);

                geo_options.Marker.setPosition(LatLng);

                marker = geo_options.Marker;

                marker.setMap(map);

                if (geo_options.InfoWindow == null) {
                    geo_options.InfoWindow = new google.maps.InfoWindow({
                        content: "<b>" + geo_options.Address + "</b>"
                    });
                }

                var geocoder = new google.maps.Geocoder();
                geo_options.InfoWindow.open(map, marker);

                SetLatAndLog(geo_options, lat, lng);

                geo_options.InfoWindow.setContent("<div style = 'height:60px;width:200px'><b>" + address + "</b></div>");
                geo_options.Map.setCenter(LatLng);
            } else {
                alert('Geocode was not successful for the following reason: ' + status);
            }
        });
    }

    function SetLatAndLog(geo_options, latitude, longitude) {
        if (geo_options.ControlLatitude !== null) {
            AutoNumeric.set('#' + geo_options.ControlLatitude, latitude);
        }
        if (geo_options.ControlLongitude !== null) {
            AutoNumeric.set('#' + geo_options.ControlLongitude, longitude);
        }
    }

    this.GeocodeLatLngInTitle = function (geocoder, geo_options, latitude, longitude) {
        try {
            var latlng = { lat: parseFloat(latitude), lng: parseFloat(longitude) };
            geocoder.geocode({ 'location': latlng }, function (results, status) {
                if (status === 'OK') {
                    if (results[0]) {
                        geo_options.InfoWindow.setContent("<div><b>" + results[0].formatted_address + "</b></div>");
                        geo_options.Address = results[0].formatted_address;
                        if (geo_options.ControlAddress != null) {
                            $('#' + geo_options.ControlAddress).val(geo_options.Address);
                        }
                    } else {
                        Address.setContent("<div><b>No results found</b></div>");
                    }
                } else {
                    console.log('Geocoder failed due to: ' + status);
                }
            });
        } catch (e) {
            console.log(e);
        }
    };

    this.UpdateAddressByLocation = function (geocoder, geo_options, latitude, longitude) {
        try {
            var latlng = { lat: parseFloat(latitude), lng: parseFloat(longitude) };
            geocoder.geocode({ 'location': latlng }, function (results, status) {
                if (status === 'OK') {
                    if (results[0]) {
                        geo_options.Address = results[0].formatted_address;
                    } else {
                        geo_options.Address = null;
                    }
                } else {
                    console.log('Geocoder failed due to: ' + status);
                }
            });
        } catch (e) {
            console.log(e);
        }
    };

    this.GeocodeLatLng = function (geocoder, latitude, longitude, Address) {
        try {
            var latlng = { lat: parseFloat(latitude), lng: parseFloat(longitude) };
            geocoder.geocode({ 'location': latlng }, function (results, status) {
                if (status === 'OK') {
                    if (results[0]) {
                        $('#' + Address).val(results[0].formatted_address);
                    } else {
                        $('#' + Address).val('No results found');
                    }
                } else {
                    console.log('Geocoder failed due to: ' + status);
                }
            });
        } catch (e) {
            console.log(e);
        }
    };
}