<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'- Objeto para el manejo del menú
Dim mobjMenu As eFunctions.Menues

'- Variables para almacenar parametros
Dim mstrBranch As String
Dim mstrProduct As String
Dim mstrPolicy As String
Dim mstrCertif As String
Dim mstrMovement As String


</script>
<%Response.Expires = 0

mobjValues = New eFunctions.Values
mobjMenu = New eFunctions.Menues
mobjValues.ActionQuery = CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 401

'+ Se cargan parametros
With Request
	mstrBranch = .QueryString.Item("nBranch")
	mstrProduct = .QueryString.Item("nProduct")
	mstrPolicy = .QueryString.Item("nPolicy")
	mstrCertif = .QueryString.Item("nCertif")
	mstrMovement = .QueryString.Item("nMovement")
End With

%>
<HTML>
<HEAD>


<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tmenu.js"></SCRIPT>        
<SCRIPT LANGUAGE=JavaScript>
//+ Variable para el control de versiones
document.VssVersion="$$Revision: 2 $|$$Date: 15/10/03 16:37 $|$$Author: Nvaplat61 $"

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
//--------------------------------------------------------------------------------------------
function insChangeField(nOption){
//--------------------------------------------------------------------------------------------
//- Variable para definir parametros
    var lstrParams = new String;

    with(document.forms[0]){
//+ Se definen parametros de la poliza/certificado
        lstrParams += 'sCertype=2' + 
                      '&nBranch=' + cbeBranch.value +
                      '&nProduct=' + valProduct.value + 
                      '&nPolicy=' + tcnPolicy.value + 
                      '&nCertif=' + tcnCertif.value;
        
        if (cbeBranch.value!='' && 
            cbeBranch.value!='0' && 
            valProduct.value!='' && 
            valProduct.value!='0' && 
            tcnPolicy.value!='' && 
            tcnPolicy.value!='0')

            if (nOption==0)
//+ Se buscan los datos de moneda de la cuenta de poliza            
                insDefValues('AccPolDat',lstrParams);
            else        
                if (tcnMovement.value!=''&& 
                    tcnMovement.value!='0'){
                
//+ Se agrega parametro de movimiento a bussar
                    lstrParams += '&nIdMov=' + tcnMovement.value;

                    if (nOption==1)
//+ Se buscan datos de movimiento
                        insDefValues('MoveAccDat',lstrParams);
                    else
//+ Se buscan datos de moneda y movimiento                   
                        insDefValues('CurrMoveAcc',lstrParams);
                }
    }
}
</SCRIPT>
<%
Response.Write(mobjValues.StyleSheet())
If Request.QueryString.Item("Type") <> "PopUp" Then
	Response.Write(mobjMenu.MakeMenu("VAC610", "VAC610_k.aspx", 1, vbNullString))
	
	mobjMenu = Nothing
	Response.Write("<SCRIPT>var nMainAction=top.frames['fraSequence'].plngMainAction</SCRIPT>")
End If
%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<BR><BR>
<FORM METHOD="POST" NAME="VAC610" ACTION="ValPolicyQue.aspx?sMode=2">
    <TABLE WIDTH="100%">
        <TR>
            <TD WIDTH="20%"><LABEL ID=0><%= GetLocalResourceObject("cbeBranchCaption") %></LABEL></TD>
            <TD WIDTH="20%"><%=mobjValues.BranchControl("cbeBranch", GetLocalResourceObject("cbeBranchToolTip"), mobjValues.StringToType(mstrBranch, eFunctions.Values.eTypeData.etdDouble), "valProduct",  ,  ,  , "", False)%></TD>
            <TD WIDTH="3%">&nbsp;</TD>
            <TD WIDTH="15%"><LABEL ID=0><%= GetLocalResourceObject("valProductCaption") %></LABEL></TD>
            <TD WIDTH="45%"><%=mobjValues.ProductControl("valProduct", GetLocalResourceObject("valProductToolTip"), mobjValues.StringToType(mstrBranch, eFunctions.Values.eTypeData.etdDouble), eFunctions.Values.eValuesType.clngWindowType, False, mobjValues.StringToType(mstrProduct, eFunctions.Values.eTypeData.etdDouble))%> </TD>
        </TR>
        <TR>
            <TD><LABEL ID=0><%= GetLocalResourceObject("tcnPolicyCaption") %></LABEL></TD>
            <TD><%=mobjValues.NumericControl("tcnPolicy", 10, mobjValues.StringToType(mstrPolicy, eFunctions.Values.eTypeData.etdDouble), True, GetLocalResourceObject("tcnPolicyToolTip"), False,  ,  ,  ,  , "insChangeField(0);", False)%></TD>
            <TD>&nbsp;</TD>
            <TD><LABEL ID=0><%= GetLocalResourceObject("tcnCertifCaption") %></LABEL></TD>
            <TD><%=mobjValues.NumericControl("tcnCertif", 10, mobjValues.StringToType(mstrCertif, eFunctions.Values.eTypeData.etdDouble),  , GetLocalResourceObject("tcnCertifToolTip"),  ,  ,  ,  ,  , "insChangeField(0);", False)%></TD> 
        </TR>
        <TR>
            <TD><LABEL ID=0><%= GetLocalResourceObject("tcnMovementCaption") %></LABEL></TD>
            <TD><%=mobjValues.NumericControl("tcnMovement", 5, mobjValues.StringToType(mstrMovement, eFunctions.Values.eTypeData.etdDouble), True, GetLocalResourceObject("tcnMovementToolTip"),  ,  ,  ,  ,  , " insChangeField(1);", False)%> </TD>
            <TD>&nbsp;</TD>
            <TD><LABEL ID=0><%= GetLocalResourceObject("AnchorCaption") %></LABEL></TD>
            <TD><%=mobjValues.DIVControl("divCurrency", False, "")%></TD>  
        </TR>
        <TR>
            <TD><LABEL ID=0><%= GetLocalResourceObject("Anchor2Caption") %></LABEL></TD>
            <TD><%=mobjValues.DIVControl("divMoveDate", False)%> </TD>
            <TD>&nbsp;</TD>
            <TD><LABEL ID=0><%= GetLocalResourceObject("Anchor3Caption") %></LABEL></TD>
            <TD><%=mobjValues.DIVControl("divMoveType", False, "")%> </TD>
        </TR>
        <TR>
            <TD><LABEL ID=0><%= GetLocalResourceObject("Anchor4Caption") %></LABEL></TD>
            <TD><%=mobjValues.DIVControl("divAmount", False, "")%> </TD>
            <TD>&nbsp;</TD>
            <TD><LABEL ID=0><%= GetLocalResourceObject("Anchor5Caption") %></LABEL></TD>
            <TD><%=mobjValues.DIVControl("divReceipt", False)%> </TD>
        </TR>        
        
        
    </TABLE>
</FORM> 
</BODY>
</HTML>
<%
mobjValues = Nothing

'+ Si fue invocada desde otra ventana deshabilita campos y muestra detalle automaticamente 
If mstrBranch <> vbNullString Then
	%>
<SCRIPT LANGUAGE=javascript>
//+ Deshabilita campos de cabecera
    with(document.forms[0]){
        cbeBranch.disabled=true;
        valProduct.disabled=true;
        tcnPolicy.disabled=true;
        tcnCertif.disabled=true;
        tcnMovement.disabled=true;
    }

//+ Carga descripciones de moneda y movimiento
    insChangeField(2);
    
//+ Carga ventana con resultado de accion consultar 
	ClientRequest(390,6);
	
</SCRIPT>
<%	
End If
%>




