var base = "~";
var DataUrl = "/Pages/Admin/AddResidentialUnit.aspx";

var pathname = window.location.pathname; // Returns path only
var url = window.location.href;     // Returns full URL
var origin = window.location.origin;   // Returns base URL
var submitt = false;
var isCheckoutSuccessful = false;
var sResponseCode = "";
var sResponseDetails = "";
var sApplicationFee = "";
var sLast4 = "";
var sExpMonth = "";
var sExpYear = "";
var bShow = true;

$(document).ready(function () {
    $('input[type="checkbox"].flat-red, input[type="radio"].flat-red').iCheck({
        checkboxClass: 'icheckbox_flat-green',
        radioClass: 'iradio_flat-green'
    });
    $(".tDate").datepicker({
        dateFormat: "mm-dd-yy",
        changeYear: false,
        changeMonth: true
    });

    //$("#txtdateFrom").datepicker({
    //    dateFormat: "mm-dd-yyyy",
    //    changeYear: true,
    //    changeMonth: true
    //});
    //$("#txtDate").datepicker({
    //    dateFormat: "mm-dd-yyyy",
    //    changeYear: true,
    //    changeMonth: true
    //});
    //$("#txtOwnerDate").datepicker({
    //    dateFormat: "mm-dd-yyyy",
    //    changeYear: true,
    //    changeMonth: true
    //});
    //txtDate  txtOwnerDate

    LoadComboBox();
    LoadDeposite();
    GelAllOwnerInfo();
    LoadApprovalCode();
    LoadTenantFee();    
    var isresult = true;
    bShow = $("#hdnShow").val();
    if (bShow === "undefined" || bShow === "") {
        $("#btnSubmit").css({ 'display': 'block' });
        $("#btnSubmitCash").css({ 'display': 'none' });
        //$("#btnSaveAndContinue").css({ 'display': 'block' }); 
        $("#btnExit").css({ 'display': 'none' });
    }
    else {
        if (bShow === "False") {
            $("#btnSubmit").css({ 'display': 'none' });
            $("#btnSubmitCash").css({ 'display': 'none' });
            //$("#btnSaveAndContinue").css({ 'display': 'none' });
            $("#btnExit").css({ 'display': 'block' });
        }
        else {
            $("#btnSubmit").css({ 'display': 'block' });
            $("#btnSubmitCash").css({ 'display': 'none' });
            //$("#btnSaveAndContinue").css({ 'display': 'block' });
            $("#btnExit").css({ 'display': 'none' });
        }
    }

});



function LoadComboBox(parameters) {
    var content = '';
    for (var i = 1; i <= 30; i++) {
        content += '<option value="' + i + '">' + i + '</option>';
    }
    $("#txtDueDate option").empty();
    $("#txtDueDate").append(content);
    $("#txtDueDate").val("1").trigger('change');

    //------------- Load Country ..............//
    var URL = "/Pages/Admin/AddResidentialUnit.aspx/" + "GetCountry";
    var obj = {};
    let Country = makeAjaxCallReturnPromiss(URL, obj);

    //.................. load State ...............//
    var URL = "/Pages/Admin/AddResidentialUnit.aspx/" + "GetState";
    var obj = {};
    let State = makeAjaxCallReturnPromiss(URL, obj);

    //GetAllCity
   // var pagePath = "/Pages/Admin/AddResidentialUnit.aspx" + "/GetCity";
   // var URL = "/Pages/Admin/AddResidentialUnit.aspx" + "/GetCity";
   // var obj = {};
   // let City = makeAjaxCallReturnPromiss(URL, obj);

    Country.then((data) => {
      //  console.log("Country Data Loaded");
        let countryData = setCombo($.parseJSON(decodeURIComponent(data.d)), 'US');
        //$("#ddlCountry option").empty();
        //$("#ddlCountry").append(countryData);

        $(".country option").empty();
        $(".country").append(countryData);
        $(".country").select2();
    }).catch((err) => {
        console.log(err);

    });
    State.then((data) => {
        //console.log("State Data Loaded");
        let StateData = setCombo($.parseJSON(decodeURIComponent(data.d)), '-1');
        $(".state option").empty();
        $(".state").append(StateData);
        $(".state").select2();

        // console.log(data.d);
    }).catch((err) => {
        console.log(err);
    });
    //City.then((data) => {
    //    let CityData = setCombo_withInt($.parseJSON(decodeURIComponent(data.d)), '-1');
    //    $(".city option").empty();
    //    $(".city").append(CityData);
    //    $(".city").select2();
    //});

    Promise.all([Country, State]).then(function () {
        //LoadTenantBasicGrid();
        //LoadTenantResidenceGrid();
        //LoadTenantEmployerInformationGrid();
        //LoadTenantReferenceGrid();
        LoadAdditionalDoc();
        GelAllSignatureInfo();
    });


}

function LoadDeposite(parameters) {
    var URL = "/Pages/Resident/ResidentialTenantAddResponceStep4_Sign_Deposit.aspx/" + "GetDepositeAmount";
    var obj = {};
    let Basic = makeAjaxCallReturnPromiss(URL, obj);
    Basic.then((data) => {
        if (data.d != null || data.d != "") {
            
            var deposit = $.parseJSON(decodeURIComponent(data.d));
            BindTenantDepositeAmount(deposit[0].getobj, deposit[0].typeChck, deposit[0].percentOrFlatVal);
            //BindCheckProcesingFee(deposit[0].getobj, deposit[0].typeChck, deposit[0].percentOrFlatVal);
            //BindBasicTenantInformationGrid($.parseJSON(decodeURIComponent(data.d)));
        }
    });
}


