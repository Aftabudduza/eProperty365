
function makeAjaxCallReturnPromiss(URL,Obj) {
    return $.ajax(
    {
        type: "POST",
        url: URL,
        data: JSON.stringify({ "Obj": Obj }),
        contentType: "application/json; charset=utf-8",
        dataType: "json"
    });
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

function showconfirm() {
    var r = confirm("Are You Sure To Delete?");
    if (r) {
        return true;
    }
    else {
        return false;
    }
}

//async function GetResult(Object,Url) {
//    
//    //let responce = await GetMeTheResult(Object,Url);
//    let responce = await promisefyAjaxCall(Object,Url);
//    let result = await responce.json();
//    return result;
//}

//function GetMeTheResult(Object,Url) {
//    $.ajax({
//        type: "POST",
//       // beforeSend: function () { $.blockUI() },
//       // complete: function () { $.unblockUI() },
//        url: Url,
//        dataType: "JSON",
//        contentType: "application/json;charset=utf-8",
//        data: {},
//        success: (data) => {
//            console.log('Data Load succeeded');
//            
//            // resolve(data);
//            return data.d;
//        },
//        error: (errData) => {
//            console.log('Data Load Falied');
//           // reject(errData);
//            return errData;
//        }
//    });
//}

//var promisefyAjaxCall = function (dataParam, dataURL) {
//    // Return the ES6 promise
//    return new Promise((resolve, reject) => {
//        if (true) { // Try changing to 'false'
//          //  setTimeout(function () {
//                //console.log('waitForMe\'s function succeeded');
//                //resolve();
//                // $.unblockUI();
//                $.ajax({
//                    type: "POST",
//                   // beforeSend: function () { $.blockUI() },
//                  //  complete: function () { $.unblockUI() },
//                    url: dataURL,
//                    dataType: "JSON",
//                    async: false,
//                    contentType: "application/json;charset=utf-8",
//                    data: JSON.stringify(dataParam),
//                    success: (data) => {
//                        console.log('Data Load succeeded');
//                        resolve(data);
//                    },
//                    error: (errData) => {
//                        console.log('Data Load Falied');
//                        reject(errData);
//                    }
//                });
//           // }, 1000);
//        }
      
//    });
//}