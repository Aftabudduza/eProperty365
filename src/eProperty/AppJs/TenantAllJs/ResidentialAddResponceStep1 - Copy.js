var cardType = "";
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

$(document).ready(function () {
    $('input[type="checkbox"].flat-red, input[type="radio"].flat-red').iCheck({
        checkboxClass: 'icheckbox_flat-green',
        radioClass: 'iradio_flat-green'
    });
    //$(".tDate").datepicker({
    //    dateFormat: "MM yyyy",
    //    changeYear: true,
    //    changeMonth: true
    //});
    $(".ddl").select2();
    LoadAddDDL();
    LoadApprovalCode();
    var isresult = true;
    var bShow = $("#hdnShow").val();
    if (bShow === "undefined" || bShow === "") {
        $("#btnSubmit").css({ 'display': 'block' });
    }
    else {
        if (bShow === "False") {
            //$("#btnSubmit").css({ 'display': 'none' });
        }
        else {
            $("#btnSubmit").css({ 'display': 'block' });
        }
    }
    $(".btnPaymentGetway").attr("allowed_methods", "echeck");
   
   
    //$("#creditcardapp1Txt").inputmask({ "mask": "(999) 999-9999" });


    var masking = {

        // User defined Values
        //maskedInputs : document.getElementsByClassName('masked'), // add with IE 8's death
        maskedInputs: document.querySelectorAll('.masked'), // kill with IE 8's death
        maskedNumber: 'XdDmMyY9',
        maskedLetter: '_',

        init: function () {
            masking.setUpMasks(masking.maskedInputs);
            masking.maskedInputs = document.querySelectorAll('.masked'); // Repopulating. Needed b/c static node list was created above.
            masking.activateMasking(masking.maskedInputs);
        },

        setUpMasks: function (inputs) {
            var i, l = inputs.length;

            for (i = 0; i < l; i++) {
                masking.createShell(inputs[i]);
            }
        },

        // replaces each masked input with a shall containing the input and it's mask.
        createShell: function (input) {
            var text = '',
                placeholder = input.getAttribute('placeholder');

            input.setAttribute('maxlength', placeholder.length);
            input.setAttribute('data-placeholder', placeholder);
            input.removeAttribute('placeholder');

            text = '<span class="shell">' +
              '<span aria-hidden="true" id="' + input.id +
              'Mask"><i></i>' + placeholder + '</span>' +
              input.outerHTML +
              '</span>';

            //input.outerHTML = text;
        },

        setValueOfMask: function (e) {
            var value = e.target.value,
                placeholder = e.target.getAttribute('data-placeholder');

            return "<i>" + value + "</i>" + placeholder.substr(value.length);
        },

        // add event listeners
        activateMasking: function (inputs) {
            var i, l;

            for (i = 0, l = inputs.length; i < l; i++) {
                if (masking.maskedInputs[i].addEventListener) { // remove "if" after death of IE 8
                    masking.maskedInputs[i].addEventListener('keyup', function (e) {
                        masking.handleValueChange(e);
                    }, false);
                } else if (masking.maskedInputs[i].attachEvent) { // For IE 8
                    masking.maskedInputs[i].attachEvent("onkeyup", function (e) {
                        e.target = e.srcElement;
                        masking.handleValueChange(e);
                    });
                }
            }
        },

        handleValueChange: function (e) {
            var id = e.target.getAttribute('id');

            switch (e.keyCode) { // allows navigating thru input
                case 20: // caplocks
                case 17: // control
                case 18: // option
                case 16: // shift
                case 37: // arrow keys
                case 38:
                case 39:
                case 40:
                case 9: // tab (let blur handle tab)
                    return;
            }

            document.getElementById(id).value = masking.handleCurrentValue(e);
            document.getElementById(id + 'Mask').innerHTML = masking.setValueOfMask(e);

        },

        handleCurrentValue: function (e) {
            var isCharsetPresent = e.target.getAttribute('data-charset'),
                placeholder = isCharsetPresent || e.target.getAttribute('data-placeholder'),
                value = e.target.value, l = placeholder.length, newValue = '',
                i, j, isInt, isLetter, strippedValue;

            // strip special characters
            strippedValue = isCharsetPresent ? value.replace(/\W/g, "") : value.replace(/\D/g, "");

            for (i = 0, j = 0; i < l; i++) {
                var x =
                isInt = !isNaN(parseInt(strippedValue[j]));
                isLetter = strippedValue[j] ? strippedValue[j].match(/[A-Z]/i) : false;
                matchesNumber = masking.maskedNumber.indexOf(placeholder[i]) >= 0;
                matchesLetter = masking.maskedLetter.indexOf(placeholder[i]) >= 0;

                if ((matchesNumber && isInt) || (isCharsetPresent && matchesLetter && isLetter)) {

                    newValue += strippedValue[j++];

                } else if ((!isCharsetPresent && !isInt && matchesNumber) || (isCharsetPresent && ((matchesLetter && !isLetter) || (matchesNumber && !isInt)))) {
                    // masking.errorOnKeyEntry(); // write your own error handling function
                    return newValue;

                } else {
                    newValue += placeholder[i];
                }
                // break if no characters left and the pattern is non-special character
                if (strippedValue[j] == undefined) {
                    break;
                }
            }
            if (e.target.getAttribute('data-valid-example')) {
                return masking.validateProgress(e, newValue);
            }
            return newValue;
        },

        validateProgress: function (e, value) {
            var validExample = e.target.getAttribute('data-valid-example'),
                pattern = new RegExp(e.target.getAttribute('pattern')),
                placeholder = e.target.getAttribute('data-placeholder'),
                l = value.length, testValue = '';

            //convert to months
            if (l == 1 && placeholder.toUpperCase().substr(0, 2) == 'MM') {
                if (value > 1 && value < 10) {
                    value = '0' + value;
                }
                return value;
            }
            // test the value, removing the last character, until what you have is a submatch
            for (i = l; i >= 0; i--) {
                testValue = value + validExample.substr(value.length);
                if (pattern.test(testValue)) {
                    return value;
                } else {
                    value = value.substr(0, value.length - 1);
                }
            }

            return value;
        },

        errorOnKeyEntry: function () {
            // Write your own error handling
        }
    }

    masking.init();

});

