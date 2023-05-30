var currentPagePath = window.location.pathname + "/";

var pathname = window.location.pathname; // Returns path only
var url = window.location.href;     // Returns full URL
var origin = window.location.origin;   // Returns base URL
var G_RelationalData = [];
var G_ImageNam = "";
var G_Doclist = 0;
var G_CurRow = "", G_ImageName="";


$(document).ready(function (parameters) {
    $("#txtDate").datepicker({
        dateFormat: "mm-dd-yy",
        changeYear: false,
        changeMonth: true
    });
    LoadVerityIncome();
    LoadAdditionalDoc();
    GelAllSignatureInfo();
    LoadApprovalCode();
      var isresult = true;
    var bShow = $("#hdnShow").val();
    if (bShow === "undefined" || bShow === "") {
        $("#btnExit").css({ 'display': 'none' });
        $("#btnContinue").css({ 'display': 'block' });
        $("#btnAddSignature").css({ 'display': 'block' });
    }
    else {
        if (bShow === "False") {
            $("#btnExit").css({ 'display': 'block' });
            $("#btnContinue").css({ 'display': 'none' });
            $("#btnAddSignature").css({ 'display': 'none' });
        }
        else {
            $("#btnExit").css({ 'display': 'none' });
            $("#btnContinue").css({ 'display': 'block' });
            $("#btnAddSignature").css({ 'display': 'block' });
        }
    }
});


function LoadVerityIncome() {
    var URL = "/Pages/Resident/ResidentialTenantRental_App_Page_4.aspx/" + "GetVerityIncomeList";
    var obj = {};
    let Basic = makeAjaxCallReturnPromiss(URL, obj);
    Basic.then((data) => {
        if (data.d != null || data.d != "") {
            BindVerityIncome($.parseJSON(decodeURIComponent(data.d)));
            //BindBasicTenantInformationGrid($.parseJSON(decodeURIComponent(data.d)));
        }
    });
}
function LoadApprovalCode(parameters) {
    var URL = "/Pages/Resident/ResidentialTentAddResponceStep1.aspx/" + "GetApprovalCode";
    var obj = {};
    let Basic = makeAjaxCallReturnPromiss(URL, obj);
    Basic.then((data) => {
        if (data.d != null || data.d != "") {
            
            var appCode = $.parseJSON(decodeURIComponent(data.d));
            $("#userName").text(appCode.EmailId);

            //BindCheckProcesingFee(deposit[0].getobj, deposit[0].typeChck, deposit[0].percentOrFlatVal);
            //BindBasicTenantInformationGrid($.parseJSON(decodeURIComponent(data.d)));
        }
    });
}
function LoadAdditionalDoc(parameters) {
   
    var URL = "/Pages/Resident/ResidentialTenantRental_App_Page_4.aspx/" + "GetAdditionalDoc";
    var obj = {};
    let Basic = makeAjaxCallReturnPromiss(URL, obj);
    Basic.then((data) => {
        if (data.d != null || data.d != "") {
            BindAdditionalDoc($.parseJSON(decodeURIComponent(data.d)));
            //BindBasicTenantInformationGrid($.parseJSON(decodeURIComponent(data.d)));
        }
    });
}
function BindVerityIncome(result) {
    //$('input[name=r3]:checked').val(),
    var content = "";
    if (result.length > 0) {
        
        $.each(result, function(i, obj) {
            content += "<tr>";
            content += "<td>" + obj.DocumentDescription + "</td>";
            content += "<td>" + obj.FileName + "</td>";
            content += "</tr>";
        });
        $("#tblVerityDoc tbody").empty();
        $("#tblVerityDoc tbody").append(content);

    }

}

