<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">
'- Objeto para el manejo de las funciones generales de carga de valores

Dim mobjValues As eFunctions.Values
Dim mobjMenu As eFunctions.Menues


</script>
<%Response.Expires = 0

mobjValues = New eFunctions.Values
mobjMenu = New eFunctions.Menues
%>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tmenu.js"></script>
<HTML>
<HEAD>
    <META NAME = "GENERATOR" Content = "Microsoft Visual Studio 6.0">




    <%With Response
	.Write(mobjValues.StyleSheet())
	.Write(mobjMenu.MakeMenu("COC006", "COC006_k.aspx", 1, ""))
End With
mobjMenu = Nothing%>
<SCRIPT>
//+ Variable para el control de versiones
     document.VssVersion="$$Revision: 2 $|$$Date: 15/10/03 16.13 $|$$Author: Nvaplat60 $"
</SCRIPT>      
<SCRIPT>
//------------------------------------------------------------------------------------------
function insCancel(){
//------------------------------------------------------------------------------------------
	return true;
}
//------------------------------------------------------------------------------------------
function  insStateZone(){
//------------------------------------------------------------------------------------------
    var lintIndex = 0;
    with(self.document.forms[0]){
        for (lintIndex=0;lintIndex<document.forms[0].length;lintIndex++)
			elements[lintIndex].disabled=false
		
		valSupCode.disabled=true;
		btnvalSupCode.disabled=true;
		cbeCardType.disabled=true;
		tcnDays.disabled=true;
		btnvalAgentCode.disabled=false,
		btn_tcdDate.disabled=false;
    }
}
//% insLockedControls: Se habilitan/deshabilitan los campos dependientes(Tipo de Tarjeta,Organizador)
//------------------------------------------------------------------------------------------
function insLockedControls(Field,sField){
//------------------------------------------------------------------------------------------
    with(self.document.forms[0].elements){
        switch(sField){
            case 'ReceiptListTyp':
                Field.value==6?cbeCardType.disabled=false:cbeCardType.disabled=true
                Field.value==1?tcnDays.disabled=false:tcnDays.disabled=true
                break;
            case 'Org':
                valSupCode.disabled=false
                btnvalSupCode.disabled=false
                valAgentCode.disabled=true
                btnvalAgentCode.disabled=true
                break;
            case 'Interm':
                valSupCode.disabled=true
                btnvalSupCode.disabled=true
                valAgentCode.disabled=false
                btnvalAgentCode.disabled=false
                break;
       }
    }
}
//%insChecked: Permite verificar el tipo de origen del recibo seleccionado
//------------------------------------------------------------------------------------------
function insChecked(Field){
//------------------------------------------------------------------------------------------
    with(document.forms[0].elements){
		switch(Field){
			case chkAll:chkRenew.checked=false
                        chkUnderw.checked=false
                        break
            default:chkAll.checked=false
        }    
    }
}

