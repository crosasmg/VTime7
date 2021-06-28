Option Strict Off
Option Explicit On
Public Class Gen_covers
	Implements System.Collections.IEnumerable
	'%-------------------------------------------------------%'
	'% $Workfile:: Gen_covers.cls                           $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 12:35p                               $%'
	'% $Revision:: 12                                       $%'
	'%-------------------------------------------------------%'
	
	Private mCol As Collection

	'**- Variable that return the data for the DP033
	'- Variable que devuelve los datos para la DP033
	Public mobjCover As Object
	
	'**- Indicator of the module existance for the product
	'- Indicador de existencia de módulos para el producto
	Public bModule As Boolean
	Public nCover As Integer

    '**%Add: adds a new instance of the "Gen_cover" class to the collection
    '%Add: Añade una nueva instancia de la clase "Gen_cover" a la colección
    Public Sub Add(ByRef lobjGen_cover As Gen_cover)
        mCol.Add(lobjGen_cover, lobjGen_cover.nModulec & lobjGen_cover.nCover & lobjGen_cover.dEffecdate & lobjGen_cover.nBranch & lobjGen_cover.nProduct)
    End Sub

    '**%insPreDP033: makes the previous action when the page loads
    '%insPreDP033: se realizan las acciones previas a la carga de la página
    Public Function insPreDP033(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal dEffecdate As Date, ByVal sBrancht As String, Optional ByVal nModulec As Integer = 0) As Boolean
		Dim lclsProduct As Product
		
		On Error GoTo insPreDP033_Err
		
		insPreDP033 = True
		
		lclsProduct = New Product
		bModule = lclsProduct.IsModule(nBranch, nProduct, dEffecdate)
        'If (sBrancht <> CStr(Product.pmBrancht.pmlife) And sBrancht <> CStr(Product.pmBrancht.pmNotTraditionalLife)) Then
        If (sBrancht <> CStr(Product.pmBrancht.pmlife) And sBrancht <> CStr(Product.pmBrancht.pmNotTraditionalLife) And sBrancht <> CStr(Product.pmBrancht.pmMedicalAtention)) Then
            mobjCover = New Gen_covers
            insPreDP033 = mobjCover.Find(nBranch, nProduct, dEffecdate, nModulec)

        Else
            mobjCover = New Life_covers
            insPreDP033 = mobjCover.Find(nBranch, nProduct, nModulec, dEffecdate)
        End If

