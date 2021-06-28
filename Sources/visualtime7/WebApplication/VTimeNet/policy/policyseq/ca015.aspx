<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="ePolicy" %>
<script language="VB" runat="Server">
'^Begin Header Block VisualTimer Utility 1.1 31/3/03 19.42.03
Dim mobjNetFrameWork As eNetFrameWork.Layout

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values
Dim mobjMenu As eFunctions.Menues
Dim mobjFranchise As ePolicy.Franchise



'% insPreCA015: Se buscan los datos para los campos de la página
'--------------------------------------------------------------------------------------------------
Function insPreCA015() As Object
	'--------------------------------------------------------------------------------------------------
	mobjFranchise = New ePolicy.Franchise
	
	Call mobjFranchise.insPreCA015(Session("sCertype"), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("npolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nCertif"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate))
End Function

</script>
<%Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage("CA015")
'~End Header Block VisualTimer Utility
Response.CacheControl = "private"

mobjMenu = New eFunctions.Menues
'^Begin Body Block VisualTimer Utility 1.1 31/3/03 19.42.04
mobjMenu.sSessionID = Session.SessionID
mobjMenu.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility
mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 31/3/03 19.42.04
mobjValues.sSessionID = Session.SessionID
mobjValues.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjValues.sCodisplPage = Request.QueryString.Item("sCodispl")

mobjValues.ActionQuery = Session("bQuery")

Call insPreCA015()
%>
<SCRIPT>

//% insOptfranchCA015: Este procedimiento es invocado cada vez que el usuario selecciona una de las
//% opciones del campo Tipo de Franquicia/Deducible asociado a la Póliza
//-------------------------------------------------------------------------------------------
function optFranchiseTypeChange(FranchiseType) {
//-------------------------------------------------------------------------------------------
	
    with (self.document.forms[0]) {
//+	Si el usuario señala que "No Tiene" Franquicia/Deducible, entonces se inicializan los campos de la ventana
		if (FranchiseType.value == 1) {
			cbeFranqApl.value = 1
			cbeFranqApl.disabled = true
			cbeCurrencyFD.value = ""
			cbeCurrencyFD.disabled = true
			tcnDiscountPerc.value = ""
			tcnDiscountPerc.disabled = true
			tcnDiscountAmou.value = ""
			tcnDiscountAmou.disabled = true
			tcnFranchisePerc.value = ""
			tcnFranchisePerc.disabled = true
			tcnFranchiseAmou.value = ""
			tcnFranchiseAmou.disabled = true
			tcnFranchiseMin.value = ""
			tcnFranchiseMin.disabled = true
			tcnFranchiseMax.value = ""
			tcnFranchiseMax.disabled = true
		} 
		else	
		{
//+	Se coloca el valor anterior que ya había sido leído del Diseñador o se limpia.
			cbeFranqApl.value = <%=mobjFranchise.sFrancApl%>
			cbeFranqApl.disabled = false
			cbeCurrencyFD.disabled = false
			tcnDiscountPerc.disabled = false
			tcnDiscountAmou.disabled = false
			tcnFranchisePerc.disabled = false
			tcnFranchiseAmou.disabled = false				
		}
	}			
}

//% insGmnFranchCA015: Este procedimiento es invocado cada vez que el usuario modifica el campo
//% Porcentaje de Franquicia/Deducible asociado a la Póliza
//-------------------------------------------------------------------------------------------
function tcnFranchisePercChange() {
//-------------------------------------------------------------------------------------------
//+ Si no se indica un porcentaje de Franquicia/Deducible, los campos correspondientes a condiciones
//+ quedan con valor 0 y deshabilitados

    with (self.document.forms[0]) {
		if(tcnFranchisePerc.value == 0) {
			tcnFranchiseMin.value = 0
			tcnFranchiseMin.disabled = true
			tcnFranchiseMax.value = 0
			tcnFranchiseMax.disabled = true
		} 
		else {
	        tcnFranchiseMin.disabled = tcnFranchisePerc.disabled
			tcnFranchiseMax.disabled = tcnFranchisePerc.disabled
		}

    }    
}

//% Si se ingresa un porcetanje de franquicia/deducible se habilitan los campos máximo
//% y mínimo si no se deshabilitan.
//--------------------------------------------------------------------------------------------------------------
function insDisabledFields(Field){
//--------------------------------------------------------------------------------------------------------------
	if((Field.value>0 || !isNaN(Field.value)) && (Field.value !==""))
	{
		self.document.forms[0].elements["tcnFranchiseMin"].disabled=false;
		self.document.forms[0].elements["tcnFranchiseMax"].disabled=false;
	}
	else
	{
		self.document.forms[0].elements["tcnFranchiseMin"].value="0";
		self.document.forms[0].elements["tcnFranchiseMax"].value="0";
		self.document.forms[0].elements["tcnFranchiseMin"].disabled=true;
		self.document.forms[0].elements["tcnFranchiseMax"].disabled=true;
	}
}
</SCRIPT>

