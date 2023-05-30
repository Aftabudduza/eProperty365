var currentPagePath = window.location.pathname + "/";
$(document).ready(function (parameters) {
    //LoadComboBox();
    InitialLoad();
    //LoadUnitId();
    //LoadTenantName();
    //$(".tDate").datepicker({
    //    dateFormat: "mm/dd/yy",
    //    changeYear: true,
    //    changeMonth: true
    //});
    $(".ddl").select2();
    //$('input[type="checkbox"].flat-red, input[type="radio"].flat-red').iCheck({
    //    checkboxClass: 'icheckbox_flat-green',
    //    radioClass: 'iradio_flat-green'
    //});
});

function LoadOwnerWiseData(parameters) {
    var UrlPropertyManager = currentPagePath + "GetPropertyManager";
    var UrlLocation = currentPagePath + "GetLocation";
    var obj = {
        "OwnerId": parameters
    };
    let PropertyManager = makeAjaxCallReturnPromiss(UrlPropertyManager, obj);
    PropertyManager.then((data) => {
        let result = $.parseJSON(decodeURIComponent(data.d));
        setDropdown(result, $("#ddlPropertyManagerId"));
    }).catch((err) => {
        console.log(err);
    });
    let Location = makeAjaxCallReturnPromiss(UrlLocation, obj);
    Location.then((data) => {
        let result = $.parseJSON(decodeURIComponent(data.d));
        setDropdown(result, $("#ddlLocationId"));
        
    }).catch((err) => {
        console.log(err);
    });
    Promise.all([PropertyManager,Location]).then(function () {
    });
}

function InitialLoad() {
    // ......... Owner ............... //
    var UrlOwner = currentPagePath + "GetOwner";
    var obj = {};
    let Owner = makeAjaxCallReturnPromiss(UrlOwner, obj);
    Owner.then((data) => {
        let result = $.parseJSON(decodeURIComponent(data.d));
        
        setDropdown(result, $("#ddlOwner"));
        if (result[0].SelectedField != "") {
            $("#ddlOwner").prop('disabled', true);
        }
    }).catch((err) => {
        console.log(err);
    });
    Promise.all([Owner]).then(function () {
    });
}

function setDropdown(data, id) {
    id.get(0).options.length = 0;
    var content = '<option value="-1">Select.......</option>';
    $.each(data, function (i, obj) {
        if (obj.SelectedField == obj.Id2) {
            content += '<option data_Id="' + obj.Id + '" value="' + obj.Id2 + '" selected>' + obj.Data + '</option>';
        } else {
            content += '<option data_Id="' + obj.Id + '" value="' + obj.Id2 + '">' + obj.Data + '</option>';
        }
    });
    id.append(content);
    if (data.length == 1) {
        id.val(data[0].Id2).trigger('change');
    }
    $(id).select2();
    let selected = $(id).val();
    $(id).val(selected).trigger('change');
}

$(document).off("change", "#ddlOwner").on("change", "#ddlOwner", function () {
    $('#tbl tbody').empty();
    let selectedValue = $("#ddlOwner option:selected").val();
    if (selectedValue != "-1") {
        LoadOwnerWiseData(selectedValue);
    }
});

$(document).off("change", "#ddlLocationId").on("change", "#ddlLocationId", function () {
    //$('#tbl tbody').empty();
    let Owner = $("#ddlOwner option:selected").val();
    let PropertyManagerId = $("#ddlPropertyManagerId option:selected").val();
    let LocationSerialId = $("#ddlLocationId option:selected").val();
    if (LocationSerialId != "" && LocationSerialId != "-1" && Owner != '-1') {
        var UrlOwnerLocationWiseData = currentPagePath + "OwnerLocationWiseData";
        var obj = {
            "OwnerId": Owner,
            "PropertyManagerSerialId":PropertyManagerId,
            "LocationSerialId": LocationSerialId
        };
        let OwnerLocationWiseData = makeAjaxCallReturnPromiss(UrlOwnerLocationWiseData, obj);
        OwnerLocationWiseData.then((data) => {
            let result = $.parseJSON(decodeURIComponent(data.d));
            SetData(result);
        }).catch((err) => {
            console.log(err);
        });
        Promise.all([OwnerLocationWiseData]).then(function () {
        });
    }
});

