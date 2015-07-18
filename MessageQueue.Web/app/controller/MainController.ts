module MQPractice.Controller {
	export interface IMainCtrlScope extends ng.IScope{
		greeting: string; 
		controller : MainController;
	}

	export class MainController
	{
		private $scope : IMainCtrlScope;

		constructor($scope: IMainCtrlScope) {
			var self = this;
			self.$scope = $scope;
			self.$scope.controller = self;

			self.$scope.greeting = "Hellow World~~";
		}
	}
}

MQPractice.Controller.MainController.$inject = ['$scope'];
myApp.addController('mainController', MQPractice.Controller.MainController);