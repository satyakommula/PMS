var deleteQualiication = 0;
var ResumeData = null;

$("#btnAddQualification").click(function () {

    $.ajax({
        type:'GET',
        url: '/Resume/Qualifications',
        dataType: 'html',
        success: function (data) {
            $('#divEducation').append(data);
           
                $(".deleteQual").on("click", function () {
                 
                    $(this).parent().parent().remove();
                });
           
        },
        error: function (e) {
            alert(e.responseText);
        }
    });

});


$("#btnAddADTQualification").click(function () {

    $.ajax({
        type: 'GET',
        url: '/Resume/AdditionalQualifications',
        dataType: 'html',
        success: function (data) {
            $('#divAddEducation').append(data);

            $(".deleteAddQual").on("click", function () {

                $(this).parent().parent().remove();
            });

        },
        error: function (e) {
            alert(e.responseText);
        }
    });

});

$(document).ready(function () {
    $("#resumeForm").submit(function () {

        if (!$("#resumeForm").valid()) {
            return false;
        }
    var listEducation = [];
    var listAddtEducation = [];
    $($("#divEducation")[0].children).each(function () {
     var qual=   $(this).find('.ddlQualification').val();
        var univ = $(this).find('.txtUniversity').val();
        var year = $(this).find('.txtyear').val();
        var id = $(this).find('#hdnId').val();
        var educationId = $(this).find('#hdnEductionId').val();
        var resumeId = $(this).find('#hdnResumeId').val();

        listEducation.push({ Qualification: qual, University: univ, Year: year, EducationType: 1, Id: id, ResumeId: resumeId, EducationId: educationId }); 


        });


    $($("#divAddEducation")[0].children).each(function () {
        var course = $(this).find('.txtCourse').val();
        var spec = $(this).find('.txtSpec').val();
        var year = $(this).find('.txtYear').val();
        var id = $(this).find('#hdnId').val();
        var educationId = $(this).find('#hdnEductionId').val();
        var resumeId = $(this).find('#hdnResumeId').val();
        listEducation.push({ Course: course, Specialisation: spec, Year: year, EducationType: 2, Id: id, ResumeId: resumeId, EducationId: educationId  });


    });

    var resumeData = {
        Id: $("#hdnId").val(),
        ResumeId: $("#hdnResumeId").val(),
        ResumeDate: $("#ResumeDate").val(),
        Designation: $("#Designation").val(),
        Department: $("#Department").val(),
        ReceivedThrough: $("#ReceivedThrough").val(),
        Name: $("#Name").val(),
        DOB: $("#DOB").val(),
        Age: $("#Age").val(),   
        Keywords: $("#Keywords").val(),
        ExpYears: $("#ExpYears").val(),
        ExpMonths: $("#ExpMonths").val(),
        PresentCompany: $("#PresentCompany").val(),
        Position: $("#Position").val(),
        PlaceOfWorking: $("#PlaceOfWorking").val(),
        NoOfCompanies: $("#NoOfCompanies").val(),
        PresentSalary: $("#PresentSalary").val(),
        ExpectedSalary: $("#ExpectedSalary").val(),
        FatherName: $("#FatherName").val(),
        Address1: $("#Address1").val(),
        Address2: $("#Address2").val(),
        Mobile: $("#Mobile").val(),
        Mobile1: $("#Mobile1").val(),
        Email: $("#Email").val(),
        tblEducations: listEducation,
        CVPath: $("#hdnCVPath").val(),
        CVCompletePath: $("#hdnCVCompletePath").val(),
    };

    var formData = new FormData();
    formData.append("File", $('#File')[0].files[0]);
    formData.append("resumeData", JSON.stringify(resumeData));
    $.ajax({
        type: 'POST',
        url: '/Resume/SaveResume',
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

        return false;
    });

    if ($("#hdnId").val()!=undefined && $("#hdnId").val() > 0) {
        $("#ResumeDate").val($("#hdnResumeDate").val());
        $("#DOB").val($("#hdnDOB").val());
    }

});


$("#btnSearch").click(function () {

    if ($("#Designation").val() == "" && $("#Department").val() == "" && $("#Name").val() == "" && $("#Years").val() == "" && $("#Skills").val() == "") {

        alert("please select any search criteria.");
        return false;
    }

    var resumeSearchModel = {
        Designation: $("#Designation").val(),
        Department: $("#Department").val(),
        Qualification: $("#Qualification").val(),
        Years: $("#Years").val(),
        Skills: $("#Skills").val(),
    };

        $.ajax({
        type: 'POST',
        url: '/Resume/SearchResume',
        contentType: "application/json",
            data: JSON.stringify(resumeSearchModel),
        success: function (data) {
            if (ResumeData != null && ResumeData != undefined) {
                jQuery('#jqGrid').jqGrid('clearGridData');
                jQuery('#jqGrid').jqGrid('setGridParam', { data: data });
                jQuery('#jqGrid').trigger('reloadGrid');
            }
            else {
                bindGridDate(data);
            }

        },
        error: function (e) {
            alert(e.responseText);
        }
    });


});

function bindGridDate(_data) {

    //$("#jqGrid").html('');
    ResumeData = _data;
    $.jgrid.defaults.autowidth = true;
   // $.jgrid.width = 1000;
    //    $("#jqGrid").jqGrid({
    //    colModel: [
    //        { label: 'OrderID', name: 'OrderID', key: true, width: 75 },
    //        { label: 'Customer ID', name: 'CustomerID', width: 150 },
    //        { label: 'Ship Name', name: 'ShipName', width: 150 }
    //    ],
    //         data: [
    //             { OrderID: 1, CustomerID: "Angela", ShipName: "Merkel" },
    //             { OrderID: 2, CustomerID: "Vladimir", ShipName: "Putin" },
    //             { OrderID: 3, CustomerID: "David", ShipName: "Cameron" },
    //             { OrderID: 4, CustomerID: "Barack", ShipName: "Obama" },
    //             { OrderID: 5, CustomerID: "François", ShipName: "Hollande" },
    //             { OrderID: 6, CustomerID: "Angela", ShipName: "Merkel" },
    //             { OrderID: 7, CustomerID: "Vladimir", ShipName: "Putin" },
    //             { OrderID: 8, CustomerID: "David", ShipName: "Cameron" },
    //             { OrderID: 9, CustomerID: "Barack", ShipName: "Obama" },
    //             { OrderID: 10, CustomerID: "François", ShipName: "Hollande" },
    //             { OrderID: 11, CustomerID: "Angela", ShipName: "Merkel" },
    //             { OrderID: 12, CustomerID: "Vladimir", ShipName: "Putin" },
    //             { OrderID: 13, CustomerID: "David", ShipName: "Cameron" },
    //             { OrderID: 14, CustomerID: "Barack", ShipName: "Obama" },
    //             { OrderID: 15, CustomerID: "François", ShipName: "Hollande" },
    //             { OrderID: 16, CustomerID: "Angela", ShipName: "Merkel" },
    //             { OrderID: 17, CustomerID: "Vladimir", ShipName: "Putin" },
    //             { OrderID: 18, CustomerID: "David", ShipName: "Cameron" },
    //             { OrderID: 19, CustomerID: "Barack", ShipName: "Obama" },
    //             { OrderID: 20, CustomerID: "François", ShipName: "Hollande" },
    //             { OrderID: 21, CustomerID: "François", ShipName: "Hollande" },
    //             { OrderID: 22, CustomerID: "François", ShipName: "Hollande" },
    //             { OrderID: 23, CustomerID: "François", ShipName: "Hollande" }
    //    ],
    //    viewrecords: true,
    //    height: 250,
    //    rowNum: 10,
    //    pager: "#jqGridPager"
    //});

    $("#jqGrid").jqGrid({
        datatype: "local",
        data: _data,
        colNames: getColNames(_data[0]),
        colModel: getColModels(_data[0]),
        loadonce: false,
        viewrecords: true,
        height: "auto",
        rownum: 10,
        pager: "#jqgridpager",
        gridComplete: function () {
            var ids = jQuery("#jqGrid").jqGrid('getDataIDs');
            for (var i = 0; i < ids.length; i++) {
                var cl = ids[i];
                ed = '<button data-code="' + cl +'" class="btnEdit ui-button ui-corner-all ui-widget ui-button-icon-only" title="Edit"><span class="ui-button-icon ui-icon ui-icon-pencil"></span><span class="ui-button-icon-space"> </span>Edit</button>';
                be = '<button data-code="' + cl +'" class="btndownload ui-button ui-corner-all ui-widget ui-button-icon-only" title="Edit"><span class="ui-button-icon ui-icon ui-icon-circle-arrow-s"></span><span class="ui-button-icon-space"> </span>Edit</button>';
               // be = '<input class="btndownload" data-code="'+cl+'" style="width:80px;min-width:80px" type="button" value="Download"  />';
                jQuery("#jqGrid").jqGrid('setRowData', ids[i], { Download: ed+be });

               
            }
            //$(".btndownload").button({
            //    icons:"ui-icon ui-icon-pencil"

            //}
                
            //);
            $(".btndownload").click(function () {
                var id = $(this).attr("data-code");
                var path = ResumeData.find(x => x.Id == id).CVCompletePath;
                window.location = '/Resume/Download?file=' + path;
            });
            $(".btnEdit").click(function () {
                var id = $(this).attr("data-code");
                window.location.href  = '/PMS/Resume/Index?id=' + id;
            });
        }
    });
}

function getColNames(data) {
    var keys = [];
    for (var key in data) {
        if (data.hasOwnProperty(key)) {
            keys.push(key);
        }
    }
    return keys;
}
function getColModels(data) {
    var colNames = getColNames(data);
    var colModelsArray = [];
    for (var i = 0; i < colNames.length-1; i++) {
        var str;
        if (i === 0) {
            str = {
                name: colNames[i],
                index: colNames[i],
                key: true,
                editable: false,
                hidden:true
            };
        } else if (colNames[i] =="ResumeDate") {
            str = {
                name: colNames[i],
                index: colNames[i],
                editable: false,
                hidden: i === 1 || i === colNames.length - 2 ? true : false,
                formatter: 'date'
            };
        }
        else {
            str = {
                name: colNames[i],
                index: colNames[i],
                editable: false,
                hidden: i === 1 || i === colNames.length - 2 ? true : false,
            };
        }
        colModelsArray.push(str);
    }

    str = {
        name: colNames[colNames.length-1],
        index: colNames[colNames.length-1],
        editable: false,
        formatter: "button",
        sortable: false
    };
    colModelsArray.push(str);
    return colModelsArray;
}
function downloadResume(id) {
    alert(id);

    $.ajax({
        type: 'POST',
        url: '/Resume/Download',
        data: '{ "id": "' + id+'" }',
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        success: function (returnValue) {
            window.location = '/Reports/Download?file=' + returnValue;
        }
    });
}

//$(document).ready(function () {

//  //  $.jgrid.defaults.autowidth = true;

//    //$("#jqGrid").jqGrid({
//    //    colModel: [
//    //        { label: 'OrderID', name: 'OrderID', key: true, width: 75 },
//    //        { label: 'Customer ID', name: 'CustomerID', width: 150 },
//    //        { label: 'Ship Name', name: 'ShipName', width: 150 }
//    //    ],
//    //         data: [
//    //             { OrderID: 1, CustomerID: "Angela", ShipName: "Merkel" },
//    //             { OrderID: 2, CustomerID: "Vladimir", ShipName: "Putin" },
//    //             { OrderID: 3, CustomerID: "David", ShipName: "Cameron" },
//    //             { OrderID: 4, CustomerID: "Barack", ShipName: "Obama" },
//    //             { OrderID: 5, CustomerID: "François", ShipName: "Hollande" },
//    //             { OrderID: 6, CustomerID: "Angela", ShipName: "Merkel" },
//    //             { OrderID: 7, CustomerID: "Vladimir", ShipName: "Putin" },
//    //             { OrderID: 8, CustomerID: "David", ShipName: "Cameron" },
//    //             { OrderID: 9, CustomerID: "Barack", ShipName: "Obama" },
//    //             { OrderID: 10, CustomerID: "François", ShipName: "Hollande" },
//    //             { OrderID: 11, CustomerID: "Angela", ShipName: "Merkel" },
//    //             { OrderID: 12, CustomerID: "Vladimir", ShipName: "Putin" },
//    //             { OrderID: 13, CustomerID: "David", ShipName: "Cameron" },
//    //             { OrderID: 14, CustomerID: "Barack", ShipName: "Obama" },
//    //             { OrderID: 15, CustomerID: "François", ShipName: "Hollande" },
//    //             { OrderID: 16, CustomerID: "Angela", ShipName: "Merkel" },
//    //             { OrderID: 17, CustomerID: "Vladimir", ShipName: "Putin" },
//    //             { OrderID: 18, CustomerID: "David", ShipName: "Cameron" },
//    //             { OrderID: 19, CustomerID: "Barack", ShipName: "Obama" },
//    //             { OrderID: 20, CustomerID: "François", ShipName: "Hollande" },
//    //             { OrderID: 21, CustomerID: "François", ShipName: "Hollande" },
//    //             { OrderID: 22, CustomerID: "François", ShipName: "Hollande" },
//    //             { OrderID: 23, CustomerID: "François", ShipName: "Hollande" }
//    //    ],
//    //    viewrecords: true,
//    //    height: 250,
//    //    rowNum: 10,
//    //    pager: "#jqGridPager"
//    //});
//});




//$("#btnSearch").click(function () {

//    var ResumeSearchModel = {
//        Department: $("#Department").val(),
//        Designation: $("#Designation").val(),
//        Qualification: $("#Qualification").val(),
//        Years: $("#Years").val(),
//        Skills: $("#Skills").val(),

//    };


//    $.ajax({
//        type: 'POST',
//        url: '/Resume/SearchResume',
//        contentType: "application/json",
//        data: JSON.stringify( ResumeSearchModel),
//        success: function (data) {
//            alert("Success");

//        },
//        error: function (e) {
//            alert(e.responseText);
//        }
//    });


//});


