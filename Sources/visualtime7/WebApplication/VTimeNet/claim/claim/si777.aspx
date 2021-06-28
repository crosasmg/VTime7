<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eClaim" %>
<script language="VB" runat="Server">
'^Begin Header Block VisualTimer Utility 1.1 7/4/03 11.34.48
Dim mobjNetFrameWork As eNetFrameWork.Layout
'~End Header Block VisualTimer Utility

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values
Dim mobjMenu As eFunctions.Menues
Dim mobjGrid As eFunctions.Grid


'% insDefineHeader: Se definen los campos del grid
'--------------------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'--------------------------------------------------------------------------------------------
	'+ Se definen las columnas del grid
	With mobjGrid.Columns
		Call .AddNumericColumn(0, "Siniestro", "tcnClaimNumber", 10, "",  ,  ,  ,  ,  ,  ,  , False)
		Call .AddCheckColumn(0, "Autorizar", "chkApproval", "")
		If mobjValues.StringToType(CStr(Session("nStatus_Payment")), eFunctions.Values.eTypeData.etdDouble) <> 1 Then
			mobjGrid.Columns("chkApproval").GridVisible = False
		Else
			mobjGrid.Columns("chkApproval").GridVisible = True
		End If
		Call .AddTextColumn(0, "Movimiento", "tctMovement", 35, "",  , "Número y descripción del movimiento de pago",  ,  ,  , False)
		Call .AddNumericColumn(0, "Monto de la orden de pago", "tcnAmountOrder", 18, "",  ,  , True, 6,  ,  ,  , False)
		Call .AddPossiblesColumn(0, "Moneda", "cbeCurrency", "Table11", eFunctions.Values.eValuesType.clngWindowType, CStr(0), False,  ,  ,  ,  , False,  , "Moneda en la que se realizó el pago")
		'Call .AddTextColumn (0,"Destino del cheque","cbeDestiny",vbNullString,, "Descripción del repuesto solicitado",,True)
		Call .AddTextColumn(0, "Destino del cheque", "cbeDestiny", 50, "",  , "Descripción del repuesto solicitado",  ,  ,  , False)
		Call .AddNumericColumn(0, "Factor de cambio", "tcnExchange", 11, "",  , "Factor de cambio indicado en la orden de pago, asociada al movimiento y siniestro en tratatmiento", True, 6,  ,  ,  , True)
		'UPGRADE_WARNING: Date was upgraded to Today and has a new behavior. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1041.aspx'
		Call .AddDateColumn(0, "Fecha del movimiento de pago", "tcdMovementDate", CStr(Today))
		'UPGRADE_WARNING: Date was upgraded to Today and has a new behavior. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1041.aspx'
		Call .AddDateColumn(0, "Fecha efectiva de pago", "tcdDeclarationDate", CStr(Today))
		Call .AddPossiblesColumn(0, "Sucursal", "cbeOffice", "Table9", eFunctions.Values.eValuesType.clngWindowType, CStr(0), False,  ,  ,  ,  , False,  , "Descripción del repuesto solicitado")
		Call .AddNumericColumn(0, "Póliza", "tcnPolicyNumber", 10, "",  ,  ,  ,  ,  ,  ,  , False)
		Call .AddNumericColumn(0, "Item", "tcnCertificatNumber", 10, "",  ,  ,  ,  ,  ,  ,  , False)
		Call .AddClientColumn(0, "Titular de la orden de pago", "cbeClient", vbNullString,  , "Nombre del Titular de la orden de pago",  , True)

		Call .AddHiddenColumn("tcnAmountOrder_AUX", CStr(0))
		Call .AddHiddenColumn("tcnChecked", CStr(1))
		Call .AddHiddenColumn("tcnClaimNumber_AUX", CStr(0))
        Call .AddHiddenColumn("tcnMovementNumber_AUX", CStr(0))
            Call .AddNumericColumn(0, "Número de orden de pago", "tcnMovementNumber_AUX2", 10, "", , , True, 0, , , , True)
		
            
            Call .AddHiddenColumn("tctCheque", "")
        Call .AddHiddenColumn("tcnConsecutive", CStr(0))
		Call .AddHiddenColumn("tcnTransactio", CStr(0))
		Call .AddHiddenColumn("hdsSel", CStr(1))
            
	End With
	'+ Se definen las propiedades generales del grid
	With mobjGrid
		.Width = 500
		.Height = 430
		.Codispl = "SI777"
		.DeleteButton = False
		.AddButton = False
		.Top = 50
		.ActionQuery = Session("bQuery")
		If Request.QueryString("Reload") = "1" Then
			.sReloadIndex = Request.QueryString("ReloadIndex")
		End If
            
		If mobjValues.StringToType(CStr(Session("nStatus_Payment")), eFunctions.Values.eTypeData.etdDouble) = 3 Then
			.Columns("Sel").Disabled = False
			.Columns("Sel").GridVisible = True
		Else
			.Columns("Sel").Disabled = True
			.Columns("Sel").GridVisible = False
		End If
	End With
