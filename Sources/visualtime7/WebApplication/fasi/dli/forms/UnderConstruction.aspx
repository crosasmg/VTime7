<%@ Page Language="VB" MasterPageFile="~/fasi/FASI.master" AutoEventWireup="false" %>
<%@ MasterType TypeName="FASI" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <!-- This code was generated by Form Designer v7.3.36.1 at 2020-03-18 10:15:19 AM model release 6, Form Generator v1.0.37.37 -->
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
    <div id="app" class="ibox">
     <div class="ibox-content">
        <form id="UnderConstructionMainForm">
            <input type="hidden" id="UnderConstructionFormId" name="UnderConstructionFormId" value="" />
            <div id="alerts-container"></div>
			<div class="row">
            <!-- Container content -->
                        <!-- label0 label -->
                        <div class='col-md-12 form-vertical'>
                          <div class='form-group text-center'>
                                          <label id='label0' data-i18n="[html]app.form.label0_Label;[title]app.form.label0_Tooltip" title='label0'>Página en construcción</label>

                          </div>
                        </div>
            <!-- End Container content -->

            </div>
        </form>





    </div>
  </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footer" runat="Server">
    <script src='/fasi/assets/js/i18next.min.js'></script>


    <script src="UnderConstruction.js?rel=20200318101519499"></script>
 
</asp:Content>