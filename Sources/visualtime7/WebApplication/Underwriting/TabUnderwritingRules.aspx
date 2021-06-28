<%@ Page Language="VB" AutoEventWireup="false" CodeFile="TabUnderwritingRules.aspx.vb"
    Inherits="Underwriting_TabUnderwritingRules" UICulture="auto"
    Culture="auto" 
    Title="TabUnderwritingRules" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
        <link href="/Styles/fasi.css?v=<%=System.IO.File.GetLastWriteTime(Server.MapPath("~") + "\Styles\fasi.css").ToString("yyyyMMddHHmmss")%>" rel="stylesheet" />
        <link href="/Styles/ui.jqgrid-bootstrap.css" rel="stylesheet" />
        <link href="/Styles/jquery-ui.min-1.11.4.css" rel="stylesheet" />   
        <script src="/Scripts/jquery.min.js"></script>
        <script src="/Scripts/jquery-migrate-1.4.1.min.js"></script>
        <script src="/Scripts/bootstrap.min.js"></script>
        <script src="/Scripts/jquery-ui.js"></script>
        <script src="/Scripts/jquery.numeric.min.js"></script>
        <script src="/Scripts/jquery.validate.min.js"></script>
        <script src="/Scripts/jquery.validate.messages-es.js"></script>
        <script src="/Scripts/additional-methods.min.js"></script>
        <script src="/Scripts/moment.min.js"></script>
        <!--JQuery Toast-->
        <script src="/Scripts/jquery.toast.js"></script>
        <script src="/Scripts/fasi.js?v=<%=System.IO.File.GetLastWriteTime(Server.MapPath("~") + "\Scripts\fasi.js").ToString("yyyyMMddHHmmss")%>"></script>   
        <script src='/fasi/assets/js/moment-with-locales.min.js'></script>
    <script src='/fasi/assets/js/loadingoverlay.min.js'></script>
    <script src="/fasi/assets/js/select2.min.js"></script>
    <script src="/fasi/assets/js/select2-es.min.js" charset="UTF-8"></script>
    <script src="/fasi/assets/js/jquery.periodic.min.js?rel=1526481090696"></script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceholder1" runat="Server">


    <link href="Styles/TabUnderwritingRules.css?v=<%=System.IO.File.GetLastWriteTime(Server.MapPath("~") + "\Underwriting\Styles\TabUnderwritingRules.css").ToString("yyyyMMddHHmmss")%>" rel="stylesheet" />
    <link href="../Styles/jquery-ui.css" rel="stylesheet" />
                    <script src="/Scripts/moment.min.js"></script> 
    <%-- ####################################################################### BOTONERA PRINCIPAL--%>
    <div style="border: solid; border-width: 1px; border-color: #A8A8A8; /*background-color: #F0F0F0; */">
        <div id="UWRules_ButtonSet">
            <div id="AddRule" class="btn disabled">
                <img src="../images/16x16/Toolbar/add.png" />
                <span><%=GetLocalResourceObject("btnUWAddRule")%></span>
            </div>
            <div id="DeleRule" class="btn disabled">
                <img src="../images/16x16/Toolbar/delete.png" />
                <span><%=GetLocalResourceObject("btnUWDeleRule")%></span>
            </div>
            <div id="EditRule" class="btn disabled">
                <img src="/Underwriting/Images/editCase.png" />
                <span><%=GetLocalResourceObject("btnUWEditRule")%></span>
            </div>
            <div id="CancelRule" class="btn disabled">
                <img src="/Underwriting/Images/cancelEdit.png" />
                <span><%=GetLocalResourceObject("btnUWCancelRule")%></span>
            </div>
            <div id="SaveRule" class="btn disabled">
                <img src="/Underwriting/Images/save.png" />
                <span><%=GetLocalResourceObject("btnUWSaveRule")%></span>
            </div>
            |<!-- TODO: caracter separador. Cambiar. -->

            <%--BOTON DE LENGUAJE--%>
            <div id="btnLanguage" class="btn-group">
                <div class="btn dropdown" id="menu1" type="button" data-toggle="dropdown" data-value="<% Response.Write(Session("LanguageID")) %>"">
                    <img src="/images/16x16/Flags/<% Response.Write(Session("LanguageID")) %>.png" />
                    <% If Session("LanguageID") = 1 %>
                        <span><%=GetLocalResourceObject("btnLanguage")%><%=GetLocalResourceObject("lang-en")%></span>
                    <% Else %>
                        <span><%=GetLocalResourceObject("btnLanguage")%><%=GetLocalResourceObject("lang-es")%></span>
                    <% End If %>
                    <span class="caret"></span>
                </div>
                <ul class="dropdown-menu" role="menu" aria-labelledby="menu1">
                    <li><a href="#" onclick="changeLanguage(this)" data-value="1">
                        <img src="/images/16x16/Flags/1.png" /><span><%=GetLocalResourceObject("lang-en")%></span></a></li>
                    <li><a href="#" onclick="changeLanguage(this)" data-value="2">
                        <img src="/images/16x16/Flags/2.png" /><span><%=GetLocalResourceObject("lang-es")%></span></a></li>
                </ul>
            </div>
        </div>
    </div>

    <div class="col-md-12 FormError bg-danger divError" style="display: none;"></div>    
    <div class="col-md-12 FormError bg-success divSuccess" style="display: none;"></div>

    <%-- ####################################################################### CONTROLES REGLAS DE SUSCRIPCION--%>
    <div id="form-uwrule" class="FormData">
        <%-- ####################################################################### DATOS REGLAS DE SUSCRIPCION--%>
        <div class="form-group">
            <div class="row" style="padding-top: 5px">
                <div class="col-md-4">
                    <label class="control-label" for="ddlUwRule" id="editRuleLabel"><%=GetLocalResourceObject("ddlUwRule")%></label>
                    <label class="control-label" for="ddlUwRuleDescription" id="newRuleLabel" style="display:none"><%=GetLocalResourceObject("ddlUwRuleDescription")%><span style="color:red">*</span>
                    </label>
                    <table style="width: 100%">
                        <tr>
                            <td style="width: 95%">
                                <input type="hidden" id="ddlUwRuleId" class="form-control" />
                                <div id="editRule">
                                    <input type="text" id="ddlUwRule" class="form-control" title="<% Response.Write(GetLocalResourceObject("ttipddlUwRule"))%>" />                                    
                                </div>
                                <div id="newRuleTextInput" style="display:none">
                                    <input type="text" id="ddlUwRuleDescription" required="required" class="form-control" title="<% Response.Write(GetLocalResourceObject("ttipddlUwRuleDescription"))%>" />
                                </div>
                            </td>
                            <td style="width: 5%">
                                <div id="seekddlUwRule" class="btn">
                                    <span class="fa fa-search" aria-hidden="true"></span>
                                </div>
                            </td>
                        </tr>
                    </table>
                    <div>
                        <div id="ddlUwRule_progressbar" style="height: 5px" hidden=""></div>
                    </div>
                </div>
                <div class="col-md-4">
                    <label class="control-label" for="txtEfecDate"><%=GetLocalResourceObject("txtEfecDate")%></label>
                    <input type="text" id="txtEfecDate" class="form-control" disabled="disabled" title="<%=GetLocalResourceObject("ttiptxtEfecDate")%>" />
                </div>
                <div class="col-md-4">
                    <label class="control-label" for="txtEnfermedad"><%=GetLocalResourceObject("txtEnfermedad")%></label>
                    <div>
                        <div>    
                            <input type="text" id="txtEnfermedad" name="txtEnfermedad" class="form-control" disabled="disabled" title="<%=GetLocalResourceObject("ttiptxtEnfermedad")%>" />
                        </div>
                    </div>
               </div>
            </div>
            <div class="row">
                <div class="col-md-4">
                    <label class="control-label" for="txtExplanation"><%=GetLocalResourceObject("txtExplanation")%></label>
                    <textarea id="txtExplanation" class="form-control" disabled="disabled" required="required" rows="4" title="<%=GetLocalResourceObject("ttiptxtExplanation")%>"></textarea>
                </div>
                <div class="col-md-4">
                    <div>
                        <label class="control-label" for="ddlUwStatus"><%=GetLocalResourceObject("ddlUwStatus")%></label>
                        <select id="ddlUwStatus" class="form-control" disabled="disabled" required="required" title="<%=GetLocalResourceObject("ttipddlUwStatus")%>">
                            <option />
                        </select>
                    </div>
                    <div style="padding-top: 5px">
                        <label class="control-label" for="ddlUwArea"><%=GetLocalResourceObject("ddlUwArea")%></label>
                        <select id="ddlUwArea" class="form-control" disabled="disabled" title="<%=GetLocalResourceObject("ttipddlUwArea")%>">
                            <option />
                        </select>
                        <input type="hidden" name="UnderRuleIDByRequest" id ="UnderRuleIDByRequest" value="<%=Me.UnderwritingRuleId%>"/>
                       <input type="hidden" name="EffectDateByRequest" id ="EffectDateByRequest" value="<%=Me.EffectDate%>"/>
                     </div>
                </div>
                <div class="col-md-4">
                    <div>
                        <label class="control-label" for="ddlNivelEnfermedad"><%=GetLocalResourceObject("ddlNivelEnfermedad")%></label>
                        <select id="ddlNivelEnfermedad" class="form-control" disabled="disabled"title="<%=GetLocalResourceObject("ttipddlNivelEnfermedad")%>">
                            <option />
                        </select>
                    </div>
                    <div style="padding-top: 5px">
                        <label class="control-label" for="txtPoints"><%=GetLocalResourceObject("txtPoints")%></label>
                        <table>
                            <tr>
                                <td style="width: 10%;">
                                    <input type="text" id="txtPoints" class="form-control" disabled="disabled" width="100%" title="<%=GetLocalResourceObject("ttiptxtPoints")%>" /></td>
                                <td style="width: 90%; padding-left: 10px;">
                                    <input type="range" id="slider" name="slider" min="-99" max="99" class="form-control" disabled="disabled" title="<%=GetLocalResourceObject("ttiptxtPoints")%>" />
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </div>
            <div class="row" style="padding-top: 5px">
                <div class="col-md-4">
                    <div>
                        <label class="control-label" for="ddlTypeCase"><%=GetLocalResourceObject("ddlTypeCase")%></label>
                        <select id="ddlCaseType" class="form-control" disabled="disabled"title="<%=GetLocalResourceObject("ttipddlCaseType")%>">
                            <option />
                        </select>
                    </div>
                </div>
                <div class="col-md-4">
                    <div>
                        <label class="control-label" for="ddlBranch"><%=GetLocalResourceObject("ddlBranch")%></label>
                        <select id="ddlBranch" class="form-control" disabled="disabled"title="<%=GetLocalResourceObject("ttipddlBranch")%>">
                            <option />
                        </select>
                    </div>
                </div>
            </div>
        </div>

        <%-- ####################################################################### DATOS DE REQUERIMIENTOS--%>
        <div id="tabsGeneralRequirement">
            <ul class="nav nav-tabs" role="tablist">
                <li role="presentation" class="active"><a id="#RequirementTab" href="#" aria-controls="RequirementTab" role="tabX" data-toggle="tab" title='<%=GetLocalResourceObject("grpUWRequirements")%>'><i class="fa fa-file-text-o fa-lg" aria-hidden="true" style="padding-right: 3px"></i><%=GetLocalResourceObject("grpUWRequirements")%></a></li>
            </ul>
        </div>
        <div class="form-group">
            <div class="row">
                <div class="col-md-4">
                    <label class="control-label" for="ddlReqType"><%=GetLocalResourceObject("ddlReqType")%></label>
                    <select id="ddlReqType" class="form-control" disabled="disabled"title="<%=GetLocalResourceObject("ttipddlReqType")%>">
                        <option />
                    </select>
                </div>
                <div class="col-md-4">
                    <label class="control-label" for="ddlReqQuestion"><%=GetLocalResourceObject("ddlReqQuestion")%></label>
                    <select id="ddlReqQuestion" class="form-control" disabled="disabled"title="<%=GetLocalResourceObject("ttipddlReqQuestion")%>">
                        <option />
                    </select>
                </div>
                <div class="col-md-4">
                    <label class="control-label" for="ddlReqStatus"><%=GetLocalResourceObject("ddlReqStatus")%></label>
                    <select id="ddlReqStatus" class="form-control" disabled="disabled"title="<%=GetLocalResourceObject("ttipddlReqStatus")%>"></select>
                </div>
            </div>
        </div>
    </div>

    <%-- ####################################################################### ALARMAS--%>
    <div id="tabsGeneralAlarms">
        <ul class="nav nav-tabs" role="tablist">
            <li role="presentation" class="active"><a id="#AlarmsTab" href="#" aria-controls="AlarmsTab" role="tabX" data-toggle="tab" title='<%=GetLocalResourceObject("grpUWAlarmsTitle")%>'><i class="fa fa-bell fa-lg" aria-hidden="true" style="padding-right: 3px"></i><%=GetLocalResourceObject("grpUWAlarms")%></a></li>
        </ul>
    </div>
    <div>
        <table id="tabAlarmList" style="width: 100%;">
        </table>
        <div id="pager-tabAlarmList"></div>
        <div id="messageInformation"></div>
    <div id="tabsGeneral">
        <ul class="nav nav-tabs" role="tablist">
            <li id="LiExclusionsTab" style="display:none" role="presentation" class="active"><a id="#ExclusionsTab" href="#ExclusionsTab" aria-controls="ExclusionsTab" role="tabX" data-toggle="tab" title='<%=GetLocalResourceObject("ExclusionsTab")%>'><i class="fa fa-ban fa-lg" aria-hidden="true" style="padding-right: 3px"></i><%=GetLocalResourceObject("ExclusionsTab")%></a></li>
            <li id="LiSurchargeDiscountTab" style="display:none" role="presentation"><a href="#SurchargeDiscountTab" aria-controls="SurchargeDiscountTab" role="tabX" data-toggle="tab" title='<%=GetLocalResourceObject("SurchargeDiscountTab")%>'><i class="fa fa-exchange fa-lg" aria-hidden="true" style="padding-right: 3px"></i><%=GetLocalResourceObject("SurchargeDiscountTab")%></a></li>
            <li id="LiMaxInsuredSumTab" style="display:none" role="presentation"><a href="#MaxInsuredSumTab" aria-controls="MaxInsuredSumTab" role="tabX" data-toggle="tab" title='<%=GetLocalResourceObject("MaxInsuredSumTab")%>'><i class="fa fa-money fa-lg" aria-hidden="true" style="padding-right: 3px"></i><%=GetLocalResourceObject("MaxInsuredSumTab")%></a></li>
        </ul>
        <div class="tab-content master" id="tabContent">
            <div role="tabpanel" class="tab-pane active" id="ExclusionsTab">
                <div>
                    <table id="tblExclusion" style="width: 100%;">
                    </table>
                    <div id="pager-tblExclusion"></div>
                </div>
            </div>
            <div role="tabpanel" class="tab-pane" id="SurchargeDiscountTab">
                <table id="tblDiscoexprem" style="width: 100%;">
                </table>
                <div id="pager-tblDiscoexprem"></div>
            </div>
            <div role="tabpanel" class="tab-pane" id="MaxInsuredSumTab">
                <table id="tblMaxInsuredSum" style="width: 100%;">
                </table>
                <div id="pager-tblMaxInsuredSum"></div>
            </div>
        </div>
    </div>

    <%-- ####################################################################### ModalRule --%>
    <div id="ModalRules" class="modal fade" role="dialog" data-toggle="modal" data-backdrop="static">
        <div id="ModalRulesDialog" class="modal-dialog modal-lg" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="modal-title"><%=GetLocalResourceObject("modal-rules-title")%></h4>
                </div>
                <div id="modal-uwrules-content" class="modal-body">
                    <table id="gvwUWRules"></table>
                    <div id="pager-Rules"></div>
                </div>
                <div class="modal-footer">
                    <div class="col-sm-12 text-right">
                        <button id="btn-cancelar-regla" type="button" data-dismiss="modal" class="btn btn-default" title="<%=GetGlobalResourceObject("Resource", "CancelButtonResource")%>">
                            <i class="glyphicon glyphicon-remove-circle"></i><%=GetGlobalResourceObject("Resource", "CancelButtonResource")%>
                        </button>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <%-- ####################################################################### Confirm pop-ups --%>
    <div id="dialog-confirm-save" title='<%=GetLocalResourceObject("title-dialog-confirm-save")%>' hidden="hidden">
        <p><span class="ui-icon ui-icon-alert" style="float: left; margin: 1px 12px 20px 0;"></span><%=GetLocalResourceObject("dialog-confirm-save")%></p>
    </div>
    <div id="dialog-confirm-delete" title='<%=GetLocalResourceObject("title-dialog-confirm-delete")%>' hidden="hidden">
        <p><span class="ui-icon ui-icon-alert" style="float: left; margin: 1px 12px 20px 0;"></span><%=GetLocalResourceObject("dialog-confirm-delete")%></p>
    </div>
    <div id="dialog-message" title='<%=GetLocalResourceObject("title-dialog-message")%>' hidden="hidden">
        <p><span class="ui-icon ui-icon-alert" style="float: left; margin: 1px 12px 20px 0;"></span><%=GetLocalResourceObject("dialog-message")%></p>
    </div>
    <div id="dialog-alert" title='<%=GetLocalResourceObject("title-dialog-message")%>' hidden="hidden">
        <p><span class="ui-icon ui-icon-alert" style="float: left; margin: 1px 12px 20px 0;"></span><%=GetLocalResourceObject("dialog-message")%></p>
    </div>

    <div class="modal fade" id="confirm_accion" tabindex="0" role="dialog" aria-labelledby="gridSystemModalLabel">
    <div class="modal-dialog">
        <div class="modal-content">
            <div class="modal-header">
                <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                <h4 class="modal-title" id="gridSystemModalLabel"><% Response.Write(GetLocalResourceObject("confirmChangeBranchTitle"))%></h4>
            </div>
            <div class="modal-body">
                <% Response.Write(GetLocalResourceObject("confirmChangeBranch"))%>
            </div>
            <div class="modal-footer">
                <button id="btn-aceptar" type="button" onclick="javascript: CleanListAlarm('confirm_accion');LoadUnderwritingRuleAlarms(0);" class="btn btn-default" title="<% Response.Write(GetGlobalResourceObject("Resource", "AcceptBtnResource"))%>">
					<i class="fa fa-pencil-square-o fa-lg"></i> <% Response.Write(GetGlobalResourceObject("Resource", "AcceptBtnResource"))%>
				</button>
				<button id="btn-cancelar" type="button" onclick="CloseModal('confirm_accion');" class="btn btn-default" title="<%=GetGlobalResourceObject("Resource", "CancelButtonResource")%>">
					<i class="glyphicon glyphicon-remove-circle"></i> <%=GetGlobalResourceObject("Resource", "CancelButtonResource")%>
				</button>
            </div>
        </div><!-- /.modal-content -->
    </div><!-- /.modal-dialog -->
