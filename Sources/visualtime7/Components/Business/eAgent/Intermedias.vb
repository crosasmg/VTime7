Option Strict Off
Option Explicit On
Public Class Intermedias
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
	Public Function Add(ByVal nIntermed As Integer, ByVal sClient As String, ByVal nComtabge As Integer, ByVal nComtabli As Integer, ByVal dInpdate As Date, ByVal nInt_status As Integer, ByVal nIntertyp As Integer, ByVal nNullcode As Integer, ByVal dNulldate As Date, ByVal nOffice As Integer, ByVal nSupervis As Integer, ByVal nTable_cod As Integer, ByVal nTax As Double, ByVal nUsercode As Integer, ByVal sCol_agree As String, ByVal nNotenum As Integer, ByVal nEco_sche As Integer, ByVal sInter_id As String, ByVal sAgreeInt As String, Optional ByVal sCliename As String = "", Optional ByVal sParticin As String = "", Optional ByVal sIntertyp As String = "", Optional ByVal sOfficeDes As String = "", Optional ByVal sOrgName As String = "", Optional ByVal dCommidate As Date = #12:00:00 AM#, Optional ByRef sClientDig As String = "") As Intermedia
		
		'create a new object
		Dim objNewMember As Intermedia
		objNewMember = New Intermedia
		
		With objNewMember
			.nIntermed = nIntermed
			.sClient = sClient
			.nComtabge = nComtabge
			.nComtabli = nComtabli
			.dInpdate = dInpdate
			.nInt_status = nInt_status
			.nIntertyp = nIntertyp
			.nNullcode = nNullcode
			.dNulldate = dNulldate
			.nOffice = nOffice
			.nSupervis = nSupervis
			.nTable_cod = nTable_cod
			.nTax = nTax
			.nUsercode = nUsercode
			.sCol_agree = sCol_agree
			.nNotenum = nNotenum
			.nEco_sche = nEco_sche
			.sInter_id = sInter_id
			.sAgreeInt = sAgreeInt
			.sCliename = sCliename
			.sParticin = sParticin
			.sIntertyp = sIntertyp
			.sOfficeDes = sOfficeDes
			.sOrgName = sOrgName
			.dCommidate = dCommidate
			.sClientDig = sClientDig
		End With
		
		mCol.Add(objNewMember)
		'return the object created
		Add = objNewMember
		'UPGRADE_NOTE: Object objNewMember may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		objNewMember = Nothing
		
	End Function
	
	'**%Find: This method fills the collection with records from the table "Intermedia" returing TRUE or FALSE
	'**%depending on the existence of the records
	'%Find: Este metodo carga la coleccion de elementos de la tabla "XXXXXX" devolviendo Verdadero o
	'%falso, dependiendo de la existencia de los registros.
    Public Function FindAGC006(ByVal nType As Integer, ByVal sClient As String, ByVal sAgent As String, ByVal sAgentName As String, ByVal nState As Integer, ByVal sAgentOrg As String, ByVal sAgentOrgName As String, ByVal nOffice As Integer, ByVal sDateAnull As String, Optional ByVal sCommidate As String = "", Optional ByVal nrows As Integer = 1) As Boolean
        Dim lrecFindAGC006 As eRemoteDB.Execute
        Dim lintTotalRecords As Integer

        On Error GoTo FindAGC006_Err

        lrecFindAGC006 = New eRemoteDB.Execute


        With lrecFindAGC006
            .StoredProcedure = "REAINTERMEDIA_PAR"
            .Parameters.Add("nIntertyp", IIf(nType = 0, eRemoteDB.Constants.intNull, nType), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nIntermed", IIf(sAgent = "", eRemoteDB.Constants.intNull, sAgent), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nSupervis", IIf(sAgentOrg = "", eRemoteDB.Constants.intNull, sAgentOrg), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nOffice", IIf(nOffice = 0, eRemoteDB.Constants.intNull, nOffice), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nInt_status", IIf(nState = 0, eRemoteDB.Constants.intNull, nState), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dCommidate", IIf(sCommidate = "", dtmNull, sCommidate), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dNulldate", IIf(sDateAnull = "", dtmNull, sDateAnull), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nrows", IIf(nrows = "0", 1, nrows), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If .Run(True) Then
                Do While Not .EOF
                    Call Add(.FieldToClass("nIntermed"), .FieldToClass("sClient"), eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, dtmNull, .FieldToClass("nInt_status"), .FieldToClass("nIntertyp"), eRemoteDB.Constants.intNull, .FieldToClass("dNulldate"), .FieldToClass("nOffice"), .FieldToClass("nSupervis"), eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, String.Empty, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, String.Empty, String.Empty, .FieldToClass("sCliename"), String.Empty, .FieldToClass("sIntertyp"), .FieldToClass("sOfficeDes"), .FieldToClass("sOrgName"), .FieldToClass("dCommidate"), .FieldToClass("sClientDig"))
                    .RNext()
                Loop
                FindAGC006 = True
                .RCloseRec()
            Else
                FindAGC006 = False
            End If
        End With


FindAGC006_Err:
        If Err.Number Then
            FindAGC006 = False
        End If
        On Error GoTo 0
        'UPGRADE_NOTE: Object lrecFindAGC006 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecFindAGC006 = Nothing
    End Function
	
	'***Item: Returns an element of the collection (acording to the index)
	'*Item: Devuelve un elemento de la colección (segun índice)
	Public ReadOnly Property Item(ByVal vntIndexKey As Object) As Intermedia
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
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
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
	'UPGRADE_NOTE: Class_Terminate was upgraded to Class_Terminate_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Terminate_Renamed()
		'UPGRADE_NOTE: Object mCol may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		mCol = Nothing
	End Sub
	Protected Overrides Sub Finalize()
		Class_Terminate_Renamed()
		MyBase.Finalize()
	End Sub
	
	'**% Add: adds a new instance
	'% Add: Añade una nueva instancia
	Public Function AddAGL008(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal dStartdate As Date, ByVal dExpirdat As Date, ByVal nIntermedPol As Integer, ByVal nPremanual As Double, ByVal nComanual As Double) As Intermedia
		'create a new object
		Dim objNewMember As Intermedia
		objNewMember = New Intermedia
		
		With objNewMember
			.sCertype = sCertype
			.nBranch = nBranch
			.nProduct = nProduct
			.nPolicy = nPolicy
			.dStartdate = dStartdate
			.dExpirdat = dExpirdat
			.nIntermedPol = nIntermedPol
			.nPremanual = nPremanual
			.nComanual = nComanual
		End With
		
		mCol.Add(objNewMember)
		'return the object created
		AddAGL008 = objNewMember
		'UPGRADE_NOTE: Object objNewMember may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		objNewMember = Nothing
		
	End Function
	
	'FindAGL008: Obtiene registros de pólizas asociadas a un intermediario para la transacción AGL008.
	Public Function FindAGL008(ByVal dDateProcess As Date, ByVal nInsur_area As Integer, ByVal sTypeBusiness As String, ByVal sTypePolicy As String, ByVal nMunicipality As Integer, ByVal nIntermed As Integer, Optional ByVal nBranch As Integer = 0, Optional ByVal nProduct As Integer = 0) As Boolean
		Dim lrecPol_Commission As eRemoteDB.Execute
		
		On Error GoTo FindAGL008_Err
		
		lrecPol_Commission = New eRemoteDB.Execute
		
		'+ Define all parameters for the stored procedures 'ReaPolCommi'.
		With lrecPol_Commission
			.StoredProcedure = "ReaPolCommi"
			.Parameters.Add("dDateProcess", CDate(dDateProcess), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nInsur_Area", nInsur_area, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sTypeBusiness", sTypeBusiness, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sTypePolicy", sTypePolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nMunicipality", nMunicipality, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nIntermed", nIntermed, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run(True) Then
				Do While Not .EOF
					Call AddAGL008(.FieldToClass("sCertype"), .FieldToClass("nBranch"), .FieldToClass("nProduct"), .FieldToClass("nPolicy"), .FieldToClass("dStartdate"), .FieldToClass("dExpirdat"), .FieldToClass("nIntermedPol"), .FieldToClass("nPremanual"), .FieldToClass("nComanual"))
					.RNext()
				Loop 
				FindAGL008 = True
				.RCloseRec()
			Else
				FindAGL008 = False
			End If
		End With
		
		'UPGRADE_NOTE: Object lrecPol_Commission may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecPol_Commission = Nothing
		
FindAGL008_Err: 
		If Err.Number Then
			FindAGL008 = False
		End If
		On Error GoTo 0
		
	End Function
End Class






