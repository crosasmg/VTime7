<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eGeneral" %>
<%@ Import namespace="eClaim" %>
<script language="VB" runat="Server">
'^Begin Header Block VisualTimer Utility 1.1 7/4/03 11.34.48
Dim mobjNetFrameWork As eNetFrameWork.Layout
'~End Header Block VisualTimer Utility

'- Objeto para el manejo de las funciones generales de carga de valores.
Dim mobjValues As eFunctions.Values

'- Objeto para el manejo de las zonas de la página    
Dim mobjMenu As eFunctions.Menues

'- Objeto para el manejo del Grid de la Pagina    
Dim mobjGrid As eFunctions.Grid
Dim mstrAlert As String
Dim mobjErrors As eGeneral.GeneralFunction

'- Variables que controlan los totales
Dim mdblTotPend As Double
Dim mdblTotPay As Double
Dim mdblTotTaxes As Double
Dim mdblTotRecover As Double
Dim mdblTotRecoverExp As Double
Dim mdblTotClaimCost As Double
Dim mdblTotPremium As Double
Dim mdblTotClaimPercent As Double



'% insDefineHeader:Este procedimiento se encarga de definir las columnas del grid y de habilitar
'% o inhabilitar los botones de añadir y eliminar.
'-----------------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'-----------------------------------------------------------------------------------------
	
	mobjGrid = New eFunctions.Grid
	'^Begin Body Block VisualTimer Utility 1.1 7/4/03 11.34.48
	mobjGrid.sSessionID = Session.SessionID
	mobjGrid.nUsercode = Session("nUsercode")
	'~End Body Block VisualTimer Utility
	
	mobjGrid.sCodisplPage = "sic002"
	Call mobjGrid.SetWindowParameters(Request.QueryString("sCodispl"), Request.QueryString("sWindowDescript"), Request.QueryString("nWindowTy"))
	
	'+ Se definen las columnas del Grid.
	
	With mobjGrid
		.Codispl = Request.QueryString("sCodispl")
		.Codisp = "SIC002"
	End With
	
	With mobjGrid.Columns
		Call .AddNumericColumn(0, "Siniestro", "tcnClaim", 10,  ,  , "Número identificativo del siniestro presentaqdo en la línea",  ,  ,  , CStr(True))
		Call .AddPossiblesColumn(0, "Caso", "valCase", "Table692", eFunctions.Values.eValuesType.clngWindowType,  ,  ,  ,  ,  ,  , True,  , "Corresponde a cada una de las reclamaciones que se realiza en un siniestro")
		Call .AddTextColumn(0, "Fecha de Ocurrencia", "tctOccurdate", 30, vbNullString,  , "Fecha de ocurrencia del siniestro mostrado en la línea",  ,  ,  , True)
		Call .AddTextColumn(0, "Estado del siniestro", "tctStatClaim", 30, vbNullString,  , "Estado del siniestro mostrado en la línea",  ,  ,  , True)
		Call .AddNumericColumn(0, "Pendiente", "tcnLoc_out_am", 18,  ,  , "Monto de reserva que se encuentra pendiente de pago", True, 6,  , CStr(True))
		Call .AddNumericColumn(0, "Pagado", "tcnLoc_pay_am", 18,  ,  , "Monto que hasta el momento ha sido pagado en el siniestro", True, 6,  , CStr(True))
		Call .AddNumericColumn(0, "Impuestos", "tcnTaxes", 18,  ,  , "Monto que hasta el momento ha sido retenido (en los pagos) por conceptos de impuestos", True, 6,  , CStr(True))
		'       Call .AddTextColumn     (0, "Tipo de Recobro", "tctRecoverTyp", 30 ,vbnullstring,, "Tipo de recobro",,,,true) 
		Call .AddHiddenColumn("tctRecoverTyp", vbNullString)
		Call .AddNumericColumn(0, "Recuperación", "tcnRecuper", 18,  ,  , "Monto que hasta el momento ha sido recuperado en el siniestro", True, 6,  , CStr(True))
		Call .AddNumericColumn(0, "Salvataje", "tcnSalvata", 18,  ,  , "Monto que hasta el momento ha sido recuperado por salvataje en el siniestro", True, 6,  , CStr(True))
		Call .AddNumericColumn(0, "Recobrado", "tcnLoc_rec_am", 18,  ,  , "Monto que hasta el momento ha sido recobrado (recuperado) en el siniestro", True, 6,  , CStr(True))
		Call .AddNumericColumn(0, "Gastos de Recobro", "tcnLoc_cos_re", 18,  ,  , "Monto que hasta el momento se ha gastado por concepto de recobro/recuperación/salvamento en el siniestro", True, 6,  , CStr(True))
		Call .AddNumericColumn(0, "Costo del siniestro", "tcnClaimCost", 18,  ,  , "Indica lo que hasta ahora está costando el siniestro", True, 6,  , CStr(True))
		Call .AddNumericColumn(0, "Deducible", "tcnDeduc", 18,  ,  , "Monto del deducible pagado por el asegurado", True, 6,  , CStr(True))
		Call .AddClientColumn(0, "Liquidador", "valClient", vbNullString,  , "Nombre del liquidador que evalúa los daños del siniestro",  , True)
		Call .AddAnimatedColumn(0, "Mas Información", "btnClaimSequence", "/VTimeNet/images/lupa.bmp", "Mas Información")
	End With
	
	'+ Se definen las propiedades generales del grid.
	With mobjGrid
		.Codispl = "SIC002"
		.DeleteButton = False
		.AddButton = False
		.Columns("Sel").GridVisible = False
		.ActionQuery = True
	End With
