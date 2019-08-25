$(document).ready(function () {
    $('#sidebarCollapse').on('click', function () {
        $('#sidebar').toggleClass('active');
    });
    $('.mainli').on('click', function () {
        $('.mainli').removeClass('active');
        $('.subli').removeClass('active');
        $(this).addClass('active');
    });


    $('.subli').on('click', function () {
        $('.mainli').removeClass('active');
        $('.subli').removeClass('active');
        $(this).addClass('active');
    });

    var url = window.location.href;
    if (url.indexOf("PMS/Resume") != -1) {

        if (url.indexOf("/Resume/SearchResume") != -1) {
            $("#aresumeDatabase").click();
          //  $("#aresumeDatabase").toggle();
            $("li").removeClass("active");
            $("#resumeSubmenu").addClass("show");
            $("#lisearchresume").addClass("active");
        }
        else {
            $("#aresumeDatabase").click();
            $("li").removeClass("active");
            $("#resumeSubmenu").addClass("show");
            $("#linewresume").addClass("active");

        }
    }

    if (url.indexOf("PMS/Vendor") != -1) {

        if (url.indexOf("Vendor/VendorRegistration") != -1) {
            $("#avendorDatabase").click();
            $("li").removeClass("active");
            $("#vendorSubmenu").addClass("show");
            $("#livendorregistration").addClass("active");
        }
        else {
            $("#avendorDatabase").click();
            $("li").removeClass("active");
            $("#vendorSubmenu").addClass("show");
            $("#livendorregistration").removeClass("active");

        }
    }

    if (url.indexOf("PMS/WorkOrder") != -1) {

        if (url.indexOf("WorkOrder/OutwardWO") != -1) {
            $("#aWorkOrder").click();
            $("li").removeClass("active");
            $("#workOrderSubmenu").addClass("show");
            $("#lioutwardWO").addClass("active");
        }
        else {
            $("#aWorkOrder").click();
            $("li").removeClass("active");
            $("#workOrderSubmenu").addClass("show");
            $("#liinwardWO").removeClass("active");

        }
    }
    if (url.indexOf("PMS/Drawing") != -1) {

        if (url.indexOf("Drawing/DrawingEntry") != -1) {
            $("#aDrawingReports").click();
            $("li").removeClass("active");
            $("#drawingReportsSubmenu").addClass("show");
            $("#liDrawingEntry").addClass("active");
        }
        else {
            $("#aDrawingReports").click();
            $("li").removeClass("active");
            $("#drawingReportsSubmenu").addClass("show");
            $("#liDrawingSearch").addClass("active");

        }
    }

    if (url.indexOf("PMS/Reports") != -1) {

        if (url.indexOf("Reports/ReportsData") != -1) {
            $("#aDrawingReports").click();
            $("li").removeClass("active");
            $("#drawingReportsSubmenu").addClass("show");
            $("#liReportsEntry").addClass("active");
        }
        else {
            $("#aDrawingReports").click();
            $("li").removeClass("active");
            $("#drawingReportsSubmenu").addClass("show");
            $("#liReportsSearch").addClass("active");

        }
    }
    if (url.indexOf("PMS/Correspondence") != -1) {

        if (url.indexOf("Correspondence/CorrespondenceData") != -1) {
            $("#aCorrespondence").click();
            $("li").removeClass("active");
            $("#correspondenceSubmenu").addClass("show");
            $("#licorrespondenceAdd").addClass("active");
        }
        else {
            $("#aCorrespondence").click();
            $("li").removeClass("active");
            $("#correspondenceSubmenu").addClass("show");
            $("#licorrespondenceSearch").addClass("active");

        }
    }

    if (url.indexOf("PMS/Vendor") != -1) {
        $("#avendorDatabase").click();
        $("li").removeClass("active");
        $("#vendorSubmenu").addClass("show");
        if (url.indexOf("Vendor/VendorRegistration") != -1) {
           
            $("#livendorregistration").addClass("active");
        }
        else if (url.indexOf("Vendor/VendorAllocation") != -1) {
            $("#liVendorAllocation").addClass("active");
        }
        else{
            $("#liVendorEvalation").addClass("active");
        }
    }

    if (url.indexOf("PMS/Invoice") != -1) {
        $("#ainvoice").click();
        $("li").removeClass("active");
        $("#invoicesubmenu").addClass("show");
        if (url.indexOf("Invoice/InvoiceData") != -1) {

            $("#liInvoiceData").addClass("active");
        }
        else {
            $("#liInvoiceConsultant").addClass("active");
        }
    }
});