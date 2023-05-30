var currentPagePath = window.location.pathname + "/";
$(document).ready(function () {
    $(".tDate").datepicker({
        dateFormat: "mm/dd/yy",
        changeYear: true,
        changeMonth: true
    });
    // ......... Account Type ............... //
    var URLAccountType = "/Pages/Account/RecordABill.aspx/" + "GetAccountType";
    let obj = {};
    let AccountType = makeAjaxCallReturnPromiss(URLAccountType, obj);
    AccountType.then((data) => {
        let result = setCombo($.parseJSON(decodeURIComponent(data.d)), '-1');
        $('#ddlType').get(0).options.length = 0;
        //$("#ddlStateSalesPartner option").empty();
        $("#ddlType").append(result);
        $("#ddlType").select2({
            placeholder: "Select a Type",
            allowClear: true
        });

    }).catch((err) => {
        console.log(err);
    });
   
});
$("#txtFromDate").datepicker({
    onSelect: function (dateText, inst) {
        $("#txtToDate").datepicker('option', 'minDate', new Date(dateText));
    }
});

$(document).off("click", "#btnGo").on("click", "#btnGo", function () {
    $('#tbl tbody').empty();
    var URL = currentPagePath + "GetRecordABill";
    var obj = {
        "fromDate": $("#txtFromDate").val(),
        "toDate": $("#txtToDate").val()
    };
    let RecordABill = makeAjaxCallReturnPromiss(URL, obj);
    RecordABill.then((data) => {
        setTableData($.parseJSON(decodeURIComponent(data.d)));
    }).catch((err) => {
        console.log(err);
    });
    Promise.all([RecordABill]).then(function () {
    });
});

function setTableData(result) {
    var content = "";
    $.each(result,
        function (i, row) {
            content += '<tr>' +
                '<td><input type="checkBox" class="Id" value="' + row.Id + '"/></td>' +
                '<td class="Date">' + row.Date + '</td>' +
                '<td class="BillNumber">' + row.BillNumber + '</td>' +
                '<td class="ContactName">' + row.ContactName + '</td>' +
                '<td class="PersonCompany">' + row.PersonCompany + '</td>' +
                '<td class="Address1">' + row.Address1 + '</td>' +
                '<td class="TotalAmount">' + row.TotalAmount + '</td>' +
                ' </tr>';
        });
    
    $('#tbl tbody').append(content);
}

$(document).off("click", "#btnSelectAItem").on("click", "#btnSelectAItem", function () {
    $('#tblDescription tbody').empty();
    let Details = [];
    $('#tbl tbody tr').each(function (index, element) {
        let check = $(element).find('.Id');
        if (check.is(':checked')) {
            let data = {
                "RecordABillId": $(element).find('.Id').val()
            };
            Details.push(data);
        }
        
    });
    let saveObj = {
        "Details": Details
    };
    var URL = currentPagePath + "GetRecordABillDetails";
   
    let RecordABillDetails = makeAjaxCallReturnPromiss(URL, saveObj);
    RecordABillDetails.then((data) => {
        setTableDescriptionData($.parseJSON(decodeURIComponent(data.d)));
    }).catch((err) => {
        console.log(err);
    });
    Promise.all([RecordABillDetails]).then(function () {
    });
});

function setTableDescriptionData(result) {
    var content = "";
    $.each(result,
        function (i, row) {
            content += '<tr>' +
                '<td class="DueDate"><input type="hidden" class="Id" value="' + row.Id + '"/>' + row.DueDate + '</td>' +
                '<td class="BillId">' + row.BillId + '</td>' +
                '<td class="Description">' + row.Description + '</td>' +
                '<td class="CreditAccountName">' + row.CreditAccountName + '</td>' +
                '<td class="DebitAccountName">' + row.DebitAccountName + '</td>' +
                '<td class="Amount">' + row.Amount + '</td>' +
                ' </tr>';
        });

    $('#tblDescription tbody').append(content);
}

$(document).off("click", "#btnPrintCheck").on("click", "#btnPrintCheck", function () {
    if (SaveValidation() == true) {
        let Details = [];
        $('#tblDescription tbody tr').each(function (index, element) {
            let data = {
                "RecordABillDetailsId": $(element).find('.Id').val(),
                "Type": $('#ddlType option:selected').val(),
                "CheckNumber": $('#txtCheckNumber').val()
            };
            Details.push(data);
        });
        let saveObj = {
            "lstSaveBillPayments": Details
        };
        var URL = currentPagePath + "PrintCheck";

        let PrintCheck = makeAjaxCallReturnPromiss(URL, saveObj);
        PrintCheck.then((data) => {
            let result = $.parseJSON(decodeURIComponent(data.d));
            if (result == true) {
                ClearData();
                notify('success', "Print Check Successfully");
            } else {
                notify('danger', "Print Check Failed !!");
            }
        }).catch((err) => {
            console.log(err);
        });
        Promise.all([PrintCheck]).then(function () {
        });
    }
    
});
function SaveValidation() {
    var isresult = true;
    var txtCheckNumber = $('#txtCheckNumber').val();
    var txtFromDate = $('#txtFromDate').val();
    var txtToDate = $('#txtToDate').val();
    
    var ddlType = $('#ddlType option:selected').val();
    
    if (txtFromDate === "undefined" || txtFromDate === "") {
        $('#txtFromDate').css({ 'border': '1px solid red' });
        isresult = false;
    }
    else {
        $('#txtFromDate').css({ 'border': '1px solid #d2d6de' });
    }
    if (txtToDate === "undefined" || txtToDate === "") {
        $('#txtToDate').css({ 'border': '1px solid red' });
        isresult = false;
    }
    else {
        $('#txtToDate').css({ 'border': '1px solid #d2d6de' });
    }
    if (txtCheckNumber === "undefined" || txtCheckNumber === "") {
        $('#txtCheckNumber').css({ 'border': '1px solid red' });
        isresult = false;
    }
    else {
        $('#txtCheckNumber').css({ 'border': '1px solid #d2d6de' });
    }
    
    if (ddlType === "undefined" || ddlType === "-1") {
        $("#s2id_ddlType").css({ 'border': '1px solid red', 'border-radius': '5px' });
        isresult = false;
    }
    else {
        $("#s2id_ddlType").css({ 'border': '1px solid Transparent' });
    }
    let length = $('#tblDescription tbody tr').length;
    if (length > 0) {

    } else {
        isresult = false;
    }
    
    if (isresult)
        isresult = true;
    else {
        isresult = false;
    }
    return isresult;
}
function ClearData() {
    $("#tbl tbody").empty();
    $("#tblDescription tbody").empty();
    $('#ddlType').val('-1').trigger('change');
    $('.tDate').val('');
    $('#txtCheckNumber').val('');
}