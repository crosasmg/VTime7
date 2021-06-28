﻿<%@ Page Language="VB" MasterPageFile="~/fasi/FASI.master" AutoEventWireup="false" %>
<%@ MasterType TypeName="FASI" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <!-- This code was generated by Form Designer v7.1.213.1 at 2018/11/19 10:45:02 AM model release 2, Form Generator v1.0.33.4 -->
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
        <form id="HT5NNCotizacionPolizaHogar1BasicoMainForm">
            <input type="hidden" id="HT5NNCotizacionPolizaHogar1BasicoFormId" name="HT5NNCotizacionPolizaHogar1BasicoFormId" value="" />
            <div id="alerts-container"></div>
			<div class="row">
            <!-- Container content -->
                        <!-- Container: zone1 zone -->
                        <div  class='col-md-12'>

                  <div id="zone1" class='panel panel-default'>
                    <div class='panel-heading' title=''></div>
                 <div class="panel-body">
    <!-- Container content -->
                <!-- EffectiveDate datepicker -->
                <div class='col-md-3 form-horizontal'>
                  <div class='form-group'>
                    <div class='col-md-4 text-left'>
                      <label id='EffectiveDateLabel' class='control-label' for='EffectiveDate'>Fecha de efecto</label>
                    </div>
                    <div class='col-md-8'>
                        <div class='input-group date' id='EffectiveDate_group'>
                                    <input id='EffectiveDate' name='EffectiveDate' type='text' class='form-control' title='Fecha de efecto (inicio de vigencia) de la póliza o certificado.' size='10' maxlength='10' disabled/>
                                    <span class='input-group-addon'>
                                        <span class='glyphicon glyphicon-calendar'></span>
                                    </span>
                                </div>
                    </div>
                  </div>
                </div>
                <!-- ProductCode dropdownlist -->
                <div class='col-md-3 form-horizontal'>
                  <div class='form-group'>
                    <div class='col-md-4 text-left'>
                      <label id='ProductCodeLabel' class='control-label' for='ProductCode'>Plan</label><span id='ProductCodeRequired' class='required-mark'>*</span>
                    </div>
                    <div class='col-md-8'>
                        <select id='ProductCode' name='ProductCode' class='form-control' title='Código del producto.' disabled='disabled'>
                                </select>

                    </div>
                  </div>
                </div>
                <!-- uwcaseid text -->
                <div class='col-md-3 form-horizontal'>
                  <div class='form-group'>
                    <div class='col-md-4 text-left'>
                      <label id='uwcaseidLabel' class='control-label hidden' for='uwcaseid'>Caso en tratamiento</label>
                    </div>
                    <div class='col-md-8'>
                        <p id='uwcaseid' name='uwcaseid' class='form-control-static@Control.Class@'></p>
                    </div>
                  </div>
                </div>
                <!-- LineOfBusiness dropdownlist -->
                <div class='col-md-3 form-horizontal'>
                  <div class='form-group'>
                    <div class='col-md-4 text-left'>
                      <label id='LineOfBusinessLabel' class='control-label hidden' for='LineOfBusiness'>Ramo</label><span id='LineOfBusinessRequired' class='required-mark hidden'>*</span>
                    </div>
                    <div class='col-md-8'>
                        <select id='LineOfBusiness' name='LineOfBusiness' class='form-control hidden' title='Código del ramo comercial.'>
                                </select>

                    </div>
                  </div>
                </div>
    <!-- End Container content -->
                 </div>
               </div>
                        </div>
                        <!-- End Container: zone1 zone -->
                        <!-- Container: zone8 zone -->
                        <div  class='col-md-12'>

                  <div id="zone8">
    <!-- Container content -->
                <!-- Container: zone10 zone -->
                <div  class='col-md-6'>

                  <div id="zone10">
    <!-- Container content -->
                <!-- Container: NNCotizacionPolizaHogarSecuencia SubPage -->
                <div  class='col-md-12'>

    <!-- Container content -->
    <!-- End Container content -->
                </div>
                <!-- End Container: NNCotizacionPolizaHogarSecuencia SubPage -->
    <!-- End Container content -->
               </div>
                </div>
                <!-- End Container: zone10 zone -->
                <!-- Container: zone2 zone -->
                <div  class='col-md-6'>

                  <div id="zone2" class='panel panel-default'>
                    <div class='panel-heading' title=''></div>
                 <div class="panel-body">
    <!-- Container content -->
                <!-- label0 label -->
                <div class='col-md-12 form-vertical'>
                  <div class='form-group text-center'>
                                  <label id='label0' title='' style='font-weight: bold; font-size: 12px;'>INFORMACIÓN BÁSICA</label>

                  </div>
                </div>
                <!-- Container: zone14 zone -->
                <div  class='col-md-12'>

                  <div id="zone14">
    <!-- Container content -->
                <!-- label18 label -->
                <div class='col-md-6 form-vertical'>
                  <div class='form-group text-left'>
                                  <label id='label18' title='label18' style='font-weight: bold; font-size: 10px;'>Información general</label>

                  </div>
                </div>
                <!-- label22 label -->
                <div class='col-md-6 form-vertical'>
                  <div class='form-group text-left'>
                                  <label id='label22' title='' style='font-weight: bold; font-size: 10px;'>Capitales básicos</label>

                  </div>
                </div>
                <!-- Container: zone0Gen zone -->
                <div  class='col-md-6'>

                  <div id="zone0Gen">
    <!-- Container content -->
                <!-- YearBuilt dropdownlist -->
                <div class='col-md-6 form-vertical'>
                  <div class='form-group'>
                    <label id='YearBuiltLabel' class='control-label' for='YearBuilt'>Año de construcción</label><span id='YearBuiltRequired' class='required-mark'>*</span>
                        <select id='YearBuilt' name='YearBuilt' class='form-control' title='Año de Construcción'>
                                </select>

                  </div>
                </div>
                <!-- DwellingType dropdownlist -->
                <div class='col-md-6 form-vertical'>
                  <div class='form-group'>
                    <label id='DwellingTypeLabel' class='control-label' for='DwellingType'>Tipo de vivienda</label><span id='DwellingTypeRequired' class='required-mark'>*</span>
                        <select id='DwellingType' name='DwellingType' class='form-control' title='Tipo de Vivienda'>
                                </select>

                  </div>
                </div>
                <!-- ConstructionMaterial dropdownlist -->
                <div class='col-md-6 form-vertical'>
                  <div class='form-group'>
                    <label id='ConstructionMaterialLabel' class='control-label' for='ConstructionMaterial'>Material de construcción</label><span id='ConstructionMaterialRequired' class='required-mark'>*</span>
                        <select id='ConstructionMaterial' name='ConstructionMaterial' class='form-control' title='Material de Construcción'>
                                </select>

                  </div>
                </div>
                <!-- Foundation dropdownlist -->
                <div class='col-md-6 form-vertical'>
                  <div class='form-group'>
                    <label id='FoundationLabel' class='control-label' for='Foundation'>Fundación</label><span id='FoundationRequired' class='required-mark'>*</span>
                        <select id='Foundation' name='Foundation' class='form-control' title='Fundación'>
                                </select>

                  </div>
                </div>
                <!-- RoofType dropdownlist -->
                <div class='col-md-6 form-vertical'>
                  <div class='form-group'>
                    <label id='RoofTypeLabel' class='control-label' for='RoofType'>Tipo de techo</label><span id='RoofTypeRequired' class='required-mark'>*</span>
                        <select id='RoofType' name='RoofType' class='form-control' title='Tipo de Techo'>
                                </select>

                  </div>
                </div>
                <!-- Stories numeric -->
                <div class='col-md-6 form-vertical'>
                  <div class='form-group'>
                    <label id='StoriesLabel' class='control-label' for='Stories'>Pisos</label><span id='StoriesRequired' class='required-mark'>*</span>
                        <input class='form-control' id='Stories' name='Stories' title='Pisos' type='text' style='text-align: right'/>
                  </div>
                </div>
                <!-- Area numeric -->
                <div class='col-md-6 form-vertical'>
                  <div class='form-group'>
                    <label id='AreaLabel' class='control-label' for='Area'>Superficie</label><span id='AreaRequired' class='required-mark'>*</span>
                        <input class='form-control' id='Area' name='Area' title='Superficie' type='text' style='text-align: right'/>
                  </div>
                </div>
                <!-- LandArea numeric -->
                <div class='col-md-6 form-vertical'>
                  <div class='form-group'>
                    <label id='LandAreaLabel' class='control-label' for='LandArea'>Superficie del terreno</label><span id='LandAreaRequired' class='required-mark'>*</span>
                        <input class='form-control' id='LandArea' name='LandArea' title='Superficie del Terreno' type='text' style='text-align: right'/>
                  </div>
                </div>
    <!-- End Container content -->
               </div>
                </div>
                <!-- End Container: zone0Gen zone -->
                <!-- Container: zone9 zone -->
                <div  class='col-md-6'>

                  <div id="zone9">
    <!-- Container content -->
                <!-- Currency dropdownlist -->
                <div class='col-md-6 form-vertical'>
                  <div class='form-group'>
                    <label id='CurrencyLabel' class='control-label' for='Currency'>Moneda</label>
                        <select id='Currency' name='Currency' class='form-control' title='' disabled='disabled'>
                                </select>

                  </div>
                </div>
                <!-- InsuredValueEstructura numeric -->
                <div class='col-md-6 form-vertical'>
                  <div class='form-group'>
                    <label id='InsuredValueEstructuraLabel' class='control-label' for='InsuredValueEstructura'>Estructura</label>
                        <input class='form-control' id='InsuredValueEstructura' name='InsuredValueEstructura' title='Valor Asegurado' type='text' style='text-align: right'/>
                  </div>
                </div>
                <!-- InsuredValueContenido numeric -->
                <div class='col-md-6 form-vertical'>
                  <div class='form-group'>
                    <label id='InsuredValueContenidoLabel' class='control-label' for='InsuredValueContenido'>Contenido</label>
                        <input class='form-control' id='InsuredValueContenido' name='InsuredValueContenido' title='Valor Asegurado' type='text' style='text-align: right'/>
                  </div>
                </div>
    <!-- End Container content -->
               </div>
                </div>
                <!-- End Container: zone9 zone -->
                <!-- label23 label -->
                <div class='col-md-6 form-vertical'>
                  <div class='form-group text-left'>
                                  <label id='label23' title='' style='font-weight: bold; font-size: 10px;'>Módulos</label>

                  </div>
                </div>
                <!-- label24 label -->
                <div class='col-md-6 form-vertical'>
                  <div class='form-group text-left'>
                                  <label id='label24' title='label24' style='font-weight: bold; font-size: 10px;'>Coberturas y prima</label>

                  </div>
                </div>
                <!-- Container: zone10Modulos zone -->
                <div  class='col-md-6'>

                  <div id="zone10Modulos">
    <!-- Container content -->
                <!-- ModuleSelected CheckBoxList -->
                <div class='col-md-12 form-horizontal'>
                  <div class='form-group'>
                    <div class='col-md-4 text-left'>
                      <label id='ModuleSelectedLabel' class='control-label' for='ModuleSelected'></label>
                    </div>
                    <div class='col-md-8'>
                        <div id='ModuleSelected'></div>

                    </div>
                  </div>
                </div>
    <!-- End Container content -->
               </div>
                </div>
                <!-- End Container: zone10Modulos zone -->
                <!-- Container: ZoneModulosCoberturas zone -->
                <div  class='col-md-6'>

                  <div id="ZoneModulosCoberturas">
    <!-- Container content -->
                <!-- Container: TcoverCollection zone -->
                <div  class='col-md-4'>

                  <div id="TcoverCollection" class='hidden' >
    <!-- Container content -->
                <!-- Container: CoverageWithCalculatedPremium grid -->
                <div  class='col-md-12'>

               <div id='CoverageWithCalculatedPremiumContainer'>
                    <div id='CoverageWithCalculatedPremiumTblPlaceHolder'></div>
               </div>

                </div>
                <!-- End Container: CoverageWithCalculatedPremium grid -->
                <!-- TotalOriginalAnnualPremium numeric -->
                <div class='col-md-12 form-horizontal'>
                  <div class='form-group'>
                    <div class='col-md-4 text-right'>
                      <label id='TotalOriginalAnnualPremiumLabel' class='control-label' style='font-weight: bold; font-size: 10px;' for='TotalOriginalAnnualPremium'>Prima total anual</label>
                    </div>
                    <div class='col-md-8'>
                        <p id='TotalOriginalAnnualPremium' name='TotalOriginalAnnualPremium' class='form-control-static text-right'></p>
                    </div>
                  </div>
                </div>
    <!-- End Container content -->
               </div>
                </div>
                <!-- End Container: TcoverCollection zone -->
    <!-- End Container content -->
               </div>
                </div>
                <!-- End Container: ZoneModulosCoberturas zone -->
    <!-- End Container content -->
               </div>
                </div>
                <!-- End Container: zone14 zone -->
                <!-- Container: zone13F1Basico zone -->
                <div  class='col-md-12'>

                  <div id="zone13F1Basico">
    <!-- Container content -->
                <!-- label20 label -->
                <div class='col-md-6 form-vertical'>
                  <div class='form-group text-left'>
                                  <label id='label20' title='label20'>Una vez que incluya toda la información requerida, presione el botón 'Cotizar' para que pueda visualizar las coberturas y la prima.</label>

                  </div>
                </div>
                <!-- button0 button -->
                <div class='col-md-6 form-vertical'>
                  <div class='form-group'>
                    <button id='button0' class='ladda-button btn pull-right btn-default' data-style='expand-right' title=''  style='font-weight: bold; font-size: 10px; background-color: #BFBFBF;'><span class='ladda-label'>
