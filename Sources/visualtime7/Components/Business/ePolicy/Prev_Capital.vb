Option Strict Off
Option Explicit On
Public Class Prev_Capital
	'%-------------------------------------------------------%'
	'% $Workfile:: Prev_Capital.cls                         $%'
	'% $Author:: Nvaplat41                                  $%'
	'% $Date:: 9/10/03 19.01                                $%'
	'% $Revision:: 13                                       $%'
	'%-------------------------------------------------------%'
	
	
	'+
	'+ Estructura de tabla INSUDB.PREV_CAPITAL al 03-13-2002 13:06:35
	'+     Property                Type         DBType   Size Scale  Prec  Null
	'+-------------------------------------------------------------------------
	Public sCertype As String ' CHAR       1    0     0    N
	Public nBranch As Integer ' NUMBER     22   0     5    N
	Public nProduct As Integer ' NUMBER     22   0     5    N
	Public nPolicy As Double ' NUMBER     22   0     10   N
	Public nCertif As Double ' NUMBER     22   0     10   N
	Public nSurrAmount As Double ' NUMBER     22   2     10   S
	Public nLoans As Double ' NUMBER     22   2     10   S
	Public nBalance As Double ' NUMBER     22   2     14   S
	Public nCapital As Double ' NUMBER     22   2     10   S
	Public nUsercode As Integer ' NUMBER     22   0     5    N
	
	
	'+
	'+ Estructura de tabla insudb.Movprev_capital al 03-13-2002 16:25:33
	'+     Property                Type         DBType   Size Scale  Prec  Null
	'+-------------------------------------------------------------------------
	Public nId As Integer ' NUMBER     22   0     5    N
	Public nReceipt As Integer ' NUMBER     22   0     10   N
	Public nCover As Integer ' NUMBER     22   0     5    N
	Public sClient As String ' CHAR       14   0     0    N
	Public nPremium As Double ' NUMBER     22   2     10   S
	Public nPercent As Double ' NUMBER     22   6     9    S
	Public nCost As Double ' NUMBER     22   6     9    S
	'Public nCapital             As Double     ' NUMBER     22   2     12   S
	'Public nSurramount          As Double     ' NUMBER     22   2     12   S
	Public nTypemov As ePrevCapitalMove ' NUMBER     22   0     5    S
	Public dEffecdate As Date ' DATE       7    0     0    N
	'Public nUsercode            As Long       ' NUMBER     22   0     5    N
	
	
	'- Indica si al crear movimiento se actualiza automaticamente el saldo
	Public nAutoupd As Boolean
	
	
	'- Tipo de movimiento
	Public Enum ePrevCapitalMove
		PrevCapMoveCapital = 1
		PrevCapMoveLoans = 2
		PrevCapMoveSurrender = 3
	End Enum
	'% insCreMovPrev_Capital: Crea un movimiento de fondo de capitalizacion
	Public Function insCreMovPrev_Capital(ByVal sCertype As String, ByVal nPolicy As Double, ByVal nBranch As Integer, ByVal nCertif As Double, ByVal nProduct As Integer, ByVal nReceipt As Integer, ByVal nCover As Integer, ByVal sClient As String, ByVal nPremium As Double, ByVal nPercent As Double, ByVal nCost As Double, ByVal nCapital As Double, ByVal nSurrAmount As Double, ByVal nTypemov As ePrevCapitalMove, ByVal dEffecdate As Date, ByVal nUsercode As Integer) As Object
		Dim lreccreMovprev_capital As eRemoteDB.Execute
		
		On Error GoTo InsCreMovPrev_Capital_Err
		'+ Definición de store procedure creMovprev_capital al 03-13-2002 13:34:33
		lreccreMovprev_capital = New eRemoteDB.Execute
		With lreccreMovprev_capital
			.StoredProcedure = "creMovprev_capital"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nId", nId, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nReceipt", nReceipt, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCover", nCover, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPremium", nPremium, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPercent", nPercent, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 9, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCost", nCost, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 9, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCapital", nCapital, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nSurramount", nSurrAmount, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTypemov", nTypemov, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAutoupd", IIf(nAutoupd, 1, 0), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCommit", 1, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			insCreMovPrev_Capital = .Run(False)
			If insCreMovPrev_Capital Then
				nId = .Parameters("nId").Value
			End If
		End With
		
