<%@ Page Language="VB" MasterPageFile="~/fasi/FASI.master" AutoEventWireup="false" %>
<%@ MasterType TypeName="FASI" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <!-- This code was generated by Form Designer v7.3.11.1 at 2019-07-18 05:18:19 p. m. model release 3, Form Generator v1.0.35.45 -->
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
        <form id="H5MantTipoRequisitosMainForm">
            <input type="hidden" id="H5MantTipoRequisitosFormId" name="H5MantTipoRequisitosFormId" value="" />
            <div id="alerts-container"></div>
			<div class="row">
            <!-- Container content -->
                        <!-- Container: TabRequirementType grid -->
                        <div  class='col-md-12'>

                       <div id='TabRequirementTypeContainer'>
                            <div id="TabRequirementTypetoolbar">
                            <div class='form-inline'>
                                <button id="TabRequirementTypeCreateBtn" type="button" data-i18n="app.form.TabRequirementType_AddButtonCaption;[data-modal-title]app.form.TabRequirementType_AddButtonCaption"  class="btn btn-default" data-modal-title="Agregar">
                                    Agregar
                                </button>
                                <button id="TabRequirementTypeRemoveBtn"  data-i18n="app.form.TabRequirementType_DeleteButtonCaption;[data-title]app.form.TabRequirementType_DeleteButtonCaption"  type="button" class="btn btn-danger" disabled data-title="Eliminar">
                                    <i class="glyphicon glyphicon-remove"></i>Eliminar
                                </button>
                <!-- ShowTranslatorGrid button -->
                  <div class='form-group'>
                    <button id='ShowTranslatorGrid' class='ladda-button btn pull-left btn-default' data-style='expand-right' title='Mostrar Vista Traducción' data-i18n="app.form.ShowTranslatorGrid_Caption;[title]app.form.ShowTranslatorGrid_Tooltip" ><span class='ladda-label'>Vista Traducción</span><span class='ladda-spinner'></span></button>

                  </div>
                            </div>
                            </div>
                            <div id='TabRequirementTypeTblPlaceHolder'></div>
                       </div>

                        </div>
                        <!-- End Container: TabRequirementType grid -->
                        <!-- Container: TabRequirementTypeTranslator_Grid grid -->
                        <div  class='col-md-12'>

                       <div id='TabRequirementTypeTranslator_GridContainer' class='hidden'>
                            <div id="TabRequirementTypeTranslator_Gridtoolbar">
                            <div class='form-inline'>
                <!-- ShowStandardGrid button -->
                  <div class='form-group'>
                    <button id='ShowStandardGrid' class='ladda-button btn pull-left btn-default' data-style='expand-right' title='Mostrar Vista Estándar' data-i18n="app.form.ShowStandardGrid_Caption;[title]app.form.ShowStandardGrid_Tooltip" ><span class='ladda-label'>Vista Estándar</span><span class='ladda-spinner'></span></button>

                  </div>
                            </div>
                            </div>
                            <div id='TabRequirementTypeTranslator_GridTblPlaceHolder' class='hidden'></div>
                       </div>

                        </div>
                        <!-- End Container: TabRequirementTypeTranslator_Grid grid -->
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



        <div id='TabRequirementTypePopup' class='modal fade' tabindex='-1' role='dialog' aria-labelledby='edit' aria-hidden='true'>
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                        <h4 class="modal-title"><span data-i18n="app.form.GridViewPopup_TitleCaption">Información</span></h4>
                    </div>
                    <div class="modal-body">
                      <div class="row">
                        <form id='TabRequirementTypeEditForm'>
                            <!-- Container content -->
                                        <!-- RequirementType numeric -->
                                        <div class='col-md-6 form-horizontal'>
                                          <div class='form-group'>
                                            <div class='col-md-4 text-left'>
                                              <label id='RequirementTypeLabel' data-i18n="[html]app.form.RequirementType_Label" class='control-label' for='RequirementType'>Tipo requisito</label><span id='RequirementTypeRequired' class='required-mark'>*</span>
                                            </div>
                                            <div class='col-md-8'>
                                                <input class='form-control' id='RequirementType' data-i18n="[title]app.form.RequirementType_Tooltip" name='RequirementType' title='Tipo requisito.' type='text' style='text-align: right'/>
                                            </div>
                                          </div>
                                        </div>
                                        <!-- ProcessType numeric -->
                                        <div class='col-md-6 form-horizontal'>
                                          <div class='form-group'>
                                            <div class='col-md-4 text-left'>
                                              <label id='ProcessTypeLabel' data-i18n="[html]app.form.ProcessType_Label" class='control-label' for='ProcessType'>Procesado por</label><span id='ProcessTypeRequired' class='required-mark'>*</span>
                                            </div>
                                            <div class='col-md-8'>
                                                <input class='form-control' id='ProcessType' data-i18n="[title]app.form.ProcessType_Tooltip" name='ProcessType' title='Tipo de Forma en que se procesa un requisito: por un wf, por un humano, etc' type='text' style='text-align: right'/>
                                            </div>
                                          </div>
                                        </div>
                                        <!-- UnderwritingArea numeric -->
                                        <div class='col-md-6 form-horizontal'>
                                          <div class='form-group'>
                                            <div class='col-md-4 text-left'>
                                              <label id='UnderwritingAreaLabel' data-i18n="[html]app.form.UnderwritingArea_Label" class='control-label' for='UnderwritingArea'>Área de suscripción</label><span id='UnderwritingAreaRequired' class='required-mark'>*</span>
                                            </div>
                                            <div class='col-md-8'>
                                                <input class='form-control' id='UnderwritingArea' data-i18n="[title]app.form.UnderwritingArea_Tooltip" name='UnderwritingArea' title='Área de suscripción: financiera, médica, etc' type='text' style='text-align: right'/>
                                            </div>
                                          </div>
                                        </div>
                                        <!-- Payer numeric -->
                                        <div class='col-md-6 form-horizontal'>
                                          <div class='form-group'>
                                            <div class='col-md-4 text-left'>
                                              <label id='PayerLabel' data-i18n="[html]app.form.Payer_Label" class='control-label' for='Payer'>Pagador</label><span id='PayerRequired' class='required-mark'>*</span>
                                            </div>
                                            <div class='col-md-8'>
                                                <input class='form-control' id='Payer' data-i18n="[title]app.form.Payer_Tooltip" name='Payer' title='Pagador del requisito' type='text' style='text-align: right'/>
                                            </div>
                                          </div>
                                        </div>
                                        <!-- Cost numeric -->
                                        <div class='col-md-6 form-horizontal'>
                                          <div class='form-group'>
                                            <div class='col-md-4 text-left'>
                                              <label id='CostLabel' data-i18n="[html]app.form.Cost_Label" class='control-label' for='Cost'>Costo</label>
                                            </div>
                                            <div class='col-md-8'>
                                                <input class='form-control' id='Cost' data-i18n="[title]app.form.Cost_Tooltip" name='Cost' title='Costo del requisito' type='text' style='text-align: right'/>
                                            </div>
                                          </div>
                                        </div>
                                        <!-- Link text -->
                                        <div class='col-md-6 form-horizontal'>
                                          <div class='form-group'>
                                            <div class='col-md-4 text-left'>
                                              <label id='LinkLabel' data-i18n="[html]app.form.Link_Label" class='control-label' for='Link'>Url</label>
                                            </div>
                                            <div class='col-md-8'>
                                                <input class='form-control' id='Link' data-i18n="[title]app.form.Link_Tooltip;[placeholder]app.form.Link_Watermark" name='Link' title='Url de la planilla del requisito' type='text' size='256' maxlength='256'/>
                                            </div>
                                          </div>
                                        </div>
                                        <!-- AcordRequirementCode numeric -->
                                        <div class='col-md-6 form-horizontal'>
                                          <div class='form-group'>
                                            <div class='col-md-4 text-left'>
                                              <label id='AcordRequirementCodeLabel' data-i18n="[html]app.form.AcordRequirementCode_Label" class='control-label' for='AcordRequirementCode'>Código Acord</label>
                                            </div>
                                            <div class='col-md-8'>
                                                <input class='form-control' id='AcordRequirementCode' data-i18n="[title]app.form.AcordRequirementCode_Tooltip" name='AcordRequirementCode' title='Código Requisito Acord.' type='text' style='text-align: right'/>
                                            </div>
                                          </div>
                                        </div>
                                        <!-- Product dropdownlist -->
                                        <div class='col-md-6 form-horizontal'>
                                          <div class='form-group'>
                                            <div class='col-md-4 text-left'>
                                              <label id='ProductLabel' data-i18n="[html]app.form.Product_Label" class='control-label' for='Product'>Producto</label>
                                            </div>
                                            <div class='col-md-8'>
                                                <select id='Product' name='Product' class='form-control' data-i18n="[title]app.form.Product_Tooltip" title='Código del producto'>
                                                </select>

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
                                        <!-- VerDocumentInt numeric -->
                                        <div class='col-md-6 form-horizontal'>
                                          <div class='form-group'>
                                            <div class='col-md-4 text-left'>
                                              <label id='VerDocumentIntLabel' data-i18n="[html]app.form.VerDocumentInt_Label" class='control-label hidden' for='VerDocumentInt'>VerDocumentInt</label>
                                            </div>
                                            <div class='col-md-8'>
                                                <input class='form-control hidden' id='VerDocumentInt' data-i18n="[title]app.form.VerDocumentInt_Tooltip" name='VerDocumentInt' title='numeric3' type='text' style='text-align: right'/>
                                            </div>
                                          </div>
                                        </div>
                                        <!-- CargaDocumentoInt numeric -->
                                        <div class='col-md-6 form-horizontal'>
                                          <div class='form-group'>
                                            <div class='col-md-4 text-left'>
                                              <label id='CargaDocumentoIntLabel' data-i18n="[html]app.form.CargaDocumentoInt_Label" class='control-label hidden' for='CargaDocumentoInt'>CargaDocumentoInt</label>
                                            </div>
                                            <div class='col-md-8'>
                                                <input class='form-control hidden' id='CargaDocumentoInt' data-i18n="[title]app.form.CargaDocumentoInt_Tooltip" name='CargaDocumentoInt' title='numeric5' type='text' style='text-align: right'/>
                                            </div>
                                          </div>
                                        </div>
                                        <!-- AllowLoadRequirement dropdownlist -->
                                        <div class='col-md-6 form-horizontal'>
                                          <div class='form-group'>
                                            <div class='col-md-4 text-left'>
                                              <label id='AllowLoadRequirementLabel' data-i18n="[html]app.form.AllowLoadRequirement_Label" class='control-label' for='AllowLoadRequirement'>Mostrar botón para cargar un documento</label>
                                            </div>
                                            <div class='col-md-8'>
                                                <select id='AllowLoadRequirement' name='AllowLoadRequirement' class='form-control' data-i18n="[title]app.form.AllowLoadRequirement_Tooltip" title='Mostrar botón para cargar un documento'>
                                                    <option value='1' data-i18n="app.form.AllowLoadRequirement_1_Display" >Si</option>
                                                    <option value='2' data-i18n="app.form.AllowLoadRequirement_2_Display" >No</option>
                                                </select>

                                            </div>
                                          </div>
                                        </div>
                                        <!-- AllowViewRequirement dropdownlist -->
                                        <div class='col-md-6 form-horizontal'>
                                          <div class='form-group'>
                                            <div class='col-md-4 text-left'>
                                              <label id='AllowViewRequirementLabel' data-i18n="[html]app.form.AllowViewRequirement_Label" class='control-label' for='AllowViewRequirement'>Mostrar botón para visualizar un documento</label>
                                            </div>
                                            <div class='col-md-8'>
                                                <select id='AllowViewRequirement' name='AllowViewRequirement' class='form-control' data-i18n="[title]app.form.AllowViewRequirement_Tooltip" title='Mostrar botón para visualizar un documento'>
                                                    <option value='1' data-i18n="app.form.AllowViewRequirement_1_Display" >Si</option>
                                                    <option value='2' data-i18n="app.form.AllowViewRequirement_2_Display" >No</option>
                                                </select>

                                            </div>
                                          </div>
                                        </div>
                                        <!-- LineOfBusiness dropdownlist -->
                                        <div class='col-md-6 form-horizontal'>
                                          <div class='form-group'>
                                            <div class='col-md-4 text-left'>
                                              <label id='LineOfBusinessLabel' data-i18n="[html]app.form.LineOfBusiness_Label" class='control-label' for='LineOfBusiness'>Ramo</label>
                                            </div>
                                            <div class='col-md-8'>
                                                <select id='LineOfBusiness' name='LineOfBusiness' class='form-control' data-i18n="[title]app.form.LineOfBusiness_Tooltip" title='Ramo'>
                                                </select>

                                            </div>
                                          </div>
                                        </div>
                            <!-- End Container content -->

                        </form>
                      </div>
                    </div>
                    <div class="modal-footer">
                        <button id="TabRequirementTypeSaveBtn"  data-i18n="app.form.GridViewPopup_SaveButtonCaption"  class="btn btn-warning">Guardar</button>
                        <button class="btn"  data-i18n="app.form.GridViewPopup_CancelButtonCaption"  data-dismiss="modal" aria-hidden="true">Cancelar</button>
                    </div>
                </div>
            </div>
        </div>
        <div id='TabRequirementTypeTranslator_GridPopup' class='modal fade' tabindex='-1' role='dialog' aria-labelledby='edit' aria-hidden='true'>
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                        <h4 class="modal-title"><span data-i18n="app.form.GridViewPopup_TitleCaption">Información</span></h4>
                    </div>
                    <div class="modal-body">
                      <div class="row">
                        <form id='TabRequirementTypeTranslator_GridEditForm'>
                            <!-- Container content -->
                                        <!-- RequirementTypeTranslator numeric -->
                                        <div class='col-md-6 form-horizontal'>
                                          <div class='form-group'>
                                            <div class='col-md-4 text-left'>
                                              <label id='RequirementTypeTranslatorLabel' data-i18n="[html]app.form.RequirementTypeTranslator_Label" class='control-label' for='RequirementTypeTranslator'>Tipo requisito</label><span id='RequirementTypeTranslatorRequired' class='required-mark'>*</span>
                                            </div>
                                            <div class='col-md-8'>
                                                <input class='form-control' id='RequirementTypeTranslator' data-i18n="[title]app.form.RequirementTypeTranslator_Tooltip" name='RequirementTypeTranslator' title='Tipo de Requisito' type='text' style='text-align: right'/>
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
                        <button id="TabRequirementTypeTranslator_GridSaveBtn"  data-i18n="app.form.GridViewPopup_SaveButtonCaption"  class="btn btn-warning">Guardar</button>
                        <button class="btn"  data-i18n="app.form.GridViewPopup_CancelButtonCaption"  data-dismiss="modal" aria-hidden="true">Cancelar</button>
                    </div>
                </div>
            </div>
        </div>


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
    <script src='/fasi/app/js/TableHelper.js?rel=20190718051819629'></script>
    <script src='/fasi/assets/js/bootstrap-table-export.js'></script>
    <script src='/fasi/assets/js/tableExport.min.js'></script>
    <script src='/fasi/assets/js/autoNumeric.min.js'></script>
    <script src='/fasi/assets/js/i18next.min.js'></script>


    <script src="H5MantTipoRequisitos.js?rel=20190718051819629"></script>
 
</asp:Content>