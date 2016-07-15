(function() {
  'use strict'
  angular.module("GerenciadorArquivo").config(function($routeProvider) {

    $routeProvider.when("/home", {
      templateUrl: "app/views/home.html",
      controller: "homeController"
    });

    $routeProvider.otherwise({
      redirectTo: "/home"
    });

  });

})();
