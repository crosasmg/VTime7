<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eInterface" %>
<%@ Import namespace="eGeneral" %>
<%@ Import namespace="eSchedule" %>
<%@ Import namespace="eReports" %>
<%@ Import Namespace="System.IO" %>
<%@ Import Namespace="System.Collections.Generic" %>
<%@ Import Namespace="System.Text" %>
<script language="VB" runat="Server">
    Dim nGI1402_K As Object

    Dim mobjGeneral As eGeneral.GeneralFunction
    Dim mobjInterface As eInterface.ValInterfaceSeq
    Dim mstrErrors As String
    Dim mobjValues As eFunctions.Values
    Dim mstrString As Object
    Dim mstrPath As String
    Dim mstrFileName As Object
    Dim mblnError As Boolean
    Dim mobjUploadRequest As Object
    
    '+  Variable para usar el querystring
    Dim mstrQueryString As String

    '+ Se define la contante para el manejo de errores en caso de advertencias
    Dim mstrCommand As String

    Dim mstrCodispl As Object

    Dim mstrLocationGI1402_K As String

    Dim ScriptObject As FileStream
    Dim ByteCount As Integer
    Dim binData() As Byte
    
    Dim myRequestFile(5) As String
    Dim fileContentIndex As Integer
    Dim fileContentLength As Integer
    Dim crlf As String = Chr(13) & Chr(10)
    Dim mstrFileFullPath As String
    Dim arrFiles() As String

'+ insvalinterfaceseq: Se realizan las validaciones masivas de la forma
'--------------------------------------------------------------------------------------------
Function insvalinterfaceseq() As String
	Dim lcolfieldsheet As eInterface.FieldSheets
    Dim lclsfieldsheet As eInterface.FieldSheet
    Dim lclsMasterSheet As eInterface.MasterSheet
    Dim lblnResult As Boolean
    Dim posArr As Integer = 0
        
	Select Case mstrCodispl
            Case "GI1402_K"
                Session("bQueryGI1402") = Request.QueryString.Item("nMainAction") = "401"
                If insUpLoadFile2(mstrPath) Then
                
                Else
                    Session("sFile") = mobjUploadRequest("hdtFileName")
                End If
                
                Session("nIntertype") = mobjUploadRequest("optnintertype")
                Session("nSheet") = mobjUploadRequest("valnsheet")
                Session("sTable") = mobjUploadRequest("tctTable")
                Session("nSystem") = mobjUploadRequest("cbeSystem")

                If mobjUploadRequest("valnsheet_nFormat") <> vbNullString And mobjUploadRequest("valnsheet_nFormat") <> "-32678" And mobjUploadRequest("valnsheet_nFormat") <> "0" Then
                    Session("nFormat") = mobjUploadRequest("valnsheet_nFormat")
                Else
                    Session("nFormat") = mobjUploadRequest("hddnFormat")
                End If
                
                lclsMasterSheet = New eInterface.MasterSheet
                Call lclsMasterSheet.Find(Session("nSheet"))
                
                mobjInterface = New eInterface.ValInterfaceSeq
                With Request
                    If .QueryString.Item("WindowType") <> "PopUp" Then
                        insvalinterfaceseq = mobjInterface.insValGI1402_K("GI1402_K", mobjValues.StringToType(Session("nSystem"), eFunctions.Values.eTypeData.etdLong, True),
                                                                                      mobjValues.StringToType(Session("nSheet"), eFunctions.Values.eTypeData.etdLong, True),
                                                                                      Session("sFile"),
                                                                                      mobjValues.StringToType(Session("nInterType"), eFunctions.Values.eTypeData.etdLong, True),
                                                                                      mobjValues.StringToType(Session("nFormat"), eFunctions.Values.eTypeData.etdLong, True),
                                                                                      Session("sTable"),
                                                                                      mobjValues.StringToType(Request.QueryString.Item("nMainAction"), eFunctions.Values.eTypeData.etdLong, True),
                                                                                      lclsMasterSheet.sOnLine,
                                                                                      lclsMasterSheet.sSheet_father)
                    Else
                        insvalinterfaceseq = ""
                    End If
                End With
                lclsMasterSheet = Nothing
                mobjInterface = Nothing
			
                If Session("nInterType") = 2 Then
                    mblnError = True
                Else
                    mstrLocationGI1402_K = "'/VTimeNet/Interface/interfaceseq/GI1402.aspx?sCodispl=GI1402&nMainAction=" & Request.QueryString.Item("nMainAction") & "'"
                End If
                mobjGeneral = New eGeneral.GeneralFunction
                Session("sKey") = mobjGeneral.getsKey(mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdLong, True))
                mobjGeneral = Nothing
			
            Case "GI1402"
                insvalinterfaceseq = vbNullString
                If Request.QueryString.Item("nZone") = "2" Then
                    mobjInterface = New eInterface.ValInterfaceSeq
                    lcolfieldsheet = New eInterface.FieldSheets
                    lclsfieldsheet = New eInterface.FieldSheet
                    If lcolfieldsheet.Find2(mobjValues.StringToType(Session("nSheet"), eFunctions.Values.eTypeData.etdDouble),
                                            mobjValues.StringToType("3", eFunctions.Values.eTypeData.etdDouble)) Then
                        For Each lclsfieldsheet In lcolfieldsheet
                            '+ LLamada por cada campo dinamico para almacenar datos para parametros
                            If lclsfieldsheet.nObjtype <> 8 Then
                                If lclsfieldsheet.nObjtype <> 10 Then
                                    Call mobjInterface.CreT_Param_Interface(Session("skey"),
                                         mobjValues.StringToType(CStr(lclsfieldsheet.nField), eFunctions.Values.eTypeData.etdDouble, True),
                                         mobjUploadRequest(lclsfieldsheet.sColumnName),
                                         mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdLong, True))
                                Else
                                    Call mobjInterface.CreT_Param_Interface(Session("skey"),
                                         mobjValues.StringToType(CStr(lclsfieldsheet.nField), eFunctions.Values.eTypeData.etdDouble, True),
                                         mobjUploadRequest(lclsfieldsheet.sColumnName & "tctFile"),
                                         mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdLong, True))

                                    lblnResult = insUpLoadFile3(arrFiles(posArr), arrFiles(posArr + 1), arrFiles(posArr + 2))
                                    lblnResult = loadFile(lclsfieldsheet.nField, mobjUploadRequest(lclsfieldsheet.sColumnName & "tctFile"))
                                    lblnResult = deleteFile(mobjUploadRequest(lclsfieldsheet.sColumnName & "tctFile"), True)
                                    posArr = posArr + 3
                                End If
                            Else
                                Call mobjInterface.CreT_Param_Interface(Session("skey"),
                                     mobjValues.StringToType(CStr(lclsfieldsheet.nField), eFunctions.Values.eTypeData.etdDouble, True),
                                     mobjUploadRequest(lclsfieldsheet.sColumnName & "hdd"),
                                     mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdLong, True))
                            End If
                        Next lclsfieldsheet
                    End If
                    '+ llamada a validacion de parametros dinamicos, almacenados en T_param_interface
                    insvalinterfaceseq = mobjInterface.insValGI1402(Session("skey"), Session("nSheet"))
				
                End If
                lcolfieldsheet = Nothing
                lclsfieldsheet = Nothing
			
            Case "GI1403"
                insvalinterfaceseq = vbNullString
			
            Case "GI1404"
                insvalinterfaceseq = vbNullString
			
            Case "GI1405"
                insvalinterfaceseq = vbNullString
			
            Case "GI1406"
                insvalinterfaceseq = vbNullString
			
            Case Else
                insvalinterfaceseq = "insvalinterfaceseq: Código lógico no encontrado (" & Request.QueryString.Item("sCodispl") & ")"
			
        End Select
