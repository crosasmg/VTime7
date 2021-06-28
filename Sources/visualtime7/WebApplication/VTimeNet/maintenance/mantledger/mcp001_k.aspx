<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">

'-   Objeto para el manejo de las funciones generales de carga de valores es definido

Dim mobjValues As eFunctions.Values

'**- generic routines objects are defined
'-   Objeto para el manejo de las rutinas genéricas es definido

Dim mobjMenu As eFunctions.Menues



'%   insDefineHeader: Permite cargar los campos del encabezado
'----------------------------------------------------------------------------
Private Sub insDefineHeader()
	'----------------------------------------------------------------------------
	
Response.Write("" & vbCrLf)
Response.Write("	<TABLE WIDTH=""100%"">" & vbCrLf)
Response.Write("		<BR>" & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("			<TD ALIGN=""LEFT"" CLASS=""HighLighted"" COLSPAN = 6><LABEL><A NAME=""Recibo"">" & GetLocalResourceObject("AnchorReciboCaption") & "</A></LABEL></TD>" & vbCrLf)
Response.Write("		</TR>" & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("			<TD>&nbsp;</TD>" & vbCrLf)
Response.Write("			<TD>&nbsp;</TD>" & vbCrLf)
Response.Write("			<TD>&nbsp;</TD>" & vbCrLf)
Response.Write("			<TD>&nbsp;</TD>" & vbCrLf)
Response.Write("			<TD COLSPAN = 2><HR></TD>		" & vbCrLf)
Response.Write("		</TR>" & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("			<TD><LABEL>" & GetLocalResourceObject("cboCompLedCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("			<TD>")


Response.Write(mobjValues.PossiblesValues("cboCompLed", "tabled_compan", eFunctions.Values.eValuesType.clngWindowType,  ,  ,  ,  ,  ,  ,  , True, 5, GetLocalResourceObject("cboCompLedToolTip"), eFunctions.Values.eTypeCode.eNumeric, 1))


Response.Write("</TD>" & vbCrLf)
Response.Write("			<TD>&nbsp;</TD>" & vbCrLf)
Response.Write("			<TD>&nbsp;</TD>" & vbCrLf)
Response.Write("			<TD> <LABEL>" & GetLocalResourceObject("cbeTratypeiCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("			<TD>")


Response.Write(mobjValues.PossiblesValues("cbeTratypei", "table24", eFunctions.Values.eValuesType.clngComboType, vbNullString, False,  ,  ,  ,  ,  , True,  , GetLocalResourceObject("cbeTratypeiToolTip"), eFunctions.Values.eTypeCode.eString, 5))


Response.Write("" & vbCrLf)
Response.Write("		</TR>" & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("			<TD><LABEL>" & GetLocalResourceObject("cbeAreaCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("			")

	mobjValues.TypeList = 1
	
	mobjValues.List = "1,2,3,4,5,6,40"
Response.Write("" & vbCrLf)
Response.Write("                         " & vbCrLf)
Response.Write("			<TD> ")


Response.Write(mobjValues.PossiblesValues("cbeArea", "table178", eFunctions.Values.eValuesType.clngComboType, "", False,  ,  ,  ,  , "insStateZone();insDisable(this,1)", True,  , GetLocalResourceObject("cbeAreaToolTip"), eFunctions.Values.eTypeCode.eString, 2))


Response.Write("" & vbCrLf)
Response.Write("			<TD>&nbsp;</TD>" & vbCrLf)
Response.Write("			<TD>&nbsp;</TD>" & vbCrLf)
Response.Write("			<!--TD><LABEL>" & GetLocalResourceObject("cboPayCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("			<TD>")


Response.Write(mobjValues.PossiblesValues("cboPay", "Table182", eFunctions.Values.eValuesType.clngComboType, vbNullString, False,  ,  ,  ,  ,  , True,  , GetLocalResourceObject("cboPayToolTip"), eFunctions.Values.eTypeCode.eString, 6))


Response.Write("</TD-->" & vbCrLf)
Response.Write("        </TR>        " & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("			<TD><LABEL>" & GetLocalResourceObject("valTransactyCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("			<TD>")


Response.Write(mobjValues.PossiblesValues("valTransacty", "table6", eFunctions.Values.eValuesType.clngWindowType, vbNullString, False,  ,  ,  ,  , "insDisable_1(this)", True,  , GetLocalResourceObject("valTransactyToolTip"), eFunctions.Values.eTypeCode.eString, 3))


Response.Write("" & vbCrLf)
Response.Write("			<TD>&nbsp;</TD>" & vbCrLf)
Response.Write("			<TD>&nbsp;</TD>" & vbCrLf)
Response.Write("			<TD>&nbsp;</TD>" & vbCrLf)
Response.Write("			<TD>&nbsp;</TD>" & vbCrLf)
Response.Write("		</TR>" & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("			<TD><LABEL>" & GetLocalResourceObject("cbeGroupCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("			<TD>")


Response.Write(mobjValues.PossiblesValues("cbeGroup", "table641", eFunctions.Values.eValuesType.clngComboType, vbNullString, False,  ,  ,  ,  ,  , True,  , GetLocalResourceObject("cbeGroupToolTip"), eFunctions.Values.eTypeCode.eString, 4))


Response.Write("" & vbCrLf)
Response.Write("			<TD>&nbsp;</TD>" & vbCrLf)
Response.Write("			<TD>&nbsp;</TD>" & vbCrLf)
Response.Write("			<TD>&nbsp;</TD>" & vbCrLf)
Response.Write("			<TD>&nbsp;</TD>" & vbCrLf)
Response.Write("		</TR>		" & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("			<TD>&nbsp;</TD>" & vbCrLf)
Response.Write("			<TD CLASS=""HighLighted""><LABEL><A NAME=""Tipo"">" & GetLocalResourceObject("AnchorTipoCaption") & "</A></LABEL></TD>" & vbCrLf)
Response.Write("			<TD>&nbsp;</TD>" & vbCrLf)
Response.Write("			<TD>&nbsp;</TD>" & vbCrLf)
Response.Write("			<TD CLASS=""HighLighted"" COLSPAN=3><LABEL><A NAME=""Tipo de ramo"">" & GetLocalResourceObject("AnchorTipo de ramoCaption") & "</A></LABEL></TD>" & vbCrLf)
Response.Write("		</TR>" & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("			<TD COLSPAN=2><HR></TD>		" & vbCrLf)
Response.Write("			<TD>&nbsp;</TD>" & vbCrLf)
Response.Write("			<TD>&nbsp;</TD>" & vbCrLf)
Response.Write("			<TD COLSPAN=2><HR></TD>		" & vbCrLf)
Response.Write("		</TR>" & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("			<TD>")

	Response.Write(mobjValues.HiddenControl("tctReceiptTy_h", CStr(1)))
	Response.Write(mobjValues.OptionControl(0, "optReceiptTy", GetLocalResourceObject("optReceiptTy_CStr1Caption"), eFunctions.Values.vbChecked, CStr(1), "InsAssignValue(this);", True, 7, GetLocalResourceObject("optReceiptTy_CStr1ToolTip")))
Response.Write("</TD>" & vbCrLf)
Response.Write("			<TD>&nbsp;</TD>" & vbCrLf)
Response.Write(" 		    <TD>&nbsp;</TD>" & vbCrLf)
Response.Write("			<TD>&nbsp;</TD>" & vbCrLf)
Response.Write("			<TD>")


Response.Write(mobjValues.OptionControl(0, "optProducTy", GetLocalResourceObject("optProducTy_CStr1Caption"), eFunctions.Values.vbChecked, CStr(1),  , True, 9, GetLocalResourceObject("optProducTy_CStr1ToolTip")))


Response.Write("</TD>			" & vbCrLf)
Response.Write("		</TR>" & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("			<TD>")


Response.Write(mobjValues.OptionControl(0, "optReceiptTy", GetLocalResourceObject("optReceiptTy_CStr2Caption"), eFunctions.Values.vbUnChecked, CStr(2), "InsAssignValue(this);", True, 8, GetLocalResourceObject("optReceiptTy_CStr2ToolTip")))


Response.Write("</TD>		" & vbCrLf)
Response.Write("			<TD>&nbsp;</TD>" & vbCrLf)
Response.Write("			<TD>&nbsp;</TD>" & vbCrLf)
Response.Write("			<TD>&nbsp;</TD>" & vbCrLf)
Response.Write("			<TD>")


Response.Write(mobjValues.OptionControl(0, "optProducTy", GetLocalResourceObject("optProducTy_CStr2Caption"), eFunctions.Values.vbUnChecked, CStr(2),  , True, 10, GetLocalResourceObject("optProducTy_CStr2ToolTip")))


Response.Write("</TD>" & vbCrLf)
Response.Write("			<TD>&nbsp;</TD>" & vbCrLf)
Response.Write("		</TR>" & vbCrLf)
Response.Write("		" & vbCrLf)
Response.Write("" & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("		    <TD>&nbsp;</TD>" & vbCrLf)
Response.Write("			<TD CLASS=""HighLighted""><LABEL><A NAME=""Siniestro"">" & GetLocalResourceObject("AnchorSiniestroCaption") & "</A></LABEL></TD>" & vbCrLf)
Response.Write("		</TR>" & vbCrLf)
Response.Write("" & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("			<TD COLSPAN=2><HR></TD>		" & vbCrLf)
Response.Write("		</TR>" & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("			<TD><LABEL>" & GetLocalResourceObject("cboPayCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("			<TD> ")


Response.Write(mobjValues.PossiblesValues("cboPayType", "Table138", eFunctions.Values.eValuesType.clngComboType, vbNullString, False,  ,  ,  ,  ,  , False,  , GetLocalResourceObject("cboPayTypeToolTip"), eFunctions.Values.eTypeCode.eString, 12))


Response.Write("" & vbCrLf)
Response.Write("		</TR>" & vbCrLf)
Response.Write("		" & vbCrLf)
Response.Write("		" & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("		    <TD>&nbsp;</TD>" & vbCrLf)
Response.Write("			<TD CLASS=""HighLighted""><LABEL><A NAME=""Cuenta corriente"">" & GetLocalResourceObject("AnchorCuenta corrienteCaption") & "</A></LABEL></TD>" & vbCrLf)
Response.Write("		</TR>" & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("			<TD COLSPAN=2><HR></TD>		" & vbCrLf)
Response.Write("		</TR>" & vbCrLf)
Response.Write("        <TR> " & vbCrLf)
Response.Write("			<TD><LABEL>" & GetLocalResourceObject("cboTypeAccCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("			<TD>")


Response.Write(mobjValues.PossiblesValues("cboTypeAcc", "Table400", eFunctions.Values.eValuesType.clngComboType, vbNullString, False,  ,  ,  ,  ,  , True,  , GetLocalResourceObject("cboTypeAccToolTip"), eFunctions.Values.eTypeCode.eString, 13))


Response.Write("</TD>" & vbCrLf)
Response.Write("		</TR>" & vbCrLf)
Response.Write("		" & vbCrLf)
Response.Write("		" & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("		    <TD>&nbsp;</TD>" & vbCrLf)
Response.Write("			<TD ALIGN=""LEFT"" CLASS=""HighLighted"" COLSPAN = 6><LABEL><A NAME=""Caja ingreso"">" & GetLocalResourceObject("AnchorCaja ingresoCaption") & "</A></LABEL></TD>" & vbCrLf)
Response.Write("		</TR>" & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("			<TD>&nbsp;</TD>" & vbCrLf)
Response.Write("			<TD>&nbsp;</TD>" & vbCrLf)
Response.Write("			<TD>&nbsp;</TD>" & vbCrLf)
Response.Write("			<TD>&nbsp;</TD>" & vbCrLf)
Response.Write("			<TD COLSPAN=2><HR></TD>		" & vbCrLf)
Response.Write("		</TR>" & vbCrLf)
Response.Write("        <TR> " & vbCrLf)
Response.Write("			<TD><LABEL>" & GetLocalResourceObject("cboPayTypeCCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("			<TD>")


Response.Write(mobjValues.PossiblesValues("cboPayTypeC", "Table182", eFunctions.Values.eValuesType.clngComboType, vbNullString, False,  ,  ,  ,  ,  , False,  , GetLocalResourceObject("cboPayTypeCToolTip"), eFunctions.Values.eTypeCode.eString, 14))


Response.Write("</TD>" & vbCrLf)
Response.Write("			<TD>&nbsp;</TD>" & vbCrLf)
Response.Write("			<TD>&nbsp;</TD>" & vbCrLf)
Response.Write("			<TD><LABEL>" & GetLocalResourceObject("cbeCollecDocTypCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("			<TD>")


Response.Write(mobjValues.PossiblesValues("cbeCollecDocTyp", "Table5587", eFunctions.Values.eValuesType.clngComboType, vbNullString, False,  ,  ,  ,  ,  , True,  , GetLocalResourceObject("cbeCollecDocTypToolTip"), eFunctions.Values.eTypeCode.eString, 14))


Response.Write("</TD>" & vbCrLf)
Response.Write("		</TR>		" & vbCrLf)
Response.Write("	</TABLE>	")

	
End Sub

</script>
<%Response.Expires = -1
mobjValues = New eFunctions.Values
mobjMenu = New eFunctions.Menues

mobjValues.sCodisplPage = "MCP001"
%>
<HTML>
<HEAD>
	<META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>


<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tmenu.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/valFunctions.js"></SCRIPT>
<SCRIPT>
//------------------------------------------------------------------------------------------
function InsAssignValue(Field) {
//------------------------------------------------------------------------------------------
    if (Field.value == 2) self.document.forms[0].tctReceiptTy_h.value = 2
    else self.document.forms[0].tctReceiptTy_h.value = 1
}

//%insDisable: Esta función permite cambiar el estado de los campos del encabezado de la página
//------------------------------------------------------------------------------------------
function insDisable(lobject, lclear){
//------------------------------------------------------------------------------------------
	document.getElementsByTagName("TR")[17].style.display='none'
	document.getElementsByTagName("TR")[18].style.display='none'
    document.getElementsByTagName("TR")[19].style.display='none'
                
	document.getElementsByTagName("TR")[20].style.display='none'
	document.getElementsByTagName("TR")[21].style.display='none'
    document.getElementsByTagName("TR")[22].style.display='none'

	document.getElementsByTagName("TR")[23].style.display='none'
	document.getElementsByTagName("TR")[24].style.display='none'
    document.getElementsByTagName("TR")[25].style.display='none'
    
    if (!self.document.forms[0].cbeArea.disabled) { 
		switch (lobject.value) {

//+   Primas 
			case '1': 
  			    self.document.forms[0].valTransacty.sTabName = "Table6";
				with (self.document.forms[0]) {
					cbeTratypei.disabled = false
					optReceiptTy[0].disabled = false
					optReceiptTy[1].disabled = false
					cboPayType.disabled = true
					cboTypeAcc.disabled = true
					
				 };
				 
			     break;
//+   Siniestros 
			case '2':
				self.document.forms[0].valTransacty.sTabName = "Table140";
				with (self.document.forms[0]) {
					cbeTratypei.disabled = true
					optReceiptTy[0].disabled = true
					optReceiptTy[1].disabled = true
					tctReceiptTy_h.value = 0
					cboTypeAcc.disabled = true
					cboPayType.disabled = false
				};
		        document.getElementsByTagName("TR")[17].style.display=''
		        document.getElementsByTagName("TR")[18].style.display=''
                document.getElementsByTagName("TR")[19].style.display=''
				break;

//+   Cuentas corrientes 
			case '3':
   				self.document.forms[0].valTransacty.sTabName = "Table401";
   				
   				self.document.forms[0].valTransacty.sTabName = "Table401";            				         
   				
				with (self.document.forms[0]) {
					cbeTratypei.disabled = true
					optReceiptTy[0].disabled = true
					optReceiptTy[1].disabled = true
					tctReceiptTy_h.value = 0
					cboPayType.disabled = true
					cboTypeAcc.disabled = false
				};
		        document.getElementsByTagName("TR")[20].style.display=''
		        document.getElementsByTagName("TR")[21].style.display=''
                document.getElementsByTagName("TR")[22].style.display=''
				break;

//+   Co/Reaseguro
			case '4':
				with (self.document.forms[0]) {
					cbeTratypei.disabled = true
					valTransacty.disabled = true
					btnvalTransacty.disabled = true
					cbeGroup.disabled = true
					cbeGroup.value=0
					optReceiptTy[0].disabled = true
					optReceiptTy[0].checked = true
					optReceiptTy[0].value = "1"
					optReceiptTy[1].disabled = true
					tctReceiptTy_h.value = 0
					cboPayType.disabled = true
					cboPayType.value = 0
					cboTypeAcc.disabled = true
					cboTypeAcc.value = 0
				};

				break;
         
//+   Caja ingresos
			case '5':
				self.document.forms[0].valTransacty.sTabName = "Table22";         
				with (self.document.forms[0]) {
					cbeTratypei.disabled = true
					optReceiptTy[0].disabled = true
					optReceiptTy[1].disabled = true
					tctReceiptTy_h.value = 0
					cboPayType.disabled = false					
					cboTypeAcc.disabled = true
				};
		        document.getElementsByTagName("TR")[23].style.display=''
		        document.getElementsByTagName("TR")[24].style.display=''
                document.getElementsByTagName("TR")[25].style.display=''
  				break;
//+   Caja egresos 
			case '6':
				 self.document.forms[0].valTransacty.sTabName = "Table293";         
				 with (self.document.forms[0]) {
					cbeTratypei.disabled = true
					optReceiptTy[0].disabled = true
					optReceiptTy[1].disabled = true
					tctReceiptTy_h.value = 0
					cboPayType.disabled = true
					cboTypeAcc.disabled = true
				 };
				 break;

//+    Financiamiento
			case '7':
				 self.document.forms[0].valTransacty.sTabName = "Table260";
				 with (self.document.forms[0]) {
					cbeTratypei.disabled = true
					optReceiptTy[0].disabled = true
					optReceiptTy[1].disabled = true
					cboPayType.disabled = true
					cboTypeAcc.disabled = true
					tctReceiptTy_h.value = 0
				 };
				 break;
//**+ Current accounts - APV.
//+ Cuentas corrientes - APV.
				 
			case '40':
				self.document.forms[0].valTransacty.sTabName = "TABLEDCURRACC_APV";
				with (self.document.forms[0]) {
					cbeTratypei.disabled = true
					optReceiptTy[0].disabled = true
					optReceiptTy[1].disabled = true
					tctReceiptTy_h.value = 0
					cboPayType.disabled = true
					cboTypeAcc.disabled = true
				};
				break;

			default:
				self.document.forms[0].valTransacty.sTabName = "Table6";         
				with (self.document.forms[0]) {
					cbeTratypei.disabled = false
					optReceiptTy[0].disabled = false
					optReceiptTy[1].disabled = false
					tctReceiptTy_h.value = 0
					cboPayType.disabled = true
					cboTypeAcc.disabled = true
				};
		}   
    
        if (lclear==1) {
			with (self.document.forms[0]) {
				valTransacty.value = ' '
				UpdateDiv('valTransactyDesc', '')
				cbeTratypei.value = 0

				if (lobject.value==1){
				    optReceiptTy[0].value = 1
				    optReceiptTy[1].value = 2
				    tctReceiptTy_h.value = 1
				}    
				else{
				    optReceiptTy[0].value = 0
				    optReceiptTy[1].value = 0
				    tctReceiptTy_h.value  = 0
				}
				
				cboPayType.value = 0
				cboTypeAcc.value = 0
			}
		}
	}
}

//%   insDisable_1: Esta función permite cambiar el estado de los campos del encabezado de la página
//------------------------------------------------------------------------------------------
function insDisable_1(lobject){
//------------------------------------------------------------------------------------------
	with (self.document.forms[0])
	{
		if (cbeArea.value==1 || cbeArea.value==2)
		{
			if (cbeArea.value==1 && 
			   (lobject.value==2 || lobject.value==4))
			{
//+   Valores tomados de la table6 - valTransacty - lobject			   
				//cboPay.disabled = false;
			}				
            else
            {					
				if ((cbeArea.value==2) && 
				   (lobject.value==5  || lobject.value==10 || lobject.value==11 || 
			        lobject.value==12 || lobject.value==15 || lobject.value==20 ||
			        lobject.value==23))
			    {
//+   Valores tomados de la table140 - valTransacty - lobject			   
				    cboPayType.disabled = false;
				}
				else
				{
					cboPayType.value = 0;
					cboPayType.disabled = true;
			    }	
			}		
		}
		
		if (cbeArea.value==5){
		    if (lobject.value==39)
		        cbeCollecDocTyp.disabled = false;
		    else
		        cbeCollecDocTyp.disabled = true;
		} 
	}		
}

//%insCancel: Ejecuta rutinas necesarias en el momento de cancelar la página
//------------------------------------------------------------------------------------------
function insCancel(){
//------------------------------------------------------------------------------------------
   return (true);
}

//%   insStateZone: Se controla el estado de los campos de la página
//------------------------------------------------------------------------------------------
function insStateZone(){
//-------------------------------------------------------- ----------------------------------
	with (self.document.forms[0]) {
	 	optProducTy[0].disabled = false
		optProducTy[1].disabled = false
		cbeArea.disabled = false
		cboCompLed.disabled = false
		valTransacty.disabled = false
		cbeGroup.disabled = false
    }
    	
    self.document.images.btnvalTransacty.disabled = false
	self.document.images.btncboCompLed.disabled = false
    
    insDisable(self.document.forms[0].cbeArea, 2)
    insDisable_1(self.document.forms[0].valTransacty)
}

//+ Esta línea guarda la versión procedente de VSS 
    document.VssVersion="$$Revision: 4 $|$$Date: 2/12/03 12:44 $" 

</SCRIPT>
<%With Response
	.Write(mobjValues.StyleSheet)
	.Write(mobjMenu.MakeMenu("MCP001", "MCP001_k.aspx", 1, ""))
End With

mobjMenu = Nothing
%>
</HEAD>

<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" ID="FORM" NAME="frmAutomEntTab" ACTION="ValMantLedger.aspx?sTime=1">
<BR>
<%
'+   Se cargan los campos del encabezado
Call insDefineHeader()
Response.Write("<SCRIPT>insDisable('0','1');</SCRIPT>")
%>	
</FORM>
</BODY>
</HTML>





