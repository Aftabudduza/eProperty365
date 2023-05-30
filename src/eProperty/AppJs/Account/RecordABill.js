var currentPagePath = window.location.pathname + "/";
var global_Type;
$(document).ready(function () {
    
    $("#txtDate").datepicker().datepicker("setDate", new Date());
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
    InitialLoad();
});

$(document).on('ifChanged', "input[type=radio][name=GType]", function (parameters) {
    let changeValue = $(this).val();
    $('#divBillnumber').css({ 'display': 'none' });
    ClearData();
    if ($(this).is(':checked')) {
        if (changeValue == 'NEW') {
            GetAutoGenNumber();
        }
        else if (changeValue == 'EDIT') {
            $('#divBillnumber').css({ 'display': 'block' });
            SetBillNumberDropdown();
        }
    }
});

function SetBillNumberDropdown() {
    var URLBillNumber = currentPagePath + "GetBillNumber";
    var obj = {};
    let BillNumber = makeAjaxCallReturnPromiss(URLBillNumber, obj);
    BillNumber.then((data) => {
        let result = setDropDown($.parseJSON(decodeURIComponent(data.d)));
        $('#ddlBillNumber').get(0).options.length = 0;
        //$("#ddlStateSalesPartner option").empty();
        $("#ddlBillNumber").append(result);
        $("#ddlBillNumber").select2({
            placeholder: "Select a Bill Number",
            allowClear: true
        });
    }).catch((err) => {
        console.log(err);
    });
}
function GetAutoGenNumber() {
    var URLBillNumber = currentPagePath + "GetAutoGenNumber";
    var obj = {};
    let BillNumber = makeAjaxCallReturnPromiss(URLBillNumber, obj);
    BillNumber.then((data) => {
        let result = $.parseJSON(decodeURIComponent(data.d));
        $('#txtBillNumber').val(result);
    }).catch((err) => {
        console.log(err);
    });
}

function InitialLoad() {
    GetAutoGenNumber();
    var URL = "/Pages/Admin/AddResidentialUnit.aspx/" + "GetState";
    var obj = {};
    let State = makeAjaxCallReturnPromiss(URL, obj);
    State.then((data) => {
        let StateData = setCombo($.parseJSON(decodeURIComponent(data.d)), '-1');
        $('#ddlState').get(0).options.length = 0;
        //$("#ddlStateSalesPartner option").empty();
        $("#ddlState").append(StateData);
        $("#ddlState").select2({
            placeholder: "Select a state",
            allowClear: true
        });
    }).catch((err) => {
        console.log(err);
    });
    // ......... Account Type ............... //
    var URLAccountType = currentPagePath + "GetAccountType";
    
    let AccountType = makeAjaxCallReturnPromiss(URLAccountType, obj);
    AccountType.then((data) => {
        global_Type = $.parseJSON(decodeURIComponent(data.d));
    }).catch((err) => {
        console.log(err);
    });
   
    Promise.all([AccountType, State]).then(function () {
        addRow();
    });
}
$(document).off('change', '#ddlBillNumber').on('change', '#ddlBillNumber', function () {
    let selectedValue = $(this).val();
    
    if (selectedValue != '-1') {
        $('#txtBillNumber').val($(this).find('option:selected').text());
        SetDataByBillId(selectedValue);
        
    } else {
        $('#txtBillNumber').val('');
    }
    //$('#ddlResidentTenantName').select2();
});

function SetDataByBillId(parameters) {
    var URL = currentPagePath + "GetBillNumberWiseData";
    var obj = {
        "Id": parameters
    };
    let BillNumberData = makeAjaxCallReturnPromiss(URL, obj);
    BillNumberData.then((data) => {
        let result = $.parseJSON(decodeURIComponent(data.d));
        SetSaveData(result.Master);
        SetGetDataDetails(result.Details);
    }).catch((err) => {
        console.log(err);
    });
}

function SetSaveData(result) {
    $('#hd').val(result.Id);
    $('#txtDate').val(result.Date);
    $('#txtPersonCompany').val(result.PersonCompany);
    $('#txtAddress1').val(result.Address1);
    $('#txtAddress2').val(result.Address2);
    $('#txtCity').val(result.City);
    $('#ddlState').val(result.State).trigger('change');
    $('#txtZipCode').val(result.ZipCode);
    $('#txtContactName').val(result.ContactName);
    $('#txtPhoneNo').val(result.PhoneNo);
    $('#txtEmailAddress').val(result.EmailAddress);
    
    if (result.HaveW9Information == true) {
        $("#chkHaveW9Information").closest('div').addClass('checked');
        $("#chkHaveW9Information").attr('checked', true);
    }
    
}

