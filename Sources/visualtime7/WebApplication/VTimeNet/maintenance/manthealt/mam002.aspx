<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eBranches" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores.
Dim mobjValues As eFunctions.Values

'- Se define la variable mobjGrid para el manejo del Grid de la ventana.
Dim mobjGrid As eFunctions.Grid

'- Objeto para el manejo de las zonas de la página.
Dim mobjMenu As eFunctions.Menues


'% insDefineHeader: Se definen las columnas del grid.
'------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'------------------------------------------------------------------------------
	mobjGrid = New eFunctions.Grid
	
	Response.Write(mobjValues.ShowWindowsName(Request.QueryString.Item("sCodispl")))
	
	'+ Se definen todas las columnas del Grid.
	With mobjGrid.Columns
		Call .AddPossiblesColumn(0, GetLocalResourceObject("cbeIllnessColumnCaption"), "cbeIllness", "Tab_am_ill", 2,  ,  ,  ,  ,  ,  ,  , 8, GetLocalResourceObject("cbeIllnessColumnToolTip"), 2)
		Call .AddPossiblesColumn(0, GetLocalResourceObject("cbeExc_codeColumnCaption"), "cbeExc_code", "Table271", 2,  ,  ,  ,  ,  ,  ,  , 2, GetLocalResourceObject("cbeExc_codeColumnToolTip"), 1)
		Call .AddDateColumn(0, GetLocalResourceObject("tcdExc_dateColumnCaption"), "tcdExc_date",  ,  , GetLocalResourceObject("tcdExc_dateColumnToolTip"),  ,  ,  , False)
	End With
	
	With mobjGrid
		.Codispl = "MAM002"
		.Codisp = "MAM002"
		.sCodisplPage = "MAM002"
		.Top = 200
		.Height = 224
		.Width = 400
		.ActionQuery = mobjValues.ActionQuery
		.bOnlyForQuery = Request.QueryString.Item("nMainAction") = "401"
		.ActionQuery = (Request.QueryString.Item("nMainAction") = "401" Or IsNothing(Request.QueryString.Item("nMainAction")))
		.Columns("Sel").GridVisible = Not .ActionQuery
		.Columns("cbeExc_code").EditRecord = True
		.Columns("cbeIllness").Disabled = Request.QueryString.Item("Action") = "Update"
		
		.sDelRecordParam = "dEffecdate=" & mobjValues.TypeToString(Request.QueryString.Item("dEffecdate"), eFunctions.Values.eTypeData.etdDate) & "&sIllness=' + marrArray[lintIndex].cbeIllness + '" & "&dexc_date=' + marrArray[lintIndex].tcdExc_date + '"
		
		.sEditRecordParam = "dEffecdate=" & mobjValues.TypeToString(Request.QueryString.Item("dEffecdate"), eFunctions.Values.eTypeData.etdDate)
		
		.sReloadAction = Request.QueryString.Item("ReloadAction")
		If Request.QueryString.Item("Reload") = "1" Then
			.sReloadIndex = Request.QueryString.Item("ReloadIndex")
		End If
	End With
End Sub

'%insPreMAM002: Permite cargar los valores en las columnas del grid.
'------------------------------------------------------------------------------
Private Sub insPreMAM002()
	'------------------------------------------------------------------------------
	Dim lclsTab_am_gexs As eBranches.Tab_am_gexs
	Dim lclsTab_am_gex As Object
	
	
