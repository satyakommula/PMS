var _OutwardData = $.extend({
    projectData: null,
    vendorData: null,
    isWorkOrderUpload: false,
    isVendor1Upload: false,
    isVendor2Upload: false,
    isVendor3Upload: false,

    validateVendors: function () {
        "use strict";
        var vendor1 = $("#VendorName1").val();
        var vendor2 = $("#VendorName2").val();
        var vendor3 = $("#VendorName3").val();

        if (vendor1 == "" && vendor2 != "" || vendor3 != "") {
            alert("Please Enter Vendor1");
            return false;
        }
        else if (vendor2 == "" && vendor3 != "") {
            alert("Please Enter Vendor2");
            return false;
        }
        else {
            return true;
        }
    },

    getProjects: function () {
        "use strict";
        $.ajax({
            url: '/ProjectMaster/GetProjects/',
            data: "",
            dataType: "json",
            type: "GET",
            async: true,
            contentType: "application/json; charset=utf-8",
            success: function (data) {
                _OutwardData.projectData = data;
                var projectcodes = _OutwardData.projectData != null ? $.map(_OutwardData.projectData, function (item) {
                    return { label: item.ProjectCode, value: item.ProjectCode };
                }) : [];

                $("#RelatedProjectCode").autocomplete({
                    source: projectcodes,
                    messages: {
                        noResults: '',
                        results: function (resultsCount) { }
                    }
                });
            },
            error: function (response) {
                alert(response.responseText);
            },
            failure: function (response) {
                alert(response.responseText);
            }
        });
    },

    getVendors: function () {
        "use strict";
        $.ajax({
            url: '/WorkOrder/GetVendors/',
            data: "",
            dataType: "json",
            type: "GET",
            async: true,
            contentType: "application/json; charset=utf-8",
            success: function (data) {
                _OutwardData.vendorData = data;
                var vendorcodes = _OutwardData.vendorData != null ? $.map(_OutwardData.vendorData, function (item) {
                    return { label: item.VendorCode, value: item.VendorCode };
                }) : [];

                $("#VendorCode").autocomplete({
                    source: vendorcodes,
                    messages: {
                        noResults: '',
                        results: function (resultsCount) { }
                    }
                });
            },
            error: function (response) {
                alert(response.responseText);
            },
            failure: function (response) {
                alert(response.responseText);
            }
        });
    }

});

