var base = "~";
var DataUrl = "/Pages/Admin/AddResidentialUnit.aspx";

var pathname = window.location.pathname; // Returns path only
var url = window.location.href;     // Returns full URL
var origin = window.location.origin;   // Returns base URL
$(document).ready(function () {
    $('input[type="checkbox"].flat-red, input[type="radio"].flat-red').iCheck({
        checkboxClass: 'icheckbox_flat-green',
        radioClass: 'iradio_flat-green'
    });
    $("#txtdateFrom").datepicker({
        dateFormat: "mm-dd-yy",
        changeYear: false,
        changeMonth: true
    });
    $("#txtDate").datepicker({
        dateFormat: "mm-dd-yy",
        changeYear: false,
        changeMonth: true
    });
    $("#txtOwnerDate").datepicker({
        dateFormat: "mm-dd-yy",
        changeYear: false,
        changeMonth: true
    });
    //txtDate  txtOwnerDate
    LoadComboBox();
    LoadDeposite();
    GelAllOwnerInfo();
    $("#myModalForRegistraton").css("display", "block");
    LoadCaptcha();

});
var CaptchaNumber;
function LoadCaptcha(parameters) {
    $("#btnGetCaptcha").prop("disabled", true);
    CaptchaNumber = 0;
     CaptchaNumber = Math.floor(Math.random() * 10000);
     $("#divGenerateRandomValues").css({ "background-image": 'url(../../Images/CaptchaBG.JPG)', 'width': '150px', 'height': '60px','margin-left':'29%' });
            $("#divGenerateRandomValues").html("<input id='txtNewInput'></input>");  
            $("#txtNewInput").css({ 'background': 'transparent', 'font-family': 'Arial', 'font-style': 'bold', 'font-size': '40px' });  
            $("#txtNewInput").css({ 'width': '100px', 'border': 'none', 'color': 'black' });  
            $("#txtNewInput").val(CaptchaNumber);
            $("#txtNewInput").prop('disabled', true);  
  
//            $("#btnGetCaptcha").click(function () {  
//if ($("#textInput").val() != iNumber) {  
//alert("Wrong Input!");  
//                }  
//else {  
//alert("Correct Input!!!");  
//                }  
//            });  
//var  wrongInput = function () {  
//if ($("#textInput").val() != iNumber) {  
//return true;  
//                }  
//else {  
//return false;  
//                }  
//            };  
//            $("#textInput").bind('input', function () {                  
//                $("#btnGetCaptcha").prop('disabled', wrongInput);  
    //            });


            
}

$(document).on('keyup', '#txtRePassword', function (parameters) {
    if ($("#txtEnterPass").val() === $(this).val()) {
        $("#txtRePassword").css({ "border": "1px solid Green" });
    } else {
        $("#txtRePassword").css({ "border": "1px solid red" });
    }
});


//jQuery.validator.setDefaults({
//    debug: true,
//    success: "valid"
//});
//$("#myModalForRegistraton").validate({
//    rules: {
//        password: "required",
//        password_again: {
//            equalTo: "#password"
//        }
//    }
//});

$(document).on('click', '#btnRefress', function (parameters) {
    LoadCaptcha();
});
$(document).on('click', '#btnLogin', function (parameters) {
    if ($("#txtCap").val() == CaptchaNumber) {
        if (validateLogin()) {
            Login();
        }
    } else {
        notify('danger', "Worng Captcha!");
    }
});

function Login(parameters) {
    var URL = "/Pages/Resident/ResidentialTenantAddResponceStep4_Sign_Deposit_Login.aspx/" + "GetLogin";
    var obj = {
        EmailId: $("#txtTenantEmail").val(),
        ApprovalCode: $("#txtApprovalCode").val(),
        Password: $("#txtEnterPass").val()
    }
    let Basic = makeAjaxCallReturnPromiss(URL, obj);
    Basic.then((data) => {
        if (data.d != null || data.d != "") {
            
          
            var Tenant = $.parseJSON(decodeURIComponent(data.d));
            if (Tenant != null && Tenant != "") {
                window.location.href = "/Pages/Resident/ResidentialTenantAddResponceStep4_Sign_Deposit.aspx";
            } else {
                notify('danger', "Incorrect Email address or Approval Code");
            }
           // BindTenantDepositeAmount(deposit[0].getobj, deposit[0].typeChck, deposit[0].percentOrFlatVal);
            //BindCheckProcesingFee(deposit[0].getobj, deposit[0].typeChck, deposit[0].percentOrFlatVal);
            //BindBasicTenantInformationGrid($.parseJSON(decodeURIComponent(data.d)));
        }
        else {
            notify('danger', "Incorrect Email address or Approval Code");
        }
    });
}

