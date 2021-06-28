Option Strict Off
Option Explicit On
Public Class Type_amend
	'%-------------------------------------------------------%'
	'% $Workfile:: Type_amend.cls                           $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 1:06p                                $%'
	'% $Revision:: 28                                       $%'
	'%-------------------------------------------------------%'
	
	'+ Propiedades según la tabla en el sistema el 11/10/2001
	'+ El campo llave corresponde a nBranch nProduct dEffecdate nType_amend.
	
	'+ Column_name        Type                 Length Prec  Scale Nullable TrimTrailingBlanks FixedLenNullInSource
	'+ ------------------ -------------------- ------ ----- ----- -------- ------------------ --------------------
	Public nBranch As Integer 'smallint 2      5     0     no       (n/a)              (n/a)
	Public nProduct As Integer 'smallint 2      5     0     no       (n/a)              (n/a)
	Public dEffecdate As Date 'datetime 8                  no       (n/a)              (n/a)
	Public dNulldate As Date 'datetime 8                  yes            (n/a)                 (n/a)
	Public nType_amend As Integer 'smallint 2      2     0     no       (n/a)              (n/a)
	Public sDescript As String 'char     30                 no        no                 yes
	Public sInd_order_serv As String 'char     1                  no        no                 yes
	Public nTypeIssue As Integer 'smallint 5      2     0     no       (n/a)              (n/a)
	Public nLevel As Integer 'smallint 5      2     0     no       (n/a)              (n/a)
	Public sRetarif As String 'varchar2 1                  no        no                 yes
	
	'% Find: Busca la información de un determinado Ramo Concepto
	Public Function Find(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal dEffecdate As Date, ByVal nType_amend As Integer, Optional ByVal lblnFind As Boolean = False) As Boolean
		Dim lrecreaType_amend As eRemoteDB.Execute
		
		On Error GoTo Find_Err
		
		If nBranch = Me.nBranch And nProduct = Me.nProduct And dEffecdate = Me.dEffecdate And nType_amend = Me.nType_amend And Not lblnFind Then
			Find = True
		Else
			lrecreaType_amend = New eRemoteDB.Execute
			
			'+ Definición de parámetros para stored procedure 'insudb.reaType_amend'
			'+ Información leída el 11/10/01 09:25:55 AM
			With lrecreaType_amend
				.StoredProcedure = "reaType_amend"
				.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nType_amend", nType_amend, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				If .Run Then
					Me.dNulldate = .FieldToClass("dNulldate")
					Me.nType_amend = .FieldToClass("nType_amend")
					Me.sDescript = .FieldToClass("sDescript")
					Me.sInd_order_serv = .FieldToClass("sInd_order_serv")
					Me.nTypeIssue = .FieldToClass("nTypeIssue")
					Me.nLevel = .FieldToClass("nLevel")
					Me.sRetarif = .FieldToClass("sRetarif")
					.RCloseRec()
					Find = True
				Else
					Find = False
				End If
			End With
			'UPGRADE_NOTE: Object lrecreaType_amend may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
			lrecreaType_amend = Nothing
		End If
		
Find_Err: 
		If Err.Number Then
			Find = False
		End If
		On Error GoTo 0
	End Function
	
	'% Add: Esta función se encarga de agregar información en la tabla principal de la clase.
	Public Function Add(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal dEffecdate As Date, ByVal nType_amend As Integer, ByVal sInd_order_serv As String, ByVal nTypeIssue As Integer, ByVal nLevel As Integer, ByVal nUsercode As Integer, ByVal sRetarif As String) As Boolean
		Dim lreccreType_amend As eRemoteDB.Execute
		
		On Error GoTo Add_err
		
		lreccreType_amend = New eRemoteDB.Execute
		
		'+ Definición de parámetros para Stored Procedure 'insudb.creType_amend'
		'+ Información leída el 11/10/01 09:30:00 AM
		With lreccreType_amend
			.StoredProcedure = "creType_amend"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nType_amend", nType_amend, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sInd_order_serv", sInd_order_serv, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTypeIssue", nTypeIssue, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nLevel", nLevel, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sRetarif", sRetarif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Add = .Run(False)
		End With
		
Add_err: 
		If Err.Number Then
			Add = False
		End If
		'UPGRADE_NOTE: Object lreccreType_amend may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lreccreType_amend = Nothing
		On Error GoTo 0
	End Function
	
	'% Update: Esta función se encarga de actualizar información en la tabla principal de la clase.
	Public Function Update(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal dEffecdate As Date, ByVal nType_amend As Integer, ByVal sInd_order_serv As String, ByVal nTypeIssue As Integer, ByVal nLevel As Integer, ByVal nUsercode As Integer, ByVal sRetarif As String) As Boolean
		Dim lrecupdType_amend As eRemoteDB.Execute
		
		On Error GoTo Update_Err
		
		lrecupdType_amend = New eRemoteDB.Execute
		
		'+ Definición de parámetros para stored procedure 'insudb.updType_amend'
		'+ Información leída el 24/09/01 03:51:44 p.m.
		With lrecupdType_amend
			.StoredProcedure = "insupdType_amend"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nType_amend", nType_amend, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sInd_order_serv", sInd_order_serv, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTypeIssue", nTypeIssue, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nLevel", nLevel, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sRetarif", sRetarif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Update = .Run(False)
		End With
		
