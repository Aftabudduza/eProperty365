var currentPagePath = window.location.pathname + "/";

var pathname = window.location.pathname; // Returns path only
var url = window.location.href;     // Returns full URL
var origin = window.location.origin;   // Returns base URL
var G_RelationalData = [];

$(document).ready(function (parameters) {
    $(".tDate").datepicker({
        dateFormat: "mm-dd-yy",
        changeYear: false,
        changeMonth: true
    });
    //$(".ddl").select2();
    //$('input[type="checkbox"].flat-red, input[type="radio"].flat-red').iCheck({
    //    checkboxClass: 'icheckbox_flat-green',
    //    radioClass: 'iradio_flat-green'
    //});
    //$(document).on('change', '#MainContent_FileUpload1', function (e) {
    //    var file = e.currentTarget.files[0];
    //    objectUrl = URL.createObjectURL(file);
    //    $("#vid").prop("src", objectUrl);
    //});
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
        G_RelationalData = [];
        G_RelationalData = $.parseJSON(decodeURIComponent(data.d));
        let RelationData = setCombo_withInt($.parseJSON(decodeURIComponent(data.d)), '-1');
        $(".relation option").empty();
        $(".relation").append(RelationData);
        $(".relation").select2();
    });

    Promise.all([Country, State, Relationship]).then(function () {
        LoadCreditInformation();
        LoadEmergencyGrid();
        LoadVehicle();
        LoadPeopleStayingInUnit();
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
function LoadCreditInformation(parameters) {
    var URL = "/Pages/Resident/ResidentialTenantRental_App_Page_2.aspx/" + "GetCreditInformationList";
    var obj = {};
    let Basic = makeAjaxCallReturnPromiss(URL, obj);
    Basic.then((data) => {
        if (data.d != null || data.d != "") {
            BindCreditInfo($.parseJSON(decodeURIComponent(data.d)));
            //BindBasicTenantInformationGrid($.parseJSON(decodeURIComponent(data.d)));
        }
    });
}
function LoadVehicle(parameters) {
    var URL = "/Pages/Resident/ResidentialTenantRental_App_Page_2.aspx/" + "GetVehicleformationList";
    var obj = {};
    let Basic = makeAjaxCallReturnPromiss(URL, obj);
    Basic.then((data) => {
        if (data.d != null || data.d != "") {
            BindVehicleInfo($.parseJSON(decodeURIComponent(data.d)));
            //BindBasicTenantInformationGrid($.parseJSON(decodeURIComponent(data.d)));
        }
    });
}
function LoadPeopleStayingInUnit(parameters) {
    var URL = "/Pages/Resident/ResidentialTenantRental_App_Page_2.aspx/" + "GetPeopleUnitformationList";
    var obj = {};
    let Basic = makeAjaxCallReturnPromiss(URL, obj);
    Basic.then((data) => {
        if (data.d != null || data.d != "") {
            BindPeopleUnitInfo($.parseJSON(decodeURIComponent(data.d)));
            //BindBasicTenantInformationGrid($.parseJSON(decodeURIComponent(data.d)));
        }
    });
}
function LoadEmergencyGrid(parameters) {
    var URL = "/Pages/Resident/ResidentialTenantRental_App_Page_2.aspx/" + "GetTenantEmergencyContactInformationList";
    var obj = {};
    let Basic = makeAjaxCallReturnPromiss(URL, obj);
    Basic.then((data) => {
        if (data.d != null || data.d != "") {
            BindEmergencyContact($.parseJSON(decodeURIComponent(data.d)));
            //BindBasicTenantInformationGrid($.parseJSON(decodeURIComponent(data.d)));
        }
    });
}

// ----------------------------------- Emergency Contact ------------------------------------//

function BindEmergencyContact(result) {
    let content = "";
    
    if (result.length > 0) {
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
            //content += "<td>" + obj.RelationshipName + "</td>";
            content += "<td><input type='button' value='Edit'  onclick='EditEmergencyGrid(this)' id='" + obj.Id + "'  class='custombtn'/><input type='button' value='Delete'  onclick='DeleteEmergency(this)' id='" + obj.Id + "'  class='custombtn'/></td>";
            //content += "<td><input type='button' value='Delete'  onclick='DeleteEmployeeGrid(this)' id='" + obj.Id + "'  class='custombtn'/></td>";
            content += "</tr>";
        });

        $("#tblEmergencyContact tbody").empty();
        $("#tblEmergencyContact tbody").append(content);
    }

}
$(document).on('click', '#btnAddAnotherContact', function (parameters) {
    
    if (ValidationForContactInformation()) {
        let Obj = getTheEmergencyObj();
        var URL = "/Pages/Resident/ResidentialTenantRental_App_Page_2.aspx/" + "SaveEmergencyContactInformation";
        let Basic = makeAjaxCallReturnPromiss(URL, Obj);
        Basic.then((data) => {
            if (data.d != null || data.d != "") {
                var res = $.parseJSON(decodeURIComponent(data.d));
                var mass = $("#btnAddAnotherContact").val();
                if (res.length > 0 && res != "") {
                    notify('success', " Emergency Contacts saved successfully");
                    ClearEmergencyContact();
                    BindEmergencyContact(res);
                } else {
                    notify('danger', "Emergency Contacts save Failed !!");
                }

            } else {
                notify('danger', "Save Failed !!");
            }
        });
    }
});
//---------- Clear Data -------------- //
function ClearEmergencyContact(parameters) {
    $("#hdRowId").val(0);
    $("#txtEmpName").val("");
    $("#txtEmpAddress").val("");
    $("#txtEmpAddress1").val("");
    //$("#ddlEmpCountry").val("-1").trigger('change');
    $("#txEmpRegion").val("");
    $("#cityEmpTxt").val("");
    $("#ddlEmpstateapp").val("-1").trigger('change');
    $("#Empzipcodeapp1Txt").val("");
    //$("#txtEmpmonthlyAmount").val("");
    //$("#txtEmpContactPersonName").val("");
    $("#txtEmpContactPersonPhone").val("");
    $("#ddlRelationship").val("-1").trigger('change');
    $("#txtRefEmailAddress").val("");
    $("#btnAddAnotherContact").val("Add another Contact");
}

