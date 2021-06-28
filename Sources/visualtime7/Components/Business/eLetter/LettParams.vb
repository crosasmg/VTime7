Option Strict Off
Option Explicit On
Public Class LettParams
	Implements System.Collections.IEnumerable
	'**+Objetive: Clase generada a partir de la tabla 'LETTPARAM' que es Parámetros requeridos por el modelo de carta
	'**+Version: $$Revision: 9 $
	'+Objetivo: Clase generada a partir de la tabla 'LETTPARAM' Parameters required in a letter template
	'+Version: $$Revision: 9 $
	
	'**-Objective: local variable to hold collection
	'-Objetivo: Variable modular de tipo colección
	Private mCol As Collection
	
	'**%Objective: Stores the registries found in the object to objNewMember the collection
	'**%Parameters:
	'**%  nLetterNum    - Number identifying the letter template.
	'**%  nParameters   - Parameter Code.The possible values as per table622.
	'**%  dEffecdate    - Date which from the record is valid.
	'**%  dNullDate     - Date when the record is cancelled.
	'**%  dCompdate     - Computer date when the record is updated or created.
	'**%  nUserCode     - Code of the user creating or updating the record.
	'**%  sDesLettParam - Description of parameter
	'**%  sKey          - Key field or index.
	'%Objetivo: Almacena los registros encontrados en el objeto objNewMember la colección
	'%Parámetros:
	'%  nLetterNum      - Código del modelo de carta.
	'%  nParameters     - Código del parámetro.Valores posibles según tabla622.
	'%  dEffecdate      - Fecha de efecto del registro.
	'%  dNullDate       - Fecha de anulación del registro.
	'%  dCompdate       - Fecha del computador en que se crea o actualiza el registro.
	'%  nUserCode       - Código del usuario que crea o actualiza el registro.
	'%  sDesLettParam   - Descripción del parametro.
	'%  sKey            - Campo clave o índice.
	Public Function Add(ByVal nLetterNum As Short, ByVal nParameters As Short, ByVal dEffecDate As Date, ByVal dNullDate As Date, ByVal dCompdate As Date, ByVal nUsercode As Short, ByVal sDesLettParam As String, Optional ByVal sKey As String = "") As LettParam
		Dim objNewMember As LettParam
		
		If Not IsIDEMode Then
		End If
		
		objNewMember = New LettParam
		
		With objNewMember
			.dCompdate = System.Date.FromOADate(nLetterNum)
			.dEffecDate = dEffecDate
			.dNullDate = dNullDate
			.nLetterNum = nLetterNum
			.nParameters = nParameters
			.nUsercode = nUsercode
			.sDesLettParam = sDesLettParam
		End With
		
		If Len(sKey) = 0 Then
			mCol.Add(objNewMember)
		Else
			mCol.Add(objNewMember, sKey)
		End If
		
		Add = objNewMember
		objNewMember = Nothing
		
		Exit Function
		Add = objNewMember
		objNewMember = Nothing
	End Function
	
	'**%Objective:  Used when referencing an element in the collection
	'**%            vntIndexKey contains either the Index or Key to the collection,
	'**%            this is why it is declared as a Variant
	'**%            Syntax: Set foo = x.Item(xyz) or Set foo = x.Item(5)
	'**%Parameters:
	'**%  vntIndexKey   - Key field or index.
	'%Objetivo: Se utiliza para referenciar un elemento de la colección, como índice o
	'%          como campo clave se usa la variable vntIndexKey de la colección,
	'%          este campo fue daclarado variant.
	'%          Sintaxis: Set foo = x.Item(xyz) or Set foo = x.Item(5)
	'%Parámetros:
	'%  vntIndexKey - Campo clave o índice.
	Public ReadOnly Property Item(ByVal vntIndexKey As Object) As LettParam
		Get
			
			If Not IsIDEMode Then
			End If
			
			Item = mCol.Item(vntIndexKey)
			
            Exit Property
		End Get
	End Property
	
	'**%Objective: Used when retrieving the number of elements in the collection. Syntax: Debug.Print x.Count
	'%Objetivo: Retorna el número de elementos de una colección. Sintaxis: Debug.Print x.Count
	Public ReadOnly Property Count() As Integer
		Get
			
			If Not IsIDEMode Then
			End If
			
			Count = mCol.Count()
			
            Exit Property
		End Get
	End Property
	
	'**%Objective: This property allows you to enumerate this collection with the For...Each syntax
	'%Objetivo: Esta propiedad localiza un elemento dentro de la colección con la sintaxis: For...Each
	'UPGRADE_NOTE: NewEnum property was commented out. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="B3FC1610-34F3-43F5-86B7-16C984F0E88E"'
	'Public ReadOnly Property NewEnum() As stdole.IUnknown
		'Get
			'
			'If Not IsIDEMode Then
			'End If
			'
			'NewEnum = mCol._NewEnum
			'
			'Exit Function
