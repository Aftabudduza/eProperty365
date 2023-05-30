var currentPagePath = window.location.pathname + "/";

var pathname = window.location.pathname; // Returns path only
var url = window.location.href;     // Returns full URL
var origin = window.location.origin;   // Returns base URL
var G_RelationalData = [];
var G_ImageNam = "";
var G_Doclist = 0;
var G_CurRow = "", G_ImageName = "";

$(document).ready(function (parameters) {
    LoadTenantName();
    LoadAdditionalDoc();
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

function LoadAdditionalDoc(parameters) {
    var URL = "/Pages/Resident/TenantDocuments.aspx/" + "GetAdditionalDoc";
    var obj = {};
    let Basic = makeAjaxCallReturnPromiss(URL, obj);
    Basic.then((data) => {
        if (data.d != null || data.d != "") {
            BindAdditionalDoc($.parseJSON(decodeURIComponent(data.d)));
        }
    });
}

function BindAdditionalDoc(result) {
    debugger;
    var content = "";
    if (result.length > 0) {
        G_Doclist = result.length;

        $.each(result, function (i, obj) {
            content += "<tr>";
            content += "<td><label>" + obj.DocumentDescription + "</label></td>";
            content += "<td><label >" + obj.FileName + "</label></td>";
            content += "<td style='width: 50%'>";
            content += "<a style='color: blue;' target='_blank' href='" + obj.FilePath + "'>Download</a>";
            content += "</td>";
            content += "</tr>";
        });
        $("#tblAdditionalDoc tbody").empty();
        $("#tblAdditionalDoc tbody").append(content);
    }
}

function SavedocumentUpload(parameters) {

    if (document.getElementById("documentUpload").value != "") {
        debugger;
        var file = document.getElementById('documentUpload').files[0];
        G_ImageName = file.name;
        var fileName = document.getElementById("documentUpload").value;
        var idxDot = fileName.lastIndexOf(".") + 1;
        var extFile = fileName.substr(idxDot, fileName.length).toLowerCase();
        if (extFile == "jpg" || extFile == "jpeg" || extFile == "png" || extFile == 'gif' || extFile == 'tiff' || extFile == 'pdf' || extFile == 'docx' || extFile == 'doc' || extFile == 'txt') {
            //TO DO
            var reader = new FileReader();
            reader.readAsDataURL(file);
            reader.onload = UpdateFilesDoc;
        } else {
            //alert("Only .gif, .jpg, .png, .tiff and .jpeg are allowed!");
            notify('danger', "Only .doc, ,docx, .txt, .pdf, .gif, .jpg, .png, .tiff and .jpeg are allowed!");
            $("#fileImageUpload").val("");
        }

    }
    else {
        //alert('Please Choose An Image');
        notify('danger', "Please select Additional File");
    }
}

function UpdateFilesDoc(evt) {

    if ($("#documentUpload").val() != "") {
        var pagePath = window.location.pathname + "/DocImageUpload";
        var DocId = "0";
        var txtAddiDoc = $("#txtAddiDoc").val();
        debugger;
        var result = evt.target.result;
        var ImageSave = "";
        var ext = G_ImageName.substr((G_ImageName.lastIndexOf('.') + 1));
        if (ext == "jpg" || ext == "jpeg" || ext == "png" || ext == 'gif' || ext == 'tiff') {
            ImageSave = result.replace("data:image/jpeg;base64,", "");
        } else if (ext == "pdf") {
            ImageSave = result.replace("data:application/pdf;base64,", "");
        }
        else if (ext == "docx") {
            ImageSave = result.replace(/^data:(.*;base64,)?/, '');
        } else if (ext == "doc") {
            ImageSave = result.replace("data:application/msword;base64,", "");
        } else if (ext == "txt") {
            ImageSave = result.replace("data:text/plain;base64,", "");
        }
        $.ajax({
            type: "POST",
            url: pagePath,
            data: "{ 'Image':'" + ImageSave + "' , 'ImageName':'" + G_ImageName + "','DocId':0,'CurrentStatus':'Uploaded' ,'DocumentDescription' : '" + txtAddiDoc + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            error:
                function (XMLHttpRequest, textStatus, errorThrown) {
                    alert("Error");
                    G_ImageName = "";
                },
            success:
                function (result) {
                    G_ImageName = "";
                    var obj = $.parseJSON(decodeURIComponent(result.d));

                    if (obj != null) {
                        $("#txtAddiDoc").val("");
                        $("#documentUpload").val("");
                        LoadAdditionalDoc();
                        notify('success', "Document Added successfully !!");
                    }

                }

        });

    } else {
        notify('danger', "Please fill out Description and Upload a file");
    }

}

$(document).on('click', '#btnCancel', function () {
    window.location.href = "/Pages/Resident/ResidentTenantDashboard.aspx";
});