<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">
'^Begin Header Block VisualTimer Utility 1.1 7/4/03 10.53.47
Dim mobjNetFrameWork As eNetFrameWork.Layout
'~End Header Block VisualTimer Utility

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'- Objeto para el manejo del menú
Dim mobjMenu As eFunctions.Menues

'- Objeto para el manejo particular de los datos de la página
Dim mcolPremiums As Object

'- Objeto para el manejo de las variables particulares de la transacción
Dim mstrString As String


Sub LoadHeader()
	
Response.Write("" & vbCrLf)
Response.Write("  <TABLE WIDTH=""100%"">" & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("			<TD><LABEL ID=0>" & GetLocalResourceObject("tcdCollectDateCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("			<TD>")


Response.Write(mobjValues.DateControl("tcdCollectDate", Request.QueryString.Item("dCollectDate"),  , GetLocalResourceObject("tcdCollectDateToolTip"), True,  ,  ,  , True))


Response.Write("</TD>" & vbCrLf)
Response.Write("	        <TD WIDTH=""25%""><LABEL ID=0>" & GetLocalResourceObject("AnchorCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("	        <TD>")


Response.Write(mobjValues.DIVControl("lblBulletins", False, Request.QueryString.Item("nBulletins")))


Response.Write("</TD>			" & vbCrLf)
Response.Write("		</TR>" & vbCrLf)
Response.Write("		" & vbCrLf)
Response.Write("	    ")

	mstrString = Request.Params.Get("Query_String")
	With Response
		.Write("<SCRIPT>")
		.Write("top.fraSequence.plngMainAction=" & Request.QueryString.Item("nMainAction") & ";top.fraFolder.document.location =""CO632A.aspx?sCodispl=CO632A&" & Request.Params.Get("Query_String") & """;")
		.Write("</" & "Script>")
	End With
Response.Write("" & vbCrLf)
Response.Write("		<TR>	" & vbCrLf)
Response.Write("            <TD WIDTH=""30%""><LABEL>" & GetLocalResourceObject("Anchor2Caption") & "</LABEL></TD>" & vbCrLf)
Response.Write("            <TD>")


Response.Write(mobjValues.DIVControl("lblTotSaldo", False, CStr(0)))


Response.Write("</TD>" & vbCrLf)
Response.Write("            <TD><LABEL>" & GetLocalResourceObject("cbeCurrencyCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("            <TD>")


Response.Write(mobjValues.PossiblesValues("cbeCurrency", "Table11", eFunctions.Values.eValuesType.clngComboType, Request.QueryString.Item("nCurrencyBul"),  , True,  ,  ,  ,  , True,  , GetLocalResourceObject("cbeCurrencyToolTip")))


Response.Write("</TD>" & vbCrLf)
Response.Write("        </TR>                " & vbCrLf)
Response.Write("		<TR>            " & vbCrLf)
Response.Write("			<TD><LABEL>" & GetLocalResourceObject("Anchor3Caption") & "</LABEL></TD>" & vbCrLf)
Response.Write("            <TD>")


Response.Write(mobjValues.DIVControl("lblStatus", False, Request.QueryString.Item("sStatus")))


Response.Write("</TD>" & vbCrLf)
Response.Write("        </TR>                " & vbCrLf)
Response.Write("	</TABLE>")

End Sub
Sub LoadFolder()
	
Response.Write("" & vbCrLf)
Response.Write("<TABLE WIDTH=""100%"">" & vbCrLf)
Response.Write("	<TR>" & vbCrLf)
Response.Write("		<TD WIDTH=""23%"">&nbsp</TD>" & vbCrLf)
Response.Write("		<TD WIDTH=""23%"">&nbsp</TD>" & vbCrLf)
Response.Write("		<TD WIDTH=""8%"">&nbsp</TD>" & vbCrLf)
Response.Write("		<TD WIDTH=""23%"">&nbsp</TD>" & vbCrLf)
Response.Write("		<TD WIDTH=""23%"">&nbsp</TD>" & vbCrLf)
Response.Write("	</TR>" & vbCrLf)
Response.Write("	<TR>" & vbCrLf)
Response.Write("		<TD><LABEL ID=0>" & GetLocalResourceObject("tcdCollectDateCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("		<TD>")


Response.Write(mobjValues.DateControl("tcdCollectDate", CStr(Today),  , GetLocalResourceObject("tcdCollectDateToolTip"),  ,  ,  ,  , True))


Response.Write("</TD>" & vbCrLf)
Response.Write("		<TD>&nbsp</TD>" & vbCrLf)
Response.Write("		<TD WIDTH=""30%""><LABEL ID=0>" & GetLocalResourceObject("tcnBulletinsCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("		<TD>")


Response.Write(mobjValues.NumericControl("tcnBulletins", 8, Request.QueryString.Item("nBulletins"),  , GetLocalResourceObject("tcnBulletinsToolTip"),  ,  ,  ,  ,  , "insShowDataBulletins(this)", True))


Response.Write("</TD>										" & vbCrLf)
Response.Write("	</TR>		" & vbCrLf)
Response.Write("	<TR>" & vbCrLf)
Response.Write("		<TD><LABEL ID=0>" & GetLocalResourceObject("cbeInsur_areaCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("		<TD>")


Response.Write(mobjValues.PossiblesValues("cbeInsur_area", "Table5001", eFunctions.Values.eValuesType.clngComboType,  ,  ,  ,  ,  ,  , "insChangeInsur_area(this)", True,  , GetLocalResourceObject("cbeInsur_areaToolTip")))


Response.Write(" </TD>" & vbCrLf)
Response.Write("		<TD>&nbsp</TD>" & vbCrLf)
Response.Write("        <TD><DIV ID=""divCollect"">")


Response.Write(mobjValues.CheckControl("chkCollect_exp", GetLocalResourceObject("chkCollect_expCaption"), "1", "1",  , True))


Response.Write("</DIV><DIV ID=""divStatus""><LABEL ID=0>" & GetLocalResourceObject("Anchor4Caption") & "</LABEL></DIV></TD>" & vbCrLf)
Response.Write("        <TD>")


Response.Write(mobjValues.DIVControl("lblStatus", False))


Response.Write("</TD>" & vbCrLf)
Response.Write("        ")


Response.Write(mobjValues.HiddenControl("tctStatus", "En Proceso"))


Response.Write("" & vbCrLf)
Response.Write("    </TR>" & vbCrLf)
Response.Write("    <TR>		" & vbCrLf)
Response.Write("		<TD COLSPAN=2><P ALIGN=RIGHT CLASS=""HIGHLIGHTED""><LABEL id=0>" & GetLocalResourceObject("Anchor5Caption") & "</LABEL></P></TD>" & vbCrLf)
Response.Write("		<TD>&nbsp</TD>" & vbCrLf)
Response.Write("		<TD COLSPAN=""4"" CLASS=""HighLighted"" ID=""tdCondi""><LABEL ID=40421><A NAME=""Período a consultar"">" & GetLocalResourceObject("AnchorPeríodo a consultarCaption") & "</A></LABEL></TD>" & vbCrLf)
Response.Write("	</TR>" & vbCrLf)
Response.Write("	<TR>" & vbCrLf)
Response.Write("		<TD COLSPAN=2 CLASS=""Horline""></TD>" & vbCrLf)
Response.Write("		<TD></TD>" & vbCrLf)
Response.Write("		<TD COLSPAN=2 CLASS=""Horline"" ID=""tdCondi1""></TD>" & vbCrLf)
Response.Write("	</TR>" & vbCrLf)
Response.Write("	<TR>" & vbCrLf)
Response.Write("		<TD COLSPAN=2>" & vbCrLf)
Response.Write("			<TABLE WIDTH=""100%"">" & vbCrLf)
Response.Write("				<TD><P ALIGN=LEFT>")


Response.Write(mobjValues.OptionControl(0, "optStyle_bull", GetLocalResourceObject("optStyle_bull_1Caption"), "1", "1", "insChangeStyle_bull(this)", True))


Response.Write("</P></TD>" & vbCrLf)
Response.Write("				<TD><DIV ID=""divCurrency"">")


Response.Write(mobjValues.PossiblesValues("cbeCurrencyBul", "Table11", eFunctions.Values.eValuesType.clngComboType,  ,  ,  ,  ,  ,  ,  , True,  , GetLocalResourceObject("cbeCurrencyBulToolTip")))


Response.Write("</DIV></TD>" & vbCrLf)
Response.Write("				<TD><P ALIGN=LEFT>")


Response.Write(mobjValues.OptionControl(0, "optStyle_bull", GetLocalResourceObject("optStyle_bull_2Caption"), "2", "2", "insChangeStyle_bull(this)", True))


Response.Write("</P></TD>				" & vbCrLf)
Response.Write("			</TABLE>" & vbCrLf)
Response.Write("        </TD>" & vbCrLf)
Response.Write("        <TD>&nbsp</TD>" & vbCrLf)
Response.Write("        <TD COLSPAN=2 ID=""tdCondi2"">" & vbCrLf)
Response.Write("			<TABLE WIDTH=""100%"">" & vbCrLf)
Response.Write("				<TR>" & vbCrLf)
Response.Write("					<TD>")


Response.Write(mobjValues.OptionControl(0, "optQueryOption", GetLocalResourceObject("optQueryOption_0Caption"), "1", "0", "insChangeTypDoc(this)", True))


Response.Write("</TD>" & vbCrLf)
Response.Write("					<TD>")


Response.Write(mobjValues.OptionControl(0, "optQueryOption", GetLocalResourceObject("optQueryOption_1Caption"), "", "1", "insChangeTypDoc(this)", True))


Response.Write("</TD>" & vbCrLf)
Response.Write("					<TD>")


Response.Write(mobjValues.OptionControl(0, "optQueryOption", GetLocalResourceObject("optQueryOption_2Caption"), "", "2", "insChangeTypDoc(this)", True))


Response.Write("</TD>" & vbCrLf)
Response.Write("					<TD>")


Response.Write(mobjValues.OptionControl(0, "optQueryOption", GetLocalResourceObject("optQueryOption_3Caption"), "", "3", "insChangeTypDoc(this)", True))


Response.Write("</TD>					" & vbCrLf)
Response.Write("				</TR>" & vbCrLf)
Response.Write("			</TABLE>" & vbCrLf)
Response.Write("		</TD>" & vbCrLf)
Response.Write("    </TR>" & vbCrLf)
Response.Write("</TABLE>" & vbCrLf)
Response.Write("<BR>" & vbCrLf)
Response.Write("<DIV ID=""divPolicy"">" & vbCrLf)
Response.Write("	<TABLE WIDTH=""50%"" ALIGN=""CENTER"">" & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("			<TD WIDTH=""50%"">&nbsp</TD>" & vbCrLf)
Response.Write("			<TD WIDTH=""50%"">&nbsp</TD>" & vbCrLf)
Response.Write("		</TR>" & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("			<TD COLSPAN=2><P ALIGN=RIGHT CLASS=""HIGHLIGHTED""><LABEL id=0>" & GetLocalResourceObject("Anchor6Caption") & "</LABEL></P></TD>" & vbCrLf)
Response.Write("		</TR>		" & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("			<TD COLSPAN=2 CLASS=""Horline"">" & vbCrLf)
Response.Write("		</TR>		" & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("			<TD WIDTH=""5%""><LABEL ID=0>" & GetLocalResourceObject("cbeBranchCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("		    <TD>")


Response.Write(mobjValues.PossiblesValues("cbeBranch", "Table10", eFunctions.Values.eValuesType.clngComboType,  ,  ,  ,  ,  ,  , "if(typeof(document.forms[0].valProduct)!=""undefined"")document.forms[0].valProduct.Parameters.Param1.sValue=this.value; insChangeBranch(this)", True,  , GetLocalResourceObject("cbeBranchToolTip")))


Response.Write(" </TD>" & vbCrLf)
Response.Write("		</TR>				" & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("			<TD><LABEL ID=0>" & GetLocalResourceObject("valProductCaption") & "</LABEL></TD>	" & vbCrLf)
Response.Write("			<TD>" & vbCrLf)
Response.Write("				")

	With mobjValues
		.Parameters.Add("nBranch", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		Response.Write(mobjValues.PossiblesValues("valProduct", "tabProdmaster1", eFunctions.Values.eValuesType.clngWindowType,  , True,  ,  ,  ,  ,  , True, 4, GetLocalResourceObject("valProductToolTip")))
	End With
	
Response.Write("" & vbCrLf)
Response.Write("		</TR>" & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("			<TD><LABEL ID=0>" & GetLocalResourceObject("tcnPolicyCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("			<TD>")


Response.Write(mobjValues.NumericControl("tcnPolicy", 8, "",  , GetLocalResourceObject("tcnPolicyToolTip"),  ,  ,  ,  ,  ,  , True))


Response.Write("</TD>" & vbCrLf)
Response.Write("		</TR>				" & vbCrLf)
Response.Write("		")


Response.Write(mobjValues.HiddenControl("tcnCertif", ""))


Response.Write("" & vbCrLf)
Response.Write("	</TABLE>	" & vbCrLf)
Response.Write("</DIV>" & vbCrLf)
Response.Write("<DIV ID=""divClient"">" & vbCrLf)
Response.Write("	<TABLE WIDTH=""50%"" ALIGN=""CENTER"">" & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("			<TD COLSPAN=2><P ALIGN=RIGHT CLASS=""HIGHLIGHTED""><LABEL id=0>" & GetLocalResourceObject("Anchor7Caption") & "</LABEL></P></TD>" & vbCrLf)
Response.Write("		</TR>		" & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("			<TD COLSPAN=2 CLASS=""Horline"">" & vbCrLf)
Response.Write("		</TR>		" & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("		    <TD><LABEL ID=10288>" & GetLocalResourceObject("dtcClientKCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("		    <TD>")


Response.Write(mobjValues.ClientControl("dtcClientK", "",  , GetLocalResourceObject("dtcClientKToolTip"),  , True, "lblCliename"))


Response.Write("</TD>" & vbCrLf)
Response.Write("		</TR>" & vbCrLf)
Response.Write("	</TABLE>	" & vbCrLf)
Response.Write("</DIV>" & vbCrLf)
Response.Write("<DIV ID=""divReceipt"">" & vbCrLf)
Response.Write("	<TABLE WIDTH=""50%"" ALIGN=""CENTER"">	" & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("			<TD COLSPAN=2><P ALIGN=RIGHT CLASS=""HIGHLIGHTED""><LABEL id=0>" & GetLocalResourceObject("Anchor8Caption") & "</LABEL></P></TD>" & vbCrLf)
Response.Write("		</TR>		" & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("			<TD COLSPAN=2 CLASS=""Horline"">" & vbCrLf)
Response.Write("		</TR>		" & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("			<TD><LABEL ID=0>" & GetLocalResourceObject("tcnReceiptCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("			<TD>")


Response.Write(mobjValues.NumericControl("tcnReceipt", 10, vbNullString,  , GetLocalResourceObject("tcnReceiptToolTip"),  ,  ,  ,  ,  ,  , True))


Response.Write("</TD> " & vbCrLf)
Response.Write("		</TR>" & vbCrLf)
Response.Write("	</TABLE>" & vbCrLf)
Response.Write("</DIV>	" & vbCrLf)
Response.Write("")

End Sub

</script>
<%Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage("co632_k")

mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.53.47
mobjValues.sSessionID = Session.SessionID
mobjValues.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjValues.sCodisplPage = "co632_k"
mobjMenu = New eFunctions.Menues
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.53.47
mobjMenu.sSessionID = Session.SessionID
mobjMenu.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjValues.ActionQuery = CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 401
%>
<HTML>
<HEAD>
	<META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>




<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tmenu.js"></SCRIPT>		
	<SCRIPT>
//+ Variable para el control de versiones
	     document.VssVersion="$$Revision: 3 $|$$Date: 20/10/03 16:01 $|$$Author: Nvaplat40 $"
    </SCRIPT>

<SCRIPT LANGUAGE=JavaScript>
//% insStateZone: se controla el estado de los campos de la página
//--------------------------------------------------------------------------------------------
function insStateZone(Action){
//--------------------------------------------------------------------------------------------
	var lblnQuery = (Action==301?false:true);
	with(self.document.forms[0]){
		tcnBulletins.value='';
		tcnBulletins.disabled=!lblnQuery;
		cbeInsur_area.value='';
		cbeInsur_area.disabled=lblnQuery;
		tcdCollectDate.disabled=lblnQuery;
		tcdCollectDate.disabled=lblnQuery;
		chkCollect_exp.disabled=true;
		optStyle_bull[0].disabled=lblnQuery;
		optStyle_bull[1].disabled=lblnQuery;
		optQueryOption[0].disabled=lblnQuery;
		optQueryOption[1].disabled=lblnQuery;
		optQueryOption[2].disabled=lblnQuery;
		optQueryOption[3].disabled=lblnQuery;
		cbeBranch.value='';
		valProduct.value='';
		UpdateDiv('valProductDesc', '');
		tcnPolicy.value='';
		dtcClientK.value=''
		UpdateDiv('lblCliename', '');
		UpdateDiv('lblStatus', '');
		cbeCurrencyBul.disabled=lblnQuery;
		cbeCurrencyBul.value=0;
		if (lblnQuery){
		    ShowDiv('divCollect', 'hide');
		    ShowDiv('divStatus', 'show');
		    document.getElementById("tdCondi").style.display='none'		    
		    document.getElementById("tdCondi1").style.display='none'
		    document.getElementById("tdCondi2").style.display='none'
		}
		else{
		    document.getElementById("tdCondi").style.display=''		    
		    document.getElementById("tdCondi1").style.display=''
		    document.getElementById("tdCondi2").style.display=''
			ShowDiv('divCollect', 'show');
		    ShowDiv('divStatus', 'hide');		    			
		}
	}
}



//% insHideDivs: Oculta las Divs hasta que no se seleccione opción.
//-------------------------------------------------------------------------------------------
function insHideDivs(){
//-------------------------------------------------------------------------------------------
	ShowDiv('divPolicy', 'hide');
	ShowDiv('divClient', 'hide');
	ShowDiv('divReceipt', 'hide');
	ShowDiv('divStatus', 'hide');
	ShowDiv('divCollect', 'hide');
	ShowDiv('divStatus', 'hide');
	document.getElementById("tdCondi").style.display='none'		    
	document.getElementById("tdCondi1").style.display='none'
	document.getElementById("tdCondi2").style.display='none'
}

//% insShowDataBulletins: Obtiene la información del boletin para su consulta o modificación.
//-------------------------------------------------------------------------------------------
function insShowDataBulletins(Field, Action){
//-------------------------------------------------------------------------------------------
//+ Si el tipo de suspensión es por póliza/certificado: lblnDisabled true sino false
	with(self.document.forms[0]){
		if (Field.value!='') {
			insDefValues("ShowBulletinsCO632", "nBulletins=" + Field.value + "&nMainAction=" + top.frames['fraSequence'].plngMainAction);
		}	
		else{
			ShowDiv('divCurrency','show');
			optStyle_bull[0].checked=true;		
			tcnBulletins.value='';
  			cbeInsur_area.value=0;
			UpdateDiv('lblStatus','');
			cbeCurrencyBul.value=0;
		}	
	}	
}

//% insChangeInsur_area: Habilita o deshabilita el campo chkCollect_exp dependiendo del área de seguro.
//-------------------------------------------------------------------------------------------
function insChangeInsur_area(Field){
//-------------------------------------------------------------------------------------------
//+ Si el tipo de suspensión es por póliza/certificado: lblnDisabled true sino false
	with(self.document.forms[0]){
		if (Field.value==1) {
			chkCollect_exp.disabled=true;
		} else {
			chkCollect_exp.disabled=false;
		}
	}
}

//% insChangeStyle_bull: Muestra u oculta el campo moneda.
//-------------------------------------------------------------------------------------------
function insChangeStyle_bull(Field){
//-------------------------------------------------------------------------------------------
	with(self.document.forms[0]){
		cbeCurrencyBul.value=0;
		if (Field.value==1) {
			ShowDiv('divCurrency', 'show');
		} else {
			ShowDiv('divCurrency', 'hide');
		}
	}
}

//% insChangeTypDoc: Habilita o deshabilita los campos dependiendo del tipo de documento seleccionado.
//-------------------------------------------------------------------------------------------
function insChangeTypDoc(Field){
//-------------------------------------------------------------------------------------------
    with(self.document.forms[0]){
		cbeBranch.disabled = true;
		cbeBranch.value='';
		valProduct.disabled = true;
		btnvalProduct.disabled = valProduct.disabled;
		valProduct.value='';
		UpdateDiv('valProductDesc', '');
		tcnPolicy.disabled = true;
		tcnPolicy.value='';
		tcnReceipt.disabled = true;
		tcnReceipt.value='';
		dtcClientK.value=''
		dtcClientK_Digit.value='';
		UpdateDiv('lblCliename', '');
		dtcClientK.disabled=true;
		btndtcClientK.disabled = dtcClientK.disabled;
		ShowDiv('divPolicy', 'hide');
		ShowDiv('divClient', 'hide');
		ShowDiv('divReceipt', 'hide');
		switch(Field.value){
//+ Si la condición es por póliza
			case "1":
				cbeBranch.disabled = false;
				valProduct.disabled = false;
				btnvalProduct.disabled = valProduct.disabled;
				tcnPolicy.disabled = false;
				ShowDiv('divPolicy', 'show');
				break;
//+ Si la condición es por cliente
			case "2":
				dtcClientK.disabled=false;
				btndtcClientK.disabled=dtcClientK.disabled;
				ShowDiv('divClient', 'show');
				break;
//+ Si la condición es por recibo
			case "3":
				tcnReceipt.disabled = false;
				ShowDiv('divReceipt', 'show');
				break;
		}
	}
}

//% insChangeBranch: Se limpian los campos producto y poliza al cambiar el ramo.
//-------------------------------------------------------------------------------------------
function insChangeBranch(Field){
//-------------------------------------------------------------------------------------------
//+ Si el tipo de suspensión es por póliza/certificado: lblnDisabled true sino false	
    with(self.document.forms[0]){
		valProduct.value='';
		UpdateDiv('valProductDesc', '');
		tcnPolicy.value='';
	}
}

//% insCancel: se controla la acción Cancelar de la página
//--------------------------------------------------------------------------------------------
function insCancel(){
//--------------------------------------------------------------------------------------------
    if (top.frames["fraSequence"].pintZone==2)
		if (top.frames["fraSequence"].plngMainAction==301) 
			insDefValues("delCO632", "")
	return true;
}

//% insFinish: se controla la acción Cancelar de la página
//--------------------------------------------------------------------------------------------
function insFinish(){
//--------------------------------------------------------------------------------------------
    return true;
}
</SCRIPT>
    <%
Response.Write(mobjValues.StyleSheet())
If Request.QueryString.Item("Type") <> "PopUp" Then
	Response.Write(mobjMenu.MakeMenu("CO632", "CO632_k.aspx", 1, vbNullString))
	mobjMenu = Nothing
	Response.Write("<SCRIPT>var nMainAction=top.frames['fraSequence'].plngMainAction</SCRIPT>")
	Response.Write("<BR><BR>")
End If
%>    

</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" NAME="CO632" ACTION="valCollectionTra.aspx?sMode=2">
<%
If Request.QueryString.Item("sConfig") = "InSequence" Then
	Call LoadHeader()
Else
	Call LoadFolder()
End If%>

</FORM> 
</BODY>
</HTML>
<%'^Begin Footer Block VisualTimer Utility 1.1 7/4/03 10.53.47
Call mobjNetFrameWork.FinishPage("co632_k")
mobjNetFrameWork = Nothing
If Request.QueryString.Item("sConfig") <> "InSequence" Then
	Response.Write("<SCRIPT>insHideDivs()</SCRIPT>")
End If

'^End Footer Block VisualTimer%>