function BindAdditionalDoc(result) {
    debugger;
    var content = "";
    if (result.length > 0) {
        G_Doclist = result.length;
        //$.each(result, function (i, obj) {
        //    var isDisabled = '',checkedStatus='',curStatus;
        //    if (obj.IsChecked == 'true') {
        //        isDisabled = '';
        //        checkedStatus = 'checked="checked"';
        //    } else {
        //        isDisabled = 'disabled';
        //    }
        //    if (obj.CurrentStatus == null) {
        //        curStatus = "";
        //    } else {
        //        curStatus = obj.CurrentStatus;
        //    }
        //    content += "<tr>";
        //    content += "<td><input disabled " + checkedStatus + " type='checkbox' id='chk_" + i + "' class='chk' data_status='" + obj.IsViewedOrDownloaded + "'><input type='hidden' value='" + obj.Id + "' /></td>";
        //    content += "<td><label>" + obj.DocumentDescription + "</label></td>";
        //    content += "<td><label >"+obj.IsViewedOrDownloaded + "</label></td>";
        //    content += "<td><input type='button' data_FilePath='" + obj.FilePath + "' data_status='" + obj.IsViewedOrDownloaded + "' id='btn_" + i + "' class='btn btnNewColor docDownloadClick' " + isDisabled + " value='" + obj.IsViewedOrDownloaded + "'></td>";
        //    content += "<td><label>" + curStatus + "</label></td>";
        //    content += "</tr>";
        //});
        //$("#tblDoc tbody").empty();
        //$("#tblDoc tbody").append(content);
     
        //$("#tblDoc tbody tr").each(function (parameters) {
            
        //    if ($($(this).find('td:eq(0)').find('input[type=checkbox]')).is(':checked')) {

        //    } else {
        //        ($(this).find('td:eq(3)').find('input[type=button]')).attr('disabled', false);
        //        return false;
        //    }
        //});
        $.each(result, function (i, obj) {
           
            content += "<tr>";
            content += "<td><label>" + obj.DocumentDescription + "</label></td>";
            content += "<td><label >" + obj.FileName + "</label></td>";
            content += "</tr>";
        });
        $("#tblAdditionalDoc tbody").empty();
        $("#tblAdditionalDoc tbody").append(content);
    }
}

$(document).on('click', '.docDownloadClick', function (parameters) {
    
    let status = $(this).attr('data_status');
    let FilePath = $(this).attr('data_FilePath');
    G_CurRow = $(this).closest('tr');
    if (status === 'Download') {
        window.open("../"+FilePath, '_blank');
       // if (firstRowStatue === 'Download') {
            $("#documentUpload").attr('disabled', false);
            $("#savedoc").attr('disabled', false);
        //}
    } else {
        var newUrl = FilePath.replace('../', origin + '/');
        var n = newUrl.replace('../', '');
        var NnewUrl = newUrl.replace('../', '');
         var ext = NnewUrl.split('.')[1];
                //alert(ext);
                if (ext == 'pdf') {
                   // alert(newUrl);
                    $("#iframedis").show();
                    PDFObject.embed(NnewUrl, "#iframedis", { fallbackLink: false });
                } else {
                    var baseurl = newUrl.replace('../','');
                  
                    $("#iframeimage").show();
                    $("#ifrmImage").attr("src", baseurl);
                    //iframeimage
                }
        //$("#iframedis").show();
        //PDFObject.embed(n, "#iframedis", { fallbackLink: false });

       
        // $($(G_CurRow).find('td:eq(3)').find('input[type="button"]')).attr("data_FilePath", obj.FilePath);


        SavedTheStatus();


        //var nxtRow = $(G_CurRow).next();
        //if (nxtRow !== 'undefined' || nxtRow !== '' || nxtRow !== null) {
        //    $((nxtRow).find('td:eq(0)').find('input[type="checkbox"]')).prop("disabled", false);
        //    $((nxtRow).find('td:eq(3)').find('input[type="button"]')).prop("disabled", false);
        //    let status = $((nxtRow).find('td:eq(0)').find('input[type="checkbox"]')).attr('data_status');
        //    if (status === 'Download') {
        //        $("#documentUpload").attr('disabled', false);
        //        $("#savedoc").attr('disabled', false);
        //    } else {
        //        $("#documentUpload").attr('disabled', true);
        //        $("#savedoc").attr('disabled', true);
        //    }
        //}
    }
});

//$(document).on('click', '#savedoc', function (parameters) {

