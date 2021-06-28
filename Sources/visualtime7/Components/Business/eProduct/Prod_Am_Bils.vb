Option Strict Off
Option Explicit On
Public Class Prod_Am_Bils
	Implements System.Collections.IEnumerable
	'%-------------------------------------------------------%'
	'% $Workfile:: Prod_Am_Bils.cls                          $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 12:35p                               $%'
	'% $Revision:: 11                                       $%'
	'%-------------------------------------------------------%'
	
	'+ Variable local para contener colección.
	
	Private mCol As Collection
	
	'+ Se definen las propiedades auxiliares.
	
	Private mintBranch As Integer
	Private mintProduct As Integer
	Private mintModulec As Integer
	Private mintCover As Integer
	Private mintRole As Integer
	Private mstrIllness As String
	Private mdtmEffecdate As Date
	
	'% AddProd_Am_Bil: Este método permite añadir registros a la colección.
	Public Function AddProd_Am_Bil(ByRef nBranch As Integer, ByRef nProduct As Integer, ByRef nModulec As Integer, ByRef nCover As Integer, ByRef nRole As Integer, ByRef sIllness As String, ByRef nGroup_Pres As Integer, ByRef nPay_Concep As Integer, ByRef dEffecdate As Date, ByRef nCurrency As Integer, ByRef nDed_Type As Integer, ByRef Desc_nDed_Type As String, ByRef nDed_Amount As Double, ByRef nDed_Percen As Double, ByRef nDed_Quanti As Double, ByRef nIndem_rate As Integer, ByRef nLimit As Double, ByRef nTypLim As Integer, ByRef Desc_TypLim As String, ByRef nCount As Double, ByRef nLimit_exe As Double, ByRef nPunish As Double, ByRef nDed_Quanti_2 As Double, ByRef nIndem_Rate_2 As Double, ByRef nLimit_2 As Double, ByRef nTypLim_2 As Integer, ByRef Desc_TypLim_2 As String, ByRef nCount_2 As Double, ByRef nLimit_exe_2 As Double, ByRef nPunish_2 As Double, ByRef dNulldate As Date) As Prod_Am_Bil
		'+ Crear un nuevo objeto.
		Dim objNewMember As Prod_Am_Bil
		
		'+ Establecer las propiedades que se transfieren al método.
		objNewMember = New Prod_Am_Bil
		With objNewMember
			.nGroup_Pres = nGroup_Pres
			.nPay_Concep = nPay_Concep
			.dEffecdate = dEffecdate
			.nCurrencyAux = nCurrency
			.nCurrency = nCurrency
			.nDed_Type = nDed_Type
			.Desc_Ded_Type = Desc_nDed_Type
			.nDed_Amount = nDed_Amount
			.nDed_Percen = nDed_Percen
			.nDed_Quanti = nDed_Quanti
			.nIndem_rate = nIndem_rate
			.nLimit = nLimit
			.nTypLim = nTypLim
			.Desc_TypLim = Desc_TypLim
			.nCount = nCount
			.nLimit_exe = nLimit_exe
			.nPunish = nPunish
			.nDed_Quanti_2 = nDed_Quanti_2
			.nIndem_Rate_2 = nIndem_Rate_2
			.nLimit_2 = nLimit_2
			.nTypLim_2 = nTypLim_2
			.Desc_TypLim_2 = Desc_TypLim_2
			.nCount_2 = nCount_2
			.nLimit_exe_2 = nLimit_exe_2
			.nPunish_2 = nPunish_2
			.dNulldate = dNulldate
		End With
		
		mCol.Add(objNewMember, "A" & CStr(nBranch) & CStr(nProduct) & CStr(nModulec) & CStr(nCover) & CStr(nRole) & CStr(nPay_Concep) & CStr(sIllness) & CStr(dEffecdate))
		
		'+ Return the object created.
		
		AddProd_Am_Bil = objNewMember
		'UPGRADE_NOTE: Object objNewMember may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		objNewMember = Nothing
	End Function
	
	'%Item: Devuelve un elemento de la colección (segun índice)
	Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As Prod_Am_Bil
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
	
	'% FindProd_Am_Bil: Verifica que exista información.
	Public Function FindProd_Am_Bil(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nModulec As Integer, ByVal nCover As Integer, ByVal nRole As Integer, ByVal sIllness As String, ByVal dEffecdate As Date, Optional ByVal lblnFind As Boolean = True) As Boolean
		Dim lrecReaProd_Am_Bil As eRemoteDB.Execute
		
		lrecReaProd_Am_Bil = New eRemoteDB.Execute
		
		On Error GoTo FindProd_Am_Bil_Err
		
		FindProd_Am_Bil = True
		
		If nBranch <> mintBranch Or nProduct <> mintProduct Or nModulec <> mintModulec Or nCover <> mintCover Or nRole <> mintRole Or sIllness <> mstrIllness Or dEffecdate <> mdtmEffecdate Or lblnFind Then
			'UPGRADE_NOTE: Object mCol may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
			mCol = Nothing
			mCol = New Collection
			
			'+ Definición de parámetros para stored procedure
			With lrecReaProd_Am_Bil
				.StoredProcedure = "reaProd_Am_Bil"
				
				.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nModulec", nModulec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nCover", nCover, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nRole", nRole, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("sIllness", sIllness, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 8, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				
				If .Run Then
					mintBranch = nBranch
					mintProduct = nProduct
					mintModulec = nModulec
					mintCover = nCover
					mintRole = nRole
					mstrIllness = sIllness
					mdtmEffecdate = dEffecdate
					
					Do While Not .EOF
						Call AddProd_Am_Bil(mintBranch, mintProduct, mintModulec, mintCover, mintRole, mstrIllness, .FieldToClass("nGroup_Pres"), .FieldToClass("nPay_Concep"), .FieldToClass("dEffecdate"), .FieldToClass("nCurrency"), .FieldToClass("nDed_Type"), .FieldToClass("Desc_nDed_Type"), .FieldToClass("nDed_Amount"), .FieldToClass("nDed_Percen"), .FieldToClass("nDed_Quanti"), .FieldToClass("nIndem_rate"), .FieldToClass("nLimit"), .FieldToClass("nTypLim"), .FieldToClass("Desc_TypLim"), .FieldToClass("nCount"), .FieldToClass("nLimit_exe"), .FieldToClass("nPunish"), .FieldToClass("nDed_Quanti_2"), .FieldToClass("nIndem_Rate_2"), .FieldToClass("nLimit_2"), .FieldToClass("nTypLim_2"), .FieldToClass("Desc_TypLim_2"), .FieldToClass("nCount_2"), .FieldToClass("nLimit_exe_2"), .FieldToClass("nPunish_2"), .FieldToClass("dNulldate"))
						.RNext()
					Loop 
					.RCloseRec()
				Else
					FindProd_Am_Bil = False
					mintBranch = 0
					mintProduct = 0
					mintModulec = 0
					mintCover = 0
					mintRole = 0
					mstrIllness = CStr(Nothing)
					mdtmEffecdate = CDate(Nothing)
				End If
			End With
			
			'UPGRADE_NOTE: Object lrecReaProd_Am_Bil may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
			lrecReaProd_Am_Bil = Nothing
		End If
		
FindProd_Am_Bil_Err: 
		If Err.Number Then
			FindProd_Am_Bil = False
		End If
		
		On Error GoTo 0
	End Function
End Class






