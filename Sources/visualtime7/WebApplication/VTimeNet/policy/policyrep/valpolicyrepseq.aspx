<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %> 
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eCollection" %>
<%@ Import namespace="eBatch" %>
<%@ Import namespace="eGeneral" %>
<%@ Import namespace="eSchedule" %>
<%@ Import namespace="eReports" %>
<%@ Import Namespace="System.IO" %>
<%@ Import Namespace="System.Collections.Generic" %>
<%@ Import Namespace="System.Text" %>
<script language="VB" runat="Server">
Dim nAction As Integer


'- Objeto para localización de archivos
Dim mstrPath As String

'- Objeto para el manejo de Reporte
    Dim mobjUploadRequest As Object

'- Variable para el manejo de Errores

Dim mstrErrors As String

'- Variables para el recorrido del grid
Dim lintCount As Object

Dim mstrCommand As String

Dim mobjValues As eFunctions.Values

Dim mobjBatch As Object
Dim mstrLocationCAL013 As String
    Dim nRepinsured As Integer

Dim mobjGeneral As eBatch.MasiveCharge

'- Esta variable es para indicar cuando debe pasarse a la siguiente ventana de la secuencia
'- al aceptar.  Para uso de casos particulares.
Dim lstrGoToNext As String

    Dim ScriptObject As FileStream
    Dim ByteCount As Integer
    Dim binData() As Byte
'    Dim mobjUploadRequest As Dictionary(Of String, String)
    Dim myRequestFile(4) As String
    Dim fileContentIndex As Integer
    Dim fileContentLength As Integer
    Dim crlf As String = Chr(13) & Chr(10)
    Dim mstrFileFullPath As String

'% insvalSequence: Se realizan las validaciones masivas de la forma
'--------------------------------------------------------------------------------------------
Function insvalSequence() As String
	'--------------------------------------------------------------------------------------------
	
	Dim lstrError As String=String.Empty
	Dim lstrCertype As String
	insvalSequence = vbNullString
	Session("sProcMasive") = ""
	
	mobjBatch = New eBatch.ValBatch
	
        If Request.QueryString.Item("nAction") <> eFunctions.Menues.TypeActions.clngAcceptdatafinish Then
		
            Select Case Request.QueryString.Item("sCodispl")
			
                '+ CAL013_K: Carga masiva
                Case "CAL013_K"
                    '+Se carga el archivo solo si no es proceso manual
				
                    If Not insUpLoadFile(mstrPath) Then
                        lstrError = "1977"
                    End If
                    With Request
                        lstrCertype = mobjUploadRequest("optType")
                        If lstrCertype < "1" Or lstrCertype > "3" Then
                            lstrCertype = "2"
                        End If
                        If mobjUploadRequest("hdsReinsuran") = "1" Then
                            nAction = 0
                        Else
                            nAction = mobjValues.StringToType(mobjUploadRequest("cbeAction"), eFunctions.Values.eTypeData.etdDouble, True)
                        End If
                        Session("sProcMasive") = mobjUploadRequest("hdsProcMasive")
                        If Request.QueryString.Item("sCodisplReload") = vbNullString Then
                            Session("bQuery") = (.QueryString.Item("nMainAction") = eFunctions.Menues.TypeActions.clngActionQuery)
                            Session("dEffecdate") = mobjValues.StringToType(mobjUploadRequest("tcdEffecdate"), eFunctions.Values.eTypeData.etdDate)
                            Session("nBranch") = mobjValues.StringToType(mobjUploadRequest("cbeBranch"), eFunctions.Values.eTypeData.etdDouble)
                            Session("nProduct") = mobjValues.StringToType(mobjUploadRequest("valProduct"), eFunctions.Values.eTypeData.etdDouble)
                            Session("nPolicy") = mobjValues.StringToType(mobjUploadRequest("tcnPolicy"), eFunctions.Values.eTypeData.etdDouble)
                            Session("nWorksheet") = mobjValues.StringToType(mobjUploadRequest("hdnWorksheet"), eFunctions.Values.eTypeData.etdDouble)
                            Session("sCertype") = mobjUploadRequest("optType")
                            Session("nAction") = nAction
                            Session("sContinue") = mobjUploadRequest("hdsContinue")
                            Session("dContinue") = mobjUploadRequest("tcdContinue")
                            Session("sManual") = mobjUploadRequest("hdsManual")
                            Session("sSeparator") = mobjUploadRequest("tctSeparator")
                            Session("sReinsuran") = mobjUploadRequest("hdsReinsuran")
                            Session("sFile") = mobjUploadRequest("hdtFileName")
                            Session("dExclude") = mobjUploadRequest("tcdExclude")
                            Session("sUseFile") = mobjUploadRequest("hdsCheckFile")
                            Session("nOptAct") = mobjUploadRequest("nOptAct")
                            Session("chkNoPreview") = mobjUploadRequest("hdsNoPreview")
                        End If
                    
                        insvalSequence = mobjBatch.insValCAL013_K("CAL013_K", _
                                                                                 lstrCertype, _
                                                                                 mobjValues.StringToType(mobjUploadRequest("cbeBranch"), eFunctions.Values.eTypeData.etdDouble), _
                                                                                 mobjValues.StringToType(mobjUploadRequest("valProduct"), eFunctions.Values.eTypeData.etdDouble), _
                                                                                 mobjValues.StringToType(mobjUploadRequest("tcnPolicy"), eFunctions.Values.eTypeData.etdDouble), _
                                                                                 mobjValues.StringToType(mobjUploadRequest("hdnWorksheet"), eFunctions.Values.eTypeData.etdDouble), _
                                                                                 mobjUploadRequest("hdtFileName"), _
                                                                                 nAction, _
                                                                                 mobjValues.StringToType(mobjUploadRequest("tcdEffecdate"), eFunctions.Values.eTypeData.etdDate), _
                                                                                 Session("sProcMasive"), _
                                                                                 mobjUploadRequest("hdsReinsuran"), _
                                                                                 mobjUploadRequest("hdsManual"), _
                                                                                 Session("sUseFile"))
					
                    End With
                    Session("nContent") = 0
                    mstrLocationCAL013 = "'/VTimeNet/Common/secWHeader.aspx?sCodispl=CAL013_K&sProject=PolicyRep&sModule=Policy&nHeight=140&sConfig=InSequence&nAction=0" & Request.QueryString.Item("nMainAction") & "&bMenu=1'"
				
                Case "CAL659"
                    insvalSequence = vbNullString
				
                Case "CAL660"
                    If Request.QueryString.Item("WindowType") = "PopUp" Then
                        insvalSequence = mobjBatch.insValCAL660(Request.Form.Item("cbeValue"))
                    Else
                        insvalSequence = vbNullString
                    End If
				
                Case Else
                    insvalSequence = "insvalSequence: Código lógico no encontrado (" & Request.QueryString.Item("sCodispl") & ")"
            End Select
        Else
		
            '+ CAL013: Verificacion de que la secuencia esta terminada
		
            insvalSequence = mobjBatch.insValCAL013(Session("nContent"))
		
        End If
