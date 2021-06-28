Option Strict Off
Option Explicit On
Public Class EndorsLetterss
	Implements System.Collections.IEnumerable
	'**+Objective: Collection that supports the class 'EndorsLetters'.
	'**+Version: $$Revision: $
	'+Objetivo: Colección que le da soporte a la clase 'EndorsLetters'.
	'+Version: $$Revision: $
	
	'**-Objective: Local variable to hold collection.
	'-Objetivo: Variable Local para almacenar la colección.
	
	Private mcolEndorsLetters As Collection
	
	'**%Objective: Adds an element to the collection.
	'**%Parameters:
	'**%    lclsEndorsLetters -
	'%Objetivo: Este método permite agregar un elemento a la colección.
	'%Parámetros:
	'%    lclsEndorsLetters -
	Public Function Add(ByRef lclsEndorsLetters As EndorsLetters) As EndorsLetters
		If Not IsIDEMode Then
		End If
		
		'**+ The properties passed to the method are assigned to the collection.
		'+ Las propiedades pasadas al método son asignadas a la colección.
		
		mcolEndorsLetters.Add(lclsEndorsLetters)
		
		'**+Returns the object created.
		'+ Retorna el objeto creado.
		
		Add = lclsEndorsLetters
		lclsEndorsLetters = Nothing
		
		Exit Function
	End Function
	
	'**%Objective: Searches for records in the table 'EndorsLetters'.
	'%Objetivo: Esta función permite realizar la búsqueda de la información en la tabla 'EndorsLetters'.
	Public Function Find() As Boolean
		
		Dim lclsEndorsLetters As eRemoteDB.Execute
		Dim lclsEndorsLettersItem As EndorsLetters
		
		If Not IsIDEMode Then
		End If
		
		lclsEndorsLetters = New eRemoteDB.Execute
		
		With lclsEndorsLetters
            .StoredProcedure = "reaEndorsLetters_a"
			If .Run(True) Then
				Do While Not .EOF
					lclsEndorsLettersItem = New EndorsLetters
					lclsEndorsLettersItem.nEndorseType = .FieldToClass("nEndorseType")
					lclsEndorsLettersItem.nLetterNum = .FieldToClass("nLetterNum")
					lclsEndorsLettersItem.sDescriptTab_Letter = .FieldToClass("sDescriptTab_Letter")
					lclsEndorsLettersItem.sDescriptTable3012 = .FieldToClass("sDescriptTable3012")
					Call Add(lclsEndorsLettersItem)
					lclsEndorsLettersItem = Nothing
					.RNext()
				Loop 
				Find = True
				.RCloseRec()
			Else
				Find = False
			End If
		End With
		
		lclsEndorsLetters = Nothing
		
		Exit Function
		ObjectRelease = lclsEndorsLetters
	End Function
	
	'**%Objective: This property is used when an element in the collection is referenced.
	'**%Parameters:
	'**%    vIndexKey - An expression that specifies the position of an element from the collection
	'%Objetivo: Esta propiedad es usada cuando se hace referencia a un elemento de la colección.
	'%Parámetros:
	'%    vIndexKey - Una expresión que especifica la posición de un elemento de la colección.
	Public ReadOnly Property Item(ByVal vIndexKey As Object) As EndorsLetters
		Get
			If Not IsIDEMode Then
			End If
			
			Item = mcolEndorsLetters.Item(vIndexKey)
			
			Exit Property
		End Get
	End Property
	
	'**%Objective: Returns the number of elements in the collection.
	'%Objetivo: Retorna la cantidad de elementos existentes en la colección.
	Public ReadOnly Property Count() As Integer
		Get
			If Not IsIDEMode Then
			End If
			
			Count = mcolEndorsLetters.Count()
			
			Exit Property
		End Get
	End Property
	
	'**%Objective: Allows you to enumerate this collection with a "For...Each" loop.
	'%Objetivo: Esta propiedad permite enumerar los elementos de la colección por medio de un "For...Each".
	'UPGRADE_NOTE: NewEnum property was commented out. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="B3FC1610-34F3-43F5-86B7-16C984F0E88E"'
	'Public ReadOnly Property NewEnum() As stdole.IUnknown
		'Get
			'If Not IsIDEMode Then
			'End If
			'
			'NewEnum = mcolEndorsLetters._NewEnum
			'
			'Exit Property
