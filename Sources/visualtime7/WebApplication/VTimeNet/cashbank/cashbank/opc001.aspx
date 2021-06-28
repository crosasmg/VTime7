<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eGeneral" %>
<%@ Import namespace="eCashBank" %>
<script language="VB" runat="Server">
'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values
Dim mobjGrid As eFunctions.Grid
Dim mobjMenu As eFunctions.Menues
Dim mobjGeneral As eGeneral.GeneralFunction
Dim lstrMessage As String


'% insDefineHeader: Se definen los campos del grid
'--------------------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'--------------------------------------------------------------------------------------------
	
	mobjGrid.sCodisplPage = "OPC001"
	
	'+ Se definen las columnas del grid    
	With mobjGrid.Columns
		Call .AddNumericColumn(40126, GetLocalResourceObject("tcnCash_idColumnCaption"), "tcnCash_id", 10,  ,  , GetLocalResourceObject("tcnCash_idColumnToolTip"))
		Call .AddNumericColumn(40126, GetLocalResourceObject("tcnBordereauxColumnCaption"), "tcnBordereaux", 10,  ,  , GetLocalResourceObject("tcnBordereauxColumnToolTip"))
		Call .AddDateColumn(40128, GetLocalResourceObject("tcdEffecdateColumnCaption"), "tcdEffecdate",  ,  , GetLocalResourceObject("tcdEffecdateColumnToolTip"))
		Call .AddTextColumn(0, GetLocalResourceObject("tctHourColumnCaption"), "tctHour", 10, CStr(eRemoteDB.Constants.strNull))
		Call .AddNumericColumn(40126, GetLocalResourceObject("tcnAmountColumnCaption"), "tcnAmount", 18,  ,  ,  , True, 6)
		Call .AddTextColumn(40127, GetLocalResourceObject("tctDocnumbeColumnCaption"), "tctDocnumbe", 10, CStr(eRemoteDB.Constants.strNull))
		Call .AddDateColumn(40129, GetLocalResourceObject("tcdDoc_dateColumnCaption"), "tcdDoc_date", CStr(eRemoteDB.Constants.dtmNull))
		Call .AddTextColumn(0, GetLocalResourceObject("tctMov_typeColumnCaption"), "tctMov_type", 30, CStr(eRemoteDB.Constants.strNull))
		Call .AddTextColumn(0, GetLocalResourceObject("tctConceptColumnCaption"), "tctConcept", 30, CStr(eRemoteDB.Constants.strNull))
		Call .AddTextColumn(0, GetLocalResourceObject("tctBankColumnCaption"), "tctBank", 30, CStr(eRemoteDB.Constants.strNull))
		Call .AddAnimatedColumn(101633, GetLocalResourceObject("sLinkColumnCaption"), "sLink", "/VTimeNet/Images/Lupa.bmp", GetLocalResourceObject("sLinkColumnToolTip"))
	End With
	
	'+ Se definen las propiedades generales del grid
	With mobjGrid
		.Codispl = "OPC001"
		.Codisp = "OPC001"
		.DeleteButton = False
		.AddButton = False
		.Columns("Sel").GridVisible = False
	End With
End Sub

'% insPreOPC001: Se cargan los controles de la página
'--------------------------------------------------------------------------------------------
Private Sub insPreOPC001()
	'--------------------------------------------------------------------------------------------
	Dim lclsCash_mov As eCashBank.Cash_mov
	Dim lcolCash_movs As eCashBank.Cash_movs
	Dim lIndex As Integer
	Dim nType_mov As Integer
	Dim nEndBalance As Object
	Dim nIniBalance As Object
	Dim lintCount As Short
	
	lcolCash_movs = New eCashBank.Cash_movs
	lclsCash_mov = New eCashBank.Cash_mov
	
	If session("nType_mov") = 0 Then
		nType_mov = eRemoteDB.Constants.intNull
	Else
		nType_mov = session("nType_mov")
	End If
	nIniBalance = 0
	If session("dDate_ini") = session("dDate_end") Then
		
