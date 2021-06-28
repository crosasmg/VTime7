<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">
'^Begin Header Block VisualTimer Utility 1.1 7/4/03 10.53.46
Dim mobjNetFrameWork As eNetFrameWork.Layout
'~End Header Block VisualTimer Utility

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'- Objeto para el manejo de las rutinas genéricas
Dim mobjMenu As eFunctions.Menues


</script>
<%Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage("CR781_K")
mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.53.46
mobjValues.sSessionID = Session.SessionID
mobjValues.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjValues.sCodisplPage = "CR781_K"
mobjMenu = New eFunctions.Menues
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.53.46
mobjMenu.sSessionID = Session.SessionID
mobjMenu.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjValues.ActionQuery = CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 401
Session("bQuery") = mobjValues.ActionQuery

%>
<HTML>
<HEAD>
    <META NAME = "GENERATOR" CONTENT = "Microsoft Visual Studio 6.0">
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tmenu.js"></SCRIPT>		
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/ValFunctions.js"></SCRIPT>	




<SCRIPT LANGUAGE=JavaScript>
//- Variable para el control de versiones
	document.VssVersion="$$Revision: 2 $|$$Date: 26/04/06 11:56 $|$$Author: Pgarin $"


//% DisabledCoverGen: Habilita y desabilita el de cobertura generica si es Vida
//--------------------------------------------------------------------------------------------
function DisabledCoverGen(Field){
//--------------------------------------------------------------------------------------------
	with (self.document.forms[0])
	{
		if(Field=='40')
		{
			valCovergen.disabled = false;
			btnvalCovergen.disabled = false;		
		}
		else
		{
			valCovergen.disabled = true;
			btnvalCovergen.disabled = true;
		}
	}
}


//% insStateZone: se controla el estado de los campos de la página
//--------------------------------------------------------------------------------------------
function insStateZone(){
//--------------------------------------------------------------------------------------------
 	with (self.document.forms[0])
 	{
 		cbeBranch.disabled= false;
 		valProduct.disabled= false;
 		tcnBranch_rei.disabled= false;
		tcnNumber.disabled= false;
		tcdEffecdate.disabled= false;
		btn_tcdEffecdate.disabled= false;
		tcdEffecdate.disabled= false;
		btn_tcdEffecdate.disabled= false;
		tcnPrem_Aseg.disabled = false;
		tcnPrem_Adic.disabled = false;
		if(top.fraSequence.plngMainAction==301 || top.fraSequence.plngMainAction==302 ||
			top.fraSequence.plngMainAction==306 || top.fraSequence.plngMainAction==401)
		{
			valCovergen.disabled = false;
			btnvalCovergen.disabled = false;		
		}
	}
}
	
//% insCancel: se controla la acción Cancelar de la página
//--------------------------------------------------------------------------------------------
function insCancel(){
//--------------------------------------------------------------------------------------------
	return true;
}

//% insFinish: se controla la acción Cancelar de la página
//--------------------------------------------------------------------------------------------
function insFinish(){
//--------------------------------------------------------------------------------------------
    return true;
}


//% ShowChangeValues: Se cargan los valores de acuerdo a los datos recibidos
//-------------------------------------------------------------------------------------------
function ShowChangeValues(lobjOption){
//-------------------------------------------------------------------------------------------
    with (document.forms[0]){
     switch (lobjOption) {
			case "tcnNumber":
	               ShowPopUp("/VTimeNet/CoReinsuran/CoReinsurantra/ShowDefValues.aspx?Field=" + "ShowDefValuesCR781" + "&nBranch=" + cbeBranch.value + "&nProduct=" + valProduct.value + "&nBranch_rei=" + tcnBranch_rei.value + "&nNumber=" + tcnNumber.value + "&nCovergen=" + valCovergen.value + "&dEffecdate=" + tcdEffecdate.value  ,1,1,"no","no",2000,2000);
		          break;
	}
 }	
}	


</SCRIPT>
<%
With Response
	.Write(mobjValues.StyleSheet() & vbCrLf)
	.Write(mobjMenu.setZone(2, "CR781_K", Request.QueryString.Item("sWindowDescript"), CShort(Request.QueryString.Item("nWindowTy"))))
	.Write(mobjMenu.MakeMenu("CR781", "CR781_K.aspx", 1, vbNullString))
End With
mobjMenu = Nothing
%>

</HEAD>


<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" ID="FORM" NAME="CR781" ACTION="ValCoReinsuranTra.aspx?sMode=1">


