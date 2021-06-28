var ClientSupport = new function () {

    this.CodeAndDigitStep1 = function (codeCtrl, digitCtrl, nameCtrl, errorCtrl, firstLetter, expression, gridCtrl, columnName, expandCode, onlyvalidate, clientType, onlyExist, ctrolList) {
        var codeValue = codeCtrl.GetValue();
        var result = false;

        if (codeValue !== null) {
            var removedText = codeValue.replace(/\D+/g, '');
            codeCtrl.SetValue(removedText);
            codeValue = removedText;
        }

        if (validate(codeValue, expression)) {
            if (expandCode === true)
                codeCtrl.SetValue(expand(codeValue, firstLetter, expression));
            codeCtrl.SetIsValid(true);
            result = true;
            switch (clientType) {
                case 'person':
                    if (codeValue >= 50000000) {
                        codeCtrl.SetIsValid(false);
                        codeCtrl.SetErrorText('El RUT pertenece a una empresa');
                        result = false;
                    }
                    break;
                case 'business':
                    if (codeValue < 50000000) {
                        codeCtrl.SetIsValid(false);
                        codeCtrl.SetErrorText('El RUT pertenece a una persona');
                        result = false;
                    }
                    break;
            }
            if (result === true) {
                if (digitCtrl === null && nameCtrl !== null) {
                    ClientInformation(codeCtrl, nameCtrl, errorCtrl, firstLetter, expression, onlyExist);
                }
            }

        } else {
            codeCtrl.SetIsValid(false);
            codeCtrl.SetErrorText('Código de cliente invalido');
        }
        if (onlyvalidate === false && digitCtrl !== null) {
            digitCtrl.SetValue('');
            //if (codeValue == null && ctrolList !== null) {
            if (ctrolList !== undefined && ctrolList !== null) {
                ctrolList.split(",").forEach(function (item) {
                    if (item.startsWith("#")){
                        ASPxClientControl.GetControlCollection().GetByName('btnADD' + item.substr(1)).SetVisible(false);
                        ASPxClientControl.GetControlCollection().GetByName(item.substr(1)).SetVisible(false);
                    }
                    else
                        ASPxClientControl.GetControlCollection().GetByName(item).SetValue(null);
                });
            }
        }
        if (nameCtrl !== null)
            nameCtrl.SetValue('');
        return result;
    }

    this.CodeAndDigitStep2 = function (codeCtrl, digitCtrl, nameCtrl, errorCtrl, firstLetter, expression, gridCtrl, columnName, expandCode, onlyvalidate, clientType, onlyExist) {
        var result = false;
        if (validate(codeCtrl.GetValue(), expression)) {
            if (CheckDigitValid(digitCtrl.GetValue())) {
                digitCtrl.SetValue(digitCtrl.GetValue().toString().toUpperCase());
                if (VerificaRut(codeCtrl.GetValue(), digitCtrl.GetValue())) {
                    codeCtrl.SetIsValid(true);
                    digitCtrl.SetIsValid(true);
                    if (nameCtrl !== null || onlyExist === true) {
                        ClientInformation(codeCtrl, nameCtrl, errorCtrl, firstLetter, expression, onlyExist);
                    }
                    result = true;
                } else {
                    digitCtrl.SetIsValid(false);
                    digitCtrl.SetErrorText('');
                    codeCtrl.SetIsValid(false);
                    codeCtrl.SetErrorText('Dígito invalido');
                    if (nameCtrl !== null)
                        nameCtrl.SetValue('');
                }
            } else {
                digitCtrl.SetIsValid(false);
                digitCtrl.SetErrorText('');
                codeCtrl.SetIsValid(false);
                codeCtrl.SetErrorText('Dígito invalido');
                if (nameCtrl !== null)
                    nameCtrl.SetValue('');
            }
        }
        return result;
    }

    function ClientInformation(codeCtrl, nameCtrl, errorCtrl, firstLetter, expression, onlyExist) {
        var param = { clientID: expand(codeCtrl.GetValue(), firstLetter, expression) };
        if (nameCtrl != null)
            nameCtrl.innerHTML = 'Buscando...';
        $.ajax({
            url: window.location.protocol + '//' + window.location.host + '/customscripts/ClientControlWebMethod.aspx/ClientInformation',
            data: JSON.stringify(param),
            dataType: 'json',
            type: 'POST',
            contentType: 'application/json; charset=utf-8',
            dataFilter: function (data) { return data; },
            success: function (data) {
                if (nameCtrl !== null)
                    nameCtrl.SetValue(data.d);
                if (errorCtrl != null)
                    errorCtrl.SetValue('');
                if (onlyExist === true && data.d === '') {
                    codeCtrl.SetIsValid(false);
                    codeCtrl.SetErrorText('Código del cliente no registrado');
                }
            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                if (nameCtrl !== null)
                    nameCtrl.SetValue('');
                if (errorCtrl != null)
                    errorCtrl.SetValue('No se pudo procesar el código del cliente');
                else
                    alert('No se pudo procesar el código del cliente');
                console.log(textStatus);
            }
        });
    }

    function validate(code, expression) {
        var regex = new RegExp(expression, 'g');
        return regex.test(code);
    }

    function expand(code, firstLetter, expression) {
        var result = code;
        if (validate(result, expression)) {
            var regex = new RegExp(expression, 'g');
            result = regex.exec(result)[0];
            if (firstLetter) {
                result = result.substr(0, 1).toUpperCase() + paddy(result.substr(1), 13, '0');
            }
            else {
                result = paddy(result, 14, '0');
            }
        }
        return result;
    }

    function paddy(n, p, c) {
        var pad_char = typeof c !== 'undefined' ? c : '0';
        var pad = new Array(1 + p).join(pad_char);
        return (pad + n).slice(-pad.length);
    }


    function VerificaRut(rut, dig) {
        if (rut.toString() !== '' && dig.toString() !== '') {
            var caracteres = new Array();
            var serie = new Array(2, 3, 4, 5, 6, 7);


            for (var i = 0; i < rut.length; i++) {
                caracteres[i] = parseInt(rut.charAt((rut.length - (i + 1))));
            }

            var sumatoria = 0;
            var k = 0;
            var resto = 0;

            for (var j = 0; j < caracteres.length; j++) {
                if (k === 6) {
                    k = 0;
                }
                sumatoria += parseInt(caracteres[j]) * parseInt(serie[k]);
                k++;
            }

            resto = sumatoria % 11;
            dv = 11 - resto;

            if (dv === 10) {
                dv = 'K';
            }
            else if (dv === 11) {
                dv = 0;
            }

            if (dv.toString().toUpperCase() === dig.toString().toUpperCase())
                return true;
            else
                return false;
        }
        else {
            return false;
        }
    }

    function CheckDigitValid(dv) {
        if (dv !== '0' && dv !== '1' && dv !== '2' && dv !== '3' && dv !== '4' && dv !== '5' && dv !== '6' && dv !== '7' && dv !== '8' && dv !== '9' && dv !== 'k' && dv !== 'K') {
            return false;
        }
        return true;
    }
}




// https://jsfiddle.net/

//var pattern1 = '^[a-zA-Z][0-9]{1,13}';
//var pattern2 = '^[0-9]{1,14}';
//var text = 'a89';

//if(validate(text,pattern1))
//alert(expand(text,true,pattern1));
//else
//alert(text + ' not valid');

//if(validate(text,pattern2))
//alert(expand(text,true,pattern2));
//else
//alert(text + ' not valid');
