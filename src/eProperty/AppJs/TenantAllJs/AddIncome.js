var currentPagePath = window.location.pathname + "/";

var pathname = window.location.pathname; // Returns path only
var url = window.location.href;     // Returns full URL
var origin = window.location.origin;   // Returns base URL
var G_RelationalData = [];
$(document).ready(function (parameters) {
    LoadComboBox();
    InitialLoad();
    //LoadUnitId();
    //LoadTenantName();
    $(".tDate").datepicker({
        dateFormat: "mm/dd/yy",
        changeYear: true,
        changeMonth: true
    });
    $(".ddl").select2();
    $('input[type="checkbox"].flat-red, input[type="radio"].flat-red').iCheck({
        checkboxClass: 'icheckbox_flat-green',
        radioClass: 'iradio_flat-green'
    });
});

function LoadComboBox(parameters) {
    // ......... Location ............... //
    var URL = "/Pages/Resident/TenantOnlinefee.aspx/" + "GetLocation";
    var obj = {};
    let location = makeAjaxCallReturnPromiss(URL, obj);
    location.then((data) => {
        console.log("Location Data Loaded");
        let StateData = setCombo_withInt($.parseJSON(decodeURIComponent(data.d)), '-1');
        $("#ddlLocation option").empty();
        $("#ddlLocation").append(StateData);
        $("#ddlLocation").select2();
    }).catch((err) => {
        console.log(err);
    });

    //////---------- Country -----------//
    var URL = "/Pages/Admin/AddResidentialUnit.aspx/" + "GetCountry";
    var obj = {};
    let Country = makeAjaxCallReturnPromiss(URL, obj);
    Country.then((data) => {
        console.log("Country Data Loaded");
        let countryData = setCombo($.parseJSON(decodeURIComponent(data.d)), 'US');
        $("#ddlCountry option").empty();
        $("#ddlCountry").append(countryData);
        $("#ddlCountry").select2();
    }).catch((err) => {
        console.log(err);
    });
    //.................. load State ...............//
    var URL = "/Pages/Admin/AddResidentialUnit.aspx/" + "GetState";
    var obj = {};
    let State = makeAjaxCallReturnPromiss(URL, obj);
    State.then((data) => {
        console.log("State Data Loaded");
        let StateData = setCombo($.parseJSON(decodeURIComponent(data.d)), '-1');
        $("#ddlState option").empty();
        $("#ddlState").append(StateData);
        $("#ddlState").select2();
    }).catch((err) => {
        console.log(err);
    });
    Promise.all([location], [Country], [State]).then(function () {
        // LoadVehicle();
        // LoadPeopleStayingInUnit();
        // loadProfileInfo();

    });


}

$(document).on('keyup', '#txtReEnterRoutingNumber2ndFromLeft', function (parameters) {
    if ($("#txtRoutingNumber2ndFromLeft").val() === $(this).val()) {
        $("#txtReEnterRoutingNumber2ndFromLeft").css({ "border": "1px solid Green" });
    } else {
        $("#txtReEnterRoutingNumber2ndFromLeft").css({ "border": "1px solid red" });
    }
});

$(document).on('keyup', '#txtReEnterAccountNumberLastNumberFromLeft', function (parameters) {
    if ($("#txtAccountNumberLastNumberFromLeft").val() === $(this).val()) {
        $("#txtReEnterAccountNumberLastNumberFromLeft").css({ "border": "1px solid Green" });
    } else {
        $("#txtReEnterAccountNumberLastNumberFromLeft").css({ "border": "1px solid red" });
    }
});

$(document).on('ifChanged', "input[type=radio][name=AccountType]", function (parameters) {
    if (this.value == 'Check') {
        $('#divCheckingAccount').css('display', 'block');
        $('#divCashReceipt').css('display', 'none');
    }
    else if (this.value == 'Cash') {
        $('#divCashReceipt').css('display', 'block');
        $('#divCheckingAccount').css('display', 'none');
    }

});

