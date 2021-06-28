<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eSecurity" %>
<%@ Import namespace="eRemoteDB" %>
<script language="VB" runat="Server">
'^Begin Header Block VisualTimer Utility 1.1 7/4/03 11.35.15
Dim mobjNetFrameWork As eNetFrameWork.Layout
'~End Header Block VisualTimer Utility

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'- Objeto para el manejo del menú de la página
Dim MobjMenu As eFunctions.Menues

Dim mintTransaction As Object
Dim blnTransac As Boolean
Dim mclsSche_Transac As eSecurity.Secur_sche


'% LoadPageInSequence: se carga la página cuando se encuentra dentro de la secuencia
'-------------------------------------------------------------------------------------------------------------------
Sub LoadPageInSequence()
	
Response.Write("" & vbCrLf)
Response.Write("<SCRIPT LANGUAGE=javascript>" & vbCrLf)
Response.Write("<!--" & vbCrLf)
Response.Write("//%insShowNextWindow. Se encarga de mostrar la siguiente ventana a ser mostrada" & vbCrLf)
Response.Write("//--------------------------------------------------------------------------------------------" & vbCrLf)
Response.Write("function insShowNextWindow(){" & vbCrLf)
Response.Write("//--------------------------------------------------------------------------------------------" & vbCrLf)
Response.Write("    var lblnDoIt=true;" & vbCrLf)
Response.Write("    if (typeof(top.frames['fraSequence'])!='undefined')" & vbCrLf)
Response.Write("        if (typeof(top.frames['fraSequence'].NextWindows)!='undefined'){" & vbCrLf)
Response.Write("            top.frames['fraSequence'].NextWindows('');" & vbCrLf)
Response.Write("            lblnDoIt = false;" & vbCrLf)
Response.Write("        }" & vbCrLf)
Response.Write("}" & vbCrLf)
Response.Write("//-->" & vbCrLf)
Response.Write("</" & "SCRIPT>")

	
	'-------------------------------------------------------------------------------------------------------------------
	'- Variable en JScript que indica la acción que seleccionó el usuario
	Response.Write("<SCRIPT>var mstrTransaction='" & Session("nTransaction") & "'</" & "Script>")
	
	Dim lstrDescBranch As String
	Dim lclsQuery As eRemoteDB.Query
	lclsQuery = New eRemoteDB.Query
	With lclsQuery
		If .OpenQuery("Table10", "sDescript", "nBranch = " & Session("nBranch")) Then
			lstrDescBranch = .FieldToClass("sDescript")
			.CloseQuery()
		End If
	End With
	lclsQuery = Nothing

