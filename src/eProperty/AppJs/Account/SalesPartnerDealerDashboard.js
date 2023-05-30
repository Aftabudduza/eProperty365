var currentPagePath = window.location.pathname + "/";
$(document).ready(function () {
    //$('#YourCommissions').show()
    //$('.nav-item').hide();
    GetData();
    $('.nav-item').removeClass('active');
    $(".tDate").datepicker().datepicker("setDate", new Date());
    $(".tDate").datepicker({
        dateFormat: "mm-dd-yy",
        changeYear: true,
        changeMonth: true
    });
    $(".ddl").select2();
    $('input[type="checkbox"].flat-red, input[type="radio"].flat-red').iCheck({
        checkboxClass: 'icheckbox_flat-green',
        radioClass: 'iradio_flat-green'
    });
    
});

///////// 
$(document).off('click', '.nav-item').on('click',
    '.nav-item',
    function () {
        //ClearDataSalesPartner();
        //ClearDataDealerProfile();
        let id = $(this).attr('id');
        if (id === "YourCommissions") {
            $('#divSaveCancel').hide();
        }
        else if (id === "YourAccounts") {
            // InitialYourAccountsLoad();
            $('#divSaveCancel').hide();
        }
        else if (id === "SalesPartnerProfile") {
            $('#divSaveCancel').show();
           // GetData();
            //InitialSalesPartnerProfileLoad();
        }
        else if (id === "DealerProfile") {
            $('#divSaveCancel').show();
            //InitialDealerProfileLoad();
        }
    });

function isEmail(email) {
    var regex = /^([a-zA-Z0-9_.+-])+\@(([a-zA-Z0-9-])+\.)+([a-zA-Z0-9]{2,4})+$/;
    return regex.test(email);
}

function GetData() {
    ///Serial Number
    var URL = currentPagePath + "GetData";
    var obj = {};
    //var obj = {
    //    "ProfileName": id
    //};
    let GetData = makeAjaxCallReturnPromiss(URL, obj);
    GetData.then((data) => {
        let result = $.parseJSON(decodeURIComponent(data.d));
        $('.nav-item').removeClass('active');
        debugger;
        $('.tab-pane').removeClass('active show');
        $('#divSaveCancel').hide();
        ClearDataSalesPartner();
        ClearDataDealerProfile();
        if (result[0].IsAdmin == false) {
            $('.adminUse').css('display', 'none');
            if (result[0].UserType === "9") {
                $('#YourCommissions').hide();
                $('#DealerProfile').hide();
                $('#YourAccounts').addClass('active');
                $('#nav-YourAccounts').addClass('active show');
                InitialYourAccountsLoad(result[0].ObjAccounts);
                InitialSalesPartnerProfileLoad(result[0]);
            }
            else if (result[0].UserType === "10") {
                $('#SalesPartnerProfile').hide();
                $('#YourCommissions').hide();
                $('#YourAccounts').addClass('active');
                $('#nav-YourAccounts').addClass('active show');
                //$('#DealerProfile').show();
                //$('#DealerProfile').addClass('active');
                //$('#nav-DealerProfile').addClass('active show');
                InitialYourAccountsLoad(result[0].ObjAccounts);
                InitialDealerProfileLoad(result[0]);
            }
        }
        else if (result[0].IsAdmin == true) {
           
            $('.adminUse').css('display', 'block');
            $('.isAdmin').prop('disabled', false);
            $('#YourAccounts').hide();
            $('#YourCommissions').addClass('active');
            $('#nav-YourCommissions').addClass('active show');
            var URL = currentPagePath + "GetAutoGenNumber";
            var obj = {};
            let SerialNumber = makeAjaxCallReturnPromiss(URL, obj);
            SerialNumber.then((data) => {
                let result = $.parseJSON(decodeURIComponent(data.d));
                $("#h6SalesPartnersIDSalesPartner").text(result[0].SerialCode);
                $("#h6DealerIdDealerProfile").text(result[1].SerialCode);
            }).catch((err) => {
                console.log(err);
            });
            Promise.all([SerialNumber]).then(function () {
               // $('#tblSalesPartnerList tbody tr').remove();
                $('#tblSalesPartnerList tbody').empty();
                $('#tblDealerProfileList tbody').empty();
                InitialSalesPartnerProfileLoad();
                InitialDealerProfileLoad();

                $(result).each(function (index, element) {
                    if (element.UserType === "9") {
                        SetAdminTableSalesPartner(element);
                    }
                    else if (element.UserType === "10") {
                        SetAdminTableDealerProfile(element);
                    }
                });
            });
        }
    }).catch((err) => {
        console.log(err);
    });
    Promise.all([GetData]).then(function () {
    });
}

$(document).off('click', '#btnSave').on('click', '#btnSave', function () {
    var saveObj;
    var validation = false;
    
    var id = $("#nav-tab").find(".active").attr('id');
    if (id === "YourCommissions") {
        
    }
    else if (id === "YourAccounts") {
        
    }
    else if (id === "SalesPartnerProfile") {
        validation = SaveValidationSalesPartner();
        saveObj = GetObjectSalesPartner();
    }
    else if (id === "DealerProfile") {
        validation = SaveValidationDealerProfile();
        saveObj = GetObjectDealerProfile();
    }

    if (validation == true) {
        var obj = saveObj;
        var pagePath = "";
        if ($(this).val() == "Save") {
            pagePath = currentPagePath + "Save";
        }
        else if ($(this).val() == "Update") {
            pagePath = currentPagePath + "Update";
        }
        

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
                        GetData();
                        notify('success', "Saved successfully");
                    } else {
                        notify('danger', "Save Failed !!");
                    }
                }
        });
    }
});


