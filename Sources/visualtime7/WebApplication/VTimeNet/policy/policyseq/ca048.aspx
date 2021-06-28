<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="ePolicy" %>
<script language="VB" runat="Server">
'^Begin Header Block VisualTimer Utility 1.1 31/3/03 19.42.04
Dim mobjNetFrameWork As eNetFrameWork.Layout
Dim mblnWaitCode As Boolean

Dim mobjValues As eFunctions.Values
Dim mobjCertificat As ePolicy.Certificat
Dim mintChkPrint As String
Dim mlngNotenum As Double
Dim mobjPolicy As ePolicy.Policy_his


</script>
<%Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage("CA048")
'~End Header Block VisualTimer Utility
Response.CacheControl = "private"

mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 31/3/03 19.42.04
mobjValues.sSessionID = Session.SessionID
mobjValues.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjValues.sCodisplPage = Request.QueryString.Item("sCodispl")
mobjCertificat = New ePolicy.Certificat

mobjValues.ActionQuery = Session("bQuery")

'+ Se cargan los datos de la página.
Call mobjCertificat.insLoadCA048(Session("sCertype"), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("npolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nCertif"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble), Session("nTransaction"), Session("sTypeCompanyUser"))

'+Se crean variables script para des/habilitar campos
'+Con estas variables se sabe si campos estan 
'+deshabilitadas por omision
Response.Write("<SCRIPT>" & vbCrLf)
'+Tipos de recibo. 
'+Inicialmente, si esta habilitado el primero ("Sin Recibo")
'+se asume habilitado el resto
If mobjCertificat.bNotReceipt Then
	Response.Write("var mblnAllReceipt = true;" & vbCrLf)
Else
	Response.Write("var mblnAllReceipt = false;" & vbCrLf)
End If
'+"Recibo automatico". Posee su propio manejo
If mobjCertificat.bAutReceipt Then
	Response.Write("var mblnAutReceip = true;" & vbCrLf)
Else
	Response.Write("var mblnAutReceip = false;" & vbCrLf)
End If
'+Impresion de documentos y Afectar certificado.
'+Como este ultimo no posee manejo propio, asume mismo manejo de Impresion
If mobjCertificat.bPrinterStat Then
	Response.Write("var mblnPrint = true;" & vbCrLf)
	Response.Write("var mblnApplyCert = true;" & vbCrLf)
Else
	Response.Write("var mblnPrint = false;" & vbCrLf)
	Response.Write("var mblnApplyCert = true;" & vbCrLf)
End If
Response.Write("</SCRIPT>" & vbCrLf)
%>

<HTML>
<HEAD>
	<META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/Constantes.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/ValFunctions.js"></SCRIPT>



<SCRIPT LANGUAGE="JavaScript">
//- Variable para el control de versiones
    document.VssVersion="$$Revision: 3 $|$$Date: 14-08-09 11:08 $|$$Author: Mpalleres $"

//% EnableCombo: Esta función se encarga de habilitar los controles de la pagina
//-----------------------------------------------------------------------------
function EnableCombo(bCheck){
//-----------------------------------------------------------------------------
	with(self.document.forms[0]){
        chkAfeccer.disabled = bCheck || !mblnPrint;
        chkPrintNow.disabled = chkAfeccer.disabled;
        if(chkAfeccer.disabled)
        {
	        chkAfeccer.checked=false;
	        chkPrintNow.checked=false;
	    }
	}
}

//% ChangeCheck
//-----------------------------------------------------------------------------
function ChangeCheck(){
//-----------------------------------------------------------------------------
	with(self.document.forms[0]){
        if(chkPrint.checked)
	        chkPrint.value=1;
	    else
	        chkPrint.value=0;
	}
}

//%doSubmit: Ejecuta el término de la transacción al aceptar boton
//-----------------------------------------------------------------------------
function doSubmit(){
//-----------------------------------------------------------------------------    
    var lblnPenden;
    var lblnPrint;
    
    with(self.document.forms[0]){
//+Habilita momentáneamente los campos 
//+para que se envíen por el formulario
		self.document.forms[0].target = 'fraGeneric';
		UpdateDiv('lblWaitProcess','<MARQUEE>Procesando, por favor espere...</MARQUEE>','');
        lblnPenden=chkPendenStat.disabled
        lblnPrint=chkPrintNow.disabled
        chkPendenStat.disabled=false;
        cbeWaitCode.disabled=false;
        chkPrintNow.disabled=false;
//+Ejecuta submit
        submit();
//+Retorna deshabilitar 
        chkPendenStat.disabled=lblnPenden;
        cbeWaitCode.disabled=true;
        chkPrintNow.disabled=lblnPrint;
    }
}

</SCRIPT>    
<%
With Response
	.Write(mobjValues.StyleSheet())
	.Write(mobjValues.WindowsTitle("CA048", Request.QueryString.Item("sWindowDescript")))