function InitialLoad() {
    var URL = currentPagePath + "GetInitialData";
    var obj = {};
    let Basic = makeAjaxCallReturnPromiss(URL, obj);
    Basic.then((data) => {
        if (data.d != null || data.d != "") {
            var result = $.parseJSON(decodeURIComponent(data.d));
            $("#spanOwnerId").text(result[0][0].Id2);
            $("#txtPropertyManagerId").text(result[0][0].Data);
            $("#txtPropertyManagerId").attr('data_Id', result[0][0].Id3);
            setComboData(result[1], null, $('#ddlUnitID'));
            setComboData(result[2], null, $('#ddlResidentTenantName'));
        }
    });
}
function setComboData(data, selectedvalue,id) {
    var content = '<option value="-1">Select.......</option>';
    if (data == undefined || data.length == 0 || data == null) {
        return content;
    }
    else {
        if (selectedvalue == undefined || selectedvalue == null) {
            $.each(data, function (i, obj) {
                content += '<option data_Id="' + obj.Id2 + '" value="' + obj.Id3 + '">' + obj.Data + '</option>';
            });
        }
        else {
            $.each(data, function (i, obj) {
                if (obj.Id == selectedvalue) {
                    content += '<option data_Id="' + obj.Id2 + '" value="' + obj.Id3 + '" selected>' + obj.Data + '()' + '</option>';
                } else {
                    content += '<option data_Id="' + obj.Id2 + '" value="' + obj.Id3 + '">' + obj.Data + '</option>';
                } 
            });
        }
    }
    $(id).append(content);
    $(id).select2();
}
$(document).off('change', '#txtEnterAmountBeingPaid').on('change', '#txtEnterAmountBeingPaid', function () {
    $('#txtAmountDisable').val($(this).val());
});
$(document).off('change', '#ddlUnitID').on('change', '#ddlUnitID', function () {
    let selectedValue = $(this).val();
    if (selectedValue != '-1') {
        let tenantId = $(this).find('option:selected').attr('data_Id');
        $('#ddlResidentTenantName').val(tenantId).trigger('change:select2');
    } else {
        $('#ddlResidentTenantName').val('-1').trigger('change:select2');
    }
    $('#ddlResidentTenantName').select2();
});
$(document).off('change', '#ddlResidentTenantName').on('change', '#ddlResidentTenantName', function () {
    let selectedValue = $(this).val();
    if (selectedValue != '-1') {
        let tenantId = $(this).find('option:selected').attr('data_Id');
        $('#ddlUnitID').val(tenantId).trigger('change:select2');
    } else {
        $('#ddlUnitID').val('-1').trigger('change:select2');
    }
    $('#ddlUnitID').select2();
});

$(document).off('click', '#btnSave').on('click', '#btnSave', function (parameters) {

    if (SaveValidation() == true) {
        var obj = GetObject();
        var pagePath = pathname + "/Save";
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
                        notify('success', "Saved successfully");
                    } else {
                        notify('danger', "Save Failed !!");
                    }
                }
        });
    }
});

