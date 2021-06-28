Option Strict Off
Option Explicit On
Public Class Interm_agre
	'%-------------------------------------------------------%'
	'% $Workfile:: Interm_agre.cls                          $%'
	'% $Author:: Nvaplat18                                  $%'
	'% $Date:: 8/10/03 10.37                                $%'
	'% $Revision:: 8                                        $%'
	'%-------------------------------------------------------%'
	
	'*-Propiedades según la tabla en el sistema el 24/10/2001
	'Column_Name                   Type          Length  Prec    Scale   Nullable
	'-------------------------   --------------- ------ -------- ------- ---------
	Public nAgreement As Integer ' NUMBER        22     5      0 No
	Public nIntermed As Integer ' NUMBER        22     5      0 No
	Public nUsercode As Integer ' NUMBER        22     5      0 No
	
	'-Variables auxiliares
	'-Variable que guarda la descripción del tipo de intermediario
	Public sCliename As String
	
	'-Variable que indica si el registro esta seleccionado
	Public sSel As String
	
	'%InsValMVA646C: Validaciones de la transacción
	Public Function InsValMVA646C(ByVal sCodispl As String, ByVal nCount As Integer) As String
		Dim lclsErrors As eFunctions.Errors
		
		On Error GoTo InsValMVA646C_Err
		lclsErrors = New eFunctions.Errors
		
		With lclsErrors
			If nCount = 0 Then
				.ErrorMessage(sCodispl, 55591)
			End If
			InsValMVA646C = .Confirm
		End With
		
InsValMVA646C_Err: 
		If Err.Number Then
			InsValMVA646C = "InsValMVA646C: " & Err.Description
		End If
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
		On Error GoTo 0
	End Function
	
	'%InsPostMVA646C: Ejecuta las actualizaciones de la transacción según funcional
	'%                Intermediarios por convenio(MVA646C)
	Public Function InsPostMVA646C(ByVal nAgreement As Integer, ByVal sSel As String, ByVal sExist As String, ByVal sIntermed As String, ByVal nUsercode As Integer) As Boolean
		Dim lrecInsPostMVA646C As eRemoteDB.Execute
		On Error GoTo InsPostMVA646C_Err
		'+ Definición de store procedure InsPostMVA646C al 25-06-2002
		lrecInsPostMVA646C = New eRemoteDB.Execute
		With lrecInsPostMVA646C
			.StoredProcedure = "InsPostMVA646C"
			.Parameters.Add("nAgreement", nAgreement, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sSel", sSel, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 4000, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sExist", sExist, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 4000, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sIntermed", sIntermed, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 4000, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			InsPostMVA646C = .Run(False)
		End With
		
InsPostMVA646C_Err: 
		If Err.Number Then
			InsPostMVA646C = False
		End If
		'UPGRADE_NOTE: Object lrecInsPostMVA646C may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecInsPostMVA646C = Nothing
		On Error GoTo 0
	End Function
	
	'%Class_Initialize: Inicializa las propiedades cuando se instancia la clase
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		nAgreement = eRemoteDB.Constants.intNull
		nIntermed = eRemoteDB.Constants.intNull
		nUsercode = eRemoteDB.Constants.intNull
		sCliename = String.Empty
		sSel = String.Empty
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
End Class