&nbsp
&nbsp
&nbsp
    <%Response.Write(mobjValues.ShowWindowsName("CR781_K", Request.QueryString.Item("sWindowDescript")))%>
	<BR>    

    <TABLE WIDTH="100%">
   		<TR>                       
			<TD>&nbsp</TD>
			<TD>&nbsp</TD>
			<TD>&nbsp</TD>
		</TR>        

		<TR>
            <TD><LABEL ID=0><%= GetLocalResourceObject("cbeBranchCaption") %></LABEL></TD>
            <TD><%=mobjValues.BranchControl("cbeBranch", GetLocalResourceObject("cbeBranchToolTip"),  ,  ,  ,  ,  ,  , True, 1)%></TD>
            <TD><LABEL ID=0><%= GetLocalResourceObject("valProductCaption") %></LABEL></TD>
            <TD><%=mobjValues.ProductControl("valProduct", GetLocalResourceObject("valProductToolTip"),  , eFunctions.Values.eValuesType.clngWindowType, True,  ,  ,  ,  ,  , 2, True)%></TD>            
		</TR>

        <TR>
			<TD><LABEL><%= GetLocalResourceObject("valCovergenCaption") %></LABEL></TD>
            <TD><%=mobjValues.PossiblesValues("valCovergen", "tabtab_lifcov2", eFunctions.Values.eValuesType.clngWindowType,  ,  ,  ,  ,  ,  ,  , True,  , GetLocalResourceObject("valCovergenToolTip"))%> </TD>
            <TD><LABEL><%= GetLocalResourceObject("tcnBranch_reiCaption") %></LABEL></TD>
            <TD><%=mobjValues.PossiblesValues("tcnBranch_rei", "table5000", eFunctions.Values.eValuesType.clngComboType,  ,  ,  ,  ,  ,  ,  , True,  , GetLocalResourceObject("tcnBranch_reiToolTip"))%></TD>
        </TR>
        <TR>
            <TD><LABEL><%= GetLocalResourceObject("tcnNumberCaption") %></LABEL></TD>
			<TD><%=mobjValues.NumericControl("tcnNumber", 5, vbNullString,  , GetLocalResourceObject("tcnNumberToolTip"),  ,  ,  ,  ,  , "ShowChangeValues(""tcnNumber"")", True)%></TD>
			<TD><LABEL><%= GetLocalResourceObject("tcdEffecdateCaption") %></LABEL></TD>
<TD><% %>
<%=mobjValues.DateControl("tcdEffecdate", CStr(Today),  , GetLocalResourceObject("tcdEffecdateToolTip"),  ,  ,  ,  , True)%></TD>
		</TR>
    </TABLE>
    <TABLE>
   		<TR>                       
			<TD>&nbsp</TD>
			<TD>&nbsp</TD>
		</TR>        
   		<TR>                       
			<TD WIDTH="45%" COLSPAN="1" CLASS="HighLighted"><LABEL><A NAME="Aplicación"></A>Primas de Reaseguro </LABEL></TD>
			<TD WIDTH="100%">&nbsp</TD>
		</TR>        
		<TR>
		    <TD COLSPAN="1"><HR></TD>
		    <TD WIDTH="10%">&nbsp</TD>
		    <TD COLSPAN="1"><HR></TD>
		</TR>
    </TABLE>
    <TABLE WIDTH="40%">
		<TR>
			<TD><LABEL><%= GetLocalResourceObject("tcnPrem_AsegCaption") %></LABEL></TD>
			<TD><%=mobjValues.NumericControl("tcnPrem_Aseg", 18,  ,  , GetLocalResourceObject("tcnPrem_AsegToolTip"),  , 6,  ,  ,  ,  , True)%></TD>
		</TR>        
		<TR>
			<TD><LABEL><%= GetLocalResourceObject("tcnPrem_AdicCaption") %></LABEL></TD>
			<TD><%=mobjValues.NumericControl("tcnPrem_Adic", 18,  ,  , GetLocalResourceObject("tcnPrem_AdicToolTip"),  , 6,  ,  ,  ,  , True)%></TD>
		</TR>        
    </TABLE>

</FORM>
</BODY>
</HTML>

<%
mobjValues = Nothing%>


<%'^Begin Footer Block VisualTimer Utility 1.1 7/4/03 10.53.46
Call mobjNetFrameWork.FinishPage("CR781_K")
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>




