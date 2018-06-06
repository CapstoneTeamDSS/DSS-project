WISKY.landing = function () {
    function init() {
        $(document).ready(function () {
            InitDatatable();
            $(document).on('click', '[data-onClick="open-create-modal"]', function () {
                showAddingModal();
            });

        });
    };


    // edit landing page
    function showEditingModal(landingID) {
        $.ajax({
            url: '/BrandManager/LandingPages/Edit',
            data: { id: landingID },
            type: "POST",
            success: function (result) {
                $('#editLandingPageModal').html(result);
                $('#editLandingPageModal').modal('show');
                setupSliders();


            },
            error: function (error) {
                alert("Error Occured");
            }
        });
    }

    function initModal() {
        $(document).ready(function () {
            //$("#passwordSlider").fadeTo('fast', 0.3);
            setModalHeaderWidth();

            // checkboxes events
            $(document).on('change', '.toggle-switch > input[id*="toggle-switch-"]', function () {
                var me = $(this),
                    targetId = me.data('target');
                if (me.prop('checked')) {
                    $(targetId).removeAttr('disabled');
                } else {
                    $(targetId).attr('disabled', 'disabled');
                }

            });


            $(document).on('change', '#toggle-switch-2', function () {
                if ($(this).prop('checked')) {
                    $('#password-field').fadeIn();
                } else {
                    $('#password-field').fadeOut();
                }
            });

            $(document).on('change', '#toggle-switch-2r', function () {
                if ($(this).prop('checked')) {
                    $('#returnPassword-field').fadeIn();
                } else {
                    $('#returnPassword-field').fadeOut();
                }
            });

            //Modal hidding
            $(document).on('hidden.bs.modal', '#createLandingPageModal, #editLandingPageModal', function () {
                $(this).removeData('bs.modal');
            });

            $(document).on('change', '#BrandLogo', function () {
                readURL(this);
            });


            $(document).on('change', '#BackgroundMobileImg', function () {
                readMobileImgURL(this);
            });

            $(document).on('change', '#BackgroundImg', function () {
                readBackgroundURL(this);
            });

            $(document).on('change', '#IsEnableSuccess', function () {
                if ($('#IsEnableSuccess').prop('checked')) {
                    $('#SuccessUrl').removeAttr('disabled')
                } else {
                    $('#SuccessUrl').attr('disabled', 'disabled');
                }
            });


            //Form submit
            //$(document).on('submit', '#createLandingPageForm', function (e) {
            //    e.preventDefault();
            //    var isvalid = true;
            //    if ($('#IsEnableSuccess').prop('checked') && $('#SuccessUrl').val() == '') {
            //        isvalid = false;
            //        alert("Success page url can not be blank.");
            //    }
            //    if ($('#IsEnablePassword').prop('checked') && $('#PasswordGenerate') == '') {
            //        isvalid = false;
            //        alert("Authorize password can not be blank.");
            //    }
            //    if ($('#IsEnableRandomPassword').prop('checked') && $('#randomPasswordGenerate') == '') {
            //        isvalid = false;
            //        alert("Authorize password can not be blank.");
            //    }

            //    if ($('#TemplateId').val() == null) {
            //        isvalid = false;
            //        alert("Please select template");
            //    }

            //    if (isvalid) {
            //        addLandingPage();
            //    }
            //});

            //"Edit" Form submit
            $(document).on('submit', '#editLandingPageForm', function (e) {
                e.preventDefault();
                var isvalid = true;
                if ($('#IsEnableSuccess').prop('checked') && $('#SuccessUrl').val() == '') {
                    isvalid = false;
                    alert("Success page url can not be blank.");
                }
                if ($('#IsEnablePassword').prop('checked') && $('#PasswordGenerate') == '') {
                    isvalid = false;
                    alert("Authorize password can not be blank.");
                }
                if ($('#IsEnableRandomPassword').prop('checked') && $('#randomPasswordGenerate') == '') {
                    isvalid = false;
                    alert("Authorize password can not be blank.");
                }
                if (isvalid) {
                    editLandingPage();
                }
            });

        });
    };

    


    //redraw datatable without reload
    function reDrawDatatable() {
        $.fn.dataTableExt.oApi.fnStandingRedraw = function (oSettings) {
            if (oSettings.oFeatures.bServerSide === false) {
                var before = oSettings._iDisplayStart;
                oSettings.oApi._fnReDraw(oSettings);
                //iDisplayStart has been reset to zero - so lets change it back
                oSettings._iDisplayStart = before;
                oSettings.oApi._fnCalculateEnd(oSettings);
            }
            //draw the 'current' page
            oSettings.oApi._fnDraw(oSettings);
        };
        $("#LandingPageDatatable").dataTable().fnStandingRedraw();
    }

    // add new landing page
    function showAddingModal() {
        $.ajax({
            type: "POST",
            url: '/BrandManager/LandingPages/Create',
            success: function (result) {
                $('#createLandingPageModal').html(result);
                $('#createLandingPageModal').modal('show');
                generatePassword();
                setupBootstrapToggle();
                setupSliders();
                setModalHeaderWidth();
            },
            error: function (error) {
                alert("Error Occured");
            }
        });
    }


    //============================= Create modal functions =============================
    function setupBootstrapToggle() {
        $('[data-toggle="toggle"]').bootstrapToggle();
    }

    function setModalHeaderWidth() {
        //set width
        $('#fixedHeader').css('width', $('#modal-dialog').width() + "px");
        $(window).resize(function () {
            $('#fixedHeader').css('width', $('#modal-dialog').width() + "px");
        });
    }

    function setupSliders() {
        $.each($('.input-slider'), function (i, e) {
            var slider = $(e).get(0);
            var a = $(e).attr('data-value');
            if(a == "-1"){
                a = "100";
            }
            noUiSlider.create(slider, {
                start: parseInt(a || 0),
                step: 10,
                range: {
                    'min': 0,
                    'max': 100,
                }
            });
            slider.noUiSlider.on('update', function (values, handle) {
                $($(e).attr('data-target')).html(parseInt(values[0]));
            });
            //$(e).Link().to($($(e).attr('data-target')));
            //$(e).Link().to($(e).next());
        });
    }



    //Preview image
    function readURL(input) {
        if (input.files && input.files[0]) {
            var reader = new FileReader();

            reader.onload = function (e) {
                $('#logoPreview').attr('src', e.target.result);
            }

            reader.readAsDataURL(input.files[0]);
        }
    }

    //background
    function readBackgroundURL(input) {
        if (input.files && input.files[0]) {
            var reader = new FileReader();

            reader.onload = function (e) {
                $('#backgroundPreview').attr('src', e.target.result);
            }

            reader.readAsDataURL(input.files[0]);
        }
    }

    //background
    function readMobileImgURL(input) {
        if (input.files && input.files[0]) {
            var reader = new FileReader();

            reader.onload = function (e) {
                $('#backgroundMobilePreview').attr('src', e.target.result);
            }

            reader.readAsDataURL(input.files[0]);
        }
    }

    //Upload Landing Template
    function addLandingPage() {
        var formData = new FormData(document.getElementById('createLandingPageForm'));
        var brandLogo = document.getElementById("BrandLogo").files[0];
        var backgroundImg = document.getElementById("BackgroundImg").files[0];
        var backgroundMobileImg = document.getElementById("BackgroundMobileImg").files[0];

        formData.append("BrandLogo", brandLogo);
        formData.append("BackgroundImg", backgroundImg);
        formData.append("BackgroundMobileImg", backgroundMobileImg);

        var clickthroughtBw = parseInt($('#clickthroughDisplay').html()),
            passwordtBw = parseInt($('#passwordDisplay').html()),
            facebookBw = parseInt($('#facebookDisplay').html()),
            googleBw = parseInt($('#googleDisplay').html());

        formData.append("BandwidthClickthrough", clickthroughtBw);
        formData.append("BandwidthPassword", passwordtBw);
        formData.append("BandwidthFacebook", facebookBw);
        formData.append("BandwidthGoogle", googleBw);

        formData.append("IsEnableSuccess", $('#IsEnableSuccess').prop('checked'));
        formData.append("IsEnableClickThrough", $('#toggle-switch-1').prop('checked'));
        formData.append("IsEnablePassword", $('#toggle-switch-2').prop('checked'));
        formData.append("IsEnableRandomPassword", $('#toggle-switch-5').prop('checked'));
        formData.append("IsEnableFacebook", $('#toggle-switch-3').prop('checked'));
        formData.append("IsEnableGoogle", $('#toggle-switch-4').prop('checked'));

        // Advance settings
        formData.append("dayParting", collectPartingSelection('#dayPartingCheck'));
        formData.append("hourParting", collectPartingSelection('#hourPartingCheck'));
        formData.append("DeviceOs", collectPartingSelection('#deviceFilterCheck'));

        $.ajax({
            type: "POST",
            url: '/BrandManager/LandingPages/AddLandingPage',
            data: formData,
            success: function (result) {
                alert(result.message);
                if (result.success) {
                    $('#createLandingPageModal').modal('hide');
                    reDrawDatatable();
                }
            },
            async: false,
            cache: false,
            contentType: false,
            processData: false,
            error: function (error) {
                alert("Error Occured");
            }
        });
    }

    //Upload Landing Template
    function editLandingPage() {
        var formData = new FormData(document.getElementById('editLandingPageForm'));
        var brandLogo = document.getElementById("BrandLogo").files[0];
        var backgroundImg = document.getElementById("BackgroundImg").files[0];
        var backgroundMobileImg = document.getElementById("BackgroundMobileImg").files[0];

        formData.append("BrandLogo", brandLogo);
        formData.append("BackgroundImg", backgroundImg);
        formData.append("BackgroundMobileImg", backgroundMobileImg);

        var clickthroughtBw = parseInt($('#clickthroughDisplay').html()),
            passwordtBw = parseInt($('#passwordDisplay').html()),
            facebookBw = parseInt($('#facebookDisplay').html()),
            googleBw = parseInt($('#googleDisplay').html());

        formData.append("BandwidthClickthrough", clickthroughtBw);
        formData.append("BandwidthPassword", passwordtBw);
        formData.append("BandwidthFacebook", facebookBw);
        formData.append("BandwidthGoogle", googleBw);

        formData.append("IsActive", $('#IsActive').prop('checked'));
        formData.append("IsEnableSuccess", $('#IsEnableSuccess').prop('checked'));
        formData.append("IsEnableClickThrough", $('#toggle-switch-1').prop('checked'));
        formData.append("IsEnablePassword", $('#toggle-switch-2').prop('checked'));
        formData.append("IsEnableRandomPassword", $('#toggle-switch-5').prop('checked'));
        formData.append("IsEnableFacebook", $('#toggle-switch-3').prop('checked'));
        formData.append("IsEnableGoogle", $('#toggle-switch-4').prop('checked'));
        
        // Advance settings
        formData.append("dayParting", collectPartingSelection('#dayPartingCheck'));
        formData.append("hourParting", collectPartingSelection('#hourPartingCheck'));
        formData.append("DeviceOs", collectPartingSelection('#deviceFilterCheck'));

        formData.append("IsEnableReturnLogin", $('#enableReturnLogin').prop('checked'));
        formData.append("IsEnableReturnClickThrough", $('#toggle-switch-1r').prop('checked'));
        formData.append("IsEnableReturnPassword", $('#toggle-switch-2r').prop('checked'));
        formData.append("IsEnableReturnRandomPassword", $('#toggle-switch-5r').prop('checked'));
        formData.append("IsEnableReturnFacebook", $('#toggle-switch-3r').prop('checked'));
        formData.append("IsEnableReturnGoogle", $('#toggle-switch-4r').prop('checked'));

        var returnClickthroughtBw = parseInt($('#returnClickthroughDisplay').html()),
            returnPasswordtBw = parseInt($('#returnPasswordDisplay').html()),
            returnFacebookBw = parseInt($('#returnFacebookDisplay').html()),
            returnGoogleBw = parseInt($('#returnGoogleDisplay').html());

        console.log(returnClickthroughtBw);
        formData.append("ReturnBandwidthClickthrough", returnClickthroughtBw);
        formData.append("ReturnBandwidthPassword", returnPasswordtBw);
        formData.append("ReturnBandwidthFacebook", returnFacebookBw);
        formData.append("ReturnBandwidthGoogle", returnGoogleBw);

        $.ajax({
            type: "POST",
            url: '/BrandManager/LandingPages/EditLandingPage',
            data: formData,
            success: function (result) {
                alert(result.message);
                if (result.success) {
                    $('#createLandingPageModal').modal('hide');
                    reDrawDatatable();
                }
            },
            async: false,
            cache: false,
            contentType: false,
            processData: false,
            error: function (error) {
                alert("Error Occured");
            }
        });
    }

    return {
        init: init,
        initModal: initModal,
        showEditingModal: showEditingModal
    };
}();