End With
%>
    </HEAD>
    <BODY ONUNLOAD="closeWindows();">
        <FORM METHOD="POST" ID="FORM" NAME="frmCA048" TARGET="fraGeneric" ACTION="valPolicySeq.aspx?sCodispl=CA048&nAction=392&nZone=2">
            <TABLE WIDTH=100%>
                <TR>
                    <TD><LABEL ID=13230><%= GetLocalResourceObject("tcnTransactioCaption") %></LABEL>&nbsp;
                    <%=mobjValues.TextControl("tcnTransactio", 5, CStr(mobjCertificat.nTransactio),  , GetLocalResourceObject("tcnTransactioToolTip"), True)%>
                    </TD>
                </TR>
                <TR>
                    <TD><%=mobjValues.TextAreaControl("tctMessage", 4, 70, mobjCertificat.sMessage,  , GetLocalResourceObject("tctMessageToolTip"),  , True)%></TD>
                </TR>
            </TABLE>
            <TABLE WIDTH="100%" COLS="5">
                <TR>
                    <TD COLSPAN="2">
                    <%	
                        If mobjCertificat.bPrinterStat Then
                            mintChkPrint = "1"
                        Else
                            mintChkPrint = "2"
                        End If
                        Response.Write(mobjValues.CheckControl("chkPrintNow", GetLocalResourceObject("chkPrintNowCaption"), mintChkPrint, "1", , Not mobjCertificat.bPrinterStat))
	                %>
					</TD>
                </TR>
                <TR>
                    <TD COLSPAN="2"><%=mobjValues.CheckControl("chkPendenStat", GetLocalResourceObject("chkPendenStatCaption"), CStr(mobjCertificat.nPendenStat), "1", "EnableCombo(this.checked);", Not mobjCertificat.bPendenstat)%></TD>
                    <TD WIDTH="5%">&nbsp;</TD>
                    <TD><LABEL ID=13229><%= GetLocalResourceObject("cbeWaitCodeCaption") %></LABEL></TD>					    
					<TD><%mobjValues.TypeList = 2
If mobjValues.StringToType(CStr(mobjCertificat.nWait_code), eFunctions.Values.eTypeData.etdInteger) > 0 Then
	mobjValues.BlankPosition = False
End If
If mobjCertificat.nWait_code <> 21 And mobjCertificat.nWait_code <> 22 Then
	mblnWaitCode = True
Else
	mblnWaitCode = False
End If
mobjValues.List = "4" 'Cotización propuesta				    
Response.Write(mobjValues.PossiblesValues("cbeWaitCode", "tab_waitpo", eFunctions.Values.eValuesType.clngComboType, CStr(mobjCertificat.nWait_code),  ,  ,  ,  ,  ,  , mblnWaitCode,  , GetLocalResourceObject("cbeWaitCodeToolTip")))%>
					</TD>               
	            </TR>
                <TR>
                    
                    <TD COLSPAN="2" >
                    <%If Session("nTransaction") = eCollection.Premium.PolTransac.clngPolicyAmendment Then
	Response.Write(mobjValues.CheckControl("chkssstatus_pol", GetLocalResourceObject("chkssstatus_polCaption"), CStr(False), "1",  ,  ,  , GetLocalResourceObject("chkssstatus_polToolTip")))
Else
	Response.Write(mobjValues.HiddenControl("chkssstatus_pol", "1"))
End If
%></TD>					    
					<TD></TD>               
	            </TR>
                <TR>
                    <TD COLSPAN="5">&nbsp;</TD>
                </TR>
                <TR>

<%
'+ Certificado sólo se muestra en modificacion de póliza matriz de colectivo
If CStr(Session("sPolitype")) <> "1" And CStr(Session("sPolitype")) <> "" And CStr(Session("nCertif")) = "0" Then
	%>
						<TD COLSPAN="4"><%=mobjValues.CheckControl("chkAfeccer", GetLocalResourceObject("chkAfeccerCaption"),  , "1",  , Not CBool(mobjCertificat.bPrinterStat))%></TD>

                    <%Else%>
						<%=mobjValues.HiddenControl("chkAfeccer", "2")%>
						<TD COLSPAN="4">&nbsp;</TD>    
	                <%End If%>
					<TD ALIGN=CENTER><LABEL ID=40758><%= GetLocalResourceObject("SCA2-810Caption") %></LABEL>
					<%With Response
	mobjPolicy = New ePolicy.Policy_his
	
	
	If mobjPolicy.Find_Policy_his_nNotenum(Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif")) Then
		mlngNotenum = mobjPolicy.nNotenum
	End If
	.Write(mobjValues.ButtonNotes("SCA2-810", mlngNotenum, False, False))
End With
%>
     				</TD>

                </TR>
			</TABLE>
			<TABLE WIDTH=100%>                
                <TR>
                    <TD COLSPAN="4" CLASS="Horline"></TD>
                </TR>
                <TR>
                    <TD WIDTH=5%><%=mobjValues.ButtonAbout("CA048")%></TD>
                    <TD WIDTH=5%><%=mobjValues.ButtonHelp("CA048")%></TD>
			        <TD WIDTH=60% ALIGN=LEFT CLASS=HIGHLIGHTED><LABEL><DIV ID=lblWaitProcess></DIV></LABEL></TD>
                    <TD ALIGN=RIGHT><%=mobjValues.ButtonAcceptCancel("doSubmit();", "top.close();", False)%></TD>
                </TR>
			</TABLE>
        </FORM>
    <%
mobjPolicy = Nothing
mobjCertificat = Nothing
mobjValues = Nothing
%>
    </BODY>
</HTML>
<%'^Begin Footer Block VisualTimer Utility 1.1 31/3/03 19.42.04
Call mobjNetFrameWork.FinishPage("CA048")
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>




