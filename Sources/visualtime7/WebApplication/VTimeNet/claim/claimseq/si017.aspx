<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eClaim" %>
<script language="VB" runat="Server">
'^Begin Header Block VisualTimer Utility 1.1 7/4/03 11.35.39
Dim mobjNetFrameWork As eNetFrameWork.Layout
'~End Header Block VisualTimer Utility

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values
Dim mobjGrid As eFunctions.Grid
Dim mobjMenu As eFunctions.Menues


'----------------------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'----------------------------------------------------------------------------------------------
	Dim lintCase_num As Object
	Dim lintDeman_type As Object
	Dim lstrClient As Object
	
	mobjGrid = New eFunctions.Grid
	'^Begin Body Block VisualTimer Utility 1.1 7/4/03 11.35.39
	mobjGrid.sSessionID = Session.SessionID
	mobjGrid.nUsercode = Session("nUsercode")
	'~End Body Block VisualTimer Utility
	
	mobjGrid.sCodisplPage = "si017"
	Call mobjGrid.SetWindowParameters(Request.QueryString("sCodispl"), Request.QueryString("sWindowDescript"), Request.QueryString("nWindowTy"))
        If Session("nTransaction") = eClaim.Claim_win.eClaimTransac.clngClaimQuery Then
            mobjGrid.ActionQuery = Session("bQuery")
        Else
            mobjGrid.ActionQuery = False
            Session("bQuery") = False
        End If
	
	With mobjGrid.Columns
		'UPGRADE_WARNING: Date was upgraded to Today and has a new behavior. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1041.aspx'
		.AddDateColumn(0, "Fecha", "tcdEffecdate", CStr(Today),  , "Fecha en que se solicita el finiquito",  ,  ,  , False)
		.AddNumericColumn(0, "Número", "tcnNumber", 5, CStr(0), False, "Número correlativo que identifica al finiquito dentro del siniestro",  ,  ,  ,  ,  , True)
		.AddNumericColumn(0, "Monto pagado anteriormente", "tcnBeforeAmount", 18, CStr(0), True, "Monto pagado en otros finiquitos",  True, 6)
		.AddNumericColumn(0, "Monto del finiquito", "tcnAmount", 18, CStr(0), True, "Monto del finiquito o pago final de indemnización", True, 6)
		.AddCheckColumn(0, "Imprimir", "chkPrinted", CStr(eRemoteDB.Constants.strNull),  ,  ,  , True)
		.AddHiddenColumn("hddSel", "")
		.AddHiddenColumn("hddId", "")
		.AddHiddenColumn("hddAmount", "")
		.AddHiddenColumn("hddPaidAmount", "")
		.AddHiddenColumn("hddNumber", "")
	End With
	
	With mobjGrid
		.Codispl = "SI017"
		.Top = 250
		.Width = 310
		.Height = 270
		.ActionQuery = mobjValues.ActionQuery
		.Columns("Sel").GridVisible = Not .ActionQuery
		.Columns("tcnNumber").EditRecord = True
		
		.AddButton = True
		.DeleteButton = True
		.Columns("Sel").OnClick = "MarkRecord(this);"
		.sDelRecordParam = "nCase_num=' + self.document.forms[0].hddCaseNum.value + '" & "&nDeman_type=' + self.document.forms[0].hddDeman_Type.value + '" & "&sClient=' + self.document.forms[0].hddClient.value + '" & "&nSettlement=' + marrArray[lintIndex].hddNumber + '" & "&nId=' + marrArray[lintIndex].hddId + '" & "&nAmount=' + marrArray[lintIndex].hddAmount + '"
		.sEditRecordParam = "nCase_num=' + self.document.forms[0].hddCaseNum.value + '" & "&nDeman_type=' + self.document.forms[0].hddDeman_Type.value + '" & "&sClient=' + self.document.forms[0].hddClient.value + '" & "&cbeCase=' + self.document.forms[0].hddcbeCase.value + '"
		If Request.QueryString("Reload") = "1" Then
			.sReloadIndex = Request.QueryString("ReloadIndex")
		End If
	End With
End Sub

