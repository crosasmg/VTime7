<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="ePolicy" %>
<%@ Import namespace="eClient" %>
<script language="VB" runat="Server">
'^Begin Header Block VisualTimer Utility 1.1 31/3/03 19.42.04
Dim mobjNetFrameWork As eNetFrameWork.Layout

Dim lblnchkprint As Boolean
    Dim mintchkprint As Object

'- Variables Generales
Dim lblnActionQuery As Boolean

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'- Objeto para el manejo de las pólizas
Dim mobjPolicy As ePolicy.Policy_his
Dim lclsClient As eClient.Client
Dim lclsClientRoles As ePolicy.Roles

Dim lstrClientAseg As String
Dim lstrClientCont As String
Dim lstrAsegurado As String
    Dim lstrContratante As String
    
    Dim lblnDisabledchkCertif As Boolean = False


</script>
<%Response.Expires = -1441
    mobjNetFrameWork = New eNetFrameWork.Layout
    mobjNetFrameWork.sSessionID = Session.SessionID
    mobjNetFrameWork.nUsercode = Session("nUsercode")
    Call mobjNetFrameWork.BeginPage("CA050")
    '~End Header Block VisualTimer Utility
    Response.CacheControl = "private"

    mobjValues = New eFunctions.Values
    '^Begin Body Block VisualTimer Utility 1.1 31/3/03 19.42.04
    mobjValues.sSessionID = Session.SessionID
    mobjValues.nUsercode = Session("nUsercode")
    '~End Body Block VisualTimer Utility

    mobjValues.sCodisplPage = "CA050"
    mobjPolicy = New ePolicy.Policy_his
    lclsClient = New eClient.Client
    lclsClientRoles = New ePolicy.Roles

    mobjValues.ActionQuery = Session("bQuery")


    If mobjValues.StringToType(Session("nTransaction2"), eFunctions.Values.eTypeData.etdInteger) = 16 Then
        Session("nTransaction") = Session("nTransaction2")
    End If

    Session("nWaitCodeca050") = False 'ehh - Ad. vt fase II reconocimiento de ingresos

    Call mobjPolicy.insPreca050(Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), Session("sTypeCompanyUser"), mobjValues.StringToType(Session("nTransaction"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble), Session("sJustQuote"))

    If mobjPolicy.nWaitCode <= 0 Then 'ehh - Ad. vt fase II reconocimiento de ingresos
        Session("nWaitCodeca050") = True
    End If

    If mobjPolicy.nPrintNow = 2 Then
        lblnchkprint = False
        mintchkprint = 1
    Else
        lblnchkprint = True
        mintchkprint = 1
    End If

    If Len(lstrClientAseg) < 14 Then
        lstrClientAseg = lclsClient.ExpandCode(lstrAsegurado)
    End If
    If Len(lstrClientCont) < 14 Then
        lstrClientCont = lclsClient.ExpandCode(lstrContratante)
    End If
    If lclsClientRoles.Find(Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), 2, lstrClientAseg, Today, True) Then
        Session("Asegurado") = lclsClientRoles.SCLIENT
    End If
    If lclsClientRoles.Find(Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), 1, lstrClientCont, Today, True) Then
        Session("Contratante") = lclsClientRoles.SCLIENT
    End If
%>
<HTML>
<HEAD>
    <META NAME = "GENERATOR" CONTENT = "Microsoft Visual Studio 6.0">


<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/Constantes.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<%
        Response.Write(mobjValues.StyleSheet())
        Response.Write(mobjValues.WindowsTitle("CA050", Request.QueryString.Item("sWindowDescript")))
%>
<SCRIPT>
//- Variable para el control de versiones
	document.VssVersion="$$Revision: 4 $|$$Date: 28/04/06 1:08p $|$$Author: Pmanzur $"

//+ Indica si el Check de cliente fué o no impreso en la página.
    var lbnlPrintCarnetsNow = false

//% insAcceptData: Se ejecutan las acciones al aceptar la ventana
//-------------------------------------------------------------------------------------------
function insAcceptData(){
//-------------------------------------------------------------------------------------------
	self.document.forms[0].target = 'fraGeneric';
	UpdateDiv('lblWaitProcess','<MARQUEE>Procesando, por favor espere...</MARQUEE>','');
	EnabledControl();
}

