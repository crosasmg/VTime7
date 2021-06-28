<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eAgent" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'- Objeto para el manejo del grid de la página
Dim mobjGrid As eFunctions.Grid

'- Objeto para el manejo del menú
Dim mobjMenu As eFunctions.Menues

'- Objeto para el manejo particular de los datos de la página
Dim mcolAdvance_users As eAgent.Advance_userss


'% insDefineHeader: se definen las propiedades del grid
'--------------------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'--------------------------------------------------------------------------------------------
	mobjGrid = New eFunctions.Grid
	
	'+ Se definen las columnas del grid    
	With mobjGrid.Columns
		Call .AddPossiblesColumn(0, GetLocalResourceObject("cbeUserColumnCaption"), "cbeUser", "Tabusers", eFunctions.Values.eValuesType.clngWindowType,  ,  ,  ,  ,  ,  , Request.QueryString.Item("Action") = "Update", 5, GetLocalResourceObject("cbeUserColumnToolTip"))
		Call .AddPossiblesColumn(0, GetLocalResourceObject("cbeCodModPayColumnCaption"), "cbeCodModPay", "Table5601", eFunctions.Values.eValuesType.clngComboType,  ,  ,  ,  ,  ,  , Request.QueryString.Item("Action") = "Update", 5, GetLocalResourceObject("cbeCodModPayColumnToolTip"))
		Call .AddPossiblesColumn(0, GetLocalResourceObject("cbeStatregtColumnCaption"), "cbeStatregt", "Table26", eFunctions.Values.eValuesType.clngComboType,  ,  ,  ,  ,  ,  ,  , 5, GetLocalResourceObject("cbeStatregtColumnToolTip"))
	End With
	
	'+ Se definen las propiedades generales del grid
	
	With mobjGrid
		.Codispl = "MAG770"
		.sCodisplPage = "MAG770"
		.ActionQuery = mobjValues.ActionQuery Or IsNothing(Request.QueryString.Item("nMainAction"))
		.Columns("cbeUser").EditRecord = True
		.Height = 220
		.Width = 370
		.nMainAction = CShort(IIf(Request.QueryString.Item("nMainAction") ="", 0, Request.QueryString.Item("nMainAction")))
		.Columns("Sel").GridVisible = Not .ActionQuery
		If Request.QueryString.Item("Reload") = "1" Then
			.sReloadIndex = Request.QueryString.Item("ReloadIndex")
		End If
		.sDelRecordParam = "nUsers='+ marrArray[lintIndex].cbeUser + '" & "&nCodModPay='+marrArray[lintIndex].cbeCodModPay + '"
	End With
End Sub

'% insPreMAG770: se realiza el manejo del grid
'--------------------------------------------------------------------------------------------
Private Sub insPreMAG770()
	'--------------------------------------------------------------------------------------------
	
	Dim lclsAdvance_users As Object
	
	mcolAdvance_users = New eAgent.Advance_userss
	
	If mcolAdvance_users.Find() Then
		For	Each lclsAdvance_users In mcolAdvance_users
			With mobjGrid
				.Columns("cbeUser").DefValue = lclsAdvance_users.nUser
				.Columns("cbeCodModPay").DefValue = lclsAdvance_users.nCodModPay
				.Columns("cbeStatregt").DefValue = lclsAdvance_users.sStatregt
				Response.Write(.DoRow)
			End With
		Next lclsAdvance_users
	End If
	
	Response.Write(mobjGrid.closeTable())
End Sub

'% insPreMAG770Upd: Se realiza el manejo de la ventana PopUp asociada al grid
'--------------------------------------------------------------------------------------------
Private Sub insPreMAG770Upd()
	'--------------------------------------------------------------------------------------------
	
	Dim lobjAdvance_users As eAgent.Advance_users
	
	lobjAdvance_users = New eAgent.Advance_users
	
	With Request
		If Request.QueryString.Item("Action") = "Del" Then
			Response.Write(mobjValues.ConfirmDelete())
			With lobjAdvance_users
				.nUser = mobjValues.StringToType(Request.QueryString.Item("nUsers"), eFunctions.Values.eTypeData.etdDouble)
				.nCodModPay = mobjValues.StringToType(Request.QueryString.Item("nCodModPay"), eFunctions.Values.eTypeData.etdDouble)
				.Delete()
			End With
		End If
		
		Response.Write(mobjGrid.DoFormUpd(.QueryString.Item("Action"), "valMantAgent.aspx", "MAG770", .QueryString.Item("nMainAction"), mobjValues.ActionQuery, CShort(.QueryString.Item("Index"))))
	End With
	lobjAdvance_users = Nothing
End Sub

</script>
<%Response.Expires = -1

mobjValues = New eFunctions.Values
mobjMenu = New eFunctions.Menues

mobjValues.ActionQuery = CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 401
mobjValues.sCodisplPage = "MAG770"
%>
<HTML>
<HEAD>
	<META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>	



<SCRIPT LANGUAGE=JavaScript>
//+ Variable para el control de versiones
    document.VssVersion="$$Revision: 2 $|$$Date: 15/10/03 15:35 $"

//% insStateZone: se controla el estado de los campos de la página
//--------------------------------------------------------------------------------------------
function insStateZone(){
//--------------------------------------------------------------------------------------------
}

//% insCancel: se controla la acción Cancelar de la página
//--------------------------------------------------------------------------------------------
function insCancel(){
//--------------------------------------------------------------------------------------------
	return true;
}

//% insFinish: se controla la acción Cancelar de la página
//--------------------------------------------------------------------------------------------
function insFinish(){
//--------------------------------------------------------------------------------------------
    return true;
}
//% insPreZone: se controla la acción de busqueda por condición
//-------------------------------------------------------------------------------------------------------------------
function insPreZone(llngAction){
//-------------------------------------------------------------------------------------------------------------------
    switch (llngAction){
        case 302:
        case 305:
        case 401:
	        document.location.href = document.location.href.replace(/&nMainAction.*/,'') + '&nMainAction=' + llngAction            
            break;
    }
}
</SCRIPT>
	<%
Response.Write(mobjValues.StyleSheet())
If Request.QueryString.Item("Type") <> "PopUp" Then
	Response.Write("<SCRIPT LANGUAGE=""JavaScript"" SRC=""/VTimeNet/Scripts/tmenu.js""></SCRIPT>")
	Response.Write(mobjMenu.MakeMenu("MAG770", "MAG770_K.aspx", 1, vbNullString))
	mobjMenu = Nothing
	Response.Write("<SCRIPT>var nMainAction=top.frames['fraSequence'].plngMainAction</SCRIPT>")
End If
%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" NAME="MAG770" ACTION="valMantAgent.aspx?sMode=2">

<%
If Request.QueryString.Item("Type") <> "PopUp" Then
	Response.Write("<BR><BR>")
End If
Response.Write(mobjValues.ShowWindowsName("MAG770"))

Call insDefineHeader()
If Request.QueryString.Item("Type") = "PopUp" Then
	Call insPreMAG770Upd()
Else
	Call insPreMAG770()
End If
%>
</FORM> 
</BODY>
</HTML>

<%
mobjValues = Nothing
mobjGrid = Nothing
mcolAdvance_users = Nothing

%>




