$(document).ready(function () {
    $(document).on('click', '#menuManageLocation a', function (e) {
        var $me = $(this),
            currStep = window.tour.getCurrentStep();
        if (currStep == 2) {
            e.preventDefault();
        }
    });

    $(document).on('click', '#menuManageLandingPage a', function (e) {
        var $me = $(this),
            currStep = window.tour.getCurrentStep();
        if (currStep == 5) {
            e.preventDefault();
        }
    });

    $(document).on('click', '#menuManageAdvCRUD a', function (e) {
        var $me = $(this),
            currStep = window.tour.getCurrentStep();
        if (currStep == 9) {
            e.preventDefault();
        }
    });

    $(document).on('click', '#menuManageAdvSchedule a', function (e) {
        var $me = $(this),
            currStep = window.tour.getCurrentStep();
        if (currStep == 9) {
            e.preventDefault();
        }
    });

    $(document).on('click', '#menuManageAdvSchedule a', function (e) {
        var $me = $(this),
            currStep = window.tour.getCurrentStep();
        if (currStep == 12) {
            e.preventDefault();
        }
    });

    //$(document).on('click', '#btnCreateLocation', function (e) {
    //    var $me = $(this),
    //        currStep = window.tour.getCurrentStep();
    //    if (currStep == 2) {
    //        e.preventDefault();
    //        window.WISKY.location.openCreateModal(function () { });
    //    }
    //});

    //$(document).on('shown.bs.modal', '#createModal', function (e) {
    //    var $me = $(this),
    //        currStep = window.tour.getCurrentStep();
    //    if (currStep == 2) {
    //        window.tour.next();
    //    }
    //});
});
// Instance the tour
window.tour = new Tour({
    steps: [
    {
        // 1. dashboard - welcome text
        //path: '/brandmanager/home/dashboard',
        element: "#btn-locations",
        title: "Welcome, " + $('#brandName').val() + ".",
        content: "It seems like your first time here."
                + "<br/> This tutorial will help you learn more about WiSky."
                + "<br/><br/><b>Let's get started!</b>"
    },
    {
        // 2. menu - location menu
        element: "#menuManageLocation",
        title: "Manage location",
        content: "This is where you can manage all the locations of your brand."
                + "<br/><br/><b>Click here to explore.</b>",
        placement: 'top',
        reflex: true,
        onShown: function () {
            $('.popover.tour').css('position', 'fixed');
            $("[data-role='next']").attr('disabled', 'disabled');
        }
    },
    {
        // 3. location - show create modal
        //path: '/brandmanager/location',
        element: "#btnCreateLocation",
        title: "Create your location",
        content: "You can click here to create your own location.",
        placement: 'left'
    },
    //{
    //    // location - general info
    //    path: '/brandmanager/location',
    //    element: "#titleLocationInfo",
    //    title: "Location information",
    //    content: "This is all the information of your location.",
    //    placement: 'left'
    //},
    //{
    //    // location - contact info
    //    path: '/brandmanager/location',
    //    element: "#titleContact",
    //    title: "Contact information",
    //    content: "And here's your contact information.",
    //    placement: 'left'
    //},
    //{
    //    // location - fill and submit
    //    path: '/brandmanager/location',
    //    element: "#submit-location-form-btn",
    //    title: "Create location",
    //    content: "Let's fill all the information then click here to create a new location.",
    //    placement: 'right',
    //    reflex: true,
    //    onShown: function () {
    //        $("[data-role='next']").attr('disabled', 'disabled');
    //    }
    //},
    {
        // 4. location - complete
        //path: '/brandmanager/location',
        element: "#tblHeaderLocationName",
        title: "Location list",
        content: "When you've already got your own locations, all of them will be displayed here."
                + "<br/><br/><b>Let's learn what you can do with these locations.</b>",
        placement: 'top'
    },
    {
        // 5. menu - landing page menu
        element: "#menuManageLandingPage",
        title: "Manage landing page",
        content: "Landing page is the first thing to impress your customers."
                + "<br/><br/><b>Click here to choose what to show them.</b>",
        placement: 'top',
        reflex: true,
        onShown: function () {
            //$('.popover.tour').css('position', 'fixed');
            $("[data-role='next']").attr('disabled', 'disabled');
        }
    },
    {
        // 6. landingPage - show create modal
        //path: '/brandmanager/landingpages',
        element: "#btnCreateLandingPage",
        title: "Create landing page",
        content: "You can click here to create landing pages for your locations with your own welcome-text, background images, login modes, or even time to display.",
        placement: 'left'
    },
    {
        // 7. landingPage - demo
        //path: '/brandmanager/landingpages',
        element: "#btnDemoLandingPage",
        title: "View demo",
        content: "Click here to preview your landing page before moving on next step.",
        placement: 'left'
    },
    {
        // 8. menu - adv menu
        element: "#menuManageAdvertising",
        title: "Manage advertising",
        content: "After login from landing page, customers will see an advertisement."
                + "<br/><br/><b>Here is the advertising management menu.</b>",
        placement: 'top',
        onShown: function () {
            //$('.popover.tour').css('position', 'fixed');
            $('#menuManageAdvertising a').click();
        }
    },
    {
        // 9. menu - adv menu
        element: "#menuManageAdvCRUD",
        title: "Manage advertising",
        content: "The advertisement can be a Facebook fanpage, a website, a video, an mobile app, a survey or a special offer."
                + "<br/><br/><b>Click here to manage your advertisements.</b>",
        placement: 'top',
        reflex: true,
        onShown: function () {
            //$('.popover.tour').css('position', 'fixed');
            $("[data-role='next']").attr('disabled', 'disabled');
        }
    },
    {
        // 10. adv - show create modal
        //path: '/campaign/advertising',
        element: "#create-advertising",
        title: "Create advertisement",
        content: "You can click here to create your advertisement.",
        placement: 'left'
    },
    {
        // 11. landingPage - demo
        //path: '/campaign/advertising',
        element: "#btnDemoAdv",
        title: "View demo",
        content: "Click here to preview your advertisement.",
        placement: 'left',
        onHidden: function () {
            $('#menuManageAdvertising a').click();
        }
    },
    {
        // 12. menu - adv menu
        element: "#menuManageAdvSchedule",
        title: "Manage advertising",
        content: "When you've already have some advertisement, you can check which advertisement would be shown in a particular time period."
                + "<br/><br/><b>Click here to view Advertising schedule.</b>",
        placement: 'top',
        reflex: true,
        onShown: function () {
            //$('.popover.tour').css('position', 'fixed');
            $("[data-role='next']").attr('disabled', 'disabled');
        }
    },
    {
        // 13. finish
        //path: '/campaign/advertising/schedule',
        orphan: true,
        title: "Bravo!",
        content: "You've completed the WiSky tutorial for the basic steps."
                + "<br/>There's many features for you to explore."
                + "<br/><br/><b>Please enjoy yourself!</b>",
        placement: 'top',
        reflex: true,
        onShown: function () {
            //$('.popover.tour').css('position', 'fixed');
            $("[data-role='next']").attr('disabled', 'disabled');
        }
    }


    ],
    template: "<div class='popover tour' style='max-width: 350px;'>"
                + "<div class='arrow'></div>"
                + "<h3 class='popover-title'></h3>"
                + "<div class='popover-content'></div>"
                + "<div class='popover-navigation '>"
                    + "<button class='btn btn-default bgm-lightblue m-r-5' data-role='prev'>« Prev</button>"
                    + "<button class='btn btn-default bgm-lightblue' data-role='next'>Next »</button>"
                    + "<button class='btn btn-default bgm-orange m-l-30' data-role='end'><i class='fa fa-check'></i> I Got it!</button>"
                + "</div>"
            + "</div>"
});




//start tour
function StartOverallTour() {
    var countLocation = parseInt($('#locationCount').val());
    if (countLocation == 0) {
        window.tour.init();
        window.tour.start();
    }    
}
