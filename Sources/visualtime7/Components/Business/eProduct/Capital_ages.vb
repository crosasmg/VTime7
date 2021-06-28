Option Strict Off
Option Explicit On
Public Class Capital_ages
	Implements System.Collections.IEnumerable
	'%-------------------------------------------------------%'
	'% $Workfile:: Capital_ages.cls                         $%'
	'% $Author:: Ljimenez                                   $%'
	'% $Date:: 29/08/08 12:35p                              $%'
	'% $Revision:: 1                                        $%'
	'%-------------------------------------------------------%'
	
	'+ Variable local para contener colección.
	
	Private mCol As Collection
	
	'% AddCapital_age: Este método permite añadir registros a la colección.
	Public Function AddCapital_age(ByRef nAge_init As Integer, ByRef nAge_end As Integer, ByRef nCapmini As Double, ByRef nCapmaxim As Double, ByRef nBranch As Integer, ByRef nProduct As Integer, ByRef dEffecdate As Date, ByRef nUsercode As Integer, ByRef nModulec As Integer, ByRef nCover As Integer, ByRef nRole As Integer) As Capital_age
		'+ Crear un nuevo objeto.
		Dim objNewMember As Capital_age
		
		'+ Establecer las propiedades que se transfieren al método.
		objNewMember = New Capital_age
		With objNewMember
			.nUsercode = nUsercode
			.nRole = nRole
			.dEffecdate = dEffecdate
			.nProduct = nProduct
			.nBranch = nBranch
			.nCapmini = nCapmini
			.nCapmaxim = nCapmaxim
			.nAge_init = nAge_init
			.nAge_end = nAge_end
			.nModulec = nModulec
			.nCover = nCover
		End With
		
		mCol.Add(objNewMember)
		
		'+ Return the object created.
		
		AddCapital_age = objNewMember
		'UPGRADE_NOTE: Object objNewMember may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		objNewMember = Nothing
	End Function
	
	'%Item: Devuelve un elemento de la colección (segun índice)
	Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As Capital_age
		Get
			Item = mCol.Item(vntIndexKey)
		End Get
	End Property
	
	'%Count: Devuelve el número de elementos que posee la colección
	Public ReadOnly Property Count() As Integer
		Get
			Count = mCol.Count()
		End Get
	End Property
	
	'%NewEnum: Permite enumerar la colección para utilizarla en un ciclo For Each... Next
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
	
	'%Remove: Elimina un elemento de la colección
	Public Sub Remove(ByRef vntIndexKey As Object)
		mCol.Remove(vntIndexKey)
	End Sub
	
	'%Class_Initialize: Controla la creación de una instancia de la colección
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		mCol = New Collection
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
	
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
	
	'% FindCapital_age: Verifica que exista información en la tabla de capitales por edad
	Public Function FindCapital_age(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal dEffecdate As Date, ByVal nModulec As Integer, ByVal nCover As Integer, ByVal nRole As Integer) As Boolean
		Dim lrecReaCapital_age As eRemoteDB.Execute
		
		lrecReaCapital_age = New eRemoteDB.Execute
		
		On Error GoTo FindCapital_age_Err
		
		FindCapital_age = True
		
		'UPGRADE_NOTE: Object mCol may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		mCol = Nothing
		mCol = New Collection
		
		'+ Definición de parámetros para stored procedure
		
		With lrecReaCapital_age
			.StoredProcedure = "reaCapital_age"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nModulec", nModulec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCover", nCover, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nRole", nRole, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run Then
				
				Do While Not .EOF
					Call AddCapital_age(.FieldToClass("nAge_init"), .FieldToClass("nAge_end"), .FieldToClass("nCapmini"), .FieldToClass("nCapmaxim"), .FieldToClass("nBranch"), .FieldToClass("nProduct"), .FieldToClass("dEffecdate"), .FieldToClass("nUsercode"), .FieldToClass("nModulec"), .FieldToClass("nCover"), .FieldToClass("nRole"))
					.RNext()
				Loop 
				.RCloseRec()
			Else
				FindCapital_age = False
			End If
		End With
		
		'UPGRADE_NOTE: Object lrecReaCapital_age may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecReaCapital_age = Nothing
		
FindCapital_age_Err: 
		If Err.Number Then
			FindCapital_age = False
		End If
		
		On Error GoTo 0
	End Function
End Class






