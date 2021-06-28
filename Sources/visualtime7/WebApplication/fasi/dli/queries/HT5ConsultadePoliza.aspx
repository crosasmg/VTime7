﻿<%@ Page Language="VB" MasterPageFile="~/fasi/FASI.master" AutoEventWireup="false" %>
<%@ MasterType TypeName="FASI" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <!-- This code was generated by Query Designer v7.1.120.1 at 2019/03/01 02:25:41 p.m. model release 2, Form Generator v1.0.34.22 - Query Generator v1.0.16.8 -->
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
    <div class="ibox">
     <div class="ibox-content">
        <form id="HT5ConsultadePolizaMainForm">
            <input type="hidden" id="HT5ConsultadePolizaFormId" name="HT5ConsultadePolizaFormId" value="" />
            <div id="alerts-container"></div>
			<div class="row">
            <!-- Container content -->
                        <!-- Container: zoneHeader zone -->
                        <div  class='col-md-12'>

                  <div id="zoneHeader">
    <!-- Container content -->
                <!-- PolicyID numeric -->
                <div class='col-md-4 form-horizontal'>
                  <div class='form-group'>
                    <div class='col-md-4 text-left'>
                      <label id='PolicyIDLabel' class='control-label' for='PolicyID'>Póliza</label><span id='PolicyIDRequired' class='required-mark'>*</span>
                    </div>
                    <div class='col-md-8'>
                        <input class='form-control' id='PolicyID' name='PolicyID' title='Número de la póliza' type='text' style='text-align: right'/>
                    </div>
                  </div>
                </div>
                <!-- RecordEffectiveDate datepicker -->
                <div class='col-md-4 form-horizontal'>
                  <div class='form-group'>
                    <div class='col-md-4 text-left'>
                      <label id='RecordEffectiveDateLabel' class='control-label' for='RecordEffectiveDate'>Fecha de efecto</label><span id='RecordEffectiveDateRequired' class='required-mark'>*</span>
                    </div>
                    <div class='col-md-8'>
                        <div class='input-group date' id='RecordEffectiveDate_group'>
                                    <input id='RecordEffectiveDate' name='RecordEffectiveDate' type='text' class='form-control' title='Fecha de efecto de la información a mostrar' size='10' maxlength='10'/>
                                    <span class='input-group-addon'>
                                        <span class='glyphicon glyphicon-calendar'></span>
                                    </span>
                                </div>
                    </div>
                  </div>
                </div>
                <!-- btnOk button -->
                <div class='col-md-4 form-vertical'>
                  <div class='form-group'>
                    <button id='btnOk' class='ladda-button btn pull-left btn-primary' data-style='expand-right' title='Aceptar consulta' ><span class='ladda-label'>OK</span><span class='ladda-spinner'></span></button>

                  </div>
                </div>
    <!-- End Container content -->
               </div>
                        </div>
                        <!-- End Container: zoneHeader zone -->
                        <!-- Container: Items grid -->
                        <div  class='col-md-12'>

                       <div id='ItemsContainer'>
                            <div id='ItemsTblPlaceHolder'></div>
                       </div>


                    <!-- Lookups: zoneChildren Sub-Grid -->
                            <select id='REINSURAN2NCOMPANY' hidden='hidden'>
                            </select>

                    <!-- End Lookups: zoneChildren grid -->

                    <!-- Lookups: zoneChildren Sub-Grid -->
                            <select id='PREMIUMNTYPE' hidden='hidden'>
                                 <option value='1'>Cobro</option>
                                 <option value='2'>Devolución</option>
                            </select>

                    <!-- End Lookups: zoneChildren grid -->
                        </div>
                        <!-- End Container: Items grid -->
                        <!-- CERTIFICATActionLbl label -->
                        <div class='col-md-12 form-vertical'>
                          <div class='form-group text-left'>
                                          <label id='CERTIFICATActionLbl' title='Acciones disponibles'>Presione el icono <span class='caret'></span> para ver el menú de acciones disponibles asociadas al campo seleccionado.</label>

                          </div>
                        </div>
            <!-- End Container content -->

            </div>
        </form>




    <ul id='Items_CERTIFICATNPOLICYContextMenu' class='dropdown-menu'>
 <li data-item='Items_CERTIFICATNPOLICY_Item1'><a>Declarar siniestro</a></li>
    </ul>
    <ul id='ROLESContextMenu' class='dropdown-menu'>
        <li data-item='ROLES_Item1'><a>Consultar datos del cliente</a></li>
        <li data-item='ROLES_Item2'><a>Modificar datos del cliente</a></li>
    </ul>
    <ul id='ROLES_ROLESSCLIENTDescContextMenu' class='dropdown-menu'>
 <li data-item='ROLES_ROLESSCLIENTDesc_Item1'><a>Consultar datos del cliente</a></li>
 <li data-item='ROLES_ROLESSCLIENTDesc_Item2'><a>Modificar datos del cliente</a></li>
    </ul>

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
    <script src='/fasi/app/js/TableHelper.js?rel=20190301022541937'></script>
    <script src='/fasi/assets/js/bootstrap-table-contextmenu.js'></script>
    <script src='/fasi/assets/js/autoNumeric.min.js'></script>


    <script src="HT5ConsultadePoliza.js?rel=20190301022541937"></script>
 
</asp:Content>