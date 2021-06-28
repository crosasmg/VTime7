<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eReports" %>
<%@ Import namespace="eSchedule" %>
<%@ Import namespace="eCollection" %>
<%@ Import namespace="eRemoteDB" %>
<%@ Import namespace="ePolicy" %>
<%@ Import namespace="eInterface" %>


<script language="VB" runat="Server">
    Dim mclsPolicy As Object
    '%--------------------------------------------------------------
    '% Nombre       : RESBATCH
    '% Descripcion  : Procesa los resultados y los errores de los procesos batch
    '%                Es llamada desde el grid de BTC001.aspx
    '% Parametros   : Field  : No usado
    '%                nBatch : Identificador del proceso batch
    '%                nGroup : Grupo de parmetros del proceso
    '%                sKey   : Clave de ejecución del proceso 
    '%                sDescBatch : Descripcion del proceso batch
    '% document.VssVersion="$$Revision: 6 $|$$Date: 25-09-09 18:47 $|$$Author: Mpalleres $"
    '%--------------------------------------------------------------

    '^Begin Header Block VisualTimer Utility 1.1 7/4/03 11.11.56
    Dim mobjNetFrameWork As eNetFrameWork.Layout
    '~End Header Block VisualTimer Utility
    Dim mobjValues As eFunctions.Values
    Dim lclsQuery As eRemoteDB.Query
    Dim lclReport As eReports.Report
    Dim lclsReport_prod As eProduct.report_prod
    'Variables para manejos de los datos de los reportes
    Dim linTypeOption As Object
    Dim linTypeReport As Object
    Dim lintBranch As Object
    Dim lintProduct As Object
    Dim lintnPolicy As Object
    Dim linnProponum As Object
    Dim linnMovement As Object
    Dim lstdEffecdate As Object
    Dim linsImpression As Object
    Dim linnCertif As Object
    Dim linnType_Hist As Object
    Dim lstrFile As String
    Dim lArrayFiles() As String
    Dim lintCount As Integer
    Dim lintMax As Integer
    Dim sCertype As String
    Dim mstrField As String
    Dim mobjDocuments As New eReports.Report
    
    Dim lstrsPolitype As String
</script>
<%  Response.Expires = -1441
    mobjNetFrameWork = New eNetFrameWork.Layout
    mobjNetFrameWork.sSessionID = Session.SessionID
    mobjNetFrameWork.nUsercode = Session("nUsercode")
    Call mobjNetFrameWork.BeginPage("showdefvalues")
    mobjValues = New eFunctions.Values
    '^Begin Body Block VisualTimer Utility 1.1 7/4/03 11.11.56
    mobjValues.sSessionID = Session.SessionID
    mobjValues.nUsercode = Session("nUsercode")
    '~End Body Block VisualTimer Utility
    mobjValues.sCodisplPage = "showdefvalues"
    Response.Write(mobjValues.StyleSheet)
    mstrField = Request.QueryString.Item("Field")
    
    'Seteo de variables pasadas por parámetros
    linTypeReport = mobjValues.StringToType(Request.QueryString.Item("nTypeReport"), eFunctions.Values.eTypeData.etdLong)
    lintBranch = mobjValues.StringToType(Request.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdLong)
    lintProduct = mobjValues.StringToType(Request.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdLong)
    lintnPolicy = mobjValues.StringToType(Request.QueryString.Item("nPolicy"), eFunctions.Values.eTypeData.etdLong)
    linnProponum = mobjValues.StringToType(Request.QueryString.Item("nProponum"), eFunctions.Values.eTypeData.etdLong)
    linnMovement = mobjValues.StringToType(Request.QueryString.Item("nMovement"), eFunctions.Values.eTypeData.etdLong)
    lstdEffecdate = Request.QueryString.Item("dEffecdate")
    linsImpression = mobjValues.StringToType(Request.QueryString.Item("sImpression"), eFunctions.Values.eTypeData.etdBoolean)
    linTypeOption = mobjValues.StringToType(Request.QueryString("nTypeOption"), eFunctions.Values.eTypeData.etdLong)
    linnCertif = mobjValues.StringToType(Request.QueryString("nCertif"), eFunctions.Values.eTypeData.etdLong)
    linnType_Hist = mobjValues.StringToType(Request.QueryString("nType_hist"), eFunctions.Values.eTypeData.etdLong)
    sCertype = Request.QueryString("scertype")
    If String.IsNullOrEmpty(sCertype) Then
        sCertype = "2"
    End If
    lstrsPolitype = Request.QueryString("sPolitype")
 %>
