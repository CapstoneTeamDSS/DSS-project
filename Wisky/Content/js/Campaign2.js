$(document).ready(function () {
    $(document).on('click', '[data-role="ad-type"]', function () {
        AdsMode = parseInt($(this).data('value'));
    });
});

/*
 * author: TrungNDT
 * method: render all checkboxes in Advance Setting, include 3 choices:
 *      - Day parting (Mon - Sun)
 *      - Hour parting (0h - 24h)
 *      - Device (PC or Device)
 */
function renderAdvanceCheckboxes() {
    function getCheckbox(name, val, containerId) {
        return $('<div class="col-md-4"><div class="checkbox"><label><input name="' + containerId.slice(1) + 'Arr" type="checkbox" value="'
            + val + '"><i class="input-helper"></i>'
            + name + '</label></div></div>');
    }

    function renderFilter(array, containerId) {
        $(containerId).children('.row').empty();
        $.each(array, function (i, e) {
            $(containerId).children('.row').append(getCheckbox(e, Math.pow(2, i), containerId));
        });
    }

    function renderMax() {
        maxDay = (parseInt($('#DayParting input[type="checkbox"]').last().val())) * 2 - 1;
        maxHour = (parseInt($('#HourParting input[type="checkbox"]').last().val())) * 2 - 1;
        maxDevice = (parseInt($('#DeviceFilter input[type="checkbox"]').last().val())) * 2 - 1;
        $("form").append('<input type="hidden" name="MaxDay" value="' + maxDay + '"/>')
        $("form").append('<input type="hidden" name="MaxHour" value="' + maxHour + '"/>')
        $("form").append('<input type="hidden" name="MaxDevice" value="' + maxDevice + '"/>')
    }

    function getHoursArray() {
        var arr = [];
        for (var i = 0; i < 24; i += 2) {
            arr.push(i + ':00 - ' + (i + 1) + ':59');
        }
        return arr;
    }

    renderFilter(['Sunday', 'Monday', 'Tuesday', 'Wednesday', 'Thursday', 'Friday', 'Saturday'], '#DayParting')
    renderFilter(getHoursArray(), '#HourParting');
    renderFilter(['PC', 'Mobile'], '#DeviceFilter');
    renderMax();
}

/*
 * author: TrungNDT
 * method: use in "Edit" modal
 */
function setCheckedInAdvanceSetting() {
    function setCheckbox(array, containerId) {
        $.each(array, function (i, e) {
            $(containerId).find('input').eq(e).prop('checked', 'checked')
        });
    }
    var dayPartings = JSON.parse($('#hiddenDayParting').val());
    var hourPartings = JSON.parse($('#hiddenHourParting').val());
    var deviceFilter = JSON.parse($('#hiddenDeviceFilter').val());
    setCheckbox(dayPartings, '#DayParting');
    setCheckbox(hourPartings, '#HourParting');
    setCheckbox(deviceFilter, '#DeviceFilter');
}
function setCheckedInAdvanceSettingDC() {
    function setCheckbox(array, containerId) {
        $.each(array, function (i, e) {
            $(containerId).find('input').eq(e).prop('checked', 'checked')
        });
    }
    var dayPartings = JSON.parse($('#hiddenDayParting').val());
    var hourPartings = JSON.parse($('#hiddenHourParting').val());
    setCheckbox(dayPartings, '#DayParting');
    setCheckbox(hourPartings, '#HourParting');
}

/*
 * author: TrungNDT
 * method: collect checked options in given container
 */
function collectPartingSelection(containerId) {
    var value = 0,
        maxVal = (parseInt($(containerId + ' input[type="checkbox"]').last().val())) * 2 - 1;
    $.each($(containerId + ' input[type="checkbox"]:checked'), function (i, e) {
        value += parseInt($(e).val());
    });
    if (value == 0) {
        value = maxVal;
    }
    return value;// == 0 ? value : maxVal;
}

function countDayParting() {

    if (document.getElementById("IsDayParting").checked) {
        if (document.getElementById("checkbox-Monday").checked) {
            dayParting = dayParting | 2;
        }
        //} else {
        //    dayParting += "0";
        //}
        if (document.getElementById("checkbox-Tuesday").checked) {
            dayParting = dayParting | 4;
        }
        //else {
        //    dayParting += "0";
        //}
        if (document.getElementById("checkbox-Wednesday").checked) {
            dayParting = dayParting | 8;
        }
        //else {
        //    dayParting += "0";
        //}
        if (document.getElementById("checkbox-Thursday").checked) {
            dayParting = dayParting | 16;
        }
        //else {
        //    dayParting += "0";
        //}
        if (document.getElementById("checkbox-Friday").checked) {
            dayParting = dayParting | 32;
        }
        //else {
        //    dayParting += "0";
        //}
        if (document.getElementById("checkbox-Saturday").checked) {
            dayParting = dayParting | 64;
        }
        //else {
        //    dayParting += "0";
        //}
        if (document.getElementById("checkbox-Sunday").checked) {
            dayParting = dayParting | 1;
        }
        //else {
        //    dayParting += "0";
        //}
    }
    return dayParting == 0 ? 127 : dayParting;
}