function ValidationForContactInformation(parameters) {
    var isresult = true;
    var Name = $("#txtEmpName").val();
    var Address = $("#txtEmpAddress").val();
    var Address1 = $("#txtEmpAddress1").val();
    var Country = $("#ddlEmpCountry").val();
    var City = $("#cityEmpTxt").val();
    var ZipCode = $("#Empzipcodeapp1Txt").val();
    //var MonthlyIncome = $("#txtEmpmonthlyAmount").val();
    //var ContactPersonName = $("#txtEmpContactPersonName").val();
    var ContactPersonPhoneNo = $("#txtEmpContactPersonPhone").val();
    var Relationship = $("#txtrefRelationship").val();
   
   // var IsStillWorkthere = $("#StillWork").is(":checked") == true ? true : false;
  
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
    //if (MonthlyIncome === "undefined" || MonthlyIncome === "") {
    //    $("#txtEmpmonthlyAmount").css({ 'border': '1px solid red' });
    //    isresult = false;
    //}
    //else {
    //    $("#txtEmpmonthlyAmount").css({ 'border': '1px solid #d2d6de' });
    //}
    //if (ContactPersonName === "undefined" || ContactPersonName === "") {
    //    $("#txtEmpContactPersonName").css({ 'border': '1px solid red' });
    //    isresult = false;
    //}
    //else {
    //    $("#txtEmpContactPersonName").css({ 'border': '1px solid #d2d6de' });
    //}
    //if (ContactPersonPhoneNo === "undefined" || ContactPersonPhoneNo === "") {
    //    $("#txtEmpContactPersonPhone").css({ 'border': '1px solid red' });
    //    isresult = false;
    //}
    //else {
    //    $("#txtEmpContactPersonPhone").css({ 'border': '1px solid #d2d6de' });
    //}
    //if (Relationship === "undefined" || Relationship === "") {
    //    $("#s2id_ddlRelationship").css({ 'border': '1px solid red' });
    //    isresult = false;
    //}
    //else {
    //    $("#s2id_ddlRelationship").css({ 'border': '1px solid #d2d6de' });
    //}
    

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
        Name: $("#txtEmpName").val(),
        Address: $("#txtEmpAddress").val(),
        Address1: $("#txtEmpAddress1").val(),
        Country: $("#ddlEmpCountry").val(),
        CountryName: $("#ddlEmpCountry option:selected").text(),
        Region: $("#txEmpRegion").val(),
        City: $("#cityEmpTxt").val(),
        State: $("#ddlEmpstateapp").val(),
        StateName: $("#ddlEmpstateapp option:selected").text(),
        ZipCode: $("#Empzipcodeapp1Txt").val(),
         MonthlyIncome: "0",
        ContactPersonName: "",
        //MonthlyIncome: $("#txtEmpmonthlyAmount").val(),
        //ContactPersonName: $("#txtEmpContactPersonName").val(),
        ContactPersonPhoneNo: $("#txtEmpContactPersonPhone").val(),
        Relationship: $("#ddlRelationship").val(),
        RelationshipName: $("#ddlRelationship option:selected").text(),
        EmailAddress: $("#txtRefEmailAddress").val()
    }
    return Obj;
}

