var chatOpened = false;
var countMessageNotRead = 0;
var token_session_id = '';

$(document).ready(function () {

    $("#btn-input").keyup(function (event) {
        if (event.keyCode == 13) {
            $('.chat-history').append('<div class="chat-message clearfix">' +
                                        '<img src="http://placehold.it/40/FA6F57/fff&text=Tu" alt="" width="32" height="32" class="img-circle" >' +
                                        '<div class="chat-message-content clearfix">' +
                                          '<span class="chat-time">' + formatAMPM(new Date()) + '</span>' +
                                          '<h5>Tu</h5>' +
                                          '<p>' + $('#btn-input').val() + '</p>' +
                                        '</div>' +
                                      '</div>' +
                                      '<hr>');
            $('.chat-history').scrollTop($('.chat-history').height() + $('#live-chat').height());
            SendInput($('#btn-input').val());
            $('#chatAudio')[0].play();
            $('#btn-input').val('');
            event.preventDefault();
        }
    });

    $('#live-chat header').on('click', function () {
        $('.chat').slideToggle(300, 'swing');
        
        chatOpened = chatOpened ? false : true;
        if (chatOpened) {
            $('.chat-history').scrollTop($('.chat-history').height() + $('#live-chat').height());
            countMessageNotRead = 0;
            $('.chat-message-counter').hide();
            if (token_session_id === '')
                tokenSetup();
        }
        else
            if (countMessageNotRead > 0) {
                $('.chat-message-counter').html(countMessageNotRead);
                $('.chat-message-counter').show(300, 'swing');
            }
    });

    $('.chat-close').on('click', function (e) {

        e.preventDefault();
        $('#live-chat').fadeOut(300);

    });
});

function convertToHex(str) {
    var hex = '';
    for (var i = 0; i < str.length; i++) {
        hex += '' + str.charCodeAt(i).toString(16);
    }
    return hex;
}

function convertFromHex(hex) {
    var hex = hex.toString();//force conversion
    var str = '';
    for (var i = 0; i < hex.length; i += 2)
        str += String.fromCharCode(parseInt(hex.substr(i, 2), 16));
    return str;
}

function formatAMPM(date) {
    var hours = date.getHours();
    var minutes = date.getMinutes();
    var ampm = hours >= 12 ? 'pm' : 'am';
    hours = hours % 12;
    hours = hours ? hours : 12; // the hour '0' should be '12'
    minutes = minutes < 10 ? '0' + minutes : minutes;
    var strTime = hours + ':' + minutes + ' ' + ampm;
    return strTime;
}

function tokenSetup() {
    var requestInicial = {
        "tipoTX": "login",
        "subdomain": "inmotionseguros",
        "id_usuario": "nelson",
        "DATA": {
            "id_usuario": "nelson",
            "clave": "123"
        }
    };
    var requestString = JSON.stringify(requestInicial);
    var requestEncode = convertToHex(requestString);

    SendChatText('Contactando al servidor de ARTU, por favor espere...');

    $.ajax({
        type: "POST",
        url: "https://api.artu.cl",
        contentType: "application/json;",
        dataType: "json",
        data: JSON.stringify({ "data_in": requestEncode }),
        success: function (data) {
            console.log(data);
            if (typeof data.json_out != "undefined") {
                var response = convertFromHex(data.json_out);
                var responseObj = JSON.parse(response);

                token_session_id = responseObj.DATA.session_id;

                SendChatText('Iniciando sesión con ARTU, por favor espere...');

                SendInput("Hola mi nombre es Nelson");

            } else if (typeof data.errorMessage != "undefined") {
                $("#session_id").html(data.errorMessage);
            } else {
                $("#session_id").html("Error desconocido");
            }


        },
        error: function (error) {
            alert(error);
        }
    });
};

function SendInput(texto) {
    var requestInicial = {
        "tipoTX": "chatbot_intencion",
        "subdomain": "inmotionseguros",
        "id_usuario": "nelson",
        "session_id": token_session_id,
        "DATA": { "question": texto }
    };
    var requestString = JSON.stringify(requestInicial);
    var requestEncode = convertToHex(requestString);

    $.ajax({
        type: "POST",
        url: "https://api.artu.cl",
        contentType: "application/json;",
        dataType: "json",
        data: JSON.stringify({ "data_in": requestEncode }),
        success: function (data) {
            console.log(data);
            if (typeof data.json_out != "undefined") {
                var response = convertFromHex(data.json_out);
                var responseObj = JSON.parse(response);

                console.log(response);

                if (typeof responseObj.DATA.codigo_respuesta != "undefined") {
                    $('#chat').append('<li class="left clearfix">Ha expirado la session</li>');
                } else {
                    SendChatText(responseObj.DATA[0].answer);
                    $('.chat-history').scrollTop($('.chat-history').height()+$('#live-chat').height());
                    $('#chatAudio')[0].play();

                    if (!chatOpened) {
                        countMessageNotRead = countMessageNotRead + 1;
                        $('.chat-message-counter').html(countMessageNotRead);
                        $('.chat-message-counter').show(300, 'swing');
                    }
                }

            } else if (typeof data.errorMessage != "undefined") {
                $('#chat').append('<li class="left clearfix">' + data.errorMessage + '</li>');
            } else {
                $('#chat').append('<li class="left clearfix">Error desconocido</li>');
            }


        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert(xhr);
        }
    });
};

function SendChatText(texto) {
    $('.chat-history').append('<div class="chat-message clearfix">' +
                                '<img src="http://40.87.63.36/images/chatbot.jpg" alt="" width="32" height="32">' +
                                '<div class="chat-message-content clearfix">' +
                                  '<span class="chat-time">' + formatAMPM(new Date()) + '</span>' +
                                  '<h5>Insurance ChatBot</h5>' +
                                  '<p>' + texto + '</p>' +
                                '</div>' +
                              '</div>' +
                              '<hr>');
}