$(document).off('click', '#btnCancel').on('click', '#btnCancel', function (parameters) {
    var origin = window.location.origin; // Returns base URL
    var url = origin + "/Pages/DashboardAdmin.aspx";
    window.location.href = url;
});

function GetDataById(parameters) {
    var URL = currentPagePath + "GetDataById";
    var obj = {
        "Id": parameters,
        "ProfileName": $("#nav-tab").find(".active").attr('id')
    };
    let EditData = makeAjaxCallReturnPromiss(URL, obj);
    EditData.then((data) => {
       
        let resultEditData = $.parseJSON(decodeURIComponent(data.d));
        if (resultEditData.UserType == "9") {
            setDataSalesPartnerProfile(resultEditData);
        }
        else if (resultEditData.UserType == "10") {
            setDataDealerProfile(resultEditData);
        }
    }).catch((err) => {
        console.log(err);
    });
    Promise.all([EditData]).then(function () {
        //GetData();
    });
}

/////// Commission ///////
function InitialYourCommissionsLoad() {
    
}
///////////////////////////////////////////////////// Accounts ///////////////////////////////////////////////////
function InitialYourAccountsLoad(result) {
    $('#txtPartnerNameYourAccount').text(result.PartnerName);
    let Location = setCombo(result.Location, '-1');
    $('#ddlLocationId').get(0).options.length = 0;
    //$("#ddlStateSalesPartner option").empty();
    $("#ddlLocationId").append(Location);
    $("#ddlLocationId").select2();
    
}
$(document).on('change', '#ddlLocationId', function () {
    let LocationId = $(this).val();
    if (LocationId != '-1') {
        var URL = currentPagePath + "GetOwnerByLocationId";
        var obj = {
            "LocationId": LocationId
        };
        let OwnerData = makeAjaxCallReturnPromiss(URL, obj);
        OwnerData.then((data) => {
            let result = $.parseJSON(decodeURIComponent(data.d));
           
            let Owner = setCombo(result.Owner, '-1');
            $('#ddlOwner').get(0).options.length = 0;
            //$("#ddlStateSalesPartner option").empty();
            $("#ddlOwner").append(Owner);
            $("#ddlOwner").select2();
        }).catch((err) => {
            console.log(err);
        });
        Promise.all([OwnerData]).then(function () {
        });
    }
    else {
        $('#ddlOwner').get(0).options.length = 0;
    }
});


$(document).off("click", "#btnSearch").on("click", "#btnSearch", function () {
    $("#tblYourAccounts tbody").empty();
    var URL = currentPagePath + "SearchData";
    var obj = {
        "LocationId": $('#ddlLocationId option:selected').val(),
        "OwnerId": $('#ddlOwner option:selected').val()
    };
    let SearchData = makeAjaxCallReturnPromiss(URL, obj);
    SearchData.then((data) => {
        let result = $.parseJSON(decodeURIComponent(data.d));
        let content = '';
        $(result).each(function (index, element) {
            content += '<tr>' +
                '<td class="OwnerName">' + element.OwnerName + '</td>' +
                '<td class="PropertyManagerName">' + element.PropertyManagerName + '</td>' +
                '<td class="PropertyLocation">' + element.PropertyLocation + '</td>' +
                '<td class="NumberUnits">' + element.NumberUnits + '</td>' +
                '<td class="StartDate">' + element.StartDate + '</td>' +
                ' </tr>';
        });
        
        $('#tblYourAccounts tbody ').append(content);
        $('#tblYourAccounts').DataTable();
    }).catch((err) => {
        console.log(err);
    });
    Promise.all([SearchData]).then(function () {
    });
});
////////////////////////////////////////////////////// Sales Partner /////////////////////////////////////////////////////////////////
function InitialSalesPartnerProfileLoad(result) {
    //////---------- Country -----------//
    var URL = "/Pages/Admin/AddResidentialUnit.aspx/" + "GetCountry";
    var obj = {};
    let Country = makeAjaxCallReturnPromiss(URL, obj);
    Country.then((data) => {
        console.log("Country Data Loaded");
        let countryData = setCombo($.parseJSON(decodeURIComponent(data.d)), 'US');
        $('#ddlCountrySalesPartner').get(0).options.length = 0;
        //$("#ddlCountrySalesPartner option").empty();
        $("#ddlCountrySalesPartner").append(countryData);
        $("#ddlCountrySalesPartner").select2();
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
        $('#ddlStateSalesPartner').get(0).options.length = 0;
        //$("#ddlStateSalesPartner option").empty();
        $("#ddlStateSalesPartner").append(StateData);
        $("#ddlStateSalesPartner").select2();
    }).catch((err) => {
        console.log(err);
    });
    Promise.all([State] , [Country]).then(function () {
        if (result != undefined) {
            setDataSalesPartnerProfile(result);
        }
        
    });
}

