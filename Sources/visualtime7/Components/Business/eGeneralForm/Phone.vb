Option Strict Off
Option Explicit On
Public Class Phone
	
	'+ Propiedades según la tabla en el sistema el 18/01/2000.
	'+ Los campos llaves corresponden a nLed_compan, sBud_code, nYear, nCurrency, sAccount, sAux_accoun, sCost_cente y nMonth
	
	'Column_name                  Type                     Computed Length      Prec  Scale Nullable TrimTrailingBlanks FixedLenNullInSource
	'---------------------------- ------------------------ -------- ----------- ----- ----- -------- ------------------ --------------------
	Public nRecowner As Integer '         smallint                    no                                  2           5     0     no                                  (n/a)                               (n/a)
	Public sKeyAddress As String '         char                        no                                  20                      no                                  yes                                 no
	Public nKeyPhones As Integer '         smallint                    no                                  2           5     0     no                                  (n/a)                               (n/a)
	Public nArea_code As Integer '         int                         no                                  4           10    0     yes                                 (n/a)                               (n/a)
	Public dEffecdate As Date '         datetime                    no                                  8                       no                                  (n/a)                               (n/a)
	Public sPhone As String '         char                        no                                  11                      yes                                 yes                                 yes
	Public nOrder As Integer '         smallint                    no                                  2           5     0     yes                                 (n/a)                               (n/a)
	Public nExtens1 As Integer '         smallint                    no                                  2           5     0     yes                                 (n/a)                               (n/a)
	Public nPhone_type As Integer '         smallint                    no                                  2           5     0     no                                  (n/a)                               (n/a)
	Public nExtens2 As Integer '         smallint                    no                                  2           5     0     yes                                 (n/a)                               (n/a)
	Public nUsercode As Integer '         smallint                    no                                  2           5     0     no                                  (n/a)                               (n/a)
	Public dNulldate As Date '         datetime                    no                                  8                       yes                                 (n/a)                               (n/a)
	
	'- Se definen las variable auxiliares
	
	'- Se define la variable para indicar el estado de cada instancia en la colección
	
	Public nStatusInstance As Integer
	Private Enum eActions
		clngAdd = 1
		clndUpdate = 2
		clngDelete = 3
	End Enum
	
	'% Add: Permite añadir registros en la tabla de resultados presupuestarios
	Public Function Add() As Boolean
		Add = insUpdPhones(eActions.clngAdd)
	End Function
	
	'% Update: Permite modificar registros en la tabla de resultados presupuestarios
	Public Function Update() As Boolean
		Update = insUpdPhones(eActions.clndUpdate)
	End Function
	
	'% Delete: Permite eliminar registros en la tabla de resultados presupuestarios
	Public Function Delete() As Boolean
		Delete = insUpdPhones(eActions.clngDelete)
	End Function
	
	'% Find: Permite buscar registros en la tabla de resultados presupuestarios
	Function Find(ByVal sKeyAddress As String, ByVal nKeyPhones As Integer, ByVal nRecowner As Address.eTypeRecOwner, ByVal dEffecdate As Date, Optional ByVal bFind As Boolean = False) As Boolean
		Dim lrecreaPhones As eRemoteDB.Execute
		lrecreaPhones = New eRemoteDB.Execute
		
		On Error GoTo Find_err
		
		If nKeyPhones = Me.nKeyPhones And nRecowner = Me.nRecowner And sKeyAddress = Me.sKeyAddress And dEffecdate = Me.dEffecdate And Not bFind Then
			Find = True
		Else
			
			'Definición de parámetros para stored procedure 'insudb.reaPhones'
			'Información leída el 12/07/2000 14:40:30
			
			With lrecreaPhones
				.StoredProcedure = "reaPhones"
				.Parameters.Add("nRecowner", nRecowner, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("sKeyAddress", sKeyAddress, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 50, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nKeyPhones", nKeyPhones, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nAll", 2, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				Find = .Run
				If Find Then
					Me.nRecowner = .FieldToClass("nRecowner")
					Me.sKeyAddress = .FieldToClass("sKeyAddress")
					Me.nKeyPhones = .FieldToClass("nKeyPhones")
					Me.nArea_code = .FieldToClass("nArea_code")
					Me.dEffecdate = .FieldToClass("dEffecdate")
					Me.sPhone = .FieldToClass("sPhone")
					Me.nOrder = .FieldToClass("nOrder")
					Me.nExtens1 = .FieldToClass("nExtens1")
					Me.nPhone_type = .FieldToClass("nPhone_type")
					Me.nExtens2 = .FieldToClass("nExtens2")
					Me.dNulldate = .FieldToClass("dNulldate")
					.RCloseRec()
				End If
			End With
			'UPGRADE_NOTE: Object lrecreaPhones may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
			lrecreaPhones = Nothing
		End If
		
Find_err: 
		If Err.Number Then
			Find = False
		End If
		On Error GoTo 0
	End Function
	
	'% Class_Initialize: se controla la apertura de la clase
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		nRecowner = numNull
		sKeyAddress = String.Empty
		nKeyPhones = numNull
		nArea_code = numNull
		dEffecdate = dtmNull
		sPhone = String.Empty
		nOrder = numNull
		nExtens1 = numNull
		nPhone_type = numNull
		nExtens2 = numNull
		nUsercode = numNull
		dNulldate = dtmNull
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
	
	'% insUpdPhones. Esta funcion se encarga de realizar la actualización de la tabla Phones
	'% en la base de datos. Como parametro para la llamada a los SP, utiliza los valores
	'% contenidos en las propiedades de la clase
	Private Function insUpdPhones(ByRef llngAction As eActions) As Boolean
		Dim lrecinsUpdPhones As eRemoteDB.Execute
		lrecinsUpdPhones = New eRemoteDB.Execute
		
		On Error GoTo insUpdPhones_err
		
		'Definición de parámetros para stored procedure 'insudb.insUpdPhones'
		'Información leída el 12/07/2000 14:46:14
		
		With lrecinsUpdPhones
            .StoredProcedure = "insUpdPhones"

            .Parameters.Add("nAction", llngAction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nRecowner", nRecowner, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sKeyAddress", sKeyAddress, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 50, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nKeyPhones", nKeyPhones, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nArea_code", .ClassToField(nArea_code, eRemoteDB.Parameter.eRmtDataType.rdbInteger), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sPhone", .ClassToField(sPhone, eRemoteDB.Parameter.eRmtDataType.rdbVarChar), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 11, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nOrder", .ClassToField(nOrder, eRemoteDB.Parameter.eRmtDataType.rdbInteger), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nExtens1", .ClassToField(nExtens1, eRemoteDB.Parameter.eRmtDataType.rdbInteger), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPhone_type", .ClassToField(nPhone_type, eRemoteDB.Parameter.eRmtDataType.rdbInteger), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nExtens2", .ClassToField(nExtens2, eRemoteDB.Parameter.eRmtDataType.rdbInteger), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
            .Parameters.Add("IN_DEFFECDATE", IIf(IsDBNull(.ClassToField(dEffecdate, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp)), Today, dEffecdate), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("IN_DNULLDATE", .ClassToField(dNulldate, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			insUpdPhones = .Run(False)
		End With
		'UPGRADE_NOTE: Object lrecinsUpdPhones may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecinsUpdPhones = Nothing
		
insUpdPhones_err: 
		If Err.Number Then
			insUpdPhones = False
		End If
	End Function
	
	'% UpdPhonesKey: Cambia un viejo cliente por uno nuevo en la tabla Phones
	Public Function UpdPhonesKey(ByVal nRecowner As Integer, ByVal sNewKey As String, ByVal sOldKey As String, ByVal nUsercode As Integer) As Boolean
		
		'- Se define la variable lrecreaTab_modcli
		Dim lrecupdPhones_key As eRemoteDB.Execute
		
		lrecupdPhones_key = New eRemoteDB.Execute
		
		On Error GoTo UpdPhonesKey_err
		
		'+ Definición de parámetros para stored procedure 'insudb.updPhones_key'
		'+ Información leída el 30/10/2000 11:59:28 AM
		With lrecupdPhones_key
			.StoredProcedure = "updPhones_key"
			.Parameters.Add("nRecowner", nRecowner, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sNewKey", sNewKey, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sOldKey", sOldKey, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			UpdPhonesKey = .Run(False)
		End With
		
		'UPGRADE_NOTE: Object lrecupdPhones_key may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecupdPhones_key = Nothing
		
UpdPhonesKey_err: 
		If Err.Number Then
			UpdPhonesKey = False
		End If
		On Error GoTo 0
	End Function
End Class