End Function

'% insPostSequence: Se realizan las actualizaciones de las ventanas
'--------------------------------------------------------------------------------------------
    Function insPostSequence() As Boolean
        '--------------------------------------------------------------------------------------------
        Dim lblnPost As Boolean
        Dim lclsGeneral As eGeneral.GeneralFunction
        Dim lstrField As Object
        Dim lstrValues As String
        Dim lclsBatch_param As eSchedule.Batch_param
        Dim lclsColsheet As eBatch.Colsheet
        Dim lclsPolicy As ePolicy.Policy
        Dim lclsProduct As eProduct.Product
        Select Case Request.QueryString.Item("sCodispl")
		
            '+ CAL013_K: Solicitud De Clave
            Case "CAL013_K"
			
                '     mobjBatch = New eBatch.ValBatch
                lclsColsheet = New eBatch.Colsheet
                lclsGeneral = New eGeneral.GeneralFunction
                Session("sKey") = lclsGeneral.getsKey(Session("nUsercode"))
                lblnPost = True
                lclsGeneral = Nothing
                lclsPolicy = New ePolicy.Policy
                lclsProduct = New eProduct.Product
                If CStr(Session("sProcMasive")) = "1" Then
                    Call lclsPolicy.Find(Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"))
                    Call lclsProduct.Find(Session("nBranch"), Session("nProduct"), Session("dEffeddate"))
                    nRepinsured = lclsPolicy.nRepInsured
                    If lclsPolicy.sMassive <> "1" and  Session("sReinsuran") <> "1" Then
                        Session("sProcMasive") = "2"
                        mstrFileFullPath = Session("sFile")
                    End If
                End If
                
                
                ' Si se esta procesando la transaccion CA658 - Nómina de cotización (Vida colectivo)                       
                ' Se almacena la variable BatchEnabled para luego ser reestablecida segun la configuracion y se 
                ' desactiva el manejo de procesos batch
                If Session("sLinkSpecial") = "CA658" Then
                    Session("BatchEnabled_Bkp") = Session("BatchEnabled")
                    Session("BatchEnabled") = "2"
                End If

                '+ Cálculo de Nómina Temporal Retroactiva
                If Session("nAction") = 6 Then
                    If CStr(Session("BatchEnabled")) <> "1" Then
                        With Request
                            mobjBatch = New eBatch.MasiveCharge
                            lblnPost = mobjBatch.InsCalTmp_Cal013_List(Session("sCertype"), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), Session("sKey"), mobjValues.StringToType(Session("nWorksheet"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble), Session("sUseFile"), Session("sFile"))
                        End With
                    Else
                        lclsBatch_param = New eSchedule.Batch_Param
                        With lclsBatch_param
                            .nBatch = 153
                            .sKey = Session("sKey")
                            .nUsercode = mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble)
                            .Add(eSchedule.Batch_job.enmAreaParameters.batchParAreaProc, Session("sCertype"))
                            .Add(eSchedule.Batch_job.enmAreaParameters.batchParAreaProc, mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble))
                            .Add(eSchedule.Batch_job.enmAreaParameters.batchParAreaProc, mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble))
                            .Add(eSchedule.Batch_job.enmAreaParameters.batchParAreaProc, mobjValues.StringToType(Session("nPolicy"), eFunctions.Values.eTypeData.etdDouble))
                            .Add(eSchedule.Batch_job.enmAreaParameters.batchParAreaProc, mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate))
                            .Add(eSchedule.Batch_job.enmAreaParameters.batchParAreaProc, .sKey)
                            .Add(eSchedule.Batch_job.enmAreaParameters.batchParAreaProc, mobjValues.StringToType(Session("nWorksheet"), eFunctions.Values.eTypeData.etdDouble))
                            .Add(eSchedule.Batch_job.enmAreaParameters.batchParAreaProc, mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble))
                            .Add(eSchedule.Batch_job.enmAreaParameters.batchParAreaProc, Session("sUseFile"))
                            .Add(eSchedule.Batch_job.enmAreaParameters.batchParAreaProc, Session("sFile"))
                            .Save()
                        End With
                        Response.Write("<SCRIPT>alert('Se generó la clave de proceso: " & lclsBatch_param.sKey & "');</" & "Script>")
					
                        lclsBatch_param = Nothing
                        lblnPost = True
                    End If
                Else
                    '+ Impresión de Nómina
                    If Session("nAction") = 8 Then
                        If CStr(Session("BatchEnabled")) <> "1" Then
                            With Request
                                mobjBatch = New eBatch.MasiveCharge
                                lblnPost = mobjBatch.InsPrintTmp_Cal013_List(Session("sCertype"), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), Session("sKey"), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble))
                            End With
                        Else
                            lclsBatch_param = New eSchedule.Batch_Param
                            With lclsBatch_param
                                .nBatch = 154
                                .sKey = Session("sKey")
                                .nUsercode = mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble)
                                .Add(eSchedule.Batch_job.enmAreaParameters.batchParAreaProc, Session("sCertype"))
                                .Add(eSchedule.Batch_job.enmAreaParameters.batchParAreaProc, mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble))
                                .Add(eSchedule.Batch_job.enmAreaParameters.batchParAreaProc, mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble))
                                .Add(eSchedule.Batch_job.enmAreaParameters.batchParAreaProc, mobjValues.StringToType(Session("nPolicy"), eFunctions.Values.eTypeData.etdDouble))
                                .Add(eSchedule.Batch_job.enmAreaParameters.batchParAreaProc, mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate))
                                .Add(eSchedule.Batch_job.enmAreaParameters.batchParAreaProc, .sKey)
                                .Add(eSchedule.Batch_job.enmAreaParameters.batchParAreaProc, mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble))
                                .Add(eSchedule.Batch_job.enmAreaParameters.batchParAreaRes, .sKey)
                                .Save()
                            End With
                            Response.Write("<SCRIPT>alert('Se generó la clave de proceso: " & lclsBatch_param.sKey & "');</" & "Script>")
						
                            lclsBatch_param = Nothing
                            lblnPost = True
                        End If
                    Else
                        '+ Carga Masiva (Archivos de texto)
                        If CStr(Session("sProcMasive")) = "1" Then
						
                            If CStr(Session("BatchEnabled")) <> "1" Then
                                lblnPost = True
                            Else
                                lclsBatch_param = New eSchedule.Batch_Param
                                With lclsBatch_param
                                    .nBatch = 150
                                    .nUsercode = mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble)
                                    .Add(eSchedule.Batch_job.enmAreaParameters.batchParAreaProc, Session("sCertype"))
                                    .Add(eSchedule.Batch_job.enmAreaParameters.batchParAreaProc, mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble))
                                    .Add(eSchedule.Batch_job.enmAreaParameters.batchParAreaProc, mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble))
                                    .Add(eSchedule.Batch_job.enmAreaParameters.batchParAreaProc, mobjValues.StringToType(Session("nPolicy"), eFunctions.Values.eTypeData.etdDouble))
                                    .Add(eSchedule.Batch_job.enmAreaParameters.batchParAreaProc, mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate))
                                    .Add(eSchedule.Batch_job.enmAreaParameters.batchParAreaProc, mobjValues.StringToType(Session("nAction"), eFunctions.Values.eTypeData.etdDouble))
                                    .Add(eSchedule.Batch_job.enmAreaParameters.batchParAreaProc, .sKey)
                                    .Add(eSchedule.Batch_job.enmAreaParameters.batchParAreaProc, mobjValues.StringToType(Session("nWorksheet"), eFunctions.Values.eTypeData.etdDouble))
                                    .Add(eSchedule.Batch_job.enmAreaParameters.batchParAreaProc, Session("sSeparator"))
                                    .Add(eSchedule.Batch_job.enmAreaParameters.batchParAreaProc, mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble))
                                    .Add(eSchedule.Batch_job.enmAreaParameters.batchParAreaProc, Session("sFile"))
                                    .Add(eSchedule.Batch_job.enmAreaParameters.batchParAreaProc, Session("sContinue"))
                                    .Add(eSchedule.Batch_job.enmAreaParameters.batchParAreaProc, Session("dContinue"))
                                    .Add(eSchedule.Batch_job.enmAreaParameters.batchParAreaProc, Session("sReinsuran"))
                                    .Add(eSchedule.Batch_job.enmAreaParameters.batchParAreaRes, mobjValues.StringToType(Session("nAction"), eFunctions.Values.eTypeData.etdDouble))
                                    .Add(eSchedule.Batch_job.enmAreaParameters.batchParAreaRes, .sKey)
                                    .Add(eSchedule.Batch_job.enmAreaParameters.batchParAreaRes, mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate))
                                    .Save()
                                End With
                                Response.Write("<SCRIPT>alert('Se generó la clave de proceso: " & lclsBatch_param.sKey & "');</" & "Script>")
							
                                lclsBatch_param = Nothing
                            End If
                        Else
                            If CStr(Session("sManual")) = "2" Then
                                If Session("nAction") = 7 Then
                                    If CStr(Session("BatchEnabled")) <> "1" Then
                                        With Request
                                            mobjBatch = New eBatch.MasiveCharge
                                            lblnPost = mobjBatch.insPostDelCal013(Session("sCertype"), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nPolicy"), eFunctions.Values.eTypeData.etdDouble), Session("sKey"))
                                        End With
                                    Else
                                        lclsBatch_param = New eSchedule.Batch_Param
                                        With lclsBatch_param
                                            .nBatch = 152
                                            .sKey = Session("sKey")
                                            .nUsercode = mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble)
                                            .Add(eSchedule.Batch_job.enmAreaParameters.batchParAreaProc, Session("sCertype"))
                                            .Add(eSchedule.Batch_job.enmAreaParameters.batchParAreaProc, mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble))
                                            .Add(eSchedule.Batch_job.enmAreaParameters.batchParAreaProc, mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble))
                                            .Add(eSchedule.Batch_job.enmAreaParameters.batchParAreaProc, mobjValues.StringToType(Session("nPolicy"), eFunctions.Values.eTypeData.etdDouble))
                                            .Add(eSchedule.Batch_job.enmAreaParameters.batchParAreaProc, .sKey)
                                            .Save()
                                        End With
                                        Response.Write("<SCRIPT>alert('Se generó la clave de proceso: " & lclsBatch_param.sKey & "');</" & "Script>")
									
                                        lclsBatch_param = Nothing
                                        lblnPost = True
                                    End If
                                Else
                                    mobjBatch = New eBatch.ValBatch
                                    lblnPost = mobjBatch.insPostCAL013_k(Session("nWorksheet"), mstrFileFullPath, Session("sKey"), Session("sSeparator"), nRepinsured, Session("nUsercode"))
                                    If Not lblnPost Then
                                        Response.Write("<SCRIPT>alert('" & mobjBatch.sError & "');</" & "Script>")
                                    End If
                                    mobjBatch = Nothing
                                End If
                            Else
                                lblnPost = True
                            End If
                        End If
                    End If

                    ' Se reestablece el valor de la sesion que tiene configurado el sistema para los procesos batch
                    If Session("sLinkSpecial") = "CA658" Then
                        Session("BatchEnabled") = Session("BatchEnabled_Bkp")
                    End If
                    
                End If
			
            Case "CAL013"
                lblnPost = True
			
            Case "CAL659"
			
                If Not IsNothing(Request.Form) Then
                    For Each lstrField In Request.Form
                        If Right(lstrField, 3) <> "Val" Then
                            If Left(lstrField, 3) <> "hdd" Then
                                'Response.Write lstrField & "=" & Request.Form(lstrField) & "<BR>"
                                If Len(lstrValues) > 0 Then
                                    lstrValues = lstrValues & "||"
                                End If
                                lstrValues = lstrValues & Mid(lstrField, 2, 2) & "=" & Request.Form.Item(lstrField)
                            End If
                        End If
                    Next lstrField
                End If
                lblnPost = mobjBatch.insPostCal659(Session("sKey"), "Add", lstrValues, Session("nWorksheet"))
			
            Case "CAL660"
                mobjBatch = New eBatch.MasiveCharge
                With Request
                    If Request.QueryString.Item("WindowType") = "PopUp" Then
                        lblnPost = mobjBatch.insPostCal660(Session("sKey"), .Form.Item("hddsField"), .Form.Item("tctValue"), .Form.Item("hddsTable"), .Form.Item("cbeValue"))
                    Else
                        lblnPost = True
                    End If
                End With
        End Select
	
        insPostSequence = lblnPost
    End Function

    '% insUpLoadFile: Se encarga de subir el archivo seleccionado al servidor según ruta pasada como parámetro.
    '% FilePath: Ruta física donde se va almacenar el archivo en el servidor. Eje. "c:\InetPub\UpLoad\"
    '--------------------------------------------------------------------------------------------
    Function insUpLoadFile(ByRef FilePath As String) As Boolean
        '--------------------------------------------------------------------------------------------

        Dim sFilename As String = Path.GetFileName(myRequestFile(2))
        Dim sSavePath As String
        Dim fileAppend As Integer
        Dim lobjValues As eFunctions.Values
        
        lobjValues = New eFunctions.Values
        lobjValues.sSessionID = Session.SessionID
        lobjValues.sCodisplPage = "valpolicyrepseq"
    
    
        sSavePath = Trim(UCase(lobjValues.insGetSetting("MASSIVELOAD", String.Empty, "PATHS")))
               
        If String.IsNullOrEmpty(sFilename) Then Return False
        
        Do While File.Exists(sSavePath & "\" & sFilename)
            fileAppend += 1
            sFilename = Path.GetFileNameWithoutExtension(myRequestFile(2)) & fileAppend.ToString & _
                Path.GetExtension(myRequestFile(2))
        Loop

        Dim newFile As FileStream = Nothing
        
        newFile = New FileStream(sSavePath & "\" & sFilename, FileMode.Create)
            
        'For i As Integer = fileContentIndex To fileContentLength
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
                        
                        fileContentIndex = (New String(postData)).IndexOf(varValue)
                        
                        fileContentLength = varValue.Length
                        
                    Else
                        mobjUploadRequest.Add(getFieldName(varInfo), varValue)
                    End If
                End If
            Next
        End If
    End Sub


    '% insFinish: se activa al finalizar el proceso
    '--------------------------------------------------------------------------------------------
    Function insFinish() As Boolean
        '--------------------------------------------------------------------------------------------
        Dim lobjDocuments As eReports.Report
        Dim ldtmEffecdate As Date

        Dim lclsBatch_param As eSchedule.Batch_param
	
        mobjBatch = New eBatch.MasiveCharge

        If Not Session("bQuery") Then
            
            ' Si se esta procesando la transaccion CA658 - Nómina de cotización (Vida colectivo)                       
            ' Se almacena la variable BatchEnabled para luego ser reestablecida segun la configuracion y se 
            ' desactiva el manejo de procesos batch
            If Session("sLinkSpecial") = "CA658" Then
                Session("BatchEnabled_Bkp") = Session("BatchEnabled")
                Session("BatchEnabled") = "2"
            End If

            If CStr(Session("BatchEnabled")) <> "1" Then
                With Request
                    insFinish = mobjBatch.insPostCal013(Session("sCertype"), _
                                                                    mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), _
                                                                    mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), _
                                                                    mobjValues.StringToType(Session("nPolicy"), eFunctions.Values.eTypeData.etdDouble), _
                                                                    mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), _
                                                                    mobjValues.StringToType(Session("nAction"), eFunctions.Values.eTypeData.etdDouble), _
                                                                    Session("sKey"), _
                                                                    mobjValues.StringToType(Session("nWorksheet"), eFunctions.Values.eTypeData.etdDouble), _
                                                                    mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble), _
                                                                    Session("sTypeage"), Session("sContinue"), mobjValues.StringToDate(Session("dContinue")), _
                                                                    mobjValues.StringToDate(Session("dExclude")), Session("nOptAct"))
				
				
                    If insFinish Then
                        If mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate) = eRemoteDB.Constants.dtmNull Then
                            ldtmEffecdate = Today
                        Else
                            ldtmEffecdate = mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate)
                        End If
					
                        If Session("nAction") <> 4 Then
                            lobjDocuments = New eReports.Report
                            With lobjDocuments
                                .sCodispl = "CAL013"
                                .ReportFilename = "CAL013A.rpt"
                                .setStorProcParam(1, Session("sKey"))
                                .setStorProcParam(2, Session("nAction"))
                                .setStorProcParam(3, .setdate(ldtmEffecdate))
                                .setStorProcParam(4, Session("nOptAct"))
                                Response.Write((.Command))
                            End With
                            lobjDocuments = Nothing
                        End If
                        lobjDocuments = New eReports.Report
                        With lobjDocuments
                            .sCodispl = "CAL013"
                            .ReportFilename = "CAL013.rpt"
                            .setStorProcParam(1, Session("sKey"))
                            .setStorProcParam(2, Session("nAction"))
                            .setStorProcParam(3, Session("nOptAct"))
                            Response.Write((.Command))
                            lobjDocuments = Nothing
                        End With
					
                    End If
                End With
            Else

                If Session("dEffecdate") = eRemoteDB.Constants.dtmNull Then
                    ldtmEffecdate = Today
                Else
                    ldtmEffecdate = mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate)
                End If
			    
                lclsBatch_param = New eSchedule.Batch_Param
                With lclsBatch_param
                    .nBatch = 113
                    .sKey = Session("sKey")
                    .nUsercode = mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble)
                    .Add(eSchedule.Batch_job.enmAreaParameters.batchParAreaProc, Session("sCertype"))
                    .Add(eSchedule.Batch_job.enmAreaParameters.batchParAreaProc, mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble))
                    .Add(eSchedule.Batch_job.enmAreaParameters.batchParAreaProc, mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble))
                    .Add(eSchedule.Batch_job.enmAreaParameters.batchParAreaProc, mobjValues.StringToType(Session("nPolicy"), eFunctions.Values.eTypeData.etdDouble))
                    .Add(eSchedule.Batch_job.enmAreaParameters.batchParAreaProc, mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate))
                    .Add(eSchedule.Batch_job.enmAreaParameters.batchParAreaProc, mobjValues.StringToType(Session("nAction"), eFunctions.Values.eTypeData.etdDouble))
                    .Add(eSchedule.Batch_job.enmAreaParameters.batchParAreaProc, .sKey)
                    .Add(eSchedule.Batch_job.enmAreaParameters.batchParAreaProc, mobjValues.StringToType(Session("nWorksheet"), eFunctions.Values.eTypeData.etdDouble))
                    .Add(eSchedule.Batch_job.enmAreaParameters.batchParAreaProc, mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble))
                    .Add(eSchedule.Batch_job.enmAreaParameters.batchParAreaProc, Session("sTypeage"))
                    .Add(eSchedule.Batch_job.enmAreaParameters.batchParAreaProc, Session("sContinue"))
                    .Add(eSchedule.Batch_job.enmAreaParameters.batchParAreaProc, Session("dContinue"))
                    .Add(eSchedule.Batch_job.enmAreaParameters.batchParAreaProc, Session("dExclude"))
                    .Add(eSchedule.Batch_job.enmAreaParameters.batchParAreaProc, Session("nOptAct"))
                    .Add(eSchedule.Batch_job.enmAreaParameters.batchParAreaProc, Session("chkNoPreview"))
                    .Add(eSchedule.Batch_job.enmAreaParameters.batchParAreaRes, Session("nAction"))
                    .Add(eSchedule.Batch_job.enmAreaParameters.batchParAreaRes, .sKey)
                    .Add(eSchedule.Batch_job.enmAreaParameters.batchParAreaRes, ldtmEffecdate)
                    .Add(eSchedule.Batch_job.enmAreaParameters.batchParAreaRes, Session("nOptAct"))

                    .Save()
                End With
            
                Response.Write("<SCRIPT>alert('Se generó la clave de proceso: " & lclsBatch_param.sKey & "');</" & "Script>")
			
                lclsBatch_param = Nothing
			
                insFinish = True
			
            End If

            ' Se reestablece el valor de la sesion que tiene configurado el sistema para los procesos batch
            If Session("sLinkSpecial") = "CA658" Then
                Session("BatchEnabled") = Session("BatchEnabled_Bkp")
            End If
		
        Else
            insFinish = True
        End If
	
	mstrLocationCAL013 = "'/VTimeNet/Common/secWHeader.aspx?sCodispl=CAL013_K&sProject=PolicyRep&sModule=Policy&nHeight=130&sCodisp=CAL013'"
	
