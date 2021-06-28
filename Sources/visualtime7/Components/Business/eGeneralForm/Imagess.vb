Option Strict Off
Option Explicit On
Public Class Imagess
	Implements System.Collections.IEnumerable
	
	
	' Local variable to hold collection
	Private mCol As Collection
	
	'- Se define la variable auxiliar para evitar una búsqueda innecesaria y
	'- almacenar temporalmente el número de Imagen
	
	Public mAuxImagenum As Integer
	
	'% Add: Este método se encarga de agregar nuevos registros a la tabla "Images". Devolviendo verdadero o
	'% falso dependiendo de si el Stored procedure se ejecutó correctamente.
	Public Function Add(ByVal nStatusInstance As Integer, ByVal nImagenum As Integer, ByVal nConsec As Integer, ByVal sDescript As String, ByVal dCompdate As Date, ByVal dNulldate As Date, ByVal nRectype As Integer, ByVal nUsercode As Integer, ByVal sCliename As String, ByVal sSource As String) As eGeneralForm.Images
		' Create a new object
		Dim objNewMember As Images
		objNewMember = New Images
		
		' Set the properties passed into the method
		With objNewMember
			.nStatusInstance = nStatusInstance
			.nImagenum = nImagenum
			.nConsec = nConsec
			.sDescript = sDescript
			.dCompdate = dCompdate
			.dNulldate = dNulldate
			.nRectype = nRectype
			.nUsercode = nUsercode
			.sCliename = sCliename
			.sSource = sSource
			.nOldImagenum = nImagenum
		End With
		
		mCol.Add(objNewMember, "I" & nImagenum & nConsec)
		
		' Return the object created
		Add = objNewMember
		'UPGRADE_NOTE: Object objNewMember may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		objNewMember = Nothing
	End Function
	
	'% Find: Este metodo retorna VERDADERO o FALSO dependiendo de la existencia o no de registros en la
	'% tabla "Images"
	Public Function Find(ByVal Imagenum As Integer) As Boolean
		Dim lrecreaImages As eRemoteDB.Execute
		
		lrecreaImages = New eRemoteDB.Execute
		
		On Error GoTo Find_err
		
		If mAuxImagenum = Imagenum Then
			Find = True
		Else
			
			'+ Definición de parámetros para stored procedure 'insudb.reaNotes'
			'+ Información leída el 07/06/2000 03:57:36 PM
			
			With lrecreaImages
				.StoredProcedure = "reaImages"
				.Parameters.Add("nImagenum", Imagenum, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				If .Run Then
					Do While Not .EOF
						Call Add(1, .FieldToClass("nImagenum", numNull), .FieldToClass("nConsec", numNull), .FieldToClass("sDescript", strNull), .FieldToClass("dCompdate", dtmNull), .FieldToClass("dNulldate", dtmNull), .FieldToClass("nRectype", numNull), .FieldToClass("nUsercode", numNull), .FieldToClass("sCliename", strNull), String.Empty)
						.RNext()
					Loop 
					
					.RCloseRec()
					Find = True
					mAuxImagenum = Imagenum
				Else
					Find = False
				End If
			End With
			'UPGRADE_NOTE: Object lrecreaImages may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
			lrecreaImages = Nothing
		End If
		
Find_err: 
		If Err.Number Then
			Find = False
		End If
		On Error GoTo 0
	End Function
	
	'% Update: Este método se encarga de actualizar registros en la tabla "Images". Devolviendo verdadero o
	'% falso dependiendo de si el Stored procedure se ejecutó correctamente.
	Public Function Update() As Boolean
		Dim lclsImages As Images
		
		Update = True
		
		On Error GoTo Update_err
		
		For	Each lclsImages In mCol
			With lclsImages
				Select Case .nStatusInstance
					
					'+ Si la acción es Agregar
					Case 0
						.nImagenum = mAuxImagenum
						
						Update = .Add()
						
						'+ Si el número de la imagen ha cambiado, se elimina de la colección para volverla a crear
						'+ con el nuevo número
						mCol.Remove(("I" & .nOldImagenum & .nConsec))
						
						mAuxImagenum = .nImagenum
						
						Call Add(1, .nImagenum, .nConsec, .sDescript, .dCompdate, .dNulldate, .nRectype, .nUsercode, .sCliename, .sSource)
						
						.nStatusInstance = 1
						
						'+ Si la acción es Actualizar
					Case 2
						Update = .Update()
						
						'+ Si la acción es Eliminar
					Case 3
						Update = .Delete
						mCol.Remove(("I" & .nImagenum & .nConsec))
						
				End Select
			End With
		Next lclsImages
		
		If mCol.Count() = 0 Then
			mAuxImagenum = 0
		End If
		
Update_err: 
		If Err.Number Then
			Update = False
		End If
		On Error GoTo 0
	End Function
	
	'*Item: Devuelve un elemento de la colección (segun índice)
	Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As Images
		Get
			' Used when referencing an element in the collection.
			' vntIndexKey contains either the Index or Key to the collection,
			' this is why it is declared as a Variant
			' Syntax: Set foo = x.Item(xyz) or Set foo = x.Item(5)
			
			Item = mCol.Item(vntIndexKey)
		End Get
	End Property
	
	'*Count: Devuelve el número de elementos que posee la colección
	Public ReadOnly Property Count() As Integer
		Get
			' Used when retrieving the number of elements in the collection.
			' Syntax: Debug.Print x.Count
			
			Count = mCol.Count()
		End Get
	End Property
	
	'**% NewEnum: Enumerates the collection for use in a For Each...Next loop
	'% NewEnum: Permite enumerar la colección para utilizarla en un ciclo For Each... Next
	'UPGRADE_NOTE: NewEnum property was commented out. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="B3FC1610-34F3-43F5-86B7-16C984F0E88E"'
	'Public ReadOnly Property NewEnum() As stdole.IUnknown
		'Get
			'This property allows you to enumerate this collection with the For...Each syntax
			'
			'NewEnum = mCol._NewEnum
		'End Get
	'End Property
	
	Public Function GetEnumerator() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
		'UPGRADE_TODO: Uncomment and change the following line to return the collection enumerator. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="95F9AAD0-1319-4921-95F0-B9D3C4FF7F1C"'
		GetEnumerator = mCol.GetEnumerator
	End Function
	
	'%Remove: Elimina un elemento de la colección
	Public Sub Remove(ByRef vntIndexKey As Object)
		' Used when removing an element from the collection.
		' vntIndexKey contains either the Index or Key, which is why
		' it is declared as a Variant
		' Syntax: x.Remove(xyz)
		
		mCol.Remove(vntIndexKey)
	End Sub
	
	'**%Class_Initialize: Controls the creation of an instance of the collection
	'%Class_Initialize: Controla la creación de una instancia de la colección
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		' Creates the collection when this class is created
		
		mCol = New Collection
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
	
	'%Class_Terminate: Controla la destrucción de una instancia de la colección
	'UPGRADE_NOTE: Class_Terminate was upgraded to Class_Terminate_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Terminate_Renamed()
		' Destroys collection when this class is terminated
		
		'UPGRADE_NOTE: Object mCol may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		mCol = Nothing
	End Sub
	Protected Overrides Sub Finalize()
		Class_Terminate_Renamed()
		MyBase.Finalize()
	End Sub
End Class