$(document).ready(function () {
    "use strict";
    _OutwardData.getProjects();
    //_OutwardData.getVendors();

    $("#RelatedProjectCode").change(function () {
        if (_OutwardData.projectData != null && _OutwardData.projectData != undefined && _OutwardData.projectData.length > 0) {
            var project = _OutwardData.projectData.find(x => x.ProjectCode === $(this).val());
            if (project != null && project != undefined) {
                $("#ProjectName").val(project.ProjectName);
            }
        }

    });

    $("#RelatedProjectCode").blur(function () {
        if (_OutwardData.projectData != null && _OutwardData.projectData != undefined && _OutwardData.projectData.length > 0) {
            var project = _OutwardData.projectData.find(x => x.ProjectCode === $(this).val());
            if (project != null && project != undefined) {
                $("#ProjectName").val(project.ProjectName);
            }
        }

    });

    //$("#VendorCode").change(function () {
    //    if (_OutwardData.vendorData != null && _OutwardData.vendorData != undefined && _OutwardData.vendorData.length > 0) {
    //        var vendors = _OutwardData.vendorData.find(x => x.VendorCode === $(this).val());
    //        if (vendors != null && vendors != undefined) {
    //            $("#VendorId").val(vendors.VendorName);      //vendor Name
    //        }
    //    }

    //});

    //$("#VendorCode").blur(function () {
    //    if (_OutwardData.vendorData != null && _OutwardData.vendorData != undefined && _OutwardData.vendorData.length > 0) {
    //        var vendors = _OutwardData.vendorData.find(x => x.VendorCode === $(this).val());
    //        if (vendors != null && vendors != undefined) {
    //            $("#VendorId").val(vendors.VendorName);    //vendor Name
    //        }
    //    }

    //});

    //Form OUtward Saveing..
    $("#formOutwardWO").submit(function () {
        if (!$("#formOutwardWO").valid()) {
            return false;
        }

        //if (_OutwardData.validateVendors()) {

        var formData = new FormData();

        if ($('#flWorkOrderPath') != undefined && $('#flWorkOrderPath').val() != "") {
            formData.append("File", $('#flWorkOrderPath')[0].files[0], $('#flWorkOrderPath')[0].files[0].name);      //flWorkOrderPath
            _OutwardData.isWorkOrderUpload = true;
        }
        if ($('#Vendor1File') != undefined && $('#Vendor1File').val() != "") {
            formData.append("File", $('#Vendor1File')[0].files[0], $('#Vendor1File')[0].files[0].name);    //VendorQuotation1
            _OutwardData.isVendor1Upload = true;
        }
        if ($('#Vendor2File') != undefined && $('#Vendor2File').val() != "") {
            formData.append("File", $('#Vendor2File')[0].files[0], $('#Vendor2File')[0].files[0].name);    //VendorQuotation2
            _OutwardData.isVendor2Upload = true;
        }
        if ($('#Vendor3File') != undefined && $('#Vendor3File').val() != "") {
            formData.append("File", $('#Vendor3File')[0].files[0], $('#Vendor3File')[0].files[0].name);    //VendorQuotation3
            _OutwardData.isVendor3Upload = true;
        }
        var outwardData = {
            Id: $("#hdnId").val(),
            WOId: $("#hdnWOId").val(),
            BranchCode: $("#BranchCode").val(),
            BranchLocation: $("#BranchLocation").val(),
            Division: $("#Division").val(),
            ProjectType: $("#ProjectType").val(),
            TypeofService: $("#TypeofService").val(),
            RelatedProjectCode: $("#RelatedProjectCode").val(),
            ProjectName: $("#ProjectName").val(),
            VendorCode: $("#VendorCode").val(),
            VendorName: $("#VendorId").val(),
            WorkOrderNo: $("#WorkOrderNo").val(),
            WorkOrderDate: $("#WorkOrderDate").val(),
            DescriptionOfWO: $("#DescriptionofWO").val(),
            WorkOrderValue: $("#WorkOrderValue").val(),
            VendorName1: $("#VendorName1").val(),
            VendorName2: $("#VendorName2").val(),
            VendorName3: $("#VendorName3").val(),
            IsWorkOrderFileAttached: _OutwardData.isWorkOrderUpload,
            WOFileName: $("#hdnWorkOrderFileName").val(),
            WOFilePath: $("#hdnWorkOrderFilePath").val(),
            IsVendor1FileAttached: _OutwardData.isVendor1Upload,
            VendorFileName1: $("#hdnVendor1FileName").val(),
            VendorFilePath1: $("#hdnVendor1FilePath").val(),
            IsVendor2FileAttached: _OutwardData.isVendor2Upload,
            VendorFileName2: $("#hdnVendor2FileName").val(),
            VendorFilePath2: $("#hdnVendor2FilePath").val(),
            IsVendor3FileAttached: _OutwardData.isVendor3Upload,
            VendorFileName3: $("#hdnVendor3FileName").val(),
            VendorFilePath3: $("#hdnVendor3FilePath").val(),
        };

        formData.append("outwardData", JSON.stringify(outwardData));
        //$.ajax({
        //    type: 'POST',
        //    url: '/WorkOrder/SaveOutwardData',
        //    cache: false,
        //    contentType: false,
        //    processData: false,
        //    data: formData,
        //    dataType: 'json',
        //    success: function (data) {
        //        alert("Outwards form submitted successfully...");
        //    },
        //    error: function (e) {
        //        alert(e.responseText);
        //    }
        //});
        $.ajax({
            type: 'POST',
            url: '/WorkOrder/SaveOutwardData',
            cache: false,
            contentType: false,
            processData: false,
            data: formData,
            dataType: 'json',
            success: function (data) {
                alert("Success");

            },
            error: function (e) {
                alert(e.responseText);
            }
        });
        // }
        //else {
        return false;
        //}
    });
});
