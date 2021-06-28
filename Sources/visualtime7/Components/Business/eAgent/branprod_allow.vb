Option Strict Off
Option Explicit On
Public Class branprod_allow
	'%-------------------------------------------------------%'
	'% $Workfile:: branprod_allow.cls                       $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 1:41p                                $%'
	'% $Revision:: 14                                       $%'
	'%-------------------------------------------------------%'
	
	'+Propiedades según la tabla 'branprod_allow' en el sistema 06/12/2001 04:06:56 p.m.
	
	Public nIntermed As Integer
	Public nBranch As Integer
	Public nProduct As Integer
	Public nModulec As Integer
	Public nInstallments As Integer
	Public nStartMonth As Integer
	Public nEndMonth As Integer
	Public nUsercode As Integer
	Public nCommQuot As Integer
	
	
	
	'Find: Función que realiza la busqueda en la tabla 'branprod_allow'
	Public Function Find(ByVal nIntermed As Integer, Optional ByVal nBranch As Integer = 0, Optional ByVal nProduct As Integer = 0, Optional ByVal nDuration As Integer = 0) As Boolean
		Dim lrecbranprod_allow As eRemoteDB.Execute
		
		lrecbranprod_allow = New eRemoteDB.Execute
		
		On Error GoTo Find_Err
		
		'+ Define all parameters for the stored procedures 'insudb.reabranprod_allow'. Generated on 06/12/2001 04:06:56 p.m.
		With lrecbranprod_allow
			.StoredProcedure = "reabranprod_allow"
			.Parameters.Add("PnIntermed", nIntermed, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", IIf(nBranch = 0, eRemoteDB.Constants.intNull, nBranch), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", IIf(nProduct = 0, eRemoteDB.Constants.intNull, nProduct), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDuration", nDuration, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run Then
				nBranch = .FieldToClass("nBranch")
				nProduct = .FieldToClass("nProduct")
				nModulec = .FieldToClass("nModulec")
				nInstallments = .FieldToClass("nInstallments")
				Find = True
				.RCloseRec()
			Else
				Find = False
			End If
		End With
		
Find_Err: 
		If Err.Number Then
			Find = False
		End If
		
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecbranprod_allow may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecbranprod_allow = Nothing
		
	End Function
	
	'% insUpdBranprod_allow: Método que realiza las actualizaciones pertinentes sobre la tabla "branprod_allow"
	Public Function insUpdBranprod_allow(ByVal nAction As Integer) As Boolean
		Dim lrecbranprod_allow As eRemoteDB.Execute
		
		lrecbranprod_allow = New eRemoteDB.Execute
		
		On Error GoTo insUpdBranprod_allow_Err
		
		'+ Define all parameters for the stored procedures 'insudb.insUpdBranprod_allow'. Generated on 06/12/2001 04:06:56 p.m.
		
		With lrecbranprod_allow
			.StoredProcedure = "insUpdBranprod_allow"
			.Parameters.Add("nAction", nAction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nIntermed", nIntermed, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", IIf(nProduct = eRemoteDB.Constants.intNull, 0, nProduct), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nModulec", IIf(nModulec = eRemoteDB.Constants.intNull, 0, nModulec), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nInstallments", IIf(nInstallments = eRemoteDB.Constants.intNull, 0, nInstallments), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nStartMonth", IIf(nStartMonth = eRemoteDB.Constants.intNull, 0, nStartMonth), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nEndMonth", IIf(nEndMonth = eRemoteDB.Constants.intNull, 0, nEndMonth), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			insUpdBranprod_allow = .Run(False)
		End With
		
insUpdBranprod_allow_Err: 
		If Err.Number Then
			insUpdBranprod_allow = False
		End If
		'UPGRADE_NOTE: Object lrecbranprod_allow may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecbranprod_allow = Nothing
		On Error GoTo 0
	End Function
	
	'%insValAG553: Función que realiza la validación de los datos introducidos por la ventana para
	'              ramos y productos permitidos para intermediarios
	Public Function insValAG553(ByVal sCodispl As String, ByVal sAction As String, ByVal nIntermed As Integer, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nModulec As Integer, ByVal nInstallments As Integer, ByVal nStartMonth As Integer, ByVal nEndMonth As Integer) As String
		Dim lclsErrors As eFunctions.Errors
		Dim lclsProduct As eProduct.Product
		Dim lclsTab_modul As eProduct.Tab_modul
		
		On Error GoTo insValAG553_Err
		
		lclsErrors = New eFunctions.Errors
		lclsTab_modul = New eProduct.Tab_modul
		lclsProduct = New eProduct.Product
		
		nProduct = IIf(nProduct < 0, 0, nProduct)
		nModulec = IIf(nModulec < 0, 0, nModulec)
		nInstallments = IIf(nInstallments < 0, 0, nInstallments)
		nStartMonth = IIf(nStartMonth < 0, 0, nStartMonth)
		nEndMonth = IIf(nEndMonth < 0, 0, nEndMonth)
		
		'+ Se valida que el campo "Ramo" no se encuentre vacio
		With lclsErrors
			If nBranch = 0 Or nBranch = eRemoteDB.Constants.intNull Then
				Call .ErrorMessage(sCodispl, 1022)
			Else
				
				'+ Se valida el campo "Producto"
				If nProduct > 0 Then
					If Not lclsProduct.Find(nBranch, nProduct, Today) Then
						Call .ErrorMessage(sCodispl, 1011)
					Else
						
						'+ Se valida el campo "Módulo"
						If nModulec > 0 Then
							If Not lclsTab_modul.Find(nBranch, nProduct, nModulec, Today) Then
								Call .ErrorMessage(sCodispl, 11011)
							End If
						End If
					End If
				End If
			End If
			
			'+ Se valida que no se inserte un registro ya existente
			If sAction = "Add" Then
				If IsExist(nIntermed, nBranch, nProduct, nModulec, nInstallments, nStartMonth, nEndMonth) Then
					Call .ErrorMessage(sCodispl, 55110)
				End If
			End If
			
			If nStartMonth <> 0 And nStartMonth <> eRemoteDB.Constants.intNull Then
				If nEndMonth = 0 Or nEndMonth = eRemoteDB.Constants.intNull Then
					Call .ErrorMessage(sCodispl, 55108)
				Else
					If nEndMonth <= nStartMonth Then
						Call .ErrorMessage(sCodispl, 55109)
					End If
				End If
			End If
			
			insValAG553 = .Confirm
		End With
		
		'UPGRADE_NOTE: Object lclsTab_modul may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsTab_modul = Nothing
		'UPGRADE_NOTE: Object lclsProduct may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsProduct = Nothing
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
		
insValAG553_Err: 
		If Err.Number Then
			insValAG553 = insValAG553 & Err.Description
		End If
		On Error GoTo 0
	End Function
	
	'%insPostAG553: Función que realiza el llamado a los métodos de actualización, borrado e inserción de registros
	Public Function insPostAG553(ByVal sCodispl As String, ByVal sAction As String, ByVal nIntermed As Integer, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nModulec As Integer, ByVal nInstallments As Integer, ByVal nStartMonth As Integer, ByVal nEndMonth As Integer, ByVal nUsercode As Integer) As Boolean
		
		On Error GoTo insPostAG553_Err
		
		With Me
			.nIntermed = nIntermed
			.nBranch = nBranch
			.nProduct = nProduct
			.nModulec = nModulec
			.nInstallments = nInstallments
			.nStartMonth = nStartMonth
			.nEndMonth = nEndMonth
			.nUsercode = nUsercode
			
			Select Case sAction
				
				'+ Acción: Agregar
				Case "Add"
					insPostAG553 = .insUpdBranprod_allow(1)
					
					'+ Acción: Actualizar
				Case "Update"
					insPostAG553 = .insUpdBranprod_allow(2)
					
					'+ Acción: Borrar
				Case "Del"
					insPostAG553 = .insUpdBranprod_allow(3)
			End Select
			
		End With
		
insPostAG553_Err: 
		If Err.Number Then
			insPostAG553 = False
		End If
		On Error GoTo 0
	End Function
	
	'IsExist: Función que realiza la busqueda en la tabla 'Branprod_Allow'
	Public Function IsExist(ByVal nIntermed As Integer, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nModulec As Integer, ByVal nInstallments As Integer, ByVal nStartMonth As Integer, ByVal nEndMonth As Integer) As Boolean
		Dim lclsBranprod_Allow As eRemoteDB.Execute
		
		lclsBranprod_Allow = New eRemoteDB.Execute
		
		On Error GoTo IsExist_Err
		
		'+ Define all parameters for the stored procedures 'insudb.valBranprod_AllowExist'. Generated on 17/01/2002 09:51:46 a.m.
		With lclsBranprod_Allow
			.StoredProcedure = "valBranprod_AllowExist"
			.Parameters.Add("nIntermed", nIntermed, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nModulec", nModulec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nInstalllments", nInstallments, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nStartMonth", nStartMonth, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nEndMonth", nEndMonth, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run() Then
				IsExist = .FieldToClass("nExist") = 1
			End If
		End With
		
IsExist_Err: 
		If Err.Number Then
			IsExist = False
		End If
		'UPGRADE_NOTE: Object lclsBranprod_Allow may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsBranprod_Allow = Nothing
		On Error GoTo 0
	End Function
	
	'FindAllow: Valida que un intermediario este permitido para un ramo-product
	'           verificando que puede estar permitido para todos los productos
	'           de un ramo en la tabla branprod_allow.
	Public Function FindAllow(ByVal nIntermed As Integer, Optional ByVal nBranch As Integer = 0, Optional ByVal nProduct As Integer = 0) As Boolean
		
		Dim lrecbranprod_allow As eRemoteDB.Execute
		Dim llngExists As Integer
		
		lrecbranprod_allow = New eRemoteDB.Execute
		
		On Error GoTo FindAllow_Err
		
		'+ Define all parameters for the stored procedures 'valBranprod_AllowPermitt'. Generated on 24/05/2002 20:00:00 p.m.
		With lrecbranprod_allow
			.StoredProcedure = "valBranprod_AllowPermitt"
			.Parameters.Add("nIntermed", nIntermed, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nExists", llngExists, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				If .Parameters.Item("nExists").Value = 1 Then
					FindAllow = True
				Else
					FindAllow = False
				End If
			End If
		End With
		
FindAllow_Err: 
		If Err.Number Then
			FindAllow = False
		End If
		
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecbranprod_allow may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecbranprod_allow = Nothing
		
	End Function
	'insCalComQuot: Función que retorna el número de cuotas a cancelar la comisión del intermediario
	Public Function insCalComQuot(ByVal nPayfreq As Integer, ByVal dStartdate As Date, ByVal dExpirdat As Date) As Boolean
		Dim nQuot_aux As Integer
		Dim nDif_year As Integer
		
		'+ Diferencia en años enteros entre fecha de venc del bono y la fecha de emisión
		nDif_year = System.Math.Abs(DateDiff(Microsoft.VisualBasic.DateInterval.Year, dStartdate, dExpirdat))
		
		Select Case nPayfreq
			Case 1
				nQuot_aux = 1
			Case 2
				nQuot_aux = 2
			Case 3
				nQuot_aux = 4
			Case 4
				nQuot_aux = 6
			Case 5
				nQuot_aux = 12
			Case Else
				nQuot_aux = 1
		End Select
		
		Me.nCommQuot = nQuot_aux * nDif_year
		insCalComQuot = True
		
insCalComQuot_Err: 
		If Err.Number Then
			Me.nCommQuot = 1
			insCalComQuot = False
		End If
		On Error GoTo 0
	End Function
	
	
	'%Class_Initialize: Controla la creación de una instancia de la clase
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		nIntermed = eRemoteDB.Constants.intNull
		nBranch = eRemoteDB.Constants.intNull
		nProduct = eRemoteDB.Constants.intNull
		nModulec = eRemoteDB.Constants.intNull
		nInstallments = eRemoteDB.Constants.intNull
		nUsercode = eRemoteDB.Constants.intNull
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
End Class






