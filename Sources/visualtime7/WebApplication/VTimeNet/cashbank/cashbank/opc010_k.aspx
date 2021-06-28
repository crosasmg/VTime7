<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values
Dim mobjMenu As eFunctions.Menues

Dim mdtmEffecdate As Object


</script>
<%Response.Expires = -1

mobjValues = New eFunctions.Values
mobjMenu = New eFunctions.Menues

mobjValues.sCodisplPage = "opc010_k"
%>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tmenu.js"></script>
<HTML>
<HEAD>
    <META NAME = "GENERATOR" CONTENT = "Microsoft Visual Studio 6.0">


    <%With Response
	.Write(mobjValues.StyleSheet())
	.Write(mobjMenu.MakeMenu("OPC010", "OPC010_k.aspx", 1, ""))
End With
mobjMenu = Nothing%>
<SCRIPT>

 //+Variable para el control de versiones
    document.VssVersion="$$Revision: 2 $|$$Date: 26/05/04 10:24 $|$$Author: Nvaplat7 $"

//%insCancel: Control la acción Cancelar de la página
//------------------------------------------------------------------------------------------
function insCancel(){
//------------------------------------------------------------------------------------------
	return true;
}

//%insStateZone: Habilita/deshabilita los campos de la ventana.
//------------------------------------------------------------------------------------------
function  insStateZone(){
//------------------------------------------------------------------------------------------
    with (document.forms[0]){
        if (top.fraSequence.plngMainAction == 401)
	        tcdEffecdate.disabled=false
	        cbeTypeAccount.disabled=false
	        cbeCurrency.disabled=false
	        btn_tcdEffecdate.disabled=false
	        document.images["btncbeCurrency"].disabled = false
    }
}

//%insEnableField: Procedimiento que habilita o deshabilita los campos de clientes o póliza
//                 según la selección del usuario
//------------------------------------------------------------------------------------------
function  insEnableField(Field){
//------------------------------------------------------------------------------------------
    var value
    with (document.forms[0]){
        if (Field.value==9){
            value=false
            tctTypeCurracc.value=1
            dtcClient.disabled=true
		    btndtcClient.disabled=true
        }
        else{
            value=true
            tctTypeCurracc.value=2
            dtcClient.disabled=false
		    btndtcClient.disabled=false
		}
		cbeBranch.disabled=value
		tcnPolicy.disabled=value
		tcnCertif.disabled=value
    }
}
//%insChangLocked: Procedimiento que habilita o deshabilita el campo tipo de negocio
//                 según el tipo de cuenta seleccionada
//------------------------------------------------------------------------------------------
function  insChangLocked(Field){
//------------------------------------------------------------------------------------------
    insEnableField(Field)
    with (document.forms[0]){
        if (Field.value==2 || Field.value==3 || Field.value==8)
            cbeBussType.disabled=false
        else{
		    cbeBussType.disabled=true
            cbeBussType.value=0
//			cbeBussType.options[3].selected=true            
        }
    }
}
//%insValidEmpty: Permite validar que el contenido del campo sea diferente a "Vacio"
//------------------------------------------------------------------------------------------
function  insValidEmpty(Field){
//------------------------------------------------------------------------------------------
    if (Field.value==0 || Field.value=='')
	    Field.value=0
	
	if(document.forms[0].cbeBranch.value!=0 && document.forms[0].valProduct.value!='')
        insDefValues('PoliType','nBranch=' + document.forms[0].cbeBranch.value + '&nProduct=' + document.forms[0].valProduct.value + '&nPolicy=' + Field.value);	                	    
}

//%insShowChangeCurrency: Se habilita/deshabilita el campo moneda
//-------------------------------------------------------------------------------------------
function insShowChangeCurrency(){
//-------------------------------------------------------------------------------------------
	with (document.forms[0]){
	if (dtcClient.value != '' &&
		cbeTypeAccount.value != 0)       
        insDefValues('BussiTypeParam','nTypeAccount=' + cbeTypeAccount.value + '&sBussiType=' + cbeBussType.value+ '&sClient=' + dtcClient.value, '/VTimeNet/CashBank/CashBank');
	}	
}
</SCRIPT>

</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="post" ID="FORM" NAME="frmCurrAccInq" ACTION="ValCashBank.aspx?sMode=1">
<BR></BR>
    <TABLE WIDTH="100%">
        <TR>
            <TD><LABEL ID=8758><%= GetLocalResourceObject("tcdEffecdateCaption") %></LABEL></TD>
            <TD><%=mobjValues.DateControl("tcdEffecdate", CStr(Today),  , GetLocalResourceObject("tcdEffecdateToolTip"),  ,  ,  ,  , True)%></TD>
            <TD WIDTH="5%">&nbsp;</TD>
            <TD><LABEL ID=8761><%= GetLocalResourceObject("cbeTypeAccountCaption") %></LABEL></TD>
            <TD><%Response.Write(mobjValues.PossiblesValues("cbeTypeAccount", "Table400", 1,  ,  ,  ,  ,  ,  , "insChangLocked(this);insShowChangeCurrency();", True,  , GetLocalResourceObject("cbeTypeAccountToolTip")))%></TD>
        </TR>
        <TR>
			<TD><LABEL ID=8754><%= GetLocalResourceObject("cbeBussTypeCaption") %></LABEL></TD>
            <TD><%mobjValues.BlankPosition = False
