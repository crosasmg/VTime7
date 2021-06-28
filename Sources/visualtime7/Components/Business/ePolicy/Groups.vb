Option Strict Off
Option Explicit On
Public Class Groups
	'%-------------------------------------------------------%'
	'% $Workfile:: Groups.cls                               $%'
	'% $Author:: Nvaplat41                                  $%'
	'% $Date:: 9/10/03 19.01                                $%'
	'% $Revision:: 17                                       $%'
	'%-------------------------------------------------------%'
	'+
	'+ Estructura de tabla Groups al 07-12-2002 09:32:10
	'+     Property                Type         DBType   Size Scale  Prec  Null
	'+-------------------------------------------------------------------------
	Public sCertype As String ' CHAR       1    0     0    N
	Public nBranch As Integer ' NUMBER     22   0     5    N
	Public nPolicy As Double ' NUMBER     22   0     10   N
	Public nProduct As Integer ' NUMBER     22   0     5    N
	Public nGroup As Integer ' NUMBER     22   0     5    N
	Public nGroup_Initial As Integer ' NUMBER     22   0     5    N
	Public dCompdate As Date ' DATE       7    0     0    S
	Public deffecdate As Date ' DATE       7    0     0    N
	Public deNulldate As Date ' DATE       7    0     0    N
	Public sClient As String ' CHAR       14   0     0    S
	Public sDescript As String ' CHAR       30   0     0    S
	Public nParticip As Double ' NUMBER     22   2     5    S
	Public sStatregt As String ' CHAR       1    0     0    S
	Public nUsercode As Integer ' NUMBER     22   0     5    S
	
	Public nParticip_b As Double 'smallint
	'**% Add: This function returns TRUE when adds succesfully the records in the data base
	'% Add: Función que retorna VERDADERO en caso de almacenar exitosamente los registros en la base de datos
	Public Function Add() As Boolean
		Dim lreccreGroups As eRemoteDB.Execute
		
		On Error GoTo Add_err
		
		lreccreGroups = New eRemoteDB.Execute
		'**+Stored procedure parameters definition 'insudb.creGroups'
		'**+Data of 11/09/2000 14.13.53
		'Definición de parámetros para stored procedure 'insudb.creGroups'
		'Información leída el 09/11/2000 14.13.53
		
		With lreccreGroups
			.StoredProcedure = "creGroups"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nGroup", nGroup, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sDescript", sDescript, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 30, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nParticip", nParticip, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sStatregt", sStatregt, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("DPRODUCTDATE", deffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Add = .Run(False)
		End With
		
Add_err: 
		If Err.Number Then
			Add = False
		End If
		On Error GoTo 0
		
		'UPGRADE_NOTE: Object lreccreGroups may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lreccreGroups = Nothing
		
	End Function
	
	'**% Update: This function returns TRUE when updates succesfully the records of a certificates group
	'**% in the data base
	'% Update: Función que retorna VERDADERO al actualizar exitosamente los registros de un grupo
	'% de certificados en la base de datos
	Public Function Update() As Boolean
		
		Dim lrecupdGroups As eRemoteDB.Execute
		
		On Error GoTo Update_Err
		
		lrecupdGroups = New eRemoteDB.Execute
		
		'**+Stored procedure parameters definition 'insudb.updGroups'
		'**+Data of 11/09/2000 14.21.43
		'Definición de parámetros para stored procedure 'insudb.updGroups'
		'Información leída el 09/11/2000 14.21.43
		
		With lrecupdGroups
			.StoredProcedure = "updGroups"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nGroup", nGroup, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sDescript", sDescript, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 30, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nParticip", nParticip, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sStatregt", sStatregt, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecDate", deffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			Update = .Run(False)
		End With
		
Update_Err: 
		If Err.Number Then
			Update = False
		End If
		On Error GoTo 0
		
		'UPGRADE_NOTE: Object lrecupdGroups may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecupdGroups = Nothing
		
	End Function
	
	'**% Delete: This function returns TRUE when  it deletes a record of a group of certificates from the database
	'% Delete: Función que retorna VERDADERO al eliminar un registro de un grupo
	'% de certificados en la base de datos
	Public Function Delete() As Boolean
		
		Dim lrecdelGroups As eRemoteDB.Execute
		
		On Error GoTo Delete_err
		
		lrecdelGroups = New eRemoteDB.Execute
		
		'**+Stored procedure parameters definition 'insudb.delGroups'
		'**+Data of 11/09/2000 14.32.16
		'Definición de parámetros para stored procedure 'insudb.delGroups'
		'Información leída el 09/11/2000 14.32.16
		
		With lrecdelGroups
			.StoredProcedure = "delGroups"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nGroup", nGroup, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecDate", deffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Delete = .Run(False)
		End With
		
Delete_err: 
		If Err.Number Then
			Delete = False
		End If
		On Error GoTo 0
		
		'UPGRADE_NOTE: Object lrecdelGroups may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecdelGroups = Nothing
		
	End Function
	
	'**% Delete: This function returns TRUE when deletes a record of a group of certificates from the database
	'% Delete: Función que retorna VERDADERO al eliminar un registro de un grupo
	'% de certificados en la base de datos
	Public Function Delete1(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double) As Boolean
		Dim lrecdelGroups1 As eRemoteDB.Execute
		
		On Error GoTo Delete1_Err
		
		lrecdelGroups1 = New eRemoteDB.Execute
		
		'**+Stored procedure parameters definition 'insudb.delGroups1'
		'**+Data of 11/13/2000 10:01:13 a.m.
		'Definición de parámetros para stored procedure 'insudb.delGroups1'
		'Información leída el 13/11/2000 10:01:13 a.m.
		
		With lrecdelGroups1
			.StoredProcedure = "delGroups1"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Delete1 = .Run(False)
		End With
		
		
Delete1_Err: 
		If Err.Number Then
			Delete1 = False
		End If
		On Error GoTo 0
		
		'UPGRADE_NOTE: Object lrecdelGroups1 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecdelGroups1 = Nothing
		
	End Function
	
	'**% valGroupExist: This function validates if there are groups associated to a policy
	'% valGroupExist: Valida si existen grupos asociados a una póliza
	Public Function valGroupExist(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal deffecdate As Date) As Boolean
		
		'**- Variable definition. lrecReaGroups_a
		'- Se define la variable lrecreaGroups_a
		Dim lrecreaGroups_a As eRemoteDB.Execute
		
		On Error GoTo valGroupExist_Err
		
		lrecreaGroups_a = New eRemoteDB.Execute
		
		'**+Stored procedure parameters definition insudb.reaGroups_a'
		'**+Data of 12/12/2000 9:28:19
		'+ Definición de parámetros para stored procedure 'insudb.reaGroups_a'
		'+ Información leída el 12/12/2000 9:28:19
		
		nGroup_Initial = eRemoteDB.Constants.intNull
		
		With lrecreaGroups_a
			.StoredProcedure = "reaGroups_a"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("DEFFECDATE", deffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				nGroup = .FieldToClass("nGroup")
				If nGroup_Initial = eRemoteDB.Constants.intNull Then
					nGroup_Initial = nGroup
				End If
				sDescript = .FieldToClass("sDescript")
				.RCloseRec()
				valGroupExist = True
			Else
				valGroupExist = False
			End If
		End With
		
		
valGroupExist_Err: 
		If Err.Number Then
			valGroupExist = False
		End If
		On Error GoTo 0
		
		'UPGRADE_NOTE: Object lrecreaGroups_a may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaGroups_a = Nothing
		
	End Function
	
	'**% valGroupExistByStatus: This routine validates if there are groups associated to the policy
	'**%  (verifying the sStatregt variable value)
	'% valGroupExistByStatus: Valida si existen grupos asociados a una póliza
	'% (tomando en cuenta el valor de la variable sStatregt)
	Public Function valGroupExistByStatus(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nGroup As Integer, ByVal sStatregt As String, ByVal deffecdate As Date) As Boolean
		
		'**- Variable definition. lrecReaGroups
		'- Se define la variable lrecreaGroups
		Dim lrecreaGroups As eRemoteDB.Execute
		
		On Error GoTo valGroupExistByStatus_Err
		
		lrecreaGroups = New eRemoteDB.Execute
		'**+Stored procedure parameters definition 'insudb.reaGroups'
		'**+Data of 01/10/2001 11:19:53
		'+ Definición de parámetros para stored procedure 'insudb.reaGroups'
		'+ Información leída el 10/01/2001 11:19:53
		
		With lrecreaGroups
			.StoredProcedure = "reaGroups"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nGroup", nGroup, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sStatregt", sStatregt, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecDate", deffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run Then
				sCertype = .FieldToClass("sCertype")
				nBranch = .FieldToClass("nBranch")
				nPolicy = .FieldToClass("nPolicy")
				nProduct = .FieldToClass("nProduct")
				nGroup = .FieldToClass("nGroup")
				sClient = .FieldToClass("sClient")
				sDescript = .FieldToClass("sDescript")
				nParticip = .FieldToClass("nParticip")
				sStatregt = .FieldToClass("sStatregt")
				nUsercode = .FieldToClass("nUsercode")
				nParticip_b = .FieldToClass("nParticip2")
				
				.RCloseRec()
				valGroupExistByStatus = True
			Else
				valGroupExistByStatus = False
			End If
		End With
		
valGroupExistByStatus_Err: 
		If Err.Number Then
			valGroupExistByStatus = False
		End If
		On Error GoTo 0
		
		'UPGRADE_NOTE: Object lrecreaGroups may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaGroups = Nothing
	End Function
	
	'**% valGroupExist_a: This function validates if there are groups associated to a policy
	'% valGroupExist_a: Valida si existen grupos asociados a una póliza
	Public Function valGroupExist_a(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal deffecdate As Date) As Boolean
		Dim lrecreaGroups_a As eRemoteDB.Execute
		Dim lintExists As Integer
		
		On Error GoTo valGroupExist_a_Err
		
		lrecreaGroups_a = New eRemoteDB.Execute
		
		With lrecreaGroups_a
			.StoredProcedure = "valExistsGroups"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecDate", deffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nExists", lintExists, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			
			.Run(False)
			If .Parameters("nExists").Value = 1 Then
				valGroupExist_a = True
			End If
		End With
		
valGroupExist_a_Err: 
		If Err.Number Then
			valGroupExist_a = False
		End If
		On Error GoTo 0
		
		'UPGRADE_NOTE: Object lrecreaGroups_a may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaGroups_a = Nothing
	End Function
	
	'% getCountGroups: Valida si existen grupos asociados a una póliza
	Public Function getCountGroups(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double) As Integer
		Dim lrecGroups As eRemoteDB.Execute
		Dim llngCount As Integer
		
		On Error GoTo getCountGroups_Err
		
		lrecGroups = New eRemoteDB.Execute
		
		With lrecGroups
			.StoredProcedure = "getCountGroups"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCount", llngCount, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Run(False)
			getCountGroups = .Parameters("nCount").Value
		End With
		
getCountGroups_Err: 
		If Err.Number Then
			getCountGroups = -1
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecGroups may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecGroups = Nothing
	End Function
End Class






