<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="ePolicy" %>
<%@ Import namespace="eBranches" %>
<%@ Import namespace="eProduct" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values
Dim mobjGrid As eFunctions.Grid
Dim mobjMenu As eFunctions.Menues
Dim mstrTypeFind As String
Dim mblnVisible As Boolean
Dim mblnDisabled As Object


'% insDefineHeader: Se definen los campos del grid
'--------------------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'--------------------------------------------------------------------------------------------
	'+ Se definen las columnas del grid
	With mobjGrid.Columns
		Call .AddTextColumn(0, GetLocalResourceObject("tctOriginColumnCaption"), "tctOrigin", 30, "",  , GetLocalResourceObject("tctOriginColumnToolTip"),  ,  ,  , True)
		Call .AddTextColumn(100196, GetLocalResourceObject("tctDescriptColumnCaption"), "tctDescript", 30, "",  , GetLocalResourceObject("tctDescriptColumnToolTip"),  ,  ,  , True)
		Call .AddHiddenColumn("tcnOrigin", CStr(0))
		Call .AddHiddenColumn("tcnFunds", CStr(0))
		Call .AddHiddenColumn("tcnExist", CStr(0))
		Call .AddHiddenColumn("tcnExist2", CStr(0))
		Call .AddNumericColumn(100193, GetLocalResourceObject("tcnPartic_minColumnCaption"), "tcnPartic_min", 5, CStr(eRemoteDB.Constants.strnull),  , GetLocalResourceObject("tcnPartic_minColumnToolTip"), True, 2)
		Call .AddNumericColumn(100193, GetLocalResourceObject("tcnParticipColumnCaption"), "tcnParticip", 5, CStr(eRemoteDB.Constants.strnull),  , GetLocalResourceObject("tcnParticipColumnToolTip"), True, 2)
		Call .AddNumericColumn(100194, GetLocalResourceObject("tcnBuy_costColumnCaption"), "tcnBuy_cost", 5, CStr(eRemoteDB.Constants.strnull),  , GetLocalResourceObject("tcnBuy_costColumnToolTip"), True, 2)
		Call .AddNumericColumn(100195, GetLocalResourceObject("tcnSell_costColumnCaption"), "tcnSell_cost", 5, CStr(eRemoteDB.Constants.strnull),  , GetLocalResourceObject("tcnSell_costColumnToolTip"), True, 2)
		Call .AddNumericColumn(100196, GetLocalResourceObject("tcnIntProyColumnCaption"), "tcnIntProy", 5, CStr(eRemoteDB.Constants.strnull),  , GetLocalResourceObject("tcnIntProyColumnToolTip"), True, 2)
		Call .AddNumericColumn(100196, GetLocalResourceObject("tcnIntProyVarMaxColumnCaption"), "tcnIntProyVarMax", 5, CStr(eRemoteDB.Constants.strnull),  , GetLocalResourceObject("tcnIntProyVarMaxColumnToolTip"), True, 2)
		Call .AddNumericColumn(100196, GetLocalResourceObject("tcnIntProyVarCleColumnCaption"), "tcnIntProyVarCle", 5, CStr(eRemoteDB.Constants.strnull),  , GetLocalResourceObject("tcnIntProyVarCleColumnToolTip"), True, 2)
		Call .AddCheckColumn(0, GetLocalResourceObject("chkVigenColumnCaption"), "chkVigen", "",  ,  ,  , request.QueryString.Item("Type") <> "PopUp", GetLocalResourceObject("chkVigenColumnToolTip"))
		Call .AddHiddenColumn("sAuxSel", CStr(2))
	End With
	'+ Se definen las propiedades generales del grid
	With mobjGrid
		.Codispl = "DP044"
		.Width = 400
		.Height = 400
		.DeleteButton = False
		.AddButton = False
		.ActionQuery = mobjValues.ActionQuery
		
		If session("bQuery") Then
			.Columns("Sel").GridVisible = True
			.bOnlyForQuery = True
		End If
		If request.QueryString.Item("Reload") = "1" Then
			.sReloadIndex = request.QueryString.Item("ReloadIndex")
		End If
		
		.Splits_Renamed.AddSplit(0, "", 6)
		.Splits_Renamed.AddSplit(0, GetLocalResourceObject("3ColumnCaption"), 3)
		
		.Columns("Sel").OnClick = "insSelected(this);"
		.DeleteScriptName = vbNullString
	End With
