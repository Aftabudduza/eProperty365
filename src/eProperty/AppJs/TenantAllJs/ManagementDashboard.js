var currentPagePath = window.location.pathname + "/";
var pathname = window.location.pathname; // Returns path only
var url = window.location.href;     // Returns full URL
var origin = window.location.origin;   // Returns base URL
var G_RelationalData = [];

$(document).ready(function (parameters) {
    InitialLoad();
    $(".ddl").select2();
    $('input[type="checkbox"].flat-red, input[type="radio"].flat-red').iCheck({
        checkboxClass: 'icheckbox_flat-green',
        radioClass: 'iradio_flat-green'
    });
});
function setComboData(data, id) {
    $(id).get(0).options.length = 0;
    var content = '<option value="-1">Select.......</option>';
    if (data == undefined || data.length == 0 || data == null) {
        return content;
    }
    else {
        $.each(data,
            function (i, obj) {
                content += '<option data_SelectedField="' + obj.SelectedField + '" data_Id="' + obj.Id3 + '" value="' + obj.Id2 + '">' + obj.Data + '</option>';
            });
    }
    $(id).append(content);
    $(id).select2();
}
function InitialLoad() {
    var URL = currentPagePath + "GetInitialData";
    var obj = {};
    let Basic = makeAjaxCallReturnPromiss(URL, obj);
    Basic.then((data) => {
        if (data.d != null || data.d != "") {
            var result = $.parseJSON(decodeURIComponent(data.d));
            $('#txtOwnerId').val(result[0][0].Data);
            $('#txtOwnerId').attr('data-value', result[0][0].Id2);
            setComboData(result[1], $('#ddlPropertyManagerId'));
            setComboData(result[2], $('#ddlLocation'));
            setComboData(result[3], $('#ddlUnitID'));
        }
    });
    Promise.all([Basic]).then(function () {
    });
}

$('#ddlLocation').change(function () {
    $('#ddlUnitID').val("-1").trigger('change');
    var URL = currentPagePath + "GetUnitData";
    var obj = {
        "OwnerId": $('#txtOwnerId').attr('data-value'),
        "PropertyManagerId": $('#ddlPropertyManagerId option:selected').val(),
        "LocationId": $('#ddlLocation option:selected').val()
    };
    let Basic = makeAjaxCallReturnPromiss(URL, obj);
    Basic.then((data) => {
        if (data.d != null || data.d != "") {
            var result = $.parseJSON(decodeURIComponent(data.d));
            setComboData(result[0], $('#ddlUnitID'));
        }
    });
    Promise.all([Basic]).then(function () {
    });
});
$('#ddlPropertyManagerId').change(function () {
    $('#ddlUnitID').val("-1").trigger('change');
    var URL = currentPagePath + "GetLocationUnitData";
    var obj = {
        "OwnerId":$('#txtOwnerId').attr('data-value'),
        "PropertyManagerId": $('#ddlPropertyManagerId option:selected').val()
    };
    let Basic = makeAjaxCallReturnPromiss(URL, obj);
    Basic.then((data) => {
        if (data.d != null || data.d != "") {
            var result = $.parseJSON(decodeURIComponent(data.d));
            setComboData(result[0], $('#ddlLocation'));
            setComboData(result[1], $('#ddlUnitID'));
        }
    });
    Promise.all([Basic]).then(function () {
    });
});
$('#ddlUnitID').change(function () {
    var selected_Value = $(this).val();
    if (selected_Value != "-1") {
        if (UnitValidation() == true) {
            var tenant_Name = $('#ddlUnitID option:selected').attr('data_SelectedField');
            var tenant_Serial = $('#ddlUnitID option:selected').attr('data_Id');
            $('#txtTenantName').val(tenant_Name);
            $('#txtTenantName').attr('data-value', tenant_Serial);

            var URL = currentPagePath + "GetTableDataByUnit";
            var obj = {
                "OwnerId": $('#txtOwnerId').attr('data-value'),
                "PropertyManagerId": $('#ddlPropertyManagerId option:selected').val(),
                "LocationId": $('#ddlLocation option:selected').val(),
                "UnitId": selected_Value
            };
            let Basic = makeAjaxCallReturnPromiss(URL, obj);
            Basic.then((data) => {
                if (data.d != null || data.d != "") {
                    var result = $.parseJSON(decodeURIComponent(data.d));
                    setTableData(result);
                }
            });
            Promise.all([Basic]).then(function() {
            });
        }
    } else {
        $('#txtTenantName').val('');
        $('#txtTenantName').attr('data-value', '');
        $('#tbl tbody').empty();
    }
});

function setTableData(result) {
    var content = '<tr>' +
                '<td class="Owner">' + result.Owner + '</td>' +
                '<td class="ProperlyManager">' + result.ProperlyManager + '</td>' +
                '<td class="PropertyLocation">' + result.PropertyLocation + '</td>' +
                '<td class="Unit">' + result.Unit + '</td>' +
                '<td class="RentalYTD">' + result.RentalYTD + '</td>' +
                '<td class="MonthRental">' + result.MonthRental + '</td>' +
                '<td class="Status">' + result.Status + '</td>' +
                '<td class="OutstandingIssues">' + result.OutstandingIssues + '</td>' +
                '<td class="EmergencyContact">' + result.EmergencyContact + '</td>' +
                '<td class="EmergencyPhone">' + result.EmergencyPhone + '</td>' +
                ' </tr>';
      
    $('#tbl tbody').empty();
    $('#tbl tbody').append(content);
}
function UnitValidation() {
    var isresult = true;
    var ddlPropertyManagerId = $('#ddlPropertyManagerId option:selected').val();
    var ddlLocation = $('#ddlLocation option:selected').val();
    var txtOwnerId = $('#txtOwnerId').attr('data-value');
    if (txtOwnerId === "undefined" || txtOwnerId === "") {
        $("#txtOwnerId").css({ 'border': '1px solid red' });
        isresult = false;
    }
    else {
        $("#txtOwnerId").css({ 'border': '1px solid #d2d6de' });
    }
    if (ddlPropertyManagerId === "undefined" || ddlPropertyManagerId === "-1") {
        $("#s2id_ddlPropertyManagerId").css({ 'border': '1px solid red', 'border-radius': '5px' });
        isresult = false;
    }
    else {
        $("#s2id_ddlPropertyManagerId").css({ 'border': '1px solid Transparent' });
    }
    if (ddlLocation === "undefined" || ddlLocation === "-1") {
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
$(document).on('click', '#btnExit', function (parameters) {
    var origin = window.location.origin; // Returns base URL
    var url = origin + "/Pages/Admin/AddResidentialUnit.aspx";
    window.location.href = url;
});