var db = null;

angular.module('starter', [
    'ionic',
    'ionic-datepicker',
    'ngCordova',
    'ngTygaSoft',
    'starter.controllers'
])

.run(function ($ionicPlatform, $ionicHistory, $rootScope, $cordovaToast, $cordovaDevice, $tygasoftDbHelper, $tygasoftLocalStorage, $tygasoftMC) {
    $ionicPlatform.ready(function () {

        //db = $cordovaSQLite.openDB({ name: "WmsDb.db", location: 1 });
        //$cordovaSQLite.execute(db, "CREATE TABLE IF NOT EXISTS KeyValue (Id integer primary key, KeyName nvarchar(100), ContentValue ntext,UserName nvarchar(50),Status nvarchar(20))");
        //$cordovaSQLite.execute(db, "CREATE TABLE IF NOT EXISTS Users (Id integer primary key, KeyName nvarchar(100), ContentValue ntext,UserName nvarchar(50),Status nvarchar(20))");
        //$cordovaSQLite.execute(db, "CREATE TABLE IF NOT EXISTS ShelfMission (Id integer primary key, KeyName nvarchar(100), ContentValue ntext,UserName nvarchar(50),Status nvarchar(20))");
        //$cordovaSQLite.execute(db, "CREATE TABLE IF NOT EXISTS ShelfMissionProduct (Id integer primary key, KeyName nvarchar(100), ContentValue ntext,UserName nvarchar(50),Status nvarchar(20))");

        // Hide the accessory bar by default (remove this to show the accessory bar above the keyboard
        // for form inputs)
        if (cordova.platformId === 'ios' && window.cordova && window.cordova.plugins && window.cordova.plugins.Keyboard) {
            cordova.plugins.Keyboard.hideKeyboardAccessoryBar(true);
            cordova.plugins.Keyboard.disableScroll(true);
        }
        if (window.StatusBar) {
            // org.apache.cordova.statusbar required
            StatusBar.styleDefault();
        }

        //var jDevice = { "Platform": "" + $cordovaDevice.getPlatform() + "", "UUID": "" + $cordovaDevice.getUUID() + "", "Version": "" + $cordovaDevice.getVersion() + "", "Latlng": "", "UserName": "", "AccessToken": "" };
        //$tygasoftDbHelper.DoInsert('KeyValue', 'Admin', $tygasoftMC.DataStatus.SysAdd, 'DeviceInfo', JSON.stringify(jDevice), false);
        //$tygasoftLocalStorage.Set("RefreshTimeout", 30);

        $ionicPlatform.registerBackButtonAction(function (e) {
            if ($ionicHistory.backView()) {
                $ionicHistory.goBack();
            }
            else {
                if ($rootScope.backButtonPressedOnceToExit) {
                    ionic.Platform.exitApp();
                } else {
                    $rootScope.backButtonPressedOnceToExit = true;
                    $cordovaToast.showShortCenter('再按一次离开');
                    setTimeout(function () {
                        $rootScope.backButtonPressedOnceToExit = false;
                    }, 2000);
                }
            }
            e.preventDefault();
            return false;
        }, 101);

        $tygasoftLocalStorage.Set("ServiceUrl", "http://www.tygaweb.com/wms");
        //RfidScan.onOpen();
        $tygasoftLocalStorage.Set("UhfOnOff", 0);

    });
})

