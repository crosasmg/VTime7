Option Strict Off
Option Explicit On
Public Class Life_loads
	Implements System.Collections.IEnumerable
	'**+Objective: Collection that supports the class: Life_load
	'**+Version: $$Revision: $
	'+Objetivo: Colección que le da soporte a la clase: Life_load
	'+Version: $$Revision: $
	
	'**-Objective:
	'-Objetivo:
	Private mCol As Collection
	
	'**-Objective: Code of the Line of Business. The possible values as per table 10.
	'-Objetivo: Código del ramo comercial. Valores posibles según tabla 10.
	Private nAuxBranch As Integer
	
	'**-Objective: Code of the product.
	'-Objetivo: Código del producto.
	Private nAuxProduct As Integer
	
	'**-Objective: Code of Current account Load
	'-Objetivo: Código del cargo a cuenta corriente
	Private nAuxLoad_cod As Integer
	
	'**-Objective: Date which from the record is valid.
	'-Objetivo: Fecha de efecto del registro.
	Private dAuxEffecdate As Date
	
	'**%Objective: Add a new instance of the Life_load class to the collection
	'**%Parameters:
	'**%    nStatusInstance -
	'**%    nBranch         - Code of the Line of Business. The possible values as per table 10.
	'**%    nProduct        - Code of the product.
	'**%    nLoad_cod       - Code of Current account Load
	'**%    dEffecdate      - Date which from the record is valid.
	'**%    nLoad_type      - Type of load  Sole values as per table 7996
	'**%    sDescript       - Complete name of the loading or charge.
	'**%    nLoadAmo        - Amount of administrative expenses to be charged to the insured
	'**%    nloadRate       - Percentage of administrative expenses to be charged to the insured
	'**%    sStatregt       - General status of the record. Sole values as per table 26.
	'**%    sShort_des      - Abbreviated description of the charge
	'**%    nPayFreq        - Payment frecuency of the premium. Sole values as per table 36.
	'**%    sRoutine        - Code of the routine to be used to calculate the cost
	'**%    sPreInv         - Indicates the charge has to be applied previous to the investment. Sole values:    1 - Affirmative    2 - Negative.
	'**%    nType_move      - Type of current account movement Sole values as per table 401
	'%Objetivo: Añade una nueva instancia de la clase Life_load a la colección
	'%Parámetros:
	'%      nStatusInstance -
	'%      nBranch         - Código del ramo comercial. Valores posibles según tabla 10.
	'%      nProduct        - Código del producto.
	'%      nLoad_cod       - Código del cargo a cuenta corriente
	'%      dEffecdate      - Fecha de efecto del registro.
	'%      nLoad_type      - Tipo de cargo  Valores únios según tabla 7996
	'%      sDescript       - Descripción completa del cargo
	'%      nLoadAmo        - Importe de gastos administrativos a cargo del asegurado
	'%      nloadRate       - Porcentaje de gastos administrativos a cargo del asegurado
	'%      sStatregt       - Estado general del registro. Valores únicos según tabla 26.
	'%      sShort_des      - Descripción abreviada del cargo
	'%      nPayFreq        - Frecuencia de pago de la prima. Valores únicos según tabla 36.
	'%      sRoutine        - Rutina de cálculo del cargo
	'%      sPreInv         - Indicador de cargo a ser aplicado previo a realizar la inversión. Valores únicos:    1 - Afirmativo    2 - Negativo
	'%      nType_move      - Tipo de movimiento de cuenta corriente Valores únicos según tabla 401
    Public Function Add(ByRef nStatusInstance As Integer, ByRef nBranch As Integer, ByRef nProduct As Integer, ByRef nLoad_cod As Integer, ByRef dEffecdate As Date, ByRef nLoad_type As Integer, ByRef sDescript As String, ByRef nLoadAmo As Double, ByRef nloadRate As Double, ByRef sStatregt As String, ByRef sShort_des As String, ByRef nPayFreq As Integer, ByRef sRoutine As String, ByRef sPreInv As String, ByRef nType_Move As Integer, ByRef sFirst_cost_pro As String, ByRef nModulec As Integer, ByRef nCover As Integer, ByRef sTaxin As String, Optional ByRef sAddTaxin As String = "", Optional ByRef nMonthi As Integer = 0, Optional ByRef nMonthe As Integer = 0, Optional ByRef nAply As Integer = 0, Optional ByRef nOriAply As Integer = 0, Optional ByRef sRetro As String = "", Optional ByRef sPremBas As String = "", Optional ByRef nFunds As Integer = 0, Optional ByRef sInstallind As String = "", Optional ByRef sFirst_apply As String = "", Optional ByRef nIndex_table As Integer = 0, Optional ByRef nMinimumAmount As Integer = 0, Optional ByRef nMaximumAmount As Integer = 0) As Life_load
        'create a new object
        Dim objNewMember As Life_load

        objNewMember = New Life_load
        With objNewMember
            .nStatusInstance = nStatusInstance
            .nBranch = nBranch
            .nProduct = nProduct
            .nLoad_cod = nLoad_cod
            .dEffecdate = dEffecdate
            .nLoad_type = nLoad_type
            .sDescript = sDescript
            .nLoadAmo = nLoadAmo
            .nloadRate = nloadRate
            .sStatregt = sStatregt
            .sShort_des = sShort_des
            .nPayFreq = nPayFreq
            .sRoutine = sRoutine
            .sPreInv = sPreInv
            .nType_Move = nType_Move
            .sFirst_cost_pro = IIf(sFirst_cost_pro = "", "2", sFirst_cost_pro)
            .nModulec = nModulec
            .nCover = nCover
            .sTaxin = IIf(sTaxin = "", "2", sTaxin)
            .sAddTaxin = IIf(sAddTaxin = "", "2", sAddTaxin)
            .nMonthi = nMonthi
            .nMonthe = nMonthe
            .nAply = nAply
            .nOriAply = nOriAply
            .sRetro = IIf(sRetro = "", "2", sRetro)
            .sPremBas = IIf(sPremBas = "", "2", sPremBas)
            .sInstallind = IIf(sInstallind = "", "2", sInstallind)
            .sFirst_apply = IIf(sFirst_apply = "", "2", sFirst_apply)
            .nIndex_table = nIndex_table
            .nFunds = nFunds
            .nMinimumAmount = nMinimumAmount
            .nMaximumAmount = nMaximumAmount
        End With

        mCol.Add(objNewMember)

        'return the object created
        Add = objNewMember
        'UPGRADE_NOTE: Object objNewMember may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        objNewMember = Nothing
    End Function
	
	'**%Objective: return an objects collection Life_load type
	'**%Parameters:
	'**%    nBranch1    -
	'**%    nProduct1   -
	'**%    dEffecDate1 -
	'**%    lblnFind    -
	'%Objetivo: Devuelve una coleccion de objetos de tipo Life_load
	'%Parámetros:
	'%      nBranch1    -
	'%      nProduct1   -
	'%      dEffecDate1 -
	'%      lblnFind    -
	Public Function Find(ByVal nBranch1 As Integer, ByVal nProduct1 As Integer, ByVal dEffecDate1 As Date, Optional ByVal lblnFind As Boolean = False) As Boolean
		
		On Error GoTo ErrorHandler
		Dim lrecLife_load As eRemoteDB.Execute
		Dim lclsLife_cover As Life_cover
		If nBranch1 = nAuxBranch And nProduct1 = nAuxProduct And dEffecDate1 = dAuxEffecdate And Not lblnFind Then
			Find = True
		Else
			'**- Define the variable lrecLife_load that will be use as a cursor.
			'- Se define la variable lrecLife_load que se utilizará como cursor.
			
			lrecLife_load = New eRemoteDB.Execute
			lclsLife_cover = New Life_cover
			
			'**+Execute the store procedure that search the intermediary movements
			'+ Se ejecuta el store procedure que busca los movimientos de un intermediario
			
			With lrecLife_load
				.StoredProcedure = "reaLife_load"
				.Parameters.Add("nBranch", nBranch1, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nProduct", nProduct1, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
				.Parameters.Add("nLoad_cod", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
				.Parameters.Add("nMonthi", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("dEffecdate", dEffecDate1, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
				.Parameters.Add("sStatregt", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				
				If Not .Run Then
					Find = False
					nAuxBranch = eRemoteDB.Constants.intNull
					nAuxProduct = eRemoteDB.Constants.intNull
					nAuxLoad_cod = eRemoteDB.Constants.intNull
					dAuxEffecdate = eRemoteDB.Constants.dtmNull
				Else
					nAuxBranch = nBranch1
					nAuxProduct = nProduct1
					dAuxEffecdate = dEffecDate1
					Find = True
					
					'+ [APV2] HAD 1021 – Cambios en la lógica de descuento de los costos coberturas. DBLANCO 03-09-2003
					Do While Not .EOF
						
						'+ + [APV2] HAD 1023. DP064 - CARGOS
						'+ Si el tipo de cargo es "Costo cobertura" el sistema busca el valor del
						'+ indicador "suma para impuesto" asociado a la cobertura
						If .FieldToClass("nLoad_type") = 1 Then
							Call lclsLife_cover.Find(nBranch1, nProduct1, .FieldToClass("nModulec"), .FieldToClass("nCover"), dEffecDate1)
						End If
						
                        Call Add(eRemoteDB.Constants.intNull, .FieldToClass("nBranch"), .FieldToClass("nProduct"), .FieldToClass("nLoad_cod"), .FieldToClass("dEffecdate"), .FieldToClass("nLoad_type"), .FieldToClass("sDescript"), .FieldToClass("nLoadAmo"), .FieldToClass("nLoadRate"), .FieldToClass("sStatregt"), .FieldToClass("sShort_des"), .FieldToClass("nPayFreq"), .FieldToClass("sRoutine"), .FieldToClass("sPreInv"), .FieldToClass("nType_move"), .FieldToClass("sFirst_cost_pro"), .FieldToClass("nModulec"), .FieldToClass("nCover"), .FieldToClass("sTaxin"), lclsLife_cover.sAddTaxin, .FieldToClass("nMonthi"), .FieldToClass("nMonthe"), .FieldToClass("nAply"), .FieldToClass("nOriAply"), .FieldToClass("sRetro"), .FieldToClass("sPremBas"), .FieldToClass("nFunds"), .FieldToClass("sInstallind"), .FieldToClass("sFirst_apply"), .FieldToClass("nIndex_table"), .FieldToClass("nLoadamo_min"), .FieldToClass("nLoadamo_Max"))
						
						.RNext()
					Loop 
				End If
			End With
		End If
		'UPGRADE_NOTE: Object lrecLife_load may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecLife_load = Nothing
		'UPGRADE_NOTE: Object lclsLife_cover may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsLife_cover = Nothing
		
		Exit Function
ErrorHandler: 
		'UPGRADE_NOTE: Object lrecLife_load may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecLife_load = Nothing
		'UPGRADE_NOTE: Object lclsLife_cover may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsLife_cover = Nothing
		Find = False
	End Function
	
	'**%Objective:
	'%Objetivo:
	Public Function Update() As Boolean
		Dim lclsLife_load As Life_load
		
		On Error GoTo ErrorHandler
		For	Each lclsLife_load In mCol
			Select Case lclsLife_load.nStatusInstance
				
				'**+Add
				'+Agregar
				
				Case 1
					Update = lclsLife_load.Update()
					'**+Update
					'+Actualizar
					
				Case 2
					Update = lclsLife_load.Update()
					'**+Delete
					'+ Eliminar
					
				Case 3
					Update = lclsLife_load.Delete()
			End Select
			If Update = False Then
				Exit For
			End If
		Next lclsLife_load
		
		Exit Function
ErrorHandler: 
		Update = False
	End Function
	
	'**%Objective:
	'**%Parameters:
	'**%    vntIndexKey -
	'%Objetivo:
	'%Parámetros:
	'%      vntIndexKey -
	Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As Life_load
		Get
			On Error GoTo ErrorHandler
			Item = mCol.Item(vntIndexKey)
			
			Exit Property
ErrorHandler: 
			'UPGRADE_NOTE: Object Item may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
			Item = Nothing
		End Get
	End Property
	
	'**%Objective:
	'%Objetivo:
	Public ReadOnly Property Count() As Integer
		Get
			On Error GoTo ErrorHandler
			Count = mCol.Count()
			
			Exit Property
ErrorHandler: 
			Count = 0
		End Get
	End Property
	
	'**%Objective:
	'%Objetivo:
	'UPGRADE_NOTE: NewEnum property was commented out. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="B3FC1610-34F3-43F5-86B7-16C984F0E88E"'
	'Public ReadOnly Property NewEnum() As stdole.IUnknown
		'Get
			'NewEnum = mCol._NewEnum
		'End Get
	'End Property
	
	Public Function GetEnumerator() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
		'UPGRADE_TODO: Uncomment and change the following line to return the collection enumerator. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="95F9AAD0-1319-4921-95F0-B9D3C4FF7F1C"'
		GetEnumerator = mCol.GetEnumerator
	End Function
	
	'**%Objective: delete one element from the collection
	'**%Parameters:
	'**%    vntIndexKey -
	'%Objetivo: Elimina un elemento de la colección
	'%Parámetros:
	'%      vntIndexKey -
	Public Sub Remove(ByRef vntIndexKey As Object)
		mCol.Remove(vntIndexKey)
	End Sub
	
	'**%Objective: Control the creation of one collection instance
	'%Objetivo: Controla la creación de una instancia de la colección
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		nAuxBranch = eRemoteDB.Constants.intNull
		nAuxProduct = eRemoteDB.Constants.intNull
		nAuxLoad_cod = eRemoteDB.Constants.intNull
		dAuxEffecdate = eRemoteDB.Constants.dtmNull
		
		mCol = New Collection
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
	
	'**%Objective: Control the deletes of one collection instance
	'%Objetivo: Controla la destrucción de una instancia de la colección
	'UPGRADE_NOTE: Class_Terminate was upgraded to Class_Terminate_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Terminate_Renamed()
		'UPGRADE_NOTE: Object mCol may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		mCol = Nothing
	End Sub
	Protected Overrides Sub Finalize()
		Class_Terminate_Renamed()
		MyBase.Finalize()
	End Sub
End Class






