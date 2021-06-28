Option Strict Off
Option Explicit On
<System.Runtime.InteropServices.ProgId("tmp_BCL804_NET.tmp_BCL804")> Public Class tmp_BCL804
	Public lstrKey As String
	
	Public Function insValBCL804(ByVal sFileName As String) As String
		
		Dim lobjErrors As Object
		Dim lobjPolicy As Object
		
		On Error GoTo insValBCL804_Err
		
		lobjErrors = eRemoteDB.NetHelper.CreateClassInstance("eFunctions.Errors")
		
		With lobjErrors
			
			If sFileName = String.Empty Then
				.ErrorMessage("COL1003", 98007)
			End If
			
			insValBCL804 = .Confirm
			
		End With
		
insValBCL804_Err: 
		If Err.Number Then
			insValBCL804 = "insValBCL804: " & insValBCL804 & Err.Description
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lobjErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lobjErrors = Nothing
		
		
		
	End Function
	
	'%insQueryimportExcel(). Esta funcion se encarga de cargar el archivo en la base de datos
	Public Function insImportExcel(ByVal sFile As String, ByVal sKey As String, ByVal nUsercode As Integer, ByVal sCodispl As String) As Boolean
        Dim sMessage As Object = New Object
        Dim x As Object
		Dim nCrut As Object
		
		'-Tamaño en que se aumenta bloque de carga
		Const ARRBLOCK As Short = 500
		
		Dim lclsValue As eFunctions.Values
		Dim lclsBatch As Object
		
		Dim lintRow As Integer
		Dim lintColumn As Integer
		Dim lintRow_end As Integer
		Dim lintColumn_end As Integer
		Dim lstrMassiveDir As String
		
		Dim lblnContinue As Boolean
		Dim lintExist As Integer
		Dim lintlength As Integer
		Dim lintFileNum As Integer
		'-Nombre de archivo con directorio
		Dim lstrFile As String
		'-Nombre de archivo con extension
		Dim lstrFileName As String
		'- Nombre archivo sin extensión
		Dim lstrFileNameWOExt As String
		Dim lstrFiledelxls As String
		Dim lstrFiledeltxt As String
		
		Dim lstrRow As String
		Dim lstrquery As String
		Dim lintPos As Integer
		'-Cantidad de columnas y valor de ultima columna
		Dim lintColCount As Integer
		Dim lintColMax As Integer
		'-Valores obtenidos de plantilla
		Dim lstrArray_txt() As String
		Dim lstrArray() As Object
		Dim lstrvalue As String
		Dim lvntValue As Object
		Dim lintCount As Integer
		
		'-Tipo de datos de la columna
		Dim lstrType As String
		
		'-Valores obtenidos de colsheet
		Dim lstrField As String
		
		Dim lRecord As Object
		
		Dim sClient As String
		Dim sRut As String
		Dim sFirstName As String
		Dim sLastName As String
		Dim sLastname2 As String
		Dim sSexclien As String
		Dim nBankext As Integer
		Dim nTyp_acc As Integer
		Dim sAccount As String
		Dim sStreet As String
		Dim se_mail As String
		Dim sPhone As String
		Dim sErrorColumn As String
		Dim sCostCenter As String
		
		On Error GoTo insImportExcel_Err
		
		insImportExcel = False
		
		sErrorColumn = String.Empty
		
		lRecord = eRemoteDB.NetHelper.CreateClassInstance("eRemoteDb.Execute")
		lclsBatch = eRemoteDB.NetHelper.CreateClassInstance("eBatch.Colsheet")
		
		lintExist = 1
		Do While lintExist <> 0
			lintExist = InStr(1, UCase(sFile), "\")
			sFile = Mid(sFile, lintExist + 1)
			If InStr(1, UCase(sFile), "\") = 0 Then
				lintExist = 0
			End If
		Loop 
		
		lintExist = InStr(1, UCase(sFile), ".XLS")
		If lintExist > 0 Then
			lstrFileNameWOExt = Mid(sFile, 1, lintExist - 1)
		Else
			lstrFileNameWOExt = sFile
		End If
		
		lclsValue = New eFunctions.Values
		
		On Error Resume Next
		lstrMassiveDir = UCase(lclsValue.insGetSetting("MASSIVELOAD", String.Empty, "PATHS"))
		If lstrMassiveDir = String.Empty Then
			lstrMassiveDir = UCase(lclsValue.insGetSetting("MASSIVELOAD", String.Empty, "Config"))
		End If
		
		On Error GoTo insImportExcel_Err
		
		'UPGRADE_NOTE: Object lclsValue may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsValue = Nothing
		
		'+Si el directorio no incorpora linea se le agrega
		lintlength = Len(lstrMassiveDir)
		If Mid(lstrMassiveDir, lintlength, 1) <> "\" Then
			lstrMassiveDir = lstrMassiveDir & "\"
		End If
		
		lstrFileName = lstrFileNameWOExt & ".XLS"
		lstrFile = lstrMassiveDir & lstrFileName
		
		lstrFiledelxls = lstrFile
		
		'+Duplica el archivo con formato texto separado por tabuladores
		'+El archivo tiene el mismo nombre con la extensión TXT
		Call lclsBatch.insTransformationExcel(lstrFileName)
		
		lstrFile = lstrMassiveDir & lstrFileNameWOExt & ".TXT"
		lstrFiledeltxt = lstrFile
		
		lintColMax = lintColCount - 1
		
		'+Si se creo el archivo de texto
		
		'UPGRADE_WARNING: Dir has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
		If Len(Dir(lstrFiledeltxt, FileAttribute.Archive)) > 0 And lstrFiledeltxt <> String.Empty Then
			
			'+Se abre archivo de texto a procesar
			On Error Resume Next
			lintFileNum = FreeFile
			FileOpen(lintFileNum, lstrFile, OpenMode.Input)
			If Err.Number Then
				FileClose(lintFileNum)
				FileOpen(lintFileNum, lstrFile, OpenMode.Input)
			End If
			On Error GoTo insImportExcel_Err
			
			'+Se lee la columna de titulos. No se cargan
			lstrRow = LineInput(lintFileNum)
			lblnContinue = True
			
			lintRow = 0
			lintColumn = 0
			
			'+Se carga archivo texto a matriz
			Do While Not EOF(lintFileNum) And lblnContinue
				sClient = String.Empty
				sRut = String.Empty
				sFirstName = String.Empty
				sLastName = String.Empty
				sLastname2 = String.Empty
				sSexclien = String.Empty
				nBankext = eRemoteDB.Constants.intNull
				nTyp_acc = eRemoteDB.Constants.intNull
				sAccount = String.Empty
				sStreet = String.Empty
				se_mail = String.Empty
				sPhone = String.Empty
				sCostCenter = String.Empty
				
				lstrRow = LineInput(lintFileNum)
				
				If Len(lstrRow) = 11 Then
					Exit Do
				End If
				
				'+Por si viene linea vacía
				lstrArray_txt = Microsoft.VisualBasic.Split(lstrRow, vbTab)
				lintColumn = 0
				On Error Resume Next
				
				
				If lstrArray_txt(0) <> String.Empty Then
					sRut = Trim(lstrArray_txt(0))
					nCrut = (14 - Len(Trim(sRut)))
					
					For x = 1 To nCrut
						sClient = Trim(sClient & "0")
					Next 
					
					sClient = Trim(CStr(sClient & sRut))
					
				End If
				
				If lstrArray_txt(1) <> String.Empty Then
					sFirstName = Mid(Trim(CStr(lstrArray_txt(1))), 1, 20)
				End If
				
				If lstrArray_txt(2) <> String.Empty Then
					sLastName = Mid(Trim(CStr(lstrArray_txt(2))), 1, 20)
				End If
				
				If lstrArray_txt(3) <> String.Empty Then
					sLastname2 = Mid(Trim(CStr(lstrArray_txt(3))), 1, 20)
				End If
				
				If lstrArray_txt(4) <> String.Empty Then
					sSexclien = Mid(Trim(CStr(lstrArray_txt(4))), 1, 1)
				End If
				
				If lstrArray_txt(5) <> String.Empty Then
					nBankext = CInt(lstrArray_txt(5))
				End If
				
				If lstrArray_txt(6) <> String.Empty Then
					nTyp_acc = CInt(lstrArray_txt(6))
				End If
				
				If lstrArray_txt(7) <> String.Empty Then
					sAccount = Mid(Trim(CStr(lstrArray_txt(7))), 1, 25)
				End If
				
				If lstrArray_txt(8) <> String.Empty Then
					sStreet = Mid(Trim(CStr(lstrArray_txt(8))), 1, 60)
				End If
				
				If lstrArray_txt(9) <> String.Empty Then
					se_mail = Mid(Trim(CStr(lstrArray_txt(9))), 1, 60)
				End If
				
				If lstrArray_txt(10) <> String.Empty Then
					sPhone = Mid(Trim(CStr(lstrArray_txt(10))), 1, 11)
				End If
				
				If lstrArray_txt(11) <> String.Empty Then
					sCostCenter = Mid(Trim(CStr(lstrArray_txt(11))), 1, 30)
				End If
				
				
				'+    sErrorColumn = IIf(lstrArray_txt(5) > String.Empty, CStr(lstrArray_txt(5)), String.Empty)
				
				With lRecord
					.StoredProcedure = "CRETMP_CLIENT"
					.Parameters.Add("sKey", sKey, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Parameters.Add("sFirstname", sFirstName, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Parameters.Add("sLastname", sLastName, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Parameters.Add("sLastname2", sLastname2, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Parameters.Add("sSexclien", sSexclien, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Parameters.Add("nBankext", nBankext, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Parameters.Add("nTyp_Acc", nTyp_acc, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Parameters.Add("sAccount", sAccount, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 25, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Parameters.Add("sStreet", sStreet, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 60, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Parameters.Add("sE_mail", se_mail, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 60, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Parameters.Add("sPhone", sPhone, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 11, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Parameters.Add("sCostCenter", sCostCenter, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 30, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					
					insImportExcel = .Run(False)
				End With
				
				On Error GoTo insImportExcel_Err
				lintRow = lintRow + 1
				
				'+Si las filas llegaron al máximo disponible se agrega un bloque nuevo
			Loop 
			
			FileClose(lintFileNum)
		End If
		
		'UPGRADE_NOTE: Object lRecord may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lRecord = Nothing
		'UPGRADE_NOTE: Object lclsBatch may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsBatch = Nothing
		
insImportExcel_Err: 
		If Err.Number Then
			insImportExcel = False
			sMessage = sMessage & "[insQueryinportExcel]" & Err.Description
		End If
		FileClose(lintFileNum)
		
		'UPGRADE_NOTE: Object lclsValue may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsValue = Nothing
		
		'UPGRADE_WARNING: Dir has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
		If Len(Dir(lstrFiledelxls, FileAttribute.Archive)) > 0 And lstrFiledelxls <> String.Empty Then
			On Error Resume Next
			Kill(lstrFiledelxls)
			On Error GoTo 0
		End If
		'UPGRADE_WARNING: Dir has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
		If Len(Dir(lstrFiledeltxt, FileAttribute.Archive)) > 0 And lstrFiledeltxt <> String.Empty Then
			On Error Resume Next
			Kill(lstrFiledeltxt)
			On Error GoTo 0
		End If
		On Error GoTo 0
	End Function
	'% Find: Devuelve el nombre de un cliente dado un código de cliente
	Public Function Find(ByVal sKey As String) As Boolean
		
		'- Se declara la variable que determina el resultado de la funcion (True/False)
		Static lblnRead As Boolean
		
		'- Se define la variable lrereaClient
		Dim lrereaClient As eRemoteDB.Execute
		lrereaClient = New eRemoteDB.Execute
		
		On Error GoTo Find_Err
		
		With lrereaClient
			.StoredProcedure = "REATMP_CLIENTLOG"
			.Parameters.Add("sKey", sKey, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run Then
				lblnRead = True
				.RCloseRec()
			Else
				lblnRead = False
			End If
		End With
		
		Find = lblnRead
		'UPGRADE_NOTE: Object lrereaClient may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrereaClient = Nothing
		
Find_Err: 
		If Err.Number Then
			Find = False
		End If
		On Error GoTo 0
	End Function
End Class






