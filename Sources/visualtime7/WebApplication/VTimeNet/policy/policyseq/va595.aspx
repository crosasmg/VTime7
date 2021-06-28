<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="ePolicy" %>
<script language="VB" runat="Server">
'^Begin Header Block VisualTimer Utility 1.1 31/3/03 19.42.05
Dim mobjNetFrameWork As eNetFrameWork.Layout

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'- Objeto para el manejo del grid de la página
Dim mobjGrid As eFunctions.Grid

'- Objeto para el manejo del menú
Dim mobjMenu As eFunctions.Menues


'% InsInitial: Crea los campos de la parte puntual de la página
'--------------------------------------------------------------------------------------------
Private Sub InsInitial()
	'--------------------------------------------------------------------------------------------
	Dim lclsActivelife As ePolicy.Activelife
	lclsActivelife = New ePolicy.Activelife
	With lclsActivelife
		.InsPreVA595(Request.QueryString.Item("ReloadAction"), Session("sCertype"), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nCertif"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), 0, mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nCurrency"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nPremiumbas"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nPremimin"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nVpprdeal"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nPremfreq"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nPremdeal"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nPrsugest"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nVpprsug"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nAmountcontr"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nIntproject"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nWarminint"), eFunctions.Values.eTypeData.etdDouble), Request.QueryString.Item("sInscalpre"), mobjValues.StringToType(Request.QueryString.Item("nRatepayf"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nInsurtime"), eFunctions.Values.eTypeData.etdDouble))
		
		Response.Write(mobjValues.HiddenControl("hddnPremAnu", CStr(.nPremiumbas)))
		Response.Write(mobjValues.HiddenControl("hddnVPprdeal", CStr(.nVpprdeal)))
		Response.Write(mobjValues.HiddenControl("hddnPrsugest", CStr(.nPrsugest)))
		Response.Write(mobjValues.HiddenControl("hddnVPprsug", CStr(.nVpprsug)))
		Response.Write(mobjValues.HiddenControl("hddnPremimin", CStr(.nPremimin)))
		Response.Write(mobjValues.HiddenControl("hddnPremdep", CStr(.nAmountcontr)))
		Response.Write(mobjValues.HiddenControl("hddsIndCalPre", .sInscalpre))
		Response.Write(mobjValues.HiddenControl("hddnCurrency", CStr(.nCurrency)))
		Response.Write(mobjValues.HiddenControl("hddnIntproject", CStr(.nIntproject)))
		Response.Write(mobjValues.HiddenControl("hddnWarminint", CStr(.nWarminint)))
		Response.Write(mobjValues.HiddenControl("hddnRatepayf", CStr(.nRatepayf)))
		Response.Write(mobjValues.HiddenControl("hddsProcessed", vbNullString))
		Response.Write(mobjValues.HiddenControl("hddnInsurtime", CStr(.nInsurtime)))
		Response.Write(mobjValues.HiddenControl("hddsPremdeal", "0"))
		Response.Write(mobjValues.HiddenControl("hddsPremdeal_Chan", "1"))
		Response.Write(mobjValues.HiddenControl("hddnYear_end", vbNullString))
		Response.Write(mobjValues.HiddenControl("hddnPremdeal_old", CStr(.nPremdeal)))
		
Response.Write("" & vbCrLf)
Response.Write("<TABLE WIDTH=""100%"">" & vbCrLf)
Response.Write("    <TR>" & vbCrLf)
Response.Write("        <TD><LABEL ID=0>" & GetLocalResourceObject("cbeCurrencyCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("        <TD>")


Response.Write(mobjValues.PossiblesValues("cbeCurrency", "Table11", eFunctions.Values.eValuesType.clngComboType, CStr(.nCurrency),  , True,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbeCurrencyToolTip")))


Response.Write("</TD>" & vbCrLf)
Response.Write("        <TD>&nbsp;</TD>" & vbCrLf)
Response.Write("        <TD><LABEL ID=0>" & GetLocalResourceObject("tcnPremAnuCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("        <TD>")


Response.Write(mobjValues.NumericControl("tcnPremAnu", 18, CStr(.nPremiumbas),  , GetLocalResourceObject("tcnPremAnuToolTip"), True, 6, True))


Response.Write("</TD>" & vbCrLf)
Response.Write("    </TR>" & vbCrLf)
Response.Write("    <TR>" & vbCrLf)
Response.Write("        <TD><LABEL ID=0>" & GetLocalResourceObject("tcnPremiminCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("        <TD>")


Response.Write(mobjValues.NumericControl("tcnPremimin", 18, CStr(.nPremimin),  , GetLocalResourceObject("tcnPremiminToolTip"), True, 6, True))


Response.Write("</TD>" & vbCrLf)
Response.Write("        <TD>&nbsp;</TD>" & vbCrLf)
Response.Write("        <TD><LABEL ID=0>" & GetLocalResourceObject("tcnVPprdealCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("        <TD>")


Response.Write(mobjValues.NumericControl("tcnVPprdeal", 18, CStr(.nVpprdeal),  , GetLocalResourceObject("tcnVPprdealToolTip"), True, 6, True))


Response.Write("</TD>" & vbCrLf)
Response.Write("    </TR>" & vbCrLf)
Response.Write("    <TR>" & vbCrLf)
Response.Write("        <TD><LABEL ID=0>" & GetLocalResourceObject("tcnPremfreqCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("        <TD>")


Response.Write(mobjValues.NumericControl("tcnPremfreq", 18, CStr(.nPremfreq),  , GetLocalResourceObject("tcnPremfreqToolTip"), True, 6,  ,  ,  ,  , True))


Response.Write("</TD>" & vbCrLf)
Response.Write("        <TD>&nbsp;</TD>" & vbCrLf)
Response.Write("        <TD><LABEL ID=0>" & GetLocalResourceObject("tcnPrsugestCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("        <TD>")


Response.Write(mobjValues.NumericControl("tcnPrsugest", 18, CStr(.nPrsugest),  , GetLocalResourceObject("tcnPrsugestToolTip"), True, 6, True))


Response.Write("</TD>" & vbCrLf)
Response.Write("    </TR>" & vbCrLf)
Response.Write("    <TR>" & vbCrLf)
Response.Write("        <TD><LABEL ID=0>" & GetLocalResourceObject("tcnVPprsugCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("        <TD>")


Response.Write(mobjValues.NumericControl("tcnVPprsug", 18, CStr(.nVpprsug),  , GetLocalResourceObject("tcnVPprsugToolTip"), True, 6, True))


Response.Write("</TD>" & vbCrLf)
Response.Write("        <TD>&nbsp;</TD>" & vbCrLf)
Response.Write("        <TD><LABEL ID=0>" & GetLocalResourceObject("tcnPremdepCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("        <TD>")


Response.Write(mobjValues.NumericControl("tcnPremdep", 18, CStr(.nAmountcontr),  , GetLocalResourceObject("tcnPremdepToolTip"), True, 6, True))


Response.Write("</TD>" & vbCrLf)
Response.Write("    </TR>" & vbCrLf)
Response.Write("    <TR>" & vbCrLf)
Response.Write("        <TD><LABEL ID=0>" & GetLocalResourceObject("tcnIntprojectCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("        <TD>")


Response.Write(mobjValues.NumericControl("tcnIntproject", 5, CStr(.nIntproject),  , GetLocalResourceObject("tcnIntprojectToolTip"), True, 2, True))


Response.Write("</TD>" & vbCrLf)
Response.Write("        <TD>&nbsp;</TD>" & vbCrLf)
Response.Write("        <TD><LABEL ID=0>" & GetLocalResourceObject("tcnWarminintCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("        <TD>")


Response.Write(mobjValues.NumericControl("tcnWarminint", 5, CStr(.nWarminint),  , GetLocalResourceObject("tcnWarminintToolTip"), True, 2, True))


Response.Write("</TD>" & vbCrLf)
Response.Write("    </TR>" & vbCrLf)
Response.Write("    <TR>" & vbCrLf)
Response.Write("        <TD><LABEL ID=0>" & GetLocalResourceObject("tcnPremdealCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("        <TD>")


Response.Write(mobjValues.NumericControl("tcnPremdeal", 18, CStr(.nPremdeal),  , GetLocalResourceObject("tcnPremdealToolTip"), True, 6,  ,  ,  , "InsDisPremdel(1, this.value, '')"))


Response.Write("</TD>" & vbCrLf)
Response.Write("        <TD>&nbsp;</TD>" & vbCrLf)
Response.Write("        <TD>")


Response.Write(mobjValues.AnimatedButtonControl("btnCalc", "..\..\images\batchStat06.png", GetLocalResourceObject("btnCalcToolTip"),  , "InsShowIlustration();"))


Response.Write("<TD>" & vbCrLf)
Response.Write("    </TR>" & vbCrLf)
Response.Write("</TABLE>" & vbCrLf)
Response.Write("<BR>")

		
	End With
	lclsActivelife = Nothing
End Sub

'% InsDefineHeader: se definen las propiedades del grid
'--------------------------------------------------------------------------------------------
Private Sub InsDefineHeader()
	'--------------------------------------------------------------------------------------------
	mobjGrid = New eFunctions.Grid
	'^Begin Body Block VisualTimer Utility 1.1 31/3/03 19.42.05
	mobjGrid.sSessionID = Session.SessionID
	mobjGrid.nUsercode = Session("nUsercode")
	'~End Body Block VisualTimer Utility
	
	mobjGrid.sCodisplPage = Request.QueryString.Item("sCodispl")
	Call mobjGrid.SetWindowParameters(Request.QueryString.Item("sCodispl"), Request.QueryString.Item("sWindowDescript"), Request.QueryString.Item("nWindowTy"))
	
	'+ Se definen las columnas del grid
	With mobjGrid.Columns
		.AddNumericColumn(0, GetLocalResourceObject("tcnYear_iniColumnCaption"), "tcnYear_ini", 5, vbNullString,  , GetLocalResourceObject("tcnYear_iniColumnToolTip"),  ,  ,  ,  ,  , Request.QueryString.Item("Action") = "Update")
		.AddNumericColumn(0, GetLocalResourceObject("tcnYear_endColumnCaption"), "tcnYear_end", 5, vbNullString,  , GetLocalResourceObject("tcnYear_endColumnToolTip"),  ,  ,  ,  ,  , Request.QueryString.Item("Action") = "Update")
		.AddNumericColumn(0, GetLocalResourceObject("tcnAmountdepColumnCaption"), "tcnAmountdep", 18, vbNullString,  , GetLocalResourceObject("tcnAmountdepColumnToolTip"), True, 6)
	End With
	
	'+ Se definen las propiedades generales del grid
	With mobjGrid
		.Codispl = "VA595"
		.ActionQuery = mobjValues.ActionQuery
		.Columns("tcnYear_ini").EditRecord = True
		.Columns("tcnYear_end").EditRecord = True
		.Height = 200
		.Width = 280
		.nMainAction = CShort(IIf(Request.QueryString.Item("nMainAction") ="", 0, Request.QueryString.Item("nMainAction")))
		.Columns("Sel").GridVisible = Not .ActionQuery
		.sDelRecordParam = "nYear_ini=' + marrArray[lintIndex].tcnYear_ini +  '"
		If Request.QueryString.Item("Reload") = "1" Then
			.sReloadIndex = Request.QueryString.Item("ReloadIndex")
		End If
	End With
End Sub

'% InsPreVA595: se realiza el manejo del grid
'--------------------------------------------------------------------------------------------
Private Sub InsPreVA595()
	'--------------------------------------------------------------------------------------------
	
Response.Write("" & vbCrLf)
Response.Write("<SCRIPT>" & vbCrLf)
Response.Write("//%InsDisPremdel: Valida la cantidad de registros en per_deposit para habilitar o no la prima" & vbCrLf)
Response.Write("//                proyectada anual" & vbCrLf)
Response.Write("//-------------------------------------------------------------------------------------------" & vbCrLf)
Response.Write("function InsDisPremdel(nCount, nPremdeal, nYear_end){" & vbCrLf)
Response.Write("//-------------------------------------------------------------------------------------------" & vbCrLf)
Response.Write("    var sThouSep = '")


Response.Write(mobjValues.msUserThousandSeparator)


Response.Write("';" & vbCrLf)
Response.Write("    var sDecSep  = '")


Response.Write(mobjValues.msUserDecimalSeparator)


Response.Write("';" & vbCrLf)
Response.Write("" & vbCrLf)
Response.Write("    with (self.document.forms[0]){" & vbCrLf)
Response.Write("        if (nCount > 1){" & vbCrLf)
Response.Write("            tcnPremdeal.disabled = true;" & vbCrLf)
Response.Write("            InsChangePremdeal(tcnPremdeal.value);" & vbCrLf)
Response.Write("            hddsPremdeal.value='2';" & vbCrLf)
Response.Write("        }" & vbCrLf)
Response.Write("        else if (nCount = 1){" & vbCrLf)
Response.Write("            tcnPremdeal.value = VTFormat(nPremdeal, '', '', '', tcnPremdeal.DecimalPlace);" & vbCrLf)
Response.Write("            InsChangePremdeal(tcnPremdeal.value);" & vbCrLf)
Response.Write("            if (nYear_end!='')" & vbCrLf)
Response.Write("				hddnYear_end.value = nYear_end;" & vbCrLf)
Response.Write("            hddsPremdeal.value='1';" & vbCrLf)
Response.Write("        }" & vbCrLf)
Response.Write("    }" & vbCrLf)
Response.Write("}" & vbCrLf)
Response.Write("</" & "SCRIPT>")

	
	'- Objetos para el manejo de los datos repetitivos de la página
	Dim lcolPer_deposit As ePolicy.Per_deposits
	Dim lclsPer_deposit As Object
	Dim ldblPrem_deal As String
	Dim llngYear_end As String
	
	lcolPer_deposit = New ePolicy.Per_deposits
	If lcolPer_deposit.Find(Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), Session("dEffecdate")) Then
		For	Each lclsPer_deposit In lcolPer_deposit
			With mobjGrid
				.Columns("tcnYear_ini").DefValue = lclsPer_deposit.nYear_ini
				ldblPrem_deal = lclsPer_deposit.nAmountdep
				llngYear_end = lclsPer_deposit.nYear_end
				.Columns("tcnYear_end").DefValue = llngYear_end
				.Columns("tcnAmountdep").DefValue = ldblPrem_deal
				Response.Write(.DoRow)
			End With
		Next lclsPer_deposit
		'+Si hay más de un registro en Per_deposit se deshabilita el campo prima proyectada anual}
		If Not mobjValues.ActionQuery Then
			Response.Write("<SCRIPT>InsDisPremdel(" & lcolPer_deposit.Count & ",'" & ldblPrem_deal & "','" & llngYear_end & "');</" & "Script>")
		End If
	End If
	Response.Write(mobjGrid.closeTable())
	lcolPer_deposit = Nothing
	lclsPer_deposit = Nothing
	
End Sub

'% InsPreVA595Upd: Se realiza el manejo de la ventana PopUp asociada al grid
'--------------------------------------------------------------------------------------------
Private Sub InsPreVA595Upd()
	'--------------------------------------------------------------------------------------------
	
Response.Write("" & vbCrLf)
Response.Write("<SCRIPT>" & vbCrLf)
Response.Write("//% UpdateFields: actualiza los campos ocultos con los campos puntuales de la BC001J" & vbCrLf)
Response.Write("//-------------------------------------------------------------------------------------------" & vbCrLf)
Response.Write("function UpdateFields(){" & vbCrLf)
Response.Write("//-------------------------------------------------------------------------------------------" & vbCrLf)
Response.Write("    with(self.document.forms[0]){" & vbCrLf)
Response.Write("        hddnPremdeal.value = top.opener.document.forms[0].tcnPremdeal.value;" & vbCrLf)
Response.Write("        hddnPremAnu.value = top.opener.document.forms[0].hddnPremAnu.value;" & vbCrLf)
Response.Write("        hddnVPprdeal.value = top.opener.document.forms[0].hddnVPprdeal.value;" & vbCrLf)
Response.Write("        hddnPrsugest.value = top.opener.document.forms[0].hddnPrsugest.value;" & vbCrLf)
Response.Write("        hddnVPprsug.value = top.opener.document.forms[0].hddnVPprsug.value;" & vbCrLf)
Response.Write("        hddnPremimin.value = top.opener.document.forms[0].hddnPremimin.value;" & vbCrLf)
Response.Write("        hddnPremdep.value = top.opener.document.forms[0].hddnPremdep.value;" & vbCrLf)
Response.Write("        hddsIndCalPre.value = top.opener.document.forms[0].hddsIndCalPre.value;" & vbCrLf)
Response.Write("        hddnCurrency.value = top.opener.document.forms[0].hddnCurrency.value;" & vbCrLf)
Response.Write("        hddnIntproject.value = top.opener.document.forms[0].hddnIntproject.value;" & vbCrLf)
Response.Write("        hddnWarminint.value = top.opener.document.forms[0].hddnWarminint.value;" & vbCrLf)
Response.Write("        hddnRatepayf.value = top.opener.document.forms[0].hddnRatepayf.value;" & vbCrLf)
Response.Write("        hddnPremfreq.value = top.opener.document.forms[0].tcnPremfreq.value;" & vbCrLf)
Response.Write("    }" & vbCrLf)
Response.Write("}" & vbCrLf)
Response.Write("</" & "SCRIPT>")

	
	Dim lclsPer_deposit As ePolicy.Per_deposit
	
	lclsPer_deposit = New ePolicy.Per_deposit
	
	With Request
		If .QueryString.Item("Action") = "Del" Then
			Response.Write(mobjValues.ConfirmDelete())
			If lclsPer_deposit.InsPostVA595Upd(.QueryString("Action"), Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), mobjValues.StringToType(.QueryString.Item("nYear_ini"), eFunctions.Values.eTypeData.etdDouble), Session("dEffecdate"), 0, 0, Session("dNulldate"), Session("nUsercode"), Session("nTransaction"), "VA595") Then
			End If
		End If
		Response.Write(mobjGrid.DoFormUpd(.QueryString.Item("Action"), "ValPolicySeq.aspx", "VA595", .QueryString.Item("nMainAction"), mobjValues.ActionQuery, CShort(.QueryString.Item("Index"))))
	End With
	With Response
		.Write(mobjValues.HiddenControl("hddnPremAnu", vbNullString))
		.Write(mobjValues.HiddenControl("hddnVPprdeal", vbNullString))
		.Write(mobjValues.HiddenControl("hddnPrsugest", vbNullString))
		.Write(mobjValues.HiddenControl("hddnVPprsug", vbNullString))
		.Write(mobjValues.HiddenControl("hddnPremimin", vbNullString))
		.Write(mobjValues.HiddenControl("hddnPremdep", vbNullString))
		.Write(mobjValues.HiddenControl("hddsIndCalPre", vbNullString))
		.Write(mobjValues.HiddenControl("hddnCurrency", vbNullString))
		.Write(mobjValues.HiddenControl("hddnIntproject", vbNullString))
		.Write(mobjValues.HiddenControl("hddnWarminint", vbNullString))
		.Write(mobjValues.HiddenControl("hddnPremdeal", vbNullString))
		.Write(mobjValues.HiddenControl("hddnRatepayf", vbNullString))
		.Write(mobjValues.HiddenControl("hddnPremfreq", vbNullString))
		.Write("<SCRIPT>UpdateFields()</" & "Script>")
	End With
	lclsPer_deposit = Nothing
End Sub

</script>
<%Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage("VA595")
'~End Header Block VisualTimer Utility
Response.CacheControl = "private"

mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 31/3/03 19.42.05
mobjValues.sSessionID = Session.SessionID
mobjValues.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjValues.sCodisplPage = Request.QueryString.Item("sCodispl")
mobjMenu = New eFunctions.Menues
'^Begin Body Block VisualTimer Utility 1.1 31/3/03 19.42.05
mobjMenu.sSessionID = Session.SessionID
mobjMenu.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility
mobjValues.ActionQuery = Session("bQuery")
%>
<HTML>
<HEAD>
    <META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>


<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT>
//- Variable para el control de versiones
    document.VssVersion="$$Revision: 4 $|$$Date: 13/10/04 17.54 $|$$Author: Nvaplat60 $"

//%InsShowIlustration: Muestra la ventana ValuePolIlustration, para mostrar la Ilustración
//------------------------------------------------------------------------------------------------
function InsShowIlustration(){
//------------------------------------------------------------------------------------------------
    var lstrQueryString
    var ldblPremdeal
    var lstrPremdeal
    var llngYear_end

    if (typeof(self.document.forms[0].tcnPremdeal)=='undefined')
        ldblPremdeal = 0;
    else
        ldblPremdeal = self.document.forms[0].tcnPremdeal.value;

    if (typeof(self.document.forms[0].tcnPremdeal)=='undefined')
        lstrPremdeal = '2';
    else
        lstrPremdeal = (self.document.forms[0].tcnPremdeal.disabled?2:1);

    if (typeof(self.document.forms[0].hddnYear_end)=='undefined')
        llngYear_end = 0;
    else
        llngYear_end = self.document.forms[0].hddnYear_end.value; 

    lstrQueryString = '&sCertype=<%=Session("sCertype")%>' + '&nBranch=<%=Session("nBranch")%>' + 
                      '&nProduct=<%=Session("nProduct")%>' + '&nPolicy=<%=Session("nPolicy")%>' + 
                      '&nCertif=<%=Session("nCertif")%>'   + '&dEffecdate=<%=Session("dEffecdate")%>' + 
                      '&nIllusttype=3&nPremdeal=' + ldblPremdeal + 
                      '&sPremdeal=' + lstrPremdeal + 
                      '&nYear_end=' + llngYear_end + 
                      '&bQuery=<%=Session("bQuery")%>'; 
    ShowPopUp("../../Common/ShowIlustration.aspx?sCodispl=VA595" + lstrQueryString, "ValuePolIlustration", 750, 500, 'yes', 'yes', 10, 10) 
} 

//%InsChangePremdeal: Calcula la prima proy. según frecuencia de pago
//------------------------------------------------------------------------------------------------
function InsChangePremdeal(nPremdeal){
//------------------------------------------------------------------------------------------------
    with (self.document.forms[0]){
        if (hddnPremdeal_old.value != insConvertNumber(tcnPremdeal.value)) {
            hddsPremdeal_Chan.value='2';
        }
        else{
            hddsPremdeal_Chan.value='1';
        }

        if (nPremdeal != ''){
            tcnPremfreq.value = insConvertNumber(hddnRatepayf.value)
                               * insConvertNumber(nPremdeal);
            tcnPremfreq.value = (Math.round(tcnPremfreq.value * 100) / 100);
            tcnPremfreq.value = VTFormat(tcnPremfreq.value, '', '', '', tcnPremfreq.DecimalPlace, true);
        }
        else tcnPremfreq.value = 0;
    }
}
</SCRIPT>
<%
Response.Write(mobjValues.StyleSheet())
If Request.QueryString.Item("Type") <> "PopUp" Then
	Response.Write(mobjMenu.setZone(2, "VA595", Request.QueryString.Item("sWindowDescript"), CShort(Request.QueryString.Item("nWindowTy"))))
	mobjMenu = Nothing
	Response.Write("<SCRIPT>var nMainAction=top.frames['fraSequence'].plngMainAction</SCRIPT>")
End If
%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" NAME="VA595" ACTION="ValPolicySeq.aspx?sMode=2">
<%
Response.Write(mobjValues.ShowWindowsName("VA595", Request.QueryString.Item("sWindowDescript")))
If Request.QueryString.Item("Type") <> "PopUp" Then
	Call InsInitial()
End If
Call InsDefineHeader()
If Request.QueryString.Item("Type") = "PopUp" Then
	Call InsPreVA595Upd()
Else
	Call InsPreVA595()
End If

mobjValues = Nothing
mobjGrid = Nothing
%>

</FORM> 
</BODY>
</HTML>
<%'^Begin Footer Block VisualTimer Utility 1.1 31/3/03 19.42.05
Call mobjNetFrameWork.FinishPage("VA595")
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>




