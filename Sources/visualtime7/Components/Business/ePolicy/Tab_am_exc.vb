Option Strict Off
Option Explicit On
Public Class Tab_am_exc
	'%-------------------------------------------------------%'
	'% $Workfile:: Tab_am_exc.cls                           $%'
	'% $Author:: Nvaplat41                                  $%'
	'% $Date:: 9/10/03 19.02                                $%'
	'% $Revision:: 27                                       $%'
	'%-------------------------------------------------------%'
	
	'**- Properties according to the table in the system on November 09,2000
	'- Propiedades según la tabla en el sistema 09/11/2000
	
	'Column_name                                    Type        Computed  Length  Prec  Scale Nullable                          TrimTrailingBlanks                  FixedLenNullInSource
	Public sCertype As String 'char       no        1                    no                                   no                                  no
	Public nBranch As Integer 'smallint   no        2        5     0     no                                  (n/a)                               (n/a)
	Public nProduct As Integer 'smallint   no        2        5     0     no                                  (n/a)                               (n/a)
	Public nPolicy As Double 'int        no        4        10    0     no                                  (n/a)                               (n/a)
	Public nCertif As Double 'int        no        4        10    0     no                                  (n/a)                               (n/a)
	Public nTariff As Integer 'smallint   no        2        5     0     no                                  (n/a)                               (n/a)
	Public sIllness As String 'char       no        8                    no                                   no                                  no
	Public dEffecdate As Date 'datetime   no        8                    no                                  (n/a)                               (n/a)
	Public sClient As String 'char       no        14                   no                                   no                                  no
	Public nExc_code As Integer 'smallint   no        2        5     0     yes                                 (n/a)                               (n/a)
	Public dInit_date As Date 'datetime   no        8                    yes                                 (n/a)                               (n/a)
	Public dNulldate As Date 'datetime   no        8                    yes                                 (n/a)                               (n/a)
	Public dEnd_date As Date 'datetime   no        8                    yes                                 (n/a)                               (n/a)
	Public nId As Integer 'int        no        4        10    0     no                                  (n/a)                               (n/a)
	Private mlngUsercode As Integer 'smallint   no        2        5     0     no                                  (n/a)                               (n/a)
    Public sType_exc As String
    Public nModulec As Integer
    Public nCover As Integer
	
	'**- Auxiliary properties
	'- Propiedades Auxliliares
	Private mlngTransaction As Integer
	
	'%InsUpdTab_am_exc: Realiza la actualización de la tabla
	Private Function InsUpdTab_am_exc(ByVal nAction As Integer) As Boolean
		Dim lrecInsUpdTab_am_exc As eRemoteDB.Execute
		
		On Error GoTo InsUpdTab_am_exc_Err
		lrecInsUpdTab_am_exc = New eRemoteDB.Execute
		'+ Definición de store procedure InsUpdTab_am_exc al 09-30-2002 17:48:33
		With lrecInsUpdTab_am_exc
			.StoredProcedure = "InsUpdTab_am_exc"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTariff", IIf(sType_exc = "1", nTariff, 0), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sIllness", sIllness, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 8, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nExc_code", nExc_code, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dInit_date", dInit_date, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dNulldate", dNulldate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", mlngUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEnd_date", dEnd_date, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sType_exc", sType_exc, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nId", nId, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAction", nAction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTransaction", mlngTransaction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCommit", 1, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nModulec", nModulec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCover", nCover, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            InsUpdTab_am_exc = .Run(False)
		End With
		
InsUpdTab_am_exc_Err: 
		If Err.Number Then
			InsUpdTab_am_exc = False
		End If
		'UPGRADE_NOTE: Object lrecInsUpdTab_am_exc may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecInsUpdTab_am_exc = Nothing
		On Error GoTo 0
	End Function
	
	'%Add: Crea un registro en la tabla
	Public Function Add() As Boolean
		Add = InsUpdTab_am_exc(1)
	End Function
	
	'%Update: Actualiza los datos de la tabla
	Public Function Update() As Boolean
		Update = InsUpdTab_am_exc(2)
	End Function
	
	'%Delete: Borra los datos de la tabla
	Public Function Delete() As Boolean
		Delete = InsUpdTab_am_exc(3)
	End Function
	
	'*sContent: Obtiene el indicador de contenido de la transacción
	Public ReadOnly Property sContent() As String
		Get
			sContent = mstrContent
		End Get
	End Property
	
	'%IsExist: Esta rutina es la encargada de evitar registros duplicados.
	Public Function IsExist(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal nTariff As Integer, ByVal dEffedate As Date, Optional ByVal sClient As String = "", Optional ByVal sIllness As String = "") As Boolean
		Dim lrecReaTab_am_exc_v As eRemoteDB.Execute
		
		On Error GoTo IsExist_Err
		lrecReaTab_am_exc_v = New eRemoteDB.Execute
		With lrecReaTab_am_exc_v
			.StoredProcedure = "ReaTab_am_exc_v"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTariff", IIf(nTariff = eRemoteDB.Constants.intNull, 0, nTariff), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sIllness", sIllness, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 8, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffedate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCount", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				IsExist = .Parameters("nCount").Value > 0
			End If
		End With
		