function LoadApprovalCode(parameters) {
      var URL = "/Pages/Resident/ResidentialTenantAddResponceStep4_Sign_Deposit.aspx/" + "GetApprovalCode";
    var obj = {};
    let Basic = makeAjaxCallReturnPromiss(URL, obj);
    Basic.then((data) => {
        if (data.d != null || data.d != "") {
            
            var appCode = $.parseJSON(decodeURIComponent(data.d));
            $("#ApplicationId").text(appCode.ApplicationCode);
            $("#userName").text(appCode.EmailId);

            //BindCheckProcesingFee(deposit[0].getobj, deposit[0].typeChck, deposit[0].percentOrFlatVal);
            //BindBasicTenantInformationGrid($.parseJSON(decodeURIComponent(data.d)));
        }
    });
}
function BindTenantDepositeAmount(result, typeChck, percentOrFlatVal) {
    
    if (result !=null) {
        $("#txtSecurityDeposite").val(result.SecurityDeposit);
        $("#txtOneMonthRent").val(result.MonthlyRent);
        $("#txtProrateAmount").val(result.ProrateAmount);
        $("#txtFirtMonthRent").val(result.FirstMonthRent);
        $("#txtTotalDue").val(result.Total);

        if (result.MonthlyPaymentDueDate != null) {
            $("#txtDueDate").val(result.MonthlyPaymentDueDate).trigger('change');
           // $("#txtDueDate").val(result.MonthlyPaymentDueDate);
        }
       
    }
    
    $("#subTotalCharge").text(result.Total);
    var percentRatio = percentOrFlatVal;
    var Amount = percentOrFlatVal;
    if (typeChck == 1) {
        $("#percentRatio").text(percentRatio.toString() +"%");
        //var vv = result.Total - (result.Total * (percentRatio / 100));
        var vv = result.Total * percentRatio / 100;
        Amount = vv;
        $("#Amount").text(Amount.toString());
        //$("#Amount").text(0.00);
    } else {
        $("#percentRatio").text(percentRatio.toString());
        Amount = percentOrFlatVal;
        $("#Amount").text(Amount.toString());
    }
    var TotalAmount = result.Total + Amount;
    $("#TotalAmountCharge").text(TotalAmount);


    $("#txtAmountOff").val(result.Total);

}



function LoadAdditionalDoc(parameters) {

    var URL = "/Pages/Resident/ResidentialTenantAddResponceStep4_Sign_Deposit.aspx/" + "GetAdditionalDoc";
    var obj = {};
    let Basic = makeAjaxCallReturnPromiss(URL, obj);
    Basic.then((data) => {
        if (data.d != null || data.d != "") {
            BindAdditionalDoc($.parseJSON(decodeURIComponent(data.d)));
            //BindBasicTenantInformationGrid($.parseJSON(decodeURIComponent(data.d)));
        }
    });
}

function BindAdditionalDoc(result) {
    debugger;
    var content = "";
    if (result.length > 0) {
        G_Doclist = result.length;
        $.each(result, function (i, obj) {
            debugger;
            var isDisabled = '', checkedStatus = '', curStatus;
            if (obj.IsChecked == 'true') {
                isDisabled = '';
                checkedStatus = 'checked="checked"';
            } else {
                isDisabled = 'disabled';
            }
            if (obj.CurrentStatus == null) {
                curStatus = "";
            } else {
                curStatus = obj.CurrentStatus;
            }
            content += "<tr>";
            content += "<td><input disabled " + checkedStatus + " type='checkbox' id='chk_" + i + "' class='chk'><input type='hidden' value='" + obj.Id + "' /></td>";
            content += "<td><label>" + obj.DocumentDescription + "</label></td>";
            content += "<td><label >" + obj.Status + "</label></td>";
            content += "<td><input type='button' data_FilePath='" + obj.FilePath + "' id='btn_" + i + "'  data_status='Download' class='btn btnNewColor docDownloadClick' " + isDisabled + " value='Download'></td>";
            content += "<td><label>" + curStatus + "</label></td>";
            content += "</tr>";
        });
        $("#tblDoc tbody").empty();
        $("#tblDoc tbody").append(content);
        //  $(".chk").prop('disabled', true);

        //$("#chk_0").prop('disabled', false);
        // $("#btn_0").prop('disabled', false);
        //let firstRowStatue = $("#chk_0").attr('data_status');
        $("#tblDoc tbody tr").each(function (parameters) {

            if ($($(this).find('td:eq(0)').find('input[type=checkbox]')).is(':checked')) {

            } else {
                ($(this).find('td:eq(3)').find('input[type=button]')).attr('disabled', false);
                return false;
            }
        });
    }
}