End Function

'% insPostInterface: Se realizan las actualizaciones a las tablas
'------------------------------------------------------------------------------------
Function insPostInterface() As Boolean
	Dim lblnPost As Boolean
	Dim lblnPrintReport As Boolean
	Dim lclsBatch_param As eSchedule.Batch_param
	Dim lclsMasterSheet As eInterface.MasterSheet
	Dim lblnResult As Boolean
    Dim sFilename_Aux As String
    
	lblnPrintReport = False
	lblnPost = False
	
	Dim lobjDocuments As eReports.Report
	Select Case mstrCodispl
		'+ GI1402_K: Encabezado de Procesamiento de Interfaces
		Case "GI1402_K"
			
			If mblnError Then
				If session("nInterType") = 1 Then
					Response.Write("<SCRIPT>updateStatus();</" & "Script>")
				Else
					lblnPost = True
				End If
			Else
				lblnPost = True
			End If
			
		Case "GI1402"
		
			lclsMasterSheet = New eInterface.MasterSheet
			Call lclsMasterSheet.Find(session("nSheet"))
			
                If CStr(Session("BatchEnabled")) = "1" And lclsMasterSheet.sOnLine = "2" Then
                    sFilename_Aux = Session("sFile")
                    If (Session("nFormat") = "2" Or Session("nFormat") = "11") And Session("nInterType") = "1" Then
                        
                        Dim sExtension = Path.GetExtension(Session("sFile")).ToString.ToLower
                        If sExtension = ".xls" Or sExtension = ".xlsx" Then
                            If mobjInterface.insTransformationExcel(Session("sFile")) Then
                                If sExtension = ".xlsx" Then
                                    sFilename_Aux = Replace(Session("sFile").ToString.ToLower, ".xlsx", ".TXT")
                                Else
                                    sFilename_Aux = Replace(Session("sFile").ToString.ToLower, ".xls", ".TXT")
                                End If
                                'Se llama a proceso que envía archivo desde carpeta TFiles a servidor de BD
                                lblnResult = mobjInterface.InsPostGI1402_File(sFilename_Aux, Session("nInterType"), Session("nFormat"), Session("sKey"), Session("nSheet"), Session("nUsercode"), False)
                            Else
                                If mobjInterface.sMessage <> "" Then
                                    'Se llama a proceso que inserta los errores en la tabla T_ERR_INTERFACE de la BD
                                    lblnResult = mobjInterface.Ins_Cret_Err_Interface(Session("sKey"), Session("nUsercode"))
                                End If
                            End If
                        Else
                            'Se llama a proceso que envía archivo desde carpeta TFiles a servidor de BD
                            lblnResult = mobjInterface.InsPostGI1402_File(sFilename_Aux, Session("nInterType"), Session("nFormat"), Session("sKey"), Session("nSheet"), Session("nUsercode"), False)
                        End If
                    Else
                        'Se llama a proceso que envía archivo desde carpeta TFiles a servidor de BD
                        lblnResult = mobjInterface.InsPostGI1402_File(sFilename_Aux, Session("nInterType"), Session("nFormat"), Session("sKey"), Session("nSheet"), Session("nUsercode"), False)
                    End If
                    
                    lclsBatch_param = New eSchedule.Batch_Param
                    With lclsBatch_param
                        .nBatch = 1402
                        .sKey = Session("sKey")
                        .nSheet = Session("nSheet")
                        .nUsercode = mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble)
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, Session("nInterType"))
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, Session("nSheet"))
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdLong, True))
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, Session("sKey"))
                        
                        '+ Si es una interfaz de entrada tipo excel, se realiza transformación.
                        If Session("nFormat") = "2" And Session("nInterType") = "1" And Session("nSystem") = "3" Then
                            .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, Session("sFile"))
                        Else
                            If Session("nFormat") = 3 Then
                                .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, Session("sTable"))
                            Else
                                .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, Session("sFile"))
                            End If
                        End If
                        
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaRes, Session("sKey"))
                        .Save()
                    End With
                    
                    'Se llama a proceso que elimina archivo generado de Tfile luego de haberlo subido al servidor de BD
                    lblnResult = deleteFile(Session("sFile"), False)
                    
                    Response.Write("<SCRIPT>alert('Se generó la clave de proceso: " & Session("sKey") & "');</" & "Script>")
                    Response.Write("<SCRIPT>insReloadTop(false);</" & "Script>")
				
                    lclsMasterSheet = Nothing
                    lclsBatch_param = Nothing
                Else
                    mobjInterface = New eInterface.ValInterfaceSeq
				
                    '+ Muevo el archivo desde SII al Servidor de BD y ejecuto rutinas correspondientes al POST
                    lblnPost = mobjInterface.InsPostGI1402(Session("nInterType"),
                                                           Session("nSheet"),
                                                           mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdLong, True),
                                                           Session("sKey"),
                                                           Session("sFile"),
                                                           mobjValues.StringToType(Session("nFormat"), eFunctions.Values.eTypeData.etdLong, True),
                                                           Session("sDescError"), 
                                                           IIf(Request.QueryString.Item("nMainAction") = "401", "1", "2"))
                    
                    'Se llama a proceso que elimina archivo generado de Tfile luego de haberlo subido al servidor de BD
                    lblnResult = deleteFile(Session("sFile"), False)
                    
                    Session("sError") = "N"
                    '+ Entrando a la secuencia se puede Imprimir el Reporte			
                    Session("Report") = "S"
				
                    '+ Si el retorno es Falso no hay registros en T_INTERFACE			
                    If Not lblnPost Then
                        Session("sError") = "S"
                    End If
				
                    mobjInterface = Nothing
				
                End If
			
		Case "GI1403"
			lblnPost = True
			
		Case "GI1404"
			lblnPost = True
			
		Case "GI1405"
			
			'+ Post: envio a Rutina 'INSOUTINTERFACE', genera archivo xml desde T_Interface para "salida"
			'+ Para "entrada" ejecuta rutinas correspondientes
                If CStr(Session("sError")) = "N" Then
                    Try
                        mobjInterface = New eInterface.ValInterfaceSeq
                        If Session("nFormat") = 3 Then
                            lblnPost = mobjInterface.InsPostGI1405(Session("nInterType"), Session("nSheet"), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdLong, True), Session("sKey"), Session("sTable"))
                        Else
                            lblnPost = mobjInterface.InsPostGI1405(Session("nInterType"), Session("nSheet"), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdLong, True), Session("sKey"), Session("sFile"))
                        End If
                    Catch x As Exception
                    
                    Finally
                        Response.Write(mobjInterface.sMessage)
                       
                    End Try
                    lblnPrintReport = mobjInterface.nExistError <> 0
                    Response.Write("<SCRIPT>")
                    Response.Write("ShowPopUp('GI1407.aspx?sCodispl=GI1407','EndProcess',1000,500);")
                    Response.Write("</" & "Script>")
				
                    If mobjInterface.insReport(Session("sKey"), Session("nSheet")) Then
					
                        lobjDocuments = New eReports.Report
                        With lobjDocuments
                            Select Case mstrCodispl
                                Case "GI1405"
                                    .sCodispl = "GI1405"
                                    .ReportFilename = "GIL1405_1.rpt"
                                    .setStorProcParam(1, Session("skey"))
                                    .setStorProcParam(2, (mobjInterface.sDescript))
                                    Response.Write((.Command))
                            End Select
                        End With
                        lobjDocuments = Nothing
                    End If
				
                    mobjInterface = Nothing
                Else
                    lblnPost = True
                    Response.Write("<SCRIPT>alert('Proceso de Interfaz Terminado con Error. No se Proceso información. ');</" & "Script>")
                    lblnPrintReport = True
                End If
			
		Case "GI1406"
			lblnPost = True
	End Select
	
	insPostInterface = lblnPost
	
	If insPostInterface Then
		If lblnPrintReport Then
			insPrintDocuments()
		End If
	End If
	
