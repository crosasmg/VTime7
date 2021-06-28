Option Strict Off
Option Explicit On
Public Class Prod_addcaps
	Implements System.Collections.IEnumerable
	'%-------------------------------------------------------%'
	'% $Workfile:: Prod_addcaps.cls                         $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 12:36p                               $%'
	'% $Revision:: 12                                       $%'
	'%-------------------------------------------------------%'
	
	Private mCol As Collection
	
	'- Variables que guardan la llave de búsqueda
	Private mlngBranch As Integer
	Private mlngProduct As Integer
	Private mlngCovergen As Integer
	Private mdtmEffecdate As Date
	
	'% Add: se agregan los elementos a la colección
	Public Function Add(ByRef objProd_addcap As Prod_addcap) As Prod_addcap
		If objProd_addcap Is Nothing Then
			objProd_addcap = New Prod_addcap
		End If
		
		With objProd_addcap
			mCol.Add(objProd_addcap)
		End With
		Add = objProd_addcap
	End Function
	
	'* Item: devuelve un elemento de la colección (según índice, o llave)
	Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As Prod_addcap
		Get
			Item = mCol.Item(vntIndexKey)
		End Get
	End Property
	
	'* Count: devuelve el número de elementos que posee la colección
	Public ReadOnly Property Count() As Integer
		Get
			Count = mCol.Count()
		End Get
	End Property
	
	'* NewEnum: permite enumerar la colección para utilizarla en un ciclo For Each... Next
	'UPGRADE_NOTE: NewEnum property was commented out. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="B3FC1610-34F3-43F5-86B7-16C984F0E88E"'
	'Public ReadOnly Property NewEnum() As stdole.IUnknown
		'Get
			'NewEnum = mCol._NewEnum
		'End Get
	'End Property
	
	Public Function GetEnumerator() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
				GetEnumerator = mCol.GetEnumerator
	End Function
	
	'% Remove: elimina un elemento de la colección
	Public Sub Remove(ByRef vntIndexKey As Object)
		mCol.Remove(vntIndexKey)
	End Sub
	
	'* Class_Initialize: se controla la creación de la instancia de la clase
	Private Sub Class_Initialize_Renamed()
		mCol = New Collection
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
	
	'* Class_Terminate: controla la destrucción de la instancia del objeto de la clase
	Private Sub Class_Terminate_Renamed()
		mCol = Nothing
	End Sub
	Protected Overrides Sub Finalize()
		Class_Terminate_Renamed()
		MyBase.Finalize()
	End Sub
	
	'% Find: Lee los datos de la tabla
	Public Function Find(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nCovergen As Integer, ByVal dEffecdate As Date, ByVal nRole As Integer, Optional ByVal bFind As Boolean = False) As Boolean
		Dim lrecReaProd_addcap As eRemoteDB.Execute
		Dim lclsProd_addcap As Prod_addcap
		
		On Error GoTo Find_Err
		Find = True
		If mlngBranch <> nBranch Or mlngProduct <> nProduct Or mlngCovergen <> nCovergen Or mdtmEffecdate <> dEffecdate Or bFind Then
			
			lrecReaProd_addcap = New eRemoteDB.Execute
			With lrecReaProd_addcap
				.StoredProcedure = "INSDP770PKG.READP770"
				.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nCovergen", nCovergen, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nRole", nRole, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				If .Run Then
					Do While Not .EOF
						lclsProd_addcap = New Prod_addcap
						lclsProd_addcap.nBranch = nBranch
						lclsProd_addcap.nProduct = nProduct
						lclsProd_addcap.nCovergen = nCovergen
						lclsProd_addcap.nBranchadd = .FieldToClass("nBranchadd")
						lclsProd_addcap.sBranchadd = .FieldToClass("sBranchadd")
						lclsProd_addcap.nProductadd = .FieldToClass("nProductadd")
						lclsProd_addcap.sProductadd = .FieldToClass("sProductadd")
						lclsProd_addcap.nCoveradd = .FieldToClass("nCoveradd")
						lclsProd_addcap.sCoveradd = .FieldToClass("sCoveradd")
						lclsProd_addcap.nRoleadd = .FieldToClass("nRoleadd")
						lclsProd_addcap.sRoleadd = .FieldToClass("sRoleadd")
						lclsProd_addcap.nClusteradd = .FieldToClass("nClusteradd")
						lclsProd_addcap.nCapitalAdd = .FieldToClass("nCapitaladd")
						lclsProd_addcap.nInverse = .FieldToClass("nInverse")
						lclsProd_addcap.nId = .FieldToClass("nId")
						lclsProd_addcap.nCapital = .FieldToClass("nCapital")
						Call Add(lclsProd_addcap)
						.RNext()
						lclsProd_addcap = Nothing
					Loop 
					.RCloseRec()
					mlngCovergen = nCovergen
					mlngBranch = nBranch
					mlngProduct = nProduct
					mlngCovergen = nCovergen
					mdtmEffecdate = dEffecdate
				End If
			End With
		End If
		
Find_Err: 
		If Err.Number Then
			Find = False
		End If
		On Error GoTo 0
		lrecReaProd_addcap = Nothing
	End Function
End Class






