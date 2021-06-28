<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eCollection" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores

Dim mobjValues As eFunctions.Values
'- Objeto para el manejo de las rutinas genéricas

Dim mobjMenu As eFunctions.Menues
Dim mobjGrid As eFunctions.Grid


'------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'------------------------------------------------------------------------------
	mobjGrid = New eFunctions.Grid
	
	'+ Se definen las columns del Grid
	
	With mobjGrid.Columns
		If Request.QueryString.Item("Action") = "Update" Then
			Call .AddNumericColumn(40590, GetLocalResourceObject("tcnRangeIniColumnCaption"), "tcnRangeIni", 18, CStr(eRemoteDB.Constants.strNull), True, GetLocalResourceObject("tcnRangeIniColumnToolTip"),  , 6,  ,  ,  , True)
			Call .AddNumericColumn(40590, GetLocalResourceObject("tcnRangeEndColumnCaption"), "tcnRangeEnd", 18, CStr(eRemoteDB.Constants.strNull), True, GetLocalResourceObject("tcnRangeEndColumnToolTip"),  , 6,  ,  ,  , True)
		Else
			Call .AddNumericColumn(40591, GetLocalResourceObject("tcnRangeIniColumnCaption"), "tcnRangeIni", 18, CStr(eRemoteDB.Constants.strNull), True, GetLocalResourceObject("tcnRangeIniColumnToolTip"),  , 6)
			Call .AddNumericColumn(40590, GetLocalResourceObject("tcnRangeEndColumnCaption"), "tcnRangeEnd", 18, CStr(eRemoteDB.Constants.strNull), True, GetLocalResourceObject("tcnRangeEndColumnToolTip"),  , 6)
		End If
		Call .AddNumericColumn(40590, GetLocalResourceObject("tcnPercentColumnCaption"), "tcnPercent", 4, CStr(eRemoteDB.Constants.strNull), True, GetLocalResourceObject("tcnPercentColumnToolTip"),  , 2)
	End With
	
	'+ Se asignan las caracteristicas del Grid
	
	With mobjGrid
		.Columns("tcnPercent").EditRecord = True
		.Codispl = Request.QueryString.Item("sCodispl")
		.Codisp = "MCO734_K"
		.sCodisplPage = "MCO734"
		If Request.QueryString.Item("nMainAction") = "401" Or Request.QueryString.Item("nMainAction") = vbNullString Then
			mobjGrid.ActionQuery = True
			mobjGrid.Columns("Sel").GridVisible = False
		End If
		.sDelRecordParam = "nInit_Range='+ marrArray[lintIndex].tcnRangeIni + '" & "&nEnd_Range='+ marrArray[lintIndex].tcnRangeEnd + '"
		.Height = 250
		.Width = 350
		
		If Request.QueryString.Item("Reload") = "1" Then
			.sReloadIndex = Request.QueryString.Item("ReloadIndex")
		End If
	End With
	
End Sub
'------------------------------------------------------------------------------
Private Sub insPreMCO734()
	'------------------------------------------------------------------------------
	Dim lcolDelay_Ints As eCollection.Delay_Ints
	Dim lclsDelay_Int As Object
	
Response.Write("" & vbCrLf)
Response.Write("<SCRIPT>" & vbCrLf)
Response.Write("//-------------------------------------------------------------------------------------------------------------------" & vbCrLf)
Response.Write("function insPreZone(llngAction){" & vbCrLf)
Response.Write("//-------------------------------------------------------------------------------------------------------------------" & vbCrLf)
Response.Write("   	switch (llngAction){" & vbCrLf)
Response.Write("	    case 301:" & vbCrLf)
Response.Write("	    case 302:" & vbCrLf)
Response.Write("	    case 401:" & vbCrLf)
Response.Write("	        document.location.href = document.location.href.replace(/&nMainAction.*/,'') + '&nMainAction=' + llngAction" & vbCrLf)
Response.Write("	    break;" & vbCrLf)
Response.Write("	}" & vbCrLf)
Response.Write("}" & vbCrLf)
Response.Write("</" & "SCRIPT>")

	
	
	lcolDelay_Ints = New eCollection.Delay_Ints
	
	If lcolDelay_Ints.Find() Then
		For	Each lclsDelay_Int In lcolDelay_Ints
			With mobjGrid
				.Columns("tcnRangeIni").DefValue = lclsDelay_Int.nInit_Range
				.Columns("tcnRangeEnd").DefValue = lclsDelay_Int.nEnd_Range
				.Columns("tcnPercent").DefValue = lclsDelay_Int.nPercent
			End With
			
			'+Se ejecuta el metodo DoRow, que se encarga de mostrar los elementos de grid
			
			Response.Write(mobjGrid.DoRow())
		Next lclsDelay_Int
	End If
	Response.Write(mobjGrid.closeTable())
