<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eBranches" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'- Objeto para el manejo del grid de la página
Dim mobjGrid As eFunctions.Grid

'- Objeto para el manejo del menú
Dim mobjMenu As eFunctions.Menues

'- Objeto para el manejo particular de los datos de la página
Dim mclsTar_Schooltrad As eBranches.Tar_schooltrad


'% insDefineHeader: se definen las propiedades del grid
'--------------------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'--------------------------------------------------------------------------------------------
	mobjGrid = New eFunctions.Grid
	'+ Se definen las columnas del grid    
	With mobjGrid.Columns
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnAge_ChildColumnCaption"), "tcnAge_Child", 3, vbNullString,  , GetLocalResourceObject("tcnAge_ChildColumnToolTip"),  ,  ,  ,  ,  , Request.QueryString.Item("Action") = "Update")
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnAge_insuColumnCaption"), "tcnAge_insu", 3, vbNullString,  , GetLocalResourceObject("tcnAge_insuColumnCaption"),  ,  ,  ,  ,  , Request.QueryString.Item("Action") = "Update")
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnPeriod_payColumnCaption"), "tcnPeriod_pay", 5, vbNullString,  , GetLocalResourceObject("tcnPeriod_payColumnToolTip"), True)
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnRateColumnCaption"), "tcnRate", 9, "",  , GetLocalResourceObject("tcnRateColumnToolTip"),  , 6)
	End With
	'+ Se definen las propiedades generales del grid
	With mobjGrid
		.Codispl = "MVI771"
		.sCodisplPage = "MVI771"
		.ActionQuery = mobjValues.ActionQuery
		.Height = 310
		.Width = 360
		.Columns("tcnAge_Child").EditRecord = True
		.nMainAction = CShort(IIf(Request.QueryString.Item("nMainAction") ="", 0, Request.QueryString.Item("nMainAction")))
		.Columns("Sel").GridVisible = True
		.sDelRecordParam = "nAge_insu=' + marrArray[lintIndex].tcnAge_insu + '" & "&nAge_Child='+ marrArray[lintIndex].tcnAge_Child + '" & "&nBranch=" & Request.QueryString.Item("nBranch") & "&nProduct=" & Request.QueryString.Item("nProduct") & "&dEffecdate=" & Request.QueryString.Item("dEffecdate")
		.sEditRecordParam = "nBranch=" & Request.QueryString.Item("nBranch") & "&nProduct=" & Request.QueryString.Item("nProduct") & "&dEffecdate=" & Request.QueryString.Item("dEffecdate")
		If Request.QueryString.Item("Reload") = "1" Then
			.sReloadIndex = Request.QueryString.Item("ReloadIndex")
		End If
	End With
End Sub

'% insPreMVI771: se realiza el manejo del grid
'--------------------------------------------------------------------------------------------
Private Sub insPreMVI771()
	'--------------------------------------------------------------------------------------------
	Dim lintIndex As Object
	Dim lblnFind As Object
	Dim mobjTar_Schooltrads As eBranches.Tar_schooltrads
	mobjTar_Schooltrads = New eBranches.Tar_schooltrads
	If mobjTar_Schooltrads.Find(mobjValues.StringToType(Request.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("dEffecdate"), eFunctions.Values.eTypeData.etdDate)) Then
		For	Each mclsTar_Schooltrad In mobjTar_Schooltrads
			With mobjGrid
				.Columns("tcnAge_Child").DefValue = CStr(mclsTar_Schooltrad.nAge_Child)
				.Columns("tcnAge_insu").DefValue = CStr(mclsTar_Schooltrad.nAge_insu)
				.Columns("tcnPeriod_pay").DefValue = CStr(mclsTar_Schooltrad.nPeriod_pay)
				.Columns("tcnRate").DefValue = CStr(mclsTar_Schooltrad.nRate)
				Response.Write(.DoRow)
			End With
		Next mclsTar_Schooltrad
	End If
	Response.Write(mobjGrid.closeTable())
	mobjTar_Schooltrads = Nothing
End Sub
'% insPreMVI771Upd: Se realiza el manejo de la ventana PopUp asociada al grid
'--------------------------------------------------------------------------------------------
Private Sub insPreMVI771Upd()
	'--------------------------------------------------------------------------------------------
	With Request
		If Request.QueryString.Item("Action") = "Del" Then
			Response.Write(mobjValues.ConfirmDelete())
			Call mclsTar_Schooltrad.insPostMVI771(.QueryString.Item("Action"), mobjValues.StringToType(.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Request.QueryString.Item("nAge_insu"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nAge_Child"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnPeriod_pay"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnRate"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(session("nUsercode"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("dNulldate"), eFunctions.Values.eTypeData.etdDate))
		End If
		Response.Write(mobjGrid.DoFormUpd(.QueryString.Item("Action"), "valMantLife.aspx", "MVI771", .QueryString.Item("nMainAction"), mobjValues.ActionQuery, CShort(.QueryString.Item("Index"))))
	End With
End Sub

</script>
<%Response.Expires = -1

mobjValues = New eFunctions.Values
mclsTar_Schooltrad = New eBranches.Tar_schooltrad
mobjMenu = New eFunctions.Menues

mobjValues.ActionQuery = CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 401
mobjValues.sCodisplPage = "MVI771"

%>
<HTML>
<HEAD>
<SCRIPT>
//- Variable para el control de versiones
    document.VssVersion="$$Revision: 2 $|$$Date: 15/10/03 16:06 $|$$Author: Nvaplat61 $"
</SCRIPT>
	<META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>




<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
	<%
Response.Write(mobjValues.StyleSheet())
If Request.QueryString.Item("Type") <> "PopUp" Then
	Response.Write(mobjMenu.setZone(2, "MVI771", "MVI771.aspx"))
	
	mobjMenu = Nothing
	Response.Write("<SCRIPT>var nMainAction=top.frames['fraSequence'].plngMainAction</SCRIPT>")
End If
%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" NAME="MVI771" ACTION="valMantLife.aspx?nBranch=<%=Request.QueryString.Item("nBranch")%>&nProduct=<%=Request.QueryString.Item("nProduct")%>&dEffecdate=<%=Request.QueryString.Item("dEffecdate")%>">
<%Response.Write(mobjValues.ShowWindowsName("MVI771"))
Call insDefineHeader()

If Request.QueryString.Item("Type") = "PopUp" Then
	Call insPreMVI771Upd()
Else
	Call insPreMVI771()
End If
%>
</FORM> 
</BODY>
</HTML>

<%
mclsTar_Schooltrad = Nothing
%>