End Sub

'**% insPreSIC002: This function allows to make the reading of the main table of the transaction.  
'% insPreSIC002: Esta función permite realizar la lectura de la tabla principal de la transacción.
'-----------------------------------------------------------------------------------------
Private Sub insPreSIC002()
	'-----------------------------------------------------------------------------------------
	Dim lintCount As Short
	Dim lclsClaim As eClaim.Claim
	Dim lcolClaims As eClaim.Claims
	Dim ldblPend As Integer
	Dim ldblPay As Integer
	Dim ldblTaxes As Integer
	Dim ldblRecover As Integer
	Dim ldblRecoverExp As Integer
	Dim ldblClaimCost As Integer
	Dim ldblPremium As Double
	
	lclsClaim = New eClaim.Claim
	lcolClaims = New eClaim.Claims
	
	mdblTotPend = 0
	mdblTotPay = 0
	mdblTotTaxes = 0
	mdblTotRecover = 0
	mdblTotRecoverExp = 0
	mdblTotClaimCost = 0
	mdblTotPremium = 0
	mdblTotClaimPercent = 0
	
	If lcolClaims.FindSIC002("2", CInt(Session("nBranch")), CInt(Session("nProduct")), CInt(Session("nPolicy")), CInt(Session("nCertif")), CDate(Session("dOccurdate"))) Then
		lintCount = 0
		For	Each lclsClaim In lcolClaims
			With lclsClaim
				ldblPend = 0
				ldblPay = 0
				ldblTaxes = 0
				ldblRecover = 0
				ldblRecoverExp = 0
				ldblClaimCost = 0
				ldblPremium = 0
				
				mobjGrid.Columns("tcnClaim").DefValue = CStr(.nClaim)
				mobjGrid.Columns("valcase").DefValue = CStr(.nDeman_type)
				mobjGrid.Columns("tctOccurdate").DefValue = CStr(.dOccurdat)
				mobjGrid.Columns("tctStatClaim").DefValue = .sStaClaimDes
				mobjGrid.Columns("tcnLoc_out_am").DefValue = CStr(.nLoc_out_am)
				mobjGrid.Columns("tcnLoc_pay_am").DefValue = CStr(.nLoc_pay_am)
				mobjGrid.Columns("tcnTaxes").DefValue = CStr(.nTax_amo)
				mobjGrid.Columns("tctRecoverTyp").DefValue = .sRecover_Typ
				mobjGrid.Columns("tcnLoc_rec_am").DefValue = CStr(.nLoc_rec_am)
				mobjGrid.Columns("tcnLoc_cos_re").DefValue = CStr(.nLoc_cos_re)
				mobjGrid.Columns("tcnClaimCost").DefValue = .nClaimCost
				mobjGrid.Columns("tcnDeduc").DefValue = CStr(.nAmount)
				mobjGrid.Columns("tcnRecuper").DefValue = CStr(.nRecuper)
				mobjGrid.Columns("tcnSalvata").DefValue = CStr(.nSalvata)
				If .sClient2 = vbNullString Then
					mobjGrid.Columns("valClient").DefValue = " "
				Else
					mobjGrid.Columns("valClient").DefValue = .sClient2
				End If
				
				If CStr(.sStaclaim) = "6" Then
					mstrAlert = "Err. 4051 " & mobjErrors.insLoadMessage(4051)
					mobjGrid.Columns("btnClaimSequence").HRefScript = "javascript:insMessaged('" & mstrAlert & "')"
				Else
					mobjGrid.Columns("btnClaimSequence").HRefScript = "ShowPopUp('/VTimeNet/Common/secWHeader.aspx?sModule=Claim&sProject=ClaimSeq&sCodispl=SI001&sOriginalForm=SIC002&SIC002=" & .nClaim & "&nClaim=" & .nClaim & "', 'Claim', 800, 550, 'yes', 'yes', 0, 0);"
				End If
				
				'Totales Acumulados
				'Pendientes
				ldblPend = mobjValues.StringToType(mobjGrid.Columns("tcnLoc_out_am").DefValue, eFunctions.Values.eTypeData.etdDouble)
				'Pagado            
				ldblPay = mobjValues.StringToType(mobjGrid.Columns("tcnLoc_pay_am").DefValue, eFunctions.Values.eTypeData.etdDouble)
				'Impuestos
				ldblTaxes = mobjValues.StringToType(mobjGrid.Columns("tcnTaxes").DefValue, eFunctions.Values.eTypeData.etdDouble)
				'Recobrado            
				ldblRecover = mobjValues.StringToType(mobjGrid.Columns("tcnLoc_rec_am").DefValue, eFunctions.Values.eTypeData.etdDouble)
				'Gastos de recobro            
				ldblRecoverExp = mobjValues.StringToType(mobjGrid.Columns("tcnLoc_cos_re").DefValue, eFunctions.Values.eTypeData.etdDouble)
				'Costo del siniestro             
				ldblClaimCost = mobjValues.StringToType(mobjGrid.Columns("tcnClaimCost").DefValue, eFunctions.Values.eTypeData.etdDouble)
				'Prima anual
				ldblPremium = .nPremium
				
				Response.Write(mobjGrid.DoRow())
			End With
			If ldblPend = eRemoteDB.Constants.intNull Then
				ldblPend = 0
			End If
			If ldblPay = eRemoteDB.Constants.intNull Then
				ldblPay = 0
			End If
			If ldblTaxes = eRemoteDB.Constants.intNull Then
				ldblTaxes = 0
			End If
			If ldblRecover = eRemoteDB.Constants.intNull Then
				ldblRecover = 0
			End If
			If ldblRecoverExp = eRemoteDB.Constants.intNull Then
				ldblRecoverExp = 0
			End If
			If ldblClaimCost = eRemoteDB.Constants.intNull Then
				ldblClaimCost = 0
			End If
			If ldblPremium = eRemoteDB.Constants.intNull Then
				ldblPremium = 0
			End If
			
			mdblTotPend = mdblTotPend + ldblPend
			mdblTotPay = mdblTotPay + ldblPay
			mdblTotTaxes = mdblTotTaxes + ldblTaxes
			mdblTotRecover = mdblTotRecover + ldblRecover
			mdblTotRecoverExp = mdblTotRecoverExp + ldblRecoverExp
			mdblTotClaimCost = mdblTotClaimCost + ldblClaimCost
			mdblTotPremium = mdblTotPremium + ldblPremium
			
			lintCount = lintCount + 1
			If lintCount = 200 Then
				Exit For
			End If
		Next lclsClaim
	End If
	
	Response.Write(mobjGrid.closeTable())
	
	'+ Se calcula el porcentaje de siniestralidad   
	If Not mdblTotClaimCost = 0 And Not mdblTotPremium = 0 Then
		mdblTotClaimPercent = (mdblTotClaimCost / mdblTotPremium) * 100
	Else
		mdblTotClaimPercent = 0
	End If
	
	'+ Se crean campos ocultos con el valor de los totales acumulados
	Response.Write(mobjValues.HiddenControl("hddnPend", CStr(mdblTotPend)))
	Response.Write(mobjValues.HiddenControl("hddnPay", CStr(mdblTotPay)))
	Response.Write(mobjValues.HiddenControl("hddnTaxes", CStr(mdblTotTaxes)))
	Response.Write(mobjValues.HiddenControl("hddnRecover", CStr(mdblTotRecover)))
	Response.Write(mobjValues.HiddenControl("hddnRecoverExp", CStr(mdblTotRecoverExp)))
	Response.Write(mobjValues.HiddenControl("hddnClaimCost", CStr(mdblTotClaimCost)))
	Response.Write(mobjValues.HiddenControl("hddnPremium", CStr(mdblTotPremium)))
	Response.Write(mobjValues.HiddenControl("hddnClaimPercent", CStr(mdblTotClaimPercent)))
	'UPGRADE_NOTE: Object lcolClaims may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
	lcolClaims = Nothing
	'UPGRADE_NOTE: Object lclsClaim may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
	lclsClaim = Nothing
