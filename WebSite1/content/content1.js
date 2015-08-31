define(['angular'],

    function (angular) {
        'use strict';

        var allocationController = function ($scope) {
            $scope.menuItems = [
			    { title: 'By Strategy', selected: 'true' },
			    { title: 'By Custodian', selected: 'false' }
            ];
        }

        allocationController.$inject = [
            '$scope'
        ];

        return allocationController;
    });