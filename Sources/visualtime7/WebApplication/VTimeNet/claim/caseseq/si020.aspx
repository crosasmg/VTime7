<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eClaim" %>
<script language="VB" runat="Server">
'^Begin Header Block VisualTimer Utility 1.1 7/4/03 11.33.47
Dim mobjNetFrameWork As eNetFrameWork.Layout
'~End Header Block VisualTimer Utility
'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values
Dim mobjGrid As eFunctions.Grid
Dim mobjMenu As eFunctions.Menues


'% insDefineHeader: Se definen los campos del grid
'--------------------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'--------------------------------------------------------------------------------------------
	'+ Se definen las columnas del grid
	With mobjGrid.Columns
		If Request.QueryString("Type") <> "PopUp" Then
			.AddTextColumn(0, "Repuesto dañado", "tctDamage_cod", 30, "",  , "Código del repuesto dañado")
			.AddHiddenColumn("valDamage_cod", CStr(0))
			.AddTextColumn(0, "Magnitud del daño", "tctMag_dam", 30, "",  , "Magnitud del daño")
			.AddHiddenColumn("cbeMag_dam", CStr(0))
		Else
			.AddPossiblesColumn(0, "Repuesto dañado", "valDamage_cod", "Table5579", eFunctions.Values.eValuesType.clngWindowType,  ,  ,  ,  ,  ,  ,  ,  , "Código del repuesto dañado",  ,  ,  , True, "")
			.AddPossiblesColumn(0, "Magnitud del daño", "cbeMag_dam", "Table5674", eFunctions.Values.eValuesType.clngComboType, CStr(0),  ,  ,  ,  ,  ,  ,  , "Magnitud del daño")
		End If
	End With
	
	'+ Se definen las propiedades generales del grid
	With mobjGrid
		.sCodisplPage = "SI020"
		.Width = 400
		.Height = 230
		If Request.QueryString("Action") = "Update" Then
			.Columns("valDamage_cod").Disabled = True
			.MoveRecordScript = "ShowDescript();"
		End If
		If Request.QueryString("Type") <> "PopUp" Then
			.Columns("tctDamage_cod").EditRecord = True
		End If
		
		.Codispl = "SI020"
		.sDelRecordParam = "nDamage_cod='+ marrArray[lintIndex].valDamage_cod + '" & "&nMag_dam='+ marrArray[lintIndex].cbeMag_dam + '"
		If Request.QueryString("Reload") = "1" Then
			.sReloadIndex = Request.QueryString("ReloadIndex")
		End If
	End With
End Sub

