Option Strict Off
Option Explicit On
<System.Runtime.InteropServices.ProgId("Deduc_auto_NET.Deduc_auto")> Public Class Deduc_auto
	'%-------------------------------------------------------%'
	'% $Workfile:: Deduc_Auto.cls                           $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 1:38p                                $%'
	'% $Revision:: 12                                       $%'
	'%-------------------------------------------------------%'
	
	'+ Propiedades según la tabla en el sistema el 22/11/2001
	
	'   Column_name                 Type
	'------------------------------ ----------
	Public nVehType As Integer
	Public nDeduc As Double
	Public dEffecdate As Date
	Public nDiscount As Double
	Public dNulldate As Date
	Public nUsercode As Integer
	Public nStatusInstance As Integer
	
	Private pLastdate As Date
	
	Private Structure udtDeduc_Auto
		Dim nDeduc As Double
		Dim nDiscount As Double
	End Structure
	
	Private arrDeduc_Auto() As udtDeduc_Auto
	
	'% Count: devuelve el número de elementos que contiene el arreglo
	Public ReadOnly Property Count() As Integer
		Get
			Count = UBound(arrDeduc_Auto)
		End Get
	End Property
	
	'% ItemDeduc_Auto: asigna los valores del arreglo a las variables públicas de la clase
	Public Function ItemDeduc_Auto(ByVal lintindex As Integer) As Boolean
		If lintindex <= UBound(arrDeduc_Auto) Then
			With arrDeduc_Auto(lintindex)
				nDeduc = .nDeduc
				nDiscount = .nDiscount
			End With
			ItemDeduc_Auto = True
		Else
			ItemDeduc_Auto = False
		End If
	End Function
	
	'% Find: se buscan los datos para un tipo de vehículo a una fecha dada
	Public Function Find(ByVal nVehType As Integer, ByVal dEffecdate As Date) As Boolean
		Dim lrecDeduc_auto As eRemoteDB.Execute
		Dim lintCount As Integer
		
		On Error GoTo Find_Err
		
		lrecDeduc_auto = New eRemoteDB.Execute
		
		'+ Definición de parámetros para stored procedure 'insudb.reaDeduc_auto'
		'+ Información leída el 22/11/2001 05:20:48 p.m.
		
		With lrecDeduc_auto
			.StoredProcedure = "reaDeduc_auto"
			.Parameters.Add("sShowNum", "1", eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
			.Parameters.Add("sCondition", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 255, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nVehType", nVehType, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run Then
				ReDim arrDeduc_Auto(1000)
				lintCount = 0
				
				Do While Not .EOF
					'+Se vacia la información en el arreglo que contiene toda la información que se mostrará
					'+en los objetos
					arrDeduc_Auto(lintCount).nDeduc = .FieldToClass("nDeduc")
					arrDeduc_Auto(lintCount).nDiscount = .FieldToClass("nDiscount")
					.RNext()
					lintCount = lintCount + 1
				Loop 
				.RCloseRec()
				
				'+Se reajusta el tamaño del arreglo a la cantidad de datos a mostrar
				ReDim Preserve arrDeduc_Auto(lintCount)
				Find = True
			End If
		End With
		
Find_Err: 
		If Err.Number Then
			Find = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecDeduc_auto may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecDeduc_auto = Nothing
	End Function
	
	'% Update: se actualiza un registro de la tabla
	Public Function Update() As Boolean
		Dim lrecinsDeduc_auto As eRemoteDB.Execute
		
		On Error GoTo Update_Err
		
		lrecinsDeduc_auto = New eRemoteDB.Execute
		
		'+ Definición de parámetros para stored procedure 'insudb.insDeduc_auto'
		'+ Información leída el 23/11/2001 10:59:25 a.m.
		
		With lrecinsDeduc_auto
			.StoredProcedure = "insDeduc_auto"
			.Parameters.Add("nVehType", nVehType, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDeduc", nDeduc, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 4, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDiscount", nDiscount, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 4, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Update = .Run(False)
		End With
		
Update_Err: 
		If Err.Number Then
			Update = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecinsDeduc_auto may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecinsDeduc_auto = Nothing
	End Function
	
	'% Delete: se elimina un registro de la tabla
	Public Function Delete() As Boolean
		Dim lrecDeduc_auto As eRemoteDB.Execute
		
		On Error GoTo Delete_Err
		
		'+ Definición de parámetros para stored procedure 'insudb.insDelDeduc_auto'
		'+ Información leída el 23/11/2001 08:38:21 a.m.
		lrecDeduc_auto = New eRemoteDB.Execute
		With lrecDeduc_auto
			.StoredProcedure = "insDelDeduc_auto"
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nVehType", nVehType, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDeduc", nDeduc, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 4, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Delete = .Run(False)
		End With
		
Delete_Err: 
		If Err.Number Then
			Delete = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecDeduc_auto may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecDeduc_auto = Nothing
	End Function
	
	'% Add: se agrega un registro en la tabla
	Public Function Add() As Boolean
		Add = Update
	End Function
	
	'% insValDeduc_auto: Función que se encarga de traer la máxima fecha de efecto para un tipo
	'%                   de vehículo determinado
	Private Function insValDeduc_auto_date(ByVal nVehType As Integer) As Date
		Dim lrecreaDeduc_auto_p As eRemoteDB.Execute
		
		On Error GoTo insValDeduc_auto_date_err
		
		lrecreaDeduc_auto_p = New eRemoteDB.Execute
		
		'+ Definición de parámetros para stored procedure 'insudb.reaDeduc_auto_p'
		'+ Información leída el 22/11/2001 07:04:53 p.m.
		
		With lrecreaDeduc_auto_p
			.StoredProcedure = "reaDeduc_auto_p"
			.Parameters.Add("nVehType", nVehType, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				insValDeduc_auto_date = .FieldToClass("dEffecdate")
				.RCloseRec()
			End If
		End With
		
insValDeduc_auto_date_err: 
		If Err.Number Then
			insValDeduc_auto_date = CDate(Nothing)
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecreaDeduc_auto_p may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaDeduc_auto_p = Nothing
	End Function
	
	'%insValDeduc_auto_date2: Función que se encarga de traer la máxima fecha de efecto para un tipo
	'%de vehículo determinado
	Private Function insValDeduc_auto_date2(ByVal nVehType As Integer, ByVal nDeduc As Double) As Date
		Dim lrecreaDeduc_auto_p2 As eRemoteDB.Execute
		
		On Error GoTo insValDeduc_auto_date2_err
		
		lrecreaDeduc_auto_p2 = New eRemoteDB.Execute
		
		'+ Definición de parámetros para stored procedure 'insudb.reaDeduc_auto_p2'
		'+ Información leída el 22/11/2001 07:05:14 p.m.
		
		With lrecreaDeduc_auto_p2
			.StoredProcedure = "reaDeduc_auto_p2"
			.Parameters.Add("nVehType", nVehType, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDeduc", nDeduc, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 4, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				insValDeduc_auto_date2 = .FieldToClass("dEffecdate")
				.RCloseRec()
			End If
		End With
		
insValDeduc_auto_date2_err: 
		If Err.Number Then
			insValDeduc_auto_date2 = CDate(Nothing)
		End If
		'UPGRADE_NOTE: Object lrecreaDeduc_auto_p2 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaDeduc_auto_p2 = Nothing
	End Function
	
	'% insValDeduc_auto: se verifica que el deducible para determinado tipo de automóvil que
	'%                   se está registrando no esté previamente registrada en la tabla
	'%                   Deduc_auto
	Private Function insValDeduc_auto(ByVal nVehType As Double, ByVal nDeduc As Double) As Boolean
		Dim lrecreaDeduc_auto_v As eRemoteDB.Execute
		
		On Error GoTo insValDeduc_auto_Err
		
		lrecreaDeduc_auto_v = New eRemoteDB.Execute
		
		insValDeduc_auto = False
		
		pLastdate = dtmNull
		
		'+ Definición de parámetros para stored procedure 'insudb.reaDeduc_auto_v'
		'+ Información leída el 22/11/2001 07:06:31 p.m.
		
		With lrecreaDeduc_auto_v
			.StoredProcedure = "reaDeduc_auto_v"
			.Parameters.Add("nVehType", nVehType, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDeduc", nDeduc, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 4, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				pLastdate = .FieldToClass("dEffecdate")
				.RCloseRec()
				insValDeduc_auto = True
			End If
		End With
		
insValDeduc_auto_Err: 
		If Err.Number Then
			insValDeduc_auto = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecreaDeduc_auto_v may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaDeduc_auto_v = Nothing
	End Function
	
	'%insValMAU101_k: Esta función se encarga de validar los datos introducidos en la cabecera de la forma.
	Public Function insValMAU101_k(ByVal sCodispl As String, ByVal nAction As Integer, ByVal nVehType As Integer, ByVal dEffecdate As Date) As String
		Dim lclsError As eFunctions.Errors
		Dim ldtmMaxDate As Date
		
		On Error GoTo insValMAU101_k_Err
		
		lclsError = New eFunctions.Errors
		
		With lclsError
			'+ Validacion del Tipo de Vehículo
			If nVehType = eRemoteDB.Constants.intNull Then
				'+ Debe estar lleno
				Call .ErrorMessage(sCodispl, 13988)
			End If
			
			'+Validacion de la Fecha
			If dEffecdate = dtmNull Then
				'+ Debe estar llena
				Call .ErrorMessage(sCodispl, 2056)
			Else
				If nAction = eFunctions.Menues.TypeActions.clngActionUpdate Then
					'+ La fecha debe ser mayor al día
					If dEffecdate <= Today Then
						.sTypeMessage = eFunctions.Errors.ErrorsType.ErrorTyp
						Call .ErrorMessage(sCodispl, 10868)
					Else
						'+ La fecha debe ser mayor a la fecha de última modificación
						ldtmMaxDate = insValDeduc_auto_date(nVehType)
						If ldtmMaxDate <> dtmNull Then
							If dEffecdate <= ldtmMaxDate Then
								.sTypeMessage = eFunctions.Errors.ErrorsType.ErrorTyp
								Call .ErrorMessage(sCodispl, 10869,  , eFunctions.Errors.TextAlign.RigthAling, "(" & ldtmMaxDate & ")")
							End If
						End If
					End If
				End If
			End If
			
			insValMAU101_k = .Confirm
		End With
		
insValMAU101_k_Err: 
		If Err.Number Then
			insValMAU101_k = "insValMAU101_k" & Err.Description
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lclsError may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsError = Nothing
	End Function
	
	'%insValMAU101: Esta función se encarga de validar los datos introducidos en la zona de detalle de la forma.
	Public Function insValMAU101(ByVal sCodispl As String, ByVal sAction As String, ByVal nDeduc As Double, ByVal nDiscount As Double, ByVal nVehType As Integer) As String
		Dim lclsError As eFunctions.Errors
		Dim ldtmMaxDate As Date
		
		On Error GoTo insValMAU101_Err
		
		lclsError = New eFunctions.Errors
		
		With lclsError
			If nDeduc = eRemoteDB.Constants.intNull Then
				'+ El deduicible debe estar lleno
				Call .ErrorMessage(sCodispl, 1012,  , eFunctions.Errors.TextAlign.LeftAling, "Deducible:")
			Else
				'+ Si la acción es registrar, el deducible no debe estar en la tabla
				If sAction = "Add" Then
					If insValDeduc_auto(nVehType, nDeduc) Then
						ldtmMaxDate = insValDeduc_auto_date2(nVehType, nDeduc)
						If ldtmMaxDate <> pLastdate Or dEffecdate < pLastdate Then
							Call lclsError.ErrorMessage(sCodispl, 3949)
						End If
					End If
				End If
				
				'+ Si el deducible está lleno, el descuento debe estar lleno
				If nDiscount = eRemoteDB.Constants.intNull Then
					Call lclsError.ErrorMessage(sCodispl, 10151)
				End If
			End If
			
			insValMAU101 = lclsError.Confirm
		End With
		
insValMAU101_Err: 
		If Err.Number Then
			insValMAU101 = "insValMAU101:" & Err.Description
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lclsError may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsError = Nothing
	End Function
	
	'%insPostFolder: Esta función se encaga de validar todos los datos introducidos en la forma
	Public Function insPostMAU101(ByVal sCodispl As String, ByVal sAction As String, ByVal nDeduc As Double, ByVal nDiscount As Double, ByVal nVehType As Integer, ByVal dEffecdate As Date, ByVal nUsercode As Integer) As Boolean
		On Error GoTo insPostMAU101_Err
		With Me
			.nDeduc = nDeduc
			.nDiscount = nDiscount
			.nVehType = nVehType
			.dEffecdate = dEffecdate
			.nUsercode = nUsercode
			
			Select Case sAction
				Case "Add"
					insPostMAU101 = .Add
					
				Case "Update"
					insPostMAU101 = .Update
					
				Case "Del"
					insPostMAU101 = .Delete
			End Select
		End With
		
insPostMAU101_Err: 
		If Err.Number Then
			insPostMAU101 = False
		End If
		On Error GoTo 0
	End Function
End Class






