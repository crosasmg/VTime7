Option Strict Off
Option Explicit On
Public Class Franchises
	Implements System.Collections.IEnumerable
	
	'variable local para contener colección
	Private mCol As Collection
	'% Find: Devuelve información de los registros de la tabla Franchise
	Public Function Find(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, ByVal nGroup As Integer) As Boolean
		
		Dim lrecReaFranchise As eRemoteDB.Execute
		Dim lclsFranchise As Franchise
		
		On Error GoTo Find_Err
		
		lrecReaFranchise = New eRemoteDB.Execute
		
		'+ Definición de parámetros para stored procedure 'insudb.reaFranchise'
		
		With lrecReaFranchise
			.StoredProcedure = "reaFranchise"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nGroup", nGroup, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run Then
				Find = True
				Do While Not .EOF
					lclsFranchise = New Franchise
					
					lclsFranchise.sCertype = .FieldToClass("sCertype")
					lclsFranchise.nBranch = .FieldToClass("nBranch")
					lclsFranchise.nProduct = .FieldToClass("nProduct")
					lclsFranchise.nPolicy = .FieldToClass("nPolicy")
					lclsFranchise.nCertif = .FieldToClass("nCertif")
					lclsFranchise.dEffecdate = .FieldToClass("dEffecdate")
					lclsFranchise.nFixamount = .FieldToClass("nFixamount")
					lclsFranchise.sFrandedi = .FieldToClass("sFrandedi")
					lclsFranchise.nMaxamount = .FieldToClass("nMaxamount")
					lclsFranchise.nMinamount = .FieldToClass("nMinamount")
					lclsFranchise.nRate = .FieldToClass("nRate")
					lclsFranchise.sFrancApl = .FieldToClass("sFrancApl")
					lclsFranchise.nCurrency = .FieldToClass("nCurrency")
					lclsFranchise.nSeq = .FieldToClass("nSeq")
					lclsFranchise.nDed_Type = .FieldToClass("nDed_Type")
					lclsFranchise.nCover = .FieldToClass("nCover")
					lclsFranchise.nPay_Concep = .FieldToClass("nPay_Concep")
					lclsFranchise.nLevel = .FieldToClass("nLevel")
					lclsFranchise.nRole = .FieldToClass("nRole")
					lclsFranchise.nOrder = .FieldToClass("nOrder")
					lclsFranchise.nModulec = .FieldToClass("nModulec")
					lclsFranchise.nGroup = .FieldToClass("nGroup")
					
					Call Add((lclsFranchise.sCertype), (lclsFranchise.nBranch), (lclsFranchise.nProduct), (lclsFranchise.nPolicy), (lclsFranchise.nCertif), (lclsFranchise.dEffecdate), (lclsFranchise.nFixamount), (lclsFranchise.sFrandedi), (lclsFranchise.nMaxamount), (lclsFranchise.nMinamount), (lclsFranchise.nRate), (lclsFranchise.sFrancApl), (lclsFranchise.nCurrency), (lclsFranchise.nSeq), (lclsFranchise.nDed_Type), (lclsFranchise.nCover), (lclsFranchise.nPay_Concep), (lclsFranchise.nLevel), (lclsFranchise.nRole), (lclsFranchise.nOrder), (lclsFranchise.nModulec), (lclsFranchise.nGroup))
					.RNext()
					'UPGRADE_NOTE: Object lclsFranchise may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
					lclsFranchise = Nothing
				Loop 
				.RCloseRec()
			End If
		End With
		
		Find = True
		
Find_Err: 
		If Err.Number Then
			Find = False
		End If
		On Error GoTo 0
		
		'UPGRADE_NOTE: Object lrecReaFranchise may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecReaFranchise = Nothing
		
	End Function
	
	'% Add: Agrega un objeto a la colección
    Public Function Add(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, ByVal nFixamount As Double, ByVal sFrandedi As String, ByVal nMaxamount As Double, ByVal nMinamount As Double, ByVal nRate As Double, ByVal sFrancApl As String, ByVal nCurrency As Integer, ByVal nSeq As Double, ByVal nDed_Type As Integer, ByVal nCover As Integer, ByVal nPay_Concep As Integer, ByVal nLevel As Integer, ByVal nRole As Integer, ByVal nOrder As Integer, ByVal nModulec As Integer, ByVal nGroup As Integer) As Franchise
        'crear un nuevo objeto
        Dim objNewMember As Franchise
        objNewMember = New Franchise

        'establecer las propiedades que se transfieren al método

        objNewMember.sCertype = sCertype
        objNewMember.nBranch = nBranch
        objNewMember.nProduct = nProduct
        objNewMember.nPolicy = nPolicy
        objNewMember.nCertif = nCertif
        objNewMember.dEffecdate = dEffecdate
        objNewMember.nFixamount = nFixamount
        objNewMember.sFrandedi = sFrandedi
        objNewMember.nMaxamount = nMaxamount
        objNewMember.nMinamount = nMinamount
        objNewMember.nRate = nRate
        objNewMember.sFrancApl = sFrancApl
        objNewMember.nCurrency = nCurrency
        objNewMember.nSeq = nSeq
        objNewMember.nDed_Type = nDed_Type
        objNewMember.nCover = nCover
        objNewMember.nPay_Concep = nPay_Concep
        objNewMember.nLevel = nLevel
        objNewMember.nRole = nRole
        objNewMember.nOrder = nOrder
        objNewMember.nModulec = nModulec
        objNewMember.nGroup = nGroup

        mCol.Add(objNewMember)

        'devolver el objeto creado
        Add = objNewMember
        'UPGRADE_NOTE: Object objNewMember may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        objNewMember = Nothing

    End Function
	
	Public ReadOnly Property Item(ByVal vntIndexKey As Object) As Franchise
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
	
	
	'UPGRADE_NOTE: NewEnum property was commented out. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="B3FC1610-34F3-43F5-86B7-16C984F0E88E"'
	'Public ReadOnly Property NewEnum() As stdole.IUnknown
		'Get
			'esta propiedad permite enumerar
			'esta colección con la sintaxis For...Each
			'NewEnum = mCol._NewEnum
		'End Get
	'End Property
	
	Public Function GetEnumerator() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
		'UPGRADE_TODO: Uncomment and change the following line to return the collection enumerator. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="95F9AAD0-1319-4921-95F0-B9D3C4FF7F1C"'
		GetEnumerator = mCol.GetEnumerator
	End Function
	
	
	Public Sub Remove(ByRef vntIndexKey As Object)
		'se usa al quitar un elemento de la colección
		'vntIndexKey contiene el índice o la clave, por lo que se
		'declara como un Variant
		'Sintaxis: x.Remove(xyz)
		
		mCol.Remove(vntIndexKey)
	End Sub
	
	
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		'crea la colección cuando se crea la clase
		mCol = New Collection
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
	
	
	'UPGRADE_NOTE: Class_Terminate was upgraded to Class_Terminate_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Terminate_Renamed()
		'destruye la colección cuando se termina la clase
		'UPGRADE_NOTE: Object mCol may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		mCol = Nothing
	End Sub
	Protected Overrides Sub Finalize()
		Class_Terminate_Renamed()
		MyBase.Finalize()
	End Sub
End Class