$(document).on('click', '.docDownloadClick', function (parameters) {

    let status = $(this).attr('data_status');
    let FilePath = $(this).attr('data_FilePath');
    G_CurRow = $(this).closest('tr');
    if (status === 'Download') {
        window.open("../" + FilePath, '_blank');
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
function SavedTheStatus(parameters) {

    var pagePath = "/Pages/Resident/ResidentialTenantAddResponceStep4_Sign_Deposit.aspx/" + "/DocViewd";
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
        notify('danger', "Please Choose a File");
    }
}
function UpdateFilesDoc(evt) {
    if ($("#documentUpload").val() != "") {
        var pagePath = "/Pages/Resident/ResidentialTenantAddResponceStep4_Sign_Deposit.aspx/" + "/DocImageUpload";
        // var CurRow = G_CurRow;

        var DocId = $(G_CurRow).find('td:eq(0)').find('input[type="hidden"]').val();
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
            data: "{ 'Image':'" + ImageSave + "' , 'ImageName':'" + G_ImageName + "','DocId':'" + DocId + "','CurrentStatus':'Uploaded' }",
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
                        $(G_CurRow).find('td:eq(4)').find('label').text("Uploaded");
                        $((G_CurRow).find('td:eq(0)').find('input[type="checkbox"]')).prop("checked", true);
                        $($(G_CurRow).find('td:eq(3)').find('input[type="button"]')).attr("data_FilePath", obj.FilePath);
                        var nxtRow = $(G_CurRow).next();
                        if (nxtRow !== 'undefined' || nxtRow !== '' || nxtRow !== null) {
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
                        $("#documentUpload").empty();
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

//----- Signature -----------//
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
function BindSecurity(result) {

    let content = "";
    if (result.length > 0) {
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

    var pagePath = "/Pages/Resident/ResidentialTenantRental_App_Page_4.aspx/" + "/Delete";
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
                    if (mass != null) {
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
function GetSignatureObject(parameters) {
    var obj = {
        Id: $("#hdId").val(),
        SignatureName: $("#txtSign").val(),
        SecurityNo: $("#txtSecurity").val(),
        AddingDate: $("#txtDate").val()
    }
    return obj;
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
$(document).on('click', '#btnTenantSubmit', function (parameters) {
    
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

                    $("#btnAddSignature").val("Add Another Agreement Signer");
                } else {
                    notify('danger', "Signature Added Failed !!");
                }
                //BindEmergencyContactToField($.parseJSON(decodeURIComponent(data.d)));
            }
        });
    }
});

function LoadTenantFee(parameters) {
    
    var URL = "/Pages/Resident/ResidentialTenantAddResponceStep4_Sign_Deposit.aspx/" + "GetTenantFee";
    var obj = {};
    let Basic = makeAjaxCallReturnPromiss(URL, obj);
    Basic.then((data) => {
        if (data.d != null || data.d != "") {
            
            var step1obj = $.parseJSON(decodeURIComponent(data.d));
            if (step1obj != null)
            {
                $('input[name=CashOrCheck]').closest('div').removeClass('checked');
                $('input[name=CashOrCheck]').attr('checked', false);
                $("input[name=CashOrCheck][value='" + step1obj.AccountType + "']").closest('div').addClass('checked');
                $("input[name=CashOrCheck][value='" + step1obj.AccountType + "']").attr('checked', true);

                $("#txtSecurityDeposite").val(step1obj.SecurityDeposit);
                $("#txtOneMonthRent").val(step1obj.MonthlyRent);
                $("#txtProrateAmount").val(step1obj.ProrateAmount);
                $("#txtFirtMonthRent").val(step1obj.FirstMonthRent);
                $("#txtTotalDue").val(step1obj.Total);

              
                 $("#nameAccountapp1Txt").val(step1obj.AccountName);
                    $("#addressapp1Txt1").val(step1obj.Address1);
                    $("#addressapp1Txt2").val(step1obj.Address2);
                    $("#ddlCountry").val(step1obj.CountryId).trigger('change');
                    $("#ddlRegion").val(step1obj.RegionName).trigger('change');
                    $("#cityapp1Txt").val(step1obj.CityName);
                    $("#ddlstateapp").val(step1obj.StateId).trigger('change');
                    $("#zipcodeapp1Txt").val(step1obj.ZipCode);

                if (step1obj.AccountType == 'Check') {                 
                   
                    $("#routingnumapp1Txt").val(step1obj.RoutingNumber);
                    $("#checkacctnumapp1Txt").val(step1obj.AccountNumber);
                    $("#rerountingnumapp1Txt").val(step1obj.RoutingNumber);
                    $("#recheckacctnumapp1Txt").val(step1obj.AccountNumber);

                    $("#subTotalCharge").val(step1obj.SubTotalCharge);
                    $("#percentRatio").val(step1obj.CheckingAccountProcessingFee);
                    $("#TotalAmountCharge").val(step1obj.TotalAmountCharge);

                    $("#tblChecking").css({ 'display': 'block' });
                    $("#tblCheckingDiv").css({ 'display': 'block' });
                    $("#tblCash").css({ 'display': 'none' });


                } else if (step1obj.AccountType == 'Cash') {
                    $("#txtPersoneName").val(step1obj.PersonName);
                    $("#PersonLastCreitCard").val(step1obj.PersonLast4CreditCardNumber);
                    $("#txtCompanyName").val(step1obj.CompanyName);
                    $("#txtAmountOff").val(step1obj.CashAmount);

                    $("#txtdateFrom").val(step1obj.CashAmountPayDate);
                    $("#txtLocation").val(step1obj.Location);
        
                    $("#txtOwnerSignature").val(step1obj.PersonName);
                    $("#txtCreditCardLast4").val(step1obj.PersonLast4CreditCardNumber);

                    $("#tblChecking").css({ 'display': 'none' });
                    $("#tblCheckingDiv").css({ 'display': 'none' });
                    $("#tblCash").css({ 'display': 'block' });

                } 
            }
        }
    });
}

//-------------- Owner Signature --------//
function GelAllOwnerInfo(parameters) {
    var obj = {}
    var URL = "/Pages/Resident/ResidentialTenantAddResponceStep4_Sign_Deposit.aspx/" + "GetAllOwner";
    let Basic = makeAjaxCallReturnPromiss(URL, obj);
    Basic.then((data) => {
        if (data.d != null || data.d != "") {
            var res = $.parseJSON(decodeURIComponent(data.d));
            if (res != null) {
                BindOwner($.parseJSON(decodeURIComponent(data.d)));

                // $("#btnAddSignature").text("Add Another Aggrement Signer");
            } else {
                notify('danger', "Save Failed !!");
            }
            //BindEmergencyContactToField($.parseJSON(decodeURIComponent(data.d)));
        }
    });

}
function BindOwner(result) {

    let content = "";
    if (result.length > 0) {
        $.each(result, function (i, obj) {

            content += "<tr>";
            content += "<td> " + obj.OwnerSignatureName + "<input type='hidden' value='" + obj.Id + "'></td>";
            content += "<td>" + obj.SecurityNo + "</td>";
            content += "<td>" + obj.AddingDate + "</td>";
            content += "<td> <input type='button' class='btn btnNewColor' style='background-color: #3B5998' onclick='EditOwner(this)' id='" + obj.Id + "' data_Id='" + obj.Id + "' value='Edit' /></td>";
            content += "<td> <input type='button' class='btn btnNewColor' style='background-color: #3B5998' onclick='DeleteOwner(this)' id='" + obj.Id + "' data_Id='" + obj.Id + "' value='Delete' /></td>";
            content += "</tr>";
        });
        $("#tblOwnerInfo tbody").empty();
        $("#tblOwnerInfo tbody").append(content);
    }
}
function EditOwner(curtd) {

    var id = $(curtd).attr('Id');
    var trRow = $(curtd).closest('tr');
    var Sign = $(trRow).find('td:eq(0)').text();
    var Security = $(trRow).find('td:eq(1)').text();
    var Date = $(trRow).find('td:eq(2)').text();

    $("#hdOwnerId").val(id);
    $("#txtOwnerSig").val(Sign);
    $("#txtOwnerSecurity").val(Security);
    $("#txtOwnerDate").val(Date);
    $("#btnSaveOwner").val("Update");
}
function DeleteOwner(curtd) {
    var id = $(curtd).attr('Id');
    var pagePath = "/Pages/Resident/ResidentialTenantAddResponceStep4_Sign_Deposit.aspx/" + "/Delete";
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
                    if (mass != null) {
                        notify('success', "Owner Signature Deleted Successfully");
                        BindSecurity(mass);
                    } else {
                        notify('success', "Owner Signature Deleted Failed!!");
                    }

                    //GelAllSignatureInfo();

                    //setEquipmentData(lstofEquipmentData.RentalUnit);
                }

            }

    });
}
function GetOwnerObject(parameters) {
    var obj = {
        Id: $("#hdOwnerId").val(),
        OwnerSignatureName: $("#txtOwnerSig").val(),
        SecurityNo: $("#txtOwnerSecurity").val(),
        AddingDate: $("#txtOwnerDate").val()
    }
    return obj;
}
function validateSignature_Owner() {
    var isresult = true;
    var signature = $("#txtOwnerSig").val();
    var secutiry = $("#txtOwnerSecurity").val();
    var date = $("#txtOwnerDate").val();


    // var IsStillWorkthere = $("#StillWork").is(":checked") == true ? true : false;

    if (signature === "undefined" || signature === "") {
        $("#txtOwnerSig").css({ 'border': '1px solid red' });
        isresult = false;
    }
    else {
        $("#txtOwnerSig").css({ 'border': '1px solid #d2d6de' });
    }
    if (secutiry === "undefined" || secutiry === "") {
        $("#txtOwnerSecurity").css({ 'border': '1px solid red' });
        isresult = false;
    }
    else {
        $("#txtOwnerSecurity").css({ 'border': '1px solid #d2d6de' });
    }

    if (date === "undefined" || date === "") {
        $("#txtOwnerDate").css({ 'border': '1px solid red' });
        isresult = false;
    }
    else {
        $("#txtOwnerDate").css({ 'border': '1px solid #d2d6de' });
    }


    if (isresult)
        isresult = true;
    else {
        isresult = false;
    }
    return isresult;
}
$(document).on('click', '#btnSaveOwner', function (parameters) {
    
    if (validateSignature_Owner()) {
        var obj = GetOwnerObject();
        var URL = "/Pages/Resident/ResidentialTenantAddResponceStep4_Sign_Deposit.aspx/" + "SaveOwnerSecurityInfo";
        let Basic = makeAjaxCallReturnPromiss(URL, obj);
        Basic.then((data) => {
            if (data.d != null || data.d != "") {

                var res = $.parseJSON(decodeURIComponent(data.d));
                if (res != null) {
                    BindOwner(res);
                    if ($("#btnSaveOwner").val() == "Update") {
                        notify('success', "Owner Signature Updated Successfully");
                    } else {
                        notify('success', "Owner Signature Added Successfully");
                    }
                    $("#hdOwnerId").val(0);
                    $("#txtOwnerSig").val("");
                    $("#txtOwnerSecurity").val("");
                    $("#txtOwnerDate").val("");

                    $("#btnSaveOwner").val("Owner Submit");
                } else {
                    notify('danger', "Owner Signature Added Failed !!");
                }
                //BindEmergencyContactToField($.parseJSON(decodeURIComponent(data.d)));
            }
        });
    }
});

