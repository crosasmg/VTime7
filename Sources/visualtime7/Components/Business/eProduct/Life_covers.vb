Option Strict Off
Option Explicit On
Public Class Life_covers
	Implements System.Collections.IEnumerable
	'%-------------------------------------------------------%'
	'% $Workfile:: Life_covers.cls                          $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 12:35p                               $%'
	'% $Revision:: 11                                       $%'
	'%-------------------------------------------------------%'
	
	Private mCol As Collection
	
	'**- Define one auxiliary variable to force the data search in the table
	'- Se define una variable auxiliar para forzar la búsqueda de los datos en la tabla
	
	Private mAuxClient As String
	
	'**% Add: add a new element to the collection
	'% Add: añade un nuevo elemento a la colección
	Public Function Add(ByRef nStatusInstance As Integer, ByRef pclsLife_Cover As Life_cover) As Life_cover
		pclsLife_Cover.nStatusInstance = nStatusInstance
		
		mCol.Add(pclsLife_Cover, "LC" & pclsLife_Cover.nBranch & pclsLife_Cover.nProduct & pclsLife_Cover.nModulec & pclsLife_Cover.nCover)
		
		
		Add = pclsLife_Cover
		'UPGRADE_NOTE: Object pclsLife_Cover may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		pclsLife_Cover = Nothing
	End Function
	
	'**% Update: runs the collection and updates the table data
	'% Update: recorre la colección y actualiza los datos en la tabla
	Public Function Update() As Boolean
		Dim lclsLife_cover As Life_cover
		
		'**+ Possible values for nStatusInstance
		'**+ 0: The record is new
		'**+ 1: The record exist in the table
		'**+ 2: The record exist, it has to be actualize
		'**+ 3: The record exist, it has to be deleted
		'+ Valores posibles para nStatusInstance
		'+ 0: El registro es nuevo
		'+ 1: El registro ya existe en la tabla
		'+ 2: El registro ya existe, hay que actualizarlo
		'+ 3: El registro ya existe, hay que eliminarlo
		
		Update = True
		
		For	Each lclsLife_cover In mCol
			With lclsLife_cover
				Select Case .nStatusInstance
					Case 0
						Update = .Update
						.nStatusInstance = 1
					Case 2
						Update = .Update
					Case 3
						Update = .Delete
						mCol.Remove(("LC" & .nBranch & .nProduct & .nModulec & .nCover))
				End Select
			End With
		Next lclsLife_cover
	End Function
	
	'**% Find: finds the corresponding data to one client
	'% Find: busca los datos correspondientes a un cliente
	Public Function Find(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nModulec As Integer, ByVal dEffecdate As Date) As Boolean
		Dim lrecReaLife_cover_a As eRemoteDB.Execute
		Dim lclsLife_cover As Life_cover
		
		On Error GoTo Find_Err
		
		lrecReaLife_cover_a = New eRemoteDB.Execute
		
		'+Definición de parámetros para stored procedure 'ReaLife_cover_a'
		'+Información leída el 30/10/01
		With lrecReaLife_cover_a
			.StoredProcedure = "ReaLife_cover_a"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nModulec", nModulec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				Find = True
				Do While Not .EOF
					lclsLife_cover = New Life_cover
					lclsLife_cover.nBranch = .FieldToClass("nBranch")
					lclsLife_cover.nCover = .FieldToClass("nCover")
					lclsLife_cover.nProduct = .FieldToClass("nProduct")
					lclsLife_cover.nModulec = .FieldToClass("nModulec")
					lclsLife_cover.dEffecdate = .FieldToClass("dEffecdate")
					lclsLife_cover.sAddSuini = .FieldToClass("sAddsuini")
					lclsLife_cover.sAddReini = .FieldToClass("sAddreini")
					lclsLife_cover.sAddTaxin = .FieldToClass("sAddtaxin")
					lclsLife_cover.nCovergen = .FieldToClass("nCovergen")
					lclsLife_cover.nBill_item = .FieldToClass("nBill_item")
					lclsLife_cover.nBranch_est = .FieldToClass("nBranch_est")
					lclsLife_cover.nBranch_gen = .FieldToClass("nBranch_gen")
					lclsLife_cover.nBranch_led = .FieldToClass("nBranch_led")
					lclsLife_cover.nBranch_rei = .FieldToClass("nBranch_rei")
					lclsLife_cover.nCaextexp = .FieldToClass("nCaextexp")
					lclsLife_cover.nCaintexp = .FieldToClass("nCaintexp")
					lclsLife_cover.sCoveruse = .FieldToClass("sCoveruse")
					lclsLife_cover.nCurrency = .FieldToClass("nCurrency")
					lclsLife_cover.nInterest = .FieldToClass("nInterest")
					lclsLife_cover.sMortacof = .FieldToClass("sMortacof")
					lclsLife_cover.sMortacom = .FieldToClass("sMortacom")
					lclsLife_cover.dNulldate = .FieldToClass("dNulldate")
					lclsLife_cover.nNotenum = .FieldToClass("nNotenum")
					lclsLife_cover.nPrextexp = .FieldToClass("nPrextexp")
					lclsLife_cover.nPrintexp = .FieldToClass("nPrintexp")
					lclsLife_cover.sRoureser = .FieldToClass("sRoureser")
					lclsLife_cover.sRousurre = .FieldToClass("sRousurre")
					lclsLife_cover.sStatregt = .FieldToClass("sStatregt")
					lclsLife_cover.sControl = .FieldToClass("sControl")
					lclsLife_cover.nRetarif = .FieldToClass("nRetarif")
					lclsLife_cover.nPer_tabmor = .FieldToClass("nPer_tabmor")
					lclsLife_cover.sCalrein = .FieldToClass("sCalrein")
					lclsLife_cover.sDepend = .FieldToClass("sDepend")
					lclsLife_cover.sDescript = .FieldToClass("sDescript")
					Call Add(1, lclsLife_cover)
					'UPGRADE_NOTE: Object lclsLife_cover may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
					lclsLife_cover = Nothing
					.RNext()
				Loop 
				.RCloseRec()
				Find = True
			Else
				Find = False
			End If
		End With
		
