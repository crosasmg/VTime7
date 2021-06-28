Option Strict Off
Option Explicit On
Public Class Protection
	'%-------------------------------------------------------%'
	'% $Workfile:: Protection.cls                           $%'
	'% $Author:: Nvaplat15                                  $%'
	'% $Date:: 8/10/04 13.09                                $%'
	'% $Revision:: 16                                       $%'
	'%-------------------------------------------------------%'
	
	'- Información de la tabla Protection en el sistema 16/11/2000
	'- Los campos llave corresponden a:
	'- sCertype, nBranch, nProduct, nPolicy, nCertif, nElement, dEffecdate
	
	'**- Public variable declaration of the class
	'- Declaración de variables Públicas de la Clase
	
	'Column_name                        Type          Computed   Length      Prec  Scale Nullable   TrimTrailingBlanks     FixedLenNullInSource
	Public sCertype As String 'char          no         1                       no         no                     no
	Public nBranch As Integer 'smallint      no         2           5     0     no         (n/a)                  (n/a)
	Public nProduct As Integer 'smallint      no         2           5     0     no         (n/a)                  (n/a)
	Public nPolicy As Double 'int           no         4           10    0     no         (n/a)                  (n/a)
	Public nCertif As Double 'int           no         4           10    0     no         (n/a)                  (n/a)
	Public nElement As Integer 'smallint      no         2           5     0     no         (n/a)                  (n/a)
	Public dEffecdate As Date 'datetime      no         8                       no         (n/a)                  (n/a)
	Public nCurrency As Integer 'smallint      no         2           5     0     yes        (n/a)                  (n/a)
	Public nDiscount As Double 'decimal       no         9           10    2     yes        (n/a)                  (n/a)
	Public dCompdate As Date 'datetime      no         8                       yes        (n/a)                  (n/a)
	Public nDisRate As Double 'decimal       no         5           4     2     yes        (n/a)                  (n/a)
	Public nMaxamount As Double 'decimal       no         9           10    2     yes        (n/a)                  (n/a)
	Public nMinamount As Double 'decimal       no         5           8     2     yes        (n/a)                  (n/a)
	Public dNulldate As Date 'datetime      no         8                       yes        (n/a)                  (n/a)
	Public nUsercode As Integer 'smallint      no         2           5     0     yes        (n/a)                  (n/a)
	
	'- Declaración de variables Públicas Auxiliares de la Clase
	'- En la variable sDescript se almacenará el valor de los campos "sDecript" y "sShort_des"
	'-  que devuelve el Stored Procedure insudb.reatab_protec_1
	Public sDescript As String
	Public sSelection As String
	Public PnDiscount As Double
	Public PnDisrate As Double
	Public PnCurrency As Integer
	
	'**% Add: Function that returns TRUE in case of successfully keeping the records in the data base.
	'% Add: Función que retorna VERDADERO en caso de almacenar exitosamente los registros en la base de datos
	Public Function Add() As Boolean
		Dim lrecinsProtection As eRemoteDB.Execute
		
		On Error GoTo Add_err
		
		lrecinsProtection = New eRemoteDB.Execute
		
		'**+ Parameter definition for stored procedure 'insudb.insProtection'
		'+Definición de parámetros para stored procedure 'insudb.insProtection'
		'**+ Information read on Novemeber 17,2000  12:05:55 p.m.
		'+Información leída el 17/11/2000 12:05:55 PM
		With lrecinsProtection
			.StoredProcedure = "insProtection"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nElement", nElement, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDiscount", nDiscount, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDisrate", nDisRate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 4, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nMaxamount", nMaxamount, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nMinamount", nMinamount, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCurrency", nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Add = .Run(False)
		End With
		'UPGRADE_NOTE: Object lrecinsProtection may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecinsProtection = Nothing
		
Add_err: 
		If Err.Number Then
			Add = False
		End If
		On Error GoTo 0
	End Function
	
	'**% Update: function that returns TRUE in case of successfully update the records in the data base.
	'% Update: Función que retorna VERDADERO en caso de atualizar exitosamente los registros en la base de datos
	Public Function Update() As Boolean
		Dim lrecinsProtection As eRemoteDB.Execute
		
		On Error GoTo Update_Err
		
		lrecinsProtection = New eRemoteDB.Execute
		
		'**+ Parameter definition for stored procedure 'insudb.insProtection'
		'+Definición de parámetros para stored procedure 'insudb.insProtection'
		'**+ Information read on November 17,2000  12:05:55 p.m.
		'+Información leída el 17/11/2000 12:05:55 PM
		With lrecinsProtection
			.StoredProcedure = "insProtection"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nElement", nElement, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDiscount", nDiscount, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDisrate", nDisRate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 4, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nMaxamount", nMaxamount, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nMinamount", nMinamount, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCurrency", nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Update = .Run(False)
		End With
		'UPGRADE_NOTE: Object lrecinsProtection may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecinsProtection = Nothing
		
Update_Err: 
		If Err.Number Then
			Update = False
		End If
		On Error GoTo 0
	End Function
	
	'**% Delete: function that returns TRUE in case of successfully delete the record of the data base.
	'% Delete: Función que retorna VERDADERO en caso de eliminar exitosamente los registros de la base de datos
	Public Function Delete() As Boolean
		Dim lrecdelProtection As eRemoteDB.Execute
		
		On Error GoTo Delete_err
		
		lrecdelProtection = New eRemoteDB.Execute
		
		'**+ Parameter definition for stored procedure 'insudb.delProtection'
		'+Definición de parámetros para stored procedure 'insudb.delProtection'
		'**+ Information read on November 17,2000  01:59:22 p.m.
		'+Información leída el 17/11/2000 01:59:22 PM
		With lrecdelProtection
			.StoredProcedure = "delProtection"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nElement", nElement, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Delete = .Run(False)
		End With
		'UPGRADE_NOTE: Object lrecdelProtection may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecdelProtection = Nothing
		
