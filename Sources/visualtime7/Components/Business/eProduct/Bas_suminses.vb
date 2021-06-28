Option Strict Off
Option Explicit On
Public Class Bas_suminses
	Implements System.Collections.IEnumerable
	'%-------------------------------------------------------%'
	'% $Workfile:: Bas_suminses.cls                         $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 12:35p                               $%'
	'% $Revision:: 8                                        $%'
	'%-------------------------------------------------------%'
	
	Private mCol As Collection
	
	'-Variables auxiliares
	Private mintBranch As Integer
	Private mintProduct As Integer
	Private mdtmEffecdate As Date
	
	'%Add: A�ade una nueva instancia de la clase Bas_sumins a la colecci�n
	Public Function Add(ByRef objElement As Bas_sumins) As Bas_sumins
		mCol.Add(objElement)
		
		'+Retorna el objeto creado
		Add = objElement
	End Function
	
	'%Find: Devuelve la informaci�n de los capitales b�sicos asegurados de un producto
	Public Function Find(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal dEffecdate As Date, Optional ByVal lblnFind As Boolean = False, Optional ByVal sCodispl As String = "", Optional ByVal nModulec As Integer = 0, Optional ByVal nCover As Integer = 0, Optional ByVal blnCapitalBasic As Boolean = False) As Boolean
		'-Se declara la variable que determina el resultado de la funcion (True/False)
		
		Static lblnRead As Boolean
		
		Dim lrecreaBas_sumins As eRemoteDB.Execute
		Dim lclsBas_sumins As eProduct.Bas_sumins
		Dim lclsSumcov_apl As Sumcov_apl
		
		lrecreaBas_sumins = New eRemoteDB.Execute
		lclsSumcov_apl = New Sumcov_apl
		
		'+Definici�n de par�metros para stored procedure 'insudb.reaBas_sumins'
		'+Informaci�n le�da el 02/04/2001 03:11:53 p.m.
		If mintBranch <> nBranch Or mintProduct <> nProduct Or mdtmEffecdate <> dEffecdate Or lblnFind Then
			
			mintBranch = nBranch
			mintProduct = nProduct
			mdtmEffecdate = dEffecdate
			
			With lrecreaBas_sumins
				.StoredProcedure = "reaBas_sumins"
				.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				
				If .Run Then
					Do While Not .EOF
						lclsBas_sumins = New eProduct.Bas_sumins
						
						lclsBas_sumins.nBranch = .FieldToClass("nBranch")
						lclsBas_sumins.nSumins_co = .FieldToClass("nSumins_co")
						lclsBas_sumins.nProduct = .FieldToClass("nProduct")
						lclsBas_sumins.dEffecdate = .FieldToClass("dEffecdate")
						lclsBas_sumins.sDescript = .FieldToClass("sDescript")
						lclsBas_sumins.sShort_des = .FieldToClass("sShort_des")
						lclsBas_sumins.dNulldate = .FieldToClass("dNulldate")
						lclsBas_sumins.nUsercode = .FieldToClass("nUsercode")
						If sCodispl = "DP052A" Then
							If Not lclsSumcov_apl.Find(nBranch, nProduct, nModulec, nCover, lclsBas_sumins.nSumins_co, dEffecdate) Then
								lclsSumcov_apl.nSumins_rat = eRemoteDB.Constants.intNull
							End If
							
						End If
						lclsBas_sumins.nSumins_rat = IIf(blnCapitalBasic, lclsSumcov_apl.nSumins_rat, eRemoteDB.Constants.intNull)
						Call Add(lclsBas_sumins)
						'UPGRADE_NOTE: Object lclsBas_sumins may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
						lclsBas_sumins = Nothing
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
		
		'UPGRADE_NOTE: Object lrecreaBas_sumins may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaBas_sumins = Nothing
		'UPGRADE_NOTE: Object lclsSumcov_apl may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsSumcov_apl = Nothing
		
	End Function
	
	'*Item: Devuelve un elemento de la colecci�n (segun �ndice)
	'-------------------------------------------------------------
	Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As Bas_sumins
		Get
			'-------------------------------------------------------------
			Item = mCol.Item(vntIndexKey)
			
		End Get
	End Property
	
	'*Count: Devuelve el n�mero de elementos que posee la colecci�n
	Public ReadOnly Property Count() As Integer
		Get
			Count = mCol.Count()
			
		End Get
	End Property
	
	'%NewEnum: Permite enumerar la colecci�n para utilizarla en un ciclo For Each... Next
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
	
	'%Remove: Elimina un elemento de la colecci�n
	'---------------------------------------------
	Public Sub Remove(ByRef vntIndexKey As Object)
		'---------------------------------------------
		mCol.Remove(vntIndexKey)
		
	End Sub
	
	'%Class_Initialize: Controla la creaci�n de una instancia de la colecci�n
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		mCol = New Collection
		
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
	
	'%Class_Terminate: Controla la destrucci�n de una instancia de la colecci�n
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






