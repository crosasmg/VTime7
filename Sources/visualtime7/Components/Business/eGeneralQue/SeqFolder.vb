Option Strict Off
Option Explicit On
Public Class SeqFolder
	'%-------------------------------------------------------%'
	'% $Workfile:: SeqFolder.cls                            $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 1:21p                                $%'
	'% $Revision:: 4                                        $%'
	'%-------------------------------------------------------%'
	'**+ Properties according to the table in the system in January 18,2000.
	'+ Propiedades según la tabla en el sistema el 18/01/2000.
	'**+ The key fields corresponds to nLed_compan, sBud_code, nYear, nCurrency, sAccount, sAuz_accoun, sCost_cente and nMonth
	'+ Los campos llaves corresponden a nLed_compan, sBud_code, nYear, nCurrency, sAccount, sAux_accoun, sCost_cente y nMonth
	
	'Column_name                  Type                     Computed Length      Prec  Scale Nullable TrimTrailingBlanks FixedLenNullInSource
	'---------------------------- ------------------------ -------- ----------- ----- ----- -------- ------------------ --------------------
	Public nQueryType As Integer '                                                                                                                       smallint                                                                                                                         no                                  2           5     0     no                                  (n/a)                               (n/a)
	Public nParent As Integer '                                                                                                                            smallint                                                                                                                         no                                  2           5     0     no                                  (n/a)                               (n/a)
	Public nFolder As Integer '                                                                                                                          smallint                                                                                                                         no                                  2           5     0     no                                  (n/a)                               (n/a)
	Public nSequence As Integer '                                                                                                                        smallint                                                                                                                         no                                  2           5     0     no                                  (n/a)                               (n/a)
	Public nUsercode As Integer '                                                                                                                        smallint                                                                                                                         no                                  2           5     0     no                                  (n/a)                               (n/a)
	
	'**- Define the additional variables
	'- Se definen las variable auxiliares
	'**- Define the variable to indicate the status of each instance in the collection.
	'- Se define la variable para indicar el estado de cada instancia en la colección
	
	Public nStatusInstance As Integer
	Private Enum eActions
		clngAdd = 1
		clndUpdate = 2
		clngDelete = 3
	End Enum
	
	'**% Add: add records in the budget results table.
	'% Add: Permite añadir registros en la tabla de resultados presupuestarios
	Public Function Add() As Boolean
		Add = insUpdSeqFolder(eActions.clngAdd)
	End Function
	
	'**% Update: modify records in the budget results table.
	'% Update: Permite modificar registros en la tabla de resultados presupuestarios
	Public Function Update() As Boolean
		Update = insUpdSeqFolder(eActions.clndUpdate)
	End Function
	
	'**% Delete: delete records in the budget results table.
	'% Delete: Permite eliminar registros en la tabla de resultados presupuestarios
	Public Function Delete() As Boolean
		Delete = insUpdSeqFolder(eActions.clngDelete)
	End Function
	
	'**% Find: search records in the budget results table.
	'% Find: Permite buscar registros en la tabla de resultados presupuestarios
	Function Find(ByVal QueryType As Integer, ByVal Parent As Integer, ByVal Folder As Integer, Optional ByVal lblnFind As Boolean = False) As Boolean
		Dim lrecreaSeqFolder As eRemoteDB.Execute
		lrecreaSeqFolder = New eRemoteDB.Execute
		If QueryType = nQueryType And Parent = nParent And Folder = nFolder And Not lblnFind Then
			Find = True
		Else
			With lrecreaSeqFolder
				.StoredProcedure = "reaSeqFolderAll"
				.Parameters.Add("nQueryType", QueryType, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nParent", Parent, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nFolder", Folder, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				Find = .Run
				If Find Then
					nQueryType = .FieldToClass("nQueryType")
					nParent = .FieldToClass("nParent")
					nFolder = .FieldToClass("nFolder")
					nSequence = .FieldToClass("nSequence")
					.RCloseRec()
				End If
			End With
			'UPGRADE_NOTE: Object lrecreaSeqFolder may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
			lrecreaSeqFolder = Nothing
		End If
	End Function
	
	'*** Class_Initialize: controls the opening of the class.
	'* Class_Initialize: se controla la apertura de la clase
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		'nUsercode = GetSetting("TIME", "GLOBALS", "USERCODE")
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
	
	'**% insUpdSeqFolder. This function is in charge of making the update of the SeqFolder table
	'**% in the data base. As a parameter for the call of the SP, use the contained values
	'**% in the class properties.
	'%insUpdSeqFolder. Esta funcion se encarga de realizar la actualización de la tabla SeqFolder
	'%en la base de datos. Como parametro para la llamada a los SP, utiliza los valores
	'%contenidos en las propiedades de la clase
	Private Function insUpdSeqFolder(ByRef llngAction As eActions) As Boolean
		Dim lrecinsUpdSeqFolder As eRemoteDB.Execute
		lrecinsUpdSeqFolder = New eRemoteDB.Execute
		
		'**+ Parameter definition for the stored procedure 'insudb.insUpdSeqFolder'
		'+Definición de parámetros para stored procedure 'insudb.insUpdSeqFolder'
		'**+ Information read on July 11,2000  11:08:23
		'+Información leída el 11/07/2000 11:08:23
		
		With lrecinsUpdSeqFolder
			.StoredProcedure = "insSeqFolder"
			.Parameters.Add("nQueryType", nQueryType, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("Parent", nParent, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nFolder", nFolder, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nSequence", nSequence, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAction", llngAction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			insUpdSeqFolder = .Run(False)
		End With
		'UPGRADE_NOTE: Object lrecinsUpdSeqFolder may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecinsUpdSeqFolder = Nothing
	End Function
End Class