function SetAdminTableSalesPartner(element) {
    let comRate = element.CommissionRate == null ? "" : element.CommissionRate;
    let content = '<tr>' +
            '<td class="SerialCode">' + element.SerialCode + '</td>' +
            '<td class="Name">' + element.FirstName + ' ' + element.LastName + '</td>' +
        '<td class="Address1">' + element.Address1 + '</td>' +
        '<td class="CommissionRate">' + comRate + '</td>' +
        '<td class="Email">' + element.Email + '</td>' +
        '<td class="City">' + element.City + '</td>' +
        '<td class="StateId">' + element.StateId + '</td>' +
        '<td class="ZipCode">' + element.ZipCode + '</td>' +
        '<td><button id="' + element.Id + '" data-DealerSalesPartnerId="' + element.DealerSalesPartnerId + '" class="btn btn-danger btnEdit" style="cursor: pointer;"><i class="fa fa-edit"></i></button></td>' +
        ' </tr>';
    $('#tblSalesPartnerList tbody ').append(content);
}
function setDataSalesPartnerProfile(result) {
    $("#h6SalesPartnersIDSalesPartner").text(result.SerialCode);
    $('#txtEmailAddressSalesPartner').val(result.Email);
    //$('#txtEmailAddressSalesPartner').val(result.userProfileId);
    $('#txtEmailAddressSalesPartner').attr('data-userProfileId', result.userProfileId);
    $('#txtReEnterEmailAddressSalesPartner').val(result.Email);
    $('#txtPasswordSalesPartner').val(result.Password);
    $('#txtReEnterPasswordSalesPartner').val(result.Password);
    if (result.Id >0) {
        $('#ddlStateSalesPartner').val(result.StateId).trigger('change:select2');
        $("#ddlStateSalesPartner").select2();
        $('#ddlCountrySalesPartner').val(result.CountryId).trigger('change:select2');
        $("#ddlCountrySalesPartner").select2();
        $('#txtSalesPartnerFirstName').val(result.FirstName);
        $('#txtSalesPartnerLastName').val(result.LastName);
        $('#txtAreaAddress1SalesPartner').val(result.Address1);
        $('#txtAreaAddress2SalesPartner').val(result.Address2);
        $('#txtCommissionRateSalesPartner').val(result.CommissionRate);
        $('#txtCitySalesPartner').val(result.City);
        $('#txtRegionSalesPartner').val(result.Region);
        $('#txtZipCodeSalesPartner').val(result.ZipCode);
        $('#txtPrimaryPhoneNumberSalesPartner').val(result.PrimaryPhoneNo);
        $('#txtMobilePhoneNumberSalesPartner').val(result.MobilePhoneNo);
        $('#btnSave').val('Update');
        $('#btnSave').attr('data-UpdateId', result.Id);
        $('#txtRoutingNoSalesPartner').val(result.RoutingNo);
        $('#txtAccountNoSalesPartner').val(result.AccountNo);
        $('#txtJoinDateSalesPartner').val(result.JoinDate);
        if (result.ListDetails.length > 0) {
            let content = '';
            $(result.ListDetails).each(function (index, element) {
                content += '<tr>' +
                    '<td class="zipCode">' + element.ZipCode + '</td>' +
                    '<td class="comrate">' + element.CommissionRate + '</td>' +
                    '<td><button id="' + element.Id + '" data-DealerSalesPartnerId="' + element.DealerSalesPartnerId + '" class="btn btn-danger btnRemoveSalesPartner" style="cursor: pointer;"><i class="fa fa-trash"></i></button></td>' +
                    //'<td>' + '<input type="button" class="btn btnRemoveSalesPartner" style="background-color: #3B5998" value="X" id="btnRemoveSalesPartner"/>' + '</td>' +
                    ' </tr>';
            });
            $('#tblSalesPartner tbody').append(content);
        }
        
    } 
}
$(document).on('keyup', '#txtEmailAddressSalesPartner', function () {
    let filter = $(this).val();
    if (isEmail(filter)) {
        $("#txtEmailAddressSalesPartner").css({ "border": "1px solid Green" });
        return true;
    }
    else {
        $("#txtEmailAddressSalesPartner").css({ "border": "1px solid red" });
        return false;
    }
});

$(document).on('keyup', '#txtReEnterEmailAddressSalesPartner', function (parameters) {
    if ($("#txtEmailAddressSalesPartner").val() === $(this).val()) {
        $("#txtReEnterEmailAddressSalesPartner").css({ "border": "1px solid Green" });
    } else {
        $("#txtReEnterEmailAddressSalesPartner").css({ "border": "1px solid red" });
    }
});

$(document).on('keyup', '#txtReEnterPasswordSalesPartner', function (parameters) {
    if ($("#txtPasswordSalesPartner").val() === $(this).val()) {
        $("#txtReEnterPasswordSalesPartner").css({ "border": "1px solid Green" });
    } else {
        $("#txtReEnterPasswordSalesPartner").css({ "border": "1px solid red" });
    }
});

$(document).off("click", "#btnAddSalesPartner").on("click", "#btnAddSalesPartner",
    function() {
        let zipCode = $('#txtTblNameofZipSalesPartner').val();
        let comrate = $('#txtTblCommissionSalesPartner').val();
        if (zipCode != "" && comrate != "") {
            let content = '<tr>' +
                '<td class="zipCode">' + zipCode + '</td>' +
                '<td class="comrate">' + comrate + '</td>' +
                '<td><button id="0" data-DealerSalesPartnerId="0" class="btn btn-danger btnRemoveSalesPartner" style="cursor: pointer;"><i class="fa fa-trash"></i></button></td>' +
                //'<td>' + '<input type="button" class="btn btnRemoveSalesPartner" style="background-color: #3B5998" value="X" id="btnRemoveSalesPartner"/>' + '</td>' +
                ' </tr>';
            $('#tblSalesPartner tbody').append(content);
            $('#txtTblNameofZipSalesPartner').val('');
            $('#txtTblCommissionSalesPartner').val(20);
        }
        
    });

