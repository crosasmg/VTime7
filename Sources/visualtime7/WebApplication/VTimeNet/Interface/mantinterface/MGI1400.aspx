<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eInterface" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores

Dim mobjValues As eFunctions.Values
'- Objeto para el manejo de las rutinas genéricas

Dim mobjMenu As eFunctions.Menues
Dim mobjGrid As eFunctions.Grid


'%insDefineHeader. Definición de columnas del GRID
'------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'------------------------------------------------------------------------------
	Dim lobjhomolog_table As eInterface.Homolog_table
	
	mobjGrid = New eFunctions.Grid
	mobjGrid.sCodisplPage = "MGI1400"
	
	'+ Se definen las columns del Grid
	With mobjGrid.Columns
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnIdColumnCaption"), "tcnId", 5, "", True, GetLocalResourceObject("tcnIdColumnToolTip"), False, 0)
		Call .AddTextColumn(0, GetLocalResourceObject("tctCampovtColumnCaption"), "tctCampovt", 20, vbNullString,  , GetLocalResourceObject("tctCampovtColumnToolTip"))
		Call .AddTextColumn(0, GetLocalResourceObject("tctCodvtColumnCaption"), "tctCodvt", 20, vbNullString,  , GetLocalResourceObject("tctCodvtColumnToolTip"))
		Call .AddTextColumn(0, GetLocalResourceObject("tctValorvtColumnCaption"), "tctValorvt", 60, vbNullString,  , GetLocalResourceObject("tctValorvtColumnToolTip"))
		
		Call .AddTextColumn(0, GetLocalResourceObject("tctTablaseColumnCaption"), "tctTablase", 20, vbNullString,  , GetLocalResourceObject("tctTablaseColumnToolTip"))
		Call .AddTextColumn(0, GetLocalResourceObject("tctCamposeColumnCaption"), "tctCampose", 20, vbNullString,  , GetLocalResourceObject("tctCamposeColumnToolTip"))
		Call .AddTextColumn(0, GetLocalResourceObject("tctValorseColumnCaption"), "tctValorse", 20, vbNullString,  , GetLocalResourceObject("tctValorseColumnToolTip"))
		
		Call .AddCheckColumn(0, GetLocalResourceObject("chkPredomColumnCaption"), "chkPredom", "", 1, "1",  , True, GetLocalResourceObject("chkPredomColumnToolTip"))
	End With
	
	'+ Se asignan las caracteristicas del Grid
	With mobjGrid
		.Codispl = "MGI1400"
		.Left = 200
		.Width = 570
		.Height = 330
		
		If Request.QueryString.Item("Action") <> "Update" And Request.QueryString.Item("Type") <> "PopUp" Then
			.Columns("tcnId").EditRecord = True
		End If
		
		'+Si es popup e ingreso, trae correlativo por nSystem y nTable
		If Request.QueryString.Item("Type") = "PopUp" Then
			lobjhomolog_table = New eInterface.Homolog_table
			
			.Columns("tcnId").DefValue = CStr(lobjhomolog_table.InsCalId(CInt(Request.QueryString.Item("nSystem")), CInt(Request.QueryString.Item("nTable"))))
			.Columns("chkPredom").Disabled = False
			
			lobjhomolog_table = Nothing
		End If
		
		.ActionQuery = (Request.QueryString.Item("nMainAction") = "401" Or IsNothing(Request.QueryString.Item("nMainAction")))
		.nMainAction = mobjValues.StringToType(Request.QueryString.Item("nMainAction"), eFunctions.Values.eTypeData.etdDouble)
		.Columns("Sel").GridVisible = Not .ActionQuery
		.Columns("tcnId").Disabled = True
		.sDelRecordParam = "nId=' + marrArray[lintIndex].tcnId + '" & "&nSystem=" & Request.QueryString.Item("nSystem") & "&nTable=" & Request.QueryString.Item("nTable")
		
		'EFR armo querystring para pasar a pop up
		.sEditRecordParam = "nSystem=" & Request.QueryString.Item("nSystem") & "&nTable=" & Request.QueryString.Item("nTable")
		
		If Request.QueryString.Item("Action") <> "Add" Then
			.Columns("tcnId").Disabled = True
		End If
		
		If Request.QueryString.Item("Reload") = "1" Then
			.sReloadIndex = Request.QueryString.Item("ReloadIndex")
		End If
	End With
	
End Sub

