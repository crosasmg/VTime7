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
Dim mcolRatings As eBranches.Ratingss


'% insDefineHeader: se definen las propiedades del grid
'--------------------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'--------------------------------------------------------------------------------------------
	mobjGrid = New eFunctions.Grid
	
	'+ Se definen las columnas del grid    
	With mobjGrid.Columns
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnAge_iniColumnCaption"), "tcnAge_ini", 3, vbNullString,  , GetLocalResourceObject("tcnAge_iniColumnToolTip"), False, 0,  ,  ,  , Request.QueryString.Item("Action") = "Update")
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnAge_endColumnCaption"), "tcnAge_end", 3, vbNullString,  , GetLocalResourceObject("tcnAge_endColumnToolTip"), False, 0,  ,  ,  , Request.QueryString.Item("Action") = "Update")
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnRatingColumnCaption"), "tcnRating", 5, vbNullString,  , GetLocalResourceObject("tcnRatingColumnToolTip"), False, 0)
	End With
	
	'+ Se definen las propiedades generales del grid
	With mobjGrid
		.Codispl = "MVA740"
		.sCodisplPage = "MVA740"
		.ActionQuery = mobjValues.ActionQuery
		.Columns("tcnAge_ini").EditRecord = True
		.Height = 200
		.Width = 280
		.nMainAction = CShort(IIf(Request.QueryString.Item("nMainAction") ="", 0, Request.QueryString.Item("nMainAction")))
		.Columns("Sel").GridVisible = Not .ActionQuery
		.sDelRecordParam = "nAge_ini=' + marrArray[lintIndex].tcnAge_ini + '"
		If Request.QueryString.Item("Reload") = "1" Then
			.sReloadIndex = Request.QueryString.Item("ReloadIndex")
		End If
	End With
End Sub

'% insPreMVA740: se realiza el manejo del grid
'--------------------------------------------------------------------------------------------
Private Sub insPreMVA740()
	'--------------------------------------------------------------------------------------------
	Dim lclsRatings As Object
	
	mcolRatings = New eBranches.Ratingss
	If mcolRatings.Find(mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), Session("dEffecdate")) Then
		For	Each lclsRatings In mcolRatings
			With mobjGrid
				.Columns("tcnAge_ini").DefValue = lclsRatings.nAge_ini
				.Columns("tcnAge_end").DefValue = lclsRatings.nAge_end
				.Columns("tcnRating").DefValue = lclsRatings.nRating
				Response.Write(.DoRow)
			End With
		Next lclsRatings
	End If
	
	Response.Write(mobjGrid.closeTable())
End Sub

'% insPreMVA740Upd: Se realiza el manejo de la ventana PopUp asociada al grid
'--------------------------------------------------------------------------------------------
Private Sub insPreMVA740Upd()
	'--------------------------------------------------------------------------------------------
	Dim lclsRatings As eBranches.Ratings
	
	
	With Request
		If Request.QueryString.Item("Action") = "Del" Then
			lclsRatings = New eBranches.Ratings
			
			Response.Write(mobjValues.ConfirmDelete())
			Call lclsRatings.InsPostMVA740(.QueryString.Item("Action"), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), Session("dEffecdate"), mobjValues.StringToType(.QueryString.Item("nAge_ini"), eFunctions.Values.eTypeData.etdDouble), eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble))
			
			lclsRatings = Nothing
		End If
		
		Response.Write(mobjGrid.DoFormUpd(.QueryString.Item("Action"), "valMantLife.aspx", "MVA740", .QueryString.Item("nMainAction"), mobjValues.ActionQuery, CShort(.QueryString.Item("Index"))))
	End With
End Sub

</script>
<%Response.Expires = -1

mobjValues = New eFunctions.Values
mobjMenu = New eFunctions.Menues

mobjValues.ActionQuery = CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 401
mobjValues.sCodisplPage = "MVA740"
%>
<HTML>
<HEAD>
<META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>


<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<%
Response.Write(mobjValues.StyleSheet())
If Request.QueryString.Item("Type") <> "PopUp" Then
	Response.Write(mobjMenu.setZone(2, "MVA740", "MVA740.aspx"))
	mobjMenu = Nothing
	Response.Write("<SCRIPT>var nMainAction=top.frames['fraSequence'].plngMainAction</SCRIPT>")
End If
%>
</SCRIPT>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" NAME="MVA740.aspx" ACTION="valMantLife.aspx?sMode=2">
<%Response.Write(mobjValues.ShowWindowsName("MVA740"))
Call insDefineHeader()

If Request.QueryString.Item("Type") = "PopUp" Then
	Call insPreMVA740Upd()
Else
	Call insPreMVA740()
End If
%>
</FORM> 
</BODY>
</HTML>




