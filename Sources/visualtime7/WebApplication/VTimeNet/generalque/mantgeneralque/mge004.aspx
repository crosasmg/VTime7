<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eGeneralQue" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

Dim mobjMenu As eFunctions.Menues

'- Objeto para el manejo del grid    
Dim mobjGrid As eFunctions.Grid
Dim lclsClassPropertyWin As eGeneralQue.ClassPropertyWin


'------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'------------------------------------------------------------------------------
	mobjGrid = New eFunctions.Grid
	
	mobjGrid.sCodisplPage = "MGE004"
	
	'+ Se definen las columns del Grid
	
	With mobjGrid.columns
		If Request.QueryString.Item("nMainAction") = "401" Then
			Call .AddNumericColumn(100688, GetLocalResourceObject("tcnIdProperty_QueColumnCaption"), "tcnIdProperty_Que", 5, CStr(eRemoteDB.Constants.strNull), True)
		End If
		Call .AddPossiblesColumn(100688, GetLocalResourceObject("tcnIdPropertyColumnCaption"), "tcnIdProperty", "PropertyLibrary", eFunctions.Values.eValuesType.clngComboType,  ,  ,  ,  ,  ,  , Request.QueryString.Item("Action") = "Update")
		Call .AddTextColumn(100690, GetLocalResourceObject("tctCaptionColumnCaption"), "tctCaption", 30, CStr(eRemoteDB.Constants.strNull))
		Call .AddNumericColumn(100689, GetLocalResourceObject("tcnOrderColumnCaption"), "tcnOrder", 5, CStr(eRemoteDB.Constants.strNull), True)
		Call .AddCheckColumn(100691, GetLocalResourceObject("chkTypVisibleColumnCaption"), "chkTypVisible", vbNullString, False, CStr(1),  , False)
		If Request.QueryString.Item("Type") <> "PopUp" Then
			mobjGrid.columns("chkTypVisible").Disabled = True
		Else
			mobjGrid.columns("chkTypVisible").Disabled = False
		End If
		
	End With
	
	'+ Se asignan las caracteristicas del Grid
	
	With mobjGrid
		.columns("tcnIdProperty").EditRecord = True
		.Codispl = "MGE004"
		.Codisp = "MGE004"
		If Request.QueryString.Item("nMainAction") = "401" Or Request.QueryString.Item("nMainAction") = vbNullString Then
			.ActionQuery = True
			.columns("Sel").GridVisible = False
			.DeleteButton = False
			.AddButton = False
		Else
			.ActionQuery = False
			.columns("Sel").GridVisible = True
			.DeleteButton = True
			.AddButton = True
		End If
		
		.sDelRecordParam = "sCodisp=" & Session("sCodispl") & "&nIdClass=" & Session("nFolder") & "&nIdProperty='+ marrArray[lintIndex].tcnIdProperty + '" & "&sCaption='+marrArray[lintIndex].tctCaption + '" & "&nOrder='+marrArray[lintIndex].tcnOrder + '" & "&sVisible=' +marrArray[lintIndex].chkTypVisible + '"
		
		.Height = 300
		.Width = 350
		
		If Request.QueryString.Item("Reload") = "1" Then
			.sReloadIndex = Request.QueryString.Item("ReloadIndex")
		End If
	End With
	
