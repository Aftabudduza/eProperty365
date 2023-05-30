var currentPagePath = window.location.pathname + "/";

var pathname = window.location.pathname; // Returns path only
var url = window.location.href;     // Returns full URL
var origin = window.location.origin;   // Returns base URL
var G_RelationalData = [];
$(document).ready(function (parameters) {
    LoadComboBox();
    LoadUnitId();
    LoadTenantName();

});
function LoadTenantName(parameters) {
    var URL = "/Pages/Resident/ResidentTenantDashboard.aspx/" + "GetTenantName";
    var obj = {};
    let tenantName = makeAjaxCallReturnPromiss(URL, obj);
    tenantName.then((data) => {
        var TeantName = $.parseJSON(decodeURIComponent(data.d));
        $("#txtTenantName").text(TeantName);

    }).catch((err) => {
        console.log(err);

    });
}
function LoadUnitId(parameters) {
    var URL = "/Pages/Resident/TenantOnlinefee.aspx/" + "GetUnitId";
    var obj = {};
    let unitId = makeAjaxCallReturnPromiss(URL, obj);
    unitId.then((data) => {
        
        let unit = $.parseJSON(decodeURIComponent(data.d));
        $("#txtUnitId").text(unit);
    }).catch((err) => {
        console.log(err);
    });
 
}
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
    //.................. load State ...............//
    var URL = "/Pages/Admin/AddResidentialUnit.aspx/" + "GetState";
    var obj = {};
    let State = makeAjaxCallReturnPromiss(URL, obj);
    State.then((data) => {
        console.log("State Data Loaded");
        let StateData = setCombo($.parseJSON(decodeURIComponent(data.d)), '-1');
        $(".state option").empty();
        $(".state").append(StateData);
        $(".state").select2();
    }).catch((err) => {
        console.log(err);
    });
    Promise.all([ State]).then(function () {
        // LoadVehicle();
        // LoadPeopleStayingInUnit();
       // loadProfileInfo();

    });


}

$(document).on('keyup', '#rerountingnumapp1Txt', function (parameters) {
    if ($("#routingnumapp1Txt").val() === $(this).val()) {
        $("#rerountingnumapp1Txt").css({ "border": "1px solid Green" });
    } else {
        $("#rerountingnumapp1Txt").css({ "border": "1px solid red" });
    }
});

