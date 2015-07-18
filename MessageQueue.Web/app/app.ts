module MQPractice.App {
	
	export class MyApp {
		app: ng.IModule;

		constructor(name: string, modules: Array<string>) {
			this.app = angular.module(name, modules);

			//this.app.run([]);
		}

		addController(name: string, controller: Function) {
			this.app.controller(name, controller);
		}

		addService(name: string, service: Function) {
			this.app.service(name, service);
		}

		addDirective(name: string, directive: ng.IDirectiveFactory) {
			this.app.directive(name, directive);
		}

		configApp(config:Function) {
			this.app.config(config);
		}
	}
}

var myApp = new MQPractice.App.MyApp('MQPractice', ['ngAnimate',  'ui.bootstrap', 'ngRoute', 'ui.router', 'ngResource']);