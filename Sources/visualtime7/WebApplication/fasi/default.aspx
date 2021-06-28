<%@ Page Title="" Language="C#" MasterPageFile="~/fasi/FASI.master" AutoEventWireup="true" CodeFile="default.aspx.cs" Inherits="fasi_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="/fasi/assets/gridstack/dist/gridstack.min.css" rel="stylesheet" />
    <link href="/fasi/assets/jstree/dist/themes/default/style.min.css" rel="stylesheet" />

    <link href="/fasi/app/css/default.css?rel=1526062220926" rel="stylesheet" />

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceholder1" runat="Server">    
    <div class="grid-stack"></div>

    <!-- Modal New Widgets -->
    <div id="widgetsModal" class="modal fade" tabindex="-1" role="dialog">
        <div class="modal-dialog modal-lg" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title" id="AddWidgetsTitle"></h4>
                </div>
                <div class="modal-body">
                </div>                
            </div>
        </div>
    </div>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footer" runat="Server">

    <script src="/fasi/assets/js/lodash.min.js"></script>
    <script src="/fasi/assets/gridstack/dist/gridstack.min.js"></script>
    <script src="/fasi/assets/gridstack/dist/gridstack.jQueryUI.min.js"></script>
    <script src="/fasi/assets/jstree/dist/jstree.min.js"></script>

    <script src="/fasi/app/js/default.js?rel=1526062220926"></script>
    <script src="/fasi/widgets/menuVTWidget.js?rel=1526062220926"></script>
    <script src="/fasi/widgets/iFrameWidget.js"></script>
</asp:Content>
