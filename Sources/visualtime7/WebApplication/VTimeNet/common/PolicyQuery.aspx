<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">

    '- Objeto para el manejo de funciones de los objetos generales
    Dim mobjValues As eFunctions.Values


    '% ShowField: se muestran los campos para la búsqueda de los datos
    '--------------------------------------------------------------------------------------------
    Private Sub ShowField()
        '--------------------------------------------------------------------------------------------

        Response.Write("" & vbCrLf)
        Response.Write("    <TABLE WIDTH=100%>" & vbCrLf)
        Response.Write("        <TR>" & vbCrLf)
        Response.Write("			<TD><LABEL ID=54>Compañía</LABEL></TD>" & vbCrLf)
        Response.Write("			<TD WIDTH=""45%"">")


        Response.Write(mobjValues.PossiblesValues("cbeCompany", "Table5638", eFunctions.Values.eValuesType.clngComboType, Session("nMultiCompany"),  ,  ,  ,  ,  ,  ,  ,  ,"Compañía a la cual pertenece la póliza"))


        Response.Write("</TD>" & vbCrLf)
        Response.Write("            <TD>&nbsp;</TD>" & vbCrLf)
        Response.Write("			<TD><LABEL ID=55>Registro</LABEL></TD>" & vbCrLf)
        Response.Write("			<TD>")

        mobjValues.BlankPosition = False
        If Request.QueryString.Item("TypeList") <> vbNullString Then
            mobjValues.TypeList = CShort(Request.QueryString.Item("TypeList"))
            mobjValues.List = Request.QueryString.Item("List")
        End If
        Response.Write(mobjValues.PossiblesValues("cbeCertype", "Table5632", eFunctions.Values.eValuesType.clngComboType, Request.QueryString.Item("sCertypeQuery"),  ,  ,  ,  ,  , "ChangeControl(""cbeCertype"",this)", CDbl(Request.QueryString.Item("sCertypeQuery")) <> 0,  ,"Tipo de registro"))

        Response.Write("" & vbCrLf)
        Response.Write("			</TD>" & vbCrLf)
        Response.Write("		</TR> " & vbCrLf)
        Response.Write("        <TR>" & vbCrLf)
        Response.Write("		    <TD><LABEL ID=56>Ramo</LABEL></TD>" & vbCrLf)
        Response.Write("            <TD>")


        Response.Write(mobjValues.BranchControl("cbeBranch","Ramo comercial al cual pertenece la póliza", Request.QueryString.Item("nBranch"), "valProduct"))


        Response.Write("</TD>" & vbCrLf)
        Response.Write("			<TD>&nbsp;</TD>" & vbCrLf)
        Response.Write("			<TD><LABEL ID=57>Producto</LABEL></TD>" & vbCrLf)
        Response.Write("            <TD>")


        Response.Write(mobjValues.ProductControl("valProduct","Producto al cual pertenece la póliza", Request.QueryString.Item("nBranch"), eFunctions.Values.eValuesType.clngWindowType, Request.QueryString.Item("nBranch") <> vbNullString, Request.QueryString.Item("nProduct")))


        Response.Write("</TD>" & vbCrLf)
        Response.Write("		</TR>" & vbCrLf)
        Response.Write("        <TR>" & vbCrLf)
        Response.Write("			<TD><DIV ID=""lblPolicy""><LABEL ID=58>Póliza</LABEL></DIV></TD>" & vbCrLf)
        Response.Write("            <TD>")


        Response.Write(mobjValues.NumericControl("tcnPolicy", 10,  ,  ,"Número identificativo de la cotización/propuesta/póliza", False, 0))


        Response.Write("</TD>" & vbCrLf)
        Response.Write("            <TD>&nbsp;</TD>" & vbCrLf)
        Response.Write("            <TD><LABEL ID=59>Certificado</LABEL></TD>" & vbCrLf)
        Response.Write("            <TD>")


        Response.Write(mobjValues.NumericControl("tcnCertif", 10,  ,  ,"Número identificativo del certificado", False, 0,  ,  ,  , "ChangeControl(""tcnCertif"",this)"))


        Response.Write("</TD>" & vbCrLf)
        Response.Write("		</TR>" & vbCrLf)
        Response.Write("		<TR>" & vbCrLf)
        Response.Write("			<TD><LABEL ID=60>Contratante</LABEL></TD>" & vbCrLf)
        Response.Write("			<TD COLSPAN=""2"">")


        Response.Write(mobjValues.ClientControl("dtcClientC", vbNullString,  ,"Código de contratante de la póliza"))


        Response.Write("</TD>" & vbCrLf)
        Response.Write("			<TD COLSPAN=""2"" CLASS=""HighLighted""><LABEL ID=61>Fecha de vigencia</LABEL></TD>" & vbCrLf)
        Response.Write("		</TR>" & vbCrLf)
        Response.Write("		<TR>" & vbCrLf)
        Response.Write("			<TD COLSPAN=""3""></TD>" & vbCrLf)
        Response.Write("			<TD COLSPAN=""2"" CLASS=""HorLine""></TD>			" & vbCrLf)
        Response.Write("		</TR>" & vbCrLf)
        Response.Write("		<TR>" & vbCrLf)
        Response.Write("			<TD><LABEL ID=62>Asegurado</LABEL></TD>" & vbCrLf)
        Response.Write("			<TD COLSPAN=""2"">")


        Response.Write(mobjValues.ClientControl("dtcClientA", vbNullString,  ,"Código de contratante de la póliza"))


        Response.Write("</TD>" & vbCrLf)
        Response.Write("			<TD><LABEL ID=63>Desde</LABEL></TD>" & vbCrLf)
        Response.Write("			<TD>")


        Response.Write(mobjValues.DateControl("tcdStartdate", vbNullString,  ,"Fecha de inicio de vigencia de la póliza",  ,  ,  , "ChangeControl(""tcdStartdate"", this)"))


        Response.Write("</TD>" & vbCrLf)
        Response.Write("		</TR>" & vbCrLf)
        Response.Write("		<TR>" & vbCrLf)
        Response.Write("			<TD><LABEL ID=64>Estado</LABEL></TD>" & vbCrLf)
        Response.Write("			<TD>")


        Response.Write(mobjValues.PossiblesValues("cbeStatus_pol", "Table181", eFunctions.Values.eValuesType.clngComboType,  ,  ,  ,  ,  ,  ,  ,  ,  ,"Estado de la póliza"))


        Response.Write("</TD>" & vbCrLf)
        Response.Write("			<TD>&nbsp;</TD>" & vbCrLf)
        Response.Write("			<TD><LABEL ID=65>Hasta</LABEL></TD>" & vbCrLf)
        Response.Write("			<TD>")


        Response.Write(mobjValues.DateControl("tcdExpirdat", vbNullString,  ,"Fecha de vencimiento de la póliza",  ,  ,  ,  , True))


        Response.Write("</TD>" & vbCrLf)
        Response.Write("		</TR>" & vbCrLf)
        Response.Write("	</TABLE>" & vbCrLf)
        Response.Write("	<TABLE WIDTH=100%>" & vbCrLf)
        Response.Write("		<TR>" & vbCrLf)
        Response.Write("			<TD COLSPAN=""5"" CLASS=""HighLighted""><LABEL ID=189>Tipo de póliza</LABEL></TD>" & vbCrLf)
        Response.Write("		</TR>" & vbCrLf)
        Response.Write("		<TR>" & vbCrLf)
        Response.Write("			<TD COLSPAN=""5"" CLASS=""HorLine""></TD>" & vbCrLf)
        Response.Write("		</TR>" & vbCrLf)
        Response.Write("		<TR>" & vbCrLf)
        Response.Write("            <TD>")


        Response.Write(mobjValues.OptionControl(66, "optType","Individual",  , "1", "ChangeControl(""optType"",this)",,,vbNullString))


        Response.Write("</TD>" & vbCrLf)
        Response.Write("            <TD>")


        Response.Write(mobjValues.OptionControl(67, "optType","Colectivo",  , "2", "ChangeControl(""optType"",this)",,,vbNullString))


        Response.Write("</TD>" & vbCrLf)
        Response.Write("            <TD>")


        Response.Write(mobjValues.OptionControl(68, "optType","Multilocalidad",  , "3", "ChangeControl(""optType"",this)",,,vbNullString))


        Response.Write("</TD>" & vbCrLf)
        Response.Write("            <TD>&nbsp;</TD>" & vbCrLf)
        Response.Write("            <TD>")


        Response.Write(mobjValues.OptionControl(69, "optType","Todos", "1", "4", "ChangeControl(""optType"",this)",,,vbNullString))


        Response.Write("</TD>" & vbCrLf)
        Response.Write("		</TR>" & vbCrLf)
        Response.Write("    </TABLE>" & vbCrLf)
        Response.Write("    <IFRAME NAME=""fraGrid"" SRC=""/VTimeNet/Common/Blank.htm"" WIDTH=""100%"" HEIGHT=""30%"" SCROLLING=AUTO FRAMEBORDER=""0"">" & vbCrLf)
        Response.Write("	</IFRAME>" & vbCrLf)
        Response.Write("	<BR><BR>" & vbCrLf)
        Response.Write("	<TABLE WIDTH=100%>" & vbCrLf)
        Response.Write("		<TR>" & vbCrLf)
        Response.Write("			<TD CLASS=""HorLine"" COLSPAN=""4""></TD>" & vbCrLf)
        Response.Write("		</TR>" & vbCrLf)
        Response.Write("		<TR>" & vbCrLf)
        Response.Write("			<TD WIDTH=""12%"">")


        Response.Write(mobjValues.ButtonBackNext( , True, True))


        Response.Write("</TD>" & vbCrLf)
        Response.Write("			<TD WIDTH=""10%"">")

        With Response
            .Write(mobjValues.ButtonAbout("GE010", "GE010"))
            .Write(mobjValues.ButtonHelp("GE010"))
        End With

        Response.Write("" & vbCrLf)
        Response.Write("			<TD WIDTH=""65%""><LABEL ID=-1><B><DIV ID=""lblWaitProcess""></DIV></B></LABEL></TD>" & vbCrLf)
        Response.Write("			<TD ALIGN=""Right"">")


        Response.Write(mobjValues.ButtonAcceptCancel("insAccept()", "top.close()", False))


        Response.Write("</TD>" & vbCrLf)
        Response.Write("		</TR>" & vbCrLf)
        Response.Write("	</TABLE>")

        ' TODO
        '	Response.Write("<SCRIPT>ChangeControl(""cbeCertype"", self.document.forms[0].cbeCertype)</" & "Script>")
    End Sub