function DeleteEmergency(curtd) {
    var id = $(curtd).attr('Id');
    if (showconfirm()) {
        var URL = "/Pages/Resident/ResidentialTenantRental_App_Page_2.aspx/" + "DeleteTenantEmergencyInfo";
        var obj = {
            Id: id
        };
        let Basic = makeAjaxCallReturnPromiss(URL, obj);
        Basic.then((data) => {
            if (data.d != null || data.d != "") {
                var res = $.parseJSON(decodeURIComponent(data.d));
                if (res.length > 0 && res != null) {
                    notify('success', "Emergency Contact Deleted successfully");
                    BindEmergencyContact(res);
                } else {
                    notify('danger', "Emergency Contact Delete Failed !!");
                }

            }
        });
    }

}
function EditEmergencyGrid(curtd) {
    
    var id = $(curtd).attr('Id');
    var URL = "/Pages/Resident/ResidentialTenantRental_App_Page_2.aspx/" + "EditTenantEmergency";
    var obj = {
        Id: id
    };
    let Basic = makeAjaxCallReturnPromiss(URL, obj);
    Basic.then((data) => {
        if (data.d != null || data.d != "") {
            BindEmergencyContactToField($.parseJSON(decodeURIComponent(data.d)));
        }
    });

}

function BindEmergencyContactToField(result) {
    if (result != "null") {
        $("#hdRowId").val(result.Id);
        $("#txtEmpName").val(result.Name);
        $("#txtEmpAddress").val(result.Address);
        $("#txtEmpAddress1").val(result.Address1);
        $("#ddlEmpCountry").val(result.Country).trigger('change');
        $("#txEmpRegion").val(result.Region);
        $("#cityEmpTxt").val(result.City);
        $("#Empzipcodeapp1Txt").val(result.ZipCode);
        $("#ddlEmpstateapp").val(result.State).trigger('change');
        //$("#txtEmpmonthlyAmount").val(result.MonthlyIncome);
        //$("#txtEmpContactPersonName").val(result.ContactPersonName);
        $("#txtEmpContactPersonPhone").val(result.ContactPersonPhoneNo);
        $("#ddlRelationship").val(result.Relationship).trigger('change');
        $("#txtRefEmailAddress").val(result.EmailAddress);
        $("#btnAddAnotherContact").val("Update Emergency Contact");
    }
}

function BindCreditInfo(result) {
    
    if (result.length>0) {
        $("#hdsavingId").val(result[0].Id);
        $("#txtsavingBank1").val(result[0].SavingsAcc_BankName_1);
        $("#txtsavingBalance1").val(result[0].SavingsAcc_Balance_1);
        $("#txtsavingBank2").val(result[0].SavingsAcc_BankName_2);
        $("#txtsavingBalance2").val(result[0].SavingsAcc_Balance_2);
        $("#txtCheckingBank1").val(result[0].CheckingAcc_BankName_1);
        $("#txtCheckingBalance1").val(result[0].CheckingAcc_Balance_1);
        $("#txtCheckingBank2").val(result[0].CheckingAcc_BankName_2);
        $("#txtCheckingBalance2").val(result[0].CheckingAcc_Balance_2);
        $("#txtAllCreditBank").val(result[0].All_Credit_Cards_BankName);
        $("#txtAllCreditBalance").val(result[0].All_Credit_Cards_Balance);
        $("#txtAutoLoanBank").val(result[0].Auto_Credit_Cards_Balance);
        $("#txtAutoLoanBalance").val(result[0].Auto_Loan_BankName);
        $("#txtOthrBank").val(result[0].Other_BankName);
        $("#txtotherBalance").val(result[0].Other_Balance);

    }
}


