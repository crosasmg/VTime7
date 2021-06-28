Option Strict Off
Option Explicit On
Public Class Tar_firecat
	'%-------------------------------------------------------%'
	'% $Workfile:: Tar_firecat.cls                          $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 1:38p                                $%'
	'% $Revision:: 6                                        $%'
	'%-------------------------------------------------------%'
	
	'**-Properties according to the table in the system on Octube 05,2001.
	'*-Propiedades según la tabla en el sistema el 14/11/2001
	'Column_name                    Type        Computed   Length  Prec  Scale Nullable   TrimTrailingBlanks   FixedLenNullInSource
	'----------------------------- ----------- ---------- -------- ----- ----- --------- -------------------- ----------------------
	Public nActivityCat As Integer 'smallint    no           2       5     0     no                                  (n/a)                               (n/a)                               NULL
	Public nConstcat As Integer 'smallint    no           2           5     0     no                                  (n/a)                               (n/a)                               NULL
	Public dEffecdate As Date 'datetime    no           8                       no                                  (n/a)                               (n/a)                               NULL
	Public nRateBuild As Double 'decimal     no           5           8     5     yes                                 (n/a)                               (n/a)                               NULL
	Public nRateCont As Double 'decimal     no           5           8     5     yes                                 (n/a)                               (n/a)                               NULL
	Public nRateRC As Double 'decimal     no           5           8     5     yes                                 (n/a)                               (n/a)                               NULL
	Public dCompdate As Date 'datetime    no           8                       no                                  (n/a)                               (n/a)                               NULL
	Public nUsercode As Integer 'smallint    no           2           5     0     no                                  (n/a)                               (n/a)                               NULL
	Public dNulldate As Date 'datetime    no           8                       yes                                 (n/a)                               (n/a)                               NULL
	
	
	
	
	
	'**%ADD: This method is in charge of adding new records to the table "Tar_firecat".  It returns TRUE or FALSE
	'**%depending on whether the stored procedure executed correctly.
	'%ADD: Este método se encarga de agregar nuevos registros a la tabla "Tar_firecat". Devolviendo verdadero o
	'%falso dependiendo de si el Stored procedure se ejecutó correctamente.
	Public Function Add(ByVal nActivityCat As Integer, ByVal nConstcat As Integer, ByVal dEffecdate As Date, ByVal nRateBuild As Double, ByVal nRateCont As Double, ByVal nRateRC As Double, ByVal nUsercode As Integer) As Boolean
		
		
		Dim lreccreTar_firecat As eRemoteDB.Execute
		
		lreccreTar_firecat = New eRemoteDB.Execute
		
		On Error GoTo err_handler
		
		'+Definición de parámetros para stored procedure 'insudb.insCreUpdTar_FireCat'
		'+Información leída el 27/11/2001 09:46:51 AM
		
		
		With lreccreTar_firecat
			.StoredProcedure = "insCreUpdTar_FireCat"
			.Parameters.Add("nActivityCat", nActivityCat, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nConstCat", nConstcat, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nRateBuild", nRateBuild, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 5, 8, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nRateCont", nRateCont, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 5, 8, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nRateRC", nRateRC, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 5, 8, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				Add = True
			End If
		End With
		'UPGRADE_NOTE: Object lreccreTar_firecat may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lreccreTar_firecat = Nothing
		Exit Function
err_handler: 
		Add = False
		
		On Error GoTo 0
	End Function
	
	'**%DELETE: This method is in charge of delete records to the table "Tar_firecat".  It returns TRUE or FALSE
	'**%depending on whether the stored procedure executed correctly.
	'%DELETE: Este método se encarga de eliminar registros a la tabla "Tar_firecat". Devolviendo verdadero o
	'%falso dependiendo de si el Stored procedure se ejecutó correctamente.
	Public Function Delete(ByVal nActivityCat As Integer, ByVal nConstcat As Integer, ByVal dEffecdate As Date, ByVal nUsercode As Integer) As Boolean
		
		Dim lreccreTar_firecat As eRemoteDB.Execute
		
		lreccreTar_firecat = New eRemoteDB.Execute
		
		On Error GoTo err_handler
		
		'+Definición de parámetros para stored procedure 'insudb.insDelUpdTar_FireCat'
		'+Información leída el 27/11/2001 09:50:30 AM
		
		With lreccreTar_firecat
			.StoredProcedure = "insDelUpdTar_FireCat"
			.Parameters.Add("nActivityCat", nActivityCat, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nConstCat", nConstcat, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				Delete = True
			End If
		End With
		'UPGRADE_NOTE: Object lreccreTar_firecat may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lreccreTar_firecat = Nothing
		Exit Function
err_handler: 
		Delete = False
		
		On Error GoTo 0
	End Function
	
	'**%Find: This method returns TRUE or FALSE depending if the records exists in the table 'Tar_firecat'
	'%Find: Este metodo retorna VERDADERO o FALSO dependiendo de la existencia o no de registros en la tabla 'Tar_firecat'
	Public Function Find(ByVal nActivityCat As String, ByVal nConstcat As Integer, ByVal dEffecdate As Date) As Boolean
		Dim lrecreaTar_firecat As eRemoteDB.Execute
		
		On Error GoTo Find_Err
		lrecreaTar_firecat = New eRemoteDB.Execute
		
		'+Definición de parámetros para stored procedure 'insudb.reaTar_FireCat_exists'
		'+Información leída el 27/11/2001 09:54:25 AM
		
		With lrecreaTar_firecat
			.StoredProcedure = "reaTar_FireCat_exists"
			.Parameters.Add("nActivityCat", nActivityCat, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nConstCat", nConstcat, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Find = .Run
			If Find Then
				nActivityCat = .FieldToClass("nActivityCat")
				nConstcat = .FieldToClass("nConstCat")
				dEffecdate = .FieldToClass("dEffecdate")
				nRateBuild = .FieldToClass("nRateBuild")
				nRateCont = .FieldToClass("nRateCont")
				nRateRC = .FieldToClass("nRateRC")
				dCompdate = .FieldToClass("dCompdate")
				nUsercode = .FieldToClass("nUsercode")
				dNulldate = .FieldToClass("dNulldate")
				.RCloseRec()
			End If
		End With
		'UPGRADE_NOTE: Object lrecreaTar_firecat may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaTar_firecat = Nothing
Find_Err: 
		If Err.Number Then
			Find = False
		End If
		On Error GoTo 0
	End Function
	'Función que valida que la fecha de efecto de las tasa sea valida y existan tasas a la fecha
	Public Function insvalMIN003_K(ByVal sCodispl As String, ByVal nAction As Integer, ByVal dEffecdate As Date) As String
		Dim lerrTime As eFunctions.Errors
		Dim lvalField As eFunctions.valField
		Dim lTar_firecats As eBranches.Tar_firecats
		Dim lobjValues As eFunctions.Values
		Dim ldtmDateMax As Object
		
		lerrTime = New eFunctions.Errors
		lvalField = New eFunctions.valField
		lTar_firecats = New eBranches.Tar_firecats
		lobjValues = New eFunctions.Values
		
		On Error GoTo insVal_MIN003_K_Err
		
		
		'+Validación del campo dEffectdate.
		If dEffecdate <> dtmNull Then
			'        lvalField.Descript = lobjValues.getMessage(110)
			If lvalField.ValDate(dEffecdate,  , eFunctions.valField.eTypeValField.onlyvalid) Then
				If nAction <> 301 Then
					If Not lTar_firecats.Find(dEffecdate) Then
						Call lerrTime.ErrorMessage(sCodispl, 700026,  , eFunctions.Errors.TextAlign.RigthAling, CStr(dEffecdate))
					End If
				End If
				If nAction = 302 Or nAction = 301 Then
					If dEffecdate < Today Then
						Call lerrTime.ErrorMessage(sCodispl, 700018,  , eFunctions.Errors.TextAlign.RigthAling, CStr(dEffecdate))
					End If
					
					ldtmDateMax = InsValEffecdate()
					If ldtmDateMax >= dEffecdate Then
						Call lerrTime.ErrorMessage(sCodispl, 700018,  , eFunctions.Errors.TextAlign.RigthAling, ldtmDateMax)
					End If
				End If
			End If
		Else
            Call lerrTime.ErrorMessage(sCodispl, 700001,  , eFunctions.Errors.TextAlign.LeftAling, eFunctions.Values.GetMessage(110))
        End If
		
		insvalMIN003_K = lerrTime.Confirm
		
		
		'UPGRADE_NOTE: Object lerrTime may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lerrTime = Nothing
		'UPGRADE_NOTE: Object lvalField may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lvalField = Nothing
		'UPGRADE_NOTE: Object lTar_firecats may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lTar_firecats = Nothing
		'UPGRADE_NOTE: Object lobjValues may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lobjValues = Nothing
		
insVal_MIN003_K_Err: 
		If Err.Number Then
			insvalMIN003_K = insvalMIN003_K & Err.Description
		End If
		On Error GoTo 0
	End Function
	
	'Función que valida los campos a actualizar en la Tabla Tar_firecat
	Public Function insValMIN003Upd(ByVal sCodispl As String, ByVal nActivityCat As Integer, ByVal nConstcat As Integer, ByVal nRateBuild As Double, ByVal nRateCont As Double, ByVal nRateRC As Double, ByVal dNulldate As Date, ByVal dDateOpe As Date, Optional ByVal sAction As String = "") As String
		
		Dim lerrTime As eFunctions.Errors
		Dim lvalField As eFunctions.valField
		
		lerrTime = New eFunctions.Errors
		lvalField = New eFunctions.valField
		
		On Error GoTo insValMIN003Upd_Err
		
		'+Validación del campo de Fecha de la operación
		If lvalField.ValDate(dDateOpe,  , eFunctions.valField.eTypeValField.onlyvalid) Then
			'+Validación de la Categoria de la Actividad y la Categoria de la Construcción
			If nActivityCat = 0 Or nConstcat = 0 Then
				Call lerrTime.ErrorMessage(sCodispl, 700001)
			End If
			
			'+Validación del Registro existente
			'If sAction = "Add" Then
			'    If Find(nActivityCat, nConstCat, dDateOpe) Then
			'        Call lerrTime.ErrorMessage(sCodispl, 10004)
			'    End If
			'End If
			
			'+Valida si el registro esta activo si se esta modificando
			'If sAction = "Update" Then
			'    If dDateReg <> dtmNull Then
			'        Call lobjerrTime.ErrorMessage(sCodispl, 700018)
			'    End If
			'End If
			
			'+Validación de fecha actualización mayor a fecha de cómputo
			If dNulldate <> dtmNull Then
				Call lerrTime.ErrorMessage(sCodispl, 700018)
			End If
			
			'+Validación de existencia de registros con fecha superior a fecha de cómputo
			'       If InsValDateComp(nActivityCat, nConstCat, dDateOpe) Then
			'          Call lerrTime.ErrorMessage(sCodispl, 700018)
			'      End If
			
			
			'+Validación de que las tasas no sean las tres cero
			If nRateBuild = eRemoteDB.Constants.intNull Then
				nRateBuild = 0
			End If
			
			If nRateCont = eRemoteDB.Constants.intNull Then
				nRateCont = 0
			End If
			
			If nRateRC = eRemoteDB.Constants.intNull Then
				nRateRC = 0
			End If
			
			If nRateBuild = 0 And nRateCont = 0 And nRateRC = 0 Then
				Call lerrTime.ErrorMessage(sCodispl, 715001)
			End If
		End If
		
		insValMIN003Upd = lerrTime.Confirm
		
		'UPGRADE_NOTE: Object lerrTime may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lerrTime = Nothing
		'UPGRADE_NOTE: Object lvalField may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lvalField = Nothing
		
insValMIN003Upd_Err: 
		If Err.Number Then
			insValMIN003Upd = insValMIN003Upd & Err.Description
		End If
		On Error GoTo 0
	End Function
	
	
	'*** insPostMIN003: create/update corresponding data in the Tar_firecat table
	'*insPostMIN003: Esta función se encarga de crear/actualizar los registros
	'*correspondientes en la tabla de Tar_firecat
	Public Function insPostMIN003(ByVal sAction As String, ByVal nActivityCat As Integer, ByVal nConstcat As Integer, ByVal dEffecdate As Date, ByVal nRateBuild As Double, ByVal nRateCont As Double, ByVal nRateRC As Double, ByVal nUsercode As Integer) As Boolean
		
		On Error GoTo insPostMIN003_err
		
		
		
		insPostMIN003 = True
		'+Si el Valor de Alguna de las Tasas es Nulo se le Asigna 0
		If nRateBuild = eRemoteDB.Constants.intNull Then
			nRateBuild = 0
		End If
		
		If nRateCont = eRemoteDB.Constants.intNull Then
			nRateCont = 0
		End If
		
		If nRateRC = eRemoteDB.Constants.intNull Then
			nRateRC = 0
		End If
		
		Select Case sAction
			
			'**+ If the selected option exists
			'+Si la opción seleccionada es Registrar
			
			Case "Add"
				insPostMIN003 = Add(nActivityCat, nConstcat, dEffecdate, nRateBuild, nRateCont, nRateRC, nUsercode)
				
				'**+  If the selected option is Modify
				'+Si la opción seleccionada es Modificar
				
			Case "Update"
				insPostMIN003 = Add(nActivityCat, nConstcat, dEffecdate, nRateBuild, nRateCont, nRateRC, nUsercode)
				
				'**+ If the selected option is Delete
				'+Si la opción seleccionada es Eliminar
				
			Case "Del"
				insPostMIN003 = Delete(nActivityCat, nConstcat, dEffecdate, nUsercode)
				
		End Select
		
insPostMIN003_err: 
		If Err.Number Then
			insPostMIN003 = False
		End If
		On Error GoTo 0
		
	End Function
	Private Function InsValDateComp(ByVal nActivityCat As Integer, ByVal nConstcat As Integer, ByVal dDateOpe As Date) As Boolean
		Dim lrecreaTar_firecat As eRemoteDB.Execute
		
		On Error GoTo InsValDateComp_Err
		lrecreaTar_firecat = New eRemoteDB.Execute
		
		
		'**+Parameter definition for stored procedure 'insudb.reaTar_firecatbydate'
		'+Definición de parámetros para stored procedure 'insudb.reaTar_firecatbydate'
		
		
		With lrecreaTar_firecat
			.StoredProcedure = "reaTar_firecatbydate"
			.Parameters.Add("nActivityCat", nActivityCat, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nConstCat", nConstcat, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dDateOpe, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			InsValDateComp = .Run
		End With
		'UPGRADE_NOTE: Object lrecreaTar_firecat may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaTar_firecat = Nothing
InsValDateComp_Err: 
		If Err.Number Then
			InsValDateComp = False
		End If
		On Error GoTo 0
	End Function
	
	Private Function InsValEffecdate() As Date
		
		Dim lrecreaTar_firecat As eRemoteDB.Execute
		
		On Error GoTo InsValEffecdate_Err
		lrecreaTar_firecat = New eRemoteDB.Execute
		
		InsValEffecdate = dtmNull
		
		'+Definición de parámetros para stored procedure 'insudb.insValTar_firecat'
		'+Información leída el 27/11/2001 10:07:00 AM
		
		With lrecreaTar_firecat
			.StoredProcedure = "insValTar_firecat"
			If .Run Then
				InsValEffecdate = .FieldToClass("dEffecDate")
			End If
		End With
		
InsValEffecdate_Err: 
		If Err.Number Then
			InsValEffecdate = dtmNull
		End If
		'UPGRADE_NOTE: Object lrecreaTar_firecat may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaTar_firecat = Nothing
		On Error GoTo 0
	End Function
End Class






