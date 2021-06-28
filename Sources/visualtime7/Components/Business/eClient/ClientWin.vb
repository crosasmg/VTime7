Option Strict Off
Option Explicit On
Public Class ClientWin
	'%-------------------------------------------------------%'
	'% $Workfile:: ClientWin.cls                            $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 1:30p                                $%'
	'% $Revision:: 16                                       $%'
	'%-------------------------------------------------------%'
	
	'- Variables para almacenar la secuencia de ventanas asociadas al cliente
	Public sV_ConClien As Object
    Public sV_WinClien As Object
    Public lcsClient As Client
    Public Required As Boolean

    Private Enum eActions
		clngActionadd = 301
		clngActionUpdate = 302
		clngActionQuery = 401
	End Enum
	
	'% insLoadTabs: Esta función es la encarga de carga la información necesaria para cada pestaña
	'%              que sera mostrada en la forma.
	Public Function insLoadTabs(ByVal sClient As String, ByVal nAction As Integer, Optional ByVal nUsercode As Integer = 0) As String
		Dim lrecreaTabWinCli_v As eRemoteDB.Execute
		Dim lrecreaClient_win As eRemoteDB.Execute
		Dim lclsSequence As eFunctions.Sequence
        Dim lstrV_conclien As String = ""
        Dim lstrV_winclien As String = ""
        Dim lstrV_ConclienN As String
		Dim lstrV_WinClienN As String
		Dim lintPosition As Integer
		Dim llngImage As eFunctions.Sequence.etypeImageSequence
        Dim lstrAction As String = ""
        On Error GoTo LoadTabs_Err
		
		lclsSequence = New eFunctions.Sequence
		
		insLoadTabs = Space(1024)
		
		lstrV_WinClienN = String.Empty
        lstrV_ConclienN = String.Empty
        Call Fin_Client(sClient)

        '+ Se obtiene el tipo de secuencia (sType_seq) dependiendo de la acción a ejecutar
        Select Case nAction
			Case eActions.clngActionadd
				lstrAction = "1" 'Registro
			Case eActions.clngActionQuery
				lstrAction = "2" 'Consulta
			Case eActions.clngActionUpdate
				lstrAction = "3" 'Modificación
		End Select
		
		
		lrecreaClient_win = New eRemoteDB.Execute
		With lrecreaClient_win
			.StoredProcedure = "reaClient_win"
			.Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", Today, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				lstrV_conclien = .FieldToClass("sV_conClien")
				lstrV_winclien = .FieldToClass("sV_WinClien")
				.RCloseRec()
			End If
		End With
		'UPGRADE_NOTE: Object lrecreaClient_win may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaClient_win = Nothing
		
		insLoadTabs = lclsSequence.makeTable("DMECLI", "Clientes")
		
		lrecreaTabWinCli_v = New eRemoteDB.Execute
		With lrecreaTabWinCli_v
            .StoredProcedure = "REATAB_WINCLI_V"
			.Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sType_Seq", lstrAction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbChar, 3, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				Do While Not .EOF
					
					If nAction = eActions.clngActionQuery Then
						lintPosition = InStr(1, lstrV_winclien, .FieldToClass("sCodispl"))
						lintPosition = IIf(lintPosition = 0, 0, ((lintPosition - 1) / 8) + 1)
						If lintPosition > 0 Then
							If Mid(lstrV_conclien, lintPosition, 1) = "2" Then
								
								'+ Se arma la secuencia de ventanas
								
								llngImage = eFunctions.Sequence.etypeImageSequence.eOK
								insLoadTabs = Trim(insLoadTabs) & lclsSequence.makeRow(.FieldToClass("sCodisp"), .FieldToClass("sCodispl"), nAction, .FieldToClass("sShort_des"), eFunctions.Sequence.etypeImageSequence.eOK,  ,  ,  ,  ,  ,  , .FieldToClass("sDescript"), .FieldToClass("nModules"), .FieldToClass("nWindowTy"))
								
							End If
						End If
						
						'+Se realiza el tratamiento en el caso de actualización o registro
						
					Else
						lstrV_WinClienN = lstrV_WinClienN & Mid(.FieldToClass("sCodispl") & Space(8), 1, 8)
						lintPosition = InStr(1, lstrV_winclien, .FieldToClass("sCodispl"))
						lintPosition = IIf(lintPosition = 0, 0, ((lintPosition - 1) / 8) + 1)
						If lintPosition > 0 Then
                            If Mid(lstrV_conclien, lintPosition, 1) = "2" Then
                                If .FieldToClass("sCodispl") = "BC007P" And (lcsClient.sCRS = "1" Or lcsClient.sUsPerson = "1" Or lcsClient.sPEP = "1") Then
                                    Required = False
                                End If
                                llngImage = eFunctions.Sequence.etypeImageSequence.eOK
                                lstrV_ConclienN = Trim(lstrV_ConclienN) & "2"
                            ElseIf Mid(lstrV_conclien, lintPosition, 1) = "3" Then
                                llngImage = eFunctions.Sequence.etypeImageSequence.eRequired
                                lstrV_ConclienN = Trim(lstrV_ConclienN) & "3"
                            Else
                                lstrV_ConclienN = Trim(lstrV_ConclienN) & "1"
                                If .FieldToClass("sRequire") = "1" Then
                                    llngImage = eFunctions.Sequence.etypeImageSequence.eRequired
                                Else
                                    If .FieldToClass("sCodispl") = "BC007P" And (lcsClient.sCRS = "1" Or lcsClient.sUsPerson = "1" Or lcsClient.sPEP = "1") Then
                                        llngImage = eFunctions.Sequence.etypeImageSequence.eRequired
                                        Required = True
                                    Else
                                        llngImage = eFunctions.Sequence.etypeImageSequence.eEmpty

                                    End If
                                End If
                            End If
                        Else
                            If .FieldToClass("sRequire") = "1" Then
								llngImage = eFunctions.Sequence.etypeImageSequence.eRequired
							Else
								llngImage = eFunctions.Sequence.etypeImageSequence.eEmpty
							End If
							lstrV_ConclienN = Trim(lstrV_ConclienN) & "1"
						End If
						insLoadTabs = Trim(insLoadTabs) & lclsSequence.makeRow(.FieldToClass("sCodisp"), .FieldToClass("sCodispl"), nAction, .FieldToClass("sShort_des"), llngImage,  ,  ,  ,  ,  ,  , .FieldToClass("sDescript"), .FieldToClass("nModules"), .FieldToClass("nWindowTy"))
					End If
					.RNext()
				Loop 
				.RCloseRec()
				
			End If
			
			
		End With
		'UPGRADE_NOTE: Object lrecreaTabWinCli_v may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaTabWinCli_v = Nothing
		insLoadTabs = insLoadTabs & lclsSequence.closeTable()
		
		If nAction <> eActions.clngActionQuery Then
			Call insUpdClient_win(sClient,  ,  , lstrV_WinClienN, lstrV_ConclienN, nUsercode)
		End If
		
