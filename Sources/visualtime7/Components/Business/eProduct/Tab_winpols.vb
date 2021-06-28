Option Strict Off
Option Explicit On
Public Class Tab_winpols
	Implements System.Collections.IEnumerable
	'%-------------------------------------------------------%'
	'% $Workfile:: Tab_winpols.cls                          $%'
	'% $Author:: Nvaplat18                                  $%'
	'% $Date:: 6/10/03 17.23                                $%'
	'% $Revision:: 11                                       $%'
	'%-------------------------------------------------------%'
	Private mCol As Collection
	
	'**-Auxiliary variable
	'- Variables auxiliares
	Private mstrBussityp As String
	Private mstrPolitype As String
	Private mstrCompon As String
	Private mintTratypep As Integer
	Private mstrBrancht As String
	Private mintType_amend As Short
	
	'**% Add: add a new instance to the class Tab_winpol to the collection
	'% Add: Añade una nueva instancia de la clase Tab_winpol a la colección
	Public Function Add(ByRef objElement As Tab_winpol) As Tab_winpol
		mCol.Add(objElement)
		
		'**+Return the creates object
		'+ Retorna el objeto creado
		Add = objElement
	End Function
	
	'**% Find: Return the information of the wndows related to policy process
	'% Find: Devuelve la información de las ventanas relacionadas con las operaciones de pólizas
	Public Function Find(ByVal sBussityp As String, ByVal nTratypep As Integer, ByVal sPolitype As String, ByVal sCompon As String, Optional ByVal bFind As Boolean = False, Optional ByVal sBrancht As String = "", Optional ByVal nType_amend As Short = 0) As Boolean
		'**-Declare the variable that determinate the result of the function (True/False)
		'- Se declara la variable que determina el resultado de la funcion (True/False)
		Dim lrecreaTab_winpol_ca As eRemoteDB.Execute
		Dim lclsTab_winpol As eProduct.Tab_winpol
		
		On Error GoTo Find_Err
		
		lrecreaTab_winpol_ca = New eRemoteDB.Execute
		
		If mstrBussityp <> sBussityp Or mstrPolitype <> sPolitype Or mstrCompon <> sCompon Or mintTratypep <> nTratypep Or mstrBrancht <> sBrancht Or mintType_amend <> nType_amend Or bFind Then
			
			'**Parameters definition for the stored procedure 'insudb.reaTab_winpol_ca'
			'**Data read on 09/13/2001 02:41:12 p.m.
			'Definición de parámetros para stored procedure 'insudb.reaTab_winpol_ca'
			'Información leída el 13/09/2001 02:41:12 p.m.
			With lrecreaTab_winpol_ca
				.StoredProcedure = "reaTab_winpol_a"
				.Parameters.Add("sBussityp", sBussityp, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("sCompon", sCompon, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nTratypep", nTratypep, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("sPolitype", sPolitype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("sBrancht", sBrancht, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nType_Amend", IIf(nType_amend = -32768, 0, nType_amend), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				If .Run Then
					Do While Not .EOF
						lclsTab_winpol = New eProduct.Tab_winpol
						lclsTab_winpol.sExist = .FieldToClass("sExist")
						lclsTab_winpol.sCodispl = .FieldToClass("sCodispl")
						lclsTab_winpol.sDescript = .FieldToClass("sDescript")
						lclsTab_winpol.nSequence = .FieldToClass("nSequence")
						lclsTab_winpol.sDefaulti = .FieldToClass("sDefaulti")
						lclsTab_winpol.sRequire = .FieldToClass("sRequire")
						lclsTab_winpol.sAutomatic = .FieldToClass("sAutomatic")
						Call Add(lclsTab_winpol)
						'UPGRADE_NOTE: Object lclsTab_winpol may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
						lclsTab_winpol = Nothing
						.RNext()
					Loop 
					.RCloseRec()
					mstrBussityp = sBussityp
					mstrPolitype = sPolitype
					mstrCompon = sCompon
					mintTratypep = nTratypep
					Find = True
				End If
			End With
		Else
			Find = True
		End If
		
Find_Err: 
		If Err.Number Then
			Find = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecreaTab_winpol_ca may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaTab_winpol_ca = Nothing
	End Function
	
	'*** Item: return one element from the collection (accourding to the index)
	'* Item: Devuelve un elemento de la colección (segun índice)
	'-------------------------------------------------------------
	Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As Tab_winpol
		Get
			'-------------------------------------------------------------
			Item = mCol.Item(vntIndexKey)
		End Get
	End Property
	
	'*** Count: return the number of elements that the collection has
	'* Count: Devuelve el número de elementos que posee la colección
	Public ReadOnly Property Count() As Integer
		Get
			Count = mCol.Count()
		End Get
	End Property
	
	'*** NewEnum: Permit to enumerate the collection to use it in a cycle For Each... Next
	'* NewEnum: Permite enumerar la colección para utilizarla en un ciclo For Each... Next
	'UPGRADE_NOTE: NewEnum property was commented out. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="B3FC1610-34F3-43F5-86B7-16C984F0E88E"'
	'Public ReadOnly Property NewEnum() As stdole.IUnknown
		'Get
			'NewEnum = mCol._NewEnum
		'End Get
	'End Property
	
	Public Function GetEnumerator() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
		'UPGRADE_TODO: Uncomment and change the following line to return the collection enumerator. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="95F9AAD0-1319-4921-95F0-B9D3C4FF7F1C"'
		GetEnumerator = mCol.GetEnumerator
	End Function
	
	'**% Remove: delete the element of the collection
	'% Remove: Elimina un elemento de la colección
	'---------------------------------------------
	Public Sub Remove(ByRef vntIndexKey As Object)
		'---------------------------------------------
		mCol.Remove(vntIndexKey)
	End Sub
	
	'**% Class_Initialize: control the creation of an instance of the collection
	'% Class_Initialize: Controla la creación de una instancia de la colección
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		mCol = New Collection
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
	
	'**% Class_Terminate: Control the destruction of an instance from the collection
	'% Class_Terminate: Controla la destrucción de una instancia de la colección
	'UPGRADE_NOTE: Class_Terminate was upgraded to Class_Terminate_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Terminate_Renamed()
		'UPGRADE_NOTE: Object mCol may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		mCol = Nothing
	End Sub
	Protected Overrides Sub Finalize()
		Class_Terminate_Renamed()
		MyBase.Finalize()
	End Sub
End Class