function SetData(result) {
    if (result.length > 0) {
        $('#tbl tbody').empty();
        let content = '';
        $.each(result, function (i, obj) {
            if (obj.Amount == null) {
                obj.Amount = '';
            }
            content += '<tr>' +
                '<td class="SendDate">' + obj.SendDate + '</td>' +
                '<td class="InvoiceID">' + obj.InvoiceID + '</td>' +
                '<td><input type="hidden" class="TenantSerialId" data-EmailId="' + obj.EmailId + '" value="' + obj.TenantSerialId + '"/>' + obj.TenantName + '</td>' +
                '<td><input type="hidden" class="UnitSerialId" value="' + obj.UnitSerialId + '"/>' + obj.UnitSerialId + '</td>' +
                '<td class="Amount">' + obj.Amount + '</td>' +
                '</tr>';
        });
        $('#tbl tbody').append(content);
    }
}

$(document).off('click', '#btnRunMonthlyRentalInvoices').on('click', '#btnRunMonthlyRentalInvoices', function (parameters) {

    if (SaveValidation() == true) {
        var obj = GetObject();
        var pagePath = currentPagePath + "/Save";
        $.ajax({
            type: "POST",
            url: pagePath,
            data: JSON.stringify({ "Obj": obj }),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            error:
                function (XMLHttpRequest, textStatus, errorThrown) {
                    alert("Error");
                },
            success:
                function (result) {
                    var obj = $.parseJSON(decodeURIComponent(result.d));
                    if (obj == true) {
                        ClearData();
                        notify('success', "Mail Sent Successfully");
                    } else {
                        notify('danger', "Mail Sent Failed !!");
                    }
                }
        });
    }
});

function SaveValidation() {
    var isresult = true;
    var OwnerId = $('#ddlOwner option:selected').val();
    var PropertyManagerId = $('#ddlPropertyManagerID option:selected').val();
    var LocationId = $('#ddlLocation option:selected').val();

    if (OwnerId === "undefined" || OwnerId === "-1") {
        $("#s2id_ddlOwner").css({ 'border': '1px solid red', 'border-radius': '5px' });
        isresult = false;
    }
    else {
        $("#s2id_ddlOwner").css({ 'border': '1px solid Transparent' });
    }
    //if (PropertyManagerId === "undefined" || PropertyManagerId === "-1") {
    //    $("#s2id_ddlPropertyManagerID").css({ 'border': '1px solid red', 'border-radius': '5px' });
    //    isresult = false;
    //}
    //else {
    //    $("#s2id_ddlPropertyManagerID").css({ 'border': '1px solid Transparent' });
    //}
    
    if (LocationId === "undefined" || LocationId === "-1") {
        $("#s2id_ddlLocation").css({ 'border': '1px solid red', 'border-radius': '5px' });
        isresult = false;
    }
    else {
        $("#s2id_ddlLocation").css({ 'border': '1px solid Transparent' });
    }
    
    if (isresult)
        isresult = true;
    else {
        isresult = false;
    }
    return isresult;
}
function GetObject() {
    let obj = {
        "OwnerId": $('#ddlOwner option:selected').val(),
        "PropertyManagerSerialId": $('#ddlPropertyManagerId option:selected').val(),
        "LocationSerialId": $('#ddlLocationId option:selected').val(),
        "OwnerName": $('#ddlOwner option:selected').text(),
        "PropertyManagerName": $('#ddlPropertyManagerId option:selected').text(),
        "LocationName": $('#ddlLocationId option:selected').text(),
        "lst": []
    }
    $('#tbl tbody tr').each(function (index, element) {
            let data = {
                "UnitSerialId": $(this).find(".UnitSerialId").val(),
                "UnitName": $(this).find(".UnitSerialId").parent().text(),
                "TenantName": $(this).find('.TenantSerialId').parent().text(),
                "TenantSerialId": $(this).find(".TenantSerialId").val(),
                "Amount": $(this).find(".Amount").text(),
                "EmailId": $(this).find(".TenantSerialId").attr('data-EmailId'),
                "SendDate": $(this).find(".SendDate").text(),
                "InvoiceID": $(this).find(".InvoiceID").text()
            };
            obj.lst.push(data);
    });
    return obj;
}
$(document).off('click', '#btnCancel').on('click', '#btnCancel', function (parameters) {
    var origin = window.location.origin; // Returns base URL
    var url = origin + "/Pages/DashboardAdmin.aspx";
    window.location.href = url;
});

function ClearData() {
    $('#ddlLocationId').val('-1').trigger('change');
}