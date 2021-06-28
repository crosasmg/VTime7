Option Strict Off
Option Explicit On
Public Class Disc_pb
	'%-------------------------------------------------------%'
	'% $Workfile:: Disc_pb.cls                              $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 1:38p                                $%'
	'% $Revision:: 8                                        $%'
	'%-------------------------------------------------------%'
	
	'**-Properties according to the table in the system on December 27,2000.
	'*-Propiedades según la tabla en el sistema el 27/12/2000
	
	'Column_name                 Type                  Nulleable
	'---------------------   ------------------------ ---------------
	Public nIntertyp As Integer 'Number          No
	Public nBranch As Integer 'Number(5)       No
	Public nProduct As Integer 'Number(5)       No
	Public nModulec As Integer 'Number(5)       No
	Public nAgreement As Integer 'Number(5)       No
	Public dEffecdate As Date 'Date            No
	Public dNulldate As Date 'Date            Yes
	Public nQPB As Integer 'Number(5)       No
	Public nPercent As Double 'Number(5,2)     Yes
	Public nUsercode As Integer 'Number(5)       No
	
	'%InsUpdDisc_pb: Realiza las actualizaciones de la tabla
	Private Function InsUpdDisc_pb(ByVal nAction As Integer) As Boolean
		Dim lrecInsUpdDisc_pb As eRemoteDB.Execute
		
		On Error GoTo InsUpdDisc_pb_Err
		
		lrecInsUpdDisc_pb = New eRemoteDB.Execute
		
		'+Definición de parámetros para stored procedure 'insudb.InsUpdDisc_pb'
		'+Información leída el 22/01/2001 11:58:10 AM
		With lrecInsUpdDisc_pb
			.StoredProcedure = "InsUpdDisc_pb"
			.Parameters.Add("nAction", nAction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nIntertyp", nIntertyp, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nModulec", nModulec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAgreement", nAgreement, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nQPB", nQPB, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPercent", nPercent, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			InsUpdDisc_pb = .Run(False)
		End With
		
InsUpdDisc_pb_Err: 
		If Err.Number Then
			InsUpdDisc_pb = False
		End If
		'UPGRADE_NOTE: Object lrecInsUpdDisc_pb may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecInsUpdDisc_pb = Nothing
		On Error GoTo 0
	End Function
	
	'%Add: Crea un registro en la tabla
	Public Function Add() As Boolean
		Add = InsUpdDisc_pb(1)
	End Function
	
	'%Update: Actualiza un registro en la tabla
	Public Function Update() As Boolean
		Update = InsUpdDisc_pb(2)
	End Function
	
	'%Delete: Borra un registro en la tabla
	Public Function Delete() As Boolean
		Delete = InsUpdDisc_pb(3)
	End Function
	
	'%Find: Lee los datos de la tabla
	Public Function Find(ByVal nIntertyp As Integer, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nModulec As Integer, ByVal nAgreement As Integer, ByVal nQPB As Integer, ByVal dEffecdate As Date, Optional ByVal bFind As Boolean = False) As Boolean
		Dim lrecReaDisc_pb As eRemoteDB.Execute
		
		On Error GoTo Find_Err
		If Me.nIntertyp <> nIntertyp Or Me.nBranch <> nBranch Or Me.nProduct <> nProduct Or Me.nModulec <> nModulec Or Me.dEffecdate <> dEffecdate Or Me.nAgreement <> nAgreement Or Me.nQPB <> nQPB Or bFind Then
			
			lrecReaDisc_pb = New eRemoteDB.Execute
			'+Definición de parámetros para stored procedure 'ReaDisc_pb'
			With lrecReaDisc_pb
				.StoredProcedure = "ReaDisc_pb_by_agree"
				.Parameters.Add("nIntertyp", nIntertyp, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nModulec", nModulec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nAgreement", nAgreement, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nQPB", nQPB, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				If .Run Then
					Me.nIntertyp = nIntertyp
					Me.nBranch = nBranch
					Me.nProduct = nProduct
					Me.nModulec = nModulec
					Me.nAgreement = nAgreement
					Me.nQPB = nQPB
					Me.dEffecdate = .FieldToClass("dEffecdate")
					Me.dNulldate = .FieldToClass("dNulldate")
					Me.nPercent = .FieldToClass("nPercent")
					.RCloseRec()
					Find = True
				End If
			End With
		End If
		
Find_Err: 
		If Err.Number Then
			Find = False
		End If
		'UPGRADE_NOTE: Object lrecReaDisc_pb may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecReaDisc_pb = Nothing
		On Error GoTo 0
	End Function
	
	'% IsExist: Verifica la existencia de un registro en la tabla usando la clave
	Public Function IsExist(ByVal nIntertyp As Integer, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nModulec As Integer, ByVal nAgreement As Integer, ByVal nQPB As Integer, ByVal dEffecdate As Date) As Boolean
		Dim lrecReaDisc_pb_by_agree_v As eRemoteDB.Execute
		
		On Error GoTo IsExist_Err
		lrecReaDisc_pb_by_agree_v = New eRemoteDB.Execute
		'+ Definición de store procedure ReaDisc_pb_by_agree_v al 06-18-2002 11:58:22
		With lrecReaDisc_pb_by_agree_v
			.StoredProcedure = "ReaDisc_pb_by_agree_v"
			.Parameters.Add("nIntertyp", nIntertyp, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nModulec", nModulec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAgreement", nAgreement, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nQpb", nQPB, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nExist", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				IsExist = .Parameters("nExist").Value = "1"
			End If
		End With
		
IsExist_Err: 
		If Err.Number Then
			IsExist = False
		End If
		'UPGRADE_NOTE: Object lrecReaDisc_pb_by_agree_v may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecReaDisc_pb_by_agree_v = Nothing
		On Error GoTo 0
	End Function
	
	'%InsValEffecdate: Valida si existen registros con fecha de efecto posterior
	Public Function InsValEffecdate(ByVal nIntertyp As Integer, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nModulec As Integer, ByVal dEffecdate As Date) As Boolean
		Dim lrecReaDisc_pb As eRemoteDB.Execute
		
		On Error GoTo InsValEffecdate_Err
		lrecReaDisc_pb = New eRemoteDB.Execute
		
		InsValEffecdate = True
		'+Definición de parámetros para stored procedure 'ReaDisc_pb'
		With lrecReaDisc_pb
			.StoredProcedure = "InsValEffecdate_Disc_pb"
			.Parameters.Add("nIntertyp", nIntertyp, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nModulec", nModulec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nExist", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				InsValEffecdate = .Parameters("nExist").Value = "0"
			End If
		End With
		
InsValEffecdate_Err: 
		If Err.Number Then
			InsValEffecdate = False
		End If
		'UPGRADE_NOTE: Object lrecReaDisc_pb may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecReaDisc_pb = Nothing
		On Error GoTo 0
	End Function
	
	'%InsValMVA600_K: Validaciones de la transacción según especificaciones funcionales de la
	'%                MVA600-Descuentos por primas basica
	Public Function InsValMVA600_K(ByVal sCodispl As String, ByVal nAction As Integer, ByVal nIntertyp As Integer, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nModulec As Integer, ByVal dEffecdate As Date) As String
		Dim lclsErrors As eFunctions.Errors
		Dim lclsProduct As Object
		Dim lblnModulec As Boolean
		Dim lblnError As Boolean
		
		On Error GoTo InsValMVA600_K_Err
		lclsErrors = New eFunctions.Errors
		lclsProduct = eRemoteDB.NetHelper.CreateClassInstance("eProduct.Product")
		nModulec = IIf(nModulec = eRemoteDB.Constants.intNull, 0, nModulec)
		
		With lclsErrors
			'+ Se valida el Campo Tipo de intermediario
			If nIntertyp = eRemoteDB.Constants.intNull Then
				.ErrorMessage(sCodispl, 10095)
			End If
			
			'+ Se valida el Campo Ramo
			If nBranch = eRemoteDB.Constants.intNull Then
				.ErrorMessage(sCodispl, 9064)
				lblnError = True
			End If
			
			'+ Se valida el Campo Producto
			If nProduct = eRemoteDB.Constants.intNull Then
				.ErrorMessage(sCodispl, 11009)
				lblnError = True
			End If
			
			'+ Se valida el Campo Fecha
			If dEffecdate = dtmNull Then
				.ErrorMessage(sCodispl, 1103)
				lblnError = True
			Else
				If nAction = eFunctions.Menues.TypeActions.clngActionUpdate Or nAction = eFunctions.Menues.TypeActions.clngActionadd Then
					If Not InsValEffecdate(nIntertyp, nBranch, nProduct, nModulec, dEffecdate) Then
						.ErrorMessage(sCodispl, 55611)
						lblnError = True
					End If
				End If
			End If
			
			'+ Se valida el Campo Modulo
			If Not lblnError Then
				lblnModulec = lclsProduct.IsModule(nBranch, nProduct, dEffecdate)
				If lblnModulec Then
					If nModulec = 0 Then
						.ErrorMessage(sCodispl, 12112)
					End If
				End If
			End If
			
			InsValMVA600_K = .Confirm
		End With
		
InsValMVA600_K_Err: 
		If Err.Number Then
			InsValMVA600_K = "InsValMVA600_K: " & Err.Description
		End If
		'UPGRADE_NOTE: Object lclsProduct may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsProduct = Nothing
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
		On Error GoTo 0
	End Function
	
	'%InsValMVA600: Validaciones de la transacción según especificaciones funcionales
	'%              Descuentos por primas basica(MVA600)
	Public Function InsValMVA600(ByVal sCodispl As String, ByVal sAction As String, ByVal nIntertyp As Integer, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nModulec As Integer, ByVal dEffecdate As Date, ByVal nAgreement As Integer, ByVal nQPB As Integer, ByVal nPercent As Double) As String
		Dim lclsErrors As eFunctions.Errors
		Dim lclsValTable As eFunctions.Values
		
		On Error GoTo InsValMVA600_Err
		lclsErrors = New eFunctions.Errors
		
		nAgreement = IIf(nAgreement = eRemoteDB.Constants.intNull, 0, nAgreement)
		nModulec = IIf(nModulec = eRemoteDB.Constants.intNull, 0, nModulec)
		
		With lclsErrors
			'+Se valida el campo Cantidad de prima básica
			If nQPB = eRemoteDB.Constants.intNull Then
				.ErrorMessage(sCodispl, 55568)
			Else
				'+Validar que no se dupliquen registros
				If sAction = "Add" Then
					If IsExist(nIntertyp, nBranch, nProduct, nModulec, nAgreement, nQPB, dEffecdate) Then
						.ErrorMessage(sCodispl, 55570)
					End If
				End If
			End If
			
			'+Se valida el campo % Descuento
			If nPercent = eRemoteDB.Constants.intNull Then
				.ErrorMessage(sCodispl, 55569)
			End If
			
			InsValMVA600 = .Confirm
		End With
		
InsValMVA600_Err: 
		If Err.Number Then
			InsValMVA600 = "InsValMVA600: " & Err.Description
		End If
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
		'UPGRADE_NOTE: Object lclsValTable may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsValTable = Nothing
		On Error GoTo 0
	End Function
	
	'%InsPostMVA600: Actualizaciones según especificaciones funcionales
	'%               Descuentos por primas basica(MVA600)
	Public Function InsPostMVA600(ByVal sAction As String, ByVal nIntertyp As Integer, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nModulec As Integer, ByVal nAgreement As Integer, ByVal dEffecdate As Date, ByVal nQPB As Integer, ByVal nPercent As Double, ByVal nUsercode As Integer) As Boolean
		On Error GoTo InsPostMVA600_Err
		With Me
			.nIntertyp = nIntertyp
			.nBranch = nBranch
			.nProduct = nProduct
			.nModulec = IIf(nModulec = eRemoteDB.Constants.intNull, 0, nModulec)
			.nAgreement = IIf(nAgreement = eRemoteDB.Constants.intNull, 0, nAgreement)
			.dEffecdate = dEffecdate
			.nQPB = nQPB
			.nPercent = nPercent
			.nUsercode = nUsercode
		End With
		
		Select Case sAction
			Case "Add"
				InsPostMVA600 = Add
			Case "Update"
				InsPostMVA600 = Update
			Case "Del"
				InsPostMVA600 = Delete
		End Select
		
InsPostMVA600_Err: 
		If Err.Number Then
			InsPostMVA600 = False
		End If
		On Error GoTo 0
	End Function
	
	'%Class_Initialize: Inicializa las propiedades cuando se instancia la clase
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		nIntertyp = eRemoteDB.Constants.intNull
		nBranch = eRemoteDB.Constants.intNull
		nProduct = eRemoteDB.Constants.intNull
		nModulec = eRemoteDB.Constants.intNull
		nAgreement = eRemoteDB.Constants.intNull
		dEffecdate = dtmNull
		dNulldate = dtmNull
		nQPB = eRemoteDB.Constants.intNull
		nPercent = eRemoteDB.Constants.intNull
		nUsercode = eRemoteDB.Constants.intNull
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
End Class






