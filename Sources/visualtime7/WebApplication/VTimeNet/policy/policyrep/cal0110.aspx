<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eSchedule" %>
<script language="VB" runat="Server">
    '- Objeto para el manejo de las funciones generales de carga de valores
    Dim mobjValues As eFunctions.Values
    '- Objeto para el manejo del grid de la página
    Dim mobjGrid As eFunctions.Grid
    '- Objeto para el manejo del menú
    Dim mobjMenu As eFunctions.Menues
    '- Objeto para el manejo particular de los datos de la página
    Dim mcolClass As Object
    'Variables para manejo de datos de los reportes
    Dim linTypeOption As Object
    Dim linTypeReport As Object
    Dim lintBranch As Object
    Dim lintProduct As Object
    Dim lidIssuedatIni As Object
    Dim lidIssuedatEnd As Object
    Dim lintnPolicy As Object
    Dim linnCertif As Object
    Dim lstrsPolitype As Object
    
    '% insDefineHeader: se definen las propiedades del grid
    '--------------------------------------------------------------------------------------------
    Private Sub insDefineHeader()
    '--------------------------------------------------------------------------------------------
        mobjGrid = New eFunctions.Grid
	   
        '+ Tipo de Opcion Puntual
        If linTypeOption = 1 Then
            '+ Se definen las columnas del grid    
            With mobjGrid.Columns
                Call .AddTextColumn(0, GetLocalResourceObject("tcttctDesProdColumnCaption"), "tctDesProd", 30, "", , GetLocalResourceObject("tctDesProdColumnToolTip"), , , , True)
                Call .AddNumericColumn(0, GetLocalResourceObject("tcnnPolicyColumnCaption"), "tcnnPolicy", 10, CStr(0), , GetLocalResourceObject("tcnnPolicytColumnToolTip"), , , , , , True)
                Call .AddNumericColumn(0, GetLocalResourceObject("tcnnProponumColumnCaption"), "tcnnProponum", 10, CStr(0), , GetLocalResourceObject("tcnnProponumColumnToolTip"), , , , , , True)
                Call .AddNumericColumn(0, GetLocalResourceObject("tcnnnMovementColumnCaption"), "tcnnMovement", 10, CStr(0), , GetLocalResourceObject("tcnnMovementColumnToolTip"), , , , , , True)
                Call .AddTextColumn(0, GetLocalResourceObject("sDesType_HistColumnCaption"), "tctDesType_Hist", 30, "", , GetLocalResourceObject("tctDesType_HisColumnToolTip"), , , , True)
                Call .AddTextColumn(0, GetLocalResourceObject("sDescType_amendColumnCaption"), "tctType_amend", 30, "", , GetLocalResourceObject("tctType_amendColumnToolTip"), , , , True)
                Call .AddTextColumn(0, GetLocalResourceObject("tctdEffecdateColumnCaption"), "tctEffecdate", 20, "", , GetLocalResourceObject("tctdEffecdateColumnToolTip"), , , , True)
                Call .AddTextColumn(0, GetLocalResourceObject("sFileName_Caption"), "sFileName", 30, "", , GetLocalResourceObject("sFileName_ToolTip"), , , , True)
                Call .AddAnimatedColumn(0, GetLocalResourceObject("btnStatusColumnCaption"), "btnStatus", "", GetLocalResourceObject("btnStatusColumnToolTip"), , , True)
            End With
        Else
            If linTypeReport = 3 Then
                '+ Tipo de Opcion Masivo
                With mobjGrid.Columns
                    Call .AddTextColumn(0, "Ramo", "tctDesBranch", 30, "", , "Ramo", , , , True)
                    Call .AddTextColumn(0, GetLocalResourceObject("tcttctDesProdColumnCaption"), "tctDesProd", 30, "", , GetLocalResourceObject("tctDesProdColumnToolTip"), , , , True)
                    Call .AddNumericColumn(0, "Número de Pólizas", "tcnnPolicy", 10, CStr(0), , "Número de Póliza", , 0, , , , True)
                    Call .AddNumericColumn(0, "Número de Certificados para generar Reporte", "tcnnCount", 10, CStr(0), , "Cantidad de Certificados", , 0, , , , True)
                End With
            Else
                '+ Tipo de Opcion Masivo
                With mobjGrid.Columns
                    Call .AddTextColumn(0, "Ramo", "tctDesBranch", 30, "", , "Ramo", , , , True)
                    Call .AddTextColumn(0, GetLocalResourceObject("tcttctDesProdColumnCaption"), "tctDesProd", 30, "", , GetLocalResourceObject("tctDesProdColumnToolTip"), , , , True)
                    Call .AddNumericColumn(0, "Número de Pólizas para generar Reporte", "tcnnCount", 10, CStr(0), , , , 0, , , , True)
                End With
            End If
            
        End If
        
        '+ Se definen las propiedades generales del grid
        With mobjGrid
            .Codispl = "CAL0110"
            .sCodisplPage = "CAL0110"
            .AddButton = False
            .DeleteButton = False
            .ActionQuery = mobjValues.ActionQuery
            .Height = 380
            .Width = 340
            .nMainAction = CShort(IIf(Request.QueryString.Item("nMainAction") = "", 0, Request.QueryString.Item("nMainAction")))
            .Columns("Sel").GridVisible = False
            If Request.QueryString.Item("Reload") = "1" Then
                .sReloadIndex = Request.QueryString.Item("ReloadIndex")
            End If
        End With
       
    End Sub

    Private Sub OpenAndShowFile(ByVal sFullPath As String)
        Response.Clear()
        Response.Buffer = True
        Response.AddHeader("Pragma", "no-cache")
        Response.AddHeader("Expires", "Mon, 1 Jan 2000 05:00:00 GMT")
        Response.AddHeader("Last-Modified", Now & " GMT")
        Response.ContentType = "application/pdf"
        Response.AddHeader("Content-Disposition", "inline; filename=jjj.pdf") ' & sFileName
        Response.WriteFile(sFullPath) 'oHelper.sExportedFilePath)
	
        Response.End()
    End Sub
    '% insPreCodispl: se realiza el manejo del grid
    '--------------------------------------------------------------------------------------------
    Private Sub insPreCAL0110()
        '--------------------------------------------------------------------------------------------    
        Dim lclsClass As ePolicy.Policy_his
        Dim mcolClass As ePolicy.Policy_hiss
        Dim sPatharchivoPdf As String
        Dim sfilereport As String
        mcolClass = New ePolicy.Policy_hiss
        lclsClass = New ePolicy.Policy_his
        Dim nCount As Integer
               
        'Diseño de check para re-impresión de cuadros de pólizas para "SOLICITUD PUNTUAL"
        If linTypeOption = 1 Then
            Response.Write("" & vbCrLf)
            Response.Write(" <TABLE WIDTH=""30%"">" & vbCrLf)
            Response.Write("	<TR>" & vbCrLf)
            Response.Write("		<TD>")
            mobjValues.BlankPosition = False
        
            If linTypeReport = 1 Or linTypeReport = 3 Then
                Response.Write(mobjValues.CheckControl("chkImpression", GetLocalResourceObject("chkImpressionCaption"), Request.QueryString("sImpression"), "1", "insReload(" & linTypeOption & "," & linTypeReport & "," & lintBranch & "," & lintProduct & "," & lintnPolicy & "," & linnCertif & "," & lstrsPolitype & ")", False, , GetLocalResourceObject("chkImpressionToolTip")))
            Else
                Response.Write(mobjValues.HiddenControl("chkImpression", 2))
            End If
            Response.Write("</td>" & vbCrLf)
            Response.Write("	</TR>" & vbCrLf)
            Response.Write("</TABLE>")
        End If
        
            sPatharchivoPdf = mobjValues.insGetSetting("ExportDirectoryPolicy", "/Reports/", "Paths")
        
            sPatharchivoPdf = Replace(sPatharchivoPdf, "\", "/")
           
        If mcolClass.FindCal0110(linTypeOption, linTypeReport, lintBranch, lintProduct, lidIssuedatIni, lidIssuedatEnd, lintnPolicy, linnCertif, True) Then
            If linTypeOption = 1 Then
                For Each lclsClass In mcolClass
                    nCount = mcolClass.Count
                    With mobjGrid
                        .Columns("tctDesProd").DefValue = lclsClass.nProduct & " - " & lclsClass.sDesProduct
                        .Columns("tcnnPolicy").DefValue = lclsClass.nPolicy
                        .Columns("tcnnProponum").DefValue = lclsClass.nProponum
                        .Columns("tcnnMovement").DefValue = lclsClass.nMovement
                        .Columns("tctEffecdate").DefValue = lclsClass.dEffecdate
                        .Columns("tctDesType_Hist").DefValue = lclsClass.nType_Hist & " - " & lclsClass.sDescType_Hist
                        If mobjValues.TypeToString(lclsClass.nType_amend, Values.eTypeData.etdLong) = "" Then
                            .Columns("tctType_amend").DefValue = ""
                        Else
                            .Columns("tctType_amend").DefValue = mobjValues.TypeToString(lclsClass.nType_amend, Values.eTypeData.etdLong) & " - " & lclsClass.sDescType_amend
                        End If
                        .Columns("sFileName").DefValue = lclsClass.sFile_report.Replace("*", "<BR>")
                        '+Se debe habilitar el mostrar los resultados
                        .Columns("btnStatus").HRefScript = ""
                        .Columns("btnStatus").sAlias = "Mostrar resultados"
                        .Columns("btnStatus").Src = "/VTimeNet/images/btcStat01.gif"
                        .Columns("btnStatus").Disabled = False
                        If lclsClass.sFile_report = "" Then
                            sfilereport = ""
                        Else
                            sfilereport = lclsClass.sFile_report
                        End If
                        .Columns("btnStatus").HRefScript = "insShowResult('" & linTypeOption & "','" & linTypeReport & "', '" & lintBranch & " ', '" & lclsClass.nProduct & "','" & lclsClass.nPolicy & "', '" & lclsClass.nProponum & "','" & lclsClass.nMovement & "','" & lclsClass.dEffecdate & "','" & linnCertif & "','" & lclsClass.nType_Hist & "','" & sfilereport & "','" & sPatharchivoPdf & "','" & lclsClass.sPolitype & "');"
                        lstrsPolitype = lclsClass.sPolitype
                        Response.Write(.DoRow)
                    End With
                Next lclsClass
            Else
                'Tipo de Reporte: Certificados de Cobertura
                If linTypeReport = 3 Then
                    For Each lclsClass In mcolClass
                        nCount = mcolClass.Count
                        With mobjGrid
                            .Columns("tctDesBranch").DefValue = lclsClass.nBranch & " - " & lclsClass.sDesBranch
                            .Columns("tctDesProd").DefValue = lclsClass.nProduct & " - " & lclsClass.sDesProduct
                            .Columns("tcnnPolicy").DefValue = lclsClass.nPolicy
                            .Columns("tcnnCount").DefValue = lclsClass.nCountRegist
                            Response.Write(.DoRow)
                        End With
                    Next lclsClass
                Else
                    
                    For Each lclsClass In mcolClass
                        nCount = mcolClass.Count
                        With mobjGrid
                            .Columns("tctDesBranch").DefValue = lclsClass.nBranch & " - " & lclsClass.sDesBranch
                            .Columns("tctDesProd").DefValue = lclsClass.nProduct & " - " & lclsClass.sDesProduct
                            .Columns("tcnnCount").DefValue = lclsClass.nCountRegist
                            Response.Write(.DoRow)
                        End With
                    Next lclsClass
                End If
            End If
        End If
            Response.Write(mobjGrid.closeTable())
            mcolClass = Nothing
        
            'Habilitar Re-impresión cuando se encuentra Registro
            If linTypeOption = 1 Then
                If (linTypeReport = 1 Or linTypeReport = 3) And nCount > 0 Then
                
                Else
                    Response.Write("<SCRIPT>enabledCheck();</" & "SCRIPT>")
                End If
            End If
    End Sub
</script>
<%  Response.Expires = 0
    Response.Buffer = False
    Server.ScriptTimeout = 3000
    mobjValues = New eFunctions.Values
    mobjValues.ActionQuery = CDbl(IIf(Request.QueryString.Item("nMainAction") = "", 0, Request.QueryString.Item("nMainAction"))) = 401
    '+Seteo de parámetros traídos de página encabezado
    linTypeOption = mobjValues.StringToType(Request.QueryString.Item("ntype"), eFunctions.Values.eTypeData.etdLong)
    linTypeReport = mobjValues.StringToType(Request.QueryString.Item("nTypeReport"), eFunctions.Values.eTypeData.etdLong)
    lintBranch = mobjValues.StringToType(Request.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdLong)
    lintProduct = mobjValues.StringToType(Request.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdLong)
    lidIssuedatIni = mobjValues.StringToType(Request.QueryString.Item("dIssuedatIni"), eFunctions.Values.eTypeData.etdDate)
    lidIssuedatEnd = mobjValues.StringToType(Request.QueryString.Item("dIssuedatEnd"), eFunctions.Values.eTypeData.etdDate)
    lintnPolicy = mobjValues.StringToType(Request.QueryString.Item("nPolicy"), eFunctions.Values.eTypeData.etdLong)
    linnCertif = mobjValues.StringToType(Request.QueryString.Item("nCertif"), eFunctions.Values.eTypeData.etdLong)
    lstrsPolitype = Request.QueryString.Item("sPolitype")
%>
<html>
<head>
	<META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>
<%  Response.Write(mobjValues.StyleSheet())%>	
    <script LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></script>
    <script LANGUAGE="JavaScript">
        //- Variable para el control de versiones
        document.VssVersion = "$$Revision: 2 $|$$Date: 9-09-09 19:38 $|$$Author: Mpalleres $"

        //% insShowResult: Invoca a página que muestra resultados de proceso
        //--------------------------------------------------------------------------------------------
        function insShowResult(nTypeOption, nTypeReport, nBranch, nProduct, nPolicy, nProponum, nMovement, dEffecdate, nCertif, nType_hist,sReport,sPath, sPolitype) {
            //--------------------------------------------------------------------------------------------
            with (self.document.forms[0]) {
                var lstr_docloc = "";
                var check;
                lstr_docloc = document.location.href;
                insDefValues('sReport', 'nTypeReport=' + nTypeReport +
                                    '&nBranch=' + nBranch +
                                    '&nProduct=' + nProduct +
                                    '&nPolicy=' + nPolicy +
                                    '&nProponum=' + nProponum +
                                    '&nMovement=' + nMovement +
                                    '&dEffecdate=' + dEffecdate +
                                    '&sImpression=' + document.getElementsByName('chkImpression')[0].checked +
                                    '&nTypeOption=' + nTypeOption +
                                    '&nCertif=' + nCertif +
                                    '&sReport=' + sReport +
                                    '&sPath=' + sPath +
                                    '&nType_hist=' + nType_hist +
                                    '&sPolitype=' + sPolitype,
                             '/VTimeNet/policy/policyrep', 'resvalpolicyrep');
                setTimeout(function () { insReload2(lstr_docloc) }, 2000);
            }
        }
        
        function insReload2(sHref) {
            document.location.href = sHref;
        }

        //%insReload: Se encarga de recargar la página al cambiar el valor del combo de la página.
        //-------------------------------------------------------------------------------------------
        function insReload(nTypeOption, nTypeReport, nBranch, nProduct, nPolicy, nCertif, sPolitype) {
            var lstr_docloc = "";
	        with (document.forms[0]) {
                lstr_docloc = document.location.href;
                /*Control de chekeo de re-impresión*/
                if (document.getElementsByName('chkImpression')[0].checked) {
                    insShowResult(nTypeOption, nTypeReport, nBranch, nProduct, nPolicy, 0, 0, GetDateSystem(), nCertif, 19, "", "", sPolitype);
                                        setTimeout(function(){insReload2(lstr_docloc)}, 3000);
                }
                else {
                    document.location.href = lstr_docloc;
                }
	        }
        }
        function insFinish() {
            //------------------------------------------------------------------------------------------
            return (true);
        }

        /* Habilitar Reimpresion cuando encuentra registro */
        function enabledCheck() {
            document.getElementsByName('chkImpression')[0].disabled = true;
        }

</script>
<%
    If Request.QueryString.Item("Type") <> "PopUp" Then
        mobjMenu = New eFunctions.Menues
        Response.Write(mobjMenu.setZone(2, "CAL0110", Request.QueryString.Item("sWindowDescript"), CShort(Request.QueryString.Item("nWindowTy"))))
        Response.Write("<SCRIPT>var nMainAction=top.frames['fraSequence'].plngMainAction</SCRIPT>")
        mobjMenu = Nothing
    End If
%>
</head>
<body ONUNLOAD="closeWindows();">
<%  Response.Write(mobjValues.ShowWindowsName("CAL0110", Request.QueryString.Item("sWindowDescript")))%>
    <FORM METHOD="POST" ID="FORM" NAME="CAL0110" ACTION="valPolicyrep.aspx?sCodispl=CAl0110&sZone=2&nMainAction=<%=Request.QueryString.Item("nMainAction")%>&ntype=<%=Request.QueryString.Item("ntype")%>&nTypeReport=<%=Request.QueryString.Item("nTypeReport")%>&nBranch=<%=Request.QueryString.Item("nBranch")%>&nProduct=<%=Request.QueryString.Item("nProduct")%>&dIssuedatIni=<%=Request.QueryString.Item("dIssuedatIni")%>&dIssuedatEnd=<%=Request.QueryString.Item("dIssuedatEnd")%>&nPolicy=<%=Request.QueryString.Item("nPolicy")%>&nCertif=<%=Request.QueryString.Item("nCertif")%>">
<%  
    Call insDefineHeader()
    Call insPreCAL0110()
%>
    </form> 
</body>
</html>