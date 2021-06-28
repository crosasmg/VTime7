Option Strict Off
Option Explicit On
Public Class Document_Pays
	Implements System.Collections.IEnumerable
	'%-------------------------------------------------------%'
	'% $Workfile:: Document_Pays.cls                        $%'
	'% $Author:: Jacob S. / Partner Consulting Ltda.        $%'
	'% $Date:: 22/05/08 15.01                               $%'
	'% $Revision:: 0                                        $%'
	'%-------------------------------------------------------%'
	
	Private mCol As Collection
	
	'% Add: Añade una nueva instancia de la clase Document_Pay a la colección
	Public Function Add(ByRef objNewDoc As Document_Pay) As Document_Pay
		
		If objNewDoc Is Nothing Then
			objNewDoc = New Document_Pay
		End If
		
		With objNewDoc
			mCol.Add(objNewDoc)
			
		End With
		
		'return the object created
		Add = objNewDoc
		'UPGRADE_NOTE: Object objNewDoc may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		objNewDoc = Nothing
	End Function
	
	'% Find: Devuelve una coleccion de objetos de tipo Document_Pay
	'------------------------------------------------------------
	Public Function Find(ByVal nTypesupport As Integer, ByVal sClient As String, ByVal nDocument As Double, ByVal nStatus As Integer, ByVal dStatus1 As Date, ByVal dStatus2 As Date, ByVal nUsercode As Integer) As Boolean
		'- Se define la variable lrecDocument_Pay que se utilizará como cursor.
		
		Dim lrecDocument_Pay As eRemoteDB.Execute
		Dim lclsDocument_Pay As Document_Pay
		
		On Error GoTo Find_Err
		
		lrecDocument_Pay = New eRemoteDB.Execute
		
		'+ Se ejecuta el store procedure que busca los documentos de pago
		
		With lrecDocument_Pay
            .StoredProcedure = "INSNC002PKG.reaDoc_Pay"
            .Parameters.Add("nTypesupport", nTypesupport, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 5, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nDocument", nDocument, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 10, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nStatus", nStatus, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 5, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dStatdate1", IIf(dStatus1 = eRemoteDB.Constants.dtmNull, System.DBNull.Value, dStatus1), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dStatdate2", IIf(dStatus2 = eRemoteDB.Constants.dtmNull, System.DBNull.Value, dStatus2), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 5, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)


			If .Run Then
				Do While Not .EOF
					lclsDocument_Pay = New Document_Pay
					
					lclsDocument_Pay.nTypesupport = .FieldToClass("nTypesupport")
					lclsDocument_Pay.sClient = .FieldToClass("sClient")
					lclsDocument_Pay.nDocument = .FieldToClass("nDocument")
					lclsDocument_Pay.nProvider = .FieldToClass("nProvider")
					lclsDocument_Pay.nAmount = .FieldToClass("nAmount")
					lclsDocument_Pay.dDocument = .FieldToClass("dDocument")
					lclsDocument_Pay.nStatus = .FieldToClass("nStatus")
					lclsDocument_Pay.sOpertype = .FieldToClass("sOpertype")
					lclsDocument_Pay.dStatdate = .FieldToClass("dStatdate")
					lclsDocument_Pay.dNulldate = .FieldToClass("dNulldate")
					lclsDocument_Pay.nUsercode = .FieldToClass("nUsercode")
					lclsDocument_Pay.dCompdate = .FieldToClass("dCompdate")
					lclsDocument_Pay.nClaim = .FieldToClass("nClaim")
					lclsDocument_Pay.nCurrency = .FieldToClass("nCurrency")
					lclsDocument_Pay.nServ_order = .FieldToClass("nServ_order")
					lclsDocument_Pay.sCliename = .FieldToClass("sCliename")
					
					Call Add(lclsDocument_Pay)
					
					'UPGRADE_NOTE: Object lclsDocument_Pay may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
					lclsDocument_Pay = Nothing
					.RNext()
				Loop 
				Find = True
			End If
		End With
		
