Option Strict Off
Option Explicit On
Public Class Tab_cost_c
	'%-------------------------------------------------------%'
	'% $Workfile:: Tab_cost_c.cls                           $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 1:18p                                $%'
	'% $Revision:: 7                                        $%'
	'%-------------------------------------------------------%'
	
	'**+Properties accourding to the table in the system 05/23/2001
	'**+The key field correspond to nLed_compan, sCost_cente
	'+ Propiedades según la tabla en el sistema 23/05/2001
	'+ El campo llave corresponde a nLed_compan, sCost_cente
	'+ Column_name                                    Type        Computed  Length  Prec  Scale Nullable                          TrimTrailingBlanks                  FixedLenNullInSource
	'-----------------                             ----------- --------- ------- ----- ----- --------------------------------- ----------------------------------- -----------------------------------
	Public nLed_compan As Integer 'smallint   no         2      5     0     no                                (n/a)                               (n/a)
	Public sCost_cente As String 'char       no         8                  no                                 yes                                 no
	Public sBlock_cre As String 'char       no         1                  yes                                yes                                 yes
	Public sBlock_deb As String 'char       no         1                  yes                                yes                                 yes
	Public dCompdate As Date 'datetime   no         8                  yes                               (n/a)                               (n/a)
	Public sDescript As String 'char       no         30                 yes                                yes                                 yes
	Public nNoteNum As Integer 'int        no         4      10    0     yes                               (n/a)                               (n/a)
	Public sStatregt As String 'char       no         1                  yes                                yes                                 yes
	Public nUsercode As Integer 'smallint   no         2      5     0     yes                               (n/a)                               (n/a)
	'**-Define the variable to indicate the status of each instance in the collection
	'**Public nStatusInstance As eStatusInstance
	'- Se define la variable para indicar el estado de cada instancia en la colección
	'Public nStatusInstance As eStatusInstance
	
	'**% Add:  Adds a new Organizative Unity or Cost center to the table
	'**%Tab_cost_c
	'% Add: Permite añadir una nueva Unidad Organizativa o Centro de Costos  a la Tabla
	'% Tab_cost_c
	Public Function Add(ByVal nLed_compan As Integer, ByVal nNoteNum As Integer, ByVal sBlock_cre As String, ByVal sBlock_deb As String, ByVal sDescript As String, ByVal sStatregt As String, ByVal dCompdate As Date, ByVal nUsercode As Integer) As Boolean
		Dim lreccreTab_cost_c As eRemoteDB.Execute
		
		lreccreTab_cost_c = New eRemoteDB.Execute
		
		'**+parameters definition for the stored procedure 'insudb.creTab_cost_c'
		'**+Data read on 05/23/2001 04:46:13p.m.
		'+ Definición de parámetros para stored procedure 'insudb.creTab_cost_c'
		'+ Información leída el 23/05/2001 04:46:13 p.m.
		
		With lreccreTab_cost_c
			.StoredProcedure = "creTab_cost_c"
			.Parameters.Add("nLed_compan", nLed_compan, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sCost_cente", sCost_cente, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 8, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sDescript", sDescript, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 30, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nNotenum", nNoteNum, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sBlock_deb", sBlock_deb, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sBlock_cre", sBlock_cre, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sStatregt", sStatregt, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Add = .Run(False)
		End With
		
		'UPGRADE_NOTE: Object lreccreTab_cost_c may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lreccreTab_cost_c = Nothing
	End Function
	
	'**%Delete: Deletes of the Organizative Unit or the Cost center
	'% Delete: Permite la eliminación de Unidad Organizativa o Centro de Costos
	Public Function Delete() As Boolean
		Dim lrecdelTab_cost_c As eRemoteDB.Execute
		
		lrecdelTab_cost_c = New eRemoteDB.Execute
		
		'**+Parameters definition for the stored procedure 'insudb.delTab_cost_c'
		'**+Data read on 05/23/2001 03:36:50 p.m.
		'+ Definición de parámetros para stored procedure 'insudb.delTab_cost_c'
		'+ Información leída el 23/05/2001 03:36:50 p.m.
		
		With lrecdelTab_cost_c
			.StoredProcedure = "delTab_cost_c"
			.Parameters.Add("nLed_compan", nLed_compan, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sCost_cente", sCost_cente, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 8, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Delete = .Run(False)
		End With
		
		'UPGRADE_NOTE: Object lrecdelTab_cost_c may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecdelTab_cost_c = Nothing
	End Function
	
	'**% Find:  Searches the Organizative Unit table or Cost Center
	'% Find: Permite buscar registros en la tabla de Unidades Organizativas o Centro de Costos
	Public Function Find(ByVal lintLed_Compan As Integer, ByVal lstrCost_cente As String, Optional ByVal lblnFind As Boolean = False) As Boolean
		Dim lrecreaTab_cost_c As eRemoteDB.Execute
		Static lblnRead As Boolean
		
		lrecreaTab_cost_c = New eRemoteDB.Execute
		
		lstrCost_cente = Trim(lstrCost_cente)
		
		If nLed_compan <> lintLed_Compan Or sCost_cente <> lstrCost_cente Or lblnFind Then
			
			nLed_compan = lintLed_Compan
			sCost_cente = lstrCost_cente
			
			'**+Parameters defintion for the stored procedure 'insudb.reaTab_cost_c'
			'**+Data read on 05/23/2001 04:27:54 PM
			'+ Definición de parámetros para stored procedure 'insudb.reaTab_cost_c'
			'+ Información leída el 23/05/2001 04:27:54 PM
			
			With lrecreaTab_cost_c
				.StoredProcedure = "reaTab_cost_c"
				.Parameters.Add("nLed_compan", lintLed_Compan, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("sCost_cente", lstrCost_cente, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 8, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				
				If .Run(True) Then
					nLed_compan = .FieldToClass("nLed_compan")
					sCost_cente = .FieldToClass("sCost_cente")
					sBlock_cre = .FieldToClass("sBlock_cre")
					sBlock_deb = .FieldToClass("sBlock_deb")
					sDescript = .FieldToClass("sDescript")
					nNoteNum = .FieldToClass("nNotenum")
					sStatregt = .FieldToClass("sStatregt")
					lblnRead = True
					.RCloseRec()
				Else
					lblnRead = False
				End If
			End With
		End If
		
		Find = lblnRead
		
		'UPGRADE_NOTE: Object lrecreaTab_cost_c may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaTab_cost_c = Nothing
		
	End Function
	
	'**% Update:  Updates the data of one Organizative Unit or Cost center
	'% Update: Permite la actualización de los datos de una Unidad Organizativa o Centro de
	'% Costos
	Public Function Update() As Boolean
		Dim lrecupdTab_cost_c As eRemoteDB.Execute
		
		lrecupdTab_cost_c = New eRemoteDB.Execute
		
		'**Parameters defintion for the stored procedure 'insudb.updTab_cost_c'
		'**+Data read on 08/17/2000 03:41:34 p.m.
		'+ Definición de parámetros para stored procedure 'insudb.updTab_cost_c'
		'+ Información leída el 17/08/2000 03:41:34 p.m.
		
		With lrecupdTab_cost_c
			.StoredProcedure = "updTab_cost_c"
			.Parameters.Add("nLed_compan", nLed_compan, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sCost_cente", sCost_cente, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 8, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sDescript", sDescript, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 30, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nNotenum", nNoteNum, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sBlock_deb", sBlock_deb, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sBlock_cre", sBlock_cre, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sStatregt", sStatregt, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Update = .Run(False)
		End With
		
		'UPGRADE_NOTE: Object lrecupdTab_cost_c may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecupdTab_cost_c = Nothing
	End Function
	
	'**%Val_Unit_Organ_struct:  Verifies the structure of the organizative unit.
	'%Val_Unit_Organ_struct: Permite verificar la estructura de la unidad organizativa.
	Public Function Val_Unit_Organ_struct(ByVal lintLed_Compan As Integer, ByVal lstrCost_cente As String) As Boolean
		
		Dim llngNum As Integer
		Dim llngPos As Integer
		Dim llngCount As Integer
		
		Dim lclsLed_compan As Led_compan
		
		lclsLed_compan = New Led_compan
		
		On Error GoTo Val_Unit_Organ_struct_Err
		
		With lclsLed_compan
			.Find(lintLed_Compan)
			
			lstrCost_cente = Trim(lstrCost_cente)
			
			Val_Unit_Organ_struct = True
			
			If Trim(.sStruct_uni) = "" Then
				Val_Unit_Organ_struct = False
			Else
				If CInt(.sStruct_uni) = 0 Then
					Val_Unit_Organ_struct = False
				Else
					llngNum = 0
					llngPos = 1
					
					For llngCount = 1 To 3
						If CDbl(Mid(.sStruct_uni, llngCount, 1)) = 0 Then
							Exit For
						End If
						
						Do While Mid(lstrCost_cente, llngPos, 1) <> "-" And llngPos <= Len(lstrCost_cente)
							llngNum = llngNum + 1
							llngPos = llngPos + 1
							
							If llngPos > Len(lstrCost_cente) Then
								Exit Do
							End If
						Loop 
						
						If llngPos > Len(lstrCost_cente) Then
							If llngNum <> CDbl(Mid(.sStruct_uni, llngCount, 1)) Then
								Val_Unit_Organ_struct = False
							End If
							
							Exit For
						Else
							If llngNum <> CDbl(Mid(.sStruct_uni, llngCount, 1)) Then
								Val_Unit_Organ_struct = False
								
								Exit For
							Else
								llngNum = 0
								llngPos = llngPos + 1
							End If
						End If
					Next llngCount
				End If
			End If
		End With
		
		'UPGRADE_NOTE: Object lclsLed_compan may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsLed_compan = Nothing
		
Val_Unit_Organ_struct_Err: 
		If Err.Number Then
			Val_Unit_Organ_struct = False
		End If
		On Error GoTo 0
	End Function
	
	'**%insValCP009: routine to validate the window header.
	'%insValCP009: Rutina de validación del encabezado de la ventana.
	Public Function insValCP009(ByVal nLed_compan As Integer, ByVal plngAction As Integer, ByVal sCodispl As String, ByVal sSel As String, ByVal sStratregt As String, ByVal sCost_cente As String, ByVal sBlock_deb As String, ByVal sBlock_cre As String, ByVal sDescript As String) As String
		Dim lclsErrors As eFunctions.Errors
		Dim lclsField As eFunctions.valField
		Dim lclsTab_cost_c As eLedge.Tab_cost_c
		Dim mcolTab_cost_cs As eLedge.Tab_cost_cs
		
		
		On Error GoTo insValCP009_Err
		
		lclsErrors = New eFunctions.Errors
		lclsField = New eFunctions.valField
		lclsTab_cost_c = New eLedge.Tab_cost_c
		mcolTab_cost_cs = New eLedge.Tab_cost_cs
		
		
		'**+Verifies the existence of the lower levels of the unit sCost_cente
		'**+when will be make the delete of it
		'+ Verificación de la existencia de niveles inferiores de la unidad
		'+ sCost_cente al momento de realizar la eliminación de la misma
		
		If plngAction = eFunctions.Menues.TypeActions.clngActionUpdate And Trim(sSel) = "2" Then
			If lclsTab_cost_c.Val_Unit_Organ_Down(nLed_compan, sCost_cente) Then
				If Not insVal_Organ_DownCut_in_vec(sCost_cente, sSel, nLed_compan) Then
					Call lclsErrors.ErrorMessage(sCodispl, 736014)
				End If
			End If
		End If
		
		'**+Validates the Unity code - sCost_cente
		'+Validaciòn de Còdigo de la unidad - sCost_cente
		If plngAction = eFunctions.Menues.TypeActions.clngActionadd Then
			If Trim(sCost_cente) = "" Then
				Call lclsErrors.ErrorMessage(sCodispl, 36051)
			Else
				If Trim(sCost_cente) <> "" Then
					If Not lclsTab_cost_c.Val_Unit_Organ_struct(nLed_compan, sCost_cente) Then
						Call lclsErrors.ErrorMessage(sCodispl, 36073)
					Else
						If mcolTab_cost_cs.Find_Cost_cente(sCost_cente, nLed_compan) Then '***
							Call lclsErrors.ErrorMessage(sCodispl, 36082)
						Else
							With lclsTab_cost_c
								.sBlock_cre = "2"
								.sBlock_deb = "2"
								.sStatregt = "1"
								
								If Not insVal_Unit_Org_Pre_vec(sCost_cente, sBlock_deb, sBlock_cre, sStratregt) Then
									If Not lclsTab_cost_c.Val_Unit_Organ_Previous(nLed_compan, sCost_cente) Then
										Call lclsErrors.ErrorMessage(sCodispl, 36074)
									Else
									End If
								End If
							End With
						End If
					End If
				End If
			End If
		End If
		
		'**+Validation of the description - sDescript.
		'+Validaciòn de la Descripciòn - sDescript.
		If Trim(sDescript) = "" Then
			Call lclsErrors.ErrorMessage(sCodispl, 36076)
		End If
		
		'**+Validates the field record field - sStatregt.
		'+Validaciòn del campo estado de registro - sStatregt.
		If Me.Find(nLed_compan, sCost_cente) Then
		End If
		With lclsTab_cost_c
			If Trim(sStatregt) <> "" Then
				If Trim(sCost_cente) <> "" Then
					.sStatregt = "1"
					If insVal_Unit_Org_Pre_vec(sCost_cente, sBlock_deb, sBlock_cre, sStratregt) Then
						If .sStatregt <> "1" Then 'Activo
							If .sStatregt <> Trim(sStatregt) Then
								Call lclsErrors.ErrorMessage(sCodispl, 736032)
							End If
						End If
						
					Else
						.sStatregt = "1"
						If lclsTab_cost_c.Val_Unit_Organ_Previous(nLed_compan, sCost_cente) Then
							If .sStatregt <> "1" Then 'Activo  '**Active
								If .sStatregt <> Trim(sStatregt) Then
									Call lclsErrors.ErrorMessage(sCodispl, 736032)
								End If
							End If
						End If
					End If
				End If
			End If
		End With
		insValCP009 = lclsErrors.Confirm
		
		'UPGRADE_NOTE: Object lclsTab_cost_c may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsTab_cost_c = Nothing
		'UPGRADE_NOTE: Object mcolTab_cost_cs may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		mcolTab_cost_cs = Nothing
		'UPGRADE_NOTE: Object lclsField may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsField = Nothing
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
		
insValCP009_Err: 
		If Err.Number Then
			insValCP009 = insValCP009 & Err.Description
		End If
		On Error GoTo 0
	End Function
	
	'**%Val_Unit_Organ_Down:  Verifies if the organizative unit has lower levels.
	'%Val_Unit_Organ_Down: Permite verificar si la unidad organizativa tiene niveles inferiores.
	Public Function Val_Unit_Organ_Down(ByVal lintLed_Compan As Integer, ByVal lstrCost_cente As String) As Boolean
		Dim lintCount As Integer
		Dim lrecreaTab_cost_cDown As eRemoteDB.Execute
		
		lintCount = 0
		
		On Error GoTo Val_Unit_Organ_Down_err
		
		Val_Unit_Organ_Down = False
		lrecreaTab_cost_cDown = New eRemoteDB.Execute
		
		lstrCost_cente = Trim(lstrCost_cente) & "-"
		
		If Len(lstrCost_cente) <= 7 Then
			
			'**+Parameters definition for the stored procedure 'insudb.reaTab_cost_cDown'
			'**Data read on 07/02/2001 09:55:38 p.m.
			'+ Definición de parámetros para stored procedure 'insudb.reaTab_cost_cDown'
			'+ Información leída el 02/07/2001 09:55:38 p.m.
			
			With lrecreaTab_cost_cDown
				.StoredProcedure = "reaTab_cost_cDown"
				.Parameters.Add("nLed_compan", lintLed_Compan, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("sCost_cente", lstrCost_cente, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 8, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				Val_Unit_Organ_Down = .Run(True)
				.RCloseRec()
			End With
		End If
		
		'UPGRADE_NOTE: Object lrecreaTab_cost_cDown may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaTab_cost_cDown = Nothing
		
Val_Unit_Organ_Down_err: 
		If Err.Number Then
			Val_Unit_Organ_Down = False
		End If
		On Error GoTo 0
	End Function
	
	'**% insVal_Organ_DownCut_in_vec: this routine  verifies if the organizative unit has lower levels cuted.
	'%insVal_Organ_DownCut_in_vec: Esta rútina permite verificar si la unidad organizativa tiene niveles inferiores cortados.
	Public Function insVal_Organ_DownCut_in_vec(ByVal sCost_cente As String, ByVal sSel As String, ByVal nLed_compan As Integer) As Boolean
		Dim llngRow As Object
		Dim lstrCost_cente As Object
		
		On Error GoTo insVal_Organ_DownCut_in_vec_err
		
		insVal_Organ_DownCut_in_vec = True
		
		'For llngRow = 0 To lxarDataCP009.Count(1) - 1
		If Me.Find(nLed_compan, sCost_cente) Then
			lstrCost_cente = Me.sCost_cente
			If Trim(sCost_cente) = Trim(Mid(lstrCost_cente, 1, Len(sCost_cente))) Then
				If Trim(sSel) <> "2" Then
					insVal_Organ_DownCut_in_vec = False
					'Exit For
				End If
			End If
		End If
		'Next llngRow
insVal_Organ_DownCut_in_vec_err: 
		If Err.Number Then
			insVal_Organ_DownCut_in_vec = False
		End If
		On Error GoTo 0
	End Function
	
	'**%insValDupgrddataarray: this routine  validates if exist a record
	'**%duplicated in the grid on treatment.
	''%insValDupgrddataarray: Esta rutina permite validar si existe un registro
	''%duplicado en el grid en tratamiento.
	''----------------------------------------------------------------------
	'Function insValDupgrdDataArray(ByVal llngRowP As Long, ByVal lblnall As Boolean) As Boolean
	''----------------------------------------------------------------------
	'    Dim llngRow As Variant
	'
	'    insValDupgrdDataArray = False
	'
	'    For llngRow = 0 To lxarDataCP009.Count(1) - 1
	'        If llngRow <> llngRowP Then
	'          If Not lblnall Then
	'             If Trim$(lxarDataCP009(llngRow, 1)) = Trim$(grddataarray(0).Columns(1).Value) Then
	'                insValDupgrdDataArray = True
	'
	'                Exit For
	'             End If
	'          Else
	'             If Trim$(lxarDataCP009(llngRow, 1)) = Trim$(lxarDataCP009(llngRowP, 1)) Then
	'                insValDupgrdDataArray = True
	'
	'                Exit For
	'             End If
	'          End If
	'        End If
	'    Next llngRow
	'End Function
	
	'**%insVal_Unit_Organ_Pre_vec: this routine  validates if the organizative routine has previous level in the vector
	'%insVal_Unit_Organ_Pre_vec: Esta rútina permite verificar si la unidad organizativa tiene niveles previos en el vector
	Private Function insVal_Unit_Org_Pre_vec(ByVal lstrUnit As String, ByVal sBlock_deb As String, ByVal sBlock_cre As String, ByVal sStratregt As String) As Boolean
		Dim llngLength As Integer
		Dim lstrCente As String
		Dim llngCount As Integer
        'Dim llngRow As Integer
		Dim mclsTab_cost_c As eLedge.Tab_cost_c
		mclsTab_cost_c = New eLedge.Tab_cost_c
		
		insVal_Unit_Org_Pre_vec = False
		
		lstrCente = Trim(lstrUnit)
		llngLength = Len(lstrCente)
		
		For llngCount = llngLength To 1 Step -1
			If Mid(lstrCente, llngCount, 1) <> "-" Then
				Mid(lstrCente, llngCount, 1) = " "
			Else
				Mid(lstrCente, llngCount, 1) = " "
				
				Exit For
			End If
		Next llngCount
		
		If Trim(lstrCente) <> "" Then
			'For llngRow = 0 To lxarDataCP009.Count(1) - 1
			With mclsTab_cost_c
				'If llngRow <> llngRowP Then
				If Trim(lstrUnit) = Trim(lstrCente) Then
					insVal_Unit_Org_Pre_vec = True
					
					If Trim(sBlock_deb) = "0" Then
						.sBlock_deb = "2"
					Else
						.sBlock_deb = "1"
					End If
					
					If Trim(sBlock_cre) = "0" Then
						.sBlock_cre = "2"
					Else
						.sBlock_cre = "1"
					End If
					
					.sStatregt = Trim(sStratregt)
					
					'Exit For
				End If
				'End If
			End With
			'Next llngRow
		Else
			insVal_Unit_Org_Pre_vec = True
		End If
	End Function
	
	'**%Val_Unit_Organ_Previous:  Verifies if the organizative unit has previous levels.
	'%Val_Unit_Organ_Previous: Permite verificar si la unidad organizativa tiene niveles previos
	Public Function Val_Unit_Organ_Previous(ByVal lintLed_Compan As Integer, ByVal lstrCost_cente As String) As Boolean
		Dim llngLength As Integer
		Dim lstrCost_centeAux As String
		Dim llngCount As Integer
		
		Val_Unit_Organ_Previous = False
		
		lstrCost_centeAux = Trim(lstrCost_cente)
		llngLength = Len(lstrCost_centeAux)
		
		For llngCount = llngLength To 1 Step -1
			If Mid(lstrCost_centeAux, llngCount, 1) <> "-" Then
				Mid(lstrCost_centeAux, llngCount, 1) = " "
			Else
				Mid(lstrCost_centeAux, llngCount, 1) = " "
				
				Exit For
			End If
		Next llngCount
		
		If Trim(lstrCost_centeAux) <> "" Then
			If Me.Find(lintLed_Compan, lstrCost_centeAux) Then
				Val_Unit_Organ_Previous = True
			End If
		End If
	End Function
	
	'**%insPostCP009: This function is in charge of validating all the entered data in the form
	'%insPostCP009: Esta función se encaga de validar todos los datos introducidos en la forma
	Public Function insPostCP009(ByVal nAction As String, ByVal nUsercode As Integer, ByVal sSel As String, ByVal nLed_compan As Integer, ByVal nNoteNum As Integer, ByVal sBlock_cre As String, ByVal sBlock_deb As String, ByVal sDescript As String, ByVal sStatregt As String, ByVal dCompdate As Date, ByVal sCost_cente As String) As Boolean
		Dim lclsLed_compan As eLedge.Led_compan
		Dim pclsAcc_transa As eLedge.Acc_transa
		Dim lclsGeneralForm As Object
		'    Set lclsGeneralForm = New eGeneralForm.Notes
		Dim lintAction As Integer
		
		
		lclsLed_compan = New eLedge.Led_compan
		pclsAcc_transa = New eLedge.Acc_transa
		lclsGeneralForm = eRemoteDB.NetHelper.CreateClassInstance("eGeneralForm.Notes")
		
		On Error GoTo insPostCP009_err
		
		insPostCP009 = True
		
		Me.nLed_compan = nLed_compan
		Me.nNoteNum = nNoteNum
		Me.nUsercode = nUsercode
		Me.sBlock_cre = sBlock_cre
		Me.sBlock_deb = sBlock_deb
		Me.sCost_cente = sCost_cente
		Me.sDescript = sDescript
		Me.sStatregt = sStatregt
		Me.dCompdate = dCompdate
		
		If nAction = "Add" Then
			lintAction = eFunctions.Menues.TypeActions.clngActionadd
		ElseIf nAction = "Update" Then 
			lintAction = eFunctions.Menues.TypeActions.clngActionUpdate
		ElseIf nAction = "303" Then 
			lintAction = eFunctions.Menues.TypeActions.clngActioncut
		End If
		
		Select Case lintAction
			
			'**+If the selected option is Register
			'+Si la opción seleccionada es Registrar
			
			Case eFunctions.Menues.TypeActions.clngActionadd
				If insCreTab_cost_c(lintAction, nLed_compan, nNoteNum, sBlock_cre, sBlock_deb, sDescript, sStatregt, dCompdate, nUsercode) Then
					insPostCP009 = True
				Else
					insPostCP009 = False
				End If
				
				
				'**+If the selected option is Modify or Delete
				'+Si la opción seleccionada es Modificar ò Eliminar
				
			Case eFunctions.Menues.TypeActions.clngActionUpdate, eFunctions.Menues.TypeActions.clngActioncut
				If insUpdTab_cost_c(lintAction, nLed_compan, nNoteNum, sBlock_cre, sBlock_deb, sDescript, sStatregt, dCompdate, sSel, sCost_cente, nUsercode) Then
					insPostCP009 = True
				Else
					insPostCP009 = False
				End If
				
				
		End Select
		
		
insPostCP009_err: 
		If Err.Number Then
			insPostCP009 = False
		End If
		On Error GoTo 0
		
	End Function
	
	'**%insCreTab_cost_c: Function that creates the record in the Tab_cost_c table (Organizative Unity).
	'%insCreTab_cost_c:Función que permite crear los registros en la tabla Tab_cost_c (Unidad Organizativa).
	Public Function insCreTab_cost_c(ByVal nAction As Integer, ByVal nLed_compan As Integer, ByVal nNoteNum As Integer, ByVal sBlock_cre As String, ByVal sBlock_deb As String, ByVal sDescript As String, ByVal sStatregt As String, ByVal dCompdate As Date, ByVal nUsercode As Integer) As Boolean
		
		'**-Defines the variable llngCount used to keep the vector index on treatment.
		'-Se define la variable llngCount utilizada para almacenar el indice del vector en tratamiento.
		
        'Dim llngCount As Integer
		Dim lstrBlock_cre As String
		Dim lstrBlock_deb As String
		Dim lclsTab_cost_c As Tab_cost_c
		Dim mcolTab_cost_cs As eLedge.Tab_cost_cs
		
		mcolTab_cost_cs = New eLedge.Tab_cost_cs
		
		insCreTab_cost_c = True
		
		'     For llngCount = 0 To lxarDataCP009.Count(1) - 1
		'       If Trim$(sSel) = "" Then
		'           Exit For
		'        End If
		If Trim(sBlock_deb) = "0" Then
			lstrBlock_deb = "2" 'Bloquear débitos- sBlock_deb '**Block debits
		Else
			lstrBlock_deb = "1"
		End If
		
		If Trim(sBlock_cre) = "0" Then
			lstrBlock_cre = "2" 'Bloquear créditos - sBlock_cre '** Block credits
		Else
			lstrBlock_cre = "1"
		End If
		
		lclsTab_cost_c = New Tab_cost_c
		lclsTab_cost_c.nLed_compan = nLed_compan
		lclsTab_cost_c.nNoteNum = nNoteNum
		lclsTab_cost_c.nUsercode = nUsercode
		lclsTab_cost_c.sBlock_cre = Trim(lstrBlock_cre)
		lclsTab_cost_c.sBlock_deb = Trim(lstrBlock_deb)
		lclsTab_cost_c.sCost_cente = sCost_cente
		lclsTab_cost_c.sDescript = sDescript
		lclsTab_cost_c.sStatregt = sStatregt
		lclsTab_cost_c.dCompdate = dCompdate
		
		Call mcolTab_cost_cs.Add(lclsTab_cost_c)
		
		If mcolTab_cost_cs.Update(nAction, sCost_cente, nLed_compan, nNoteNum, lstrBlock_cre, lstrBlock_deb, sDescript, sStatregt, dCompdate, nUsercode) Then
		End If
		
		'UPGRADE_NOTE: Object lclsTab_cost_c may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsTab_cost_c = Nothing
		
	End Function
	
	'**%insUpdTab_cost_c: routine that  deletes or modifies the window records.
	'%insUpdTab_cost_c: Rutina que permite eliminar o modificar los registros en la ventana.
	Private Function insUpdTab_cost_c(ByVal nAction As Integer, ByVal nLed_compan As Integer, ByVal nNoteNum As Integer, ByVal sBlock_cre As String, ByVal sBlock_deb As String, ByVal sDescript As String, ByVal sStatregt As String, ByVal dCompdate As Date, ByVal sSel As String, ByVal sCost_cente As String, ByVal nUsercode As Integer) As Boolean

        '**-Define the variable llngCount used to keep the vector index on treatment.
        '-Se define la variable llngCount utilizada para almacenar el indice del vector en tratamiento.

        'Dim llngCount As Integer
        Dim lstrBlock_cre As String = ""
        Dim lstrBlock_deb As String = ""
        Dim lclsTab_cost_c As eLedge.Tab_cost_c
		Dim mcolTab_cost_cs As eLedge.Tab_cost_cs
		Dim lclsGeneralForm As Object
		Dim lvntParametersNotes(0) As Object
		
		insUpdTab_cost_c = True
		
		lclsGeneralForm = eRemoteDB.NetHelper.CreateClassInstance("eGeneralForm.Notes")
		lclsTab_cost_c = New eLedge.Tab_cost_c
		mcolTab_cost_cs = New eLedge.Tab_cost_cs
		
		With lclsTab_cost_c
			'.nStatusInstance = eftUpDate
			.nLed_compan = nLed_compan
			.sCost_cente = Trim(sCost_cente)
			.sBlock_cre = Trim(lstrBlock_cre)
			.sBlock_deb = Trim(lstrBlock_deb)
			.sDescript = Trim(sDescript)
			.nNoteNum = nNoteNum
			.sStatregt = Trim(sStatregt)
			.nUsercode = nUsercode
		End With
		
		
		
		'    For llngCount = 0 To lxarDataCP009.Count(1) - 1
		
		If sSel = "1" Then 'Modificar
			If Trim(sBlock_deb) = "0" Then
				lstrBlock_deb = "2" 'Bloquear débitos- sBlock_deb '**Block debits
			Else
				lstrBlock_deb = "1"
			End If
			If Trim(sBlock_cre) = "0" Then
				lstrBlock_cre = "2" 'Bloquear créditos - sBlock_cre '**Block credits
			Else
				lstrBlock_cre = "1"
			End If
			
			'Set lclsTab_cost_c = mcolTab_cost_cs("TCC" & nLed_compan & Trim$(sCost_cente))
			
		ElseIf sSel = "2" Then  'Cortar
			'lvntParametersNotes(0) = lxarDataCP009(llngCount, 7)
			
			If CInt(nNoteNum) <> 0 Then
				If lclsGeneralForm.DeleteNote(nNoteNum) Then
				End If
				'                Call insExecuteQuery("insudb.delNotes", clngUpdate, lvntParametersNotes(), True, False)
			End If
			
			'Set lclsTab_cost_c = mcolTab_cost_cs("TCC" & nLed_compan & Trim$(sCost_cente))
			'lclsTab_cost_c.nStatusInstance = eftDelete
			
		End If
		
		'    Next llngCount
		
		If mcolTab_cost_cs.Update(nAction, sCost_cente, nLed_compan, nNoteNum, lstrBlock_cre, lstrBlock_deb, sDescript, sStatregt, dCompdate, nUsercode) Then
		End If
		
	End Function
End Class






