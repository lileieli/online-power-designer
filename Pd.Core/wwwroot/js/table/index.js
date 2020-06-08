var project = "";
$(document).ready(function () {
    var currentYear;

    $.ajax({
        type: "post",
        url: "/Pd/Table/GetProject",
        success: function (data) {
            currentYear = data.rows;
            let strData = "";
            // strData += '<ul><li class="lsm-sidebar-item"><i class="iconfont" id="addProject">&#xe620;</i></li></ul>'
            strData += "<ul>";
            $.each(currentYear, function (index, element) {
                strData += `<li class="lsm-sidebar-item">
                <a href="javascript:;"><i class="iconfont">&#xe67c;</i><span>${element.year}</span><i class="right-icon iconfont">&#xe629;</i></a>`
                strData += "<ul>";
                $.each(element.data, function (tindex, tdata) {
                    strData += `<li class="menu-click" id='${tdata.projectId}'><a href="javascript:;"><span>${tdata.projectName}</span></a></li>`
                })
                strData += "</li></ul>"
            })
            strData += "</ul>";
            $(".lsm-sidebar").append(strData);
        }
    })
    //加载左侧table
    $(document).on('click', '.menu-click', function () {
        $(".search").show();
        $(".add-table").show();
        $(".del-table").show();
        $(".create-sql").show();
        let obtainId = $(this).attr('id');
        project = obtainId;
        let tableDetail = [];
        $(this).css('background-color', '#6e809c').siblings().css('background-color', '');
        $.ajax({
            type: "post",
            url: "/Pd/Table/GetTableTable",
            data: { "projectId": obtainId },
            success: function (data1) {
                tableDetail = data1.rows;
                let tableData = "<div class='table-wrap'>";
                $(".table-outer").empty();
                $.each(tableDetail, function (index, data) {
                    tableData += `<div class="box-table table-blue" id="${data.tableId}">
                        <div class="box-head head-blue"><input class="checkbox-content" type="checkbox" />${data.tableName}</div>
                        <div class="box-content">`
                    if (data.data != null && data.data.length > 0) {
                        $.each(data.data, function (dindex, obj) {
                            if (obj.b != null) {
                                tableData += `<div class="font-content" fieldContentId="${obj.b.fieldContentId == null ? "" : obj.b.fieldContentId}">
                                        <div>${obj.b.fieldName}</div>
                                        <div>${obj.b.fieldType}</div>
                                    </div>`
                            }

                        })
                    }
                    tableData += "</div></div>"
                })
                tableData += "</div>";
                $(".table-outer").html(tableData);
            }
        })


    })
    // 新增一个table 弹出层
    $(".add-table").click(function () {
        $(".pop-model").show();
        $(".screen-model").show();
    })
    // 提交新增的table
    $(".submit").click(function () {
        let tableName = $(".line-pop input").val();
        if (tableName == null || tableName == "") {
            alert('表头名称为必填项');
            return;
        }
        $.ajax({
            type: "post",
            url: "/Pd/Table/AddTableTable",
            data: { "tableName": tableName, "projectId": project },
            success: function (data) {
                if (data.success == 1) {
                    $(".cancle").click();
                    $(".line-pop input").val("");
                } else {
                    alert("新增失败！")
                }
            },
            error: function (err) {
                alert("新增失败!!!")
            }
        })
        //if (tableName) {
        //    let addData = `<div class="box-table table-blue">
        //                <div class="box-head head-blue"><input class="checkbox-content" type="checkbox" />${tableName}</div>
        //                <div class="box-content"></div></div>`
        //    if ($(".table-outer").children().hasClass('table-wrap')) {
        //        $(".table-wrap").append(addData);
        //    } else {
        //        addData = `<div class='table-wrap'>
        //            <div class="box-table table-blue">
        //                <div class="box-head head-blue"><input class="checkbox-content" type="checkbox" />${tableName}</div>
        //                <div class="box-content"></div>
        //            </div></div>`
        //        $(".table-wrap").append(addData);
        //    }
        //    $(".pop-model").hide();
        //    $(".screen-model").hide();
        //} else {
        //    alert('表头名称为必填项');
        //}

    })
    // 新增取消
    $(".cancle").click(function () {
        $(".screen-model").hide();
        $(".pop-model").hide();
    })
    //删除取消
    $(".cancle-message").click(function () {
        $(".message-model").hide();
        $(".pop-message").hide();
    })
    //搜索
    $(".btn-default").click(function () {
        let tablename = $(".form-control").val();
        //let projectid = $(".menu-click").attr('id');

        $.ajax({
            type: "post",
            url: "/Pd/Table/GetTableTable",
            data: { "projectId": project, "tableName": tablename },
            success: function (data1) {
                tableDetail = data1.rows;
                let tableData = "<div class='table-wrap'>";
                $(".table-outer").empty();
                $.each(tableDetail, function (index, data) {
                    tableData += `<div class="box-table table-blue" id="${data.tableId}">
                        <div class="box-head head-blue"><input class="checkbox-content" type="checkbox" />${data.tableName}</div>
                        <div class="box-content">`
                    if (data.data != null && data.data.length > 0) {
                        $.each(data.data, function (dindex, obj) {
                            if (obj.b != null) {
                                console.log(obj.b.fieldContentId);
                                tableData += `<div class="font-content" fieldContentId="${obj.b.fieldContentId == null ? "" : obj.b.fieldContentId}">
                                        <div>${obj.b.fieldName}</div>
                                        <div>${obj.b.fieldType}</div>
                                    </div>`
                            }

                        })
                    }
                    tableData += "</div></div>"
                })
                tableData += "</div>";
                $(".table-outer").html(tableData);

            }
        })


    })
    //点击字段 级联表
    $(document).on('click', '.font-content', function () {
        $(this).css("background", "").siblings().css("background", "");
        let key = $(this).attr("fieldContentId");
        if ($(this).parent().parent().siblings().hasClass('table-pink')) {
            $(this).parent().parent().siblings().removeClass('table-pink').addClass('table-blue');
            if ($(this).parent().parent().siblings().find('.box-content').children().css('background')) {
                $(this).parent().parent().siblings().find('.box-content').children().css('background', '');
            }
        }
        for (let i = 0; i < $(this).parents().find('.box-table').length; i++) {
            if (key == $(this).parents().find('.box-table').eq(i).attr('id')) {
                if ($(this).parents().find('.box-table').eq(i).hasClass("table-blue")) {
                    $(this).css("background", "#FFF9E6");
                    $(this).parents().find('.box-table').eq(i).removeClass('table-blue').addClass('table-pink').children().eq(0).removeClass('head-blue').addClass('head-pink')
                } else if ($(this).parents().find('.box-table').eq(i).hasClass("table-pink")) {
                    $(this).css("background", "");
                    $(this).parents().find('.box-table').eq(i).removeClass('table-pink').addClass('table-blue').children().eq(0).removeClass('head-pink').addClass('head-blue');
                }
            }
        }
    })
    // 双击表头跳转
    $(document).on("dblclick", ".box-head", function () {
        //let titleName = $(this).textmodify
        let obtainInfo = $(this).parent().attr('id');
        $("#myModal").modal({
            backdrop: 'static',     // 点击空白不关闭
            keyboard: false,        // 按键盘esc也不会关闭
            remote: '/Pd/Table/Field?tableId=' + obtainInfo   // 从远程加载内容的地址
        });
    })
    let checkArray = [];
    $(document).on("click", ".checkbox-content", function (event) {
        event.stopPropagation();   //  阻止事件冒泡
        let checkId = $(this).parent().parent().attr('id');
        if (checkArray.includes(checkId)) {
            checkArray = checkArray.filter(function (item) {
                return item !== checkId;
            })
        } else {
            checkArray.push(checkId);
        }
    })
    //表删除弹出框
    $(".del-table").click(function () {
        if (checkArray.length === 0) {
            alert('请先选中需要删除的表');
        } else {
            $(".pop-message").show();
            $(".message-model").show();
        }
    })
    //删除
    $(".submit-message").click(function () {
        var idlist = "";
        $(".table-wrap input:checkbox").each(function () {
            if ($(this).is(':checked')) {
                var id = $(this).parent().parent().attr("id");
                idlist += id + "|";
            }
        });
        $.ajax({
            type: "post",
            url: "/Pd/Table/DeleteTableTable",
            data: { "idlist": idlist },
            success: function (data) {
                if (data.success == 1) {
                    $(".pop-message").hide();
                    $(".message-model").hide();
                }
            }
        })
    })

    $(".submit-sql").click(function () {
        $(".sql-model").hide();
        $(".sql-message").hide();
    })

    $(document).on('click', '#addProject', function () {
        $("#projectView").modal({
            backdrop: 'static',     // 点击空白不关闭
            keyboard: false,        // 按键盘esc也不会关闭
            remote: '/Pd/Table/Project'   // 从远程加载内容的地址
        });

    })

    ///生成sql

    $(".create-sql").click(function () {
        if (checkArray.length === 0) {
            alert('请先选中需要生成sql的表');
            return;
        }
        var tableIds = [];
        $(".table-wrap input:checkbox").each(function () {
            if ($(this).is(':checked')) {
                var id = $(this).parent().parent().attr("id");
                tableIds.push(id);
            }
        });
        console.log(tableIds);
        $.ajax({
            type: "post",
            url: "/Pd/Table/CreateSqls",
            data: { "tableIds": tableIds },
            success: function (data) {
                if (data.success == "1") {
                    $(".sql-model").show();
                    $(".sql-message").show();
                    $('.sql-message textarea').val(data.message);
                }
            }
        })


    })



});