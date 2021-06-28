<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">
'^Begin Header Block VisualTimer Utility 1.1 7/4/03 10.24.56
Dim mobjNetFrameWork As eNetFrameWork.Layout
'~End Header Block VisualTimer Utility

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'- Objeto para el manejo del menú de la página
Dim mobjMenu As eFunctions.Menues


'% LoadPageInSequence: se carga la página cuando se encuentra dentro de la secuencia
'--------------------------------------------------------------------------------------------
Sub LoadPageInSequence()
	'--------------------------------------------------------------------------------------------
	
Response.Write("" & vbCrLf)
Response.Write("	<BR>	" & vbCrLf)
Response.Write("	<TABLE WIDTH=100%>	" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("			<TD><LABEL ID=0>" & GetLocalResourceObject("tctClientCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("			<TD WIDTH=90%>")


Response.Write(mobjValues.ClientControl("tctClient", Session("sClient"),  , GetLocalResourceObject("tctClientToolTip"), "ShowChangeValues(""Client"")", True, "lblClient",  , True,  ,  ,  ,  , True))


Response.Write("</TD>" & vbCrLf)
Response.Write("		</TR>" & vbCrLf)
Response.Write("	</TABLE>	")

	
End Sub

'% LoadHeader: se carga la página cuando muestra los datos de la secuencia
'--------------------------------------------------------------------------------------------
Sub LoadHeader()
	'--------------------------------------------------------------------------------------------
	
Response.Write("" & vbCrLf)
Response.Write("	<P>&nbsp;</P>" & vbCrLf)
Response.Write("    <TABLE WIDTH=""100%"">" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("			<TD><LABEL ID=0>" & GetLocalResourceObject("tctClientCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("			<TD COLSPAN=""4"">")


Response.Write(mobjValues.ClientControl("tctClient", vbNullString,  , GetLocalResourceObject("tctClientToolTip"), "ShowChangeValues(""Client"")", True, "x",  ,  ,  ,  ,  ,  , True))


Response.Write("</TD>" & vbCrLf)
Response.Write("		</TR> " & vbCrLf)
Response.Write("		<TR> " & vbCrLf)
Response.Write("		  <TD COLSPAN=""5"" CLASS=""HighLighted""><LABEL><A NAME=""Datos"">" & GetLocalResourceObject("AnchorDatosCaption") & "</A></LABEL></TD> " & vbCrLf)
Response.Write("		</TR> " & vbCrLf)
Response.Write("		<TR> " & vbCrLf)
Response.Write("		  <TD WIDTH=""100%"" COLSPAN=""5""><HR></TD> " & vbCrLf)
Response.Write("		</TR> " & vbCrLf)
Response.Write("        <TR> " & vbCrLf)
Response.Write("			<TD><LABEL ID=0>" & GetLocalResourceObject("tctFirstnameCaption") & "</LABEL></TD> " & vbCrLf)
Response.Write("			<TD COLSPAN=""4"">")


Response.Write(mobjValues.TextControl("tctFirstname", 40, "",  , GetLocalResourceObject("tctFirstnameToolTip"),  ,  ,  ,  , True))


Response.Write("</TD> " & vbCrLf)
Response.Write("		</TR> " & vbCrLf)
Response.Write("        <TR> " & vbCrLf)
Response.Write("			<TD><LABEL ID=0>" & GetLocalResourceObject("tctLastnameCaption") & "</LABEL></TD> " & vbCrLf)
Response.Write("			<TD>")


Response.Write(mobjValues.TextControl("tctLastname", 10, "",  , GetLocalResourceObject("tctLastnameToolTip"),  ,  ,  ,  , True))


Response.Write("</TD>	 " & vbCrLf)
Response.Write("			<TD><LABEL ID=0>" & GetLocalResourceObject("tctLastname2Caption") & "</LABEL></TD> " & vbCrLf)
Response.Write("			<TD>")


Response.Write(mobjValues.TextControl("tctLastname2", 10, "",  , GetLocalResourceObject("tctLastname2ToolTip"),  ,  ,  ,  , True))


Response.Write("</TD> " & vbCrLf)
Response.Write("			<TD>&nbsp;</TD>	" & vbCrLf)
Response.Write("		</TR>" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("			<TD><LABEL ID=0>" & GetLocalResourceObject("tcdBirthdatCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("			<TD>")


Response.Write(mobjValues.DateControl("tcdBirthdat", "",  , GetLocalResourceObject("tcdBirthdatToolTip"),  ,  ,  ,  , True))


Response.Write("</TD>" & vbCrLf)
Response.Write("			<TD COLSPAN=""3""></TD> " & vbCrLf)
Response.Write("		</TR>		" & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("		  <TD COLSPAN=""5"" CLASS=""HighLighted""><LABEL><A NAME=""Datos poliza"">" & GetLocalResourceObject("AnchorDatos polizaCaption") & "</A></LABEL></TD>" & vbCrLf)
Response.Write("		</TR>" & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("		  <TD WIDTH=""100%"" COLSPAN=""5""><HR></TD>" & vbCrLf)
Response.Write("		</TR> " & vbCrLf)
Response.Write("		<TR> " & vbCrLf)
Response.Write("            <TD><LABEL ID=9380>" & GetLocalResourceObject("cbeCertypeCaption") & "</LABEL></TD> " & vbCrLf)
Response.Write("			<TD>")


Response.Write(mobjValues.PossiblesValues("cbeCertype", "Table5632", eFunctions.Values.eValuesType.clngComboType, "1",  ,  ,  ,  ,  , "ShowChangeValues(""Certype"")", True,  , GetLocalResourceObject("cbeCertypeToolTip")))


Response.Write("</TD> " & vbCrLf)
Response.Write("		</TR>		" & vbCrLf)
Response.Write("		<TR> " & vbCrLf)
Response.Write("            <TD><LABEL ID=9380>" & GetLocalResourceObject("cbeBranchCaption") & "</LABEL></TD> " & vbCrLf)
Response.Write("			<TD>")


Response.Write(mobjValues.BranchControl("cbeBranch", GetLocalResourceObject("cbeBranchToolTip"),  ,  ,  ,  ,  , "ShowChange(this);", True))


Response.Write("</TD> " & vbCrLf)
Response.Write("            <TD><LABEL ID=9389>" & GetLocalResourceObject("valProductCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("			<TD COLSPAN=""2"">")


Response.Write(mobjValues.ProductControl("valProduct", GetLocalResourceObject("valProductToolTip"), CStr(0), eFunctions.Values.eValuesType.clngWindowType, True,  ,  ,  ,  , "ShowChange(this);"))


Response.Write("</TD> " & vbCrLf)
Response.Write("		</TR>" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("			<TD>" & vbCrLf)
Response.Write("				<DIV ID=""DivPro"" >" & vbCrLf)
Response.Write("					<LABEL ID=0>" & GetLocalResourceObject("AnchorCaption") & "</LABEL>" & vbCrLf)
Response.Write("				</DIV>" & vbCrLf)
Response.Write("				<DIV ID=""DivPol"">" & vbCrLf)
Response.Write("					<LABEL ID=0>" & GetLocalResourceObject("Anchor2Caption") & "</LABEL>" & vbCrLf)
Response.Write("				</DIV>" & vbCrLf)
Response.Write("				<DIV ID=""DivCot"">" & vbCrLf)
Response.Write("					<LABEL ID=0>" & GetLocalResourceObject("tcnPolicyCaption") & "</LABEL>" & vbCrLf)
Response.Write("				</DIV>	" & vbCrLf)
Response.Write("			</TD>" & vbCrLf)
Response.Write("			<TD>")


Response.Write(mobjValues.NumericControl("tcnPolicy", 10, vbNullString,  , GetLocalResourceObject("tcnPolicyToolTip"),  , 0,  ,  ,  , "ShowChangeValues(""DataPol"")", True))


Response.Write("</TD> " & vbCrLf)
Response.Write("			<TD><LABEL ID=9381>" & GetLocalResourceObject("tcnCertifCaption") & "</LABEL></TD> " & vbCrLf)
Response.Write("			<TD>")


Response.Write(mobjValues.NumericControl("tcnCertif", 10, vbNullString,  , GetLocalResourceObject("tcnCertifToolTip"),  , 0,  ,  ,  ,  , True))


Response.Write(" </TD> " & vbCrLf)
Response.Write("		</TR> " & vbCrLf)
Response.Write("        <TR> " & vbCrLf)
Response.Write("			<TD>")


Response.Write(mobjValues.AnimatedButtonControl("bCarga", "/VTimeNet/Images/RevVoucherInside.bmp", GetLocalResourceObject("bCargaToolTip"),  , "ShowPopUp('/VTimeNet/Common/Goto.aspx?sCodispl=CAL013_K&sLinkSpecial=BC668_K','Consulta',750,500,'no','no',10,10)"))


Response.Write("</TD> " & vbCrLf)
Response.Write("        </TR> " & vbCrLf)
Response.Write("    </TABLE> " & vbCrLf)
Response.Write("<SCRIPT> " & vbCrLf)
Response.Write("	ShowDiv('DivPro', 'show')" & vbCrLf)
Response.Write("	ShowDiv('DivPol', 'hide')" & vbCrLf)
Response.Write("	ShowDiv('DivCot', 'hide')" & vbCrLf)
Response.Write("" & vbCrLf)
Response.Write("//%ShowVerifyData: Habilita/Deshabilita los controles dependientes de la página" & vbCrLf)
Response.Write("//-------------------------------------------------------------------------------------------*/" & vbCrLf)
Response.Write("function ShowVerifyData(){" & vbCrLf)
Response.Write("//-------------------------------------------------------------------------------------------" & vbCrLf)
Response.Write("	with(self.document.forms[""SI001""]){" & vbCrLf)
Response.Write("		ShowPolicyData(""2"", +" & vbCrLf)
Response.Write("					   cbeBranch.value, +" & vbCrLf)
Response.Write("					   valProduct.value, + " & vbCrLf)
Response.Write("					   tcnPolicy.value, + " & vbCrLf)
Response.Write("					   tcnCertificat.value)" & vbCrLf)
Response.Write("	}" & vbCrLf)
Response.Write("}" & vbCrLf)
Response.Write("</" & "SCRIPT>")

End Sub

</script>
<%Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage("bc668_k")
mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.24.56
mobjValues.sSessionID = Session.SessionID
mobjValues.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjValues.sCodisplPage = "bc668_k"
mobjMenu = New eFunctions.Menues
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.24.56
mobjMenu.sSessionID = Session.SessionID
mobjMenu.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility
%>
<HTML>
<HEAD>
<META NAME = "GENERATOR" CONTENT = "Microsoft Visual Studio 6.0">
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/Constantes.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tmenu.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/Claim.js"></SCRIPT>


<SCRIPT>
//+ Variable para el control de versiones
	document.VssVersion="$$Revision: 1 $|$$Date: 2/09/03 19:01 $"
	
//%insStateZone: Habilita/deshabilita los campos de la ventana.
//--------------------------------------------------------------------------------------------
function insStateZone(){
//--------------------------------------------------------------------------------------------
	with (self.document.forms[0]){
		tctClient.disabled = false;
		btntctClient.disabled = tctClient.disabled;
		cbeCertype.disabled = false;
		cbeBranch.disabled = false;		
	}
}

//%insCancel: se activa al presionar el botón de Cancelar
//--------------------------------------------------------------------------------------------
function insCancel(){
//--------------------------------------------------------------------------------------------
	top.document.location.href = "/VTimeNet/Common/SecWHeader.aspx?sCodispl=BC668_K&sModule=Client&sProject=DocumSeq" 
}

//%insFinish: Ejecuta la acción de Finalizar de la página.
//--------------------------------------------------------------------------------------------
function insFinish(){
//--------------------------------------------------------------------------------------------
		return true
}

//%ShowChangeValues: Se asigna valor a los controles cuyo valor depende de otros controles
//-------------------------------------------------------------------------------------------
function ShowChangeValues(sField){
//-------------------------------------------------------------------------------------------
	var lstrParams = ""; 

	switch(sField){
		case "Client":
		    with(self.document.forms[0]){ 
			    lstrParams = "sClient=" + tctClient.value;
			}
			insDefValues(sField,lstrParams,'/VTimeNet/Client/DocumSeq');
			break;
		case "DataPol":
		    with(self.document.forms[0]){ 
			    lstrParams = "nbranch=" + cbeBranch.value + 
							 "&nProduct=" + valProduct.value +
							 "&nPolicy=" + tcnPolicy.value +
							 "&sCertype=" + cbeCertype.value;							  
			}
		
			insDefValues(sField,lstrParams,'/VTimeNet/Client/DocumSeq');
			break;	
		case "Certype":
		    with(self.document.forms[0]){ 
		    
				if (cbeCertype.value == "0"){
					cbeBranch.disabled = true;
					cbeBranch.value = '';
				}
				else{
					cbeBranch.disabled = false;
				}
		    
		        if (cbeCertype.value == '1' || 
		            cbeCertype.value == '6' ||
		            cbeCertype.value == '7'){
		    	    ShowDiv('DivPro', 'show')
					ShowDiv('DivPol', 'hide')
					ShowDiv('DivCot', 'hide')
				}
				else{
				    if (cbeCertype.value == '2'){
		    			ShowDiv('DivPro', 'hide')
						ShowDiv('DivPol', 'show')
						ShowDiv('DivCot', 'hide')
				    }else{
		    			ShowDiv('DivPro', 'hide')
						ShowDiv('DivPol', 'hide')
						ShowDiv('DivCot', 'show')
				    }
				}	
			}
			break;				
	}
}   

//%ShowChange: Se habilita/deshabilita campos cuando se abandona el campo rut.
//-------------------------------------------------------------------------------------------
function ShowChange(sField){
//-------------------------------------------------------------------------------------------
	if (sField.value!=0){
		self.document.forms[0].valProduct.disabled = false
		self.document.forms[0].tcnPolicy.disabled = false
		self.document.forms[0].tcnCertif.disabled = false
	}	
	else{
		self.document.forms[0].valProduct.disabled = true
		self.document.forms[0].tcnPolicy.disabled = true
		self.document.forms[0].tcnCertif.disabled = true
	}
}   
</SCRIPT>    
<%
Response.Write(mobjValues.StyleSheet())
Response.Write(mobjMenu.MakeMenu("BC668_K", "BC668_K.aspx", 1, Request.QueryString.Item("sWindowDescript"), Session("sDesMultiCompany"), Session("sSche_code")))
mobjMenu = Nothing
%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" ID="FORM" NAME="SI001" ACTION="valDocumentSeq.aspx?mode=1">
<P>&nbsp;</P>
<%
If Request.QueryString.Item("sConfig") = "InSequence" Then
	Call LoadPageInSequence()
Else
        Call LoadHeader()
End If
%>
</FORM>
</BODY>
</HTML>
<%
mobjValues = Nothing%>
<%'^Begin Footer Block VisualTimer Utility 1.1 7/4/03 10.24.56
Call mobjNetFrameWork.FinishPage("bc668_k")
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>




