Option Strict Off
Option Explicit On
Public Class Ctrol_dates
	Implements System.Collections.IEnumerable
	'- variable local para contener colección
	Private mCol As Collection
	
	'Public Function Add(ByVal objClass As Ctrol_date) As Ctrol_date
	
	Public Function Add(ByVal nType_proce As Integer, ByVal dEffecdate As Date, Optional ByRef sDescript As Object = Nothing) As Ctrol_date
		'+ crear un nuevo objeto
		Dim objNewMember As Ctrol_date
		objNewMember = New Ctrol_date
		
		With objNewMember
			.nType_proce = nType_proce
			.dEffecdate = dEffecdate
			.sDescript = sDescript
		End With
		
		mCol.Add(objNewMember)
		
		'+ devolver el objeto creado
		Add = objNewMember
		'UPGRADE_NOTE: Object objNewMember may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		objNewMember = Nothing
		
	End Function
	
	Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As Ctrol_date
		Get
			'+ se usa al hacer referencia a un elemento de la colección
			'+ vntIndexKey contiene el índice o la clave de la colección,
			'+ por lo que se declara como un Variant
			'+ Syntax: Set foo = x.Item(xyz) or Set foo = x.Item(5)
			Item = mCol.Item(vntIndexKey)
			
		End Get
	End Property
	
	Public ReadOnly Property Count() As Integer
		Get
			'+ se usa al obtener el número de elementos de la
			'+ colección. Sintaxis: Debug.Print x.Count
			Count = mCol.Count()
			
		End Get
	End Property
	
	'UPGRADE_NOTE: NewEnum property was commented out. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="B3FC1610-34F3-43F5-86B7-16C984F0E88E"'
	'Public ReadOnly Property NewEnum() As stdole.IUnknown
		'Get
			'+ esta propiedad permite enumerar
			'+ esta colección con la sintaxis For...Each
			'NewEnum = mCol._NewEnum
			'
		'End Get
	'End Property
	
	Public Function GetEnumerator() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
		'UPGRADE_TODO: Uncomment and change the following line to return the collection enumerator. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="95F9AAD0-1319-4921-95F0-B9D3C4FF7F1C"'
		GetEnumerator = mCol.GetEnumerator
	End Function
	
	Public Sub Remove(ByRef vntIndexKey As Object)
		'+ se usa al quitar un elemento de la colección
		'+ vntIndexKey contiene el índice o la clave, por lo que se
		'+ declara como un Variant
		'+ Sintaxis: x.Remove(xyz)
		
		mCol.Remove(vntIndexKey)
	End Sub
	
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		'+ crea la colección cuando se crea la clase
		mCol = New Collection
		
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
	
	'UPGRADE_NOTE: Class_Terminate was upgraded to Class_Terminate_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Terminate_Renamed()
		'+ destruye la colección cuando se termina la clase
		'UPGRADE_NOTE: Object mCol may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		mCol = Nothing
		
	End Sub
	Protected Overrides Sub Finalize()
		Class_Terminate_Renamed()
		MyBase.Finalize()
	End Sub
	
	'%Find: select from the numerator table the record based on a condition and an indicator
	'%Find: Seleciona de la tabla numerator los Registros Basados en una Condsion y un Indicador
	Public Function Find() As Boolean
		Dim lrecreaCtrol_date_a As eRemoteDB.Execute
		
		lrecreaCtrol_date_a = New eRemoteDB.Execute
		
		'+ Definición de parámetros para stored procedure 'insudb.reaCtrol_date_a'
		With lrecreaCtrol_date_a
			.StoredProcedure = "reaCtrol_date_a"
			If .Run Then
				While Not .EOF
					'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
                    Call Add(.FieldToClass("nType_proce"), .FieldToClass("dEffecdate"), vbNullString)
					.RNext()
				End While
				Find = True
				.RCloseRec()
			End If
		End With
		
		'UPGRADE_NOTE: Object lrecreaCtrol_date_a may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaCtrol_date_a = Nothing
	End Function
	
	Public Function insPreCP8000() As Boolean
		Dim lclsCtrolDate As Ctrol_date
		Dim lrecCtrolDate As eRemoteDB.Execute
		On Error GoTo insPreCP8000_Err
		
		lrecCtrolDate = New eRemoteDB.Execute
		insPreCP8000 = True
		
		With lrecCtrolDate
			.StoredProcedure = "InsPreCP8000"
			If .Run(True) Then
				Do While Not .EOF
					lclsCtrolDate = New Ctrol_date
					lclsCtrolDate.nType_proce = .FieldToClass("nType_Proce")
					lclsCtrolDate.sDescript = .FieldToClass("sDescript")
					lclsCtrolDate.dEffecdate = .FieldToClass("dEffecdate")
					
					Call Add(.FieldToClass("nType_proce"), .FieldToClass("dEffecdate"), .FieldToClass("sDescript"))
					'UPGRADE_NOTE: Object lclsCtrolDate may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
					lclsCtrolDate = Nothing
					.RNext()
				Loop 
				insPreCP8000 = True
				.RCloseRec()
			End If
		End With
insPreCP8000_Err: 
		If Err.Number Then
			insPreCP8000 = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecCtrolDate may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecCtrolDate = Nothing
	End Function
End Class






