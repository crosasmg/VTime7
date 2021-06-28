Option Strict Off
Option Explicit On
'UPGRADE_WARNING: Class instancing was changed to public. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="ED41034B-3890-49FC-8076-BD6FC2F42A85"'
Public Class tmp_undo_Move_Accs
	Implements System.Collections.IEnumerable
	'variable local para contener colección
	Private mCol As Collection
	
	Public Function Add(ByVal objClass As Tmp_undo_Move_Acc) As Tmp_undo_Move_Acc
		'crear un nuevo objeto
		If objClass Is Nothing Then
			objClass = New Tmp_undo_Move_Acc
		End If
		
		With objClass
			mCol.Add(objClass, CStr(.nidconsec))
		End With
		
		'return the object created
		Add = objClass
	End Function
	
	Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As Tmp_undo_Move_Acc
		Get
			'se usa al hacer referencia a un elemento de la colección
			'vntIndexKey contiene el índice o la clave de la colección,
			'por lo que se declara como un Variant
			'Syntax: Set foo = x.Item(xyz) or Set foo = x.Item(5)
			Item = mCol.Item(vntIndexKey)
		End Get
	End Property
	
	
	
	Public ReadOnly Property Count() As Integer
		Get
			'se usa al obtener el número de elementos de la
			'colección. Sintaxis: Debug.Print x.Count
			Count = mCol.Count()
		End Get
	End Property
	
	
	'UPGRADE_NOTE: NewEnum property was commented out. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="B3FC1610-34F3-43F5-86B7-16C984F0E88E"'
	'Public ReadOnly Property NewEnum() As stdole.IUnknown
		'Get
			'esta propiedad permite enumerar
			'esta colección con la sintaxis For...Each
			'NewEnum = mCol._NewEnum
		'End Get
	'End Property
	
	Public Function GetEnumerator() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
		'UPGRADE_TODO: Uncomment and change the following line to return the collection enumerator. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="95F9AAD0-1319-4921-95F0-B9D3C4FF7F1C"'
		GetEnumerator = mCol.GetEnumerator
	End Function
	
	
	Public Sub Remove(ByRef vntIndexKey As Object)
		'se usa al quitar un elemento de la colección
		'vntIndexKey contiene el índice o la clave, por lo que se
		'declara como un Variant
		'Sintaxis: x.Remove(xyz)
		
		
		mCol.Remove(vntIndexKey)
	End Sub
	
	
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		'crea la colección cuando se crea la clase
		mCol = New Collection
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
	
	
	'UPGRADE_NOTE: Class_Terminate was upgraded to Class_Terminate_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Terminate_Renamed()
		'destruye la colección cuando se termina la clase
		'UPGRADE_NOTE: Object mCol may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		mCol = Nothing
	End Sub
	Protected Overrides Sub Finalize()
		Class_Terminate_Renamed()
		MyBase.Finalize()
	End Sub
	
	'% Find: se buscan los elementos asociados a una tabla temporal
	Public Function Find(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Integer, ByVal nCertif As Double, ByVal nOption_ejec As Integer) As Boolean
		Dim lrecReaTmp_undo_Move_Acc As eRemoteDB.Execute
		Dim lobjTmp_undo_Move_Acc As Tmp_undo_Move_Acc
		On Error GoTo Find_Err
		mCol = New Collection
		lrecReaTmp_undo_Move_Acc = New eRemoteDB.Execute
		
		'Definición de parámetros para stored procedure 'insudb.DelTmp_undo_Move_Acc'
		
		With lrecReaTmp_undo_Move_Acc
			.StoredProcedure = "insVI818pkg.reavi818"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nOption_ejec", nOption_ejec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run Then
				Find = True
				Do While Not .EOF
					lobjTmp_undo_Move_Acc = New Tmp_undo_Move_Acc
                    lobjTmp_undo_Move_Acc.nBranch = .FieldToClass("nBranch")
                    lobjTmp_undo_Move_Acc.nProduct = .FieldToClass("nProduct")
                    lobjTmp_undo_Move_Acc.nPolicy = .FieldToClass("nPolicy")
                    lobjTmp_undo_Move_Acc.nCertif = .FieldToClass("nCertif")
                    lobjTmp_undo_Move_Acc.dOperdate = .FieldToClass("dOperdate")
                    lobjTmp_undo_Move_Acc.nidconsec = .FieldToClass("nIdconsec")
                    lobjTmp_undo_Move_Acc.nType_move = .FieldToClass("nType_move")
                    lobjTmp_undo_Move_Acc.nCurrency = .FieldToClass("nCurrency")
                    lobjTmp_undo_Move_Acc.nOrigin = .FieldToClass("nOrigin")
                    lobjTmp_undo_Move_Acc.nCredit = .FieldToClass("nCredit")
                    lobjTmp_undo_Move_Acc.nDebit = .FieldToClass("nDebit")
                    lobjTmp_undo_Move_Acc.nInvested = .FieldToClass("nInvested")
                    lobjTmp_undo_Move_Acc.nId_reverse = .FieldToClass("nId_reverse")
                    lobjTmp_undo_Move_Acc.sSel = .FieldToClass("sSel")
                    lobjTmp_undo_Move_Acc.nCreditmanual = .FieldToClass("nCreditmanual")
                    lobjTmp_undo_Move_Acc.nDebitmanual = .FieldToClass("nDebitmanual")
                    lobjTmp_undo_Move_Acc.dOperdatemanual = .FieldToClass("dOperdatemanual")
                    lobjTmp_undo_Move_Acc.sManual = .FieldToClass("sManual")
                    lobjTmp_undo_Move_Acc.sType_move = .FieldToClass("sType_move")
                    lobjTmp_undo_Move_Acc.sCurrency = .FieldToClass("sCurrency")
                    lobjTmp_undo_Move_Acc.sOrigin = .FieldToClass("sOrigin")
                    lobjTmp_undo_Move_Acc.nTax = .FieldToClass("nTax")
                    lobjTmp_undo_Move_Acc.sReverse = .FieldToClass("sReverse")
                    lobjTmp_undo_Move_Acc.dValuedate = .FieldToClass("dValuedate")
                    lobjTmp_undo_Move_Acc.dDate_origin = .FieldToClass("dDate_origin")
                    lobjTmp_undo_Move_Acc.nReceipt = .FieldToClass("nReceipt")
                    lobjTmp_undo_Move_Acc.nType = .FieldToClass("nType")
                    lobjTmp_undo_Move_Acc.nTyp_profitworker = .FieldToClass("nTyp_profitworker")
                    lobjTmp_undo_Move_Acc.sProfitworker = .FieldToClass("sProfitworker")
                    lobjTmp_undo_Move_Acc.nOperDateType = .FieldToClass("nOperDateType")
                    lobjTmp_undo_Move_Acc.nOperDateManualType = .FieldToClass("nOperDateManualType")
                    lobjTmp_undo_Move_Acc.dLedgerDat = .FieldToClass("dLedgerdat")
                    lobjTmp_undo_Move_Acc.dLastProcess_date = .FieldToClass("dLastProcess_date")
                    lobjTmp_undo_Move_Acc.dOperdate_new = .FieldToClass("dOperdate_new")
                    Call Add(lobjTmp_undo_Move_Acc)
                    .RNext()
				Loop 
				.RCloseRec()
			Else
				Find = False
			End If
			
		End With
		
		'UPGRADE_NOTE: Object lrecReaTmp_undo_Move_Acc may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecReaTmp_undo_Move_Acc = Nothing
		
Find_Err: 
		If Err.Number Then
			Find = False
		End If
	End Function
End Class