IsExist_Err: 
		If Err.Number Then
			IsExist = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecReaTab_am_exc_v may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecReaTab_am_exc_v = Nothing
	End Function
	
	'%InsValAM006Upd: valida la información almacenada en la ventana AM006
	Public Function InsValAM006Upd(ByVal sCodispl As String, ByVal sAction As String, ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal nTariff As Integer, ByVal dEffecdate As Date, ByVal sClient As String, ByVal sIllness As String, ByVal nExc_code As Integer, ByVal dInitDate As Date, ByVal dEnd_date As Date, ByVal dStartdate As Date, ByVal dExpirdat As Date, ByVal sOptExc As String, ByVal sType_exc As String) As String
		Dim lclsErrors As eFunctions.Errors
		Dim lclsRoles As ePolicy.Roles
		Dim lblnError As Boolean
		
		On Error GoTo InsValAM006Upd_Err
		lclsErrors = New eFunctions.Errors
		With lclsErrors
			
			'+ Validación del campo: Tarifa. si es poliza matriz y colectiva
			'+ este campo esta en 0
			If nTariff = eRemoteDB.Constants.intNull And sType_exc = "1" Then
				.ErrorMessage("AM006", 10117)
			End If
			
			'+Validación del campo "Asegurado".
			If sClient <> String.Empty Then
				lclsRoles = New ePolicy.Roles
				If Not lclsRoles.valExistsRoles(sCertype, nBranch, nProduct, nPolicy, nCertif, eRemoteDB.Constants.intNull, sClient, dEffecdate) Then
					.ErrorMessage(sCodispl, 3606)
				End If
				'UPGRADE_NOTE: Object lclsRoles may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
				lclsRoles = Nothing
			Else
				If nCertif > 0 And sOptExc = "2" And sClient = String.Empty Then
					.ErrorMessage(sCodispl, 3605)
				End If
			End If
			
			'+Validación del campo "Código de la enfermedad ".
			If sIllness = String.Empty Then
				.ErrorMessage(sCodispl, 4230)
			Else
				If sAction = "Add" Then
					If IsExist(sCertype, nBranch, nProduct, nPolicy, nCertif, nTariff, dEffecdate, sClient, sIllness) Then
						.ErrorMessage(sCodispl, 10199)
					End If
				End If
				
				'+ Validación de la causa de exclusión de la enfermedad
				If nExc_code = eRemoteDB.Constants.intNull Then
					.ErrorMessage(sCodispl, 3978)
				End If
				
				'+ Validación de la fecha de inicio de exclusión de la enfermedad
				If dInitDate = eRemoteDB.Constants.dtmNull Then
					.ErrorMessage(sCodispl, 3565)
				Else
					If dInitDate < dStartdate And dInitDate <> eRemoteDB.Constants.dtmNull And sType_exc = "1" Then
						.ErrorMessage(sCodispl, 3566,  , eFunctions.Errors.TextAlign.RigthAling, " (" & CStr(dStartdate) & ")")
					End If
				End If
			End If
			
			
			'+ En caso que sea preexistencia la fecha debe ser menor a la fecha de inicio de vigencia
			If (dInitDate <> eRemoteDB.Constants.dtmNull And sType_exc = "2") Then
				If (dInitDate > dStartdate) Then
					.ErrorMessage(sCodispl, 100115,  , eFunctions.Errors.TextAlign.RigthAling, " (" & CStr(dStartdate) & ")")
				End If
			End If
			
			'+ Validación de la fecha fin de exclusión de la enfermedad
			If dEnd_date <> eRemoteDB.Constants.dtmNull Then
				If dExpirdat <> eRemoteDB.Constants.dtmNull Then
					If dEnd_date > dExpirdat Then
						.ErrorMessage(sCodispl, 11424,  , eFunctions.Errors.TextAlign.RigthAling, " (" & dStartdate & "-" & dExpirdat & ")")
					End If
				End If
				If dEnd_date < dInitDate And dInitDate <> eRemoteDB.Constants.dtmNull Then
					.ErrorMessage(sCodispl, 11425)
				End If
			End If
			
			InsValAM006Upd = .Confirm
		End With
		