function SetGetDataDetails(result) {
    $("#tbl tbody").empty();
    let content = '';
    $(result).each(function (index, element) {
        content += getRow(element,index);
    });
    $("#tbl tbody").append(content);
    //$('.ddl').select2();
    $(".tDate").datepicker({
        dateFormat: "mm-dd-yy",
        changeYear: true,
        changeMonth: true
    });

}

function setDropDown(data) {
    var content = '<option value="-1">Select.......</option>';
    $.each(data, function (i, obj) {
        content += '<option data_type="' + obj.Id2 + '" value="' + obj.Id + '">' + obj.Data + '</option>';
    });
    return content;
}

function addRow() {
    let content = '';
    let element = {
        "Id": 0,
        "RecordABillId": 0,
        "DueDate": '',
        "BillId": "",
        "Description": "",
        "Type": -1,
        "CreditAccountName": "",
        "DebitAccountName": "",
        "CreditLedgerCode": "",
        "DebitLedgerCode": "",
        "Amount": "",
        "LstCharts": null
    };
    let rowcount = $("#tbl tbody tr").length;
    content = getRow(element,rowcount);
    $("#tbl tbody").append(content);
    //$.each($("#tbl tbody tr"),function(index, element) {
        
    //});
    //$('.ddl').select2({});
    $(".tDate").datepicker({
        dateFormat: "mm-dd-yy",
        changeYear: true,
        changeMonth: true
    });
}

function getRow(element, rowcount) {
    var content = '<option value="-1">Select.......</option>';
    $.each(global_Type, function (i, obj) {
        if (obj.Id2 == element.Type) {
            content += '<option data_type="' + obj.Id2 + '" value="' + obj.Id + '" selected>' + obj.Data + '</option>';
        } else {
            content += '<option data_type="' + obj.Id2 + '" value="' + obj.Id + '">' + obj.Data + '</option>';
        }
    });
    var ddlCreditAccountName = '<option value="-1">Select.......</option>';
    var ddlDebitAccountName = '<option value="-1">Select.......</option>';
    
    if (element.LstCharts != null) {
        $.each(element.LstCharts, function (i, obj) {
            if (obj.id == element.CreditAccountName) {
                ddlCreditAccountName += '<option data_type="' + obj.accountCode + '" value="' + obj.id + '" selected>' + obj.accountName + '</option>';
            } else {
                ddlCreditAccountName += '<option data_type="' + obj.accountCode + '" value="' + obj.id + '">' + obj.accountName + '</option>';
            }
            if (obj.id == element.DebitAccountName) {
                ddlDebitAccountName += '<option data_type="' + obj.accountCode + '" value="' + obj.id + '" selected>' + obj.accountName + '</option>';
            } else {
                ddlDebitAccountName += '<option data_type="' + obj.accountCode + '" value="' + obj.id + '">' + obj.accountName + '</option>';
            }
        });
    }
    return '<tr>' +
        '<td style="width:12%; text-align:center;padding:5px;">' +
        '<button style="margin-right: 2px;padding: 0.5rem 0.676rem;height: 34px; background-color:#66FF00;" type="button" class="btn btnAdd" data-ID=' + element.Id + '>Add</button>' +
        '<button style="padding: 0.5rem 0.676rem;height: 34px;background-color: #3B5998"  type="button" class="btn btnDelete" data-ID=' + element.Id + '>Remove</button>' +
        '</td>' +
        '<td><input type="text" class="form-control tDate DueDate" id="DueDate'+rowcount+'" value="' + element.DueDate + '"/></td>' +
        '<td><input type="text" class="form-control BillId" id="BillId' + rowcount + '" value="' + element.BillId + '"/></td>' +
        '<td><input type="text" class="form-control Description" id="BillId' + rowcount + '"   value="' + element.Description + '"/></td>' +
        '<td><select class="ddl Type form-control" id="Type' + rowcount + '">' + content + '</select></td>' +
        '<td><select class="ddl AccountName CreditAccountName form-control" id="CreditAccountName' + rowcount + '" >' + ddlCreditAccountName + '</select></td>' +
        '<td class="CreditLedgerCode">' + element.CreditLedgerCode + '</td>' +
        '<td><select class="ddl AccountName DebitAccountName form-control" id="DebitAccountName' + rowcount + '" >' + ddlDebitAccountName + '</select></td>' +
        '<td class="DebitLedgerCode">' + element.DebitLedgerCode+ '</td>' +
        '<td><input type="text" class="form-control Amount qty" id="Amount' + rowcount + '"  value="' + element.Amount + '"></td>' +
        '</tr>';
}

