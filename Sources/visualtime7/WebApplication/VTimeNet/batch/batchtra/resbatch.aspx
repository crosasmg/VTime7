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
Dim mobjDocuments2 As eCrystalExport.Export
Dim mobjDocuments As eReports.Report
Dim lclsBatchParam As eSchedule.Batch_param
Dim mstrField As String
Dim mlngBatch As Object
Dim mlngGroup As String
Dim mstrKey As String
Dim mstrInsGenVIL900_k As Object
Dim mobjInterface As eInterface.ValInterfaceSeq
Dim mlngSheet As String
Dim lclsCollectionRep As eCollection.CollectionRep
Dim lclsQuery As eRemoteDB.Query
'+ corrige problema e parametros  
Dim sFileName As String
Dim sIncrease As String
Dim mclsPolicyRep As ePolicy.ValPolicyRep
Dim sNameReport As Object
Dim bReport As Object
Dim lobjPolicy_His As Object
Dim lobjPolicyHis As Object    
</script>
<%Response.Expires = -1441
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

'+Se almacenan los paramtros de la página	
mstrField = Request.QueryString.Item("Field")
mlngBatch = Request.QueryString.Item("nBatch")
mlngGroup = Request.QueryString.Item("nGroup")
mstrKey = Request.QueryString.Item("sKey")
mlngSheet = Request.QueryString.Item("nsheet")

%>
<HTML>
<HEAD>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>


<SCRIPT>
//+ Variable para el control de versiones 
    document.VssVersion="$$Revision: 6 $|$$Date: 25-09-09 18:47 $|$$Author: Mpalleres $"
</SCRIPT>
<SCRIPT>

function openWindowChild(URL,left,width,height) 
{
	child = window.open();
	child.location.href = URL;
}
function AbrirArchivo(sfilename_aux)
{
	openWindowChild(sfilename_aux,'0','800','600');
	//window.location= sfilename_aux;
	
}
//-->	
</SCRIPT>

</HEAD>
<BODY>
	<FORM NAME="ShowDefValues">
	</FORM>
