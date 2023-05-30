var currentPagePath = "/Pages/ReportPage/BasicReport.aspx" + "/";
$(document).ready(function (parameters) {
    $("#txFromDate").datepicker({
        dateFormat: "mm-dd-yy",
        changeYear: true,
        changeMonth: true
    });
});
$(document).on('click', '#btnShowReport', function (parameters) {
    if ($("#txFromDate").val() != "") {
        var obj = {
            "ReportId": 0,
            "ReportName": "Balance Sheet",
            "createdate": $("#txFromDate").val()
        };
        // "/Pages/ReportPage/BasicReport.aspx/SetReportSessionData"
        var URL = currentPagePath + "SetReportSessionData";
        let ReportSession = makeAjaxCallReturnPromiss(URL, obj);
        ReportSession.then((data) => {
           
            let result = $.parseJSON(decodeURIComponent(data.d));
            window.open(result, '_blank');
        }).catch((err) => {
            console.log(err);
        });
        Promise.all([ReportSession]).then(function() {
        });
    } else {
        notify('danger', "Please Select Date");
    }
   
});