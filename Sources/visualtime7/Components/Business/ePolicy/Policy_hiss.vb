Option Strict Off
Option Explicit On
Public Class Policy_hiss
	Implements System.Collections.IEnumerable
	'%-------------------------------------------------------%'
	'% $Workfile:: Policy_hiss.cls                          $%'
	'% $Author:: Nvaplat41                                  $%'
	'% $Date:: 9/10/03 19.01                                $%'
	'% $Revision:: 11                                       $%'
	'%-------------------------------------------------------%'
	
	'- Variable de la colection
	Private mCol As Collection
	
	'- Variables que guardan la llave de busqueda
	Private mstrCertype As String
	Private mlngBranch As Integer
	Private mlngProduct As Integer
	Private mlngPolicy As Integer
	Private mlngCertif As Integer
	Private mdtmEffecdate As Date
	
	'% Add: Agrega un nuevo registro a la colección
	Public Function Add(ByRef objClass As Policy_his) As Policy_his
		If objClass Is Nothing Then
			objClass = New Policy_his
		End If
		
		With objClass
			mCol.Add(objClass, .sCertype & .nBranch & .nProduct & .nPolicy & .nCertif & .nMovement)
		End With
		'+ Retorna objeto creado
		Add = objClass
	End Function
	
	'% Find: Busca los registros de la historio
	Public Function Find(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, Optional ByVal nCertif As Double = eRemoteDB.Constants.intNull, Optional ByVal dEffecdate As Date = eRemoteDB.Constants.dtmNull, Optional ByVal nType_amend As Integer = eRemoteDB.Constants.intNull, Optional ByVal lblnFind As Boolean = False) As Boolean
		Dim lrecreaPolicy_his_a As eRemoteDB.Execute
		Dim lclsPolicy_his As Policy_his
		
		On Error GoTo reaPolicy_his_a_Err
		
		lrecreaPolicy_his_a = New eRemoteDB.Execute
		'+ Definición de store procedure reaPolicy_his_a al 04-30-2002 18:37:41
		With lrecreaPolicy_his_a
			.StoredProcedure = "reaPolicy_his_a"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nType_amend", nType_amend, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				Find = True
				Do While Not .EOF
					lclsPolicy_his = New Policy_his
					lclsPolicy_his.sCertype = .FieldToClass("sCertype")
					lclsPolicy_his.nBranch = .FieldToClass("nBranch")
					lclsPolicy_his.nProduct = .FieldToClass("nProduct")
					lclsPolicy_his.nPolicy = .FieldToClass("nPolicy")
					lclsPolicy_his.nCertif = .FieldToClass("nCertif")
					lclsPolicy_his.nMovement = .FieldToClass("nMovement")
					lclsPolicy_his.nClaim = .FieldToClass("nClaim")
					lclsPolicy_his.nCurrency = .FieldToClass("nCurrency")
					lclsPolicy_his.dEffecdate = .FieldToClass("dEffecdate")
					lclsPolicy_his.sNull_move = .FieldToClass("sNull_move")
					lclsPolicy_his.dNulldate = .FieldToClass("dNulldate")
					lclsPolicy_his.nReceipt = .FieldToClass("nReceipt")
					lclsPolicy_his.nTransactio = .FieldToClass("nTransactio")
					lclsPolicy_his.nType_Hist = .FieldToClass("nType_hist")
					lclsPolicy_his.dLedgerDat = .FieldToClass("dLedgerdat")
					lclsPolicy_his.nOficial_p = .FieldToClass("nOficial_p")
					lclsPolicy_his.nType_amend = .FieldToClass("nType_amend")
					lclsPolicy_his.nServ_order = .FieldToClass("nServ_order")
					lclsPolicy_his.nNotenum = .FieldToClass("nNotenum")
					lclsPolicy_his.sIntermei = .FieldToClass("sIntermei")
					lclsPolicy_his.dFer = .FieldToClass("dFer")
					lclsPolicy_his.nproponum = .FieldToClass("nProponum")
					lclsPolicy_his.dCompdate = .FieldToClass("dCompdate")
					lclsPolicy_his.sDesctran = .FieldToClass("sDesctran")
					lclsPolicy_his.sDescurr = .FieldToClass("sDescurr")
					lclsPolicy_his.sDescType_amend = .FieldToClass("sDescType_amend")
					lclsPolicy_his.sCliename = .FieldToClass("scliename")
					lclsPolicy_his.nWait_code = .FieldToClass("nWait_code")
					Call Add(lclsPolicy_his)
					'UPGRADE_NOTE: Object lclsPolicy_his may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
					lclsPolicy_his = Nothing
					.RNext()
				Loop 
				.RCloseRec()
			Else
				Find = False
			End If
		End With
		
