<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">
    '%--------------------------------------------------------------
    '% Nombre :      CAL110
    '% Descripcion : Permite Consultar y genera cuadro de poliza y reporte de endoso 
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
    
    Dim Fecha_ini As Date = DateAdd(DateInterval.Month, -1, Today)
    
</script>
<%  Response.Expires = 0
    mobjValues = New eFunctions.Values
    mobjMenu = New eFunctions.Menues
    mobjValues.ActionQuery = CDbl(IIf(Request.QueryString.Item("nMainAction") = "", 0, Request.QueryString.Item("nMainAction"))) = 401
%>
<HTML>
<HEAD>
	<META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>
    <SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
    <SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/ValFunctions.js"></SCRIPT>
    <SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/Constantes.js"></SCRIPT>
    <SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tmenu.js"></SCRIPT>
    <SCRIPT>
        var nCost = 0;
        var nCurrency = 0;

        //% InsChangeField: Carga campos a utilizar dependiendo de su selección dentro del radio button
        //------------------------------------------------------------------------------------------
        function InsChangeField(vObj, sField) {
            switch (sField) {
                case 'optntype':
                    if (vObj.value == 1) {
                        document.getElementsByTagName("TR")[8].style.display = '';
                        document.getElementsByTagName("TR")[9].style.display = 'none';
                        document.getElementsByTagName("TD")[33].style.display = '';
                        document.getElementsByTagName("TD")[34].style.display = '';
                        //%Manejo para filtrar resultados de posibbles values de tipos de reportes
                        with (self.document.forms[0]) {
                            valTypeReport.value = '';
                            UpdateDiv('valTypeReportDesc', '');
                            insBlankFields();
                            valTypeReport.Parameters.Param1.sValue = vObj.value;
                        }
                    }
                    else {
                        document.getElementsByTagName("TR")[8].style.display = '';
                        document.getElementsByTagName("TR")[9].style.display = 'none';
                        //%Manejo para filtrar resultados de posibbles values de tipos de reportes
                        with (self.document.forms[0]) {
                            valTypeReport.value = '1';
                            //UpdateDiv('valTypeReportDesc', '');
                            $(valTypeReport).change();
                            insBlankFields();
                            valTypeReport.Parameters.Param1.sValue = vObj.value;
                        }
                    }
                    break;
            }
        }

        //%   insCancel: Ejecuta rutinas necesarias en el momento de cancelar la página.
        //------------------------------------------------------------------------------------------
        function insCancel() {
            //------------------------------------------------------------------------------------------
            return true;
        }

        //%   insStateZone: Se controla los campos que se deben mostrar al momento de cargar la página por primera vez.
        //------------------------------------------------------------------------------------------
        function insShowInitials() {
        //------------------------------------------------------------------------------------------
            document.getElementsByTagName("TR")[9].style.display = 'none';
        }

        //%   insBlankFields: Blanque los campos al cambiar el tipo
        //------------------------------------------------------------------------------------------
        function insBlankFields() {
            //------------------------------------------------------------------------------------------
            with (self.document.forms[0]) {
                cbeBranch.value = "";
                valProduct.value = "";
                UpdateDiv("valProductDesc", "");
                tcnPolicy.value = "";
            }
        }

        //% insShowValues:Habilita o deshabilita el campo Certificado dependiendo del tipo de póliza pasada como parámetro.
        //-------------------------------------------------------------------------------------------
        function insShowValues(sField) {
            //-------------------------------------------------------------------------------------------
            with (self.document.forms[0]) {
                switch (sField) {
                    case "Policy":
                        if (tcnPolicy.value != "")
                            insDefValues("ShowDataProduct3", "nPolicy=" + tcnPolicy.value + '&nBranch=' + cbeBranch.value + '&nProduct=' + valProduct.value)
                        break;
                }
            }
        }

        //%   insChargeProduct: Se cargan los parámetros del campo producto.
        //------------------------------------------------------------------------------------------
        function insChargeProduct(lobject) {
            //------------------------------------------------------------------------------------------
            if (lobject.value != 0) {
                with (self.document.forms[0]) {
                    valProduct.disabled = false;
                    btnvalProduct.disabled = false;
                    valProduct.value = "";
                    UpdateDiv("valProductDesc", "");
                    valProduct.Parameters.Param1.sValue = lobject.value;
                    valProduct.Parameters.Param2.sValue = 0;
                }
            }
        }

        //% insShowInitial: Oculta los campos de la página al entrar en ella
        //------------------------------------------------------------------------------------------
        function insShowInitial() {
            //------------------------------------------------------------------------------------------
            document.getElementsByTagName("TR")[8].style.display = 'none';
            document.getElementsByTagName("TR")[9].style.display = '';
            document.getElementsByTagName("TD")[33].style.display = '';
            document.getElementsByTagName("TD")[34].style.display = '';
        }

        //%: Permite habilitar e inhabilitar los campos de la página.
        //------------------------------------------------------------------------------------------
        function insEnabledFields(lobject) {
            //------------------------------------------------------------------------------------------
            insBlankFields();

            switch (lobject.value) {
                //Ninguno	 
                case "":
                    {
                        insShowInitial();
                        break;
                    }
                    //Cuadro póliza
                case "1":
                    {
                        with (self.document.forms[0]) {
                            if (document.getElementsByName('optntype')[0].checked) {
                                document.getElementsByTagName("TR")[8].style.display = '';
                                document.getElementsByTagName("TR")[9].style.display = 'none';
                                document.getElementsByTagName("TD")[33].style.display = '';
                                document.getElementsByTagName("TD")[34].style.display = '';
                            }
                            if (document.getElementsByName('optntype')[1].checked) {
                                document.getElementsByTagName("TR")[8].style.display = 'none';
                                document.getElementsByTagName("TR")[9].style.display = '';
                                document.getElementsByTagName("TD")[33].style.display = 'block';
                                document.getElementsByTagName("TD")[34].style.display = 'block';
                            }
                        }

                        break;
                    }
                    //Certificado de Coberturas
                case "3":
                    {
                        with (self.document.forms[0]) {
                            if (document.getElementsByName('optntype')[0].checked) {
                                document.getElementsByTagName("TR")[8].style.display = '';
                                document.getElementsByTagName("TR")[9].style.display = 'none';
                                document.getElementsByTagName("TD")[33].style.display = '';
                                document.getElementsByTagName("TD")[34].style.display = '';
                                with (self.document.forms[0]) {
                                    valTypeReport.value = '3';
                                    UpdateDiv('valTypeReportDesc', '');
                                    //$(valTypeReport).change();
                                }
                            }
                            if (document.getElementsByName('optntype')[1].checked) {
                                document.getElementsByTagName("TR")[8].style.display = '';
                                document.getElementsByTagName("TR")[9].style.display = 'block';
                                with (self.document.forms[0]) {
                                    valTypeReport.value = '3';
                                    UpdateDiv('valTypeReportDesc', '');
                                    //$(valTypeReport).change();
                                }
                            }
                        }
                        break;
                    }
                    //Certificados de Endosos
                case "4":
                    {
                        document.getElementsByTagName("TR")[8].style.display = '';
                        document.getElementsByTagName("TR")[9].style.display = 'none';
                        document.getElementsByTagName("TD")[33].style.display = '';
                        document.getElementsByTagName("TD")[34].style.display = '';
                        break;
                    }
                    //Historia póliza	
                case "5":
                    {
                        document.getElementsByTagName("TR")[8].style.display = 'none';
                        document.getElementsByTagName("TR")[9].style.display = '';
                        document.getElementsByTagName("TD")[33].style.display = '';
                        document.getElementsByTagName("TD")[34].style.display = '';
                        break;
                    }
            }
        }

        //%   insEnabledPolicy(): Permite habilitar e inhabilitar el campo Póliza.
        //------------------------------------------------------------------------------------------
        function insEnabledPolicy(lobject) {
            //------------------------------------------------------------------------------------------
            if (lobject.value)
                with (self.document.forms[0]) {
                    tcnPolicy.disabled = false;
                    tcnPolicy.value = "";
                }
            else {
                with (self.document.forms[0]) {
                    tcnPolicy.disabled = true;
                    tcnPolicy.value = "";
                }
            }
        }

        //%   insEnabledCertif(): Permite habilitar e inhabilitar el campo Certificado.
        //------------------------------------------------------------------------------------------
        function insEnabledCertif(lobject) {
            //------------------------------------------------------------------------------------------
        }

        //%   insChangeValue: Se cargan parámetros del campo Nota
        //------------------------------------------------------------------------------------------
        function insChangeValue(lobject) {
            //
        }
        //% insFinish: se controla la acción Finalizar de la página
        //------------------------------------------------------------------------------------------
        function insFinish() {
            //------------------------------------------------------------------------------------------
            return (true);
        }

    </SCRIPT>