Update_Err: 
		If Err.Number Then
			Update = False
		End If
		'UPGRADE_NOTE: Object lrecupdType_amend may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecupdType_amend = Nothing
		On Error GoTo 0
	End Function
	
	'% Function Delete: Esta función se encarga de eliminar información en la tabla principal de la clase.
	Public Function Delete(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal dEffecdate As Date, ByVal nType_amend As Integer, ByVal nUsercode As Integer) As Boolean
		Dim lrecdelType_amend As eRemoteDB.Execute
		
		On Error GoTo Delete_err
		
		lrecdelType_amend = New eRemoteDB.Execute
		
		'+ Definición de parámetros para stored procedure 'insudb.delType_amend'
		'+ Información leída el 11/10/01 09:20:02 AM
		With lrecdelType_amend
			.StoredProcedure = "delType_amend"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nType_amend", nType_amend, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Delete = .Run(False)
			
		End With
		
Delete_err: 
		If Err.Number Then
			Delete = False
		End If
		'UPGRADE_NOTE: Object lrecdelType_amend may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecdelType_amend = Nothing
		On Error GoTo 0
	End Function
	
	'% Type_amendExist_old: Esta función se encarga de eliminar información en la tabla principal de la clase.
	Public Function Type_amendExist_old(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal dEffecdate As Date) As Boolean
		Dim lobjType_amend As eRemoteDB.Execute
		
		On Error GoTo Type_amendExist_old_Err
		
		lobjType_amend = New eRemoteDB.Execute
		With lobjType_amend
			.StoredProcedure = "reaType_amend_old"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Type_amendExist_old = .Run
		End With
		
Type_amendExist_old_Err: 
		If Err.Number Then
			Type_amendExist_old = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lobjType_amend may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lobjType_amend = Nothing
	End Function
	
	'% InsValMACA632_k: Realiza la validación de los campos a actualizar en la ventana MCA632_k (Header)
	Public Function IsExist(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal dEffecdate As Date) As Boolean
		Dim lrecreaexist_type_amend As eRemoteDB.Execute
		
		On Error GoTo IsExist_Err
		
		lrecreaexist_type_amend = New eRemoteDB.Execute
		
		'+Definición de parámetros para stored procedure 'reaexist_type_amend'
		'+Información leída el 15/03/2003
		With lrecreaexist_type_amend
			.StoredProcedure = "ReaExist_Type_Amend"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nExist", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 22, 0, 1, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Run(False)
			IsExist = .Parameters("nExist").Value = 1
		End With
		
IsExist_Err: 
		If Err.Number Then
			IsExist = False
		End If
		'UPGRADE_NOTE: Object lrecreaexist_type_amend may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaexist_type_amend = Nothing
		On Error GoTo 0
	End Function
	
	'% InsValMACA632_k: Realiza la validación de los campos a actualizar en la ventana MCA632_k (Header)
	Public Function insValMCA632_k(ByVal sCodispl As String, ByVal nMainAction As Integer, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal dEffecdate As Date) As String
		Dim lobjErrors As Object
		Dim lclsType_amend As ePolicy.Type_amend
		
		On Error GoTo insValMCA632_k_Err
		
		lobjErrors = eRemoteDB.NetHelper.CreateClassInstance("eFunctions.Errors")
		lclsType_amend = New ePolicy.Type_amend
		
		'+ Validación del ramo
		With lobjErrors
			If nBranch <= 0 Then
				Call .ErrorMessage(sCodispl, 9064)
			End If
			
			'+ Validación de fecha
			If dEffecdate = eRemoteDB.Constants.dtmNull Then
				Call .ErrorMessage(sCodispl, 2056)
			End If
			
			'+ Validacion de actualizacion
			If nMainAction = eFunctions.Menues.TypeActions.clngActionUpdate Then
				If lclsType_amend Is Nothing Then
					lclsType_amend = New ePolicy.Type_amend
				End If
				lclsType_amend.nBranch = nBranch
				lclsType_amend.nProduct = nProduct
				lclsType_amend.dEffecdate = dEffecdate
				
				If lclsType_amend.Type_amendExist_old(nBranch, nProduct, dEffecdate) Then
					Call .ErrorMessage(sCodispl, 10869)
				End If
			End If
			
			insValMCA632_k = .Confirm
		End With
		
