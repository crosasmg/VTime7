﻿<%@ Page Language="VB" MasterPageFile="~/fasi/FASI.master" AutoEventWireup="false" %>
<%@ MasterType TypeName="FASI" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <!-- This code was generated by Form Designer v7.1.212.1 at 2018/11/07 03:23:00 PM model release 35, Form Generator v1.0.33.2 -->
    <!-- Changes to this file may cause incorrect behavior and will be lost if the code is regenerated. -->
    
    <!-- Bootstrap -->
    <link rel='stylesheet' href='/fasi/assets/css/ladda-themeless.min.css' />
    <link rel='stylesheet' href='/fasi/assets/css/bootstrap-multiselect.css' />
    <link rel='stylesheet' href='/fasi/assets/css/bootstrap-datetimepicker.min.css' />
    <link rel='stylesheet' href='/fasi/assets/css/bootstrap-table.min.css' />
    <link rel='stylesheet' href='/fasi/assets/css/select2.min.css' />

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
        <form id="UserManagerMainForm">
            <input type="hidden" id="UserManagerFormId" name="UserManagerFormId" value="" />
            <div id="alerts-container"></div>
			<div class="row">
            <!-- Container content -->
                        <!-- Container: User grid -->
                        <div  class='col-md-12'>

                       <div id='UserContainer'>
                            <div id="Usertoolbar">
                            <div class='form-inline'>
                                <button id="UserRemoveBtn"  type="button" class="btn btn-danger" disabled data-title="Eliminar">
                                    <i class="glyphicon glyphicon-remove"></i>Eliminar
                                </button>
                <!-- btnAllUser button -->
                  <div class='form-group'>
                    <button id='btnAllUser' class='ladda-button btn pull-left btn-default' data-style='expand-right' title='Permite mostrar todos los usuarios registrados' ><span class='ladda-label'>Todos los usuarios</span><span class='ladda-spinner'></span></button>

                  </div>
                <!-- btnPendingApproval button -->
                  <div class='form-group'>
                    <button id='btnPendingApproval' class='ladda-button btn pull-left btn-default' data-style='expand-right' title='Permite mostrar los usuarios pendientes de aprobación' ><span class='ladda-label'>Usuarios por aprobar</span><span class='ladda-spinner'></span></button>

                  </div>
                            </div>
                            </div>
                            <div id='UserTblPlaceHolder'></div>
                       </div>

                        </div>
                        <!-- End Container: User grid -->
                        <!-- Container: popup0 Popup -->
                        <div id='popup0Popup' class='modal fade' role='dialog'>
                          <div class='modal-dialog'>
                  <div class='modal-content'>
                    <div class='modal-header'>
                      <button type='button' class='close' data-dismiss='modal'>&times;</button>
                      <h4 class='modal-title' >Cambio de email</h4>
                    </div>
                    <div class='modal-body'>
                      <div class='row'>
    <!-- Container content -->
                <!-- EmailOld text -->
                <div class='col-md-12 form-horizontal'>
                  <div class='form-group'>
                    <div class='col-md-4 text-left'>
                      <label id='EmailOldLabel' class='control-label' for='EmailOld'>Correo electrónico nuevo</label><span id='EmailOldRequired' class='required-mark'>*</span>
                    </div>
                    <div class='col-md-8'>
                        <input class='form-control' id='EmailOld' name='EmailOld' title='text1' type='text' size='80' maxlength='80'/>
                    </div>
                  </div>
                </div>
                <!-- btnEmailChange button -->
                <div class='col-md-12 form-horizontal'>
                  <div class='form-group'>
                    <div class='col-md-12'>
                    <button id='btnEmailChange' class='ladda-button btn pull-left btn-default' data-style='expand-right' title='Cambiar' ><span class='ladda-label'>Cambiar</span><span class='ladda-spinner'></span></button>

                    </div>
                  </div>
                </div>
    <!-- End Container content -->
                      </div>
                    </div>
                  </div>
                          </div>
                        </div>
                        <!-- End Container: popup0 Popup -->
                        <!-- EmailChangeResult checkbox -->
                        <div class='col-md-12 form-horizontal'>
                          <div class='form-group'>
                            <div class='col-md-4 text-left'>
                              <label id='EmailChangeResultLabel' class='control-label hidden' for='EmailChangeResult'>EmailChangeResult</label>
                            </div>
                            <div class='col-md-8'>
                                <div class='checkbox'>
                                    <label id='EmailChangeResultLabel'  hidden>
                                        <input id='EmailChangeResult' name='EmailChangeResult' type='checkbox' title='text0'   class='hidden'  />
                                    </label>
                                </div>

                            </div>
                          </div>
                        </div>
                        <!-- Container: zone0 zone -->
                        <div  class='col-md-12'>

                        </div>
                        <!-- End Container: zone0 zone -->
                        <!-- Type numeric -->
                        <div class='col-md-12 form-horizontal'>
                          <div class='form-group'>
                            <div class='col-md-4 text-left'>
                              <label id='TypeLabel' class='control-label hidden' for='Type'>Type</label>
                            </div>
                            <div class='col-md-8'>
                                <input class='form-control hidden' id='Type' name='Type' title='numeric0' type='text' style='text-align: right'/>
                            </div>
                          </div>
                        </div>
            <!-- End Container content -->

            </div>
        </form>



        <div id='UserPopup' class='modal fade' tabindex='-1' role='dialog' aria-labelledby='edit' aria-hidden='true'>
            <div class="modal-dialog modal-lg">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                        <h4 class="modal-title">title</h4>
                    </div>
                    <div class="modal-body">
                      <div class="row">
                        <form id='UserEditForm'>
                            <!-- Container content -->
                                        <!-- UserId numeric -->
                                        <div class='col-md-6 form-horizontal'>
                                          <div class='form-group'>
                                            <div class='col-md-4 text-left'>
                                              <label id='UserIdLabel' class='control-label' for='UserId'>Código de usuario</label>
                                            </div>
                                            <div class='col-md-8'>
                                                <input class='form-control' id='UserId' name='UserId' title='Código de usuario' type='text' style='text-align: right'/>
                                            </div>
                                          </div>
                                        </div>
                                        <!-- UserName text -->
                                        <div class='col-md-6 form-horizontal'>
                                          <div class='form-group'>
                                            <div class='col-md-4 text-left'>
                                              <label id='UserNameLabel' class='control-label' for='UserName'>Usuario</label>
                                            </div>
                                            <div class='col-md-8'>
                                                <input class='form-control' id='UserName' name='UserName' title='Nombre del usuario' type='text' size='80' maxlength='80'/>
                                            </div>
                                          </div>
                                        </div>
                                        <!-- Email text -->
                                        <div class='col-md-6 form-horizontal'>
                                          <div class='form-group'>
                                            <div class='col-md-4 text-left'>
                                              <label id='EmailLabel' class='control-label' for='Email'>Correo electrónico</label>
                                            </div>
                                            <div class='col-md-8'>
                                                <input class='form-control' id='Email' name='Email' title='Correo electrónico' type='text' size='80' maxlength='80'/>
                                            </div>
                                          </div>
                                        </div>
                                        <!-- IsEmployee checkbox -->
                                        <div class='col-md-6 form-horizontal'>
                                          <div class='form-group'>
                                            <div class='col-md-4 text-left'>
                                              <label id='IsEmployeeLabel' class='control-label' for='IsEmployee'>Empleado</label>
                                            </div>
                                            <div class='col-md-8'>
                                                <div class='checkbox'>
                                                    <label id='IsEmployeeLabel' >
                                                        <input id='IsEmployee' name='IsEmployee' type='checkbox' title='Empleado'   />
                                                    </label>
                                                </div>

                                            </div>
                                          </div>
                                        </div>
                                        <!-- IsApproved checkbox -->
                                        <div class='col-md-6 form-horizontal'>
                                          <div class='form-group'>
                                            <div class='col-md-4 text-left'>
                                              <label id='IsApprovedLabel' class='control-label hidden' for='IsApproved'>Aprobado</label>
                                            </div>
                                            <div class='col-md-8'>
                                                <div class='checkbox'>
                                                    <label id='IsApprovedLabel'  hidden>
                                                        <input id='IsApproved' name='IsApproved' type='checkbox' title='Aprobado'   class='hidden'  />
                                                    </label>
                                                </div>

                                            </div>
                                          </div>
                                        </div>
                                        <!-- IsAdministrator checkbox -->
                                        <div class='col-md-6 form-horizontal'>
                                          <div class='form-group'>
                                            <div class='col-md-4 text-left'>
                                              <label id='IsAdministratorLabel' class='control-label' for='IsAdministrator'>Administrador</label>
                                            </div>
                                            <div class='col-md-8'>
                                                <div class='checkbox'>
                                                    <label id='IsAdministratorLabel' >
                                                        <input id='IsAdministrator' name='IsAdministrator' type='checkbox' title='Es Administrador'   />
                                                    </label>
                                                </div>

                                            </div>
                                          </div>
                                        </div>
                                        <!-- AllowScheduler checkbox -->
                                        <div class='col-md-6 form-horizontal'>
                                          <div class='form-group'>
                                            <div class='col-md-4 text-left'>
                                              <label id='AllowSchedulerLabel' class='control-label' for='AllowScheduler'>Agenda</label>
                                            </div>
                                            <div class='col-md-8'>
                                                <div class='checkbox'>
                                                    <label id='AllowSchedulerLabel' >
                                                        <input id='AllowScheduler' name='AllowScheduler' type='checkbox' title='Permite Agenda'   />
                                                    </label>
                                                </div>

                                            </div>
                                          </div>
                                        </div>
                                        <!-- IsLockedOut checkbox -->
                                        <div class='col-md-6 form-horizontal'>
                                          <div class='form-group'>
                                            <div class='col-md-4 text-left'>
                                              <label id='IsLockedOutLabel' class='control-label' for='IsLockedOut'>Bloqueado</label>
                                            </div>
                                            <div class='col-md-8'>
                                                <div class='checkbox'>
                                                    <label id='IsLockedOutLabel' >
                                                        <input id='IsLockedOut' name='IsLockedOut' type='checkbox' title='Bloqueado'   />
                                                    </label>
                                                </div>

                                            </div>
                                          </div>
                                        </div>
                                        <!-- PasswordNeverExpires checkbox -->
                                        <div class='col-md-6 form-horizontal'>
                                          <div class='form-group'>
                                            <div class='col-md-4 text-left'>
                                              <label id='PasswordNeverExpiresLabel' class='control-label' for='PasswordNeverExpires'>Contraseña nunca expira</label>
                                            </div>
                                            <div class='col-md-8'>
                                                <div class='checkbox'>
                                                    <label id='PasswordNeverExpiresLabel' >
                                                        <input id='PasswordNeverExpires' name='PasswordNeverExpires' type='checkbox' title='Contraseña nunca expira'   />
                                                    </label>
                                                </div>

                                            </div>
                                          </div>
                                        </div>
                                        <!-- SupervisorId dropdownlist -->
                                        <div class='col-md-6 form-horizontal'>
                                          <div class='form-group'>
                                            <div class='col-md-4 text-left'>
                                              <label id='SupervisorIdLabel' class='control-label' for='SupervisorId'>Supervisor</label>
                                            </div>
                                            <div class='col-md-8'>
                                                <select id='SupervisorId' name='SupervisorId' class='form-control' title='Supervisor'>
                                                </select>

                                            </div>
                                          </div>
                                        </div>
                                        <!-- RolAssiged CheckComboBox -->
                                        <div class='col-md-6 form-horizontal'>
                                          <div class='form-group'>
                                            <div class='col-md-4 text-left'>
                                              <label id='RolAssigedLabel' class='control-label' for='RolAssiged'>Roles</label><span id='RolAssigedRequired' class='required-mark'>*</span>
                                            </div>
                                            <div class='col-md-8'>
                                                <select id='RolAssiged' name='RolAssiged' class='form-control' multiple='multiple' title='Roles asignados'>
                                                </select>

                                            </div>
                                          </div>
                                        </div>
                                        <!-- GroupAssiged CheckComboBox -->
                                        <div class='col-md-6 form-horizontal'>
                                          <div class='form-group'>
                                            <div class='col-md-4 text-left'>
                                              <label id='GroupAssigedLabel' class='control-label' for='GroupAssiged'>Grupos</label>
                                            </div>
                                            <div class='col-md-8'>
                                                <select id='GroupAssiged' name='GroupAssiged' class='form-control' multiple='multiple' title='Grupos asignados'>
                                                </select>

                                            </div>
                                          </div>
                                        </div>
                                        <!-- SecurityLevel numeric -->
                                        <div class='col-md-6 form-horizontal'>
                                          <div class='form-group'>
                                            <div class='col-md-4 text-left'>
                                              <label id='SecurityLevelLabel' class='control-label' for='SecurityLevel'>Nivel seguridad</label>
                                            </div>
                                            <div class='col-md-8'>
                                                <input class='form-control' id='SecurityLevel' name='SecurityLevel' title='Nivel seguridad' type='text' style='text-align: right'/>
                                            </div>
                                          </div>
                                        </div>
                                        <!-- ClientId dropdownlist -->
                                        <div class='col-md-6 form-horizontal'>
                                          <div class='form-group'>
                                            <div class='col-md-4 text-left'>
                                              <label id='ClientIdLabel' class='control-label' for='ClientId'>Código cliente</label>
                                            </div>
                                            <div class='col-md-8'>
                                                <select id='ClientId' name='ClientId' class='form-control' title='Código cliente'>
                                                </select>

                                            </div>
                                          </div>
                                        </div>
                                        <!-- ProducerId dropdownlist -->
                                        <div class='col-md-6 form-horizontal'>
                                          <div class='form-group'>
                                            <div class='col-md-4 text-left'>
                                              <label id='ProducerIdLabel' class='control-label' for='ProducerId'>Código productor</label>
                                            </div>
                                            <div class='col-md-8'>
                                                <select id='ProducerId' name='ProducerId' class='form-control' title='Código productor'>
                                                </select>

                                            </div>
                                          </div>
                                        </div>
                                        <!-- CreationDate datepicker -->
                                        <div class='col-md-6 form-horizontal'>
                                          <div class='form-group'>
                                            <div class='col-md-4 text-left'>
                                              <label id='CreationDateLabel' class='control-label' for='CreationDate'>Creado</label>
                                            </div>
                                            <div class='col-md-8'>
                                                <div class='input-group date' id='CreationDate_group'>
                                                    <input id='CreationDate' name='CreationDate' type='text' class='form-control' title='Fecha de creación' size='10' maxlength='10'/>
                                                    <span class='input-group-addon'>
                                                        <span class='glyphicon glyphicon-calendar'></span>
                                                    </span>
                                                </div>
                                            </div>
                                          </div>
                                        </div>
                                        <!-- LastLoginDate datepicker -->
                                        <div class='col-md-6 form-horizontal'>
                                          <div class='form-group'>
                                            <div class='col-md-4 text-left'>
                                              <label id='LastLoginDateLabel' class='control-label' for='LastLoginDate'>Último acceso</label>
                                            </div>
                                            <div class='col-md-8'>
                                                <div class='input-group date' id='LastLoginDate_group'>
                                                    <input id='LastLoginDate' name='LastLoginDate' type='text' class='form-control' title='Fecha de último acceso' size='10' maxlength='10'/>
                                                    <span class='input-group-addon'>
                                                        <span class='glyphicon glyphicon-calendar'></span>
                                                    </span>
                                                </div>
                                            </div>
                                          </div>
                                        </div>
                                        <!-- LastLockedOutDate datepicker -->
                                        <div class='col-md-6 form-horizontal'>
                                          <div class='form-group'>
                                            <div class='col-md-4 text-left'>
                                              <label id='LastLockedOutDateLabel' class='control-label' for='LastLockedOutDate'>Último bloqueo</label>
                                            </div>
                                            <div class='col-md-8'>
                                                <div class='input-group date' id='LastLockedOutDate_group'>
                                                    <input id='LastLockedOutDate' name='LastLockedOutDate' type='text' class='form-control' title='Fecha de último bloqueo' size='10' maxlength='10'/>
                                                    <span class='input-group-addon'>
                                                        <span class='glyphicon glyphicon-calendar'></span>
                                                    </span>
                                                </div>
                                            </div>
                                          </div>
                                        </div>
                            <!-- End Container content -->

                        </form>
                      </div>
                    </div>
                    <div class="modal-footer">
                        <button id="UserSaveBtn"  class="btn btn-warning">Guardar</button>
                        <button class="btn"  data-dismiss="modal" aria-hidden="true">Cancelar</button>
                    </div>
                </div>
            </div>
        </div>

    <ul id='UserContextMenu' class='dropdown-menu'>
        <li data-item='User_Item1'><a>Reset contraseña</a></li>
        <li data-item='User_Item2'><a>Cambiar email</a></li>
        <li data-item='User_Item3'><a>Aprobar usuario</a></li>
        <li data-item='User_Item4'><a>Desbloquear usuario</a></li>
    </ul>

    </div>
  </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footer" runat="Server">
    <script src='/fasi/assets/js/spin.min.js'></script>
    <script src='/fasi/assets/js/ladda.min.js'></script>
    <script src='/fasi/assets/js/ladda.jquery.min.js'></script>
    <script src='/fasi/assets/js/bootstrap-multiselect.js'></script>
    <script src='/fasi/assets/js/bootstrap-datetimepicker.min.js'></script>
    <script src='/fasi/assets/js/bootstrap-table.min.js'></script>
    <script src='/fasi/assets/js/bootstrap-table-es-CR.min.js'></script>
    <script src='/fasi/app/js/TableHelper.js?rel=20181107032300180'></script>
    <script src='/fasi/assets/js/bootstrap-table-contextmenu.js'></script>
    <script src='/fasi/assets/js/bootstrap-table-export.js'></script>
    <script src='/fasi/assets/js/tableExport.min.js'></script>
    <script src='/fasi/assets/js/autoNumeric.min.js'></script>
    <script src='/fasi/app/js/Security.js?rel=20181107032300180'></script>


    <script src="UserManager.js?rel=20181107032300180"></script>
 
</asp:Content>