function Validationform() {
    var isresult = true;
    var AdditionalDoc = $("#tblDoc tbody tr").length;
    var SignatureCheck = $("#tblSignature tbody tr").length;
    var Owner = $("#tblOwnerInfo tbody tr").length;
    
    var txtSecurityDeposite= $("#txtSecurityDeposite").val();
    var txtOneMonthRent = $("#txtOneMonthRent").val();
    var  txtProrateAmount =$("#txtProrateAmount").val();
    var txtFirtMonthRent = $("#txtFirtMonthRent").val();
    var txtTotalDue = $("#txtTotalDue").val();

    var nameAccountapp1Txt = $("#nameAccountapp1Txt").val();
    var addressapp1Txt1 = $("#addressapp1Txt1").val();
    var addressapp1Txt2 = $("#addressapp1Txt2").val();
    var ddlCountry = $("#ddlCountry").val();
    var ddlRegion = $("#ddlRegion").val();
    var cityapp1Txt = $("#cityapp1Txt").val();
    var ddlstateapp = $("#ddlstateapp").val();
    var zipcodeapp1Txt = $("#zipcodeapp1Txt").val();


    var AccountType = $('input[name=CashOrCheck]:checked').val();
    var routingnumapp1Txt = $("#routingnumapp1Txt").val();
    var checkacctnumapp1Txt = $("#checkacctnumapp1Txt").val();


    var txtPersoneName = $("#txtPersoneName").val();
    var PersonLastCreitCard = $("#PersonLastCreitCard").val();
    var txtCompanyName = $("#txtCompanyName").val();
    var txtAmountOff = $("#txtAmountOff").val();
    var txtdateFrom = $("#txtdateFrom").val();
    var txtLocation = $("#txtLocation").val();
    var txtOwnerSignature = $("#txtOwnerSignature").val();
    var txtCreditCardLast4 = $("#txtCreditCardLast4").val();



    if (Owner == null || Owner == 0) {
        notify('danger', "Please Add Owner Signature");
        isresult = false;
    }

    if (SignatureCheck === "undefined" || SignatureCheck === "" || SignatureCheck == 0 || SignatureCheck == null) {
        notify('danger', "Please Add Signature Information");
        isresult = false;
    }

    if (AdditionalDoc != null || AdditionalDoc > 0) {
        $("#tblDoc tbody tr").each(function (parameters) {

            if ($($(this).find('td:eq(0)').find('input[type=checkbox]')).is(':checked')) {

            } else {
                isresult = false;

                notify('danger', "All the Additional documents Status must be Uploaded or Downloaded");
                return false;
            }
        });
    }



    if (txtSecurityDeposite === "undefined" || txtSecurityDeposite === "") {
        $("#txtSecurityDeposite").css({ 'border': '1px solid red' });
        isresult = false;
    }
    else {
        $("#txtSecurityDeposite").css({ 'border': '1px solid #d2d6de' });
    }
    if (txtOneMonthRent === "undefined" || txtOneMonthRent === "") {
        $("#txtOneMonthRent").css({ 'border': '1px solid red' });
        isresult = false;
    }
    else {
        $("#txtOneMonthRent").css({ 'border': '1px solid #d2d6de' });
    }
    if (txtProrateAmount === "undefined" || txtProrateAmount === "") {
        $("#txtProrateAmount").css({ 'border': '1px solid red' });
        isresult = false;
    }
    else {
        $("#txtProrateAmount").css({ 'border': '1px solid #d2d6de' });
    }
    if (txtFirtMonthRent === "undefined" || txtFirtMonthRent === "") {
        $("#txtFirtMonthRent").css({ 'border': '1px solid red' });
        isresult = false;
    }
    else {
        $("#txtFirtMonthRent").css({ 'border': '1px solid #d2d6de' });
    }
    if (txtTotalDue === "undefined" || txtTotalDue === "") {
        $("#txtTotalDue").css({ 'border': '1px solid red' });
        isresult = false;
    }
    else {
        $("#txtTotalDue").css({ 'border': '1px solid #d2d6de' });
    }
    
    if (nameAccountapp1Txt === "undefined" || nameAccountapp1Txt === "") {
        $("#nameAccountapp1Txt").css({ 'border': '1px solid red' });
        isresult = false;
    }
    else {
        $("#nameAccountapp1Txt").css({ 'border': '1px solid #d2d6de' });
    }
    if (addressapp1Txt1 === "undefined" || addressapp1Txt1 === "") {
        $("#addressapp1Txt1").css({ 'border': '1px solid red' });
        isresult = false;
    }
    else {
        $("#addressapp1Txt1").css({ 'border': '1px solid #d2d6de' });
    }

    if (ddlCountry === "undefined" || ddlCountry === "-1") {
        $("#s2id_ddlCountry").css({ 'border': '1px solid red' });
        isresult = false;
    }
    else {
        $("#s2id_ddlCountry").css({ 'border': '1px solid #d2d6de' });
    }
    //if (ddlRegion === "undefined" || ddlRegion === "") {
    //    $("#ddlRegion").css({ 'border': '1px solid red' });
    //    isresult = false;
    //}
    //else {
    //    $("#ddlRegion").css({ 'border': '1px solid #d2d6de' });
    //}
    if (cityapp1Txt === "undefined" || cityapp1Txt === "") {
        $("#cityapp1Txt").css({ 'border': '1px solid red' });
        isresult = false;
    }
    else {
        $("#cityapp1Txt").css({ 'border': '1px solid #d2d6de' });
    }
    if (ddlstateapp === "undefined" || ddlstateapp === "-1") {
        $("#s2id_ddlstateapp").css({ 'border': '1px solid red' });
        isresult = false;
    }
    else {
        $("#s2id_ddlstateapp").css({ 'border': '1px solid #d2d6de' });
    }
    if (zipcodeapp1Txt === "undefined" || zipcodeapp1Txt === "") {
        $("#zipcodeapp1Txt").css({ 'border': '1px solid red' });
        isresult = false;
    }
    else {
        $("#zipcodeapp1Txt").css({ 'border': '1px solid #d2d6de' });
    }


    if (AccountType == 'Check') {
       

        //routingnumapp1Txt
        if (routingnumapp1Txt === "undefined" || routingnumapp1Txt === "") {
            $("#routingnumapp1Txt").css({ 'border': '1px solid red' });
            isresult = false;
        }
        else {
            $("#routingnumapp1Txt").css({ 'border': '1px solid #d2d6de' });
        }
        if (checkacctnumapp1Txt === "undefined" || checkacctnumapp1Txt === "") {
            $("#checkacctnumapp1Txt").css({ 'border': '1px solid red' });
            isresult = false;
        }
        else {
            $("#checkacctnumapp1Txt").css({ 'border': '1px solid #d2d6de' });
        }
    } else {

        if (txtPersoneName === "undefined" || txtPersoneName === "") {
            $("#txtPersoneName").css({ 'border': '1px solid red' });
            isresult = false;
        }
        else {
            $("#txtPersoneName").css({ 'border': '1px solid #d2d6de' });
        }
        if (PersonLastCreitCard === "undefined" || PersonLastCreitCard === "") {
            $("#PersonLastCreitCard").css({ 'border': '1px solid red' });
            isresult = false;
        }
        else {
            $("#PersonLastCreitCard").css({ 'border': '1px solid #d2d6de' });
        }
        if (txtCompanyName === "undefined" || txtCompanyName === "") {
            $("#txtCompanyName").css({ 'border': '1px solid red' });
            isresult = false;
        }
        else {
            $("#txtCompanyName").css({ 'border': '1px solid #d2d6de' });
        }
        if (txtAmountOff === "undefined" || txtAmountOff === "") {
            $("#txtAmountOff").css({ 'border': '1px solid red' });
            isresult = false;
        }
        else {
            $("#txtAmountOff").css({ 'border': '1px solid #d2d6de' });
        }
        if (txtdateFrom === "undefined" || txtdateFrom === "") {
            $("#txtdateFrom").css({ 'border': '1px solid red' });
                isresult = false;
            }
        else {
                $("#txtdateFrom").css({ 'border': '1px solid #d2d6de' });
            }
        if (txtLocation === "undefined" || txtLocation === "") {
            $("#txtLocation").css({ 'border': '1px solid red' });
            isresult = false;
        }
        else {
            $("#txtLocation").css({ 'border': '1px solid #d2d6de' });

        }

        if (txtOwnerSignature === "undefined" || txtOwnerSignature === "") {
            $("#txtOwnerSignature").css({ 'border': '1px solid red' });
            isresult = false;
        }
        else {
            $("#txtOwnerSignature").css({ 'border': '1px solid #d2d6de' });

        }
        if (txtCreditCardLast4 === "undefined" || txtCreditCardLast4 === "") {
            $("#txtCreditCardLast4").css({ 'border': '1px solid red' });
            isresult = false;
        }
        else {
            $("#txtCreditCardLast4").css({ 'border': '1px solid #d2d6de' });
        }
        
    }

    if (isresult)
        isresult = true;
    else {
        isresult = false;
    }
    return isresult;
}


