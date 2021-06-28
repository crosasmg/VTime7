Option Strict Off
Option Explicit On
Public Class Comm_mods
	Implements System.Collections.IEnumerable
	'%-------------------------------------------------------%'
	'% $Workfile:: Comm_mods.cls                            $%'
	'% $Author:: Nvaplat37                                  $%'
	'% $Date:: 22/08/03 5:26p                               $%'
	'% $Revision:: 1                                        $%'
	'%-------------------------------------------------------%'
	
	Private mCol As Collection
	
	'- Variables auxiliares
	
	Private mintBranch As Integer
	Private mintProduct As Integer
	
	'% Add: Añade una nueva instancia de la clase Cliallopro a la colección
	Public Function Add(ByRef objElement As Comm_mod) As Comm_mod
		mCol.Add(objElement)
		
		'+ Retorna el objeto creado
		Add = objElement
	End Function
	
	'% Find: Devuelve la información de los clientes permitidos del producto en tratamiento
	Public Function Find(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal dEffecdate As Date, Optional ByVal lblnFind As Boolean = False) As Boolean
		'- Se declara la variable que determina el resultado de la funcion (True/False)
		Dim lrecReaComm_mod As eRemoteDB.Execute
		Dim lclsComm_mod As Comm_mod
		
		'+ Definición de parámetros para stored procedure 'insudb.reaCliallopro'
		'+ Información leída el 03/04/2001 01:31:33 p.m.
		On Error GoTo Find_Err
		If mintBranch <> nBranch Or mintProduct <> nProduct Or lblnFind Then
			
			mintBranch = nBranch
			mintProduct = nProduct
			
			lrecReaComm_mod = New eRemoteDB.Execute
			With lrecReaComm_mod
				.StoredProcedure = "reaComm_mod"
				.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				
				If .Run Then
					Do While Not .EOF
						lclsComm_mod = New Comm_mod
						lclsComm_mod.nBranch = nBranch
						lclsComm_mod.nProduct = nProduct
						lclsComm_mod.nModulec_ex = .FieldToClass("nModulec_ex")
						lclsComm_mod.sDescModulec_ex = .FieldToClass("sDescModulec_ex")
						lclsComm_mod.nCover_ex = .FieldToClass("nCover_ex")
						lclsComm_mod.sDescCover_ex = .FieldToClass("sDescCover_ex")
						lclsComm_mod.nRole_ex = .FieldToClass("nRole_ex")
						lclsComm_mod.sDescRole_ex = .FieldToClass("sDescRole_ex")
						lclsComm_mod.nModulec_ad = .FieldToClass("nModulec_ad")
						lclsComm_mod.sDescModulec_ad = .FieldToClass("sDescModulec_ad")
						lclsComm_mod.nCover_ad = .FieldToClass("nCover_ad")
						lclsComm_mod.sDescCover_ad = .FieldToClass("sDescCover_ad")
						lclsComm_mod.nRole_ad = .FieldToClass("nRole_ad")
						lclsComm_mod.sDescRole_ad = .FieldToClass("sDescRole_ad")
						lclsComm_mod.nType_comm = .FieldToClass("nType_comm")
						lclsComm_mod.sDescType_comm = .FieldToClass("sDescType_comm")
						lclsComm_mod.dEffecdate = .FieldToClass("dEffecdate")
						
						Call Add(lclsComm_mod)
						
						'UPGRADE_NOTE: Object lclsComm_mod may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
						lclsComm_mod = Nothing
						.RNext()
					Loop 
					
					.RCloseRec()
					Find = True
				Else
					Find = False
				End If
			End With
		End If
		
Find_Err: 
		If Err.Number Then
			Find = False
		End If
		'UPGRADE_NOTE: Object lrecReaComm_mod may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecReaComm_mod = Nothing
		'UPGRADE_NOTE: Object lclsComm_mod may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsComm_mod = Nothing
		On Error GoTo 0
	End Function
	'* Item: Devuelve un elemento de la colección (segun índice)
	'-------------------------------------------------------------
	Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As Cliallopro
		Get
			'-------------------------------------------------------------
			Item = mCol.Item(vntIndexKey)
			
		End Get
	End Property
	
	'* Count: Devuelve el número de elementos que posee la colección
	Public ReadOnly Property Count() As Integer
		Get
			Count = mCol.Count()
			
		End Get
	End Property
	
	'* NewEnum: Permite enumerar la colección para utilizarla en un ciclo For Each... Next
	'UPGRADE_NOTE: NewEnum property was commented out. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="B3FC1610-34F3-43F5-86B7-16C984F0E88E"'
	'Public ReadOnly Property NewEnum() As stdole.IUnknown
		'Get
			'NewEnum = mCol._NewEnum
			'
		'End Get
	'End Property
	
	Public Function GetEnumerator() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
		'UPGRADE_TODO: Uncomment and change the following line to return the collection enumerator. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="95F9AAD0-1319-4921-95F0-B9D3C4FF7F1C"'
		GetEnumerator = mCol.GetEnumerator
	End Function
	
	'% Remove: Elimina un elemento de la colección
	'---------------------------------------------
	Public Sub Remove(ByRef vntIndexKey As Object)
		'---------------------------------------------
		mCol.Remove(vntIndexKey)
		
	End Sub
	
	'% Class_Initialize: Controla la creación de una instancia de la colección
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		mCol = New Collection
		
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
	
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