$(document).off("change", ".Type").on('change', '.Type', function () {
    let tblRow = $(this).closest('tr');
    let type = $(this).find('option:selected').attr('data_type');
    // ......... Account Name ............... //
    var URLAccountName = currentPagePath + "GetAccountName";
    var obj = {
        "Type": type
    };
    let AccountType = makeAjaxCallReturnPromiss(URLAccountName, obj);
    AccountType.then((data) => {
        $(tblRow).find('.CreditAccountName').get(0).options.length = 0;
        $(tblRow).find('.DebitAccountName').get(0).options.length = 0;
        //$(tblRow).find('.AccountName').empty();
        let result = $.parseJSON(decodeURIComponent(data.d));
        var debit = $.grep(result, function (v) {
            return v.Id3 === type;
        });
        var credit = $.grep(result, function (v) {
            return v.Id3 === "Asset" || v.Id3 === "Lib";
        });
        let resultdebit = setDropDown(debit);
        let resultcredit = setDropDown(credit);
        if (type == "Inc") {
            $(tblRow).find('.DebitAccountName').append(resultcredit);
            $(tblRow).find('.CreditAccountName').append(resultdebit);
        } else {
            $(tblRow).find('.CreditAccountName').append(resultcredit);
            $(tblRow).find('.DebitAccountName').append(resultdebit);
        }
        

    }).catch((err) => {
        console.log(err);
    });
});

$(document).off("change", ".CreditAccountName").on('change', '.CreditAccountName', function () {
    let tblRow = $(this).closest('tr');
    let ledgerCode = $(this).find('option:selected').attr('data_type');
    $(tblRow).find('.CreditLedgerCode').text(ledgerCode);
});

$(document).off("change", ".DebitAccountName").on('change', '.DebitAccountName', function () {
    let tblRow = $(this).closest('tr');
    let ledgerCode = $(this).find('option:selected').attr('data_type');
    $(tblRow).find('.DebitLedgerCode').text(ledgerCode);
});

$(document).off("click", ".btnAdd").on('click', '.btnAdd', function () {
    let tblRow = $(this).closest('tr');
    if (AddValidation(tblRow) == true) {
        addRow();
    }
});

function AddValidation(tblRow) {
    var isresult = true;
    let DueDate = $(tblRow).find('.DueDate').val();
    let BillId = $(tblRow).find('.BillId').val();
    //let Description = $(tblRow).find('.Description').val();
    let Type = $(tblRow).find('.Type option:selected').val();
    let CreditAccountName = $(tblRow).find('.CreditAccountName option:selected').val();
    let DebitAccountName = $(tblRow).find('.DebitAccountName option:selected').val();
    let Amount = $(tblRow).find('.Amount').val();
    if (DueDate === "undefined" || DueDate === "") {
        $(tblRow).find('.DueDate').css({ 'border': '1px solid red' });
        isresult = false;
    }
    else {
        $(tblRow).find('.DueDate').css({ 'border': '1px solid #d2d6de' });
    }
    if (BillId === "undefined" || BillId === "") {
        $(tblRow).find('.BillId').css({ 'border': '1px solid red' });
        isresult = false;
    }
    else {
        $(tblRow).find('.BillId').css({ 'border': '1px solid #d2d6de' });
    }
    if (Amount === "undefined" || Amount === "") {
        $(tblRow).find('.Amount').css({ 'border': '1px solid red' });
        isresult = false;
    }
    else {
        $(tblRow).find('.Amount').css({ 'border': '1px solid #d2d6de' });
    }
    if (Type === "undefined" || Type === "-1") {
        $(tblRow).find('.Type').css({ 'border': '1px solid red', 'border-radius': '5px' });
        isresult = false;
    }
    else {
        $(tblRow).find('.Type').css({ 'border': '1px solid #d2d6de' });
    }
    if (CreditAccountName === "undefined" || CreditAccountName === "-1") {
        $(tblRow).find('.CreditAccountName').css({ 'border': '1px solid red', 'border-radius': '5px' });
        isresult = false;
    }
    else {
        $(tblRow).find('.CreditAccountName').css({ 'border': '1px solid #d2d6de' });
    }
    if (DebitAccountName === "undefined" || DebitAccountName === "-1") {
        $(tblRow).find('.DebitAccountName').css({ 'border': '1px solid red', 'border-radius': '5px' });
        isresult = false;
    }
    else {
        $(tblRow).find('.DebitAccountName').css({ 'border': '1px solid #d2d6de' });
    }
    if (isresult)
        isresult = true;
    else {
        isresult = false;
    }
    return isresult;
}

