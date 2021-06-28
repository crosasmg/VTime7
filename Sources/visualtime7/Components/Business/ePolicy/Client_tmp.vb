Option Strict Off
Option Explicit On
Public Class Client_tmp
	'%-------------------------------------------------------%'
	'% $Workfile:: Client_tmp.cls                           $%'
	'% $Author:: Nvaplat28                                  $%'
	'% $Date:: 29/01/04 18.01                               $%'
	'% $Revision:: 44                                       $%'
	'%-------------------------------------------------------%'
	
	'+ Definición de la tabla en el sistema al 17/01/2002
	'+ Los campos llave corresponden a SCERTYPE, NBRANCH, NPRODUCT, NPOLICY, NGROUP, NROLE, NID
	'+ Name                 Type                    Nullable
	'---------------------- ----------------------- --------
	Public sCertype As String 'CHAR(1)    NO
	Public nBranch As Integer 'NUMBER(5)  NO
	Public nProduct As Integer 'NUMBER(5)  NO
	Public nPolicy As Double 'NUMBER(10) NO
	Public nGroup As Integer 'NUMBER(5)  NO
	Public nRole As Integer 'NUMBER(5)  NO
	Public nId As Integer 'NUMBER(5)  NO
	Public sTypeAge As String 'Char(1)
	Public dBirthdate As Date 'Date
	Public nInitAge As Integer 'Number(5)
	Public nEndAge As Integer 'Number(5)
	Public nInsured As Double 'Number(10)
	Public nRentamount As Double 'Number(12)
	Public nCurrency As Integer 'Number(5)
	Public nUsercode As Integer 'NUMBER(5)  NO
	Public sVIP As String 'CHAR(1)    NO
	
	'+ Variables auxiliares
	
	'- Se define la variable que para definir si existen grupos colectivos para la póliza
	
	Public bGroupsExist As Boolean
	
	'- Se define la variable que para controlar la existencia de la ventana dentro de la secuencia
	
	Public bErrors As Boolean
	
	'- Edad del asegurado.  Se calcula en base a la fecha de efecto y la fecha del registro
	
	Public nAge As Integer
	Public dEffecdate As Date
	
	Private mclsPolicy As Policy
	
	'% insvalCA658: se realizan las validaciones correspondientes al detalle de la transacción
	Public Function insvalCA658(ByVal WindowType As String, ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal dEffecdate As Date, ByVal nRol As Integer, ByVal dBirthDat As Date, ByVal nAge As Integer, ByVal nTramAge As Integer, ByVal nInsured As Double, ByVal nCurrency As Integer, ByVal nGroup As Integer, ByVal nRentamount As Double, ByVal sTypeAge As String, ByVal bExistGroups As Boolean, ByVal sTypenom As String) As String
		Dim lobjErrors As eFunctions.Errors
		Dim lcolClient_tmps As Client_tmps
		Dim lobjClient_tmp As Client_tmp
		'- se utiliza para obtener la sumatoria del n° de asegurados
		Dim nCountInsured As Object
		
		On Error GoTo insvalCA658_err
		
		lobjErrors = New eFunctions.Errors
		
		If WindowType = "PopUp" Then
			'+ Tipo de asegurado: debe estar lleno
			If nRol = eRemoteDB.Constants.intNull Then
				Call lobjErrors.ErrorMessage("CA658", 10241)
			End If
			
			Select Case sTypeAge
				'+ Fecha de nacimiento: debe estar lleno
				Case "1"
					If dBirthDat = eRemoteDB.Constants.dtmNull Then
						Call lobjErrors.ErrorMessage("CA658", 55696)
					End If
					'+ Edad: debe estar lleno
				Case "2"
					If nAge = eRemoteDB.Constants.intNull Then
						Call lobjErrors.ErrorMessage("CA658", 6026)
					Else
						If nAge < 0 Then
							Call lobjErrors.ErrorMessage("CA658", 55694)
						End If
					End If
					'+ Tramo de edad: debe estar lleno
				Case "3"
					If nTramAge = eRemoteDB.Constants.intNull Then
						Call lobjErrors.ErrorMessage("CA658", 55698)
					End If
			End Select
			
			'+ Nro. de asegurados: debe estar lleno y ser mayor que cero
			If nInsured = eRemoteDB.Constants.intNull Then
				Call lobjErrors.ErrorMessage("CA658", 3332)
			Else
				If nInsured <= 0 Then
					Call lobjErrors.ErrorMessage("CA658", 3332)
				End If
			End If
			
			'+ Moneda: si se indicó la renta, debe estar lleno
			If nRentamount <> eRemoteDB.Constants.intNull Then
				If nCurrency = eRemoteDB.Constants.intNull Then
					Call lobjErrors.ErrorMessage("CA658", 10107)
				End If
			End If
			
			'+ Grupo colectivo: si las coberturas de la póliza es por grupo, debe estar lleno
			If bExistGroups Then
				If nGroup = eRemoteDB.Constants.intNull Then
					Call lobjErrors.ErrorMessage("CA658", 3308)
				End If
			End If
		Else
			If sTypenom = "1" Then
				lcolClient_tmps = New Client_tmps
				lobjClient_tmp = New Client_tmp
				If lcolClient_tmps.Find(sCertype, nBranch, nProduct, nPolicy, dEffecdate) Then
					'+ Si la nómina es temporal deben existir registros en el grid
					If lcolClient_tmps(1).sTypeAge <> sTypeAge Then
						Call lobjErrors.ErrorMessage("CA658", 707009)
					End If
					'+ la sumatoria del n° de asegurados debe ser igual al N° de Asegurados del detalle
					nCountInsured = 0
					For	Each lobjClient_tmp In lcolClient_tmps
						nCountInsured = nCountInsured + lobjClient_tmp.nInsured
					Next lobjClient_tmp
					If nCountInsured <> nInsured Then
						Call lobjErrors.ErrorMessage("CA658", 55695)
					End If
				Else
					'+ Si se indicó nómina temporal, debe tener registros en la tabla
					Call lobjErrors.ErrorMessage("CA658", 707009)
				End If
			End If
		End If
		
		insvalCA658 = lobjErrors.Confirm
		
