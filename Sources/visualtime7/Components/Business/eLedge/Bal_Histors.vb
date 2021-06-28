Option Strict Off
Option Explicit On
Public Class Bal_Histors
	Implements System.Collections.IEnumerable
	'%-------------------------------------------------------%'
	'% $Workfile:: Bal_Histors.cls                          $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 1:18p                                $%'
	'% $Revision:: 6                                        $%'
	'%-------------------------------------------------------%'
	
	Private mCol As Collection
	
	'**-Define the variables that are going to be used for the search
	'- Se definen las variables que se van a utilizar para la busqueda
	
	Private mintLed_Compan As Integer
	Private mstrAccount As String
	Private mstrAux As String
	Private mstrCost_cente As String
	Private mintLed_year As Integer
	
	'**% Add: add a new instance to the Bal_histor class to the collection
	'% Add: Agnade una nueva instancia de la clase Bal_histor a la coleccion
	Public Function Add(ByRef nLed_compan As Integer, ByRef sAccount As String, ByRef sAux_accoun As String, ByRef sCost_cente As String, ByRef nYear As Integer, ByRef nMonth As Integer, ByRef nBalance As Double, ByRef nCredit As Double, ByRef nDebit As Double, ByRef sPreliminar As String, ByRef sStatregt As String, ByRef nLed_year As Integer, ByRef nInd_automa As Integer) As Bal_histor
		
		'**-Define the variable that will contain the instance to add
		'- Se define la variable que contendra la instancia a agnadir
		
		Dim objNewMember As Bal_histor
		objNewMember = New Bal_histor
		
		With objNewMember
			.nLed_compan = nLed_compan
			.sAccount = sAccount
			.sAux_accoun = sAux_accoun
			.sCost_cente = sCost_cente
			.nYear = nYear
			.nMonth = nMonth
			.nBalance = nBalance
			.nCredit = nCredit
			.nDebit = nDebit
			.sPreliminar = sPreliminar
			.sStatregt = sStatregt
			.nLed_year = nLed_year
			.nInd_automa = nInd_automa
		End With
		
		mCol.Add(objNewMember)
		
		'**+Return the created object
		'+ Retorna el objeto creado
		
		Add = objNewMember
		'UPGRADE_NOTE: Object objNewMember may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		objNewMember = Nothing
	End Function
	
	'*** Item: return one element from the collection (accourding to the index)
	'* Item: Devuelve un elemento de la coleccion (segun indice)
	'-----------------------------------------------------------
	Default Public ReadOnly Property Item(ByVal lngIndexKey As Integer) As Bal_histor
		Get
			'-----------------------------------------------------------
			
			Item = mCol.Item(lngIndexKey)
		End Get
	End Property
	
	'*** Count: returm the elements number that the collection has
	'* Count: Devuelve el numero de elementos que posee la coleccion
	Public ReadOnly Property Count() As Integer
		Get
			
			Count = mCol.Count()
		End Get
	End Property
	
	'*** NewEnum: Permit to enumerate the collection to used it in one cicle For Each...Next
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
	
	'**% Find: return a objects collection type Bal_histor
	'** (read the monthly balances a an account accourding to the countable given year)
	'% Find: Devuelve una coleccion de objetos de tipo Bal_histor
	'  (Lee los balances (cierres) mensuales de una cuenta segun agno contable dado)
	Public Function Find(ByVal intLed_compan As Integer, ByVal strAccount As String, ByVal strAux As String, ByVal strCost_cente As String, ByVal intLed_year As Integer, Optional ByRef lblnFind As Boolean = False) As Boolean
		
		'**-Declare the variable that determinate the result of the function (true/False)
		'- Se declara la variable que determina el resultado de la funcion (True/False)
		Static lblnRead As Boolean
		
		'**-Define the variable lrecreaBal_historYear
		'- Se define la variable lrecreaBal_historYear
		Dim lrecreaBal_historYear As eRemoteDB.Execute
		
		lrecreaBal_historYear = New eRemoteDB.Execute
		
		On Error GoTo Find_Err
		'**+Parameters definition for the stored procedure 'insudb.reaBal_historYear'
		'**+Data read on 06/12/2001 11:27:04 AM
		'+ Definicion de parametros para stored procedure 'insudb.reaBal_historYear'
		'+ Informacion leida el 12/06/2001 11:27:04 AM
		
		With lrecreaBal_historYear
			.StoredProcedure = "reaBal_historYear"
			.Parameters.Add("nLed_compan", intLed_compan, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sAccount", strAccount, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sAux_accoun", strAux, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sCost_cente", strCost_cente, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 8, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nLed_Year", intLed_year, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run Then
				Do While Not .EOF
					Call Add(.FieldToClass("nLed_compan"), .FieldToClass("sAccount"), .FieldToClass("sAux_accoun"), .FieldToClass("sCost_cente"), .FieldToClass("nYear"), .FieldToClass("nMonth"), .FieldToClass("nBalance"), .FieldToClass("nCredit"), .FieldToClass("nDebit"), .FieldToClass("sPreliminar"), .FieldToClass("sStatregt"), .FieldToClass("nLed_Year"), .FieldToClass("nInd_automa"))
					.RNext()
				Loop 
				
				.RCloseRec()
				lblnRead = True
			Else
				lblnRead = False
			End If
		End With
		
		Find = lblnRead
		'UPGRADE_NOTE: Object lrecreaBal_historYear may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaBal_historYear = Nothing
Find_Err: 
		If Err.Number Then
			Find = False
		End If
		On Error GoTo 0
	End Function
	
	'**% Remove: delete an element from the collection
	'% Remove: Elimina un elemento de la coleccion
	'---------------------------------------------
	Public Sub Remove(ByRef vntIndexKey As Object)
		'---------------------------------------------
		
		mCol.Remove(vntIndexKey)
	End Sub
	
	'**% Class_Initialize: control the creation of an instance from the collection
	'% Class_Initialize: Controla la creacion de una instancia de la coleccion
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		
		mCol = New Collection
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
	
	'**% Class_Terminate: Control the destruction of an instance from the collection
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