'ErrorHandler: '
			'ProcError("EndorsLetterss.NewEnum()")
		'End Get
	'End Property
	
	Public Function GetEnumerator() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
				GetEnumerator = mcolEndorsLetters.GetEnumerator
	End Function
	
	'**%Objective: Removes an element from the collection.
	'**%Parameters:
	'**%    vIndexKey - An expression that specifies the position of an element from the collection
	'%Objetivo: Permite eliminar un elemento de la colección.
	'%Parámetros:
	'%    vIndexKey - Una expresión que especifica la posición de un elemento de la colección.
	Public Sub Remove(ByRef vIndexKey As Object)
		If Not IsIDEMode Then
		End If
		
		mcolEndorsLetters.Remove(vIndexKey)
		
		Exit Sub
	End Sub
	
	
	'**%Objective: Searches for records in the table 'EndorsLetters'.
	'**%Parameters:
	'%Objetivo: Esta función permite realizar la búsqueda de la información en la tabla 'EndorsLetters'.
	'%Parámetros:
    Public Function FindEndorsLetters(ByVal sClient As String, ByVal nUsercode As Double, Optional ByVal sCodispl As String = "", Optional ByVal sCertype As String = "", Optional ByVal nBranch As Short = intNull, Optional ByVal nProduct As Short = intNull, Optional ByVal nPolicy As Integer = intNull, Optional ByVal nCertif As Integer = intNull, Optional ByVal nClaim As Integer = intNull, Optional ByVal nCase_num As Short = intNull, Optional ByVal nDeman_type As Short = intNull, Optional ByVal nBordereaux As Integer = intNull) As Boolean
        Dim lclsEndorsLetters As eRemoteDB.Execute
        Dim lclsEndorsLettersItem As EndorsLetters
        Dim lclsLettRequest As LettRequest

        If Not IsIDEMode() Then
        End If

        lclsEndorsLetters = New eRemoteDB.Execute
        lclsLettRequest = New LettRequest

        Select Case sCodispl
            Case "SCA801"
                nBranch = intNull
                nProduct = intNull
                nPolicy = intNull
                nCertif = intNull
                nClaim = intNull
                nCase_num = intNull
                nDeman_type = intNull
                nBordereaux = intNull
            Case "SCA802"
                nClaim = intNull
                nCase_num = intNull
                nDeman_type = intNull
                nBordereaux = intNull
            Case "SCA803"
                nBranch = intNull
                nProduct = intNull
                nPolicy = intNull
                nCertif = intNull
        End Select
        FindEndorsLetters = False

        With lclsEndorsLetters
            .StoredProcedure = "reaEndorsement"
            .Parameters.Add("sCodispl", sCodispl, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 8, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nClaim", nClaim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBordereaux", nBordereaux, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            If .Run(True) Then
                Do While Not .EOF
                    Call lclsLettRequest.PostSCA008(sCodispl, intNull, .FieldToClass("nLetterNum"), .FieldToClass("nLanguage"), .FieldToClass("sDescript"), intNull, dtmNull, .FieldToClass("tLetter"), .FieldToClass("sAddress"), sClient, sCertype, nBranch, nProduct, nPolicy, nCertif, nClaim, nCase_num, nBordereaux, Today, nUsercode, intNull, Today, nDeman_type)

                    lclsEndorsLettersItem = Nothing
                    .RNext()
                Loop
                FindEndorsLetters = True
                .RCloseRec()
            Else
                FindEndorsLetters = False
            End If
        End With

        lclsEndorsLetters = Nothing
        lclsLettRequest = Nothing
        Exit Function
        ObjectRelease = lclsEndorsLetters
    End Function
	
	'**%Objective: Creates the collection when this class is created.
	'%Objetivo: Esta método crea la colección cuando se crea la clase.
	Private Sub Class_Initialize_Renamed()
		If Not IsIDEMode Then
		End If
		
		mcolEndorsLetters = New Collection
		
		Exit Sub
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
	
	'**%Objective: Destroys collection when this class is terminated.
	'%Objetivo: Este método destruye la colección cuando se termina la clase.
	Private Sub Class_Terminate_Renamed()
		If Not IsIDEMode Then
		End If
		
		mcolEndorsLetters = Nothing
		
		Exit Sub
	End Sub
	Protected Overrides Sub Finalize()
		Class_Terminate_Renamed()
		MyBase.Finalize()
	End Sub
End Class