//% ChangeWaitCode: Habilita o no ciertos controles, dependiendo del valor del combo.
//-------------------------------------------------------------------------------------------
function ChangeWaitCode(lNewCode){
//-------------------------------------------------------------------------------------------
    if (lNewCode.value!=0){
    
//+ Se desabilita tanto el check de impresión inmediata como el hidden asociado a él.    
        self.document.forms[0].blnEnabledPrintNow.value = false
        
//+ Valida si fué impreso el control de impresión de carnets
        if (lbnlPrintCarnetsNow) {
            self.document.forms[0].chkCarnetsNow.disabled = true
            self.document.forms[0].blnPrintCarnets.value = false
        }
    }
    else{
    
//+ Se habilita tanto el check de impresión inmediata como el hidden asociado a él.    
        self.document.forms[0].blnEnabledPrintNow.value = true
        
//+ Valida si fué impreso el control de impresión de carnets        
        if (lbnlPrintCarnetsNow) {
            self.document.forms[0].chkCarnetsNow.disabled = false
            self.document.forms[0].blnPrintCarnets.value = true
        }
    }
}
//% ChangeCheck
//-----------------------------------------------------------------------------
function ChangeCheck(){
//-----------------------------------------------------------------------------
	with(self.document.forms[0]){
        if(chkPrintNow.checked)
	        chkPrintNow.value=1;
	    else
	        chkPrintNow.value=0;
	}
}

//% ChangeCheckCD
//-----------------------------------------------------------------------------
function ChangeCheckCD(){
//-----------------------------------------------------------------------------
	with(self.document.forms[0]){
        if(chkPrintControlDig.checked)
	        chkPrintControlDig.value=1;
	    else
	        chkPrintControlDig.value=0;
	}
}
//% ChangeCheck
//-----------------------------------------------------------------------------
function ChangeGraphics(){
//-----------------------------------------------------------------------------
	with(self.document.forms[0]){
        if(chkGraphics.checked)
	        hddGraphics.value='1';
	    else
	        hddGraphics.value='0';
	}
}

</SCRIPT>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="post" ID="FORM" NAME="frmCA050" ACTION="valPolicySeq.aspx?nAction=392&sCodispl=CA050">
    <TABLE BORDER=1 CELLPADDING=5 BGCOLOR=WHITE  WIDTH="100%">
        <TR>
            <TD><%
With mobjValues
	lblnActionQuery = .ActionQuery
	.ActionQuery = True
	Response.Write(mobjValues.TextAreaControl("txtMessage", 5, 40, mobjPolicy.sTextMessage,  , GetLocalResourceObject("txtMessageToolTip")))
	.ActionQuery = lblnActionQuery