$(document).off("click", ".btnRemoveSalesPartner").on("click", ".btnRemoveSalesPartner",
    function () {
        var tblRow = $(this).closest('tr');
        let DealerSalesPartnerId = $(this).attr('data-DealerSalesPartnerId');
        debugger;
        let commissionRate = $(tblRow).find('.comrate').text();
        let zipCode = $(tblRow).find('.zipCode').text();
        let Id = $(this).attr('id');
        if (parseInt(Id) > 0) {
            var URL = currentPagePath + "DeleteData";
            var obj = {
                "DealerSalesPartnerId": DealerSalesPartnerId,
                "Id": Id,
                "commissionRate": commissionRate,
                "zipCode": zipCode
            };
            let DeleteData = makeAjaxCallReturnPromiss(URL, obj);
            DeleteData.then((data) => {
                let result = $.parseJSON(decodeURIComponent(data.d));
                if (result == true) {
                    $(tblRow).remove();
                }
            }).catch((err) => {
                console.log(err);
            });
            Promise.all([DeleteData]).then(function() {
            });
        } else {
            $(tblRow).remove();
        }
    });

$(document).off("click", ".btnEdit").on("click", ".btnEdit",
    function () {
        $('#tblDealerProfile tbody').empty();
        $('#tblSalesPartner tbody').empty();
        let id = $(this).attr('id');
        GetDataById(id);
    });

function SaveValidationSalesPartner() {
    var isresult = true;
    var ddlStateSalesPartner = $('#ddlStateSalesPartner option:selected').val();
    var ddlCountrySalesPartner = $('#ddlCountrySalesPartner option:selected').val();
    var txtSalesPartnerFirstName = $('#txtSalesPartnerFirstName').val();
    var txtAreaAddress1SalesPartner = $('#txtAreaAddress1SalesPartner').val();
    var txtCitySalesPartner = $('#txtCitySalesPartner').val();
    var txtZipCodeSalesPartner = $('#txtZipCodeSalesPartner').val();
    var txtPrimaryPhoneNumberSalesPartner = $('#txtPrimaryPhoneNumberSalesPartner').val();
    var txtReEnterEmailAddressSalesPartner = $('#txtReEnterEmailAddressSalesPartner').val();
    var txtReEnterPasswordSalesPartner = $('#txtReEnterPasswordSalesPartner').val();

    if (txtSalesPartnerFirstName === "undefined" || txtSalesPartnerFirstName === "") {
        $("#txtSalesPartnerFirstName").css({ 'border': '1px solid red' });
        isresult = false;
    }
    else {
        $("#txtSalesPartnerFirstName").css({ 'border': '1px solid #d2d6de' });
    }
    if (txtAreaAddress1SalesPartner === "undefined" || txtAreaAddress1SalesPartner === "") {
        $("#txtAreaAddress1SalesPartner").css({ 'border': '1px solid red' });
        isresult = false;
    }
    else {
        $("#txtAreaAddress1SalesPartner").css({ 'border': '1px solid #d2d6de' });
    }
    if (txtCitySalesPartner === "undefined" || txtCitySalesPartner === "") {
        $("#txtCitySalesPartner").css({ 'border': '1px solid red' });
        isresult = false;
    }
    else {
        $("#txtCitySalesPartner").css({ 'border': '1px solid #d2d6de' });
    }
    if (txtZipCodeSalesPartner === "undefined" || txtZipCodeSalesPartner === "") {
        $("#txtZipCodeSalesPartner").css({ 'border': '1px solid red' });
        isresult = false;
    }
    else {
        $("#txtZipCodeSalesPartner").css({ 'border': '1px solid #d2d6de' });
    }
    if (txtPrimaryPhoneNumberSalesPartner === "undefined" || txtPrimaryPhoneNumberSalesPartner === "") {
        $("#txtPrimaryPhoneNumberSalesPartner").css({ 'border': '1px solid red' });
        isresult = false;
    }
    else {
        $("#txtPrimaryPhoneNumberSalesPartner").css({ 'border': '1px solid #d2d6de' });
    }
    if ($('#txtEmailAddressSalesPartner').val() === "") {
        $("#txtEmailAddressSalesPartner").css({ 'border': '1px solid red' });
        isresult = false;
    }
    else {
        $("#txtEmailAddressSalesPartner").css({ 'border': '1px solid #d2d6de' });
    }
    if ($('#txtPasswordSalesPartner').val() === "") {
        $("#txtPasswordSalesPartner").css({ 'border': '1px solid red' });
        isresult = false;
    }
    else {
        $("#txtPasswordSalesPartner").css({ 'border': '1px solid #d2d6de' });
    }
    if (txtReEnterEmailAddressSalesPartner === "undefined" || txtReEnterEmailAddressSalesPartner === "" 
        || txtReEnterEmailAddressSalesPartner != $('#txtEmailAddressSalesPartner').val()) {
        $("#txtReEnterEmailAddressSalesPartner").css({ 'border': '1px solid red' });
        isresult = false;
    }
    else {
        $("#txtReEnterEmailAddressSalesPartner").css({ 'border': '1px solid #d2d6de' });
    }
    if (txtReEnterPasswordSalesPartner === "undefined" || txtReEnterPasswordSalesPartner === ""
            || txtReEnterPasswordSalesPartner != $('#txtPasswordSalesPartner').val()) {
        $("#txtReEnterPasswordSalesPartner").css({ 'border': '1px solid red' });
        isresult = false;
    }
    else {
        $("#txtReEnterPasswordSalesPartner").css({ 'border': '1px solid #d2d6de' });
    }
    if (ddlStateSalesPartner === "undefined" || ddlStateSalesPartner === "-1") {
        $("#s2id_ddlStateSalesPartner").css({ 'border': '1px solid red', 'border-radius': '5px' });
        isresult = false;
    }
    else {
        $("#s2id_ddlStateSalesPartner").css({ 'border': '1px solid Transparent' });
    }
    if (ddlCountrySalesPartner === "undefined" || ddlCountrySalesPartner === "-1") {
        $("#s2id_ddlCountrySalesPartner").css({ 'border': '1px solid red', 'border-radius': '5px' });
        isresult = false;
    }
    else {
        $("#s2id_ddlCountrySalesPartner").css({ 'border': '1px solid Transparent' });
    }
    if (isresult)
        isresult = true;
    else {
        isresult = false;
    }
    return isresult;
}