Find_Err: 
		If Err.Number Then
			Find = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecDocument_Pay may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecDocument_Pay = Nothing
	End Function
	
	'% FindNC003: Busca la información para desplegarla en el grid
	Public Function FindNC003(ByVal sKey As String) As Boolean
		Dim lrecreaDoc_Pay As eRemoteDB.Execute
		Dim lclsDocument_Pay As Document_Pay
		
		On Error GoTo Find_Err
		
		lrecreaDoc_Pay = New eRemoteDB.Execute
		
		'+ Definición de parámetros para stored procedure 'insreanc003'
		With lrecreaDoc_Pay
            .StoredProcedure = "insnc003pkg.insreanc003"
            .Parameters.Add("sKey", sKey, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

			If .Run Then
				Do While Not .EOF
					lclsDocument_Pay = New Document_Pay
					
					lclsDocument_Pay.nAction = .FieldToClass("nAction")
					lclsDocument_Pay.sDescript = .FieldToClass("sDescript")
					lclsDocument_Pay.nClaim = .FieldToClass("nClaim")
					lclsDocument_Pay.nServ_order = .FieldToClass("nServ_order")
					lclsDocument_Pay.nProvider = .FieldToClass("nProvider")
					lclsDocument_Pay.nTypesupport = .FieldToClass("nTypesupport")
					lclsDocument_Pay.sClient = .FieldToClass("sClient")
					lclsDocument_Pay.sCliename = .FieldToClass("sCliename")
					lclsDocument_Pay.nDocument = .FieldToClass("nDocument")
					
					Call Add(lclsDocument_Pay)
					
					'UPGRADE_NOTE: Object lclsDocument_Pay may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
					lclsDocument_Pay = Nothing
					.RNext()
				Loop 
				FindNC003 = True
			End If
		End With
		
Find_Err: 
		If Err.Number Then
			FindNC003 = False
		End If
		
		'UPGRADE_NOTE: Object lrecreaDoc_Pay may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaDoc_Pay = Nothing
		
		On Error GoTo 0
	End Function
	'% Find: Devuelve una coleccion de objetos de tipo Document_Pay
	'------------------------------------------------------------
	Public Function Find_NC005(ByVal sClient As String) As Boolean
			'------------------------------------------------------------
		'- Se define la variable lrecDocument_Pay que se utilizará como cursor.
		
		Dim lrecDoc_Pay As eRemoteDB.Execute
		Dim lclsDoc_Pay As Document_Pay
		
		On Error GoTo Find_NC_Err
		
		lrecDoc_Pay = New eRemoteDB.Execute
		
		'+ Se ejecuta el store procedure que busca los documentos de pago
		
		With lrecDoc_Pay
			.StoredProcedure = "INSNC005PKG.reaMove_Acc"
            .Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

			If .Run Then
				Do While Not .EOF
					lclsDoc_Pay = New Document_Pay
					
					lclsDoc_Pay.nTypesupport = .FieldToClass("nTypesupport")
					lclsDoc_Pay.sClient = .FieldToClass("sClient")
					lclsDoc_Pay.nDocument = .FieldToClass("nDocument")
					lclsDoc_Pay.nAmount = .FieldToClass("nAmount")
					lclsDoc_Pay.nClaim = .FieldToClass("nClaim")
					lclsDoc_Pay.nCurrency = .FieldToClass("nCurrency")
					lclsDoc_Pay.nServ_order = .FieldToClass("nServ_order")
					lclsDoc_Pay.nIdconsec = .FieldToClass("nIdconsec")
					lclsDoc_Pay.nTyp_acco = .FieldToClass("nTyp_acco")
					lclsDoc_Pay.dOperdate = .FieldToClass("dOperdate")
					lclsDoc_Pay.nId = .FieldToClass("nId")
					lclsDoc_Pay.sKey = .FieldToClass("sDescript")
					lclsDoc_Pay.nCredit = .FieldToClass("nCredit")
					lclsDoc_Pay.nDebit = .FieldToClass("nDebit")
					
					Call Add(lclsDoc_Pay)
					
					'UPGRADE_NOTE: Object lclsDoc_Pay may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
					lclsDoc_Pay = Nothing
					.RNext()
				Loop 
				Find_NC005 = True
			End If
		End With
		
Find_NC_Err: 
		If Err.Number Then
			Find_NC005 = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecDoc_Pay may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecDoc_Pay = Nothing
	End Function
	
	
	
	'% Item: Devuelve un elemento de la colección (segun índice)
	Public ReadOnly Property Item(ByVal vntIndexKey As Object) As Document_Pay
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
	
	
	
	Public Function FindNC004(ByVal sKey As String) As Boolean
		Dim lrecreaDoc_Pay As eRemoteDB.Execute
		Dim lclsDocument_Pay As Document_Pay
		
		On Error GoTo Find04_Err
		
		lrecreaDoc_Pay = New eRemoteDB.Execute
		
		'+ Definición de parámetros para stored procedure 'insreanc003'
		With lrecreaDoc_Pay
            .StoredProcedure = "insnc004pkg.REA_NC004"
            .Parameters.Add("sKey", sKey, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

			If .Run Then
				Do While Not .EOF
					lclsDocument_Pay = New Document_Pay
					
					lclsDocument_Pay.sKey = .FieldToClass("SKEY")
					lclsDocument_Pay.nId = .FieldToClass("NID")
					lclsDocument_Pay.sSel = .FieldToClass("SSEL")
					lclsDocument_Pay.nClaim = .FieldToClass("NCLAIM")
					lclsDocument_Pay.nServ_order = .FieldToClass("NSERV_ORDER")
					lclsDocument_Pay.sClient = .FieldToClass("SCLIENT")
					lclsDocument_Pay.sCliename = .FieldToClass("SDESRIPT")
					lclsDocument_Pay.nDocument = .FieldToClass("NDOCUMENT")
					lclsDocument_Pay.nAmount = .FieldToClass("NAMOUNT")
					
					Call Add(lclsDocument_Pay)
					
					'UPGRADE_NOTE: Object lclsDocument_Pay may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
					lclsDocument_Pay = Nothing
					.RNext()
				Loop 
				FindNC004 = True
			End If
		End With
		
Find04_Err: 
		If Err.Number Then
			FindNC004 = False
		End If
		
		'UPGRADE_NOTE: Object lrecreaDoc_Pay may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaDoc_Pay = Nothing
		
		On Error GoTo 0
	End Function
End Class






