Option Strict Off
Option Explicit On
Public Class BatchProcess
	'%-------------------------------------------------------%'
	'% $Workfile:: BatchProcess.cls                          $%'
	'% $Author:: Fmendoza                                    $%'
	'% $Date:: 16/02/06 15:31                                $%'
	'% $Revision:: 2                                         $%'
	'%-------------------------------------------------------%'
	
	'Private Function Send_Mail(ByVal sTo As String, ByVal sSubject As String, Optional ByVal sBody As String, _
	''                      Optional ByVal oImportance As Outlook.OlImportance, Optional ByVal sCc As String, _
	''                      Optional ByVal sAttachment As String) As String
	'
	'    Dim objSendFiles As Object
	'    On Error GoTo Send_Mail_Err
	'
	'    Set objSendFiles = eRemoteDB.NetHelper.CreateClassInstance("eSendFile.SendFiles", "pcjrp")
	'    Call objSendFiles.Send_Mail(sTo, sSubject, sBody, oImportance, sCc, sAttachment)
	'
	'Send_Mail_Err:
	'    If Err Then
	'        Send_Mail = Err.Description
	'    End If
	'    Set objSendFiles = Nothing
	'    On Error GoTo 0
	'End Function
	
	Private Function GetMessage(ByRef nStatus As Short) As String
		Select Case nStatus
			Case 0
				GetMessage = "Deshabilitado"
			Case 1
				GetMessage = "Habilitado para proceso"
			Case 2
				GetMessage = "Enviado a proceso"
			Case 3
				GetMessage = "En ejecución"
			Case 4
				GetMessage = "Término anormal"
			Case 5
                GetMessage = "Término exitoso"
            Case Else
                GetMessage = String.Empty
        End Select
	End Function
	
	Private Function MakeHtmlParamTable(ByVal sKey As String) As String
        Dim sHtml As String = String.Empty
		Dim lrecGetParamBatch As eRemoteDB.Execute
		
		On Error GoTo MakeHtmlParamTable_Err
		
		lrecGetParamBatch = New eRemoteDB.Execute
		
		With lrecGetParamBatch
			.StoredProcedure = "VTBATCHPKG.GetParamBatch"
			.Parameters.Add("sKey", sKey, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(True) Then
				sHtml = "<TABLE WIDTH=100% ALIGN=center BORDER=0 CELLSPACING=1 CELLPADDING=1><TR><TD align=middle>Parámetro</TD><TD align=middle>Valor</TD></TR>"
				Do While Not .EOF
					sHtml = sHtml & "<TR><TD><STRONG>" & .FieldToClass("sName") & "</STRONG></TD><TD><STRONG>" & .FieldToClass("sValue") & "</STRONG></TD></TR>"
					.RNext()
				Loop 
				sHtml = sHtml & "</TABLE>"
				.RCloseRec()
			End If
		End With
		
		MakeHtmlParamTable = sHtml
		
MakeHtmlParamTable_Err: 
		If Err.Number Then
			MakeHtmlParamTable = Err.Description
		End If
		
		'UPGRADE_NOTE: Object lrecGetParamBatch may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecGetParamBatch = Nothing
		On Error GoTo 0
	End Function
	'+ Se encarga de construir los archivos a enviar por los procesos
	Public Function getFiles(ByVal sKey As String, ByRef nBatch As Object) As String
		Dim lclsBatchParam As Object
		Dim lclsCollectionRep As Object
        Dim lstrFile As String = ""
        Dim varAux As String = ""


        lclsBatchParam = eRemoteDB.NetHelper.CreateClassInstance("eSchedule.Batch_param")

        If lclsBatchParam.Find_Value(sKey, nBatch, 2) Then
            Select Case nBatch
                '+ Generación de Cobranza
                Case "101"
                    lclsCollectionRep = eRemoteDB.NetHelper.CreateClassInstance("eCollection.CollectionRep")
                    Call lclsCollectionRep.insGenFilesCOL500(lclsBatchParam.Value(4), lclsBatchParam.Value(8), lclsBatchParam.Value(3), lclsBatchParam.Value(1), lclsBatchParam.Value(9))

                    '+ se busca la ruta donde se encuentra el archivo generado
                    '               Set lobjGeneral = eRemoteDB.NetHelper.CreateClassInstance("eGeneral.GeneralFunction")
                    '              lstrLoadFile = lobjGeneral.GetLoadFile()
                    '             Set lobjGeneral = Nothing

                    If lclsCollectionRep.sFileName <> "" Then
                        'lstrFile = lstrLoadFile & Right(lclsCollectionRep.sFileName, Len(lclsCollectionRep.sFileName) - InStrRev(lclsCollectionRep.sFileName, "/")) & "|"
                        lstrFile = lclsCollectionRep.sFileName & "|"
                    End If

                    varAux = lstrFile
                    'UPGRADE_NOTE: Object lclsCollectionRep may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
                    lclsCollectionRep = Nothing
                    '+ Imputación automátca de Pac/Transbank
                Case "102"
                    lclsCollectionRep = eRemoteDB.NetHelper.CreateClassInstance("eCollection.CollectionRep")

                    lclsCollectionRep.insGenFilesCOL502(lclsBatchParam.Value(1))

                    '+ se busca la ruta donde se encuentra el archivo generado

                    '                Set lobjGeneral = eRemoteDB.NetHelper.CreateClassInstance("eGeneral.GeneralFunction")
                    '                lstrLoadFile = lobjGeneral.GetLoadFile()
                    '                Set lobjGeneral = Nothing

                    If lclsCollectionRep.sFileName <> "" Then
                        'lstrFile = lstrLoadFile & Right(lclsCollectionRep.sFileName, Len(lclsCollectionRep.sFileName) - InStrRev(lclsCollectionRep.sFileName, "/")) & "|"
                        lstrFile = lclsCollectionRep.sFileName & "|"
                    End If

                    varAux = lstrFile
                    'UPGRADE_NOTE: Object lclsCollectionRep may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
                    lclsCollectionRep = Nothing
                    '+ COnciliación automática de primas recaudadas
                Case "116"
                    lclsCollectionRep = eRemoteDB.NetHelper.CreateClassInstance("eCollection.CollectionRep")
                    Call lclsCollectionRep.insGenFilesCOL556(lclsBatchParam.Value(1))
                    '+ se busca la ruta donde se encuentra el archivo generado

                    'Set lobjGeneral = eRemoteDB.NetHelper.CreateClassInstance("eGeneral.GeneralFunction")
                    'lstrLoadFile = lobjGeneral.GetLoadFile()
                    'Set lobjGeneral = Nothing

                    If lclsCollectionRep.sFileName <> "" Then
                        'lstrFile = lstrLoadFile & Right(lclsCollectionRep.sFileName, Len(lclsCollectionRep.sFileName) - InStrRev(lclsCollectionRep.sFileName, "/")) & "|"
                        lstrFile = lclsCollectionRep.sFileName & "|"
                    End If

                    If lclsCollectionRep.sFileName1 <> "" Then
                        '                    lstrFile = lstrFile & lstrLoadFile & Right(lclsCollectionRep.sFileName1, Len(lclsCollectionRep.sFileName1) - InStrRev(lclsCollectionRep.sFileName1, "/")) & "|"
                        lstrFile = lstrFile & lclsCollectionRep.sFileName1 & "|"
                    End If

                    varAux = lstrFile
                    'UPGRADE_NOTE: Object lclsCollectionRep may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
                    lclsCollectionRep = Nothing
            End Select
        End If
        Return varAux
        'UPGRADE_NOTE: Object lclsBatchParam may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsBatchParam = Nothing
	End Function
	
	''%VerifyProcess: Verifica el estado de los procesos batch para informarlos
	''------------------------------------------------------------------
	'Public Function VerifyProcess(ByVal nUsercode As Long) As String
	''------------------------------------------------------------------
	'
	'    Dim lrecVerifyBatch As eRemoteDB.Execute
	'    Dim sBody As String
	'    Dim oImportance As OlImportance
	'    Dim sFiles As String
	'    Dim sMessage As String
	'    Dim lsrtErr As String
	'
	'    On Error GoTo VerifyProcess_Err''
	'
	'    Set lrecVerifyBatch = New eRemoteDB.Execute
	'
	''+
	''+ Definición de store procedure reaBatch_job al 05-30-2003 19:10:04
	''+
	'    With lrecVerifyBatch
	'        .StoredProcedure = "VTBATCHPKG.VerifyBatch"
	'        .Parameters.Add "nUserCode", nUsercode, rdbParamInput, rdbInteger, 22, 0, 5, rdbParamNullable
	'        VerifyProcess = .Run(True)
	'        If VerifyProcess Then
	'            Do While Not .EOF
	'                If .FieldToClass("nStatus") = 5 Or .FieldToClass("nStatus") = 4 Then
	'
	'                    If .FieldToClass("nStatus") = 5 Then
	'                        sFiles = getFiles(.FieldToClass("sKey"), .FieldToClass("nBatch"))
	'                    End If
	'
	'                    sBody = "<BODY bgcolor=#fffaf0 ALIGN=center style='COLOR: navy; FONT-FAMILY: Verdana, Tahoma FONT-SIZE: 10pt'><BR>" & _
	''                            "<TABLE WIDTH=80% ALIGN=center BORDER=0 CELLSPACING=1 CELLPADDING=1 borderColorDark=blue bgColor=#cccccc><TR ALIGN=center><TD><STRONG>FINALIZACION PROCESO BATCH</STRONG></TD></TD></TR></TABLE><HR width=80% color=navy>" & _
	''                            "<BR><BR><TABLE WIDTH=80% ALIGN=center BORDER=0 CELLSPACING=1 CELLPADDING=1>" & _
	''                            "<TR><TD WIDTH=35%>Número..................................</TD><TD><STRONG>" & .FieldToClass("nBatch") & "</STRONG></TD></TR>" & _
	''                            "<TR><TD>Proceso..................................</TD><TD><STRONG>" & .FieldToClass("sDescript") & "</STRONG></TD></TR>" & _
	''                            "<TR><TD>Usuario (Carga).......................</TD><TD><STRONG>" & .FieldToClass("sNameUserLoad") & "</STRONG></TD></TR>" & _
	''                            "<TR><TD>Usuario (Ejecuta).....................</TD><TD><STRONG>" & .FieldToClass("sNameUserSubmit") & "</STRONG></TD></TR>" & _
	''                            "<TR><TD>Inicio Ejecución.......................</TD><TD><STRONG>" & .FieldToClass("dStart") & "</STRONG></TD></TR>" & _
	''                            "<TR><TD>Fin Ejecución...........................</TD><TD><STRONG>" & .FieldToClass("dEnd") & "</STRONG> </TD></TR>" & _
	''                            "<TR><TD>Estado...................................</TD><TD><STRONG>" & GetMessage(.FieldToClass("nStatus")) & "</STRONG> </TD></TR>" & _
	''                            "<TR><TD>Procedure Ejecutado.................</TD><TD><STRONG>" & Left(.FieldToClass("sCommand"), IIf(InStr(1, .FieldToClass("sCommand"), "(") - 1 = -1, Len(.FieldToClass("sCommand")), InStr(1, .FieldToClass("sCommand"), "(") - 1)) & "</STRONG></TD></TR>" & _
	''                            "<TR><TD>Parámetros............................</TD><TD>" & MakeHtmlParamTable(.FieldToClass("sKey")) & "</TD></TR>"
	'
	'                            If sFiles <> "" Then
	'                                sBody = sBody & "<TR><TD>Archivo(s) Generados................</TD>" & _
	''                                                "<TD><TABLE WIDTH=100% ALIGN=center BORDER=0 CELLSPACING=1 CELLPADDING=1>"
	'
	'                                While InStr(1, sFiles, "|") <> 0
	'                                    sBody = sBody & "<TR><TD><STRONG><A href=""" & Left(sFiles, InStr(1, sFiles, "|") - 1) & """>" & Right(Left(sFiles, InStr(1, sFiles, "|") - 1), Len(Left(sFiles, InStr(1, sFiles, "|") - 1)) - InStrRev(Left(sFiles, InStr(1, sFiles, "|") - 1), "/")) & "</A></STRONG></TD></TR>"
	'                                    sFiles = Mid(sFiles, InStr(1, sFiles, "|") + 1)
	'                                Wend
	'                                sBody = sBody & "</TABLE></TD></TR>"
	'                            End If
	'
	'                            sBody = sBody & "</TABLE></TABLE></BODY><BR><HR width=80% color=navy>"
	'
	'                    oImportance = olImportanceHigh
	'                Else
	'                    sBody = "<BODY bgcolor=#fffaf0 ALIGN=center style='COLOR: navy; FONT-FAMILY: Verdana, Tahoma FONT-SIZE: 10pt'><BR>" & _
	''                            "<TABLE WIDTH=80% ALIGN=center BORDER=0 CELLSPACING=1 CELLPADDING=1 borderColorDark=blue bgColor=#cccccc><TR ALIGN=center><TD><STRONG>PROCESO BATCH</STRONG></TD></TD></TR></TABLE><HR width=80% color=navy>" & _
	''                            "<BR><BR><TABLE WIDTH=80% ALIGN=center BORDER=0 CELLSPACING=1 CELLPADDING=1>" & _
	''                            "<TR><TD WIDTH=35%>Número..................................</TD><TD><STRONG>" & .FieldToClass("nBatch") & "</STRONG></TD></TR>" & _
	''                            "<TR><TD>Proceso..................................</TD><TD><STRONG>" & .FieldToClass("sDescript") & "</STRONG></TD></TR>" & _
	''                            "<TR><TD>Usuario (Carga).......................</TD><TD><STRONG>" & .FieldToClass("sNameUserLoad") & "</STRONG></TD></TR>" & _
	''                            "<TR><TD>Estado...................................</TD><TD><STRONG>" & GetMessage(.FieldToClass("nStatus")) & "</STRONG> </TD></TR>" & _
	''                            "<TR><TD>Procedure Ejecutado.................</TD><TD><STRONG>" & Left(.FieldToClass("sCommand"), IIf(InStr(1, .FieldToClass("sCommand"), "(") - 1 = -1, Len(.FieldToClass("sCommand")), InStr(1, .FieldToClass("sCommand"), "(") - 1)) & "</STRONG></TD></TR>" & _
	''                            "<TR><TD>Parámetros............................</TD><TD>" & MakeHtmlParamTable(.FieldToClass("sKey")) & "</TD></TR>"
	'                            sBody = sBody & "</TABLE></TABLE></BODY><BR><HR width=80% color=navy>"
	'                    oImportance = olImportanceNormal
	'                End If
	'                sMessage = "Proceso " & .FieldToClass("sKey") & " (" & .FieldToClass("sNameCompany") & "-" & .FieldToClass("sDescript") & ") se encuentra en estado " & GetMessage(.FieldToClass("nStatus"))
	'                If .FieldToClass("sTo") <> "" Then
	'                    lsrtErr = Send_Mail(.FieldToClass("sTo"), sMessage, sBody, oImportance, .FieldToClass("sCC"))
	'                End If
	'                .RNext
	'            Loop
	'            .RCloseRec
	'        End If
	'    End With
	'
	'VerifyProcess_Err:
	'    If Err Then
	'        VerifyProcess = lsrtErr
	'    End If
	'    Set lrecVerifyBatch = Nothing
	'    On Error GoTo 0
	'End Function
End Class






