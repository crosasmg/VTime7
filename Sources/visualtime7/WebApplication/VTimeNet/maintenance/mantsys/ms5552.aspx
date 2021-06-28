<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eAgent" %>
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
	mobjGrid = New eFunctions.Grid
	
	mobjGrid.sCodisplPage = "ms5552"
	
	'+ Se definen las columns del Grid
	
	With mobjGrid.Columns
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnCodeColumnCaption"), "tcnCode", 5, "", True, GetLocalResourceObject("tcnCodeColumnToolTip"), False, 0)
		Call .AddPossiblesColumn(0, GetLocalResourceObject("tctTypeTaxColumnCaption"), "tctTypeTax", "Table5602", eFunctions.Values.eValuesType.clngComboType, CStr(2),  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("tctTypeTaxColumnCaption"))
		Call .AddPossiblesColumn(0, GetLocalResourceObject("tctTypeSupportColumnCaption"), "tctTypeSupport", "Table5570", eFunctions.Values.eValuesType.clngComboType,  ,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("tctTypeSupportColumnToolTip"))
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnpercentColumnCaption"), "tcnpercent", 5, CStr(0),  , GetLocalResourceObject("tcnpercentColumnToolTip"),  , 2)
		Call .AddHiddenColumn("sParam", vbNullString)
		Call .AddHiddenColumn("chkTaxhide", "2")
	End With
	
	'+ Se asignan las caracteristicas del Grid
	
	With mobjGrid
		.Codispl = "MS5552" 'Request.QueryString("sCodispl")				
		.Left = 250
		.Width = 420
		.Height = 260
		
		If Request.QueryString.Item("Action") <> "Update" And Request.QueryString.Item("Type") <> "PopUp" Then
			.Columns("tcnCode").EditRecord = True
		End If
		
		.ActionQuery = (Request.QueryString.Item("nMainAction") = "401" Or IsNothing(Request.QueryString.Item("nMainAction")))
		.nMainAction = mobjValues.StringToType(Request.QueryString.Item("nMainAction"), eFunctions.Values.eTypeData.etdDouble)
		.Columns("Sel").GridVisible = Not .ActionQuery
		.sDelRecordParam = "' + marrArray[lintIndex].sParam + '"
		.sEditRecordParam = "dEffecdate=" & mobjValues.StringToType(Request.QueryString.Item("dEffecdate"), eFunctions.Values.eTypeData.etdDate)
		If Request.QueryString.Item("Action") <> "Add" Then
			.Columns("tcnCode").Disabled = True
		End If
		
		If Request.QueryString.Item("Reload") = "1" Then
			.sReloadIndex = Request.QueryString.Item("ReloadIndex")
		End If
	End With
	
End Sub

'%insPreMS5552_K: Esta función se encarga de cargar los datos en la forma "Folder" 
'------------------------------------------------------------------------------
Private Sub insPreMS5552_K()
	'------------------------------------------------------------------------------
	Dim lcoltax_fixval As eAgent.Tax_fixvals
	Dim lclstax_fixval As eAgent.tax_fixval
	Dim lblnFind As Object
	lcoltax_fixval = New eAgent.Tax_fixvals
	lclstax_fixval = New eAgent.tax_fixval
	
	If lcoltax_fixval.Find(mobjValues.StringToType(Request.QueryString.Item("dEffecdate"), eFunctions.Values.eTypeData.etdDate)) Then
		For	Each lclstax_fixval In lcoltax_fixval
			With mobjGrid
				.Columns("tcnCode").DefValue = CStr(lclstax_fixval.ncode)
				.Columns("tctTypeTax").DefValue = lclstax_fixval.sTypeTax
				.Columns("tctTypeSupport").DefValue = CStr(lclstax_fixval.nTypeSupport)
				.Columns("tcnpercent").DefValue = CStr(lclstax_fixval.nPercent)
				.Columns("sParam").DefValue = "nCode=" & lclstax_fixval.ncode & "&sTypeTax=" & lclstax_fixval.sTypeTax & "&nTypeSupport=" & lclstax_fixval.nTypeSupport & "&nPercent=" & lclstax_fixval.nPercent & "&dEffecdate=" & mobjValues.StringToType(Request.QueryString.Item("dEffecdate"), eFunctions.Values.eTypeData.etdDate)
				
			End With
			Response.Write(mobjGrid.DoRow())
		Next lclstax_fixval
	End If
	Response.Write(mobjGrid.closeTable())
	lcoltax_fixval = Nothing
	lclstax_fixval = Nothing
End Sub
'% insPreMS5552Upd. Se define esta función para contruir el contenido de la ventana UPD
'---------------------------------------------------------------------------------------
Private Sub insPreMS5552_K_Upd()
	'---------------------------------------------------------------------------------------
	Dim lclstax_fixval As eAgent.tax_fixval
	lclstax_fixval = New eAgent.tax_fixval
	
	If Request.QueryString.Item("Action") = "Del" Then
		Response.Write(mobjValues.ConfirmDelete())
		
		Call lclstax_fixval.insPostMS5552(mobjValues.StringToType(Request.QueryString.Item("nTypeSupport"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nPercent"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nCode"), eFunctions.Values.eTypeData.etdDouble), Request.QueryString.Item("sTypeTax"), "MS5552", "Del", CDate(Request.QueryString.Item("dEffecdate")))
	End If
	
	lclstax_fixval = Nothing
	Response.Write(mobjGrid.DoFormUpd(Request.QueryString.Item("Action"), "valMantSys.aspx", "MS5552", Request.QueryString.Item("MainAction"),  , CShort(Request.QueryString.Item("Index"))))
	If Request.QueryString.Item("Action") = "Update" Then
		Response.Write("<SCRIPT>self.document.forms[0].elements['tctTypeSupport'].disabled=true;</" & "Script>")
		Response.Write("<SCRIPT>self.document.forms[0].elements['tctTypeTax'].disabled=true;</" & "Script>")
	End If
	
End Sub

</script>
<%Response.Expires = -1

mobjValues = New eFunctions.Values

mobjValues.sCodisplPage = "ms5552"
%>
<HTML>
<HEAD>
   <META NAME = "GENERATOR" Content = "Microsoft Visual Studio 6.0">
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
	 <%=mobjValues.WindowsTitle("MS5552")%>
	
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/Constantes.js"></SCRIPT>




    <META NAME = "GENERATOR" Content = "Microsoft Visual Studio 6.0">
<%
With Response
	.Write(mobjValues.StyleSheet() & vbCrLf)
	If Request.QueryString.Item("Type") <> "PopUp" Then
		mobjMenu = New eFunctions.Menues
		.Write(mobjMenu.setZone(2, "MS5552", "MS5552.aspx"))
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
<FORM METHOD="POST" ID="FORM" NAME="frmNumerator" ACTION="valmantsys.aspx?mode=1&dEffecdate=<%=Request.QueryString.Item("dEffecdate")%>">
 <%
Response.Write("<SCRIPT>var	nMainAction	= 0" & Request.QueryString.Item("nMainAction") & "</SCRIPT>")
Response.Write(mobjValues.ShowWindowsName("MS5552"))
Call insDefineHeader()
If Request.QueryString.Item("Type") <> "PopUp" Then
	Call insPreMS5552_K()
Else
	Call insPreMS5552_K_Upd()
End If
mobjValues = Nothing
%>
</FORM>
</BODY>
</HTML>




