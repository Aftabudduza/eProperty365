var currentPagePath = window.location.pathname + "/";
$(document).ready(function () {
    InitialLoad();
    $(".ddl").select2();
    $('input[type="checkbox"].flat-red, input[type="radio"].flat-red').iCheck({
        checkboxClass: 'icheckbox_flat-green',
        radioClass: 'iradio_flat-green'
    });
});
function InitialLoad() {
    // ......... Account Type ............... //
    var URL = currentPagePath + "GetAccountType";
    var obj = {};
    let AccountType = makeAjaxCallReturnPromiss(URL, obj);
    AccountType.then((data) => {
        console.log("Account Type Data Loaded");
        let AccountTypeData = setDropDown($.parseJSON(decodeURIComponent(data.d)), '-1');
        $("#ddlAccountType option").empty();
        $("#ddlAccountType").append(AccountTypeData);
        $("#ddlAccountType").select2();
    }).catch((err) => {
        console.log(err);
    });
    Promise.all([AccountType]).then(function () {
        getAccountChartData();
    });
}

function getAccountChartData() {
    // ......... Account Chart ............... //
    var URL = currentPagePath + "GetAccountChart";
    var obj = {};
    let AccountChart = makeAjaxCallReturnPromiss(URL, obj);
    AccountChart.then((data) => {
        console.log("Account Chart Data Loaded");
        setTableData($.parseJSON(decodeURIComponent(data.d)));
    }).catch((err) => {
        console.log(err);
    });
    Promise.all([AccountChart]).then(function () {
    });
}
function setDropDown(data) {
    var content = '<option value="-1">Select.......</option>';
    $.each(data, function (i, obj) {
        content += '<option data_type="' + obj.Id2 + '" value="' + obj.Id + '">' + obj.Data + '</option>';
    });
    return content;
}
function setTableData(result) {
    var content = "";
    $.each(result,
        function (i, row) {
            let button = '';
            if (row.EditAble == true) {
                button = '<input type="button" class="btn btnEdit" style="background-color: #3B5998" value="Edit" >';
            }
            content += '<tr>' +
                '<td>' + (i + 1) + '<input type="hidden" class="Id" value="' + row.Id + '"/>' + '</td>' +
                '<td class="AccountCode">' + row.AccountCode + '</td>' +
                '<td class="AccountName">' + row.AccountName + '</td>' +
                '<td><input type="hidden" class="AccountTypeId" value="' + row.AccountTypeId + '"/>' + row.AccountType + '</td>' +
                '<td>' + row.AccountDescription + '</td>' +
                '<td class="IsActive">' + row.IsActive + '</td>' +
                '<td>' + row.CreateDate + '</td>' +
                '<td>' + button + '</td>' +
                ' </tr>';
        });
    $('#tbl tbody').empty();
    $('#tbl tbody').append(content);
}

$(document).off("click", ".btnEdit").on("click", ".btnEdit", function () {
    let tblRow = $(this).closest('tr');
    let id = $(tblRow).find('.Id').val();
    let accountCode = $(tblRow).find('.AccountCode').text();
    let accountName = $(tblRow).find('.AccountName').text();
    let AccountTypeId = $(tblRow).find('.AccountTypeId').val();
    let isActive = $(tblRow).find('.IsActive').text();

    $('#ddlAccountType').val(AccountTypeId).trigger('change');
    $('#txtAccountCode').val(accountCode);
    $('#txtAccountName').val(accountName);
    if (isActive == "true") {
        $("#chkIsActive").parent().addClass('checked');
        $('#chkIsActive').prop('checked', true);
    } else {
        $('#chkIsActive').parent().removeClass('checked');
        $('#chkIsActive').prop('checked', false);
    }

    $('#btnSave').attr('data-id', id);
    $('#btnSave').val('Update');

    window.scrollTo(0, 0);
});

$(document).on('keyup', '#txtAccountCode', function () {
    if ($.isNumeric($(this).val())) {
        return true;
    } else {
        $(this).val('');
        notify('danger', "Only Numeric");
    }
});
$(document).off('click', '#btnSave').on('click', '#btnSave', function () {
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
                        getAccountChartData();
                        let id = $('#btnSave').attr('data-id');
                        if (id > 0) {
                            notify('success', "Update successfully");
                        } else {
                            notify('success', "Saved successfully");
                        }
                        $('#btnSave').attr('data-id', "0")
                        $('#btnSave').val('Save');

                    } else {
                        notify('danger', "Save Failed !!");
                    }
                }
        });
    }
});

function SaveValidation() {
    var isresult = true;
    var ddlAccountType = $('#ddlAccountType option:selected').val();
    var txtAccountCode = $('#txtAccountCode').val();
    var txtAccountName = $('#txtAccountName').val();
    if (txtAccountCode === "undefined" || txtAccountCode === "") {
        $("#txtAccountCode").css({ 'border': '1px solid red' });
        isresult = false;
    }
    else {
        $("#txtAccountCode").css({ 'border': '1px solid #d2d6de' });
    }
    if (txtAccountName === "undefined" || txtAccountName === "") {
        $("#txtAccountName").css({ 'border': '1px solid red' });
        isresult = false;
    }
    else {
        $("#txtAccountName").css({ 'border': '1px solid #d2d6de' });
    }

    if (ddlAccountType === "undefined" || ddlAccountType === "-1") {
        $("#s2id_ddlAccountType").css({ 'border': '1px solid red', 'border-radius': '5px' });
        isresult = false;
    }
    else {
        $("#s2id_ddlAccountType").css({ 'border': '1px solid Transparent' });
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
        "Id": $('#btnSave').attr('data-id'),
        "isActive": $("#chkIsActive").is(':checked'),
        "accountCode": $('#txtAccountCode').val(),
        "accountName": $('#txtAccountName').val(),
        "accountTypeId": $('#ddlAccountType option:selected').attr('data_type')
    }
    return obj;
}
$(document).off('click', '#btnCancel').on('click', '#btnCancel', function (parameters) {
    var origin = window.location.origin; // Returns base URL
    var url = origin + "/Pages/DashboardOwner.aspx";
    window.location.href = url;
});
function ClearData() {
    $('#ddlAccountType').val('-1').trigger('change:select2');
    $("#ddlAccountType").select2();
    $('#txtAccountCode').val('');
    $('#txtAccountName').val('');
    $("#chkIsActive").closest('div').addClass('checked');
    $("#chkIsActive").attr('checked', true);

    //$('input[name=Active]').closest('div').removeClass('checked');
    //$('input[name=Active]').attr('checked', false);
    //$("input[name=Active][value='trueFalse']").closest('div').addClass('checked');
    //$("input[name=Active][value='trueFalse']").attr('checked', true);

}