Option Strict Off
Option Explicit On
Public Class Coinsuran
	'%-------------------------------------------------------%'
	'% $Workfile:: Coinsuran.cls                            $%'
	'% $Author:: Nvaplat31                                  $%'
	'% $Date:: 4/11/03 18:09                                $%'
	'% $Revision:: 26                                       $%'
	'%-------------------------------------------------------%'
	
	'+ Propiedades según la tabla en el sistema 01/12/2000
	'+ Los campos llave corresponden a sCertype, nBranch, nPolicy, nProduct, nCompany, dEffecdate
	
	'+ Column_name        Type
	'-------------------- ----------------------------
	Public sCertype As String 'CHAR(1)
	Public nBranch As Integer 'NUMBER(5)
	Public nPolicy As Double 'NUMBER(10)
	Public nProduct As Integer 'NUMBER(5)
	Public nCompany As Integer 'NUMBER(5)
	Public dEffecdate As Date 'DATETIME
	Public nExpenses As Double 'NUMBER(4,2)
	Public nShare As Double 'NUMBER(4,2)
	Public nUsercode As Integer 'NUMBER(5)
	Public nTransaction As Double
	Public nCertif As Double
	
	'% Find: Se leen los datos de la tabla
	Public Function Find(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCompany As Integer, ByVal dEffecdate As Date, Optional ByVal bFind As Boolean = False) As Boolean
		Static lblnRead As Boolean
		
		Dim lrecReaCoinsuran As eRemoteDB.Execute
		
		On Error GoTo Find_Err
		
		lrecReaCoinsuran = New eRemoteDB.Execute
		
		If Me.sCertype <> sCertype Or Me.nBranch <> nBranch Or Me.nProduct <> nProduct Or Me.nPolicy <> nPolicy Or Me.nCompany <> nCompany Or Me.dEffecdate <> dEffecdate Or bFind Then
			
			'+Definición de parámetros para stored procedure 'insudb.ReaCoinsuran'
			'+Información leída el 04/12/2000 09:21:01
			
			With lrecReaCoinsuran
				.StoredProcedure = "ReaCoinsuran"
				.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nCompany", nCompany, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				If .Run Then
					nCompany = .FieldToClass("nCompany")
					nExpenses = .FieldToClass("nExpenses")
					nShare = .FieldToClass("nShare")
					lblnRead = True
					Me.sCertype = sCertype
					Me.nBranch = nBranch
					Me.nProduct = nProduct
					Me.nPolicy = nPolicy
					Me.nCompany = nCompany
					Me.dEffecdate = dEffecdate
					.RCloseRec()
				Else
					lblnRead = False
				End If
			End With
		End If
		
		Find = lblnRead
		
Find_Err: 
		If Err.Number Then
			Find = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecReaCoinsuran may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecReaCoinsuran = Nothing
	End Function
	
	'% Delete: Este método se encarga de eliminar registros en la tabla "Coinsuran".
	Public Function Delete() As Boolean
		Dim lrecinsDelCoinsuran As eRemoteDB.Execute
		
		On Error GoTo Delete_err
		
		lrecinsDelCoinsuran = New eRemoteDB.Execute
		
		'+ Definición de parámetros para stored procedure 'insudb.insDelCoinsuran'
		'+ Información leída el 01/12/2000 14:54:17
		
		With lrecinsDelCoinsuran
			.StoredProcedure = "insDelCoinsuran"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCompany", nCompany, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Delete = .Run(False)
		End With
		
Delete_err: 
		If Err.Number Then
			Delete = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecinsDelCoinsuran may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecinsDelCoinsuran = Nothing
	End Function
	
	'%Update: Este método se encarga de actualizar registros en la tabla "Coinsuran".
	Public Function Update() As Boolean
		Dim lrecinsCoinsuran As eRemoteDB.Execute
		
		On Error GoTo Update_Err
		
		lrecinsCoinsuran = New eRemoteDB.Execute
		
		'+Definición de parámetros para stored procedure 'insudb.insCoinsuran'
		'+Información leída el 01/12/2000 14:48:06
		
		With lrecinsCoinsuran
			.StoredProcedure = "insCoinsuran"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCompany", nCompany, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nExpenses", nExpenses, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 4, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nShare", nShare, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 4, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTransaction", nTransaction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			Update = .Run(False)
		End With
		
