app.smc = app.smc || {};
app.smc.api = (function () {
    return {
        /**
         * Permite crear una instancia para un modelo.
         * @param {string} instanceName Nombre del modelo (SMC, MailBoxInfo, Lookup)
         */
        InstanceFactory: function (instanceName) {
            var result = null;
            switch (instanceName) {
                case 'SMC':
                    result = {
                        ID: 0,
                        SENDER: 0,
                        RECEIVER: 0,
                        SUBJECT: '',
                        BODY: '',
                        PRIORITY: 0,
                        STATE: 0,
                        UNREAD: 0,
                        RECEIVED: new Date('0001-01-01T00:00:00').utc(),
                        SENDED: new Date('0001-01-01T00:00:00').utc(),
                        READED: new Date('0001-01-01T00:00:00').utc(),
                        SOURCEID: 0,
                        MAILJOBID: '',
                        SENDERNAME: ''
                    };
                    break;
                case 'MailBoxInfo':
                    result = {
                        InBox: 0,
                        SendMail: 0,
                        Drafts: 0,
                        Trash: 0
                    };
                    break;
                case 'Lookup':
                    result = {
                        Code: 0,
                        Description: ''
                    };
                    break;
            }
        },
        /**
         * Crea un registro en la tabla 'SMC'.
         * @param {any} smc Estructura de datos de tipo SMC
         * @param {boolean} async Indica si la ejecución debe hacerse de forma asíncrona
         * @param {successCallback} success Función de tipo 'callback' encargada de procesar si la ejecución fue exitosa
         * @param {failCallback} fail Función de tipo 'callback' encargada de procesar si ocurre alguna falla en la ejecución
         */
        Create: function (smc, async, success, fail) {
            $.ajax({
                type: 'POST',
                contentType: 'application/json; charset=utf-8',
                dataType: 'json',
                data: JSON.stringify(smc),
                url: constants.logic.api.base + 'SMC/Create',
                async: async,
                success: function (data) {
                    if (typeof success !== 'undefined')
                        success(data);
                },
                error: function (jqXHR, textStatus, errorThrown) {
                    if (typeof fail !== 'undefined')
                        fail(jqXHR, textStatus, errorThrown);
                }
            });
        },
        /**
         * Recupera un mensaje por medio de su identificador
         * @param {number} id Identificador del mensaje
         * @param {boolean} async Indica si la ejecución debe hacerse de forma asíncrona
         * @param {successCallback} success Función de tipo 'callback' encargada de procesar si la ejecución fue exitosa
         * @param {failCallback} fail Función de tipo 'callback' encargada de procesar si ocurre alguna falla en la ejecución
         */
        Message: function (id, async, success, fail) {
            $.ajax({
                type: 'GET',
                url: constants.logic.api.base + 'SMC/Message?ID=' + id,
                async: async,
                success: function (data) {
                    if (typeof success !== 'undefined')
                        success(data);
                },
                error: function (jqXHR, textStatus, errorThrown) {
                    if (typeof fail !== 'undefined')
                        fail(jqXHR, textStatus, errorThrown);
                }
            });
        },
        /**
         * Actualiza un registro de la tabla 'SMC' por medio de su clave.
         * @param {any} smc Estructura de datos de tipo SMC
         * @param {boolean} async Indica si la ejecución debe hacerse de forma asíncrona
         * @param {successCallback} success Función de tipo 'callback' encargada de procesar si la ejecución fue exitosa
         * @param {failCallback} fail Función de tipo 'callback' encargada de procesar si ocurre alguna falla en la ejecución
         */
        Update: function (smc, async, success, fail) {
            $.ajax({
                type: 'PUT',
                url: constants.logic.api.base + 'SMC/Update',
                async: async,
                success: function (data) {
                    if (typeof success !== 'undefined')
                        success(data);
                },
                error: function (jqXHR, textStatus, errorThrown) {
                    if (typeof fail !== 'undefined')
                        fail(jqXHR, textStatus, errorThrown);
                }
            });
        },
        /**
         * Elimina un registro de la tabla 'SMC' por medio de su clave.
         * @param {number} id Identificador del mensaje
         * @param {boolean} async Indica si la ejecución debe hacerse de forma asíncrona
         * @param {successCallback} success Función de tipo 'callback' encargada de procesar si la ejecución fue exitosa
         * @param {failCallback} fail Función de tipo 'callback' encargada de procesar si ocurre alguna falla en la ejecución
         */
        Delete: function (id, async, success, fail) {
            $.ajax({
                type: 'DELETE',
                url: constants.logic.api.base + 'SMC/Delete?ID=' + id,
                async: async,
                success: function (data) {
                    if (typeof success !== 'undefined')
                        success(data);
                },
                error: function (jqXHR, textStatus, errorThrown) {
                    if (typeof fail !== 'undefined')
                        fail(jqXHR, textStatus, errorThrown);
                }
            });
        },
        /**
         * Recupera la cantidad de registros existente en la tabla 'SMC'.
         * @param {boolean} async Indica si la ejecución debe hacerse de forma asíncrona
         * @param {successCallback} success Función de tipo 'callback' encargada de procesar si la ejecución fue exitosa
         * @param {failCallback} fail Función de tipo 'callback' encargada de procesar si ocurre alguna falla en la ejecución
         */
        RecordCount: function (async, success, fail) {
            $.ajax({
                type: 'GET',
                url: constants.logic.api.base + 'SMC/RecordCount',
                async: async,
                success: function (data) {
                    if (typeof success !== 'undefined')
                        success(data);
                },
                error: function (jqXHR, textStatus, errorThrown) {
                    if (typeof fail !== 'undefined')
                        fail(jqXHR, textStatus, errorThrown);
                }
            });
        },
        /**
         * Mensajes recibidos por un usuario
         * @param {number} userid Código del usuario
         * @param {boolean} async Indica si la ejecución debe hacerse de forma asíncrona
         * @param {successCallback} success Función de tipo 'callback' encargada de procesar si la ejecución fue exitosa
         * @param {failCallback} fail Función de tipo 'callback' encargada de procesar si ocurre alguna falla en la ejecución
         */
        InBox: function (userid, async, success, fail) {
            $.ajax({
                type: 'GET',
                url: constants.logic.api.base + 'SMC/InBox?userId=' + userid,
                async: async,
                success: function (data) {
                    if (typeof success !== 'undefined')
                        success(data);
                },
                error: function (jqXHR, textStatus, errorThrown) {
                    if (typeof fail !== 'undefined')
                        fail(jqXHR, textStatus, errorThrown);
                }
            });
        },
        /**
         * Mensajes enviados por un usuario
         * @param {number} userid Código del usuario
         * @param {boolean} async Indica si la ejecución debe hacerse de forma asíncrona
         * @param {successCallback} success Función de tipo 'callback' encargada de procesar si la ejecución fue exitosa
         * @param {failCallback} fail Función de tipo 'callback' encargada de procesar si ocurre alguna falla en la ejecución
         */
        SendItems: function (userid, async, success, fail) {
            $.ajax({
                type: 'GET',
                url: constants.logic.api.base + 'SMC/SendItems?userId=' + userid,
                async: async,
                success: function (data) {
                    if (typeof success !== 'undefined')
                        success(data);
                },
                error: function (jqXHR, textStatus, errorThrown) {
                    if (typeof fail !== 'undefined')
                        fail(jqXHR, textStatus, errorThrown);
                }
            });
        },
        /**
         * Información de la cantidad de mensajes en cada buzón
         * @param {number} userid Código del usuario
         * @param {boolean} async Indica si la ejecución debe hacerse de forma asíncrona
         * @param {successCallback} success Función de tipo 'callback' encargada de procesar si la ejecución fue exitosa
         * @param {failCallback} fail Función de tipo 'callback' encargada de procesar si ocurre alguna falla en la ejecución
         */
        MailBoxInformation: function (userid, async, success, fail) {
            $.ajax({
                type: 'GET',
                url: constants.logic.api.base + 'SMC/MailBoxInformation?userId=' + userid,
                async: async,
                success: function (data) {
                    if (typeof success !== 'undefined')
                        success(data);
                },
                error: function (jqXHR, textStatus, errorThrown) {
                    if (typeof fail !== 'undefined')
                        fail(jqXHR, textStatus, errorThrown);
                }
            });
        },
        /**
         * 
         * @param {any} filter Parameter0
         * @param {boolean} async Indica si la ejecución debe hacerse de forma asíncrona
         * @param {successCallback} success Función de tipo 'callback' encargada de procesar si la ejecución fue exitosa
         * @param {failCallback} fail Función de tipo 'callback' encargada de procesar si ocurre alguna falla en la ejecución
         */
        Usuarios: function (filter, async, success, fail) {
            $.ajax({
                type: 'GET',
                url: constants.logic.api.base + 'SMC/Usuarios?filter=' + filter,
                async: async,
                success: function (data) {
                    if (typeof success !== 'undefined')
                        success(data);
                },
                error: function (jqXHR, textStatus, errorThrown) {
                    if (typeof fail !== 'undefined')
                        fail(jqXHR, textStatus, errorThrown);
                    else
                        app.core.ErrorHandler(jqXHR, textStatus, errorThrown);
                }
            });
        },
        /**
         * Borradores de mensajes de un usuario
         * @param {number} userid Código del usuario
         * @param {boolean} async Indica si la ejecución debe hacerse de forma asíncrona
         * @param {successCallback} success Función de tipo 'callback' encargada de procesar si la ejecución fue exitosa
         * @param {failCallback} fail Función de tipo 'callback' encargada de procesar si ocurre alguna falla en la ejecución
         */
        Drafts: function (userid, async, success, fail) {
            $.ajax({
                type: 'GET',
                url: constants.logic.api.base + 'SMC/Drafts?userId=' + userid,
                async: async,
                success: function (data) {
                    if (typeof success !== 'undefined')
                        success(data);
                },
                error: function (jqXHR, textStatus, errorThrown) {
                    if (typeof fail !== 'undefined')
                        fail(jqXHR, textStatus, errorThrown);
                }
            });
        },
        /**
         * Mensajes eliminados por un usuario
         * @param {number} userid Código del usuario
         * @param {boolean} async Indica si la ejecución debe hacerse de forma asíncrona
         * @param {successCallback} success Función de tipo 'callback' encargada de procesar si la ejecución fue exitosa
         * @param {failCallback} fail Función de tipo 'callback' encargada de procesar si ocurre alguna falla en la ejecución
         */
        Trash: function (userid, async, success, fail) {
            $.ajax({
                type: 'GET',
                url: constants.logic.api.base + 'SMC/Trash?userId=' + userid,
                async: async,
                success: function (data) {
                    if (typeof success !== 'undefined')
                        success(data);
                },
                error: function (jqXHR, textStatus, errorThrown) {
                    if (typeof fail !== 'undefined')
                        fail(jqXHR, textStatus, errorThrown);
                }
            });
        },
    };
})();