function BindVehicleInfo(result) {
    var content = "";
    if (result.length > 0) {
        
        for (var i = 0; i < 3; i++) {
            if (result[i] != null) {
                content += "<tr>";
                content += "<td style='width: 50px'><input style='width: 90px' id='txtMake1' value='" + result[i].Make + "' /><input id='hdRowId' value='" + result[i].Id + "' type='hidden'/></td>";
                content += " <td  style='width: 50px'><input style='width: 90px' id='txtModel1' value='" + result[i].Model + "' /></td>";
                content += "<td  style='width: 50px'><input style='width: 90px' id='txtColor1' value='" + result[i].Color + "' /></td>";
                content += "<td  style='width: 50px'><input style='width: 90px' id='txtYear1' value='" + result[i].Year + "'/></td>";
                content += "<td  style='width: 50px'><input style='width: 90px' id='txtLicensePlate1' value='" + result[i].LicensePlate + "'/></td>";
                content += "</tr>";
            } else {
                content += "<tr>";
                content += "<td style='width: 50px'><input style='width: 90px' id='txtMake1' /><input id='hdRowId' value='0' type='hidden'/></td>";
                content += "<td  style='width: 50px'><input style='width: 90px' id='txtModel1' /></td>";
                content += "<td  style='width: 50px'><input style='width: 90px' id='txtColor1' /></td>";
                content += "<td  style='width: 50px'><input style='width: 90px' id='txtYear1' /></td>";
                content += "<td  style='width: 50px'><input style='width: 90px' id='txtLicensePlate1' /></td>";
                content += "</tr>";
            }
        }
       
    } else {
        content += "<tr>";
        content += "<td style='width: 50px'><input style='width: 90px' id='txtMake1' /><input id='hdRowId' value='0' type='hidden'/></td>";
        content += " <td  style='width: 50px'><input style='width: 90px' id='txtModel1' /></td>";
        content += "<td  style='width: 50px'><input style='width: 90px' id='txtColor1' /></td>";
        content += "<td  style='width: 50px'><input style='width: 90px' id='txtYear1' /></td>";
        content += "<td  style='width: 50px'><input style='width: 90px' id='txtLicensePlate1' /></td>";
        content += "</tr>";
        content += "<tr>";
        content += "<td style='width: 50px'><input style='width: 90px' id='txtMake1' /><input id='hdRowId' value='0' type='hidden'/></td>";
        content += " <td  style='width: 50px'><input style='width: 90px' id='txtModel1' /></td>";
        content += "<td  style='width: 50px'><input style='width: 90px' id='txtColor1' /></td>";
        content += "<td  style='width: 50px'><input style='width: 90px' id='txtYear1' /></td>";
        content += "<td  style='width: 50px'><input style='width: 90px' id='txtLicensePlate1' /></td>";
        content += "</tr>";
        content += "<tr>";
        content += "<td style='width: 50px'><input style='width: 90px' id='txtMake1' /><input id='hdRowId' value='0' type='hidden'/></td>";
        content += " <td  style='width: 50px'><input style='width: 90px' id='txtModel1' /></td>";
        content += "<td  style='width: 50px'><input style='width: 90px' id='txtColor1' /></td>";
        content += "<td  style='width: 50px'><input style='width: 90px' id='txtYear1' /></td>";
        content += "<td  style='width: 50px'><input style='width: 90px' id='txtLicensePlate1' /></td>";
        content += "</tr>";
    }
    $("#tblVehicle tbody").empty();
    $("#tblVehicle tbody").append(content);
}

