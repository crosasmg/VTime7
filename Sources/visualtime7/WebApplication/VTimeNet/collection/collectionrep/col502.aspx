<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eCollection" %>
<script language="VB" runat="Server">
'^Begin Header Block VisualTimer Utility 1.1 7/4/03 10.47.59
Dim mobjNetFrameWork As eNetFrameWork.Layout
'~End Header Block VisualTimer Utility
'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values
Dim mobjGrid As eFunctions.Grid
Dim mobjMenues As eFunctions.Menues


'% insDefineHeader: Se definen los campos del grid
'--------------------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'--------------------------------------------------------------------------------------------
	mobjGrid = New eFunctions.Grid
	'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.47.59
	mobjGrid.sSessionID = Session.SessionID
	mobjGrid.nUsercode = Session("nUsercode")
	'~End Body Block VisualTimer Utility
	
	mobjGrid.sCodisplPage = "col502"
	Call mobjGrid.SetWindowParameters(Request.QueryString.Item("sCodispl"), Request.QueryString.Item("sWindowDescript"), Request.QueryString.Item("nWindowTy"))
	
	'+ Se definen las columnas del grid  
	With mobjGrid.Columns
		Call .AddPossiblesColumn(0, GetLocalResourceObject("tctnBankColumnCaption"), "tctnBank", "TABTABLES_BANKPACTBK", eFunctions.Values.eValuesType.clngComboType,  , True,  ,  ,  ,  , True,  , GetLocalResourceObject("tctnBankColumnCaption"))
		With mobjGrid.Columns("tctnBank").Parameters
			If Request.QueryString.Item("Type") <> "PopUp" Then
				.Add("ANBANK_ACC_CODE", 1010, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Else
				.Add("ANBANK_ACC_CODE", 1010, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			End If
			.Add("ANWAY_PAY", Request.QueryString.Item("nWay_Pay"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		End With
		
		Call .AddTextColumn(0, GetLocalResourceObject("tctAcc_NumberColumnCaption"), "tctAcc_Number", 25, CStr(eRemoteDB.Constants.strNull), True,  ,  ,  ,  , True)
		Call .AddTextColumn(0, GetLocalResourceObject("tctDep_NumberColumnCaption"), "tctDep_Number", 12, CStr(eRemoteDB.Constants.strNull), True,  ,  ,  ,  , True)
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnAmountColumnCaption"), "tcnAmount", 18, CStr(0), True,  , True, 6,  ,  ,  , True)
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnCommissColumnCaption"), "tcnCommiss", 19, CStr(0), True,  , True, 6,  ,  ,  , True)
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnAmountDocColumnCaption"), "tcnAmountDoc", 18, CStr(0), True,  , True, 6,  ,  ,  , True)
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnAmountDifColumnCaption"), "tcnAmountDif", 18, CStr(0), True,  , True, 6,  ,  ,  , True)
		Call .AddHiddenColumn("nAcc_Bankhdr", vbNullString)
		Call .AddHiddenColumn("tcnId_Register", vbNullString)
		Call .AddHiddenColumn("sSelected", vbNullString)
		Call .AddHiddenColumn("hddnAmount", CStr(eRemoteDB.Constants.intNull))
		Call .AddHiddenColumn("hddnCommission", CStr(eRemoteDB.Constants.intNull))
		Call .AddHiddenColumn("hddnBank", CStr(eRemoteDB.Constants.intNull))
		Call .AddHiddenColumn("hdddEffecdate", CStr(eRemoteDB.Constants.dtmNull))
		Call .AddHiddenColumn("hddnMovement", CStr(eRemoteDB.Constants.intNull))
	End With
	
	'+ Se asignan las caracteristicas del Grid
	With mobjGrid
		.Codispl = "COL502"
		.Codisp = "COL502"
		.bCheckVisible = False
		.DeleteButton = False
		.AddButton = False
		.Columns("Sel").GridVisible = True
		.Height = 350
		.Width = 380
		
		If Request.QueryString.Item("Reload") = "1" Then
			.sReloadIndex = Request.QueryString.Item("ReloadIndex")
		End If
		
		.Columns("tctnBank").EditRecord = True
	End With
	
	Response.Write(mobjValues.HiddenControl("sWaypayhdr", Request.QueryString.Item("nWay_Pay")))
	
End Sub

'% insPreCOL502: Se cargan los controles de la página
'--------------------------------------------------------------------------------------------
Private Sub insPreCOL502()
	'--------------------------------------------------------------------------------------------
	Dim lclsPremium As eCollection.Premium
	Dim lcolPremiums As eCollection.Premiums
	Dim lintCount As Object
	Dim lintCommmiss As Object
	
	lclsPremium = New eCollection.Premium
	lcolPremiums = New eCollection.Premiums
	
	lintCount = 0
	If lcolPremiums.FindTMP_COL502 Then
		For	Each lclsPremium In lcolPremiums
			With mobjGrid
				
				.Columns("Sel").OnClick = "insCheckSelClick(this," & CStr(lintCount) & ");"
				
				If mobjValues.stringToType(Request.QueryString.Item("nWay_Pay"), eFunctions.Values.eTypeData.etdDouble, True) = 1 Then
					.Columns("tctnBank").DefValue = CStr(lclsPremium.nBank_Code) '+ Cuenta respectiva		        	        			    
				Else
					.Columns("tctnBank").DefValue = CStr(lclsPremium.nBank_Code) '+ Código del Banco
				End If
				
				.Columns("tctAcc_Number").DefValue = lclsPremium.sAcc_Number
				.Columns("tctDep_Number").DefValue = lclsPremium.sDep_Number
				.Columns("tcnAmount").DefValue = CStr(lclsPremium.nAmount_PAC)
				.Columns("hddnAmount").DefValue = CStr(lclsPremium.nAmount_PAC)
				.Columns("tcnCommiss").DefValue = CStr(lclsPremium.nCommission)
				.Columns("hddnCommission").DefValue = CStr(lclsPremium.nCommission)
				.Columns("nAcc_Bankhdr").DefValue = CStr(lclsPremium.nAcc_Bank)
				.Columns("tcnId_Register").DefValue = CStr(lclsPremium.nId_Register)
				.Columns("hddnBank").DefValue = CStr(lclsPremium.nBank_Code)
				.Columns("hdddEffecdate").DefValue = CStr(lclsPremium.dEffecdate)
				.Columns("hddnMovement").DefValue = CStr(lclsPremium.nMovement)
				.Columns("tcnAmountDoc").DefValue = CStr(lclsPremium.nAmountDoc)
				.Columns("tcnAmountDif").DefValue = CStr(lclsPremium.nAmountDif)
				
				If lclsPremium.nCommission >= 0 Then
					.Columns("Sel").checked = CShort("1")
					.Columns("sSelected").DefValue = "1"
				Else
					.Columns("Sel").checked = CShort("0")
					.Columns("sSelected").DefValue = "0"
				End If
				
				.sEditRecordParam = "nId_Register='+" & lclsPremium.nId_Register & "  + " & "'&nCommis='+" & lclsPremium.nCommission & "  + " & "'&nWay_Pay='+ '" & Request.QueryString.Item("nWay_Pay") & "' + " & "'&nInsur_Area='+ '" & Request.QueryString.Item("nInsur_Area") & "' + " & "'&dPayDate='+ '" & Request.QueryString.Item("dPayDate") & "' + " & "'&dLimit_pay='+ '" & Request.QueryString.Item("dLimit_pay") & "' + " & "'&nBank='   + marrArray[" & CStr(lintCount) & "].tctnBank +  " & "'&nAmount=' + marrArray[" & CStr(lintCount) & "].tcnAmount +  " & "'&nCount='+ '" & lintCount
				lintCount = lintCount + 1
			End With
			
			If lintCount = 200 Then
				Exit For
			End If
			Response.Write(mobjGrid.DoRow())
		Next lclsPremium
	End If
	
	Response.Write(mobjValues.HiddenControl("nCount", lintCount))
	Response.Write(mobjValues.HiddenControl("hddnInsur_Area", Request.QueryString.Item("nInsur_Area")))
	Response.Write(mobjValues.HiddenControl("hddnWay_Pay", Request.QueryString.Item("nWay_Pay")))
	Response.Write(mobjValues.HiddenControl("hdddLimit_pay", Request.QueryString.Item("dLimit_pay")))
	Response.Write(mobjValues.HiddenControl("hdddPayDate", Request.QueryString.Item("dPayDate")))
	
	Response.Write(mobjGrid.closeTable())
	
	lclsPremium = Nothing
	lcolPremiums = Nothing
	
End Sub

'% insPreCOL502Upd: Se hace el llamado a la ventana PopUp.
'------------------------------------------------------------------------------
Private Sub insPreCOL502Upd()
	'------------------------------------------------------------------------------
	Dim lclsCollectionRep As eCollection.CollectionRep
	Dim lstrResult As String
	
	lclsCollectionRep = New eCollection.CollectionRep
	With Response
		.Write(mobjGrid.DoFormUpd(Request.QueryString.Item("Action"), "valCollectionRep.aspx", Request.QueryString.Item("sCodispl"), Request.QueryString.Item("MainAction"),  , CShort(Request.QueryString.Item("Index"))))
		.Write(mobjValues.HiddenControl("sAction", Request.QueryString.Item("Action")))
	End With
	
	Session("nInsur_Area") = Request.QueryString.Item("nInsur_Area")
	Session("nWay_Pay") = Request.QueryString.Item("nWay_Pay")
	Session("dLimit_pay") = Request.QueryString.Item("dLimit_pay")
	Session("nBank") = Request.QueryString.Item("nBank")
	Session("nAmount") = Request.QueryString.Item("nAmount")
	
	If lclsCollectionRep.insFindMultLess(mobjValues.stringToType(Request.QueryString.Item("nInsur_Area"), eFunctions.Values.eTypeData.etdDouble), mobjValues.stringToType(Request.QueryString.Item("nWay_Pay"), eFunctions.Values.eTypeData.etdDouble), mobjValues.stringToType(Request.QueryString.Item("dLimit_pay"), eFunctions.Values.eTypeData.etdDate), mobjValues.stringToType(Request.QueryString.Item("nBank"), eFunctions.Values.eTypeData.etdDouble), mobjValues.stringToType(Request.QueryString.Item("nAmount"), eFunctions.Values.eTypeData.etdDouble)) Then
		lstrResult = "false"
	Else
		lstrResult = "true"
	End If
	Response.Write("<SCRIPT>")
	Response.Write("document.forms[0].tcnCommiss.disabled=" & lstrResult & ";")
	Response.Write("</" & "Script>")
	
End Sub

</script>
<%
Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage("col502")
mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.47.59
mobjValues.sSessionID = Session.SessionID
mobjValues.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjValues.sCodisplPage = "col502"
%>
<HTML>
<HEAD>
    <META NAME = "GENERATOR" Content = "Microsoft Visual Studio 6.0">
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
	<SCRIPT>
//+ Variable para el control de versiones
	     document.VssVersion="$$Revision: 3 $|$$Date: 16/02/04 19:16 $|$$Author: Nvaplat40 $"
    </SCRIPT>



	
<%
With Response
	.Write(mobjValues.StyleSheet())
	If Request.QueryString.Item("Type") <> "PopUp" Then
		mobjMenues = New eFunctions.Menues
		'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.47.59
		mobjMenues.sSessionID = Session.SessionID
		mobjMenues.nUsercode = Session("nUsercode")
		'~End Body Block VisualTimer Utility
		.Write(mobjMenues.setZone(2, "COL502", Request.QueryString.Item("sWindowDescript"), CShort(Request.QueryString.Item("nWindowTy"))))
		mobjMenues = Nothing
	End If
	.Write("<SCRIPT>var	nMainAction	= 0" & Request.QueryString.Item("nMainAction") & "</SCRIPT>")
End With
%>
<SCRIPT>    
//% insCheckSelClick: Al cambiar el valor de la columna sel.
//-------------------------------------------------------------------------------------------
function insCheckSelClick(Field,lintIndex){
//-------------------------------------------------------------------------------------------
    var sEditRecordParam;
    var lstrString
    
    if (!Field.checked){
	    insDefValues("UpdSelCOL502", "nId_Register=" +  marrArray[lintIndex].tcnId_Register + "&nCommission=" + " " + "&sSel=" + (Field.checked?'1':'0'))
    }
    else {           
		sEditRecordParam =	'nId_Register=' + marrArray[lintIndex].tcnId_Register;	
		sEditRecordParam += '&nAmount='		+ marrArray[lintIndex].tcnAmount;	
		sEditRecordParam += '&nCommis='		+ marrArray[lintIndex].tcncommiss;	
		sEditRecordParam += '&nWay_Pay='	+ self.document.forms[0].hddnWay_Pay.value;
		sEditRecordParam += '&nInsur_Area='	+ self.document.forms[0].hddnInsur_Area.value;	
		sEditRecordParam += '&dPayDate='	+ self.document.forms[0].hdddPayDate.value;
		sEditRecordParam += '&dLimit_pay='	+ self.document.forms[0].hdddLimit_pay.value;
		sEditRecordParam += '&nBank='       + marrArray[lintIndex].tctnBank;		
		sEditRecordParam += '&nCount='      + marrArray.length;

//		alert(sEditRecordParam);
	    lstrString = '&nInsur_Area=' + self.document.forms[0].hddnInsur_Area.value;
	    lstrString = lstrString + '&nWay_Pay=' + self.document.forms[0].hddnInsur_Area.value;	
	    lstrString = lstrString + '&dLimit_pay=' + self.document.forms[0].hdddLimit_pay.value;
		lstrString = lstrString + '&nBank=' + marrArray[lintIndex].tctnBank;

		lstrString = lstrString + '&nAmount=' + marrArray[lintIndex].tcnAmount;
        lstrString = lstrString + "&nIndex=" + Field.value;
//      lstrString = lstrString + "&nCount=" + marrArray.length;        
//		alert('lstrString:' + sEditRecordParam); 									
		
		EditRecord(lintIndex,nMainAction, 'Update', sEditRecordParam)
		Field.checked = !Field.checked   
	}
}
</SCRIPT>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<!--<FORM METHOD="POST" ID="FORM" NAME="frmTabBanks" ACTION="ValCollectionRep.aspx?nMainAction=<%=Request.QueryString.Item("nMainAction")%>"> -->
<FORM METHOD="POST" ID="FORM" NAME="COL502" ACTION="valCollectionRep.aspx?sMode=2">
    <%Response.Write(mobjValues.ShowWindowsName("COL502", Request.QueryString.Item("sWindowDescript")))
Call insDefineHeader()
If Request.QueryString.Item("Type") <> "PopUp" Then
	Call insPreCOL502()
Else
	Call insPreCOL502Upd()
End If
mobjGrid = Nothing
mobjValues = Nothing%>     
</FORM>
</BODY>
</HTML>
<%'^Begin Footer Block VisualTimer Utility 1.1 7/4/03 10.47.59
Call mobjNetFrameWork.FinishPage("col502")
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>




