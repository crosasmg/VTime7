Option Strict Off
Option Explicit On
Public Class Saapv_pols
	Implements System.Collections.IEnumerable
	'%-------------------------------------------------------%'
	'% $Workfile:: Intermedias.cls                          $%'
	'% $Author:: Nvaplat22                                  $%'
	'% $Date:: 31/05/04 8:18p                               $%'
	'% $Revision:: 20                                       $%'
	'%-------------------------------------------------------%'
	
	'local variable to hold collection
	Private mCol As Collection
	
	'- Se define la variable para almacenar el nro. de registros que devuelve la consulta por condición
	Public RecordCount As Double
	
	
	'**% Add: adds a new instance to the Intermedia class to the collection
	'% Add: Añade una nueva instancia de la clase Intermedia a la colección
	Public Function Add(ByVal nCod_saapv As Double) As Saapv_pol
		
		'create a new object
		Dim objNewMember As Saapv_pol
		objNewMember = New Saapv_pol
		
		With objNewMember
			.nCod_saapv = nCod_saapv
		End With
		
		mCol.Add(objNewMember)
		'return the object created
		Add = objNewMember
		objNewMember = Nothing
		
	End Function
	
	'**% Add: adds a new instance to the Intermedia class to the collection
	'% Add: Añade una nueva instancia de la clase Intermedia a la colección
	Public Function Add_vi7502(ByVal nCod_saapv As Double, ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal nType_saapv As Integer, ByVal dissue_dat As Date, ByVal nstatus_saapv As Integer, ByVal scheckamend As String, ByVal scheckrequest As String, ByVal nBordereaux As Double, ByVal nAmount_Rel As Double, ByVal sStatus As String, ByVal sAutodif As String, ByVal nNotenum As Double, ByVal nInstitution As Integer, ByVal ntype_ameapv As Double, ByVal dLimitDate As Date) As Saapv_pol
		
		'create a new object
		Dim objNewMember As Saapv_pol
		objNewMember = New Saapv_pol
		
		With objNewMember
			.nCod_saapv = nCod_saapv
			.sCertype = sCertype
			.nBranch = nBranch
			.nProduct = nProduct
			.nPolicy = nPolicy
			.nCertif = nCertif
			.nType_saapv = nType_saapv
			.dissue_dat = dissue_dat
			.nstatus_saapv = nstatus_saapv
			.scheckamend = scheckamend
			.scheckrequest = scheckrequest
			.nBordereaux = nBordereaux
			.nAmount_Rel = nAmount_Rel
			.sStatus = sStatus
			.sAutodif = sAutodif
			.nNotenum = nNotenum
			.nInstitution = nInstitution
			.ntype_ameapv = ntype_ameapv
			.dLimitDate = dLimitDate
		End With
		
		mCol.Add(objNewMember)
		'return the object created
		Add_vi7502 = objNewMember
		objNewMember = Nothing
		
	End Function
	
	
	'**%Find: This method fills the collection with records from the table "Intermedia" returing TRUE or FALSE
	'**%depending on the existence of the records
	'%Find: Este metodo carga la coleccion de elementos de la tabla "XXXXXX" devolviendo Verdadero o
	'%falso, dependiendo de la existencia de los registros.
	Public Function Find(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double) As Boolean
		Dim lrecFind As eRemoteDB.Execute
		Dim lintTotalRecords As Integer
		
		On Error GoTo Find_Err
		
		lrecFind = New eRemoteDB.Execute
		
		With lrecFind
			.StoredProcedure = "insVi7501pkg.Find"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(True) Then
				Do While Not .EOF
					Call Add(.FieldToClass("ncod_saapv"))
					.RNext()
				Loop 
				Find = True
				.RCloseRec()
			Else
				Find = False
			End If
		End With
		