<HTML>
    <HEAD>
        <SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
        <SCRIPT>
            //+ Variable para el control de versiones 
            document.VssVersion = "$$Revision: 6 $|$$Date: 25-09-09 18:47 $|$$Author: Mpalleres $"
        </SCRIPT>
        <SCRIPT>
            function openWindowChild(URL, left, width, height) {
                child = window.open();
                child.location.href = URL;
            }
            function AbrirArchivo(sfilename_aux) {
                openWindowChild(sfilename_aux, '0', '800', '600');
            }
        </SCRIPT>
    </HEAD>
    <BODY>
	    <FORM NAME="ShowDefValues"></FORM>
    </BODY>
</HTML>
<%

    If Request.QueryString.Item("sReport") <> vbNullString Then
        Dim sPath = Request.QueryString("sPath")
        lArrayFiles = Request.QueryString.Item("sReport").split({"*"C}, StringSplitOptions.RemoveEmptyEntries) 
        lintMax = UBound(lArrayFiles)
        For Me.lintCount = 0 To lintMax
            lstrFile = sPath & lArrayFiles(lintCount)
            'lstrFile = Replace(lstrFile, "/", "\")
            'Response.Write("<SCRIPT>alert(""por aqui"");</SCRIPT>")
            Response.Write("<SCRIPT>ShowPopUp(""/VTimeNet/Common/PDFPlaceHolder.aspx?file=" & lstrFile & """, ""PDFFile" & Me.lintCount & """,780,530,'yes','yes',20,20,'no','no');</" & "SCRIPT>")
        Next
        'Response.Clear()
        'Response.Buffer = True
        'Response.AddHeader("Pragma", "no-cache")
        'Response.AddHeader("Expires", "Mon, 1 Jan 2000 05:00:00 GMT")
        'Response.AddHeader("Last-Modified", Now & " GMT")
        'Response.ContentType = "application/pdf"
        'Response.AddHeader("Content-Disposition", "inline; filename=jjj.pdf") ' & sFileName
        'lstrFile = Request.QueryString.Item("sReport")
        'lstrFile = Replace(lstrFile, "/", "\")
        'Response.WriteFile(lstrFile) 'oHelper.sExportedFilePath)
        'Response.End()
    Else

        'Validación del tipo de transacción a realizar con los reportes
        If linTypeOption = 1 Then '*Transacciones puntuales
            'Validación del tipo de reporte a visualizar
            If linTypeReport = 1 Then '*Cuadro de pólizas
                lclReport = New eReports.Report
                lclsReport_prod = New eProduct.report_prod
                Dim lclsPolicyHist As ePolicy.Policy_his
                lclsPolicyHist = New ePolicy.Policy_his
        
                'Validación de re-impresiones
                If linsImpression = True Then '*Re-impresión

                    Dim lcolReport_prod As eProduct.report_prods
                    Dim lclsReport_prod As eProduct.report_prod
                    lcolReport_prod = New eProduct.report_prods
                
                    If lclsPolicyHist.insCreaPolicy_his_v2("2", lintBranch, lintProduct, lintnPolicy, lstdEffecdate, eRemoteDB.Constants.intNull, Session("nUsercode"), 0, linTypeReport) = True Then '*Genera registro en policy_his 
                        If lcolReport_prod.FindReport_prod_By_Transac("2", lintBranch, lintProduct, lintnPolicy, 0, 1, eRemoteDB.Constants.intNull, lstdEffecdate, True) Then '*Búsquedas de reportes automáticos

                            For Each lclsReport_prod In lcolReport_prod
                                With mobjDocuments
                                    .ReportFilename = lclsReport_prod.sReport
                                    .nReport = 1
                                    .setStorProcParam(1, "2")
                                    .setStorProcParam(2, lintBranch)
                                    .setStorProcParam(3, lintProduct)
                                    .setStorProcParam(4, lintnPolicy)
                                    .setStorProcParam(5, 0)
                                    .setStorProcParam(6, .setdate(lstdEffecdate))
                                    .nMovement = lclsPolicyHist.nMovement
                                    .Merge = False
                                    .nGenPolicy = 1
                                    .nForzaRep = 1
                                    .nTratypep = lclsReport_prod.nTratypep
                                    .MergeCertype = "2"
                                    .MergeBranch = lintBranch
                                    .MergeProduct = lintProduct
                                    .MergePolicy = lintnPolicy
                                    .MergeCertif = 0
                                    .sPolitype = lstrsPolitype
                                    '.sNameReport = Request.QueryString.Item("sReport")
                                    Response.Write((.Command))
                                    .Reset()
                                    .bTimeOut = True
                                End With
                            Next
                        End If
                    End If
                Else 'Opción cuando se selecciona desde el registro de la grilla
                    Dim lcolReport_prod As eProduct.report_prods
                    Dim lclsReport_prod As eProduct.report_prod
                    lcolReport_prod = New eProduct.report_prods
                    If linnType_Hist = 1 Or linnType_Hist = 5 Or linnType_Hist = 19 Then 'Validación de reportes de cuadros de pólizas
                        If lcolReport_prod.FindReport_prod_By_Transac("2", lintBranch, lintProduct, lintnPolicy, 0, 1, eRemoteDB.Constants.intNull, lstdEffecdate, True) Then '*Búsquedas de reportes automáticos
                            For Each lclsReport_prod In lcolReport_prod
                                With mobjDocuments
                                    .ReportFilename = lclsReport_prod.sReport
                                    .nReport = 1
                                    .setStorProcParam(1, "2")
                                    .setStorProcParam(2, lintBranch)
                                    .setStorProcParam(3, lintProduct)
                                    .setStorProcParam(4, lintnPolicy)
                                    .setStorProcParam(5, 0)
                                    .setStorProcParam(6, .setdate(lstdEffecdate))
                                    .nMovement = linnMovement
                                    .Merge = False
                                    .nGenPolicy = 1
                                    .nForzaRep = 1
                                    .nTratypep = lclsReport_prod.nTratypep
                                    .MergeCertype = "2"
                                    .MergeBranch = lintBranch
                                    .MergeProduct = lintProduct
                                    .MergePolicy = lintnPolicy
                                    .MergeCertif = 0
                                    .sPolitype = lstrsPolitype
                                    
                                    '.sNameReport = Request.QueryString.Item("sReport")
                                    Response.Write((.Command))
                                    .Reset()
                                    .bTimeOut = True
                                End With
                            Next
                        End If
                    Else '*Se va por este camino cuando no son reportes de cuadros de pólizas
                        With mobjDocuments
                            .nReport = 1
                            .setStorProcParam(1, "2")
                            .setStorProcParam(2, lintBranch)
                            .setStorProcParam(3, lintProduct)
                            .setStorProcParam(4, lintnPolicy)
                            .setStorProcParam(5, 0)
                            .setStorProcParam(6, .setdate(lstdEffecdate))
                            .nMovement = linnMovement
                            .Merge = False
                            .nGenPolicy = 1
                            .nForzaRep = 1
                            .nTratypep = -1
                            .MergeCertype = "2"
                            .MergeBranch = lintBranch
                            .MergeProduct = lintProduct
                            .MergePolicy = lintnPolicy
                            .MergeCertif = 0
                            .sPolitype = lstrsPolitype
                            '.sNameReport = Request.QueryString.Item("sReport")
                            Response.Write((.Command))
                            .Reset()
                            .bTimeOut = True
                        End With
                    End If
                End If
            ElseIf linTypeReport = 3 Then '*Certificado de cobertura
                lclReport = New eReports.Report
                lclsReport_prod = New eProduct.report_prod
                Dim lclsPolicyHist As ePolicy.Policy_his
                lclsPolicyHist = New ePolicy.Policy_his
            
                'Validación de re-impresiones
                If linsImpression = True Then '*Re-impresión
                    Dim lcolReport_prod As eProduct.report_prods
                    Dim lclsReport_prod As eProduct.report_prod
                    lcolReport_prod = New eProduct.report_prods

                    If lclsPolicyHist.insCreaPolicy_his_v2("2", lintBranch, lintProduct, lintnPolicy, lstdEffecdate, eRemoteDB.Constants.intNull, Session("nUsercode"), linnCertif, linTypeReport) = True Then '*Genera registro en policy_his 
                        If lcolReport_prod.FindReport_prod_By_Transac("2", lintBranch, lintProduct, lintnPolicy, linnCertif, 1, eRemoteDB.Constants.intNull, lstdEffecdate, True) Then
                            For Each lclsReport_prod In lcolReport_prod
                                With mobjDocuments
                                    .ReportFilename = lclsReport_prod.sReport
                                    .nReport = 3
                                    .setStorProcParam(1, "2")
                                    .setStorProcParam(2, lintBranch)
                                    .setStorProcParam(3, lintProduct)
                                    .setStorProcParam(4, lintnPolicy)
                                    .setStorProcParam(5, linnCertif)
                                    .setStorProcParam(6, .setdate(lstdEffecdate))
                                    .nMovement = lclsPolicyHist.nMovement
                                    .Merge = False
                                    .nGenPolicy = 1
                                    .nForzaRep = 1
                                    .nTratypep = lclsReport_prod.nTratypep
                                    .MergeCertype = "2"
                                    .MergeBranch = lintBranch
                                    .MergeProduct = lintProduct
                                    .MergePolicy = lintnPolicy
                                    .MergeCertif = linnCertif
                                    .sPolitype = lstrsPolitype
                                    '.sNameReport = Request.QueryString.Item("sReport")
                                    Response.Write((.Command))
                                    .Reset()
                                    .bTimeOut = True
                                End With
                            Next
                        End If
                    End If
                Else 'Opción cuando se selecciona desde el registro de la grilla
                    Dim lcolReport_prod As eProduct.report_prods
                    Dim lclsReport_prod As eProduct.report_prod
                    lcolReport_prod = New eProduct.report_prods
                
                    If lcolReport_prod.FindReport_prod_By_Transac("2", lintBranch, lintProduct, lintnPolicy, linnCertif, 1, eRemoteDB.Constants.intNull, lstdEffecdate, True) Then
                        For Each lclsReport_prod In lcolReport_prod
                            With mobjDocuments
                                .ReportFilename = lclsReport_prod.sReport
                                .nReport = 3
                                .setStorProcParam(1, "2")
                                .setStorProcParam(2, lintBranch)
                                .setStorProcParam(3, lintProduct)
                                .setStorProcParam(4, lintnPolicy)
                                .setStorProcParam(5, linnCertif)
                                .setStorProcParam(6, .setdate(lstdEffecdate))
                                .nMovement = linnMovement
                                .Merge = False
                                .nGenPolicy = 1
                                .nForzaRep = 1
                                .nTratypep = lclsReport_prod.nTratypep
                                .MergeCertype = "2"
                                .MergeBranch = lintBranch
                                .MergeProduct = lintProduct
                                .MergePolicy = lintnPolicy
                                .MergeCertif = linnCertif
                                .sPolitype = lstrsPolitype
                                '.sNameReport = Request.QueryString.Item("sReport")
                                Response.Write((.Command))
                                .Reset()
                                .bTimeOut = True
                            End With
                        Next
                    End If
                End If
            ElseIf linTypeReport = 4 Then '*Certificado de endoso
                lclReport = New eReports.Report
                lclsReport_prod = New eProduct.report_prod
        
                With mobjDocuments
                    .sCodispl = "CAL0110" 'sCodispl
                    .ReportFilename = "CAL001_A_V.RPT"
                    .setStorProcParam(1, sCertype)
                    .setStorProcParam(2, lintBranch)
                    .setStorProcParam(3, lintProduct)
                    .setStorProcParam(4, lintnPolicy)
                    .setStorProcParam(5, "0")
                    .setStorProcParam(6, .setdate(lstdEffecdate))
                    .setStorProcParam(7, "1")
                    .setStorProcParam(8, "")
                    .setStorProcParam(9, linnMovement)
               
                    .nReport = 2
                    .Merge = False
                    .MergeCertype = sCertype
                    .MergeBranch = lintBranch
                    .MergeProduct = lintProduct
                    .MergePolicy = lintnPolicy
                    .nGenPolicy = 1
                    .nMovement = linnMovement
                    .nForzaRep = 1
                    .nTratypep = 2
                    '.MergeCodispl = sCodispl
                    lclsReport_prod = Nothing
                    .sPolitype = lstrsPolitype
                    ' .sNameReport = Request.QueryString.Item("sReport")
                    Response.Write(.Command)
                End With
            Else
                Response.Write("<SCRIPT>alert('No existe reporte asociado ');</SCRIPT>")
            End If
        End If
    End If
    '+Se escribe en duro el código final (en vez de llamar a dll) para que no haga
    '+proceso de cerrar ventana.
    '+Esto porque cuando se cargan reportes con timeout, se podría
    '+perder la invocación de algún reporte (la págna se recarga antes que pueda
    '+mostrar el reporte)
    Response.Write(vbCrLf)
    Response.Write("<SCRIPT>" & vbCrLf)
    Response.Write("try{" & vbCrLf)
    Response.Write("top.frames['fraFolder'].UpdateDiv('lblWaitProcess','<BR>','');" & vbCrLf & "    if (typeof(top.frames['fraFolder'])!='undefined')" & vbCrLf & "        if (typeof(top.frames['fraFolder'].mstrDoSubmit)!='undefined')" & vbCrLf & "            top.frames['fraFolder'].mstrDoSubmit='1';" & vbCrLf)
    Response.Write("}" & vbCrLf)
    Response.Write("catch(ex){}" & vbCrLf)

    Response.Write("</SCRIPT>")
    mobjValues = Nothing
%>

<%
    '^Begin Footer Block VisualTimer Utility 1.1 7/4/03 11.11.56
    Call mobjNetFrameWork.FinishPage("showdefvalues")
    mobjNetFrameWork = Nothing
    '^End Footer Block VisualTimer
%>