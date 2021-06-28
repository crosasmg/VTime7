<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores		
Dim mobjMenu As eFunctions.Menues

Dim mobjValues As eFunctions.Values


</script>
<%Response.Expires = -1
mobjValues = New eFunctions.Values

mobjValues.sCodisplPage = "op714_k"
%>
<HTML>
<HEAD>


    <%=mobjValues.StyleSheet()%>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/Constantes.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tMenu.js"></SCRIPT>
    <SCRIPT LANGUAGE="JavaScript">

 //+Variable para el control de versiones
    document.VssVersion="$$Revision: 1 $|$$Date: 11/02/04 17:25 $|$$Author: Nvaplat7 $"

//%insStateZone: Habilita/Deshabilita los campos de la ventana
//--------------------------------------------------------------------------------------------------
function insStateZone(){
//--------------------------------------------------------------------------------------------------
	with (self.document.forms[0]){
		cbeCompany.disabled = false;
		tcdStartDate.disabled = false;
		btn_tcdStartDate.disabled = false;
		tcdEndDate.disabled = false;
		btn_tcdEndDate.disabled = false;
		optTypeOper[0].disabled = false;
		optTypeOper[1].disabled = false;
		}	
}
//%CalAvaliable: Retorna el monto disponible en la cuenta
//--------------------------------------------------------------------------------------------------
function CalAvaliable(){
//--------------------------------------------------------------------------------------------------
	with (self.document.forms[0]){
		    insDefValues('Avaliable', "nAccBank=" + valAccBank.value + "&nMainAction=" + top.fraSequence.plngMainAction, '/VTimeNet/CashBank/CashBank/');
		}	
}
//%ChangeConcept: Asigna el valor del parámetro para obtener los conceptos de la compañía
//--------------------------------------------------------------------------------------------------
function ChangeConcept(value){
//--------------------------------------------------------------------------------------------------
    with (self.document.forms[0]){
	    if(value!=0)
	    {
		    valConcept.Parameters.Param1.sValue=value;
		    valConcept.disabled=false;
		    btnvalConcept.disabled=false;
	    }
	    else
	    {
		    valConcept.disabled=true;
		    btnvalConcept.disabled=true;
		    valConcept.value='';
		    UpdateDiv('valConceptDesc','','Normal');	    
	    }
	}
}

//%insCancel: Controla la acción "Cancelar" de la página
//--------------------------------------------------------------------------------------------------
function insCancel(){
//--------------------------------------------------------------------------------------------------
	return true;
}   

//%insFinish: Controla la acción "Finalizar" de la página
//--------------------------------------------------------------------------------------------------
function insFinish(){
//--------------------------------------------------------------------------------------------------
    return true;
}
</SCRIPT>
<META HTTP-EQUIV="Content-Language" CONTENT="Microsoft Visual Studio 6.0">
    <%mobjMenu = New eFunctions.Menues
Response.Write(mobjMenu.MakeMenu("OP714", "OP714.aspx", 1, ""))
mobjMenu = Nothing
%>
    <BR>
