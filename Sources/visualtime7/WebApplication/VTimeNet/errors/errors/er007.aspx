<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eErrors" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

Dim mobjMenu As eFunctions.Menues

Dim mobjError As eErrors.ErrorTyp

'- Objeto para el manejo del grid    
Dim mobjGrid As eFunctions.Grid


'% insDefineHeader: se definen los campos del grid
'--------------------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'--------------------------------------------------------------------------------------------
	mobjGrid.sCodisplPage = "er007"
	
	'+ Se definen las columnas del grid
	With mobjGrid.Columns
		Call .AddTextColumn(6784,"Error", "tcnErrorNum", 10, vbNullString,  ,"Es el identificativo del error.",  ,  ,  , True)
		Call .AddTextColumn(6785,"Ventana", "tctCodisp", 50, vbNullString,  ,"Código de la ventana donde se detectó el error.",  ,  ,  , True)
		Call .AddPossiblesColumn(6786,"Estado actual", "tctStat_error", "table999", eFunctions.Values.eValuesType.clngComboType, vbNullString,  ,  ,  ,  ,  , True,  ,"Estado (descripción corta) en que se encuentra el error que se desea actualizar")
		Call .AddPossiblesColumn(6787,"Procedencia", "tcnSource", "table531", eFunctions.Values.eValuesType.clngComboType, vbNullString,  ,  ,  ,  ,  , True,  ,"Identifica la procedencia (descripción corta) donde ocurre el error.")
		Call .AddPossiblesColumn(6788,"Prioridad", "tcnPriority", "table1006", eFunctions.Values.eValuesType.clngComboType, vbNullString,  ,  ,  ,  ,  , True,  ,"Indica la prioridad (descripción corta) que tenga el error.")
		Call .AddPossiblesColumn(15281,"Severidad", "cbeSeverity", "table6014", eFunctions.Values.eValuesType.clngComboType, vbNullString,  ,  ,  ,  ,  , True,  ,"Indica el grado de importancia en que debe ser corregido los errores de prioridad 1, 2, y 3. ")
		Call .AddPossiblesColumn(6790,"Módulo afectado", "tcnModule_Err", "table997", eFunctions.Values.eValuesType.clngComboType, vbNullString,  ,  ,  ,  ,  , True,  ,"Identifica el módulo (descripción corta) al cual afecta el error.")
		Call .AddPossiblesColumn(6791,"Estado a asignar", "cbeStaterr_new", "Table999", eFunctions.Values.eValuesType.clngWindowType, vbNullString,  ,  ,  ,  ,  ,  ,  ,"Indica que se cambiará el estado al seleccionado en este campo.")
		Call .AddTextColumn(6792,"Responsable", "tctUser", 12, vbNullString,  ,"Iniciales del usuario, que está actualizando el error.")
		Call .AddDateColumn(6793,"Fecha", "tcdDate", vbNullString,  ,"Fecha de actualización del error.",  ,  ,  , True)
		Call .AddTextColumn(6794,"Hora", "tcdHour", 10, vbNullString,  ,"Hora de actualización del error.",  ,  ,  , True)
		Call .AddTextColumn(6795,"Días Utilizados", "tcnDays_user", 3, vbNullString,  ,"Días utilizados / invertidos para el cambio de estado del error.",  ,  ,  , True)
		Call .AddTextColumn(6796,"Horas Utilizadas", "tctHour_user", 6, vbNullString,  ,"Horas utilizadas / invertidas para el cambio de estado del error.",  ,  ,  , True)
	End With
	
	'+ Se definen las propiedades generales del grid
	With mobjGrid
		.Codispl = "ER007"
		.Codisp = "ER007"
		.DeleteButton = False
		.AddButton = False
		.Top = 25
		.Width = 480
		.Height = 520
		.Columns("tcnErrorNum").EditRecord = True
		.Columns("tctCodisp").EditRecord = True
		.Columns("Sel").GridVisible = False
		If Request.QueryString.Item("Reload") = "1" Then
			.sReloadIndex = Request.QueryString.Item("ReloadIndex")
		End If
	End With
End Sub