End Function

</script>
<%Response.Expires = -1

%>
<HTML>
<HEAD>
	<LINK REL="StyleSheet" TYPE="text/css" HREF="../../Common/Custom.css">  
    <SCRIPT LANGUAGE="JavaScript" SRC="../../Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT>
//+ Variable para el control de versiones
     document.VssVersion="$$Revision: 9 $|$$Date: 5/06/06 6:09p $|$$Author: Clobos $"

//% CancelErrors: Regresa a la Página Anterior
//------------------------------------------------------------------------------
function CancelErrors()
//------------------------------------------------------------------------------
{
self.history.go(-1)
}

//% NewLocation: Establece la Localizacion de la Pagina que se este trabajando.
//------------------------------------------------------------------------------
function NewLocation(Source,Codisp)
//------------------------------------------------------------------------------
{
    var lstrLocation = "";
    lstrLocation += Source.location;
    lstrLocation = lstrLocation.replace(/&OPENER=.*/,"") + "&OPENER=" + Codisp;
    Source.location = lstrLocation
}
</SCRIPT>

</HEAD>

<%If CDbl(Request.QueryString.Item("nZone")) = 1 Then
	%><BODY><%	
Else
	%><BODY CLASS="Header"><%	
End If
%>
<FORM>
<%
mobjValues = New eFunctions.Values

