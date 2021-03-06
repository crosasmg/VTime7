Option Strict Off
Option Explicit On
<System.Runtime.InteropServices.ProgId("Lend_Agree_Press_NET.Lend_Agree_Press")> Public Class Lend_Agree_Press
	Implements System.Collections.IEnumerable
	'%-------------------------------------------------------%'
	'% $Workfile:: Lend_Agree_Press.cls                          $%'
	'% $Author:: lsereno                                   $%'
	'% $Date:: 03/05/07 12:35p                               $%'
	'% $Revision:: 01                                       $%'
	'%-------------------------------------------------------%'
	
	'+ Variable local para contener colecci?n.
	
	Private mCol As Collection
	'% AddLend_Agree_Pres: Este m?todo permite a?adir registros a la colecci?n.
	Public Function AddLend_Agree_Pres(ByRef nPrestac As Integer, ByRef nCod_agree As Integer, ByRef nCover As Integer, ByRef nModulec As Integer, ByRef nGroup As Integer) As Lend_Agree_Pres
		'+ Crear un nuevo objeto.
		Dim objNewMember As Lend_Agree_Pres
		
		'+ Establecer las propiedades que se transfieren al m?todo.
		objNewMember = New Lend_Agree_Pres
		With objNewMember
			.nPrestac = nPrestac
			.nCod_agree = nCod_agree
			.nCover = nCover
			.nModulec = nModulec
			.nGroup = nGroup
		End With
		
		mCol.Add(objNewMember)
		
		'+ Return the object created.
		
		AddLend_Agree_Pres = objNewMember
		'UPGRADE_NOTE: Object objNewMember may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		objNewMember = Nothing
	End Function
	
	'%Item: Devuelve un elemento de la colecci?n (segun ?ndice)
	Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As Prod_Am_Bil
		Get
			Item = mCol.Item(vntIndexKey)
		End Get
	End Property
	
	'%Count: Devuelve el n?mero de elementos que posee la colecci?n
	Public ReadOnly Property Count() As Integer
		Get
			Count = mCol.Count()
		End Get
	End Property
	
	'%NewEnum: Permite enumerar la colecci?n para utilizarla en un ciclo For Each... Next
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
	
	'%Remove: Elimina un elemento de la colecci?n
	Public Sub Remove(ByRef vntIndexKey As Object)
		mCol.Remove(vntIndexKey)
	End Sub
	
	'%Class_Initialize: Controla la creaci?n de una instancia de la colecci?n
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		mCol = New Collection
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
	
	'%Class_Terminate: Controla la destrucci?n de una instancia de la colecci?n
	'UPGRADE_NOTE: Class_Terminate was upgraded to Class_Terminate_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Terminate_Renamed()
		'UPGRADE_NOTE: Object mCol may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		mCol = Nothing
	End Sub
	Protected Overrides Sub Finalize()
		Class_Terminate_Renamed()
		MyBase.Finalize()
	End Sub
	
	'% FindLend_agree_Pres: Verifica que exista informaci?n por cobertura.
	Public Function FindLend_agree_Pres(ByVal sCertype As Integer, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Integer, ByVal dinit_date As Date, Optional ByVal lblnFind As Boolean = True) As Boolean
		Dim lrecReaLend_agree_Pres As eRemoteDB.Execute
		
		lrecReaLend_agree_Pres = New eRemoteDB.Execute
		
		On Error GoTo FindLend_agree_Pres_Err
		
		FindLend_agree_Pres = True
		
		'+ Definici?n de par?metros para stored procedure
		With lrecReaLend_agree_Pres
			.StoredProcedure = "Fnd_Lend_Agree_pres"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("npolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("Dinit_date", dinit_date, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run Then
				Do While Not .EOF
					Call AddLend_Agree_Pres(.FieldToClass("nprestac"), .FieldToClass("ncod_agree"), .FieldToClass("nCover"), .FieldToClass("nModulec"), .FieldToClass("nGroup"))
					.RNext()
				Loop 
				.RCloseRec()
			End If
		End With
		
FindLend_agree_Pres_Err: 
		If Err.Number Then
			FindLend_agree_Pres = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecReaLend_agree_Pres may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecReaLend_agree_Pres = Nothing
	End Function
End Class