End Sub
'% insPreDP044: Se cargan los controles de la página
'--------------------------------------------------------------------------------------------
Private Sub insPreDP044()
	'--------------------------------------------------------------------------------------------
	Dim lclsFund_inv As ePolicy.Fund_inv
	Dim lcolFund_inv As ePolicy.Fund_invs
	Dim lcolFund As ePolicy.Fundss
	Dim lclsTab_ord_origins As eBranches.Tab_Ord_Origins
	Dim lclsTab_ord_origin As eBranches.Tab_Ord_Origin
	lclsFund_inv = New ePolicy.Fund_inv
	lcolFund_inv = New ePolicy.Fund_invs
	lcolFund = New ePolicy.Fundss
	lclsTab_ord_origin = New eBranches.Tab_Ord_Origin
	lclsTab_ord_origins = New eBranches.Tab_Ord_Origins
	
	
Response.Write("" & vbCrLf)
Response.Write("<SCRIPT LANGUAGE=""jAVASCRIPT"">" & vbCrLf)
Response.Write("//- insSelected : Determina la acción a seguir" & vbCrLf)
Response.Write("//---------------------------------------------------------------------------" & vbCrLf)
Response.Write("function insSelected(Field){" & vbCrLf)
Response.Write("//---------------------------------------------------------------------------" & vbCrLf)
Response.Write("	if(Field.checked)" & vbCrLf)
Response.Write("		EditRecord(Field.value,nMainAction, 'Update')" & vbCrLf)
Response.Write("    else{" & vbCrLf)
Response.Write("        EditRecord(Field.value,nMainAction, 'Del',""nFunds="" + marrArray[Field.value].tcnFunds + ""&nBuy_cost="" + marrArray[Field.value].tcnBuy_cost + ""&nPartic_min="" + marrArray[Field.value].tcnPartic_min + ""&nParticip="" + marrArray[Field.value].tcnParticip + ""&nSell_cost="" + marrArray[Field.value].tcnSell_cost + ""&nOrigin="" + marrArray[Field.value].tcnOrigin)" & vbCrLf)
Response.Write("    }" & vbCrLf)
Response.Write("    Field.checked = !Field.checked" & vbCrLf)
Response.Write("}" & vbCrLf)
Response.Write("</" & "SCRIPT>")

	
	Dim lnIntProy As String
	Dim lnIntProyVar As Object
	
	'+ Se busca la cantidad de fondos permitidos
	If lcolFund_inv.Find(True) Then
		Call lcolFund.Find(mobjValues.StringToType(session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToDate(session("dEffecdate")))
		session("nCountReg") = 0
		session("nTotparticip") = 0
		session("nTotCtas") = 0
		If lclsTab_ord_origins.Find(mobjValues.StringToType(session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(session("nProduct"), eFunctions.Values.eTypeData.etdDouble)) Then
			
			For	Each lclsTab_ord_origin In lclsTab_ord_origins
				session("nTotCtas") = session("nTotCtas") + 1
				'+ Se recorre los fondos de inversión asociados a un producto
				For	Each lclsFund_inv In lcolFund_inv
					With mobjGrid
						.Columns("tctOrigin").DefValue = lclsTab_ord_origin.sDescript
						.Columns("tctOrigin").EditRecord = True
						.Columns("tcnOrigin").DefValue = CStr(lclsTab_ord_origin.nOrigin)
						.Columns("tctDescript").DefValue = lclsFund_inv.sDescript
						.Columns("tctDescript").Disabled = True
						.Columns("tcnFunds").DefValue = CStr(lclsFund_inv.nFunds)
						.Columns("tctDescript").EditRecord = True
						'+	Se verifica que esos fondos se encuentren en la tabla funds y se muestran sus respectivos valores
						If lcolFund.FindItem(lclsFund_inv.nFunds, lclsTab_ord_origin.nOrigin, mobjValues.StringToType(lnIntProy, eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(lnIntProy, eFunctions.Values.eTypeData.etdDouble)) Then
							session("nCountReg") = session("nCountReg") + 1
							.Columns("sel").Checked = 1
							.Columns("tcnExist").DefValue = CStr(1)
							.Columns("tcnExist2").DefValue = CStr(0)
							.Columns("tcnPartic_min").DefValue = CStr(lcolFund.CurrentFunds.nPartic_min)
							.Columns("tcnParticip").DefValue = CStr(lcolFund.CurrentFunds.nParticip)
							.Columns("tcnBuy_cost").DefValue = CStr(lcolFund.CurrentFunds.nBuy_cost)
							.Columns("tcnSell_cost").DefValue = CStr(lcolFund.CurrentFunds.nSell_cost)
							.Columns("tcnIntProy").DefValue = CStr(lcolFund.CurrentFunds.nIntProy)
							.Columns("tcnIntProyVarMax").DefValue = CStr(lcolFund.CurrentFunds.nIntProyVarMax)
							.Columns("tcnIntProyVarCle").DefValue = CStr(lcolFund.CurrentFunds.nIntProyVarCle)
							.Columns("chkVigen").Checked = CShort(lcolFund.CurrentFunds.sVigen)
							session("nTotparticip") = session("nTotparticip") + lcolFund.CurrentFunds.nParticip
						Else
							'+ Si no esta en la tabla funds se muestran los campos en blanco				
							.Columns("sel").Checked = 0
							.Columns("tcnExist2").DefValue = CStr(1)
							.Columns("tcnExist").DefValue = CStr(0)
							.Columns("tcnPartic_min").DefValue = "0,00"
							.Columns("tcnParticip").DefValue = "0,00"
							.Columns("tcnBuy_cost").DefValue = "0,00"
							.Columns("tcnSell_cost").DefValue = "0,00"
							.Columns("tcnIntProy").DefValue = "0,00"
							.Columns("tcnIntProyVarMax").DefValue = "0,00"
							.Columns("tcnIntProyVarCle").DefValue = "0,00"
							.Columns("chkVigen").Checked = CShort("2")
						End If
						Response.Write(.DoRow)
					End With
				Next lclsFund_inv
			Next lclsTab_ord_origin
		End If
	Else
		mblnVisible = True
	End If
	Response.Write(mobjGrid.closeTable())
	lclsFund_inv = Nothing
	lcolFund_inv = Nothing
	lclsTab_ord_origins = Nothing
End Sub
'% insPreDP044Upd: Se muetra la ventana Popup para efecto de actualización del Gird
'--------------------------------------------------------------------------------------------
Private Sub insPreDP044Upd()
	'--------------------------------------------------------------------------------------------
	Dim lclsFund_inv As Object
	Dim nAction As Object
	Dim lclsProdLifeSeq As eProduct.ProdLifeSeq
	lclsProdLifeSeq = New eProduct.ProdLifeSeq
	'+ Se elimina el registro marcado
	If request.QueryString.Item("Action") = "Del" Then
		Response.Write(mobjValues.ConfirmDelete)
		Call lclsProdLifeSeq.insPostDP044(request.QueryString.Item("Action"), mobjValues.StringToType(CStr(0), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToDate(session("dEffecdate")), mobjValues.StringToType(session("nUsercode"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(request.QueryString.Item("nFunds"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(request.QueryString.Item("nBuy_cost"), eFunctions.Values.eTypeData.etdDouble), eRemoteDB.Constants.dtmNull, mobjValues.StringToType(request.QueryString.Item("nPartic_min"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(request.QueryString.Item("nParticip"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(request.QueryString.Item("nSell_cost"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(request.QueryString.Item("nOrigin"), eFunctions.Values.eTypeData.etdDouble), eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull)
		session("nCountReg") = session("nCountReg") - 1
	End If
	'+ Se muestra la ventana PopUp para modificar o actualizar
	Response.Write(mobjGrid.DoFormUpd(request.QueryString.Item("Action"), "valprodlifeseq.aspx", "DP044", request.QueryString.Item("nMainAction"), mobjValues.ActionQuery, CShort(request.QueryString.Item("Index"))))
	lclsProdLifeSeq = Nothing
End Sub

</script>
<%Response.Expires = -1
mobjValues = New eFunctions.Values
mobjGrid = New eFunctions.Grid
mobjMenu = New eFunctions.Menues
If IsNothing(request.QueryString.Item("sTypeFind")) Then
	mstrTypeFind = "1"
Else
	mstrTypeFind = "2"
End If
mobjValues.ActionQuery = session("bQuery")

mobjValues.sCodisplPage = "dp044"
mobjGrid.sCodisplPage = "dp044"
%>
<SCRIPT>
//- Variable para el control de versiones
       document.VssVersion="$$Revision: 3 $|$$Date: 25/08/09 3:58p $"

</SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<HTML>
<HEAD>
    <META NAME = "GENERATOR" CONTENT = "Microsoft Visual Studio 6.0">




    <%
With Response
	.Write(mobjValues.StyleSheet())
	If request.QueryString.Item("Type") <> "PopUp" Then
		.Write("<SCRIPT>var nMainAction=304</SCRIPT>")
		.Write(mobjMenu.setZone(2, "DP044", "DP044.aspx"))
		mobjMenu = Nothing
	End If
End With

%></HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="post" ID="FORM" NAME="DP044" ACTION="valProdLifeSeq.aspx?nMainAction=<%=request.QueryString.Item("nMainAction")%>">
<%
With Response
	.Write(mobjValues.ShowWindowsName("DP044"))
	.Write("<BR>")
End With
Call insDefineHeader()
If request.QueryString.Item("Type") = "PopUp" Then
	Call insPreDP044Upd()
Else
	Call insPreDP044()
End If
%>
</FORM>
</BODY>
</HTML>
<%
mobjValues = Nothing
mobjGrid = Nothing
%>




