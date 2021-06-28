Option Strict Off
Option Explicit On
Public Class Life_p_specis
	Implements System.Collections.IEnumerable
	'%-------------------------------------------------------%'
	'% $Workfile:: Life_p_specis.cls                        $%'
	'% $Author:: Nvaplat37                                  $%'
	'% $Date:: 6/04/04 7:51p                                $%'
	'% $Revision:: 13                                       $%'
	'%-------------------------------------------------------%'
	
	'local variable to hold collection
	Private mCol As Collection
	
	'-Variables de busqueda
	Private mlngBranch As Integer
	Private mlngProduct As Integer
	Private mlngModulec As Integer
    Private mlngCover As Integer
    Private mlngRole As Integer
	Private mtdmEffecdate As Date
	
	'-Variables auxiliares
	'-Indica si el producto es modular
	Public bIsModule As Boolean
	
	'-Moneda indicada en datos particulares del producto
	Public nCurrency As Integer
	
	'%Add: Agrega un nuevo registro a la colección
	Public Function Add(ByRef objClass As Life_p_speci) As Life_p_speci
		If objClass Is Nothing Then
			objClass = New Life_p_speci
		End If
		With objClass
            mCol.Add(objClass, "LPS" & .sCertype & .nBranch & .nProduct & .nPolicy & .nCertif & .nModulec & .nCover & .nRole & .nConsec & .dEffecdate.ToString("yyyyMMdd"))
		End With
		
		Add = objClass
	End Function
	
	'%Find_VI641: Lee los datos de la tabla para la transaccón VI641, si no hay información en
	'             life_p_speci, los inserta a partir de la tabla life_speci
    Public Function Find_VI641(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer,
                               ByVal nPolicy As Double, ByVal nCertif As Double, ByVal nModulec As Integer,
                               ByVal nCover As Integer, ByVal dEffecdate As Date, ByVal dNulldate As Date,
                               ByVal nUsercode As Integer, ByVal nTransaction As Integer,
                               Optional ByVal bFind As Boolean = False, Optional ByVal nRole As Integer = 0) As Boolean
        Dim lrecInsReaVI641 As eRemoteDB.Execute
        Dim lclsLife_p_speci As Life_p_speci

        On Error GoTo Find_VI641_Err

        If mlngBranch <> nBranch Or mlngProduct <> nProduct Or mlngModulec <> nModulec Or mlngCover <> nCover Or mtdmEffecdate <> dEffecdate Or bFind Then
            '+ Definición de Stored Procedure InsReaVI641 al 03-07-2002
            lrecInsReaVI641 = New eRemoteDB.Execute
            With lrecInsReaVI641
                .StoredProcedure = "InsReaVI641"
                .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nModulec", nModulec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nCover", nCover, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("dNulldate", dNulldate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nTransaction", nTransaction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nRole", nRole, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                If .Run Then
                    Find_VI641 = True
                    Do While Not .EOF
                        lclsLife_p_speci = New Life_p_speci
                        lclsLife_p_speci.dEffecdate = .FieldToClass("dEffecdate")
                        lclsLife_p_speci.nConsec = .FieldToClass("nConsec")
                        lclsLife_p_speci.nAgeStart = .FieldToClass("nAgestart")
                        lclsLife_p_speci.nAgeEnd = .FieldToClass("nAgeend")
                        lclsLife_p_speci.nCapEnd = .FieldToClass("nCapend")
                        lclsLife_p_speci.nCapStart = .FieldToClass("nCapstart")
                        lclsLife_p_speci.nCurrency = .FieldToClass("nCurrency")
                        lclsLife_p_speci.nCrthecni = .FieldToClass("nCrthecni")
                        lclsLife_p_speci.dNulldate = .FieldToClass("dNulldate")
                        lclsLife_p_speci.sSexclien = .FieldToClass("sSexclien")
                        lclsLife_p_speci.nModulec = .FieldToClass("nModulec")
                        lclsLife_p_speci.nCover = .FieldToClass("nCover")
                        lclsLife_p_speci.nRole = .FieldToClass("nRole")

                        Call Add(lclsLife_p_speci)
                        'UPGRADE_NOTE: Object lclsLife_p_speci may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
                        lclsLife_p_speci = Nothing
                        .RNext()
                    Loop
                    .RCloseRec()
                    mlngBranch = nBranch
                    mlngProduct = nProduct
                    mlngModulec = nModulec
                    mlngCover = nCover
                    mlngRole = nRole
                    mtdmEffecdate = dEffecdate
                End If
            End With
        Else
            Find_VI641 = True
        End If

Find_VI641_Err:
        If Err.Number Then
            Find_VI641 = False
        End If
        'UPGRADE_NOTE: Object lrecInsReaVI641 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecInsReaVI641 = Nothing
        On Error GoTo 0
    End Function
	'%Find_VI849: Lee los datos de la tabla LIFE_SPECI para la transaccón VI849
	Public Function Find_VI849(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal nModulec As Integer, ByVal nCover As Integer, ByVal sClient As String, ByVal dEffecdate As Date, ByVal dNulldate As Date, ByVal nUsercode As Integer, ByVal nTransaction As Integer, Optional ByVal bFind As Boolean = False) As Boolean
		Dim lrecInsReaVI849 As eRemoteDB.Execute
		Dim lclsLife_p_speci As Life_p_speci
		
		On Error GoTo Find_VI849_Err
		
		If mlngBranch <> nBranch Or mlngProduct <> nProduct Or mlngModulec <> nModulec Or mlngCover <> nCover Or mtdmEffecdate <> dEffecdate Or bFind Then
			'+ Definición de Stored Procedure InsReaVI641 al 03-07-2002
			lrecInsReaVI849 = New eRemoteDB.Execute
			With lrecInsReaVI849
				.StoredProcedure = "InsReaVI849"
				.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nModulec", nModulec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nCover", nCover, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("dNulldate", dNulldate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nTransaction", nTransaction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				If .Run Then
					Find_VI849 = True
					Do While Not .EOF
						lclsLife_p_speci = New Life_p_speci
						lclsLife_p_speci.dEffecdate = .FieldToClass("dEffecdate")
						lclsLife_p_speci.nConsec = .FieldToClass("nConsec")
						lclsLife_p_speci.nAgeStart = .FieldToClass("nAgestart")
						lclsLife_p_speci.nAgeEnd = .FieldToClass("nAgeend")
						lclsLife_p_speci.nCapEnd = .FieldToClass("nCapend")
						lclsLife_p_speci.nCapStart = .FieldToClass("nCapstart")
						lclsLife_p_speci.nCurrency = .FieldToClass("nCurrency")
						lclsLife_p_speci.nCrthecni = .FieldToClass("nCrthecni")
						lclsLife_p_speci.dNulldate = .FieldToClass("dNulldate")
						lclsLife_p_speci.sSexclien = .FieldToClass("sSexclien")
						lclsLife_p_speci.nModulec = .FieldToClass("nModulec")
						lclsLife_p_speci.nCover = .FieldToClass("nCover")
						Call Add(lclsLife_p_speci)
						'UPGRADE_NOTE: Object lclsLife_p_speci may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
						lclsLife_p_speci = Nothing
						.RNext()
					Loop 
					.RCloseRec()
					mlngBranch = nBranch
					mlngProduct = nProduct
					mlngModulec = nModulec
                    mlngCover = nCover
					mtdmEffecdate = dEffecdate
				End If
			End With
		Else
			Find_VI849 = True
		End If
		
Find_VI849_Err: 
		If Err.Number Then
			Find_VI849 = False
		End If
		'UPGRADE_NOTE: Object lrecInsReaVI849 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecInsReaVI849 = Nothing
		On Error GoTo 0
	End Function
	
	
	
	'%InsPreVI641: Obtener la información de la VI641-Criterio de riesgos de seleccion según
	'%             especificaciones funcionales
    Public Function InsPreVI641(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer,
                                ByVal nPolicy As Double, ByVal nCertif As Double, ByVal nModulec As Integer,
                                ByVal nCover As Integer, ByVal dEffecdate As Date, ByVal dNulldate As Date,
                                ByVal nUsercode As Integer, ByVal nTransaction As Integer, ByVal nRole As Integer) As Boolean
        Dim lclsProduct As eProduct.Product
        Dim lclsCurren_pol As ePolicy.Curren_pol

        lclsProduct = New eProduct.Product
        lclsCurren_pol = New ePolicy.Curren_pol
        With lclsProduct
            bIsModule = .IsModule(nBranch, nProduct, dEffecdate)
            nModulec = IIf(nModulec = eRemoteDB.Constants.intNull, 0, nModulec)
            If lclsCurren_pol.findCurrency(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate) <> "" Then
                nCurrency = lclsCurren_pol.nCurrency
                InsPreVI641 = Find_VI641(sCertype, nBranch, nProduct,
                                         nPolicy, nCertif, nModulec,
                                         nCover, dEffecdate, dNulldate,
                                         nUsercode, nTransaction, False,
                                         nRole)
            End If
        End With
        'UPGRADE_NOTE: Object lclsProduct may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsProduct = Nothing
        lclsCurren_pol = Nothing
    End Function
	
	'%InsPreVI849: Obtener la información de la VI849-Criterio de selección de riesgos (Asegurado)
	Public Function InsPreVI849(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal nModulec As Integer, ByVal nCover As Integer, ByVal sClient As String, ByVal dEffecdate As Date, ByVal dNulldate As Date, ByVal nUsercode As Integer, ByVal nTransaction As Integer) As Boolean
		Dim lclsProduct As eProduct.Product
		lclsProduct = New eProduct.Product
		With lclsProduct
			bIsModule = .IsModule(nBranch, nProduct, dEffecdate)
			nModulec = IIf(nModulec = eRemoteDB.Constants.intNull, 0, nModulec)
			If .FindProduct_li(nBranch, nProduct, dEffecdate) Then
				nCurrency = .nCurrency
				InsPreVI849 = Find_VI849(sCertype, nBranch, nProduct, nPolicy, nCertif, nModulec, nCover, sClient, dEffecdate, dNulldate, nUsercode, nTransaction)
				
			End If
		End With
		'UPGRADE_NOTE: Object lclsProduct may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsProduct = Nothing
	End Function
	
	'% Item: Se usa para referenciar un elemento de la colección
	Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As Life_p_speci
		Get
			Item = mCol.Item(vntIndexKey)
		End Get
	End Property
	
	'% Count: Se usa para obtener el numero de elementos de la colección
	Public ReadOnly Property Count() As Integer
		Get
			Count = mCol.Count()
		End Get
	End Property
	
	'% NewEnum: Obtiene un item de la colección
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
	
	'% Remove: Se usa para remover elementos de la colección
	Public Sub Remove(ByRef vntIndexKey As Object)
		mCol.Remove(vntIndexKey)
	End Sub
	
	'% Class_Initialize: Crea la colección
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		mlngBranch = eRemoteDB.Constants.intNull
		mlngProduct = eRemoteDB.Constants.intNull
		mlngModulec = eRemoteDB.Constants.intNull
        mlngCover = eRemoteDB.Constants.intNull
        mlngRole = eRemoteDB.Constants.intNull

		mtdmEffecdate = eRemoteDB.Constants.dtmNull
		mCol = New Collection
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
	
	'% Class_Terminate: Destruye la colección
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






