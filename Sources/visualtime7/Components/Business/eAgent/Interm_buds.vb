Option Strict Off
Option Explicit On
Public Class Interm_buds
	Implements System.Collections.IEnumerable
	'%-------------------------------------------------------%'
	'% $Workfile:: Interm_buds.cls                          $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 1:41p                                $%'
	'% $Revision:: 8                                        $%'
	'%-------------------------------------------------------%'
	
	'local variable to hold collection
	Private mCol As Collection
	
	'**%Add: adds a new instance to the Interm_bud class to the collection
	'%Add: A�ade una nueva instancia de la clase Interm_bud a la colecci�n
	Public Function Add(ByVal nIntermed As Integer, ByVal nCurrency As Integer, ByVal sType_Infor As String, ByVal sPeriodTyp As String, ByVal nPeriodNum As Integer, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal dEffecdate As Date, ByVal nBud_total As Double, ByVal nReal_total As Double, ByVal sDesc_prod As String) As Interm_bud
		'create a new object
		Dim objNewMember As Interm_bud
		
		objNewMember = New Interm_bud
		
		With objNewMember
			.nBranch = nBranch
			.nProduct = nProduct
			.nBud_total = nBud_total
			.nReal_total = nReal_total
			.sDesc_prod = sDesc_prod
		End With
		
		'set the properties passed into the method
		mCol.Add(objNewMember, nIntermed & nCurrency & sType_Infor & sPeriodTyp & nPeriodNum & nBranch & nProduct & dEffecdate)
		
		
		'return the object created
		Add = objNewMember
		'UPGRADE_NOTE: Object objNewMember may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		objNewMember = Nothing
	End Function
	
	'*** Item: Restores an element of the collection (according to the index)
	'* Item: Devuelve un elemento de la colecci�n (segun �ndice)
	Public ReadOnly Property Item(ByVal vntIndexKey As Object) As Interm_bud
		Get
			'used when referencing an element in the collection
			'vntIndexKey contains either the Index or Key to the collection,
			'this is why it is declared as a Variant
			'Syntax: Set foo = x.Item(xyz) or Set foo = x.Item(5)
			Item = mCol.Item(vntIndexKey)
		End Get
	End Property
	
	'*** Count: Restores the number of elements that the collection owns
	'* Count: Devuelve el n�mero de elementos que posee la colecci�n
	Public ReadOnly Property Count() As Integer
		Get
			'used when retrieving the number of elements in the
			'collection. Syntax: Debug.Print x.Count
			Count = mCol.Count()
		End Get
	End Property
	
	'** NewEnum: Allows to enumerate the collection for using it in a Cycle for Each...Next
	'* NewEnum: Permite enumerar la colecci�n para utilizarla en un ciclo For Each... Next
	'UPGRADE_NOTE: NewEnum property was commented out. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="B3FC1610-34F3-43F5-86B7-16C984F0E88E"'
	'Public ReadOnly Property NewEnum() As stdole.IUnknown
		'Get
			'***this property allows you to enumerate
			'***this collection with the For...Each syntax
			'NewEnum = mCol._NewEnum
		'End Get
	'End Property
	
	Public Function GetEnumerator() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
		'UPGRADE_TODO: Uncomment and change the following line to return the collection enumerator. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="95F9AAD0-1319-4921-95F0-B9D3C4FF7F1C"'
		GetEnumerator = mCol.GetEnumerator
	End Function
	
	'**% Remove: Deletes an element of the collection
	'% Remove: Elimina un elemento de la colecci�n
	Public Sub Remove(ByRef vntIndexKey As Object)
		'used when removing an element from the collection
		'vntIndexKey contains either the Index or Key, which is why
		'it is declared as a Variant
		'Syntax: x.Remove(xyz)
		mCol.Remove(vntIndexKey)
	End Sub
	
	'**+ Class_Initialize: Controla la creaci�n de una instancia de la colecci�n
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		'**+creates the collection when this class is created
		mCol = New Collection
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
	
	'**% Class_Terminate: controls the delete of an instance of the collection
	'% Class_Terminate: Controla la destrucci�n de una instancia de la colecci�n
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
	
	'**% Find: search the production goals for an intermediary
	'% Find: se buscan las metas de producci�n para un intermediario
	Public Function Find(ByVal lintIntermed As Integer, ByVal lintCurrency As Integer, ByVal lstrType_infor As String, ByVal lstrPeriodtyp As String, ByVal lintPeriodnum As Integer, ByVal ldtmEffecdate As Date) As Boolean
		
		Dim lrecreaInterm_bud_a As eRemoteDB.Execute
		
		On Error GoTo Find_Err
		lrecreaInterm_bud_a = New eRemoteDB.Execute
		'**+ Parameter definitions for stored procedure 'insud.reaInterm_bud_a'
		'+Definici�n de par�metros para stored procedure 'insudb.reaInterm_bud_a'
		'**+ Information read on February 05,2001 2:47:06p.m.
		'+Informaci�n le�da el 05/02/2001 2:47:06 PM
		With lrecreaInterm_bud_a
			.StoredProcedure = "reaInterm_bud_a"
			.Parameters.Add("nIntermed", lintIntermed, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCurrency", lintCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sType_infor", lstrType_infor, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sPeriodtyp", lstrPeriodtyp, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPeriodnum", lintPeriodnum, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", ldtmEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				Find = True
				Do While Not .EOF
					Call Add(lintIntermed, lintCurrency, lstrType_infor, lstrPeriodtyp, lintPeriodnum, .FieldToClass("nBranch"), .FieldToClass("nProduct"), ldtmEffecdate, .FieldToClass("nBud_total"), .FieldToClass("nReal_total"), .FieldToClass("sDescript"))
					.RNext()
				Loop 
				.RCloseRec()
			Else
				Find = False
			End If
		End With
		'UPGRADE_NOTE: Object lrecreaInterm_bud_a may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaInterm_bud_a = Nothing
		
Find_Err: 
		If Err.Number Then
			Find = False
		End If
		On Error GoTo 0
	End Function
	
	'**% valWinBranchProdPar: validate if a specific value is in the collection,
	'** considering the line of business - product.
	'% valWinBranchProdPar: Permite validar si un valor determinado existe la colecci�n,
	'%                      tomando en cuenta el ramo - producto.
	Public Function valWinBranchProdPar(ByVal nBranch As Integer) As Boolean
		Dim lclsInterm_bud As Interm_bud
		
		valWinBranchProdPar = False
		
		On Error GoTo valWinBranchProdPar_err
		
		For	Each lclsInterm_bud In mCol
			If lclsInterm_bud.nBranch = nBranch Then
				If (lclsInterm_bud.nProduct <> eRemoteDB.Constants.intNull And lclsInterm_bud.nProduct <> 0) Then
					valWinBranchProdPar = True
					Exit For
				End If
			End If
		Next lclsInterm_bud
		
valWinBranchProdPar_err: 
		If Err.Number Then
			valWinBranchProdPar = False
		End If
		On Error GoTo 0
		
		'UPGRADE_NOTE: Object lclsInterm_bud may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsInterm_bud = Nothing
	End Function
	
	'**% valDupTwoCol: validate if a specific value is in the collection
	'**% considering the product line of business.
	'% valDupTwoCol: Permite validar si un valor determinado existe en la colecci�n
	'%               tomando en cuenta el ramo producto.
	Public Function valDupTwoCol(ByVal nBranch As Integer, ByVal nProduct As Integer) As Boolean
		Dim lclsInterm_bud As Interm_bud
		
		valDupTwoCol = False
		
		On Error GoTo valDupTwoCol_err
		
		For	Each lclsInterm_bud In mCol
			If lclsInterm_bud.nBranch = nBranch Then
				If lclsInterm_bud.nProduct = nProduct Or lclsInterm_bud.nProduct = eRemoteDB.Constants.intNull Then
					valDupTwoCol = True
					Exit For
				End If
			End If
		Next lclsInterm_bud
		
valDupTwoCol_err: 
		If Err.Number Then
			valDupTwoCol = False
		End If
		On Error GoTo 0
		
		'UPGRADE_NOTE: Object lclsInterm_bud may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsInterm_bud = Nothing
	End Function
End Class






