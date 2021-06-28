<%@ Page Title="" Language="C#" MasterPageFile="~/fasi/FASI.master" AutoEventWireup="true" CodeFile="menu.aspx.cs" Inherits="fasi_menu" %>

<%@ MasterType TypeName="FASI" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="/fasi/assets/jstree/dist/themes/default/style.min.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceholder1" runat="Server">
    <div class="row">
        <div class="col-lg-4">
            <div class="ibox" id="ibox1">
                <div class="ibox-title">
                    <h5>Menú personalizado</h5>
                    <div class="ibox-tools">
                        <a class="collapse-link" title="Minimizar">
                            <i class="fa fa-chevron-up"></i>
                        </a>
                        <a class="dropdown-toggle" data-toggle="dropdown" href="#" title="Seleccionar menú a mostrar">
                            <i class="fa fa-wrench"></i>
                        </a>
                        <ul class="dropdown-menu dropdown-user" id="buttonsPlace2">
                            <li><a href="#" class="dropdown-item">Opción</a></li>
                        </ul>
                    </div>
                </div>
                <div class="ibox-content">
                    <div class="sk-spinner sk-spinner-double-bounce">
                        <div class="sk-double-bounce1"></div>
                        <div class="sk-double-bounce2"></div>
                    </div>
                    <ul class="sortable-list connectList agile-list ui-sortable" id="todo">
                        <li class="warning-element" id="task1">Base de datos de clientes
                        </li>
                        <li class="success-element" id="task2">Tratamiento de pólizas
                        </li>
                        <li class="info-element" id="task3">Características de los productos
                        </li>
                        <li class="danger-element" id="task4">Cobro, devolución o conciliación
                        </li>
                    </ul>

                </div>
            </div>
        </div>
        <div class="col-lg-8">
            <div class="row">
                <div class="col-lg-12">
                    <div class="ibox">
                        <div class="ibox-title">
                            <h5><span id="MainMenuTitle">Menú</span></h5>
                            <div class="ibox-tools">
                                <a class="collapse-link" title="Minimizar">
                                    <i class="fa fa-chevron-up"></i>
                                </a>
                                <a class="dropdown-toggle" data-toggle="dropdown" href="#" title="Seleccionar menú a mostrar">
                                    <i class="fa fa-wrench"></i>
                                </a>
                                <ul class="dropdown-menu dropdown-user" id="buttonsPlace">
                                </ul>
                            </div>
                        </div>
                        <div class="ibox-content">
                            <%--            <select class="form-control" id="mainmenu">
            </select>--%>
                            <div id='vt'></div>
                        </div>
                    </div>
                </div>
                <div class="col-lg-12">
                    <div class="ibox ">
                        <div class="ibox-title">
                            <h5>Procesos batch <span class="badge badge-info">5</span></h5>
                            <div class="ibox-tools">
                                <a class="collapse-link">
                                    <i class="fa fa-chevron-up"></i>
                                </a>
                            </div>
                        </div>
                        <div class="ibox-content table-responsive">
                            <table class="table table-hover no-margins">
                                <thead>
                                    <tr>
                                        <th>Status</th>
                                        <th>Date</th>
                                        <th>User</th>
                                        <th>Value</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <tr>
                                        <td><small>Pending...</small></td>
                                        <td><i class="fa fa-clock-o"></i>11:20pm</td>
                                        <td>Samantha</td>
                                        <td class="text-navy"><i class="fa fa-level-up"></i>24% </td>
                                    </tr>
                                    <tr>
                                        <td><span class="label label-warning">Canceled</span> </td>
                                        <td><i class="fa fa-clock-o"></i>10:40am</td>
                                        <td>Monica</td>
                                        <td class="text-navy"><i class="fa fa-level-up"></i>66% </td>
                                    </tr>
                                    <tr>
                                        <td><small>Pending...</small> </td>
                                        <td><i class="fa fa-clock-o"></i>01:30pm</td>
                                        <td>John</td>
                                        <td class="text-navy"><i class="fa fa-level-up"></i>54% </td>
                                    </tr>
                                    <tr>
                                        <td><small>Pending...</small> </td>
                                        <td><i class="fa fa-clock-o"></i>02:20pm</td>
                                        <td>Agnes</td>
                                        <td class="text-navy"><i class="fa fa-level-up"></i>12% </td>
                                    </tr>
                                    <tr>
                                        <td><small>Pending...</small> </td>
                                        <td><i class="fa fa-clock-o"></i>09:40pm</td>
                                        <td>Janet</td>
                                        <td class="text-navy"><i class="fa fa-level-up"></i>22% </td>
                                    </tr>
                                </tbody>
                            </table>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footer" runat="Server">
    <script src="/fasi/assets/jstree/dist/jstree.min.js"></script>

    <script src="menu.js"></script>
</asp:Content>

