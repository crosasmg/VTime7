<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values
Dim mobjMenu As eFunctions.Menues


'-------------------------------------------------------------------------------------------
Private Sub insPreMAU001_1()
	'-------------------------------------------------------------------------------------------
	
	
Response.Write("" & vbCrLf)
Response.Write("<SCRIPT>" & vbCrLf)
Response.Write("//% DisableProcess: Deshabilita los campos relacionados a la opción de proceso que no este" & vbCrLf)
Response.Write("//% seleccionada." & vbCrLf)
Response.Write("//--------------------------------------------------------------------------------" & vbCrLf)
Response.Write("function DisabledProcess(Field){" & vbCrLf)
Response.Write("//--------------------------------------------------------------------------------" & vbCrLf)
Response.Write("	switch(Field.value){" & vbCrLf)
Response.Write("" & vbCrLf)
Response.Write("		case ""0"":" & vbCrLf)
Response.Write("           self.document.forms[0].tcnYear.disabled = false" & vbCrLf)
Response.Write("           self.document.forms[0].cbeMonth.disabled = false" & vbCrLf)
Response.Write("           " & vbCrLf)
Response.Write("           self.document.forms[0].tcdInitdate.disabled = true" & vbCrLf)
Response.Write("           self.document.forms[0].btn_tcdInitdate.disabled = true" & vbCrLf)
Response.Write("           self.document.forms[0].tcdEnddate.disabled = true" & vbCrLf)
Response.Write("           self.document.forms[0].btn_tcdEnddate.disabled = true" & vbCrLf)
Response.Write("           " & vbCrLf)
Response.Write("           self.document.forms[0].tcdInitdate.value = ' '" & vbCrLf)
Response.Write("           self.document.forms[0].tcdEnddate.value = ' '           " & vbCrLf)
Response.Write("           break	" & vbCrLf)
Response.Write("           	" & vbCrLf)
Response.Write("		case ""1"":		" & vbCrLf)
Response.Write("           self.document.forms[0].tcdInitdate.disabled = false" & vbCrLf)
Response.Write("           self.document.forms[0].btn_tcdInitdate.disabled = false" & vbCrLf)
Response.Write("           self.document.forms[0].tcdEnddate.disabled = false" & vbCrLf)
Response.Write("           self.document.forms[0].btn_tcdEnddate.disabled = false" & vbCrLf)
Response.Write("           		" & vbCrLf)
Response.Write("           self.document.forms[0].tcnYear.disabled = true" & vbCrLf)
Response.Write("           self.document.forms[0].cbeMonth.disabled = true" & vbCrLf)
Response.Write("" & vbCrLf)
Response.Write("           self.document.forms[0].tcnYear.value = ' '" & vbCrLf)
Response.Write("           self.document.forms[0].cbeMonth.value = ' '           " & vbCrLf)
Response.Write("	}" & vbCrLf)
Response.Write("}" & vbCrLf)
Response.Write("</" & "SCRIPT>" & vbCrLf)
Response.Write("" & vbCrLf)
Response.Write("    <TABLE WIDTH=""100%"">  " & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("			<TD align=""left"">" & vbCrLf)
Response.Write("				")

	
	With Response
		.Write(mobjValues.OptionControl(0, "optProcess", GetLocalResourceObject("optProcess_CStr0Caption"), CStr(1), CStr(0), "DisabledProcess(this)",  , 3))
Response.Write("" & vbCrLf)
Response.Write("					" & vbCrLf)
Response.Write("                    <TABLE WIDTH=""100%"">" & vbCrLf)
Response.Write("                        <TR>" & vbCrLf)
Response.Write("                            <TD><LABEL ID=0>" & GetLocalResourceObject("tcnYearCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("                            <TD>")


Response.Write(mobjValues.TextControl("tcnYear", 4, Session("Year"), False, GetLocalResourceObject("tcnYearToolTip"),  ,  ,  ,  ,  , 4))


Response.Write("</TD>" & vbCrLf)
Response.Write("                        </TR>                               " & vbCrLf)
Response.Write("                        <TR>            " & vbCrLf)
Response.Write("                            <TD><LABEL ID=0>" & GetLocalResourceObject("cbeMonthCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("                            <TD>")


Response.Write(mobjValues.PossiblesValues("cbeMonth", "Table7013", eFunctions.Values.eValuesType.clngComboType, "",  ,  ,  ,  ,  ,  ,  ,  , "",  , 5))


Response.Write("</TD>" & vbCrLf)
Response.Write("                        </TR>" & vbCrLf)
Response.Write("                    </TABLE>" & vbCrLf)
Response.Write("" & vbCrLf)
Response.Write("					")

		
		.Write(mobjValues.OptionControl(0, "optProcess", GetLocalResourceObject("optProcess_CStr1Caption"), CStr(0), CStr(1), "DisabledProcess(this)",  , 6))
Response.Write("" & vbCrLf)
Response.Write("					" & vbCrLf)
Response.Write("                    <TABLE WIDTH=""100%"">" & vbCrLf)
Response.Write("                        <TR>" & vbCrLf)
Response.Write("                            <TD><LABEL ID=0>" & GetLocalResourceObject("tcdInitdateCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("     		                <TD>")

		
		Response.Write(mobjValues.DateControl("tcdInitdate",  ,  , GetLocalResourceObject("tcdInitdateToolTip"),  ,  ,  ,  , True, 7))
		
Response.Write("" & vbCrLf)
Response.Write("			                </TD>" & vbCrLf)
Response.Write("                        </TR>                               " & vbCrLf)
Response.Write("                        <TR>            " & vbCrLf)
Response.Write("                            <TD><LABEL ID=0>" & GetLocalResourceObject("tcdEnddateCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("     		                <TD>")

		
		Response.Write(mobjValues.DateControl("tcdEnddate",  ,  , GetLocalResourceObject("tcdEnddateToolTip"),  ,  ,  ,  , True, 8))
		
Response.Write("" & vbCrLf)
Response.Write("			                </TD>" & vbCrLf)
Response.Write("                        </TR>                   " & vbCrLf)
Response.Write("                    </TABLE>					" & vbCrLf)
Response.Write("					" & vbCrLf)
Response.Write("				")

		
	End With
	
Response.Write("" & vbCrLf)
Response.Write("			</TD>		" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("" & vbCrLf)
Response.Write("    </TABLE>		" & vbCrLf)
Response.Write("")

	
End Sub


'-------------------------------------------------------------------------------------------
Private Sub insPreMAU001_2()
	'-------------------------------------------------------------------------------------------
	
	
Response.Write("" & vbCrLf)
Response.Write("    <TABLE WIDTH=""100%"">    " & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("            <TD align=""left""> " & vbCrLf)
Response.Write("                <TABLE WIDTH=""100%"">" & vbCrLf)
Response.Write("			        <TD><LABEL ID=0>" & GetLocalResourceObject("AnchorCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("			        <TD><LABEL ID=0>" & GetLocalResourceObject("tcnInitVoucherCaption") & "</LABEL></TD>			" & vbCrLf)
Response.Write("                    <TD>")


Response.Write(mobjValues.NumericControl("tcnInitVoucher", 4, "", False, "", False,  ,  ,  ,  ,  ,  , 9))


Response.Write("</TD>" & vbCrLf)
Response.Write("			        <TD><LABEL ID=0>" & GetLocalResourceObject("tcnEndVoucherCaption") & "</LABEL></TD>			" & vbCrLf)
Response.Write("                    <TD>")


Response.Write(mobjValues.NumericControl("tcnEndVoucher", 4, "", False, "", False,  ,  ,  ,  ,  ,  , 10))


Response.Write("</TD>" & vbCrLf)
Response.Write("                </TABLE>" & vbCrLf)
Response.Write("            </TD>" & vbCrLf)
Response.Write("        </TR>        " & vbCrLf)
Response.Write("        " & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("            <TABLE WIDTH=""100%"">             " & vbCrLf)
Response.Write("                <TR>" & vbCrLf)
Response.Write("                    <TD><LABEL ID=0>" & GetLocalResourceObject("tctAccountCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("		            <TD>")

	
	With mobjValues
		.Parameters.Add("nLed_compan", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		Response.Write(mobjValues.PossiblesValues("tctAccount", "tabLedger_acc", eFunctions.Values.eValuesType.clngWindowType, "", True,  ,  ,  ,  , "ShowChangeValues(this)", False, 20, "", eFunctions.Values.eTypeCode.eString, 11))
	End With
	
Response.Write("</TD>" & vbCrLf)
Response.Write("                </TR>" & vbCrLf)
Response.Write("                <TR>" & vbCrLf)
Response.Write("                    <TD><LABEL ID=11465>" & GetLocalResourceObject("tctAux_accounCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("			        <TD>")

	
	With mobjValues
		.Parameters.Add("nLed_compan", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Parameters.Add("sAccount", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		Response.Write(mobjValues.PossiblesValues("tctAux_accoun", "tabLedger_accAux", eFunctions.Values.eValuesType.clngWindowType, "", True,  ,  ,  ,  , "ShowChangeValues(this)", False, 20, "", eFunctions.Values.eTypeCode.eString, 12))
	End With
	
Response.Write("</TD>" & vbCrLf)
Response.Write("                </TR>" & vbCrLf)
Response.Write("                <TR>" & vbCrLf)
Response.Write("                    <TD><LABEL ID=0>" & GetLocalResourceObject("cbeCost_centeCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("                    <TD>")

	
	With mobjValues
		.Parameters.Add("nLed_compan", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		Response.Write(mobjValues.PossiblesValues("cbeCost_cente", "tabtab_cost_c", eFunctions.Values.eValuesType.clngWindowType, "", True,  ,  ,  ,  ,  , False,  , GetLocalResourceObject("cbeCost_centeToolTip"), eFunctions.Values.eTypeCode.eString, 13))
	End With
	
Response.Write("</TD>" & vbCrLf)
Response.Write("                </TR>                        " & vbCrLf)
Response.Write("                <TR>            " & vbCrLf)
Response.Write("                    <TD>")


Response.Write(mobjValues.CheckControl("chkSum", GetLocalResourceObject("chkSumCaption"), "2", "1",  , False, 14))


Response.Write("</TD>" & vbCrLf)
Response.Write("                </TR>                " & vbCrLf)
Response.Write("            </TABLE>" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("        " & vbCrLf)
Response.Write("    </TABLE>		" & vbCrLf)
Response.Write("")

	
End Sub

</script>
<%Response.Expires = -1

With Server
	mobjValues = New eFunctions.Values
	mobjMenu = New eFunctions.Menues
	mobjValues.sCodisplPage = "CPL004_K"
End With
%>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<HTML>
<HEAD>

<SCRIPT>
//% insStateZone: se manejan los campos de la página
//------------------------------------------------------------------------------------------
function insStateZone(){
//------------------------------------------------------------------------------------------
}
//% insCancel: Ejecuta rutinas necesarias en el momento de cancelar la página
//------------------------------------------------------------------------------------------
function insCancel(){
//------------------------------------------------------------------------------------------
}
//% ShowAccount: Asigna los datos necesarios para la búsqueda de la cuenta contable
//---------------------------------------------------------------------------------
function ShowAccount(){
//---------------------------------------------------------------------------------

//+ Parámetro necesario para la búsqueda de la cuenta en el control tctAccount (Cuenta Contable)
	self.document.forms[0].tctAccount.Parameters.Param1.sValue = self.document.forms[0].cbeLedCompan.value
	
//+ Parámetro necesario para la búsqueda de las unidades organizativas cbeCost_cente
	self.document.forms[0].cbeCost_cente.Parameters.Param1.sValue = self.document.forms[0].cbeLedCompan.value	
}
//% DisabledField: deshabilita algunos campos dependiendo del tipo de reporte.
//---------------------------------------------------------------------------------
function DisabledField(Field){
//---------------------------------------------------------------------------------
   switch(Field.value){
		case "1"://Minutas
           self.document.forms[0].tcnInitVoucher.disabled = false
           self.document.forms[0].tcnEndVoucher.disabled = false
           self.document.forms[0].chkSum.disabled = false
           
           self.document.forms[0].tctAccount.disabled = true
           self.document.forms[0].btntctAccount.disabled = true
           self.document.forms[0].tctAux_accoun.disabled = true
           self.document.forms[0].btntctAux_accoun.disabled = true
           self.document.forms[0].cbeCost_cente.disabled = true
           self.document.forms[0].btncbeCost_cente.disabled = true
           
           self.document.forms[0].tctAccount.value = ' '
           self.document.forms[0].tctAux_accoun.value = ' '
           self.document.forms[0].cbeCost_cente.value = ' '
           
           self.document.forms[0].optProcess[1].checked = '1'
           self.document.forms[0].optProcess[0].disabled = true
           
           self.document.forms[0].tcnYear.disabled = true
           self.document.forms[0].cbeMonth.disabled = true
           
           self.document.forms[0].tcdInitdate.disabled = false
           self.document.forms[0].btn_tcdInitdate.disabled = false
           self.document.forms[0].tcdEnddate.disabled = false
           self.document.forms[0].btn_tcdEnddate.disabled = false
           
           self.document.forms[0].tcdInitdate.value = ' '
           self.document.forms[0].tcdEnddate.value = ' '
           self.document.forms[0].tcnYear.value = ' '          
           self.document.forms[0].cbeMonth.value = ' '
           break
           
        case "2"://Mayor
           self.document.forms[0].tctAccount.disabled = false
           self.document.forms[0].btntctAccount.disabled = false
           self.document.forms[0].tctAux_accoun.disabled = false
           self.document.forms[0].btntctAux_accoun.disabled = false
           self.document.forms[0].cbeCost_cente.disabled = false
           self.document.forms[0].btncbeCost_cente.disabled = false
           
           self.document.forms[0].tcnInitVoucher.disabled = true
           self.document.forms[0].tcnEndVoucher.disabled = true           
           self.document.forms[0].chkSum.disabled = true
           
           self.document.forms[0].tcnInitVoucher.value = ' '
           self.document.forms[0].tcnEndVoucher.value = ' '           
           self.document.forms[0].chkSum.value = ' '
           
           
           self.document.forms[0].optProcess[0].checked = '1'
           self.document.forms[0].optProcess[1].disabled = true
           
           self.document.forms[0].tcnYear.disabled = false
           self.document.forms[0].cbeMonth.disabled = false
           
           self.document.forms[0].tcdInitdate.disabled = true
           self.document.forms[0].btn_tcdInitdate.disabled = true
           self.document.forms[0].tcdEnddate.disabled = true
           self.document.forms[0].btn_tcdEnddate.disabled = true
           
           self.document.forms[0].tcdInitdate.value = ' '
           self.document.forms[0].tcdEnddate.value = ' '
           self.document.forms[0].tcnYear.value = ' '          
           self.document.forms[0].cbeMonth.value = ' '
           break
           
        case "3"://Diario General
           self.document.forms[0].chkSum.disabled = false
                   
           self.document.forms[0].tcnInitVoucher.disabled = true
           self.document.forms[0].tcnEndVoucher.disabled = true
           self.document.forms[0].tctAccount.disabled = true
           self.document.forms[0].btntctAccount.disabled = true
           self.document.forms[0].tctAux_accoun.disabled = true
           self.document.forms[0].btntctAux_accoun.disabled = true
           self.document.forms[0].cbeCost_cente.disabled = true
           self.document.forms[0].btncbeCost_cente.disabled = true

           self.document.forms[0].tcnInitVoucher.value = ' '
           self.document.forms[0].tcnEndVoucher.value = ' '
           self.document.forms[0].tctAccount.value = ' '
           self.document.forms[0].tctAux_accoun.value = ' '
           self.document.forms[0].cbeCost_cente.value = ' '
           break
           
        case "4":// Todos  
           self.document.forms[0].chkSum.disabled = false
                   
           self.document.forms[0].tcnInitVoucher.disabled = true
           self.document.forms[0].tcnEndVoucher.disabled = true
           self.document.forms[0].tctAccount.disabled = true
           self.document.forms[0].btntctAccount.disabled = true
           self.document.forms[0].tctAux_accoun.disabled = true
           self.document.forms[0].btntctAux_accoun.disabled = true
           self.document.forms[0].cbeCost_cente.disabled = true
           self.document.forms[0].btncbeCost_cente.disabled = true
           
           self.document.forms[0].tcnInitVoucher.value = ' '
           self.document.forms[0].tcnEndVoucher.value = ' '
           self.document.forms[0].tctAccount.value = ' '
           self.document.forms[0].tctAux_accoun.value = ' '
           self.document.forms[0].cbeCost_cente.value = ' '

   }    
}

//% ShowChangeValues: Llama a la página ShowDefValues que ejecuta código necesario
//% para la actualización de los controles de "Header"
//--------------------------------------------------------------------------------
function ShowChangeValues(Field){
//--------------------------------------------------------------------------------

	switch(Field.name){
		case "tctAccount":

//+ Parámetros necesarios para la búsqueda de la cuenta auxiliar en el control valAux (Cuenta Auxiliar Contable)

			self.document.forms[0].tctAux_accoun.Parameters.Param1.sValue = self.document.forms[0].cbeLedCompan.value
			self.document.forms[0].tctAux_accoun.Parameters.Param2.sValue = self.document.forms[0].tctAccount.value;

			break;
		case "tctAux_accoun":
			if (Field.value == '')
			{
				self.document.forms[0].tctAux_accoun.value = '                    '
			};
	}
}
</SCRIPT>
    <META NAME = "GENERATOR" Content = "Microsoft Visual Studio 6.0">
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tMenu.js"></SCRIPT>


	<%
Response.Write(mobjValues.StyleSheet())
Response.Write(mobjMenu.MakeMenu("CPL004", "CPL004_K.aspx", 1, ""))
mobjMenu = Nothing
%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="post" ID="FORM" NAME="frmMajorDaily" ACTION="ValLedGerRep.aspx?mode=1">
    <BR></BR>
    <TABLE WIDTH="100%">     
        <TR>
            <TD><LABEL ID=0><%= GetLocalResourceObject("cbeLedCompanCaption") %></LABEL></TD>
            <TD>
                <%
With mobjValues
	.Parameters.Add("nCompany", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	Response.Write(mobjValues.PossiblesValues("cbeLedCompan", "TabLedCompanyclient", eFunctions.Values.eValuesType.clngComboType,  , True,  ,  ,  ,  , "ShowAccount()", False, 30, GetLocalResourceObject("cbeLedCompanToolTip"), eFunctions.Values.eTypeCode.eString, 1))
End With
%>
            </TD>
            <TD><LABEL ID=0><%= GetLocalResourceObject("cbeReportTypeCaption") %></LABEL></TD>            
            <TD><%=mobjValues.PossiblesValues("cbeReportType", "table289", eFunctions.Values.eValuesType.clngComboType, "",  ,  ,  ,  ,  , "DisabledField(this)", False, 30, GetLocalResourceObject("cbeReportTypeToolTip"),  , 2)%></TD>                    
        </TR>        
    </TABLE>
    
    <BR></BR>    
    
    <TABLE WIDTH="100%">   
		<TR>
			<TD WIDTH="25%" COLSPAN="2" CLASS="HighLighted"><LABEL ID=0><a NAME="Período a procesar"><%= GetLocalResourceObject("AnchorPeríodo a procesarCaption") %></a></LABEL></td>
			<TD>&nbsp;</TD>
			<TD WIDTH="70%" COLSPAN="2" CLASS="HighLighted"><LABEL ID=0><a NAME="Opciones"><%= GetLocalResourceObject("AnchorOpcionesCaption") %></a></LABEL></td>
		</TR>
		<TR>
			<TD COLSPAN="2" CLASS="HorLine"></TD>
			<TD></TD>
			<TD COLSPAN="2" CLASS="HorLine"></TD>
		</TR> 
    </TABLE>   
        		     
    <TABLE WIDTH="100%" COLS=2>       		     
            <TR>
                <TD VALIGN=TOP WIDTH="10%">
                    <DIV ID="Scroll" style="width:155;height:250;overflow:auto; outset gray">
                        <%
Call insPreMAU001_1()
%>
                    </DIV>
                </TD>
                <TD VALIGN=TOP>
                    <DIV ID="Scroll" style="width:18;height:250;overflow:auto; outset gray">
                    &nbsp;
                    </DIV>
                </TD>
                <TD VALIGN=TOP>
                    <DIV ID="Scroll" style="width:415;height:250;overflow:auto; outset gray">
                        <%
Call insPreMAU001_2()
%>
                    </DIV>
                </TD>
            </TR>
    </TABLE>
</FORM>
</BODY>
</HTML>

<%
Response.Write("<SCRIPT>ShowAccount()</SCRIPT>")

mobjValues = Nothing
%>