.config(function ($stateProvider, $urlRouterProvider, $ionicConfigProvider, ionicDatePickerProvider) {

    // Ionic uses AngularUI Router which uses the concept of states
    // Learn more here: https://github.com/angular-ui/ui-router
    // Set up the various states which the app can be in.
    // Each state's controller can be found in controllers.js

    $ionicConfigProvider.platform.android.tabs.position('bottom');
    $ionicConfigProvider.platform.ios.tabs.position('bottom');
    //$ionicConfigProvider.platform.android.tabs.style('standard');
    $ionicConfigProvider.navBar.alignTitle('center');
    $ionicConfigProvider.scrolling.jsScrolling(true);

    ionicDatePickerProvider.configDatePicker({
        inputDate: new Date(),
        setLabel: '确定',
        todayLabel: '今天',
        closeLabel: '关闭',
        mondayFirst: false,
        weeksList: ["日", "一", "二", "三", "四", "五", "六"],
        monthsList: ["一月", "二月", "三月", "四月", "五月", "六月", "七月", "八月", "九月", "十月", "十一月", "十二月"],
        templateType: 'popup',
        showTodayButton: true,
        dateFormat: 'yyyy年MM月dd日',
        closeOnSelect: false,
        disableWeekdays: [6],
    });

    $stateProvider
    .state('tab.ShelfMission', {
        url: '/ShelfMission',
        views: {
            'tab-Home': {
                templateUrl: 'templates/ShelfMission.html',
                controller: 'ShelfMissionCtrl'
            }
        }
    })
    .state('tab.ShelfMissionProduct', {
        url: '/ShelfMissions/:item',
        views: {
            'tab-Home': {
                templateUrl: 'templates/ShelfMissionProduct.html',
                controller: 'ShelfMissionProductCtrl'
            }
        }
    })
    .state('tab.StockLocationProduct', {
        url: '/StockLocationProduct/:item/:key',
        views: {
            'tab-Home': {
                templateUrl: 'templates/StockLocationProduct.html',
                controller: 'StockLocationProductCtrl'
            }
        }
    })
    .state('tab.OrderPicked', {
        url: '/OrderPicked',
        views: {
            'tab-Home': {
                templateUrl: 'templates/OrderPicked.html',
                controller: 'OrderPickedCtrl'
            }
        }
    })
    .state('tab.OrderPickProduct', {
        url: '/OrderPicks/:item',
        views: {
            'tab-Home': {
                templateUrl: 'templates/OrderPickProduct.html',
                controller: 'OrderPickProductCtrl'
            }
        }
    })
    .state('tab.StockProduct', {
        url: '/StockProduct',
        views: {
            'tab-Home': {
                templateUrl: 'templates/StockProduct.html',
                controller: 'StockProductCtrl'
            }
        }
    })
    .state('tab.Pandian', {
         url: '/Pandian',
         views: {
             'tab-Home': {
                 templateUrl: 'templates/Pandian.html',
                 controller: 'PandianCtrl'
             }
         }
    })
    .state('tab.PandianProduct', {
        url: '/Pandians/:item',
        views: {
            'tab-Home': {
                templateUrl: 'templates/PandianProduct.html',
                controller: 'PandianProductCtrl'
            }
        }
    })

    .state('tab.Rfid', {
        url: '/Rfid',
        views: {
            'tab-Home': {
                templateUrl: 'templates/Rfid.html',
                controller: 'RfidCtrl'
            }
        }
    })

    .state('tab.AppPackage', {
        url: '/AppPackage',
        views: {
            'tab-Sys': {
                templateUrl: 'templates/AppPackage.html',
                controller: 'AppPackageCtrl'
            }
        }
    })

    .state('tab.ListImportData', {
        url: '/ListImportData',
        views: {
            'tab-Found': {
                templateUrl: 'templates/Sys/ListImportData.html',
                controller: 'ListImportDataCtrl'
            }
        }
    })

    .state('tab.SysSet', {
        url: '/SysSet',
        views: {
            'tab-Sys': {
                templateUrl: 'templates/SysSet.html',
                controller: 'SysSetCtrl'
            }
        }
    })

    .state('tab', {
          url: '/tab',
          abstract: true,
          templateUrl: 'templates/tabs.html'
    })

    .state('tab.Home', {
        url: '/Home',
        views: {
            'tab-Home': {
                templateUrl: 'templates/tab-Home.html',
                controller: 'HomeCtrl'
            }
        }
    })

    .state('tab.Found', {
        url: '/Found',
        views: {
            'tab-Found': {
                templateUrl: 'templates/tab-Found.html',
                controller: 'FoundCtrl'
            }
        }
    })

    .state('tab.Sys', {
        url: '/Sys',
        views: {
            'tab-Sys': {
                templateUrl: 'templates/tab-Sys.html',
                controller: 'SysCtrl'
            }
        }
    });

    // if none of the above states are matched, use this as the fallback
    $urlRouterProvider.otherwise('/tab/Home');

});
