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
constants.fasiApi.version = 'v1/';
//constants.fasiApi.base = 'http://34.228.171.165:8082/FASI/api/';
constants.fasiApi.base = 'https://host.docker.internal:8090/FASI/api/';
constants.logic = {};
constants.logic.api = {};
constants.logic.api.base = 'https://host.docker.internal:8084/logic/api/';
//constants.fasiApi.base = 'http://localhost:26816/api/';
constants.fasiApi.backoffice = constants.fasiApi.base + 'backoffice/' + constants.fasiApi.version;
constants.fasiApi.diary = constants.fasiApi.base + 'diary/' + constants.fasiApi.version;
constants.fasiApi.fasi = constants.fasiApi.base + 'fasi/' + constants.fasiApi.version;
constants.fasiApi.members = constants.fasiApi.base + 'members/' + constants.fasiApi.version;
constants.fasiApi.notifications = constants.fasiApi.base + 'notifications/' + constants.fasiApi.version;
constants.fasiApi.smc = constants.fasiApi.base + 'smc/' + constants.fasiApi.version;
constants.fasiApi.widgetSupport = constants.fasiApi.base + 'widget/' + constants.fasiApi.version;

