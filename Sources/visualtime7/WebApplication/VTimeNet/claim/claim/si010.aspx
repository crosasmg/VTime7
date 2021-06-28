<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eClaim" %>
<script language="VB" runat="Server">
'^Begin Header Block VisualTimer Utility 1.1 7/4/03 11.34.12
Dim mobjNetFrameWork As eNetFrameWork.Layout
'~End Header Block VisualTimer Utility

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values
Dim mobjGrid As eFunctions.Grid
Dim mobjMenu As eFunctions.Menues


'% insDefineHeader: se definen las propiedades del grid
'----------------------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'----------------------------------------------------------------------------------------------
	'+ Se definen las columnas del grid
	With mobjGrid.Columns
		Call .AddTextColumn(40275, "Fecha movimiento", "tctDate", 10, "",  , "Fecha en que ha sido realizado el movimiento.")
		If mobjValues.StringToType(Request.QueryString("nCase_num"), eFunctions.Values.eTypeData.etdDouble) = eRemoteDB.Constants.intNull Then
			Call .AddNumericColumn(40273, "Caso", "tcnCaseNum", 6, "",  , "Caso al cual pertenece el movimiento.")
		End If
		Call .AddTextColumn(40271, "Tipo movimiento", "tctTypeMovement", 30, "",  , "Tipo de movimiento realizado.")
		Call .AddNumericColumn(40274, "Monto", "tcnAmount", 18, "",  , "Importe asociado al movimiento.", True, 6)
		Call .AddPossiblesColumn(40272, "Moneda", "cbeCurrency", "Table11", eFunctions.Values.eValuesType.clngComboType,  ,  ,  ,  ,  ,  ,  ,  , "Moneda en la que se encuentra el importe del movimiento.")
		Call .AddCheckColumn(40276, "Aso", "chkAssociate", "",  ,  ,  , True)
		
		Call .AddHiddenColumn("cbeTypeMovement", "")
		Call .AddHiddenColumn("nTypMov", "")
		Call .AddHiddenColumn("CaseNum", "")
		Call .AddHiddenColumn("Movement", "")
		Call .AddHiddenColumn("DemanType", "")
		Call .AddHiddenColumn("Selected", CStr(2))
	End With
	
	mobjGrid.Columns("Sel").OnClick = "if(document.forms[0].Selected.length>0)document.forms[0].Selected[this.value].value =(this.checked?1:2); else document.forms[0].Selected.value =(this.checked?1:2);"
	
	'+ Se definen las propiedades generales del grid    
	With mobjGrid
		.Codispl = "SI010"
		.Width = 300
		.Height = 300
		.AddButton = False
		.DeleteButton = False
	End With
End Sub

