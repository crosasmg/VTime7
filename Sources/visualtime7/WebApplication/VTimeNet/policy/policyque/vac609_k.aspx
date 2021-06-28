<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">
'^Begin Header Block VisualTimer Utility 1.1 7/4/03 10.27.21
Dim mobjNetFrameWork As eNetFrameWork.Layout
'~End Header Block VisualTimer Utility
'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values
'- Objeto para el manejo del menú
Dim mobjMenu As eFunctions.Menues
'~End Body Block VisualTimer Utility

'- Variables para almacenar parametros
Dim mstrBranch As String
Dim mstrProduct As String
Dim mstrPolicy As String
Dim mstrCertif As String
Dim mstrStartdate As String


</script>
<%Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage("vac609_k")

'- Objeto para el manejo particular de los datos de la página
mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.27.21
mobjValues.sSessionID = Session.SessionID
mobjValues.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjValues.sCodisplPage = "vac609_k"
mobjMenu = New eFunctions.Menues
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.27.21
mobjMenu.sSessionID = Session.SessionID
mobjMenu.nUsercode = Session("nUsercode")

'+ Se cargan datos de parametros
With Request
	mstrBranch = .QueryString.Item("nBranch")
	mstrProduct = .QueryString.Item("nProduct")
	mstrPolicy = .QueryString.Item("nPolicy")
	mstrCertif = .QueryString.Item("nCertif")
	mstrStartdate = .QueryString.Item("dStartdate")
End With
%>
<HTML>
<HEAD>
    <META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>


<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tmenu.js"></SCRIPT>        
<SCRIPT LANGUAGE=JavaScript>
//- Variable para el control de versiones
       document.VssVersion="$$Revision: 1 $|$$Date: 2/09/03 19:03 $"