//%insBlankDescript:
//------------------------------------------------------------------------------------------
function  insBlankDescript(){
//------------------------------------------------------------------------------------------
    with(document.forms[0].elements){
        cbeReceiptListTyp.value=0
        cbeCurrency.value=0
        cbeCardType.value=0
    }    
}
</SCRIPT>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="post" ID="FORM" NAME="frmReceiptIntermed" ACTION="valCollectionQue.aspx?mode=1">
<BR></BR>
    <TABLE WIDTH="100%">
		<TR>
            <TD COLSPAN="2" CLASS="HighLighted"><LABEL ID=40438><A NAME="Origen del recibo"><%= GetLocalResourceObject("AnchorOrigen del reciboCaption") %></A></LABEL></TD>
        </TR>
        <TR>
			<TD COLSPAN="3"><HR></TD>
        </TR>
		<TR>
            <TD><%=mobjValues.CheckControl("chkUnderw", GetLocalResourceObject("chkUnderwCaption"),  ,  , "insChecked(this)", True)%></TD>
            <TD>&nbsp;</TD><TD>&nbsp;</TD>
            <TD><LABEL ID=10034><%= GetLocalResourceObject("cbeReceiptListTypCaption") %></LABEL></TD>
            <%mobjValues.BlankDescript = "Todos"%>
            <TD><%=mobjValues.PossiblesValues("cbeReceiptListTyp", "Table7051", 1,  ,  ,  ,  ,  ,  , "insLockedControls(this,""ReceiptListTyp"")", True)%></TD>
        </TR>
        <TR>
			<TD><%=mobjValues.CheckControl("chkRenew", GetLocalResourceObject("chkRenewCaption"),  ,  , "insChecked(this)", True)%></TD>
			<TD><%=mobjValues.CheckControl("chkAll", GetLocalResourceObject("chkAllCaption"), CStr(1), CStr(1), "insChecked(this)", True)%></TD>
			<TD>&nbsp;</TD>
			<TD><LABEL ID=10031><%= GetLocalResourceObject("cbeCurrencyCaption") %></LABEL></TD>
			<TD><%=mobjValues.PossiblesValues("cbeCurrency", "Table11", 1,  ,  ,  ,  ,  ,  ,  , True,  , "")%></TD>
		</TR>
		<TR>
			<TD>&nbsp;</TD><TD>&nbsp;</TD><TD>&nbsp;</TD>
			<TD><LABEL ID=10030><%= GetLocalResourceObject("cbeCardTypeCaption") %></LABEL></TD>
            <TD><%=mobjValues.PossiblesValues("cbeCardType", "Table183", 1,  ,  ,  ,  ,  ,  ,  , True,  , "")%></TD>
        </TR>
        <TR>
			<TD><LABEL ID=10032><%= GetLocalResourceObject("tcdDateCaption") %></LABEL></TD>
            <TD><%=mobjValues.DateControl("tcdDate",  ,  , "",  ,  ,  ,  , True)%></TD>
            <TD>&nbsp;</TD>
			<TD><LABEL ID=10033><%= GetLocalResourceObject("tcnDaysCaption") %></LABEL></TD>
            <TD><%=mobjValues.NumericControl("tcnDays", 4, CStr(0),  , "",  ,  ,  ,  ,  ,  , True)%></TD>
        </TR>
    </TABLE>
    <TABLE WIDTH="100%">
        <TR>
            <TD COLSPAN="4" CLASS="HighLighted"><LABEL ID=40439><A NAME="Búsqueda por"><%= GetLocalResourceObject("AnchorBúsqueda porCaption") %></A></LABEL></TD>
        </TR>
        <TR>
            <TD WIDTH="100%" COLSPAN="4"><HR></TD>
        </TR>
        <TR>
            <TD><%=mobjValues.OptionControl(40440, "optClient", GetLocalResourceObject("optClient_CStr1Caption"), CStr(1), CStr(1), "insLockedControls(this,""Interm"")", True)%></TD>
			<%mobjValues.Parameters.Add("nIntertyp", eCollection.Dir_debit.Interm_typ.clngProducer, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)%>
            <TD><%=mobjValues.PossiblesValues("valAgentCode", "TabIntermedia1", eFunctions.Values.eValuesType.clngWindowType, CStr(eRemoteDB.Constants.strnull), True,  ,  ,  ,  ,  , True,  , "")%></TD>
			<TD><%=mobjValues.OptionControl(40441, "optClient", GetLocalResourceObject("optClient_CStr2Caption"),  , CStr(2), "insLockedControls(this,""Org"")", True)%></TD>
			<%mobjValues.Parameters.Add("nIntertyp", eCollection.Dir_debit.Interm_typ.clngOrganizer, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)%>
            <TD><%=mobjValues.PossiblesValues("valSupCode", "TabIntermedia1", eFunctions.Values.eValuesType.clngWindowType, CStr(eRemoteDB.Constants.strnull), True,  ,  ,  ,  ,  , True,  , "")%></TD>
        </TR>
        
	</TABLE>
</FORM>
</BODY>
</HTML>
<%
mobjValues = Nothing%>




