Option Strict Off
Option Explicit On
Public Class Cond_covers
	Implements System.Collections.IEnumerable
	'%-------------------------------------------------------%'
	'% $Workfile:: Cond_covers.cls                          $%'
	'% $Author:: Nvaplat41                                  $%'
	'% $Date:: 9/10/03 19.01                                $%'
	'% $Revision:: 12                                       $%'
	'%-------------------------------------------------------%'
	
	'local variable to hold collection
	Private mCol As Collection
	
	'%Add: Añade una nueva instancia de la clase
	Public Sub Add(ByVal objNewMember As Cond_cover)
		'agrega a la colección la clase el parametro.
		mCol.Add(objNewMember)
	End Sub
	
	'*Item: Devuelve un elemento de la colección (segun índice)
	Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As Cond_cover
		Get
			'used when referencing an element in the collection
			'vntIndexKey contains either the Index or Key to the collection,
			'this is why it is declared as a Variant
			'Syntax: Set foo = x.Item(xyz) or Set foo = x.Item(5)
			Item = mCol.Item(vntIndexKey)
		End Get
	End Property
	
	
	
	'*Count: Devuelve el número de elementos que posee la colección
	Public ReadOnly Property Count() As Integer
		Get
			'used when retrieving the number of elements in the
			'collection. Syntax: Debug.Print x.Count
			Count = mCol.Count()
		End Get
	End Property
	
	
	'*NewEnum: Permite enumerar la colección para utilizarla en un ciclo For Each... Next
	'UPGRADE_NOTE: NewEnum property was commented out. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="B3FC1610-34F3-43F5-86B7-16C984F0E88E"'
	'Public ReadOnly Property NewEnum() As stdole.IUnknown
		'Get
			'this property allows you to enumerate
			'this collection with the For...Each syntax
			'NewEnum = mCol._NewEnum
		'End Get
	'End Property
	
	Public Function GetEnumerator() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
		'UPGRADE_TODO: Uncomment and change the following line to return the collection enumerator. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="95F9AAD0-1319-4921-95F0-B9D3C4FF7F1C"'
		GetEnumerator = mCol.GetEnumerator
	End Function
	
	
	'%Remove: Elimina un elemento de la colección
	Public Sub Remove(ByRef vntIndexKey As Object)
		'used when removing an element from the collection
		'vntIndexKey contains either the Index or Key, which is why
		'it is declared as a Variant
		'Syntax: x.Remove(xyz)
		mCol.Remove(vntIndexKey)
	End Sub
	
	
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		'creates the collection when this class is created
		mCol = New Collection
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
	
	'UPGRADE_NOTE: Class_Terminate was upgraded to Class_Terminate_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Terminate_Renamed()
		'destroys collection when this class is terminated
		'UPGRADE_NOTE: Object mCol may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		mCol = Nothing
	End Sub
	Protected Overrides Sub Finalize()
		Class_Terminate_Renamed()
		MyBase.Finalize()
	End Sub
	'%Find: Devuelve una colección de objetos de tipo Cond_Cover
	Public Function Find(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal nGroup As Integer, ByVal nModulec As Integer, ByVal dEffecdate As Date) As Object
		'-Se define la variable lreaConv_Cover
		Dim lreaCond_Cover As New eRemoteDB.Execute
		Dim lclsCond_Cover As Cond_cover
		On Error GoTo Find_Err
		
		'+Definición de parámetros para stored procedure 'reaConv_Cover'
		'+Información leída el 29/10/2001 11:37
		
		With lreaCond_Cover
			.StoredProcedure = "reaCond_Cover_A"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nGroup", nGroup, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nModulec", nModulec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run Then
				Do While Not .EOF
					lclsCond_Cover = New Cond_cover
					lclsCond_Cover.sCertype = .FieldToClass("sCertype")
					lclsCond_Cover.nBranch = .FieldToClass("nBranch")
					lclsCond_Cover.nProduct = .FieldToClass("nProduct")
					lclsCond_Cover.nPolicy = .FieldToClass("nPolicy")
					lclsCond_Cover.nGroup = .FieldToClass("nGroup")
					lclsCond_Cover.nCertif = .FieldToClass("nCertif")
					lclsCond_Cover.nModulec = .FieldToClass("nModulec")
					lclsCond_Cover.nCover = .FieldToClass("nCover")
					lclsCond_Cover.nRole = .FieldToClass("nRole")
					lclsCond_Cover.dEffecdate = .FieldToClass("dEffecdate")
					lclsCond_Cover.nTypcond = .FieldToClass("nTypcond")
					lclsCond_Cover.nAmount = .FieldToClass("nAmount")
					lclsCond_Cover.nPercent = .FieldToClass("nPercent")
					lclsCond_Cover.nRent = .FieldToClass("nRent")
					lclsCond_Cover.nCurrency = .FieldToClass("nCurrency")
					lclsCond_Cover.dNulldate = .FieldToClass("dNulldate")
                    lclsCond_Cover.dCompdate = .FieldToClass("dCompdate")
                    lclsCond_Cover.nMonthI = .FieldToClass("nMonthI")
                    lclsCond_Cover.nMonthE = .FieldToClass("nMonthE")
                    lclsCond_Cover.nID = .FieldToClass("nID")

					Call Add(lclsCond_Cover)
					.RNext()
					'UPGRADE_NOTE: Object lclsCond_Cover may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
					lclsCond_Cover = Nothing
				Loop 
				.RCloseRec()
				Find = True
			Else
				Find = False
			End If
		End With
		'UPGRADE_NOTE: Object lreaCond_Cover may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lreaCond_Cover = Nothing
		
Find_Err: 
		If Err.Number Then
			Find = False
		End If
		On Error GoTo 0
	End Function
End Class






