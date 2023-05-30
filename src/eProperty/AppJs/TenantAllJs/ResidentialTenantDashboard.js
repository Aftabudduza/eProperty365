var currentPagePath = window.location.pathname + "/";

var pathname = window.location.pathname; // Returns path only
var url = window.location.href;     // Returns full URL
var origin = window.location.origin;   // Returns base URL
var G_RelationalData = [];
$(document).ready(function (parameters) {
    LoadTenantName();
    LoadMessage();

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

function LoadMessage(parameters) {
    var URL = "/Pages/Resident/ResidentTenantDashboard.aspx/" + "LoadMessage";
    const monthNames = ["January", "February", "March", "April", "May", "June","July", "August", "September", "October", "November", "December"];
    const d = new Date();
    
    var currentMonth = "";
    if ($("#showMore").is(':checked') == true) {
        var newDate = new Date(d.getFullYear(), d.getMonth(), d.getDate() + 30);
        currentMonth = monthNames[newDate.getMonth()];
    } else {
        currentMonth = monthNames[d.getMonth()];
    }
    var RequestType = $("#ddlRequestType").val();
    var obj = {
        MonthName: currentMonth,
        RequestType: RequestType == '-1' ? '' : RequestType
    };
    let tenantName = makeAjaxCallReturnPromiss(URL, obj);
    tenantName.then((data) => {
        
        var Message = $.parseJSON(decodeURIComponent(data.d));
        if (Message.length>0) {
            BindMessage(Message);
        }
       // $("#txtTenantName").text(TeantName);

    }).catch((err) => {
        console.log(err);

    });
}

$(document).on('change', '#showMore', function(parameters) {
    LoadMessage();
});
$(document).on('change', '#ddlRequestType', function (parameters) {
    LoadMessage();
});
function BindMessage(Message) {
    
    var content = "";
    if (Message.length > 0) {
        $.each(Message, function (i, obj) {
            
            content += "<p style='margin-top:0px;margin-bottom:0px;'>From - <span style='font-weight:bold'>" + obj.FromUser + "</span> - <span style='font-weight:bold'>" + obj.fromDate + "  " + obj.FromTime + "</span></p>";
            content += "<p style='margin-left:10px;'>" + obj.QustionMessage + "</p>";
            if (obj.ToUser != "") {
                content += "<p style='color: #8BB7DE;margin-bottom: 0px;'>To - <span style='font-weight:bold'>" + obj.ToUser + "</span> - <span style='font-weight:bold'>" + obj.ToDate + "  " + obj.ToTime + "</span></p>";
                content += "<p style='color:#8BB7DE;margin-bottom:10px;margin-left:10px;'>" + obj.AnswerFromOwner + "</p>";
            }

        });
        $("#txtMessageLoad").html("");
        $("#txtMessageLoad").html(content);
    }
}
$(document).on('click', '#btnSendQuote', function (parameters) {
    var URL = "/Pages/Resident/ResidentTenantDashboard.aspx/" + "SendMessage";
    var obj= {
        Message: $("#txtMessagesend").val(),
        RequestType: $("#ddlRequestType").val()
    }
    let SendMessage = makeAjaxCallReturnPromiss(URL, obj);
    SendMessage.then((data) => {
       // var status = $.parseJSON(decodeURIComponent(data.d));
        
        var Message = $.parseJSON(decodeURIComponent(data.d));
        if (Message.length > 0) {
            notify('success', " Save Successfully !!");
            BindMessage(Message);
        } else {
            notify('danger', "Save Failed !!");
        }

    }).catch((err) => {
        console.log(err);

    });
});