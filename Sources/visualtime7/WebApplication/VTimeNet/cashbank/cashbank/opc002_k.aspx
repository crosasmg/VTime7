<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eCashBank" %>
<script language="VB" runat="Server">
'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values
Dim mobjGrid As eFunctions.Grid
Dim mobjMenu As eFunctions.Menues
Dim sw As Object
Dim Bank As Object
Dim nCurrency As Object


'% insDefineHeader: Se definen los campos del grid
'--------------------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'--------------------------------------------------------------------------------------------
	
	mobjGrid = New eFunctions.Grid
	
	mobjGrid.sCodisplPage = "opc002_k"
	
	mobjGrid.bCheckVisible = False
	If Request.QueryString.Item("Type") <> "PopUp" Then
		
Response.Write("" & vbCrLf)
Response.Write("	<TABLE WIDTH = ""100%"" BORDER = 0>" & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("		   <TD WIDTH = ""30%"" CLASS=""HighLighted""><LABEL ID=41425><A NAME=""Tipo de Información"">" & GetLocalResourceObject("AnchorTipo de InformaciónCaption") & "</A></LABEL></TD>" & vbCrLf)
Response.Write("		   <TD WIDTH = ""10%""></TD>" & vbCrLf)
Response.Write("		   <TD WIDTH = ""60%"" COLSPAN = 2 CLASS=""HighLighted""><LABEL ID=41425><A NAME=""Cuenta bancaria"">" & GetLocalResourceObject("AnchorCuenta bancariaCaption") & "</A></LABEL></TD>	 " & vbCrLf)
Response.Write("		</TR>" & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("		   <TD><HR></TD>		    " & vbCrLf)
Response.Write("		   <TD></TD>	" & vbCrLf)
Response.Write("		   <TD COLSPAN = 2 ><HR></TD>		    " & vbCrLf)
Response.Write("		</TR>" & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("		   <TD COLSPAN = 2>" & vbCrLf)
Response.Write("		   ")

		
		If CDbl(Request.QueryString.Item("optInfType")) = 1 Or Request.QueryString.Item("optInfType") = vbNullString Then
			session("opt") = 1
		Else
			session("opt") = 2
		End If
		Response.Write(mobjValues.OptionControl(40132, "optInfType", GetLocalResourceObject("optInfType_CStr1Caption"),  , CStr(1), "Location(this);"))
		
Response.Write("</TD>" & vbCrLf)
Response.Write("" & vbCrLf)
Response.Write("		   <TD><LABEL ID=8688>" & GetLocalResourceObject("gmnAccountCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("           <TD> ")

		
		If CDbl(Request.QueryString.Item("optInfType")) = 1 Then
			Response.Write(mobjValues.PossiblesValues("gmnAccount", "tabBank_acc", 2, CStr(0), False,  ,  ,  ,  ,  , True,  , GetLocalResourceObject("gmnAccountToolTip")))
		ElseIf CDbl(Request.QueryString.Item("optInfType")) = 2 Then 
			If Request.QueryString.Item("nAccount") = vbNullString Then
				Response.Write(mobjValues.PossiblesValues("gmnAccount", "tabBank_acc", 2, session("nAccount"), False,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("gmnAccountToolTip")))
			Else
				Response.Write(mobjValues.PossiblesValues("gmnAccount", "tabBank_acc", 2, Request.QueryString.Item("nAccount"), False,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("gmnAccountToolTip")))
			End If
		Else
			Response.Write(mobjValues.PossiblesValues("gmnAccount", "tabBank_acc", 2, CStr(0), False,  ,  ,  ,  ,  , True,  , GetLocalResourceObject("gmnAccountToolTip")))
		End If
		
Response.Write("</TD>" & vbCrLf)
Response.Write("		</TR>" & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write(" 	        <TD COLSPAN = 2>" & vbCrLf)
Response.Write(" 						")

		Response.Write(mobjValues.OptionControl(40133, "optInfType", GetLocalResourceObject("optInfType_CStr2Caption"), CStr(1), CStr(2), "Location(this);"))
Response.Write("" & vbCrLf)
Response.Write("			</TD>" & vbCrLf)
Response.Write("		   <TD><LABEL ID=8689>" & GetLocalResourceObject("AnchorCaption") & "</LABEL></td>" & vbCrLf)
Response.Write("		   <TD>")

mobjValues.DIVControl("tcnAvailable")
Response.Write("" & vbCrLf)
Response.Write("			")


Response.Write(mobjValues.DIVControl("sCurrency"))


Response.Write("" & vbCrLf)
Response.Write("		  </TD>" & vbCrLf)
Response.Write("		</TR>" & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("		   <TD COLSPAN = 2></TD>" & vbCrLf)
Response.Write("		   <TD><LABEL ID=8690>" & GetLocalResourceObject("Anchor2Caption") & "</LABEL></d></TD>	" & vbCrLf)
Response.Write("		   <TD>")


Response.Write(mobjValues.DIVControl("tcnTransit"))


Response.Write("</TD>" & vbCrLf)
Response.Write("		</TR>" & vbCrLf)
Response.Write("	</TABLE>" & vbCrLf)
Response.Write("<Script>" & vbCrLf)
Response.Write("function Checked(opt) {" & vbCrLf)
Response.Write("/*{	with (self.document.forms[0] )" & vbCrLf)
Response.Write("	{	if (opt == 1)" & vbCrLf)
Response.Write("		{optInfType[0].checked = true;}" & vbCrLf)
Response.Write("		else" & vbCrLf)
Response.Write("		{optInfType[1].checked = true;}" & vbCrLf)
Response.Write("	}*/" & vbCrLf)
Response.Write("}" & vbCrLf)
Response.Write("</" & "Script>")

		
		'	Response.Write "<NOTSCRIPT>"
		'	Response.Write "Checked(" & Session("opt") & ");"
		'	Response.Write "</" & "Script>"
	Else
		Response.Write(mobjValues.HiddenControl("hoptInfType", CStr(1)))
		Response.Write(mobjValues.HiddenControl("hgmnAccount", CStr(0)))
	End If
	
	'+ Se definen las columnas del grid   
	
	With mobjGrid.Columns
		
		If session("opt") = 1 Then
			Call .AddTextColumn(40139, GetLocalResourceObject("tctChequeColumnCaption"), "tctCheque", 10, CStr(eRemoteDB.Constants.strNull),  , GetLocalResourceObject("tctChequeColumnToolTip"))
		ElseIf session("opt") = 2 Then 
			Call .AddNumericColumn(40137, GetLocalResourceObject("nRequest_nuColumnCaption"), "nRequest_nu", 10, CStr(eRemoteDB.Constants.strNull),  , GetLocalResourceObject("nRequest_nuColumnToolTip"))
		Else
			Call .AddTextColumn(40139, GetLocalResourceObject("tctChequeColumnCaption"), "tctCheque", 10, CStr(eRemoteDB.Constants.strNull),  , GetLocalResourceObject("tctChequeColumnToolTip"))
		End If
		
		
		Call .AddPossiblesColumn(40134, GetLocalResourceObject("cboSta_chequeColumnCaption"), "cboSta_cheque", "Table187", eFunctions.Values.eValuesType.clngComboType, CStr(eRemoteDB.Constants.intNull),  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cboSta_chequeColumnToolTip"))
		Call .AddNumericColumn(40138, GetLocalResourceObject("tcnAmountColumnCaption"), "tcnAmount", 19, "",  , GetLocalResourceObject("tcnAmountColumnCaption"), True, 6)
		Call .AddDateColumn(40143, GetLocalResourceObject("tcdDat_proposColumnCaption"), "tcdDat_propos", CStr(eRemoteDB.Constants.dtmNull),  , GetLocalResourceObject("tcdDat_proposColumnToolTip"))
		Call .AddDateColumn(40144, GetLocalResourceObject("tcdIssue_datColumnCaption"), "tcdIssue_dat", CStr(eRemoteDB.Constants.dtmNull),  , GetLocalResourceObject("tcdIssue_datColumnToolTip"))
		Call .AddPossiblesColumn(40135, GetLocalResourceObject("cboConceptColumnCaption"), "cboConcept", "Table293", eFunctions.Values.eValuesType.clngComboType, CStr(eRemoteDB.Constants.intNull),  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cboConceptColumnToolTip"))
		Call .AddClientColumn(0, GetLocalResourceObject("tctClientColumnCaption"), "tctClient", vbNullString,  , GetLocalResourceObject("tctClientColumnToolTip"),  ,  , "lblCliename", False,  ,  ,  ,  , True)
	End With
	
	'+ Se definen las propiedades generales del grid
	
	With mobjGrid
		.Codispl = "OPC002_k"
		.DeleteButton = False
		.AddButton = False
		.Columns("Sel").GridVisible = False
		.Height = 320
		.Width = 400
		.Top = 10
		.Left = 10
	End With
End Sub

'% insPreOPC002: Se cargan los controles de la página
'--------------------------------------------------------------------------------------------
Private Sub insPreOPC002()
	'--------------------------------------------------------------------------------------------
	Dim lclsCheque As eCashBank.Cheque
	Dim lcolCheques As eCashBank.Cheques
	Dim lCountReg As Byte
	Dim lclsacc As Object
	
	lclsCheque = New eCashBank.Cheque
	lcolCheques = New eCashBank.Cheques
	
	'+ Se ejecuta el select preparado
	If Not IsNothing(Request.QueryString.Item("nAccount")) Or CStr(session("optInfType")) = "2" Then
		session("optInfType") = ""
		If lcolCheques.Find_Cheques(mobjValues.StringToType(Request.QueryString.Item("nAccount"), eFunctions.Values.eTypeData.etdDouble), Request.QueryString.Item("sCheque"), mobjValues.StringToType(Request.QueryString.Item("nRequest_nu"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nSta_cheque"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(Request.QueryString.Item("nAmount"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("dDat_propos"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Request.QueryString.Item("dIssue_dat"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Request.QueryString.Item("nConcept"), eFunctions.Values.eTypeData.etdLong), Request.QueryString.Item("sClient"), mobjValues.StringToType(session("opt"), eFunctions.Values.eTypeData.etdLong)) Then
			
			
			lCountReg = 1
			
			For	Each lclsCheque In lcolCheques
				With lclsCheque
					
					If lCountReg = 1 Then
						Response.Write("<SCRIPT>")
						Response.Write("with (self.document.forms[0])")
						Response.Write("{")
						Response.Write("UpdateDiv('sCurrency','" & .sCurrency & "');")
						Response.Write("UpdateDiv('tcnTransit','" & mobjValues.TypeToString(.nTransit_1 + .nTransit_2 + .nTransit_3 + .nTransit_4 + .nTransit_5 + .nAvailable, eFunctions.Values.eTypeData.etdDouble, True, 2) & "');")
						Response.Write("UpdateDiv('tcnAvailable','" & mobjValues.TypeToString(.nAvailable, eFunctions.Values.eTypeData.etdDouble, True, 2) & "');")
						Response.Write("}")
						Response.Write("</" & "Script>")
					End If
					
					If session("opt") = 1 Then
						mobjGrid.Columns("tctcheque").DefValue = lclsCheque.scheque
					Else
						mobjGrid.Columns("nRequest_nu").DefValue = CStr(lclsCheque.nRequest_nu)
					End If
					mobjGrid.Columns("cboSta_cheque").DefValue = CStr(lclsCheque.nSta_cheque)
					mobjGrid.Columns("tcnAmount").DefValue = CStr(lclsCheque.nAmount)
					mobjGrid.Columns("tcdDat_propos").DefValue = CStr(lclsCheque.dDat_propos)
					mobjGrid.Columns("tcdIssue_dat").DefValue = CStr(lclsCheque.dIssue_dat)
					mobjGrid.Columns("cboConcept").DefValue = CStr(lclsCheque.nConcept)
					mobjGrid.Columns("tctClient").DefValue = lclsCheque.sClient
					Response.Write(mobjGrid.DoRow())
				End With
			Next lclsCheque
		End If
		
Response.Write("" & vbCrLf)
Response.Write("<SCRIPT>" & vbCrLf)
Response.Write("//%Disabled: Inhabilita los campos de la forma cuando se muestra el resultado" & vbCrLf)
Response.Write("//-------------------------------------------------------------------------------------" & vbCrLf)
Response.Write("function Disabled()" & vbCrLf)
Response.Write("//-------------------------------------------------------------------------------------" & vbCrLf)
Response.Write("{" & vbCrLf)
Response.Write("	with (self.document.forms[0])" & vbCrLf)
Response.Write("	{" & vbCrLf)
Response.Write("		optInfType[0].disabled = true;" & vbCrLf)
Response.Write("		optInfType[1].disabled = true;" & vbCrLf)
Response.Write("		gmnAccount.disabled = true;" & vbCrLf)
Response.Write("		btngmnAccount.disabled = true" & vbCrLf)
Response.Write("	}" & vbCrLf)
Response.Write("}" & vbCrLf)
Response.Write("</" & "SCRIPT>")

		
		Response.Write("<SCRIPT>")
		Response.Write("Disabled();")
		Response.Write("</" & "Script>")
		
	End If
	Response.Write(mobjGrid.closeTable())
	
        lclsCheque = Nothing
	lcolCheques = Nothing
	
End Sub
'-----------------------------------------------------------------------------
Private Sub insPreOPC002Upd()
	'-----------------------------------------------------------------------------
	Response.Write(mobjGrid.DoFormUpd(Request.QueryString.Item("Action"), "ValCashBank.aspx", "OPC002", Request.QueryString.Item("nMainAction"), False, CShort(Request.QueryString.Item("nIndex"))))
	Response.Write("<SCRIPT>")
	Response.Write("SaveOptions();")
	Response.Write("</" & "Script>")
End Sub

</script>
<%

Response.Expires = 0

mobjValues = New eFunctions.Values
mobjMenu = New eFunctions.Menues

mobjValues.sCodisplPage = "opc002_k"
%>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<%
If Request.QueryString.Item("Type") <> "PopUp" Then
	%>
	<%	'$$EWI_1012:D:\VisualTIMEChile\Result\VTimeStep1\cashbank\cashbank\Vtime\Scripts\tMenu.js#%>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tMenu.js"></SCRIPT>
<%	
End If
%>
<HTML>
<HEAD>
    <META NAME = "GENERATOR" Content = "Microsoft Visual Studio 6.0">


<SCRIPT>
var Opt

//------------------------------------------------------------------------------------------
function insCancel(){
//------------------------------------------------------------------------------------------
	return true;
}

//------------------------------------------------------------------------------------------
function  insStateZone(){
//------------------------------------------------------------------------------------------
    var lintIndex = 0;
    for (lintIndex = 0; lintIndex < document.forms[0].length; lintIndex++)
        document.forms[0].elements[lintIndex].disabled = false;
    EditRecord(-1, nMainAction, 'Add')
    document.forms[0].btngmnAccount.disabled = false;
}

//------------------------------------------------------------------------------------------
function SaveOptions()
{
//------------------------------------------------------------------------------------------ 
	with (self.document.forms[0])
	{
		if (top.opener.document.forms[0].optInfType[0].checked) 
		    hoptInfType.value = 1;
		else
			hoptInfType.value = 2;
			
		hgmnAccount.value = top.opener.document.forms[0].gmnAccount.value;
	}
}
function Location(Value)
{
	/*with (self.document.forms[0])
	{	if (optInfType[0].checked )
			{self.document.location.href="OPC002_k.aspx?sCodispl=OPC002&optInfType=1";}
		else
			{self.document.location.href="OPC002_k.aspx?sCodispl=OPC002&optInfType=2";}
	}*/
}
</SCRIPT>
<%
With Response
	.Write(mobjValues.StyleSheet() & vbCrLf)
	.Write(mobjValues.WindowsTitle("OPC002"))
	If Request.QueryString.Item("Type") <> "PopUp" Then
		.Write(mobjMenu.setZone(2, Request.QueryString.Item("sCodispl"), "OPC002.aspx"))
		.Write(mobjMenu.MakeMenu("OPC002", "OPC002_k.aspx", 2, ""))
		.Write("<SCRIPT>var nMainAction=top.frames[""fraSequence""].plngMainAction</SCRIPT>")
	End If
End With
mobjMenu = Nothing
%>

</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="post" ID="FORM" NAME="frmChequesInq" ACTION="ValCashBank.aspx?Zone=1">
<%
If Request.QueryString.Item("Type") <> "PopUp" Then
	Response.Write("<BR><BR>")
End If
Response.Write(mobjValues.ShowWindowsName("OPC002"))

Call insDefineHeader()

If Request.QueryString.Item("Type") <> "PopUp" Then
	Response.Write("<BR>")
	Call insPreOPC002()
Else
	Call insPreOPC002Upd()
End If
mobjGrid = Nothing
mobjValues = Nothing
%>     
</FORM>
</BODY>
</HTML>





