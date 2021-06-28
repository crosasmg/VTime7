<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eAgent" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores

Dim mobjValues As eFunctions.Values
'- Objeto para el manejo de las rutinas genéricas

Dim mobjMenu As eFunctions.Menues
Dim mobjGrid As eFunctions.Grid



'+ insDefineHeader: Definición del encabezado del Grid
'------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'------------------------------------------------------------------------------
	mobjGrid = New eFunctions.Grid
	
	'+ Se definen las columns del Grid
	With mobjGrid.Columns
		If Request.QueryString.Item("Action") = "Update" Then
			Call .AddNumericColumn(40590, GetLocalResourceObject("tcnTypeColumnCaption"), "tcnType", 5, CStr(eRemoteDB.Constants.strNull), True, GetLocalResourceObject("tcnTypeColumnToolTip"),  ,  ,  ,  ,  , True)
		Else
			Call .AddNumericColumn(40591, GetLocalResourceObject("tcnTypeColumnCaption"), "tcnType", 5, CStr(eRemoteDB.Constants.strNull), True, GetLocalResourceObject("tcnTypeColumnToolTip"))
		End If
		Call .AddTextColumn(40592, GetLocalResourceObject("tctDescriptColumnCaption"), "tctDescript", 30, CStr(eRemoteDB.Constants.strNull), True, GetLocalResourceObject("tctDescriptColumnToolTip"))
		Call .AddTextColumn(40593, GetLocalResourceObject("tctShortdesColumnCaption"), "tctShortdes", 12, CStr(eRemoteDB.Constants.strNull), True, GetLocalResourceObject("tctShortdesColumnToolTip"))
		Call .AddPossiblesColumn(0, GetLocalResourceObject("cbeTyp_AccoColumnCaption"), "cbeTyp_Acco", "Table400", eFunctions.Values.eValuesType.clngComboType, vbNullString,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbeTyp_AccoColumnCaption"))
		Call .AddPossiblesColumn(40588, GetLocalResourceObject("cbeParticinColumnCaption"), "cbeParticin", "Table23", eFunctions.Values.eValuesType.clngComboType, CStr(1),  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbeParticinColumnCaption"))
		Call .AddCheckColumn(0, GetLocalResourceObject("chkInd_FECUColumnCaption"), "chkInd_FECU", vbNullString,  , "1",  , Request.QueryString.Item("Type") <> "PopUp", GetLocalResourceObject("chkInd_FECUColumnToolTip"))
		Call .AddCheckColumn(0, GetLocalResourceObject("chkGen_certifColumnCaption"), "chkGen_certif", vbNullString,  , "1",  , Request.QueryString.Item("Type") <> "PopUp", GetLocalResourceObject("chkGen_certifColumnToolTip"))
		Call .AddPossiblesColumn(40589, GetLocalResourceObject("cbeStatregtColumnCaption"), "cbeStatregt", "Table26", eFunctions.Values.eValuesType.clngComboType, vbNullString,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbeStatregtColumnToolTip"))
	End With
	
	'+ Se asignan las caracteristicas del Grid
	
	With mobjGrid
		.Columns("cbeParticin").BlankPosition = False
		.Columns("cbeStatregt").TypeList = 2
		.Columns("cbeStatregt").List = CStr(2)
		.Columns("tctDescript").EditRecord = True
		.Codispl = Request.QueryString.Item("sCodispl")
		.Codisp = "MAG001_K"
		If Request.QueryString.Item("nMainAction") = "401" Or Request.QueryString.Item("nMainAction") = vbNullString Then
			mobjGrid.ActionQuery = True
			mobjGrid.Columns("Sel").GridVisible = False
		End If
		.sDelRecordParam = "nIntertyp='+ marrArray[lintIndex].tcnType + '"
		.Height = 360
		.Width = 400
		.Top = 100
		.Left = 100
		.sCodisplPage = "MAG001"
		
		If Request.QueryString.Item("Reload") = "1" Then
			.sReloadIndex = Request.QueryString.Item("ReloadIndex")
		End If
	End With
	
End Sub

'+ insPreMAG001: Carga de los registros de la tabla interm_typ en el grid
'------------------------------------------------------------------------------
Private Sub insPreMAG001()
	'------------------------------------------------------------------------------
	Dim lcolInterm_Typs As eAgent.Interm_typs
	Dim lclsInterm_typ As Object
	
	lcolInterm_Typs = New eAgent.Interm_typs
	
	If lcolInterm_Typs.Find() Then
		For	Each lclsInterm_typ In lcolInterm_Typs
			With mobjGrid
				.Columns("tcnType").DefValue = lclsInterm_typ.nInterTyp
				.Columns("tctDescript").DefValue = lclsInterm_typ.sDescript
				.Columns("tctShortdes").DefValue = lclsInterm_typ.sShort_des
				.Columns("cbeParticin").DefValue = lclsInterm_typ.sParticin
				.Columns("cbeStatregt").DefValue = lclsInterm_typ.sStatregt
				.Columns("cbeTyp_Acco").DefValue = lclsInterm_typ.nTyp_Acco
				.Columns("chkInd_FECU").Checked = lclsInterm_typ.sInd_FECU
				.Columns("chkGen_certif").Checked = lclsInterm_typ.sGen_certif
			End With
			
			'+Se ejecuta el metodo DoRow, que se encarga de mostrar los elementos de grid
			
			Response.Write(mobjGrid.DoRow())
		Next lclsInterm_typ
	End If
	Response.Write(mobjGrid.closeTable())
	Response.Write(mobjValues.BeginPageButton)
	
	lcolInterm_Typs = Nothing
	
End Sub

'+ insPreMAG001Upd: Actualización de la tabla interm_typ
'------------------------------------------------------------------------------
Private Sub insPreMAG001Upd()
	'------------------------------------------------------------------------------
	Dim lclsInterm_typ As eAgent.Interm_typ
	Dim lstrErrors As String
	Dim mstrCommand As String
	mstrCommand = "sModule=Maintenance&sProject=MantAgent&sCodisplReload=MAG001"
	
	If Request.QueryString.Item("Action") = "Del" Then
		lclsInterm_typ = New eAgent.Interm_typ
		lstrErrors = lclsInterm_typ.insValMAG001("MAG001", "Del", 1, CInt(Request.QueryString.Item("nIntertyp")), CStr(eRemoteDB.Constants.strNull), CStr(eRemoteDB.Constants.strNull), CStr(eRemoteDB.Constants.strNull), CStr(eRemoteDB.Constants.strNull))
		If lstrErrors = vbNullString Then
			Response.Write(mobjValues.ConfirmDelete())
			With lclsInterm_typ
				.nInterTyp = mobjValues.StringToType(Request.QueryString.Item("nIntertyp"), eFunctions.Values.eTypeData.etdDouble)
				.Delete()
			End With
		Else
			Session("sErrorTable") = lstrErrors
			With Response
				.Write("<SCRIPT LANGUAGE=JAVASCRIPT>")
				.Write("ShowPopUp(""/VTimeNet/Common/Errors.aspx?sSource=" & Server.URLEncode(mstrCommand) & "&sQueryString=" & Server.URLEncode(Request.Params.Get("Query_String")) & """, ""MantAgentError"",660,330);")
				.Write("</" & "Script>")
			End With
Response.Write("" & vbCrLf)
Response.Write("			<SCRIPT>top.window.close()</" & "SCRIPT>")

		End If
		lclsInterm_typ = Nothing
	End If
	
	Response.Write(mobjGrid.DoFormUpd(Request.QueryString.Item("Action"), "valMantAgent.aspx", Request.QueryString.Item("sCodispl"), Request.QueryString.Item("MainAction"),  , CShort(Request.QueryString.Item("Index"))))
	Response.Write(mobjValues.HiddenControl("sAction", Request.QueryString.Item("Action")))
	
End Sub

</script>
<%Response.Expires = -1

mobjValues = New eFunctions.Values
mobjValues.sCodisplPage = "MAG001"
%>

<HTML>
<HEAD>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
    <META NAME = "GENERATOR" CONTENT = "Microsoft Visual Studio 6.0">


	
<SCRIPT>
//+ Variable para el control de versiones
    document.VssVersion="$$Revision: 3 $|$$Date: 5/12/03 15:59 $|$$Author: Nvaplat18 $"    

//% insCancel: Funcion que cancela las las acciones de la Pagina
//-------------------------------------------------------------------------------------------------------------------
function insCancel(){
//-------------------------------------------------------------------------------------------------------------------
	return(true)
}

//+ insStateZone: Controla el estado de los campos de la página
//-------------------------------------------------------------------------------------------------------------------
function insStateZone(){
//-------------------------------------------------------------------------------------------------------------------
}

//+ insPreZone: Controla las acciones a ejecutar sobre la ventana
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
</SCRIPT>

<%
If Request.QueryString.Item("Type") <> "PopUp" Then
	Response.Write("<SCRIPT>var nMainAction = " & CShort("0" & Request.QueryString.Item("nMainAction")) & "</SCRIPT>")
End If
With Response
	.Write(mobjValues.StyleSheet())
	If Request.QueryString.Item("Type") <> "PopUp" Then
		.Write("<SCRIPT LANGUAGE=""JavaScript"" SRC=""/VTimeNet/Scripts/tmenu.js""></SCRIPT>")
		mobjMenu = New eFunctions.Menues
		.Write(mobjMenu.MakeMenu(Request.QueryString.Item("sCodispl"), "MAG001_k.aspx", 1, ""))
		mobjMenu = Nothing
	End If
End With


If Request.QueryString.Item("Action") <> "Del" Then
	Response.Write("<BODY ONUNLOAD='closeWindows();'>")
Else
	Response.Write("<BODY>")
End If

If Request.QueryString.Item("Type") <> "PopUp" Then
	Response.Write("<BR><BR>")
End If

Response.Write(mobjValues.ShowWindowsName(Request.QueryString.Item("sCodispl")))
%>

</HEAD>

<FORM METHOD="POST" ID="FORM" NAME="frmIntermType" ACTION="valMantAgent.aspx?mode=1">
 <%
Call insDefineHeader()
If Request.QueryString.Item("Type") <> "PopUp" Then
	Call insPreMAG001()
Else
	Call insPreMAG001Upd()
End If

mobjValues = Nothing
mobjGrid = Nothing
%>
</FORM>
</BODY>
</HTML>