Response.Write("" & vbCrLf)
Response.Write("	<TABLE WIDTH=100%>	" & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("			<TD>")


Response.Write(mobjValues.TextControl("lblClaim", 10, "Siniestro",  , "", True))


Response.Write("</TD>" & vbCrLf)
Response.Write("			<TD>")


Response.Write(mobjValues.TextControl("lblLabelClaim", 10, Session("nClaim"),  , "", True))


Response.Write("</TD>" & vbCrLf)
Response.Write("			<TD WIDTH=10%>&nbsp;</TD>" & vbCrLf)
Response.Write("			<TD>")


Response.Write(mobjValues.TextControl("lblBranch", 4, "Ramo",  , "", True))


Response.Write("</TD>" & vbCrLf)
Response.Write("			<TD>")


Response.Write(mobjValues.TextControl("lblLabelBranch", 30, lstrDescBranch,  , "", True))


Response.Write("</TD>" & vbCrLf)
Response.Write("			<TD WIDTH=10%>&nbsp;</TD>" & vbCrLf)
Response.Write("			<TD>")


Response.Write(mobjValues.TextControl("lblPolicy", 6, "Póliza",  , "", True))


Response.Write("</TD>" & vbCrLf)
Response.Write("			<TD>")


Response.Write(mobjValues.TextControl("lblLabelPolicy", 10, Session("nPolicy"),  , "", True))
        Session("nPolicyGM") = Session("nPolicy")

Response.Write("</TD>" & vbCrLf)
Response.Write("			<TD>")


Response.Write(mobjValues.TextControl("lblSlash", 1, "/",  , "", True))


Response.Write("</TD>" & vbCrLf)
Response.Write("			<TD>")


Response.Write(mobjValues.TextControl("lblCertif", 10, Session("nCertif"),  , "", True))


Response.Write("</TD>" & vbCrLf)
Response.Write("		</TR>" & vbCrLf)
Response.Write("	</TABLE>" & vbCrLf)
Response.Write("<SCRIPT>insShowNextWindow();</" & "SCRIPT>")

	
End Sub
'% LoadHeader: se carga la página cuando muestra los datos de la secuencia
'------------------------------------------------------------------------------------------------------------------
Sub LoadHeader()
	'------------------------------------------------------------------------------------------------------------------
	
Response.Write("	<!-- <P>&nbsp;</P> -->" & vbCrLf)
Response.Write("    <TABLE WIDTH=""100%"">" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("			<TD><LABEL ID=9393>" & GetLocalResourceObject("cbeTransactioCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("			<TD>" & vbCrLf)
Response.Write("			")


Response.Write(mobjValues.ComboControl("cbeTransactio", mclsSche_Transac.Sche_Transac(Session("sSche_code"), "SI001"), mintTransaction, True, 1, GetLocalResourceObject("cbeTransactioToolTip"), "insSelTransaction();", blnTransac))


Response.Write("</TD>" & vbCrLf)
Response.Write("			<TD>&nbsp;</TD>" & vbCrLf)
Response.Write("			<TD><LABEL ID=9384>" & GetLocalResourceObject("tcdEffecdateCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("            <TD>")

Response.Write(mobjValues.DateControl("tcdEffecdate", CStr(Today),  , GetLocalResourceObject("tcdEffecdateToolTip"),  ,  ,  , "SetLedgerDateValue(this.value);", True))


Response.Write("</TD>" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("			<TD><LABEL ID=9376>" & GetLocalResourceObject("tcnClaimCaption") & "</LABEL></TD>	" & vbCrLf)
Response.Write("			<TD>")

	
	If Request.QueryString.Item("DP051_nClaim") = vbNullString Then
		If Request.QueryString.Item("SIC001") = vbNullString And Request.QueryString.Item("SIC002") = vbNullString Then
			Response.Write(mobjValues.NumericControl("tcnClaim", 10, vbNullString,  , GetLocalResourceObject("tcnClaimToolTip"),  , 0,  ,  ,  , "ShowChangeValues(""Claim"")", True))
		Else
			Response.Write(mobjValues.NumericControl("tcnClaim", 10, Request.QueryString.Item("nClaim"),  , GetLocalResourceObject("tcnClaimToolTip"),  , 0,  ,  ,  , "(""Claim"");", True))
		End If
	Else
		Response.Write(mobjValues.NumericControl("tcnClaim", 10, Session("DP051_nClaim"),  , GetLocalResourceObject("tcnClaimToolTip"),  , 0,  ,  ,  , "ShowChangeValues(""Claim"")", True))
	End If
	
	
Response.Write("</TD>" & vbCrLf)
Response.Write("			<TD>&nbsp;</TD>" & vbCrLf)
Response.Write("			" & vbCrLf)
Response.Write("			<TD><LABEL ID=9387>" & GetLocalResourceObject("cbeOfficeCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("            <TD>")


Response.Write(mobjValues.PossiblesValues("cbeOffice", "table9", eFunctions.Values.eValuesType.clngComboType,  ,  ,  ,  ,  ,  , "BlankOfficeDepend();insInitialAgency(1,1)", True))


Response.Write("</TD>			" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("            <TD><LABEL ID=0>" & GetLocalResourceObject("cbeOfficeAgenCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("            <TD>")

	With mobjValues
		.Parameters.Add("nOfficeAgen", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Parameters.Add("nAgency", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Parameters.ReturnValue("nBran_off",  ,  , True)
		Response.Write(.PossiblesValues("cbeOfficeAgen", "TabAgencies_T5556", eFunctions.Values.eValuesType.clngWindowType,  , True,  ,  ,  ,  , "BlankAgencyDepend();insInitialAgency(2,1)", True,  , GetLocalResourceObject("cbeOfficeAgenToolTip")))
	End With
	
Response.Write("" & vbCrLf)
Response.Write("            </TD>" & vbCrLf)
Response.Write("            <TD>&nbsp;</TD>            " & vbCrLf)
Response.Write("            <TD><LABEL ID=0>" & GetLocalResourceObject("cbeAgencyCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("            <TD>")

	With mobjValues
		.Parameters.Add("nOfficeAgen", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Parameters.Add("nAgency", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Parameters.ReturnValue("nBran_off",  ,  , True)
		.Parameters.ReturnValue("nOfficeAgen",  ,  , True)
		.Parameters.ReturnValue("sDesAgen",  ,  , True)
		Response.Write(.PossiblesValues("cbeAgency", "TabAgencies_T5555", eFunctions.Values.eValuesType.clngWindowType, Request.Form.Item("cbeAgency"), True,  ,  ,  ,  , "insInitialAgency(3,1)", True,  , GetLocalResourceObject("cbeAgencyToolTip")))
	End With
	
Response.Write("" & vbCrLf)
Response.Write("            </TD>" & vbCrLf)
Response.Write("        </TR>                                       " & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("            <TD><LABEL ID=9380>" & GetLocalResourceObject("cbeBranchCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("            <TD>")


Response.Write(mobjValues.BranchControl("cbeBranch", GetLocalResourceObject("cbeBranchToolTip"),  , "valProduct",  ,  ,  , "ShowChangeValues(""Branch"");", True))


Response.Write("</TD>" & vbCrLf)
Response.Write("            <TD>&nbsp;</TD>" & vbCrLf)
Response.Write("			<TD><LABEL ID=9389>" & GetLocalResourceObject("valProductCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("			<TD>")

	With mobjValues
		Response.Write(mobjValues.ProductControl("valProduct", GetLocalResourceObject("valProductToolTip"),  , eFunctions.Values.eValuesType.clngWindowType, True,  ,  ,  ,  , "ShowChangeValues(""valProduct"")"))
	End With
	
Response.Write("" & vbCrLf)
Response.Write("			</TD>" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("			<TD><LABEL ID=9388>" & GetLocalResourceObject("tcnPolicyCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("			<TD>")


Response.Write(mobjValues.NumericControl("tcnPolicy", 10, vbNullString,  , GetLocalResourceObject("tcnPolicyToolTip"),  , 0,  ,  ,  , "ShowChangeValues(""Policy"")", True))


Response.Write("</TD>" & vbCrLf)
Response.Write("			<TD>&nbsp;</TD>" & vbCrLf)
Response.Write("			<TD><LABEL ID=9381>" & GetLocalResourceObject("tcnCertificatCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("			<TD>")


Response.Write(mobjValues.NumericControl("tcnCertificat", 10, vbNullString,  , GetLocalResourceObject("tcnCertificatToolTip"),  , 0,  ,  ,  , "ShowChangeValues(""Certif"")", True))


Response.Write("" & vbCrLf)
Response.Write("			</TD>" & vbCrLf)
Response.Write("		</TR>" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("			<TD><LABEL ID=9388>" & GetLocalResourceObject("btnClientPolicyCaption") & "</LABEL>" & vbCrLf)
Response.Write("			    ")


Response.Write(mobjValues.AnimatedButtonControl("btnClientPolicy", "/VTimeNet/images/btn_ValuesOff.png", GetLocalResourceObject("btnClientPolicyToolTip"),  , "ShowPolicies()", False))


Response.Write("</TD>" & vbCrLf)
Response.Write("			<TD>" & vbCrLf)
Response.Write("				")


Response.Write(mobjValues.ClientControl("dtcClient", vbNullString,  , GetLocalResourceObject("dtcClientToolTip"), "ShowChangeValues(""Client"")", False, "sCliename",  ,  ,  ,  ,  ,  ,  , True))


Response.Write("" & vbCrLf)
Response.Write("			</TD>" & vbCrLf)
Response.Write("			<TD>&nbsp;</TD>" & vbCrLf)
Response.Write("			<TD><LABEL ID=9381>" & GetLocalResourceObject("tcdBirthdatCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("			<TD>")


Response.Write(mobjValues.DateControl("tcdBirthdat", vbNullString,  , GetLocalResourceObject("tcdBirthdatToolTip"),  ,  ,  ,  , True))


Response.Write("" & vbCrLf)
Response.Write("		</TR>" & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("</TD>" & vbCrLf)
        Response.Write("			<TD><LABEL ID=0>" & GetLocalResourceObject("tctRegisterCaption") & "</LABEL>" & vbCrLf)
        Response.Write(mobjValues.AnimatedButtonControl("btnAutoRegist", "/VTimeNet/images/btn_ValuesOff.png", GetLocalResourceObject("btnAutoRegistTooltip"), , "ShowPolicies(""regist"")", False))
Response.Write("			</TD>" & vbCrLf)

Response.Write("			<TD>")


        Response.Write(mobjValues.TextControl("tctRegister", 10, "", , GetLocalResourceObject("tctRegisterTooltip")) & "-" & mobjValues.TextControl("tctDigit", 1, "", , GetLocalResourceObject("tctDigitTooltip"), , , , , True))


Response.Write("" & vbCrLf)
Response.Write("		</TR>" & vbCrLf)
        Response.Write("        <TR>" & vbCrLf)
Response.Write("			<TD><LABEL ID=0>" & GetLocalResourceObject("AnchorCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("			<TD COLSPAN=2>")


Response.Write(mobjValues.DIVControl("valStatuspol",  , ""))


Response.Write("</TD>" & vbCrLf)
Response.Write("			<TD><LABEL ID=0>" & GetLocalResourceObject("tcdOccurrdatCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("			<TD>")


Response.Write(mobjValues.DateControl("tcdOccurrdat",  ,  , GetLocalResourceObject("tcdOccurrdatToolTip"),  ,  ,  , "ShowChangeValues(""Certif"")", True))


Response.Write("" & vbCrLf)
Response.Write("		</TR>" & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("			<TD><LABEL>" & GetLocalResourceObject("tcdContinueCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("			<TD>")


Response.Write(mobjValues.DateControl("tcdContinue",  ,  , GetLocalResourceObject("tcdContinueToolTip"),  ,  ,  ,  , True))


Response.Write("" & vbCrLf)
Response.Write("			<TD>&nbsp;</TD>" & vbCrLf)
        Response.Write("			<TD><LABEL ID=0>" & GetLocalResourceObject("valIdCatasCaption") & "</LABEL></TD>" & vbCrLf)
        Response.Write("			<TD>")


        With mobjValues
            .Parameters.Add("NTRANSACTION", 1, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            Response.Write(.PossiblesValues("valIdCatas", "TabCat_Event", eFunctions.Values.eValuesType.clngWindowType, , True, , , , , , True, , GetLocalResourceObject("valIdCatasToolTip")))
        End With


        Response.Write("" & vbCrLf)
        Response.Write("		</TD>" & vbCrLf)

        Response.Write("		</TR>" & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("            <TD COLSPAN=""5"" CLASS=""HighLighted""><LABEL ID=40300><A NAME=""Relaciones"">" & GetLocalResourceObject("AnchorRelacionesCaption") & "</A></LABEL></TD>" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("            <TD COLSPAN=""5"" CLASS=""Horline""></TD>" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("            <TD><LABEL ID=9378>" & GetLocalResourceObject("tctRequest_nuCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("            <TD>")


Response.Write(mobjValues.TextControl("tctRequest_nu", 12, vbNullString,  , GetLocalResourceObject("tctRequest_nuToolTip"),  ,  ,  ,  , True))


Response.Write("</TD> " & vbCrLf)
Response.Write("            <TD>&nbsp;</TD>" & vbCrLf)
Response.Write("            <TD><LABEL ID=9386>" & GetLocalResourceObject("tcdLedgerDateCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("            <TD>")

Response.Write(mobjValues.DateControl("tcdLedgerDate", CStr(Today),  , GetLocalResourceObject("tcdLedgerDateToolTip")))


Response.Write("</TD>" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("			<TD><LABEL ID=9390>" & GetLocalResourceObject("tcnReferenceCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("			<TD>")


Response.Write(mobjValues.NumericControl("tcnReference", 10, vbNullString,  , GetLocalResourceObject("tcnReferenceToolTip"),  , 0,  ,  ,  ,  , True))


Response.Write("</TD>" & vbCrLf)
Response.Write("			<TD COLSPAN=""3"">&nbsp;</TD>" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("            <TD COLSPAN=""5"" CLASS=""HighLighted""><LABEL ID=0><A NAME=""Relaciones"">" & GetLocalResourceObject("AnchorRelaciones2Caption") & "</A></LABEL></TD>" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("            <TD COLSPAN=""5"" CLASS=""Horline""></TD>" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("        <TABLE WIDTH=""100%"">" & vbCrLf)
Response.Write("            <TR>" & vbCrLf)
Response.Write("                <TD WIDTH=""25%""><LABEL ID=0>" & GetLocalResourceObject("Anchor2Caption") & "</LABEL></TD>" & vbCrLf)
Response.Write("                <TD COLSPAN=3>")


Response.Write(mobjValues.DIVControl("valClient",  , ""))


Response.Write("</TD>" & vbCrLf)
Response.Write("            </TR>" & vbCrLf)
Response.Write("		    <TR>            " & vbCrLf)
Response.Write("                <TD WIDTH=""25%""><LABEL ID=0>" & GetLocalResourceObject("Anchor3Caption") & "</LABEL></TD>" & vbCrLf)
Response.Write("                <TD COLSPAN=3>")


Response.Write(mobjValues.DIVControl("valIntermedia",  , ""))


Response.Write("</TD>" & vbCrLf)
Response.Write("		    </TR>" & vbCrLf)
Response.Write("		    <TR>" & vbCrLf)
Response.Write("                <TD WIDTH=""25%""><LABEL ID=0>" & GetLocalResourceObject("Anchor4Caption") & "</LABEL></TD>" & vbCrLf)
Response.Write("                <TD COLSPAN=3>")


Response.Write(mobjValues.DIVControl("tcnLastMovement",  , ""))


Response.Write("</TD>" & vbCrLf)
Response.Write("            </TR>" & vbCrLf)
Response.Write("		    <TR>" & vbCrLf)
Response.Write("                <TD WIDTH=""25%""><LABEL ID=0>" & GetLocalResourceObject("btnPolicyValuesCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("                <TD COLSPAN=3>")


Response.Write(mobjValues.AnimatedButtonControl("btnPolicyValues", "/VTimeNet/images/btn_ValuesOff.png", GetLocalResourceObject("btnPolicyValuesToolTip"),  , "VerifyDataCover()", False))


Response.Write("</TD>" & vbCrLf)
Response.Write("            </TR>" & vbCrLf)
Response.Write("        </TABLE>" & vbCrLf)
Response.Write("    </TABLE>")

	
	Response.Write(mobjValues.HiddenControl("sPoliType", vbNullString))
	Response.Write(mobjValues.HiddenControl("sCertype", vbNullString))
End Sub

</script>
<%Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage("si001_k")
mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 11.35.15
mobjValues.sSessionID = Session.SessionID
mobjValues.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjValues.sCodisplPage = "si001_k"

'+ Se realiza la validacion de operaciones permitidas al esquema del usuario
mclsSche_Transac = New eSecurity.Secur_sche

'+ Se realiza la validacion de operaciones permitidas al esquema del usuario
If Request.QueryString.Item("DP051_nClaim") = vbNullString And Request.QueryString.Item("SIC001") = vbNullString And Request.QueryString.Item("SIC002") = vbNullString Then
	blnTransac = False
Else
	mintTransaction = 5
	blnTransac = True
End If
%>
<HTML>
<HEAD>
    <META NAME = "GENERATOR" CONTENT = "Microsoft Visual Studio 6.0">
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/Constantes.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tmenu.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/Claim.js"></SCRIPT>

    
    <%Response.Write(mobjValues.StyleSheet())
MobjMenu = New eFunctions.Menues
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 11.35.15
MobjMenu.sSessionID = Session.SessionID
MobjMenu.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility
Response.Write(MobjMenu.MakeMenu("SI001", "SI001_K.aspx", 1, Request.QueryString.Item("sWindowDescript"), Session("sDesMultiCompany"), Session("sSche_code")))
MobjMenu = Nothing
%>
<SCRIPT>    
//- Esta línea guarda la versión procedente de VSS 
   document.VssVersion="$$Revision: 5 $|$$Date: 25/11/03 16:53 $" 

//- Variable que contiene el tipo enumerado para identificar la transacción a ejecutar
	var eClaimTransac = new eClaimTransac()

//% insCancel: se activa al presionar el botón de Cancelar
//----------------------------------------------------------------------------------------------------------------
function insCancel(){
//----------------------------------------------------------------------------------------------------------------
	top.document.location.href = "/VTimeNet/Common/SecWHeader.aspx?sCodispl=SI001&sModule=Claim&sProject=ClaimSeq" 
}

//% insFinish: Ejecuta la acción de Finalizar de la página.
//----------------------------------------------------------------------------------------------------------------
function insFinish(){
//----------------------------------------------------------------------------------------------------------------
	var lintIndex = 0;
	var lstrCodispl = '';
    if(mstrTransaction==eClaimTransac.clngClaimIssue ||
	   mstrTransaction==eClaimTransac.clngClaimRecovery ||
	   mstrTransaction==eClaimTransac.clngClaimAmendment ||
	   mstrTransaction==eClaimTransac.clngApproval ||
	   mstrTransaction==eClaimTransac.clngClaimReopening||
	   mstrTransaction==eClaimTransac.clngClaimRejection ||
       mstrTransaction==eClaimTransac.clngClaimCancellation ||
       mstrTransaction==eClaimTransac.clngClaimRejection ||
       mstrTransaction == eClaimTransac.clngCaratula){
	   	lintIndex   = top.frames['fraFolder'].document.location.href.indexOf("sCodispl=");
		lstrCodispl = top.frames['fraFolder'].location.href.substr(lintIndex+9,5);				
		if (lstrCodispl != 'SI003'){ 			
			ShowPopUp("/VTimeNet/Claim/claimSeq/SI050.aspx?sCodispl=SI050&nAction=392","EndProcess",800,350)
        }
        else	
			return true;
	}
	else
		return true;
}

//% insSelTransaction: Habilita/Deshabilita los controles de la página, dependiendo de la 
//%                    acción que se seleccione.
//-----------------------------------------------------------------------------------------------------------------
function insSelTransaction(){
//-----------------------------------------------------------------------------------------------------------------
    var lintTransac = document.forms["SI001"].elements["cbeTransactio"].value;

	insStateControls(true, true)
		
	if(lintTransac!=0)
	{
	    document.forms["SI001"].elements["valIdCatas"].Parameters.Param1.sValue = lintTransac;
		document.forms["SI001"].elements["tcdEffecdate"].disabled=false;
		$("[name=btn_tcdEffecdate]").prop("disabled", false);	
		document.forms["SI001"].elements["tcnClaim"].disabled=false;
		
//+ Si la transacción es declaración de siniestros
		if(lintTransac==eClaimTransac.clngClaimIssue)
		{
			document.forms["SI001"].elements["cbeBranch"].disabled=false;
			document.forms["SI001"].elements["cbeOffice"].disabled=false;
			document.forms["SI001"].elements["cbeOfficeAgen"].disabled = false;
			$("[name=btncbeOfficeAgen]").prop("disabled", false);
			document.forms["SI001"].elements["cbeAgency"].disabled=false;
			$("[name=btncbeAgency]").prop("disabled", false);
			document.forms["SI001"].elements["tcnPolicy"].disabled = false;
			document.forms["SI001"].elements["tcnCertificat"].disabled=true;
			document.forms["SI001"].elements["tctRequest_nu"].disabled=false;
			document.forms["SI001"].elements["valProduct"].disabled=false;
			document.forms["SI001"].elements["tcdOccurrdat"].disabled=false;
			$("[name=btn_tcdOccurrdat]").prop("disabled", false);

			document.forms["SI001"].elements["valIdCatas"].disabled = false;
			$("[name=btnvalIdCatas]").prop("disabled", false);
//+ Se asigna por defecto la oficina asociada al usuario			
			ShowChangeValues("Office")
		}
		else
		{		
			document.forms["SI001"].elements["cbeBranch"].disabled=true;
			document.forms["SI001"].elements["cbeOffice"].disabled=true;		
			document.forms["SI001"].elements["cbeOfficeAgen"].disabled=true;
			$("[name=btncbeOfficeAgen]").prop("disabled", true);

			document.forms["SI001"].elements["cbeAgency"].disabled = true;
			$("[name=btncbeAgency]").prop("disabled", true);

			document.forms["SI001"].elements["tcnPolicy"].disabled = true;
			document.forms["SI001"].elements["tcnCertificat"].disabled=true;
			document.forms["SI001"].elements["tctRequest_nu"].disabled=true;
			document.forms["SI001"].elements["tcdLedgerDate"].disabled=true;
			$("[name=btn_tcdLedgerDate]").prop("disabled", true);
			document.forms["SI001"].elements["valIdCatas"].disabled = true;
			$("[name=btnvalIdCatas]").prop("disabled", true);

			if(document.forms["SI001"].elements["cbeOffice"].value!="")
				document.forms["SI001"].elements["cbeOffice"].value="";
		}
    		
		switch(lintTransac)
		{

//+Emision de Siniestro
			case '1':
			{
			    document.forms["SI001"].elements["tcnReference"].disabled=false;
			    document.forms["SI001"].elements["tcnClaim"].disabled=true;
			    break;
			}
//+Consultar Siniestro
        
			case eClaimTransac.clngClaimQuery:
			{
			    self.document.forms["SI001"].elements["tcnReference"].disabled=true;
				self.document.forms[0].elements["tcdLedgerDate"].disabled=true;
				$("[name=btn_tcdLedgerDate]").prop("disabled", true);
			    break;
			}
//+Cualquier otra transacción

		    default:
		    {
		        if(lintTransac==eClaimTransac.clngClaimRecovery)
		        {
		            document.forms["SI001"].elements["tctRequest_nu"].disabled=false;
		            document.forms["SI001"].elements["tcdLedgerDate"].disabled=false;
		            $("[name=btn_tcdLedgerDate]").prop("disabled", false);
					document.forms["SI001"].elements["tcdOccurrdat"].disabled=false;
					$("[name=btn_tcdOccurrdat]").prop("disabled", false);
					document.forms["SI001"].elements["valIdCatas"].disabled = false;
					$("[name=btnvalIdCatas]").prop("disabled", false);
		        }
		        else
		        {
		            if(lintTransac==eClaimTransac.clngRecovery ||
		               lintTransac==eClaimTransac.clngClaimPayme)
		            {
						document.forms["SI001"].elements["tcnReference"].disabled=false;
						document.forms["SI001"].elements["tctRequest_nu"].disabled=false;
						document.forms["SI001"].elements["tcdLedgerDate"].disabled=false;
						$("[name=btn_tcdLedgerDate]").prop("disabled", false);
					}
		            else
		            {
		                if(lintTransac==eClaimTransac.clngApproval ||
		                   lintTransac==eClaimTransac.clngClaimAmendment)
		                {
							if (lintTransac==eClaimTransac.clngClaimAmendment){
								document.forms["SI001"].elements["tcdOccurrdat"].disabled=false;
								$("[name=btn_tcdOccurrdat]").prop("disabled", false);
								document.forms["SI001"].elements["valIdCatas"].disabled = false;
								$("[name=btnvalIdCatas]").prop("disabled", false);

							}
							document.forms["SI001"].elements["tctRequest_nu"].disabled=false;
							document.forms["SI001"].elements["tcdLedgerDate"].disabled=false;
							$("[name=btn_tcdLedgerDate]").prop("disabled", false);

		                }
		                else
		                {
							document.forms["SI001"].elements["tctRequest_nu"].disabled=false;
							document.forms["SI001"].elements["tcdLedgerDate"].disabled=false;
							$("[name=btn_tcdLedgerDate]").prop("disabled", false);
						}
					}
				}
				break;
			}
		}
	}
}

//% ShowChangeValues: Se asigna valor a los controles cuyo valor depende de otros controles
//-----------------------------------------------------------------------------------------------------------------
function ShowChangeValues(ControlName){
//-----------------------------------------------------------------------------------------------------------------	
	var strParams; 
	
	with (self.document.forms[0]){
	switch(ControlName)
	{
		case "Claim":
		{
			if(self.document.forms[0].tcnClaim.value!=""){
				insDefValues('ClaimData','nClaim=' + self.document.forms[0].tcnClaim.value + '&dEffecdate=' + self.document.forms[0].tcdEffecdate.value + '&nTransaction=' + self.document.forms[0].cbeTransactio.value + '&nType=1','/VTimeNet/Claim/ClaimSeq')
            }
			break;
		}
		case "Policy":
		{
			if(tcnPolicy.value!=""){
    			strParams = "nPolicy=" + tcnPolicy.value + 
					        "&dEffecdate=" + tcdEffecdate.value + 
					        "&nBranch=" + cbeBranch.value + 
					        "&nProduct=" + valProduct.value + 
					        "&nCertif=" + tcnCertificat.value + 
					        "&nTransaction=" + cbeTransactio.value + 
					        "&nType=2" + 
							"&nOffice=" + cbeOffice.value +
							"&nOfficeAgen=" + cbeOfficeAgen.value +
							"&nAgency=" + cbeAgency.value
				insDefValues('ClaimData',strParams,'/VTimeNet/Claim/ClaimSeq')
            }
            else
            {
                UpdateDiv('valIntermedia',' ');
                UpdateDiv('valStatuspol',' ');
		        UpdateDiv('valClient',' ');
                UpdateDiv('tcnLastMovement',' ');
		    }
			break;
		}
		
		case "Branch":
		{		
		    self.document.forms[0].tcnPolicy.value="";
            UpdateDiv('valIntermedia',' ');
            UpdateDiv('valStatuspol',' ');
		    UpdateDiv('valClient',' ');
            UpdateDiv('tcnLastMovement',' ');
		    break;
		}
		case "valProduct":
		{		
			if(tcnPolicy.value!="")
			{		
    			strParams = "nPolicy=" + tcnPolicy.value + 
					        "&dEffecdate=" + tcdEffecdate.value + 
					        "&nBranch=" + cbeBranch.value + 
					        "&nProduct=" + valProduct.value + 
					        "&nCertif=" + tcnCertificat.value + 
					        "&nTransaction=" + cbeTransactio.value + 
					        "&nType=2" + 
							"&nOffice=" + cbeOffice.value +
							"&nOfficeAgen=" + cbeOfficeAgen.value +
							"&nAgency=" + cbeAgency.value
				insDefValues('ClaimData',strParams,'/VTimeNet/Claim/ClaimSeq')
			}
	    }
	    
		case "Client":
		{	
		    if (tcdOccurrdat.value!="")
		    {	
			if(tcnPolicy.value!="")
			{		
			   strParams = "nPolicy=" + tcnPolicy.value + 
					        "&dEffecdate=" + tcdOccurrdat.value + 
					        "&nBranch=" + cbeBranch.value + 
					        "&nProduct=" + valProduct.value + 
					        "&nCertif=" + tcnCertificat.value + 
					        "&nTransaction=" + cbeTransactio.value + 
					        "&sClient=" + dtcClient.value  
				insDefValues('ClaimClient',strParams,'/VTimeNet/Claim/ClaimSeq')
				break;
			}
		  }
	    }
	    
		case "Certif":
		{	 
		    if (tcdOccurrdat.value!="")
		    { 	
			if(tcnPolicy.value!="")
			{		
    			if (dtcClient.value=="")
    			{
    			strParams = "nPolicy=" + tcnPolicy.value + 
					        "&dEffecdate=" + tcdOccurrdat.value + 
					        "&nBranch=" + cbeBranch.value + 
					        "&nProduct=" + valProduct.value + 
					        "&nCertif=" + tcnCertificat.value + 
					        "&nTransaction=" + cbeTransactio.value + 
					        "&sClient=" + dtcClient.value  
				insDefValues('ClaimCertif',strParams,'/VTimeNet/Claim/ClaimSeq')
				}
				else
				{ strParams = "nPolicy=" + tcnPolicy.value + 
					        "&dEffecdate=" + tcdOccurrdat.value + 
					        "&nBranch=" + cbeBranch.value + 
					        "&nProduct=" + valProduct.value + 
					        "&nCertif=" + tcnCertificat.value + 
					        "&nTransaction=" + cbeTransactio.value + 
					        "&sClient=" + dtcClient.value  
				 insDefValues('ClaimClient',strParams,'/VTimeNet/Claim/ClaimSeq')
				}
			}
		  }	
	    }
	    
	}}
}   

//% insStateControls: Habilita/Deshabilita los controles de la página
//------------------------------------------------------------------------------------------------------------------
function insStateControls(lblnEnabled, lblnClear){
//------------------------------------------------------------------------------------------------------------------
    if(lblnEnabled)
    {
        document.forms["SI001"].elements["tcdEffecdate"].disabled = lblnEnabled;
        $("[name=btn_tcdEffecdate]").prop("disabled", lblnEnabled);
		document.forms["SI001"].elements["cbeOffice"].disabled=lblnEnabled;
		document.forms["SI001"].elements["cbeOfficeAgen"].disabled=lblnEnabled;	
		$("[name=btncbeOfficeAgen]").prop("disabled", lblnEnabled);
		document.forms["SI001"].elements["cbeAgency"].disabled=lblnEnabled;
		$("[name=btncbeAgency]").prop("disabled", lblnEnabled);
		document.forms["SI001"].elements["cbeBranch"].disabled = lblnEnabled;
		document.forms["SI001"].elements["tcnPolicy"].disabled=lblnEnabled;
		document.forms["SI001"].elements["tcnCertificat"].disabled=lblnEnabled;
		document.forms["SI001"].elements["tcnClaim"].disabled=lblnEnabled;
		document.forms["SI001"].elements["tctRequest_nu"].disabled=lblnEnabled;
		document.forms["SI001"].elements["tcnReference"].disabled=lblnEnabled;
		document.forms["SI001"].elements["tcdLedgerDate"].disabled=lblnEnabled;
		document.forms["SI001"].elements["valProduct"].disabled=lblnEnabled;
		$("[name=btnvalProduct]").prop("disabled", lblnEnabled);
    }

    if(lblnClear)
    {
		document.forms["SI001"].elements["tcdEffecdate"].value=GetDateSystem();
		document.forms["SI001"].elements["cbeOffice"].value="";
		document.forms["SI001"].elements["cbeOfficeAgen"].value="";
		document.forms["SI001"].elements["cbeAgency"].value="";				
		document.forms["SI001"].elements["cbeBranch"].value=""
		document.forms["SI001"].elements["tcnPolicy"].value="";
		document.forms["SI001"].elements["tcnCertificat"].value="0";
		document.forms["SI001"].elements["tcnClaim"].value="";
		document.forms["SI001"].elements["tctRequest_nu"].value="";
		document.forms["SI001"].elements["tcnReference"].value="";
		document.forms["SI001"].elements["tcdLedgerDate"].value=GetDateSystem();
		document.forms["SI001"].elements["valProduct"].value="";		
		UpdateDiv('valStatuspol',' ');
		UpdateDiv('valClient',' ');
		UpdateDiv('tcnLastMovement', ' ');
		UpdateDiv('valIntermedia',' ');
		UpdateDiv("valProductDesc","")
		UpdateDiv('valClient_Name',' ')
        UpdateDiv('cbeOfficeAgenDesc','')
        UpdateDiv('cbeAgencyDesc','')

    }
}
//% ShowVerifyData: Habilita/Deshabilita los controles dependientes de la página
//-----------------------------------------------------------------------------------------------------------------
function ShowVerifyData(){
//-------------------------------------------------------------------------------------------------
	with(self.document.forms["SI001"])
	{

		 ShowPolicyData("2", +
			       cbeBranch.value, +
	                       valPolicy.value, +  
			       tcnPolicy.value, + 
  			       tcnCertificat.value)
	}
}

//% VerifyDataCover: Habilita/Deshabilita los controles dependientes de la página
//-----------------------------------------------------------------------------------------------------------------
function VerifyDataCover(){
//-----------------------------------------------------------------------------------------------------------------

	with(self.document.forms["SI001"]){	

        ShowPopUp('/VTimeNet/Common/CovdataSI001.aspx?sCertype=2' + "&nBranch=" + cbeBranch.value + 
														   "&nProduct=" + valProduct.value +
														   "&nPolicy=" + tcnPolicy.value + 
                                                           "&nCertif=" + tcnCertificat.value +
                                                           "&dEffecdate=" + tcdEffecdate.value + 
                                                           "&sClient=" + dtcClient.value, 'PolicyData', 600, 450, "yes", "no", 100, 50)	
	}
}

//% ShowPolicies: Muestra pólizas de un asegurado
//-----------------------------------------------------------------------------------------------------------------
function ShowPolicies(sField){
//-----------------------------------------------------------------------------------------------------------------
	with (self.document.forms["SI001"]) {
		if (sField == "regist") {
			
			if (tctRegister.value != '')
				ShowPopUp('/VTimeNet/Common/PoldataSI001.aspx?sCertype=2' + "&sregist=" + tctRegister.value +
			    													"&sdigit=" + tctDigit.value + 
					     											"&dEffecdate=" + tcdEffecdate.value +
						    										"&sCodispl=SI001", 'PolicyData', 800, 450, "yes", "no", 100, 50)
			
			
		}

		else if (dtcClient.value!='')
            ShowPopUp('/VTimeNet/Common/PoldataSI001.aspx?sCertype=2' + "&nBranch=" + cbeBranch.value + 
			    													"&nProduct=" + valProduct.value + 
				    												"&sClient=" + dtcClient.value +
					     											"&dEffecdate=" + tcdEffecdate.value +
						    										"&sCodispl=SI001", 'PolicyData', 800, 450, "yes", "no", 100, 50)
	}
}

//%SetLedgerDateValue: Obtiene y asigan el valor a la fecha de contabilización
//-------------------------------------------------------------------------------------------------------------------
function SetLedgerDateValue(DateValue)
//-------------------------------------------------------------------------------------------------------------------
{
    var lintTransac = document.forms["SI001"].elements["cbeTransactio"].value;
	
	if (lintTransac==1)
	{
		self.document.forms[0].elements["tcdLedgerDate"].value = DateValue;
		self.document.forms[0].elements["tcdLedgerDate"].disabled = false;
	}
}

//% insStateZone: Habilita/Deshabilita los campos de la ventana
//----------------------------------------------------------------------------------------------------------------
function insStateZone(){
//----------------------------------------------------------------------------------------------------------------
    document.forms["SI001"].elements["cbeTransactio"].disabled=false;
}
</SCRIPT>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" ID="FORM" NAME="SI001" ACTION="valClaimSeq.aspx?sMode=1">
	<P>&nbsp;</P>
<%
If Request.QueryString.Item("sConfig") = "InSequence" Then
	Call LoadPageInSequence()
Else
	'Call LoadHeader()
    Call LoadHeader()
End If
mobjValues = Nothing
mclsSche_Transac = Nothing
%>
</FORM>
</BODY>
</HTML>

<%'+ Se invoca la secuencia de siniestros en modo consulta si es llamada desde la SI051.
If CStr(Session("DP051_nClaim")) <> vbNullString Or Request.QueryString.Item("SIC001") <> vbNullString Or Request.QueryString.Item("SIC002") <> vbNullString Then
	Response.Write("<SCRIPT>ShowChangeValues(""Claim"");")
	Response.Write("self.document.forms[0].cbeTransactio.disabled=true;")
	Response.Write("</SCRIPT>")
	Session("DP051_nClaim") = vbNullString
End If
%>
<%'^Begin Footer Block VisualTimer Utility 1.1 7/4/03 11.35.15
Call mobjNetFrameWork.FinishPage("si001_k")
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>




