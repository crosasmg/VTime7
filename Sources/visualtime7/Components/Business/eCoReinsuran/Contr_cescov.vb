Option Strict Off
Option Explicit On
<System.Runtime.InteropServices.ProgId("contr_cescov_NET.contr_cescov")> Public Class contr_cescov
	'%-------------------------------------------------------%'
	'% $Workfile:: contr_cescov.cls                         $%'
	'% $Author:: Vvera                                      $%'
	'% $Date:: 28/03/06 22:19                               $%'
	'% $Revision:: 5                                        $%'
	'%-------------------------------------------------------%'
	
	'+
	'+ Estructura de tabla CONTR_CESCOV al 04-19-2002 17:33:20
	'+     Property                Type         DBType   Size Scale  Prec  Null
	'+-------------------------------------------------------------------------
	Public nNumber As Integer ' NUMBER     22   0     5    N
	Public nBranch_rei As Integer ' NUMBER     22   0     5    N
	Public nType As Integer ' NUMBER     22   0     5    N
	Public nInsur_area As Integer ' NUMBER     22   0     5    N
	Public nCovergen As Integer ' NUMBER     22   0     5    N
	Public dEffecdate As Date ' DATE       7    0     0    N
	Public dNulldate As Date ' DATE       7    0     0    S
	Public nTypecap As Integer ' NUMBER     22   0     1    N
	Public sRoucess As String ' CHAR       12   0     0    S
	Public nRate As Double ' NUMBER     22   6     9    S
	Public nCessprfix As Double ' NUMBER     22   2     10   S
	Public sInothercov As String ' CHAR       1    0     0    S
	Public dCompdate As Date ' DATE       7    0     0    N
	Public nUsercode As Integer ' NUMBER     22   0     5    N
    Public nCompany As Integer ' NUMBER     22   0     5    N
    Public nCovergen_Other As Integer ' NUMBER     22   0     5    N
	'%InsUpdcontr_cescov: Se encarga de actualizar la tabla contr_cescov
	Private Function InsUpdcontr_cescov(ByVal nAction As Integer) As Boolean
		Dim lrecinsUpdcontr_cescov As eRemoteDB.Execute
		
		On Error GoTo InsUpdcontr_cescov_Err
		
		lrecinsUpdcontr_cescov = New eRemoteDB.Execute
		
		'+
		'+ Definición de store procedure insUpdcontr_cescov al 04-08-2002 13:29:28
		'+
		With lrecinsUpdcontr_cescov
			.StoredProcedure = "insUpdcontr_cescov"
			.Parameters.Add("nNumber", nNumber, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch_rei", nBranch_rei, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nType", nType, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nInsur_area", nInsur_area, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCovergen", nCovergen, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCompany", nCompany, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTypecap", nTypecap, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sRoucess", sRoucess, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nRate", nRate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 9, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCessprfix", nCessprfix, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sInothercov", sInothercov, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAction", nAction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCovergen_Other", nCovergen_Other, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			InsUpdcontr_cescov = .Run(False)
		End With
		
InsUpdcontr_cescov_Err: 
		If Err.Number Then
			InsUpdcontr_cescov = False
		End If
		'UPGRADE_NOTE: Object lrecinsUpdcontr_cescov may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecinsUpdcontr_cescov = Nothing
		On Error GoTo 0
	End Function
	
	'%Add: Crea un registro en la tabla
	Public Function Add() As Boolean
		Add = InsUpdcontr_cescov(1)
	End Function
	
	'%Update: Actualiza un registro en la tabla
	Public Function Update() As Boolean
		Update = InsUpdcontr_cescov(2)
	End Function
	'%Delete: Borra un registro en la tabla
	Public Function Delete() As Boolean
		Delete = InsUpdcontr_cescov(3)
	End Function
	'%Annulment: Anula siempre un registro
	Public Function Annulment() As Boolean
		Annulment = InsUpdcontr_cescov(4)
	End Function
	
	'%InsValCR725: Validaciones de la transacción(Folder)
	'%              Tabla de control de prima mínima(CR725)
    Public Function InsValCR725(ByVal sAction As String, ByVal sCodispl As String, ByVal nNumber As Integer, ByVal nBranch_rei As Integer, ByVal nType As Integer, ByVal nInsur_area As Integer, ByVal nCovergen As Integer, ByVal dEffecdate As Date, ByVal sRoucess As String, ByVal nRate As Double, ByVal nCessprfix As Double, ByVal sInothercov As String, ByVal nCompany As Integer, ByVal nTypecap As Integer, ByVal nCovergen_Other As Integer) As String
        Dim lclsErrors As eFunctions.Errors
        Dim lobjContr_Cescovs As contr_cescovs


        On Error GoTo InsValCR725_Err
        lclsErrors = New eFunctions.Errors
        lobjContr_Cescovs = New contr_cescovs


        With lclsErrors
            '+Validar que no se dupliquen registros
            If sAction = "Add" Then
                If lobjContr_Cescovs.Find(nNumber, nBranch_rei, nType, dEffecdate, nCovergen, , nInsur_area, nCompany) Then
                    .ErrorMessage(sCodispl, 60322)
                End If
            End If

            If nRate = eRemoteDB.Constants.intNull And nCessprfix = eRemoteDB.Constants.intNull And sRoucess = String.Empty And sInothercov = String.Empty Then
                .ErrorMessage(sCodispl, 60335)
            End If

            If nInsur_area = eRemoteDB.Constants.intNull Then
                .ErrorMessage(sCodispl, 60321)
            End If

            If sInothercov = String.Empty Then
                If (nTypecap = eRemoteDB.Constants.intNull Or nTypecap = 0) Then
                    .ErrorMessage(sCodispl, 300019)
                End If
            End If

            If nCovergen = eRemoteDB.Constants.intNull Then
                .ErrorMessage(sCodispl, 60315)
            End If

            If (sInothercov <> String.Empty And (nCovergen_Other = eRemoteDB.Constants.intNull Or nCovergen_Other = 0)) Then
                .ErrorMessage(sCodispl, 90000041)
            End If
            InsValCR725 = .Confirm
        End With

InsValCR725_Err:
        If Err.Number Then
            InsValCR725 = "InsValCR725: " & Err.Description
        End If
        'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsErrors = Nothing
        On Error GoTo 0
    End Function
	'%InsPostCR725: Ejecuta el post de la transacción
	'%               Tabla de control de prima mínima(CR725)
    Public Function InsPostCR725(ByVal sAction As String, ByVal nNumber As Integer, ByVal nBranch_rei As Integer, ByVal nType As Integer, ByVal nInsur_area As Integer, ByVal nCovergen As Integer, ByVal nCompany As Integer, ByVal dEffecdate As Date, ByVal sRoucess As String, ByVal nRate As Double, ByVal nCessprfix As Double, ByVal sInothercov As String, ByVal nTypecap As Integer, ByVal nUsercode As Integer, ByVal nCovergen_Other As Integer) As Boolean

        On Error GoTo InsPostCR725_Err

        With Me
            .nNumber = nNumber
            .nBranch_rei = nBranch_rei
            .nType = nType
            .nInsur_area = nInsur_area
            .nCovergen = nCovergen
            .nCompany = nCompany
            .dEffecdate = dEffecdate
            .sRoucess = sRoucess
            .nRate = nRate
            .nCessprfix = nCessprfix
            .sInothercov = sInothercov
            .nUsercode = nUsercode
            .nTypecap = IIf(nTypecap = eRemoteDB.Constants.intNull, 2, nTypecap)
            .nCovergen_Other = nCovergen_Other
        End With

        Select Case sAction
            Case "Add"
                InsPostCR725 = Add()
            Case "Update"
                InsPostCR725 = Update()
            Case "Del"
                InsPostCR725 = Delete()
        End Select

InsPostCR725_Err:
        If Err.Number Then
            InsPostCR725 = False
        End If
        On Error GoTo 0
    End Function
	
	'%Class_Initialize: Inicializa las propiedades cuando se instancia la clase
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		nBranch_rei = eRemoteDB.Constants.intNull
		nType = eRemoteDB.Constants.intNull
		nInsur_area = eRemoteDB.Constants.intNull
		nCovergen = eRemoteDB.Constants.intNull
		sRoucess = String.Empty
		nRate = eRemoteDB.Constants.intNull
		nCessprfix = eRemoteDB.Constants.intNull
		sInothercov = String.Empty
		dNulldate = eRemoteDB.Constants.dtmNull
		dEffecdate = eRemoteDB.Constants.dtmNull
		nUsercode = eRemoteDB.Constants.intNull
        nCompany = eRemoteDB.Constants.intNull
        nCovergen_Other = eRemoteDB.Constants.intNull
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
End Class






