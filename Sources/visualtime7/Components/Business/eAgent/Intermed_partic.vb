Option Strict Off
Option Explicit On
Public Class Intermed_partic
	'%-------------------------------------------------------%'
	'% $Workfile:: Intermed_partic.cls                      $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 1:41p                                $%'
	'% $Revision:: 10                                       $%'
	'%-------------------------------------------------------%'
	
	'**+ Properties according to the table in the system on Dec 14, 2001.
	'+ Propiedades según la tabla en el sistema el 14/12/2001
	'**+ The key field correspond to nIntermed
	'+ El campo llave corresponde a nIntermed
	
	
	'+ Column_name           Type                 Length Prec Scale Nullable TrimTrailingBlanks FixedLenNullInSource
	'+ -------------------   -------------------- ------ ---- ----- -------- ------------------ --------------------
	Public nIntermed As Integer 'smallint 2     10    0     no       (n/a)              (n/a)
	Public nSuperin_num As Double 'smallint 2     10    0     no       (n/a)              (n/a)
	Public dSuperin_num As Date 'datetime 8                 yes      (n/a)              (n/a)
	Public nWarran_pol As Double 'smallint 2     10    0     no       (n/a)              (n/a)
	Public nUsercode As Integer 'smallint 2      5    0     no       (n/a)              (n/a)
	
	Public nStatusInstance As Integer
	Public blnNotBroker As Boolean
	
	Private lblnInquiry As Boolean
	Private lblnModify As Boolean
	
	'%Add: Crea un registro en la tabla
	Public Function Add() As Boolean
		Add = insUpdIntermed_partic(1)
	End Function
	
	'%Update: Actualiza los datos de la tabla
	Public Function Update() As Boolean
		Update = insUpdIntermed_partic(2)
	End Function
	
	'% Find: Busca la información particular de intermediarios.
	Public Function Find(ByVal nIntermed As Integer, Optional ByVal lblnFind As Boolean = False) As Boolean
		Dim lrecreaIntermed_partic As eRemoteDB.Execute
		
		On Error GoTo Find_Err
		
		If nIntermed = Me.nIntermed And Not lblnFind Then
			Find = True
		Else
			lrecreaIntermed_partic = New eRemoteDB.Execute
			With lrecreaIntermed_partic
				.StoredProcedure = "reaIntermed_partic"
				.Parameters.Add("nIntermed", nIntermed, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				If .Run(True) Then
					Me.nIntermed = .FieldToClass("nIntermed")
					Me.nSuperin_num = .FieldToClass("nSuperin_num")
					Me.dSuperin_num = .FieldToClass("dSuperin_num")
					Me.nWarran_pol = .FieldToClass("nWarran_pol")
					Me.nUsercode = .FieldToClass("nUsercode")
					.RCloseRec()
					Find = True
				Else
					Find = False
				End If
			End With
		End If
		
Find_Err: 
		If Err.Number Then
			Find = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecreaIntermed_partic may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaIntermed_partic = Nothing
	End Function
	
	'**% insUpdIntermed_partic: update the information in the main table for the transaction.
	'% insUpdIntermed_partic: Esta función se encarga de actualizar la información en tratamiento de la
	'% tabla principal para la transacción.
	Public Function insUpdIntermed_partic(ByVal nAction As Integer) As Boolean
		Dim lrecinsUpdIntermed_partic As eRemoteDB.Execute
		
		On Error GoTo insUpdIntermed_partic_Err
		
		lrecinsUpdIntermed_partic = New eRemoteDB.Execute
		
		'**+Parameter definitions for stored procedure 'insudb.insUpdIntermed_partic'
		'+Definición de parámetros para stored procedure 'insudb.insUpdIntermed_partic'
		'**+ Data of Dec 14,2001 02:44:47 p.m.
		'+Información leída el 14/12/2001 02:44:47 p.m.
		
		With lrecinsUpdIntermed_partic
			.StoredProcedure = "insUpdIntermed_partic"
			.Parameters.Add("nAction", nAction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 38, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nIntermed", nIntermed, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nSuperin_num", nSuperin_num, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dSuperin_num", dSuperin_num, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nWarran_pol", nWarran_pol, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCircular_doc", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			insUpdIntermed_partic = .Run(False)
		End With
		
insUpdIntermed_partic_Err: 
		If Err.Number Then
			insUpdIntermed_partic = False
		End If
		'UPGRADE_NOTE: Object lrecinsUpdIntermed_partic may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecinsUpdIntermed_partic = Nothing
		On Error GoTo 0
	End Function
	
	'**% Delete: Delete information in the main table of the class.
	'% Delete: Esta función se encarga de eliminar información en la tabla principal de la clase.
	Public Function Delete() As Boolean
		Delete = insUpdIntermed_partic(3)
	End Function
	
	'**% insValAG550: validate the data entered on the detail zone for the form
	'%insValAG550: Esta función se encarga de validar los datos introducidos en la zona de detalle para
	'%forma.
	Public Function insValAG550(ByVal sCodispl As String, ByVal sAction As String, Optional ByVal nSuperin_num As Double = 0, Optional ByVal dSuperin_num As Date = #12:00:00 AM#, Optional ByVal nWarran_pol As Double = 0, Optional ByVal nCircular_doc As Integer = 0) As String
		
		'- Se define la variable lclsErrors para el envío de errores de la ventana
		
		Dim lclsErrors As eFunctions.Errors
		
		On Error GoTo insValAG550_Err
		
		lclsErrors = New eFunctions.Errors
		
		'+ Validación del campo Nombramiento de superintendencia
		If nSuperin_num = eRemoteDB.Constants.intNull Or nSuperin_num = 0 Then
			Call lclsErrors.ErrorMessage(sCodispl, 55560)
		End If
		
		'+ Validación del campo Fecha de nombramiento de superintendencia
		If dSuperin_num = dtmNull Then
			Call lclsErrors.ErrorMessage(sCodispl, 55561)
		End If
		
		'+ Validación del campo Póliza de garantía
		If nWarran_pol = eRemoteDB.Constants.intNull Or nWarran_pol = 0 Then
			Call lclsErrors.ErrorMessage(sCodispl, 55562)
		End If
		
		insValAG550 = lclsErrors.Confirm
		
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
		
insValAG550_Err: 
		If Err.Number Then
			insValAG550 = insValAG550 & Err.Description
		End If
		On Error GoTo 0
	End Function
	
	'*** InsPostAG550: create/update correspondent registrations in the Intermed_partic table
	'*InsPostAG550: Esta función se encarga de crear/actualizar los registros
	'*correspondientes en la tabla de Intermed_partic
	Public Function insPostAG550(ByVal nAction As Integer, Optional ByVal nIntermed As Integer = 0, Optional ByVal nSuperin_num As Double = 0, Optional ByVal dSuperin_num As Date = #12:00:00 AM#, Optional ByVal nCircular_doc As Integer = 0, Optional ByVal nWarran_pol As Double = 0, Optional ByVal nUsercode As Integer = 0) As Boolean
		
		On Error GoTo insPostAG550_err
		
		With Me
			.nIntermed = nIntermed
			.nSuperin_num = nSuperin_num
			.dSuperin_num = dSuperin_num
			.nWarran_pol = nWarran_pol
			.nUsercode = nUsercode
		End With
		
		insPostAG550 = True
		
		Select Case nAction
			
			'**+ If the selected option exists
			'+Si la opción seleccionada es Registrar
			
			Case eFunctions.Menues.TypeActions.clngActionadd, eFunctions.Menues.TypeActions.clngActionDuplicate
				insPostAG550 = Add()
				
				'**+  If the selected option is Modify
				'+Si la opción seleccionada es Modificar
				
			Case eFunctions.Menues.TypeActions.clngActionUpdate
				insPostAG550 = Update()
				
				'**+ If the selected option is Delete
				'+Si la opción seleccionada es Eliminar
				
			Case eFunctions.Menues.TypeActions.clngActioncut
				insPostAG550 = Delete()
				
		End Select
		
insPostAG550_err: 
		If Err.Number Then
			insPostAG550 = False
		End If
		On Error GoTo 0
	End Function
	
	'* insPreAG550: Carga los valores por defecto de la trasacción AG550
	Public Function insPreAG550(ByRef pnIntermed As Integer) As Boolean
		Dim lclsIntermedia As eAgent.Intermedia
		
		On Error GoTo insPreAG550_Err
		
		lclsIntermedia = New eAgent.Intermedia
		
		If lclsIntermedia.Find(pnIntermed) Then
			If lclsIntermedia.nInterTyp = 3 Then
				blnNotBroker = False
			Else
				blnNotBroker = True
			End If
			insPreAG550 = Find(pnIntermed)
		End If
		
insPreAG550_Err: 
		If Err.Number Then
			insPreAG550 = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lclsIntermedia may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsIntermedia = Nothing
	End Function
	
	'*** Class_Initialize: controls the opening of the class
	'* Class_Initialize: se controla la apertura de la clase
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		nIntermed = eRemoteDB.Constants.intNull
		nSuperin_num = eRemoteDB.Constants.intNull
		nWarran_pol = eRemoteDB.Constants.intNull
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
End Class