</script>
<%Response.Expires = -1
mobjValues = New eFunctions.Values
mobjValues.sCodisplPage = "GE010"
%>
<HTML>
<HEAD>
	<META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>

<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>

<%
With Response
        .Write(mobjValues.StyleSheet())
        .Write(mobjValues.ShowWindowsName("GE010", "Valores posibles de póliza"))
End With
%>
<SCRIPT>
    var sNextQuery = '';

        //TODO
        //top.document.title = <%=Request.QueryString.Item("sWindowDescript")%>;
        top.document.title = "Valores posibles de póliza";

    //- Variable para el control de versiones
    document.VssVersion = "$$Revision: 4 $|$$Date: 22/09/04 11:06a $|$$Author: Fbonilla $"

    //% ChangeControl: se maneja el cambio de valor de lo campos de la página
    //-------------------------------------------------------------------------------------------
    function ChangeControl(Option, Field) {
        //-------------------------------------------------------------------------------------------
        with (self.document.forms[0]) {
            switch (Option) {
                //+ Se actualiza el título del campo "Póliza", dependiendo del tipo de registro que  
                //+ se seleccione 
                case "cbeCertype":
                    if (Field.value == 0 ||
				   Field.value == 2)
                        UpdateDiv('lblPolicy', 'Póliza', '');
                    else
                        if (Field.value == 3 ||
					   Field.value == 4 ||
					   Field.value == 5)
                            UpdateDiv('lblPolicy', 'Cotización', '');
                        else
                            UpdateDiv('lblPolicy', 'Propuesta', '');
                    break;
                //+ Se habilita el campo "Fecha de vigencia - hasta", si el campo "Fecha de vigencia - desde" 
                //+ está lleno 
                case "tcdStartdate":
                    tcdExpirdat.disabled = (Field.value == '') ? true : false;
                    btn_tcdExpirdat.disabled = tcdExpirdat.disabled;
                    if (tcdExpirdat.disabled)
                        tcdExpirdat.value = '';
                    break;
                //+ Si el número de certificado es mayor a cero (0), se deshabilita la opción para póliza 
                //+ individual.  Si al hacer esto no existe ninguna opción marcada, se selecciona la opción 
                //+ "Colectivo" 
                case "tcnCertif":
                    optType[0].disabled = (Field.value == '' || Field.value == 0) ? false : true;
                    if (optType[0].disabled)
                        optType[0].checked = false;
                    break;
                //+ Si sel selecciona "Tipo de Póliza - Individual", se deshabilita el número de certificado 
                //+ y se muestra cero (0) por defecto.  En caso contrario se blanquea el campo 
                case "optType":
                    if (Field.value == 1) {
                        tcnCertif.value = 0;
                        tcnCertif.disabled = true;
                    }
                    else {
                        tcnCertif.disabled = false;
                        optType[0].disabled = (tcnCertif.value == '' || tcnCertif.value == 0) ? false : true;
                    }
            }
        }
    }
    //% insAccept: se realizan las acciones al aceptar la ventana
    //-------------------------------------------------------------------------------------------
    function insAccept() {
        //-------------------------------------------------------------------------------------------
        with (self.document.forms[0]) {
            //+ El tipo de registro debe estar lleno
            if (cbeCertype.value == 0)
                alert('Err. 10214: ' + 'Indique el tipo de registro')
            else
            //+ Alguno de los campos debe estar lleno
                if (cbeCompany.value == 0 &&
			   cbeBranch.value == 0 &&
			   valProduct.value == '' &&
			   (tcnPolicy.value == '' ||
			    tcnPolicy.value == 0) &&
			   tcnCertif.value == '' &&
			   dtcClientC.value == '' &&
			   dtcClientA.value == '' &&
			   tcdStartdate.value == '' &&
			   cbeStatus_pol.value == 0 &&
			   tcdExpirdat.value == '')
                    alert('Err. 1068: ' + 'Debe indicar la condición de búsqueda')
                else {
                    //+ Se realiza la búsqueda y se muestran los datos en el grid
                    ShowMessage()
                    self.document.frames["fraGrid"].location = 'PolicyQueryGrid.aspx?Type=PopUp&FieldPolicy=<%=Request.QueryString.Item("FieldPolicy")%>&FieldBranch=<%=Request.QueryString.Item("FieldBranch")%>&FieldProduct=<%=Request.QueryString.Item("FieldProduct")%>&FieldCertif=<%=Request.QueryString.Item("FieldCertif")%>' + '&nCompany=' + cbeCompany.value + '&sCertype=' + cbeCertype.value + '&nBranch=' + cbeBranch.value + '&nProduct=' + valProduct.value + '&nPolicy=' + tcnPolicy.value + '&nCertif=' + tcnCertif.value + '&sClientC=' + dtcClientC.value + '&sClientA=' + dtcClientA.value + '&dStartdate=' + tcdStartdate.value + '&sStatus_pol=' + cbeStatus_pol.value + '&dExpirdat=' + tcdExpirdat.value + '&sPolitype=' + ((optType[0].checked) ? 1 : (optType[1].checked) ? 2 : (optType[3].checked) ? 4 : 3)
                }
        }
    }

    //% ShowMessage: se muestra el mensaje de espera mientras se realiza la búsqueda
    //-------------------------------------------------------------------------------------------
    function ShowMessage() {
        //-------------------------------------------------------------------------------------------
        // TODO
        //UpdateDiv('lblWaitProcess', '<MARQUEE>' + moMSGGenFunctions.c_waitprocess + '</MARQUEE>', '');
        UpdateDiv('lblWaitProcess', '<MARQUEE>' + 'Procesando por favor espere...' + '</MARQUEE>', '');
    }

    //%	MoveRecord: se manejan las acciones "Anterior" y "Próximo" de la página
    //-------------------------------------------------------------------------------------------
    function MoveRecord(sWay) {
        //-------------------------------------------------------------------------------------------
        var lstrLocation = '';
        ShowMessage()
        with (self.document) {
            lstrLocation = frames["fraGrid"].location.href;
            lstrLocation = lstrLocation.replace(/&nCompany_First=.*/, '');
            switch (sWay) {
                case "Next":
                    lstrLocation += sNextQuery + '&sDirection=Next';
                    break;
                case "Back":
                    lstrLocation += sNextQuery + '&sDirection=Back';
            }
            cmdNext.disabled = true;
            cmdBack.disabled = true;
            frames["fraGrid"].location = lstrLocation;
        }
    }
</SCRIPT>
</HEAD>
<BODY>
<FORM METHOD=POST ACTION="PolicyQuery.aspx">
<%
Call ShowField()
%>
</FORM>
</BODY>
</HTML>
<%
mobjValues = Nothing
%>





