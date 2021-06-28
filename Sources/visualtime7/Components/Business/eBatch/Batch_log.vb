Option Strict Off
Option Explicit On
Public Class Batch_log
	'%-------------------------------------------------------%'
	'% $Workfile:: Batch_log.cls                            $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 1:39p                                $%'
	'% $Revision:: 7                                        $%'
	'%-------------------------------------------------------%'
	'+
	'+ Estructura de tabla TMP_BATCH_LOG al 09-13-2002 15:59:38
	'+     Property                Type         DBType   Size Scale  Prec  Null
	'+-------------------------------------------------------------------------
	Public sKey As String ' VARCHAR2   20   0     0    N
	Public nMessseq As Integer ' NUMBER     22   0     5    N
	Public nMessline As Integer ' NUMBER     22   0     5    N
	Public nMesscod As Integer ' NUMBER     22   0     5    S
	Public sLog As String ' CHAR       255  0     0    S
	Public nUsercode As Integer ' NUMBER     22   0     5    S
	
	
	'%Add: Crea un registro en la tabla
	Public Function Add() As Boolean
		Dim lreccreTmp_batch_log As eRemoteDB.Execute
		On Error GoTo creTmp_batch_log_Err
		
		lreccreTmp_batch_log = New eRemoteDB.Execute
		
		'+
		'+ Definición de store procedure creTmp_batch_log al 09-13-2002 16:43:40
		'+
		With lreccreTmp_batch_log
			.StoredProcedure = "creTmp_batch_log"
			.Parameters.Add("sKey", sKey, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nMessseq", nMessseq, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nMessline", nMessline, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nMesscod", nMesscod, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sLog", sLog, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 255, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			Add = .Run(False)
		End With
		
creTmp_batch_log_Err: 
		If Err.Number Then
			Add = False
		End If
		'UPGRADE_NOTE: Object lreccreTmp_batch_log may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lreccreTmp_batch_log = Nothing
		On Error GoTo 0
	End Function
	
	'%valExistsTmp_batch_log: Verifica si existe información para procesar según condición de filtro de la transacción CO632_K.
	Public Function valExistsTmp_batch_log(ByVal sKey As String) As Boolean
		Dim lrecTmp_batch_log As eRemoteDB.Execute
		Dim llngExists As Integer
		
		On Error GoTo valExistsTmp_batch_log_Err
		
		lrecTmp_batch_log = New eRemoteDB.Execute
		
		With lrecTmp_batch_log
			.StoredProcedure = "valExistsTmp_batch_log"
			.Parameters.Add("sKey", sKey, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nExists", llngExists, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Run(False)
			valExistsTmp_batch_log = (.Parameters("nExists").Value = 1)
		End With
		
valExistsTmp_batch_log_Err: 
		If Err.Number Then
			valExistsTmp_batch_log = False
		End If
		'UPGRADE_NOTE: Object lrecTmp_batch_log may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecTmp_batch_log = Nothing
		On Error GoTo 0
	End Function
	
	'%Class_Initialize: Inicializa las propiedades cuando se instancia la clase
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		sKey = String.Empty
		nMessseq = eRemoteDB.Constants.intNull
		nMessline = eRemoteDB.Constants.intNull
		nMesscod = eRemoteDB.Constants.intNull
		sLog = String.Empty
		nUsercode = eRemoteDB.Constants.intNull
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
End Class






