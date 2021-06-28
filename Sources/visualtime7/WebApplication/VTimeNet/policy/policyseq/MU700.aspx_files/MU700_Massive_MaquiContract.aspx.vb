Imports Microsoft.VisualBasic

Imports System.Globalization

Imports eNetFrameWork
Imports eFunctions
Imports System.Data
Imports eFunctions.Values
Imports eRemoteDB.Parameter
Imports eProduct
Imports ePolicy

Imports System.IO

Public Class MU700_Massive_MaquiContract_aspx
    Inherits InMotionGIT.Web.Page.BackOfficeCommon


    public mobjValues As New eFunctions.Values
    Dim myRequestFile(4) As String
    Dim fileContentIndex As Integer
    Dim fileContentLength As Integer
    Dim crlf As String = Chr(13) & Chr(10)
    Dim binData() As Byte
    Dim mstrFileFullPath As String
    Dim mobjGeneral As New  eBatch.MasiveCharge
    'Dim mstrPath As String

    Dim sMassiveChargeDirectory As String

    Dim resxValues As IEnumerable(Of DictionaryEntry) = eFunctions.Values.GetResxValue("MU700")
    
    Dim lstrCommand As String = "sModule=Policy&sProject=PolicySeq&sCodisplReload=" & "MU700"
    Dim lstrQueryString As String = ""
    Dim lstrErrors As String = ""
            Protected Overrides Sub OnLoad(ByVal e As System.EventArgs)
                MyBase.OnLoad(e)
        
                If Request.QueryString("MassiveMaquiContract") = "1" Then
		            If MassiveMaquiContract() Then
                            Response.Write("<script> opener.document.location.reload(); </script>")
                    End If
                End If
        
            End Sub
    
    Private Function MassiveMaquiContract() As Boolean
        Try

            sMassiveChargeDirectory = mobjGeneral.GetLoadFile(True)
        
            binData = Request.BinaryRead(Request.TotalBytes)
        
            If BuildUploadRequest(binData) Then
        
	            insUpLoadFile(sMassiveChargeDirectory)
        
	            Dim colSheet As New eBatch.Colsheet
	            Response.Write(colSheet.sMessage)
        
	            insTransformationExcel(Path.GetFileNameWithoutExtension(myRequestFile(2)))
            
                If FileToArray(sMassiveChargeDirectory & Path.GetFileNameWithoutExtension(myRequestFile(2)) & ".txt") Then

                Return True
                Else
                    Return False
                End If
            Else 
                Response.Write("<script> alert('Debe seleccionar un archivo') </script>")
                Return False
            End If

        Catch ex As Exception
            Session("sErrorTable") = ex.ToString()
            Session("sForm") = Request.Form.ToString()

            Response.Write("<script type='text/javascript' language='JavaScript' src='/VTimeNet/Scripts/GenFunctions.js'></script>")
            Response.Write("<script type='text/javascript'> ")
            Response.Write(" ShowPopUp(""/VTimeNet/Common/Errors.aspx?sSource=" & Server.UrlEncode(lstrCommand) & "&sQueryString=" & Server.UrlEncode(Request.Params.Get("Query_String")) & lstrQueryString & """, ""PolicySeqError"",660,330); ")
            Response.Write(" </script>")

            'Response.Write("<script> alert('No se logró cargar la plantilla, verifique e intente nuevamente') </script>")
        End Try
        
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

        'Dim newFile As FileStream = Nothing
        
        Using newFile As FileStream = New FileStream(sSavePath & "\" & sFilename, FileMode.Create)
            
        'For i As Integer = fileContentIndex To fileContentLength
        For i As Integer = 0 To fileContentLength - 1
            newFile.WriteByte(binData(fileContentIndex + i))
        Next
        
        newFile.Close()
            
        End Using

        If File.Exists(sSavePath & "\" & sFilename) Then
            mstrFileFullPath = sSavePath & "\" & sFilename
            Return True
        Else
            mstrFileFullPath = String.Empty
            Return False
        End If
	
    End Function    
    
    public Function BuildUploadRequest(ByVal data() As Byte) As Boolean
        'Array que contendrá la data decodificada
        Dim postData(data.Length) As Char
    
        'Se inicializa el decodificador ASCII
        Dim decoder As Decoder = Encoding.ASCII.GetDecoder
        
        Dim result As Boolean = false

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
            Dim mobjUploadRequest = New Dictionary(Of String, String)
        
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
            If getFileName(varInfo) <> "" Then
                result = true
            End If
        End If
        Return result
    End Function    
    
    ' This function retreives a field's name
    public Function getFieldName(ByVal infoStr As String) As String
        Dim sPos As Integer = infoStr.IndexOf("name=")
        Dim endPos As Integer = infoStr.Substring(sPos + 5).IndexOf(Chr(34) & ";")
        If endPos = -1 Then
            endPos = infoStr.Substring(sPos + 6).IndexOf(Chr(34))
        End If
        
        Return infoStr.Substring(sPos + 6, endPos)
    End Function    
    
    ' This function retreives a file field's mime type
    public Function getFileType(ByVal infoStr As String) As String
        Dim sPos As Integer = infoStr.IndexOf("Content-Type: ")
        Return infoStr.Substring(sPos + 14)
    End Function    
    
    ' This function retreives a file field's filename
    Function getFileName(ByVal infoStr As String) As String
        Dim sPos As Integer = infoStr.IndexOf("filename=")
        Dim endPos As Integer = infoStr.IndexOf(Chr(34) & crlf)
        getFileName = Path.GetFileName(infoStr.Substring(sPos + 10, endPos - (sPos + 10)))
    End Function    
    
    Dim lstrArray(,) As Object
    Public Function  FileToArray(ByVal lstrFile As String) As Boolean
        
        Dim lstrFiledeltxt As String = lstrFile
        Dim sMessage As string
        Dim lintFileNum As Integer 
        Dim lstrRow As String
        Dim lblnContinue As Boolean
        
		Dim lstrArray_txt() As String
                
        
        Dim lintColMax As Integer = 16

		Const ARRBLOCK As Short = 500
        Dim lintRow As Integer = 0
        Dim lintColumn As Integer = 0
        Dim sbFileinText As new StringBuilder()
        
        If Len(Dir(lstrFiledeltxt, FileAttribute.Archive)) > 0 And lstrFiledeltxt <> String.Empty Then
            sMessage &= "16_"

            '+Se abre archivo de texto a procesar
            On Error Resume Next
            lintFileNum = FreeFile()
            FileOpen(lintFileNum, lstrFile, OpenMode.Input)
            
            If Err.Number Then
                FileClose(lintFileNum)
                FileOpen(lintFileNum, lstrFile, OpenMode.Input)
            End If
                

            '+Se lee la columna de titulos. No se cargan
            lstrRow = LineInput(lintFileNum)
            lblnContinue = True

            '+Se redefine matriz al bloque máximo
            'UPGRADE_ISSUE: As Variant was removed from ReDim lstrArray(lintColMax, ARRBLOCK) statement. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="19AFCB41-AA8E-4E6B-A441-A3E802E5FD64"'
            ReDim lstrArray(lintColMax, ARRBLOCK)

            lintRow = 0
            lintColumn = 0
                
            '+Se carga archivo texto a matriz
            Do While Not EOF(lintFileNum) And lblnContinue
                    lstrRow = Replace(LineInput(lintFileNum), """", "")
                '+Por si viene linea vacía
                If Len(lstrRow) = 0 Or Len(lstrRow) = lintColMax Then
                    lblnContinue = False
                Else
                    lstrArray_txt = Microsoft.VisualBasic.Split(lstrRow, vbTab)
                    If lstrArray_txt(0) = "5" Then 'Solo si nType = 5 Equipo y maquinaria contratistas
                        sbFileinText.Append(String.Join(";" , lstrArray_txt) & "|" )
                        lintColumn = 0
                        On Error Resume Next
                        For lintColumn = 0 To lintColMax
                            lstrArray(lintColumn, lintRow) = lstrArray_txt(lintColumn)
                        Next
                        'On Error GoTo insQueryinportExcel_Err
                    End If
                    lintRow = lintRow + 1
                End If
                '+Si las filas llegaron al máximo disponible se agrega un bloque nuevo
                If (lintRow Mod ARRBLOCK) = 0 Then
                    'UPGRADE_ISSUE: As Variant was removed from ReDim lstrArray(lintColMax, lintRow + ARRBLOCK) statement. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="19AFCB41-AA8E-4E6B-A441-A3E802E5FD64"'
                    ReDim Preserve lstrArray(lintColMax, lintRow + ARRBLOCK)
                End If
            Loop

            FileClose(lintFileNum)     
        End if

    On Error Goto ja
        If sbFileinText.Length > 0 Then
        lstrErrors = (New ePolicy.ValPolicySeq_MU700).ValMassiveCharge_MultiRisk_Det(sbFileinText.ToString(), 5)
        If lstrErrors Is Nothing Then
        Dim lrecMassiveCharge_MultiRisk_Det = New eRemoteDB.Execute(True)

            'Definición de parámetros para stored procedure 'insudb.CreColSheet'
            'Información leída el 05/02/2001 10:58:38 a.m.

            With lrecMassiveCharge_MultiRisk_Det
                .StoredProcedure = "insMassiveCharge_MultiRisk_Det"
			    .Parameters.Add("sCertype", Session("sCertype"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			    .Parameters.Add("nBranch", Session("nBranch"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			    .Parameters.Add("nProduct", Session("nProduct"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			    .Parameters.Add("nPolicy", Session("nPolicy"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nCertif", Session("nCertif"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("dEffecdate", Session("dEffecdate"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nUserCode", Session("nUserCode"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 5, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("sFileinText", sbFileinText.ToString(), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 4000, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Run(False)
            End With
            Return True
        Else
            With Response
                Session("sErrorTable") = lstrErrors
                Session("sForm") = Request.Form.ToString

                Response.Write("<script type='text/javascript' language='JavaScript' src='/VTimeNet/Scripts/GenFunctions.js'></script>")
                Response.Write("<script type='text/javascript'> ")
                Response.Write(" ShowPopUp(""/VTimeNet/Common/Errors.aspx?sSource=" & Server.UrlEncode(lstrCommand) & "&sQueryString=" & Server.UrlEncode(Request.Params.Get("Query_String")) & lstrQueryString & """, ""PolicySeqError"",660,330); ")
                Response.Write(" </script>")
            End With
            Return False
        End If
        Else
            Response.Write("<script> alert('No hay datos para procesar') </script>")
        End If
        ja:

        If Err.Number > 0 then
            Return False
            Throw New Exception(Err.Description)

        End If 


    End Function
        
	Public Function insTransformationExcel(ByVal sFile As String) As Boolean
        Dim sMessage As string
		Dim lclsvalue As eFunctions.Values
        CheckExcellProcesses()
		Dim mvarSalidaExcel As Microsoft.Office.Interop.Excel.Application
		Dim lstrFileName As String
		Dim lintExist As Integer
		Dim lstrFile As String
		Dim lintlength As Integer
		
		On Error GoTo insTransformationExcel_Err
		
		lintExist = InStr(1, UCase(sFile), ".XLS")
		If lintExist > 0 Then
			lstrFile = Mid(sFile, 1, lintExist - 1)
		Else
			lstrFile = sFile
		End If
		
		lclsvalue = New eFunctions.Values
		On Error Resume Next
		
		lstrFileName = UCase(lclsvalue.insGetSetting("MASSIVELOAD", String.Empty, "PATHS"))
		
		If lstrFileName = String.Empty Then
			lstrFileName = UCase(lclsvalue.insGetSetting("MASSIVELOAD", String.Empty, "Config"))
		End If
		
		'UPGRADE_NOTE: Object lclsvalue may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsvalue = Nothing
		
		On Error GoTo insTransformationExcel_Err
		
		lintlength = Len(lstrFileName)
		
		If Mid(lstrFileName, lintlength, 1) <> "\" Then
			lstrFileName = lstrFileName & "\"
		End If
		
		mvarSalidaExcel = New Microsoft.Office.Interop.Excel.Application
        
		mvarSalidaExcel.DisplayAlerts = False
        sMessage &= "7_"
        
        If System.IO.File.Exists(lstrFileName & Trim(lstrFile) & ".XLS") Then
            
            mvarSalidaExcel.Workbooks.Open(lstrFileName & Trim(lstrFile) & ".XLS", 0, True )
            mvarSalidaExcel.Cells.Select()
            mvarSalidaExcel.Selection.Replace(What:="|", Replacement:="", LookAt:=2, SearchOrder:=1, MatchCase:=False, SearchFormat:=False, _
            ReplaceFormat:=False)
        End if
        sMessage &= "8_"

		'+Se guarda el archivo como texto separador por tabuladores
		mvarSalidaExcel.ActiveWorkbook.SaveAs(lstrFileName & Trim(lstrFile) & ".TXT", Microsoft.Office.Interop.Excel.XlPivotFieldDataType.xlText, False)
        sMessage &= "9_"

		insTransformationExcel = True
		
insTransformationExcel_Err: 
		If Err.Number Then
			insTransformationExcel = False
            sMessage = sMessage & "[insTransformationExcel]" & Err.Description & vbCrLf
            Throw New Exception(sMessage)
        End If

        'On Error Resume Next
        If Not mvarSalidaExcel Is Nothing Then
            If Not mvarSalidaExcel.ActiveWorkbook Is Nothing Then
                mvarSalidaExcel.ActiveWorkbook.Close()
                mvarSalidaExcel.Quit()
                KillExcel()
            End If
        End If

        sMessage &= "10_"

        'UPGRADE_NOTE: Object mvarSalidaExcel may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        mvarSalidaExcel = Nothing
        On Error GoTo 0
    End Function
    
Dim myHashtable As Hashtable    
Private Sub CheckExcellProcesses()
	Dim AllProcesses As System.Diagnostics.Process() = System.Diagnostics.Process.GetProcessesByName("excel")
	myHashtable = New Hashtable()
	Dim iCount As Integer = 0

	For Each ExcelProcess As System.Diagnostics.Process In AllProcesses
		myHashtable.Add(ExcelProcess.Id, iCount)
		iCount = iCount + 1
	Next
End Sub
    
    
Private Sub KillExcel()
	Dim AllProcesses As System.Diagnostics.Process() = System.Diagnostics.Process.GetProcessesByName("excel")

	' check to kill the right process
	For Each ExcelProcess As System.Diagnostics.Process In AllProcesses
		If myHashtable.ContainsKey(ExcelProcess.Id) = False Then
			ExcelProcess.Kill()
		End If
	Next

	AllProcesses = Nothing
End Sub

End Class