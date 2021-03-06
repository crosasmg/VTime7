<%@ Page Language="VB" MasterPageFile="~/fasi/FASI.master" AutoEventWireup="false" %>
<%@ MasterType TypeName="FASI" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <!-- This code was generated by Query Designer v7.2.22.1 at 2020/02/10 12:48:57 PM model release 1, Form Generator v1.0.37.32 - Query Generator v1.0.17.15 -->
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
    <div class="ibox">
     <div class="ibox-content">
        <form id="TestPolicyExportMainForm">
            <input type="hidden" id="TestPolicyExportFormId" name="TestPolicyExportFormId" value="" />
            <div id="alerts-container"></div>
			<div class="row">
            <!-- Container content -->
                        <!-- Container: Items grid -->
                        <div  class='col-md-12'>

                       <div id='ItemsContainer'>
                            <div id='ItemsTblPlaceHolder'></div>
                       </div>

                        </div>
                        <!-- End Container: Items grid -->
                        <!-- ROLESActionLbl label -->
                        <div class='col-md-12 form-vertical'>
                          <div class='form-group text-left'>
                                          <label id='ROLESActionLbl' title='Acciones disponibles'>Presione el icono <span class='caret'></span> para ver el menú de acciones disponibles asociadas al campo seleccionado.</label>

                          </div>
                        </div>
            <!-- End Container content -->

            </div>
        </form>




    <ul id='Items_ROLESNPOLICYContextMenu' class='dropdown-menu'>
 <li data-item='Items_ROLESNPOLICY_Item1'><a>Test</a></li>
    </ul>

    </div>
  </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footer" runat="Server">
    <script src='/fasi/assets/js/bootstrap-table.min.js'></script>
    <script src='/fasi/assets/js/bootstrap-table-es-CR.min.js'></script>
    <script src='/fasi/app/js/TableHelper.js?rel=20200210124857669'></script>
    <script src='/fasi/assets/js/bootstrap-table-contextmenu.js'></script>
    <script src='/fasi/assets/js/bootstrap-table-export.js'></script>
    <script src='/fasi/assets/js/tableExport.min.js'></script>
    <script src='/fasi/assets/js/pdfmake.min.js'></script>
    <script src='/fasi/assets/js/vfs_fonts.js'></script>
    <script src='/fasi/assets/js/jquery.base64.js'></script>
    <script src='/fasi/assets/js/autoNumeric.min.js'></script>


    <script src="TestPolicyExport.js?rel=20200210124857669"></script>
 
</asp:Content>