function GetSavedObjForSave(creditType) {
    
    var obj = {
        SecurityDeposit: $("#txtSecurityDeposite").val(),
        MonthlyRent: $("#txtOneMonthRent").val(),
        ProrateAmount: $("#txtProrateAmount").val(),
        FirstMonthRent: $("#txtFirtMonthRent").val(),
        Total: $("#txtTotalDue").val(),
        AccountName: $("#nameAccountapp1Txt").val(),
        Address1: $("#addressapp1Txt1").val(),
        Address2: $("#addressapp1Txt2").val(),
        CountryName: $("#ddlCountry option.selected").text(),
        CountryId: $("#ddlCountry").val(),
        RegionName: $("#ddlRegion").val(),
        CityName: $("#cityapp1Txt").val(),
        StateName: $("#ddlstateapp option.selected").text(),
        StateId: $("#ddlstateapp").val(),
        ZipCode: $("#zipcodeapp1Txt").val(),

        AccountType: creditType,     
        RoutingNo: $("#routingnumapp1Txt").val(),
        AccountNumber: sLast4,
        TransactionCode: sResponseCode,
        AuthorizationCode: sResponseCode,
        TransactionDescription: sResponseDetails,
        RentalFeeAgrement: $("#RentalAmount").is(':checked') ? true : false,
        SubTotalCharge: $("#subTotalCharge").text(),
        //CheckingAccountProcessingFee: $("#percentRatio").text().replace('%',''),
        CheckingAccountProcessingFee: creditType == 'Check' ? $("#Amount").text() : '0',
        TotalAmountCharge: creditType == 'Check' ? $("#TotalAmountCharge").text() : $("#txtAmountOff").val(),
        
        PersonName: $("#txtPersoneName").val(),
        PersonLast4CreditCardNumber: creditType == 'Check' ? '' : $("#PersonLastCreitCard").val(),
        CompanyName: creditType == 'Check' ? '' : $("#txtCompanyName").val(),
        CashAmount: creditType == 'Check' ? '' : $("#txtAmountOff").val(),
        CashAmountPayDate: creditType == 'Check' ? '' : $("#txtdateFrom").val(),
        Location: creditType == 'Check' ? '' : $("#txtLocation").val(),
        ApplicationId: $("#ApplicationId").text()
   
    }

    return obj;
    
}