$(document).on('keyup', '#recheckacctnumapp1Txt', function (parameters) {
    if ($("#checkacctnumapp1Txt").val() === $(this).val()) {
        $("#recheckacctnumapp1Txt").css({ "border": "1px solid Green" });
    } else {
        $("#recheckacctnumapp1Txt").css({ "border": "1px solid red" });
    }
});
$('#ddlLocation').change(function() {
    let selectedValue = $(this).val();
    if (selectedValue === "-1") {
        $("#s2id_ddlLocation").css({ 'border': '1px solid red', 'border-radius': '5px' });
    } else {
        $("#s2id_ddlLocation").css({ 'border': '1px solid Transparent' });
    }
});
$('#ddlstateapp').change(function () {
    let selectedValue = $(this).val();
    if (selectedValue === "-1") {
        $("#s2id_ddlstateapp").css({ 'border': '1px solid red', 'border-radius': '5px' });
    } else {
        $("#s2id_ddlstateapp").css({ 'border': '1px solid Transparent' });
    }
});
function Validation() {
    var isresult = true;
    var accName = $("#nameAccountapp1Txt").val().trim();
    var amount = $("#txtPaidAmount").val().trim();
    var cardAddress = $("#addressapp1Txt1").val().trim();
    var city = $("#cityapp1Txt").val().trim();
    var state = $("#ddlstateapp option:selected").val();
    var zip = $("#zipcodeapp1Txt").val().trim();

    var routingNo = $("#routingnumapp1Txt").val().trim();
    var reRoutingNo = $("#rerountingnumapp1Txt").val().trim();
    var accountNo = $("#checkacctnumapp1Txt").val().trim();
    var reAccountNo = $("#recheckacctnumapp1Txt").val().trim();

    if ($('#ddlLocation option:selected').val() == "-1") {
        $("#s2id_ddlLocation").css({ 'border': '1px solid red','border-radius' : '5px'});
        isresult = false;
    }
    else {
        $("#s2id_ddlLocation").css({ 'border': '1px solid Transparent' });
    }

    if ($('#txtUnitId').text() === "") {
        $("#txtUnitId").css({ 'border': '1px solid red' });
        isresult = false;
    }
    else {
        $("#txtUnitId").css({ 'border': '1px solid Transparent' });
    }

    if (amount == "0" || amount === "") {
        $("#txtPaidAmount").css({ 'border': '1px solid red' });
        isresult = false;
    }
    else {
        $("#txtPaidAmount").css({ 'border': '1px solid #d2d6de' });
    }
    if (accName === "undefined" || accName === "") {
        $("#nameAccountapp1Txt").css({ 'border': '1px solid red' });
        isresult = false;
    }
    else {
        $("#nameAccountapp1Txt").css({ 'border': '1px solid #d2d6de' });
    }
    if (cardAddress === "undefined" || cardAddress === "") {
        $("#addressapp1Txt1").css({ 'border': '1px solid red' });
        isresult = false;
    }
    else {
        $("#addressapp1Txt1").css({ 'border': '1px solid #d2d6de' });
    }

    if (city === "undefined" || city === "") {
        $("#cityapp1Txt").css({ 'border': '1px solid red' });
        isresult = false;
    }
    else {
        $("#cityapp1Txt").css({ 'border': '1px solid #d2d6de' });
    }

    if (state === "undefined" || state === "-1") {
        $("#s2id_ddlstateapp").css({ 'border': '1px solid red', 'border-radius': '5px' });
        isresult = false;
    }
    else {
        $("#s2id_ddlstateapp").css({ 'border': '1px solid Transparent' });
    }

    if (zip === "undefined" || zip === "") {
        $("#zipcodeapp1Txt").css({ 'border': '1px solid red' });
        isresult = false;
    }
    else {
        $("#zipcodeapp1Txt").css({ 'border': '1px solid #d2d6de' });
    }
    if (routingNo === "undefined" || routingNo === "") {
        $("#routingnumapp1Txt").css({ 'border': '1px solid red' });
        isresult = false;
    }
    else if (routingNo != reRoutingNo) {
        $("#rerountingnumapp1Txt").css({ 'border': '1px solid red' });
        isresult = false;
    } else if (routingNo === reRoutingNo) {
        $("#rerountingnumapp1Txt").css({ 'border': '1px solid #d2d6de' });
    } else {
        $("#routingnumapp1Txt").css({ 'border': '1px solid #d2d6de' });
    }
    if (accountNo === "undefined" || accountNo === "") {
        $("#checkacctnumapp1Txt").css({ 'border': '1px solid red' });
        isresult = false;
    }
    else if (accountNo != reAccountNo) {
        $("#recheckacctnumapp1Txt").css({ 'border': '1px solid red' });
        isresult = false;
    } else if (accountNo === reAccountNo) {
        $("#recheckacctnumapp1Txt").css({ 'border': '1px solid #d2d6de' });
    } else {
        $("#checkacctnumapp1Txt").css({ 'border': '1px solid #d2d6de' });
    }
    if (isresult)
        isresult = true;
    else {
        isresult = false;
    }
    return isresult;
}

function GetSavedObj() {
    var obj = {
        "LocationId": $("#ddlLocation option:selected").val(),
        "UnitId": $('#txtUnitId').text(),
        "PaidAmount": $("#txtPaidAmount").val().trim(),
        "AccountName": $('#nameAccountapp1Txt').val().trim(),
        "Address": $("#addressapp1Txt1").val().trim(),
        "City": $("#cityapp1Txt").val().trim(),
        "State": $("#ddlstateapp option:selected").text(),
        "Zip": $("#zipcodeapp1Txt").val().trim(),
        "RoutingNo": $("#routingnumapp1Txt").val().trim(),
        "AccountNo": $("#checkacctnumapp1Txt").val().trim()
        }
    return obj;
}
$(document).on('click', '#btnSubmit', function() {
    if (Validation() == true) {
        
        // if (CheckOut()) {
        var obj = GetSavedObj();
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
                        submitt = true;
                        notify('success', "Saved successfully");
                        // window.location.href = "/Pages/Resident/ResidentialTenantRental_App_Page_1.aspx";
                    } else {
                        submitt = false;
                        notify('danger', "Save Failed !!");
                    }
                }
        });
    }
});
//btnCancel
$(document).on('click', '#btnCancel', function () {
    window.location.href = "/Pages/Resident/ResidentTenantDashboard.aspx";
});