</BODY>
</HTML>
<%
    If mstrField = "BatchError" Then

        mobjDocuments = New eReports.Report
        With mobjDocuments
            .sCodispl = "BTC001"
            .ReportFilename = "Btc001Err.rpt"
            .setParamField(1, "Title", Request.QueryString.Item("sDescBatch"))
            .setStorProcParam(1, mstrKey)
            .bTimeOut = True
            Response.Write((.Command))
            .Reset()
            If mlngBatch = "150" Or mlngBatch = "113" Then
                lclsBatchParam = New eSchedule.Batch_param
                If lclsBatchParam.Find_Value(mstrKey, mlngBatch, 2) Then
                    If lclsBatchParam.Value(1) <> 4 Then
                        .sCodispl = "CAL013"
                        .ReportFilename = "CAL013A.rpt"
                        .setStorProcParam(1, lclsBatchParam.Value(2))
                        .setStorProcParam(2, lclsBatchParam.Value(1))
                        .setStorProcParam(3, .setdate(lclsBatchParam.Value(3)))
                        .setStorProcParam(4, lclsBatchParam.Value(4))
                        .bTimeOut = True
                        .nTimeOut = 3000
                        Response.Write((.Command))
                        .Reset()
                    End If
                    .sCodispl = "CAL013"
                    .ReportFilename = "CAL013.rpt"
                    .setStorProcParam(1, lclsBatchParam.Value(2))
                    .setStorProcParam(2, lclsBatchParam.Value(1))
                    .setStorProcParam(4, lclsBatchParam.Value(4))
                    .bTimeOut = True
                    .nTimeOut = 6000
                    Response.Write((.Command))
                    .Reset()
                End If
            ElseIf mlngBatch = "80359"
                If lclsBatchParam.Find_Value(mstrKey, mlngBatch, 2) Then
                    .sCodispl = "CAL013"
                    .ReportFilename = "CAL013A.rpt"
                    .setStorProcParam(1, lclsBatchParam.Value(2))
                    .setStorProcParam(2, 11)
                    .setStorProcParam(3, .setdate(Today))
                    .setStorProcParam(4, lclsBatchParam.Value(4))
                    .bTimeOut = True
                    .nTimeOut = 3000
                    Response.Write((.Command))
                    .Reset()

                    .sCodispl = "CAL013"
                    .ReportFilename = "CAL013.rpt"
                    .setStorProcParam(1, lclsBatchParam.Value(2))
                    .setStorProcParam(2, 11)
                    .setStorProcParam(4, lclsBatchParam.Value(4))
                    .bTimeOut = True
                    .nTimeOut = 6000
                    Response.Write((.Command))
                    .Reset()
                End If
            End If
        End With

        lclsBatchParam = Nothing
        mobjDocuments = Nothing

    ElseIf mstrField = "BatchResult" Then

        '+Se recuperan parametros del reporte
        lclsBatchParam = New eSchedule.Batch_param
        If lclsBatchParam.Find_Value(mstrKey, mlngBatch, 2) Then
            Select Case mlngBatch
                Case "1", "2", "3", "5", "6", "40"
                    mobjDocuments = New eReports.Report
                    With mobjDocuments
                        .sCodispl = "CPL999"
                        .ReportFilename = "CPL999_" & mlngBatch & "BTC.RPT"
                        .setStorProcParam(1, .setdate(lclsBatchParam.Value(1)))
                        .setStorProcParam(2, lclsBatchParam.Value(2))
                        Response.Write((.Command))
                        .Reset()

                        .ReportFilename = "CPL999ABTC.RPT"
                        .setStorProcParam(1, lclsBatchParam.Value(3))
                        .setStorProcParam(2, .setdate(lclsBatchParam.Value(1)))
                        Response.Write((.Command))
                        .Reset()

                        .ReportFilename = "CPL999BBTC.RPT"
                        .setStorProcParam(1, lclsBatchParam.Value(3))
                        .setStorProcParam(2, .setdate(lclsBatchParam.Value(1)))
                        Response.Write((.Command))

                    End With
                    mobjDocuments = Nothing

                Case "8"
                    mobjDocuments = New eReports.Report
                    With mobjDocuments
                        .sCodispl = "AGL009"
                        .ReportFilename = "AGL009BTC.rpt"
                        .setStorProcParam(1, lclsBatchParam.Value(1))
                        .setStorProcParam(2, lclsBatchParam.Value(2))
                        Response.Write((.Command))
                    End With
                    mobjDocuments = Nothing

                Case "60"
                    mobjDocuments = New eReports.Report
                    With mobjDocuments
                        .sCodispl = "COL686"
                        .ReportFilename = "COL686BTC.rpt"
                        .setParamField(1, "nTypeprocess", lclsBatchParam.Value(1))
                        .setStorProcParam(1, lclsBatchParam.Value(2))
                        .setStorProcParam(2, .setdate(lclsBatchParam.Value(3)))
                        .setStorProcParam(3, .setdate(lclsBatchParam.Value(4)))
                        Response.Write((.Command))
                    End With
                    mobjDocuments = Nothing

                Case "70"
                    mobjDocuments = New eReports.Report
                    With mobjDocuments
                        .sCodispl = "AGL7000"
                        .ReportFilename = "AGL7000.rpt"
                        .setStorProcParam(1, lclsBatchParam.Value(1))
                        .setStorProcParam(2, lclsBatchParam.Value(2))
                        '.setStorProcParam 3, lclsBatchParam.Value(3)
                        '.setStorProcParam 4, lclsBatchParam.Value(4)
                        Response.Write((.Command))
                    End With
                    mobjDocuments = Nothing

                Case "34"
                    mobjDocuments = New eReports.Report
                    With mobjDocuments
                        .sCodispl = "COL636"
                        .ReportFilename = "COL636btc.rpt"
                        .setParamField(1, "dProcDate", lclsBatchParam.Value(1))
                        .setStorProcParam(1, lclsBatchParam.Value(2))
                        Response.Write((.Command))
                    End With
                    mobjDocuments = Nothing

                Case "35"
                    mobjDocuments = New eReports.Report
                    With mobjDocuments
                        .sCodispl = "AGL728"
                        .ReportFilename = "AGL728.rpt"
                        .setStorProcParam(1, lclsBatchParam.Value(2))
                        .setStorProcParam(2, .setdate(lclsBatchParam.Value(1)))
                        Response.Write((.Command))
                    End With
                    mobjDocuments = Nothing

                Case "36", 76
                    mobjDocuments = New eReports.Report
                    With mobjDocuments
                        .sCodispl = "AGL605"
                        .ReportFilename = "AGL605.rpt"
                        .setParamField(1, "dEndDate", lclsBatchParam.Value(1))
                        .setParamField(1, "sOptProcess", lclsBatchParam.Value(2))
                        .setParamField(1, "nIntertyp", lclsBatchParam.Value(3))
                        .setParamField(1, "sKey", lclsBatchParam.Value(4))
                        Response.Write((.Command))
                    End With
                    mobjDocuments = Nothing

                Case "37", "77"
                    mobjDocuments = New eReports.Report
                    With mobjDocuments
                        .sCodispl = "AGL620"

                        If mlngBatch = 37 Then
                            .ReportFilename = "AGL620btc.rpt"
                        Else
                            .ReportFilename = "AGL620_1btc.rpt"
                        End If
                        .setStorProcParam(1, lclsBatchParam.Value(3))
                        .setStorProcParam(2, lclsBatchParam.Value(4))
                        .setStorProcParam(3, lclsBatchParam.Value(1))
                        .setStorProcParam(4, lclsBatchParam.Value(2))

                        Response.Write((.Command))
                    End With
                    mobjDocuments = Nothing

                Case "86"

                    Session("sKey") = mstrKey
                    Response.Write("<script>")
                    Response.Write("ShowPopUp('/VTimeNet/Interface/InterfaceSeq/GI1407.aspx?sCodispl=GI1407','EndProcess',1000,500);")
                    Response.Write("</" & "Script>")

                'mobjDocuments = New eReports.Report
                'With mobjDocuments
                '.sCodispl = "CAL006"
                ' .ReportFilename = "RPT_CAL006_1.rpt"
                '.setStorProcParam(1, lclsBatchParam.Value(1))
                '.setStorProcParam(2, .setdate(lclsBatchParam.Value(2)))
                '.setStorProcParam(3, lclsBatchParam.Value(3))
                '.bTimeOut = True
                '.nTimeOut = 3000
                'Response.Write((.Command))
                '.Reset()


                'End With
                'mobjDocuments = Nothing

                Case "100"
                    mobjDocuments = New eReports.Report
                    With mobjDocuments
                        .sCodispl = "CAL683"
                        .ReportFilename = "CAL683.rpt"
                        .setStorProcParam(1, lclsBatchParam.Value(1))
                        .setStorProcParam(2, lclsBatchParam.Value(2))
                        Response.Write((.Command))

                    End With
                    mobjDocuments = Nothing

                Case "101"

                    lclsCollectionRep = New eCollection.CollectionRep
                    Call lclsCollectionRep.valDataGenCOL500(lclsBatchParam.Value(1))
                    '+Generación de los archivos de resultado                    
                    Call lclsCollectionRep.insGenFilesCOL500(lclsBatchParam.Value(4), lclsBatchParam.Value(8), lclsBatchParam.Value(3), lclsBatchParam.Value(1), lclsBatchParam.Value(9))
                    '+Si hay alguna reporte que mostrar
                    If lclsCollectionRep.sProcess = "1" Or lclsCollectionRep.sNoProcess = "1" Then

                        mobjDocuments = New eReports.Report
                        With mobjDocuments
                            If lclsCollectionRep.sProcess = "1" Then
                                If lclsCollectionRep.sFileName = "" Then
                                    sFileName = " "
                                Else
                                    sFileName = lclsCollectionRep.sFileName
                                End If
                                If lclsBatchParam.Value(5) = "" Then
                                    sIncrease = " "
                                Else
                                    sIncrease = lclsBatchParam.Value(5)
                                End If
                                '+ Reporte de Información procesada
                                .sCodispl = "COL500"
                                .ReportFilename = "COL500btc.rpt"
                                .setParamField(1, "sFileName", sFileName)
                                .setParamField(2, "sOptGenera", lclsBatchParam.Value(2))
                                .setParamField(3, "sExpirdate", lclsBatchParam.Value(4))
                                .setParamField(4, "sIncrease", sIncrease)
                                .setStorProcParam(5, lclsBatchParam.Value(1)) '+sKey
                                .setStorProcParam(6, lclsBatchParam.Value(6)) '+optProcess
                                .setStorProcParam(7, lclsBatchParam.Value(7)) '+optCurrency


                                Response.Write((.Command))
                                .Reset()
                                '+ Reporte Resumen Proceso de Generación de Cobranza
                                .sCodispl = "COL500"
                                .ReportFilename = "COL500_RES.rpt"
                                .setStorProcParam(1, lclsBatchParam.Value(1))
                                .setStorProcParam(2, .setdate(lclsBatchParam.Value(4)))
                                Response.Write((.Command))
                                .Reset()
                            End If

                            If lclsBatchParam.Value(6) = "2" Then '+Definitiva
                                Select Case mobjValues.StringToType(lclsBatchParam.Value(8), eFunctions.Values.eTypeData.etdDouble, True) '+WayPay
                                    ' PAC y PAT(Transbank)
                                    Case 1, 2
                                        '+ Reporte de Errores generados  
                                        '									If lclsCollectionRep.sNoProcess = "1" Then
                                        '                                        .sCodispl = "COL500"
                                        '                                       .ReportFilename = "COL500Abtc.rpt"
                                        '                                      .setStorProcParam(1, lclsBatchParam.Value(1))
                                        '                                     Response.Write((.Command))
                                        '							End If

                                        ' Convenio
                                    Case 3
                                        '+ Planilla: Reporte de cargos realizados  
                                        If lclsCollectionRep.sProcess = "1" Then
                                            '+ Planilla convenio convencional            							
                                            If Right(lclsCollectionRep.sAgreeApvSef, 1) = "1" Then
                                                .sCodispl = "COL500"
                                                .ReportFilename = "COL500Bbtc.rpt"
                                                .setStorProcParam(1, lclsBatchParam.Value(1))
                                                .setStorProcParam(2, lclsBatchParam.Value(7))
                                                Response.Write((.Command))
                                                .Reset()

                                                '.sCodispl = "COL500"
                                                '.ReportFilename = "COL701a.rpt"
                                                '.setStorProcParam(1, lclsBatchParam.Value(8))
                                                '.setStorProcParam(2, lclsBatchParam.Value(3))
                                                '.setStorProcParam(3, lclsBatchParam.Value(10))
                                                '.setStorProcParam(4, .setdate(lclsBatchParam.Value(4)))
                                                '.setStorProcParam(5, vbNullString)
                                                '.setStorProcParam(6, vbNullString)
                                                '.setStorProcParam(7, "1")
                                                '.setStorProcParam(8, "1")
                                                '.setStorProcParam(9, mstrKey)
                                                '.setStorProcParam(10, Session("nUsercode"))
                                                'Response.Write(.Command)
                                                '.Reset()
                                            End If
                                            '+ Planilla: Reporte de Comprobante de pago APV  
                                            If Left(lclsCollectionRep.sAgreeApvSef, 1) = "1" Then
                                                .ReportFilename = "COL500Ebtc.rpt"
                                                .setStorProcParam(1, lclsBatchParam.Value(1))
                                                .setStorProcParam(2, .setdate(lclsBatchParam.Value(4)))
                                                Response.Write((.Command))
                                                .Reset()
                                            End If
                                            '+ Planilla: Reporte de Comprobante depago SEF  		         		
                                            If Mid(lclsCollectionRep.sAgreeApvSef, 2, 1) = "1" Then
                                                .ReportFilename = "COL500Fbtc.rpt"
                                                .setStorProcParam(1, lclsBatchParam.Value(1))
                                                .setStorProcParam(2, .setdate(lclsBatchParam.Value(4)))
                                                Response.Write((.Command))
                                            End If
                                        End If
                                    ' Aviso
                                    Case 4
                                        '+ Boletin: Reporte de Avisos de Cobranza 
                                        If lclsCollectionRep.sProcess = "1" Or lclsCollectionRep.sNoProcess = "1" Then
                                            .sCodispl = "COL500"
                                            If mobjValues.insGetSetting("Active", "No", "CustomBillingNotice").ToUpper = "YES" Then
                                                .ReportFilename = "COL500_CUPON.rpt"

                                                .setStorProcParam(1, mstrKey)
                                            Else
                                                .ReportFilename = "COL701a.rpt"

                                                .setStorProcParam(1, lclsBatchParam.Value(8))
                                                .setStorProcParam(2, lclsBatchParam.Value(3))
                                                .setStorProcParam(3, lclsBatchParam.Value(10))
                                                .setStorProcParam(4, .setdate(lclsBatchParam.Value(4)))
                                                .setStorProcParam(5, vbNullString)
                                                .setStorProcParam(6, vbNullString)
                                                .setStorProcParam(7, "1")
                                                .setStorProcParam(8, "1")
                                                .setStorProcParam(9, mstrKey)
                                                .setStorProcParam(10, Session("nUsercode"))

                                            End If

                                            Response.Write(.Command)
                                            .Reset()
                                        End If
                                End Select
                            End If
                        End With
                        mobjDocuments = Nothing
                    Else
                        lclsQuery = New eRemoteDB.Query
                        If lclsQuery.OpenQuery("Message", "sMessaged", "nErrornum = 20024") Then
                            Response.Write("<SCRIPT>alert('" & lclsQuery.FieldToClass("sMessaged") & "');</SCRIPT>")
                        End If
                        lclsQuery = Nothing
                    End If

                Case "102"

                    mobjDocuments = New eReports.Report
                    lclsCollectionRep = New eCollection.CollectionRep

                    Call lclsCollectionRep.valDataGenCOL502(lclsBatchParam.Value(1))
                    '+Si hay alguna reporte que mostrar
                    If lclsCollectionRep.sProcess = "1" Or lclsCollectionRep.sNoProcess = "1" Then

                        With mobjDocuments
                            .sCodispl = "COL502"
                            .ReportFilename = "COL502btc.rpt"
                            .setStorProcParam(1, lclsBatchParam.Value(1))
                            .setStorProcParam(2, 1) '+Realizadas
                            Response.Write((.Command))
                            .Reset()
                        End With

                        If lclsCollectionRep.insGenFilesCOL502(lclsBatchParam.Value(1)) Then
                            '+Generación del archivo excel para imputaciones rechazadas
                            Response.Write("<SCRIPT>AbrirArchivo('" & lclsCollectionRep.sFileName & "');</SCRIPT>")
                        Else
                            Response.Write("<SCRIPT>alert('No se generó archivo de Rechazos');</SCRIPT>")
                        End If
                    Else
                        lclsQuery = New eRemoteDB.Query
                        If lclsQuery.OpenQuery("Message", "sMessaged", "nErrornum = 20024") Then
                            Response.Write("<SCRIPT>alert('" & lclsQuery.FieldToClass("sMessaged") & "');</SCRIPT>")
                        End If
                        lclsQuery = Nothing
                    End If
                    lclsCollectionRep = Nothing
                    mobjDocuments = Nothing


                Case "103"
                    mobjDocuments = New eReports.Report
                    With mobjDocuments
                        .sCodispl = "CAL712"
                        .ReportFilename = "CAL712btc.rpt"
                        .setStorProcParam(1, lclsBatchParam.Value(1))
                        Response.Write((.Command))
                    End With
                    mobjDocuments = Nothing

                Case "104"
                    mobjDocuments = New eReports.Report
                    With mobjDocuments
                        .sCodispl = "COL009"
                        .ReportFilename = "COL009.rpt"
                        .setParamField(1, "nTypeProce", lclsBatchParam.Value(1))
                        .setParamField(2, "dDateProcec", .setdate(lclsBatchParam.Value(2)))
                        .setStorProcParam(1, lclsBatchParam.Value(3))
                        Response.Write((.Command))
                    End With
                    mobjDocuments = Nothing

                Case "105"
                    mobjDocuments = New eReports.Report
                    With mobjDocuments
                        .sCodispl = "VAL696"
                        .ReportFilename = "VAL696btc.rpt"
                        .setStorProcParam(1, .setdate(lclsBatchParam.Value(1)))
                        .setStorProcParam(2, lclsBatchParam.Value(2))
                        .setStorProcParam(3, lclsBatchParam.Value(3))
                        Response.Write((.Command))
                    End With
                    mobjDocuments = Nothing

                Case "106"
                    mobjDocuments = New eReports.Report
                    With mobjDocuments
                        .sCodispl = "VIL7002"
                        .ReportFilename = "VIL7002.rpt"
                        .setStorProcParam(1, lclsBatchParam.Value(1))
                        Response.Write((.Command))
                    End With
                    mobjDocuments = Nothing

                Case "108"
                    mobjDocuments = New eReports.Report
                    With mobjDocuments
                        .sCodispl = "AGL002"
                        .ReportFilename = "AGL002btc.rpt"
                        .setStorProcParam(1, lclsBatchParam.Value(1))
                        .setStorProcParam(2, lclsBatchParam.Value(2))
                        .setStorProcParam(3, lclsBatchParam.Value(3))
                        Response.Write((.Command))

                    End With
                    mobjDocuments = Nothing

                Case "109"
                    mobjDocuments = New eReports.Report
                    With mobjDocuments
                        .sCodispl = "VAL601"
                        .ReportFilename = "VAL601btc.rpt"
                        .setStorProcParam(1, lclsBatchParam.Value(1))
                        .setStorProcParam(2, lclsBatchParam.Value(2))
                        .setStorProcParam(3, lclsBatchParam.Value(3))
                        Response.Write((.Command))
                        .Reset()
                        .sCodispl = "VAL601"
                        .ReportFilename = "VAL852.rpt"
                        .setStorProcParam(1, lclsBatchParam.Value(3))
                        Response.Write((.Command))
                    End With
                    mobjDocuments = Nothing

                Case "110"
                    mobjDocuments = New eReports.Report
                    With mobjDocuments
                        .sCodispl = "CAL005"
                        .ReportFilename = "CAL005.rpt"
                        .setStorProcParam(1, lclsBatchParam.Value(4))
                        .setStorProcParam(2, lclsBatchParam.Value(5))
                        .setStorProcParam(3, lclsBatchParam.Value(6))
                        .setStorProcParam(4, lclsBatchParam.Value(3))
                        Response.Write((.Command))
                        '        				.setParamField 1,"dStartDate", .setdate(lclsBatchParam.Value(1))
                        '       				.setParamField 2,"dEndDate", .setdate(lclsBatchParam.Value(1))

                    End With
                    mobjDocuments = Nothing

                Case "111"
                    mobjDocuments = New eReports.Report
                    With mobjDocuments
                        .sCodispl = "VAL633"
                        .ReportFilename = "VAL633btc.rpt"
                        .setParamField(1, "dFromDate", lclsBatchParam.Value(1))
                        .setParamField(2, "dToDate", lclsBatchParam.Value(2))
                        .setStorProcParam(1, lclsBatchParam.Value(3))
                        Response.Write((.Command))

                        .Reset()
                        'mblnTimeOut = True
                        .sCodispl = "VAL633"
                        .ReportFilename = "VAL633_Er.rpt"
                        .setStorProcParam(1, lclsBatchParam.Value(3))
                        Response.Write((.Command))

                    End With
                    mobjDocuments = Nothing

                Case "113"
                    mobjDocuments = New eReports.Report
                    With mobjDocuments
                        If lclsBatchParam.Value(1) <> 4 Then
                            .sCodispl = "CAL013"
                            .ReportFilename = "CAL013A.rpt"
                            .setStorProcParam(1, lclsBatchParam.Value(2))
                            .setStorProcParam(2, lclsBatchParam.Value(1))
                            .setStorProcParam(3, .setdate(lclsBatchParam.Value(3)))
                            .setStorProcParam(4, lclsBatchParam.Value(4))
                            .bTimeOut = True
                            Response.Write((.Command))
                            .Reset()
                        End If
                        .sCodispl = "CAL013"
                        .ReportFilename = "CAL013.rpt"
                        .setStorProcParam(1, lclsBatchParam.Value(2))
                        .setStorProcParam(2, lclsBatchParam.Value(1))
                        .setStorProcParam(4, lclsBatchParam.Value(4))
                        .bTimeOut = True
                        .nTimeOut = 3000
                        Response.Write((.Command))
                        .Reset()
                    End With
                    mobjDocuments = Nothing

                Case "80359"
                    mobjDocuments = New eReports.Report
                    With mobjDocuments
                        .sCodispl = "CAL013"
                        .ReportFilename = "CAL013A.rpt"
                        .setStorProcParam(1, lclsBatchParam.Value(2))
                        .setStorProcParam(2, 11)
                        .setStorProcParam(3, .setdate(Today))
                        .setStorProcParam(4, lclsBatchParam.Value(4))
                        .bTimeOut = True
                        Response.Write((.Command))
                        .Reset()

                        .sCodispl = "CAL013"
                        .ReportFilename = "CAL013.rpt"
                        .setStorProcParam(1, lclsBatchParam.Value(2))
                        .setStorProcParam(2, 11)
                        .setStorProcParam(4, lclsBatchParam.Value(4))
                        .bTimeOut = True
                        .nTimeOut = 3000
                        Response.Write((.Command))
                        .Reset()
                    End With
                    mobjDocuments = Nothing
                Case "112"
                    mobjDocuments = New eReports.Report
                    With mobjDocuments
                        .sCodispl = "VAL708"
                        .ReportFilename = "VAL708btc.rpt"
                        .setParamField(1, "lMonth", lclsBatchParam.Value(1))
                        .setParamField(2, "lYear", lclsBatchParam.Value(2))
                        .setParamField(3, "lProc", lclsBatchParam.Value(3))
                        .setStorProcParam(1, lclsBatchParam.Value(4))
                        Response.Write((.Command))
                    End With
                    mobjDocuments = Nothing

                Case "114"
                    mobjDocuments = New eReports.Report
                    With mobjDocuments
                        .sCodispl = "VIL702"
                        .ReportFilename = "VIL702btc.rpt"
                        .setStorProcParam(1, lclsBatchParam.Value(1))
                        Response.Write((.Command))
                    End With
                    mobjDocuments = Nothing

                Case "116"
                    mobjDocuments = New eReports.Report
                    '+Generación de los archivos de resultado                                        
                    lclsCollectionRep = New eCollection.CollectionRep

                    Call lclsCollectionRep.insGenFilesCOL556(lclsBatchParam.Value(1))

                    With mobjDocuments
                        .sCodispl = "COL556"
                        .ReportFilename = "COL556.rpt"
                        .setStorProcParam(1, lclsBatchParam.Value(1))
                        .setStorProcParam(2, 1) '+Procesados
                        .setParamField(1, "sFileName", (lclsCollectionRep.sFileName))
                        Response.Write((.Command))
                        .Reset()

                        .sCodispl = "COL556"
                        .ReportFilename = "COL556.rpt"
                        .setStorProcParam(1, lclsBatchParam.Value(1))
                        .setStorProcParam(2, 0) '+Incidencias
                        .setParamField(1, "sFileName", (lclsCollectionRep.sFileName1))
                        '+Se dejan 5 segundos de espera porque, como es el mismo reporte, servidor
                        '+de reportes piensa que se está recargando el anterior y da mensaje de error
                        .bTimeOut = True
                        .nTimeOut = 5000
                        Response.Write((.Command))
                    End With
                    lclsCollectionRep = Nothing
                    mobjDocuments = Nothing

                Case "118"

                    mobjDocuments = New eReports.Report
                    With mobjDocuments
                        If lclsBatchParam.Value(1) <> 4 And lclsBatchParam.Value(1) <> 9 Then

                            If lclsBatchParam.Value(2) = 1 Then
                                .sCodispl = "OPL719"
                                .ReportFilename = "OPL719btc.rpt"
                                .setStorProcParam(1, lclsBatchParam.Value(3))
                                Response.Write((.Command))
                                .Reset()

                                '+Ingreso de recaudación de primas registradas.
                                .ReportFilename = "OPL719_det1btc.rpt"
                                .sCodispl = "OPL719"
                                .setStorProcParam(1, lclsBatchParam.Value(3))
                                Response.Write((.Command))
                                .Reset()

                                '+Detalle de pagos ingresados.       	            
                                .ReportFilename = "OPL719_det2btc.rpt"
                                .sCodispl = "OPL719"
                                .setStorProcParam(1, lclsBatchParam.Value(3))
                                Response.Write((.Command))
                                .Reset()

                                '+Detalle de depósitos ingresados.    
                                .ReportFilename = "OPL719_det3btc.rpt"
                                .sCodispl = "OPL719"
                                .setStorProcParam(1, lclsBatchParam.Value(3))
                                Response.Write((.Command))
                                .Reset()

                                '+Detalle de ingresos no operacionales.    				
                                .ReportFilename = "OPL719_det4btc.rpt"
                                .sCodispl = "OPL719"
                                .setStorProcParam(1, lclsBatchParam.Value(3))
                                Response.Write((.Command))
                                .Reset()

                                '+Detalle de diferencias.    				
                                .ReportFilename = "OPL719_det5btc.rpt"
                                .sCodispl = "OPL719"
                                .setStorProcParam(1, lclsBatchParam.Value(3))
                                Response.Write((.Command))
                                .Reset()

                                '+Detalle de ingresos operacionales.    				
                                .ReportFilename = "OPL719_det6btc.rpt"
                                .sCodispl = "OPL719"
                                .setStorProcParam(1, lclsBatchParam.Value(3))
                                Response.Write((.Command))
                            End If
                        Else
                            Response.Write("<SCRIPT>alert('Modo de ejecución seleccionada no genera reportes')</SCRIPT>")
                        End If
                    End With
                    mobjDocuments = Nothing

                Case "119"
                    mobjDocuments = New eReports.Report
                    With mobjDocuments
                        .sCodispl = "VIL733"
                        .ReportFilename = "VIL733btc.rpt"
                        .setStorProcParam(1, lclsBatchParam.Value(1))
                        .setStorProcParam(2, .setdate(lclsBatchParam.Value(2)))
                        .setStorProcParam(3, lclsBatchParam.Value(3))
                        Response.Write((.Command))
                    End With
                    mobjDocuments = Nothing

                Case "121"
                    mobjDocuments = New eReports.Report
                    With mobjDocuments
                        .sCodispl = "SIL704"
                        .ReportFilename = "SIL704.rpt"
                        .setStorProcParam(1, lclsBatchParam.Value(1))
                        .setStorProcParam(2, .setdate(lclsBatchParam.Value(2)))
                        .setStorProcParam(3, .setdate(lclsBatchParam.Value(3)))
                        .setStorProcParam(4, lclsBatchParam.Value(4))
                        Response.Write((.Command))
                    End With
                    mobjDocuments = Nothing

                Case "122"
                    mobjDocuments = New eReports.Report
                    With mobjDocuments
                        .sCodispl = "SIL705"
                        .ReportFilename = "SIL705.RPT"
                        .setStorProcParam(1, lclsBatchParam.Value(1))
                        Response.Write((.Command))
                    End With
                    mobjDocuments = Nothing

                Case "124"
                    mobjDocuments = New eReports.Report
                    With mobjDocuments
                        .sCodispl = "COL507"
                        .ReportFilename = "COL507btc.rpt"
                        .setParamField(1, "sDate_pay", lclsBatchParam.Value(1))
                        .setStorProcParam(1, lclsBatchParam.Value(2))
                        Response.Write((.Command))
                    End With
                    mobjDocuments = Nothing

                Case "125"
                    mobjDocuments = New eReports.Report
                    With mobjDocuments
                        .sCodispl = "CAL002"
                        .ReportFilename = "CAL002btc.rpt"
                        .setParamField(1, "nInsur_area", lclsBatchParam.Value(1))
                        .setStorProcParam(1, lclsBatchParam.Value(2))
                        Response.Write((.Command))
                    End With
                    mobjDocuments = Nothing

                Case "126"
                    mobjDocuments = New eReports.Report
                    With mobjDocuments
                        .sCodispl = "CAL908"
                        .ReportFilename = "CAL908.rpt"
                        .setStorProcParam(1, lclsBatchParam.Value(1))
                        .setStorProcParam(2, lclsBatchParam.Value(2))
                        .setStorProcParam(3, lclsBatchParam.Value(3))
                        Response.Write((.Command))
                    End With
                    mobjDocuments = Nothing

                Case "127"
                    mobjDocuments = New eReports.Report
                    With mobjDocuments
                        .sCodispl = "CAL671"
                        .ReportFilename = "CAL671.rpt"
                        .setStorProcParam(1, lclsBatchParam.Value(1))
                        Response.Write((.Command))
                    End With
                    mobjDocuments = Nothing

                Case "128"
                    mobjDocuments = New eReports.Report
                    With mobjDocuments
                        .sCodispl = "VAL709"
                        .ReportFilename = "VAL709.rpt"
                        .setStorProcParam(1, lclsBatchParam.Value(1))
                        .setParamField(1, "nProcType", lclsBatchParam.Value(2))
                        .setParamField(2, "dProcDate", lclsBatchParam.Value(3))
                        Response.Write((.Command))
                    End With
                    mobjDocuments = Nothing


                Case "129"
                    mobjDocuments = New eReports.Report
                    With mobjDocuments
                        .sCodispl = "COL504"
                        'cambio en el formato del libro
                        '.ReportFilename = "COL504.rpt"
                        .ReportFilename = "COL504B.rpt"
                        .setStorProcParam(1, lclsBatchParam.Value(1))
                        .setStorProcParam(2, lclsBatchParam.Value(2))
                        .setStorProcParam(3, lclsBatchParam.Value(3))
                        .setStorProcParam(4, lclsBatchParam.Value(4))
                        Response.Write((.Command))
                    End With
                    mobjDocuments = Nothing

                Case "132"
                    mobjDocuments = New eReports.Report
                    With mobjDocuments
                        .sCodispl = "CAL503"
                        .ReportFilename = "CAL503.rpt"
                        .setStorProcParam(1, lclsBatchParam.Value(1))
                        Response.Write((.Command))
                    End With
                    mobjDocuments = Nothing


                Case "134"
                    mobjDocuments = New eReports.Report
                    With mobjDocuments
                        .sCodispl = "AGL776"
                        .ReportFilename = "AGL776.rpt"
                        .setStorProcParam(1, lclsBatchParam.Value(1))
                        Response.Write((.Command))
                    End With
                    mobjDocuments = Nothing


                Case "150"
                    mobjDocuments = New eReports.Report
                    With mobjDocuments
                        If lclsBatchParam.Value(1) <> 4 Then
                            .sCodispl = "CAL013"
                            .ReportFilename = "CAL013A.rpt"
                            .setStorProcParam(1, lclsBatchParam.Value(2))
                            .setStorProcParam(2, lclsBatchParam.Value(1))
                            .setStorProcParam(3, .setdate(lclsBatchParam.Value(3)))
                            .bTimeOut = True
                            Response.Write((.Command))
                            .Reset()
                        End If
                        .sCodispl = "CAL013"
                        .ReportFilename = "CAL013.rpt"
                        .setStorProcParam(1, lclsBatchParam.Value(2))
                        .setStorProcParam(2, lclsBatchParam.Value(1))
                        .bTimeOut = True
                        .nTimeOut = 3000
                        Response.Write((.Command))
                        .Reset()

                        .sCodispl = "BTC001"
                        .ReportFilename = "Btc001Err.rpt"
                        .setParamField(1, "Title", Request.QueryString.Item("sDescBatch"))
                        .setStorProcParam(1, mstrKey)
                        .bTimeOut = True
                        .nTimeOut = 6000
                        Response.Write((.Command))
                    End With
                    mobjDocuments = Nothing

                Case "151"
                    mobjDocuments = New eReports.Report
                    With mobjDocuments
                        .sCodispl = "SI957"
                        .ReportFilename = "SIL957.rpt"
                        .setStorProcParam(1, lclsBatchParam.Value(1))
                        .setStorProcParam(2, lclsBatchParam.Value(2))
                        .setStorProcParam(3, .setdate(lclsBatchParam.Value(3)))
                        Response.Write((.Command))
                    End With
                    mobjDocuments = Nothing

                Case "154"
                    mobjDocuments = New eReports.Report
                    With mobjDocuments
                        .sCodispl = "CAL013"
                        .ReportFilename = "CAL013_1.rpt"
                        .setStorProcParam(1, lclsBatchParam.Value(1))
                        Response.Write((.Command))
                    End With
                    mobjDocuments = Nothing

                Case "300"

                    mclsPolicyRep = New ePolicy.ValPolicyRep
                    '+Generación de los archivos de resultado                    
                    Call mclsPolicyRep.insGenVIL900_k(mstrKey)
                    '+Muestra el Archivo *.txt recien generado                    
                    Response.Write("<SCRIPT>window.open('/VTimeNet/Tfiles/" & mclsPolicy.sFile_Name & "', 'Archivo', 'toolbar=yes,location=no,directories=no,status=yes,menubar=yes,scrollbars=yes,copyhistory=no,resizable=yes,width=300,height=300,left=0,top=0',false);</SCRIPT>")
                    mclsPolicyRep = Nothing

                Case "704", "705", "801", "802", "803", "804", "805"
                    mobjDocuments = New eReports.Report
                    With mobjDocuments
                        .sCodispl = "GI1405"
                        .ReportFilename = "GIL1405.rpt"
                        .setStorProcParam(1, lclsBatchParam.Value(1))
                        Response.Write((.Command))
                    End With
                    mobjDocuments = Nothing
                '+Relación de cesiones de prima                
                Case "44"
                    mobjDocuments = New eReports.Report
                    With mobjDocuments
                        .sCodispl = "CRL004"
                        .ReportFilename = "CRL004.rpt"
                        .setStorProcParam(1, lclsBatchParam.Value(1))
                        Response.Write((.Command))
                    End With
                    mobjDocuments = Nothing

                Case "1402"
                    mobjDocuments = New eReports.Report
                    mobjInterface = New eInterface.ValInterfaceSeq
                    ' revisa para deplegar reporte de error
                    If mobjInterface.insReportError(mstrKey, CInt(mlngSheet)) Then
                        With mobjDocuments
                            .Reset()
                            .sCodispl = "GI1405"
                            .ReportFilename = "GIL1405.rpt"
                            .setStorProcParam(1, mstrKey)
                            Response.Write((.Command))
                        End With
                    End If
                    ' revisa para deplegar reporte de loog del proceso
                    If mobjInterface.insReport(mstrKey, CInt(mlngSheet)) Then
                        With mobjDocuments
                            .Reset()
                            .sCodispl = "GI1405_2"
                            .ReportFilename = "GIL1405_1.rpt"
                            .setStorProcParam(1, mstrKey)
                            .setStorProcParam(2, (mobjInterface.sDescript))
                            Response.Write((.Command))
                        End With
                    End If

                    mobjInterface = Nothing
                    mobjDocuments = Nothing
                    Session("sKey") = lclsBatchParam.Value(1)

                    Response.Write("<SCRIPT>")
                    Response.Write("ShowPopUp('/VTimeNet/Interface/InterfaceSeq/GI1407.aspx?sCodispl=GI1407','EndProcess',1000,500);")
                    Response.Write("</" & "Script>")

                Case "163"
                    mobjDocuments = New eReports.Report
                    ' revisa para deplegar reporte de error
                    With mobjDocuments
                        .sCodispl = "GI1405"
                        .ReportFilename = "GIL1405.rpt"
                        .setStorProcParam(1, "T2009092518500902216") 'lclsBatchParam.Value(1)
                        .setStorProcParam(2, "Reporte")
                        Response.Write((.Command))
                    End With

                '+Cartola APV
                Case "1486"
                    mobjDocuments = New eReports.Report

                    Dim lclsTmp_VIl7700_APV As ePolicy.Tmp_Vil7700
                    Dim lclsTmp_VIL7700s_APV As ePolicy.Tmp_Vil7700s
                    lclsTmp_VIl7700_APV = New ePolicy.Tmp_Vil7700
                    lclsTmp_VIL7700s_APV = New ePolicy.Tmp_Vil7700s

                    If lclsTmp_VIL7700s_APV.Find(mstrKey, True) Then
                        For Each lclsTmp_VIl7700_APV In lclsTmp_VIL7700s_APV
                            With mobjDocuments
                                .sCodispl = "VIL1486"
                                .ReportFilename = "VIL1486.rpt"
                                .setStorProcParam(1, lclsTmp_VIl7700_APV.nBranch)
                                .setStorProcParam(2, lclsTmp_VIl7700_APV.nProduct)
                                .setStorProcParam(3, lclsTmp_VIl7700_APV.nPolicy)
                                .setStorProcParam(4, lclsBatchParam.Value(1))

                                Response.Write((.Command))
                                .Reset()

                            End With

                        Next
                        mobjDocuments = Nothing
                    End If


                '--------
                '+Cartola CUI
                Case "1488"
                    mobjDocuments = New eReports.Report
                    bReport = False

                    'mobjDocuments2 = New eCrystalExport.Export
                    With mobjDocuments
                        .Reset()
                        .sCodispl = "VIL1488"
                        .ReportFilename = "VIL1488.rpt"

                        .setStorProcParam(1, lclsBatchParam.Value(4))
                        .setStorProcParam(2, lclsBatchParam.Value(5))
                        .setStorProcParam(3, lclsBatchParam.Value(6))
                        .setStorProcParam(4, lclsBatchParam.Value(7))
                        .setStorProcParam(5, "")
                        .setStorProcParam(6, lclsBatchParam.Value(1))
                        .setStorProcParam(7, lclsBatchParam.Value(1))
                        .setStorProcParam(8, lclsBatchParam.Value(1))
                        .setStorProcParam(9, "2")
                        .setStorProcParam(10, lclsBatchParam.Value(3))
                        .setStorProcParam(11, lclsBatchParam.Value(10))
                        .setStorProcParam(12, lclsBatchParam.Value(2))
                        .setStorProcParam(13, lclsBatchParam.Value(9))
                        .setStorProcParam(14, "0")
                        .setStorProcParam(15, lclsBatchParam.Value(8))
                        .sCodispl = "VIL1488"
                        .sCartol = "1"
                        .nCartol = "1"
                        .MergeCartol = "1"
                        ' .bPuntual = True
                        .Merge = False
                        .nGenPolicy = 1
                        .MergeCertype = "2"
                        .nForzaRep = 1
                        .nTratypep = 2
                        .nCopyPolicy = 1
                        .MergeCertype = "2"
                        If Not IsNumeric(lclsBatchParam.Value(7))  Then
                            .MergePolicy = 0
                            bReport = True
                        Else
                            .MergePolicy = lclsBatchParam.Value(7)
                        End If
                        If .MergePolicy <> 0 Then
                            .MergeBranch = lclsBatchParam.Value(5)
                            .MergeProduct = lclsBatchParam.Value(6)
                            .MergePolicy = lclsBatchParam.Value(7)
                            .MergeCertif = 0
                            lobjPolicy_His = New ePolicy.Policy
                            lobjPolicy_His.Find("2", lclsBatchParam.Value(5), lclsBatchParam.Value(6), lclsBatchParam.Value(7), True)
                            .nMovement = lobjPolicy_His.nMov_histor
                            lobjPolicy_His = Nothing
                            Response.Write((.Command))

                        End If
                        sNameReport = .sNameReport

                        If .MergePolicy = 0 Then

                            mobjDocuments2 = New eCrystalExport.Export

                        End If

                        Dim lclsTmp_VIl7700_APV As New ePolicy.Tmp_Vil7700
                        Dim lclsTmp_VIL7700s_APV As New ePolicy.Tmp_Vil7700s

                        If lclsBatchParam.Value(9) = "VIL1488" And lclsBatchParam.Value(8) = "1"  And lclsTmp_VIL7700s_APV.Find(mstrKey, True) Then

                            For Each lclsTmp_VIl7700_APV In lclsTmp_VIL7700s_APV
                                Dim mobjDocuments2 As New eCrystalExport.Export

                                With mobjDocuments2

                                    .sCertype = "2"
                                    .nBranch = lclsTmp_VIl7700_APV.nBranch
                                    .nProduct = lclsTmp_VIl7700_APV.nProduct
                                    .nPolicy = lclsTmp_VIl7700_APV.nPolicy
                                    .nCertif = 0
                                    .nCopyPolicy = 1
                                    .nGenPolicy = 1
                                    .sPolitype = "1"
                                    'oHelper.sCartol = Request.QueryString("MergeCartol")
                                    .nCartol = lclsTmp_VIl7700_APV.NCARTPOL

                                    .DBParameters.Add(lclsTmp_VIl7700_APV.nNumcart)
                                    .DBParameters.Add(lclsTmp_VIl7700_APV.nBranch)
                                    .DBParameters.Add(lclsTmp_VIl7700_APV.nProduct)
                                    .DBParameters.Add(lclsTmp_VIl7700_APV.nPolicy)
                                    .DBParameters.Add(lclsTmp_VIl7700_APV.NCARTPOL)
                                    .DBParameters.Add(lclsBatchParam.Value(1))
                                    .DBParameters.Add(lclsBatchParam.Value(1))
                                    .DBParameters.Add(lclsBatchParam.Value(1))
                                    .DBParameters.Add("2")
                                    .DBParameters.Add(lclsBatchParam.Value(3))
                                    .DBParameters.Add(lclsBatchParam.Value(10))
                                    .DBParameters.Add(lclsBatchParam.Value(2))
                                    .DBParameters.Add(lclsBatchParam.Value(9))
                                    .DBParameters.Add(0)
                                    .DBParameters.Add(lclsBatchParam.Value(8))


                                    lobjPolicy_His = New ePolicy.Policy
                                    lobjPolicy_His.Find("2", lclsTmp_VIl7700_APV.nBranch, lclsTmp_VIl7700_APV.nProduct, lclsTmp_VIl7700_APV.nPolicy, True)

                                    .nMovement = lobjPolicy_His.nMov_histor
                                    sNameReport = .sNameReport
                                    lobjPolicy_His = Nothing
                                    .sCodispl = "VIL7700"
                                    .ReportFilename = "VIL1488.rpt"
                                    .RealExport("C:\VisualTimeNet\WebApplication\VTimeNet\reports\VIL1488.rpt", "VIL1488.rpt", "PDF", lclsBatchParam.Value(1), "", Session("sInitialsCon"), Session("sAccesswoCon"))

                                    mobjDocuments = Nothing

                                End With
                            Next lclsTmp_VIl7700_APV
                            lclsTmp_VIL7700s_APV.UpdateProcess(lclsBatchParam.Value(1))


                        End If
                    End With
                    mobjDocuments2 = Nothing
                    mobjDocuments = Nothing
                    'Se muetra reporte con el total de las cartolas emitidas
                    If bReport Then
                        mobjDocuments = New eReports.Report
                        With mobjDocuments
                            .sCodispl = "vil1488_1.rpt"
                            .ReportFilename = "vil1488_1.rpt"
                            sNameReport = "vil1488_1.rpt"

                            .setParamField(1, "sKey", mstrKey)
                            .setStorProcParam(1, mstrKey)
                            Response.Write((.Command))
                        End With
                        mobjDocuments = Nothing
                    End If
                '-------- 

                '+Estipendio por Contrato
                Case "7903"
                    mobjDocuments = New eReports.Report
                    With mobjDocuments
                        .sCodispl = "AGL955"
                        If Request.Form.Item("sOptinfo") = "1" Then
                            .ReportFilename = "AGL955_2.rpt"
                        Else
                            .ReportFilename = "AGL955.rpt"
                        End If

                        .setParamField(1, "sKey", mstrKey)

                        .setStorProcParam(1, lclsBatchParam.Value(1))
                        .setStorProcParam(2, lclsBatchParam.Value(2))
                        .setStorProcParam(3, lclsBatchParam.Value(3))
                        .setStorProcParam(4, lclsBatchParam.Value(4))
                        Response.Write((.Command))
                    End With
                    mobjDocuments = Nothing

                Case "706"
                    mobjDocuments = New eReports.Report
                    With mobjDocuments
                        .sCodispl = "CRL706"
                        .ReportFilename = "CRL706.rpt"
                        .setStorProcParam(1, lclsBatchParam.Value(1))
                        Response.Write((.Command))
                    End With
                    mobjDocuments = Nothing

                Case Else
                    Response.Write("<SCRIPT>alert('Transacción no registrada');</SCRIPT>")

            End Select
        Else
            Response.Write("<SCRIPT>alert('Transacción no genera resultados');</SCRIPT>")
        End If
        lclsBatchParam = Nothing
    End If

    '+Se escribe en duro el código final (en vez de llamar a dll) para que no haga
    '+proceso de cerrar ventana.
    '+Esto porque cuando se cargan reportes con timeout, se podría
    '+perder la invocación de algún reporte (la págna se recarga antes que pueda
    '+mostrar el reporte)
    Response.Write(vbCrLf)
    Response.Write("<SCRIPT>" & vbCrLf)
    'Response.Write mobjValues.CloseShowDefValues(Request.QueryString("sFrameCaller"))    
    Response.Write("top.frames['fraFolder'].UpdateDiv('lblWaitProcess','<BR>','');" & vbCrLf & "    if (typeof(top.frames['fraFolder'])!='undefined')" & vbCrLf & "        if (typeof(top.frames['fraFolder'].mstrDoSubmit)!='undefined')" & vbCrLf & "            top.frames['fraFolder'].mstrDoSubmit='1';" & vbCrLf)
    Response.Write("</SCRIPT>")


    mobjValues = Nothing
%>

<%
'^Begin Footer Block VisualTimer Utility 1.1 7/4/03 11.11.56
Call mobjNetFrameWork.FinishPage("showdefvalues")
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>