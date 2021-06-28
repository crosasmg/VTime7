Option Strict Off
Option Explicit On
Public Class Tab_Commission
	'%-------------------------------------------------------%'
	'% $Workfile:: Tab_Commission.cls                       $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 1:41p                                $%'
	'% $Revision:: 15                                       $%'
	'%-------------------------------------------------------%'
	
	'**+ Properties according to tha table in the system on May 30, 2001
	'+ Propiedades según la tabla en el sistema el 30/05/2001
	'**+ The key field corresponds to nComyabli
	'+ El campo llave corresponde a nComtabli.
	
	'+ Column_name         Type                 Length Prec  Scale Nullable TrimTrailingBlanks FixedLenNullInSource
	'+ ------------------- -------------------- ------ ----- ----- -------- ------------------ --------------------
	Public nTable_cod As Integer 'smallint 2      5     0     no       (n/a)              (n/a)
	Public sType_assig As String 'char     1                  yes      no                 yes
	Public nUsercode As Integer 'smallint 2      5     0     yes      (n/a)              (n/a)
	Public sDescript As String 'char     30                 yes      no                 yes
	Public sShort_des As String 'char     12                 yes      no                 yes
	Public sStatregt As String 'char     1                  yes      no                 yes
	
	Public nCommType As Integer
	Public nStatusInstance As Integer
	
	'**- Define the enumerate for the function that validates the chosen table
	'- Se define el enumerado para la fucnión que valida la tabla escogida
	Public Enum eActions
		Cre
		Del
		Rea
		Upd
		Val
	End Enum
	
	'**% Find. validate that the type of table does not exists in the table of types.
	'%Find. Esta funcion se encarga de validar que el tipo de tabla no exista en la
	'%tabla de tipos.
	Public Function Find(ByVal nTable_cod As Integer, Optional ByVal lblnFind As Boolean = False) As Boolean
		'**- Variable definition lrecTabCommission for the execution of the StoredProcedure
		'- Se define la variable lrecTabCommission para la ejecución del StoredProcedure
		
		Dim lrecTabCommission As eRemoteDB.Execute
		
		lrecTabCommission = New eRemoteDB.Execute
		
		If nTable_cod = Me.nTable_cod And Not lblnFind Then
			Find = True
		Else
			
			With lrecTabCommission
				.StoredProcedure = ValCommType(eActions.Val)
				.Parameters.Add("nTable", nTable_cod, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				
				If .Run Then
					If nCommType = 0 Then
						Me.nTable_cod = .FieldToClass("nComtabge")
					Else
						If nCommType = 1 Then
							Me.nTable_cod = .FieldToClass("nComtabli")
						Else
							If nCommType = 2 Then
								Me.nTable_cod = .FieldToClass("nEco_sche")
							Else
								If nCommType = 3 Then
									Me.nTable_cod = .FieldToClass("nTable_cod")
									Me.sType_assig = .FieldToClass("sType_assig")
								Else
									Me.nTable_cod = .FieldToClass("nExist")
								End If
							End If
						End If
					End If
					
					If nCommType <> 4 Then
						Me.sDescript = .FieldToClass("sDescript")
						Me.sShort_des = .FieldToClass("sShort_des")
						Me.sStatregt = .FieldToClass("sStatregt")
						Find = True
					Else
						nTable_cod = .FieldToClass("NEXIST")
						If nTable_cod = 1 Then
							Find = True
						Else
							Find = False
						End If
					End If
					.RCloseRec()
				Else
					Find = False
				End If
			End With
		End If
		
		'UPGRADE_NOTE: Object lrecTabCommission may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecTabCommission = Nothing
		
	End Function
	
	'**%ADD: add new records to the table "Tab_Commission".  It returns TRUE or FALSE
	'**%depending on whether the stored procedure executed correctly.
	'%ADD: Este método se encarga de agregar nuevos registros a la tabla "Tab_Commission". Devolviendo verdadero o
	'%falso dependiendo de si el Stored procedure se ejecutó correctamente.
	Public Function Add() As Boolean
		
		Dim lreccreTab_excomm As eRemoteDB.Execute
		
		lreccreTab_excomm = New eRemoteDB.Execute
		
		'**+ Parameter definition for the stored procedures of the creation of the commissin tables
		'+Definición de parámetros para los stored procedures de creación de las tablas de comisiones
		'**+ Information read on June 04,2001 03:52:17 p.m.
		'+Información leída el 04/06/2001 03:52:17 p.m.
		
		With lreccreTab_excomm
			.StoredProcedure = ValCommType(eActions.Cre)
			
			If nCommType = 4 Then 'Metas base (Tab_goals)
				.Parameters.Add("nAction", 1, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nCode", nTable_cod, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Else
				.Parameters.Add("nTable", nTable_cod, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			End If
			
			.Parameters.Add("sDescript", sDescript, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 30, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sShort_des", sShort_des, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If nCommType = 3 Then 'Sobre Comisiones (Tab_excomm)
				.Parameters.Add("sAssign", sType_assig, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			End If
			
			If nCommType = 4 Then 'Metas base (Tab_goals)
				.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("sStatregt", sStatregt, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Else
				.Parameters.Add("sStatregt", sStatregt, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			End If
			
			
			Add = .Run(False)
			
		End With
		
		'UPGRADE_NOTE: Object lreccreTab_excomm may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lreccreTab_excomm = Nothing
		
	End Function
	
	'**%Update: update records in the table "Tab_commission".  It returns TRUE or FALSE
	'**%depending on whether the stored procedure executed correctly.
	'%Update: Este método se encarga de actualizar registros en la tabla "Tab_commission". Devolviendo verdadero o
	'%falso dependiendo de si el Stored procedure se ejecutó correctamente.
	Public Function Update() As Boolean
		
		Dim lrecupdTab_excomm As eRemoteDB.Execute
		
		lrecupdTab_excomm = New eRemoteDB.Execute
		
		'**+ Parameter definition for the store procedure of updating the comission tables.
		'+Definición de parámetros para los stored procedure de actualización de las tablas de comisiones
		'**+ Information read on June 05,2001 03:21:23 p.m.
		'+Información leída el 05/06/2001 03:21:23 p.m.
		
		With lrecupdTab_excomm
			.StoredProcedure = ValCommType(eActions.Upd)
			
			If nCommType = 4 Then 'Metas base (Tab_goals)
				.Parameters.Add("nAction", 2, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nCode", nTable_cod, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Else
				.Parameters.Add("nTable", nTable_cod, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			End If
			
			.Parameters.Add("sDescript", sDescript, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 30, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sShort_des", sShort_des, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If nCommType = 3 Then 'Sobre Comisiones (Tab_excomm)
				.Parameters.Add("sAssign", sType_assig, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			End If
			
			If nCommType = 4 Then 'Metas base (Tab_goals)
				.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("sStatregt", sStatregt, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Else
				.Parameters.Add("sStatregt", sStatregt, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			End If
			
			Update = .Run(False)
			
		End With
		'UPGRADE_NOTE: Object lrecupdTab_excomm may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecupdTab_excomm = Nothing
		
	End Function
	
	'**%Delete: Delete records in the table "Tab_Commission".  Returns TRUE or FALSE
	'**%depending on whether the stored procedure executed correctly.
	'%Delete: Este método se encarga de eliminar registros en la tabla "XXXXXX". Devolviendo verdadero o
	'%falso dependiendo de si el Stored procedure se ejecutó correctamente.
	Public Function Delete() As Boolean
		
		Dim lrecdelTab_comgen As eRemoteDB.Execute
		
		lrecdelTab_comgen = New eRemoteDB.Execute
		
		'**+ Parameter definition for the stored procedure of the delete of the commission tables.
		'+Definición de parámetros para los stored procedure de eliminación de las tablas de comisiones
		'**+ Information read on June 05,2001 04:22:21 p.m.
		'+Información leída el 05/06/2001 04:22:21 p.m.
		
		With lrecdelTab_comgen
			.StoredProcedure = ValCommType(eActions.Del)
			
			If nCommType = 4 Then 'Metas base (Tab_goals)
				.Parameters.Add("nAction", 3, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nCode", nTable_cod, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("sDescript", sDescript, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 30, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("sShort_des", sShort_des, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("sStatregt", sStatregt, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Else
				.Parameters.Add("nTable", nTable_cod, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			End If
			
			Delete = .Run(False)
			
		End With
		
		'UPGRADE_NOTE: Object lrecdelTab_comgen may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecdelTab_comgen = Nothing
		
	End Function
	
	'**% Find_Det: validate the existence of the detail records of the different commission tables.
	'%Find_Det: Esta función se encarga de validar la existencia de los registros de detalle
	'%de las diferentes tablas de comisiones.
	Public Function Find_Det(ByVal nTable_cod As Integer, Optional ByVal lblnFind As Boolean = False) As Boolean
		
		Dim lrecreaTab_commission_v As eRemoteDB.Execute
		
		lrecreaTab_commission_v = New eRemoteDB.Execute
		
		'**+ Parameter definition for the stored procedure 'insudb.reaTab_commission_v'
		'+Definición de parámetros para stored procedure 'insudb.reaTab_commission_v'
		'**+ Information read on May 22,2001 10:46:20 a.m.
		'+Información leída el 22/05/2001 10:46:20 a.m.
		
		With lrecreaTab_commission_v
			.StoredProcedure = ValDetCommission()
			.Parameters.Add("PnTable_cod", nTable_cod, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run Then
				.RCloseRec()
				Find_Det = True
			Else
				Find_Det = False
			End If
		End With
		
		'UPGRADE_NOTE: Object lrecreaTab_commission_v may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaTab_commission_v = Nothing
		
	End Function
	
	'**% ValCommType (). This funtion is in cahrge of select the stroed procedure to execute
	'**% according to the commission tyoe and the operation that is's being made.
	'%ValCommType(). Esta funcion se encarga de seleccionar el stored procedure a ejecutar
	'%según el tipo de comisión y la operación que se esté relizando.
	Public Function ValCommType(ByVal lAction As eActions) As String
        If nCommType = 0 Then
            Dim varAux = ""

            Select Case lAction

                Case eActions.Cre
                    ValCommType = "creTab_comgen"
                Case eActions.Del
                    ValCommType = "delTab_comgen"
                Case eActions.Rea
                    ValCommType = "reaTab_comgen_a"
                Case eActions.Upd
                    ValCommType = "updTab_comgen"
                Case eActions.Val
                    ValCommType = "reaTab_comgen_v"

            End Select
        Else
            If nCommType = 1 Then
                Select Case lAction

                    Case eActions.Cre
                        ValCommType = "creTab_comlif"
                    Case eActions.Del
                        ValCommType = "delTab_comlif"
                    Case eActions.Rea
                        ValCommType = "reaTab_comlif_a"
                    Case eActions.Upd
                        ValCommType = "updTab_comlif"
                    Case eActions.Val
                        ValCommType = "reaTab_comlif_v"

                End Select
            Else
                If nCommType = 2 Then
                    Select Case lAction

                        Case eActions.Cre
                            ValCommType = "creDisex_int_m"
                        Case eActions.Del
                            ValCommType = "delDisex_int_m"
                        Case eActions.Rea
                            ValCommType = "reaDisex_int_m_a"
                        Case eActions.Upd
                            ValCommType = "updDisex_int_m"
                        Case eActions.Val
                            ValCommType = "reaDisex_int_m_v"

                    End Select
                Else
                    If nCommType = 3 Then
                        Select Case lAction

                            Case eActions.Cre
                                ValCommType = "creTab_excomm"
                            Case eActions.Del
                                ValCommType = "delTab_excomm"
                            Case eActions.Rea
                                ValCommType = "reaTab_excomm_a"
                            Case eActions.Upd
                                ValCommType = "updTab_excomm"
                            Case eActions.Val
                                ValCommType = "reaTab_excomm_v"

                        End Select
                    Else
                        If nCommType = 4 Then
                            Select Case lAction

                                Case eActions.Cre
                                    ValCommType = "insupdtab_goals"
                                Case eActions.Del
                                    ValCommType = "insupdtab_goals"
                                Case eActions.Rea
                                    ValCommType = "reaTab_goals_a"
                                Case eActions.Upd
                                    ValCommType = "insupdtab_goals"
                                Case eActions.Val
                                    ValCommType = "reaTab_goals_v"

                            End Select
                        End If
                    End If
                End If
            End If
        End If

    End Function
	
	'**% ValDetCommission. This function selects, according to the commission type,
	'**% the procedure for the validation of the existence of the detail records.
	'%ValDetCommission. Esta función se encarga de seleccionar, según el tipo de comisión,
	'% el procedimiento para la validacion de la existencia de los registros de detalle.
	Public Function ValDetCommission() As String
        If nCommType = 0 Then
            ValDetCommission = "reaDet_comgen_all"
        Else
            If nCommType = 1 Then
                ValDetCommission = "reaDet_comlif_all"
            Else
                If nCommType = 2 Then
                    ValDetCommission = "reaDisex_int_d_all"
                Else
                    If nCommType = 3 Then
                        ValDetCommission = "reaTab_comrat_all"
                    Else
                        If nCommType = 4 Then
                            ValDetCommission = "reaGoals"
                        End If
                    End If
                End If
            End If
        End If
        Return Nothing
    End Function
	
	'**% ValIntermedia. This function validates the type of the commission table
	'**% to be eliminated is not associated to an intermediary's code.
	'%ValIntermedia. Esta funcion se encarga de validar que el tipo de tabla de comisión
	'%a eliminar no se encuentre asociado a un codigo de intermediario.
	Public Function ValIntermedia() As Boolean
		
		'**- Variable definition lrecIntermedia for the execution of the StoredProcedure.
		'- Se define la variable lrecIntermedia para la ejecución del StoredProcedure
		
		Dim lrecIntermedia As eRemoteDB.Execute
		
		lrecIntermedia = New eRemoteDB.Execute
		
		ValIntermedia = False
		
		With lrecIntermedia
			.StoredProcedure = "reaIntermedia_table"
			.Parameters.Add("nTable", nTable_cod, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			Select Case nCommType
				Case 0
					.Parameters.Add("sField", "NCOMTABGE", eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 30, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				Case 1
					.Parameters.Add("sField", "NCOMTABLI", eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 30, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				Case 2
					.Parameters.Add("sField", "ECO_SCHE", eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 30, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				Case 3
					.Parameters.Add("sField", "TABLE_COD", eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 30, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				Case 4
					.Parameters.Add("sField", "nCode", eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 30, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			End Select
			
			If .Run Then
				.RCloseRec()
				ValIntermedia = True
			Else
				ValIntermedia = False
			End If
		End With
		
		'UPGRADE_NOTE: Object lrecIntermedia may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecIntermedia = Nothing
		
	End Function
	
	'**% insValMAG006_K: validate the data entered on the header form.
	'%insValMAG006_K: Esta función se encarga de validar los datos introducidos en la cabecera de la
	'%forma.
	Public Function insValMAG006_K(ByVal sCodispl As String, ByVal nAction As eFunctions.Menues.TypeActions, ByVal nSeleted As Integer, ByVal nCommType As Integer) As String
		Dim lclsErrors As eFunctions.Errors
        Dim lcolTab_commissions As eAgent.Tab_commissions = New Tab_commissions

        On Error GoTo insValMAG006_K_Err
		
		lclsErrors = New eFunctions.Errors
		
		'**+ In case of a query, validate that there is record to be shown
		'+En el caso de consulta, se valida que existan registros a ser mostrados
		If nAction = eFunctions.Menues.TypeActions.clngActionQuery Then
			
			If lcolTab_commissions Is Nothing Then
				lcolTab_commissions = New eAgent.Tab_commissions
			End If
			If Not (lcolTab_commissions.Find(nCommType)) Then
				Call lclsErrors.ErrorMessage(sCodispl, 1073)
			End If
			'UPGRADE_NOTE: Object lcolTab_commissions may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
			lcolTab_commissions = Nothing
		End If
		
		insValMAG006_K = lclsErrors.Confirm
		
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
		
insValMAG006_K_Err: 
		If Err.Number Then
			insValMAG006_K = lclsErrors.Confirm & Err.Description
		End If
		On Error GoTo 0
		
	End Function
	
	'**% insValMAG006: validate the data entered on the detail zone for the form,
	'%insValMAG006: Esta función se encarga de validar los datos introducidos en la zona de
	'%detalle de la forma.
	Public Function insValMAG006(ByVal sCodispl As String, ByVal sAction As String, ByVal nSeleted As Integer, ByVal nCommType As Integer, ByVal nTable_cod As Integer, ByVal sDescript As String, ByVal sShort_des As String, ByVal sType_assig As String, ByVal sStatregt As String) As String
		'**- Variable definition lclsErrors for the errorss in the window sending.
		'- Se define la variable lclsErrors para el envío de errores de la ventana
		Dim lclsErrors As eFunctions.Errors
		
		'**- Variable definition lclsTab_Commission for executing the validation methods.
		'- Se define la variable lclsTab_Commission para ejecutar los métodos de validación.
		Dim lblnError As Boolean
		
		lclsErrors = New eFunctions.Errors
		
		On Error GoTo insValMAG006_Err
		With Me
			.nCommType = nCommType
			.nTable_cod = nTable_cod
		End With
		
		lblnError = True
		
		'**+ Initiate de validatino cycle
		'+Se da inicio al ciclo de validaciones.
		If nTable_cod = eRemoteDB.Constants.intNull Or nTable_cod = 0 Then
			Call lclsErrors.ErrorMessage(sCodispl, 1942)
			
			If sDescript <> strNull Or sShort_des <> strNull Or sType_assig <> strNull Or (sStatregt <> strNull And CDbl(sStatregt) <> 0) Then
				Call lclsErrors.ErrorMessage(sCodispl, 1084)
			End If
		End If
		
		If sAction = "Del" Then
			If ValIntermedia() Then
				Call lclsErrors.ErrorMessage(sCodispl, 10047)
			Else
				If Find_Det(nTable_cod) Then
					Call lclsErrors.ErrorMessage(sCodispl, 10047)
				End If
			End If
		Else
			
			If nTable_cod <> eRemoteDB.Constants.intNull And nTable_cod <> 0 And sAction = "Add" Then
				
				'**+ Validate that the value in the field does not exist in the table.
				'+Se valida que el valor introducido en el campo no se encuentre en la tabla registrado
				If Find(nTable_cod, True) Then
					lblnError = False
					Call lclsErrors.ErrorMessage(sCodispl, 10284)
				End If
			End If
			
			If nTable_cod <> eRemoteDB.Constants.intNull And nTable_cod <> 0 And lblnError Then
				
				'**+ If the table field has a value, the other arrengement fields must be full
				'+Si el campo tabla tiene valor deben estar llenos los demas campos del arreglo.
				
				If sDescript = strNull Then
					Call lclsErrors.ErrorMessage(sCodispl, 10857)
				End If
				
				If sShort_des = strNull Then
					Call lclsErrors.ErrorMessage(sCodispl, 10858)
				End If
				
				If (Trim(sType_assig) = String.Empty Or Trim(sType_assig) = "0" Or Trim(sType_assig) = strNull) And nCommType = 3 Then
					Call lclsErrors.ErrorMessage(sCodispl, 10180)
				End If
				
				If sStatregt = strNull Or CDbl(sStatregt) = 0 Then
					Call lclsErrors.ErrorMessage(sCodispl, 9089)
				End If
			End If
		End If
		insValMAG006 = lclsErrors.Confirm
		
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
		
insValMAG006_Err: 
		If Err.Number Then
			insValMAG006 = lclsErrors.Confirm & Err.Description
		End If
		On Error GoTo 0
		
	End Function
	'%insValMAG006dup: Esta función se encarga de validar los datos
	Public Function insValMAG006dup(ByVal sCodispl As String, ByVal nCommType As Integer, ByVal nTable_cod As Integer, ByVal sDescript As String) As String
		'- Se define la variable lclsErrors para el envío de errores de la ventana
		Dim lclsErrors As eFunctions.Errors
		
		'- Se define la variable lclsTab_Commission para ejecutar los métodos de validación.
		Dim lblnError As Boolean
		
		lclsErrors = New eFunctions.Errors
		
		On Error GoTo insValMAG006dup_Err
		
		
		With Me
			.nCommType = nCommType
			.nTable_cod = nTable_cod
		End With
		
		lblnError = True
		'Se verifica que nuevo codigo nos sea null
		If nTable_cod = eRemoteDB.Constants.intNull Then
			Call lclsErrors.ErrorMessage(sCodispl, 1942)
		Else
			'Se verifica que nuevo codigo no exista, segun la tabla
			If Find(nTable_cod, True) Then
				lblnError = False
				Call lclsErrors.ErrorMessage(sCodispl, 10284)
			End If
			'Se verifica que la descripcion no sea null
			If lblnError Then
				If sDescript = strNull Then
					Call lclsErrors.ErrorMessage(sCodispl, 10857)
				End If
			End If
		End If
		
		insValMAG006dup = lclsErrors.Confirm
		
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
		
insValMAG006dup_Err: 
		If Err.Number Then
			insValMAG006dup = lclsErrors.Confirm & Err.Description
		End If
		On Error GoTo 0
		
	End Function
	'%insPostMAG006: Esta función se encarga de llamar al método correspondiente a la acción
	'* ejecutada (crear/actualizar/eliminar) sobre las tablas de comisión
	Public Function insPostMAG006(ByVal sAction As String, ByVal nSeleted As Integer, ByVal nCommType As Integer, ByVal nTable_cod As Integer, ByVal sDescript As String, ByVal sShort_des As String, ByVal sType_assig As String, ByVal sStatregt As String, ByVal nUsercode As Integer) As Boolean
		On Error GoTo insPostMAG006_err
		
		Me.nCommType = nCommType
		Me.nTable_cod = nTable_cod
		Me.sDescript = sDescript
		Me.sShort_des = sShort_des
		Me.sType_assig = sType_assig
		
		If sAction = "Add" Then
			Me.sStatregt = "2"
		Else
			Me.sStatregt = sStatregt
		End If
		
		Me.nUsercode = nUsercode
		
		insPostMAG006 = True
		
		Select Case sAction
			
			'**+ If the selected option is Add
			'+Si la opción seleccionada es Registrar
			
			Case "Add"
				insPostMAG006 = Add()
				
				'**+ If the selected option is Modify
				'+Si la opción seleccionada es Modificar
				
			Case "Update"
				insPostMAG006 = Update()
				
				'**+ If the selected option is Eliminate
				'+Si la opción seleccionada es Eliminar
				
			Case "Del"
				insPostMAG006 = Delete()
				
		End Select
		
insPostMAG006_err: 
		If Err.Number Then
			insPostMAG006 = False
		End If
		On Error GoTo 0
		
	End Function
	'%insPostMAG006dup: Esta función se encarga de llamar al método correspondiente a la acción
	'* ejecutada (crear/actualizar/eliminar) sobre las tablas de comisión
	Public Function insPostMAG006dup(ByVal nCommType As Integer, ByVal nTable_coddup As Integer, ByVal sDescript As String, ByVal dEffecdate As Date, ByVal nTable_cod As Integer, ByVal nUsercode As Integer) As Boolean
		Dim lrecdupTab As eRemoteDB.Execute
		
		lrecdupTab = New eRemoteDB.Execute
		
		On Error GoTo insPostMAG006dup_err
		
		insPostMAG006dup = True
		
		With lrecdupTab
			Select Case nCommType
				'+ Ramos generales
				Case 0
					.StoredProcedure = "insTab_Comgendup"
					.Parameters.Add("nComtabgedu", nTable_coddup, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Parameters.Add("sDescript", sDescript, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 30, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Parameters.Add("nComtabge", nTable_cod, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					'+ Vida
				Case 1
					.StoredProcedure = "insTab_Comlifdup"
					.Parameters.Add("nComtablidu", nTable_coddup, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Parameters.Add("sDescript", sDescript, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 30, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Parameters.Add("nComtabli", nTable_cod, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					'+Esquema economico
				Case 2
					.StoredProcedure = "insDisex_int_mdup"
					.Parameters.Add("neco_schedu", nTable_coddup, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Parameters.Add("sDescript", sDescript, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 30, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Parameters.Add("neco_sche", nTable_cod, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					'+Sobre comisiones
				Case 3
					.StoredProcedure = "insTab_Excommdup"
					.Parameters.Add("ntable_coddu", nTable_coddup, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Parameters.Add("sDescript", sDescript, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 30, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Parameters.Add("ntable_cod", nTable_cod, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					'+Metas Base
				Case 4
					.StoredProcedure = "insTab_Goalsdup"
					.Parameters.Add("ncodedu", nTable_coddup, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Parameters.Add("sDescript", sDescript, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 30, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Parameters.Add("ncode", nTable_cod, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			End Select
			
			
			insPostMAG006dup = .Run(False)
			
		End With
insPostMAG006dup_err: 
		If Err.Number Then
			insPostMAG006dup = False
		End If
		On Error GoTo 0
	End Function
	
	
	'*** Class_Initialize: controls the opening of the class
	'* Class_Initialize: se controla la apertura de la clase
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		'nUsercode = GetSetting("TIME", "GLOBALS", "USERCODE", 0)
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
End Class






