var app = angular.module("myApp", ["ngRoute"])
app.config(function ($routeProvider) {
    $routeProvider
    .when("/", {
        templateUrl: "views/index.html"
    });
});

app.controller("myCtrl", function ($http) {
    var ctrl = this;

    ctrl.numberOfUsers = "Loading data...";
    ctrl.grumpyUsers = "Loading data....";
    ctrl.happyUsers = "Loading data....";
    ctrl.mostActive = "Loading data....";

    $http.get("http://localhost:50518/api/tweets").then(function (res) {
        ctrl.numberOfUsers = res.data;
    });

    $http.get("http://localhost:50518/api/tweets/grumpy").then(function (res) {
        var users = "";
        for (var i = 0; i < res.data.length; i++) {
            users += res.data[i] + ", ";
        }
        ctrl.grumpyUsers = users;
    });

    $http.get("http://localhost:50518/api/tweets/happy").then(function (res) {
        var users = "";
        for (var i = 0; i < res.data.length; i++) {
            users += res.data[i] + ", ";
        }
        ctrl.happyUsers = users;
    });

    //$http.get("http://localhost:50518/api/tweets/active").then(function (res) {
    //    var users = "";
    //    for (var i = 0; i < res.data.length; i++) {
    //        users += res.data[i] + ", ";
    //    }
    //    ctrl.mostActive = users;
    //});
});