'ErrorHandler: '
			'ProcError("LettParams.NewEnum()")
		'End Get
	'End Property
	
	Public Function GetEnumerator() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
				GetEnumerator = mCol.GetEnumerator
	End Function
	
	'**%Objective:  Used when removing an element from the collection
	'**%            vntIndexKey contains either the Index or Key, which is why
	'**%            it is declared as a Variant
	'**%            Syntax: x.Remove(xyz)
	'**%Parameters:
	'**%  vntIndexKey  - Key field or index.
	'%Objetivo: Se utiliza para referenciar un elemento de la colección, como índice o
	'%          como campo clave se usa la variable vntIndexKey de la colección,
	'%          este campo fue daclarado variant.
	'%          Sintaxis:x.Remove(xyz)
	'%Parámetros:
	'%  vntIndexKey  - Campo clave o índice.
	Public Sub Remove(ByVal vntIndexKey As Object)
		
		If Not IsIDEMode Then
		End If
		
		mCol.Remove(vntIndexKey)
		
		Exit Sub
	End Sub
	
	'**%Objective: Creates the collection when this class is created
	'%Objetivo: Crea la colección cuando la clase es creada.
	Private Sub Class_Initialize_Renamed()
		If Not IsIDEMode Then
		End If
		
		mCol = New Collection
		
		Exit Sub
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
	
	'**%Objective: Destruction of an instance of the collection
	'%Objetivo: Elimina la instancia de la colección
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
	
	'**%Objective: load the records from the LettParams table filtring by nLetterNum
	'**%Parameters:
	'**%  nLetterNum - Number identifying the letter template.
	'**%  dEffecdate - Date which from the record is valid.
	'%Objetivo: Carga los registros de la table LettParams filtrados por el campo nLettNum
	'%Parámetros:
	'%  nLetterNum   - Código del modelo de carta.
	'%  dEffecdate   - Fecha de efecto del registro.
	Public Function FindByLetter(ByVal nLetterNum As Short, ByVal dEffecDate As Date) As Boolean
		Dim lrecreaLettParamsByLettNum As eRemoteDB.Execute
		
		If Not IsIDEMode Then
		End If
		
		lrecreaLettParamsByLettNum = New eRemoteDB.Execute
		
		With lrecreaLettParamsByLettNum
			.StoredProcedure = "reaLettParamsByLettNum"
			.Parameters.Add("nLetterNum", nLetterNum, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecDate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				FindByLetter = True
				While Not .EOF
					Add(.FieldToClass("nLetterNum", intNull), .FieldToClass("nParameters", intNull), dtmNull, dtmNull, dtmNull, intNull, .FieldToClass("sDescript", String.Empty))
					.RNext()
				End While
				.RCloseRec()
			Else
				FindByLetter = False
			End If
		End With
		lrecreaLettParamsByLettNum = Nothing
		
		Exit Function
		lrecreaLettParamsByLettNum = Nothing
	End Function
End Class











