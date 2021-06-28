Option Strict Off
Option Explicit On
Public Class Int_typ_agre
	'%-------------------------------------------------------%'
	'% $Workfile:: Int_typ_agre.cls                         $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 1:38p                                $%'
	'% $Revision:: 8                                        $%'
	'%-------------------------------------------------------%'
	
	'*-Propiedades según la tabla en el sistema el 27/12/2000
	'Column_Name                   Type          Length  Prec    Scale   Nullable
	'-------------------------   --------------- ------ -------- ------- ---------
	Public nAgreement As Integer ' NUMBER        22     5      0 No
	Public nIntertyp As Integer ' NUMBER        22     5      0 No
	Public nUsercode As Integer ' NUMBER        22     5      0 No
	
	'-Variables auxiliares
	'-Variable que guarda la descripción del tipo de intermediario
	Public sDescript As String
	
	'-Variable que indica si el registro esta seleccionado
	Public sSel As String
	
	'%InsValMVA646B: Validaciones de la transacción
	Public Function InsValMVA646B(ByVal sCodispl As String, ByVal nCount As Integer) As String
		Dim lclsErrors As eFunctions.Errors
		
		On Error GoTo InsValMVA646B_Err
		lclsErrors = New eFunctions.Errors
		
		With lclsErrors
			If nCount = 0 Then
				.ErrorMessage(sCodispl, 55590)
			End If
			InsValMVA646B = .Confirm
		End With
		
InsValMVA646B_Err: 
		If Err.Number Then
			InsValMVA646B = "InsValMVA646B: " & Err.Description
		End If
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
		On Error GoTo 0
	End Function
	
	'%InsPostMVA646B: Ejecuta las actualizaciones de la transacción según funcional
	'%                Tipos de intermediarios por convenio(MVA646B)
	Public Function InsPostMVA646B(ByVal nAgreement As Integer, ByVal sSel As String, ByVal sExist As String, ByVal sIntertyp As String, ByVal nUsercode As Integer) As Boolean
		Dim lrecInsPostMVA646B As eRemoteDB.Execute
		On Error GoTo InsPostMVA646B_Err
		'+ Definición de store procedure InsPostMVA646B al 06-24-2002 18:23:15
		lrecInsPostMVA646B = New eRemoteDB.Execute
		With lrecInsPostMVA646B
			.StoredProcedure = "InsPostMVA646B"
			.Parameters.Add("nAgreement", nAgreement, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sSel", sSel, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1000, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sExist", sExist, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1000, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sIntertyp", sIntertyp, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1000, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			InsPostMVA646B = .Run(False)
		End With
		
InsPostMVA646B_Err: 
		If Err.Number Then
			InsPostMVA646B = False
		End If
		'UPGRADE_NOTE: Object lrecInsPostMVA646B may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecInsPostMVA646B = Nothing
		On Error GoTo 0
	End Function
	
	'%Class_Initialize: Inicializa las propiedades cuando se instancia la clase
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		nAgreement = eRemoteDB.Constants.intNull
		nIntertyp = eRemoteDB.Constants.intNull
		nUsercode = eRemoteDB.Constants.intNull
		sDescript = String.Empty
		sSel = String.Empty
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
End Class






