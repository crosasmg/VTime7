Option Strict Off
Option Explicit On
Public Class Dsex_condis
	Implements System.Collections.IEnumerable
	'%-------------------------------------------------------%'
	'% $Workfile:: Dsex_condis.cls                          $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 12:35p                               $%'
	'% $Revision:: 16                                       $%'
	'%-------------------------------------------------------%'
	
	'local variable to hold collection
	Private mCol As Collection
	
	Public Function Add(ByRef lclsDsex_condi As Dsex_condi) As Dsex_condi
		
		With lclsDsex_condi
			mCol.Add(lclsDsex_condi, "DC" & .nBranch & .nProduct & .nDisexprc & .nAplication & .dEffecdate & .nCode & .nRate & .nExist & .sDescript & .nModulec & .sDescriptModulec & .sDescriptRol & .nRole)
		End With
		
		'Se retorna el objeto creado
		Add = lclsDsex_condi
	End Function
	
	'%Find: Busca las condiciones de aplicación del recargo/descuento
	Public Function Find(ByVal sBrancht As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nDisexprc As Integer, ByVal dEffecdate As Date, ByVal nOrder_apl As Integer, ByVal nCapitalAplied As Integer, ByVal lbytSubScript As Byte, ByVal nRow As Integer) As Boolean
		Dim lrecDsex_condi As eRemoteDB.Execute
		Dim lclsDsex_condi As Dsex_condi
		Dim nCount As Integer
		
		lrecDsex_condi = New eRemoteDB.Execute
		
		On Error GoTo Find_Err
		With lrecDsex_condi
			If lbytSubScript = 1 Then
				.StoredProcedure = IIf(sBrancht = "1" Or sBrancht = "2", "realifcovdp08", "reagencovdp08")
			Else
				.StoredProcedure = IIf(nCapitalAplied = 1, "reabas_suminsDP08", "readisco_exprDP08")
			End If
			
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDisexprc", nDisexprc, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			'+ Si se van a leer los recargos y descuentos, se le pasa el orden de aplicación
			If lbytSubScript = 2 And nCapitalAplied = 2 Then
				.Parameters.Add("nOrder_apl", nOrder_apl, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			End If
			
			nCount = 1
			
			If .Run Then
				Find = True
				Do While Not .EOF And nCount < nRow
					nCount = nCount + 1
					.RNext()
				Loop 
				
				Do While Not .EOF And nCount < nRow + 50
					nCount = nCount + 1
					lclsDsex_condi = New Dsex_condi
					With lclsDsex_condi
						.nBranch = nBranch
						.nProduct = nProduct
						.nDisexprc = nDisexprc
						.nAplication = lrecDsex_condi.FieldToClass("nAplication", eRemoteDB.Constants.intNull)
						.dEffecdate = dEffecdate
						.nCode = lrecDsex_condi.FieldToClass("nCode", eRemoteDB.Constants.intNull)
						.nRate = lrecDsex_condi.FieldToClass("nPercent", 0)
						.nExist = lrecDsex_condi.FieldToClass("nExist", eRemoteDB.Constants.intNull)
						.sDescript = lrecDsex_condi.FieldToClass("sDescript", String.Empty)
						
						If lrecDsex_condi.StoredProcedure = "realifcovdp08" Then
							.nModulec = lrecDsex_condi.FieldToClass("nModulec", eRemoteDB.Constants.intNull)
							.sDescriptModulec = lrecDsex_condi.FieldToClass("sDescriptmodulec", String.Empty)
							.sDescriptRol = lrecDsex_condi.FieldToClass("sDescriptRol", String.Empty)
							.nRole = lrecDsex_condi.FieldToClass("nRole", eRemoteDB.Constants.intNull)
						End If
						
						If lrecDsex_condi.StoredProcedure = "reagencovdp08" Then
							.nModulec = lrecDsex_condi.FieldToClass("nModulec", eRemoteDB.Constants.intNull)
							.sDescriptModulec = lrecDsex_condi.FieldToClass("sDescriptmodulec", String.Empty)
							.sDescriptRol = String.Empty
							.nRole = eRemoteDB.Constants.intNull
						End If
						
						If lrecDsex_condi.StoredProcedure = "reabas_suminsDP08" Or lrecDsex_condi.StoredProcedure = "readisco_exprDP08" Then
							.nModulec = eRemoteDB.Constants.intNull
							.sDescriptModulec = String.Empty
							.sDescriptRol = String.Empty
							.nRole = eRemoteDB.Constants.intNull
						End If
						
					End With
					Call Add(lclsDsex_condi)
					'UPGRADE_NOTE: Object lclsDsex_condi may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
					lclsDsex_condi = Nothing
					.RNext()
				Loop 
				.RCloseRec()
			Else
				Find = False
			End If
		End With
		'UPGRADE_NOTE: Object lrecDsex_condi may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecDsex_condi = Nothing
		
Find_Err: 
		If Err.Number Then
			Find = False
		End If
		On Error GoTo 0
	End Function
	
	
	'*Item: Devuelve un elemento de la colección (segun índice)
	Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As Dsex_condi
		Get
			Item = mCol.Item(vntIndexKey)
		End Get
	End Property
	
	'*Count: Devuelve el número de elementos que posee la colección
	Public ReadOnly Property Count() As Integer
		Get
			Count = mCol.Count()
		End Get
	End Property
	
	'*NewEnum: Permite enumerar la colección para utilizarla en un ciclo For Each... Next
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
End Class






