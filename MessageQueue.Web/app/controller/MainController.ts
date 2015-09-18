module MQPractice.Controller {
	import ModalService = angular.ui.bootstrap.IModalService;

	export interface IMainCtrlScope extends ng.IScope{
		greeting: string; 
		controller: MainController;
		maxJob: number;
		completedJob : number;
	}

	export class MainController
	{
		private $scope : IMainCtrlScope;
		private processTaskService : Services.ProcessTaskService;

		private jobsInqueue: Model.JobsInQueue;
		private $modal: ModalService;
		private $interval: ng.IIntervalService;

		constructor($scope: IMainCtrlScope, processTaskService: Services.ProcessTaskService, $modal: ModalService, $interval:ng.IIntervalService) {
			var self = this;
			self.$scope = $scope;
			self.$scope.controller = self;
			self.$scope.maxJob = 10;
			self.$scope.completedJob = 0;

			self.$scope.greeting = "Message Queue Test Client";

			self.processTaskService = processTaskService;

			self.jobsInqueue = new Model.JobsInQueue();

			self.$modal = $modal;

			self.$interval = $interval;
		}

		createProcessTasks() {
			var self = this;
			self.$scope.completedJob = 0;
			self.jobsInqueue = new Model.JobsInQueue();
			var modalInstance = self.$modal.open({
				animation: true,
				templateUrl: 'jobStatusModal.html',
				scope: self.$scope,
				backdrop: 'static',
				keyboard: false
			});

			for (var i = 0; i < this.$scope.maxJob; i++) {
				self.processTaskService.createSingleProcessTask().then((result: Services.CreateProcessTaskResult) => {
					//alert(result.message);
					self.jobsInqueue.createdJobIds.push(result.jobId);
					self.jobsInqueue.processingJobIds.push(result.jobId);
				}, (result: Services.CreateProcessTaskResult) => {
					alert(result.message);
				});
			}

			var repeater = self.$interval(() => {
				self.updateJobStatus(self);
			}, 1000);

			var unregister = self.$scope.$watch("completedJob", (newValue, oldValue) => {
				if (newValue === self.$scope.maxJob) {
					modalInstance.close();
					self.$interval.cancel(repeater);
					unregister();
				}
			});
		}

		updateJobStatus(self : MainController) {
			self.processTaskService.queryProcessTaskStatus(self.jobsInqueue).then((result: Model.JobsInQueue) => {
				self.jobsInqueue = result;
				self.$scope.completedJob = self.jobsInqueue.completeJobIds.length;
			}, (result: Services.CreateProcessTaskResult) => {
				alert(result.message);
			});
		}

	}
}

MQPractice.Controller.MainController.$inject = ['$scope', 'processTaskService', '$modal', '$interval'];
myApp.addController('mainController', MQPractice.Controller.MainController);