Option Strict Off
Option Explicit On
Public Class Margin_Adj
	'%-------------------------------------------------------%'
	'% $Workfile:: Margin_Adj.cls                           $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 1:13p                                $%'
	'% $Revision:: 4                                        $%'
	'%-------------------------------------------------------%'
	
	'+
	'+ Estructura de tabla insudb.margin_adj al 06-02-2003 12:45:18
	'+     Property                Type         DBType   Size Scale  Prec  Null
	'+-------------------------------------------------------------------------
	Public nInsur_area As Integer ' NUMBER     22   0     5    N
	Public dInitdate As Date ' DATE       7    0     0    N
	Public nIdtable As Double ' NUMBER     22   0     10   N
	Public nIdrec As Double ' NUMBER     22   0     10   N
	Public nMovement As Integer ' NUMBER     22   0     5    N
	Public nAdjustamoloc As Double ' NUMBER     22   10    30   N
	Public nAdjustamoori As Double ' NUMBER     24   6     18   N
	Public sDescript As String ' CHAR       30   0     0    S
	Public dCompdate As Date ' DATE       7    0     0    N
	Public nUsercode As Integer ' NUMBER     22   0     5    S
	'
	'+ variables usadas en la clase
	Public dValDate As Date
	Public nCurrency As Integer
	'
	
	'%InsUpdMargin_Adj: Se encarga de actualizar la tabla Margin_Adj
	Private Function InsUpdMargin_Adj(ByVal nAction As Short) As Boolean
		Dim lclsRemote As eRemoteDB.Execute
		
		On Error GoTo InsUpdMargin_Allow_err
		
		lclsRemote = New eRemoteDB.Execute
		
		With lclsRemote
			.StoredProcedure = "InsUpdMargin_Adj"
			.Parameters.Add("nAction", nAction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nInsur_Area", nInsur_area, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dInitdate", dInitdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nIdtable", nIdtable, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nIdrec", nIdrec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nMovement", nMovement, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAdjustamoori", nAdjustamoori, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sDescript", sDescript, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 30, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dValdate", dValDate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCurrency", nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			InsUpdMargin_Adj = .Run(False)
		End With
		
InsUpdMargin_Allow_err: 
		If Err.Number Then
			InsUpdMargin_Adj = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lclsRemote may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsRemote = Nothing
	End Function
	
	'%Add: Crea un registro en la tabla
	Public Function Add() As Boolean
		Add = InsUpdMargin_Adj(1)
	End Function
	
	'%Update: Actualiza un registro en la tabla
	Public Function Update() As Boolean
		Update = InsUpdMargin_Adj(2)
	End Function
	
	'%Delete: Borra un registro en la tabla
	Public Function Delete() As Boolean
		Delete = InsUpdMargin_Adj(3)
	End Function
	
	'%Find: Lee los datos de la tabla
	Public Function Find(ByVal losCamposLlave As Object, Optional ByVal lblnFind As Boolean = False) As Boolean
		
	End Function
	
	'%InsValEffecdate: Valida la fecha de efecto de la transacción
	Public Function InsValEffecdate(ByVal losCamposLlave As Object, ByVal dEffecdate As Date) As Boolean
		
	End Function
	
	'%InsValMGS002Upd: Validaciones de la transacción(Folder)
	'%              Tabla de control de prima mínima(MGS002)
	Public Function InsValMGS002Upd(ByVal sCodispl As String, ByVal sAction As String, ByVal nAdjustamoori As Double) As String
		Dim lclsErrors As eFunctions.Errors
		
		On Error GoTo InsValMGS002Upd_Err
		lclsErrors = New eFunctions.Errors
		
		With lclsErrors
			'+Validar que no se dupliquen registros
			If nAdjustamoori = eRemoteDB.Constants.intNull Or nAdjustamoori = 0 Then
				.ErrorMessage(sCodispl, 55918)
			End If
			
			InsValMGS002Upd = .Confirm
		End With
		
InsValMGS002Upd_Err: 
		If Err.Number Then
			InsValMGS002Upd = "InsValMGS002Upd: " & Err.Description
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
	End Function
	
	'%InsPostMGS002: Ejecuta el post de la transacción
	'%               Tabla de control de prima mínima(MGS002)
	Public Function InsPostMGS002(ByVal sAction As String, ByVal nInsur_area As Integer, ByVal dInitdate As Date, ByVal nIdtable As Double, ByVal nIdrec As Double, ByVal nMovement As Integer, ByVal nAdjustamoori As Double, ByVal sDescript As String, ByVal dValDate As Date, ByVal nCurrency As Integer, ByVal nUsercode As Integer) As Boolean
		On Error GoTo InsPostMGS002_Err
		
		With Me
			.nInsur_area = nInsur_area
			.dInitdate = dInitdate
			.nIdtable = nIdtable
			.nIdrec = nIdrec
			.nMovement = nMovement
			.nAdjustamoori = nAdjustamoori
			.sDescript = sDescript
			.dValDate = dValDate
			.nCurrency = nCurrency
			.nUsercode = nUsercode
		End With
		
		Select Case sAction
			Case "Add"
				InsPostMGS002 = Add
			Case "Update"
				InsPostMGS002 = Update
			Case "Del"
				InsPostMGS002 = Delete
		End Select
		
InsPostMGS002_Err: 
		If Err.Number Then
			InsPostMGS002 = False
		End If
		On Error GoTo 0
	End Function
	
	'%Class_Initialize: Inicializa las propiedades cuando se instancia la clase
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		nInsur_area = eRemoteDB.Constants.intNull
		dInitdate = eRemoteDB.Constants.dtmNull
		nIdtable = eRemoteDB.Constants.intNull
		nIdrec = eRemoteDB.Constants.intNull
		nMovement = eRemoteDB.Constants.intNull
		nAdjustamoloc = eRemoteDB.Constants.intNull
		nAdjustamoori = eRemoteDB.Constants.intNull
		sDescript = String.Empty
		dCompdate = eRemoteDB.Constants.dtmNull
		nUsercode = eRemoteDB.Constants.intNull
		'+ Otras
		dValDate = eRemoteDB.Constants.dtmNull
		nCurrency = eRemoteDB.Constants.intNull
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
End Class






