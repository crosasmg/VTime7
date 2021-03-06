<%@ Page Language="VB" AutoEventWireup="false" CodeFile="is.aspx.vb" Inherits="Support_is" %>

<!DOCTYPE html>
<html>
<head>
    <meta charset="UTF-8">
    <title></title>
    <link rel="stylesheet" href="../styles/bootstrap.min.css">
    <link rel="stylesheet" href="../styles/bootstrap-theme.min.css">
    <link rel="stylesheet" href="../Styles/jquery-ui-1.11.4.css">
    <link rel="stylesheet" href="../Styles/bootstrap-table.min.css">
    <link rel="stylesheet" href="../Styles/Prototy/font-awesome/css/font-awesome.css" />
    <link rel="stylesheet" href="../Styles/Prototy/style.css" />
    <link rel="stylesheet" href="../Styles/Prototy/jsTree/style.css" />
</head>
<body class="pace-done gray-bg">
    <div class="page-wrapper">
        <div class="wrapper wrapper-content">
            <div class="row">
                <div class="container" style="padding: 4px;">
                    <div class="form-horizontal" data-toggle="validator">
                        <fieldset>

                            <ul class="nav nav-tabs">
                                <li role="presentation" class="active">
                                    <a href="#DataFactory" id="headerDataFactory" aria-controls="Exclusions" role="tab" data-toggle="tab">Data Factory</a>
                                </li>
                                <li role="presentation">
                                    <a href="#FileManager" id="headerFileManager" aria-controls="Exclusions" role="tab" data-toggle="tab">File Manager</a>
                                </li>
                            </ul>
                            <div class="panel-body">

                                <!-- Tab panes -->
                                <div class="tab-content clearfix">
                                   <div role="tabpanel" class="tab-pane active" id="DataFactory">
                                        <div class="form-group">
                                            <label class="col-md-3 control-label" for="DecriptionRule">ConnectionString Enables</label>
                                            <div class="col-md-9">
                                                <select id="ddlConnectionEnable" class="form-control"></select>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-md-3 control-label" for="Explanation">Query</label>
                                            <div class="col-md-7">
                                                <textarea id="txtQureyText" class="form-control" rows="4">select count(*) from usermember</textarea>
                                            </div>
                                            <div class="col-md-2">
                                                <button type="button" id="btnDataFactoryRun" class="btn btn-primary">Consultar</button>
                                            </div>
                                        </div>
                                        <div class="col-md-12">
                                            <div class="table-responsive">
                                                <table class="table" id="dataTableDataFactoryResult" data-click-to-select="true">
                                                    <thead>
                                                        <tr>
                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                                    </tbody>
                                                </table>
                                            </div>
                                        </div>
                                        <div id="zoneMessageErrorDataFactory" class="col-md-12">
                                            <label class="col-md-12" style="color: #f00;" id="lblMessageErrorDataFactory">Error</label>
                                        </div>
                                    </div>
                                    <div class="tab-pane" id="FileManager">
                                        <div class="row">
                                            <div class="col-lg-3">
                                                <div class="file-manager">
                                                    <h5>Mostrar:</h5>
                                                    <div class="hr-line-dashed"></div>
                                                    <button class="btn btn-primary btn-block">Upload Files</button>
                                                    <div class="hr-line-dashed"></div>
                                                    <h5>Folders</h5>
                                                    <div id="jstree1">
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-lg-9">
                                                <div class="row">
                                                    <div id="ContainerFile" class="col-lg-12">
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </fieldset>
                    </div>
                </div>
                <div id="loading" title="In Process">
                    <p>Please wait ...</p>
                </div>
            </div>
        </div>
    </div>
    <script src="../Scripts/jquery.min.js"></script>
    <script src="../Scripts/bootstrap.min.js"></script>
    <script src="../Scripts/jquery.validate.min.js"></script>
    <script src="../Scripts/jquery.jqGrid-5.1.min.js"></script>
    <script src="../Scripts/jquery-ui.js"></script>
    <script src="../Scripts/bootstrap-table.min.js"></script>
    <script src="../Scripts/moment.min.js"></script>
    <script src="../Styles/Prototy/jsTree/jstree.min.js"></script>
    <script src="../Scripts/is.js"></script>
</body>
</html>