Find_Err: 
		If Err.Number Then
			Find = False
		End If
		On Error GoTo 0
		lrecFind = Nothing
	End Function
	
	Public Function Find_VI7502(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal sClient As String, ByVal nCod_saapv As Double, ByVal nInstitution As Integer) As Boolean
		Dim lrecFind As eRemoteDB.Execute
		Dim lintCod_saapv As Double
		
		On Error GoTo Find_VI7502_Err
		lrecFind = New eRemoteDB.Execute
		
		lintCod_saapv = 0
		
		With lrecFind
			
			.StoredProcedure = "insVi7502pkg.find"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("ncod_saapv", nCod_saapv, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nInstitution", nInstitution, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(True) Then
				Do While Not .EOF
					If lintCod_saapv <> .FieldToClass("nCod_saapv") Then
						Call Add_vi7502(.FieldToClass("nCod_saapv"), .FieldToClass("sCertype"), .FieldToClass("nBranch"), .FieldToClass("nProduct"), .FieldToClass("nPolicy"), .FieldToClass("nCertif"), .FieldToClass("nType_saapv"), .FieldToClass("dissue_dat"), .FieldToClass("nstatus_saapv"), .FieldToClass("scheckamend"), .FieldToClass("scheckrequest"), .FieldToClass("nBordereaux"), .FieldToClass("nAmount_Rel"), .FieldToClass("sStatus"), .FieldToClass("sAutodif"), .FieldToClass("nNotenum"), .FieldToClass("nInstitution"), .FieldToClass("ntype_ameapv"), .FieldToClass("dLimitDate"))
						
					End If
					lintCod_saapv = .FieldToClass("nCod_saapv")
					.RNext()
				Loop 
				Find_VI7502 = True
				.RCloseRec()
			Else
				Find_VI7502 = False
			End If
		End With
		
Find_VI7502_Err: 
		If Err.Number Then
			Find_VI7502 = False
		End If
		On Error GoTo 0
		lrecFind = Nothing
	End Function
	
	
	'***Item: Returns an element of the collection (acording to the index)
	'*Item: Devuelve un elemento de la colección (segun índice)
	Public ReadOnly Property Item(ByVal vntIndexKey As Object) As Saapv_pol
		Get
			'used when referencing an element in the collection
			'vntIndexKey contains either the Index or Key to the collection,
			'this is why it is declared as a Variant
			'Syntax: Set foo = x.Item(xyz) or Set foo = x.Item(5)
			'-----------------------------------------------------------
			Item = mCol.Item(vntIndexKey)
		End Get
	End Property
	
	'***Count: Returns the number of elements that the collection has
	'*Count: Devuelve el número de elementos que posee la colección
	Public ReadOnly Property Count() As Integer
		Get
			'used when retrieving the number of elements in the
			'collection. Syntax: Debug.Print x.Count
			Count = mCol.Count()
		End Get
	End Property
	
	'***NewEnum: Enumerates the collection for use in a For Each...Next loop
	'*NewEnum: Permite enumerar la colección para utilizarla en un ciclo For Each... Next
	'UPGRADE_NOTE: NewEnum property was commented out. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="B3FC1610-34F3-43F5-86B7-16C984F0E88E"'
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
	
	
	'**%Remove: Deletes an element from the collection
	'%Remove: Elimina un elemento de la colección
	Public Sub Remove(ByRef vntIndexKey As Object)
		'used when removing an element from the collection
		'vntIndexKey contains either the Index or Key, which is why
		'it is declared as a Variant
		'Syntax: x.Remove(xyz)
		mCol.Remove(vntIndexKey)
	End Sub
	
	'**%Class_Initialize: Controls the creation of an instance of the collection
	'%Class_Initialize: Controla la creación de una instancia de la colección
	Private Sub Class_Initialize_Renamed()
		'+ Se crea la coleccion cuando la clase se esta creando
		'**+creates the collection when this class is created
		mCol = New Collection
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
	
	'**%Class_Terminate: Controls the destruction of an instance of the collection
	'%Class_Terminate: Controla la destrucción de una instancia de la colección
	Private Sub Class_Terminate_Renamed()
		mCol = Nothing
	End Sub
	Protected Overrides Sub Finalize()
		Class_Terminate_Renamed()
		MyBase.Finalize()
	End Sub
End Class






