Option Strict Off
Option Explicit On
Public Class Det_comgens
	Implements System.Collections.IEnumerable
	'%-------------------------------------------------------%'
	'% $Workfile:: Det_comgens.cls                          $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 1:41p                                $%'
	'% $Revision:: 10                                       $%'
	'%-------------------------------------------------------%'
	
	Private mCol As Collection
	Private nAuxComtabge As Integer
	Private dAuxEffecdate As Date
	
	'**% Add: Adds a new instance of the Det_Comgen class to the collection
	'% Add: Añade una nueva instancia de la clase Det_comgen a la colección
	Public Function Add(ByVal oDet_comgen As Det_comgen) As Det_comgen
		mCol.Add(oDet_comgen)
		
		Add = oDet_comgen
		'UPGRADE_NOTE: Object oDet_comgen may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		oDet_comgen = Nothing
	End Function
	
	'**% FindMAG003: Restores a collection of objects of Det_comgen type
	'% FindMAG003: Devuelve una coleccion de objetos de tipo Det_comgen
	Public Function Find(ByVal nComtabge As Integer, ByVal dEffecdate As Date, Optional ByVal lblnFind As Boolean = False) As Boolean
		'**- Variable definition lrecDet_comgen that will be used as a cursor
		'- Se define la variable lrecDet_comgen que se utilizará como cursor.
		Dim lrecDet_comgen As eRemoteDB.Execute
		Dim lclsDet_comgen As Det_comgen
		
		On Error GoTo Find_Err
		
		lrecDet_comgen = New eRemoteDB.Execute
		
		If nAuxComtabge = nComtabge And dAuxEffecdate = dEffecdate And Not lblnFind Then
			Find = True
		Else
			
			'**+ Execute the store procedure that searches an intermediary's movements
			'+ Se ejecuta el store procedure que busca los movimientos de un intermediario
			
			With lrecDet_comgen
				.StoredProcedure = "reaDet_comgen_a"
				.Parameters.Add("nComtabge", nComtabge, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				If Not .Run Then
					Find = False
					nAuxComtabge = eRemoteDB.Constants.intNull
					dAuxEffecdate = dtmNull
				Else
					nAuxComtabge = nComtabge
					dAuxEffecdate = dEffecdate
					Do While Not .EOF
						lclsDet_comgen = New Det_comgen
						lclsDet_comgen.nComtabge = nComtabge
						lclsDet_comgen.dEffecdate = dEffecdate
						lclsDet_comgen.nBranch = .FieldToClass("nBranch")
						lclsDet_comgen.nProduct = .FieldToClass("nProduct")
						lclsDet_comgen.nCover = .FieldToClass("nCover")
						lclsDet_comgen.nWay_Pay = .FieldToClass("nWay_Pay")
						lclsDet_comgen.nModulec = .FieldToClass("nModulec")
						lclsDet_comgen.nCurrency = .FieldToClass("nCurrency")
						lclsDet_comgen.nAmount = .FieldToClass("nAmount")
						lclsDet_comgen.nInit_Month = .FieldToClass("nInit_Month")
						lclsDet_comgen.nFinal_Month = .FieldToClass("nFinal_Month")
						lclsDet_comgen.nPercent = .FieldToClass("nPercent")
						lclsDet_comgen.nDuration = .FieldToClass("nDuration")
						lclsDet_comgen.nInstallments = .FieldToClass("nInstallments")
						lclsDet_comgen.nPayfreq = .FieldToClass("nPayFreq")
						lclsDet_comgen.sProductDes = .FieldToClass("sDescript")
                        lclsDet_comgen.sShort_des = .FieldToClass("sShort_des")
                        lclsDet_comgen.nAgreement = .FieldToClass("nAgreement")
						
						Call Add(lclsDet_comgen)
						'UPGRADE_NOTE: Object lclsDet_comgen may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
						lclsDet_comgen = Nothing
						.RNext()
					Loop 
					Find = True
				End If
			End With
		End If
		
Find_Err: 
		If Err.Number Then
			Find = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecDet_comgen may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecDet_comgen = Nothing
		'UPGRADE_NOTE: Object lclsDet_comgen may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsDet_comgen = Nothing
	End Function
	
	'***Item: Returns an element of the collection (acording to the index)
	'*Item: Devuelve un elemento de la colección (segun índice)
	Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As Det_comgen
		Get
			Item = mCol.Item(vntIndexKey)
		End Get
	End Property
	
	'***Count: Returns the number of elements that the collection has
	'*Count: Devuelve el número de elementos que posee la colección
	Public ReadOnly Property Count() As Integer
		Get
			Count = mCol.Count()
		End Get
	End Property
	
	'***NewEnum: Enumerates the collection for use in a For Each...Next loop
	'*NewEnum: Permite enumerar la colección para utilizarla en un ciclo For Each... Next
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
	
	'**%Remove: Deletes an element from the collection
	'%Remove: Elimina un elemento de la colección
	Public Sub Remove(ByRef vntIndexKey As Object)
		mCol.Remove(vntIndexKey)
	End Sub
	
	'**%Class_Initialize: Controls the creation of an instance of the collection
	'%Class_Initialize: Controla la creación de una instancia de la colección
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		nAuxComtabge = eRemoteDB.Constants.intNull
		dAuxEffecdate = dtmNull
		mCol = New Collection
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
	
	'**%Class_Terminate: Controls the destruction of an instance of the collection
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
End Class






