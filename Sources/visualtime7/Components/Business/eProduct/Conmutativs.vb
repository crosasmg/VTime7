Option Strict Off
Option Explicit On
Public Class Conmutativs
	Implements System.Collections.IEnumerable
	'%-------------------------------------------------------%'
	'% $Workfile:: Conmutativs.cls                          $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 12:35p                               $%'
	'% $Revision:: 15                                       $%'
	'%-------------------------------------------------------%'
	
	Public sMortalco As String
	Public mdblInt As Double
	Public nusercode As Integer
	
	'- Local variable to hold collection
	Private mCol As Collection
	
	'**- Define the auxiliary property of the DP015 transaction - Conmutatives values generation and the DP016-
	'**- Modification of the conmutatives values.
	'- Se definen las propiedades auxiliares de la transacción DP015 - Generación de valores conmutativos y
	'- la DP016 - Modificación de los valores conmutativos.
	Private mstrMortalco As String
	Private mdblInterest As Double
	
	Private lclsConmutativ As eProduct.Conmutativ = New eProduct.Conmutativ
	
	'**% Add: adds a new instance of the "Conmutativ" class to the collection
	'% Add: Añade una nueva instancia de la clase "Conmutativ" a la colección
	Public Function Add(ByRef bytAge As Byte, ByRef nMonth As Integer, ByRef npx As Double, ByRef nqx As Double, ByRef nlx As Double, ByRef ndx As Double) As Conmutativ
		Dim objNewMember As Conmutativ
		
		objNewMember = New Conmutativ
		
		If mCol Is Nothing Then
			mCol = New Collection
		End If
		
		With objNewMember
			.bytAge = bytAge
			.nMonth = nMonth
			.npx = npx
			.nqx = nqx
			.nlx = nlx
			.ndx = ndx
		End With
		
		mCol.Add(objNewMember)
		
		Add = objNewMember
		'UPGRADE_NOTE: Object objNewMember may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		objNewMember = Nothing
	End Function
	
	'**% AddConmutativ: This method allows to add records to the collection.
	'% AddConmutativ: Este método permite añadir registros a la colección.
	Public Function AddConmutativ(ByRef sMortalco As String, ByRef nInterest As Double, ByRef nAge As Integer, ByRef nMonth As Integer, ByRef nConmu_cx As Double, ByRef nConmu_dx As Double, ByRef nConmu_mx As Double, ByRef nConmu_nx As Double, ByRef nConmu_rx As Double, ByRef nConmu_sx As Double, ByRef nConmu_tx As Double, ByRef nDeath_dx As Double, ByRef nLive_lx As Double, ByRef nDeath_qx As Double, ByRef nLiver_px As Double, ByRef nConmu_vx As Double, ByRef nConmu_ex As Double) As Conmutativ
		Dim objNewMember As eProduct.Conmutativ
		objNewMember = New eProduct.Conmutativ
		
		If mCol Is Nothing Then
			mCol = New Collection
		End If
		
		With objNewMember
			.sMortalco = sMortalco
			.nInterest = nInterest
			.nAge = nAge
			.nMonth = nMonth
			.nConmu_cx = nConmu_cx
			.nConmu_dx = nConmu_dx
			.nConmu_mx = nConmu_mx
			.nConmu_nx = nConmu_nx
			.nConmu_rx = nConmu_rx
			.nConmu_sx = nConmu_sx
			.nConmu_tx = nConmu_tx
			.nDeath_dx = nDeath_dx
			.nLive_lx = nLive_lx
			.nDeath_qx = nDeath_qx
			.nLiver_px = nLiver_px
			.nConmu_vx = nConmu_vx
			.nConmu_ex = nConmu_ex
		End With
		
		mCol.Add(objNewMember, "A" & sMortalco & "-" & nInterest & "-" & nAge & "-" & nMonth)
		
		AddConmutativ = objNewMember
		'UPGRADE_NOTE: Object objNewMember may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		objNewMember = Nothing
	End Function
	
	'**% Item: Returns an element of the collection (according to the index)
	'% Item: Devuelve un elemento de la colección (segun índice)
	Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As Conmutativ
		Get
			Item = mCol.Item(vntIndexKey)
		End Get
	End Property
	
	'**% Count: Returns the number of elements that the collection has
	'% Count: Devuelve el número de elementos que posee la colección
	Public ReadOnly Property Count() As Integer
		Get
			Count = mCol.Count()
		End Get
	End Property
	
	'**% NewEnum: Enumerates the collection for use in a For Each...Next loop
	'% NewEnum: Permite enumerar la colección para utilizarla en un ciclo For Each... Next
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
	
	'**% Remove: Deletes an element from the collection
	'% Remove: Elimina un elemento de la colección
	Public Sub Remove(ByRef vntIndexKey As Object)
		mCol.Remove(vntIndexKey)
	End Sub
	
	'**% Class_Initialize: Controls the creation of an instance of the collection
	'% Class_Initialize: Controla la creación de una instancia de la colección
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		mCol = New Collection
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
	
	'**% Class_Terminate: Controls the destruction of an instance of the collection
	'% Class_Terminate: Controla la destrucción de una instancia de la colección
	'UPGRADE_NOTE: Class_Terminate was upgraded to Class_Terminate_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Terminate_Renamed()
		'UPGRADE_NOTE: Object mCol may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		mCol = Nothing
		'UPGRADE_NOTE: Object lclsConmutativ may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsConmutativ = Nothing
	End Sub
	Protected Overrides Sub Finalize()
		Class_Terminate_Renamed()
		MyBase.Finalize()
	End Sub
	
	'**% Find: Verify that the mortality table, indicated by the user, exists and loads
	'**% the values for each age of the collection.
	'% Find: Verifica que exista la tabla de mortalidad indicada por el usuario y carga
	'% los valores para cada edad en la colección.
	Public Function Find(ByVal sMortalco As String, Optional ByVal lblnFind As Boolean = True) As Boolean
		Dim lrecConmutativs As eRemoteDB.Execute
		
		lrecConmutativs = New eRemoteDB.Execute
		
		On Error GoTo Find_Err
		
		Find = True
		
		If sMortalco <> mstrMortalco Or lblnFind Then
			'UPGRADE_NOTE: Object mCol may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
			mCol = Nothing
			mCol = New Collection
			
			'**+ Parameter definition for stored procedure 'insudb.reaMortality'
			'+ Definición de parámetros para stored procedure 'insudb.reaMortality'.
			With lrecConmutativs
				.StoredProcedure = "reaMortality"
				
				.Parameters.Add("sMortalco", sMortalco, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 6, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
				.Parameters.Add("nAge", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
				.Parameters.Add("nMonth", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				
				If .Run Then
					mstrMortalco = sMortalco
					
					'**+ Charge in the collection the Age (nAge), Probability of life (1 - nDeath_qx), Probability of death
					'**+ (nDeath_qx), Alive at the age X (nLive_lx), Dead at the age X (nDeath_dx)
					'+ Se cargan en la colección la Edad (nAge), Probabilidad de vida (1 - nDeath_qx),
					'+ Probabilidad de morir (nDeath_qx), Vivos a la edad x (nLive_lx), Muertos a la edad x (nDeath_dx).
					Do While Not .EOF
						Call Add(.FieldToClass("nAge"), .FieldToClass("nMonth"), 1 - .FieldToClass("nDeath_qx"), .FieldToClass("nDeath_qx"), .FieldToClass("nLive_lx"), .FieldToClass("nDeath_dx"))
						
						
						.RNext()
					Loop 
					
					.RCloseRec()
				Else
					Find = False
					
					mstrMortalco = CStr(Nothing)
				End If
			End With
			
			'UPGRADE_NOTE: Object lrecConmutativs may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
			lrecConmutativs = Nothing
		End If
		
Find_Err: 
		If Err.Number Then
			Find = False
		End If
		On Error GoTo 0
	End Function
	
	'**% insCalConmutativ: Calculates the conmutatives values foe each age.
	'% insCalConmutativ: Calcula los valores conmutativos para cada edad.
	Public Function insCalConmutativ() As Boolean
		Dim lclsMortality As eProduct.Mortality
		Dim ldblInt As Double
		
		lclsMortality = New eProduct.Mortality
		
		insCalConmutativ = True
		
		ldblInt = mdblInt
		
		'**+ Take the interest to a value between zero and one for calculation effects.
		'+ Se lleva el interés a un valor entre cero y uno para efectos del cálculo.
		mdblInt = mdblInt / 100
		
		'**+ Calculate the conmutative D
		'+ Se calcula el conmutativo D.
		Call inscalConm_D()
		
		'**+ Calculate the conmutative C
		'+ Se calcula el conmutativo C.
		Call inscalConm_C()
		
		'**+ Calculate the conmutative N
		'+ Se calcula el conmutativo N.
		Call insCalConm_N(0)
		
		'**+ Calculate the conmutative M
		'+ Se calcula el conmutativo M.
		Call insCalConm_M(0)
		
		'**+ Calculate the conmutative S
		'+ Se calcula el conmutativo S.
		Call insCalConm_S(0)
		
		'**+ Calculate the conmutative R
		'+ Se calcula el conmutativo R.
		Call insCalConm_R(0)
		
		'**+ Calculate the conmutative T
		'+ Se calcula el conmutativo T.
		Call insCalConm_T(0)
		
		'+ Se calcula el conmutativo V.
		Call inscalConm_V()
		
		'+ Se calcula el conmutativo E.
		Call inscalConm_E()
		
		'**+ Take the interest to a value between zero and one hundred.
		'+ Se lleva el interés a un valor entre cero y cien.
		mdblInt = ldblInt
		
		lclsConmutativ.sMortalco = sMortalco
		lclsConmutativ.mdblInt = mdblInt
		lclsConmutativ.nusercode = nusercode
		
		If insCalConmutativ Then
			If lclsMortality.insReaConm_master(sMortalco, mdblInt) Then
				If lclsConmutativ.DeleteConmutativ Then
					For	Each lclsConmutativ In mCol
						lclsConmutativ.sMortalco = sMortalco
						lclsConmutativ.mdblInt = mdblInt
						lclsConmutativ.nusercode = nusercode
						
						If lclsConmutativ.Add Then
						End If
					Next lclsConmutativ
				End If
			Else
				If lclsConmutativ.AddConm_master Then
					For	Each lclsConmutativ In mCol
						lclsConmutativ.sMortalco = sMortalco
						lclsConmutativ.mdblInt = mdblInt
						lclsConmutativ.nusercode = nusercode
						
						If lclsConmutativ.Add Then
						End If
					Next lclsConmutativ
				End If
			End If
		End If
		'UPGRADE_NOTE: Object lclsMortality may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsMortality = Nothing
		
	End Function
	
	'**% inscalConm_D: Calculates the conmutative D.
	'% inscalConm_D: Calcula el conmutativo D.
	Private Function inscalConm_D() As Boolean
		For	Each lclsConmutativ In mCol
			lclsConmutativ.nConm_D = lclsConmutativ.nlx / (1 + mdblInt) ^ lclsConmutativ.bytAge
		Next lclsConmutativ
		
		inscalConm_D = True
	End Function
	
	'**% inscalConm_C: Calculates the conmutative C.
	'% inscalConm_C: Calcula el conmutativo C.
	Private Function inscalConm_C() As Boolean
		For	Each lclsConmutativ In mCol
			lclsConmutativ.nConm_C = lclsConmutativ.ndx / (1 + mdblInt) ^ (lclsConmutativ.bytAge + 0.5)
		Next lclsConmutativ
		
		inscalConm_C = True
	End Function
	
	'**% inscalConm_V: Calculates the conmutative V.
	'% inscalConm_V: Calcula el conmutativo V = ((1/(1+i)^x)) donde i es la tasa de interés y x es la edad
	Private Function inscalConm_V() As Boolean
		For	Each lclsConmutativ In mCol
			lclsConmutativ.nConm_V = ((1 / (1 + mdblInt) ^ lclsConmutativ.bytAge))
		Next lclsConmutativ
		
		inscalConm_V = True
	End Function
	
	'**% inscalConm_E: Calculates the conmutative E.
	'% inscalConm_E: Calcula el conmutativo E = lx/l0 * vx  donde lx son los sobrevivientes a la edad x, l0 los sobrevivientes iniciales (inicio de tabla)
	Private Function inscalConm_E() As Boolean
		Dim nLxIni As Double
		
		nLxIni = Item(1).nlx
		
		For	Each lclsConmutativ In mCol
			lclsConmutativ.nConm_E = lclsConmutativ.nlx / nLxIni * lclsConmutativ.nConm_V
		Next lclsConmutativ
		
		inscalConm_E = True
	End Function
	
	'**% inscalConm_N: Calculates the conmutative N.
	'% inscalConm_N: Calcula el conmutativo N.
	Private Function insCalConm_N(ByRef lbytIndex As Integer) As Double
		lclsConmutativ = Item(lbytIndex + 1)
		
		With lclsConmutativ
			If (lbytIndex + 1) = mCol.Count() Then
				.nConm_N = CDbl(.nConm_D)
			Else
				.nConm_N = .nConm_D + insCalConm_N(lbytIndex + 1)
			End If
			
			insCalConm_N = .nConm_N
		End With
	End Function
	
	'**% inscalConm_M: Calculates the conmutative M.
	'% inscalConm_M: Calcula el conmutativo M.
	Private Function insCalConm_M(ByRef lbytIndex As Integer) As Double
		lclsConmutativ = Item(lbytIndex + 1)
		
		With lclsConmutativ
			If (lbytIndex + 1) = mCol.Count() Then
				.nConm_M = CDbl(.nConm_C)
			Else
				.nConm_M = .nConm_C + insCalConm_M(lbytIndex + 1)
			End If
			
			insCalConm_M = .nConm_M
		End With
	End Function
	
	'**% inscalConm_S: Calculates the conmutative S.
	'% inscalConm_S: Calcula el conmutativo S.
	Private Function insCalConm_S(ByRef lbytIndex As Integer) As Double
		lclsConmutativ = Item(lbytIndex + 1)
		
		With lclsConmutativ
			If (lbytIndex + 1) = mCol.Count() Then
				.nConm_S = CDbl(.nConm_N)
			Else
				.nConm_S = .nConm_N + insCalConm_S(lbytIndex + 1)
			End If
			
			insCalConm_S = .nConm_S
		End With
	End Function
	
	'**% inscalConm_R: Calculates the conmutative R.
	'% inscalConm_R: Calcula el conmutativo R.
	Private Function insCalConm_R(ByRef lbytIndex As Integer) As Double
		lclsConmutativ = Item(lbytIndex + 1)
		
		With lclsConmutativ
			If (lbytIndex + 1) = mCol.Count() Then
				.nConm_R = CDbl(.nConm_M)
			Else
				.nConm_R = .nConm_M + insCalConm_R(lbytIndex + 1)
			End If
			
			insCalConm_R = .nConm_R
		End With
	End Function
	
	'**% inscalConm_T: Calculates the conmutative T.
	'% inscalConm_T: Calcula el conmutativo T.
	Private Function insCalConm_T(ByRef lbytIndex As Integer) As Double
		lclsConmutativ = Item(lbytIndex + 1)
		
		With lclsConmutativ
			If (lbytIndex + 1) = mCol.Count() Then
				.nConm_T = CDbl(.nConm_R)
			Else
				.nConm_T = .nConm_R + insCalConm_T(lbytIndex + 1)
			End If
			
			insCalConm_T = .nConm_T
		End With
	End Function
	
	'**% FindConmutativ: Verify that there is information in the conmutatives table.
	'% FindConmutativ: Verifica que exista información en la tabla de conmutativos.
	Public Function FindConmutativ(ByVal sMortalco As String, ByVal nInterest As Double, Optional ByVal lblnFind As Boolean = True) As Boolean
		Dim lrecConmutativ As eRemoteDB.Execute
		
		lrecConmutativ = New eRemoteDB.Execute
		
		On Error GoTo FindConmutativ_Err
		
		FindConmutativ = True
		
		If sMortalco <> mstrMortalco Or nInterest <> mdblInterest Or lblnFind Then
			'UPGRADE_NOTE: Object mCol may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
			mCol = Nothing
			mCol = New Collection
			
			'**+ Parameter definition for stored procedure 'insudb.reaConmutativ'.
			'+ Definición de parámetros para stored procedure 'insudb.reaConmutativ'.
			With lrecConmutativ
				.StoredProcedure = "reaConmutativ"
				
				.Parameters.Add("sMortalco", sMortalco, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 6, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nInterest", nInterest, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 4, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
				.Parameters.Add("nAge", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
				.Parameters.Add("nMonth", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				
				If .Run Then
					mstrMortalco = sMortalco
					mdblInterest = nInterest
					
					Do While Not .EOF
						Call AddConmutativ(sMortalco, nInterest, .FieldToClass("nAge"), .FieldToClass("nMonth"), .FieldToClass("nConmu_cx"), .FieldToClass("nConmu_dx"), .FieldToClass("nConmu_mx"), .FieldToClass("nConmu_nx"), .FieldToClass("nConmu_rx"), .FieldToClass("nConmu_sx"), .FieldToClass("nConmu_tx"), .FieldToClass("nDeath_dx"), .FieldToClass("nLive_lx"), .FieldToClass("nDeath_qx"), .FieldToClass("nLiver_px"), .FieldToClass("nConmu_vx"), .FieldToClass("nConmu_ex"))
						.RNext()
					Loop 
					
					.RCloseRec()
				Else
					FindConmutativ = False
					
					mstrMortalco = CStr(Nothing)
					mdblInterest = 0
				End If
			End With
			
			'UPGRADE_NOTE: Object lrecConmutativ may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
			lrecConmutativ = Nothing
		End If
		
FindConmutativ_Err: 
		If Err.Number Then
			FindConmutativ = False
		End If
		On Error GoTo 0
	End Function
End Class






