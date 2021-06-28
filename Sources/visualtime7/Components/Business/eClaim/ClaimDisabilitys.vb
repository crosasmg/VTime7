Option Strict Off
Option Explicit On
Public Class ClaimDisabilitys
	Implements System.Collections.IEnumerable
	
	'variable local para contener colección
	Private mCol As Collection
	
	'%Add: Agrega un nuevo registro a la colección
	Public Function Add(ByRef objClass As ClaimDisability) As ClaimDisability
		If objClass Is Nothing Then
			objClass = New ClaimDisability
		End If
		
		With objClass
			mCol.Add(objClass, "CP" & .nClaim & .nCase_num & .nDeman_type & .nCovergen & .nDisability)
		End With
		
		Add = objClass
	End Function
	
	Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As ClaimDisability
		Get
			'se usa al hacer referencia a un elemento de la colección
			'vntIndexKey contiene el índice o la clave de la colección,
			'por lo que se declara como un Variant
			'Syntax: Set foo = x.Item(xyz) or Set foo = x.Item(5)
			Item = mCol.Item(vntIndexKey)
		End Get
	End Property
	
	Public ReadOnly Property Count() As Integer
		Get
			'se usa al obtener el número de elementos de la
			'colección. Sintaxis: Debug.Print x.Count
			Count = mCol.Count()
		End Get
	End Property
	
	'UPGRADE_NOTE: NewEnum property was commented out. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="B3FC1610-34F3-43F5-86B7-16C984F0E88E"'
	'Public ReadOnly Property NewEnum() As stdole.IUnknown
		'Get
			'esta propiedad permite enumerar
			'esta colección con la sintaxis For...Each
			'NewEnum = mCol._NewEnum
		'End Get
	'End Property
	
	Public Function GetEnumerator() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
				GetEnumerator = mCol.GetEnumerator
	End Function
	
	Public Sub Remove(ByRef vntIndexKey As Object)
		'se usa al quitar un elemento de la colección
		'vntIndexKey contiene el índice o la clave, por lo que se
		'declara como un Variant
		'Sintaxis: x.Remove(xyz)
		
		
		mCol.Remove(vntIndexKey)
	End Sub
	
	
	Private Sub Class_Initialize_Renamed()
		'crea la colección cuando se crea la clase
		mCol = New Collection
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
	
	Private Sub Class_Terminate_Renamed()
		'destruye la colección cuando se termina la clase
		mCol = Nothing
	End Sub
	Protected Overrides Sub Finalize()
		Class_Terminate_Renamed()
		MyBase.Finalize()
	End Sub
	
	'%Find: Lee los datos de la tabla
	Public Function Find(ByVal nClaim As Integer, ByVal nCase_num As Integer, ByVal nDeman_type As Integer) As Boolean
		Dim lrecClaimDisability As eRemoteDB.Execute
		Dim lclsClaimDisability As ClaimDisability
		
		On Error GoTo Find_Err
		
		lrecClaimDisability = New eRemoteDB.Execute
		
		
		'+Definición de parámetros para stored procedure 'ReaTar_Disability'
		'+Información leída el 25/10/01
		With lrecClaimDisability
			.StoredProcedure = "reaClaimDisability"
			.Parameters.Add("nClaim", nClaim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCase_num", nCase_num, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDeman_type", nDeman_type, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				Do While Not .EOF
					lclsClaimDisability = New ClaimDisability
					lclsClaimDisability.nExist = .FieldToClass("nExist")
					lclsClaimDisability.nClaim = .FieldToClass("nClaim")
					lclsClaimDisability.nCase_num = .FieldToClass("nCase_num")
					lclsClaimDisability.nDeman_type = .FieldToClass("nDeman_type")
					lclsClaimDisability.nCovergen = .FieldToClass("nCovergen")
					lclsClaimDisability.nDisability = .FieldToClass("nDisability")
					lclsClaimDisability.nRate = .FieldToClass("nRate")
					Call Add(lclsClaimDisability)
					.RNext()
					lclsClaimDisability = Nothing
				Loop 
				Find = True
				.RCloseRec()
			End If
		End With
		
Find_Err: 
		If Err.Number Then
			Find = False
		End If
		lrecClaimDisability = Nothing
		On Error GoTo 0
	End Function
End Class