//});
function SavedTheStatus(parameters) {
    
    var pagePath = window.location.pathname + "/DocViewd";
    var DocId = $(G_CurRow).find('td:eq(0)').find('input[type="hidden"]').val();
    $.ajax({
        type: "POST",
        url: pagePath,
        data: "{ 'DocId':'" + DocId + "','CurrentStatus':'Viewed' }",
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
                    var nxtRow = $(G_CurRow).next();
                    if (nxtRow !== 'undefined' || nxtRow !== '' || nxtRow !== null) {
                        $(G_CurRow).find('td:eq(4)').find('label').text("Viewed");
                        $((G_CurRow).find('td:eq(0)').find('input[type="checkbox"]')).prop("checked", true);
                        $((nxtRow).find('td:eq(0)').find('input[type="checkbox"]')).prop("disabled", false);
                        $((nxtRow).find('td:eq(3)').find('input[type="button"]')).prop("disabled", false);
                        let status = $((nxtRow).find('td:eq(0)').find('input[type="checkbox"]')).attr('data_status');
                        if (status === 'Download') {
                            $("#documentUpload").attr('disabled', false);
                            $("#savedoc").attr('disabled', false);
                        } else {
                            $("#documentUpload").attr('disabled', true);
                            $("#savedoc").attr('disabled', true);
                        }
                    }

                    //data_FilePath
                }
                //BindVerityIncome(obj);
                //$("#txtsavingBankDoc").val("");
                //$("#fileImageUpload").val("");
                //notify('success', "Upload successfully");
            }

    });
}
function SavedocumentUpload(parameters) {
    
    if (document.getElementById("documentUpload").value != "") {
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


//function UpdateFilesDoc(evt) {
    
//    if ($("#documentUpload").val() != "") {
//        var pagePath = window.location.pathname + "/DocImageUpload";
//        // var CurRow = G_CurRow;
        
//        var DocId = $(G_CurRow).find('td:eq(0)').find('input[type="hidden"]').val();
//        //var docId=
//        var result = evt.target.result;
//        var ImageSave = result.replace("data:image/jpeg;base64,", "");
//        $.ajax({
//            type: "POST",
//            url: pagePath,
//            data: "{ 'Image':'" + ImageSave + "' , 'ImageName':'" + G_ImageName + "','DocId':'" + DocId + "','CurrentStatus':'Uploaded' }",
//            contentType: "application/json; charset=utf-8",
//            dataType: "json",
//            error:
//                function (XMLHttpRequest, textStatus, errorThrown) {
//                    alert("Error");
//                    G_ImageName = "";
//                },
//            success:
//                function (result) {
//                    G_ImageName = "";
//                    var obj = $.parseJSON(decodeURIComponent(result.d));
                    
//                    if (obj !=null) {
//                        $(G_CurRow).find('td:eq(4)').find('label').text("Downloaded");
//                        $((G_CurRow).find('td:eq(0)').find('input[type="checkbox"]')).prop("checked", true);
//                        $($(G_CurRow).find('td:eq(3)').find('input[type="button"]')).attr("data_FilePath", obj.FilePath);
//                        var nxtRow = $(G_CurRow).next();
//                        if (nxtRow !== 'undefined' || nxtRow !== '' || nxtRow !==null) {
//                            $((nxtRow).find('td:eq(0)').find('input[type="checkbox"]')).prop("disabled", false);
//                            $((nxtRow).find('td:eq(3)').find('input[type="button"]')).prop("disabled", false);
//                            let status = $((nxtRow).find('td:eq(0)').find('input[type="checkbox"]')).attr('data_status');
//                            if (status === 'Download') {
//                                $("#documentUpload").attr('disabled', false);
//                                $("#savedoc").attr('disabled', false);
//                            } else {
//                                $("#documentUpload").attr('disabled', true);
//                                $("#savedoc").attr('disabled', true);
//                            }
//                        }
                        
//                        //data_FilePath
//                    }
//                    //BindVerityIncome(obj);
//                    //$("#txtsavingBankDoc").val("");
//                    //$("#fileImageUpload").val("");
//                    //notify('success', "Upload successfully");
//                }

//        });

//    } else {
//        //  alert("please fill up red field");
//        notify('danger', "Please fill out Description and Upload a file");
//    }

//}

function UpdateFilesDoc(evt) {

    if ($("#documentUpload").val() != "") {
        var pagePath = window.location.pathname + "/DocImageUpload";
        // var CurRow = G_CurRow;

        //var DocId = $(G_CurRow).find('td:eq(0)').find('input[type="hidden"]').val();
        var DocId = "0";
        var txtAddiDoc = $("#txtAddiDoc").val();
        //var docId=
        var result = evt.target.result;
        var ImageSave = "";
        if (G_ImageName == "jpg" || G_ImageName == "jpeg" || G_ImageName == "png" || G_ImageName == 'gif' || G_ImageName == 'tiff') {
            ImageSave = result.replace("data:image/jpeg;base64,", "");
        } else if (G_ImageName == "pdf") {
            ImageSave = result.replace("data:application/pdf;base64,", "");
        }
        else if (G_ImageName == "docx") {
            ImageSave = result.replace(/^data:(.*;base64,)?/, '');
        } else if (G_ImageName == "doc") {
            ImageSave = result.replace("data:application/msword;base64,", "");
        } else if (G_ImageName == "txt") {
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
                        //$(G_CurRow).find('td:eq(4)').find('label').text("Downloaded");
                        //$((G_CurRow).find('td:eq(0)').find('input[type="checkbox"]')).prop("checked", true);
                        //$($(G_CurRow).find('td:eq(3)').find('input[type="button"]')).attr("data_FilePath", obj.FilePath);
                        //var nxtRow = $(G_CurRow).next();
                        //if (nxtRow !== 'undefined' || nxtRow !== '' || nxtRow !== null) {
                        //    $((nxtRow).find('td:eq(0)').find('input[type="checkbox"]')).prop("disabled", false);
                        //    $((nxtRow).find('td:eq(3)').find('input[type="button"]')).prop("disabled", false);
                        //    let status = $((nxtRow).find('td:eq(0)').find('input[type="checkbox"]')).attr('data_status');
                        //    if (status === 'Download') {
                        //        $("#documentUpload").attr('disabled', false);
                        //        $("#savedoc").attr('disabled', false);
                        //    } else {
                        //        $("#documentUpload").attr('disabled', true);
                        //        $("#savedoc").attr('disabled', true);
                        //    }
                        //}
                        $("#txtAddiDoc").val("");
                        $("#documentUpload").val("");
                        LoadAdditionalDoc();
                        notify('success', "Document Added successfully !!");
                        //data_FilePath
                    }
                    //BindVerityIncome(obj);
                    //$("#txtsavingBankDoc").val("");
                    //$("#fileImageUpload").val("");
                    //notify('success', "Upload successfully");
                }

        });

    } else {
        //  alert("please fill up red field");
        notify('danger', "Please fill out Description and Upload a file");
    }

}
function SaveVerityIncome() {
    if (document.getElementById("fileImageUpload").value != "") {
        var file = document.getElementById('fileImageUpload').files[0];
        G_ImageName = file.name;
        var fileName = document.getElementById("fileImageUpload").value;
        var idxDot = fileName.lastIndexOf(".") + 1;
        var extFile = fileName.substr(idxDot, fileName.length).toLowerCase();
        if (extFile == "jpg" || extFile == "jpeg" || extFile == "png" || extFile == 'gif' || extFile == 'tiff' || extFile == 'pdf' || extFile == 'docx' || extFile == 'doc' || extFile == 'txt') {
            //TO DO
            var reader = new FileReader();
            reader.readAsDataURL(file);
            reader.onload = UpdateFilesMaintenance;
        } else {
            //alert("Only .gif, .jpg, .png, .tiff and .jpeg are allowed!");
            notify('danger', "Only .doc, ,docx, .txt, .pdf, .gif, .jpg, .png, .tiff and .jpeg are allowed!");
            $("#fileImageUpload").val("");
        }

    }
    else {
        //alert('Please Choose An Image');
        notify('danger', "Please select Income File");
    }
}
function UpdateFilesMaintenance(evt) {
    if ($("#txtsavingBankDoc").val() != "") {
        var desc = $("#txtsavingBankDoc").val();
        var pagePath = window.location.pathname + "/VerifyImageUpload";
        var result = evt.target.result;
        var ImageSave = "";
        if (G_ImageName == "jpg" || G_ImageName == "jpeg" || G_ImageName == "png" || G_ImageName == 'gif' || G_ImageName == 'tiff') {
            ImageSave = result.replace("data:image/jpeg;base64,", "");
        } else if (G_ImageName == "pdf") {
            ImageSave = result.replace("data:application/pdf;base64,", "");
        }
        else if (G_ImageName == "docx") {
            ImageSave = result.replace(/^data:(.*;base64,)?/, '');
        } else if (G_ImageName == "doc") {
            ImageSave = result.replace("data:application/msword;base64,", "");
        } else if (G_ImageName == "txt") {
            ImageSave = result.replace("data:text/plain;base64,", "");
        }
        $.ajax({
            type: "POST",
            url: pagePath,
            data: "{ 'Image':'" + ImageSave + "' , 'ImageName':'" + G_ImageName + "','Description':'" + desc + "' }",
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
                    BindVerityIncome(obj);
                    $("#txtsavingBankDoc").val("");
                    $("#fileImageUpload").val("");
                    notify('success', "Uploaded successfully");
                }

        });

    } else {
        //  alert("please fill up red field");
        notify('danger', "Please fill out Description and Upload a file");
    }

}
//---------- Clear Data -------------- //

