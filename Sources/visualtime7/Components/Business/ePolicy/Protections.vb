Option Strict Off
Option Explicit On
Public Class Protections
	Implements System.Collections.IEnumerable
	'**- Local variable to hold collection
	Private mCol As Collection
	
	'**%Find: Function that returns TRUE in case of finding in the data base the records
	'**% associated with the key that supplies and fill the public variables with the values.
	'% Find: Función que retorna VERDADERO en caso de encontrar en la base de datos los registros
	'% asociados con la llave que se le suministra y llena las variables públicas con los valores encontrados.
	Public Function Find(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date) As Boolean
		Dim lrecreaTab_Protec_1 As eRemoteDB.Execute
		
		On Error GoTo ProtElemetss_Find_Err
		
		lrecreaTab_Protec_1 = New eRemoteDB.Execute
		
		'**+ Paramter definition for stored procedure 'insudb.reaTab_Protec_1'
		'+Definición de parámetros para stored procedure 'insudb.reaTab_Protec_1'
		'**+ Information read on November 16,2000  09:55:42 a.m.
		'+Información leída el 11/16/2000 9:55:42 AM
		With lrecreaTab_Protec_1
			.StoredProcedure = "reaTab_Protec_1"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run() Then
				Do While Not .EOF
					'+ En caso que el Stored Procedure devuelva en el campo "selection" un valor distinto
					'+ a NULO (String.Empty o "") se actualizan los valores de la clase con los campos:
					'+ "PnCurrency", "PnDiscount" y "PnDisRate" en lugar de "nCurrency", "nDiscount" y "nDisRate"
					If .FieldToClass("selection") <> String.Empty Then
						Call Add(.FieldToClass("nBranch"), .FieldToClass("nProduct"), .FieldToClass("nElement"), .FieldToClass("dEffecdate"), .FieldToClass("nCurrency"), .FieldToClass("nDiscount"), .FieldToClass("dCompdate"), .FieldToClass("nDisRate"), .FieldToClass("nMaxamount"), .FieldToClass("nMinamount"), .FieldToClass("dNulldate"), .FieldToClass("nUsercode"), .FieldToClass("sDescript"), .FieldToClass("selection"), .FieldToClass("PnDiscount"), .FieldToClass("PnDisrate"), .FieldToClass("PnCurrency"))
					Else
						Call Add(.FieldToClass("nBranch"), .FieldToClass("nProduct"), .FieldToClass("nElement"), .FieldToClass("dEffecdate"), .FieldToClass("nCurrency"), .FieldToClass("nDiscount"), .FieldToClass("dCompdate"), .FieldToClass("nDisRate"), .FieldToClass("nMaxamount"), .FieldToClass("nMinamount"), .FieldToClass("dNulldate"), .FieldToClass("nUsercode"), .FieldToClass("sDescript"), .FieldToClass("selection"))
					End If
					.RNext()
				Loop 
				.RCloseRec()
				Find = True
			Else
				Find = False
			End If
		End With
		'UPGRADE_NOTE: Object lrecreaTab_Protec_1 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaTab_Protec_1 = Nothing
		
ProtElemetss_Find_Err: 
		If Err.Number Then
			Find = False
		End If
		On Error GoTo 0
	End Function
	
	'**% Add: Function that adds to the class the respective values of each element that integrates it.
	'% Add: Función que añade a la clase los valores respectivos a cada elemento que le integra
	Public Function Add(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nElement As Integer, ByVal dEffecdate As Date, ByVal nCurrency As Integer, ByVal nDiscount As Double, ByVal dCompdate As Date, ByVal nDisRate As Double, ByVal nMaxamount As Double, ByVal nMinamount As Double, ByVal dNulldate As Date, ByVal nUsercode As Integer, ByVal sDescript As String, ByVal sSelection As String, Optional ByVal PnDiscount As Double = 0, Optional ByVal PnDisrate As Double = 0, Optional ByVal PnCurrency As Integer = 0) As Protection
        Dim nCertif As Object = New Object
        Dim nPolicy As Object = New Object
        Dim sCertype As Object = New Object
        '**+ Creates an instance of the class.
        '+ Crea una instancia de la clase
        Dim objNewMember As Protection
		
		On Error GoTo ProtElementssAdd_Err
		
		objNewMember = New Protection
		With objNewMember
			.sCertype = sCertype
			.nBranch = nBranch
			.nProduct = nProduct
			.nPolicy = nPolicy
			.nCertif = nCertif
			.nElement = nElement
			.dEffecdate = dEffecdate
			.nCurrency = nCurrency
			.nDiscount = nDiscount
			.dCompdate = dCompdate
			.nDisRate = nDisRate
			.nMaxamount = nMaxamount
			.nMinamount = nMinamount
			.dNulldate = dNulldate
			.nUsercode = nUsercode
			.sDescript = sDescript
			.sSelection = sSelection
			.PnDiscount = PnDiscount
			.PnDisrate = PnDisrate
			.PnCurrency = PnCurrency
		End With
		
		mCol.Add(objNewMember, "EP" & sCertype & nBranch & nProduct & nPolicy & nCertif & nElement & dEffecdate)
		
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
	Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As Protection
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






