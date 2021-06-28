<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eClaim" %>
<script language="VB" runat="Server">
'^Begin Header Block VisualTimer Utility 1.1 7/4/03 11.34.13
Dim mobjNetFrameWork As eNetFrameWork.Layout
'~End Header Block VisualTimer Utility

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values
Dim mobjMenu As eFunctions.Menues
Dim mobjGrid As eFunctions.Grid
Dim lclsT_PayCla As Object
Dim lcolT_PayClas As Object


'% insDefineHeader: Se definen los campos del grid
'--------------------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'--------------------------------------------------------------------------------------------
	'+ Se definen las columnas del grid
	With mobjGrid.Columns
		Call .AddNumericColumn(0, "Siniestro", "tcnClaim", 10, "",  , "Número del siniestro a indemnizar",  ,  ,  ,  ,  , True)
		Call .AddClientColumn(0, "Beneficiario", "valBenef", "",  , "RUT (código del cliente) del beneficiario de la renta",  , True, "lblCliename")
		Call .AddClientColumn(0, "Titular de la orden de pago", "valClient_Rep", "",  , "RUT (código del cliente) del representante legal de beneficiario",  , True)
		Call .AddDateColumn(0, "Fecha próximo pago", "tcdNext_Pay", "",  , "Fecha en que corresponde pagar la renta",  ,  ,  , True)
		If Request.QueryString("Type") = "PopUp" Then
			Call .AddPossiblesColumn(0, "Forma de pago", "cbePayForm", "Table138", eFunctions.Values.eValuesType.clngComboType, "",  ,  ,  ,  ,  , False,  , "Forma de pagar la renta")
			mobjGrid.Columns("cbePayForm").TypeList = 1
			mobjGrid.Columns("cbePayForm").List = "1,4,8,9"
		End If
		Call .AddPossiblesColumn(0, "Moneda", "cbeCurrency", "Table11", eFunctions.Values.eValuesType.clngComboType, "",  ,  ,  ,  ,  , True,  , "Moneda en que está expresada la renta (Origen de la póliza)")
		Call .AddNumericColumn(0, "Monto de renta", "tcnAmount", 18, "",  , "Monto a indemnizar en moneda origen de la póliza.", True, 6,  ,  ,  , True)
		Call .AddNumericColumn(0, "Monto a pagar", "tcnAmountPay", 18, "",  , "Monto a real a pagar en moneda origen de la póliza.", True, 6,  ,  , "ChangeValues(this);", False)
		If Request.QueryString("Type") = "PopUp" Then
			Call .AddPossiblesColumn(0, "Moneda de pago", "cbeCurrencyPay", "Table11", eFunctions.Values.eValuesType.clngComboType, "",  ,  ,  ,  , "ChangeValues(this);",  ,  , "Moneda de pago.")
		End If
		'UPGRADE_WARNING: Date was upgraded to Today and has a new behavior. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1041.aspx'
		Call .AddDateColumn(0, "Fecha de valorización", "tcdValdate", CStr(Today),  , "Fecha para la conversión de la moneda del pago a la moneda local",  ,  , "ChangeValues(this);")
		Call .AddNumericColumn(0, "Factor de cambio", "tcnExchange", 11, CStr(0),  , "Indica el factor de cambio a utilizar para convertir el importe en la moneda del pago a la moneda local", True, 6,  ,  ,  , True)
		Call .AddNumericColumn(0, "Monto neto (Moneda de pago)", "tcnAmountPayCurrPay", 18, CStr(0),  , "Monto correspondiente al pago en la moneda seleccionada.", True, 6,  ,  , "ChangeValues(this);", False)
		
		'UPGRADE_WARNING: Date was upgraded to Today and has a new behavior. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1041.aspx'
		Call .AddDateColumn(0, "Fecha efectiva del pago", "tcdPaydate", CStr(Today),  , "Fecha en la que debe hacerse efectivo el pago.",  ,  , "ChangeValues(this);")
		Call .AddPossiblesColumn(0, "Destino del cheque", "cbeOffice", "Table9", eFunctions.Values.eValuesType.clngComboType,  ,  ,  ,  ,  , "BlankOfficeDepend();insInitialAgency(1,0)",  ,  , "Sucursal donde se registra el pago.")
		Call .AddPossiblesColumn(0, "Oficina", "cbeOfficeAgen", "TabAgencies_T5556", eFunctions.Values.eValuesType.clngWindowType,  , True,  ,  ,  , "insInitialAgency(2,0)",  ,  , "Oficina donde se registra el pago.")
		Call .AddPossiblesColumn(0, "Agencia", "cbeAgency", "TabAgencies_T5555", eFunctions.Values.eValuesType.clngWindowType,  , True,  ,  ,  , "insInitialAgency(3,0)",  ,  , "Agencia donde se registra el pago.")
		
		With mobjGrid
			.Columns("cbeOfficeAgen").Parameters.Add("nOfficeAgen", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Columns("cbeOfficeAgen").Parameters.Add("nAgency", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Columns("cbeOfficeAgen").Parameters.ReturnValue("nBran_off",  ,  , True)
			
			.Columns("cbeAgency").Parameters.Add("nOfficeAgen", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Columns("cbeAgency").Parameters.Add("nAgency", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Columns("cbeAgency").Parameters.ReturnValue("nBran_off",  ,  , True)
			.Columns("cbeAgency").Parameters.ReturnValue("nOfficeAgen",  ,  , True)
			.Columns("cbeAgency").Parameters.ReturnValue("sDesAgen",  ,  , True)
		End With
		Call .AddHiddenColumn("nClaim", "")
		Call .AddHiddenColumn("nCase_num", "")
		Call .AddHiddenColumn("nDeman_type", "")
		Call .AddHiddenColumn("sClient", "")
		Call .AddHiddenColumn("nId", "")
		Call .AddHiddenColumn("nOfficeAgen", "")
		Call .AddHiddenColumn("nAgency", "")
		Call .AddHiddenColumn("sPrint", "")
	End With
	
	
	
	'+ Se definen las propiedades generales del grid
	With mobjGrid
		.Width = 725
		.Height = 525
		.Codispl = "SI773"
		.DeleteButton = False
		.AddButton = False
		.Top = 210
		.Left = 150
		.FieldsByRow = 2
		.ActionQuery = Session("bQuery")
		.bCheckVisible = False
		.Columns("Sel").GridVisible = CDbl(Session("nProcess")) = 1
		.Columns("Sel").OnClick = "insSelected(this);"
		
		If Request.QueryString("Reload") = "1" Then
			.sReloadIndex = Request.QueryString("ReloadIndex")
		End If
	End With
End Sub

'% insPreSI773: Se cargan los controles de la página
'--------------------------------------------------------------------------------------------
Private Sub insPreSI773()
	'--------------------------------------------------------------------------------------------
	Dim lclsT_PayCla As eClaim.T_PayCla
	lclsT_PayCla = New eClaim.T_PayCla
	
	lcolT_PayClas = New eClaim.T_PayClas
	
	If lcolT_PayClas.FindSI773(Session("nBranch"), Session("nProduct"), Session("nClaim"), Session("dStartDate"), Session("dEndDate")) Then
		For	Each lclsT_PayCla In lcolT_PayClas
			With mobjGrid
				.Columns("tcnClaim").DefValue = CStr(lclsT_PayCla.nClaim)
				.Columns("valBenef").DefValue = lclsT_PayCla.sBenef
				If lclsT_PayCla.sClient_Rep <> vbNullString Then
					.Columns("valClient_Rep").DefValue = lclsT_PayCla.sClient_Rep
				Else
					.Columns("valClient_Rep").DefValue = lclsT_PayCla.sBenef
				End If
				.Columns("tcdNext_Pay").DefValue = CStr(lclsT_PayCla.dNext_Pay)
				.Columns("cbeCurrency").DefValue = CStr(lclsT_PayCla.nCover_curr)
				.Columns("tcnAmount").DefValue = CStr(lclsT_PayCla.nPay_amount)
				.Columns("tcnAmountPay").DefValue = CStr(lclsT_PayCla.nPay_amount)
				.Columns("nClaim").DefValue = CStr(lclsT_PayCla.nClaim)
				.Columns("nCase_num").DefValue = CStr(lclsT_PayCla.nCase_num)
				.Columns("nDeman_type").DefValue = CStr(lclsT_PayCla.nDeman_type)
				.Columns("sClient").DefValue = lclsT_PayCla.sBenef
				.Columns("nId").DefValue = CStr(lclsT_PayCla.nId)
				.Columns("cbeOffice").DefValue = CStr(lclsT_PayCla.nOffice_pay)
				With mobjGrid.Columns("cbeOfficeAgen").Parameters
					.Add("nOfficeAgen", lclsT_PayCla.nOffice_pay, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Add("nAgency", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				End With
				.Columns("cbeOfficeAgen").DefValue = CStr(lclsT_PayCla.nOfficeAgen_pay)
				With mobjGrid.Columns("cbeAgency").Parameters
					.Add("nOfficeAgen", lclsT_PayCla.nOffice_pay, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Add("nAgency", lclsT_PayCla.nOfficeAgen_pay, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				End With
				.Columns("cbeAgency").DefValue = CStr(lclsT_PayCla.nAgency_pay)
				.Columns("tcdValdate").GridVisible = False
				.Columns("tcnExchange").GridVisible = False
				.Columns("tcnAmountPayCurrPay").GridVisible = False
				.Columns("nOfficeAgen").DefValue = CStr(lclsT_PayCla.nOfficeAgen_pay)
				.Columns("nAgency").DefValue = CStr(lclsT_PayCla.nAgency_pay)
			End With
			Response.Write(mobjGrid.DoRow())
		Next lclsT_PayCla
	End If
	Response.Write(mobjGrid.closeTable())
	If lcolT_PayClas.Count > 0 Then
Response.Write("" & vbCrLf)
Response.Write("		<BR>" & vbCrLf)
Response.Write("		<TABLE ALIGN=""CENTER"">" & vbCrLf)
Response.Write("			<TR><TD ALIGN=""CENTER"" COLSPAN=1>	" & vbCrLf)
Response.Write("				<P>" & vbCrLf)
Response.Write("				<TR>" & vbCrLf)
Response.Write("					<TD COLSPAN=""1"" CLASS=""HighLighted""><LABEL ID=40202><A NAME=""Listado"">Listado</A></LABEL></TD>" & vbCrLf)
Response.Write("				</TR>" & vbCrLf)
Response.Write("				<TR>" & vbCrLf)
Response.Write("					<TD COLSPAN=""1"" CLASS=""Horline""></TD>" & vbCrLf)
Response.Write("				</TR>" & vbCrLf)
Response.Write("				<TR>" & vbCrLf)
Response.Write("					<TD>")


Response.Write(mobjValues.CheckControl("chkPrint", "Imprimir listado", CStr(2),  , "ChangeValues(this);"))


Response.Write("</TD>" & vbCrLf)
Response.Write("				</TR> 	" & vbCrLf)
Response.Write("			</TABLE>	" & vbCrLf)
Response.Write(" ")

		
		Response.Write(mobjValues.HiddenControl("hddPrint", "2"))
		Response.Write(mobjValues.BeginPageButton)
	End If
	'UPGRADE_NOTE: Object lcolT_PayClas may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
	lcolT_PayClas = Nothing
End Sub
'----------------------------------------------------------------------------------------------
Private Sub insPreSI773Upd()
	'----------------------------------------------------------------------------------------------
	With Request
		Response.Write(mobjGrid.DoFormUpd(.QueryString("Action"), "ValClaim.aspx", "SI773", .QueryString("nMainAction"), mobjValues.ActionQuery, .QueryString("Index")))
		Response.Write("<SCRIPT>self.document.forms[0].elements['sPrint'].value = top.opener.document.forms[0].elements['hddPrint'].value;</" & "Script>")
	End With
End Sub

</script>
<%Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage("si773")

mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 11.34.13
mobjValues.sSessionID = Session.SessionID
mobjValues.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjValues.sCodisplPage = "si773"
mobjGrid = New eFunctions.Grid
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 11.34.13
mobjGrid.sSessionID = Session.SessionID
mobjGrid.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjGrid.sCodisplPage = "si773"
Call mobjGrid.SetWindowParameters(Request.QueryString("sCodispl"), Request.QueryString("sWindowDescript"), Request.QueryString("nWindowTy"))
mobjMenu = New eFunctions.Menues
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 11.34.13
mobjMenu.sSessionID = Session.SessionID
mobjMenu.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility
%>
<HTML>
<HEAD>
	<META NAME = "GENERATOR" CONTENT = "Microsoft Visual Studio 6.0">
	<%'UPGRADE_NOTE: Language element 'SCRIPT' was migrated to the same language element but still may have a different behavior. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1011.htm  %>
<%'UPGRADE_NOTE: The file '/VTimeNet/Scripts/GenFunctions.js' was not found in the migration directory. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1003.htm  %>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
	<%'UPGRADE_NOTE: Language element 'SCRIPT' was migrated to the same language element but still may have a different behavior. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1011.htm  %>
<%'UPGRADE_NOTE: The file '/VTimeNet/Scripts/Claim.js' was not found in the migration directory. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1003.htm  %>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/Claim.js"></SCRIPT>
	<%'UPGRADE_NOTE: Language element '#INCLUDE' was migrated to the same language element but still may have a different behavior. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1011.htm  %>
<%'UPGRADE_NOTE: The file 'VTime/Includes/Constantes.aspx' was not found in the migration directory. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1003.htm  %>
<!-- #INCLUDE VIRTUAL="~/VTimeNet/Includes/Constantes.aspx" -->

<%
With Response
	If Request.QueryString("Type") <> "PopUp" Then
		'   			.Write mobjMenu.setZone(2,"SI773", Request.QueryString("sWindowDescript"), Request.QueryString("nWindowTy"))
		.Write(mobjMenu.setZone(2, "SI773", "SI773.aspx"))
		'UPGRADE_NOTE: Object mobjMenu may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
		mobjMenu = Nothing
		Response.Write("<SCRIPT>var nMainAction=top.frames['fraSequence'].plngMainAction</SCRIPT>")
	End If
	.Write(mobjValues.StyleSheet())
	.Write(mobjValues.WindowsTitle("SI773"))
	
	'	    .Write  mobjValues.StyleSheet()
	'    	    .Write  mobjValues.WindowsTitle("SI773", Request.QueryString("sWindowDescript"))
	'	    .Write "<NOTSCRIPT>var nMainAction=" & Request.QueryString("nMainAction") & "</SCRIPT>"
End With

%>
<SCRIPT>   
// Version soursafe 
    document.VssVersion="$$Revision: 8 $|$$Date: 6/08/04 13:05 $|$$Author: Nvaplat9 $"

//%insSelected: Hace el llamado a la Popup cuando se selecciona el registro
//---------------------------------------------------------------------------
function insSelected(Field){
//---------------------------------------------------------------------------

    var lstrQueryString = '&sPrint=' + self.document.forms[0].hddPrint.value
    if (Field.checked){
        EditRecord(Field.value, nMainAction, 'Update', lstrQueryString)
        Field.checked = !Field.checked
	}
}

//%	ChangeValues: se realiza los cambios en los controles dependientes
//-------------------------------------------------------------------------------------------
function ChangeValues(Field){
//-------------------------------------------------------------------------------------------
    var lstrQString

	switch(Field.name)
	{
		case "tcnAmountPay":
			with(self.document.forms[0]){
				if(Field.value=="" || Field.value=="0"){
					tcnAmountPayCurrPay.value = "0.000000"
					tcnAmount.value = "0.000000"
				}
				else {
//+	Se calcula el monto	con	el porcentaje de impuesto
					lstrQString = 'nAmount=' + Field.value + '&nCurrency=' + cbeCurrencyPay.value + 
					              '&nPolicyCurr=' + cbeCurrency.value + 
					              '&dValdate=' + tcdValdate.value
					insDefValues('AmountPay',lstrQString,'/VTimeNet/Claim/Claim');
				}
			}
			break;			
		case "tcnAmountPayCurrPay":
		    with(self.document.forms[0]){
				if(Field.value=="" || Field.value=="0"){
						tcnAmountPay.value = "0.000000"
						tcnAmount.value = "0.000000"
				}
				else{
					lstrQString = 'nAmount=' + Field.value + 
								  '&nCurrency=' + cbeCurrencyPay.value + 
					              '&nPolicyCurr=' + cbeCurrency.value + 
					              '&dValdate=' + tcdValdate.value
					insDefValues('tcnAmountPayCurrPay',lstrQString,'/VTimeNet/Claim/Claim');				
				}
			}
		    break;			
		case "cbeCurrencyPay":	//+	Moneda de pago
			with(self.document.forms[0]){
				if(cbeCurrency.value==0){
					tcnExchange.value=VTFormat('0','','','',6);					
				}
				else {
//+ Se calcula el factor de cambio				        
				    lstrQString = 'nAmount=' + tcnAmountPay.value + '&nCurrency=' + Field.value +
 				                  '&nPolicyCurr=' + cbeCurrency.value +
				                  '&dValdate=' + tcdValdate.value;
                    insDefValues('AmountPay',lstrQString ,'/VTimeNet/Claim/Claim');
				}	
			}
			break;
			
		case "tcdValdate":	//+	Fecha de valorización
			with(self.document.forms[0]){
				    lstrQString = 'nAmount=' + tcnAmountPay.value + '&nCurrency=' + cbeCurrencyPay.value +
 				                  '&nPolicyCurr=' + cbeCurrency.value +
				                  '&dValdate=' + tcdValdate.value +
				                  '&dValdate=' + Field.value;
                    insDefValues('AmountPay',lstrQString ,'/VTimeNet/Claim/Claim');
			}		
			break;
		case "chkPrint":	//+	Check de Impresión
			with(self.document.forms[0]){
				if(chkPrint.checked)
					hddPrint.value = 1
				else
					hddPrint.value = 2;
			}
	}
}
</SCRIPT>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" ID="FORM" NAME="SI773" ACTION="ValClaim.aspx?x=1&nTransacio=SI774">
<%
Response.Write(mobjValues.ShowWindowsName("SI773", Request.QueryString("sWindowDescript")))
Call insDefineHeader()
If Request.QueryString("Type") = "PopUp" Then
	Call insPreSI773Upd()
Else
	Call insPreSI773()
End If
'UPGRADE_NOTE: Object mobjValues may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
mobjValues = Nothing
'UPGRADE_NOTE: Object mobjGrid may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
mobjGrid = Nothing

%>
</FORM>
</BODY>
</HTML>

   

<%'^Begin Footer Block VisualTimer Utility 1.1 7/4/03 11.34.13
Call mobjNetFrameWork.FinishPage("si773")
'UPGRADE_NOTE: Object mobjNetFrameWork may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>





