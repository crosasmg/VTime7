<%@ Page Language="VB" Explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon"
    EnableViewState="false" %>

<%@ Import Namespace="eFunctions" %>
<%@ Import Namespace="eClaim" %>
<%@ Import Namespace="eAgent" %>
<script language="VB" runat="Server">

    '- Objeto para el manejo de las funciones generales de carga de valores
    Dim mobjValues As eFunctions.Values
    Dim mobjMenu As eFunctions.Menues


    '% insPreSI775_A: Se cargan los controles de la página
    '--------------------------------------------------------------------------------------------
    Private Sub insPreSI775_A()
        '--------------------------------------------------------------------------------------------
        Dim lclsFire_budget As eClaim.Fire_budget
        Dim ldblAmount As Integer
        Dim ldblIva As Integer
        Dim lstrAction As String
        Dim ldblAmountTotal As Byte
        Dim ldtmBudg_date As Object
	
        ldblAmount = 0
        ldblIva = 0
        ldblAmountTotal = 0
        lstrAction = ""
        lclsFire_budget = New eClaim.Fire_budget
        If lclsFire_budget.Find_Budget(mobjValues.StringToType(Request.QueryString("nServ_order"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString("nClaim"), eFunctions.Values.eTypeData.etdDouble)) Then
            ldblAmount = mobjValues.StringToType(CStr(lclsFire_budget.nAmount), eFunctions.Values.eTypeData.etdDouble, True)
            ldblIva = mobjValues.StringToType(CStr(lclsFire_budget.nIVA), eFunctions.Values.eTypeData.etdDouble, True)
            ldtmBudg_date = lclsFire_budget.dBudg_date
            If ldblAmount = eRemoteDB.Constants.intNull Then
                ldblAmount = 0
            End If
            If ldblIva = eRemoteDB.Constants.intNull Then
                ldblIva = 0
            End If
            If ldtmBudg_date = eRemoteDB.Constants.dtmNull Then
                'UPGRADE_WARNING: Date was upgraded to Today and has a new behavior. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1041.aspx'
                ldtmBudg_date = Today
            End If
            If lclsFire_budget.nExist = 1 Then
                lstrAction = "Update"
            Else
                lstrAction = "Add"
            End If
        Else
            ldblIva = 0
            lstrAction = "Add"
            'UPGRADE_WARNING: Date was upgraded to Today and has a new behavior. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1041.aspx'
            ldtmBudg_date = Today
        End If
	
        Dim lclsTax_Fixval As eAgent.tax_fixval
        If ldblIva = 0 Then
            '+ Se obtiene el porcentaje fijo de IVA (Tabla Tax_Fixval) 
            lclsTax_Fixval = New eAgent.tax_fixval
            '+ Se obtiene el porcentaje fijo de IVA (Tabla Tax_Fixval) 
            If lclsTax_Fixval.Find(1, ldtmBudg_date) Then
                ldblIva = mobjValues.StringToType(CStr(lclsTax_Fixval.nPercent), eFunctions.Values.eTypeData.etdDouble, True)
            End If
            'UPGRADE_NOTE: Object lclsTax_Fixval may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
            lclsTax_Fixval = Nothing
        End If
	
        '+ Campos puntuales de la ventana:  
	
        Response.Write("  " & vbCrLf)
        Response.Write("		<BR>" & vbCrLf)
        Response.Write("		<TABLE WIDTH=100%>" & vbCrLf)
        Response.Write("		")

        If Request.QueryString("nMainAction") = eFunctions.Menues.TypeActions.clngActionUpdate Then
            Response.Write("" & vbCrLf)
            Response.Write("				<TR>" & vbCrLf)
            Response.Write("					<TD COLSPAN=""5"" CLASS=""HighLighted""><LABEL ID=0>Acción a ejecutar</LABEL></TD>" & vbCrLf)
            Response.Write("				</TR>" & vbCrLf)
            Response.Write("				<TR>" & vbCrLf)
            Response.Write("				    <TD COLSPAN=""5"" CLASS=""HorLine""></TD>" & vbCrLf)
            Response.Write("				</TR>            " & vbCrLf)
            Response.Write("				<TR>" & vbCrLf)
            Response.Write("					<TD COLSPAN=""2"">")


            Response.Write(mobjValues.OptionControl(1, "optAction", "Aprobar", "1", CStr(1)))


            Response.Write("</TD>" & vbCrLf)
            Response.Write("					<TD>&nbsp;</TD>" & vbCrLf)
            Response.Write("					<TD COLSPAN=""2"">")


            Response.Write(mobjValues.OptionControl(2, "optAction", "Rechazar", , CStr(2)))


            Response.Write("</TD>" & vbCrLf)
            Response.Write("				</TR>" & vbCrLf)
            Response.Write("				<TR>" & vbCrLf)
            Response.Write("					<TD COLSPAN=""5"" CLASS=""HeightRow""></TD>" & vbCrLf)
            Response.Write("				</TR>" & vbCrLf)
            Response.Write("				<TR>" & vbCrLf)
            Response.Write("					<TD COLSPAN=""5"" CLASS=""HeightRow""></TD>" & vbCrLf)
            Response.Write("				</TR>            " & vbCrLf)
            Response.Write("		")

        End If
	
        If Request.QueryString("nMainAction") = 302 Then
            mobjValues.ActionQuery = True
        Else
            mobjValues.ActionQuery = Request.QueryString("nMainAction") = eFunctions.Menues.TypeActions.clngActionQuery
        End If
        If mobjValues.ActionQuery Then
            If ldblIva > 0 Then
                ldblAmountTotal = mobjValues.StringToType(CStr(((lclsFire_budget.nHand_amount + lclsFire_budget.nMat_amount) - (lclsFire_budget.nDeduc_amount + lclsFire_budget.nDeprec_amount)) * ((ldblIva / 100) + 1)), eFunctions.Values.eTypeData.etdDouble, True)
            Else
                ldblAmountTotal = mobjValues.StringToType(CStr((lclsFire_budget.nHand_amount + lclsFire_budget.nMat_amount) - (lclsFire_budget.nDeduc_amount + lclsFire_budget.nDeprec_amount)), eFunctions.Values.eTypeData.etdDouble, True)
            End If
        End If
	
        Response.Write("" & vbCrLf)
        Response.Write("			<TR>" & vbCrLf)
        Response.Write("				<TD><LABEL>Nota</LABEL></TD>" & vbCrLf)
        Response.Write("		        <TD>")


        Response.Write(mobjValues.ButtonNotes("SCA2-PS", lclsFire_budget.nNotenum, False, False, , , , , , "btnNotenum"))


        Response.Write("</TD>" & vbCrLf)
        Response.Write("				<TD>&nbsp;</TD>" & vbCrLf)
        Response.Write("				<TD><LABEL>Imagen</LABEL></TD>" & vbCrLf)
        Response.Write("		        <TD>")


        Response.Write(mobjValues.ButtonImages("SCA10-3", lclsFire_budget.nNumimages, False, False))


        Response.Write(" </TD>" & vbCrLf)
        Response.Write("			<TR>" & vbCrLf)
        Response.Write("			<TR>" & vbCrLf)
        Response.Write("				<TD><LABEL>Descripción</LABEL></TD>" & vbCrLf)
        Response.Write("				<TD COLSPAN=4>")


        Response.Write(mobjValues.TextControl("tctItem", 60, lclsFire_budget.sItem, , "Texto Libre"))


        Response.Write("</TD>" & vbCrLf)
        Response.Write("			<TR>" & vbCrLf)
        Response.Write("			<TR>" & vbCrLf)
        Response.Write("				<TD><LABEL>Materiales</LABEL></TD>" & vbCrLf)
        Response.Write("				<TD>")


        Response.Write(mobjValues.NumericControl("tcnMat_amount", 18, CStr(lclsFire_budget.nMat_amount), , "Monto neto correspondiente al valor de materiales", True, 6, , , , "CalculateTotal();"))


        Response.Write("</TD>" & vbCrLf)
        Response.Write("				<TD>&nbsp;</TD>" & vbCrLf)
        Response.Write("				<TD><LABEL>Mano de obra</LABEL></TD>  " & vbCrLf)
        Response.Write("				<TD>")


        Response.Write(mobjValues.NumericControl("tcnHand_amount", 18, CStr(lclsFire_budget.nHand_amount), , "Monto neto correspondiente a valor de mano de obra", True, 6, , , , "CalculateTotal();"))


        Response.Write("</TD>" & vbCrLf)
        Response.Write("			</TR>" & vbCrLf)
        Response.Write("			<TR>" & vbCrLf)
        Response.Write("				<TD><LABEL>Deducciones</LABEL></TD>" & vbCrLf)
        Response.Write("				<TD>")


        Response.Write(mobjValues.NumericControl("tcnDeprec_amount", 18, CStr(lclsFire_budget.nDeprec_amount), , "Monto correspondiente al valor de depreciación", True, 6, , , , "CalculateTotal();"))


        Response.Write("</TD>" & vbCrLf)
        Response.Write("				<TD>&nbsp;</TD>" & vbCrLf)
        Response.Write("				<TD><LABEL>Deducibles</LABEL></TD>  " & vbCrLf)
        Response.Write("				<TD>")


        Response.Write(mobjValues.NumericControl("tcnDeduc_amount", 18, CStr(lclsFire_budget.nDeduc_amount), , "Monto correspondiente al valor deducible", True, 6, , , , "CalculateTotal();"))


        Response.Write("</TD>" & vbCrLf)
        Response.Write("			</TR>" & vbCrLf)
        Response.Write("			<TR>  " & vbCrLf)
        Response.Write("				<TD><LABEL>Total neto</LABEL></TD>  " & vbCrLf)
        Response.Write("				<TD>")


        Response.Write(mobjValues.NumericControl("tcnAmount", 18, CStr(ldblAmount), , "Valor total de los repuestos incluídos en la cotización", True, 6, , "", "", , True))


        Response.Write("</TD>  " & vbCrLf)
        Response.Write("				<TD>&nbsp;</TD>" & vbCrLf)
        Response.Write("				<TD><LABEL>I.V.A.</LABEL></TD>  " & vbCrLf)
        Response.Write("				<TD>")


        Response.Write(mobjValues.NumericControl("tcnIVA", 5, CStr(ldblIva), False, "Porcentaje de impuesto", True, 2, , , , , True))


        Response.Write("</TD>  " & vbCrLf)
        Response.Write("			</TR> " & vbCrLf)
        Response.Write("			<TR> " & vbCrLf)
        Response.Write("				<TD><LABEL>Total</LABEL></TD> " & vbCrLf)
        Response.Write("				<TD COLSPAN=""4"">")


        Response.Write(mobjValues.NumericControl("tcnTotal", 18, CStr(ldblAmountTotal), False, "Monto total de la cotización", True, 6, , , , , True))


        Response.Write("</TD> " & vbCrLf)
        Response.Write("			</TR> " & vbCrLf)
        Response.Write("		</TABLE> " & vbCrLf)
        Response.Write("	")

	
        Response.Write(mobjValues.HiddenControl("tctAction", lstrAction))
        If Not mobjValues.ActionQuery Then
            Response.Write("<script>CalculateTotal();</" & "Script>")
        End If
        'UPGRADE_NOTE: Object lclsFire_budget may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
        lclsFire_budget = Nothing
    End Sub

</script>
<%Response.Expires = -1

    mobjValues = New eFunctions.Values
    mobjMenu = New eFunctions.Menues
%>
<html>
<head>
    <meta name="GENERATOR" content="Microsoft Visual Studio 6.0">
    <script type="text/javascript" src="/VTimeNet/Scripts/GenFunctions.js"></script>
    <script type="text/javascript" src="/VTimeNet/Scripts/Constantes.js"></script>
    <%
        With Response
            .Write(mobjValues.StyleSheet())
            If Request.QueryString("Type") <> "PopUp" Then
                .Write(mobjMenu.setZone(2, "SI775_A", Request.QueryString("sWindowDescript"), Request.QueryString("nWindowTy")))
                'UPGRADE_NOTE: Object mobjMenu may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
                mobjMenu = Nothing
                Response.Write("<script>" & Request.QueryString("nMainAction") & "</script>")
            End If
        End With
    %>
    <script>
        //% CalculateTotal: Calcula el total una vez que se añaden los montos
        //------------------------------------------------------------------------------------- 
        function CalculateTotal() {
            //------------------------------------------------------------------------------------- 
            var ldblIVA = 0;
            var ldblAmount = 0;
            var ldblMat_Amount = 0;
            var ldblHand_Amount = 0;
            var ldblDeduc_Amount = 0;
            var ldblDeprec_Amount = 0;

            with (self.document.forms[0]) {
                if (tcnIVA.value != "")
                    ldblIVA = insConvertNumber(tcnIVA.value);
                if (tcnMat_amount.value != "")
                    ldblMat_Amount = insConvertNumber(tcnMat_amount.value);
                if (tcnHand_amount.value != "")
                    ldblHand_Amount = insConvertNumber(tcnHand_amount.value);
                if (tcnDeprec_amount.value != "")
                    ldblDeduc_Amount = insConvertNumber(tcnDeprec_amount.value);
                if (tcnDeduc_amount.value != "")
                    ldblDeprec_Amount = insConvertNumber(tcnDeduc_amount.value);

                tcnMat_amount.value = VTFormat(ldblMat_Amount, '', '', '', 6, true);
                tcnHand_amount.value = VTFormat(ldblHand_Amount, '', '', '', 6, true);
                tcnDeprec_amount.value = VTFormat(ldblDeduc_Amount, '', '', '', 6, true);
                tcnDeduc_amount.value = VTFormat(ldblDeprec_Amount, '', '', '', 6, true);

                if (ldblIVA > 0) {
                    ldblIVA = (ldblIVA / 100) + 1;
                    ldblAmount = (ldblAmount + ldblMat_Amount + ldblHand_Amount) - (ldblDeduc_Amount + ldblDeprec_Amount);
                    ldblTotal = (ldblAmount) * ldblIVA;
                    tcnTotal.value = VTFormat(ldblTotal, '', '', '', 6, true);
                    tcnAmount.value = VTFormat(ldblAmount, '', '', '', 6, true);
                }
                else {
                    ldblAmount = (ldblAmount + ldblMat_Amount + ldblHand_Amount) - (ldblDeduc_Amount + ldblDeprec_Amount);
                    ldblTotal = ldblAmount;
                    tcnTotal.value = VTFormat(ldblTotal, '', '', '', 6, true);
                    tcnAmount.value = VTFormat(ldblTotal, '', '', '', 6, true);
                }
            }
        }

    </script>
</head>
<body onunload="closeWindows();">
    <form method="POST" id="FORM" name="SI775_A" action="ValClaim.aspx?nServ_order=<%=Request.QueryString("nServ_order")%>&nMainAction=<%=Request.QueryString("nMainAction")%>&nClaim=<%=Request.QueryString("nClaim")%>">
    <%
        Response.Write(mobjValues.ShowWindowsName("SI775_A", Request.QueryString("sWindowDescript")))
        Call insPreSI775_A()
        'UPGRADE_NOTE: Object mobjValues may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
        mobjValues = Nothing
    %>
    </form>
</body>
</html>
