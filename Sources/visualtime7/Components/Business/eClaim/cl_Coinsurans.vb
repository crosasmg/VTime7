Option Strict Off
Option Explicit On
Public Class cl_Coinsurans
	Implements System.Collections.IEnumerable
	'-Collecion de objetos CL_Coinsuran
	Private mCol As Collection
	'%Add: Agrega un objeto a la colección
	Public Function Add(ByVal nClaim As Double, ByVal nCase_num As Integer, ByVal nDeman_type As Integer, ByVal nCompany As Double, ByVal sCompany As String, ByVal dEffecdate As Date, ByVal nShare As Double, ByVal nExpenses As Double, ByVal sSel As String) As Cl_Coinsuran
		Dim objNewMember As Cl_Coinsuran
		objNewMember = New Cl_Coinsuran
		
		With objNewMember
			.nClaim = nClaim
			.nCase_num = nCase_num
			.nDeman_type = nDeman_type
			.nCompany = nCompany
			.sCompany = sCompany
			.dEffecdate = dEffecdate
			.nExpenses = nExpenses
			.nShare = nShare
			.sSel = sSel
		End With
		
		mCol.Add(objNewMember)
		
		
		'return the object created
		Add = objNewMember
		objNewMember = Nothing
		
	End Function
	'**% Finf: find the records in cl_coinsuran
	'% Find: busca los registrons en cl_coinsuran
	Public Function Find(ByVal nClaim As Double, ByVal nCase_num As Integer, ByVal nDeman_type As Integer, ByVal dEffecdate As Date, ByVal nUsercode As Integer) As Boolean
		Dim lrecreacl_Reinsuran As eRemoteDB.Execute
		
		lrecreacl_Reinsuran = New eRemoteDB.Execute
		
		On Error GoTo Find_Err
		
		'**+Parameters definition for the stored procedure 'insudb.reaT_payclaAll'
		'**Data read on 02/20/2001 04:38:22 p.m.
		'+ Definición de parámetros para stored procedure 'insudb.reaT_payclaAll'
		'+ Información leída el 20/02/2001 04:38:22 p.m.
		
		With lrecreacl_Reinsuran
			.StoredProcedure = "reacl_Coinsuran2"
			.Parameters.Add("nClaim", nClaim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCase_num", nCase_num, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDeman_type", nDeman_type, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				Do While Not .EOF
					Call Add(nClaim, nCase_num, nDeman_type, .FieldToClass("nCompany"), .FieldToClass("sCompany"), .FieldToClass("dEffecdate"), .FieldToClass("nShare"), .FieldToClass("nExpenses"), .FieldToClass("sSel"))
					.RNext()
				Loop 
				.RCloseRec()
				Find = True
			Else
				Find = False
			End If
		End With
		lrecreacl_Reinsuran = Nothing
		
Find_Err: 
		If Err.Number Then
			Find = False
		End If
		On Error GoTo 0
	End Function
	
	
	'%Item: Retorna un objeto de la colección
	Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As Cl_Coinsuran
		Get
			
			Item = mCol.Item(vntIndexKey)
			
		End Get
	End Property
	
	
	
	
	'%Count: Retorna la cantidad de elementos en la colección
	Public ReadOnly Property Count() As Integer
		Get
			Count = mCol.Count()
		End Get
	End Property
	
	
	'%NewEnum: Permite recorrer la coleccion
	'UPGRADE_NOTE: NewEnum property was commented out. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="B3FC1610-34F3-43F5-86B7-16C984F0E88E"'
	'Public ReadOnly Property NewEnum() As stdole.IUnknown
		'Get
			'NewEnum = mCol._NewEnum
		'End Get
	'End Property
	
	Public Function GetEnumerator() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
				GetEnumerator = mCol.GetEnumerator
	End Function
	
	
	
	'%Remove: Elimina un elemento de la coleccion
	Public Sub Remove(ByVal vntIndexKey As Object)
		mCol.Remove(vntIndexKey)
	End Sub
	
	
	'%Class_Initialize: Inicializa las variables del objeto
	Private Sub Class_Initialize_Renamed()
		mCol = New Collection
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
	
	
	'%Class_Terminate: Termina el uso del objeto
	Private Sub Class_Terminate_Renamed()
		mCol = Nothing
	End Sub
	Protected Overrides Sub Finalize()
		Class_Terminate_Renamed()
		MyBase.Finalize()
	End Sub
End Class






