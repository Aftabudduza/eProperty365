
var currentPagePath = window.location.pathname + "/";

var pathname = window.location.pathname; // Returns path only
var url = window.location.href;     // Returns full URL
var origin = window.location.origin;   // Returns base URL
var G_RelationalData = [];
$(document).ready(function (parameters) {
    LoadComboBox();
    
});
function LoadComboBox(parameters) {
    $(".tDate").datepicker({
        dateFormat: "mm-dd-yy",
        changeYear: false,
        changeMonth: true
    });
    var content = '<option value="-1">Select.......</option>';
    for (var i = 1; i <= 8; i++) {
        content += '<option value="' + i + '">' + i + '</option>';
    }
    $(".people option").empty();
    $(".people").append(content);
    $(".people").val("-1").trigger('change');
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
        LoadVehicle();
        LoadPeopleStayingInUnit();
        loadProfileInfo();
        LoadEmergencyGrid();
    });

   
}

function loadProfileInfo(parameters) {
    var URL = "/Pages/Resident/AddNewTenantProfile.aspx/" + "GetProfileInfoList";
    var obj = {};
    let Basic = makeAjaxCallReturnPromiss(URL, obj);
    Basic.then((data) => {
        if (data.d != null || data.d != "") {
            
            BindProfileInfo($.parseJSON(decodeURIComponent(data.d)));
            //BindBasicTenantInformationGrid($.parseJSON(decodeURIComponent(data.d)));
        }
    });
}

function BindProfileInfo(res) {
    $("#txtEmailUser").val("");
    $("#txtReEmailUser").val("");
    $("#txtPassword").val("");
    $("#txtRePassword").val("");

    var content = "";
    if (res != null) {
        $("#txtUnitLocation").text(res.LocationId);
        $("#txtUnitId").text(res.UnitId);
        $("#txtEmailUser").val(res.EmailAddress);
        $("#txtReEmailUser").val(res.EmailAddress);
        $("#txtFirstName").val(res.FirstName);
        $("#txtLastName").val(res.LastName);
        $("#txtEmailAddress").val(res.EmailAddress);
        $("#txtSecurityDeposit").text(res.SecurityDeposit);
        $("#txtOneMonthRent").text(res.MonthlyRentHeld);
        $("#txtOtherAmountHeld").text(res.OtherAmountHeld);
        $("#txtLeaseSignerDate").text(res.LeaseSignDate);
        $("#txtDateOfPaymentDue").text(res.MonthlyPayDueDate);

        //var tenant = res.aggrementNameOf;
        //if (tenant != null) {
        //    $("#txtUnitLocation").text(tenant.LocationId);
        //    $("#txtUnitId").text(tenant.UnitId);
        //    $("#txtEmailUser").text(tenant.EmailAddress); 
        //    $("#txtReEmailUser").text(tenant.EmailAddress);
        //    $("#txtFirstName").val(tenant.FirstName);          
        //    $("#txtLastName").val(tenant.LastName);
        //    $("#txtEmailAddress").val(tenant.EmailAddress);
        //    $("#txtSecurityDeposit").text(tenant.SecurityDeposit);
        //    $("#txtOneMonthRent").text(tenant.MonthlyRentHeld);
        //    $("#txtOtherAmountHeld").val(tenant.OtherAmountHeld);
        //    $("#txtLeaseSignerDate").text(tenant.LeaseSignDate);
        //    $("#txtDateOfPaymentDue").text(tenant.MonthlyPayDueDate);
        //}

        //var Emergency = res.Emergency;
        //if (Emergency.length > 0) {
        //   // for (var i = 0; i < Emergency.length; i++) {
        //        if (Emergency != null) {
        //            $.each(Emergency, function (i, obj) {
        //                content += "<tr>";
        //                content += "<td>" + obj.Name + "</td>";
        //                content += "<td>" + obj.Address + "</td>";
        //                content += "<td>" + obj.Address1 + "</td>";
        //                content += "<td>" + obj.City + "</td>";
        //                content += "<td>" + obj.StateName + "</td>";
        //                content += "<td>" + obj.ZipCode + "</td>";
        //                content += "<td>" + obj.ContactPersonName + "</td>";
        //                content += "<td>" + obj.ContactPersonPhoneNo + "</td>";
        //                content += "<td>" + obj.RelationshipName + "</td>";
        //                content += "<td><input type='button' value='Edit'  onclick='EditEmergencyGrid(this)' id='" + obj.Id + "'  class='custombtn'/><input type='button' value='Delete'  onclick='DeleteEmergency(this)' id='" + obj.Id + "'  class='custombtn'/></td>";
        //                //content += "<td><input type='button' value='Delete'  onclick='DeleteEmployeeGrid(this)' id='" + obj.Id + "'  class='custombtn'/></td>";
        //                content += "</tr>";
        //            });

        //            $("#tblEmergencyContact tbody").empty();
        //            $("#tblEmergencyContact tbody").append(content);
        //        } 
        //   // }
        //}
        //BindVehicleInfo(res.Vehicle);
        //BindPeopleUnitInfo(res.People);
    }
}

