Option Strict Off
Option Explicit On
Public Class Plan_Intwar_Month
	Implements System.Collections.IEnumerable
	'%-------------------------------------------------------%'
	'% $Workfile:: Plan_Intwar_Month.cls                          $%'
	'% $Author:: Ljimenez                                  $%'
	'% $Date:: 5-10-09 13:51                                $%'
	'% $Revision:: 3                                        $%'
	'%-------------------------------------------------------%'
	
	Public nMonth As Short
	Public nRate As Double
	Public nRate_sec As Double
	Public nUsercode As Integer
	
	'- Local variable to hold collection
	Private mCol As Collection
	
	'**% Item: Returns an element of the collection (according to the index)
	'% Item: Devuelve un elemento de la colección (segun índice)
	Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As Plan_Intwar_Month
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
	End Sub
	Protected Overrides Sub Finalize()
		Class_Terminate_Renamed()
		MyBase.Finalize()
	End Sub
	
	'Add_Plan_Intwar_Month: Agrega un nuevo registro a la colección
	Public Function Add(ByVal nMonth As Integer, ByVal nRate As Double, ByVal nRate_sec As Double) As Plan_Intwar_Month
		Dim objNewMember As Plan_Intwar_Month
		objNewMember = New Plan_Intwar_Month
		
		If mCol Is Nothing Then
			mCol = New Collection
		End If
		
		With objNewMember
			.nMonth = nMonth
			.nRate = nRate
			.nRate_sec = nRate_sec
		End With
		
		mCol.Add(objNewMember)
		'Return the object created
		Add = objNewMember
		'UPGRADE_NOTE: Object objNewMember may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		objNewMember = Nothing
	End Function
	
	
	'%Find_Plan_Intwar_Month_Plan_Intwar_Month: Lee los datos de la tabla de tasas mensuales
	Public Function Find_Plan_Intwar_Month(ByVal nYear As Integer, ByVal nTypeInvest As Integer) As Boolean
		Dim lrecReaPlan_Intwar_Month As eRemoteDB.Execute
		On Error GoTo Find_Plan_Intwar_Month_Err
		
		Find_Plan_Intwar_Month = True
		
		lrecReaPlan_Intwar_Month = New eRemoteDB.Execute
		
		'+Definición de parámetros para stored procedure 'ReaPlan_Intwar_Month_a'
		With lrecReaPlan_Intwar_Month
			.StoredProcedure = "reaPlan_Intwar_Month"
			.Parameters.Add("nYear", nYear, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTypeInvest", nTypeInvest, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run Then
				Find_Plan_Intwar_Month = True
				Do While Not .EOF
					Call Add(.FieldToClass("nMonth"), .FieldToClass("nRate"), .FieldToClass("nRate_sec"))
					.RNext()
				Loop 
				.RCloseRec()
			Else
				Find_Plan_Intwar_Month = False
			End If
		End With
		'UPGRADE_NOTE: Object lrecReaPlan_Intwar_Month may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecReaPlan_Intwar_Month = Nothing
		
Find_Plan_Intwar_Month_Err: 
		If Err.Number Then
			Find_Plan_Intwar_Month = False
		End If
		'UPGRADE_NOTE: Object lrecReaPlan_Intwar_Month may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecReaPlan_Intwar_Month = Nothing
		On Error GoTo 0
	End Function
	
	'% insValMDP8050_K: Realiza la validación de los campos del encabezado, correspondiente a la ventana DP8050 - Tasa mensual de rentabilidad
	Public Function insValMDP8050_k(ByVal nYear As Integer, ByVal nTypeInvest As Integer) As String
		Dim lobjErrors As eFunctions.Errors
		Dim lclsValField As eFunctions.valField
		
		On Error GoTo insValMDP8050_k_Err
		
		lobjErrors = New eFunctions.Errors
		With lobjErrors
			'+ Se valida el campo año.
			If nYear = 0 Or nYear = eRemoteDB.Constants.intNull Then
				Call .ErrorMessage("MDP8050", 60338)
			Else
				If nYear < 2000 Then
					Call .ErrorMessage("MDP8050", 80136)
				End If
			End If
			
			'+ Se valida el campo tipo de tasa.
			If nTypeInvest = 0 Or nTypeInvest = eRemoteDB.Constants.intNull Then
				Call .ErrorMessage("MDP8050", 60213)
			End If
			
			insValMDP8050_k = .Confirm
			
		End With
		
insValMDP8050_k_Err: 
		If Err.Number Then
			insValMDP8050_k = "insValMDP8050_k: " & Err.Description
		End If
		'UPGRADE_NOTE: Object lobjErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lobjErrors = Nothing
		'UPGRADE_NOTE: Object lclsValField may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsValField = Nothing
		On Error GoTo 0
	End Function
	
	
	'% insValMDP8050: Realiza la validación de los campos del Detalle de la ventana DP8050 - Tasa mensual de rentabilidad
	Public Function insValMDP8050(ByVal sAction As String, ByVal nYear As Integer, ByVal nTypeInvest As Integer, ByVal nMonth As Integer, ByVal nRate As Double) As String
		Dim lobjValues As eFunctions.Values
		Dim lobjErrors As eFunctions.Errors
		
		On Error GoTo insValMDP8050_Err
		
		lobjValues = New eFunctions.Values
		lobjErrors = New eFunctions.Errors
		
		insValMDP8050 = String.Empty
		
		With lobjErrors
			'+ Incluya el mes.
			If nMonth = 0 Or nMonth = eRemoteDB.Constants.intNull Then
				Call .ErrorMessage("MDP8050", 1137)
			End If
			
			'+ Se valida la duplicidad del registro.
			If sAction = "Add" Then
				If valExistPlan_Intwar_Month(nYear, nMonth, nTypeInvest) Then
					Call .ErrorMessage("MDP8050", 12101)
				End If
			End If
			
			insValMDP8050 = .Confirm
		End With
		
insValMDP8050_Err: 
		If Err.Number Then
			insValMDP8050 = "insValMDP8050: " & Err.Description
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lobjErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lobjErrors = Nothing
		'UPGRADE_NOTE: Object lobjValues may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lobjValues = Nothing
	End Function
	
	
	'% valExistPlan_Intwar_Month: Permite verificar la existencia de la tasa de rentabilidad para un Año/mes.
	Public Function valExistPlan_Intwar_Month(ByVal nYear As Integer, ByVal nMonth As Integer, ByVal nTypeInvest As Integer) As Boolean
		Dim lrecPlan_Intwar_Month As eRemoteDB.Execute
		Dim lintExists As Short
		
		On Error GoTo valPlan_Intwar_Month_Err
		
		lrecPlan_Intwar_Month = New eRemoteDB.Execute
		With lrecPlan_Intwar_Month
			.StoredProcedure = "valPlan_Intwar_Month"
			.Parameters.Add("nYear", nYear, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nMonth", nMonth, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTypeInvest", nTypeInvest, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nExists", lintExists, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Run(False)
			valExistPlan_Intwar_Month = (.Parameters("nExists").Value = 1)
		End With
		
valPlan_Intwar_Month_Err: 
		If Err.Number Then
			valExistPlan_Intwar_Month = False
		End If
		'UPGRADE_NOTE: Object lrecPlan_Intwar_Month may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecPlan_Intwar_Month = Nothing
		On Error GoTo 0
	End Function
	
	
	'%insPostMDP8050: Actualización de la tabla de intereses garantizados para APV
	Public Function insPostMDP8050(ByVal sAction As String, ByVal nYear As Integer, ByVal nTypeInvest As Integer, ByVal nMonth As Integer, ByVal nRate As Double, ByVal nRate_sec As Double, ByVal nUsercode As Integer) As Boolean
		Dim nAction As Short
		Dim lrecPlan_Intwar_Month As eRemoteDB.Execute
		
		On Error GoTo insPostMDP8050_Err
		
		If sAction = "Add" Then
			nAction = 1
		ElseIf sAction = "Update" Then 
			nAction = 2
		Else
			nAction = 3
		End If
		
		lrecPlan_Intwar_Month = New eRemoteDB.Execute
		With lrecPlan_Intwar_Month
			.StoredProcedure = "insPlan_Intwar_Month"
			.Parameters.Add("nYear", nYear, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nMonth", nMonth, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTypeInvest", nTypeInvest, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nRate", nRate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 9, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nRate_sec", nRate_sec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 9, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAction", nAction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			insPostMDP8050 = .Run(False)
		End With
		
		'UPGRADE_NOTE: Object lrecPlan_Intwar_Month may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecPlan_Intwar_Month = Nothing
		
insPostMDP8050_Err: 
		If Err.Number Then
			insPostMDP8050 = False
		End If
		On Error GoTo 0
	End Function
End Class






