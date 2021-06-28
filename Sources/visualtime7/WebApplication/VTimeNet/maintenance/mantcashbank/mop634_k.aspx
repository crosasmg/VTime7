<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eCashBank" %>
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
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnCashNumColumnCaption"), "tcnCashNum", 5, CStr(eRemoteDB.Constants.strNull),  , GetLocalResourceObject("tcnCashNumColumnToolTip"))
		Call .AddPossiblesColumn(0, GetLocalResourceObject("valUserColumnCaption"), "valUser", "tabUsers", eFunctions.Values.eValuesType.clngWindowType, "",  ,  ,  ,  , "InsChangeUser(this.value)",  , 5, GetLocalResourceObject("valUserColumnToolTip"))
		Call .AddPossiblesColumn(0, GetLocalResourceObject("cbeOfficeColumnCaption"), "cbeOffice", "Table9", eFunctions.Values.eValuesType.clngComboType, CStr(2),  ,  ,  ,  ,  , True,  , GetLocalResourceObject("cbeOfficeColumnToolTip"))
		Call .AddPossiblesColumn(0, GetLocalResourceObject("ValOfficeAgenColumnCaption"), "ValOfficeAgen", "table5556", eFunctions.Values.eValuesType.clngWindowType,  ,  ,  ,  ,  ,  , False, 5, GetLocalResourceObject("ValOfficeAgenColumnToolTip"))
		Call .AddPossiblesColumn(0, GetLocalResourceObject("cbeStatusColumnCaption"), "cbeStatus", "Table26", eFunctions.Values.eValuesType.clngComboType, CStr(1),  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbeStatusColumnToolTip"))
		Call .AddPossiblesColumn(0, GetLocalResourceObject("valCashSupColumnCaption"), "valCashSup", "tabUsers", eFunctions.Values.eValuesType.clngWindowType, "",  ,  ,  ,  ,  ,  , 5, GetLocalResourceObject("valCashSupColumnToolTip"))
		Call .AddPossiblesColumn(0, GetLocalResourceObject("valHeadSupColumnCaption"), "valHeadSup", "tabUsers", eFunctions.Values.eValuesType.clngWindowType, "",  ,  ,  ,  ,  ,  , 5, GetLocalResourceObject("valHeadSupColumnToolTip"))
	End With
	
	'+ Se asignan las caracteristicas del Grid
	
	With mobjGrid
		.Columns("valUser").EditRecord = True
		
		If Request.QueryString.Item("Type") = "PopUp" Then
			.Columns("cbeStatus").TypeList = 2
			.Columns("cbeStatus").List = "2"
		End If
		
		.Codispl = Request.QueryString.Item("sCodispl")
		.Codisp = "MOP634_K"
		.sCodisplPage = "MOP634"
		
		If Request.QueryString.Item("nMainAction") = "401" Or Request.QueryString.Item("nMainAction") = vbNullString Then
			mobjGrid.ActionQuery = True
			mobjGrid.Columns("Sel").GridVisible = False
		End If
		.sDelRecordParam = "nCashNum='+ marrArray[lintIndex].tcnCashNum + '&nUser=' + marrArray[lintIndex].valUser + '&sStatus=' + marrArray[lintIndex].cbeStatus + '&nOfficeAgen=' + marrArray[lintIndex].ValOfficeAgen + '"
		.Height = 400
		.Width = 350
		
		If Request.QueryString.Item("Reload") = "1" Then
			.sReloadIndex = Request.QueryString.Item("ReloadIndex")
		End If
	End With
	
End Sub
'------------------------------------------------------------------------------
Private Sub insPreMOP634()
	'------------------------------------------------------------------------------
	Dim lcolUser_cashnums As eCashBank.User_cashnums
	Dim lclsUser_cashnum As Object
	
	Response.Write(mobjValues.ShowWindowsName(Request.QueryString.Item("sCodispl")))
	
Response.Write("" & vbCrLf)
Response.Write("<SCRIPT>" & vbCrLf)
Response.Write("//-------------------------------------------------------------------------------------------------------------------" & vbCrLf)
Response.Write("function insStateZone(){}" & vbCrLf)
Response.Write("//-------------------------------------------------------------------------------------------------------------------" & vbCrLf)
Response.Write("" & vbCrLf)
Response.Write("//% insCancel: se controla la acción Cancelar de la página" & vbCrLf)
Response.Write("//--------------------------------------------------------------------------------------------" & vbCrLf)
Response.Write("function insCancel(){" & vbCrLf)
Response.Write("//--------------------------------------------------------------------------------------------" & vbCrLf)
Response.Write("	return true;" & vbCrLf)
Response.Write("}" & vbCrLf)
Response.Write("" & vbCrLf)
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
Response.Write("</" & "SCRIPT>")

	
	
	lcolUser_cashnums = New eCashBank.User_cashnums
	
	If lcolUser_cashnums.Find(True) Then
		For	Each lclsUser_cashnum In lcolUser_cashnums
			With mobjGrid
				.Columns("tcnCashNum").DefValue = lclsUser_cashnum.nCashNum
				.Columns("valUser").DefValue = lclsUser_cashnum.nUser
				.Columns("cbeOffice").DefValue = lclsUser_cashnum.nOffice
				.Columns("ValOfficeAgen").DefValue = lclsUser_cashnum.nOfficeAgen
				.Columns("cbeStatus").DefValue = lclsUser_cashnum.sStatus
				.Columns("valCashSup").DefValue = lclsUser_cashnum.nCashSup
				.Columns("valHeadSup").DefValue = lclsUser_cashnum.nHeadSup
			End With
			
			'+Se ejecuta el metodo DoRow, que se encarga de mostrar los elementos de grid
			
			Response.Write(mobjGrid.DoRow())
		Next lclsUser_cashnum
	End If
	Response.Write(mobjGrid.closeTable())
End Sub
'------------------------------------------------------------------------------
Private Sub insPreMOP634Upd()
	'------------------------------------------------------------------------------
	Dim lclsUser_cashnum As eCashBank.User_cashnum
	lclsUser_cashnum = New eCashBank.User_cashnum
	
	' Accion para eliminacion de datos del grid
	With Request
		If Request.QueryString.Item("Action") = "Del" Then
			Response.Write(mobjValues.ConfirmDelete())
			
			If lclsUser_cashnum.insPostMOP634(.QueryString.Item("Action"), CInt(.QueryString.Item("nCashnum")), CInt(.QueryString.Item("nUser")), .QueryString.Item("sStatus"), CInt(.QueryString.Item("nCashSup")), CInt(.QueryString.Item("nHeadSup")), Session("nUsercode"), CInt(.QueryString.Item("nOfficeAgen"))) Then
				
			End If
			
			'+ En caso de que se elimine de la tabla de cajas asociadas el mismo usuario que está ejecutando la aplicación, se asigna null a la variable de Session para tal fin.
			If mobjValues.StringToType(.QueryString.Item("nUser"), eFunctions.Values.eTypeData.etdDouble) = mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble) Then
				Session("nCashNum") = eRemoteDB.Constants.intNull
			End If
			
			lclsUser_cashnum = Nothing
		End If
	End With
	Response.Write(mobjGrid.DoFormUpd(Request.QueryString.Item("Action"), "valMantCashBank.aspx", Request.QueryString.Item("sCodispl"), Request.QueryString.Item("nMainAction"), mobjValues.ActionQuery, CShort(Request.QueryString.Item("Index"))))
	Response.Write(mobjValues.HiddenControl("sAction", Request.QueryString.Item("Action")))
