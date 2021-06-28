Option Strict Off
Option Explicit On
Public Class FinanceCos
	Implements System.Collections.IEnumerable
	'%-------------------------------------------------------%'
	'% $Workfile:: FinanceCos.cls                           $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 1:25p                                $%'
	'% $Revision:: 4                                        $%'
	'%-------------------------------------------------------%'
	
	'**+ Local variable to keep the collection
	'+ Variable local para contener colección
	
	Private mCol As Collection
	
	'**- Properties to be used in the FIC006 window - Search for contrats query
	'- Se definen las propiedades a ser utilizadas enla ventana FIC006 - Consulta de búsqueda de contratos.
	
	Private pstrQueryFinance_co As String
	Private pstrQueryCliename As String
	Private mstrCondition As String
	Private mstrCliename As String
	
	'**%Add: adds a new instance of the "financeCO" class to the collection
	'%Add: Añade una nueva instancia de la clase "financeCO" a la colección
	Public Function Add(ByRef nStat_instanc As FinanceDraft.eStatusInstance, ByRef nContrat As Double, ByRef sClient As String, ByRef sCliename As String, ByRef dEffecdate As Date, ByRef nAmount As Double, ByRef nFrecuency As Integer, ByRef nAmount_d As Double, ByRef dCompdate As Date, ByRef nCurrency As Integer, ByRef dDate_print As Date, ByRef nDscto_amo As Double, ByRef nStat_contr As Integer, ByRef dFirst_draf As Date, ByRef nInitial As Double, ByRef nInitial_or As Double, ByRef nInterest As Double, ByRef dLedger_dat As Date, ByRef nNotenum As Integer, ByRef dNulldate As Date, ByRef nOffice As Integer, ByRef sOpt_commi As String, ByRef sPaymen_in As String, ByRef nQ_draft As Integer, ByRef sWait_contr As String, ByRef nUsercode As Integer, ByRef sDescript As String) As financeCO
		'**+ Creates a new object
		
		Dim objNewMember As eFinance.financeCO
		objNewMember = New eFinance.financeCO
		
		
		'**+ Set the properties passed into the method.
		
		With objNewMember
			.nAmount = nAmount
			.nFrequency = nFrecuency
			.nAmount_d = nAmount_d
			.sClient = sClient
			.sClientName = sCliename
			.dCompdate = dCompdate
			.nContrat = nContrat
			.nCurrency = nCurrency
			.dEffecdate = dEffecdate
			.dDate_print = dDate_print
			.nDscto_amo = nDscto_amo
			.nStat_contr = nStat_contr
			.dFirst_draf = dFirst_draf
			.nInitial = nInitial
			.nInitial_or = nInitial_or
			.nInterest = nInterest
			.dLedger_dat = dLedger_dat
			.nNotenum = nNotenum
			.dNulldate = dNulldate
			.nOffice = nOffice
			.sOpt_commi = sOpt_commi
			.sPayment_in = CShort(sPaymen_in)
			.nQ_draft = nQ_draft
			.sWait_contr = sWait_contr
			.nUsercode = nUsercode
		End With
		
		mCol.Add(objNewMember, "C" & nContrat & dEffecdate)
		
		'**+ Returns the object created.
		
		Add = objNewMember
		'UPGRADE_NOTE: Object objNewMember may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		objNewMember = Nothing
	End Function
	
	'***Item: Returns an element of the collection (acording to the index)
	'*Item: Devuelve un elemento de la colección (segun índice)
	Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As financeCO
		Get
			'**+ This property is used to  make a reference to an element of the collection. VntIndexKey containes the index or the key
			'**+of the collection, so it's declared as a Variant. Syntax: Set foo = x.Item(xyz) or Set foo = x.Item(5)
			'+ se usa al hacer referencia a un elemento de la colección vntIndexKey contiene el índice o la clave de la colección,
			'+ por lo que se declara como un Variant Syntax: Set foo = x.Item(xyz) or Set foo = x.Item(5)
			
			Item = mCol.Item(vntIndexKey)
		End Get
	End Property
	
	'***Count: Returns the number of elements that the collection has
	'*Count: Devuelve el número de elementos que posee la colección
	Public ReadOnly Property Count() As Integer
		Get
			'**+ It's used to get the quantity of elements of the collection. Syntax: Debug.Print x.Count.
			'+ Se usa al obtener el número de elementos de la colección. Sintaxis: Debug.Print x.Count.
			
			Count = mCol.Count()
		End Get
	End Property
	
	'***NewEnum: Enumerates the collection for use in a For Each...Next loop
	'*NewEnum: Permite enumerar la colección para utilizarla en un ciclo For Each... Next
	'UPGRADE_NOTE: NewEnum property was commented out. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="B3FC1610-34F3-43F5-86B7-16C984F0E88E"'
	'Public ReadOnly Property NewEnum() As stdole.IUnknown
		'Get
			'**+This property enumerates this collection with the syntax For...Each.
			'+ Esta propiedad permite enumerar esta colección con la sintaxis For...Each.
			'
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
		'**+This routine is used to delete an element of the collection. VntIndexKey contains the index or the key.
		'**+Syntax: x.Remove(xyz)
		'+ Se usa al quitar un elemento de la colección vntIndexKey contiene el índice o la clave, por lo que se declara como un Variant
		'+ Sintaxis: x.Remove(xyz)
		mCol.Remove(vntIndexKey)
	End Sub
	
	'**%Class_Initialize: Controls the creation of an instance of the collection
	'%Class_Initialize: Controla la creación de una instancia de la colección
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		'**+ Creates the colection when the class is created
		'+ Crea la colección cuando se crea la clase
		
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
		'**+ Destroys the collection when the class is finished
		'+ Destruye la colección cuando se termina la clase.
		
		'UPGRADE_NOTE: Object mCol may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		mCol = Nothing
	End Sub
	Protected Overrides Sub Finalize()
		Class_Terminate_Renamed()
		MyBase.Finalize()
	End Sub
	'% insConstructFinance_co: Construct the searching condition introduced in the form
	'% insConstructFinance_co: Permite construir la condición de búsqueda incluída en la ventana.
	Public Function insConstructFinance_co(ByVal sContrat As String, ByVal sClient As String, ByVal sCliename As String, ByVal sDate As String, ByVal nStat_contr As Integer) As Boolean
		Dim lclsUser As eSecurity.User
		Dim lstrCondition As String
		
		lclsUser = New eSecurity.User
		
		insConstructFinance_co = True
		
		lstrCondition = String.Empty
		pstrQueryFinance_co = String.Empty
		pstrQueryCliename = String.Empty
		
		'**+ Contract validatons
		'+ Se realizan las validaciones del campo "Contrato".
		
		'UPGRADE_WARNING: IsEmpty was upgraded to IsNothing and has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
		'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
		If Not IsDbNull(sContrat) And Not IsNothing(sContrat) And Trim(sContrat) <> String.Empty And Trim(sContrat) <> "0" Then
			If lclsUser.InsConstruct("Finance_co.nContrat", sContrat, eSecurity.User.eTypValConst.ConstNumeric, lstrCondition) Then
				pstrQueryFinance_co = Trim(pstrQueryFinance_co) & " AND " & lstrCondition
			End If
		End If
		
		'**+ Client validations
		'+ Se realizan las validaciones del campo "Cliente".
		
		'UPGRADE_WARNING: IsEmpty was upgraded to IsNothing and has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
		'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
		If Not IsDbNull(sClient) And Not IsNothing(sClient) And Trim(sClient) <> String.Empty And Trim(sClient) <> "0" Then
			If lclsUser.InsConstruct("Finance_co.sClient", sClient, eSecurity.User.eTypValConst.ConstString, lstrCondition) Then
				pstrQueryFinance_co = Trim(pstrQueryFinance_co) & " AND " & lstrCondition
			End If
		End If
		
		'**+ Effective date validations
		'+ Se realizan las validaciones del campo "Fecha de efecto".
		
		'UPGRADE_WARNING: IsEmpty was upgraded to IsNothing and has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
		'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
		If Not IsDbNull(sDate) And Not IsNothing(sDate) And Trim(sDate) <> String.Empty And Trim(sDate) <> "0" Then
			If lclsUser.InsConstruct("Finance_co.dEffecdate", sDate, eSecurity.User.eTypValConst.ConstDate, lstrCondition) Then
				pstrQueryFinance_co = Trim(pstrQueryFinance_co) & " AND " & lstrCondition
			End If
		End If
		
		'** "Status" field validations
		'+ Se realizan las validaciones del campo "Estado".
		
		If nStat_contr <> 0 And nStat_contr <> eRemoteDB.Constants.intNull Then
			If lclsUser.InsConstruct("Finance_co.nStat_contr", CStr(nStat_contr), eSecurity.User.eTypValConst.ConstNumeric, lstrCondition) Then
				pstrQueryFinance_co = Trim(pstrQueryFinance_co) & " AND " & lstrCondition
			End If
		End If
		
		'**+ "Name" field validations
		'+ Se realizan las validaciones del campo "Nombre"
		
		'UPGRADE_WARNING: IsEmpty was upgraded to IsNothing and has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
		'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
		If Not IsDbNull(sCliename) And Not IsNothing(sCliename) And Trim(sCliename) <> String.Empty And Trim(sCliename) <> "0" Then
			If lclsUser.InsConstruct("Client.sCliename", sCliename, eSecurity.User.eTypValConst.ConstString, lstrCondition) Then
				pstrQueryCliename = Trim(pstrQueryCliename) & " AND " & lstrCondition
			End If
		End If
		
		insConstructFinance_co = FindPrepareQueryFinance_co(pstrQueryFinance_co, pstrQueryCliename, True)
	End Function
	
	'**% FindPrepareQueryFinance_co: This function reads the table finance_co with the parameters included in the search condition
	'% FindPrepareQueryFinance_co: Función que permite realizar la lectura de la tabla Finance_co
	'% dependiendo de la condición incluída.
	Public Function FindPrepareQueryFinance_co(ByVal sCondition As String, ByVal sCliename As String, Optional ByVal lblnFind As Boolean = True) As Boolean
		Dim lrecReaFinance_cos As eRemoteDB.Execute
		lrecReaFinance_cos = New eRemoteDB.Execute
		
		FindPrepareQueryFinance_co = True
		
		On Error GoTo FindPrepareQueryFinance_co_Err
		
		If sCondition <> mstrCondition Or sCliename <> mstrCliename Or lblnFind Then
			
			'UPGRADE_NOTE: Object mCol may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
			mCol = Nothing
			mCol = New Collection
			
			'**+Stored procedure parameters definition 'insudb.insReaFinance_coASP'
			'**+Data read on 01/15/2001 15.29.55
			'+ Definición de parámetros para stored procedure 'insudb.insReaFinance_coASP'
			'+ Información leída el 15/01/2001 15.29.55
			
			With lrecReaFinance_cos
				.StoredProcedure = "insReaFinance_coASP"
				
				If sCliename <> String.Empty Then
					.Parameters.Add("sCliename", sCliename, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 255, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				Else
					'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
					.Parameters.Add("sCliename", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 255, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				End If
				
				If sCondition <> String.Empty Then
					.Parameters.Add("sCondition", sCondition, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 300, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				Else
					'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
					.Parameters.Add("sCondition", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 300, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				End If
				
				If .Run Then
					mstrCondition = sCondition
					mstrCliename = sCliename
					
					Do While Not .EOF
						Call Add(FinanceDraft.eStatusInstance.eftQuery, .FieldToClass("nContrat"), .FieldToClass("sClient"), .FieldToClass("sCliename"), .FieldToClass("dEffecdate"), .FieldToClass("nAmount"), .FieldToClass("nFrecuency"), .FieldToClass("nAmount_d"), .FieldToClass("dCompdate"), .FieldToClass("nCurrency"), .FieldToClass("dDate_print"), .FieldToClass("nDscto_amo"), .FieldToClass("nStat_contr"), .FieldToClass("dFirst_draf"), .FieldToClass("nInitial"), .FieldToClass("nInitial_or"), .FieldToClass("nInterest"), .FieldToClass("dLedger_dat"), .FieldToClass("nNotenum"), .FieldToClass("dNulldate"), .FieldToClass("nOffice"), .FieldToClass("sOpt_commi"), .FieldToClass("sPayment_in"), .FieldToClass("nQ_draft"), .FieldToClass("sWait_contr"), .FieldToClass("nUsercode"), .FieldToClass("sDescript"))
						
						.RNext()
					Loop 
					
					.RCloseRec()
				Else
					FindPrepareQueryFinance_co = False
					
					mstrCondition = CStr(Nothing)
					mstrCliename = CStr(Nothing)
				End If
			End With
			
			'UPGRADE_NOTE: Object lrecReaFinance_cos may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
			lrecReaFinance_cos = Nothing
		End If
		
FindPrepareQueryFinance_co_Err: 
		If Err.Number Then
			FindPrepareQueryFinance_co = False
		End If
		
		On Error GoTo 0
	End Function
End Class






