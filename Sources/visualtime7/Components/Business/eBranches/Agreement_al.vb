Option Strict Off
Option Explicit On
Public Class Agreement_al
	'%-------------------------------------------------------%'
	'% $Workfile:: Agreement_al.cls                         $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 1:38p                                $%'
	'% $Revision:: 23                                       $%'
	'%-------------------------------------------------------%'
	
	'*-Propiedades según la tabla en el sistema el 27/12/2000
	'Column_Name                   Type          Length  Prec    Scale   Nullable
	'-------------------------   --------------- ------ -------- ------- ---------
	Public nAgreement As Integer ' NUMBER          22     5        0       No
	Public sDescript As String ' CHAR            30                      No
	Public dStartDate As Date ' DATE             7                      No
	Public dNulldate As Date ' DATE             7                      Yes
	Public sLevelint As String ' CHAR             1                      No
	Public sStatregt As String ' CHAR             1                      No
    Public nUsercode As Integer ' NUMBER          22     5        0       No

    Public nAgree_Type As Integer ' NUMBER          22     5        0       No
    Public nIntermed As Integer ' NUMBER          22     5        0       No


	
	'-Variables auxiliares
	Public WithInformation As String
	
	'-Nivel de asociados del convenio para los intermediarios
	Public Enum LevelIntType
        levelIntByIntermType = 1 ' Por tipo de intermediario
        levelIntByInterm = 2 ' Por intermediario
	End Enum
	
	'%InsUpdAgreement_al: Realiza la actualización de la tabla
	Private Function InsUpdAgreement_al(ByVal nAction As Integer) As Boolean
		Dim lrecInsUpdAgreement_al As eRemoteDB.Execute
		
		On Error GoTo InsUpdAgreement_al_Err
		
		lrecInsUpdAgreement_al = New eRemoteDB.Execute
		
		'+Definición de parámetros para stored procedure 'InsUpdAgreement_al'
		'+Información leída el 19/10/01
		With lrecInsUpdAgreement_al
			.StoredProcedure = "InsUpdAgreement_al"
			.Parameters.Add("nAction", nAction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 38, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAgreement", nAgreement, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sDescript", sDescript, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 30, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dStartdate", dStartDate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dNulldate", dNulldate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sLevelint", sLevelint, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sStatregt", sStatregt, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nAgree_Type", nAgree_Type, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nIntermed", nIntermed, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			InsUpdAgreement_al = .Run(False)
		End With
		
InsUpdAgreement_al_Err: 
		If Err.Number Then
			InsUpdAgreement_al = False
		End If
		'UPGRADE_NOTE: Object lrecInsUpdAgreement_al may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecInsUpdAgreement_al = Nothing
		On Error GoTo 0
	End Function
	
	'%Add: Crea un registro en la tabla
	Public Function Add() As Boolean
		Add = InsUpdAgreement_al(1)
	End Function
	
	'%Update: Actualiza los datos de la tabla
	Public Function Update() As Boolean
		Update = InsUpdAgreement_al(2)
	End Function
	
	'%Delete: Borra los datos de la tabla
	Public Function Delete() As Boolean
		Delete = InsUpdAgreement_al(3)
	End Function
	
	'%Find: Lee los datos de la tabla
	Public Function Find(ByVal nAgreement As Integer, Optional ByVal bFind As Boolean = False) As Boolean
		Dim lrecReaAgreement_al As eRemoteDB.Execute
		
		On Error GoTo Find_Err
		If Me.nAgreement <> nAgreement Or bFind Then
			'+Definición de parámetros para stored procedure 'ReaAgreement_al'
			'+Información leída el 19/10/01
			lrecReaAgreement_al = New eRemoteDB.Execute
			With lrecReaAgreement_al
				.StoredProcedure = "ReaAgreement_al"
				.Parameters.Add("nAgreement", nAgreement, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				If .Run Then
					Find = True
					Me.nAgreement = nAgreement
					sDescript = .FieldToClass("sDescript")
					dStartDate = .FieldToClass("dStartdate")
					dNulldate = .FieldToClass("dNulldate")
					sLevelint = .FieldToClass("sLevelint")
                    sStatregt = .FieldToClass("sStatregt")
                    nAgree_Type = .FieldToClass("nAgree_Type")
                    nIntermed = .FieldToClass("nIntermed")
				End If
			End With
		Else
			Find = True
		End If
		
Find_Err: 
		If Err.Number Then
			Find = False
		End If
		'UPGRADE_NOTE: Object lrecReaAgreement_al may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecReaAgreement_al = Nothing
		On Error GoTo 0
	End Function
	
	'%IsExist: Valida si un registro existe en la tabla
	Public Function IsExist(ByVal nAgreement As Integer) As Boolean
		Dim lrecReaAgreement_al As eRemoteDB.Execute
		
		On Error GoTo IsExist_Err
		'+Definición de parámetros para stored procedure 'ReaAgreement_al_v'
		'+Información leída el 19/10/01
		lrecReaAgreement_al = New eRemoteDB.Execute
		With lrecReaAgreement_al
			.StoredProcedure = "ReaAgreement_al_v"
			.Parameters.Add("nAgreement", nAgreement, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nExist", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				IsExist = .Parameters("nExist").Value = "1"
			End If
		End With
IsExist_Err: 
		If Err.Number Then
			IsExist = False
		End If
		'UPGRADE_NOTE: Object lrecReaAgreement_al may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecReaAgreement_al = Nothing
		On Error GoTo 0
	End Function
	
	'%LoadTabs: Arma la secuencia para los convenios de VidActiva
	Public Function LoadTabs(ByVal nAction As Integer, ByVal nAgreement As Integer, ByVal sUserSchema As String) As Object
        Dim lrecWindows As eRemoteDB.Query = Nothing
        Dim lclsSecurSche As eSecurity.Secur_sche = Nothing
        Dim lintPageImage As eFunctions.Sequence.etypeImageSequence
        Dim lclsSequence As eFunctions.Sequence = Nothing
        Dim lintCountWindows As Integer = 0
        Dim lstrCodisp As String = String.Empty
        Dim lstrCodispl As String = String.Empty
        Dim lstrShort_desc As String = String.Empty
        Dim lblnContent As Boolean = False
        Dim lstrHTMLCode As String = String.Empty
        Dim lblnAddWindows As Boolean = False
		'-Constante que guarda el conjunto de ventanas de la secuencia
		Const CN_WINDOWS As String = "MVA646A MVA646B MVA646C MVA646D "
		
		
		On Error GoTo LoadTabs_Err
		
		lclsSecurSche = New eSecurity.Secur_sche
		lclsSequence = New eFunctions.Sequence
		lrecWindows = New eRemoteDB.Query
		
		lstrHTMLCode = String.Empty
		
		Call ValContent(nAgreement)
		lstrHTMLCode = lclsSequence.makeTable
		lintCountWindows = 1
		lstrCodispl = Trim(Mid(CN_WINDOWS, lintCountWindows, 8))
		'+ Se asigna el valor a la variable que indica como requerida
		Do While lstrCodispl <> String.Empty
			'+ Se asigna el valor a la variable que indica si la ventana se agrega a la secuencia
			'+La ventana se agrega si el convenio es por tipo de intermediario
			If lstrCodispl = "MVA646B" Then
				lblnAddWindows = (sLevelint = "1" Or sLevelint = String.Empty)
				
				'+La ventana se agrega si el convenio es por intermediario
			ElseIf lstrCodispl = "MVA646C" Then 
				lblnAddWindows = sLevelint = "2"
				
			Else
				lblnAddWindows = True
			End If
			
			If lblnAddWindows Then
				'+ Se asignan los valores a las variables de contenido
				lblnContent = InStr(1, WithInformation, lstrCodispl) <> 0
				
				'+ Se asignan los valores a las variables de descripcion
				With lrecWindows
					If .OpenQuery("windows", "sCodisp, sShort_des", "scodispl='" & lstrCodispl & "'") Then
						lstrCodisp = .FieldToClass("sCodisp")
						lstrShort_desc = .FieldToClass("sShort_des")
						.CloseQuery()
					End If
				End With
				
				'+ Se busca la imagen a colocar en los links
				With lclsSecurSche
					If Not .valTransAccess(sUserSchema, lstrCodisp, "1") Then
						If lblnContent Then
							lintPageImage = eFunctions.Sequence.etypeImageSequence.eDeniedOK
						Else
							lintPageImage = eFunctions.Sequence.etypeImageSequence.eDeniedReq
						End If
					Else
						If Not lblnContent Then
							lintPageImage = eFunctions.Sequence.etypeImageSequence.eRequired
						Else
							lintPageImage = eFunctions.Sequence.etypeImageSequence.eOK
						End If
					End If
				End With
				
				lstrHTMLCode = lstrHTMLCode & lclsSequence.makeRow(lstrCodisp, lstrCodispl, nAction, lstrShort_desc, lintPageImage)
			End If
			
			'+ Se mueve al siguiente registro encontrado
			lintCountWindows = lintCountWindows + 8
			lstrCodispl = Trim(Mid(CN_WINDOWS, lintCountWindows, 8))
		Loop 
		lstrHTMLCode = lstrHTMLCode & lclsSequence.closeTable()
		
		LoadTabs = lstrHTMLCode
		
LoadTabs_Err: 
		If Err.Number Then
			LoadTabs = "LoadTabs: " & Err.Description
		End If
		'UPGRADE_NOTE: Object lclsSecurSche may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsSecurSche = Nothing
		'UPGRADE_NOTE: Object lrecWindows may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecWindows = Nothing
		'UPGRADE_NOTE: Object lclsSequence may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsSequence = Nothing
		On Error GoTo 0
	End Function
	
	'%ValContent: Obtiene las ventanas requeridas para los convenios de VidActiva
	Public Function ValContent(ByVal nAgreement As Integer) As Boolean
		Dim lrecinsValContent_Cover As eRemoteDB.Execute
		
		On Error GoTo ValContent_Err
		ValContent = False
		'+Definición de parámetros para stored procedure 'insValRequired_Agreement_al'
		lrecinsValContent_Cover = New eRemoteDB.Execute
		With lrecinsValContent_Cover
			.StoredProcedure = "insValRequired_Agreement_al"
			.Parameters.Add("nAgreement", nAgreement, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sWithInformation", String.Empty, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 255, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sLevelint", String.Empty, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			ValContent = .Run(False)
			Me.WithInformation = .Parameters("sWithInformation").Value
			Me.sLevelint = Trim(.Parameters("sLevelint").Value)
		End With
		
ValContent_Err: 
		If Err.Number Then
			ValContent = False
		End If
		'UPGRADE_NOTE: Object lrecinsValContent_Cover may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecinsValContent_Cover = Nothing
		On Error GoTo 0
	End Function
	
	'% InsValMVA646_K: Valida los datos de la MVA646_K(Administración de convenios)
	Public Function InsValMVA646_K(ByVal sCodispl As String, ByVal nAction As Integer, ByVal nAgreement As Integer) As String
		Dim lclsErrors As eFunctions.Errors
		Dim lblnExist As Boolean
		
		On Error GoTo InsValMVA646_K_Err
		lclsErrors = New eFunctions.Errors
		With lclsErrors
			'+Se valida el campo Convenio
			If nAgreement = eRemoteDB.Constants.intNull Then
				.ErrorMessage(sCodispl, 55585)
			Else
				lblnExist = IsExist(nAgreement)
				Select Case nAction
					Case eFunctions.Menues.TypeActions.clngActionadd
						If lblnExist Then
							.ErrorMessage(sCodispl, 55586)
						End If
						
					Case eFunctions.Menues.TypeActions.clngActionUpdate, eFunctions.Menues.TypeActions.clngActionQuery
						If Not lblnExist Then
							.ErrorMessage(sCodispl, 55567)
						End If
				End Select
			End If
			InsValMVA646_K = .Confirm
		End With
InsValMVA646_K_Err: 
		If Err.Number Then
			InsValMVA646_K = "InsValMVA646_K: " & Err.Description
		End If
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
		On Error GoTo 0
	End Function
	
	'% InsPostMVA646_K: MVA646_K(Administración de convenios)
	Public Function InsPostMVA646_K(ByVal nAction As Integer, ByVal nAgreement As Integer, ByVal nUsercode As Integer) As Boolean
		
		On Error GoTo InsPostMVA646_K_Err
		
		InsPostMVA646_K = True
		If nAction = eFunctions.Menues.TypeActions.clngActionadd Then
            InsPostMVA646_K = InsPostMVA646A(nAction, nAgreement, " ", Today, dtmNull, "1", "2", nUsercode)
		End If
		
InsPostMVA646_K_Err: 
		If Err.Number Then
			InsPostMVA646_K = False
		End If
	End Function
	
    '%InsValMVA646A: Validaciones de la transacción
    Public Function InsValMVA646A(ByVal sCodispl As String, ByVal nAction As Integer, ByVal sDescript As String, ByVal dStartDate As Date, ByVal dNulldate As Date, ByVal nAgree_Type As Integer, ByVal nIntermed As Integer) As String
        Dim lclsErrors As eFunctions.Errors

        On Error GoTo InsValMVA646A_Err
        InsValMVA646A = String.Empty

        If nAction = eFunctions.Menues.TypeActions.clngActionadd Or nAction = eFunctions.Menues.TypeActions.clngActionUpdate Then
            lclsErrors = New eFunctions.Errors

            With lclsErrors
                '+Se valida el campo Descripción
                If Trim(sDescript) = String.Empty Then
                    .ErrorMessage(sCodispl, 55587)
                End If

                '+Se valida el campo Inicio de Vigencia
                If dStartDate = dtmNull Then
                    .ErrorMessage(sCodispl, 55588)
                End If

                '+Se valida si es Tipo convenio es SOAP ELECRÓNICO el código de intermediario debe tener valor
                If nAgree_Type = 1 And nIntermed = intNull Then
                    .ErrorMessage(sCodispl, 55591)
                End If

                InsValMVA646A = .Confirm
            End With
        End If

InsValMVA646A_Err:
        If Err.Number Then
            InsValMVA646A = "InsValMVA646: " & Err.Description
        End If
        'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsErrors = Nothing
        On Error GoTo 0
    End Function
	
	'%InsPostMVA646A: Ejecuta el post de la transacción
	'%                MVA646A(Datos generales del convenio)
    Public Function InsPostMVA646A(ByVal nAction As Integer, ByVal nAgreement As Integer, ByVal sDescript As String, ByVal dStartDate As Date, ByVal dNulldate As Date, ByVal sLevelint As String, ByVal sStatregt As String, ByVal nUsercode As Integer, Optional ByVal nAgree_Type As Integer = intNull, Optional ByVal nIntermed As Integer = intNull) As Boolean

        On Error GoTo InsPostMVA646A_Err

        InsPostMVA646A = True
        If nAction = eFunctions.Menues.TypeActions.clngActionadd Or nAction = eFunctions.Menues.TypeActions.clngActionUpdate Then
            With Me
                .nAgreement = nAgreement
                .sDescript = sDescript
                .dStartDate = dStartDate
                .dNulldate = dNulldate
                .sLevelint = sLevelint
                .sStatregt = sStatregt
                .nUsercode = nUsercode
                .nAgree_Type = nAgree_Type
                .nIntermed = nIntermed

                If nAction = eFunctions.Menues.TypeActions.clngActionadd Then
                    InsPostMVA646A = .Add
                Else
                    If sStatregt = "1" Then
                        .dNulldate = dtmNull
                    ElseIf sStatregt = "3" And .dNulldate = dtmNull Then
                        .dNulldate = Today
                    End If
                    InsPostMVA646A = .Update
                End If
            End With
        End If

InsPostMVA646A_Err:
        If Err.Number Then
            InsPostMVA646A = False
        End If
    End Function
	
	'%Class_Initialize: Inicializa todas las propiedades
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		nAgreement = eRemoteDB.Constants.intNull
		sDescript = String.Empty
		dStartDate = dtmNull
		dNulldate = dtmNull
		sLevelint = String.Empty
		sStatregt = String.Empty
        nUsercode = eRemoteDB.Constants.intNull
        nAgree_Type = eRemoteDB.Constants.intNull
        nIntermed = eRemoteDB.Constants.intNull

		WithInformation = String.Empty
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
End Class






