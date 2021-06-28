<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/fasi/FASI.master" CodeFile="mailBoxesAdmin.aspx.cs" Inherits="fasi_scheduler_MailBoxesAdmin" %>

<%@ MasterType TypeName="FASI" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="/fasi/assets/jstree/dist/themes/default/style.min.css" rel="stylesheet" />
    <link href="/fasi/assets/css/bootstrap-table.min.css" rel="stylesheet" />
    <link href="/fasi/assets/css/bootstrap-checkbox.css" rel="stylesheet" />
    <link href="/fasi/assets/css/nouislider.min.css" rel="stylesheet" />

    <link href="/fasi/scheduler/config/mailBoxesConfig.css?rel=1526481090736" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceholder1" runat="Server">
    <div class="col-lg-3 remove-padding">
        <div class="ibox float-e-margins">
            <div class="ibox-content mailbox-content">
                <div class="file-manager">
                    <ul class="treeViewTopMenu">
                        <li><a id="lnkCreate" href="javascript:mailBoxesAdminSupport.createNode();" data-toggle="tooltip-task"><i class="fa fa-plus"></i></a></li>
                        <li><a id="lnkUpdate" href="javascript:mailBoxesAdminSupport.renameNode();" data-toggle="tooltip-task"><i class="fa fa-pencil"></i></a></li>
                        <li><a id="lnkDelete" href="javascript:mailBoxesAdminSupport.deleteNode();" data-toggle="tooltip-task"><i class="fa fa-trash"></i></a></li>
                    </ul>
                    <div id="treeView"></div>
                    <div class="clearfix"></div>
                </div>
            </div>
        </div>
    </div>
    <div class="col-lg-9 remove-padding">
        <div class="tabs-container">
            <ul class="nav nav-tabs">
                <li class="active"><a data-toggle="tab" class="trn" href="#tab-1" data-trn-key="Columns"></a></li>
                <li class=""><a data-toggle="tab" href="#tab-2" class="trn" data-trn-key="Conditions"></a></li>
                <%--<li class=""><a data-toggle="tab" href="#tab-3" class="trn" data-trn-key="Semaphore"></a></li>--%>
            </ul>
            <div class="tab-content">
                <div id="tab-1" class="tab-pane active">
                    <div class="panel-body">
                        <h4><span class="trn" data-trn-key="DragAndDropColumnsTitle"></span><small class="trn" data-trn-key="DragAndDropColumnsDescription"></small></h4>
                        <div class="row" style="padding-top: 10px;">
                            <div class="col-lg-6">
                                <div class="ibox">
                                    <div class="ibox-content">
                                        <h3 class="trn" data-trn-key="Available"></h3>
                                        <ul class="sortable-list connectList agile-list" id="availableColumns"></ul>
                                    </div>
                                </div>
                            </div>
                            <div class="col-lg-6">
                                <div class="ibox">
                                    <div class="ibox-content">
                                        <h3 class="trn" data-trn-key="Selected"></h3>
                                        <ul class="sortable-list connectList agile-list" id="selectedColumns"></ul>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div id="tab-2" class="tab-pane">
                    <div class="panel-body">
                        <div id="itemBox" class="row" style="margin: 0">
                            <div class="ibox float-e-margins">
                                <div class="ibox-title">
                                    <h5><span id="title"></span>&nbsp;<small class="trn" data-trn-key="SelectedNodeConditions"></small></h5>
                                </div>
                                <div class="ibox-content">
                                    <div class="form-group">
                                        <form id="formCondition" class="form-horizontal" style="display: none;">
                                            <label class="col-sm-1 control-label"><span class="trn" data-trn-key="Field"></span>:</label>
                                            <div class="col-sm-2">
                                                <select id="cbxField" name="field" class="form-control" style="width: 100%;" onchange="mailBoxesAdminSupport.onChangeField(this);"></select>
                                            </div>
                                            <label class="col-sm-1 control-label"><span class="trn" data-trn-key="Operator"></span>:</label>
                                            <div class="col-sm-3">
                                                <select id="cbxOperator" name="operator" class="form-control" style="width: 100%;"></select>
                                            </div>
                                            <label class="col-sm-1 control-label"><span class="trn" data-trn-key="Value"></span>:</label>
                                            <div class="col-sm-3">
                                                <select class="form-control" name="value" id="cbxStatus" style="width: 100%; display: none;"></select>
                                                <select class="form-control" name="value" id="cbxPriority" style="width: 100%; display: none;"></select>
                                                <select class="form-control" name="value" id="cbxOriginType" style="width: 100%; display: none;"></select>
                                                <select class="form-control" name="value" id="cbxLOB" style="width: 100%; display: none;"></select>
                                            </div>
                                        </form>
                                        <div class="col-sm-1">
                                            <button id="addCondition" class="btn btn-primary" onclick="mailBoxesAdminSupport.createCondition(event);" data-toggle="tooltip-task"><i class="fa fa-plus"></i></button>
                                        </div>
                                    </div>
                                    <table id="grdFilter"></table>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
<%--                <div id="tab-3" class="tab-pane">
                    <div class="panel-body">
                        <div class="row" style="margin: 0">
                            <div class="ibox float-e-margins">
                                <div class="ibox-title">
                                    <h5 class="trn" data-trn-key="SemaphoreConfig"></h5>
                                </div>
                                <div class="ibox-content">
                                    <form id="formSemaphore" class="form-horizontal">
                                        <div class="form-group">
                                            <div class="col-sm-12 checkbox c-checkbox">
                                                <label id="enableSemaphore">
                                                    <input id="semaphoreActive" type="checkbox" onchange="mailBoxesAdminSupport.enableSemaphoreClick(this);" value="true" />
                                                    <span class="fa fa-check"></span>
                                                </label>
                                            </div>
                                        </div>
                                        <div class="hr-line-dashed"></div>
                                        <div class="form-group">
                                            <div class="col-sm-12">
                                                <div id="sliderSelection"></div>
                                            </div>
                                        </div>
                                    </form>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>--%>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footer" runat="Server">
    <script type="text/javascript" src="/fasi/assets/jstree/dist/jstree.min.js"></script>
    <script type="text/javascript" src="/fasi/assets/js/bootstrap-table.min.js"></script>
    <script type="text/javascript" src="/fasi/assets/js/bootstrap-table-es-CR.min.js"></script>
    <script type="text/javascript" src="/fasi/assets/js/bootstrap-table-en-US.min.js"></script>
    <script type="text/javascript" src="/fasi/assets/js/nouislider.min.js"></script>

    <script type="text/javascript" src="/fasi/scheduler/admin/mailBoxesAdmin.js?rel=1526481090736"></script>
</asp:Content>
