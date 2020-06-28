import { __extends } from "tslib";
import { baseService } from '../../Base/baseService';
var ValuesService = (function (_super) {
    __extends(ValuesService, _super);
    function ValuesService() {
        var _this = _super !== null && _super.apply(this, arguments) || this;
        _this.controllerRoute = 'api/Values';
        return _this;
    }
    ValuesService.prototype.getValues = function () {
        var parameters = {};
        var actionRoute = 'GetValues';
        var route = this.getRoute(this.controllerRoute, actionRoute);
        return this.requestGet(route, parameters);
    };
    ValuesService.prototype.getValue = function (id) {
        var parameters = { id: id };
        var actionRoute = 'GetValue';
        var route = this.getRoute(this.controllerRoute, actionRoute);
        return this.requestGet(route, parameters);
    };
    ValuesService.prototype.postValue = function (value) {
        var parameters = { value: value };
        var actionRoute = 'PostValue';
        var route = this.getRoute(this.controllerRoute, actionRoute);
        return this.requestPost(route, parameters);
    };
    ValuesService.prototype.putValue = function (id, value) {
        var parameters = { id: id, value: value };
        var actionRoute = 'PutValue';
        var route = this.getRoute(this.controllerRoute, actionRoute);
        return this.requestPut(route, parameters);
    };
    ValuesService.prototype.deleteValue = function (id) {
        var parameters = { id: id };
        var actionRoute = 'DeleteValue';
        var route = this.getRoute(this.controllerRoute, actionRoute);
        return this.requestDelete(route, parameters);
    };
    return ValuesService;
}(baseService));
export { ValuesService };
//# sourceMappingURL=ValuesService.js.map