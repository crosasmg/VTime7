<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eClaim" %>
<script language="VB" runat="Server">

Dim mobjValues As eFunctions.Values
Dim mobjMenu As eFunctions.Menues
Dim mobjGrid As eFunctions.Grid



'% insDefineHeader: Configura los datos del grid.
'%--------------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'%--------------------------------------------------------------------------------------
	
	mobjGrid = New eFunctions.Grid
	
	mobjGrid.sCodisplPage = "MSI020"
	
	'+ Se definen las columnas del grid.
	
	With mobjGrid.Columns
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnYearColumnCaption"), "tcnYear", 5, "",  , GetLocalResourceObject("tcnYearColumnToolTip"), True,  ,  ,  ,  , Request.QueryString.Item("Action") = "Update")
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnIndexLiabColumnCaption"), "tcnIndexLiab", 9, "",  , GetLocalResourceObject("tcnIndexLiabColumnToolTip"), True, 8)
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnIndexAssetColumnCaption"), "tcnIndexAsset", 9, "",  , GetLocalResourceObject("tcnIndexAssetColumnToolTip"), True, 8)
	End With
	
	'+ Se definen las propiedades generales del grid.
	With mobjGrid
		.Codispl = "MSI020"
		.sCodisplPage = "MSI020"
		.Height = 300
		.Width = 350
		.nMainAction = CShort(IIf(Request.QueryString.Item("nMainAction") ="", 0, Request.QueryString.Item("nMainAction")))
		.AddButton = CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) <> 401
		.DeleteButton = CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) <> 401
		.Columns("Sel").GridVisible = CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) <> 401
		.Columns("tcnYear").EditRecord = CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) <> 401
		.sDelRecordParam = "nYear='+ marrArray[lintIndex].tcnYear + '"
		
		
		'+ Permite continuar si el check está marcado.
		If Request.QueryString.Item("Reload") = "1" Then
			.sReloadIndex = Request.QueryString.Item("ReloadIndex")
		End If
	End With
End Sub

'% insPreMSI020: Obtiene los datos de los fondos de inversión.
'%--------------------------------------------------------------------------------------
Private Sub insPreMSI020()
	'%--------------------------------------------------------------------------------------
	Dim mclsIndex_Cover As eClaim.Index_Cover
	Dim mcolIndex_Cover As eClaim.Index_Covers
	
	mclsIndex_Cover = New eClaim.Index_Cover
	mcolIndex_Cover = New eClaim.Index_Covers
	
	If mcolIndex_Cover.Find(mobjValues.StringToDate(Session("dEffecdate"))) Then
		With mobjGrid
			For	Each mclsIndex_Cover In mcolIndex_Cover
				.Columns("tcnYear").DefValue = CStr(mclsIndex_Cover.nYear)
				.Columns("tcnIndexLiab").DefValue = CStr(mclsIndex_Cover.nIndexLiab)
				.Columns("tcnIndexAsset").DefValue = CStr(mclsIndex_Cover.nIndexAssets)
				Response.Write(.DoRow)
			Next mclsIndex_Cover
		End With
	End If
	
	Response.Write(mobjGrid.closeTable)
	
	mclsIndex_Cover = Nothing
	mcolIndex_Cover = Nothing
End Sub

'% insPreMSI020Upd: Muestra la ventana Popup para las actualizaciones.
'%--------------------------------------------------------------------------------------
Private Function insPreMSI020Upd() As Object
	'%------------------------------------------------------------------------
	Dim mobjMantClaim As eClaim.Index_Cover
	If Request.QueryString.Item("Action") = "Del" Then
		mobjMantClaim = New eClaim.Index_Cover
		Response.Write(mobjValues.ConfirmDelete())
		Call mobjMantClaim.insPostMSI020Upd(Request.QueryString.Item("Action"), mobjValues.StringToType(Request.QueryString.Item("nYear"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, Session("nUserCode"))
		mobjMantClaim = Nothing
	End If
	
	Response.Write(mobjGrid.DoFormUpd(Request.QueryString.Item("Action"), "valMantClaim.aspx", "MSI020", Request.QueryString.Item("nMainAction"), mobjValues.ActionQuery, CShort(Request.QueryString.Item("Index"))))
End Function

</script>
<%Response.Expires = -1

mobjValues = New eFunctions.Values
mobjMenu = New eFunctions.Menues
mobjGrid = New eFunctions.Grid

If CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 401 Then
	mobjGrid.ActionQuery = True
	mobjValues.ActionQuery = True
End If

mobjValues.sCodisplPage = "MSI020"
%>
<HTML>
<HEAD>
    <META NAME = "GENERATOR" CONTENT = "Microsoft Visual Studio 6.0">
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>



<SCRIPT LANGUAGE="JavaScript">
    var nMainAction = <%=Request.QueryString.Item("nMainAction")%>;

//+ Para Control de Versiones "NO REMOVER"
//------------------------------------------------------------------------------
	document.VssVersion="$$Revision: 1 $|$$Date: 2/09/03 19:03 $"
//------------------------------------------------------------------------------

//% insCancel: Esta función ejecuta la acción Cancelar de la página.
//------------------------------------------------------------------------------------
function insCancel(){
//------------------------------------------------------------------------------------
	return true
}
</SCRIPT>    
        <%With Response
	.Write(mobjValues.StyleSheet() & vbCrLf)
	
	If Request.QueryString.Item("Type") <> "PopUp" Then
		.Write(mobjMenu.setZone(2, "MSI020", "MSI020.aspx"))
	End If
End With

mobjMenu = Nothing%>
    </HEAD>
    <BODY ONUNLOAD="closeWindows();">
        <FORM METHOD="post" ID="FORM" NAME="frmIndexCover" ACTION="valMantClaim.aspx?mode=1&nMainAction=<%=Request.QueryString.Item("nMainAction")%>">
            <%=mobjValues.ShowWindowsName("MSI020")%>
            <BR>
            <%
Call insDefineHeader()

If Request.QueryString.Item("Type") <> "PopUp" Then
	Call insPreMSI020()
Else
	Call insPreMSI020Upd()
End If

mobjValues = Nothing
mobjGrid = Nothing
%>
        </FORM>
    </BODY>
</HTML>