</HEAD>
<BODY CLASS="Header" VLINK=white LINK=white ALINK=white >
<BR>
<FORM METHOD="post" ID="FORM" NAME="frmAproved" ACTION="valCashBank.aspx?sMode=1">
    <TABLE WIDTH="100%" >
    <TR>
        <TD COLSPAN="2" CLASS=HIGHLIGHTED><LABEL ID=LABEL1> </LABEL></TD>
        <TD COLSPAN="1">&nbsp</TD>
        <TD COLSPAN="2" CLASS=HIGHLIGHTED><LABEL ID=LABEL2><%= GetLocalResourceObject("BankCaption")%></LABEL></TD>
     </TR>
     <TR>
 	    <TD COLSPAN="2"></TD>
	    <TD></TD>
	    <TD COLSPAN="2" CLASS="Horline"></TD>
     </TR>     
    <TR>
		<TD><LABEL ID=0><%= GetLocalResourceObject("cbeCompanyCaption") %></LABEL></TD>
		<TD><%=mobjValues.PossiblesValues("cbeCompany", "company", eFunctions.Values.eValuesType.clngComboType, Request.QueryString.Item("nCompany"),  ,  ,  ,  ,  , "ChangeConcept(this.value)", True,  , GetLocalResourceObject("cbeCompanyToolTip"),  , 1)%></TD>
        <TD><LABEL id=0><%= GetLocalResourceObject("valAccBankCaption") %></LABEL></TD>
        <TD><%= mobjValues.PossiblesValues("valAccBank", "tabBank_acc", 2, , False, , , , , "CalAvaliable(this.value);", False, , GetLocalResourceObject("valAccBankToolTip"))%></TD>
	</TR>
	 <TR>
		<TD><LABEL ID=0><%= GetLocalResourceObject("valConceptCaption") %></LABEL></TD>
            <%With mobjValues.Parameters
	If IsNothing(Request.QueryString.Item("nCompany")) Then
		.Add("nCompany", 1, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	Else
		.Add("nCompany", Request.QueryString.Item("nCompany"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	End If
End With%>			   	
	    <TD><%=mobjValues.PossiblesValues("valConcept", "tabconceptscompany", eFunctions.Values.eValuesType.clngWindowType,  , True,  ,  ,  ,  ,  , True, 8, GetLocalResourceObject("valConceptToolTip"),  , 2)%></TD>
	    <TD><LABEL id=LABEL3><%= GetLocalResourceObject("lblAvaliableCaption") %></LABEL></TD>
        <TD><DIV id=Available CLASS=Field> </DIV></TD>
     </TR>      
     </TABLE>
     <TABLE WIDTH="100%" >   
     <TR>
        <TD COLSPAN="2" CLASS=HIGHLIGHTED><LABEL ID=0><%= GetLocalResourceObject("AnchorCaption") %></LABEL></TD>
        <TD COLSPAN="1">&nbsp</TD>
        <TD COLSPAN="2" CLASS=HIGHLIGHTED><LABEL ID=0><%= GetLocalResourceObject("Anchor2Caption") %></LABEL></TD>
     </TR>
     <TR>
 	    <TD COLSPAN="2" CLASS="Horline"></TD>
	    <TD></TD>
	    <TD COLSPAN="2" CLASS="Horline"></TD>
     </TR>     
     <TR>		
		 <TD><LABEL ID=0><%= GetLocalResourceObject("tcdStartDateCaption") %></LABEL></TD>
         <TD><%=mobjValues.DateControl("tcdStartDate",  ,  , GetLocalResourceObject("tcdStartDateToolTip"),  ,  ,  ,  , True, 3)%></TD>
         <TD COLSPAN="1">&nbsp</TD>
         <TD><%=mobjValues.OptionControl(0, "optTypeOper", GetLocalResourceObject("optTypeOper_1Caption"), CStr(1), "1",  , True, 5, GetLocalResourceObject("optTypeOper_1ToolTip"))%></TD>
	</TR>
	<TR>		
		 <TD><LABEL ID=0><%= GetLocalResourceObject("tcdEndDateCaption") %></LABEL></TD>
         <TD><%=mobjValues.DateControl("tcdEndDate",  ,  , GetLocalResourceObject("tcdEndDateToolTip"),  ,  ,  ,  , True, 4)%></TD>
         <TD COLSPAN="1">&nbsp</TD>
         <TD><%=mobjValues.OptionControl(0, "optTypeOper", GetLocalResourceObject("optTypeOper_2Caption"),  , "2",  , True, 6, GetLocalResourceObject("optTypeOper_2ToolTip"))%></TD>
	</TR>
    </TABLE>
<%
    mobjValues = Nothing
%>
</FORM>
</BODY>
</HTML>




