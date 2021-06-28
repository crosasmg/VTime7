<%@ Page Language="VB" explicit="true"  EnableViewState="false"%>
<%@ Import namespace="eCrystalExport" %>
<%@ Import namespace="ADODB" %>
<script language="VB" runat="Server">
    Dim oHelper As eCrystalExport.Export
    Dim i As Object
    Dim oMergePDF As eCrystalExport.MergePDF
    Dim nMyTicket As Object
    Dim nCounter As Byte 
    Dim bBreak As Boolean
    Dim nPriority As Object
    Dim sCertype_desc As String
      
    '--------------------------------
    Private Sub InsLoadParameters()
    '--------------------------------
    Dim lintIndex As Integer
    Dim lstrValue As String
        If Not Request.QueryString.GetValues("p") Is Nothing Then
            For lintIndex = 0 To Request.QueryString.GetValues("p").Count - 1
                lstrValue = Request.QueryString.GetValues("p").GetValue(lintIndex)
                If lstrValue = CStr(eRemoteDB.Constants.intNull) Then
                    lstrValue = ""
                ElseIf lstrValue = "@@" Then
                    lstrValue = ""
                End If
                oHelper.ReportParameters.Add(lstrValue)
            Next
        End If
        If Not Request.QueryString.GetValues("sp") Is Nothing Then
            For lintIndex = 0 To Request.QueryString.GetValues("sp").Count - 1
                lstrValue = Request.QueryString.GetValues("sp").GetValue(lintIndex)
                If lstrValue = CStr(eRemoteDB.Constants.intNull) Then
                    lstrValue = ""
                ElseIf lstrValue = "@@" Then
                    lstrValue = ""
                End If
                oHelper.DBParameters.Add(lstrValue)
            Next
        End If
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
    
    Private Sub OpenAndShowFile2(ByVal sFullPath As String)
        Response.Clear()
        Response.Buffer = True
        Response.WriteFile(oHelper.sExportedFilePath)
        Response.End()
    End Sub
</script>
<%  nCounter = 0
    Dim bSuccess As Boolean = False
    oHelper = New eCrystalExport.Export
    InsLoadParameters()
 
    oHelper.nGenPolicy = Request.QueryString("nGenPolicy")
    If oHelper.nGenPolicy = 1 Or oHelper.nCopyPolicy = 1 Then
        oHelper.sCertype = Request.QueryString("MergeCertype")
        oHelper.nBranch = Request.QueryString("MergeBranch")
        oHelper.nProduct = Request.QueryString("MergeProduct")
        oHelper.nPolicy = Request.QueryString("MergePolicy")
        oHelper.nCertif = Request.QueryString("MergeCertif")
        oHelper.nMovement = Request.QueryString("nMovement")
        oHelper.nCopyPolicy = Request.QueryString("nCopyPolicy")
        oHelper.sPolitype = Request.QueryString("sPolitype")
        'oHelper.sCartol = Request.QueryString("MergeCartol")
        oHelper.sCartol = Request.QueryString("sCartol")
        oHelper.nCartol = Request.QueryString("nCartol")
    End If
    
    If IsNumeric(Request.QueryString("nReport")) AndAlso Request.QueryString("nReport") > 0 Then
        oHelper.sCertype = Request.QueryString("MergeCertype")
        oHelper.nBranch = Request.QueryString("MergeBranch")
        oHelper.nProduct = Request.QueryString("MergeProduct")
        oHelper.nPolicy = Request.QueryString("MergePolicy")
        oHelper.nCertif = Request.QueryString("MergeCertif")
        oHelper.nMovement = Request.QueryString("nMovement")
        oHelper.nForzaRep = Request.QueryString("nForzaRep")
        oHelper.nTratypep = Request.QueryString("nTratypep")
        oHelper.nCopyPolicy = Request.QueryString("nCopyPolicy")
        oHelper.sPolitype = Request.QueryString("sPolitype")
        If Request.QueryString("nGenPolicy") = 1 Then
            bSuccess = oHelper.GenPoliza(Request.QueryString("nFormat"), Request.QueryString("nReport"), Session("sInitialsCon"), Session("sAccesswoCon"), , Server.MapPath("/VTIMENET"))
        Else
            bSuccess = oHelper.InvokeRealExport2(Request.QueryString("nFormat"), Request.QueryString("nReport"), Session("sInitialsCon"), Session("sAccesswoCon"), , Server.MapPath("/VTIMENET"))
        End If
    Else
        bSuccess = oHelper.RealExport(Server.MapPath(Request.QueryString.Item("URL")), "xxx.rpt", "PDF", "", "", Session("sInitialsCon"), Session("sAccesswoCon"))
    End If
    
    Select Case oHelper.sCertype
        Case 1
            sCertype_desc = "PROPUESTA"
        Case 2
            sCertype_desc = "PÓLIZA"
        Case 3
            sCertype_desc = "COTIZACIÓN"
        Case 4
            sCertype_desc = "COTIZACIÓN DE MODIFICACIÓN"
        Case 6
            sCertype_desc = "ENDOSO DE MODIFICACIÓN"
        Case 8
            sCertype_desc = "ENDOSO ESPECIAL"
        Case Else
            sCertype_desc = ""
    End Select
    
    If bSuccess Then
        
        If Request.QueryString("Merge") = "1" Or Request.QueryString("Merge") = "Verdadero" Or Request.QueryString("Merge") = "True" Then
            'ejecuta el merge de cuadros de  polizas mas sus condicionados (es necesario que esten en pdf)
            'Response.Write "llamando el merge  " & oHelper.sCopyName & " " & oHelper.nCopies
            'Response.flush
            oMergePDF = New MergePDF()
            If oMergePDF.MergePDFs2(Request.QueryString("MergeBranch"), Request.QueryString("MergeProduct"), Request.QueryString("MergePolicy"), Request.QueryString("MergeCertif"), oHelper._sExportedFilePath, oHelper.sCopyName, oHelper.nCopies, Request.QueryString("MergeCertype")) Then
                OpenAndShowFile(oMergePDF.sExportedFilePath)
            Else
                Response.Write("******HA OCURRIDO UNA EXCEPCIÓN AL ANEXAR CONDICIONADOS ******" & "<BR>" & oMergePDF.sException)
            End If
        Else
            OpenAndShowFile(oHelper._sExportedFilePath)
        End If
    Else
        Response.Write("<center><font face=calibri><h4>*** SE PRODUJO UN ERROR AL GENERAR EL REPORTE ***</h4></font></center>")
        Response.Write("<BR><font face=calibri><b> NOMBRE: </b>" & oHelper.sReportName & "</font><BR>")
        Response.Write("<BR><font face=calibri><b> TIPO DE REGISTRO: </b>" & sCertype_desc & "</font><BR>")
        Response.Write("<BR><font face=calibri><b> "& sCertype_desc &": </b>" & oHelper.nPolicy & "/" & oHelper.nCertif & "</font><BR>")
        Response.Write("<BR><font face=calibri><b> ERROR: </b><BR>" & oHelper.sErrorReport & "</font>")
    End If
    
    oHelper = Nothing
        oMergePDF = Nothing
 %>