Option Strict Off
Option Explicit On
Public Class Tab_Fn_Inst
	'%-------------------------------------------------------%'
	'% $Workfile:: Tab_Fn_Inst.cls                          $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 1:38p                                $%'
	'% $Revision:: 6                                        $%'
	'%-------------------------------------------------------%'
	
	'      Column_name             Type      Length
	'      ------------            -------   ------
	Public nInstitution As Integer 'Number   5
	Public nTypeInstitu As Integer 'Number   5
	Public sName As String 'Char     50
	Public dCompdate As Date 'Date
	Public nUsercode As Integer 'Number   5
	Public sStatregt As String 'Char     1
    Public sClient As String
    Public sDigit As String
    Public sInstitution As String

	'%IsExist: Este metodo retorna VERDADERO o FALSO dependiendo de la existencia o no de registros en la
	'%         tabla "Tab_Fn_Institution"
	Public Function IsExist(ByVal nInstitution As Integer) As Boolean
		Dim lrecreaTab_Fn_Inst As eRemoteDB.Execute
		
		On Error GoTo IsExist_Err
		
		lrecreaTab_Fn_Inst = New eRemoteDB.Execute
		
		With lrecreaTab_Fn_Inst
			.StoredProcedure = "reaTab_Fn_Inst"
			
			.Parameters.Add("nInstitution", nInstitution, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCount", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			.Run(False)
			
			IsExist = .Parameters("nCount").Value > 0
		End With
		
		'UPGRADE_NOTE: Object lrecreaTab_Fn_Inst may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaTab_Fn_Inst = Nothing
		
IsExist_Err: 
		If Err.Number Then
			IsExist = False
		End If
		
		'UPGRADE_NOTE: Object lrecreaTab_Fn_Inst may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaTab_Fn_Inst = Nothing
		
		On Error GoTo 0
	End Function
	
	'%InsUpdTab_Fn_Inst: Actualiza la informacion de la tabla
	Private Function InsUpdTab_Fn_Inst(ByVal nAction As Integer) As Boolean
		Dim lrecinsUpdTab_Fn_Inst As eRemoteDB.Execute
		
		On Error GoTo InsUpdTab_Fn_Inst_Err
		
		lrecinsUpdTab_Fn_Inst = New eRemoteDB.Execute
		
		'+ Definición de store procedure insUpdTab_Fn_Inst al Dec 02, 2002
		
		With lrecinsUpdTab_Fn_Inst
			.StoredProcedure = "insUpdTab_Fn_Inst"
			
			.Parameters.Add("nAction", nAction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nInstitution", nInstitution, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTypeInstitu", nTypeInstitu, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sName", sName, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 50, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sStatregt", sStatregt, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sDigit", sDigit, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sInstitution", sInstitution, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 5, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

			InsUpdTab_Fn_Inst = .Run(False)
		End With
		
		'UPGRADE_NOTE: Object lrecinsUpdTab_Fn_Inst may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecinsUpdTab_Fn_Inst = Nothing
		
InsUpdTab_Fn_Inst_Err: 
		If Err.Number Then
			InsUpdTab_Fn_Inst = False
		End If
		
		'UPGRADE_NOTE: Object lrecinsUpdTab_Fn_Inst may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecinsUpdTab_Fn_Inst = Nothing
		
		On Error GoTo 0
	End Function
	
	'%Add: Esta función agrega registros a la tabla Tab_Fn_Institution
	Public Function Add() As Boolean
		Add = InsUpdTab_Fn_Inst(1)
	End Function
	
	'%Update: Esta función actualiza registros en la tabla Tab_Fn_Institution
	Public Function Update() As Boolean
		Update = InsUpdTab_Fn_Inst(2)
	End Function
	
	'%Delete: Esta función elimina registros de la tabla TAB_FN_INSTITUTION
	Public Function Delete() As Boolean
		Delete = InsUpdTab_Fn_Inst(3)
	End Function
	
	'%InsValMS7000: Esta función se encarga de validar los datos introducidos en la zona de detalle
    Public Function InsValMS7000(ByVal sCodispl As String, ByVal sAction As String, ByVal nInstitution As Integer, ByVal nTypeInstitu As Integer, ByVal sName As String, ByVal sStatregt As String) As String ', ByVal sClient As String, ByVal sDigit As String) As String
        Dim lclsErrors As eFunctions.Errors

        On Error GoTo InsValMS7000_Err

        lclsErrors = New eFunctions.Errors

        With lclsErrors

            '+ Se valida la columna: nInstitution

            If nInstitution = eRemoteDB.Constants.intNull Then
                .ErrorMessage(sCodispl, 70055)
            ElseIf nInstitution <= 0 Then
                .ErrorMessage(sCodispl, 70104)
            ElseIf sAction = "Add" Then
                If IsExist(nInstitution) Then
                    .ErrorMessage(sCodispl, 70103)
                End If
            End If

            '+ Se valida la columna: sName

            If sName = String.Empty Then
                .ErrorMessage(sCodispl, 70056)
            End If

            '+ Se valida la columna: nTypeInstitu

            If nTypeInstitu <= 0 Or nTypeInstitu = eRemoteDB.Constants.intNull Then
                .ErrorMessage(sCodispl, 70057)
            End If

            '+ Se valida la columna: sStatregt

            If Trim(sStatregt) = String.Empty Or Trim(sStatregt) = "0" Then
                .ErrorMessage(sCodispl, 70058)
            End If

            '+ Se valida la columna: sClient
            'If sClient = String.Empty Then
            '    .ErrorMessage(sCodispl, 2792)
            'Else
            '    '+ Se valida la columna: sDigit
            '    If sDigit = String.Empty Then
            '        .ErrorMessage(sCodispl, 2090)
            '    End If
            'End If

            'If sClient <> String.Empty And _
            '   sDigit <> String.Empty Then
            '    If IsExistClient(sClient, sDigit, nInstitution) Then
            '        .ErrorMessage(sCodispl, 80511)
            '    End If
            'End If

            InsValMS7000 = lclsErrors.Confirm
        End With

        'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsErrors = Nothing

InsValMS7000_Err:
        If Err.Number Then
            InsValMS7000 = "InsValMS7000: " & Err.Description
        End If

        'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsErrors = Nothing

        On Error GoTo 0
    End Function
	
	'% InsPostMS7000Upd: Esta función se encarga de crear/actualizar los registros
	'%                   correspondientes en la tabla TAB_FN_INSTITUTION
    Public Function InsPostMS7000Upd(ByVal sAction As String, ByVal nInstitution As Integer, ByVal nTypeInstitu As Integer, ByVal sName As String, ByVal sStatregt As String, ByVal nUsercode As Integer, ByVal sClient As String, ByVal sDigit As String, ByVal sInstitution As String) As Boolean

        On Error GoTo InsPostMS7000Upd_Err

        With Me
            .nInstitution = nInstitution
            .nTypeInstitu = nTypeInstitu
            .sName = sName
            .sStatregt = sStatregt
            .nUsercode = nUsercode
            .sClient = sClient
            .sDigit = sDigit
            .sInstitution = sInstitution

            InsPostMS7000Upd = True

            Select Case sAction

                '+ Si la opción seleccionada es Registrar

                Case "Add"
                    InsPostMS7000Upd = .Add()

                    '+ Si la opción seleccionada es Modificar

                Case "Update"
                    InsPostMS7000Upd = .Update()

                    '+ Si la opción seleccionada es Eliminar

                Case "Del"
                    InsPostMS7000Upd = .Delete()
            End Select
        End With

InsPostMS7000Upd_Err:
        If Err.Number Then
            InsPostMS7000Upd = False
        End If

        On Error GoTo 0
    End Function
    '%IsExistClient: Este metodo retorna VERDADERO o FALSO dependiendo de la existencia o no de registros en la
    '%               tabla "Tab_Fn_Institution"
    '-------------------------------------------------------------------------------------------------
    Public Function IsExistClient(ByVal sClient As String, _
                                  ByVal sDigit As String, _
                                  ByVal nInstitution As Long) As Boolean
        '-------------------------------------------------------------------------------------------------
        Dim lrecreaTab_Fn_Inst As eRemoteDB.Execute

        On Error GoTo IsExistClient_Err

        lrecreaTab_Fn_Inst = New eRemoteDB.Execute

        With lrecreaTab_Fn_Inst
            .StoredProcedure = "reaTab_Fn_Inst_2"

            .Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sDigit", sDigit, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nInstitution", nInstitution, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCount", nInstitution, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Run(False)

            IsExistClient = .Parameters("nCount").Value > 0
        End With

        lrecreaTab_Fn_Inst = Nothing

IsExistClient_Err:
        If Err.Number Then
            IsExistClient = False
        End If

        lrecreaTab_Fn_Inst = Nothing

        On Error GoTo 0
    End Function

End Class