function SaveTransaction(creditType) {
    
    var obj = GetSavedObjForSave(creditType);
        var URL = "/Pages/Resident/ResidentialTenantAddResponceStep4_Sign_Deposit.aspx/" + "SaveAndContinue";
        let Basic = makeAjaxCallReturnPromiss(URL, obj);
        Basic.then((data) => {
            if (data.d != null || data.d != "") {
                var res = $.parseJSON(decodeURIComponent(data.d));
                if (res != null) {
                    if (res == true) {
                        window.location.href = "/Pages/Resident/ResidentTenantDashboard.aspx";
                    }
                } else {
                    notify('danger', "Save Failed !!");
                }
                
            }
        });
}

function oncallback(e) {
    
    var creditType = $('input[name=CashOrCheck]:checked').val();
    var validate = true;   

    if (creditType === "undefined" || creditType === "") {
            validate = false;
        }
    else {
        validate = Validationform();
    }

    if (validate == true && creditType == 'Check') {

        var response = JSON.parse(e.data);
        switch (response.event) {

            case 'begin':

                //call to forte checkout is successful
                break;

            case 'success':

                //transaction successful

                isCheckoutSuccessful = true;
                sResponseCode = response.authorization_code;
                sResponseDetails = response.response_description;
                sApplicationFee = response.total_amount;
                sLast4 = response.last_4;
                if (response.method_used == "echeck") {
                    sExpMonth = "";
                    sExpYear = "";
                }
                else {
                    sExpMonth = response.expire_month;
                    sExpYear = response.expire_year;
                }

                SaveTransaction(creditType);

                break;

            case 'failure':

                //handle failed transaction

                alert('sorry, transaction failed. failed reason is ' + response.response_description);

                isCheckoutSuccessful = false;
                sResponseCode = response.response_code;
                sResponseDetails = response.response_description;

        }

    }

   
}


//$(document).on('click', '#btnSaveAndContinue', function (parameters) {
    
//    if (Validationform()) {
//        var obj = GetSavedObjForSave();
//        var URL = "/Pages/Resident/ResidentialTenantAddResponceStep4_Sign_Deposit.aspx/" + "SaveAndContinue";
//        let Basic = makeAjaxCallReturnPromiss(URL, obj);
//        Basic.then((data) => {
//            if (data.d != null || data.d != "") {
//                var res = $.parseJSON(decodeURIComponent(data.d));
//                if (res != null) {
//                    if (res == true) {
//                        window.location.href = "/Pages/Resident/ResidentTenantDashboard.aspx";
//                    }
//                } else {
//                    notify('danger', "Save Failed !!");
//                }
//                //BindEmergencyContactToField($.parseJSON(decodeURIComponent(data.d)));
//            }
//        });
//    }
//});

$(document).on('ifChanged', "input[type=radio][name=CashOrCheck]", function (parameters) {

    if ($(this).attr('id') == 'Checking') {
        $("#tblChecking").css({ 'display': 'block' });//tblCheckingDiv
        $("#tblCheckingDiv").css({ 'display': 'block' });//tblCheckingDiv
        $("#tblCash").css({ 'display': 'none' });
        // $("#tblCredit tbody").css({ 'width': '100% !important' });

        if (bShow === "False") {
            $("#btnSubmit").css({ 'display': 'none' });
            $("#btnSubmitCash").css({ 'display': 'none' });
        }
        else {
            $("#btnSubmit").css({ 'display': 'block' });
            $("#btnSubmitCash").css({ 'display': 'none' });
        }

    }
    else if ($(this).attr('id') == 'Cash') {
        $("#tblChecking").css({ 'display': 'none' });//tblCheckingDiv
        $("#tblCheckingDiv").css({ 'display': 'none' });//tblCheckingDiv
        $("#tblCash").css({ 'display': 'block' });
        if (bShow === "False") {
            $("#btnSubmit").css({ 'display': 'none' });
            $("#btnSubmitCash").css({ 'display': 'none' });
        }
        else {
            $("#btnSubmit").css({ 'display': 'none' });
            $("#btnSubmitCash").css({ 'display': 'block' });
        }
    }
    else {
        $("#tblChecking").css({ 'display': 'block' });//tblCheckingDiv
        $("#tblCheckingDiv").css({ 'display': 'block' });//tblCheckingDiv
        $("#tblCash").css({ 'display': 'none' });

        if (bShow === "False") {
            $("#btnSubmit").css({ 'display': 'none' });
            $("#btnSubmitCash").css({ 'display': 'none' });
        }
        else {
            $("#btnSubmit").css({ 'display': 'block' });
            $("#btnSubmitCash").css({ 'display': 'none' });
        }

    }

   

    var res = "";
    if ($(this).attr('id') == 'Checking') {
        res = "echeck";
        cardType = "Check";
        clearAllPaymentField();
    }
    else if ($(this).attr('id') == 'Cash') {
        cardType = "Cash";
        clearAllPaymentField();
    }
    else {
        res = "echeck";
        cardType = "Check";
        clearAllPaymentField();
    }

    $(".btnPaymentGetway").attr("allowed_methods", res);

});

$(document).on('keyup', '#rerountingnumapp1Txt', function (parameters) {
    if ($("#routingnumapp1Txt").val() === $(this).val()) {
        $("#rerountingnumapp1Txt").css({ "border": "1px solid Green" });
    } else {
        $("#rerountingnumapp1Txt").css({ "border": "1px solid red" });
    }
});

