module MQPractice.Services {
	
	export class CreateProcessTaskResult {
		public message: string;
		public jobId : string;
	}

	export class ProcessTaskService {
		
		private $http : ng.IHttpService;
		private $q : ng.IQService;

		constructor($http : ng.IHttpService, $q : ng.IQService) {
			var self = this;
			self.$http = $http;
			self.$q = $q;
		}

		createSingleProcessTask() : ng.IPromise<CreateProcessTaskResult> {
			var deferred = this.$q.defer();

			this.$http({
				method: 'Post',
				url: Config.CommonSetting.seriveBaseUrl + '/api/Process/ProcessTask'
			}).success(response => {
				deferred.resolve(response);
			}).error(response => {
				var result = new CreateProcessTaskResult();
				if (!response) {
					result.message = 'Unknown Error!';
				}else if (!response.message) {
					result.message = 'Internal error!';
				} else {
					result.message = response.message;
				}

				deferred.reject(result);
			});

			return deferred.promise;
		}

		queryProcessTaskStatus(jobsInQueue : Model.JobsInQueue): ng.IPromise<Model.JobsInQueue> {

			var deferred = this.$q.defer();

			this.$http({
				method: 'Post',
				url: Config.CommonSetting.seriveBaseUrl + '/api/Process/QueryProcessTaskStatus',
				data: jobsInQueue
			}).success(response => {
				deferred.resolve(response);
			}).error(response => {
				var result = new Model.JobsInQueue();
				if (!response) {
					result.message = 'Unknown Error!';
				} else if (!response.message) {
					result.message = 'Internal error!';
				} else {
					result.message = response.message;
				}

				deferred.reject(result);
			});

			return deferred.promise;
		}
	}

}

MQPractice.Services.ProcessTaskService.$inject = ['$http', '$q'];
myApp.addService('processTaskService', MQPractice.Services.ProcessTaskService);


