Option Strict Off
Option Explicit On
Public Class Client_req
	'%-------------------------------------------------------%'
	'% $Workfile:: Client_req.cls                           $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 12:35p                               $%'
	'% $Revision:: 11                                       $%'
	'%-------------------------------------------------------%'
	
	'**-Properties according to the table in the system on March 30, 2001.
	'**-The key fields of the table corresponds to: nBranch, nProduct, nRole and dEffecdate.
	'-Propiedades según la tabla en el sistema al 30/03/2001.
	'-Los campos llave de la tabla corresponden a: nBranch, nProduct, nRole y dEffecdate.
	
	'   Column_name                    Type      Computed  Length      Prec  Scale Nullable TrimTrailingBlanks FixedLenNullInSource
	Public nBranch As Integer 'smallint    no        2           5     0     no          (n/a)               (n/a)
	Public nProduct As Integer 'smallint    no        2           5     0     no          (n/a)               (n/a)
	Public nRole As Integer 'smallint    no        2           5     0     no          (n/a)               (n/a)
	Public dEffecdate As Date 'datetime    no        8                       no          (n/a)               (n/a)
	Public dNulldate As Date 'datetime    no        8                       yes         (n/a)               (n/a)
	Public nusercode As Integer 'smallint    no        2           5     0     yes         (n/a)               (n/a)
	Public nTratypep As Integer 'smallint    no        2           5     0     no          (n/a)               (n/a)
	Public nField As Integer 'smallint    no        2           5     0     no          (n/a)               (n/a)
	Public sRequired As String 'smallint    no        1           0     0     yes         (n/a)               (n/a)
	
	
	Public sBirthdai As String 'char        no        1                       yes         no                  yes
	Public sCivistai As String 'char        no        1                       yes         no                  yes
	Public sOccupati As String 'char        no        1                       yes         no                  yes
	Public sSexinsui As String 'char        no        1                       yes         no                  yes
	Public sTax_situa As String 'char        no        1                       yes         no                  yes
	Public sAddress As String 'char        no        1                       yes         no                  yes
	Public sCreditLine As String 'char        no        1                       yes         no                  yes
	
	'**-Auxiliary variables.
	'-Variables auxiliares
	
	Public nClialloproRole As Integer
	Public nFieldabe As Integer
	Public sDescript As String
	
	
	'**%ADD: This method is in charge of adding new records to the table "client_req".  It returns TRUE or FALSE
	'**%depending on whether the stored procedure executed correctly.
	'%ADD: Este método se encarga de agregar nuevos registros a la tabla "client_req". Devolviendo verdadero o
	'%falso dependiendo de si el Stored procedure se ejecutó correctamente.
	Public Function Add() As Boolean
		
		'**-Variable definition lreccreClient_req
		'-Se define la variable lreccreCLient_req
		
		Dim lreccreCLient_req As eRemoteDB.Execute
		
		On Error GoTo Add_err
		
		lreccreCLient_req = New eRemoteDB.Execute
		
		'**+Parameter definition for stored procedure 'insudb.creCLient_req'
		'**+Information required on March 28,2001  03:18:25 p.m.
		'+Definición de parámetros para stored procedure 'insudb.creCLient_req'
		'+Información leída el 28/03/2001 03:18:25 p.m.
		
		With lreccreCLient_req
			.StoredProcedure = "creCLient_req"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nRole", nRole, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTratypeP", nTratypep, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nField", nField, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sRequired", sRequired, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nusercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dProductDate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			Add = .Run(False)
		End With
		'UPGRADE_NOTE: Object lreccreCLient_req may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lreccreCLient_req = Nothing
		
Add_err: 
		If Err.Number Then
			Add = False
		End If
		On Error GoTo 0
	End Function
	
	'**%Update: This method is in charge of updating records in the table "Client_req".  It returns TRUE or FALSE
	'**%depending on whether the stored procedure executed correctly.
	'%Update: Este método se encarga de actualizar registros en la tabla "Client_req". Devolviendo verdadero o
	'%falso dependiendo de si el Stored procedure se ejecutó correctamente.
	Public Function Update() As Boolean
		
		'**-Variable definition lrecupdClient_req
		'-Se define la variable lrecupdCLient_req
		
		Dim lrecupdCLient_req As eRemoteDB.Execute
		
		On Error GoTo Update_Err
		
		lrecupdCLient_req = New eRemoteDB.Execute
		
		'**+Parameter definition for stored procedure 'insudb.updClient_req'
		'**+Information read on March 28,2001  02:04:20 p.m.
		'+Definición de parámetros para stored procedure 'insudb.updCLient_req'
		'+Información leída el 28/03/2001 02:04:20 p.m.
		Update = True
		With lrecupdCLient_req
			.StoredProcedure = "updCLient_req"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nRole", nRole, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTratypeP", nTratypep, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nField", nField, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sRequired", sRequired, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dProductDate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nusercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			Update = .Run(False)
		End With
		'UPGRADE_NOTE: Object lrecupdCLient_req may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecupdCLient_req = Nothing
		
