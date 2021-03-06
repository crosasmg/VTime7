<%@ Page Language="VB" MasterPageFile="~/fasi/FASI.master" AutoEventWireup="false" %>
<%@ MasterType TypeName="FASI" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <!-- This code was generated by Form Designer v7.3.36.1 at 2020-03-18 09:54:53 AM model release 8, Form Generator v1.0.37.37 -->
    <!-- Changes to this file may cause incorrect behavior and will be lost if the code is regenerated. -->
    
    <!-- Bootstrap -->
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
        <form id="NavigationWidgetMainForm">
            <input type="hidden" id="NavigationWidgetFormId" name="NavigationWidgetFormId" value="" />
            <div id="alerts-container"></div>
			<div class="row">
            <!-- Container content -->
                        <!-- Container: NAVIGATIONDIRECTORY_Grid grid -->
                        <div  class='col-md-12'>

                       <div id='NAVIGATIONDIRECTORY_GridContainer'>
                            <div id="NAVIGATIONDIRECTORY_Gridtoolbar">
                            <div class='form-inline'>
                                <button id="NAVIGATIONDIRECTORY_GridCreateBtn" type="button" data-i18n="app.form.NAVIGATIONDIRECTORY_Grid_AddButtonCaption;[data-modal-title]app.form.NAVIGATIONDIRECTORY_Grid_AddButtonCaption"  class="btn btn-default" data-modal-title="Agregar">
                                    Agregar
                                </button>
                                <button id="NAVIGATIONDIRECTORY_GridRemoveBtn"  data-i18n="app.form.NAVIGATIONDIRECTORY_Grid_DeleteButtonCaption;[data-title]app.form.NAVIGATIONDIRECTORY_Grid_DeleteButtonCaption"  type="button" class="btn btn-danger" disabled data-title="Eliminar">
                                    <i class="glyphicon glyphicon-remove"></i>Eliminar
                                </button>
                <!-- ShowTranslatorGrid button -->
                  <div class='form-group'>
                    <button id='ShowTranslatorGrid' class='ladda-button btn pull-left btn-info' data-style='expand-right' title='Mostrar Vista Traducción' data-i18n="app.form.ShowTranslatorGrid_Caption;[title]app.form.ShowTranslatorGrid_Tooltip" ><span class='ladda-label'>Vista Traducción</span><span class='ladda-spinner'></span></button>

                  </div>
                            </div>
                            </div>
                            <div id='NAVIGATIONDIRECTORY_GridTblPlaceHolder'></div>
                       </div>

                        </div>
                        <!-- End Container: NAVIGATIONDIRECTORY_Grid grid -->
                        <!-- Container: NAVIGATIONDIRECTORYTranslator_Grid grid -->
                        <div  class='col-md-12'>

                       <div id='NAVIGATIONDIRECTORYTranslator_GridContainer' class='hidden'>
                            <div id="NAVIGATIONDIRECTORYTranslator_Gridtoolbar">
                            <div class='form-inline'>
                <!-- ShowStandardGrid button -->
                  <div class='form-group'>
                    <button id='ShowStandardGrid' class='ladda-button btn pull-left btn-info' data-style='expand-right' title='Mostrar Vista Estándar' data-i18n="app.form.ShowStandardGrid_Caption;[title]app.form.ShowStandardGrid_Tooltip" ><span class='ladda-label'>Vista Estándar</span><span class='ladda-spinner'></span></button>

                  </div>
                            </div>
                            </div>
                            <div id='NAVIGATIONDIRECTORYTranslator_GridTblPlaceHolder' class='hidden'></div>
                       </div>

                        </div>
                        <!-- End Container: NAVIGATIONDIRECTORYTranslator_Grid grid -->
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



        <div id='NAVIGATIONDIRECTORY_GridPopup' class='modal fade' tabindex='-1' role='dialog' aria-labelledby='edit' aria-hidden='true'>
            <div class="modal-dialog modal-lg">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                        <h4 class="modal-title"><span data-i18n="app.form.GridViewPopup_TitleCaption">Información</span></h4>
                    </div>
                    <div class="modal-body">
                      <div class="row">
                        <form id='NAVIGATIONDIRECTORY_GridEditForm'>
                            <!-- Container content -->
                                        <!-- ID numeric -->
                                        <div class='col-md-6 form-horizontal'>
                                          <div class='form-group'>
                                            <div class='col-md-4 text-left'>
                                              <label id='IDLabel' data-i18n="[html]app.form.ID_Label" class='control-label' for='ID'>Identificador</label><span id='IDRequired' class='required-mark'>*</span>
                                            </div>
                                            <div class='col-md-8'>
                                                <input class='form-control' id='ID' data-i18n="[title]app.form.ID_Tooltip" name='ID' title='Identificador' type='text' style='text-align: right'/>
                                            </div>
                                          </div>
                                        </div>
                                        <!-- NAME text -->
                                        <div class='col-md-6 form-horizontal'>
                                          <div class='form-group'>
                                            <div class='col-md-4 text-left'>
                                              <label id='NAMELabel' data-i18n="[html]app.form.NAME_Label" class='control-label' for='NAME'>Nombre</label><span id='NAMERequired' class='required-mark'>*</span>
                                            </div>
                                            <div class='col-md-8'>
                                                <input class='form-control' id='NAME' data-i18n="[title]app.form.NAME_Tooltip;[placeholder]app.form.NAME_Watermark" name='NAME' title='Nombre' type='text' size='255' maxlength='255'/>
                                            </div>
                                          </div>
                                        </div>
                                        <!-- TITLE text -->
                                        <div class='col-md-6 form-horizontal'>
                                          <div class='form-group'>
                                            <div class='col-md-4 text-left'>
                                              <label id='TITLELabel' data-i18n="[html]app.form.TITLE_Label" class='control-label' for='TITLE'>Título</label>
                                            </div>
                                            <div class='col-md-8'>
                                                <input class='form-control' id='TITLE' data-i18n="[title]app.form.TITLE_Tooltip;[placeholder]app.form.TITLE_Watermark" name='TITLE' title='Título' type='text' size='120' maxlength='120'/>
                                            </div>
                                          </div>
                                        </div>
                                        <!-- DESCRIPTION text -->
                                        <div class='col-md-6 form-horizontal'>
                                          <div class='form-group'>
                                            <div class='col-md-4 text-left'>
                                              <label id='DESCRIPTIONLabel' data-i18n="[html]app.form.DESCRIPTION_Label" class='control-label' for='DESCRIPTION'>Descripción</label>
                                            </div>
                                            <div class='col-md-8'>
                                                <input class='form-control' id='DESCRIPTION' data-i18n="[title]app.form.DESCRIPTION_Tooltip;[placeholder]app.form.DESCRIPTION_Watermark" name='DESCRIPTION' title='Descripción' type='text' size='255' maxlength='255'/>
                                            </div>
                                          </div>
                                        </div>
                                        <!-- URLPATH text -->
                                        <div class='col-md-6 form-horizontal'>
                                          <div class='form-group'>
                                            <div class='col-md-4 text-left'>
                                              <label id='URLPATHLabel' data-i18n="[html]app.form.URLPATH_Label" class='control-label' for='URLPATH'>Ruta</label><span id='URLPATHRequired' class='required-mark'>*</span>
                                            </div>
                                            <div class='col-md-8'>
                                                <input class='form-control' id='URLPATH' data-i18n="[title]app.form.URLPATH_Tooltip;[placeholder]app.form.URLPATH_Watermark" name='URLPATH' title='Ruta' type='text' size='255' maxlength='255'/>
                                            </div>
                                          </div>
                                        </div>
                                        <!-- CATEGORYCODE dropdownlist -->
                                        <div class='col-md-6 form-horizontal'>
                                          <div class='form-group'>
                                            <div class='col-md-4 text-left'>
                                              <label id='CATEGORYCODELabel' data-i18n="[html]app.form.CATEGORYCODE_Label" class='control-label' for='CATEGORYCODE'>Categoría</label>
                                            </div>
                                            <div class='col-md-8'>
                                                <select id='CATEGORYCODE' name='CATEGORYCODE' class='form-control' data-i18n="[title]app.form.CATEGORYCODE_Tooltip" title='Código de Categoría'>
                                                </select>

                                            </div>
                                          </div>
                                        </div>
                                        <!-- IMAGEFILE text -->
                                        <div class='col-md-6 form-horizontal'>
                                          <div class='form-group'>
                                            <div class='col-md-4 text-left'>
                                              <label id='IMAGEFILELabel' data-i18n="[html]app.form.IMAGEFILE_Label" class='control-label' for='IMAGEFILE'>Imagen</label>
                                            </div>
                                            <div class='col-md-8'>
                                                <input class='form-control' id='IMAGEFILE' data-i18n="[title]app.form.IMAGEFILE_Tooltip;[placeholder]app.form.IMAGEFILE_Watermark" name='IMAGEFILE' title='Archivo Imagen' type='text' size='50' maxlength='50'/>
                                            </div>
                                          </div>
                                        </div>
                                        <!-- STATUS dropdownlist -->
                                        <div class='col-md-6 form-horizontal'>
                                          <div class='form-group'>
                                            <div class='col-md-4 text-left'>
                                              <label id='STATUSLabel' data-i18n="[html]app.form.STATUS_Label" class='control-label' for='STATUS'>Estado</label><span id='STATUSRequired' class='required-mark'>*</span>
                                            </div>
                                            <div class='col-md-8'>
                                                <select id='STATUS' name='STATUS' class='form-control' data-i18n="[title]app.form.STATUS_Tooltip" title='Estado'>
                                                </select>

                                            </div>
                                          </div>
                                        </div>
                                        <!-- MODELID text -->
                                        <div class='col-md-6 form-horizontal'>
                                          <div class='form-group'>
                                            <div class='col-md-4 text-left'>
                                              <label id='MODELIDLabel' data-i18n="[html]app.form.MODELID_Label" class='control-label hidden' for='MODELID'>Modelo</label>
                                            </div>
                                            <div class='col-md-8'>
                                                <input class='form-control hidden' id='MODELID' data-i18n="[title]app.form.MODELID_Tooltip;[placeholder]app.form.MODELID_Watermark" name='MODELID' title='' type='text' size='36' maxlength='36'/>
                                            </div>
                                          </div>
                                        </div>
                            <!-- End Container content -->

                        </form>
                      </div>
                    </div>
                    <div class="modal-footer">
                        <button id="NAVIGATIONDIRECTORY_GridSaveBtn"  data-i18n="app.form.GridViewPopup_SaveButtonCaption"  class="btn btn-warning">Guardar</button>
                        <button class="btn"  data-i18n="app.form.GridViewPopup_CancelButtonCaption"  data-dismiss="modal" aria-hidden="true">Cancelar</button>
                    </div>
                </div>
            </div>
        </div>
        <div id='NAVIGATIONDIRECTORYTranslator_GridPopup' class='modal fade' tabindex='-1' role='dialog' aria-labelledby='edit' aria-hidden='true'>
            <div class="modal-dialog modal-lg">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                        <h4 class="modal-title"><span data-i18n="app.form.GridViewPopup_TitleCaption">Información</span></h4>
                    </div>
                    <div class="modal-body">
                      <div class="row">
                        <form id='NAVIGATIONDIRECTORYTranslator_GridEditForm'>
                            <!-- Container content -->
                                        <!-- IDTranslator numeric -->
                                        <div class='col-md-6 form-horizontal'>
                                          <div class='form-group'>
                                            <div class='col-md-4 text-left'>
                                              <label id='IDTranslatorLabel' data-i18n="[html]app.form.IDTranslator_Label" class='control-label' for='IDTranslator'>Identificador</label><span id='IDTranslatorRequired' class='required-mark'>*</span>
                                            </div>
                                            <div class='col-md-8'>
                                                <input class='form-control' id='IDTranslator' data-i18n="[title]app.form.IDTranslator_Tooltip" name='IDTranslator' title='Identificador' type='text' style='text-align: right'/>
                                            </div>
                                          </div>
                                        </div>
                                        <!-- NAMETranslator text -->
                                        <div class='col-md-6 form-horizontal'>
                                          <div class='form-group'>
                                            <div class='col-md-4 text-left'>
                                              <label id='NAMETranslatorLabel' data-i18n="[html]app.form.NAMETranslator_Label" class='control-label' for='NAMETranslator'>Nombre</label><span id='NAMETranslatorRequired' class='required-mark'>*</span>
                                            </div>
                                            <div class='col-md-8'>
                                                <input class='form-control' id='NAMETranslator' data-i18n="[title]app.form.NAMETranslator_Tooltip;[placeholder]app.form.NAMETranslator_Watermark" name='NAMETranslator' title='Nombre' type='text' size='255' maxlength='255'/>
                                            </div>
                                          </div>
                                        </div>
                                        <!-- TITLETranslator text -->
                                        <div class='col-md-6 form-horizontal'>
                                          <div class='form-group'>
                                            <div class='col-md-4 text-left'>
                                              <label id='TITLETranslatorLabel' data-i18n="[html]app.form.TITLETranslator_Label" class='control-label' for='TITLETranslator'>Título</label>
                                            </div>
                                            <div class='col-md-8'>
                                                <input class='form-control' id='TITLETranslator' data-i18n="[title]app.form.TITLETranslator_Tooltip;[placeholder]app.form.TITLETranslator_Watermark" name='TITLETranslator' title='Título' type='text' size='120' maxlength='120'/>
                                            </div>
                                          </div>
                                        </div>
                                        <!-- DESCRIPTIONTranslator text -->
                                        <div class='col-md-6 form-horizontal'>
                                          <div class='form-group'>
                                            <div class='col-md-4 text-left'>
                                              <label id='DESCRIPTIONTranslatorLabel' data-i18n="[html]app.form.DESCRIPTIONTranslator_Label" class='control-label' for='DESCRIPTIONTranslator'>Descripción</label>
                                            </div>
                                            <div class='col-md-8'>
                                                <input class='form-control' id='DESCRIPTIONTranslator' data-i18n="[title]app.form.DESCRIPTIONTranslator_Tooltip;[placeholder]app.form.DESCRIPTIONTranslator_Watermark" name='DESCRIPTIONTranslator' title='Descripción' type='text' size='255' maxlength='255'/>
                                            </div>
                                          </div>
                                        </div>
                                        <!-- URLPATHTranslator text -->
                                        <div class='col-md-6 form-horizontal'>
                                          <div class='form-group'>
                                            <div class='col-md-4 text-left'>
                                              <label id='URLPATHTranslatorLabel' data-i18n="[html]app.form.URLPATHTranslator_Label" class='control-label' for='URLPATHTranslator'>Ruta</label><span id='URLPATHTranslatorRequired' class='required-mark'>*</span>
                                            </div>
                                            <div class='col-md-8'>
                                                <input class='form-control' id='URLPATHTranslator' data-i18n="[title]app.form.URLPATHTranslator_Tooltip;[placeholder]app.form.URLPATHTranslator_Watermark" name='URLPATHTranslator' title='Ruta' type='text' size='255' maxlength='255'/>
                                            </div>
                                          </div>
                                        </div>
                                        <!-- CATEGORYCODETranslator dropdownlist -->
                                        <div class='col-md-6 form-horizontal'>
                                          <div class='form-group'>
                                            <div class='col-md-4 text-left'>
                                              <label id='CATEGORYCODETranslatorLabel' data-i18n="[html]app.form.CATEGORYCODETranslator_Label" class='control-label' for='CATEGORYCODETranslator'>Categoría</label>
                                            </div>
                                            <div class='col-md-8'>
                                                <select id='CATEGORYCODETranslator' name='CATEGORYCODETranslator' class='form-control' data-i18n="[title]app.form.CATEGORYCODETranslator_Tooltip" title='Código de Categoría'>
                                                </select>

                                            </div>
                                          </div>
                                        </div>
                                        <!-- IMAGEFILETranslator text -->
                                        <div class='col-md-6 form-horizontal'>
                                          <div class='form-group'>
                                            <div class='col-md-4 text-left'>
                                              <label id='IMAGEFILETranslatorLabel' data-i18n="[html]app.form.IMAGEFILETranslator_Label" class='control-label' for='IMAGEFILETranslator'>Imagen</label>
                                            </div>
                                            <div class='col-md-8'>
                                                <input class='form-control' id='IMAGEFILETranslator' data-i18n="[title]app.form.IMAGEFILETranslator_Tooltip;[placeholder]app.form.IMAGEFILETranslator_Watermark" name='IMAGEFILETranslator' title='Archivo Imagen' type='text' size='50' maxlength='50'/>
                                            </div>
                                          </div>
                                        </div>
                                        <!-- STATUSTranslator dropdownlist -->
                                        <div class='col-md-6 form-horizontal'>
                                          <div class='form-group'>
                                            <div class='col-md-4 text-left'>
                                              <label id='STATUSTranslatorLabel' data-i18n="[html]app.form.STATUSTranslator_Label" class='control-label' for='STATUSTranslator'>Estado</label><span id='STATUSTranslatorRequired' class='required-mark'>*</span>
                                            </div>
                                            <div class='col-md-8'>
                                                <select id='STATUSTranslator' name='STATUSTranslator' class='form-control' data-i18n="[title]app.form.STATUSTranslator_Tooltip" title='Estado'>
                                                </select>

                                            </div>
                                          </div>
                                        </div>
                                        <!-- LANGUAGEIDTranslator dropdownlist -->
                                        <div class='col-md-6 form-horizontal'>
                                          <div class='form-group'>
                                            <div class='col-md-4 text-left'>
                                              <label id='LANGUAGEIDTranslatorLabel' data-i18n="[html]app.form.LANGUAGEIDTranslator_Label" class='control-label' for='LANGUAGEIDTranslator'>Lenguaje</label><span id='LANGUAGEIDTranslatorRequired' class='required-mark'>*</span>
                                            </div>
                                            <div class='col-md-8'>
                                                <select id='LANGUAGEIDTranslator' name='LANGUAGEIDTranslator' class='form-control' data-i18n="[title]app.form.LANGUAGEIDTranslator_Tooltip" title='Identificador del Lenguaje'>
                                                </select>

                                            </div>
                                          </div>
                                        </div>
                                        <!-- MODELIDTranslator text -->
                                        <div class='col-md-6 form-horizontal'>
                                          <div class='form-group'>
                                            <div class='col-md-4 text-left'>
                                              <label id='MODELIDTranslatorLabel' data-i18n="[html]app.form.MODELIDTranslator_Label" class='control-label hidden' for='MODELIDTranslator'>Modelo</label>
                                            </div>
                                            <div class='col-md-8'>
                                                <input class='form-control hidden' id='MODELIDTranslator' data-i18n="[title]app.form.MODELIDTranslator_Tooltip;[placeholder]app.form.MODELIDTranslator_Watermark" name='MODELIDTranslator' title='' type='text' size='36' maxlength='36'/>
                                            </div>
                                          </div>
                                        </div>
                            <!-- End Container content -->

                        </form>
                      </div>
                    </div>
                    <div class="modal-footer">
                        <button id="NAVIGATIONDIRECTORYTranslator_GridSaveBtn"  data-i18n="app.form.GridViewPopup_SaveButtonCaption"  class="btn btn-warning">Guardar</button>
                        <button class="btn"  data-i18n="app.form.GridViewPopup_CancelButtonCaption"  data-dismiss="modal" aria-hidden="true">Cancelar</button>
                    </div>
                </div>
            </div>
        </div>


    </div>
  </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footer" runat="Server">
    <script src='/fasi/assets/js/bootstrap-table.min.js'></script>
    <script src='/fasi/assets/js/bootstrap-table-es-CR.min.js'></script>
    <script src='/fasi/app/js/TableHelper.js?rel=20200318095453718'></script>
    <script src='/fasi/assets/js/bootstrap-table-export.js'></script>
    <script src='/fasi/assets/js/tableExport.min.js'></script>
    <script src='/fasi/assets/js/xlsx.core.min.js'></script>
    <script src='/fasi/assets/js/autoNumeric.min.js'></script>
    <script src='/fasi/assets/js/i18next.min.js'></script>


    <script src="NavigationWidget.js?rel=20200318095453718"></script>
 
</asp:Content>