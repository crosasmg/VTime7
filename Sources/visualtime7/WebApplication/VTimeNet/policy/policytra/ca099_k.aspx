<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eSecurity" %>
<script language="VB" runat="Server">
'^Begin Header Block VisualTimer Utility 1.1 7/4/03 10.31.20
Dim mobjNetFrameWork As eNetFrameWork.Layout
'~End Header Block VisualTimer Utility

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'- Objeto para el manejo de las rutinas genéricas
Dim mobjMenu As eFunctions.Menues
'~End Body Block VisualTimer Utility

'- Valores parametros
Dim mstrDocType As String
Dim mstrOperat As String
Dim mstrAction As String
Dim mstrOrigin As String
Dim mstrBranch As String
Dim mstrProduct As String
Dim mstrEffecdate As String
Dim mstrBrancht As String
Dim mstrdateCont As String
Dim mstrQs As String
Dim mclsSche_Transac As eSecurity.Secur_sche


'% LoadHeader: Configura los campos de la cabecera y llama a pagina de detalle
'-----------------------------------------------------------------------------------
Private Sub LoadHeader()
	'-----------------------------------------------------------------------------------
	
Response.Write("" & vbCrLf)
Response.Write("    <TABLE WIDTH=""100%"" BORDER=0>" & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("        ")

	If Request.QueryString.Item("sCodispl_orig") = "CA099C" Then
Response.Write("" & vbCrLf)
Response.Write("            <TD><DIV ID=""divTipocot""><LABEL ID=0>" & GetLocalResourceObject("AnchorCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("		")

	Else
Response.Write("" & vbCrLf)
Response.Write("            <TD><DIV ID=""divTipocot""><LABEL ID=0>" & GetLocalResourceObject("valOriginCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("		")

	End If
Response.Write("" & vbCrLf)
Response.Write("            <TD><DIV ID=""divvalOrigin"">")


Response.Write(mobjValues.PossiblesValues("valOrigin", "Table5580", eFunctions.Values.eValuesType.clngComboType, mstrOrigin,  , True))


Response.Write("</TD>" & vbCrLf)
Response.Write("            <TD>&nbsp;</TD>" & vbCrLf)
Response.Write("            <TD><DIV ID=""divOperacion""><LABEL ID=0>" & GetLocalResourceObject("cbeOperatCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("			<TD><DIV ID=""divcbeOperat""><LABEL ID=0>")


Response.Write(mobjValues.PossiblesValues("cbeOperat", "Table7043", eFunctions.Values.eValuesType.clngComboType, mstrOperat,  , True,  ,  ,  , "ChangeValues(this)", True))


Response.Write("</LABEL></TD>" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("    </TABLE>" & vbCrLf)
Response.Write("    <SCRIPT>" & vbCrLf)
Response.Write("    var nElem = """)


Response.Write(mstrOrigin)


Response.Write("""" & vbCrLf)
Response.Write("    var nOper = '")


Response.Write(mstrOperat)


Response.Write("'" & vbCrLf)
Response.Write("//+Asigna a area de detalle la transaccion que corresponda" & vbCrLf)
Response.Write("//+Si el tipo corresponde a: 'Anulacion', 'Saldado', " & vbCrLf)
Response.Write("//+ 'Prorrogado', 'Rescate', 'Prestamo'" & vbCrLf)
Response.Write("    if ((nElem==""4"") ||" & vbCrLf)
Response.Write("	    (nElem==""6"") ||" & vbCrLf)
Response.Write("	    (nElem==""7"") ||" & vbCrLf)
Response.Write("	    (nElem==""8"") ||" & vbCrLf)
Response.Write("	    (nElem==""9"") ||" & vbCrLf)
Response.Write("	    (nElem==""10"")){" & vbCrLf)
Response.Write("	    //self.document.forms[0].cbeOperat.value" & vbCrLf)
Response.Write("	       if (nOper != 1)" & vbCrLf)
Response.Write("	           top.fraFolder.document.location = 'CA767.aspx?sCodispl=CA767' + '")


Response.Write(mstrQs)


Response.Write("';" & vbCrLf)
Response.Write("	}" & vbCrLf)
Response.Write("//+Si el tipo corresponde a: 'Emisión', 'Modificacion', 'Renovacion', 'Rehabilitación'" & vbCrLf)
Response.Write("    if ((nElem==""1"") ||" & vbCrLf)
Response.Write("	    (nElem==""2"") ||" & vbCrLf)
Response.Write("	    (nElem==""3"") ||" & vbCrLf)
Response.Write("	    (nElem==""5"") ||" & vbCrLf)
Response.Write("	    (nElem=="""")  ||" & vbCrLf)
Response.Write("	    (nOper==1)){" & vbCrLf)
Response.Write("	    top.fraFolder.document.location = 'CA099A.aspx?sCodispl=CA099A' + '")


Response.Write(mstrQs)


Response.Write("';" & vbCrLf)
Response.Write("	}" & vbCrLf)
Response.Write("    </" & "SCRIPT>")

	
End Sub
'% LoadFolder: Carga datos del detalle
'-----------------------------------------------------------------------------------
Private Sub LoadFolder()
	'-----------------------------------------------------------------------------------
	
Response.Write("" & vbCrLf)
Response.Write("    <TABLE WIDTH=""100%"" BORDER=0>" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("            <TD WIDTH=""20%"" CLASS=""HighLighted""><DIV ID=""divRegistro""><LABEL ID=0>" & GetLocalResourceObject("Anchor2Caption") & "</LABEL></TD>" & vbCrLf)
Response.Write("            <TD COLSPAN=""4"">&nbsp;</TD>" & vbCrLf)
Response.Write("        </TR>")

	If Request.QueryString.Item("sCodispl") <> "CA099C" Then
Response.Write("" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("		    <TD CLASS=""Horline""></TD>" & vbCrLf)
Response.Write("		    <TD COLSPAN=""4""></TD>" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("")

	End If
Response.Write("" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("            <TD><DIV ID=""divoptTypeDoc"">")


Response.Write(mobjValues.OptionControl(0, "optTypeDoc", GetLocalResourceObject("optTypeDoc_1Caption"),  , "1", "insCheck(this)", True))


Response.Write("</TD>" & vbCrLf)
Response.Write("            <TD WIDTH=""1%"">&nbsp;</TD>" & vbCrLf)
Response.Write("            <TD WIDTH=""22%""><DIV ID=""divOperacion""><LABEL ID=0>" & GetLocalResourceObject("cbeOperatCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("			<TD><DIV ID=""divcbeOperat"">")


Response.Write(mobjValues.ComboControl("cbeOperat", mclsSche_Transac.Sche_Transac(Session("sSche_code"), Request.QueryString.Item("sCodispl")), mstrOperat, True, 1, GetLocalResourceObject("cbeOperatToolTip"), "ChangeValues(this)", True))


Response.Write("</TD>" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("            <TD><DIV ID=""divoptTypeDoc1"">")


Response.Write(mobjValues.OptionControl(0, "optTypeDoc", GetLocalResourceObject("optTypeDoc_2Caption"), CStr(1), "2", "insCheck(this)", True))


Response.Write("</TD>" & vbCrLf)
Response.Write("            <TD>&nbsp;</TD>" & vbCrLf)
Response.Write("            <TD><DIV ID=""divTipocot""><LABEL ID=0>" & GetLocalResourceObject("valOriginCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("            <TD><DIV ID=""divvalOrigin"">")


Response.Write(mobjValues.PossiblesValues("valOrigin", "Table5580", eFunctions.Values.eValuesType.clngWindowType,  ,  ,  ,  ,  ,  , "ChangeValues(this)", True,  , GetLocalResourceObject("valOriginToolTip")))


Response.Write("</TD>" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("    </TABLE>" & vbCrLf)
Response.Write("    <BR>" & vbCrLf)
Response.Write("    <TABLE WIDTH=""100%"" BORDER=0>" & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("            <TD WIDTH=""20%""><DIV ID=""divRamo""><LABEL ID=13871>" & GetLocalResourceObject("cbeBranchCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("            <TD WIDTH=""25%""><DIV ID=""divcbeBranch"">")


Response.Write(mobjValues.BranchControl("cbeBranch", GetLocalResourceObject("cbeBranchToolTip"),  , "valProduct",  ,  ,  ,  , True))


Response.Write("</TD>" & vbCrLf)
Response.Write("			<TD WIDTH=""2%"">&nbsp;</TD>" & vbCrLf)
Response.Write("            <TD WIDTH=""15%""><DIV ID=""divProducto""><LABEL ID=13872>" & GetLocalResourceObject("valProductCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("            <TD><DIV ID=""divvalProduct"">")

	
	'+ Se crea parametro de salida para retornar el ramo tecnico (sBrancht)
	With mobjValues.Parameters
		.Add("nBranch", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.ReturnValue("sBrancht", False, "Ramo técnico", True)
	End With
	Response.Write(mobjValues.PossiblesValues("valProduct", "tabProdmaster1", eFunctions.Values.eValuesType.clngWindowType, "", True,  ,  ,  ,  ,  , True, 5, GetLocalResourceObject("valProductToolTip")))
	
Response.Write("" & vbCrLf)
Response.Write("			</TD>" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("        ")

	If Request.QueryString.Item("sCodispl") <> "CA099C" Then
Response.Write("" & vbCrLf)
Response.Write("            <TD><DIV ID=""divNodecot""><LABEL ID=0>" & GetLocalResourceObject("tcnPolicyCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("            <TD><DIV ID=""divtcnPolicy"">")


Response.Write(mobjValues.NumericControl("tcnPolicy", 9, "",  , GetLocalResourceObject("tcnPolicyToolTip"),  ,  ,  ,  ,  , "ShowChangeValues('CotProp')", True))


Response.Write("</TD>" & vbCrLf)
Response.Write("			<TD>&nbsp;</TD>" & vbCrLf)
Response.Write("            <TD><DIV ID=""divPolicy""><LABEL ID=0>" & GetLocalResourceObject("tcnProponumCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("            <TD><DIV ID=""divtcnProponum"">")


Response.Write(mobjValues.NumericControl("tcnProponum", 9, "",  , GetLocalResourceObject("tcnProponumToolTip"),  ,  ,  ,  ,  , "ShowChangeValues('Policy_CA099')", True))


Response.Write("" & vbCrLf)
Response.Write("" & vbCrLf)
Response.Write("        </TR>                    " & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("            <TD><DIV ID=""divCertificado""><LABEL ID=0>" & GetLocalResourceObject("tcnCertifCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("            <TD><DIV ID=""divtcnCertif"">")


Response.Write(mobjValues.NumericControl("tcnCertif", 9, "",  , GetLocalResourceObject("tcnCertifToolTip"),  ,  ,  ,  ,  ,  , True))


Response.Write("</TD>" & vbCrLf)
Response.Write("			<TD>&nbsp;</TD>" & vbCrLf)
Response.Write("            <TD><DIV ID=""divEstado""><LABEL ID=0>" & GetLocalResourceObject("cbeStatCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("            <TD><DIV ID=""divcbeStat"">")


Response.Write(mobjValues.PossiblesValues("cbeStat", "table5526", eFunctions.Values.eValuesType.clngComboType, "",  ,  ,  ,  ,  ,  , True,  , GetLocalResourceObject("cbeStatToolTip")))


Response.Write("</TD>" & vbCrLf)
Response.Write(" 		")

	Else
Response.Write("" & vbCrLf)
Response.Write("			<TD><DIV ID=""divNodecot""><LABEL ID=0>" & GetLocalResourceObject("tcnPolicyCaption") & "</LABEL></TD>		" & vbCrLf)
Response.Write("            <TD><DIV ID=""divtcnPolicy"">")


Response.Write(mobjValues.NumericControl("tcnPolicy", 9, "",  , GetLocalResourceObject("tcnPolicyToolTip"),  ,  ,  ,  ,  , "ShowChangeValues('CotProp')", True))


Response.Write("</TD>" & vbCrLf)
Response.Write("			<TD>&nbsp;</TD>" & vbCrLf)
Response.Write("            <TD><DIV ID=""divEstado""><LABEL ID=0>" & GetLocalResourceObject("cbeStatCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("            <TD><DIV ID=""divcbeStat"">")


Response.Write(mobjValues.PossiblesValues("cbeStat", "table5526", eFunctions.Values.eValuesType.clngComboType, "",  ,  ,  ,  ,  ,  , True,  , GetLocalResourceObject("cbeStatToolTip")))


Response.Write("</TD>" & vbCrLf)
Response.Write("        ")

	End If
Response.Write("        " & vbCrLf)
Response.Write("        </TR>                    " & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("            <TD><DIV ID=""divCliente""><LABEL ID=0>" & GetLocalResourceObject("dtcClientCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("            <TD COLSPAN=""1""><DIV ID=""divdtcClient"">")


Response.Write(mobjValues.ClientControl("dtcClient", "",  , GetLocalResourceObject("dtcClientToolTip"),  , True, "lblCliename", True))


Response.Write("</TD>" & vbCrLf)
Response.Write("            <TD COLSPAN=""1""><DIV ID=""divlblCliename"">")


Response.Write(mobjValues.DIVControl("lblCliename", False, ""))


Response.Write("&nbsp;</TD>" & vbCrLf)
Response.Write("            <TD><DIV ID=""divCausa""><LABEL ID=0>" & GetLocalResourceObject("cboWaitCodeCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("            <TD><DIV ID=""divcboWaitCode"">")

	mobjValues.Parameters.Add("sBrancht", Session("sBrancht"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	mobjValues.BlankPosition = True
	Response.Write(mobjValues.PossiblesValues("cboWaitCode", "tabtab_waitpo", eFunctions.Values.eValuesType.clngComboType, "", True,  ,  ,  ,  ,  , True))
Response.Write("</TD>" & vbCrLf)
Response.Write("            " & vbCrLf)
Response.Write("        </TR>        " & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("            <TD><DIV ID=""divIntermediario""><LABEL ID=0>" & GetLocalResourceObject("valIntermedCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("            <TD COLSPAN=""4""><DIV ID=""divvalIntermed"">")


Response.Write(mobjValues.PossiblesValues("valIntermed", "tabintermedia_o", eFunctions.Values.eValuesType.clngWindowType,  ,  ,  ,  ,  ,  ,  , True,  , GetLocalResourceObject("valIntermedToolTip")))


Response.Write("</TD>" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("            <TD><DIV ID=""divAgencia""><LABEL ID=0>" & GetLocalResourceObject("valAgencyCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("            <TD COLSPAN=""4""><DIV ID=""divvalAgency"">")

	
	mobjValues.Parameters.Add("nOfficeagen", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	mobjValues.Parameters.Add("nAgency", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	Response.Write(mobjValues.PossiblesValues("valAgency", "TabAgencies_T5555", 2, "", True,  ,  ,  ,  ,  , True,  , GetLocalResourceObject("valAgencyToolTip")))
	
Response.Write("" & vbCrLf)
Response.Write("            </TD>" & vbCrLf)
Response.Write("        </TR> " & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("	")

	If Request.QueryString.Item("sCodispl") <> "CA099C" Then
Response.Write("" & vbCrLf)
Response.Write("           <TD><LABEL ID=0>" & GetLocalResourceObject("Anchor3Caption") & "</LABEL></TD>" & vbCrLf)
Response.Write("	")

	Else
Response.Write("" & vbCrLf)
Response.Write("           <TD><LABEL ID=0>" & GetLocalResourceObject("tcdEffecdateCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("	")

	End If
Response.Write("" & vbCrLf)
Response.Write("            <TD COLSPAN=""2"">")


Response.Write(mobjValues.DateControl("tcdEffecdate", "",  , GetLocalResourceObject("tcdEffecdateToolTip"),  ,  ,  ,  , True))


Response.Write("</TD>" & vbCrLf)
Response.Write("            <TD><DIV ID=""divFecha2""><LABEL ID=0>" & GetLocalResourceObject("tcdLastdateCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("            <TD COLSPAN=""2""><DIV ID=""divtcdEffecdate2"">")


Response.Write(mobjValues.DateControl("tcdLastdate", "",  , GetLocalResourceObject("tcdLastdateToolTip"),  ,  ,  ,  , True))


Response.Write("</TD>" & vbCrLf)
Response.Write("        </TR> " & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("            <TD COLSPAN=""2""><DIV ID=""tcdchkDueDate"">" & vbCrLf)
Response.Write("            ")


Response.Write(mobjValues.CheckControl("chkDueDate", GetLocalResourceObject("chkDueDateCaption"), "", "1",  , True,  , GetLocalResourceObject("chkDueDateToolTip")))


Response.Write("" & vbCrLf)
Response.Write("            </TD>" & vbCrLf)
Response.Write("        </TR> " & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("            <TD COLSPAN=""2""><DIV ID=""chkApplyCostFP"">" & vbCrLf)
Response.Write("            ")


Response.Write(mobjValues.CheckControl("chkApplyCostFP", GetLocalResourceObject("chkApplyCostFPCaption"), "", "1",  , True,  , GetLocalResourceObject("chkApplyCostFPToolTip")))


Response.Write("" & vbCrLf)
Response.Write("            </TD>" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("	")

	If Request.QueryString.Item("sCodispl") = "CA099C" Then
Response.Write("" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("            <TD><DIV ID=""divPolicy""><LABEL ID=0>" & GetLocalResourceObject("tcnProponumCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("            <TD><DIV ID=""divtcnProponum"">")


Response.Write(mobjValues.NumericControl("tcnProponum", 9, "",  , GetLocalResourceObject("tcnProponumToolTip"),  ,  ,  ,  ,  , "ShowChangeValues('Policy_CA099')", True))


Response.Write("" & vbCrLf)
Response.Write("			<TD>&nbsp;</TD>" & vbCrLf)
Response.Write("            <TD><DIV ID=""divCertificado""><LABEL ID=0>" & GetLocalResourceObject("tcnCertifCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("            <TD><DIV ID=""divtcnCertif"">")


Response.Write(mobjValues.NumericControl("tcnCertif", 9, "",  , GetLocalResourceObject("tcnCertifToolTip"),  ,  ,  ,  ,  ,  , True))


Response.Write("</TD>" & vbCrLf)
Response.Write("        </TR> " & vbCrLf)
Response.Write("	")

	End If
Response.Write("" & vbCrLf)
Response.Write("    </TABLE>" & vbCrLf)
Response.Write("    ")

	
End Sub

</script>
<%Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage("CA099_K")
mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.31.20
mobjValues.sSessionID = Session.SessionID
mobjValues.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjValues.sCodisplPage = "CA099_K"
mobjMenu = New eFunctions.Menues
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.31.20
mobjMenu.sSessionID = Session.SessionID
mobjMenu.nUsercode = Session("nUsercode")
'+ se limpia la session skey
Session("sKey") = ""
'+ Se asignan variables de parametro
With Request
	mstrBranch = .QueryString.Item("nBranch")
	mstrProduct = .QueryString.Item("nProduct")
	mstrDocType = .QueryString.Item("sTypeDoc")
	mstrOperat = .QueryString.Item("nOperat")
	If mstrOperat = "1" Then
		mstrAction = "401"
	Else
		mstrAction = .QueryString.Item("nMainAction")
	End If
	mstrOrigin = .QueryString.Item("nOrigin")
	mstrEffecdate = .QueryString.Item("dEffecdate")
	mstrBrancht = .QueryString.Item("sBrancht")
	mstrdateCont = .QueryString.Item("dateCont")
End With

Session("OP006_sCodispl") = ""
Session("OP006_sCertype") = ""
Session("OP006_nBranch") = ""
Session("OP006_nProduct") = ""
Session("OP006_nPolicy") = ""
Session("OP006_nCertif") = ""
Session("OP006_nConcept") = ""

mclsSche_Transac = New eSecurity.Secur_sche
mstrQs = Request.QueryString.Item("sCodispl")
'+ Se crea cadena de parametros (solo si es encabezado de secuencia)
If Request.QueryString.Item("sConfig") = "InSequence" Then
	mstrQs = mstrQs & "&nBranch=" & mstrBranch & "&nProduct=" & mstrProduct & "&dateCont=" & mstrdateCont & "&dEffecdate=" & mstrEffecdate & "&nOrigin=" & mstrOrigin & "&sTypeDoc=" & mstrDocType & "&sExpired=" & Request.QueryString.Item("sExpired") & "&sApplyCostFP=" & Request.QueryString.Item("sApplyCostFP") & "&dStartdate=" & Request.QueryString.Item("dStartdate") & "&nOperat=" & mstrOperat & "&nMainAction=" & mstrAction & "&sBrancht=" & mstrBrancht & "&sCertype=" & Request.QueryString.Item("sCertype") & "&nPolicy=" & Session("nPolicy") & "&nCertif=" & Request.QueryString.Item("nCertif") & "&nProponum=" & Request.QueryString.Item("nProponum") & "&sClient=" & Request.QueryString.Item("sClient") & "&nStatus=" & Request.QueryString.Item("nStatus") & "&nIntermed=" & Request.QueryString.Item("nIntermed") & "&nAgency=" & Request.QueryString.Item("nAgency") & "&dLastdate=" & Request.QueryString.Item("dLastdate") & "&sCodispl_orig=" & Request.QueryString.Item("sCodispl_orig") & "&nWaitCode=" & Request.QueryString.Item("nWaitCode")
End If
%>
<HTML>
<HEAD>
    <META NAME = "GENERATOR" CONTENT = "Microsoft Visual Studio 6.0">


<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/Constantes.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tmenu.js"></SCRIPT>
<SCRIPT> 
//- Variable para el control de versiones
    document.VssVersion="$$Revision: 6 $|$$Date: 5-04-06 21:25 $|$$Author: Clobos $"
var scodispl='<%=Request.QueryString.Item("sCodispl")%>'
var scodispl_orig='<%=Request.QueryString.Item("scodispl_orig")%>'


//% insCancel: realiza el manejo en caso que el usuario cancele la transacción
//------------------------------------------------------------------------------------------
function insCancel(){
//------------------------------------------------------------------------------------------
	if (scodispl_orig!=''){
		top.document.location.href = '/VTimeNet/common/GoTo.aspx?sCodispl=' + scodispl_orig;
		return false;
	}
	else
		return true;
}
//% insCancel: realiza el manejo en caso que el usuario cancele la transacción
//------------------------------------------------------------------------------------------
function insFinish(){
//------------------------------------------------------------------------------------------
	return true;
}
//% insStateZone: controla el estado de los campos de la página
//------------------------------------------------------------------------------------------
function insStateZone(nAction){
//------------------------------------------------------------------------------------------      
//- Tipo de acciones del menu
	var eTypeActions = new TypeActions()	
    
	with(self.document.forms[0]){
        valOrigin.value        = "";
		UpdateDiv('valOriginDesc',"")	
		valOrigin.disabled     = false;
		btnvalOrigin.disabled  = false;

//+ Para accion consulta solo se habilita la operacion consultar		
		if (top.frames["fraSequence"].plngMainAction==eTypeActions.clngActionQuery)
		{
		    cbeOperat.value			= 1;
		    cbeOperat.disabled		= true;
//+ Se habilitan campos para consulta general y se deshabilitan para actualización 
			dtcClient.disabled      = 
			btndtcClient.disabled   = 
			cbeStat.disabled        = 
			valIntermed.disabled    = 
			btnvalIntermed.disabled = 
			valAgency.disabled      = 
			btnvalAgency.disabled   = 
			chkDueDate.disabled     = false;
			chkApplyCostFP.disabled = false;
        }		    
		else
		{
			dtcClient.disabled      = 
			btndtcClient.disabled   = 
			cbeStat.disabled        = 
			valIntermed.disabled    = 
			btnvalIntermed.disabled = 
			valAgency.disabled      = 
			btnvalAgency.disabled   = 
			chkDueDate.disabled     = 
			cboWaitCode.disabled    =
			cbeOperat.disabled		= false;
			chkApplyCostFP.disabled = false;
		}
//+ Se habilitan controles 
		optTypeDoc[0].disabled      = 
		optTypeDoc[1].disabled      = 
		tcnPolicy.disabled          = 
		tcdEffecdate.disabled       =     
		btn_tcdEffecdate.disabled   = 
		tcdLastdate.disabled       =     
		btn_tcdLastdate.disabled   = 
		cbeBranch.disabled          = false;
            
		tcnPolicy.value             = "";
		tcdEffecdate.value          = "";
		tcdLastdate.value          = "";

        if (scodispl=="CA099C"){
		    valOrigin.value        = 1;		
		    optTypeDoc[0].checked = true;
		    optTypeDoc[0].value = 1;
		    valIntermed.value=""
		    valAgency.value=""
		}
	}
}
//% insCheck: controla el estado de los campos de la página cuando se cambia el indicador 
//%           de cotizacion / propuesta
//------------------------------------------------------------------------------------------
function insCheck(Field){
//------------------------------------------------------------------------------------------      
	with(self.document.forms[0]){
//+ Si es cotizacion, se sacan las opciones de Anulacion, Rehabilitacion, 
//+ Saldado, Prorrogado, Rescate y Prestamo
		 valOrigin.disabled     = false   
         btnvalOrigin.disabled  = false	
         cbeOperat.value		= "";         			   
         
	    if(optTypeDoc[0].checked){
	        valOrigin.List     = "4,5,6,7,8,9" 
	        valOrigin.TypeList = 2
            valOrigin.value    = "";
	        UpdateDiv('valOriginDesc',"")
	    }
	    else{
	        valOrigin.TypeList = 0
            valOrigin.value    = "";
		    UpdateDiv('valOriginDesc',"")
	    }
        if (scodispl=="CA099C"){
		    valOrigin.value        = 1;		
		    optTypeDoc[0].checked = true;
		    optTypeDoc[0].value = 1;
		    valIntermed.value=""
		    valAgency.value=""
		}
	}
}
//% ShowChangeValues: Se cargan los valores de acuerdo producto seleccionado
//-------------------------------------------------------------------------------------------
function ShowChangeValues(sField){
//-------------------------------------------------------------------------------------------    
	var sCertype

	sCertype = "0";

	if (self.document.forms[0].optTypeDoc[0].checked){
		if (self.document.forms[0].valOrigin.value == "1")
			sCertype = "3";
		else
		if (self.document.forms[0].valOrigin.value == "2")
			sCertype = "4";
		else
		if (self.document.forms[0].valOrigin.value == "3")
			sCertype = "5";
	}
	else{
		if (self.document.forms[0].valOrigin.value == "1")
			sCertype = "1";
		else
		if (self.document.forms[0].valOrigin.value == "2")
			sCertype = "6";
		else
		if (self.document.forms[0].valOrigin.value == "3")
			sCertype = "7";
		else
		if (self.document.forms[0].valOrigin.value == "10")
			sCertype = "8";
		else
		if (self.document.forms[0].valOrigin.value >= "4")
			sCertype = "8";
	}
	
	if (sCertype != "0"){
	
		switch(sField){
			case "CotProp":
			    if (self.document.forms[0].tcnPolicy.value!='')
				    ShowPopUp("/VTimeNet/Policy/PolicyTra/ShowDefValues.aspx?Field=" + sField  + "&sCertype=" + sCertype + "&nProponum=" + self.document.forms[0].tcnPolicy.value + "&nBranch=" + self.document.forms[0].cbeBranch.value + "&nProduct=" + self.document.forms[0].valProduct.value + "&nAction=" + top.frames["fraSequence"].plngMainAction + "&valOrigin=" + self.document.forms[0].valOrigin.value, "ShowDefValuesCotProp", 1, 1,"no","no",2000,2000);
				break;
			case "Policy_CA099":
		        insDefValues(sField,"nBranch=" + self.document.forms[0].cbeBranch.value + "&nProduct=" + self.document.forms[0].valProduct.value + "&nPolicy=" + self.document.forms[0].tcnPolicy.value)
				break;			
		}	
	}	
}
//% ChangeValues: se maneja la habilitacion de los controles de la página
//------------------------------------------------------------------------------------------
function ChangeValues(Field){
//------------------------------------------------------------------------------------------
//- Tipo de accion del menu	
	var eTypeActions = new TypeActions();
//- Operacion de tipo consulta	
	var lboolOperQuery = true;
//- Origen de Cotizacion/Propuesta de tipo Emision, Modificacion o Renovacion
	var lboolOrigIssue = true;
//- Origen de Cotizacion/Propuesta de tipo Anulación
	var lboolOrigNull = true;
	with(self.document.forms[0]){
	    if (Field.name=="valOrigin"){
//+ Se asignan valores a indicadores
	        lboolOperQuery = cbeOperat.value == '1';
	        lboolOrigIssue = valOrigin.value == '1' ||
	                         valOrigin.value == '2' ||
	                         valOrigin.value == '3' ||
	                         valOrigin.value == '';
			lboolOrigNull = valOrigin.value == '4';
//+ Se habilitan los campos segun corresponda
            btnvalProduct.disabled  = lboolOperQuery && !lboolOrigIssue;
			tcnProponum.disabled    = 
			tcnCertif.disabled      = !lboolOrigIssue;

//			tcnProponum.disabled    = !lboolOrigNull

			dtcClient.disabled      = 
			btndtcClient.disabled   = 
			cbeStat.disabled        = 
			valIntermed.disabled    = 
			btnvalIntermed.disabled = 
			valAgency.disabled      = 
			btnvalAgency.disabled   = 
			chkDueDate.disabled     = !(lboolOperQuery && lboolOrigIssue);
			//chkApplyCostFP.disabled = !(lboolOperQuery && lboolOrigIssue);

//+ Los campos deshabilitados quedan en blanco			
			if (cbeBranch.disabled) 
			    cbeBranch.value         = "";
			if (valProduct.disabled){
			    valProduct.value        = "";
			    UpdateDiv('valProductDesc',"")
			}
            if (tcnProponum.disabled)
			    tcnProponum.value       = "";
            if (tcnCertif.disabled)
			    tcnCertif.value         = "";
            if (dtcClient.disabled)
			    dtcClient.value         = "";
			if (cbeStat.disabled)
			    cbeStat.value           = "";
			if (valIntermed.disabled){
			    valIntermed.value       = "";
			    UpdateDiv('valIntermedDesc',"")
			}
			if (valAgency.disabled){
			    valAgency.value         = "";
			    UpdateDiv('valAgencyDesc',"")
			}
			if (chkDueDate.disabled)
			    chkDueDate.checked        = false;
			    
			if (chkApplyCostFP.disabled)
			    chkApplyCostFP.checked    = false;
	    }

		if (Field.name=="cbeOperat"){
			
			if (Field.value=="7") {
				cboWaitCode.disabled = true
				cbeStat.disabled = true
			}	
			else{
				cboWaitCode.disabled = false
				cbeStat.disabled = true
			}
		}
		
		if (Field.name=="cbeOperat"){
//	        alert('cbeOperat->' + cbeOperat.value)
			if (Field.value=="8") {
                valOrigin.value	= 1		
                $(valOrigin).change()                
				valOrigin.disabled = true
        		btnvalOrigin.disabled  = true				
                cbeBranch.disabled = false
                valProduct.disabled = false                				
                tcnProponum.disabled = false
            }
			else
				if (Field.value=="1") {
					cbeStat.disabled = false
				}
				
				if (scodispl!="CA099C"){
					valOrigin.value	= ""
					UpdateDiv('valOriginDesc',"")									
					valOrigin.disabled = false   
        			btnvalOrigin.disabled  = false				             
				}	
		}		
	}
}
//% insHideFields: Oculta campos de la página
//-------------------------------------------------------------------------------------------
function insHideFields(ValueJustQuote){
//-------------------------------------------------------------------------------------------

	with(self.document.forms[0]){	
		
		if (ValueJustQuote=="CA099C"){	
			ShowDiv('divTipocot', 'hide');
			ShowDiv('divvalOrigin', 'hide');
			ShowDiv('divRegistro', 'hide');
			ShowDiv('divoptTypeDoc', 'hide');
			ShowDiv('divoptTypeDoc1', 'hide');
			ShowDiv('divIntermediario', 'hide');
			ShowDiv('divvalIntermed', 'hide');
			ShowDiv('divAgencia', 'hide');
			ShowDiv('divvalAgency', 'hide');
			ShowDiv('divtcnProponum', 'hide');
			ShowDiv('divPolicy', 'hide');
			ShowDiv('divCertificado', 'hide');
			ShowDiv('divtcnCertif', 'hide');
		}
		else
		{
			ShowDiv('divFecha2', 'hide');
			ShowDiv('divtcdEffecdate2', 'hide');
		}		
	}
}
</SCRIPT>
    <%With Response
	.Write(mobjValues.StyleSheet() & vbCrLf)
	.Write(mobjMenu.MakeMenu("CA099", "CA099_K.aspx", 1, ""))
	.Write("<BR><BR>")
End With
mobjMenu = Nothing
%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" ID="FORM" NAME="frmPropoOperat" ACTION="ValPolicyTra.aspx?sTime=1&sCodispl_orig=<%=mstrQs%>">
<%If Request.QueryString.Item("sConfig") = "InSequence" Then
	Call LoadHeader()
Else
	Call LoadFolder()
	Response.Write("<SCRIPT>insHideFields('" & Request.QueryString.Item("sCodispl") & "');</script>")
End If

mobjValues = Nothing
mclsSche_Transac = Nothing
%>	
</FORM>
</BODY>
</HTML>

<%'^Begin Footer Block VisualTimer Utility 1.1 7/4/03 10.31.20

Call mobjNetFrameWork.FinishPage("CA099_K")
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>





