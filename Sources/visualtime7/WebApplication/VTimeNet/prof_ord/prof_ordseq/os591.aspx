<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eClaim" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'- Objeto para el manejo del grid de la página
Dim mobjGrid As eFunctions.Grid

'- Objeto para el manejo del menú
Dim mobjMenu As eFunctions.Menues

'- Objeto para el manejo particular de los datos de la página
Dim mcolauto_damage As eClaim.Auto_damages


'% insDefineHeader: se definen las propiedades del grid
'--------------------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'--------------------------------------------------------------------------------------------
	
	mobjGrid = New eFunctions.Grid
	
	mobjGrid.sCodisplPage = "os591"
	
	Dim sAccion As String
	sAccion = Request.QueryString.Item("Action")
	'+ Se definen las columnas del grid  
	With mobjGrid.Columns
		If Request.QueryString.Item("Action") = "Add" Then
			Call .AddPossiblesColumn(0, GetLocalResourceObject("cbenpart_autoColumnCaption"), "cbenpart_auto", "Table5533", eFunctions.Values.eValuesType.clngComboType, CStr(eRemoteDB.Constants.intNull),  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbenpart_autoColumnToolTip"))
		Else
			Call .AddPossiblesColumn(0, GetLocalResourceObject("cbenpart_autoColumnCaption"), "cbenpart_auto", "Table5533", eFunctions.Values.eValuesType.clngComboType, CStr(eRemoteDB.Constants.intNull),  ,  ,  ,  ,  , True,  , GetLocalResourceObject("cbenpart_autoColumnToolTip"))
		End If
		Call .AddPossiblesColumn(0, GetLocalResourceObject("cbendamag_autoColumnCaption"), "cbendamag_auto", "Table5534", eFunctions.Values.eValuesType.clngComboType, CStr(eRemoteDB.Constants.intNull),  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbendamag_autoColumnToolTip"))
		Call .AddPossiblesColumn(0, GetLocalResourceObject("cbendamage_magnifColumnCaption"), "cbendamage_magnif", "Table5535", eFunctions.Values.eValuesType.clngComboType, CStr(eRemoteDB.Constants.intNull),  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbendamage_magnifColumnToolTip"))
		Call .AddNumericColumn(0, GetLocalResourceObject("tctndeducColumnCaption"), "tctndeduc", 4, CStr(eRemoteDB.Constants.intNull),  , GetLocalResourceObject("tctndeducColumnToolTip"),  , 2)
	End With
	
	'+ Se definen las propiedades generales del grid
	
	With mobjGrid
		.Columns("cbenpart_auto").BlankPosition = False
		.Columns("cbendamag_auto").BlankPosition = False
		.Columns("cbendamage_magnif").BlankPosition = False
		.codispl = "OS591"
		.ActionQuery = mobjValues.ActionQuery
		.Columns("cbenpart_auto").EditRecord = True
		.Height = 230
		.Width = 350
		.nMainAction = CShort(IIf(Request.QueryString.Item("nMainAction") ="", 0, Request.QueryString.Item("nMainAction")))
		.Columns("Sel").GridVisible = Not .ActionQuery
		.sDelRecordParam = "npart_auto='+ marrArray[lintIndex].cbenpart_auto + '"
		If Request.QueryString.Item("Reload") = "1" Then
			.sReloadIndex = Request.QueryString.Item("ReloadIndex")
		End If
	End With
End Sub

'% insPreOS591: se realiza el manejo del grid
'--------------------------------------------------------------------------------------------
Private Sub insPreOS591()
	'--------------------------------------------------------------------------------------------
	Dim lclsauto_damage As Object
	
	mcolauto_damage = New eClaim.Auto_damages
	If mcolauto_damage.Find(Session("Nserv_order")) Then
		For	Each lclsauto_damage In mcolauto_damage
			With mobjGrid
				.Columns("cbenpart_auto").DefValue = lclsauto_damage.npart_auto
				.Columns("cbendamag_auto").DefValue = lclsauto_damage.ndamag_auto
				.Columns("cbendamage_magnif").DefValue = lclsauto_damage.ndamage_magnif
				.Columns("tctndeduc").DefValue = lclsauto_damage.ndeduc
				Response.Write(.DoRow)
			End With
		Next lclsauto_damage
	End If
	
	Response.Write(mobjGrid.closeTable())
End Sub

'% insPreOS591Upd: Se realiza el manejo de la ventana PopUp asociada al grid
'--------------------------------------------------------------------------------------------
Private Sub insPreOS591Upd()
	'--------------------------------------------------------------------------------------------
	'*++ Modificar nombre del objeto. Modificar "Class" por el nombre de la clase con la cual se trabaja
	Dim lobjClass As eClaim.Auto_damage
	lobjClass = New eClaim.Auto_damage
	With Request
		If .QueryString.Item("Action") = "Del" Then
			Response.Write(mobjValues.ConfirmDelete())
			If lobjClass.InsPostOS591(.QueryString.Item("Action"), mobjValues.StringToType(Session("Nserv_order"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("npart_auto"), eFunctions.Values.eTypeData.etdDouble), eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble)) Then
			End If
		End If
		Response.Write(mobjGrid.DoFormUpd(.QueryString.Item("Action"), "valProf_ordseq.aspx", "OS591", .QueryString.Item("nMainAction"), mobjValues.ActionQuery, CShort(.QueryString.Item("Index"))))
	End With
End Sub

</script>
<%Response.Expires = -1

mobjValues = New eFunctions.Values
mobjMenu = New eFunctions.Menues
mobjValues.ActionQuery = CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 401

mobjValues.sCodisplPage = "os591"
%>
<HTML>
<HEAD>
<SCRIPT>
//- Variable para el control de versiones
	document.VssVersion="$$Revision: 2 $|$$Date: 15/10/03 18.00 $"
</SCRIPT>	
<META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>


<%
Response.Write(mobjValues.StyleSheet())
If Request.QueryString.Item("Type") <> "PopUp" Then
	Response.Write(mobjMenu.setZone(2, "OS591", "OS591.aspx"))
	
	mobjMenu = Nothing
	Response.Write("<SCRIPT>var nMainAction=top.frames['fraSequence'].plngMainAction</SCRIPT>")
End If
%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" NAME="OS591" ACTION="valProf_ordseq.aspx?nMainAction=<%=Request.QueryString.Item("nMainAction")%>">
<%Response.Write(mobjValues.ShowWindowsName("OS591"))
Call insDefineHeader()
If Request.QueryString.Item("Type") = "PopUp" Then
	Call insPreOS591Upd()
Else
	Call insPreOS591()
End If
%>
</FORM> 
</BODY>
</HTML>