function BindPeopleUnitInfo(result) {
    var content = "";
    //G_RelationalData
    
    if (result.length > 0) {
        
        for (var i = 0; i < 6; i++) {
            if (result[i] != null) {
                content += "<tr>";
                content += "<td><input type='text' id='txtName1' class='from-control' value='" + result[i].Name + "'/><input id='hdRowId' value='" + result[i].Id + "' type='hidden'/></td>";
                content += " <td style='text-align:left;'> <select id='ddlPeopleRelationship' styel='with:100%' class='rel'>'" + setCombo_withInt(G_RelationalData, result[i].Relationship) + "</select></td>";
                content += "<td ><input type='text' id='txtAge1' class='from-control' value='" + result[i].Age + "'/></td>";
                content += "</tr>";
            } else {
                content += "<tr>";
                content += "<td><input type='text' id='txtName1' class='from-control'/><input id='hdRowId' value='0' type='hidden'/></td>";
                content += " <td style='text-align:left;'><select id='ddlPeopleRelationship' class='rel'>'" + setCombo_withInt(G_RelationalData, '-1') + "</select></td>";
                content += "<td ><input type='text' id='txtAge1' class='from-control'/></td>";
                content += "</tr>";
            }
        }

    } else {
        content += "<tr>";
        content += "<td><input type='text' id='txtName1' class='from-control'/><input id='hdRowId' value='0' type='hidden'/></td>";
        content += " <td style='text-align:left;'><select id='ddlPeopleRelationship' class='rel'>'" + setCombo_withInt(G_RelationalData, '-1') + "</select></td>";
        content += "<td ><input type='text' id='txtAge1' class='from-control'/></td>";
        content += "</tr>";
        content += "<tr>";
        content += "<td><input type='text' id='txtName1' class='from-control'/><input id='hdRowId' value='0' type='hidden'/></td>";
        content += " <td style='text-align:left;'><select id='ddlPeopleRelationship' class='rel'>'" + setCombo_withInt(G_RelationalData, '-1') + "</select></td>";
        content += "<td ><input type='text' id='txtAge1' class='from-control'/></td>";
        content += "</tr>";
        content += "<tr>";
        content += "<td><input type='text' id='txtName1' class='from-control'/><input id='hdRowId' value='0' type='hidden'/></td>";
        content += " <td style='text-align:left;'><select id='ddlPeopleRelationship' class='rel'>'" + setCombo_withInt(G_RelationalData, '-1') + "</select></td>";
        content += "<td ><input type='text' id='txtAge1' class='from-control'/></td>";
        content += "</tr>";
        content += "<tr>";
        content += "<td><input type='text' id='txtName1' class='from-control'/><input id='hdRowId' value='0' type='hidden'/></td>";
        content += " <td style='text-align:left;'><select id='ddlPeopleRelationship' class='rel'>'" + setCombo_withInt(G_RelationalData, '-1') + "</select></td>";
        content += "<td ><input type='text' id='txtAge1' class='from-control'/></td>";
        content += "</tr>";
        content += "<tr>";
        content += "<td><input type='text' id='txtName1' class='from-control'/><input id='hdRowId' value='0' type='hidden'/></td>";
        content += " <td style='text-align:left;'><select id='ddlPeopleRelationship' class='rel'>'" + setCombo_withInt(G_RelationalData, '-1') + "</select></td>";
        content += "<td ><input type='text' id='txtAge1' class='from-control'/></td>";
        content += "</tr>";
        content += "<tr>";
        content += "<td><input type='text' id='txtName1' class='from-control'/><input id='hdRowId' value='0' type='hidden'/></td>";
        content += " <td style='text-align:left;'><select id='ddlPeopleRelationship' class='rel'>'" + setCombo_withInt(G_RelationalData, '-1') + "</select></td>";
        content += "<td ><input type='text' id='txtAge1' class='from-control'/></td>";
        content += "</tr>";
    }
    $("#tblPeople tbody").empty();
    $("#tblPeople tbody").append(content);
    $(".rel").select2();
}
//--------------------------------------------- Save And Continue ---------------------------------------//
$(document).on('click', '#btnContinue', function (parameters) {
    
    var emergency = $("#tblEmergencyContact tbody tr").length;
    if (emergency >0) {
        var loadCreditCard = GetCreditCard();
        var vehicle = GetVehicle();
        var PeopleUnit = GetPeopleUnit();

        var obj= {
            Credit: loadCreditCard,
            Vehicles: vehicle,
            PeopleStayingUnit: PeopleUnit
        }

        var URL = "/Pages/Resident/ResidentialTenantRental_App_Page_2.aspx/" + "SaveTetantCreditVehicleAndUnitInformation";
        let Basic = makeAjaxCallReturnPromiss(URL, obj);
        Basic.then((data) => {
            if (data.d != null || data.d != "") {
                var res = $.parseJSON(decodeURIComponent(data.d));
                if (res == true) {
                    window.location.href = "/Pages/Resident/ResidentialTenantRental_App_Page_3.aspx";
                } else {
                    notify('danger', "Save Failed !!");
                }
                //BindEmergencyContactToField($.parseJSON(decodeURIComponent(data.d)));
            }
        });
    }    
    else {
        notify('danger', "Please fill out at least one Emergency Contact.");
    }
    
});


