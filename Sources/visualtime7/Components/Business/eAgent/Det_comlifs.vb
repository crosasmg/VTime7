Option Strict Off
Option Explicit On
Public Class Det_comlifs
	Implements System.Collections.IEnumerable
	'%-------------------------------------------------------%'
	'% $Workfile:: Det_comlifs.cls                          $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 1:41p                                $%'
	'% $Revision:: 12                                       $%'
	'%-------------------------------------------------------%'
	
	Private mCol As Collection
	Private nAuxComtabli As Integer
	Private dAuxEffecdate As Date
	Public nCount As Integer
	
	
	'% Add: Añade una nueva instancia de la clase Det_comlif a la colección
	Public Function Add(ByVal nStatusInstance As Integer, ByVal nComtabli As Integer, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy_dur As Integer, ByVal nMin_durat As Integer, ByVal dEffecdate As Date, ByVal nPercent As Double, ByVal nUsercode As Integer, ByVal dNulldate As Date, ByVal sProductDes As String, ByVal nModulec As Integer, ByVal nCover As Integer, ByVal nWay_Pay As Integer, ByVal nSellchannel As Integer, ByVal nMax_durat As Integer, ByVal nCurrency As Integer, ByVal nAmount As Double, ByVal sDesc_Currency As String, ByVal sDesc_Way_Pay As String, ByVal sDesc_Sellchannel As String, ByVal sDesc_Modulec As String, ByVal sDesc_Branch As String, ByVal sDesc_Cover As String) As Det_comlif
		'create a new object
		
		Dim objNewMember As Det_comlif
		objNewMember = New Det_comlif
		
		With objNewMember
			.nStatusInstance = nStatusInstance
			.nComtabli = nComtabli
			.nBranch = nBranch
			.nProduct = nProduct
			.nPolicy_dur = nPolicy_dur
			.nMin_durat = nMin_durat
			.dEffecdate = dEffecdate
			.nPercent = nPercent
			.nUsercode = nUsercode
			.dNulldate = dNulldate
			.sProductDes = sProductDes
			.nModulec = nModulec
			.nCover = nCover
			.nWay_Pay = nWay_Pay
			.nSellchannel = nSellchannel
			.nMax_durat = nMax_durat
			.nCurrency = nCurrency
			.nAmount = nAmount
			.sDesc_Currency = sDesc_Currency
			.sDesc_Way_Pay = sDesc_Way_Pay
			.sDesc_Sellchannel = sDesc_Sellchannel
			.sDesc_Modulec = sDesc_Modulec
			.sDesc_Branch = sDesc_Branch
			.sDesc_Cover = sDesc_Cover
		End With
		
		mCol.Add(objNewMember)
		'return the object created
		Add = objNewMember
		'UPGRADE_NOTE: Object objNewMember may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		objNewMember = Nothing
		
	End Function
	
	'% FindMAGC002: Devuelve una coleccion de objetos de tipo Det_comlif
	'------------------------------------------------------------
	Public Function Find(ByVal nComtabli As Integer, ByVal dEffecdate As Date, ByVal nRow As Integer, Optional ByVal lblnFind As Boolean = False) As Boolean
		'------------------------------------------------------------
		
		'- Se define la variable lrecDet_comlif que se utilizará como cursor.
		Dim lrecDet_comlif As eRemoteDB.Execute
		
		lrecDet_comlif = New eRemoteDB.Execute
		
		If nAuxComtabli = nComtabli And dAuxEffecdate = dEffecdate And Not lblnFind Then
			Find = True
		Else
			
			'+ Se ejecuta el store procedure que busca los movimientos de un intermediario
			
			With lrecDet_comlif
				.StoredProcedure = "reaDet_comlif_a"
				.Parameters.Add("nComtabli", nComtabli, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				
				nCount = 1
				
				If Not .Run Then
					Find = False
					nAuxComtabli = eRemoteDB.Constants.intNull
					dAuxEffecdate = dtmNull
				Else
					
					Find = True
					Do While Not .EOF And nCount < nRow
						nCount = nCount + 1
						.RNext()
					Loop 
					
					nAuxComtabli = nComtabli
					dAuxEffecdate = dEffecdate
					
					Do While Not .EOF And nCount < nRow + 50
						nCount = nCount + 1
						Call Add(eRemoteDB.Constants.intNull, .FieldToClass("nComtabli"), .FieldToClass("nBranch"), .FieldToClass("nProduct"), .FieldToClass("nPolicy_dur"), .FieldToClass("nMin_durat"), .FieldToClass("dEffecdate"), .FieldToClass("nPercent"), .FieldToClass("nUsercode"), .FieldToClass("dNulldate"), .FieldToClass("sDescript"), .FieldToClass("nModulec"), .FieldToClass("nCover"), .FieldToClass("nWay_Pay"), .FieldToClass("nSellchannel"), .FieldToClass("nMax_durat"), .FieldToClass("nCurrency"), .FieldToClass("nAmount"), .FieldToClass("sDesc_Currency"), .FieldToClass("sDesc_Way_Pay"), .FieldToClass("sDesc_Sellchannel"), .FieldToClass("sDesc_Modulec"), .FieldToClass("sDesc_Branch"), .FieldToClass("sDesc_Cover"))
						.RNext()
					Loop 
				End If
			End With
		End If
		'UPGRADE_NOTE: Object lrecDet_comlif may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecDet_comlif = Nothing
	End Function
	Public Function Update() As Boolean
		Dim lclsDet_comlif As Det_comlif
		For	Each lclsDet_comlif In mCol
			Select Case lclsDet_comlif.nStatusInstance
				
				'+Agregar
				
				Case 1
					Update = lclsDet_comlif.Update()
					'+Actualizar
					
				Case 2
					Update = lclsDet_comlif.Update()
					'+ Eliminar
					
				Case 3
					Update = lclsDet_comlif.Delete()
			End Select
			If Update = False Then
				Exit For
			End If
		Next lclsDet_comlif
	End Function
	
	'* Item: Devuelve un elemento de la colección (segun índice)
	Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As Det_comlif
		Get
			
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
			'
			'NewEnum = mCol._NewEnum
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
		nAuxComtabli = eRemoteDB.Constants.intNull
		dAuxEffecdate = dtmNull
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






