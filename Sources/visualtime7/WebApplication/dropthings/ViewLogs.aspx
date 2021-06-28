<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ViewLogs.aspx.vb" Inherits="Support_ViewLogs" %>

<!DOCTYPE html>
<html>
<head>
    <meta charset="UTF-8">
    <title>View Logs</title>

    <!-- Latest compiled and minified CSS -->
    <link rel="stylesheet" href="../Styles/jquery-ui-1.11.4.css">
    <link rel="stylesheet" href="../Styles/bootstrap-table.min.css">

    <link href="../Styles/Prototy/font-awesome/css/font-awesome.css" rel="stylesheet" />
    <link href="../Styles/Prototy/style.css" rel="stylesheet" />
</head>
<body class="pace-done gray-bg">
    <div id="PageContent" class="page-wrapper wrapper text-center">
        <div id="WrappperContent" class="wrapper wrapper-content gray-bg">
            <div class="row">
                <div class="col-lg-12">
                    <div class="panel blank-panel">
                        <div class="panel-heading">
                            <div class="panel-title m-b-md">
                                <h4>View Logs</h4>
                            </div>
                        </div>
                        <div class="panel-body">
                            <table class="table" id="Files" data-click-to-select="true">
                                <thead>
                                    <tr>
                                        <th data-field="Path" data-align="center" data-formatter="LinkFormatter"><i class="fa fa-download"></i></th>
                                        <th data-field="Fecha">Fecha</th>
                                        <th data-field="Name">Nombre</th>
                                    </tr>
                                </thead>
                                <tbody style="width: 400px">
                                </tbody>
                            </table>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div id="loading" title="In Process">
            <p>Please wait ...</p>
        </div>
    </div>
    <%--<script src="https://ajax.googleapis.com/ajax/libs/jquery/1.11.3/jquery.min.js"></script>--%>
    <script src="../Scripts/jquery.min.js"></script>
    <script src="../Scripts/jquery-ui.js"></script>
    <script src="../Scripts/jquery.jqGrid-5.1.min.js"></script>
    <script src="../Scripts/bootstrap-table.min.js"></script>
    <script src="../Scripts/moment.min.js"></script>
    <script src="../Scripts/ViewLogs.js"></script>
</body>
</html>