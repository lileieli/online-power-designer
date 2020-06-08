$(document).ready(function () {
    var time = 0;//只能新增一跳，要想再次新增必须保存正在编辑的行
    $(".modify").click(function (event) {
        let buttonFonts = $(this).text();
        var parent = $(this).parent().parent();
        let amount = parent.find('td').length;
        if (buttonFonts === '修改') {
            if (time == 1) {
                alert("请先保存编辑的行！");
                return;
            }
            time = 1;
            $(this).text('保存');
            for (i = 0; i < amount; i++) {
                if (parent.find('td').eq(i).attr("class") == "none") {
                    continue;
                }
                
                if (parent.find('td').eq(i).find('input').eq(0).attr("class") == "modify-status") {
                    
                    parent.find('td').eq(i).find(".modify-status").val(parent.find('td').eq(i).find('div').text());
                }
                else {
                    //如果是不input 肯定是select  后期要改
                    parent.find('td').eq(i).find("select").eq(0).val(parent.find('td').eq(i).find('div').eq(0).attr("val"));
                    
                }
                
            }
            parent.find(".modify-status").show().siblings().hide();
            parent.find("select").show().siblings().hide();
            parent.find(".main-about input").removeAttr("disabled");
        } else {

            var json = {};
            for (var i = 0; i < amount; i++) {
                var key = "";
                var value = "";
                if (parent.find('td').eq(i).attr("class") == "none") {
                    continue;
                }
                if (parent.find('td').eq(i).find('input').eq(0).attr("class") == "modify-status") {
                    key = parent.find('td').eq(i).find('input').eq(0).attr("fid");
                    value = parent.find('td').eq(i).find('input').eq(0).val();
                    json[key] = value;
                }
                else {
                    key = parent.find('td').eq(i).find('select').eq(0).attr("fid");
                    value = parent.find('td').eq(i).find("select").eq(0).val();
                    json[key] = value;
                }
               
            }
           
            $.ajax({
                type: "post",
                url: "/Pd/Table/ModifyField",
                data: json,
                success: function (data) {
                    alert("操作成功！");
                    //刷新
                }
            })
        }
    })
    $(document).on("click", ".delete", function () {
        var fileId = $(this).parent().parent().attr("id");
        $.ajax({
            type: "post",
            url: "/Pd/Table/DeleteField",
            data: { "field": fileId },
            success: function (data) {
                if (data.success == 1) {
                    alert("删除成功");
                }
            }
        });
    })
    $(".add-line").click(function () {
        if (time == 1) {
            alert("请先保存编辑的行！");
            return;
        }
        time = 1;
        var CommonBrlong = ["TrueOrFalse","FiledType"];
        $.ajax({
            type: "post",
            url: "/Pd/Table/GetCommonType",
            data: { "CommonBrlong": CommonBrlong },
            success: function (data) {
                var trueOrFalse = eval(data.rows).filter(function (e) { return e.commonBrlong == "TrueOrFalse"; });
                var FiledType = eval(data.rows).filter(function (e) { return e.commonBrlong == "FiledType"; });
                console.log(trueOrFalse);
                console.log(FiledType);
                let newLine ='<tr class="add-new">'
                newLine+= ' <td style="display:none"><input type = "text" class="modify-status" fid = "FieldId" ></td >'
                newLine += '<td> <input type = "text" class="modify-status" fid = "FieldName" ></td >'
                newLine+='<td>  <select fid="FieldlKey">'
                for (var i = 0; i < trueOrFalse.length; i++) {
                    newLine += ' <option value="' + trueOrFalse[i].commonValue + '">' + trueOrFalse[i].commonName+'</option>';
                }
                newLine += ' </select>  </td>' 
                newLine += '<td> <input type = "text" class="modify-status" fid = "FieldExplain" ></td >'
                newLine += '<td> <select fid="FieldType">'
                for (var i = 0; i < FiledType.length; i++) {
                    newLine += ' <option value="' + FiledType[i].commonValue + '">' + FiledType[i].commonName + '</option>';
                }
                newLine += ' </select>  </td>' 
                newLine += '<td> <input type = "text" class="modify-status" fid = "FieldLength" ></td >'
                newLine += '<td><input type = "text" class="modify-status" fid = "FieldDefultValue" ></td >'
                newLine += '<td>  <select fid="FieldIsNull">'
                for (var i = 0; i < trueOrFalse.length; i++) {
                    newLine += ' <option value="' + trueOrFalse[i].commonValue + '">' + trueOrFalse[i].commonName + '</option>';
                }
                newLine += ' </select>  </td>'
                newLine += '<td> <input type = "text" class="modify-status" fid = "FieldContentId" > </td >'
                newLine += '<td class="none"> <button class="amodify"> 保存</button ><button class="adelete">删除</button> </td >'
                newLine +='<tr >'
                $(".des-table tbody ").append(newLine);
            }
        });
       
    })

    $(document).on("click", ".adelete", function () {
        //刷新
    })

    $(document).on("click", ".amodify", function () {
        var parent = $(this).parent().parent();
        let amount = parent.find('td').length;
        var json = {};
        for (var i = 0; i < amount; i++) {
            var key = "";
            var value = "";
            if (parent.find('td').eq(i).attr("class") == "none") {
                continue;
            }
            if (parent.find('td').eq(i).find('input').eq(0).attr("class") == "modify-status") {
                key = parent.find('td').eq(i).find('input').eq(0).attr("fid");
                value = parent.find('td').eq(i).find('input').eq(0).val();
                json[key] = value;
            }
            else {
                key = parent.find('td').eq(i).find('select').eq(0).attr("fid");
                value = parent.find('td').eq(i).find("select").eq(0).val();
                json[key] = value;
            }
        }
        json["TableId"] = $("#tableId").val();
        if (json["FieldlKey"] == "1" && json["FieldIsNull"] == "1") {
            alert("该字段已设置主键不可为NULL!")
            return;
        }
        $.ajax({
            type: "post",
            url: "/Pd/Table/AddField",
            data: { "filedModel": json },
            success: function (data) {
                if (data.success == 1) {
                    alert("新增成功!");
                }
            }
        });
       
    })
})