Option Strict Off
Option Explicit On
Public Class Cl_m_covers
	Implements System.Collections.IEnumerable
	'%-------------------------------------------------------%'
	'% $Workfile:: Cl_m_covers.cls                          $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 1:34p                                $%'
	'% $Revision:: 7                                        $%'
	'%-------------------------------------------------------%'
	
	'+ local variable to hold collection
	
	Private mCol As Collection
	
	Public Function Add(ByVal nAmount As Double, ByVal sCurrency As String, ByVal sGencov As String, ByVal nVat_amount As Double, ByVal sConcept As String) As Cl_m_cover
		
		'+ Create a new object
		
		Dim objNewMember As Cl_m_cover
		objNewMember = New Cl_m_cover
		
		
		With objNewMember
			.nAmount = nAmount
			.sCurrency = sCurrency
			.sGencov = sGencov
			.nVat_amount = nVat_amount
			.sConcept = sConcept
		End With
		
		'set the properties passed into the method
		mCol.Add(objNewMember)
		
		'return the object created
		Add = objNewMember
		objNewMember = Nothing
		
Add_err: 
		If Err.Number Then
            Add = Nothing
		End If
		On Error GoTo 0
		
		'return the object created
		Add = objNewMember
		objNewMember = Nothing
	End Function
	
	Public Function AddProvider(ByVal nServ_Order As Double, ByVal sDescript As String, ByVal sClient As String) As Cl_m_cover
		'+ Create a new object
		Dim objNewMember As Cl_m_cover
		objNewMember = New Cl_m_cover
		
		With objNewMember
			.nServ_Order = nServ_Order
			.sDescript = sDescript
			.sClient = sClient
		End With
		
		'set the properties passed into the method
		mCol.Add(objNewMember)
		
		'return the object created
		AddProvider = objNewMember
		objNewMember = Nothing
		
Add_err: 
		If Err.Number Then
            AddProvider = Nothing
		End If
		On Error GoTo 0
		
		'return the object created
		AddProvider = objNewMember
		objNewMember = Nothing
	End Function
	Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As Cl_m_cover
		Get
			'+ Used when referencing an element in the collection
			'+ vntIndexKey contains either the Index or Key to the collection,
			'+ this is why it is declared as a Variant
			'+ Syntax: Set foo = x.Item(xyz) or Set foo = x.Item(5)
			
			Item = mCol.Item(vntIndexKey)
			
		End Get
	End Property
	
	Public ReadOnly Property Count() As Integer
		Get
			'+ Used when retrieving the number of elements in the
			'+ collection. Syntax: Debug.Print x.Count
			
			Count = mCol.Count()
			
		End Get
	End Property
	
	'UPGRADE_NOTE: NewEnum property was commented out. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="B3FC1610-34F3-43F5-86B7-16C984F0E88E"'
	'Public ReadOnly Property NewEnum() As stdole.IUnknown
		'Get
			'+ This property allows you to enumerate
			'+ This collection with the For...Each syntax
			'
			'NewEnum = mCol._NewEnum
			'
		'End Get
	'End Property
	
	Public Function GetEnumerator() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
				GetEnumerator = mCol.GetEnumerator
	End Function
	
	Public Sub Remove(ByRef vntIndexKey As Object)
		'+ Used when removing an element from the collection
		'+ vntIndexKey contains either the Index or Key, which is why
		'+ it is declared as a Variant
		'+ Syntax: x.Remove(xyz)
		
		mCol.Remove(vntIndexKey)
		
	End Sub
	
	Private Sub Class_Initialize_Renamed()
		'+ Creates the collection when this class is created
		
		mCol = New Collection
		
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
	
	Private Sub Class_Terminate_Renamed()
		'+ Destroys collection when this class is terminated
		
		mCol = Nothing
		
	End Sub
	Protected Overrides Sub Finalize()
		Class_Terminate_Renamed()
		MyBase.Finalize()
	End Sub
	
	
	'**% Find: It allows to make the reading of the table to reaCl_m_Cover.
	'% Find: Permite realizar la lectura de la tabla reaCl_m_Cover.
	Public Function Find(ByVal nClaim As Double, ByVal nCase_num As Integer, ByVal nDeman_type As Integer, ByVal nOper_type As Integer, Optional ByVal lblnFind As Boolean = True) As Boolean
		Dim lrecReaCl_m_cover As eRemoteDB.Execute
		
		lrecReaCl_m_cover = New eRemoteDB.Execute
		
		On Error GoTo Find_Err
		
		'**+ Parameters definition for the stored procedure 'insudb.reaTar_Hail_In_Force'.
		'+ Definición de parámetros para stored procedure 'insudb.reaTar_Hail_In_Force'.
		
		With lrecReaCl_m_cover
			.StoredProcedure = "reaCl_m_Cover"
			
			.Parameters.Add("nClaim", nClaim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCase_num", nCase_num, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDeman_type", nDeman_type, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nOper_type", nOper_type, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run Then
				
				Do While Not .EOF
					Call Add(.FieldToClass("nAmount"), .FieldToClass("sCurrency"), .FieldToClass("sGencov"), .FieldToClass("nVat_amount"), .FieldToClass("sConcept"))
					.RNext()
				Loop 
				Find = True
				.RCloseRec()
			Else
				Find = False
			End If
		End With
		
		lrecReaCl_m_cover = Nothing
		
		
Find_Err: 
		If Err.Number Then
			Find = False
		End If
		
		On Error GoTo 0
	End Function
	
	'**% Find: It allows to make the reading of the table to reaProvider.
	'% Find: Permite realizar la lectura de la tabla reaProvider.
	Public Function FindProvider(ByVal nClaim As Double, ByVal nCase_num As Integer, ByVal nOper_type As Integer) As Boolean
		Dim lrec As eRemoteDB.Execute
		
		lrec = New eRemoteDB.Execute
		
		On Error GoTo Find_Err
		
		'**+ Parameters definition for the stored procedure 'reaProvider'.
		'+ Definición de parámetros para stored procedure 'reaProvider'.
		
		With lrec
			.StoredProcedure = "reaProvider"
			
			.Parameters.Add("nClaim", nClaim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCase_num", nCase_num, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nOper_type", nOper_type, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run Then
				Do While Not .EOF
					Call AddProvider(.FieldToClass("nServ_Order"), .FieldToClass("sDescript"), .FieldToClass("sClient"))
					.RNext()
				Loop 
				FindProvider = True
				.RCloseRec()
			Else
				FindProvider = False
			End If
		End With
		
		lrec = Nothing
		
		
Find_Err: 
		If Err.Number Then
			FindProvider = False
		End If
		
		On Error GoTo 0
	End Function
End Class