</div><!-- /.modal -->

<div class="modal fade" id="confirm_update_rule" tabindex="0" role="dialog" aria-labelledby="gridSystemModalLabel">
    <div class="modal-dialog">
        <div class="modal-content">
            <div class="modal-header">
                <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                <h4 class="modal-title" id="gridSystemModalLabel-update-rule"><% Response.Write(GetLocalResourceObject("UpdateTitle"))%></h4>
            </div>
            <div class="modal-body">
                <% Response.Write(GetLocalResourceObject("ConfirmChanges"))%>
            </div>
            <div class="modal-footer">
                <button id="btn-aceptar-act" type="button" onclick="javascript: UpdateRule();" class="btn btn-default" title="<% Response.Write(GetGlobalResourceObject("Resource", "AcceptBtnResource"))%>">
					<i class="fa fa-pencil-square-o fa-lg"></i> <% Response.Write(GetGlobalResourceObject("Resource", "AcceptBtnResource"))%>
				</button>
				<button id="btn-cancelar-act" type="button" onclick="javascript:$('#confirm_update_rule').modal('toggle');" class="btn btn-default" title="<%=GetGlobalResourceObject("Resource", "CancelButtonResource")%>">
					<i class="glyphicon glyphicon-remove-circle"></i> <%=GetGlobalResourceObject("Resource", "CancelButtonResource")%>
				</button>
            </div>
        </div><!-- /.modal-content -->
    </div><!-- /.modal-dialog -->
