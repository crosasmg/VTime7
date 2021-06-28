Option Strict Off
Option Explicit On
Public Class Tab_commissions
	Implements System.Collections.IEnumerable
	'%-------------------------------------------------------%'
	'% $Workfile:: Tab_commissions.cls                      $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 1:41p                                $%'
	'% $Revision:: 11                                       $%'
	'%-------------------------------------------------------%'
	
	Private mCol As Collection
	Private nAuxCommType As Integer
	
	'**% Add: Adds a new instance of the Tab_commission class  to the collection
	'% Add: Añade una nueva instancia de la clase Tab_commission a la colección
	Public Function Add(ByVal nStatusInstance As Integer, ByVal nTable_cod As Integer, ByVal sType_assig As String, ByVal sDescript As String, ByVal sShort_des As String, ByVal sStatregt As String) As Tab_Commission
		'create a new object
		
		Dim objNewMember As Tab_Commission
		objNewMember = New Tab_Commission
		
		With objNewMember
			.nStatusInstance = nStatusInstance
			.nTable_cod = nTable_cod
			.sType_assig = sType_assig
			.sDescript = sDescript
			.sShort_des = sShort_des
			.sStatregt = sStatregt
		End With
		
		mCol.Add(objNewMember)
		
		'return the object created
		Add = objNewMember
		'UPGRADE_NOTE: Object objNewMember may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		objNewMember = Nothing
		
	End Function
	
	'**% Find: Restores a collection of objects of Tab_commission type
	'% Find: Devuelve una coleccion de objetos de tipo Tab_commission
	'------------------------------------------------------------
	Public Function Find(ByVal nCommType As Integer, Optional ByVal lblnFind As Boolean = False) As Boolean
		'------------------------------------------------------------
		
		'**- Variable definition lrecTab_commission that will be used as a cursor.
		'- Se define la variable lrecTab_commission que se utilizará como cursor.
		Dim lrecTab_commission As eRemoteDB.Execute
		Dim lclsTab_Commission As eAgent.Tab_Commission
		
		Dim lintTable_cod As Integer
        Dim lstrType_assig As String = ""

        lrecTab_commission = New eRemoteDB.Execute
		lclsTab_Commission = New eAgent.Tab_Commission
		
		If nAuxCommType = nCommType And Not lblnFind Then
			Find = True
		Else
			
			'**+ Execute the store procedure that searches an intermediary's transactions
			'+ Se ejecuta el store procedure que busca los movimientos de un intermediario
			lclsTab_Commission.nCommType = nCommType
			With lrecTab_commission
				.StoredProcedure = lclsTab_Commission.ValCommType(Tab_Commission.eActions.Rea)
				If UCase(.StoredProcedure) = "REATAB_COMLIF_A" Then
					.Parameters.Add("nContabli", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				End If
				If Not .Run(True) Then
					Find = False
					nAuxCommType = eRemoteDB.Constants.intNull
				Else
					nAuxCommType = nCommType
					Find = True
					
					Do While Not .EOF
						If nCommType = 0 Then
							lintTable_cod = .FieldToClass("nComtabge")
							lstrType_assig = strNull
						Else
							If nCommType = 1 Then
								lintTable_cod = .FieldToClass("nComtabli")
								lstrType_assig = strNull
							Else
								If nCommType = 2 Then
									lintTable_cod = .FieldToClass("nEco_sche")
									lstrType_assig = strNull
								Else
									If nCommType = 3 Then
										lintTable_cod = .FieldToClass("nTable_cod")
										lstrType_assig = .FieldToClass("sType_assig")
									Else
										If nCommType = 4 Then
											lintTable_cod = .FieldToClass("nCode")
											lstrType_assig = strNull
										End If
									End If
								End If
							End If
						End If
						
						Call Add(eRemoteDB.Constants.intNull, lintTable_cod, lstrType_assig, .FieldToClass("sDescript"), .FieldToClass("sShort_des"), .FieldToClass("sStatregt"))
						
						.RNext()
					Loop 
				End If
			End With
		End If
		'UPGRADE_NOTE: Object lrecTab_commission may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecTab_commission = Nothing
		'UPGRADE_NOTE: Object lclsTab_Commission may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsTab_Commission = Nothing
	End Function
	
	'**% Update: This function calls the procedures to Add, Update, and Delete.
	'% Update: Esta función se encarga de llamar al procedimiento correspondiente
	'% a la acción que se esté realizando.
	Public Function Update(ByVal nCommType As Integer) As Boolean
		Dim lclsTab_Commission As Tab_Commission
		For	Each lclsTab_Commission In mCol
			lclsTab_Commission.nCommType = nCommType
			Select Case lclsTab_Commission.nStatusInstance
				
				'**+ Add
				'+Agregar
				
				Case 1
					Update = lclsTab_Commission.Add()
					'**+ Update
					'+Actualizar
					
				Case 2
					Update = lclsTab_Commission.Update()
					
					'**+ Delete
					'+Eliminar
					
				Case 3
					Update = lclsTab_Commission.Delete()
					
			End Select
			
			If Update = False Then
				Exit For
			End If
		Next lclsTab_Commission
	End Function
	
	'*** Item: Restores an element to the collection (accordin to the index)
	'* Item: Devuelve un elemento de la colección (segun índice)
	Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As Tab_Commission
		Get
			
			Item = mCol.Item(vntIndexKey)
		End Get
	End Property
	
	
	'*** Count: Restores the number of elements that the collection owns.
	'* Count: Devuelve el número de elementos que posee la colección
	Public ReadOnly Property Count() As Integer
		Get
			
			Count = mCol.Count()
		End Get
	End Property
	
	'**NewEnum: allows to enumerate the collection for using it in a Cycle For Each...Next
	'* NewEnum: Permite enumerar la colección para utilizarla en un ciclo For Each... Next
	'UPGRADE_NOTE: NewEnum property was commented out. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="B3FC1610-34F3-43F5-86B7-16C984F0E88E"'
	'Public ReadOnly Property NewEnum() As stdole.IUnknown
		'Get
			'
			'NewEnum = mCol._NewEnum
		'End Get
	'End Property
	
	Public Function GetEnumerator() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
		'UPGRADE_TODO: Uncomment and change the following line to return the collection enumerator. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="95F9AAD0-1319-4921-95F0-B9D3C4FF7F1C"'
		GetEnumerator = mCol.GetEnumerator
	End Function
	
	'**% Remove: Removes an element from the collection
	'% Remove: Elimina un elemento de la colección
	'---------------------------------------------
	Public Sub Remove(ByRef vntIndexKey As Object)
		'---------------------------------------------
		
		mCol.Remove(vntIndexKey)
	End Sub
	
	'**% Class_Initialize: controls the creation of an instance of the collection
	'% Class_Initialize: Controla la creación de una instancia de la colección
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		nAuxCommType = eRemoteDB.Constants.intNull
		mCol = New Collection
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
	
	'**% Class_Terminate: controls the delete of an instance of the collection
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
End Class