End Sub

'% insPreSI777: Se cargan los controles de la página
'--------------------------------------------------------------------------------------------
Private Sub insPreSI777()
	'--------------------------------------------------------------------------------------------
	Dim lclsClaim_his As eClaim.Claim_his
	Dim lintCount As Integer
	Dim nAmountPayCorr As Double
	Dim nPolicyAux As Object
	Dim lintTotAmount As Double
	Dim lintTotAmount_Orig As Double
	Dim lintCurrencyO As Integer
	
	
	lclsClaim_his = New eClaim.Claim_his
	
	lintTotAmount = 0
	lintTotAmount_Orig = 0
	
	
Response.Write("" & vbCrLf)
Response.Write("	 <TABLE WIDTH=""100%"">" & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("            <TD COLSPAN=""5"">&nbsp;</TD>" & vbCrLf)
Response.Write("        </TR>    " & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("	        <TD><LABEL ID=0>Total </LABEL></TD>" & vbCrLf)
Response.Write("            <TD>")


Response.Write(mobjValues.NumericControl("tcnTotalAmount", 18,  ,  , "Monto total a pagar.", True))


Response.Write("</TD>" & vbCrLf)
Response.Write("            <TD>&nbsp;</TD>" & vbCrLf)
Response.Write("            <TD>&nbsp;</TD> " & vbCrLf)
Response.Write("            <TD>&nbsp;</TD>" & vbCrLf)
Response.Write("        </TR>    " & vbCrLf)
Response.Write("       	<TR>" & vbCrLf)
Response.Write("            <TD COLSPAN=""5"">&nbsp;</TD>" & vbCrLf)
Response.Write("        </TR> " & vbCrLf)
Response.Write("     </TABLE>   " & vbCrLf)
Response.Write("    ")

	
	
	If lclsClaim_his.Find_SI777(mobjValues.StringToType(CStr(Session("nBranch")), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(CStr(Session("nProduct")), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(CStr(Session("nPolicy")), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(CStr(Session("nStatus_Payment")), eFunctions.Values.eTypeData.etdDouble), CDate(Session("dInitial_date")), Session("dFinal_date"), CStr(Session("sClient"))) Then
		If Trim(CStr(Session("nAmountAp"))) <> "" Then
			nAmountPayCorr = 0
			For lintCount = 1 To lclsClaim_his.CountSI777
				nAmountPayCorr = lclsClaim_his.nAmountPay + nAmountPayCorr
			Next 
		End If
		For lintCount = 1 To lclsClaim_his.CountSI777
			If lclsClaim_his.ItemSI777(lintCount) Then
				With mobjGrid
					If CStr(Session("nPolicy")) = "" Then
						If CDbl(Session("nAmountAp")) <= lclsClaim_his.nAmountPay Or Trim(CStr(Session("nAmountAp"))) = "" Then
							.Columns("tcnClaimNumber").DefValue = CStr(lclsClaim_his.nClaim)
							.Columns("tctMovement").DefValue = lclsClaim_his.sMovement
							.Columns("tcnAmountOrder").DefValue = CStr(lclsClaim_his.nAmountPay)
							.Columns("cbeCurrency").DefValue = CStr(lclsClaim_his.nCurrency)
							.Columns("cbeDestiny").DefValue = lclsClaim_his.sDest_Cheque
							.Columns("tcdMovementDate").DefValue = CStr(lclsClaim_his.dOperdate)
							.Columns("tcdDeclarationDate").DefValue = CStr(lclsClaim_his.dDecladat)
							.Columns("cbeOffice").DefValue = CStr(lclsClaim_his.nOffice)
							.Columns("tcnPolicyNumber").DefValue = CStr(lclsClaim_his.nPolicy)
							.Columns("tcnCertificatNumber").DefValue = CStr(lclsClaim_his.nCertif)
							.Columns("cbeClient").DefValue = lclsClaim_his.sClient
							.Columns("tcnAmountOrder_AUX").DefValue = CStr(lclsClaim_his.nAmountPay)
							'.Columns("chkApproval").OnClick = "insSelected(this.checked, " & lintCount-1 & " ," & Cstr(lclsClaim_his.nAmountPay) & "," & Cstr(lclsClaim_his.nAmountPay_Orig) & ")"
							.Columns("chkApproval").OnClick = "insSelected(this.checked, " & lintCount - 1 & " ," & CStr(lclsClaim_his.nAmountPay) & ")"
							.Columns("chkApproval").Checked = 1
							.Columns("Sel").OnClick = "insClick(this.checked, " & lintCount - 1 & " ," & CStr(lclsClaim_his.nAmountPay) & "," & CStr(lclsClaim_his.nAmountPay_Orig) & ")"
							.Columns("Sel").Checked = 1
                            .Columns("hdsSel").Checked = 1
                            .Columns("hdsSel").DefValue = "1"
							.Columns("tcnClaimNumber_AUX").DefValue = CStr(lclsClaim_his.nClaim)
							.Columns("tcnMovementNumber_AUX").DefValue = CStr(lclsClaim_his.nRequest_nu)
                            .Columns("tcnMovementNumber_AUX2").DefValue = CStr(lclsClaim_his.nRequest_nu)

                                .Columns("tcnConsecutive").DefValue = CStr(lclsClaim_his.nConsec)
							.Columns("tctCheque").DefValue = lclsClaim_his.sCheque
							.Columns("tcnTransactio").DefValue = CStr(lclsClaim_his.nTransac)
							.Columns("tcnExchange").DefValue = CStr(lclsClaim_his.nExchange)
                                
							lintTotAmount = lintTotAmount + lclsClaim_his.nAmountPay
							lintTotAmount_Orig = lintTotAmount_Orig + lclsClaim_his.nAmountPay_Orig
							lintCurrencyO = lclsClaim_his.nCurrency_Orig
							Response.Write(mobjGrid.DoRow())
							
						End If
					Else
						If CDbl(Session("nAmountAp")) <= nAmountPayCorr Or Trim(CStr(Session("nAmountAp"))) <> "" Then
							.Columns("tcnClaimNumber").DefValue = CStr(lclsClaim_his.nClaim)
							.Columns("tctMovement").DefValue = lclsClaim_his.sMovement
							.Columns("tcnAmountOrder").DefValue = CStr(lclsClaim_his.nAmountPay)
							.Columns("cbeCurrency").DefValue = CStr(lclsClaim_his.nCurrency)
							.Columns("cbeDestiny").DefValue = lclsClaim_his.sDest_Cheque
							.Columns("tcdMovementDate").DefValue = CStr(lclsClaim_his.dOperdate)
							.Columns("tcdDeclarationDate").DefValue = CStr(lclsClaim_his.dDecladat)
							.Columns("cbeOffice").DefValue = CStr(lclsClaim_his.nOffice)
							.Columns("tcnPolicyNumber").DefValue = CStr(lclsClaim_his.nPolicy)
							.Columns("tcnCertificatNumber").DefValue = CStr(lclsClaim_his.nCertif)
							.Columns("cbeClient").DefValue = lclsClaim_his.sClient
							.Columns("tcnAmountOrder_AUX").DefValue = CStr(lclsClaim_his.nAmountPay)
							'.Columns("chkApproval").OnClick = "insSelected(this.checked, " & lintCount-1 & "," & Cstr(lclsClaim_his.nAmountPay) & "," & Cstr(lclsClaim_his.nAmountPay_Orig) & ")"
							.Columns("chkApproval").OnClick = "insSelected(this.checked, " & lintCount - 1 & "," & CStr(lclsClaim_his.nAmountPay) & ")"
							.Columns("chkApproval").Checked = 1
							.Columns("Sel").OnClick = "insClick(this.checked, " & lintCount - 1 & " ," & CStr(lclsClaim_his.nAmountPay) & "," & CStr(lclsClaim_his.nAmountPay_Orig) & ")"
							.Columns("Sel").Checked = 1
                            .Columns("hdsSel").Checked = 1
                            .Columns("hdsSel").DefValue = "1"
							.Columns("tcnClaimNumber_AUX").DefValue = CStr(lclsClaim_his.nClaim)
							.Columns("tcnMovementNumber_AUX").DefValue = CStr(lclsClaim_his.nRequest_nu)
							.Columns("tcnConsecutive").DefValue = CStr(lclsClaim_his.nConsec)
							.Columns("tctCheque").DefValue = lclsClaim_his.sCheque
							.Columns("tcnTransactio").DefValue = CStr(lclsClaim_his.nTransac)
							.Columns("tcnExchange").DefValue = CStr(lclsClaim_his.nExchange)
                                
							lintTotAmount = lintTotAmount + lclsClaim_his.nAmountPay
							lintTotAmount_Orig = lintTotAmount_Orig + lclsClaim_his.nAmountPay_Orig
							lintCurrencyO = lclsClaim_his.nCurrency_Orig
							Response.Write(mobjGrid.DoRow())
						End If
					End If
				End With
			End If
			
		Next 
	End If
	Response.Write(mobjGrid.closeTable())
	Response.Write(mobjValues.BeginPageButton)
	
	
	Response.Write(mobjValues.HiddenControl("hddTotalAmountO", CStr(lintTotAmount_Orig)))
	Response.Write(mobjValues.HiddenControl("hddCurrencyO", CStr(lintCurrencyO)))
	Response.Write("<SCRIPT>TotAmountPay(" & CStr(lintTotAmount) & " )</" & "Script>")
	
	'UPGRADE_NOTE: Object lclsClaim_his may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
	lclsClaim_his = Nothing
	'UPGRADE_NOTE: Object lintCount may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
	lintCount = Nothing
End Sub
'----------------------------------------------------------------------------------------------
Private Sub insPreSI777Upd()
	'----------------------------------------------------------------------------------------------
	With Request
		Response.Write(mobjGrid.DoFormUpd(.QueryString("Action"), "ValClaim_ACM.aspx", "SI777", .QueryString("nMainAction"), mobjValues.ActionQuery, .QueryString("Index")))
	End With
End Sub

</script>
<%Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage("si777")

mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 11.34.48
mobjValues.sSessionID = Session.SessionID
mobjValues.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjValues.sCodisplPage = "si777"
mobjGrid = New eFunctions.Grid
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 11.34.48
mobjGrid.sSessionID = Session.SessionID
mobjGrid.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjGrid.sCodisplPage = "si777"
Call mobjGrid.SetWindowParameters(Request.QueryString("sCodispl"), Request.QueryString("sWindowDescript"), Request.QueryString("nWindowTy"))
mobjMenu = New eFunctions.Menues
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 11.34.48
mobjMenu.sSessionID = Session.SessionID
mobjMenu.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility
%>
<%'UPGRADE_NOTE: Language element 'SCRIPT' was migrated to the same language element but still may have a different behavior. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1011.htm  %>
<%'UPGRADE_NOTE: The file '/VTimeNet/Scripts/GenFunctions.js' was not found in the migration directory. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1003.htm  %>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<%'UPGRADE_NOTE: Language element '#INCLUDE' was migrated to the same language element but still may have a different behavior. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1011.htm  %>
<%'UPGRADE_NOTE: The file 'VTime/Includes/Constantes.aspx' was not found in the migration directory. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1003.htm  %>
<!-- #INCLUDE VIRTUAL="~/VTimeNet/Includes/Constantes.aspx" -->

<SCRIPT>
    document.VssVersion="$$Revision: 3 $|$$Date: 30/10/03 10:17 $"
</SCRIPT>

<SCRIPT>
//% insSelected: Asigna valor a una columna oculta una vez que se presiona el checkbox de la columna SEL
//-------------------------------------------------------------------------------------
function insSelected(blnChecked, Field, Value){
//-------------------------------------------------------------------------------------
  var ldblValues = 0;
  var ldblAmount = 0;
  var ldblValues_Orig = 0;
    
  ldblValues = insConvertNumber(Value,'','', true);
    
    with (document.forms[0]){
		if (elements["tcnChecked"].length==-1 || 
			typeof(elements["tcnChecked"].length)=='undefined')
			{
			elements["tcnChecked"].value=(blnChecked)?1:2
		    }
		else
		   {
			elements["tcnChecked"][Field].value=(blnChecked)?1:2
		    
		   if(!blnChecked)
			{
			    ldblAmount = insConvertNumber(tcnTotalAmount.value,'','', true) - ldblValues;
			    tcnTotalAmount.value= ldblAmount
			    
			    ldblAmount_Orig = insConvertNumber(hddTotalAmountO.value,'','', true) - ldblValues_Orig;
		        hddTotalAmountO.value= ldblAmount_Orig
		      
		    }
		    else
		    {
		        ldblAmount = insConvertNumber(tcnTotalAmount.value,'','', true) + ldblValues;
		        tcnTotalAmount.value= ldblAmount

			    ldblAmount_Orig = insConvertNumber(hddTotalAmountO.value,'','', true) + ldblValues_Orig;
		        hddTotalAmountO.value= ldblAmount_Orig
		      
		    }
		    
		    
		   }
	}
}

//-------------------------------------------------------------------------------------
function TotAmountPay(Value){
//-------------------------------------------------------------------------------------
	
	
	with(self.document.forms[0]){
	     tcnTotalAmount.value=Value;
	     tcnTotalAmount.disabled = true;
	}     
		
}
</SCRIPT>

<SCRIPT>
//% insClick: Asigna valor a una columna oculta una vez que se presiona el sSel
//-------------------------------------------------------------------------------------
function insClick(blnChecked, Field, Value, Value2){
//-------------------------------------------------------------------------------------
var ldblAmount = 0;
var ldblValues = 0;
var ldblAmount_Orig = 0;
var ldblValues_Orig = 0;    

  ldblValues = insConvertNumber(Value,'','', true);
  ldblValues_Orig = insConvertNumber(Value2,'','', true);
    
       
    with (document.forms[0]){
		if (elements["hdsSel"].length==-1 || 
			typeof(elements["hdsSel"].length)=='undefined')
			elements["hdsSel"].value=(blnChecked)?1:2
		else
			{elements["hdsSel"][Field].value=(blnChecked)?1:2
			 
			if(!blnChecked)
			{
			    ldblAmount = insConvertNumber(tcnTotalAmount.value,'','', true) - ldblValues;
			    tcnTotalAmount.value= ldblAmount
			    
			    ldblAmount_Orig = insConvertNumber(hddTotalAmountO.value,'','', true) - ldblValues_Orig;
		        hddTotalAmountO.value= ldblAmount_Orig
		      
		    }
		    else
		    {
		        ldblAmount = insConvertNumber(tcnTotalAmount.value,'','', true) + ldblValues;
		        tcnTotalAmount.value= ldblAmount

			    ldblAmount_Orig = insConvertNumber(hddTotalAmountO.value,'','', true) + ldblValues_Orig;
		        hddTotalAmountO.value= ldblAmount_Orig
		      
		    }
		} 
	}
}
</SCRIPT>


<HTML>
<HEAD>
    <META NAME = "GENERATOR" CONTENT = "Microsoft Visual Studio 6.0">
<%
With Response
	If Request.QueryString("Type") <> "PopUp" Then
		.Write(mobjMenu.setZone(2, "SI777", Request.QueryString("sWindowDescript"), Request.QueryString("nWindowTy")))
		'UPGRADE_NOTE: Object mobjMenu may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
		mobjMenu = Nothing
		Response.Write("<SCRIPT>var nMainAction=304</SCRIPT>")
	End If
	.Write(mobjValues.StyleSheet())
	.Write(mobjValues.WindowsTitle("SI777", Request.QueryString("sWindowDescript")))
End With
%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="post" ID="FORM" NAME="SI774" ACTION="ValClaim.aspx?x=1&nTransacio=SI777">
<%
Response.Write(mobjValues.ShowWindowsName("SI777", Request.QueryString("sWindowDescript")))
%>
    <BR>
<%
Call insDefineHeader()
If Request.QueryString("Type") = "PopUp" Then
	Call insPreSI777Upd()
Else
	Call insPreSI777()
End If
'UPGRADE_NOTE: Object mobjValues may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
mobjValues = Nothing
'UPGRADE_NOTE: Object mobjGrid may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
mobjGrid = Nothing
%>
</FORM>
</BODY>
</HTML>

   

<%'^Begin Footer Block VisualTimer Utility 1.1 7/4/03 11.34.48
Call mobjNetFrameWork.FinishPage("si777")
'UPGRADE_NOTE: Object mobjNetFrameWork may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>




