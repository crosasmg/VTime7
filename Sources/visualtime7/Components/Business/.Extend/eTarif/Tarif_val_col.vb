Option Strict Off
Option Explicit On
Public Class Tarif_val_col
	'%-------------------------------------------------------%'
	'% $Workfile:: Tarif_val_col.cls                            $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 1:34p                                $%'
	'% $Revision:: 9                                        $%'
	'%-------------------------------------------------------%'
	
	Public nId_table As Integer
	Public nId_column As Integer
	Public dEffecdate As Date
	Public nRow As Integer
	Public sValue As String
	Public nValue As Double
	Public dValue As Date
	Public dNulldate As Date
	Public nRate As Double
	Public nType_tar As Integer
	Public nAmount As Double
	Public mobjGrid As Object
	
	'%InsPostDP8002: Ejecuta el post de la transacción
	'%               Tabla de control de prima mínima(DP8002)
	Public Function InsPostDP8002(ByVal sAction As String, ByVal nId_table As Integer, ByVal sCol_id As String, ByVal sCol_Value As String, ByVal sCol_Type As String, ByVal nRate As Double, ByVal nType_tar As Integer, ByVal nAmount As Double, ByVal nUsercode As Integer, ByVal nRow As Integer, ByVal dEffecdate As Date) As Boolean
		Dim lrecInsPostDP8002 As eRemoteDB.Execute
		
		On Error GoTo InsPostDP8002_Err
		
		lrecInsPostDP8002 = New eRemoteDB.Execute
		
		With lrecInsPostDP8002
			.StoredProcedure = "InsDP8002pkg.InsPostDP8002"
			.Parameters.Add("sAction", sAction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 10, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sCol_id", sCol_id, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 5000, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sCol_Value", sCol_Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 5000, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sCol_Type", sCol_Type, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 5000, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nId_table", nId_table, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nRate", nRate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 9, 0, 6, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nType_tar", nType_tar, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAmount", nAmount, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 18, 0, 6, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nRow", nRow, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			InsPostDP8002 = .Run(False)
		End With
		
InsPostDP8002_Err: 
		If Err.Number Then
			InsPostDP8002 = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecInsPostDP8002 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecInsPostDP8002 = Nothing
	End Function
	'% DP8002: Genera una grilla dinamica con las columnas definidas para la tabla logica de tarifas
	Public Function MakeGrid(ByVal sCodispl As String, ByVal nId_table As Integer, ByVal sWindowType As String, ByVal bActionQuery As Boolean, ByVal nMainAction As Integer, ByVal sReload As String) As String
		Call Initialize()
		
		Call insDefineHeader(nId_table, bActionQuery, nMainAction, sReload)
		
		On Error GoTo MakeGrid_Err
		
MakeGrid_Err:
        If Err.Number Then
            MakeGrid = ""
            MakeGrid = MakeGrid & Err.Description
        End If
        On Error GoTo 0
	End Function
    '% insDefineHeader: Genera encabezado de la grilla
    Public Sub insDefineHeader(ByVal nId_table As Integer, ByVal bActionQuery As Boolean, ByVal nMainAction As Integer, ByVal sReload As String)
        Dim lTarif_columns As eTarif.tarif_columns
        Dim lTarif_column As eTarif.tarif_column
        Dim lintcount As Short

        mobjGrid.sCodisplPage = "DP8002"

        lTarif_columns = New tarif_columns

        '+ Columnas Variables
        lintcount = 0
        If lTarif_columns.Find_ColTab(nId_table) Then
            For Each lTarif_column In lTarif_columns
                lintcount = lintcount + 1
                If Trim(lTarif_column.sTablefk) <> "" Then
                    Call mobjGrid.Columns.AddPossiblesColumn(0, lTarif_column.sName_col, "Col_" & lTarif_column.nId_column, lTarif_column.sTablefk, 1,  ,  ,  ,  ,  ,  ,  , lTarif_column.nSize, lTarif_column.sName_col)
                Else
                    '+ VARCHAR2
                    If lTarif_column.nData_type = 1 Then
                        Call mobjGrid.Columns.AddTextColumn(0, lTarif_column.sName_col, "Col_" & lTarif_column.nId_column, lTarif_column.nSize, "",  , lTarif_column.sName_col)
                    Else
                        '+ DATE
                        If lTarif_column.nData_type = 2 Then
                            Call mobjGrid.Columns.AddDateColumn(0, lTarif_column.sName_col, "Col_" & lTarif_column.nId_column,  ,  , lTarif_column.sName_col)
                        Else
                            '+ NUMBER
                            If lTarif_column.nData_type = 3 Then
                                Call mobjGrid.Columns.AddNumericColumn(0, lTarif_column.sName_col, "Col_" & lTarif_column.nId_column, lTarif_column.nSize, eRemoteDB.Constants.intNull,  , lTarif_column.sName_col, False, lTarif_column.nDecimal)
                            End If
                        End If
                    End If
                End If

                If lintcount = 1 Then
                    mobjGrid.Columns("Col_" & lTarif_column.nId_column).EditRecord = True
                End If

                Call mobjGrid.Columns.AddHiddenColumn("hddColId" & lintcount, lTarif_column.nId_column)
                Call mobjGrid.Columns.AddHiddenColumn("hddColName" & lintcount, lTarif_column.sName_col)
                Call mobjGrid.Columns.AddHiddenColumn("hddColType" & lintcount, lTarif_column.nData_type)
            Next lTarif_column
        End If

        Call mobjGrid.Columns.AddHiddenColumn("hddTotalCol", lintcount)

        mobjGrid.Splits_Renamed.AddSplit(0, "Columnas de la tabla de Tarifa", lintcount)

        '+ Columnas fijas
        mobjGrid.Splits_Renamed.AddSplit(0, "Valor Tarifa", 3)
        Call mobjGrid.Columns.AddNumericColumn(0, "Tarifa", "tcnRate", 9, eRemoteDB.Constants.intNull,  , "Tarifa asociada a la fila de la tabla", False, 6)
        Call mobjGrid.Columns.AddPossiblesColumn(0, "Tipo de tasa", "cbeType_tar", "Table5584", 1, eRemoteDB.Constants.intNull,  ,  ,  ,  ,  , False,  , "Tipo de tasa")
        Call mobjGrid.Columns.AddNumericColumn(0, "Monto Fijo", "tcnAmount", 18, eRemoteDB.Constants.intNull,  , "Monto fijo", False, 6)
        Call mobjGrid.Columns.AddHiddenColumn("hddnRow", "")

        mobjGrid.Columns("cbeType_tar").EditRecord = True
        mobjGrid.Columns("cbeType_tar").TypeList = 2
        mobjGrid.Columns("cbeType_tar").List = "3,4"
        mobjGrid.Codispl = "DP8002"
        mobjGrid.ActionQuery = bActionQuery
        mobjGrid.DeleteButton = True
        mobjGrid.AddButton = True
        If lintcount > 5 Then
            mobjGrid.Top = 50
        Else
            mobjGrid.Top = 200
        End If
        If lintcount < 5 Then
            mobjGrid.Height = 300
        Else
            mobjGrid.Height = lintcount * 50
        End If
        mobjGrid.Width = 400
        mobjGrid.WidthDelete = 400
        mobjGrid.nMainAction = nMainAction
        mobjGrid.Columns("Sel").GridVisible = Not mobjGrid.ActionQuery
        mobjGrid.sDelRecordParam = "nRow='+ marrArray[lintIndex].hddnRow + '"
        If sReload = "1" Then
            mobjGrid.sReloadIndex = sReload
        End If
    End Sub
    '%Initialize: Inicializa las propiedades necesarias
    Private Sub Initialize()
		mobjGrid = eRemoteDB.NetHelper.CreateClassInstance("eFunctions.Grid")
	End Sub
	'%Initialize: Inicializa las propiedades necesarias
	Public Sub SetNothing()
		'UPGRADE_NOTE: Object mobjGrid may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		mobjGrid = Nothing
	End Sub
	
	
	'%Class_Initialize: Inicializa las propiedades cuando se instancia la clase
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		nId_table = eRemoteDB.Constants.intNull
		nId_column = eRemoteDB.Constants.intNull
		dEffecdate = eRemoteDB.Constants.dtmNull
		nRow = eRemoteDB.Constants.intNull
		nRate = eRemoteDB.Constants.intNull
		nType_tar = eRemoteDB.Constants.intNull
		nAmount = eRemoteDB.Constants.intNull
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
	
	'% InsValDP8002: Realiza todas las validaciones según especificaciones funcionales
	Public Function InsValDP8002(ByVal sCodispl As String, ByVal sCol_Value As String, ByVal sCol_Name As String, ByVal nRate As Double, ByVal nType_tar As Integer) As String
		Dim lrecInsValDP8002 As eRemoteDB.Execute
		Dim lobjErrors As Object
        Dim lstrError As String = String.Empty
		
		'+Definición de parámetros para stored procedure 'InsCA013pkg.InsValCA013Upd'
		'+Información leída el 24/04/2003
		On Error GoTo InsValDP8002_Err
		lrecInsValDP8002 = New eRemoteDB.Execute
		With lrecInsValDP8002
			.StoredProcedure = "InsDP8002pkg.insvalDP8002"
			.Parameters.Add("sCol_Value", sCol_Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 5000, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sCol_Name", sCol_Name, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 5000, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nRate", nRate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 9, 0, 6, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nType_tar", nType_tar, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("Arrayerrors", String.Empty, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 4000, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Run(False)
			lstrError = .Parameters("Arrayerrors").Value
			
			If lstrError <> String.Empty Then
				lobjErrors = eRemoteDB.NetHelper.CreateClassInstance("eFunctions.Errors")
				With lobjErrors
					.ErrorMessage("DP8002",  ,  ,  ,  ,  , lstrError)
					InsValDP8002 = lobjErrors.Confirm
				End With
			End If
			
		End With
InsValDP8002_Err: 
		If Err.Number Then
			InsValDP8002 = "InsValDP8002: " & Err.Description
		End If
		'UPGRADE_NOTE: Object lrecInsValDP8002 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecInsValDP8002 = Nothing
		'UPGRADE_NOTE: Object lobjErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lobjErrors = Nothing
		On Error GoTo 0
	End Function
End Class






