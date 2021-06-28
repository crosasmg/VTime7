<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eBranches" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'- Se define la variable mobjGrid para el manejo del Grid de la ventana
Dim mobjGrid As eFunctions.Grid

'- Se define la variable para la carga del Grid de la ventana 'MAU101'
Dim mclsDeduc_Auto As eBranches.Deduc_auto

'- Objeto para el manejo de las zonas de la página
Dim mobjMenu As eFunctions.Menues


'% insDefineHeader: Definición de columnas del GRID
'--------------------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'--------------------------------------------------------------------------------------------	    
	'+Se definen todas las columnas del Grid
	With mobjGrid.Columns
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnDeducColumnCaption"), "tcnDeduc", 4, CStr(0),  , GetLocalResourceObject("tcnDeducColumnToolTip"), True, 2,  ,  ,  , Request.QueryString.Item("Action") = "Update")
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnDiscountColumnCaption"), "tcnDiscount", 4, CStr(0),  , GetLocalResourceObject("tcnDiscountColumnToolTip"), True, 2)
	End With
	
	With mobjGrid
		.Codispl = "MAU101"
		.sCodisplPage = "MAU101"
		.Top = 200
		.Left = 140
		.Height = 170
		.Columns("tcnDeduc").EditRecord = True
		.sEditRecordParam = "nVehType=" & Request.QueryString.Item("nVehType") & "&dEffecdate=" & Request.QueryString.Item("dEffecdate")
		.sDelRecordParam = .sEditRecordParam & "&nDeduc=' + marrArray[lintIndex].tcnDeduc + '&nDiscount=' + marrArray[lintIndex].tcnDiscount + '"
		.ActionQuery = CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 401
		.Columns("Sel").GridVisible = Not .ActionQuery
		.bOnlyForQuery = .ActionQuery
		If Request.QueryString.Item("Reload") = "1" Then
			.sReloadIndex = Request.QueryString.Item("ReloadIndex")
		End If
	End With
End Sub

'%insPreMAU101: Esta función se encarga de cargar los datos en la forma "Folder" 
'--------------------------------------------------------------------------------------------
Private Sub insPreMAU101()
	'--------------------------------------------------------------------------------------------
	Dim lintCount As Integer
	
	With mobjGrid
		If mclsDeduc_Auto.Find(mobjValues.StringToType(Request.QueryString.Item("nVehType"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("dEffecDate"), eFunctions.Values.eTypeData.etdDate)) Then
			For lintCount = 0 To mclsDeduc_Auto.Count - 1
				If mclsDeduc_Auto.ItemDeduc_Auto(lintCount) Then
					.Columns("tcnDeduc").DefValue = CStr(mclsDeduc_Auto.nDeduc)
					.Columns("tcnDiscount").DefValue = CStr(mclsDeduc_Auto.nDiscount)
					Response.Write(.DoRow)
				End If
			Next 
		End If
		Response.Write(.closeTable)
	End With
	Response.Write(mobjValues.BeginPageButton)
End Sub

'% insPreMAU101Upd: se realiza el tratamiento de la ventana PopUp
'-------------------------------------------------------------------------------------------------------------------
Private Sub insPreMAU101Upd()
	'-------------------------------------------------------------------------------------------------------------------
	With Request
		If .QueryString.Item("Action") = "Del" Then
			Response.Write(mobjValues.ConfirmDelete())
			Call mclsDeduc_Auto.insPostMAU101("MAU101", .QueryString.Item("Action"), CDbl(.QueryString.Item("nDeduc")), CDbl(.QueryString.Item("nDiscount")), CInt(.QueryString.Item("nVehType")), CDate(.QueryString.Item("dEffecdate")), Session("nUsercode"))
		End If
		Response.Write(mobjGrid.DoFormUpd(.QueryString.Item("Action"), "valMantAuto.aspx", "MAU101", .QueryString.Item("nMainAction"), mobjValues.ActionQuery, CShort(.QueryString.Item("Index"))))
		Response.Write(mobjValues.HiddenControl("hddVehType", Request.QueryString.Item("nVehType")))
		Response.Write(mobjValues.HiddenControl("hddEffecdate", Request.QueryString.Item("dEffecdate")))
	End With
End Sub

</script>
<%
Response.Expires = -1

mobjMenu = New eFunctions.Menues
mclsDeduc_Auto = New eBranches.Deduc_auto
mobjValues = New eFunctions.Values
mobjGrid = New eFunctions.Grid

mobjValues.sCodisplPage = "MAU101"
%>
<HTML>
<HEAD>
    <META NAME = "GENERATOR" CONTENT = "Microsoft Visual Studio 6.0">
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>


<%
If Request.QueryString.Item("Type") <> "PopUp" Then
	Response.Write(mobjMenu.setZone(2, "MAU101", "MAU101.aspx"))
	Response.Write("<SCRIPT>var nMainAction=302</SCRIPT>")
End If
Response.Write(mobjValues.StyleSheet())
%>
<SCRIPT>
//- Variable para el control de versiones
    document.VssVersion="$$Revision: 2 $|$$Date: 15/10/03 15:45 $|$$Author: Nvaplat61 $"
</SCRIPT>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="post" ID="FORM" NAME="frmDeduc_Auto" ACTION="valMantAuto.aspx?sMode=1">
<%
Response.Write(mobjValues.ShowWindowsName("MAU101"))
Call insDefineHeader()
If Request.QueryString.Item("Type") <> "PopUp" Then
	Call insPreMAU101()
Else
	Call insPreMAU101Upd()
End If
%>
</FORM>
</BODY>
</HTML>
			