InsValAM006Upd_Err: 
		If Err.Number Then
			InsValAM006Upd = "InsValAM006Upd: " & Err.Description
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
		'UPGRADE_NOTE: Object lclsRoles may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsRoles = Nothing
	End Function
	
	'%InsValAM006: valida la información almacenada en la ventana AM006
	Public Function InsValAM006(ByVal sCodispl As String, ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date) As String
		Dim lclsErrors As eFunctions.Errors
		On Error GoTo InsValAM006_Err
		
		If Not IsExist(sCertype, nBranch, nProduct, nPolicy, nCertif, eRemoteDB.Constants.intNull, dEffecdate) Then
			lclsErrors = New eFunctions.Errors
			lclsErrors.ErrorMessage(sCodispl, 1928)
			InsValAM006 = lclsErrors.Confirm
		End If
		
InsValAM006_Err: 
		If Err.Number Then
			InsValAM006 = "InsValAM006: " & Err.Description
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
	End Function
	
	'%InsPostAM006Upd: realiza las actualizaciones par ala ventana AM006 en las respectivas tablas
    Public Function InsPostAM006Upd(ByVal sAction As String, ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal nTariff As Integer, ByVal sIllness As String, ByVal sClient As String, ByVal nId As Integer, ByVal nUsercode As Integer, ByVal dEffecdate As Date, ByVal nTransaction As Integer, ByVal sType_exc As String, Optional ByVal nCount As Integer = 0, Optional ByVal dNulldate As Date = eRemoteDB.Constants.dtmNull, Optional ByVal nExc_code As Integer = eRemoteDB.Constants.intNull, Optional ByVal dInit_date As Date = eRemoteDB.Constants.dtmNull, Optional ByVal dEnd_date As Date = eRemoteDB.Constants.dtmNull, Optional ByVal nModulec As Integer = eRemoteDB.Constants.intNull, Optional ByVal nCover As Integer = eRemoteDB.Constants.intNull) As Boolean
        Dim lclsPolicy_Win As ePolicy.Policy_Win
        Dim lblnUpdPw As Boolean

        On Error GoTo InsPostAM006Upd_Err
        mstrContent = String.Empty
        With Me
            .sCertype = sCertype
            .nBranch = nBranch
            .nProduct = nProduct
            .nPolicy = nPolicy
            .nCertif = nCertif
            .nTariff = nTariff
            .sIllness = sIllness
            .dEffecdate = dEffecdate
            .sClient = sClient
            .nExc_code = nExc_code
            .dInit_date = dInit_date
            .dNulldate = dNulldate
            .nId = nId
            .sType_exc = sType_exc
            .nModulec = nModulec
            .nCover = nCover

            mlngUsercode = nUsercode

            .dEnd_date = dEnd_date
            mlngTransaction = nTransaction
            Select Case sAction
                Case "Add"
                    If .Add Then
                        InsPostAM006Upd = True
                        If nCount <= 0 Then
                            mstrContent = "2"
                            lblnUpdPw = True
                        End If
                    End If

                Case "Update"
                    InsPostAM006Upd = .Update

                Case "Delete"
                    If .Delete Then
                        InsPostAM006Upd = True
                        If Not IsExist(sCertype, nBranch, nProduct, nPolicy, nCertif, nTariff, dEffecdate) Then
                            mstrContent = "1"
                            lblnUpdPw = True
                        End If
                    End If
            End Select
        End With

        If lblnUpdPw Then
            lclsPolicy_Win = New ePolicy.Policy_Win
            Call lclsPolicy_Win.Add_PolicyWin(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate, nUsercode, "AM006", mstrContent)
        End If