<%  Response.Write(mobjValues.StyleSheet())
    Response.Write(mobjMenu.MakeMenu("CAL0110", "CAL0110.aspx", 1, vbNullString))
    mobjMenu = Nothing
    Response.Write("<SCRIPT>var nMainAction=top.frames['fraSequence'].plngMainAction</SCRIPT>")
%>
</HEAD>
<BODY>
<BR>
<FORM METHOD="POST" NAME="BTC001" ACTION="valpolicyrep.aspx?sMode=2">
    <table width="100%">
        <TR>
            <TD width="20%"><label><%= GetLocalResourceObject("anchorCaption")%></label></TD>
            <TD width="40%"><%= mobjValues.OptionControl(0, "optntype", GetLocalResourceObject("optntype_1Caption"), CStr(1), "1", "InsChangeField(this, ""optntype"")",False)%></TD>
            <TD width="20%"><%= mobjValues.OptionControl(1, "optntype", GetLocalResourceObject("optntype_2Caption"), , "2", "InsChangeField(this, ""optntype"")", False)%></TD>
            <TD width="40%"></TD> 
        </TR>
        <TR>
		    <TD WIDTH="20%"><LABEL><%= GetLocalResourceObject("cbenTypeReportCaption")%></LABEL></TD>
            <td width="40%">
            <%
                With mobjValues
                    .Parameters.Add("ntype_report", 1, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                End With
            %>
            <%= mobjValues.PossiblesValues("valTypeReport", "TABTYPEREPORT", eFunctions.Values.eValuesType.clngWindowType, vbNullString, True, , , , , "insEnabledFields(this);", , , "", eFunctions.Values.eTypeCode.eNumeric, 5)%>
            </td>
            <td></td>
            <td></td>
        </TR>
        <TR>
            <td><LABEL><%= GetLocalResourceObject("cbeBranch1Caption")%></LABEL></TD>
            <td><%= mobjValues.BranchControl("cbeBranch", GetLocalResourceObject("cbeBranchToolTip"), , , , , , , False)%></td>
			<td width="20%"><LABEL><%= GetLocalResourceObject("valProduct1Caption")%></LABEL></TD>
			<td width="40%">
            <%= mobjValues.ProductControl("valProduct", GetLocalResourceObject("valProductToolTip"), , eFunctions.Values.eValuesType.clngWindowType, False)%>
			</td>
        </TR>

         <TR>
         	<td width="20%"><LABEL><%= GetLocalResourceObject("tcnPolicyCaption")%></LABEL></TD>
            <td width="40%"><%= mobjValues.NumericControl("tcnPolicy", 10, , , GetLocalResourceObject("tcnPolicyToolTip"), , , , , , "insShowValues(""Policy"")")%></td>
            <td width="20%"><label><%= GetLocalResourceObject("tcnCertifCaption") %></label></td>
            <td width="40%"><%= mobjValues.NumericControl("tcnCertif", 10, 0,, GetLocalResourceObject("tcnCertifToolTip")) %></td>
        </TR>
        <TR>
            <td width="20%"><label><%= GetLocalResourceObject("tcdIssuedatIniCaption")%></label></td>
            <td width="40%"><%= mobjValues.DateControl("tcdIssuedatIni", Fecha_ini , , GetLocalResourceObject("tcdIssuedatIniToolTip"))%></td>
            <td width="20%"><label><%= GetLocalResourceObject("tcdIssuedatEndCaption")%></label></td>
            <td width="40%"><%= mobjValues.DateControl("tcdIssuedatEnd", Today, , GetLocalResourceObject("tcdIssuedatEndToolTip"))%></td>
        </TR>
    </table>
</FORM>
<script>    insShowInitials();</script>
</BODY>
</HTML>