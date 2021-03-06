<%@ Page Language="VB" MasterPageFile="~/fasi/FASI.master" AutoEventWireup="false" %>
<%@ MasterType TypeName="FASI" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <!-- This code was generated by Form Designer v7.1.221.1 at 2019/03/12 11:19:12 AM model release 2, Form Generator v1.0.34.24 -->
    <!-- Changes to this file may cause incorrect behavior and will be lost if the code is regenerated. -->
    
    <!-- Bootstrap -->
    <link rel='stylesheet' href='/fasi/assets/css/ladda-themeless.min.css' />

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
        <form id="NSF0312AMainForm">
            <input type="hidden" id="NSF0312AFormId" name="NSF0312AFormId" value="" />
            <div id="alerts-container"></div>
			<div class="row">
            <!-- Container content -->
                        <!-- Parameter1 numeric -->
                        <div class='col-md-12 form-horizontal'>
                          <div class='form-group'>
                            <div class='col-md-4 text-left'>
                              <label id='Parameter1Label' class='control-label' for='Parameter1'>Numero</label>
                            </div>
                            <div class='col-md-8'>
                                <input class='form-control' id='Parameter1' name='Parameter1' title='Parameter1' type='text' style='text-align: right'/>
                            </div>
                          </div>
                        </div>
                        <!-- Parameter3 text -->
                        <div class='col-md-12 form-horizontal'>
                          <div class='form-group'>
                            <div class='col-md-4 text-left'>
                              <label id='Parameter3Label' class='control-label' for='Parameter3'>Texto</label>
                            </div>
                            <div class='col-md-8'>
                                <input class='form-control' id='Parameter3' name='Parameter3' title='Parameter3' type='text' size='15' maxlength='15'/>
                            </div>
                          </div>
                        </div>
                        <!-- button1 button -->
                        <div class='col-md-12 form-horizontal'>
                          <div class='form-group'>
                            <div class='col-md-12'>
                            <button id='button1' class='ladda-button btn pull-left btn-default' data-style='expand-right' title='button1' ><span class='ladda-label'>Accion</span><span class='ladda-spinner'></span></button>

                            </div>
                          </div>
                        </div>
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
    <script src='/fasi/assets/js/autoNumeric.min.js'></script>


    <script src="NSF0312A.js?rel=20190312111912345"></script>
 
</asp:Content>