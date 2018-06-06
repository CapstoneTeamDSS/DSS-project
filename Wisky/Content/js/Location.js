locationJS = function () {
    //Set default value
    $('#MonthlyMainter').val('false');
    $('#MonthlyMainterCheck').click(function () {
        if ($(this).prop('checked')) {
            $('#MonthlyMainter').val('true');
        }
        else {
            $('#MonthlyMainter').val('false');
        }
    });
    $('#TimeMainter').val('false');
    $('#TimeMainterCheck').click(function () {
        if ($(this).prop('checked')) {
            $('#TimeMainter').val('true');
        }
        else {
            $('#TimeMainter').val('false');
        }
    });
    $('#WMConfiguration').val('false');
    $('#WMConfigurationCheck').click(function () {
        if ($(this).prop('checked')) {
            $('#WMConfiguration').val('true');
        }
        else {
            $('#WMConfiguration').val('false');
        }
    });
    $('#VPNConfiguration').val('false');
    $('#VPNConfigurationCheck').click(function () {
        if ($(this).prop('checked')) {
            $('#VPNConfiguration').val('true');
        }
        else {
            $('#VPNConfiguration').val('false');
        }
    });
    $('#BlockConfiguration').val('false');
    $('#BlockConfigurationCheck').click(function () {
        if ($(this).prop('checked')) {
            $('#BlockConfiguration').val('true');
        }
        else {
            $('#BlockConfiguration').val('false');
        }
    });
    $('#ChannelStandardize').val('false');
    $('#ChannelStandardizeCheck').click(function () {
        if ($(this).prop('checked')) {
            $('#ChannelStandardize').val('true');
        }
        else {
            $('#ChannelStandardize').val('false');
        }
    });

    //Relating System
    $('#poscheck').click(function () {
        if ($(this).prop('checked')) {
            $('#posip').prop('readonly', false);
        }
        else {
            $('#posip').prop('readonly', true);
            $('#posip').val('');
        }
    });
    $('#camcheck').click(function () {
        if ($(this).prop('checked')) {
            $('#camip').prop('readonly', false);
        }
        else {
            $('#camip').prop('readonly', true);
            $('#camip').val('');
        }
    });
    $('#scorecheck').click(function () {
        if ($(this).prop('checked')) {
            $('#scoreip').prop('readonly', false);
        }
        else {
            $('#scoreip').prop('readonly', true);
            $('#scoreip').val('');
        }
    });
    $('#printcheck').click(function () {
        if ($(this).prop('checked')) {
            $('#printip').prop('readonly', false);
        }
        else {
            $('#printip').prop('readonly', true);
            $('#printip').val('');
        }
    });

    //Router Configuration
    $('#pppoeCheck').click(function () {
        if ($(this).prop('checked')) {
            $('#pppoeUser').prop('readonly', false);
            $('#pppoePass').prop('readonly', false);
        }
        else {
            $('#pppoeUser').prop('readonly', true);
            $('#pppoePass').prop('readonly', true);
            $('#pppoeUser').val('');
            $('#pppoePass').val('');
        }
    });
    $('#wanCheck').click(function () {
        if ($(this).prop('checked')) {
            $('#wanSpeed').prop('readonly', false);
        }
        else {
            $('#wanSpeed').prop('readonly', true);
            $('#wanSpeed').val('');
        }
    });
    $('#bvdConfigCheck').click(function () {
        if ($(this).prop('checked')) {
            $('#bridgeConfig').prop('readonly', false);
            $('#ipbridgeConfig').prop('readonly', false);
            $('#vlanidConfig').prop('readonly', false);
            $('#ipvlanidConfig').prop('readonly', false);
        }
        else {
            $('#bridgeConfig').prop('readonly', true);
            $('#ipbridgeConfig').prop('readonly', true);
            $('#vlanidConfig').prop('readonly', true);
            $('#ipvlanidConfig').prop('readonly', true);
            $('#bridgeConfig').val('');
            $('#ipbridgeConfig').val('');
            $('#vlanidConfig').val('');
            $('#ipvlanidConfig').val('');
        }
    });
    $('#portCamConfig').click(function () {
        if ($(this).prop('checked')) {
            $('#camIpConfig').prop('readonly', false);
            $('#camPort').prop('readonly', false);
            $('#scoreIpConfig').prop('readonly', false);
            $('#scorePort').prop('readonly', false);
        }
        else {
            $('#camIpConfig').prop('readonly', true);
            $('#camPort').prop('readonly', true);
            $('#scoreIpConfig').prop('readonly', true);
            $('#scorePort').prop('readonly', true);
            $('#camIpConfig').val('');
            $('#camPort').val('');
            $('#scoreIpConfig').val('');
            $('#scorePort').val('');
        }
    });

    //Access Point Configuration
    $('#roamConfig').click(function () {
        if ($(this).prop('checked')) {
            $('#roamName1').prop('readonly', false);
            $('#roamName2').prop('readonly', false);
        }
        else {
            $('#roamName1').prop('readonly', true);
            $('#roamName2').prop('readonly', true);
            $('#roamName1').val('');
            $('#roamName2').val('');
        }
    });
    $('#backupConfig').click(function () {
        if ($(this).prop('checked')) {
            $('#backUpName1').prop('readonly', false);
            $('#backUpName2').prop('readonly', false);
            $('#backUpPass1').prop('readonly', false);
            $('#backUpPass2').prop('readonly', false);
        }
        else {
            $('#backUpName1').prop('readonly', true);
            $('#backUpName2').prop('readonly', true);
            $('#backUpPass1').prop('readonly', true);
            $('#backUpPass2').prop('readonly', true);
            $('#backUpName1').val('');
            $('#backUpName2').val('');
            $('#backUpPass1').val('');
            $('#backUpPass2').val('');
        }
    });
}

editLocationDefault = function () {
    //On load Relating System
    if (!$('#poscheck').is(':checked')) {
        $('#posip').prop('readonly', true);
    };
    if (!$('#camcheck').is(':checked')) {
        $('#camip').prop('readonly', true);
    };
    if (!$('#scorecheck').is(':checked')) {
        $('#scoreip').prop('readonly', true);
    };
    if (!$('#printcheck').is(':checked')) {
        $('#printip').prop('readonly', true);
    };

    //Router Configuration on load
    if (!$('#pppoeCheck').is(':checked')) {
        $('#pppoeUser').prop('readonly', true);
        $('#pppoePass').prop('readonly', true);
    };
    if (!$('#wanCheck').is(':checked')) {
        $('#wanSpeed').prop('readonly', true);
    };
    if (!$('#bvdConfigCheck').is(':checked')) {
        $('#bridgeConfig').prop('readonly', true);
        $('#ipbridgeConfig').prop('readonly', true);
        $('#vlanidConfig').prop('readonly', true);
        $('#ipvlanidConfig').prop('readonly', true);
    };
    if (!$('#portCamConfig').is(':checked')) {
        $('#camIpConfig').prop('readonly', true);
        $('#camPort').prop('readonly', true);
        $('#scoreIpConfig').prop('readonly', true);
        $('#scorePort').prop('readonly', true);
    };

    //Access Point Configuration on load
    if (!$('#roamConfig').is(':checked')) {
        $('#roamName1').prop('readonly', true);
        $('#roamName2').prop('readonly', true);
    };
    if (!$('#backupConfig').is(':checked')) {
        $('#backUpName1').prop('readonly', true);
        $('#backUpName2').prop('readonly', true);
        $('#backUpPass1').prop('readonly', true);
        $('#backUpPass2').prop('readonly', true);
    };
}