Find_Err: 
		If Err.Number Then
			Find = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecReaLife_cover_a may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecReaLife_cover_a = Nothing
	End Function
	
	'**% Find_Covergen: finds the corresponding data to one client
	'% Find_Covergen: busca los datos correspondientes a un cliente
	Public Function Find_Covergen(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal dEffecdate As Date) As Boolean
		Dim lrecreaLife_cover_2 As eRemoteDB.Execute
		Dim lclsLife_cover As Life_cover
		
		On Error GoTo Find_covergen_err
		
		lrecreaLife_cover_2 = New eRemoteDB.Execute
		
		'+Definición de parámetros para stored procedure 'ReaLife_cover_a'
		'+Información leída el 30/10/01
		With lrecreaLife_cover_2
			.StoredProcedure = "reaLife_cover_2"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run Then
				Do While Not .EOF
					lclsLife_cover = New Life_cover
					lclsLife_cover.nBranch = .FieldToClass("nBranch")
					lclsLife_cover.nCover = .FieldToClass("nCover")
					lclsLife_cover.nProduct = .FieldToClass("nProduct")
					lclsLife_cover.nModulec = .FieldToClass("nModulec")
					lclsLife_cover.dEffecdate = .FieldToClass("dEffecdate")
					lclsLife_cover.sAddSuini = .FieldToClass("sAddsuini")
					lclsLife_cover.sAddReini = .FieldToClass("sAddreini")
					lclsLife_cover.sAddTaxin = .FieldToClass("sAddtaxin")
					lclsLife_cover.nCovergen = .FieldToClass("nCovergen")
					lclsLife_cover.nBill_item = .FieldToClass("nBill_item")
					lclsLife_cover.nBranch_led = .FieldToClass("nBranch_led")
					lclsLife_cover.nBranch_rei = .FieldToClass("nBranch_rei")
					lclsLife_cover.nBranch_est = .FieldToClass("nBranch_est")
					lclsLife_cover.nBranch_gen = .FieldToClass("nBranch_gen")
					lclsLife_cover.nCaextexp = .FieldToClass("nCaextexp")
					lclsLife_cover.nCaintexp = .FieldToClass("nCaintexp")
					lclsLife_cover.sCoveruse = .FieldToClass("sCoveruse")
					lclsLife_cover.nCurrency = .FieldToClass("nCurrency")
					lclsLife_cover.nInterest = .FieldToClass("nInterest")
					lclsLife_cover.sMortacof = .FieldToClass("sMortacof")
					lclsLife_cover.sMortacom = .FieldToClass("sMortacom")
					lclsLife_cover.dNulldate = .FieldToClass("dNulldate")
					lclsLife_cover.nNotenum = .FieldToClass("nNotenum")
					lclsLife_cover.nPrextexp = .FieldToClass("nPrextexp")
					lclsLife_cover.nPrintexp = .FieldToClass("nPrintexp")
					lclsLife_cover.sRoureser = .FieldToClass("sRoureser")
					lclsLife_cover.sRousurre = .FieldToClass("sRousurre")
					lclsLife_cover.sStatregt = .FieldToClass("sStatregt")
					lclsLife_cover.sDescript = .FieldToClass("sDescript_tab")
					
					Call Add(1, lclsLife_cover)
					
					'UPGRADE_NOTE: Object lclsLife_cover may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
					lclsLife_cover = Nothing
					.RNext()
				Loop 
				.RCloseRec()
				Find_Covergen = True
			Else
				Find_Covergen = False
			End If
		End With
		
Find_covergen_err: 
		If Err.Number Then
			Find_Covergen = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecreaLife_cover_2 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaLife_cover_2 = Nothing
	End Function
	
	'*** Item: takes one element from the collection
	'* Item: toma un elemento de la colección
	Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As Life_cover
		Get
			Item = mCol.Item(vntIndexKey)
		End Get
	End Property
	
	'*** Count: counts the number of elements inside the collection
	'* Count: cuenta el número de elementos dentro de la colección
	Public ReadOnly Property Count() As Integer
		Get
			Count = mCol.Count()
		End Get
	End Property
	
	'*** NewEnum : enumerates the elements inside the collection
	'* NewEnum: enumera los elementos dentro de la colección
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
	
	'*** Remove: deletes one element inside the collection
	'* Remove: elimina un elemento dentro de la colección
	Public Sub Remove(ByRef vntIndexKey As Object)
		mCol.Remove(vntIndexKey)
	End Sub
	
	'*** Class_Initialize: controls the reopening of each instance from the collection
	'* Class_Initialize: controla la apertura de cada instancia de la colección
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		mCol = New Collection
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
	
	'*** Class_Terminate: deletes the collection
	'* Class_Terminate: elimina la colección
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






