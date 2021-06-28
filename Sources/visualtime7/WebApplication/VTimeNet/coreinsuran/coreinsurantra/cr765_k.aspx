<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'- Objeto para el manejo del menú
Dim mobjMenu As eFunctions.Menues

'- Objeto para el manejo particular de los datos de la página
Dim mcolClass As Object


</script>
<%Response.Expires = -1

mobjValues = New eFunctions.Values
mobjMenu = New eFunctions.Menues

mobjValues.sCodisplPage = "cr765_k"
%>
<HTML>
<HEAD>
	<META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>


<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tmenu.js"></SCRIPT>		

<SCRIPT LANGUAGE=JavaScript>
// Variable para el control de versiones
document.VssVersion="$$Revision: 1 $|$$Date: 2/09/03 19:02 $|$$Author: Iusr_llanquihue $"


//ChangeSmoking: Cambia el valor del campo Fumadores
//-----------------------------------------------------------------------------------
function ChangeSmoking(Field){
//-----------------------------------------------------------------------------------
	if(Field=="2")
		self.document.forms[0].chksmoking.value='1'
	else
		self.document.forms[0].chksmoking.value='2';
}

// CkeckedPeriodpol : Deshabilita el check para que sólo uno quede habilitado
//-----------------------------------------------------------------------------------
function CkeckedPeriodpol(Field){
//-----------------------------------------------------------------------------------
	 
	 if (Field.name == 'chkperiodpol1')
	 {
	     if  (self.document.forms[0].chkperiodpol1.checked)
			self.document.forms[0].chkperiodpol2.checked = false;
     }
     if (Field.name == 'chkperiodpol2')
     {
         if  (self.document.forms[0].chkperiodpol2.checked)
             self.document.forms[0].chkperiodpol1.checked = false;
    }
}