$(document).off("click", ".btnDelete").on('click', '.btnDelete', function () {
    let tblRow = $(this).closest('tr');
    let id = $(this).attr('data-id');
    if (id > 0) {
        var URL = currentPagePath + "DeleteData";
        var obj = {
            "Id": id
        };
        let DeleteData = makeAjaxCallReturnPromiss(URL, obj);
        DeleteData.then((data) => {
            let result = $.parseJSON(decodeURIComponent(data.d));
            if (result == true) {
                $(tblRow).remove();
                let rowCount = $("#tbl tbody tr").length;
                if (rowCount == 0) {
                    addRow();
                }
            }
        }).catch((err) => {
            console.log(err);
        });
    }
});

$(document).off('click', '#btnSave').on('click', '#btnSave', function (parameters) {
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
                        GetAutoGenNumber();
                        notify('success', "Save Successfully");
                    } else {
                        notify('danger', "Save Failed !!");
                    }
                }
        });
    }
});

function SaveValidation() {
    var isresult = true;
    var txtDate = $('#txtDate').val();
    var txtPersonCompany = $('#txtPersonCompany').val();
    var txtAddress1 = $('#txtAddress1').val();
    var txtCity = $('#txtCity').val();
    var txtZipCode = $('#txtZipCode').val();
    var txtContactName = $('#txtContactName').val();
    var txtEmailAddress = $('#txtEmailAddress').val();
    var txtPhoneNo = $('#txtPhoneNo').val();
    var ddlState = $('#ddlState option:selected').val();

    if (txtDate === "undefined" || txtDate === "") {
        $('#txtDate').css({ 'border': '1px solid red' });
        isresult = false;
    }
    else {
        $('#txtDate').css({ 'border': '1px solid #d2d6de' });
    }
    if (txtPersonCompany === "undefined" || txtPersonCompany === "") {
        $('#txtPersonCompany').css({ 'border': '1px solid red' });
        isresult = false;
    }
    else {
        $('#txtPersonCompany').css({ 'border': '1px solid #d2d6de' });
    }

    if (txtAddress1 === "undefined" || txtAddress1 === "") {
        $('#txtAddress1').css({ 'border': '1px solid red' });
        isresult = false;
    }
    else {
        $('#txtAddress1').css({ 'border': '1px solid #d2d6de' });
    }
    if (txtCity === "undefined" || txtCity === "") {
        $('#txtCity').css({ 'border': '1px solid red' });
        isresult = false;
    }
    else {
        $('#txtCity').css({ 'border': '1px solid #d2d6de' });
    }
    if (txtZipCode === "undefined" || txtZipCode === "") {
        $('#txtZipCode').css({ 'border': '1px solid red' });
        isresult = false;
    }
    else {
        $('#txtZipCode').css({ 'border': '1px solid #d2d6de' });
    }
    if (txtContactName === "undefined" || txtContactName === "") {
        $('#txtContactName').css({ 'border': '1px solid red' });
        isresult = false;
    }
    else {
        $('#txtContactName').css({ 'border': '1px solid #d2d6de' });
    }
    if (txtEmailAddress === "undefined" || txtEmailAddress === "") {
        $('#txtEmailAddress').css({ 'border': '1px solid red' });
        isresult = false;
    }
    else {
        $('#txtEmailAddress').css({ 'border': '1px solid #d2d6de' });
    }
    if (txtPhoneNo === "undefined" || txtPhoneNo === "") {
        $('#txtPhoneNo').css({ 'border': '1px solid red' });
        isresult = false;
    }
    else {
        $('#txtPhoneNo').css({ 'border': '1px solid #d2d6de' });
    }
    
    if (ddlState === "undefined" || ddlState === "-1") {
        $("#s2id_ddlState").css({ 'border': '1px solid red', 'border-radius': '5px' });
        isresult = false;
    }
    else {
        $("#s2id_ddlState").css({ 'border': '1px solid Transparent' });
    }

    $('#tbl tbody tr').each(function (index, element) {
        if (AddValidation(element) == false) {
            isresult = false;
        }
    });
    if (isresult)
        isresult = true;
    else {
        isresult = false;
    }
    return isresult;
}

