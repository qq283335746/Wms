var DlgFiles = {
    TableName: '',
    IsSingle: true,
    DlgPictureSelect: function (tableName, isMutilSelect,callBackFun) {
        this.TableName = tableName;
        this.IsSingle = !isMutilSelect;
        var h = $(window).height();
        if (h > 700) h = 700;
        else h = h * 0.95;
        var w = $(window).width();
        if (w > 960) w = 960;
        else w = w * 0.95;

        if ($("body").find("#dlgFile" + tableName + "").length == 0) {
            $("body").append('<div id="dlgFile' + tableName + '"></div>');
        }
        var dlg = $("#dlgFile" + tableName + "");
        dlg.dialog({
            title: '选择图片',
            width: w,
            height: h,
            closed: false,
            modal: true,
            buttons: [{
                id: 'btnOkPicture', text: '确定', iconCls: 'icon-ok',
                handler: function () {
                    DlgFiles.GetPicSelected(tableName, callBackFun);
                    dlg.dialog('destroy');
                }
            }, {
                id: 'btnCancelPicture', text: '取消', iconCls: 'icon-cancel',
                handler: function () {
                    dlg.dialog('destroy');
                }
            }],
            toolbar: [{
                id: 'btnAddPicture', text: '上传', iconCls: 'icon-add',
                handler: function () {
                    DlgFiles.DlgUpload(tableName);
                }
            }, {
                id: 'btnDelPicture', text: '删除', iconCls: 'icon-remove',
                handler: function () {
                    DlgFiles.OnDelete(tableName);
                }
            }],
            content: '<div class="easyui-layout" data-options="fit:true,border:false"><div id="dlgFileContent" data-options="region:\'center\',border:false" style="padding:10px;"></div><div data-options="region:\'south\',border:false"><div id="dlgFilePager" class="easyui-pagination" data-options="pageSize:20"></div></div></div>',
            onOpen: function () {
                DlgFiles.LoadData(1, 20, tableName);
            }
        })
    },
    DlgUpload: function (tableName, callBackFun) {
        this.TableName = tableName;
        var h = $(window).height();
        if (h > 400) h = 400;
        else h = h * 0.95;
        var w = $(window).width();
        if (w > 630) w = 630;
        else w = w * 0.95;

        if ($("body").find('#dlgUpload' + tableName + '').length == 0) {
            $("body").append('<div id="dlgUpload' + tableName + '" style="padding:10px;"></div>');
        }
        var dlg = $('#dlgUpload' + tableName + '');
        dlg.dialog({
            title: '上传文件',
            width: w,
            height: h,
            closed: false,
            modal: true,
            buttons: [{
                id: 'btnSaveUpload', text: '上传', iconCls: 'icon-ok',
                handler: function () {
                    DlgFiles.OnUpload(tableName);
                }
            }, {
                id: 'btnCancelUpload', text: '取消', iconCls: 'icon-cancel',
                handler: function () {
                    dlg.dialog('destroy');
                }
            }],
            toolbar: [{
                id: 'btnAddFile', text: '添加', iconCls: 'icon-add',
                handler: function () {
                    DlgFiles.OnToolbarAdd();
                }
            }],
            content: '<form id="dlgUploadFm_' + DlgFiles.TableName + '" method="post" enctype="multipart/form-data"><div class="mb10"><input class="easyui-filebox" id="' + DlgFiles.TableName + '_file1" name="' + DlgFiles.TableName + '_file1" data-options="prompt:\'选择图片\',buttonText: \'选择文件\'" style="width:500px;" /></div></form>'
        })
    },
    OnToolbarAdd: function () {
        var currDlgFm = $("#dlgUploadFm_" + DlgFiles.TableName + "");
        var rowLen = currDlgFm.children("div").length + 1;
        var newRow = $("<div class=\"mb10\"><input type=\"text\" id=\"" + DlgFiles.TableName + "_file" + rowLen + "\" name=\"" + DlgFiles.TableName + "_file" + rowLen + "\" style=\"width:500px;\" /><a href=\"#\" onclick=\"$(this).parent().remove();return false;\" style=\"margin-left:10px;\">删 除</a></div>");
        currDlgFm.append(newRow);
        newRow.find("#" + DlgFiles.TableName + "_file" + rowLen + "").filebox({
            buttonText: '选择文件',
            prompt: '选择图片'
        })
        newRow.find("a:last").linkbutton({
            iconCls: 'icon-remove',
            plain: true
        });
    },
    OnUpload: function (tableName) {
        try {
            var dlgFm = $('#dlgUploadFm_' + tableName + '');
            $.messager.progress({ title: '请稍等', msg: '正在执行...' });
            dlgFm.form('submit', {
                url: '/wms/h/upload.html',
                onSubmit: function (param) {
                    var hasNotFile = true;
                    dlgFm.find("[class*=filebox-f]").each(function () {
                        if ($.trim($(this).filebox('getValue')) == "") {
                            hasNotFile = false;
                        }
                    })
                    if (!hasNotFile) {
                        $.messager.progress('close');
                        $.messager.alert('错误提示', "包含一个或多个未选择文件，无法上传，请检查！", 'error');
                        return false;
                    }
                    param.ReqName = tableName;
                    return true;
                },
                success: function (data) {
                    console.log('data--' + data);
                    $.messager.progress('close');
                    var jData = eval('(' + data + ')');
                    if (!jData.success) {
                        $.messager.alert('错误提示', jData.message, 'error');
                        return false;
                    }
                    jeasyuiFun.show("温馨提醒", jData.message);

                    var dlg = $('#dlgUpload' + tableName + '');
                    dlg.dialog('destroy');

                    DlgFiles.LoadData(1, 20, tableName);
                }
            });
        }
        catch (e) {
            $.messager.progress('close');
            $.messager.alert('错误提醒', e.name + ": " + e.message, 'error');
        }
    },
    LoadData: function (pageIndex, pageSize, funName) {
        var sData = '{"PageIndex":"' + pageIndex + '","PageSize":"' + pageSize + '","Keyword":"' + funName + '"}';
        $.ajax({
            url: "/wms/Services/WmsService.svc/GetSitePictureList",
            type: "post",
            data: '{"model":' + sData + '}',
            contentType: "application/json; charset=utf-8",
            beforeSend: function () {
                Common.OnProgressStart();
            },
            complete: function () {
                Common.OnProgressStop();
            },
            success: function (result) {
                //console.log('result--' + JSON.stringify(result));
                if (result.ResCode != 1000) {
                    if (result.Msg != "") {
                        $.messager.alert('系统提示', result.Msg, 'info');
                    }
                    return false;
                }
                var jData = JSON.parse(result.Data);
                var dlg = $("#dlgFile" + funName + "");
                var imgs = dlg.find('img');

                var sAppend = '';
                for (var i = 0; i < jData.rows.length; i++) {
                    var item = jData.rows[i];
                    var isExist = false;
                    imgs.each(function () {
                        if ($(this).attr('code') == item.Id) isExist = true;
                    })
                    if (!isExist) {
                        sAppend += '<div class="row_col w110"><img src="' + item.Text + '" class="img" alt="图片" code="' + item.Id + '" /></div>';
                    }
                }
                if (sAppend == '') return false;
                //sAppend += '<span class="clr"></span>';
                var dlg = $("#dlgFile" + funName + "");
                dlg.find('#dlgFileContent').append(sAppend);
                var pager = dlg.find('#dlgFilePager');
                pager.pagination({
                    total: jData.total,
                    onSelectPage: function (pageNumber, pageSize) {
                        DlgFiles.GetDlgFiles(pageNumber, pageSize, funName);
                    }
                });

                DlgFiles.OnPicSelect(funName, DlgFiles.IsSingle);
            }
        });
    },
    OnPicSelect: function (funName,isSingle) {
        var dlg = $("#dlgFile" + funName + "");
        var imgs = dlg.find('img');

        imgs.bind("click", function () {
            var curr = $(this).parent();
            if (isSingle) {
                curr.addClass("curr").siblings().removeClass("curr");
            }
            else {
                if (curr.hasClass("curr")) {
                    curr.removeClass("curr");
                }
                else {
                    curr.addClass("curr");
                }
            }
        })
    },
    GetPicSelected: function (tableName,callBackFun) {
        var data = [];
        var dlg = $("#dlgFile" + tableName + "");
        var imgs = dlg.find('img');
        imgs.each(function () {
            var curr = $(this);
            if (curr.parent().hasClass('curr')) {
                data.push({ "Id": "" + curr.attr('code') + "", "Src": "" + curr.attr('src') + "" });
            }
        })
        if (typeof (eval(callBackFun)) == 'function') {
            callBackFun(data);
        }
    },
    OnDelete: function (tableName) {

        var dlg = $("#dlgFile" + tableName + "");
        var imgs = dlg.find('img');
        var itemAppend = '';
        var index = 0;
        imgs.each(function () {
            var curr = $(this);
            if (curr.parent().hasClass('curr')) {
                if (index > 0) itemAppend += ',';
                itemAppend += curr.attr('code')
            }
        })
        if (itemAppend == '') {
            $.messager.alert('错误提示', "请选择要删除的文件！", 'error');
            return false;
        }
        $.messager.confirm('温馨提醒', '确定要删除吗？', function (r) {
            if (r) {
                $.ajax({
                    url: "/wms/Services/WmsService.svc/DeleteSitePicture",
                    type: "post",
                    data: '{"itemAppend":"' + itemAppend + '"}',
                    contentType: "application/json; charset=utf-8",
                    beforeSend: function () {
                        $.messager.progress({ title: '请稍等', msg: '正在执行...' });
                    },
                    complete: function () {
                        $.messager.progress('close');
                    },
                    success: function (result) {
                        if (result.ResCode != 1000) {
                            $.messager.alert('系统提示', result.Msg, 'info');
                            return false;
                        }

                        imgs.each(function () {
                            var curr = $(this);
                            if (curr.parent().hasClass('curr')) {
                                curr.parent().remove();
                            }
                        })
                        jeasyuiFun.show("温馨提示", "操作成功！");
                    }
                });
            }
        });
    }
}