function GetObjectSalesPartner() {
    let obj = {
        "Id": $('#btnSave').attr('data-UpdateId'),
        "SerialCode": $('#h6SalesPartnersIDSalesPartner').text(),
        "FirstName": $('#txtSalesPartnerFirstName').val(),
        "LastName": $('#txtSalesPartnerLastName').val(),
        "Address1": $('#txtAreaAddress1SalesPartner').val(),
        "Address2": $('#txtAreaAddress2SalesPartner').val(),
        "CommissionRate": $('#txtCommissionRateSalesPartner').val(),
        "City": $('#txtCitySalesPartner').val(),
        "Region": $('#txtRegionSalesPartner').val(),
        "ZipCode": $('#txtZipCodeSalesPartner').val(),
        "PrimaryPhoneNo": $('#txtPrimaryPhoneNumberSalesPartner').val(),
        "MobilePhoneNo": $('#txtMobilePhoneNumberSalesPartner').val(),
        "Email": $('#txtEmailAddressSalesPartner').val(),
        "ProfileName": $("#nav-tab").find(".active").attr('id'),
        "Password": $('#txtPasswordSalesPartner').val(),
        "JoinDate": $('#txtJoinDateSalesPartner').val(),
        "StateId": $('#ddlStateSalesPartner option:selected').val(),
        "CountryId": $('#ddlCountrySalesPartner option:selected').val(),
        "userProfileId": $('#txtEmailAddressSalesPartner').attr('data-userProfileId'),
        "RoutingNo": $('#txtRoutingNoSalesPartner').val(),
        "AccountNo": $('#txtAccountNoSalesPartner').val(),
        "ListDetails":null
    }
    var dtlList = [];
    $('#tblSalesPartner tbody tr').each(function() {
        let dtl = {
            "Id": $(this).find('.btnRemoveSalesPartner').attr('id'),
            "DealerSalesPartnerId": $(this).find('.btnRemoveSalesPartner').attr('data-DealerSalesPartnerId'),
            "ZipCode": $(this).find('.zipCode').text(),
            "CommissionRate": $(this).find('.comrate').text()
        };
        dtlList.push(dtl);
    });
    obj.ListDetails = dtlList;
    return obj;
}

function ClearDataSalesPartner() {
    $('#ddlStateSalesPartner').val('-1').trigger('change:select2');
    $("#ddlStateSalesPartner").select2();
    $('#ddlCountrySalesPartner').val('US').trigger('change:select2');
    $("#ddlCountrySalesPartner").select2();
    $(".tDate").datepicker().datepicker("setDate", new Date());
    //GetData();
    $('#txtSalesPartnerFirstName').val('');
    $('#txtSalesPartnerLastName').val('');
    $('#txtAreaAddress1SalesPartner').val('');
    $('#txtAreaAddress2SalesPartner').val('');
    $('#txtCommissionRateSalesPartner').val('20');
    $('#txtCitySalesPartner').val('');
    $('#txtRegionSalesPartner').val('');
    $('#txtZipCodeSalesPartner').val('');
    $('#txtPrimaryPhoneNumberSalesPartner').val('');
    $('#txtMobilePhoneNumberSalesPartner').val('');
    $('#txtEmailAddressSalesPartner').val('');
    $('#txtReEnterEmailAddressSalesPartner').val('');
    $('#txtPasswordSalesPartner').val('');
    $('#txtReEnterPasswordSalesPartner').val('');
    //$("#chkIsActive").closest('div').addClass('checked');
    //$("#chkIsActive").attr('checked', true);
    $('#tblSalesPartner tbody tr').remove();
    $('#btnSave').val('Save');
    $('#txtEmailAddressSalesPartner').attr('data-userProfileId', "0");
    $('#btnSave').attr('data-UpdateId', "0");
}

$(document).off("change", ".mailcheck").on("change", ".mailcheck", function () {
    var obj = {
        "ProfileName": $("#nav-tab").find(".active").attr('id'),
        "Email": $(this).val()
    };
    var URL = currentPagePath + "AlreadyExistMail";
    let id = $(this).attr('id');
    let checkMail = makeAjaxCallReturnPromiss(URL, obj);
    checkMail.then((data) => {
        let result = $.parseJSON(decodeURIComponent(data.d));
        if (result == true) {
            $('#' + id).val('');
        }
    }).catch((err) => {
        console.log(err);
    });
    Promise.all([checkMail]).then(function () {
    });
});
//////////////////////////////////////////   DealerProfile ///////////////////////////////////////////////