function GetCreditCard(parameters) {
    var obj = {
        Id:$("#hdsavingId").val(),
        SavingsAcc_BankName_1: $("#txtsavingBank1").val() ,
        SavingsAcc_Balance_1: $("#txtsavingBalance1").val(),
        SavingsAcc_BankName_2: $("#txtsavingBank2").val() ,
        SavingsAcc_Balance_2:$("#txtsavingBalance2").val() ,
        CheckingAcc_BankName_1:$("#txtCheckingBank1").val() ,
        CheckingAcc_Balance_1:$("#txtCheckingBalance1").val() ,
        CheckingAcc_BankName_2:$("#txtCheckingBank2").val() ,
        CheckingAcc_Balance_2:$("#txtCheckingBalance2").val() ,
        All_Credit_Cards_BankName:$("#txtAllCreditBank").val() ,
        All_Credit_Cards_Balance:$("#txtAllCreditBalance").val() ,
        Auto_Loan_BankName: $("#txtAutoLoanBank").val() ,
        Auto_Credit_Cards_Balance :$("#txtAutoLoanBalance") .val() ,
        Other_BankName:$("#txtOthrBank").val() ,
        Other_Balance: $("#txtotherBalance").val()

    }
    return obj;
}

function GetVehicle(parameters) {
    var arrVehicale = [];
    $("#tblVehicle tbody tr").each(function (parameters) {
        
        if ($($($(this).find("td:eq(0)").children())[0]).val() !== "" && $($($(this).find("td:eq(1)").children())[0]).val() !== "" && $($($(this).find("td:eq(2)").children())[0]).val() !=="") {
            var obj = {
                Id: $($(this).find("td:eq(0)").find('input[type=hidden]')).val(),
                Make: $($($(this).find("td:eq(0)").children())[0]).val(),
                Model: $($($(this).find("td:eq(1)").children())[0]).val(),
                Color: $($($(this).find("td:eq(2)").children())[0]).val(),
                Year: $($($(this).find("td:eq(3)").children())[0]).val(),
                LicensePlate: $($($(this).find("td:eq(4)").children())[0]).val()
            }
            arrVehicale.push(obj);
        }
       
    });
    return arrVehicale;
}

function GetPeopleUnit(parameters) {
    var arrPeople = [];
    $("#tblPeople tbody tr").each(function (parameters) {
        if ($($($(this).find("td:eq(0)").children())[0]).val() !== "" && $($(this).find("td:eq(1)").find('select option:selected')).val() !="-1") {
            var obj = {
                Id: $($(this).find("td:eq(0)").find('input[type=hidden]')).val(),
                Name: $($($(this).find("td:eq(0)").children())[0]).val(),
                Relationship: $($(this).find("td:eq(1)").find('select option:selected')).val(),
                RelationshipName: $($(this).find("td:eq(1)").find('select option:selected')).text(),
                Age: $($($(this).find("td:eq(2)").children())[0]).val()
            }
            arrPeople.push(obj);
        }
       
    });
    return arrPeople;
}

$(document).on('click', '#btnBack', function (parameters) {
    var origin = window.location.origin; // Returns base URL
    var url = origin + "/Pages/Resident/ResidentialTenantRental_App_Page_1.aspx";
    window.location.href = url;
});

$(document).on('click', '#btnExit', function (parameters) {
    
    var origin = window.location.origin; // Returns base URL
    var url = origin + "/Pages/DashboardAdmin.aspx";
    window.location.href = url;
});