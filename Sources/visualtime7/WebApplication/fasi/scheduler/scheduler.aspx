<%@ Page Language="C#" MasterPageFile="~/fasi/FASI.master" AutoEventWireup="true" CodeFile="scheduler.aspx.cs" Inherits="fasi_scheduler_scheduler" %>

<%@ MasterType TypeName="FASI" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="/fasi/assets/jstree/dist/themes/default/style.min.css" rel="stylesheet" />
    <link href="/fasi/assets/css/bootstrap-table.min.css" rel="stylesheet" />
    <link href="/fasi/assets/css/bootstrap-datetimepicker.min.css" rel="stylesheet" />
    <link href="/fasi/assets/css/bootstrap-checkbox.css" rel="stylesheet" />

    <link href="/fasi/scheduler/schedule.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceholder1" runat="Server">
    <div class="col-lg-3 remove-padding">
        <div class="ibox float-e-margins">
            <div class="ibox-content mailbox-content">
                <div class="file-manager">
                    <h5><span class="trn" data-trn-key="TaskMailboxes">Bandejas de tareas</span>&nbsp;<a id="mailBoxConfig" href="/fasi/scheduler/config/mailBoxesConfig.aspx" data-toggle="tooltip-task"><i class="fa fa-cog"></i></a></h5>
                    <div id="treeView"></div>
                    <div class="clearfix"></div>
                </div>
            </div>
        </div>
    </div>
    <div class="col-lg-9 remove-padding">
        <div class="mail-box">
            <div id="toolbar">
                <div class="form-inline" role="form">
                    <a href="#" class="btn btn-white btn-sm trn" data-toggle="modal" data-target="#taskModal" onclick="taskSupport.clearAll()" data-trn-key="NewTask"></a>
                    <a id="btnAssignMultiple" href="#" class="btn btn-white btn-sm trn" data-toggle="modal" data-target="#assignedToModal" onclick="taskSupport.clearAllAssingModal()" data-trn-key="AssignSelectedTasks" style="display: none;"></a>
                    <a href="#" class="btn btn-danger btn-sm trn" data-toggle="modal" data-target="#inactivateUserModal" data-trn-key="InactivateUser" onclick="inactivateUserSupport.clearAll();"></a>
                </div>
            </div>
            <table id="grdTable" data-toggle="table"></table>
        </div>
    </div>
    <div class="modal fade" id="taskModal" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog modal-lg" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title trn" id="exampleModalLabel" data-trn-key="TaskTitle"></h4>
                </div>
                <div class="modal-body">
                    <div class="container-fluid">
                        <form id="schedulerForm" class="form-horizontal">
                            <input type="hidden" id="taskId" name="taskId" />
                            <div class="row">
                                <div class="form-group">
                                    <label for="txtTitle" class="col-sm-2 control-label"><span class="trn" data-trn-key="Subject"></span>:</label>
                                    <div class="col-sm-10">
                                        <input type="text" class="form-control" id="taskShortDescription" name="taskShortDescription" maxlength="50" />
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label for="txtUbication" class="col-sm-2 control-label"><span class="trn" data-trn-key="Location"></span>:</label>
                                    <div class="col-sm-10">
                                        <input type="text" class="form-control" id="location" name="location" maxlength="50"/>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label for="txtStartHours" class="col-sm-2 control-label"><span class="trn" data-trn-key="StartingTime"></span>:</label>
                                    <div class="col-sm-4">
                                        <input type="text" class="form-control" id="startingDatetime" name="startingDatetime" autocomplete="off" />
                                    </div>
                                    <label for="txtEndHours" class="col-sm-2 control-label"><span class="trn" data-trn-key="EndingTime"></span>:</label>
                                    <div class="col-sm-4">
                                        <input type="text" class="form-control" id="endingDatetime" name="endingDatetime" autocomplete="off" />
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label for="cbxPriority" class="col-sm-2 control-label"><span id="lblPriority" class="trn" data-trn-key="Priority" data-trn-title="PriorityTitle"></span>:</label>
                                    <div class="col-sm-4">
                                        <select class="form-control" name="priority" id="priority"></select>
                                    </div>
                                    <label for="cbxState" class="col-sm-2 control-label"><span id="lblTaskStatus" class="trn" data-trn-key="TaskStatus"></span>:</label>
                                    <div class="col-sm-4">
                                        <select class="form-control" name="status" id="status">
                                        </select>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label for="cbxWho" class="col-sm-2 control-label"><span id="lblAssignedTo" class="trn" data-trn-key="AssignedTo"></span>:</label>
                                    <div class="col-sm-4">
                                        <select class="form-control" name="owners" id="owners"></select>
                                    </div>
                                    <label for="txtProgress" class="col-sm-2 control-label">%&nbsp;<span id="lblCompleted" class="trn" data-trn-key="Completed"></span>:</label>
                                    <div class="col-sm-4">
                                        <input type="text" class="form-control" id="percentageCompleted" name="percentageCompleted" data-mask="990" data-mask-reverse="true" value="0" style="text-align: right;" />
                                    </div>
                                </div>
                                <div class="form-group">
	                                <label for="cbxLineOfBusiness" class="col-sm-2 control-label"><span id="lblLineOfBusiness" class="trn" data-trn-key="LineOfBusiness" data-trn-title="LineOfBusinessTitle"></span>:</label>
	                                <div class="col-sm-4">
		                                <select class="form-control" name="LineOfBusiness" id="LineOfBusiness"></select>
	                                </div>
                                </div>
                                <div class="form-group">
                                    <div class="col-sm-2 checkbox c-checkbox">
                                        <label id="reminder">
                                            <input type="checkbox" id="alarmActive" name="alarmActive" onclick="taskSupport.reminderCheckChanged(this);" value="true" />
                                            <span class="fa fa-check"></span>
                                        </label>
                                    </div>
                                    <div class="col-sm-4">
                                        <select class="form-control" name="alarmDatetime" id="alarmDatetime" disabled="disabled">
                                        </select>
                                    </div>
                                    <div class="col-sm-6 checkbox c-checkbox">
                                        <label id="individualIndicator">
                                            <input type="checkbox" id="individualTaskIndicator" name="individualTaskIndicator" value="true" />
                                            <span class="fa fa-check"></span>
                                        </label>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="col-sm-6 checkbox c-checkbox">
                                        <label id="warningCompleted">
                                            <input type="checkbox" id="warningWhenCompleted" name="warningWhenCompleted" value="true" />
                                            <span class="fa fa-check"></span>
                                        </label>
                                    </div>
                                    <div class="col-sm-6 checkbox c-checkbox">
                                        <label id="allDay">
                                            <input type="checkbox" id="allDayActivity" name="allDayActivity" value="true" />
                                            <span class="fa fa-check"></span>
                                        </label>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label for="cbxWho" class="col-sm-2 control-label"><span id="lblTransaction" class="trn" data-trn-key="Transaction"></span>:</label>
                                    <div class="col-sm-4">
                                        <select class="form-control" name="Transaction" id="Transaction"></select>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="col-sm-12">
                                        <textarea class="form-control" rows="5" id="taskLongDescription" name="taskLongDescription"></textarea>
                                    </div>
                                </div>
                            </div>
                        </form>
                    </div>
                </div>
                <div class="modal-footer">
                    <button id="btnSave" type="button" class="btn btn-primary" style="display: none;"><i class="fa fa-save"></i>&nbsp;<span class="trn" ></span></button>
                    <button id="btnCancel" type="button" class="btn btn-secondary trn" data-dismiss="modal" data-trn-key="Cancel"></button>
                    <button id="btnDelete" type="button" class="btn btn-danger" style="display: none;"><i class="fa fa-trash-o"></i>&nbsp;<span class="trn" data-trn-key="Delete"></span></button>
                </div>
            </div>
        </div>
    </div>

    <div class="modal fade" id="modalHistory" tabindex="-1" role="dialog" aria-labelledby="modalHistoryLabel" aria-hidden="true">
        <div class="modal-dialog modal-lg" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title" id="modalHistoryLabel"></h4>
                </div>
                <div class="modal-body">
                    <table id="grdHistory"
                        data-toggle="table"
                        data-detail-view="true"
                        data-detail-formatter="taskHistorySupport.detailFormatter">
                    </table>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default trn" data-dismiss="modal" data-trn-key="Close"></button>
                </div>
            </div>
        </div>
    </div>

    <div class="modal fade" id="assignedToModal" tabindex="-1" role="dialog" aria-labelledby="assignedToModalLabel" aria-hidden="true">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title trn" id="assignedToModalLabel" data-trn-key="AssignSelectedTasksTo"></h4>
                </div>
                <div class="modal-body">
                    <div class="container-fluid">
                        <form id="assinedsForm" class="form-horizontal">
                            <div class="form-group">
                                <label for="cbxWho" class="col-sm-3 control-label"><span class="trn" data-trn-key="AssignedTo"></span>:</label>
                                <div class="col-sm-9">
                                    <select class="form-control" name="ownersMassive" id="ownersMassive" multiple></select>
                                </div>
                            </div>
                        </form>
                    </div>
                </div>
                <div class="modal-footer">
                    <button id="btnSaveAssignMultiple" type="button" class="btn btn-primary"><i class="fa fa-save"></i>&nbsp;<span class="trn"></span></button>
                    <button id="btnCancelAssined" type="button" class="btn btn-secondary trn" data-dismiss="modal" data-trn-key="Cancel"></button>
                </div>
            </div>
        </div>
    </div>

    <div class="modal fade" id="inactivateUserModal" tabindex="-1" role="dialog" aria-labelledby="inactivateUserModalLabel" aria-hidden="true">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title trn" id="inactivateUserModalLabel" data-trn-key="InactivateUser"></h4>
                </div>
                <div class="modal-body">
                    <div class="container-fluid">
                        <div class="form-group">
                            <form id="inactivateUserForm" class="form-horizontal">                             
                                <label class="col-sm-1 control-label"><span class="trn" data-trn-key="From"></span>:</label>
                                <div class="col-sm-4">
                                    <input type="text" class="form-control" id="startingInactivate" name="startingInactivate" autocomplete="off" />
                                </div>
                                <label class="col-sm-2 control-label"><span class="trn" data-trn-key="To"></span>:</label>
                                <div class="col-sm-4">
                                    <input type="text" class="form-control" id="endingInactivate" name="endingInactivate" autocomplete="off" />
                                </div>                                
                            </form>
                            <div class="col-sm-1" style="padding-left: 9px;">
                                <button id="btnSaveInactivate" class="btn btn-primary" data-trn-key="SavePage"><i class="fa fa-plus" ></i></button>
                            </div>
                        </div>
                        <table id="grdInactivate" data-toggle="table"></table>
                    </div>
                </div>
                <div class="modal-footer">                    
                    <button type="button" class="btn btn-secondary trn" data-dismiss="modal" data-trn-key="Close"></button>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footer" runat="Server">
    <script type="text/javascript" src="/fasi/assets/jstree/dist/jstree.min.js"></script>
    <script type="text/javascript" src="/fasi/assets/js/bootstrap-table.min.js"></script>
    <script type="text/javascript" src="/fasi/assets/js/context.js"></script>
    <script type="text/javascript" src="/fasi/assets/js/bootstrap-table-es-CR.min.js"></script>
    <script type="text/javascript" src="/fasi/assets/js/bootstrap-table-en-US.min.js"></script>
    <script type="text/javascript" src="/fasi/assets/js/bootstrap-datetimepicker.min.js"></script>
    <script type="text/javascript" src="/fasi/assets/js/jquery.mask.min.js"></script>

    <script type='text/javascript' src='/fasi/app/js/general.js?rel=20180607035508421'></script>
    <script type="text/javascript" src="/fasi/scheduler/scheduler.js"></script>    
    <script type="text/javascript" src="/fasi/scheduler/task.js"></script>
    <script type="text/javascript" src="/fasi/scheduler/taskHistory.js"></script>    
    <script type="text/javascript" src="/fasi/scheduler/inactivateUser.js"></script>
</asp:Content>
