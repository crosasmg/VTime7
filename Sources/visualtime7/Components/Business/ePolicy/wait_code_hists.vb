Option Strict Off
Option Explicit On
Public Class wait_code_hists
	Implements System.Collections.IEnumerable
	'**- Local variable to hold collection
	Private mCol As Collection
	
	'**%Find: Function that returns TRUE in case of finding in the data base the records
	'**% associated with the key that supplies and fill the public variables with the values.
	'% Find: Función que retorna VERDADERO en caso de encontrar en la base de datos los registros
	'% asociados con la llave que se le suministra y llena las variables públicas con los valores encontrados.
	Public Function Find(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double) As Boolean
		Dim lrecreaWait_Code_Hist As eRemoteDB.Execute
		
		On Error GoTo ProtElemetss_Find_Err
		
		lrecreaWait_Code_Hist = New eRemoteDB.Execute
		
		With lrecreaWait_Code_Hist
			.StoredProcedure = "REAWAIT_CODE_HIST"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run() Then
				Do While Not .EOF
					Call Add(.FieldToClass("sCertype"), .FieldToClass("nBranch"), .FieldToClass("nProduct"), .FieldToClass("nPolicy"), .FieldToClass("nCertif"), .FieldToClass("nWait_code"), .FieldToClass("nSeq"), .FieldToClass("dEffecdate"), .FieldToClass("dCompdate"), .FieldToClass("nUsercode"), .FieldToClass("Ramo"), .FieldToClass("Producto"), .FieldToClass("Causal"), .FieldToClass("Usuario"))
					.RNext()
				Loop 
				.RCloseRec()
				Find = True
			Else
				Find = False
			End If
		End With
		'UPGRADE_NOTE: Object lrecreaWait_Code_Hist may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaWait_Code_Hist = Nothing
		
ProtElemetss_Find_Err: 
		If Err.Number Then
			Find = False
		End If
		On Error GoTo 0
	End Function
	
	'**% Add: Function that adds to the class the respective values of each element that integrates it.
	'% Add: Función que añade a la clase los valores respectivos a cada elemento que le integra
	Public Function Add(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal nWait_code As Integer, ByVal nSeq As Double, ByVal dEffecdate As Date, ByVal sCompdate As String, ByVal nUsercode As Integer, ByVal sRamo As String, ByVal sProducto As String, ByVal sCausal As String, ByVal sUsuario As String) As wait_code_hist
		'**+ Creates an instance of the class.
		'+ Crea una instancia de la clase
		Dim objNewMember As wait_code_hist
		
		On Error GoTo ProtElementssAdd_Err
		
		objNewMember = New wait_code_hist
		With objNewMember
			.sCertype = sCertype
			.nBranch = nBranch
			.nProduct = nProduct
			.nPolicy = nPolicy
			.nCertif = nCertif
			.nWait_code = nWait_code
			.dEffecdate = dEffecdate
			.sCompdate = sCompdate
			.nSeq = nSeq
			.nUsercode = nUsercode
			.sRamo = sRamo
			.sProducto = sProducto
			.sCausal = sCausal
			.sUsuario = sUsuario
			
			
			
		End With
		
		mCol.Add(objNewMember, "EP" & sCertype & nBranch & nProduct & nPolicy & nCertif & nWait_code & nSeq)
		
		'**+ Return the created object.
		'+ Retorna el objeto creado
		Add = objNewMember
		'UPGRADE_NOTE: Object objNewMember may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		objNewMember = Nothing
		
ProtElementssAdd_Err: 
		If Err.Number Then
            Add = Nothing
		End If
		On Error GoTo 0
	End Function
	
	'**% Item: Restores an element of the collection (according to index)
	'% Item: Devuelve un elemento de la colección (segun índice)
	Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As wait_code_hist
		Get
			Item = mCol.Item(vntIndexKey)
		End Get
	End Property
	
	'**% Count: Restores the number of elements that the collection owns.
	'% Count: Devuelve el número de elementos que posee la colección
	Public ReadOnly Property Count() As Integer
		Get
			Count = mCol.Count()
		End Get
	End Property
	
	'**% NewEnum: enumerate the collection for using it in a cycle For Each...Next
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
	
	'**% Remove: Removes an element from the collection.
	'% Remove: Elimina un elemento de la colección
	Public Sub Remove(ByRef vntIndexKey As Object)
		mCol.Remove(vntIndexKey)
	End Sub
	
	'**% Class_Initialize: controls the creation of an instance of the collection.
	'% Class_Initialize: Controla la creación de una instancia de la colección
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		mCol = New Collection
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
	
	'**% Class_Terminate: controls the destruction of an instance of the collection.
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
End Class