insvalCA658_err: 
		If Err.Number Then
			insvalCA658 = "insvalCA658:" & Err.Description
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lobjErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lobjErrors = Nothing
		'UPGRADE_NOTE: Object lcolClient_tmps may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lcolClient_tmps = Nothing
		'UPGRADE_NOTE: Object lobjClient_tmp may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lobjClient_tmp = Nothing
	End Function
	
	'% inspostCA658: se realizan las actualizaciones sobre la tabla
	Public Function inspostCA658(ByVal sWindowType As String, ByVal sAction As String, ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nGroup As Integer, ByVal nRole As Integer, ByVal nId As Integer, Optional ByVal sTypeAge As String = "", Optional ByVal dBirthdate As Date = #12:00:00 AM#, Optional ByVal nInitAge As Integer = 0, Optional ByVal nEndAge As Integer = 0, Optional ByVal nInsured As Double = 0, Optional ByVal nRentamount As Double = 0, Optional ByVal nCurrency As Integer = 0, Optional ByVal nUsercode As Integer = 0, Optional ByVal sVIP As String = "", Optional ByVal sTypenom As String = "", Optional ByVal dEffecdate As Date = #12:00:00 AM#, Optional ByVal nCertif As Double = 0, Optional ByVal nAge As Integer = 0) As Boolean
		Dim lclsPolicy As Policy
		Dim lclsPolicy_Win As Policy_Win
		
		On Error GoTo inspostCA658_err
		If sWindowType = "PopUp" Then
			With Me
				.sCertype = sCertype
				.nBranch = nBranch
				.nProduct = nProduct
				.nPolicy = nPolicy
				.nGroup = IIf(nGroup = eRemoteDB.Constants.intNull, 0, nGroup)
				.nRole = nRole
				.nId = nId
				.sTypeAge = sTypeAge
				.dBirthdate = dBirthdate
				.nInitAge = nInitAge
				.nEndAge = nEndAge
				.nInsured = nInsured
				.nRentamount = nRentamount
				.nCurrency = nCurrency
				.nUsercode = nUsercode
				.sVIP = IIf(sVIP = String.Empty, "2", sVIP)
				.dEffecdate = dEffecdate
				.nAge = nAge
			End With
			
			Select Case sAction
				Case "Add"
					inspostCA658 = Add
				Case "Update"
					inspostCA658 = Update(2)
				Case "Del"
					inspostCA658 = Delete
			End Select
			lclsPolicy_Win = New Policy_Win
			Call lclsPolicy_Win.Add_PolicyWin(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate, nUsercode, "CA014A", CStr(3))
		Else
			lclsPolicy = New Policy
			With lclsPolicy
				If .Find(sCertype, nBranch, nProduct, nPolicy) Then
					.sTypenom = sTypenom
					inspostCA658 = .Add
					If inspostCA658 Then
						If sTypenom = "2" Then
							inspostCA658 = Delete_All(sCertype, nBranch, nProduct, nPolicy)
						End If
						If inspostCA658 Then
							lclsPolicy_Win = New Policy_Win
							inspostCA658 = lclsPolicy_Win.Add_PolicyWin(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate, nUsercode, "CA658", "2")
							Call lclsPolicy_Win.Add_PolicyWin(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate, nUsercode, "CA014A", CStr(3))
						End If
					End If
				End If
			End With
		End If
inspostCA658_err: 
		If Err.Number Then
			inspostCA658 = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lclsPolicy may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsPolicy = Nothing
		'UPGRADE_NOTE: Object lclsPolicy_Win may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsPolicy_Win = Nothing
	End Function
	
	'% Add: se inserta un registro en la tabla
	Private Function Add() As Boolean
		Add = Update(1)
	End Function
	
	'% Delete: se elimina un registro en la tabla
	Private Function Delete() As Boolean
		Delete = Update(3)
	End Function
	
	'% Delete_detail: se eliminan los registros asociados a la póliza
	Public Function Delete_All(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, Optional ByVal nCertif As Double = 0, Optional ByVal dEffecdate As Date = #12:00:00 AM#, Optional ByVal nUsercode As Integer = 0) As Boolean
		Dim lclsExecute As eRemoteDB.Execute
		On Error GoTo Delete_All_Err
		lclsExecute = New eRemoteDB.Execute
		
		'+Definición de parámetros para stored procedure 'insudb.insupdClient_tmp'
		'+Información leída el 17/01/2002
		
		With lclsExecute
			.StoredProcedure = "Delete_Client_tmp_a"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCommit", 1, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Delete_All = .Run(False)
		End With
		
Delete_All_Err: 
		If Err.Number Then
			Delete_All = False
		End If
		'UPGRADE_NOTE: Object lclsExecute may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsExecute = Nothing
	End Function
	
	'% Update: actualiza la informacón de la tabla
	Private Function Update(ByVal nAction As Integer) As Boolean
		Dim lclsExecute As eRemoteDB.Execute
		On Error GoTo Update_Err
		lclsExecute = New eRemoteDB.Execute
		
		'+Definición de parámetros para stored procedure 'insudb.insupdClient_tmp'
		'+Información leída el 17/01/2002
		
		With lclsExecute
			.StoredProcedure = "insupdClient_tmp"
			.Parameters.Add("nAction", nAction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nGroup", nGroup, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nRole", nRole, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nId", nId, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sTypeage", sTypeAge, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dBirthdate", dBirthdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nInitAge", nInitAge, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nEndAge", nEndAge, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nInsured", nInsured, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nRentAmount", nRentamount, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCurrency", nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sVIP", sVIP, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAge", nAge, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Update = .Run(False)
		End With
		
Update_Err: 
		If Err.Number Then
			Update = False
		End If
		'UPGRADE_NOTE: Object lclsExecute may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsExecute = Nothing
	End Function
	
	'% inspreCA658: se controlan los datos a mostrar en la página
	Public Sub inspreCA658(ByVal WindowType As String, ByVal sPolitype As String, ByVal nCertif As Double, Optional ByVal sCertype As String = "", Optional ByVal nBranch As Integer = 0, Optional ByVal nProduct As Integer = 0, Optional ByVal nPolicy As Double = 0, Optional ByVal dEffecdate As Date = #12:00:00 AM#)
        'Dim lintCount As Integer
        'Dim lintTop As Integer
        'Dim lstrCodispl As String
		Dim lclsPolicy As Policy
		Dim lclsGroupss As Groupss
		
		lclsPolicy = New Policy
		lclsGroupss = New Groupss
		'+ Se determina si el tratamiento de las coberturas durante la emision es por grupo.
		If lclsPolicy.Find(sCertype, nBranch, nProduct, nPolicy) And lclsGroupss.Find(sCertype, nBranch, nProduct, nPolicy, dEffecdate) Then
			bGroupsExist = IIf(lclsPolicy.sTyp_module = "3", True, False)
		End If
		If WindowType <> "PopUp" Then
			If nCertif <> 0 Or sPolitype <> "2" Then
				'+ La ventana debe aparecer en la secuencia de la póliza matriz
				bErrors = True
			End If
		End If
		'UPGRADE_NOTE: Object lclsPolicy may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsPolicy = Nothing
		'UPGRADE_NOTE: Object lclsGroupss may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsGroupss = Nothing
	End Sub
	
	'% DefaultValueCA658: se maneja el estado de los campos de la página
	Public Function DefaultValueCA658(ByVal sValue As String, ByVal sField As String) As Object
		DefaultValueCA658 = True
		Select Case sValue
			'+ Fecha de nacimiento
			Case "1"
				If sField = "tcdBirthDat" Then
					DefaultValueCA658 = False
				End If
				'+ Edad
			Case "2"
				If sField = "tcnAge" Then
					DefaultValueCA658 = False
				End If
				'+ Tramos de edad
			Case "3"
				If sField = "cbeTAge" Then
					DefaultValueCA658 = False
				End If
		End Select
		
		If mclsPolicy Is Nothing Then
			mclsPolicy = New Policy
			Call mclsPolicy.Find(sCertype, nBranch, nProduct, nPolicy)
		End If
		
		If sField = "optAge_Temp" Or sField = "optAge_Def" Then
			'+ Se busca el tipo de nómina asociado a la póliza
			If mclsPolicy.sTypenom = "1" Or mclsPolicy.sTypenom = String.Empty Then
				DefaultValueCA658 = IIf(sField = "optAge_Temp", "1", "2")
			Else
				DefaultValueCA658 = IIf(sField = "optAge_Def", "1", "2")
			End If
		End If
		
		'+ Las opciones para el ingreso de edades se deshabilitan si el tipo de nómina es Definitiva
		'+ o se trata de un producto de salud
		Select Case sField
			Case "optAge_1", "optAge_2"
				DefaultValueCA658 = mclsPolicy.sTypenom = "2"
				
			Case "optAge_3", "chkMassive"
				DefaultValueCA658 = mclsPolicy.sTypenom = "2"
				
			Case "URLFrame"
				If bErrors Or mclsPolicy.sTypenom = "2" Then
					DefaultValueCA658 = "/VTimeNet/Common/Blank.htm"
				Else
					DefaultValueCA658 = "CA658Frame.aspx?sCodispl=CA658&sCodisp=CA658&nMainAction=304&sOnSeq=1&nOptAge=1"
				End If
		End Select
	End Function
	
	'% ClearFields: se inicializan las variables públicas de la clase
	Private Sub ClearFields()
		sCertype = String.Empty
		nBranch = eRemoteDB.Constants.intNull
		nProduct = eRemoteDB.Constants.intNull
		nPolicy = eRemoteDB.Constants.intNull
		nGroup = eRemoteDB.Constants.intNull
		nRole = eRemoteDB.Constants.intNull
		nId = eRemoteDB.Constants.intNull
		sTypeAge = String.Empty
		dBirthdate = eRemoteDB.Constants.dtmNull
		nInitAge = eRemoteDB.Constants.intNull
		nEndAge = eRemoteDB.Constants.intNull
		nInsured = eRemoteDB.Constants.intNull
		nRentamount = eRemoteDB.Constants.intNull
		nCurrency = eRemoteDB.Constants.intNull
		nUsercode = eRemoteDB.Constants.intNull
		sVIP = String.Empty
	End Sub
	
	'* Class_Initialize: se controla el acceso a la clase
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		Call ClearFields()
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
	
	'* Class_Initialize: se controla el acceso a la clase
	'UPGRADE_NOTE: Class_Terminate was upgraded to Class_Terminate_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Terminate_Renamed()
		'UPGRADE_NOTE: Object mclsPolicy may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		mclsPolicy = Nothing
	End Sub
	Protected Overrides Sub Finalize()
		Class_Terminate_Renamed()
		MyBase.Finalize()
	End Sub
End Class






