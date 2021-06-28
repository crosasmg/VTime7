Option Strict Off
Option Explicit On
Public Class Mortalitys
	Implements System.Collections.IEnumerable
	'%-------------------------------------------------------%'
	'% $Workfile:: Mortalitys.cls                           $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 12:35p                               $%'
	'% $Revision:: 12                                       $%'
	'%-------------------------------------------------------%'
	
	Public nInit_age As Integer
	Public nEnd_age As Integer
	Public nLive_lx As Double
	
	'**-Local variable to contein the collection
	'- Variable local para contener colección
	Private mCol As Collection
	
	'**-Defines the auxiliary property of the transaction DP013 - Parameters for the mortality table.
	'- Se definen las propiedades auxiliares de la transacción DP013 - Parámetros para la tabla de mortalidad.
	Private mstrMortalco As String
	Private mintAge As Integer
	
	'% Add: se agrega un elemento a la colección
	Public Function Add(ByRef oMortality As Mortality) As Mortality
		mCol.Add(oMortality)
		Add = oMortality
		'UPGRADE_NOTE: Object oMortality may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		oMortality = Nothing
	End Function
	
	'**% Item: restores one element of the collection (accourding to the index)
	'% Item: Devuelve un elemento de la colección (segun índice)
	Public ReadOnly Property Item(ByVal sMortalco As String, ByVal nAge As Integer) As Mortality
		Get
			Item = mCol.Item("A" & sMortalco & CStr(nAge))
		End Get
	End Property
	
	'**% Count: reatores the number of elements that the collection owns
	'% Count: Devuelve el número de elementos que posee la colección
	Public ReadOnly Property Count() As Integer
		Get
			Count = mCol.Count()
		End Get
	End Property
	
	'**% NewEnum: Allows to enumerate the collection for using it in a cycle For Each... Next
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
	
	'**% Remove: deletes one element of the collection
	'% Remove: Elimina un elemento de la colección
	Public Sub Remove(ByRef vntIndexKey As Object)
		mCol.Remove(vntIndexKey)
	End Sub
	
	'**% Class_Initialize: Controls the creation of an instance of the collection
	'% Class_Initialize: Controla la creación de una instancia de la colección
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		nInit_age = eRemoteDB.Constants.intNull
		nEnd_age = eRemoteDB.Constants.intNull
		nLive_lx = eRemoteDB.Constants.intNull
		mCol = New Collection
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
	
	'**% Class_Terminate: Controls the delete of one instance of the collection
	'% Class_Terminate: Controla la destrucción de una instancia de la colección
	'UPGRADE_NOTE: Class_Terminate was upgraded to Class_Terminate_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Terminate_Renamed()
		'UPGRADE_NOTE: Object mCol may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		mCol = Nothing
	End Sub
	Protected Overrides Sub Finalize()
		Class_Terminate_Renamed()
		MyBase.Finalize()
	End Sub
	
	'**% Find: Makes the reading of the parameters for the Mortality Table - DP013
	'% Find: Permite realizar la lectura de los parámetros para la Tabla de Mortalidad - DP013.
	Public Function Find(ByVal sMortalco As String, ByVal nAge As Integer, Optional ByVal lblnFind As Boolean = True) As Boolean
		Dim lclsMortality As eProduct.Mortality
		Dim lrecMortality As eRemoteDB.Execute
		
		On Error GoTo Find_Err
		
		lrecMortality = New eRemoteDB.Execute
		
		Find = True
		
		If sMortalco <> mstrMortalco Or nAge <> mintAge Or lblnFind Then
			'UPGRADE_NOTE: Object mCol may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
			mCol = Nothing
			mCol = New Collection
			
			'**+ Parameters definition for the stored procedure 'insudb.reaAllCovergen'.
			'+ Definición de parámetros para stored procedure 'insudb.reaAllCovergen'.
			With lrecMortality
				.StoredProcedure = "reaMortality"
				.Parameters.Add("sMortalco", sMortalco, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 6, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
				'UPGRADE_WARNING: IsEmpty was upgraded to IsNothing and has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
				.Parameters.Add("nAge", IIf(IsNothing(nAge) Or nAge = 0, System.DBNull.Value, nAge), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nMonth", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				If .Run Then
					mstrMortalco = sMortalco
					mintAge = nAge
					nInit_age = .FieldToClass("nAge")
					nLive_lx = .FieldToClass("nLive_lx")
					
					Do While Not .EOF
						lclsMortality = New eProduct.Mortality
						With lclsMortality
							.nStatusInstance = 0
							.sMortalco = lrecMortality.FieldToClass("sMortalco")
							.nAge = lrecMortality.FieldToClass("nAge")
							.nDeath_qx = lrecMortality.FieldToClass("nDeath_qx")
							.nLive_lx = lrecMortality.FieldToClass("nLive_lx")
							.nUsercode = eRemoteDB.Constants.intNull
							.nDeath_dx = lrecMortality.FieldToClass("nDeath_dx")
							.sInsert = String.Empty
							.sUpdateF = String.Empty
							.nExist = 1
							.nMonth = lrecMortality.FieldToClass("nMonth")
							
							Call Add(lclsMortality)
						End With
						nEnd_age = .FieldToClass("nAge")
						'UPGRADE_NOTE: Object lclsMortality may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
						lclsMortality = Nothing
						.RNext()
					Loop 
					.RCloseRec()
				Else
					Find = False
					nInit_age = eRemoteDB.Constants.intNull
					nLive_lx = eRemoteDB.Constants.intNull
					nEnd_age = eRemoteDB.Constants.intNull
					mstrMortalco = CStr(Nothing)
					mintAge = 0
				End If
			End With
		End If
		
Find_Err: 
		If Err.Number Then
			Find = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecMortality may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecMortality = Nothing
		'UPGRADE_NOTE: Object lclsMortality may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsMortality = Nothing
	End Function
	
	'**% Update: Makes the treatment of each instance of the class in the collection.
	'% Update: Realiza el tratamiento de cada instancia de la clase en la colección.
	Public Function Update() As Boolean
		Dim lclsMortality As eProduct.Mortality
		Dim lcolAux As Collection
		
		On Error GoTo Update_Err
		
		Update = True
		
		lcolAux = New Collection
		
		For	Each lclsMortality In mCol
			With lclsMortality
				Select Case .nStatusInstance
					
					'**+ If the action is Add.
					'+ Si la acción es Agregar.
					Case 1
						Update = .Add()
						
						'**+ If the action is Update
						'+ Si la acción es Actualizar.
					Case 2
						Update = .Update()
				End Select
				
				If .nStatusInstance <> 2 Then
					If Update Then
						.nStatusInstance = 0
					End If
					
					lcolAux.Add(lclsMortality)
				End If
			End With
		Next lclsMortality
		
		mCol = lcolAux
		
Update_Err: 
		If Err.Number Then
			Update = False
		End If
		'UPGRADE_NOTE: Object lclsMortality may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsMortality = Nothing
		'UPGRADE_NOTE: Object lcolAux may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lcolAux = Nothing
		On Error GoTo 0
	End Function
	
	'**% FindReload: Performs the reading of the parameters for the Mortality Table - DP013.
	'% FindReload: Permite realizar la lectura de los parámetros para la Tabla de Mortalidad - DP013.
	Public Function FindReload(ByVal sMortalco As String, ByVal nInit_age As Integer, ByVal nEnd_age As Integer, ByVal nInit_ageOld As Integer, ByVal nEnd_ageOld As Integer, ByVal nUsercode As Integer, ByVal nLive_lx As Double) As Boolean
		Dim llngAge As Integer
		Dim llngIndex As Integer
		Dim lblnFind As Boolean
		Dim lclsMortality As eProduct.Mortality
		
		lclsMortality = New eProduct.Mortality
		
		On Error GoTo FindReload_Err
		
		FindReload = lclsMortality.Update_Live(sMortalco, nLive_lx, nUsercode)
		
		Call Find(sMortalco, 0, True)
		
		'**+ Establishes the initial age in zero if it´s without this parameter
		'+ Establece la edad inicial en cero, en caso de prescidir de este parámetro
		If nInit_age < 0 Then
			nInit_age = 0
		End If
		
		If nInit_age > nInit_ageOld Or nEnd_age < nEnd_ageOld Then
			Call lclsMortality.Delete(sMortalco, nInit_age, nEnd_age)
		End If
		
		For llngAge = nInit_age To nEnd_age
			lblnFind = False
			
			For llngIndex = 1 To mCol.Count()
				If mCol.Item(llngIndex).nAge = llngAge Then
					lblnFind = True
					Exit For
				End If
			Next llngIndex
			
			If Not lblnFind Then
				lclsMortality = New eProduct.Mortality
				With lclsMortality
					.nStatusInstance = 1
					.sMortalco = sMortalco
					.nAge = llngAge
					.nDeath_qx = 0
					.nLive_lx = eRemoteDB.Constants.intNull
					.nUsercode = nUsercode
					.nDeath_dx = eRemoteDB.Constants.intNull
					.sInsert = String.Empty
					.sUpdateF = String.Empty
					.nExist = 1
					
					lclsMortality.Add()
				End With
				'UPGRADE_NOTE: Object lclsMortality may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
				lclsMortality = Nothing
			End If
		Next llngAge
		
		Call Find(sMortalco, 0, True)
		
		If mCol.Count() = 0 Then
			FindReload = False
		End If
		
		'UPGRADE_NOTE: Object lclsMortality may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsMortality = Nothing
		
FindReload_Err: 
		If Err.Number Then
			FindReload = False
		End If
		On Error GoTo 0
	End Function
End Class






