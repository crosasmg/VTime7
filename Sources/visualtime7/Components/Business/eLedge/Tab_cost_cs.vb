Option Strict Off
Option Explicit On
Public Class Tab_cost_cs
	'**- local variable to hold collection
	
	Private Primero As Boolean
	Private mCol As Collection
	
	Private lAuxLed_Compan As Integer
	
	'**% Add: adds a new instance of the Tab_cost_c class to the collection
	'% Add: Añade una nueva instancia de la clase Tab_cost_c a la colección
	Public Function Add(ByVal objElement As Object) As Tab_cost_c
		
		'**-Defines the variable that will contein the instance to add
		'- Se define la variable que contendra la instancia a añadir
		
		Dim objNewMember As Tab_cost_c
		objNewMember = objElement
		
		mCol.Add(objNewMember)
		
		'**+Return the created object
		'+ Retorna el objeto creado
		
		Add = objNewMember
		'UPGRADE_NOTE: Object objNewMember may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		objNewMember = Nothing
	End Function
	
	
	'**% Find: Loads the information of the Organizative Units
	'**%      that belongs to a Countable Company
	'% Find: Carga la información de las Unidades Organizativas
	'%       pertenecientes a una Compañia Contable
	Public Function Find(ByVal lintLed_Compan As Integer, Optional ByVal lbnFind As Boolean = False) As Boolean
		Dim lclsTab_cost_c As Object
		Dim lrecreaTab_cost_cActiveCut As eRemoteDB.Execute
		Dim lclsTab_cost_cActiveCut As Tab_cost_c
		lrecreaTab_cost_cActiveCut = New eRemoteDB.Execute
		
		'**+ Variable that will permit to control the execution of the method in case of exist any info
		'**+in the collection for the Countable Company required
		'+ Variable que permitira controlar la ejecución del metodo en caso de que exista información
		'+ en la colección para la Compañía Contable requerida
		
		'Static lAuxLed_Compan As long
		'Static lblnRead As Boolean
		
		'**Parameters definition for the stored procedure 'insudb.reaTab_cost_cActiveCut'
		'**Data read on 05/23/2001 04:20:47 p.m.
		'Definición de parámetros para stored procedure 'insudb.reaTab_cost_cActiveCut'
		'Información leída el 23/05/2001 04:20:47 p.m.
		
		'UPGRADE_NOTE: Object mCol may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		mCol = Nothing
		mCol = New Collection
		
		lAuxLed_Compan = lintLed_Compan
		
		With lrecreaTab_cost_cActiveCut
			.StoredProcedure = "reaTab_cost_cActiveCut"
			.Parameters.Add("nLed_compan", lintLed_Compan, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(True) Then
				Do While Not .EOF
					lclsTab_cost_cActiveCut = New Tab_cost_c
					lclsTab_cost_cActiveCut.nLed_compan = .FieldToClass("nLed_compan")
					lclsTab_cost_cActiveCut.sCost_cente = .FieldToClass("sCost_cente")
					lclsTab_cost_cActiveCut.sBlock_cre = .FieldToClass("sBlock_cre")
					lclsTab_cost_cActiveCut.sBlock_deb = .FieldToClass("sBlock_deb")
					lclsTab_cost_cActiveCut.sDescript = .FieldToClass("sDescript")
					lclsTab_cost_cActiveCut.nNoteNum = .FieldToClass("nNotenum")
					lclsTab_cost_cActiveCut.sStatregt = .FieldToClass("sStatregt")
					
					Call Add(lclsTab_cost_cActiveCut)
					'UPGRADE_NOTE: Object lclsTab_cost_cActiveCut may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
					lclsTab_cost_cActiveCut = Nothing
					.RNext()
				Loop 
				
				.RCloseRec()
				
				Find = True
			Else
				Find = False
			End If
		End With
		
		'UPGRADE_NOTE: Object lclsTab_cost_c may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsTab_cost_c = Nothing
	End Function
	
	'**% Find_Cost:cente: Allows to find inside the collection one organizative Unit
	'% Find_Cost_cente: Permite buscar dentro de la Colección una Unidad Organizativa Determinada
	Public Function Find_Cost_cente(ByVal lstrCost_cente As String, ByVal nLed_compan As Integer) As Boolean
		
		Dim lclsTab_cost_c As Tab_cost_c
		
		lstrCost_cente = Trim(lstrCost_cente)
		Find_Cost_cente = False
		
		If Me.Find(nLed_compan) Then
		End If
		
		For	Each lclsTab_cost_c In mCol
			With lclsTab_cost_c
				If .sCost_cente = lstrCost_cente Then
					Find_Cost_cente = True
					Exit For
				End If
			End With
		Next lclsTab_cost_c
		
		'UPGRADE_NOTE: Object lclsTab_cost_c may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsTab_cost_c = Nothing
	End Function
	
	'**% Update: Makes the action that indicates nStatusInstance
	'% Update: Realiza la accion que indica nStatusInstance
	Public Function Update(ByVal nAction As Integer, ByVal sCost_cente As String, ByVal nLed_compan As Integer, ByVal nNoteNum As Integer, ByVal sBlock_cre As String, ByVal sBlock_deb As String, ByVal sDescript As String, ByVal sStatregt As String, ByVal dCompdate As Date, ByVal nUsercode As Integer) As Boolean
		Dim lclsTab_cost_c As eLedge.Tab_cost_c
		lclsTab_cost_c = New eLedge.Tab_cost_c
		Update = True
		
		'For Each lclsTab_cost_c In mCol
		With lclsTab_cost_c
			.sCost_cente = sCost_cente
			.nLed_compan = nLed_compan
			.nNoteNum = nNoteNum
			.sBlock_cre = sBlock_cre
			.sBlock_deb = sBlock_deb
			.sDescript = sDescript
			.sStatregt = sStatregt
			.dCompdate = dCompdate
			.nUsercode = nUsercode
			Select Case nAction
				Case eFunctions.Menues.TypeActions.clngActionadd
					Update = .Add(nLed_compan, nNoteNum, sBlock_cre, sBlock_deb, sDescript, sStatregt, dCompdate, nUsercode)
					'                    .nStatusInstance = eftQuery
					
				Case eFunctions.Menues.TypeActions.clngActionUpdate
					Update = .Update
					
				Case eFunctions.Menues.TypeActions.clngActioncut
					Update = .Delete
					'mCol.Remove ("TCC" & .nLed_compan & .sCost_cente)
			End Select
		End With
		'Next lclsTab_cost_c
		
		'UPGRADE_NOTE: Object lclsTab_cost_c may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsTab_cost_c = Nothing
	End Function
	
	
	'*** Item: takes one element from the collection
	'* Item: toma un elemento de la colección
	Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As Tab_cost_c
		Get
			Item = mCol.Item(vntIndexKey)
		End Get
	End Property
	
	'*** Count:  count the elements number inside the collection
	'* Count: cuenta el número de elementos dentro de la colección
	Public ReadOnly Property Count() As Integer
		Get
			Count = mCol.Count()
		End Get
	End Property
	
	'*** Remove: deletes one element inside the collection
	'* Remove: elimina un elemento dentro de la colección
	Public Sub Remove(ByRef vntIndexKey As Object)
		mCol.Remove(vntIndexKey)
	End Sub
	
	''*** NewEnum: enumerates the elements inside the collection
	''* NewEnum: enumera los elementos dentro de la colección
	''--------------------------------------------------------------------------------------------
	'Public Property Get NewEnum() As IUnknown
	''--------------------------------------------------------------------------------------------
	'    Set NewEnum = mCol.[_NewEnum]
	'End Property
	
	'*** Class_Initialize: controls the reopening of each instance of the collection
	'* Class_Initialize: controla la apertura de cada instancia de la colección
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		mCol = New Collection
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
	
	'*** Class_Terminate:deletes the collection
	'* Class_Terminate: elimina la colección
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