End With%>
            </TD>
        </TR>
    </TABLE>
    <TABLE WIDTH="100%">
        <TR><TD></TD></TR>
        <TR>
            <TD><%mobjValues.Parameters.Add("sBrancht", Session("sBrancht"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
With Response
	If mobjValues.StringToType(CStr(mobjPolicy.nWaitCode), eFunctions.Values.eTypeData.etdInteger) > 0 Then
		'mobjValues.BlankPosition = False
		If mobjValues.StringToType(CStr(mobjPolicy.nWaitCode), eFunctions.Values.eTypeData.etdInteger) = 29 Then
			mobjValues.TypeList = 2
			mobjValues.List = CStr(4)
		End If
                        End If
                        .Write(mobjValues.HiddenControl("chkDetailedEntryPrintedCaption", CStr(True)))

	.Write(mobjValues.PossiblesValues("cboWaitCode", "tabtab_waitpo", eFunctions.Values.eValuesType.clngComboType, CStr(mobjPolicy.nWaitCode), True,  ,  ,  ,  , "ChangeWaitCode(this)", mobjPolicy.blnWaitCode))
                        .Write(mobjValues.HiddenControl("blnEnabledPrintNow", CStr(mobjPolicy.blnWaitCode)))
	.Write(mobjValues.HiddenControl("blnEnabledWaitCode", CStr(mobjPolicy.blnWaitCode)))
End With
%>
			</TD>
        </TR>
        <TR>
            <TD><%
With Response
	If mobjPolicy.blnVisibleCarnetsNow Then
		.Write("<SCRIPT>lbnlPrintCarnetsNow=true</script>")
		'.Write mobjValues.CheckControl("chkCarnetsNow", GetLocalResourceObject("chkCarnetsNowCaption"),mobjPolicy.nCarnetsNow,,,,, GetLocalResourceObject("chkCarnetsNowToolTip"))
		.Write(mobjValues.HiddenControl("blnPrintCarnets", CStr(True)))
	Else
		.Write(mobjValues.HiddenControl("blnPrintCarnets", CStr(False)))
                        End If
                        If Session("sCertype") <> "1" Then
                            .Write(mobjValues.HiddenControl("chkPrintControlDig", CStr(True)))
                        End If
                        .Write(mobjValues.HiddenControl("pblnDocQuotation", CStr(mobjPolicy.blnDocQuotation)))
                    End With
%>
            </TD>
        </TR>
        <TR> 
            <TD><%
                    ' El campo 'Afectar certificados', es visible solo si la póliza es colectiva y 
                    ' si se trata de una cotización o propuesta de renovación, es seleccionado por defecto y el usuario no podrá cambiar su contenido
                    If mobjPolicy.blnCertif Then
                        lblnDisabledchkCertif = (Session("nTransaction") = 28 Or Session("nTransaction") = 30)
                        Response.Write(mobjValues.CheckControl("chkCertif", GetLocalResourceObject("chkCertifCaption"), "1", "1", , lblnDisabledchkCertif, , GetLocalResourceObject("chkCertifToolTip")))
                    End If
%> 
            </TD>
        </TR>
        <TR>
			
            <%If Session("nTransaction") = -1 Then%> 
                <%	If Session("nTransaction") = 6 Or Session("nTransaction") = 23 Then%>
                      <TD COLSPAN="2"><%=mobjValues.CheckControl("chkPrintNow", GetLocalResourceObject("chkPrintNowCaption"), CStr(2),  , "ChangeCheck();", False)%></TD>
                <%	Else%>
                  <TD WIDTH="5%">&nbsp;</TD>
                <%	End If%> 
           
            <TD></TD>					    
            <%Else%>
            
            <TD COLSPAN="2"><%= mobjValues.CheckControl("chkPrintNow", GetLocalResourceObject("chkPrintNowCaption"), , , "ChangeCheck();", Not lblnchkprint)%></TD>
            
            <%End If%>
			<TD></TD>               
	    </TR>
        <%If Session("sCertype") = "1" Then%>
            <TR>
                <TD COLSPAN="3"><%=mobjValues.CheckControl("chkPrintControlDig", GetLocalResourceObject("chkPrintControlDigCaption"), CStr(2),  , "ChangeCheckCD();")%></TD>
	        </TR>
        <%End If%>
    </TABLE>
    <TABLE WIDTH=100%>
        <TR>
			<TD COLSPAN="4" CLASS="Horline"></TD>
		</TR>
		<TR>
			<TD WIDTH=5%><%=mobjValues.ButtonAbout("CA050")%></TD>
			<TD WIDTH=5%><%=mobjValues.ButtonHelp("CA050")%></TD>
			<TD WIDTH=60% ALIGN=LEFT CLASS=HIGHLIGHTED><LABEL><DIV ID=lblWaitProcess></DIV></LABEL></TD>
			<TD ALIGN=RIGHT><%=mobjValues.ButtonAcceptCancel("insAcceptData();", "top.close();", True)%></TD>
        </TR>
    </TABLE>
<%
mobjValues = Nothing
mobjPolicy = Nothing
%>
</FORM>
</BODY>
</HTML>
<%'^Begin Footer Block VisualTimer Utility 1.1 31/3/03 19.42.04
Call mobjNetFrameWork.FinishPage("CA050")
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>






