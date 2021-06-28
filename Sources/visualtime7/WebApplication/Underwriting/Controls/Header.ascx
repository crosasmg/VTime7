<%@ Control Language="VB" AutoEventWireup="false" CodeFile="Header.ascx.vb" Inherits="Underwriting_Controls_Header" %>

<style>
    #header-menu {
        margin-top: 8px;
        padding: 0 5px;
		display: inline-block;
    }

    #header-menu li {
        padding: 0;
    }

    #header-menu li button {
        border-radius: 4px !important;
    }

    #header-form {
        margin: 0;
        padding: 8px 0;
        border: 1px solid #DDD;
    }

    #header-form .input-group {
        padding: 0 15px;
    }

    #dpeCaseId_I {
        height: 23px;
        border-right: none;
    }
    #dpeCaseId_I:disabled + #caseIdAddon {
        background: #eee;
        cursor: not-allowed;
    }

    #caseIdAddon {
        padding: 3px;
        background: transparent;
    }

</style>

<div id="header-conatiner" class="container-full">
    <ul id="header-menu" class="list-inline">
        <li>
            <button id="editCaseItem" class="btn btn-default">
                <i class="fa fa-pencil" aria-hidden="true"></i>
                <% Response.Write(GetLocalResourceObject("EditCaseItemResource.Text"))%>
            </button>
        </li>
        <li>
            <button id="editCancelCaseItem" class="btn btn-default">
                <i class="fa fa-ban" aria-hidden="true"></i>
                <% Response.Write(GetLocalResourceObject("EditCancelCaseItemResource.Text"))%>
            </button>
        </li>
        <li>
            <button id="saveCaseItem" class="btn btn-default">
                <i class="fa fa-floppy-o" aria-hidden="true"></i>
                <% Response.Write(GetLocalResourceObject("SaveCaseItemResource.Text"))%>
            </button>
        </li>
        <li>
            <button id="saveCloseCaseItem" class="btn btn-default">
                <i class="fa fa-floppy-o" aria-hidden="true"></i>
                <% Response.Write(GetLocalResourceObject("SaveCloseCaseItemResource.Text"))%>
            </button>
        </li>
        <li>
            <button id="acceptCaseItem" class="btn btn-default">
                <i class="fa fa-check-circle" aria-hidden="true"></i>
                <% Response.Write(GetLocalResourceObject("AcceptCaseItemResource.Text"))%>
            </button>
        </li>
        <li>
            <button id="acceptEndorsementItem" class="btn btn-default">

                <% Response.Write(GetLocalResourceObject("AcceptEndorsementItemResource.Text"))%>
            </button>
        </li>
        <li>
            <button id="declineCaseItem" class="btn btn-default">
                <i class="fa fa-ban" aria-hidden="true"></i>
                <% Response.Write(GetLocalResourceObject("DeclineCaseItemResource.Text"))%>
            </button>
        </li>
        <li>
            <button id="declineEndorsementItem" class="btn btn-default">
                <i class="fa fa-ban" aria-hidden="true"></i>
                <% Response.Write(GetLocalResourceObject("DeclineEndorsementItemResource.Text"))%>
            </button>
        </li>
        <li>
            <button id="reopenCaseItem" class="btn btn-default">
                <i class="fa fa-external-link" aria-hidden="true"></i>
                <% Response.Write(GetLocalResourceObject("ReopenCaseItemResource.Text"))%>
            </button>
        </li>
        <li>
            <button id="editPolicyToBeIsued" class="btn btn-default">
                <i class="fa fa-pencil" aria-hidden="true"></i>
                <% Response.Write(GetLocalResourceObject("EditPolicyToBeIsuedResource.Text"))%>
            </button>
        </li>
        <li>
            <button id="editPolicyToBeEndorse" class="btn btn-default">
                <i class="fa fa-pencil" aria-hidden="true"></i>
                <% Response.Write(GetLocalResourceObject("EditPolicyToBeEndorseItemResource.Text"))%>
            </button>
        </li>
        <li>
            <button id="openFormItem" class="btn btn-default">
                <i class="fa fa-external-link" aria-hidden="true"></i>
                <% Response.Write(GetLocalResourceObject("OpenFormItemResource.Text"))%>
            </button>
        </li>
    </ul>
