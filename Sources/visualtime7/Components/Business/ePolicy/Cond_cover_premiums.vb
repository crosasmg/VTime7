Option Strict Off
Option Explicit On
Public Class Cond_cover_premiums
	Implements System.Collections.IEnumerable
	'%-------------------------------------------------------%'
	'% $Workfile:: Cond_cover_premiums.cls                   $%'
	'% $Author:: jRengifo                                    $%'
	'% $Date:: 27/06/03 19.01                                $%'
	'% $Revision:: 1                                         $%'
	'%-------------------------------------------------------%'
	
	'local variable to hold collection
	Private mCol As Collection
	
	'%Add: Añade una nueva instancia de la clase
	Public Sub Add(ByVal objNewMember As Cond_cover_premium)
		'agrega a la colección la clase el parametro.
		mCol.Add(objNewMember)
	End Sub
	
	'*Item: Devuelve un elemento de la colección (segun índice)
	Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As Cond_cover_premium
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
	'Public ReadOnly Property NewEnum() As stdole.IUnknown
		'Get
			'this property allows you to enumerate
			'this collection with the For...Each syntax
			'NewEnum = mCol._NewEnum
		'End Get
	'End Property
	
	Public Function GetEnumerator() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
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
	
	
	Private Sub Class_Initialize_Renamed()
		'creates the collection when this class is created
		mCol = New Collection
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
	
	Private Sub Class_Terminate_Renamed()
		'destroys collection when this class is terminated
		mCol = Nothing
	End Sub
	Protected Overrides Sub Finalize()
		Class_Terminate_Renamed()
		MyBase.Finalize()
	End Sub

	'%Find: Devuelve una colección de objetos de tipo Cond_cover_premium
	Public Function Find(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal nGroup As Integer, ByVal nModulec As Integer, ByVal dEffecdate As Date) As Object
		'-Se define la variable lreaCond_cover_premium
		Dim lreaCond_cover_premium As New eRemoteDB.Execute
		Dim lclsCond_cover_premium As Cond_cover_premium
		On Error GoTo Find_Err
		
		'+Definición de parámetros para stored procedure 'reaCond_cover_premium'
		'+Información leída el 29/10/2001 11:37
		
		With lreaCond_cover_premium
			.StoredProcedure = "reaCond_cover_premium_A"
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
					lclsCond_cover_premium = New Cond_cover_premium
					lclsCond_cover_premium.sCertype = .FieldToClass("sCertype")
					lclsCond_cover_premium.nBranch = .FieldToClass("nBranch")
					lclsCond_cover_premium.nProduct = .FieldToClass("nProduct")
					lclsCond_cover_premium.nPolicy = .FieldToClass("nPolicy")
					lclsCond_cover_premium.nGroup = .FieldToClass("nGroup")
					lclsCond_cover_premium.nCertif = .FieldToClass("nCertif")
					lclsCond_cover_premium.nModulec = .FieldToClass("nModulec")
					lclsCond_cover_premium.nCover = .FieldToClass("nCover")
					lclsCond_cover_premium.nRole = .FieldToClass("nRole")
					lclsCond_cover_premium.dEffecdate = .FieldToClass("dEffecdate")
					lclsCond_cover_premium.nTypcond = .FieldToClass("nTypcond")
                    lclsCond_cover_premium.nPremium = .FieldToClass("nPremium")
                    lclsCond_cover_premium.nCapital_min = .FieldToClass("nCapital_min")
                    lclsCond_cover_premium.nCapital_max = .FieldToClass("nCapital_max")
                    lclsCond_cover_premium.nRate = .FieldToClass("nRate")
                    lclsCond_cover_premium.sRoutine = .FieldToClass("sRoutine")           
                    lclsCond_cover_premium.nId_table = .FieldToClass("nId_table")
					lclsCond_cover_premium.nCurrency = .FieldToClass("nCurrency")
					lclsCond_cover_premium.dNulldate = .FieldToClass("dNulldate")
                    lclsCond_cover_premium.dCompdate = .FieldToClass("dCompdate")
                    lclsCond_cover_premium.nId = .FieldToClass("nID")
					Call Add(lclsCond_cover_premium)
					.RNext()
					lclsCond_cover_premium = Nothing
				Loop 
				.RCloseRec()
				Find = True
			Else
				Find = False
			End If
		End With
		lreaCond_cover_premium = Nothing
		
Find_Err: 
		If Err.Number Then
			Find = False
		End If
		On Error GoTo 0
	End Function
End Class