Delete_err: 
		If Err.Number Then
			Delete = False
		End If
		On Error GoTo 0
	End Function
	
	'% insValCA012: Realiza la validación de los campos a actualizar en la ventana CA012
	Public Function insValCA012(ByVal sCodispl As String, ByVal nCode As Integer, ByVal sDescript As String, ByVal nDisRate As Double, ByVal nCurrency As Integer, ByVal nDisAmount As Double) As String
		Dim lintSubscript As Integer
		Dim lobjErrors As eFunctions.Errors
		
		On Error GoTo insValCA012_Err
		
		lobjErrors = New eFunctions.Errors
		'+ Si no existe un importe asociado al elemento, el campo MONEDA no debe estar lleno
		If (nDisAmount = eRemoteDB.Constants.intNull) And (nCurrency <> eRemoteDB.Constants.intNull) Then
			Call lobjErrors.ErrorMessage(sCodispl, 11417)
		End If
		
		If (nDisAmount <> eRemoteDB.Constants.intNull) And (nDisAmount <> 0) And (nCurrency = eRemoteDB.Constants.intNull Or nCurrency = 0) Then
			Call lobjErrors.ErrorMessage(sCodispl, 750024)
		End If
		
		
		insValCA012 = lobjErrors.Confirm
		
insValCA012_Err: 
		If Err.Number Then
			insValCA012 = "insValCA012: " & Err.Description
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lobjErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lobjErrors = Nothing
	End Function
	
	'% insPostCA012: Se realiza la actualización de los datos
	Public Function insPostCA012(ByVal sAction As String, ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal nElement As Integer, ByVal dEffecdate As Date, Optional ByVal nCurrency As Integer = 0, Optional ByVal nDiscount As Double = 0, Optional ByVal dCompdate As Date = #12:00:00 AM#, Optional ByVal nDisRate As Double = 0, Optional ByVal nMaxamount As Double = 0, Optional ByVal nMinamount As Double = 0, Optional ByVal dNulldate As Date = #12:00:00 AM#, Optional ByVal nUsercode As Integer = 0, Optional ByVal sDescript As String = "") As Boolean
		Dim lclsPolicyWin As ePolicy.Policy_Win
		
		On Error GoTo insPostCA012_Err
		With Me
			.sCertype = sCertype
			.nBranch = nBranch
			.nProduct = nProduct
			.nPolicy = nPolicy
			.nCertif = nCertif
			.nElement = nElement
			.dEffecdate = dEffecdate
			.nCurrency = nCurrency
			.nDiscount = nDiscount
			.dCompdate = dCompdate
			.nDisRate = nDisRate
			.nMaxamount = nMaxamount
			.nMinamount = nMinamount
			.dNulldate = dNulldate
			.nUsercode = nUsercode
			.sDescript = sDescript
			
			Select Case sAction
				Case "Add"
					insPostCA012 = .Add
					
				Case "Update"
					insPostCA012 = .Update
					
				Case "Del"
					insPostCA012 = .Delete
			End Select
		End With
		
		If insPostCA012 Then
			lclsPolicyWin = New ePolicy.Policy_Win
			If Me.Find Then
				Call lclsPolicyWin.Add_PolicyWin(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate, nUsercode, "CA012", "2")
				Call lclsPolicyWin.Add_PolicyWin(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate, nUsercode, "CA016", "3")
			Else
				Call lclsPolicyWin.Add_PolicyWin(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate, nUsercode, "CA012", "1")
				Call lclsPolicyWin.Add_PolicyWin(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate, nUsercode, "CA016", "3")
			End If
			
			Call lclsPolicyWin.Add_PolicyWin(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate, nUsercode, "CA017", "1")
			
		End If
		
		
insPostCA012_Err: 
		If Err.Number Then
			insPostCA012 = False
		End If
		'UPGRADE_NOTE: Object lclsPolicyWin may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsPolicyWin = Nothing
		On Error GoTo 0
	End Function
	
	'% Find: Función que retorna la cantidad de elementos en la poliza
	Public Function Find() As Boolean
		Dim lrecFindProtection As eRemoteDB.Execute
		Dim lintCount As Integer
		On Error GoTo Find_Err
		
		lrecFindProtection = New eRemoteDB.Execute
		
		'**+ Parameter definition for stored procedure 'insudb.delProtection'
		'+Definición de parámetros para stored procedure 'insudb.delProtection'
		'**+ Information read on November 17,2000  01:59:22 p.m.
		'+Información leída el 17/11/2000 01:59:22 PM
		With lrecFindProtection
			.StoredProcedure = "reaProtection"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				lintCount = .FieldToClass("nCount")
				If lintCount > 0 Then
					Find = True
				End If
				.RCloseRec()
			Else
				Find = False
			End If
		End With
		'UPGRADE_NOTE: Object lrecFindProtection may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecFindProtection = Nothing
		
Find_Err: 
		If Err.Number Then
			Find = False
		End If
		On Error GoTo 0
	End Function
End Class






