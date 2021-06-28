<%@ Page Language="VB" AutoEventWireup="false" CodeFile="EventLog.aspx.vb" Inherits="dropthings_Admin_EventLog" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Historical Event Log</title>
    <link href="../../Scripts/jtable/Content/Site.css" rel="stylesheet" type="text/css" />
    <link href="../../Scripts/jtable/Content/themes/metroblue/jquery-ui.css" rel="stylesheet"
        type="text/css" />
    <!-- jTable style file -->
    <link href="../../Scripts/jtable/themes/metro/blue/jtable.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/modernizr-2.6.2.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery-ui.js" type="text/javascript"></script>
    <script src="../../Scripts/jtable/jquery.jtable.js" type="text/javascript"></script>
    <!-- A helper library for JSON serialization -->
    <script type="text/javascript" src="../../Scripts/jtable/external/json2.js"></script>
    <!-- Core jTable script file -->
    <script type="text/javascript" src="../../Scripts/jtable/jquery.jtable.js"></script>
    <!-- ASP.NET Web Forms extension for jTable -->
    <script type="text/javascript" src="../../Scripts/jtable/extensions/jquery.jtable.aspnetpagemethods.js"></script>
    <!-- Formater datetime --->
    <script type="text/javascript" src="../../Scripts/moment.min.js"></script>
    <meta charset="utf-8" />
    <style>
        div.filtering
        {
            border: 1px solid #999;
            margin-bottom: 5px;
            padding: 10px;
            background-color: #EEE;
        }
        #DateTo
        {
            width: 168px;
        }
    </style>
    <script>
        $(function () {
            $("#DateFrom").datepicker();
        });
        $(function () {
            $("#DateTo").datepicker();
        });

        function Validate() {
            var Result = "";
            var CHK = document.getElementById("<%=ddlTypeTrace.ClientID%>");
            var checkbox = CHK.getElementsByTagName("input");
            var counter = 0;
            for (var i = 0; i < checkbox.length; i++) {
                if (checkbox[i].checked) {
                    if (counter == 0) {
                        Result = checkbox[i].value;
                    } else {
                        Result = Result + "," + checkbox[i].value;
                    }
                    counter++;
                }
            }
            return Result;
        }
        function onSplInstructionChecked(chkbox) {
            if (chkbox.value == -1) {
                var checked = chkbox.checked;
                var CHK = document.getElementById("<%=ddlTypeTrace.ClientID%>");
                var checkbox = CHK.getElementsByTagName("input");
                for (var i = 0; i < checkbox.length; i++) {
                    checkbox[i].checked = checked;
                }
            }
        }
    </script>
</head>
<body>
    <div class="site-container">
        <div class="filtering">
            <form id="Form1" runat="server">
            <table style="width: 750px">
                <tr>
                    <td style="text-align: center">
                        <h1>
                            Historical Event Log</h1>
                    </td>
                </tr>
                <tr align="center">
                    <td>
                        <table>
                            <tr>
                                <td>
                                    Date From:
                                    <input type="text" name="DateFrom" id="DateFrom">
                                </td>
                                <td>
                                    Date To:<input type="text" name="DateTo" id="DateTo">
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td align="center">
                        <table>
                            <tr>
                                <td>
                                    Error Type:
                                </td>
                                <td>
                                    <div style="height: 50px; width: 300px; overflow-y: scroll">
                                        <asp:CheckBoxList ID="ddlTypeTrace" runat="server" />
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td align="center">
                        <table>
                            <tr>
                                <td>
                                    Code Trace:
                                </td>
                                <td>
                                     <input type="text"  id="txtCode">
                                     <input type="checkbox" checked=checked  id="chkIsLike" > IsLike
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            <div style="text-align: center">
                <br />
                <button type="submit" id="LoadRecordsButton" onclick="return LoadRecordsButton_onclick()">
                    Load records</button>
            </div>
            </form>
        </div>
        <div id="EventLogTableContainer">
        </div>
    </div>
    <script type="text/javascript">

        $(document).ready(function () {
            $('#DateFrom').datepicker('setDate', '+0');
            $('#DateTo').datepicker('setDate', '+0');
            //Prepare jtable plugin
            $('#EventLogTableContainer').jtable({
                title: 'The Event Log List',
                paging: true,
                pageSize: 10,
                sorting: true,
                defaultSorting: 'Source ASC',
                actions: {
                    listAction: 'EventLog.aspx/EventLogListByFilter'
                },
                fields: {
                    Id: {
                        title: "Id Event Log",
                        key: true,
                        create: false,
                        edit: false,
                        list: false
                    },
                    FactTime: {
                        title: 'Record date',
                        width: '15%',
                        type: 'date',
                        display: function (data) {
                            return moment(data.record.FactTime).format('DD/MM/YYYY HH:mm:ss');
                        },
                        create: false,
                        edit: false
                    },
                    HostSource: {
                        title: 'Host Source',
                        width: '13%'
                    },
                    TypeTrace: {
                        title: 'Type Trace',
                        width: '13%',
                        options: { '0': 'Error', '1': 'Trace', '2': 'Warrning', '3': 'Other', '4': 'Audit' }
                    },
                    Source: {
                        title: 'Source',
                        width: '13%'
                    },
                    Code: {
                        title: 'Code',
                        width: '13%'
                    },
                     IsActive: {
                      list: false
                    },
                    EventLogDetaild: {
                        title: '',
                        width: '3%',
                        sorting: false,
                        edit: false,
                        create: false,
                        listClass: 'child-opener-image-column',
                        display: function (studentData) {
                            //Create an image that will be used to open child table
                            var $img;

                         var IsActive =   studentData.record.IsActive;

                         if(IsActive){
                             $img = $('<img class="child-opener-image" src="/Scripts/jtable/Content/images/view.png" title="Detail Event Log" />');
                             $img.disabled = IsActive;
                            //Open child table when user clicks the image
                            $img.click(function () {
                                $('#EventLogTableContainer').jtable('openChildTable',
                                    $img.closest('tr'),
                                    {
                                        title: ' Detaild Event Log.',
                                        actions: {
                                            listAction: 'EventLog.aspx/EventLogDetaild?EventLogId=' + studentData.record.Id
                                        },
                                        fields: {
                                            Detail: {
                                                title: 'Detail',
                                                width: '100%',
                                            }
                                        }
                                    }, function (data) { //opened handler
                                        data.childTable.jtable('load');
                                    });
                            });
                         }
                          else{
                             $img = $('<img class="child-opener-image" src="/Scripts/jtable/Content/images/view_disable.png" title="No Detail" />');
                         }
                         return $img;
                        }
                    }
                }
            });

            //Re-load records when user click 'load records' button.
            $('#LoadRecordsButton').click(function (e) {
                e.preventDefault();
                $('#EventLogTableContainer').jtable('load', {
                    DateFrom: $('#DateFrom').val(),
                    DateTo: $('#DateTo').val(),
                    typeTraceName: Validate(),
                    filter: $('#txtCode').val(),
                    IsLike:document.getElementById("chkIsLike").checked
                });
            });

            //Load all records when page is first shown
            $('#LoadRecordsButton').click();
        });
function LoadRecordsButton_onclick() {

}
    </script>
</body>
</html>