Option Strict Off
Option Explicit On
Public Class contr_rate_III
	'%-------------------------------------------------------%'
	'% $Workfile:: contr_rate_III.cls                       $%'
	'% $Author:: Nvaplat40                                  $%'
	'% $Date:: 13/10/03 6:15p                               $%'
	'% $Revision:: 21                                       $%'
	'%-------------------------------------------------------%'
	
	'+
	'+ Estructura de tabla contr_rate_III al 04-08-2002 16:26:00
	'+     Property                Type         DBType   Size Scale  Prec  Null
	'+-------------------------------------------------------------------------
	Public nNumber As Integer ' NUMBER     22   0     5    N
	Public nBranch_rei As Integer ' NUMBER     22   0     5    N
	Public nType As Integer ' NUMBER     22   0     5    N
	Public nCovergen As Integer ' NUMBER     22   0     5    N
	Public nDeductible As Double ' NUMBER     22   2     10   N
	Public nQFamily As Integer ' NUMBER     22   0     5    N
	Public nCapital As Double ' NUMBER     22   0     12   N
	Public nAge_reinsu As Integer ' NUMBER     22   0     5    N
	Public dEffecdate As Date ' DATE       7    0     0    N
	Public dNulldate As Date ' DATE       7    0     0    S
	Public nRate As Double ' NUMBER     22   6     8    S
	Public nPremium As Double ' NUMBER     22   2     10   S
	Public dCompdate As Date ' DATE       7    0     0    N
	Public nUsercode As Integer ' NUMBER     22   0     5    N
	
	'%Add: Crea un registro en la tabla
	Public Function Add() As Boolean
		Dim lreccreContr_rate_iii As eRemoteDB.Execute
		
		On Error GoTo creContr_rate_iii_Err
		
		lreccreContr_rate_iii = New eRemoteDB.Execute
		
		'+
		'+ Definición de store procedure creContr_rate_iii al 04-09-2002 12:49:14
		'+
		With lreccreContr_rate_iii
			.StoredProcedure = "creContr_rate_iii"
			.Parameters.Add("nNumber", nNumber, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch_rei", nBranch_rei, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nType", nType, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCovergen", nCovergen, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDeductible", nDeductible, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nQfamily", nQFamily, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCapital", nCapital, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAge_reinsu", nAge_reinsu, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nRate", nRate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 9, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPremium", nPremium, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			Add = .Run(False)
		End With
		
creContr_rate_iii_Err: 
		If Err.Number Then
			Add = False
		End If
		'UPGRADE_NOTE: Object lreccreContr_rate_iii may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lreccreContr_rate_iii = Nothing
		On Error GoTo 0
		
	End Function
	
	'%Update: Actualiza un registro en la tabla
	Public Function Update() As Boolean
		Dim lrecupdContr_rate_iii As eRemoteDB.Execute
		
		On Error GoTo updContr_rate_iii_Err
		
		lrecupdContr_rate_iii = New eRemoteDB.Execute
		
		'+
		'+ Definición de store procedure updContr_rate_iii al 04-09-2002 12:52:04
		'+
		With lrecupdContr_rate_iii
			.StoredProcedure = "updContr_rate_iii"
			.Parameters.Add("nNumber", nNumber, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch_rei", nBranch_rei, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nType", nType, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCovergen", nCovergen, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDeductible", nDeductible, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nQfamily", nQFamily, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCapital", nCapital, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAge_reinsu", nAge_reinsu, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nRate", nRate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 9, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPremium", nPremium, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			Update = .Run(False)
		End With
		
updContr_rate_iii_Err: 
		If Err.Number Then
			Update = False
		End If
		'UPGRADE_NOTE: Object lrecupdContr_rate_iii may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecupdContr_rate_iii = Nothing
		On Error GoTo 0
		
	End Function
	
	'%Delete: Borra un registro en la tabla
	Public Function Delete() As Boolean
		Dim lrecdelContr_rate_iii As eRemoteDB.Execute
		
		On Error GoTo delContr_rate_iii_Err
		
		lrecdelContr_rate_iii = New eRemoteDB.Execute
		
		'+
		'+ Definición de store procedure delContr_rate_iii al 04-09-2002 12:50:25
		'+
		With lrecdelContr_rate_iii
			.StoredProcedure = "delContr_rate_iii"
			.Parameters.Add("nNumber", nNumber, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch_rei", nBranch_rei, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nType", nType, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCovergen", nCovergen, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDeductible", nDeductible, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nQfamily", nQFamily, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCapital", nCapital, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAge_reinsu", nAge_reinsu, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			Delete = .Run(False)
		End With
		
delContr_rate_iii_Err: 
		If Err.Number Then
			Delete = False
		End If
		'UPGRADE_NOTE: Object lrecdelContr_rate_iii may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecdelContr_rate_iii = Nothing
		On Error GoTo 0
		
	End Function
	
	
	
	
	'%InsValCR766_K: Validaciones de la transacción(Header)
	Public Function InsValCR766_K(ByVal sCodispl As String, ByVal nNumber As Integer, ByVal nBranch_rei As Integer, ByVal nType As Integer, ByVal nCovergen As Integer, ByVal nDeductible As Integer, ByVal nQFamily As Integer, ByVal nCapital As Double, ByVal dEffecdate As Date, ByVal nAction As Integer) As String
		Dim lclsErrors As eFunctions.Errors
		Dim lclsContrproc As eCoReinsuran.Contrproc
		Dim lclsContr_rate_iiis As eCoReinsuran.contr_rate_IIIs
		
		lclsErrors = New eFunctions.Errors
		lclsContrproc = New eCoReinsuran.Contrproc
		lclsContr_rate_iiis = New eCoReinsuran.contr_rate_IIIs
		
        On Error GoTo insValCR766_k_Err

        '+ Se valida que el registro exista en la tabla CONTRPROC
        If lclsContrproc.Find(nNumber, nType, nBranch_rei, dEffecdate) Then

            '+ Se valida el ramo del reaseguro
            If nBranch_rei = eRemoteDB.Constants.intNull Or nBranch_rei = 0 Then
                Call lclsErrors.ErrorMessage(sCodispl, 60314)
            End If

            '+Se valida que el código del contrato
            If nNumber = eRemoteDB.Constants.intNull Or nNumber = 0 Then
                Call lclsErrors.ErrorMessage(sCodispl, 3357)
            End If

            '+Se valida que el tipo de contrato
            If nType = eRemoteDB.Constants.intNull Or nType = 0 Then
                Call lclsErrors.ErrorMessage(sCodispl, 6018)
            End If

            '+Se valida la cobertura genérica
            If nCovergen = eRemoteDB.Constants.intNull Or nCovergen = 0 Then
                Call lclsErrors.ErrorMessage(sCodispl, 60315)
            End If

            '+ Validación del deducible
            If nDeductible = eRemoteDB.Constants.intNull Then
                Call lclsErrors.ErrorMessage(sCodispl, 60342)
            End If

            '+ Validación del Tope del seguro
            If nCapital = eRemoteDB.Constants.intNull Then
                Call lclsErrors.ErrorMessage(sCodispl, 60343)
            End If

            '+ Validación de los miembros del grupo familiar
            If nQFamily = eRemoteDB.Constants.intNull Then
                Call lclsErrors.ErrorMessage(sCodispl, 60344)
            End If

            '+ Fecha : Debe estar lleno. 
            If dEffecdate = eRemoteDB.Constants.dtmNull Then
                Call lclsErrors.ErrorMessage(sCodispl, 2056)
            End If


            If nAction = eFunctions.Menues.TypeActions.clngActionDuplicate Or nAction = eFunctions.Menues.TypeActions.clngActionUpdate Then

                '+ La información a duplicar no debe existir en la tabla (Contr_rate_III). 
                If nAction = eFunctions.Menues.TypeActions.clngActionDuplicate Then
                    If lclsContr_rate_iiis.Find(nNumber, nBranch_rei, nType, nCovergen, nDeductible, nQFamily, nCapital, dEffecdate) Then
                        Call lclsErrors.ErrorMessage(sCodispl, 55858)
                    End If
                End If

                '+ Si la fecha está llena y la acción es "modificar", se valida que no exista una modificación posterior a la que se está colocando en la  ventana. 
                '+ De ser así, se envía un mensaje al usuario indicando que la modificación sólo se 
                '+ puede realizar a una fecha posterior o igual a la de la última modificación realizada. 
                If nAction = eFunctions.Menues.TypeActions.clngActionUpdate Then
                    If FindLastModifyCR766(nNumber, nBranch_rei, nType, nCovergen, nDeductible, nQFamily, nCapital) Then
                        If Me.dEffecdate <> eRemoteDB.Constants.dtmNull AndAlso dEffecdate < Me.dEffecdate Then
                            Call lclsErrors.ErrorMessage(sCodispl, 10869, , , Me.dEffecdate)
                        End If
                    End If
                End If
            End If
        Else
            Call lclsErrors.ErrorMessage(sCodispl, 21002)
        End If

        InsValCR766_K = lclsErrors.Confirm
        lclsErrors = Nothing

insValCR766_k_Err:
        If Err.Number Then
            InsValCR766_K = InsValCR766_K & Err.Description
        End If
        On Error GoTo 0
    End Function
	
	'%InsValCR766: Validaciones de la transacción(Folder)
	'%              Tabla de control de prima mínima(CR766)
	Public Function InsValCR766(ByVal sCodispl As String, ByVal sAction As String, ByVal nNumber As Integer, ByVal nBranch_rei As Integer, ByVal nType As Integer, ByVal nCovergen As Integer, ByVal nDeductible As Double, ByVal nQFamily As Integer, ByVal nCapital As Double, ByVal dEffecdate As Date, ByVal nAge_reinsu As Integer, ByVal nRate As Double, ByVal nPremium As Double) As boolean
		Dim lclsErrors As eFunctions.Errors
		Dim lclsContr_rate_iiis As eCoReinsuran.contr_rate_IIIs
		
		On Error GoTo InsValCR766_Err
		
		lclsErrors = New eFunctions.Errors
		lclsContr_rate_iiis = New eCoReinsuran.contr_rate_IIIs
		
		'+ Se valida que el registro no exista en la tabla CONTR_RATE_III
		If sAction = "Add" And lclsContr_rate_iiis.FindCR766(nNumber, nBranch_rei, nType, nCovergen, nDeductible, nQFamily, nCapital, nAge_reinsu, dEffecdate) Then
			Call lclsErrors.ErrorMessage(sCodispl, 55845)
		End If
		
		'+ Se valida la edad actuarial
		If nAge_reinsu = eRemoteDB.Constants.intNull Then
			Call lclsErrors.ErrorMessage(sCodispl, 3954)
		End If
		
		'+ Se valida la tasa
		If nRate = eRemoteDB.Constants.intNull Then
			Call lclsErrors.ErrorMessage(sCodispl, 2042)
		End If
		'+ Se valida la prima
		If nPremium = eRemoteDB.Constants.intNull Then
			Call lclsErrors.ErrorMessage(sCodispl, 60345)
		End If
		
		InsValCR766 = lclsErrors.Confirm
		
InsValCR766_Err: 
		If Err.Number Then
			InsValCR766 = "InsValCR766: " & Err.Description
		End If
		
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
		'UPGRADE_NOTE: Object lclsContr_rate_iiis may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsContr_rate_iiis = Nothing
		On Error GoTo 0
	End Function
	
	'%InsPostCR766: Ejecuta el post de la transacción
	'%               Tabla de control de prima mínima(CR766)
	Public Function InsPostCR766(ByVal sAction As String, ByVal nNumber As Integer, ByVal nBranch_rei As Integer, ByVal nType As Integer, ByVal nCovergen As Integer, ByVal nDeductible As Double, ByVal nQFamily As Integer, ByVal nCapital As Double, ByVal dEffecdate As Date, ByVal nAge_reinsu As Integer, ByVal nRate As Double, ByVal nPremium As Double, ByVal nUsercode As Integer) As Boolean
		
		On Error GoTo InsPostCR766_Err
		
		With Me
			.nNumber = nNumber
			.nBranch_rei = nBranch_rei
			.nType = nType
			.nCovergen = nCovergen
			.nDeductible = nDeductible
			.nQFamily = nQFamily
			.nCapital = nCapital
			.dEffecdate = dEffecdate
			.nAge_reinsu = nAge_reinsu
			.nRate = nRate
			.nPremium = nPremium
			.nUsercode = nUsercode
		End With
		
		Select Case sAction
			Case "Add"
				InsPostCR766 = Add
			Case "Update"
				InsPostCR766 = Update
			Case "Del"
				InsPostCR766 = Delete
		End Select
InsPostCR766_Err: 
		If Err.Number Then
			InsPostCR766 = False
		End If
		On Error GoTo 0
	End Function
	
	'%Class_Initialize: Inicializa las propiedades cuando se instancia la clase
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		nNumber = eRemoteDB.Constants.intNull
		nBranch_rei = eRemoteDB.Constants.intNull
		nType = eRemoteDB.Constants.intNull
		nCovergen = eRemoteDB.Constants.intNull
		nDeductible = eRemoteDB.Constants.intNull
		nQFamily = eRemoteDB.Constants.intNull
		nCapital = eRemoteDB.Constants.intNull
		nAge_reinsu = eRemoteDB.Constants.intNull
		dEffecdate = eRemoteDB.Constants.dtmNull
		dNulldate = eRemoteDB.Constants.dtmNull
		nRate = eRemoteDB.Constants.intNull
		nPremium = eRemoteDB.Constants.intNull
		dCompdate = eRemoteDB.Constants.dtmNull
		nUsercode = eRemoteDB.Constants.intNull
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
	'% InsDupContr_rate_III: Invoca al procedimiento que duplica, la información
	'%                       de la tabla para un nueva llave
	Public Function InsDupContr_rate_III(ByVal nNumber As Integer, ByVal nBranch_rei As Integer, ByVal nType As Integer, ByVal nCovergen As Integer, ByVal nDeductible As Double, ByVal nQFamily As Integer, ByVal nCapital As Double, ByVal dEffecdate As Date, ByVal nNumber_new As Integer, ByVal nBranch_rei_new As Integer, ByVal nType_new As Integer, ByVal nCovergen_new As Integer, ByVal nDeductible_new As Double, ByVal nQFamily_new As Integer, ByVal nCapital_new As Double, ByVal dEffecdate_new As Date, ByVal nUsercode As Integer) As Boolean
		Dim lrecinsDupcontr_rate_iii As eRemoteDB.Execute
		On Error GoTo insDupcontr_rate_iii_Err
		
		lrecinsDupcontr_rate_iii = New eRemoteDB.Execute
		
		'+
		'+ Definición de store procedure insDupcontr_rate_iii al 04-22-2002 17:56:30
		'+
		With lrecinsDupcontr_rate_iii
			.StoredProcedure = "insDupcontr_rate_iii"
			.Parameters.Add("nNumber", nNumber, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch_rei", nBranch_rei, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nType", nType, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCovergen", nCovergen, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDeductible", nDeductible, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nQfamily", nQFamily, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCapital", nCapital, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nNumber_new", nNumber_new, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch_rei_new", nBranch_rei_new, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nType_new", nType_new, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCovergen_new", nCovergen_new, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDeductible_new", nDeductible_new, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nQfamily_new", nQFamily_new, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCapital_new", nCapital_new, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate_new", dEffecdate_new, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			InsDupContr_rate_III = .Run(False)
		End With
		
insDupcontr_rate_iii_Err: 
		If Err.Number Then
			InsDupContr_rate_III = False
		End If
		'UPGRADE_NOTE: Object lrecinsDupcontr_rate_iii may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecinsDupcontr_rate_iii = Nothing
		On Error GoTo 0
    End Function

    '%insValLastnModify: Se realiza la validación de la fecha de última modificación del contrato
    Public Function FindLastModifyCR766(ByVal nNumber As Integer, ByVal nBranch_rei As Integer, ByVal nType As Integer, ByVal nCovergen As Integer, ByVal nDeductible As Integer, ByVal nQFamily As Integer, ByVal nCapital As Double) As Boolean
        Dim lrecreaContrnpro_effecdate As eRemoteDB.Execute

        lrecreaContrnpro_effecdate = New eRemoteDB.Execute

        On Error GoTo insValLastnModify_Err

        '+ Definición de parámetros para stored procedure 'insudb.reaContrnpro_effecdate'
        ' Información leída el 07/06/2001 10:30:09 a.m.

        With lrecreaContrnpro_effecdate
            .StoredProcedure = "reaContr_rate_iii_effecdate"
            .Parameters.Add("nNumber", nNumber, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch_rei", nBranch_rei, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nType", nType, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCovergen", nCovergen, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nDeductible", nDeductible, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nQfamily", nQFamily, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCapital", nCapital, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If .Run Then
                Me.dEffecdate = .FieldToClass("dEffecdate")
                FindLastModifyCR766 = True
            Else
                FindLastModifyCR766 = False
            End If
        End With
        'UPGRADE_NOTE: Object lrecreaContrnpro_effecdate may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecreaContrnpro_effecdate = Nothing

insValLastnModify_Err:
        If Err.Number Then
            FindLastModifyCR766 = False
        End If
    End Function
End Class






