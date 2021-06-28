<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eClient" %>
<script language="VB" runat="Server">
'^Begin Header Block VisualTimer Utility 1.1 31/3/03 17.17.03
Dim mobjNetFrameWork As eNetFrameWork.Layout
'~End Header Block VisualTimer Utility

'- Objeto para el manejo de las funciones generales de carga de valores

Dim mobjValues As eFunctions.Values
Dim mobjMenu As eFunctions.Menues
Dim mcolFinanc_clis As eClient.Financ_Clis

'- Objeto para el manejo del grid
Dim mobjGrid As eFunctions.Grid


'% insDefineHeader: se definen los campos del grid
'--------------------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'--------------------------------------------------------------------------------------------
	Dim lblnFinanCli As Boolean
	
	mobjGrid = New eFunctions.Grid
	'^Begin Body Block VisualTimer Utility 1.1 31/3/03 17.17.03
	mobjGrid.sSessionID = Session.SessionID
	mobjGrid.nUsercode = Session("nUsercode")
	'~End Body Block VisualTimer Utility
	
	mobjGrid.sCodisplPage = Request.QueryString.Item("sCodispl")
	Call mobjGrid.SetWindowParameters(Request.QueryString.Item("sCodispl"), Request.QueryString.Item("sWindowDescript"), Request.QueryString.Item("nWindowTy"))
	
	'+ Se verifica si existen datos a mostrar
	If CStr(Session("sOriginalForm")) <> "" Then
		mcolFinanc_clis = New eClient.Financ_Clis
		If mcolFinanc_clis.Find(Session("sClient")) Then
			If mcolFinanc_clis.Count > 0 Then
				lblnFinanCli = True
			Else
				lblnFinanCli = False
			End If
		Else
			lblnFinanCli = False
		End If
		mcolFinanc_clis = Nothing
	Else
		lblnFinanCli = False
	End If
	
	'+ Se definen las columnas del grid    
	With mobjGrid.Columns
		Call .AddDateColumn(40391, GetLocalResourceObject("tcdFinanDateColumnCaption"), "tcdFinanDate", "",  , GetLocalResourceObject("tcdFinanDateColumnToolTip"),  ,  ,  , Request.QueryString.Item("Action") = "Update")
		Call .AddPossiblesColumn(40386, GetLocalResourceObject("cbeConceptColumnCaption"), "cbeConcept", "Table416", eFunctions.Values.eValuesType.clngComboType, CStr(0),  ,  ,  ,  , "LockControl(this.value)", Request.QueryString.Item("Action") = "Update",  , GetLocalResourceObject("cbeConceptColumnToolTip"))
		Call .AddButtonColumn(0, GetLocalResourceObject("SCA2-LColumnCaption"), "SCA2-L", CDbl(Request.QueryString.Item("nNoteNum")),  , Request.QueryString.Item("Type") <> "PopUp")
		Call .AddNumericColumn(40389, GetLocalResourceObject("tcnUnitsColumnCaption"), "tcnUnits", 9, "",  , GetLocalResourceObject("tcnUnitsColumnToolTip"), False)
		Call .AddPossiblesColumn(40387, GetLocalResourceObject("cbeCurrencyColumnCaption"), "cbeCurrency", "Table11", eFunctions.Values.eValuesType.clngComboType, CStr(0),  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbeCurrencyColumnToolTip"))
		Call .AddNumericColumn(40390, GetLocalResourceObject("tcnAmountColumnCaption"), "tcnAmount", 18, "",  , GetLocalResourceObject("tcnAmountColumnToolTip"), True, 6)
		Call .AddPossiblesColumn(40388, GetLocalResourceObject("cbeFinanStatColumnCaption"), "cbeFinanStat", "Table185", eFunctions.Values.eValuesType.clngComboType, CStr(0),  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbeFinanStatColumnToolTip"))
	End With
	
	'+ Se definen las propiedades generales del grid
	With mobjGrid
		.Codispl = "BC014"
		.ActionQuery = mobjValues.ActionQuery
		.Columns("cbeConcept").EditRecord = True
		.Height = 320
		.Width = 400
		.Top = 200
		.nMainAction = CShort(IIf(Request.QueryString.Item("nMainAction") ="", 0, Request.QueryString.Item("nMainAction")))
		.Columns("Sel").GridVisible = Not .ActionQuery
		.sDelRecordParam = "dFinanDate='+ marrArray[lintIndex].tcdFinanDate + '&nConcept='+ marrArray[lintIndex].cbeConcept + '"
		'+ Si la variable de sesión "sOriginalForm" es distinta de blanco (la secuencia de clientes fue
		'+ invocada desde la CA025 en el módulo de Cartera) y existen registros en el Grid
		'+ (lblnFinanCli = True), entonces se esconde el botón de agregar y se inhabilita el Grid - ACM - 07/08/2001
		If CStr(Session("sOriginalForm")) <> vbNullString And lblnFinanCli Then
			.AddButton = False
			.DeleteButton = False
			.ActionQuery = True
		End If
		If Request.QueryString.Item("Reload") = "1" Then
			.sReloadIndex = Request.QueryString.Item("ReloadIndex")
		End If
		'+ Se dejan controles deshabilitados segun el concepto cada vez que cambia el registro
		.MoveRecordScript = "LockControl(self.document.forms[0].cbeConcept.value);"
	End With
	
End Sub

'% insPreBC014: Lee los valores de la tabla Financ_cli.
'%              Los valores corresponden a un cliente específico
'--------------------------------------------------------------------------------------------
Private Sub insPreBC014()
	'--------------------------------------------------------------------------------------------
	Dim lintIndex As Integer
	
	mcolFinanc_clis = New eClient.Financ_Clis
	
	If mcolFinanc_clis.Find(Session("sClient")) Then
		If mcolFinanc_clis.Count > 0 Then
                   For lintIndex = 1 To mcolFinanc_clis.Count
                        With mcolFinanc_clis(lintIndex)
                            mobjGrid.Columns("tcdFinanDate").DefValue = CStr(.dFinanDate)
                            mobjGrid.Columns("cbeConcept").DefValue = CStr(.nConcept)
                            mobjGrid.Columns("btnNotenum").nNotenum = .nNotenum
                            mobjGrid.Columns("tcdFinanDate").DefValue = CStr(.dFinanDate)
                            mobjGrid.Columns("tcnUnits").DefValue = CStr(.nUnits)
                            mobjGrid.Columns("cbeCurrency").DefValue = CStr(.nCurrency)
                            mobjGrid.Columns("tcnAmount").DefValue = CStr(.nAmount)
                            mobjGrid.Columns("cbeFinanStat").DefValue = CStr(.nFinanStat)
                        End With
                        Response.Write(mobjGrid.DoRow())
                    Next
		End If
	End If
	
	Response.Write(mobjGrid.closeTable())
	mcolFinanc_clis = Nothing
	
End Sub

'% insPreBC014Upd: Se realiza el manejo de los campos del grid 
'--------------------------------------------------------------------------------------------
Private Sub insPreBC014Upd()
	'--------------------------------------------------------------------------------------------
	
Response.Write("" & vbCrLf)
Response.Write("<SCRIPT>" & vbCrLf)
Response.Write("// LockControl: Habilita/Deshabilita los controles excluyentes de la página" & vbCrLf)
Response.Write("//-------------------------------------------------------------------------------------------" & vbCrLf)
Response.Write("function LockControl(intVal){" & vbCrLf)
Response.Write("//-------------------------------------------------------------------------------------------" & vbCrLf)
Response.Write("" & vbCrLf)
Response.Write("	with(document.frmBC014){" & vbCrLf)
Response.Write("/*	" & vbCrLf)
Response.Write("	intVal:" & vbCrLf)
Response.Write("		 ""1"": //Limite de crédito" & vbCrLf)
Response.Write("		 ""2"": //Volumen de pólizas anuales" & vbCrLf)
Response.Write("		 ""3"": //Total prima anual" & vbCrLf)
Response.Write("		 ""4"": //Capital" & vbCrLf)
Response.Write("		 ""5"": //Cantidad de empleados " & vbCrLf)
Response.Write("*/" & vbCrLf)
Response.Write("		cbeCurrency.disabled=(intVal=='2')||(intVal=='5');" & vbCrLf)
Response.Write("		tcnAmount.disabled=(intVal=='2')||(intVal=='5');" & vbCrLf)
Response.Write("		tcnUnits.disabled=(intVal=='1')||(intVal=='3')||(intVal=='4');" & vbCrLf)
Response.Write("		if(cbeCurrency.disabled) cbeCurrency.value=0;" & vbCrLf)
Response.Write("		if(tcnAmount.disabled) tcnAmount.value=0;" & vbCrLf)
Response.Write("		if(tcnUnits.disabled) tcnUnits.value=0;" & vbCrLf)
Response.Write("	}" & vbCrLf)
Response.Write("}" & vbCrLf)
Response.Write("</" & "SCRIPT>")

	
	Dim lobjClientSeq As eClient.ClientSeq
	
	lobjClientSeq = New eClient.ClientSeq
	
	With Request
		If Request.QueryString.Item("Action") = "Del" Then
			Response.Write(mobjValues.ConfirmDelete())
			If lobjClientSeq.insPostBC014("Delete", Session("sClient"), mobjValues.stringtotype(.QueryString.Item("dFinanDate"), eFunctions.Values.eTypeData.etdDate), mobjValues.stringtotype(.QueryString.Item("nConcept"), eFunctions.Values.eTypeData.etdDouble)) Then
				Response.Write("<SCRIPT>top.opener.top.frames['fraSequence'].document.location='/VTimeNet/Client/ClientSeq/Sequence.aspx?nAction=" & Request.QueryString.Item("nMainAction") & "&nOpener=" & Request.QueryString.Item("sCodispl") & "&sGoToNext=NO" & "';</" & "Script>")
			End If
		End If
		Response.Write(mobjGrid.DoFormUpd(.QueryString.Item("Action"), "ValClientSeq.aspx", "BC014", .QueryString.Item("nMainAction"), mobjValues.ActionQuery, CShort(.QueryString.Item("Index"))))
		'+ Se dejan controles deshabilitados segun el concepto
		If Request.QueryString.Item("Action") <> "Del" Then
			Response.Write("<SCRIPT>LockControl(self.document.forms[0].cbeConcept.value)</" & "Script>")
		End If
		If Request.QueryString.Item("Action") = "Update" Then
			Response.Write("<SCRIPT>self.document.forms[0].tcnNotenum.value = top.opener.marrArray[CurrentIndex].btnNotenum</" & "Script>")
		End If
	End With
	lobjClientSeq = Nothing
	
End Sub

</script>
<%Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage("bc014")

mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 31/3/03 17.17.03
mobjValues.sSessionID = Session.SessionID
mobjValues.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjValues.sCodisplPage = "bc014"
mobjMenu = New eFunctions.Menues
'^Begin Body Block VisualTimer Utility 1.1 31/3/03 17.17.03
mobjMenu.sSessionID = Session.SessionID
mobjMenu.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjValues.ActionQuery = CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 401
%>
<HTML>
<HEAD>


<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<%
Response.Write(mobjValues.StyleSheet())

If Request.QueryString.Item("Type") <> "PopUp" Then
	Response.Write(mobjMenu.setZone(2, "BC014", Request.QueryString.Item("sWindowDescript"), CShort(Request.QueryString.Item("nWindowTy"))))
	mobjMenu = Nothing
	Response.Write("<SCRIPT>var nMainAction=top.frames['fraSequence'].plngMainAction</SCRIPT>")
End If

%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" NAME="frmBC014" ACTION="valClientSeq.aspx?sMode=2&nMainAction=<%=Request.QueryString.Item("nMainAction")%>">
    <%Response.Write(mobjValues.ShowWindowsName("BC014", Request.QueryString.Item("sWindowDescript")))

Call insDefineHeader()
If Request.QueryString.Item("Type") = "PopUp" Then
	Call insPreBC014Upd()
Else
	Call insPreBC014()
End If
%>
</FORM> 
</BODY>
</HTML>
<%

mobjValues = Nothing
mobjGrid = Nothing
%>
<%'^Begin Footer Block VisualTimer Utility 1.1 31/3/03 17.17.03
Call mobjNetFrameWork.FinishPage("bc014")
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>




