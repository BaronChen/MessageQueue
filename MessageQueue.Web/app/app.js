var MQPractice;
(function (MQPractice) {
    var App;
    (function (App) {
        var MyApp = (function () {
            function MyApp(name, modules) {
                this.app = angular.module(name, modules);
                //this.app.run([]);
            }
            MyApp.prototype.addController = function (name, controller) {
                this.app.controller(name, controller);
            };
            MyApp.prototype.addService = function (name, service) {
                this.app.service(name, service);
            };
            MyApp.prototype.addDirective = function (name, directive) {
                this.app.directive(name, directive);
            };
            MyApp.prototype.configApp = function (config) {
                this.app.config(config);
            };
            return MyApp;
        })();
        App.MyApp = MyApp;
    })(App = MQPractice.App || (MQPractice.App = {}));
})(MQPractice || (MQPractice = {}));
var myApp = new MQPractice.App.MyApp('MQPractice', ['ngAnimate', 'ui.bootstrap', 'ngRoute', 'ui.router', 'ngResource']);
var MQPractice;
(function (MQPractice) {
    var Config;
    (function (Config_1) {
        var Config = (function () {
            function Config($stateProvider, $urlRouterProvider) {
                $urlRouterProvider.otherwise("/home");
                $stateProvider
                    .state('home', {
                    url: "/home",
                    templateUrl: "app/view/app.html"
                });
            }
            return Config;
        })();
        Config_1.Config = Config;
        var CommonSetting = (function () {
            function CommonSetting() {
            }
            CommonSetting.seriveBaseUrl = "http://localhost:5279/";
            return CommonSetting;
        })();
        Config_1.CommonSetting = CommonSetting;
    })(Config = MQPractice.Config || (MQPractice.Config = {}));
})(MQPractice || (MQPractice = {}));
MQPractice.Config.Config.$inject = ['$stateProvider', '$urlRouterProvider'];
myApp.configApp(MQPractice.Config.Config);
var MQPractice;
(function (MQPractice) {
    var Controller;
    (function (Controller) {
        var MainController = (function () {
            function MainController($scope, processTaskService, $modal, $interval) {
                var self = this;
                self.$scope = $scope;
                self.$scope.controller = self;
                self.$scope.maxJob = 10;
                self.$scope.completedJob = 0;
                self.$scope.greeting = "Message Queue Test Client";
                self.processTaskService = processTaskService;
                self.jobsInqueue = new MQPractice.Model.JobsInQueue();
                self.$modal = $modal;
                self.$interval = $interval;
            }
            MainController.prototype.createProcessTasks = function () {
                var self = this;
                self.$scope.completedJob = 0;
                self.jobsInqueue = new MQPractice.Model.JobsInQueue();
                var modalInstance = self.$modal.open({
                    animation: true,
                    templateUrl: 'jobStatusModal.html',
                    scope: self.$scope,
                    backdrop: 'static',
                    keyboard: false
                });
                for (var i = 0; i < this.$scope.maxJob; i++) {
                    self.processTaskService.createSingleProcessTask().then(function (result) {
                        //alert(result.message);
                        self.jobsInqueue.createdJobIds.push(result.jobId);
                        self.jobsInqueue.processingJobIds.push(result.jobId);
                    }, function (result) {
                        alert(result.message);
                    });
                }
                var repeater = self.$interval(function () {
                    self.updateJobStatus(self);
                }, 1000);
                var unregister = self.$scope.$watch("completedJob", function (newValue, oldValue) {
                    if (newValue === self.$scope.maxJob) {
                        modalInstance.close();
                        self.$interval.cancel(repeater);
                        unregister();
                    }
                });
            };
            MainController.prototype.updateJobStatus = function (self) {
                self.processTaskService.queryProcessTaskStatus(self.jobsInqueue).then(function (result) {
                    self.jobsInqueue = result;
                    self.$scope.completedJob = self.jobsInqueue.completeJobIds.length;
                }, function (result) {
                    alert(result.message);
                });
            };
            return MainController;
        })();
        Controller.MainController = MainController;
    })(Controller = MQPractice.Controller || (MQPractice.Controller = {}));
})(MQPractice || (MQPractice = {}));
MQPractice.Controller.MainController.$inject = ['$scope', 'processTaskService', '$modal', '$interval'];
myApp.addController('mainController', MQPractice.Controller.MainController);
var MQPractice;
(function (MQPractice) {
    var Model;
    (function (Model) {
        var JobsInQueue = (function () {
            function JobsInQueue() {
                this.createdJobIds = [];
                this.completeJobIds = [];
                this.processingJobIds = [];
                this.errorJobIds = [];
                this.successJobIds = [];
            }
            return JobsInQueue;
        })();
        Model.JobsInQueue = JobsInQueue;
    })(Model = MQPractice.Model || (MQPractice.Model = {}));
})(MQPractice || (MQPractice = {}));
var MQPractice;
(function (MQPractice) {
    var Services;
    (function (Services) {
        var CreateProcessTaskResult = (function () {
            function CreateProcessTaskResult() {
            }
            return CreateProcessTaskResult;
        })();
        Services.CreateProcessTaskResult = CreateProcessTaskResult;
        var ProcessTaskService = (function () {
            function ProcessTaskService($http, $q) {
                var self = this;
                self.$http = $http;
                self.$q = $q;
            }
            ProcessTaskService.prototype.createSingleProcessTask = function () {
                var deferred = this.$q.defer();
                this.$http({
                    method: 'Post',
                    url: MQPractice.Config.CommonSetting.seriveBaseUrl + '/api/Process/ProcessTask'
                }).success(function (response) {
                    deferred.resolve(response);
                }).error(function (response) {
                    var result = new CreateProcessTaskResult();
                    if (!response) {
                        result.message = 'Unknown Error!';
                    }
                    else if (!response.message) {
                        result.message = 'Internal error!';
                    }
                    else {
                        result.message = response.message;
                    }
                    deferred.reject(result);
                });
                return deferred.promise;
            };
            ProcessTaskService.prototype.queryProcessTaskStatus = function (jobsInQueue) {
                var deferred = this.$q.defer();
                this.$http({
                    method: 'Post',
                    url: MQPractice.Config.CommonSetting.seriveBaseUrl + '/api/Process/QueryProcessTaskStatus',
                    data: jobsInQueue
                }).success(function (response) {
                    deferred.resolve(response);
                }).error(function (response) {
                    var result = new MQPractice.Model.JobsInQueue();
                    if (!response) {
                        result.message = 'Unknown Error!';
                    }
                    else if (!response.message) {
                        result.message = 'Internal error!';
                    }
                    else {
                        result.message = response.message;
                    }
                    deferred.reject(result);
                });
                return deferred.promise;
            };
            return ProcessTaskService;
        })();
        Services.ProcessTaskService = ProcessTaskService;
    })(Services = MQPractice.Services || (MQPractice.Services = {}));
})(MQPractice || (MQPractice = {}));
MQPractice.Services.ProcessTaskService.$inject = ['$http', '$q'];
myApp.addService('processTaskService', MQPractice.Services.ProcessTaskService);
//# sourceMappingURL=app.js.map