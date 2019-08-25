var projectData = null;
var ReportsData = null;
$(document).ready(function () {
    var url = window.location.href;
    if (url.indexOf("/Reports/SearchReports") == -1) {
        getProjects();

        var projectnamess = projectData != null ? $.map(projectData, function (item) {
            return { label: item.ProjectName, value: item.ProjectName }
        }) : [];

        $("#ProjectCode").change(function () {

            if (projectData != null && projectData != undefined && projectData.length > 0) {
                var project = projectData.find(x => x.ProjectCode === $(this).val());
                if (project != null && project != undefined) {
                    $("#ProjectName").val(project.ProjectName);
                }
            }

        });

        $("#ProjectCode").blur(function () {

            if (projectData != null && projectData != undefined && projectData.length > 0) {
                var project = projectData.find(x => x.ProjectCode === $(this).val());
                if (project != null && project != undefined) {
                    $("#ProjectName").val(project.ProjectName);
                }
            }

        });
    }

    $("#ReportsForm").submit(function () {

        if (!$("#ReportsForm").valid()) {
            return false;
        }
        var isFileUploaded = false;
        var isOtherFileUploaded = false;

        var formData = new FormData();
        formData.append("__RequestVerificationToken", $('input[name="__RequestVerificationToken"]').val());

        if ($('#File').val() != '') {
            formData.append("File", $('#File')[0].files[0]);
            isFileUploaded = true;
        }
        if ($('#OtherFile').val() != '') {
            formData.append("File", $('#OtherFile')[0].files[0]);
            isOtherFileUploaded = true;
        }




        var data = {
            Id: $("#hdnId").val(),
            ReportId: $("#hdnReportId").val(),
            ProjectCode: $("#ProjectCode").val(),
            ProjectName: $("#ProjectName").val(),
            ReportNo: $("#ReportNo").val(),
            ReportDate: $("#ReportDate").val(),
            ReportTitle: $("#ReportTitle").val(),
            Chainage: $("#Chainage").val(),
            DateOfSubmission: $("#DateOfSubmission").val(),
            SubmittedTo: $("#SubmittedTo").val(),
            SoftHardCopy: $("#SoftHardCopy").val(),
            Department: $("#Department").val(),
            Comments: $("#Comments").val(),
            ViaEmailLetter: $("#ViaEmailLetter").val(),
            OtherDate: $("#OtherDate").val(),
            RevisionNo: $("#RevisionNo").val(),
            RevisionDate: $("#RevisionDate").val(),
            OtherSubmittedTo: $("#OtherSubmittedTo").val(),
            OtherSoftHardCopy: $("#OtherSoftHardCopy").val(),
            ApprovalDate: $("#ApprovalDate").val(),
            Remarks: $("#Remarks").val(),
            FileName: $("#hdnFileName").val(),
            FilePath: $("#hdnFilePath").val(),
            OtherFileName: $("#hdnOtherFileName").val(),
            OtherFilePath: $("#hdnOtherFilePath").val(),
            IsFileAttached: isFileUploaded,
            IsOtherFileAttached: isOtherFileUploaded
        };

        formData.append("data", JSON.stringify(data));
        $.ajax({
            type: 'POST',
            url: '/Reports/SaveReport',
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
    if ($("#hdnId").val() != undefined && $("#hdnId").val() > 0) {
        $("#ReportDate").val($("#hdnReportDate").val());
        $("#DateOfSubmission").val($("#hdnDateOfSubmission").val());
        $("#OtherDate").val($("#hdnOtherDate").val());
        $("#RevisionDate").val($("#hdnRevisionDate").val());
        $("#ApprovalDate").val($("#hdnApprovalDate").val());
    }

});

function getProjects() {
    $.ajax({
        url: '/ProjectMaster/GetProjects/',
        data: "",
        dataType: "json",
        type: "GET",
        async: true,
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            projectData = data;
            var projectcodes = projectData != null ? $.map(projectData, function (item) {
                return { label: item.ProjectCode, value: item.ProjectCode }
            }) : [];

            $("#ProjectCode") ({
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
}

$("#btnSearch").click(function () {

    if ($("#ProjectCode").val() == "" && $("#ReportNo").val() == "" && $("#ReportTitle").val() == "" && ($("#Department").val() == "0" || $("#Department").val() == undefined)) {

        alert("please select any search criteria.");
        return false;
    }

    var reportSearchModel = {
        ReportNo: $("#ReportNo").val(),
        ProjectCode: $("#ProjectCode").val(),
        ReportTitle: $("#ReportTitle").val(),
        Department: $("#Department").val(),
    };

    $.ajax({
        type: 'POST',
        url: '/Reports/SearchReports',
        contentType: "application/json",
        data: JSON.stringify(reportSearchModel),
        success: function (data) {
            if (ReportsData != null && ReportsData != undefined) {
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
    ReportsData = _data;
    $.jgrid.defaults.autowidth = true;
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
        emptyrecords: "No records to display",
        gridComplete: function () {
            var ids = jQuery("#jqGrid").jqGrid('getDataIDs');
            for (var i = 0; i < ids.length; i++) {
                var cl = ids[i];
                ed = '<button data-code="' + cl + '" class="btnEdit ui-button ui-corner-all ui-widget ui-button-icon-only" title="Edit"><span class="ui-button-icon ui-icon ui-icon-pencil"></span><span class="ui-button-icon-space"> </span>Edit</button>';
                be = '<button data-code="' + cl + '" class="btndownload ui-button ui-corner-all ui-widget ui-button-icon-only" title="Edit"><span class="ui-button-icon ui-icon ui-icon-circle-arrow-s"></span><span class="ui-button-icon-space"> </span>Edit</button>';
                jQuery("#jqGrid").jqGrid('setRowData', ids[i], { Download: ed + be });
            }

            $(".btndownload").click(function () {
                var id = $(this).attr("data-code");
                var path = ReportsData.find(x => x.Id == id).FilePath;
                window.location = '/Correspondence/Download?file=' + path;
            });
            $(".btnEdit").click(function () {
                var id = $(this).attr("data-code");
                window.location.href = '/PMS/Reports/ReportsData?id=' + id;
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
    for (var i = 0; i < colNames.length - 1; i++) {
        var str;
        if (i === 0) {
            str = {
                name: colNames[i],
                index: colNames[i],
                key: true,
                editable: false,
                hidden: true
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
        name: colNames[colNames.length - 1],
        index: colNames[colNames.length - 1],
        editable: false,
        formatter: "button",
        sortable: false
    };
    colModelsArray.push(str);
    return colModelsArray;
}
function downloadCorrespondence(id) {
    alert(id);

    $.ajax({
        type: 'POST',
        url: '/Reports/Download',
        data: '{ "id": "' + id + '" }',
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        success: function (returnValue) {
            window.location = '/Reports/Download?file=' + returnValue;
        }
    });
}