<HTML>
<HEAD>


<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
    <META NAME = "GENERATOR" CONTENT = "Microsoft Visual Studio 6.0">
    <%=mobjValues.StyleSheet()%>
    <%Response.Write(mobjMenu.setZone(2, Request.QueryString.Item("sCodispl"), Request.QueryString.Item("sWindowDescript"), CShort(Request.QueryString.Item("nWindowTy"))))
mobjMenu = Nothing%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="post" ID="FORM" NAME="CA015" ACTION="valPolicySeq.aspx?nMainAction=301&nHolder=1">
<%=mobjValues.ShowWindowsName(Request.QueryString.Item("sCodispl"), Request.QueryString.Item("sWindowDescript"))%>
    <TABLE WIDTH="100%">
        <TR>
            <TD ROWSPAN="4" COLS="2" WIDTH=47%>
				<TABLE WIDTH=100%>
					<TR>
						<TD CLASS="HighLighted" COLSPAN="2"><LABEL ID=40818><A NAME="Tipo"><%= GetLocalResourceObject("AnchorTipoCaption") %></A></LABEL></TD>
					</TR>
					<TR>
						<TD CLASS="HorLine" COLSPAN="2"></TD>
					</TR>
					<TR>
						<TD COLSPAN="2"><%=mobjValues.OptionControl(40822, "optFranchiseType", GetLocalResourceObject("optFranchiseType_CStr1Caption"), mobjFranchise.GetFranchiseType(1), CStr(1), "optFranchiseTypeChange(this)",  ,  , GetLocalResourceObject("optFranchiseType_CStr1ToolTip"))%></TD>
					</TR>
					<TR>
						<TD COLSPAN="2"><%=mobjValues.OptionControl(40823, "optFranchiseType", GetLocalResourceObject("optFranchiseType_CStr2Caption"), mobjFranchise.GetFranchiseType(2), CStr(2), "optFranchiseTypeChange(this)",  ,  , GetLocalResourceObject("optFranchiseType_CStr2ToolTip"))%></TD>
					</TR>
					<TR>
						<TD COLSPAN="2"><%=mobjValues.OptionControl(40824, "optFranchiseType", GetLocalResourceObject("optFranchiseType_CStr3Caption"), mobjFranchise.GetFranchiseType(3), CStr(3), "optFranchiseTypeChange(this)",  ,  , GetLocalResourceObject("optFranchiseType_CStr3ToolTip"))%></TD>
					</TR>
				</TABLE>
            </TD>
            <TD COLSPAN="3">&nbsp;</TD>
        </TR>
        <TR>
            <TD WIDTH=50pcx>&nbsp;</TD>
            <TD><LABEL ID=13085><%= GetLocalResourceObject("cbeFranqAplCaption") %></LABEL></TD>
            <%If CDbl(mobjFranchise.sFrancApl) <> 1 Then%>
				<TD><%=mobjValues.PossiblesValues("cbeFranqApl", "table33", eFunctions.Values.eValuesType.clngComboType, mobjFranchise.sFrancApl,  ,  ,  ,  ,  ,  , True, 1, GetLocalResourceObject("cbeFranqAplToolTip"))%></TD>
			<%Else%>				
				<TD><%=mobjValues.PossiblesValues("cbeFranqApl", "table33", eFunctions.Values.eValuesType.clngComboType, mobjFranchise.sFrancApl,  ,  ,  ,  ,  ,  ,  , 1, GetLocalResourceObject("cbeFranqAplToolTip"))%></TD>
			<%End If%>
        </TR>
        <TR>
            <TD>&nbsp;</TD>
            <TD><LABEL ID=13078><%= GetLocalResourceObject("cbeCurrencyFDCaption") %></LABEL></TD>
            <TD><%=mobjValues.PossiblesValues("cbeCurrencyFD", "table11", 1, CStr(mobjFranchise.nCurrency),  ,  ,  ,  ,  ,  ,  , 5, GetLocalResourceObject("cbeCurrencyFDToolTip"))%></TD>
        </TR>
        <TR>
            <TD COLSPAN="3">&nbsp;</TD>
        </TR>
    </TABLE>
    <TABLE WIDTH=100%>
        <TR>
            <TD ROWSPAN="1" COLS="2">
				<TABLE WIDTH=100%>
					<TR>
						<TD COLSPAN="2" CLASS="HighLighted"><LABEL ID=40819><A NAME="Descuento"><%= GetLocalResourceObject("AnchorDescuentoCaption") %></A></LABEL></TD>
					</TR>
					<TR>
						<TD CLASS="HorLine" COLSPAN="2"></TD>
					</TR>
					<TR>
						<TD><LABEL ID=13080><%= GetLocalResourceObject("tcnDiscountPercCaption") %></LABEL></TD>
						<TD><%=mobjValues.NumericControl("tcnDiscountPerc", 4, CStr(mobjFranchise.nDiscount),  , GetLocalResourceObject("tcnDiscountPercToolTip"), True, 2)%></TD>
					</TR>
					<TR>
						<TD><LABEL ID=13079><%= GetLocalResourceObject("tcnDiscountAmouCaption") %></LABEL></TD>
						<TD><%=mobjValues.NumericControl("tcnDiscountAmou", 18, CStr(mobjFranchise.nDisc_amoun),  , GetLocalResourceObject("tcnDiscountAmouToolTip"), True, 6)%></TD>
					</TR>
				</TABLE>
            </TD>
            <TD WIDTH=50pcx>&nbsp;</TD>
            <TD ROWSPAN="1" COLS="2">
				<TABLE WIDTH=100%>
					<TR>
						<TD COLSPAN="2" CLASS="HighLighted"><LABEL ID=40820><A NAME="Determinación"><%= GetLocalResourceObject("AnchorDeterminaciónCaption") %></A></LABEL></TD>
					</TR>
					<TR>
						<TD CLASS="HorLine" COLSPAN="2"></TD>
					</TR>
					<TR>
						<TD><LABEL ID=19340><%= GetLocalResourceObject("tcnFranchisePercCaption") %></LABEL></TD>
						<TD><%=mobjValues.NumericControl("tcnFranchisePerc", 4, CStr(mobjFranchise.nRate),  , GetLocalResourceObject("tcnFranchisePercToolTip"), True, 2,  ,  ,  , "tcnFranchisePercChange()")%></TD>
					</TR>
					<TR>
						<TD><LABEL ID=13081><%= GetLocalResourceObject("tcnFranchiseAmouCaption") %></LABEL></TD>
						<TD><%=mobjValues.NumericControl("tcnFranchiseAmou", 18, CStr(mobjFranchise.nFixamount),  , GetLocalResourceObject("tcnFranchiseAmouToolTip"), True, 6)%></TD>
					</TR>
				</TABLE>
            </TD>
        </TR>
	</TABLE>
	<TABLE WIDTH=100% COLS=5>
        <TR>
            <TD COLSPAN="5" CLASS="HighLighted"><LABEL ID=40821><A NAME="Condiciones"><%= GetLocalResourceObject("AnchorCondicionesCaption") %></A></LABEL></TD>
        </TR>
        <TR>
            <TD CLASS="HorLine" COLSPAN="5"></TD>
        </TR>
        <TR>
			<TD><LABEL ID=13083><%= GetLocalResourceObject("tcnFranchiseMinCaption") %></LABEL></TD>
			<TD><%=mobjValues.NumericControl("tcnFranchiseMin", 18, mobjValues.StringToType(CStr(mobjFranchise.nMinamount), eFunctions.Values.eTypeData.etdDouble),  , GetLocalResourceObject("tcnFranchiseMinToolTip"), True, 6)%></TD>
			<TD WIDTH=100pcx>&nbsp;</TD>
			<TD><LABEL ID=13082><%= GetLocalResourceObject("tcnFranchiseMaxCaption") %></LABEL></TD>
			<TD><%=mobjValues.NumericControl("tcnFranchiseMax", 18, mobjValues.StringToType(CStr(mobjFranchise.nMaxamount), eFunctions.Values.eTypeData.etdDouble),  , GetLocalResourceObject("tcnFranchiseMaxToolTip"), True, 6)%></TD>
        </TR>
    </TABLE>
</FORM>
</BODY>
</HTML>
<%
Response.Write("<SCRIPT> tcnFranchisePercChange() </SCRIPT>")
mobjFranchise = Nothing
mobjValues = Nothing
%>
<%'^Begin Footer Block VisualTimer Utility 1.1 31/3/03 19.42.04
Call mobjNetFrameWork.FinishPage("CA015")
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>




