Option Strict Off
Option Explicit On
Public Class Tab_am_excprods
	Implements System.Collections.IEnumerable
	'%-------------------------------------------------------%'
	'% $Workfile:: Tab_am_excprods.cls                      $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 1:38p                                $%'
	'% $Revision:: 7                                        $%'
	'%-------------------------------------------------------%'
	
	'- Variables locales
	Private mCol As Collection
	
	'%Add: Añade una nueva instancia de la clase a la colección
	Public Function Add(ByVal dNulldate As Date, ByVal dEnd_date As Date, ByVal dInit_date As Date, ByVal nExc_code As Integer, ByVal sIllness As String, ByVal dEffecdate As Date, ByVal sDescript As String) As Tab_am_excprod
		'Se crea un nuevo objeto
		Dim objNewMember As Tab_am_excprod
		objNewMember = New Tab_am_excprod
		
		
		With objNewMember
			.dEffecdate_reg = dEffecdate
			.sIllness = sIllness
			.nExc_code = nExc_code
			.dInit_date = dInit_date
			.dEnd_date = dEnd_date
			.dNulldate = dNulldate
			.sDescript = sDescript
			
			
		End With
		mCol.Add(objNewMember)
		'Retorna el objeto creado
		Add = objNewMember
		'UPGRADE_NOTE: Object objNewMember may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		objNewMember = Nothing
		
		
	End Function
	'%Item: Devuelve un elemento de la colección (segun índice)
	Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As Tab_am_excprod
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
	'%Class_Terminate: Desctruye la colección cuando la clase ha terminado
	'UPGRADE_NOTE: Class_Terminate was upgraded to Class_Terminate_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Terminate_Renamed()
		'UPGRADE_NOTE: Object mCol may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		mCol = Nothing
	End Sub
	Protected Overrides Sub Finalize()
		Class_Terminate_Renamed()
		MyBase.Finalize()
	End Sub
	
	'%Find: Permite consultar las enfermedades excluídas para una tarifa o producto
	Public Function Find(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nTariff As Integer, ByVal dEffecdate As Date, ByVal sTypeexcl As String, Optional ByRef lblnFind As Boolean = False) As Boolean
		Dim lrecreaTab_am_excprod As eRemoteDB.Execute
		
		On Error GoTo reaTab_am_excprod_Err
		
		lrecreaTab_am_excprod = New eRemoteDB.Execute
		
		'+Definición de parámetros para stored procedure 'insudb.reaTab_am_excprod'
		'+Información leída el 26/01/2000 10:17:27
		
		With lrecreaTab_am_excprod
			.StoredProcedure = "reaTab_am_excprod"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTariff", nTariff, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sTypeexcl", sTypeexcl, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				Do While Not .EOF
					Call Add(.FieldToClass("dNulldate"), .FieldToClass("dEnd_date"), .FieldToClass("dInit_date"), .FieldToClass("nExc_code"), .FieldToClass("sIllness"), .FieldToClass("dEffecdate"), .FieldToClass("sDescript"))
					
					.RNext()
				Loop 
				
				Find = True
				
				.RCloseRec()
			End If
		End With
		
reaTab_am_excprod_Err: 
		If Err.Number Then
			Find = False
		End If
		
		'UPGRADE_NOTE: Object lrecreaTab_am_excprod may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaTab_am_excprod = Nothing
		On Error GoTo 0
		
	End Function
End Class






