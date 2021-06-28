Option Strict Off
Option Explicit On
<System.Runtime.InteropServices.ProgId("Tab_Wincli_NET.Tab_Wincli")> Public Class Tab_Wincli
	'%-------------------------------------------------------%'
	'% $Workfile:: Tab_WinCli.cls                           $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 1:30p                                $%'
	'% $Revision:: 14                                       $%'
	'%-------------------------------------------------------%'
	
	'La llave está conformada por sType_clie, sType_seq, nSequence
	'Column_name        Type                   Computed   Length      Prec  Scale Nullable   TrimTrailingBlanks   FixedLenNullInSource   Collation
	'------------------ ---------------------- ---------- ----------- ----- ----- ---------- -------------------- ---------------------- -----------------------------
	Public nChecked As Integer 'Integer
	Public sType_Clie As String 'char       no         1                       no         no                   no                     SQL_Latin1_General_CP1_CI_AS
	Public sType_seq As String 'char       no         3                       no         no                   no                     SQL_Latin1_General_CP1_CI_AS
	Public nSequence As Integer 'smallint   no         2           5     0     no         (n/a)                (n/a)                  NULL
	Public sCodispl As String 'char       no         8                       no         no                   no                     SQL_Latin1_General_CP1_CI_AS
	Public sDefaulti As String 'char       no         1                       yes        no                   yes                    SQL_Latin1_General_CP1_CI_AS
	Public sRequire As String 'char       no         1                       yes        no                   yes                    SQL_Latin1_General_CP1_CI_AS
	Public nUsercode As Integer ' NUMBER   22   0     5    N
	Public sDescript As String
	Public sExist As String
	Public nIndex As Integer
	
	'% Find: Lee los datos de la secuencia de clientes
	Public Function Find(ByVal sType_Clie As String, ByVal sType_seq As String, ByVal sCodispl As String) As Boolean
		
		'- Se define variable que almacena las propiedades y metodos de la clase principal
		Dim lrecTabWincli As eRemoteDB.Execute
		
		On Error GoTo Find_Tab_WinCli_Err
		lrecTabWincli = New eRemoteDB.Execute
		
		With lrecTabWincli
			.StoredProcedure = "reaTab_wincli2"
			.Parameters.Add("sType_clie", sType_Clie, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sType_seq", sType_seq, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 3, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sCodispl", sCodispl, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 8, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				Me.sType_Clie = .FieldToClass("sType_clie")
				Me.sType_seq = .FieldToClass("sType_seq")
				Me.sCodispl = .FieldToClass("sCodispl")
				nSequence = .FieldToClass("nSequence")
				sDefaulti = .FieldToClass("sDefaulti")
				sRequire = .FieldToClass("sRequire")
				nUsercode = .FieldToClass("nUsercode")
				sDescript = .FieldToClass("sDescript")
				Find = True
				.RCloseRec()
			End If
		End With
		
Find_Tab_WinCli_Err: 
		If Err.Number Then
			Find = False
		End If
		'UPGRADE_NOTE: Object lrecTabWincli may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecTabWincli = Nothing
		On Error GoTo 0
	End Function
	
	'% InsUpdtab_wincli: Realiza la actualización de la tabla
	Private Function InsUpdtab_wincli(ByVal nAction As Integer) As Boolean
		Dim lrecInsUpdtab_wincli As eRemoteDB.Execute
		
		On Error GoTo InsUpdtab_wincli_Err
		lrecInsUpdtab_wincli = New eRemoteDB.Execute
		
		With lrecInsUpdtab_wincli
			.StoredProcedure = "InsUpdtab_wincli"
			.Parameters.Add("nAction", nAction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sType_clie", sType_Clie, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sType_seq", sType_seq, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 3, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nSequence", nSequence, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sCodispl", sCodispl, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 8, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sDefaulti", sDefaulti, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sRequire", sRequire, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			InsUpdtab_wincli = .Run(False)
		End With
		
InsUpdtab_wincli_Err: 
		If Err.Number Then
			InsUpdtab_wincli = False
		End If
		'UPGRADE_NOTE: Object lrecInsUpdtab_wincli may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecInsUpdtab_wincli = Nothing
		On Error GoTo 0
	End Function
	
	'% Add: Crea un registro en la tabla
	Public Function Add() As Boolean
		Add = InsUpdtab_wincli(1)
	End Function
	
	'% Delete: Borra los datos de la tabla
	Public Function Delete() As Boolean
		Delete = InsUpdtab_wincli(3)
	End Function
	
	'% Update: Actualiza los datos de la tabla
	Public Function Update() As Boolean
		Update = InsUpdtab_wincli(2)
	End Function
	
	'% InsValMBC001_K: Esta función se encarga de validar los datos introducidos en la cabecera de la
	'%                 forma.
	Public Function InsValMBC001_K(ByVal sType_Clie As String, ByVal sType_seq As String) As String
		Dim lclsErrors As eFunctions.Errors
		Dim lcolTab_wincli As Tab_Wincli
		
		On Error GoTo InsValMBC001_K_Err
		lclsErrors = New eFunctions.Errors
		lcolTab_wincli = New eClient.Tab_Wincli
		
		If Not lcolTab_wincli.Find(sType_Clie, "3", String.Empty) Then
			If sType_seq = "2" Or sType_seq = "3" Then
				lclsErrors.ErrorMessage("MBC001", 99077)
			End If
		End If
		
		'UPGRADE_NOTE: Object lcolTab_wincli may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lcolTab_wincli = Nothing
		
		InsValMBC001_K = lclsErrors.Confirm
		
InsValMBC001_K_Err: 
		If Err.Number Then
			InsValMBC001_K = "InsValMBC001_K: " & Err.Description
		End If
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
		On Error GoTo 0
	End Function
	
	'% InsValMBC001: Esta función se encarga de validar los datos introducidos en la zona de
	'%               detalle de la forma.
	Public Function InsValMBC001(ByVal nSel As Integer) As String
		'- Se define la variable lclsErrors para el envío de errores de la ventana
		Dim lclsErrors As eFunctions.Errors
		
		lclsErrors = New eFunctions.Errors
		On Error GoTo InsValMBC001_Err
		
		If nSel = eRemoteDB.Constants.intNull Or nSel = 0 Then
			lclsErrors.ErrorMessage("MBC001", 99007)
		End If
		
		InsValMBC001 = lclsErrors.Confirm
		
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
		
InsValMBC001_Err: 
		If Err.Number Then
			InsValMBC001 = lclsErrors.Confirm & Err.Description
		End If
		On Error GoTo 0
	End Function
	
	'%InsPostMBC001: Esta función se encarga de llamar al método correspondiente a la acción
	'%               ejecutada (crear/actualizar/eliminar) sobre las tablas de comisión
	Public Function InsPostMBC001(ByVal sExist As String, ByVal sSel As String, ByVal sType_Clie As String, ByVal sType_seq As String, ByVal nSequence As Integer, ByVal sCodispl As String, ByVal sDefaulti As String, ByVal sRequire As String, ByVal nUsercode As Integer) As Boolean
		Dim lblnAdd As Boolean
		
		On Error GoTo InsPostMBC001_err
		InsPostMBC001 = True
		With Me
			.sType_Clie = sType_Clie
			.sType_seq = sType_seq
			.nSequence = nSequence
			.sCodispl = sCodispl
			.sDefaulti = sDefaulti
			.sRequire = sRequire
			.nUsercode = nUsercode
			'+Si el registro existe y no esta seleccionado
			If sExist = "1" And sSel = "2" Then
				InsPostMBC001 = .Delete
				
				'+Si el registro no existe y esta seleccionado
			ElseIf sExist = "2" And sSel = "1" Then 
				InsPostMBC001 = .Add
				lblnAdd = True
			ElseIf sExist = "1" Then 
				InsPostMBC001 = .Update
			End If
			
			
			'+Si la transacción que se procesa es registrar, se valida
			'+ la existencia de la secuencia de consultar y modificar
			If lblnAdd Then
				If sType_seq = "1" Then
					Me.sDefaulti = "2"
					Me.sRequire = "2"
					
					'+Se valida que ya exista una secuencia de modificar,
					'+de no existir se genera una automática
					If Not Find(sType_Clie, "2", sCodispl) Then
						Me.sType_seq = "2"
						InsPostMBC001 = .Add
					End If
					
					'+Se valida que ya exista una secuencia de consulta,
					'+de no existir se genera una automática
					If Not Find(sType_Clie, "3", sCodispl) Then
						Me.sType_seq = "3"
						InsPostMBC001 = .Add
					End If
					
				End If
			End If
		End With
		
InsPostMBC001_err: 
		If Err.Number Then
			InsPostMBC001 = False
		End If
		On Error GoTo 0
		
	End Function
	
	'% Class_Initialize: el objetivo de esta rutina es la de controlar la apertura de la clase
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		nChecked = eRemoteDB.Constants.intNull
		sType_Clie = String.Empty
		sType_seq = String.Empty
		nSequence = eRemoteDB.Constants.intNull
		sCodispl = String.Empty
		sDefaulti = String.Empty
		sRequire = String.Empty
		nUsercode = eRemoteDB.Constants.intNull
		sDescript = String.Empty
		sExist = String.Empty
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
End Class