'% insPreER007: Se carga el Grid con la Información
'--------------------------------------------------------------------------------------------
Private Sub insPreER007()
	'--------------------------------------------------------------------------------------------
	Dim lcolErrors As eErrors.Errors
	Dim lclsError As eErrors.ErrorTyp
	Dim lIndex As Integer
	Dim nType_mov As Object
	Dim nEndBalance As Object
	Dim nIniBalance As Object
	Dim ldtmAuxDate As Date
	
	lcolErrors = New eErrors.Errors
	lclsError = New eErrors.ErrorTyp
	
	If lcolErrors.Find_T_Errors(Session("SessionId"), Session("nUsercode")) Then
            ldtmAuxDate = System.DateTime.FromOADate(Today.ToOADate + TimeOfDay.ToOADate)
		For lIndex = 1 To lcolErrors.Count
			lclsError = lcolErrors.Item(lIndex)
			With lclsError
				mobjGrid.Columns("tcnErrorNum").DefValue = CStr(.nErrorNum)
				mobjGrid.Columns("tctCodisp").DefValue = .sCodisp & " - " & .sDescript_win
				mobjGrid.Columns("tctStat_error").DefValue = CStr(.sStat_error)
				mobjGrid.Columns("cbeStaterr_new").DefValue = CStr(.sStat_error)
				
				mobjGrid.Columns("tcnSource").DefValue = CStr(.nSource)
				mobjGrid.Columns("tcnPriority").DefValue = CStr(.nPriority)
				
				mobjGrid.Columns("cbeSeverity").DefValue = CStr(.nSeverity)
				mobjGrid.Columns("tctUser").DefValue = .sUse_assign
				mobjGrid.Columns("tcnModule_Err").DefValue = CStr(.nModule_Err)
				mobjGrid.Columns("tcdDate").DefValue = CStr(ldtmAuxDate)
				mobjGrid.Columns("tcdHour").DefValue = CStr(TimeOfDay)
				mobjGrid.Columns("tcnDays_user").DefValue = CStr(.nDays_user)
				mobjGrid.Columns("tctHour_user").DefValue = .sHour_user
			End With
			Response.Write(mobjGrid.DoRow())
		Next 
	End If
	Response.Write(mobjGrid.closeTable)
	
	lcolErrors = Nothing
	lclsError = Nothing
End Sub

'% insPreER007Upd: Permite realizar el llamado a la ventana PopUp.
'-----------------------------------------------------------------------------------------
Private Sub insPreER007Upd()
	'-----------------------------------------------------------------------------------------
	Response.Write(mobjGrid.DoFormUpd(Request.QueryString.Item("Action"), "ValErrors.aspx", Request.QueryString.Item("sCodispl"), Request.QueryString.Item("nMainAction"),  , CShort(Request.QueryString.Item("Index"))))
	
Response.Write("" & vbCrLf)
Response.Write("<SCRIPT>" & vbCrLf)
Response.Write("	//if(self.document.forms[0].tctStat_error.value==7){" & vbCrLf)
Response.Write("	//	self.document.forms[0].cbeStaterr_new.TypeList=1" & vbCrLf)
Response.Write("	//	self.document.forms[0].cbeStaterr_new.List='1,8,9,10'" & vbCrLf)
Response.Write("	//}" & vbCrLf)
Response.Write("	if(self.document.forms[0].tctStat_error.value==1){" & vbCrLf)
Response.Write("		self.document.forms[0].cbeStaterr_new.TypeList=1" & vbCrLf)
Response.Write("		self.document.forms[0].cbeStaterr_new.List='2,3,6,8,9,10'" & vbCrLf)
Response.Write("	}" & vbCrLf)
Response.Write("	if(self.document.forms[0].tctStat_error.value==2){" & vbCrLf)
Response.Write("		self.document.forms[0].cbeStaterr_new.TypeList=1" & vbCrLf)
Response.Write("		self.document.forms[0].cbeStaterr_new.List='3,6,8,9,10'" & vbCrLf)
Response.Write("	}" & vbCrLf)
Response.Write("	if(self.document.forms[0].tctStat_error.value==3){" & vbCrLf)
Response.Write("		self.document.forms[0].cbeStaterr_new.TypeList=1" & vbCrLf)
Response.Write("		self.document.forms[0].cbeStaterr_new.List='4,5,6'" & vbCrLf)
Response.Write("	}" & vbCrLf)
Response.Write("	if(self.document.forms[0].tctStat_error.value==4){" & vbCrLf)
Response.Write("		self.document.forms[0].cbeStaterr_new.TypeList=1" & vbCrLf)
Response.Write("		self.document.forms[0].cbeStaterr_new.List='11,12'" & vbCrLf)
Response.Write("	}" & vbCrLf)
Response.Write("</" & "SCRIPT>")

	
End Sub

</script>
<%Response.Expires = -1
mobjValues = New eFunctions.Values
mobjMenu = New eFunctions.Menues
mobjError = New eErrors.ErrorTyp
mobjGrid = New eFunctions.Grid

mobjValues.sCodisplPage = "er007"

%>
<%="<SCRIPT>nMainAction='" & Request.QueryString.Item("nMainAction") & "'</SCRIPT>"%>
<HTML>
<HEAD>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/ValFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/Constantes.js"></SCRIPT>
	<META NAME = "GENERATOR" CONTENT = "Microsoft Visual Studio 6.0">

<%If Request.QueryString.Item("Type") <> "PopUp" Then
	Response.Write(mobjMenu.setZone(2, "ER007", "ER007.aspx"))
End If
Response.Write(mobjValues.StyleSheet)
%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="post" ID="FORM" NAME="frmErroUpd" ACTION="valerrors.aspx?nMainAction=<%=Request.QueryString.Item("nMainAction")%>">
<%Response.Write(mobjValues.ShowWindowsName("ER007"))

Call insDefineHeader()
If Request.QueryString.Item("Type") <> "PopUp" Then
	Call insPreER007()
Else
	Call insPreER007Upd()
End If

mobjMenu = Nothing
mobjError = Nothing
mobjValues = Nothing
mobjGrid = Nothing
%>
</FORM>
</BODY>
</HTML>





