﻿<%@ Page Language="VB" MasterPageFile="~/fasi/FASI.master" AutoEventWireup="false" %>
<%@ MasterType TypeName="FASI" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <!-- This code was generated by Form Designer v7.3.34.1 at 2020-01-17 02:21:14 p. m. model release 9, Form Generator v1.0.37.30 -->
    <!-- Changes to this file may cause incorrect behavior and will be lost if the code is regenerated. -->
    
    <!-- Bootstrap -->
    <link rel='stylesheet' href='/fasi/assets/css/bootstrap-datetimepicker.min.css' />

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
        <form id="AuthorizationToObtainDiscloseInformationUWMainForm">
            <input type="hidden" id="AuthorizationToObtainDiscloseInformationUWFormId" name="AuthorizationToObtainDiscloseInformationUWFormId" value="" />
            <div id="alerts-container"></div>
			<div class="row">
            <!-- Container content -->
                        <!-- Container: part1 zone -->
                        <div  class='col-md-12'>

                  <div id="part1" class='panel panel-default'>
                    <div class='panel-heading' title=''></div>
                 <div class="panel-body">
    <!-- Container content -->
                <!-- image0 Image -->
                <div class='col-md-6 form-horizontal'>
                  <div class='form-group'>
                    <div class='col-md-10'>
                                     <img src='/images/Banners/Life Insurance/3.jpg' id='image0' class='img-responsive ' alt='' width='40' />

                    </div>
                  </div>
                </div>
                <!-- ClientName text -->
                <div class='col-md-6 form-horizontal col-md-offset-6'>
                  <div class='form-group'>
                    <div class='col-md-4 text-left'>
                      <label id='ClientNameLabel' class='control-label' for='ClientName'>Solicitante del seguro</label>
                    </div>
                    <div class='col-md-8'>
                        <input class='form-control' id='ClientName' name='ClientName' title='First name of the proposed insured' type='text' size='30' maxlength='30'/>
                    </div>
                  </div>
                </div>
                <!-- uwcaseid text -->
                <div class='col-md-6 form-horizontal'>
                  <div class='form-group'>
                    <div class='col-md-4 text-left'>
                      <label id='uwcaseidLabel' class='control-label' for='uwcaseid'>Solicitud</label>
                    </div>
                    <div class='col-md-8'>
                        <input class='form-control' id='uwcaseid' name='uwcaseid' title='Middle name of the proposed insured' type='text' size='15' maxlength='15'/>
                    </div>
                  </div>
                </div>
    <!-- End Container content -->
                 </div>
               </div>
                        </div>
                        <!-- End Container: part1 zone -->
                        <!-- Container: PART2 zone -->
                        <div  class='col-md-12'>

                  <div id="PART2" class='panel panel-default'>
                    <div class='panel-heading' title=''>AUTORIZACIÓN</div>
                 <div class="panel-body">
    <!-- Container content -->
                <!-- label1 label -->
                <div class='col-md-12 form-vertical'>
                  <div class='form-group text-left'>
                                  <label id='label1' title=''>Entiendo que los siguientes partes pueden necesitar recopilar información sobre mí en cuanto a la cobertura propuesta: la empresa citada anteriormente, cualquier organización de apoyo de seguros, cualquier agencia de informes del consumidor, y todas las personas autorizadas para representar a estas organizaciones para este propósito. Aquellos sujetos que puedan necesitar recoger información puede revelar información a lo siguiente: otras compañías de seguros a la que el asegurado solicitante ha aplicado o pueden ser aplicables; reaseguradores, el MIB Group, Inc. o de las personas que realizan actividades empresariales, profesionales o las tareas de seguro para ellos. Podrán revelar la información según lo permitido o requerido por la ley. El MIB puede revelar información sólo según lo establecido en un acuerdo con una empresa u organización miembro. Yo autorizo ​​a la preparación de un informe del consumidor y un informe de investigación del consumidor acerca de mí y mis hijos si su nombre figura como Asegurados propuestos en esta política. Previa solicitud, puedo ser entrevistado como parte de esta solicitud. Además, previa solicitud por escrito, entiendo que tengo derecho a recibir una copia del informe investigativo del consumidor.</label>

                  </div>
                </div>
                <!-- label2 label -->
                        <div class='col-md-12 margin-top-2'></div>
                <div class='col-md-12 form-vertical'>
                  <div class='form-group text-left'>
                                  <label id='label2' title=''>La información que puede ser recopilada y revelada incluye: datos sobre mi salud mental o física y consumo de drogas o alcohol, otros seguros, actividades peligrosas, carácter, reputación general, el modo de vida, finanzas, registro de conducir, vocación y otras características personales. No incluye datos acerca de mi orientación sexual.</label>

                  </div>
                </div>
                <!-- label3 label -->
                <div class='col-md-12 form-vertical'>
                  <div class='form-group text-left'>
                                  <label id='label3' title=''>Al firmar a continuación, autorizo ​​a petición: cualquier médico o profesional médico, cualquier hospital, clínica u otra instalación relacionada con la medicina, y cualquier compañía de seguros, y cualquier agencia del Gobierno y cualquier otra organización, institución, empresa o persona que tenga registros o conocimiento en relación con la salud de un asegurado solicitante, los hábitos, el empleo, los ingresos y las finanzas si la Compañía nombrado arriba hacer una solicitud, para dar cualquiera de esos registros o conocimiento de: la empresa citada más arriba; sus reaseguradores, afiliados y productores, y los terceros que realizan servicios por la empresa citada anteriormente con el fin de suscribir, procesar reclamaciones y administrar cualquier póliza emitida y ofrecer productos y servicios financieros.</label>

                  </div>
                </div>
                <!-- label4 label -->
                        <div class='col-md-12 margin-top-2'></div>
                <div class='col-md-12 form-vertical'>
                  <div class='form-group text-left'>
                                  <label id='label4' title=''>Una copia de esta autorización será tan válida como el original.</label>

                  </div>
                </div>
                <!-- label5 label -->
                        <div class='col-md-12 margin-top-1'></div>
                <div class='col-md-12 form-vertical'>
                  <div class='form-group text-left'>
                                  <label id='label5' title=''>Entiendo que yo o mi representante autorizado puede solicitar recibir una copia de esta autorización. Reconozco que he recibido una copia del Aviso al Asegurado Solicitante.</label>

                  </div>
                </div>
                <!-- label6 label -->
                        <div class='col-md-12 margin-top-1'></div>
                <div class='col-md-12 form-vertical'>
                  <div class='form-group text-left'>
                                  <label id='label6' title=''>Las declaraciones de esta autorización son tomadas por el Asegurado Solicitante (s) o de la persona autorizada para actuar en nombre del asegurado solicitante (s).</label>

                  </div>
                </div>
                <!-- Container: zone0 zone -->
                <div  class='col-md-12'>

                  <div id="zone0" class='panel panel-default'>
                    <div class='panel-heading' title=''></div>
                 <div class="panel-body">
    <!-- Container content -->
                <!-- label0 label -->
                <div class='col-md-3 form-vertical'>
                  <div class='form-group text-left'>
                                  <label id='label0' title='' class='hidden' >...</label>

                  </div>
                </div>
                <!-- label7 label -->
                <div class='col-md-3 form-vertical'>
                  <div class='form-group text-left'>
                                  <label id='label7' title='' class='hidden' >...</label>

                  </div>
                </div>
                <!-- label8 label -->
                <div class='col-md-3 form-vertical'>
                  <div class='form-group text-left'>
                                  <label id='label8' title='' class='hidden' >...</label>

                  </div>
                </div>
                <!-- AcceptanceIndicator radiobuttonlist -->
                <div class='col-md-3 form-horizontal'>
                  <div class='form-group'>
                    <div class='col-md-4 text-right'>
                      <label id='AcceptanceIndicatorLabel' class='control-label' for='AcceptanceIndicator'>Acepto</label>
                    </div>
                    <div class='col-md-8'>
                                <div id='AcceptanceIndicatorWrap'>
                <label class='radio-inline'>
                  <input type='radio' name='AcceptanceIndicator' id='AcceptanceIndicator_true' value='true' title='Indicador de aceptación.' class='default' />
                  <span >Si</span>
                </label>
                <label class='radio-inline'>
                  <input type='radio' name='AcceptanceIndicator' id='AcceptanceIndicator_false' value='false'/>
                  <span >No</span>
                </label>
                <div id='AcceptanceIndicator_validate'></div>
            </div>

                    </div>
                  </div>
                </div>
    <!-- End Container content -->
                 </div>
               </div>
                </div>
                <!-- End Container: zone0 zone -->
    <!-- End Container content -->
                 </div>
               </div>
                        </div>
                        <!-- End Container: PART2 zone -->
                        <!-- Container: part4 zone -->
                        <div  class='col-md-12'>

                  <div id="part4" class='panel panel-default'>
                    <div class='panel-heading' title=''></div>
                 <div class="panel-body">
    <!-- Container content -->
                <!-- DateReceived datepicker -->
                <div class='col-md-4 form-vertical'>
                  <div class='form-group'>
                    <label id='DateReceivedLabel' class='control-label' for='DateReceived'>Fecha</label><span id='DateReceivedRequired' class='required-mark'>*</span>
                        <div class='input-group date' id='DateReceived_group'>
                                    <input id='DateReceived' name='DateReceived' type='text' class='form-control' title='' size='10' maxlength='10'/>
                                    <span class='input-group-addon'>
                                        <span class='glyphicon glyphicon-calendar'></span>
                                    </span>
                                </div>
                  </div>
                </div>
                <!-- submit button -->
                <div class='col-md-4 form-vertical'>
                  <div class='form-group'>
                    <button id='submit' class='ladda-button btn pull-right btn-default' data-style='expand-right' title='' >
   <img src='/images/Library/16x16_ASPNetIcons/file_manager_16x16.gif' id='submitImage' />   <span class='ladda-label'>Guardar temporalmente</span><span class='ladda-spinner'></span></button>

                  </div>
                </div>
                <!-- save button -->
                <div class='col-md-4 form-vertical'>
                  <div class='form-group'>
                    <button id='save' class='ladda-button btn pull-right btn-default' data-style='expand-right' title='' disabled='disabled' >
   <img src='/images/Library/16x16_ASPNetIcons/mail2_(add)_16x16.gif' id='saveImage' />   <span class='ladda-label'>Enviar</span><span class='ladda-spinner'></span></button>

                  </div>
                </div>
    <!-- End Container content -->
                 </div>
               </div>
                        </div>
                        <!-- End Container: part4 zone -->
            <!-- End Container content -->

            </div>
        </form>





    </div>
  </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footer" runat="Server">
    <script src='/fasi/assets/js/bootstrap-datetimepicker.min.js'></script>


    <script src="AuthorizationToObtainDiscloseInformationUW.js?rel=20200117022114581"></script>
 
</asp:Content>