function ValidationForGeneralInfo(parameters) {
    var isresult = true;
    var Pets = $("#txtPetsDetails").val();
    var RecuringProblem = $("#txtreoccurring").val();
    var movingReason = $("#txtMovingReason").val();


    // var IsStillWorkthere = $("#StillWork").is(":checked") == true ? true : false;

    if (Pets === "undefined" || Pets === "") {
        $("#txtPetsDetails").css({ 'border': '1px solid red' });
        isresult = false;
    }
    else {
        $("#txtPetsDetails").css({ 'border': '1px solid #d2d6de' });
    }
    if (RecuringProblem === "undefined" || RecuringProblem === "") {
        $("#txtreoccurring").css({ 'border': '1px solid red' });
        isresult = false;
    }
    else {
        $("#txtreoccurring").css({ 'border': '1px solid #d2d6de' });
    }

    if (movingReason === "undefined" || movingReason === "") {
        $("#txtMovingReason").css({ 'border': '1px solid red' });
        isresult = false;
    }
    else {
        $("#txtMovingReason").css({ 'border': '1px solid #d2d6de' });
    }


    if (isresult)
        isresult = true;
    else {
        isresult = false;
    }
    return isresult;
}

function getTheEmergencyObj(parameters) {
    var Obj = {
        Id: $("#hdRowId").val(),
        LateRentNotice: $('input[name=late]:checked').val(),
        PropertySmoke: $('input[name=smoke]:checked').val(),
        RentingFrom: $("#ddlRenting").val(),
        Bankruptcy: $('input[name=bank]:checked').val(),
        MoveIn: $("#ddlMoveIn").val(),
        Felony: $('input[name=felony]:checked').val(),
        Eviction: $('input[name=eviction]:checked').val(),
        EvictionWhen: $("#txtwhen").val(),
        PetsDetails: $("#txtPetsDetails").val(),
        Reoccurringproblems: $('input[name=reoccurring]:checked').val(),
        ReoccurringProblemsWhen: $("#txtreoccurring").val(),
        MovingReason: $("#txtMovingReason").val(),
        VerifiableIncome: $("#txtAmountOfIncome").val(),
        LoanHelpingPersons: $("#txtFinancialProblem").val(),
        LawsuitParty: $('input[name=lawsuit]:checked').val(),
        LawsuitPartyReason: $("#txtlawsuit").val(),
        CriminalBackground: $('input[name=creditcheck]:checked').val(),
        CriminalBackgroundReason: $("#txtcreditcheck").val(),
        AboutApartment: $("#txtthisApartment").val(),
        ApartmentRefererPersonDetails: $("#txtOtherPeopleForAppartment").val()
    }
    return Obj;
}

