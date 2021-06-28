﻿<%@ Page Language="VB" MasterPageFile="~/fasi/FASI.master" AutoEventWireup="false" %>
<%@ MasterType TypeName="FASI" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <!-- This code was generated by Form Designer v7.3.24.1 at 2019-11-08 04:29:52 p. m. model release 1, Form Generator v1.0.37.9 -->
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
        <form id="H5MantTipoDeDecisionMainForm">
            <input type="hidden" id="H5MantTipoDeDecisionFormId" name="H5MantTipoDeDecisionFormId" value="" />
            <div id="alerts-container"></div>
			<div class="row">
            <!-- Container content -->
                        <!-- Container: TabDecisionType_Grid grid -->
                        <div  class='col-md-12'>

                       <div id='TabDecisionType_GridContainer'>
                            <div id="TabDecisionType_Gridtoolbar">
                            <div class='form-inline'>
                                <button id="TabDecisionType_GridCreateBtn" type="button" data-i18n="app.form.TabDecisionType_Grid_AddButtonCaption;[data-modal-title]app.form.TabDecisionType_Grid_AddButtonCaption"  class="btn btn-default" data-modal-title="Agregar">
                                    Agregar
                                </button>
                                <button id="TabDecisionType_GridRemoveBtn"  data-i18n="app.form.TabDecisionType_Grid_DeleteButtonCaption;[data-title]app.form.TabDecisionType_Grid_DeleteButtonCaption"  type="button" class="btn btn-danger" disabled data-title="Eliminar">
                                    <i class="glyphicon glyphicon-remove"></i>Eliminar
                                </button>
                <!-- ShowTranslatorGrid button -->
                  <div class='form-group'>
                    <button id='ShowTranslatorGrid' class='ladda-button btn pull-left btn-default' data-style='expand-right' title='Mostrar Vista Traducción' data-i18n="app.form.ShowTranslatorGrid_Caption;[title]app.form.ShowTranslatorGrid_Tooltip" ><span class='ladda-label'>Vista Traducción</span><span class='ladda-spinner'></span></button>

                  </div>
                            </div>
                            </div>
                            <div id='TabDecisionType_GridTblPlaceHolder'></div>
                       </div>

                        </div>
                        <!-- End Container: TabDecisionType_Grid grid -->
                        <!-- Container: TabDecisionTypeTranslator_Grid grid -->
                        <div  class='col-md-12'>

                       <div id='TabDecisionTypeTranslator_GridContainer' class='hidden'>
                            <div id="TabDecisionTypeTranslator_Gridtoolbar">
                            <div class='form-inline'>
                <!-- ShowStandardGrid button -->
                  <div class='form-group'>
                    <button id='ShowStandardGrid' class='ladda-button btn pull-left btn-default' data-style='expand-right' title='Mostrar Vista Estándar' data-i18n="app.form.ShowStandardGrid_Caption;[title]app.form.ShowStandardGrid_Tooltip" ><span class='ladda-label'>Vista Estándar</span><span class='ladda-spinner'></span></button>

                  </div>
                            </div>
                            </div>
                            <div id='TabDecisionTypeTranslator_GridTblPlaceHolder' class='hidden'></div>
                       </div>

                        </div>
                        <!-- End Container: TabDecisionTypeTranslator_Grid grid -->
                        <!-- Container: zoneStandardToolBar zone -->
                        <div  class='col-md-12'>

                        </div>
                        <!-- End Container: zoneStandardToolBar zone -->
                        <!-- Container: zoneTranslatorToolBar zone -->
                        <div  class='col-md-12'>

                        </div>
                        <!-- End Container: zoneTranslatorToolBar zone -->
            <!-- End Container content -->

            </div>
        </form>



        <div id='TabDecisionType_GridPopup' class='modal fade' tabindex='-1' role='dialog' aria-labelledby='edit' aria-hidden='true'>
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                        <h4 class="modal-title"><span data-i18n="app.form.GridViewPopup_TitleCaption">Información</span></h4>
                    </div>
                    <div class="modal-body">
                      <div class="row">
                        <form id='TabDecisionType_GridEditForm'>
                            <!-- Container content -->
                                        <!-- Decision numeric -->
                                        <div class='col-md-6 form-horizontal'>
                                          <div class='form-group'>
                                            <div class='col-md-4 text-left'>
                                              <label id='DecisionLabel' data-i18n="[html]app.form.Decision_Label" class='control-label' for='Decision'>Decisión</label><span id='DecisionRequired' class='required-mark'>*</span>
                                            </div>
                                            <div class='col-md-8'>
                                                <input class='form-control' id='Decision' data-i18n="[title]app.form.Decision_Tooltip" name='Decision' title='Código de la decisión del caso de suscripción' type='text' style='text-align: right'/>
                                            </div>
                                          </div>
                                        </div>
                                        <!-- RecordStatus dropdownlist -->
                                        <div class='col-md-6 form-horizontal'>
                                          <div class='form-group'>
                                            <div class='col-md-4 text-left'>
                                              <label id='RecordStatusLabel' data-i18n="[html]app.form.RecordStatus_Label" class='control-label' for='RecordStatus'>Estado del registro</label><span id='RecordStatusRequired' class='required-mark'>*</span>
                                            </div>
                                            <div class='col-md-8'>
                                                <select id='RecordStatus' name='RecordStatus' class='form-control' data-i18n="[title]app.form.RecordStatus_Tooltip" title='Estado del registro.'>
                                                </select>

                                            </div>
                                          </div>
                                        </div>
                                        <!-- CreatorUserCode numeric -->
                                        <div class='col-md-6 form-horizontal'>
                                          <div class='form-group'>
                                            <div class='col-md-4 text-left'>
                                              <label id='CreatorUserCodeLabel' data-i18n="[html]app.form.CreatorUserCode_Label" class='control-label hidden' for='CreatorUserCode'>Creado por</label><span id='CreatorUserCodeRequired' class='required-mark hidden'>*</span>
                                            </div>
                                            <div class='col-md-8'>
                                                <input class='form-control hidden' id='CreatorUserCode' data-i18n="[title]app.form.CreatorUserCode_Tooltip" name='CreatorUserCode' title='Código del usuario que crea el registro.' type='text' style='text-align: right'/>
                                            </div>
                                          </div>
                                        </div>
                                        <!-- CreationDate datepicker -->
                                        <div class='col-md-6 form-horizontal'>
                                          <div class='form-group'>
                                            <div class='col-md-4 text-left'>
                                              <label id='CreationDateLabel' data-i18n="[html]app.form.CreationDate_Label" class='control-label hidden' for='CreationDate'>Creado en</label><span id='CreationDateRequired' class='required-mark hidden'>*</span>
                                            </div>
                                            <div class='col-md-8'>
                                                <div class='input-group date hidden' id='CreationDate_group'>
                                                    <input id='CreationDate' name='CreationDate' type='text' class='form-control hidden' title='Fecha del computador en que se crea el registro.' size='19' data-i18n="[title]app.form.CreationDate_Tooltip" maxlength='19'/>
                                                    <span class='input-group-addon'>
                                                        <span class='glyphicon glyphicon-calendar'></span>
                                                    </span>
                                                </div>
                                            </div>
                                          </div>
                                        </div>
                                        <!-- UpdateUserCode numeric -->
                                        <div class='col-md-6 form-horizontal'>
                                          <div class='form-group'>
                                            <div class='col-md-4 text-left'>
                                              <label id='UpdateUserCodeLabel' data-i18n="[html]app.form.UpdateUserCode_Label" class='control-label hidden' for='UpdateUserCode'>Última actualización por</label><span id='UpdateUserCodeRequired' class='required-mark hidden'>*</span>
                                            </div>
                                            <div class='col-md-8'>
                                                <input class='form-control hidden' id='UpdateUserCode' data-i18n="[title]app.form.UpdateUserCode_Tooltip" name='UpdateUserCode' title='Usuario que actualizó por última vez el registro.' type='text' style='text-align: right'/>
                                            </div>
                                          </div>
                                        </div>
                                        <!-- UpdateDate datepicker -->
                                        <div class='col-md-6 form-horizontal'>
                                          <div class='form-group'>
                                            <div class='col-md-4 text-left'>
                                              <label id='UpdateDateLabel' data-i18n="[html]app.form.UpdateDate_Label" class='control-label hidden' for='UpdateDate'>Última actualización en</label><span id='UpdateDateRequired' class='required-mark hidden'>*</span>
                                            </div>
                                            <div class='col-md-8'>
                                                <div class='input-group date hidden' id='UpdateDate_group'>
                                                    <input id='UpdateDate' name='UpdateDate' type='text' class='form-control hidden' title='Fecha que se actualizó por última vez el registro.' size='19' data-i18n="[title]app.form.UpdateDate_Tooltip" maxlength='19'/>
                                                    <span class='input-group-addon'>
                                                        <span class='glyphicon glyphicon-calendar'></span>
                                                    </span>
                                                </div>
                                            </div>
                                          </div>
                                        </div>
                                        <!-- Description text -->
                                        <div class='col-md-6 form-horizontal'>
                                          <div class='form-group'>
                                            <div class='col-md-4 text-left'>
                                              <label id='DescriptionLabel' data-i18n="[html]app.form.Description_Label" class='control-label' for='Description'>Descripción</label><span id='DescriptionRequired' class='required-mark'>*</span>
                                            </div>
                                            <div class='col-md-8'>
                                                <input class='form-control' id='Description' data-i18n="[title]app.form.Description_Tooltip;[placeholder]app.form.Description_Watermark" name='Description' title='Descripción del código.' type='text' size='60' maxlength='60'/>
                                            </div>
                                          </div>
                                        </div>
                                        <!-- ShortDescription text -->
                                        <div class='col-md-6 form-horizontal'>
                                          <div class='form-group'>
                                            <div class='col-md-4 text-left'>
                                              <label id='ShortDescriptionLabel' data-i18n="[html]app.form.ShortDescription_Label" class='control-label' for='ShortDescription'>Descripción breve</label><span id='ShortDescriptionRequired' class='required-mark'>*</span>
                                            </div>
                                            <div class='col-md-8'>
                                                <input class='form-control' id='ShortDescription' data-i18n="[title]app.form.ShortDescription_Tooltip;[placeholder]app.form.ShortDescription_Watermark" name='ShortDescription' title='Descripción abreviada del código.' type='text' size='20' maxlength='20'/>
                                            </div>
                                          </div>
                                        </div>
                            <!-- End Container content -->

                        </form>
                      </div>
                    </div>
                    <div class="modal-footer">
                        <button id="TabDecisionType_GridSaveBtn"  data-i18n="app.form.GridViewPopup_SaveButtonCaption"  class="btn btn-warning">Guardar</button>
                        <button class="btn"  data-i18n="app.form.GridViewPopup_CancelButtonCaption"  data-dismiss="modal" aria-hidden="true">Cancelar</button>
                    </div>
                </div>
            </div>
        </div>
        <div id='TabDecisionTypeTranslator_GridPopup' class='modal fade' tabindex='-1' role='dialog' aria-labelledby='edit' aria-hidden='true'>
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                        <h4 class="modal-title"><span data-i18n="app.form.GridViewPopup_TitleCaption">Información</span></h4>
                    </div>
                    <div class="modal-body">
                      <div class="row">
                        <form id='TabDecisionTypeTranslator_GridEditForm'>
                            <!-- Container content -->
                                        <!-- DecisionTranslator numeric -->
                                        <div class='col-md-6 form-horizontal'>
                                          <div class='form-group'>
                                            <div class='col-md-4 text-left'>
                                              <label id='DecisionTranslatorLabel' data-i18n="[html]app.form.DecisionTranslator_Label" class='control-label' for='DecisionTranslator'>Decisión</label><span id='DecisionTranslatorRequired' class='required-mark'>*</span>
                                            </div>
                                            <div class='col-md-8'>
                                                <input class='form-control' id='DecisionTranslator' data-i18n="[title]app.form.DecisionTranslator_Tooltip" name='DecisionTranslator' title='Código de la decisión del caso de suscripción' type='text' style='text-align: right'/>
                                            </div>
                                          </div>
                                        </div>
                                        <!-- LanguageIdTranslator dropdownlist -->
                                        <div class='col-md-6 form-horizontal'>
                                          <div class='form-group'>
                                            <div class='col-md-4 text-left'>
                                              <label id='LanguageIdTranslatorLabel' data-i18n="[html]app.form.LanguageIdTranslator_Label" class='control-label' for='LanguageIdTranslator'>Idioma</label><span id='LanguageIdTranslatorRequired' class='required-mark'>*</span>
                                            </div>
                                            <div class='col-md-8'>
                                                <select id='LanguageIdTranslator' name='LanguageIdTranslator' class='form-control' data-i18n="[title]app.form.LanguageIdTranslator_Tooltip" title='Idioma en el que se encuentra la descripción'>
                                                </select>

                                            </div>
                                          </div>
                                        </div>
                                        <!-- DescriptionTranslator text -->
                                        <div class='col-md-6 form-horizontal'>
                                          <div class='form-group'>
                                            <div class='col-md-4 text-left'>
                                              <label id='DescriptionTranslatorLabel' data-i18n="[html]app.form.DescriptionTranslator_Label" class='control-label' for='DescriptionTranslator'>Descripción</label><span id='DescriptionTranslatorRequired' class='required-mark'>*</span>
                                            </div>
                                            <div class='col-md-8'>
                                                <input class='form-control' id='DescriptionTranslator' data-i18n="[title]app.form.DescriptionTranslator_Tooltip;[placeholder]app.form.DescriptionTranslator_Watermark" name='DescriptionTranslator' title='Descripción del código.' type='text' size='60' maxlength='60'/>
                                            </div>
                                          </div>
                                        </div>
                                        <!-- ShortDescriptionTranslator text -->
                                        <div class='col-md-6 form-horizontal'>
                                          <div class='form-group'>
                                            <div class='col-md-4 text-left'>
                                              <label id='ShortDescriptionTranslatorLabel' data-i18n="[html]app.form.ShortDescriptionTranslator_Label" class='control-label' for='ShortDescriptionTranslator'>Descripción breve</label><span id='ShortDescriptionTranslatorRequired' class='required-mark'>*</span>
                                            </div>
                                            <div class='col-md-8'>
                                                <input class='form-control' id='ShortDescriptionTranslator' data-i18n="[title]app.form.ShortDescriptionTranslator_Tooltip;[placeholder]app.form.ShortDescriptionTranslator_Watermark" name='ShortDescriptionTranslator' title='Descripción abreviada del código.' type='text' size='20' maxlength='20'/>
                                            </div>
                                          </div>
                                        </div>
                            <!-- End Container content -->

                        </form>
                      </div>
                    </div>
                    <div class="modal-footer">
                        <button id="TabDecisionTypeTranslator_GridSaveBtn"  data-i18n="app.form.GridViewPopup_SaveButtonCaption"  class="btn btn-warning">Guardar</button>
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
    <script src='/fasi/app/js/TableHelper.js?rel=20191108042952341'></script>
    <script src='/fasi/assets/js/bootstrap-table-export.js'></script>
    <script src='/fasi/assets/js/tableExport.min.js'></script>
    <script src='/fasi/assets/js/autoNumeric.min.js'></script>
    <script src='/fasi/assets/js/i18next.min.js'></script>


    <script src="H5MantTipoDeDecision.js?rel=20191108042952341"></script>
 
</asp:Content>