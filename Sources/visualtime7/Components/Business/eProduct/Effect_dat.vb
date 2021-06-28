Option Strict Off
Option Explicit On
Public Class Effect_dat
	'%-------------------------------------------------------%'
	'% $Workfile:: Effect_dat.cls                           $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 12:35p                               $%'
	'% $Revision:: 9                                        $%'
	'%-------------------------------------------------------%'
	
	'- Propiedades según la tabla en el sistema 10/07/2002
	'- Los campos llave corresponden a nDaynumen, nBranch, nDaynumin, nProduct, dEffecdate
	
	'+ Column_name        Type
	'-------------------- ----------------------
	Public nDaynumen As Integer 'NUMBER(5)
	Public nBranch As Integer 'NUMBER(5)
	Public nDaynumin As Integer 'NUMBER(5)
	Public nProduct As Integer 'NUMBER(5)
	Public dEffecdate As Date 'DATETIME
	Public nDayadd As Integer 'NUMBER(5)
	Public nValuesmo As Integer 'NUMBER(10)
	Public nValuesty As Integer 'NUMBER(10)
	Public nUsercode As Integer 'NUMBER(5)
	
	'% ValRange: Permite verificar si el dia se encuentra dentro de otro rango
	Public Function ValRange(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nDay As Integer, ByVal dEffecdate As Date) As Boolean
		Dim lrecinsValRangeEffect_dat As eRemoteDB.Execute
		
		On Error GoTo ValRange_err
		
		lrecinsValRangeEffect_dat = New eRemoteDB.Execute
		
		'+Definición de parámetros para stored procedure 'insudb.insValRangeEffect_dat'
		'+Información leída el 22/11/1999 10:41:47
		
		With lrecinsValRangeEffect_dat
			.StoredProcedure = "insValRangeEffect_dat"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDay", nDay, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				ValRange = True
				.RCloseRec()
			End If
		End With
		
ValRange_err: 
		If Err.Number Then
			ValRange = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecinsValRangeEffect_dat may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecinsValRangeEffect_dat = Nothing
	End Function
	
	'%ValOtherRange : Permite verificar si el rango de días se encuentra contenido dentro de otro rango.
	Public Function ValOtherRange(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nDaynumin As Integer, ByVal nDaynumen As Integer, ByVal dEffecdate As Date) As Boolean
		Dim lrecinsValRangeEffect_dat_1 As eRemoteDB.Execute
		Static lblnRead As Boolean
		
		On Error GoTo valOtherRange_err
		
		lrecinsValRangeEffect_dat_1 = New eRemoteDB.Execute
		
		If Me.nBranch <> nBranch Or Me.nProduct <> nProduct Or Me.nDaynumin <> nDaynumin Or Me.nDaynumen <> nDaynumen Or Me.dEffecdate <> dEffecdate Then
			
			With lrecinsValRangeEffect_dat_1
				.StoredProcedure = "insValRangeEffect_dat_1"
				.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nDaynumin", nDaynumin, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nDaynumen", nDaynumen, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				If .Run Then
					Me.nBranch = nBranch
					Me.nProduct = nProduct
					Me.nDaynumin = nDaynumin
					Me.nDaynumen = nDaynumen
					Me.dEffecdate = dEffecdate
					lblnRead = True
					.RCloseRec()
				End If
			End With
		End If
		
		ValOtherRange = lblnRead
		
valOtherRange_err: 
		If Err.Number Then
			ValOtherRange = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecinsValRangeEffect_dat_1 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecinsValRangeEffect_dat_1 = Nothing
	End Function
	
	'% FindSpecificDate: Función que retorna la fecha efectiva de un aporte
	Public Function FindSpecificDate() As Boolean
		Dim lrecReaEffect_dat As eRemoteDB.Execute
		
		On Error GoTo FindSpecificDate_err
		
		lrecReaEffect_dat = New eRemoteDB.Execute
		
		'+Definición de parámetros para stored procedure 'insudb.ReaEffect_dat'
		'+Información leída el 17/11/1999 09:53:03 AM
		
		With lrecReaEffect_dat
			.StoredProcedure = "ReaEffect_dat"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				nDaynumen = .FieldToClass("nDaynumen")
				nBranch = .FieldToClass("nBranch")
				nDaynumin = .FieldToClass("nDaynumin")
				nProduct = .FieldToClass("nProduct")
				dEffecdate = .FieldToClass("dEffecdate")
				nDayadd = .FieldToClass("nDayadd")
				nValuesmo = .FieldToClass("nValuesmo")
				nValuesty = .FieldToClass("nValuesty")
				FindSpecificDate = True
				.RCloseRec()
			End If
		End With