function validateLogin(parameters) {
    var isresult = true;
    var email = $("#txtTenantEmail").val();
    var code = $("#txtApprovalCode").val();
    var pass = $("#txtEnterPass").val();
    var Repass = $("#txtRePassword").val();
    if (email === "undefined" || email === "") {
        $("#txtTenantEmail").css({ 'border': '1px solid red' });
        isresult = false;
    }
    else {
        $("#txtTenantEmail").css({ 'border': '1px solid #d2d6de' });
    }
    if (code === "undefined" || code === "") {
        $("#txtApprovalCode").css({ 'border': '1px solid red' });
        isresult = false;
    }
    else {
        $("#txtApprovalCode").css({ 'border': '1px solid #d2d6de' });
    }

    if (pass === "undefined" || pass === "") {
        $("#txtEnterPass").css({ 'border': '1px solid red' });
        isresult = false;
    }
    else {
        $("#txtEnterPass").css({ 'border': '1px solid #d2d6de' });
    }

    if (Repass === "undefined" || Repass === "") {
        $("#txtRePassword").css({ 'border': '1px solid red' });
        isresult = false;
    }
    else {
        $("#txtRePassword").css({ 'border': '1px solid #d2d6de' });
    }
    if (pass !== Repass) {
        $("#txtRePassword").css({ 'border': '1px solid red' });
        isresult = false;
    } else {
        $("#txtRePassword").css({ 'border': '1px solid green' });
    }

    if (isresult)
        isresult = true;
    else {
        isresult = false;
    }
    return isresult;
}

$(document).on('ifChanged', "input[type=radio][name=CashOrCheck]", function (parameters) {

    if ($(this).attr('id') == 'Checking') {
        $("#tblChecking").css({ 'display': 'block' });//tblCheckingDiv
        $("#tblCheckingDiv").css({ 'display': 'block' });//tblCheckingDiv
        $("#tblCash").css({ 'display': 'none' });
        // $("#tblCredit tbody").css({ 'width': '100% !important' });
    }
    else if ($(this).attr('id') == 'Cash') {
        $("#tblChecking").css({ 'display': 'none' });//tblCheckingDiv
        $("#tblCheckingDiv").css({ 'display': 'none' });//tblCheckingDiv
        $("#tblCash").css({ 'display': 'block' });
    }
    else {
        $("#tblChecking").css({ 'display': 'block' });//tblCheckingDiv
        $("#tblCheckingDiv").css({ 'display': 'block' });//tblCheckingDiv
        $("#tblCash").css({ 'display': 'none' });
    }

});

function LoadComboBox(parameters) {
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

function BindTenantDepositeAmount(result, typeChck, percentOrFlatVal) {
    if (result != null) {
        $("#txtSecurityDeposite").val(result.SecurityDeposit);
        $("#txtOneMonthRent").val(result.MonthlyRent);
        $("#txtProrateAmount").val(result.ProrateAmount);
        $("#txtFirtMonthRent").val(result.FirstMonthRent);
        $("#txtTotalDue").val(result.Total);

    }
    
    $("#subTotalCharge").text(result.Total);
    var percentRatio = percentOrFlatVal;
    var Amount = percentOrFlatVal;
    if (typeChck == 1) {
        $("#percentRatio").text(percentRatio.toString() + " %");
        var vv = result.Total - (result.Total * (percentRatio / 100));
        // $("#Amount").val(vv);
        $("#Amount").text(0.00);
    } else {
        $("#percentRatio").text(percentRatio.toString());
        $("#Amount").text(percentRatio.toString());
    }
    var TotalAmount = result.Total + percentRatio;
    $("#TotalAmountCharge").text(TotalAmount);


    $("#txtAmountOff").val(TotalAmount);

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
    var content = "";
    if (result.length > 0) {
        G_Doclist = result.length;
        $.each(result, function (i, obj) {
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
            content += "<td><input disabled " + checkedStatus + " type='checkbox' id='chk_" + i + "' class='chk' data_status='" + obj.IsViewedOrDownloaded + "'><input type='hidden' value='" + obj.Id + "' /></td>";
            content += "<td><label>" + obj.DocumentDescription + "</label></td>";
            content += "<td><label >" + obj.IsViewedOrDownloaded + "</label></td>";
            content += "<td><input type='button' data_FilePath='" + obj.FilePath + "' data_status='" + obj.IsViewedOrDownloaded + "' id='btn_" + i + "' class='btn btnNewColor docDownloadClick' " + isDisabled + " value='" + obj.IsViewedOrDownloaded + "'></td>";
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
        if (extFile == "jpg" || extFile == "jpeg" || extFile == "png" || extFile == 'gif' || extFile == 'tiff' || extFile == 'docx' || extFile == 'doc') {
            //TO DO
            var reader = new FileReader();
            reader.readAsDataURL(file);
            reader.onload = UpdateFilesDoc;
        } else {
            //alert("Only .gif, .jpg, .png, .tiff and .jpeg are allowed!");
            notify('danger', "Only .gif, .jpg, .png, .tiff and .jpeg are allowed!");
            $("#fileImageUpload").val("");
        }

    }
    else {
        //alert('Please Choose An Image');
        notify('danger', "Please Choose An Image");
    }
}
function UpdateFilesDoc(evt) {
    if ($("#documentUpload").val() != "") {
        var pagePath = "/Pages/Resident/ResidentialTenantAddResponceStep4_Sign_Deposit.aspx/" + "/DocImageUpload";
        // var CurRow = G_CurRow;

        var DocId = $(G_CurRow).find('td:eq(0)').find('input[type="hidden"]').val();
        //var docId=
        var result = evt.target.result;
        var ImageSave = result.replace("data:image/jpeg;base64,", "");
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
                        $(G_CurRow).find('td:eq(4)').find('label').text("Downloaded");
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