//% insStateZone: se controla el estado de los campos de la página
//--------------------------------------------------------------------------------------------
function insStateZone(){
//--------------------------------------------------------------------------------------------
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
//% insChangeField: controla cambio de datos en controles
//-------------------------------------------------------------------------------
function insChangeField(vObj){
//-------------------------------------------------------------------------------
//- Variable para parametros
    var lstrParams = new String;
    with(document.forms[0]){
        lstrParams += 'sCertype=2' +
                  '&nBranch=' + cbeBranch.value +
                  '&nProduct=' + valProduct.value +
                  '&nPolicy=' + tcnPolicy.value +
                  '&nCertif=' + tcnCertif.value +
                  '&nRole=2' +
'&dEffecdate=<% %>
<%=mobjValues.TypeToString(Today, eFunctions.Values.eTypeData.etdDate)%>' +
                  '&sExecCertif=1';

		if (cbeBranch.value!='0' && 
		    valProduct.value!='0' && 
		    tcnPolicy.value!='' && 
		    tcnPolicy.value!='0')
			
			if (vObj.name == 'tcnPolicy')
				insDefValues('insValPolitype',lstrParams);
				
	    insDefValues('AccPolDat',lstrParams);				
    }
}
</SCRIPT>
<%
Response.Write(mobjValues.StyleSheet())
If Request.QueryString.Item("Type") <> "PopUp" Then
	Response.Write(mobjMenu.MakeMenu("VAC609", "VAC609_K.aspx", 1, vbNullString))
	mobjMenu = Nothing
	Response.Write("<SCRIPT>var nMainAction=top.frames['fraSequence'].plngMainAction</SCRIPT>")
End If
%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<BR><BR>
<FORM METHOD="POST" NAME="VAC609" ACTION="ValPolicyQue.aspx?sMode=2">
    <TABLE WIDTH="100%">
        <TR>
            <TD WIDTH="12%"><LABEL ID=0><%= GetLocalResourceObject("cbeBranchCaption") %></LABEL></TD>
            <TD WIDTH="45%"><%=mobjValues.BranchControl("cbeBranch", GetLocalResourceObject("cbeBranchToolTip"), mstrBranch)%></TD>
            <TD WIDTH="3%">&nbsp;</TD>
            <TD><LABEL ID=0><%= GetLocalResourceObject("valProductCaption") %></LABEL></TD>
            <TD><%=mobjValues.ProductControl("valProduct", GetLocalResourceObject("valProductToolTip"),  ,  ,  , mstrProduct)%> </TD>
        </TR>
        <TR>
            <TD WIDTH="15%"><LABEL ID=0><%= GetLocalResourceObject("tcnPolicyCaption") %></LABEL></TD>
            <TD WIDTH="15%"><%=mobjValues.NumericControl("tcnPolicy", 9, mstrPolicy,  , GetLocalResourceObject("tcnPolicyToolTip"),  ,  ,  ,  ,  , "insChangeField(this);")%></TD>
            <TD>&nbsp;</TD>
            <TD><LABEL ID=0><%= GetLocalResourceObject("tcnCertifCaption") %></LABEL></TD>
            <TD><%=mobjValues.NumericControl("tcnCertif", 9, mstrCertif,  , GetLocalResourceObject("tcnCertifToolTip"),  ,  ,  ,  ,  , "insChangeField(this);")%></TD> 
        </TR>
        <TR>
            <TD><LABEL ID=0><%= GetLocalResourceObject("tcdMoveDateCaption") %></LABEL></TD>
            <TD><%=mobjValues.DateControl("tcdMoveDate", mstrStartdate,  , GetLocalResourceObject("tcdMoveDateToolTip"))%></TD>
            <TD COLSPAN="3" CLASS="HighLighted"><LABEL ID=0><%= GetLocalResourceObject("AnchorCaption") %></LABEL></TD>
        </TR>
        <TR>
            <TD COLSPAN="7" CLASS="HorLine"></TD>
        </TR>
    </TABLE>
    <TABLE WIDTH="100%">
        <TR>
            <TD WIDTH="15%"><LABEL ID=0><%= GetLocalResourceObject("Anchor2Caption") %></LABEL></TD>
            <TD COLSPAN="2" ALIGN="RIGHT"><%=mobjValues.DIVControl("divCurrency",  , "<Moneda>")%> </TD>
            <TD WIDTH="1%">&nbsp;</TD>
            <TD COLSPAN="2" WIDTH="23%"><LABEL ID=0><%= GetLocalResourceObject("Anchor3Caption") %></LABEL></TD>
            <TD ALIGN="RIGHT"><%=mobjValues.DIVControl("divLastDate",  , "<Ultimo movto>")%> </TD>
        </TR>
        <TR>
            <TD COLSPAN="2" WIDTH="25%"><LABEL ID=0><%= GetLocalResourceObject("Anchor4Caption") %></LABEL></TD>
            <TD WIDTH="15%" ALIGN="RIGHT"><%=mobjValues.DIVControl("divLastPay",  , "<Ultimo Pago>")%> </TD>
            <TD>&nbsp;</TD>
            <TD COLSPAN="2" WIDTH="20%"><LABEL ID=0><%= GetLocalResourceObject("Anchor5Caption") %></LABEL></TD>
            <TD WIDTH="15%" ALIGN="RIGHT"><%=mobjValues.DIVControl("divVP_neg",  , "<VP Neg>")%> </TD>
        </TR>
        <TR>
            <TD COLSPAN="2"><LABEL ID=0><%= GetLocalResourceObject("Anchor6Caption") %></LABEL></TD>
            <TD ALIGN="RIGHT"><%=mobjValues.DIVControl("divPays",  , "<Pagos>")%> </TD>
            <TD>&nbsp;</TD>
            <TD COLSPAN="2"><LABEL ID=0><%= GetLocalResourceObject("Anchor7Caption") %></LABEL></TD>
            <TD ALIGN="RIGHT"><%=mobjValues.DIVControl("divFixCharge",  , "<Cargo fijo>")%> </TD>
        </TR>
        <TR>
            <TD COLSPAN="2"><LABEL ID=0><%= GetLocalResourceObject("Anchor8Caption") %></LABEL></TD>
            <TD ALIGN="RIGHT"><%=mobjValues.DIVControl("divCoverCost",  , "<Costo Cobertura>")%> </TD>
            <TD>&nbsp;</TD>
            <TD COLSPAN="2"><LABEL ID=0><%= GetLocalResourceObject("Anchor9Caption") %></LABEL></TD>
            <TD ALIGN="RIGHT"><%=mobjValues.DIVControl("divNetPays",  , "<Primas netas>")%> </TD>
        </TR>
        <TR>
            <TD COLSPAN="2"><LABEL ID=0><%= GetLocalResourceObject("Anchor10Caption") %></LABEL></TD>
            <TD ALIGN="RIGHT"><%=mobjValues.DIVControl("divProfit",  , "<Intereses>")%> </TD>
            <TD>&nbsp;</TD>
            <TD COLSPAN="2"><LABEL ID=0><%= GetLocalResourceObject("Anchor11Caption") %></LABEL></TD>
            <TD ALIGN="RIGHT"><%=mobjValues.DIVControl("divAmoSurren",  , "<Monto de rescate>")%> </TD>
        </TR>
        <TR>
            <TD COLSPAN="2"><LABEL ID=0><%= GetLocalResourceObject("Anchor12Caption") %></LABEL></TD>
            <TD ALIGN="RIGHT"><%=mobjValues.DIVControl("divValuePol",  , "<V.P.>")%> </TD>
        </TR>
        <TR>
            <TD COLSPAN="2"><LABEL ID=0><%= GetLocalResourceObject("Anchor13Caption") %></LABEL></TD>
            <TD COLSPAN="5"><%=mobjValues.DIVControl("divContracting",  , "<Rut-Cliente>")%> </TD>
        </TR>
    </TABLE>
</FORM>
</BODY>
</HTML>
<%
mobjValues = Nothing
%>
<%'^Begin Footer Block VisualTimer Utility 1.1 7/4/03 10.27.21
Call mobjNetFrameWork.FinishPage("vac609_k")
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>




