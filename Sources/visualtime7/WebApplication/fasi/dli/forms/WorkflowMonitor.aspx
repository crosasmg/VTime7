<%@ Page Language="VB" MasterPageFile="~/fasi/FASI.master" AutoEventWireup="false" %>
<%@ MasterType TypeName="FASI" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <!-- This code was generated by Form Designer v7.3.42.1 at 2020-05-20 04:11:48 PM model release 17, Form Generator v1.0.37.65 -->
    <!-- Changes to this file may cause incorrect behavior and will be lost if the code is regenerated. -->
    
    <!-- Bootstrap -->
    <link rel='stylesheet' href='/fasi/assets/css/bootstrap-datetimepicker.min.css' />
    <link rel='stylesheet' href='/fasi/assets/css/bootstrap-table.min.css' />

    <!-- HTML5 Shim and Respond.js IE8 support of HTML5 elements and media queries -->
    <!-- WARNING: Respond.js doesn't work if you view the page via file:// -->
    <!--[if lt IE 9]>
      <script src="https://oss.maxcdn.com/libs/html5shiv/3.7.0/html5shiv.js"></script>
      <script src="https://oss.maxcdn.com/libs/respond.js/1.3.0/respond.min.js"></script>
   <![endif]-->
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceholder1" runat="Server">
    <div id="app" class="ibox">
     <div class="ibox-content">
        <form id="WorkflowMonitorMainForm">
            <input type="hidden" id="WorkflowMonitorFormId" name="WorkflowMonitorFormId" value="" />
            <div id="alerts-container"></div>
			<div class="row">
            <!-- Container content -->
                        <!-- Container: zone0 zone -->
                        <div  class='col-md-12'>

                  <div id="zone0">
    <!-- Container content -->
                <!-- StartDP datepicker -->
                <div class='col-md-4 form-horizontal'>
                  <div class='form-group'>
                    <div class='col-md-4 text-left'>
                      <label id='StartDPLabel' data-i18n="[html]app.form.StartDP_Label" class='control-label' for='StartDP'>Inicio</label><span id='StartDPRequired' class='required-mark'>*</span>
                    </div>
                    <div class='col-md-8'>
                        <div class='input-group date' id='StartDP_group'>
                                    <input id='StartDP' name='StartDP' type='text' class='form-control' title='Fecha Inicio' size='19' data-i18n="[title]app.form.StartDP_Tooltip" maxlength='19'/>
                                    <span class='input-group-addon'>
                                        <span class='glyphicon glyphicon-calendar'></span>
                                    </span>
                                </div>
                    </div>
                  </div>
                </div>
                <!-- FinishDP datepicker -->
                <div class='col-md-4 form-horizontal'>
                  <div class='form-group'>
                    <div class='col-md-4 text-left'>
                      <label id='FinishDPLabel' data-i18n="[html]app.form.FinishDP_Label" class='control-label' for='FinishDP'>Fin</label><span id='FinishDPRequired' class='required-mark'>*</span>
                    </div>
                    <div class='col-md-8'>
                        <div class='input-group date' id='FinishDP_group'>
                                    <input id='FinishDP' name='FinishDP' type='text' class='form-control' title='Fecha de Fin' size='19' data-i18n="[title]app.form.FinishDP_Tooltip" maxlength='19'/>
                                    <span class='input-group-addon'>
                                        <span class='glyphicon glyphicon-calendar'></span>
                                    </span>
                                </div>
                                   <div id='FinishDP_validate'></div>
                    </div>
                  </div>
                </div>
                <!-- OkButton button -->
                <div class='col-md-4 form-horizontal'>
                  <div class='form-group'>
                    <div class='col-md-8'>
                    <button id='OkButton' class='ladda-button btn pull-left btn-primary' data-style='expand-right' title='' data-i18n="app.form.OkButton_Caption;[title]app.form.OkButton_Tooltip" ><span class='ladda-label'>OK</span><span class='ladda-spinner'></span></button>

                    </div>
                  </div>
                </div>
    <!-- End Container content -->
               </div>
                        </div>
                        <!-- End Container: zone0 zone -->
                        <!-- Container: Client grid -->
                        <div  class='col-md-12'>

                       <div id='ClientContainer'>
                            <div id='ClientTblPlaceHolder'></div>
                       </div>

                        </div>
                        <!-- End Container: Client grid -->
            <!-- End Container content -->

            </div>
        </form>





    </div>
  </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footer" runat="Server">
    <script src='/fasi/assets/js/bootstrap-datetimepicker.min.js'></script>
    <script src='/fasi/assets/js/bootstrap-table.min.js'></script>
    <script src='/fasi/assets/js/bootstrap-table-es-CR.min.js'></script>
    <script src='/fasi/app/js/TableHelper.js?rel=20200520041148811'></script>
    <script src='/fasi/assets/js/bootstrap-table-export.js'></script>
    <script src='/fasi/assets/js/tableExport.min.js'></script>
    <script src='/fasi/assets/js/xlsx.core.min.js'></script>
    <script src='/fasi/assets/js/jspdf.min.js'></script>
    <script src='/fasi/assets/js/jspdf.plugin.autotable.min.js'></script>
    <script src='/fasi/assets/js/jquery.base64.js'></script>
    <script src='/fasi/assets/js/autoNumeric.min.js'></script>
    <script src='/fasi/assets/js/i18next.min.js'></script>


    <script src="WorkflowMonitor.js?rel=20200520041148811"></script>
 
</asp:Content>