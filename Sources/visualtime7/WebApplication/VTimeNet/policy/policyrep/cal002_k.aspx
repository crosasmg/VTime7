<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">
'^Begin Header Block VisualTimer Utility 1.1 7/4/03 10.28.02
Dim mobjNetFrameWork As eNetFrameWork.Layout
'~End Header Block VisualTimer Utility

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values
'- Objeto para el manejo del menú
Dim mobjMenu As eFunctions.Menues


</script>
<%Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage("CAL002_K")
'- Objeto para el manejo particular de los datos de la página
mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.28.02
mobjValues.sSessionID = Session.SessionID
mobjValues.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjValues.sCodisplPage = "CAL002_K"
mobjMenu = New eFunctions.Menues
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.28.02
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
<SCRIPT LANGUAGE=JavaScript>
//- Variable para el control de versiones
    document.VssVersion="$$Revision: 2 $|$$Date: 12/09/03 17:36 $|$$Author: Nvaplat28 $"
//------------------------------------------------------------------------------------
function insCancel(){ 
//------------------------------------------------------------------------------------
	return true; 
}    
//------------------------------------------------------------------------------------
function insFinish(){ 
//------------------------------------------------------------------------------------
    return true; 
} 
//------------------------------------------------------------------------------------
function insStateZone(){ 
//------------------------------------------------------------------------------------
	var lintIndex; 
    var error; 
    try { 
		for(lintIndex=0;lintIndex < self.document.forms[0].elements.length;lintIndex++){ 
			self.document.forms[0].elements[lintIndex].disabled=false; 
			if(self.document.images.length>0) 
			    if(typeof(self.document.images["btn_" + self.document.forms[0].elements[lintIndex].name])!='undefined') 
			       self.document.images["btn_" + self.document.forms[0].elements[lintIndex].name].disabled = self.document.forms[0].elements[lintIndex].disabled  
		} 
	} catch(error){} 
}	    

//% insChangeField: se controla la acción Modificar parametros de la pantalla  
//--------------------------------------------------------------------------------------------
function insChangeField(objField){
//--------------------------------------------------------------------------------------------
    with(self.document.forms[0]){
		switch (objField.name){
//Revisa Ramo  
			case 'cbeBranch':
     			valProduct.Parameters.Param1.sValue=cbeBranch.value; 
    			if (cbeBranch.value!=0 && cbeBranch.value!='') { 
					valProduct.disabled = false; 
					btnvalProduct.disabled = false; 
				}
    			else {
					valProduct.value = "";
        			UpdateDiv("valProductDesc", "")
					valProduct.disabled = true;
					btnvalProduct.disabled = true;
					tcnPolicy.value  = '';
					tcnPolicy.disabled = true;
					tcnRec_Beg.value="";
					tcnRec_End.value="";
					tcnRec_Beg.disabled = true;
					tcnRec_End.disabled = true;
					tcnCon_Beg.value="";
					tcnCon_End.value="";
					tcnCon_Beg.disabled = true;
					tcnCon_End.disabled = true;
					tcdStarDate.value = "";
					tcdEndDate.value = "";
				}
				break;
//Revisa Producto
			case 'valProduct':
    			if (valProduct.value!=0) {
				    tcnPolicy.disabled = false; 
				}
				else{
				    tcnPolicy.value  = '';
				    tcnPolicy.disabled = true;
				    tcnRec_Beg.value="";
				    tcnRec_End.value="";
				    tcnRec_Beg.disabled = true;
				    tcnRec_End.disabled = true;
				    tcnCon_Beg.value="";
				    tcnCon_End.value="";
				    tcnCon_Beg.disabled = true;
				    tcnCon_End.disabled = true;
					tcdStarDate.value = "";										
					tcdEndDate.value = "";					
				}
				break;
//Revisa numero de poliza
			case 'tcnPolicy':
    			if (valProduct.value!=0) {
				    tcnRec_Beg.value="";
				    tcnRec_End.value="";
				    tcnRec_Beg.disabled = false;
				    tcnRec_End.disabled = false;
				    tcnCon_Beg.value="";
				    tcnCon_End.value="";
				    tcnCon_Beg.disabled = false;
				    tcnCon_End.disabled = false;
					tcdStarDate.value = "";
					tcdEndDate.value = "";
					if (tcnPolicy.value !=0)
						insDefValues('ShownReceipt', "nBranch=" + cbeBranch.value + "&nProduct=" + valProduct.value + "&nPolicy=" + tcnPolicy.value);
				}
				else{
				    tcnRec_Beg.value="";
				    tcnRec_End.value="";
				    tcnRec_Beg.disabled = true;
				    tcnRec_End.disabled = true;
				    tcnCon_Beg.value="";
				    tcnCon_End.value="";
				    tcnCon_Beg.disabled = true;
				    tcnCon_End.disabled = true;
					tcdStarDate.value = "";
					tcdEndDate.value = "";
				}
				break;
		}
    }
}

</SCRIPT>
	<%
With Response
	.Write(mobjValues.StyleSheet())
	.Write(mobjMenu.MakeMenu("CAL002", "CAL002_K.aspx", 1, vbNullString))
	.Write(mobjMenu.setZone(1, "CAL002", Request.QueryString.Item("sWindowDescript"), CShort(Request.QueryString.Item("nWindowTy"))))
	Response.Write("<SCRIPT>var nMainAction=top.frames['fraSequence'].plngMainAction</SCRIPT>")
