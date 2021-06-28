Option Strict Off
Option Explicit On
Public Class Tab_winpros
	Implements System.Collections.IEnumerable
	'%-------------------------------------------------------%'
	'% $Workfile:: Tab_winpros.cls                          $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 12:36p                               $%'
	'% $Revision:: 13                                       $%'
	'%-------------------------------------------------------%'
	
	Private mCol As Collection
	
	'**- Auxiliary variables
	'- Variables auxiliares
	Private mstrBranchtype As String
	Private mintnSequence As Integer
	
	'**% Add: Add a new instance of the class Tab_winpro to the collection
	'% Add: Añade una nueva instancia de la clase Tab_winpro a la colección
	Public Function Add(ByRef objElement As Tab_winpro) As Tab_winpro
		'**- Define the variable that will contain the instance to add
		'- Se define la variable que contendra la instancia a añadir
		mCol.Add(objElement)
		Add = objElement
	End Function
	
	'**% Find: restores the information about one window
	'% Find: Devuelve información acerca de una ventana
	Public Function Find(ByVal sBranchtype As String, ByVal nSequence As Integer, Optional ByVal lblnFind As Boolean = False) As Boolean
		On Error GoTo Find_Err
		'**- Declares the variable thar determinate the result of the function (True/False)
		'- Se declara la variable que determina el resultado de la funcion (True/False)
		Static lblnRead As Boolean
		
		'**- Define the variable lrecreaTab_winpro
		'- Se define la variable lrecreaTab_winpro
		Dim lrecreaTab_winpro As eRemoteDB.Execute
		Dim lclsTab_winpro As Tab_winpro
		
		lrecreaTab_winpro = New eRemoteDB.Execute
		
		If mstrBranchtype <> sBranchtype Or mintnSequence <> nSequence Or lblnFind Then
			
			mstrBranchtype = sBranchtype
			mintnSequence = nSequence
			
			'**+ Parameters definition for the stored procedure 'insudb.reaTab_winpro'
			'**+ Data read on 03/21/2001 04:36:11 p.m.
			'+ Definición de parámetros para stored procedure 'insudb.reaTab_winpro'
			'+ Información leída el 21/03/2001 04:36:11 p.m.
			
			With lrecreaTab_winpro
				.StoredProcedure = "reaTab_winpro"
				.Parameters.Add("sBranchtype", sBranchtype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nSequence", nSequence, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				If .Run Then
					Do While Not .EOF
						lclsTab_winpro = New Tab_winpro
						lclsTab_winpro.sBranchtype = sBranchtype
						lclsTab_winpro.nSequence = .FieldToClass("nSequence")
						lclsTab_winpro.sCodisp = .FieldToClass("sCodisp")
						lclsTab_winpro.sCodispl = .FieldToClass("sCodispl")
						lclsTab_winpro.sRequire = .FieldToClass("sRequire")
						lclsTab_winpro.nUsercode = .FieldToClass("nUsercode")
						lclsTab_winpro.sDescript = .FieldToClass("sDescript")
						lclsTab_winpro.sShort_des = .FieldToClass("sShort_des")
						lclsTab_winpro.nWindowty = .FieldToClass("nWindowty")
						Call Add(lclsTab_winpro)
						'UPGRADE_NOTE: Object lclsTab_winpro may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
						lclsTab_winpro = Nothing
						.RNext()
					Loop 
					.RCloseRec()
					lblnRead = True
				Else
					lblnRead = False
				End If
			End With
		End If
		
		Find = lblnRead
		
Find_Err: 
		If Err.Number Then
			Find = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lclsTab_winpro may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsTab_winpro = Nothing
		'UPGRADE_NOTE: Object lrecreaTab_winpro may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaTab_winpro = Nothing
	End Function
	
	'% Find_Win: Devuelve información acerca de una ventana
	Public Function Find_Win(ByVal sBranchtype As String, Optional ByVal lblnFind As Boolean = False) As Boolean
		On Error GoTo Find_Win_Err
		'**- Declares the variable thar determinate the result of the function (True/False)
		'- Se declara la variable que determina el resultado de la funcion (True/False)
		Static lblnRead As Boolean
		
		'**- Define the variable lrecreaTab_winpro
		'- Se define la variable lrecreaTab_winpro
		Dim lrecreaTab_winpro As eRemoteDB.Execute
		Dim lclsTab_winpro As Tab_winpro
		lrecreaTab_winpro = New eRemoteDB.Execute
		If mstrBranchtype <> sBranchtype Or lblnFind Then
			mstrBranchtype = sBranchtype
			'+ Definición de parámetros para stored procedure 'insudb.reaTab_winpro'
			'+ Información leída el 21/03/2001 04:36:11 p.m.
			With lrecreaTab_winpro
				.StoredProcedure = "reaTab_winpro_win"
				.Parameters.Add("sBranchtype", sBranchtype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				If .Run Then
					Do While Not .EOF
						lclsTab_winpro = New Tab_winpro
						lclsTab_winpro.sBranchtype = sBranchtype
						lclsTab_winpro.nSequence = .FieldToClass("nSequence")
						lclsTab_winpro.sCodisp = .FieldToClass("sCodisp")
						lclsTab_winpro.sCodispl = .FieldToClass("sCodispl")
						lclsTab_winpro.sRequire = .FieldToClass("sRequire")
						lclsTab_winpro.nUsercode = .FieldToClass("nUsercode")
						lclsTab_winpro.sDescript = .FieldToClass("sDescript")
						lclsTab_winpro.sShort_des = .FieldToClass("sShort_des")
						lclsTab_winpro.nWindowty = .FieldToClass("nWindowty")
						Call Add(lclsTab_winpro)
						'UPGRADE_NOTE: Object lclsTab_winpro may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
						lclsTab_winpro = Nothing
						.RNext()
					Loop 
					.RCloseRec()
					lblnRead = True
				Else
					lblnRead = False
				End If
			End With
		End If
		Find_Win = lblnRead
Find_Win_Err: 
		If Err.Number Then
			Find_Win = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lclsTab_winpro may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsTab_winpro = Nothing
		'UPGRADE_NOTE: Object lrecreaTab_winpro may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaTab_winpro = Nothing
	End Function
	'**% Item: restores one element of the collection (accourding to the index)
	'% Item: Devuelve un elemento de la colección (segun índice)
	Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As Tab_winpro
		Get
			Item = mCol.Item(vntIndexKey)
		End Get
	End Property
	
	'**% Count: reatores the number of elements that the collection owns
	'% Count: Devuelve el número de elementos que posee la colección
	Public ReadOnly Property Count() As Integer
		Get
			Count = mCol.Count()
		End Get
	End Property
	
	'**% NewEnum: Allows to enumerate the collection for using it in a cycle For Each... Next
	'% NewEnum: Permite enumerar la colección para utilizarla en un ciclo For Each... Next
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
	
	'**% Remove: deletes one element of the collection
	'% Remove: Elimina un elemento de la colección
	'---------------------------------------------
	Public Sub Remove(ByRef vntIndexKey As Object)
		'---------------------------------------------
		mCol.Remove(vntIndexKey)
	End Sub
	
	'**% Class_Initialize: Controls the creation of an instance of the collection
	'% Class_Initialize: Controla la creación de una instancia de la colección
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		mCol = New Collection
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
	
	'**% Class_Terminate: Controls the delete of one instance of the collection
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






