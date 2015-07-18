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
    })(Config = MQPractice.Config || (MQPractice.Config = {}));
})(MQPractice || (MQPractice = {}));
MQPractice.Config.Config.$inject = ['$stateProvider', '$urlRouterProvider'];
myApp.configApp(MQPractice.Config.Config);
var MQPractice;
(function (MQPractice) {
    var Controller;
    (function (Controller) {
        var MainController = (function () {
            function MainController($scope) {
                var self = this;
                self.$scope = $scope;
                self.$scope.controller = self;
                self.$scope.greeting = "Hellow World~~";
            }
            return MainController;
        })();
        Controller.MainController = MainController;
    })(Controller = MQPractice.Controller || (MQPractice.Controller = {}));
})(MQPractice || (MQPractice = {}));
MQPractice.Controller.MainController.$inject = ['$scope'];
myApp.addController('mainController', MQPractice.Controller.MainController);
//# sourceMappingURL=app.js.map