End With
mobjMenu = Nothing
%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<BR>
<BR>
<FORM METHOD="POST" NAME="CAL002_K" ACTION="valPolicyRep.aspx?sMode=2">
<%Response.Write(mobjValues.ShowWindowsName("CAL002", Request.QueryString.Item("sWindowDescript")))%>
    <TABLE WIDTH="100%">
        <TR>
			<TD WIDTH="25%">&nbsp</TD>
            <TD WIDTH="25%">&nbsp</TD>
            <TD WIDTH="25%">&nbsp</TD>
            <TD WIDTH="25%">&nbsp</TD>
        </TR>		
        <TR>
			<TD><LABEL ID=101242><%= GetLocalResourceObject("valOfficeCaption") %></LABEL></TD>			
			<TD><%With Response
	.Write(mobjValues.PossiblesValues("valOffice", "Table9", eFunctions.Values.eValuesType.clngComboType, CStr(0),  , False,  ,  ,  ,  ,  ,  , GetLocalResourceObject("valOfficeToolTip"),  , 2))
End With
%>
	        </TD>
			<TD><LABEL ID=0><%= GetLocalResourceObject("cbeBranchCaption") %></LABEL></TD>			
				<TD><%=mobjValues.BranchControl("cbeBranch", GetLocalResourceObject("cbeBranchToolTip"), vbNullString, "valProduct")%></TD>
			</TD>
        </TR>
		<TR>
			<TD><LABEL ID=0><%= GetLocalResourceObject("valProductCaption") %></LABEL></td>			
			<TD><%=mobjValues.ProductControl("valProduct", GetLocalResourceObject("valProductToolTip"),  ,  ,  ,  ,  ,  ,  , "insChangeField(this)", 4)%></TD>
    		<TD><LABEL ID=8759><%= GetLocalResourceObject("tcnPolicyCaption") %></LABEL></TD>    		
			<TD><%=mobjValues.NumericControl("tcnPolicy", 10,  ,  , GetLocalResourceObject("tcnPolicyToolTip"),  ,  ,  ,  ,  , "insChangeField(this)", True, 5)%></TD>
		</TR>
		</TABLE>
		<BR>
		<TABLE WIDTH="100%">
		<TR>
			<TD COLSPAN="2" CLASS="HighLighted"><LABEL ID=101970><A NAME="RECIBALIST"><%= GetLocalResourceObject("AnchorRECIBALISTCaption") %></A></LABEL></TD>
			<TD COLSPAN="1">&nbsp</TD>
			<TD COLSPAN="2" CLASS="HighLighted"><LABEL ID=101970><A NAME="CONTRATLIST"><%= GetLocalResourceObject("AnchorCONTRATLISTCaption") %></A></LABEL></TD>
			<TD COLSPAN="1">&nbsp</TD>
			<TD COLSPAN="2" CLASS="HighLighted"><LABEL ID=101970><A NAME="DATELIST"><%= GetLocalResourceObject("AnchorDATELISTCaption") %></A></LABEL></TD>
			
		</TR>
		<TR>
			<TD COLSPAN="2" CLASS="HorLine"></TD>
			<TD COLSPAN="1"></TD>
			<TD COLSPAN="2" CLASS="HorLine"></TD>
			<TD COLSPAN="1"></TD>
			<TD COLSPAN="2" CLASS="HorLine"></TD>			
		</TR>
		<TR>
			<TD><LABEL><%= GetLocalResourceObject("tcnRec_BegCaption") %></LABEL></TD>
			<TD><%=mobjValues.NumericControl("tcnRec_Beg", 8,  ,  , GetLocalResourceObject("tcnRec_BegToolTip"),  ,  ,  ,  ,  ,  , True, 6)%></TD>
			<TD COLSPAN="1">&nbsp</TD>			
			<TD><LABEL><%= GetLocalResourceObject("tcnCon_BegCaption") %></LABEL></TD>
			<TD><%=mobjValues.NumericControl("tcnCon_Beg", 8,  ,  , GetLocalResourceObject("tcnCon_BegToolTip"),  ,  ,  ,  ,  ,  , True, 8)%></TD>
			<TD COLSPAN="1">&nbsp</TD>			
			<TD><LABEL><%= GetLocalResourceObject("tcnRec_BegCaption") %></LABEL></TD>
			<TD><%=mobjValues.DateControl("tcdStarDate",  ,  , GetLocalResourceObject("tcdStarDateToolTip"),  ,  ,  ,  ,  , 10)%></TD>
			
		</TR>
		<TR>
			<TD><LABEL><%= GetLocalResourceObject("tcnRec_EndCaption") %></LABEL></TD>
			<TD><%=mobjValues.NumericControl("tcnRec_End", 8,  ,  , GetLocalResourceObject("tcnRec_EndToolTip"),  ,  ,  ,  ,  ,  , True, 7)%></TD>
			<TD COLSPAN="1">&nbsp</TD>			
			<TD><LABEL><%= GetLocalResourceObject("tcnCon_EndCaption") %></LABEL></TD>
			<TD><%=mobjValues.NumericControl("tcnCon_End", 8,  ,  , GetLocalResourceObject("tcnCon_EndToolTip"),  ,  ,  ,  ,  ,  , True, 9)%></TD>			
			<TD COLSPAN="1">&nbsp</TD>			
			<TD><LABEL><%= GetLocalResourceObject("tcnRec_EndCaption") %></LABEL></TD>
			<TD><%=mobjValues.DateControl("tcdEndDate",  ,  , GetLocalResourceObject("tcdEndDateToolTip"),  ,  ,  ,  ,  , 11)%></TD>
		</TR>
	</TABLE>
</FORM>
</BODY>
</HTML>

<%
mobjValues = Nothing%>
<%'^Begin Footer Block VisualTimer Utility 1.1 7/4/03 10.28.02
Call mobjNetFrameWork.FinishPage("CAL002_K")
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>





