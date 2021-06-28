<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">
'^Begin Header Block VisualTimer Utility 1.1 7/4/03 10.44.07
Dim mobjNetFrameWork As eNetFrameWork.Layout
'~End Header Block VisualTimer Utility

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'- Objeto para el manejo del menú
Dim mobjMenu As eFunctions.Menues

'- Objeto para el manejo particular de los datos de la página
Dim mblnDisabledBranch As Boolean
Dim mblnDisabledProduct As Boolean
Dim mblnDisabledReceipt As Boolean

    

</script>
<%Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage("coc009_k")
mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.44.07
mobjValues.sSessionID = Session.SessionID
mobjValues.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjValues.sCodisplPage = "coc009_k"
mobjMenu = New eFunctions.Menues
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.44.07
mobjMenu.sSessionID = Session.SessionID
mobjMenu.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjValues.ActionQuery = CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 401
mblnDisabledBranch = False
mblnDisabledProduct = False
mblnDisabledReceipt = False
%>
<HTML>
<HEAD>
	<META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>

<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tmenu.js"></SCRIPT>		
    <SCRIPT>
//+ Variable para el control de versiones
	     document.VssVersion="$$Revision: 1 $|$$Date: 2/09/03 19:01 $|$$Author: Iusr_llanquihue $"
    </SCRIPT>
<SCRIPT LANGUAGE=JavaScript>

