<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eClaim" %>
<script language="VB" runat="Server">
'^Begin Header Block VisualTimer Utility 1.1 7/4/03 10.27.20
Dim mobjNetFrameWork As eNetFrameWork.Layout
'~End Header Block VisualTimer Utility

'- Objeto para el manejo de las funciones generales de carga de valores

Dim mobjValues As eFunctions.Values
Dim mobjGrid As eFunctions.Grid
Dim mobjMenu As eFunctions.Menues
Dim mcolClaimDisabilitys As eClaim.ClaimDisabilitys


'% insDefineHeader: Se definen los campos del grid
'--------------------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'--------------------------------------------------------------------------------------------
	mobjGrid = New eFunctions.Grid
	'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.27.20
	mobjGrid.sSessionID = Session.SessionID
	mobjGrid.nUsercode = Session("nUsercode")
	'~End Body Block VisualTimer Utility
	
	mobjGrid.sCodisplPage = "SI024D"
	Call mobjGrid.SetWindowParameters("SI024D", "", "")
	
	'+ Se definen las columnas del grid  
	
	With mobjGrid.Columns
		Call .AddPossiblesColumn(0, "Cobertura genérica", "tcnCovergen", "tabTab_LifCov", eFunctions.Values.eValuesType.clngWindowType, vbNullString, True,  ,  ,  ,  , False, 4, "Cobertura genérica asociada a la tasa de indemnización de invalidez")
		Call .AddPossiblesColumn(0, "Forma de invalidez", "tcnDisability", "Table5505", eFunctions.Values.eValuesType.clngComboType, "2",  ,  ,  ,  ,  , False,  , "Forma de invalidez asociada a la tasa")
		Call .AddNumericColumn(0, "Porcentaje de indemnización", "tcnRate", 9, vbNullString,  , "Porcentaje de indemnización", False, 6)
	End With
	
	'+ Se definen las propiedades generales del grid
	
	With mobjGrid
		.DeleteButton = False
		.AddButton = False
		.Columns("Sel").GridVisible = True
		.Columns("tcnCovergen").Parameters.Add("sStatregt", "1", eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Columns("tcnCovergen").Parameters.Add("nCoverGen", mobjValues.StringToType("", eFunctions.Values.eTypeData.etdDouble), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	End With
End Sub

'% insTar_DisabilityQuery: Se cargan los controles de la página
'--------------------------------------------------------------------------------------------
Private Sub insClaimDisabilityQuery()
	'--------------------------------------------------------------------------------------------
	Dim lclsClaimDisability As eClaim.ClaimDisability
	
	mcolClaimDisabilitys = New eClaim.ClaimDisabilitys
	
	If mcolClaimDisabilitys.Find(CInt(Session("nClaim")), CInt(Session("nCase_num")), CInt(Session("nDeman_type"))) Then
		For	Each lclsClaimDisability In mcolClaimDisabilitys
			With mobjGrid
				.Columns("Sel").Checked = lclsClaimDisability.nExist
				.Columns("Sel").OnClick = "insSelDisability(this," & lclsClaimDisability.nCovergen & "," & lclsClaimDisability.nDisability & ",""" & mobjValues.TypeToString(lclsClaimDisability.nRate, eFunctions.Values.eTypeData.etdDouble, True, 2) & """);"
				.Columns("tcnCovergen").DefValue = CStr(lclsClaimDisability.nCovergen)
				.Columns("tcnDisability").DefValue = CStr(lclsClaimDisability.nDisability)
				.Columns("tcnRate").DefValue = CStr(lclsClaimDisability.nRate)
				Response.Write(.DoRow)
			End With
		Next lclsClaimDisability
	End If
	Response.Write(mobjGrid.closeTable())
	mobjValues.ActionQuery = True
End Sub

</script>
<%Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage("si024D")

mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.27.20
mobjValues.sSessionID = Session.SessionID
mobjValues.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjValues.sCodisplPage = "SI024D"
%>
<HTML>
<HEAD>
    <META NAME = "GENERATOR" CONTENT = "Microsoft Visual Studio 6.0">
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>

<%mobjMenu = New eFunctions.Menues
mobjMenu.sSessionID = Session.SessionID
mobjMenu.nUsercode = Session("nUsercode")
Response.Write(mobjValues.StyleSheet())
'Response.Write  mobjMenu.setZone(2 , "SI024D",  Request.QueryString("sWindowDescript"), Request.QueryString("nWindowTy"))
'mobjValues.ActionQuery = True		
'UPGRADE_NOTE: Object mobjMenu may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
mobjMenu = Nothing
%>
<SCRIPT LANGUAGE="JavaScript">
//+ Variable para el control de versiones
    document.VssVersion="$$Revision: 3 $|$$Date: 25-03-13 7:33 $"    
//------------------------------------------------------------------------------------------
function insSelDisability(Sel,nCovergen,nDisability,nRate){
//------------------------------------------------------------------------------------------
	var lQuery
	
	if (Sel.checked == true) 
	{
	   lQuery = '&nAction=1' + '&nCovergen=' + nCovergen + '&nDisability=' + nDisability + '&nRate=' + nRate;
	   top.document.frames["fraGeneric"].location.href = '/VTimeNet/Claim/CaseSeq/ShowDefValues.aspx?Field=insPostSI024D' + lQuery + '&sFrameCaller=fraHeader';
	}
	else
	{
	   lQuery = '&nAction=2' + '&nCovergen=' + nCovergen + '&nDisability=' + nDisability + '&nRate=' + nRate;
	   top.document.frames["fraGeneric"].location.href = '/VTimeNet/Claim/CaseSeq/ShowDefValues.aspx?Field=insPostSI024D' + lQuery + '&sFrameCaller=fraHeader';
	}
}
</SCRIPT>

</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="post" ID="FORM" NAME="SI024D" ACTION="valcaseseq.aspx?Zone=2">
<%
Response.Write(mobjValues.ShowWindowsName("SI024D"))
Response.Write(mobjValues.WindowsTitle("SI024D"))
Response.Write("<BR>")
Call insDefineHeader()

Call insClaimDisabilityQuery()


'UPGRADE_NOTE: Object mobjGrid may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
mobjGrid = Nothing
'UPGRADE_NOTE: Object mobjValues may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
mobjValues = Nothing
%>     
</FORM>
</BODY>
</HTML>
<%'^Begin Footer Block VisualTimer Utility 1.1 7/4/03 10.27.20
Call mobjNetFrameWork.FinishPage("si024D")
'UPGRADE_NOTE: Object mobjNetFrameWork may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>




