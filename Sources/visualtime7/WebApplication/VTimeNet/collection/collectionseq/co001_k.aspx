<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eCashBank" %>
<%@ Import namespace="eGeneral" %>
<script language="VB" runat="Server">
'^Begin Header Block VisualTimer Utility 1.1 3/4/03 11.58.23
Dim mobjNetFrameWork As eNetFrameWork.Layout
'~End Header Block VisualTimer Utility
Dim mobjMenu As eFunctions.Menues
'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

Dim lclsCash_stat As eCashBank.Cash_stat
Dim lobjErrors As eGeneral.GeneralFunction
    Dim lstrAlert As String
    


'% LoadPageInSequence: Muestra los campos cuando se encuentra en la secuencia
'--------------------------------------------------------------------------------
Private Sub LoadPageInSequence()
	'--------------------------------------------------------------------------------
	
Response.Write("" & vbCrLf)
Response.Write("	<TABLE>" & vbCrLf)
Response.Write("	    <TR>" & vbCrLf)
Response.Write("            <TD WIDTH=""20%""><LABEL ID=19640>" & GetLocalResourceObject("AnchorCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("            <TD WIDTH=""25%"">")


        Response.Write(mobjValues.DIVControl("lblBordereaux", False, Session("nBordereaux")))
        


Response.Write("</TD>" & vbCrLf)
Response.Write("            <TD WIDTH=""10%"">&#160;</TD>" & vbCrLf)
Response.Write("            <TD WIDTH=""10%""><LABEL ID=19642>" & GetLocalResourceObject("cbeActionCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("            <TD>")


Response.Write(mobjValues.PossiblesValues("cbeAction", "Table645", 1, Session("CO001_nAction"),  , True,  ,  ,  ,  , True,  , GetLocalResourceObject("cbeActionToolTip")))


Response.Write("</TD>" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("            <TD><LABEL ID=19644>" & GetLocalResourceObject("Anchor2Caption") & "</LABEL></TD>" & vbCrLf)
Response.Write("            <TD>")


Response.Write(mobjValues.DIVControl("lblTotCobDev", False, CStr(0)))


Response.Write("</TD>" & vbCrLf)
Response.Write("            <TD WIDTH=""10%"">&nbsp;</TD>" & vbCrLf)
Response.Write("            <TD><LABEL ID=19641>" & GetLocalResourceObject("cbeCurrencyCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("            <TD>")


Response.Write(mobjValues.PossiblesValues("cbeCurrency", "Table11", eFunctions.Values.eValuesType.clngComboType, CStr(1),  , True,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbeCurrencyToolTip")))


Response.Write("</TD>" & vbCrLf)
Response.Write("            ")


Response.Write(mobjValues.HiddenControl("hddnCurrency", CStr(1)))


Response.Write("" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("            <TD><LABEL ID=19643>" & GetLocalResourceObject("Anchor3Caption") & "</LABEL></TD>" & vbCrLf)
Response.Write("            <TD>")


Response.Write(mobjValues.DIVControl("lblTotIn", False, CStr(0)))


Response.Write("</TD>" & vbCrLf)
Response.Write("            <TD>&nbsp;</TD>" & vbCrLf)
Response.Write("            <TD><LABEL ID=19645>" & GetLocalResourceObject("Anchor4Caption") & "</LABEL></TD>" & vbCrLf)
Response.Write("            <TD>")


Response.Write(mobjValues.DIVControl("lblTotSaldo", False, CStr(0)))


Response.Write("</TD>" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("    </TABLE>")

	
End Sub
'% LoadHeader: Muestra todos los campos de la transaccion
'--------------------------------------------------------------------------------
Private Sub LoadHeader()
	'--------------------------------------------------------------------------------
	
Response.Write("" & vbCrLf)
Response.Write("" & vbCrLf)
Response.Write("    <TABLE WIDTH=""100%"" CELLSPACING=""10"">" & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("			<TD ALIGN=""LEFT"" CLASS=""HighLighted"" COLSPAN = 5><LABEL ID=10341><A NAME=""Origen de la relación"">" & GetLocalResourceObject("AnchorOrigen de la relaciónCaption") & "</A></LABEL></TD>" & vbCrLf)
Response.Write("		</TR>" & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("			<TD COLSPAN=5 CLASS=""HorLine""></TD>" & vbCrLf)
Response.Write("		</TR>" & vbCrLf)
Response.Write("" & vbCrLf)
Response.Write("        <TR ALIGN=CENTER>" & vbCrLf)
Response.Write("        	<TD>")


        Response.Write(mobjValues.OptionControl(0, "optRelOrigi", GetLocalResourceObject("optRelOrigi_CStr1Caption"), CStr(1), CStr(1), "EnableFields(this.value)", Session("nCashnum") = 0, 2, GetLocalResourceObject("optRelOrigi_CStr1ToolTip")))


Response.Write("</TD>" & vbCrLf)
Response.Write("			<TD>")


Response.Write(mobjValues.OptionControl(0, "optRelOrigi", GetLocalResourceObject("optRelOrigi_CStr2Caption"), CStr(0), CStr(2), "EnableFields(this.value)", Session("nCashnum") = 0, 2, GetLocalResourceObject("optRelOrigi_CStr2ToolTip")))


Response.Write("</TD>" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("    </TABLE>" & vbCrLf)
Response.Write("    " & vbCrLf)
Response.Write("    <BR>" & vbCrLf)
Response.Write("" & vbCrLf)
Response.Write("    <TABLE WIDTH=""100%"" CELLSPACING=""10"">" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("    		<TD WIDTH=""100%"" ALIGN=CENTER COLSPAN=""4""><LABEL>" & GetLocalResourceObject("cbeActionCaption") & "&nbsp;&nbsp;</LABEL>" & vbCrLf)
Response.Write("    		")

	mobjValues.BlankPosition = False
Response.Write("" & vbCrLf)
Response.Write("    		")


Response.Write(mobjValues.PossiblesValues("cbeAction", "Table645", 1, CStr(eCollection.ColformRef.TypeBordereaux.cstrcollect),  ,  ,  ,  ,  , "insChangeAction();",  ,  , GetLocalResourceObject("cbeActionToolTip")))


Response.Write("" & vbCrLf)
Response.Write("    		</TD>" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("        <TR COLSPAN=4>" & vbCrLf)
Response.Write("            <TD>&nbsp;</TD>" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("    		<TD WIDTH=""23%""><LABEL ID=10295>" & GetLocalResourceObject("tcnRelaNumCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("            <TD WIDTH=""20%"">")


Response.Write(mobjValues.NumericControl("tcnRelaNum", 10, vbNullString,  , GetLocalResourceObject("tcnRelaNumToolTip"),  , 0,  ,  ,  , "insChangeStatus();"))


Response.Write("</TD>" & vbCrLf)
Response.Write("            <TD WIDTH=""25%""><LABEL ID=10296>" & GetLocalResourceObject("tctStatusCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("            <TD>")


Response.Write(mobjValues.TextControl("tctStatus", 15, CStr(False),  , GetLocalResourceObject("tctStatusToolTip"),  ,  ,  ,  , True))


Response.Write("</TD>" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("			<TD COLSPAN=""2"">")


Response.Write(mobjValues.CheckControl("chkRentVital", GetLocalResourceObject("chkRentVitalCaption"), Request.QueryString.Item("chkRentVital"), "1", "ReloadPage(this);",  ,  , GetLocalResourceObject("chkRentVitalToolTip")))


Response.Write("</TD>" & vbCrLf)
Response.Write("    		<TD WIDTH=""16%""><LABEL>" & GetLocalResourceObject("cbeRel_TypeCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("    		")

	mobjValues.BlankPosition = True
Response.Write("" & vbCrLf)
Response.Write("    		<TD WIDTH=""34%"">" & vbCrLf)
Response.Write("   		" & vbCrLf)
Response.Write("            ")

	With Response
		mobjValues.TypeList = 1
		If Request.QueryString.Item("chkRentVital") = "1" Then
			mobjValues.List = "2,4,5"
		Else
			mobjValues.List = "1,2,3,4,5,6"
		End If
		.Write(mobjValues.PossiblesValues("cbeRel_Type", "table7502", eFunctions.Values.eValuesType.clngComboType, CStr(2),  ,  ,  ,  ,  , "insSetOperation();",  , 1, GetLocalResourceObject("cbeRel_TypeToolTip")))
	End With
	
Response.Write("   " & vbCrLf)
Response.Write("    		" & vbCrLf)
Response.Write("    		</TD>" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("        " & vbCrLf)
Response.Write("    </TABLE>    " & vbCrLf)
Response.Write("" & vbCrLf)
Response.Write("    <TABLE WIDTH=""100%"" CELLSPACING=""10"">" & vbCrLf)
Response.Write("        <TR>        " & vbCrLf)
Response.Write("		    <TD WIDTH=""23%""><LABEL ID=""10295"">" & GetLocalResourceObject("valCollectorCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("			<TD WIDTH=""20%"">")


Response.Write(mobjValues.PossiblesValues("valCollector", "tabCollector_Cliname", eFunctions.Values.eValuesType.clngWindowType,  ,  ,  ,  ,  ,  ,  , True, 10, GetLocalResourceObject("valCollectorToolTip")))


Response.Write("</TD>" & vbCrLf)
Response.Write("            <TD WIDTH=""25%""><LABEL ID=10290>" & GetLocalResourceObject("tcdCollectDateCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("            <TD>")


Response.Write(mobjValues.DateControl("tcdCollectDate", CStr(Today),  , GetLocalResourceObject("tcdCollectDateToolTip"),  ,  ,  , "InsValCash_dEffecdate(this.value);"))


Response.Write("</TD>" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("    </TABLE>" & vbCrLf)
Response.Write("" & vbCrLf)
Response.Write("    <TABLE WIDTH=""100%"" CELLSPACING=""10"">" & vbCrLf)
Response.Write("       <TR>" & vbCrLf)
Response.Write("            <TD WIDTH=""23%""><LABEL ID=10295>" & GetLocalResourceObject("cbeInputTypCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("            ")

	mobjValues.BlankPosition = False
Response.Write("" & vbCrLf)
Response.Write("            <TD>")


Response.Write(mobjValues.PossiblesValues("cbeInputTyp", "table5554", eFunctions.Values.eValuesType.clngComboType, CStr(1),  ,  ,  ,  ,  , "Activecollector(this.value);",  , 5, GetLocalResourceObject("cbeInputTypToolTip")))


Response.Write("</TD>" & vbCrLf)
Response.Write("       </TR>" & vbCrLf)
Response.Write("    </TABLE>" & vbCrLf)
Response.Write("    " & vbCrLf)
Response.Write("    <DIV ID=""DivTableClient"">" & vbCrLf)
Response.Write("    <TABLE WIDTH=""100%""  CELLSPACING=""10"">" & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("			<TD ALIGN=""LEFT"" CLASS=""HighLighted"" COLSPAN = 5><LABEL ID=10341><A NAME=""Datos de la relación"">" & GetLocalResourceObject("AnchorDatos de la relaciónCaption") & "</A></LABEL></TD>" & vbCrLf)
Response.Write("		</TR>" & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("			<TD COLSPAN=5 CLASS=""HorLine""></TD>" & vbCrLf)
Response.Write("		</TR>" & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("            <TD WIDTH=""23%""><LABEL ID=10288>" & GetLocalResourceObject("dtcClientCaption") & "</LABEL></TD>            " & vbCrLf)
Response.Write("            <TD COLSPAN=""2"">")


Response.Write(mobjValues.ClientControl("dtcClient", Request.QueryString.Item("dtcClient"),  , GetLocalResourceObject("dtcClientToolTip"), "ChangeValues()", True, "lblCliename", False,  ,  ,  ,  , 18,  , True,  , True))


Response.Write("</TD>" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("        " & vbCrLf)
Response.Write("    </TABLE> " & vbCrLf)
Response.Write("    </DIV>   " & vbCrLf)
Response.Write("        " & vbCrLf)
Response.Write("" & vbCrLf)
Response.Write("    <DIV ID=""DivTableVentanilla"">" & vbCrLf)
Response.Write("    <TABLE  WIDTH=""100%""  CELLSPACING=""10"">" & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("			<TD ALIGN=""LEFT"" CLASS=""HighLighted"" COLSPAN = 5><LABEL ID=10341><A NAME=""Datos de la relación"">" & GetLocalResourceObject("AnchorDatos de la relaciónCaption") & "</A></LABEL></TD>" & vbCrLf)
Response.Write("		</TR>" & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("			<TD COLSPAN=5 CLASS=""HorLine""></TD>" & vbCrLf)
Response.Write("		</TR>" & vbCrLf)
Response.Write("    " & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("    	    <TD WIDTH=""23%""><LABEL>" & GetLocalResourceObject("cbeBankCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("                ")

	Call mobjValues.Parameters.Add("sType_Bankagree", "2", eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
Response.Write(" " & vbCrLf)
Response.Write("    	    <TD WIDTH=""20%"">")


Response.Write(mobjValues.PossiblesValues("cbeBank", "tabBank_Agree_Banks", eFunctions.Values.eValuesType.clngComboType,  , True,  ,  ,  ,  , "GetCod_Agree(this);self.document.forms[0].valBank_Agree.Parameters.Param1.sValue=this.value",  , 10, GetLocalResourceObject("cbeBankToolTip")))


Response.Write("</TD>" & vbCrLf)
Response.Write("    	    <TD WIDTH=""25%""><LABEL ID=10294>" & GetLocalResourceObject("valBank_AgreeCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("    	        ")

	mobjValues.Parameters.Add("nBank", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
Response.Write("    		    		    " & vbCrLf)
Response.Write("    	    <TD>")


Response.Write(mobjValues.PossiblesValues("valBank_Agree", "tabBank_Agree_Cta", 2, vbNullString, True,  ,  ,  ,  ,  ,  , 5, GetLocalResourceObject("valBank_AgreeToolTip")))


Response.Write("</TD>" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("        " & vbCrLf)
Response.Write("    </TABLE>   " & vbCrLf)
Response.Write("    </DIV>" & vbCrLf)
Response.Write("    " & vbCrLf)
Response.Write("    <DIV ID=""DivTablePlanilla"">" & vbCrLf)
Response.Write("    <TABLE  WIDTH=""100%""  CELLSPACING=""10""> " & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("			<TD ALIGN=""LEFT"" CLASS=""HighLighted"" COLSPAN = 5><LABEL ID=10341><A NAME=""Datos de la relación"">" & GetLocalResourceObject("AnchorDatos de la relaciónCaption") & "</A></LABEL></TD>" & vbCrLf)
Response.Write("		</TR>" & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("			<TD COLSPAN=5 CLASS=""HorLine""></TD>" & vbCrLf)
Response.Write("		</TR>" & vbCrLf)
Response.Write("    " & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("            <TD WIDTH=""23%""><LABEL>" & GetLocalResourceObject("valAgreementCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("            <TD WIDTH=""20%"">")


Response.Write(mobjValues.PossiblesValues("valAgreement", "tabAgreement", eFunctions.Values.eValuesType.clngWindowType,  ,  ,  ,  ,  ,  ,  ,  , 5, GetLocalResourceObject("valAgreementToolTip")))


Response.Write("</TD>" & vbCrLf)
Response.Write("            <TD WIDTH=""25%""><LABEL ID=10290>" & GetLocalResourceObject("tcdCollectCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("            <TD>")


Response.Write(mobjValues.DateControl("tcdCollect", CStr(Today),  , GetLocalResourceObject("tcdCollectToolTip")))


Response.Write("</TD>" & vbCrLf)
Response.Write("    	</TR>" & vbCrLf)
Response.Write("    	" & vbCrLf)
Response.Write("    </TABLE>" & vbCrLf)
Response.Write("    </DIV>" & vbCrLf)
Response.Write("    " & vbCrLf)
Response.Write("    <DIV ID=""DivTableValueDate"">" & vbCrLf)
Response.Write("    <TABLE  WIDTH=""100%""  CELLSPACING=""10""> " & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("            <TD WIDTH=""23%""><LABEL>" & GetLocalResourceObject("tcdValueDateCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("            <TD>")


Response.Write(mobjValues.DateControl("tcdValueDate",  ,  , GetLocalResourceObject("tcdValueDateToolTip")))


Response.Write("</TD>" & vbCrLf)
Response.Write("    	</TR>" & vbCrLf)
Response.Write("    	" & vbCrLf)
Response.Write("    </TABLE>" & vbCrLf)
Response.Write("    </DIV>    " & vbCrLf)
Response.Write("" & vbCrLf)
Response.Write("    <DIV ID=""DivTablePolicy"">" & vbCrLf)
Response.Write("    <TABLE  WIDTH=""100%""  CELLSPACING=""10""> " & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("			<TD ALIGN=""LEFT"" CLASS=""HighLighted"" COLSPAN = 5><LABEL ID=10341><A NAME=""Datos de la relación"">" & GetLocalResourceObject("AnchorDatos de la relaciónCaption") & "</A></LABEL></TD>" & vbCrLf)
Response.Write("		</TR>" & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("			<TD COLSPAN=5 CLASS=""HorLine""></TD>" & vbCrLf)
Response.Write("		</TR>" & vbCrLf)
Response.Write("    " & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("            <TD WIDTH=""23%""><LABEL ID=10286>" & GetLocalResourceObject("cbeBranchCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("			<TD WIDTH=""20%"">")


        Response.Write(mobjValues.BranchControl("cbeBranch", GetLocalResourceObject("cbeBranchToolTip"), CStr(1), , , , , , False))


Response.Write("</TD>" & vbCrLf)
Response.Write("            <TD WIDTH=""25%""><LABEL ID=10294>" & GetLocalResourceObject("valProductCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("            <TD COLSPAN=2>")


        Response.Write(mobjValues.ProductControl("valProduct", GetLocalResourceObject("valProductToolTip"), , eFunctions.Values.eValuesType.clngWindowType, False))


Response.Write("</TD>" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("            <TD WIDTH=""23%""><LABEL ID=10293>" & GetLocalResourceObject("tcnPolicyCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("            <TD WIDTH=""20%"">")


Response.Write(mobjValues.NumericControl("tcnPolicy", 9, vbNullString,  , GetLocalResourceObject("tcnPolicyToolTip"),  , 0,  ,  ,  , "insShowValues(""Policy"")"))


Response.Write("</TD>" & vbCrLf)
Response.Write("            <TD WIDTH=""25%""><LABEL ID=10287>" & GetLocalResourceObject("tcnCertifCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("            <TD>")


Response.Write(mobjValues.NumericControl("tcnCertif", 9, vbNullString,  , GetLocalResourceObject("tcnCertifToolTip"),  , 0))


Response.Write("</TD>" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("	</TABLE>" & vbCrLf)
Response.Write("	</DIV>" & vbCrLf)
Response.Write("" & vbCrLf)
Response.Write("    ")


Response.Write(mobjValues.HiddenControl("hddnCurrency", CStr(1)))


Response.Write("" & vbCrLf)
Response.Write("	" & vbCrLf)
Response.Write("	")

Response.Write("")

	
	'+ Se inicializan los campos de la ventana.
	Response.Write("<SCRIPT>insChangeAction();insSetOperation();DisabledField();</" & "Script>")
	If Request.QueryString.Item("sLinkSpecial") = "1" Then
		Response.Write("<SCRIPT>insShowLinkSpecial(" & Request.QueryString.Item("nBordereaux") & ");</" & "Script>")
	End If
End Sub

</script>
<%Response.Expires = -1
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage("CO001_k")
mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 3/4/03 11.58.23
mobjValues.sSessionID = Session.SessionID
mobjValues.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility
mobjValues.sCodisplPage = "CO001_k"

If CStr(Session("nCashnum")) <> "" And CStr(Session("nCashnum")) <> "0" Then
	lclsCash_stat = New eCashBank.Cash_stat
	lobjErrors = New eGeneral.GeneralFunction
	
	If Request.QueryString.Item("sConfig") <> "InSequence" Then
		If lclsCash_stat.valCash_statClosed(Session("nCashnum"), Today) Then
			lstrAlert = "Err. 60129 " & lobjErrors.insLoadMessage(60129)
			Response.Write("<SCRIPT>alert('" & lstrAlert & "')</SCRIPT>")
		End If
	End If
	
	lclsCash_stat = Nothing
	lobjErrors = Nothing
End If

If Request.QueryString.Item("sLinkSpecial") = "1" Then
	Response.Write("<SCRIPT>var sLinkSpecial='1'</SCRIPT>")
Else
	Response.Write("<SCRIPT>var sLinkSpecial='0'</SCRIPT>")
End If
%>
<HTML>
<HEAD>
	<META NAME = "GENERATOR" CONTENT="Microsoft Visual Studio 6.0">
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/Constantes.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tmenu.js"></SCRIPT>




	<%=mobjValues.StyleSheet()%>								
	<SCRIPT LANGUAGE= "JavaScript">
//+ Variable para el control de versiones
    document.VssVersion="$$Revision: 14 $|$$Date: 12/05/04 10:59 $"
    
	var sCodisplOrig    
	sCodisplOrig = "<%=Request.QueryString.Item("sCodisplOrig")%>"    
//ChangeValues: Cambia y asigna los valores según la opción seleccionada.
//Enlace NovaRed.
//-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------
function ChangeValues(){
//-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    with (self.document.forms[0]){
        if(dtcClient.value!="")
    		insDefValuesNR('Client', 'sClient=' + dtcClient.value, 'sDigit=' + dtcClient_Digit.value , 'sForm=' + self.document.forms[0].name,'/VTimeNet/Collection/CollectionSeq')
    }
}
//-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------
function InsValCash_dEffecdate(sDate){
//-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    with (self.document.forms[0]){
        if(sDate!="")
    		insDefValues("ValCash_dEffecdate","dEffecdate=" + sDate, '/VTimeNet/Collection/CollectionSeq');
    }
}
//% insStateZone: se establece el estado de la página
//--------------------------------------------------------------------------------------------
function insStateZone(){
//--------------------------------------------------------------------------------------------
}
//%insFinish. Esta función es utilizada para realizar cambios al momento de finalizar la transacción
//--------------------------------------------------------------------------------    
function insFinish(){
//--------------------------------------------------------------------------------    
    return(true);
}
//%insShowLinkSpecial. Actualiza los valores y desabilita los campos cuando se llama en modo consulta la transacción
//--------------------------------------------------------------------------------    
function insShowLinkSpecial(nBordereaux){
//--------------------------------------------------------------------------------    
	with(self.document.forms[0]){
		cbeAction.value=2;
		insChangeAction();
		tcnRelaNum.value = nBordereaux;
		insChangeStatus();
		tcnRelaNum.disabled = true;
		cbeAction.disabled = true;
		}
}

//% insCancel: ejecuta la acción Cancelar de la página
//--------------------------------------------------------------------------------------------
function insCancel(){
//--------------------------------------------------------------------------------------------
    var lintAction; 

    <%If CStr(Session("CO001_nAction")) <> vbNullString Then%>
          lintAction=<%=Session("CO001_nAction")%>;
    <%Else%>
          lintAction=0;
    <%End If%>
    if(top.frames["fraSequence"].pintZone==2){
	    if(lintAction==<%=eCollection.ColformRef.TypeActionsSeqColl.cstrAdd%> ||
		   lintAction==<%=eCollection.ColformRef.TypeActionsSeqColl.cstrUpdate%>){
			ShowPopUp("/VTimeNet/Common/GE101.aspx?sCodispl=CO001_K","EndProcess",300,150)
		}
		else
			{
			if (sLinkSpecial == '1')
				top.close(); 
			return(true);
			}
	}
	else {
		if (sLinkSpecial == '1')
			top.close(); 
	    return(true);
	}
}
//% insDisableAll: Permite deshabilitar todos los controles de la ventana.
//--------------------------------------------------------------------------------------------
function insDisableAll(){
//--------------------------------------------------------------------------------------------
    var lintIndex = 0;
    with(self.document.forms[0]){
        for (lintIndex=0;lintIndex<document.forms[0].length;lintIndex++)
        {
			elements[lintIndex].disabled=true;
		}
		btndtcClient.disabled=true;
		btnvalAgreement.disabled=true;
		btnvalBank_Agree.disabled=true;
		btn_tcdCollectDate.disabled=true;
		cbeRel_Type.disabled=true;
		chkRentVital.disabled=true;
		A304.disabled=true;
		A390.disabled=true;
		A391.disabled=true;
    }
}

//%EnableFields: Habilita los campos según la opción seleccionada
//------------------------------------------------------------------------------------------------
function EnableFields(nValue){
//------------------------------------------------------------------------------------------------
	switch(nValue)
	{
		case "1": //Recaudación
		{
			self.document.forms[0].elements["chkRentVital"].disabled=false;
			self.document.forms[0].elements["cbeRel_Type"].value=2;
			self.document.forms[0].elements["cbeRel_Type"].disabled=false;
			break;
		}
		case "2": //Entrada de conceptos
		{
			self.document.forms[0].elements["chkRentVital"].disabled=true;
			self.document.forms[0].elements["cbeRel_Type"].value=5;
			self.document.forms[0].elements["cbeRel_Type"].disabled=true;
			break;
		}
	}
	with (self.document.forms[0]){
        cbeBank.value = "";
        valBank_Agree.value = "";
		valBank_Agree.Parameters.Param1.sValue=0;
        UpdateDiv("valBank_AgreeDesc", "");
        valAgreement.value = "";
        UpdateDiv("valAgreementDesc", "");
        cbeBranch.value = "";
        valProduct.value = "";
        UpdateDiv("valProductDesc", "");
        tcnPolicy.value = '';
        tcnCertif.value = '';
        dtcClient.value = "";
        dtcClient_Digit.value = "";
		UpdateDiv("lblCliename", '');
        tcdCollectDate.value = GetDateSystem();
   }
   insSetOperation();
}

//% Activecollector: Permite deshabilitar todos los controles de la ventana.
//--------------------------------------------------------------------------------------------
function Activecollector(nValue){
//--------------------------------------------------------------------------------------------
    if (nValue == 2)
        with(self.document.forms[0]){
            valCollector.disabled=false;
            btnvalCollector.disabled=false;
        }
    else
        with(self.document.forms[0]){
            valCollector.value = ""
            UpdateDiv('valCollectorDesc',"");
            valCollector.disabled=true;
            btnvalCollector.disabled=true;
        }
}
//% insShowValues:Habilita o deshabilita el campo Certificado dependiendo del tipo de póliza pasada como parámetro.
//-------------------------------------------------------------------------------------------
function insShowValues(sField){
//-------------------------------------------------------------------------------------------
	with(self.document.forms[0]){
		switch(sField){
			case "Policy":
				if(tcnPolicy.value!="")
				    insDefValues("ShowCertif", "nPolicy=" + tcnPolicy.value + '&nProduct=' + valProduct.value + '&nBranch=' + cbeBranch.value)
				break;
		}
	}
}
//% insChangeStatus: Muestra el Estado de la Relación
//-------------------------------------------------------------------------------------------
function insChangeStatus(){
//-------------------------------------------------------------------------------------------
    if (document.forms[0].cbeAction.value == 1){
        document.forms[0].tctStatus.value = "Pendiente"
    }
    else {
		lstrQueryString = "nBordereaux=" + document.forms[0].tcnRelaNum.value +
						  "&nAction=" + document.forms[0].cbeAction.value +
						  "&nCashnum=" + <%=Session("nCashnum")%>;
        insDefValues("CO001_K", lstrQueryString, '/VTimeNet/Collection/CollectionSeq');
    }
}

//%GetCod_Agree: Obtiene el código interno de la cuenta bancaria para asignarla al número de convenio 
//               Si los conceptos son: Pago en ventanilla o Deposito PAC/Transbank
//-------------------------------------------------------------------------------------------------------------------------
function GetCod_Agree(nBank){
//-------------------------------------------------------------------------------------------------------------------------
        insDefValues("Cod_Agree2", "nBank_Agree=" + nBank.value + "&sRelType=" + self.document.forms[0].cbeRel_Type.value, '/VTimeNet/Collection/Collectionseq');
}

//% insChangeAction: Coloca los valores por defecto de los campos dependiendo de la acción
//-------------------------------------------------------------------------------------------
function insChangeAction(){
//-------------------------------------------------------------------------------------------

var nBordereaux
var lstrString
nBordereaux  = "<%=Request.QueryString.Item("nBordereaux")%>"

   with (self.document.forms[0]){
        if (cbeAction.value != ""){
            cbeInputTyp.value = 1;
            cbeBank.value = "";
            valBank_Agree.value = "";
		    valBank_Agree.Parameters.Param1.sValue=0;
            UpdateDiv("valBank_AgreeDesc", "");
            valAgreement.value = "";
            UpdateDiv("valAgreementDesc", "");
            cbeBranch.value = "";
            valProduct.value = "";
            UpdateDiv("valProductDesc", "");
            tcnPolicy.value = '';
            tcnCertif.value = '';
            dtcClient.value = "";
            dtcClient_Digit.value = "";
		    UpdateDiv("lblCliename", "");
            tcdCollectDate.value = GetDateSystem();
            tcnRelaNum.value = 0;
            cbeBank.disabled = true;
            valBank_Agree.disabled = true;
            btnvalBank_Agree.disabled = true;
            valAgreement.disabled = true;
            btnvalAgreement.disabled = true;
            tcnPolicy.disabled = true;
            tcnCertif.disabled = true;
            dtcClient.disabled = true;
            btndtcClient.disabled = true;
            tcdCollectDate.disabled = false;
            btn_tcdCollectDate.disabled = false;
            switch (cbeAction.value){
                case "<%=eCollection.ColformRef.TypeActionsSeqColl.cstrAdd%>":
                    <%If Request.QueryString.Item("sCodispl") = "CO001_K" Then%>
                        optRelOrigi[0].disabled =false;
                        optRelOrigi[1].disabled =false;
                    <%Else%>
                        optRelOrigi[0].disabled =true;
                        optRelOrigi[1].disabled =true;
                    <%End If%>
                    
                    optRelOrigi[0].checked = 1;
                    optRelOrigi[0].value   = 1;
                    optRelOrigi[1].value   = 2;
                    cbeInputTyp.disabled = false;
                    cbeRel_Type.disabled = false;
                    chkRentVital.disabled=false;
                    tcnRelaNum.disabled = false;
                    tctStatus.value = "Pendiente";
                    insSetOperation();
                    break;
                case "<%=eCollection.ColformRef.TypeActionsSeqColl.cstrQuery%>":
				    tcdCollectDate.disabled = true;
				    btn_tcdCollectDate.disabled = true;
                    optRelOrigi[0].disabled = true;
                    optRelOrigi[1].disabled = true;
                    cbeInputTyp.disabled = true;
                    cbeRel_Type.disabled = true;
                    dtcClient.disabled = true;
                    btndtcClient.disabled = true;
				    chkRentVital.disabled=true;
                    tcnRelaNum.disabled = false;
                    tctStatus.value = "";				    
                    break;
                default:
                    optRelOrigi[0].disabled = true;
                    optRelOrigi[1].disabled = true;
                    cbeInputTyp.disabled = true;
                    cbeRel_Type.disabled = true;
                    dtcClient.disabled = true;
                    btndtcClient.disabled = true;
				    chkRentVital.disabled=true;
                    tcnRelaNum.disabled = false;
                    tctStatus.value = "";
                    break;
            }
        } 
        
        if (sCodisplOrig == "OPC001"){
			cbeAction.disabled  = true;
			cbeAction.value     = 2;
			tcnRelaNum.value    = nBordereaux;
			
            optRelOrigi[0].disabled = true;
            optRelOrigi[1].disabled = true;
            cbeInputTyp.disabled = true;
            cbeRel_Type.disabled = true;
            dtcClient.disabled = true;
            btndtcClient.disabled = true;
			chkRentVital.disabled = true;
			
			lstrString = "nBordereaux=" + document.forms[0].tcnRelaNum.value +
						 "&nAction=" + document.forms[0].cbeAction.value +
						 "&nCashnum=" + <%=Session("nCashnum")%>;
			insDefValues("CO001_K", lstrString, '/VTimeNet/Collection/CollectionSeq');
			
        }
    }
}
// insSetOperation: Habilita y deshabilita los campos por el tipo de relación
//-------------------------------------------------------------------------------------------
function insSetOperation(){
//-------------------------------------------------------------------------------------------
    var lblnDisabled
    
	with (self.document.forms[0]){
        lblnDisabled = cbeAction.value==1?false:true;
        valBank_Agree.value = "";
		valBank_Agree.Parameters.Param1.sValue=0;
        UpdateDiv("valBank_AgreeDesc", "");
        valAgreement.value = "";
        UpdateDiv("valAgreementDesc", "");
        cbeBranch.value = "";
        valProduct.value = "";
        UpdateDiv("valProductDesc", "");
        tcnPolicy.value = '';
        tcnCertif.value = '';
        dtcClient.value = "";
        dtcClient_Digit.value = "";
		UpdateDiv("lblCliename", "");
        //tcdCollectDate.value = "";        

        switch (cbeRel_Type.value){
            case "1":
                cbeBank.disabled = true;
				valBank_Agree.disabled = true;
				btnvalBank_Agree.disabled = true;
				tcdCollect.disabled = lblnDisabled;
				btn_tcdCollect.disabled = lblnDisabled;
				tcdValueDate.disabled = lblnDisabled;
				btn_tcdValueDate.disabled = lblnDisabled;
				valAgreement.disabled = lblnDisabled;
				btnvalAgreement.disabled = lblnDisabled;
				tcnPolicy.disabled = true;
				tcnCertif.disabled = true;
				dtcClient.disabled = true;
				btndtcClient.disabled = true;
				tcdCollectDate.disabled = lblnDisabled;
				btn_tcdCollectDate.disabled = lblnDisabled;
				ShowDiv('DivTablePlanilla', 'show');				
				ShowDiv('DivTableValueDate', 'show');
                ShowDiv('DivTableVentanilla', 'hide');								
                ShowDiv('DivTablePolicy', 'hide');								
                ShowDiv('DivTableClient', 'hide');
                
				break;
			case "2":
                cbeBank.disabled = true;
				valBank_Agree.disabled = true;
				btnvalBank_Agree.disabled = true;
				valAgreement.disabled = true;
				btnvalAgreement.disabled = true;
				tcnCertif.disabled = true;
				dtcClient.disabled = true;
				btndtcClient.disabled = true;
				if (sCodisplOrig == "OPC001"){
					tcnPolicy.disabled = true;				
					tcdCollectDate.disabled = true;
					btn_tcdCollectDate.disabled = true;
				}
				else{
					tcnPolicy.disabled = lblnDisabled;				
					tcdCollectDate.disabled = lblnDisabled;
					btn_tcdCollectDate.disabled = lblnDisabled;
				}
				ShowDiv('DivTablePolicy', 'show');
				ShowDiv('DivTablePlanilla', 'hide');				
                ShowDiv('DivTableVentanilla', 'hide');								
                ShowDiv('DivTableClient', 'hide');
                ShowDiv('DivTableValueDate', 'hide');
				break;
			case "3":
                cbeBank.disabled = lblnDisabled;
				valBank_Agree.disabled = lblnDisabled;
				btnvalBank_Agree.disabled = lblnDisabled;
				tcdCollect.disabled = lblnDisabled;
				btn_tcdCollect.disabled = lblnDisabled;
				tcdValueDate.disabled = lblnDisabled;
				btn_tcdValueDate.disabled = lblnDisabled;
				valAgreement.disabled = true;
				btnvalAgreement.disabled = true;
				tcnPolicy.disabled = true;
				tcnCertif.disabled = true;
				dtcClient.disabled = true;
				btndtcClient.disabled = true;
				tcdCollectDate.disabled = lblnDisabled;
				btn_tcdCollectDate.disabled = lblnDisabled;
				ShowDiv('DivTableVentanilla', 'show');
				ShowDiv('DivTableValueDate', 'show');
				ShowDiv('DivTablePolicy', 'hide');
				ShowDiv('DivTablePlanilla', 'hide');				
                ShowDiv('DivTableClient', 'hide');
								
				break;
			case "4":
                cbeBank.disabled = true;
				valBank_Agree.disabled = true;
				btnvalBank_Agree.disabled = true;
				valAgreement.disabled = true;
				btnvalAgreement.disabled = true;
				tcnPolicy.disabled = true;
				tcnCertif.disabled = true;
				dtcClient.disabled = lblnDisabled;
				btndtcClient.disabled = lblnDisabled;
				tcdCollectDate.disabled = lblnDisabled;
				btn_tcdCollectDate.disabled = lblnDisabled;
				ShowDiv('DivTableClient', 'show');	
				ShowDiv('DivTablePolicy', 'hide');
				ShowDiv('DivTablePlanilla', 'hide');				
                ShowDiv('DivTableVentanilla', 'hide');								
                ShowDiv('DivTableValueDate', 'hide');
                							
				break;
			case "6":
			    //cbeRel_Type.value=0;				
				ShowDiv('DivTableClient', 'hide');	
				ShowDiv('DivTablePolicy', 'hide');
				ShowDiv('DivTablePlanilla', 'hide');				
                ShowDiv('DivTableVentanilla', 'hide');								
                ShowDiv('DivTableValueDate', 'hide');
				break;
            default:
                cbeBank.disabled = true;
				valBank_Agree.disabled = true;
				btnvalBank_Agree.disabled = true;
				valAgreement.disabled = true;
				btnvalAgreement.disabled = true;
				tcnPolicy.disabled = true;
				tcnCertif.disabled = true;
				dtcClient.disabled = true;
				btndtcClient.disabled = true;
				tcdCollectDate.disabled = lblnDisabled;
				btn_tcdCollectDate.disabled = lblnDisabled;
                ShowDiv('DivTablePlanilla', 'hide');
                ShowDiv('DivTableVentanilla', 'hide');								
                ShowDiv('DivTablePolicy', 'hide');								
                ShowDiv('DivTableClient', 'hide');																
                ShowDiv('DivTableValueDate', 'hide');
                break;
        }
    }
}
//% DisabledField: Inhabilita algunos campos de la ventana dependiendo
//-----------------------------------------------------------------------------------
function DisabledField(){
//-----------------------------------------------------------------------------------
   
	if(typeof(self.document.forms[0].elements["chkRentVital"])!='undefined')
	{
		with (self.document.forms[0]){
			if(chkRentVital.checked)
			{
				cbeRel_Type.value=2;
				chkRentVital.value=1;
				insSetOperation();
				valProduct.sTabName="Tabprodmaster3"
			}
			else
			{
				cbeRel_Type.value=2;
				chkRentVital.value="";
				valProduct.sTabName="Tabprodmaster1"
			}
		}
	}
}
//% ReloadPage: Recarga la página y asigna los valores almacenados en el QueryString - ACM - 18/07/2002
//-----------------------------------------------------------------------------------------------------
function ReloadPage(objField)
//-----------------------------------------------------------------------------------------------------
{
	var lstrLocation = '';
	lstrLocation += document.location.href;
	lstrLocation = lstrLocation.replace(/&chkRentVital.*/,"")
	if (objField.checked)
		lstrLocation  = lstrLocation + "&chkRentVital=1"
	else
		lstrLocation  = lstrLocation + "&chkRentVital="
	document.location.href = lstrLocation;
}

</SCRIPT>
<%
mobjMenu = New eFunctions.Menues
'^Begin Body Block VisualTimer Utility 1.1 3/4/03 11.58.23
mobjMenu.sSessionID = Session.SessionID
mobjMenu.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility
If Request.QueryString.Item("sCodispl") = vbNullString Then
	Response.Write(mobjMenu.MakeMenu(Session("sCodispl_Aux"), "CO001_K.aspx", 1, Request.QueryString.Item("sWindowDescript"), Session("sDesMultiCompany"), Session("sSche_code")))
Else
	Response.Write(mobjMenu.MakeMenu(Request.QueryString.Item("sCodispl"), "CO001_K.aspx", 1, Request.QueryString.Item("sWindowDescript"), Session("sDesMultiCompany"), Session("sSche_code")))
End If
mobjMenu = Nothing
%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" ID="FORM" NAME="frmCO001_k" ACTION="valCollectionSeq.aspx?time=1&sLinkSpecial=<%=Request.QueryString.Item("sLinkSpecial")%>">
<BR><BR>
<%If Request.QueryString.Item("sConfig") = "InSequence" Then
	Call LoadPageInSequence()
Else
	Call LoadHeader()
End If
%>
</FORM>
</BODY>
</HTML>
</BODY>
</HTML>
<%

If Request.QueryString.Item("sConfig") = "InSequence" Then
%>
    <SCRIPT>
    top.frames["fraSequence"].pintZone=2;
    pstrCodispl='CO001';
    </SCRIPT>
<%
End If

'^Begin Footer Block VisualTimer Utility 1.1 3/4/03 11.58.23
Call mobjNetFrameWork.FinishPage("CO001_k")
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>