insValMCA632_k_Err: 
		If Err.Number Then
			insValMCA632_k = insValMCA632_k & Err.Description
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lobjErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lobjErrors = Nothing
		'UPGRADE_NOTE: Object lclsType_amend may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsType_amend = Nothing
	End Function
	
	'%insValMCA632: Esta función se encarga de validar los datos introducidos en la zona de detalle para
	'%forma.
	Public Function insValMCA632(ByVal sCodispl As String, ByVal sAction As String, Optional ByVal nBranch As Integer = 0, Optional ByVal nProduct As Integer = 0, Optional ByVal dEffecdate As Date = #12:00:00 AM#, Optional ByVal nType_amend As Integer = 0, Optional ByVal sOrder_serv As String = "", Optional ByVal nTypeIssue As Integer = 0, Optional ByVal nLevel As Integer = 0) As String
        '- Se define el objeto lclsType_amend, el manejo de la libreria de Endosos por Ramo Producto
        Dim lclsType_amend As ePolicy.Type_amend = New ePolicy.Type_amend

        '- Se define la variable lclserrors para el envío de errores de la ventana
        Dim lclsErrors As eFunctions.Errors
		
		On Error GoTo insValMCA632_Err
		lclsErrors = New eFunctions.Errors
		With lclsErrors
			'+ Validación del campo: Código.
			If sAction = "Add" Or sAction = "Update" Then
				If nType_amend <= 0 Then
					Call .ErrorMessage(sCodispl, 55509)
				End If
			End If
			
			'+ Si el campo Ramo fecha efecto inicio de vigencia debe estar lleno.
			If sAction = "Add" Or sAction = "Update" Then
				If nTypeIssue <= 0 Then
					Call .ErrorMessage(sCodispl, 55511)
				End If
			End If
			
			'+ Si Ramo, fecha efecto tienen valor Nivel usuario  debe estar lleno.
			If sAction = "Add" Or sAction = "Update" Then
				If nLevel <= 0 Then
					Call .ErrorMessage(sCodispl, 12008)
				End If
			End If
			
			If nBranch <> eRemoteDB.Constants.intNull And dEffecdate <> eRemoteDB.Constants.dtmNull And nType_amend <> eRemoteDB.Constants.intNull And sAction = "Add" Then
				If lclsType_amend Is Nothing Then
					lclsType_amend = New ePolicy.Type_amend
				End If
				'+ Se valida que el registro con los campos de la llave primaria no existan en la tabla '
				If lclsType_amend.Find(nBranch, nProduct, dEffecdate, nType_amend, True) Then
					Call .ErrorMessage(sCodispl, 55510)
				End If
			End If
			
			insValMCA632 = .Confirm
		End With
		
insValMCA632_Err: 
		If Err.Number Then
			insValMCA632 = lclsErrors.Confirm & Err.Description
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lclsType_amend may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsType_amend = Nothing
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
	End Function
	
	'% InsPostMCA632: Esta función se encarga de crear/actualizar los registros
	'% correspondientes en la tabla de Type_amend
	Public Function insPostMCA632(ByVal sAction As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal dEffecdate As Date, ByVal nType_amend As Integer, ByVal nUsercode As Integer, Optional ByVal sInd_order_serv As String = "", Optional ByVal nTypeIssue As Integer = 0, Optional ByVal nLevel As Integer = 0, Optional ByVal sRetarif As String = "") As Boolean
		On Error GoTo InsPostMCA632_err
		
		Me.nBranch = nBranch
		Me.nProduct = nProduct
		Me.dEffecdate = dEffecdate
		Me.nType_amend = nType_amend
		Me.sDescript = sDescript
		Me.sInd_order_serv = sInd_order_serv
		Me.nTypeIssue = nTypeIssue
		Me.nLevel = nLevel
		If sRetarif = String.Empty Then
			sRetarif = "2"
		End If
		Me.sRetarif = sRetarif
		
		insPostMCA632 = True
		
		If sAction = "Add" Or sAction = "Update" Then
			insPostMCA632 = Update(nBranch, nProduct, dEffecdate, nType_amend, sInd_order_serv, nTypeIssue, nLevel, nUsercode, sRetarif)
		Else
			insPostMCA632 = Delete(nBranch, nProduct, dEffecdate, nType_amend, nUsercode)
		End If
		
InsPostMCA632_err: 
		If Err.Number Then
			insPostMCA632 = False
		End If
		On Error GoTo 0
		
	End Function
	
	'% Class_Initialize: se controla la apertura de la clase
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		nBranch = eRemoteDB.Constants.intNull
		nProduct = eRemoteDB.Constants.intNull
		dEffecdate = eRemoteDB.Constants.dtmNull
		nType_amend = eRemoteDB.Constants.intNull
		sDescript = String.Empty
		dNulldate = eRemoteDB.Constants.dtmNull
		sInd_order_serv = String.Empty
		nTypeIssue = eRemoteDB.Constants.intNull
		nLevel = eRemoteDB.Constants.intNull
		sRetarif = String.Empty
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
End Class






