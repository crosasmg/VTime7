<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
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
	
	mobjGrid.sCodisplPage = "sic005"
	Call mobjGrid.SetWindowParameters(Request.QueryString("sCodispl"), Request.QueryString("sWindowDescript"), Request.QueryString("nWindowTy"))
	
	'+ Se definen las columnas del Grid.
	
	With mobjGrid
		.Codispl = Request.QueryString("sCodispl")
		.Codisp = "SIC005"
	End With
	
	With mobjGrid.Columns
		Call .AddTextColumn(0, "Siniestro", "tctClaim", 10, vbNullString,  , "Número identificativo del siniestro sobre el cual se hizo la opración que se muestra en la línea")
		Call .AddTextColumn(0, "Tipo de operación", "tctOper_Type", 30, vbNullString,  , "Tipo de operación realizada al siniestro")
		Call .AddTextColumn(0, "Moneda", "tctCurrency", 30, vbNullString,  , "Descripción breve de la moneda en la que se ha realizado la operación")
		Call .AddTextColumn(0, "Fecha de la operación", "tctOperdate", 15, vbNullString,  , "Fecha en la que se ha realizado la operación")
		Call .AddNumericColumn(0, "Monto", "tcnAmount", 18,  ,  , "Monto de la operación.", True, 6)
		Call .AddTextColumn(0, "Sucursal", "tctOffice", 30, vbNullString,  , "Sucursal a la que pertenece el siniestro")
		Call .AddTextColumn(0, "Ramo", "tctBranchDesc", 30, vbNullString,  , "Descripción breve del ramo de la póliza del siniestro.")
		Call .AddTextColumn(0, "Producto", "tctProductDesc", 30, vbNullString,  , "Descripción breve del producto de la póliza del siniestro")
		Call .AddTextColumn(0, "Póliza", "tctPolicy", 10, vbNullString,  , "Número identificativo de la póliza del siniestro")
		Call .AddTextColumn(0, "Certificado", "tctCertif", 10, vbNullString,  , "Número identificativo del certificado asociado al siniestro.")
	End With
	
	'+ Se definen las propiedades generales del grid.
	With mobjGrid
		.Codispl = "SIC005"
		.DeleteButton = False
		.AddButton = False
		.Columns("Sel").GridVisible = False
		.ActionQuery = True
	End With
End Sub

'**% insPreSIC005: This function allows to make the reading of the main table of the transaction.  
'% insPreSIC005: Esta función permite realizar la lectura de la tabla principal de la transacción.
'-----------------------------------------------------------------------------------------
Private Sub insPreSIC005()
	'-----------------------------------------------------------------------------------------
	Dim lintCount As Short
	Dim lclsClaim As eClaim.Claim
	Dim lcolClaims As eClaim.Claims
	
	lclsClaim = New eClaim.Claim
	lcolClaims = New eClaim.Claims
	
	If lcolClaims.FindSIC005(CDate(Session("dInitdate")), CInt(Session("nBranch")), CInt(Session("nOper_type")), CInt(Session("nOffice")), CInt(Session("nProduct")), CInt(Session("nCurrency"))) Then
		
		lintCount = 0
		For	Each lclsClaim In lcolClaims
			With lclsClaim
				
				mobjGrid.Columns("tctClaim").DefValue = CStr(.nClaim)
				mobjGrid.Columns("tctOper_type").DefValue = .sOper_TypeDesc
				mobjGrid.Columns("tctCurrency").DefValue = .sCurrencyDesc
				
				If .dOperdate = eRemoteDB.Constants.dtmNull Then
					mobjGrid.Columns("tctOperdate").DefValue = CStr(eRemoteDB.Constants.StrNull)
				Else
					mobjGrid.Columns("tctOperdate").DefValue = CStr(.dOperdate)
				End If
				
				'+ Si el tipo de operación es pago de siniestro se suma al campo importe el monto del importe del impuesto
				If .nOper_Type = 10 Or .nOper_Type = 11 Then
					mobjGrid.Columns("tcnAmount").DefValue = CStr(.nAmount + .nInc_Amount)
				Else
					mobjGrid.Columns("tcnAmount").DefValue = CStr(.nAmount)
				End If
				
				mobjGrid.Columns("tctOffice").DefValue = .sOfficeDesc
				mobjGrid.Columns("tctBranchDesc").DefValue = .sBranchDesc
				mobjGrid.Columns("tctProductDesc").DefValue = .sProductDesc
				mobjGrid.Columns("tctPolicy").DefValue = CStr(.nPolicy)

				If .nCertif > 0 Then
					mobjGrid.Columns("tctCertif").DefValue = CStr(.nCertif)
				Else
					mobjGrid.Columns("tctCertif").DefValue = "0"
				End If
				
				Response.Write(mobjGrid.DoRow())
			End With
			
			lintCount = lintCount + 1
			If lintCount = 200 Then
				Exit For
			End If
		Next lclsClaim
	End If
	
	Response.Write(mobjGrid.closeTable())
	
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
Call mobjNetFrameWork.BeginPage("sic005")

mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 11.34.48
mobjValues.sSessionID = Session.SessionID
mobjValues.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjValues.sCodisplPage = "sic005"
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
		.Write(mobjMenu.setZone(2, "SIC005", Request.QueryString("sWindowDescript"), Request.QueryString("nWindowTy")))
		'UPGRADE_NOTE: Object mobjMenu may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
		mobjMenu = Nothing
	End If
	
	If Request.QueryString("nMainAction") = eFunctions.Menues.TypeActions.clngActionQuery Then
		mobjValues.ActionQuery = True
	End If
End With
%>
	
<SCRIPT>
//+ Variable para el control de versiones
    document.VssVersion="$$Revision: 1 $|$$Date: 2/09/03 19:01 $|&&Author: &"
//% insCancel: Ejecuta rutinas necesarias en el momento de cancelar la página
//------------------------------------------------------------------------------------------
function insCancel()
//------------------------------------------------------------------------------------------
{		    
	return true;
}
</SCRIPT>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" ID="FORM" NAME="SIC005" ACTION="ValClaim.ASPX?sZone=2&nMainAction=<%=Request.QueryString("nMainAction")%>">

    	<%Response.Write(mobjValues.ShowWindowsName("SIC005", Request.QueryString("sWindowDescript")))%>
	<BR><BR>
<%Call insDefineHeader()
Call insPreSIC005()

'UPGRADE_NOTE: Object mobjGrid may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
mobjGrid = Nothing
'UPGRADE_NOTE: Object mobjValues may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
mobjValues = Nothing
%>
</FORM>
</BODY>
</HTML>

<%'^Begin Footer Block VisualTimer Utility 1.1 7/4/03 11.34.48
Call mobjNetFrameWork.FinishPage("sic005")
'UPGRADE_NOTE: Object mobjNetFrameWork may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>




