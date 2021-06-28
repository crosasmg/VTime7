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

%>
<HTML>
<HEAD>
    <META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tMenu.js"></SCRIPT>


<SCRIPT>
//- Variable para el control de versiones
	document.VssVersion="$$Revision: 2 $|$$Date: 27/11/03 12:08 $|$$Author: Nvaplat15 $"
	
//% insStateZone: habilita los campos de la forma
//-----------------------------------------------------------------------------
function insStateZone(){
//-----------------------------------------------------------------------------
    with (document.forms[0]) {
		cbeInsur_area.disabled=false;
		cbeTableTyp.disabled=false;
		cbeSource.disabled=false;
		cbeClaimClass.disabled=false;
		tcdEffecdate.disabled=false;
		btn_tcdEffecdate.disabled=false;
		insEffecdate();
    }
}

//% insCancel: Ejecuta rutinas necesarias en el momento de cancelar la página
//-----------------------------------------------------------------------------
function insCancel(){
//-----------------------------------------------------------------------------
   return true
}
//% insChangeField: Realiza acciones según modificaciones a los campos 
//-----------------------------------------------------------------------------
function insChangeField(sField){
//-----------------------------------------------------------------------------
	var lstrHREF = self.document.location.href;
    with (document.forms[0]) {
		switch (sField.name){
			case 'cbeInsur_area':
			case 'cbeSource':
			case 'cbeClaimClass':
// se rescata la fecha predeterminada 
				insEffecdate()
				break;
			case 'cbeTableTyp':
// se valida cbeTableTyp 
				if (sField.value==5 || sField.value==2 || 
			       (sField.value!=5 && '<%=Request.QueryString.Item("nTableTyp")%>'=='5') ||
				   (sField.value!=2 && '<%=Request.QueryString.Item("nTableTyp")%>'=='2') ){
				    cbeClaimClass.value = '';
					lstrHREF = lstrHREF.replace(/&nInsur_area.*/,'') + 
					           '&nInsur_area=' + cbeInsur_area.value + 
					           '&nTableTyp=' + cbeTableTyp.value + 
					           '&nSource=' + cbeSource.value + 
					           '&nClaimClass=' + cbeClaimClass.value + 
					           '&dEffecdate=' + tcdEffecdate.value + 
					           '&sReload=1' ;
					document.location.href = lstrHREF;
				} 
				else{
					insEffecdate()
				}
				break;
		} 
	} 
} 

//% insChangeField: Realiza acciones según modificaciones a los campos 
//-----------------------------------------------------------------------------
function insEffecdate(){
//-----------------------------------------------------------------------------
	var strParams; 
    with (document.forms[0]) {
		strParams = "nInsur_area=" + cbeInsur_area.value + 
					"&nTableTyp=" + cbeTableTyp.value + 
					"&nSource=" + cbeSource.value + 
					"&nClaimClass=" + cbeClaimClass.value 
		insDefValues("dEffecdate",strParams,'/VTimeNet/maintenance/mantmargin/');
	} 
}
</SCRIPT>
<%
Response.Write(mobjValues.StyleSheet())
mobjMenu = New eFunctions.Menues
Response.Write(mobjMenu.MakeMenu("MMGS002", "MMGS002_K.aspx", 1, vbNullString))
mobjMenu = Nothing
%>    
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" ID="FORM" NAME="MMGS002" ACTION="valMantMargin.aspx?sMode=1">
    <BR><BR>
    <TABLE>
        <TR> 
            <%
With mobjValues
	.BlankPosition = False
	Response.Write(mobjValues.HiddenControl("cbeInsur_area", session("nInsur_area")))
End With
%> 
            <TD><LABEL ID=0><%= GetLocalResourceObject("cbeTableTypCaption") %></LABEL></TD>
			<TD><%With mobjValues
	.BlankPosition = False
	Response.Write(.PossiblesValues("cbeTableTyp", "table5607", eFunctions.Values.eValuesType.clngComboType, Request.QueryString.Item("nTableTyp"),  ,  ,  ,  ,  , "insChangeField(this);", IsNothing(Request.QueryString.Item("sreload")),  , GetLocalResourceObject("cbeTableTypToolTip")))
End With%> </TD>
			<%If Request.QueryString.Item("nTableTyp") = "2" Then%>
				<TD><LABEL ID=0><%= GetLocalResourceObject("cbeClaimClassCaption") %></LABEL></TD>
				<TD><%	With mobjValues
		.BlankPosition = False
		Response.Write(.PossiblesValues("cbeClaimClass", "table5609", eFunctions.Values.eValuesType.clngComboType, Request.QueryString.Item("nClaimClass"),  ,  ,  ,  ,  , "insChangeField(this);", IsNothing(Request.QueryString.Item("sreload")),  , GetLocalResourceObject("cbeClaimClassToolTip")))
	End With%> </TD>
			<%Else%>
				<TD><%=mobjValues.HiddenControl("cbeClaimClass", Request.QueryString.Item("nClaimClass"))%> </TD>
			<%End If%>		            
        </TR> 
        <TR> 
            <TD><LABEL ID=0><%= GetLocalResourceObject("cbeSourceCaption") %></LABEL></TD>
			<TD><%With mobjValues
	If Request.QueryString.Item("nTableTyp") = "5" Then
		.List = "4,5,6" '"Corto plazo/Largo plazo/Indirecto" 
		.TypeList = 1 'Incluir 
	Else
		.List = "4,5,6" '"Corto plazo/Largo plazo/Indirecto" 
		.TypeList = 2 'Excluir 
	End If
	.BlankPosition = False
	Response.Write(.PossiblesValues("cbeSource", "table5608", eFunctions.Values.eValuesType.clngComboType, vbNullString,  ,  ,  ,  ,  , "insChangeField(this);", IsNothing(Request.QueryString.Item("sreload")),  , GetLocalResourceObject("cbeSourceToolTip")))
End With%></TD>
        </TR> 
        <TR> 
            <TD><LABEL ID=0><%= GetLocalResourceObject("tcdEffecdateCaption") %></LABEL></TD>
            <TD><%=mobjValues.DateControl("tcdEffecdate", Request.QueryString.Item("tcdEffecdate"),  , GetLocalResourceObject("tcdEffecdateToolTip"),  ,  ,  ,  , IsNothing(Request.QueryString.Item("sreload")))%></TD>
        </TR>
    </TABLE>
</FORM>
</BODY>
</HTML>
<%
'	If Request.QueryString("sreload") = "1" Then 
Response.Write("<SCRIPT>insEffecdate();</script>")
'	End If  
mobjValues = Nothing
%>