</div>


<div id="header-form">
    <div class="form-group padding-group">
        <div class="row">

            <div class="col-md-4">
                <div>
                    <label for="dpeCaseId_I" class="col-sm-4 control-label">
                        <% Response.Write(GetLocalResourceObject("CaseIDLabelResource1.Text"))%></label>
                    <div class="col-sm-8 input-group">
                        <input type="text" class="form-control" id="dpeCaseId_I" name="dpeCaseId_I" title='<% Response.Write(GetLocalResourceObject("dpeCaseIdResource1.ToolTip"))%>' />
                        <span id="caseIdAddon" class="input-group-addon"><i class="fa fa-caret-down"></i></span>
                    </div>
                    <span id="invalidCase" class="col-sm-offset-4 col-sm-8" style="display: none;color:red;"><% Response.Write(GetLocalResourceObject("CaseIDMessageLabelResource1.Text"))%></span>
                </div>
            </div>

            <div class="col-md-4">
                <div class="form-group">
                    <label for="underwritingCaseStatus" class="col-sm-4 control-label">
                        <% Response.Write(GetLocalResourceObject("UnderwritingCaseStatusLabelResource1.Text"))%></label>
                    <div class="col-sm-8">
                        <label class="control-label normal-font" id="underwritingCaseStatus" title='<% Response.Write(GetLocalResourceObject("CaseStatusResource1.ToolTip"))%>'></label>
                    </div>
                </div>
            </div>

            <div class="col-md-4">
                <img id="imgWFProgress" src="" />
                <label id="wFProgressText"></label>
            </div>
        </div>

        <div class="row">
            <div class="col-md-4">
                <div class="form-group">
                    <label for="fullproposal" class="col-sm-4 control-label">
                        <% Response.Write(GetLocalResourceObject("FullproposalIDLabelResource1.Text"))%></label>
                    <div class="col-sm-8">
                        <label id="fullproposal" class="control-label normal-font" title='<% Response.Write(GetLocalResourceObject("FullproposalIDResource1.ToolTip"))%>'></label>
                    </div>
                </div>
            </div>

            <div class="col-md-4">
                <div class="form-group">
                    <label for="decision" class="col-sm-4 control-label">
                        <% Response.Write(GetLocalResourceObject("DecisionLabelResource1.Text"))%></label>
                    <div class="col-sm-8">
                        <img src="" id="imgDecision" />
                        <label class="control-label normal-font" id="decision" title='<% Response.Write(GetLocalResourceObject("DecitionResource1.ToolTip"))%>'></label>
                    </div>
                </div>
            </div>
        </div>

        <div class="row">
            <div class="col-md-4 col-md-offset-4">
                <div class="form-group">
                    <label for="stage" class="col-sm-4 control-label">
                        <% Response.Write(GetLocalResourceObject("StageLabelResource1.Text"))%></label>
                    <div class="col-sm-8">
                        <select class="form-control" name="stage" id="stage" disabled="disabled" title='<% Response.Write(GetLocalResourceObject("StageResource1.ToolTip"))%>'></select>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>

<!-- Modal case search -->
<div id="modal-case-search" class="modal fade" role="dialog">
  <div class="modal-dialog modal-lg">

    <!-- Modal content-->
    <div class="modal-content">
      <div class="modal-header">
        <button type="button" class="close" data-dismiss="modal">&times;</button>
      </div>
      <div class="modal-body">
        <div class="grid-case-search-wrapper">
            <table id="grid-case-search"></table>
            <div id="pager-case-search"></div>
        </div>
      </div>
    </div>

  </div>
</div>


