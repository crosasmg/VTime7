Option Strict Off
Option Explicit On
Public Class Margin_Allow
	'%-------------------------------------------------------%'
	'% $Workfile:: Margin_Allow.cls                         $%'
	'% $Author:: Nvaplat40                                  $%'
	'% $Date:: 19/12/03 11:42a                              $%'
	'% $Revision:: 4                                        $%'
	'%-------------------------------------------------------%'
	
	'- Variables segun campos en la tabla Margin_Allow al 26/05/2003
	
	'+ Nombre              Tipo                     ¿Nulo?
	'+ ------------------- ------------------------ ------
	Public nInsur_area As Integer 'NUMBER(5)   No
	Public dEffecdate As Date 'DATE        No
	Public nTableTyp As Short 'NUMBER(1)   No
	Public nSource As Short 'NUMBER(1)   No
	Public nIdRec As Integer 'NUMBER(5)   No
	Public nClaimClass As Short 'NUMBER(1)   Si
	Public nBranch As Integer 'NUMBER(5)   Si
	Public nProduct As Integer 'NUMBER(5)   Si
	Public nModulec As Integer 'NUMBER(5)   Si
	Public nCover As Integer 'NUMBER(5)   No
	Public dNulldate As Date 'DATE        Si
	Public dCompdate As Date 'DATE        No
	Public nUsercode As Integer 'NUMBER(5)   No
	
	Public sBranch As String
	Public sProduct As String
	Public sModulec As String
	Public sCover As String
	
	'%InsUpdMargin_Allow: Se encarga de actualizar la tabla Margin_Allow
	Private Function InsUpdMargin_Allow(ByVal nAction As Short) As Boolean
		Dim lclsRemote As eRemoteDB.Execute
		
		On Error GoTo InsUpdMargin_Allow_err
		
		lclsRemote = New eRemoteDB.Execute
		
		With lclsRemote
			.StoredProcedure = "InsUpdMargin_Allow"
			.Parameters.Add("nAction", nAction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nInsur_Area", nInsur_area, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTabletyp", nTableTyp, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nSource", nSource, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nIdRec", nIdRec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nClaimClass", nClaimClass, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nModulec", nModulec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCover", nCover, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			InsUpdMargin_Allow = .Run(False)
		End With
		
InsUpdMargin_Allow_err: 
		If Err.Number Then
			InsUpdMargin_Allow = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lclsRemote may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsRemote = Nothing
	End Function
	
	'%Add: Crea un registro en la tabla
	Public Function Add() As Boolean
		Add = InsUpdMargin_Allow(1)
	End Function
	
	'%Update: Actualiza un registro en la tabla
	Public Function Update() As Boolean
		Update = InsUpdMargin_Allow(2)
	End Function
	
	'%Delete: Borra un registro en la tabla
	Public Function Delete() As Boolean
		Delete = InsUpdMargin_Allow(3)
	End Function
	
	'%Find: Lee los datos de la tabla
	Public Function Find(ByVal nInsur_area As Integer, ByVal nTableTyp As Short, ByVal nSource As Short, ByVal nIdRec As Integer, ByVal nClaimClass As Short, ByVal dEffecdate As Date, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nModulec As Integer, ByVal nCover As Integer, Optional ByVal lblnFind As Boolean = False) As Boolean
		
	End Function
	
	'%insvalExist: Verifica si existen el registro que se esta ingresando
	Public Function insvalExist(ByVal nInsur_area As Integer, ByVal nTableTyp As Short, ByVal nSource As Short, ByVal nIdRec As Integer, ByVal nClaimClass As Short, ByVal dEffecdate As Date, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nModulec As Integer, ByVal nCover As Integer, Optional ByVal lblnFind As Boolean = False) As Boolean
		Dim lclsRemote As eRemoteDB.Execute
		
		On Error GoTo insvalExist_err
		
		lclsRemote = New eRemoteDB.Execute
		
		With lclsRemote
			.StoredProcedure = "valExist_Margin_allow"
			.Parameters.Add("nInsur_Area", nInsur_area, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTabletyp", nTableTyp, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nSource", nSource, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nClaimClass", nClaimClass, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nModulec", nModulec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCover", nCover, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nExists", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				insvalExist = .Parameters("nExists").Value = 1
			End If
		End With
		
insvalExist_err: 
		If Err.Number Then
			insvalExist = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lclsRemote may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsRemote = Nothing
	End Function
	
	'%InsValMMGS002_K: Validaciones de la transacción(Header)
	Public Function InsValMMGS002_K(ByVal sCodispl As String, ByVal nAction As Integer, ByVal nInsur_area As Integer, ByVal nTableTyp As Short, ByVal nSource As Short, ByVal nClaimClass As Short, ByVal dEffecdate As Date) As String
		Dim lclsErrors As eFunctions.Errors
		Dim ValMaxEffecdate1 As Date
		
		On Error GoTo InsValMMGS002_K_Err
		
		lclsErrors = New eFunctions.Errors
		
		With lclsErrors
			'+ Se valida el Campo Fecha
			If nInsur_area = eRemoteDB.Constants.intNull Or nInsur_area = 0 Then
				.ErrorMessage(sCodispl, 55031)
			End If
			
			If dEffecdate = eRemoteDB.Constants.dtmNull Then
				.sTypeMessage = eFunctions.Errors.ErrorsType.Warning
				.ErrorMessage(sCodispl, 4003)
			Else
				If nAction = eFunctions.Menues.TypeActions.clngActionadd Or nAction = eFunctions.Menues.TypeActions.clngActionUpdate Then
					ValMaxEffecdate1 = ValMaxEffecdate(nInsur_area, nTableTyp, nSource, nClaimClass)
					If ValMaxEffecdate1 > dEffecdate Then
						.ErrorMessage(sCodispl, 55912)
					End If
				End If
			End If
			
			InsValMMGS002_K = .Confirm
		End With
		
InsValMMGS002_K_Err: 
		If Err.Number Then
			InsValMMGS002_K = "InsValMMGS002_K: " & Err.Description
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
	End Function
	
	'%ValMaxEffecdate: Valida la fecha de efecto de la transacción
	Public Function ValMaxEffecdate(ByVal nInsur_area As Integer, ByVal nTableTyp As Short, ByVal nSource As Short, ByVal nClaimClass As Short) As Date
		Dim lrecReaMargin_Allow As eRemoteDB.Execute
		Dim dEffecdateMax As Date
		
		On Error GoTo ValMaxEffecdate_Err
		
		ValMaxEffecdate = eRemoteDB.Constants.dtmNull
		lrecReaMargin_Allow = New eRemoteDB.Execute
		With lrecReaMargin_Allow
			.StoredProcedure = "ReaMargin_Allow_Fec"
			.Parameters.Add("nInsur_Area", nInsur_area, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTabletyp", nTableTyp, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nSource", nSource, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nClaimClass", nClaimClass, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", eRemoteDB.Constants.dtmNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				ValMaxEffecdate = .Parameters("dEffecdate").Value
			Else
				ValMaxEffecdate = eRemoteDB.Constants.dtmNull
			End If
		End With
ValMaxEffecdate_Err: 
		If Err.Number Then
			ValMaxEffecdate = eRemoteDB.Constants.dtmNull
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecReaMargin_Allow may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecReaMargin_Allow = Nothing
	End Function
	
	'%InsValMMGS002: Validaciones de la transacción(Folder)
	'%              Tabla de control de prima mínima(MMGS002)
	Public Function InsValMMGS002(ByVal sCodispl As String, ByVal sAction As String, ByVal nInsur_area As Integer, ByVal nTableTyp As Short, ByVal nSource As Short, ByVal nIdRec As Integer, ByVal nClaimClass As Short, ByVal dEffecdate As Date, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nModulec As Integer, ByVal nCover As Integer) As String
		Dim lclsErrors As eFunctions.Errors
		
		On Error GoTo InsValMMGS002_Err
		
		lclsErrors = New eFunctions.Errors
		
		With lclsErrors
			If nBranch = eRemoteDB.Constants.intNull Or nBranch = 0 Then
				.ErrorMessage(sCodispl, 99999)
			End If
			'+Validar que no se dupliquen registros
			If sAction = "Add" Then
				If insvalExist(nInsur_area, nTableTyp, nSource, nIdRec, nClaimClass, dEffecdate, nBranch, nProduct, nModulec, nCover) Then
					.ErrorMessage(sCodispl, 55913)
				End If
			End If
			
			InsValMMGS002 = .Confirm
		End With
		
InsValMMGS002_Err: 
		If Err.Number Then
			InsValMMGS002 = "InsValMMGS002: " & Err.Description
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
	End Function
	
	'%InsPostMMGS002: Ejecuta el post de la transacción
	'%               Tabla de control de prima mínima(MMGS002)
	Public Function InsPostMMGS002(ByVal sAction As String, ByVal nInsur_area As Integer, ByVal nTableTyp As Short, ByVal nSource As Short, ByVal nIdRec As Integer, ByVal nClaimClass As Short, ByVal dEffecdate As Date, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nModulec As Integer, ByVal nCover As Integer, ByVal nUsercode As Integer) As Boolean
		
		On Error GoTo InsPostMMGS002_Err
		
		With Me
			.nInsur_area = nInsur_area
			.nTableTyp = nTableTyp
			.nSource = nSource
			.nIdRec = nIdRec
			.nClaimClass = nClaimClass
			.dEffecdate = dEffecdate
			.nBranch = nBranch
			.nProduct = nProduct
			.nModulec = nModulec
			.nCover = nCover
			.nUsercode = nUsercode
		End With
		
		Select Case sAction
			Case "Add"
				InsPostMMGS002 = Add
			Case "Update"
				InsPostMMGS002 = Update
			Case "Del"
				InsPostMMGS002 = Delete
		End Select
		
InsPostMMGS002_Err: 
		If Err.Number Then
			InsPostMMGS002 = False
		End If
		On Error GoTo 0
	End Function
	
	'%Class_Initialize: Inicializa las propiedades cuando se instancia la clase
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		nInsur_area = eRemoteDB.Constants.intNull
		dEffecdate = eRemoteDB.Constants.dtmNull
		nTableTyp = eRemoteDB.Constants.intNull
		nSource = eRemoteDB.Constants.intNull
		nIdRec = eRemoteDB.Constants.intNull
		nClaimClass = eRemoteDB.Constants.intNull
		nBranch = eRemoteDB.Constants.intNull
		nProduct = eRemoteDB.Constants.intNull
		nModulec = eRemoteDB.Constants.intNull
		nCover = eRemoteDB.Constants.intNull
		dNulldate = eRemoteDB.Constants.dtmNull
		dCompdate = eRemoteDB.Constants.dtmNull
		nUsercode = eRemoteDB.Constants.intNull
		sBranch = String.Empty
		sProduct = String.Empty
		sModulec = String.Empty
		sCover = String.Empty
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
End Class






