Option Strict Off
Option Explicit On
<System.Runtime.InteropServices.ProgId("Batch_Param_NET.Batch_Param")> Public Class Batch_Param
	'%-------------------------------------------------------%'
	'% $Workfile:: Batch_param.cls                          $%'
	'% $Author:: Mpalleres                                  $%'
	'% $Date:: 9-09-09 19:22                                $%'
	'% $Revision:: 2                                        $%'
	'%-------------------------------------------------------%'
	
	Public nBatch As Integer
	Public nArea As enmBatchParArea
	Public sValue As String
	'Public nType        As enmBatchParType
	Public nUsercode As Integer
	
	Public nSheet As Integer
	
	
	
	'-Propiedad sDescript se usa para la descripcion del nombre
	'-y del valor
	Public sDescript As String
	Public sName As String
	
	
	'-Tipo de datos de los parametros
	Public Enum enmBatchParType
		batchParTypeStr = 1 '+Tipo caracter
		batchParTypeNum = 2 '+Tipo numerico
		batchParTypeDtm = 3 '+Tipo fecha
	End Enum
	
	'-Area de uso de los parametros
	Public Enum enmBatchParArea
		batchParAreaProc = 1 '+Area de proceso
		batchParAreaRes = 2 '+Area de resultados
	End Enum
	
	'-Tipos de datos para almacenar valores de parametros
	Private Structure udtValue
		Dim sKey As String
		Dim nArea As Integer
		Dim nSeq As Integer
		Dim nType As Integer
		Dim sValue As String
		Dim sDescript As String
	End Structure
	
	'-Tipos de datos para almacenar información de parámetros
	Private Structure udtValueInfo
		Dim nArea As Integer
		Dim nSeq As Integer
		Dim sName As String
		Dim sDescript As String
	End Structure
	
	'-Arreglo con valores y nombres de parametros
	Private arrValue() As udtValue
	Private arrValueInfo() As udtValueInfo
	'-COntador de registros del arreglo
	Private mintCountValue As Integer
	Private mintCountName As Integer
	
	'-Cadena con parametros a cargar
	Private mstrParams As String
	'-Datos de parametros
	Private mlngArea As Integer
	Private mintSeq As Short
	Private mstrKey As String
	
	'-Caracter del separador de miles y decimal
	Private mstrDecChar As String
	Private mstrMilChar As String
	'%CountName: Cantidad de registros en arreglo de nombres
	'------------------------------------------------------
	Public ReadOnly Property CountName() As Object
		Get
			'------------------------------------------------------
			
			CountName = mintCountName
			
		End Get
	End Property
	
	
	'%CountValueInfo: Cantidad de registros en arreglo de valores
	'------------------------------------------------------
	Public ReadOnly Property CountValue() As Object
		Get
			'------------------------------------------------------
			
			CountValue = mintCountValue
			
		End Get
	End Property
	
	
	'% sKey: Retorna una clave de proceso
	'------------------------------------------------------------
	
	'% sKey: Asigna una clave de proceso desde fuera del objeto
	'------------------------------------------------------------
	Public Property sKey() As String
		Get
			'------------------------------------------------------------
			
			If mstrKey = String.Empty Then
				mstrKey = Today.ToString("yyyyMMdd") & TimeOfDay.ToString("hhmmss") & Format(Me.nUsercode, "00000")
				'+Se el antepone una "T" para completar los 20 espacios
                mstrKey = "T" & mstrKey.PadRight(19, "0")
			End If
			
			sKey = mstrKey
			
		End Get
		Set(ByVal Value As String)
			'------------------------------------------------------------
			
            mstrKey = Value.PadRight(20, "0")
			
		End Set
	End Property
	
	
	'%Value: Retorna campo formateado
	Public ReadOnly Property Value(ByVal nIdx As Integer) As Object
		Get
			Dim sValue As String
			
			If nIdx <= mintCountValue Then
				With arrValue(nIdx)
					Me.nArea = .nArea
					'Me.nType = .nType
					mstrKey = .sKey
					Me.sValue = .sValue
					Me.sDescript = .sDescript
					
					If .nType = enmBatchParType.batchParTypeDtm Then
						'+Como fecha se grabó como yyyyMMdd se realiza conversión
						sValue = .sValue
						If sValue = "" Then
							Value = eRemoteDB.Constants.dtmNull
						Else
							On Error Resume Next
							Value = DateSerial(CInt(Mid(sValue, 1, 4)), CInt(Mid(sValue, 5, 2)), CInt(Mid(sValue, 7, 2)))
							If Err.Number Then
								Value = eRemoteDB.Constants.dtmNull
							End If
						End If
					ElseIf .nType = enmBatchParType.batchParTypeNum Then 
						If .sValue = "" Then
							Value = eRemoteDB.Constants.intNull
						Else
							'+Como en campo se grabó numero con punto como signo decimal
							'+se usa funcion Val() para reconvertir el texto en numero
							Value = CDbl(Val(.sValue))
						End If
					Else
						Value = .sValue
					End If
					
				End With
			Else
				Value = ""
			End If
			
		End Get
	End Property
	
	'%ValueInfo: Carga una casilla del arreglo de datos del parametro
	Public ReadOnly Property Name(ByVal nIdx As Integer) As String
		Get
			
			If nIdx <= mintCountName Then
				With arrValueInfo(nIdx)
					Me.nArea = .nArea
					Me.sName = .sName
					Me.sDescript = .sDescript
					
					Name = .sName
					
				End With
			Else
				Name = ""
			End If
			
		End Get
	End Property
	
	
	'%Find_Value: Carga matriz con datos de parametros
	Public Function Find_Value(ByVal sKey As String, ByVal nBatch As Integer, ByVal nArea As enmBatchParArea) As Boolean
		Dim lrecreaBatch_param As eRemoteDB.Execute
		
		On Error GoTo Find_Value_Err
		
		lrecreaBatch_param = New eRemoteDB.Execute
		
		'+
		'+ Definición de store procedure reaBatch_param al 05-28-2003 12:31:51
		'+
		With lrecreaBatch_param
			.StoredProcedure = "reaBatch_param_value"
			.Parameters.Add("sKey", sKey, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBatch", nBatch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nArea", nArea, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 1, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			Find_Value = .Run
			
			If Find_Value Then
				'UPGRADE_WARNING: Lower bound of array arrValue was changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="0F1C9BE1-AF9D-476E-83B1-17D43BECFF20"'
				ReDim arrValue(50)
				mintCountValue = 0
				Do While Not .EOF
					mintCountValue = mintCountValue + 1
					If mintCountValue Mod 50 = 0 Then
						'UPGRADE_WARNING: Lower bound of array arrValue was changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="0F1C9BE1-AF9D-476E-83B1-17D43BECFF20"'
						ReDim Preserve arrValue(mintCountValue + 50)
					End If
					arrValue(mintCountValue).sKey = .FieldToClass("sKey")
					arrValue(mintCountValue).nArea = .FieldToClass("nArea")
					arrValue(mintCountValue).nSeq = .FieldToClass("nSeq")
					arrValue(mintCountValue).sValue = .FieldToClass("sValue")
					arrValue(mintCountValue).sDescript = .FieldToClass("sDescript")
					arrValue(mintCountValue).nType = .FieldToClass("nDataType")
					
					.RNext()
				Loop 
				.RCloseRec()
				'UPGRADE_WARNING: Lower bound of array arrValue was changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="0F1C9BE1-AF9D-476E-83B1-17D43BECFF20"'
				ReDim Preserve arrValue(mintCountValue)
			End If
		End With
		
Find_Value_Err: 
		If Err.Number Then
			Find_Value = False
		End If
		'UPGRADE_NOTE: Object lrecreaBatch_param may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaBatch_param = Nothing
		On Error GoTo 0
	End Function
	
	
	'%Find_Name: Carga matriz con nombres de parametros
	Public Function Find_Name(ByVal nBatch As Integer) As Boolean
		Dim lrecreaBatch_param As eRemoteDB.Execute
		
		On Error GoTo Find_Name_Err
		
		lrecreaBatch_param = New eRemoteDB.Execute
		
		'+
		'+ Definición de store procedure reaBatch_param al 05-28-2003 12:31:51
		'+
		With lrecreaBatch_param
			.StoredProcedure = "reaBatch_param_info"
			.Parameters.Add("nBatch", nBatch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			Find_Name = .Run
			
			If Find_Name Then
				'UPGRADE_WARNING: Lower bound of array arrValueInfo was changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="0F1C9BE1-AF9D-476E-83B1-17D43BECFF20"'
				ReDim arrValueInfo(50)
				mintCountName = 0
				Do While Not .EOF
					mintCountName = mintCountName + 1
					arrValueInfo(mintCountName).nArea = .FieldToClass("nArea")
					arrValueInfo(mintCountName).sName = .FieldToClass("sName")
					arrValueInfo(mintCountName).sDescript = .FieldToClass("sDescript")
					.RNext()
				Loop 
				.RCloseRec()
				'UPGRADE_WARNING: Lower bound of array arrValueInfo was changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="0F1C9BE1-AF9D-476E-83B1-17D43BECFF20"'
				ReDim Preserve arrValueInfo(mintCountName)
			End If
		End With
		
Find_Name_Err: 
		If Err.Number Then
			Find_Name = False
		End If
		'UPGRADE_NOTE: Object lrecreaBatch_param may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaBatch_param = Nothing
		On Error GoTo 0
	End Function
	
	
	
	'%Add: Agrega un nuevo parametro a la cadena existente
	'---------------------------------------------------------
	Public Function Add(ByVal nArea As enmBatchParArea, ByVal vValue As Object, Optional ByVal sDescript As String = "") As String
		'---------------------------------------------------------
		Dim sParam As String
		Dim nVartype As VariantType
		Dim sAux As String
		
		'UPGRADE_WARNING: VarType has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
		nVartype = VarType(vValue)
		
		Select Case nVartype
			'+Numeros se almacenan con punto como separador de miles
			Case VariantType.Integer, VariantType.Short, VariantType.Double
				nVartype = enmBatchParType.batchParTypeNum
				If vValue = eRemoteDB.Constants.intNull Then
					sAux = ""
				Else
					sAux = vValue
					If mstrDecChar <> "." Then
						sAux = Replace(sAux, mstrMilChar, "")
						sAux = Replace(sAux, mstrDecChar, ".")
					End If
				End If
				vValue = sAux
				
				'+Las fechas se almacenan con el formato indicado
			Case VariantType.Date
				nVartype = enmBatchParType.batchParTypeDtm
                If vValue = eRemoteDB.Constants.dtmNull Or vValue Is Nothing Then
                    vValue = ""
                Else
                    vValue = Format(vValue, "yyyyMMdd")
                End If
			Case Else
				nVartype = enmBatchParType.batchParTypeStr
				
		End Select
		
		'+Si es un area nueva se inicializa contador de parametros
		If mlngArea <> nArea Then
			mlngArea = nArea
			mintSeq = 1
		Else
			mintSeq = mintSeq + 1
		End If
		
		'+Se forma cadena con datos del parametro
		sParam = "|" & nArea & "::" & mintSeq & "::" & vValue & "::" & nVartype & "::" & sDescript
		
		'+Se anexa a parámetros existentes
		mstrParams = mstrParams & sParam
		
		Add = sParam
		
	End Function
	
	'%Reset: Borra todos los parametros cargados
	'------------------------------------------------------------
	'UPGRADE_NOTE: Reset was upgraded to Reset_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Public Sub Reset_Renamed()
		'------------------------------------------------------------
		
		mstrParams = ""
		mstrKey = ""
		
	End Sub
	
	
	'%Save: Almacena los parámetros de una transacción
	'----------------------------------------------------------
	Public Function Save() As Boolean
		'----------------------------------------------------------
		Dim lrecinsBatch_param As eRemoteDB.Execute
		On Error GoTo insBatch_param_Err
		
		lrecinsBatch_param = New eRemoteDB.Execute
		
		'+
		'+ Definición de store procedure insBatch_param al 05-28-2003 12:03:18
		'+
		With lrecinsBatch_param
			.StoredProcedure = "insBatch_Job_Param"
			.Parameters.Add("sKey", Me.sKey, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBatch", Me.nBatch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sParams", mstrParams, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 500, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", Me.nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nSheet", Me.nSheet, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Save = .Run(False)
		End With
		
insBatch_param_Err: 
		If Err.Number Then
			Save = False
		End If
		'UPGRADE_NOTE: Object lrecinsBatch_param may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecinsBatch_param = Nothing
		On Error GoTo 0
	End Function
	
	
	
	'%Class_Initialize:
	'-------------------------------------------------------
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		'-------------------------------------------------------
		Dim lstrValue As String
		
		nUsercode = eRemoteDB.Constants.intNull
		nBatch = eRemoteDB.Constants.intNull
		'nType = NumNull
		nArea = eRemoteDB.Constants.intNull
		nSheet = eRemoteDB.Constants.intNull
		
		mlngArea = eRemoteDB.Constants.intNull
		mstrKey = String.Empty
		
		lstrValue = Format(1000.1, "#,###.#")
		mstrDecChar = Mid(lstrValue, 6, 1)
		mstrMilChar = Mid(lstrValue, 2, 1)
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
End Class






