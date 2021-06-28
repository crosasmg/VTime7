Option Strict Off
Option Explicit On
Public Class Inquiry_as
	'%-------------------------------------------------------%'
	'% $Workfile:: Inquiry_as.cls                           $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 1:24p                                $%'
	'% $Revision:: 7                                        $%'
	'%-------------------------------------------------------%'
	
	'+
	'+ Estructura de tabla insudb.INQUIRY_AS al 12-20-2001 17:38:16
	'+     Property                Type         DBType   Size Scale  Prec  Null
	'+-------------------------------------------------------------------------
	Public nKeynum As Integer ' NUMBER     22   0     5    N
	Public sCodispl As String ' CHAR       8    0     0    N
	Public dCompdate As Date ' DATE       7    0     0    S
	Public nUsercode As Integer ' NUMBER     22   0     5    S
	
	'+ Variable para guardar el QueryString
	Public sDescript As String
	
	'**% Update: updates records in the inquiry_as table.
	'%Update: Esta rutina se encarga de actualizar los registros de la tabla inquiry_as.
	Public Function Update() As Boolean
		
		'**- Variable definition for the execution of the SP and the parameteres.
		'-Se define la variable para la ejecución de los SP y de los parámetros
		
		On Error GoTo Update_err
		
		Dim lrecinsUpdInquiry_as As eRemoteDB.Execute
		
		lrecinsUpdInquiry_as = New eRemoteDB.Execute
		
		'Definición de parámetros para stored procedure 'insudb.insUpdInquiry_as'
		'Información leída el 13/02/2002 11:41:48 a.m.
		
		With lrecinsUpdInquiry_as
			.StoredProcedure = "insUpdInquiry_as"
			.Parameters.Add("ActionValue", 2, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nKeynum", nKeynum, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sCodIspl", sCodispl, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 8, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Update = .Run(False)
		End With
		'UPGRADE_NOTE: Object lrecinsUpdInquiry_as may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecinsUpdInquiry_as = Nothing
		
Update_err: 
		If Err.Number Then
			Update = False
		End If
		On Error GoTo 0
		
	End Function
	
	'**% Delete: delete records in the actions table.
	'%Delete: Esta rutina se encarga de borrar el registros de la tabla actions.
	Public Function Delete() As Boolean
		
		'**- Variable definition of the execution of the SP and the parameteres.
		'-Se define la variable para la ejecución de los SP y de los parámetros
		On Error GoTo Delete_err
		
		Dim lrecinsUpdInquiry_as As eRemoteDB.Execute
		
		lrecinsUpdInquiry_as = New eRemoteDB.Execute
		
		'Definición de parámetros para stored procedure 'insudb.insUpdInquiry_as'
		'Información leída el 13/02/2002 11:41:48 a.m.
		
		With lrecinsUpdInquiry_as
			.StoredProcedure = "insUpdInquiry_as"
			.Parameters.Add("ActionValue", 3, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nKeynum", nKeynum, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sCodIspl", sCodispl, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 8, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Delete = .Run(False)
		End With
		'UPGRADE_NOTE: Object lrecinsUpdInquiry_as may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecinsUpdInquiry_as = Nothing
Delete_err: 
		If Err.Number Then
			Delete = False
		End If
		On Error GoTo 0
		
	End Function
	'**% Add: add records to the actions table.
	'%Add: Esta rutina se encarga de Añadir registro en la tabla actions.
	Public Function Add() As Boolean
		
		'**- Variable definition for the use of the SP and the parameters sent to the same.
		'-Se define la variable para el uso del SP y de los parámetros enviados al mismo
		On Error GoTo Add_err
		
		Dim lrecinsUpdInquiry_as As eRemoteDB.Execute
		
		lrecinsUpdInquiry_as = New eRemoteDB.Execute
		
		'Definición de parámetros para stored procedure 'insudb.insUpdInquiry_as'
		'Información leída el 13/02/2002 11:41:48 a.m.
		
		With lrecinsUpdInquiry_as
			.StoredProcedure = "insUpdInquiry_as"
			.Parameters.Add("ActionValue", 1, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nKeynum", nKeynum, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sCodIspl", sCodispl, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 8, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			Add = .Run(False)
		End With
		'UPGRADE_NOTE: Object lrecinsUpdInquiry_as may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecinsUpdInquiry_as = Nothing
		
Add_err: 
		If Err.Number Then
			Add = False
		End If
		On Error GoTo 0
		
	End Function
	'**% Find: validates the control in the actions table if the Inquiry_as code already exists.
	'%Find: valida contra la tabla Inquiry_as si ya el código del Inquiry_as existe
	Public Function Find(ByVal pnKeynum As Integer) As Boolean
		
		'**- Variable definition for the treatment with the SP and with the parameteres
		'-Se define la variable para el tratamiento con el SP y con los parámetros
		
		Dim lrecreaInquiry_as As eRemoteDB.Execute
		On Error GoTo Find_err
		lrecreaInquiry_as = New eRemoteDB.Execute
		
		'Definición de parámetros para stored procedure 'insudb.reaInquiry_As'
		'Información leída el 13/02/2002 11:44:17 a.m.
		
		With lrecreaInquiry_as
			.StoredProcedure = "reaInquiry_As"
			.Parameters.Add("nKeynum", pnKeynum, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				nKeynum = .FieldToClass("nKeynum")
				sCodispl = .FieldToClass("sCodispl")
				nUsercode = .FieldToClass("nUsercode")
				Find = True
				.RCloseRec()
			Else
				Find = False
			End If
		End With
		'UPGRADE_NOTE: Object lrecreaInquiry_as may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaInquiry_as = Nothing
		
Find_err: 
		If Err.Number Then
			Find = False
		End If
		On Error GoTo 0
		
	End Function
	
	'**% insValMS008_K: Validates the Inquiry_as.
	'% insValMS008_K: Valida las Inquiry_as
	Public Function insValMS008_K(ByVal ActionType As String, ByVal nKeynum As Integer, ByVal sCodispl As String, ByVal nUsercode As Integer) As String
		Dim i As Integer
		Dim lclsInquiry_as As eGeneral.Inquiry_ass
		Dim lclsErrors As eFunctions.Errors
		
		On Error GoTo insValMS008_K_err
		
		lclsInquiry_as = New eGeneral.Inquiry_ass
		lclsErrors = New eFunctions.Errors
		'+Si la accion es registrar no debe estar asociado a la misma clave en tratamiento
		'+Validaciones de codigo consulta asociada
		If Trim(ActionType) = "Add" Then
			If lclsInquiry_as.Find(nKeynum) Then
				For i = 1 To lclsInquiry_as.Count
					If lclsInquiry_as.Item(i).sCodispl = sCodispl Then
						Call lclsErrors.ErrorMessage(sCodispl, 80028)
					End If
				Next 
			End If
		End If
		insValMS008_K = lclsErrors.Confirm
		
		'UPGRADE_NOTE: Object lclsInquiry_as may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsInquiry_as = Nothing
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
		
insValMS008_K_err: 
		If Err.Number Then
			insValMS008_K = insValMS008_K & Err.Description
		End If
		On Error GoTo 0
		
	End Function
	
	'**% insPostMS008: Updates the Error Message Window.
	'% insPostMS008: Actualiza la Ventana de Mensajes de Error
	Public Function insPostMS008_K(ByVal sAction As String, ByVal nKeynum As Integer, ByVal sCodispl As String, ByVal nUsercode As Integer) As Boolean
		
		On Error GoTo insPostMS008_K_err
		
		With Me
			.nKeynum = nKeynum
			.sCodispl = sCodispl
			.nUsercode = nUsercode
		End With
		
		sAction = Trim(sAction)
		Select Case sAction
			
			'**+ If the selected option is Add
			'+Si la opción seleccionada es Registrar
			
			Case "Add"
				insPostMS008_K = Add
				
				'**+ If the selected option is Modify
				'+Si la opción seleccionada es Modificar
				
			Case "Update"
				insPostMS008_K = Update
				
				'**+ If the selected option is Delete
				'+Si la opción seleccionada es Eliminar
				
			Case "Del"
				insPostMS008_K = Delete
				
		End Select
		
insPostMS008_K_err: 
		If Err.Number Then
			insPostMS008_K = False
		End If
		On Error GoTo 0
		
	End Function
End Class






