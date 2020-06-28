const url = require('url');

export class baseService {
	requestPost<T>(route: void, parameters): T {
		throw new Error("Method not implemented.");
	}
	requestPut<T>(route: void, parameters): T {
		throw new Error("Method not implemented.");
	}
	requestGet<T>(route: void, parameters): T {
		throw new Error("Method not implemented.");
	}
	requestDelete<T>(route: void, parameters): T {
		throw new Error("Method not implemented.");
	}
	getRoute(controllerRoute: any, actionRoute: string) {
		if (!actionRoute.startsWith('/') || !controllerRoute.endsWith('/')) {
			controllerRoute += '/';
		}
		return url.resolve(controllerRoute, actionRoute);
	}
}