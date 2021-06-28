Option Strict Off
Option Explicit On
Public Class Cur_Allows
	Implements System.Collections.IEnumerable
	'%-------------------------------------------------------%'
	'% $Workfile:: Cur_Allows.cls                           $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 12:35p                               $%'
	'% $Revision:: 10                                       $%'
	'%-------------------------------------------------------%'
	
	Private mCol As Collection
	
	'**%Find: Loads the information of the of the allowed currencies for the policy
	'%Find: Carga la informaci�n de las monedas permitidas para la p�liza
	Public Function Find_DP005(ByVal nBranch As Integer, ByVal nProduct As Integer) As Boolean
		Dim lrecreaCur_allow As eRemoteDB.Execute
		On Error GoTo Find_DP005_err
		lrecreaCur_allow = New eRemoteDB.Execute
		
		'**+Stored procedure parameters definition 'insudb.reaCur_allow'
		'**+Data of 04/06/2001 09:46:59 a.m.
		'+Definici�n de par�metros para stored procedure 'insudb.reaCur_allow'
		'+Informaci�n le�da el 06/04/2001 09:46:59 a.m.
		
		Find_DP005 = True
		
		With lrecreaCur_allow
			.StoredProcedure = "reaCur_allow"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				Do While Not .EOF
					Call Add(.FieldToClass("sDescript"), .FieldToClass("sDefaulti"), .FieldToClass("nCurrency"), .FieldToClass("nCodigint"))
					
					.RNext()
				Loop 
				.RCloseRec()
			End If
		End With
		
Find_DP005_err: 
		If Err.Number Then
			Find_DP005 = False
		End If
		'UPGRADE_NOTE: Object lrecreaCur_allow may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaCur_allow = Nothing
		On Error GoTo 0
	End Function
	
	'**%Find: Loads the information of the of the allowed currencies for the policy
	'%Find: Carga la informaci�n de las monedas permitidas para la p�liza
	Public Function Find_CA001(ByVal nBranch As Integer, ByVal nProduct As Integer) As Boolean
		Dim lrecreaCur_allow As eRemoteDB.Execute
		On Error GoTo Find_CA001_err
		lrecreaCur_allow = New eRemoteDB.Execute
		
		'**+Stored procedure parameters definition 'insudb.reaCur_allow'
		'**+Data of 04/06/2001 09:46:59 a.m.
		'+Definici�n de par�metros para stored procedure 'insudb.reaCur_allow'
		'+Informaci�n le�da el 06/04/2001 09:46:59 a.m.
		
		Find_CA001 = True
		
		With lrecreaCur_allow
			.StoredProcedure = "REACUR_ALLOW_CA001"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				Do While Not .EOF
					Call Add(.FieldToClass("sDescript"), .FieldToClass("sDefaulti"), .FieldToClass("nCurrency"), .FieldToClass("nCodigint"))
					
					.RNext()
				Loop 
				.RCloseRec()
			Else
				Find_CA001 = False
			End If
		End With
		
Find_CA001_err: 
		If Err.Number Then
			Find_CA001 = False
		End If
		'UPGRADE_NOTE: Object lrecreaCur_allow may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaCur_allow = Nothing
		On Error GoTo 0
	End Function
	
	'***Count: Returns the number of elements that the collection has
	'*Count: Devuelve el n�mero de elementos que posee la colecci�nn
	Public ReadOnly Property Count() As Integer
		Get
			Count = mCol.Count()
		End Get
	End Property
	
	'***Item: Returns an element from the collection (according to the index)
	'*Item: Devuelve un elemento de la colecci�n (segun �ndice)
	'--------------------------------------------------------------
	Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As Cur_Allow
		Get
			'--------------------------------------------------------------
			Item = mCol.Item(vntIndexKey)
		End Get
	End Property
	
	'***NewEnum: Enumerates the collection for use in a For Each...Next loop
	'*NewEnum: Permite enumerar la colecci�n para utilizarla en un ciclo For Each... Next
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
	
	'**% Remove: Deletes an element from the collection
	'% Remove: Elimina un elemento de la colecci�n
	'---------------------------------------------
	Public Sub Remove(ByRef vntIndexKey As Object)
		'---------------------------------------------
		mCol.Remove(vntIndexKey)
	End Sub
	
	'**%Add: Adds a new instance of the class "Cur_Allow" to the collection
	'%Add: A�ade una nueva instancia de la clase "Cur_Allow" a la colecci�n
	Public Function Add(ByRef sDescript As String, ByRef sDefaulti As String, ByRef nCurrency As Integer, Optional ByRef nCodigInt As Double = 0, Optional ByRef nBranch As Integer = 0, Optional ByRef nProduct As Integer = 0, Optional ByRef dCompdate As Date = #12:00:00 AM#, Optional ByRef nUsercode As Integer = 0, Optional ByRef dEffecdate As Date = #12:00:00 AM#, Optional ByRef nExchange As Double = 0) As Cur_Allow
		
		Dim objNewMember As Cur_Allow
		objNewMember = New Cur_Allow
		
		With objNewMember
			.nBranch = nBranch
			.nProduct = nProduct
			
			.sDefaulti = sDefaulti
			.nCurrency = nCurrency
			
			.dCompdate = dCompdate
			.nUsercode = nUsercode
			
			'**+Auxiliary properties
			'+Propiedades auxiliares
			.dEffecdate = dEffecdate
			.sDescript = sDescript
			.nExchange = nExchange
			
			.nCodigInt = nCodigInt
			
		End With
		
		mCol.Add(objNewMember)
		
		Add = objNewMember
		'UPGRADE_NOTE: Object objNewMember may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		objNewMember = Nothing
		
	End Function
	
	'**%Class_Initialize: Controls the creation of an instance of the collection
	'%Class_Initialize: Controla la creaci�n de una instancia de la colecci�n
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		'creates the collection when this class is created
		mCol = New Collection
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
End Class






