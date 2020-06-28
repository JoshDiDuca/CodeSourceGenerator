
/*
 * CHANGES WILL BE OVERRIDEN
 * Auto generated for source object: ValuesController
 * Template: Typescript/Templates/typescriptService.scriban
 */
 




import { ValuesResponseViewModel } from '../Models/ValuesResponseViewModel';
import { ValuesRequestViewModel } from '../Models/ValuesRequestViewModel';



import { baseService } from '../../Base/baseService';

export class ValuesService extends baseService {
	private controllerRoute = 'api/Values';
	
	public getValues() : string[] {
		const parameters = {};
		const actionRoute = 'GetValues';
		const route = this.getRoute(this.controllerRoute, actionRoute);
		return this.requestGet<string[]>(route, parameters);
	}
	
	public getValue(id : number) : string {
		const parameters = {id};
		const actionRoute = 'GetValue';
		const route = this.getRoute(this.controllerRoute, actionRoute);
		return this.requestGet<string>(route, parameters);
	}
	
	public postValue(value : ValuesRequestViewModel) : ValuesResponseViewModel {
		const parameters = {value};
		const actionRoute = 'PostValue';
		const route = this.getRoute(this.controllerRoute, actionRoute);
		return this.requestPost<ValuesResponseViewModel>(route, parameters);
	}
	
	public putValue(id : number, value : string) : void {
		const parameters = {id, value};
		const actionRoute = 'PutValue';
		const route = this.getRoute(this.controllerRoute, actionRoute);
		return this.requestPut<void>(route, parameters);
	}
	
	public deleteValue(id : number) : void {
		const parameters = {id};
		const actionRoute = 'DeleteValue';
		const route = this.getRoute(this.controllerRoute, actionRoute);
		return this.requestDelete<void>(route, parameters);
	}
	
}