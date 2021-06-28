Option Strict Off
Option Explicit On
<System.Runtime.InteropServices.ProgId("Lend_Agree_Pres_NET.Lend_Agree_Pres")> Public Class Lend_Agree_Pres
	'%-------------------------------------------------------%'
	'% $Workfile:: Lend_Agree_Pres.cls                           $%'
	'% $Author:: lsereno                                   $%'
	'% $Date:: 03/05/07 12:35p                               $%'
	'% $Revision:: 01                                       $%'
	'%-------------------------------------------------------%'
	
	'-
	'- Estructura de tabla Prod_Am_Bil al 06-27-2002 10:44:47
	'-        Property                Type
	'----------------------------------------
	Public nBranch As Integer
	Public nProduct As Integer
	Public nModulec As Integer
	Public nCover As Integer
	Public dEffecdate As Date
	Public dNulldate As Date
	Public nusercode As Integer
	Public dCompdate As Date
	Public nCod_agree As Integer
	Public nPrestac As Integer
	Public dinit_date As Date
	Public sCertype As String
	Public nPolicy As Integer
	Public nCertif As Integer
	Public sAction As String
	Public nGroup As Integer
	
	'%Add: Permite registrar la información los prestadores por producto.
	Public Function Add() As Boolean
		Add = InsUpdLend_Agree_Pres
	End Function
	
	'%Update: Permite actualizar la información de los criterios de selección de riesgos.
	Public Function Update() As Boolean
		Update = InsUpdLend_Agree_Pres
	End Function
	
	'%Delete: Permite borrar la información de criterios de selección de riesgos.
	Public Function Delete() As Boolean
		Delete = InsUpdLend_Agree_Pres
	End Function
	
	''% insValDP080: Realiza la validación de los campos a actualizar en el frame (ventana) DP042
	''  (clientes permitidos para todos los tipos de póliza)
	''--------------------------------------------------------------------------------------------
	Public Function insValCA100(ByVal sAction As String, ByVal sCodispl As String, ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nModulec As Integer, ByVal nCover As Integer, ByVal nPrestac As Integer, ByVal nCod_agree As Integer, ByVal dEffecdate As Date, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal nGroup As Integer) As String
		Dim lclsErrors As eFunctions.Errors
		Dim lblnError As Boolean
		
		On Error GoTo insValCA100_Err
		
		lclsErrors = New eFunctions.Errors
		
		With lclsErrors
			'+ Valida el Tipo de Producto.
			If nBranch = eRemoteDB.Constants.intNull Then
				Call .ErrorMessage(sCodispl, 11289)
			End If
			
			'+ valida cobertura
			If nCover > 0 And nPrestac = eRemoteDB.Constants.intNull Then
				Call .ErrorMessage(sCodispl, 767094)
			End If
			
			'+ valida prestador
			If nCod_agree <= 0 Then
				Call .ErrorMessage(sCodispl, 767093)
			End If
			
			If Find(sCertype, nBranch, nProduct, nModulec, nCover, nPolicy, nCertif, nPrestac, nCod_agree, dEffecdate, nGroup) Then
				Call .ErrorMessage(sCodispl, 10004)
			End If
			
			insValCA100 = .Confirm
			
		End With
		
		insValCA100 = lclsErrors.Confirm
		