'% insPreSI010: Se realiza el manejo del grid
'--------------------------------------------------------------------------------------------------------------------------------------
Private Sub insPreSI010()
	'--------------------------------------------------------------------------------------------------------------------------------------
	Dim lcolClaimHis As eClaim.Claim_hiss
	Dim lintCount As Integer
	
	lcolClaimHis = New eClaim.Claim_hiss
	lintCount = 0
	
	Call lcolClaimHis.Find_SI010(mobjValues.StringToType(Request.QueryString("nClaim"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString("nCase_num"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString("nDeman_type"), eFunctions.Values.eTypeData.etdDouble), Request.QueryString("dEffecdate"))
	
	For lintCount = 1 To lcolClaimHis.Count
		With mobjGrid
			.Columns("tctDate").DefValue = CStr(lcolClaimHis.Item(lintCount).dOperdate)
			If mobjValues.StringToType(Request.QueryString("nCase_num"), eFunctions.Values.eTypeData.etdDouble) = eRemoteDB.Constants.intNull Then
				.Columns("tcnCaseNum").DefValue = CStr(lcolClaimHis.Item(lintCount).nCase_num)
			End If
			.Columns("cbeTypeMovement").DefValue = CStr(lcolClaimHis.Item(lintCount).noper_type)
			.Columns("tctTypeMovement").DefValue = lcolClaimHis.Item(lintCount).soper_type
			.Columns("tcnAmount").DefValue = CStr(lcolClaimHis.Item(lintCount).nAmount)
			.Columns("cbeCurrency").DefValue = CStr(lcolClaimHis.Item(lintCount).nCurrency)
			
			.Columns("nTypMov").DefValue = CStr(lcolClaimHis.Item(lintCount).noper_type)
			.Columns("CaseNum").DefValue = CStr(lcolClaimHis.Item(lintCount).nCase_num)
			.Columns("Movement").DefValue = CStr(lcolClaimHis.Item(lintCount).nTransac)
			.Columns("DemanType").DefValue = CStr(lcolClaimHis.Item(lintCount).nDeman_type)
			If lcolClaimHis.Item(lintCount).nAso = 1 Then
				.Columns("chkAssociate").Checked = lcolClaimHis.Item(lintCount).nAso
			Else
				.Columns("chkAssociate").Checked = 0
			End If
		End With
		Response.Write(mobjGrid.DoRow())
	Next 
	
	Response.Write(mobjGrid.CloseTable())
	
	Response.Write(mobjValues.HiddenControl("hddnClaim", Request.QueryString("nClaim")))
	Response.Write(mobjValues.HiddenControl("hddnCase_num", Request.QueryString("nCase_num")))
	Response.Write(mobjValues.HiddenControl("hddnDeman_type", Request.QueryString("nDeman_type")))
	
	'UPGRADE_NOTE: Object lcolClaimHis may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
	lcolClaimHis = Nothing
	'UPGRADE_NOTE: Object mobjValues may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
	mobjValues = Nothing
	'UPGRADE_NOTE: Object mobjGrid may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
	mobjGrid = Nothing
	'UPGRADE_NOTE: Object mobjMenu may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
	mobjMenu = Nothing
End Sub

</script>
<%Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage("si010")

mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 11.34.12
mobjValues.sSessionID = Session.SessionID
mobjValues.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjValues.sCodisplPage = "si010"
mobjGrid = New eFunctions.Grid
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 11.34.12
mobjGrid.sSessionID = Session.SessionID
mobjGrid.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjGrid.sCodisplPage = "si010"
Call mobjGrid.SetWindowParameters(Request.QueryString("sCodispl"), Request.QueryString("sWindowDescript"), Request.QueryString("nWindowTy"))
mobjMenu = New eFunctions.Menues
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 11.34.12
mobjMenu.sSessionID = Session.SessionID
mobjMenu.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

%>
<%'UPGRADE_NOTE: Language element 'SCRIPT' was migrated to the same language element but still may have a different behavior. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1011.htm  %>
<%'UPGRADE_NOTE: The file '../../Scripts/GenFunctions.js' was not found in the migration directory. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1003.htm  %>
<SCRIPT LANGUAGE="JavaScript" SRC="../../Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript">

//+Variable para el control de versiones
    document.VssVersion="$$Revision: 4 $|$$Date: 29/03/04 18:55 $"

//%insChargePage: Se recarga la página una vez que el combo del caso cambia de valor - ACM - 12/02/2001
//-------------------------------------------------------------------------------------------------------------
function insChargePage(){
//-------------------------------------------------------------------------------------------------------------
	var lstrstring = "";
	lstrstring += document.location;
	document.location = lstrstring;
}
</SCRIPT>
<HTML>
<HEAD>
<%'UPGRADE_NOTE: Language element '#INCLUDE' was migrated to the same language element but still may have a different behavior. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1011.htm  %>
<%'UPGRADE_NOTE: The file 'vtime/Includes/Constantes.aspx' was not found in the migration directory. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1003.htm  %>
<!-- #INCLUDE VIRTUAL="~/VTimeNet/Includes/Constantes.aspx" -->

<%'UPGRADE_NOTE: Language element '#INCLUDE' was migrated to the same language element but still may have a different behavior. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1011.htm  %>
<%'UPGRADE_NOTE: The file 'vtime/Includes/Claim.aspx' was not found in the migration directory. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1003.htm  %>
<!-- #INCLUDE VIRTUAL="~/VTimeNet/Includes/Claim.aspx" -->

    <META NAME = "GENERATOR" CONTENT = "Microsoft Visual Studio 6.0">
    <%=mobjValues.StyleSheet()%>
    <%
If Request.QueryString("Type") <> "PopUp" Then
	Response.Write(mobjMenu.setZone(2, "SI010", Request.QueryString("sWindowDescript"), Request.QueryString("nWindowTy")))
	'UPGRADE_NOTE: Object mobjMenu may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
	mobjMenu = Nothing
	Response.Write("<SCRIPT>" & Request.QueryString("nMainAction") & "</SCRIPT>")
End If
%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="post" ID="FORM" NAME="frmSI010" ACTION="valClaim.aspx?sMode=1">
<%
Response.Write(mobjValues.ShowWindowsName("SI010", Request.QueryString("sWindowDescript")))
Call insDefineHeader()
Call insPreSI010()
%>
</FORM>
</BODY>
</HTML>
<%'^Begin Footer Block VisualTimer Utility 1.1 7/4/03 11.34.12
Call mobjNetFrameWork.FinishPage("si010")
'UPGRADE_NOTE: Object mobjNetFrameWork may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>




