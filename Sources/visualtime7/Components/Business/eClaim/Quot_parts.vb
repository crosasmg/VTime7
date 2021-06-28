Option Strict Off
Option Explicit On
Public Class Quot_parts
	'%-------------------------------------------------------%'
	'% $Workfile:: Quot_parts.cls                           $%'
	'% $Author:: Nvaplat22                                  $%'
	'% $Date:: 15/12/03 3:50p                               $%'
	'% $Revision:: 26                                       $%'
	'%-------------------------------------------------------%'
	
	'- Declaración de propiedades públicas necesarias para el manejo de operaciones
	'- dentro de la clase - Valores tomados de la descripción de la tabla QUOT-PARTS
	
	'- Se definen las propiedades principales de la clase correspondientes a la tabla Quot_Parts
	'- El campo llave corresponde a nServ_Order y nID
	
	'Name                                      Null?    Type
	'----------------------------------------- -------- ----------------------------
	Public nServ_Order As Double '                   NOT NULL NUMBER(10)
	Public dQuot_Date As Date '                   NOT NULL DATE
	Public nId As Integer '                   NOT NULL NUMBER(5)
	Public nQuantity_Parts As Integer '                   NOT NULL NUMBER(5)
	Public nAuto_parts As Integer 'Number(5)
	Public sOriginal As String 'Char(1)
	Public nAmount_Part As Double 'Number(14, 2)
	Public sSel As String 'Char(1)
	Public nUsercode As Integer 'Number(5)
    Public dCompdate As Date 'Date
    Public nServ_Order_new As Double

	
	'- Variables auxiliares
	Public mblnCharge As Boolean
	Public nVehicleBrand As Integer
	Public sVehicleModel As String
	Public nYear As Integer
	Public sChassis As String
	
	'- Tipo registro
	Private Structure udtQuot_parts
		Dim nServ_Order As Double
		Dim dQuot_Date As Date
		Dim nId As Integer
		Dim nQuantity_Parts As Integer
		Dim nAuto_parts As Integer
		Dim sOriginal As String
		Dim nAmount_Part As Double
		Dim sSel As String
		Dim nUsercode As Integer
		Dim dCompdate As Date
	End Structure
	
	'- Arreglo
	Private arrQuot_parts() As udtQuot_parts
	
	'% Delete: Realiza la eliminación de los registros tabla QUOT_PARTS
	Public Function Delete() As Boolean
		Dim DelQuot_parts As eRemoteDB.Execute
		
		On Error GoTo DelQuot_Err
		
		DelQuot_parts = New eRemoteDB.Execute
		
		With DelQuot_parts
			.StoredProcedure = "DelQuot_parts"
			.Parameters.Add("nServ_order", nServ_Order, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nId", nId, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Delete = .Run(False)
			
		End With
		
DelQuot_Err: 
		If Err.Number Then
			Delete = False
		End If
		On Error GoTo 0
		DelQuot_parts = Nothing
	End Function
	
	'% insValSI774_K: Validaciones al encabezado de la transacción SI774 - ACM - 12/06/2002
	Public Function insValSI774_K(ByVal sCodispl As String, ByVal dEffecdate As Date, ByVal nClaimNumber As Double, ByVal nServiceOrder As Double, ByVal nTypeServiceOrder As Integer, ByVal nAction As Integer, ByVal nStatusOrder As Integer, ByVal nCaseNumber As Integer, ByVal nDeman_type As Integer) As String
		Dim lclsErrors As New eFunctions.Errors
		Dim lclsClaim As New Claim
		Dim lclsProf_ord As New Prof_ord
		Dim lstrSep As String
        Dim lstrError As String = ""

        lclsErrors = New eFunctions.Errors
		
		On Error GoTo insValSI774_K_err
		
		lstrSep = "||"
		
		'+ Validación del campo fecha
		If dEffecdate = eRemoteDB.Constants.dtmNull Then
			lstrError = lstrError & lstrSep & "1103"
		End If
		
		'+ Validación del campo Siniestro
		If nClaimNumber <= 0 Then
			lstrError = lstrError & lstrSep & "4006"
		Else
			If Not lclsClaim.Find(nClaimNumber) Then
				lstrError = lstrError & lstrSep & "4005"
			End If
		End If
		
		If nServiceOrder <= 0 Then
			lstrError = lstrError & lstrSep & "4055"
		Else
			If lclsProf_ord.Find_nServ(nServiceOrder) Then
				If nServiceOrder > 0 And nTypeServiceOrder <> 4 Then
					lstrError = lstrError & lstrSep & "55760"
				End If
				
				'+ Verificar que la orden este asociada al siniestro-caso-demandante
				If Not lclsProf_ord.ValProf_ord(nClaimNumber, nCaseNumber, nDeman_type, nServiceOrder, True) Then
					lstrError = lstrError & lstrSep & "55755"
				End If
				
				'+Si la accion es registrar el estado de la orden debe ser realizada
                If nAction = 1 And (nStatusOrder = 3 Or nStatusOrder = 11) Then
                    lstrError = lstrError & lstrSep & "55761"
                End If
				
				'+Si la accion es rechazar el estado de la orden debe ser realizada o aceptada
				If (nAction = 2) And (nStatusOrder <> 3 And nStatusOrder <> 7) Then
					lstrError = lstrError & lstrSep & "55762"
				End If
				
				'+Si la accion es rechazar el estado de la orden debe ser realizada o aceptada
				If (nAction = 3) And (nStatusOrder <> 3 And nStatusOrder <> 7) Then
					lstrError = lstrError & lstrSep & "55763"
				End If
			Else
				'+Si la orden de servicio no existe se envia un mensaje indicando que la misma no existe.
				lstrError = lstrError & lstrSep & "4056"
			End If
		End If
		
		If lstrError <> String.Empty Then
			lstrError = Mid(lstrError, 3)
			With lclsErrors
				.ErrorMessage("SI774",  ,  ,  ,  ,  , lstrError)
				insValSI774_K = .Confirm()
			End With
		End If
		
insValSI774_K_err: 
		If Err.Number Then
			insValSI774_K = "insValSI774_K_err: " & Err.Description
		End If
		On Error GoTo 0
		lclsErrors = Nothing
		lclsClaim = Nothing
	End Function
	
	'% insValSI774: Validaciones al cuerpo de la transacción SI774
    Public Function insValSI774(ByVal sCodispl As String, ByVal blnSelected As Boolean, ByVal nQuantity As Integer, ByVal nSpareCode As Integer, ByVal nUnitValue As Double, ByVal nAction As Integer, ByVal lstrAction As String, ByVal sWindowType As String, ByVal nNum_Budget As Integer) As String
        Dim lclsErrors As New eFunctions.Errors
        Dim lstrSep As String
        Dim lstrError As String = ""

        lclsErrors = New eFunctions.Errors

        On Error GoTo insValSI774_err

        lstrSep = "||"

        If Not blnSelected And sWindowType <> "PopUp" Then
            lstrError = lstrError & lstrSep & "55764"
        Else

            If nQuantity <= 0 Then
                lstrError = lstrError & lstrSep & "55765"
            End If

            If nSpareCode <= 0 Then
                lstrError = lstrError & lstrSep & "55766"
            End If

            If nSpareCode > 0 And nSpareCode <> 888 And nUnitValue <= 0 Then
                lstrError = lstrError & lstrSep & "55767"
            End If
        End If

        If sWindowType <> "PopUp" Then
            If nNum_Budget <= 0 Then
                lstrError = lstrError & lstrSep & "55756"
            End If
        End If

        If lstrError <> String.Empty Then
            lstrError = Mid(lstrError, 3)
            With lclsErrors
                .ErrorMessage("SI774", , , , , , lstrError)
                insValSI774 = .Confirm()
            End With
        End If

insValSI774_err:
        If Err.Number Then
            insValSI774 = "insValSI774: " & Err.Description
        End If
        On Error GoTo 0
        lclsErrors = Nothing
    End Function

    Public Function insValSI774_FromDB(ByVal sCodispl As String, ByVal blnSelected As Boolean, ByVal nQuantity As Integer, ByVal nSpareCode As Integer, ByVal nUnitValue As Double, ByVal nAction As Integer, ByVal lstrAction As String, ByVal sWindowType As String, nClaim As Integer, nCase_Num As Integer, nDeman_Type As Integer, nModulec As Integer, nCover As Integer, ByRef nCapitalDisponible As Double) As String
        Dim lrecInsValSi774 As eRemoteDB.Execute
        Dim lobjErrors As eFunctions.Errors
        Dim lstrError As String

        Dim nReservaDisponible As Double

        Dim nIndStatus As Integer
        Dim nIndautorizacion As Integer
        Dim sListas As String

        On Error GoTo InsValSi774_Err

        lrecInsValSi774 = New eRemoteDB.Execute


        With lrecInsValSi774
            .StoredProcedure = "InsValSi774"
            .Parameters.Add("nClaim", nClaim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCase_num", nCase_Num, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nDeman_type", nDeman_Type, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nModulec", nModulec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCover", nCover, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nAmount", nUnitValue * nQuantity, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("SERRORLIST", String.Empty, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 4000, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("NRESERVA_DISPONIBLE", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("NCAPITAL_DISPONIBLE", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            .Run(False)
            lstrError = .Parameters("SERRORLIST").Value

            nReservaDisponible = .Parameters("NRESERVA_DISPONIBLE").Value
            nCapitalDisponible = .Parameters("NCAPITAL_DISPONIBLE").Value

            lobjErrors = New eFunctions.Errors

            '+ Las validaciones de seguridad no estan en el procedure por todos los cambios que se
            '+ deben realizar en seguridad
            If lstrError <> String.Empty Then
                With lobjErrors
                    Call .ErrorMessage(sCodispl,   , ,   , "(" & nReservaDisponible.ToString() & ")", , lstrError)
                    Return lobjErrors.Confirm
                End With
                lobjErrors = Nothing
            End If

        End With
InsValSi774_Err:
        If Err.Number Then
            Return "insValSI774_FromDB: " & Err.Description
        End If
        On Error GoTo 0
        lrecInsValSi774 = Nothing
    End Function


    '% Find: Realiza la búsqueda sobre la tabla QUOT_PARTS - ACM - 17/06/2002
    Public Function Find(ByVal nServiceOrder As Integer, Optional ByVal sCerType As String = "", Optional ByVal nBranch As Integer = 0, Optional ByVal nProduct As Integer = 0, Optional ByVal nPolicy As Double = 0, Optional ByVal nCertif As Integer = 0, Optional ByVal dEffecdate As Date = #12:00:00 AM#, Optional ByVal sCodispl As String = "", Optional ByVal nUsercode As Integer = 0) As Boolean
        Dim recReaQuot_parts As eRemoteDB.Execute
        Dim lclsAuto As New ePolicy.Automobile
        Dim lcolProf_ord As New Prof_ords
        Dim lclsProf_ord As Object
        Dim lintIndex As Integer
        Dim nAll_parts As Short


        On Error GoTo Find_Err

        recReaQuot_parts = New eRemoteDB.Execute

        '+ Definición de parámetros para stored procedure 'insudb.reaTab_WinCla'
        '+ Información leída el 10/02/2000 15:35:36

        If sCodispl = "SI776" Then
            nAll_parts = 2 'busca solo las partes seleccionadas
        Else
            nAll_parts = 1 'busca todas las partes con o sin seleccion
        End If

    
        With recReaQuot_parts
            .StoredProcedure = "ReaQuot_parts"
            .Parameters.Add("nService_order", nServiceOrder, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nAll_parts", nAll_parts, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 1, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If .Run Then
                lintIndex = 0
                ReDim arrQuot_parts(50)
                Do While Not .EOF
                    lintIndex = lintIndex + 1
                    arrQuot_parts(lintIndex).nServ_Order = .FieldToClass("nServ_order")
                    arrQuot_parts(lintIndex).dQuot_Date = .FieldToClass("DQUOT_DATE")
                    arrQuot_parts(lintIndex).nId = .FieldToClass("nId")
                    arrQuot_parts(lintIndex).nQuantity_Parts = .FieldToClass("NQUANTITY_PARTS")
                    arrQuot_parts(lintIndex).nAuto_parts = .FieldToClass("nAuto_part")
                    arrQuot_parts(lintIndex).sOriginal = .FieldToClass("sOriginal")
                    arrQuot_parts(lintIndex).nAmount_Part = .FieldToClass("NAMOUNT_PART")
                    arrQuot_parts(lintIndex).sSel = .FieldToClass("sSel")
                    arrQuot_parts(lintIndex).nUsercode = .FieldToClass("nUsercode")
                    arrQuot_parts(lintIndex).dCompdate = .FieldToClass("dCompdate")
                    .RNext()
                Loop
                .RCloseRec()
                ReDim Preserve arrQuot_parts(lintIndex)
                mblnCharge = True
            Else
                mblnCharge = False
            End If

            'UPGRADE_WARNING: IsEmpty was upgraded to IsNothing and has a new behavior. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
            If mblnCharge And sCerType <> String.Empty And nBranch <> eRemoteDB.Constants.intNull And nProduct <> eRemoteDB.Constants.intNull And nPolicy <> eRemoteDB.Constants.intNull And nCertif <> eRemoteDB.Constants.intNull And Not IsNothing(dEffecdate) Then
                If lclsAuto.Find(sCerType, nBranch, nProduct, nPolicy, nCertif, dEffecdate) Then
                    sChassis = lclsAuto.sChassis
                    If lclsAuto.Find_Tab_au_veh(lclsAuto.sVehcode) Then
                        nYear = lclsAuto.nYear
                        sVehicleModel = lclsAuto.sVehcode
                        nVehicleBrand = lclsAuto.nVehBrand
                    End If
                End If
            End If
        End With

        Find = mblnCharge

Find_Err:
        If Err.Number Then
            Find = False
        End If
        On Error GoTo 0
        recReaQuot_parts = Nothing
    End Function

    '% insPostSI774: Almacenamiento de data en las tablas QUOT_PARTS y PROF_ORD
    Public Function insPostSI774(ByVal dEffecdate As Date, ByVal nClaimNumber As Double, ByVal nCaseNumber As Integer, ByVal nServiceOrder As Integer, ByVal nTypeOrder As Integer, ByVal nStateOrder As Integer, ByVal nDemandantType As Integer, ByVal nAction As Integer, ByVal sSelected As String, ByVal nQuantity As Integer, ByVal nSparepartCode As Integer, ByVal sOriginal As String, ByVal nUnitValue As Double, ByVal nVehicleBrand As Integer, ByVal sVehicleModel As String, ByVal nYear As Integer, ByVal sChassis As String, ByVal nConsecutive As Integer, ByVal nUsercode As Integer, ByVal sWindowType As String, ByVal nTransaction As Integer, ByVal nIVA As Double, ByVal nSendCost As Double, ByVal nFreightage As Double, Optional ByVal nNum_Budget As Integer = 0, Optional ByVal nMainAction As Integer = 0, Optional ByVal nCapitalDisponible As Double = 0) As Boolean

        Dim lrecInsQuot_parts As eRemoteDB.Execute
        Dim lclsProf_ord As New Prof_ord
        Dim lclsBuy_ord As New Buy_ord


        On Error GoTo insPostSI774_err
        If sWindowType <> "PopUp" Then
            lclsProf_ord.nAction = 2
            Select Case nAction
                Case 1
                    If lclsProf_ord.Find_nServ(nServiceOrder) Then
                        lclsProf_ord.nStatus_ord = 3
                        lclsProf_ord.dDate_done = dEffecdate
                        lclsProf_ord.nNum_Budget = nNum_Budget
                        insPostSI774 = lclsProf_ord.Update_ProfOrdGeneric
                    End If
                Case 2
                    If lclsProf_ord.Find_nServ(nServiceOrder) Then
                        '  lclsProf_ord.nStatus_ord = 8
                        lclsProf_ord.nStatus_ord = 3
                        lclsProf_ord.dDate_done = dEffecdate
                        insPostSI774 = lclsProf_ord.Update_ProfOrdGeneric
                    End If
                Case 3
                    If lclsProf_ord.Find_nServ(nServiceOrder) Then
                        lclsProf_ord.nStatus_ord = 9
                        lclsProf_ord.dDate_done = dEffecdate
                        insPostSI774 = lclsProf_ord.Update_ProfOrdGeneric
                    End If
            End Select
            '+ACTUALIZA EL IVA DE LA TABLA PROF_ORD
            Call Updprof_ord(nServiceOrder, nIVA, nSendCost, nFreightage, nUsercode, dEffecdate)

            If nMainAction <> 401 Then
                Dim bFromSI774 As Boolean = True
                lclsBuy_ord.insPostSI776(lclsProf_ord.nClaim, lclsProf_ord.nCase_Num, lclsProf_ord.nDeman_Type, nServiceOrder, dEffecdate, lclsProf_ord.sClient, "0", "0", "0", "0", lclsProf_ord.nAmount, nUsercode, , , , , , , , 1, bFromSI774)
            End If
            Me.nServ_Order_new = lclsBuy_ord.nServ_Order

        Else

            insPostSI774 = InsQuot_parts(dEffecdate, nClaimNumber, nCaseNumber, nServiceOrder, nTypeOrder, nStateOrder, nDemandantType, nAction, sSelected, nQuantity, nSparepartCode, sOriginal, nUnitValue, nVehicleBrand, sVehicleModel, nYear, sChassis, nConsecutive, nUsercode, sWindowType, nTransaction, nIVA, nSendCost, nFreightage, nNum_Budget, nMainAction)

            If lclsProf_ord.Find_nServ(nServiceOrder) Then
                '*+ Se determina el monto de ajuste para la orden segun la disponibilida del capital de la cobertura.
                Dim nAmont_Ajus_Ord As Double

                If lclsProf_ord.nAmount > nCapitalDisponible And nCapitalDisponible > 0 Then
                    nAmont_Ajus_Ord = IIf((lclsProf_ord.nAmount - nCapitalDisponible) < 0, 0, lclsProf_ord.nAmount - nCapitalDisponible)
                    'lclsProf_ord.nAmount = nCapitalDisponible

                    If nAmont_Ajus_Ord > 0 Then

                        Dim qry As New eRemoteDB.Query
                        Dim nExist_AjustPart As Integer

                        If qry.OpenQuery("QUOT_PARTS", "COUNT(*) EXIST_AJUSTPART", " NSERV_ORDER =  " & nServ_Order & " AND NAUTO_PART = 888 ") Then
                            nExist_AjustPart = qry.FieldToClass("EXIST_AJUSTPART")
                            If nExist_AjustPart > 0 Then
                                nAction = 2
                            Else
                                nAction = 1
                            End If
                        End If

                        insPostSI774 = InsQuot_parts(dEffecdate, nClaimNumber, nCaseNumber, nServiceOrder, nTypeOrder, nStateOrder, nDemandantType, nAction, sSelected, nQuantity, 888, sOriginal, -1 * nAmont_Ajus_Ord, nVehicleBrand, sVehicleModel, nYear, sChassis, nConsecutive, nUsercode, sWindowType, nTransaction, nIVA, nSendCost, nFreightage, nNum_Budget, nMainAction)
                    End If
                End If
            End If

        End If

insPostSI774_err:
        If Err.Number Then
            insPostSI774 = False
        End If
        On Error GoTo 0
        lrecInsQuot_parts = Nothing
        lclsProf_ord = Nothing
    End Function

    Private Function InsQuot_parts(ByVal dEffecdate As Date, ByVal nClaimNumber As Double, ByVal nCaseNumber As Integer, ByVal nServiceOrder As Integer, ByVal nTypeOrder As Integer, ByVal nStateOrder As Integer, ByVal nDemandantType As Integer, ByVal nAction As Integer, ByVal sSelected As String, ByVal nQuantity As Integer, ByVal nSparepartCode As Integer, ByVal sOriginal As String, ByVal nUnitValue As Double, ByVal nVehicleBrand As Integer, ByVal sVehicleModel As String, ByVal nYear As Integer, ByVal sChassis As String, ByVal nConsecutive As Integer, ByVal nUsercode As Integer, ByVal sWindowType As String, ByVal nTransaction As Integer, ByVal nIVA As Double, ByVal nSendCost As Double, ByVal nFreightage As Double, Optional ByVal nNum_Budget As Integer = 0, Optional ByVal nMainAction As Integer = 0) As Boolean


        Dim lrecInsQuot_parts As eRemoteDB.Execute = New eRemoteDB.Execute
        On Error GoTo InsQuot_parts_err
        With lrecInsQuot_parts
            .StoredProcedure = "InsQuot_parts"
            .Parameters.Add("nService_Order", nServiceOrder, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 10, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dQuot_Date", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nId", nConsecutive, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 5, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nQuantity_Parts", nQuantity, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 5, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nAuto_Part", nSparepartCode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 5, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sOriginal", sOriginal, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nAmount_Part", nUnitValue, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sSel", sSelected, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 5, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dCompdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nAction", nAction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 5, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            InsQuot_parts = .Run(False)
        End With
InsQuot_parts_err:
        If Err.Number Then
            InsQuot_parts = False
        End If
        On Error GoTo 0
        lrecInsQuot_parts = Nothing
    End Function

    Public ReadOnly Property CountQuot_parts() As Integer
		Get
			If mblnCharge Then
				CountQuot_parts = UBound(arrQuot_parts)
			Else
				CountQuot_parts = -1
			End If
		End Get
	End Property
	
	'% Item: Dada una determinada posición dentro del arreglo, si se consigue data, la
	'%       asigna a las propiedades públicas de la clase - ACM - 17/06/2002
	Public Function Item(ByVal nIndex As Object) As Boolean
		If mblnCharge Then
			If nIndex <= UBound(arrQuot_parts) Then
				With arrQuot_parts(nIndex)
					Me.nServ_Order = .nServ_Order
					Me.dQuot_Date = .dQuot_Date
					Me.nId = .nId
					Me.nQuantity_Parts = .nQuantity_Parts
					Me.nAuto_parts = .nAuto_parts
					Me.sOriginal = .sOriginal
					Me.nAmount_Part = .nAmount_Part
					Me.sSel = .sSel
					Me.nUsercode = .nUsercode
					Me.dCompdate = .dCompdate
				End With
				Item = True
			Else
				Item = False
			End If
		End If
	End Function
	
	'% Update: Realiza la actualizacion de los registros seleccionados en la tabla QUOT_PARTS
	Public Function Update(ByVal nServ_Order As Double, ByVal nId As Integer, ByVal sSel As String, ByVal nUsercode As Integer) As Boolean
		Dim UpdQuot_parts As eRemoteDB.Execute
		
		On Error GoTo Update_Err
		
		UpdQuot_parts = New eRemoteDB.Execute
		
		With UpdQuot_parts
			.StoredProcedure = "UpdQuot_parts"
			.Parameters.Add("nServ_order", nServ_Order, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nId", nId, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sSel", sSel, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Update = .Run(False)
			
		End With
		
Update_Err: 
		If Err.Number Then
			Update = False
		End If
		On Error GoTo 0
		UpdQuot_parts = Nothing
	End Function
	
	'% Updprof_ord: Actualiza el iva en la tabla prof_ord
	Public Function Updprof_ord(ByVal nServ_Order As Double, ByVal nIVA As Double, ByVal nSendCost As Double, ByVal nFreightage As Double, ByVal nUsercode As Integer, Optional ByVal dEffecdate As Date = #12:00:00 AM#) As Boolean
		Dim lrecUpdprof_ord As eRemoteDB.Execute
		
		On Error GoTo Updprof_ord_Err
		
		lrecUpdprof_ord = New eRemoteDB.Execute
		
		With lrecUpdprof_ord
			.StoredProcedure = "Updprof_ord"
			.Parameters.Add("nServ_order", nServ_Order, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nIva", nIVA, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nSendCost", nSendCost, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nFreightage", nFreightage, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 7, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dDate_Done", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Updprof_ord = .Run(False)
			
		End With
		
Updprof_ord_Err: 
		If Err.Number Then
			Updprof_ord = False
		End If
		On Error GoTo 0
		lrecUpdprof_ord = Nothing
	End Function
	
	'% Find_exists: Realiza la búsqueda sobre la tabla QUOT_PARTS - ACM - 17/06/2002
	Public Function Find_exists(ByVal nServiceOrder As Integer) As Boolean
		Dim recReaQuot_parts As eRemoteDB.Execute
		
		On Error GoTo Find_exists_Err
		
		recReaQuot_parts = New eRemoteDB.Execute
		
		With recReaQuot_parts
			.StoredProcedure = "ReaQuot_part_exist"
			.Parameters.Add("nService_order", nServiceOrder, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nExists", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Run(False)
			Find_exists = .Parameters("nExists").Value > 0
		End With
		
Find_exists_Err: 
		If Err.Number Then
			Find_exists = False
		End If
		On Error GoTo 0
		recReaQuot_parts = Nothing
	End Function
End Class






