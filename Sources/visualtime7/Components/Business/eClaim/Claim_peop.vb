Option Strict Off
Option Explicit On
Public Class Claim_peop
	'%-------------------------------------------------------%'
	'% $Workfile:: Claim_peop.cls                           $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 1:34p                                $%'
	'% $Revision:: 13                                       $%'
	'%-------------------------------------------------------%'
	
	'**-Defined the principal properties of the corresponding class to the claim_peop (01/15/2001)
	'-Se definen las propiedades principales de la clase correspondientes a la tabla claim_peop (15/01/2001)
	'Column_name                     Type                                                                                                                             Computed                            Length      Prec  Scale Nullable                            TrimTrailingBlanks                  FixedLenNullInSource
	Public nClaim As Double 'int                                                                                                                              no                                  4           10    0     no                                  (n/a)                               (n/a)
	Public sClient As String 'char                                                                                                                             no                                  14                      no                                  no                                  no
	Public nCase_num As Integer 'smallint                                                                                                                         no                                  2           5     0     no                                  (n/a)                               (n/a)
	Public nDeman_type As Integer 'smallint                                                                                                                         no                                  2           5     0     no                                  (n/a)                               (n/a)
	Public nDamage_typ As Integer 'smallint                                                                                                                         no                                  2           5     0     yes                                 (n/a)                               (n/a)
	Public nNotenum As Integer 'int                                                                                                                              no                                  4           10    0     yes                                 (n/a)                               (n/a)
	Public nUsercode As Integer
	Public nId As Integer
	
	Public sCliename As String
	Public sDigit As String
	Public tDs_Text As String
	
	'%Find:
	Public Function Find(ByVal ldblClaim As Double, ByVal llngCase_num As Integer, ByVal llngDeman_type As Integer, ByVal llngId As Integer) As Boolean
		Dim lrecreaClaim_peop As eRemoteDB.Execute
		Dim larrClientInfo() As String
		
		On Error GoTo Find_Err
		
		lrecreaClaim_peop = New eRemoteDB.Execute
		
		'**parameters definition for the stored procedure 'insudb.reaClaim_peop'
		'Definición de parámetros para stored procedure 'insudb.reaClaim_peop'
		'**Data read on 01/23/2001 10.22.54
		'Información leída el 23/01/2001 10.22.54
		
		With lrecreaClaim_peop
			
			.StoredProcedure = "reaClaim_peop"
			.Parameters.Add("nClaim", ldblClaim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCase_num", llngCase_num, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDeman_type", llngDeman_type, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nId", llngId, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				Find = True
				nClaim = .FieldToClass("nClaim")
				sClient = .FieldToClass("sClient")
				'+Como campo "sCliedesc" es compuesto, se separa en nombre y digito
				larrClientInfo = Microsoft.VisualBasic.Split(.FieldToClass("sCliedesc"), "|")
				sDigit = larrClientInfo(1)
				sCliename = larrClientInfo(2)
				nCase_num = .FieldToClass("nCase_num")
				nDeman_type = .FieldToClass("nDeman_type")
				nDamage_typ = .FieldToClass("nDamage_typ")
				nNotenum = .FieldToClass("nNotenum")
				tDs_Text = .FieldToClass("tDs_Text")
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
		lrecreaClaim_peop = Nothing
	End Function
	
	'**% Update: Function that updates the table data "claim_peop"
	'%Update: Función que actualiza los datos de la tabla "claim_peop"
	Public Function Update(ByVal nClaim As Double, ByVal nCase_num As Integer, ByVal nDeman_type As Integer, ByVal sClient As String, ByVal nDamage_typ As Integer, ByVal nNotenum As Integer, ByVal nUsercode As Integer, ByVal nId As Integer) As Boolean
		Dim lrecinsUpdClaim_peop As eRemoteDB.Execute
		
		On Error GoTo Update_Err
		
		lrecinsUpdClaim_peop = New eRemoteDB.Execute
		
		'**+ Parameters definition for the stored procedure 'insudb.insUpdClaim_peop'
		'+ Definición de parámetros para stored procedure 'insudb.insUpdClaim_peop'
		'**+ Data read on 01/23/2001 10.51.40
		'+ Información leída el 23/01/2001 10.51.40
		
		With lrecinsUpdClaim_peop
			.StoredProcedure = "insUpdClaim_peop"
			.Parameters.Add("nClaim", nClaim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCase_num", nCase_num, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDeman_type", nDeman_type, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDamagesTyp", nDamage_typ, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nNotenum", nNotenum, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nId", nId, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Update = .Run(False)
		End With
		
Update_Err: 
		If Err.Number Then
			Update = False
		End If
		On Error GoTo 0
		lrecinsUpdClaim_peop = Nothing
	End Function
	
	'**%ValClientClaim_peop: The objetive of this function is to validate if a record exists into the Claim_peop table.
	'%ValClientClaim_peop: El objetivo de esta función es validar si existe un registro en la tabla Claim_peop.
	'**%If loose the client it search for ot or verify
	'%Si se le pasa el Cliente busca ese determinado cliente, sino verifica
	'**%If the table has an info related to the claim case on treatment
	'%si la tabla tiene información relacionada al caso del siniestro en tratamiento.
	Public Function ValClientClaim_peop(ByVal nClaim As Double, ByVal nCase_num As Integer, ByVal nDeman_type As Integer, ByVal nId As Integer, Optional ByVal sClient As String = "", Optional ByVal lblnFind As Boolean = False) As Boolean
		'**Defined the variable lrecClaim_peop to execute the stored procedure
		'Se define la variable lrecClaim_peop para ejecutar el store procedure
		Dim lrecClaim_peop As eRemoteDB.Execute
		
		Static lblnRead As Boolean
		Static llngOldClaim As Double
		Static lintOldCase_num As Integer
		Static lintOldDeman_type As Integer
		Static lstrOldClient As String
		
		On Error GoTo ValClientClaim_peop_Err
		
		llngOldClaim = nClaim
		lintOldCase_num = nCase_num
		lintOldDeman_type = nDeman_type
		If sClient <> String.Empty Then
			lstrOldClient = sClient
		End If
		
		lrecClaim_peop = New eRemoteDB.Execute
		
		With lrecClaim_peop
			.StoredProcedure = "valClientClaim_peop" 'Listo
			.Parameters.Add("nClaim", nClaim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCase_num", nCase_num, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDeman_type", nDeman_type, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nId", nId, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If sClient <> String.Empty Then
				.Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Else
				'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
				.Parameters.Add("sClient", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			End If
			
			If .Run Then
				If .FieldToClass("lCount") > 0 Then
					lblnRead = True
				Else
					lblnRead = False
				End If
				.RCloseRec()
			Else
				lblnRead = False
			End If
			
		End With
		
		ValClientClaim_peop = lblnRead
		
ValClientClaim_peop_Err: 
		If Err.Number Then
			lblnRead = False
			ValClientClaim_peop = False
		End If
		On Error GoTo 0
		lrecClaim_peop = Nothing
	End Function
	'** insValSI070: this function makes the frame validations
	'insValSI070: esta función realiza las validaciones del frame
	Public Function insValSI070(ByVal cboDamagesTy As Integer) As String
		'**+Declaration of the variables that will be used
		'+ Declaración de variables a utilizar
		Dim lclsErrors As eFunctions.Errors
		
		'**+Set the objects and the classes to be used
		'+ Se setean los objetos y las clases a utilizar
		lclsErrors = New eFunctions.Errors
		
		On Error GoTo insValSI070_Err
		
		'**+Validates damage type
		'+Se valida el tipo de daño.
		If cboDamagesTy = 0 Then
			Call lclsErrors.ErrorMessage("SI070", 4333)
		End If
		
		insValSI070 = lclsErrors.Confirm
		
insValSI070_Err: 
		If Err.Number Then
			insValSI070 = "insValSI070: " & Err.Description
		End If
		On Error GoTo 0
		lclsErrors = Nothing
	End Function
	
	
	'**insPostSI070. This method updates the database (as described in the functional specifications)
	'**%for the page "Claim_peop"
	'insPostSI070: Este metodo se encarga de realizar el impacto en la base de datos (descrito en las
	'%especificaciones funcionales)de la ventana "Claim_peop"
	Public Function insPostSI070(ByVal nClaim As Double, ByVal nCase_num As Integer, ByVal nDeman_type As Integer, ByVal sClient As String, ByVal nId As Integer, ByVal nDamagesTy As Integer, ByVal nNote As Integer, ByVal nUsercode As Integer) As Boolean
		'**+Dlecaration of the variables that are going to be used
		'+ Declaración de variables a utilizar
		Dim lclsCases_win As eClaim.Cases_win
		
		On Error GoTo insPostSI070_err
		
		insPostSI070 = True
		
		If Update(nClaim, nCase_num, nDeman_type, sClient, nDamagesTy, nNote, nUsercode, nId) Then
			'**+Updates the Cases_win
			'+ Se actualiza Cases_win
			lclsCases_win = New eClaim.Cases_win
			
			insPostSI070 = lclsCases_win.Add_Cases_win(nClaim, nCase_num, nDeman_type, "SI070", "2", nUsercode)
			lclsCases_win = Nothing
		Else
			insPostSI070 = False
		End If
		
insPostSI070_err: 
		If Err.Number Then
			insPostSI070 = False
		End If
		On Error GoTo 0
		lclsCases_win = Nothing
	End Function
End Class