Update_Err: 
		If Err.Number Then
			Update = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecinsCoinsuran may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecinsCoinsuran = Nothing
	End Function
	
	'% insPostCA020: Realiza la actualización de registros de la ventana CA020Upd
	Public Function insPostCA020(ByVal sWindowType As String, ByVal sAction As String, ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, ByVal nCompany As Integer, ByVal nShare As Double, ByVal nUsercode As Integer, Optional ByVal nExpenses As Double = 0, Optional ByVal nTransaction As Double = 0) As Boolean
		Dim lclsPolicyWin As ePolicy.Policy_Win
		Dim lclsPolicy As ePolicy.Policy
		
		On Error GoTo insPostCA020_Err
		
		With Me
			.sCertype = sCertype
			.nBranch = nBranch
			.nPolicy = nPolicy
			.nProduct = nProduct
			.nCompany = nCompany
			.dEffecdate = dEffecdate
			.nExpenses = nExpenses
			.nShare = nShare
			.nUsercode = nUsercode
			.nTransaction = nTransaction
			.nCertif = nCertif
			
			Select Case sAction
				Case "Add", "Update"
					insPostCA020 = Update
					
				Case "Delete"
					insPostCA020 = Delete
			End Select
		End With
		
		If insPostCA020 Then
			lclsPolicy = New ePolicy.Policy
			If lclsPolicy.Find(sCertype, nBranch, nProduct, nPolicy) Then
				lclsPolicy.sCoinsuri = "1"
				insPostCA020 = lclsPolicy.Add()
			End If
		End If
		If insPostCA020 Then
			lclsPolicyWin = New ePolicy.Policy_Win
			insPostCA020 = lclsPolicyWin.Add_PolicyWin(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate, nUsercode, "CA020", IIf(sWindowType = "PopUp", "1", "2"))
		End If
		
insPostCA020_Err: 
		If Err.Number Then
			insPostCA020 = False
		End If
		'UPGRADE_NOTE: Object lclsPolicyWin may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsPolicyWin = Nothing
		'UPGRADE_NOTE: Object lclsPolicy may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsPolicy = Nothing
		On Error GoTo 0
	End Function
	
	'% insValCA020: Realiza la verificación de que la suma de los porcentajes de participación
	'%              incluyendo la retención propia sea = a 100% - ACM - 07/12/2000
	Public Function insValCA020(ByVal sWindowType As String, ByVal sAction As String, ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCompany As Integer, ByVal dEffecdate As Date, ByVal nShare As Double, Optional ByVal nRecordCount As Integer = 0) As String
		Dim lcolCoinsuran As Coinsurans
		Dim lclsError As eFunctions.Errors
		Dim ldblTotalPercent As Double
		
		On Error GoTo insValCA020_Err
		
		lclsError = New eFunctions.Errors
		
		With lclsError
			If sWindowType = "PopUp" Then
				If nCompany = eRemoteDB.Constants.intNull Then
					Call .ErrorMessage("CA020", 6012)
				Else
					'+ La compañía no debe estar repetida en la distribución
					If sAction = "Add" Then
						If insvalCompany(sCertype, nBranch, nProduct, nPolicy, nCompany, dEffecdate) Then
							Call .ErrorMessage("CA020", 3071)
						End If
					End If
					
					'+ El campo "% Participación" debe estar lleno
					If nShare = eRemoteDB.Constants.intNull Then
						Call .ErrorMessage("CA020", 3069)
					End If
				End If
			Else
				'+ El % de Participación propia debe estar lleno
				If nShare = eRemoteDB.Constants.intNull Then
					Call .ErrorMessage("CA020", 3067)
				Else
					lcolCoinsuran = New Coinsurans
					'+ La suma de los porcentajes, incluyendo la propia debe ser igual al 100%
					ldblTotalPercent = nShare + lcolCoinsuran.TotalShare(sCertype, nBranch, nProduct, nPolicy, nCompany, dEffecdate)
					If ldblTotalPercent <> 100 Then
						Call .ErrorMessage("CA020", 3070)
					End If
				End If
				
				'+ Debe indicarse por lo menos una compañía
				If nRecordCount = 0 Then
					Call .ErrorMessage("CA020", 55885)
				End If
			End If
			
			insValCA020 = .Confirm
		End With
		
insValCA020_Err: 
		If Err.Number Then
			insValCA020 = "insValCA020: " & Err.Description
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lclsError may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsError = Nothing
		'UPGRADE_NOTE: Object lcolCoinsuran may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lcolCoinsuran = Nothing
	End Function
	
	'% insvalCompany: verifica que la compañía se enceuntre asociada a la distribución de coaseguro
	'%                de la póliza
	Private Function insvalCompany(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCompany As Integer, ByVal dEffecdate As Date) As Boolean
		Dim lclsRemote As eRemoteDB.Execute
		
		On Error GoTo insvalCompany_Err
		
		lclsRemote = New eRemoteDB.Execute
		
		With lclsRemote
			.StoredProcedure = "valCoinsuran_policy"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCompany", nCompany, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nExists", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				insvalCompany = IIf(.Parameters("nExists").Value > 0, True, False)
			End If
		End With
		
insvalCompany_Err: 
		If Err.Number Then
			insvalCompany = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lclsRemote may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsRemote = Nothing
	End Function
End Class