function BindEmergencyContact(Emergency) {
    var content = "";
    if (Emergency.length > 0) {
        // for (var i = 0; i < Emergency.length; i++) {
        if (Emergency != null) {
            $.each(Emergency, function (i, obj) {
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
        // }
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
        $("#txtEmailEmergency").val(result.EmailAddress);
        $("#btnAddAnotherContact").val("Update Emergency Contact");
    }
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
        EmailAddress: $("#txtEmailEmergency").val()
    }
    return Obj;
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
   
    if (isresult)
        isresult = true;
    else {
        isresult = false;
    }
    return isresult;
}
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
    $("#txtEmailEmergency").val("");
    $("#btnAddAnotherContact").val("Add another Contact");
}
$(document).on('click', '#btnAddanotherContact', function (parameters) {
    debugger;
    if (ValidationForContactInformation()) {
        let Obj = getTheEmergencyObj();
        var URL = "/Pages/Resident/AddNewTenantProfile.aspx/" + "SaveEmergencyContactInformation";
        let Basic = makeAjaxCallReturnPromiss(URL, Obj);
        Basic.then((data) => {
            
            if (data.d != null || data.d != "") {
                var res = $.parseJSON(decodeURIComponent(data.d));
                var mass = $("#btnAddAnotherContact").val();
                if (res.length > 0 && res != "") {
                    notify('success', " Emergency Contact saved successfully");
                    ClearEmergencyContact();
                    BindEmergencyContact(res);
                } else {
                    notify('danger', mass + " save Failed !!");
                }

            } else {
                notify('danger', "Save Failed !!");
            }
        });
    }
    else {
        notify('danger', "Please fill out required information");
    }
});
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
        RelationShip: $("#ddlUserRelationship").val(),
        Other: $("#txtOther").val(),
        Birthday: $("#txtBirthday").val(),
        NoOfPeopleLivingUnit: $("#ddlNoPeopleLiving").val(),
        NoOfPeopleLiving18: $("#ddlPeopleUnderAge").val()

}
    return Obj;
}
function GetVehicle(parameters) {
    var arrVehicale = [];
    $("#tblVehicle tbody tr").each(function (parameters) {
        if ($($($(this).find("td:eq(0)").children())[0]).val() !== "" && $($($(this).find("td:eq(1)").children())[0]).val() !== "" && $($($(this).find("td:eq(2)").children())[0]).val() !== "") {
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
        if ($($($(this).find("td:eq(0)").children())[0]).val() !== "" && $($(this).find("td:eq(1)").find('select option:selected')).val() != "-1") {
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
$(document).on('click', '#btnSave', function (parameters) {
    debugger;
    if (ValidateAlltheGridAndBasicDataForContinue()) {
     var emergency = $("#tblEmergencyContact tbody tr").length;
     if (emergency > 0) {
             var pagePath = window.location.pathname + "/ExistTenant";
             var obj = {};
             var bExist = false;
             $.ajax({
                 type: "POST",
                 url: pagePath,
                 data: JSON.stringify(obj),
                 contentType: "application/json; charset=utf-8",
                 dataType: "json",
                 error:
                     function (XMLHttpRequest, textStatus, errorThrown) {
                         bExist = false;
                     },
                 success:
                     function (result) {
                         var lstofFeatureName = $.parseJSON(decodeURIComponent(result.d));
                         var content = "";
                         if (lstofFeatureName != null) {
                             if (lstofFeatureName.Status == "Approved" && lstofFeatureName.Email != $.trim($("#txtEmailUser").val())) {
                                 bExist = true;
                                 notify('danger', "This unit is rented !!");
                             }
                             else if (lstofFeatureName.Status == "Review" && lstofFeatureName.Email != $.trim($("#txtEmailUser").val())) {
                                 notify('danger', "This unit is under review with another Tenant !!");
                             }
                             else {
                                 var loadaggrementNameOf = getTheBasicObj();
                                 var vehicle = GetVehicle();
                                 var PeopleUnit = GetPeopleUnit();
                                 var Password = $("#txtPassword").val();

                                 var Obj = {
                                     aggrementNameOf: loadaggrementNameOf,
                                     Vehicles: vehicle,
                                     PeopleStayingUnit: PeopleUnit,
                                     Password: Password
                                 }

                                 var URL = "/Pages/Resident/AddNewTenantProfile.aspx/" + "SaveTetantCreditVehicleAndUnitInformation";
                                 let Basic = makeAjaxCallReturnPromiss(URL, Obj);
                                 Basic.then((data) => {
                                     if (data.d != null || data.d != "") {
                                         var res = $.parseJSON(decodeURIComponent(data.d));

                                         if (res == true) {
                                             notify('success', "Saved successfully");
                                             window.location.href = "/Pages/Resident/ResidentTenantDashboard.aspx";
                                         } else {
                                             notify('danger', "Save Failed !!");
                                         }
                                    
                                     }
                                     else {
                                         notify('danger', "Save Failed !!");
                                     }
                                 });

                             }
                         }
                     }
             });           
            }
            else {
                notify('danger', "Please Add Emergency Contacts");
            }
        }
    else
    {
        notify('danger', "Please Add Agreement Information above");
    }

   
});

$(document).on('click', '#btnIn', function(parameters) {
    window.location.href = "/Pages/Resident/ResidentTenantDashboard.aspx";
});

function ValidateAlltheGridAndBasicDataForContinue(parameters) {
    var isresult = true;
    var Name = $("#txtFirstName").val();
    var LastName = $("#txtLastName").val();
    var Address = $("#txtAddress").val();  
    var Country = $("#ddlCountry").val();
    var City = $("#cityapp1Txt").val();
    var ZipCode = $("#zipcodeapp1Txt").val();
    var Email = $("#txtEmailAddress").val();  
    var Birthdate = $("#txtBirthday").val();

    var UserEmail = $("#txtEmailUser").val();
    var ReUserEmail = $("#txtReEmailUser").val();
    var Password = $("#txtPassword").val();
    var RePassword = $("#txtRePassword").val();

    if (Name === "undefined" || Name === "") {
        $("#txtFirstName").css({ 'border': '1px solid red' });
        isresult = false;
    }
    else {
        $("#txtFirstName").css({ 'border': '1px solid #d2d6de' });
    }

    if (LastName === "undefined" || LastName === "") {
        $("#txtLastName").css({ 'border': '1px solid red' });
        isresult = false;
    }
    else {
        $("#txtLastName").css({ 'border': '1px solid #d2d6de' });
    }

    if (Address === "undefined" || Address === "") {
        $("#txtAddress").css({ 'border': '1px solid red' });
        isresult = false;
    }
    else {
        $("#txtAddress").css({ 'border': '1px solid #d2d6de' });
    }   

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
    
    if (Birthdate === "undefined" || Birthdate === "") {
        $("#txtBirthday").css({ 'border': '1px solid red' });
        isresult = false;
    }
    else {
        $("#txtBirthday").css({ 'border': '1px solid #d2d6de' });
    }
   
    if (UserEmail === "undefined" || UserEmail === "") {
        $("#txtEmailUser").css({ 'border': '1px solid red' });
        isresult = false;
    }
    else {
        $("#txtEmailUser").css({ 'border': '1px solid #d2d6de' });
    }

    if (ReUserEmail === "undefined" || ReUserEmail === "") {
        $("#txtReEmailUser").css({ 'border': '1px solid red' });
        isresult = false;
    }
    else {
        $("#txtReEmailUser").css({ 'border': '1px solid #d2d6de' });
    }
    if (Password === "undefined" || Password === "") {
        $("#txtPassword").css({ 'border': '1px solid red' });
        isresult = false;
    }
    else {
        $("#txtPassword").css({ 'border': '1px solid #d2d6de' });
    }

    if (RePassword === "undefined" || RePassword === "") {
        $("#txtRePassword").css({ 'border': '1px solid red' });
        isresult = false;
    }
    else {
        $("#txtRePassword").css({ 'border': '1px solid #d2d6de' });
    }

    if (UserEmail != ReUserEmail) {
        $("#txtReEmailUser").css({ 'border': '1px solid red' });
        isresult = false;
    }
    else {
        $("#txtReEmailUser").css({ 'border': '1px solid #d2d6de' });
    }
    if (Password != RePassword) {
        $("#txtRePassword").css({ 'border': '1px solid red' });
        isresult = false;
    }
    else {
        $("#txtRePassword").css({ 'border': '1px solid #d2d6de' });
    }

    if (isresult)
        isresult = true;
    else {
        isresult = false;
    }
    return isresult;
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