End Sub
'------------------------------------------------------------------------------
Private Sub insPreMGE004()
	'------------------------------------------------------------------------------
	Dim lcolClassPropertiesWin As eGeneralQue.ClassPropertiesWin
	
	lcolClassPropertiesWin = New eGeneralQue.ClassPropertiesWin
	
	If lcolClassPropertiesWin.Find("GE099", Session("nFolder")) Then
		For	Each lclsClassPropertyWin In lcolClassPropertiesWin
			With mobjGrid
				If Request.QueryString.Item("nMainAction") = "401" Then
					.columns("tcnIdProperty_Que").DefValue = CStr(lclsClassPropertyWin.nIdProperty)
				End If
				.columns("tcnIdProperty").DefValue = CStr(lclsClassPropertyWin.nIdProperty)
				.columns("tctCaption").DefValue = lclsClassPropertyWin.sCaption
				.columns("tcnOrder").DefValue = CStr(lclsClassPropertyWin.nOrder)
				.columns("chkTypVisible").checked = CShort(lclsClassPropertyWin.sVisible)
			End With
			
			'+Se ejecuta el metodo DoRow, que se encarga de mostrar los elementos de grid
			
			Response.Write(mobjGrid.DoRow())
		Next lclsClassPropertyWin
	End If
	lcolClassPropertiesWin = Nothing
	Response.Write(mobjGrid.closeTable())
	Response.Write(mobjValues.BeginPageButton)
End Sub
'------------------------------------------------------------------------------
Private Sub insPreMGE004Upd()
	'------------------------------------------------------------------------------
	Dim lstrErrors As String
	With Request
		If .QueryString.Item("Action") = "Del" Then
			lstrErrors = lclsClassPropertyWin.insValMGE004(.QueryString.Item("sCodispl"), .QueryString.Item("Action"), mobjValues.StringToType(Session("nFolder"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nIdProperty"), eFunctions.Values.eTypeData.etdDouble), .QueryString.Item("sVisible"), .QueryString.Item("sCaption"), mobjValues.StringToType(.QueryString.Item("nOrder"), eFunctions.Values.eTypeData.etdDouble))
			
			
			If lstrErrors = vbNullString Then
				Response.Write(mobjValues.ConfirmDelete())
				
				If lclsClassPropertyWin.insPostMGE004(.QueryString.Item("sCodispl"), .QueryString.Item("Action"), mobjValues.StringToType(Session("nFolder"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nIdProperty"), eFunctions.Values.eTypeData.etdDouble), .QueryString.Item("sVisible"), .QueryString.Item("sCaption"), mobjValues.StringToType(.QueryString.Item("nOrder"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble)) Then
				End If
			Else
				Response.Write(lstrErrors)
			End If
		Else
			Response.Write(mobjValues.ShowWindowsName("MGE004"))
		End If
	End With
	With Response
		.Write(mobjGrid.DoFormUpd(Request.QueryString.Item("Action"), "valMantGeneralQue.aspx", Request.QueryString.Item("sCodispl"), Request.QueryString.Item("MainAction"),  , CShort(Request.QueryString.Item("Index"))))
		.Write(mobjValues.HiddenControl("sAction", Request.QueryString.Item("Action")))
	End With
End Sub

</script>
<%Response.Expires = -1
mobjValues = New eFunctions.Values
mobjMenu = New eFunctions.Menues

mobjValues.sCodisplPage = "MGE004_K"

%>
<HTML>
<HEAD>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/Constantes.js"></SCRIPT>
	<META NAME = "GENERATOR" Content = "Microsoft Visual Studio 6.0">




<%
Response.Write(mobjValues.StyleSheet())
If Request.QueryString.Item("Type") <> "PopUp" Then
	Response.Write(mobjMenu.setZone(2, "MGE004", "MGE004.aspx"))
	mobjMenu = Nothing
End If
%>
<SCRIPT>
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

<FORM METHOD="POST" ID="FORM" NAME="frmClassPropertyWin" ACTION="valMantGeneralQue.aspx?mode=1">
<%
Response.Write(mobjValues.ShowWindowsName("MGE004"))
Response.Write("<SCRIPT>var	nMainAction	= 0" & Request.QueryString.Item("nMainAction") & "</SCRIPT>")
lclsClassPropertyWin = New eGeneralQue.ClassPropertyWin
Call insDefineHeader()
If Request.QueryString.Item("Type") <> "PopUp" Then
	Call insPreMGE004()
Else
	Call insPreMGE004Upd()
End If
mobjValues = Nothing
mobjGrid = Nothing
lclsClassPropertyWin = Nothing
%>
</FORM>
</BODY>
</HTML>




