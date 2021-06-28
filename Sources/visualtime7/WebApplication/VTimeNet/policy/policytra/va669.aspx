<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="ePolicy" %>
<script language="VB" runat="Server">
'^Begin Header Block VisualTimer Utility 1.1 7/4/03 10.31.23
Dim mobjNetFrameWork As eNetFrameWork.Layout
'~End Header Block VisualTimer Utility

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'- Objeto para el manejo del grid de la página
Dim mobjGrid As eFunctions.Grid

'- Objeto para el manejo del menú
Dim mobjMenu As eFunctions.Menues

'- Objeto para valores de pagina	
Dim mobjActivelife As ePolicy.Activelife

'- Variable para almacenar la duración de la póliza
Dim mintPolYear As String


'% insPreVA669: Crea la pagina cuando no es PopUp
'-------------------------------------------------------------
Private Sub insPreVA669()
	'-------------------------------------------------------------
	
Response.Write("" & vbCrLf)
Response.Write("    <TABLE WIDTH=""100%"">" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("            <TD COLSPAN=""5"" CLASS=""HighLighted"">" & vbCrLf)
Response.Write("			    <LABEL ID=0><A NAME=""Datos Poliza"">" & GetLocalResourceObject("AnchorDatos PolizaCaption") & "</A></LABEL>" & vbCrLf)
Response.Write("            </TD>" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("            <TD COLSPAN=""5"" CLASS=""Horline""></TD>" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("			<TD><LABEL ID=0>" & GetLocalResourceObject("tctClientCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("			<TD>")


Response.Write(mobjValues.TextControl("tctClient", 0, mobjActivelife.sClient, False, GetLocalResourceObject("tctClientToolTip"), True))


Response.Write("</TD>" & vbCrLf)
Response.Write("			<TD>&nbsp;</TD>" & vbCrLf)
Response.Write("			<TD COLSPAN=""2"">")


Response.Write(mobjValues.TextControl("tctCliename", 0, mobjActivelife.sCliename, False, GetLocalResourceObject("tctClienameToolTip"), True))


Response.Write("</TD>" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("            <TD COLSPAN=""2"" CLASS=""HighLighted"">" & vbCrLf)
Response.Write("			    <LABEL ID=0><A NAME=""Vigencia"">" & GetLocalResourceObject("AnchorVigenciaCaption") & "</A></LABEL>" & vbCrLf)
Response.Write("            </TD>" & vbCrLf)
Response.Write("            <TD COLSPAN=""3"" CLASS=""HighLighted"">" & vbCrLf)
Response.Write("			    <LABEL ID=0><A NAME=""Prima"">" & GetLocalResourceObject("AnchorPrimaCaption") & "</A></LABEL>" & vbCrLf)
Response.Write("            </TD>" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("            <TD COLSPAN=""2"" CLASS=""Horline""></TD>" & vbCrLf)
Response.Write("            <TD COLSPAN=""3"" CLASS=""Horline""></TD>" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("			<TD WIDTH=""25%""><LABEL ID=0>" & GetLocalResourceObject("tcdFromCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("            <TD>")


Response.Write(mobjValues.DateControl("tcdFrom", CStr(mobjActivelife.dStartdate), False, GetLocalResourceObject("tcdFromToolTip"), True))


Response.Write(" " & vbCrLf)
Response.Write("            </TD>" & vbCrLf)
Response.Write("			<TD>&nbsp;</TD>" & vbCrLf)
Response.Write("			<TD WIDTH=""30%""><LABEL ID=0>" & GetLocalResourceObject("cbeCurrencyCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("            <TD>")


Response.Write(mobjValues.TextControl("cbeCurrency", 20, mobjActivelife.sCurrDescript,  , GetLocalResourceObject("cbeCurrencyToolTip"), True))


Response.Write("</TD> " & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("			<TD><LABEL ID=0>" & GetLocalResourceObject("tcdToCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("            <TD>")


Response.Write(mobjValues.DateControl("tcdTo", CStr(mobjActivelife.dExpirdat), False, GetLocalResourceObject("tcdToToolTip"), True))


Response.Write(" " & vbCrLf)
Response.Write("            </TD>" & vbCrLf)
Response.Write("			<TD>&nbsp;</TD>" & vbCrLf)
Response.Write("			<TD><LABEL ID=0>" & GetLocalResourceObject("tcnYearBasPremCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("            <TD>&nbsp;")


Response.Write(mobjValues.NumericControl("tcnYearBasPrem", 18, CStr(mobjActivelife.nPremiumbas), False, GetLocalResourceObject("tcnYearBasPremToolTip"), True, 6, True))


Response.Write("</TD> " & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("            <TD COLSPAN=""2"" CLASS=""Horline""></TD>" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("			<TD><LABEL ID=0>" & GetLocalResourceObject("tcnPremContributionCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("            <TD>")


Response.Write(mobjValues.NumericControl("tcnPremContribution", 18, CStr(mobjActivelife.nAmountcontr), False, GetLocalResourceObject("tcnPremContributionToolTip"), True, 6, True))


Response.Write("</TD> " & vbCrLf)
Response.Write("            </TD>" & vbCrLf)
Response.Write("			<TD>&nbsp;</TD>" & vbCrLf)
Response.Write("			<TD><LABEL ID=0>" & GetLocalResourceObject("tcnMinPremCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("            <TD>")


Response.Write(mobjValues.NumericControl("tcnMinPrem", 18, CStr(mobjActivelife.nPremimin), False, GetLocalResourceObject("tcnMinPremToolTip"), True, 6, True))


Response.Write("</TD> " & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("			<TD><LABEL ID=0>" & GetLocalResourceObject("tcdLastContribCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("            <TD>")


Response.Write(mobjValues.DateControl("tcdLastContrib", CStr(mobjActivelife.dLastContrib), False, GetLocalResourceObject("tcdLastContribToolTip"), True))


Response.Write("</TD>" & vbCrLf)
Response.Write("			<TD>&nbsp;</TD>" & vbCrLf)
Response.Write("			<TD><LABEL ID=0>" & GetLocalResourceObject("tcnProjPremCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("            <TD>")


Response.Write(mobjValues.NumericControl("tcnProjPrem", 18, CStr(mobjActivelife.nPremdeal), False, GetLocalResourceObject("tcnProjPremToolTip"), True, 6, True))


Response.Write("</TD> " & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("			<TD><LABEL ID=0>" & GetLocalResourceObject("tcdLastModCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("            <TD>")


Response.Write(mobjValues.DateControl("tcdLastMod", CStr(mobjActivelife.dLastMove), False, GetLocalResourceObject("tcdLastModToolTip"), True))


Response.Write(" " & vbCrLf)
Response.Write("            </TD>" & vbCrLf)
Response.Write("            <TD>&nbsp;</TD>" & vbCrLf)
Response.Write("   			<TD><LABEL ID=0>" & GetLocalResourceObject("tcnProjPremPayCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("            <TD>")


Response.Write(mobjValues.NumericControl("tcnProjPremPay", 18, CStr(mobjActivelife.nPremfreq), False, GetLocalResourceObject("tcnProjPremPayToolTip"), True, 6, True))


Response.Write("</TD> " & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("        ")

	If Request.QueryString.Item("nIllustType") = "1" Then
Response.Write("" & vbCrLf)
Response.Write("			<TD COLSPAN = 2></TD>" & vbCrLf)
Response.Write("            <TD>&nbsp;</TD>" & vbCrLf)
Response.Write("			<TD><LABEL ID=0>" & GetLocalResourceObject("tcnAddPremCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("            <TD>")


Response.Write(mobjValues.NumericControl("tcnAddPrem", 18, CStr(0),  , GetLocalResourceObject("tcnAddPremToolTip"), True, 6))


Response.Write("</TD> " & vbCrLf)
Response.Write("		")

	Else
Response.Write("          " & vbCrLf)
Response.Write("            <TD COLSPAN=""3"">&nbsp;</TD>  " & vbCrLf)
Response.Write("		")

	End If
Response.Write("" & vbCrLf)
Response.Write("		</TR>	" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("            <TD COLSPAN=""5"" CLASS=""HighLighted"">" & vbCrLf)
Response.Write("			    <LABEL ID=0><A NAME=""Datos Poliza"">" & GetLocalResourceObject("AnchorDatos Poliza2Caption") & "</A></LABEL>" & vbCrLf)
Response.Write("            </TD>" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("            <TD COLSPAN=""5"" CLASS=""Horline""></TD>" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("			<TD><LABEL ID=0>" & GetLocalResourceObject("tcnProjRentCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("            <TD>")


Response.Write(mobjValues.NumericControl("tcnProjRent", 5, CStr(mobjActivelife.nIntproject), False, GetLocalResourceObject("tcnProjRentToolTip"), True, 2))


Response.Write("</TD> " & vbCrLf)
Response.Write("            <TD>&nbsp;</TD>" & vbCrLf)
Response.Write("            <TD><LABEL ID=0>" & GetLocalResourceObject("tcnWarrRentCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("            <TD>")


Response.Write(mobjValues.NumericControl("tcnWarrRent", 5, CStr(mobjActivelife.nWarminint), False, GetLocalResourceObject("tcnWarrRentToolTip"), True, 2, True))


Response.Write("</TD> " & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("     </TABLE>")

	
	'+ 5 - Prima proyectada
	If Request.QueryString.Item("nIllustType") <> "5" Then
		
Response.Write("" & vbCrLf)
Response.Write("    <TABLE WIDTH=""100%"">" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("            <TD COLSPAN=""6"" CLASS=""HighLighted"">" & vbCrLf)
Response.Write("			    <LABEL ID=0><A NAME=""Rescates"">" & GetLocalResourceObject("AnchorRescatesCaption") & "</A></LABEL>" & vbCrLf)
Response.Write("            </TD>" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("            <TD COLSPAN=""6"" CLASS=""Horline""></TD>" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("	    	<TD WIDTH=""10%""><LABEL ID=0>" & GetLocalResourceObject("tcnSurrYearCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("            <TD WIDTH=""10%"">")


Response.Write(mobjValues.NumericControl("tcnSurrYear", 4, CStr(0), False, GetLocalResourceObject("tcnSurrYearToolTip")))


Response.Write("</TD> " & vbCrLf)
Response.Write("            <TD WIDTH=""10%""><LABEL ID=0>" & GetLocalResourceObject("tcnSurrMonthCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("            <TD WIDTH=""12%"">")


Response.Write(mobjValues.NumericControl("tcnSurrMonth", 2, CStr(0), False, GetLocalResourceObject("tcnSurrMonthToolTip")))


Response.Write("</TD> " & vbCrLf)
Response.Write("            <TD WIDTH=""25%""><LABEL ID=0>" & GetLocalResourceObject("tcnSurrAmountCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("            <TD WIDTH=""25%"">")


Response.Write(mobjValues.NumericControl("tcnSurrAmount", 18, CStr(0), False, GetLocalResourceObject("tcnSurrAmountToolTip"), True, 6))


Response.Write("</TD> " & vbCrLf)
Response.Write("        </TR>        " & vbCrLf)
Response.Write("    </TABLE>")

	Else
Response.Write("" & vbCrLf)
Response.Write("    <TABLE WIDTH=""100%"">" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("            <TD COLSPAN=""5"" CLASS=""HighLighted"">" & vbCrLf)
Response.Write("			    <LABEL ID=0><A NAME=""Prima proyectada"">" & GetLocalResourceObject("AnchorPrima proyectadaCaption") & "</A></LABEL>" & vbCrLf)
Response.Write("            </TD>" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("            <TD COLSPAN=""5"" CLASS=""Horline""></TD>" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("	    	<TD><LABEL ID=0>" & GetLocalResourceObject("tcnTargetVPCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("            <TD>")


Response.Write(mobjValues.NumericControl("tcnTargetVP", 18, CStr(0), False, GetLocalResourceObject("tcnTargetVPToolTip"), True, 6))


Response.Write("</TD> " & vbCrLf)
Response.Write("            <TD>")


Response.Write(mobjValues.AnimatedButtonControl("bntCalcProjPrem", "/VTimeNet/images/btnLargeNextOff.png", GetLocalResourceObject("bntCalcProjPremToolTip"), "", "insCalcProjPrem();", False))


Response.Write("</TD> " & vbCrLf)
Response.Write("            <TD><LABEL ID=0>" & GetLocalResourceObject("tcnProposalProjPremCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("            <TD>")


Response.Write(mobjValues.NumericControl("tcnProposalProjPrem", 18, CStr(0), False, GetLocalResourceObject("tcnProposalProjPremToolTip"), True, 6,  ,  ,  ,  , True))


Response.Write("</TD> " & vbCrLf)
Response.Write("        </TR>        " & vbCrLf)
Response.Write("    </TABLE>")

	End If
Response.Write("    " & vbCrLf)
Response.Write("    <TABLE WIDTH=""100%"">" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("            <TD COLSPAN=""3"" CLASS=""HighLighted"">" & vbCrLf)
Response.Write("			    <LABEL ID=0><A NAME=""Ilustracion"">" & GetLocalResourceObject("AnchorIlustracionCaption") & "</A></LABEL>" & vbCrLf)
Response.Write("            </TD>" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("            <TD COLSPAN=""3"" CLASS=""Horline""></TD>" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("            <TD>")


Response.Write(mobjValues.CheckControl("chkIllustPrint", GetLocalResourceObject("chkIllustPrintCaption"), CStr(0), CStr(1)))


Response.Write("</TD> " & vbCrLf)
Response.Write("            <TD>&nbsp;</TD>" & vbCrLf)
Response.Write("            <TD>")


Response.Write(mobjValues.CheckControl("chkShowIllustration", GetLocalResourceObject("chkShowIllustrationCaption"), CStr(0), "1"))


Response.Write("<TD>" & vbCrLf)
Response.Write("        </TR>        " & vbCrLf)
Response.Write("    </TABLE>")

	
	
	'+ 4 - Plan de Pago 
	If Request.QueryString.Item("nIllustType") = "4" Then
		Call insShowGrid()
	End If
	
	'+ Campos ocultos con informacion de cabecera
	Response.Write(mobjValues.HiddenControl("hddCertype", "2"))
	Response.Write(mobjValues.HiddenControl("hddBranch", Request.QueryString.Item("nBranch")))
	Response.Write(mobjValues.HiddenControl("hddProduct", Request.QueryString.Item("nProduct")))
	Response.Write(mobjValues.HiddenControl("hddPolicy", Request.QueryString.Item("nPolicy")))
	Response.Write(mobjValues.HiddenControl("hddCertif", Request.QueryString.Item("nCertif")))
	Response.Write(mobjValues.HiddenControl("hddEffecdate", Request.QueryString.Item("dEffecdate")))
	Response.Write(mobjValues.HiddenControl("hddIllustType", Request.QueryString.Item("nIllustType")))
	'+ Prima proyectada anual. 
	'+ Como campo original es de tipo gridfield (no se crea objeto HTML) se crea este 
	'+ campo oculto para recuperar valor desde javascript     
	Response.Write(mobjValues.HiddenControl("hddProjPrem", CStr(mobjActivelife.nPremdeal)))
	
	'- Fin insPreVA669 
End Sub


'% insDefineHeader: se definen las propiedades del grid
'--------------------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'--------------------------------------------------------------------------------------------
	mobjGrid = New eFunctions.Grid
	'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.31.23
	mobjGrid.sSessionID = Session.SessionID
	mobjGrid.nUsercode = Session("nUsercode")
	'~End Body Block VisualTimer Utility
	
	mobjGrid.sCodisplPage = "va669"
	Call mobjGrid.SetWindowParameters(Request.QueryString.Item("sCodispl"), Request.QueryString.Item("sWindowDescript"), Request.QueryString.Item("nWindowTy"))
	
	'+ Se definen las columnas del grid    
	With mobjGrid.Columns
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnIniYearColumnCaption"), "tcnIniYear", 5, "",  , GetLocalResourceObject("tcnIniYearColumnToolTip"))
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnEndYearColumnCaption"), "tcnEndYear", 5, "",  , GetLocalResourceObject("tcnEndYearColumnToolTip"))
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnYearPremColumnCaption"), "tcnYearPrem", 18, CStr(0),  , GetLocalResourceObject("tcnYearPremColumnToolTip"), True, 6)
		
		'+ Solo se crean cuando es popoup, porque de lo contrario ya existen en formulario principal		
		If Request.QueryString.Item("Type") = "PopUp" Then
			Call .AddHiddenColumn("hddBranch", Request.QueryString.Item("nBranch"))
			Call .AddHiddenColumn("hddProduct", Request.QueryString.Item("nProduct"))
			Call .AddHiddenColumn("hddPolicy", Request.QueryString.Item("nPolicy"))
			Call .AddHiddenColumn("hddCertif", Request.QueryString.Item("nCertif"))
			Call .AddHiddenColumn("hddEffecdate", Request.QueryString.Item("dEffecdate"))
			Call .AddHiddenColumn("hddIllustType", Request.QueryString.Item("nIllustType"))
			Call .AddHiddenColumn("hddPolYears", Request.QueryString.Item("nPolYears"))
		End If
		
	End With
	
	'+ Se definen las propiedades generales del grid
	
	With mobjGrid
		.Codispl = "VA669"
		.ActionQuery = mobjValues.ActionQuery
		.Height = 250
		.Width = 280
		.nMainAction = CShort(IIf(Request.QueryString.Item("nMainAction") ="", 0, Request.QueryString.Item("nMainAction")))
		.Columns("Sel").GridVisible = Not .ActionQuery
		If Request.QueryString.Item("Reload") = "1" Then
			.sReloadIndex = Request.QueryString.Item("ReloadIndex")
		End If
		.sEditRecordParam = "nBranch=" & Request.QueryString.Item("nBranch") & "&nProduct=" & Request.QueryString.Item("nProduct") & "&nPolicy=" & Request.QueryString.Item("nPolicy") & "&nCertif=" & Request.QueryString.Item("nCertif") & "&dEffecdate=" & Request.QueryString.Item("dEffecdate") & "&nIllustType=" & Request.QueryString.Item("nIllustType") & "&nPolYears=" & mintPolYear
		.sDelRecordParam = "nBranch=" & Request.QueryString.Item("nBranch") & "&nProduct=" & Request.QueryString.Item("nProduct") & "&nPolicy=" & Request.QueryString.Item("nPolicy") & "&nCertif=" & Request.QueryString.Item("nCertif") & "&dEffecdate=" & Request.QueryString.Item("dEffecdate") & "&nIniYear=' + marrArray[lintIndex].tcnIniYear + '"
		
	End With
End Sub

'% insReaInitials: Carga los valores por defecto de la ventana
'--------------------------------------------------------------------------------------------
Private Sub insReaInitial()
	'--------------------------------------------------------------------------------------------
	mobjActivelife = New ePolicy.Activelife
	With Request
		If mobjActivelife.insGetDataVA669("2", mobjValues.StringToType(.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.QueryString.Item("nPolicy"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.QueryString.Item("nCertif"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("dEffecdate"), eFunctions.Values.eTypeData.etdDate, True)) Then
		End If
		mintPolYear = CStr(mobjActivelife.nPolYears)
	End With
End Sub

'% insShowGrid: se realiza el manejo del grid
'--------------------------------------------------------------------------------------------
Private Sub insShowGrid()
	'--------------------------------------------------------------------------------------------
	Dim lclsPer_deposit As Object
	'- Objeto para el manejo de plan de pago
	Dim mcolPer_Deposit As ePolicy.Per_deposits
	
	mcolPer_Deposit = New ePolicy.Per_deposits
	
Response.Write("" & vbCrLf)
Response.Write("    <TABLE WIDTH=""100%"">" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("            <TD CLASS=""HighLighted"">" & vbCrLf)
Response.Write("			    <LABEL ID=0><A NAME=""Plan de pago"">" & GetLocalResourceObject("AnchorPlan de pagoCaption") & "</A></LABEL>" & vbCrLf)
Response.Write("            </TD>" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("            <TD CLASS=""Horline""></TD>" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("    </TABLE>")

	
	If mcolPer_Deposit.Find("2", mobjValues.StringToType(Request.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nCertif"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("dEffecdate"), eFunctions.Values.eTypeData.etdDate)) Then
		For	Each lclsPer_deposit In mcolPer_Deposit
			With mobjGrid
				.Columns("tcnIniYear").DefValue = lclsPer_deposit.nYear_ini
				.Columns("tcnEndYear").DefValue = lclsPer_deposit.nYear_end
				.Columns("tcnYearPrem").DefValue = lclsPer_deposit.nAmountdep
				Response.Write(.DoRow)
			End With
		Next lclsPer_deposit
	End If
	
	Response.Write(mobjGrid.closeTable())
	mcolPer_Deposit = Nothing
End Sub

'% insPreVA669Upd: Se realiza el manejo de la ventana PopUp asociada al grid
'--------------------------------------------------------------------------------------------
Private Sub insPreVA669Upd()
	'--------------------------------------------------------------------------------------------
	Dim lclsPer_deposit As ePolicy.Per_deposit
	
	With Request
		If Request.QueryString.Item("Action") = "Del" Then
			Response.Write(mobjValues.ConfirmDelete())
			lclsPer_deposit = New ePolicy.Per_deposit
			If lclsPer_deposit.InsPostVA595Upd("Del", "2", mobjValues.StringToType(.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nCertif"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nIniYear"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType("", eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType("", eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType("", eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType("1", eFunctions.Values.eTypeData.etdDouble), "VA669") Then
			End If
			lclsPer_deposit = Nothing
		End If
		Response.Write(mobjGrid.DoFormUpd(.QueryString.Item("Action"), "valPolicyTra.aspx", "VA669", .QueryString.Item("nMainAction"), mobjValues.ActionQuery, CShort(.QueryString.Item("Index"))))
	End With
End Sub

</script>
<%Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage("va669")

mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.31.23
mobjValues.sSessionID = Session.SessionID
mobjValues.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjValues.sCodisplPage = "va669"
mobjMenu = New eFunctions.Menues
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.31.23
mobjMenu.sSessionID = Session.SessionID
mobjMenu.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjValues.ActionQuery = CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 401
%>
<HTML>
<HEAD>
<SCRIPT>
//- Variable para el control de versiones
    document.VssVersion="$$Revision: 3 $|$$Date: 15/10/03 16:53 $|$$Author: Nvaplat61 $"
</SCRIPT>
<META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>




<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE=JavaScript>
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
<%
'+ 5 - Prima proyectada
If Request.QueryString.Item("nIllustType") = "5" Then
	%>
//% insCalcProjPrem: Calcula la prima 
//--------------------------------------------------------------------------------------------
function insCalcProjPrem(){
//--------------------------------------------------------------------------------------------
	var lstrParams = new String;
<%	
	With Request
		Response.Write("var lstrBranch='" & .QueryString.Item("nBranch") & "';")
		Response.Write("var lstrProduct='" & .QueryString.Item("nProduct") & "';")
		Response.Write("var lstrPolicy='" & .QueryString.Item("nPolicy") & "';")
		Response.Write("var lstrCertif='" & .QueryString.Item("nCertif") & "';")
		Response.Write("var ldatEffecdate='" & .QueryString.Item("dEffecdate") & "';")
	End With
	%>
    with(self.document.forms[0]){
        lstrParams += 'sCertype=2&nBranch=' + lstrBranch +
                      '&nProduct=' + lstrProduct +
                      '&nPolicy=' + lstrPolicy +
                      '&nCertif=' + lstrCertif +
                      '&dEffecdate=' + ldatEffecdate +
                      '&nTargetPremium=' + hddProjPrem.value +
                      '&nTargetVP=' + tcnTargetVP.value;
    }
	top.frames['fraHeader'].setPointer('wait');
	insDefValues('SuggestPrem', lstrParams,'/VTimeNet/Policy/PolicyTra');
}
<%End If%>
</SCRIPT>
<%
Response.Write(mobjValues.StyleSheet())
If Request.QueryString.Item("Type") <> "PopUp" Then
	Response.Write(mobjMenu.setZone(2, "VA669", Request.QueryString.Item("sWindowDescript"), CShort(Request.QueryString.Item("nWindowTy"))))
	mobjMenu = Nothing
	Response.Write("<SCRIPT>var nMainAction=top.frames['fraSequence'].plngMainAction</SCRIPT>")
End If
%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" NAME="VA669" ACTION="valPolicyTra.aspx?sMode=2">
    <%Response.Write(mobjValues.ShowWindowsName("VA669", Request.QueryString.Item("sWindowDescript")))
If Request.QueryString.Item("Type") <> "PopUp" Then
	Call insReaInitial()
End If
Call insDefineHeader()
If Request.QueryString.Item("Type") = "PopUp" Then
	Call insPreVA669Upd()
Else
	Call insPreVA669()
End If
%>
</FORM> 
</BODY>
</HTML>
<%
mobjActivelife = Nothing
mobjValues = Nothing
mobjGrid = Nothing
%>
<%'^Begin Footer Block VisualTimer Utility 1.1 7/4/03 10.31.23
Call mobjNetFrameWork.FinishPage("va669")
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>




