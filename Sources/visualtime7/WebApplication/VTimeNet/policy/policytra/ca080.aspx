<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="ePolicy" %>
<%@ Import namespace="eProduct" %>
<%@ Import namespace="eGeneral" %>
<script language="VB" runat="Server">

    '^Begin Header Block VisualTimer Utility 1.1 7/4/03 10.31.19
    Dim mobjNetFrameWork As eNetFrameWork.Layout
    '~End Header Block VisualTimer Utility

    '- Objeto para el manejo de las funciones generales de carga de valores
    Dim mobjValues As eFunctions.Values

    '- Objeto para el manejo de las rutinas genéricas
    Dim mobjMenu As eFunctions.Menues

    '- Se define la variable mobjGrid para el manejo del grid de la ventana
    Dim mobjGrid As eFunctions.Grid


    '- Se define las variables para el manejo del Grid de la ventana
    Dim mclsTDetail_pre As ePolicy.TDetail_pre

    '- Se define la variable para consultar el tipo de producto de vida
    Dim mclsProduct_li As eProduct.Product

    '- Se define variable para almacenar QueryString
    Dim lstrQueryString As String

    Dim lstrQueryString2 As String

    Dim lstrCertype As Object
    Dim lstrBranch As Object
    Dim lstrProduct As Object
    Dim lstrPolicy As Object
    Dim lstrCertif As Object
    '	Dim lstrNullDate    
    '	Dim lstrNullReceipt 
    Dim lstrExeMode As String
    Dim lstrExeReport As String
    'Dim lstrAgency      
    Dim lstrCodisplOrig As String
    Dim lstrCodispl As String
    Dim lstrOnSeq As String
    Dim lstrNewData As String
    Dim lstrPolitype As String
    Dim lstrExist As String
    Dim ldblCapital As Object
    Dim ldtmPolStartDate As Object
    Dim ldblCerPremium As Object
    Dim ldtmPolExpirdat As Object
    Dim lstrClient As String
    Dim ldtmEffecdate As Object
    Dim ldtmEffecdateIni As Object
    Dim lstrTypeReceipt As Object
    Dim ldtmExpirReceipt As Object
    Dim llngReceipt As Object
    Dim lstrOrigReceipt As String
    Dim llngCurrency As Object
    Dim llngTratypei As Object
    Dim ldtmIssuedat As Object
    Dim lstrKey As String
    Dim lstrAdjust As String
    Dim lstrAdjReceipt As String
    Dim lstrAdjAmount As String
    Dim lstrTypePay As String
    Dim llngPayfreq As Object
    Dim ldblPremiumOri As String
    Dim ldblBalanceOri As String
    Dim llngProdClas As Object

    '+ Vriable para ser usadas si la ventana se encuentra dentro de la secuencia
    Dim mblnError As Boolean
    Dim mblnSequence As Boolean

    Dim mclsPolicy_his As Object

    Dim nCertif_aux As Double
    Dim ldtmEffecdate_aux As Object
    Dim mstrRecDevEqualColl As String
    Dim mstrDisabled As Boolean
    Dim mlbnDisabled As Boolean
    Dim mdblPorcCommision As Double

    Dim mclsRoles As ePolicy.Roles
    Dim mclsPolicy As ePolicy.Policy

    '%insLoadParameterQS: Valores recuperados tras recargar la ventana
    '--------------------------------------------------------------------------------------------
    Private Sub insLoadParameterQS()
        '--------------------------------------------------------------------------------------------	
        lstrCertype = Request.QueryString.Item("sCertype")
        If lstrCertype = "" Then lstrCertype = Session("sCertype")
        lstrBranch = Request.QueryString.Item("nBranch")
        If lstrBranch = "" Then lstrBranch = Session("nBranch")
        lstrProduct = Request.QueryString.Item("nProduct")
        If lstrProduct = "" Then lstrProduct = Session("nProduct")
        lstrPolicy = Request.QueryString.Item("nPolicy")
        If lstrPolicy = "" Then lstrPolicy = Session("nPolicy")
        lstrCertif = Request.QueryString.Item("nCertif")
        If lstrCertif = "" Then lstrCertif = Session("nCertif")
        If lstrCertif = "" Then lstrCertif = 0
        ldblCapital = Request.QueryString.Item("nCapitalPol")
        ldtmPolStartDate = Request.QueryString.Item("dStartPolicy")
        ldtmPolExpirdat = Request.QueryString.Item("dExpirPolicy")
        ldblCerPremium = Request.QueryString.Item("nPremiumCer")
        lstrClient = Request.QueryString.Item("sClient")
        '   lstrNullDate    = Request.QueryString("dNullDate")
        '	lstrNullReceipt = Request.QueryString("sNullReceipt")
        lstrTypeReceipt = Request.QueryString.Item("sTypeReceipt")
        lstrOrigReceipt = Request.QueryString.Item("sOrigReceipt")
        lstrExeMode = Request.QueryString.Item("nExeMode")
        'lstrExeReport = Request.QueryString.Item("sExeReport")
        lstrExeReport = "1"
        '	lstrAgency      = Request.QueryString("nAgency") 
        llngTratypei = mobjValues.StringToType(Request.QueryString.Item("nTratypei"), eFunctions.Values.eTypeData.etdLong)
        llngCurrency = mobjValues.StringToType(Request.QueryString.Item("nCurrency"), eFunctions.Values.eTypeData.etdLong)
        lstrCodisplOrig = Request.QueryString.Item("sCodisplOrig")
        lstrOnSeq = Request.QueryString.Item("sOnSeq")
        lstrCodispl = Request.QueryString.Item("sCodispl")
        lstrNewData = Request.QueryString.Item("sNewData")
        lstrKey = Request.QueryString.Item("sKey")
        ldtmEffecdate = mobjValues.StringToType(Request.QueryString.Item("dEffecdate"), eFunctions.Values.eTypeData.etdDate)
        ldtmEffecdateIni = mobjValues.StringToType(Request.QueryString.Item("dEffecdateIni"), eFunctions.Values.eTypeData.etdDate)
        ldtmExpirReceipt = mobjValues.StringToType(Request.QueryString.Item("dExpirdate"), eFunctions.Values.eTypeData.etdDate)
        lstrTypePay = Request.QueryString.Item("nTypePay")
        lstrAdjust = "1" '---Request.QueryString("sAdjust")
        lstrAdjReceipt = Request.QueryString.Item("nAdjReceipt")
        lstrAdjAmount = Request.QueryString.Item("nAdjAmount")
        ldtmIssuedat = Today
        ldblPremiumOri = mobjValues.StringToType(Request.QueryString.Item("nPremiumOri"), eFunctions.Values.eTypeData.etdDouble)
        ldblBalanceOri = mobjValues.StringToType(Request.QueryString.Item("nBalanceOri"), eFunctions.Values.eTypeData.etdDouble)

    End Sub

    '%insLoadParameterBD: Valores obtenidos de la BD al carga por primera vez 
    '--------------------------------------------------------------------------------------------
    Private Sub insLoadParameterBD()
        '--------------------------------------------------------------------------------------------	
        lstrPolitype = mclsTDetail_pre.mclsPolicy.sPolitype
        lstrExist = mclsTDetail_pre.sExist
        ldblCapital = mclsTDetail_pre.mclsPolicy.nCapital
        ldtmPolStartDate = mclsTDetail_pre.mclsCertificat.dStartdate
        ldblCerPremium = mclsTDetail_pre.mclsCertificat.nPremium
        ldtmPolExpirdat = mclsTDetail_pre.mclsCertificat.dExpirdat
        lstrClient = mclsTDetail_pre.mclsCertificat.sClient
        lstrTypeReceipt = mclsTDetail_pre.nTypeReceipt
        ldtmEffecdate = mclsTDetail_pre.mclsCertificat.dNextReceip
        ldtmEffecdateIni = ldtmEffecdate

        '+Se calcula fecha de termino del recibo. Se pasa nulo
        Call mclsTDetail_pre.mclsCertificat.insCalcPeriodDates(ldtmEffecdate, mclsTDetail_pre.mclsCertificat.nPayfreq, mclsTDetail_pre.mclsCertificat.sFracreceip, "", mclsTDetail_pre.mclsCertificat.dExpirdat)
        ldtmExpirReceipt = mclsTDetail_pre.mclsCertificat.dEndCurrentPeriod

        'llngReceipt = mclsTDetail_pre.nReceipt
        'llngReceipt = mclsTDetail_pre.mclsPremium.nReceipt
        llngCurrency = mclsTDetail_pre.nCurrency
        'llngTratypei     = mclsTDetail_pre.nTratypei
        ldtmIssuedat = mclsTDetail_pre.dIssuedat
        lstrKey = mclsTDetail_pre.mcolTDetail_pre.sKey(Session("nUsercode"), Session("SessionID"))

    End Sub

    '% insDefineGrid: se definen las características del grid
    '--------------------------------------------------------------------------------------------
    Private Sub insDefineGrid()
        '--------------------------------------------------------------------------------------------	
        '+ Se definen todas las columnas del Grid
        With mobjGrid.Columns


            'Call .AddClientColumn(2905,"Asegurado", "dtcClient", vbNullString, ,"Código del asegurado asociado al detalle a facturar",,True,,,,,,,,,,2)
            Call .AddNumericColumn(2906, "Código", "tcnCodeItem", 5, vbNullString,, "Código del detalle a facturar (cobertura, recargo, descuento, impuesto)",,,,,, True)
            Call .AddPossiblesColumn(2907, "Tipo", "cbeType", "Table298", eFunctions.Values.eValuesType.clngComboType, 0,,,,,, True,, "Tipo de detalle a facturar (cobertura, recargo, descuento, impuesto)")
            Call .AddNumericColumn(0, "Certif.", "tcnCertif", 10, vbNullString, False, "Certificado al que pertenece el movimiento", True, 0,,,, True)
            Call .AddTextColumn(0, "Módulo", "tctModule", 12, vbNullString,, "Descripción abreviada de los módulos existentes en la póliza o certificado",,,, True)
            Call .AddTextColumn(2908, "Elemento", "tctElement", 20, vbNullString,, "Descripción abreviada de las coberturas, recargos, descuentos e impuestos existentes en la póliza o certificado",,,, True)
            Call .AddPossiblesColumn(2909, "Tipo de desglose", "cbePrem_det", "Table5651", eFunctions.Values.eValuesType.clngComboType, 3,,,,, "changevaluesField(""Prem_det"",this)",,, "Tipo de desglose a manejar en la prima")
            Call .AddAnimatedColumn(2910, "Distribución de prima", "btnPrem_det", "/VTimeNet/Images/Window_dolarOff.gif", "Desglose de prima del recibo",, "showDetai();")
            Call .AddNumericColumn(2911, "Capital", "tcnCapital", 18, vbNullString, False, "Monto de capital correspondiente al detalle", True, 6,,,, True)
            Call .AddNumericColumn(2912, "Prima a facturar (Afecta)", "tcnPremiumA", 18, vbNullString, False, "Monto de prima afecta a facturar", True, 6,,, "changevaluesField(""Premium"",this)",,, True)
            Call .AddNumericColumn(2913, "Prima a facturar (Exenta)", "tcnPremiumE", 18, vbNullString, False, "Monto de prima exenta a facturar", True, 6,,, "changevaluesField(""Premium"",this)")
            Call .AddNumericColumn(2913, "Prima de la cobertura", "tcnPremium", 18, vbNullString, False, "Monto de prima de la cobertura", True, 6,,,, True)
            Call .AddNumericColumn(2914, "% Comisión", "tcnCommi_rate", 4, vbNullString, False, "Porcentaje de comisión correspondiente a la prima a facturar", True, 2,,,, True)
            Call .AddNumericColumn(2915, "Comisión fija", "tcnCommission", 18, vbNullString, False, "Monto de comisión fija correspondiente a la prima a facturar", True, 6,,,, True)
            Call .AddHiddenColumn("hddAddTax", vbNullString)
            Call .AddHiddenColumn("hddBill_item", 0)
            Call .AddHiddenColumn("hddBranch_est", 0)
            Call .AddHiddenColumn("hddBranch_led", 0)
            Call .AddHiddenColumn("hddBranch_rei", 0)
            Call .AddHiddenColumn("hddModulec", 0)
            Call .AddHiddenColumn("hddAddsuini", vbNullString)
            Call .AddHiddenColumn("hddCacalili", vbNullString)
            Call .AddHiddenColumn("hddCommissi_i", vbNullString)
            Call .AddHiddenColumn("hddId_Bill", vbNullString)
            Call .AddHiddenColumn("hddPrem_det_proc", "2")
            Call .AddHiddenColumn("hddPrem_det_old", vbNullString)
            Call .AddHiddenColumn("hddExistColum", "2")
            Call .AddHiddenColumn("dtcClient", vbNullString)

        End With

        With mobjGrid
            .Codispl = Request.QueryString.Item("sCodispl")
            .Width = 500
            .Height = 550
            .Top = 50

            If Request.QueryString.Item("nRecDevEqualColl") = "1" And Request.QueryString.Item("nReceipt_Collec") <> "" Then
                .Columns("cbeType").EditRecord = False
            Else
                .Columns("cbeType").EditRecord = True
            End If

            .Columns("cbePrem_det").BlankPosition = False
            .Columns("Sel").OnClick = "insSelected(this)"
            .DeleteButton = False
            .AddButton = False
            .DeleteScriptName = vbNullString
            .MoveRecordScript = "changevaluesField(""InitialPopUp"");"

            .sEditRecordParam = "sCodisplOrig=" & Request.QueryString.Item("sCodisplOrig") &
                            "&sCertype=" & Request.QueryString.Item("sCertype") &
                            "&nBranch=" & Request.QueryString.Item("nBranch") &
                            "&nProduct=" & Request.QueryString.Item("nProduct") &
                            "&nPolicy=" & Request.QueryString.Item("nPolicy") &
                            "&nCertif=" & Request.QueryString.Item("nCertif") &
                            "&dNullDate=" & Request.QueryString.Item("dNullDate") &
                            "&sNullReceipt=" & Request.QueryString.Item("sNullReceipt") &
                            "&soptReceipt=' + (self.document.forms[0].optType[0].checked ? self.document.forms[0].optType[0].value : self.document.forms[0].optType[1].value) + '" &
                            "&nExeMode=" & Request.QueryString.Item("nExeMode") &
                            "&sExeReport=" & Request.QueryString.Item("sExeReport") &
                            "&nAgency=" & Request.QueryString.Item("nAgency") &
                            "&sOnSeq=" & Request.QueryString.Item("sOnSeq") &
                            "&nReceiptGrid=' + self.document.forms[0].tcnReceipt.value + '" &
                            "&nExists=' + self.document.forms[0].hddExists.value + '" &
                            "&nExists=' + self.document.forms[0].hddId_Bill.value + '" &
                            "&nPremium_Collect=' + self.document.forms[0].tcnPremium_Collec.value + '" &
                            "&nReceipt_Collec=" + Request.QueryString.Item("nReceipt_Collec") &
                            "&dEffecdate=' + self.document.forms[0].tcdStartDateR.value + '" &
                            "&nTypeReceipt=' + (self.document.forms[0].optType[0].checked ? self.document.forms[0].optType[0].value : self.document.forms[0].optType[1].value) + '" &
                            "&nContrat=" & Request.QueryString.Item("nContrat") &
                            "&nCoupon=" & Request.QueryString.Item("nCoupon") &
                            "&nCouponAmount=" & Request.QueryString.Item("nCouponAmount")


            If Request.QueryString.Item("sOnSeq") <> "1" Then
                .sEditRecordParam = .sEditRecordParam + "&sClient=' + self.document.forms[0].tctClient.value + '"
            Else
                .sEditRecordParam = .sEditRecordParam + "&hddClient=' + self.document.forms[0].hddClient_policy.value + '"
            End If

            Call .Splits_Renamed.AddSplit(-1, vbNullString, 6)
            Call .Splits_Renamed.AddSplit(2916, "Facturación", 5)
            If Request.QueryString.Item("Reload") = "1" Then
                .sReloadIndex = Request.QueryString.Item("ReloadIndex")
            End If
            .CancelScript = "CancelSelec();"
        End With

        With Response
            .Write(mobjValues.HiddenControl("hddType", 1))
            .Write(mobjValues.HiddenControl("hddCurrency", 0))
            .Write(mobjValues.HiddenControl("hddStartDateR", vbNullString))
            .Write(mobjValues.HiddenControl("hddExpirDate", vbNullString))
            .Write(mobjValues.HiddenControl("hddIssueDate", vbNullString))
            .Write(mobjValues.HiddenControl("hddSource", vbNullString))
            .Write(mobjValues.HiddenControl("hddClient", vbNullString))
            .Write(mobjValues.HiddenControl("hddOrigReceipt", vbNullString))
            .Write(mobjValues.HiddenControl("hddExists", vbNullString))
            .Write(mobjValues.HiddenControl("hddReceipt", vbNullString))
            .Write(mobjValues.HiddenControl("hddReceiptCollect", vbNullString))
            .Write(mobjValues.HiddenControl("hddVerif", True))
        End With
    End Sub

    '%insPreCA080: Esta función se encarga de cargar los datos en la forma "Folder" 
    '--------------------------------------------------------------------------------------------
    Private Sub insPreCA080()
        '--------------------------------------------------------------------------------------------
        Dim lclsTDetail_pre As ePolicy.TDetail_pre
        Dim lintCount As Double
        Dim lintIndex As Short
        Dim lstrType_detai As Object
        Dim lintCodeItem As Integer
        Dim ldblAmount As Object

        Dim bDisabledClient As Boolean
        Dim lstrClientAux As String
        Dim ldtmEffecdateR As Date
        Dim nDefReceipt As Double

        'mblnSequence = False

        If Not mblnSequence And Request.QueryString.Item("sOnSeq") <> "1" Then

            Response.Write("    " & vbCrLf)
            Response.Write("    <P ALIGN=""Center"">" & vbCrLf)
            Response.Write("    <LABEL ID=9362><A HREF=""ca080.aspx#""> Datos del recibo</A></LABEL>")
            Response.Write("    <TABLE WIDTH = ""100%"" >")
            Response.Write("        <TR>" & vbCrLf)
            Response.Write("            <TD COLSPAN = ""5"" Class=""HighLighted""><LABEL ID=""9363"">Datos de la póliza</LABEL></TD>")
            Response.Write("        </TR>" & vbCrLf)
            Response.Write("        <TR>" & vbCrLf)
            Response.Write("            <TD Class=""HorLine"" COLSPAN=""5""></TD>")
            Response.Write("        </TR>" & vbCrLf)
            Response.Write("        <TR>" & vbCrLf)
            Response.Write("            <TD COLSPAN = ""2"" Class=""HighLighted""><LABEL ID=""9364"">Vigencia</LABEL></TD>")
            Response.Write("            <TD WIDTH = ""5%"" >&nbsp;</TD>" & vbCrLf)
            Response.Write("            <TD> <Label ID =""9365""> Capital asegurado</LABEL></TD>")
            Response.Write("            <TD>")

            Response.Write(mobjValues.NumericControl("tcnCapital_policy", 18, mclsTDetail_pre.mclsPolicy.NCAPITAL,, "Capital asegurado de la póliza o certificado", True, 6, True))

            Response.Write("            </TD>" & vbCrLf)
            Response.Write("        </TR>" & vbCrLf)
            Response.Write("        <TR>" & vbCrLf)
            Response.Write("            <TD Class=""HorLine"" COLSPAN=""2""></TD>")
            Response.Write("            <TD COLSPAN = ""3"" ></TD>")
            Response.Write("        </TR>" & vbCrLf)
            Response.Write("        <TR>" & vbCrLf)
            Response.Write("            <TD WIDTH = ""15%""><Label ID=""9366"">Desde</LABEL></TD>")
            Response.Write("            <TD>")

            Response.Write(mobjValues.DateControl("tcdStartDate_policy", mclsTDetail_pre.mclsCertificat.dStartdate,, "Fecha desde o de inicio de vigencia de la póliza o certificado", True))

            Response.Write("            </TD>" & vbCrLf)
            Response.Write("            <td>&nbsp;</td>" & vbCrLf)
            Response.Write("            <TD> <Label ID =""9367""> Prima neta anual</LABEL></TD>")
            Response.Write("            <TD>")

            Response.Write(mobjValues.NumericControl("tcnNetPremium_policy", 18, mclsTDetail_pre.mclsCertificat.nPremium,, "Prima neta anual (de la coberturas) de la póliza o certificado", True, 6, True))

            Response.Write("            </TD>" & vbCrLf)
            Response.Write("        </TR>" & vbCrLf)

            Response.Write("        <TR>" & vbCrLf)
            Response.Write("            <TD> <Label ID=""9368""> Hasta</LABEL></TD>")
            Response.Write("            <TD>")

            Response.Write(mobjValues.DateControl("tcdExpirdate_policy", mclsTDetail_pre.mclsCertificat.dExpirdat,, "Fecha hasta o de fin de vigencia de la póliza o certificado", True))

            Response.Write("            </TD>" & vbCrLf)
            Response.Write("            <td>&nbsp;</td>" & vbCrLf)
            Response.Write("            <TD> <Label ID =""9908""> Titular</LABEL></TD>")
            Response.Write("            <TD>")

            If Session("dEffecdate") = "" Then
                If Request.QueryString.Item("dEffecdate") <> vbNullString Then
                    ldtmEffecdate = mobjValues.TypeToString(Request.QueryString.Item("dEffecdate"), eFunctions.Values.eTypeData.etdDate)
                Else
                    ldtmEffecdate = mobjValues.TypeToString(mclsTDetail_pre.mclsCertificat.dStartdate, eFunctions.Values.eTypeData.etdDate)
                End If
            Else
                ldtmEffecdate = Session("dEffecdate")
            End If

            mobjValues.TypeList = 2
            mobjValues.ClientRole = "13"

            '%+ Si es un Certificado y la póliza tiene una facturacion por "Póliza" o por "Situación de Riesgo" se deben mostrar 
            '%+ las figuras de la Matriz para generar el recibo.

            If CDbl(Session("nCertif")) > 0 And
                (mclsTDetail_pre.mclsPolicy.sColinvot = "1" Or
                mclsTDetail_pre.mclsPolicy.sColinvot = "3") Then
                nCertif_aux = 0
            Else
                nCertif_aux = Session("nCertif")
            End If

            lstrQueryString2 = "&sCertype= " & Session("sCertype") &
  "&nBranch=" & Session("nBranch") &
  "&nProduct=" & Session("nProduct") &
  "&nPolicy=" & Session("nPolicy") &
  "&nCertif=" & nCertif_aux &
  "&dEffecdate=" & ldtmEffecdate

            If Request.QueryString.Item("sClient") <> vbNullString And Request.QueryString.Item("sClient") <> mclsTDetail_pre.mclsCertificat.sClient Then

                If Request.QueryString.Item("sChangeDate") = "1" Then
                    If CDbl(Session("nCertif")) > 0 And
                        (mclsTDetail_pre.mclsPolicy.sColinvot = "1" Or
                        mclsTDetail_pre.mclsPolicy.sColinvot = "3") Then
                        lstrClientAux = mclsTDetail_pre.mclsPolicy.SCLIENT
                    Else
                        lstrClientAux = mclsTDetail_pre.mclsCertificat.sClient
                    End If
                Else
                    lstrClientAux = Request.QueryString.Item("sClient")
                End If

            Else
                If CDbl(Session("nCertif")) > 0 And
                (mclsTDetail_pre.mclsPolicy.sColinvot = "1" Or
                mclsTDetail_pre.mclsPolicy.sColinvot = "3") Then
                    lstrClientAux = mclsTDetail_pre.mclsPolicy.SCLIENT
                Else
                    lstrClientAux = mclsTDetail_pre.mclsCertificat.sClient
                End If
            End If

            If Request.QueryString.Item("soptReceipt") <> vbNullString Then
                If Request.QueryString.Item("soptReceipt") = "1" Then
                    bDisabledClient = False
                Else
                    bDisabledClient = True
                End If
            Else
                If mclsTDetail_pre.nTypeReceipt = 1 Then
                    bDisabledClient = False
                Else
                    bDisabledClient = True
                End If
            End If

            Response.Write(mobjValues.ClientControl("tctClient", lstrClientAux,, "Código que identifica al titular del recibo", "changevaluesField(""Client"",this)", bDisabledClient,,,,,,, 6,,, lstrQueryString2))

            Response.Write("            </TD>" & vbCrLf)
            Response.Write("        </TR>" & vbCrLf)
            Response.Write("        <TR>" & vbCrLf)
            Response.Write("            <TD COLSPAN = ""5"" Class=""HighLighted""><LABEL ID=""9370""><A NAME=""Datos del recibo"">Datos del recibo</A></LABEL></TD>")
            Response.Write("        </TR>" & vbCrLf)
            Response.Write("        <TR>" & vbCrLf)
            Response.Write("            <TD Class=""HorLine"" COLSPAN=""5""></TD>")
            Response.Write("        </TR>" & vbCrLf)
            Response.Write("    </TABLE>")

            Response.Write("    <Table WIDTH = ""100%"" >")
            Response.Write("        <TR>" & vbCrLf)
            Response.Write("            <TD><LABEL ID=""9371"">Ramo</LABEL></TD>")
            Response.Write("            <TD>")

            Response.Write(mobjValues.PossiblesValues("cbeBranch", "Table10", eFunctions.Values.eValuesType.clngComboType, Session("nBranch"),, True,,,,,,, "Ramo al que pertenece la póliza"))

            Response.Write("            </TD>" & vbCrLf)
            Response.Write("            <TD WIDTH=""10%"">&nbsp;</TD>")
            Response.Write("            <TD><LABEL ID=""9372"">Póliza</LABEL></TD>")
            Response.Write("            <TD>")
            Response.Write(mobjValues.TextControl("tcnPolicy", 30, Session("nPolicy"),, "Número de póliza", True) & " / " & mobjValues.TextControl("tcnCertif", 30, Session("nCertif"),, "Número de certificado", True))
            Response.Write("            </TD>" & vbCrLf)
            Response.Write("        </TR>" & vbCrLf)
            Response.Write("    </Table>")

        End If

        Response.Write("    <Table WIDTH = ""100%"" >")
        Response.Write("        <TR>" & vbCrLf)
        Response.Write("            <TD COLSPAN = ""2"" Class=""HighLighted""><LABEL ID=9374>Vigencia</LABEL></TD>")
        Response.Write("            <TD>&nbsp;</TD>")
        Response.Write("            <TD COLSPAN = ""5"" Class=""HighLighted""><LABEL ID=9375>Tipo</LABEL></TD>")
        Response.Write("        </TR>")
        Response.Write("        <TR>" & vbCrLf)
        Response.Write("            <TD COLSPAN = ""2"" Class=""HorLine""></TD>")
        Response.Write("            <TD></TD>")
        Response.Write("            <TD COLSPAN = ""5"" Class=""HorLine""></TD>")
        Response.Write("        </TR>")

        If Not mblnSequence And Request.QueryString.Item("sOnSeq") <> "1" Then

            'If mobjValues.TypeToString(Request.QueryString("dDateFrom"), eFunctions.Values.eTypeData.etdDate) <> eRemoteDB.Constants.dtmNull Then
            If Request.QueryString.Item("dDateFrom") <> eRemoteDB.Constants.strNull Then
                ldtmEffecdateR = mobjValues.TypeToString(Request.QueryString.Item("dDateFrom"), eFunctions.Values.eTypeData.etdDate)
            Else
                'If mobjValues.TypeToString(Request.QueryString("dEffecdate"), eFunctions.Values.eTypeData.etdDate) <> eRemoteDB.Constants.dtmNull Then
                If Request.QueryString.Item("dEffecdate") <> eRemoteDB.Constants.strNull Then
                    ldtmEffecdateR = Request.QueryString.Item("dEffecdate")
                Else
                    If mobjValues.TypeToString(mclsTDetail_pre.dEffecdate, eFunctions.Values.eTypeData.etdDate) <> eRemoteDB.Constants.dtmNull Then
                        ldtmEffecdateR = mclsTDetail_pre.dEffecdate
                    Else
                        ldtmEffecdateR = mclsTDetail_pre.mclsCertificat.dStartdate
                    End If

                End If
            End If
        Else
            ldtmEffecdateR = Session("dEffecdate")
        End If

        Response.Write("        <TR>" & vbCrLf)
        Response.Write("            <TD><LABEL ID =""9376"" > Desde</LABEL></TD>")
        Response.Write("            <TD>")
        Response.Write(mobjValues.DateControl("tcdStartDateR", ldtmEffecdateR,, "Fecha de emisión del recibo",,,, "changevaluesField(""EffecdateDate"",this)", mblnError Or Session("dEffecdate") <> vbNullString, 1))
        Response.Write("            </TD>")
        Response.Write("            <TD>&nbsp;</TD>")
        Response.Write("            <TD COLSPAN = ""5"" >")

        If Session("CA037_nDevolution1") <> eRemoteDB.Constants.intNull And
           Request.QueryString.Item("sCodisplOrig") = "CA037" Then
            '+ El recibo es de cobro
            If Session("CA037_nDevolution1") = 2 Then
                Response.Write(mobjValues.OptionControl(2931, "optType", "Cobro", 1, "1", "changevaluesField(""Receipt_Collect"", this)", True, 3, 9377))
            Else
                Response.Write(mobjValues.OptionControl(2931, "optType", "Cobro", 2, "1", "changevaluesField(""Receipt_Collect"",this)", True, 3, 9377))
            End If
        Else
            If mclsTDetail_pre.mcolTDetail_pre.TotPremium_Alt = 0 And
            Request.QueryString.Item("sDevol") = "1" Then
                Response.Write(mobjValues.OptionControl(2931, "optType", "Cobro", 2, "1", "changevaluesField(""Receipt_Collect"",this)",, 3, 9377))
            Else
                Response.Write(mobjValues.OptionControl(2931, "optType", "Cobro", mclsTDetail_pre.nTypeReceipt, "1", "changevaluesField(""Receipt_Collect"",this)",, 3, 9377))
            End If
        End If
        Response.Write("            </TD>")
        Response.Write("        </TR>")


        Response.Write("        <TR>" & vbCrLf)
        Response.Write("            <TD><LABEL ID =""9378""> Hasta</LABEL></TD>")
        Response.Write("            <TD>")

        Dim lstrNextReceip

        If Request.QueryString.Item("dExpirDate") <> vbNullString Then
            If Request.QueryString.Item("dExpirDate") <> mclsTDetail_pre.mclsPolicy.dNextReceip.ToShortDateString Then

                If Request.QueryString.Item("sChangeDate") = "1" Then
                    '+ Cuando se hace el cambio de fecha de Renovacion y se refresca la pagina, se lee de la BD
                    '+ la nueva fecha de Renovacion y la coloca mal en la vigencia del recibo	
                    If Request.QueryString.Item("sCodisplOrig") = "CA038" Then
                        lstrNextReceip = Request.QueryString.Item("dExpirDate")
                    Else
                        lstrNextReceip = mclsTDetail_pre.mclsPolicy.dNextReceip
                    End If
                Else
                    lstrNextReceip = Request.QueryString.Item("dExpirDate")
                End If
            Else
                lstrNextReceip = mclsTDetail_pre.mclsPolicy.dNextReceip
            End If
        Else
            lstrNextReceip = mclsTDetail_pre.mclsPolicy.dNextReceip
        End If

        Response.Write(mobjValues.DateControl("tcdExpirDateR", lstrNextReceip,, "Fecha de expiración del recibo",,,,, mblnError Or Session("dEffecdate") <> vbNullString, 2))

        Response.Write("            </TD>")
        Response.Write("            <TD>&nbsp;</TD>")
        Response.Write("            <TD>")

        If Session("CA037_nDevolution1") <> eRemoteDB.Constants.intNull And
           Request.QueryString.Item("sCodisplOrig") = "CA037" Then
            '+ El reibo es de devolución
            If Session("CA037_nDevolution1") = 1 Then
                Response.Write(mobjValues.OptionControl(2933, "optType", "Devolución", 1, "2", "changevaluesField(""Receipt_Collect"",this)", True, 4, 9379))
            Else
                Response.Write(mobjValues.OptionControl(2933, "optType", "Devolución", 2, "2", "changevaluesField(""Receipt_Collect"",this)", True, 4, 9379))
            End If
        Else
            If mclsTDetail_pre.mcolTDetail_pre.TotPremium_Alt = 0 And
            Request.QueryString.Item("sDevol") = "1" Then
                Response.Write(mobjValues.OptionControl(2933, "optType", "Devolución", 1, "2", "changevaluesField(""Receipt_Collect"",this)",, 4, 9379))
            Else
                Response.Write(mobjValues.OptionControl(2933, "optType", "Devolución", mclsTDetail_pre.nTypeReceipt - 1, "2", "changevaluesField(""Receipt_Collect"",this)",, 4, 9379))
            End If
        End If

        Response.Write("            </TD>")
        Response.Write("            <TD COLSPAN = ""4"" >")

        mstrRecDevEqualColl = Request.QueryString.Item("nRecDevEqualColl")

        '+ Si el recibo es de cobro se deshabilita el check de recibo de devolucion igual a de cobro
        If Request.QueryString.Item("nTypeReceipt") = "2" Then
            mstrDisabled = False
        Else
            mstrDisabled = True
            mstrRecDevEqualColl = "2"
        End If

        If Not mblnSequence And Request.QueryString.Item("sOnSeq") <> "1" Then
            Response.Write(mobjValues.CheckControl("chkDevReceipt", "Igual al recibo de cobro asociado", mstrRecDevEqualColl, 1, "insDevReceipt(this);", mstrDisabled))
        Else
            Response.Write(mobjValues.HiddenControl("chkDevReceipt", vbNullString))
        End If

        Response.Write("            </TD>")
        Response.Write("        </TR>")

        Response.Write("        <TR>")
        Response.Write("            <TD COLSPAN = ""8"" >&nbsp;</TD>")
        Response.Write("        </TR>")

        Response.Write("        <TR>")
        Response.Write("            <TD><LABEL ID =""9380"" > Recibo</LABEL></TD>")
        Response.Write("            <TD>")

        If mclsTDetail_pre.nReceipt > 0 Then
            nDefReceipt = mclsTDetail_pre.nReceipt
        Else
            nDefReceipt = IIf(Request.QueryString.Item("nReceiptGrid") = eRemoteDB.Constants.strNull, eRemoteDB.Constants.intNull, Request.QueryString.Item("nReceiptGrid"))
        End If

        If Not mblnSequence And Request.QueryString.Item("sOnSeq") <> "1" Then
            'Response.Write mobjvalues.NumericControl("tcnReceipt",10,nDefReceipt,,"Número del recibo",,0,,,,"changevaluesField(""Receipt"", this)",mblnError,5)
            Response.Write(mobjValues.NumericControl("tcnReceipt", 10, nDefReceipt,, "Número del recibo",, 0,,,, "changevaluesField(""Receipt"", this)", True, 5))
        Else
            Response.Write(mobjValues.NumericControl("tcnReceipt", 10, nDefReceipt,, "Número del recibo",, 0,,,, "changevaluesField(""Receipt"", this)", True, 5))
        End If

        Response.Write("            </TD>")
        Response.Write("            <TD>&nbsp;</TD>")
        Response.Write("            <TD><LABEL ID =""9381""> Recibo de cobro asociado</LABEL></TD>")
        Response.Write("            <TD>")

        If Request.QueryString.Item("sVerif") <> "" Then
            mlbnDisabled = Request.QueryString.Item("sVerif")
        Else
            mlbnDisabled = True
        End If

        If mclsTDetail_pre.nTypeReceipt = 2 Then
            mlbnDisabled = False
        Else
            If mclsTDetail_pre.mcolTDetail_pre.TotPremium_Alt = 0 And
               Request.QueryString.Item("sDevol") = "1" Then
                mlbnDisabled = False
            Else
                mlbnDisabled = True
            End If
        End If

        If mclsTDetail_pre.nReceiptCollec > 0 Then
            Response.Write(mobjValues.NumericControl("tcnReceipt_Collec", 10, mclsTDetail_pre.nReceiptCollec,, "Número del recibo asociado",, 0,,,, "insDevReceipt(this);", True))
        Else
            Response.Write(mobjValues.NumericControl("tcnReceipt_Collec", 10, Request.QueryString.Item("nReceipt_Collec"),, "Número del recibo asociado",, 0,,,, "insDevReceipt(this);", True))
        End If

        Response.Write(mobjValues.HiddenControl("tcnPremium_Collec", Request.QueryString.Item("nPremium_Collect")))
        Response.Write("&nbsp;")

        If Not mobjValues.ActionQuery Then
            If Session("CA037_nDevolution1") <> eRemoteDB.Constants.intNull And
               Request.QueryString.Item("sCodisplOrig") = "CA037" Then
                '+ El reibo es de devolución
                If Session("CA037_nDevolution1") = 1 Then
                    Response.Write(mobjValues.AnimatedButtonControl("btn_receiptCobro", "/VtimeNet/images/btn_ApplyOff.gif", "Carga los recibos asociados a la póliza",, "ShowReceiptPol(2)", False))
                Else
                    Response.Write(mobjValues.AnimatedButtonControl("btn_receiptCobro", "/VtimeNet/images/btn_ApplyOff.gif", "Carga los recibos asociados a la póliza",, "ShowReceiptPol(2)", True))
                End If
            Else
                If Not mblnSequence And Request.QueryString.Item("sOnSeq") <> "1" Then
                    Response.Write(mobjValues.AnimatedButtonControl("btn_receiptCobro", "/VtimeNet/images/btn_ApplyOff.gif", "Carga los recibos asociados a la póliza",, "ShowReceiptPol(2)", mlbnDisabled))
                Else
                    Response.Write(mobjValues.AnimatedButtonControl("btn_receiptCobro", "/VtimeNet/images/btn_ApplyOff.gif", "Carga los recibos asociados a la póliza",, "ShowReceiptPol(1)", mlbnDisabled))
                End If
            End If
        End If

        Response.Write("            </TD>")
        Response.Write("            <TD>&nbsp;</TD>")
        Response.Write("            <TD><LABEL ID =""9388""> Contrato - Cup&oacute;n (Pendiente)</LABEL></TD>")
        Response.Write("            <TD>")

        If Request.QueryString.Item("sVerif") <> "" Then
            mlbnDisabled = Request.QueryString.Item("sVerif")
        Else
            mlbnDisabled = True
        End If

        If mclsTDetail_pre.nTypeReceipt = 2 Then
            mlbnDisabled = False
        Else
            If mclsTDetail_pre.mcolTDetail_pre.TotPremium_Alt = 0 And
               Request.QueryString.Item("sDevol") = "1" Then
                mlbnDisabled = False
            Else
                mlbnDisabled = True
            End If
        End If

        Response.Write(mobjValues.NumericControl("tcnContrat", 10, Request.QueryString.Item("nContrat"),, "Número del Contrato-Cupon",, 0,,,, "insDevReceipt(this);", True))
        Response.Write("&nbsp;")
        Response.Write(mobjValues.NumericControl("tcnCoupon", 3, Request.QueryString.Item("nCoupon"),, "Número del Contrato-Cupon",, 0,,,, "insDevReceipt(this);", True))
        Response.Write(mobjValues.HiddenControl("hddCouponAmount", Request.QueryString.Item("nCouponAmount")))

        If Not mobjValues.ActionQuery Then
            If Session("CA037_nDevolution1") <> eRemoteDB.Constants.intNull And
               Request.QueryString.Item("sCodisplOrig") = "CA037" Then
                '+ El recibo es de devolución
                If Session("CA037_nDevolution1") = 1 Then
                    Response.Write(mobjValues.AnimatedButtonControl("btn_receiptCobro", "/VtimeNet/images/btn_ApplyOff.gif", "Carga los cupones del contrato",, "ShowContratCoupon(2)", False))
                Else
                    Response.Write(mobjValues.AnimatedButtonControl("btn_receiptCobro", "/VtimeNet/images/btn_ApplyOff.gif", "Carga los cupones del contrato",, "ShowContratCoupon(2)", True))
                End If
            Else
                If Not mblnSequence And Request.QueryString.Item("sOnSeq") <> "1" Then
                    Response.Write(mobjValues.AnimatedButtonControl("btn_receiptCobro", "/VtimeNet/images/btn_ApplyOff.gif", "Carga los cupones del contrato",, "ShowContratCoupon(2)", mlbnDisabled))
                Else
                    Response.Write(mobjValues.AnimatedButtonControl("btn_receiptCobro", "/VtimeNet/images/btn_ApplyOff.gif", "Carga los cupones del contrato",, "ShowContratCoupon(1)", mlbnDisabled))
                End If
            End If
        End If

        Response.Write("            </TD>")
        Response.Write("        </TR>")

        Response.Write("        <TR>")
        Response.Write("            <TD><LABEL ID = ""9383"" > Moneda</LABEL></TD>")
        Response.Write("            <TD>")

        mobjValues.BlankPosition = False
        With mobjValues.Parameters
            .Add("sCertype", Session("sCertype"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Add("nBranch", Session("nBranch"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Add("nProduct", Session("nProduct"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Add("nPolicy", Session("nPolicy"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Add("nCertif", Session("nCertif"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Add("dEffecdate", ldtmEffecdateR, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
        End With
        Response.Write(mobjValues.PossiblesValues("cbeCurrency", "tabCurren_pol", eFunctions.Values.eValuesType.clngComboType, mclsTDetail_pre.nCurrency, True,,,,,, mblnError,, "Moneda en la que debe generarse el recibo",, 6))

        Response.Write("            </TD>")
        Response.Write("            <TD>&nbsp;</TD>")
        Response.Write("            <TD><LABEL ID =""9384""> Emisión</LABEL></TD>")
        Response.Write("            <TD COLSPAN = ""4"" >")

        If Request.QueryString.Item("sCodisplOrig") = "CA037" Then
            Response.Write(mobjValues.DateControl("tcdIssueDate", mclsTDetail_pre.mclsCertificat.dStartdate,, "Fecha de emisión del recibo",,,, "changevaluesField(""IssueDate"",this)", True, 7))
        Else
            Response.Write(mobjValues.DateControl("tcdIssueDate", mclsTDetail_pre.dIssuedat,, "Fecha de emisión del recibo",,,, "changevaluesField(""IssueDate"",this)", True, 7))
        End If

        Response.Write("            </TD>")
        Response.Write("        </TR>")

        Response.Write("        <TR>")
        Response.Write("            <TD><LABEL ID = 9385 > Origen</LABEL></TD>")
        Response.Write("            <TD>")

        mobjValues.TypeList = 2
        mobjValues.List = "13,14,15,16"
        Response.Write(mobjValues.PossiblesValues("cbeSource", "Table24", 1, IIf(mclsTDetail_pre.nTratypei = eRemoteDB.Constants.intNull, 1, mclsTDetail_pre.nTratypei),,,,,,, mblnError Or mblnSequence,, "Transacción que dá origen al recibo",, 8))

        Response.Write("            </TD>")
        Response.Write("            <TD>&nbsp;</TD>")
        Response.Write("            <TD><LABEL ID = ""9386"" > Comisión</LABEL></TD>")
        Response.Write("            <TD COLSPAN = ""4"">")
        Response.Write(mobjValues.NumericControl("tcnCommision", 20, mclsTDetail_pre.mcolTDetail_pre.Commission,, "Monto de comisión total del recibo", True, 6, True))
        Response.Write("            </TD>")
        Response.Write("        </TR>")

        Response.Write("        <TR>")
        Response.Write("            <TD><LABEL ID = ""9387""> Recibo lider</LABEL></TD>")
        Response.Write("            <TD>")
        Response.Write(mobjValues.TextControl("tctOrigReceipt", 12, mclsTDetail_pre.sOrigReceipt,, "Número asignado por la compañía lider",,,,, mblnError Or mclsTDetail_pre.mclsPolicy.sBussityp = "1", 9))
        Response.Write("            </TD>")
        Response.Write("            <TD>&nbsp;</TD>")
        Response.Write("            <TD><LABEL ID = ""9386"" >% Comisión</LABEL></TD>")
        Response.Write("            <TD COLSPAN = ""4"">")
        Response.Write(mobjValues.DIVControl("lblPorcCommision"))
        Response.Write("            </TD>")
        Response.Write("        </TR>")

        Response.Write("        <TR>")

        If mblnSequence Then
            Response.Write("            <TD COLSPAN = ""8"">")
            mobjValues.CheckControl("chkDelReceipt", "Eliminar recibo",, "1",, mblnError,, "Indica que se debe eliminar el recibo mostrado en la ventana")
            Response.Write("            </TD>")
        End If

        Response.Write("        </TR>")
        Response.Write("    </Table>")

        'GRID

        If mblnSequence Or Request.QueryString.Item("sOnSeq") = "1" Then

            '%+ Si es un Certificado y la póliza tiene una facturacion por "Póliza" o por "Situación de Riesgo" se deben mostrar 
            '%+ las figuras de la Matriz para generar el recibo.

            If CDbl(Session("nCertif")) > 0 And
                (mclsTDetail_pre.mclsPolicy.sColinvot = "1" Or
                mclsTDetail_pre.mclsPolicy.sColinvot = "3") Then
                Response.Write(mobjValues.HiddenControl("hddClient_policy", mclsTDetail_pre.mclsPolicy.SCLIENT))
            Else
                Response.Write(mobjValues.HiddenControl("hddClient_policy", mclsTDetail_pre.mclsCertificat.sClient))
            End If

            Response.Write(mobjValues.HiddenControl("hddOnSeq", "1"))
        Else
            Response.Write(mobjValues.HiddenControl("hddChangeDateFrom", vbNullString))
        End If

        lintCount = 0
        lintIndex = 0

        If Not mblnError Then
            For Each lclsTDetail_pre In mclsTDetail_pre.mcolTDetail_pre
                With mobjGrid
                    .Columns("Sel").Checked = "2"

                    If lclsTDetail_pre.nPremiumA <> eRemoteDB.Constants.intNull Or
                       lclsTDetail_pre.nPremiumE <> eRemoteDB.Constants.intNull Or
                       lclsTDetail_pre.sPrem_det = "3" Then
                        .Columns("Sel").Checked = "1"
                        .Columns("hddExistColum").DefValue = "1"
                    End If

                    .Columns("dtcClient").DefValue = lclsTDetail_pre.sClient
                    .Columns("tcnCodeItem").DefValue = lclsTDetail_pre.nItem
                    .Columns("cbeType").DefValue = lclsTDetail_pre.nType
                    .Columns("tcnCertif").DefValue = lclsTDetail_pre.nCertif
                    .Columns("tctModule").DefValue = lclsTDetail_pre.sModulec
                    .Columns("tctElement").DefValue = lclsTDetail_pre.sShort_des
                    .Columns("tcnCapital").DefValue = lclsTDetail_pre.nCapital
                    .Columns("hddAddTax").DefValue = lclsTDetail_pre.sAddtax
                    If lclsTDetail_pre.nPremiumA <> eRemoteDB.Constants.intNull Then
                        'If lclsTDetail_pre.nItem = 5 And Request.QueryString.Item("nTypeReceipt") = 2 Then

                        If lclsTDetail_pre.nItem = 5 And mclsTDetail_pre.nTypeReceipt = 2 Then
                            .Columns("tcnPremiumA").DefValue = -1 * lclsTDetail_pre.nPremiumA
                        Else
                            If lclsTDetail_pre.nPremiumA = 0 And lclsTDetail_pre.nPremium <> 0 And Request.QueryString.Item("nTypeReceipt") = 2 Then
                                .Columns("tcnPremiumA").DefValue = -1 * lclsTDetail_pre.nPremium
                            Else
                                .Columns("tcnPremiumA").DefValue = System.Math.Abs(lclsTDetail_pre.nPremiumA)
                            End If
                        End If

                        '.Columns("tcnPremiumA").DefValue = lclsTDetail_pre.nPremiumA
                    Else
                        .Columns("tcnPremiumA").DefValue = lclsTDetail_pre.nPremiumA
                    End If
                    If lclsTDetail_pre.nPremiumE <> eRemoteDB.Constants.intNull Then
                        .Columns("tcnPremiumE").DefValue = System.Math.Abs(lclsTDetail_pre.nPremiumE)
                    Else
                        .Columns("tcnPremiumE").DefValue = lclsTDetail_pre.nPremiumE
                    End If
                    .Columns("tcnPremium").DefValue = lclsTDetail_pre.nPremium_Origi
                    .Columns("tcnCommi_rate").DefValue = lclsTDetail_pre.nPercent
                    .Columns("tcnCommission").DefValue = lclsTDetail_pre.nAmount
                    Session("nCommi_rate_CA080") = lclsTDetail_pre.nPercent
                    Session("nnCommission_CA080") = lclsTDetail_pre.nAmount

                    .Columns("hddBill_item").DefValue = lclsTDetail_pre.nBill_item
                    .Columns("hddBranch_est").DefValue = lclsTDetail_pre.nBranch_est
                    .Columns("hddBranch_led").DefValue = lclsTDetail_pre.nBranch_led
                    .Columns("hddBranch_rei").DefValue = lclsTDetail_pre.nBranch_rei
                    .Columns("hddModulec").DefValue = lclsTDetail_pre.nModulec
                    .Columns("hddAddsuini").DefValue = lclsTDetail_pre.sAddsuini
                    .Columns("hddCacalili").DefValue = lclsTDetail_pre.sCacalili
                    .Columns("hddCommissi_i").DefValue = lclsTDetail_pre.sCommissi_i

                    If lstrType_detai <> lclsTDetail_pre.nType Then
                        lstrType_detai = lclsTDetail_pre.nType
                        lintCodeItem = lclsTDetail_pre.nItem
                        lintCount = lintCount + 1
                    Else
                        If lintCodeItem <> lclsTDetail_pre.nItem Then
                            lintCodeItem = lclsTDetail_pre.nItem
                            lintCount = lintCount + 1
                        End If
                    End If


                    .Columns("btnPrem_det").HRefScript = "showDetai(" & lintIndex & ");"

                    '+ Si se trata de una cobertura, o un capital básico no se habilitan las opciones de 
                    '+ distribución de prima

                    If lstrType_detai = "1" Or
                       lstrType_detai = "7" Then
                        .Columns("btnPrem_det").Disabled = True
                    Else
                        .Columns("btnPrem_det").Disabled = False
                    End If

                    .Columns("cbePrem_det").DefValue = lclsTDetail_pre.nPrem_det
                    .Columns("hddPrem_det_old").DefValue = lclsTDetail_pre.nPrem_det
                    .Columns("hddPrem_det_proc").DefValue = lclsTDetail_pre.sPrem_det
                    .Columns("hddId_Bill").DefValue = lclsTDetail_pre.nId_Bill

                    .Columns("Sel").Disabled = mblnError

                    If Request.QueryString.Item("nRecDevEqualColl") = "1" And Request.QueryString.Item("nReceipt_Collec") <> "" Then
                        .Columns("Sel").Disabled = True
                    End If

                    Response.Write(mobjGrid.DoRow())
                End With
                lintIndex = lintIndex + 1

            Next lclsTDetail_pre
        End If

        Response.Write(mobjGrid.closeTable())

        If mclsTDetail_pre.mcolTDetail_pre.TotPremComm <> 0 Then
            mdblPorcCommision = System.Math.Abs((mclsTDetail_pre.mcolTDetail_pre.Commission * 100) / mclsTDetail_pre.mcolTDetail_pre.TotPremComm)
        Else
            mdblPorcCommision = 0
        End If

        Response.Write(mobjValues.HiddenControl("hddPorcCommision", mdblPorcCommision))

        Response.Write("<BR>")
        Response.Write("    <Table WIDTH = ""100%"" >")
        Response.Write("        <TR>" & vbCrLf)
        Response.Write("            <TD><LABEL ID=""9389"">Prima Neta</LABEL></TD>")
        Response.Write("            <TD>")
        Response.Write(mobjValues.NumericControl("tcnPremium_All", 18, mclsTDetail_pre.mcolTDetail_pre.TotPremium_Alt,, "Monto total de la prima neta del recibo", True, 6, False,,,, True))
        Response.Write("            </TD>")
        Response.Write("            <TD><LABEL ID=""9390"">Prima total</LABEL></TD>")
        Response.Write("            <TD>")
        Response.Write(mobjValues.NumericControl("tcnPremiumFact_All", 18, mclsTDetail_pre.mcolTDetail_pre.Premio_Alt,, "Monto total de la prima a facturar del recibo", True, 6, False,,,, True))
        Response.Write("            </TD>")
        Response.Write(mobjValues.HiddenControl("hddPremiumFact_All", mclsTDetail_pre.mcolTDetail_pre.Premio_Alt))
        Response.Write("        </TR>")
        Response.Write("    </TABLE>")
        Response.Write("<BR>")

        Response.Write(mobjValues.BeginPageButton)

        If Request.QueryString.Item("Type") <> "PopUp" And Not mblnSequence Then
            If Session("dEffecdate") <> vbNullString Then
                If Request.QueryString.Item("sCodisplOrig") <> "CA033_CA080" Then

                    Response.Write("    <TABLE WIDTH =""100%"" >")
                    Response.Write("        <TR>")
                    Response.Write("            <TD CLASS=""HORLINE"" COLSPAN=""3""></TD>")
                    Response.Write("        </TR>")
                    Response.Write("        <TR>")
                    Response.Write("            <TD WIDTH =""5%"">")
                    Response.Write(mobjValues.ButtonAbout("CA080"))
                    Response.Write("            </TD>")
                    Response.Write("            <TD WIDTH = ""5%"">")
                    Response.Write(mobjValues.ButtonHelp("CA080"))
                    Response.Write("            </TD>")
                    Response.Write("            <TD ALIGN =""Right"">")
                    Response.Write(mobjValues.ButtonAcceptCancel("EnabledControl()",, True))
                    Response.Write("            </TD>")
                    Response.Write("        </TR>")
                    Response.Write("    </TABLE>")

                End If
            End If
        End If

        If Request.QueryString.Item("sCodisplOrig") = "CA033_CA080" Or
       Request.QueryString.Item("sCodisplOrig") = "CA642" Then

            Response.Write("    <TABLE WIDTH = ""100%"" >")
            Response.Write("        <TR>")
            Response.Write("            <TD CLASS=""HORLINE"" COLSPAN=""3""></TD>")
            Response.Write("        </TR>")
            Response.Write("        <TR>")
            Response.Write("            <TD WIDTH = ""5%"">")
            Response.Write(mobjValues.ButtonAbout("CA080"))
            Response.Write("            </TD>")
            Response.Write("            <TD WIDTH = ""5%"">")
            Response.Write(mobjValues.ButtonHelp("CA080"))
            Response.Write("            </TD>")
            Response.Write("            <TD ALIGN = ""Right"">")
            Response.Write(mobjValues.ButtonAcceptCancel("EnabledControl()",, True))
            Response.Write("            </TD>>")
            Response.Write("        </TR>")
            Response.Write("    </TABLE>")

        End If

        If Request.QueryString.Item("sCodisplOrig") = "CA037" Then
            Response.Write(mobjValues.HiddenControl("hddIssueDate_Old", mclsTDetail_pre.mclsCertificat.dStartdate))
        Else
            Response.Write(mobjValues.HiddenControl("hddIssueDate_Old", mclsTDetail_pre.dIssuedat))
        End If

        Response.Write(mobjValues.HiddenControl("hddEffecdateDate_Old", ldtmEffecdateR))
        Response.Write(mobjValues.HiddenControl("hddClient_Old", Session("sClient")))

    End Sub

    '% convertToLocal: Convierte monto en moneda local
    '---------------------------------------------------------------------------------------------
    Private Function convertToLocal(ByRef nAmount As Object, ByRef nCurrency As Object, ByRef dEffecdate As Object) As Object
        '---------------------------------------------------------------------------------------------
        Dim lclsGeneral As eGeneral.Exchange
        lclsGeneral = New eGeneral.Exchange

        Call lclsGeneral.Convert(eRemoteDB.Constants.intNull, nAmount, nCurrency, 1, dEffecdate, eRemoteDB.Constants.intNull)

        convertToLocal = lclsGeneral.pdblResult

        lclsGeneral = Nothing

    End Function


    '% insPreCA080Upd. Se define esta funcion para contruir el contenido de la ventana UPD del recibo manual
    '---------------------------------------------------------------------------------------------------------
    Private Sub insPreCA080Upd()
        '---------------------------------------------------------------------------------------------------------

        '+En ventana popup se crean campos ocultos con informacion de ventana inicial
        '+obtenidas desde el querystring. Estos son todos los campos importantes que 
        '+no forman parte del grid
        With Response

            .Write(mobjValues.HiddenControl("tctCertype", lstrCertype))
            .Write(mobjValues.HiddenControl("cbeBranch", lstrBranch))
            .Write(mobjValues.HiddenControl("tcnPolicy", lstrPolicy))
            .Write(mobjValues.HiddenControl("tcnCertif", lstrCertif))

            '.Write(mobjValues.HiddenControl("optType", lstrTypeReceipt))
            .Write(mobjValues.HiddenControl("hddType", lstrTypeReceipt))
            .Write(mobjValues.HiddenControl("cbeCurrency", llngCurrency))
            .Write(mobjValues.HiddenControl("chkAdjust", lstrAdjust))

            .Write(mobjValues.HiddenControl("tcdStartDateR", ldtmEffecdate))
            .Write(mobjValues.HiddenControl("tcdExpirDateR", ldtmExpirReceipt))
            .Write(mobjValues.HiddenControl("tcdIssueDate", ldtmIssuedat))
            .Write(mobjValues.HiddenControl("cbeSource", llngTratypei))
            .Write(mobjValues.HiddenControl("tcnReceipt", vbNullString))

            '.Write mobjValues.HiddenControl("tcnAdjReceipt", lstrAdjReceipt)

            .Write(mobjValues.HiddenControl("cbenreceipt", lstrAdjReceipt))
            .Write(mobjValues.HiddenControl("tcnAdjAmount", lstrAdjAmount))
            .Write(mobjValues.HiddenControl("cbePayWay", lstrTypePay))
            .Write(mobjValues.HiddenControl("hddClient_policy", lstrClient))
            .Write(mobjValues.HiddenControl("hddKey", lstrKey))

        End With

        With Request
            If Request.QueryString.Item("Action") = "Del" Then
                Response.Write(mobjValues.ConfirmDelete())
                Call mclsTDetail_pre.inspostCA028Upd(lstrCodispl, .QueryString.Item("Action"), lstrCertype, lstrBranch, lstrProduct, lstrPolicy, lstrCertif, ldtmEffecdate, llngCurrency, mobjValues.StringToType(.QueryString.Item("sType_detai"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nBill_item"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nBranch_est"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nBranch_led"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nBranch_rei"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nCapital"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nCode"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nCommi_rate"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nCommission"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nModulec"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nPremiumA"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nPremiumE"), eFunctions.Values.eTypeData.etdDouble), CDbl(.QueryString.Item("sAddsuini")), .QueryString.Item("sTypeReceipt"), mobjValues.StringToType(.QueryString.Item("nBill_item"), eFunctions.Values.eTypeData.etdDouble), CInt(.QueryString.Item("sClient")), .QueryString.Item("sAddTax"), Session("nUsercode"), Session("SessionID"), mobjValues.StringToType(.QueryString.Item("nPrem_det"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(.QueryString.Item("nPrem_det_old"), eFunctions.Values.eTypeData.etdInteger), CShort("2"))
            End If
            Response.Write(mobjGrid.DoFormUpd(.QueryString.Item("Action"), "ValPolicyTra.aspx", Request.QueryString.Item("sCodispl"), .QueryString.Item("nMainAction"), mobjValues.ActionQuery, CShort(.QueryString.Item("Index"))))
            If Request.QueryString.Item("Action") <> "Del" Then
                Response.Write("<SCRIPT>changeValuesField(""InitialPopUp"")</" & "Script>")
            End If
        End With
    End Sub

</script>
<%Response.Expires = -1441
    mobjNetFrameWork = New eNetFrameWork.Layout
    mobjNetFrameWork.sSessionID = Session.SessionID
    mobjNetFrameWork.nUsercode = Session("nUsercode")
    Call mobjNetFrameWork.BeginPage("ca080")
    mobjValues = New eFunctions.Values
    '^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.31.19
    mobjValues.sSessionID = Session.SessionID
    mobjValues.nUsercode = Session("nUsercode")
    '~End Body Block VisualTimer Utility

    mobjValues.sCodisplPage = Request.QueryString.Item("sCodispl")

    'Call insLoadParameterQS()

    'lstrQueryString = "&sCertype=" & Request.QueryString.Item("sCertype") & "&nBranch=" & Request.QueryString.Item("nBranch") & "&nProduct=" & Request.QueryString.Item("nProduct") & "&nPolicy=" & Request.QueryString.Item("nPolicy") & "&nCertif=" & Request.QueryString.Item("nCertif") & "&dNullDate=" & Request.QueryString.Item("dNullDate") & "&sNullReceipt=" & Request.QueryString.Item("sNullReceipt") & "&sTypeReceipt=" & Request.QueryString.Item("sTypeReceipt") & "&nExeMode=" & Request.QueryString.Item("nExeMode") & "&sExeReport=" & Request.QueryString.Item("sExeReport") & "&nAgency=" & Request.QueryString.Item("nAgency") & "&sCodisplOrig=" & Request.QueryString.Item("sCodisplOrig") & "&sOnSeq=" & Request.QueryString.Item("sOnSeq")
    lstrQueryString = "&sCertype=" & Request.QueryString.Item("sCertype") &
                      "&nBranch=" & Request.QueryString.Item("nBranch") &
                      "&nProduct=" & Request.QueryString.Item("nProduct") &
                      "&nPolicy=" & Request.QueryString.Item("nPolicy") &
                      "&nCertif=" & Request.QueryString.Item("nCertif") &
                      "&dNullDate=" & Request.QueryString.Item("dNullDate") &
                      "&sNullReceipt=" & Request.QueryString.Item("sNullReceipt") &
                      "&soptReceipt=" & Request.QueryString.Item("soptReceipt") &
                      "&nExeMode=" & Request.QueryString.Item("nExeMode") &
                      "&sExeReport=" & Request.QueryString.Item("sExeReport") &
                      "&nAgency=" & Request.QueryString.Item("nAgency") &
                      "&sCodisplOrig=" & Request.QueryString.Item("sCodisplOrig") &
                      "&sOnSeq=" & Request.QueryString.Item("sOnSeq")


    '+ Cuando es llamada desde la CA033 se agregan variables al QueryString	
    'If lstrCodisplOrig = "CA033_CA080" Then
    '    lstrQueryString = lstrQueryString & "&sCodispl=" & lstrCodispl & "&sPopUp=1"
    'End If
    If Request.QueryString.Item("sCodisplOrig") = "CA033_CA080" Or
Request.QueryString.Item("sCodisplOrig") = "CA038" Then
        lstrQueryString = lstrQueryString & "&sCodispl=" & Request.QueryString.Item("sCodispl") & "&sPopUp=1"
    End If

    '- Se crean las instancias de las variables modulares
    With Server
        mobjMenu = New eFunctions.Menues
        '^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.31.19
        mobjMenu.sSessionID = Session.SessionID
        mobjMenu.nUsercode = Session("nUsercode")
        '~End Body Block VisualTimer Utility
        mobjGrid = New eFunctions.Grid
        '^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.31.19
        mobjGrid.sSessionID = Session.SessionID
        mobjGrid.nUsercode = Session("nUsercode")
        '~End Body Block VisualTimer Utility

        mobjGrid.sCodisplPage = lstrCodispl
        Call mobjGrid.SetWindowParameters(lstrCodispl, Request.QueryString.Item("sWindowDescript"), Request.QueryString.Item("nWindowTy"))
        mclsTDetail_pre = New ePolicy.TDetail_pre

        mclsProduct_li = New eProduct.Product
    End With
    'Dim mclsPremium
%>	
<script>
//- Variable para el control de versiones
    document.VssVersion="$$Revision: 2 $|$$Date: 14/06/06 18:05 $|$$Author: Gazuaje $"
 
    function CancelSelec() {
        var lintIndex = '<%=Request.QueryString("Index")%>';

        if (top.opener.marrArray.length > 1)
            var inschecked = top.opener.document.forms[0].Sel[lintIndex].checked;
        else
            var inschecked = top.opener.document.forms[0].Sel.checked;

        var Exists = self.document.forms[0].hddExistColum.value;

        if (Exists == "2")
            if (top.opener.marrArray.length > 1) {
                top.opener.document.forms[0].Sel[lintIndex].checked = false;
                top.opener.marrArray[lintIndex].Sel = false;
            }
            else {
                top.opener.document.forms[0].Sel.checked = false;
                top.opener.marrArray[0].Sel = false;
            }
    }

    //% showDetai: se muestra la ventana para distribuir la prima de los rec/desc/imp
    //-------------------------------------------------------------------------------------------
    function showDetai(Index) {
        //-------------------------------------------------------------------------------------------
        var nCodeItem, nType, sDescript, nPrem_det, nAction, sPrem_det, dIssueDate, nTypeReceipt;

//+ Se asigna valor a las variables a mostrar por el QueryString, dependiendo si es Popup o no 
<%	If Request.QueryString.Item("Type") = "PopUp" Then %>
    nAction = 302;
    Index = <%=Request.QueryString.Item("Index")%>;
    with (self.document.forms[0]) {
        nCodeItem = tcnCodeItem.value;
        dIssueDate = hddIssueDate.value;
        nType = cbeType.value;
        sDescript = tctElement.value;
        nPrem_det = cbePrem_det.value;
        sPrem_det = "2";
    }
<%	Else %>
    nAction = 401;
    with (marrArray[Index]) {
        nCodeItem = tcnCodeItem;
        dIssueDate = self.document.forms[0].tcdIssueDate.value;
        nType = cbeType;
        sDescript = tctElement;
        nPrem_det = cbePrem_det;
        sPrem_det = hddPrem_det_proc;
    }
<%  End If %>

    if (nPrem_det == 2 ||
        sPrem_det == '3')
	   <% If Request.QueryString.Item("nTypeReceipt") = "2" Then%>
	   nTypeReceipt="2";
	   <%      Else%>
	   nTypeReceipt="1";
	   <%end if%>
		ShowPopUp('CA080_1.aspx?dIssueDate=' + dIssueDate + '&nPrem_det=' + nPrem_det + '&sPrem_det=' + sPrem_det + '&nIndex=' + Index + '&nMainAction=' + nAction + '&nCodeItem=' + nCodeItem + '&nType=' + nType + '&sDescript=' + sDescript + '&nTypeReceipt=' + nTypeReceipt, 'CA080_1', 650, 400, 'no', 'no', 50, 50) 
}

//% insSelected: se controla la acción sobre la columna SEL
//-------------------------------------------------------------------------------------------
function insSelected(Field){
//-------------------------------------------------------------------------------------------
    var lstrParameters;
    var nPrem_det;
    var nPrem_det_old;
	var sOnSeq; 
	
	

	sOnSeq = '<%=Request.QueryString.Item("sOnSeq")%>';
	
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
		                 '&dEffecdate=' + self.document.forms[0].tcdStartDateR.value + 
		                 '&nPrem_det=' + nPrem_det + '&nPrem_det_old=' + nPrem_det_old
		                 // INI MDP-AFU:RQ2019-470
		                 + '&nContrat=' + self.document.forms[0].tcnContrat.value 
		                 + '&nCoupon=' + self.document.forms[0].tcnCoupon.value
		                 + '&nCouponAmount=' + self.document.forms[0].hddCouponAmount.value
		                 ;
		                 // FIN MDP-AFU:RQ2019-470
		if(checked){
		
		    if (sOnSeq=="1"){
				EditRecord(value, nMainAction, 'Update', 'sSelCheck=1' + '&sOnSeq=' + sOnSeq + '&hddClient=' + self.document.forms[0].hddClient_policy.value);
		    }else{
				EditRecord(value, nMainAction, 'Update', 'sSelCheck=1' + '&sOnSeq=' + sOnSeq + '&sClient=' + self.document.forms[0].tctClient.value + '&nPremium_Collect=' + self.document.forms[0].tcnPremium_Collec.value + '&nContrat=' + self.document.forms[0].tcnContrat.value + '&nCoupon=' + self.document.forms[0].tcnCoupon.value + '&nCouponAmount=' + self.document.forms[0].hddCouponAmount.value); // MDP-AFU:RQ2019-470
		    }
	    }
		else
			EditRecord(value, nMainAction, 'Del', lstrParameters);
	}
}
//% changevaluesField: se controla el cambio de valor de los campos de la ventana
//--------------------------------------------------------------------------------------------
function changevaluesField(Option, Field){
//--------------------------------------------------------------------------------------------
	var lstrQueryString1; 	
	var sCodispl; 
	var sOnSeq; 
	
	lstrQueryString1 = '<%=lstrQueryString%>';
	sCodispl = '<%=Request.QueryString.Item("sCodispl")%>';	
        sOnSeq = '<%=Request.QueryString.Item("sOnSeq")%>';

        sCodispl = sCodispl.replace(/,.*/, '');

        lstrQueryString1 = lstrQueryString1.replace(/&sCodispl=.*/, '');

        switch (Option) {
            // INI MDP-AFU:RQ2019-470
            case "Contrat":
                var lstrURL = self.document.location.href;
                lstrURL = lstrURL.replace(/\?.*/, '');

                var strContrat = Field.value;
                var strCoupon = self.document.forms[0].tcnCoupon.value;
                var strCouponAmount = self.document.forms[0].hddCouponAmount.value;

                with (self.document.forms[0]) {
                    if (sOnSeq == "1") {
                        lstrURL = lstrURL + "?sCodispl=" + sCodispl + "&nMainAction=304&dEffecdate=" + tcdStartDateR.value + "&dExpirDate=" + tcdExpirDateR.value + "&nTypeReceipt=" + (optType[0].checked ? optType[0].value : optType[1].value) + "&nReceipt=" + hddReceipt.value + "&dIssuedat=" + tcdIssueDate.value + "&nCurrency=" + cbeCurrency.value + "&nTratypei=" + cbeSource.value + "&sOrigReceipt=" + tctOrigReceipt.value + "&hddClient=" + hddClient_policy.value + "&nReceipt_Collec=" + tcnReceipt_Collec.value + "&nRecDevEqualColl=" + (chkDevReceipt.checked ? 1 : 2) + '&nPremium_Collect=' + self.document.forms[0].tcnPremium_Collec.value + "&sNewData=1" + "&sChangeDate=1" + "&nContrat=" + strContrat + "&nCoupon=" + strCoupon + "&nCouponAmount=" + strCouponAmount + lstrQueryString1;
                    } else {
                        lstrURL = lstrURL + "?sCodispl=" + sCodispl + "&nMainAction=304&dEffecdate=" + tcdStartDateR.value + "&dExpirDate=" + tcdExpirDateR.value + "&nTypeReceipt=" + (optType[0].checked ? optType[0].value : optType[1].value) + "&nReceipt=" + hddReceipt.value + "&dIssuedat=" + tcdIssueDate.value + "&nCurrency=" + cbeCurrency.value + "&nTratypei=" + cbeSource.value + "&sOrigReceipt=" + tctOrigReceipt.value + "&sClient=" + tctClient.value + "&nReceipt_Collec=" + tcnReceipt_Collec.value + "&nRecDevEqualColl=" + (chkDevReceipt.checked ? 1 : 2) + '&nPremium_Collect=' + self.document.forms[0].tcnPremium_Collec.value + "&sNewData=1" + "&sChangeDate=1" + "&nContrat=" + strContrat + "&nCoupon=" + strCoupon + "&nCouponAmount=" + strCouponAmount + lstrQueryString1;
                    }
                }

                self.document.location.href = lstrURL
            // FIN MDP-AFU:RQ2019-470	
            case "InitialPopUp":
                //+ Si se trata de una cobertura, o un capital básico no se habilitan las opciones de 
                //+ distribución de prima
                with (self.document.forms[0]) {
                    cbePrem_det.disabled = (cbeType.value == 1 ||
                        cbeType.value == 7);

                    //tcnPremiumA.disabled=(cbePrem_det.value==2);
                    //tcnPremiumE.	disabled=tcnPremiumA.disabled;

                    //tcnCommi_rate.disabled=tcnPremiumA.disabled;
                    //tcnCommission.disabled=tcnPremiumA.disabled;
                }
                break;

            case "Prem_det":
                with (self.document.forms[0]) {
                    hddPrem_det_proc.value = '2';
                    //+ Si el tipo de desglose es "Detallar prima", se deshabilitan los campos de prima y comisiones, 
                    //+ ya que la información de estos campos se grabará al detallar el recargo/descuento/impuesto
                    tcnPremiumA.disabled = (Field.value == 2);
                    tcnPremiumE.disabled = tcnPremiumA.disabled;
                    //tcnCommi_rate.disabled=tcnPremiumA.disabled;
                    //tcnCommission.disabled=tcnPremiumA.disabled;

                    if (tcnPremiumA.disabled) {
                        //	tcnPremiumA.value='';
                        //	tcnPremiumE.value='';
                        tcnCommi_rate.value = '';
                        tcnCommission.value = '';
                    }

                }

                break;

            case "Receipt":
                //+ Se obtiene y asigna el número de recibo de forma automática
                if (Field.value == "")
                    if (self.document.forms[0].hddReceipt.value == "")
                        insDefValues('Receipt', "nReceipt=" + Field.value, '/VtimeNet/Policy/PolicyTra/');
                    else
                        Field.value = self.document.forms[0].hddReceipt.value;
                break;
            case "Premium":
                with (self.document.forms[0]) {
                    if (Field.value != '' && Field.value != 0) {
                        if (Field.name == 'tcnPremiumA') {
                            tcnPremiumE.value = '';
                            hddAddTax.value = '1';
                        }
                        else {
                            tcnPremiumA.value = '';
                            hddAddTax.value = '2';
                        }
                    }
                }
                break;
            case "IssueDate":
                with (self.document.forms[0]) {
                    if (hddIssueDate_Old.value != Field.value) {
                        self.document.location.href = "ca080.aspx?sCodispl=" + sCodispl + "&nMainAction=304&dEffecdate=" + tcdStartDateR.value + "&dExpirDate=" + tcdExpirDateR.value + "&nTypeReceipt=" + (optType[0].checked ? optType[0].value : optType[1].value) + "&nReceipt=" + hddReceipt.value + "&dIssuedat=" + tcdIssueDate.value + "&nCurrency=" + cbeCurrency.value + "&nTratypei=" + cbeSource.value + "&sOrigReceipt=" + tctOrigReceipt.value + '&nPremium_Collect=' + self.document.forms[0].tcnPremium_Collec.value + lstrQueryString1
                        hddIssueDate.value = Option
                    }
                }
                break;
            case "Client":
                with (self.document.forms[0]) {
                    if (hddClient_Old != Field.value) {
                        //		 RQ2014-000070 - INICIO - ANDEAN-RDAR
                        //		 self.document.location.href = "CA080.asp?sCodispl=" + sCodispl+ "&nMainAction=304&dEffecdate=" + tcdStartDateR.value + "&dExpirDate=" + tcdExpirDateR.value + "&nTypeReceipt=" + (optType[0].checked?optType[0].value:optType[1].value) + "&nReceipt=" + hddReceipt.value + "&dIssuedat=" + tcdIssueDate.value + "&nCurrency=" + cbeCurrency.value + "&nTratypei=" + cbeSource.value + "&sOrigReceipt=" + "&sClient=" + tctClient.value + tctOrigReceipt.value + '&nPremium_Collect=' + self.document.forms[0].tcnPremium_Collec.value  + lstrQueryString1;
                        //		 hddClient_Old.value = Option;

                        var lstrURL = self.document.location.href;
                        lstrURL = lstrURL.replace(/Common.*/, 'Policy/policyseq');
                        lstrURL = lstrURL.replace(/\?.*/, '');
                        lstrURL = lstrURL + "?sCodispl=" + sCodispl + "&nMainAction=304&dEffecdate=" + tcdStartDateR.value + "&dExpirDate=" + tcdExpirDateR.value + "&nTypeReceipt=" + (optType[0].checked ? optType[0].value : optType[1].value) + "&nReceipt=" + hddReceipt.value + "&dIssuedat=" + tcdIssueDate.value + "&nCurrency=" + cbeCurrency.value + "&nTratypei=" + cbeSource.value + "&sOrigReceipt=" + "&sClient=" + tctClient.value + tctOrigReceipt.value + '&nPremium_Collect=' + self.document.forms[0].tcnPremium_Collec.value + lstrQueryString1;
                        self.document.location.href = lstrURL;
                        hddClient_Old.value = Option;
                        //		RQ2014-000070 - FIN - ANDEAN-RDAR
                    }
                }

                break;
            case "EffecdateDate":
                
                with (self.document.forms[0]) {
                    if (hddEffecdateDate_Old.value != Field.value) {
                        var lstrURL = self.document.location.href

                        //+ Cuando se cambia la fecha Desde de Vigencia del recibo abriendo el calendario daba un error al momento de asignar el 
                        //+ nuevo HREf de la página

                        lstrURL = lstrURL.replace(/Common.*/, 'Policy/policytra')
                        lstrURL = lstrURL.replace(/\?.*/, '')
                        lstrURL = lstrURL + "?sCodispl=" + sCodispl + "&nMainAction=304&dEffecdate=" + tcdStartDateR.value + "&dExpirDate=" + tcdExpirDateR.value + "&nTypeReceipt=" + (optType[0].checked ? optType[0].value : optType[1].value) + "&nReceipt=" + hddReceipt.value + "&dIssuedat=" + tcdIssueDate.value + "&nCurrency=" + cbeCurrency.value + "&nTratypei=" + cbeSource.value + "&sOrigReceipt=" + tctOrigReceipt.value + "&sClient=" + tctClient.value + "&nReceipt_Collec=" + tcnReceipt_Collec.value + "&nRecDevEqualColl=" + (chkDevReceipt.checked ? 1 : 2) + "&sNewData=1" + '&nPremium_Collect=' + self.document.forms[0].tcnPremium_Collec.value + "&sChangeDate=1" + lstrQueryString1
                        self.document.location.href = lstrURL
                        hddEffecdateDate_Old.value = Option
                    }
                }
                break;
            case "Receipt_Collect":
                var strContrat = self.document.forms[0].tcnContrat.value;

                with (self.document.forms[0]) {
                    if (optType[1].checked) {
                        btn_receiptCobro.disabled = false;
                        //chkDevReceipt.checked = false;

                        if (sCodispl == "CA080") {
                            tctClient.disabled = true;
                            btntctClient.disabled = true;
                        }
                    }
                    else {
                        tcnReceipt_Collec.value = '';
                        btn_receiptCobro.disabled = true;

                        if (sCodispl == "CA080") {
                            tctClient.disabled = false;
                            btntctClient.disabled = false;
                        }
                    }

                    var lstrURL = self.document.location.href

                    lstrURL = lstrURL.replace(/\?.*/, '')

                    if (sOnSeq == "1")
                        lstrURL = lstrURL + "?sCodispl=" + sCodispl + "&nMainAction=304&dEffecdate=" + tcdStartDateR.value + "&dExpirDate=" + tcdExpirDateR.value + "&nTypeReceipt=" + (optType[0].checked ? optType[0].value : optType[1].value) + "&nReceipt=" + hddReceipt.value + "&dIssuedat=" + tcdIssueDate.value + "&nCurrency=" + cbeCurrency.value + "&nTratypei=" + cbeSource.value + "&sOrigReceipt=" + tctOrigReceipt.value + "&hddClient=" + hddClient_policy.value + "&nReceipt_Collec=" + tcnReceipt_Collec.value + "&nRecDevEqualColl=" + (chkDevReceipt.checked ? 1 : 2) + '&nPremium_Collect=' + self.document.forms[0].tcnPremium_Collec.value + "&nContrat=" + strContrat + "&sNewData=1" + "&sChangeDate=1" + lstrQueryString1
                    else
                        lstrURL = lstrURL + "?sCodispl=" + sCodispl + "&nMainAction=304&dEffecdate=" + tcdStartDateR.value + "&dExpirDate=" + tcdExpirDateR.value + "&nTypeReceipt=" + (optType[0].checked ? optType[0].value : optType[1].value) + "&nReceipt=" + hddReceipt.value + "&dIssuedat=" + tcdIssueDate.value + "&nCurrency=" + cbeCurrency.value + "&nTratypei=" + cbeSource.value + "&sOrigReceipt=" + tctOrigReceipt.value + "&sClient=" + tctClient.value + "&nReceipt_Collec=" + tcnReceipt_Collec.value + "&nRecDevEqualColl=" + (chkDevReceipt.checked ? 1 : 2) + '&nPremium_Collect=' + self.document.forms[0].tcnPremium_Collec.value + "&nContrat=" + strContrat + "&sNewData=1" + "&sChangeDate=1" + lstrQueryString1
                }

                self.document.location.href = lstrURL
        }
    }

    //% insDevReceipt: se controla la acción el check "Igual al recibo de cobro asociado"
    //-------------------------------------------------------------------------------------------
    function insDevReceipt(Option) {
        //-------------------------------------------------------------------------------------------
        //+ Si el campo es "Recibo de cobro asociado" se debe refrescar la pagina para cargar la nueva informacion.
        //+ Si se esta desmarcando para que traiga la informacion de la poliza/certificado.
        //+ Si es esta marcando para que traiga la informacion asociada al recibo
        with (self.document.forms[0]) {
            switch (Option.name) {
                case "chkDevReceipt":
                    if (tcnReceipt_Collec.value != ''){
                        changevaluesField('Receipt_Collect', tcnReceipt_Collec);
                    } else{
                        chkDevReceipt.checked = false;
                    }
                        
                    break;

                case "tcnReceipt_Collec":
                    //if(chkDevReceipt.checked) RQ2019-77
                    changevaluesField('Receipt_Collect', tcnReceipt_Collec);
                // INI MDP-AFU:RQ2019-470
                case "tcnContrat":
                    changevaluesField('Contrat', tcnContrat);
                    // FIN MDP-AFU:RQ2019-470
                    break;
            }
        }
    }
   
</script>
<html>
<head>




<script LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></script>
    <META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>
    <%
        Response.Write(mobjValues.StyleSheet())

        '+ Si Session("dEffecdate") está vacío significa que se está trabajando desde el menú 
        '+ principal del sistema
        If CStr(Session("dEffecdate")) <> vbNullString Then
            If Request.QueryString.Item("Type") <> "PopUp" Then
                Response.Write("<SCRIPT>var nMainAction=304</SCRIPT>")
                '+ Si la ventana se está mostrando en la secuencia de la póliza 
                If lstrOnSeq = "1" Then
                    Response.Write(mobjMenu.setZone(2, Request.QueryString.Item("sCodispl"), Request.QueryString.Item("sWindowDescript"), CShort(Request.QueryString.Item("nWindowTy"))))
                End If
            End If
        Else
            If Request.QueryString.Item("Type") <> "PopUp" Then
                With Response
                    If Request.QueryString.Item("sCodisplOrig") <> "CA033_CA080" Then
                        .Write(mobjMenu.setZone(2, Request.QueryString.Item("sCodispl"), Request.QueryString.Item("sWindowDescript"), CShort(Request.QueryString.Item("nWindowTy"))))
                    End If
                    .Write("<SCRIPT>var nMainAction=304</SCRIPT>")
                End With
            End If
        End If
        mobjMenu = Nothing
%>
</head>
<body ONUNLOAD="closeWindows();">
<form METHOD="post" ID="FORM" NAME="CA080" ACTION="ValPolicyTra.aspx?sTime=1<%=lstrQueryString%>">
<%
    Response.Write(mobjValues.ShowWindowsName(Request.QueryString.Item("sCodispl"), Request.QueryString.Item("sWindowDescript")))
    Call insDefineGrid()
    If Request.QueryString.Item("Type") <> "PopUp" Then

        mblnSequence = False
        '+ Si se invoca desde la secuencia de Cartera
        If (Session("nTransaction") = eCollection.Premium.PolTransac.clngPolicyAmendment Or
              Session("nTransaction") = eCollection.Premium.PolTransac.clngTempPolicyAmendment Or
              Session("nTransaction") = eCollection.Premium.PolTransac.clngCertifAmendment Or
              Session("nTransaction") = eCollection.Premium.PolTransac.clngTempCertifAmendment Or
             Session("nTransaction") = eCollection.Premium.PolTransac.clngPolicyQuotAmendent Or
            Session("nTransaction") = eCollection.Premium.PolTransac.clngCertifQuotAmendent Or
            Session("nTransaction") = eCollection.Premium.PolTransac.clngPolicyPropAmendent Or
            Session("nTransaction") = eCollection.Premium.PolTransac.clngCertifPropAmendent) And
           Request.QueryString.Item("sOnSeq") = "1" Then
            mblnSequence = True
        End If

        If mblnSequence = True Or Request.QueryString.Item("sCodisplOrig") = "CA037" Then
            ldtmEffecdate = Session("dEffecdate")
            ldtmEffecdate_aux = Session("dEffecdate")
        Else
            ldtmEffecdate = Request.QueryString.Item("dEffecdate")
            'ldtmEffecdate_aux = Request.QueryString.Item("dEffecdate")
        End If

        If Session("sClient") = "" Or Request.QueryString.Item("sClient") = "" Then
            mclsTDetail_pre.mclsCertificat = New ePolicy.Certificat
            Call mclsTDetail_pre.mclsCertificat.Find(Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), True)
            If mclsTDetail_pre.mclsCertificat.sClient = "" Then
                If ldtmEffecdate_aux = "" Then
                    ldtmEffecdate_aux = mclsTDetail_pre.mclsCertificat.dStartdate
                End If

                mclsRoles = Server.CreateObject("ePolicy.Roles")

                Call mclsRoles.Find(Session("sCertype"),
                     Session("nBranch"),
                     Session("nProduct"),
                     Session("nPolicy"),
                     Session("nCertif"),
                     2,
                     "",
                     ldtmEffecdate_aux,
                     True)
                Session("sClient") = mclsRoles.SCLIENT
            Else
                Session("sClient") = mclsTDetail_pre.mclsCertificat.sClient
            End If
        Else
            Session("sClient") = Request.QueryString.Item("sClient")
        End If

        'INICIO DMendoza 14/07/2021
        lstrNewData = Request.QueryString.Item("sNewData")

        If lstrNewData = Nothing Then
            lstrNewData = "1"
        End If
        If lstrNewData = "1" Then
            Session("sKey") = Nothing
            mclsTDetail_pre.sKey = Nothing
        End If
        If lstrNewData = "2" Then
            If Session("sKey") <> "" Then
                mclsTDetail_pre.sKey = Session("sKey")
            End If
        End If

        'FIN DMendoza 14/07/2021

        Call mclsTDetail_pre.inspreCA080(Session("sCertype"),
                    Session("nBranch"),
                    Session("nProduct"),
                    Session("nPolicy"),
                    Session("nCertif"),
                    mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate),
                    mobjValues.StringToType(ldtmEffecdate, eFunctions.Values.eTypeData.etdDate),
                    mobjValues.StringToType(Request.QueryString.Item("dExpirdate"), eFunctions.Values.eTypeData.etdDate),
                    mobjValues.StringToType(Request.QueryString.Item("nTypeReceipt"), eFunctions.Values.eTypeData.etdInteger),
                    mobjValues.StringToType(Request.QueryString.Item("nReceipt"), eFunctions.Values.eTypeData.etdDouble),
                    mobjValues.StringToType(Request.QueryString.Item("nCurrency"), eFunctions.Values.eTypeData.etdInteger),
                    mobjValues.StringToType(Request.QueryString.Item("dIssuedat"), eFunctions.Values.eTypeData.etdDate),
                    mobjValues.StringToType(Request.QueryString.Item("nTratypei"), eFunctions.Values.eTypeData.etdInteger),
                    Request.QueryString.Item("sOrigReceipt"),
                    Session("SessionID"),
                    mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdInteger),
                    lstrNewData,
                    mobjValues.StringToType(Request.QueryString.Item("nReceipt_Collec"), eFunctions.Values.eTypeData.etdDouble),
                    mblnSequence,
                    mobjValues.StringToType(Request.QueryString.Item("nRecDevEqualColl"), eFunctions.Values.eTypeData.etdDouble),
                    Session("sClient"))

        mblnError = mclsTDetail_pre.bError

        If Not mblnError Then
            If Session("sCodisplOri") = "CA038" Then
                mclsPolicy = Server.CreateObject("ePolicy.Policy")

                Call mclsPolicy.Update_dNextReceip("2",
                                           mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdLong),
                                           mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdLong),
                                           mobjValues.StringToType(Session("nPolicy"), eFunctions.Values.eTypeData.etdDouble),
                                           mobjValues.StringToType(Session("nCertif"), eFunctions.Values.eTypeData.etdDouble),
                                           mobjValues.StringToType(Session("dNextReceip"), eFunctions.Values.eTypeData.etdDate),
                                           mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdLong))

                mclsPolicy = Nothing
            End If
        End If


        Response.Write(mobjValues.HiddenControl("hddProvince", mclsTDetail_pre.mclsPolicy.nProvince))

        Response.Write(mobjValues.HiddenControl("tcnProceedingNum", 0))
        'INICIO DMendoza 14/07/2021
        lstrKey = mclsTDetail_pre.sKey
        Response.Write(mobjValues.HiddenControl("hddsKey", lstrKey))

        If Session("sKey") = "" Then
            Session("sKey") = lstrKey
        End If
        'FIN DMendoza 14/07/2021

        If Request.QueryString.Item("sCodispl") = "CA080" Then
            Response.Write("<SCRIPT>self.document.forms[0].tcnProceedingNum.value=top.frames['fraHeader'].document.forms[0].tcnProceedingNum.value</SCRIPT>")
        End If

        If mclsTDetail_pre.mclsPolicy.sPolitype <> vbNullString Then
            Session("sPoliType") = mclsTDetail_pre.mclsPolicy.sPolitype
        End If

        Response.Write("<SCRIPT>" &
                       "self.document.forms[0].hddExists.value='" & mclsTDetail_pre.sExist & "';" &
                       "self.document.forms[0].hddReceipt.value='" & mobjValues.TypeToString(mclsTDetail_pre.nReceipt, eFunctions.Values.eTypeData.etdDouble) & "'" &
                       "</SCRIPT>")

        Call insPreCA080()
    Else
        Call insPreCA080Upd()
    End If

    If Request.QueryString.Item("Type") <> "PopUp" And CStr(Session("dEffecdate")) <> vbNullString Then
        Response.Write("<SCRIPT>self.document.forms[0].action='ValPolicyTra.aspx?sCodispl=" & Request.QueryString.Item("sCodispl") & "&sPopUp=1'</SCRIPT>")
    End If

    If mblnError Then
        Response.Write("<SCRIPT>alert(""Err. 60583: " & eFunctions.Values.GetMessage(60583) & """);</SCRIPT>")
    End If

    mclsTDetail_pre = Nothing
    mobjGrid = Nothing
    mobjValues = Nothing
    mclsPolicy_his = Nothing
    mclsProduct_li = Nothing
    'Set mclsPremium = Nothing
%>
</form>
</body>
</html>
<%'^Begin Footer Block VisualTimer Utility 1.1 7/4/03 10.31.19
        Call mobjNetFrameWork.FinishPage("ca080")
        mobjNetFrameWork = Nothing
        '^End Footer Block VisualTimer%>

<SCRIPT>
    //% ShowReceiptPol: se controla el cambio de valor de los campos de la ventana
    //--------------------------------------------------------------------------------------------
    function ShowReceiptPol(nSequence) {
        //--------------------------------------------------------------------------------------------
        ShowPopUp('/VtimeNet/Common/SCO6000.aspx?sCodispl=SCO6000&nBranch=' + <%=Session("nBranch")%> + '&nPolicy=' + <%=Session("nPolicy")%> +'&nProduct=' + <%=Session("nProduct")%> +'&nCertif='+ <%=Session("nCertif")%> +'&sPoliType='+ <%=Session("sPoliType")%>+'&nPremium=0' + '&ReceiptManu=1' + '&nSequence=' + nSequence + '&dEffecdate=' + self.document.forms[0].tcdStartDateR.value + '&sDevReceipt=' + self.document.forms[0].chkDevReceipt.value, 'Consulta', 600, 400, 'yes', 'no', 250, 150);
    }
    
</SCRIPT>