function InitialDealerProfileLoad(result) {
    //// State ////
    var URL = "/Pages/Admin/AddResidentialUnit.aspx/" + "GetState";
    var obj = {};
    let State = makeAjaxCallReturnPromiss(URL, obj);
    State.then((data) => {
        console.log("State Data Loaded");
        let StateData = setCombo($.parseJSON(decodeURIComponent(data.d)), '-1');
        $('#ddlStateDealerProfile').get(0).options.length = 0;
        $("#ddlStateDealerProfile option").empty();
        $("#ddlStateDealerProfile").append(StateData);
        $("#ddlStateDealerProfile").select2();
    }).catch((err) => {
        console.log(err);
    });
    Promise.all([State]).then(function () {
        if (result != undefined) {
            setDataDealerProfile(result);
        }
    });
}

function setDataDealerProfile(result) {
    $("#h6DealerIdDealerProfile").text(result.SerialCode);
    $('#txtEmailAddressDealerProfile').val(result.Email);
    //$('#txtEmailAddressDealerProfile').val(result.userProfileId);
    $('#txtEmailAddressDealerProfile').attr('data-userProfileId', result.userProfileId);
    $('#txtReEnterEmailAddressDealerProfile').val(result.Email);
    $('#txtPasswordDealerProfile').val(result.Password);
    $('#txtReEnterPasswordDealerProfile').val(result.Password);
    if (result.Id > 0) {
        $('#ddlStateDealerProfile').val(result.StateId).trigger('change:select2');
        $("#ddlStateDealerProfile").select2();
        //$('#ddlCountryDealerProfile').val(result.CountryId).trigger('change:select2');
        //$("#ddlCountryDealerProfile").select2();
        $('#txtDealerProfileFirstName').val(result.FirstName);
        $('#txtDealerProfileLastName').val(result.LastName);
        $('#txtAreaAddress1DealerProfile').val(result.Address1);
        $('#txtAreaAddress2DealerProfile').val(result.Address2);
        $('#txtCommissionRateDealerProfile').val(result.CommissionRate);
        $('#txtCityDealerProfile').val(result.City);
        //$('#txtRegionDealerProfile').val(result.Region);
        $('#txtZipCodeDealerProfile').val(result.ZipCode);
        $('#txtPrimaryPhoneNumberDealerProfile').val(result.PrimaryPhoneNo);
        $('#txtMobilePhoneNumberDealerProfile').val(result.MobilePhoneNo);
        $('#btnSave').val('Update');
        $('#btnSave').attr('data-UpdateId', result.Id);
        $('#txtRoutingNoDealerProfile').val(result.RoutingNo);
        $('#txtAccountNoDealerProfile').val(result.AccountNo);
        $('#txtJoinDateDealerProfile').val(result.JoinDate);
        if (result.ListDetails.length > 0) {
            let content = '';
            $(result.ListDetails).each(function (index, element) {
                content += '<tr>' +
                    '<td class="zipCode">' + element.ZipCode + '</td>' +
                    '<td class="comrate">' + element.CommissionRate + '</td>' +
                    '<td><button id="' + element.Id + '" data-DealerSalesPartnerId="' + element.DealerSalesPartnerId + '" class="btn btn-danger btnRemoveDealerProfile" style="cursor: pointer;"><i class="fa fa-trash"></i></button></td>' +
                    //'<td>' + '<input type="button" class="btn btnRemoveDealerProfile" style="background-color: #3B5998" value="X" id="btnRemoveDealerProfile"/>' + '</td>' +
                    ' </tr>';
            });
            $('#tblDealerProfile tbody').append(content);
        }

    }
}
function SetAdminTableDealerProfile(element) {
    let comRate = element.CommissionRate == null ? "" : element.CommissionRate;
    let content = '<tr>' +
        '<td class="SerialCode">' + element.SerialCode + '</td>' +
        '<td class="Name">' + element.FirstName + ' ' + element.LastName + '</td>' +
        '<td class="Address1">' + element.Address1 + '</td>' +
        '<td class="CommissionRate">' + comRate + '</td>' +
        '<td class="Email">' + element.Email + '</td>' +
        '<td class="City">' + element.City + '</td>' +
        '<td class="StateId">' + element.StateId + '</td>' +
        '<td class="ZipCode">' + element.ZipCode + '</td>' +
        '<td><button id="' + element.Id + '" data-DealerSalesPartnerId="' + element.DealerSalesPartnerId + '" class="btn btn-danger btnEdit" style="cursor: pointer;"><i class="fa fa-edit"></i></button></td>' +
        ' </tr>';

    $('#tblDealerProfileList tbody ').append(content);

}

$(document).on('keyup', '#txtEmailAddressDealerProfile', function () {
    let filter = $(this).val();
    if (isEmail(filter)) {
        $("#txtEmailAddressDealerProfile").css({ "border": "1px solid Green" });
        return true;
    }
    else {
        $("#txtEmailAddressDealerProfile").css({ "border": "1px solid red" });
        return false;
    }
});

