angular.module("CinemaApp").controller("ScheduleController", function ($scope, $http, $log)
{
    BlockUi();
    var config = angular.fromJson(angular.element(document.querySelector('#initializationData')).html());
    GetData($scope, $http, $log, config);
    UnblockUi();
});     // getting the existing module and add a new controller


// FUNCTIONS //////////////////////////////////////////////////////////////////////////////////////
function GetData($scope, $http, $log, config)
{
    $http.get(config.GetAllCinemasUrl).then(function(result)
    {
        $scope.Cinemas = result.data;
        $scope.SelectedCinema = $scope.Cinemas[0];

        return $http.get(config.GetAllFilmsUrl);
    }).then(function (result)
    {
        $scope.AvalibleFilms = result.data;

        InitEvents($scope);
    }).catch(function(error)
    {
        var message = "An error occured: " + error.data.Message;
        $log.error(message);
        alert(message);
    }).finally(function()
    {
        $log.info("Loading of data was finished!");
    });
}
function InitEvents($scope)
{
    $scope.OkBtnClick = function ()
    {

    }
    $scope.CancelBtnClick = function ()
    {

    }
    $scope.RemoveBtnClick = function ()
    {

    }
    $scope.OnCinemaChange = function ()
    {
        
    }
    $scope.OnFilmChange = function ()
    {
        
    }
    $scope.OnDateChange = function ()
    {

    }
    $scope.TestBtnClick = function ()
    {
        
    }
}