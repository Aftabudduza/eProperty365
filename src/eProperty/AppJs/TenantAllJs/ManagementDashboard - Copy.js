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
    //var calendar = new Calendar('#calendar', {
    //    defaultView: 'month',
    //    taskView: true,
    //    template: {
    //        monthGridHeader: function (model) {
    //            var date = new Date(model.date);
    //            var template = '<span class="tui-full-calendar-weekday-grid-date">' + date.getDate() + '</span>';
    //            return template;
    //        }
    //    }
    //});
});
function setComboData(data, id) {
    $(id).get(0).options.length = 0;
    var content = '<option value="-1">Select.......</option>';
    if (data == undefined || data.length == 0 || data == null) {
        return content;
    }
    else {
        $.each(data, function (i, obj) {
            if (obj.SelectedField == obj.Id2) {
                    content += '<option data_Id="' + obj.Id3 + '" value="' + obj.Id2 + '" selected>' + obj.Data + '</option>';
                } else {
                    content += '<option data_Id="' + obj.Id3 + '" value="' + obj.Id2 + '">' + obj.Data + '</option>';
                }
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
            Promise.all([setComboData(result[0], $('#ddlOwnerId'))]).then(function () {
                setComboData(result[1], $('#ddlPropertyManagerId'));
                $('#ddlOwnerId').val($('#ddlOwnerId option:selected').val()).trigger('change');

            });
        }
    });
}

$('#ddlOwnerId').change(function() {
    let selectedValue = $(this).val();
    if (selectedValue != "-1") {
        $('#ddlPropertyManagerId').val($('#ddlOwnerId option:selected').attr('data_Id')).trigger('change');
        LoadLocationUnit(selectedValue);
    } else {
        $('#ddlPropertyManagerId').val('-1').trigger('change');
    }
});
$('#ddlUnitID').change(function () {
    
    let selectedValue = $(this).val();
    if (selectedValue != "-1") {
        $('#txtTenantName').text($('#ddlOwnerId option:selected').attr('data_Id'));
        
        //LoadLocationUnit(selectedValue);
    } else {
        $('#txtTenantName').text('');
    }
});

function LoadLocationUnit(ownerId) {
    var obj = {
        "OwnerId": ownerId
    };
    var URLLocation = currentPagePath + "GetLocationData";
    let LocationData = makeAjaxCallReturnPromiss(URLLocation, obj);
    LocationData.then((data) => {
        if (data.d != null || data.d != "") {
            var result = $.parseJSON(decodeURIComponent(data.d));
            setComboData(result[0], $('#ddlLocation'));
        }
    });
    var URLUnit = currentPagePath + "GetUnitData";
    let UnitData = makeAjaxCallReturnPromiss(URLUnit, obj);
    UnitData.then((data) => {
        if (data.d != null || data.d != "") {
            var result = $.parseJSON(decodeURIComponent(data.d));
            setComboData(result[0], $('#ddlUnitID'));
        }
    });
    Promise.all([LocationData], [UnitData]).then(function () {
    });
}