mobjValues.sCodisplPage = "valpolicyrepseq"

Response.Write(mobjValues.StyleSheet())

mobjGeneral = New eBatch.MasiveCharge

mstrPath = mobjGeneral.GetLoadFile(True)

mobjGeneral = Nothing

mstrCommand = "&sModule=Policy&sProject=PolicyRep&sCodisplReload=" & Request.QueryString.Item("sCodispl")

binData = Request.BinaryRead(Request.TotalBytes)
BuildUploadRequest(binData)
	'+ Se toman los valores de los objectos que recibe la forma	
        'mlngImagenum = mobjUploadRequest("tcnImagenum").ToString
        'mintConsec = mobjUploadRequest("tcnConsec").ToString
        'mstrDescript = mobjUploadRequest("tctDescript").ToString
        'mdtmCompdate = mobjUploadRequest("tcdCompdate").ToString
        'mdtmNulldate = mobjUploadRequest("tcdNulldate").ToString
        'mintUsercode = mobjUploadRequest("tcnUsercode").ToString
        'mintRectype = mobjUploadRequest("nRectype").ToString
        'mstrCodispl = mobjUploadRequest("sCodispl").ToString

'+ Si no se han validado los campos de la página
If Request.QueryString.Item("sCodispl") <> "CAL013_K" Then
	If Request.Form.Item("sCodisplReload") = vbNullString Then
		mstrErrors = insvalSequence
		Session("sErrorTable") = mstrErrors
		Session("sForm") = Request.Form.ToString
	Else
		Session("sErrorTable") = vbNullString
		Session("sForm") = vbNullString
	End If