Response.Write(mobjValues.PossiblesValues("cbeBussType", "Table20", 1,  ,  ,  ,  ,  ,  ,  , True,  , GetLocalResourceObject("cbeBussTypeToolTip")))%></TD>
            <TD COLSPAN="3">&nbsp;</TD>
        </TR>
        <TR>    
            <TD><LABEL ID=8756><%= GetLocalResourceObject("dtcClientCaption") %></LABEL></TD>
            <TD COLSPAN="4"><%=mobjValues.ClientControl("dtcClient", vbNullString,  , GetLocalResourceObject("dtcClientToolTip"), "insShowChangeCurrency();", True, "lblClieName")%>
            </TD>
        </TR>
        <TR>
            <TD COLSPAN="5" CLASS="HighLighted"><LABEL ID=40151><A NAME="Datos de la póliza"><%= GetLocalResourceObject("AnchorDatos de la pólizaCaption") %></A></LABEL></TD>
        </TR>
        <TR>
            <TD COLSPAN="5" CLASS="Horline"></TD>
        </TR>
    </TABLE>
    <TABLE WIDTH="100%">
        <TR>
            <TD><LABEL ID=8753><%= GetLocalResourceObject("cbeBranchCaption") %></LABEL></TD>
            <TD><%=mobjValues.BranchControl("cbeBranch", GetLocalResourceObject("cbeBranchToolTip"), "",  ,  ,  ,  ,  , True)%></TD>
            <TD><LABEL ID=8760><%= GetLocalResourceObject("valProductCaption") %></LABEL></TD>
            <TD><%=mobjValues.ProductControl("valProduct", GetLocalResourceObject("valProductToolTip"), "", eFunctions.Values.eValuesType.clngWindowType, True)%></TD>
            <TD><LABEL ID=8759><%= GetLocalResourceObject("tcnPolicyCaption") %></LABEL></TD>
            <TD><%=mobjValues.NumericControl("tcnPolicy", 8, CStr(0),  , GetLocalResourceObject("tcnPolicyToolTip"),  ,  ,  ,  ,  , "insValidEmpty(this)", True)%></TD>
            <TD><LABEL ID=8755><%= GetLocalResourceObject("tcnCertifCaption") %><<%= GetLocalResourceObject("tcnCertifCaption") %>LABEL></TD>
            <TD><%=mobjValues.NumericControl("tcnCertif", 4, CStr(0),  , GetLocalResourceObject("tcnCertifToolTip"),  ,  ,  ,  ,  , "insValidEmpty(this)", True)%></TD>
            <TD></TD>
            <TD></TD>
        </TR>
    </TABLE>
    <TABLE WIDTH="100%">
        <TR>
            <TD><LABEL ID=8757><%= GetLocalResourceObject("cbeCurrencyCaption") %></LABEL></TD>
<%mobjValues.Parameters.Add("nTyp_acco", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
mobjValues.Parameters.Add("sType_acc", "0", eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 255, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
mobjValues.Parameters.Add("nIntermed", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
mobjValues.Parameters.Add("sClient", eRemoteDB.Constants.strNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 255, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
%>
            <TD><%=mobjValues.PossiblesValues("cbeCurrency", "TabCurr_Cli_Inter", 2, "", True,  ,  ,  ,  ,  , True,  , GetLocalResourceObject("cbeCurrencyToolTip"))%></TD>
            
            <TD><LABEL ID=8752><%= GetLocalResourceObject("AnchorCaption") %></LABEL></TD>            
            <TD><%=mobjValues.DIVControl("lblBalance", True, "0,00")%></TD>

            <TD><%=mobjValues.OptionControl(40152, "optTypeAmou", GetLocalResourceObject("optTypeAmouCreditor_Caption"),  ,  ,  , True,  , GetLocalResourceObject("optTypeAmou_ToolTip"))%></TD>
            <TD><%=mobjValues.OptionControl(40153, "optTypeAmou", GetLocalResourceObject("optTypeAmou_Caption"),  ,  ,  , True,  , GetLocalResourceObject("optTypeAmou_ToolTip"))%></TD>
        </TR>
        <%Response.Write(mobjValues.HiddenControl("tctTypeCurracc", "0"))%>
    </TABLE>
</FORM>
</BODY>
</HTML>
<%mobjValues = Nothing%>