Cotizar   <img src='/images/Library/16x16_ASPNetIcons/settings1_16x16.gif' id='button0Image' /></span><span class='ladda-spinner'></span></button>

                  </div>
                </div>
                <!-- button5 button -->
                <div class='col-md-6 form-vertical'>
                  <div class='form-group'>
                    <button id='button5' class='ladda-button btn pull-right hidden btn-default' data-style='expand-right' title=''  style='font-weight: bold; font-size: 10px; background-color: #BFBFBF;'><span class='ladda-label'>
Guardar y seguir   <img src='/images/Library/16x16_ASPNetIcons/ok_16x16.gif' id='button5Image' /></span><span class='ladda-spinner'></span></button>

                  </div>
                </div>
    <!-- End Container content -->
               </div>
                </div>
                <!-- End Container: zone13F1Basico zone -->
                <!-- FinalMessageLabel label -->
                <div class='col-md-12 form-vertical'>
                  <div class='form-group text-left'>
                                  <label id='FinalMessageLabel' title=''>FinalMessage</label>

                  </div>
                </div>
    <!-- End Container content -->
                 </div>
               </div>
                </div>
                <!-- End Container: zone2 zone -->
    <!-- End Container content -->
               </div>
                        </div>
                        <!-- End Container: zone8 zone -->
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
    <script src="/fasi/app/js/checkboxlist.js?rel=20181119104502656"></script>
    <script src='/fasi/assets/js/bootstrap-datetimepicker.min.js'></script>
    <script src='/fasi/assets/js/bootstrap-table.min.js'></script>
    <script src='/fasi/assets/js/bootstrap-table-es-CR.min.js'></script>
    <script src='/fasi/app/js/TableHelper.js?rel=20181119104502656'></script>
    <script src='/fasi/assets/js/autoNumeric.min.js'></script>


    <script src="HT5NNCotizacionPolizaHogar1Basico.js?rel=20181119104502656"></script>
 
</asp:Content>