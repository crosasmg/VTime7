Option Strict Off
Option Explicit On
Public Class Tar_Disabilitys
	Implements System.Collections.IEnumerable
	'variable local para contener colección
	Private mCol As Collection
	
	'%Add: Agrega un nuevo registro a la colección
	Public Function Add(ByRef objClass As Tar_Disability) As Tar_Disability
		If objClass Is Nothing Then
			objClass = New Tar_Disability
		End If
		
		With objClass
			mCol.Add(objClass, "CP" & .nBranch & .nCovergen & .nDisability & .dEffecdate.ToString("yyyyMMdd"))
		End With
		
		Add = objClass
		
	End Function
	
	Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As Tar_Disability
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
	Public Function Find(ByVal nBranch As Integer, ByVal dEffecdate As Date, Optional ByVal bFind As Boolean = False) As Boolean
        Dim mdtmEffecdate As Object = New Object
        Dim mintBranch As Object = New Object
        Dim lrecReaTar_Disability As eRemoteDB.Execute
		Dim lclsTar_Disability As Tar_Disability
		
		On Error GoTo Find_Err
		Find = True
		
		If mintBranch <> nBranch Or mdtmEffecdate <> dEffecdate Or bFind Then
			
			lrecReaTar_Disability = New eRemoteDB.Execute
			
			With lrecReaTar_Disability
				.StoredProcedure = "ReaTar_Disability_a"
				.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				If .Run Then
					Find = True
					mintBranch = nBranch
					mdtmEffecdate = dEffecdate
					Do While Not .EOF
						lclsTar_Disability = New Tar_Disability
						lclsTar_Disability.nBranch = nBranch
						lclsTar_Disability.nCovergen = .FieldToClass("nCovergen")
						lclsTar_Disability.dEffecdate = .FieldToClass("dEffecdate")
						lclsTar_Disability.nDisability = .FieldToClass("nDisability")
						lclsTar_Disability.nRate = .FieldToClass("nRate")
						lclsTar_Disability.dNulldate = .FieldToClass("dNulldate")
						lclsTar_Disability.sShort_Des = .FieldToClass("sShort_Des")
						Call Add(lclsTar_Disability)
						.RNext()
						lclsTar_Disability = Nothing
					Loop 
					.RCloseRec()
				Else
					Find = False
				End If
			End With
		End If
Find_Err: 
		If Err.Number Then
			Find = False
		End If
		lrecReaTar_Disability = Nothing
		On Error GoTo 0
	End Function
End Class






