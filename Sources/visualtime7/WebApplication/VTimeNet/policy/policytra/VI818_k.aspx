<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">
'^Begin Header Block VisualTimer Utility 1.1 7/4/03 10.31.20
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
Call mobjNetFrameWork.BeginPage("VI818_k")
mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.31.20
mobjValues.sSessionID = Session.SessionID
mobjValues.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjValues.sCodisplPage = "VI818_k"
mobjMenu = New eFunctions.Menues
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.31.20
mobjMenu.sSessionID = Session.SessionID
mobjMenu.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility
%>

<HTML>
<HEAD>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tmenu.js"></SCRIPT>


<SCRIPT>
//- Variable para el control de versiones 
    document.VssVersion="$$Revision: 2 $|$$Date: 21/10/09 10:31a $|$$Author: Gletelier $"  

//% insFinish: se controla la acción Finalizar de la página
//------------------------------------------------------------------------------------------
function insFinish(){
//------------------------------------------------------------------------------------------
	return(true);
}
//% insStateZone: se controla el estado de los campos de la página
//------------------------------------------------------------------------------------------
function insStateZone(){
//------------------------------------------------------------------------------------------
	with (self.document.forms[0]){
		cbeBranch.disabled = false
		tcnPolicy.disabled = false
		tcnCertif.disabled = false
		tcdEffecdate.disabled = false
		btn_tcdEffecdate.disabled = false		
	}

}
//% insPreZone: Modifica el comportamiento de la página dependiendo de la acción
//% que proviene del menú principal
//------------------------------------------------------------------------------------------
function insPreZone(llngAction)
//------------------------------------------------------------------------------------------
{
}
//% insCancel: se controla la acción Cancelar de la página
//------------------------------------------------------------------------------------------
function insCancel()		
//------------------------------------------------------------------------------------------
{
	return true
}
//% insChangeField: Se recargan los valores cuando cambia el campo
//-------------------------------------------------------------------------------------------
function insChangeField(Field){
//-------------------------------------------------------------------------------------------    
	with (self.document.forms[0]){
		switch(Field.name){
			case "tcnPolicy":
				insDefValues("Policy_CA099","sCodispl=VI818&nBranch=" + cbeBranch.value + "&nProduct=" + valProduct.value + "&nPolicy= " + tcnPolicy.value)
				break;
		}
	}
}
//-------------------------------------------------------------------------------------------
function ChangeValues(){
//-------------------------------------------------------------------------------------------    
	with (self.document.forms[0]){
		tcnPolicy.value='';
		tcnCertif.value='';
	}
}

function FindPolicy(){
//-----------------------------------------------------------------------------
	var frm = self.document.forms[0];
	insDefValues('Switch_Curr_Pol', 'nBranch=' + frm.cbeBranch.value +
                                    '&nProduct=' + frm.valProduct.value +
                                    '&nPolicy=' + frm.tcnPolicy.value +
									'&dEffecdate=' + frm.tcdEffecdate.value +                                     
                                    '&sCodispl=VI818');
}

</SCRIPT>
		<META NAME = "GENERATOR" Content = "Microsoft Visual Studio 6.0">
		<%With Response
	.Write(mobjValues.StyleSheet() & vbCrLf)
	.Write(mobjMenu.MakeMenu("VI818", "VI818_K.aspx", 1, ""))
End With
mobjMenu = Nothing
%>
	</HEAD>
	<BODY ONUNLOAD="closeWindows();">
		<TD><BR></TD>
		<FORM METHOD="post" ID="FORM" NAME="VI818" ACTION="valPolicyTra.aspx?x=1">
	<TABLE WIDTH=100%>
		<BR></BR>
		<BR></BR>
		<TR>
			<TD WIDTH=25%><LABEL ID=13901><%= GetLocalResourceObject("cbeBranchCaption") %></LABEL></TD>
			<TD>
				<%Response.Write(mobjValues.HiddenControl("tctCertype", "2"))
Response.Write(mobjValues.BranchControl("cbeBranch", GetLocalResourceObject("cbeBranchToolTip"), , "valProduct",  ,  ,  ,  , True))
%>
			</TD>
			<TD>&nbsp;</TD>
			<TD COLSPAN="2" CLASS="HighLighted"><LABEL ID=0><%= GetLocalResourceObject("AnchorCaption") %></LABEL></TD>
		</TR>
		<TR>
			<TD COLSPAN="3"></TD>
			<TD COLSPAN="2" CLASS="HorLine"></TD>
		</TR>
		<TR>
			<TD><LABEL ID=13909><%= GetLocalResourceObject("valProductCaption") %></LABEL></TD>
			<TD><%Response.Write(mobjValues.ProductControl("valProduct", GetLocalResourceObject("valProductToolTip"),  ,  , True))%></TD>
			<TD>&nbsp;</TD>
			<TD COLSPAN="2"><%=mobjValues.OptionControl(0, "optExecute", GetLocalResourceObject("optExecute_1Caption"), "1", "1")%></TD>
		</TR>
		<TR>
		    <TD><LABEL ID=13803><%= GetLocalResourceObject("tcnPolicyCaption") %></LABEL></TD>
			<TD><%Response.Write(mobjValues.NumericControl("tcnPolicy", 10,  ,  , GetLocalResourceObject("tcnPolicyToolTip"),  , 0,  ,  ,  , "insChangeField(tcnPolicy);FindPolicy();", True))%><TD>
			<TD COLSPAN="2"><%=mobjValues.OptionControl(0, "optExecute", GetLocalResourceObject("optExecute_2Caption"), "", "2")%></TD>
		</TR>
		<TR>
			<TD><LABEL ID=0><%= GetLocalResourceObject("tcnCertifCaption") %></LABEL></TD>
			<TD><%Response.Write(mobjValues.NumericControl("tcnCertif", 10,  ,  , GetLocalResourceObject("tcnCertifToolTip"),  ,  ,  ,  ,  ,  , True))%></TD>
            <TD>&nbsp;</TD>
            <TD><LABEL><%= GetLocalResourceObject("tcdEffecdateCaption") %></LABEL></TD>
            <TD><%=mobjValues.DateControl("tcdEffecdate",  ,  , GetLocalResourceObject("tcdEffecdateToolTip"),  ,  ,  ,  , True)%></TD>

		</TR>
	</TABLE>
</FORM>
</BODY>
</HTML>
<%
mobjValues = Nothing%>