reaPolicy_his_a_Err: 
		If Err.Number Then
			Find = False
		End If
		'UPGRADE_NOTE: Object lrecreaPolicy_his_a may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaPolicy_his_a = Nothing
		On Error GoTo 0
	End Function
	
	'% Item: Devuelve un elemento de la colección (segun índice)
	Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As Policy_his
		Get
			Item = mCol.Item(vntIndexKey)
		End Get
	End Property
	
	'% Count: Devuelve el número de elementos que posee la colección
	Public ReadOnly Property Count() As Integer
		Get
			Count = mCol.Count()
		End Get
	End Property
	
	'% NewEnum: Permite enumerar la colección para utilizarla en un ciclo For Each... Next
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
	
	'% Remove: Elimina un elemento de la colección
	Public Sub Remove(ByRef vntIndexKey As Object)
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

    Public Function FindCal0110(ByVal nTypeOption As Integer, ByVal nTypeReport As Integer, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal dIssuedatIni As Date, ByVal dIssuedatEnd As Date, ByVal nPolicy As Double, ByVal nCertif As Integer, Optional ByVal lblnFind As Boolean = False) As Boolean
        'Public Function FindCal0110(ByVal nTypeReport As Integer, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nPolicyini As Double, ByVal nPolicyfin As Double, ByVal nProponum As Double, Optional ByVal lblnFind As Boolean = False) As Boolean
        Dim lrecreaCal0110 As eRemoteDB.Execute
        Dim lclsPolicy_his As Policy_his

        On Error GoTo reaCal0110_Err

        lrecreaCal0110 = New eRemoteDB.Execute
        '+ Definición de store procedure reaPolicy_his_a al 04-30-2002 18:37:41
        With lrecreaCal0110
            .StoredProcedure = "INSCAL0110PKG.REACAL0110"
            .Parameters.Add("nTypeOption", nTypeOption, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nTypeReport", nTypeReport, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dIssuedatIni", dIssuedatIni, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dIssuedatEnd", dIssuedatEnd, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            If .Run Then
                FindCal0110 = True
                Do While Not .EOF
                    lclsPolicy_his = New Policy_his
                    If nTypeOption = 1 Then '+ Puntual
                        lclsPolicy_his.nProduct = .FieldToClass("nProduct")
                        lclsPolicy_his.nPolicy = .FieldToClass("nPolicy")
                        lclsPolicy_his.nMovement = .FieldToClass("nMovement")
                        lclsPolicy_his.sNull_move = .FieldToClass("sNull_move")
                        lclsPolicy_his.dNulldate = .FieldToClass("dNulldate")
                        lclsPolicy_his.nType_Hist = .FieldToClass("nType_hist")
                        lclsPolicy_his.nType_amend = .FieldToClass("nType_amend")
                        lclsPolicy_his.nProponum = .FieldToClass("nProponum")
                        lclsPolicy_his.sFile_report = .FieldToClass("sfile_report")
                        lclsPolicy_his.sDesProduct = .FieldToClass("sDesProduct")
                        lclsPolicy_his.sDescType_amend = .FieldToClass("sDescType_amend")
                        lclsPolicy_his.sDescType_Hist = .FieldToClass("sDesCType_Hist")
                        lclsPolicy_his.dEffecdate = .FieldToClass("dEffecdate")
                        lclsPolicy_his.sPolitype = .FieldToClass("sPolitype")
                    Else '+ Masivo

                        lclsPolicy_his.nBranch = .FieldToClass("nBranch")
                        lclsPolicy_his.nProduct = .FieldToClass("nProduct")
                        If nTypeReport = 3 Then 'Certificados de Cobertura
                            lclsPolicy_his.nPolicy = .FieldToClass("nPolicy")
                        End If
                        lclsPolicy_his.nCountRegist = .FieldToClass("nCountReg")
                        lclsPolicy_his.sDesBranch = .FieldToClass("sDesBranch")
                        lclsPolicy_his.sDesProduct = .FieldToClass("sDesProduct")
                    End If

                    Call Add(lclsPolicy_his)
                    'UPGRADE_NOTE: Object lclsPolicy_his may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
                    lclsPolicy_his = Nothing
                    .RNext()
                Loop
                .RCloseRec()
            Else
                FindCal0110 = False
            End If
        End With

reaCal0110_Err:
        If Err.Number Then
            FindCal0110 = False
        End If
        'UPGRADE_NOTE: Object lrecreaPolicy_his_a may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecreaCal0110 = Nothing
        On Error GoTo 0
    End Function
    '% FindCal0110_massive: Se encarga de cargar datos en la tabla TMP_GIL5010 
    Public Function FindCal0110_massive(ByVal sKey As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal dIssuedatIni As Date, ByVal dIssuedatEnd As Date, ByVal nUsercode As Double, ByVal nPolicy As Double, ByVal nCertif As Double) As Boolean

        Dim lrecreaCal0110 As eRemoteDB.Execute
        Dim lclsPolicy_his As Policy_his

        On Error GoTo reaCal0110_Err

        lrecreaCal0110 = New eRemoteDB.Execute
        '+ Definición de store procedure INSCAL0110MASSIVEPKG.INSCAL0110MASSIVE
        With lrecreaCal0110
            .StoredProcedure = "INSCAL0110MASSIVEPKG.INSCAL0110MASSIVE"
            .Parameters.Add("sKey", sKey, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dIssuedatIni", dIssuedatIni, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dIssuedatEnd", dIssuedatEnd, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If .Run Then
                FindCal0110_massive = True
                Do While Not .EOF
                    lclsPolicy_his = New Policy_his
                    lclsPolicy_his.sCertype = .FieldToClass("sCertype")
                    lclsPolicy_his.nBranch = .FieldToClass("nBranch")
                    lclsPolicy_his.nProduct = .FieldToClass("nProduct")
                    lclsPolicy_his.nProduct = .FieldToClass("nProduct")
                    lclsPolicy_his.nPolicy = .FieldToClass("nPolicy")
                    lclsPolicy_his.nCertif = .FieldToClass("nCertif")
                    lclsPolicy_his.dEffecdate = .FieldToClass("DSTARTDATE")
                    lclsPolicy_his.nTransactio = .FieldToClass("NCONSEC")
                    Call Add(lclsPolicy_his)
                    'UPGRADE_NOTE: Object lclsPolicy_his may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
                    lclsPolicy_his = Nothing
                    .RNext()
                Loop
                .RCloseRec()
            Else
                FindCal0110_massive = False
            End If
        End With

reaCal0110_Err:
        If Err.Number Then
            FindCal0110_massive = False
        End If
        'UPGRADE_NOTE: Object lrecreaPolicy_his_a may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecreaCal0110 = Nothing
        On Error GoTo 0
    End Function
    Public Function updCal0110_massive(ByVal sKey As String, ByVal nConsec As Double) As Boolean
        'Public Function FindCal0110(ByVal nTypeReport As Integer, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nPolicyini As Double, ByVal nPolicyfin As Double, ByVal nProponum As Double, Optional ByVal lblnFind As Boolean = False) As Boolean
        Dim lrecreaCal0110 As eRemoteDB.Execute
        Dim lclsPolicy_his As Policy_his

        On Error GoTo reaCal0110_Err

        lrecreaCal0110 = New eRemoteDB.Execute
        '+ Definición de store procedure reaPolicy_his_a al 04-30-2002 18:37:41
        With lrecreaCal0110
            .StoredProcedure = "INSCAL0110MASSIVEPKG.INSUPDCAL0110MASSIVE"
            .Parameters.Add("sKey", sKey, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nConsec", nConsec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If .Run(False) Then
                updCal0110_massive = True
            Else
                updCal0110_massive = False
            End If
        End With

reaCal0110_Err:
        If Err.Number Then
            updCal0110_massive = False
        End If
        'UPGRADE_NOTE: Object lrecreaPolicy_his_a may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecreaCal0110 = Nothing
        On Error GoTo 0
    End Function

    Public Function updCal0110_massive_end(ByVal sKey As String, ByVal nUsercode As Double) As Boolean
        'Public Function FindCal0110(ByVal nTypeReport As Integer, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nPolicyini As Double, ByVal nPolicyfin As Double, ByVal nProponum As Double, Optional ByVal lblnFind As Boolean = False) As Boolean
        Dim lrecreaCal0110 As eRemoteDB.Execute
        Dim lclsPolicy_his As Policy_his

        On Error GoTo reaCal0110_Err

        lrecreaCal0110 = New eRemoteDB.Execute
        '+ Definición de store procedure reaPolicy_his_a al 04-30-2002 18:37:41
        With lrecreaCal0110
            .StoredProcedure = "INSCAL0110MASSIVEPKG.UPDCAL0110MASSIVE"
            .Parameters.Add("sKey", sKey, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If .Run(False) Then
                updCal0110_massive_end = True
            Else
                updCal0110_massive_end = False
            End If
        End With

reaCal0110_Err:
        If Err.Number Then
            updCal0110_massive_end = False
        End If
        'UPGRADE_NOTE: Object lrecreaPolicy_his_a may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecreaCal0110 = Nothing
        On Error GoTo 0
    End Function

End Class