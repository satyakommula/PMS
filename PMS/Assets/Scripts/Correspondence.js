var CorrespondenceData = null;
var projectData = null;
$(document).ready(function () {
    var url = window.location.href;
    $("#CorrespondenceForm").submit(function () {

        if (!$("#CorrespondenceForm").valid()) {
            return false;
        }

        var CorrespondenceData = {
            Id: $("#hdnId").val(),
            CorrespondenceId: $("#hdnCorrespondenceId").val(),
            CorrespondenceType: $("input[name='CorrespondenceType']:checked").val(),
            ProjectCode: $("#ProjectCode").val(),
            Department: $("#Department").val(),
            ProjectName: $("#ProjectName").val(),
            FromName: $("#FromName").val(),
            FromDesignation: $("#FromDesignation").val(),
            FromOrganisation: $("#FromOrganisation").val(),
            FromAddress: $("#FromAddress").val(),
            ToName: $("#ToName").val(),
            ToDesignation: $("#ToDesignation").val(),
            ToOrganisation: $("#ToOrganisation").val(),
            ToAddress: $("#ToAddress").val(),
            CopyToName: $("#CopyToName").val(),
            CopyToDesignation: $("#CopyToDesignation").val(),
            CopyToOrganisation: $("#CopyToOrganisation").val(),
            CopyToAddress: $("#CopyToAddress").val(),
            Subject: $("#Subject").val(),
            LetterNo: $("#LetterNo").val(),
            LetterDate: $("#LetterDate").val(),
            Reference1: $("#Reference1").val(),
            ReferenceDate1: $("#ReferenceDate1").val(),
            Reference2: $("#Reference2").val(),
            ReferenceDate2: $("#ReferenceDate2").val(),
            Reference3: $("#Reference3").val(),
            ReferenceDate3: $("#ReferenceDate3").val(),
            Keywords: $("#Keywords").val(),
            ReceivedThrough: $("#ReceivedThrough").val(),
            ReceivedDate: $("#ReceivedDate").val(),
            EmailID1: $("#EmailID1").val(),
            EmailID2: $("#EmailID2").val(),
            EmailID3: $("#EmailID3").val(),
            ReceivedType: $("#ReceivedType").val(),
            ReceivedDate1: $("#ReceivedDate1").val(),
            ReceivedBy1: $("#ReceivedBy1").val(),
            ReceivedDate2: $("#ReceivedDate2").val(),
            ReceivedBy2: $("#ReceivedBy2").val(),
            FilePath: $("#hdnFilePath").val(),
        };

        var formData = new FormData();
        formData.append("File", $('#File')[0].files[0]);
        formData.append("correspondenceData", JSON.stringify(CorrespondenceData));
        $.ajax({
            type: 'POST',
            url: '/Correspondence/SaveCorrespondence',
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
    if (url.indexOf("/Correspondence/SearchCorrespondence") == -1) {
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

    if ($("#hdnId").val() != undefined && $("#hdnId").val() > 0) {
        $("#LetterDate").val($("#hdnLetterDate").val());
        $("#ReferenceDate1").val($("#hdnReferenceDate1").val());
        $("#ReferenceDate2").val($("#hdnReferenceDate2").val());
        $("#ReferenceDate3").val($("#hdnReferenceDate3").val());
        $("#ReceivedDate").val($("#hdnReceivedDate").val());
        $("#ReceivedDate1").val($("#hdnReceivedDate1").val());
        $("#ReceivedDate2").val($("#hdnReceivedDate2").val());
    }
});

$("#btnSearchCorrespondence").click(function () {

    if ($("#ProjectCode").val() == "" && $("#LetterNo").val() == "" && $("#Keywords").val() == "" && $("input[name='CorrespondenceType']:checked").val() == "0") {

        alert("please select any search criteria.");
        return false;
    }

    var correspondenceSearchModel = {
        CorrespondenceType: $("input[name='CorrespondenceType']:checked").val(),
        LetterNo: $("#LetterNo").val(),
        ProjectCode: $("#ProjectCode").val(),
        KeyWords: $("#KeyWords").val(),
    };

    $.ajax({
        type: 'POST',
        url: '/Correspondence/SearchCorrespondence',
        contentType: "application/json",
        data: JSON.stringify(correspondenceSearchModel),
        success: function (data) {
            if (CorrespondenceData != null && CorrespondenceData != undefined) {
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
    CorrespondenceData = _data;
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
                var path = CorrespondenceData.find(x => x.Id == id).FilePath;
                window.location = '/Correspondence/Download?file=' + path;
            });
            $(".btnEdit").click(function () {
                var id = $(this).attr("data-code");
                window.location.href = '/PMS/Correspondence/CorrespondenceData?id=' + id;
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
        } else if (colNames[i] == "LetterDate") {
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
                hidden: i === 1 || i == 4 || i === colNames.length - 2 || i === colNames.length - 3 ? true : false,
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
        url: '/Correspondence/Download',
        data: '{ "id": "' + id + '" }',
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        success: function (returnValue) {
            window.location = '/Reports/Download?file=' + returnValue;
        }
    });
}


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

            $("#ProjectCode").autocomplete({
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