insPreDP033_Err:
        If Err.Number Then
            insPreDP033 = False
        End If
        On Error GoTo 0
        'UPGRADE_NOTE: Object lclsProduct may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsProduct = Nothing
    End Function
	
	'**%Find: This method fills the collection with records from the table "Gen_cover" returning TRUE or FALSE
	'**%      depending on the existence of the records
	'%Find: Este metodo carga la coleccion de elementos de la tabla "Gen_cover" devolviendo Verdadero o
	'%      falso, dependiendo de la existencia de los registros.
	Public Function Find(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal dEffecdate As Date, ByVal nModulec As Integer) As Boolean
		Dim lrecreaGen_cover1 As eRemoteDB.Execute
		Dim lclsGen_cover As Gen_cover
		
		On Error GoTo Find_Err
		
		lrecreaGen_cover1 = New eRemoteDB.Execute
		
		'**+Parameters definition for the stored procedure 'insudb.reaGen_cover1'
		'**+Data read on 04/03/2001 02:20:06 p.m.
		'+ Definición de parámetros para stored procedure 'insudb.reaGen_cover1'
		'+ Información leída el 03/04/2001 02:20:06 p.m.
		
		With lrecreaGen_cover1
			.StoredProcedure = "reaGen_cover1"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nModulec", nModulec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				Find = True
				Do While Not .EOF
					lclsGen_cover = New Gen_cover
					With lclsGen_cover
						.nModulec = lrecreaGen_cover1.FieldToClass("nModulec")
						.nBranch = lrecreaGen_cover1.FieldToClass("nBranch")
						.nCover = lrecreaGen_cover1.FieldToClass("nCover")
						.nCovergen = lrecreaGen_cover1.FieldToClass("nCovergen")
						.nProduct = lrecreaGen_cover1.FieldToClass("nProduct")
						.dEffecdate = lrecreaGen_cover1.FieldToClass("dEffecdate")
						.sAddReini = lrecreaGen_cover1.FieldToClass("sAddreini")
						.nBranch_led = lrecreaGen_cover1.FieldToClass("nBranch_led")
						.sAddSuini = lrecreaGen_cover1.FieldToClass("sAddsuini")
						.nBranch_est = lrecreaGen_cover1.FieldToClass("nBranch_est")
						.sAddTaxin = lrecreaGen_cover1.FieldToClass("sAddtaxin")
						.sAutomrep = lrecreaGen_cover1.FieldToClass("sAutomrep")
						.nBill_item = lrecreaGen_cover1.FieldToClass("nBill_item")
						.nBranch_gen = lrecreaGen_cover1.FieldToClass("nBranch_gen")
						.nBranch_rei = lrecreaGen_cover1.FieldToClass("nBranch_rei")
						.nCacalcov = lrecreaGen_cover1.FieldToClass("nCacalcov")
						.nCacalfix = lrecreaGen_cover1.FieldToClass("nCacalfix")
						.sCacalfri = lrecreaGen_cover1.FieldToClass("sCacalfri")
						.sCacalili = lrecreaGen_cover1.FieldToClass("sCacalili")
						.sBas_sumins = lrecreaGen_cover1.FieldToClass("sBas_sumins")
						.nCacalmax = lrecreaGen_cover1.FieldToClass("nCacalmax")
						.nCacalper = lrecreaGen_cover1.FieldToClass("nCacalper")
						.sCacalrei = lrecreaGen_cover1.FieldToClass("sCacalrei")
						.nRateCapAdd = lrecreaGen_cover1.FieldToClass("nRateCapAdd")
						.nRateCapSub = lrecreaGen_cover1.FieldToClass("nRateCapSub")
						.sCh_typ_cap = lrecreaGen_cover1.FieldToClass("sCh_typ_cap")
						.nRatePreAdd = lrecreaGen_cover1.FieldToClass("nRatePreAdd")
						.nRatePreSub = lrecreaGen_cover1.FieldToClass("nRatePreSub")
						.sChange_typ = lrecreaGen_cover1.FieldToClass("sChange_typ")
						.nCover_in = lrecreaGen_cover1.FieldToClass("nCover_in")
						.nCoverapl = lrecreaGen_cover1.FieldToClass("nCoverapl")
						.nCovergen = lrecreaGen_cover1.FieldToClass("nCovergen")
						.nCurrency = lrecreaGen_cover1.FieldToClass("nCurrency")
						.sDefaulti = lrecreaGen_cover1.FieldToClass("sDefaulti")
						.sFrancApl = lrecreaGen_cover1.FieldToClass("sFrancapl")
						.nFrancFix = lrecreaGen_cover1.FieldToClass("nFrancfix")
						.nFrancMax = lrecreaGen_cover1.FieldToClass("nFrancmax")
						.nFrancMin = lrecreaGen_cover1.FieldToClass("nFrancmin")
						.nFrancrat = lrecreaGen_cover1.FieldToClass("nFrancrat")
						.sFrantype = lrecreaGen_cover1.FieldToClass("sFrantype")
						.nMedreser = lrecreaGen_cover1.FieldToClass("nMedreser")
						.nNotenum = lrecreaGen_cover1.FieldToClass("nNotenum")
						.dNulldate = lrecreaGen_cover1.FieldToClass("dNulldate")
						.nPremifix = lrecreaGen_cover1.FieldToClass("nPremifix")
						.nPremimax = lrecreaGen_cover1.FieldToClass("nPremimax")
						.nPremimin = lrecreaGen_cover1.FieldToClass("nPremimin")
						.nPremirat = lrecreaGen_cover1.FieldToClass("nPremirat")
						.sRequire = lrecreaGen_cover1.FieldToClass("sRequire")
						.sRoucapit = lrecreaGen_cover1.FieldToClass("sRoucapit")
						.sRoufranc = lrecreaGen_cover1.FieldToClass("sRoufranc")
						.sRoupremi = lrecreaGen_cover1.FieldToClass("sRoupremi")
						.sRoureser = lrecreaGen_cover1.FieldToClass("sRoureser")
						.sStatregt = lrecreaGen_cover1.FieldToClass("sStatregt")
						.nChCapLev = lrecreaGen_cover1.FieldToClass("nChCapLev")
						.nChPreLev = lrecreaGen_cover1.FieldToClass("nChPreLev")
						.nCacalmin = lrecreaGen_cover1.FieldToClass("nCacalmin")
						.sFDRequire = lrecreaGen_cover1.FieldToClass("sFDRequire")
						.sFDChantyp = lrecreaGen_cover1.FieldToClass("sFDChantyp")
						.nFDUserLev = lrecreaGen_cover1.FieldToClass("nFDUserLev")
						.nFDRateAdd = lrecreaGen_cover1.FieldToClass("nFDRateAdd")
						.nFDRateSub = lrecreaGen_cover1.FieldToClass("nFDRateSub")
						.nFDRateSub = lrecreaGen_cover1.FieldToClass("nFDRateSub")
						.sInd_Med_Exp = lrecreaGen_cover1.FieldToClass("sInd_Med_Exp")
						.nApply_Perc = lrecreaGen_cover1.FieldToClass("nApply_Perc")
						.sRou_verify = lrecreaGen_cover1.FieldToClass("sRou_verify")
						.sDescript = lrecreaGen_cover1.FieldToClass("sDescript_Tab")
					End With
					Call Add(lclsGen_cover)
					'UPGRADE_NOTE: Object lclsGen_cover may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
					lclsGen_cover = Nothing
					.RNext()
				Loop 
				.RCloseRec()
			Else
				Find = False
			End If
		End With
		