'%insPreMGI1400_K: Esta función se encarga de cargar los datos en la forma "Folder" 
'------------------------------------------------------------------------------
Private Sub insPreMGI1400_K()
	'------------------------------------------------------------------------------
	Dim lcolhomolog_table As eInterface.Homolog_tables
	Dim lclshomolog_table As eInterface.Homolog_table
	Dim lblnFind As Object
	
	lcolhomolog_table = New eInterface.Homolog_tables
	lclshomolog_table = New eInterface.Homolog_table
	
	If lcolhomolog_table.Find(CInt(Request.QueryString.Item("nSystem")), CInt(Request.QueryString.Item("nTable"))) Then
		
		For	Each lclshomolog_table In lcolhomolog_table
			With mobjGrid
				.Columns("tcnId").DefValue = CStr(lclshomolog_table.nId)
				.Columns("tctCampovt").DefValue = lclshomolog_table.sColumnname_Vt
				.Columns("tctCodvt").DefValue = lclshomolog_table.sCodvalue_Vt
				.Columns("tctValorvt").DefValue = lclshomolog_table.sValue_Vt
				.Columns("tctTablase").DefValue = lclshomolog_table.sTablename
				.Columns("tctCampose").DefValue = lclshomolog_table.sColumnname
				.Columns("tctValorse").DefValue = lclshomolog_table.sCodvalue
				'MANEJO DE CAMPO PREDOMINA EN EL CHECK
				If CDbl(lclshomolog_table.sPredom) = 1 Then
					.Columns("chkPredom").Checked = 1
				Else
					.Columns("chkPredom").Checked = 2
				End If
			End With
			Response.Write(mobjGrid.DoRow())
		Next lclshomolog_table
	End If
	Response.Write(mobjGrid.closeTable())
	lcolhomolog_table = Nothing
	lclshomolog_table = Nothing
End Sub

'% insPreMGI1400Upd. Se define esta función para contruir el contenido de la ventana UPD
'---------------------------------------------------------------------------------------
Private Sub insPreMGI1400_K_Upd()
	'---------------------------------------------------------------------------------------
	Dim lclshomolog_table As eInterface.Homolog_table
	lclshomolog_table = New eInterface.Homolog_table
	
	If Request.QueryString.Item("Action") = "Del" Then
		Response.Write(mobjValues.ConfirmDelete())
		
		Call lclshomolog_table.insPostMGI1400("Del", mobjValues.StringToType(Request.QueryString.Item("nSystem"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nTable"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nId"), eFunctions.Values.eTypeData.etdDouble), CStr(eRemoteDB.Constants.strnull), CStr(eRemoteDB.Constants.strnull), CStr(eRemoteDB.Constants.strnull), CStr(eRemoteDB.Constants.strnull), CStr(eRemoteDB.Constants.strnull), CStr(eRemoteDB.Constants.strnull), CStr(eRemoteDB.Constants.strnull), session("nUsercode"))
	End If
	
	lclshomolog_table = Nothing
	Response.Write(mobjGrid.DoFormUpd(Request.QueryString.Item("Action"), "valmantinterface.aspx", "MGI1400", Request.QueryString.Item("MainAction"),  , CShort(Request.QueryString.Item("Index"))))
End Sub

</script>
<%Response.Expires = -1


mobjValues = New eFunctions.Values

mobjValues.sCodisplPage = "MGI1400"
%>
<HTML>
<HEAD>
   <META NAME = "GENERATOR" Content = "Microsoft Visual Studio 6.0">
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
	 <%=mobjValues.WindowsTitle("MGI1400")%>
	
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/Constantes.js"></SCRIPT>




    <META NAME = "GENERATOR" Content = "Microsoft Visual Studio 6.0">
<%
With Response
	.Write(mobjValues.StyleSheet() & vbCrLf)
	If Request.QueryString.Item("Type") <> "PopUp" Then
		mobjMenu = New eFunctions.Menues
		.Write(mobjMenu.setZone(2, "MGI1400", "MGI1400.aspx"))
		mobjMenu = Nothing
	End If
End With
%>
<SCRIPT>
//+ Variable para el control de versiones
    document.VssVersion="$$Revision: 3 $|$$Date: 31/10/03 17:16 $"
 
//-------------------------------------------------------------------------------------------------------------------
function insStateZone(){}

//-------------------------------------------------------------------------------------------------------------------
//-------------------------------------------------------------------------------------------------------------------
function insPreZone(llngAction){
//-------------------------------------------------------------------------------------------------------------------
	switch (llngAction){
	    case 301:
	    case 302:
	    case 401:
	        document.location.href = document.location.href.replace(/&nMainAction.*/,'') + '&nMainAction=' + llngAction
	        break;
	}
}
function insCancel(){
//------------------------------------------------------------------------------------------
	return true;
}
//------------------------------------------------------------------------------------------
function insFinish(){
//------------------------------------------------------------------------------------------
	return true;
	
}
</SCRIPT>		

</HEAD>
<BODY ONUNLOAD="closeWindows();">
<%
If Request.QueryString.Item("Type") <> "PopUp" Then
	Response.Write("<BR><BR>")
End If
%>
<FORM METHOD="POST" ID="FORM" NAME="MGI1400" ACTION="valmantinterface.aspx?nSystem=<%=Request.QueryString.Item("nSystem")%>&nTable=<%=Request.QueryString.Item("nTable")%>">
 <%
Response.Write("<SCRIPT>var	nMainAction	= 0" & Request.QueryString.Item("nMainAction") & "</SCRIPT>")
Response.Write(mobjValues.ShowWindowsName("MGI1400"))
Call insDefineHeader()
If Request.QueryString.Item("Type") <> "PopUp" Then
	Call insPreMGI1400_K()
Else
	Call insPreMGI1400_K_Upd()
End If
mobjValues = Nothing
%>
</FORM>
</BODY>
</HTML>




