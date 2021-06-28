Option Strict Off
Option Explicit On
Public Class Fires
	Implements System.Collections.IEnumerable
	'variable local para contener colección
	Private mCol As Collection
	
	Private pexeConstruct As New eRemoteDB.ConstructSelect
	
	Public Function Add(ByVal nBranch As Integer, ByVal nCertif As Double, ByVal nPolicy As Double, ByVal sBranchName As String, ByVal sCliename As String, ByVal nProduct As Integer, ByVal sDescript As String) As Fire
		
		'crear un nuevo objeto
		Dim objNewMember As Fire
		objNewMember = New Fire
		
		
		'establecer las propiedades que se transfieren al método
		
		With objNewMember
			.nProduct = nProduct
			.nBranch = nBranch
			.nCertif = nCertif
			.nPolicy = nPolicy
			.sBranchName = sBranchName
			.sCliename = sCliename
			.sDescript = sDescript
		End With
		
		mCol.Add(objNewMember)
		
		
		
		'devolver el objeto creado
		Add = objNewMember
		'UPGRADE_NOTE: Object objNewMember may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		objNewMember = Nothing
		
		
	End Function
	
	Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As Fire
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
	
	Private Sub insReaVeh()
        'Dim lstrCondition As String
		Dim lstrHead As String
        'Dim lstrDate As String
		
		pexeConstruct.NameFatherTable("Fire", "Fire")
		pexeConstruct.SelectClause("Fire.nBranch, Fire.nPolicy, Fire.nCertif,  " & "Prod.sDescript , " & "Prod.nProduct, " & "Cli.sCliename, " & "T10.sDescript sBranchName ")
		
		lstrHead = "Prod.nBranch = Fire.nBranch " & " AND Prod.nProduct = Fire.nProduct  " & " AND Prod.sStatregt = '1' "
		
		
		pexeConstruct.RelationsTables(eRemoteDB.ConstructSelect.eRelationTables.RelInner, "Prodmaster", "Prod", lstrHead)
		
		pexeConstruct.RelationsTables(eRemoteDB.ConstructSelect.eRelationTables.RelInner, "Policy", "Pol", "     Pol.sCertype = Fire.sCertype " & " AND Pol.nBranch = Fire.nBranch " & " AND Pol.nProduct = Fire.nProduct " & " AND Pol.nPolicy = Fire.nPolicy " & " AND (Pol.sStatus_pol < '2' " & "  OR Pol.sStatus_pol > '3') " & " AND Pol.dNulldate IS NULL ")
		
		
		pexeConstruct.RelationsTables(eRemoteDB.ConstructSelect.eRelationTables.RelInner, "Table10", "T10", "     T10.nBranch = Fire.nBranch" & " AND T10.sStatregt = '1' ")
		
		pexeConstruct.RelationsTables(eRemoteDB.ConstructSelect.eRelationTables.RelInner, "Roles", "Rol", " Rol.sCertype = Fire.sCertype " & " AND Rol.nBranch = Fire.nBranch " & " AND Rol.nProduct = Fire.nProduct " & " AND Rol.nPolicy = Fire.nPolicy " & " AND Rol.nCertif = Fire.nCertif " & " AND Rol.nRole = 2" & " AND Fire.dEffecdate <= Rol.dEffecdate " & " AND (Rol.dNulldate IS NULL " & " OR (Fire.dEffecdate < Rol.dNulldate)) ")
		pexeConstruct.RelationsTables(eRemoteDB.ConstructSelect.eRelationTables.RelInner, "Client", "Cli", "Cli.sClient= Rol.sClient ")
		
	End Sub
	
	
	'%Find_INC001_K: Esta función se encarga de de buscar la colección de datos de acuerdo
	'%a las condiciones que el usuario
	Public Function Find_INC001_K(ByVal sCodispl As String, ByVal sAction As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPayfreq As Integer, ByVal nCapital As Double, ByVal nPremium As Double, ByVal dEffecdate As Date, ByVal dNulldate As Date, ByVal nTypePolicy As Integer, ByVal nArticle As Integer, ByVal nDetailArt As Integer, ByVal nActivityCat As Integer, ByVal nConstcat As Integer, ByVal nFloor_quan As Integer, ByVal nSpCombType As Integer, ByVal nSideCloseType As Integer, ByVal nIndPeriod As Integer, ByVal nRoofType As Integer) As Boolean
		Dim lstrWhere As String
		Dim lrecselect As eRemoteDB.Execute
		
		
		Call insReaVeh()
		'+ Campo Ramo
		Call pexeConstruct.WhereClause("Fire.nBranch", eRemoteDB.ConstructSelect.eTypeValue.TypCNumeric, Str(nBranch))
		
		'+ Campo Producto.
		If nProduct > 0 Then
			Call pexeConstruct.WhereClause("Fire.nProduct", eRemoteDB.ConstructSelect.eTypeValue.TypCNumeric, CStr(nProduct), eRemoteDB.ConstructSelect.eWordConnection.eAnd)
		End If
		'+ Campo Tipo de pago.
		
		If nPayfreq > 0 Then
			Call pexeConstruct.WhereClause("Pol.nPayfreq", eRemoteDB.ConstructSelect.eTypeValue.TypCNumeric, Str(nPayfreq), eRemoteDB.ConstructSelect.eWordConnection.eAnd)
		End If
		
		'+ Campo Capital.
		
		If nCapital > 0 Then
			Call pexeConstruct.WhereClause("Fire.nCapital", eRemoteDB.ConstructSelect.eTypeValue.TypCNumeric, Str(nCapital), eRemoteDB.ConstructSelect.eWordConnection.eAnd)
		End If
		
		'+Validación de la prima
		
		If nPremium > 0 Then
			Call pexeConstruct.WhereClause("Fire.nPremium", eRemoteDB.ConstructSelect.eTypeValue.TypCNumeric, Str(nPremium), eRemoteDB.ConstructSelect.eWordConnection.eAnd)
		End If
		'+Validación del campo Fecha de Efecto.
		
		If dEffecdate <> dtmNull Then
			Call pexeConstruct.WhereClause("Fire.dNulldate", eRemoteDB.ConstructSelect.eTypeValue.TypCDate, ">" & Trim(CStr(dEffecdate)), eRemoteDB.ConstructSelect.eWordConnection.eAnd, "(", "OR Fire.dNulldate IS NULL)")
		End If
		
		'+Validación del campo Fecha de Vencimiento.
		
		If dNulldate <> dtmNull Then
			Call pexeConstruct.WhereClause("Fire.dExpirdat", eRemoteDB.ConstructSelect.eTypeValue.TypCDate, "=" & Trim(CStr(dNulldate)), eRemoteDB.ConstructSelect.eWordConnection.eAnd)
		End If
		
		
		'+Validación del campo Tipo de Póliza.
		
		If nTypePolicy > 0 Then
			Call pexeConstruct.WhereClause("Pol.sPolitype", eRemoteDB.ConstructSelect.eTypeValue.TypCString, Trim(Str(nTypePolicy)), eRemoteDB.ConstructSelect.eWordConnection.eAnd)
		End If
		
		'+Campo Actividad
		
		If nArticle > 0 Then
			Call pexeConstruct.WhereClause("Fire.nArticle", eRemoteDB.ConstructSelect.eTypeValue.TypCNumeric, Trim(Str(nArticle)), eRemoteDB.ConstructSelect.eWordConnection.eAnd)
		End If
		
		'+ Campo detalle
		
		If nDetailArt > 0 Then
			Call pexeConstruct.WhereClause("Fire.nDetailArt", eRemoteDB.ConstructSelect.eTypeValue.TypCNumeric, Trim(Str(nDetailArt)), eRemoteDB.ConstructSelect.eWordConnection.eAnd)
		End If
		
		'+ Campo Categoría de actividad
		
		If nActivityCat > 0 Then
			Call pexeConstruct.WhereClause("Fire.nActivityCat", eRemoteDB.ConstructSelect.eTypeValue.TypCNumeric, Trim(Str(nActivityCat)), eRemoteDB.ConstructSelect.eWordConnection.eAnd)
		End If
		
		
		'+ Campo categoría de construcción
		
		If nConstcat > 0 Then
			Call pexeConstruct.WhereClause("Fire.nConstCat", eRemoteDB.ConstructSelect.eTypeValue.TypCNumeric, Trim(Str(nConstcat)), eRemoteDB.ConstructSelect.eWordConnection.eAnd)
		End If
		
		
		'+ Campo numéro de plantas
		
		If nFloor_quan > 0 Then
			Call pexeConstruct.WhereClause("Fire.nFloor_quan", eRemoteDB.ConstructSelect.eTypeValue.TypCNumeric, Trim(Str(nFloor_quan)), eRemoteDB.ConstructSelect.eWordConnection.eAnd)
		End If
		
		
		'+ Capo tipo de riesgo
		
		If nSpCombType > 0 Then
			Call pexeConstruct.WhereClause("Fire.nSpCombType", eRemoteDB.ConstructSelect.eTypeValue.TypCNumeric, Trim(Str(nSpCombType)), eRemoteDB.ConstructSelect.eWordConnection.eAnd)
		End If
		
		'+ Campo tipo de cerramiento
		
		If nSideCloseType > 0 Then
			Call pexeConstruct.WhereClause("Fire.nSideCloseType", eRemoteDB.ConstructSelect.eTypeValue.TypCNumeric, Trim(Str(nSideCloseType)), eRemoteDB.ConstructSelect.eWordConnection.eAnd)
		End If
		
		'+ Campo Periodo de indemnización
		
		If nIndPeriod > 0 Then
			Call pexeConstruct.WhereClause("Fire.nIndPeriod", eRemoteDB.ConstructSelect.eTypeValue.TypCNumeric, Trim(Str(nIndPeriod)), eRemoteDB.ConstructSelect.eWordConnection.eAnd)
		End If
		
		'+ Campo tipo de techo
		
		If nRoofType > 0 Then
			Call pexeConstruct.WhereClause("Fire.nRoofType", eRemoteDB.ConstructSelect.eTypeValue.TypCNumeric, Trim(Str(nRoofType)), eRemoteDB.ConstructSelect.eWordConnection.eAnd)
		End If
		
		
		lstrWhere = pexeConstruct.Answer
		lrecselect = New eRemoteDB.Execute
		With lrecselect
			.SQL = Trim(lstrWhere)
			If .Run Then
				Do While Not .EOF
					Call Add(.FieldToClass("nBranch"), .FieldToClass("nCertif"), .FieldToClass("nPolicy"), .FieldToClass("sBranchName"), .FieldToClass("sClieName"), .FieldToClass("nProduct"), .FieldToClass("sDescript"))
					
					.RNext()
				Loop 
				Find_INC001_K = True
				.RCloseRec()
			End If
		End With
		
		'UPGRADE_NOTE: Object pexeConstruct may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		pexeConstruct = Nothing
		'UPGRADE_NOTE: Object lrecselect may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecselect = Nothing
	End Function
End Class