</div><!-- /.modal -->

    <div class="modal fade" id="confirm_delete_rule" tabindex="0" role="dialog" aria-labelledby="gridSystemModalLabel">
    <div class="modal-dialog">
        <div class="modal-content">
            <div class="modal-header">
                <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                <h4 class="modal-title" id="gridSystemModalLabel-delete-rule"><% Response.Write(GetLocalResourceObject("DeleteTitle"))%></h4>
            </div>
            <div class="modal-body" id="confirm_delete_rule_message">
                <% Response.Write(GetLocalResourceObject("ConfirmChanges"))%>
            </div>
            <div class="modal-footer">
                <button id="btn-aceptar-del" type="button" onclick="javascript: DeleteRule();" class="btn btn-default" title="<% Response.Write(GetGlobalResourceObject("Resource", "AcceptBtnResource"))%>">
					<i class="fa fa-pencil-square-o fa-lg"></i> <% Response.Write(GetGlobalResourceObject("Resource", "AcceptBtnResource"))%>
				</button>
				<button id="btn-cancelar-del" type="button" onclick="javascript:$('#confirm_delete_rule').modal('toggle');" class="btn btn-default" title="<%=GetGlobalResourceObject("Resource", "CancelButtonResource")%>">
					<i class="glyphicon glyphicon-remove-circle"></i> <%=GetGlobalResourceObject("Resource", "CancelButtonResource")%>
				</button>
            </div>
        </div><!-- /.modal-content -->
    </div><!-- /.modal-dialog -->
