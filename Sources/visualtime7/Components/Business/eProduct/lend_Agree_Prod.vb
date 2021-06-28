Option Strict Off
Option Explicit On
<System.Runtime.InteropServices.ProgId("Lend_Agree_Prod_NET.Lend_Agree_Prod")> Public Class Lend_Agree_Prod
	'%-------------------------------------------------------%'
	'% $Workfile:: Lend_Agree_Prod.cls                           $%'
	'% $Author:: lsereno                                   $%'
	'% $Date:: 03/05/07 12:35p                               $%'
	'% $Revision:: 01                                       $%'
	'%-------------------------------------------------------%'
	
	'- Estructura de tabla Prod_Am_Bil al 06-27-2002 10:44:47
	'-        Property                Type
	'----------------------------------------
	Public nBranch As Integer
	Public nProduct As Integer
	Public dEffecdate As Date
	Public nCod_Agree As Integer
	Public nUsercode As Integer
	'%Add: Permite registrar la información los prestadores por producto.
	Public Function Add() As Boolean
		Dim lrecCreProd_Le_Pr As eRemoteDB.Execute
		
		lrecCreProd_Le_Pr = New eRemoteDB.Execute
		
		On Error GoTo Add_err
		
		'+ Definición de parámetros para stored procedure 'insudb.Add_Lend_Agree_Prod'
		
		With lrecCreProd_Le_Pr
			
			.StoredProcedure = "Add_Lend_Agree_Prod"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("ncod_agree", nCod_Agree, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("deffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			Add = .Run(False)
		End With
		
		'UPGRADE_NOTE: Object lrecCreProd_Le_Pr may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecCreProd_Le_Pr = Nothing
		
Add_err: 
		If Err.Number Then
			Add = False
		End If
		
		On Error GoTo 0
	End Function
	
	'%Delete: Permite borrar la información de criterios de selección de riesgos.
	Public Function Delete() As Boolean
		Dim lrecDel_Agree_Prod As eRemoteDB.Execute
		
		lrecDel_Agree_Prod = New eRemoteDB.Execute
		
		On Error GoTo Delete_Err
		
		'+ Definición de parámetros para stored procedure 'insudb.delProd_Am_Bil'
		
		With lrecDel_Agree_Prod
			.StoredProcedure = "Del_Lend_Agree_prod"
			
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("ncod_agree", nCod_Agree, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("deffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			Delete = .Run(False)
		End With
		
		'UPGRADE_NOTE: Object lrecDel_Agree_Prod may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecDel_Agree_Prod = Nothing
		
Delete_Err: 
		If Err.Number Then
			Delete = False
		End If
		
		On Error GoTo 0
	End Function
	
	''% insValDP080: Realiza la validación de los campos a actualizar en el frame (ventana) DP042
	''  (clientes permitidos para todos los tipos de póliza)
	''--------------------------------------------------------------------------------------------
	Public Function insValDP080(ByVal sCodispl As String, ByVal sAction As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nCod_Agree As Integer, ByVal dEffecdate As Date) As String
		Dim lclsErrors As eFunctions.Errors
		
		On Error GoTo insValDP080_Err
		
		lclsErrors = New eFunctions.Errors
		
		With lclsErrors
			'+ valida prestador
			If nCod_Agree = eRemoteDB.Constants.intNull Then
				Call .ErrorMessage(sCodispl, 767093)
			End If
			If Find(nBranch, nProduct, dEffecdate, nCod_Agree) Then
				Call .ErrorMessage(sCodispl, 10004)
			End If
			
			insValDP080 = .Confirm
		End With
		
		insValDP080 = lclsErrors.Confirm
		
insValDP080_Err: 
		If Err.Number Then
			insValDP080 = "insValDP080: " & Err.Description
		End If
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
		On Error GoTo 0
	End Function

    '% insPostDP080: Esta función se encarga de almacenar los datos en las tablas, en este caso lend_agree_prod
    '% ventana DP080 - Prestadores en convenio
    Public Function insPostDP080(ByVal lstrAction As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nCod_Agree As Integer, ByVal dEffecdate As Date, ByVal nUsercode As Integer) As Boolean

        insPostDP080 = True

        Me.nBranch = nBranch
        Me.nProduct = nProduct
        Me.nCod_Agree = nCod_Agree
        Me.dEffecdate = dEffecdate
        Me.nUsercode = nUsercode

        Select Case lstrAction
            '+ Si la opción seleccionada es Registrar.
            Case "Add"
                insPostDP080 = Add()

                '+ Si la opción seleccionada es Eliminar.
            Case "Delete"
                insPostDP080 = Delete()
        End Select
    End Function

    '% FindLend_agree_Prod: Verifica que exista información por cobertura.
    Public Function Find(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal dEffecdate As Date, ByVal nCod_Agree As Double, Optional ByVal lblnFind As Boolean = True) As Boolean
		Dim lrecReaLend_agree_Prod As eRemoteDB.Execute
		
		lrecReaLend_agree_Prod = New eRemoteDB.Execute
		
		On Error GoTo Find_Err
		
		'+ Definición de parámetros para stored procedure
		With lrecReaLend_agree_Prod
			.StoredProcedure = "reaLend_Agree_prod"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("deffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCod_Agree", nCod_Agree, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run Then
				Find = True
				Me.nCod_Agree = .FieldToClass("nCod_Agree")
			End If
			
		End With
		
Find_Err: 
		If Err.Number Then
			Find = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecReaLend_agree_Prod may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecReaLend_agree_Prod = Nothing
	End Function
End Class