$(document).on('keyup', '#recheckacctnumapp1Txt', function (parameters) {
    if ($("#checkacctnumapp1Txt").val() === $(this).val()) {
        $("#recheckacctnumapp1Txt").css({ "border": "1px solid Green" });
    } else {
        $("#recheckacctnumapp1Txt").css({ "border": "1px solid red" });
    }
});

//$(document).on('click', '#btnSubmit', function (parameters) {
    
//    if (Validationform()) {
//        var obj = GetSavedObjForSave();
//        var URL = "/Pages/Resident/ResidentialTenantAddResponceStep4_Sign_Deposit.aspx/" + "SaveAndContinue";
//        let Basic = makeAjaxCallReturnPromiss(URL, obj);
//        Basic.then((data) => {
//            if (data.d != null || data.d != "") {
//                var res = $.parseJSON(decodeURIComponent(data.d));
//                if (res != null) {
//                    if (res == true) {
//                        window.location.href = "/Pages/Resident/ResidentTenantDashboard.aspx";
//                    }
//                } else {
//                    notify('danger', "Save Failed !!");
//                }
//                //BindEmergencyContactToField($.parseJSON(decodeURIComponent(data.d)));
//            }
//        });
//    }
//});

$(document).on('click', '#btnSubmitCash', function (parameters) {
    
    var creditType = $('input[name=CashOrCheck]:checked').val();
    if (Validationform()) {
        var obj = GetSavedObjForSave(creditType);
        var URL = "/Pages/Resident/ResidentialTenantAddResponceStep4_Sign_Deposit.aspx/" + "SaveAndContinue";
        let Basic = makeAjaxCallReturnPromiss(URL, obj);
        Basic.then((data) => {
            if (data.d != null || data.d != "") {
                var res = $.parseJSON(decodeURIComponent(data.d));
                if (res != null) {
                    if (res == true) {
                        window.location.href = "/Pages/Resident/ResidentTenantDashboard.aspx";
                    }
                } else {
                    notify('danger', "Save Failed !!");
                }
                //BindEmergencyContactToField($.parseJSON(decodeURIComponent(data.d)));
            }
        });
    }
});

$(document).on('click', '#btnExit', function (parameters) {
    
    var origin = window.location.origin; // Returns base URL
    var url = origin + "/Pages/DashboardAdmin.aspx";
    window.location.href = url;
});

$(document).on('click', '#btnCancel', function (parameters) {
    var origin = window.location.origin; // Returns base URL
    var url = origin + "/Pages/DashboardAdmin.aspx";
    window.location.href = url;
});

$(document).on("keyup", ".nameAccountapp1Txt", function (parameters) {
    var creditType = $('input[name=CashOrCheck]:checked').val();
    var accName = "";
    if (creditType == "Check") {
        accName = $("#nameAccountapp1Txt").val();
    } 
    $(".btnPaymentGetway").attr("billing_name", accName);
});

$(document).on("keyup", "#addressapp1Txt1", function (parameters) {
    
    var compName = $(this).val();
    $(".btnPaymentGetway").attr("billing_street_line1", compName);
});

$(document).on("keyup", "#cityapp1Txt", function (parameters) {
    
    var compName = $(this).val();
    $(".btnPaymentGetway").attr("billing_locality", compName);
});
$(document).on("change", "#ddlstateapp", function (parameters) {
    
    var compName = $("#ddlstateapp").val(); //$("#ddlstateapp option").text();
    $(".btnPaymentGetway").attr("billing_region", compName);
});
$(document).on("keyup", "#zipcodeapp1Txt", function (parameters) {
    
    var compName = $(this).val();
    $(".btnPaymentGetway").attr("billing_postal_code", compName);
});

$(document).on("keyup", "#routingnumapp1Txt", function (parameters) {
    
    var compName = $(this).val();
    $(".btnPaymentGetway").attr("routing_number", compName);
});

//$(document).on("keyup", "#rerountingnumapp1Txt", function (parameters) {
//    
//    var compName = $(this).val();
//    $(".btnPaymentGetway").attr("routing_number", compName);
//});

$(document).on("keyup", "#checkacctnumapp1Txt", function (parameters) {
    
    var compName = $(this).val();
    $(".btnPaymentGetway").attr("account_number", compName);
});
$(document).on("keyup", "#recheckacctnumapp1Txt", function (parameters) {
    
    var compName = $(this).val();
    $(".btnPaymentGetway").attr("account_number2", compName);
});

//$(document).on("keyup", "#txtTotalDue", function (parameters) {
//    
//    var compName = $(this).val();
//    if (compName.length >= 0) {
//        $(".btnPaymentGetway").attr("total_amount", compName);
//    }

//});

function clearAllPaymentField() {
    $(".btnPaymentGetway").attr("billing_company_name", "");   
    $(".btnPaymentGetway").attr("billing_name", "");
    $(".btnPaymentGetway").attr("billing_street_line1", "");
    $(".btnPaymentGetway").attr("billing_locality", "");
    $(".btnPaymentGetway").attr("billing_region", "");
    $(".btnPaymentGetway").attr("billing_postal_code", "");
    $(".btnPaymentGetway").attr("routing_number", "");
    $(".btnPaymentGetway").attr("account_number", "");
    $(".btnPaymentGetway").attr("account_number2", "");
    $(".btnPaymentGetway").attr("allowed_methods", "echeck");
}


