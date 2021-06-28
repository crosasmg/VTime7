<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eGeneral" %>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eClient" %>
<script language="VB" runat="Server">

Dim mobjValues As eFunctions.Values
Dim mobjMenu As eFunctions.Menues
Dim mobjGrid As eFunctions.Grid
Dim lobjErrors As eGeneral.GeneralFunction
Dim mstrAlert As String


'%insDefineHeader: Se define la estructura del grid
'------------------------------------------------------------------------
Private Function insDefineHeader() As Object
	'------------------------------------------------------------------------
	mobjGrid = New eFunctions.Grid
	
	With mobjGrid
		.sDelRecordParam = "sClient=' + marrArray[lintIndex].sClient + '&nBankext=' + marrArray[lintIndex].cbeBank + '&nCardType=' + marrArray[lintIndex].cbeCardType + '&sCredi_card=' + marrArray[lintIndex].tctCredi_card + '&sIndDirDebit=' + marrArray[lintIndex].sIndDirDebit + '"
		
		.Columns.AddHiddenColumn("sClient", Session("sClient"))
		.Columns.AddHiddenColumn("sIndDirDebit", "")
		.Columns.AddPossiblesColumn(0, GetLocalResourceObject("cbeBankColumnCaption"), "cbeBank", "Table7", eFunctions.Values.eValuesType.clngComboType,  , False,  ,  ,  ,  , Request.QueryString.Item("Action") = "Update",  , GetLocalResourceObject("cbeBankColumnToolTip"))
		.Columns.AddPossiblesColumn(0, GetLocalResourceObject("cbeCardTypeColumnCaption"), "cbeCardType", "Table183", eFunctions.Values.eValuesType.clngComboType, CStr(0), False,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbeCardTypeColumnToolTip"))
		.Columns.AddTextColumn(0, GetLocalResourceObject("tctCredi_cardColumnCaption"), "tctCredi_card", 16, vbNullString,  , GetLocalResourceObject("tctCredi_cardColumnToolTip"),  ,  ,  , Request.QueryString.Item("Action") = "Update")
		.Columns("tctCredi_card").bNumericText = True
		.Columns.AddDateColumn(0, GetLocalResourceObject("tcdCardExpirColumnCaption"), "tcdCardExpir",  ,  , GetLocalResourceObject("tcdCardExpirColumnToolTip"))
		.Columns.AddPossiblesColumn(0, GetLocalResourceObject("cbeStatusColumnCaption"), "cbeStatus", "Table26", eFunctions.Values.eValuesType.clngComboType,  , False,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbeStatusColumnToolTip"))
		.Columns("cbeStatus").TypeList = 2
		.Columns("cbeStatus").List = CStr(2)
		.nMainAction = CShort(IIf(Request.QueryString.Item("nMainAction") ="", 0, Request.QueryString.Item("nMainAction")))
		.ActionQuery = Session("bQuery")
		.Codispl = "BC016"
		.AddButton = True
		.DeleteButton = True
		.Columns("Sel").GridVisible = True
		.Columns("tctCredi_card").EditRecord = True
		.height = 300
		.Width = 450
		
		If Request.QueryString.Item("Reload") = "1" Then
			.sReloadIndex = Request.QueryString.Item("ReloadIndex")
		End If
	End With
End Function

'%InsPreBC016: Se carga el grid según la información introducida en la ventana.
'--------------------------------------------------------------------------------------------
Private Sub insPreBC016()
	'--------------------------------------------------------------------------------------------
	Dim lblnFind As Boolean
	Dim lobjCred_cards As eClient.cred_cards
	Dim lobjCred_card As Object
	Dim lstrInd As String
	
	'+ Variable para indicar si devolvió o no información la búsqueda
	lblnFind = True
	
	lobjCred_cards = New eClient.cred_cards
	
	'+ Se buscan las relaciones del cliente
	
	If lobjCred_cards.Find(Session("sClient")) Then
		If lobjCred_cards.count > 0 Then
			For	Each lobjCred_card In lobjCred_cards
				With mobjGrid
					.Columns("cbeBank").DefValue = lobjCred_card.nBankExt
					.Columns("cbeCardType").DefValue = lobjCred_card.nCard_type
					.Columns("tctCredi_card").DefValue = lobjCred_card.sCredi_card
					.Columns("tcdCardexpir").DefValue = lobjCred_card.dCardexpir
					.Columns("cbeStatus").DefValue = lobjCred_card.sStatregt
					.Columns("sIndDirDebit").DefValue = lobjCred_card.sIndDirDebit
					lstrInd = "0"
					
					If lobjCred_card.sIndDirDebit <> eRemoteDB.Constants.strnull Then
						lstrInd = "1"
					End If
					
					.Columns("Sel").OnClick = "InsChangeSel(this," & lstrInd & ");"
					
					Response.Write(.DoRow)
				End With
			Next lobjCred_card
		Else
			lblnFind = False
		End If
	Else
		lblnFind = False
	End If
	
	Response.Write(mobjGrid.closeTable)
	
	lobjCred_cards = Nothing
	lobjCred_card = Nothing
End Sub

'%InsPreBC016Upd: Se encarga de realizar las acciones de la ventana PopUp
'------------------------------------------------------------------------
Private Function InsPreBC016Upd() As Object
	'------------------------------------------------------------------------
	Dim lclsCred_card As eClient.cred_card
	
	With Request
		If .QueryString.Item("Action") = "Del" Then
			If .QueryString.Item("sIndDirDebit") = CStr(eRemoteDB.Constants.strnull) Then
				Response.Write(mobjValues.ConfirmDelete())
				
				lclsCred_card = New eClient.cred_card
				lclsCred_card.InsPostBC016(eFunctions.Menues.TypeActions.clngActionCut, .QueryString.Item("sClient"), mobjValues.StringToType(.QueryString.Item("nBankext"), eFunctions.Values.eTypeData.etdDouble), eRemoteDB.Constants.intNull, .QueryString.Item("sCredi_card"), eRemoteDB.Constants.dtmNull, CStr(eRemoteDB.Constants.strnull), eRemoteDB.Constants.intNull)
				lclsCred_card = Nothing
			End If
		End If
	End With
	
	Response.Write(mobjGrid.DoFormUpd(Request.QueryString.Item("Action"), "ValClientSeq.aspx", "BC016", Request.QueryString.Item("nMainAction"), mobjValues.ActionQuery, CShort(Request.QueryString.Item("Index"))))
End Function

</script>
<%
Response.Expires = -1

lobjErrors = New eGeneral.GeneralFunction

mstrAlert = "Err. 2814 " & lobjErrors.insLoadMessage(2814)

lobjErrors = Nothing

With Server
	mobjValues = New eFunctions.Values
	mobjMenu = New eFunctions.Menues
End With

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
<%="<SCRIPT>"%>
    var nMainAction = <%=Request.QueryString.Item("nMainAction")%>;
<%="</SCRIPT>"%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">      
<%If Request.QueryString.Item("Type") <> "PopUp" Then
	Response.Write(mobjMenu.setZone(2, "BC016", "BC016.aspx"))
End If

mobjMenu = Nothing

Response.Write(mobjValues.StyleSheet())
Response.Write(mobjValues.ShowWindowsName(Request.QueryString.Item("sCodispl")))
%>
<FORM METHOD="POST" NAME="frmBC016" ACTION="valClientSeq.aspx?nMainAction=<%=Request.QueryString.Item("nMainAction")%>">
<%
insDefineHeader()

If Request.QueryString.Item("Type") <> "PopUp" Then
	insPreBC016()
Else
	InsPreBC016Upd()
End If

mobjGrid = Nothing
%>
	</TABLE>
</FORM>
<%="<SCRIPT>"%>
//%InsChangeSel:  Se envia mensaje de validación al intentar eliminar información que no es posible.
//----------------------------------------------------------------------------
function InsChangeSel(Field, sIndDirDebit){
//----------------------------------------------------------------------------
	if (Field.checked && sIndDirDebit == "1") {
		alert('<%=mstrAlert%>');
		Field.checked = false
	}
}
<%="</SCRIPT>"%>
</BODY>
</HTML>