End Function

    '%loadFile(). Función que carga en BD en campo CLOB información de un archivo.
    '% nId: Identificador del archivo.
    '% filePath: Ruta física del archivo.
    Function loadFile(ByVal nId As Integer, ByVal fileName As String) As Boolean
        Dim lblnResult As Boolean
        
        Dim sFile As String = fileName
        Dim sExtension = Path.GetExtension(sFile).ToString.ToLower
        If sExtension = ".xls" Or sExtension = ".xlsx" Then
            If mobjInterface.insTransformationExcel(sFile) Then
                If sExtension = ".xlsx" Then
                    sFile = Replace(sFile.ToLower, ".xlsx", ".TXT")
                Else
                    sFile = Replace(sFile.ToLower, ".xls", ".TXT")
                End If
                'Se llama a proceso que envía archivo desde carpeta TFiles a servidor de BD
                lblnResult = mobjInterface.InsPostGI1402_File(sFile, Session("nInterType"), Session("nFormat"), Session("sKey"), nId, Session("nUsercode"), True)
            Else
                If mobjInterface.sMessage <> "" Then
                    'Se llama a proceso que inserta los errores en la tabla T_ERR_INTERFACE de la BD
                    lblnResult = mobjInterface.Ins_Cret_Err_Interface(Session("sKey"), Session("nUsercode"))
                End If
            End If
        Else
            'Se llama a proceso que envía archivo desde carpeta TFiles a servidor de BD
            lblnResult = mobjInterface.InsPostGI1402_File(sFile, Session("nInterType"), Session("nFormat"), Session("sKey"), nId, Session("nUsercode"), True)
        End If
   
        Return lblnResult
    End Function
    
    '%deleteFile(). Función que elimina archivo desde Tfile.
    Function deleteFile(ByVal fileName As String, ByVal bPermitted As Boolean) As Boolean
        Dim sSavePath As String
        Dim sFilename2 As String
        Dim bResult As Boolean
        Dim sExtension As String
        Dim lobjValues As eFunctions.Values
        
        lobjValues = New eFunctions.Values
        sSavePath = Trim(UCase(lobjValues.insGetSetting("MASSIVELOAD", String.Empty, "PATHS")))
        
        If String.IsNullOrEmpty(fileName) Then
            bResult = False
        End If
        
        'Se verifica existencia del archivo antes de ser eliminado
        If File.Exists(sSavePath & "\" & fileName) Then
            File.Delete(sSavePath & "\" & fileName)
            
            'Se valida si el archivo a subir es excel
            If (Session("nFormat") = "2" Or Session("nFormat") = "11") And Session("nInterType") = "1" Then
                sExtension = Path.GetExtension(fileName).ToString.ToLower
                If sExtension = ".xlsx" Then
                    sFilename2 = Replace(fileName.ToLower, ".xlsx", ".TXT")
                    If File.Exists(sSavePath & "\" & sFilename2) Then File.Delete(sSavePath & "\" & sFilename2)
                ElseIf sExtension = ".xls" Then
                    sFilename2 = Replace(fileName.ToLower, ".xls", ".TXT")
                    If File.Exists(sSavePath & "\" & sFilename2) Then File.Delete(sSavePath & "\" & sFilename2)
                End If
            End If
            
            If bPermitted Then
                sExtension = Path.GetExtension(fileName).ToString.ToLower
                If sExtension = ".xlsx" Then
                    sFilename2 = Replace(fileName.ToLower, ".xlsx", ".TXT")
                    If File.Exists(sSavePath & "\" & sFilename2) Then File.Delete(sSavePath & "\" & sFilename2)
                ElseIf sExtension = ".xls" Then
                    sFilename2 = Replace(fileName.ToLower, ".xls", ".TXT")
                    If File.Exists(sSavePath & "\" & sFilename2) Then File.Delete(sSavePath & "\" & sFilename2)
                End If
            End If

            bResult = True
        End If
        
        Return bResult
    End Function
    
    '% insUpLoadFile: Se encarga de subir el archivo seleccionado al servidor según ruta pasada como parámetro.
    '% FilePath: Ruta física donde se va almacenar el archivo en el servidor. Eje. "c:\InetPub\UpLoad\"
    '--------------------------------------------------------------------------------------------
    Function insUpLoadFile2(ByRef FilePath As String) As Boolean

        Dim sFilename As String = Path.GetFileName(myRequestFile(4))
        Dim sSavePath As String
        Dim fileAppend As Integer
        Dim lobjValues As eFunctions.Values
        
        lobjValues = New eFunctions.Values
        lobjValues.sSessionID = Session.SessionID
        lobjValues.sCodisplPage = "ValInterfaceSeq"
        
        sSavePath = Trim(UCase(lobjValues.insGetSetting("MASSIVELOAD", String.Empty, "PATHS")))
               
        If String.IsNullOrEmpty(sFilename) Then Return False
        
        Session("sFile") = sFilename
        
        'Do While File.Exists(sSavePath & "\" & sFilename)
        'fileAppend += 1
        'sFilename = Path.GetFileNameWithoutExtension(myRequestFile(4)) & fileAppend.ToString & _
        'Path.GetExtension(myRequestFile(4))
        'Loop
        If File.Exists(sSavePath & "\" & sFilename) Then
            File.Delete(sSavePath & "\" & sFilename)
        End If

        Dim newFile As FileStream = Nothing
        
        newFile = New FileStream(sSavePath & "\" & sFilename, FileMode.Create)
            
      
        For i As Integer = 0 To fileContentLength - 1
            newFile.WriteByte(binData(fileContentIndex + i))
        Next
        
        newFile.Close()

        If File.Exists(sSavePath & "\" & sFilename) Then
            mstrFileFullPath = sSavePath & "\" & sFilename
            Return True
        Else
            mstrFileFullPath = String.Empty
            Return False
        End If
	
    End Function
    
    '% insUpLoadFile: Se encarga de subir el archivo seleccionado al servidor según ruta pasada como parámetro.
    '% FilePath: Ruta física donde se va almacenar el archivo en el servidor. Eje. "c:\InetPub\UpLoad\"
    '--------------------------------------------------------------------------------------------
    Function insUpLoadFile(ByRef FilePath As String) As Boolean
        Dim sFilename As String = Path.GetFileName(myRequestFile(4))
        Dim sSavePath As String
        Dim fileAppend As Integer
        Dim lobjValues As eFunctions.Values
        Dim lclsInterface As eInterface.ValInterfaceSeq
        
        lobjValues = New eFunctions.Values
        lclsInterface = New eInterface.ValInterfaceSeq
        lobjValues.sSessionID = Session.SessionID
        lobjValues.sCodisplPage = "valpolicyrepseq"
    
        Call lclsInterface.Find_Opt_Interfase()
        sSavePath = lclsInterface.sIPremote & "\dirwork"
        lclsInterface = Nothing
        
        Session("sFile") = sFilename
        
        If String.IsNullOrEmpty(sFilename) Then Return False
        
        If File.Exists(sSavePath & "\" & sFilename) Then
            File.Delete(sSavePath & "\" & sFilename)
        End If
        
        Do While File.Exists(sSavePath & "\" & sFilename)
            fileAppend += 1
            sFilename = Path.GetFileNameWithoutExtension(myRequestFile(4)) & fileAppend.ToString & _
                Path.GetExtension(myRequestFile(4))
        Loop

        Dim newFile As FileStream = Nothing
        
        newFile = New FileStream(sSavePath & "\" & sFilename, FileMode.Create)
            
        For i As Integer = 0 To fileContentLength - 1
            newFile.WriteByte(binData(fileContentIndex + i))
        Next
        
        newFile.Close()

        If File.Exists(sSavePath & "\" & sFilename) Then
            mstrFileFullPath = sSavePath & "\" & sFilename
            Return True
        Else
            mstrFileFullPath = String.Empty
            Return False
        End If
	
    End Function
    
    ''% insUpLoadFile: Se encarga de subir el archivo seleccionado al servidor según ruta pasada como parámetro.
    ''% FilePath: Ruta física donde se va almacenar el archivo en el servidor. Eje. "c:\InetPub\UpLoad\"
    ''--------------------------------------------------------------------------------------------
    Function insUpLoadFile3(ByVal FilePath As String, ByVal indexFile As Integer, ByVal lengthFile As Integer) As Boolean

        Dim sFilename As String = Path.GetFileName(FilePath)
        Dim sSavePath As String
        Dim fileAppend As Integer
        Dim lobjValues As eFunctions.Values
        
        lobjValues = New eFunctions.Values
        lobjValues.sSessionID = Session.SessionID
        lobjValues.sCodisplPage = "ValInterfaceSeq"
        
        sSavePath = Trim(UCase(lobjValues.insGetSetting("MASSIVELOAD", String.Empty, "PATHS")))
               
        If String.IsNullOrEmpty(sFilename) Then Return False
        
        Do While File.Exists(sSavePath & "\" & sFilename)
            fileAppend += 1
            sFilename = Path.GetFileNameWithoutExtension(FilePath) & fileAppend.ToString & _
                        Path.GetExtension(FilePath)
        Loop
        
        Dim newFile As FileStream = Nothing
        newFile = New FileStream(sSavePath & "\" & sFilename, FileMode.Create)
        
        For i As Integer = 0 To lengthFile - 1
            newFile.WriteByte(binData(indexFile + i))
        Next
        
        newFile.Close()

        If File.Exists(sSavePath & "\" & sFilename) Then
            mstrFileFullPath = sSavePath & "\" & sFilename
            Return True
        Else
            mstrFileFullPath = String.Empty
            Return False
        End If
    End Function

    '% getString: Conversión de los datos de Byte a String
    '--------------------------------------------------------------------------------------------
    Function getString(ByRef sStringBin As String) As String
        '--------------------------------------------------------------------------------------------
        Dim lintCount As Integer
	
        getString = vbNullString
	
        For lintCount = 1 To Len(sStringBin)
            getString = getString & Chr(Asc(Mid(sStringBin, lintCount, 1)))
        Next
	
    End Function

    '% getByteString: Conversión de los datos de String a Byte
    '--------------------------------------------------------------------------------------------
    Function getByteString(ByRef sStringStr As String) As String
        '--------------------------------------------------------------------------------------------
        Dim linCount As Integer
        Dim lstrchar As String
        For linCount = 1 To Len(sStringStr)
            lstrchar = Mid(sStringStr, linCount, 1)
            getByteString = getByteString & Chr(Asc(lstrchar))
        Next
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

    ' This function retreives a field's name
    Function getFieldName(ByVal infoStr As String) As String
        Dim sPos As Integer = infoStr.IndexOf("name=")
        Dim endPos As Integer = infoStr.Substring(sPos + 5).IndexOf(Chr(34) & ";")
        If endPos = -1 Then
            endPos = infoStr.Substring(sPos + 6).IndexOf(Chr(34))
        End If
        
        Return infoStr.Substring(sPos + 6, endPos)
    End Function

    Sub BuildUploadRequest(ByVal data() As Byte)
        Dim pos As Integer = 0
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
                        myRequestFile(2) = New Random().Next(100000000, 900000000) & "_" & getFileName(varInfo)
                        myRequestFile(3) = getFileType(varInfo)
                        myRequestFile(4) = getFileName(varInfo)
                        
                        fileContentIndex = (New String(postData)).IndexOf(varValue)
                        fileContentLength = varValue.Length
                        
                        ReDim Preserve arrFiles(pos + 2)
                        arrFiles(pos) = getFileName(varInfo)
                        arrFiles(pos + 1) = fileContentIndex
                        arrFiles(pos + 2) = fileContentLength
                        pos = pos + 3
                        
                    Else
                        mobjUploadRequest.Add(getFieldName(varInfo), varValue)
                    End If
                End If
            Next
        End If
    End Sub
    
'+ insFinish: se activa al finalizar el proceso
'--------------------------------------------------------------------------------------------
Function insFinish() As Boolean
	'--------------------------------------------------------------------------------------------
        If Session("bQueryGI1402") = True Then
            Response.Write("<SCRIPT>")
            Response.Write("insReloadTop(false);")
            Response.Write("</" & "Script>")
        Else
            Response.Write("<SCRIPT>")
            Response.Write("ShowPopUp('/VTimeNet/interface/InterfaceSeq/GI1405.aspx?sCodispl=GI1405&nAction=392','EndProcess',400,130);")
            Response.Write("setPointer('');")
            Response.Write("</" & "Script>")
        End If
        insFinish = True
    End Function

    
'% insPrintDocuments : Realiza la ejecución del reporte
'-------------------------------------------------------------------------------------------
Private Sub insPrintDocuments()
	'-------------------------------------------------------------------------------------------
	Dim lobjDocuments As eReports.Report
	
	'+ cargo reporte con errores si corresponde (solo luego de entrar a la secuencia)
	If CStr(session("Report")) = "S" Then
		
		lobjDocuments = New eReports.Report
		With lobjDocuments
			Select Case mstrCodispl
				
				Case "GI1405"
					.sCodispl = "GI1405"
					.ReportFilename = "GIL1405.rpt"
					.setStorProcParam(1, session("skey"))
					Response.Write((.Command))
			End Select
		End With
		lobjDocuments = Nothing
		
	End If
	
End Sub

</script>
<%Response.Expires = -1

        mstrCodispl = Request.QueryString.Item("sCodispl")

        mobjValues = New eFunctions.Values

        mstrPath = "C:\\InetPub\\UpLoad\\"

        mstrCommand = "&sModule=Interface&sProject=interfaceseq&sCodisplReload=" & mstrCodispl

        If InStr(1, Request.QueryString.Item("sCodispl"), "INT") > 0 Then
            mstrCodispl = nGI1402_K
        End If


%>
<SCRIPT>
//% updateStatus: Actualiza estado de botones y cursor de mouse
//-------------------------------------------------------------------------------------------
function updateStatus(){
//-------------------------------------------------------------------------------------------
    var lintZone = 1
	var lintWindowty = '2'    
    var lintActionType = '' 
    var lintIndex = ''
    var lintMainAction = '' 
    var lstrKey = '' 
    var lobjErr
    
	if(typeof(top)!='unknown')
		
        if(typeof(top.fraFolder)!='undefined')
            if(typeof(top.fraFolder.document)!='undefined')        
                if(typeof(top.fraFolder.document.cmdAccept)!='undefined')
		            top.fraFolder.document.cmdAccept.disabled = false;
	
//+ Se habilitan/deshabilitan las acciones del ToolBar
        if(typeof(top.fraHeader)!='undefined'){
			with(top.fraHeader){
			    if (document.location.href.indexOf("InSequence")>=0 && (lintWindowty=='7' || lintWindowty=='9'))
			    	insHandImage("A390", true);
			    else
			        insHandImage("A390", !(lintZone==2 || lintWindowty==5));

			    insHandImage("A301", !(lintZone==2));
			    insHandImage("A302", !(lintZone==2));
			    insHandImage("A303", !(lintZone==2));
			    insHandImage("A304", !(lintZone==2));
			    insHandImage("A401", !(lintZone==2));
			    insHandImage("A402", !(lintZone==2));
			    insHandImage("A392", (lintZone==2 || lintWindowty==5));
			    insHandImage("A393", (lintZone==2));
			    insHandImage("A391", true);
			}
		}
        
        try{
            top.fraHeader.setPointer('');
        }
        catch(lobjErr){
			if(typeof(opener.top.fraFolder)!='undefined')
				top.fraFolder.setPointer('');
			else {
				top.setPointer('');
			}
        }
}
</SCRIPT>
<HTML>
<HEAD>
	<META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>




<SCRIPT>
//- Variable para el control de versiones
	document.VssVersion="$$Revision: 6 $|$$Date: 15-09-09 19:25 $|$$Author: Mpalleres $"

</SCRIPT>
</HEAD>
<BODY>
<FORM id=form1 name=form1>
<%

    binData = Request.BinaryRead(Request.TotalBytes)
    BuildUploadRequest(binData)

        'Response.Write "<NOTSCRIPT>alert(""" & Request.Form.ToSTring & """);</script>" 
        '+ Si no se han validado los campos de la página
        If mstrCodispl <> "GI1402_K" Then
            If Request.Form.Item("sCodisplReload") = vbNullString Then
                mstrErrors = insvalinterfaceseq()
                Session("sErrorTable") = mstrErrors
                Session("sForm") = Request.Form.ToString
            Else
                Session("sErrorTable") = vbNullString
                Session("sForm") = vbNullString
            End If
        Else
            mstrErrors = insvalinterfaceseq()
            Session("sErrorTable") = mstrErrors
            Session("sForm") = vbNullString
        End If

        If mstrErrors > vbNullString Then
            With Response
                .Write("<SCRIPT LANGUAGE=JAVASCRIPT>")
                .Write("ShowPopUp(""/VTimeNet/Common/Errors.aspx?sSource=" & Server.UrlEncode(mstrCommand) & "&sQueryString=" & Server.UrlEncode(Request.Params.Get("Query_String")) & """, ""MantGeneralError"",660,330);document.location.href='/VTimeNet/common/blank.htm';")
                .Write(mobjValues.StatusControl(False, CShort(Request.QueryString.Item("nZone")), Request.QueryString.Item("WindowType")))
                .Write("</SCRIPT>")
            End With
        Else
            If Request.QueryString.Item("nAction") <> CStr(eFunctions.Menues.TypeActions.clngAcceptdatafinish) Then
                If insPostInterface() Then
                    If Request.QueryString.Item("nZone") = "1" Then
                        If mstrLocationGI1402_K = vbNullString Then
                        If Request.Form.Item("sCodisplReload") = vbNullString Then
                            Response.Write("<SCRIPT>top.fraFolder.document.location.href=""" & Replace(UCase(Request.QueryString("sCodispl")), "_K", "") & ".aspx?nMainAction=" & Request.QueryString("nMainAction") & "&sCodispl=" & Request.QueryString("sCodispl") & mstrQueryString & """;self.history.go(-1);</SCRIPT>")
                        Else
                            Response.Write("<SCRIPT>window.close();opener.top.fraFolder.document.location=""" & Replace(UCase(Request.QueryString.Item("sCodispl")), "_K", "") & ".aspx?nMainAction=" & Request.QueryString.Item("nMainAction") & "&sCodispl=" & Request.QueryString.Item("sCodispl") & mstrQueryString & """;</SCRIPT>")
                        End If
                    Else
                        Response.Write("<SCRIPT>top.fraFolder.document.location.href=" & mstrLocationGI1402_K & mstrQueryString & ";self.history.go(-1);</SCRIPT>")
                        End If
                    Else
                        If Request.QueryString.Item("WindowType") <> "PopUp" Then
                            If Request.Form.Item("sCodisplReload") = vbNullString Then
                                Response.Write("<SCRIPT>top.frames['fraSequence'].document.location='/VTimeNet/Interface/Interfaceseq/Sequence.aspx?nAction=" & Request.QueryString.Item("nAction") & "&nOpener=" & Request.QueryString.Item("sCodispl") & "&nMainAction=" & Request.QueryString.Item("nMainAction") & mstrCommand & "';</SCRIPT>")
                            Else
                                Response.Write("<SCRIPT>window.close();opener.top.frames['fraSequence'].document.location='/VTimeNet/Interface/Interfaceseq/Sequence.aspx?nMainAction=" & Request.QueryString.Item("nAction") & "&nOpener=" & Request.QueryString.Item("sCodispl") & mstrCommand & "';</SCRIPT>")
                            End If
                        Else
                            '+ Se recarga la página que invocó la PopUp
                            Select Case Request.QueryString.Item("sCodispl")
                                Case "XXXX"
                                    Response.Write("<SCRIPT>top.opener.document.location.href='XXXX.aspx?Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("ReloadIndex") & "&sCodispl=" & Request.QueryString.Item("sCodispl") & "&nMainAction=302'</SCRIPT>")
							
                                Case Else
                                    Response.Write("<SCRIPT>top.opener.document.location.href='" & Request.QueryString.Item("sCodispl") & ".aspx?Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("ReloadIndex") & "&sCodispl=" & Request.QueryString.Item("sCodispl") & "&nMainAction=302'</SCRIPT>")
                            End Select
                        End If
                    End If
                End If
            Else
                If Request.QueryString.Item("nZone") = "2" Then
                    If Request.QueryString.Item("sCodispl") = "GI1402" Then
                        insPostInterface()
                        Response.Write("<SCRIPT>top.frames['fraSequence'].document.location='/VTimeNet/Interface/Interfaceseq/Sequence.aspx?nAction=" & Request.QueryString.Item("nAction") & "&nOpener=" & Request.QueryString.Item("sCodispl") & "&nMainAction=" & Request.QueryString.Item("nMainAction") & mstrCommand & "';</SCRIPT>")
                    Else
                        If Request.QueryString.Item("sCodispl") = "GI1405" Then
                            insPostInterface()
                            With Response
                                .Write("<SCRIPT>")
                                .Write("insReloadTop(false)")
                                .Write("</SCRIPT>")
                            End With
                        Else
                            insFinish()
                        End If
                    End If
                End If
            End If
        End If

        mobjInterface = Nothing
        mobjValues = Nothing
%>
</FORM>
</BODY>
</HTML>




