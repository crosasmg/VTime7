Option Strict Off
Option Explicit On
Public Class Claim_damas
	Implements System.Collections.IEnumerable
	'%-------------------------------------------------------%'
	'% $Workfile:: Claim_damas.cls                          $%'
	'% $Author:: Nvaplat37                                  $%'
	'% $Date:: 8/09/03 1:49p                                $%'
	'% $Revision:: 7                                        $%'
	'%-------------------------------------------------------%'
	
	Private mCol As Collection
	
	'%Add: Agrega un nuevo registro a la colección
	Public Function Add(ByVal lclsClaim_dama As Claim_Dama) As Claim_Dama
		mCol.Add(lclsClaim_dama)
		
		'+ Devolver el objeto creado
		Add = lclsClaim_dama
	End Function
	
	'**% Find: Allows to charge to the collection the possible damage of a claim
	'% Find: Permite cargar en la colección los daños posibles de un siniestro
	Public Function Find(ByVal nBranch As Integer, ByVal nClaim As Double, ByVal nCase_num As Integer, ByVal nDeman_type As Integer) As Boolean
		
		Dim lrecReaClaim_Dama As eRemoteDB.Execute
		Dim lclsClaim_dama As Claim_Dama
		
		lrecReaClaim_Dama = New eRemoteDB.Execute
		
		On Error GoTo Find_Err
		
		With lrecReaClaim_Dama
			.StoredProcedure = "ReaClaim_Dama"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nClaim", nClaim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCase_num", nCase_num, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDeman_type", nDeman_type, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				Do While Not .EOF
					lclsClaim_dama = New Claim_Dama
					lclsClaim_dama.nBranch = nBranch
					lclsClaim_dama.nCase_num = nCase_num
					lclsClaim_dama.nClaim = nClaim
					lclsClaim_dama.nDeman_type = nDeman_type
					lclsClaim_dama.nDamage_cod = .FieldToClass("nDamage_cod")
					lclsClaim_dama.sDes_Damage_cod = .FieldToClass("sDes_Damage_cod")
					lclsClaim_dama.nMag_dam = .FieldToClass("nMag_dam")
					lclsClaim_dama.sDes_Mag_dam = .FieldToClass("sDes_Mag_dam")
					Call Add(lclsClaim_dama)
					lclsClaim_dama = Nothing
					.RNext()
				Loop 
				Find = True
			Else
				Find = False
			End If
		End With
		
Find_Err: 
		If Err.Number Then
			Find = False
		End If
		On Error GoTo 0
		lrecReaClaim_Dama = Nothing
		lclsClaim_dama = Nothing
	End Function
	
	
	'*** Item: takes an element from the collection
	'* Item: toma un elemento de la colección
	Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As Tab_damage
		Get
			Item = mCol.Item(vntIndexKey)
		End Get
	End Property
	
	'*** Count : counts the number of elements inside the collection
	'* Count: cuenta el número de elementos dentro de la colección
	Public ReadOnly Property Count() As Integer
		Get
			Count = mCol.Count()
		End Get
	End Property
	
	'*** NewEnum: enumerates the elements inside the collection
	'* NewEnum: enumera los elementos dentro de la colección
	'UPGRADE_NOTE: NewEnum property was commented out. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="B3FC1610-34F3-43F5-86B7-16C984F0E88E"'
	'Public ReadOnly Property NewEnum() As stdole.IUnknown
		'Get
			'NewEnum = mCol._NewEnum
		'End Get
	'End Property
	
	Public Function GetEnumerator() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
				GetEnumerator = mCol.GetEnumerator
	End Function
	
	'*** Remove: deletes one element inside the collection
	'* Remove: elimina un elemento dentro de la colección
	Public Sub Remove(ByRef vntIndexKey As Object)
		mCol.Remove(vntIndexKey)
	End Sub
	
	'*** Class_Initialize: controls the opening of each instance of the collection
	'* Class_Initialize: controla la apertura de cada instancia de la colección
	Private Sub Class_Initialize_Renamed()
		mCol = New Collection
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
	
	'*** Claa_Terminate: deletes the collection
	'* Class_Terminate: elimina la colección
	Private Sub Class_Terminate_Renamed()
		mCol = Nothing
	End Sub
	Protected Overrides Sub Finalize()
		Class_Terminate_Renamed()
		MyBase.Finalize()
	End Sub
End Class