Update_Err: 
		If Err.Number Then
			Update = False
		End If
		On Error GoTo 0
	End Function
	
	'**%insValDP004: This method validates the page "DP004" as described in the functional specifications
	'%InsValDP004: Este metodo se encarga de realizar las validaciones descritas en el funcional
	'%de la ventana "DP004"
	Public Function insValDP004(ByVal sCodispl As String, Optional ByVal nBranch As Integer = 0, Optional ByVal nProduct As Integer = 0, Optional ByVal dEffecdate As Date = #12:00:00 AM#) As String
		
		Dim lclsErrors As eFunctions.Errors
		Dim lcolCliallopros As eProduct.Cliallopros
		
		On Error GoTo insValDP004_Err
		
		lclsErrors = New eFunctions.Errors
		lcolCliallopros = New eProduct.Cliallopros
		
		If Not lcolCliallopros.Find_O(nBranch, nProduct) Then
			Call lclsErrors.ErrorMessage(sCodispl, 11348)
		End If
		
		insValDP004 = lclsErrors.Confirm
		
		'UPGRADE_NOTE: Object lcolCliallopros may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lcolCliallopros = Nothing
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
		
insValDP004_Err: 
		If Err.Number Then
			insValDP004 = insValDP004 & Err.Description
		End If
		On Error GoTo 0
	End Function
	
	'**%insPostDP004: This method updates the database (as described in the functional specifications)
	'**%for the page "DP004"
	'%insPostDP004: Este metodo se encarga de realizar el impacto en la base de datos (descrito en las
	'%especificaciones funcionales)de la ventana "DP004"
	Public Function insPostDP004(ByVal nAction As Integer, Optional ByVal nBranch As Integer = 0, Optional ByVal nProduct As Integer = 0, Optional ByVal nRole As Integer = 0, Optional ByVal nTratypep As Integer = 0, Optional ByVal nField As Integer = 0, Optional ByVal sRequired As String = "", Optional ByVal nusercode As Integer = 0, Optional ByVal dEffecdate As Date = #12:00:00 AM#) As Boolean
		'**+This assignment is used for using the incoming information in all
		'**+the functions called inside of insPostDP004, without having to pass it as a parameter.
		'+Esta asignación es para utilizar la información entrante en todas
		'+las funciones llamadas dentro de insPostDP004, sin tener que pasarla como parámetro
		
		On Error GoTo insPostDP004_err
		Me.nBranch = nBranch
		Me.nProduct = nProduct
		Me.nRole = nRole
		Me.nTratypep = nTratypep
		Me.nField = nField
		Me.sRequired = IIf(sRequired = String.Empty, 2, sRequired)
		Me.nusercode = nusercode
		Me.dEffecdate = dEffecdate
		
		
		insPostDP004 = True
		'**+If the selected option is register or modify
		'+Si la opción seleccionada es registrar o modificar.
		If nAction = eFunctions.Menues.TypeActions.clngActionadd Or nAction = eFunctions.Menues.TypeActions.clngActionUpdate Then
			insPostDP004 = insUpdClient_req()
		End If
		
insPostDP004_err: 
		If Err.Number Then
			insPostDP004 = False
		End If
		On Error GoTo 0
	End Function
	
	'**%insUpdClient_req: Updates in the data base the information of the
	'**%required fields during the emission of the product in process
	'%insUpdClient_req: Actualiza en la Base de Datos la información de los
	'%campos requeridos en la emisión del producto en tratamiento
	Private Function insUpdClient_req() As Boolean
		Dim lclsClient_req As eProduct.Client_req
		
		On Error GoTo insUpdClient_req_Err
		
		lclsClient_req = New eProduct.Client_req
		With lclsClient_req
			.nBranch = Me.nBranch
			.nProduct = Me.nProduct
			.nRole = Me.nRole
			.nTratypep = Me.nTratypep
			.nField = Me.nField
			.sRequired = Me.sRequired
			.nusercode = Me.nusercode
			.dEffecdate = Me.dEffecdate
			insUpdClient_req = .Update
		End With
		
insUpdClient_req_Err: 
		If Err.Number Then
			insUpdClient_req = False
		End If
		'UPGRADE_NOTE: Object lclsClient_req may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsClient_req = Nothing
		On Error GoTo 0
	End Function
	
	
	'%Find_Role: lee campos requeridos en la emisión del client
	Public Function Find_Role(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nRole As Integer, Optional ByVal nTratypep As Integer = 0, Optional ByVal nField As Integer = 0) As Object
		Dim lrecreaClient_req As eRemoteDB.Execute
		Dim lclsreaClient_req As Client_req
		
		On Error GoTo reaClient_req_Err
		
		lrecreaClient_req = New eRemoteDB.Execute
		
		'+
		'+ Definición de store procedure reaClient_req al 02-26-2002 10:24:38
		'+
		With lrecreaClient_req
			.StoredProcedure = "reaClient_req"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nRole", nRole, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run(True) Then
				Find_Role = True
				Me.nBranch = .FieldToClass("nBranch")
				Me.nProduct = .FieldToClass("nProduct")
				Me.nRole = .FieldToClass("nRole")
				Me.dEffecdate = .FieldToClass("dEffecdate")
				Me.sBirthdai = .FieldToClass("sBirthdai")
				Me.sCivistai = .FieldToClass("sCivistai")
				Me.dNulldate = .FieldToClass("dNulldate")
				Me.sOccupati = .FieldToClass("sOccupati")
				Me.sSexinsui = .FieldToClass("sSexinsui")
				Me.nusercode = .FieldToClass("nUsercode")
				Me.sTax_situa = .FieldToClass("sTax_situa")
				Me.sAddress = .FieldToClass("sAddress")
				Me.sCreditLine = .FieldToClass("sCreditline")
				Me.nTratypep = .FieldToClass("nTratypeP")
				Me.nField = .FieldToClass("nField")
				Me.sRequired = .FieldToClass("sRequired")
			Else
				Find_Role = False
			End If
		End With
		
reaClient_req_Err: 
		If Err.Number Then
			Find_Role = False
		End If
		'UPGRADE_NOTE: Object lrecreaClient_req may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaClient_req = Nothing
		On Error GoTo 0
	End Function
	
	'%insValClient_Req: Este metodo se encarga de realizar las validaciones en la ventana
	'%de clientes
	Public Function insValClient_Req(ByVal sCodispl As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nRole As Integer, Optional ByVal nTransaction As Integer = 0, Optional ByVal dEffecdate As Date = #12:00:00 AM#) As String
		
		Dim lclsErrors As eFunctions.Errors
		Dim lcolClient_reqs As eProduct.Client_reqs
		Dim lclsClient_req As eProduct.Client_req
		Dim nTratypep As Integer
		
		On Error GoTo insValClient_Req_Err
		
		lclsErrors = New eFunctions.Errors
		lcolClient_reqs = New eProduct.Client_reqs
		
		If lcolClient_reqs.Find(nBranch, nProduct, nRole, nTratypep, dEffecdate) Then
			
			Select Case nTransaction
				Case CDec("1"), CDec("2"), CDec("3"), CDec("18"), CDec("19")
					nTratypep = CInt("1")
				Case CDec("12"), CDec("13"), CDec("14"), CDec("15")
					nTratypep = CInt("2")
				Case CDec("8"), CDec("9"), CDec("10"), CDec("11"), CDec("44")
					nTratypep = CInt("3")
				Case CDec("21")
					nTratypep = CInt("4")
				Case CDec("4"), CDec("5"), CDec("24"), CDec("25"), CDec("39"), CDec("28"), CDec("29"), CDec("41")
					nTratypep = CInt("6")
				Case CDec("6"), CDec("7"), CDec("26"), CDec("27"), CDec("40"), CDec("30"), CDec("31"), CDec("42"), CDec("34"), CDec("43"), CDec("23")
					nTratypep = CInt("7")
			End Select
			
			For	Each lclsClient_req In lcolClient_reqs
				If lclsClient_req.sRequired = "1" Then
					Call lclsErrors.ErrorMessage(sCodispl, 55811,  , eFunctions.Errors.TextAlign.LeftAling, lclsClient_req.sDescript)
				End If
			Next lclsClient_req
		End If
		
		lclsErrors.bError = True
		insValClient_Req = lclsErrors.Confirm
		
		'UPGRADE_NOTE: Object lclsClient_req may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsClient_req = Nothing
		'UPGRADE_NOTE: Object lcolClient_reqs may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lcolClient_reqs = Nothing
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
		
insValClient_Req_Err: 
		If Err.Number Then
			insValClient_Req = insValClient_Req & Err.Description
		End If
		On Error GoTo 0
	End Function
End Class






