/*="D:\PROJECT\EProperty\src\eProperty\Pages/ReportPage/CommonReportViewer.aspx" />*/
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
    // ......... Report Name Load ............... //
    var URL = currentPagePath + "GetReportList";
    var obj = {};
    let ReportName = makeAjaxCallReturnPromiss(URL, obj);
    ReportName.then((data) => {
        let ReportNameData = setCombo($.parseJSON(decodeURIComponent(data.d)), '-1');
        $("#ddlReportName option").empty();
        $("#ddlReportName").append(ReportNameData);
        $("#ddlReportName").select2();
    }).catch((err) => {
        console.log(err);
    });
    Promise.all([ReportName]).then(function () {
    });
}

function getReportObject() {
    var obj = {
        "ReportId": $('#ddlReportName option:selected').attr('data_id'),
        "ReportName": $('#ddlReportName  option:selected').text()
    };
    return obj;
}

$(document).off('click', '#btnShowReport').on('click', '#btnShowReport', function () {
    let obj = getReportObject();
    var URL = currentPagePath + "SetReportSessionData";
    debugger;
    let ReportSession = makeAjaxCallReturnPromiss(URL, obj);
    ReportSession.then((data) => {
        debugger;
        let result = $.parseJSON(decodeURIComponent(data.d));
        window.open(result, '_blank');
    }).catch((err) => {
        console.log(err);
    });
    Promise.all([ReportSession]).then(function () {
    });

       
});