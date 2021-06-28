Option Strict Off
Option Explicit On
Public Class Tab_winclas
	Implements System.Collections.IEnumerable
	'variable local para contener colección
	Private mCol As Collection
	
	Public Function Add(ByVal objClass As Tab_wincla) As Tab_wincla
		'crear un nuevo objeto
		If objClass Is Nothing Then
			objClass = New Tab_wincla
		End If
		
		With objClass
			mCol.Add(objClass, .nTraTypec & .sBrancht & .sBussityp & .nSequence & .nIndex)
		End With
		
		'Return the object created
		Add = objClass
	End Function
	
	Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As Tab_wincla
		Get
			'se usa al hacer referencia a un elemento de la colección
			'vntIndexKey contiene el índice o la clave de la colección,
			'por lo que se declara como un Variant
			'Syntax: Set foo = x.Item(xyz) or Set foo = x.Item(5)
			Item = mCol.Item(vntIndexKey)
		End Get
	End Property
	
	
	
	Public ReadOnly Property Count() As Integer
		Get
			'se usa al obtener el número de elementos de la
			'colección. Sintaxis: Debug.Print x.Count
			Count = mCol.Count()
		End Get
	End Property
	
	
	'UPGRADE_NOTE: NewEnum property was commented out. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="B3FC1610-34F3-43F5-86B7-16C984F0E88E"'
	'Public ReadOnly Property NewEnum() As stdole.IUnknown
		'Get
			'esta propiedad permite enumerar
			'esta colección con la sintaxis For...Each
			'NewEnum = mCol._NewEnum
		'End Get
	'End Property
	
	Public Function GetEnumerator() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
				GetEnumerator = mCol.GetEnumerator
	End Function
	
	
	Public Sub Remove(ByRef vntIndexKey As Object)
		'se usa al quitar un elemento de la colección
		'vntIndexKey contiene el índice o la clave, por lo que se
		'declara como un Variant
		'Sintaxis: x.Remove(xyz)
		
		
		mCol.Remove(vntIndexKey)
	End Sub
	
	
	Private Sub Class_Initialize_Renamed()
		'crea la colección cuando se crea la clase
		mCol = New Collection
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
	
	
	Private Sub Class_Terminate_Renamed()
		'destruye la colección cuando se termina la clase
		mCol = Nothing
	End Sub
	Protected Overrides Sub Finalize()
		Class_Terminate_Renamed()
		MyBase.Finalize()
	End Sub
	
	'% Find: Busca las ventanas asociadas a la secuencia de clientes
	Public Function Find(ByVal nTraTypec As Integer, ByVal sBrancht As String, ByVal sBussityp As String) As Boolean
		
		Dim lrecreaTab_wincla_a As eRemoteDB.Execute
		Dim lclsTab_wincla As Tab_wincla
		Dim lintIndex As Integer
		
		On Error GoTo reaTab_wincla_a_Err
		
		lrecreaTab_wincla_a = New eRemoteDB.Execute
		
		'+ Definición de store procedure reaTab_wincli_a al 03-23-2002 12:56:38
		With lrecreaTab_wincla_a
			.StoredProcedure = "reaTab_wincla_a"
			.Parameters.Add("nTraTypec", nTraTypec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sBrancht", sBrancht, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sBussityp", sBussityp, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run Then
				Find = True
				Do While Not .EOF
					lclsTab_wincla = New Tab_wincla
					lclsTab_wincla.nTraTypec = nTraTypec
					lclsTab_wincla.sBrancht = sBrancht
					lclsTab_wincla.sBussityp = sBussityp
					lclsTab_wincla.sExist = .FieldToClass("sExist")
					lclsTab_wincla.sCodispl = .FieldToClass("sCodispl")
					lclsTab_wincla.sDescript = .FieldToClass("sDescript")
					lclsTab_wincla.nSequence = .FieldToClass("nSequence")
					lclsTab_wincla.sDefaulti = .FieldToClass("sDefaulti")
					lclsTab_wincla.sRequire = .FieldToClass("sRequire")
					lclsTab_wincla.nIndex = lintIndex
					Call Add(lclsTab_wincla)
					lclsTab_wincla = Nothing
					lintIndex = lintIndex + 1
					.RNext()
				Loop 
				.RCloseRec()
			Else
				Find = False
			End If
		End With
		
reaTab_wincla_a_Err: 
		If Err.Number Then
			Find = False
		End If
		lrecreaTab_wincla_a = Nothing
		On Error GoTo 0
	End Function
	
	'% Update: recorre la colección y actualiza los datos en la tabla
	Public Function Update() As Boolean
		Dim lclsTab_wincla As Tab_wincla
		Update = True
		For	Each lclsTab_wincla In mCol
			With lclsTab_wincla
				Update = .Update()
			End With
		Next lclsTab_wincla
	End Function
End Class






