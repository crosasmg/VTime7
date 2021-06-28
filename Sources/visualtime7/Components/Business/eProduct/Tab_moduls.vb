Option Strict Off
Option Explicit On
Public Class Tab_moduls
	Implements System.Collections.IEnumerable
	'%-------------------------------------------------------%'
	'% $Workfile:: Tab_moduls.cls                           $%'
	'% $Author:: Nvaplat15                                  $%'
	'% $Date:: 10/11/04 15.13                               $%'
	'% $Revision:: 15                                       $%'
	'%-------------------------------------------------------%'
	
	Private mCol As Collection
	
	'**% Add: add a new instance of the class Tab_modul to the collection
	'% Add: Añade una nueva instancia de la clase Tab_modul a la colección
	Public Function Add(ByRef objClass As Tab_modul) As Tab_modul
		mCol.Add(objClass)
		
		Add = objClass
		'UPGRADE_NOTE: Object objClass may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		objClass = Nothing
	End Function
	
	'**%Find: This method fills the collection with records from the table "Tab_modul1"
	'%Find: Este metodo carga la coleccion de elementos de la tabla "Tab_modul1"
	Public Function Find(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal dEffecdate As Date) As Boolean
		Dim lrecreaTab_modul As eRemoteDB.Execute
		Dim lobjTab_modul As Tab_modul
		
		On Error GoTo Find_Err
		
		lrecreaTab_modul = New eRemoteDB.Execute
		
		With lrecreaTab_modul
			.StoredProcedure = "reaTab_modul1"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sDefaulti", String.Empty, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				Find = True
				Do While Not .EOF
					lobjTab_modul = New Tab_modul
					lobjTab_modul.nBranch = .FieldToClass("nBranch")
					lobjTab_modul.nModulec = .FieldToClass("nModulec")
					lobjTab_modul.nProduct = .FieldToClass("nProduct")
					lobjTab_modul.dEffecdate = .FieldToClass("dEffecdate")
					lobjTab_modul.sChanallo = IIf(.FieldToClass("sChanallo") = "1", "1", "2")
					lobjTab_modul.sDefaulti = IIf(.FieldToClass("sDefaulti") = "1", "1", "2")
					lobjTab_modul.sDescript = .FieldToClass("sDescript")
					lobjTab_modul.sRequire = IIf(.FieldToClass("sRequire") = "1", "1", "2")
					lobjTab_modul.sShort_des = .FieldToClass("sShort_des")
					lobjTab_modul.sCondSVS = .FieldToClass("sCondSVS")
					
					lobjTab_modul.nPremirat = .FieldToClass("npremirat")
					lobjTab_modul.nChPreLev = .FieldToClass("nchprelev")
					lobjTab_modul.nRatePreAdd = .FieldToClass("nratepreadd")
					lobjTab_modul.nRatePreSub = .FieldToClass("nratepresub")
					lobjTab_modul.sChangetyp = .FieldToClass("schangetyp")
					lobjTab_modul.styp_rat = .FieldToClass("styp_rat")
					lobjTab_modul.sVigen = .FieldToClass("sVigen")
					Call Add(lobjTab_modul)
					
					'UPGRADE_NOTE: Object lobjTab_modul may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
					lobjTab_modul = Nothing
					.RNext()
				Loop 
				.RCloseRec()
			Else
				Find = False
			End If
		End With
		
Find_Err: 
		If Err.Number Then
			Find = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecreaTab_modul may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaTab_modul = Nothing
		'UPGRADE_NOTE: Object lobjTab_modul may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lobjTab_modul = Nothing
	End Function
	
	'*** Item: restores an element of the collection (accourding to the index)
	'* Item: Devuelve un elemento de la colección (segun índice)
	Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As Tab_modul
		Get
			Item = mCol.Item(vntIndexKey)
		End Get
	End Property
	
	'*** Count: restores the number of elements that the collection owns
	'* Count: Devuelve el número de elementos que posee la colección
	Public ReadOnly Property Count() As Integer
		Get
			Count = mCol.Count()
		End Get
	End Property
	
	'*** NewEnum: Allows to enumerate the collection for using it in a cycle For Each...Next
	'* NewEnum: Permite enumerar la colección para utilizarla en un ciclo For Each... Next
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
	
	'**% Remove: deletes an element of the collection
	'% Remove: Elimina un elemento de la colección
	Public Sub Remove(ByRef vntIndexKey As Object)
		mCol.Remove(vntIndexKey)
	End Sub
	
	'**% Class_Initialize: controls the creation of an instance of the collection
	'% Class_Initialize: Controla la creación de una instancia de la colección
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		mCol = New Collection
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
	
	'**% Class_Terminate: controls the delete of an instance of of the collection
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






