<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'- Objeto para el manejo de las zonas de la página    
Dim mobjMenu As eFunctions.Menues
Dim mstrMarca As String


</script>
<%Response.Expires = -1

mobjValues = New eFunctions.Values
mobjValues.sCodisplPage = "MAU571"

%>



<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tMenu.js"></SCRIPT>
<SCRIPT>
//- Variable para el control de versiones
document.VssVersion="$$Revision: 2 $|$$Date: 15/10/03 15:45 $|$$Author: Nvaplat61 $"

//% ChangeControl: Habilita/Deshabilita los controles dependientes de la página
//-------------------------------------------------------------------------------------------
function ChangeControl(){
//-------------------------------------------------------------------------------------------
	UpdateDiv("valProductDesc","");
	with(self.document.forms[0]){
		valProduct.value="";
		if(cbeBranch.value=="0"){
			valProduct.disabled=true;
			self.document.btnvalProduct.disabled=true;
		}
		else{
			valProduct.disabled=false;
			document.btnvalProduct.disabled=false;
			valProduct.Parameters.Param1.sValue=cbeBranch.value;
		}
	}
}

//% insStateZone: habilita los campos de la forma
//-----------------------------------------------------------------------------
function insStateZone(){
//-----------------------------------------------------------------------------
    with (document.forms[0]) {
        //cbeBranch.disabled=false;
        valProduct.disabled=false;
        tcdEffecdate.disabled=false;
        valCurrency.disabled=false;
        optTyp_var.disabled=false;
        valVehcode.disabled=false;
		btn_tcdEffecdate.disabled=false;        
    }
}

//% insCancel: Ejecuta rutinas necesarias en el momento de cancelar la página
//-----------------------------------------------------------------------------
function insCancel(){
//-----------------------------------------------------------------------------
   return true
}

//% insChangeField: Se recargan los valores cuando cambia el campo
//-----------------------------------------------------------------------------
function insChangeField(Field){
//-----------------------------------------------------------------------------    
	with (self.document.forms[0]){
		switch(Field.name){
            case "valVehcode":
                self.document.forms[0].cbeVehbrand.value = valVehcode_nVehbrand.value
                self.document.forms[0].tctVehmodel.value = valVehcode_sVehmodel.value
                break;
		}
	}
}
</SCRIPT>
<HTML>
<HEAD>
<META NAME="GENERATOR" CONTENT= "Microsoft Visual Studio 6.0">
<%
Response.Write(mobjValues.StyleSheet())
mobjMenu = New eFunctions.Menues
Response.Write(mobjMenu.MakeMenu("MAU571", "MAU571_k.aspx", 1, vbNullString))
mobjMenu = Nothing
%>    
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" ID="FORM" NAME="MAU571" ACTION="valMantAuto.aspx?sMode=1">
    <BR><BR>
    <TABLE WIDTH="100%">
        <TR>
            <TD><LABEL ID=0><%= GetLocalResourceObject("cbeBranchCaption") %></LABEL></TD>
            <TD><%=mobjValues.PossiblesValues("cbeBranch", "Table10", eFunctions.Values.eValuesType.clngComboType, 6, , , , , , "ChangeControl()", True, , GetLocalResourceObject("cbeBranchToolTip"))%> </TD>
            <TD></TD>
			<TD><LABEL ID=0><%= GetLocalResourceObject("valProductCaption") %></LABEL></TD>
			<TD><%
With mobjValues
			            .Parameters.Add("nBranch", 6, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			            Response.Write(mobjValues.PossiblesValues("valProduct", "tabProdmaster1", eFunctions.Values.eValuesType.clngWindowType, Session("nProduct"), True, , , , , , False, 4, GetLocalResourceObject("valProductToolTip")))
End With
%>
			</TD>
        </TR>

        <TR>
            <TD><LABEL ID=0><%= GetLocalResourceObject("valVehcodeCaption") %></LABEL></TD>
            <TD>            
		        <%
With mobjValues.Parameters
	.Add("sStatregt", "1", eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	.ReturnValue("nVehbrand",  , "Marca", True)
	.ReturnValue("sVehmodel",  , "Modelo", True)
	Response.Write(mobjValues.PossiblesValues("valVehcode", "TabTab_au_veh", eFunctions.Values.eValuesType.clngWindowType, Request.QueryString.Item("sVehcode"), True,  ,  ,  ,  , "insChangeField(this);", False, 6, GetLocalResourceObject("valVehcodeToolTip")))
End With
%>
            </TD>               
            <TD></TD>
            <TD><LABEL ID=0><%= GetLocalResourceObject("cbeVehbrandCaption") %></LABEL></TD>
            <TD><%=mobjValues.PossiblesValues("cbeVehbrand", "Table7042", eFunctions.Values.eValuesType.clngComboType, mobjValues.StringToType(mstrMarca, eFunctions.Values.eTypeData.etdDouble), False,  ,  ,  ,  ,  , True,  , GetLocalResourceObject("cbeVehbrandToolTip"))%></TD>            
        </TR>
		<TR>        
            <TD><LABEL ID=0><%= GetLocalResourceObject("tctVehmodelCaption") %></LABEL></TD>
            <TD><%=mobjValues.TextControl("tctVehmodel", 10, Session("sVehmodel"),  , GetLocalResourceObject("tctVehmodelToolTip"),  ,  ,  ,  , True)%></TD>
            <TD></TD>
            <TD><LABEL ID=0><%= GetLocalResourceObject("tcdEffecdateCaption") %></LABEL></TD>
            <TD><%=mobjValues.DateControl("tcdEffecdate", Session("dEffecdate"),  , GetLocalResourceObject("tcdEffecdateToolTip"),  ,  ,  ,  , True)%></TD>
        <TR>        
            <TD><LABEL ID=0><%= GetLocalResourceObject("valCurrencyCaption") %></LABEL></TD>            
            <TD><%=mobjValues.PossiblesValues("valCurrency", "Table11", eFunctions.Values.eValuesType.clngComboType, CStr(4),  ,  ,  ,  ,  ,  , False, 2, GetLocalResourceObject("valCurrencyToolTip"))%></TD>
        </TR>
        <TR>
            <TD COLSPAN="5" CLASS="HighLighted"><LABEL ID=0><%= GetLocalResourceObject("AnchorCaption") %></LABEL></TD>
        </TR>
		<TR>
			<TD COLSPAN="5" CLASS="HorLine"></TD>
		</TR>
        <TR>
			<TD><%=mobjValues.OptionControl(0, "optTyp_var", GetLocalResourceObject("optTyp_var_1Caption"), "1", "1", , True, , GetLocalResourceObject("optTyp_var_1ToolTip"))%></TD>
			<TD><%=mobjValues.OptionControl(0, "optTyp_var", GetLocalResourceObject("optTyp_var_2Caption"), "2", "2", , True, , GetLocalResourceObject("optTyp_var_2ToolTip"))%></TD>               
            <TD></TD>
       	    <TD><LABEL ID=0><%= GetLocalResourceObject("tctRateAddSubCaption") %></LABEL></TD>
	        <TD><%=mobjValues.NumericControl("tctRateAddSub", 5, CStr(0),  , GetLocalResourceObject("tctRateAddSubToolTip"), True, 2,  ,  ,  ,  , True)%></TD>
        </TR>
    </TABLE>
</FORM>
</BODY>
</HTML>
<%
mobjValues = Nothing
%>






