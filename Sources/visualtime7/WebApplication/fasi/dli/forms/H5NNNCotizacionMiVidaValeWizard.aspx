﻿<%@ Page Language="VB" MasterPageFile="~/fasi/FASI.master" AutoEventWireup="false" %>
<%@ MasterType TypeName="FASI" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <!-- This code was generated by Form Designer v7.1.216.1 at 2019/02/11 02:27:49 p.m. model release 1, Form Generator v1.0.34.9 -->
    <!-- Changes to this file may cause incorrect behavior and will be lost if the code is regenerated. -->
    
    <!-- Bootstrap -->
    <link rel='stylesheet' href='/fasi/assets/css/ladda-themeless.min.css' />
    <link rel='stylesheet' href='/fasi/assets/css/bootstrap-datetimepicker.min.css' />
    <link rel='stylesheet' href='/fasi/assets/css/bootstrap-table.min.css' />
    <link href="/fasi/assets/css/jquery.steps.css" rel="stylesheet" />
    <!-- HTML5 Shim and Respond.js IE8 support of HTML5 elements and media queries -->
    <!-- WARNING: Respond.js doesn't work if you view the page via file:// -->
    <!--[if lt IE 9]>
      <script src="https://oss.maxcdn.com/libs/html5shiv/3.7.0/html5shiv.js"></script>
      <script src="https://oss.maxcdn.com/libs/respond.js/1.3.0/respond.min.js"></script>
   <![endif]-->
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceholder1" runat="Server">
    <div id="example-basic">
    <h3 class="steptitle">Información Personal</h3>
    <section>
        <div id="FirstPage"></div>
    </section>
    <h3 class="steptitle">Cotización</h3>
    <section>
     <div id="SecondPage"></div>
    </section>
    <h3 class="steptitle">Compra</h3>
    <section>
         <div id="ThirdPage"></div>
    </section>
</div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footer" runat="Server">
    <script src='/fasi/assets/js/spin.min.js'></script>
    <script src='/fasi/assets/js/ladda.min.js'></script>
    <script src='/fasi/assets/js/ladda.jquery.min.js'></script>
    <script src='/fasi/assets/js/bootstrap-datetimepicker.min.js'></script>
    <script src='/fasi/assets/js/bootstrap-table.min.js'></script>
    <script src='/fasi/assets/js/bootstrap-table-es-CR.min.js'></script>    
    <script src='/fasi/assets/js/bootstrap-datetimepicker.min.js'></script>
    <script src='/fasi/app/js/TableHelper.js?rel=20190211022749900'></script>
    <script src='https://maps.googleapis.com/maps/api/js?key=AIzaSyDs6FAyd0OFweXJiR360F-bovr-lXSYQGA&libraries=places'></script>
    <script src='/fasi/app/js/map-handling.min.js'></script>
    <script src='/fasi/assets/js/autoNumeric.min.js'></script>
    <script src='/fasi/assets/js/i18next.min.js'></script>
    <script src="/fasi/assets/js/jquery.steps.min.js"></script>
    <script src="H5NNNCotizacionMiVidaValeWizard.js"></script>
    </asp:Content>