<!-- Modal decline case -->
<div id="modal-decline-case" class="modal fade" role="dialog">
  <div class="modal-dialog">

    <!-- Modal content-->
    <div class="modal-content">
      <div class="modal-header">
        <button type="button" class="close" data-dismiss="modal">&times;</button>
        <h4 class="modal-title"><% Response.Write(GetLocalResourceObject("RejectionReasonResource"))%></h4>
      </div>
      <div class="modal-body">
        <div class="form-group padding-group">
            <div id="errorMessages" class="bg-danger">

                <div class="row">
                    <div class="col-sm-12">
                        <div class="form-group">
                            <label id="errorComboRejectionReason" class="col-sm-12 control-label">
                                <% Response.Write(GetLocalResourceObject("ReasonResource"))%>: &nbsp; <% Response.Write(GetLocalResourceObject("RejectedReasonErrorField"))%>
                            </label>
                        </div>
                    </div>
                </div>

                <div class="row">
                    <div class="col-sm-12">
                        <div class="form-group">
                            <label id="errorfreeTextReason" class="col-sm-12 control-label">
                                <% Response.Write(GetLocalResourceObject("FreeTextReasonResource"))%>: &nbsp; <% Response.Write(GetLocalResourceObject("RejectedReasonErrorField"))%>
                            </label>
                        </div>
                    </div>
                </div>
            </div>

            <div class="row padding-group">
                <div class="col-sm-12">
                    <div class="form-group">
                        <label for="comboRejectionReason" class="col-sm-4 control-label">
                            <% Response.Write(GetLocalResourceObject("ReasonResource"))%>
                        </label>
                        <div class="col-sm-8">
                            <select class="form-control" name="comboRejectionReason" id="comboRejectionReason" title='<% Response.Write(GetLocalResourceObject("ReasonResourceComboBox"))%>'></select>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row padding-group">
                <div class="col-sm-12">
                    <div class="form-group">
                        <label for="freeTextReason" class="col-sm-4 control-label">
                            <% Response.Write(GetLocalResourceObject("FreeTextReasonResource"))%>
                        </label>
                        <div class="col-sm-8">
                            <textarea class="form-control" name="freeTextReason" id="freeTextReason" cols="" rows="4" title='<% Response.Write(GetLocalResourceObject("FreeReasonResourceText"))%>'></textarea>
                        </div>
                    </div>
                </div>
            </div>
        </div>
      </div>
      <div class="modal-footer">
        <button id="declineCaseModalButton" class="btn btn-default">
        <i class="fa fa-pencil-square-o fa-lg" aria-hidden="true"></i>
            <% Response.Write(GetLocalResourceObject("DeclineCaseItemResource"))%>
        </button>
        <button type="button" class="btn btn-default" data-dismiss="modal">
            <i class="glyphicon glyphicon-remove-circle" aria-hidden="true"></i>
            <% Response.Write(GetGlobalResourceObject("Resource", "Cancel"))%>
        </button>
      </div>
    </div>

  </div>
</div>

<!-- Modal workflow response -->
<div id="modal-workflow-response" class="modal fade" role="dialog">
  <div class="modal-dialog">
    <!-- Modal content-->
    <div class="modal-content">
      <div class="modal-header">
        <button type="button" class="close" data-dismiss="modal">&times;</button>
        <h4 class="modal-title"><% Response.Write(GetLocalResourceObject("Information"))%></h4>
      </div>
      <div class="modal-body">
        <div class="form-group padding-group">
        <div class="row">

            <div class="col-sm-12">
                <div class="form-group">
                    <label id="workflowInformation" class="col-sm-12 control-label"></label>
                </div>
            </div>

            <div class="col-sm-12">
                <div class="form-group">
                    <label id="workflowStackTrace" class="col-sm-12 control-label"></label>
                </div>
            </div>
        </div>

        </div>
      </div>
      <div class="modal-footer">
        <button type="button" class="btn btn-default" data-dismiss="modal">
            <% Response.Write(GetGlobalResourceObject("Resource", "Ok"))%>
        </button>
      </div>
    </div>
  </div>
</div>
