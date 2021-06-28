Option Strict Off
Option Explicit On
Public Class Tar_Health
	'%-------------------------------------------------------%'
	'% $Workfile:: Tar_Health.cls                          $%'
	'% $Author:: Nvaplat18                                  $%'
	'% $Date:: 20/10/03 13.35                               $%'
	'% $Revision:: 25                                       $%'
	'%-------------------------------------------------------%'
	
	'**-Properties according to the table in the system on October 16,2001.
	'*-Propiedades según la tabla en el sistema el 16/10/2001
	
	'Column_name               Type                        Nulleable
	'-----------------------   ------------------------    ---------------
	Public nBranch As Integer 'Number(5)       No
	Public nProduct As Integer 'Number(5)       No
	Public nCover As Integer 'Number(5)       No
	Public nAgreement As Integer
	Public nAge As Integer 'Number(5)       No
	Public nSex As Integer
	Public nInsu_Count_Ini As Integer 'Number(5)       No
	Public nInsu_Count_End As Integer 'Number(5)       Yes
	Public nRate As Double 'Number(9, 6)    Yes
	Public dEffecdate As Date 'Date            No
	Public dNulldate As Date 'Date            Yes
	Public nUsercode As Integer 'Number(5)       Yes
	Public dCompdate As Date 'Date            No
	
	Private mvarTar_Health As Tar_Health
	
	'% Get Tar_Health: toma el objeto de la clase
	
	'% Set Tar_Health: setea el objeto de la clase
	Public Property Tar_Health() As Tar_Health
		Get
			If mvarTar_Health Is Nothing Then
				mvarTar_Health = New Tar_Health
			End If
			
			Tar_Health = mvarTar_Health
		End Get
		Set(ByVal Value As Tar_Health)
			mvarTar_Health = Value
		End Set
	End Property
	
	'% Class_Terminate: se controla el cierre de la instancia del objeto de la clase
	'UPGRADE_NOTE: Class_Terminate was upgraded to Class_Terminate_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Terminate_Renamed()
		'UPGRADE_NOTE: Object mvarTar_Health may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		mvarTar_Health = Nothing
	End Sub
	Protected Overrides Sub Finalize()
		Class_Terminate_Renamed()
		MyBase.Finalize()
	End Sub
	
	'% insUpdTar_Health: Se crean/actualizan/eliminan los datos de la tabla
	Private Function InsUpdTar_Health(ByVal nAction As Integer) As Boolean
		Dim lrecInsUpdTar_Health As eRemoteDB.Execute
		
		On Error GoTo InsUpdTar_Health_Err
		
		lrecInsUpdTar_Health = New eRemoteDB.Execute
		
		'**+Parameter definition for stored procedure 'InsUpdTar_Health'
		'**+Information read on October 16,2001 11:58:10 a.m.
		'+Definición de parámetros para stored procedure 'InsUpdTar_Health'
		'+Información leída el 16/10/2001 11:58:10 AM
		
		With lrecInsUpdTar_Health
			.StoredProcedure = "InsUpdTar_Health"
			
			.Parameters.Add("nAction", nAction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 38, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCover", nCover, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("NCOD_AGREE", nAgreement, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAge", nAge, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nSex", nSex, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("NCOUNT_INSU_INI", nInsu_Count_Ini, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("NCOUNT_INSU_end", nInsu_Count_End, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nRate", nRate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 9, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			InsUpdTar_Health = .Run(False)
		End With
		
InsUpdTar_Health_Err: 
		If Err.Number Then
			InsUpdTar_Health = False
		End If
		'UPGRADE_NOTE: Object lrecInsUpdTar_Health may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecInsUpdTar_Health = Nothing
		On Error GoTo 0
	End Function
	
	'%Add: Crea un registro en la tabla
	Public Function Add() As Boolean
		Add = InsUpdTar_Health(1)
	End Function
	
	'%Update: Actualiza un registro en la tabla
	Public Function Update() As Boolean
		Update = InsUpdTar_Health(2)
	End Function
	
	'%Delete: Borra un registro en la tabla
	Public Function Delete() As Boolean
		Delete = InsUpdTar_Health(3)
	End Function
	
	'%Find: Lee los datos de la tabla
	Public Function Find(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal dEffecdate As Date, ByVal nCover As Integer, ByVal nAgreement As Integer, Optional ByVal lblnFind As Boolean = False) As Boolean
		Dim lrecReaTar_Health As eRemoteDB.Execute
		
		On Error GoTo Find_Err
		
		lrecReaTar_Health = New eRemoteDB.Execute
		
		Me.nBranch = nBranch
		Me.nProduct = nProduct
		Me.nCover = nCover
		Me.nAge = nAge
		Me.dEffecdate = dEffecdate
		
		'+Definición de parámetros para stored procedure 'ReaTar_Health_by_age'
		
		With lrecReaTar_Health
			.StoredProcedure = "ReaTar_Health_by_age"
			
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCover", nCover, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAgreement", nAgreement, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run Then
				Me.nBranch = nBranch
				Me.nProduct = nProduct
				Me.dEffecdate = .FieldToClass("dEffecdate")
				Me.nCover = nCover
				Me.nAgreement = nAgreement
				Me.nAge = .FieldToClass("nAge")
				Me.nSex = .FieldToClass("nSex")
				Me.nInsu_Count_Ini = .FieldToClass("nInsu_Count_Ini")
				Me.nInsu_Count_End = .FieldToClass("nInsu_Count_End")
				Me.nRate = .FieldToClass("nRate")
				Me.dNulldate = .FieldToClass("dNulldate")
				
				Find = True
				.RCloseRec()
			End If
		End With
		
Find_Err: 
		If Err.Number Then
			Find = False
		End If
		'UPGRADE_NOTE: Object lrecReaTar_Health may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecReaTar_Health = Nothing
		On Error GoTo 0
	End Function
	
	'% insValEffecdate: verifica que la fecha sea posterior a la última actualización a la tabla
	Public Function InsValEffecdate(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal dEffecdate As Date, ByVal nCover As Integer, ByVal nAgreement As Integer) As Boolean
		Dim lrecReaTar_Health As eRemoteDB.Execute
		
		On Error GoTo InsValEffecdate_Err
		
		lrecReaTar_Health = New eRemoteDB.Execute
		
		InsValEffecdate = True
		
		'+Definición de parámetros para stored procedure 'InsValEffecdate_Tar_Health'
		
		With lrecReaTar_Health
			.StoredProcedure = "InsValEffecdate_Tar_Health"
			
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCover", nCover, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAgreement", nAgreement, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			InsValEffecdate = Not .Run
		End With
		
InsValEffecdate_Err: 
		If Err.Number Then
			InsValEffecdate = False
		End If
		'UPGRADE_NOTE: Object lrecReaTar_Health may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecReaTar_Health = Nothing
		On Error GoTo 0
	End Function
	
	'% insvalMAM8000_K: se realizan las validaciones de la ventana
	Public Function insvalMAM8000_K(ByVal sCodispl As String, ByVal nAction As Integer, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal dEffecdate As Date, ByVal nCover As Integer, ByVal nAgreement As Integer) As String
		Dim lclsErrors As eFunctions.Errors
		Dim lclsProduct As eProduct.Product
		
		On Error GoTo InsValMAM8000_K_Err
		
		lclsErrors = New eFunctions.Errors
		lclsProduct = New eProduct.Product
		
		With lclsErrors
			
			'+ Se valida el Campo Ramo
			
			If nBranch = eRemoteDB.Constants.intNull Then
				.ErrorMessage(sCodispl, 9064)
			End If
			
			'+ Se valida el Campo Producto
			
			If nProduct = eRemoteDB.Constants.intNull Then
				.ErrorMessage(sCodispl, 11009)
			End If
			
			'+ Se valida el Campo Cobertura
			
			If nCover = eRemoteDB.Constants.intNull Then
				.ErrorMessage(sCodispl, 11163)
			End If
			
			'+ Se valida el Campo Cobertura
			
			If nAgreement = eRemoteDB.Constants.intNull Then
				.ErrorMessage(sCodispl, 60117)
			End If
			
			'+ Se valida el Campo Fecha
			
			If dEffecdate = dtmNull Then
				.ErrorMessage(sCodispl, 1103)
			Else
				
				'+ Debe ser posterior a la última modificación
				
				If nAction = eFunctions.Menues.TypeActions.clngActionUpdate Or nAction = eFunctions.Menues.TypeActions.clngActionadd Then
					'If Not InsValEffecdate(nBranch, nProduct, dEffecdate, nCover, nAgreement) Then
					'    .ErrorMessage sCodispl, 55611
					'End If
				End If
			End If
			
			insvalMAM8000_K = .Confirm
		End With
		
InsValMAM8000_K_Err: 
		If Err.Number Then
			insvalMAM8000_K = "InsValMAM8000_K: " & Err.Description
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lclsProduct may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsProduct = Nothing
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
	End Function
	
	'% insvalMAM8000: se realizan las validaciones de la ventana
	Public Function insvalMAM8000(ByVal sCodispl As String, ByVal sAction As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal dEffecdate As Date, ByVal nCover As Integer, ByVal nAgreement As Integer, ByVal nAge As Integer, ByVal nSex As Integer, ByVal nInsu_Count_Ini As Integer, ByVal nInsu_Count_End As Integer, ByVal nRate As Double) As String
		Dim lclsErrors As eFunctions.Errors
		
		On Error GoTo InsValMAM8000_Err
		
		lclsErrors = New eFunctions.Errors
		
		With lclsErrors
			
			'+Se valida el campo Edad
			
			If nAge = eRemoteDB.Constants.intNull Then
				.ErrorMessage(sCodispl, 1012,  , eFunctions.Errors.TextAlign.LeftAling, "Edad:")
			Else
				If sAction = "Add" Then
					If Find(nBranch, nProduct, dEffecdate, nCover, nAgreement, True) Then
						.ErrorMessage(sCodispl, 55610)
					End If
				End If
			End If
			
			If nSex = eRemoteDB.Constants.intNull Then
				
				'+ Debe indicar información en Sexo
				
				.ErrorMessage(sCodispl, 2015)
			End If
			
			If nInsu_Count_Ini = eRemoteDB.Constants.intNull Then
				
				'+ Debe indicar información Cantidad Inicial de Asegurados
				
				.ErrorMessage(sCodispl, 10160)
			End If
			
			If nInsu_Count_End = eRemoteDB.Constants.intNull Then
				
				'+ Debe indicar información en Cantidad Final de Asegurados
				
				.ErrorMessage(sCodispl, 10161)
			End If
			
			If nRate = eRemoteDB.Constants.intNull Then
				
				'+ Debe indicar información en tasa (Hombres/Mujeres) o monto fijo (Hombres/Mujeres)
				
				.ErrorMessage(sCodispl, 60140)
			End If
			
			insvalMAM8000 = .Confirm
		End With
		
InsValMAM8000_Err: 
		If Err.Number Then
			insvalMAM8000 = "InsValMAM8000: " & Err.Description
		End If
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
		On Error GoTo 0
	End Function
	
	'% insPostMAM8000: se actualizan los campos de la página
	Public Function InsPostMAM8000(ByVal sAction As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal dEffecdate As Date, ByVal nCover As Integer, ByVal nAgreement As Integer, ByVal nAge As Integer, ByVal nSex As Integer, ByVal nInsu_Count_Ini As Integer, ByVal nInsu_Count_End As Integer, ByVal nRate As Double, ByVal dNulldate As Date, ByVal nUsercode As Integer) As Boolean
		On Error GoTo InsPostMAM8000_Err
		
		With Me
			.nBranch = nBranch
			.nProduct = nProduct
			.nCover = nCover
			.nAgreement = nAgreement
			.nAge = nAge
			.nSex = nSex
			.nInsu_Count_Ini = nInsu_Count_Ini
			.nInsu_Count_End = nInsu_Count_End
			.nRate = nRate
			.dEffecdate = dEffecdate
			.nUsercode = nUsercode
		End With
		
		Select Case sAction
			Case "Add"
				InsPostMAM8000 = Add
			Case "Update"
				InsPostMAM8000 = Update
			Case "Del"
				InsPostMAM8000 = Delete
		End Select
		
InsPostMAM8000_Err: 
		If Err.Number Then
			InsPostMAM8000 = False
		End If
		On Error GoTo 0
	End Function
	
	'%Class_Initialize: Inicializa las propiedades cuando se instancia la clase
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		nBranch = eRemoteDB.Constants.intNull
		nProduct = eRemoteDB.Constants.intNull
		nCover = eRemoteDB.Constants.intNull
		nAge = eRemoteDB.Constants.intNull
		nSex = eRemoteDB.Constants.intNull
		nAgreement = eRemoteDB.Constants.intNull
		nInsu_Count_Ini = eRemoteDB.Constants.intNull
		nInsu_Count_End = eRemoteDB.Constants.intNull
		dEffecdate = dtmNull
		nRate = eRemoteDB.Constants.intNull
		dNulldate = dtmNull
		nUsercode = eRemoteDB.Constants.intNull
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
End Class






