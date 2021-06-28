<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eGeneral" %>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eClient" %>
<script language="VB" runat="Server">
Dim mobjValues As eFunctions.Values
Dim mobjMenu As eFunctions.Menues
Dim mobjGrid As eFunctions.Grid
Dim mobjColumn As eFunctions.Values
Dim lobjErrors As eGeneral.GeneralFunction
Dim mstrAlert As String


'%insDefineHeader: Se define la estructura del grid
'------------------------------------------------------------------------
Private Function insDefineHeader() As Object
	'------------------------------------------------------------------------
	
	mobjGrid = New eFunctions.Grid
	mobjColumn = New eFunctions.Values
	
	
	With mobjGrid
		.sDelRecordParam = "sClient=' + marrArray[lintIndex].sClient + '&nBankext=' + marrArray[lintIndex].cbeBankext + '&sAccount=' + marrArray[lintIndex].tctAccount + '&sIndDirDebit=' + marrArray[lintIndex].sIndDirDebit + '&nBank=' + marrArray[lintIndex].nBank + '"
		.Columns.AddHiddenColumn("sClient", Session("sClient"))
		.Columns.AddHiddenColumn("sIndDirDebit", "")
		.Columns.AddHiddenColumn("nBank", "")
		.Columns.AddPossiblesColumn(0, GetLocalResourceObject("cbeBankextColumnCaption"), "cbeBankext", "Table7", 1,  ,  ,  ,  ,  ,  , Request.QueryString.Item("Action") = "Update",  , GetLocalResourceObject("cbeBankextColumnToolTip"))
		.Columns.AddTextColumn(0, GetLocalResourceObject("tctAccountColumnCaption"), "tctAccount", 25, "",  , GetLocalResourceObject("tctAccountColumnToolTip"),  ,  ,  , Request.QueryString.Item("Action") = "Update")
		.Columns("tctAccount").bNumericText = True
		.Columns.AddPossiblesColumn(0, GetLocalResourceObject("tcnTyp_accColumnCaption"), "tcnTyp_acc", "table190", eFunctions.Values.eValuesType.clngComboType, "",  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("tcnTyp_accColumnToolTip"))
		.Columns.AddPossiblesColumn(0, GetLocalResourceObject("cbeStatRegtColumnCaption"), "cbeStatRegt", "Table26", 1,  ,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbeStatRegtColumnToolTip"))
		.Columns.AddCheckColumn(0, GetLocalResourceObject("chkDepositColumnCaption"), "chkDeposit", " ")
		.Columns("cbeStatRegt").TypeList = 2
		.Columns("cbeStatRegt").List = CStr(2)
		.Columns("tcnTyp_acc").TypeList = 2
		.Columns("tcnTyp_acc").List = "8,9"
		If Request.QueryString.Item("Type") <> "PopUp" Then
			.Columns("chkDeposit").Disabled = True
		End If
		.nMainAction = CShort(IIf(Request.QueryString.Item("nMainAction") ="", 0, Request.QueryString.Item("nMainAction")))
		.ActionQuery = Session("bQuery")
		.Codispl = "BC013"
		.AddButton = True
		.DeleteButton = True
		.Columns("Sel").GridVisible = True
		.Columns("tctAccount").EditRecord = True
		.Height = 380
		.Width = 380
		
		If Request.QueryString.Item("Reload") = "1" Then
			.sReloadIndex = Request.QueryString.Item("ReloadIndex")
		End If
	End With
End Function

'%InsPreBC013: Se carga el contenido del grid según la información ingresada en la ventana.
'--------------------------------------------------------------------------------------------
Private Sub insPreBC013()
	'--------------------------------------------------------------------------------------------
	Dim lblnFind As Boolean
	Dim lobjBk_accounts As eClient.bk_accounts
	Dim lobjBk_account As Object
	Dim lstrInd As String
	Dim lintcount As Short
	'+ Variable para indicar si devolvió o no información la búsqueda
	lblnFind = True
	lobjBk_accounts = New eClient.bk_accounts
	'+ Se buscan las relaciones del cliente
	lintcount = 0
	If lobjBk_accounts.Find(Session("sClient")) Then
		If lobjBk_accounts.count > 0 Then
			For	Each lobjBk_account In lobjBk_accounts
				With mobjGrid
					If lobjBk_account.sDeposit = eRemoteDB.Constants.strnull Then
						lobjBk_account.sDeposit = "2"
					End If
					.Columns("cbeBankext").DefValue = lobjBk_account.nBankExt
					.Columns("tctAccount").DefValue = lobjBk_account.sAccount
					.Columns("tcnTyp_acc").DefValue = lobjBk_account.nTyp_acc
					.Columns("cbeStatRegt").DefValue = lobjBk_account.sStatregt
					.Columns("sIndDirDebit").DefValue = lobjBk_account.sIndDirDebit
					.Columns("nBank").DefValue = lobjBk_account.nBankExt
					.Columns("chkDeposit").Checked = lobjBk_account.sDeposit
					lstrInd = "0"
					If lobjBk_account.sIndDirDebit <> eRemoteDB.Constants.strnull Then
						lstrInd = "1"
					End If
					.Columns("Sel").OnClick = "InsChangeSel(this," & lstrInd & ");"
					.sEditRecordParam = "nbank=' + marrArray[" & CStr(lintcount) & "].cbeBankext + '"
					Response.Write(.DoRow)
					lintcount = lintcount + 1
				End With
			Next lobjBk_account
		Else
			lblnFind = False
		End If
	Else
		lblnFind = False
	End If
	Response.Write(mobjGrid.closeTable)
	
	lobjBk_accounts = Nothing
	lobjBk_account = Nothing
End Sub

'%InsPreBC013Upd: Se efectúan las acciones de la ventana PopUp
'------------------------------------------------------------------------
Private Function InsPreBC013Upd() As Object
	'------------------------------------------------------------------------
	Dim lclsBk_account As eClient.bk_account
	With Request
		If .QueryString.Item("Action") = "Del" Then
			If .QueryString.Item("sIndDirDebit") = CStr(eRemoteDB.Constants.strnull) Then
				Response.Write(mobjValues.ConfirmDelete())
				lclsBk_account = New eClient.bk_account
				If lclsBk_account.InsPostBC013Upd(eFunctions.Menues.TypeActions.clngActionCut, .QueryString.Item("sClient"), mobjValues.StringToType(.QueryString.Item("nBankext"), eFunctions.Values.eTypeData.etdDouble), .QueryString.Item("sAccount"), CStr(eRemoteDB.Constants.strnull), eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, CStr(eRemoteDB.Constants.strnull)) Then
					Response.Write("<SCRIPT>top.opener.top.frames['fraSequence'].document.location='/VTimeNet/Client/ClientSeq/Sequence.aspx?nAction=" & Request.QueryString.Item("nMainAction") & "&nOpener=" & Request.QueryString.Item("sCodispl") & "&sGoToNext=NO" & "';</" & "Script>")
				End If
				lclsBk_account = Nothing
			End If
		End If
	End With
	Response.Write(mobjGrid.DoFormUpd(Request.QueryString.Item("Action"), "ValClientSeq.aspx", "BC013", Request.QueryString.Item("nMainAction"), mobjValues.ActionQuery, CShort(Request.QueryString.Item("Index"))))
End Function

</script>
<%Response.Expires = 0
lobjErrors = New eGeneral.GeneralFunction
mstrAlert = "Err. 2814 " & lobjErrors.insLoadMessage(2814)
lobjErrors = Nothing
mobjValues = New eFunctions.Values
mobjMenu = New eFunctions.Menues
If CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 401 Then
	mobjValues.ActionQuery = True
End If
%> 
<HTML>
<HEAD>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>    
    <META NAME = "GENERATOR" CONTENT = "Microsoft Visual Studio 6.0">




<SCRIPT>
//+ Variable para el control de versiones
	document.VssVersion="$$Revision: 2 $|$$Date: 15/10/03 15.57 $"
</SCRIPT>
<SCRIPT LANGUAGE=""JavaScript"">
    var nMainAction = <%=Request.QueryString.Item("nMainAction")%>;

//InsChangeSel: Función encargada de enviar mensaje de validación para cuando no se pueda eliminar un registro del grid
//------------------------------------------------------------------------
function InsChangeSel(Field, sIndDirDebit){
//------------------------------------------------------------------------
	if (Field.checked && sIndDirDebit == "1") {
		alert('<%=mstrAlert%>');
		Field.checked = false
	}
}
</SCRIPT>
</HEAD>
<BODY ONUNLOAD="closeWindows();">      
<%If Request.QueryString.Item("Type") <> "PopUp" Then
	Response.Write(mobjMenu.setZone(2, "BC013", "BC013.aspx"))
End If
mobjMenu = Nothing
Response.Write(mobjValues.StyleSheet())
Response.Write(mobjValues.ShowWindowsName("BC013"))
%>
<FORM METHOD="POST" NAME="frmBC013" ACTION="valClientSeq.aspx?nMainAction=<%=Request.QueryString.Item("nMainAction")%>">
<%
insDefineHeader()
If Request.QueryString.Item("Type") <> "PopUp" Then
	insPreBC013()
Else
	InsPreBC013Upd()
End If
mobjGrid = Nothing
%>
	</TABLE>
</FORM>
</BODY>
</HTML>




