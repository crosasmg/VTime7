<%@ Page Language="VB" Explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false" %>

<%@ Import Namespace="eFunctions" %>
<script language="VB" runat="Server">
    '%--------------------------------------------------------------
    '% Nombre :      BTC001
    '% Descripcion : Permite consultar los resultados de procesos batch 
    '%               asociados a una transaccion
    '%
    '% document.VssVersion="$$Revision: 2 $|$$Date: 9-09-09 19:37 $|$$Author: Mpalleres $"
    '%--------------------------------------------------------------

    '- Objeto para el manejo de las funciones generales de carga de valores
    Dim mobjValues As eFunctions.Values

    '- Objeto para el manejo del grid de la página
    Dim mobjGrid As Object

    '- Objeto para el manejo del menú
    Dim mobjMenu As eFunctions.Menues

    '- Objeto para el manejo particular de los datos de la página
    Dim mcolClass As Object

    Dim linBatch_Out As Integer
    Dim linSheet_Out As Integer
    Dim sInt_Aux As String
    Dim sResult As String
    Dim sValue As String

</script>
<%Response.Expires = 0

    mobjValues = New eFunctions.Values
    mobjMenu = New eFunctions.Menues

    mobjValues.ActionQuery = CDbl(IIf(Request.QueryString.Item("nMainAction") = "", 0, Request.QueryString.Item("nMainAction"))) = 401

%>
<html>
<head>
    <meta name="GENERATOR" content="Microsoft Visual Studio 6.0" />


    <script language="JavaScript" src="/VTimeNet/Scripts/GenFunctions.js"></script>
    <script language="JavaScript" src="/VTimeNet/Scripts/tmenu.js"></script>
    <script language="JavaScript">
        //- Variable para el control de versiones
        document.VssVersion = "$$Revision: 2 $|$$Date: 9-09-09 19:37 $|$$Author: Mpalleres $"

        //% insStateZone: se controla el estado de los campos de la página
        //--------------------------------------------------------------------------------------------
        function insStateZone() {
            //--------------------------------------------------------------------------------------------
            var objErr;
            try {

                self.document.forms[0].tcnsheet.disabled = false;
                self.document.forms[0].btntcnsheet.disabled = false;
                self.document.forms[0].valBatch.disabled = false;
                self.document.forms[0].btnvalBatch.disabled = false;
                self.document.forms[0].valUsercod.disabled = false;
                self.document.forms[0].btnvalUsercod.disabled = false;
                self.document.forms[0].tcdProc.disabled = false;
                self.document.forms[0].btn_tcdProc.disabled = false;

            }
            catch (objErr) { };
        }

        //% insCancel: se controla la acción Cancelar de la página
        //--------------------------------------------------------------------------------------------
        function insCancel() {
            //--------------------------------------------------------------------------------------------
            return true;
        }

        //% insFinish: se controla la acción Cancelar de la página
        //--------------------------------------------------------------------------------------------
        function insFinish() {
            //--------------------------------------------------------------------------------------------
            return true;
        }

        function insInterzace() {
            //--------------------------------------------------------------------------------------------

            if (self.document.forms[0].tcnsheet.value != '') {
                self.document.forms[0].valBatch.value = 1402;
                self.document.forms[0].valBatch.select;
                $(self.document.forms[0].valBatch).change();
            }

        }


        function insBacth() {
            //--------------------------------------------------------------------------------------------

            if (self.document.forms[0].valBatch.value != 1402) {
                self.document.forms[0].tcnsheet.value = '';
                UpdateDiv('tcnsheetDesc', '');
            }

        }
    </script>
    <%
        Response.Write(mobjValues.StyleSheet())
        Response.Write(mobjMenu.MakeMenu("BTC001", "BTC001.aspx", 1, vbNullString))
        mobjMenu = Nothing
        Response.Write("<SCRIPT>var nMainAction=top.frames['fraSequence'].plngMainAction</SCRIPT>")
    %>
