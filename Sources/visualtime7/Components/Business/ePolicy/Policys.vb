Option Strict Off
Option Explicit On
Public Class Policys
	Implements System.Collections.IEnumerable
	'%-------------------------------------------------------%'
	'% $Workfile:: Policys.cls                              $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 1:06p                                $%'
	'% $Revision:: 15                                       $%'
	'%-------------------------------------------------------%'
	
	'**- Auxiliary properties
	'- Propiedades Auxiliares
	
	Private mintBranch As Integer
	Private mintOffice As Integer
	
	'- local variable to hold collection
	
	Private mCol As Collection
	
	'%Add: Agrega un objeto a la colección
	Public Function Add(ByRef objClass As Policy) As Policy
		If objClass Is Nothing Then
			objClass = New Policy
		End If

        With objClass
            mCol.Add(objClass, .sCertype & .nBranch & .nProduct & .nPolicy & .nCertif & .nCurrency)
        End With
        Return objClass
    End Function
	
	'**%Find: This method fills the collection with records from the table "PolCapInc" returning TRUE or FALSE
	'**%depending on the existence of the records
	'%Find: Este metodo carga la coleccion de elementos de la tabla "PolCapInc" devolviendo Verdadero o
	'%falso, dependiendo de la existencia de los registros.
	Public Function Find_CAC002(ByVal nOffice As Integer, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nInterm As Integer, ByVal nOption As Integer) As Boolean
		Dim lrecreaPolCapInc As eRemoteDB.Execute
		Dim lclsPolicy As Policy
		
		lrecreaPolCapInc = New eRemoteDB.Execute
		
		On Error GoTo Find_CAC002_Err
		
		'**+ Parameter definiiton for stored procedure 'insudb.reaPolCapInc'
		'+ Definición de parámetros para stored procedure 'insudb.reaPolCapInc'
		'**+ Information read on April 09,2001  9:44:07
		'+ Información leída el 09/04/2001 9:44:07
		
		With lrecreaPolCapInc
			.StoredProcedure = "reaPolCapInc"
			
			.Parameters.Add("nOffice", nOffice, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nproduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nInterm", nInterm, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nOption", nOption, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run Then
				Find_CAC002 = True
				Do While Not .EOF
					lclsPolicy = New Policy
					lclsPolicy.sCertype = "2"
					lclsPolicy.nBranch = .FieldToClass("nBranch")
					lclsPolicy.nProduct = .FieldToClass("nProduct")
					lclsPolicy.nPolicy = .FieldToClass("nPolicy")
					lclsPolicy.nOffice = .FieldToClass("nOffice")
					lclsPolicy.nIntermed = .FieldToClass("nIntermed")
					lclsPolicy.sClient = .FieldToClass("sClient")
					lclsPolicy.nCapital = .FieldToClass("nCapital")
					lclsPolicy.nCertif = .FieldToClass("nCertif")
					lclsPolicy.sClient_Inter = .FieldToClass("sClient_Inter")
					lclsPolicy.nWait_code = .FieldToClass("nWait_code")
					lclsPolicy.sWait_des = .FieldToClass("sWait_des")
					lclsPolicy.sShort_des = .FieldToClass("sShort_des")
					lclsPolicy.nCurrency = .FieldToClass("nCurrency")
					lclsPolicy.sCliename = .FieldToClass("sCliename")
					lclsPolicy.sCliename_Inter = .FieldToClass("sCliename_Inter")
					lclsPolicy.sDesOfficeIns = .FieldToClass("sDescOfficeIns")
					Call Add(lclsPolicy)
					'UPGRADE_NOTE: Object lclsPolicy may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
					lclsPolicy = Nothing
					.RNext()
				Loop 
				.RCloseRec()
			Else
				Find_CAC002 = False
			End If
		End With
		
		
		'UPGRADE_NOTE: Object lrecreaPolCapInc may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaPolCapInc = Nothing
		
Find_CAC002_Err: 
		If Err.Number Then
			Find_CAC002 = False
		End If
		
		On Error GoTo 0
	End Function
	
	Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As Policy
		Get
			'**+ used when referencing an element in the collection
			'**+ vntIndexKey contains either the Index or Key to the collection,
			'**+ this is why it is declared as a Variant
			'**+ Syntax: Set foo = x.Item(xyz) or Set foo = x.Item(5)
			
			Item = mCol.Item(vntIndexKey)
		End Get
	End Property
	
	Public ReadOnly Property Count() As Integer
		Get
			
			'**+ used when retrieving the number of elements in the
			'**+ collection. Syntax: Debug.Print x.Count
			
			Count = mCol.Count()
		End Get
	End Property
	
	'UPGRADE_NOTE: NewEnum property was commented out. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="B3FC1610-34F3-43F5-86B7-16C984F0E88E"'
	'Public ReadOnly Property NewEnum() As stdole.IUnknown
		'Get
			'
			'**+ this property allows you to enumerate
			'**+ this collection with the For...Each syntax
			'
			'NewEnum = mCol._NewEnum
		'End Get
	'End Property
	
	Public Function GetEnumerator() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
		'UPGRADE_TODO: Uncomment and change the following line to return the collection enumerator. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="95F9AAD0-1319-4921-95F0-B9D3C4FF7F1C"'
		GetEnumerator = mCol.GetEnumerator
	End Function
	
	'**% ItemTable10: Select a registration of the collection for the branches code.
	'% ItemTable10: Selecciona un registro de la colección por el código
	'% Ramo.
	Public ReadOnly Property ItemTable10(ByVal vntIndexKey As Object) As eProduct.Branches
		Get
			ItemTable10 = mCol.Item("A" & vntIndexKey)
		End Get
	End Property
	
	Public Sub Remove(ByRef vntIndexKey As Object)
		
		'**+ used when removing an element from the collection
		'**+ vntIndexKey contains either the Index or Key, which is why
		'**+ it is declared as a Variant
		'**+ Syntax: x.Remove(xyz)
		
		mCol.Remove(vntIndexKey)
	End Sub
	
	'**% Class_Initialize: Initialize the class.
	'% Class_Initialize: Inicializa la clase.
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		mCol = New Collection
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
	
	'**% Class_Terminate: Finish the class.
	'% Class_Terminate: Finaliza la clase.
	'UPGRADE_NOTE: Class_Terminate was upgraded to Class_Terminate_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Terminate_Renamed()
		'UPGRADE_NOTE: Object mCol may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		mCol = Nothing
	End Sub
	Protected Overrides Sub Finalize()
		Class_Terminate_Renamed()
		MyBase.Finalize()
	End Sub
	
	'**% FindCAC003: select all the pending policies for print.
	'% FindCAC003: Permite seleccionar todas las pólizas pendientes por imprimir.
	Public Function FindCAC003(ByVal nOffice As Integer, ByVal nBranch As Integer, Optional ByVal lblnFind As Boolean = True) As Boolean
		Dim lrecReaCAC003 As eRemoteDB.Execute
		Dim lclsPolicy As ePolicy.Policy
		
		On Error GoTo FindCAC003_Err
		lrecReaCAC003 = New eRemoteDB.Execute
		
		FindCAC003 = True
		If mintOffice <> nOffice Or mintBranch <> nBranch Or lblnFind Then
			
			'**+ Parameter definition for stored procedure 'reapolicyunprinted'
			'+ Definición de parámetros para stored procedure 'reapolicyunprinted'.
			
			With lrecReaCAC003
				.StoredProcedure = "reapolicyunprinted"
				.Parameters.Add("nOffice", nOffice, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				If .Run Then
					mintOffice = nOffice
					mintBranch = nBranch
					
					Do While Not .EOF
						lclsPolicy = New Policy
						lclsPolicy.sCertype = "2"
						lclsPolicy.nBranch = .FieldToClass("nBranch")
						lclsPolicy.sDesBranch = .FieldToClass("sDesBranch")
						lclsPolicy.nProduct = .FieldToClass("nProduct")
						lclsPolicy.sDesProduct = .FieldToClass("sDesProd")
						lclsPolicy.nPolicy = .FieldToClass("nPolRec")
						lclsPolicy.nCertif = .FieldToClass("nCertif")
						lclsPolicy.nOffice = .FieldToClass("nOffice")
						lclsPolicy.sDesOffice = .FieldToClass("sDesOffice")
						lclsPolicy.sClient = .FieldToClass("sClient")
						lclsPolicy.sCliename = .FieldToClass("sCliename")
						Call Add(lclsPolicy)
						'UPGRADE_NOTE: Object lclsPolicy may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
						lclsPolicy = Nothing
						.RNext()
					Loop 
					.RCloseRec()
				Else
					FindCAC003 = False
					mintOffice = 0
					mintBranch = 0
				End If
			End With
			
		End If
		
FindCAC003_Err: 
		If Err.Number Then
			FindCAC003 = False
		End If
		'UPGRADE_NOTE: Object lrecReaCAC003 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecReaCAC003 = Nothing
		On Error GoTo 0
	End Function
	
	'**% AddTable10: Adds a new element to the collection.
	'% AddTable10: Añade un nuevo elemento a la colección.
	Public Function AddTable10(ByVal nStatusInstance As Integer, ByVal nBranch As Integer, ByVal sDescript As String, ByVal sShort_des As String, ByVal sStatregt As String, ByVal nUsercode As Integer, ByVal sTabname As String) As eProduct.Branches
		Dim objNewMember As eProduct.Branches
		
		On Error GoTo AddTable10_Err
		
		objNewMember = New eProduct.Branches
		
		If mCol Is Nothing Then
			mCol = New Collection
		End If
		
		'**+ Set the properties passed into the method
		With objNewMember
			.nStatusInstance = nStatusInstance
			.nBranch = nBranch
			.sDescript = sDescript
			.sShort_des = sShort_des
			.sStatregt = sStatregt
			.nUsercode = nUsercode
			.sTabname = sTabname
		End With
		
		mCol.Add(objNewMember, "A" & nBranch)
		
		AddTable10 = objNewMember
		'UPGRADE_NOTE: Object objNewMember may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		objNewMember = Nothing
		
AddTable10_Err: 
		If Err.Number Then
            AddTable10 = Nothing
		End If
		On Error GoTo 0
	End Function
	
	'% reaTable10: This function is in charge of showing the information of the branches table.
	'% reaTable10: Esta función se encarga mostrar la información de la tabla de
	'% ramos.
	Public Function reaTable10(Optional ByVal lblnFind As Boolean = True) As Boolean
		Dim lrecReaTable10 As eRemoteDB.Execute
		
		On Error GoTo reaTable10_Err
		
		lrecReaTable10 = New eRemoteDB.Execute
		
		reaTable10 = True
		
		If lblnFind Then
			'+ Parameter definition for stored procedure 'insudb.reaTable10'
			'+ Definición de parámetros para stored procedure 'insudb.reaTable10'.
			'UPGRADE_NOTE: Object mCol may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
			mCol = Nothing
			mCol = New Collection
			
			With lrecReaTable10
				.StoredProcedure = "reaTable10"
				If .Run Then
					Do While Not .EOF
						Call AddTable10(0, .FieldToClass("nBranch", 0), .FieldToClass("sDescript", String.Empty), .FieldToClass("sShort_des", String.Empty), .FieldToClass("sStatregt", String.Empty), .FieldToClass("nUsercode", 0), .FieldToClass("sTabname", String.Empty))
						.RNext()
					Loop 
					.RCloseRec()
				Else
					reaTable10 = False
				End If
			End With
		End If
		
reaTable10_Err: 
		If Err.Number Then
			reaTable10 = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecReaTable10 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecReaTable10 = Nothing
	End Function
	
	'**% UpdateTable10: Process each instance of the class in the collection.
	'% UpdateTable10: Realiza el tratamiento de cada instancia de la clase en la colección.
	Public Function UpdateTable10() As Boolean
		Dim lclsBranches As eProduct.Branches
		Dim lcolAux As Collection
		
		On Error GoTo UpdateTable10_Err
		
		UpdateTable10 = True
		
		lcolAux = New Collection
		
		For	Each lclsBranches In mCol
			With lclsBranches
				Select Case .nStatusInstance
					
					'**+ If the action is Add
					'+ Si la acción es Agregar
					Case 1
						UpdateTable10 = .Add()
						
						'**+ If the action is Update
						'+ Si la acción es Actualizar
					Case 2
						UpdateTable10 = .Update()
						
						'**+ If the action is Delete.
						'+ Si la acción es Eliminar
					Case 3
						UpdateTable10 = .Delete()
				End Select
				
				If .nStatusInstance <> 3 Then
					If UpdateTable10 Then
						.nStatusInstance = 0
					End If
					
					lcolAux.Add(lclsBranches)
				End If
			End With
		Next lclsBranches
		
		mCol = lcolAux
		
UpdateTable10_Err: 
		If Err.Number Then
			UpdateTable10 = False
		End If
		'UPGRADE_NOTE: Object lcolAux may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lcolAux = Nothing
		On Error GoTo 0
	End Function
	
	'**%FindBCC003: This method fills the collection with records from the table "BCC003" returning TRUE or FALSE
	'**%depending on the existence of the records
	'%FindBCC003: Este metodo carga la coleccion de elementos de la tabla "BCC003" devolviendo Verdadero o
	'%falso, dependiendo de la existencia de los registros.
	Public Function FindBCC003(ByVal sCompanyType As String, ByVal sCertype As String, ByVal sClient As String, ByVal nRole As Integer, ByVal dEffecdate As Date) As Boolean
		'**- Variable definition lrecreaPolicy and lclsPolicy
		'- Se define las variables lrecreaPolicy y lclsPolicy
		Dim lrecreaPolicy As eRemoteDB.Execute
		Dim lclsPolicy As ePolicy.Policy
		
		On Error GoTo FindBCC003_Err
		
		lrecreaPolicy = New eRemoteDB.Execute
		lclsPolicy = New ePolicy.Policy
		
		'**+ Parameter definition for stored procedure 'insudb.reaBCC003'
		'+ Definición de parámetros para stored procedure 'insudb.reaBCC003'
		
		With lrecreaPolicy
			.StoredProcedure = "reaBCC003"
			'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
			.Parameters.Add("sCompanyType", IIf(sCompanyType = CStr(Policy.CompanyType.cstrBrokerOrBrokerageFirm), "C", System.DBNull.Value), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nRole", nRole, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("deffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				FindBCC003 = True
				Do While Not .EOF
					lclsPolicy = New Policy
					
					lclsPolicy.sCertype = sCertype
					lclsPolicy.nBranch = .FieldToClass("nBranch")
					lclsPolicy.nProduct = .FieldToClass("nProduct")
					lclsPolicy.nPolicy = .FieldToClass("nPolicy")
					lclsPolicy.nCertif = .FieldToClass("nCertif")
					lclsPolicy.nRole = .FieldToClass("nRole")
					lclsPolicy.sDesOffice = .FieldToClass("sOfficeDes")
					lclsPolicy.sDesBranch = .FieldToClass("descBranch")
					lclsPolicy.sDesProduct = .FieldToClass("descProd")
					lclsPolicy.dStartdate = .FieldToClass("dStartdate")
					lclsPolicy.dExpirdat = .FieldToClass("dExpirdat")
                    lclsPolicy.sPolitype = .FieldToClass("sPolitype")
                    lclsPolicy.sCliename = .FieldToClass("sCliename")
					If sCompanyType = CStr(Policy.CompanyType.cstrBrokerOrBrokerageFirm) Then
                        lclsPolicy.sDesOfficeIns = .FieldToClass("sDescOfficeIns")
                    End If
					lclsPolicy.nCapital = .FieldToClass("nCapital")
					lclsPolicy.nPremium = .FieldToClass("nPremium")
					lclsPolicy.nNullcode = .FieldToClass("nNullcode")
					lclsPolicy.sOriginal = .FieldToClass("sOriginal")
					lclsPolicy.nMaxCurr = .FieldToClass("MaxCurr")
					lclsPolicy.nCountCur = .FieldToClass("CountCur")
					lclsPolicy.sStatus_pol = .FieldToClass("sStatus_pol")
					
					Call Add(lclsPolicy)
					'UPGRADE_NOTE: Object lclsPolicy may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
					lclsPolicy = Nothing
					.RNext()
				Loop 
			Else
				FindBCC003 = False
			End If
		End With
		
		'UPGRADE_NOTE: Object lrecreaPolicy may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaPolicy = Nothing
		'UPGRADE_NOTE: Object lclsPolicy may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsPolicy = Nothing
FindBCC003_Err: 
		If Err.Number Then
			FindBCC003 = False
		End If
		On Error GoTo 0
	End Function
End Class






