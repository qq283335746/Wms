angular.module('ngTygaSoft.services.Rfid', [])

.factory('$tygasoftRfid', function () {

    var ts = {};

    ts.Size = 1000;
    ts.Queue = new Qu.Queue(ts.Size);
    ts.RfidEnqueue = "";
    ts.RfidDequeue = "";

    ts.GetRfidItems = function () {
        var list = [];
        while (!ts.Queue.isEmpty()) {
            list.push({ "Name": "" + ts.Queue.dequeue() + "" });
        }
        return list;
    };

    ts.GetRfid = function () {
        var s = "";
        //alert('ts.Queue.size()--' + ts.Queue.size());
        while (!ts.Queue.isEmpty()) {
            var rfid = ts.Queue.dequeue();
            if (ts.RfidDequeue.indexOf(rfid) == -1) {
                s = rfid;
                break;
            }
            if (ts.Queue.isEmpty()) break;
        }
        return s;
    };

    ts.ItvResult = null;
    ts.SetResult = function () {
        ts.ItvResult = setInterval(function () {
            if (ts.Queue.size() < ts.Size) {
                RfidScan.getData(function (result) {
                    if (result && result != '') {
                        var items = result.split(',');
                        for (var i = 0; i < items.length; i++) {
                            if (ts.RfidEnqueue.indexOf(items[i]) == -1) {
                                if (ts.RfidEnqueue != "") ts.RfidEnqueue += ',';
                                ts.RfidEnqueue += items[i];
                                ts.Queue.enqueue(items[i]);
                            }
                        }
                    }
                })
            }
        }, 100);
    };

    ts.ClearResult = function () {
        clearInterval(ts.ItvResult);
    };

    ts.ClearQueueData = function () {
        while (!ts.Queue.isEmpty()) {
            ts.Queue.dequeue();
        }
    };

    ts.Reset = function () {
        ts.ClearResult();
        RfidScan.clearData();
        ts.ClearQueueData();
        ts.RfidEnqueue = "";
        ts.RfidDequeue = "";
    };

    return ts;
});