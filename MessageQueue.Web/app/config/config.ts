module MQPractice.Config {
	
	export class Config {
		
		constructor($stateProvider: ng.ui.IStateProvider, $urlRouterProvider: ng.ui.IUrlRouterProvider) {
			$urlRouterProvider.otherwise("/home");

			$stateProvider
				.state('home', {
				url: "/home",
				templateUrl: "app/view/app.html"
			});
		}
	}
}

MQPractice.Config.Config.$inject = ['$stateProvider', '$urlRouterProvider'];
myApp.configApp(MQPractice.Config.Config);