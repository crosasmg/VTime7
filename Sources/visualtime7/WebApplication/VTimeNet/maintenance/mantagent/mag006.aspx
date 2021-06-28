<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eAgent" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'- Objeto para el manejo de las rutinas genéricas
Dim mobjGrid As eFunctions.Grid

'- Objeto para el manejo de las zonas de la página
Dim mobjMenues As eFunctions.Menues


'+ Defincion del Encabezado del Grid
'------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'------------------------------------------------------------------------------
	mobjGrid = New eFunctions.Grid
	
	'+ Se definen las columns del Grid
	With mobjGrid.Columns
		Call .AddNumericColumn(100009, GetLocalResourceObject("tcnTable_codColumnCaption"), "tcnTable_cod", 5, CStr(eRemoteDB.Constants.strNull), True, GetLocalResourceObject("tcnTable_codColumnToolTip"),  ,  ,  ,  ,  , Request.QueryString.Item("Action") = "Update")
		Call .AddTextColumn(100010, GetLocalResourceObject("tctDescriptColumnCaption"), "tctDescript", 30, CStr(eRemoteDB.Constants.strNull), True, GetLocalResourceObject("tctDescriptColumnToolTip"))
		Call .AddTextColumn(100011, GetLocalResourceObject("tctShort_desColumnCaption"), "tctShort_des", 12, CStr(eRemoteDB.Constants.strNull), True, GetLocalResourceObject("tctShort_desColumnToolTip"))
		If Session("nCommType") = 3 Then
			Call .AddPossiblesColumn(100008, GetLocalResourceObject("cbeType_assigColumnCaption"), "cbeType_assig", "Table291", eFunctions.Values.eValuesType.clngComboType, "",  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbeType_assigColumnToolTip"))
		End If
		Call .AddPossiblesColumn(100008, GetLocalResourceObject("cbeStatregtColumnCaption"), "cbeStatregt", "Table26", eFunctions.Values.eValuesType.clngComboType, CStr(eRemoteDB.Constants.strNull),  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbeStatregtColumnToolTip"))
	End With
	
	'+ Se asignan las caracteristicas del Grid
	
	With mobjGrid
		.Columns("tctDescript").EditRecord = True
		.Columns("cbeStatregt").BlankPosition = False
		.Codispl = "MAG006"
		.Codisp = "MAG006"
		.sCodisplPage = "MAG006"
		
		'+ Si la transacción es "Consulta", se oculta la columna SEL 
		If Request.QueryString.Item("nMainAction") = "401" Or Request.QueryString.Item("nMainAction") = vbNullString Then
			mobjGrid.ActionQuery = True
			mobjGrid.Columns("Sel").GridVisible = False
		End If
		
		'+ Si la transacción es "Agregar", se desabilita el campo ESTADO y se coloca en 
		'+ "En proceso de instalación" (sStatregt = 2)
		If Request.QueryString.Item("Action") = "Add" Then
			mobjGrid.Columns("cbeStatregt").DefValue = CStr(2)
			mobjGrid.Columns("cbeStatregt").Disabled = True
		End If
		
		'+ El estado "En proceso de instalación" (sStatregt = 2) solo es usado por el sistema
		
		.sDelRecordParam = "nTable_cod='+marrArray[lintIndex].tcnTable_cod + '"
		.Height = 300
		.Width = 350
		
		'+Duplicacion del registro
		If Request.QueryString.Item("nMainAction") = CStr(eFunctions.Menues.TypeActions.clngActionDuplicate) Then
			.Columns("tctDescript").EditRecord = False
			.Columns("Sel").GridVisible = True
			.Columns("Sel").Disabled = False
			.AddButton = False
			.DeleteButton = False
		End If
		
		If Request.QueryString.Item("Reload") = "1" Then
			.sReloadIndex = Request.QueryString.Item("ReloadIndex")
		End If
	End With
End Sub

'+ Carga de los registros en el Grid de la Tabla de Comisiones seleccioanda
'------------------------------------------------------------------------------
Private Sub insPreMAG006()
	'------------------------------------------------------------------------------
	Dim lcolTab_commissions As eAgent.Tab_commissions
	Dim lclsTab_commission As Object
	
Response.Write("		" & vbCrLf)
Response.Write("" & vbCrLf)
Response.Write("<SCRIPT>" & vbCrLf)
Response.Write("//--------------------------------------------------------------------------------------------------------" & vbCrLf)
Response.Write("function insDupConv(Field,Action,TabDup,Desc){" & vbCrLf)
Response.Write("//--------------------------------------------------------------------------------------------------------    " & vbCrLf)
Response.Write("" & vbCrLf)
Response.Write("  if (Action == ""306"" ) {" & vbCrLf)
Response.Write("     if (Field.checked){" & vbCrLf)
Response.Write("        ShowPopUp(""MAG006dup.aspx?nTabdup=""+TabDup+""&nAction=""+Action+""&sCodispl=MAG006&tctDesc="" + Desc ,""MAG006"",400,200,""no"",""no"",200,100);" & vbCrLf)
Response.Write("	}" & vbCrLf)
Response.Write("}" & vbCrLf)
Response.Write("}" & vbCrLf)
Response.Write("" & vbCrLf)
Response.Write("</" & "SCRIPT> ")

	
	
	lcolTab_commissions = New eAgent.Tab_commissions
	
	'+ Se asignan los valores leidos que se mostrarán en pantalla
	If lcolTab_commissions.Find(mobjValues.StringToType(Session("nCommType"), eFunctions.Values.eTypeData.etdInteger)) Then
		For	Each lclsTab_commission In lcolTab_commissions
			With mobjGrid
				.Columns("Sel").OnClick = "insDupConv(this,""" & Request.QueryString.Item("nMainAction") & """," & CStr(lclsTab_commission.nTable_cod) & ",""" & lclsTab_commission.sDescript & """)"
				.Columns("tcnTable_cod").DefValue = lclsTab_commission.nTable_cod
				.Columns("tctDescript").DefValue = lclsTab_commission.sDescript
				.Columns("tctShort_des").DefValue = lclsTab_commission.sShort_des
				If Session("nCommType") = 3 Then
					.Columns("cbeType_assig").DefValue = lclsTab_commission.sType_assig
				End If
				.Columns("cbeStatregt").DefValue = lclsTab_commission.sStatregt
				
			End With
			
			'+Se ejecuta el metodo DoRow, que se encarga de mostrar los elementos de grid
			
			Response.Write(mobjGrid.DoRow())
		Next lclsTab_commission
	End If
	Response.Write(mobjGrid.closeTable())
End Sub

'+ Actualización de la Tabla de Comision seleccionada
'------------------------------------------------------------------------------
Private Sub insPreMAG006Upd()
	'------------------------------------------------------------------------------
	Dim lclsTab_commission As eAgent.Tab_Commission
	Dim lstrErrors As String
	Dim mstrCommand As String
	mstrCommand = "sModule=Maintenance&sProject=MantAgent&sCodisplReload=MAG006"
	
	If Request.QueryString.Item("Action") = "Del" Then
		
		lclsTab_commission = New eAgent.Tab_Commission
		
		With lclsTab_commission
			.nCommType = mobjValues.StringToType(Session("nCommType"), eFunctions.Values.eTypeData.etdInteger)
			.nTable_cod = mobjValues.StringToType(Request.QueryString.Item("nTable_cod"), eFunctions.Values.eTypeData.etdInteger)
			lstrErrors = .insValMAG006(Request.QueryString.Item("sCodispl"), Request.QueryString.Item("Action"), eRemoteDB.Constants.intNull, .nCommType, .nTable_cod, vbNullString, vbNullString, vbNullString, vbNullString)
			If lstrErrors = vbNullString Then
				Response.Write(mobjValues.ConfirmDelete())
				.Delete()
			Else
				Session("sErrorTable") = lstrErrors
				With Response
					.Write("<SCRIPT LANGUAGE=JAVASCRIPT>")
					.Write("ShowPopUp(""/VTimeNet/Common/Errors.aspx?sSource=" & Server.URLEncode(mstrCommand) & "&sQueryString=" & Server.URLEncode(Request.Params.Get("Query_String")) & """, ""MantAgentError"",660,330);")
					.Write("</" & "Script>")
				End With
Response.Write("" & vbCrLf)
Response.Write("				<SCRIPT>top.window.close()</" & "SCRIPT>")

			End If
		End With
		
		lclsTab_commission = Nothing
	End If
	
	With Response
		.Write(mobjGrid.DoFormUpd(Request.QueryString.Item("Action"), "valMantAgent.aspx", Request.QueryString.Item("sCodispl"), Request.QueryString.Item("MainAction"),  , CShort(Request.QueryString.Item("Index"))))
		.Write(mobjValues.HiddenControl("sAction", Request.QueryString.Item("Action")))
	End With
End Sub

</script>
<%Response.Expires = -1

mobjValues = New eFunctions.Values
mobjValues.sCodisplPage = "MAG006"
%>




<HTML>
<HEAD>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
    <META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>
<SCRIPT>
//+ Variable para el control de versiones
    document.VssVersion="$$Revision: 2 $|$$Date: 31/10/03 17:59 $"
</SCRIPT>    
    
    <%=mobjValues.StyleSheet()%>
    <%="<SCRIPT>var nMainAction=" & Request.QueryString.Item("nMainAction") & "</SCRIPT>"%>
    <%
If Request.QueryString.Item("Type") <> "PopUp" Then
	mobjMenues = New eFunctions.Menues
	Response.Write(mobjMenues.setZone(2, "MAG006", "MAG006"))
	mobjMenues = Nothing
End If
%>
<%="<script>var nMainAction=" & Request.QueryString.Item("nMainAction") & "</script>"%>
<%="<script>var sAction='" & Request.QueryString.Item("Action") & "'</script>"%>        
   
</HEAD>
<%
If Request.QueryString.Item("Action") <> "Del" Then
	Response.Write("<BODY ONUNLOAD='closeWindows();'>")
Else
	Response.Write("<BODY>")
End If
%>
<FORM METHOD="POST" ID="FORM" NAME="frmTabCommission" ACTION="valMantAgent.aspx?mode=1">
<%
Response.Write(mobjValues.ShowWindowsName(Request.QueryString.Item("sCodispl")))
Call insDefineHeader()
If Request.QueryString.Item("Type") <> "PopUp" Then
	Call insPreMAG006()
Else
	Call insPreMAG006Upd()
End If

mobjValues = Nothing
mobjGrid = Nothing
%>
</FORM>
</BODY>
</HTML>