InsPostAM006Upd_Err:
        If Err.Number Then
            InsPostAM006Upd = False
        End If
        On Error GoTo 0
        'UPGRADE_NOTE: Object lclsPolicy_Win may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsPolicy_Win = Nothing
    End Function
	
	'%GeTTab_am_exc_by_prod: Permite crear las enfermedades excluídas a partir de las del diseñador.
	Public Function GeTTab_am_exc_by_prod(ByVal sCodispl As String, ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal nTariff As Integer, ByVal sClient As String, ByVal dEffecdate As Date, ByVal nUsercode As Integer, ByVal dNulldate As Date, ByVal nTransaction As Integer) As Boolean
		Dim lclsPolicy_Win As Policy_Win
		
		On Error GoTo GeTTab_am_exc_by_prod_Err
		
		If insReaTab_am_Exc(sCertype, nBranch, nProduct, nPolicy, nCertif, nTariff, sClient, dEffecdate, nUsercode, dNulldate, nTransaction) Then
			lclsPolicy_Win = New Policy_Win
			GeTTab_am_exc_by_prod = lclsPolicy_Win.Add_PolicyWin(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate, nUsercode, sCodispl, "2")
			'UPGRADE_NOTE: Object lclsPolicy_Win may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
			lclsPolicy_Win = Nothing
		End If
		
GeTTab_am_exc_by_prod_Err: 
		If Err.Number Then
			GeTTab_am_exc_by_prod = False
		End If
		On Error GoTo 0
	End Function
	
	'%insReaTab_am_Exc: Permite crear las enfermedades excluídas a partir de las del diseñador.
	Private Function insReaTab_am_Exc(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal nTariff As Integer, ByVal sClient As String, ByVal dEffecdate As Date, ByVal nUsercode As Integer, ByVal dNulldate As Date, ByVal nTransaction As Integer) As Boolean
		Dim lrecReaTab_am_Exc As eRemoteDB.Execute
		
		On Error GoTo insReaTab_am_Exc_Err
		lrecReaTab_am_Exc = New eRemoteDB.Execute
		With lrecReaTab_am_Exc
			.StoredProcedure = "insReaTab_am_Exc"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTariff", nTariff, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dNulldate", dNulldate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTransaction", nTransaction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCount", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Call .Run(False)
			insReaTab_am_Exc = .Parameters("nCount").Value > 0
		End With
		
insReaTab_am_Exc_Err: 
		If Err.Number Then
			insReaTab_am_Exc = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecReaTab_am_Exc may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecReaTab_am_Exc = Nothing
	End Function
	
	'%FindDeftValues: Obtiene los valores asociados a la tarifa a mostrar por defecto.
	Public Function FindDeftValues(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, Optional ByVal bFind As Boolean = False) As Boolean
		Dim lrecTab_am_Exc As eRemoteDB.Execute
		
		On Error GoTo FindDeftValues_Err
		If sCertype <> Me.sCertype Or nBranch <> Me.nBranch Or nProduct <> Me.nProduct Or nPolicy <> Me.nPolicy Or nCertif <> Me.nCertif Or dEffecdate <> Me.dEffecdate Or bFind Then
			
			lrecTab_am_Exc = New eRemoteDB.Execute
			With lrecTab_am_Exc
				.StoredProcedure = "getTab_am_exc_defvalue"
				.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nTariff_o", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("sClient_o", String.Empty, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				If .Run(False) Then
					If .Parameters("nTariff_o").Value > 0 Then
						Me.nTariff = .Parameters("nTariff_o").Value
						Me.sClient = .Parameters("sClient_o").Value
						Me.sCertype = sCertype
						Me.nBranch = nBranch
						Me.nProduct = nProduct
						Me.nPolicy = nPolicy
						Me.nCertif = nCertif
						Me.dEffecdate = dEffecdate
						FindDeftValues = True
					End If
				End If
			End With
		Else
			FindDeftValues = True
		End If
		
FindDeftValues_Err: 
		If Err.Number Then
			FindDeftValues = False
		End If
		'UPGRADE_NOTE: Object lrecTab_am_Exc may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecTab_am_Exc = Nothing
		On Error GoTo 0
	End Function
	
	'%Class_Initialize: Se ejecuta cuando se instancia la clase
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		sCertype = String.Empty
		nBranch = eRemoteDB.Constants.intNull
		nProduct = eRemoteDB.Constants.intNull
		nPolicy = eRemoteDB.Constants.intNull
		nCertif = eRemoteDB.Constants.intNull
		nTariff = eRemoteDB.Constants.intNull
		sIllness = String.Empty
		dEffecdate = eRemoteDB.Constants.dtmNull
		sClient = String.Empty
		nExc_code = eRemoteDB.Constants.intNull
		dInit_date = eRemoteDB.Constants.dtmNull
		dNulldate = eRemoteDB.Constants.dtmNull
		dEnd_date = eRemoteDB.Constants.dtmNull
		nId = eRemoteDB.Constants.intNull
		mlngUsercode = eRemoteDB.Constants.intNull
		mlngTransaction = eRemoteDB.Constants.intNull
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
End Class