'% insPreSI020: Se cargan los controles de la página
'--------------------------------------------------------------------------------------------
Private Sub insPreSI020()
	'--------------------------------------------------------------------------------------------
	Dim lintIndex As Short
	Dim lclsClaim_Dama As eClaim.Claim_Dama
	Dim lcolClaim_damas As eClaim.Claim_damas
	
	lintIndex = 0
	With Server
		lclsClaim_Dama = New eClaim.Claim_Dama
		lcolClaim_damas = New eClaim.Claim_damas
	End With
	Response.Write("<SCRIPT>top.frames['fraSequence'].document.location='/VTimeNet/Claim/CaseSeq/Sequence.aspx?nAction=" & Request.QueryString("nMainAction") & "&sGoToNext=NO" & "';</" & "Script>")
	If lcolClaim_damas.Find(mobjValues.StringToType(CStr(Session("nBranch")), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(CStr(Session("nClaim")), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(CStr(Session("nCase_num")), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(CStr(Session("nDeman_type")), eFunctions.Values.eTypeData.etdDouble)) Then
		For	Each lclsClaim_Dama In lcolClaim_damas
			With mobjGrid
				.Columns("valDamage_cod").DefValue = CStr(lclsClaim_Dama.nDamage_cod)
				.Columns("cbeMag_dam").DefValue = CStr(lclsClaim_Dama.nMag_dam)
				.Columns("tctDamage_cod").DefValue = lclsClaim_Dama.sDes_Damage_cod
				.Columns("tctMag_dam").DefValue = lclsClaim_Dama.sDes_Mag_dam
				Response.Write(.DoRow)
			End With
			lintIndex = lintIndex + 1
		Next lclsClaim_Dama
	End If
	Response.Write(mobjGrid.closeTable())
	
	'UPGRADE_NOTE: Object lclsClaim_Dama may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
	lclsClaim_Dama = Nothing
	'UPGRADE_NOTE: Object lcolClaim_damas may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
	lcolClaim_damas = Nothing
End Sub

'----------------------------------------------------------------------------------------------
Private Sub insPreSI020Upd()
	'----------------------------------------------------------------------------------------------
	Dim lclsClaim_Dama As eClaim.Claim_Dama
	Dim lblnPost As Boolean
	
	With Request
		If .QueryString("Action") = "Del" Then
			lclsClaim_Dama = New eClaim.Claim_Dama
			lblnPost = lclsClaim_Dama.insPostSI020("SI020", .QueryString("Action"), mobjValues.StringToType(CStr(Session("nBranch")), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(CStr(Session("nClaim")), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(CStr(Session("nCase_num")), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(CStr(Session("nDeman_type")), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString("nDamage_cod"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(CStr(Session("nUsercode")), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString("nMag_dam"), eFunctions.Values.eTypeData.etdDouble))
			
			Response.Write(mobjValues.ConfirmDelete)
			'UPGRADE_NOTE: Object lclsClaim_Dama may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
			lclsClaim_Dama = Nothing
		End If
		Response.Write(mobjGrid.DoFormUpd(.QueryString("Action"), "valCaseSeq.aspx", "SI020", .QueryString("nMainAction"), mobjValues.ActionQuery, .QueryString("Index")))
	End With
	
End Sub

</script>
<%Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage("si020")

With Server
	mobjValues = New eFunctions.Values
	'^Begin Body Block VisualTimer Utility 1.1 7/4/03 11.33.47
	mobjValues.sSessionID = Session.SessionID
	mobjValues.nUsercode = Session("nUsercode")
	'~End Body Block VisualTimer Utility
	
	mobjValues.sCodisplPage = "si020"
	mobjGrid = New eFunctions.Grid
	'^Begin Body Block VisualTimer Utility 1.1 7/4/03 11.33.47
	mobjGrid.sSessionID = Session.SessionID
	mobjGrid.nUsercode = Session("nUsercode")
	'~End Body Block VisualTimer Utility
	
	Call mobjGrid.SetWindowParameters(Request.QueryString("sCodispl"), Request.QueryString("sWindowDescript"), Request.QueryString("nWindowTy"))
	mobjMenu = New eFunctions.Menues
	'^Begin Body Block VisualTimer Utility 1.1 7/4/03 11.33.47
	mobjMenu.sSessionID = Session.SessionID
	mobjMenu.nUsercode = Session("nUsercode")
	'~End Body Block VisualTimer Utility
End With

mobjGrid.ActionQuery = Session("bQuery")
%>
<%'UPGRADE_NOTE: Language element 'SCRIPT' was migrated to the same language element but still may have a different behavior. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1011.htm  %>
<%'UPGRADE_NOTE: The file '/VTimeNet/Scripts/GenFunctions.js' was not found in the migration directory. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1003.htm  %>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<HTML>
<HEAD>
    <META NAME = "GENERATOR" Content = "Microsoft Visual Studio 6.0">
        <%'UPGRADE_NOTE: Language element '#INCLUDE' was migrated to the same language element but still may have a different behavior. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1011.htm  %>
<%'UPGRADE_NOTE: The file '/VTimeNet/Includes/Constantes.aspx' was not found in the migration directory. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1003.htm  %>
<!-- #INCLUDE VIRTUAL="~/VTimeNet/Includes/Constantes.aspx" -->

    <%Response.Write(mobjValues.StyleSheet())
If Request.QueryString("Type") <> "PopUp" Then
	With Response
		.Write(mobjMenu.setZone(2, "SI020", Request.QueryString("sWindowDescript"), Request.QueryString("nWindowTy")))
		.Write("<SCRIPT>var nMainAction=304</SCRIPT>")
	End With
	'UPGRADE_NOTE: Object mobjMenu may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
	mobjMenu = Nothing
End If
%>
<SCRIPT>
//+ Variable para el control de versiones
    document.VssVersion="$$Revision: 4 $|$$Date: 15/10/03 12.24 $"        
</SCRIPT>
<SCRIPT>
//% ShowDescrip: 
//-------------------------------------------------------------------------------------------
function ShowDescript(){
//-------------------------------------------------------------------------------------------
	with(self.document.forms[0]){
		$(valDamage_cod).change();	
	}
}  
</SCRIPT>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="post" ID="FORM" NAME="frmSI020" ACTION="valCaseSeq.aspx?smode=1">
    <%Response.Write(mobjValues.ShowWindowsName("SI020", Request.QueryString("sWindowDescript")))
Call insDefineHeader()
If Request.QueryString("Type") = "PopUp" Then
	Call insPreSI020Upd()
	If Request.QueryString("Action") = "Update" Then
		Response.Write("<SCRIPT>ShowDescript();</SCRIPT>")
	End If
Else
	Call insPreSI020()
End If
%>
</FORM>
</BODY>
</HTML>
<%
'UPGRADE_NOTE: Object mobjValues may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
mobjValues = Nothing
'UPGRADE_NOTE: Object mobjGrid may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
mobjGrid = Nothing
%>

<%'^Begin Footer Block VisualTimer Utility 1.1 7/4/03 11.33.47
Call mobjNetFrameWork.FinishPage("si020")
'UPGRADE_NOTE: Object mobjNetFrameWork may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>




