Option Strict Off
Option Explicit On
Public Class Letters_ass
	Implements System.Collections.IEnumerable
	'**+Objetivo: Class generated from the table 'LETTERS_AS' Letter formats associated with a transaction window.A record per every letter format associated
	'**+Version: $$Revision: 9 $
	'+Objetive: Clase generada a partir de la tabla 'LETTERS_AS' Modelos de cartas asociados a una ventana.Un registro por cada modelo de carta asociado a una ventana.
	'+Version: $$Revision: 9 $
	
	'**-Objective: Variable to store the collection generated by the Letters_As class.
	'-Objetivo: Variable para almacenar la coleccion generada por la clase Letters_As.
	Private mCol As Collection
	
	'**-Objective: Temporary variable, code that identifies the window (logical).
	'-Objetivo: Variable temporal, c?digo identificativo de la ventana (l?gico).
	Private mstrCodispl As String
	
	Private mintProcess As Short
	
	
	'**%Objective: Adds a new instance of the Letters_as class to the collection.
	'**%Parameters:
	'**%  objClass  - Object of type collection that stores a group of variables.
	'%Objetivo: A?ade una nueva instancia de la clase Letters_as a la colecci?n
	'%Par?metros:
	'%  objClass    - Objeto de tipo colecci?n que almacena un grupo de variables.
	Private Function Add(ByRef objClass As Letters_as) As Letters_as
		Dim objNewMember As Letters_as
		
		If Not IsIDEMode Then
		End If
		
		objNewMember = New Letters_as
		mCol.Add(objClass)

        Add = objNewMember

		Exit Function
		Add = objNewMember
		objNewMember = Nothing
	End Function
	
	'**%Objective: Look for the letter or letters associated a consulted transaction
	'**%Parameters:
	'**%  sCodispl  - Consulted page
	'**%  lblnAll   - Variable of boolean condition, this indicates if it found some registry or not
	'%Objetivo: Tiene como objetivo buscar la carta o cartas asociadas a una transacci?n consultada
	'%Par?metros:
	'%  sCodispl    - Pagina consultada
	'%  lblnAll     - Variable de condici?n booleana, esta indica si encontro alg?n registro o no
	Public Function FindLT002(ByVal sCodispl As String, Optional ByVal nProcess As Short = intNull, Optional ByVal lblnAll As Boolean = False) As Boolean
		Dim lrecReaLetters_as As eRemoteDB.Execute
		Dim lobjLetters_as As Letters_as
		Dim lintVar As Short
		
		If Not IsIDEMode Then
		End If
		
		
		lrecReaLetters_as = New eRemoteDB.Execute
		
		If mstrCodispl <> sCodispl Or mintProcess <> nProcess Or lblnAll Then
			mCol = New Collection
			
			With lrecReaLetters_as
				.StoredProcedure = "reaLetters_as2"
				.Parameters.Add("sCodispl", sCodispl, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 8, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
				.Parameters.Add("nLetterNum", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
				.Parameters.Add("nConsec", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nProcess", nProcess, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
				.Parameters.Add("nBranch", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				If .Run Then
					FindLT002 = True
					If sCodispl <> "SI119" Then
						lintVar = 0
						Do While Not .EOF
							If lintVar <> .FieldToClass("nConsec") Then
								lobjLetters_as = New Letters_as
								lobjLetters_as.nConsec = .FieldToClass("nConsec")
								lobjLetters_as.sCodispl = .FieldToClass("sCodispl")
								lobjLetters_as.nProcess = .FieldToClass("nProcess")
								lobjLetters_as.nBranch = .FieldToClass("nBranch")
								lobjLetters_as.nLetterNum = .FieldToClass("nLetterNum")
								lobjLetters_as.nUsercode = .FieldToClass("nUsercode")
								lobjLetters_as.nStatusInstance = 2
								lobjLetters_as.nProduct = .FieldToClass("nProduct")
								lobjLetters_as.sRoutine = .FieldToClass("sRoutine")
								lobjLetters_as.nLanguage = .FieldToClass("nLanguage")
								lobjLetters_as.sRequired = .FieldToClass("sRequired")
								Call Add(lobjLetters_as)
								lintVar = .FieldToClass("nConsec")
							End If
							.RNext()
						Loop 
					Else
						Do While Not .EOF
							lobjLetters_as = New Letters_as
							lobjLetters_as.nConsec = .FieldToClass("nConsec")
							lobjLetters_as.sCodispl = .FieldToClass("sCodispl")
							lobjLetters_as.nProcess = .FieldToClass("nProcess")
							lobjLetters_as.nBranch = .FieldToClass("nBranch")
							lobjLetters_as.nLetterNum = .FieldToClass("nLetterNum")
							lobjLetters_as.nUsercode = .FieldToClass("nUsercode")
							lobjLetters_as.nStatusInstance = 2
							lobjLetters_as.nProduct = .FieldToClass("nProduct")
							lobjLetters_as.sRoutine = .FieldToClass("sRoutine")
							lobjLetters_as.sDescript = .FieldToClass("ProductDesc")
							lobjLetters_as.nLanguage = .FieldToClass("nLanguage")
							lobjLetters_as.sRequired = .FieldToClass("sRequired")
							
							'lobjLetters_as.ProcessDesc = .FieldToClass("ProcessDesc")
							'lobjLetters_as.BranchDesc = .FieldToClass("BranchDesc")
							'                        lobjLetters_as.Sub_typeDesc = .FieldToClass("Sub_typeDesc")
							'lobjLetters_as.LetterDesc = .FieldToClass("LetterDesc")
							
							Call Add(lobjLetters_as)
							.RNext()
						Loop 
					End If
					.RCloseRec()
					mstrCodispl = sCodispl
					mintProcess = nProcess
				Else
					FindLT002 = False
				End If
			End With
		Else
			FindLT002 = True
		End If
		
		Exit Function
		lrecReaLetters_as = Nothing
		lobjLetters_as = Nothing
	End Function
	
	'**%Objective: Has as objective executing an action according to is its initial condition
	'%Objetivo: Tiene como objetivo el ejecutar una acci?n seg?n sea su condici?n inicial
	Private Function Update() As Boolean
		Dim lclsLetters_as As Letters_as
		
		If Not IsIDEMode Then
		End If
		
		For	Each lclsLetters_as In mCol
			Select Case lclsLetters_as.nStatusInstance
				Case 1 ' Add.  ' Registrar.
					Update = lclsLetters_as.Add()
				Case 2 ' Update. ' Modificar.
					Update = lclsLetters_as.Update()
				Case 3 ' Delete. ' Eliminar
					Update = lclsLetters_as.Delete()
			End Select
		Next lclsLetters_as
		
		Exit Function
	End Function
	
	'**%Objective: Gives back an element of the collection (according to index)
	'**%Parameters:
	'**%   vntIndexKey    - Variable that serves as index.
	'%Objetivo: Devuelve un elemento de la colecci?n (segun ?ndice)
	'%Par?metros:
	'%   vntIndexKey    - Variable que sirve de indice.
	Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As Letters_as
		Get
			
			If Not IsIDEMode Then
			End If
			
			Item = mCol.Item(vntIndexKey)
			
			Exit Property
		End Get
	End Property
	
	'**%Objective: Gives back the number of elements that the collection has.
	'%Objetivo: Devuelve el n?mero de elementos que posee la colecci?n
	Public ReadOnly Property Count() As Integer
		Get
			
			If Not IsIDEMode Then
			End If
			
			Count = mCol.Count()
			
			Exit Property
		End Get
	End Property
	
	'**%Objective: It allows to enumerate the collection to use it in a cycle For Each...Next
	'%Objetivo: Permite enumerar la colecci?n para utilizarla en un ciclo For Each... Next
	'UPGRADE_NOTE: NewEnum property was commented out. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="B3FC1610-34F3-43F5-86B7-16C984F0E88E"'
	'Public ReadOnly Property NewEnum() As stdole.IUnknown
		'Get
			'
			'If Not IsIDEMode Then
			'End If
			'
			'NewEnum = mCol._NewEnum
			'
			'Exit Property
'ErrorHandler: '
			'ProcError("Letters_ass.NewEnum()")
		'End Get
	'End Property
	
	Public Function GetEnumerator() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
				GetEnumerator = mCol.GetEnumerator
	End Function
	
	'**%Objective: Eliminates an element of the collection (according to index)
	'**%Parameters:
	'**%   vntIndexKey    - Variable that serves as index.
	'%Objetivo: Elimina un elemento de la colecci?n (segun ?ndice)
	'%Par?metros:
	'%   vntIndexKey    - Variable que sirve de indice.
	Private Sub Remove(ByVal vntIndexKey As Object)
		
		If Not IsIDEMode Then
		End If
		
		mCol.Remove(vntIndexKey)
		
		Exit Sub
	End Sub
	
	'**%Objective: Creates the instance of the collection
	'%Objetivo: Crea la instancia de la colecci?n
	Private Sub Class_Initialize_Renamed()
		If Not IsIDEMode Then
		End If
		
		mCol = New Collection
		mstrCodispl = String.Empty
		mintProcess = intNull
		Exit Sub
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
	
	'**%Objective: Destruction of an instance of the collection
	'%Objetivo: Elimina la instancia de la colecci?n
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











