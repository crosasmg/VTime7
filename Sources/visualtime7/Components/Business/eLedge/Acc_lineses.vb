Option Strict Off
Option Explicit On
Public Class Acc_lineses
	Implements System.Collections.IEnumerable
	'%-------------------------------------------------------%'
	'% $Workfile:: Acc_lineses.cls                          $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 1:18p                                $%'
	'% $Revision:: 7                                        $%'
	'%-------------------------------------------------------%'
	
	Private mCol As Collection
	
	'**-Define the variables that are going to be used for the search
	'- Se definen las variables que se van a utilizar para la busqueda
	Private mintLed_Compan As Integer
	Private mlngVoucher As Integer
	
	Private mdblTotDebit As Double
	Private mdblTotCredit As Double
	Private mblnManCalc As Boolean
	Public nTot_Credits As Double
	Public nTot_Debits As Double
	
	'**% Add: add a new instance of the class Acc_transa to the collection
	'% Add: Agnade una nueva instancia de la clase Acc_transa a la coleccion
	Public Function Add(ByRef nVoucher As Integer, ByRef nLed_compan As Integer, ByRef nLine As Integer, ByRef sAccount As String, ByRef sAux_accoun As String, ByRef sClient As String, ByRef nCredit As Double, ByRef dDate_doc As Date, ByRef nDebit As Double, ByRef sDescript As String, ByRef nDoc_type As Integer, ByRef nDocNumber As Integer, ByRef nNoteNum As Integer, ByRef nOri_curr As Integer, ByRef sStatregt As String, ByRef nUsercode As Integer, ByRef sCost_cente As String, ByRef nExchange As Double, ByRef nOri_amo As Double) As Acc_lines
		
		'**-Define the variable that will contein the instance to add
		'- Se define la variable que contendra la instancia a agnadir
		Dim objNewMember As Acc_lines
		objNewMember = New Acc_lines
		
		With objNewMember
			.nVoucher = nVoucher
			.nLed_compan = nLed_compan
			.nLine = nLine
			.sAccount = sAccount
			.sAux_accoun = sAux_accoun
			.sClient = sClient
			.nCredit = nCredit
			.dDate_doc = dDate_doc
			.nDebit = nDebit
			.sDescript = sDescript
			.nDoc_type = nDoc_type
			.nDocNumber = nDocNumber
			.nNoteNum = nNoteNum
			.nOri_curr = nOri_curr
			.sStatregt = sStatregt
			.nUsercode = nUsercode
			.sCost_cente = sCost_cente
			.nExchange = nExchange
			.nOri_amo = nOri_amo
		End With
		
		mCol.Add(objNewMember)
		
		'**+Return the created object
		'+ Retorna el objeto creado
		Add = objNewMember
		'UPGRADE_NOTE: Object objNewMember may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		objNewMember = Nothing
	End Function
	
	
	'**% Find: return a objects collection type acc_lines
	'**%(Read all the voucher lines (countable establishment)
	'% Find: Devuelve una coleccion de objetos de tipo Acc_lines
	'  (Lee las todas las lineas de un comprobante (asiento))
	Public Function Find(ByVal intLed_compan As Integer, ByVal lngVoucher As Integer, Optional ByRef lblnFind As Boolean = False) As Boolean
		
		'**-Declare the variable that determinate the function result (True/False)
		'- Se declara la variable que determina el resultado de la funcion (True/False)
		Static lblnRead As Boolean
		
		'**-Define the variable lrecreaAcc_lines
		'- Se define la variable lrecreaAcc_lines
		Dim lrecreaAcc_lines As eRemoteDB.Execute
		
		On Error GoTo Find_Err
		
		'**+If the search is not make...
		'+ Si la busqueda no se ha realizado...
		If mintLed_Compan <> intLed_compan Or mlngVoucher <> lngVoucher Or lblnFind Then
			
			lrecreaAcc_lines = New eRemoteDB.Execute
			
			mintLed_Compan = intLed_compan
			mlngVoucher = lngVoucher
			
			'**+Parameters definition for the stored procedure 'insudb.reaAcc_lines'
			'**+Data read on 06/19/2001 12:32:23 PM
			'+ Definicion de parametros para stored procedure 'insudb.reaAcc_lines'
			'+ Informacion leida el 19/06/2001 12:32:23 PM
			
			With lrecreaAcc_lines
				.StoredProcedure = "reaAcc_lines"
				.Parameters.Add("nLed_compan", intLed_compan, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nVoucher", lngVoucher, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				
				If .Run Then
					mblnManCalc = False
					Do While Not .EOF
						Call Add(.FieldToClass("nVoucher"), .FieldToClass("nLed_compan"), .FieldToClass("nLine"), .FieldToClass("sAccount"), .FieldToClass("sAux_accoun"), .FieldToClass("sClient"), .FieldToClass("nCredit"), .FieldToClass("dDate_doc"), .FieldToClass("nDebit"), .FieldToClass("sDescript"), .FieldToClass("nDoc_type"), .FieldToClass("nDocNumber"), .FieldToClass("nNotenum"), .FieldToClass("nOri_curr"), .FieldToClass("sStatregt"), .FieldToClass("nUsercode"), .FieldToClass("sCost_cente"), .FieldToClass("nExchange"), .FieldToClass("nOri_amo"))
						.RNext()
					Loop 
					
					.RCloseRec()
					Find = True
				Else
					mintLed_Compan = 0
					Find = False
				End If
			End With
		Else
			Find = True
		End If
		
		
		'UPGRADE_NOTE: Object lrecreaAcc_lines may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaAcc_lines = Nothing
		
Find_Err: 
		If Err.Number Then
			Find = False
		End If
		On Error GoTo 0
	End Function
	
	'% FindTot_Credits: Devuelve una coleccion de objetos de tipo Acc_lines
	'  (Lee las todas las lineas de un comprobante (asiento))
	Public Function FindTotals(ByVal intLed_compan As Integer, ByVal lngVoucher As Integer) As Integer
		
		Dim ldblTot_Credits As Double
		Dim ldblTot_Debits As Double
		Dim lrecreaAcc_lines As eRemoteDB.Execute
		lrecreaAcc_lines = New eRemoteDB.Execute
		
		'**+Parameters definition for the stored procedure 'insudb.reaAcc_lines'
		'**+Data read on 06/19/2001 12:32:23 PM
		'+ Definicion de parametros para stored procedure 'insudb.reaAcc_lines'
		'+ Informacion leida el 19/06/2001 12:32:23 PM
		
		With lrecreaAcc_lines
			.StoredProcedure = "reaAcc_lines"
			.Parameters.Add("nLed_compan", intLed_compan, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nVoucher", lngVoucher, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run Then
				Do While Not .EOF
					ldblTot_Credits = ldblTot_Credits + .FieldToClass("nCredit")
					Me.nTot_Credits = ldblTot_Credits
					ldblTot_Debits = ldblTot_Debits + .FieldToClass("nDebit")
					Me.nTot_Debits = ldblTot_Debits
					.RNext()
				Loop 
				.RCloseRec()
			End If
		End With
		
		'UPGRADE_NOTE: Object lrecreaAcc_lines may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaAcc_lines = Nothing
		
	End Function
	
	'**% FindAll: read all the lines of a voucher from table and assign to collection Acc_lines
	'% FindAll: Devuelve una coleccion de objetos de tipo Acc_lines
	'  (Lee las todas las lineas de un comprobante (asiento))
	Public Function FindAll(ByVal intLed_compan As Integer, ByVal lngVoucher As Integer, Optional ByRef lblnFind As Boolean = False) As Boolean
		
		'**-Declare the variable that derterminate the fuction result (True/False)
		'- Se declara la variable que determina el resultado de la funcion (True/False)
		Static lblnRead As Boolean
		
		'**-Define the variable lrecreaAcc_lines
		'- Se define la variable lrecreaAcc_lines
		Dim lrecreaAcc_lines As eRemoteDB.Execute
		
		On Error GoTo FindAll_Err
		
		'**+If the search is not make...
		'+ Si la busqueda no se ha realizado...
		If mintLed_Compan <> intLed_compan Or mlngVoucher <> lngVoucher Or lblnFind Then
			
			lrecreaAcc_lines = New eRemoteDB.Execute
			
			mintLed_Compan = intLed_compan
			mlngVoucher = lngVoucher
			
			'**+Parameters definition for the stored procedure 'insudb.reaAcc_lines'
			'**+Data read on 06/26/2001 10:32:23 AM
			'+ Definicion de parametros para stored procedure 'insudb.reaAcc_lines'
			'+ Informacion leida el 26/06/2001 10:32:23 AM
			
			With lrecreaAcc_lines
				.StoredProcedure = "reaAcc_linesAll"
				.Parameters.Add("nLed_compan", intLed_compan, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nVoucher", lngVoucher, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				
				If .Run Then
					mblnManCalc = False
					Do While Not .EOF
						Call Add(.FieldToClass("nVoucher"), .FieldToClass("nLed_compan"), .FieldToClass("nLine"), .FieldToClass("sAccount"), .FieldToClass("sAux_accoun"), .FieldToClass("sClient"), .FieldToClass("nCredit"), .FieldToClass("dDate_doc"), .FieldToClass("nDebit"), .FieldToClass("sDescript"), .FieldToClass("nDoc_type"), .FieldToClass("nDocNumber"), .FieldToClass("nNotenum"), .FieldToClass("nOri_curr"), .FieldToClass("sStatregt"), .FieldToClass("nUsercode"), .FieldToClass("sCost_cente"), .FieldToClass("nExchange"), .FieldToClass("nOri_amo"))
						.RNext()
					Loop 
					
					.RCloseRec()
					FindAll = True
				Else
					mintLed_Compan = 0
					FindAll = False
				End If
			End With
		Else
			FindAll = True
		End If
		
		
		'UPGRADE_NOTE: Object lrecreaAcc_lines may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaAcc_lines = Nothing
		
FindAll_Err: 
		If Err.Number Then
			FindAll = False
		End If
		On Error GoTo 0
	End Function
	
	'**% insCalTotAmo: This function is in charge of calculate the debits and credits totals.
	'%insCalTotAmo. Esta funcion se encarga de calcular los totales de debitos y creditos
	Private Sub insCalTotAmo()
		Dim lobjAcc_Lines As Acc_lines
		
		mdblTotDebit = 0
		mdblTotCredit = 0
		mblnManCalc = True
		
		For	Each lobjAcc_Lines In mCol
			mdblTotDebit = mdblTotDebit + IIf(lobjAcc_Lines.nDebit <> eRemoteDB.Constants.intNull, lobjAcc_Lines.nDebit, 0)
			mdblTotCredit = mdblTotCredit + IIf(lobjAcc_Lines.nCredit <> eRemoteDB.Constants.intNull, lobjAcc_Lines.nCredit, 0)
		Next lobjAcc_Lines
	End Sub
	
	'**% Credits. This property restores the credits total of the records contained in the collection.
	'%Credits: Esta propiedad devuelve el total de los créditos de los registros contenidos en la
	'%coleccion
	Public ReadOnly Property Credits() As Double
		Get
			If Not mblnManCalc Then
				Call insCalTotAmo()
			End If
			Credits = mdblTotCredit
		End Get
	End Property
	
	'**% Debits. This property restores the debits total of the records contained in the collection.
	'%Debits: Esta propiedad devuelve el total de los débitos de los registros contenidos en la
	'%coleccion
	Public ReadOnly Property Debits() As Double
		Get
			If Not mblnManCalc Then
				Call insCalTotAmo()
			End If
			Debits = mdblTotDebit
		End Get
	End Property
	
	
	'***Property Get Count: return the elements number that the collection has
	'* Property Get Count: Devuelve el numero de elementos que posee la coleccion
	Public ReadOnly Property Count() As Integer
		Get
			
			Count = mCol.Count()
		End Get
	End Property
	'***Item: takes an alement from the collection
	'* Item: Toma un elemento de la coleccion
	'+ Used when referencing an element in the collection.
	'+ vntIndexKey contains either the Index or Key to the collection,
	'+ this is why it is declared as a Variant
	'+ Syntax: Set foo = x.Item(xyz) or Set foo = x.Item(5)
	Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As Acc_lines
		Get
			
			Item = mCol.Item(vntIndexKey)
		End Get
	End Property
	
	'***NewEnum: Permit to enumerate the collection to used it in a cicle For Each...Next
	'* NewEnum: Permite enumerar la coleccion para utilizarla en un ciclo For Each... Next
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
	
	'**% Remove: delete one element of the collection
	'% Remove: Elimina un elemento de la coleccion
	'---------------------------------------------
	Public Sub Remove(ByRef vntIndexKey As Object)
		'---------------------------------------------
		
		mCol.Remove(vntIndexKey)
	End Sub
	
	'**% Class_Initialize: Control the creation of one instance from the collection
	'% Class_Initialize: Controla la creacion de una instancia de la coleccion
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		
		mCol = New Collection
		mblnManCalc = False
		mdblTotDebit = 0
		mdblTotCredit = 0
		
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
	
	'**% Class_Terminate: Control the destruction of one instance from the collection
	'% Class_Terminate: Controla la destruccion de una instancia de la coleccion
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