//--------------------------------------------- Save And Continue ---------------------------------------//
$(document).on('click', '#btnContinue', function (parameters) {
debugger;
    if ($("#chkaggrement").is(':checked')) {
        if (Validationform()) {
            var pagePath = window.location.pathname + "/FinalSubmit";
            var investigateReport = $("#investigateReport").val();
            var CredtReport = $("#CredtReport").val();
           
            $.ajax({
                type: "POST",
                url: pagePath,
                data: "{ 'investigateReport':'" + investigateReport + "' , 'CredtReport':'" + CredtReport + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                error:
                    function (XMLHttpRequest, textStatus, errorThrown) {
                        alert("Error");
                       
                    },
                success:
                    function (result) {
                        
                        var obj = $.parseJSON(decodeURIComponent(result.d));
                        
                        if (obj == "true") {
                            notify('success', "Your Application submitted successfully. Please check your Email");
                        } else {
                            notify('danger', obj);
                        }
                       
                    }

            });
        }
        else {
            notify('danger', "Please Add Income Document and Signature Information");
            }

    } else {
        notify('danger', "Please click the agreement checkbox");
    }
});

$(document).on('click', '#btnBack', function (parameters) {
    var origin = window.location.origin; // Returns base URL
    var url = origin + "/Pages/Resident/ResidentialTenantRental_App_Page_3.aspx";
    window.location.href = url;
});