End Sub

</script>
<%Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage("sic002")

mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 11.34.48
mobjValues.sSessionID = Session.SessionID
mobjValues.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjValues.sCodisplPage = "sic002"
mobjErrors = New eGeneral.GeneralFunction

%>
<HTML>
<HEAD>
    <META NAME = "GENERATOR" Content = "Microsoft Visual Studio 6.0">
	<%'UPGRADE_NOTE: Language element 'SCRIPT' was migrated to the same language element but still may have a different behavior. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1011.htm  %>
<%'UPGRADE_NOTE: The file '/VTimeNet/Scripts/GenFunctions.js' was not found in the migration directory. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1003.htm  %>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
	<%'UPGRADE_NOTE: Language element '#INCLUDE' was migrated to the same language element but still may have a different behavior. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1011.htm  %>
<%'UPGRADE_NOTE: The file '/VTimeNet/Includes/Constantes.aspx' was not found in the migration directory. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1003.htm  %>
<!-- #INCLUDE VIRTUAL="~/VTimeNet/Includes/Constantes.aspx" -->

	<%'UPGRADE_NOTE: Language element '#INCLUDE' was migrated to the same language element but still may have a different behavior. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1011.htm  %>
<%'UPGRADE_NOTE: The file '/VTimeNet/Includes/General.aspx' was not found in the migration directory. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1003.htm  %>
<!-- #INCLUDE VIRTUAL="~/VTimeNet/Includes/General.aspx" -->