FindSpecificDate_err: 
		If Err.Number Then
			FindSpecificDate = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecReaEffect_dat may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecReaEffect_dat = Nothing
	End Function
	
	'% Add: Este método se encarga de agregar nuevos registros a la tabla "Effect_dat"
	Public Function Add() As Boolean
		Dim lreccreEffect_dat As eRemoteDB.Execute
		
		On Error GoTo Add_err
		
		lreccreEffect_dat = New eRemoteDB.Execute
		
		'+Definición de parámetros para stored procedure 'insudb.creEffect_dat'
		'+Información leída el 22/11/1999 14:01:51
		
		With lreccreEffect_dat
			.StoredProcedure = "creEffect_dat"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDaynumin", nDaynumin, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDaynumen", nDaynumen, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDayadd", nDayadd, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nValuesmo", nValuesmo, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nValuesty", nValuesty, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Add = .Run(False)
		End With
		
Add_err: 
		If Err.Number Then
			Add = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lreccreEffect_dat may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lreccreEffect_dat = Nothing
	End Function
	
	'%Update: Este método se encarga de actualizar registros en la tabla "Effect_dat"
	Public Function Update() As Boolean
		Dim lrecupdEffect_dat As eRemoteDB.Execute
		
		On Error GoTo Update_Err
		
		lrecupdEffect_dat = New eRemoteDB.Execute
		
		'+Definición de parámetros para stored procedure 'insudb.updEffect_dat'
		'+Información leída el 22/11/1999 15:18:32
		
		With lrecupdEffect_dat
			.StoredProcedure = "updEffect_dat"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDaynumen", nDaynumen, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDaynumin", nDaynumin, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDayadd", nDayadd, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nValuesmo", nValuesmo, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nValuesty", nValuesty, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Update = .Run(False)
		End With
Update_Err: 
		If Err.Number Then
			Update = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecupdEffect_dat may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecupdEffect_dat = Nothing
	End Function
	
	'%Delete: Este método se encarga de eliminar registros en la tabla "Effect_dat"
	Public Function Delete() As Boolean
		Dim lrecdelEffect_dat As eRemoteDB.Execute
		
		On Error GoTo Delete_Err
		
		lrecdelEffect_dat = New eRemoteDB.Execute
		
		'+Definición de parámetros para stored procedure 'insudb.delEffect_dat'
		'+Información leída el 22/11/1999 15:39:44
		
		With lrecdelEffect_dat
			.StoredProcedure = "delEffect_dat"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDaynumen", nDaynumen, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDaynumin", nDaynumin, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Delete = .Run(False)
		End With