Else
	If Request.QueryString.Item("sCodisplReload") = vbNullString Then
		mstrErrors = insvalSequence
		Session("sErrorTable") = mstrErrors
		Session("sForm") = vbNullString
	Else
		Session("sErrorTable") = vbNullString
		Session("sForm") = vbNullString
	End If
End If


If mstrErrors > vbNullString Then
	With Response
		.Write("<SCRIPT LANGUAGE=JAVASCRIPT>")
		.Write("ShowPopUp(""/VTimeNet/Common/Errors.aspx?sSource=" & Server.URLEncode(mstrCommand) & "&sQueryString=" & Server.URLEncode(Request.Params.Get("Query_String")) & "&sValPage=policyrepseq" & """, ""PolicyRepErrors"",660,330);")
            .Write(mobjValues.StatusControl(False, CShort(Request.QueryString.Item("nZone")), Request.QueryString.Item("WindowType")))
		.Write("</SCRIPT>")
	End With
Else
        If Request.QueryString.Item("nAction") <> eFunctions.Menues.TypeActions.clngAcceptdatafinish Then
            If insPostSequence() Then
                If Session("chkNoPreview") = "1" Then
                    If insFinish() Then
                        If CStr(Session("sLinkSpecial")) = "" Then
                            Response.Write("<SCRIPT>top.document.location.href=" & mstrLocationCAL013 & ";</SCRIPT>")
                        Else
                            Response.Write("<SCRIPT>top.opener.top.frames[""fraFolder""].location.reload();</SCRIPT>")
                            Response.Write("<SCRIPT>top.close();</SCRIPT>")
                        End If
                    End If
                Else
                    If Request.QueryString.Item("WindowType") <> "PopUp" Then
                        '+nAction => 6 -> Calculo Nómina Temporal Retroactiva
                        '+nAction => 7 -> Elimna Nómina Temporal Retroactiva
                        '+nAction => 8 -> Impresión de Nómina
                        If Session("nAction") = 7 Or Session("nAction") = 6 Or Session("nAction") = 8 Then
                            mstrLocationCAL013 = "'/VTimeNet/Common/secWHeader.aspx?sCodispl=CAL013_K&sProject=PolicyRep&sModule=Policy&nHeight=140&sCodisp=CAL013'"
                            Response.Write("<SCRIPT>top.document.location.href=" & mstrLocationCAL013 & ";</SCRIPT>")
                        Else
                            If CStr(Session("sProcMasive")) <> "1" Then
                                If mstrLocationCAL013 = vbNullString Then
                                    lstrGoToNext = "Yes"
                                    If Request.Form.Item("sCodisplReload") = vbNullString Then
                                        Response.Write("<SCRIPT>top.frames['fraSequence'].document.location=""/VTimeNet/Policy/PolicyRep/Sequence.aspx?nAction=" & Request.QueryString.Item("nMainAction") & "&sGoToNext=" & lstrGoToNext & "&nOpener=" & Request.QueryString.Item("sCodispl") & """;</SCRIPT>")
                                    Else
                                        If Request.Form.Item("sCodisplReload") = "CAL013_K" Then
                                            mstrLocationCAL013 = "'/VTimeNet/Common/secWHeader.aspx?sCodispl=CAL013_K&sProject=PolicyRep&sModule=Policy&nHeight=140&sConfig=InSequence&nAction=0" & Request.QueryString.Item("nMainAction") & "&bMenu=1'"
                                            Response.Write("<SCRIPT>window.close();opener.top.document.location.href=" & mstrLocationCAL013 & ";</SCRIPT>")
                                            'Response.Write("<SCRIPT>window.close();opener.top.frames['fraSequence'].document.location=""/VTimeNet/Policy/PolicyRep/Sequence.aspx?nAction=" & Request.QueryString.Item("nMainAction") & "&sGoToNext=" & lstrGoToNext & "&nOpener=" & Request.QueryString.Item("sCodispl") & """;</SCRIPT>")
                                        Else
                                            Response.Write("<SCRIPT>window.close();opener.top.frames['fraSequence'].document.location=""/VTimeNet/Policy/PolicyRep/Sequence.aspx?nAction=" & Request.QueryString.Item("nMainAction") & "&sGoToNext=" & lstrGoToNext & "&nOpener=" & Request.QueryString.Item("sCodispl") & """;</SCRIPT>")
                                        End If
                                        
                                    
                                    End If
                                Else
                                    Response.Write("<SCRIPT>top.document.location.href=" & mstrLocationCAL013 & ";</SCRIPT>")
                                End If
                            Else
                                If Request.Form.Item("sCodisplReload") = vbNullString Then
                                    mstrLocationCAL013 = "'/VTimeNet/Common/secWHeader.aspx?sCodispl=CAL013_K&sProject=PolicyRep&sModule=Policy&nHeight=140&sCodisp=CAL013'"
                                    Response.Write("<SCRIPT>top.document.location.href =" & mstrLocationCAL013 & ";</SCRIPT>")
                                Else
                                    mstrLocationCAL013 = "'/VTimeNet/Common/secWHeader.aspx?sCodispl=CAL013_K&sProject=PolicyRep&sModule=Policy&nHeight=140&sCodisp=CAL013'"
                                    Response.Write("<SCRIPT>window.close();opener.top.document.location.href=" & mstrLocationCAL013 & ";</SCRIPT>")
                                End If
                            End If
                        End If
                    Else
                        '+ Se recarga la página que invocó la PopUp
                        Select Case Request.QueryString.Item("sCodispl")
                            Case "CAL659", "CAL660"
                                If Request.QueryString.Item("EditWithoutPopPup") = "True" Then
                                    'Response.Write "<NOTSCRIPT>top.fraFolder.document.location.href='" & Request.QueryString("sCodispl") & ".aspx?sCodispl=" & Request.QueryString("sCodispl") & "&Reload=" & Request.Form("chkContinue") & "&ReloadAction=" & Request.QueryString("Action") & "&ReloadIndex=0" & Request.QueryString("ReloadIndex") & "&nMainAction=" & Request.QueryString("nMainAction") & mstrQueryString & "'</SCRIPT>"
                                    Response.Write("<SCRIPT>top.fraFolder.document.location.href='" & Request.QueryString.Item("sCodispl") & ".aspx?Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("Index") & "'</SCRIPT>")
                                Else
                                    Response.Write("<SCRIPT>top.opener.document.location.href ='" & Request.QueryString.Item("sCodispl") & ".aspx?Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("Index") & "'</SCRIPT>")
                                End If
                                'Response.Write "<NOTSCRIPT>top.opener.document.URL='" & Request.QueryString("sCodispl") & ".aspx?Reload=" & Request.Form("chkContinue") & "&ReloadAction=" & Request.QueryString("Action") & "&ReloadIndex=" & Request.QueryString("Index") & "'</SCRIPT>"
                        End Select
                    End If
                End If
            Else
                Response.Write("<SCRIPT>alert('La plantilla tiene formato inválido')</SCRIPT>")
                mstrLocationCAL013 = "'/VTimeNet/Common/secWHeader.aspx?sCodispl=CAL013_K&sProject=PolicyRep&sModule=Policy&nHeight=140&sCodisp=CAL013'"
                Response.Write("<SCRIPT>opener.top.document.location.href=" & mstrLocationCAL013 & ";window.close();</SCRIPT>")
            End If
        Else
            If insFinish() Then
                If CStr(Session("sLinkSpecial")) = "" Then
                    Response.Write("<SCRIPT>top.document.location.href=" & mstrLocationCAL013 & ";</SCRIPT>")
                Else
                    Response.Write("<SCRIPT>top.opener.top.frames[""fraFolder""].location.reload();</SCRIPT>")
                    Response.Write("<SCRIPT>top.close();</SCRIPT>")
                End If
            End If
        End If
End If

mobjBatch = Nothing
mobjValues = Nothing
mobjUploadRequest = Nothing

%>
</FORM>
</BODY>
</HTML>





