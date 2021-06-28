Option Strict Off
Option Explicit On
Public Class MasiveCharge
	'%-------------------------------------------------------%'
	'% $Workfile:: MasiveCharge.cls                         $%'
	'% $Author:: Nvaplat41                                  $%'
	'% $Date:: 10/10/03 17.34                               $%'
	'% $Revision:: 28                                       $%'
	'%-------------------------------------------------------%'
	
	'Column_name                      Type        Length      Prec  Scale Nullable
	'-------------------------------- ----------- ----------- ----- ----- ---------
	Public sKey As String 'char       20                       yes
	Public nRows As Integer 'int        4           10    0     no
	Public nColumns As Integer 'int        4           10    0     no
	Public sField As String 'char       20                      no
	Public sFieldName As String 'char       20                      no
	Public sValue As String 'char       300                     no
	Public nSearch As Integer 'smallint   2           5     0     yes
	Public sTable As String 'char       20                      yes
	Public sValuesList As String 'char       30                      yes
	Public nInconsist As Integer 'int        4           10    0     no
	Public nUsercode As Integer 'int        4           10    0     no
	
	'-Indica que carga de datos se hace manualmente (no por carga de archivo)
	Public bManualProc As Boolean
	
	Private Const clngActionadd As String = "301" '+  Registrar
	Private Const clngActionUpdate As String = "302" '+  Actualizar
	Private Const clngActioncut As String = "303" '+  Eliminar
	
	Private mobjGrid As eFunctions.Grid
	Private Structure udtArray
		Dim sField As String
	End Structure
	Public mlngIndex As Integer
	Private marray() As udtArray
	
	
	
	
	
	'%Add(). Esta funcion se encarga de incluir un campo en la Tabla MasiveCharge
	Public Function Add() As Boolean
		
		Dim lrecCreMasiveCharge As eRemoteDB.Execute
		
		On Error GoTo Add_Err
		
		lrecCreMasiveCharge = New eRemoteDB.Execute
		
		'Definición de parámetros para stored procedure 'insudb.CreMasiveCharge'
		
		With lrecCreMasiveCharge
			.StoredProcedure = "CreT_MasiveCharge"
			.Parameters.Add("sKey", sKey, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nColumns", nColumns, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sField", sField, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sValue", sValue, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 30000, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nSearch", nSearch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sTable", sTable, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sValuesList", sValuesList, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 30, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Add = .Run(False)
		End With
		
		'UPGRADE_NOTE: Object lrecCreMasiveCharge may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecCreMasiveCharge = Nothing
		
Add_Err: 
		If Err.Number Then
			Add = False
		End If
		
		On Error GoTo 0
		
	End Function
	
	'%Delete(). Esta funcion se encarga de eliminar los registros de la carga masiva asociado a una LLave
	Public Function Delete(ByVal sKey As String) As Boolean
		
		Dim lrecDelMasiveCharge As eRemoteDB.Execute
		
		On Error GoTo Delete_Err
		
		lrecDelMasiveCharge = New eRemoteDB.Execute
		
		'Definición de parámetros para stored procedure 'insudb.DelMasiveCharge'
		
		With lrecDelMasiveCharge
			.StoredProcedure = "DelT_MasiveCharge"
			.Parameters.Add("sKey", sKey, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Delete = .Run(False)
		End With
		
		'UPGRADE_NOTE: Object lrecDelMasiveCharge may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecDelMasiveCharge = Nothing
		
Delete_Err: 
		If Err.Number Then
			Delete = False
		End If
		
		On Error GoTo 0
		
	End Function
	
	'%InsUpdChargeCharge(). Esta funcion se encarga de Actualizar la Tabla Temporal de Carga Masiva
	Public Function InsUpdMasiveCharge(ByVal sKey As String, ByVal nRows As Integer, ByVal nColumns As Integer, ByVal sField As String, ByVal sValue As Object, ByVal nSearch As Integer, ByVal sTable As String, ByVal sValuesList As String, ByVal nMainAction As Integer, ByVal nInconsist As Integer) As Boolean
		
		With Me
			.sKey = sKey
			.nRows = nRows
			.nColumns = nColumns
			.sField = sField
			'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
			If IsDbNull(sValue) Then
				.sValue = ""
			Else
				.sValue = sValue
			End If
			.nSearch = nSearch
			.sTable = sTable
			.sValuesList = sValuesList
			If nInconsist <> eRemoteDB.Constants.intNull Then
				.nInconsist = nInconsist
			End If
			
			Select Case nMainAction
				Case CDec(clngActionadd)
					InsUpdMasiveCharge = .Add
			End Select
			
		End With
		
	End Function
	
	'%Update(). Esta funcion Actualiza el Temporal para eliminar las inscositencias
	Public Function Update(ByVal sKey As String, ByVal sField As String, ByVal sValue As String, ByVal sTable As String, ByVal sValue1 As String) As Boolean
		
		Dim lrecUpdMasiveCharge As eRemoteDB.Execute
		
		On Error GoTo Update_Err
		
		lrecUpdMasiveCharge = New eRemoteDB.Execute
		
		'Definición de parámetros para stored procedure 'insudb.CreMasiveCharge'
		
		With lrecUpdMasiveCharge
			.StoredProcedure = "Updt_MasiveCharge"
			.Parameters.Add("sKey", sKey, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sField", sField, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sValue", sValue, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 300, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sTable", sTable, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sValue1", sValue1, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 300, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Update = .Run(False)
		End With
		
		'UPGRADE_NOTE: Object lrecUpdMasiveCharge may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecUpdMasiveCharge = Nothing
		
Update_Err: 
		If Err.Number Then
			Update = False
		End If
		
		On Error GoTo 0
		
	End Function

    '% MakeCa0659 : Construye el Grid que muestra la Tabla Temporal de la transaccion CA0659
    Public Function MakeCa0659(ByVal sKey As String, ByVal nId As Integer, ByVal nRow As Integer) As String
        Dim lobjValues As eFunctions.Values
        Dim lcolMasiveCharge As MasiveCharges
        Dim lclsMasiveCharge As MasiveCharge
        Dim llngIndex As Object
        Dim strResultado As String = ""

        Try
            mobjGrid = New eFunctions.Grid
            lcolMasiveCharge = New MasiveCharges
            lclsMasiveCharge = New MasiveCharge
            lobjValues = New eFunctions.Values

            Call insDefineHeader(nId)

            nRow = IIf(nRow = eRemoteDB.Constants.intNull, 1, nRow)
            If lcolMasiveCharge.Find(sKey, nRow) Then
                llngIndex = 1
                For Each lclsMasiveCharge In lcolMasiveCharge
                    With mobjGrid
                        If llngIndex = mlngIndex Then
                            .Columns("hddnRow").DefValue = CStr(lclsMasiveCharge.nRows)
                        End If

                        If llngIndex = mlngIndex + 1 Then

                            strResultado = strResultado & .DoRow
                            llngIndex = 1
                        End If
                        .Columns(marray(llngIndex).sField).DefValue = lclsMasiveCharge.sValue
                        llngIndex = llngIndex + 1
                    End With
                Next lclsMasiveCharge
                strResultado = strResultado & mobjGrid.DoRow
            End If
            strResultado = strResultado & mobjGrid.closeTable
            Return strResultado
        Catch ex As Exception
            Return strResultado

        Finally
            'UPGRADE_NOTE: Object mobjGrid may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
            mobjGrid = Nothing
            'UPGRADE_NOTE: Object lcolMasiveCharge may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
            lcolMasiveCharge = Nothing
            'UPGRADE_NOTE: Object lclsMasiveCharge may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
            lclsMasiveCharge = Nothing
            'UPGRADE_NOTE: Object lobjValues may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
            lobjValues = Nothing
        End Try
    End Function

    '% insDefineHeader: se definen las propiedades del grid
    Private Sub insDefineHeader(ByVal nId As Integer)
		'-Coleción que trae los Encabezado de la grilla
		Dim lcolColSheet As Colsheets
		Dim lclsColsheet As Colsheet
		Dim llngIndex As Integer
		'-Marcador de columna criterio de búsqueda
		Dim lstrIndCriter As String
		Dim lstrFieldName As String
		
		lcolColSheet = New Colsheets
		
		Call lcolColSheet.Find(nId)
		
		mlngIndex = lcolColSheet.Count
		
		ReDim Preserve marray(mlngIndex)
		
		mobjGrid = New eFunctions.Grid
		'+ Se definen las columnas del grid
		
		With mobjGrid.Columns
			llngIndex = 1
			For	Each lclsColsheet In lcolColSheet
				If lclsColsheet.sSelected = "1" Then
					lstrIndCriter = "*"
				Else
					lstrIndCriter = ""
				End If
                'lstrFieldName = Format(llngIndex, "A00") & lclsColsheet.sField
                lstrFieldName = "C" & llngIndex.ToString.PadLeft(2, "0") & lclsColsheet.sField

				If Not bManualProc Then
					Call .AddTextColumn(0, lstrIndCriter & " " & lclsColsheet.sColumnName, lstrFieldName, 30, "",  ,  ,  ,  ,  , True)
				Else
					If lclsColsheet.sValuesList <> "" Then
						Call .AddPossiblesColumn(0, lstrIndCriter & " " & lclsColsheet.sColumnName, lstrFieldName, lclsColsheet.sValuesList, eFunctions.Values.eValuesType.clngComboType,  , False,  ,  ,  ,  ,  ,  ,  ,  , llngIndex)
					Else
						Select Case lclsColsheet.sData_Type
							Case lclsColsheet.CN_DATE
								Call .AddDateColumn(0, lstrIndCriter & " " & lclsColsheet.sColumnName, lstrFieldName,  ,  ,  ,  ,  ,  ,  , llngIndex)
							Case lclsColsheet.CN_NUMBER
								Call .AddNumericColumn(0, lstrIndCriter & " " & lclsColsheet.sColumnName, lstrFieldName, lclsColsheet.nData_Precision, CStr(0), False,  , False, lclsColsheet.nData_Scale,  ,  ,  ,  , llngIndex)
							Case lclsColsheet.CN_CHAR, lclsColsheet.CN_VARCHAR2
								Call .AddTextColumn(0, lstrIndCriter & " " & lclsColsheet.sColumnName, lstrFieldName, lclsColsheet.nData_Length, "",  ,  ,  ,  ,  , True, llngIndex)
						End Select
					End If
				End If
				marray(llngIndex).sField = lstrFieldName
				llngIndex = llngIndex + 1
			Next lclsColsheet
			Call .AddHiddenColumn("hddnRow", "")
		End With
		
		
		'+ Se definen las propiedades generales del grid
		With mobjGrid
			.Codispl = "CAL659"
			'+Se indica que es un grid actualizable
			.bUpdateGrid = bManualProc
			'+Se asigna al
            .nParentForm = 1
			.DeleteButton = bManualProc
			.AddButton = False
			.Top = 50
			.Height = 430
			.Width = 400
            .Columns("Sel").GridVisible = bManualProc
            If bManualProc Then
                .sDelRecordParam = "DelRow=" & "' + marrArray[lintIndex].hddnRow + '"
            End If
        End With
		
		'UPGRADE_NOTE: Object lcolColSheet may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lcolColSheet = Nothing
		
	End Sub
	
	'%insPostCal660(). Esta funcion Actualiza la tabla temporal t_masivecharge
	Public Function insPostCal660(ByVal sKey As String, ByVal sField As String, ByVal sValue As String, ByVal sTable As String, ByVal sValue1 As String) As Boolean
		
		On Error GoTo insPostCal660_Err
		
		insPostCal660 = Update(sKey, sField, sValue, sTable, sValue1)
		
		
insPostCal660_Err: 
		If Err.Number Then
			insPostCal660 = False
		End If
		
		On Error GoTo 0
		
	End Function

    '%insPostCal013(). Esta funcion invoca al proceso de carga masiva
    Public Function insPostCal013(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal dEffecdate As Date, ByVal nAction As Integer, ByVal sKey As String, ByVal nId As Integer, ByVal nUsercode As Integer, ByVal sTypeage As String, ByVal sContinue As String, ByVal dContinue As Date, ByVal dNulldate As Date, ByVal sOptAct As String) As Boolean
        Dim lrecMasiveCharge As eRemoteDB.Execute
        Dim ldtmEffecdate As Date


        ldtmEffecdate = IIf(dEffecdate = eRemoteDB.Constants.dtmNull, Today, dEffecdate)
        Try

            lrecMasiveCharge = New eRemoteDB.Execute
            'Definición de parámetros para stored procedure 'insudb.CreMasiveCharge'

            With lrecMasiveCharge
                .StoredProcedure = "INSMASSIVE_CHARGPKG.INSMASSIVE_CHARG"
                .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("dEffecdate", ldtmEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nAction", nAction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("sKey", sKey, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nId", nId, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("sTypeage", sTypeage, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("sContinue", sContinue, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("dContinue", dContinue, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("dNulldate", dNulldate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("sProcess", sOptAct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                insPostCal013 = .Run(False)
            End With
        Catch ex As Exception
            insPostCal013 = False
        Finally
            'UPGRADE_NOTE: Object lrecMasiveCharge may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
            lrecMasiveCharge = Nothing
        End Try
    End Function
    '%insPostDelCal013(). Esta funcion Borra la tabla TMP_CAL013_LIST
    Public Function insPostDelCal013(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal sKey As String) As Boolean
		
		Dim lrecMasiveCharge As eRemoteDB.Execute
		
		On Error GoTo insPostDelCal013_Err
		
		lrecMasiveCharge = New eRemoteDB.Execute
		
		With lrecMasiveCharge
			.StoredProcedure = "DELTMP_CAL013_LIST"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sKey", sKey, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			insPostDelCal013 = .Run(False)
		End With
		
insPostDelCal013_Err: 
		If Err.Number Then
			insPostDelCal013 = False
		End If
		'UPGRADE_NOTE: Object lrecMasiveCharge may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecMasiveCharge = Nothing
		On Error GoTo 0
	End Function

    '%GetRegister: Obtiene la ruta del servidor donde se van a insertar los archivos
    Public Function GetLoadFile(Optional ByVal nOrigin As Boolean = False) As String
        Dim lclsValue As eFunctions.Values
        Dim lstrName As String
        Dim lintlength As Integer
        Dim lstrFileName As String
        Dim strResult As String = ""

        Try

            lclsValue = New eFunctions.Values

            lstrFileName = Trim(UCase(lclsValue.insGetSetting("MASSIVELOAD", String.Empty, "PATHS")))
            If lstrFileName = String.Empty Then
                lstrFileName = Trim(UCase(lclsValue.insGetSetting("MASSIVELOAD", String.Empty, "Config")))
            End If

            lintlength = Len(lstrFileName)
            If Mid(lstrFileName, lintlength, 1) <> "\" Then
                lstrFileName = lstrFileName & "\"
            End If
            If nOrigin Then
                Do While lstrFileName <> String.Empty
                    lstrName = Mid(lstrFileName, 1, 1)
                    strResult = strResult & IIf(lstrName = "\", "\\", lstrName)
                    lstrFileName = Mid(lstrFileName, 2)
                Loop
            Else
                strResult = lstrFileName
            End If

            Return strResult
        Catch ex As Exception
            Return strResult = CStr(False)
        Finally
            'UPGRADE_NOTE: Object lclsValue may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
            lclsValue = Nothing
        End Try
    End Function
    '% Find: se buscan Archivos en el directorio definido en utl_file_dir
    Public Function Find_Files(ByVal sMassive As String) As String
		Dim lrecReaMasiveCharge As eRemoteDB.Execute
		On Error GoTo Find_Files_Err
		lrecReaMasiveCharge = New eRemoteDB.Execute
		
		With lrecReaMasiveCharge
			.StoredProcedure = "INSREADIR_LIST"
			.Parameters.Add("sMassive", sMassive, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sList_File", "", eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 3000, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				Find_Files = .Parameters("sList_File").Value
			Else
				Find_Files = ""
			End If
			
		End With
		
Find_Files_Err: 
		If Err.Number Then
			Find_Files = ""
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecReaMasiveCharge may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecReaMasiveCharge = Nothing
	End Function
	
	'%InsCalTmp_Cal013_List(). Esta funcion invoca al proceso de cálculo temporal de nómina por endoso retroactivo
	Public Function InsCalTmp_Cal013_List(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal dEffecdate As Date, ByVal nAction As Integer, ByVal sKey As String, ByVal nId As Integer, ByVal nUsercode As Integer, ByVal sFile As String, ByVal sFileName As String) As Boolean
		
		Dim lrecMasiveCharge As eRemoteDB.Execute
		
		Dim ldtmEffecdate As Date
		
		ldtmEffecdate = IIf(dEffecdate = eRemoteDB.Constants.dtmNull, Today, dEffecdate)
		
		On Error GoTo InsCalTmp_Cal013_List_Err
		
		lrecMasiveCharge = New eRemoteDB.Execute
		
		'Definición de parámetros para stored procedure 'insudb.CreMasiveCharge'
		
		With lrecMasiveCharge
			.StoredProcedure = "INSCALTMP_CAL013_LIST"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", ldtmEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sKey", sKey, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nId", nId, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sFile", sFile, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sFileName", sFileName, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 100, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			InsCalTmp_Cal013_List = .Run(False)
		End With
		
InsCalTmp_Cal013_List_Err: 
		If Err.Number Then
			InsCalTmp_Cal013_List = False
		End If
		'UPGRADE_NOTE: Object lrecMasiveCharge may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecMasiveCharge = Nothing
		On Error GoTo 0
	End Function
	
	'%InsPrintTmp_Cal013_List(). Esta funcion imprime una nómina de asegurados
	Public Function InsPrintTmp_Cal013_List(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal dEffecdate As Date, ByVal sKey As String, ByVal nUsercode As Integer) As Boolean
		
		Dim lrecMasiveCharge As eRemoteDB.Execute
		
		Dim ldtmEffecdate As Date
		
		ldtmEffecdate = IIf(dEffecdate = eRemoteDB.Constants.dtmNull, Today, dEffecdate)
		
		On Error GoTo InsPrintTmp_Cal013_List_Err
		
		lrecMasiveCharge = New eRemoteDB.Execute
		
		'Definición de parámetros para stored procedure 'insudb.CreMasiveCharge'
		
		With lrecMasiveCharge
			.StoredProcedure = "INSPRINTTMP_CAL013_LIST"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", ldtmEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sKey", sKey, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			InsPrintTmp_Cal013_List = .Run(False)
		End With
		
InsPrintTmp_Cal013_List_Err: 
		If Err.Number Then
			InsPrintTmp_Cal013_List = False
		End If
		'UPGRADE_NOTE: Object lrecMasiveCharge may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecMasiveCharge = Nothing
		On Error GoTo 0
	End Function
End Class






