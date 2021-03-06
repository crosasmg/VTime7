<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">
'^Begin Header Block VisualTimer Utility 1.1 7/4/03 10.28.05
Dim mobjNetFrameWork As eNetFrameWork.Layout
'~End Header Block VisualTimer Utility

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'- Objeto para el manejo del menú
Dim mobjMenu As eFunctions.Menues


'% inspreCAL854: se definen los campos de la forma
'--------------------------------------------------------------------------------------------
Private Sub insPreCAL854()
	'--------------------------------------------------------------------------------------------
	
Response.Write("	" & vbCrLf)
Response.Write("<BR><BR>" & vbCrLf)
Response.Write("	")

	Response.Write(mobjValues.ShowWindowsName(Request.QueryString.Item("sCodispl"), Request.QueryString.Item("sWindowDescript")))
Response.Write("" & vbCrLf)
Response.Write("<BR><BR>	" & vbCrLf)
Response.Write("	<TABLE WIDTH=""100%"" BORDER=0 >" & vbCrLf)
Response.Write("	    <BR>" & vbCrLf)
Response.Write("	    <TR>" & vbCrLf)
Response.Write("	        <TD CLASS=""HighLighted""  WIDTH=""30%"" ><LABEL ID=0>" & GetLocalResourceObject("AnchorCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("	        <TD WIDTH=""20%"" ></TD>" & vbCrLf)
Response.Write("	        <TD><LABEL ID=0>" & GetLocalResourceObject("valOriginCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("            <TD>")


Response.Write(mobjValues.PossiblesValues("valOrigin", "Table5580", eFunctions.Values.eValuesType.clngWindowType,  ,  ,  ,  ,  ,  , "", False,  , GetLocalResourceObject("valOriginToolTip")))


Response.Write("</TD>" & vbCrLf)
Response.Write("	    </TR>    " & vbCrLf)
Response.Write("	    <TR>" & vbCrLf)
Response.Write("            <TD CLASS=""HorLine""></TD>" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("	    <TR>" & vbCrLf)
Response.Write("			<TD>" & vbCrLf)
Response.Write("	            ")


Response.Write(mobjValues.OptionControl(1, "ChkOption", GetLocalResourceObject("ChkOption_CStr1Caption"), CStr(1), CStr(1)))


Response.Write("" & vbCrLf)
Response.Write("	            ")


Response.Write(mobjValues.OptionControl(1, "ChkOption", GetLocalResourceObject("ChkOption_CStr2Caption"), CStr(0), CStr(2)))


Response.Write("" & vbCrLf)
Response.Write("	        </TD>" & vbCrLf)
Response.Write("	        <TD></TD>" & vbCrLf)
Response.Write("	        <TD><LABEL ID=0>" & GetLocalResourceObject("tcdDateCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("			<TD>")


Response.Write(mobjValues.DateControl("tcdDate",  ,  , GetLocalResourceObject("tcdDateToolTip")))


Response.Write("</TD>" & vbCrLf)
Response.Write("			</TR>" & vbCrLf)
Response.Write("		            <TR>" & vbCrLf)
Response.Write("                <TD><LABEL ID=13372>" & GetLocalResourceObject("cbeBranchCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("                <TD>")


Response.Write(mobjValues.BranchControl("cbeBranch", GetLocalResourceObject("cbeBranchToolTip")))


Response.Write(" </TD>" & vbCrLf)
Response.Write("            </TR> " & vbCrLf)
Response.Write("            <TR>" & vbCrLf)
Response.Write("                <TD><LABEL ID=13382>" & GetLocalResourceObject("valProductCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("                <TD>")


Response.Write(mobjValues.ProductControl("valProduct", GetLocalResourceObject("valProductToolTip")))


Response.Write("</TD>" & vbCrLf)
Response.Write("            </TR>" & vbCrLf)
Response.Write("" & vbCrLf)
Response.Write("	</TABLE>")

End Sub

</script>
<%Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage("CAL854_k")

mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.28.05
mobjValues.sSessionID = Session.SessionID
mobjValues.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjValues.sCodisplPage = "CAL854_k"
mobjMenu = New eFunctions.Menues
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.28.05
mobjMenu.sSessionID = Session.SessionID
mobjMenu.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility
%>
<HTML>
<HEAD>
<SCRIPT>
//- Variable para el control de versiones
    document.VssVersion="$$Revision: 2 $|$$Date: 16/11/04 14:51 $|$$Author: Nvaplat15 $"
</SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tmenu.js"></SCRIPT>
<SCRIPT>
//% insCancel: Ejecuta rutinas necesarias en el momento de cancelar la página.
//------------------------------------------------------------------------------------------
function insCancel(){
//------------------------------------------------------------------------------------------
	return true;
}

//% insStateZone: Se controla el estado de los campos de la página.
//------------------------------------------------------------------------------------------
function  insStateZone(){
//------------------------------------------------------------------------------------------
}
//% BlankOfficeDepend: Blanquea los campos OFICINA y AGENCIA si y sólo si el valor del
//%                 campo SUCURSAL cambia
//-------------------------------------------------------------------------------------
function BlankOfficeDepend()
//-------------------------------------------------------------------------------------
{
    with(document.forms[0]){
        cbeOfficeAgen.value="";
        cbeOfficeAgen_nBran_off.value = "";
    }
    UpdateDiv('cbeOfficeAgenDesc','');
}

</SCRIPT>
    <META NAME = "GENERATOR" Content = "Microsoft Visual Studio 6.0">


	<%With Response
	.Write(mobjValues.StyleSheet())
	.Write(mobjMenu.MakeMenu(Request.QueryString.Item("sCodispl"), "CAL854_k.aspx", 1, Request.QueryString.Item("sWindowDescript"), Session("sDesMultiCompany"), Session("sSche_code")))
	.Write(mobjMenu.setZone(1, "CAL854", Request.QueryString.Item("sWindowDescript"), CShort(Request.QueryString.Item("nWindowTy"))))
End With
mobjMenu = Nothing
%>

</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="post" ID="FORM" NAME="CAL854" ACTION="valPolicyRep.aspx?sMode=1">
<%Call insPreCAL854()
mobjValues = Nothing
%>
</FORM>
</BODY>
</HTML>

<%'^Begin Footer Block VisualTimer Utility 1.1 7/4/03 10.28.05
Call mobjNetFrameWork.FinishPage("CAL854_k")
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>