</head>
<body onunload="closeWindows();">
    <br>
    <form method="POST" name="BTC001" action="valBatch.aspx?sMode=2">
        <table width="100%">
            <TR>
                <!--Si la pagina es cargada atraves del botón ir a proceso desde alguna interfaz o proceso batch entonces debe precargar los valores de nprocess y ninterface-->
			    <%If Request.QueryString("ActionByToolbar") = 396 Then
                        sValue = Request.QueryString.Item("prevScodispl")
                        'Se busca si ventana desde la que se accede a procesos masivos es interfaz o página de algún proceso
                        sInt_Aux = sValue.Substring(0, 3)

                        'Se valida si ventana desde la que se accede a procesos es interfaz
                        If sInt_Aux <> "INT" Then
                            sResult = "1"
                            mobjMenu = New eFunctions.Menues
                            mobjMenu.insFindBatchProcess(Request.QueryString.Item("nUsercode"), Request.QueryString.Item("prevScodispl"), Today, linBatch_Out)
                        Else
                            sResult = "2"
                            linSheet_Out = Replace(sValue, "INT", "")
                        End If
                %>
                    <TD WIDTH="20%"><LABEL><%= GetLocalResourceObject("valBatchCaption") %></LABEL></TD>
			        <TD><%mobjValues.Parameters.Add("nStatreg", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    Response.Write(mobjValues.PossiblesValues("valBatch", "TABBATCH_PROCESS", eFunctions.Values.eValuesType.clngWindowType, IIf(sResult = "1", linBatch_Out, ""), True,  ,  ,  , 30, "insBacth();", True, 5, GetLocalResourceObject("valBatchToolTip")))%>
			        </TD>
			        <TD><LABEL><%= GetLocalResourceObject("tcnsheetCaption") %></LABEL></TD>
			        <TD>
			            <%mobjValues.Parameters.Add("NINTERTYPE", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                            mobjValues.Parameters.Add("NSYSTEM", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                            mobjValues.Parameters.ReturnValue("nOpertype",  ,  , True)
                            mobjValues.Parameters.ReturnValue("sOpertype",  ,  , True)
                            mobjValues.Parameters.ReturnValue("nFormat",  ,  , True)
                            mobjValues.Parameters.ReturnValue("sFormat",  ,  , True)
                            Response.Write(mobjValues.PossiblesValues("tcnsheet", "TABTABLEMASTERSHEET", eFunctions.Values.eValuesType.clngWindowType, IIf(sResult = "2", linSheet_Out, ""), True,  ,  ,  , 30, "insInterzace();", True, 5, GetLocalResourceObject("tcnsheetToolTip")))
                            If sResult = "2" Then
                                Response.Write("<SCRIPT>insInterzace();</SCRIPT>")
                            Else
                                Response.Write("<SCRIPT>insBacth();</SCRIPT>")
                            End If
                         %>
                     </TD>
                <!--Si uno ingresa a la pagina desde el portal entonces debe mostrar todos los campos en blanco-->
                <%Else %>
                    <TD WIDTH="20%"><LABEL><%=GetLocalResourceObject("valBatchCaption") %></LABEL></TD>
			        <TD><%mobjValues.Parameters.add("nStatreg", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                            Response.Write(mobjValues.PossiblesValues("valBatch", "TABBATCH_PROCESS", eFunctions.Values.eValuesType.clngWindowType,  , True,  ,  ,  , 30, "insBacth();", True, 5, GetLocalResourceObject("valBatchToolTip")))%>
			        </TD>
			        <TD><LABEL><%=GetLocalResourceObject("tcnsheetCaption") %></LABEL></TD>
			        <TD>
			            <%mobjValues.Parameters.Add("NINTERTYPE", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                            mobjValues.Parameters.Add("NSYSTEM", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                            mobjValues.Parameters.ReturnValue("nOpertype",  ,  , True)
                            mobjValues.Parameters.ReturnValue("sOpertype",  ,  , True)
                            mobjValues.Parameters.ReturnValue("nFormat",  ,  , True)
                            mobjValues.Parameters.ReturnValue("sFormat",  ,  , True)
                            Response.Write(mobjValues.PossiblesValues("tcnsheet", "TABTABLEMASTERSHEET", eFunctions.Values.eValuesType.clngWindowType,  , True,  ,  ,  , 30, "insInterzace();", True, 5, GetLocalResourceObject("tcnsheetToolTip")))%>
                     </TD>
                <%End If %>  			
          </TR>
            <TR>
                <%If Request.QueryString("ActionByToolbar") = 396 Then  %>
                    <TD WIDTH="20%"><LABEL><%= GetLocalResourceObject("valUsercodCaption") %></LABEL></TD>
			        <TD WIDTH="50%"><%= mobjValues.PossiblesValues("valUsercod", "tabUsers", eFunctions.Values.eValuesType.clngWindowType, Request.QueryString.Item("nUsercode"), , , , , , , True, 5, GetLocalResourceObject("valUsercodToolTip"), , , , False)%></TD>
			        <TD><LABEL><%= GetLocalResourceObject("tcdProcCaption") %></LABEL></TD>
			        <TD><%=mobjValues.DateControl("tcdProc", Today, False, GetLocalResourceObject("tcdProcToolTip"),  ,  ,  ,  , True)%>
			        </TD>
                <%Else %>
                    <TD WIDTH="20%"><LABEL><%= GetLocalResourceObject("valUsercodCaption") %></LABEL></TD>
			        <TD WIDTH="50%"><%= mobjValues.PossiblesValues("valUsercod", "tabUsers", eFunctions.Values.eValuesType.clngWindowType, vbNullString, , , , , , , True, 5, GetLocalResourceObject("valUsercodToolTip"), , , , False)%></TD>
			        <TD><LABEL><%= GetLocalResourceObject("tcdProcCaption") %></LABEL></TD>
			        <TD><%= mobjValues.DateControl("tcdProc", "", False, GetLocalResourceObject("tcdProcToolTip"),  ,  ,  ,  , True)%>
                        <%mobjMenu = Nothing %>
			        </TD>
                <%End If %>
            </TR>
        </table>
    </form>
</body>
</html>





