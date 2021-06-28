Option Strict Off
Option Explicit On
Public Class Plan_agre
	'%-------------------------------------------------------%'
	'% $Workfile:: Plan_agre.cls                            $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 1:38p                                $%'
	'% $Revision:: 7                                        $%'
	'%-------------------------------------------------------%'
	
	'*-Propiedades según la tabla en el sistema el 27/12/2000
	'Column_Name                   Type          Length  Prec    Scale   Nullable
	'-------------------------   --------------- ------ -------- ------- ---------
	Public nAgreement As Integer ' NUMBER        22     5      0 No
	Public nBranch As Integer ' NUMBER        22     5      0 No
	Public nProduct As Integer ' NUMBER        22     5      0 No
	Public nModulec As Integer ' NUMBER        22     5      0 No
	Public nUsercode As Integer ' NUMBER        22     5      0 No
	
	'-Variables auxiliares
	'-Variable que indica si el registro esta seleccionado
	Public sSel As String
	
	'-Variable que guarda la descripción del ramo
	Public sDesBranch As String
	
	'-Variable que guarda la descripción del producto
	Public sDesProduct As String
	
	'-Variable que guarda la descripción del módulo
	Public sDesModulec As String
	
	'%InsValMVA646D: Validaciones de la transacción
	Public Function InsValMVA646D(ByVal sCodispl As String, ByVal nCount As Integer) As String
		Dim lclsErrors As eFunctions.Errors
		On Error GoTo InsValMVA646D_Err
		lclsErrors = New eFunctions.Errors
		With lclsErrors
			If nCount = 0 Then
				.ErrorMessage(sCodispl, 55592)
			End If
			InsValMVA646D = .Confirm
		End With
		
InsValMVA646D_Err: 
		If Err.Number Then
			InsValMVA646D = "InsValMVA646D: " & Err.Description
		End If
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
		On Error GoTo 0
	End Function
	
	'%InsPostMVA646D: Ejecuta las actualizaciones de la transacción según funcional
	'%                Planes por convenio(MVA646C)
	Public Function InsPostMVA646D(ByVal nAgreement As Integer, ByVal sSel As String, ByVal sExist As String, ByVal sBranch As String, ByVal sProduct As String, ByVal sModulec As String, ByVal nUsercode As Integer) As Boolean
		Dim lrecInsPostMVA646D As eRemoteDB.Execute
		On Error GoTo InsPostMVA646D_Err
		'+ Definición de store procedure InsPostMVA646D al 25-06-2002
		lrecInsPostMVA646D = New eRemoteDB.Execute
		With lrecInsPostMVA646D
			.StoredProcedure = "InsPostMVA646D"
			.Parameters.Add("nAgreement", nAgreement, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sSel", sSel, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1000, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sExist", sExist, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1000, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sBranch", sBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 2000, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sProduct", sProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 2000, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sModulec", sModulec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 2000, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			InsPostMVA646D = .Run(False)
		End With
		
InsPostMVA646D_Err: 
		If Err.Number Then
			InsPostMVA646D = False
		End If
		'UPGRADE_NOTE: Object lrecInsPostMVA646D may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecInsPostMVA646D = Nothing
		On Error GoTo 0
	End Function
	
	'%Class_Initialize: Inicializa las propiedades cuando se instancia la clase
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		nAgreement = eRemoteDB.Constants.intNull
		nBranch = eRemoteDB.Constants.intNull
		nProduct = eRemoteDB.Constants.intNull
		nModulec = eRemoteDB.Constants.intNull
		nUsercode = eRemoteDB.Constants.intNull
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
End Class