function countHourParting() {
    var hourParting = 0;
    if (document.getElementById("IsHourParting").checked) {
        if (document.getElementById("checkbox-0-2").checked) {
            hourParting |= 1;
        }
        //else {
        //    hourParting += "0";
        //}
        if (document.getElementById("checkbox-2-4").checked) {
            hourParting |= 2;
        }
        //else {
        //    hourParting += "0";
        //}
        if (document.getElementById("checkbox-4-6").checked) {
            hourParting |= 4;
        }
        //else {
        //    hourParting += "0";
        //}
        if (document.getElementById("checkbox-6-8").checked) {
            hourParting |= 8;
        }
        //else {
        //    hourParting += "0";
        //}
        if (document.getElementById("checkbox-8-10").checked) {
            hourParting |= 16;
        }
        //else {
        //    hourParting += "0";
        //}
        if (document.getElementById("checkbox-10-12").checked) {
            hourParting |= 32;
        }
        //else {
        //    hourParting += "0";
        //}
        if (document.getElementById("checkbox-12-14").checked) {
            hourParting |= 64;
        }
        //else {
        //    hourParting += "0";
        //}
        if (document.getElementById("checkbox-14-16").checked) {
            hourParting |= 128;
        }
        //else {
        //    hourParting += "0";
        //}
        if (document.getElementById("checkbox-16-18").checked) {
            hourParting |= 256;
        }
        //else {
        //    hourParting += "0";
        //}
        if (document.getElementById("checkbox-18-20").checked) {
            hourParting |= 512;
        }
        //else {
        //    hourParting += "0";
        //}
        if (document.getElementById("checkbox-20-22").checked) {
            hourParting |= 1024;
        }
        //else {
        //    hourParting += "0";
        //}
        if (document.getElementById("checkbox-22-24").checked) {
            hourParting |= 2048;
        }
        //else {
        //    hourParting += "0";
        //}
    }

    return hourParting == 0 ? 4095 : hourParting;
}

function countDeviceOs() {
    var DeviceOs = 0;
    if (document.getElementById("IsDeviceFilter").checked) {
        if (document.getElementById("checkbox-PC").checked) {
            DeviceOs |= 1;
        }
        if (document.getElementById("checkbox-Mobile").checked) {
            DeviceOs |= 2;
        }
    }
    return DeviceOs == 0 ? 3 : DeviceOs;
}


    function onChangeCheckboxMultiLanguage() {
        $('#MultiLanguage').slideToggle(100);
        $('.GeneralLanguage').slideToggle(500);
        $('.MultiLanguage').slideToggle(500);
    }

    function onChangeCheckboxDayParting() {
        $('#DayParting').slideToggle(100);
    }

    function onChangeCheckboxHourParting() {
        $('#HourParting').slideToggle(100);
    }

    function onChangeCheckboxDeviceOs() {
        $('#DeviceFilter').slideToggle(100);
    }

    function onChangeCheckboxCheckin() {
        $('#check-in-field').slideToggle(100);
    }

    function onChangeCheckBoxJsonEditor() {
        
        $('#JsonEditor').slideToggle(100);
    }

    // AdsMode
    // AppInstall = 0,
    // ClickWebsite = 1,
    // LikeFacebook = 2,
    // Offers = 3,
    // Survey = 4,
    // Videos = 5
    //function onClickAppInstall() {
    //    AdsMode = 0;
    //}
    //function onClickWebClick() {
    //    AdsMode = 1;
    //}
    //function onClickLikeFb() {
    //    AdsMode = 2;
    //}
    //function onClickGetOffer() {
    //    AdsMode = 3;
    //}
    //function onClickDoSurvey() {
    //    AdsMode = 4;
    //}
    //function onClickWatchVideo() {
    //    AdsMode = 5;
    //}

    // Priority
    // 0 = Default
    // 1 = Low (Daily)
    // 2 = Medium (Weekly)
    // 3 = High (Monthy)
    // 4 = High (Monthy)
    // 5 = Special (One time)
    // 6 = Contract (Advertiser)
    function onClickCbbPriority(level) {
        var CbbPriority = document.getElementById("cbb-priority-level");
        switch (level) {
            case 0:
                $('#priority').val(level);
                CbbPriority.innerText = "Default";
                var a = document.createElement("span");
                a.setAttribute("class", "caret");
                CbbPriority.appendChild(a);
                break;
            case 1:
                $('#priority').val(level);
                CbbPriority.innerText = "Low (Daily)";
                var a = document.createElement("span");
                a.setAttribute("class", "caret");
                CbbPriority.appendChild(a);
                break;
            case 2:
                $('#priority').val(level);
                CbbPriority.innerText = "Medium (Weekly)";
                var a = document.createElement("span");
                a.setAttribute("class", "caret");
                CbbPriority.appendChild(a);
                break;
            case 3:
                $('#priority').val(level);
                CbbPriority.innerText = "High (Monthy)";
                var a = document.createElement("span");
                a.setAttribute("class", "caret");
                CbbPriority.appendChild(a);
                break;
            case 4:
                $('#priority').val(level);
                CbbPriority.innerText = "Special (One time)";
                var a = document.createElement("span");
                a.setAttribute("class", "caret");
                CbbPriority.appendChild(a);
                break;
            case 5:
                $('#priority').val(level);
                CbbPriority.innerText = "Area (APG)";
                var a = document.createElement("span");
                a.setAttribute("class", "caret");
                CbbPriority.appendChild(a);
                break;
            case 6:
                $('#priority').val(level);
                CbbPriority.innerText = "Contract (Advertiser)";
                var a = document.createElement("span");
                a.setAttribute("class", "caret");
                CbbPriority.appendChild(a);
                break;

            default:
        }
    }