<SCRIPT>
//+ Variable para el control de versiones
	document.VssVersion="$$Revision: 5 $|$$Date: 27/11/03 20:41 $|$$Author: Nvapla10 $"
</SCRIPT>
   <%
With Response
	.Write(mobjValues.StyleSheet())
	
	If Request.QueryString("Type") <> "PopUp" Then
		mobjMenu = New eFunctions.Menues
		'^Begin Body Block VisualTimer Utility 1.1 7/4/03 11.34.48
		mobjMenu.sSessionID = Session.SessionID
		mobjMenu.nUsercode = Session("nUsercode")
		'~End Body Block VisualTimer Utility
		.Write("<SCRIPT>var	nMainAction	= 0" & Request.QueryString("nMainAction") & "</SCRIPT>")
		.Write(mobjMenu.setZone(2, "SIC002", Request.QueryString("sWindowDescript"), Request.QueryString("nWindowTy")))
		'UPGRADE_NOTE: Object mobjMenu may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
		mobjMenu = Nothing
	End If
	
	If Request.QueryString("nMainAction") = eFunctions.Menues.TypeActions.clngActionQuery Then
		mobjValues.ActionQuery = True
	End If
End With
%>
	
<SCRIPT>
//% insCancel: Ejecuta rutinas necesarias en el momento de cancelar la página
//------------------------------------------------------------------------------------------
function insCancel()
//------------------------------------------------------------------------------------------
{		    
	return true;
}
//------------------------------------------------------------------------------------------
//insMessaged: Si el siniestro tiene estado 6 "Pendiente de completitud." no puede ser consultado.
//------------------------------------------------------------------------------------------
function insMessaged(Message)
{
  alert(Message);
}
</SCRIPT>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" ID="FORM" NAME="SIC002" ACTION="ValClaim.ASPX?sZone=2&nMainAction=<%=Request.QueryString("nMainAction")%>">

    	<%Response.Write(mobjValues.ShowWindowsName("SIC002", Request.QueryString("sWindowDescript")))%>
	<BR><BR>
    <%Call insDefineHeader()