'----------------------------------------------------------------------------------------------
Private Sub insPreSI017()
	'----------------------------------------------------------------------------------------------
	Dim lclsSettlement As eClaim.Settlement
	Dim lcolSettlement As eClaim.Settlements
	Dim lstrDefValueCase As String
	Dim lintCase_num As Integer
	Dim lintDeman_type As Integer
	Dim lstrClient As Object
	
	
Response.Write("" & vbCrLf)
Response.Write("<TABLE WIDTH=""100%"">" & vbCrLf)
Response.Write("    <TR>" & vbCrLf)
Response.Write("        <TD><LABEL ID=9501>Caso</LABEL></TD>" & vbCrLf)
Response.Write("        <TD COLSPAN=""3"">")

	
	
	If CStr(Session("nClaim")) = vbNullString Then
		mobjValues.Parameters.Add("nClaim", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	Else
		lstrDefValueCase = Request.QueryString("nCase_num") & "/" & Request.QueryString("nDeman_type") & "/" & Request.QueryString("sClient")
		lintCase_num = mobjValues.StringToType(Request.QueryString("nCase_num"), eFunctions.Values.eTypeData.etdDouble)
		lintDeman_type = mobjValues.StringToType(Request.QueryString("nDeman_type"), eFunctions.Values.eTypeData.etdDouble)
		lstrClient = Request.QueryString("sClient")
		
		mobjValues.Parameters.Add("nClaim", mobjValues.StringToType(CStr(Session("nClaim")), eFunctions.Values.eTypeData.etdDouble), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		
	End If
	With Response
		.Write(mobjValues.PossiblesValues("cbeCase", "tabClaim_cases", eFunctions.Values.eValuesType.clngComboType, "" & lstrDefValueCase, True,  ,  ,  ,  , "insParam(this.value)", CStr(Session("nClaim")) = vbNullString,  , "Caso asociado al beneficiario, del cual sale el finiquito"))
		.Write(mobjValues.HiddenControl("hddCaseNum", CStr(lintCase_num)))
		.Write(mobjValues.HiddenControl("hddcbeCase", lstrDefValueCase))
		.Write(mobjValues.HiddenControl("hddDeman_Type", CStr(lintDeman_type)))
		.Write(mobjValues.HiddenControl("hddClient", lstrClient))
	End With
	
	
Response.Write("" & vbCrLf)
Response.Write("    </TR>" & vbCrLf)
Response.Write("</TABLE> ")

	
	With Request
		lclsSettlement = New eClaim.Settlement
		lcolSettlement = New eClaim.Settlements
		If lcolSettlement.Find(CDbl(Session("nClaim")), lintCase_num, lintDeman_type) Then
			With mobjGrid
				.Columns("Sel").OnClick = "insUpdateSelection(this)"
				For	Each lclsSettlement In lcolSettlement
					.Columns("tcdEffecdate").DefValue = CStr(lclsSettlement.dPropou_Dat)
					.Columns("tcnNumber").DefValue = CStr(lclsSettlement.nSettlement)
					.Columns("tcnBeforeAmount").DefValue = CStr(lclsSettlement.nPaid_Amoun)
					.Columns("tcnAmount").DefValue = CStr(lclsSettlement.nAmount)
					.Columns("hddId").DefValue = CStr(lclsSettlement.nId)
					.Columns("hddAmount").DefValue = CStr(lclsSettlement.nAmount)
					.Columns("hddNumber").DefValue = CStr(lclsSettlement.nSettlement)
					
					If lclsSettlement.sStatus_Fin = "1" Then
						.Columns("chkPrinted").Checked = CShort("1")
					Else
						.Columns("chkPrinted").Checked = CShort("2")
					End If
					
					Response.Write(mobjGrid.DoRow())
				Next lclsSettlement
			End With
		End If
	End With
	
	Response.Write(mobjGrid.CloseTable())
	
	'UPGRADE_NOTE: Object lclsSettlement may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
	lclsSettlement = Nothing
	'UPGRADE_NOTE: Object lcolSettlement may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
	lcolSettlement = Nothing
End Sub

'% insPreSI017Upd: manejo de la ventana PopUp
'----------------------------------------------------------------------------------------------
Private Sub insPreSI017Upd()
	'----------------------------------------------------------------------------------------------
	Dim lclsClaimSeq As eClaim.Settlement
	Dim ldblSettlement_Next as Double 
	Dim ldblAmountSettlement as Double 
	ldblAmountSettlement = 0
	
	lclsClaimSeq = New eClaim.Settlement
	
	With Request
		Select Case .QueryString("Action")
			Case "Del"
				Response.Write(mobjValues.ConfirmDelete())
				
				'UPGRADE_WARNING: Date was upgraded to Today and has a new behavior. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1041.aspx'
				Call lclsClaimSeq.InsPostSI017(Request.QueryString("Action"), CDbl(Session("nClaim")), mobjValues.StringToType(Request.QueryString("nCase_num"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString("nDeman_type"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString("nSettlement"), eFunctions.Values.eTypeData.etdDouble), Request.QueryString("sClient"), mobjValues.StringToType(Request.QueryString("nAmount"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(CStr(0), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(CStr(Session("nUsercode")), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(CStr(Today), eFunctions.Values.eTypeData.etdDate), "1", mobjValues.StringToType(CStr(Session("dEffecdate")), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Request.QueryString("nId"), eFunctions.Values.eTypeData.etdDouble))
				
		End Select
		Response.Write(mobjGrid.DoFormUpd(.QueryString("Action"), "ValClaimSeq.aspx", "SI017", .QueryString("nMainAction"), mobjValues.ActionQuery, .QueryString("Index")))
		
		If .QueryString("Action") = "Update" Then
			Response.Write("<SCRIPT>self.document.forms[0].chkPrinted.disabled=false;</" & "Script>")
			
Response.Write("	       " & vbCrLf)
Response.Write("	       <SCRIPT>" & vbCrLf)
Response.Write("	              var ldblAmountPaid = 0;" & vbCrLf)
Response.Write("	              for(var llngCount = 0;llngCount <= top.opener.marrArray.length;llngCount++)" & vbCrLf)
Response.Write("                     if (top.opener.marrArray[llngCount].tcnNumber < self.document.forms[0].tcnNumber.value){" & vbCrLf)
Response.Write("                        ldblAmountPaid = parseFloat(ldblAmountPaid) + insConvertNumber(top.opener.marrArray[llngCount].tcnAmount);" & vbCrLf)
Response.Write("                     }" & vbCrLf)
Response.Write("                     self.document.forms[0].tcnBeforeAmount.value = ldblAmountPaid;" & vbCrLf)
Response.Write("	       </" & "SCRIPT>")

			
		End If
		
		If .QueryString("Action") = "Add" Then
			Call lclsClaimSeq.FindSettlement_Next(CDbl(Session("nClaim")), mobjValues.StringToType(Request.QueryString("nCase_num"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString("nDeman_type"), eFunctions.Values.eTypeData.etdDouble), Request.QueryString("sClient"))
			
			ldblSettlement_Next = mobjValues.StringToType(CStr(lclsClaimSeq.nSettlement_Next), eFunctions.Values.eTypeData.etdDouble)
			Response.Write("<SCRIPT>self.document.forms[0].tcnNumber.value=" & ldblSettlement_Next & ";</" & "Script>")
			
			
			If lclsClaimSeq.FindAmount(CDbl(Session("nClaim")), mobjValues.StringToType(Request.QueryString("nCase_num"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString("nDeman_type"), eFunctions.Values.eTypeData.etdDouble), Request.QueryString("sClient")) Then
				ldblAmountSettlement = FormatNumber(mobjValues.StringToType(CStr(lclsClaimSeq.nAmount_Settlement), eFunctions.Values.eTypeData.etdDouble), 2)
				Response.Write("<SCRIPT>self.document.forms[0].tcnBeforeAmount.value=" & ldblAmountSettlement & ";</" & "Script>")
			End If
			
		End If
		
	End With
	
	'UPGRADE_NOTE: Object lclsClaimSeq may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
	lclsClaimSeq = Nothing
End Sub

</script>
<%Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage("si017")

mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 11.35.39
mobjValues.sSessionID = Session.SessionID
mobjValues.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjValues.sCodisplPage = "si017"
mobjMenu = New eFunctions.Menues
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 11.35.39
mobjMenu.sSessionID = Session.SessionID
mobjMenu.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjValues.ActionQuery = Session("bQuery")
%>
<HTML>
<HEAD>
<!-- #INCLUDE VIRTUAL="~/VTimeNet/Includes/Constantes.aspx" -->
<!-- #INCLUDE VIRTUAL="~/VTimeNet/Includes/Claim.aspx" -->

    <SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/Constantes.js"></SCRIPT>    
    <SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
	<META NAME = "GENERATOR" Content = "Microsoft Visual Studio 6.0">
    <%With Response
	.Write(mobjValues.StyleSheet())
	.Write(mobjValues.WindowsTitle("SI017", Request.QueryString("sWindowDescript")))
	If Request.QueryString("Type") <> "PopUp" Then
		.Write(mobjMenu.setZone(2, "SI017", Request.QueryString("sWindowDescript"), Request.QueryString("nWindowTy")))
		'UPGRADE_NOTE: Object mobjMenu may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
		mobjMenu = Nothing
		.Write("<SCRIPT>var nMainAction=top.frames['fraSequence'].plngMainAction;</SCRIPT>")
	End If
End With
%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" ID="FORM" NAME="frmSI017" ACTION="ValClaimSeq.aspx?x=1">
<%Response.Write(mobjValues.ShowWindowsName("SI017", Request.QueryString("sWindowDescript")))

Call insDefineHeader()
If Request.QueryString("Type") = "PopUp" Then
	Call insPreSI017Upd()
Else
	Call insPreSI017()
End If
'UPGRADE_NOTE: Object mobjValues may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
mobjValues = Nothing
'UPGRADE_NOTE: Object mobjGrid may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
mobjGrid = Nothing
%>
</FORM>
</BODY>
</HTML>
<SCRIPT>
//+ Variable para el control de versiones
    document.VssVersion="$$Revision: 2 $|$$Date: 15/10/03 12.31 $"        
//---------------------------------------------------------------------------------------------------------
function insUpdateSelection(lobj){
//---------------------------------------------------------------------------------------------------------
	if(mintArrayCount>0)
	    if(lobj.checked==false){
		    self.document.forms[0].hddSel[lobj.value].value = "0";
		    self.document.forms[0].Sel[lobj.value].value = "0";		    
		}    
		else{
		    self.document.forms[0].hddSel[lobj.value].value = "1";
	        self.document.forms[0].Sel[lobj.value].value = "1";		    
	    }
	else{
		self.document.forms[0].Sel.checked = lobj.checked;
		if(lobj.checked)
			self.document.forms[0].hddSel.value = "1";
		else
			self.document.forms[0].hddSel.value = "0";
	}    
}

//%insParam: Asigna los valores a los campos ocultos
//%------------------------------------------------------------------------------------------
function insParam(Case) 
//%------------------------------------------------------------------------------------------
{
    var lstrLocation = '';
    var lstrString = '';
    var lstrClient = '';
	var lstrCampo=self.document.forms[0].cbeCase.value;
	var lstrStart=lstrCampo.indexOf("/");
	var lstrCase_num = unescape(lstrCampo.substring(0,lstrStart));
	var lstrCampo1 = lstrCampo.substring(lstrStart+1,lstrCampo.legth);
    var lstrStart1 = lstrCampo1.indexOf("/");		
	var lstrDemanType = unescape(lstrCampo1.substring(0,lstrStart1));

    if (self.document.forms[0].cbeCase.value==0){
       self.document.forms[0].hddCaseNum.value = -32768;
       self.document.forms[0].hddDeman_Type.value = -32768;
       self.document.forms[0].hddClient.value = '';
	}
	else{lstrString += Case
	     lstrClient += lstrString.replace(/.*\//,"")   
	     self.document.forms[0].hddCaseNum.value = lstrCase_num
         self.document.forms[0].hddDeman_Type.value = lstrDemanType
         self.document.forms[0].hddClient.value = lstrClient
       
	     lstrLocation += document.location.href
	     lstrLocation = lstrLocation.replace(/&nCase_num.*/,"")
	     lstrLocation = lstrLocation + "&nCase_num=" + lstrCase_num + "&nDeman_type=" + lstrDemanType + "&sClient=" + lstrClient
	     document.location.href = lstrLocation;
     }
}
</SCRIPT>
<%'^Begin Footer Block VisualTimer Utility 1.1 7/4/03 11.35.39
Call mobjNetFrameWork.FinishPage("si017")
'UPGRADE_NOTE: Object mobjNetFrameWork may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>




