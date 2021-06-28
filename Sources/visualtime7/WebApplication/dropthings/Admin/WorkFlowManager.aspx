<%@ Page Title="WorkFlowManager" Language="VB" MasterPageFile="~/DropthingsMasterPage.master"
    AutoEventWireup="false" CodeFile="WorkFlowManager.aspx.vb" Inherits="WorkFlowManager"
    meta:resourcekey="PageResource1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <%-- <link href="../../Styles/bootstrap.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/bootstrap.js"></script>--%>

    <link href="../../Styles/Prototy/font-awesome/css/font-awesome.css" rel="stylesheet" />

    <script type="text/javascript" src="../../Scripts/moment.min.js"></script>
    <script type="text/javascript" src="//cdn.jsdelivr.net/bootstrap.daterangepicker/2/daterangepicker.js"></script>
    <link rel="stylesheet" type="text/css" href="//cdn.jsdelivr.net/bootstrap.daterangepicker/2/daterangepicker.css" />

    <link rel="stylesheet" href="../../Styles/bootstrap-table.min.css" />
    <script type="text/javascript" src="../../Scripts/bootstrap-table.min.js"></script>
    <script type="text/javascript" src="../../Scripts/WorkFlowManager.js"></script>
    <style>
        #myModal:before {
            height: 0px !important;
        }

        .modal-dialog {
            width: 1000px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceholder1" runat="Server">
    <div class="container" style="margin-top: 10px;">
        <div class="form-group col-xs-3 col-md-3">
            <div class="d-flex flex-column">
                <label for="dpDateStart" class="control-label">Fecha Inicio</label>
                <input id="dpDateStart" name="dpDateStart" type="text" placeholder="Nombre" class="form-control" />
            </div>
        </div>
        <div class="form-group col-xs-3 col-md-3">
            <div class="d-flex flex-column">
                <label for="dpDateEnd" class="control-label">Fecha Fin</label>
                <input id="dpDateEnd" name="dpDateEnd" type="text" placeholder="Nombre" class="form-control" />
            </div>
        </div>
        <div class="form-group col-xs-3 col-md-3">
            <div class="d-flex flex-column">
                <label for="txtFilter" class="control-label">Filtro</label>
                <input id="txtFilter" name="txtFilter" type="text" placeholder="Filtro" class="form-control" />
            </div>
        </div>
        <div class="form-group col-xs-3 col-md-3 text-center">
            <div class="btn-group">
                <br />
                <input type="button" id="btnQuery" name="btnQuery" class="btn btn-primary" value="Consultar" />
            </div>
        </div>
        <div class="form-group">
            <table class="table table-fit" id="Files" data-click-to-select="true">
                <thead>
                    <tr>
                        <th data-field="WorkflowinstanceId" data-visible="false">WorkflowinstanceId</th>
                        <th data-field="TimeCreated" class="col-sm-2" data-align="left">Creado</th>
                        <th data-field="Identify" data-formatter="FormaterIdentify" data-align="left">Identificación</th>
                        <th data-field="WorkflowState" data-formatter="FormaterWorkflowState" data-align="center">Estado</th>
                        <th data-field="Duration" data-align="right">Duración</th>
                        <th data-field="Reason" data-align="center" data-formatter="FormatterReason">Detalle</th>                        
                    </tr>
                </thead>
                <tbody style="width: 400px">
                </tbody>
            </table>
        </div>

        <!-- Modal -->
        <div class="modal fade" id="myModal" role="dialog">
            <div class="modal-dialog">

                <!-- Modal content-->
                <div id="modalContent" class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                        <h4 class="modal-title"><i class="fa fa-history" aria-hidden="true">  Detalle del workflow</i></h4>
                    </div>
                    <div class="modal-body">
                        <iframe name="ifWindows" id="ifWindows" src="" width="99.6%" height="500" frameborder="0"></iframe>
                    </div>
                    <div class="modal-footer">
                        <button name="btnRefresh" id="btnRefresh" class="btn btn-default"><i class="fa fa-refresh" aria-hidden="true"></i></button>
                        <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>