function LoadRentalDocumentGrid() {
    var pagePath = window.location.pathname + "/LoadRentalDocument";
    var obj = {};
    var unitSerialId = $.trim($("[id*=txtUnitID]").val());
    obj.unitSerialId = unitSerialId;

    $.ajax({
        type: "POST",
        url: pagePath,
        data: JSON.stringify(obj),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        error:
            function (XMLHttpRequest, textStatus, errorThrown) {
                alert("Error in Loading Document Grid");
            },
        success:
            function (result) {

                var lstofRentalDocumentData = $.parseJSON(decodeURIComponent(result.d));
                var content = "";
                if (result.d == '') {

                } else {

                    if (lstofRentalDocumentData.length > 0) {
                        $.each(lstofRentalDocumentData, function (i, obj) {

                            content += "<tr>";
                            content += "<td style='width:15%'><button data_RowIdAdd='" + obj.Id + "' type='button' class='btnAdd'>" + G_buttonAdd + "</button><button data_RowIdRem='" + obj.Id + "' type='button' class='btnRemove'>" + G_buttonRmv + "</button></td>";
                            content += "<td style='width:20%'> <textarea  data_RowId='" + obj.Id + "' id='txtDocumentDesc' rows='3' cols='5' class='form-control'>" + obj.DocumentDescription + "</textarea></td>";
                            content += "<td style='width:15%'><input  type='text' id='RentalDocFileName' class='form-control' value='" + obj.FileName.split('.')[0] + "'></td>"; //DateAdded
                            content += "<td style='width:30%'> <input style='float: left;width: 74%;'  type='file' class='form-control' id='fileRentalDocument' /> <input  type='button' style='width: 24%;margin-left: 4px;margin-top: 5px;' value='Upload' data_RowId='" + obj.Id + "' class='btn btnNewColor' onclick='SaveRentalImage(this);' /></td>"; //DateAdded
                            content += "<td style='width:15%'><select class='form-control ddl browsStatus' >" + LoadBrowsStatus(obj.IsViewedOrDownloaded, obj.Id, obj.FilePath) + "</select><a href='#' style='display:none;' class='download' download></a></td>"; //
                            content += "<td style='width:5%'><input type='button' value='Delete' onclick='DeleteRentalDocument(this)' id='" + obj.Id + "' class='custombtn'/></td>";
                            content += "</tr>";
                        });
                    } else {
                        // $.each(lstofEquipmentData, function (i, obj) {

                        content += "<tr>";
                        content += "<td style='width:15%'><button data_RowIdAdd='0' type='button' class='btnAdd'>" + G_buttonAdd + "</button><button data_RowIdRem='0' type='button' class='btnRemove'>" + G_buttonRmv + "</button></td>";
                        content += "<td style='width:20%'> <textarea  data_RowId='0' id='txtDocumentDesc' rows='3' cols='5' class='form-control'></textarea></td>";
                        content += "<td style='width:15%'><input type='text' id='RentalDocFileName' class='form-control' value=''></td>";//DateAdded
                        content += "<td style='width:35%'> <input  type='file' style='float: left;width: 74%;' style='width: 230px;float: left;' class='form-control' id='fileRentalDocument' /> <input type='button' style='margin-top:2px;' value='Upload' data_RowId='0' style='width: 24%;margin-left: 4px;margin-top: 5px;' class='btn btnNewColor' onclick='SaveRentalImage(this);' /></td>";//DateAdded
                        content += "<td style='width:15%'><select class='form-control ddl browsStatus' >" + LoadBrowsStatusWithOutValue(0) + "</select></td>";//
                        content += "<td style='width:5%'><input disabled type='button' value='Delete' onclick='DeleteRentalDocument(this)' id='" + obj.Id + "' class='custombtn'/></td>";
                        content += "</tr>";
                        // });
                    }

                    $("#tblDocumentFileList tbody").empty();
                    $("#tblDocumentFileList tbody").append(content);
                    $(".browsStatus").select2();
                    //setEquipmentData(lstofEquipmentData.RentalUnit);
                }

            }

    });
}
function LoadBrowsStatus(IsViewedOrDownloaded, rowId, filePath) {
    var content = '<option data_Imagepath="' + filePath + '" data_RowId="' + rowId + '" value="-1">Select........</option>';

    var sView = "", sDownload = "";
    if (IsViewedOrDownloaded === "View") {
        sView = "selected";
    } else if (IsViewedOrDownloaded === "Download") {
        sDownload = "selected";
    } else {

    }
    content += '<option ' + sView + ' data_Imagepath="' + filePath + '"  data_RowId="' + rowId + '" value="View">View</option>';
    content += '<option ' + sDownload + ' data_Imagepath="' + filePath + '"  data_RowId="' + rowId + '" value="Download">Download</option>';
    return content;

}
function LoadBrowsStatusWithOutValue(rowId) {
    var content = '<option data_RowId="' + rowId + '" value="-1">Select........</option>';

    content += '<option data_Imagepath=""  data_RowId="' + rowId + '" value="View">View</option>';
    content += '<option data_Imagepath=""  data_RowId="' + rowId + '"  value="Download">Download</option>';
    return content;

}
$(document).off('click', '.btnAdd').on('click', '.btnAdd', function () {
    //if (g_IsEdit == false) {

    var curentRow = $(this).closest('tr');
    var content = "";
    var checkitemsave = parseInt($(curentRow).find('td:eq(4)').find('select option').attr("data_RowId"));

    if (checkitemsave > 0) {
        content += "<tr>";
        content += "<td style='width:15%'><button data_RowIdAdd='0' type='button' class='btnAdd'>" + G_buttonAdd + "</button><button data_RowIdRem='0' type='button' class='btnRemove'>" + G_buttonRmv + "</button></td>";
        content += "<td style='width:20%'> <textarea  data_RowId='0' id='txtDocumentDesc' rows='3' cols='5' class='form-control'></textarea></td>";
        content += "<td style='width:15%'><input type='text' id='RentalDocFileName' class='form-control' value=''></td>";//DateAdded
        content += "<td style='width:35%'> <input  type='file' style='float: left;width: 74%;'  class='form-control fileRentalDocument' id='fileRentalDocument' /> <input type='button' style='width: 24%;margin-left: 4px;margin-top: 5px;' value='Upload' data_RowId='0' class='btn btnNewColor' onclick='SaveRentalImage(this);' /></td>";//DateAdded
        content += "<td style='width:15%'><select id='browsStatus' class='form-control ddl browsStatus' ><option data_RowId='0' value=-1'>Select........</option>" +
            "<option data_Imagepath=''  data_RowId='0' value='View'>View</option>" +
            "<option data_Imagepath=''  data_RowId='0'  value='Download'>Download</option>" +
            "</select></td>";//
        content += "<td style='width:5%'><input disabled type='button' value='Delete' onclick='DeleteRentalDocument(this)' class='custombtn'/></td>";
        content += "</tr>";
        $("#tblDocumentFileList tbody").append(content);
        $("#browsStatus").select2();
    }
    else {
        notify("danger", "Please Browse and save a file first");
    };

});
$(document).off('click', '.btnRemove').on('click', '.btnRemove', function () {
    var index = $(this).closest('tr').index();
    var curentRow = $(this).closest('tr');
    var content = "";
    var checkitemsave = parseInt($(curentRow).find('td:eq(4)').find('select option').attr("data_RowId"));
    if (index !== 0) {
        if (checkitemsave > 0) {
            notify("danger", "This Document is already saved and can not removed, you can update it");
        } else {
            $(this).closest('tr').remove();
        }

    }

});