$(document).on('keyup', '#txtReEnterEmailAddressDealerProfile', function (parameters) {
    if ($("#txtEmailAddressDealerProfile").val() === $(this).val()) {
        $("#txtReEnterEmailAddressDealerProfile").css({ "border": "1px solid Green" });
    } else {
        $("#txtReEnterEmailAddressDealerProfile").css({ "border": "1px solid red" });
    }
});

$(document).on('keyup', '#txtReEnterPasswordDealerProfile', function (parameters) {
    if ($("#txtPasswordDealerProfile").val() === $(this).val()) {
        $("#txtReEnterPasswordDealerProfile").css({ "border": "1px solid Green" });
    } else {
        $("#txtReEnterPasswordDealerProfile").css({ "border": "1px solid red" });
    }
});

$(document).off("click", "#btnAddDealerProfile").on("click", "#btnAddDealerProfile",
    function () {
        let zipCode = $('#txtTblZipCodeDealerProfile').val();
        let comrate = $('#txtTblCommissionPaidDealerProfile').val();
        if (zipCode != "" && comrate != "") {
            let content = '<tr>' +
                '<td class="zipCode">' +
                zipCode +
                '</td>' +
                '<td class="comrate">' +
                comrate +
                '</td>' +
                '<td>' +
                '<button  id="0" data-DealerSalesPartnerId="0" class="btn btn-danger btnRemoveDealerProfile" style="cursor: pointer;"><i class="fa fa-trash"></i></button>' +
                '</td>' +
                //'<td>' + '<input type="button" class="btn btnRemoveDealer" style="background-color: #3B5998" value="X" id="btnRemoveDealer"/>' + '</td>' +
                ' </tr>';
            $('#tblDealerProfile tbody').append(content);
            $('#txtTblZipCodeDealerProfile').val('');
            $('#txtTblCommissionPaidDealerProfile').val(10);
        }
    });

$(document).off("click", ".btnRemoveDealerProfile").on("click", ".btnRemoveDealerProfile",
    function () {
        var tblRow = $(this).closest('tr');
        let DealerSalesPartnerId = $(this).attr('data-dealersalespartnerid');
        let commissionRate = $(tblRow).find('.comrate').text();
        let zipCode = $(tblRow).find('.zipCode').text();
        let Id = $(this).attr('id');
        if (parseInt(Id) > 0) {
            var URL = currentPagePath + "DeleteData";
            var obj = {
                "DealerSalesPartnerId": DealerSalesPartnerId,
                "Id": Id,
                "commissionRate": commissionRate,
                "zipCode": zipCode
            };
            let DeleteData = makeAjaxCallReturnPromiss(URL, obj);
            DeleteData.then((data) => {
                let result = $.parseJSON(decodeURIComponent(data.d));

                if (result == true) {
                    $(tblRow).remove();
                }
            }).catch((err) => {
                console.log(err);
            });
            Promise.all([DeleteData]).then(function() {
            });
        } else {
            $(tblRow).remove();
        }
    });

function SaveValidationDealerProfile() {
    var isresult = true;
    var ddlStateDealerProfile = $('#ddlStateDealerProfile option:selected').val();
    var txtDealerProfileFirstName = $('#txtDealerProfileFirstName').val();
    var txtAreaAddress1DealerProfile = $('#txtAreaAddress1DealerProfile').val();
    var txtCityDealerProfile = $('#txtCityDealerProfile').val();
    var txtZipCodeDealerProfile = $('#txtZipCodeDealerProfile').val();
    var txtPrimaryPhoneNumberDealerProfile = $('#txtPrimaryPhoneNumberDealerProfile').val();
    var txtReEnterEmailAddressDealerProfile = $('#txtReEnterEmailAddressDealerProfile').val();
    var txtReEnterPasswordDealerProfile = $('#txtReEnterPasswordDealerProfile').val();

    if (txtDealerProfileFirstName === "undefined" || txtDealerProfileFirstName === "") {
        $("#txtDealerProfileFirstName").css({ 'border': '1px solid red' });
        isresult = false;
    }
    else {
        $("#txtDealerProfileFirstName").css({ 'border': '1px solid #d2d6de' });
    }
    if (txtAreaAddress1DealerProfile === "undefined" || txtAreaAddress1DealerProfile === "") {
        $("#txtAreaAddress1DealerProfile").css({ 'border': '1px solid red' });
        isresult = false;
    }
    else {
        $("#txtAreaAddress1DealerProfile").css({ 'border': '1px solid #d2d6de' });
    }
    if (txtCityDealerProfile === "undefined" || txtCityDealerProfile === "") {
        $("#txtCityDealerProfile").css({ 'border': '1px solid red' });
        isresult = false;
    }
    else {
        $("#txtCityDealerProfile").css({ 'border': '1px solid #d2d6de' });
    }
    if (txtZipCodeDealerProfile === "undefined" || txtZipCodeDealerProfile === "") {
        $("#txtZipCodeDealerProfile").css({ 'border': '1px solid red' });
        isresult = false;
    }
    else {
        $("#txtZipCodeDealerProfile").css({ 'border': '1px solid #d2d6de' });
    }
    if (txtPrimaryPhoneNumberDealerProfile === "undefined" || txtPrimaryPhoneNumberDealerProfile === "") {
        $("#txtPrimaryPhoneNumberDealerProfile").css({ 'border': '1px solid red' });
        isresult = false;
    }
    else {
        $("#txtPrimaryPhoneNumberDealerProfile").css({ 'border': '1px solid #d2d6de' });
    }
    if ($('#txtEmailAddressDealerProfile').val() === "") {
        $("#txtEmailAddressDealerProfile").css({ 'border': '1px solid red' });
        isresult = false;
    }
    else {
        $("#txtEmailAddressDealerProfile").css({ 'border': '1px solid #d2d6de' });
    }
    if ($('#txtPasswordDealerProfile').val() === "") {
        $("#txtPasswordDealerProfile").css({ 'border': '1px solid red' });
        isresult = false;
    }
    else {
        $("#txtPasswordDealerProfile").css({ 'border': '1px solid #d2d6de' });
    }
    if (txtReEnterEmailAddressDealerProfile === "undefined" || txtReEnterEmailAddressDealerProfile === ""
            || txtReEnterEmailAddressDealerProfile != $('#txtEmailAddressDealerProfile').val()) {
        $("#txtReEnterEmailAddressDealerProfile").css({ 'border': '1px solid red' });
        isresult = false;
    }
    else {
        $("#txtReEnterEmailAddressDealerProfile").css({ 'border': '1px solid #d2d6de' });
    }
   
    if (txtReEnterPasswordDealerProfile === "undefined" || txtReEnterPasswordDealerProfile === ""
        || txtReEnterPasswordDealerProfile != $('#txtPasswordDealerProfile').val()) {
        $("#txtReEnterPasswordDealerProfile").css({ 'border': '1px solid red' });
        isresult = false;
    }
    else {
        $("#txtReEnterPasswordDealerProfile").css({ 'border': '1px solid #d2d6de' });
    }
    if (ddlStateDealerProfile === "undefined" || ddlStateDealerProfile === "-1") {
        $("#s2id_ddlStateDealerProfile").css({ 'border': '1px solid red', 'border-radius': '5px' });
        isresult = false;
    }
    else {
        $("#s2id_ddlStateDealerProfile").css({ 'border': '1px solid Transparent' });
    }
    if (isresult)
        isresult = true;
    else {
        isresult = false;
    }
    return isresult;
}

