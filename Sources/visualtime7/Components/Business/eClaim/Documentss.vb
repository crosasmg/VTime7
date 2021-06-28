Option Strict Off
Option Explicit On
Public Class Documentss
	Implements System.Collections.IEnumerable
	'%-------------------------------------------------------%'
	'% $Workfile:: Documentss.cls                           $%'
	'% $Author:: Jrengifo                                   $%'
	'% $Date:: 2-05-13 9:19                                 $%'
	'% $Revision:: 2                                        $%'
	'%-------------------------------------------------------%'
	'local variable to hold collection
	Private mCol As Collection
	'**% Add: Method that adds an element to the collection
	'% Add. Método que agrega un elemento a la colección
    Public Function Add(ByVal nAction As Integer,
                        ByVal nClaim As Double,
                        ByVal nCase_num As Integer,
                        ByVal nDeman_type As Integer,
                        ByVal sClient As String,
                        ByVal nCode As Integer,
                        ByVal nDoc_code As Integer,
                        ByVal sDescript As String,
                        ByVal dRecepdate As Date,
                        ByVal nUserCode As Integer,
                        ByVal nId As Integer,
                        ByVal nDocnumbe As Double,
                        ByVal nQuantity As Integer,
                        ByVal dPropo_date As Date,
                        ByVal dPrescDate As Date,
                        ByVal nDays_presc As Integer,
                        ByVal sDesc_docu As String,
                        ByVal nConsec As Short,
                        ByVal nAmount As Double,
                        ByVal nCurrency As Integer) As Documents

        'create a new object
        Dim objNewMember As eClaim.Documents
        objNewMember = New eClaim.Documents

        With objNewMember
            .nAction = nAction
            .nClaim = nClaim
            .nCase_num = nCase_num
            .nDeman_type = nDeman_type
            .sClient = sClient
            .nCode = nCode
            .nDoc_code = nDoc_code
            .sDescript = sDescript
            .sDesc_docu = sDesc_docu
            .dRecepdate = dRecepdate
            .nId = nId
            .nDocnumbe = nDocnumbe
            .nQuantity = nQuantity
            .dPropo_date = dPropo_date
            .dPrescDate = dPrescDate
            .nDays_presc = nDays_presc
            .nConsec = nConsec
            .nAmount = nAmount
            .nCurrency = nCurrency
        End With

        mCol.Add(objNewMember)
        'return the object created
        Add = objNewMember
        objNewMember = Nothing

    End Function
	'**%Update. Method that for each element of the collection updates the collection
	'**%and the table in the data base
	'% Update. Método que por cada elemento de la colección,
	'% actualiza la colección y la tabla de la base de datos
	Public Function Update() As Boolean
		Dim lclsDocuments As eClaim.Documents
		
		Update = True
		
		For	Each lclsDocuments In mCol
			With lclsDocuments
				If .nAction <> 0 Then
					Update = .Update_DocumentsGeneric(.nClaim, .nDoc_code, .nCase_num, .nDeman_type, .sClient, .nId, .dRecepdate, .nDocnumbe, .nQuantity, .dPropo_date, .dPrescDate, .nAction, .nUserCode, .sDesc_docu, .nConsec)
					If .nAction = 3 And Update Then
						mCol.Remove(("DC" & .nClaim & .nCase_num & .nDeman_type & .sClient & .nCode & .nId))
					End If
				End If
			End With
		Next lclsDocuments
	End Function
	'**% Find: Method that find the records in the table of the data base and add
	'**%elements to the collection
	'% Find. Método que busca los registros en la tabla de la base de datos y
	' agrega elementos a la colección
	Public Function Find(ByVal nClaim As Double, ByVal nCase_num As Integer, ByVal nDeman_type As Integer, ByVal sClient As String, ByVal nId As Integer, ByVal nBranch As Integer) As Boolean
		Dim lrecDocuments As eRemoteDB.Execute
		
		On Error GoTo Find_Err
		lrecDocuments = New eRemoteDB.Execute
		
		With lrecDocuments
			.StoredProcedure = "reaDocuments"
			.Parameters.Add("nClaim", nClaim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCase_num", nCase_num, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDeman_type", nDeman_type, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nId", nId, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				mCol = Nothing
				mCol = New Collection
				Do While Not .EOF
                    Call Add(0,
                             nClaim,
                             nCase_num,
                             nDeman_type,
                             sClient,
                             .FieldToClass("nCode"),
                             .FieldToClass("nDoc_code"),
                             .FieldToClass("sDescript"),
                             .FieldToClass("dRecepdate"),
                             .FieldToClass("nUsercode"),
                             .FieldToClass("nId"),
                             .FieldToClass("nDocnumbe"),
                             .FieldToClass("nQuantity"),
                             .FieldToClass("dPropo_date"),
                             .FieldToClass("dPrescdate"),
                             .FieldToClass("nDays_Presc"),
                             .FieldToClass("SDESC_DOCU"),
                             .FieldToClass("NCONSEC"),
                             .FieldToClass("NAMOUNT"),
                             .FieldToClass("NCURRENCY"))
					.RNext()
				Loop 
				.RCloseRec()
				Find = True
			Else
				Find = False
			End If
		End With
		
		lrecDocuments = Nothing
		
Find_Err: 
		If Err.Number Then
			Find = False
		End If
		On Error GoTo 0
	End Function
	
	
	Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As Documents
		Get
			'used when referencing an element in the collection
			'vntIndexKey contains either the Index or Key to the collection,
			'this is why it is declared as a Variant
			'Syntax: Set foo = x.Item(xyz) or Set foo = x.Item(5)
			Item = mCol.Item(vntIndexKey)
		End Get
	End Property
	
	
	
	Public ReadOnly Property Count() As Integer
		Get
			'used when retrieving the number of elements in the
			'collection. Syntax: Debug.Print x.Count
			Count = mCol.Count()
		End Get
	End Property
	
	
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
End Class






