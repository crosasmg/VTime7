<%@ Page Language="VB" MasterPageFile="~/fasi/FASI.master" AutoEventWireup="false" %>
<%@ MasterType TypeName="FASI" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <!-- This code was generated by Form Designer v7.3.36.1 at 2020-03-18 09:46:09 AM model release 4, Form Generator v1.0.37.37 -->
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
        <form id="LookupDetailMainForm">
            <input type="hidden" id="LookupDetailFormId" name="LookupDetailFormId" value="" />
            <div id="alerts-container"></div>
			<div class="row">
            <!-- Container content -->
                        <!-- Container: LOOKUP_Grid grid -->
                        <div  class='col-md-12'>

                       <div id='LOOKUP_GridContainer'>
                            <div id="LOOKUP_Gridtoolbar">
                            <div class='form-inline'>
                                <button id="LOOKUP_GridCreateBtn" type="button" data-i18n="app.form.LOOKUP_Grid_AddButtonCaption;[data-modal-title]app.form.LOOKUP_Grid_AddButtonCaption"  class="btn btn-default" data-modal-title="Agregar">
                                    Agregar
                                </button>
                                <button id="LOOKUP_GridRemoveBtn"  data-i18n="app.form.LOOKUP_Grid_DeleteButtonCaption;[data-title]app.form.LOOKUP_Grid_DeleteButtonCaption"  type="button" class="btn btn-danger" disabled data-title="Eliminar">
                                    <i class="glyphicon glyphicon-remove"></i>Eliminar
                                </button>
                <!-- filter dropdownlist -->
                  <div class='form-group'>
                      <label id='filterLabel' data-i18n="[html]app.form.filter_Label" class='control-label' for='filter'>Lista</label>
                        <select id='filter' name='filter' class='form-control' data-i18n="[title]app.form.filter_Tooltip" title='Lista'>
                                </select>

                  </div>
                <!-- dropdownlist3 dropdownlist -->
                  <div class='form-group'>
                      <label id='dropdownlist3Label' data-i18n="[html]app.form.dropdownlist3_Label" class='control-label' for='dropdownlist3'>en</label>
                        <select id='dropdownlist3' name='dropdownlist3' class='form-control' data-i18n="[title]app.form.dropdownlist3_Tooltip" title='en'>
                                </select>

                  </div>
                <!-- SetFilter button -->
                  <div class='form-group'>
                    <button id='SetFilter' class='ladda-button btn pull-left btn-info' data-style='expand-right' title='' data-i18n="app.form.SetFilter_Caption;[title]app.form.SetFilter_Tooltip" ><span class='ladda-label'>Filtrar</span><span class='ladda-spinner'></span></button>

                  </div>
                            </div>
                            </div>
                            <div id='LOOKUP_GridTblPlaceHolder'></div>
                       </div>

                        </div>
                        <!-- End Container: LOOKUP_Grid grid -->
                        <!-- Container: FilterBar zone -->
                        <div  class='col-md-12'>

                        </div>
                        <!-- End Container: FilterBar zone -->
            <!-- End Container content -->

            </div>
        </form>



        <div id='LOOKUP_GridPopup' class='modal fade' tabindex='-1' role='dialog' aria-labelledby='edit' aria-hidden='true'>
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                        <h4 class="modal-title"><span data-i18n="app.form.GridViewPopup_TitleCaption">Información</span></h4>
                    </div>
                    <div class="modal-body">
                      <div class="row">
                        <form id='LOOKUP_GridEditForm'>
                            <!-- Container content -->
                                        <!-- LOOKUPID numeric -->
                                        <div class='col-md-12 form-horizontal'>
                                          <div class='form-group'>
                                            <div class='col-md-4 text-left'>
                                              <label id='LOOKUPIDLabel' data-i18n="[html]app.form.LOOKUPID_Label" class='control-label hidden' for='LOOKUPID'>Lista</label><span id='LOOKUPIDRequired' class='required-mark hidden'>*</span>
                                            </div>
                                            <div class='col-md-8'>
                                                <input class='form-control hidden' id='LOOKUPID' data-i18n="[title]app.form.LOOKUPID_Tooltip" name='LOOKUPID' title='Identificador del Lenguaje' type='text' style='text-align: right'/>
                                            </div>
                                          </div>
                                        </div>
                                        <!-- LANGUAGEID numeric -->
                                        <div class='col-md-12 form-horizontal'>
                                          <div class='form-group'>
                                            <div class='col-md-4 text-left'>
                                              <label id='LANGUAGEIDLabel' data-i18n="[html]app.form.LANGUAGEID_Label" class='control-label hidden' for='LANGUAGEID'>Lenguaje</label><span id='LANGUAGEIDRequired' class='required-mark hidden'>*</span>
                                            </div>
                                            <div class='col-md-8'>
                                                <input class='form-control hidden' id='LANGUAGEID' data-i18n="[title]app.form.LANGUAGEID_Tooltip" name='LANGUAGEID' title='Nombre de Languaje' type='text' style='text-align: right'/>
                                            </div>
                                          </div>
                                        </div>
                                        <!-- CODE numeric -->
                                        <div class='col-md-12 form-horizontal'>
                                          <div class='form-group'>
                                            <div class='col-md-4 text-left'>
                                              <label id='CODELabel' data-i18n="[html]app.form.CODE_Label" class='control-label' for='CODE'>Código</label>
                                            </div>
                                            <div class='col-md-8'>
                                                <input class='form-control' id='CODE' data-i18n="[title]app.form.CODE_Tooltip" name='CODE' title='Codigo del lenguaje' type='text' style='text-align: right'/>
                                            </div>
                                          </div>
                                        </div>
                                        <!-- DESCRIPTION text -->
                                        <div class='col-md-12 form-horizontal'>
                                          <div class='form-group'>
                                            <div class='col-md-4 text-left'>
                                              <label id='DESCRIPTIONLabel' data-i18n="[html]app.form.DESCRIPTION_Label" class='control-label' for='DESCRIPTION'>Descripción</label><span id='DESCRIPTIONRequired' class='required-mark'>*</span>
                                            </div>
                                            <div class='col-md-8'>
                                                <input class='form-control' id='DESCRIPTION' data-i18n="[title]app.form.DESCRIPTION_Tooltip;[placeholder]app.form.DESCRIPTION_Watermark" name='DESCRIPTION' title='Descripción del Lenguaje' type='text' size='255' maxlength='255'/>
                                            </div>
                                          </div>
                                        </div>
                                        <!-- QUERYORDER numeric -->
                                        <div class='col-md-12 form-horizontal'>
                                          <div class='form-group'>
                                            <div class='col-md-4 text-left'>
                                              <label id='QUERYORDERLabel' data-i18n="[html]app.form.QUERYORDER_Label" class='control-label' for='QUERYORDER'>Orden</label>
                                            </div>
                                            <div class='col-md-8'>
                                                <input class='form-control' id='QUERYORDER' data-i18n="[title]app.form.QUERYORDER_Tooltip" name='QUERYORDER' title='Orden de la Consulta' type='text' style='text-align: right'/>
                                            </div>
                                          </div>
                                        </div>
                                        <!-- RECORDSTATUS dropdownlist -->
                                        <div class='col-md-12 form-horizontal'>
                                          <div class='form-group'>
                                            <div class='col-md-4 text-left'>
                                              <label id='RECORDSTATUSLabel' data-i18n="[html]app.form.RECORDSTATUS_Label" class='control-label' for='RECORDSTATUS'>Estado</label><span id='RECORDSTATUSRequired' class='required-mark'>*</span>
                                            </div>
                                            <div class='col-md-8'>
                                                <select id='RECORDSTATUS' name='RECORDSTATUS' class='form-control' data-i18n="[title]app.form.RECORDSTATUS_Tooltip" title='Estado del registro'>
                                                </select>

                                            </div>
                                          </div>
                                        </div>
                                        <!-- LASTUPDATEBY text -->
                                        <div class='col-md-12 form-horizontal'>
                                          <div class='form-group'>
                                            <div class='col-md-4 text-left'>
                                              <label id='LASTUPDATEBYLabel' data-i18n="[html]app.form.LASTUPDATEBY_Label" class='control-label hidden' for='LASTUPDATEBY'>Actualizado por</label>
                                            </div>
                                            <div class='col-md-8'>
                                                <input class='form-control hidden' id='LASTUPDATEBY' data-i18n="[title]app.form.LASTUPDATEBY_Tooltip;[placeholder]app.form.LASTUPDATEBY_Watermark" name='LASTUPDATEBY' title='Actualizado por' type='text' size='255' maxlength='255'/>
                                            </div>
                                          </div>
                                        </div>
                                        <!-- LASTUPDATEDON datepicker -->
                                        <div class='col-md-12 form-horizontal'>
                                          <div class='form-group'>
                                            <div class='col-md-4 text-left'>
                                              <label id='LASTUPDATEDONLabel' data-i18n="[html]app.form.LASTUPDATEDON_Label" class='control-label hidden' for='LASTUPDATEDON'>Actualizado</label>
                                            </div>
                                            <div class='col-md-8'>
                                                <div class='input-group date hidden' id='LASTUPDATEDON_group'>
                                                    <input id='LASTUPDATEDON' name='LASTUPDATEDON' type='text' class='form-control hidden' title='Última actualización' size='19' data-i18n="[title]app.form.LASTUPDATEDON_Tooltip" maxlength='19'/>
                                                    <span class='input-group-addon'>
                                                        <span class='glyphicon glyphicon-calendar'></span>
                                                    </span>
                                                </div>
                                            </div>
                                          </div>
                                        </div>
                            <!-- End Container content -->

                        </form>
                      </div>
                    </div>
                    <div class="modal-footer">
                        <button id="LOOKUP_GridSaveBtn"  data-i18n="app.form.GridViewPopup_SaveButtonCaption"  class="btn btn-warning">Guardar</button>
                        <button class="btn"  data-i18n="app.form.GridViewPopup_CancelButtonCaption"  data-dismiss="modal" aria-hidden="true">Cancelar</button>
                    </div>
                </div>
            </div>
        </div>


    </div>
  </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footer" runat="Server">
    <script src='/fasi/assets/js/bootstrap-datetimepicker.min.js'></script>
    <script src='/fasi/assets/js/bootstrap-table.min.js'></script>
    <script src='/fasi/assets/js/bootstrap-table-es-CR.min.js'></script>
    <script src='/fasi/app/js/TableHelper.js?rel=20200318094609162'></script>
    <script src='/fasi/assets/js/bootstrap-table-export.js'></script>
    <script src='/fasi/assets/js/tableExport.min.js'></script>
    <script src='/fasi/assets/js/xlsx.core.min.js'></script>
    <script src='/fasi/assets/js/autoNumeric.min.js'></script>
    <script src='/fasi/assets/js/i18next.min.js'></script>


    <script src="LookupDetail.js?rel=20200318094609162"></script>
 
</asp:Content>