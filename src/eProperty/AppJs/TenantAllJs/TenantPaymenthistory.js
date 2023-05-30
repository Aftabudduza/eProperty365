var currentPagePath = window.location.pathname + "/";

var pathname = window.location.pathname; // Returns path only
var url = window.location.href;     // Returns full URL
var origin = window.location.origin;   // Returns base URL
var G_RelationalData = [];
$(document).ready(function (parameters) {
    LoadTenantName();
    LoadPaymentHistory();


});

function LoadPaymentHistory(parameters) {
    var URL = "/Pages/Resident/TenantPaymentHistory.aspx/" + "GetPaymentHistory";
    var obj = {};
    let PaymentHistory = makeAjaxCallReturnPromiss(URL, obj);
    PaymentHistory.then((data) => {
        var PaymentHistoryObj = $.parseJSON(decodeURIComponent(data.d));
        BindPaymentHistory(PaymentHistoryObj);
    }).catch((err) => {
        console.log(err);

    });

}
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
function BindPaymentHistory(PaymentHistoryObj) {
    var content = "";
    if (PaymentHistoryObj.length > 0) {
        $.each(PaymentHistoryObj, function (i, obj) {
            
            var date = ParseJsonDate(obj.CreateDate);

            content += "<tr>";
            content += "<td>" + date + "</td>";
            content += "<td>" + obj.RoutingNo + "</td>";
            content += "<td>" + obj.AccountNo + "</td>";
            content += "<td>" + obj.CheckNo + "</td>";
            content += "<td>" + obj.LastFourDigitCard + "</td>";
            content += "<td>" + obj.AccountType + "</td>";
            content += "<td>" + obj.Amount + "</td>";
            content += "<td>" + obj.TransactionType + "</td>";
            content += "</tr>";
        });
        $("#tblTenantHistory tbody").empty();
        $("#tblTenantHistory tbody").append(content);
    }

}

function ParseJsonDate(date) {
    if (date != null) {
        //return $.datepicker.formatDate('dd-mm-yy', new Date(parseInt(date.substr(6))));
        return $.datepicker.formatDate('yy-mm-dd', new Date(parseInt(date.substr(6))));
    } else {
        return "";
    }

}

$(document).on("click", "#btnPrint", function () {
    var pageTitle = 'Tenant Payment History',
        //stylesheet = '//maxcdn.bootstrapcdn.com/bootstrap/3.3.2/css/bootstrap.min.css',
        win = window.open('', 'Print', 'width=500,height=300');
    win.document.write('<html><head><title>' + pageTitle + '</title>' +
        //'<link rel="stylesheet" href="' + stylesheet + '">' +
        '</head><body>' + $('#tblTenantHistory')[0].outerHTML + '</body></html>');
    win.document.close();
    win.print();
    win.close();
    return false;
});
$(document).on("click", "#btnImport", function () {
    $("#tblTenantHistory").tableToCSV();
});
jQuery.fn.tableToCSV = function () {
    var clean_text = function (text) {
        text = text.replace(/"/g, '\\"').replace(/'/g, "\\'");
        return '"' + text + '"';
    };

    $(this).each(function () {
        var table = $(this);
        var caption = "";
        let tenantname = "Tenant Name : " + $('#txtTenantName').text() + "\n";
        var title = [];
        var rows = [];
        $(this).find('tr').each(function () {
            var data = [];
            $(this).find('th').each(function () {
                var text = clean_text($(this).text());
                title.push(text);
            });
            $(this).find('td').each(function () {
                var text = clean_text($(this).text());
                data.push(text);
            });
            data = data.join(",");
            rows.push(data);
        });
        title = title.join(",");
        rows = rows.join("\n");
        var csv = tenantname + title + rows;
        var uri = 'data:text/csv;charset=utf-8,' + encodeURIComponent(csv);
        var download_link = document.createElement('a');
        download_link.href = uri;
        var ts = new Date().getTime();
        if (caption == "") {
            download_link.download = ts + ".csv";
        } else {
            download_link.download = caption + "-" + ts + ".csv";
        }
        document.body.appendChild(download_link);
        download_link.click();
        document.body.removeChild(download_link);
    });

};
$(document).on('click', '#btnCancel', function () {
    window.location.href = "/Pages/Resident/ResidentTenantDashboard.aspx";
});