//% DisabledCoverGen: Habilita y desabilita el de cobertura generica si es Vida
//--------------------------------------------------------------------------------------------
function DisabledCoverGen(Field){
//--------------------------------------------------------------------------------------------
	with (self.document.forms[0])
	{
		alert('Field...' + Field);
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
 		cbenBranch_rei.disabled= false;
		tcnNumber.disabled= false;
		cbeType.disabled= false;
		tcdEffecdate.disabled= false;
		btn_tcdEffecdate.disabled= false;
		tcdEffecdate.disabled= false;
		btn_tcdEffecdate.disabled= false;
		chksmoking.disabled = false;
		cbetyperisk.disabled = false;
		optperiodpol[0].disabled = false;
		optperiodpol[1].disabled = false;
		tcnCap_ini.disabled = false;
		tcnCap_end.disabled = false;
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
</SCRIPT>
	<%
Response.Write(mobjValues.StyleSheet())
Response.Write(mobjMenu.MakeMenu("CR765", "CR765.aspx", 1, vbNullString))
mobjMenu = Nothing
'"DisabledCoverGen(this.value);"
%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<BR><BR>
<FORM METHOD="POST" ID="FORM" NAME="CR765" ACTION="ValCoReinsuranTra.aspx?sMode=1">
    <TABLE WIDTH="100%">
        <TR>
            <TD><LABEL><%= GetLocalResourceObject("cbenBranch_reiCaption") %></LABEL></TD>
            <TD><%=mobjValues.PossiblesValues("cbenBranch_rei", "table5000", eFunctions.Values.eValuesType.clngComboType,  ,  ,  ,  ,  ,  ,  , True,  , GetLocalResourceObject("cbenBranch_reiToolTip"))%></TD>
            <TD><LABEL><%= GetLocalResourceObject("tcnNumberCaption") %></LABEL></TD>
			<TD><%=mobjValues.NumericControl("tcnNumber", 5, vbNullString,  , GetLocalResourceObject("tcnNumberToolTip"),  ,  ,  ,  ,  ,  , True)%></TD>
        </TR>
        <TR>
			<TD><LABEL><%= GetLocalResourceObject("cbeTypeCaption") %></LABEL></TD>
            <TD><%mobjValues.TypeList = 2
mobjValues.List = "1"
Response.Write(mobjValues.PossiblesValues("cbeType", "table173", eFunctions.Values.eValuesType.clngComboType,  ,  ,  ,  ,  ,  ,  , True,  , GetLocalResourceObject("cbeTypeToolTip")))%></TD>
			<TD><LABEL><%= GetLocalResourceObject("tcdEffecdateCaption") %></LABEL></TD>
<TD><% %>
<%=mobjValues.DateControl("tcdEffecdate", CStr(Today),  , GetLocalResourceObject("tcdEffecdateToolTip"),  ,  ,  ,  , True)%></TD>
        </TR>
        <TR>
			<TD><LABEL><%= GetLocalResourceObject("valCovergenCaption") %></LABEL></TD>
            <TD><%=mobjValues.PossiblesValues("valCovergen", "tabtab_lifcov2", eFunctions.Values.eValuesType.clngWindowType,  ,  ,  ,  ,  ,  ,  , True,  , GetLocalResourceObject("valCovergenToolTip"))%> </TD>
			<TD><%=mobjValues.CheckControl("chksmoking", GetLocalResourceObject("chksmokingCaption"), CStr(False), "2", "ChangeSmoking(this.value);", True,  , GetLocalResourceObject("chksmokingToolTip"))%> </TD>
		</TR>
    </TABLE>
    <TABLE WIDTH="100%">
   		<TR>                       
			<TD WIDTH="45%" COLSPAN="1" CLASS="HighLighted"><LABEL><A NAME="Aplicación"></A>Período de la póliza</LABEL></TD>
			<TD WIDTH="10%">&nbsp</TD>
			<TD WIDTH="45%" COLSPAN="1" CLASS="HighLighted"><LABEL><A NAME="Aplicación"></A>Rangos de Capital</LABEL></TD>
		</TR>        
		<TR>
		    <TD COLSPAN="1"><HR></TD>
		    <TD WIDTH="10%">&nbsp</TD>
		    <TD COLSPAN="1"><HR></TD>
		</TR>
		<TR>
            <TD WIDTH="45%"><%=mobjValues.OptionControl(0, "optperiodpol", GetLocalResourceObject("optperiodpol_1Caption"), "1", "1",  , True,  , GetLocalResourceObject("optperiodpol_1ToolTip"))%> </TD>
            <TD WIDTH="10%">&nbsp</TD>
			<TD>
				<TABLE>
					<TD WIDTH="5%"><LABEL><%= GetLocalResourceObject("tcnCap_iniCaption") %></LABEL></TD>
					<TD WIDTH="15%"><%=mobjValues.NumericControl("tcnCap_ini", 18, vbNullString,  , GetLocalResourceObject("tcnCap_iniToolTip"), True, 6,  ,  ,  ,  , True)%></TD>
				</TABLE>
			</TD>
        </TR>
        <TR>
            <TD WIDTH="45%"><%=mobjValues.OptionControl(0, "optperiodpol", GetLocalResourceObject("optperiodpol_2Caption"),  , "2",  , True,  , GetLocalResourceObject("optperiodpol_2ToolTip"))%> </TD>
            <TD WIDTH="10%">&nbsp</TD>
			<TD>
				<TABLE>
					<TD WIDTH="5%"><LABEL><%= GetLocalResourceObject("tcnCap_endCaption") %></LABEL></TD>
					<TD WIDTH="15%"><%=mobjValues.NumericControl("tcnCap_end", 18, vbNullString,  , GetLocalResourceObject("tcnCap_endToolTip"), True, 6,  ,  ,  ,  , True)%></TD>
				</TABLE>
			</TD>
        </TR>
    </TABLE>
<BR>
    <TABLE WIDTH="100%">
		<TR>                       
            <TD WIDTH="20%" ><LABEL><%= GetLocalResourceObject("cbetyperiskCaption") %></LABEL></TD>
            <TD><%=mobjValues.PossiblesValues("cbetyperisk", "Table5639", eFunctions.Values.eValuesType.clngComboType, CStr(1),  ,  ,  ,  ,  ,  , True,  , GetLocalResourceObject("cbetyperiskToolTip"), eFunctions.Values.eTypeCode.eNumeric)%></TD>
				<%mobjValues.BlankPosition = False%>
		</TR>        
	</TABLE>
</FORM> 
</BODY>
</HTML>
<%
mobjValues = Nothing%>




