Option Strict Off
Option Explicit On
Public Class GroupVariabless
	Implements System.Collections.IEnumerable
	'**+Objetive: Clase generada a partir de la tabla 'GROUPVARIABLES' que contiene Grupos de variables de correspondencia.
	'**+Version: $$Revision: 9 $
	'+Objetivo: Clase generada a partir de la tabla 'GROUPVARIABLES' Groups of correspondence variables.
	'+Version: $$Revision: 9 $
	
	'**-Objective:
	'-Objetivo:
	Private mCol As Collection
	
	'**-Objective:
	'-Objetivo:
	Private mintLett_group As Short
	
	'**%Objective: Añade una nueva instancia de la clase GroupVariables a la colección
	'**%Parameters:
	'**%  objClass
	'%Objetivo: Añade una nueva instancia de la clase GroupVariables a la colección
	'%Parámetros:
	'%    objClass
	Public Function Add(ByRef objClass As GroupVariables) As GroupVariables
		Dim objNewMember As GroupVariables
		
		If Not IsIDEMode Then
		End If
		
		objNewMember = New GroupVariables
		
		With objNewMember
			.nLett_group = objClass.nLett_group
			.nStatusInstance = objClass.nStatusInstance
			.nUsercode = objClass.nUsercode
			.sColumName = objClass.sColumName
			.sTableName = objClass.sTableName
			.sDescript = objClass.sDescript
			.sVariable = objClass.sVariable
			.nTypVariable = objClass.nTypVariable
			.sAliasTable = objClass.sAliasTable
			.sAliasColumn = objClass.sAliasColumn
			.sGroupDescript = objClass.sGroupDescript
			.sFldSource = objClass.sFldSource
			.sFldValue = objClass.sFldValue
		End With
		mCol.Add(objNewMember)
		Add = objNewMember
		objNewMember = Nothing
		
		Exit Function
		objNewMember = Nothing
	End Function
	
	'**%Objective:
	'**%Parameters:
	'**%  nLett_group
	'**%  lblnAll
	'%Objetivo:
	'%Parámetros:
	'%  nLett_group
	'%  lblnAll
	Public Function Find(ByVal nLett_group As Short, Optional ByVal lblnAll As Boolean = False) As Boolean
		Dim lrecReaGroupVariables As eRemoteDB.Execute
		Dim lobjGroupVariables As GroupVariables
		
		If Not IsIDEMode Then
		End If
		
		lrecReaGroupVariables = New eRemoteDB.Execute
		
		If mintLett_group <> nLett_group Or lblnAll Then
			mCol = New Collection
			
			With lrecReaGroupVariables
				.StoredProcedure = "reaGroupVariables"
				.Parameters.Add("nLett_group", nLett_group, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
				.Parameters.Add("sVariable", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				If .Run Then
					Find = True
					Do While Not .EOF
						lobjGroupVariables = New GroupVariables
						lobjGroupVariables.nLett_group = .FieldToClass("nLett_group")
						lobjGroupVariables.sVariable = .FieldToClass("sVariable")
						lobjGroupVariables.sDescript = .FieldToClass("sDescript")
						lobjGroupVariables.sTableName = .FieldToClass("sTableName")
						lobjGroupVariables.sColumName = .FieldToClass("sColumName")
						lobjGroupVariables.nTypVariable = .FieldToClass("nTypVariable")
						lobjGroupVariables.nUsercode = .FieldToClass("nUsercode")
						lobjGroupVariables.sAliasTable = .FieldToClass("sAliasTable")
						lobjGroupVariables.sAliasColumn = .FieldToClass("sAliasColumn")
						lobjGroupVariables.sGroupDescript = .FieldToClass("sGroupDescript")
						Call Add(lobjGroupVariables)
						.RNext()
					Loop 
					.RCloseRec()
				Else
					Find = False
				End If
			End With
		Else
			Find = True
		End If
		
		Exit Function
	End Function
	
	'**%Objective:
	'%Objetivo:
	Private Function Update() As Boolean
		Dim lclsGroupVariables As GroupVariables
		
		If Not IsIDEMode Then
		End If
		
		For	Each lclsGroupVariables In mCol
			Select Case lclsGroupVariables.nStatusInstance
				Case 1 ' Registrar
					Update = lclsGroupVariables.Add()
				Case 2 ' Modificar
					Update = lclsGroupVariables.Update()
				Case 3 ' Eliminar
					Update = lclsGroupVariables.Delete()
			End Select
		Next lclsGroupVariables
		
		Exit Function
	End Function
	
	'**%Objective: Devuelve un elemento de la colección (segun índice)
	'**%Parameters:
	'**%  vntIndexKey
	'%Objetivo: Devuelve un elemento de la colección (segun índice)
	'%Parámetros:
	'%  vntIndexKey
	Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As GroupVariables
		Get
			If Not IsIDEMode Then
			End If
			
			Item = mCol.Item(vntIndexKey)
			
            Exit Property
		End Get
	End Property
	
	'**%Objective: Devuelve el número de elementos que posee la colección
	'%Objetivo: Devuelve el número de elementos que posee la colección
	Public ReadOnly Property Count() As Integer
		Get
			If Not IsIDEMode Then
			End If
			
			Count = mCol.Count()
			
            Exit Property
		End Get
	End Property
	
	'**%Objective: Permite enumerar la colección para utilizarla en un ciclo For Each... Next
	'%Objetivo: Permite enumerar la colección para utilizarla en un ciclo For Each... Next
	'UPGRADE_NOTE: NewEnum property was commented out. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="B3FC1610-34F3-43F5-86B7-16C984F0E88E"'
	'Public ReadOnly Property NewEnum() As stdole.IUnknown
		'Get
			'If Not IsIDEMode Then
			'End If
			'
			'NewEnum = mCol._NewEnum
			'
			'Exit Property
'ErrorHandler: '
			'ProcError("GroupVariabless.NewEnum()")
		'End Get
	'End Property
	
	Public Function GetEnumerator() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
				GetEnumerator = mCol.GetEnumerator
	End Function
	
	'**%Objective: Elimina un elemento de la colección
	'**%Parameters:
	'**%  vntIndexKey
	'%Objetivo: Elimina un elemento de la colección
	'%Parámetros:
	'%  vntIndexKey
	Private Sub Remove(ByVal vntIndexKey As Object)
		If Not IsIDEMode Then
		End If
		
		mCol.Remove(vntIndexKey)
		
		Exit Sub
	End Sub
	
	'**%Objective: Controla la creación de una instancia de la colección
	'%Objetivo: Controla la creación de una instancia de la colección
	Private Sub Class_Initialize_Renamed()
		If Not IsIDEMode Then
		End If
		
		mCol = New Collection
		mintLett_group = intNull
		
		Exit Sub
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
	
	'**%Objective: Controla la destrucción de una instancia de la colección
	'%Objetivo: Controla la destrucción de una instancia de la colección
	Private Sub Class_Terminate_Renamed()
		If Not IsIDEMode Then
		End If
		
		mCol = Nothing
		
		Exit Sub
	End Sub
	Protected Overrides Sub Finalize()
		Class_Terminate_Renamed()
		MyBase.Finalize()
	End Sub
End Class











