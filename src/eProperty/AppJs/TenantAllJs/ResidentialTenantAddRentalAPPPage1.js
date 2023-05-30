var currentPagePath = window.location.pathname + "/";

var pathname = window.location.pathname; // Returns path only
var url = window.location.href;     // Returns full URL
var origin = window.location.origin;   // Returns base URL


$(document).ready(function (parameters) {
    $(".tDate").datepicker({
                dateFormat: "mm-dd-yy",
                changeYear: false,
                changeMonth: true
            });
            //$(".ddl").select2();
            $('input[type="checkbox"].flat-red, input[type="radio"].flat-red').iCheck({
                checkboxClass: 'icheckbox_flat-green',
                radioClass: 'iradio_flat-green'
            });
            $(document).on('change', '#MainContent_FileUpload1', function (e) {
                var file = e.currentTarget.files[0];
                objectUrl = URL.createObjectURL(file);
                $("#vid").prop("src", objectUrl);
            });
    LoadComboBox();
    LoadApprovalCode();

     var isresult = true;
    var bShow = $("#hdnShow").val();
    if (bShow === "undefined" || bShow === "") {
        $("#btnExit").css({ 'display': 'none' });
    }
    else {
        if (bShow === "False") {
            $("#btnExit").css({ 'display': 'block' });
        }
        else {
            $("#btnExit").css({ 'display': 'none' });
        }
    }

    //LoadCountry();
    //LoadState();
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

    //GetAllRelationship
    var URL = "/Pages/Resident/ResidentialTenantRental_App_Page_1.aspx/" + "GetAllRelationship";
    var obj = {};
    let Relationship = makeAjaxCallReturnPromiss(URL, obj);

    Country.then((data) => {
        console.log("Country Data Loaded");
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
        console.log("State Data Loaded");
        let StateData = setCombo($.parseJSON(decodeURIComponent(data.d)), '-1');
        $(".state option").empty();
        $(".state").append(StateData);
        $(".state").select2();

        // console.log(data.d);
    }).catch((err) => {
        console.log(err);
    });
    Relationship.then((data) => {
        let RelationData = setCombo_withInt($.parseJSON(decodeURIComponent(data.d)), '-1');
        $(".relation option").empty();
        $(".relation").append(RelationData);
        $(".relation").select2();
    });

    Promise.all([Country, State, Relationship]).then(function () {
        LoadTenantBasicGrid();
        LoadTenantResidenceGrid();
        LoadTenantEmployerInformationGrid();
        LoadTenantReferenceGrid();
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

function LoadTenantBasicGrid(parameters) {
    var URL = "/Pages/Resident/ResidentialTenantRental_App_Page_1.aspx/" + "GetAggrementTenantBasicInformation";
    var obj = {};
    let Basic = makeAjaxCallReturnPromiss(URL, obj);
    Basic.then((data) => {
        if (data.d != null || data.d != "") {
            BindBasicDataToField($.parseJSON(decodeURIComponent(data.d)));
            //BindBasicTenantInformationGrid($.parseJSON(decodeURIComponent(data.d)));
        }
    });
}
function LoadTenantResidenceGrid(parameters) {
    var URL = "/Pages/Resident/ResidentialTenantRental_App_Page_1.aspx/" + "GetTenantResidentHistoryList";
    var obj = {};
    let Basic = makeAjaxCallReturnPromiss(URL, obj);
    Basic.then((data) => {
        if (data.d != null || data.d != "") {
            
            BindBasicTenantResidenceGrid($.parseJSON(decodeURIComponent(data.d)));
        }
    });
}
function LoadTenantEmployerInformationGrid(parameters) {
    var URL = "/Pages/Resident/ResidentialTenantRental_App_Page_1.aspx/" + "GetTenantResidentEmployeeInformationList";
    var obj = {};
    let Basic = makeAjaxCallReturnPromiss(URL, obj);
    Basic.then((data) => {
        if (data.d != null || data.d != "") {
            BindBasicTenantEmployerInformationGrid($.parseJSON(decodeURIComponent(data.d)));
        }
    });
}
function LoadTenantReferenceGrid(parameters) {
    var URL = "/Pages/Resident/ResidentialTenantRental_App_Page_1.aspx/" + "GetTenantResidentReferenceList";
    var obj = {};
    let Basic = makeAjaxCallReturnPromiss(URL, obj);
    Basic.then((data) => {
        if (data.d != null || data.d != "") {
            BindBasicTenantReferenceGrid($.parseJSON(decodeURIComponent(data.d)));
        }
    });
}

function BindBasicTenantInformationGrid(result) {
    if (result != 'null') {
        let content = "";
        content += "<tr>";
        content += "<td>" + result.FirstName + " " + result.MiddleName + " " + result.LastName + "</td>";
        content += "<td>" + result.Address + "</td>";
        content += "<td>" + result.Address1 + "</td>";
        content += "<td>" + result.City + "</td>";
        content += "<td>" + result.State + "</td>";
        content += "<td>" + result.ZipCode + "</td>";
        content += "<td>" + result.DriversLicenseNo + "</td>";
        content += "<td>" + result.LicenceState + "</td>";
        content += "<td>" + result.RelationShip + "</td>";
        content += "<td>" + result.MobilePhone + "</td>";
        content += "<td><input type='button' value='Edit'  onclick='EditCommunication(this)' id='" + result.Id + "'  class='custombtn'/></td>";
        //content += "<td><input type='button' value='Delete'  onclick='DeleteCommunication(this)' id='" + result.Id + "'  class='custombtn'/></td>";
        content += "</tr>";
        $("#tblAggrement tbody").empty();
        $("#tblAggrement tbody").append(content);
    }

}

function BindBasicTenantResidenceGrid(result) {
    let content = "";
   
    if (result.length>0) {
        $.each(result, function (i, obj) {
            content += "<tr>";
            content += "<td>" + obj.Address + "</td>";
            //content += "<td>" + obj.Address1 + "</td>";
            content += "<td>" + obj.City + "</td>";
            content += "<td>" + obj.StateName + "</td>";
            content += "<td>" + obj.ZipCode + "</td>";
            //content += "<td>" + obj.OwnerManagerName + "</td>";
            //content += "<td>" + obj.OwnerManagerPhone + "</td>";
            //content += "<td>" + obj.MonthlyAmount + "</td>";
            content += "<td><input type='button' value='Edit'  onclick='EditResienceGrid(this)' id='" + obj.Id + "'  class='custombtn'/><input type='button' value='Delete'  onclick='DeleteResidenceGrid(this)' id='" + obj.Id + "'  class='custombtn'/></td>";
            //content += "<td><input type='button' value='Delete'  onclick='DeleteResidenceGrid(this)' id='" + obj.Id + "'  class='custombtn'/></td>";
            content += "</tr>";
        });

        $("#tblResidenceHistory tbody").empty();
        $("#tblResidenceHistory tbody").append(content);

    }
  
}
function BindBasicTenantEmployerInformationGrid(result) {
    let content = "";
    
    if (result.length>0) {
        $.each(result, function (i, obj) {
            content += "<tr>";
            content += "<td>" + obj.Name + "</td>";
            content += "<td>" + obj.Address + "</td>";
            //content += "<td>" + obj.Address1 + "</td>";
            content += "<td>" + obj.City + "</td>";
            content += "<td>" + obj.StateName + "</td>";
            content += "<td>" + obj.ZipCode + "</td>";
            //content += "<td>" + obj.ContactPersonName + "</td>";
            //content += "<td>" + obj.ContactPersonPhoneNo + "</td>";
            content += "<td>" + obj.MonthlyIncome + "</td>";
            content += "<td><input type='button' value='Edit'  onclick='EditEmployeeGrid(this)' id='" + obj.Id + "'  class='custombtn'/><input type='button' value='Delete'  onclick='DeleteEmployeeGrid(this)' id='" + obj.Id + "'  class='custombtn'/></td>";
            //content += "<td><input type='button' value='Delete'  onclick='DeleteEmployeeGrid(this)' id='" + obj.Id + "'  class='custombtn'/></td>";
            content += "</tr>";
        });

        $("#tblEmpInformation tbody").empty();
        $("#tblEmpInformation tbody").append(content);
    }
    
}
function BindBasicTenantReferenceGrid(result) {
    let content = "";
  
    if (result.length>0) {
        $.each(result, function (i, obj) {
            content += "<tr>";
            content += "<td>" + obj.Name + "</td>";
            content += "<td>" + obj.Address + "</td>";
            //content += "<td>" + obj.Address1 + "</td>";
            content += "<td>" + obj.City + "</td>";
            content += "<td>" + obj.StateName + "</td>";
            content += "<td>" + obj.ZipCode + "</td>";
            content += "<td>" + obj.PhoneNo + "</td>";
            //content += "<td>" + obj.MonthlyIncome + "</td>";
            content += "<td>" + obj.RelationshipName + "</td>";
            content += "<td><input type='button' value='Edit'  onclick='EditReferenceGrid(this)' id='" + obj.Id + "'  class='custombtn'/><input type='button' value='Delete'  onclick='DeleteReferenceGrid(this)' id='" + obj.Id + "'  class='custombtn'/></td>";
            //content += "<td><input type='button' value='Delete'  onclick='DeleteReferenceGrid(this)' id='" + obj.Id + "'  class='custombtn'/></td>";
            content += "</tr>";
        });

        $("#tblReferenceList tbody").empty();
        $("#tblReferenceList tbody").append(content);
    }
   
}




function setCombo(data, selectedvalue) {

    var content = '<option value="-1">Select.......</option>';
    if (data == undefined || data.length == 0 || data == null) {
        return content;
    }
    else {
        if (selectedvalue == undefined || selectedvalue == null) {
            $.each(data, function (i, obj) {
                content += '<option data_Id="' + obj.Id + '" value="' + obj.Id2 + '">' + obj.Data + '</option>';
            });
        }

        else {
            $.each(data, function (i, obj) {

                if (obj.Id2 == selectedvalue) {
                    content += '<option data_Id="' + obj.Id + '" value="' + obj.Id2 + '" selected>' + obj.Data + '</option>';
                } else {
                    content += '<option data_Id="' + obj.Id + '" value="' + obj.Id2 + '">' + obj.Data + '</option>';
                }
            });
        }

    }

    return content;
}

function setCombo_withInt(data, selectedvalue) {

    var content = '<option value="-1">Select.......</option>';
    if (data == undefined || data.length == 0 || data == null) {
        return content;
    }
    else {
        if (selectedvalue == undefined || selectedvalue == null) {
            $.each(data, function (i, obj) {
                content += '<option data_Id="' + obj.Id + '" value="' + obj.Id + '">' + obj.Data + '</option>';
            });
        }

        else {
            $.each(data, function (i, obj) {

                if (obj.Id == selectedvalue) {
                    content += '<option data_Id="' + obj.Id + '" value="' + obj.Id + '" selected>' + obj.Data + '</option>';
                } else {
                    content += '<option data_Id="' + obj.Id + '" value="' + obj.Id + '">' + obj.Data + '</option>';
                }
            });
        }

    }

    return content;
}

// -------------------------------------------------------------- Residence History ------------------------------------------------------------------------//

// ------ Add or Update Residence History button click ------------//

$(document).on('click', '#btnAddResidence', function (parameters) {
    
    if (validationResidence()) {
        let Obj = getTheResidenceObj();
        var URL = "/Pages/Resident/ResidentialTenantRental_App_Page_1.aspx/" + "SaveResidentHistory";
        let Basic = makeAjaxCallReturnPromiss(URL, Obj);
        Basic.then((data) => {
            if (data.d != null || data.d != "") {
                var res = $.parseJSON(decodeURIComponent(data.d));
                var mass = $("#btnAddResidence").val();
                if (res.length > 0 && res != "") {
                    notify('success', "Residence History saved successfully !!");
                    ClearResidenceField();
                    BindBasicTenantResidenceGrid(res);
                } else {
                    notify('danger', " Residence History save Failed !!");
                }

            } else {
                notify('danger', "Save Failed !!");
            }
        });
    }
});
 //---------- Clear Data -------------- //
function ClearResidenceField(parameters) {
    $("#hdResidentId").val(0);
    $("#txtResAddress").val("");
    $("#txtResAddress1").val("");
    //$("#ddlresCountry").val("-1").trigger('change');
    $("#txRestRegion").val("");
    $("#cityResTxt").val("");
    $("#ddlResstateapp").val("-1").trigger('change');
    $("#Reszipcodeapp1Txt").val("");
    $("#txtmonthlyAmount").val("");
    $("#txtOwnerManagerName").val("");
    $("#txtOwnerManagerPhone").val("");
    $("#txtReasonForLeaving").val("");
    $('input[name=rentPaid]').closest('div').removeClass('checked');
    $('input[name=rentPaid]').attr('checked', false);
    $("input[name=rentPaid][value='Yes']").closest('div').addClass('checked');
    $("input[name=rentPaid][value='Yes']").attr('checked', true);

    $('input[name=didGiveNotice]').closest('div').removeClass('checked');
    $('input[name=didGiveNotice]').attr('checked', false);
    $("input[name=didGiveNotice][value='Yes']").closest('div').addClass('checked');
    $("input[name=didGiveNotice][value='Yes']").attr('checked', true);

    $('input[name=askToMove]').closest('div').removeClass('checked');
    $('input[name=askToMove]').attr('checked', false);
    $("input[name=askToMove][value='Yes']").closest('div').addClass('checked');
    $("input[name=askToMove][value='Yes']").attr('checked', true);

  
    $("#txtbilled").val("");
    $("#txtLivedFrom").val("");
    $("#txtLivedTo").val("");
    $("#btnAddResidence").val("Add another Residence");
}

function validationResidence(parameters) {
    var isresult = true;
    var Address = $("#txtResAddress").val();
    var Address1 = $("#txtResAddress1").val();
    var Country = $("#ddlresCountry").val();
    var City = $("#cityResTxt").val();
    var ZipCode = $("#Reszipcodeapp1Txt").val();
    var MonthlyAmount = $("#txtmonthlyAmount").val();
    var OwnerManagerName = $("#txtOwnerManagerName").val();
    var OwnerManagerPhone = $("#txtOwnerManagerPhone").val();
    var ReasonForLeaving = $("#txtReasonForLeaving").val();
    var UtilizesBilled = $("#txtbilled").val();
    var LivedFromDate = $("#txtLivedFrom").val();
    var LivedTo = $("#txtLivedTo").val();

    if (Address === "undefined" || Address === "") {
        $("#txtResAddress").css({ 'border': '1px solid red' });
        isresult = false;
    }
    else {
        $("#txtResAddress").css({ 'border': '1px solid #d2d6de' });
    }

    //if (Address1 === "undefined" || Address1 === "") {
    //    $("#txtResAddress1").css({ 'border': '1px solid red' });
    //    isresult = false;
    //}
    //else {
    //    $("#txtResAddress1").css({ 'border': '1px solid #d2d6de' });
    //}

    if (Country === "undefined" || Country === "-1") {
        $("#s2id_ddlresCountry").css({ 'border': '1px solid red' });
        isresult = false;
    }
    else {
        $("#s2id_ddlresCountry").css({ 'border': '1px solid #d2d6de' });
    }

    if (City === "undefined" || City === "-1") {
        $("#s2id_cityResTxt").css({ 'border': '1px solid red' });
        isresult = false;
    }
    else {
        $("#s2id_cityResTxt").css({ 'border': '1px solid #d2d6de' });
    }


    if (ZipCode === "undefined" || ZipCode === "") {
        $("#Reszipcodeapp1Txt").css({ 'border': '1px solid red' });
        isresult = false;
    }
    else {
        $("#Reszipcodeapp1Txt").css({ 'border': '1px solid #d2d6de' });
    }
    if (MonthlyAmount === "undefined" || MonthlyAmount === "") {
        $("#txtmonthlyAmount").css({ 'border': '1px solid red' });
        isresult = false;
    }
    else {
        $("#txtmonthlyAmount").css({ 'border': '1px solid #d2d6de' });
    }
    if (OwnerManagerName === "undefined" || OwnerManagerName === "") {
        $("#txtOwnerManagerName").css({ 'border': '1px solid red' });
        isresult = false;
    }
    else {
        $("#txtOwnerManagerName").css({ 'border': '1px solid #d2d6de' });
    }
    if (OwnerManagerPhone === "undefined" || OwnerManagerPhone === "") {
        $("#txtOwnerManagerPhone").css({ 'border': '1px solid red' });
        isresult = false;
    }
    else {
        $("#txtOwnerManagerPhone").css({ 'border': '1px solid #d2d6de' });
    }
    if (ReasonForLeaving === "undefined" || ReasonForLeaving === "") {
        $("#txtReasonForLeaving").css({ 'border': '1px solid red' });
        isresult = false;
    }
    else {
        $("#txtReasonForLeaving").css({ 'border': '1px solid #d2d6de' });
    }
    if (UtilizesBilled === "undefined" || UtilizesBilled === "") {
        $("#txtbilled").css({ 'border': '1px solid red' });
        isresult = false;
    }
    else {
        $("#txtbilled").css({ 'border': '1px solid #d2d6de' });
    }
    if (LivedFromDate === "undefined" || LivedFromDate === "") {
        $("#txtLivedFrom").css({ 'border': '1px solid red' });
        isresult = false;
    }
    else {
        $("#txtLivedFrom").css({ 'border': '1px solid #d2d6de' });
    }
    if (LivedTo === "undefined" || LivedTo === "") {
        $("#txtLivedTo").css({ 'border': '1px solid red' });
        isresult = false;
    }
    else {
        $("#txtLivedTo").css({ 'border': '1px solid #d2d6de' });
    }

    
    if (isresult)
        isresult = true;
    else {
        isresult = false;
    }
    return isresult;
}

function getTheResidenceObj(parameters) {
    var Obj = {
        Id: $("#hdResidentId").val(),
        Address: $("#txtResAddress").val(),
        Address1: $("#txtResAddress1").val(),
        Country: $("#ddlresCountry").val(),
        CountryName:$("#ddlresCountry option:selected").text(),
        Region: $("#txRestRegion").val(),
        City: $("#cityResTxt").val(),
        State: $("#ddlResstateapp").val(),
        StateName: $("#ddlResstateapp option:selected").text(),
        ZipCode: $("#Reszipcodeapp1Txt").val(),
        MonthlyAmount: $("#txtmonthlyAmount").val(),
        OwnerManagerName: $("#txtOwnerManagerName").val(),
        OwnerManagerPhone: $("#txtOwnerManagerPhone").val(),
        ReasonForLeaving: $("#txtReasonForLeaving").val(),
        IsRentPaidFull: $('input[name=rentPaid]:checked').val(),
        DidGiveNotice: $('input[name=didGiveNotice]:checked').val(),
        AskToMove: $('input[name=askToMove]:checked').val(),
        UtilizesBilled: $("#txtbilled").val(),
        LivedFromDate: $("#txtLivedFrom").val(),
        LivedTo: $("#txtLivedTo").val()
      
    }
    return Obj;
}

function DeleteResidenceGrid(curtd) {
    var id = $(curtd).attr('Id');
    if (showconfirm()) {
        var URL = "/Pages/Resident/ResidentialTenantRental_App_Page_1.aspx/" + "DeleteTenantResidenceHistory";
        var obj = {
            Id: id
        };
        let Basic = makeAjaxCallReturnPromiss(URL, obj);
        Basic.then((data) => {
            if (data.d != null || data.d != "") {
                var res = $.parseJSON(decodeURIComponent(data.d));
                if (res.length > 0 && res != null) {
                    notify('success', "Residence Deleted successfully");
                    BindBasicTenantResidenceGrid(res);
                } else {
                    notify('danger', "Residence Delete Failed !!");
                }
                
            }
        });
    } 
   
}
function EditResienceGrid(curtd) {
    var id = $(curtd).attr('Id');
    var URL = "/Pages/Resident/ResidentialTenantRental_App_Page_1.aspx/" + "EditTenantResidenceHistory";
    var obj = {
        Id: id
    };
    let Basic = makeAjaxCallReturnPromiss(URL, obj);
    Basic.then((data) => {
        if (data.d != null || data.d != "") {
            BindResidenceDataToField($.parseJSON(decodeURIComponent(data.d)));
        }
    });

}

function BindResidenceDataToField(result) {
    if (result != "null") {
        $("#hdResidentId").val(result.Id);
        $("#txtResAddress").val(result.Address);
        $("#txtResAddress1").val(result.Address1);
        $("#ddlresCountry").val(result.Country).trigger('change');
        $("#txRestRegion").val(result.Region);
        $("#cityResTxt").val(result.City);
        $("#Reszipcodeapp1Txt").val(result.ZipCode);
        $("#ddlResstateapp").val(result.State).trigger('change');
        $("#txtmonthlyAmount").val(result.MonthlyAmount);
        $("#txtOwnerManagerName").val(result.OwnerManagerName);
        $("#txtOwnerManagerPhone").val(result.OwnerManagerPhone);
        $("#txtReasonForLeaving").val(result.ReasonForLeaving);
        $('input[name=rentPaid]').closest('div').removeClass('checked');
        $('input[name=rentPaid]').attr('checked', false);
        $("input[name=rentPaid][value='" + result.IsRentPaidFull + "']").closest('div').addClass('checked');
        $("input[name=rentPaid][value='" + result.IsRentPaidFull + "']").attr('checked', true);
        $('input[name=didGiveNotice]').closest('div').removeClass('checked');
        $('input[name=didGiveNotice]').attr('checked', false);
        $("input[name=didGiveNotice][value='" + result.DidGiveNotice + "']").closest('div').addClass('checked');
        $("input[name=didGiveNotice][value='" + result.DidGiveNotice + "']").attr('checked', true);
        $('input[name=askToMove]').closest('div').removeClass('checked');
        $('input[name=askToMove]').attr('checked', false);
        $("input[name=askToMove][value='" + result.AskToMove + "']").closest('div').addClass('checked');
        $("input[name=askToMove][value='" + result.AskToMove + "']").attr('checked', true);
        $("#txtbilled").val(result.UtilizesBilled);
        $("#txtLivedFrom").val(result.LivedFromDate);
        $("#txtLivedTo").val(result.LivedTo);
        $("#btnAddResidence").val("Update Residence History");
    }
}



// -------------------------------------------------------------- Employer Information ------------------------------------------------------------------------//
$(document).on('click', '#btnAddEmployee', function (parameters) {
    
    if (ValidationForEmployerInformation()) {
        let Obj = getTheEmployeeObj();
        var URL = "/Pages/Resident/ResidentialTenantRental_App_Page_1.aspx/" + "SaveEmployeeInformation";
        let Basic = makeAjaxCallReturnPromiss(URL, Obj);
        Basic.then((data) => {
            if (data.d != null || data.d != "") {
                var res = $.parseJSON(decodeURIComponent(data.d));
                var mass = $("#btnAddEmployee").val();
                if (res.length > 0 && res != "") {
                    notify('success', " Employer Information saved successfully !!");
                    ClearEmployeeField();
                    BindBasicTenantEmployerInformationGrid(res);
                } else {
                    notify('danger', " Employer Information save Failed !!");
                }

            } else {
                notify('danger', "Save Failed !!");
            }
        });
    }
});
//---------- Clear Data -------------- //
function ClearEmployeeField(parameters) {
    $("#hdEmpId").val(0);
    $("#txtEmpName").val("");
    $('input[name=JobType]').closest('div').removeClass('checked');
    $('input[name=JobType]').attr('checked', false);
    $("input[name=JobType][value='Yes']").closest('div').addClass('checked');
    $("input[name=JobType][value='Yes']").attr('checked', true);
    $("#txtEmpAddress").val("");
    $("#txtEmpAddress1").val("");
    //$("#ddlEmpCountry").val("-1").trigger('change');
    $("#txEmpRegion").val("");
    $("#cityEmpTxt").val("");
    $("#ddlEmpstateapp").val("-1").trigger('change');
    $("#Empzipcodeapp1Txt").val("");
    $("#txtEmpmonthlyAmount").val("");
    $("#txtEmpContactPersonName").val("");
    $("#txtEmpContactPersonPhone").val("");
    $("#txtEmpFromDate").val("");
    $("#txtEmpFromTo").val("");
    $("#StillWork").prop('checked', false);
    $("#btnAddEmployee").val("Add another Employer");
}

function ValidationForEmployerInformation(parameters) {
    var isresult = true;
    var Name = $("#txtEmpName").val();
    var Address = $("#txtEmpAddress").val();
    var Address1 = $("#txtEmpAddress1").val();
    var Country = $("#ddlEmpCountry").val();
    var City = $("#cityEmpTxt").val();
    var ZipCode = $("#Empzipcodeapp1Txt").val();
    var MonthlyIncome = $("#txtEmpmonthlyAmount").val();
    var ContactPersonName = $("#txtEmpContactPersonName").val();
    var ContactPersonPhoneNo = $("#txtEmpContactPersonPhone").val();
    var EmploymentFromDate = $("#txtEmpFromDate").val();
    var EmploymentFromTo= $("#txtEmpFromTo").val();
    var IsStillWorkthere = $("#StillWork").is(":checked") == true ? true : false;
  
    if (Name === "undefined" || Name === "") {
        $("#txtEmpName").css({ 'border': '1px solid red' });
        isresult = false;
    }
    else {
        $("#txtEmpName").css({ 'border': '1px solid #d2d6de' });
    }
    if (Address === "undefined" || Address === "") {
        $("#txtEmpAddress").css({ 'border': '1px solid red' });
        isresult = false;
    }
    else {
        $("#txtEmpAddress").css({ 'border': '1px solid #d2d6de' });
    }

    //if (Address1 === "undefined" || Address1 === "") {
    //    $("#txtEmpAddress1").css({ 'border': '1px solid red' });
    //    isresult = false;
    //}
    //else {
    //    $("#txtEmpAddress1").css({ 'border': '1px solid #d2d6de' });
    //}

    if (Country === "undefined" || Country === "-1") {
        $("#s2id_ddlEmpCountry").css({ 'border': '1px solid red' });
        isresult = false;
    }
    else {
        $("#s2id_ddlresCountry").css({ 'border': '1px solid #d2d6de' });
    }

    if (City === "undefined" || City === "") {
        $("#cityEmpTxt").css({ 'border': '1px solid red' });
        isresult = false;
    }
    else {
        $("#cityEmpTxt").css({ 'border': '1px solid #d2d6de' });
    }


    if (ZipCode === "undefined" || ZipCode === "") {
        $("#Empzipcodeapp1Txt").css({ 'border': '1px solid red' });
        isresult = false;
    }
    else {
        $("#Empzipcodeapp1Txt").css({ 'border': '1px solid #d2d6de' });
    }
    if (MonthlyIncome === "undefined" || MonthlyIncome === "") {
        $("#txtEmpmonthlyAmount").css({ 'border': '1px solid red' });
        isresult = false;
    }
    else {
        $("#txtEmpmonthlyAmount").css({ 'border': '1px solid #d2d6de' });
    }
    if (ContactPersonName === "undefined" || ContactPersonName === "") {
        $("#txtEmpContactPersonName").css({ 'border': '1px solid red' });
        isresult = false;
    }
    else {
        $("#txtEmpContactPersonName").css({ 'border': '1px solid #d2d6de' });
    }
    if (ContactPersonPhoneNo === "undefined" || ContactPersonPhoneNo === "") {
        $("#txtEmpContactPersonPhone").css({ 'border': '1px solid red' });
        isresult = false;
    }
    else {
        $("#txtEmpContactPersonPhone").css({ 'border': '1px solid #d2d6de' });
    }

    if ($("#StillWork").prop("checked") === false)
    {
        if (EmploymentFromDate === "undefined" || EmploymentFromDate === "") {
            $("#txtEmpFromDate").css({ 'border': '1px solid red' });
            isresult = false;
        }
        else {
            $("#txtEmpFromDate").css({ 'border': '1px solid #d2d6de' });
        }
        if (EmploymentFromTo === "undefined" || EmploymentFromTo === "") {
            $("#txtEmpFromTo").css({ 'border': '1px solid red' });
            isresult = false;
        }
        else {
            $("#txtEmpFromTo").css({ 'border': '1px solid #d2d6de' });
        }
    }
    

    if (isresult)
        isresult = true;
    else {
        isresult = false;
    }
    return isresult;
}

function getTheEmployeeObj(parameters) {
    var Obj = {
        Id: $("#hdEmpId").val(),
        Name: $("#txtEmpName").val(),
        JobType: $('input[name=JobType]:checked').val(),
        Address: $("#txtEmpAddress").val(),
        Address1: $("#txtEmpAddress1").val(),
        Country: $("#ddlEmpCountry").val(),
        CountryName: $("#ddlEmpCountry option:selected").text(),
        Region: $("#txEmpRegion").val(),
        City: $("#cityEmpTxt").val(),
        State: $("#ddlEmpstateapp").val(),
        StateName: $("#ddlEmpstateapp option:selected").text(),
        ZipCode: $("#Empzipcodeapp1Txt").val(),
        MonthlyIncome: $("#txtEmpmonthlyAmount").val(),
        ContactPersonName: $("#txtEmpContactPersonName").val(),
        ContactPersonPhoneNo: $("#txtEmpContactPersonPhone").val(),
        EmploymentFromDate: $("#txtEmpFromDate").val(),
        EmployeeFromTo : $("#txtEmpFromTo").val(),
        IsStillWorkthere: $("#StillWork").is(":checked") == true ? true : false
       

    }
    return Obj;
}

function DeleteEmployeeGrid(curtd) {
    var id = $(curtd).attr('Id');
    if (showconfirm()) {
        var URL = "/Pages/Resident/ResidentialTenantRental_App_Page_1.aspx/" + "DeleteTenantEmployeeInfo";
        var obj = {
            Id: id
        };
        let Basic = makeAjaxCallReturnPromiss(URL, obj);
        Basic.then((data) => {
            if (data.d != null || data.d != "") {
                var res = $.parseJSON(decodeURIComponent(data.d));
                if (res.length > 0 && res != null) {
                    notify('success', "Employee Deleted successfully");
                    BindBasicTenantEmployerInformationGrid(res);
                } else {
                    notify('danger', "Residence Deleted Failed !!");
                }

            }
        });
    }

}
function EditEmployeeGrid(curtd) {
    
    var id = $(curtd).attr('Id');
    var URL = "/Pages/Resident/ResidentialTenantRental_App_Page_1.aspx/" + "EditTenantEmployee";
    var obj = {
        Id: id
    };
    let Basic = makeAjaxCallReturnPromiss(URL, obj);
    Basic.then((data) => {
        if (data.d != null || data.d != "") {
            BindEmployeeDataToField($.parseJSON(decodeURIComponent(data.d)));
        }
    });

}

function BindEmployeeDataToField(result) {
    if (result != "null") {
        $("#hdEmpId").val(result.Id);
        $("#txtEmpName").val(result.Name);
        $('input[name=JobType]').closest('div').removeClass('checked');
        $('input[name=JobType]').attr('checked', false);
        $("input[name=JobType][value='" + result.JobType + "']").closest('div').addClass('checked');
        $("input[name=JobType][value='" + result.JobType + "']").attr('checked', true);
        $("#txtEmpAddress").val(result.Address);
        $("#txtEmpAddress1").val(result.Address1);
        $("#ddlEmpCountry").val(result.Country).trigger('change');
        $("#txEmpRegion").val(result.Region);
        $("#cityEmpTxt").val(result.City);
        $("#Empzipcodeapp1Txt").val(result.ZipCode);
        $("#ddlEmpstateapp").val(result.State).trigger('change');
        $("#txtEmpmonthlyAmount").val(result.MonthlyIncome);
        $("#txtEmpContactPersonName").val(result.ContactPersonName);
        $("#txtEmpContactPersonPhone").val(result.ContactPersonPhoneNo);
        $("#txtEmpFromDate").val(result.EmploymentFromDate);
        $("#txtEmpFromTo").val(result.EmployeeFromTo);
        if (result.IsStillWorkthere == true) {
            $("#StillWork").prop('checked', true);
        } else {
            $("#StillWork").prop('checked', false);
        }
       
        $("#btnAddEmployee").val("Update Employee Information");
    }
}

// ------------------------------------------------------------ Reference Information ---------------------------------------------------------------------------//
$(document).on('click', '#btnAddReference', function (parameters) {
    
    if (ValidationForReference()) {
        let Obj = getTheReferenceObj();
        var URL = "/Pages/Resident/ResidentialTenantRental_App_Page_1.aspx/" + "SaveReference";
        let Basic = makeAjaxCallReturnPromiss(URL, Obj);
        Basic.then((data) => {
            if (data.d != null || data.d != "") {
                var res = $.parseJSON(decodeURIComponent(data.d));
                var mass = $("#btnAddReference").val();
                if (res.length > 0 && res != "") {
                    notify('success', " Reference saved successfully !!");
                    ClearReferenceField();
                    BindBasicTenantReferenceGrid(res);
                } else {
                    notify('danger', " Reference save Failed !!");
                }

            } else {
                notify('danger', "Save Failed !!");
            }
        });
    }
});
//---------- Clear Data -------------- //
function ClearReferenceField(parameters) {
    $("#hdReferenceId").val(0);
    $("#txtRefName").val("");
    $("#txtRefAddress").val("");
    $("#txtRefAddress1").val("");
    //$("#ddlRefCountry").val("-1").trigger('change');
    $("#txRefRegion").val("");
    $("#cityRefTxt").val("");
    $("#ddlRefstateapp").val("-1").trigger('change');
    $("#Refzipcodeapp1Txt").val("");
    $("#txtRefmonthlyAmount").val("");
    $("#ddlrefRelationship").val("-1").trigger("change");
    $("#txtrefPersonPhone").val("");
    $("#txtRefEmailAddress").val("");
    $("#btnAddReference").val("Add another References");
}

function ValidationForReference(parameters) {
    var isresult = true;
    var Name = $("#txtRefName").val();
    var Address = $("#txtRefAddress").val();
    var Address1 = $("#txtRefAddress1").val();
    var Country = $("#ddlRefCountry").val();
    var City = $("#cityRefTxt").val();
    var ZipCode = $("#Refzipcodeapp1Txt").val();
    var MonthlyIncome = $("#txtRefmonthlyAmount").val();
    var Relationship = $("#ddlrefRelationship").val();
    var PersonePhone = $("#txtrefPersonPhone").val();
    var Email = $("#txtRefEmailAddress").val();
    // var IsStillWorkthere = $("#StillWork").is(":checked") == true ? true : false;

    if (Name === "undefined" || Name === "") {
        $("#txtRefName").css({ 'border': '1px solid red' });
        isresult = false;
    }
    else {
        $("#txtRefName").css({ 'border': '1px solid #d2d6de' });
    }
    if (Address === "undefined" || Address === "") {
        $("#txtRefAddress").css({ 'border': '1px solid red' });
        isresult = false;
    }
    else {
        $("#txtRefAddress").css({ 'border': '1px solid #d2d6de' });
    }

    //if (Address1 === "undefined" || Address1 === "") {
    //    $("#txtRefAddress1").css({ 'border': '1px solid red' });
    //    isresult = false;
    //}
    //else {
    //    $("#txtRefAddress1").css({ 'border': '1px solid #d2d6de' });
    //}

    if (Country === "undefined" || Country === "-1") {
        $("#s2id_ddlRefCountry").css({ 'border': '1px solid red' });
        isresult = false;
    }
    else {
        $("#s2id_ddlRefCountry").css({ 'border': '1px solid #d2d6de' });
    }

    if (City === "undefined" || City === "") {
        $("#cityRefTxt").css({ 'border': '1px solid red' });
        isresult = false;
    }
    else {
        $("#cityRefTxt").css({ 'border': '1px solid #d2d6de' });
    }


    if (ZipCode === "undefined" || ZipCode === "") {
        $("#Refzipcodeapp1Txt").css({ 'border': '1px solid red' });
        isresult = false;
    }
    else {
        $("#Refzipcodeapp1Txt").css({ 'border': '1px solid #d2d6de' });
    }
    //if (MonthlyIncome === "undefined" || MonthlyIncome === "") {
    //    $("#txtRefmonthlyAmount").css({ 'border': '1px solid red' });
    //    isresult = false;
    //}
    //else {
    //    $("#txtRefmonthlyAmount").css({ 'border': '1px solid #d2d6de' });
    //}

    if (Relationship === "undefined" || Relationship === "-1") {
        $("#s2id_ddlrefRelationship").css({ 'border': '1px solid red' });
        isresult = false;
    }
    else {
        $("#s2id_ddlrefRelationship").css({ 'border': '1px solid #d2d6de' });
    }


    if (PersonePhone === "undefined" || PersonePhone === "") {
        $("#txtrefPersonPhone").css({ 'border': '1px solid red' });
        isresult = false;
    }
    else {
        $("#txtrefPersonPhone").css({ 'border': '1px solid #d2d6de' });
    }
    if (Email === "undefined" || Email === "") {
        $("#txtRefEmailAddress").css({ 'border': '1px solid red' });
        isresult = false;
    }
    else {
        $("#txtRefEmailAddress").css({ 'border': '1px solid #d2d6de' });
    }
    


    if (isresult)
        isresult = true;
    else {
        isresult = false;
    }
    return isresult;
}

function getTheReferenceObj(parameters) {
    var Obj = {
        Id: $("#hdReferenceId").val(),
        Name: $("#txtRefName").val(),
        Address: $("#txtRefAddress").val(),
        Address1: $("#txtRefAddress1").val(),
        Country: $("#ddlRefCountry").val(),
        CountryName: $("#ddlRefCountry option:selected").text(),
        Region: $("#txRefRegion").val(),
        City: $("#cityRefTxt").val(),
        State: $("#ddlRefstateapp").val(),
        StateName: $("#ddlRefstateapp option:selected").text(),
        ZipCode: $("#Refzipcodeapp1Txt").val(),
        MonthlyIncome: $("#txtRefmonthlyAmount").val(),
        Relationship: $("#ddlrefRelationship").val(),
        RelationshipName: $("#ddlrefRelationship option:selected").text(),
        PhoneNo: $("#txtrefPersonPhone").val(),
        EmailAddress: $("#txtRefEmailAddress").val()
    }
    return Obj;
}

function DeleteReferenceGrid(curtd) {
    var id = $(curtd).attr('Id');
    if (showconfirm()) {
        var URL = "/Pages/Resident/ResidentialTenantRental_App_Page_1.aspx/" + "DeleteTenantRefenceInfo";
        var obj = {
            Id: id
        };
        let Basic = makeAjaxCallReturnPromiss(URL, obj);
        Basic.then((data) => {
            if (data.d != null || data.d != "") {
                var res = $.parseJSON(decodeURIComponent(data.d));
                if (res.length > 0 && res != null) {
                    notify('success', "Employee Deleted successfully");
                    BindBasicTenantReferenceGrid(res);
                } else {
                    notify('danger', "Residence Delete Failed !!");
                }

            }
        });
    }

}
function EditReferenceGrid(curtd) {
    
    var id = $(curtd).attr('Id');
    var URL = "/Pages/Resident/ResidentialTenantRental_App_Page_1.aspx/" + "EditTenantReference";
    var obj = {
        Id: id
    };
    let Basic = makeAjaxCallReturnPromiss(URL, obj);
    Basic.then((data) => {
        if (data.d != null || data.d != "") {
            BindReferenceDataToField($.parseJSON(decodeURIComponent(data.d)));
        }
    });

}

function BindReferenceDataToField(result) {
    if (result != "null") {
        $("#hdReferenceId").val(result.Id);
        $("#txtRefName").val(result.Name);
        $("#txtRefAddress").val(result.Address);
        $("#txtRefAddress1").val(result.Address1);
        $("#ddlRefCountry").val(result.Country).trigger('change');
        $("#txRefRegion").val(result.Region);
        $("#cityRefTxt").val(result.City);
        $("#Refzipcodeapp1Txt").val(result.ZipCode);
        $("#ddlRefstateapp").val(result.State).trigger('change');
        $("#txtRefmonthlyAmount").val(result.MonthlyIncome);
        $("#ddlrefRelationship").val(result.Relationship).trigger('change');
        $("#txtrefPersonPhone").val(result.PhoneNo);
        $("#txtRefEmailAddress").val(result.EmailAddress);
        $("#btnAddReference").val("Update Reference");
    }
}


//--------------------------------------------- Save And Continue ---------------------------------------//
$(document).on('click', '#btnContinue', function (parameters) {
    
    if (ValidateAlltheGridAndBasicDataForContinue()) {
        
        var ResidenceHistory = $("#tblResidenceHistory tbody tr").length;
        if (ResidenceHistory > 0) {
            var emp = $("#tblEmpInformation tbody tr").length;
            if (emp > 0) {
                var reference = $("#tblReferenceList tbody tr").length;
                if (reference > 2) {

                    let Obj = getTheBasicObj();
                    var URL = "/Pages/Resident/ResidentialTenantRental_App_Page_1.aspx/" + "SaveAndCotinue";
                  //  window.location.href = "/Pages/Resident/ResidentialTenantRental_App_Page_2.aspx";
                    //window.open("/Pages/Resident/ResidentialTenantRental_App_Page_2.aspx", '');
                    let Basic = makeAjaxCallReturnPromiss(URL, Obj);
                    Basic.then((data) => {
                        if (data.d != null || data.d != "") {
                            var res = $.parseJSON(decodeURIComponent(data.d));
                            if (res == true) {
                                window.location.href = "/Pages/Resident/ResidentialTenantRental_App_Page_2.aspx";
                            } else {
                                notify('danger', "Save Failed !!");
                            }
                           
                            //var mass = $("#btnAddReference").val();
                            //window.open("/Pages/Resident/ResidentialTenantRental_App_Page_2.aspx", '');
                            

                        } else {
                            notify('danger', "Save Failed !!");
                        }
                    });
                } else {
                    notify('danger', "Add References Information only had " + reference);
                }
            } else {
                notify('danger', "Please Add Employer Information");
            }
        } else {
            notify('danger', "Please Add Residence History");
        }
    }
    else {
        notify('danger', "Please Add Agreement Information above");
    }
    
});

$(document).on('click', '#btnBack', function (parameters) {
    var pagePath = currentPagePath + "apply";
   
    $.ajax({
        type: "POST",
        url: pagePath,
        data: JSON.stringify(),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        error:
            function (XMLHttpRequest, textStatus, errorThrown) {
                // alert("Error");
                notify('danger', "Error"); //../../Uploads/Images/38860545_2093714167366803_1085132197528076288_n.jpg
            },
        success:
            function (result) {
                
                var res = $.parseJSON(decodeURIComponent(result.d));
                if (res != null) {
                    if (res.isFirstSignIn == true) {
                        var origin = window.location.origin; // Returns base URL
                        var url = origin + "/Pages/Resident/ResidentialTentAddResponceStep1.aspx?ResidentialUnitSerial=" + res.ResidentialUnitSerialId + '&&TenentId=' + res.Serial;
                        window.location.href = url;
                    }
                }
            }
    });

});

$(document).on('click', '#btnExit', function (parameters) {
    
    var origin = window.location.origin; // Returns base URL
    var url = origin + "/Pages/DashboardAdmin.aspx";
    window.location.href = url;
});

function ValidateAlltheGridAndBasicDataForContinue(parameters) {
    var isresult = true;
    var Name = $("#txtFirstName").val();
    var Address = $("#txtAddress").val();
    var Address1 = $("#txtAddress1").val();
    var Country = $("#ddlCountry").val();
    var City = $("#cityapp1Txt").val();
    var ZipCode = $("#zipcodeapp1Txt").val();
    var Email = $("#txtEmailAddress").val();
    var Mobile = $("#txtMobilePhone").val();
    var HomePhone = $("#txtHomePhone").val();
    var DriverLicense = $("#txtDriverLicense").val();

    var Social = $("#txtSocial").val();

    var DrivingState = $("#ddlLicenceState").val();
    var Birthdate = $("#txtBirthday").val();

    // var IsStillWorkthere = $("#StillWork").is(":checked") == true ? true : false;

    if (Name === "undefined" || Name === "") {
        $("#txtFirstName").css({ 'border': '1px solid red' });
        isresult = false;
    }
    else {
        $("#txtFirstName").css({ 'border': '1px solid #d2d6de' });
    }
    if (Address === "undefined" || Address === "") {
        $("#txtAddress").css({ 'border': '1px solid red' });
        isresult = false;
    }
    else {
        $("#txtAddress").css({ 'border': '1px solid #d2d6de' });
    }

    //if (Address1 === "undefined" || Address1 === "") {
    //    $("#txtAddress1").css({ 'border': '1px solid red' });
    //    isresult = false;
    //}
    //else {
    //    $("#txtAddress1").css({ 'border': '1px solid #d2d6de' });
    //}

    if (Country === "undefined" || Country === "-1") {
        $("#s2id_ddlCountry").css({ 'border': '1px solid red' });
        isresult = false;
    }
    else {
        $("#s2id_ddlCountry").css({ 'border': '1px solid #d2d6de' });
    }

    if (City === "undefined" || City === "") {
        $("#cityapp1Txt").css({ 'border': '1px solid red' });
        isresult = false;
    }
    else {
        $("#cityapp1Txt").css({ 'border': '1px solid #d2d6de' });
    }


    if (ZipCode === "undefined" || ZipCode === "") {
        $("#zipcodeapp1Txt").css({ 'border': '1px solid red' });
        isresult = false;
    }
    else {
        $("#zipcodeapp1Txt").css({ 'border': '1px solid #d2d6de' });
    }
    if (Email === "undefined" || Email === "") {
        $("#txtEmailAddress").css({ 'border': '1px solid red' });
        isresult = false;
    }
    else {
        $("#txtEmailAddress").css({ 'border': '1px solid #d2d6de' });
    }

    //if (Mobile === "undefined" || Mobile === "") {
    //    $("#txtMobilePhone").css({ 'border': '1px solid red' });
    //    isresult = false;
    //}
    //else {
    //    $("#txtMobilePhone").css({ 'border': '1px solid #d2d6de' });
    //}


    //if (HomePhone === "undefined" || HomePhone === "") {
    //    $("#txtHomePhone").css({ 'border': '1px solid red' });
    //    isresult = false;
    //}
    //else {
    //    $("#txtHomePhone").css({ 'border': '1px solid #d2d6de' });
    //}

    if (DriverLicense === "undefined" || DriverLicense === "") {
        $("#txtDriverLicense").css({ 'border': '1px solid red' });
        isresult = false;
    }
    else {
        $("#txtDriverLicense").css({ 'border': '1px solid #d2d6de' });
    }

    if (DrivingState === "undefined" || DrivingState === "-1") {
        $("#s2id_ddlLicenceState").css({ 'border': '1px solid red' });
        isresult = false;
    }
    else {
        $("#s2id_ddlLicenceState").css({ 'border': '1px solid #d2d6de' });
    }

    if (Social === "undefined" || Social === "") {
        $("#txtSocial").css({ 'border': '1px solid red' });
        isresult = false;
    }
    else {
        $("#txtSocial").css({ 'border': '1px solid #d2d6de' });
    }
    
    if (Birthdate === "undefined" || Birthdate === "") {
        $("#txtBirthday").css({ 'border': '1px solid red' });
        isresult = false;
    }
    else {
        $("#txtBirthday").css({ 'border': '1px solid #d2d6de' });
    }


    if (isresult)
        isresult = true;
    else {
        isresult = false;
    }
    return isresult;
}


function getTheBasicObj(parameters) {
    var Obj = {
        Id: $("#hdTenantBasicId").val(),
        FirstName: $("#txtFirstName").val(),
        MiddleName: $("#txtMiddleName").val(),
        LastName: $("#txtLastName").val(),
        AliasName: $("#txtAliasName").val(),
        Address: $("#txtAddress").val(),
        Address1: $("#txtAddress1").val(),
        Country: $("#ddlCountry").val(),
        Region: $("#txtRegion").val(),
        City: $("#cityapp1Txt").val(),
        State: $("#ddlstateapp").val(),
        ZipCode: $("#zipcodeapp1Txt").val(),
        EmailAddress: $("#txtEmailAddress").val(),
        MobilePhone: $("#txtMobilePhone").val(),
        HomePhone: $("#txtHomePhone").val(),
        DriversLicenseNo: $("#txtDriverLicense").val(),
        LicenceState: $("#ddlLicenceState").val(),
        SocialSecurityNo: $("#txtSocial").val(),
        RelationShip: $("#ddlRelationship").val(),
        Other: $("#txtOther").val(),
        Birthday: $("#txtBirthday").val()
    }
    return Obj;
}



function BindBasicDataToField(result) {
    if (result != "null") {
        $("#hdTenantBasicId").val(result.Id);
        $("#txtFirstName").val(result.FirstName);
        $("#txtMiddleName").val(result.MiddleName);
        $("#txtLastName").val(result.LastName);
        $("#txtAliasName").val(result.AliasName);
        $("#txtAddress").val(result.Address);
        $("#txtAddress1").val(result.Address1);
        $("#ddlCountry").val(result.Country).trigger('change');
        $("#txtRegion").val(result.Region);
        $("#cityapp1Txt").val(result.City);
        $("#ddlstateapp").val(result.State).trigger('change');
        $("#zipcodeapp1Txt").val(result.ZipCode);
        $("#txtEmailAddress").val(result.EmailAddress);
        $("#txtMobilePhone").val(result.MobilePhone);
        $("#txtHomePhone").val(result.HomePhone);
        $("#txtDriverLicense").val(result.DriversLicenseNo);
        $("#ddlLicenceState").val(result.LicenceState).trigger('change');
        $("#txtSocial").val(result.SocialSecurityNo);
        $("#ddlRelationship").val(result.RelationShip).trigger('change');
        $("#txtOther").val(result.Other);
        $("#txtBirthday").val(result.Birthday);
    }
}
function showconfirm() {
    var r = confirm("Are You Sure To Delete?");
    if (r) {
        return true;
    }
    else {
        return false;
    }
}

$(document).on('change', ".chkfStillWork", function () {
    if ($(this).is(":checked") == true) {
        $("#txtEmpFromDate").attr('disabled', true);
        $("#txtEmpFromTo").attr('disabled', true);
        $("#txtEmpFromDate").css({ 'border': '1px solid #d2d6de' });
        $("#txtEmpFromTo").css({ 'border': '1px solid #d2d6de' });
        $("#txtEmpFromDate").val("");
        $("#txtEmpFromTo").val("");
    }
     else {
        $("#txtEmpFromDate").attr('disabled', false);
        $("#txtEmpFromTo").attr('disabled', false);
        $("#txtEmpFromDate").css({ 'border': '1px solid #d2d6de' });
        $("#txtEmpFromTo").css({ 'border': '1px solid #d2d6de' });
        $("#txtEmpFromDate").val("");
        $("#txtEmpFromTo").val("");
     }

});