InsCreMovPrev_Capital_Err: 
		If Err.Number Then
			insCreMovPrev_Capital = False
		End If
		'UPGRADE_NOTE: Object lreccreMovprev_capital may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lreccreMovprev_capital = Nothing
		On Error GoTo 0
	End Function
	
	'%InsUpdPrev_Capital: Se encarga de actualizar la tabla Prev_Capital
	Private Function InsUpdPrev_Capital(ByVal nAction As Integer) As Boolean
		Dim lrecinsUpdprev_capital As eRemoteDB.Execute
		On Error GoTo insUpdprev_capital_Err
		
		lrecinsUpdprev_capital = New eRemoteDB.Execute
		
		'+
		'+ Definición de store procedure insUpdprev_capital al 03-13-2002 13:11:57
		'+
		With lrecinsUpdprev_capital
			.StoredProcedure = "insUpdprev_capital"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nSurramount", nSurrAmount, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nLoans", nLoans, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBalance", nBalance, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCapital", nCapital, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			InsUpdPrev_Capital = .Run(False)
		End With
		
insUpdprev_capital_Err: 
		If Err.Number Then
			InsUpdPrev_Capital = False
		End If
		'UPGRADE_NOTE: Object lrecinsUpdprev_capital may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecinsUpdprev_capital = Nothing
		On Error GoTo 0
		
	End Function
	'%Add: Crea un registro en la tabla
	Public Function Add() As Boolean
		Add = InsUpdPrev_Capital(1)
	End Function
	
	'%Update: Actualiza un registro en la tabla
	Public Function Update() As Boolean
		Update = InsUpdPrev_Capital(2)
	End Function
	
	'%Delete: Borra un registro en la tabla
	Public Function Delete() As Boolean
		'Delete = InsUpdPrev_Capital(3)
	End Function
	
	'%Find: Lee un registro de la tabla Prev_capital
	Public Function Find(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, Optional ByVal bFind As Boolean = False) As Boolean
		Dim lrecreaPrev_capital As eRemoteDB.Execute
		
		On Error GoTo Find_Err
		'+ Definición de store procedure reaPrev_capital al 03-13-2002 13:10:06
		If Me.sCertype <> sCertype Or Me.nBranch <> nBranch Or Me.nProduct <> nProduct Or Me.nPolicy <> nPolicy Or Me.nCertif <> nCertif Or bFind Then
			lrecreaPrev_capital = New eRemoteDB.Execute
			With lrecreaPrev_capital
				.StoredProcedure = "ReaPrev_capital"
				.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				If .Run Then
					Me.sCertype = sCertype
					Me.nBranch = nBranch
					Me.nProduct = nProduct
					Me.nPolicy = nPolicy
					Me.nCertif = nCertif
					Me.nSurrAmount = .FieldToClass("nSurramount")
					Me.nLoans = .FieldToClass("nLoans")
					Me.nBalance = .FieldToClass("nBalance")
					Me.nCapital = .FieldToClass("nCapital")
					Me.nUsercode = .FieldToClass("nUsercode")
					Find = True
				End If
			End With
		Else
			Find = True
		End If
		
Find_Err: 
		If Err.Number Then
			Find = False
		End If
		'UPGRADE_NOTE: Object lrecreaPrev_capital may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaPrev_capital = Nothing
		On Error GoTo 0
	End Function
	
	
	'%Class_Initialize: Inicializa las propiedades cuando se instancia la clase
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		
		With Me
			.sCertype = String.Empty
			.nBranch = eRemoteDB.Constants.intNull
			.nProduct = eRemoteDB.Constants.intNull
			.nPolicy = eRemoteDB.Constants.intNull
			.nCertif = eRemoteDB.Constants.intNull
			.nSurrAmount = eRemoteDB.Constants.intNull
			.nLoans = eRemoteDB.Constants.intNull
			.nBalance = eRemoteDB.Constants.intNull
			.nCapital = eRemoteDB.Constants.intNull
			.nUsercode = eRemoteDB.Constants.intNull
		End With
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
End Class






