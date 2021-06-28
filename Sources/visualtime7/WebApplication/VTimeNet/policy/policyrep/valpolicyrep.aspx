<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="ePolicy" %>
<%@ Import namespace="eApvc" %>
<%@ Import namespace="eClaim" %>
<%@ Import namespace="eProduct" %>
<%@ Import namespace="eReports" %>
<%@ Import namespace="eBranches" %>
<%@ Import namespace="eSchedule" %> 
<%@ Import Namespace="System.IO" %>
<%@ Import Namespace="System.Collections.Generic" %>
<%@ Import Namespace="System.Text" %>

<script language="VB" runat="Server">
    Dim mobjValues As eFunctions.Values
    Private mstrErrors As String
    Private UploadRequest As Dictionary(Of String, String)
    '+ Se declara la variable para almacenar el String en donde se definen los controles HIDDEN
    '+ de la página que la invoca.
    Dim mstrCommand As String
    Dim mclsPolicy As Object
    Dim mclsPremium As Object
    Dim mblnTimeOut As Boolean
    Dim mstrKey As String
    Dim mclsGeneral As Object
    Dim mstrFileName As String
    Dim mstrPath As Object
    Dim mstrPathReport As Object
    Dim sCodispl As String
    Dim lstrKey702 As String
    Dim lstrKey696 As String
    Dim lstrKey683 As String
    Dim lstrPre_def As String
    Dim lstrIndMass As String
    Dim mdblNumCart As Double
    Dim lstrKeyVil7700 As String
    Dim lclsProduct As Object
    '+ proceso batch 
    Dim lclsBatch_param As Object
    Dim mobjUploadRequest As Object
    Dim crlf As String = Chr(13) & Chr(10)
    Dim myRequestFile(4) As String
    Dim fileContentIndex As Integer
    Dim fileContentLength As Integer
    Dim mstrFileFullPath As String
    ' +Declaración de las variables que reciben los valores de los campos que se deben validar.
    Dim sClient As String
    Dim nIntermed As Integer
    Dim nOfficeAgen As Integer
    Dim ntcnYear As Integer
    Dim dIniDate As Date
    Dim tPolicyType As Integer
    Dim nPolicy As Integer
    Dim tAddressType As Object
    Dim nPolicyFin As Integer
    Dim ncbeMonth As Integer
    Dim nbranch As Integer
    Dim tWayPay As Integer
    Dim nModulec As Integer
    Dim nproduct As Integer
    Dim nClaim As Integer
    Dim dEndDate As Date
    Dim nAgen As Integer
    Dim nUserCode As Integer
    Dim nOffice As Integer
    Dim nPolicyIni As Integer
    Dim nCertif As Integer
    Dim nEstadoag As Integer
    Dim nTypeAmend As Integer
    Dim nBank As Integer
    Dim dDateCopy As String
    Dim sOutputSev As String
    Dim sPolitype As String
    Dim sDirectory
    'UPGRADE_NOTE: IIf was upgraded to IIf_Renamed. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1061.aspx'
    Function IIf_Renamed(ByRef condition As Boolean, ByRef value1 As Object, ByRef value2 As Object) As Object
        If condition Then IIf_Renamed = value1 Else IIf_Renamed = value2
    End Function

    '% insValPolicy: Se realizan las validaciones masivas de la forma
    '-------------------------------------------------------------------------------------------
    Function insValPolicy() As String
        Dim mclsPolicy_CALXXXXX As Object
        Dim mclsPolicy_CAL001 As Object
        Dim lsprocess As String
        Dim mclsPolicy_CAL970 As Object
        Dim mclsPolicy_CAL01415 As Object
        Dim mclsPolicy_CAL01502 As Object
        Dim mclsPolicy_CAL010 As Object
        Dim mclsPolicy_CAL0110 As Object
        '-------------------------------------------------------------------------------------------
        Dim lclsErrors As eFunctions.Errors
        Dim lclsPolicy As ePolicy.Policy

        'UPGRADE_NOTE: The 'eFunctions.Errors' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Program Files\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm
        lclsErrors = New eFunctions.Errors
        'UPGRADE_NOTE: The 'ePolicy.Policy' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Program Files\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm
        lclsPolicy = New ePolicy.Policy
        insValPolicy = vbNullString

        With Request
            Select Case sCodispl
                '+ VIL1890: Declaración jurada 1899 - Certificado No 7
                Case "VIL1890"
                    mclsPolicy = New ePolicy.ValPolicyRep

                    insValPolicy = mclsPolicy.insValVIL1890_K(sCodispl, Request.Form("optProcessType"), mobjValues.StringToType(Request.Form("tcnYear"), eFunctions.Values.eTypeData.etdLong, True), mobjValues.StringToType(Request.Form("cbeDecType"), eFunctions.Values.eTypeData.etdLong, True), mobjValues.StringToType(Request.Form("tcnRectif"), eFunctions.Values.eTypeData.etdLong, True), Request.Form("valClient"), mobjValues.StringToType(Request.Form("tcdPrintDate"), eFunctions.Values.eTypeData.etdDate))
                    'UPGRADE_NOTE: Object mclsPolicy may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
                    mclsPolicy = Nothing

                '%CAL00008: Informe de Control de Digitación
                Case "CAL00008"
                    'UPGRADE_NOTE: The 'ePolicy.ValPolicyRep' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Program Files\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm
                    mclsPolicy = New ePolicy.ValPolicyRep
                    insValPolicy = mclsPolicy.insValCAL00008(sCodispl, dIniDate, dEndDate, tPolicyType)
                    mclsPolicy = Nothing

                '% VT00059 HAD002 Informe de Detalle de Primeras Primas
                Case "CAL01500"
                    'UPGRADE_NOTE: The 'ePolicy.ValPolicyRep' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Program Files\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm
                    mclsPolicy = New ePolicy.ValPolicyRep
                    insValPolicy = mclsPolicy.insValCAL01500(sCodispl, dIniDate, dEndDate)
                    mclsPolicy = Nothing

                Case "CAL001"
                    insValPolicy = True
                    'UPGRADE_NOTE: The 'ePolicy.Policy' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Program Files\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm
                    mclsPolicy_CAL001 = New ePolicy.Policy
                    If mobjValues.StringToType(Request.Form.Item("optEje"), eFunctions.Values.eTypeData.etdDouble, True) = 1 Then
                        insValPolicy = mclsPolicy_CAL001.insValCAL001("CAL001", mobjValues.StringToType(Request.Form.Item("optEje"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Request.Form.Item("cbeBranch1"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Request.Form.Item("valProduct1"), eFunctions.Values.eTypeData.etdLong, True), mobjValues.StringToType(Request.Form.Item("chkRe_im1"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Request.Form.Item("tcnPolicy"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Request.Form.Item("tcnCertif"), eFunctions.Values.eTypeData.etdDouble, True), eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, mobjValues.StringToType(Request.Form.Item("tcdmodDate"), eFunctions.Values.eTypeData.etdDate, True), mobjValues.StringToType(Request.Form.Item("optTrans"), eFunctions.Values.eTypeData.etdLong, True), mobjValues.StringToType(Request.Form.Item("cbeProponum"), eFunctions.Values.eTypeData.etdLong, True), mobjValues.StringToType(Request.Form.Item("tcnProponum"), eFunctions.Values.eTypeData.etdDouble, True))
                    Else
                        insValPolicy = mclsPolicy_CAL001.insValCAL001("CAL001", mobjValues.StringToType(Request.Form.Item("optEje"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Request.Form.Item("cbeBranch2"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Request.Form.Item("valProduct2"), eFunctions.Values.eTypeData.etdDouble, True), eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, mobjValues.StringToType(Request.Form.Item("cbeOffice"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Request.Form.Item("cbeAgency"), eFunctions.Values.eTypeData.etdDouble, True))

                    End If
                    mclsPolicy_CAL001 = Nothing

                '% CAL002: Impresión de cuponeras 
                Case "CAL002"
                    'UPGRADE_NOTE: The 'eCollection.Premium' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Program Files\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm
                    mclsPremium = New eCollection.Premium
                    insValPolicy = mclsPremium.insValCAL002_K(.QueryString("sCodispl"), mobjValues.StringToType(Request.Form.Item("valOffice"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("tcnPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("tcnRec_Beg"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("tcnRec_End"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("tcnCon_Beg"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("tcnCon_End"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("tcdStarDate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Request.Form.Item("tcdEndDate"), eFunctions.Values.eTypeData.etdDate))
                    mclsPremium = Nothing

                '%CAL006: Reservas de Primas
                Case "CAL006"
                    'UPGRADE_NOTE: The 'ePolicy.Policy' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Program Files\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm
                    mclsPolicy = New ePolicy.Policy
                    insValPolicy = mclsPolicy.insValCAL006("CAL006", .Form.Item("sOptOutput"), mobjValues.StringToDate(.Form.Item("tcdEffecdate")), mobjValues.StringToType(.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble, True))

                '%CAL00832: Informe de Entregas de Mandatos
                Case "CAL00832"
                    'UPGRADE_NOTE: The 'ePolicy.ValPolicyRep' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Program Files\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm
                    mclsPolicy = New ePolicy.ValPolicyRep
                    insValPolicy = mclsPolicy.insValCAL00832(sCodispl, dIniDate, dEndDate)

                '%CAL011: Estadísticas asegurado vida y salud
                Case "CAL011"
                    'UPGRADE_NOTE: The 'ePolicy.Policy' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Program Files\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm
                    mclsPolicy = New ePolicy.Policy
                    insValPolicy = mclsPolicy.insValCAL011("CAL011", mobjValues.StringToDate(.Form.Item("tcdInitial")), mobjValues.StringToDate(.Form.Item("tcdFinish")))

                '%CAL014: Nomina de asegurados con DPS
                Case "CAL014"
                    'UPGRADE_NOTE: The 'ePolicy.Policy' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Program Files\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm
                    mclsPolicy = New ePolicy.Policy
                    insValPolicy = mclsPolicy.insValCAL014("CAL014", mobjValues.StringToType(.Form.Item("cbeBranch1"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("valProduct1"), eFunctions.Values.eTypeData.etdLong, True), mobjValues.StringToType(.Form.Item("tcnPolicy"), eFunctions.Values.eTypeData.etdDouble, True))

                '%CAL010: Reportes Carteras.            
                Case "CAL010"
                    insValPolicy = True
                    'UPGRADE_NOTE: The 'ePolicy.Policy' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Program Files\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm
                    mclsPolicy_CAL010 = New ePolicy.Policy
                    If mobjValues.StringToType(Request.Form.Item("cbenTypeReport"), eFunctions.Values.eTypeData.etdDouble, True) = 2 Then
                        insValPolicy = mclsPolicy_CAL010.insValCAL010("CAL010", mobjValues.StringToType(Request.Form.Item("cbeBranch1"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Request.Form.Item("valProduct1"), eFunctions.Values.eTypeData.etdLong, True), mobjValues.StringToType(Request.Form.Item("cbenTypeReport"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Request.Form.Item("tcnPolicy"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Request.Form.Item("tcnCertif"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Request.Form.Item("cbenTypeLetter"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Request.Form.Item("tcdEffecdate"), eFunctions.Values.eTypeData.etdDate, True), mobjValues.StringToType(Request.Form.Item("tcnLetter"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Request.Form.Item("tcnini"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Request.Form.Item("tcnend"), eFunctions.Values.eTypeData.etdDouble, True))
                    ElseIf mobjValues.StringToType(Request.Form.Item("cbenTypeReport"), eFunctions.Values.eTypeData.etdDouble, True) = 5 Then
                        insValPolicy = ""
                    Else
                        insValPolicy = mclsPolicy_CAL010.insValCAL010("CAL010", mobjValues.StringToType(Request.Form.Item("cbeBranch1"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Request.Form.Item("valProduct1"), eFunctions.Values.eTypeData.etdLong, True), mobjValues.StringToType(Request.Form.Item("cbenTypeReport"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Request.Form.Item("tcnPolicy"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Request.Form.Item("tcnCertif"), eFunctions.Values.eTypeData.etdDouble, True), eRemoteDB.Constants.intNull, eRemoteDB.Constants.dtmNull, eRemoteDB.Constants.intNull, mobjValues.StringToType(Request.Form.Item("tcnini"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Request.Form.Item("tcnend"), eFunctions.Values.eTypeData.etdDouble, True))
                    End If
                    mclsPolicy_CAL010 = Nothing

                'Cartola APV
                Case "VIL1486"
                    mclsPolicy = New ePolicy.ValPolicyRep
                    insValPolicy = mclsPolicy.insValVIL1486("VIL1486", Session("sTypeCompanyUser"), mobjValues.StringToType(.Form.Item("cbeBranch1"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("valProduct1"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnPolicy"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnCertif"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcdEffecdate"), eFunctions.Values.eTypeData.etdDate, True), mobjValues.StringToType(.Form.Item("tcdEffecdate2"), eFunctions.Values.eTypeData.etdDate, True))


                'Cartola CUI
                Case "VIL1488"
                    mclsPolicy = New ePolicy.ValPolicyRep
                    insValPolicy = mclsPolicy.insValVIL1488("VIL1488", _
                                                            mobjValues.StringToType(Request.Form("cbeBranch"), eFunctions.Values.eTypeData.etdDouble, True), _
                                                            mobjValues.StringToType(Request.Form("valProduct"), eFunctions.Values.eTypeData.etdDouble), _
                                                            mobjValues.StringToType(Request.Form("tcnPolicy"), eFunctions.Values.eTypeData.etdDouble), _
                                                            mobjValues.StringToType(Request.Form("tcnCertif"), eFunctions.Values.eTypeData.etdDouble), _
                                                            mobjValues.StringToType(Request.Form("tcdDate_ini"), eFunctions.Values.eTypeData.etdDate), _
                                                            mobjValues.StringToType(Request.Form("tcdDate_end"), eFunctions.Values.eTypeData.etdDate), _
                                                            Request.Form("hddsCodispl"))
                    mclsPolicy = Nothing


                '%CAL010: Reportes Carteras.            
                Case "CAL970"
                    insValPolicy = True
                    'UPGRADE_NOTE: The 'ePolicy.Policy' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Program Files\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm
                    mclsPolicy_CAL970 = New ePolicy.Policy
                    insValPolicy = mclsPolicy_CAL970.insValCAL970("CAL970", mobjValues.StringToType(Request.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Request.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdLong, True), mobjValues.StringToType(Request.Form.Item("tcnPolicy"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Request.Form.Item("tcdEndDate"), eFunctions.Values.eTypeData.etdDate, True))
                    mclsPolicy_CAL970 = Nothing

                '%CAL825: Cálculo masivo del Tope de capital por evaluacion
                Case "CAL825"
                    'UPGRADE_NOTE: The 'ePolicy.Policy' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Program Files\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm
                    mclsPolicy = New ePolicy.Policy
                    insValPolicy = mclsPolicy.insValCAL825("CAL825", .Form.Item("optType"), mobjValues.StringToType(.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnPolicy"), eFunctions.Values.eTypeData.etdDouble, True))

                '%CAL683: Exclusión de asegurados
                Case "CAL683"
                    'UPGRADE_NOTE: The 'ePolicy.Roles' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Program Files\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm
                    mclsPolicy = New ePolicy.Roles
                    insValPolicy = mclsPolicy.insValCAL683_k("CAL683", mobjValues.StringToType(.Form.Item("tcdDateRun"), eFunctions.Values.eTypeData.etdDate))

                '%VAL708: Proceso de calculo de intereses por prestamo
                Case "VAL708"
                    'UPGRADE_NOTE: The 'ePolicy.ValPolicyRep' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Program Files\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm
                    mclsPolicy = New ePolicy.ValPolicyRep
                    insValPolicy = mclsPolicy.insValVAL708_k(mobjValues.StringToType(Request.Form.Item("tcnYear"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("tcnMonth"), eFunctions.Values.eTypeData.etdDouble))

                '%CAL782: Pólizas pendientes de impresión/cuponera
                Case "CAL782"
                    'UPGRADE_NOTE: The 'ePolicy.ValPolicyRep' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Program Files\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm
                    mclsPolicy = New ePolicy.ValPolicyRep
                    insValPolicy = mclsPolicy.insValCAL782_k(mobjValues.StringToType(Request.Form.Item("tcdStart"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Request.Form.Item("tcdEnd"), eFunctions.Values.eTypeData.etdDate))

                '+CAL712: Polizas con coberturas de servicios a terceros
                Case "CAL712"
                    With Request
                        'UPGRADE_NOTE: The 'ePolicy.Cover' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Program Files\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm
                        mclsPolicy = New ePolicy.Cover
                        insValPolicy = mclsPolicy.InsValCAL712("CAL712", Session("nInsur_area"), mobjValues.StringToType(.Form.Item("tcdEffecdate"), eFunctions.Values.eTypeData.etdDate, True), mobjValues.StringToType(.Form.Item("valCoverGen"), eFunctions.Values.eTypeData.etdDouble, True))
                        mclsPolicy = Nothing
                    End With

                Case "CAL908"
                    'UPGRADE_NOTE: The 'ePolicy.ValPolicyRep' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Program Files\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm
                    mclsPolicy = New ePolicy.ValPolicyRep
                    insValPolicy = mclsPolicy.InsValCAL908("CAL908", Request.Form.Item("valClient"), mobjValues.StringToType(Request.Form.Item("tcnPolicy"), eFunctions.Values.eTypeData.etdDouble))
                    mclsPolicy = Nothing

                    '+Reporte de compras y ventas de unidades 
                Case "CAL600"
                    insValPolicy = vbNullString
                    'UPGRADE_NOTE: The 'ePolicy.ValPolicyRep' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Program Files\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm
                    mclsPolicy = New ePolicy.ValPolicyRep
                    insValPolicy = mclsPolicy.InsValCAL600_K(mobjValues.StringToType(.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcdEffecdate"), eFunctions.Values.eTypeData.etdDate))

                '+ Reporte de Propuestas/Anticipos
                Case "CAL671"
                    'UPGRADE_NOTE: The 'ePolicy.Policy' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Program Files\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm
                    mclsPolicy = New ePolicy.Policy
                    insValPolicy = mclsPolicy.insValCAL671_k(.QueryString("sCodispl"), .Form.Item("optCertype"), mobjValues.StringToType(.Form.Item("cbeInsur_Area"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcdDateFrom"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcdDateTo"), eFunctions.Values.eTypeData.etdDate))

                '+ CAL01501: Informe de Gestión de Operaciones													 
                Case "CAL01501"
                    'UPGRADE_NOTE: The 'ePolicy.valPolicyrep' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Program Files\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm
                    mclsPolicy = New ePolicy.ValPolicyRep
                    insValPolicy = mclsPolicy.insValCAL01501(.QueryString("sCodispl"), .Form.Item("optCertype"), mobjValues.StringToType(.Form.Item("cbeInsur_Area"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcdDateFrom"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcdDateTo"), eFunctions.Values.eTypeData.etdDate))

                '+CAL01502: Informe de Polizas emitidas
                Case "CAL01502"
                    insValPolicy = True
                    'UPGRADE_NOTE: The 'ePolicy.valPolicyrep' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Program Files\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm
                    mclsPolicy_CAL01502 = New ePolicy.ValPolicyRep

                    If mobjValues.StringToType(Request.Form.Item("optEje"), eFunctions.Values.eTypeData.etdDouble, True) = 1 Then
                        'para el caso de PUNTUAL
                        insValPolicy = mclsPolicy_CAL01502.insValCAL01502("CAL01502", mobjValues.StringToType(Request.Form.Item("optEje"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Request.Form.Item("cbeBranch1"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(Request.Form.Item("valProduct1"), eFunctions.Values.eTypeData.etdLong), nPolicy, nCertif, mobjValues.StringToType(Request.Form.Item("cbePolicyType"), eFunctions.Values.eTypeData.etdLong, True), eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, mobjValues.StringToType(Request.Form.Item("tcdPrintDate"), eFunctions.Values.eTypeData.etdDate))
                    Else
                        'para el caso de MASIVA
                        insValPolicy = mclsPolicy_CAL01502.insValCAL01502("CAL01502", mobjValues.StringToType(Request.Form.Item("optEje"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Request.Form.Item("cbeBranch1"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(Request.Form.Item("valProduct1"), eFunctions.Values.eTypeData.etdLong), eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, mobjValues.StringToType(Request.Form.Item("cbeOfficeAgen"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Request.Form.Item("cbeAgency"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Request.Form.Item("tcdPrintDate"), eFunctions.Values.eTypeData.etdDate))
                    End If
                    mclsPolicy_CAL01502 = Nothing

                '%CAL01503: Reporte de Carta de Polizas
                Case "CAL01503"
                    'UPGRADE_NOTE: The 'ePolicy.ValPolicyRep' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Program Files\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm
                    mclsPolicy = New ePolicy.ValPolicyRep
                    insValPolicy = mclsPolicy.insValCAL01503(sCodispl, nbranch, nproduct, nOfficeAgen, nAgen, dIniDate, dEndDate)

                '+ CAL01504: Reporte de Cotización Más Salud													 
                Case "CAL01504"
                    'UPGRADE_NOTE: The 'ePolicy.valPolicyrep' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Program Files\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm
                    mclsPolicy = New ePolicy.ValPolicyRep
                    insValPolicy = mclsPolicy.InsValCAL01504(.QueryString("sCodispl"), nbranch, nproduct, nPolicy, IIf(mobjValues.StringToType(Request.Form.Item("valIntermedia"), eFunctions.Values.eTypeData.etdLong) < 0, 0, mobjValues.StringToType(Request.Form.Item("valIntermedia"), eFunctions.Values.eTypeData.etdLong)), nUserCode)

                '+ VIL701: Listado de pendientes por exigencias médicas                                            
                Case "VIL701"
                    'UPGRADE_NOTE: The 'ePolicy.Policy' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Program Files\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm
                    mclsPolicy = New ePolicy.Policy
                    insValPolicy = mclsPolicy.insValVIL701_k("VIL701", mobjValues.StringToType(Request.Form.Item("optType"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Request.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Request.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Request.Form.Item("cbeOffice"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Request.Form.Item("tcnPolicy"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Request.Form.Item("tcnCertif"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Request.Form.Item("tcdEffecdatestart"), eFunctions.Values.eTypeData.etdDate, True), mobjValues.StringToType(Request.Form.Item("tcdEffecdateend"), eFunctions.Values.eTypeData.etdDate, True))

                '+ VAL630: Impresión de cartolas de VidActiva
                Case "VAL630"
                    'UPGRADE_NOTE: The 'ePolicy.Activelife' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Program Files\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm
                    mclsPolicy = New ePolicy.Activelife
                    insValPolicy = mclsPolicy.insValVAL630_K("VIL701", mobjValues.StringToType(.QueryString("nMainAction"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("optType"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Request.Form.Item("tcdStartDate"), eFunctions.Values.eTypeData.etdDate, True), mobjValues.StringToType(Request.Form.Item("tcdEndDate"), eFunctions.Values.eTypeData.etdDate, True), "2", mobjValues.StringToType(Request.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Request.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Request.Form.Item("tcnPolicy"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Request.Form.Item("tcnCertif"), eFunctions.Values.eTypeData.etdDouble))

                '+ VIL702: Listado de excluídos por asegurabilidad
                Case "VIL702"
                    'UPGRADE_NOTE: The 'ePolicy.Policy' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Program Files\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm
                    mclsPolicy = New ePolicy.Policy
                    insValPolicy = mclsPolicy.insvalVIL702(.Form.Item("optCertype"), mobjValues.StringToType(.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnCertif"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcdEffecdate"), eFunctions.Values.eTypeData.etdDate))

                '+ VIL7002: Proceso unificado de inversión, intereses y costos
                Case "VIL7002"
                    With Request
                        'UPGRADE_NOTE: The 'ePolicy.Policy' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Program Files\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm
                        mclsPolicy = New ePolicy.Policy
                        insValPolicy = mclsPolicy.insValVIL7002(mobjValues.StringToType(.Form.Item("tcdEffecDate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdDouble))
                    End With

                '+ VIL7003: Listado de movimientos de unidades
                Case "VIL7003"
                    'UPGRADE_NOTE: The 'ePolicy.Policy' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Program Files\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm
                    mclsPolicy = New ePolicy.Policy
                    insValPolicy = mclsPolicy.insvalVIL7003(mobjValues.StringToType(.Form.Item("tcdFromDate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcdToDate"), eFunctions.Values.eTypeData.etdDate))

                '+ VIL7004: Cálculo de saldos diarios.
                Case "VIL7004"
                    With Request
                        'UPGRADE_NOTE: The 'ePolicy.Policy' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Program Files\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm
                        mclsPolicy = New ePolicy.Policy
                        insValPolicy = mclsPolicy.insValVIL7004(mobjValues.StringToType(.Form.Item("tcdEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnCertif"), eFunctions.Values.eTypeData.etdDouble))
                    End With

                '+ VIL7008: Listado de traspasos
                Case "VIL7008"
                    'UPGRADE_NOTE: The 'ePolicy.Policy' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Program Files\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm
                    mclsPolicy = New ePolicy.Policy
                    insValPolicy = mclsPolicy.insvalVIL7008("2", mobjValues.StringToType(.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnCertif"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("valCompany"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcdEffecdatefrom"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcdEffecdateto"), eFunctions.Values.eTypeData.etdDate))

                '+ VAL601: Proceso de cálculo valor póliza
                Case "VAL601"
                    'UPGRADE_NOTE: The 'ePolicy.Account_pol' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Program Files\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm
                    mclsPolicy = New ePolicy.Account_Pol
                    insValPolicy = mclsPolicy.InsValVAL601("VAL601", "2", mobjValues.StringToType(.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnPolicy"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnCertif"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("optEjecution"), mobjValues.StringToType(.Form.Item("tcnYear"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("cbeMonth"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("hdddVp_neg"), eFunctions.Values.eTypeData.etdDate))

                '+ VAL696: Control de Caducidad para polizas de VidActiva
                Case "VAL696"
                    'UPGRADE_NOTE: The 'ePolicy.Activelife' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Program Files\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm
                    mclsPolicy = New ePolicy.Activelife
                    insValPolicy = mclsPolicy.insValVAL696_K("VAL696", mobjValues.StringToType(.QueryString("nMainAction"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("optType"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcdEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble, True))

                '+ VAL633: Generación de recibos para pólizas de VidActiva
                Case "VAL633"
                    With Request
                        'UPGRADE_NOTE: The 'ePolicy.valPolicyTra' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Program Files\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm
                        mclsPolicy = New ePolicy.ValPolicyTra
                        insValPolicy = mclsPolicy.insValVAL633_k("VAL633", "2", mobjValues.StringToType(.Form.Item("cbeBranchP"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("valProductP"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnCertif"), eFunctions.Values.eTypeData.etdDouble), Request.Form.Item("OptInfo"), mobjValues.StringToType(.Form.Item("tcdNextReceip"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcnNegVPMonths"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcdFromDate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcdToDate"), eFunctions.Values.eTypeData.etdDate))
                    End With

                '+ VAL709: Saldación de Primas 
                Case "VAL709"
                    'UPGRADE_NOTE: The 'ePolicy.Activelife' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Program Files\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm
                    mclsPolicy = New ePolicy.Activelife
                    insValPolicy = mclsPolicy.insValVAL709_K("VAL709", mobjValues.StringToType(.QueryString("nMainAction"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcdEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdDouble))

                Case "VIL7012"
                    With Request
                        'UPGRADE_NOTE: The 'ePolicy.Policy' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Program Files\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm
                        mclsPolicy = New ePolicy.Policy
                        insValPolicy = mclsPolicy.insValVIL7012(mobjValues.StringToType(.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnYear"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("cbeMonth"), eFunctions.Values.eTypeData.etdDouble, True))
                    End With

                Case "VIL7000"
                    With Request
                        'UPGRADE_NOTE: The 'ePolicy.Policy' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Program Files\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm
                        mclsPolicy = New ePolicy.Policy
                        insValPolicy = mclsPolicy.insValVIL7000("VIL7000", Session("sTypeCompanyUser"), mobjValues.StringToType(.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnPolicy"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnCertif"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcdInitDate"), eFunctions.Values.eTypeData.etdDate, True), mobjValues.StringToType(.Form.Item("tcdEndDate"), eFunctions.Values.eTypeData.etdDate, True))
                    End With

                Case "VIL7006"
                    With Request
                        'UPGRADE_NOTE: The 'ePolicy.Policy' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Program Files\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm
                        mclsPolicy = New ePolicy.Policy
                        insValPolicy = mclsPolicy.insValVIL7006("VIL7006", Session("sTypeCompanyUser"), mobjValues.StringToType(.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnPolicy"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnCertif"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcdInitDate"), eFunctions.Values.eTypeData.etdDate, True), mobjValues.StringToType(.Form.Item("tcdEndDate"), eFunctions.Values.eTypeData.etdDate, True))
                    End With

                Case "VIL7001"
                    With Request
                        'UPGRADE_NOTE: The 'ePolicy.Policy' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Program Files\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm
                        mclsPolicy = New ePolicy.Policy
                        insValPolicy = mclsPolicy.insValVIL7001("VIL7000", Session("sTypeCompanyUser"), mobjValues.StringToType(.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnPolicy"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnCertif"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcdEndDate"), eFunctions.Values.eTypeData.etdDate, True))
                    End With

                '%CAL826: Calculo de la prima ganada incobrable
                Case "CAL826"
                    'UPGRADE_NOTE: The 'ePolicy.ValPolicyRep' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Program Files\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm
                    mclsPolicy = New ePolicy.ValPolicyRep
                    insValPolicy = mclsPolicy.insValCAL826_k(mobjValues.StringToType(Request.Form.Item("tcdStart"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Request.Form.Item("tcdEnd"), eFunctions.Values.eTypeData.etdDate))

                '%CAL784: Calculo de la prima ganada incobrable
                Case "CAL784"
                    'UPGRADE_NOTE: The 'eBatch.ValBatch' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Program Files\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm
                    mclsPolicy = New eBatch.ValBatch
                    insValPolicy = mclsPolicy.insValCAL784_k(mobjValues.StringToType(Request.Form.Item("tcdStart"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Request.Form.Item("tcdEnd"), eFunctions.Values.eTypeData.etdDate))

                '%CAL854: Validacion Anulación automatica de propuestas/cotizacion 
                Case "CAL854"
                    insValPolicy = ""

                Case "VIL7700"
                    'UPGRADE_NOTE: The 'ePolicy.ValPolicyRep' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Program Files\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm
                    mclsPolicy = New ePolicy.ValPolicyRep
                    insValPolicy = mclsPolicy.insValVIL7700("VIL7700", mobjValues.StringToType(Request.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Request.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("tcnPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("tcnCertif"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("tcdInitDate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Request.Form.Item("tcdEndDate"), eFunctions.Values.eTypeData.etdDate))
                    mclsPolicy = Nothing

                '+ VIL7701: Conversión Automática de Propuesta a Póliza
                '+[APV2]: HAD 1018. Conversión Automática de Propuesta a Póliza
                Case "VIL7701"
                    If .Form.GetValues("optTypeIM").GetValue(1 - 1) = "1" Then
                        lstrIndMass = "1" '--Individual
                    Else
                        lstrIndMass = "2" '--Masivo
                    End If

                    With Request
                        'UPGRADE_NOTE: The 'ePolicy.ValPolicyRep' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Program Files\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm
                        mclsPolicy = New ePolicy.ValPolicyRep
                        insValPolicy = mclsPolicy.insValVIL7701(sCodispl, lstrIndMass, mobjValues.StringToType(.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnProponum"), eFunctions.Values.eTypeData.etdDouble, True))
                    End With
                    mclsPolicy = Nothing

                '+ VIL7020: 
                Case "VIL7020"
                    insValPolicy = ""


                '+ VIL7021: Certificado Nº 24 sobre movimiento anual de APV (por RUT)
                Case "VIL7021"
                    With Request
                        'UPGRADE_NOTE: The 'ePolicy.ValPolicyRep' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Program Files\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm
                        mclsPolicy = New ePolicy.ValPolicyRep
                        insValPolicy = mclsPolicy.insValVIL7021(sCodispl, .Form.Item("tcnYear"), .Form.Item("optST"), .Form.Item("tctClient"))
                    End With

                '+ CAL848: 
                Case "CAL848"
                    If Request.QueryString.Item("nZone") <> "2" Then
                        'UPGRADE_NOTE: The 'ePolicy.ValPolicyRep' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Program Files\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm
                        mclsPolicy = New ePolicy.ValPolicyRep
                        insValPolicy = mclsPolicy.insValCAL848_k(mobjValues.StringToDate(.Form.Item("tcdDateFrom")), mobjValues.StringToDate(Request.Form.Item("tcdDateTo")))
                        mclsPolicy = Nothing
                    Else
                        insValPolicy = ""
                    End If

                Case "VIL1405"
                    'UPGRADE_NOTE: The 'ePolicy.ValPolicyTra' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Program Files\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm
                    mclsPolicy = New ePolicy.ValPolicyTra
                    insValPolicy = mclsPolicy.insValVIL1405(sCodispl, mobjValues.StringToType(.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcdEffecDate"), eFunctions.Values.eTypeData.etdDate))
                    mclsPolicy = Nothing

                Case "VIL1411"
                    With Request
                        If .Form.Item("chkprocess") = "1" Then
                            lsprocess = "1"
                        Else
                            lsprocess = "2"
                        End If
                        'UPGRADE_NOTE: The 'ePolicy.Policy' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Program Files\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm
                        mclsPolicy = New ePolicy.Policy
                        insValPolicy = mclsPolicy.insValVIL1411("VIL1411", Session("sTypeCompanyUser"), mobjValues.StringToType(.Form.Item("cbebranch"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("valproduct"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnpolicy"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcdfecha"), eFunctions.Values.eTypeData.etdDate, True), lsprocess)
                    End With

                Case "VIL900"
                    'UPGRADE_NOTE: The 'ePolicy.ValPolicyRep' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Program Files\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm
                    mclsPolicy = New ePolicy.ValPolicyRep
                    insValPolicy = mclsPolicy.insValVIL900_k(mobjValues.StringToDate(Request.Form.Item("tcdEffecdate")))
                    mclsPolicy = Nothing

                Case "VIL1412"
                    With Request
                        'UPGRADE_NOTE: The 'ePolicy.Policy' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Program Files\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm
                        mclsPolicy = New ePolicy.Policy
                        insValPolicy = mclsPolicy.insValVIL1412("VIL1412", Session("sTypeCompanyUser"), mobjValues.StringToType(.Form.Item("cbebranch"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("valproduct"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcdfecha"), eFunctions.Values.eTypeData.etdDate, True))
                    End With

                Case "VIL1413"
                    'UPGRADE_NOTE: The 'ePolicy.ValPolicyRep' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Program Files\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm
                    mclsPolicy = New ePolicy.ValPolicyRep
                    insValPolicy = mclsPolicy.insValVIL1413_k(sCodispl, mobjValues.StringToType(.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcdDate_ini"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcdDate_end"), eFunctions.Values.eTypeData.etdDate), "1")
                    mclsPolicy = Nothing

                '+ Libro de facturación 
                Case "CAL503"
                    insValPolicy = vbNullString

                '+ Libro de producción SOAP-AS400
                Case "CAL504"
                    insValPolicy = vbNullString

                Case "CAL01415"
                    'UPGRADE_NOTE: The 'ePolicy.Policy' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Program Files\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm
                    mclsPolicy_CAL01415 = New ePolicy.Policy
                    insValPolicy = vbNullString
                    insValPolicy = mclsPolicy_CAL01415.insValCAL1415("CAL01415", .Form.Item("hddsCertype"), mobjValues.StringToType(Request.Form.Item("cbeBranch1"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Request.Form.Item("valProduct1"), eFunctions.Values.eTypeData.etdLong, True), mobjValues.StringToType(Request.Form.Item("tcnPolicy"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Request.Form.Item("tcnCertif"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Request.Form.Item("tcdEffecdate"), eFunctions.Values.eTypeData.etdDate, True))
                    mclsPolicy_CAL01415 = Nothing

                '+ VT00015 GAP 10 Historial del Asegurado
                Case "CAL00975"
                    'UPGRADE_NOTE: The 'ePolicy.ValPolicyRep' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Program Files\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm
                    mclsPolicy = New ePolicy.ValPolicyRep
                    insValPolicy = mclsPolicy.insValCAL00975(sCodispl, sClient, dEndDate)
                    mclsPolicy = Nothing

                '+ VT00055 GAP 07 Resumen de Producción por Cobertura
                Case "CAL00976"
                    'UPGRADE_NOTE: The 'ePolicy.ValPolicyRep' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Program Files\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm
                    mclsPolicy = New ePolicy.ValPolicyRep
                    insValPolicy = mclsPolicy.insValCAL00976(sCodispl, dIniDate, dEndDate)
                    mclsPolicy = Nothing

                '+ VT00065 GAP08 Reporte de Inversiones
                Case "VIL01600"
                    With Request
                        'UPGRADE_NOTE: The 'ePolicy.ValPolicyRep' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Program Files\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm
                        mclsPolicy = New ePolicy.ValPolicyRep
                        insValPolicy = mclsPolicy.insValVIL01600("VIL01600", Session("sTypeCompanyUser"), mobjValues.StringToType(.Form.Item("cbebranch"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("valproduct"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnpolicy"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Request.Form.Item("tcdStarDate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Request.Form.Item("tcdEndDate"), eFunctions.Values.eTypeData.etdDate))
                    End With

                '+CAL01505: Cuadro de Póliza Mas Salud
                '+CAL01506: Cuadro de Póliza Protector
                '+CAL01507: Cuadro de Póliza planificador
                '+CAL01508: Cuadro de Póliza Nuevo APV
                '+CAL01509: Cuadro de Póliza Previsor
                Case "CAL01505", "CAL01506", "CAL01507", "CAL01508", "CAL01509", "CAL01512"
                    insValPolicy = True
                    'UPGRADE_NOTE: The 'ePolicy.valPolicyrep' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Program Files\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm
                    mclsPolicy_CALXXXXX = New ePolicy.ValPolicyRep

                    If mobjValues.StringToType(Request.Form.Item("optEje"), eFunctions.Values.eTypeData.etdDouble, True) = 1 Then
                        'para el caso de PUNTUAL	
                        insValPolicy = mclsPolicy_CALXXXXX.insValCALXXXXX(sCodispl, mobjValues.StringToType(Request.Form.Item("optEje"), eFunctions.Values.eTypeData.etdDouble, True), IIf(mobjValues.StringToType(Request.Form.Item("cbeBranch1"), eFunctions.Values.eTypeData.etdLong, True) < 0, 0, mobjValues.StringToType(Request.Form.Item("cbeBranch1"), eFunctions.Values.eTypeData.etdLong, True)), IIf(mobjValues.StringToType(Request.Form.Item("valProduct1"), eFunctions.Values.eTypeData.etdLong, True) < 0, 0, mobjValues.StringToType(Request.Form.Item("valProduct1"), eFunctions.Values.eTypeData.etdLong, True)), nPolicy, nCertif, mobjValues.StringToType(Request.Form.Item("optTrans"), eFunctions.Values.eTypeData.etdLong, True), eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, mobjValues.StringToType(Request.Form.Item("tcdCopydate"), eFunctions.Values.eTypeData.etdDate, True))
                    Else
                        'para el caso de MASIVA
                        insValPolicy = mclsPolicy_CALXXXXX.insValCALXXXXX(sCodispl, mobjValues.StringToType(Request.Form.Item("optEje"), eFunctions.Values.eTypeData.etdDouble, True), IIf(mobjValues.StringToType(Request.Form.Item("cbeBranch2"), eFunctions.Values.eTypeData.etdLong, True) < 0, 0, mobjValues.StringToType(Request.Form.Item("cbeBranch2"), eFunctions.Values.eTypeData.etdLong, True)), IIf(mobjValues.StringToType(Request.Form.Item("valProduct2"), eFunctions.Values.eTypeData.etdLong, True) < 0, 0, mobjValues.StringToType(Request.Form.Item("valProduct2"), eFunctions.Values.eTypeData.etdLong, True)), eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, IIf(mobjValues.StringToType(Request.Form.Item("cbeOfficeAgen"), eFunctions.Values.eTypeData.etdDouble, True) < 0, 0, mobjValues.StringToType(Request.Form.Item("cbeOfficeAgen"), eFunctions.Values.eTypeData.etdDouble, True)), IIf(mobjValues.StringToType(Request.Form.Item("cbeAgency"), eFunctions.Values.eTypeData.etdDouble, True) < 0, 0, mobjValues.StringToType(Request.Form.Item("cbeAgency"), eFunctions.Values.eTypeData.etdDouble, True)), mobjValues.StringToType(Request.Form.Item("tcdCopydate"), eFunctions.Values.eTypeData.etdDate, True))
                    End If

                '+ CAL08000: Reporte de Cotización de los Productos Protector y Más Protector													 
                Case "CAL08000"
                    'UPGRADE_NOTE: The 'ePolicy.valPolicyrep' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Program Files\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm
                    mclsPolicy = New ePolicy.ValPolicyRep
                    insValPolicy = mclsPolicy.InsValCAL01504(.QueryString("sCodispl"), nbranch, nproduct, nPolicy, IIf(mobjValues.StringToType(Request.Form.Item("valIntermedia"), eFunctions.Values.eTypeData.etdLong) < 0, 0, mobjValues.StringToType(Request.Form.Item("valIntermedia"), eFunctions.Values.eTypeData.etdLong)), nUserCode)

                '+ VIL08000: Reporte  de Ahorros Garantizados			
                Case "VIL08000"
                    'UPGRADE_NOTE: The 'ePolicy.ValPolicyRep' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Program Files\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm
                    mclsPolicy = New ePolicy.ValPolicyRep
                    insValPolicy = mclsPolicy.insValVIL08000(sCodispl, dIniDate, dEndDate)
                    mclsPolicy = Nothing

                '+ VIL08001: reporte de esquema del ahorro garantizado
                Case "VIL08001"
                    'UPGRADE_NOTE: The 'ePolicy.ValPolicyRep' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Program Files\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm
                    mclsPolicy = New ePolicy.ValPolicyRep
                    insValPolicy = mclsPolicy.insValVIL08001_K(sCodispl, mobjValues.StringToType(Request.Form.Item("tcnYear"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Request.Form.Item("cbeMonth"), eFunctions.Values.eTypeData.etdDouble, True))

                '+ VIL8011: Reporte de Emision
                Case "VIL8011"
                    'UPGRADE_NOTE: The 'ePolicy.ValPolicyRep' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Program Files\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm
                    mclsPolicy = New ePolicy.ValPolicyRep
                    insValPolicy = mclsPolicy.insValVIL8033(sCodispl, mobjValues.StringToType(Request.Form.Item("tcnYear"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Request.Form.Item("cbeMonth"), eFunctions.Values.eTypeData.etdDouble, True))

                '% HAD033 VIL8032 : Reporte de resumen producción por oficina
                Case "VIL8032"
                    'UPGRADE_NOTE: The 'ePolicy.ValPolicyRep' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Program Files\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm
                    mclsPolicy = New ePolicy.ValPolicyRep
                    insValPolicy = mclsPolicy.insValVIL8032(sCodispl, dIniDate, dEndDate)
                    mclsPolicy = Nothing

                '% HAD034 - Reporte de Resumen de Produccion por cobertura
                Case "VIL8033"
                    'UPGRADE_NOTE: The 'ePolicy.ValPolicyRep' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Program Files\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm
                    mclsPolicy = New ePolicy.ValPolicyRep
                    insValPolicy = mclsPolicy.insValVIL8033(sCodispl, mobjValues.StringToType(Request.Form.Item("tcnYear"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Request.Form.Item("cbeMonth"), eFunctions.Values.eTypeData.etdDouble, True))
                    mclsPolicy = Nothing

                '+ VIL8020: Reserva/Rentabilidad del ahorro garantizado
                Case "VIL8020"
                    insValPolicy = vbNullString

                '+ VIL8003: Reporte de cotización del producto Previsor Plus
                Case "VIL8003"
                    With Request
                        'UPGRADE_NOTE: The 'ePolicy.ValPolicyRep' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Program Files\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm
                        mclsPolicy = New ePolicy.ValPolicyRep
                        insValPolicy = mclsPolicy.InsValVIL8003("VIL8003", mobjValues.StringToType(.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnPolicy"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("valIntermedia"), eFunctions.Values.eTypeData.etdDouble, True))
                    End With
                    mclsPolicy = Nothing

                '+ VIL8002: Reporte de cotización del producto nuevo APV
                Case "VIL8002"
                    With Request
                        'UPGRADE_NOTE: The 'ePolicy.ValPolicyRep' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Program Files\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm
                        mclsPolicy = New ePolicy.ValPolicyRep
                        insValPolicy = mclsPolicy.InsValVIL8002("VIL8002", mobjValues.StringToType(.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnPolicy"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("valIntermedia"), eFunctions.Values.eTypeData.etdDouble, True))
                    End With
                    mclsPolicy = Nothing

                '+ VIL8004: Reporte de cotización del producto Planificador
                Case "VIL8004"
                    With Request
                        'UPGRADE_NOTE: The 'ePolicy.ValPolicyRep' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Program Files\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm
                        mclsPolicy = New ePolicy.ValPolicyRep
                        insValPolicy = mclsPolicy.InsValVIL8004("VIL8004", mobjValues.StringToType(.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnPolicy"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("valIntermedia"), eFunctions.Values.eTypeData.etdDouble, True))
                    End With
                    mclsPolicy = Nothing

                '+ VIL08006: Reporte de saldos finales por fondo				
                Case "VIL08006"
                    'UPGRADE_NOTE: The 'ePolicy.ValPolicyRep' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Program Files\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm
                    mclsPolicy = New ePolicy.ValPolicyRep
                    insValPolicy = mclsPolicy.insValVIL08006_K(sCodispl, ntcnYear, ncbeMonth)
                    mclsPolicy = Nothing

                '+ VIL8007: Reporte de cartolas mensuales
                Case "VIL8007"
                    With Request
                        'UPGRADE_NOTE: The 'ePolicy.ValPolicyRep' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Program Files\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm
                        mclsPolicy = New ePolicy.ValPolicyRep
                        insValPolicy = mclsPolicy.InsValVIL8007("VIL8007", nbranch, nproduct, ncbeMonth, ntcnYear)
                    End With
                    mclsPolicy = Nothing

                '+ VIL8005: Reporte de Esquema APV
                Case "VIL8005"
                    'UPGRADE_NOTE: The 'ePolicy.ValPolicyRep' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Program Files\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm
                    mclsPolicy = New ePolicy.ValPolicyRep
                    insValPolicy = mclsPolicy.insValVIL8033(sCodispl, ntcnYear, ncbeMonth)
                    mclsPolicy = Nothing

                '+ VIL8009: Reservas por Producto de Ahorros Garantizados
                Case "VIL8009"
                    'UPGRADE_NOTE: The 'ePolicy.ValPolicyRep' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Program Files\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm
                    mclsPolicy = New ePolicy.ValPolicyRep
                    insValPolicy = mclsPolicy.insValVIL8009_K(sCodispl, ntcnYear, ncbeMonth)
                    mclsPolicy = Nothing

                '+ VIL8012: Reporte de FECU Corredores
                Case "VIL8012"
                    'UPGRADE_NOTE: The 'ePolicy.ValPolicyRep' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Program Files\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm
                    mclsPolicy = New ePolicy.ValPolicyRep
                    insValPolicy = mclsPolicy.insValVIL8012_K(sCodispl, dIniDate, dEndDate)
                    mclsPolicy = Nothing

                '+ VIL8030: Reporte de resumen de libro de producción foliado
                Case "VIL8030"
                    'UPGRADE_NOTE: The 'ePolicy.ValPolicyRep' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Program Files\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm
                    mclsPolicy = New ePolicy.ValPolicyRep
                    insValPolicy = mclsPolicy.InsValVIL8030(sCodispl, dIniDate, dEndDate)
                    mclsPolicy = Nothing

                '+ VIL8010: Reporte de FECU Mensual Interno
                Case "VIL8010"
                    'UPGRADE_NOTE: The 'ePolicy.ValPolicyRep' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Program Files\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm
                    mclsPolicy = New ePolicy.ValPolicyRep
                    insValPolicy = mclsPolicy.insValVIL8010_K(sCodispl, dEndDate)
                    mclsPolicy = Nothing

                '+ VIL8031: Reporte de resumen de libro de producción
                Case "VIL8031"
                    'UPGRADE_NOTE: The 'ePolicy.ValPolicyRep' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Program Files\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm
                    mclsPolicy = New ePolicy.ValPolicyRep
                    insValPolicy = mclsPolicy.InsValVIL8031(sCodispl, dIniDate, dEndDate)
                    mclsPolicy = Nothing

                '+ CAL803: Reporte de Cobranza Indiapv por Póliza				
                Case "CAL803"
                    'UPGRADE_NOTE: The 'ePolicy.ValPolicyRep' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Program Files\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm
                    mclsPolicy = New ePolicy.ValPolicyRep
                    insValPolicy = mclsPolicy.insValCAL803(sCodispl, ntcnYear, ncbeMonth)
                    mclsPolicy = Nothing

                Case "CAL01510"
                    'UPGRADE_NOTE: The 'ePolicy.ValPolicyRep' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Program Files\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm
                    mclsPolicy = New ePolicy.ValPolicyRep
                    insValPolicy = ""
                    insValPolicy = mclsPolicy.InsValCAl01510(sCodispl, "2", mobjValues.StringToType(Request.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdLong, True), mobjValues.StringToType(Request.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdLong, True), mobjValues.StringToType(Request.Form.Item("tcnPolicy"), eFunctions.Values.eTypeData.etdLong, True), mobjValues.StringToType(Request.Form.Item("tcnCertif"), eFunctions.Values.eTypeData.etdLong, True), mobjValues.StringToType(Request.Form.Item("tcdBeginDate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Request.Form.Item("tcdEndDate"), eFunctions.Values.eTypeData.etdDate), Session("nusercode"))
                    mclsPolicy = Nothing

                '%CAL08001: Detalle de póliza de vida
                Case "CAL08001"
                    'UPGRADE_NOTE: The 'ePolicy.Valpolicyrep' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Program Files\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm
                    mclsPolicy = New ePolicy.ValPolicyRep
                    insValPolicy = mclsPolicy.insValCAL08001("CAL08001", mobjValues.StringToType(.Form.Item("cbeBranch1"), eFunctions.Values.eTypeData.etdLong, True), mobjValues.StringToType(.Form.Item("valProduct1"), eFunctions.Values.eTypeData.etdLong, True), mobjValues.StringToType(.Form.Item("tcnPolicy"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdLong))

                '%CAL979: Proceso de actualización automática de capitales crecientes/decrecientes
                Case "CAL979"
                    'UPGRADE_NOTE: The 'ePolicy.Valpolicyrep' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Program Files\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm
                    mclsPolicy = New ePolicy.ValPolicyRep
                    With Request
                        insValPolicy = mclsPolicy.insValCAL979("CAL979", mobjValues.StringToType(.Form.Item("tcdEffecdate"), eFunctions.Values.eTypeData.etdDate, True), mobjValues.StringToType(.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdLong, True), mobjValues.StringToType(.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdLong, True), mobjValues.StringToType(.Form.Item("tcnPolicy"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("valModulec"), eFunctions.Values.eTypeData.etdLong, True))
                    End With

                Case "CAL01511"
                    'UPGRADE_NOTE: The 'ePolicy.ValPolicyRep' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Program Files\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm
                    mclsPolicy = New ePolicy.ValPolicyRep
                    insValPolicy = mclsPolicy.InsValCAl01511(sCodispl, "2", mobjValues.StringToType(Request.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdLong, True), mobjValues.StringToType(Request.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdLong, True), mobjValues.StringToType(Request.Form.Item("tcdBeginDate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Request.Form.Item("tcdEndDate"), eFunctions.Values.eTypeData.etdDate), Session("nusercode"))
                    mclsPolicy = Nothing

                Case "CAL8000"
                    insValPolicy = vbNullString

                Case "CAL0110"
                    mclsPolicy_CAL0110 = New ePolicy.ValPolicyRep
                    If CDbl(Request.QueryString.Item("nZone")) <> 1 Then
                        insValPolicy = vbNullString
                    Else
                        insValPolicy = mclsPolicy_CAL0110.insValCAL0110_k(sCodispl, mobjValues.StringToType(Request.Form.Item("optntype"), eFunctions.Values.eTypeData.etdLong, True), mobjValues.StringToType(Request.Form.Item("valTypeReport"), eFunctions.Values.eTypeData.etdLong, True), mobjValues.StringToType(Request.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdLong, True), mobjValues.StringToType(Request.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdLong, True), mobjValues.StringToType(Request.Form.Item("tcdIssuedatIni"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Request.Form.Item("tcdIssuedatEnd"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Request.Form.Item("tcnPolicy"), eFunctions.Values.eTypeData.etdLong, True), mobjValues.StringToType(Request.Form.Item("tcnCertif"), eFunctions.Values.eTypeData.etdLong, True))
                        '+ solo para Solicitud Puntual
                        If Request.Form.Item("optntype") = 1 Then
                            sPolitype = mclsPolicy_CAL0110.sPolitype
                        End If
                    End If
                    mclsPolicy_CAL0110 = Nothing

                Case "CAL665"
                    Dim lobjGeneralFunction As New eGeneral.GeneralFunction
                    mclsPolicy = New ePolicy.ValPolicyRep
                    insValPolicy = mclsPolicy.insValCAL665(sCodispl, Request.Form.Item("hdsFileNameProp"), Request.Form.Item("hdsFileNameRoles"), Request.Form.Item("hdsFileNameBenef"))

                    '+ Si no hubo error se suben los archivos y asigna el sKey.
                    If IsNothing(insValPolicy) OrElse insValPolicy <> vbNullString Then
                        Call insUpLoadFile(mstrPath, Request.Form.Item("hdsFileNameProp"), "tctFileProp", Request.Form.Item("hdsFileNameRoles"), "tctFileRoles", Request.Form.Item("hdsFileNameBenef"), "tctFileBenef")
                        mstrKey = lobjGeneralFunction.getsKey(mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble))
                    End If
                    lobjGeneralFunction = Nothing

                Case "CAL7933"
                    Session("sFile") = String.Empty
                    If Request.Files.Count > 0 AndAlso Not String.IsNullOrEmpty(Request.Files(0).FileName.Trim()) Then
                        Dim sPath As String = mobjValues.insGetSetting("LoadFile", String.Empty, "PATHS")
                        Session("sFile") = sPath & "\" & System.Guid.NewGuid().ToString() & Path.GetFileName(Request.Files(0).FileName)
                        Request.Files(0).SaveAs(Session("sFile"))
                    End If
                    mstrKey = New eGeneral.GeneralFunction().getsKey(Session("nUsercode"))

                    insValPolicy = vbNullString

                Case Else
                    insValPolicy = "ValPolicyRep: Código lógico no encontrado (" & sCodispl & ")"
            End Select

            lclsPolicy = Nothing
            lclsErrors = Nothing

        End With
    End Function

    '% insPostPolicy: Se realizan las actualizaciones de las ventanas
    '-------------------------------------------------------------------------------------------
    Function insPostPolicy() As Boolean
        Dim lstrKey1 As String
        Dim lclsVAL696 As Object
        Dim lclsVIL7000 As Object
        Dim lstrAuxKey1 As String
        Dim lstrAuxKey2 As String
        Dim mclsCAL010 As Object
        Dim lclsVIL8020 As Object
        Dim lsprocess As String
        Dim lobjAuxDocuments2 As Object
        Dim lclsCAL683 As Object
        Dim mclsCal908 As Object
        Dim lobjDocuments1 As Object
        Dim lobjAuxDocuments1 As Object
        Dim mclsCAL001 As Object
        Dim lobjAuxDocuments As Object
        Dim lobjDocuments As Object
        Dim lobjReporte As Object
        Dim lclsVIL7012 As Object
        Dim lclsVIL7002 As Object
        Dim lobjReport2 As Object
        Dim lstrProdVAL633 As String
        Dim lstrRamoVAL633 As String
        Dim lobjReport As Object
        Dim lclsVIL702 As Object
        Dim lstrKey As String
        Dim lstrAuxKey As String
        Dim lclsVIL7006 As Object
        Dim lclsVIL7001 As Object
        '-------------------------------------------------------------------------------------------
        '-Objeto para transacciones batch	
        Dim lclsBatch_param As Object

        '-Indicador de imprimir reportes
        Dim lblnPrintReport As Boolean

        lblnPrintReport = True

        Select Case sCodispl
            '+ VIL701: Listado de pendientes por exigencias médicas
            '+ VIL7003: Listado de movimientos de unidades
            Case "CAL00008"
                insPostPolicy = True

            Case "CAL01500"
                insPostPolicy = True
                lblnPrintReport = True

            Case "CAL001"
                'UPGRADE_NOTE: The 'ePolicy.policy' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Program Files\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm
                mclsCAL001 = New ePolicy.policy
                If mobjValues.StringToType(Request.Form.Item("optEje"), eFunctions.Values.eTypeData.etdDouble, True) = 1 Then
                    insPostPolicy = mclsCAL001.insPostCAL001("2", mobjValues.StringToType(Request.Form.Item("cbeBranch1"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Request.Form.Item("valProduct1"), eFunctions.Values.eTypeData.etdLong, True), mobjValues.StringToType(Request.Form.Item("tcnPolicy"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Request.Form.Item("tcnCertif"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Request.Form.Item("tcdmodDate"), eFunctions.Values.eTypeData.etdDate, True))
                Else
                    insPostPolicy = mclsCAL001.insPostCAL001("2", mobjValues.StringToType(Request.Form.Item("cbeBranch2"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Request.Form.Item("valProduct2"), eFunctions.Values.eTypeData.etdDouble, True), eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, Today)
                End If
                lstrKey683 = mclsCAL001.sKey
                'Response.Write "<NOTSCRIPT>alert('"&mclsCAL001.skey&"');</" & "Script>"   

            Case "CAL00832"
                insPostPolicy = True
                lblnPrintReport = True

            Case "CAL782", "VIL701", "VIL7003", "VIL7008"
                insPostPolicy = True

            '+ VIL1486 Cartola APV
            Case "VIL1486"
                If CStr(Session("BatchEnabled")) <> "1" Then
                    Dim mclsPolicy2 = New ePolicy.ValPolicyRep
                    insPostPolicy = mclsPolicy2.insPostVIL1486(mobjValues.StringToType(Request.Form.Item("cbeBranch1"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Request.Form.Item("valProduct1"), eFunctions.Values.eTypeData.etdLong, True), mobjValues.StringToType(Request.Form.Item("tcnPolicy"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Request.Form.Item("tcnCertif"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Request.Form.Item("tcdEffecdate"), eFunctions.Values.eTypeData.etdDate, True), mobjValues.StringToType(Request.Form.Item("tcdEffecdate2"), eFunctions.Values.eTypeData.etdDate, True), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble))
                    mstrKey = mclsPolicy.sKey

                    If mobjValues.StringToType(Request.Form("tcnNumCart"), eFunctions.Values.eTypeData.etdDouble) = eRemoteDB.Constants.intNull Then
                        mdblNumCart = mclsPolicy.nNumCart
                    Else
                        mdblNumCart = mobjValues.StringToType(Request.Form("tcnNumCart"), eFunctions.Values.eTypeData.etdDouble)
                        insPostPolicy = True
                    End If
                Else
                    '+Se almacenan los parámetros del proceso batch
                    lclsBatch_param = New eSchedule.Batch_Param

                    With lclsBatch_param
                        .nBatch = 1486
                        .nUsercode = mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble)
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, .sKey)         '"sKey",
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, mobjValues.StringToType(Request.Form("cbeBranch1"), eFunctions.Values.eTypeData.etdDouble))      'nBranch
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, mobjValues.StringToType(Request.Form("valProduct1"), eFunctions.Values.eTypeData.etdDouble))     'nProduct
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, mobjValues.StringToType(Request.Form("tcnPolicy"), eFunctions.Values.eTypeData.etdDouble))      'nPolicy
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, mobjValues.StringToType(Request.Form("tcnCertif"), eFunctions.Values.eTypeData.etdDouble))      'nCertif
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, mobjValues.StringToType(Request.Form("tcdEffecdate"), eFunctions.Values.eTypeData.etdDate))     'dStartDate
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, mobjValues.StringToType(Request.Form("tcdEffecdate2"), eFunctions.Values.eTypeData.etdDate))      'dEndDate
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, Session("nUserCode"))                                              'nUsercode
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, "1")
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, "VIL1486")                                       'sCodispl
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, "") 'directorio de archivos.                    
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaRes, .sKey)
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaRes, Request.Form("tcdEffecdate"))
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaRes, Request.Form("tcdEffecdate2"))
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaRes, mdblNumCart)
                        If mobjValues.StringToType(Request.Form("cbeBranch1"), eFunctions.Values.eTypeData.etdDouble, True) = eRemoteDB.Constants.intNull Then
                            .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaRes, "")
                        Else
                            .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaRes, mobjValues.StringToType(Request.Form("cbeBranch1"), eFunctions.Values.eTypeData.etdDouble, True))
                        End If

                        If mobjValues.StringToType(Request.Form("valProduct1"), eFunctions.Values.eTypeData.etdDouble) = eRemoteDB.Constants.intNull Then
                            .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaRes, "")
                        Else
                            .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaRes, mobjValues.StringToType(Request.Form("valProduct1"), eFunctions.Values.eTypeData.etdDouble))
                        End If

                        If mobjValues.StringToType(Request.Form("tcnPolicy"), eFunctions.Values.eTypeData.etdDouble) = eRemoteDB.Constants.intNull Then
                            .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaRes, "")
                        Else
                            .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaRes, mobjValues.StringToType(Request.Form("tcnPolicy"), eFunctions.Values.eTypeData.etdDouble))
                        End If

                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaRes, "1")
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaRes, "VIL1486")
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaRes, Session("nUsercode"))
                        .Save()
                    End With
                    Response.Write("<SCRIPT>alert('Se generó la clave de proceso: " & lclsBatch_param.sKey & "');</" & "Script>")
                    insPostPolicy = True
                    lblnPrintReport = False
                    lclsBatch_param = Nothing
                End If

            Case "CAL010"
                'UPGRADE_NOTE: The 'ePolicy.policy' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Program Files\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm
                mclsCAL010 = New ePolicy.Policy
                If Request.Form.Item("cbenTypeReport") = "5" Then
                    insPostPolicy = mclsCAL010.insPostCAL010("2", mobjValues.StringToType(Request.Form.Item("cbeBranch1"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Request.Form.Item("valProduct1"), eFunctions.Values.eTypeData.etdLong, True), mobjValues.StringToType(Request.Form.Item("tcnPolicy"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Request.Form.Item("tcnCertif"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("cbenTypeReport"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Request.Form.Item("cbenTypeLetter"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Request.Form.Item("tcninia"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Request.Form.Item("tcnenda"), eFunctions.Values.eTypeData.etdDouble, True), Request.Form.Item("txtComent"), Request.Form.Item("tctAtention"))
                Else
                    insPostPolicy = mclsCAL010.insPostCAL010("2", mobjValues.StringToType(Request.Form.Item("cbeBranch1"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Request.Form.Item("valProduct1"), eFunctions.Values.eTypeData.etdLong, True), mobjValues.StringToType(Request.Form.Item("tcnPolicy"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Request.Form.Item("tcnCertif"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("cbenTypeReport"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Request.Form.Item("cbenTypeLetter"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Request.Form.Item("tcnini"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Request.Form.Item("tcnend"), eFunctions.Values.eTypeData.etdDouble, True), Request.Form.Item("txtComent"), Request.Form.Item("tctAtention"))
                End If

                lstrKey683 = mclsCAL010.sKey
                If lstrKey683 <> vbNullString Then
                    lblnPrintReport = True
                Else
                    lblnPrintReport = False
                    Response.Write("<SCRIPT>alert('No existen datos para imprimir, según los parámetros ingresados');</" & "Script>")
                End If

            Case "CAL970"
                insPostPolicy = True

            Case "CAL011"
                If Not insPostPolicy Then
                    If Not lblnPrintReport Then
                        insPostPolicy = True
                        lblnPrintReport = True
                    End If
                    insPostPolicy = True
                End If

            Case "CAL014"
                If Not insPostPolicy Then
                    If Not lblnPrintReport Then
                        insPostPolicy = True
                        lblnPrintReport = True
                    End If
                    insPostPolicy = True
                End If

            Case "CAL002"
                If CStr(Session("BatchEnabled")) <> "1" Then
                    insPostPolicy = True
                Else
                    'UPGRADE_NOTE: The 'eSchedule.Batch_param' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Program Files\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm
                    lclsBatch_param = New eSchedule.Batch_Param
                    With lclsBatch_param
                        .nBatch = 125
                        .nUserCode = mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble)
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, .sKey)
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, mobjValues.StringToType(Request.Form.Item("valOffice"), eFunctions.Values.eTypeData.etdDouble))
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, mobjValues.StringToType(Request.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble, True))
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, mobjValues.StringToType(Request.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdDouble))
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, mobjValues.StringToType(Request.Form.Item("tcnPolicy"), eFunctions.Values.eTypeData.etdDouble))
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, mobjValues.StringToType(Request.Form.Item("tcnRec_Beg"), eFunctions.Values.eTypeData.etdDouble))
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, mobjValues.StringToType(Request.Form.Item("tcnRec_End"), eFunctions.Values.eTypeData.etdDouble))
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, mobjValues.StringToType(Request.Form.Item("tcnCon_Beg"), eFunctions.Values.eTypeData.etdDouble))
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, mobjValues.StringToType(Request.Form.Item("tcnCon_End"), eFunctions.Values.eTypeData.etdDouble))
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, mobjValues.StringToType(Request.Form.Item("tcdStarDate"), eFunctions.Values.eTypeData.etdDate))
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, mobjValues.StringToType(Request.Form.Item("tcdEndDate"), eFunctions.Values.eTypeData.etdDate))
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, .nUserCode)
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaRes, mobjValues.StringToType(Session("nInsur_area"), eFunctions.Values.eTypeData.etdDouble, True))
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaRes, .sKey)
                        .Save()
                    End With
                    Response.Write("<SCRIPT>alert('Se generó la clave de proceso: " & lclsBatch_param.sKey & "');</" & "Script>")
                    lclsBatch_param = Nothing

                    '+Para este proceso no se imprimen los reportes desde acá
                    lblnPrintReport = False
                    insPostPolicy = True
                End If

            Case "CAL712"
                If CStr(Session("BatchEnabled")) <> "1" Then
                    insPostPolicy = True
                Else
                    'UPGRADE_NOTE: The 'eSchedule.Batch_param' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Program Files\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm
                    lclsBatch_param = New eSchedule.Batch_Param
                    With lclsBatch_param
                        .nBatch = 103
                        .nUserCode = mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble)
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, .sKey)
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, mobjValues.StringToType(Session("nInsur_area"), eFunctions.Values.eTypeData.etdDouble))
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, mobjValues.StringToType(Request.Form.Item("tcdEffecdate"), eFunctions.Values.eTypeData.etdDate, True))
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, mobjValues.StringToType(Request.Form.Item("valCoverGen"), eFunctions.Values.eTypeData.etdDouble, True))
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaRes, .sKey)
                        .Save()
                    End With
                    Response.Write("<SCRIPT>alert('Se generó la clave de proceso: " & lclsBatch_param.sKey & "');</" & "Script>")
                    lclsBatch_param = Nothing

                    '+Para este proceso no se imprimen los reportes desde acá
                    lblnPrintReport = False
                    insPostPolicy = True
                End If

            Case "VAL708"
                If CStr(Session("BatchEnabled")) <> "1" Then
                    insPostPolicy = True
                Else
                    'UPGRADE_NOTE: The 'eSchedule.Batch_param' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Program Files\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm
                    lclsBatch_param = New eSchedule.Batch_Param
                    With lclsBatch_param
                        .nBatch = 112
                        .nUserCode = mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble)
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, .sKey)
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, mobjValues.StringToType(Request.Form.Item("tcnMonth"), eFunctions.Values.eTypeData.etdDouble, True))
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, mobjValues.StringToType(Request.Form.Item("tcnYear"), eFunctions.Values.eTypeData.etdDouble, True))
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble))
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, Request.Form.Item("optType"))
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaRes, mobjValues.StringToType(Request.Form.Item("tcnMonth"), eFunctions.Values.eTypeData.etdDouble, True))
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaRes, mobjValues.StringToType(Request.Form.Item("tcnYear"), eFunctions.Values.eTypeData.etdDouble, True))
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaRes, Request.Form.Item("optType"))
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaRes, .sKey)
                        .Save()
                    End With
                    Response.Write("<SCRIPT>alert('Se generó la clave de proceso: " & lclsBatch_param.sKey & "');</" & "Script>")
                    lclsBatch_param = Nothing

                    '+Para este proceso no se imprimen los reportes desde acá                    
                    lblnPrintReport = False
                    insPostPolicy = True
                End If

            '+ CAL908: Pólizas y propuestas de un Asegurado
            Case "CAL908"
                If Request.Form.Item("chkBatch") <> "1" Then
                    'UPGRADE_NOTE: The 'ePolicy.ValPolicyRep' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Program Files\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm
                    mclsCal908 = New ePolicy.ValPolicyRep
                    insPostPolicy = mclsCal908.InsPostCAL908(Request.Form.Item("valClient"), mobjValues.StringToType(Request.Form.Item("tcnPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble))
                    mstrKey = mclsCal908.sKey
                    If mstrKey <> vbNullString Then
                        insPostPolicy = True
                    Else
                        insPostPolicy = False
                    End If
                    lblnPrintReport = insPostPolicy
                    mclsCal908 = Nothing
                Else
                    '+Se almacenan los parámetros del proceso batch
                    'UPGRADE_NOTE: The 'eSchedule.Batch_param' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Program Files\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm
                    lclsBatch_param = New eSchedule.Batch_Param
                    With lclsBatch_param
                        .nBatch = 126
                        .nUserCode = mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble)
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, .sKey)
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, Request.Form.Item("valClient"))
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, mobjValues.StringToType(Request.Form.Item("tcnPolicy"), eFunctions.Values.eTypeData.etdDouble))
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble))
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaRes, Request.Form.Item("valClient"))
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaRes, mobjValues.StringToType(Request.Form.Item("tcnPolicy"), eFunctions.Values.eTypeData.etdDouble))
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaRes, .sKey)
                        .Save()
                    End With
                    Response.Write("<SCRIPT>alert('Se generó la clave de proceso: " & lclsBatch_param.sKey & "');</" & "Script>")
                    lclsBatch_param = Nothing

                    '+Para este proceso no se imprimen los reportes desde acá                    
                    lblnPrintReport = False
                    insPostPolicy = True

                End If
                '+cal600: Reporte de compras y ventas de unidades 
            Case "CAL600"
                insPostPolicy = True
                lobjReport = New eReports.Report
                With lobjReport
                    .sCodispl = "CAL600"
                    If mobjValues.StringToType(Request.Form.Item("opttipo"), eFunctions.Values.eTypeData.etdDouble, True) = 2 Then
                        .ReportFilename = "cal600_det.rpt"
                    Else
                        .ReportFilename = "cal600_group.rpt"
                    End If
                    .setStorProcParam(1, mobjValues.StringToType(Request.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble))
                    .setStorProcParam(2, mobjValues.StringToType(Request.Form.Item("valproduct"), eFunctions.Values.eTypeData.etdDouble))
                    .setStorProcParam(3, mobjValues.StringToType(Request.Form.Item("tcnpolicy"), eFunctions.Values.eTypeData.etdDouble))
                    .setStorProcParam(4, .setdate(Request.Form.Item("tcdEffecdate")))
                    Response.Write((.Command))
                End With
                lobjReport = Nothing
                insPostPolicy = True

                '+ CAL683: Exclusión de asegurados
            Case "CAL683"
                If CStr(Session("BatchEnabled")) <> "1" Then
                    'UPGRADE_NOTE: The 'ePolicy.Roles' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Program Files\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm
                    lclsCAL683 = New ePolicy.Roles
                    lstrKey683 = lclsCAL683.InsPostCAL683("2", mobjValues.StringToType(Request.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("tcnPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("tcdDateRun"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Request.Form.Item("optType"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble))
                    lclsCAL683 = Nothing

                    If lstrKey683 <> vbNullString Then
                        insPostPolicy = True
                    Else
                        insPostPolicy = False
                    End If
                Else
                    '+Se almacenan los parámetros del proceso batch
                    'UPGRADE_NOTE: The 'eSchedule.Batch_param' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Program Files\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm
                    lclsBatch_param = New eSchedule.Batch_Param
                    With lclsBatch_param
                        .nBatch = 100
                        .nUserCode = mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble)
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, .sKey)
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, "2")
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, mobjValues.StringToType(Request.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble))
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, mobjValues.StringToType(Request.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdDouble))
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, mobjValues.StringToType(Request.Form.Item("tcnPolicy"), eFunctions.Values.eTypeData.etdDouble))
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, mobjValues.StringToType(Request.Form.Item("tcdDateRun"), eFunctions.Values.eTypeData.etdDate))
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, mobjValues.StringToType(Request.Form.Item("optType"), eFunctions.Values.eTypeData.etdDouble))
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble))
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaRes, .sKey)
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaRes, mobjValues.StringToType(Request.Form.Item("optType"), eFunctions.Values.eTypeData.etdDouble))
                        .Save()
                    End With
                    Response.Write("<SCRIPT>alert('Se generó la clave de proceso: " & lclsBatch_param.sKey & "');</" & "Script>")
                    lclsBatch_param = Nothing

                    '+Para este proceso no se imprimen los reportes desde acá                    
                    lblnPrintReport = False
                    insPostPolicy = True
                End If

            '+ VAL696: Control de caducidad para pólizas de VidActiva
            Case "VAL696"
                If CStr(Session("BatchEnabled")) <> "1" Then
                    'UPGRADE_NOTE: The 'ePolicy.Activelife' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Program Files\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm
                    lclsVAL696 = New ePolicy.Activelife
                    lstrKey696 = lclsVAL696.InsPostVAL696(mobjValues.StringToType(Request.Form.Item("optType"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("tcdEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdDouble))
                    lclsVAL696 = Nothing

                    If lstrKey696 <> vbNullString Then
                        insPostPolicy = True
                    Else
                        insPostPolicy = False
                    End If
                Else
                    '+Se almacenan los parámetros del proceso batch
                    'UPGRADE_NOTE: The 'eSchedule.Batch_param' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Program Files\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm
                    lclsBatch_param = New eSchedule.Batch_Param
                    With lclsBatch_param
                        .nBatch = 105
                        .nUserCode = mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble)
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, .sKey)
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, mobjValues.StringToType(Request.Form.Item("optType"), eFunctions.Values.eTypeData.etdDouble))
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, mobjValues.StringToType(Request.Form.Item("tcdEffecdate"), eFunctions.Values.eTypeData.etdDate))
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble))
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, mobjValues.StringToType(Request.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble))
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, mobjValues.StringToType(Request.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdDouble))
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaRes, Request.Form.Item("tcdEffecdate"))
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaRes, mobjValues.StringToType(Request.Form.Item("optType"), eFunctions.Values.eTypeData.etdDouble))
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaRes, .sKey)
                        .Save()
                    End With
                    Response.Write("<SCRIPT>alert('Se generó la clave de proceso: " & lclsBatch_param.sKey & "');</" & "Script>")
                    lclsBatch_param = Nothing

                    '+Para este proceso no se imprimen los reportes desde acá                    
                    lblnPrintReport = False
                    insPostPolicy = True
                End If

            '+ VAL709: Saldación de primas
            Case "VAL709"
                '+Se almacenan los parámetros del proceso batch
                'UPGRADE_NOTE: The 'eSchedule.Batch_param' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Program Files\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm
                lclsBatch_param = New eSchedule.Batch_Param
                With lclsBatch_param
                    .nBatch = 128
                    .nUserCode = mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble)
                    .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, .sKey)
                    .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, mobjValues.StringToType(Request.Form.Item("tcdEffecdate"), eFunctions.Values.eTypeData.etdDate))
                    .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, mobjValues.StringToType(Request.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble))
                    .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, mobjValues.StringToType(Request.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdDouble))
                    .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble))
                    .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, mobjValues.StringToType(Request.Form.Item("optType"), eFunctions.Values.eTypeData.etdDouble))
                    .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, mobjValues.StringToType(Request.Form.Item("nPolicy"), eFunctions.Values.eTypeData.etdDouble))
                    .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, mobjValues.StringToType(Request.Form.Item("nReceipt"), eFunctions.Values.eTypeData.etdDouble))
                    .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaRes, .sKey)
                    .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaRes, mobjValues.StringToType(Request.Form.Item("optType"), eFunctions.Values.eTypeData.etdDouble))
                    .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaRes, mobjValues.StringToType(Request.Form.Item("tcdEffecdate"), eFunctions.Values.eTypeData.etdDate))
                    .Save()
                End With
                Response.Write("<SCRIPT>alert('Se generó la clave de proceso: " & lclsBatch_param.sKey & "');</" & "Script>")
                lclsBatch_param = Nothing

                '+Para este proceso no se imprimen los reportes desde acá                    
                lblnPrintReport = False
                insPostPolicy = True

            '+ CAL006 : Reservas de primas
            Case "CAL006"
                If CStr(Session("BatchEnabled")) <> "1" Then
                    With Request
                        insPostPolicy = mclsPolicy.insPostCAL006(.Form.Item("cbeInsurArea"), .Form.Item("sOptOutput"), mobjValues.StringToDate(.Form.Item("tcdEffecdate")), mobjValues.StringToType(.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("nOptAct"), eFunctions.Values.eTypeData.etdDouble, True))
                    End With
                    mstrKey = mclsPolicy.sKey
                    lblnPrintReport = True
                Else
                    'UPGRADE_NOTE: The 'eSchedule.Batch_param' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Program Files\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm
                    lclsBatch_param = New eSchedule.Batch_Param
                    With lclsBatch_param
                        .nBatch = 86
                        .nUserCode = mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble)
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, .sKey)
                        'Parametros Proceso
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, Request.Form.Item("cbeInsurArea"))
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, Request.Form.Item("sOptOutput"))
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, mobjValues.StringToType(Request.Form.Item("tcdEffecdate"), eFunctions.Values.eTypeData.etdDate))
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, mobjValues.StringToType(Request.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble))
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, mobjValues.StringToType(Request.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdDouble))
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, .nUserCode)
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, mobjValues.StringToType(Request.Form.Item("nOptAct"), eFunctions.Values.eTypeData.etdDouble))
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, mobjValues.StringToType(Request.Form.Item("cbeType_reserve"), eFunctions.Values.eTypeData.etdDouble))
                        'Parametros Reporte
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaRes, mobjValues.StringToType(Request.Form.Item("nOptAct"), eFunctions.Values.eTypeData.etdDouble))
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaRes, mobjValues.StringToType(Request.Form.Item("tcdEffecdate"), eFunctions.Values.eTypeData.etdDate))
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaRes, .sKey)
                        .Save()
                    End With
                    Response.Write("<SCRIPT>alert('Se generó la clave de proceso: " & lclsBatch_param.sKey & "');</" & "Script>")
                    lclsBatch_param = Nothing
                    insPostPolicy = True
                    lblnPrintReport = False
                End If

            '+ CAL825: Cálculo masivo del Tope de capital por evaluacion
            Case "CAL825"
                With Request
                    insPostPolicy = mclsPolicy.insPostCAL825(.Form.Item("optType"), mobjValues.StringToType(.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnPolicy"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToDate(.Form.Item("tcdEffecdate")), mobjValues.StringToType(.Form.Item("tcnLegAmount"), eFunctions.Values.eTypeData.etdDouble, True), Session("nUsercode"), Session("SessionId"), Session("sTypeCompanyUser"))
                End With
                If insPostPolicy Then
                    Response.Write("<SCRIPT> alert('Proceso terminado satisfactoriamente')</" & "Script>")
                End If

            '+ CAL671 : Reporte de Propuestas / Cotizaciones
            Case "CAL671"
                With Request
                    insPostPolicy = mclsPolicy.insPostCAL671_k(.QueryString("sCodispl"), mobjValues.StringToType(.QueryString("nMainAction"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("optProcessType"), eFunctions.Values.eTypeData.etdDouble, True), .Form.Item("optCertype"), mobjValues.StringToType(.Form.Item("cbeInsur_Area"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcdDateFrom"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcdDateTo"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("hddUserCode"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeAgency"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeStatQuota"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeWaitCode"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("valIntermedia"), eFunctions.Values.eTypeData.etdDouble))
                End With

            '+CAL01501: Informe de Gestión de Operaciones
            Case "CAL01501"
                insPostPolicy = True
                lblnPrintReport = True

            '+CAL01502: Informe de Polizas emitidas
            Case "CAL01502"
                insPostPolicy = True
                lblnPrintReport = True

            '+CAL01503: Reporte de Carta de Polizas
            Case "CAL01503"
                insPostPolicy = True
                lblnPrintReport = True

            '+CAL01504: Reporte de Cotización Más Salud
            Case "CAL01504"
                insPostPolicy = True
                lblnPrintReport = True

            '+CAL01505: Cuadro de Póliza Mas Salud
            '+CAL01506: Cuadro de Póliza Protector
            '+CAL01507: Cuadro de Póliza planificador
            '+CAL01508: Cuadro de Póliza Nuevo APV
            '+CAL01509: Cuadro de Póliza Previsor
            Case "CAL01505", "CAL01506", "CAL01507", "CAL01508", "CAL01509", "CAL01512"
                dDateCopy = Request.Form.Item("tcdCopydate")
                insPostPolicy = True
                lblnPrintReport = True

            '+ CAL08000: Reporte de Cotización de los Productos Protector y Más Protector	
            Case "CAL08000"
                insPostPolicy = True
                lblnPrintReport = True

            '+ VAL601: Proceso de cálculo valor póliza
            Case "VAL601"
                If CStr(Session("BatchEnabled")) <> "1" Then
                    With Request
                        insPostPolicy = mclsPolicy.InsPostVAL601(.Form.Item("optEjecution"), mobjValues.StringToType(.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnPolicy"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnCertif"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeMonth"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnYear"), eFunctions.Values.eTypeData.etdDouble, True), Session("nUsercode"), Session("SessionId"), .Form.Item("optType"))
                        mstrKey = mclsPolicy.sKey(Session("nUsercode"), Session("SessionId"))
                        lblnPrintReport = True
                    End With
                Else
                    '+Se almacenan los parámetros del proceso batch
                    'UPGRADE_NOTE: The 'eSchedule.Batch_param' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Program Files\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm
                    lclsBatch_param = New eSchedule.Batch_Param
                    With lclsBatch_param
                        .nBatch = 109
                        .nUserCode = mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble)
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, .sKey)
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, Request.Form.Item("optEjecution"))
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, mobjValues.StringToType(Request.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble, True))
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, mobjValues.StringToType(Request.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdDouble, True))
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, mobjValues.StringToType(Request.Form.Item("tcnPolicy"), eFunctions.Values.eTypeData.etdDouble, True))
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, mobjValues.StringToType(Request.Form.Item("tcnCertif"), eFunctions.Values.eTypeData.etdDouble))
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, mobjValues.StringToType(Request.Form.Item("cbeMonth"), eFunctions.Values.eTypeData.etdDouble, True))
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, mobjValues.StringToType(Request.Form.Item("tcnYear"), eFunctions.Values.eTypeData.etdDouble, True))
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble, True))
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, Request.Form.Item("optType")) '+Se ingresa el mismo valor por omision del proceso
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaRes, Request.Form.Item("cbeMonth"))
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaRes, Request.Form.Item("tcnYear"))
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaRes, .sKey)
                        .Save()
                    End With
                    Response.Write("<SCRIPT>alert('Se generó la clave de proceso: " & lclsBatch_param.sKey & "');</" & "Script>")
                    lclsBatch_param = Nothing

                    '+Para este proceso no se imprimen los reportes desde acá                    
                    lblnPrintReport = False
                    insPostPolicy = True
                End If

            '+ VAL633: Generación de recibos para pólizas de VidActiva
            Case "VAL633"
                If CStr(Session("BatchEnabled")) <> "1" Then
                    With Request
                        'UPGRADE_NOTE: The 'ePolicy.valPolicyTra' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Program Files\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm
                        mclsPolicy = New ePolicy.ValPolicyTra
                        If Request.Form.Item("OptInfo") = "2" Then
                            '+ Si se realiza el tratamiento de una póliza específica
                            insPostPolicy = mclsPolicy.insPostVAL633_k("VAL633", "2", mobjValues.StringToType(.Form.Item("cbeBranchP"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("valProductP"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnCertif"), eFunctions.Values.eTypeData.etdDouble), Request.Form.Item("OptInfo"), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("SessionID"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcdFromDate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcdToDate"), eFunctions.Values.eTypeData.etdDate))
                        Else
                            insPostPolicy = mclsPolicy.insPostVAL633_k("VAL633", "2", mobjValues.StringToType(.Form.Item("cbeBranchM"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("valProductM"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnCertif"), eFunctions.Values.eTypeData.etdDouble), Request.Form.Item("OptInfo"), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("SessionID"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcdFromDate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcdToDate"), eFunctions.Values.eTypeData.etdDate))
                        End If
                        mstrKey = mclsPolicy.sKey
                    End With
                    '+Nuevo manejo batch
                Else
                    '+Proceso puntual
                    If Request.Form.Item("OptInfo") = "2" Then
                        lstrRamoVAL633 = Request.Form.Item("cbeBranchP")
                        lstrProdVAL633 = Request.Form.Item("valProductP")
                        '+Proceso masivo
                    Else
                        lstrRamoVAL633 = Request.Form.Item("cbeBranchM")
                        lstrProdVAL633 = Request.Form.Item("valProductM")
                    End If

                    '+Se almacenan los parámetros del proceso batch
                    'UPGRADE_NOTE: The 'eSchedule.Batch_param' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Program Files\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm
                    lclsBatch_param = New eSchedule.Batch_Param
                    With lclsBatch_param
                        .nBatch = 111
                        .nUserCode = mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble)
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, .sKey)
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, "2")
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, mobjValues.StringToType(lstrRamoVAL633, eFunctions.Values.eTypeData.etdDouble, True))
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, mobjValues.StringToType(lstrProdVAL633, eFunctions.Values.eTypeData.etdDouble, True))
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, mobjValues.StringToType(Request.Form.Item("tcnPolicy"), eFunctions.Values.eTypeData.etdDouble))
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, mobjValues.StringToType(Request.Form.Item("tcnCertif"), eFunctions.Values.eTypeData.etdDouble))
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, Today)
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, mobjValues.StringToType(Request.Form.Item("tcdFromDate"), eFunctions.Values.eTypeData.etdDate))
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, mobjValues.StringToType(Request.Form.Item("tcdToDate"), eFunctions.Values.eTypeData.etdDate))
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble))
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaRes, Request.Form.Item("tcdFromDate"))
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaRes, Request.Form.Item("tcdToDate"))
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaRes, .sKey)
                        .Save()
                    End With
                    Response.Write("<SCRIPT>alert('Se generó la clave de proceso: " & lclsBatch_param.sKey & "');</" & "Script>")
                    lclsBatch_param = Nothing

                    '+Para este proceso no se imprimen los reportes desde acá                    
                    lblnPrintReport = False
                    insPostPolicy = True
                End If

            '+ VIL702: Listado de excluídos por asegurabilidad
            Case "VIL702"
                If CStr(Session("BatchEnabled")) <> "1" Then
                    'UPGRADE_NOTE: The 'ePolicy.Policy' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Program Files\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm
                    lclsVIL702 = New ePolicy.Policy
                    lstrKey702 = lclsVIL702.insVIL702(Request.Form.Item("optCertype"), mobjValues.StringToType(Request.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Request.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Request.Form.Item("tcnPolicy"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Request.Form.Item("tcnCertif"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Request.Form.Item("tcdEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble))
                    lclsVIL702 = Nothing
                    If lstrKey702 <> vbNullString Then
                        insPostPolicy = True
                    Else
                        insPostPolicy = False
                    End If
                Else
                    '+Se almacenan los parámetros del proceso batch
                    'UPGRADE_NOTE: The 'eSchedule.Batch_param' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Program Files\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm
                    lclsBatch_param = New eSchedule.Batch_Param
                    With lclsBatch_param
                        .nBatch = 114
                        .nUserCode = mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble)
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, .sKey)
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, Request.Form.Item("optCertype"))
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, mobjValues.StringToType(Request.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble, True))
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, mobjValues.StringToType(Request.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdDouble, True))
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, mobjValues.StringToType(Request.Form.Item("tcnPolicy"), eFunctions.Values.eTypeData.etdDouble, True))
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, mobjValues.StringToType(Request.Form.Item("tcnCertif"), eFunctions.Values.eTypeData.etdDouble, True))
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, mobjValues.StringToType(Request.Form.Item("tcdEffecdate"), eFunctions.Values.eTypeData.etdDate))
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, .nUserCode)
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaRes, .sKey)
                        .Save()
                    End With
                    Response.Write("<SCRIPT>alert('Se generó la clave de proceso: " & lclsBatch_param.sKey & "');</" & "Script>")
                    lclsBatch_param = Nothing

                    '+ Para este proceso no se imprimen los reportes desde acá                    
                    lblnPrintReport = False
                    insPostPolicy = True
                End If

            '+ VIL7002: Proceso unificado de inversión, intereses y costos
            Case "VIL7002"
                If CStr(Session("BatchEnabled")) <> "1" Then
                    'UPGRADE_NOTE: The 'eReports.Report' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Program Files\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm
                    lobjDocuments = CreateObject("eReports.Report")
                    With lobjDocuments
                        'UPGRADE_NOTE: The 'ePolicy.Policy' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Program Files\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm
                        lclsVIL7002 = New ePolicy.Policy
                        lstrKey = lclsVIL7002.insVIL7002(mobjValues.StringToType(Request.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("tcdEffecDate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble))
                        If lstrKey <> vbNullString Then
                            .sCodispl = "VIL7002"
                            .ReportFilename = "VIL7002.rpt"
                            .setStorProcParam(1, lstrKey)
                            Response.Write((.Command))
                        End If
                        lclsVIL7002 = Nothing
                    End With
                    lobjDocuments = Nothing
                Else
                    '+Se almacenan los parámetros del proceso batch
                    'UPGRADE_NOTE: The 'eSchedule.Batch_param' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Program Files\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm
                    lclsBatch_param = New eSchedule.Batch_Param
                    With lclsBatch_param
                        .nBatch = 106
                        .nUserCode = mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble)
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, .sKey)
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, mobjValues.StringToType(Request.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble))
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, mobjValues.StringToType(Request.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdDouble))
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble))
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, mobjValues.StringToType(Request.Form.Item("tcdEffecDate"), eFunctions.Values.eTypeData.etdDate))
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaRes, .sKey)
                        .Save()
                    End With
                    Response.Write("<SCRIPT>alert('Se generó la clave de proceso: " & lclsBatch_param.sKey & "');</" & "Script>")
                    lclsBatch_param = Nothing
                End If
                insPostPolicy = True

            '+ VIL7004: Cálculo de saldos diarios.
            Case "VIL7004"
                If CStr(Session("BatchEnabled")) <> "1" Then
                    'UPGRADE_NOTE: The 'ePolicy.Policy' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Program Files\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm
                    mclsPolicy = New ePolicy.Policy
                    insPostPolicy = mclsPolicy.insPostVIL7004(mobjValues.StringToType(Request.Form.Item("tcdEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Request.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("tcnPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("tcnCertif"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble))
                    lblnPrintReport = False
                    If insPostPolicy Then
                        Response.Write("<SCRIPT> alert('Proceso terminado satisfactoriamente')</" & "Script>")
                    End If
                Else
                    '+ Se almacenan los parámetros del proceso batch
                    'UPGRADE_NOTE: The 'eSchedule.Batch_param' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Program Files\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm
                    lclsBatch_param = New eSchedule.Batch_Param
                    With lclsBatch_param
                        .nBatch = 107
                        .nUserCode = mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble)
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, .sKey)
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, mobjValues.StringToType(Request.Form.Item("tcdEffecdate"), eFunctions.Values.eTypeData.etdDate))
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble))
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, 1)
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, mobjValues.StringToType(Request.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble))
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, mobjValues.StringToType(Request.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdDouble))
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, mobjValues.StringToType(Request.Form.Item("tcnPolicy"), eFunctions.Values.eTypeData.etdDouble))
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, mobjValues.StringToType(Request.Form.Item("tcnCertif"), eFunctions.Values.eTypeData.etdDouble))
                        .Save()
                    End With
                    Response.Write("<SCRIPT>alert('Se generó la clave de proceso: " & lclsBatch_param.sKey & "');</" & "Script>")
                    lclsBatch_param = Nothing
                    lblnPrintReport = False
                    insPostPolicy = True
                End If

            '+ VIL7008: Listado de traspasos
            Case "VIL7008"
                'UPGRADE_NOTE: The 'eReports.Report' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Program Files\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm
                lobjDocuments = CreateObject("eReports.Report")
                With lobjDocuments
                    .sCodispl = "VIL7008"
                    .ReportFilename = "VIL7008.rpt"
                    .setStorProcParam(1, mobjValues.StringToType(Request.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble))
                    .setStorProcParam(2, mobjValues.StringToType(Request.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdDouble))
                    .setStorProcParam(3, mobjValues.StringToType(Request.Form.Item("tcnPolicy"), eFunctions.Values.eTypeData.etdDouble))
                    .setStorProcParam(4, mobjValues.StringToType(Request.Form.Item("tcnCertif"), eFunctions.Values.eTypeData.etdDouble))
                    .setStorProcParam(5, mobjValues.StringToType(Request.Form.Item("valCompany"), eFunctions.Values.eTypeData.etdDouble))
                    .setStorProcParam(6, Request.Form.Item("tcdEffecdatefrom"))
                    .setStorProcParam(7, Request.Form.Item("tcdEffecdateto"))
                    Response.Write((.Command))
                End With
                lobjDocuments = Nothing

            Case "VIL7012"
                lobjDocuments1 = New eReports.Report
                With lobjDocuments1
                    lclsVIL7012 = New ePolicy.Policy
                    lstrKey1 = lclsVIL7012.insVIL7012(mobjValues.StringToType(Request.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Request.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Request.Form.Item("tcnYear"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Request.Form.Item("cbeMonth"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble))

                    If lstrKey1 <> vbNullString Then
                        .sCodispl = "VIL7012"
                        .ReportFilename = "VIL7012.rpt"
                        .setStorProcParam(1, lstrKey1)
                        Response.Write((.Command))
                    End If
                    lclsVIL7012 = Nothing
                End With

                lobjDocuments1 = Nothing
                insPostPolicy = True

            '+ VIL7000 Cartola detallada de movimientos
            Case "VIL7000"

                'UPGRADE_NOTE: The 'eReports.Report' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Program Files\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm
                lobjAuxDocuments = New eReports.Report

                With lobjAuxDocuments
                    'UPGRADE_NOTE: The 'ePolicy.Policy' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Program Files\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm
                    lclsVIL7000 = New ePolicy.Policy
                    lstrAuxKey = lclsVIL7000.insPostVIL7000(mobjValues.StringToType(Request.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Request.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Request.Form.Item("tcnPolicy"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Request.Form.Item("tcnCertif"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Request.Form.Item("tcdInitDate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Request.Form.Item("tcdEndDate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble))

                    If lstrAuxKey <> vbNullString Then
                        .sCodispl = "VIL7000"
                        .ReportFilename = "VIL7000.rpt"
                        .setStorProcParam(1, lstrAuxKey)
                        Response.Write((.Command))
                    End If
                    lclsVIL7000 = Nothing
                End With

                lobjAuxDocuments = Nothing
                insPostPolicy = True

            '+ VIL7006 Reserva por valor del fondo
            Case "VIL7006"

                'UPGRADE_NOTE: The 'eReports.Report' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Program Files\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm
                lobjAuxDocuments1 = New eReports.Report
                With lobjAuxDocuments1
                    'UPGRADE_NOTE: The 'ePolicy.Policy' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Program Files\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm
                    lclsVIL7006 = New ePolicy.Policy
                    lstrAuxKey1 = lclsVIL7006.insPostVIL7006(mobjValues.StringToType(Request.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Request.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Request.Form.Item("tcnPolicy"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Request.Form.Item("tcnCertif"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Request.Form.Item("tcdInitDate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Request.Form.Item("tcdEndDate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble))

                    If lstrAuxKey1 <> vbNullString Then
                        .sCodispl = "VIL7006"
                        .ReportFilename = "VIL7006.rpt"
                        .setStorProcParam(1, lstrAuxKey1)
                        Response.Write((.Command))
                    End If
                    lclsVIL7006 = Nothing
                End With
                lobjAuxDocuments1 = Nothing
                insPostPolicy = True

            '+ VIL7001 Cartola Anual Tributaria
            Case "VIL7001"
                'UPGRADE_NOTE: The 'eReports.Report' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Program Files\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm
                lobjAuxDocuments2 = New eReports.Report

                With lobjAuxDocuments2
                    'UPGRADE_NOTE: The 'ePolicy.Policy' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Program Files\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm
                    lclsVIL7001 = New ePolicy.Policy
                    lstrAuxKey2 = lclsVIL7001.insPostVIL7001(mobjValues.StringToType(Request.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Request.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Request.Form.Item("tcnPolicy"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Request.Form.Item("tcnCertif"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Request.Form.Item("tcdEndDate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble))

                    If lstrAuxKey2 <> vbNullString Then
                        .sCodispl = "VIL7001"
                        .ReportFilename = "VIL7001.rpt"
                        .setStorProcParam(1, lstrAuxKey2)
                        Response.Write((.Command))
                    End If
                    lclsVIL7001 = Nothing
                End With
                lobjAuxDocuments2 = Nothing
                insPostPolicy = True

            '%CAL826: Calculo de la prima ganada incobrable
            Case "CAL826"
                insPostPolicy = mclsPolicy.insPostCAL826_k(mobjValues.StringToType(Request.Form.Item("tcdStart"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Request.Form.Item("tcdEnd"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Request.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Request.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble), Request.Form.Item("optProcessType"))

            '%VAL630: Cartola VidActiva
            Case "VAL630"
                'UPGRADE_NOTE: The 'ePolicy.ValPolicyRep' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Program Files\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm
                mclsPolicy = New ePolicy.ValPolicyRep
                insPostPolicy = mclsPolicy.insPostVAL630_k(mobjValues.StringToType(Request.Form.Item("tcdStartDate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Request.Form.Item("tcdEndDate"), eFunctions.Values.eTypeData.etdDate), "2", mobjValues.StringToType(Request.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Request.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Request.Form.Item("tcnPolicy"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Request.Form.Item("tcnCertif"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Request.Form.Item("optType"), eFunctions.Values.eTypeData.etdDouble))
                mstrKey = mclsPolicy.sKey

            '%CAL784: Generación automática de propuestas de renovación
            Case "CAL784"
                insPostPolicy = mclsPolicy.insPostCAL784_k(mobjValues.StringToType(Request.Form.Item("tcdStart"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Request.Form.Item("tcdEnd"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Request.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Request.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Request.Form.Item("cbeOffice"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Request.Form.Item("cbeOfficeAgen"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Request.Form.Item("valIntermedia"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble), Request.Form.Item("ChkOption"))
                Session("ChkOptionCAL784") = Request.Form.Item("ChkOption")

            '%CAL854: Validacion Anulación automatica de propuestas/cotizacion 
            Case "CAL854"
                'UPGRADE_NOTE: The 'eBatch.ValBatch' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Program Files\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm
                mclsPolicy = New eBatch.ValBatch
                insPostPolicy = mclsPolicy.insPostCAL854_k(Request.Form.Item("ChkOption"), mobjValues.StringToType(Request.Form.Item("valOrigin"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Request.Form.Item("tcdDate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Request.Form.Item("valproduct"), eFunctions.Values.eTypeData.etdDouble, True))

            Case "VIL7700"
                If mobjValues.StringToType(Request.Form.Item("tcnNumCart"), eFunctions.Values.eTypeData.etdDouble) = eRemoteDB.Constants.intNull Then
                    'UPGRADE_NOTE: The 'ePolicy.ValPolicyRep' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Program Files\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm
                    mclsPolicy = New ePolicy.ValPolicyRep
                    insPostPolicy = mclsPolicy.insPostVIL7700(0, mobjValues.StringToType(Request.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Request.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("tcnPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("tcnCertif"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("tcdInitDate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Request.Form.Item("tcdEndDate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble))
                    mdblNumCart = mclsPolicy.nNumCart
                    lstrKeyVil7700 = mclsPolicy.sKey
                    mclsPolicy = Nothing
                Else
                    mdblNumCart = mobjValues.StringToType(Request.Form.Item("tcnNumCart"), eFunctions.Values.eTypeData.etdDouble)
                    insPostPolicy = True
                End If


                '+--------------------------------------------
                'CARTOLA  CUI

            Case "VIL1488"
                If Session("BatchEnabled") <> "1" Then
                    mclsPolicy = New ePolicy.ValPolicyRep
                    insPostPolicy = mclsPolicy.insPostVIL1488(0, _
                                                              "", _
                                                              mobjValues.StringToType(Request.Form("cbeBranch"), eFunctions.Values.eTypeData.etdDouble, True), _
                                                              mobjValues.StringToType(Request.Form("valProduct"), eFunctions.Values.eTypeData.etdDouble), _
                                                              mobjValues.StringToType(Request.Form("tcnPolicy"), eFunctions.Values.eTypeData.etdDouble), _
                                                              mobjValues.StringToType(Request.Form("tcnCertif"), eFunctions.Values.eTypeData.etdDouble), _
                                                              mobjValues.StringToType(Request.Form("tcdDate_ini"), eFunctions.Values.eTypeData.etdDate), _
                                                              mobjValues.StringToType(Request.Form("tcdDate_end"), eFunctions.Values.eTypeData.etdDate), _
                                                              mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble), _
                                                              Request.Form("chkprocess"), _
                                                              Request.Form("hddsCodispl"))

                    mstrKey = mclsPolicy.sKey
                    If mobjValues.StringToType(Request.Form("tcnNumCart"), eFunctions.Values.eTypeData.etdDouble) = eRemoteDB.Constants.intNull Then
                        mdblNumCart = mclsPolicy.nNumCart
                        'insPostPolicy = True
                        mclsPolicy = Nothing
                    Else
                        mdblNumCart = mobjValues.StringToType(Request.Form("tcnNumCart"), eFunctions.Values.eTypeData.etdDouble)
                        insPostPolicy = True
                    End If
                Else

                    sDirectory = mobjValues.insGetSetting("ExportDirectoryReport", "/Reports/", "Paths")
                    mdblNumCart = mobjValues.StringToType(Request.Form("tcnNumCart"), eFunctions.Values.eTypeData.etdDouble)
                    '+Se almacenan los parámetros del proceso batch
                    lclsBatch_param = New eSchedule.Batch_Param
                    With lclsBatch_param
                        .nBatch = 1488
                        .nUsercode = mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble)

                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, .sKey)         '"sKey",
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, mobjValues.StringToType(Request.Form("cbeBranch"), eFunctions.Values.eTypeData.etdDouble))      'nBranch
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, mobjValues.StringToType(Request.Form("valProduct"), eFunctions.Values.eTypeData.etdDouble))     'nProduct
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, mobjValues.StringToType(Request.Form("tcnPolicy"), eFunctions.Values.eTypeData.etdDouble))      'nPolicy
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, mobjValues.StringToType(Request.Form("tcnCertif"), eFunctions.Values.eTypeData.etdDouble))      'nCertif
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, mobjValues.StringToType(Request.Form("tcdDate_ini"), eFunctions.Values.eTypeData.etdDate))     'dStartDate
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, mobjValues.StringToType(Request.Form("tcdDate_end"), eFunctions.Values.eTypeData.etdDate))      'dEndDate
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, Session("nUserCode"))                                              'nUsercode
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, Request.Form("chkprocess"))                                        'sProjectvul
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, Request.Form("hddsCodispl"))                                       'sCodispl
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, sDirectory) 'directorio de archivos.                    
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, mdblNumCart)

                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaRes, .sKey)
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaRes, Request.Form("tcdDate_ini"))
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaRes, Request.Form("tcdDate_end"))
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaRes, mdblNumCart)
                        If mobjValues.StringToType(Request.Form("cbeBranch"), eFunctions.Values.eTypeData.etdDouble, True) = eRemoteDB.Constants.intNull Then
                            .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaRes, "")
                        Else
                            .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaRes, mobjValues.StringToType(Request.Form("cbeBranch"), eFunctions.Values.eTypeData.etdDouble, True))
                        End If

                        If mobjValues.StringToType(Request.Form("valProduct"), eFunctions.Values.eTypeData.etdDouble) = eRemoteDB.Constants.intNull Then
                            .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaRes, "")
                        Else
                            .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaRes, mobjValues.StringToType(Request.Form("valProduct"), eFunctions.Values.eTypeData.etdDouble))
                        End If

                        If mobjValues.StringToType(Request.Form("tcnPolicy"), eFunctions.Values.eTypeData.etdDouble) = eRemoteDB.Constants.intNull Then
                            .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaRes, "")
                        Else
                            .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaRes, mobjValues.StringToType(Request.Form("tcnPolicy"), eFunctions.Values.eTypeData.etdDouble))
                        End If

                        If Request.Form("chkprocess") = "1" Then
                            .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaRes, "1")
                        Else
                            .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaRes, "2")
                        End If
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaRes, Request.Form("hddsCodispl"))
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaRes, Session("nUsercode"))

                        .Save()
                    End With
                    Response.Write("<SCRIPT>alert('Se generó la clave de proceso: " & lclsBatch_param.sKey & "');</" & "SCRIPT>")

                    insPostPolicy = True
                    lblnPrintReport = False

                    lclsBatch_param = Nothing

                End If


                '+ VIL900: Cálculo Devolucion de Experiencia Favorable
            Case "VIL900"
                If CStr(Session("BatchEnabled")) <> "1" Then
                    'UPGRADE_NOTE: The 'ePolicy.ValPolicyRep' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Program Files\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm
                    mclsPolicy = New ePolicy.ValPolicyRep
                    insPostPolicy = mclsPolicy.insPostVIL900_k(mobjValues.StringToType(Request.Form.Item("tcdEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble))
                    '+Muestra el Archivo *.txt recien generado                    
                    Response.Write("<SCRIPT>window.open('/VTimeNet/Tfiles/" & mclsPolicy.sFile_name & "', 'Archivo', 'toolbar=yes,location=no,directories=no,status=yes,menubar=yes,scrollbars=yes,copyhistory=no,resizable=yes,width=300,height=300,left=0,top=0',false);</" & "Script>")
                    lblnPrintReport = False
                    mclsPolicy = Nothing
                Else
                    '+ Se almacenan los parámetros del proceso batch
                    'UPGRADE_NOTE: The 'eSchedule.Batch_param' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Program Files\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm
                    lclsBatch_param = New eSchedule.Batch_Param
                    With lclsBatch_param
                        .nBatch = 300
                        .nUserCode = mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble)
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, mobjValues.StringToType(Request.Form.Item("tcdEffecdate"), eFunctions.Values.eTypeData.etdDate))
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, .nUserCode)
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, .sKey)
                        .Save()
                    End With
                    Response.Write("<SCRIPT>alert('Se generó la clave de proceso: " & lclsBatch_param.sKey & "');</" & "Script>")
                    lclsBatch_param = Nothing
                    lblnPrintReport = False
                    insPostPolicy = True
                End If

            '+ VIL7701: Conversión Automática de Propuesta a Póliza
            '+[APV2]: HAD 1018. Conversión Automática de Propuesta a Póliza
            Case "VIL7701"
                With Request
                    If .Form.GetValues("optTypePD").GetValue(1 - 1) = "1" Then
                        lstrPre_def = "1" '--Preliminar
                    Else
                        lstrPre_def = "2" '--Definitivo
                    End If

                    If .Form.GetValues("optTypeIM").GetValue(1 - 1) = "1" Then
                        lstrIndMass = "1" '--Individual
                    Else
                        lstrIndMass = "2" '--Masivo
                    End If
                    'UPGRADE_NOTE: The 'ePolicy.ValPolicyRep' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Program Files\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm
                    mclsPolicy = New ePolicy.ValPolicyRep
                    insPostPolicy = mclsPolicy.InsPostVil7701(lstrIndMass, lstrPre_def, mobjValues.StringToType(.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnProponum"), eFunctions.Values.eTypeData.etdDouble))
                    If insPostPolicy Then
                        mstrKey = mclsPolicy.sKey
                        lblnPrintReport = True
                    Else
                        lblnPrintReport = False
                    End If
                End With
            Case "VIL7020"
                lblnPrintReport = True
                insPostPolicy = True

            '+ VIL7021: Certificado Nº 24 sobre movimiento anual de APV (por RUT)
            Case "VIL7021"
                With Request
                    'UPGRADE_NOTE: The 'ePolicy.ValPolicyRep' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Program Files\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm
                    mclsPolicy = New ePolicy.ValPolicyRep
                    insPostPolicy = mclsPolicy.insPostVIL7021(.Form.Item("optPT"), .Form.Item("optST"), .Form.Item("tcnYear"), .Form.Item("tctClient"), mobjValues.StringToType(.Form.Item("tcnAnnualcertifnr"), eFunctions.Values.eTypeData.etdDouble), Session("nUsercode"))
                    lblnPrintReport = insPostPolicy
                End With

            '+ CAL848 :		    
            Case "CAL848"
                If Request.QueryString.Item("nZone") <> "2" Then
                    'UPGRADE_NOTE: The 'ePolicy.ValPolicyRep' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Program Files\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm
                    mclsPolicy = New ePolicy.ValPolicyRep
                    insPostPolicy = mclsPolicy.insPostCAL848_K(mobjValues.StringToDate(Request.Form.Item("tcdDateFrom")), mobjValues.StringToDate(Request.Form.Item("tcdDateTO")), mobjValues.StringToType(Request.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(Request.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(Request.Form.Item("cbeOffice"), eFunctions.Values.eTypeData.etdLong, True), mobjValues.StringToType(Request.Form.Item("cbeOfficeAgen"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(Request.Form.Item("cbeAgency"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(Request.Form.Item("valIntermedia"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(Request.Form.Item("cbeStatQuota"), eFunctions.Values.eTypeData.etdLong, True), mobjValues.StringToType(Request.Form.Item("cbeOrigin"), eFunctions.Values.eTypeData.etdLong))
                    Session("sFile_name") = "../../tFiles/" & mclsPolicy.sFile_name
                Else
                    insPostPolicy = True
                End If

            Case "VIL1405"
                'UPGRADE_NOTE: The 'ePolicy.ValPolicyTra' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Program Files\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm
                mclsPolicy = New ePolicy.ValPolicyTra
                insPostPolicy = mclsPolicy.insPostVIL1405(mobjValues.StringToType(Request.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Request.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Request.Form.Item("tcdEffecDate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Request.Form.Item("hddnIdproces"), eFunctions.Values.eTypeData.etdDouble, True))

            Case "VIL1413"
                insPostPolicy = True
                lblnPrintReport = True

            '+ VIL1411 Reporte de ordenes de compra/venta a inversiones
            Case "VIL1411"
                'UPGRADE_NOTE: The 'eReports.Report' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Program Files\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm
                lobjReport = New eReports.Report
                If Request.Form.Item("chkprocess") = "1" Then
                    lsprocess = "1"
                Else
                    lsprocess = "2"
                End If

                With lobjReport
                    .sCodispl = "VIL1411"
                    .ReportFilename = "VIL1411.rpt"
                    .setStorProcParam(1, mobjValues.StringToType(Request.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble))
                    .setStorProcParam(2, mobjValues.StringToType(Request.Form.Item("valproduct"), eFunctions.Values.eTypeData.etdDouble))
                    .setStorProcParam(3, mobjValues.StringToType(Request.Form.Item("tcnpolicy"), eFunctions.Values.eTypeData.etdDouble))
                    .setStorProcParam(4, lsprocess)
                    .setStorProcParam(5, .setdate(Request.Form.Item("tcdfecha")))
                    .setStorProcParam(6, mobjValues.StringToType(Request.Form.Item("opttipo"), eFunctions.Values.eTypeData.etdDouble, True))
                    Response.Write((.Command))
                End With
                lobjReport = Nothing
                insPostPolicy = True

            '+ VIL1412 Reporte de Post-cargos pendientes
            Case "VIL1412"
                'UPGRADE_NOTE: The 'eReports.Report' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Program Files\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm
                lobjReport2 = New eReports.Report

                With lobjReport2
                    .sCodispl = "VIL1412"
                    .ReportFilename = "VIL1412.rpt"

                    .setStorProcParam(1, mobjValues.StringToType(Request.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble))
                    .setStorProcParam(2, mobjValues.StringToType(Request.Form.Item("valproduct"), eFunctions.Values.eTypeData.etdDouble))
                    .setStorProcParam(3, .setdate(Request.Form.Item("tcdfecha")))
                    .setStorProcParam(4, mobjValues.StringToType(Request.Form.Item("cbeType"), eFunctions.Values.eTypeData.etdDouble, True))
                    .setStorProcParam(5, "1")
                    '.setStorProcParam 6, "2"
                    Response.Write((.Command))
                End With
                lobjReport2 = Nothing
                insPostPolicy = True

            '+ CAL503: Libro timbrado de Producción
            Case "CAL503"
                If CStr(Session("BatchEnabled")) <> "1" Then
                    'Set lclsGeneralFunction = new eGeneral.GeneralFunction
                    'mstrKey = lclsGeneralFunction.getsKey(Session("P_SKEY"))				     
                    'Set lclsGeneralFunction = Nothing

                    'UPGRADE_NOTE: The 'ePolicy.ValPolicyRep' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Program Files\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm
                    mclsPolicy = New ePolicy.ValPolicyRep
                    insPostPolicy = mclsPolicy.InsCreTMP_CAL503(mobjValues.StringToType(Session("nCompanyUser"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nInsur_Area"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("tcdDateIni"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Request.Form.Item("tcdDateEnd"), eFunctions.Values.eTypeData.etdDate))

                    '/* se saca parametro antes de mstrKey */	
                    'mobjValues.StringToType(Session("nUsercode"),eFunctions.Values.eTypeData.etdDouble),                 
                    Session("P_SKEY") = mclsPolicy.P_Skey

                    Response.Write("<SCRIPT>alert('skey : " & Session("P_SKEY") & "');</" & "Script>")
                    'If insPostPolicy Then
                    '    insPrintDocuments()
                    'End If
                Else
                    'UPGRADE_NOTE: The 'eSchedule.Batch_param' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Program Files\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm
                    lclsBatch_param = New eSchedule.Batch_Param

                    With lclsBatch_param
                        .nBatch = 132
                        .nUserCode = mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble)
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, mobjValues.StringToType(Session("nCompanyUser"), eFunctions.Values.eTypeData.etdDouble))
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, mobjValues.StringToType(Session("nInsur_Area"), eFunctions.Values.eTypeData.etdDouble))
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, mobjValues.StringToType(Request.Form.Item("tcdDateIni"), eFunctions.Values.eTypeData.etdDate))
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, mobjValues.StringToType(Request.Form.Item("tcdDateEnd"), eFunctions.Values.eTypeData.etdDate))
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, .sKey)
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble))
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, mobjValues.StringToType(Request.Form.Item("optOption"), eFunctions.Values.eTypeData.etdInteger))
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaRes, .sKey)
                        .Save()
                    End With

                    Response.Write("<SCRIPT>alert('Se generó la clave de proceso : " & lclsBatch_param.sKey & "');</" & "Script>")
                    lclsBatch_param = Nothing
                    insPostPolicy = True
                End If

            '+ CAL504: Libro de producción SOAP-AS400
            Case "CAL504"
                If CStr(Session("BatchEnabled")) <> "1" Then
                    'Set lclsGeneralFunction = new eGeneral.GeneralFunction
                    'mstrKey = lclsGeneralFunction.getsKey(Session("P_SKEY"))				     
                    'Set lclsGeneralFunction = Nothing

                    'UPGRADE_NOTE: The 'ePolicy.ValPolicyRep' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Program Files\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm
                    mclsPolicy = New ePolicy.ValPolicyRep
                    insPostPolicy = mclsPolicy.InsCreTMP_CAL504(mobjValues.StringToType(Session("nCompanyUser"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nInsur_Area"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("tcdDateIni"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Request.Form.Item("tcdDateEnd"), eFunctions.Values.eTypeData.etdDate),mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble),mobjValues.StringToType(Request.Form.Item("optOption"), eFunctions.Values.eTypeData.etdDouble))

                    '/* se saca parametro antes de mstrKey */	
                    'mobjValues.StringToType(Session("nUsercode"),eFunctions.Values.eTypeData.etdDouble),                 
                    Session("P_SKEY") = mclsPolicy.P_Skey

                    Response.Write("<SCRIPT>alert('skey : " & Session("P_SKEY") & "');</" & "Script>")
                    'If insPostPolicy Then
                    '    insPrintDocuments()
                    'End If
                Else
                    'UPGRADE_NOTE: The 'eSchedule.Batch_param' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Program Files\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm
                    lclsBatch_param = New eSchedule.Batch_Param

                    With lclsBatch_param
                        .nBatch = 970
                        .nUserCode = mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble)
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, mobjValues.StringToType(Session("nCompanyUser"), eFunctions.Values.eTypeData.etdDouble))
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, mobjValues.StringToType(Session("nInsur_Area"), eFunctions.Values.eTypeData.etdDouble))
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, mobjValues.StringToType(Request.Form.Item("tcdDateIni"), eFunctions.Values.eTypeData.etdDate))
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, mobjValues.StringToType(Request.Form.Item("tcdDateEnd"), eFunctions.Values.eTypeData.etdDate))
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, .sKey)
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble))
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, mobjValues.StringToType(Request.Form.Item("optOption"), eFunctions.Values.eTypeData.etdInteger))
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaRes, .sKey)
                        .Save()
                    End With

                    Response.Write("<SCRIPT>alert('Se generó la clave de proceso : " & lclsBatch_param.sKey & "');</" & "Script>")
                    lclsBatch_param = Nothing
                    insPostPolicy = True
                End If

            Case "CAL01415"
                insPostPolicy = True

            Case "CAL00975"
                insPostPolicy = True

            Case "CAL00976"
                insPostPolicy = True

            '+ VT00065 GAP08 Reporte de Inversiones
            Case "VIL01600"
                'UPGRADE_NOTE: The 'eReports.Report' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Program Files\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm
                lobjReporte = New eReports.Report
                With lobjReporte
                    .sCodispl = "VIL01600"
                    If Request.Form.Item("opttipo") = "1" Then
                        'Resumen 1
                        .ReportFilename = "vil01600_res.rpt"
                        '.ReportFilename = "vil01600_res_b.rpt"
                        .setStorProcParam(1, mobjValues.StringToType(Request.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble))
                        .setStorProcParam(2, mobjValues.StringToType(Request.Form.Item("valproduct"), eFunctions.Values.eTypeData.etdDouble))
                        .setStorProcParam(3, mobjValues.StringToType(Request.Form.Item("tcnpolicy"), eFunctions.Values.eTypeData.etdDouble))
                        .setStorProcParam(4, Request.Form.Item("tcdStarDate"))
                        .setStorProcParam(5, Request.Form.Item("tcdEndDate"))
                        .setStorProcParam(6, mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble))
                        Response.Write((.Command))
                        .Reset()
                        .sCodispl = "VIL01600_res"
                        .ReportFilename = "vil01600_res_b.rpt"
                        .setStorProcParam(1, mobjValues.StringToType(Request.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble))
                        .setStorProcParam(2, mobjValues.StringToType(Request.Form.Item("valproduct"), eFunctions.Values.eTypeData.etdDouble))
                        .setStorProcParam(3, mobjValues.StringToType(Request.Form.Item("tcnpolicy"), eFunctions.Values.eTypeData.etdDouble))
                        .setStorProcParam(4, Request.Form.Item("tcdStarDate"))
                        .setStorProcParam(5, Request.Form.Item("tcdEndDate"))
                        .setStorProcParam(6, mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble))
                        Response.Write((.Command))
                    Else
                        ' Detalle 2
                        .ReportFilename = "vil01600_det.rpt"
                        .setStorProcParam(1, mobjValues.StringToType(Request.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble))
                        .setStorProcParam(2, mobjValues.StringToType(Request.Form.Item("valproduct"), eFunctions.Values.eTypeData.etdDouble))
                        .setStorProcParam(3, mobjValues.StringToType(Request.Form.Item("tcnpolicy"), eFunctions.Values.eTypeData.etdDouble))
                        .setStorProcParam(4, Request.Form.Item("tcdStarDate"))
                        .setStorProcParam(5, Request.Form.Item("tcdEndDate"))
                        .setStorProcParam(6, mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble))
                        Response.Write((.Command))
                    End If
                End With
                lobjReporte = Nothing
                insPostPolicy = True

            '+ VIL8020: Reserva/Rentabilidad del ahorro garantizado
            Case "VIL8020"
                'UPGRADE_NOTE: The 'ePolicy.Policy' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Program Files\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm
                lclsVIL8020 = New ePolicy.Policy
                insPostPolicy = lclsVIL8020.insVIL8020(mobjValues.StringToType(Request.Form.Item("tcdEffecDate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble))
                lclsVIL8020 = Nothing

            '+ VIL08000: Reporte de Ahorros Garantizados			
            Case "VIL08000"
                insPostPolicy = True
                lblnPrintReport = True

            '+ VIL08001: Reporte de Esquemas del Ahorro Garantizado
            Case "VIL08001"
                insPostPolicy = True
                lblnPrintReport = True

            '+ VIL8003: Reporte de cotización del producto Previsor Plus
            Case "VIL8003"
                insPostPolicy = True

            '+ VIL8002: Reporte de cotización del producto nuevo APV
            Case "VIL8002"
                insPostPolicy = True

            '+ VIL8004: Reporte de cotización del producto Planificador
            Case "VIL8004"
                insPostPolicy = True

            '+ VIL08006: Reporte de saldos finales por fondo	
            Case "VIL08006"
                insPostPolicy = True
                lblnPrintReport = True

            '+ VIL8007: Reporte de cartolas mensuales
            Case "VIL8007"
                insPostPolicy = True
                lblnPrintReport = True

            '+ VIL8005: Reporte de Esquema APV
            Case "VIL8005"
                insPostPolicy = True
                lblnPrintReport = True

            '+ VIL08011: Reporte de emision
            Case "VIL8011"
                insPostPolicy = True
                lblnPrintReport = True

            '+ VIL8009: Reservas por Producto de Ahorros Garantizados
            Case "VIL8009"
                insPostPolicy = True
                lblnPrintReport = True

            '+ VIL8012: Reporte de FECU Corredores
            Case "VIL8012"
                insPostPolicy = True
                lblnPrintReport = True

            '+ VIL8030: Reporte de resumen de libro de producción foliado
            Case "VIL8030"
                insPostPolicy = True
                lblnPrintReport = True

            '+ VIL8010: Reporte de FECU Mensual Interno
            Case "VIL8010"
                insPostPolicy = True
                lblnPrintReport = True

            '+ VIL8031: Reporte de resumen de libro de producción
            Case "VIL8031"
                insPostPolicy = True
                lblnPrintReport = True

            '+ HAD033 VIL8032 : Reporte de resumen producción por oficina			
            Case "VIL8032"
                insPostPolicy = True
                lblnPrintReport = True

            '+ HAD034 - Reporte de Resumen de Produccion por cobertura
            Case "VIL8033"
                insPostPolicy = True
                lblnPrintReport = True

            '+ CAL803: Reporte de Cobranza Indiapv por Póliza				
            Case "CAL803"
                insPostPolicy = True
                lblnPrintReport = True

            '+CAL01510: reporte de Endoso
            Case "CAL01510"
                insPostPolicy = True
                lblnPrintReport = True

            '+ CAL08001: Detalle de póliza de vida		
            Case "CAL08001"
                insPostPolicy = True
                lblnPrintReport = True

            '+ CAL979: Actualización automática de capitales crecientes/decrecientes
            Case "CAL979"
                '+ Se verifica si la ejecución se realiza en línea o como un procso batch
                If CStr(Session("BatchEnabled")) = "1" Then
                    'UPGRADE_NOTE: The 'ePolicy.Valpolicyrep' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Program Files\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm
                    mclsPolicy = New ePolicy.ValPolicyRep
                    insPostPolicy = mclsPolicy.insPostCAL979(IIf(mobjValues.StringToType(Request.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble) = 0, eRemoteDB.Constants.intNull, mobjValues.StringToType(Request.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble)), mobjValues.StringToType(Request.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("tcnPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("valModulec"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("tcdEffecdate"), eFunctions.Values.eTypeData.etdDate))
                    lblnPrintReport = False

                    If insPostPolicy Then
                        Response.Write("<SCRIPT> alert('Proceso terminado satisfactoriamente')</" & "Script>")
                    End If
                Else
                    '+ Se almacenan los parámetros del proceso batch
                    'UPGRADE_NOTE: The 'eSchedule.Batch_param' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Program Files\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm
                    lclsBatch_param = New eSchedule.Batch_Param
                    With lclsBatch_param
                        .nBatch = 80357
                        .nUserCode = mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble)
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, .sKey)
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, IIf(mobjValues.StringToType(Request.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble) = 0, eRemoteDB.Constants.intNull, mobjValues.StringToType(Request.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble)))
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, mobjValues.StringToType(Request.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdDouble))
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, mobjValues.StringToType(Request.Form.Item("tcnPolicy"), eFunctions.Values.eTypeData.etdDouble))
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, mobjValues.StringToType(Request.Form.Item("valModulec"), eFunctions.Values.eTypeData.etdDouble))
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, .nUserCode)
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, mobjValues.StringToType(Request.Form.Item("tcdEffecdate"), eFunctions.Values.eTypeData.etdDate))
                        .Save()
                    End With
                    Response.Write("<SCRIPT>alert('Se generó la clave de proceso: " & lclsBatch_param.sKey & "');</" & "Script>")

                    lclsBatch_param = Nothing
                    lblnPrintReport = False
                    insPostPolicy = True
                End If

            '+ CAL01511: Detalle de póliza de vida
            Case "CAL01511"
                'UPGRADE_NOTE: The 'eSchedule.Batch_param' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Program Files\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm
                lclsBatch_param = New eSchedule.Batch_Param
                With lclsBatch_param
                    .nBatch = 163
                    .nUserCode = mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble)
                    .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, .sKey)
                    .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, mobjValues.StringToType(Request.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble))
                    .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, nproduct) 'mobjValues.StringToType(Request.Form("valProduct"),eFunctions.Values.eTypeData.etdDouble)
                    .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, mobjValues.StringToType(Request.Form.Item("tcdBeginDate"), eFunctions.Values.eTypeData.etdDate))
                    .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, mobjValues.StringToType(Request.Form.Item("tcdEndDate"), eFunctions.Values.eTypeData.etdDate))
                    .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble))
                    .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaRes, .sKey)
                    .Save()
                End With
                Response.Write("<SCRIPT>alert('Se generó la clave de proceso: " & lclsBatch_param.sKey & "');</" & "Script>")
                lclsBatch_param = Nothing
                insPostPolicy = True
                lblnPrintReport = False

            '+ CAL8000: Reporte de pólizas saldadas
            Case "CAL8000"
                insPostPolicy = True
                lblnPrintReport = True

            Case "CAL0110"
                insPostPolicy = True
                If CDbl(Request.QueryString.Item("nZone")) = 1 Then
                    With Request.Form
                        '+ Puntual
                        If .Item("optntype") = 1 Then

                            mstrCommand = "&ntype=" & .Item("optntype") & _
                                          "&nTypeReport=" & .Item("valTypeReport") & _
                                          "&nBranch=" & .Item("cbeBranch") & _
                                          "&nProduct=" & .Item("valProduct") & _
                                          "&dIssuedatIni=" & .Item("tcdIssuedatIni") & _
                                          "&dIssuedatEnd=" & .Item("tcdIssuedatEnd") & _
                                          "&nPolicy=" & .Item("tcnPolicy") & _
                                          "&nCertif=" & .Item("tcnCertif") & _
                                          "&sPolitype=" & sPolitype
                        Else
                            '+ Masivo
                            mstrCommand = "&ntype=" & .Item("optntype") & _
                                          "&nTypeReport=" & .Item("valTypeReport") & _
                                          "&nBranch=" & .Item("cbeBranch") & _
                                          "&nProduct=" & .Item("valProduct") & _
                                          "&dIssuedatIni=" & .Item("tcdIssuedatIni") & _
                                          "&dIssuedatEnd=" & .Item("tcdIssuedatEnd") & _
                                          "&nPolicy=" & .Item("tcnPolicy") & _
                                          "&nCertif=" & .Item("tcnCertif") & _
                                          "&sPolitype=" & sPolitype
                        End If
                    End With
                Else
                    '+ Solo para el manejo de Cuadro de Polizas Masivo
                    If Request.QueryString.Item("ntype") = "2" Then


                        Dim lobjGeneralFunction As New eGeneral.GeneralFunction
                        Dim lclsPolicy_hiss As New ePolicy.Policy_hiss
                        Dim lclsPolicy_his As New ePolicy.Policy_his

                        '+ Creacion de Skey
                        mstrKey = lobjGeneralFunction.getsKey(mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble))

                        '+ Creacion de BATCH_PARAM_VALUE y BATCH_JOB
                        lclsBatch_param = New eSchedule.Batch_Param
                        With lclsBatch_param
                            .nBatch = 200
                            .nUserCode = mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble)
                            .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, mstrKey)
                            .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, mobjValues.StringToType(Request.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble))
                            .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, mobjValues.StringToType(Request.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdDouble))
                            .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, mobjValues.StringToType(Request.QueryString.Item("dIssuedatIni"), eFunctions.Values.eTypeData.etdDate))
                            .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, mobjValues.StringToType(Request.QueryString.Item("dIssuedatEnd"), eFunctions.Values.eTypeData.etdDate))
                            .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, Session("nUsercode"))
                            .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, mobjValues.StringToType(Request.QueryString.Item("nPolicy"), eFunctions.Values.eTypeData.etdDouble))
                            .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, mobjValues.StringToType(Request.QueryString.Item("nCertif"), eFunctions.Values.eTypeData.etdDouble))
                            .Save()
                        End With
                        '+ BATCH_JOB una vez creado, se debe ejecutar automaticamente

                        '+ Creacion de polizas consultadas en la transacción para crear los PDFs
                        If lclsPolicy_hiss.FindCal0110_massive(mstrKey, mobjValues.StringToType(Request.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("dIssuedatIni"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Request.QueryString.Item("dIssuedatEnd"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nCertif"), eFunctions.Values.eTypeData.etdDouble)) Then

                            '+ Recorre Todas las polizas en donde no exista el reporte generado
                            For Each lclsPolicy_his In lclsPolicy_hiss
                                Session("sCertype") = lclsPolicy_his.sCertype
                                Session("nBranch") = lclsPolicy_his.nBranch
                                Session("nProduct") = lclsPolicy_his.nProduct
                                Session("nPolicy") = lclsPolicy_his.nPolicy
                                Session("nCertif") = lclsPolicy_his.nCertif
                                Session("dEffecdate") = lclsPolicy_his.dEffecdate
                                If lclsPolicy_his.nCertif = 0 Then
                                    Session("nTransaction") = "1"
                                    Session("sPolitype") = "1"
                                Else
                                    Session("nTransaction") = "2"
                                    Session("sPolitype") = "2"
                                End If

                                Dim mobjDocuments As New eCrystalExport.Export
                                Dim mobjReport As New eReports.Report
                                Dim lcolReport_prod As New eProduct.report_prods
                                Dim lclsReport_prod As New eProduct.report_prod

                                If lcolReport_prod.FindReport_prod_By_Transac(Session("sCertype"), _
                                                                  Session("nBranch"), _
                                                                  Session("nProduct"), _
                                                                  Session("nPolicy"), _
                                                                  Session("nCertif"), _
                                                                  Session("nTransaction"), _
                                                                  eRemoteDB.Constants.intNull, _
                                                                  Session("dEffecdate"), _
                                                                  True) Then


                                    For Each lclsReport_prod In lcolReport_prod
                                        With mobjDocuments

                                            Dim mcolParameters As New Collection
                                            Dim mcolSPParameters As New Collection

                                            .sReportName = lclsReport_prod.sReport
                                            .DBParameters.Add(Session("sCertype"))
                                            .DBParameters.Add(Session("nBranch"))
                                            .DBParameters.Add(Session("nProduct"))
                                            .DBParameters.Add(Session("nPolicy"))
                                            .DBParameters.Add(0)
                                            .DBParameters.Add(mobjReport.setdate(Session("dEffecdate")))
                                            .nMovement = 1
                                            .bMerger = False
                                            .nGenPolicy = 1
                                            .nForzaRep = 1
                                            .nTratypep = lclsReport_prod.nTratypep
                                            .sCertype = Session("sCertype")
                                            .nBranch = Session("nBranch")
                                            .nProduct = Session("nProduct")
                                            .nPolicy = Session("nPolicy")
                                            .nCertif = Session("nCertif")
                                            .sPolitype = Session("sPolitype")

                                            '+ Se llama directamente a la generación de la poliza para evitar el levantamiento de PopUp, por performance
                                            .GenPoliza(0, 1, Session("sInitialsCon"), Session("sAccesswoCon"), , Server.MapPath("/VTIMENET"))

                                            '.Reset()
                                            '.bTimeOut = True
                                        End With
                                    Next
                                End If
                                lcolReport_prod = Nothing
                                lclsReport_prod = Nothing
                                mobjDocuments = Nothing

                                '+ Se registra en nombre del reporte en tabla Temporal
                                lclsPolicy_hiss.updCal0110_massive(mstrKey, lclsPolicy_his.nTransactio)
                            Next
                        End If

                        '+ Se termina el proceso Batch
                        lclsPolicy_hiss.updCal0110_massive_end(mstrKey, Session("nUsercode"))

                        '+ Se cambia el mensaje en caso de que se encuentren o no polizas a pocesar
                        If lclsPolicy_his.nPolicy <> eRemoteDB.Constants.intNull Then
                            Response.Write("<SCRIPT>alert('Se generó la clave de proceso: " & mstrKey & "\n\n El Proceso ha terminado exitosamente\n\n Ver resultados en Procesos Masivos');</" & "Script>")
                            lclsBatch_param = Nothing
                        Else
                            Response.Write("<SCRIPT>alert('El Proceso no realizó ninguna generación de Pólizas porque ya se encuentran generadas');</" & "Script>")
                            lclsBatch_param = Nothing
                        End If
                    End If
                End If
                insPostPolicy = True
                lblnPrintReport = False

            Case "CAL665"
                '+ Se verifica si la ejecución se realiza en línea o como un procso batch
                If CStr(Session("BatchEnabled")) <> "1" Then
                    mclsPolicy = New ePolicy.ValPolicyRep
                    insPostPolicy = mclsPolicy.insPostCAL665(sCodispl,
                                                             mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble),
                                                             mstrKey,
                                                             mstrPath & Request.Form.Item("hdsFileNameProp"),
                                                             mstrPath & Request.Form.Item("hdsFileNameRoles"),
                                                             mstrPath & Request.Form.Item("hdsFileNameBenef"))
                    lblnPrintReport = False

                    If insPostPolicy Then
                        Response.Write("<SCRIPT> alert('Proceso terminado satisfactoriamente')</" & "Script>")
                    End If
                Else
                    '+ Se almacenan los parámetros del proceso batch
                    lclsBatch_param = New eSchedule.Batch_Param
                    mclsPolicy = New ePolicy.ValPolicyRep
                    With lclsBatch_param
                        .nBatch = 80359
                        .nUserCode = mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble)

                        If mclsPolicy.insPostCAL665(sCodispl,
                                                  mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble),
                                                  .sKey,
                                                  mstrPath & Request.Form.Item("hdsFileNameProp"),
                                                  mstrPath & Request.Form.Item("hdsFileNameRoles"),
                                                  mstrPath & Request.Form.Item("hdsFileNameBenef")) Then
                            .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, .sKey)
                            .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, sCodispl)
                            .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, mstrPath & Request.Form.Item("hdsFileNameProp"))
                            .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, mstrPath & Request.Form.Item("hdsFileNameRoles"))
                            .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, mstrPath & Request.Form.Item("hdsFileNameBenef"))
                            .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, .nUserCode)
                            .Add(eSchedule.Batch_job.enmAreaParameters.batchParAreaRes, 11)
                            .Add(eSchedule.Batch_job.enmAreaParameters.batchParAreaRes, .sKey)
                            .Add(eSchedule.Batch_job.enmAreaParameters.batchParAreaRes, Today)
                            .Save()

                            Response.Write("<SCRIPT>alert('Se generó la clave de proceso: " & .sKey & "');</" & "Script>")
                        Else
                            Response.Write("<SCRIPT>alert('El proceso no se ejecuto');</" & "Script>")
                        End If
                    End With
                    lclsBatch_param = Nothing
                    lblnPrintReport = False
                    insPostPolicy = True
                End If

            Case "CAL7933"
                Dim lclsPolicy As New ePolicy.ValPolicyRep
                Dim lclsInterface As New eInterface.ValInterfaceSeq

                insPostPolicy = lclsInterface.CreT_Param_Interface(mstrKey, 1, mobjValues.StringToType(Request.Form("cbeType"), eFunctions.Values.eTypeData.etdLong),
                                                                   mobjValues.StringToType(CStr(Session("nUsercode")), eFunctions.Values.eTypeData.etdLong))

                insPostPolicy = lclsPolicy.InsPostCAL7933(1, Session("sFile"), 2)

                If insPostPolicy Then
                    lclsBatch_param = New eSchedule.Batch_Param

                    With lclsBatch_param
                        .nBatch = 1402
                        .sKey = mstrKey
                        .nSheet = 7933
                        .nUsercode = mobjValues.StringToType(CStr(Session("nUsercode")), eFunctions.Values.eTypeData.etdDouble)
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, 1)
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, 7933)
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, mobjValues.StringToType(CStr(Session("nUsercode")), eFunctions.Values.eTypeData.etdLong, True))
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, mstrKey)
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, lclsPolicy.mstrFile)

                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaRes, mstrKey)
                        .Save()
                    End With

                    Response.Write("<SCRIPT>alert('Se generó la clave de proceso: " & mstrKey & "');</" & "Script>")

                    'UPGRADE_NOTE: Object lclsBatch_param may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
                    lclsBatch_param = Nothing
                End If

            '+ VIL1890: Declaración jurada 1899 - Certificado No 7
            Case "VIL1890"
                Dim lclsCertifseven As New ePolicy.Detail_1890
                Dim lclsCertif As New ePolicy.Detail_1890
                Dim lobjGeneralFunction As New eGeneral.GeneralFunction
                lclsBatch_param = New eSchedule.Batch_Param


                'If Request.QueryString.Item() Then
                '+ Creacion de Skey
                mstrKey = lobjGeneralFunction.getsKey(mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble))

                '+ Creacion de BATCH_PARAM_VALUE y BATCH_JOB
                With lclsBatch_param
                    .nBatch = 1890
                    .nUserCode = mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble)
                    .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, .sKey)
                    .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, mstrKey)
                    .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, mobjValues.StringToType(Request.Form.Item("optProcessType"), eFunctions.Values.eTypeData.etdDouble))
                    .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, mobjValues.StringToType(Request.Form.Item("tcnYear"), eFunctions.Values.eTypeData.etdDouble))
                    .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, mobjValues.StringToType(Request.Form.Item("tcdPrintDate"), eFunctions.Values.eTypeData.etdDate))
                    .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, mobjValues.StringToType(Request.Form.Item("cbeDecType"), eFunctions.Values.eTypeData.etdDouble))
                    .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, mobjValues.StringToType(Request.Form.Item("tcnRectif"), eFunctions.Values.eTypeData.etdDouble))
                    .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, mobjValues.StringToType(Request.Form.Item("valClient"), eFunctions.Values.eTypeData.etdDouble))
                    .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble))
                End With
                '+ BATCH_JOB una vez creado, se debe ejecutar automaticamente
                'If lclsCertifseven.FindCertSeven_massive(mstrKey, mobjValues.StringToType(Request.Form.Item("optProcessType"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("tcnYear"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("tcdPrintDate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Request.Form.Item("tcnRectif"), eFunctions.Values.eTypeData.etdDouble), IIf(mobjValues.StringToType(Request.Form.Item("valClient"), eFunctions.Values.eTypeData.etdDouble) = -32768, String.Empty, mobjValues.StringToType(Request.Form.Item("valClient"), eFunctions.Values.eTypeData.etdDouble))) Then
                If lclsCertifseven.FindCertSeven_massive(mstrKey, Request.Form.Item("optProcessType").ToString, Request.Form.Item("tcnYear").ToString, Request.Form.Item("tcdPrintDate").ToString, Request.Form.Item("tcnRectif").ToString, Request.Form.Item("valClient").ToString) Then

                    '+ Recorrer todos los clientes que existen para generar el certificado por cada uno, obtenemos la lista de clientes desde la clase Detail_1890 y se agrupan por clientes ya que el certificado es por cliente
                    Dim l = lclsCertifseven.List_Detail1890.GroupBy(Function(x) x.sClient).Select(Function(y) y.First).ToList

                    For Each ob As Detail_1890 In l 'lclsCertifseven.mCol
                        sOutputSev = sOutputSev + String.Format("RUT: {0} ", ob.sClient_1)
                        Session("sMassive") = 0
                        Session("nYear") = Request.Form.Item("tcnYear")
                        Session("dCompdate") = Request.Form.Item("tcdPrintDate")
                        Session("nRectif") = IIf(ob.nRectif = -32768, 0, ob.nRectif)
                        Session("sClient") = ob.sClient
                        Session("nId") = ob.nId

                        Dim mobjDocuments As New eCrystalExport.Export
                        Dim mobjReport As New eReports.Report
                        With mobjDocuments

                            Dim mcolParameters As New Collection
                            Dim mcolSPParameters As New Collection

                            .sReportName = "VIL1890.rpt"
                            .sMassive = Session("sMassive")
                            .nYear = Session("nYear")
                            .dCompdate = Session("dCompdate")
                            .nRectif = IIf(IsDBNull(Session("nRectif")), 0, Session("nRectif"))
                            .sClient = Session("sClient")
                            .nId = ob.nId
                            .nMovement = 1
                            .bMerger = False
                            .nGenReportseven = 1
                            .nForzaRep = 1
                            .ReportParameters.Add(Session("sMassive"))
                            .ReportParameters.Add(Session("nYear"))
                            .ReportParameters.Add(Session("nRectif"))
                            .ReportParameters.Add(Session("sClient"))
                            .ReportParameters.Add(Session("dCompdate"))

                            '+ Se llama directamente a la generación del certificado 7 para evitar el levantamiento de PopUp, por performance
                            .GenCertifSeven(0, 1, Session("sInitialsCon"), Session("sAccesswoCon"), , Server.MapPath("/VTIMENET"))
                        End With

                        mobjDocuments = Nothing
                    Next

                    '+ Se cambia el mensaje en caso de que se encuentren o no polizas a pocesar
                    If l.Count <> 0 Then
                        Dim lclsGetsettings As New eRemoteDB.VisualTimeConfig
                        mstrPathReport = lclsGetsettings.LoadSetting("ExportDirectoryReport", "", "Paths").ToString
                        Response.Write("<SCRIPT>alert('Se generaron: " & l.Count & " certificados.\nListado por RUT generados:\n" + sOutputSev + "\nPuede revisar sus certificados generados en: " & mstrPathReport.ToString.Replace("\", "/") & " ');</" & "Script>")
                        lclsBatch_param = Nothing

                    Else
                        Response.Write("< Script > alert('El Proceso no realizó ninguna generación de certificados');</" & "Script>")
                        lclsBatch_param = Nothing
                    End If
                Else
                    Response.Write("< Script > alert('No hay información para el cliente ingresado');</" & "Script>")
                End If

                lblnPrintReport = False

        End Select
        insPostPolicy = True
    End Function

    '% insPrintDocuments : Realiza la ejecución del reporte
    '-------------------------------------------------------------------------------------------
    Private Sub insPrintDocuments()
        Dim lintCountVIL1413 As Object
        Dim nentra As Object
        Dim lstrTtype_move As Object
        Dim lobjDocuments As Object
        Dim lstrDay As Object
        Dim lstrMonth As Object
        Dim lngBranch As Integer
        Dim lngProduct As Integer
        Dim lclsReport_prod As eProduct.report_prod
        Dim lclsReport_prods As eProduct.report_prods
        'UPGRADE_NOTE: The 'eReports.Report' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Program Files\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm
        lobjDocuments = New eReports.Report

        With lobjDocuments
            Select Case sCodispl

                Case "CAL00008"
                    .sCodispl = "CAL00008"
                    .ReportFilename = "CAL00008.RPT"
                    .setStorProcParam(1, tPolicyType)
                    .setStorProcParam(2, Request.Form.Item("tcdIniDate"))
                    .setStorProcParam(3, Request.Form.Item("tcdEndDate"))
                    .setStorProcParam(4, nUserCode)
                    Response.Write((.Command))

                Case "CAL01500"
                    .sCodispl = "CAL01500"
                    .ReportFilename = "CAL01500.RPT"
                    .setStorProcParam(1, Request.Form.Item("tcdIniDate"))
                    .setStorProcParam(2, Request.Form.Item("tcdEndDate"))
                    .setStorProcParam(3, nUserCode)
                    Response.Write((.Command))

                '%CAL00832: Informe de Entregas de Mandatos
                Case "CAL00832"
                    .sCodispl = "CAL00832"
                    .ReportFilename = "CAL00832.RPT"
                    .setStorProcParam(1, Request.Form.Item("tcdIniDate"))
                    .setStorProcParam(2, Request.Form.Item("tcdEndDate"))
                    .setStorProcParam(3, nbranch)
                    .setStorProcParam(4, nproduct)
                    .setStorProcParam(5, tAddressType)
                    .setStorProcParam(6, nBank)
                    .setStorProcParam(7, tWayPay)
                    .setStorProcParam(8, nUserCode)
                    Response.Write((.Command))

                Case "CAL001"
                    .sCodispl = "CAL001"
                    .ReportFilename = "CAL001_B.RPT"
                    .setStorProcParam(1, lstrKey683)
                    .setStorProcParam(2, mobjValues.StringToType(Request.Form.Item("tcnini"), eFunctions.Values.eTypeData.etdDouble))
                    .setStorProcParam(3, mobjValues.StringToType(Request.Form.Item("tcnend"), eFunctions.Values.eTypeData.etdDouble))
                    Response.Write((.Command))

                Case "CAL011"
                    .sCodispl = "CAL011"
                    .ReportFilename = "CAL011.RPT"
                    .setStorProcParam(1, .setdate(Request.Form.Item("tcdInitial")))
                    .setStorProcParam(2, .setdate(Request.Form.Item("tcdFinish")))
                    Response.Write((.Command))

                Case "CAL014"
                    .sCodispl = "CAL014"
                    .ReportFilename = "CAL014.rpt"
                    .setStorProcParam(1, "2")
                    .setStorProcParam(2, mobjValues.StringToType(Request.Form.Item("cbeBranch1"), eFunctions.Values.eTypeData.etdDouble))
                    .setStorProcParam(3, mobjValues.StringToType(Request.Form.Item("valProduct1"), eFunctions.Values.eTypeData.etdDouble))
                    .setStorProcParam(4, mobjValues.StringToType(Request.Form.Item("tcnPolicy"), eFunctions.Values.eTypeData.etdDouble))
                    Response.Write((.Command))

                Case "CAL010"
                    If mobjValues.StringToType(Request.Form.Item("cbenTypeReport"), eFunctions.Values.eTypeData.etdDouble) = 2 Then
                        .sCodispl = "CAL010"
                        .ReportFilename = "CAL010_LETTER_1.rpt"
                        .setStorProcParam(1, lstrKey683)
                        .setStorProcParam(2, mobjValues.StringToType(Request.Form.Item("cbenTypeLetter"), eFunctions.Values.eTypeData.etdDouble))
                        .setStorProcParam(3, .setdate(Request.Form.Item("tcdEffecdate")))
                        .setParamField(1, "Snumletter", mobjValues.StringToType(Request.Form.Item("tcnLetter"), eFunctions.Values.eTypeData.etdDouble))
                        Response.Write((.Command))
                    ElseIf mobjValues.StringToType(Request.Form.Item("cbenTypeReport"), eFunctions.Values.eTypeData.etdDouble) = 3 Then
                        '                        .sCodispl = "CAL010"
                        '                        .ReportFilename = "vil901.RPT"
                        '                        .setStorProcParam(1, lstrKey683)
                        '                        .setStorProcParam(2, .setdate(Request.Form.Item("tcdEffecdate")))
                        '                        Response.Write((.Command))
                        lclsReport_prods = New eProduct.report_prods
                        If lclsReport_prods.FindReport_prod_By_Transac("2", _
                                                                         mobjValues.StringToType(Request.Form("cbeBranch1"), eFunctions.Values.eTypeData.etdDouble), _
                                                                         mobjValues.StringToType(Request.Form("valProduct1"), eFunctions.Values.eTypeData.etdDouble), _
                                                                         mobjValues.StringToType(Request.Form.Item("tcnPolicy"), eFunctions.Values.eTypeData.etdDouble, True), _
                                                                         mobjValues.StringToType(Request.Form.Item("tcnCertif"), eFunctions.Values.eTypeData.etdDouble), _
                                                                         1, _
                                                                         mobjValues.StringToType(Request.Form.Item("cbenTypeReport"), eFunctions.Values.eTypeData.etdDouble), _
                                                                         Today, True) Then

                            For Each lclsReport_prod In lclsReport_prods
                                .sCodispl = Trim(lclsReport_prod.sCodCodispl)
                                .ReportFilename = lclsReport_prod.sReport
                                .setStorProcParam(1, "2")
                                .setStorProcParam(2, mobjValues.StringToType(Request.Form("cbeBranch1"), eFunctions.Values.eTypeData.etdDouble))
                                .setStorProcParam(3, mobjValues.StringToType(Request.Form("valProduct1"), eFunctions.Values.eTypeData.etdDouble))
                                .setStorProcParam(4, mobjValues.StringToType(Request.Form.Item("tcnPolicy"), eFunctions.Values.eTypeData.etdDouble, True))
                                .setStorProcParam(5, mobjValues.StringToType(Request.Form.Item("tcnCertif"), eFunctions.Values.eTypeData.etdDouble))
                                .setStorProcParam(8, .setdate(Request.Form("tcdEffecdateRpt")))
                                Response.Write((.Command))
                                .Reset()
                            Next
                        End If
                    ElseIf mobjValues.StringToType(Request.Form.Item("cbenTypeReport"), eFunctions.Values.eTypeData.etdDouble) = 1 Then
                        lclsReport_prod = New eProduct.report_prod
                        Call lclsReport_prod.Find(mobjValues.StringToType(Request.Form("cbeBranch1"), eFunctions.Values.eTypeData.etdDouble), _
                                                  mobjValues.StringToType(Request.Form("valProduct1"), eFunctions.Values.eTypeData.etdDouble), _
                                                  Today, _
                                                  Request.QueryString("sCodispl"), _
                                                  1)

                        If Request.Form("tcnini") = Request.Form("tcnend") Then
                            lclsReport_prods = New eProduct.report_prods
                            If lclsReport_prods.FindReport_prod_By_Transac("2", _
                                                                             mobjValues.StringToType(Request.Form("cbeBranch1"), eFunctions.Values.eTypeData.etdDouble), _
                                                                             mobjValues.StringToType(Request.Form("valProduct1"), eFunctions.Values.eTypeData.etdDouble), _
                                                                             mobjValues.StringToType(Request.Form("tcnini"), eFunctions.Values.eTypeData.etdDouble), _
                                                                             0, _
                                                                             1, _
                                                                             mobjValues.StringToType(Request.Form.Item("cbenTypeReport"), eFunctions.Values.eTypeData.etdDouble), _
                                                                             Today, True) Then

                                For Each lclsReport_prod In lclsReport_prods
                                    .sCodispl = Trim(lclsReport_prod.sCodCodispl)
                                    .ReportFilename = lclsReport_prod.sReport
                                    .setStorProcParam(1, "2")
                                    .setStorProcParam(2, mobjValues.StringToType(Request.Form("cbeBranch1"), eFunctions.Values.eTypeData.etdDouble))
                                    .setStorProcParam(3, mobjValues.StringToType(Request.Form("valProduct1"), eFunctions.Values.eTypeData.etdDouble))
                                    .setStorProcParam(4, mobjValues.StringToType(Request.Form("tcnini"), eFunctions.Values.eTypeData.etdDouble))
                                    .setStorProcParam(5, 0)
                                    .setStorProcParam(6, .setdate(Request.Form("tcdEffecdateRpt")))
                                    .nReport = 2
                                    .Merge = True
                                    .MergeCertype = "2"
                                    .MergeBranch = mobjValues.StringToType(Request.Form("cbeBranch1"), eFunctions.Values.eTypeData.etdDouble)
                                    .MergeProduct = mobjValues.StringToType(Request.Form("valProduct1"), eFunctions.Values.eTypeData.etdDouble)
                                    .MergePolicy = mobjValues.StringToType(Request.Form("tcnini"), eFunctions.Values.eTypeData.etdDouble)
                                    .MergeCertif = 0
                                    Response.Write((.Command))
                                    .Reset()
                                Next
                            End If
                        Else
                            Dim lclsPolicy As ePolicy.Policy
                            lclsPolicy = New ePolicy.Policy
                            If lclsPolicy.InsProcessPDF(Request.QueryString("sCodispl"), _
                                                        "2", _
                                                        mobjValues.StringToType(Request.Form("cbeBranch1"), eFunctions.Values.eTypeData.etdDouble), _
                                                        mobjValues.StringToType(Request.Form("valProduct1"), eFunctions.Values.eTypeData.etdDouble), _
                                                        mobjValues.StringToType(Request.Form("tcnini"), eFunctions.Values.eTypeData.etdDouble), _
                                                        mobjValues.StringToType(Request.Form("tcnend"), eFunctions.Values.eTypeData.etdDouble), _
                                                        Server.MapPath("/VTIMENET") & "\reports\" & "BBVA_DESG_HPT_P1.rpt", _
                                                        lclsReport_prod.sReport, _
                                                        Session("sInitialsCon"), _
                                                        Session("sAccesswoCon")) Then
                                Response.Write("<SCRIPT>ShowPopUp(""/VTimeNet/Common/Reports/PDFReportShow.aspx?sPDFname=" & lclsPolicy.sPDFName & "&sPDFFullPath=" & Server.UrlEncode(lclsPolicy.sPDFFullPath) & """, ""PDFReportShow"",780,530,'yes','yes',20,20,'no','no');</" & "SCRIPT>")
                            End If
                        End If
                    ElseIf mobjValues.StringToType(Request.Form.Item("cbenTypeReport"), eFunctions.Values.eTypeData.etdDouble) = 4 Then

                        .sCodispl = "CAL010"
                        .ReportFilename = "CAL010_E.rpt"
                        .setStorProcParam(1, "2")
                        .setStorProcParam(2, mobjValues.StringToType(Request.Form("cbeBranch1"), eFunctions.Values.eTypeData.etdDouble))
                        .setStorProcParam(3, mobjValues.StringToType(Request.Form("valProduct1"), eFunctions.Values.eTypeData.etdLong))
                        .setStorProcParam(4, mobjValues.StringToType(Request.Form.Item("tcnPolicy"), eFunctions.Values.eTypeData.etdDouble))
                        .setStorProcParam(5, .setdate(Request.Form("tcdEffecEnd")))
                        .setStorProcParam(6, Request.Form("txtComent"))
                        .setStorProcParam(7, Request.Form("tcnLetter"))
                        .setStorProcParam(8, "")
                        .setStorProcParam(9, mobjValues.StringToType(Request.Form("ValNote"), eFunctions.Values.eTypeData.etdDouble))
                        Response.Write(.Command)
                    End If
                Case "CAL970"
                    .sCodispl = "CAL970"
                    .ReportFilename = "CAL970.RPT"
                    .setStorProcParam(1, mobjValues.StringToType(Request.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble))
                    .setStorProcParam(2, mobjValues.StringToType(Request.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdDouble))
                    .setStorProcParam(3, mobjValues.StringToType(Request.Form.Item("tcnPolicy"), eFunctions.Values.eTypeData.etdDouble))
                    .setStorProcParam(4, mobjValues.StringToType(Request.Form.Item("tcdEffecdate"), eFunctions.Values.eTypeData.etdDate))
                    Response.Write((.Command))

                '+ CAL002: Impresión de cuponeras
                Case "CAL002"
                    .sCodispl = "CAL002"
                    .ReportFilename = "CAL002.rpt"
                    .setParamField(1, "nInsur_area", Session("nInsur_area"))
                    .setStorProcParam(1, mobjValues.StringToType(Request.Form.Item("valOffice"), eFunctions.Values.eTypeData.etdDouble))
                    .setStorProcParam(2, mobjValues.StringToType(Request.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble))
                    .setStorProcParam(3, mobjValues.StringToType(Request.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdDouble))
                    .setStorProcParam(4, mobjValues.StringToType(Request.Form.Item("tcnPolicy"), eFunctions.Values.eTypeData.etdDouble))
                    .setStorProcParam(5, mobjValues.StringToType(Request.Form.Item("tcnRec_Beg"), eFunctions.Values.eTypeData.etdDouble))
                    .setStorProcParam(6, mobjValues.StringToType(Request.Form.Item("tcnRec_End"), eFunctions.Values.eTypeData.etdDouble))
                    .setStorProcParam(7, mobjValues.StringToType(Request.Form.Item("tcnCon_Beg"), eFunctions.Values.eTypeData.etdDouble))
                    .setStorProcParam(8, mobjValues.StringToType(Request.Form.Item("tcnCon_End"), eFunctions.Values.eTypeData.etdDouble))
                    If mobjValues.StringToType(Request.Form.Item("tcdStarDate"), eFunctions.Values.eTypeData.etdDate) <> eRemoteDB.Constants.dtmNull Then
                        .setStorProcParam(9, .setdate(Request.Form.Item("tcdStarDate")))
                    Else
                        .setStorProcParam(9, vbNullString)
                    End If

                    If mobjValues.StringToType(Request.Form.Item("tcdEndDate"), eFunctions.Values.eTypeData.etdDate) <> eRemoteDB.Constants.dtmNull Then
                        .setStorProcParam(10, .setdate(Request.Form.Item("tcdEndDate")))
                    Else
                        .setStorProcParam(10, vbNullString)
                    End If
                    Response.Write((.Command))

                '+ CAL006: Reservas de Primas
                Case "CAL006"
                    '+ Ramo Vida
                    If Request.Form.Item("cbeInsurArea") = "2" Then
                        .sCodispl = "CAL006"
                        .ReportFilename = "RPT_CAL006_1.rpt"
                        .setStorProcParam(1, mobjValues.StringToType(Request.Form.Item("nOptAct"), eFunctions.Values.eTypeData.etdDouble))
                        .setStorProcParam(2, .setdate(Request.Form.Item("tcdEffecdate")))
                        .setStorProcParam(3, mstrKey)
                        Response.Write((.Command))
                    ElseIf Request.Form.Item("cbeInsurArea") = "1" Then
                        If Request.Form.Item("sOptOutput") = "1" Then
                            '+ Ramo Generales
                            '+ Reservas de Primas Detalle
                            .sCodispl = "CAL006"
                            .ReportFilename = "RPT_CAL006_1.rpt"
                            .setStorProcParam(1, mobjValues.StringToType(Request.Form.Item("nOptAct"), eFunctions.Values.eTypeData.etdDouble))
                            .setStorProcParam(2, .setdate(Request.Form.Item("tcdEffecdate")))
                            .setStorProcParam(3, mstrKey)
                            Response.Write((.Command))
                        ElseIf Request.Form.Item("sOptOutput") = "2" Then
                            '+ Reservas de Primas Resumido
                            .sCodispl = "CAL006"
                            .ReportFilename = "RPT_CAL006_1.rpt"
                            .setStorProcParam(1, mobjValues.StringToType(Request.Form.Item("nOptAct"), eFunctions.Values.eTypeData.etdDouble))
                            .setStorProcParam(2, .setdate(Request.Form.Item("tcdEffecdate")))
                            .setStorProcParam(3, mstrKey)
                            Response.Write((.Command))
                        Else
                            .sCodispl = "CAL006"
                            .ReportFilename = "RPT_CAL006_1.rpt"
                            .setStorProcParam(1, mobjValues.StringToType(Request.Form.Item("nOptAct"), eFunctions.Values.eTypeData.etdDouble))
                            .setStorProcParam(2, .setdate(Request.Form.Item("tcdEffecdate")))
                            .setStorProcParam(3, mstrKey)
                            Response.Write((.Command))
                            .Reset()
                            mblnTimeOut = True
                            .sCodispl = "CAL006"
                            .ReportFilename = "RPT_CAL006_1.rpt"
                            .setStorProcParam(1, mobjValues.StringToType(Request.Form.Item("nOptAct"), eFunctions.Values.eTypeData.etdDouble))
                            .setStorProcParam(2, .setdate(Request.Form.Item("tcdEffecdate")))
                            .setStorProcParam(3, mstrKey)
                            .bTimeOut = mblnTimeOut
                            Response.Write((.Command))
                        End If
                    End If

                '+ CA683: Exclusión de Asegurados
                Case "CAL683"
                    .sCodispl = "CAL683"
                    .ReportFilename = "CAL683.rpt"
                    .setStorProcParam(1, lstrKey683)
                    .setStorProcParam(2, mobjValues.StringToType(Request.Form.Item("optType"), eFunctions.Values.eTypeData.etdDouble))
                    Response.Write((.Command))

                '+ VAL708: Proceso de calculo de intereses por prestamo
                Case "VAL708"
                    .sCodispl = "VAL708"
                    .ReportFilename = "VAL708.rpt"
                    .setStorProcParam(1, mobjValues.StringToType(Request.Form.Item("tcnMonth"), eFunctions.Values.eTypeData.etdDouble, True))
                    .setStorProcParam(2, mobjValues.StringToType(Request.Form.Item("tcnYear"), eFunctions.Values.eTypeData.etdDouble, True))
                    .setStorProcParam(3, mobjValues.StringToType(Request.Form.Item("tcnUsercode"), eFunctions.Values.eTypeData.etdDouble, True))
                    .setStorProcParam(4, Request.Form.Item("optType"))
                    Response.Write((.Command))

                '+ CAL908
                Case "CAL908"
                    .sCodispl = "CAL908"
                    .ReportFilename = "CAL908.rpt"
                    .setStorProcParam(1, Request.Form.Item("valClient"))
                    .setStorProcParam(2, mobjValues.StringToType(Request.Form.Item("tcnPolicy"), eFunctions.Values.eTypeData.etdDouble, True))
                    .setStorProcParam(3, mstrKey)
                    Response.Write((.Command))

                '+ CAL782: Pólizas pendientes de impresión/cuponera        
                Case "CAL782"
                    .sCodispl = "CAL782"
                    .ReportFilename = "CAL782.rpt"
                    .setStorProcParam(1, .setdate(Request.Form.Item("tcdStart")))
                    .setStorProcParam(2, .setdate(Request.Form.Item("tcdEnd")))
                    Response.Write((.Command))

                '+ VIL701: Listado de pendientes por exigencias médicas
                Case "VIL701"
                    .sCodispl = "VIL701"
                    .ReportFilename = "VIL701.rpt"
                    .setStorProcParam(1, Request.Form.Item("optType"))
                    .setStorProcParam(2, mobjValues.StringToType(Request.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble, True))
                    .setStorProcParam(3, mobjValues.StringToType(Request.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdDouble, True))
                    .setStorProcParam(4, mobjValues.StringToType(Request.Form.Item("cbeOffice"), eFunctions.Values.eTypeData.etdDouble, True))
                    .setStorProcParam(5, mobjValues.StringToType(Request.Form.Item("tcnPolicy"), eFunctions.Values.eTypeData.etdDouble, True))
                    .setStorProcParam(6, mobjValues.StringToType(Request.Form.Item("tcnCertif"), eFunctions.Values.eTypeData.etdDouble, True))
                    .setStorProcParam(7, .setdate(Request.Form.Item("tcdEffecdatestart")))
                    .setStorProcParam(8, .setdate(Request.Form.Item("tcdEffecdateend")))
                    .setStorProcParam(9, mobjValues.StringToType(Request.Form.Item("cbnStatus_val"), eFunctions.Values.eTypeData.etdDouble, True))
                    Response.Write((.Command))

                '+ VIL7003: Listado de movimientos de unidades
                Case "VIL7003"
                    .sCodispl = "VIL7003"
                    .ReportFilename = "VIL7003.rpt"
                    .setStorProcParam(1, .setdate(Request.Form.Item("tcdFromDate")))
                    .setStorProcParam(2, .setdate(Request.Form.Item("tcdToDate")))
                    Response.Write((.Command))

                '+ VIL7008: Listado de traspasos
                Case "VIL7008"
                    .sCodispl = "VIL7008"
                    .ReportFilename = "VIL7008.rpt"
                    .setStorProcParam(1, mobjValues.StringToType(Request.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble))
                    .setStorProcParam(2, mobjValues.StringToType(Request.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdDouble))
                    .setStorProcParam(3, mobjValues.StringToType(Request.Form.Item("tcnPolicy"), eFunctions.Values.eTypeData.etdDouble))
                    .setStorProcParam(4, mobjValues.StringToType(Request.Form.Item("tcnCertif"), eFunctions.Values.eTypeData.etdDouble))
                    .setStorProcParam(5, mobjValues.StringToType(Request.Form.Item("valCompany"), eFunctions.Values.eTypeData.etdDouble))
                    .setStorProcParam(6, Request.Form.Item("tcdEffecdatefrom"))
                    .setStorProcParam(7, Request.Form.Item("tcdEffecdateto"))
                    Response.Write((.Command))

                '+ CAL712: Polizas con coberturas de servicios de terceros 
                Case "CAL712"
                    .ReportFilename = "CAL712.rpt"
                    .sCodispl = "CAL712"
                    .setStorProcParam(1, Session("nInsur_area"))
                    .setStorProcParam(2, .setdate(Request.Form.Item("tcdEffecdate")))
                    .setStorProcParam(3, mobjValues.StringToType(Request.Form.Item("valCoverGen"), eFunctions.Values.eTypeData.etdDouble))
                    Response.Write((.Command))

                '+ CAL671: Reporte de Propuestas/Cotizaciones
                Case "CAL671"
                    .ReportFilename = "CAL671.rpt"
                    .sCodispl = "CAL671"
                    .setStorProcParam(1, mclsPolicy.sKey)
                    Response.Write((.Command))

                '+ VIL702: Listado de excluídos por asegurabilidad
                Case "VIL702"
                    .sCodispl = "VIL702"
                    .ReportFilename = "VIL702.rpt"
                    .setStorProcParam(1, lstrKey702)
                    Response.Write((.Command))

                '+CAL01501: Informe de Gestión de Operaciones
                Case "CAL01501"
                    .sCodispl = "CAL01501"
                    .ReportFilename = "CAL01501.RPT"
                    .setStorProcParam(1, mobjValues.StringToType(Request.Form.Item("cbeInsur_Area"), eFunctions.Values.eTypeData.etdLong))
                    .setStorProcParam(2, mobjValues.StringToType(Request.Form.Item("optCertype"), eFunctions.Values.eTypeData.etdLong))
                    .setStorProcParam(3, mobjValues.StringToType(Request.Form.Item("cbeAgency"), eFunctions.Values.eTypeData.etdLong))
                    .setStorProcParam(4, IIf(mobjValues.StringToType(Request.Form.Item("valIntermedia"), eFunctions.Values.eTypeData.etdLong) < 0, 0, mobjValues.StringToType(Request.Form.Item("valIntermedia"), eFunctions.Values.eTypeData.etdLong)))
                    .setStorProcParam(5, nbranch)
                    .setStorProcParam(6, nproduct)
                    .setStorProcParam(7, mobjValues.StringToType(Request.Form.Item("cbeStatQuota"), eFunctions.Values.eTypeData.etdLong))
                    .setStorProcParam(8, mobjValues.StringToType(Request.Form.Item("cbeWaitCode"), eFunctions.Values.eTypeData.etdLong))
                    .setStorProcParam(9, Request.Form.Item("tcdDateFrom"))
                    .setStorProcParam(10, Request.Form.Item("tcdDateTo"))
                    .setStorProcParam(11, nUserCode)
                    Response.Write((.Command))

                '+CAL01502: Informe de Polizas emitidas								
                Case "CAL01502"
                    .sCodispl = "CAL01502"
                    .ReportFilename = "cal01502.rpt"
                    .setStorProcParam(1, mobjValues.StringToType(Request.Form.Item("cbeBranch1"), eFunctions.Values.eTypeData.etdLong))

                    If mobjValues.StringToType(Request.Form.Item("valProduct1"), eFunctions.Values.eTypeData.etdLong) < 0 Then
                        .setStorProcParam(2, 0)
                    Else
                        .setStorProcParam(2, mobjValues.StringToType(Request.Form.Item("valProduct1"), eFunctions.Values.eTypeData.etdLong))
                    End If
                    .setStorProcParam(3, nPolicy)
                    .setStorProcParam(4, nCertif)
                    .setStorProcParam(5, nOffice) 'mobjValues.StringToType(Request.Form("cbeOffice"),eFunctions.Values.eTypeData.etdLong, True)
                    .setStorProcParam(6, nOfficeAgen) ' mobjValues.StringToType(Request.Form("cbeOfficeAgen"),eFunctions.Values.eTypeData.etdDouble, True)
                    .setStorProcParam(7, nAgen) ' mobjValues.StringToType(Request.Form("cbeAgency"),eFunctions.Values.eTypeData.etdDouble, True)
                    .setStorProcParam(8, Request.Form.Item("dtcClientCO"))
                    .setStorProcParam(9, Request.Form.Item("dtcClientAS"))
                    .setStorProcParam(10, Request.Form.Item("tcdChangdat"))
                    .setStorProcParam(11, Request.Form.Item("tcdPrintDate"))
                    .setStorProcParam(12, Request.Form.Item("tcdIniDate"))
                    .setStorProcParam(13, Request.Form.Item("tcdEndDate"))
                    .setStorProcParam(14, tPolicyType) ' mobjValues.StringToType(Request.Form("cbePolicyType"),eFunctions.Values.eTypeData.etdLong, True)
                    .setStorProcParam(15, nPolicyIni) ' mobjValues.StringToType(Request.Form("tcnPolicyIni"),eFunctions.Values.eTypeData.etdDouble, True)
                    .setStorProcParam(16, nPolicyFin) ' mobjValues.StringToType(Request.Form("tcnPolicyFin"),eFunctions.Values.eTypeData.etdDouble, True)
                    .setStorProcParam(17, nTypeAmend) ' mobjValues.StringToType(Request.Form("valTypeAmend"),eFunctions.Values.eTypeData.etdDouble, True)
                    .setStorProcParam(18, nUserCode)
                    Response.Write((.Command))

                '+CAL01503: Reporte de Carta de Polizas				
                Case "CAL01503"
                    .sCodispl = "CAL01503"
                    .ReportFilename = "CAL01503.RPT"
                    .setStorProcParam(1, nbranch)
                    .setStorProcParam(2, nproduct)
                    .setStorProcParam(3, nOfficeAgen)
                    .setStorProcParam(4, nAgen)
                    .setStorProcParam(5, Request.Form.Item("tcdIniDate"))
                    .setStorProcParam(6, Request.Form.Item("tcdEndDate"))
                    .setStorProcParam(7, nUserCode)
                    Response.Write((.Command))

                '+CAL01504: Reporte de Cotización Más Salud								
                Case "CAL01504"
                    .sCodispl = "CAL01504"
                    .ReportFilename = "CAL01504.RPT"
                    .setStorProcParam(1, nPolicy)
                    .setStorProcParam(2, nbranch)
                    .setStorProcParam(3, nproduct)
                    Response.Write((.Command))

                Case "CAL01512"
                    .sCodispl = "CAL01512" 'sCodispl
                    lclsReport_prod = New eProduct.report_prod
                    Call lclsReport_prod.Find(mobjValues.StringToType(Request.Form("cbeBranch1"), eFunctions.Values.eTypeData.etdDouble), _
                        mobjValues.StringToType(Request.Form("valProduct1"), eFunctions.Values.eTypeData.etdDouble), _
                      mobjValues.StringToType(Request.Form("tcdCopydate"), eFunctions.Values.eTypeData.etdDate), _
                      Request.QueryString("sCodispl"), _
                         1)
                    If lclsReport_prod.sReport = "CAL01506.RPT" Or _
                       lclsReport_prod.sReport = "CAL01507.RPT" Or _
                       lclsReport_prod.sReport = "CAL01507_619.RPT" Or _
                       lclsReport_prod.sReport = "CAL01507_630.RPT" Or _
                       lclsReport_prod.sReport = "CAL01507_637.RPT" Or _
                                   lclsReport_prod.sReport = "CAL01507_BIS.RPT" Then
                        .ReportFilename = lclsReport_prod.sReport
                        .setstorprocparam(1, mobjValues.StringToType(Request.Form("cbeBranch1"), eFunctions.Values.eTypeData.etdDouble))
                        .setstorprocparam(2, mobjValues.StringToType(Request.Form("valProduct1"), eFunctions.Values.eTypeData.etdDouble))
                        .setStorProcParam(3, mobjValues.StringToType(Request.Form("tcnPolicy"), eFunctions.Values.eTypeData.etdDouble))
                        .setstorprocparam(4, mobjValues.StringToType(Request.Form("tcnCertif"), eFunctions.Values.eTypeData.etdDouble))
                        .setStorProcParam(5, 2)
                        .setStorProcParam(6, nUserCode)
                        .setstorprocparam(7, .setdate(Request.Form("tcdPrintDate")))
                        .setStorProcParam(8, sCodispl)
                    ElseIf lclsreport_prod.sReport.toupper() = "CAL001_HOME.RPT" Or lclsreport_prod.sReport.toupper() = "CAL001_FIRE.RPT" Then
                        .ReportFilename = lclsreport_prod.sReport
                        .setstorprocparam(1, "2")
                        .setstorprocparam(2, mobjValues.StringToType(Request.Form("cbeBranch1"), eFunctions.Values.eTypeData.etdDouble)) '2
                        .setstorprocparam(3, mobjValues.StringToType(Request.Form("valProduct1"), eFunctions.Values.eTypeData.etdDouble)) '"801"
                        .setStorProcParam(4, mobjValues.StringToType(Request.Form("tcnPolicy"), eFunctions.Values.eTypeData.etdDouble))  '"291"
                        .setstorprocparam(5, mobjValues.StringToType(Request.Form("tcnCertif"), eFunctions.Values.eTypeData.etdDouble))
                        .setstorprocparam(6, .setdate(Request.Form("tcdCopydate"))) '"20090101"
                        .setstorprocparam(7, 1)
                        .setstorprocparam(8, "1") ' strquery
                        .setstorprocparam(9, 1) 'transac
                        .setstorprocparam(10, 0) 'receipt
                        .Merge = False
                    Else
                        .ReportFilename = "XXx"
                        .setstorprocparam(1, "2")
                        .setstorprocparam(2, mobjValues.StringToType(Request.Form("cbeBranch1"), eFunctions.Values.eTypeData.etdDouble)) '2
                        .setstorprocparam(3, mobjValues.StringToType(Request.Form("valProduct1"), eFunctions.Values.eTypeData.etdDouble)) '"801"
                        .setStorProcParam(4, mobjValues.StringToType(Request.Form("tcnPolicy"), eFunctions.Values.eTypeData.etdDouble))  '"291"
                        .setstorprocparam(5, mobjValues.StringToType(Request.Form("tcnCertif"), eFunctions.Values.eTypeData.etdDouble))
                        .setstorprocparam(6, .setdate(Request.Form("tcdCopydate"))) '"20090101"
                        .nReport = 2
                        .Merge = True
                        .MergeCertype = "2"
                        .MergeBranch = mobjValues.StringToType(Request.Form("cbeBranch1"), eFunctions.Values.eTypeData.etdDouble) '"2"
                        .MergeProduct = mobjValues.StringToType(Request.Form("valProduct1"), eFunctions.Values.eTypeData.etdDouble) '"801"
                        .MergePolicy = mobjValues.StringToType(Request.Form("tcnPolicy"), eFunctions.Values.eTypeData.etdDouble)  '"291"
                        .MergeCertif = mobjValues.StringToType(Request.Form("tcnCertif"), eFunctions.Values.eTypeData.etdDouble)  '"291"
                    End If
                    lclsReport_prod = Nothing
                    Response.Write(.Command)

                '+CAL01505: Cuadro de Póliza Mas Salud
                '+CAL01506: Cuadro de Póliza Protector
                '+CAL01507: Cuadro de Póliza planificador
                '+CAL01508: Cuadro de Póliza Nuevo APV
                '+CAL01509: Cuadro de Póliza Previsor
                Case "CAL01505", "CAL01506", "CAL01507", "CAL01508", "CAL01509", "CAL01512"
                    .sCodispl = sCodispl
                    If CDbl(Request.Form.Item("optEje")) = 1 Then
                        '***********parametros puntuales	
                        lngBranch = IIf(mobjValues.StringToType(Request.Form.Item("cbeBranch1"), eFunctions.Values.eTypeData.etdLong, True) < 0, 0, mobjValues.StringToType(Request.Form.Item("cbeBranch1"), eFunctions.Values.eTypeData.etdLong, True)) 'ramo 
                        lngProduct = IIf(mobjValues.StringToType(Request.Form.Item("valProduct1"), eFunctions.Values.eTypeData.etdLong, True) < 0, 0, mobjValues.StringToType(Request.Form.Item("valProduct1"), eFunctions.Values.eTypeData.etdLong, True)) 'producto
                    Else
                        lngBranch = IIf(mobjValues.StringToType(Request.Form.Item("cbeBranch2"), eFunctions.Values.eTypeData.etdLong, True) < 0, 0, mobjValues.StringToType(Request.Form.Item("cbeBranch2"), eFunctions.Values.eTypeData.etdLong, True)) 'ramo 
                        lngProduct = IIf(mobjValues.StringToType(Request.Form.Item("valProduct2"), eFunctions.Values.eTypeData.etdLong, True) < 0, 0, mobjValues.StringToType(Request.Form.Item("valProduct2"), eFunctions.Values.eTypeData.etdLong, True)) 'producto
                    End If

                    Call lclsProduct.Find(mobjValues.StringToType(lngBranch, 2), mobjValues.StringToType(lngProduct, 2), Today)

                    If lclsProduct.nprodclas <> 4 And lclsProduct.nprodclas <> 7 And sCodispl <> "CAL01507" And sCodispl <> "CAL01509" And sCodispl = "CAL01512" Then
                        .ReportFilename = "cal01506.rpt" 'reporte de cuadros de polizas de productos tradicionales
                    ElseIf sCodispl = "CAL01507" Or sCodispl = "CAL01509" Or sCodispl = "CAL01512" Then
                        .ReportFilename = "cal01507.rpt" 'reporte de cuadros de polizas de productos tradicionales
                    Else
                        .ReportFilename = ".rpt" 'aqui va el reporte de cuadros de polizas de productos APV (no tradicionales)
                    End If

                    'Aqui hay que agregar el campo fecha de copia (tcdCopydate), para el scodispl CAL01512	
                    If CDbl(Request.Form.Item("optEje")) = 1 Then
                        '***********parametros puntuales	
                        .setStorProcParam(1, lngBranch)
                        .setStorProcParam(2, lngProduct)
                        .setStorProcParam(3, nPolicy) 'poliza 
                        .setStorProcParam(4, nCertif) 'certificado
                        .setStorProcParam(5, Request.Form.Item("optTrans")) 'tipo de informacion
                        .setStorProcParam(6, nUserCode)
                        If sCodispl = "CAL01512" Then
                            .setStorProcParam(7, .setdate(dDateCopy))
                        Else
                            .setStorProcParam(7, dDateCopy)
                        End If
                        .Merge = True
                        .MergeBranch = lngBranch
                        .MergeProduct = lngProduct
                        .MergePolicy = nPolicy
                        .MergeCertif = nCertif
                        .nCopies = 1
                        Response.Write((.Command))
                    Else
                        .setStorProcParam(1, lngBranch)
                        .setStorProcParam(2, lngProduct)
                        .setStorProcParam(3, 0) 'poliza 
                        .setStorProcParam(4, 0) 'certificado
                        .setStorProcParam(5, 0) 'tipo de informacion
                        .setStorProcParam(6, nUserCode)
                        If sCodispl = "CAL01512" Then
                            .setStorProcParam(7, .setdate(dDateCopy))
                        Else
                            .setStorProcParam(7, dDateCopy)
                        End If
                        .Merge = True
                        .MergeBranch = lngBranch
                        .MergeProduct = lngProduct
                        Response.Write((.Command))
                    End If

                '+CAL08000: Reporte de Cotización de los productos Protector y Más Protector						
                Case "CAL08000"
                    .sCodispl = sCodispl
                    .ReportFilename = sCodispl & ".rpt"
                    .setStorProcParam(1, nbranch)
                    .setStorProcParam(2, nproduct)
                    .setStorProcParam(3, nPolicy)
                    .setStorProcParam(4, nUserCode)
                    Response.Write((.Command))

                '+ VAL601: Proceso de cálculo valor póliza
                Case "VAL601"
                    '+Si la opción de ejecución es masiva se ejecuta el reporte
                    If Request.Form.Item("optEjecution") = "1" Then
                        .sCodispl = "VAL601"
                        .ReportFilename = "VAL601.rpt"
                        .setStorProcParam(1, Request.Form.Item("cbeMonth"))
                        .setStorProcParam(2, Request.Form.Item("tcnYear"))
                        .setStorProcParam(3, mstrKey)
                        Response.Write((.Command))
                    End If
                    .Reset()
                    mblnTimeOut = True
                    If Request.Form.Item("chkRep") = "1" Then
                        .sCodispl = "VAL601"
                        .ReportFilename = "VAL852.rpt"
                        .setStorProcParam(1, mstrKey)
                        Response.Write((.Command))
                    End If

                '+ VAL630: Impresión de cartolas de VidActiva
                Case "VAL630"
                    .sCodispl = "VAL630"
                    .ReportFilename = "VAL630.rpt"
                    .setStorProcParam(1, mstrKey)
                    Response.Write((.Command))

                '+ VAL696: Control de Caducidad de pólizas de VidActive
                Case "VAL696"
                    .sCodispl = "VAL696"
                    .ReportFilename = "VAL696.rpt"
                    .setStorProcParam(1, lstrKey696)
                    .setStorProcParam(2, .setdate(Request.Form.Item("tcdEffecdate")))
                    .setStorProcParam(3, mobjValues.StringToType(Request.Form.Item("optType"), eFunctions.Values.eTypeData.etdDouble))
                    Response.Write((.Command))

                '+ VAL633: Generación de recibos para Pólizas VidActiva
                Case "VAL633"
                    If Request.Form.Item("OptInfo") = "1" Then
                        .ReportFilename = "VAL633.rpt"
                        .sCodispl = "VAL633"
                        .setStorProcParam(1, mstrKey)
                        .setParamField(1, "dFromDate", mobjValues.StringToType(Request.Form.Item("tcdFromDate"), eFunctions.Values.eTypeData.etdDate))
                        .setParamField(2, "dToDate", mobjValues.StringToType(Request.Form.Item("tcdToDate"), eFunctions.Values.eTypeData.etdDate))
                        Response.Write((.Command))
                    End If
                    .Reset()
                    mblnTimeOut = True
                    .sCodispl = "VAL633"
                    .ReportFilename = "VAL633_Er.rpt"
                    .setStorProcParam(1, mstrKey)
                    Response.Write((.Command))

                '+ CAL826: Calculo de la prima ganada incobrable
                Case "CAL826"
                    .sCodispl = "CAL826"
                    .ReportFilename = "CAL826.rpt"
                    .setStorProcParam(1, .setdate(Request.Form.Item("tcdStart")))
                    .setStorProcParam(2, .setdate(Request.Form.Item("tcdEnd")))
                    .setStorProcParam(5, Request.Form.Item("optProcessType"))
                    .setStorProcParam(6, mclsPolicy.sKey)
                    Response.Write((.Command))

                '+ CAL784: Genración automática de propuestas de renovación
                Case "CAL784"
                    .sCodispl = "CAL784"
                    .ReportFilename = "CAL784.rpt"
                    .setStorProcParam(1, .setdate(Request.Form.Item("tcdStart")))
                    .setStorProcParam(2, .setdate(Request.Form.Item("tcdEnd")))
                    .setStorProcParam(3, Session("ChkOptionCAL784"))
                    .setStorProcParam(4, mclsPolicy.sKey)
                    Response.Write((.Command))

                '+ VIL7700: CARTOLA APV [APV2] - ACM - 16/09/2003
                Case "VIL7700"
                    .sCodispl = "VIL7700"
                    .ReportFilename = "VIL7700.rpt"
                    .setParamField(1, "dStartDate", Request.Form.Item("tcdInitDate"))
                    .setParamField(2, "dEndDate", Request.Form.Item("tcdEndDate"))
                    .setStorProcParam(1, mdblNumCart)
                    If mobjValues.StringToType(Request.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble, True) = eRemoteDB.Constants.intNull Then
                        .setStorProcParam(2, "")
                    Else
                        .setStorProcParam(2, mobjValues.StringToType(Request.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble, True))
                    End If

                    If mobjValues.StringToType(Request.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdDouble) = eRemoteDB.Constants.intNull Then
                        .setStorProcParam(3, "")
                    Else
                        .setStorProcParam(3, mobjValues.StringToType(Request.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdDouble))
                    End If

                    If mobjValues.StringToType(Request.Form.Item("tcnPolicy"), eFunctions.Values.eTypeData.etdDouble) = eRemoteDB.Constants.intNull Then
                        .setStorProcParam(4, "")
                    Else
                        .setStorProcParam(4, mobjValues.StringToType(Request.Form.Item("tcnPolicy"), eFunctions.Values.eTypeData.etdDouble))
                    End If
                    .setStorProcParam(5, "")
                    .setStorProcParam(6, lstrKeyVil7700)

                    Response.Write((.Command))

                '+ VIL7701: Conversión Automática de Propuesta a Póliza
                '+[APV2]: HAD 1018. Conversión Automática de Propuesta a Póliza
                Case "VIL7701"
                    .ReportFilename = "VIL7701.rpt"
                    .sCodispl = "VIL7701"
                    .setStorProcParam(1, mstrKey) 'Request.Form("optTypePD")
                    Response.Write((.Command))

                '+ VIL7020: Certificado Nº 24 sobre movimiento anual de APV (por RUT)
                Case "VIL7020"
                    .ReportFilename = "VIL7020.rpt"
                    .sCodispl = "VIL7020"
                    nCertif = mobjValues.StringToType(Request.Form.Item("tcnCertif"), eFunctions.Values.eTypeData.etdDouble)
                    if nCertif < 0 then
                        nCertif = 0
                    end if
                    .setStorProcParam(1, mobjValues.StringToType(Request.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble))
                    .setStorProcParam(2, mobjValues.StringToType(Request.Form.Item("valproduct"), eFunctions.Values.eTypeData.etdDouble))
                    .setStorProcParam(3, mobjValues.StringToType(Request.Form.Item("tcnPolicy"), eFunctions.Values.eTypeData.etdDouble))
                    .setStorProcParam(4, nCertif)
                    .setStorProcParam(5, Request.Form.Item("dtcClient"))
                    .setStorProcParam(6, .setdate(Request.Form.Item("tcdInitDate")))
                    .setStorProcParam(7, .setdate(Request.Form.Item("tcdEndDate")))
                    Response.Write((.Command))


                '+ VIL7021: Certificado Nº 24 sobre movimiento anual de APV (por RUT)
                Case "VIL7021"
                    .ReportFilename = "VIL7021.rpt"
                    .sCodispl = "VIL7021"
                    Response.Write((.Command))

                '+ VIL891: Cartola detalle de préstamos y rescates
                Case "VIL1413"
                    .sCodispl = "VIL1413"
                    .ReportFilename = "VIL1413.rpt"
                    .setStorProcParam(1, mobjValues.StringToType(Request.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble))
                    .setStorProcParam(2, mobjValues.StringToType(Request.Form.Item("valproduct"), eFunctions.Values.eTypeData.etdDouble))
                    .setStorProcParam(3, .setdate(Request.Form.Item("tcdDate_ini")))
                    .setStorProcParam(4, .setdate(Request.Form.Item("tcdDate_end")))
                    .setStorProcParam(5, mobjValues.StringToType(Request.Form.Item("cbeTypeMove"), eFunctions.Values.eTypeData.etdDouble))
                    .setStorProcParam(6, Request.Form.Item("hddsType_move"))
                    .setStorProcParam(7, mobjValues.StringToType(Request.Form.Item("tcnPolicy"), eFunctions.Values.eTypeData.etdDouble))
                    Response.Write((.Command))

                '+ CAL503: Libro timbrado de produccion
                Case "CAL503"
                    If CStr(Session("BatchEnabled")) <> "1" Then
                        .sCodispl = "CAL503"
                        .ReportFilename = "CAL503.rpt"
                        .setStorProcParam(1, Session("P_SKEY"))
                        Response.Write((.Command))
                    End If

                Case "CAL01415"
                    .sCodispl = "CAL01415"
                    .ReportFilename = "CAL01415.RPT"
                    .setStorProcParam(1, Request.Form.Item("hddsCertype"))
                    .setStorProcParam(2, Request.Form.Item("cbeBranch1"))
                    .setStorProcParam(3, Request.Form.Item("valProduct1"))
                    .setStorProcParam(4, Request.Form.Item("tcnPolicy"))
                    .setStorProcParam(5, nCertif) 'Request.Form("tcnCertif")			    
                    If Not IsNothing(Request.Form.Item("dtcClientCO")) Then
                        .setStorProcParam(6, Request.Form.Item("dtcClientCO"))
                    Else
                        .setStorProcParam(6, "0")
                    End If
                    If Not IsNothing(Request.Form.Item("dtcClientAS")) Then
                        .setStorProcParam(7, Request.Form.Item("dtcClientAS"))
                    Else
                        .setStorProcParam(7, "0")
                    End If
                    .setStorProcParam(8, Request.Form.Item("tcdEffecdate").Substring(6, 4) & Request.Form.Item("tcdEffecdate").Substring(3, 2) & Request.Form.Item("tcdEffecdate").Substring(0, 2))
                    '.setStorProcParam 8, Request.Form("tcdExpirdat")
                    .setStorProcParam(9, "")
                    .setStorProcParam(10, Session("nUsercode"))
                    Response.Write((.Command))

                Case "CAL00975"
                    .sCodispl = sCodispl
                    .ReportFilename = sCodispl & ".rpt"
                    .setStorProcParam(1, Request.Form.Item("dtcClient"))
                    .setStorProcParam(2, Request.Form.Item("tcdEndDate"))
                    Response.Write((.Command))

                Case "CAL00976"
                    .sCodispl = sCodispl
                    .ReportFilename = sCodispl & ".rpt"
                    .setStorProcParam(1, Request.Form.Item("tcdIniDate"))
                    .setStorProcParam(2, Request.Form.Item("tcdEndDate"))
                    Response.Write((.Command))

                '+ VIL08000: Reporte de Ahorros Garantizados				
                Case "VIL08000"
                    .sCodispl = "VIL08000"
                    .ReportFilename = "VIL08000.RPT"
                    .setStorProcParam(1, Request.Form.Item("tcdIniDate"))
                    .setStorProcParam(2, Request.Form.Item("tcdEndDate"))
                    .setStorProcParam(3, nEstadoag)
                    .setStorProcParam(4, nUserCode)
                    Response.Write((.Command))

                    '+ VIL08001: Reporte de Esquemas del Ahorro Garantizado
                Case "VIL08001"
                    .sCodispl = "VIL08001"
                    .ReportFilename = sCodispl & ".rpt"
                    .setStorProcParam(1, Request.Form.Item("tcnYear"))
                    .setStorProcParam(2, Request.Form.Item("cbeMonth"))
                    Response.Write((.Command))


                Case "VIL8002"
                    .sCodispl = "VIL8002"
                    .ReportFilename = "QuotationNewAPV.RPT"
                    .setStorProcParam(1, "3")
                    .setStorProcParam(2, Request.Form.Item("cbeBranch"))
                    .setStorProcParam(3, Request.Form.Item("valProduct"))
                    .setStorProcParam(4, Request.Form.Item("tcnPolicy"))
                    .setStorProcParam(5, 0)
                    .setStorProcParam(6, Request.Form.Item("valIntermedia"))
                    Response.Write((.Command))

                Case "VIL8003"
                    .sCodispl = "VIL8003"
                    .ReportFilename = "QuotationPrevisorPlus.RPT"
                    .setStorProcParam(1, "3")
                    .setStorProcParam(2, Request.Form.Item("cbeBranch"))
                    .setStorProcParam(3, Request.Form.Item("valProduct"))
                    .setStorProcParam(4, Request.Form.Item("tcnPolicy"))
                    .setStorProcParam(5, 0)
                    .setStorProcParam(6, Request.Form.Item("valIntermedia"))
                    Response.Write((.Command))

                Case "VIL8004"
                    .sCodispl = "VIL8004"
                    .ReportFilename = "QuotationPlanificador.RPT"
                    .setStorProcParam(1, "3")
                    .setStorProcParam(2, Request.Form.Item("cbeBranch"))
                    .setStorProcParam(3, Request.Form.Item("valProduct"))
                    .setStorProcParam(4, Request.Form.Item("tcnPolicy"))
                    .setStorProcParam(5, 0)
                    .setStorProcParam(6, Request.Form.Item("valIntermedia"))
                    Response.Write((.Command))

                    '+ VIL08006: Reporte de saldos finales por fondo			
                Case "VIL08006"
                    .sCodispl = "VIL08006"
                    .ReportFilename = "VIL08006.RPT"
                    .setStorProcParam(1, ncbeMonth)
                    .setStorProcParam(2, ntcnYear)
                    .setStorProcParam(3, nUserCode)
                    Response.Write((.Command))

                    '+ VIL8005: Reporte de Esquema APV
                Case "VIL8005"
                    'UPGRADE_NOTE: The 'eGeneral.GeneralFunction' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Program Files\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm
                    mclsGeneral = New eGeneral.GeneralFunction
                    mstrKey = mclsGeneral.getsKey(Session("nUsercode"))
                    mstrFileName = "window.open('/VTimeNet/tfiles/" & mstrKey & ".xls','Listado');"
                    mclsGeneral = Nothing
                    'UPGRADE_NOTE: The 'eBatch.MasiveCharge' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Program Files\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm
                    mclsGeneral = New eBatch.MasiveCharge
                    mstrPath = mclsGeneral.GetLoadFile(True)
                    mclsGeneral = Nothing
                    'UPGRADE_NOTE: The 'ePolicy.ValPolicyRep' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Program Files\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm
                    mclsPolicy = New ePolicy.ValPolicyRep
                    If mclsPolicy.insReportExcel_VIL8005(mstrKey, mstrPath, ncbeMonth, ntcnYear) Then
                        Response.Write("<SCRIPT>" & mstrFileName & " </" & "Script>")
                    Else
                        Response.Write("<SCRIPT>alert('Error');</" & "Script>")
                    End If
                    mclsPolicy = Nothing

                    '+ VIL8007: Reporte de cartolas mensuales
                Case "VIL8007"
                    If CStr(Session("BatchEnabled")) <> "1" Then
                        'UPGRADE_NOTE: The 'eGeneral.GeneralFunction' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Program Files\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm
                        mclsGeneral = New eGeneral.GeneralFunction
                        mstrKey = mclsGeneral.getsKey(Session("nUsercode"))
                        mstrFileName = "window.open('/VTimeNet/tfiles/" & Replace(Request.Form.Item("tctFile"), ".xls", "") & ".xls','Listado');"
                        mclsGeneral = Nothing
                        'UPGRADE_NOTE: The 'eBatch.MasiveCharge' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Program Files\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm
                        mclsGeneral = New eBatch.MasiveCharge
                        mstrPath = mclsGeneral.GetLoadFile(True)
                        mclsGeneral = Nothing
                        'UPGRADE_NOTE: The 'ePolicy.ValPolicyRep' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Program Files\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm
                        mclsPolicy = New ePolicy.ValPolicyRep
                        If mclsPolicy.insPostVIL8007(ncbeMonth, ntcnYear, nbranch, nproduct, Request.Form.Item("tctFile")) Then
                            Response.Write("<SCRIPT>" & mstrFileName & " </" & "Script>")
                        Else
                            Response.Write("<SCRIPT>alert('No existen datos para los parámetros ingresados');</" & "Script>")
                        End If
                        mclsPolicy = Nothing
                    Else
                        '+Se almacenan los parámetros del proceso batch
                        'UPGRADE_NOTE: The 'eSchedule.Batch_param' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Program Files\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm
                        lclsBatch_param = New eSchedule.Batch_Param
                        With lclsBatch_param
                            .nBatch = 162
                            .nUserCode = mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble)
                            .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, .sKey)
                            .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, mobjValues.StringToType(ncbeMonth, eFunctions.Values.eTypeData.etdDouble))
                            .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, mobjValues.StringToType(ntcnYear, eFunctions.Values.eTypeData.etdDouble))
                            .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, mobjValues.StringToType(nbranch, eFunctions.Values.eTypeData.etdDouble))
                            .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, mobjValues.StringToType(nproduct, eFunctions.Values.eTypeData.etdDouble))
                            .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble))
                            .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaRes, .sKey)
                            .Save()
                        End With
                        Response.Write("<SCRIPT>alert('Se generó la clave de proceso: " & lclsBatch_param.sKey & "');</" & "Script>")
                        lclsBatch_param = Nothing
                    End If

                    '+ CAL803: Reporte de cartolas mensuales
                Case "CAL803"
                    'UPGRADE_NOTE: The 'eGeneral.GeneralFunction' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Program Files\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm
                    mclsGeneral = New eGeneral.GeneralFunction
                    mstrKey = mclsGeneral.getsKey(Session("nUsercode"))
                    mstrFileName = "window.open('/VTimeNet/tfiles/" & Replace(Request.Form.Item("tctFile"), ".xls", "") & ".xls','Listado');"
                    mclsGeneral = Nothing
                    'UPGRADE_NOTE: The 'eBatch.MasiveCharge' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Program Files\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm
                    mclsGeneral = New eBatch.MasiveCharge
                    mstrPath = mclsGeneral.GetLoadFile(True)
                    mclsGeneral = Nothing
                    'UPGRADE_NOTE: The 'ePolicy.ValPolicyRep' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Program Files\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm
                    mclsPolicy = New ePolicy.ValPolicyRep
                    If mclsPolicy.insPostCAL803(ncbeMonth, ntcnYear, Request.Form.Item("tctFile")) Then
                        Response.Write("<SCRIPT>" & mstrFileName & " </" & "Script>")
                    Else
                        Response.Write("<SCRIPT>alert('No existen datos para los parámetros ingresados');</" & "Script>")
                    End If
                    mclsPolicy = Nothing

                    '+ VIL08011: Reporte de emision
                Case "VIL8011"
                    .sCodispl = "VIL8011"
                    .ReportFilename = "VIL8011.RPT"
                    .setStorProcParam(1, ncbeMonth)
                    .setStorProcParam(2, ntcnYear)
                    Response.Write((.Command))

                    '+ VIL8009: Reservas por Producto de Ahorros Garantizados
                Case "VIL8009"
                    'UPGRADE_NOTE: The 'eGeneral.GeneralFunction' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Program Files\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm
                    mclsGeneral = New eGeneral.GeneralFunction
                    mstrKey = mclsGeneral.getsKey(Session("nUsercode"))
                    mstrFileName = "window.open('/VTimeNet/tfiles/" & mstrKey & ".xls','Listado');"
                    mclsGeneral = Nothing
                    'UPGRADE_NOTE: The 'eBatch.MasiveCharge' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Program Files\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm
                    mclsGeneral = New eBatch.MasiveCharge
                    mstrPath = mclsGeneral.GetLoadFile(True)
                    mclsGeneral = Nothing
                    'UPGRADE_NOTE: The 'ePolicy.ValPolicyRep' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Program Files\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm
                    mclsPolicy = New ePolicy.ValPolicyRep
                    If mclsPolicy.insReportExcel_VIL8009(mstrKey, mstrPath, ncbeMonth, ntcnYear) Then
                        Response.Write("<SCRIPT>" & mstrFileName & " </" & "Script>")
                    Else
                        Response.Write("<SCRIPT>alert('Error');</" & "Script>")
                    End If
                    mclsPolicy = Nothing

                    '+ VIL8012: Reporte de FECU Corredores
                Case "VIL8012"
                    .sCodispl = "VIL8012"
                    .ReportFilename = "VIL8012.RPT"
                    .setStorProcParam(1, .setdate(Request.Form.Item("tcdIniDate")))
                    .setStorProcParam(2, .setdate(Request.Form.Item("tcdEndDate")))
                    Response.Write((.Command))

                    '+ VIL8030: Reporte de resumen de libro de producción foliado
                Case "VIL8030"
                    .sCodispl = "VIL8030"
                    .ReportFilename = "VIL8030.RPT"
                    .setStorProcParam(1, .setdate(dIniDate))
                    .setStorProcParam(2, .setdate(dEndDate))
                    .setStorProcParam(3, nbranch)
                    .setStorProcParam(4, nproduct)
                    Response.Write((.Command))
                    .Reset()
                    .sCodispl = "VIL8030"
                    .ReportFilename = "VIL8030_1.RPT"
                    .setStorProcParam(1, .setdate(dIniDate))
                    .setStorProcParam(2, .setdate(dEndDate))
                    .setStorProcParam(3, nbranch)
                    .setStorProcParam(4, nproduct)
                    .setStorProcParam(5, nModulec)
                    Response.Write((.Command))

                    '+ VIL8010: Reporte de FECU Mensual Interno
                Case "VIL8010"
                    .sCodispl = "VIL8010"
                    .ReportFilename = "VIL8010.RPT"
                    .setStorProcParam(1, .setdate(dEndDate))
                    Response.Write((.Command))

                    '+ VIL8031: Reporte de resumen de libro de producción foliado
                Case "VIL8031"
                    .sCodispl = "VIL8031"
                    .ReportFilename = "VIL8031.RPT"
                    .setStorProcParam(1, .setdate(dIniDate))
                    .setStorProcParam(2, .setdate(dEndDate))
                    Response.Write((.Command))
                    .Reset()
                    .sCodispl = "VIL8031"
                    .ReportFilename = "VIL8031_1.RPT"
                    .setStorProcParam(1, .setdate(dIniDate))
                    .setStorProcParam(2, .setdate(dEndDate))
                    Response.Write((.Command))


                    '% HAD033 VIL8032 : Reporte de resumen producción por oficina					   
                Case "VIL8032"
                    .sCodispl = "VIL8032"
                    .ReportFilename = "VIL8032.RPT"
                    .setStorProcParam(1, Request.Form.Item("tcdIniDate"))
                    .setStorProcParam(2, Request.Form.Item("tcdEndDate"))
                    .setStorProcParam(3, nUserCode)
                    Response.Write((.Command))

                    '% HAD034 - Reporte de Resumen de Produccion por cobertura
                Case "VIL8033"
                    .sCodispl = "VIL8033"
                    .ReportFilename = "VIL8033.RPT"
                    .setStorProcParam(1, ncbeMonth)
                    .setStorProcParam(2, ntcnYear)
                    .setStorProcParam(3, Request.Form.Item("optType"))
                    .setStorProcParam(4, Request.Form.Item("optTypeCover"))
                    .setStorProcParam(5, nUserCode)
                    Response.Write((.Command))
                    '% CAL01510 - Reporte de Endoso
                Case "CAL01510"

                    .sCodispl = "CAL01510"
                    .ReportFilename = "CAL01510"
                    .setStorProcParam(1, "2")
                    .setStorProcParam(2, mobjValues.StringToType(Request.Form("cbeBranch"), eFunctions.Values.eTypeData.etdDouble))
                    .setStorProcParam(3, mobjValues.StringToType(Request.Form("valproduct"), eFunctions.Values.eTypeData.etdDouble))
                    .setStorProcParam(4, mobjValues.StringToType(Request.Form("tcnPolicy"), eFunctions.Values.eTypeData.etdDouble))
                    .setStorProcParam(5, mobjValues.StringToType(Request.Form("tcnCertif"), eFunctions.Values.eTypeData.etdDouble))
                    .setStorProcParam(6, "0")
                    .setStorProcParam(7, .setdate(Request.Form("tcdBeginDate")))
                    .setStorProcParam(8, .setdate(Request.Form("tcdEndDate")))
                    .nReport = 1 'clnAmendent
                    .MergeCertype = "6"
                    .MergeBranch = mobjValues.StringToType(Request.Form("cbeBranch"), eFunctions.Values.eTypeData.etdDouble)
                    .MergeProduct = mobjValues.StringToType(Request.Form("valproduct"), eFunctions.Values.eTypeData.etdDouble)
                    Response.Write(.Command)

                    '+ CAL08001: Detalle de póliza de vida
                Case "CAL08001"
                    .sCodispl = "CAL08001"
                    .ReportFilename = "CAL08001.rpt"
                    .setStorProcParam(1, mobjValues.StringToType(Request.Form.Item("cbeBranch1"), eFunctions.Values.eTypeData.etdDouble))
                    .setStorProcParam(2, mobjValues.StringToType(Request.Form.Item("valProduct1"), eFunctions.Values.eTypeData.etdDouble))
                    .setStorProcParam(3, mobjValues.StringToType(Request.Form.Item("tcnPolicy"), eFunctions.Values.eTypeData.etdDouble))
                    Response.Write((.Command))

                    '+ CAL8000: Reporte de pólizas saldadas
                Case "CAL8000"
                    .sCodispl = "CAL8000"
                    .ReportFilename = "CAL8000.rpt"
                    .setStorProcParam(1, Request.Form.Item("tcnYear"))
                    .setStorProcParam(2, Request.Form.Item("cboMonth"))
                    Response.Write((.Command))

                    '+ CAL0110: Reporte cuadro poliza    
                Case "CAL0110"
                    .sCodispl = "CAL0110" 'sCodispl
                    lclsReport_prod = New eProduct.report_prod
                    Call lclsReport_prod.Find(mobjValues.StringToType(Request.Form("cbeBranch"), eFunctions.Values.eTypeData.etdDouble), _
                                                      mobjValues.StringToType(Request.Form("valProduct"), eFunctions.Values.eTypeData.etdDouble), _
                                                       Now, _
                                                       Request.QueryString("sCodispl"), _
                                                       1)

                    .ReportFilename = lclsReport_prod.sReport

                    .setstorprocparam(1, "7")
                    .setstorprocparam(2, "1")
                    .setStorProcParam(3, "1261")
                    .setstorprocparam(4, "0")
                    .setstorprocparam(5, "2")
                    .setstorprocparam(6, "1286")
                    .setStorProcParam(7, "20121219")
                    .nReport = 2
                    .Merge = True
                    .MergeCertype = "2"
                    .MergeBranch = 7
                    .MergeProduct = 1
                    .MergePolicy = 1261
                    .nGenPolicy = 1
                    .nMovement = 1
                    .nForzaRep = 1
                    '.MergeCodispl = sCodispl
                    lclsReport_prod = Nothing


                '+ VIL1890: Reporte certificado 7
                Case "VIL1890"
                    .sCodispl = "VIL1890"
                    .ReportFilename = "VIL1890.RPT"
                    .setStorProcParam(1, mobjValues.StringToType(Request.Form("optProcessType"), eFunctions.Values.eTypeData.etdDouble, True))
                    .setStorProcParam(2, mobjValues.StringToType(Request.Form("tcnYear"), eFunctions.Values.eTypeData.etdDouble, True))
                    .setStorProcParam(3, mobjValues.StringToType(Request.Form("tcnRectif"), eFunctions.Values.eTypeData.etdDouble, True))
                    .setStorProcParam(4, Request.Form("valClient"))
                    .setStorProcParam(5, mobjValues.StringToType(Request.Form("tcdPrintDate"), eFunctions.Values.eTypeData.etdDate))
                    Response.Write(.Command)
            End Select
        End With
        lobjDocuments = Nothing
    End Sub


    Sub BuildUploadRequest()
        Dim data() As Byte
        Dim ByteCount = Request.TotalBytes
        Dim RequestBin = Request.BinaryRead(ByteCount)


        'Array que contendrá la data decodificada
        Dim postData(data.Length) As Char

        'Se inicializa el decodificador ASCII
        Dim decoder As Decoder = Encoding.ASCII.GetDecoder

        'Se decodifican los bytes contenidos en binData, y se almacena en el array postData
        decoder.GetChars(data, 0, data.Length, postData, 0)

        'Se obtiene el Encoding Type y el Boundary del Form, y se separan en un array.
        Dim contentType As String = Request.ServerVariables("HTTP_CONTENT_TYPE")
        Dim conTypArr() As String = contentType.Split("; ")

        'Se verifica que el Encoding Type sea el correcto. De otro modo no se podra leer el archivo.
        If conTypArr(0) = "multipart/form-data" Then
            'Se obtiene el Boundary del Form. Este dato es el que separa los valores de cada control en el Request.
            Dim bound(1) As String
            bound(1) = conTypArr(1).Split("=")(1)
            'Se obtiene un array, que contiene la data de todos los controles del Form.
            Dim formData() As String = (New String(postData)).Split(bound, StringSplitOptions.RemoveEmptyEntries)

            'Se inicializa el diccionario.
            mobjUploadRequest = New Dictionary(Of String, String)

            Dim endInfo As Integer
            Dim varInfo As String
            Dim varValue As String

            For i As Integer = 0 To formData.Length - 1
                'Se ubican los caracteres separadores.
                endInfo = formData(i).IndexOf(crlf & crlf)

                If endInfo > -1 Then
                    'Obtiene el nombre de la variable
                    varInfo = formData(i).Substring(2, endInfo - 2)
                    'Obtiene el valor de la variable
                    varValue = formData(i).Substring(endInfo + 4, formData(i).Length - endInfo - 8)

                    'Es este elemento un archivo?
                    If varInfo.Contains("filename=") Then
                        myRequestFile(0) = getFieldName(varInfo)
                        myRequestFile(1) = varValue
                        myRequestFile(2) = getFileName(varInfo)
                        myRequestFile(3) = getFileType(varInfo)

                        fileContentIndex = (New String(postData)).IndexOf(varValue)

                        fileContentLength = varValue.Length

                    Else
                        mobjUploadRequest.Add(getFieldName(varInfo), varValue)
                    End If
                End If
            Next
        End If
    End Sub


    '% insUpLoadFileOri: Se encarga de subir el archivo seleccionado al servidor según una ruta 
    '% pasada como parámetro.
    '% FilePath: Ruta física donde se va almacenar el archivo en el servidor. 
    '%Eje. "c:\InetPub\UpLoad\"
    '--------------------------------------------------------------------------------------------
    Function insUpLoadFileOri(ByRef FilePath As String) As Boolean
        Dim mstrFileContent As String
        '--------------------------------------------------------------------------------------------
        Dim ForWriting As Integer
        Dim adLongVarChar As Integer
        Dim lngNumberUploaded As Integer
        Dim LenBinary As Object
        Dim strBoundry As String
        Dim lngBoundryPos As Integer
        Dim lngCurrentBegin As Integer
        Dim lngCurrentEnd As Integer
        Dim strData As String
        Dim strDataWhole As String
        Dim lngEndFileName As Integer
        Dim FileName As String
        Dim lngBeginPos As Integer
        Dim lngEndPos As Integer
        Dim lngDataLenth As Integer
        Dim ByteCount As Integer
        Dim RequestBin As Object
        Dim PosBeg As Double
        Dim PosEnd As Integer
        Dim boundary As String
        Dim boundaryPos As Integer
        Dim Pos As Integer
        Dim Name As String
        Dim PosFile As Integer
        Dim ContentType As String
        Dim Value As String
        Dim PosBound As Integer
        Dim PrevPos As Integer
        Dim tmpLng As Integer
        Dim lngCt As Integer
        Dim strFileData As String
        Dim RST As Object 'ADODB.Recordset
        Dim f As Object

        UploadRequest = Nothing 'New Scripting.Dictionary

        ForWriting = 2
        adLongVarChar = 201
        lngNumberUploaded = 0

        ByteCount = Request.TotalBytes
        RequestBin = Request.BinaryRead(ByteCount)

        RST = Nothing 'New ADODB.Recordset
        'UPGRADE_ISSUE: LenB function is not supported. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1040.aspx'
        LenBinary = Len(RequestBin)

        If LenBinary > 0 Then
            RST.Fields.Append("myBinary", adLongVarChar, LenBinary)
            RST.Open()
            RST.AddNew()
            RST.Fields("myBinary").AppendChunk(RequestBin)
            RST.Update()
            strDataWhole = IIF(IsDBNull(RST.Fields.Item("myBinary").Value), Nothing, RST.Fields.Item("myBinary").Value)
            'Creates a raw data file for with all data sent. Uncomment for debuging. 
            '        Set fso = CreateObject("Scripting.FileSystemObject")
            '        Set f = fso.OpenTextFile("c:\InetPub\UpLoad" & "\rawINI.txt", ForWriting, True)
            '        f.Write strDataWhole
            '        set f = nothing
            '        set fso = nothing
        End If
        RST = Nothing

        '+ Se calcula el número de elementos a evaluar
        PosBeg = 1
        'UPGRADE_ISSUE: InStrB function is not supported. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1040.aspx'
        PosEnd = InStr(CInt(PosBeg), RequestBin, getByteString(Chr(13)))

        'UPGRADE_ISSUE: MidB function is not supported. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1040.aspx'
        boundary = Mid(RequestBin, PosBeg, PosEnd - PosBeg)
        'UPGRADE_ISSUE: InStrB function is not supported. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1040.aspx'
        boundaryPos = InStr(1, RequestBin, boundary)

        '+ Se busca entre todos los elementos que recibe la página, el que corresponde a la imagen
        Dim UploadControl As Object 'Scripting.Dictionary
        'UPGRADE_ISSUE: InStrB function is not supported. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1040.aspx'
        Do Until (boundaryPos = InStr(RequestBin, boundary & getByteString("--")))

            '+ Variable para el manejo del diccionario del objeto
            UploadControl = Nothing 'New Scripting.Dictionary

            '+ Se toma el nombre del objeto
            'UPGRADE_ISSUE: InStrB function is not supported. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1040.aspx'
            Pos = InStr(boundaryPos, RequestBin, getByteString("Content-Disposition"))
            'UPGRADE_ISSUE: InStrB function is not supported. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1040.aspx'
            Pos = InStr(Pos, RequestBin, getByteString("name="))
            PosBeg = Pos + 6
            'UPGRADE_ISSUE: InStrB function is not supported. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1040.aspx'
            PosEnd = InStr(CInt(PosBeg), RequestBin, getByteString(Chr(34)))
            'UPGRADE_ISSUE: MidB function is not supported. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1040.aspx'
            Name = getString(Mid(RequestBin, PosBeg, PosEnd - PosBeg))
            'UPGRADE_ISSUE: InStrB function is not supported. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1040.aspx'
            PosFile = InStr(boundaryPos, RequestBin, getByteString("filename="))
            'UPGRADE_ISSUE: InStrB function is not supported. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1040.aspx'
            PosBound = InStr(PosEnd, RequestBin, boundary)
            '+ Se verifica si el objeto corresponde a un <INPUT TYPE=FILE id=FILE1 name=FILE1>
            If PosFile <> 0 And (PosFile < PosBound) Then

                insUpLoadFileOri = True
                'get the boundry indicator
                strBoundry = Request.ServerVariables.Item("HTTP_CONTENT_TYPE")
                lngBoundryPos = InStr(1, strBoundry, "boundary=") + 8
                strBoundry = "--" & Right(strBoundry, Len(strBoundry) - lngBoundryPos)
                'Get first file boundry positions.
                lngCurrentBegin = InStr(1, strData, strBoundry)
                lngCurrentEnd = InStr(lngCurrentBegin + 1, strData, strBoundry) - 1

                'Get the data between current boundry and remove it from the whole.
                strData = Mid(strData, lngCurrentBegin, lngCurrentEnd - lngCurrentBegin)

                strDataWhole = Replace(strDataWhole, strData, "")

                '+ Se toma el tipo, nombre y contenido del archivo
                PosBeg = PosFile + 10
                'UPGRADE_ISSUE: InStrB function is not supported. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1040.aspx'
                PosEnd = InStr(CInt(PosBeg), RequestBin, getByteString(Chr(34)))
                'UPGRADE_ISSUE: MidB function is not supported. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1040.aspx'
                FileName = getString(Mid(RequestBin, PosBeg, PosEnd - PosBeg))

                'Create the file.
                tmpLng = InStr(1, FileName, "\")
                Do While tmpLng > 0
                    PrevPos = tmpLng
                    tmpLng = InStr(PrevPos + 1, FileName, "\")
                Loop
                FileName = Right(FileName, Len(FileName) - PrevPos)
                If FileName = vbNullString Then
                    insUpLoadFileOri = False
                End If
                '+ Se añade el nombre al diccionario del objeto
                UploadControl.Add("FileName", FileName)
                'UPGRADE_ISSUE: InStrB function is not supported. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1040.aspx'
                Pos = InStr(PosEnd, RequestBin, getByteString("Content-Type:"))
                PosBeg = Pos + 14
                'UPGRADE_ISSUE: InStrB function is not supported. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1040.aspx'
                PosEnd = InStr(CInt(PosBeg), RequestBin, getByteString(Chr(13)))

                '+ Se añade el tipo al diccionario del objeto
                'UPGRADE_ISSUE: MidB function is not supported. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1040.aspx'
                ContentType = getString(Mid(RequestBin, PosBeg, PosEnd - PosBeg))
                UploadControl.Add("ContentType", ContentType)

                '+ Se toma contenido del archivo
                PosBeg = PosEnd + 4
                'UPGRADE_ISSUE: InStrB function is not supported. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1040.aspx'
                PosEnd = InStr(CInt(PosBeg), RequestBin, boundary) - 2
                'UPGRADE_ISSUE: MidB function is not supported. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1040.aspx'
                Value = Mid(RequestBin, PosBeg, PosEnd - PosBeg)

                lngCt = InStr(1, strData, "Content-Type:")

                If lngCt > 0 Then
                    lngBeginPos = InStr(lngCt, strData, Chr(13) & Chr(10)) + 4
                Else
                    lngBeginPos = lngEndFileName
                End If
                'Get the ending position of the file data sent.
                lngEndPos = Len(strData)

                'Calculate the file size. 
                lngDataLenth = lngEndPos - lngBeginPos

                'Get the file data 
                strFileData = Mid(strData, lngBeginPos, lngDataLenth)

                '+ En caso de que se haya seleccionado algún archivo.
                If insUpLoadFileOri Then

                    '                Set fso = CreateObject("Scripting.FileSystemObject")
                    '                Set f = fso.OpenTextFile(FilePath & FileName, ForWriting, True)
                    '                f.Write strFileData
                    mstrFileContent = strFileData
                    '                if not fso.FileExists(FilePath & FileName) then
                    '                    insUpLoadFileOri = false
                    '                end if
                    '                Set f = nothing
                    '                Set fso = nothing
                End If
            Else
                strBoundry = Request.ServerVariables.Item("HTTP_CONTENT_TYPE")
                lngBoundryPos = InStr(1, strBoundry, "boundary=") + 8
                strBoundry = "--" & Right(strBoundry, Len(strBoundry) - lngBoundryPos)

                'Get first file boundry positions.
                lngCurrentBegin = InStr(1, strDataWhole, strBoundry)
                lngCurrentEnd = InStr(lngCurrentBegin + 1, strDataWhole, strBoundry) - 1

                'Get the data between current boundry and remove it from the whole.
                strData = Mid(strDataWhole, lngCurrentBegin, lngCurrentEnd - lngCurrentBegin)

                strDataWhole = Replace(strDataWhole, strData, "")
                strData = strDataWhole

                '+ Si el objeto no es una imagen, se toma la información del mismo
                'UPGRADE_ISSUE: InStrB function is not supported. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1040.aspx'
                Pos = InStr(Pos, RequestBin, getByteString(Chr(13)))
                PosBeg = Pos + 4
                'UPGRADE_ISSUE: InStrB function is not supported. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1040.aspx'
                PosEnd = InStr(CInt(PosBeg), RequestBin, boundary) - 2
                'UPGRADE_ISSUE: MidB function is not supported. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1040.aspx'
                Value = getString(Mid(RequestBin, PosBeg, PosEnd - PosBeg))
            End If

            '+ Se añade el contenido al diccionario del objeto
            UploadControl.Add("Value", Value)

            '+ Se añade el objeto al diccionario principal de la página
            UploadRequest.Add(Name, UploadControl)

            '+ Se busca el siguiente objeto
            'UPGRADE_ISSUE: LenB function is not supported. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1040.aspx'
            'UPGRADE_ISSUE: InStrB function is not supported. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1040.aspx'
            boundaryPos = InStr(boundaryPos + Len(boundary), RequestBin, boundary)
            UploadControl = Nothing
        Loop
    End Function

    '% getString: Conversión de los datos de Byte a String
    '--------------------------------------------------------------------------------------------
    Function getString(ByRef StringBin As String) As String
        '--------------------------------------------------------------------------------------------
        Dim intCount As Integer
        getString = ""
        'UPGRADE_ISSUE: LenB function is not supported. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1040.aspx'
        For intCount = 1 To Len(StringBin)
            'UPGRADE_ISSUE: MidB function is not supported. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1040.aspx'
            'UPGRADE_ISSUE: AscB function is not supported. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1040.aspx'
            getString = getString & Chr(Asc(Mid(StringBin, intCount, 1)))
        Next
    End Function

    '% getByteString: Conversión de los datos de String a Byte
    '--------------------------------------------------------------------------------------------
    Function getByteString(ByRef StringStr As String) As String
        '--------------------------------------------------------------------------------------------
        Dim i As Integer
        'UPGRADE_NOTE: char was upgraded to char_Renamed. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1061.aspx'
        Dim char_Renamed As String
        For i = 1 To Len(StringStr)
            char_Renamed = Mid(StringStr, i, 1)
            'UPGRADE_ISSUE: AscB function is not supported. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1040.aspx'
            'UPGRADE_ISSUE: ChrB function is not supported. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1040.aspx'
            getByteString = getByteString & Chr(Asc(char_Renamed))
        Next
    End Function

    '% getReport: determina  si el reporte a traer es de apvc o normal 
    '--------------------------------------------------------------------------------------------
    Function getReport(ByRef nbranch As Object, ByRef nproduct As Object) As String
        '--------------------------------------------------------------------------------------------
        Dim lclsProduct As Object
        Dim lclsaction As Object
        getReport = "cal001_1.rpt"

        'Se verifica si es hogar
        If nbranch = 9 Then
            getReport = "cal001_Home.rpt"
        Else
            'Se verifica si el apvc
            lclsProduct = New eProduct.Product
            If lclsProduct.FindProduct_li(nbranch, nproduct, Now, True) Then
                If lclsProduct.SAPV = "1" Then
                    If lclsProduct.Find(nbranch, nproduct, Now, True) Then
                        ' si la poliza es colectiva 
                        If lclsProduct.sPolitype = "2" Then
                            getReport = "cal001_APVC.rpt"
                        End If
                    End If
                End If
            End If
            lclsProduct = Nothing
        End If
    End Function

    '% insUpLoadFile: Se encarga de subir el archivo seleccionado al servidor según ruta pasada como parámetro.
    '% FilePath: Ruta física donde se va almacenar el archivo en el servidor. Eje. "c:\InetPub\UpLoad\"
    '--------------------------------------------------------------------------------------------
    Function insUpLoadFile(ByRef FilePath As String,
                           ByVal FileName As string, ByVal FieldName As string,
                           Optional ByVal FileName1 As string = vbNullString, Optional ByVal FieldName1 As string = vbNullString,
                           Optional ByVal FileName2 As string = vbNullString, Optional ByVal FieldName2 As string = vbNullString) As Boolean
        '--------------------------------------------------------------------------------------------
        Dim llngForWriting As Integer
        Dim lstrBoundry As String
        Dim llngBoundryPos As Integer
        Dim lbytByteCount As Integer
        Dim lbytRequestBin() As Byte
        Dim mobjFormFile As eCollection.FormFile
        Dim llngBoundryPosaux As Integer
        Dim oFile as System.IO.File
        Dim oWrite as System.IO.StreamWriter

        llngForWriting = 2
        llngBoundryPos = 0
        llngBoundryPosaux = 0
        lbytByteCount = Request.TotalBytes
        lbytRequestBin = Request.BinaryRead(lbytByteCount)
        lstrBoundry = Request.ServerVariables.Item("HTTP_CONTENT_TYPE")
        llngBoundryPos = InStr(1, lstrBoundry, "boundary=") + 8

        If llngBoundryPos <> 8 Then
            llngBoundryPosaux = InStr(llngBoundryPos, lstrBoundry, "boundary=") + 8
        End If

        If llngBoundryPosaux <> 8 Then
            lstrBoundry = "--" & Right(lstrBoundry, Len(lstrBoundry) - llngBoundryPosaux)
        Else
            lstrBoundry = "--" & Right(lstrBoundry, Len(lstrBoundry) - llngBoundryPos)
        End If

        mobjFormFile = New eCollection.FormFile
        mobjFormFile.iBoundary = lstrBoundry
        mobjFormFile.iStreamBuffer = lbytRequestBin.Clone()

        ' Primer archivo
        oWrite = oFile.CreateText(mstrPath & FileName)
        oWrite.Write(mobjFormFile.Request(FieldName))
        oWrite.Close()

        ' Segundo archivo
        If Not String.IsNullOrEmpty(FileName1) then
            oWrite = oFile.CreateText(mstrPath & FileName1)
            oWrite.Write(mobjFormFile.Request(FieldName1))
            oWrite.Close()
        End If

        ' Tercer archivo
        If Not String.IsNullOrEmpty(FileName2) then
            oWrite = oFile.CreateText(mstrPath & FileName2)
            oWrite.Write(mobjFormFile.Request(FieldName2))
            oWrite.Close()
        End If

        'mstrFileName = lstrFileName
        mobjFormFile = Nothing
        insUpLoadFile = True

    End Function


    '% showClauses: Muestra las clausulas asociadas al producto en los cuadros pólizas
    '--------------------------------------------------------------------------------------------
    Private Sub showClauses()
        '--------------------------------------------------------------------------------------------
        Dim lobjTab_clauses As Object
        Dim lobjTab_clausess As Object
        Dim nInt As Short

        'UPGRADE_NOTE: The 'eProduct.Tab_clauses' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Program Files\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm
        lobjTab_clausess = new eProduct.Tab_clauses
        If lobjTab_clausess.Find(mobjValues.StringToType(Request.Form.Item("cbeBranch1"), eFunctions.Values.eTypeData.etdLong, True), mobjValues.StringToType(Request.Form.Item("valProduct1"), eFunctions.Values.eTypeData.etdLong, True), Today) Then
            nInt = 1
            For Each lobjTab_clauses In lobjTab_clausess
                If lobjTab_clauses.sDoc_attach <> "" Then
                    Response.Write("<SCRIPT LANGUAGE=JAVASCRIPT>")
                    Response.Write("ShowPopUp(""../../../VTimeNet/TFiles/Clause/" & lobjTab_clauses.sDoc_attach & """, ""Clause_" & nInt & """,660,330);")
                    Response.Write("</" & "Script>")
                    nInt = nInt + 1
                End If
            Next lobjTab_clauses
        End If
        lobjTab_clauses = Nothing
        lobjTab_clausess = Nothing
    End Sub

    ' This function retreives a field's name
    Function getFieldName(ByVal infoStr As String) As String
        Dim sPos As Integer = infoStr.IndexOf("name=")
        Dim endPos As Integer = infoStr.Substring(sPos + 5).IndexOf(Chr(34) & ";")
        If endPos = -1 Then
            endPos = infoStr.Substring(sPos + 6).IndexOf(Chr(34))
        End If

        Return infoStr.Substring(sPos + 6, endPos)
    End Function

    ' This function retreives a file field's filename
    Function getFileName(ByVal infoStr As String) As String
        Dim sPos As Integer = infoStr.IndexOf("filename=")
        Dim endPos As Integer = infoStr.IndexOf(Chr(34) & crlf)
        getFileName = infoStr.Substring(sPos + 10, endPos - (sPos + 10))
    End Function

    ' This function retreives a file field's mime type
    Function getFileType(ByVal infoStr As String) As String
        Dim sPos As Integer = infoStr.IndexOf("Content-Type: ")
        Return infoStr.Substring(sPos + 14)
    End Function

</SCRIPT>
<%
Response.Expires = -1
sCodispl = UCase(Request.QueryString.Item("sCodispl"))

mstrCommand = "&sModule=Policy&sProject=PolicyRep&sCodisplReload=" & sCodispl
'UPGRADE_NOTE: The 'eFunctions.values' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Program Files\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm
mobjValues = New eFunctions.values

mobjValues.sCodisplPage = "valpolicyrep"
mblnTimeOut = False
'UPGRADE_NOTE: The 'eProduct.Product' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Program Files\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm
lclsProduct = new eProduct.Product
 
nbranch = mobjValues.StringToType(Request.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdLong)
nproduct = mobjValues.StringToType(Request.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdLong)
nPolicy = mobjValues.StringToType(Request.Form.Item("tcnPolicy"), eFunctions.Values.eTypeData.etdDouble, True)

nClaim = mobjValues.StringToType(Request.Form.Item("tcnClaim"), eFunctions.Values.eTypeData.etdDouble, True)
dIniDate = mobjValues.StringToType(Request.Form.Item("tcdIniDate"), eFunctions.Values.eTypeData.etdDate)
dEndDate = mobjValues.StringToType(Request.Form.Item("tcdEndDate"), eFunctions.Values.eTypeData.etdDate)
sClient = Request.Form.Item("dtcClient")
nUserCode = mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdLong)
tPolicyType = mobjValues.StringToType(Request.Form.Item("cbePolicyType"), eFunctions.Values.eTypeData.etdDouble)
tAddressType = Request.Form.Item("optAddressType")
tWayPay = mobjValues.StringToType(Request.Form.Item("cbeWayPay"), eFunctions.Values.eTypeData.etdLong)
nBank = mobjValues.StringToType(Request.Form.Item("cbeBank"), eFunctions.Values.eTypeData.etdLong)
nCertif = mobjValues.StringToType(Request.Form.Item("tcnCertif"), eFunctions.Values.eTypeData.etdLong)

nOffice = mobjValues.StringToType(Request.Form.Item("cbeOffice"), eFunctions.Values.eTypeData.etdLong, True)
nOfficeAgen = mobjValues.StringToType(Request.Form.Item("cbeOfficeAgen"), eFunctions.Values.eTypeData.etdDouble, True)
nAgen = mobjValues.StringToType(Request.Form.Item("cbeAgency"), eFunctions.Values.eTypeData.etdDouble, True)
nPolicyIni = mobjValues.StringToType(Request.Form.Item("tcnPolicyIni"), eFunctions.Values.eTypeData.etdDouble, True)
nPolicyFin = mobjValues.StringToType(Request.Form.Item("tcnPolicyFin"), eFunctions.Values.eTypeData.etdDouble, True)
nTypeAmend = mobjValues.StringToType(Request.Form.Item("valTypeAmend"), eFunctions.Values.eTypeData.etdDouble, True)
nIntermed = mobjValues.StringToType(Request.Form.Item("nIntermed"), eFunctions.Values.eTypeData.etdLong)
nEstadoag = mobjValues.StringToType(Request.Form.Item("cbeCollectorType"), eFunctions.Values.eTypeData.etdLong)
ntcnYear = mobjValues.StringToType(Request.Form.Item("tcnYear"), eFunctions.Values.eTypeData.etdLong)
ncbeMonth = mobjValues.StringToType(Request.Form.Item("cbeMonth"), eFunctions.Values.eTypeData.etdLong)
nModulec = mobjValues.StringToType(Request.Form.Item("valModulec"), eFunctions.Values.eTypeData.etdLong)

If nbranch <= 0 Then nbranch = 0
If nproduct <= 0 Then nproduct = 0
If nPolicy <= 0 Then nPolicy = 0
If nClaim <= 0 Then nClaim = 0
If tPolicyType <= 0 Then tPolicyType = 0
If tAddressType <= 0 Then tAddressType = 0
If tWayPay <= 0 Then tWayPay = 0
If nBank <= 0 Then nBank = 0
If nCertif <= 0 Then nCertif = 0
If nOffice <= 0 Then nOffice = 0
If nOfficeAgen <= 0 Then nOfficeAgen = 0
If nAgen <= 0 Then nAgen = 0
If nPolicyIni <= 0 Then nPolicyIni = 0
If nPolicyFin <= 0 Then nPolicyFin = 0
If nTypeAmend <= 0 Then nTypeAmend = 0
If nIntermed <= 0 Then nIntermed = 0
If nEstadoag <= 0 Then nEstadoag = 0
If ntcnYear <= 0 Then ntcnYear = 0
If ncbeMonth <= 0 Then ncbeMonth = 0
If nModulec <= 0 Then nModulec = 0
%>
<HTML>
<HEAD>
	<SCRIPT>
	    //- Variable para el control de versiones
	    document.VssVersion = "$$Revision: 23 $|$$Date: 16-10-09 12:40 $|$$Author: Nmoreno $"
	</SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/Constantes.js"></SCRIPT>
	<META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>
	<%=mobjValues.StyleSheet()%>






</HEAD>

<BODY>
<% 
    Dim lclsGetsettings As New eRemoteDB.VisualTimeConfig
    mstrPath = lclsGetsettings.LoadSetting("LoadFile", "", "Paths")
    lclsGetsettings = Nothing

    If Not Session("bQuery") Or Request.QueryString.Item("nZone") = "1" Then
        If sCodispl = "CAL013" Then
            insUpLoadFileOri("c:\inetPub\")
        End If
        '+ Si no se han validado los campos de la página
        If Request.QueryString.Item("sCodisplReload") = vbNullString Then
            mstrErrors = insValPolicy
            Session("sErrorTable") = mstrErrors
            If sCodispl = "CAL013" Then
                Session("sForm") = "FIELDS=BYNARYREAD"
            Else
                Session("sForm") = Request.Form.ToString
            End If
        Else
            Session("sErrorTable") = vbNullString
            Session("sForm") = vbNullString
        End If
    End If
    If mstrErrors > vbNullString Then
        With Response
            .Write("<SCRIPT LANGUAGE=JAVASCRIPT>")
            .Write("ShowPopUp(""/VTimeNet/Common/Errors.aspx?sSource=" & Server.URLEncode(mstrCommand) & "&sQueryString=" & Server.URLEncode(Request.Params.Get("Query_String")) & """, ""PolicyRepErrors"",660,330);")
            .Write(mobjValues.StatusControl(False, Request.QueryString.Item("nZone"), Request.QueryString.Item("WindowType")))
            .Write("</SCRIPT>")
        End With
    Else
        If insPostPolicy Then
            If sCodispl = "CAL848" Then
                If Request.QueryString.Item("nZone") = "2" Then
                    Response.Write("<SCRIPT>top.document.location.reload();</SCRIPT>")
                Else
                    If Request.Form.Item("sCodisplReload") = vbNullString Then
                        Response.Write("<SCRIPT>self.history.go(-1);top.fraFolder.document.location=""" & Replace(UCase(sCodispl), "_K", "") & ".aspx?nMainAction=" & Request.QueryString.Item("nMainAction") & """;</SCRIPT>")
                    Else
                        Response.Write("<SCRIPT>window.close();opener.top.fraFolder.document.location=""" & Replace(UCase(sCodispl), "_K", "") & ".aspx?nMainAction=" & Request.QueryString.Item("nMainAction") & """;</SCRIPT>")
                    End If
                End If
                'ElseIf sCodispl = "CAL010" Then
                'Response.End()
            Else
                If sCodispl = "CAL0110" Then
                    If Request.QueryString.Item("nAction") = eFunctions.Menues.TypeActions.clngAcceptdatafinish Then
                        If Request.Form.Item("sCodisplReload") = vbNullString Then
                            Response.Write("<SCRIPT>top.document.location.reload();</SCRIPT>")
                        Else
                            Response.Write("<SCRIPT>window.close();opener.top.document.location.reload();</SCRIPT>")
                        End If
                    Else
                        If Request.QueryString.Item("nZone") = "1" Then

                            If Request.Form.Item("sCodisplReload") = vbNullString Then
                                '   Response.Write("<SCRIPT>top.fraFolder.document.location=""" & Replace(UCase(Request.QueryString.Item("sCodispl")), "_K", "") & ".aspx?nMainAction=" & Request.QueryString.Item("nMainAction") & mstrQueryString & """;</SCRIPT>")
                                Response.Write("<SCRIPT>top.fraFolder.document.location=""" & Replace(UCase(Request.QueryString.Item("sCodispl")), "_K", "") & ".aspx?nMainAction=" & Request.QueryString.Item("nMainAction") & mstrCommand & """;</SCRIPT>")
                            Else
                                Response.Write("<SCRIPT>window.close();opener.top.fraFolder.document.location=""" & Replace(UCase(Request.QueryString.Item("sCodispl")), "_K", "") & ".aspx?nMainAction=" & Request.QueryString.Item("nMainAction") & mstrCommand & """;</SCRIPT>")
                            End If
                        Else
                            Response.Write("<SCRIPT>top.document.location.reload();</SCRIPT>")
                            '                            If Request.QueryString.Item("nZone") = "1" Then
                            'If Request.Form.Item("sCodisplReload") = vbNullString Then
                            'Response.Write("<SCRIPT>top.fraFolder.document.location=""" & Replace(UCase(Request.QueryString.Item("sCodispl")), "_K", "") & ".aspx?nMainAction=" & Request.QueryString.Item("nMainAction") & mstrCommand & """;</SCRIPT>")
                            'Else
                            'Response.Write("<SCRIPT>window.close();opener.top.fraFolder.document.location=""" & Replace(UCase(Request.QueryString.Item("sCodispl")), "_K", "") & ".aspx?nMainAction=" & Request.QueryString.Item("nMainAction") & mstrCommand & """;</SCRIPT>")
                            'End If
                        End If
                    End If
                Else
                    If Request.Form.Item("sCodisplReload") = vbNullString Then
                        If mblnTimeOut Then
                            Response.Write("<SCRIPT>setTimeout('top.document.location.reload();',3000);</SCRIPT>")
                        Else
                            Response.Write("<SCRIPT>top.document.location.reload();</SCRIPT>")
                        End If
                    Else
                        Response.Write("<SCRIPT>window.close();opener.top.document.location.reload();</SCRIPT>")
                    End If
                End If
            End If
        Else
            Response.Write("<SCRIPT>alert('Problemas en la actualización');</SCRIPT>")
            Response.Write("<SCRIPT>top.document.location.reload();</SCRIPT>")
        End If
    End If

    mobjValues = Nothing
    mclsPolicy = Nothing
%>
    </BODY>
</HTML>

<%
lstrPre_def = Nothing
lstrIndMass = Nothing
%>






