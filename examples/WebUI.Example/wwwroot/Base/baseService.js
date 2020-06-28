var url = require('url');
var baseService = (function () {
    function baseService() {
    }
    baseService.prototype.requestPost = function (route, parameters) {
        throw new Error("Method not implemented.");
    };
    baseService.prototype.requestPut = function (route, parameters) {
        throw new Error("Method not implemented.");
    };
    baseService.prototype.requestGet = function (route, parameters) {
        throw new Error("Method not implemented.");
    };
    baseService.prototype.requestDelete = function (route, parameters) {
        throw new Error("Method not implemented.");
    };
    baseService.prototype.getRoute = function (controllerRoute, actionRoute) {
        if (!actionRoute.startsWith('/') || !controllerRoute.endsWith('/')) {
            controllerRoute += '/';
        }
        return url.resolve(controllerRoute, actionRoute);
    };
    return baseService;
}());
export { baseService };
//# sourceMappingURL=baseService.js.map