function Validationform() {
    var isresult = true;
    var verityIncomeDocCheck = $("#tblVerityDoc tbody tr").length;
    var SignatureCheck = $("#tblSignature tbody tr").length;
    var AdditionalDoc = $("#tblDoc tbody tr").length;

    //if (verityIncomeDocCheck == null || verityIncomeDocCheck ==0) {
    //    notify('danger', "Please Add Income Document");
    //    isresult = false;
    //}
   
    if (SignatureCheck === "undefined" || SignatureCheck === "" || SignatureCheck == 0 || SignatureCheck==null) {
        notify('danger', "Please Add Signature Information");
        isresult = false;
    }
    
    //if (AdditionalDoc != null || AdditionalDoc > 0) {
    //    $("#tblDoc tbody tr").each(function (parameters) {
            
    //        if ($($(this).find('td:eq(0)').find('input[type=checkbox]')).is(':checked')) {

    //        } else {
    //            isresult = false;

    //            notify('danger', "All the Additional documents must be Uploaded or Downloaded");
    //            return false;
    //        }
    //    });
    //}
   


    if (isresult)
        isresult = true;
    else {
        isresult = false;
    }
    return isresult;
}
function GelAllSignatureInfo(parameters) {
    var obj = {}
    var URL = "/Pages/Resident/ResidentialTenantRental_App_Page_4.aspx/" + "GetAllSignature";
    let Basic = makeAjaxCallReturnPromiss(URL, obj);
    Basic.then((data) => {
        if (data.d != null || data.d != "") {
            var res = $.parseJSON(decodeURIComponent(data.d));
            if (res != null) {
                BindSecurity($.parseJSON(decodeURIComponent(data.d)));
               
               // $("#btnAddSignature").text("Add Another Aggrement Signer");
            } else {
                notify('danger', "Save Failed !!");
            }
            //BindEmergencyContactToField($.parseJSON(decodeURIComponent(data.d)));
        }
    });

}  
function validateSignature() {
    var isresult = true;
    var signature = $("#txtSign").val();
    var secutiry = $("#txtSecurity").val();
    var date = $("#txtDate").val();


    // var IsStillWorkthere = $("#StillWork").is(":checked") == true ? true : false;

    if (signature === "undefined" || signature === "") {
        $("#txtSign").css({ 'border': '1px solid red' });
        isresult = false;
    }
    else {
        $("#txtSign").css({ 'border': '1px solid #d2d6de' });
    }
    if (secutiry === "undefined" || secutiry === "") {
        $("#txtSecurity").css({ 'border': '1px solid red' });
        isresult = false;
    }
    else {
        $("#txtSecurity").css({ 'border': '1px solid #d2d6de' });
    }

    if (date === "undefined" || date === "") {
        $("#txtDate").css({ 'border': '1px solid red' });
        isresult = false;
    }
    else {
        $("#txtDate").css({ 'border': '1px solid #d2d6de' });
    }


    if (isresult)
        isresult = true;
    else {
        isresult = false;
    }
    return isresult;
}

