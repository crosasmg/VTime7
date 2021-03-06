<%@ Page Language="VB" MasterPageFile="~/fasi/FASI.master" AutoEventWireup="false" %>
<%@ MasterType TypeName="FASI" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <!-- This code was generated by Form Designer v7.1.224.2 at 2019/04/04 01:21:22 PM model release 2, Form Generator v1.0.34.31 -->
    <!-- Changes to this file may cause incorrect behavior and will be lost if the code is regenerated. -->
    
    <!-- Bootstrap -->
    <link rel='stylesheet' href='/fasi/assets/css/ladda-themeless.min.css' />
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
        <form id="HT5OnLineServiceUnderwriterIIMainForm">
            <input type="hidden" id="HT5OnLineServiceUnderwriterIIFormId" name="HT5OnLineServiceUnderwriterIIFormId" value="" />
            <div id="alerts-container"></div>
			<div class="row">
            <!-- Container content -->
                        <!-- Container: zone5 zone -->
                        <div  class='col-md-12'>

                  <div id="zone5">
    <!-- Container content -->
                <!-- ActionType radiobuttonlist -->
                <div class='col-md-6 form-horizontal'>
                  <div class='form-group'>
                    <div class='col-md-4 text-left'>
                      <label id='ActionTypeLabel' data-i18n="[html]app.form.ActionType_Label" class='control-label' for='ActionType'></label>
                    </div>
                    <div class='col-md-8'>
                                <div id='ActionTypeWrap'>
                <div class='radio'>
                  <label><input type='radio' name='ActionType' id='ActionType_1' title='' value='1' />                  <span data-i18n="app.form.ActionType_1_Display" >Cotizaciones</span>
                </label>
                </div>
                <div class='radio'>
                  <label><input type='radio' name='ActionType' id='ActionType_2' value='2' />                  <span data-i18n="app.form.ActionType_2_Display" >Panel de suscripción</span>
                </label>
                </div>
                <div class='radio'>
                  <label><input type='radio' name='ActionType' id='ActionType_3' value='3' />                  <span data-i18n="app.form.ActionType_3_Display" >Consulta de casos pendientes</span>
                </label>
                </div>
                <div id='ActionType_validate'></div>
            </div>

                    </div>
                  </div>
                </div>
                <!-- Container: zone0 zone -->
                <div  class='col-md-6'>

                  <div id="zone0" class='hidden' >
    <!-- Container content -->
                <!-- StartDate datepicker -->
                <div class='col-md-6 form-vertical'>
                  <div class='form-group'>
                    <label id='StartDateLabel' data-i18n="[html]app.form.StartDate_Label" class='control-label' for='StartDate'>Período</label><span id='StartDateRequired' class='required-mark'>*</span>
                    <div class='col-xs-12 input-group'>    <div class='input-group date' id='StartDate_group'>
                                    <input id='StartDate' name='StartDate' type='text' class='form-control' title='Fecha inicial del período a consultar' size='10' data-i18n="[title]app.form.StartDate_Tooltip" maxlength='10'/>
                                    <span class='input-group-addon'>
                                        <span class='glyphicon glyphicon-calendar'></span>
                                    </span>
                                </div><span class='input-group-btn'></span>    <div class='input-group date' id='EndDate_group'>
                                    <input id='EndDate' name='EndDate' type='text' class='form-control' title='Fecha final del período a consultar' size='10' data-i18n="[title]app.form.EndDate_Tooltip" maxlength='10'/>
                                    <span class='input-group-addon'>
                                        <span class='glyphicon glyphicon-calendar'></span>
                                    </span>
                                </div></div>
                  </div>
                </div>
                <!-- button4 button -->
                <div class='col-md-6 form-vertical'>
                  <div class='form-group'>
                    <button id='button4' class='ladda-button btn pull-right btn-default' data-style='expand-right' title='Busca los casos registrados en el período indicado' data-i18n="app.form.button4_Caption;[title]app.form.button4_Tooltip" ><span class='ladda-label'>
