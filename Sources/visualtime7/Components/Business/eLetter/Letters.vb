Option Strict Off
Option Explicit On
Public Class Letters
	Implements System.Collections.IEnumerable
	'**+Objetive: Clase generada a partir de la tabla 'TAB_LETTERS' al 15/08/2002 03:25:46 p.m.
	'**+Version: $$Revision: 9 $
	'+Objetivo: Clase generada a partir de la tabla 'TAB_LETTERS' al 15/08/2002 03:25:46 p.m.
	'+Version: $$Revision: 9 $
	
	'**-Objective: local variable to hold collection.
	'-Objetivo: Variable Local para almacenar la coleccion.
	Private mCol As Collection
	
	'**%Objective: Adds a new instance of the Letters class to the collection
	'**%Parameters:
	'**%  nUsercode     - Code of the user creating or updating the record.'
	'**%  tLetter       - Content of the letter.
	'**%  dNullDate     - Date of cancellation of the letter.
	'**%  dEffecDate    - Date which from the record is valid.
	'**%  sDescript     - Description of the letter template.
	'**%  nLetterNum    - Number identifying the letter template.
	'**%  nLanguage     - Code of the language in which the data are expressed.Sole values as per table 85
	'**%  sCtroLettInd  - Correspondence Control Indicator.Sole Values: 1 - Affirmative 2 - Negative
	'**%  nMinTimeAns   - Response time.
	'**%  sKey
	'**%  sDelivInvalid - Delivery to invalid address indicator.
	'%Objetivo: Añade una nueva instancia de la clase Letters a la colección.
	'%Parámetros:
	'%    nUsercode      - Código del usuario que crea o actualiza el registro.
	'%    tLetter        - Contenido de la carta.
	'%    dNullDate      - Fecha de anulación de la carta.
	'%    dEffecDate     - Fecha de efecto del registro.
	'%    sDescript      - Descripción del modelo de carta.
	'%    nLetterNum     - Código del modelo de carta.
	'%    nLanguage      - Lenguaje en que se muestra la información del sistema.Valores únicos según tabla 85
	'%    sCtroLettInd   - Indicador de control (seguimiento) de la correspondencia.Valores únicos: 1 - Afirmativo 2 - Negativo
	'%    nMinTimeAns    - Tiempo de respuesta. Tiempo máximo que debe esperar el sistema por una respuesta del destinatario.
	'%    sKey
	'%    sDelivInvalid  - Indicador de envío a direcciones invalidas
	Private Function Add(ByVal nUsercode As Short, ByVal tletter As String, ByVal dNullDate As Date, ByVal dEffecDate As Date, ByVal sDescript As String, ByVal nLetterNum As Short, ByVal nLanguage As Short, ByVal sCtroLettInd As String, ByVal nMinTimeAns As Short, Optional ByVal sKey As String = "", Optional ByVal sDelivInvalid As String = "") As Letter
		Dim objNewMember As Letter
		
		If Not IsIDEMode Then
		End If
		
		objNewMember = New Letter
		
		'**+ Set the properties passed into the method
		'+ Se le asignan los valores encotrados al objeto objNewMember
		With objNewMember
			.nUsercode = nUsercode
			.tletter = tletter
			.dNullDate = dNullDate
			.dEffecDate = dEffecDate
			.sDescript = sDescript
			.nLetterNum = nLetterNum
			.nLanguage = nLanguage
			.sCtroLettInd = sCtroLettInd
			.nMinTimeAns = nMinTimeAns
			.sDelivInvalidInd = sDelivInvalid
		End With
		If Len(sKey) = 0 Then
			mCol.Add(objNewMember)
		Else
			mCol.Add(objNewMember, sKey)
		End If
		
		Add = objNewMember
		objNewMember = Nothing
		
		Exit Function
		objNewMember = Nothing
	End Function
	
	'**%Objective: Used when referencing an element in the collection, vntIndexKey contains either the Index or Key to the collection, this is why it is declared as a Variant Syntax: Set foo = x.Item(xyz) or Set foo = x.Item(5)
	'%Objetivo: Utilizado para hacer referencia a un elemento de la colección, la misma esat indexada por un campo clave su sintaxis: Set foo = x.Item(xyz) or Set foo = x.Item(5)
	Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As Letter
		Get
			
			If Not IsIDEMode Then
			End If
			
			Item = mCol.Item(vntIndexKey)
			
            Exit Property
		End Get
	End Property
	
	'**%Objective: Used when retrieving the number of elements in the collection. Syntax: Debug.Print x.Count
	'%Objetivo: Utilizado para conocer el numero de elemntos de una colección. Sintaxis: Debug.Print x.Count
	Public ReadOnly Property Count() As Integer
		Get
			
			If Not IsIDEMode Then
			End If
			
			Count = mCol.Count()
			
            Exit Property
		End Get
	End Property
	
	'**%Objective: Allows to enumerate the collection for using it in a cycle For Each...Next
	'%Objetivo: Permite enumerar la colección para utilizarla en un ciclo For Each... Next
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
			'ProcError("Letters.NewEnum()")
		'End Get
	'End Property
	
	Public Function GetEnumerator() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
				GetEnumerator = mCol.GetEnumerator
	End Function
	
	'**%Objective: Used when removing an element from the collection vntIndexKey contains either the Index or Key, which is why it is declared as a Variant Syntax: x.Remove(xyz)
	'%Objetivo: Utilizado para eliminar un elemento de la colección, dependiendo de su llave calve, su sintaxis: x.Remove(xyz)
	Private Sub Remove(ByVal vntIndexKey As Object)
		
		If Not IsIDEMode Then
		End If
		
		mCol.Remove(vntIndexKey)
		
		Exit Sub
	End Sub
	
	'**%Objective: Returns the valid letters to the date
	'**%Parameters:
	'**%  dEffecDate - Date which from the record is valid.
	'%Objetivo: Devuelve las cartas válidas a la fecha
	'%Parámetros:
	'%    dEffecDate - Fecha de efecto del registro.
	Public Function Find(ByVal dEffecDate As Date) As Boolean
		Dim lrecreaTab_Letters As eRemoteDB.Execute
		
		If Not IsIDEMode Then
		End If
		
		lrecreaTab_Letters = New eRemoteDB.Execute
		
		With lrecreaTab_Letters
			.StoredProcedure = "reaTab_Letters"
			'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
			.Parameters.Add("nLetterNum", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
			.Parameters.Add("nLanguage", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecDate", dEffecDate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				Do While Not .EOF
					Call Add(0, "[no text]", CDate(Nothing), .FieldToClass("dEffecDate"), .FieldToClass("sDescript"), .FieldToClass("nLetterNum"), .FieldToClass("nLanguage"), .FieldToClass("sCtroLettInd"), .FieldToClass("nMinTimeAns"), "l" & .FieldToClass("nLetterNum") & .FieldToClass("dEffecDate") & .FieldToClass("nLanguage"), .FieldToClass("sDelivInvalid"))
					.RNext()
				Loop 
				.RCloseRec()
				Find = True
			Else
				Find = False
			End If
		End With
		
		Exit Function
		lrecreaTab_Letters = Nothing
	End Function
	
	'**%Objective: Creates the collection when this class is created
	'%Objetivo: Crea la colección cuando la clase es creada
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
	
	'**%Objective: Destroys collection when this class is terminated
	'%Objetivo: Elimina la colección cuando la clase finaliza su ejecución
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
	
	'**%Objective: Returns the valid letters to the date
	'**%Parameters:
	'**%  nLetterNum - Code of the model of letter
	'%Objetivo: Devuelve las cartas válidas a la fecha
	'%Parámetros:
	'%    nLetterNum - Código del modelo de carta
	Public Function FindTab_Letters(ByVal nLetterNum As Short) As Boolean
		Dim lrecreaTab_Letters As eRemoteDB.Execute
		
		If Not IsIDEMode Then
		End If
		
		lrecreaTab_Letters = New eRemoteDB.Execute
		
		With lrecreaTab_Letters
			.StoredProcedure = "reaTab_Letters2"
			.Parameters.Add("nLetterNum", nLetterNum, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				Do While Not .EOF
					Call AddTab_Letters(.FieldToClass("sDescript"), .FieldToClass("sCtroLettInd"), .FieldToClass("nMinTimeAns"), .FieldToClass("sDelivInvalid"))
					.RNext()
				Loop 
				.RCloseRec()
				FindTab_Letters = True
			Else
				FindTab_Letters = False
			End If
		End With
		
		Exit Function
		lrecreaTab_Letters = Nothing
	End Function
	
	'**%Objective: Adds a new instance of the Letters class to the collection
	'**%Parameters:
	'**%  sDescript     - Description of the letter template.
	'**%  sCtroLettInd  - Correspondence Control Indicator.Sole Values: 1 - Affirmative 2 - Negative
	'**%  nMinTimeAns   - Response time.
	'**%  sDelivInvalid - Delivery to invalid address indicator.
	'%Objetivo: Añade una nueva instancia de la clase Letters a la colección.
	'%Parámetros:
	'%    sDescript      - Descripción del modelo de carta.
	'%    sCtroLettInd   - Indicador de control (seguimiento) de la correspondencia.Valores únicos: 1 - Afirmativo 2 - Negativo
	'%    nMinTimeAns    - Tiempo de respuesta. Tiempo máximo que debe esperar el sistema por una respuesta del destinatario.
	'%    sDelivInvalid  - Indicador de envío a direcciones invalidas
	Private Function AddTab_Letters(ByVal sDescript As String, ByVal sCtroLettInd As String, ByVal nMinTimeAns As Short, ByVal sDelivInvalid As String) As Letter
		Dim objNewMember As Letter
		
		If Not IsIDEMode Then
		End If
		
		objNewMember = New Letter
		
		'**+ Set the properties passed into the method
		'+ Se le asignan los valores encotrados al objeto objNewMember
		With objNewMember
			.sDescript = sDescript
			.sCtroLettInd = sCtroLettInd
			.nMinTimeAns = nMinTimeAns
			.sDelivInvalidInd = sDelivInvalid
		End With
		
		mCol.Add(objNewMember)
		
		AddTab_Letters = objNewMember
		objNewMember = Nothing
		
		Exit Function
		objNewMember = Nothing
	End Function
End Class











