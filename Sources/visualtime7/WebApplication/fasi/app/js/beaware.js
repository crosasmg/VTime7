app.beaware = (function () {

    /**
     * Devuelve el token de acceso de BeAware tomando en cuenta la vigencia de 30 min del mismo.
     * @returns {string} Token de acceso de BeAware
     */
    CacheToken = function () {
        var result = '';
        var incache = false;

        if (sessionStorage.getItem('BeAwareTokenDate'))
            incache = (new Date().getTime() - new Date(sessionStorage.getItem('BeAwareTokenDate')).getTime()) / 60000 < 30;

        if (!incache) {
            sessionStorage.setItem('BeAwareTokenDate', new Date());
            result = CreateToken();
            sessionStorage.setItem('BeAwareToken', result);
        }
        else
            result = sessionStorage.getItem('BeAwareToken');
        return result;
    };

    /**
     * Devuelve el token de acceso de BeAware sin manejo de cache.
     * @returns {string} Token de acceso de BeAware
     */
    CreateToken = function () {
        var result = '';

        $.ajax({
            type: 'POST',
            url: 'https://api.beaware360.com/ba360/apir/v10/login/auth',
            contentType: "application/json; charset=utf-8",
            async: false,
            dataType: "json",
            data: JSON.stringify({
                company: 'cloudclaim',
                pass: '6329255',
                user: 'api_fasi_dli'
            }),
            success: function (data) {
                result = "Basic " + btoa('cloudclaim/api_fasi_dli' + ":" + data.token);
            },
            error: function (jqXHR, textStatus, errorThrown) {
                console.error(errorThrown);
            }
        });
        return result;
    };

    return {
        Authorization: function () {
            return CacheToken();
        }
    };
})();
