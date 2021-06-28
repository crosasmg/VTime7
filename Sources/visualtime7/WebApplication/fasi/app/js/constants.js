var app = {};
var index = 0;
var constants = {};
app.workers = [];

constants.defaultLanguageId = 2;
constants.defaultLanguageName = 'es';
constants.gridStackWidth = 12;
constants.gridStackNodeHeight = 6;
constants.defaultPage = '/fasi/default.aspx';
constants.logInPage = '/fasi/security/logIn.aspx';
constants.availableWidgetsPopup = '/fasi/widgets/configuration/widgetsPopup.html';
constants.timeout = 5;

constants.fasiApi = {};
constants.fasiApi.version = '';
constants.fasiApi.base = '';
constants.logic = {};
constants.logic.api = {};
constants.logic.api.base = '';
constants.fasiApi.backoffice = constants.fasiApi.base + 'backoffice/' + constants.fasiApi.version;
constants.fasiApi.diary = constants.fasiApi.base + 'diary/' + constants.fasiApi.version;
constants.fasiApi.fasi = constants.fasiApi.base + 'fasi/' + constants.fasiApi.version;
constants.fasiApi.members = constants.fasiApi.base + 'members/' + constants.fasiApi.version;
constants.fasiApi.notifications = constants.fasiApi.base + 'notifications/' + constants.fasiApi.version;
constants.fasiApi.smc = constants.fasiApi.base + 'smc/' + constants.fasiApi.version;
constants.fasiApi.widgetSupport = constants.fasiApi.base + 'widget/' + constants.fasiApi.version;

constantsSupport = (function () {
    this.fasiApiBase = function (endPoints) {
        var fasi = generalSupport.findKey(endPoints, "Fasi");
        var api = generalSupport.findKey(fasi, "Api");
        var base = generalSupport.findKey(api, "Base");
        return base;
    };

    this.logicApiBase = function (endPoints) {
        var fasi = generalSupport.findKey(endPoints, "Logic");
        var api = generalSupport.findKey(fasi, "Api");
        var base = generalSupport.findKey(api, "Base");
        return base;
    };

    this.fasiApiVersion = function (endPoints) {
        var fasi = generalSupport.findKey(endPoints, "Fasi");
        var api = generalSupport.findKey(fasi, "Api");
        var base = generalSupport.findKey(api, "Version");
        return base;
    };

    this.setup = function (data) {
        var endPoints = generalSupport.findKey(data.settings, "endPoints");
        constants.fasiApi.base = fasiApiBase(endPoints);
        constants.fasiApi.version = fasiApiVersion(endPoints);

        constants.logic.api.base = logicApiBase(endPoints);

        constants.fasiApi.backoffice = constants.fasiApi.base + 'backoffice/' + constants.fasiApi.version;
        constants.fasiApi.diary = constants.fasiApi.base + 'diary/' + constants.fasiApi.version;
        constants.fasiApi.fasi = constants.fasiApi.base + 'fasi/' + constants.fasiApi.version;
        constants.fasiApi.members = constants.fasiApi.base + 'members/' + constants.fasiApi.version;
        constants.fasiApi.notifications = constants.fasiApi.base + 'notifications/' + constants.fasiApi.version;
        constants.fasiApi.smc = constants.fasiApi.base + 'smc/' + constants.fasiApi.version;
        constants.fasiApi.widgetSupport = constants.fasiApi.base + 'widget/' + constants.fasiApi.version;

    };

    return {
        setup: function (data) {
            setup(data);
        }
    };
})();