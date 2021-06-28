﻿<%@ Page Language="VB" MasterPageFile="~/fasi/FASI.master" AutoEventWireup="false" %>
<%@ MasterType TypeName="FASI" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <!-- This code was generated by Form Designer v7.3.39.1 at 2020-04-20 04:11:31 PM model release 10, Form Generator v1.0.37.52 -->
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
        <form id="AccesosSeguridadMainForm">
            <input type="hidden" id="AccesosSeguridadFormId" name="AccesosSeguridadFormId" value="" />
            <div id="alerts-container"></div>
			<div class="row">
            <!-- Container content -->
                        <!-- label0 label -->
                        <div class='col-md-12 form-vertical'>
                          <div class='form-group text-left'>
                                          <label id='label0' data-i18n="[html]app.form.label0_Label;[title]app.form.label0_Tooltip" title='Rutas de acceso a los recursos del sistema' class='h3' >Rutas de acceso a los recursos del sistema</label>

                          </div>
                        </div>
                        <!-- Container: Acceso_Grid grid -->
                        <div  class='col-md-12'>

                       <div id='Acceso_GridContainer'>
                            <div id="Acceso_Gridtoolbar">
                            <div class='form-inline'>
                                <button id="Acceso_GridCreateBtn" type="button" data-i18n="app.form.Acceso_Grid_AddButtonCaption;[data-modal-title]app.form.Acceso_Grid_AddButtonCaption"  class="btn btn-default" data-modal-title="Agregar">
                                    Agregar
                                </button>
                                <button id="Acceso_GridRemoveBtn"  data-i18n="app.form.Acceso_Grid_DeleteButtonCaption;[data-title]app.form.Acceso_Grid_DeleteButtonCaption"  type="button" class="btn btn-danger" disabled data-title="Eliminar">
                                    <i class="glyphicon glyphicon-remove"></i>Eliminar
                                </button>
                            </div>
                            </div>
                            <div id='Acceso_GridTblPlaceHolder'></div>
                       </div>

                        </div>
                        <!-- End Container: Acceso_Grid grid -->
            <!-- End Container content -->

            </div>
        </form>



        <div id='Acceso_GridPopup' class='modal fade' tabindex='-1' role='dialog' aria-labelledby='edit' aria-hidden='true'>
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                        <h4 class="modal-title"><span data-i18n="app.form.GridViewPopup_TitleCaption">Información</span></h4>
                    </div>
                    <div class="modal-body">
                      <div class="row">
                        <form id='Acceso_GridEditForm'>
                            <!-- Container content -->
                                        <!-- Id_Acceso numeric -->
                                        <div class='col-md-6 form-horizontal'>
                                          <div class='form-group'>
                                            <div class='col-md-4 text-left'>
                                              <label id='Id_AccesoLabel' data-i18n="[html]app.form.Id_Acceso_Label" class='control-label hidden' for='Id_Acceso'>Id Acceso</label><span id='Id_AccesoRequired' class='required-mark hidden'>*</span>
                                            </div>
                                            <div class='col-md-8'>
                                                <input class='form-control hidden' id='Id_Acceso' data-i18n="[title]app.form.Id_Acceso_Tooltip" name='Id_Acceso' title='Código de acceso a los recursos.' type='text' style='text-align: right'/>
                                            </div>
                                          </div>
                                        </div>
                                        <!-- Id_Tipo_Acceso numeric -->
                                        <div class='col-md-6 form-horizontal'>
                                          <div class='form-group'>
                                            <div class='col-md-4 text-left'>
                                              <label id='Id_Tipo_AccesoLabel' data-i18n="[html]app.form.Id_Tipo_Acceso_Label" class='control-label hidden' for='Id_Tipo_Acceso'>Id Tipo Acceso</label><span id='Id_Tipo_AccesoRequired' class='required-mark hidden'>*</span>
                                            </div>
                                            <div class='col-md-8'>
                                                <input class='form-control hidden' id='Id_Tipo_Acceso' data-i18n="[title]app.form.Id_Tipo_Acceso_Tooltip" name='Id_Tipo_Acceso' title='Código de tipo de acceso.' type='text' style='text-align: right'/>
                                            </div>
                                          </div>
                                        </div>
                                        <!-- Descripcion text -->
                                        <div class='col-md-6 form-horizontal'>
                                          <div class='form-group'>
                                            <div class='col-md-4 text-left'>
                                              <label id='DescripcionLabel' data-i18n="[html]app.form.Descripcion_Label" class='control-label' for='Descripcion'>Descripción</label><span id='DescripcionRequired' class='required-mark'>*</span>
                                            </div>
                                            <div class='col-md-8'>
                                                <input class='form-control' id='Descripcion' data-i18n="[title]app.form.Descripcion_Tooltip;[placeholder]app.form.Descripcion_Watermark" name='Descripcion' title='Descripción del código.' type='text' size='200' maxlength='200'/>
                                            </div>
                                          </div>
                                        </div>
                                        <!-- Descripcion_Corta text -->
                                        <div class='col-md-6 form-horizontal'>
                                          <div class='form-group'>
                                            <div class='col-md-4 text-left'>
                                              <label id='Descripcion_CortaLabel' data-i18n="[html]app.form.Descripcion_Corta_Label" class='control-label' for='Descripcion_Corta'>Descripcion Corta</label><span id='Descripcion_CortaRequired' class='required-mark'>*</span>
                                            </div>
                                            <div class='col-md-8'>
                                                <input class='form-control' id='Descripcion_Corta' data-i18n="[title]app.form.Descripcion_Corta_Tooltip;[placeholder]app.form.Descripcion_Corta_Watermark" name='Descripcion_Corta' title='Descripción abreviada del código.' type='text' size='200' maxlength='200'/>
                                            </div>
                                          </div>
                                        </div>
                                        <!-- Estado_Registro dropdownlist -->
                                        <div class='col-md-6 form-horizontal'>
                                          <div class='form-group'>
                                            <div class='col-md-4 text-left'>
                                              <label id='Estado_RegistroLabel' data-i18n="[html]app.form.Estado_Registro_Label" class='control-label' for='Estado_Registro'>Estado Registro</label><span id='Estado_RegistroRequired' class='required-mark'>*</span>
                                            </div>
                                            <div class='col-md-8'>
                                                <select id='Estado_Registro' name='Estado_Registro' class='form-control' data-i18n="[title]app.form.Estado_Registro_Tooltip" title='Estado del registro.'>
                                                </select>

                                            </div>
                                          </div>
                                        </div>
                                        <!-- CreationDate datepicker -->
                                        <div class='col-md-6 form-horizontal'>
                                          <div class='form-group'>
                                            <div class='col-md-4 text-left'>
                                              <label id='CreationDateLabel' data-i18n="[html]app.form.CreationDate_Label" class='control-label hidden' for='CreationDate'>Fecha de Creación</label><span id='CreationDateRequired' class='required-mark hidden'>*</span>
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
                                        <!-- CreatorUserCode numeric -->
                                        <div class='col-md-6 form-horizontal'>
                                          <div class='form-group'>
                                            <div class='col-md-4 text-left'>
                                              <label id='CreatorUserCodeLabel' data-i18n="[html]app.form.CreatorUserCode_Label" class='control-label hidden' for='CreatorUserCode'>User Code Creador</label><span id='CreatorUserCodeRequired' class='required-mark hidden'>*</span>
                                            </div>
                                            <div class='col-md-8'>
                                                <input class='form-control hidden' id='CreatorUserCode' data-i18n="[title]app.form.CreatorUserCode_Tooltip" name='CreatorUserCode' title='Código del usuario que crea el registro.' type='text' style='text-align: right'/>
                                            </div>
                                          </div>
                                        </div>
                                        <!-- UpdateDate datepicker -->
                                        <div class='col-md-6 form-horizontal'>
                                          <div class='form-group'>
                                            <div class='col-md-4 text-left'>
                                              <label id='UpdateDateLabel' data-i18n="[html]app.form.UpdateDate_Label" class='control-label hidden' for='UpdateDate'>Fecha de Actualización del Registro</label><span id='UpdateDateRequired' class='required-mark hidden'>*</span>
                                            </div>
                                            <div class='col-md-8'>
                                                <div class='input-group date hidden' id='UpdateDate_group'>
                                                    <input id='UpdateDate' name='UpdateDate' type='text' class='form-control hidden' title='Fecha del computador en que se crea o actualiza el registro.' size='19' data-i18n="[title]app.form.UpdateDate_Tooltip" maxlength='19'/>
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
                                              <label id='UpdateUserCodeLabel' data-i18n="[html]app.form.UpdateUserCode_Label" class='control-label hidden' for='UpdateUserCode'>Código de Usuario Que Actualiza</label><span id='UpdateUserCodeRequired' class='required-mark hidden'>*</span>
                                            </div>
                                            <div class='col-md-8'>
                                                <input class='form-control hidden' id='UpdateUserCode' data-i18n="[title]app.form.UpdateUserCode_Tooltip" name='UpdateUserCode' title='Código del usuario que crea o actualiza el registro.' type='text' style='text-align: right'/>
                                            </div>
                                          </div>
                                        </div>
                            <!-- End Container content -->

                        </form>
                      </div>
                    </div>
                    <div class="modal-footer">
                        <button id="Acceso_GridSaveBtn"  data-i18n="app.form.GridViewPopup_SaveButtonCaption"  class="btn btn-warning">Guardar</button>
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
    <script src='/fasi/app/js/TableHelper.js?rel=20200420041131612'></script>
    <script src='/fasi/assets/js/bootstrap-table-export.js'></script>
    <script src='/fasi/assets/js/tableExport.min.js'></script>
    <script src='/fasi/assets/js/xlsx.core.min.js'></script>
    <script src='/fasi/assets/js/jspdf.min.js'></script>
    <script src='/fasi/assets/js/jspdf.plugin.autotable.min.js'></script>
    <script src='/fasi/assets/js/jquery.base64.js'></script>
    <script src='/fasi/assets/js/autoNumeric.min.js'></script>
    <script src='/fasi/assets/js/i18next.min.js'></script>


    <script src="AccesosSeguridad.js?rel=20200420041131612"></script>
 
</asp:Content>