function LoadAddDDL(parameters) {

    var content = '<option value="-1">Select.......</option>';
    for (var i = 1; i <= 5; i++) {
        content += '<option value="' + i + '">' + i + '</option>';
    }
    $("#numberofPeople option").empty();
    $("#numberofPeople").append(content);
    //.................. load State ...............//
    var URL = "/Pages/Admin/AddResidentialUnit.aspx/" + "GetState";
    var obj = {};
    let State = makeAjaxCallReturnPromiss(URL, obj);

    State.then((data) => {
        var content = setCombo($.parseJSON(decodeURIComponent(data.d)), '-1');
        $("#ddlstateapp option").empty();
        $("#ddlstateapp").append(content);
        $("#ddlstateapp").val("-1").trigger('change');
    }).catch((err) => {
        console.log(err);
    });

    //---------- Saved Data --------//
    var URL = window.location.pathname + "/Load";
    var obj = {
        "Id": 0
    };
    let step1objAll = makeAjaxCallReturnPromiss(URL, obj);
    //---------- Application Free-----------//
    var URL = window.location.pathname + "/LoadApplicationFree";
    var obj = {
        "Id": 0
    };
    let applicationFree = makeAjaxCallReturnPromiss(URL, obj);

    Promise.all([State]).then(function () {
        //LoadSavedData();
        //$.parseJSON(decodeURIComponent(result.d));

        step1objAll.then((data) => {
            
            var step1objAll = $.parseJSON(decodeURIComponent(data.d));


            if (step1objAll == null || step1objAll == "") {
                applicationFree.then((data) => {
                    var app = $.parseJSON(decodeURIComponent(data.d));
                    if (app != null) {
                        $("#TotalApp").attr('data_AppFee', app.TotalFee);
                        $("#TotalApp").val(app.TotalFee);
                         $("#hdTotalAmount").val(app.TotalFee);
                        
                    }

                });

            } else {

                var step1obj = step1objAll[0].getobj;
                var appfee = step1objAll[0].TotalFee;

                if (step1obj == null || step1obj == "") {
                    $("#TotalApp").val(appfee);
                    $("#TotalApp").attr('data_AppFee', appfee);
                    $("#hdTotalAmount").val(appfee);
                }
                else {
                    submitt = true;
                    $('input[name=card]').closest('div').removeClass('checked');
                    $('input[name=card]').attr('checked', false);
                    $("input[name=card][value='" + step1obj.AccountType + "']").closest('div').addClass('checked');
                    $("input[name=card][value='" + step1obj.AccountType + "']").attr('checked', true);

                    $("#hdId").val(step1obj.Id);
                    $("#numberofPeople").val(step1obj.NumberOfPeopleSigning).trigger('change');

                    $('input[name=r3]').closest('div').removeClass('checked');
                    $('input[name=r3]').attr('checked', false);
                    $("input[name=r3][value='" + step1obj.PerFormTenantBackgroundScreening + "']").closest('div').addClass('checked');
                    $("input[name=r3][value='" + step1obj.PerFormTenantBackgroundScreening + "']").attr('checked', true);

                    $("#hdTotalAmount").val(appfee);
                    $("#TotalApp").val(appfee);
                    $("#TotalApp").attr('data_AppFee', appfee);
                    //appfee
                    if (step1obj.AccountType == 'Credit') {
                        $("#nameAccountapp1Txt").val(step1obj.NameOfAccount);
                        $("#addressapp1Txt1").val(step1obj.Address);
                        $("#cityapp1Txt").val(step1obj.City);
                        $("#ddlstateapp").val(step1obj.State).trigger('change');
                        // $("#ddlstateapp").val(step1obj.State).trigger('change');
                        $("#zipcodeapp1Txt").val(step1obj.ZipCode);
                        $("#creditcardapp1Txt").val(step1obj.CreditCardNumber);
                        $("#cvsNumber").val(step1obj.CVSNumber);
                        $("#txtExpire").val(step1obj.Exp);
                        $("#tblCheck").css({ 'display': 'none' });
                        $("#tblCredit").css({ 'display': 'block' });

                    } else if (step1obj.AccountType == 'Check') {
                        $("#routingnumapp1Txt").val(step1obj.RoutingNumber);
                        $("#checkacctnumapp1Txt").val(step1obj.CheckingAccountNumber);
                        $("#rerountingnumapp1Txt").val(step1obj.RoutingNumber);
                        $("#recheckacctnumapp1Txt").val(step1obj.CheckingAccountNumber);
                        $("#cnameAccountapp1Txt").val(step1obj.NameOfAccount);
                        $("#tblCheck").css({ 'display': 'block' });
                        $("#tblCredit").css({ 'display': 'none' });
                    } else {
                        $("#tblCheck").css({ 'display': 'none' });
                        $("#tblCredit").css({ 'display': 'none' });
                    }
                }

            }
        }).catch((err) => {
            console.log(err);
        });

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
$(document).on('ifChanged', "input[type=radio][name=card]", function (parameters) {

    if ($(this).attr('id') == 'Checking') {
        $("#tblCheck").css({ 'display': 'block' });
        $("#tblCredit").css({ 'display': 'none' });
        $("#tblCredit tbody").css({ 'width': '100% !important' });
    }
    else if ($(this).attr('id') == 'Cash') {
        $("#tblCheck").css({ 'display': 'none' });
        $("#tblCredit").css({ 'display': 'none' });
    }
    else {
        $("#tblCredit").css({ 'display': 'block' });
        $("#tblCredit tbody").css({ 'width': '100% !important' });
        $("#tblCheck").css({ 'display': 'none' });
    }

});

function LoadSavedData(parameters) {
    var pagePath = window.location.pathname + "/Load";
    var obj = {
        "Id": 0
    };
    $.ajax({
        type: "POST",
        url: pagePath,
        data: JSON.stringify({ "obj": obj }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        error:
            function (XMLHttpRequest, textStatus, errorThrown) {
                // alert("Error");
                notify('danger', "Error");
            },
        success:
            function (result) {

                var step1obj = $.parseJSON(decodeURIComponent(result.d));
                if (step1obj == null || step1obj == "") {
                    loadAplicationFree();
                } else {
                    submitt = true;
                    $('input[name=card]').closest('div').removeClass('checked');
                    $('input[name=card]').attr('checked', false);
                    $("input[name=card][value='" + step1obj.AccountType + "']").closest('div').addClass('checked');
                    $("input[name=card][value='" + step1obj.AccountType + "']").attr('checked', true);

                    $("#hdId").val(step1obj.Id);
                    $("#numberofPeople").val(step1obj.NumberOfPeopleSigning).trigger('change');

                    $('input[name=r3]').closest('div').removeClass('checked');
                    $('input[name=r3]').attr('checked', false);
                    $("input[name=r3][value='" + step1obj.PerFormTenantBackgroundScreening + "']").closest('div').addClass('checked');
                    $("input[name=r3][value='" + step1obj.PerFormTenantBackgroundScreening + "']").attr('checked', true);


                    $("#TotalApp").val(step1obj.TotalApplicationFree);
                    if (step1obj.AccountType == 'Credit') {
                        $("#nameAccountapp1Txt").val(step1obj.NameOfAccount);
                        $("#addressapp1Txt1").val(step1obj.Address);
                        $("#cityapp1Txt").val(step1obj.City);
                        $("#ddlstateapp").val(step1obj.State).trigger('change');
                        // $("#ddlstateapp").val(step1obj.State).trigger('change');
                        $("#zipcodeapp1Txt").val(step1obj.ZipCode);
                        $("#creditcardapp1Txt").val(step1obj.CreditCardNumber);
                        $("#cvsNumber").val(step1obj.CVSNumber);
                        $("#txtExpire").val(step1obj.Exp);

                    } else if (step1obj.AccountType == 'Check') {
                        $("#routingnumapp1Txt").val(step1obj.RoutingNumber);
                        $("#checkacctnumapp1Txt").val(step1obj.CheckingAccountNumber);
                    } else {

                    }
                }

                //submitt = false

                //var content = setCombo(lstofState, '-1');
                //$("#ddlstateapp option").empty();
                //$("#ddlstateapp").append(content);
                //$("#ddlstateapp").val("-1").trigger('change');

            }
    });
}

function loadAplicationFree(parameters) {
    
    var pagePath = window.location.pathname + "/LoadApplicationFree";
    var obj = {
        "Id": 0
    };
    $.ajax({
        type: "POST",
        url: pagePath,
        data: JSON.stringify({ "obj": obj }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        error:
            function (XMLHttpRequest, textStatus, errorThrown) {
                // alert("Error");
                notify('danger', "Error");
            },
        success:
            function (result) {
                var applicationFree = $.parseJSON(decodeURIComponent(result.d));
                if (applicationFree != null) {

                    $("#TotalApp").attr('data_AppFee', applicationFree);
                    $("#TotalApp").val(applicationFree);
                    // $("#TotalApp").val(applicationFree.UnitApplicationFee);
                }


            }
    });
}
//All Function
function LoadNumberOfPeople(parameters) {

    var content = '<option value="-1">Select.......</option>';
    for (var i = 1; i <= 5; i++) {
        content += '<option value="' + i + '">' + i + '</option>';
    }
    $("#numberofPeople option").empty();
    $("#numberofPeople").append(content);
    LoadState();
}
function LoadState() {
    //
    var pagePath = DataUrl + "/GetState";
    var obj = {};
    $.ajax({
        type: "POST",
        url: pagePath,
        data: JSON.stringify(obj),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        error:
            function (XMLHttpRequest, textStatus, errorThrown) {
                // alert("Error");
                notify('danger', "Error");
            },
        success:
            function (result) {
                var lstofState = $.parseJSON(decodeURIComponent(result.d));
                var content = setCombo(lstofState, '-1');
                $("#ddlstateapp option").empty();
                $("#ddlstateapp").append(content);
                $("#ddlstateapp").val("-1").trigger('change');
                LoadSavedData();
            }

    });
}
function LoadCity() {
    var pagePath = DataUrl + "/GetCity";
    var obj = {};
    $.ajax({
        type: "POST",
        url: pagePath,
        data: JSON.stringify(obj),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        error:
            function (XMLHttpRequest, textStatus, errorThrown) {
                // alert("Error");
                notify('danger', "Error");
            },
        success:
            function (result) {
                var lstofCity = $.parseJSON(decodeURIComponent(result.d));
                var content = setCombo(lstofCity, '-1');
                $("#ddlcityapp option").empty();
                $("#ddlcityapp").append(content);
                $("#ddlcityapp").val("-1").trigger('change');
            }
    });
}

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

// Combo SetUp
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

function setComboWithIntValue(data, selectedvalue) {
    var content = '<option value="-1">Select.......</option>';
    if (data == undefined || data.length == 0 || data == null) {
        return content;
    }
    else {
        if (selectedvalue == undefined || selectedvalue == null) {
            $.each(data, function (i, obj) {
                content += '<option data_Id="' + obj.Id2 + '" value="' + obj.Id + '">' + obj.Data + '</option>';
            });
        }

        else {
            $.each(data, function (i, obj) {
                if (obj.Id == selectedvalue) {
                    content += '<option data_Id="' + obj.Id2 + '" value="' + obj.Id + '" selected>' + obj.Data + '</option>';
                } else {
                    content += '<option data_Id="' + obj.Id2 + '" value="' + obj.Id + '">' + obj.Data + '</option>';
                }
            });
        }

    }

    return content;
}

function ValidateCredit(parameters) {
    var isresult = true;
    var accName = $("#nameAccountapp1Txt").val().trim();
    var address = $("#addressapp1Txt1").val().trim();
    var city = $("#cityapp1Txt").val().trim();
    var state = $("#ddlstateapp").val().trim();
    var zip = $("#zipcodeapp1Txt").val().trim();
    var card = $("#creditcardapp1Txt").val().trim();
    var cvs = $("#cvsNumber").val().trim();
    var exp = $("#txtExpire").val().trim();

    var nTotal = $("#TotalApp").val().trim();

    if (nTotal === "undefined" || nTotal === "") {
        $("#TotalApp").css({ 'border': '1px solid red' });
        isresult = false;
    }
    else {
        $("#TotalApp").css({ 'border': '1px solid #d2d6de' });
    }

    if (accName === "undefined" || accName === "") {
        $("#nameAccountapp1Txt").css({ 'border': '1px solid red' });
        isresult = false;
    }
    else {
        $("#nameAccountapp1Txt").css({ 'border': '1px solid #d2d6de' });
    }

    if (address === "undefined" || address === "") {
        $("#addressapp1Txt1").css({ 'border': '1px solid red' });
        isresult = false;
    }
    else {
        $("#addressapp1Txt1").css({ 'border': '1px solid #d2d6de' });
    }

    if (city === "undefined" || city === "") {
        $("#cityapp1Txt").css({ 'border': '1px solid red' });
        isresult = false;
    }
    else {
        $("#cityapp1Txt").css({ 'border': '1px solid #d2d6de' });
    }

    if (state === "undefined" || state === "-1") {
        $("#s2id_ddlstateapp").css({ 'border': '1px solid red' });
        isresult = false;
    }
    else {
        $("#s2id_ddlstateapp").css({ 'border': '1px solid #d2d6de' });
    }


    if (zip === "undefined" || zip === "") {
        $("#zipcodeapp1Txt").css({ 'border': '1px solid red' });
        isresult = false;
    }
    else {
        $("#zipcodeapp1Txt").css({ 'border': '1px solid #d2d6de' });
    }
    if (card === "undefined" || card === "") {
        $("#creditcardapp1Txt").css({ 'border': '1px solid red' });
        isresult = false;
    }
    else {
        $("#creditcardapp1Txt").css({ 'border': '1px solid #d2d6de' });
    }
    if (cvs === "undefined" || cvs === "") {
        $("#cvsNumber").css({ 'border': '1px solid red' });
        isresult = false;
    }
    else {
        $("#cvsNumber").css({ 'border': '1px solid #d2d6de' });
    }
    if (exp === "undefined" || exp === "") {
        $("#txtExpire").css({ 'border': '1px solid red' });
        isresult = false;
    }
    else {
        $("#txtExpire").css({ 'border': '1px solid #d2d6de' });
    }
    // });
    if (isresult)
        isresult = true;
    else {
        isresult = false;
    }
    return isresult;
}

function ValidateCheck(parameters) {
    var isresult = true;
    var RoutingNumber = $("#routingnumapp1Txt").val().trim();
    var RoutingNumberRe = $("#rerountingnumapp1Txt").val().trim();
    var AccNo = $("#checkacctnumapp1Txt").val().trim();
    var AccNoRech = $("#recheckacctnumapp1Txt").val().trim();
    var accName = $("#cnameAccountapp1Txt").val().trim();
    var nTotal = $("#TotalApp").val().trim();

    if (nTotal === "undefined" || nTotal === "") {
        $("#TotalApp").css({ 'border': '1px solid red' });
        isresult = false;
    }
    else {
        $("#TotalApp").css({ 'border': '1px solid #d2d6de' });
    }

    if (accName === "undefined" || accName === "") {
        $("#cnameAccountapp1Txt").css({ 'border': '1px solid red' });
        isresult = false;
    }
    else {
        $("#cnameAccountapp1Txt").css({ 'border': '1px solid #d2d6de' });
    }

    if (RoutingNumber === "undefined" || RoutingNumber === "") {
        $("#routingnumapp1Txt").css({ 'border': '1px solid red' });
        isresult = false;
    }
    else {
        $("#routingnumapp1Txt").css({ 'border': '1px solid #d2d6de' });
    }

    if (RoutingNumber != RoutingNumberRe) {
        $("#rerountingnumapp1Txt").css({ 'border': '1px solid red' });
        isresult = false;
    }
    else {
        $("#rerountingnumapp1Txt").css({ 'border': '1px solid #d2d6de' });
    }

    if (AccNo === "undefined" || AccNo === "") {
        $("#checkacctnumapp1Txt").css({ 'border': '1px solid red' });
        isresult = false;
    }
    else {
        $("#checkacctnumapp1Txt").css({ 'border': '1px solid #d2d6de' });
    }

    if (AccNo != AccNoRech) {
        $("#recheckacctnumapp1Txt").css({ 'border': '1px solid red' });
        isresult = false;
    }
    else {
        $("#recheckacctnumapp1Txt").css({ 'border': '1px solid #d2d6de' });
    }




    // });
    if (isresult)
        isresult = true;
    else {
        isresult = false;
    }
    return isresult;
}

function GetSavedObj(creditType) {
    var obj = {};
    if (creditType == 'Credit') {
        obj = {
            "Id": $("#hdId").val(),
            "NumberOfPeopleSigning": $("#numberofPeople").val(),
            "PerFormTenantBackgroundScreening": $('input[name=r3]:checked').val(),
            "TotalApplicationFree": $("#TotalApp").val().trim(),
            "AccountType": $('input[name=card]:checked').val(),
            "NameOfAccount": $("#nameAccountapp1Txt").val(),
            "Address": $("#addressapp1Txt1").val(),
            "City": $("#cityapp1Txt").val(),
            "State": $("#ddlstateapp").val(),
            "ZipCode": $("#zipcodeapp1Txt").val(),
            "CreditCardNumber": sLast4,
            "CVSNumber": $("#cvsNumber").val(),
            "Exp": sExpMonth+sExpYear,
            "TransactionCode": sResponseCode,
            "AuthorizationCode": sResponseCode,
            "TransactionDescription": sResponseDetails,
            "RoutingNumber": '',
            "CheckingAccountNumber": ''
        }
    }
    else if (creditType == 'Check') {
        obj = {
            "Id": $("#hdId").val(),
            "NumberOfPeopleSigning": $("#numberofPeople").val(),
            "PerFormTenantBackgroundScreening": $('input[name=r3]:checked').val(),
            "TotalApplicationFree": $("#TotalApp").val().trim(),
            "AccountType": $('input[name=card]:checked').val(),
            "NameOfAccount": $("#cnameAccountapp1Txt").val().trim(),
            "RoutingNumber": $("#routingnumapp1Txt").val(),
            "CheckingAccountNumber": sLast4,
            "TransactionCode": sResponseCode,
            "AuthorizationCode": sResponseCode,
            "TransactionDescription": sResponseDetails,
            "Address": '',
            "City": '',
            "State": '',
            "ZipCode": '',
            "CreditCardNumber": '',
            "CVSNumber": '',
            "Exp": ''
        }
    } else {

        obj = {
            "Id": $("#hdId").val(),
            "NumberOfPeopleSigning": $("#numberofPeople").val(),
            "PerFormTenantBackgroundScreening": $('input[name=r3]:checked').val(),
            "TotalApplicationFree": $("#TotalApp").val().trim(),
            "AccountType": $('input[name=card]:checked').val(),
            "NameOfAccount": '',
            "RoutingNumber": '',
            "CheckingAccountNumber": '',
            "TransactionCode": sResponseCode,
            "AuthorizationCode": sResponseCode,
            "TransactionDescription": sResponseDetails,
            "Address": '',
            "City": '',
            "State": '',
            "ZipCode": '',
            "CreditCardNumber": '',
            "CVSNumber": '',
            "Exp": ''
        }
    }
   
    return obj;
}


function SaveTransaction(creditType) {

    var obj = GetSavedObj(creditType);
    var pagePath = window.location.pathname + "/Save";
    $.ajax({
        type: "POST",
        url: pagePath,
        data: JSON.stringify({ "obj": obj }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        error:
            function (XMLHttpRequest, textStatus, errorThrown) {
                alert("Error");
            },
        success:
            function (result) {

                var obj = $.parseJSON(decodeURIComponent(result.d));
                if (obj == true) {
                    submitt = true;
                    notify('success', "Saved successfully");
                    // window.location.href = "/Pages/Resident/ResidentialTenantRental_App_Page_1.aspx";
                } else {
                    submitt = false;
                    notify('danger', "Save Failed !!");
                }

            }

    });

}

function oncallback(e) {
   
    //$('#message').html(e.data);
    
    var creditType = $('input[name=card]:checked').val();
    var validate = true;
    //if (creditType == 'Credit') {
    //    validate = ValidateCredit();
    //}
    //else if (creditType == 'Check') {
    //    validate = ValidateCheck();
    //}
    //else if (creditType == 'Cash') {
    //    validate = true;
    //}
    //else {
    //    if (creditType === "undefined" || creditType === "") {
    //        validate = false;
    //    }
    //}

    if (validate == true && (creditType == 'Credit' || creditType == 'Check')) {
       
        var response = JSON.parse(e.data);
        switch (response.event) {
            
            case 'begin':

                //call to forte checkout is successful
                //$('#success').css('display', 'none');
                break;

            case 'success':

                //transaction successful

                //(optional) validate from response.signature that this message is coming from forte

                //display a receipt

                //$('#success').css('display', '');
              //  alert('thanks for your order. the trace number is ' + response.trace_number);

                isCheckoutSuccessful = true;
                sResponseCode = response.authorization_code;
                sResponseDetails = response.response_description;
                sApplicationFee = response.total_amount;
                sLast4 = response.last_4
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
       // $('input[name=billing_name]').val("test");
    }
    
    else {
        if (validate == true && creditType == 'Cash') {
            SaveTransaction(creditType);
        }
    }

}


//$(document).on('click', "#btnSubmit", function (parameters) {

//    var creditType = $('input[name=card]:checked').val();
//    var validate = true;
//    if (creditType == 'Credit') {
//        validate = ValidateCredit();
//    }
//    else if (creditType == 'Check') {
//        validate = ValidateCheck();
//    }
//    else if (creditType == 'Cash') {
//        validate = true;
//    }
//    else {
//        if (creditType === "undefined" || creditType === "") {
//            validate = false;
//        }
//    }

//    if (validate == true) {

//        var obj = GetSavedObj(creditType);
//        var pagePath = window.location.pathname + "/Save";
//        $.ajax({
//            type: "POST",
//            url: pagePath,
//            data: JSON.stringify({ "obj": obj }),
//            contentType: "application/json; charset=utf-8",
//            dataType: "json",
//            error:
//                function (XMLHttpRequest, textStatus, errorThrown) {
//                    alert("Error");
//                },
//            success:
//                function (result) {

//                    var obj = $.parseJSON(decodeURIComponent(result.d));
//                    if (obj == true) {
//                        submitt = true;
//                        notify('success', "Saved successfully");
//                        // window.location.href = "/Pages/Resident/ResidentialTenantRental_App_Page_1.aspx";
//                    } else {
//                        submitt = false;
//                        notify('danger', "Save Failed !!");
//                    }

//                }

//        });

//    }
//});


$(document).on('click', "#btnNext", function (parameters) {
    if (submitt == true) {
        obj = {
            "Id": $("#hdId").val()
        }
        var pagePath = window.location.pathname + "/Continue";
        $.ajax({
            type: "POST",
            url: pagePath,
            data: JSON.stringify({ "obj": obj }),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            error:
                function (XMLHttpRequest, textStatus, errorThrown) {
                    alert("Error");
                },
            success:
                function (result) {

                    var obj = $.parseJSON(decodeURIComponent(result.d));
                    if (obj == true) {
                        submitt = false;
                        window.location.href = "/Pages/Resident/ResidentialTenantRental_App_Page_1.aspx";
                    } else {
                        submitt = false;
                        notify('danger', "Saved Failed !!");
                    }

                }

        });
    } else {
        notify('danger', "Please Save card or checking information");
        submitt = false;
    }
});

$(document).on('change', '#numberofPeople', function (parameters) {
    
    if ($(this).val() != '-1') {
        var curVal = parseInt($(this).val());
        var OldapplicationFee = parseInt($("#TotalApp").attr("data_AppFee"));
        var newApplicationFee = curVal * OldapplicationFee;
        $("#TotalApp").val(newApplicationFee);
    }

});

$(document).on('click', '#btnExit', function (parameters) {
    var origin = window.location.origin; // Returns base URL
    var url = origin + "/Pages/DashboardAdmin.aspx";
    window.location.href = url;
});


$(document).on("keyup", "#cnameAccountapp1Txt", function (parameters) {
    
    var compName = $(this).val();
    $(".btnPaymentGetway").attr("billing_company_name", compName);
});

$(document).on("keyup", "#creditcardapp1Txt", function (parameters) {
    
    var compName = $(this).val();
    if (compName.length >= 15) {
        $(".btnPaymentGetway").attr("card_number", compName);
    }
    
});

$(document).on("keyup", "#txtExpire", function (parameters) {
    
    var compName = $(this).val();
    $(".btnPaymentGetway").attr("expire", compName);
});
$(document).on("keyup", "#cvsNumber", function (parameters) {
    
    var compName = $(this).val();
    $(".btnPaymentGetway").attr("cvv", compName);
});
$(document).on("keyup", ".nameAccountapp1Txt", function (parameters) {
    
    var accName = "";
    if (cardType == "Card") {
        accName = $("#nameAccountapp1Txt").val();
    } else if (cardType=="Check") {
        accName = $("#cnameAccountapp1Txt").val();
    }
    else
    {
        
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
    
    var compName = $("#ddlstateapp option").text();
    $(".btnPaymentGetway").attr("billing_region", compName);
});
$(document).on("keyup", "#zipcodeapp1Txt", function (parameters) {
    
    var compName = $(this).val();
    $(".btnPaymentGetway").attr("billing_postal_code", compName);
});

$(document).on("keyup", "#rerountingnumapp1Txt", function (parameters) {
    
    var compName = $(this).val();
    $(".btnPaymentGetway").attr("routing_number", compName);
});
$(document).on("keyup", "#checkacctnumapp1Txt", function (parameters) {
    
    var compName = $(this).val();
    $(".btnPaymentGetway").attr("account_number", compName);
});
$(document).on("keyup", "#recheckacctnumapp1Txt", function (parameters) {
    
    var compName = $(this).val();
    $(".btnPaymentGetway").attr("account_number2", compName);
});


//$(document).on("change", "input[type=radio][name=card]", function () {
$(document).on('ifChanged', "input[type=radio][name=card]", function (parameters) {
    
    var res = "";
     if ($(this).attr('id') == 'Checking') {
         res = "echeck";
         cardType = "Check";
         clearAllPaymentField();
     }
     else if ($(this).attr('id') == 'Cash') {
         cardType = "Cash";
         clearAllPaymentField();
        //$("#tblCheck").css({ 'display': 'none' });
        //$("#tblCredit").css({ 'display': 'none' });
    }
    else {
         res = "visa, mast, disc, amex, dine, jcb";
         cardType = "Card";
         clearAllPaymentField();
    }



    //var val = $('input[name=card]:checked').val();
   
    //if (val === "Credit") {
    //    res = "visa";
    //} else if (val === "Check") {
    //    res = "echeck";
    //} else {
        
    //}
    $(".btnPaymentGetway").attr("allowed_methods", res);
});

function  clearAllPaymentField() {
    $(".btnPaymentGetway").attr("billing_company_name", "");
    $(".btnPaymentGetway").attr("card_number", "");
    $(".btnPaymentGetway").attr("expire", "");
    $(".btnPaymentGetway").attr("cvv", "");
    $(".btnPaymentGetway").attr("billing_name", "");
    $(".btnPaymentGetway").attr("billing_street_line1", "");
    $(".btnPaymentGetway").attr("billing_locality", "");
    $(".btnPaymentGetway").attr("billing_region", "");
    $(".btnPaymentGetway").attr("billing_postal_code", "");
    $(".btnPaymentGetway").attr("routing_number", "");
    $(".btnPaymentGetway").attr("account_number", "");
    $(".btnPaymentGetway").attr("account_number2", "");
    $(".btnPaymentGetway").attr("allowed_methods", "visa, mast, disc, amex, dine, jcb");
}