Call insPreSIC002()%>
		<BR>
    	<TABLE WIDTH="100%">
			<TR>
			    <TD WIDTH="25%"><LABEL ID=0>Pendiente</LABEL></TD>
			    <TD><%=mobjValues.DIVControl("lblPend")%></TD>
			</TR>
			<TR>
			    <TD WIDTH="25%"><LABEL ID=0>Pagado</LABEL></TD>
			    <TD><%=mobjValues.DIVControl("lblPay")%></TD>
			</TR>
			<TR>
			 	<TD WIDTH="25%"><LABEL ID=0>Impuestos</LABEL></TD>
				<TD><%=mobjValues.DIVControl("lblTaxes")%>
			</TR>
			<TR>
			    <TD WIDTH="25%"><LABEL ID=0>Recobrado</LABEL></TD>
				<TD><%=mobjValues.DIVControl("lblRecover")%></TD>
			</TR>
			<TR>
			    <TD WIDTH="25%"><LABEL ID=0>Gastos de recobro</LABEL></TD>
				<TD><%=mobjValues.DIVControl("lblRecoverExp")%></TD>
			</TR>
			<TR>
			    <TD WIDTH="25%"><LABEL ID=0>Costo del siniestro</LABEL></TD>
				<TD><%=mobjValues.DIVControl("lblClaimCost")%></TD>
			</TR>
			<TR>
			    <TD WIDTH="25%"><LABEL ID=0>Prima anual</LABEL></TD>
				<TD><%=mobjValues.DIVControl("lblPremium")%></TD>
			</TR>
  			<TR>
			    <TD WIDTH="25%"><LABEL ID=0>Porcentaje de siniestralidad</LABEL></TD>
				<TD><%=mobjValues.DIVControl("lblClaimPercent")%></TD>
			</TR>
        </TABLE>
	<SCRIPT>
// Se agregan los valores a los campos ocultos de los otales acumulados.	
        document.getElementById("lblPend").innerHTML = VTFormat(document.forms[0].hddnPend.value,'','','',6);
        document.getElementById("lblPay").innerHTML = VTFormat(document.forms[0].hddnPay.value,'','','',6);
        document.getElementById("lblTaxes").innerHTML = VTFormat(document.forms[0].hddnTaxes.value,'','','',6);
        document.getElementById("lblRecover").innerHTML= VTFormat(document.forms[0].hddnRecover.value,'','','',6);
        document.getElementById("lblRecoverExp").innerHTML= VTFormat(document.forms[0].hddnRecoverExp.value,'','','',6);
        document.getElementById("lblClaimCost").innerHTML= VTFormat(document.forms[0].hddnClaimCost.value,'','','',6);
        document.getElementById("lblPremium").innerHTML= VTFormat(document.forms[0].hddnPremium.value,'','','',6);
        document.getElementById("lblClaimPercent").innerHTML= VTFormat(document.forms[0].hddnClaimPercent.value, '', '', '', 6) + ' %';
	</SCRIPT>
    <%'UPGRADE_NOTE: Object mobjGrid may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
mobjGrid = Nothing
'UPGRADE_NOTE: Object mobjValues may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
mobjValues = Nothing
'UPGRADE_NOTE: Object mobjErrors may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
mobjErrors = Nothing%>
</FORM>
</BODY>
</HTML>

<%'^Begin Footer Block VisualTimer Utility 1.1 7/4/03 11.34.48
Call mobjNetFrameWork.FinishPage("sic002")
'UPGRADE_NOTE: Object mobjNetFrameWork may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>