Response.Write("" & vbCrLf)
Response.Write("" & vbCrLf)
Response.Write("    <TABLE WIDTH=""100%"">" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("            <TD WIDTH= ""60%""></TD>" & vbCrLf)
Response.Write("			<TD  WIDTH = ""15%""><LABEL ID=8840>" & GetLocalResourceObject("tcnIniBalanceCaption") & " </LABEL></TD>" & vbCrLf)
Response.Write("			<TD>")


Response.Write(mobjValues.NumericControl("tcnIniBalance", 18, nIniBalance,  , "", True, 6, True))


Response.Write("</TD>" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("    </TABLE>")

		
	End If
	
	If lcolCash_movs.FindByCash(CDate(Request.QueryString.Item("dDate_ini")), CDate(Request.QueryString.Item("dDate_end")), CInt(Request.QueryString.Item("nOffice")), CInt(Request.QueryString.Item("nCurrency")), CInt(Request.QueryString.Item("nType_mov")), CInt(Request.QueryString.Item("nCashnum")), CInt(Request.QueryString.Item("nConcept"))) Then
		nEndBalance = 0
		lintCount = 0
		For lIndex = 1 To lcolCash_movs.Count
			lclsCash_mov = lcolCash_movs.Item(lIndex)
			With lclsCash_mov
				mobjGrid.Columns("tcnCash_id").DefValue = CStr(.nCash_id)
				mobjGrid.Columns("tcnBordereaux").DefValue = CStr(.nBordereaux)
				mobjGrid.Columns("tcdEffecdate").DefValue = CStr(.dEffecdate)
				
				If Len(Trim(CStr(.dCompdate))) > 10 Then
					mobjGrid.Columns("tctHour").DefValue = Mid(Trim(CStr(.dCompdate)), InStr(1, Trim(CStr(.dCompdate)), " ") + 1)
				Else
					mobjGrid.Columns("tctHour").DefValue = "12:00 AM"
				End If
				mobjGrid.Columns("tcnAmount").DefValue = CStr(.nAmount)
				mobjGrid.Columns("sLink").HRefScript = "insShowAssociated(" & .nMov_type & "," & .nBordereaux & ",'&sVoucherNumber=" & .sDep_number & "&nAccount=" & .nAcc_bank & "&nBordereaux=" & .nBordereaux & "&nCashnum=" & Request.QueryString.Item("nCashnum") & "');"
				If .nMov_type = 6 Or .nMov_type = 7 Then
					mobjGrid.Columns("tctDocnumbe").DefValue = .sDep_number
				ElseIf .nMov_type = 5 Then 
					mobjGrid.Columns("tctDocnumbe").DefValue = .sCard_num
				Else
					mobjGrid.Columns("tctDocnumbe").DefValue = .sDocnumbe
				End If
				
				If .dRealDep <> CDate("01/01/1900") And .dRealDep <> eRemoteDB.Constants.dtmNull Then
					mobjGrid.Columns("tcdDoc_date").DefValue = mobjValues.TypeToString(.dRealDep, eFunctions.Values.eTypeData.etdDate)
				Else
					mobjGrid.Columns("tcdDoc_date").DefValue = mobjValues.TypeToString(.dDoc_date, eFunctions.Values.eTypeData.etdDate)
				End If
				
				mobjGrid.Columns("tctMov_type").DefValue = .sMov_typeDes
				mobjGrid.Columns("tctConcept").DefValue = .sDes_Concep
				mobjGrid.Columns("tctBank").DefValue = .sBank_descript
				
				'+ Los movimientos de depósitos y los movimientos de RV (29,30,31,16,3,46) como forma de pago no se suman
				If (.nMov_type = 6 Or .nMov_type = 7 Or .nMov_type = 29 Or .nMov_type = 30 Or .nMov_type = 31 Or .nMov_type = 32 Or .nMov_type = 16 Or .nMov_type = 3 Or .nMov_type = 46) And .nBordereaux <> eRemoteDB.Constants.intNull Then
					nEndBalance = nEndBalance
				Else
					If .nMov_type <> 5 And .nMov_type <> 10 Then
						nEndBalance = nEndBalance + .nAmount
					End If
				End If
				
				Response.Write(mobjGrid.DoRow())
			End With
			lintCount = lintCount + 1
		Next 
	End If
	Response.Write(mobjGrid.closeTable())
	nEndBalance = nEndBalance + nIniBalance
	
	If session("dDate_ini") = session("dDate_end") Then
		
Response.Write("    " & vbCrLf)
Response.Write("    <TABLE WIDTH=""100%"">" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("            <TD WIDTH = ""60%""> </TD>" & vbCrLf)
Response.Write("			<TD WIDTH =""15%""><LABEL ID=8839>" & GetLocalResourceObject("tcnEndBalanceCaption") & " </LABEL></TD>" & vbCrLf)
Response.Write("            <TD>")


Response.Write(mobjValues.NumericControl("tcnEndBalance", 18, nEndBalance,  , "", True, 6, True))


Response.Write("</TD>" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("    </TABLE>	" & vbCrLf)
Response.Write("    ")

		
	End If
	lclsCash_mov = Nothing
	lcolCash_movs = Nothing
End Sub

</script>
<%Response.Expires = -1

mobjValues = New eFunctions.Values
mobjGrid = New eFunctions.Grid
mobjMenu = New eFunctions.Menues
mobjGeneral = New eGeneral.GeneralFunction

mobjValues.sCodisplPage = "OPC001"

lstrMessage = mobjGeneral.insLoadMessage(1101)
mobjGeneral = Nothing
%>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<HTML>
<HEAD>
    <META NAME = "GENERATOR" CONTENT = "Microsoft Visual Studio 6.0">


<SCRIPT>
//%Variable para el control de Versiones
    document.VssVersion="$$Revision: 1 $|$$Date: 11/02/04 17:25 $"
</SCRIPT>        
<SCRIPT>
//------------------------------------------------------------------------------------------
function insCancel(){
//------------------------------------------------------------------------------------------
	return true;
}

//------------------------------------------------------------------------------------------
function  insStateZone(){
//------------------------------------------------------------------------------------------
}

//------------------------------------------------------------------------------------------
function  insShowAssociated(nType_move,nBordereaux,sParameters){
//------------------------------------------------------------------------------------------
if (nBordereaux==-32768){
	if (nType_move == 7 || nType_move == 6){    
	    if (nType_move == 7)
	        ShowPopUp('/VTimeNet/Common/SpeWHeader.aspx?sCodispl=OP002&sModule=CashBank&sProject=CashBankSeq&nHeight=300&sCodisp=OP002&sWindowDescript=Depósitos bancarios&nWindowTy=1&sLinkSpecial=1&nOptDeposit=1' + sParameters, 'CashBank', 800, 700, 'yes', 'yes');
	    else
	        ShowPopUp('/VTimeNet/Common/SpeWHeader.aspx?sCodispl=OP002&sModule=CashBank&sProject=CashBankSeq&nHeight=300&sCodisp=OP002&sWindowDescript=Depósitos bancarios&nWindowTy=1&sLinkSpecial=1&nOptDeposit=2' + sParameters, 'CashBank', 800, 700, 'yes', 'yes');    
	    windows['CashBank'].moveTo(0, 0);
	    windows['CashBank'].resizeTo(window.screen.availWidth, window.screen.availHeight);
	    }
    else{
	    alert('<%=lstrMessage%>');
	    }
	}    
else{ 
    ShowPopUp('/VTimeNet/Common/SecWHeader.aspx?sCodispl=CO001_K&sModule=Collection&sProject=CollectionSeq&sConfig=&LoadWithAction=&nAction=304&nHeight=170&sLinkSpecial=1&nBordereaux=' + nBordereaux, 'Collection', 800, 700, 'yes', 'yes');    
    windows['Collection'].moveTo(0, 0);
    windows['Collection'].resizeTo(window.screen.availWidth, window.screen.availHeight);
    }
}
    
</SCRIPT>
<%
With Response
	.Write(mobjValues.StyleSheet() & vbCrLf)
	.Write(mobjValues.WindowsTitle("OPC001"))
	.Write(mobjMenu.setZone(2, "OPC001", "OPC001.aspx"))
End With
mobjMenu = Nothing
%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="post" ID="FORM" NAME="fraContent" ACTION="ValCashBank.aspx?Zone=1">
<%Response.Write(mobjValues.ShowWindowsName("OPC001"))%>
</FORM>
</BODY>
</HTML>

<%
Call insDefineHeader()
Call insPreOPC001()
mobjValues = Nothing
mobjGrid = Nothing
%>







