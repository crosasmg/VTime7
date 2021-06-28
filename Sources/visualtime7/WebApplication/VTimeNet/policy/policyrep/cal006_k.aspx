<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">
'^Begin Header Block VisualTimer Utility 1.1 7/4/03 10.28.02
Dim mobjNetFrameWork As eNetFrameWork.Layout
'- Objeto para el manejo de las funciones generales de carga de valores

Dim mobjValues As eFunctions.Values
Dim mobjMenu As eFunctions.Menues


</script>
<%Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage("cal006_k")
'~End Header Block VisualTimer Utility
'' Response.CacheControl = False
mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.28.02
mobjValues.sSessionID = Session.SessionID
mobjValues.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjValues.sCodisplPage = "cal006_k"
mobjMenu = New eFunctions.Menues
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.28.02
mobjMenu.sSessionID = Session.SessionID
mobjMenu.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility
%>
<HTML>
<HEAD>
<SCRIPT>
//- Variable para el control de versiones
    document.VssVersion="$$Revision: 2 $|$$Date: 26/11/03 16:40 $|$$Author: Nvaplat37 $"
</SCRIPT>
    <META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/Constantes.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tmenu.js"></SCRIPT>




<SCRIPT>
//% insChangeArea: habilita/deshabilita las opciones
//----------------------------------------------------------------------------
function insChangeArea(){
//----------------------------------------------------------------------------
	with (self.document.forms[0])
	{
		if(cbeInsurArea.value == "2")
		{
			ShowDiv('divsOptOutput', 'show');
		}
		else
		{
			ShowDiv('divsOptOutput', 'show');
		    sOptOutput[0].disabled=false;
		    sOptOutput[1].disabled=false;
		    sOptOutput[2].disabled=false;
		    sOptOutput[0].checked=false;
		    sOptOutput[1].checked=false;
		    sOptOutput[2].checked=true;
		}

		if((.nOptAct[0].checked==true)
		{
			nOptAct.value = 1;
		}
		else
		{
			nOptAct.value = 2;
		}
	}
}
//% insEnabledField: habilita/deshabilita los campos que dependen del valor de otros
//-------------------------------------------------------------------------------
function insEnabledField(Field, Option){
//-------------------------------------------------------------------------------
    switch(Option){
        case "Detail":
            self.document.forms[0].sOptOutput[1].checked=false;
            self.document.forms[0].sOptOutput[2].checked=false;
            Field.value = "1";
            break;
        case "Summary":
            self.document.forms[0].sOptOutput[0].checked=false;
            self.document.forms[0].sOptOutput[2].checked=false;
            Field.value = "2";
            break;
        case "Both":
            self.document.forms[0].sOptOutput[0].checked=false;
            self.document.forms[0].sOptOutput[1].checked=false;
            Field.value = "3";
            break;
        case "LoadPage":
            self.document.forms[0].sOptOutput[0].checked=false;
            self.document.forms[0].sOptOutput[1].checked=false;
            self.document.forms[0].sOptOutput[2].checked=true;
    }
}
//% insFinish: Terminar transacción
//--------------------------------------------------------------------
function insFinish(){
//--------------------------------------------------------------------
    var nAction = new TypeActions();
//+ En modo consulta refresca la página
    if (top.frames["fraSequence"].plngMainAction == nAction.clngActionQuery){
        insReloadTop(false);
        return false;
    }
    else
//+ En otro modo ejecuta la validación
        return true;
}
//% insCancel: Anular ingreso
//--------------------------------------------------------------------
function insCancel(){
//--------------------------------------------------------------------
    return true;
}
//--------------------------------------------------------------------
function  insStateZone(){
//--------------------------------------------------------------------
}
</SCRIPT>
<%
With Response
	.Write(mobjValues.StyleSheet())
	.Write(mobjMenu.MakeMenu("CAL006", "CAL006_k.aspx", 1, Request.QueryString.Item("sWindowDescript"), Session("sDesMultiCompany"), Session("sSche_code")))
	.Write(mobjMenu.setZone(1, "CAL006", Request.QueryString.Item("sWindowDescript"), CShort(Request.QueryString.Item("nWindowTy"))))
End With
mobjMenu = Nothing
%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<BR><BR>
<FORM METHOD="post" ID="FORM" NAME="frmRenDateChange" ACTION="ValPolicyRep.aspx?x=1">
    	<%Response.Write(mobjValues.ShowWindowsName("CAL006", Request.QueryString.Item("sWindowDescript")))%>
	<BR><BR>
	<TABLE WIDTH="100%">
	    <TR>
			<TD WIDTH="70%">
				<TABLE WIDTH="100%">
					<TR>
						<TD><LABEL ID=0><%= GetLocalResourceObject("cbeInsurAreaCaption") %></LABEL></TD>
						<TD><%
						        mobjValues.BlankPosition = False
						        Response.Write(mobjValues.PossiblesValues("cbeInsurArea", "table5001", eFunctions.Values.eValuesType.clngComboType, Session("nInsur_area"),  , , , , ,"insChangeArea();",True, , GetLocalResourceObject("cbeInsurAreaToolTip")))
                            %>
						</TD>
					</TR>
					<TR>
						<TD COLSPAN=2></TD>
					</TR>
					<TR>
						<TD><LABEL ID=13937><%= GetLocalResourceObject("cbeBranchCaption") %></LABEL></TD>
						<TD><%=mobjValues.BranchControl("cbeBranch", GetLocalResourceObject("cbeBranchToolTip"))%></TD>
					</TR>
				    <TR>
						<TD><LABEL ID=13947><%= GetLocalResourceObject("valProductCaption") %></LABEL></TD>
						<TD><%=mobjValues.ProductControl("valProduct", GetLocalResourceObject("valProductToolTip"))%></TD>
					</TR>
					<TR>
					    <TD><LABEL ID=13722><%= GetLocalResourceObject("tcdEffecdateCaption") %></LABEL></TD>
<TD><% %>
<%=mobjValues.DateControl("tcdEffecdate", CStr(Today),  , GetLocalResourceObject("tcdEffecdateToolTip"))%></TD>
					</TR>
					<TR>
						<TD><LABEL ID=0><%= GetLocalResourceObject("cbeType_reserveCaption") %></LABEL></TD>
						<TD><%
						        mobjValues.TypeList = 2
						        '+Nota: Se excluye el tipo 80 ya que se procesa desde otra interfaz.
						        mobjValues.List = "80"
						        Response.Write(mobjValues.PossiblesValues("cbeType_reserve", "table127", eFunctions.Values.eValuesType.clngComboType, , , , , , , , , , GetLocalResourceObject("cbeType_reserveToolTip")))
					        
%>
						</TD>
					</TR>					
				</TABLE>
			</TD>
			<TD WIDTH="30%">
				<DIV ID="divsOptOutput">
					<TABLE WIDTH="100%">
						<!--TR>
							<TD CLASS="HIGHLIGHTED"><LABEL ID=0><%= GetLocalResourceObject("AnchorCaption") %></LABEL></TD>
						</TR>
						<TR>
							<TD CLASS="HorLine"></TD>
						</TR>
						<TR>
							<TD><%=mobjValues.OptionControl(0, "sOptOutput", GetLocalResourceObject("sOptOutput_3Caption"), "1", "3", "insEnabledField(this, ""Detail"");", False,  , GetLocalResourceObject("sOptOutput_3ToolTip"))%></TD>
						</TR>
						<TR>
							<TD><%=mobjValues.OptionControl(0, "sOptOutput", GetLocalResourceObject("sOptOutput_3Caption"), "2", "3", "insEnabledField(this, ""Summary"");", False,  , GetLocalResourceObject("sOptOutput_3ToolTip"))%></TD>
						</TR>
						<TR>
					        <TD><%=mobjValues.OptionControl(0, "sOptOutput", GetLocalResourceObject("sOptOutput_3Caption"), "3", "3", "insEnabledField(this, ""Both"");", False,  , GetLocalResourceObject("sOptOutput_3ToolTip"))%></TD>
							<%Response.Write(mobjValues.HiddenControl("sCertype", "2"))%>
						</TR-->
						<TR>
							<%Response.Write(mobjValues.HiddenControl("sOptOutput", "1"))%>
							<%Response.Write(mobjValues.HiddenControl("sCertype", "2"))%>
						</TR>
						<TR>
							<TD CLASS="HIGHLIGHTED"><LABEL ID=0><%= GetLocalResourceObject("Anchor2Caption") %></LABEL></TD>
						</TR>
						<TR>
							<TD CLASS="HorLine"></TD>
						</TR>
						<TR>
						    <TD><%=mobjValues.OptionControl(0, "nOptAct", GetLocalResourceObject("nOptAct_1Caption"), "1", "1", "insChangeArea();",  ,  , GetLocalResourceObject("nOptAct_1ToolTip"))%></TD>
						</TR>		
						<TR>
						    <TD><%=mobjValues.OptionControl(0, "nOptAct", GetLocalResourceObject("nOptAct_2Caption"),  , "2", "insChangeArea();",  ,  , GetLocalResourceObject("nOptAct_2ToolTip"))%></TD>
						</TR>					
					</TABLE>
				</DIV>
			</TD>
	</TABLE>
	<%
mobjValues = Nothing
mobjMenu = Nothing
%>
</FORM>
</BODY>
</HTML>
<SCRIPT>insChangeArea();</SCRIPT>
<%If Not Session("bQuery") Then%>
		<SCRIPT>insEnabledField("", "LoadPage");</SCRIPT>
<%End If%>
<%'^Begin Footer Block VisualTimer Utility 1.1 7/4/03 10.28.02
Call mobjNetFrameWork.FinishPage("cal006_k")
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>




