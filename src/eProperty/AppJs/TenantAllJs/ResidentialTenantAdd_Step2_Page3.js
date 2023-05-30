var currentPagePath = window.location.pathname + "/";

var pathname = window.location.pathname; // Returns path only
var url = window.location.href;     // Returns full URL
var origin = window.location.origin;   // Returns base URL
var G_RelationalData = [];

$(document).ready(function (parameters) {
    $('input[type="checkbox"].flat-red, input[type="radio"].flat-red').iCheck({
        checkboxClass: 'icheckbox_flat-green',
        radioClass: 'iradio_flat-green'
    });
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
        LoadGeneralIfo();
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
function LoadGeneralIfo(parameters) {
    
    var URL = "/Pages/Resident/ResidentialTenantRental_App_Page_3.aspx/" + "GetGeneralInformationList";
    var obj = {};
    let Basic = makeAjaxCallReturnPromiss(URL, obj);
    Basic.then((data) => {
        if (data.d != null || data.d != "") {
            var res = $.parseJSON(decodeURIComponent(data.d));
            BindGeneralInfo(res[0]);
            //BindBasicTenantInformationGrid($.parseJSON(decodeURIComponent(data.d)));
        }
    });
}


function BindGeneralInfo(result) {
    //$('input[name=r3]:checked').val(),
    
    $("#hdRowId").val(result.Id);
    $('input[name=late]').closest('div').removeClass('checked');
    $('input[name=late]').attr('checked', false);
    $("input[name=late][value='" + result.LateRentNotice + "']").closest('div').addClass('checked');
    $("input[name=late][value='" + result.LateRentNotice + "']").attr('checked', true);

    $('input[name=smoke]').closest('div').removeClass('checked');
    $('input[name=smoke]').attr('checked', false);
    $("input[name=smoke][value='" + result.PropertySmoke + "']").closest('div').addClass('checked');
    $("input[name=smoke][value='" + result.PropertySmoke + "']").attr('checked', true);


    $("#ddlRenting").val(result.RentingFrom).trigger('change');
    $("#ddlRenting").select2();

    $('input[name=bank]').closest('div').removeClass('checked');
    $('input[name=bank]').attr('checked', false);
    $("input[name=bank][value='" + result.Bankruptcy + "']").closest('div').addClass('checked');
    $("input[name=bank][value='" + result.Bankruptcy + "']").attr('checked', true);
    
    $("#txtbankruptcy").val(result.BankruptcyDate);
    //$("#ddlMoveIn").val(result.MoveIn).trigger('change');
    //$("#ddlMoveIn").select2();
    $("#txtMoveIn").val(result.MoveIn);

    $('input[name=felony]').closest('div').removeClass('checked');
    $('input[name=felony]').attr('checked', false);
    $("input[name=felony][value='" + result.Felony + "']").closest('div').addClass('checked');
    $("input[name=felony][value='" + result.Felony + "']").attr('checked', true);



    $('input[name=eviction]').closest('div').removeClass('checked');
    $('input[name=eviction]').attr('checked', false);
    $("input[name=eviction][value='" + result.Eviction + "']").closest('div').addClass('checked');
    $("input[name=eviction][value='" + result.Eviction + "']").attr('checked', true);
    $("#txtwhen").val(result.EvictionWhen);
    $("#txtPetsDetails").val(result.PetsDetails);

    $('input[name=reoccurring]').closest('div').removeClass('checked');
    $('input[name=reoccurring]').attr('checked', false);
    $("input[name=reoccurring][value='" + result.Reoccurringproblems + "']").closest('div').addClass('checked');
    $("input[name=reoccurring][value='" + result.Reoccurringproblems + "']").attr('checked', true);

    $("#txtreoccurring").val(result.ReoccurringProblemsWhen);
    $("#txtMovingReason").val(result.MovingReason);
    $("#txtAmountOfIncome").val(result.VerifiableIncome);

    $("#txtFinancialProblem").val(result.LoanHelpingPersons);

    $('input[name=lawsuit]').closest('div').removeClass('checked');
    $('input[name=lawsuit]').attr('checked', false);
    $("input[name=lawsuit][value='" + result.LawsuitParty + "']").closest('div').addClass('checked');
    $("input[name=lawsuit][value='" + result.LawsuitParty + "']").attr('checked', true);
    $("#txtlawsuit").val(result.LawsuitPartyReason);
   

    $('input[name=creditcheck]').closest('div').removeClass('checked');
    $('input[name=creditcheck]').attr('checked', false);
    $("input[name=creditcheck][value='" + result.CriminalBackground + "']").closest('div').addClass('checked');
    $("input[name=creditcheck][value='" + result.CriminalBackground + "']").attr('checked', true);

    $("#txtcreditcheck").val(result.CriminalBackgroundReason);
    $("#txtthisApartment").val(result.AboutApartment);

    $("#txtOtherPeopleForAppartment").val(result.ApartmentRefererPersonDetails);

   


}

//---------- Clear Data -------------- //

function ValidationForGeneralInfo(parameters) {
    
    var isresult = true;
    var Pets = $("#txtPetsDetails").val();
    var RecuringProblem = $("#txtreoccurring").val();
    var movingReason = $("#txtMovingReason").val();
   

    var bReoccurringproblem = $('input[name=reoccurring]:checked').val();
    var bBankruptcyDate = $('input[name=bank]:checked').val(); 
    var bEvictionwhen = $('input[name=eviction]:checked').val();

    var BankruptcyDate = $("#txtbankruptcy").val();
    var Evictionwhen = $("#txtwhen").val();
    var MoveIn = $("#txtMoveIn").val();
    // var IsStillWorkthere = $("#StillWork").is(":checked") == true ? true : false;

    var RentingFrom = $("#ddlRenting").val(); 

    if (Pets === "undefined" || Pets === "") {
        $("#txtPetsDetails").css({ 'border': '1px solid red' });
        isresult = false;
    }
    else {
        $("#txtPetsDetails").css({ 'border': '1px solid #d2d6de' });
    }
   
    if (RentingFrom === "undefined" || RentingFrom === "-1") {
        $("#ddlRenting").css({ 'border': '1px solid red' });
        isresult = false;
    }
    else {
        $("#ddlRenting").css({ 'border': '1px solid #d2d6de' });
    }

     if (MoveIn === "undefined" || MoveIn === "") {
        $("#txtMoveIn").css({ 'border': '1px solid red' });
        isresult = false;
    }
    else {
        $("#txtMoveIn").css({ 'border': '1px solid #d2d6de' });
    }

    if (movingReason === "undefined" || movingReason === "") {
        $("#txtMovingReason").css({ 'border': '1px solid red' });
        isresult = false;
    }
    else {
        $("#txtMovingReason").css({ 'border': '1px solid #d2d6de' });
    }

    if (bReoccurringproblem === 'Yes') {
         if (RecuringProblem === "undefined" || RecuringProblem === "") {
            $("#txtreoccurring").css({ 'border': '1px solid red' });
            isresult = false;
        }
        else {
            $("#txtreoccurring").css({ 'border': '1px solid #d2d6de' });
        }
    }
    if (bBankruptcyDate ===  'Yes') {
        if (BankruptcyDate === "undefined" || BankruptcyDate === "") {
            $("#txtbankruptcy").css({ 'border': '1px solid red' });
            isresult = false;
        }
        else {
            $("#txtbankruptcy").css({ 'border': '1px solid #d2d6de' });
        }
    }

    if (bEvictionwhen === 'Yes') {
        if (Evictionwhen === "undefined" || Evictionwhen === "") {
            $("#txtwhen").css({ 'border': '1px solid red' });
            isresult = false;
        }
        else {
            $("#txtwhen").css({ 'border': '1px solid #d2d6de' });
        }
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
        BankruptcyDate: $("#txtbankruptcy").val(),
        MoveIn: $("#txtMoveIn").val(),
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
    
   
    if (ValidationForGeneralInfo()) {
        var Obj = getTheEmergencyObj();
        var URL = "/Pages/Resident/ResidentialTenantRental_App_Page_3.aspx/" + "SaveGeneralInfo";
        let Basic = makeAjaxCallReturnPromiss(URL, Obj);
        Basic.then((data) => {
            if (data.d != null || data.d != "") {
                var res = $.parseJSON(decodeURIComponent(data.d));
                if (res == true) {
                    window.location.href = "/Pages/Resident/ResidentialTenantRental_App_Page_4.aspx";
                } else {
                    notify('danger', "Save Failed !!");
                }
                //BindEmergencyContactToField($.parseJSON(decodeURIComponent(data.d)));
            }
        });
    }
     else {        
        notify('danger', "Please fill out the form with valid input.");
    }


});


$(document).on('click', '#btnBack', function (parameters) {

    var origin = window.location.origin; // Returns base URL
    var url = origin + "/Pages/Resident/ResidentialTenantRental_App_Page_2.aspx";
    window.location.href = url;
});

$(document).on('click', '#btnExit', function (parameters) {
    
    var origin = window.location.origin; // Returns base URL
    var url = origin + "/Pages/DashboardAdmin.aspx";
    window.location.href = url;
});