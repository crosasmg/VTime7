Option Strict Off
Option Explicit On
Public Class Contr_comm
	'%-------------------------------------------------------%'
	'% $Workfile:: Contr_comm.cls                           $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 1:28p                                $%'
	'% $Revision:: 9                                        $%'
	'%-------------------------------------------------------%'
	'+
	'+ Estructura de tabla insudb.contr_comm al 05-02-2002 10:44:46
	'+     Property                Type         DBType   Size Scale  Prec  Null
	'+-------------------------------------------------------------------------
	Public nNumber As Integer ' NUMBER     22   0     5    N
	Public nBranch_rei As Integer ' NUMBER     22   0     5    N
	Public nType As Integer ' NUMBER     22   0     5    N
	Public nInsur_area As Integer ' NUMBER     22   0     5    N
	Public nCovergen As Integer ' NUMBER     22   0     5    N
	Public dEffecdate As Date ' DATE       7    0     0    N
	Public dNulldate As Date ' DATE       7    0     0    S
	Public nFirstYear As Double ' NUMBER     22   2     4    S
	Public nNextYear As Double ' NUMBER     22   2     4    S
	Public nPermExp As Double ' NUMBER     22   2     4    S
	Public nTempexp As Double ' NUMBER     22   2     4    S
	Public sRoutine As String ' CHAR       12   0     0    S
	Public dCompdate As Date ' DATE       7    0     0    N
	Public nUsercode As Integer ' NUMBER     22   0     5    N
	
	Public sCommCov As String
	
	'%InsUpdContr_comm: Se encarga de actualizar la tabla Contr_comm
	Private Function InsUpdContr_comm(ByVal nAction As Integer) As Boolean
		Dim lrecinsUpdcontr_comm As eRemoteDB.Execute
		
		On Error GoTo insUpdcontr_comm_Err
		
		lrecinsUpdcontr_comm = New eRemoteDB.Execute
		'+
		'+ Definición de store procedure insUpdcontr_comm al 05-02-2002 11:02:05
		'+
		With lrecinsUpdcontr_comm
			.StoredProcedure = "insUpdcontr_comm"
			.Parameters.Add("nNumber", nNumber, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch_rei", nBranch_rei, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nType", nType, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nInsur_area", nInsur_area, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCovergen", nCovergen, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dNulldate", dNulldate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nFirstyear", nFirstYear, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 4, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nNextyear", nNextYear, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 4, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPermexp", nPermExp, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 4, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTempexp", nTempexp, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 4, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sRoutine", sRoutine, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAction", nAction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			InsUpdContr_comm = .Run(False)
		End With
		
insUpdcontr_comm_Err: 
		If Err.Number Then
			InsUpdContr_comm = False
		End If
		'UPGRADE_NOTE: Object lrecinsUpdcontr_comm may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecinsUpdcontr_comm = Nothing
		On Error GoTo 0
	End Function
	
	'%Add: Crea un registro en la tabla
	Public Function Add() As Boolean
		Add = InsUpdContr_comm(1)
	End Function
	
	'%Update: Actualiza un registro en la tabla
	Public Function Update() As Boolean
		Update = InsUpdContr_comm(2)
	End Function
	
	'%Delete: Borra un registro en la tabla
	Public Function Delete() As Boolean
		Delete = InsUpdContr_comm(3)
	End Function
	
	
	'%InsValCR731: Validaciones de la transacción(Folder)
	'%              Tabla de control de prima mínima(CR724)
	Public Function InsValCR731(ByVal sCodispl As String, ByVal nInsur_area As Integer, ByVal nCover As Integer, ByVal sRoutine As String, ByVal nFirstYear As Double, ByVal nNextYear As Double, ByVal nNumber As Integer, ByVal nBranch_rei As Integer, ByVal nType As Integer, ByVal dEffecdate As Date, ByVal sAction As String) As String
		Dim lclsErrors As eFunctions.Errors
		Dim lclsContr_comm As eCoReinsuran.Contr_comm
		Dim lcolContr_comms As eCoReinsuran.Contr_comms
		
		On Error GoTo InsValCR731_Err
		
		lclsErrors = New eFunctions.Errors
		lclsContr_comm = New eCoReinsuran.Contr_comm
		lcolContr_comms = New eCoReinsuran.Contr_comms
		
		With lclsErrors
			
			'+ Tipo de cobertura Debe estar lleno
			If nInsur_area <= 0 Then
				Call lclsErrors.ErrorMessage(sCodispl, 60321)
			End If
			
			'+ Cobertura Debe estar lleno
			If nCover <= 0 Then
				Call lclsErrors.ErrorMessage(sCodispl, 60315)
			Else
				If lcolContr_comms.Find(nNumber, nBranch_rei, nType, dEffecdate, nInsur_area, nCover) Then
					If sAction = "Add" Then
						For	Each lclsContr_comm In lcolContr_comms
							If lclsContr_comm.nCovergen = nCover Then
								Call lclsErrors.ErrorMessage(sCodispl, 60322)
							End If
						Next lclsContr_comm
					End If
				End If
			End If
			
			'+ Si no se indicó información en los campos "% 1er año, % años subsiguientes, ó rutina"
			If sRoutine = String.Empty And nFirstYear = eRemoteDB.Constants.intNull And nNextYear = eRemoteDB.Constants.intNull Then
				Call lclsErrors.ErrorMessage(sCodispl, 60336)
			End If
			
			'+ Si la cobertura ya fue creada a la fecha
			If FindDateProcess(nNumber, nBranch_rei, nType, dEffecdate, nInsur_area, nCover) Then
				Call lclsErrors.ErrorMessage(sCodispl, 11078)
			End If
			
			InsValCR731 = .Confirm
		End With
		
InsValCR731_Err: 
		If Err.Number Then
			InsValCR731 = "InsValCR731: " & Err.Description
		End If
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
		'UPGRADE_NOTE: Object lclsContr_comm may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsContr_comm = Nothing
		'UPGRADE_NOTE: Object lcolContr_comms may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lcolContr_comms = Nothing
		On Error GoTo 0
	End Function
	
	'%InsPostCR731: Ejecuta el post de la transacción
	'%               Tabla de control de prima mínima(CR731)
	Public Function InsPostCR731(ByVal sAction As String, ByVal nCoverType As Integer, ByVal nCover As Integer, ByVal sRoutine As String, ByVal nFirstYear As Double, ByVal nNextYear As Double, ByVal nPermExp As Double, ByVal nTemExp As Double, ByVal nNumber As Integer, ByVal nBranch_rei As Integer, ByVal nType As Integer, ByVal dEffecdate As Date, ByVal dNulldate As Date, ByVal nUsercode As Integer) As Boolean
		
		On Error GoTo InsPostCR731_Err
		
		With Me
			.nNumber = nNumber
			.nBranch_rei = nBranch_rei
			.nType = nType
			.nInsur_area = nCoverType
			.nCovergen = nCover
			.dEffecdate = dEffecdate
			.dNulldate = dNulldate
			.nFirstYear = nFirstYear
			.nNextYear = nNextYear
			.nPermExp = nPermExp
			.nTempexp = nTemExp
			.sRoutine = sRoutine
			.nUsercode = nUsercode
		End With
		
		Select Case sAction
			Case "Add"
				InsPostCR731 = Add
			Case "Update"
				InsPostCR731 = Update
			Case "Del"
				InsPostCR731 = Delete
		End Select
		
InsPostCR731_Err: 
		If Err.Number Then
			InsPostCR731 = False
		End If
		On Error GoTo 0
	End Function
	
	'%Find: Lee los datos de la tabla
	Private Function FindDateProcess(ByVal nNumber As Integer, ByVal nBranch_rei As Integer, ByVal nType As Integer, ByVal dEffecdate As Date, ByVal nInsur_area As Integer, ByVal nCovergen As Integer) As Boolean
		Dim lrecreaContr_comm As eRemoteDB.Execute
		Dim lclsContr_comm As eCoReinsuran.Contr_comm
		
		On Error GoTo reaContr_comm_Err
		
		lrecreaContr_comm = New eRemoteDB.Execute
		
		'+ Definición de store procedure reaContr_comm al 05-02-2002 11:28:32
		FindDateProcess = False
		
		With lrecreaContr_comm
			.StoredProcedure = "reaContr_comm_Date"
			.Parameters.Add("nNumber", nNumber, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch_rei", nBranch_rei, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nType", nType, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nInsur_Area", nInsur_area, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCoverGen", nCovergen, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run(True) Then
				Do While Not .EOF
					If .FieldToClass("dEffecdate") = .FieldToClass("dNulldate") Then
						FindDateProcess = True
						Exit Do
					End If
					.RNext()
				Loop 
			Else
				FindDateProcess = False
			End If
		End With
		
reaContr_comm_Err: 
		If Err.Number Then
			FindDateProcess = False
		End If
		
		'UPGRADE_NOTE: Object lrecreaContr_comm may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaContr_comm = Nothing
		On Error GoTo 0
	End Function
	'%Class_Initialize: Inicializa las propiedades cuando se instancia la clase
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		dEffecdate = eRemoteDB.Constants.dtmNull
		nUsercode = eRemoteDB.Constants.intNull
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
	
	
	
	'%Annulment: Anula siempre un registro
	Public Function Annulment() As Boolean
		Annulment = InsUpdContr_comm(4)
	End Function
End Class






