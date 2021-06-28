Option Strict Off
Option Explicit On
Public Class PolReport
	'%-------------------------------------------------------%'
	'% $Workfile:: PolReport.cls                            $%'
	'% $Author:: Nvaplat41                                  $%'
	'% $Date:: 9/10/03 19.01                                $%'
	'% $Revision:: 20                                       $%'
	'%-------------------------------------------------------%'
	
	'+ Column_name                 Type           Computed   Length      Prec  Scale Nullable    TrimTrailingBlanks   FixedLenNullInSource    Collation
	'+ -------------------         -------------- ---------- ----------- ----- ----- ----------- -------------------- ----------------------- -----------
	'lmoreno
	
	Public sCertype As String 'char          no         1                       yes          no                  yes                     SQL_Latin1_General_CP1_CI_AS
	Public nBranch As Integer 'int           no         2           5     0     no          (n/a)                (n/a)                   NULL
	Public nProduct As Integer 'int           no         2           5     0     no          (n/a)                (n/a)                   NULL
	Public nPolicy As Double 'Long          no         4      10    0          no          (n/a)                (n/a)
	Public nCertif As Double 'int           no         4      10    0          no          (n/a)                (n/a)
	Public dEffecdate As Date 'datetime      no         8                       no          (n/a)                (n/a)                   NULL
	Public sCodispl As String 'char          no         8                       yes          no                  yes
	Public dNulldate As Date 'datetime      no         8                       yes         (n/a)                (n/a)                   NULL
	Public nTransactype As Integer 'long          no         4           10    0     no          (n/a)                (n/a)                   NULL
	Public nUsercode As Integer 'int           no         2           5     0     yes         (n/a)                (n/a)                   NULL
	
	'- Se define las constantes que contienen los máximos y minimos valores para las
	'- edades y capitales.
	
	Const MaxE As Integer = 130
	Const MinE As Integer = 0
	Const MaxCap As Double = 99999999#
	Const MinCap As Double = 1
	
	'- Se declara variable que indica si la ventana tiene contenido o no
	Public sContent As String
	
	'%Add: Permite registrar la información de los criterios de selección de riesgos.
	Public Function Add() As Boolean
		Dim lrecCrePolReport As eRemoteDB.Execute
		
		On Error GoTo Add_err
		
		lrecCrePolReport = New eRemoteDB.Execute
		
		'+ Definición de parámetros para stored procedure 'insudb.crePolReport'
		
		With lrecCrePolReport
			.StoredProcedure = "crePolReport"
			
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sCodispl", sCodispl, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 8, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTransactype", nTransactype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			Add = .Run(False)
		End With
		
		'UPGRADE_NOTE: Object lrecCrePolReport may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecCrePolReport = Nothing
		
Add_err: 
		If Err.Number Then
			Add = False
		End If
		
		On Error GoTo 0
	End Function
	
	'%Update: Permite actualizar la información de los criterios de selección de riesgos.
	Public Function Update() As Boolean
		Dim lrecUpdPolReport As eRemoteDB.Execute
		
		lrecUpdPolReport = New eRemoteDB.Execute
		
		On Error GoTo Update_Err
		
		'+ Definición de parámetros para stored procedure 'insudb.insMortalityCre'
		
		With lrecUpdPolReport
			.StoredProcedure = "updPolReport"
			
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sCodispl", sCodispl, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 8, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTransactype", nTransactype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			Update = .Run(False)
		End With
		
		'UPGRADE_NOTE: Object lrecUpdPolReport may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecUpdPolReport = Nothing
		
Update_Err: 
		If Err.Number Then
			Update = False
		End If
		
		On Error GoTo 0
	End Function
	
	'%Delete: Permite borrar la información de criterios de selección de riesgos.
	Public Function Delete() As Boolean
		Dim lrecDePolReport As eRemoteDB.Execute
		
		On Error GoTo Delete_err
		
		lrecDePolReport = New eRemoteDB.Execute
		
		'+ Definición de parámetros para stored procedure
		
		With lrecDePolReport
			.StoredProcedure = "delPolReport"
			
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sCodispl", sCodispl, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 8, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTransactype", nTransactype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			
			Delete = .Run(False)
		End With
		
		'UPGRADE_NOTE: Object lrecDePolReport may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecDePolReport = Nothing
		
Delete_err: 
		If Err.Number Then
			Delete = False
		End If
		
		On Error GoTo 0
	End Function
	
	'% insValCA727: Realiza la validación de los campos puntuales de la página
	Public Function insValCA727(ByVal sCodispl1 As String, ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, ByVal sCodispl As String, ByVal nTransactype As Integer) As String
		Dim lobjErrors As eFunctions.Errors
		Dim lrecPolReport As eRemoteDB.Execute
		Dim lObjValField As eFunctions.valField
		Dim llngCount As Integer
		
		On Error GoTo insValCA727_Err
		
		lobjErrors = New eFunctions.Errors
		lObjValField = New eFunctions.valField
		
		insValCA727 = String.Empty
		'+ validación del Codigo
		If sCodispl = "" Then
			Call lobjErrors.ErrorMessage(sCodispl1, 55689)
		End If
		
		insValCA727 = lobjErrors.Confirm
		
		'UPGRADE_NOTE: Object lobjErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lobjErrors = Nothing
		'UPGRADE_NOTE: Object lrecPolReport may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecPolReport = Nothing
		
insValCA727_Err: 
		If Err.Number Then
			insValCA727 = insValCA727 & Err.Description
		End If
		
		On Error GoTo 0
	End Function
	'% insPostCA727: Esta función se encarga de almacenar los datos en las tablas
	Public Function insPostCA727(ByVal sAction As String, ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, ByVal sCodispl As String, ByVal nTransactype As Integer, Optional ByVal nUsercode As Integer = 0) As Boolean
		Dim lclsPolReports As ePolicy.PolReports
		
		insPostCA727 = True
		
		Me.sCertype = sCertype
		Me.nBranch = nBranch
		Me.nProduct = nProduct
		Me.nPolicy = nPolicy
		Me.nCertif = nCertif
		Me.dEffecdate = dEffecdate
		Me.sCodispl = sCodispl
		Me.nTransactype = nTransactype
		Me.nUsercode = nUsercode
		
		Select Case sAction
			
			'+ Si la opción seleccionada es Registrar.
			Case "Add"
				insPostCA727 = Add()
				
				'+ Si la opción seleccionada es Modificar.
			Case "Update"
				insPostCA727 = Update()
				
			Case "Delete"
				insPostCA727 = Delete()
				lclsPolReports = New PolReports
				If lclsPolReports.FindPolReport(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate) Then
					Me.sContent = "2"
				Else
					Me.sContent = "1"
				End If
				'UPGRADE_NOTE: Object lclsPolReports may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
				lclsPolReports = Nothing
		End Select
	End Function
End Class






