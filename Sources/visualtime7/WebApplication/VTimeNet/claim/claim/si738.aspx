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

'- Objeto para el manejo del grid de la página
Dim mobjGrid As eFunctions.Grid

'- Objeto para el manejo del menú
Dim mobjMenu As eFunctions.Menues

Dim lclsOpt_sinies As eClaim.Opt_sinies
Dim lintOpt_cur As Integer


'% insDefineHeader: se definen las propiedades del grid
'--------------------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'--------------------------------------------------------------------------------------------
	mobjGrid = New eFunctions.Grid
	'^Begin Body Block VisualTimer Utility 1.1 7/4/03 11.34.13
	mobjGrid.sSessionID = Session.SessionID
	mobjGrid.nUsercode = Session("nUsercode")
	'~End Body Block VisualTimer Utility
	
	mobjGrid.sCodisplPage = "si738"
	Call mobjGrid.SetWindowParameters(Request.QueryString("sCodispl"), Request.QueryString("sWindowDescript"), Request.QueryString("nWindowTy"))
	
	'+ Se definen las columnas del grid    
	With mobjGrid.Columns
		Call .AddNumericColumn(0, "Siniestro", "tcnClaim", 10, "",  , "Número del siniestro a pagar, asociados a la relación.")
		Call .AddNumericColumn(0, "Monto Moneda Origen", "tcnNetAmount", 18, "0",  , "Número del siniestro a pagar, asociados a la relación.", True, 6)
		Call .AddPossiblesColumn(0, "Moneda", "cbeCurrencyOrig", "Table11", eFunctions.Values.eValuesType.clngComboType, CStr(0),  ,  ,  ,  ,  , True,  , vbNullString)
		Call .AddNumericColumn(0, "Monto Moneda Local", "tcnNetAmount_Loc", 18, "0",  , "Número del siniestro a pagar, asociados a la relación.", True)
		Call .AddHiddenColumn("tcnAuxClaim", CStr(0))
		Call .AddHiddenColumn("cbeAuxCurrencyOrig", CStr(0))
		Call .AddHiddenColumn("chkAuxStatus", CStr(1))
	End With
	
	'+ Se definen las propiedades generales del grid
	
	With mobjGrid
		.Codispl = "SI738"
		.ActionQuery = mobjValues.ActionQuery
		.Height = 350
		.Width = 280
		.nMainAction = Request.QueryString("nMainAction")
		.AddButton = False
		.DeleteButton = False
		.Columns("Sel").GridVisible = Not .ActionQuery
		If Request.QueryString("Reload") = "1" Then
			.sReloadIndex = Request.QueryString("ReloadIndex")
		End If
	End With
End Sub


'% insPreSI738: Se realiza el manejo del grid
'--------------------------------------------------------------------------------------------
Private Sub insPreSI738()
	'--------------------------------------------------------------------------------------------
	Dim lclsClaim As eClaim.Claim
	Dim lintIndex As Integer
	Dim lintIndex2 As Short
	Dim lintClaim As Double
	Dim lobjColumn As Object
	
	Dim lintTotAmount As Double
	
	lclsClaim = New eClaim.Claim
	
	lintIndex2 = 0
	lintTotAmount = 0
	
	