Find_Err: 
		If Err.Number Then
			Find = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lclsGen_cover may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsGen_cover = Nothing
		'UPGRADE_NOTE: Object lrecreaGen_cover1 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaGen_cover1 = Nothing
	End Function
	
	'**% valModuleGen_cover: This method validates as described in the functional specifications
	'% valModuleGen_cover: Este metodo se encarga de realizar las validaciones descritas en el funcional
	Public Function valModuleGen_cover(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nElement As Integer, ByVal dEffecdate As Date) As Boolean
		Dim lrecvalModuleGen_cover As eRemoteDB.Execute
		
		On Error GoTo valModuleGen_cover_Err
		
		lrecvalModuleGen_cover = New eRemoteDB.Execute
		
		'**+Parameters definition for the stored procedure'insudb.valGen_cover'
		'**+Data read on 04/16/2001 9:52:00 a.m.
		'+ Definición de parámetros para stored procedure 'insudb.valGen_cover'
		'+ Información leída el 16/04/2001 9:52:00 a.m.
		
		With lrecvalModuleGen_cover
			.StoredProcedure = "valGen_cover"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nModulec", nElement, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				nCover = .FieldToClass("nCover")
				valModuleGen_cover = True
				.RCloseRec()
			Else
				valModuleGen_cover = False
			End If
		End With
		
valModuleGen_cover_Err: 
		If Err.Number Then
			valModuleGen_cover = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecvalModuleGen_cover may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecvalModuleGen_cover = Nothing
	End Function
	
	'**%Find_All: Searches all the associated coverage to the branch-product for a given date
	'%Find_All: busca todas las coberturas asociadas al ramo-producto para una fecha dada
	Public Function Find_All(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal dEffecdate As Date) As Boolean
		Dim lrecreaGen_cover As eRemoteDB.Execute
		Dim lclsGen_cover As Gen_cover
		
		On Error GoTo Find_All_err
		
		lrecreaGen_cover = New eRemoteDB.Execute
		
		'**+Parameters definition for the stored procedure 'insudb.reaGen_cover'
		'**+Data read on 06/23/2001 01:50:23 p.m.
		'+Definición de parámetros para stored procedure 'insudb.reaGen_cover'
		'+Información leída el 23/06/2001 01:50:23 p.m.
		
		'**+NOTE: assiged only some parameters because they were needed when the method was created
		'**+The SP return all the field, add if its necesary
		'+NOTA: se asignan solo algunos parámetros ya que se necesitaron estos al crear el método.
		'+El SP devuelve todos los campos, añadir en caso de ser necesario.
		
		With lrecreaGen_cover
			.StoredProcedure = "reaGen_cover"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				Do While Not .EOF
					lclsGen_cover = New Gen_cover
					lclsGen_cover.nModulec = .FieldToClass("nModulec")
					lclsGen_cover.nCover = .FieldToClass("nCover")
					lclsGen_cover.dEffecdate = .FieldToClass("dEffecdate")
					lclsGen_cover.nBranch = .FieldToClass("nBranch")
					lclsGen_cover.nProduct = .FieldToClass("nProduct")
					lclsGen_cover.nCovergen = .FieldToClass("nCovergen")
					
					Call Add(lclsGen_cover)
					'UPGRADE_NOTE: Object lclsGen_cover may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
					lclsGen_cover = Nothing
					.RNext()
				Loop 
				.RCloseRec()
				Find_All = True
			End If
		End With
		
Find_All_err: 
		If Err.Number Then
			Find_All = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lclsGen_cover may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsGen_cover = Nothing
		'UPGRADE_NOTE: Object lrecreaGen_cover may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaGen_cover = Nothing
	End Function
	
	'***Item: Returns an element of the collection (according to the index)
	'*Item: Devuelve un elemento de la colección (segun índice)
	Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As Gen_cover
		Get
			Item = mCol.Item(vntIndexKey)
		End Get
	End Property
	
	'***Count: Returns the number of elements that the collection has
	'*Count: Devuelve el número de elementos que posee la colección
	Public ReadOnly Property Count() As Integer
		Get
			Count = mCol.Count()
		End Get
	End Property
	
	'***NewEnum: Enumerates the collection for use in a For Each...Next loop
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
	
	'**%Remove: Deletes an element from the collection
	'%Remove: Elimina un elemento de la colección
	Public Sub Remove(ByRef vntIndexKey As Object)
		mCol.Remove(vntIndexKey)
	End Sub
	
	'**%Class_Initialize: Controls the creation of an instance of the collection
	'%Class_Initialize: Controla la creación de una instancia de la colección
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		mCol = New Collection
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
	
	'**%Class_Terminate: Controls the destruction of an instance of the collection
	'%Class_Terminate: Controla la destrucción de una instancia de la colección
	'UPGRADE_NOTE: Class_Terminate was upgraded to Class_Terminate_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Terminate_Renamed()
		'UPGRADE_NOTE: Object mCol may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		mCol = Nothing
		'UPGRADE_NOTE: Object mobjCover may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		mobjCover = Nothing
	End Sub
	Protected Overrides Sub Finalize()
		Class_Terminate_Renamed()
		MyBase.Finalize()
	End Sub
End Class






