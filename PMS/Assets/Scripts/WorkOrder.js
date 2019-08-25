var projectData = null;
var clientsData = null;
var InwardSearchData = null;
$(document).ready(function () {
    var url = window.location.href;
    if (url.indexOf("/WorkOrder/SearchInward") == -1) {
        getProjects();
        getClients();


        var projectnamess = projectData != null ? $.map(projectData, function (item) {
            return { label: item.ProjectName, value: item.ProjectName }
        }) : [];


        //$("#ProjectName").autocomplete({
        //    source: projectnamess,
        //    messages: {
        //        noResults: '',
        //        results: function (resultsCount) { }
        //    }
        //});  

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

        $("#ClientName").change(function () {

            if (clientsData != null && clientsData != undefined && clientsData.length > 0) {
                var clientsData = clientsData.find(x => x.ClientName === $(this).val());
                if (client != null && client != undefined) {
                    $("#Address1").val(client.Address1);
                    $("#Address2").val(client.Address2);
                    $("#State").val(client.State);
                    $("#PinCode").val(client.PinCode);
                }
            }

        });

        $("#ClientName").blur(function () {

            if (clientsData != null && clientsData != undefined && clientsData.length > 0) {
                var client = clientsData.find(x => x.ClientName === $(this).val());
                if (client != null && client != undefined) {
                    $("#Address1").val(client.Address1);
                    $("#Address2").val(client.Address2);
                    $("#State").val(client.State);
                    $("#PinCode").val(client.PinCode);
                }
            }

        });

        //$("#CityName").autocomplete({
        //    source: function (request, response) {
        //        $.ajax({
        //            url: "/Home/Index",
        //            type: "POST",
        //            dataType: "json",
        //            data: { Prefix: request.term },
        //            success: function (data) {
        //                response($.map(data, function (item) {
        //                    return { label: item.CityName, value: item.CityName };
        //                }))

        //            }
        //        })
        //    },
        //    messages: {
        //        noResults: "", results: ""
        //    }
        //});  



        $("#formInwardWO").submit(function () {

            if (!$("#formInwardWO").valid()) {
                return false;
            }
            var inwardData = {
                Id: $("#hdnId").val(),
                WOId: $("#hdnWOId").val(),
                BranchCode: $("#BranchCode").val(),
                BranchLocation: $("#BranchLocation").val(),
                Division: $("#Division").val(),
                ProjectType: $("#ProjectType").val(),
                TypeofService: $("#TypeofService").val(),
                ProjectCode: $("#ProjectCode").val(),
                ProjectName: $("#ProjectName").val(),
                WorkOrderNo: $("#WorkOrderNo").val(),
                WorkOrderDate: $("#WorkOrderDate").val(),
                WorkOrderValue: $("#WorkOrderValue").val(),
                WOPath: $("#hdnWOPath").val(),
                ClientName: $("#ClientName").val(),
                Address1: $("#Address1").val(),
                Address2: $("#Address2").val(),
                State: $("#State").val(),
                PinCode: $("#PinCode").val(),
                ContactPerson1: $("#ContactPerson1").val(),
                ContactPerson2: $("#ContactPerson2").val(),
                Mobile1: $("#Mobile1").val(),
                Mobile2: $("#Mobile2").val(),
                Email1: $("#Email1").val(),
                Email2: $("#Email2").val(),
                InvoiceName: $("#InvoiceName").val(),
                BilligAddress1: $("#BilligAddress1").val(),
                BillingAddress2: $("#BillingAddress2").val(),
                BillingState: $("#BillingState").val(),
                BillingPiCode: $("#BillingPiCode").val(),
                GSTIN: $("#GSTIN").val(),
                PAN: $("#PAN").val(),
                Other: $("#Other").val(),
                ScopeofService: $("#ScopeofService").val(),
            };

            var formData = new FormData();
            formData.append("File", $('#File')[0].files[0]);
            formData.append("inwardData", JSON.stringify(inwardData));
            $.ajax({
                type: 'POST',
                url: '/WorkOrder/SaveIWWorkOrder',
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

function getClients() {
    $.ajax({
        url: '/WorkOrder/GetClients/',
        data: "",
        dataType: "json",
        type: "GET",
        async: true,
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            clientsData = data;
            var clients = clientsData != null ? $.map(clientsData, function (item) {
                return { label: item.ClientName, value: item.ClientName }
            }) : [];

            $("#ClientName").autocomplete({
                source: clients,
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

$("#btnSearchInward").click(function () {

    if ($("#ProjectCode").val() == "" && $("#WorkOrderNo").val() == "" && $("#BranchCode").val() == "" && ($("#ClientName").val() == "")) {

        alert("please select any search criteria.");
        return false;
    }

    var inwardSearchModel = {
        WorkOrderNo: $("#WorkOrderNo").val(),
        ProjectCode: $("#ProjectCode").val(),
        ClientName: $("#ClientName").val(),
        BranchCode: $("#BranchCode").val(),
    };

    $.ajax({
        type: 'POST',
        url: '/WorkOrder/SearchInward',
        contentType: "application/json",
        data: JSON.stringify(inwardSearchModel),
        success: function (data) {
            if (InwardSearchData != null && InwardSearchData != undefined) {
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
    InwardSearchData = _data;
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
                var path = InwardSearchData.find(x => x.Id == id).FilePath;
                window.location = '/WorkOrder/Download?file=' + path;
            });
            $(".btnEdit").click(function () {
                var id = $(this).attr("data-code");
                window.location.href = '/PMS/WorkOrder/InwardWO?id=' + id;
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
function downloadInward(id) {
    alert(id);

    $.ajax({
        type: 'POST',
        url: '/WorkOrder/Download',
        data: '{ "id": "' + id + '" }',
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        success: function (returnValue) {
            window.location = '/WorkOrder/Download?file=' + returnValue;
        }
    });
}