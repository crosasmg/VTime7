Option Strict Off
Option Explicit On
Public Class tarif_column
	'%-------------------------------------------------------%'
	'% $Workfile:: tarif_column.cls                         $%'
	'% $Author:: Pmanzur                                   $%'
	'% $Date:: 21/04/05 1:39p                                $%'
	'% $Revision:: 1                                        $%'
	'%-------------------------------------------------------%'
	'+
	'+ Estructura de tabla TARIF_COLUMN al 21/04/05 1:39p
	
	Public nId_column As Integer ' NUMBER     22   0     5    N
	Public sTable As String ' CHAR       255  0     0    S
	Public sColumn As String ' CHAR       255  0     0    S
	Public sName_col As String ' CHAR       255  0     0    S
	Public nData_type As Integer ' NUMBER     22   0     5    N
	Public nSize As Integer ' NUMBER     22   0     5    N
	Public nDecimal As Integer ' NUMBER     22   0     5    N
	Public sData_type As String ' CHAR       255  0     0    S
	Public sTablefk As String ' CHAR       30   0     0    S
	Public nUsercode As Integer ' NUMBER     22   0     5    S
	
	'-Se define la lista enumerada para ser usada con la propiedad ETextAlign
	Public Enum TextAlign
		LeftAling
		RigthAling
	End Enum
	
	'insPostMDP8003: Actualiza un registro en la tabla
	Public Function insPostMDP8003(ByVal sAction As String, ByVal nId_column As Integer, Optional ByVal sTable As String = "", Optional ByVal sColumn As String = "", Optional ByVal sName_col As String = "", Optional ByVal nData_type As Integer = 0, Optional ByVal nSize As Integer = 0, Optional ByVal nDecimal As Integer = 0, Optional ByVal sData_type As String = "", Optional ByVal nUsercode As Integer = 0, Optional ByVal sTablefk As String = "") As Boolean
		Dim lrectarif_column As Object
		Dim nAction As Short
		On Error GoTo insPostMDP8003_Err
		
		lrectarif_column = eRemoteDB.NetHelper.CreateClassInstance("eRemoteDB.Execute")
		
		Select Case sAction
			Case "Add"
				nAction = 1
			Case "Update"
				nAction = 2
			Case "Del"
				nAction = 3
		End Select
		'+
		'+ Definición de store procedure Insupdtarif_column al al 21/04/05 1:39p
		'+
		With lrectarif_column
			.StoredProcedure = "Insupdtarif_column"
			.Parameters.Add("nId_column", nId_column, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sTable", sTable, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 30, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sColumn", sColumn, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 30, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sName_col", sName_col, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 30, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nData_type", nData_type, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sData_type", sData_type, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 30, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nSize", nSize, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDecimal", nDecimal, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sTablefk", sTablefk, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 30, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAction", nAction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCommit", 1, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			insPostMDP8003 = .Run(False)
		End With
		
insPostMDP8003_Err: 
		If Err.Number Then
			insPostMDP8003 = False
		End If
		'UPGRADE_NOTE: Object lrectarif_column may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrectarif_column = Nothing
		On Error GoTo 0
	End Function
	
	'% insValMDP8003: Valida la tabla tarif_column
	Public Function insValMDP8003(ByVal sCodispl As String, ByVal sAction As String, ByVal nId_column As Integer, Optional ByVal sTable As String = "", Optional ByVal sColumn As String = "", Optional ByVal sName_col As String = "", Optional ByVal nData_type As Integer = 0, Optional ByVal nSize As Integer = 0, Optional ByVal nDecimal As Integer = 0, Optional ByVal sData_type As String = "", Optional ByVal nUsercode As Integer = 0) As String
		Dim lclsErrors As Object
		Dim blnError As Boolean
		
		On Error GoTo insValMDP8003_Err
		
		lclsErrors = eRemoteDB.NetHelper.CreateClassInstance("eFunctions.Errors")
		
		sCodispl = "MDP8003"
		'+Validaciones
		If Trim(sAction) = "Del" Then
			If Find(nId_column, sTable, sColumn, sName_col, 1) Then
				Call lclsErrors.ErrorMessage(sCodispl, 8000)
			End If
		Else
			If Trim(sTable) = String.Empty Then
				blnError = True
				Call lclsErrors.ErrorMessage(sCodispl, 55537,  , TextAlign.LeftAling, "El campo tabla ")
			End If
			If Trim(sColumn) = String.Empty Then
				blnError = True
				Call lclsErrors.ErrorMessage(sCodispl, 55537,  , TextAlign.LeftAling, "El campo columna ")
			End If
			If Trim(sName_col) = String.Empty Then
				blnError = True
				Call lclsErrors.ErrorMessage(sCodispl, 55537,  , TextAlign.LeftAling, "El campo nombre de la columna ")
			End If
			If Not blnError Then
				If Find(nId_column, sTable, sColumn, sName_col, 2) Then
					Call lclsErrors.ErrorMessage(sCodispl, 10931)
				End If
			End If
		End If
		
		insValMDP8003 = lclsErrors.Confirm
		
insValMDP8003_Err: 
		If Err.Number Then
			insValMDP8003 = insValMDP8003 & Err.Description
		End If
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
		On Error GoTo 0
		
	End Function
	
	'Find: Busca el nombre de una columna de la tabla
	Public Function Find(ByVal nId_column As Integer, ByVal sTable As String, ByVal sColumn As String, ByVal sName_col As String, ByVal nOption As Short) As Boolean
		Dim lrectarif_column As Object
		Dim nExists As Short
		On Error GoTo Find_Err
		
		lrectarif_column = eRemoteDB.NetHelper.CreateClassInstance("eRemoteDB.Execute")
		
		'+ Definición de store procedure Insupdtarif_column al al 21/04/05 1:39p
		'+
		With lrectarif_column
			.StoredProcedure = "ReaValtarif_column"
			.Parameters.Add("nId_column", nId_column, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sTable", sTable, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 30, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sColumn", sColumn, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 30, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sName_col", sName_col, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 30, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nOption", nOption, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nExists", nExists, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Run(False)
			Find = .Parameters("nExists").Value = 1
		End With
		
Find_Err: 
		If Err.Number Then
			Find = False
		End If
		'UPGRADE_NOTE: Object lrectarif_column may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrectarif_column = Nothing
		On Error GoTo 0
	End Function
	
	'Find: Busca los valores de nData_type,nSize,nDecimal en la definicion de la columna
	Public Function Find_columns(ByVal sTable As String, ByVal sColumn As String) As Boolean
		Dim lrectarif_column As Object
		Dim nExists As Short
		
		On Error GoTo Find_columns_Err
		
		lrectarif_column = eRemoteDB.NetHelper.CreateClassInstance("eRemoteDB.Execute")
		
		'+ Definición de store procedure Insupdtarif_column al al 21/04/05 1:39p
		'+
		With lrectarif_column
			.StoredProcedure = "Reatarif_column_1"
			.Parameters.Add("sTable", sTable, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 30, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sColumn", sColumn, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 30, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sData_type", sData_type, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 30, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nData_type", nData_type, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nSize", nSize, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDecimal", nDecimal, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sTablefk", sTablefk, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 30, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Run(False)
			Find_columns = .Parameters("nData_type").Value <> eRemoteDB.Constants.intNull
			If Find_columns Then
				sData_type = .Parameters("sData_type").Value
				nData_type = .Parameters("nData_type").Value
				nSize = .Parameters("nSize").Value
				nDecimal = .Parameters("nDecimal").Value
				sTablefk = .Parameters("sTablefk").Value
			Else
				sData_type = String.Empty
				nData_type = eRemoteDB.Constants.intNull
				nSize = eRemoteDB.Constants.intNull
				nDecimal = eRemoteDB.Constants.intNull
			End If
		End With
		
Find_columns_Err: 
		If Err.Number Then
			Find_columns = False
		End If
		'UPGRADE_NOTE: Object lrectarif_column may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrectarif_column = Nothing
		On Error GoTo 0
	End Function
	
	'%Class_Initialize: Inicializa las propiedades cuando se instancia la clase
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		nId_column = eRemoteDB.Constants.intNull
		sTable = String.Empty
		sColumn = String.Empty
		sName_col = String.Empty
		nData_type = eRemoteDB.Constants.intNull
		nSize = eRemoteDB.Constants.intNull
		nDecimal = eRemoteDB.Constants.intNull
		sData_type = CStr(eRemoteDB.Constants.intNull)
		nUsercode = eRemoteDB.Constants.intNull
		sTablefk = String.Empty
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
End Class