LoadTabs_Err: 
		If Err.Number Then
			insLoadTabs = "insLoadTabs: " & Err.Description
		End If
		'UPGRADE_NOTE: Object lclsSequence may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsSequence = Nothing
		On Error GoTo 0
	End Function
	
	'% insUpdClient_win: Esta funcion se encarga de realizar las actualizaciones necesarias al
	'%                   archivo de ventanas de clientes.
	Public Function insUpdClient_win(ByVal sClient As String, Optional ByVal sCodispl As String = "", Optional ByVal sContent As String = "", Optional ByVal sV_WinClien As String = "", Optional ByVal sV_ConClien As String = "", Optional ByVal nUsercode As Integer = 0) As Boolean
        'On Error GoTo insUpdClient_win_err
		Dim lobjClientWin As eRemoteDB.Execute
		lobjClientWin = New eRemoteDB.Execute
        Try

            With lobjClientWin
                .StoredProcedure = "insUpdClient_win"
                .Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("sCodispl", sCodispl, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 8, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("IN_sV_ConClien", sV_ConClien, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("IN_sV_WinClien", sV_WinClien, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 160, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("sContent", sContent, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("IN_dEffecdate", Today, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                insUpdClient_win = .Run(False)
            End With
        Catch ex As Exception
            insUpdClient_win = False
        Finally
            lobjClientWin = Nothing
        End Try

        'UPGRADE_NOTE: Object lobjClientWin may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'

        'insUpdClient_win_err: 
        '		If Err.Number Then
        '			insUpdClient_win = False
        '		End If
        '		On Error GoTo 0
    End Function
	
	'% Find: busqueda de los datos asociados al cliente
	Public Function Find(ByVal sClient As String, ByVal dEffecdate As Date) As Boolean
		Dim lrecreaClient_win As eRemoteDB.Execute
		
		lrecreaClient_win = New eRemoteDB.Execute
		On Error GoTo Find_Err
		
		Find = False
		
		With lrecreaClient_win
			.StoredProcedure = "reaClient_win"
			.Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				Find = True
				sV_ConClien = .FieldToClass("sV_conclien")
				sV_WinClien = .FieldToClass("sV_winclien")
				.RCloseRec()
			End If
		End With
		'UPGRADE_NOTE: Object lrecreaClient_win may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaClient_win = Nothing
		
Find_Err: 
		If Err.Number Then
			Find = False
		End If
		On Error GoTo 0
	End Function

    '% IsPageRequired: Verifica que no existan ventanas requeridas sin información para el
    '%                 el cliente en tratamiento
    Public Function IsPageRequired(ByVal sClient As String, ByVal nMainAction As Integer) As Boolean
        Dim lrecvalRequiredPage As eRemoteDB.Execute
        Dim lstrAction As String = ""
        Call Fin_Client(sClient)
        Call insLoadTabs(sClient, nMainAction)

        lrecvalRequiredPage = New eRemoteDB.Execute

        On Error GoTo IsPageRequired_Err

        IsPageRequired = False

        '+ Se obtiene el tipo de secuencia (sType_seq) dependiendo de la acción a ejecutar
        Select Case nMainAction
            Case eActions.clngActionadd
                lstrAction = "1" 'Registro
            Case eActions.clngActionQuery
                lstrAction = "2" 'Consulta
            Case eActions.clngActionUpdate
                lstrAction = "3" 'Modificación
        End Select

        With lrecvalRequiredPage
            .StoredProcedure = "valRequiredPage"
            .Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sType_seq", lstrAction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If .Run() Then
                If .FieldToClass("IsRequired") = "1" Then
                    IsPageRequired = True
                End If
            End If
        End With
        'UPGRADE_NOTE: Object lrecvalRequiredPage may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        If Required = True Then
            IsPageRequired = True
        End If
        lrecvalRequiredPage = Nothing

IsPageRequired_Err:
        If Err.Number Then
            IsPageRequired = False
        End If
        On Error GoTo 0
    End Function
    Public Sub Fin_Client(ByVal sClient As String)
        lcsClient = New Client
        lcsClient.Find(sClient)
    End Sub

End Class