function GetSignatureObject(parameters) {
    
    var obj = {
        Id: $("#hdId").val(),
        SignatureName: $("#txtSign").val(),
        SecurityNo: $("#txtSecurity").val(),
        AddingDate: $("#txtDate").val()
    }
    return obj;
}

function BindSecurity(result) {
    
    let content = "";
    if (result.length>0) {
        $.each(result, function (i, obj) {
            
            content += "<tr>";
            content += "<td> " + obj.SignatureName + "<input type='hidden' value='" + obj.Id + "'></td>";
            content += "<td>" + obj.SecurityNo + "</td>";
            content += "<td>" + obj.AddingDate + "</td>";
            content += "<td> <input type='button' class='btn btnNewColor' style='background-color: #3B5998' onclick='Edit(this)' id='" + obj.Id + "' data_Id='" + obj.Id + "' value='Edit' /></td>";
            content += "<td> <input type='button' class='btn btnNewColor' style='background-color: #3B5998' onclick='Delete(this)' id='" + obj.Id + "' data_Id='" + obj.Id + "' value='Delete' /></td>";
            content += "</tr>";
        });
        $("#tblSignature tbody").empty();
        $("#tblSignature tbody").append(content);
    }
}
$(document).on('click', '#btnAddSignature', function (parameters) {
    
    if (validateSignature()) {
        var obj = GetSignatureObject();
        var URL = "/Pages/Resident/ResidentialTenantRental_App_Page_4.aspx/" + "SaveSecurityInfo";
        let Basic = makeAjaxCallReturnPromiss(URL, obj);
        Basic.then((data) => {
            if (data.d != null || data.d != "") {
                
                var res = $.parseJSON(decodeURIComponent(data.d));
                if (res != null) {
                    BindSecurity(res);
                    if ($("#btnAddSignature").val() == "Update") {
                        notify('success', "Signature Updated Successfully");
                    } else {
                        notify('success', "Signature Added Successfully");
                    }
                    $("#hdId").val(0);
                    $("#txtSign").val("");
                    $("#txtSecurity").val("");
                    $("#txtDate").val("");

                    $("#btnAddSignature").val("Add Agreement Signer");
                } else {
                    notify('danger', "Signature Added Failed !!");
                }
                //BindEmergencyContactToField($.parseJSON(decodeURIComponent(data.d)));
            }
        });
    }
});

function Edit(curtd) {

    var id = $(curtd).attr('Id');
    var trRow = $(curtd).closest('tr');
    var Sign = $(trRow).find('td:eq(0)').text();
    var Security = $(trRow).find('td:eq(1)').text();
    var Date = $(trRow).find('td:eq(2)').text();

    $("#hdId").val(id);
    $("#txtSign").val(Sign);
    $("#txtSecurity").val(Security);
    $("#txtDate").val(Date);
    $("#btnAddSignature").val("Update");
}
function Delete(curtd) {
    
    var id = $(curtd).attr('Id');

    var pagePath = window.location.pathname + "/Delete";
    $.ajax({
        type: "POST",
        url: pagePath,
        data: JSON.stringify({ "obj": id }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        error:
            function (XMLHttpRequest, textStatus, errorThrown) {
                alert("Error in post");
            },
        success:
            function (result) {
                var mass = $.parseJSON(decodeURIComponent(result.d));
                var content = "";
                if (mass == '') {

                } else {
                    if (mass !=null) {
                        notify('success', "Signature Deleted Successfully");
                        BindSecurity(mass);
                    } else {
                        notify('success', "Signature Deleted Failed!!");
                    }

                    //GelAllSignatureInfo();

                    //setEquipmentData(lstofEquipmentData.RentalUnit);
                }

            }

    });
}

$(document).on('click', '#btnExit', function (parameters) {
    
    var origin = window.location.origin; // Returns base URL
    var url = origin + "/Pages/DashboardAdmin.aspx";
    window.location.href = url;
});