Response.Write("" & vbCrLf)
Response.Write("" & vbCrLf)
Response.Write("    <TABLE WIDTH=""100%"">" & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("            <TD COLSPAN=""5"">&nbsp;</TD>" & vbCrLf)
Response.Write("        </TR>    " & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("			<TD><LABEL ID=0>Tipo de pago</LABEL></TD>" & vbCrLf)
Response.Write("			<TD>")

	mobjValues.List = "2"
	mobjValues.TypeList = 1
	mobjValues.BlankPosition = False
	Response.Write(mobjValues.PossiblesValues("cbePayType", "Table199", eFunctions.Values.eValuesType.clngComboType, "",  ,  ,  ,  ,  ,  , True,  , "Tipo de pago que se realiza sobre el siniestro."))
Response.Write("</TD>   " & vbCrLf)
Response.Write("			<TD WIDTH=10%>&nbsp;</TD>" & vbCrLf)
Response.Write("			<TD><LABEL ID=0>Moneda</LABEL></TD>" & vbCrLf)
Response.Write("            <TD>")


Response.Write(mobjValues.PossiblesValues("cbeCurrency", "Table11", eFunctions.Values.eValuesType.clngComboType, CStr(lintOpt_cur),  ,  ,  ,  ,  , "ChangeValues(""Currency"",this);",  ,  , "Moneda en la que se realiza el pago."))


Response.Write(" </TD>" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("			<TD><LABEL ID=0>Fecha de valorización</LABEL></TD>" & vbCrLf)
Response.Write("            <TD>")


Response.Write(mobjValues.DateControl("tcdValdate", Request.QueryString("dPayDate"),  , "Fecha a tomar para la conversión de la moneda del pago a la moneda local.",  ,  ,  , "ChangeValues(""Currency"",this);"))


Response.Write(" </TD>" & vbCrLf)
Response.Write("			<TD>&nbsp;</TD>" & vbCrLf)
Response.Write("			<TD><LABEL ID=0>Factor de cambio</LABEL></TD>" & vbCrLf)
Response.Write("            <TD>")


Response.Write(mobjValues.NumericControl("tcnExchange", 14, "",  , "Factor para convertir el importe en la moneda del pago a la moneda local.", True, 6))


Response.Write("</TD>" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("			<TD><LABEL ID=0>Destino del cheque</LABEL></TD>" & vbCrLf)
Response.Write("			<TD>")


Response.Write(mobjValues.PossiblesValues("cbeOfficePay", "Table9", eFunctions.Values.eValuesType.clngComboType, "",  ,  ,  ,  ,  ,  ,  ,  , "Sucursal a donde debe ser enviado el cheque del pago del siniestro."))


Response.Write(" </TD>		" & vbCrLf)
Response.Write("		" & vbCrLf)
Response.Write("		    <TD>&nbsp;</TD>" & vbCrLf)
Response.Write("	        <TD><LABEL ID=0>Destinatario - RUT</LABEL></TD>" & vbCrLf)
Response.Write("	        <TD COLSPAN=""2"">")


Response.Write(mobjValues.ClientControl("tctClientCode", CStr(Session("SI738_sClientCont")),  , "Código del cliente RUT a nombre de quien se emite la orden de pago."))


Response.Write(" </TD>" & vbCrLf)
Response.Write("		</TR>" & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("		    <TD><LABEL ID=0>Tipo de beneficiarios</LABEL></TD>" & vbCrLf)
Response.Write("		    <TD>")


Response.Write(mobjValues.OptionControl(0, "optTypeBenef", "Único", CStr(1), "1", "ChangeValues(""TypeBenef"",this);"))


Response.Write(" </TD>		    		" & vbCrLf)
Response.Write("			<TD>&nbsp;</TD>		    " & vbCrLf)
Response.Write("			<TD><LABEL ID=0>Forma de pago</LABEL></TD>" & vbCrLf)
Response.Write("			<TD>")

	mobjValues.List = "9"
	mobjValues.TypeList = 1
	mobjValues.BlankPosition = False
	Response.Write(mobjValues.PossiblesValues("cbeWayPay", "Table138", eFunctions.Values.eValuesType.clngComboType, "",  ,  ,  ,  ,  ,  , True,  , "Forma del pago que se quiere realizar."))
Response.Write(" </TD>				    " & vbCrLf)
Response.Write("		</TR>" & vbCrLf)
Response.Write("		<TR>			" & vbCrLf)
Response.Write("			<TD>&nbsp;</TD>			" & vbCrLf)
Response.Write("			<TD>")


Response.Write(mobjValues.OptionControl(0, "optTypeBenef", "Varios",  , "2", "ChangeValues(""TypeBenef"",this);"))


Response.Write(" </TD>" & vbCrLf)
Response.Write("			<TD>&nbsp;</TD>" & vbCrLf)
Response.Write("			<TD><LABEL ID=0>Total a pagar</LABEL></TD>" & vbCrLf)
Response.Write("            <TD>")


Response.Write(mobjValues.NumericControl("tcnTotalAmount", 18, CStr(lintTotAmount),  , "Monto total a pagar, correspondiente a la sumatoria de los totales a pagar de cada siniestro.", True))


Response.Write("</TD>" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("            <TD COLSPAN=""5"">&nbsp;</TD>" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("    </TABLE>    ")

	
	If lclsClaim.FindClaimByBordereaux(mobjValues.StringToType(CStr(Session("SI738_nBranch")), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(CStr(Session("SI738_nProduct")), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(CStr(Session("SI738_nPolicy")), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(CStr(Session("SI738_nCertif")), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(CStr(Session("SI738_nCod_Agree")), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(CStr(Session("SI738_nUser")), eFunctions.Values.eTypeData.etdDouble)) Then
		For lintIndex = 0 To lclsClaim.CountClaimBordereaux
			If lclsClaim.ItemClaimBordereaux(lintIndex) Then
				With mobjGrid
					.Columns("tcnClaim").DefValue = CStr(lclsClaim.nClaim)
					.Columns("tcnNetAmount").DefValue = CStr(lclsClaim.nReserve)
					.Columns("tcnNetAmount_Loc").DefValue = CStr(lclsClaim.nLoc_Reserv)
					.Columns("cbeCurrencyOrig").DefValue = CStr(lclsClaim.nCurrency)
					.Columns("Sel").Checked = 1
					.Columns("Sel").OnClick = "insSelected(this.checked," & lintIndex & "," & CStr(lclsClaim.nLoc_Reserv) & ")"
					
					.Columns("chkAuxStatus").Checked = 1
					.Columns("tcnAuxClaim").DefValue = CStr(lclsClaim.nClaim)
					.Columns("cbeAuxCurrencyOrig").DefValue = CStr(lclsClaim.nCurrency)
					
					Response.Write(.DoRow)
					lintIndex2 = lintIndex2 + 1
					lintTotAmount = lintTotAmount + lclsClaim.nLoc_Reserv
					
					
				End With
				lintClaim = lclsClaim.nClaim
			End If
			
			Response.Write("<SCRIPT>" & "insAddCustomerFields(""" & lclsClaim.nClaim & """)</" & "Script>")
			
			
		Next 
	End If
	
	Response.Write(mobjGrid.closeTable())
	Response.Write(mobjValues.HiddenControl("tcnClaim_Aux", CStr(0)))
	Response.Write(mobjValues.HiddenControl("tcnIndex_Aux", CStr(0)))
	Response.Write(mobjValues.HiddenControl("tcnCheck", CStr(0)))
	Response.Write(mobjValues.HiddenControl("tcnBordereaux_cl", Request.QueryString("nBordereaux_cl")))
	
	
	Response.Write("<SCRIPT>ChangeValues(""Demandant"",'" & lintClaim & "')</" & "Script>")
	Response.Write("<SCRIPT>ChangeValues(""Currency"", self.document.forms[0].cbeCurrency )</" & "Script>")
	
	Response.Write("<SCRIPT>TotAmountPay(" & CStr(lintTotAmount) & " )</" & "Script>")
	
	
	'UPGRADE_NOTE: Object lclsClaim may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
	lclsClaim = Nothing
End Sub

'% insPreSI738Upd: Se realiza el manejo de la ventana PopUp asociada al grid
'--------------------------------------------------------------------------------------------
Private Sub insPreSI738Upd()
	'--------------------------------------------------------------------------------------------
	
	Dim lobjClass As Object
	
'UPGRADE_NOTE: The 'eDll.Class' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm
	lobjClass = Server.CreateObject("eDll.Class")
	
	With Request
		If Request.QueryString("Action") = "Del" Then
			Response.Write(mobjValues.ConfirmDelete())
			If lobjClass.insPostSI738() Then
			End If
		End If
		
		Response.Write(mobjGrid.DoFormUpd(.QueryString("Action"), "valXXX.aspx", "SI738", .QueryString("nMainAction"), mobjValues.ActionQuery, .QueryString("Index")))
	End With
	'UPGRADE_NOTE: Object lobjClass may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
	lobjClass = Nothing
End Sub

</script>
<%Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage("si738")

mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 11.34.13
mobjValues.sSessionID = Session.SessionID
mobjValues.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjValues.sCodisplPage = "si738"
mobjMenu = New eFunctions.Menues
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 11.34.13
mobjMenu.sSessionID = Session.SessionID
mobjMenu.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjValues.ActionQuery = Request.QueryString("nMainAction") = 401
%>
<HTML>
<HEAD>
	<META NAME="GENERATOR" Content="Microsoft Visual Studio 6.0">
    <%'UPGRADE_NOTE: Language element '#INCLUDE' was migrated to the same language element but still may have a different behavior. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1011.htm  %>
<%'UPGRADE_NOTE: The file 'VTime/Includes/Constantes.aspx' was not found in the migration directory. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1003.htm  %>
<!-- #INCLUDE VIRTUAL="~/VTimeNet/Includes/Constantes.aspx" -->

    <%'UPGRADE_NOTE: Language element 'SCRIPT' was migrated to the same language element but still may have a different behavior. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1011.htm  %>
<%'UPGRADE_NOTE: The file '/VTimeNet/Scripts/GenFunctions.js' was not found in the migration directory. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1003.htm  %>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>    
<SCRIPT>    
//+ Variable para el control de versiones
    document.VssVersion="$$Revision: 1 $|$$Date: 2/09/03 19:01 $"
    
var marrSI738		= []
var mintCount		= -1

//% insAddCustomerFields: Añade los registros obtenidos en la consulta a un arreglo - VCVG - 19/06/2002
//------------------------------------------------------------------------------------------------------------
function insAddCustomerFields(nClaim){
//------------------------------------------------------------------------------------------------------------
    var ludtCustomerFields   = []
    
    ludtCustomerFields[0]    = nClaim    
    marrSI738[++mintCount]	 = ludtCustomerFields    
}    

//insCheckSelClick: Invoca la función que ejecuta el "submit" de la página (validaciones puntuales)
//-------------------------------------------------------------------------------------------------
function insCheckSelClick(Field,lintIndex){
//-------------------------------------------------------------------------------------------------
//+ Se invoca la función "ClientRequest" para ejecutar las validaciones    

   if(Field.checked){
       self.document.forms[0].tcnClaim_Aux.value = marrArray[lintIndex].tcnClaim;
       self.document.forms[0].tcnIndex_Aux.value = lintIndex;
       self.document.forms[0].tcnCheck.value = 1;
       top.frames['fraHeader'].ClientRequest(392,6);
    } 
}

//ChangeValues: Cambia y asigna los valores según la opción seleccionada.
//-------------------------------------------------------------------------------------------------
function ChangeValues(Option, Field){
//-------------------------------------------------------------------------------------------------
		
	with(self.document.forms[0]){
	    switch(Option){
		    case "Currency":
	            if(Field.value!=0)	            
		            insDefValues('Exchange_1','nCurrency=' + Field.value + '&dValdate=' + self.document.forms[0].elements["tcdValdate"].value);
			break; 

		    case "TypeBenef":
	            if(Field.value==2){ //Varios
		           tctClientCode.disabled=true;
		           tctClientCode_Digit.disabled=true;
		           cbeOfficePay.disabled=true;
		           cbeWayPay.disabled=true;
		           tcnTotalAmount.disabled=true;
		           tctClientCode.value='';
		           tctClientCode_Digit.value='';
		           cbeOfficePay.value='';
		           cbeWayPay.value='';
		           tcnTotalAmount.value='0';
		        }   
				else if(Field.value==1){ //Único
		           tctClientCode.disabled=false;
		           tctClientCode_Digit.disabled=false;
		           cbeOfficePay.disabled=false;
		           cbeWayPay.disabled=false;
		           tcnTotalAmount.disabled=false;		           
		        }   
			break; 
			
			case "Demandant":			
			    insDefValues('Demandant','nBordereaux_cl=' + tcnBordereaux_cl.value);
			    tctClientCode.disabled=true;
			break;	
			
			case "TotAmount":
			     break;	
			
		}
	}
}

//-------------------------------------------------------------------------------------
function insSelected(blnChecked, Index, Value){
//-------------------------------------------------------------------------------------
	var ldblTotal = 0;
	var ldblAmount = 0;
	var strParams;
	var ldblValues = 0;

    ldblValues = insConvertNumber(Value,'','', true);
    with (document.forms[0]){
		if(!blnChecked){
		    ldblAmount = insConvertNumber(tcnTotalAmount.value,'','', true) - ldblValues;
		    tcnTotalAmount.value= ldblAmount
		    chkAuxStatus[Index].value=2
		 }else{
		    ldblAmount = insConvertNumber(tcnTotalAmount.value,'','', true) + ldblValues;
		     tcnTotalAmount.value= ldblAmount
		     chkAuxStatus[Index].value=1
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
	
	<%
Response.Write(mobjValues.StyleSheet())
If Request.QueryString("Type") <> "PopUp" Then
	Response.Write(mobjMenu.setZone(2, "SI738", Request.QueryString("sWindowDescript"), Request.QueryString("nWindowTy")))
	'UPGRADE_NOTE: Object mobjMenu may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
	mobjMenu = Nothing
	Response.Write("<SCRIPT>var nMainAction=top.frames['fraSequence'].plngMainAction</SCRIPT>")
End If
%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" NAME="ClaimPaymentLot" ACTION="valClaim.aspx?sMode=2">
<%
lclsOpt_sinies = New eClaim.Opt_sinies
If lclsOpt_sinies.Find() Then
	lintOpt_cur = lclsOpt_sinies.nCurrency
Else
	lintOpt_cur = 1
End If

Call insDefineHeader()

If Request.QueryString("Type") = "PopUp" Then
	Call insPreSI738Upd()
Else
	Call insPreSI738()
End If



'UPGRADE_NOTE: Object mobjValues may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
mobjValues = Nothing
'UPGRADE_NOTE: Object mobjGrid may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
mobjGrid = Nothing
'UPGRADE_NOTE: Object lclsOpt_sinies may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
lclsOpt_sinies = Nothing
%>
</FORM> 
</BODY>
</HTML>
<%'^Begin Footer Block VisualTimer Utility 1.1 7/4/03 11.34.13
Call mobjNetFrameWork.FinishPage("si738")
'UPGRADE_NOTE: Object mobjNetFrameWork may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>