</div><!-- /.modal -->
        <div class="loadingDiv" id="loadingDiv"></div>
    <div id="loadingFog" class="loadingFog"></div>
        <% If ConfigurationManager.AppSettings.Get("NBEnableHTML5") IsNot Nothing AndAlso ConfigurationManager.AppSettings.Get("NBEnableHTML5") = "False" Then %>            

    <link href="../Styles/fasi.css?v=<%=System.IO.File.GetLastWriteTime(Server.MapPath("~") + "\Styles\fasi.css").ToString("yyyyMMddHHmmss")%>" rel="stylesheet" />
    <script src="/Scripts/grid.icons.extend.js" charset="utf-8"></script>
    <script src="/Scripts/grid.locale-<%=Threading.Thread.CurrentThread.CurrentCulture.Name%>.js" charset="utf-8"></script>
    <script src="/Scripts/grid.locale.extend-<%=Threading.Thread.CurrentThread.CurrentCulture.Name%>.js" charset="utf-8"></script>
    <script src="/Scripts/jquery.ui.datepicker-<%=Threading.Thread.CurrentThread.CurrentCulture.Name%>.js"></script>
    <script src="/Scripts/jquery.free.jqgrid.min.js"></script>
    <script type="text/javascript" src="Scripts/TabUnderwritingRules.js?v=<%=System.IO.File.GetLastWriteTime(Server.MapPath("~") + "\Underwriting\scripts\TabUnderwritingRules.js").ToString("yyyyMMddHHmmss")%>"></script>
    <%End If %>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footer" runat="server">
        <% If ConfigurationManager.AppSettings.Get("NBEnableHTML5") IsNot Nothing AndAlso ConfigurationManager.AppSettings.Get("NBEnableHTML5") Then %>            
    
        <link href="/Styles/fasi.css?v=<%=System.IO.File.GetLastWriteTime(Server.MapPath("~") + "\Styles\fasi.css").ToString("yyyyMMddHHmmss")%>" rel="stylesheet" />
        <link href="/Styles/ui.jqgrid-bootstrap.css" rel="stylesheet" />
        <link href="/Styles/jquery-ui.min-1.11.4.css" rel="stylesheet" /> 

            <script src="/Scripts/jquery-migrate-1.4.1.min.js"></script>
            <script src="/Scripts/bootstrap.min.js"></script>
            <script src="/Scripts/jquery-ui.js"></script>
            <script src="/Scripts/moment.min.js"></script>
            <script src="/Scripts/jquery.numeric.min.js"></script>
            <script src="/Scripts/jquery.validate.min.js"></script>
            <script src="/Scripts/jquery.validate.messages-es.js"></script>
            <script src="/Scripts/additional-methods.min.js"></script>

            <!--JQuery Toast-->
            <script src="/Scripts/jquery.toast.js"></script>
    
            <script src="/Scripts/grid.icons.extend.js" charset="utf-8"></script>        
            <script src="/Scripts/grid.locale-<%=Threading.Thread.CurrentThread.CurrentCulture.Name%>.js" charset="utf-8"></script>
            <script src="/Scripts/grid.locale.extend-<%=Threading.Thread.CurrentThread.CurrentCulture.Name%>.js" charset="utf-8"></script>
            <script src="/Scripts/jquery.free.jqgrid.min.js"></script>
            <script src="/Scripts/jquery.ext.js"></script>
            <script src="/Scripts/jquery-ui.js"></script>
            <script src="/Scripts/jquery.numeric.min.js"></script>
            <script src="/Scripts/jquery.ui.datepicker-<%=Threading.Thread.CurrentThread.CurrentCulture.Name%>.js"></script>
            <script src="/Scripts/moment.min.js"></script>


           <!-- -->
            <script src="/Scripts/fasi.js?v=<%=System.IO.File.GetLastWriteTime(Server.MapPath("~") + "\Scripts\fasi.js").ToString("yyyyMMddHHmmss")%>"></script>   
            <script type="text/javascript" src="Scripts/TabUnderwritingRules.js?v=<%=System.IO.File.GetLastWriteTime(Server.MapPath("~") + "\Underwriting\scripts\TabUnderwritingRules.js").ToString("yyyyMMddHHmmss")%>"></script>
        <% End If %>
    </asp:Content>