End Sub

</script>
<%Response.Expires = -1

mobjValues = New eFunctions.Values
mobjValues.sCodisplPage = "MOP634"
%>
<HTML>
<HEAD>
<SCRIPT>
//%Variable para el control de las versiones
    document.VssVersion="$$Revision: 2 $|$$Date: 15/10/03 15:51 $"
</SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>


	<META NAME = "GENERATOR" Content = "Microsoft Visual Studio 6.0">
<%
If Request.QueryString.Item("Type") <> "PopUp" Then
	Response.Write("<SCRIPT>var nMainAction=0</SCRIPT>")
	Response.Write("<SCRIPT LANGUAGE=""JavaScript"" SRC=""/VTimeNet/Scripts/tmenu.js""></SCRIPT>" & vbCrLf)
End If
With Response
	.Write(mobjValues.StyleSheet() & vbCrLf)
	If Request.QueryString.Item("Type") <> "PopUp" Then
		mobjMenu = New eFunctions.Menues
		.Write(mobjMenu.MakeMenu(Request.QueryString.Item("sCodispl"), "MOP634_k.aspx", 1, ""))
		mobjMenu = Nothing
	End If
End With
%>
<SCRIPT>
//InsChangeUser: Llama al procedimiento que obtiene la oficina asociada al usuario
//--------------------------------------------------------------------------------------------
function InsChangeUser(nUsercode){
//--------------------------------------------------------------------------------------------
	ShowPopUp('/VTimeNet/Maintenance/MantCashBank/ShowDefValues.aspx?sField=MOP634&nUser='+nUsercode, 'ShowDefValues', 1, 1,'no','no',2000,2000);    
}
</SCRIPT>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<%
If Request.QueryString.Item("Type") <> "PopUp" Then
	Response.Write("<BR><BR>")
End If
%>
<FORM METHOD="POST" ID="FORM" NAME="frmMOP634" ACTION="valMantCashBank.aspx?mode=1">
 <%
Call insDefineHeader()
If Request.QueryString.Item("Type") <> "PopUp" Then
	Call insPreMOP634()
Else
	Response.Write(mobjValues.ShowWindowsName(Request.QueryString.Item("sCodispl")))
	Call insPreMOP634Upd()
End If
mobjValues = Nothing
%>
</FORM>
</BODY>
</HTML>