End Sub
'------------------------------------------------------------------------------
Private Sub insPreMCO734Upd()
	'------------------------------------------------------------------------------
	Dim lclsDelay_Int As eCollection.Delay_Int
	Dim lstrErrors As Object
	
	If Request.QueryString.Item("Action") = "Del" Then
		lclsDelay_Int = New eCollection.Delay_Int
		Response.Write(mobjValues.ConfirmDelete())
		With lclsDelay_Int
			.nInit_Range = mobjValues.StringToType(Request.QueryString.Item("nInit_Range"), eFunctions.Values.eTypeData.etdDouble)
			.nEnd_Range = mobjValues.StringToType(Request.QueryString.Item("nEnd_Range"), eFunctions.Values.eTypeData.etdDouble)
			.Delete()
		End With
		lclsDelay_Int = Nothing
	End If
	
	Response.Write(mobjGrid.DoFormUpd(Request.QueryString.Item("Action"), "valMantCollection.aspx", Request.QueryString.Item("sCodispl"), Request.QueryString.Item("MainAction"),  , CShort(Request.QueryString.Item("Index"))))
	Response.Write(mobjValues.HiddenControl("sAction", Request.QueryString.Item("Action")))
End Sub

</script>
<%Response.Expires = -1

mobjValues = New eFunctions.Values
mobjValues.sCodisplPage = "MCO734"
%>

<HTML>
<HEAD>




    <META NAME = "GENERATOR" Content = "Microsoft Visual Studio 6.0">
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<%If Request.QueryString.Item("Type") <> "PopUp" Then%>
       <%	'$$EWI_1012:D:\VisualTIMEChile\Result\VTimeStep1\maintenance\mantcollection\Vtime\Scripts\tMenu.js#%>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tMenu.js"></SCRIPT>
<%End If%>
	<SCRIPT>
    
//- Variable para el control de versiones
	     document.VssVersion="$$Revision: 1 $|$$Date: 2/09/03 19:03 $|$$Author: Iusr_llanquihue $"

    </SCRIPT> 

<SCRIPT>
//------------------------------------------------------------------------------------------
function insCancel(){
//------------------------------------------------------------------------------------------
	return true;
}	
</SCRIPT>	
 
<%

With Response
	.Write(mobjValues.StyleSheet() & vbCrLf)
	If Request.QueryString.Item("Type") <> "PopUp" Then
		mobjMenu = New eFunctions.Menues
		.Write("<SCRIPT>var nMainAction=top.frames['fraSequence'].plngMainAction</SCRIPT>")
		.Write(mobjMenu.MakeMenu(Request.QueryString.Item("sCodispl"), "MCO734_k.aspx", 1, ""))
		mobjMenu = Nothing
	End If
End With
%>
</HEAD>

<BODY ONUNLOAD="closeWindows();">
<%
If Request.QueryString.Item("Type") <> "PopUp" Then
	Response.Write("<BR><BR>")
End If

Response.Write(mobjValues.ShowWindowsName(Request.QueryString.Item("sCodispl")))

%>
<FORM METHOD="POST" ID="FORM" NAME="frmTabIntMora" ACTION="valMantCollection.aspx?mode=1">
<%
Call insDefineHeader()
If Request.QueryString.Item("Type") <> "PopUp" Then
	Call insPreMCO734()
Else
	Call insPreMCO734Upd()
End If
mobjValues = Nothing
%>
</FORM>
</BODY>
</HTML>




