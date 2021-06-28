Option Strict Off
Option Explicit On
Public Class Cliallopros
	Implements System.Collections.IEnumerable
	'%-------------------------------------------------------%'
	'% $Workfile:: Cliallopros.cls                          $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 12:35p                               $%'
	'% $Revision:: 8                                        $%'
	'%-------------------------------------------------------%'
	
	Private mCol As Collection
	
	'- Variables auxiliares
	
	Private mintBranch As Integer
	Private mintProduct As Integer
	
	'% Add: Añade una nueva instancia de la clase Cliallopro a la colección
	Public Function Add(ByRef objElement As Cliallopro) As Cliallopro
		mCol.Add(objElement)
		
		'+ Retorna el objeto creado
		Add = objElement
	End Function
	
	'% Find: Devuelve la información de los clientes permitidos del producto en tratamiento
	Public Function Find(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal dEffecdate As Date, Optional ByVal sPolitype As String = "1", Optional ByVal sCompon As String = "1", Optional ByVal lblnFind As Boolean = False) As Boolean
		'- Se declara la variable que determina el resultado de la funcion (True/False)
		Static lblnRead As Boolean
		Dim lrecreaCliallopro As eRemoteDB.Execute
		Dim lclsCliallopro As Cliallopro
		Dim lclsProduct As Product
		Dim llngCounter As Integer
		
		'+ Definición de parámetros para stored procedure 'insudb.reaCliallopro'
		'+ Información leída el 03/04/2001 01:31:33 p.m.
		On Error GoTo Find_Err
		If mintBranch <> nBranch Or mintProduct <> nProduct Or lblnFind Then
			
			mintBranch = nBranch
			mintProduct = nProduct
			lclsProduct = New Product
			lclsProduct.Find(nBranch, nProduct, dEffecdate)
			
			lrecreaCliallopro = New eRemoteDB.Execute
			With lrecreaCliallopro
				.StoredProcedure = "reaCliallopro"
				.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("sPolitype", sPolitype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("sCompon", sCompon, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				
				If .Run Then
					llngCounter = 0
					Do While Not .EOF
						lclsCliallopro = New Cliallopro
						llngCounter = llngCounter + 1
						lclsCliallopro.sSel = .FieldToClass("sSel")
						lclsCliallopro.sPolitype = .FieldToClass("sPolitype")
						lclsCliallopro.sCompon = .FieldToClass("sCompon")
						lclsCliallopro.nCodigInt = .FieldToClass("nRole")
						lclsCliallopro.sRole_descript = .FieldToClass("sDescript")
						lclsCliallopro.sRequire = .FieldToClass("sRequire")
						lclsCliallopro.sDefaulti = .FieldToClass("sDefaulti")
						lclsCliallopro.nMax_role = .FieldToClass("nMax_role")
						lclsCliallopro.sOptionalQuo = .FieldToClass("sOptionalQuo")
						If llngCounter <> 2 And llngCounter <> 4 Then
							lclsCliallopro.nSelected = CInt(lclsProduct.sHolder)
						Else
							lclsCliallopro.nSelected = 2
						End If
						
						Call Add(lclsCliallopro)
						
						'UPGRADE_NOTE: Object lclsCliallopro may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
						lclsCliallopro = Nothing
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
		'UPGRADE_NOTE: Object lrecreaCliallopro may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaCliallopro = Nothing
		'UPGRADE_NOTE: Object lclsProduct may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsProduct = Nothing
		On Error GoTo 0
	End Function
	
	'% Find: Devuelve la información de los clientes permitidos del producto en tratamiento
	Public Function Find_O(ByVal nBranch As Integer, ByVal nProduct As Integer, Optional ByVal lblnFind As Boolean = False) As Boolean
		'- Se declara la variable que determina el resultado de la funcion (True/False)
		Static lblnRead As Boolean
		Dim lrecreaCliallopro As eRemoteDB.Execute
		Dim lclsCliallopro As Cliallopro
		Dim llngCounter As Integer
		
		On Error GoTo Find_Err
		'+ Definición de parámetros para stored procedure 'insudb.reaCliallopro'
		'+ Información leída el 03/04/2001 01:31:33 p.m.
		If mintBranch <> nBranch Or mintProduct <> nProduct Or lblnFind Then
			lrecreaCliallopro = New eRemoteDB.Execute
			With lrecreaCliallopro
				.StoredProcedure = "reaCliallopro_o"
				.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				If .Run Then
					llngCounter = 0
					Do While Not .EOF
						lclsCliallopro = New Cliallopro
						llngCounter = llngCounter + 1
						lclsCliallopro.sPolitype = .FieldToClass("sPolitype")
						lclsCliallopro.sCompon = .FieldToClass("sCompon")
						lclsCliallopro.nRole = .FieldToClass("nRole")
						lclsCliallopro.sRequire = .FieldToClass("sRequire")
						lclsCliallopro.sDefaulti = .FieldToClass("sDefaulti")
						lclsCliallopro.nMax_role = .FieldToClass("nMax_role")
						lclsCliallopro.sOptionalQuo = .FieldToClass("sOptionalQuo")
						Call Add(lclsCliallopro)
						'UPGRADE_NOTE: Object lclsCliallopro may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
						lclsCliallopro = Nothing
						.RNext()
					Loop 
					
					.RCloseRec()
					lblnRead = True
					mintBranch = nBranch
					mintProduct = nProduct
				Else
					lblnRead = False
				End If
			End With
		End If
		
		Find_O = lblnRead
Find_Err: 
		If Err.Number Then
			Find_O = False
		End If
		'UPGRADE_NOTE: Object lrecreaCliallopro may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaCliallopro = Nothing
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






