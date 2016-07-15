(function() {
  'use strict';

  angular.module('GerenciadorArquivo')
    .controller('homeController', homeController);

  function homeController($scope, Upload, $timeout) {
    var vm = this;


    /**Pega o arquivo da página e atribui em uma váriavel*/
    $scope.uploadFiles = function(file) {
      $scope.f = file;
      console.log($scope.f);
      salvarArquivo(file);
    }

    /**Envia a imagem para o servidor para que possa ser tratada, envia apenas quando clicar no botão salvar*/
    var salvarArquivo = function(file) {
      if (file && !file.$error) {
        file.upload = Upload.upload({

          url: 'http://localhost:15538/api/Arquivos/upload',
          data: {
            file: file
          }
        });

        file.upload.then(function(response) {
          $timeout(function() {
            file.result = response.data;
            console.log("REsposta: " + response.data);
          });
        }, function(response) {
          if (response.status > 0)
            $scope.errorMsg = response.status + ': ' + response.data;
        });

        file.upload.progress(function(evt) {
          file.progress = Math.min(100, parseInt(100.0 *
            evt.loaded / evt.total));
        });
      }
    }

  }
})();