Delete_Err: 
		If Err.Number Then
			Delete = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecdelEffect_dat may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecdelEffect_dat = Nothing
	End Function
	
	'%insValDP047: Verifica los datos del frame de Fecha efectiva del aporte
	Public Function insValDP047(ByVal sCodispl As String, ByVal sAction As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal dEffecdate As Date, ByVal nDaynumin As Integer, ByVal nDaynumen As Integer, ByVal nDayadd As Integer, ByVal nValuesty As Integer, ByVal nValuesmo As Integer) As String
		Dim lclsErrors As eFunctions.Errors
		
		On Error GoTo insValDP047_Err
		
		lclsErrors = New eFunctions.Errors
		
		With lclsErrors
			
			'+ Validación del "Día inicial".
			If nDaynumin = eRemoteDB.Constants.intNull Then
				Call .ErrorMessage(sCodispl, 17005)
			ElseIf nDaynumin < 1 Or nDaynumin > 31 Then 
				Call .ErrorMessage(sCodispl, 1935,  , eFunctions.Errors.TextAlign.LeftAling, "Día inicial:")
			End If
			
			'+ Validación del "Día final".
			If nDaynumen = eRemoteDB.Constants.intNull Then
				Call .ErrorMessage(sCodispl, 17006)
			ElseIf nDaynumen < 1 Or nDaynumen > 31 Then 
				Call .ErrorMessage(sCodispl, 1935,  , eFunctions.Errors.TextAlign.LeftAling, "Día final:")
			End If
			
			If nDaynumen <> eRemoteDB.Constants.intNull And nDaynumin <> eRemoteDB.Constants.intNull Then
				If nDaynumin >= nDaynumen Then
					Call .ErrorMessage(sCodispl, 10184)
				End If
				If sAction <> "Update" Then
					If ValOtherRange(nBranch, nProduct, nDaynumin, nDaynumen, dEffecdate) Then
						Call .ErrorMessage(sCodispl, 11138)
					End If
				End If
			End If
			
			'+ Validación del "Dia efectivo".
			If nDayadd <> eRemoteDB.Constants.intNull And (nDayadd < 1 Or nDayadd > 31) Then
				Call .ErrorMessage(sCodispl, 1935,  , eFunctions.Errors.TextAlign.LeftAling, "Día:")
			End If
			
			'+ Validación del "Mes efectivo".
			If nValuesty = 2 Or nValuesty = 3 Then
				If nDayadd = eRemoteDB.Constants.intNull Then
					Call .ErrorMessage(sCodispl, 11141)
				End If
				If nValuesmo = eRemoteDB.Constants.intNull Then
					Call .ErrorMessage(sCodispl, 11142)
				End If
			End If
			
			insValDP047 = .Confirm
		End With
		
insValDP047_Err: 
		If Err.Number Then
			insValDP047 = "insValDP047" & Err.Description
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
	End Function
	
	'%insPostDP047: actualiza los datos de la ventana en la BD
	Public Function insPostDP047(ByVal sCodispl As String, ByVal sAction As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal dEffecdate As Date, ByVal nValuesty As Integer, ByVal nValuesmo As Integer, ByVal nDaynumin As Integer, ByVal nDaynumen As Integer, ByVal nDayadd As Integer, ByVal nUsercode As Integer) As Boolean
		Dim lclsProd_win As Prod_win
		
		On Error GoTo insPostDP047_Err
		
		With Me
			.nBranch = nBranch
			.nProduct = nProduct
			.dEffecdate = dEffecdate
			.nDaynumin = nDaynumin
			.nDaynumen = nDaynumen
			.nValuesty = nValuesty
			.nValuesmo = nValuesmo
			.nDayadd = IIf(nDayadd = eRemoteDB.Constants.intNull, 0, nDayadd)
			.nUsercode = nUsercode
			If sAction = "Add" Then
				insPostDP047 = .Add
			ElseIf sAction = "Update" Then 
				insPostDP047 = .Update
			Else
				insPostDP047 = .Delete
			End If
			If insPostDP047 Then
				lclsProd_win = New Prod_win
				insPostDP047 = lclsProd_win.Add_Prod_win(nBranch, nProduct, dEffecdate, sCodispl, "2", nUsercode)
			End If
		End With
		
insPostDP047_Err: 
		If Err.Number Then
			insPostDP047 = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lclsProd_win may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsProd_win = Nothing
	End Function
	
	'%InitValues: Se inicializan los valores de las variables públicas de la clase
	Private Sub InitValues()
		nDaynumen = eRemoteDB.Constants.intNull
		nBranch = eRemoteDB.Constants.intNull
		nDaynumin = eRemoteDB.Constants.intNull
		nProduct = eRemoteDB.Constants.intNull
		dEffecdate = CDate(Nothing)
		nDayadd = eRemoteDB.Constants.intNull
		nValuesmo = eRemoteDB.Constants.intNull
		nValuesty = eRemoteDB.Constants.intNull
	End Sub
	
	'%Class_Initialize: Se controla la creación del objeto de la clase
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		Call InitValues()
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
End Class