Response.Write("" & vbCrLf)
Response.Write("<SCRIPT>" & vbCrLf)
Response.Write("//%insPreZone: Se encarga de recargar la página según la acción en tratamiento." & vbCrLf)
Response.Write("//-------------------------------------------------------------------------------------------------------------------" & vbCrLf)
Response.Write("function insPreZone(llngAction){" & vbCrLf)
Response.Write("//-------------------------------------------------------------------------------------------------------------------" & vbCrLf)
Response.Write("	switch (llngAction){" & vbCrLf)
Response.Write("	    case 301:" & vbCrLf)
Response.Write("	    case 302:" & vbCrLf)
Response.Write("	    case 401:" & vbCrLf)
Response.Write("	        document.location.href = document.location.href.replace(/&nMainAction.*/,'') + '&nMainAction=' + llngAction" & vbCrLf)
Response.Write("	        break;" & vbCrLf)
Response.Write("	}" & vbCrLf)
Response.Write("}" & vbCrLf)
Response.Write("</" & "SCRIPT>        ")

	
	lclsTab_am_gexs = New eBranches.Tab_am_gexs
	
	With mobjGrid
		If lclsTab_am_gexs.Find(mobjValues.StringToType(Request.QueryString.Item("dEffecdate"), eFunctions.Values.eTypeData.etdDate)) Then
			For	Each lclsTab_am_gex In lclsTab_am_gexs
				.Columns("cbeIllness").DefValue = lclsTab_am_gex.sIllness
				.Columns("cbeExc_code").DefValue = lclsTab_am_gex.nExc_code
				.Columns("tcdExc_date").DefValue = lclsTab_am_gex.dExc_date
				Response.Write(mobjGrid.DoRow())
			Next lclsTab_am_gex
		End If
	End With
	
	Response.Write(mobjGrid.CloseTable())
	
	lclsTab_am_gex = Nothing
	lclsTab_am_gexs = Nothing
End Sub

'% insPreMAM002Upd: Se define esta funcion para construir el contenido de la 
'% ventana UPD de los archivos de datos particulares.
'------------------------------------------------------------------------------
Private Sub insPreMAM002Upd()
	'------------------------------------------------------------------------------
	Dim lclsTab_am_gex As eBranches.Tab_am_gex
	Dim lstrErrors As Object
	
	If Request.QueryString.Item("Action") = "Del" Then
		lclsTab_am_gex = New eBranches.Tab_am_gex
		
		Response.Write(mobjValues.ConfirmDelete())
		
		With lclsTab_am_gex
			Call lclsTab_am_gex.insPostMAM002(Request.QueryString.Item("Action"), mobjValues.StringToType(Request.QueryString.Item("dEffecdate"), eFunctions.Values.eTypeData.etdDate), Request.QueryString.Item("sIllness"), mobjValues.StringToType(Request.QueryString.Item("nExc_code"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("dExc_date"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble))
		End With
		
		lclsTab_am_gex = Nothing
	End If
	
	With Response
		.Write(mobjGrid.DoFormUpd(Request.QueryString.Item("Action"), "ValMantHealt.aspx", Request.QueryString.Item("sCodispl"), Request.QueryString.Item("nMainAction"),  , CShort(Request.QueryString.Item("Index"))))
	End With
End Sub

</script>
<%Response.Expires = -1

mobjValues = New eFunctions.Values
mobjValues.ActionQuery = (Request.QueryString.Item("nMainAction") = "401")
mobjValues.sCodisplPage = "MAM002"
%>
<HTML>
<HEAD>
	<META NAME="GENERATOR" CONTENT="eTransaction Designer for Visual TIME">
<SCRIPT	LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>


<SCRIPT>
//- Variable para el control de versiones
	document.VssVersion="$$Revision: 2 $|$$Date: 15/10/03 16:02 $|$$Author: Nvaplat61 $"
</SCRIPT>
<%
With Response
	.Write(mobjValues.StyleSheet())
	.Write("<SCRIPT>var	nMainAction	= " & Request.QueryString.Item("nMainAction") & "</SCRIPT>")
	If Request.QueryString.Item("Type") <> "PopUp" Then
		mobjMenu = New eFunctions.Menues
		.Write(mobjMenu.setZone(2, "MAM002", "MAM002.aspx"))
		mobjMenu = Nothing
	End If
	.Write(mobjValues.WindowsTitle("MAM002"))
End With
%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST"	ID="FORM" NAME="frmMAM002" ACTION="ValMantHealt.aspx?sZone=2"> 
<%Call insDefineHeader()

If Request.QueryString.Item("Type") <> "PopUp" Then
	Call insPreMAM002()
Else
	Call insPreMAM002Upd()
End If
mobjValues = Nothing
mobjGrid = Nothing
%>	  
</FORM>
</BODY>
</HTML>




