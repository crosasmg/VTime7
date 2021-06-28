<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'- Objeto para el manejo de las zonas de la página    
Dim mobjMenu As eFunctions.Menues


</script>
<%Response.Expires = -1

mobjValues = New eFunctions.Values
mobjValues.sCodisplPage = "MAG582"
%>


<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tMenu.js"></SCRIPT>
<SCRIPT> 
//+ Variable para el control de versiones
    document.VssVersion="$$Revision: 1 $|$$Date: 2/09/03 19:02 $"

// EnabledFields: Habilita el campo "Producto" dependiendo del "Ramo"
//--------------------------------------------------------------------------------
function EnabledFields(Field){
//--------------------------------------------------------------------------------		
	if(Field.name=='cboBranch')
	{
		with (self.document.forms[0]){
			if(Field.value!=0)
			{
				valProduct.value='';
				valProduct.disabled=false;
			    UpdateDiv('valProductDesc','');
			    btnvalProduct.disabled=false;
			}
			else
			{
				valProduct.value='';
				valProduct.disabled=true;
			    btnvalProduct.disabled=true;
			    UpdateDiv('valProductDesc','');
			}
				
			if(typeof(valProduct)!='undefined')
			    valProduct.Parameters.Param1.sValue=Field.value;
        }
	}
}

//-----------------------------------------------------------------------------
function insStateZone(){
//-----------------------------------------------------------------------------
    with (document.forms[0]){
         cboIntertyp.disabled=false		 
		 cboBranch.disabled=false 
	}
}
//-----------------------------------------------------------------------------
function insPreZone(llngAction){
//-----------------------------------------------------------------------------
}
//-----------------------------------------------------------------------------
function insCancel(){
//-----------------------------------------------------------------------------
   return true
}
</SCRIPT>
<HTML>
<HEAD>
    <META NAME="GENERATOR" Content="eTransaction Designer for Visual TIME">
<%
Response.Write(mobjValues.StyleSheet())
mobjMenu = New eFunctions.Menues
Response.Write(mobjMenu.MakeMenu("MAG582", "MAG582_k.aspx", 1, ""))
mobjMenu = Nothing
%>    
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" ID="FORM" NAME="MAG582" ACTION="valMantAgent.aspx?sMode=1">
	<BR><BR>
    <TABLE WIDTH="100%">
		<TR>
			<TD WIDTH="17%"><LABEL ID=0><%= GetLocalResourceObject("cboIntertypCaption") %></LABEL></TD>
			<TD WIDTH="25%"><%With mobjValues
	.List = "4,9,11" '"Agente de mantención/Asistente de seguros/Supervisor de mantención"
	.TypeList = 1 'Incluir
	.BlankPosition = True
	Response.Write(.PossiblesValues("cboIntertyp", "Interm_typ", 1, "", False,  ,  ,  ,  ,  , True, 2, GetLocalResourceObject("cboIntertypToolTip"), 1))
End With
%>
			</TD>
			<TD WIDTH="5%"><LABEL ID=0><%= GetLocalResourceObject("cboBranchCaption") %></LABEL></TD>
			<TD WIDTH="22%"><%=mobjValues.PossiblesValues("cboBranch", "Table10", 1, "",  ,  ,  ,  ,  , "EnabledFields(this)", True, 2, GetLocalResourceObject("cboBranchToolTip"), 1)%></TD>
			<TD WIDTH="5%"><LABEL ID=0><%= GetLocalResourceObject("valProductCaption") %></LABEL></TD>
			<TD WIDTH="28%"><%mobjValues.Parameters.Add("nBranch", "", eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)%>
							<%=mobjValues.PossiblesValues("valProduct", "tabProdMaster1", 2, "", True,  ,  ,  ,  ,  , True, 5, GetLocalResourceObject("valProductToolTip"), 1)%></TD>
		</TR>
    </TABLE>
</FORM>
</BODY>
</HTML>
<%
mobjValues = Nothing%>