function GetObject() {
    let obj = {
        "Id": $('#hd').val(),
        "Date": $('#txtDate').val(),
        "BillNumber": $('#txtBillNumber').val(),
        "PersonCompany": $('#txtPersonCompany').val(),
        "Address1": $('#txtAddress1').val(),
        "Address2": $('#txtAddress2').val(),
        "City": $('#txtCity').val(),
        "PhoneNo": $('#txtPhoneNo').val(),
        "ZipCode": $('#txtZipCode').val(),
        "ContactName": $('#txtContactName').val(),
        "EmailAddress": $('#txtEmailAddress').val(),
        "State": $('#ddlState option:selected').val(),
        "HaveW9Information": $('#chkHaveW9Information').is(':checked')
    }
    let Details = [];
    $('#tbl tbody tr').each(function (index, element) {
        let DueDate = $(element).find('.DueDate').val();
        let BillId = $(element).find('.BillId').val();
        let Description = $(element).find('.Description').val();
        let Type = $(element).find('.Type option:selected').attr('data_type');
        let CreditAccountName = $(element).find('.CreditAccountName option:selected').val();
        let DebitAccountName = $(element).find('.DebitAccountName option:selected').val();
        let Amount = $(element).find('.Amount').val();
        let id = $(element).find('.btnDelete').attr('data-ID');
        let data = {
            "Id": id,
            "DueDate": DueDate,
            "BillId": BillId,
            "Description": Description,
            "Type": Type,
            "CreditAccountName": CreditAccountName,
            "DebitAccountName": DebitAccountName,
            "CreditLedgerCode": $(element).find(".CreditLedgerCode").text(),
            "DebitLedgerCode": $(element).find(".DebitLedgerCode").text(),
            "Amount": Amount
        };
        Details.push(data);
    });
    let saveObj = {
        "Master": obj,
        "Details": Details
    };
    return saveObj;
}

$(document).off('click', '#btnCancel').on('click', '#btnCancel', function (parameters) {
    var origin = window.location.origin; // Returns base URL
    var url = origin + "/Pages/DashboardAdmin.aspx";
    window.location.href = url;
});

function ClearData() {
    $("#tbl tbody").empty();
    $('#ddlState').val('-1').trigger('change');
    $("#txtDate").datepicker().datepicker("setDate", new Date());
    $('#txtBillNumber').val('');
    $('#hd').val('0');
    $('#txtPersonCompany').val('');
    $('#txtAddress1').val('');
    $('#txtAddress2').val('');
    $('#txtCity').val('');
    $('#txtPhoneNo').val('');
    $('#txtZipCode').val('');
    $('#txtContactName').val('');
    $('#txtEmailAddress').val('');
    $("#chkHaveW9Information").closest('div').removeClass('checked');
    $("#chkHaveW9Information").attr('checked', false);
    $('#ddlBillNumber').val('-1').trigger('change');

    addRow();
}

$(document).off("keyup", ".qty").on('keyup', '.qty', function () {
    let value = $(this).val();
    var Reg = /^-?\d*[.,]?\d*$/;//    /[a-zA-Z0-9]/i;// /^\s*[a-zA-Z0-9]+\s*$/;
    if (!Reg.test(value)) {
        $(this).val('');
    }
});

function isEmail(email) {
    var regex = /^([a-zA-Z0-9_.+-])+\@(([a-zA-Z0-9-])+\.)+([a-zA-Z0-9]{2,4})+$/;
    return regex.test(email);
}
$(document).on('keyup', '#txtEmailAddress', function () {
    let filter = $(this).val();
    if (isEmail(filter)) {
        $("#txtEmailAddress").css({ "border": "1px solid Green" });
        return true;
    }
    else {
        $("#txtEmailAddress").css({ "border": "1px solid red" });
        return false;
    }
});