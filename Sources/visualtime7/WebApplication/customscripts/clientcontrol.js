var ClientSupport = new function () {

    this.CodeAndDigitStep1 = function(codeCtrl, digitCtrl, nameCtrl, errorCtrl, firstLetter, expression) {
        var result = codeCtrl.value;
        if (validate(result, expression)) {
            codeCtrl.value = expand(result, firstLetter, expression);
            errorCtrl.innerHTML = '';
            if (digitCtrl === null) {
                ClientInformation(codeCtrl, nameCtrl, errorCtrl);
            }
        } else {
            errorCtrl.innerHTML = 'codigo invalido';
        }
        if (digitCtrl !== null) {
            digitCtrl.value = '';
        }
        nameCtrl.innerHTML = '';
    }

    this.CodeAndDigitStep2 = function(codeCtrl, digitCtrl, nameCtrl, errorCtrl, firstLetter, expression) {
        if (CheckDigitValid(digitCtrl.value)) {
            digitCtrl.value = digitCtrl.value.toString().toUpperCase();
            if (VerificaRut(codeCtrl.value, digitCtrl.value)) {
                errorCtrl.innerHTML = '';
                ClientInformation(codeCtrl, nameCtrl, errorCtrl);
            } else {
                errorCtrl.innerHTML = 'digito invalido segun el codigo';
                nameCtrl.innerHTML = '';
            }
        } else {
            errorCtrl.innerHTML = 'Digito invalido';
            nameCtrl.innerHTML = '';
        }
    }

    function ClientInformation(codeCtrl, nameCtrl, errorCtrl) {
        var param = { clientID: codeCtrl.value };
        nameCtrl.innerHTML = 'Buscando...';
        $.ajax({
            url: window.location.protocol + '//' + window.location.host + '/customscripts/ClientControlWebMethod.aspx/ClientInformation',
            data: JSON.stringify(param),
            dataType: 'json',
            type: 'POST',
            contentType: 'application/json; charset=utf-8',
            dataFilter: function (data) { return data; },
            success: function (data) {
                nameCtrl.innerHTML = data.d;
                errorCtrl.innerHTML = '';
            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                nameCtrl.innerHTML = '';
                errorCtrl.innerHTML = 'No se pudo procesar el codigo';
                alert(textStatus);
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
