
angular.module('ngTygaSoft.services.DbHelper', [])

.factory('$tygasoftDbHelper', function ($cordovaSQLite, $q) {

    var dal = {};

    dal.DropTable = function () {
        $cordovaSQLite.execute(db, "DROP TABLE KeyValue");
    };

    dal.Insert = function (tableName, userName, status, key, value) {
        var q = $q.defer();

        var sql = "INSERT INTO " + tableName + " (KeyName, ContentValue,UserName,Status) VALUES (?,?,?,?) ";
        $cordovaSQLite.execute(db, sql, [key, value, userName, status]).then(function (res) {
            return q.resolve(res.insertId);
        }, function (err) {
            return q.reject(0);
        });

        return q.promise;
    };

    dal.Update = function (tableName, userName, status, key, value) {
        var q = $q.defer();

        var sql = "update " + tableName + " set ContentValue = ?,UserName = ?,Status = ? where KeyName = ? ";
        $cordovaSQLite.execute(db, sql, [value, userName, status, key]).then(function (res) {
            return q.resolve(res.rowsAffect);
        }, function (err) {
            return q.reject(0);
        });

        return q.promise;
    };

    dal.DeleteById = function (tableName, Id) {
        var q = $q.defer();
        var sql = 'DELETE FROM ' + tableName + ' WHERE Id = ?';
        $cordovaSQLite.execute(db, sql, [Id]).then(function (res) {
            return q.resolve(res.rowsAffect);
        }, function (err) {
            return q.resolve(0);
        });
        return q.promise;
    };

    dal.Delete = function (tableName, key) {
        var q = $q.defer();
        var sql = 'DELETE FROM ' + tableName + ' WHERE KeyName = ?';
        $cordovaSQLite.execute(db, sql, [key]).then(function (res) {
            return q.resolve(res.rowsAffect);
        }, function (err) {
            return q.resolve(0);
        });
        return q.promise;
    };

    dal.DeleteAll = function (tableName) {
        var sql = 'DELETE FROM ' + tableName + '';
        $cordovaSQLite.execute(db, sql).then(function (res) {
            return res.rowsAffect;
        }, function (err) {
            return 0;
        });
    };

    dal.GetAll = function (tableName) {
        var q = $q.defer();

        var sql = "select * from " + tableName + " ";
        $cordovaSQLite.execute(db, sql, []).then(function (res) {
            if (res.rows.length == 0) return q.resolve(null);
            return q.resolve(res.rows);
        }, function (err) {
            return q.reject(null);
        });

        return q.promise;
    };

    dal.ExecuteReader = function (tableName, sqlWhere) {
        var q = $q.defer();

        var sql = "select * from " + tableName + " where 1=1 " + sqlWhere + "";
        $cordovaSQLite.execute(db, sql, []).then(function (res) {
            if (res.rows.length == 0) return q.resolve(null);
            return q.resolve(res.rows);
        }, function (err) {
            return q.reject(err);
        });

        return q.promise;
    };

    dal.GetValueByKey = function (tableName, key) {
        var q = $q.defer();

        var sql = "select ContentValue from " + tableName + " where KeyName = ? ";
        $cordovaSQLite.execute(db, sql, [key]).then(function (res) {
            if (res.rows.length == 0) return q.resolve(null);
            return q.resolve(res.rows.item(0).ContentValue);
        }, function (err) {
            return q.reject(err);
        });

        return q.promise;
    };

    dal.GetTotal = function (tableName) {
        var q = $q.defer();

        var sql = "select count(*) Total from " + tableName + " ";
        $cordovaSQLite.execute(db, sql, []).then(function (res) {
            if (res.rows.length == 0) return q.resolve(null);
            return q.resolve(res.rows.item(0).Total);
        }, function (err) {
            return q.reject(err);
        });

        return q.promise;
    };

    dal.CallGetValueByKey = function (tableName, key, callbackFun) {
        var sql = "select ContentValue from " + tableName + " where KeyName = ? ";
        $cordovaSQLite.execute(db, sql, [key]).then(function (res) {
            callbackFun(res);
        });
    };

    dal.CallGetValueByKeyLike = function (tableName, key, callbackFun) {
        var sql = "select ContentValue from " + tableName + " where KeyName like '%" + key + "%' ";
        $cordovaSQLite.execute(db, sql, []).then(function (res) {
            callbackFun(res);
        });
    };

    dal.CallSearch = function (tableName, sqlWhere, callbackFun) {
        var sql = "select * from " + tableName + " where 1=1 " + sqlWhere + "";
        $cordovaSQLite.execute(db, sql, []).then(function (res) {
            callbackFun(res);
        });
    };

    dal.CallGetAll = function (tableName) {
        var q = $q.defer();

        var sql = "select * from " + tableName + " ";
        $cordovaSQLite.execute(db, sql, []).then(function (res) {
            if (res.rows.length == 0) return q.resolve(null);
            return q.resolve(res.rows);
        }, function (err) {
            return q.reject(err);
        });

        return q.promise;
    };

    dal.DoInsert = function (tableName, userName, status, key, value, isAways) {
        var q = $q.defer();

        dal.GetValueByKey(tableName, key).then(function (res) {
            if (res) {
                if (isAways) {
                    dal.Update(tableName, userName, status, key, value).then(function (res) {
                        return q.resolve(res);
                    }, function (err) {
                        return q.resolve(null);
                    })
                }
            }
            else {
                dal.Insert(tableName, userName, status, key, value).then(function (res) {
                    return q.resolve(res);
                }, function (err) {
                    return q.resolve(null);
                })
            }
            return q.resolve(1);
        })

        return q.promise;
    };

    return dal;
});