function SaveValidation() {
    var isresult = true;
    var OwnerId = $("#spanOwnerId").text();
    var PropertyManagerId = $('#txtPropertyManagerId').attr('data_Id');
    var LocationId = $('#ddlLocation option:selected').val();
    var UnitId = $('#ddlUnitID option:selected').val();
    var ResidentTenantName = $('#ddlResidentTenantName option:selected').val();
    var EnterAmountBeingPaid = $('#txtEnterAmountBeingPaid').val();
    var AccountType = $('input[name=AccountType]:checked').val();
    var TransactionType = $('input[name=TransactionType]:checked').val();
    var AccountName = $('#txtName').val();
    var Address = $('#txtAddress').val();
    var Country = $('#ddlCountry option:selected').val();
    var Region = $('#txtRegion').val();
    var City = $('#txtCity').val();
    var State = $('#ddlState option:selected').val();
    var ZipCode = $('#txtZipCode').val();
    var RoutingNo = $('#txtRoutingNumber2ndFromLeft').val();
    var AccountNo = $('#txtAccountNumberLastNumberFromLeft').val();
    var ReRoutingNo = $('#txtReEnterRoutingNumber2ndFromLeft').val();
    var ReAccountNo = $('#txtReEnterAccountNumberLastNumberFromLeft').val();
    var Signature = $('#txtSignature').val();
    var CashReceiptDate = $('#txtDate').val();
    var CashReceiptFrom = $('#txtFrom').val();
    var CashReceiptFor = $('#txtFor').val();

    if (OwnerId === "undefined" || OwnerId === "") {
        isresult = false;
    }
    if (PropertyManagerId === "undefined" || PropertyManagerId === "") {
        isresult = false;
    }
    if (LocationId === "undefined" || LocationId === "-1") {
        $("#s2id_ddlLocation").css({ 'border': '1px solid red', 'border-radius': '5px' });
        isresult = false;
    }
    else {
        $("#s2id_ddlLocation").css({ 'border': '1px solid Transparent' });
    }
    if (EnterAmountBeingPaid == "0" || EnterAmountBeingPaid === "") {
        $("#txtEnterAmountBeingPaid").css({ 'border': '1px solid red' });
        isresult = false;
    }
    else {
        $("#txtEnterAmountBeingPaid").css({ 'border': '1px solid #d2d6de' });
    }
    if (AccountName === "undefined" || AccountName === "") {
        $("#txtName").css({ 'border': '1px solid red' });
        isresult = false;
    }
    else {
        $("#txtName").css({ 'border': '1px solid #d2d6de' });
    }
    if (Address === "undefined" || Address === "") {
        $("#txtAddress").css({ 'border': '1px solid red' });
        isresult = false;
    }
    else {
        $("#txtAddress").css({ 'border': '1px solid #d2d6de' });
    }
    if (City === "undefined" || City === "") {
        $("#txtCity").css({ 'border': '1px solid red' });
        isresult = false;
    }
    else {
        $("#txtCity").css({ 'border': '1px solid #d2d6de' });
    }
    if (Country === "undefined" || Country === "-1") {
        $("#s2id_ddlCountry").css({ 'border': '1px solid red', 'border-radius': '5px' });
        isresult = false;
    }
    else {
        $("#s2id_ddlCountry").css({ 'border': '1px solid Transparent' });
    }
    if (Region === "undefined" || Region === "") {
        $("#txtRegion").css({ 'border': '1px solid red' });
        isresult = false;
    }
    else {
        $("#txtRegion").css({ 'border': '1px solid #d2d6de' });
    }
    if (State === "undefined" || State === "-1") {
        $("#s2id_ddlState").css({ 'border': '1px solid red', 'border-radius': '5px' });
        isresult = false;
    }
    else {
        $("#s2id_ddlState").css({ 'border': '1px solid Transparent' });
    }
    if (ZipCode === "undefined" || ZipCode === "") {
        $("#txtZipCode").css({ 'border': '1px solid red' });
        isresult = false;
    }
    else {
        $("#txtZipCode").css({ 'border': '1px solid #d2d6de' });
    }
    if (AccountType === "Check") {
        if (RoutingNo === "undefined" || RoutingNo === "") {
            $("#txtRoutingNumber2ndFromLeft").css({ 'border': '1px solid red' });
            isresult = false;
        }
        else if (RoutingNo != ReRoutingNo) {
            $("#txtReEnterRoutingNumber2ndFromLeft").css({ 'border': '1px solid red' });
            isresult = false;
        }
        else if (RoutingNo === ReRoutingNo) {
            $("#txtReEnterRoutingNumber2ndFromLeft").css({ 'border': '1px solid #d2d6de' });
        } else {
            $("#txtRoutingNumber2ndFromLeft").css({ 'border': '1px solid #d2d6de' });
        }
        if (AccountNo === "undefined" || AccountNo === "") {
            $("#txtAccountNumberLastNumberFromLeft").css({ 'border': '1px solid red' });
            isresult = false;
        }
        else if (AccountNo != ReAccountNo) {
            $("#txtReEnterAccountNumberLastNumberFromLeft").css({ 'border': '1px solid red' });
            isresult = false;
        }
        else if (AccountNo === ReAccountNo) {
            $("#txtReEnterAccountNumberLastNumberFromLeft").css({ 'border': '1px solid #d2d6de' });
        }
        else {
            $("#txtAccountNumberLastNumberFromLeft").css({ 'border': '1px solid #d2d6de' });
        }
    }
    else {
        if (CashReceiptDate === "undefined" || CashReceiptDate === "") {
            $("#txtDate").css({ 'border': '1px solid red' });
            isresult = false;
        }
        else {
            $("#txtDate").css({ 'border': '1px solid #d2d6de' });
        }
        if (CashReceiptFrom === "undefined" || CashReceiptFrom === "") {
            $("#txtFrom").css({ 'border': '1px solid red' });
            isresult = false;
        }
        else {
            $("#txtFrom").css({ 'border': '1px solid #d2d6de' });
        }
        if (CashReceiptFor === "undefined" || CashReceiptFor === "") {
            $("#txtFor").css({ 'border': '1px solid red' });
            isresult = false;
        }
        else {
            $("#txtFor").css({ 'border': '1px solid #d2d6de' });
        }
        if (Signature === "undefined" || Signature === "") {
            $("#txtSignature").css({ 'border': '1px solid red' });
            isresult = false;
        }
        else {
            $("#txtSignature").css({ 'border': '1px solid #d2d6de' });
        }
    }
    if (TransactionType != 'Payment') {
        if (UnitId === "undefined" || UnitId === "-1") {
            $("#s2id_ddlUnitID").css({ 'border': '1px solid red', 'border-radius': '5px' });
            isresult = false;
        }
        else {
            $("#s2id_ddlUnitID").css({ 'border': '1px solid Transparent' });
        }
        if (ResidentTenantName === "undefined" || ResidentTenantName === "-1") {
            $("#s2id_ddlResidentTenantName").css({ 'border': '1px solid red', 'border-radius': '5px' });
            isresult = false;
        }
        else {
            $("#s2id_ddlResidentTenantName").css({ 'border': '1px solid Transparent' });
        }
    }
    if (isresult)
        isresult = true;
    else {
        isresult = false;
    }
    return isresult;
}
function GetObject() {
    let obj= {
        "OwnerId": $("#spanOwnerId").text(),
        "PropertyManagerId": $('#txtPropertyManagerId').attr('data_Id'),
        "LocationId": $('#ddlLocation option:selected').val(),
        "UnitId": $('#ddlUnitID option:selected').val(),
        "ResidentTenantName": $('#ddlResidentTenantName option:selected').val(),
        "TransactionType": $('input[name=TransactionType]:checked').val(),
        "EnterAmountBeingPaid": $('#txtEnterAmountBeingPaid').val(),
        "AccountType": $('input[name=AccountType]:checked').val(),
        "AccountName": $('#txtName').val(),
        "Address": $('#txtAddress').val(),
        "Country": $('#ddlCountry option:selected').val(),
        "Region": $('#txtRegion').val(),
        "City": $('#txtCity').val(),
        "State": $('#ddlState option:selected').val(),
        "ZipCode": $('#txtZipCode').val(),
        "RoutingNo": $('#txtRoutingNumber2ndFromLeft').val(),
        "AccountNo": $('#txtAccountNumberLastNumberFromLeft').val(),
        "Signature": $('#txtSignature').val(),
        "CashReceiptDate": $('#txtDate').val(),
        "CashReceiptFrom": $('#txtFrom').val(),
        "CashReceiptFor": $('#txtFor').val()
    }
    return obj;
}
$(document).off('click', '#btnCancel').on('click', '#btnCancel', function (parameters) {
    var origin = window.location.origin; // Returns base URL
    var url = origin + "/Pages/DashboardAdmin.aspx";
    window.location.href = url;
});
$('#ddlLocation').change(function () {
    let selectedValue = $(this).val();
    if (selectedValue === "-1") {
        $("#s2id_ddlLocation").css({ 'border': '1px solid red', 'border-radius': '5px' });
    } else {
        $("#s2id_ddlLocation").css({ 'border': '1px solid Transparent' });
    }
});
$('#ddlState').change(function () {
    let selectedValue = $(this).val();
    if (selectedValue === "-1") {
        $("#s2id_ddlState").css({ 'border': '1px solid red', 'border-radius': '5px' });
    } else {
        $("#s2id_ddlState").css({ 'border': '1px solid Transparent' });
    }
});

