﻿<%@ Page Language="VB" MasterPageFile="~/fasi/FASI.master" AutoEventWireup="false" %>
<%@ MasterType TypeName="FASI" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <!-- This code was generated by Form Designer v7.3.34.1 at 2020-01-22 11:31:36 a. m. model release 4, Form Generator v1.0.37.30 -->
    <!-- Changes to this file may cause incorrect behavior and will be lost if the code is regenerated. -->
    
    <!-- Bootstrap -->

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
        <form id="H5CotizacionesTodosLosRamosIconosMainForm">
            <input type="hidden" id="H5CotizacionesTodosLosRamosIconosFormId" name="H5CotizacionesTodosLosRamosIconosFormId" value="" />
            <div id="alerts-container"></div>
			<div class="row">
            <!-- Container content -->
                        <!-- Container: zone3 zone -->
                        <div  class='col-md-12'>

                  <div id="zone3">
    <!-- Container content -->
                <!-- label6 label -->
                <div class='col-md-12 form-vertical'>
                  <div class='form-group text-center'>
                                  <label id='label6' title='' style='font-weight: bold; font-size: 14px;'>Nosotros asumimos los riesgos  y garantizamos su tranquilidad</label>

                  </div>
                </div>
                <!-- CompleteClientNameCTLR text -->
                        <div class='col-md-12 margin-top-1'></div>
                <div class='col-md-12 form-horizontal'>
                  <div class='form-group'>
                    <div class='col-md-4 text-center'>
                      <label id='CompleteClientNameCTLRLabel' class='control-label' for='CompleteClientNameCTLR'></label>
                    </div>
                    <div class='col-md-8'>
                        <p id='CompleteClientNameCTLR' name='CompleteClientNameCTLR' class='form-control-static'></p>
                    </div>
                  </div>
                </div>
                <!-- Container: zone12CTLR zone -->
                <div  class='col-md-12'>

                  <div id="zone12CTLR" class='hidden'>
    <!-- Container content -->
                <!-- ClientID Client -->
                <div class='col-md-12 form-horizontal'>
                  <div class='form-group'>
                    <div class='col-md-4 text-left'>
                      <label id='ClientIDLabel' class='control-label' for='ClientID'>Asegurado</label><span id='ClientIDRequired' class='required-mark'>*</span>
                    </div>
                    <div class='col-md-8'>
                        <select class='form-control' id='ClientID' name='ClientID' title='Asegurado'  ></select>
                    </div>
                  </div>
                </div>
    <!-- End Container content -->
               </div>
                </div>
                <!-- End Container: zone12CTLR zone -->
                <!-- Container: zone10AgenteCTLR zone -->
                <div  class='col-md-12'>

                  <div id="zone10AgenteCTLR">
    <!-- Container content -->
                <!-- ClienteProductor dropdownlist -->
                <div class='col-md-12 form-vertical'>
                  <div class='form-group'>
                    <label id='ClienteProductorLabel' class='control-label' for='ClienteProductor'>Asegurados del Agente</label>
                        <select id='ClienteProductor' name='ClienteProductor' class='form-control' title='Asegurado'>
                                </select>

                  </div>
                </div>
    <!-- End Container content -->
               </div>
                </div>
                <!-- End Container: zone10AgenteCTLR zone -->
                <!-- image9 Image -->
                <div class='col-md-12 form-horizontal'>
                  <div class='form-group'>
                    <div class='col-md-12 text-center'>
                                     <img src='' id='image9' class='img-responsive center-block  hidden' alt=''/>

                    </div>
                  </div>
                </div>
                <!-- btnCotizarFinal button -->
                <div class='col-md-12 form-vertical'>
                  <div class='form-group'>
                    <button id='btnCotizarFinal' class='ladda-button btn center-block hidden btn-danger' data-style='expand-right' title='Cotización en línea del producto seleccionado' ><span class='ladda-label'>Iniciar cotización</span><span class='ladda-spinner'></span></button>

                  </div>
                </div>
    <!-- End Container content -->
               </div>
                        </div>
                        <!-- End Container: zone3 zone -->
                        <!-- Container: zone0CTLR zone -->
                        <div  class='col-md-12'>

                  <div id="zone0CTLR">
    <!-- Container content -->
                <!-- Container: zone3CTLR zone -->
                <div  class='col-md-2'>

                  <div id="zone3CTLR">
    <!-- Container content -->
                <!-- button3MVVNN button -->
                <div class='col-md-12 form-vertical'>
                  <div class='form-group'>
                    <button id='button3MVVNN' class='ladda-button btn center-block btn-default' data-style='expand-right' title='Cotización en línea de Mi Vida Vale' ><span class='ladda-label'></span>
   <img src='/images/WidgetIconRamos/2.png' id='button3MVVNNImage' /><span class='ladda-spinner'></span></button>

                  </div>
                </div>
                <!-- label0 label -->
                <div class='col-md-12 form-vertical'>
                  <div class='form-group text-center'>
                                  <label id='label0' title=''>Mi Vida Vale</label>

                  </div>
                </div>
    <!-- End Container content -->
               </div>
                </div>
                <!-- End Container: zone3CTLR zone -->
                <!-- Container: zone32CTLR zone -->
                <div  class='col-md-2'>

                  <div id="zone32CTLR">
    <!-- Container content -->
                <!-- buttonCotVI button -->
                <div class='col-md-12 form-vertical'>
                  <div class='form-group'>
                    <button id='buttonCotVI' class='ladda-button btn center-block btn-default' data-style='expand-right' title='Cotización en línea de Mi Inversión Segura' ><span class='ladda-label'></span>
   <img src='/images/WidgetIconRamos/family-care.png' id='buttonCotVIImage' /><span class='ladda-spinner'></span></button>

                  </div>
                </div>
                <!-- label1 label -->
                <div class='col-md-12 form-vertical'>
                  <div class='form-group text-center'>
                                  <label id='label1' title=''>Mi Inversión Segura</label>

                  </div>
                </div>
    <!-- End Container content -->
               </div>
                </div>
                <!-- End Container: zone32CTLR zone -->
                <!-- Container: zone5CTLR zone -->
                <div  class='col-md-2'>

                  <div id="zone5CTLR">
    <!-- Container content -->
                <!-- CotizaMAD button -->
                <div class='col-md-12 form-vertical'>
                  <div class='form-group'>
                    <button id='CotizaMAD' class='ladda-button btn center-block btn-default' data-style='expand-right' title='Cotización en línea de Mi Auro aDorado' ><span class='ladda-label'></span>
   <img src='/images/WidgetIconRamos/6.png' id='CotizaMADImage' /><span class='ladda-spinner'></span></button>

                  </div>
                </div>
                <!-- label2 label -->
                <div class='col-md-12 form-vertical'>
                  <div class='form-group text-center'>
                                  <label id='label2' title=''>Mi Auto aDorado</label>

                  </div>
                </div>
    <!-- End Container content -->
               </div>
                </div>
                <!-- End Container: zone5CTLR zone -->
                <!-- Container: zone6CTLR zone -->
                <div  class='col-md-2'>

                  <div id="zone6CTLR">
    <!-- Container content -->
                <!-- CotizaHV button -->
                <div class='col-md-12 form-vertical'>
                  <div class='form-group'>
                    <button id='CotizaHV' class='ladda-button btn center-block btn-default' data-style='expand-right' title='Cotización en línea de Póliza Hogar Seguro' ><span class='ladda-label'></span>
   <img src='/images/WidgetIconRamos/9.png' id='CotizaHVImage' /><span class='ladda-spinner'></span></button>

                  </div>
                </div>
                <!-- label3 label -->
                <div class='col-md-12 form-vertical'>
                  <div class='form-group text-center'>
                                  <label id='label3' title=''>Póliza Hogar Seguro</label>

                  </div>
                </div>
    <!-- End Container content -->
               </div>
                </div>
                <!-- End Container: zone6CTLR zone -->
                <!-- Container: zone9CTLR zone -->
                <div  class='col-md-2'>

                  <div id="zone9CTLR">
    <!-- Container content -->
                <!-- CotizaHV2 button -->
                <div class='col-md-12 form-vertical'>
                  <div class='form-group'>
                    <button id='CotizaHV2' class='ladda-button btn center-block btn-default' data-style='expand-right' title='Cotización en línea de Fianzas' ><span class='ladda-label'></span>
   <img src='/images/WidgetIconRamos/Crédito.png' id='CotizaHV2Image' /><span class='ladda-spinner'></span></button>

                  </div>
                </div>
                <!-- label4 label -->
                <div class='col-md-12 form-vertical'>
                  <div class='form-group text-center'>
                                  <label id='label4' title=''>Fianzas</label>

                  </div>
                </div>
    <!-- End Container content -->
               </div>
                </div>
                <!-- End Container: zone9CTLR zone -->
                <!-- Container: zone8 zone -->
                <div  class='col-md-2'>

                  <div id="zone8">
    <!-- Container content -->
                <!-- button13 button -->
                <div class='col-md-12 form-vertical'>
                  <div class='form-group'>
                    <button id='button13' class='ladda-button btn center-block btn-default' data-style='expand-right' title='button13' ><span class='ladda-label'></span>
   <img src='/images/WidgetIconRamos/5.png' id='button13Image' /><span class='ladda-spinner'></span></button>

                  </div>
                </div>
                <!-- label5 label -->
                <div class='col-md-12 form-vertical'>
                  <div class='form-group text-center'>
                                  <label id='label5' title=''>Mi Salud Vale Oro</label>

                  </div>
                </div>
    <!-- End Container content -->
               </div>
                </div>
                <!-- End Container: zone8 zone -->
    <!-- End Container content -->
               </div>
                        </div>
                        <!-- End Container: zone0CTLR zone -->
            <!-- End Container content -->

            </div>
        </form>





    </div>
  </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footer" runat="Server">
    <script src='/fasi/app/js/client.js?rel=20200122113136741'></script>


    <script src="H5CotizacionesTodosLosRamosIconos.js?rel=20200122113136741"></script>
 
</asp:Content>