function GetObjectDealerProfile() {
    let obj = {
        "Id": $('#btnSave').attr('data-UpdateId'),
        "SerialCode": $('#h6DealerIdDealerProfile').text(),
        "FirstName": $('#txtDealerProfileFirstName').val(),
        "LastName": $('#txtDealerProfileLastName').val(),
        "Address1": $('#txtAreaAddress1DealerProfile').val(),
        "Address2": $('#txtAreaAddress2DealerProfile').val(),
        "CommissionRate": $('#txtCommissionRateDealerProfile').val(),
        "City": $('#txtCityDealerProfile').val(),
        "ZipCode": $('#txtZipCodeDealerProfile').val(),
        "PrimaryPhoneNo": $('#txtPrimaryPhoneNumberDealerProfile').val(),
        "MobilePhoneNo": $('#txtMobilePhoneNumberDealerProfile').val(),
        "Email": $('#txtEmailAddressDealerProfile').val(),
        "ProfileName": $("#nav-tab").find(".active").attr('id'),
        "Password": $('#txtPasswordDealerProfile').val(),
        "JoinDate": $('#txtJoinDateDealerProfile').val(),
        "StateId": $('#ddlStateDealerProfile option:selected').val(),
        "userProfileId": $('#txtEmailAddressDealerProfile').attr('data-userProfileId'),
        "RoutingNo": $('#txtRoutingNoDealerProfile').val(),
        "AccountNo": $('#txtAccountNoDealerProfile').val(),
        "ListDetails": null
    }
    var dtlList = [];
    $('#tblDealerProfile tbody tr').each(function () {
        let dtl = {
            "Id": $(this).find('.btnRemoveDealerProfile').attr('id'),
            "DealerSalesPartnerId": $(this).find('.btnRemoveDealerProfile').attr('data-DealerSalesPartnerId'),
            "ZipCode": $(this).find('.zipCode').text(),
            "CommissionRate": $(this).find('.comrate').text()
        };
        dtlList.push(dtl);
    });
    obj.ListDetails = dtlList;
    return obj;
}

function ClearDataDealerProfile() {
    $('#ddlStateDealerProfile').val('-1').trigger('change:select2');
    $("#ddlStateDealerProfile").select2();
    $(".tDate").datepicker().datepicker("setDate", new Date());
    //GetData();
    $('#txtDealerProfileFirstName').val('');
    $('#txtDealerProfileLastName').val('');
    $('#txtAreaAddress1DealerProfile').val('');
    $('#txtAreaAddress2DealerProfile').val('');
    $('#txtCommissionRateDealerProfile').val('20');
    $('#txtCityDealerProfile').val('');
    $('#txtZipCodeDealerProfile').val('');
    $('#txtPrimaryPhoneNumberDealerProfile').val('');
    $('#txtMobilePhoneNumberDealerProfile').val('');
    $('#txtEmailAddressDealerProfile').val('');
    $('#txtReEnterEmailAddressDealerProfile').val('');
    $('#txtPasswordDealerProfile').val('');
    $('#txtReEnterPasswordDealerProfile').val('');
    $('#txtRoutingNoDealerProfile').val('');
    $('#txtAccountNoDealerProfile').val('');
    //$("#chkIsActive").closest('div').addClass('checked');
    //$("#chkIsActive").attr('checked', true);
    $('#tblDealerProfile tbody tr').remove();
    $('#btnSave').val('Save');
    $('#txtEmailAddressDealerProfile').attr('data-userProfileId', "0");
    $('#btnSave').attr('data-UpdateId', "0");
    
}