//% insStateZone: se controla el estado de los campos de la página
//--------------------------------------------------------------------------------------------
function insStateZone(){
//-------------------------------------------------------------------------------------------    
   with(self.document.forms[0]){
		cbeBranch.disabled=true;
		btnvalProduct.disabled=true;
		valProduct.disabled=true;
		tcnReceipt.disabled=false;
		cbeBranch.value='';
		valProduct.value='';
		UpdateDiv('valProductDesc','');
		tcnReceipt.value='';
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

//% ShowPages: Llama a la ventana de Datos de verificación del recibo (SCO001) - ACM - 26/06/2001
//-------------------------------------------------------------------------------------------
function ShowPage(){
//-------------------------------------------------------------------------------------------
//- Variable lstrLocation: Se usa para armar el QueryString que va a recibir la ventana
//- SCO001 para poder realizar la búsqueda de los datos de verificación del recibo - ACM - 26/06/2001
	var lstrLocation="";
	
	lstrLocation = lstrLocation + "&nReceipt=" + self.document.forms[0].elements["tcnReceipt"].value;
	lstrLocation = lstrLocation + "&sCertype=2";
	lstrLocation = lstrLocation + "&nDigit=0";
	lstrLocation = lstrLocation + "&nPayNumber=0";
	lstrLocation = lstrLocation + "&nGeneralNumerator=<%=Session("sReceiptnum")%>";
	lstrLocation = lstrLocation + "&nBranch=" + self.document.forms[0].elements["cbeBranch"].value;
	lstrLocation = lstrLocation + "&nProduct=" + self.document.forms[0].elements["valProduct"].value;
//+ Se hace el llamado a la ventana SCO001
	ShowPopUp("/VTimeNet/Common/SCO001.aspx?sCodispl=SCO001"+lstrLocation,"",700,400,true,false,20,20)
}  

//%	ShowDefValues: Condiciona el recargo por el cambio en el patrón de busqueda
//-------------------------------------------------------------------------------------------
function ShowDefValues(Field){
//-------------------------------------------------------------------------------------------
    with (document.forms[0]){           
        if (Field.value != 0 && Field.value != ""){
				insDefValues("Receipt_COC009_k", "nReceipt=" + tcnReceipt.value + "&nBranch=" + cbeBranch.value + "&nProduct=" + valProduct.value + "&sCertype=" + tctCertype.value)
		}   
	}
} 
</SCRIPT>
	<%
Response.Write(mobjValues.StyleSheet())
If Request.QueryString.Item("Type") <> "PopUp" Then
	Response.Write(mobjMenu.MakeMenu("COC009", "COC009_k.aspx", 1, vbNullString))
	mobjMenu = Nothing
	Response.Write("<SCRIPT>var nMainAction=top.frames['fraSequence'].plngMainAction</SCRIPT>")
End If
%>
    
   
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<BR><BR>
<FORM METHOD="post" NAME="COC009" ACTION="ValCollectionQue.aspx?sMode=2">	
<%Response.Write(mobjValues.ShowWindowsName("COC009", Request.QueryString.Item("sWindowDescript")))%>
<BR>
    <%
'+ Se inhabilitan los campos dependiendo de la opción de instalación del sistema    
If CStr(Session("sReceiptnum")) = "1" Then
	mblnDisabledBranch = True
	mblnDisabledProduct = True
ElseIf CStr(Session("sReceiptnum")) = "2" Then 
	mblnDisabledBranch = True
	mblnDisabledReceipt = True
End If
%>
    <TABLE WIDTH="100%">
         <%=mobjValues.HiddenControl("tctCertype", "2")%>
        <TR>
            <TD WIDTH="10%"> <LABEL ID=41200><%= GetLocalResourceObject("cbeBranchCaption") %></LABEL> </TD>
			<TD WIDTH="25%"><%=mobjValues.BranchControl("cbeBranch", GetLocalResourceObject("cbeBranchToolTip"),  ,  ,  ,  ,  ,  , mblnDisabledBranch)%> </TD>
            <TD WIDTH="10%"><LABEL ID=40010><%= GetLocalResourceObject("valProductCaption") %></LABEL> </TD>
			<TD WIDTH="25%"><%=mobjValues.ProductControl("valProduct", GetLocalResourceObject("valProductToolTip"),  ,  , mblnDisabledProduct)%></TD>
			<TD WIDTH="10%"><LABEL ID=40020><%= GetLocalResourceObject("tcnReceiptCaption") %></LABEL> </TD>
		    <TD><%=mobjValues.NumericControl("tcnReceipt", 10, CStr(eRemoteDB.Constants.intNull),  , GetLocalResourceObject("tcnReceiptToolTip"),  ,  ,  ,  ,  , "ShowDefValues(this)", True)%> 
		        <%=mobjValues.AnimatedButtonControl("bQuery", "/VTimeNet/Images/btn_ValuesOff.png", GetLocalResourceObject("bQueryToolTip"),  , "ShowPage();", True)%></TD>
        </TR>
     </TABLE>   
     <TABLE WIDTH="100%">
        <TR>
            <TD COLSPAN = "5" CLASS="HighLighted"><LABEL ID=40438 ALIGN=RIGHT><A NAME="Datos del recibo"><%= GetLocalResourceObject("AnchorDatos del reciboCaption") %></A></LABEL></TD>           
        </TR> 
        <TR>
			<TD COLSPAN="5" CLASS="HorLine"></TD>
	    </TR>
        <TR>
			<TD> <LABEL ID=41205><%= GetLocalResourceObject("cbeAgencyCaption") %></LABEL> </TD>
			<TD><%=mobjValues.PossiblesValues("cbeAgency", "table5555", eFunctions.Values.eValuesType.clngComboType, "",  ,  ,  ,  ,  ,  , True,  , GetLocalResourceObject("cbeAgencyToolTip"))%></TD>
			<TD><LABEL ID=40015><%= GetLocalResourceObject("tcnPolicyCaption") %></LABEL> </TD>
			<TD><%=mobjValues.NumericControl("tcnPolicy", 10, "",  , GetLocalResourceObject("tcnPolicyToolTip"),  ,  ,  ,  ,  ,  , True)%></TD>
			<TD></TD>
        </TR>        
		<TR>
			<TD><LABEL ID=40025><%= GetLocalResourceObject("dtcClientCaption") %></LABEL> </TD>
		    <TD><%=mobjValues.ClientControl("dtcClient", "",  , GetLocalResourceObject("dtcClientToolTip"),  , True, "lblCliename")%></TD>
			<TD> <LABEL ID=41205><%= GetLocalResourceObject("cbeIntermedCaption") %></LABEL> </TD>
			<TD><%=mobjValues.PossiblesValues("cbeIntermed", "TabIntermedia", eFunctions.Values.eValuesType.clngWindowType, "",  ,  ,  ,  ,  ,  , True,  10, GetLocalResourceObject("cbeIntermedToolTip"))%></TD>
			<TD></TD>
		</TR>
		<TR>
		    <TD> <LABEL ID=41205><%= GetLocalResourceObject("cbeInspectoCaption") %></LABEL> </TD>
			<TD><%
			        With mobjValues.Parameters
			            .Add("nIntertyp", 5, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			        End With
                    Response.Write(mobjValues.PossiblesValues("cbeInspecto", "tabintermedia1", eFunctions.Values.eValuesType.clngWindowType, CStr(eRemoteDB.Constants.intNull), True,  ,  ,  ,  ,  , True,  10, GetLocalResourceObject("cbeInspectoToolTip")))%>
			</TD>
		    <TD><LABEL ID=41205><%= GetLocalResourceObject("tcnPremiumCaption") %></LABEL></TD>
			<TD><%=mobjValues.NumericControl("tcnPremium", 18, CStr(eRemoteDB.Constants.intNull),  , GetLocalResourceObject("tcnPremiumToolTip"), True, 6,  ,  ,  ,  , True)%>
				<%=mobjValues.DIVControl("divCurrency", True)%>
			</TD>			
		</TR>
        <TR>
            <TD><LABEL ID=41205><%= GetLocalResourceObject("cbeStatus_preCaption") %></LABEL></TD>
			<TD><%=mobjValues.PossiblesValues("cbeStatus_pre", "table19", eFunctions.Values.eValuesType.clngWindowType, "",  ,  ,  ,  ,  ,  , True,  , GetLocalResourceObject("cbeStatus_preToolTip"))%></TD>
			<TD><LABEL ID=41205><%= GetLocalResourceObject("tcnContratCaption") %></LABEL></TD>
			<TD><%=mobjValues.NumericControl("tcnContrat", 10, CStr(eRemoteDB.Constants.intNull),  , GetLocalResourceObject("tcnContratToolTip"),  ,  ,  ,  ,  ,  , True)%></TD>
			<TD></TD>
        </TR>
    </TABLE>
</FORM> 
</BODY>
</HTML>
<%'^Begin Footer Block VisualTimer Utility 1.1 7/4/03 10.44.07
Call mobjNetFrameWork.FinishPage("COC009_K")
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>