Buscar casos   <img src='/images/Library/16x16_ASPNetIcons/zoom_in_16x16.gif' id='button4Image' /></span><span class='ladda-spinner'></span></button>

                  </div>
                </div>
    <!-- End Container content -->
               </div>
                </div>
                <!-- End Container: zone0 zone -->
    <!-- End Container content -->
               </div>
                        </div>
                        <!-- End Container: zone5 zone -->
                        <!-- Container: UnderwritingCaseCollection zone -->
                        <div  class='col-md-12'>

                  <div id="UnderwritingCaseCollection" class='panel panel-default hidden'>
                    <div class='panel-heading' data-i18n="app.form.UnderwritingCaseCollection_Title;[title]app.form.UnderwritingCaseCollection_Tooltip" title='Casos'>Casos registrados en el período indicado</div>
                 <div class="panel-body">
    <!-- Container content -->
                <!-- Container: UnderwritingCase grid -->
                <div  class='col-md-12'>

               <div id='UnderwritingCaseContainer'>
                    <div id='UnderwritingCaseTblPlaceHolder'></div>
                    <!-- Lookups: UnderwritingCase Grid -->
                            <select id='LineOfBusiness' hidden='hidden'>
                            </select>

                            <select id='Product' hidden='hidden'>
                            </select>

                            <select id='Decision' hidden='hidden'>
                            </select>

                            <select id='Status' hidden='hidden'>
                            </select>

                    <!-- End Lookups: UnderwritingCase grid -->
               </div>

                </div>
                <!-- End Container: UnderwritingCase grid -->
                <!-- Container: zone6 zone -->
                <div  class='col-md-12'>

                  <div id="zone6">
    <!-- Container content -->
                <!-- CaseToQuery dropdownlist -->
                <div class='col-md-6 form-vertical'>
                  <div class='form-group'>
                    <label id='CaseToQueryLabel' data-i18n="[html]app.form.CaseToQuery_Label" class='control-label' for='CaseToQuery'>Caso a consultar/modificar</label>
                        <select id='CaseToQuery' name='CaseToQuery' class='form-control' data-i18n="[title]app.form.CaseToQuery_Tooltip" title='Si así lo desea, coloque en este campo el número del caso a consultar en detalle'>
                                </select>

                  </div>
                </div>
                <!-- button7 button -->
                <div class='col-md-6 form-vertical'>
                  <div class='form-group'>
                    <button id='button7' class='ladda-button btn pull-right btn-default' data-style='expand-right' title='Ir al panel de suscripción a fin de visualizar en detalle el caso seleccionado' data-i18n="app.form.button7_Caption;[title]app.form.button7_Tooltip" ><span class='ladda-label'>
Panel de suscripción   <img src='/images/Library/16x16_ASPNetIcons/right_16x16.gif' id='button7Image' /></span><span class='ladda-spinner'></span></button>

                  </div>
                </div>
    <!-- End Container content -->
               </div>
                </div>
                <!-- End Container: zone6 zone -->
                <!-- label4 label -->
                <div class='col-md-12 form-vertical'>
                  <div class='form-group text-center'>
                                  <label id='label4' data-i18n="[html]app.form.label4_Label;[title]app.form.label4_Tooltip" title='' class='hidden'  style='font-weight: bold;'>No existen casos registrados en el período indicado</label>

                  </div>
                </div>
    <!-- End Container content -->
                 </div>
               </div>
                        </div>
                        <!-- End Container: UnderwritingCaseCollection zone -->
            <!-- End Container content -->

            </div>
        </form>





    </div>
  </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footer" runat="Server">
    <script src='/fasi/assets/js/spin.min.js'></script>
    <script src='/fasi/assets/js/ladda.min.js'></script>
    <script src='/fasi/assets/js/ladda.jquery.min.js'></script>
    <script src='/fasi/assets/js/bootstrap-datetimepicker.min.js'></script>
    <script src='/fasi/assets/js/bootstrap-table.min.js'></script>
    <script src='/fasi/assets/js/bootstrap-table-es-CR.min.js'></script>
    <script src='/fasi/app/js/TableHelper.js?rel=20190404012122169'></script>
    <script src='/fasi/assets/js/i18next.min.js'></script>


    <script src="HT5OnLineServiceUnderwriterII.js?rel=20190404012122169"></script>
 
</asp:Content>