Option Strict Off
Option Explicit On
Public Class Life_specis
	Implements System.Collections.IEnumerable
	'%-------------------------------------------------------%'
	'% $Workfile:: Life_specis.cls                          $%'
	'% $Author:: Ljimenez                                   $%'
	'% $Date:: 25-09-09 23:49                               $%'
	'% $Revision:: 2                                        $%'
	'%-------------------------------------------------------%'
	
	'+ Variable local para contener colección.
	
	Private mCol As Collection
	
	'+ Se definen las propiedades auxiliares.
	
	Private mintBranch As Integer
	Private mintProduct As Integer
	Private mdtmEffecdate As Date
	Public nCurrencyAux As Integer
	
	'% AddLife_speci: Este método permite añadir registros a la colección.
	Public Function AddLife_speci(ByRef nAgeEnd As Integer, ByRef nAgeStart As Integer, ByRef nCapEnd As Double, ByRef nConsec As Double, ByRef nCapStart As Double, ByRef nBranch As Integer, ByRef nCurrency As Integer, ByRef nProduct As Integer, ByRef dEffecdate As Date, ByRef nCrthecni As Integer, ByRef dNulldate As Date, ByRef sSexInsur As String, ByRef nUsercode As Integer, ByRef sDesCurrency As String, ByRef sDesCrite As String, ByRef nModulec As Integer, ByRef nCover As Integer, ByRef nRole As Integer) As Life_speci
		'+ Crear un nuevo objeto.
		Dim objNewMember As Life_speci
		
		'+ Establecer las propiedades que se transfieren al método.
		objNewMember = New Life_speci
		With objNewMember
			.nUsercode = nUsercode
			.sSexInsur = sSexInsur
			.dNulldate = dNulldate
			.nCrthecni = nCrthecni
			.dEffecdate = dEffecdate
			.nProduct = nProduct
			.nCurrency = nCurrency
			.nBranch = nBranch
			.nCapStart = nCapStart
			.nConsec = nConsec
			.nCapEnd = nCapEnd
			.nAgeStart = nAgeStart
			.nAgeEnd = nAgeEnd
			.sDesCurrency = sDesCurrency
			.sDesCrite = sDesCrite
			.nModulec = nModulec
			.nCover = nCover
			.nRole = nRole
		End With
		
		mCol.Add(objNewMember, "A" & CStr(nBranch) & CStr(nProduct) & CStr(nConsec) & CStr(dEffecdate))
		
		'+ Return the object created.
		
		AddLife_speci = objNewMember
		'UPGRADE_NOTE: Object objNewMember may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		objNewMember = Nothing
	End Function
	
	'%Item: Devuelve un elemento de la colección (segun índice)
	Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As Life_speci
		Get
			Item = mCol.Item(vntIndexKey)
		End Get
	End Property
	
	'%Count: Devuelve el número de elementos que posee la colección
	Public ReadOnly Property Count() As Integer
		Get
			Count = mCol.Count()
		End Get
	End Property
	
	'%NewEnum: Permite enumerar la colección para utilizarla en un ciclo For Each... Next
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
	
	'%Remove: Elimina un elemento de la colección
	Public Sub Remove(ByRef vntIndexKey As Object)
		mCol.Remove(vntIndexKey)
	End Sub
	
	'%Class_Initialize: Controla la creación de una instancia de la colección
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		mCol = New Collection
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
	
	'%Class_Terminate: Controla la destrucción de una instancia de la colección
	'UPGRADE_NOTE: Class_Terminate was upgraded to Class_Terminate_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Terminate_Renamed()
		'UPGRADE_NOTE: Object mCol may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		mCol = Nothing
	End Sub
	Protected Overrides Sub Finalize()
		Class_Terminate_Renamed()
		MyBase.Finalize()
	End Sub
	
	'% FindLife_speci: Verifica que exista información en la tabla de conmutativos.
	Public Function FindLife_speci(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal dEffecdate As Date, ByVal nModulec As Integer, ByVal nCover As Integer, Optional ByVal lblnFind As Boolean = True) As Boolean
		Dim lrecReaLife_speci As eRemoteDB.Execute
		
		lrecReaLife_speci = New eRemoteDB.Execute
		
		On Error GoTo FindLife_speci_Err
		
		FindLife_speci = True
		
		If nBranch <> mintBranch Or nProduct <> mintProduct Or dEffecdate <> mdtmEffecdate Or lblnFind Then
			'UPGRADE_NOTE: Object mCol may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
			mCol = Nothing
			mCol = New Collection
			
			'+ Definición de parámetros para stored procedure 'insudb.reaConmutativ'.
			
			With lrecReaLife_speci
				.StoredProcedure = "reaLife_speci"
				.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nModulec", nModulec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nCover", nCover, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				
				If .Run Then
					mintBranch = nBranch
					mintProduct = nProduct
					mdtmEffecdate = dEffecdate
					nCurrencyAux = .FieldToClass("nCurrency")
					
					Do While Not .EOF
						Call AddLife_speci(.FieldToClass("nAgeend"), .FieldToClass("nAgestart"), .FieldToClass("nCapend"), .FieldToClass("nConsec"), .FieldToClass("nCapstart"), .FieldToClass("nBranch"), .FieldToClass("nCurrency"), .FieldToClass("nProduct"), .FieldToClass("dEffecdate"), .FieldToClass("nCrthecni"), .FieldToClass("dNulldate"), .FieldToClass("sSexinsur"), .FieldToClass("nUsercode"), .FieldToClass("sDesCurrency"), .FieldToClass("sDesCrite"), .FieldToClass("nModulec"), .FieldToClass("nCover"), .FieldToClass("nRole"))
						.RNext()
					Loop 
					.RCloseRec()
				Else
					FindLife_speci = False
					mintBranch = 0
					mintProduct = 0
					mdtmEffecdate = CDate(Nothing)
					nCurrencyAux = eRemoteDB.Constants.intNull
				End If
			End With
			
			'UPGRADE_NOTE: Object lrecReaLife_speci may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
			lrecReaLife_speci = Nothing
		End If
		
FindLife_speci_Err: 
		If Err.Number Then
			FindLife_speci = False
		End If
		
		On Error GoTo 0
	End Function
End Class






