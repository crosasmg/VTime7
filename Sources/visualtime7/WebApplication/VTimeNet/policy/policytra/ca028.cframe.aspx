<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="ePolicy" %>
<script language="VB" runat="Server">
'^Begin Header Block VisualTimer Utility 1.1 7/4/03 10.31.19
Dim mobjNetFrameWork As eNetFrameWork.Layout
'~End Header Block VisualTimer Utility

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'- Objeto para el manejo de las rutinas genéricas
Dim mobjMenu As eFunctions.Menues
Dim mclsPolicy_his As Object
Dim mclsPremium As Object

'- Se define variable para almacenar QueryString
Dim lstrQueryString As String

Dim lstrKey As String

'+ Vriable para ser usadas si la ventana se encuentra dentro de la secuencia
Dim mblnError As Boolean


'%insPreCA028: Esta función se encarga de cargar los datos en la forma "Folder" 
'--------------------------------------------------------------------------------------------
Private Sub insPreCA028()
	'--------------------------------------------------------------------------------------------
	Dim lclsTDetail_pre As Object
	Dim lintCount As Object
	Dim lintIndex As Object
	Dim lstrType_detai As Object
	Dim lintCodeItem As Object
	Dim mblnSequence As Boolean
	Dim mstrFrameLocation As Object
	
	'- Se define las variables para el manejo del Grid de la ventana
	Dim mclsTDetail_pre As ePolicy.TDetail_pre
	
	mclsTDetail_pre = New ePolicy.TDetail_pre
	
	mblnSequence = False
	
	'+ Si se invoca desde la secuencia de Cartera
	If (Session("nTransaction") = eCollection.Premium.PolTransac.clngPolicyAmendment Or Session("nTransaction") = eCollection.Premium.PolTransac.clngTempPolicyAmendment Or Session("nTransaction") = eCollection.Premium.PolTransac.clngCertifAmendment Or Session("nTransaction") = eCollection.Premium.PolTransac.clngTempCertifAmendment Or Session("nTransaction") = eCollection.Premium.PolTransac.clngPolicyQuotAmendent Or Session("nTransaction") = eCollection.Premium.PolTransac.clngCertifQuotAmendent Or Session("nTransaction") = eCollection.Premium.PolTransac.clngPolicyPropAmendent Or Session("nTransaction") = eCollection.Premium.PolTransac.clngCertifPropAmendent) And Request.QueryString.Item("sOnSeq") = "1" Then
		mblnSequence = True
	End If
	
	Call mclsTDetail_pre.insPreCA028(Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Request.QueryString.Item("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Request.QueryString.Item("dExpirdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Request.QueryString.Item("nTypeReceipt"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nReceipt"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nCurrency"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("dIssuedat"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Request.QueryString.Item("nTratypei"), eFunctions.Values.eTypeData.etdDouble), Request.QueryString.Item("sOrigReceipt"), mblnSequence)
	
	mblnError = mclsTDetail_pre.bError
	If mclsTDetail_pre.mclsPolicy.sPolitype <> vbNullString Then
		Session("sPoliType") = mclsTDetail_pre.mclsPolicy.sPolitype
	End If
	
	Response.Write(mobjValues.HiddenControl("cbeCertype", Session("sCertype")))
	Response.Write(mobjValues.HiddenControl("cbeBranch", Session("nBranch")))
	Response.Write(mobjValues.HiddenControl("valProduct", Session("nProduct")))
	Response.Write(mobjValues.HiddenControl("tcnPolicy", Session("nPolicy")))
	Response.Write(mobjValues.HiddenControl("tcnCertif", Session("nCertif")))
	
	If Not mblnSequence Then
		
Response.Write("    " & vbCrLf)
Response.Write("    <P ALIGN=""Center"">" & vbCrLf)
Response.Write("    <LABEL ID=41097><A HREF=""#Datos del recibo""> " & GetLocalResourceObject("AnchorDatos del reciboCaption") & "</A></LABEL>" & vbCrLf)
Response.Write("    <TABLE WIDTH=""100%"">" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("            <TD COLSPAN=""7"" CLASS=""HighLighted""><LABEL ID=41098>" & GetLocalResourceObject("AnchorCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("            <TD CLASS=""HorLine"" COLSPAN=""7""></TD>" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("			<TD COLSPAN=""2"" CLASS=""HighLighted""><LABEL ID=0>" & GetLocalResourceObject("Anchor2Caption") & "</LABEL></TD>" & vbCrLf)
Response.Write("			<TD WIDTH=""5%"">&nbsp;</TD>" & vbCrLf)
Response.Write("			<TD><LABEL ID=13765>" & GetLocalResourceObject("tcnCapital_policyCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("            <TD>")


Response.Write(mobjValues.NumericControl("tcnCapital_policy", 18, CStr(mclsTDetail_pre.mclsPolicy.nCapital),  , GetLocalResourceObject("tcnCapital_policyToolTip"), True, 6, True))


Response.Write("</TD>" & vbCrLf)
Response.Write("			<TD><LABEL ID=13770>" & GetLocalResourceObject("tcnNetPremium_policyCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("            <TD>")


Response.Write(mobjValues.NumericControl("tcnNetPremium_policy", 18, CStr(mclsTDetail_pre.mclsCertificat.nPremium),  , GetLocalResourceObject("tcnNetPremium_policyToolTip"), True, 6, True))


Response.Write("</TD>" & vbCrLf)
Response.Write("		</TR>" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("            <TD CLASS=""HorLine"" COLSPAN=""2""></TD>" & vbCrLf)
Response.Write("            <TD COLSPAN=""5""></TD>" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("            <TD COLSPAN=""2"">" & vbCrLf)
Response.Write("                <LABEL ID=0>" & GetLocalResourceObject("tcdStartDate_policyCaption") & "</LABEL>")


Response.Write(mobjValues.DateControl("tcdStartDate_policy", CStr(mclsTDetail_pre.mclsCertificat.dStartdate),  , GetLocalResourceObject("tcdStartDate_policyToolTip"), True))


Response.Write("&nbsp;<BR>" & vbCrLf)
Response.Write("                <LABEL ID=0> " & GetLocalResourceObject("tcdExpirdate_policyCaption") & "</LABEL>")


Response.Write(mobjValues.DateControl("tcdExpirdate_policy", CStr(mclsTDetail_pre.mclsCertificat.dExpirdat),  , GetLocalResourceObject("tcdExpirdate_policyToolTip"), True))


Response.Write("</TD>" & vbCrLf)
Response.Write("            <TD>&nbsp;</TD>" & vbCrLf)
Response.Write("            <TD><LABEL ID=41099>" & GetLocalResourceObject("dtcClient_policyCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("            <TD colspan=3>")


Response.Write(mobjValues.ClientControl("dtcClient_policy", mclsTDetail_pre.mclsCertificat.sClient,  , GetLocalResourceObject("dtcClient_policyToolTip"),  ,  , "lblCliename", True, True,  ,  ,  ,  ,  , True))


Response.Write("</TD>" & vbCrLf)
Response.Write("		</TR>" & vbCrLf)
Response.Write("    </TABLE>" & vbCrLf)
Response.Write("")

		
	End If
	
	
Response.Write("" & vbCrLf)
Response.Write("	<TABLE WIDTH=""100%"" border = 0>" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("            <TD COLSPAN=""5"" CLASS=""HighLighted""><LABEL ID=41098>" & GetLocalResourceObject("Anchor3Caption") & "</LABEL></TD>" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("            <TD CLASS=""HorLine"" COLSPAN=""5""></TD>" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("			<TD COLSPAN=""2"" CLASS=""HighLighted""><LABEL ID=0>" & GetLocalResourceObject("Anchor2Caption") & "</LABEL></TD>" & vbCrLf)
Response.Write("			<TD>&nbsp;</TD>" & vbCrLf)
Response.Write("			<TD COLSPAN=""2"" CLASS=""HighLighted""><LABEL ID=0>" & GetLocalResourceObject("Anchor5Caption") & "</LABEL></TD>" & vbCrLf)
Response.Write("        </TR>                " & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("			<TD COLSPAN=""2"" CLASS=""HorLine""></TD>" & vbCrLf)
Response.Write("			<TD></TD>" & vbCrLf)
Response.Write("			<TD COLSPAN=""2"" CLASS=""HorLine""></TD>" & vbCrLf)
Response.Write("        </TR>                " & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("			<TD><LABEL ID=13755>" & GetLocalResourceObject("tcdStartDateRCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("			<TD>")


Response.Write(mobjValues.DateControl("tcdStartDateR", CStr(mclsTDetail_pre.dEffecdate),  , GetLocalResourceObject("tcdStartDateRToolTip"),  ,  ,  , "insUpdFrameSource(""CA028"");", False, 1))


Response.Write("</TD>" & vbCrLf)
Response.Write("			<TD>&nbsp;</TD>" & vbCrLf)
Response.Write("			<TD COLSPAN=""1"">")


Response.Write(mobjValues.OptionControl(41101, "optType", GetLocalResourceObject("optType_1Caption"), CStr(mclsTDetail_pre.nTypeReceipt), "1", "changeValuesField(""CheckType"", this)",  , 3, GetLocalResourceObject("optType_1ToolTip")))


Response.Write("</TD>" & vbCrLf)
Response.Write("			<TD COLSPAN=""1"">")


Response.Write(mobjValues.OptionControl(41102, "optType", GetLocalResourceObject("optType_2Caption"), CStr(mclsTDetail_pre.nTypeReceipt - 1), "2", "changeValuesField(""CheckType"", this)",  , 4, GetLocalResourceObject("optType_2ToolTip")))


Response.Write("</TD>" & vbCrLf)
Response.Write("		</TR>" & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("            <TD><LABEL ID=13746>" & GetLocalResourceObject("tcdExpirDateRCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("            <TD>")


Response.Write(mobjValues.DateControl("tcdExpirDateR", CStr(mclsTDetail_pre.mclsPolicy.dNextReceip),  , GetLocalResourceObject("tcdExpirDateRToolTip"),  ,  ,  ,  , True, 2))


Response.Write("</TD>" & vbCrLf)
Response.Write("            <TD COLSPAN=""3"">&nbsp;</TD>" & vbCrLf)
Response.Write("		</TR>" & vbCrLf)
Response.Write("        </TR>        " & vbCrLf)
Response.Write("            <TD COLSPAN=""2"">&nbsp;</TD>" & vbCrLf)
Response.Write("            <TD>&nbsp;</TD>")

	
	If Not mblnSequence Then
		Response.Write("<TD COLSPAN='2'>" & mobjValues.CheckControl("chkAdjust", GetLocalResourceObject("chkAdjustCaption"), "2", "1", "changeValuesField(""CheckAdjust"", this);") & "</TD>")
	Else
		Response.Write("<TD COLSPAN='2'>&nbsp;</TD>" & vbCrLf)
	End If
	
Response.Write("  " & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("			<TD><LABEL ID=13752>" & GetLocalResourceObject("tcnReceiptCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("			<TD>")

	Response.Write(mobjValues.NumericControl("tcnReceipt", 8, CStr(mclsTDetail_pre.nReceipt),  , GetLocalResourceObject("tcnReceiptToolTip"),  , 0,  ,  ,  , "changeValuesField(""Receipt"", this)", True, 5))
	Response.Write(" ")
	If mblnSequence Then
		Response.Write(mobjValues.CheckControl("chkDelReceipt", GetLocalResourceObject("chkDelReceiptCaption"),  , "1",  , mblnError,  , GetLocalResourceObject("chkDelReceiptToolTip")))
	End If
	
Response.Write("" & vbCrLf)
Response.Write("			</TD>" & vbCrLf)
Response.Write("    		<TD>&nbsp;</TD>")

	If Not mblnSequence Then
Response.Write("" & vbCrLf)
Response.Write("    		<TD><LABEL ID=13752>" & GetLocalResourceObject("tcnAdjReceiptCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("			<TD>")


Response.Write(mobjValues.NumericControl("tcnAdjReceipt", 8, CStr(eRemoteDB.Constants.intNull),  , GetLocalResourceObject("tcnAdjReceiptToolTip"),  , 0,  ,  ,  , "changeValuesField(""AdjReceipt"", this)", True, 5))


Response.Write("</TD>")

	Else
Response.Write("" & vbCrLf)
Response.Write("            <TD COLSPAN=""2"">&nbsp;</TD>")

	End If
Response.Write("			" & vbCrLf)
Response.Write("		</TR>" & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("            <TD><LABEL ID=13745>" & GetLocalResourceObject("cbeCurrencyCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("            <TD>")

	
	mobjValues.BlankPosition = False
	With mobjValues.Parameters
		.Add("sCertype", Session("sCertype"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Add("nBranch", Session("nBranch"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Add("nProduct", Session("nProduct"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Add("nPolicy", Session("nPolicy"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Add("nCertif", Session("nCertif"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Add("dEffecdate", Today, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	End With
	Response.Write(mobjValues.PossiblesValues("cbeCurrency", "tabCurren_pol", eFunctions.Values.eValuesType.clngComboType, CStr(mclsTDetail_pre.nCurrency), True,  ,  ,  ,  ,  , mblnError,  , GetLocalResourceObject("cbeCurrencyToolTip"),  , 6))
	
Response.Write("" & vbCrLf)
Response.Write("			</TD>" & vbCrLf)
Response.Write("    		<TD>&nbsp;</TD>")

	If Not mblnSequence Then
Response.Write("" & vbCrLf)
Response.Write("    		<TD><LABEL ID=13752>" & GetLocalResourceObject("tcnAdjAmountCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("			<TD>")


Response.Write(mobjValues.NumericControl("tcnAdjAmount", 8, CStr(eRemoteDB.Constants.intNull),  , GetLocalResourceObject("tcnAdjAmountToolTip"),  , 6,  ,  ,  , "changeValuesField(""AdjAmount"", this)", True, 5, True, False))


Response.Write("</TD>")

	Else
Response.Write("" & vbCrLf)
Response.Write("            <TD COLSPAN=""2"">&nbsp;</TD>")

	End If
Response.Write("" & vbCrLf)
Response.Write("			" & vbCrLf)
Response.Write("        </TR>                " & vbCrLf)
Response.Write("        </TR>        " & vbCrLf)
Response.Write("            <TD><LABEL ID=13754>" & GetLocalResourceObject("cbeSourceCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("            <TD>")

	
	mobjValues.TypeList = 2
	mobjValues.List = "13"
	Response.Write(mobjValues.PossiblesValues("cbeSource", "Table24", 1, CStr(mclsTDetail_pre.nTratypei),  ,  ,  ,  ,  ,  , mblnError Or mblnSequence,  , GetLocalResourceObject("cbeSourceToolTip"),  , 8))
	
Response.Write("" & vbCrLf)
Response.Write("			</TD>" & vbCrLf)
Response.Write("			<TD>&nbsp;</TD>" & vbCrLf)
Response.Write("			<TD><LABEL ID=13768>" & GetLocalResourceObject("tcdIssueDateCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("            <TD>")


Response.Write(mobjValues.DateControl("tcdIssueDate", CStr(mclsTDetail_pre.dIssuedat),  , GetLocalResourceObject("tcdIssueDateToolTip"),  ,  ,  , "changeValuesField(""IssueDate"",this)", mblnError, 7))


Response.Write("</TD>" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("<!--Se oculta campo para evitar confusiones con Recibo a ajustar " & vbCrLf)
Response.Write("    En su reemplazo se ubica el campo de ordenes de pago -->" & vbCrLf)
Response.Write("<!--            <TD><LABEL ID=13748>" & GetLocalResourceObject("tctOrigReceiptCaption") & "</LABEL></TD> -->" & vbCrLf)
Response.Write("<!--            <TD>< %= mobjValues.TextControl(""tctOrigReceipt"",20,mclsTDetail_pre.sOrigReceipt,, GetLocalResourceObject(""tctOrigReceiptToolTip""),,,,,mblnError Or mclsTDetail_pre.mclsPolicy.sBussityp = ""1"",9) % > </TD>-->" & vbCrLf)
Response.Write("            <TD><LABEL ID=13850>" & GetLocalResourceObject("cbeTypePayCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("            <TD>")


Response.Write(mobjValues.PossiblesValues("cbeTypePay", "Table5527", eFunctions.Values.eValuesType.clngComboType, Request.QueryString.Item("nTypePay"),  ,  ,  ,  ,  ,  , True,  , GetLocalResourceObject("cbeTypePayToolTip")))


Response.Write("</TD>" & vbCrLf)
Response.Write("            <TD COLSPAN=3>&nbsp;</TD>" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("	</TABLE>")

	
	lstrKey = mclsTDetail_pre.mcolTDetail_pre.sKey(Session("nUsercode"), Session("SessionID"))
	
	
	Response.Write(mobjValues.HiddenControl("hddAdjAmount", "0"))
	Response.Write(mobjValues.HiddenControl("hddClient_policy", mclsTDetail_pre.mclsCertificat.sClient))
	Response.Write(mobjValues.HiddenControl("hddKey", lstrKey))
	
	
Response.Write("" & vbCrLf)
Response.Write("    " & vbCrLf)
Response.Write("	<IFRAME NAME=""fraGrid"" SRC=""/VTimeNet/Common/Blank.htm"" WIDTH=""100%"" HEIGHT=""52%"" SCROLLING=AUTO FRAMEBORDER=""0"">" & vbCrLf)
Response.Write("	</IFRAME>" & vbCrLf)
Response.Write("" & vbCrLf)
Response.Write("" & vbCrLf)
Response.Write("<SCRIPT>" & vbCrLf)
Response.Write("    insUpdFrameSource(""CA028"");" & vbCrLf)
Response.Write("</" & "SCRIPT>" & vbCrLf)
Response.Write("")

	
	Response.Write(mobjValues.BeginPageButton)
	
	If Request.QueryString.Item("Type") <> "PopUp" And Not mblnSequence Then
		If CStr(Session("dEffecdate")) <> vbNullString Then
			If Request.QueryString.Item("sCodisplOrig") <> "CA033_CA028" Then
				
Response.Write("" & vbCrLf)
Response.Write("	<TABLE WIDTH=100%>" & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("			<TD CLASS=""HORLINE"" COLSPAN=""3""></TD>" & vbCrLf)
Response.Write("		</TR>" & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("			<TD WIDTH=5%>")


Response.Write(mobjValues.ButtonAbout("CA028"))


Response.Write("</TD>" & vbCrLf)
Response.Write("			<TD WIDTH=5%>")


Response.Write(mobjValues.ButtonHelp("CA028"))


Response.Write("</TD>" & vbCrLf)
Response.Write("			<TD ALIGN=RIGHT>")


Response.Write(mobjValues.ButtonAcceptCancel("EnabledControl()",  , True))


Response.Write("</TD>" & vbCrLf)
Response.Write("		</TR>" & vbCrLf)
Response.Write("	</TABLE>")

				
			End If
		End If
	End If
	
	If Request.QueryString.Item("sCodisplOrig") = "CA033_CA028" Then
		
Response.Write("" & vbCrLf)
Response.Write("	<TABLE WIDTH=100%>" & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("			<TD CLASS=""HORLINE"" COLSPAN=""3""></TD>" & vbCrLf)
Response.Write("		</TR>" & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("			<TD WIDTH=5%>")


Response.Write(mobjValues.ButtonAbout("CA028"))


Response.Write("</TD>" & vbCrLf)
Response.Write("			<TD WIDTH=5%>")


Response.Write(mobjValues.ButtonHelp("CA028"))


Response.Write("</TD>" & vbCrLf)
Response.Write("			<TD ALIGN=RIGHT>")


Response.Write(mobjValues.ButtonAcceptCancel())


Response.Write("</TD>" & vbCrLf)
Response.Write("		</TR>" & vbCrLf)
Response.Write("	</TABLE>" & vbCrLf)
Response.Write("	")

		
	End If
	
	mclsTDetail_pre = Nothing
	
End Sub

</script>
<%Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage("ca028")
mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.31.19
mobjValues.sSessionID = Session.SessionID
mobjValues.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjValues.sCodisplPage = Request.QueryString.Item("sCodispl")

lstrQueryString = "&sCertype=" & Request.QueryString.Item("sCertype") & "&nBranch=" & Request.QueryString.Item("nBranch") & "&nProduct=" & Request.QueryString.Item("nProduct") & "&nPolicy=" & Request.QueryString.Item("nPolicy") & "&nCertif=" & Request.QueryString.Item("nCertif") & "&dNullDate=" & Request.QueryString.Item("dNullDate") & "&sNullReceipt=" & Request.QueryString.Item("sNullReceipt") & "&sOptReceipt=" & Request.QueryString.Item("sOptReceipt") & "&nExeMode=" & Request.QueryString.Item("nExeMode") & "&sExeReport=" & Request.QueryString.Item("sExeReport") & "&nAgency=" & Request.QueryString.Item("nAgency") & "&sCodisplOrig=" & Request.QueryString.Item("sCodisplOrig") & "&sOnSeq=" & Request.QueryString.Item("sOnSeq")

'+ Cuando es llamada desde la CA033 se agregan variables al QueryString	
If Request.QueryString.Item("sCodisplOrig") = "CA033_CA028" Then
	lstrQueryString = lstrQueryString & "&sCodispl=" & Request.QueryString.Item("sCodispl") & "&sPopUp=1"
End If

'- Se crean las instancias de las variables modulares
With Server
	mobjMenu = New eFunctions.Menues
	'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.31.19
	mobjMenu.sSessionID = Session.SessionID
	mobjMenu.nUsercode = Session("nUsercode")
	'~End Body Block VisualTimer Utility
End With


%>	
<SCRIPT>
//- Variable para el control de versiones
    document.VssVersion="$$Revision: 8 $|$$Date: 20/10/04 16:18 $|$$Author: Nvaplat7 $"
 

//% insSelected: se controla la acción sobre la columna SEL
//-------------------------------------------------------------------------------------------
function insSelected(Field){
//-------------------------------------------------------------------------------------------
    var lstrParameters;
    var nPrem_det;
    var nPrem_det_old;
    with(Field){
		nPrem_det = (marrArray[value].cbeType==1)?3:2;
		nPrem_det_old = (marrArray[value].cbeType==1)?nPrem_det:'';
		lstrParameters = 'sType_detai=' + marrArray[value].cbeType + '&nCode=' + marrArray[value].tcnCodeItem + 
		                 '&sClient=' + marrArray[value].dtcClient + '&nBill_item=' + marrArray[value].hddBill_item + 
		                 '&nBranch_est=' + marrArray[value].hddBranch_est + '&nBranch_led=' + marrArray[value].hddBranch_led + 
		                 '&nBranch_rei=' + marrArray[value].hddBranch_rei + '&nCapital=' + marrArray[value].tcnCapital + 
		                 '&nCommi_rate=' + marrArray[value].tcnCommi_rate + '&nCommission=' + marrArray[value].tcnCommission + 
		                 '&nModulec=' + marrArray[value].hddModulec + '&nPremiumA=' + marrArray[value].tcnPremiumA + 
		                 '&nPremiumE=' + marrArray[value].tcnPremiumE + '&sAddsuini=' + marrArray[value].hddAddsuini + 
		                 '&sTypeReceipt=' + self.document.forms[0].hddType.value + '&sAddTax=' + marrArray[value].hddAddTax +
		                 '&dEffecdate=' + self.document.forms[0].tcdIssueDate.value + 
		                 '&nPrem_det=' + nPrem_det + '&nPrem_det_old=' + nPrem_det_old;
		if(checked)
			EditRecord(value, nMainAction, 'Update');
		else
			EditRecord(value, nMainAction, 'Del', lstrParameters);
	}
}
//--------------------------------------------------------------------------------------------
function insUpdAdjAmount() {
//--------------------------------------------------------------------------------------------
    with(self.document.forms[0]){
//    alert('convert:' + insConvertNumber(tcnAdjAmount.value))
        if(optType[1].checked)
            hddAdjAmount.value = VTFormat(-insConvertNumber(tcnAdjAmount.value), '','', '', 6, true);
        else
            if(optType[0].checked)
                hddAdjAmount.value = VTFormat(insConvertNumber(tcnAdjAmount.value), '','', '', 6, true);
//        alert('valor : ' + hddAdjAmount.value);
    }

}

//% changeValuesField: se controla el cambio de valor de los campos de la ventana
//--------------------------------------------------------------------------------------------
function changeValuesField(Option, Field){
//--------------------------------------------------------------------------------------------
	var lstrQueryString1; 
	var lstrCodispl;

<%
Response.Write("lstrQueryString1 = '" & lstrQueryString & "';")
Response.Write("lstrCodispl = '" & Request.QueryString.Item("sCodispl") & "';")
%>	
	
    switch(Option){
        case "Receipt":
//+ Se obtiene y asigna el número de recibo de forma automática
/*            if(Field.value=="")
				if(self.document.forms[0].hddReceipt=="")
					insDefValues('Receipt', "nReceipt=" + Field.value,'/VTimeNet/Policy/PolicyTra/');
				else
					Field.value=self.document.forms[0].hddReceipt.value;
*/					
            break;

        case "AdjReceipt":
//+ Se recuperan las fechas del recibo a ajustar
            if(Field.value!='')
				insDefValues('AdjReceipt', "nAdjReceipt=" + Field.value,'/VTimeNet/Policy/PolicyTra/');
            break;

        case "IssueDate":
			with(self.document.forms[0]){
				self.document.location.href = "CA028.aspx?sCodispl=<%=Request.QueryString.Item("sCodispl")%>&nMainAction=304&dEffecdate=" + tcdStartDateR.value + 
				    "&dExpirDate=" + tcdExpirDateR.value + 
				    "&nTypeReceipt=" + (optType[0].checked?optType[0].value:optType[1].value) + 
				    "&dIssuedat=" + tcdIssueDate.value + 
				    "&nCurrency=" + cbeCurrency.value + 
				    "&nTratypei=" + cbeSource.value + 
				    "&sOrigReceipt=" + tctOrigReceipt.value + 
				    lstrQueryString1
			}
			break;
			
        case "CheckAdjust":
            with(self.document.forms[0]){
                cbeSource.value = (Field.checked?'3':'0'); //3-Modificacion 
                cbeSource.disabled = Field.checked;
                tcnAdjReceipt.disabled = !Field.checked;
                tcnAdjAmount.disabled = !Field.checked;
                if (tcnAdjReceipt.disabled) tcnAdjReceipt.value = '';
                if (tcnAdjAmount.disabled) tcnAdjAmount.value = '';
                
			}
			break;

        case "CheckType":
            with(self.document.forms[0]){
                cbeTypePay.disabled = !optType[1].checked;
                if(cbeTypePay.disabled) cbeTypePay.value = 0;
			}
			insUpdAdjAmount();
			break;

        case "AdjAmount":
            insUpdAdjAmount();
            if(Field.value!='') 
                insUpdFrameSource(sCodispl);
			break;
   }
}

function insUpdFrameSource(sCodispl) {

    var sDelReceipt

    with(self.document.forms[0]){
        if(chkDelReceipt='undefined')   
            sDelReceipt = '0';
        else
            sDelReceipt = (chkDelReceipt.checked?'1':'2');

	    self.document.frames['fraGrid'].location = 
	        "CA028Frame.aspx?sCodispl=" + sCodispl +
	                    "&dEffecdate=" + tcdStartDateR.value +  
	                    "&nCurrency=" + cbeCurrency.value + 
	                    "&sNewData=" + "1" +
	                    "&sKey=" + hddKey.value +  
	                    "&sAdjust=" + (chkAdjust.checked?'1':'2') +
	                    "&nAdjReceipt=" + tcnAdjReceipt.value +
	                    "&nAdjAmount=" + hddAdjAmount.value +
	                    "&sCertype=" + cbeCertype.value +
						"&nBranch=" + cbeBranch.value + 
						"&nProduct=" + valProduct.value + 
						"&nPolicy=" + tcnPolicy.value +
						"&nCertif=" + tcnCertif.value +
						"&dNullDate=" + '' +
						"&sNullReceipt=" + sDelReceipt +
						"&sOptReceipt=" + (optType[0].checked?'1':'2') +
						"&nExeMode=" + "1" +
						"&sExeReport=" + "2" +
						"&nAgency=" + 
						"&sOnSeq=";
    }

}

</SCRIPT>
<HTML>
<HEAD>




<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
    <META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>
    <%
Response.Write(mobjValues.StyleSheet())

'+ Si Session("dEffecdate") está vacío significa que se está trabajando desde el menú 
'+ principal del sistema, y la ventana no se muestra como ventana PopUp

If CStr(Session("dEffecdate")) <> vbNullString Then
	If Request.QueryString.Item("Type") <> "PopUp" Then
		Response.Write("<SCRIPT>var nMainAction=304</SCRIPT>")
		'+ Si la ventana se está mostrando en la secuencia de la póliza 
		If Request.QueryString.Item("sOnSeq") = "1" Then
			Response.Write(mobjMenu.setZone(2, Request.QueryString.Item("sCodispl"), Request.QueryString.Item("sWindowDescript"), CShort(Request.QueryString.Item("nWindowTy"))))
		End If
	End If
Else
	If Request.QueryString.Item("Type") <> "PopUp" Then
		With Response
			If Request.QueryString.Item("sCodisplOrig") <> "CA033_CA028" Then
				.Write(mobjMenu.setZone(2, Request.QueryString.Item("sCodispl"), Request.QueryString.Item("sWindowDescript"), CShort(Request.QueryString.Item("nWindowTy"))))
			End If
			.Write("<SCRIPT>var nMainAction=304</SCRIPT>")
		End With
	End If
End If
mobjMenu = Nothing
%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">

<FORM METHOD="post" ID="FORM" NAME="CA028" ACTION="ValPolicyTra.aspx?sTime=1<%=lstrQueryString%>">
<%
Response.Write(mobjValues.ShowWindowsName(Request.QueryString.Item("sCodispl"), Request.QueryString.Item("sWindowDescript")))

Call insPreCA028()

If Request.QueryString.Item("Type") <> "PopUp" And CStr(Session("dEffecdate")) <> vbNullString Then
	Response.Write("<SCRIPT>self.document.forms[0].action='ValPolicyTra.aspx?sCodispl=" & Request.QueryString.Item("sCodispl") & "&sPopUp=1'</SCRIPT>")
End If

If mblnError Then
	Response.Write("<SCRIPT>alert(""Err. 60583: " & eFunctions.Values.GetMessage(60583) & """);</SCRIPT>")
End If

mobjValues = Nothing
mclsPolicy_his = Nothing
mclsPremium = Nothing
%>
</FORM>
</BODY>
</HTML>

<%'^Begin Footer Block VisualTimer Utility 1.1 7/4/03 10.31.19
Call mobjNetFrameWork.FinishPage("ca028")
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer
%>





