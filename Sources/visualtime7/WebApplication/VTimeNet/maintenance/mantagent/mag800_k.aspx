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
Dim mcolBud_agen As eAgent.Bud_agens


'% insDefineHeader: se definen las propiedades del grid
'--------------------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'--------------------------------------------------------------------------------------------
	mobjGrid = New eFunctions.Grid
	
	'+ Se definen las columnas del grid    
	With mobjGrid.Columns
		If Request.QueryString.Item("Type") <> "PopUp" Then
			Call .AddNumericColumn(0, GetLocalResourceObject("tcnAgencyColumnCaption"), "tcnAgency", 10,  ,  , GetLocalResourceObject("tcnAgencyColumnToolTip"),  ,  ,  ,  ,  , True)
		End If
		Call .AddPossiblesColumn(0, GetLocalResourceObject("valAgencyColumnCaption"), "valAgency", "Table5555", eFunctions.Values.eValuesType.clngWindowType,  ,  ,  ,  ,  ,  , Request.QueryString.Item("Action") = "Update",  , GetLocalResourceObject("valAgencyColumnToolTip"))
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnAgent_quanColumnCaption"), "tcnAgent_quan", 5, vbNullString,  , GetLocalResourceObject("tcnAgent_quanColumnToolTip"))
	End With
	
	'+ Se definen las propiedades generales del grid
	
	With mobjGrid
		.Codispl = "MAG800"
		.sCodisplPage = "MAG800"
		.ActionQuery = mobjValues.ActionQuery Or IsNothing(Request.QueryString.Item("nMainAction"))
		.Columns("valAgency").EditRecord = True
		.Height = 220
		.Width = 370
		.nMainAction = CShort(IIf(Request.QueryString.Item("nMainAction") ="", 0, Request.QueryString.Item("nMainAction")))
		.Columns("Sel").GridVisible = Not .ActionQuery
		If Request.QueryString.Item("Reload") = "1" Then
			.sReloadIndex = Request.QueryString.Item("ReloadIndex")
		End If
		
		.sDelRecordParam = "nAgency='+ marrArray[lintIndex].valAgency + '" & "&nAgent_quan='+marrArray[lintIndex].tcnAgent_quan + '"
	End With
End Sub


'% insPreMAG800: se realiza el manejo del grid
'--------------------------------------------------------------------------------------------
Private Sub insPreMAG800()
	'--------------------------------------------------------------------------------------------
	
	Dim lclsBud_Agen As Object
	
	mcolBud_agen = New eAgent.Bud_agens
	
	If mcolBud_agen.Find() Then
		For	Each lclsBud_Agen In mcolBud_agen
			With mobjGrid
				.Columns("tcnAgency").DefValue = lclsBud_Agen.nAgency
				.Columns("valAgency").DefValue = lclsBud_Agen.nAgency
				.Columns("tcnAgent_quan").DefValue = lclsBud_Agen.nAgent_quan
				Response.Write(.DoRow)
			End With
		Next lclsBud_Agen
	End If
	
	Response.Write(mobjGrid.closeTable())
End Sub

'% insPreMAG800Upd: Se realiza el manejo de la ventana PopUp asociada al grid
'--------------------------------------------------------------------------------------------
Private Sub insPreMAG800Upd()
	'--------------------------------------------------------------------------------------------
	
	Dim lobjBud_Agen As eAgent.Bud_agen
	
	lobjBud_Agen = New eAgent.Bud_agen
	
	With Request
		If Request.QueryString.Item("Action") = "Del" Then
			Response.Write(mobjValues.ConfirmDelete())
			With lobjBud_Agen
				.nAgency = CInt(Request.QueryString.Item("nAgency"))
				.nAgent_quan = CInt(Request.QueryString.Item("nAgent_quan"))
				.Delete()
			End With
		End If
		
		Response.Write(mobjGrid.DoFormUpd(.QueryString.Item("Action"), "valMantAgent.aspx", "MAG800", .QueryString.Item("nMainAction"), mobjValues.ActionQuery, CShort(.QueryString.Item("Index"))))
	End With
	lobjBud_Agen = Nothing
End Sub

</script>
<%Response.Expires = -1

mobjValues = New eFunctions.Values
mobjMenu = New eFunctions.Menues

mobjValues.ActionQuery = CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 401
mobjValues.sCodisplPage = "MAG800"
%>
<HTML>
<HEAD>
	<META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>


    
<SCRIPT>
//+ Variable para el control de versiones
    document.VssVersion="$$Revision: 1 $|$$Date: 2/09/03 19:02 $"

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
	Response.Write(mobjMenu.MakeMenu("MAG800", "MAG800_K.aspx", 1, vbNullString))
	mobjMenu = Nothing
	Response.Write("<SCRIPT>var nMainAction=top.frames['fraSequence'].plngMainAction</SCRIPT>")
End If
%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" NAME="MAG800" ACTION="valMantAgent.aspx?sMode=2">
<%
If Request.QueryString.Item("Type") <> "PopUp" Then
	Response.Write("<BR><BR>")
End If
Response.Write(mobjValues.ShowWindowsName("MAG800"))

Call insDefineHeader()
If Request.QueryString.Item("Type") = "PopUp" Then
	Call insPreMAG800Upd()
Else
	Call insPreMAG800()
End If
%>
</FORM> 
</BODY>
</HTML>

<%
mobjValues = Nothing
mobjGrid = Nothing
mcolBud_agen = Nothing

%>