insValCA100_Err: 
		If Err.Number Then
			insValCA100 = "insValCA100: " & Err.Description
		End If
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
		On Error GoTo 0
	End Function

    '% insPostDP080: Esta función se encarga de almacenar los datos en las tablas, en este caso lend_agree_prod
    '% ventana DP080 - Prestadores en convenio
    Public Function insPostCA100(ByVal lstrAction As String, ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nModulec As Integer, ByVal nCover As Integer, ByVal nPolicy As Integer, ByVal nCertif As Integer, ByVal nPrestac As Integer, ByVal nCod_agree As Integer, ByVal dinit_date As Date, ByVal nusercode As String, ByVal nGroup As Integer) As Boolean

        insPostCA100 = True
        Me.sAction = lstrAction
        Me.sCertype = sCertype
        Me.nBranch = nBranch
        Me.nProduct = nProduct
        Me.nModulec = IIf(nModulec = eRemoteDB.Constants.intNull, 0, nModulec)
        Me.nCover = IIf(nCover = eRemoteDB.Constants.intNull, 0, nCover)
        Me.nPolicy = nPolicy
        Me.nCertif = nCertif
        Me.nPrestac = IIf(nPrestac = eRemoteDB.Constants.intNull, 0, nPrestac)
        Me.nCod_agree = nCod_agree
        Me.dinit_date = dinit_date
        Me.nusercode = CInt(nusercode)
        Me.nGroup = IIf(nGroup = eRemoteDB.Constants.intNull, 0, nGroup)

        Select Case lstrAction
            '+ Si la opción seleccionada es Registrar.
            Case "Add"
                insPostCA100 = Add()

                '+ Si la opción seleccionada es Modificar.
            Case "Update"
                insPostCA100 = Update()

                '+ Si la opción seleccionada es Eliminar.
            Case "Del"
                insPostCA100 = Delete()
        End Select
    End Function

    '%Delete: Permite borrar la información de criterios de selección de riesgos.
    Public Function InsUpdLend_Agree_Pres() As Boolean
		Dim lrecDel_Agree_Prod As eRemoteDB.Execute
		
		lrecDel_Agree_Prod = New eRemoteDB.Execute
		
		On Error GoTo InsUpdLend_Agree_Pres_Err
		
		'+ Definición de parámetros para stored procedure 'insudb.delProd_Am_Bil'
		
		With lrecDel_Agree_Prod
			.StoredProcedure = "INSUPDLEND_AGREE_PRES"
			.Parameters.Add("sAction", sAction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 3, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nModulec", nModulec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("ncover", nCover, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("npolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nprestac", nPrestac, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("ncod_agree", nCod_agree, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("Dinit_date", dinit_date, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
			.Parameters.Add("dNulldate", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nusercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nGroup", nGroup, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			InsUpdLend_Agree_Pres = .Run(False)
		End With
		
InsUpdLend_Agree_Pres_Err: 
		If Err.Number Then
			InsUpdLend_Agree_Pres = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecDel_Agree_Prod may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecDel_Agree_Prod = Nothing
	End Function
	
	'% FindLend_agree_Pres: Verifica que exista información por cobertura.
	Public Function Find(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nModulec As Integer, ByVal nCover As Integer, ByVal nPolicy As Integer, ByVal nCertif As Integer, ByVal nPrestac As Integer, ByVal nCod_agree As Integer, ByVal dEffecdate As Date, ByVal nGroup As Integer) As Boolean
		Dim lrecReaLend_agree_Pres As eRemoteDB.Execute
		
		lrecReaLend_agree_Pres = New eRemoteDB.Execute
		
		On Error GoTo Find_Err
		
		'+ Definición de parámetros para stored procedure
		With lrecReaLend_agree_Pres
			.StoredProcedure = "ReaLend_Agree_pres"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nModulec", IIf(nModulec = eRemoteDB.Constants.intNull, 0, nModulec), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("ncover", IIf(nCover = eRemoteDB.Constants.intNull, 0, nCover), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("npolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nprestac", IIf(nPrestac = eRemoteDB.Constants.intNull, 0, nPrestac), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("ncod_agree", nCod_agree, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nGroup", IIf(nGroup = eRemoteDB.Constants.intNull, 0, nGroup), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run Then
				Find = True
				Me.nCod_agree = .FieldToClass("nCod_Agree")
			End If
		End With
		
Find_Err: 
		If Err.Number Then
			Find = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecReaLend_agree_Pres may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecReaLend_agree_Pres = Nothing
	End Function
End Class






