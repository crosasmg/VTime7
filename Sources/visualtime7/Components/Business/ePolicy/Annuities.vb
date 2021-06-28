Option Strict Off
Option Explicit On
Public Class Annuities
	'%-------------------------------------------------------%'
	'% $Workfile:: Annuities.cls                            $%'
	'% $Author:: Nvaplat41                                  $%'
	'% $Date:: 9/10/03 19.01                                $%'
	'% $Revision:: 19                                       $%'
	'%-------------------------------------------------------%'
	
	'-Propiedades según la tabla en el sistema el 08/07/2002
	'-La llave primaria corresponde a sCertype , nBranch, nProduct, nPolicy, nCertif, dEffecdate
	
	'Column_name               Type                        Computed   Length Prec  Scale Nullable TrimTrailingBlanks  FixedLenNullInSource
	'------------------------  -------------------------   --------   ------ ----- ----- -------- ------------------  --------------------
	Public sCertype As String 'char       no         1      no    no       no
	Public nBranch As Integer 'smallint   no         2      5     0        no    (n/a)               (n/a)
	Public nProduct As Integer 'smallint   no         2      5     0        no    (n/a)               (n/a)
	Public nPolicy As Double 'int        no         4      10    0        no    (n/a)               (n/a)
	Public nCertif As Double 'int        no         4      10    0        no    (n/a)               (n/a)
	Public dEffecdate As Date 'datetime   no         8      no                   (n/a)               (n/a)
	Public nPremiumbas As Double 'decimal    no
	Public sClient As String
	Public nCapital As Double
	Public dStartdate As Date
	Public dExpirdat As Date
	Public dIssuedat As Date
	Public dNulldate As Date
	Public nNullcode As Integer
	Public nPremium As Double
	
	Private mlngUsercode As Integer
	Private mlngAction As Integer
	
	'%Find: Función que retorna VERDADERO realizar la lectura de registros en la tabla 'Annuities'
	Public Function Find(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date) As Boolean
		
		Dim lrecReaAnnuities As eRemoteDB.Execute
		
		'+Definición de parámetros para stored procedure 'ReaAnnuities'
		'+Información leída el 08/07/2002
		On Error GoTo Find_Err
		lrecReaAnnuities = New eRemoteDB.Execute
		With lrecReaAnnuities
			.StoredProcedure = "ReaAnnuities"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				Me.sCertype = sCertype
				Me.nBranch = nBranch
				Me.nProduct = nProduct
				Me.nPolicy = nPolicy
				Me.nCertif = nCertif
				Me.dEffecdate = dEffecdate
				sClient = .FieldToClass("sClient")
				nCapital = .FieldToClass("nCapital")
				dStartdate = .FieldToClass("dStartdate")
				dExpirdat = .FieldToClass("dExpirdat")
				dIssuedat = .FieldToClass("dIssuedat")
				dNulldate = .FieldToClass("dNulldate")
				nNullcode = .FieldToClass("nNullcode")
				nPremium = .FieldToClass("nPremium")
				nPremiumbas = .FieldToClass("nPremiumbas")
				Find = True
				.RCloseRec()
			End If
		End With
		
Find_Err: 
		If Err.Number Then
			Find = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecReaAnnuities may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecReaAnnuities = Nothing
	End Function
	
	'%InsUpdAnnuities: Actualiza la tabla de datos particulares de rentas vitalicias
	Private Function InsUpdAnnuities() As Boolean
		Dim lrecInsUpdAnnuities As eRemoteDB.Execute
		
		On Error GoTo InsUpdAnnuities_Err
		lrecInsUpdAnnuities = New eRemoteDB.Execute
		'+ Definición de store procedure InsUpdAnnuities al 01-23-2003 17:33:49
		With lrecInsUpdAnnuities
			.StoredProcedure = "InsUpdAnnuities"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCapital", nCapital, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dStartdate", dStartdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dExpirdat", dExpirdat, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dIssuedat", dIssuedat, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dNulldate", dNulldate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nNullcode", nNullcode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPremium", nPremium, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPremiumbas", nPremiumbas, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", mlngUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAction", mlngAction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCommit", 1, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			InsUpdAnnuities = .Run(False)
		End With
		
InsUpdAnnuities_Err: 
		If Err.Number Then
			InsUpdAnnuities = False
		End If
		'UPGRADE_NOTE: Object lrecInsUpdAnnuities may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecInsUpdAnnuities = Nothing
		On Error GoTo 0
	End Function
	
	'% InsValRV778: Realiza la validación puntual de los campos a actualizar en la ventana RV778
	Public Function InsValRV778(ByVal sCodispl As String, ByVal nPremiumbas As Double) As String
		Dim lclsErrors As eFunctions.Errors
		
		On Error GoTo InsValRV778_Err
		'+ Validaciones de la Prima Unica
		If nPremiumbas = eRemoteDB.Constants.intNull Then
			lclsErrors = New eFunctions.Errors
			lclsErrors.ErrorMessage(sCodispl, 60441)
			InsValRV778 = lclsErrors.Confirm
		End If
		
InsValRV778_Err: 
		If Err.Number Then
			InsValRV778 = "InsValRV778: " & Err.Description
		End If
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
		On Error GoTo 0
	End Function
	
	'%InsPostRV778: Se realiza la actualización de los datos en la ventana RV778
	Public Function InsPostRV778(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, ByVal nPremiumbas As Double, ByVal nUsercode As Integer) As Boolean
		On Error GoTo InsPostRV778_Err
		InsPostRV778 = True
		With Me
			If .Find(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate) Then
				mlngAction = 2
			Else
				mlngAction = 1
			End If
			If mlngAction = 1 Or nPremiumbas <> .nPremiumbas Then
				.nPremiumbas = nPremiumbas
				mlngUsercode = nUsercode
				InsPostRV778 = InsUpdAnnuities
			End If
		End With
		
InsPostRV778_Err: 
		If Err.Number Then
			InsPostRV778 = False
		End If
		On Error GoTo 0
		
	End Function
End Class