function ClearData() {
    $('#ddlLocation').val('-1').trigger('change:select2');
    $("#ddlLocation").select2();
    $('#ddlUnitID').val('-1').trigger('change:select2');
    $("#ddlUnitID").select2();
    $('#ddlResidentTenantName').val('-1').trigger('change:select2');
    $("#ddlResidentTenantName").select2();
    $('#txtEnterAmountBeingPaid').val('');
    $('#txtAmountDisable').val('');
    $('#txtName').val('');
    $('#txtAddress').val('');
    $('#ddlCountry').val('US').trigger('change:select2');
    $("#ddlCountry").select2();
    $('#txtRegion').val('');
    $('#txtCity').val('');
    $('#ddlState').val('-1').trigger('change:select2');
    $("#ddlState").select2();
    $('#txtZipCode').val('');
    $('#txtRoutingNumber2ndFromLeft').val('');
    $('#txtAccountNumberLastNumberFromLeft').val('');
    $('#txtReEnterRoutingNumber2ndFromLeft').val('');
    $('#txtReEnterAccountNumberLastNumberFromLeft').val('');
    $('#txtSignature').val('');
    $('#txtDate').val('');
    $('#txtFrom').val('');
    $('#txtFor').val('');
    $('input[name=TransactionType]').closest('div').removeClass('checked');
    $('input[name=TransactionType]').attr('checked', false);
    $("input[name=TransactionType][value='Payment']").closest('div').addClass('checked');
    $("input[name=TransactionType][value='Payment']").attr('checked', true);
    $('input[name=AccountType]').closest('div').removeClass('checked');
    $('input[name=AccountType]').attr('checked', false);
    $("input[name=AccountType][value='Check']").closest('div').addClass('checked');
    $("input[name=AccountType][value='Check']").attr('checked', true);
}