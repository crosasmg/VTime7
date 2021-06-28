Option Strict Off
Option Explicit On
Public Class eval_master
	'%-------------------------------------------------------%'
	'% $Workfile:: eval_master.cls                          $%'
	'% $Author:: Nvaplat41                                  $%'
	'% $Date:: 10/10/03 17.35                               $%'
	'% $Revision:: 26                                       $%'
	'%-------------------------------------------------------%'
	'+
	'+ Estructura de tabla INSUDB.EVAL_MASTER al 09-26-2002 17:20:09
	'+         Property                Type         DBType   Size Scale  Prec  Null
	'+-------------------------------------------------------------------------
	Public nEval As Double ' NUMBER     22   0     10   N
	Public sClient As String ' CHAR       14   0     0    S
	Public nBranch As Integer ' NUMBER     22   0     5    S
	Public nProduct As Integer ' NUMBER     22   0     5    S
	Public npolicy As Double ' NUMBER     22   0     10   S
	Public dStartdate As Date ' DATE       7    0     0    N
	Public ncertif As Double ' NUMBER     22   0     10   S
	Public dExpirdat As Date ' DATE       7    0     0    N
	Public nStatus_eval As Integer ' NUMBER     22   0     5    N
	Public nCapital As Double ' NUMBER     22   6     18   S
	Public nNoterest As Double ' NUMBER     22   0     10   S
	Public nCurrency As Integer ' NUMBER     22   0     5    S
	Public nCumul As Double ' NUMBER     22   6     18   S
	Public dCompdate As Date ' DATE       7    0     0    N
	Public nUsercode As Integer ' NUMBER     22   0     5    N
	Public sCertype As String ' CHAR       1    0     0    S
	
	Public sExist As String
	
	'% InsUpdeval_master: Se encarga de actualizar la tabla eval_master
	Private Function InsUpdeval_master(ByVal nAction As Integer) As Boolean
		
		Dim lrecinsUpdeval_master As eRemoteDB.Execute
		
		On Error GoTo insUpdeval_master_Err
		lrecinsUpdeval_master = New eRemoteDB.Execute
		
		With lrecinsUpdeval_master
			.StoredProcedure = "insUpdeval_master"
			.Parameters.Add("nAction", nAction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nEval", nEval, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", npolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dStartdate", dStartdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", ncertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dExpirdat", dExpirdat, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nStatus_eval", nStatus_eval, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCapital", nCapital, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nNoterest", nNoterest, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCurrency", nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCumul", nCumul, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCommit", 1, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			InsUpdeval_master = .Run(False)
		End With
		
insUpdeval_master_Err: 
		If Err.Number Then
			InsUpdeval_master = False
		End If
		'UPGRADE_NOTE: Object lrecinsUpdeval_master may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecinsUpdeval_master = Nothing
		On Error GoTo 0
	End Function
	
	'% Add: Crea un registro en la tabla
	Public Function Add() As Boolean
		Add = InsUpdeval_master(1)
	End Function
	
	'% Update: Actualiza un registro en la tabla
	Public Function Update() As Boolean
		Update = InsUpdeval_master(2)
	End Function
	
	'% Delete: Borra un registro en la tabla
	Public Function Delete() As Boolean
		Delete = InsUpdeval_master(3)
	End Function
	
	'% InsValBC802: Validaciones de la transacción(Folder)
	'%              para las evaluaciones(BC802)
	Public Function InsValBC802(ByVal nAction As Integer, ByVal dStartdate As Date, ByVal dExpirdat As Date, ByVal nCapital As Double, ByVal nCurrency As Integer, ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal npolicy As Double, ByVal ncertif As Double) As String
		Dim lclsErrors As eFunctions.Errors
		Dim lclsCertificat As Object
		Dim lclsPolicy As Object
		
		On Error GoTo InsValBC802_Err
		
		lclsErrors = New eFunctions.Errors
		lclsCertificat = eRemoteDB.NetHelper.CreateClassInstance("ePolicy.Certificat")
		lclsPolicy = eRemoteDB.NetHelper.CreateClassInstance("ePolicy.Policy")
		
		With lclsErrors
			'+Validar la vigencia (Inicio)
			If dStartdate = eRemoteDB.Constants.dtmNull Then
				.ErrorMessage("BC802", 55625)
			End If
			
			'+Validar la vigencia (Fin)
			If dExpirdat = eRemoteDB.Constants.dtmNull Then
				.ErrorMessage("BC802", 55626)
			End If
			
			'+Validar la vigencia (Inicio < Fin)
			If dExpirdat <= dStartdate And dExpirdat <> eRemoteDB.Constants.dtmNull Then
				.ErrorMessage("BC802", 55627)
			End If
			
			'+Validar la vigencia (Fin <= fecha de hoy)
			If nAction = eFunctions.Menues.TypeActions.clngActionUpdate Then
				If dExpirdat <= Today And dExpirdat <> eRemoteDB.Constants.dtmNull Then
					.ErrorMessage("BC802", 55629)
				End If
			End If
			
			'+Validar la vigencia (Fin)
			If nCapital <> eRemoteDB.Constants.intNull And nCurrency = eRemoteDB.Constants.intNull Then
				.ErrorMessage("BC802", 55628)
			End If
			
			'+Validar el producto y al poliza (si el ramo esta lleno)
			If nBranch <> eRemoteDB.Constants.intNull Then
				If nProduct = eRemoteDB.Constants.intNull Then
					.ErrorMessage("BC802", 1014)
				End If
				
				If npolicy = eRemoteDB.Constants.intNull Then
					.ErrorMessage("BC802", 55623)
				Else
					If sCertype = "2" Then
						Call lclsPolicy.Find(sCertype, nBranch, nProduct, npolicy)
						If (lclsPolicy.sStatus_pol <> 1 And lclsPolicy.sStatus_pol <> 4 And lclsPolicy.sStatus_pol <> 5) Then
							.ErrorMessage("BC802", 55958)
						End If
					Else
						Call lclsCertificat.Find(sCertype, nBranch, nProduct, npolicy, ncertif)
						If lclsCertificat.nStatquota <> 1 Then
							.ErrorMessage("BC802", 55958)
						End If
					End If
				End If
				
				If ncertif <> eRemoteDB.Constants.intNull Then
					If Not lclsCertificat.Find(sCertype, nBranch, nProduct, npolicy, ncertif) Then
						.ErrorMessage("BC802", 55624)
					End If
				End If
			End If
			
			InsValBC802 = .Confirm
		End With
		
InsValBC802_Err: 
		If Err.Number Then
			InsValBC802 = "InsValBC802: " & Err.Description
		End If
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
		On Error GoTo 0
	End Function
	
	'% Find_eval: Lee los datos de una evaluacion
	Function Find_eval(ByVal nEval As Double, ByVal sClient As String) As Object
		Dim lrecreaEval_master_o As eRemoteDB.Execute
		Dim lclsreaEval_master_o As eval_master
		
		On Error GoTo reaEval_master_o_Err
		lrecreaEval_master_o = New eRemoteDB.Execute
		
		With lrecreaEval_master_o
			.StoredProcedure = "reaEval_master_o"
			.Parameters.Add("nEval", nEval, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run(True) Then
				Me.nEval = .FieldToClass("nEval")
				Me.sClient = .FieldToClass("sClient")
				Me.nBranch = .FieldToClass("nBranch")
				Me.nProduct = .FieldToClass("nProduct")
				Me.npolicy = .FieldToClass("nPolicy")
				Me.dStartdate = .FieldToClass("dStartdate")
				Me.ncertif = .FieldToClass("nCertif")
				Me.dExpirdat = .FieldToClass("dExpirdat")
				Me.nStatus_eval = .FieldToClass("nStatus_eval")
				Me.nCapital = .FieldToClass("nCapital")
				Me.nNoterest = .FieldToClass("nNoterest")
				Me.nCurrency = .FieldToClass("nCurrency")
				Me.nCumul = .FieldToClass("nCumul")
				Me.nUsercode = .FieldToClass("nUsercode")
				Me.sCertype = .FieldToClass("sCertype")
				Find_eval = True
			Else
				Find_eval = False
			End If
		End With
		
reaEval_master_o_Err: 
		If Err.Number Then
			Find_eval = False
		End If
		'UPGRADE_NOTE: Object lrecreaEval_master_o may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaEval_master_o = Nothing
		On Error GoTo 0
	End Function
	
	'% InsPostBC802: Ejecuta el post de la transacción
	'%               Evaluaciones(BC802)
	Public Function InsPostBC802(ByVal sAction As String, ByVal nEval As Double, ByVal sClient As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal npolicy As Double, ByVal dStartdate As Date, ByVal ncertif As Double, ByVal dExpirdat As Date, ByVal nStatus_eval As Integer, ByVal nCapital As Double, ByVal nNoterest As Double, ByVal nCurrency As Integer, ByVal nCumul As Double, ByVal nUsercode As Integer, ByVal sCertype As String) As Boolean
		
		On Error GoTo InsPostBC802_Err
		
		With Me
			.nEval = nEval
			.sClient = sClient
			.nBranch = nBranch
			.nProduct = nProduct
			.npolicy = npolicy
			.dStartdate = dStartdate
			.ncertif = ncertif
			.dExpirdat = dExpirdat
			.nStatus_eval = nStatus_eval
			.nCapital = nCapital
			.nNoterest = nNoterest
			.nCurrency = nCurrency
			.nCumul = nCumul
			.nUsercode = nUsercode
			.sCertype = sCertype
		End With
		
		Select Case sAction
			Case "Add"
				InsPostBC802 = Add
			Case "Update"
				InsPostBC802 = Update
			Case "Del"
				InsPostBC802 = Delete
		End Select
		
InsPostBC802_Err: 
		If Err.Number Then
			InsPostBC802 = False
		End If
		On Error GoTo 0
	End Function
	
	'% Class_Initialize: Inicializa las propiedades cuando se instancia la clase
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		nEval = eRemoteDB.Constants.intNull
		sClient = String.Empty
		nBranch = eRemoteDB.Constants.intNull
		nProduct = eRemoteDB.Constants.intNull
		npolicy = eRemoteDB.Constants.intNull
		dStartdate = eRemoteDB.Constants.dtmNull
		ncertif = eRemoteDB.Constants.intNull
		dExpirdat = eRemoteDB.Constants.dtmNull
		nStatus_eval = eRemoteDB.Constants.intNull
		nCapital = eRemoteDB.Constants.intNull
		nNoterest = eRemoteDB.Constants.intNull
		nCurrency = eRemoteDB.Constants.intNull
		nCumul = eRemoteDB.Constants.intNull
		nUsercode = eRemoteDB.Constants.intNull
		sCertype = String.Empty
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
End Class






