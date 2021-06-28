<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eClient" %>
<script language="VB" runat="Server">
'^Begin Header Block VisualTimer Utility 1.1 7/4/03 10.24.56
Dim mobjNetFrameWork As eNetFrameWork.Layout
'~End Header Block VisualTimer Utility

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values
'- Objeto para el manejo del menú de la página
Dim mobjMenu As eFunctions.Menues
'- Objeto para el manejo de la grilla 
Dim mobjGrid As eFunctions.Grid

Dim mclsEval_master As eClient.eval_master


'%insDefineHeader: Se define la estructura del grid
'------------------------------------------------------------------------
Private Function insDefineHeader() As Object
	'------------------------------------------------------------------------
	mobjGrid = New eFunctions.Grid
	'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.24.56
	mobjGrid.sSessionID = Session.SessionID
	mobjGrid.nUsercode = Session("nUsercode")
	'~End Body Block VisualTimer Utility
	
	mobjGrid.sCodisplPage = "bc803"
	Call mobjGrid.SetWindowParameters(Request.QueryString.Item("sCodispl"), Request.QueryString.Item("sWindowDescript"), Request.QueryString.Item("nWindowTy"))
	With mobjGrid
		'+ Se definen las columnas ocultas del grid
		.Columns.AddHiddenColumn("hddEval", Request.QueryString.Item("nEval"))
		.Columns.AddHiddenColumn("hddId", "")
		.Columns.AddHiddenColumn("hddCapital", mobjValues.StringToType(CStr(mclsEval_master.nCapital), eFunctions.Values.eTypeData.etdDouble))
		
		.Columns.AddPossiblesColumn(0, GetLocalResourceObject("cbeTypedocColumnCaption"), "cbeTypedoc", "Table32", eFunctions.Values.eValuesType.clngComboType, "1",  ,  ,  ,  , "ShowChangeValues(""Docreq"")",  ,  , GetLocalResourceObject("cbeTypedocColumnCaption"))
		
		.Columns("cbeTypedoc").Parameters.Add("nAction", Session("Action_Docum"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Columns("cbeTypedoc").Parameters.Add("sCertype", "2", eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Columns("cbeTypedoc").Parameters.Add("nBranch", mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdLong), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Columns("cbeTypedoc").Parameters.Add("nProduct", mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdLong), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Columns("cbeTypedoc").Parameters.Add("nPolicy", mobjValues.StringToType(Session("nPolicy"), eFunctions.Values.eTypeData.etdLong), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Columns("cbeTypedoc").Parameters.Add("nCertif", mobjValues.StringToType(Session("nCertif"), eFunctions.Values.eTypeData.etdLong), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Columns("cbeTypedoc").Parameters.Add("nCapital", mobjValues.StringToType(CStr(mclsEval_master.nCapital), eFunctions.Values.eTypeData.etdDouble), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Columns("cbeTypedoc").Parameters.Add("nCumul", mobjValues.StringToType(CStr(mclsEval_master.nCumul), eFunctions.Values.eTypeData.etdDouble), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Columns("cbeTypedoc").Parameters.Add("nCurrency", mobjValues.StringToType(CStr(mclsEval_master.nCurrency), eFunctions.Values.eTypeData.etdDouble), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		
		.Columns.AddTextColumn(0, GetLocalResourceObject("tctDescriptColumnCaption"), "tctDescript", 30, "",  , GetLocalResourceObject("tctDescriptColumnCaption"))
		.Columns.AddCheckColumn(0, GetLocalResourceObject("chkRequireColumnCaption"), "chkRequire", "",  , "2",  , True, GetLocalResourceObject("chkRequireColumnToolTip"))
		
		.Columns.AddPossiblesColumn(0, GetLocalResourceObject("cbeStatusdocColumnCaption"), "cbeStatusdoc", "Table275", eFunctions.Values.eValuesType.clngComboType, CStr(1),  ,  ,  ,  , "ShowChange()",  ,  , GetLocalResourceObject("cbeStatusdocColumnCaption"))
		.Columns.AddDateColumn(0, GetLocalResourceObject("tcdDocreqColumnCaption"), "tcdDocreq", CStr(Today),  , GetLocalResourceObject("tcdDocreqColumnCaption"))
		.Columns.AddDateColumn(0, GetLocalResourceObject("tcdDocrecColumnCaption"), "tcdDocrec", "",  , GetLocalResourceObject("tcdDocrecColumnCaption"))
		.Columns.AddDateColumn(0, GetLocalResourceObject("tcdDocdateColumnCaption"), "tcdDocdate", CStr(Today),  , GetLocalResourceObject("tcdDocdateColumnCaption"))
		.Columns.AddDateColumn(0, GetLocalResourceObject("tcdExpirdatColumnCaption"), "tcdExpirdat", "",  , GetLocalResourceObject("tcdExpirdatColumnCaption"))
		.Columns.AddDateColumn(0, GetLocalResourceObject("tcdDatetoColumnCaption"), "tcdDateto", "",  , GetLocalResourceObject("tcdDatetoColumnCaption"))
		.Columns.AddDateColumn(0, GetLocalResourceObject("tcdDatefreeColumnCaption"), "tcdDatefree", "",  , GetLocalResourceObject("tcdDatefreeColumnCaption"),  ,  ,  , True)
		.Columns.AddButtonColumn(0, GetLocalResourceObject("SCA804ColumnCaption"), "SCA804", CDbl(Request.QueryString.Item("nNoteNum")),  , Not Request.QueryString.Item("Type") = "PopUp")
		
		.nMainAction = CShort(IIf(Request.QueryString.Item("nMainAction") ="", 0, Request.QueryString.Item("nMainAction")))
		.ActionQuery = Session("bQuery")
		.Columns("cbeTypedoc").EditRecord = True
		.Columns("cbeTypedoc").BlankPosition = 0
		.Columns("cbeStatusdoc").DefValue = "1"
		.Columns("cbeStatusdoc").BlankPosition = 0
		.Codispl = "BC803"
		
		If mclsEval_master.nEval = eRemoteDB.Constants.intNull Then
			.AddButton = False
			.DeleteButton = False
		Else
			.AddButton = True
			.DeleteButton = True
		End If
		.Columns("Sel").GridVisible = True
		.Top = 50
		.Left = 200
		.height = 450
		.Width = 400
		.sEditRecordParam = "nEval=" & mclsEval_master.nEval
		
		If Request.QueryString.Item("Reload") = "1" Then
			.sReloadIndex = Request.QueryString.Item("ReloadIndex")
		End If
		.sDelRecordParam = "nEval=' + marrArray[lintIndex].hddEval + '&nId=' + marrArray[lintIndex].hddId +  '"
	End With
End Function

'%InsPreBC803: Se carga la información del grid
'--------------------------------------------------------------------------------------------
Private Sub InsPreBC803()
	'--------------------------------------------------------------------------------------------
	Dim lclsDoc_req_clis As eClient.Doc_req_clis
	Dim lclsDoc_req_cli As eClient.Doc_req_cli
	Dim lstrinderr As Byte
	
	With Server
		lclsDoc_req_clis = New eClient.Doc_req_clis
		lclsDoc_req_cli = New eClient.Doc_req_cli
	End With
	
	lstrinderr = 0
	'+ Se buscan las relaciones del cliente
	If lclsDoc_req_clis.Find(mobjValues.StringToType(CStr(mclsEval_master.nEval), eFunctions.Values.eTypeData.etdDouble)) Then
		For	Each lclsDoc_req_cli In lclsDoc_req_clis
			With mobjGrid
				.Columns("hddEval").DefValue = Request.QueryString.Item("nEval")
				.Columns("hddId").DefValue = CStr(lclsDoc_req_cli.nId)
				.Columns("cbeTypedoc").DefValue = CStr(lclsDoc_req_cli.nTypedoc)
				.Columns("tctDescript").DefValue = lclsDoc_req_cli.sDescript
				.Columns("cbeStatusdoc").DefValue = CStr(lclsDoc_req_cli.nStatusdoc)
				.Columns("tcdDocreq").DefValue = CStr(lclsDoc_req_cli.dDocreq)
				.Columns("tcdDocrec").DefValue = CStr(lclsDoc_req_cli.dDocrec)
				.Columns("tcdDocdate").DefValue = CStr(lclsDoc_req_cli.dDocdate)
				.Columns("tcdExpirdat").DefValue = CStr(lclsDoc_req_cli.dExpirdat)
				.Columns("tcdDateto").DefValue = CStr(lclsDoc_req_cli.dDateto)
				.Columns("tcdDatefree").DefValue = CStr(lclsDoc_req_cli.dDatefree)
				
				If lclsDoc_req_cli.sRequest = "1" Then
					.Columns("Sel").Disabled = True
				Else
					.Columns("Sel").Disabled = False
				End If
				
				If lclsDoc_req_cli.sRequire = "1" Then
					.Columns("Sel").Disabled = True
					.Columns("chkRequire").Checked = CShort("1")
				Else
					.Columns("Sel").Disabled = False
					.Columns("chkRequire").Checked = CShort("2")
				End If
				
				.Columns("btnNotenum").nNotenum = lclsDoc_req_cli.nNotenum
				Response.Write(.DoRow)
			End With
		Next lclsDoc_req_cli
	End If
	Response.Write(mobjGrid.closeTable)
	
	lclsDoc_req_clis = Nothing
	lclsDoc_req_cli = Nothing
End Sub

'%InsPreBC803Upd: Se efectúan las acciones de la ventana PopUp
'------------------------------------------------------------------------
Private Function InsPreBC803Upd() As Object
	'------------------------------------------------------------------------
	Dim lclsDoc_req_cli_d As eClient.Doc_req_cli
	With Request
		If .QueryString.Item("Action") = "Del" Then
			Response.Write(mobjValues.ConfirmDelete())
			
			lclsDoc_req_cli_d = New eClient.Doc_req_cli
			lclsDoc_req_cli_d.InsPostBC803(.QueryString.Item("Action"), mobjValues.StringToType(Request.QueryString.Item("nEval"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nId"), eFunctions.Values.eTypeData.etdDouble), 0, 0, "", Today, Today, Today, Today, 0, 0, 0, Today, Today)
			lclsDoc_req_cli_d = Nothing
		End If
	End With
	
	Response.Write(mobjGrid.DoFormUpd(Request.QueryString.Item("Action"), "valDocumentSeq.aspx", "BC803", Request.QueryString.Item("nMainAction"), mobjValues.ActionQuery, CShort(Request.QueryString.Item("Index"))))
	
	If Request.QueryString.Item("Action") = "Update" Then
		Response.Write("<SCRIPT>self.document.forms[0].tcnNotenum.value = top.opener.marrArray[CurrentIndex].btnNotenum</" & "Script>")
		Response.Write("<SCRIPT>self.document.forms[0].hddEval.value = '" & Request.QueryString.Item("nEval") & "'</" & "Script>")
	ElseIf Request.QueryString.Item("Action") = "Add" Then 
		Response.Write("<SCRIPT>ShowChangeValues('Docreq');</" & "Script>")
	End If
End Function

'%InsPreBC803_Eval: Se obtiene la información de la tabla eval_master.
'--------------------------------------------------------------------------------------------
Private Sub InsPreBC803_Eval()
	'--------------------------------------------------------------------------------------------
	mclsEval_master = New eClient.eval_master
	mclsEval_master.Find_eval(mobjValues.StringToType(Request.QueryString.Item("nEval"), eFunctions.Values.eTypeData.etdDouble), Session("sClient"))
End Sub

</script>
<%Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage("bc803")

With Server
	mobjValues = New eFunctions.Values
	'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.24.56
	mobjValues.sSessionID = Session.SessionID
	mobjValues.nUsercode = Session("nUsercode")
	'~End Body Block VisualTimer Utility
	
	mobjValues.sCodisplPage = "bc803"
	mobjMenu = New eFunctions.Menues
	'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.24.56
	mobjMenu.sSessionID = Session.SessionID
	mobjMenu.nUsercode = Session("nUsercode")
	'~End Body Block VisualTimer Utility
End With

%>
<HTML>
<HEAD>
    <META NAME = "GENERATOR" CONTENT = "Microsoft Visual Studio 6.0">
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>




<SCRIPT LANGUAGE="JavaScript">
	var nMainAction = <%=Request.QueryString.Item("nMainAction")%>;
</SCRIPT>
<%With Response
	.Write(mobjValues.StyleSheet())
	If Request.QueryString.Item("Type") <> "PopUp" Then
		.Write(mobjMenu.setZone(2, "BC803", Request.QueryString.Item("sWindowDescript"), CShort(Request.QueryString.Item("nWindowTy"))))
	End If
End With
mobjMenu = Nothing
%>
<SCRIPT>
//+ Variable para el control de versiones
	document.VssVersion="$$Revision: 3 $|$$Date: 29/10/03 21:13 $"

//%insReload: se controla el cambio de valor del campo "Evaluación"
//-------------------------------------------------------------------------------------------
function insReload(Field){
//-------------------------------------------------------------------------------------------
	var lstrURL;
	lstrURL = document.location.href.replace(/Reload=1/,'Reload=2');
	lstrURL = lstrURL.replace(/&nEval=.*/,'') + "&nEval=" + Field.value + "&nValid=2";
	document.location.href = lstrURL;
}

//%ShowChange: Habilita/deshabilita campos de la ventana cuando abandona el campo del estado del documento.
//-------------------------------------------------------------------------------------------
function ShowChange(){
//-------------------------------------------------------------------------------------------
	with (self.document.forms[0]){
		if (cbeStatusdoc.value==7){
		    tcdDatefree.disabled = false
		    btn_tcdDatefree.disabled = false
		}
		else{
		    tcdDatefree.disabled = true
		    btn_tcdDatefree.disabled = true
		}
    }
}
//% ShowChangeValues: Se asigna valor a los controles cuyo valor depende de otros controles 
//------------------------------------------------------------------------------------------- 
function ShowChangeValues(sField){ 
//------------------------------------------------------------------------------------------- 
	switch(sField){ 
		case "Docreq": 
		    with(self.document.forms[0]){ 
			    lstrParams = "nTypedoc=" + cbeTypedoc.value +  
							 "&dDocdate=" + tcdDocdate.value 
			} 
			insDefValues(sField,lstrParams,'/VTimeNet/Client/DocumSeq'); 
			break; 
	} 
} 

//%ChangeInform: Segun el tipo de iunformación es la etiqueta Cotización/propuesta/póliza
//-------------------------------------------------------------------------------------------
function ChangeInform(sField){
//-------------------------------------------------------------------------------------------

	if (sField == '1' || 
	    sField == '6' ||
	    sField == '7'){
	    ShowDiv('DivPro', 'show')
		ShowDiv('DivPol', 'hide')
		ShowDiv('DivCot', 'hide')
	}
	else{
	    if (sField == '2'){
			ShowDiv('DivPro', 'hide')
			ShowDiv('DivPol', 'show')
			ShowDiv('DivCot', 'hide')
	    }else{
			ShowDiv('DivPro', 'hide')
			ShowDiv('DivPol', 'hide')
			ShowDiv('DivCot', 'show')
	    }
	}	
	
	if (sField == ''){
	    ShowDiv('DivPro', 'show')
		ShowDiv('DivPol', 'hide')
		ShowDiv('DivCot', 'hide')	
	}
}   
</SCRIPT>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" ID="FORM" NAME="frmBC001N" ACTION="valDocumentSeq.aspx?mode=1">
<%=mobjValues.ShowWindowsName(Request.QueryString.Item("sCodispl"), Request.QueryString.Item("sWindowDescript"))%>
<%
Call InsPreBC803_Eval()
Call insDefineHeader()
If Request.QueryString.Item("Type") <> "PopUp" Then%>

		<TABLE WIDTH="100%">
			<TR>
				<TD><LABEL ID=0><%= GetLocalResourceObject("cbeEvalCaption") %></LABEL></TD>
					
				<TD><%	
	With mobjValues.Parameters
		.Add("sClient", Session("sClient"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Add("nBranch", mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble, True), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Add("nProduct", mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble, True), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Add("nPolicy", mobjValues.StringToType(Session("nPolicy"), eFunctions.Values.eTypeData.etdDouble), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Add("nCertif", mobjValues.StringToType(Session("nCertif"), eFunctions.Values.eTypeData.etdDouble), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	End With
	Response.Write(mobjValues.PossiblesValues("cbeEval", "Tab_eval", eFunctions.Values.eValuesType.clngComboType, mobjValues.StringToType(CStr(mclsEval_master.nEval), eFunctions.Values.eTypeData.etdDouble), True,  ,  ,  ,  , "insReload(this);",  ,  , GetLocalResourceObject("cbeEvalToolTip")))
	mobjValues.ActionQuery = Session("bQuery")
	%>
				</TD>
				<TD >&nbsp</TD>
				<TD><LABEL ID=0><%= GetLocalResourceObject("cbeStatus_evalCaption") %></LABEL></TD>
				<TD><%=mobjValues.PossiblesValues("cbeStatus_eval", "table5572", eFunctions.Values.eValuesType.clngComboType, mobjValues.StringToType(CStr(mclsEval_master.nStatus_eval), eFunctions.Values.eTypeData.etdDouble),  , True,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbeStatus_evalToolTip"))%></TD>
			</TR>
			<TR>
				<TD><LABEL ID=0><%= GetLocalResourceObject("tcnCapitalCaption") %></LABEL></TD>
				<TD><%=mobjValues.NumericControl("tcnCapital", 18, mobjValues.StringToType(CStr(mclsEval_master.nCapital), eFunctions.Values.eTypeData.etdDouble), True, GetLocalResourceObject("tcnCapitalToolTip"), True, 6,  ,  ,  ,  , True)%></TD>
				<TD >&nbsp</TD>
				<TD><LABEL ID=0><%= GetLocalResourceObject("cbeCurrencyCaption") %></LABEL></TD>
				<TD><%=mobjValues.PossiblesValues("cbeCurrency", "table11", eFunctions.Values.eValuesType.clngComboType, mobjValues.StringToType(CStr(mclsEval_master.nCurrency), eFunctions.Values.eTypeData.etdDouble),  , True,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbeCurrencyToolTip"))%></TD>
			</TR>  
			<TR>
				<TD><LABEL ID=0><%= GetLocalResourceObject("tcnCumulCaption") %></LABEL></TD>
				<TD><%=mobjValues.NumericControl("tcnCumul", 18, mobjValues.StringToType(CStr(mclsEval_master.nCumul), eFunctions.Values.eTypeData.etdDouble),  , GetLocalResourceObject("tcnCumulToolTip"), True, 6,  ,  ,  ,  , True)%></TD>
			</TR>
			<TR>
			  <TD COLSPAN="5" CLASS="HighLighted"><LABEL><A NAME="Datos poliza"><%= GetLocalResourceObject("AnchorDatos polizaCaption") %></A></LABEL></TD>
			</TR>
			<TR>
			  <TD WIDTH="100%" COLSPAN="5"><HR></TD>
			</TR> 
			<TR> 
			    <TD><LABEL ID=9380><%= GetLocalResourceObject("cbeCertypeCaption") %></LABEL></TD> 
				<TD><%=mobjValues.PossiblesValues("cbeCertype", "Table5632", eFunctions.Values.eValuesType.clngComboType, mclsEval_master.sCertype,  , True,  ,  ,  ,  , True,  , GetLocalResourceObject("cbeCertypeToolTip"))%></TD> 
			</TR>		
			<TR> 
			    <TD><LABEL ID=9380><%= GetLocalResourceObject("cbeBranchCaption") %></LABEL></TD> 
				<TD><%=mobjValues.BranchControl("cbeBranch", GetLocalResourceObject("cbeBranchToolTip"), CStr(mclsEval_master.nBranch),  , True,  ,  ,  , True)%></TD> 
			    <TD><LABEL ID=9389><%= GetLocalResourceObject("valProductCaption") %></LABEL></TD>
			    <%	mobjValues.ActionQuery = True%>
			    <TD COLSPAN="2"><%	mobjValues.Parameters.Add("nBranch", mclsEval_master.nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	Response.Write(mobjValues.PossiblesValues("valProduct", "tabProdmaster1", eFunctions.Values.eValuesType.clngWindowType, CStr(mclsEval_master.nProduct), True, True))
	%>
				</TD> 
				<%	mobjValues.ActionQuery = Session("bQuery")%>
			</TR>
			<TR>
				<TD>
					<DIV ID="DivPro" >
						<LABEL ID=0><%= GetLocalResourceObject("AnchorCaption") %></LABEL>
					</DIV>
					<DIV ID="DivPol">
						<LABEL ID=0><%= GetLocalResourceObject("Anchor2Caption") %></LABEL>
					</DIV>
					<DIV ID="DivCot">
						<LABEL ID=0><%= GetLocalResourceObject("Anchor3Caption") %></LABEL>
					</DIV>	
				</TD>
				<!--<TD><LABEL ID=9388><%= GetLocalResourceObject("tcnPolicyCaption") %></LABEL></TD> -->
				<TD><%=mobjValues.NumericControl("tcnPolicy", 5, CStr(mclsEval_master.nPolicy),  , GetLocalResourceObject("tcnPolicyToolTip"),  , 0,  ,  ,  ,  , True)%></TD> 
				<TD><LABEL ID=9381><%= GetLocalResourceObject("tcnCertifCaption") %></LABEL></TD> 
				<TD><%=mobjValues.NumericControl("tcnCertif", 5, CStr(mclsEval_master.nCertif),  , GetLocalResourceObject("tcnCertifToolTip"),  , 0,  ,  ,  ,  , True)%> </TD> 
			</TR> 
			<TR> 
		</TABLE>
		<BR>
		
<SCRIPT> 
	ChangeInform('<%=mclsEval_master.sCertype%>')
</SCRIPT>		
		
<%	
	Call InsPreBC803()
Else
	Call InsPreBC803Upd()
End If

mobjGrid = Nothing
mobjValues = Nothing
mclsEval_master = Nothing
%>
</FORM>    
</BODY>
</HTML>

<%'^Begin Footer Block VisualTimer Utility 1.1 7/4/03 10.24.56
Call mobjNetFrameWork.FinishPage("bc803")
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>




