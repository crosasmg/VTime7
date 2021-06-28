Option Strict Off
Option Explicit On
Public Class Tar_SOAT
	'**+Objective: Class that supports the table Execute it's content is:
	'**+Version: $$Revision: 2 $
	'+Objetivo: Clase que le da soporte a la tabla Execute cuyo contenido es:
	'+Version: $$Revision: 2 $
	
	'**+Objective: Properties according to the table 'Tar_SOAT' in the system 05/01/2005 11:41:59 AM
	'+Objetivo: Propiedades según la tabla 'Tar_SOAT' en el sistema 05/01/2005 11:41:59 AM
	
	'**+Objective:
	'+Objetivo:
	Public nCurrency As Short
	
	'**+Objective:
	'+Objetivo:
	Public dEffecdate As Date
	
	'**+Objective:
	'+Objetivo:
	Public nBranch As Short
	
	'**+Objective:
	'+Objetivo:
	Public nProduct As Short
	
	'**+Objective:
	'+Objetivo:
	Public nTypeCalculate As Short
	
	'**+Objective:
	'+Objetivo:
	Public nGroupVeh As Short
	
	'**+Objective:
	'+Objetivo:
	Public nTariff As Short
	
	'**+Objective:
	'+Objetivo:
	Public nVehType As Short
	
	'**+Objective:
	'+Objetivo:
	Public nLocat_Type As Short
	
	'**+Objective:
	'+Objetivo:
	Public nPremiumn As Double
	
	'**+Objective:
	'+Objetivo:
	Public nPremiumTar As Double
	
	'**+Objective:
	'+Objetivo:
    Public dNullDate As Date

    Public nVehBrand As Integer

    Public sVehModel As String

    Public nPlace As Integer

    Public nPersontyp As Integer

    Public nTypePremium As Integer

    Public nSeats As Integer
	
	'**+Objective:
	'+Objetivo:
	Public bEditRecord As Boolean
	
	
	'**%Objective: Add a record to the table "Tar_SOAT"
	'**%Parameters:
	'**%    Pending   -
	'%Objetivo: Agrega un registro a la tabla "Tar_SOAT"
	'%Parámetros:
	'%    Pendiente -
	Private Function Add(ByVal nUsercode As Integer, ByVal nCurrency As Short, ByVal dEffecdate As Date, ByVal nBranch As Short, ByVal nProduct As Short, ByVal nTypeCalculate As Short, ByVal nGroupVeh As Short, ByVal nTariff As Short, ByVal nVehType As Short, ByVal nLocat_Type As Short, ByVal nPremiumn As Double, ByVal nPremiumTar As Double, ByVal sAction As String) As Boolean
		Dim lclsTar_SOAT As eRemoteDB.Execute
		
        On Error GoTo ErrorHandler
		
		lclsTar_SOAT = New eRemoteDB.Execute
		
		'+ Define all parameters for the stored procedures 'insudb.creTar_SOAT'. Generated on 05/01/2005 11:41:59 AM
		
		With lclsTar_SOAT
			.StoredProcedure = "insupdTar_Soat"
			.Parameters.Add("ncurrency", nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecDate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTypeCalculate", nTypeCalculate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nGroupVeh", nGroupVeh, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTariff", nTariff, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nVehType", nVehType, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nLocat_Type", nLocat_Type, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("npremiumn", nPremiumn, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPremiumTar", nPremiumTar, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sAction", sAction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Add = .Run(False)
		End With
		lclsTar_SOAT = Nothing
		
		Exit Function
ErrorHandler: 
        If Err.Number Then
            Add = False
        End If
	End Function

    Private Function AddMSO8500(ByVal nUsercode As Integer, ByVal nCurrency As Short, ByVal dEffecdate As Date, ByVal nBranch As Short, ByVal nProduct As Short, ByVal nGroupAuto As Short, ByVal nClass As Integer, ByVal nTrademarks As Integer, ByVal nModel As Integer, ByVal nSeats As Integer, ByVal nMovement As Integer, ByVal nTypeperson As Integer, ByVal Typepremiun As Integer, ByVal nTypeCalculate As Integer, ByVal nPremium As Integer, ByVal sAction As String) As Boolean
        Dim lclsTar_SOAT As eRemoteDB.Execute

        On Error GoTo ErrorHandler

        lclsTar_SOAT = New eRemoteDB.Execute

        '+ Define all parameters for the stored procedures 'insudb.creTar_SOAT'. Generated on 05/01/2005 11:41:59 AM

        With lclsTar_SOAT
            .StoredProcedure = "INSUPDTARIF_SOAT"
            .Parameters.Add("ncurrency", nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecDate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nGroupAuto", nGroupAuto, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nClass", nClass, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nTrademarks", nTrademarks, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nModel", nModel, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nSeats", nSeats, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nMovement", nMovement, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nTypeperson", nTypeperson, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("Typepremiun", Typepremiun, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nTypeCalculate", nTypeCalculate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPremium", nPremium, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sAction", sAction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            AddMSO8500 = .Run(False)
        End With
        lclsTar_SOAT = Nothing

        Exit Function
ErrorHandler:
        If Err.Number Then
            AddMSO8500 = False
        End If
    End Function

    Private Function AddMSO009(ByVal nUsercode As Integer, ByVal nCurrency As Short, ByVal dEffecdate As Date, ByVal nBranch As Short, ByVal nProduct As Short, ByVal nGroupAuto As Short, ByVal nClass As Integer, ByVal nTrademarks As Integer, ByVal nModel As Integer, ByVal nSeats As Integer, ByVal nMovement As Integer, ByVal nTypeperson As Integer, ByVal Typepremiun As Integer, ByVal nTypeCalculate As Integer, ByVal nPremium As Integer, ByVal sAction As String) As Boolean
        Dim lclsTar_SOAT As eRemoteDB.Execute

        On Error GoTo ErrorHandler

        lclsTar_SOAT = New eRemoteDB.Execute

        '+ Define all parameters for the stored procedures 'insudb.creTar_SOAT'. Generated on 05/01/2005 11:41:59 AM

        With lclsTar_SOAT
            .StoredProcedure = "INSUPDTARIF_SOAT"
            .Parameters.Add("ncurrency", nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecDate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nGroupAuto", nGroupAuto, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nClass", nClass, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nTrademarks", nTrademarks, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            ' .Parameters.Add("nModel", nModel, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            ' .Parameters.Add("nSeats", nSeats, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            ' .Parameters.Add("nMovement", nMovement, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            ' .Parameters.Add("nTypeperson", nTypeperson, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            ' .Parameters.Add("Typepremiun", Typepremiun, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            ' .Parameters.Add("nTypeCalculate", nTypeCalculate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            ' .Parameters.Add("nPremium", nPremium, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            ' .Parameters.Add("sAction", sAction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            ' .Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            AddMSO009 = .Run(False)
        End With
        lclsTar_SOAT = Nothing

        Exit Function
ErrorHandler:
        If Err.Number Then
            AddMSO009 = False
        End If
    End Function


	'**%Objective: Updates a registry to the table "Tar_SOAT" using the key for this table.
	'**%Parameters:
	'**%    Pending   -
	'%Objetivo: Actualiza un registro a la tabla "Tar_SOAT" usando la clave para dicha tabla.
	'%Parámetros:
	'%    Pendiente -
	Private Function Update(ByVal nUsercode As Integer, ByVal nCurrency As Short, ByVal dEffecdate As Date, ByVal nBranch As Short, ByVal nProduct As Short, ByVal nTypeCalculate As Short, ByVal nGroupVeh As Short, ByVal nTariff As Short, ByVal nVehType As Short, ByVal nLocat_Type As Short, ByVal nPremiumn As Double, ByVal nPremiumTar As Double, ByVal sAction As String) As Boolean
		Dim lclsTar_SOAT As eRemoteDB.Execute
		
        On Error GoTo ErrorHandler
		
		lclsTar_SOAT = New eRemoteDB.Execute
		
		'+ Define all parameters for the stored procedures 'insudb.updTar_SOAT'. Generated on 05/01/2005 11:41:59 AM
		With lclsTar_SOAT
			.StoredProcedure = "insupdTar_Soat"
			.Parameters.Add("ncurrency", nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecDate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTypeCalculate", nTypeCalculate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nGroupVeh", nGroupVeh, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTariff", nTariff, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nVehType", nVehType, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nLocat_Type", nLocat_Type, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("npremiumn", nPremiumn, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPremiumTar", nPremiumTar, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			.Parameters.Add("sAction", sAction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Update = .Run(False)
		End With
		lclsTar_SOAT = Nothing
		
		Exit Function
ErrorHandler: 
        If Err.Number Then
            Update = False
        End If
    End Function




    '**%Objective: Updates a registry to the table "Tar_SOAT" using the key for this table.
    '**%Parameters:
    '**%    Pending   -
    '%Objetivo: Actualiza un registro a la tabla "Tar_SOAT" usando la clave para dicha tabla.
    '%Parámetros:
    '%    Pendiente -
    Private Function UpdateMSO8500(ByVal nUsercode As Integer, ByVal nCurrency As Short, ByVal dEffecdate As Date, ByVal nBranch As Short, ByVal nProduct As Short, ByVal nGroupAuto As Short, ByVal nClass As Integer, ByVal nTrademarks As Integer, ByVal nModel As Integer, ByVal nSeats As Integer, ByVal nMovement As Integer, ByVal nTypeperson As Integer, ByVal Typepremiun As Integer, ByVal nTypeCalculate As Integer, ByVal nPremium As Integer, ByVal sAction As String) As Boolean
        Dim lclsTar_SOAT As eRemoteDB.Execute

        On Error GoTo ErrorHandler

        lclsTar_SOAT = New eRemoteDB.Execute

        '+ Define all parameters for the stored procedures 'insudb.updTar_SOAT'. Generated on 05/01/2005 11:41:59 AM
        With lclsTar_SOAT
            .StoredProcedure = "INSUPDTARIF_SOAT"
            .Parameters.Add("ncurrency", nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecDate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nGroupAuto", nGroupAuto, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nClass", nClass, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nTrademarks", nTrademarks, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nModel", nModel, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nSeats", nSeats, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nMovement", nMovement, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nTypeperson", nTypeperson, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("Typepremiun", Typepremiun, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nTypeCalculate", nTypeCalculate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPremium", nPremium, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sAction", sAction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            UpdateMSO8500 = .Run(False)
        End With
        lclsTar_SOAT = Nothing

        Exit Function
ErrorHandler:
        If Err.Number Then
            UpdateMSO8500 = False
        End If
    End Function



    '**%Objective: Updates a registry to the table "Tar_SOAT" using the key for this table.
    '**%Parameters:
    '**%    Pending   -
    '%Objetivo: Actualiza un registro a la tabla "Tar_SOAT" usando la clave para dicha tabla.
    '%Parámetros:
    '%    Pendiente -
    Private Function UpdateMSO009(ByVal nUsercode As Integer, ByVal nCurrency As Short, ByVal dEffecdate As Date, ByVal nBranch As Short, ByVal nProduct As Short, ByVal nGroupAuto As Short, ByVal nClass As Integer, ByVal nTrademarks As Integer, ByVal nModel As Integer, ByVal nSeats As Integer, ByVal nMovement As Integer, ByVal nTypeperson As Integer, ByVal Typepremiun As Integer, ByVal nTypeCalculate As Integer, ByVal nPremium As Integer, ByVal sAction As String) As Boolean
        Dim lclsTar_SOAT As eRemoteDB.Execute

        On Error GoTo ErrorHandler

        lclsTar_SOAT = New eRemoteDB.Execute

        '+ Define all parameters for the stored procedures 'insudb.updTar_SOAT'. Generated on 05/01/2005 11:41:59 AM
        With lclsTar_SOAT
            .StoredProcedure = "INSUPDTARIF_SOAT"
            .Parameters.Add("ncurrency", nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecDate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nGroupAuto", nGroupAuto, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nClass", nClass, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nTrademarks", nTrademarks, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nModel", nModel, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nSeats", nSeats, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nMovement", nMovement, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nTypeperson", nTypeperson, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("Typepremiun", Typepremiun, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nTypeCalculate", nTypeCalculate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPremium", nPremium, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sAction", sAction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            UpdateMSO009 = .Run(False)
        End With
        lclsTar_SOAT = Nothing

        Exit Function
ErrorHandler:
        If Err.Number Then
            UpdateMSO009 = False
        End If
    End Function

	'**%Objective: Delete a registry the table "Tar_SOAT" using the key for this table.
	'**%Parameters:
	'**%    Pending   -
	'%Objetivo: Elimina un registro a la tabla "Tar_SOAT" usando la clave para dicha tabla.
	'%Parámetros:
	'%    Pendiente -
	Private Function Delete(ByVal nUsercode As Integer, ByVal nCurrency As Short, ByVal dEffecdate As Date, ByVal nBranch As Short, ByVal nProduct As Short, ByVal nTypeCalculate As Short, ByVal nGroupVeh As Short, ByVal nTariff As Short, ByVal nVehType As Short, ByVal nLocat_Type As Short, ByVal nPremiumn As Double, ByVal nPremiumTar As Double, ByVal sAction As String) As Boolean
		Dim lclsTar_SOAT As eRemoteDB.Execute
		
        On Error GoTo ErrorHandler

        lclsTar_SOAT = New eRemoteDB.Execute

        '+ Define all parameters for the stored procedures 'insudb.delTar_SOAT'. Generated on 05/01/2005 11:41:59 AM
        With lclsTar_SOAT
            .StoredProcedure = "insupdTar_Soat"
            .Parameters.Add("ncurrency", nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecDate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nTypeCalculate", nTypeCalculate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nGroupVeh", nGroupVeh, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nTariff", nTariff, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nVehType", nVehType, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nLocat_Type", nLocat_Type, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("npremiumn", nPremiumn, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPremiumTar", nPremiumTar, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            .Parameters.Add("sAction", sAction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            Delete = .Run(False)
        End With
        lclsTar_SOAT = Nothing

        Exit Function
ErrorHandler:
        If Err.Number Then
            Delete = False
        End If
    End Function

    '**%Objective: Delete a registry the table "Tar_SOAT" using the key for this table.
    '**%Parameters:
    '**%    Pending   -
    '%Objetivo: Elimina un registro a la tabla "Tar_SOAT" usando la clave para dicha tabla.
    '%Parámetros:
    '%    Pendiente -
    Private Function DeleteMSO8500(ByVal nUsercode As Integer, ByVal nCurrency As Short, ByVal dEffecdate As Date, ByVal nBranch As Short, ByVal nProduct As Short, ByVal nGroupAuto As Short, ByVal nClass As Integer, ByVal nTrademarks As Integer, ByVal nModel As Integer, ByVal nSeats As Integer, ByVal nMovement As Integer, ByVal nTypeperson As Integer, ByVal Typepremiun As Integer, ByVal nTypeCalculate As Integer, ByVal nPremium As Integer, ByVal sAction As String) As Boolean
        Dim lclsTar_SOAT As eRemoteDB.Execute

        On Error GoTo ErrorHandler

        lclsTar_SOAT = New eRemoteDB.Execute

        '+ Define all parameters for the stored procedures 'insudb.delTar_SOAT'. Generated on 05/01/2005 11:41:59 AM
        With lclsTar_SOAT
            .StoredProcedure = "INSUPDTARIF_SOAT"
            .Parameters.Add("ncurrency", nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecDate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nGroupAuto", nGroupAuto, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nClass", nClass, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nTrademarks", nTrademarks, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nModel", nModel, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nSeats", nSeats, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nMovement", nMovement, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nTypeperson", nTypeperson, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("Typepremiun", Typepremiun, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nTypeCalculate", nTypeCalculate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPremium", nPremium, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sAction", sAction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            DeleteMSO8500 = .Run(False)
        End With
        lclsTar_SOAT = Nothing

        Exit Function
ErrorHandler:
        If Err.Number Then
            DeleteMSO8500 = False
        End If
    End Function


    '**%Objective: Delete a registry the table "Tar_SOAT" using the key for this table.
    '**%Parameters:
    '**%    Pending   -
    '%Objetivo: Elimina un registro a la tabla "Tar_SOAT" usando la clave para dicha tabla.
    '%Parámetros:
    '%    Pendiente -
    Private Function DeleteMSO009(ByVal nUsercode As Integer, ByVal nCurrency As Short, ByVal dEffecdate As Date, ByVal nBranch As Short, ByVal nProduct As Short, ByVal nGroupAuto As Short, ByVal nClass As Integer, ByVal nTrademarks As Integer, ByVal nModel As Integer, ByVal nSeats As Integer, ByVal nMovement As Integer, ByVal nTypeperson As Integer, ByVal Typepremiun As Integer, ByVal nTypeCalculate As Integer, ByVal nPremium As Integer, ByVal sAction As String) As Boolean
        Dim lclsTar_SOAT As eRemoteDB.Execute

        On Error GoTo ErrorHandler

        lclsTar_SOAT = New eRemoteDB.Execute

        '+ Define all parameters for the stored procedures 'insudb.delTar_SOAT'. Generated on 05/01/2005 11:41:59 AM
        With lclsTar_SOAT
            .StoredProcedure = "INSUPDTARIF_SOAT"
            .Parameters.Add("ncurrency", nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecDate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nGroupAuto", nGroupAuto, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nClass", nClass, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nTrademarks", nTrademarks, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nModel", nModel, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            '.Parameters.Add("nSeats", nSeats, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            '.Parameters.Add("nMovement", nMovement, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            '.Parameters.Add("nTypeperson", nTypeperson, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            '.Parameters.Add("Typepremiun", Typepremiun, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            '.Parameters.Add("nTypeCalculate", nTypeCalculate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            '.Parameters.Add("nPremium", nPremium, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            '.Parameters.Add("sAction", sAction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            '.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            DeleteMSO009 = .Run(False)
        End With
        lclsTar_SOAT = Nothing

        Exit Function
ErrorHandler:
        If Err.Number Then
            DeleteMSO009 = False
        End If
    End Function


    '**%Objective: It verifies the existence of a registry in table "Tar_SOAT" using the key of this table.
    '**%Parameters:
    '**%    Pending   -
    '%Objetivo: Verifica la existencia de un registro en la tabla "Tar_SOAT" usando la clave de dicha tabla.
    '%Parámetros:
    '%    Pendiente -
    Private Function IsExist(ByVal nCurrency As Short, ByVal dEffecdate As Date, ByVal nBranch As Short, ByVal nProduct As Short, ByVal nGroupVeh As Short, ByVal nVehType As Short, ByVal nLocat_Type As Short) As Boolean
        Dim lclsTar_SOAT As eRemoteDB.Execute
        Dim lintExist As Short

        On Error GoTo ErrorHandler

        lclsTar_SOAT = New eRemoteDB.Execute
        lintExist = 0

        '+ Define all parameters for the stored procedures 'insudb.valTar_SOATExist'. Generated on 05/01/2005 11:41:59 AM
        With lclsTar_SOAT
            .StoredProcedure = "reaTar_SOAT_v"
            .Parameters.Add("ncurrency", nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecDate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nGroupVeh", nGroupVeh, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nVehType", nVehType, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nLocat_Type", nLocat_Type, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nExist", lintExist, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If .Run(False) Then
                IsExist = (.Parameters("nExist").Value = 1)
            Else
                IsExist = False
            End If
        End With
        lclsTar_SOAT = Nothing

        Exit Function
ErrorHandler:
        If Err.Number Then
            IsExist = False
        End If
    End Function

    Private Function IsExistMSO8500(ByVal nCurrency As Short, ByVal dEffecdate As Date, ByVal nBranch As Short, ByVal nProduct As Short, ByVal nTypeperson As Short) As Boolean
        Dim lclsTar_SOAT As eRemoteDB.Execute
        Dim lintExist As Short

        On Error GoTo ErrorHandler

        lclsTar_SOAT = New eRemoteDB.Execute
        lintExist = 0

        '+ Define all parameters for the stored procedures 'insudb.valTar_SOATExist'. Generated on 05/01/2005 11:41:59 AM
        With lclsTar_SOAT
            .StoredProcedure = "Existtarif_soat"
            .Parameters.Add("ncurrency", nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecDate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nTypeperson", nTypeperson, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nExist", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If .Run(False) Then
                IsExistMSO8500 = (.Parameters("nExist").Value = 1)
            Else
                IsExistMSO8500 = False
            End If
        End With
        lclsTar_SOAT = Nothing

        Exit Function
ErrorHandler:
        If Err.Number Then
            IsExistMSO8500 = False
        End If
    End Function


    Private Function IsExistMSO009(ByVal nCurrency As Short, ByVal dEffecdate As Date, ByVal nBranch As Short, ByVal nProduct As Short, ByVal nTypeperson As Short) As Boolean
        Dim lclsTar_SOAT As eRemoteDB.Execute
        Dim lintExist As Short

        On Error GoTo ErrorHandler

        lclsTar_SOAT = New eRemoteDB.Execute
        lintExist = 0

        '+ Define all parameters for the stored procedures 'insudb.valTar_SOATExist'. Generated on 05/01/2005 11:41:59 AM
        With lclsTar_SOAT
            .StoredProcedure = "Existtarif_soat"
            .Parameters.Add("ncurrency", nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecDate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nTypeperson", nTypeperson, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nExist", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If .Run(False) Then
                IsExistMSO009 = (.Parameters("nExist").Value = 1)
            Else
                IsExistMSO009 = False
            End If
        End With
        lclsTar_SOAT = Nothing

        Exit Function
ErrorHandler:
        If Err.Number Then
            IsExistMSO009 = False
        End If
    End Function

    '**%Objective: Validation of the data for the page of the headed one.
    '**%Parameters:
    '**%    Pending   -
    '%Objetivo: Validación de los datos para la página del encabezado.
    '%Parámetros:
    '%    Pendiente -
    Public Function InsValMSO6001_k(ByVal sCodispl As String, ByVal nMainAction As Integer, ByVal sAction As String, ByVal nCurrency As Short, ByVal dEffecdate As Date, ByVal nBranch As Short, ByVal nProduct As Short) As String
        Dim lclsErrors As eFunctions.Errors

        On Error GoTo ErrorHandler

        lclsErrors = New eFunctions.Errors

        '+ SE VALIDA QUE EL RAMO ESTE LLENO
        If (nBranch = 0 Or nBranch = eRemoteDB.Constants.intNull) Then
            Call lclsErrors.ErrorMessage(sCodispl, 9064)
        End If

        '+ Se valida que el producto este lleno
        If (nProduct = 0 Or nProduct = eRemoteDB.Constants.intNull) Then
            Call lclsErrors.ErrorMessage(sCodispl, 1014)
        End If

        '+ SE VALIDA QUE LA MONEDA ESTE LLENA
        If (nCurrency = 0 Or nCurrency = eRemoteDB.Constants.intNull) Then
            Call lclsErrors.ErrorMessage(sCodispl, 1351)
        End If

        '+ SE VALIDA QUE LA FECHA ESTE LLENA
        If dEffecdate = dtmNull Then
            Call lclsErrors.ErrorMessage(sCodispl, 4003)
        Else
            '+ SE VALIDA QUE SI LAACCIÓN ES DIFERENTE DE CONSULTA LA FECHA DEBE SER
            '+ POSTERIOR A LA FECHA DEL COMPUTADOR
            If nMainAction <> 401 And dEffecdate <= Today Then
                Call lclsErrors.ErrorMessage(sCodispl, 10868)
            End If

            '+ Verifica si hay registro que no se pueden modificar
            If nMainAction = 302 Then
                If dEffecdate <= Me.Find_LastDate(nBranch, nProduct, nCurrency, dEffecdate) Then
                    Call lclsErrors.ErrorMessage(sCodispl, 91004, , , , , "(" & Me.Find_LastDate(nBranch, nProduct, nCurrency, dEffecdate) & ")")
                End If
            End If

        End If

        InsValMSO6001_k = lclsErrors.Confirm

        lclsErrors = Nothing

        Exit Function
ErrorHandler:
        If Err.Number Then
            InsValMSO6001_k = InsValMSO6001_k & Err.Description
        End If
    End Function

    '**%Objective: Validation of the data for the page details.
    '**%Parameters:
    '**%    Pending   -
    '%Objetivo: Validación de los datos para la página detalle.
    '%Parámetros:
    '%    Pendiente -
    Public Function InsValMSO6001(ByVal sCodispl As String, ByVal nMainAction As Integer, ByVal sAction As String, ByVal nCurrency As Short, ByVal dEffecdate As Date, ByVal nBranch As Short, ByVal nProduct As Short, ByVal nTypeCalculate As Short, ByVal nGroupVeh As Short, ByVal nTariff As Short, ByVal nVehType As Short, ByVal nLocat_Type As Short, ByVal nPremiumn As Double, ByVal nPremiumTar As Double) As String
        Dim lclsErrors As eFunctions.Errors

        On Error GoTo ErrorHandler

        lclsErrors = New eFunctions.Errors

        If (nVehType = 0 Or nVehType = eRemoteDB.Constants.intNull) Then
            Call lclsErrors.ErrorMessage(sCodispl, 91002)
        End If

        If sAction = "Add" And IsExist(nCurrency, dEffecdate, nBranch, nProduct, nGroupVeh, nVehType, nLocat_Type) Then
            Call lclsErrors.ErrorMessage(sCodispl, 7148)
        End If

        If (nGroupVeh = 0 Or nGroupVeh = eRemoteDB.Constants.intNull) Then
            Call lclsErrors.ErrorMessage(sCodispl, 91003)
        End If

        If (nTypeCalculate = 0 Or nTypeCalculate = eRemoteDB.Constants.intNull) Then
            Call lclsErrors.ErrorMessage(sCodispl, 9040)
        End If

        If (nPremiumn = 0 Or nPremiumn = eRemoteDB.Constants.intNull) Then
            Call lclsErrors.ErrorMessage(sCodispl, 13944)
        End If

        If (nPremiumTar = 0 Or nPremiumTar = eRemoteDB.Constants.intNull) Then
            Call lclsErrors.ErrorMessage(sCodispl, 3689)
        End If

        InsValMSO6001 = lclsErrors.Confirm
        lclsErrors = Nothing

        Exit Function
ErrorHandler:
        If Err.Number Then
            InsValMSO6001 = InsValMSO6001 & Err.Description
        End If
    End Function

    '**%Objective: Pass of the information introduced towards the layers of rules of business and access of data.
    '**%Parameters:
    '**%    Pending   -
    '%Objetivo: Se encarga de llevar la información introducida hacia las capas de reglas de negocio y acceso de datos.
    '%Parámetros:
    '%    Pendiente -
    Public Function InsPostMSO6001(ByVal pblnHeader As Boolean, ByVal sCodispl As String, ByVal nMainAction As Integer, ByVal sAction As String, ByVal nUsercode As Integer, ByVal nCurrency As Integer, ByVal dEffecdate As Date, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nTypeCalculate As Integer, ByVal nGroupVeh As Integer, ByVal nTariff As Integer, ByVal nVehType As Integer, ByVal nLocat_Type As Integer, ByVal nPremiumn As Double, ByVal nPremiumTar As Double) As Boolean
        On Error GoTo ErrorHandler

        If pblnHeader Then
            InsPostMSO6001 = True
        Else
            If sAction = "Add" Then
                InsPostMSO6001 = Add(nUsercode, nCurrency, dEffecdate, nBranch, nProduct, nTypeCalculate, nGroupVeh, nTariff, nVehType, nLocat_Type, nPremiumn, nPremiumTar, CStr(1))
            ElseIf sAction = "Update" Then
                InsPostMSO6001 = Update(nUsercode, nCurrency, dEffecdate, nBranch, nProduct, nTypeCalculate, nGroupVeh, nTariff, nVehType, nLocat_Type, nPremiumn, nPremiumTar, CStr(2))
            ElseIf sAction = "Del" Then
                InsPostMSO6001 = Delete(nUsercode, nCurrency, dEffecdate, nBranch, nProduct, nTypeCalculate, nGroupVeh, nTariff, nVehType, nLocat_Type, nPremiumn, nPremiumTar, CStr(3))
            End If
        End If

        Exit Function
ErrorHandler:
        If Err.Number Then
            InsPostMSO6001 = False
        End If
    End Function
    '**%Objective: Validation of the data for the page of the headed one.
    '**%Parameters:
    '**%    Pending   -
    '%Objetivo: Validación de los datos para la página del encabezado.
    '%Parámetros:
    '%    Pendiente -
    Public Function InsValMSO8500_k(ByVal sCodispl As String, ByVal nMainAction As Integer, ByVal sAction As String, ByVal nCurrency As Short, ByVal dEffecdate As Date, ByVal nBranch As Short, ByVal nProduct As Short) As String
        Dim lclsErrors As eFunctions.Errors

        On Error GoTo ErrorHandler

        lclsErrors = New eFunctions.Errors

        '+ SE VALIDA QUE EL RAMO ESTE LLENO
        If (nBranch = 0 Or nBranch = eRemoteDB.Constants.intNull) Then
            Call lclsErrors.ErrorMessage(sCodispl, 9064)
        End If

        '+ SE VALIDA QUE EL PRODUCTO ESTE LLENO
        If (nProduct = 0 Or nProduct = eRemoteDB.Constants.intNull) Then
            Call lclsErrors.ErrorMessage(sCodispl, 70109)
        End If

        '+ SE VALIDA QUE LA MONEDA ESTE LLENA
        If (nCurrency = 0 Or nCurrency = eRemoteDB.Constants.intNull) Then
            Call lclsErrors.ErrorMessage(sCodispl, 1351)
        End If

        '+ SE VALIDA QUE LA FECHA ESTE LLENA
        If dEffecdate = dtmNull Then
            Call lclsErrors.ErrorMessage(sCodispl, 4003)
        End If

        '+ SE VALIDA QUE SI LAACCIÓN ES DIFERENTE DE CONSULTA LA FECHA DEBE SER
        '+ POSTERIOR A LA FECHA DEL COMPUTADOR
        If nMainAction <> 401 And dEffecdate <= Today And dEffecdate <> dtmNull Then
            Call lclsErrors.ErrorMessage(sCodispl, 10868)
        End If

        '+ Verifica si hay registro que no se pueden modificar
        If nMainAction = 302 And dEffecdate <> dtmNull Then
            If dEffecdate <= Me.Find_LastDate_Soat(nBranch, nProduct, nCurrency, dEffecdate) Then
                Call lclsErrors.ErrorMessage(sCodispl, 91004, , , , , "(" & Me.Find_LastDate_Soat(nBranch, nProduct, nCurrency, dEffecdate) & ")")
            End If
        End If


        InsValMSO8500_k = lclsErrors.Confirm

        lclsErrors = Nothing

        Exit Function
ErrorHandler:
        If Err.Number Then
            InsValMSO8500_k = InsValMSO8500_k & Err.Description
        End If
    End Function

    '**%Objective: Validation of the data for the page details.
    '**%Parameters:
    '**%    Pending   -
    '%Objetivo: Validación de los datos para la página detalle.
    '%Parámetros:
    '%    Pendiente -
    Public Function InsValMSO8500(ByVal sCodispl As String, ByVal nMainAction As Integer, ByVal sAction As String, ByVal nCurrency As Short, ByVal dEffecdate As Date, ByVal nBranch As Short, ByVal nProduct As Short, ByVal nGroupVeh As Short, ByVal nClass As Integer, ByVal nTrademarks As Integer, ByVal nMovement As Integer, ByVal nTypepremiun As Double, ByVal nPremium As Double, ByVal nTypeperson As Integer, ByVal nTypeCalculate As Integer) As String
        Dim lclsErrors As eFunctions.Errors

        On Error GoTo ErrorHandler

        lclsErrors = New eFunctions.Errors

        If (nGroupVeh = 0 Or nGroupVeh = eRemoteDB.Constants.intNull) Then
            Call lclsErrors.ErrorMessage(sCodispl, 91003)
        End If

        If (nClass = 0 Or nClass = eRemoteDB.Constants.intNull) Then
            Call lclsErrors.ErrorMessage(sCodispl, 91002)
        End If

        If (nTrademarks = 0 Or nTrademarks = eRemoteDB.Constants.intNull) Then
            Call lclsErrors.ErrorMessage(sCodispl, 10013)
        End If

        If (nMovement = 0 Or nMovement = eRemoteDB.Constants.intNull) Then
            Call lclsErrors.ErrorMessage(sCodispl, 8518)
        End If

        If sAction = "Add" And IsExist(nCurrency, dEffecdate, nBranch, nProduct, nGroupVeh, nVehType, nMovement) Then
            Call lclsErrors.ErrorMessage(sCodispl, 7148)
        End If

        If (nTypeCalculate = 0 Or nTypeCalculate = eRemoteDB.Constants.intNull) Then
            Call lclsErrors.ErrorMessage(sCodispl, 8509)
        End If

        If (nPremium = 0 Or nPremium = eRemoteDB.Constants.intNull) Then
            Call lclsErrors.ErrorMessage(sCodispl, 3689)
        End If

        InsValMSO8500 = lclsErrors.Confirm
        lclsErrors = Nothing

        Exit Function
ErrorHandler:
        If Err.Number Then
            InsValMSO8500 = InsValMSO8500 & Err.Description
        End If
    End Function

    '**%Objective: Pass of the information introduced towards the layers of rules of business and access of data.
    '**%Parameters:
    '**%    Pending   -
    '%Objetivo: Se encarga de llevar la información introducida hacia las capas de reglas de negocio y acceso de datos.
    '%Parámetros:
    '%    Pendiente -
    Public Function InsPostMSO8500(ByVal pblnHeader As Boolean, ByVal sCodispl As String, ByVal nMainAction As Integer, ByVal sAction As String, ByVal nUsercode As Integer, ByVal nCurrency As Integer, ByVal dEffecdate As Date, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nGroupAuto As Integer, ByVal nClass As Integer, ByVal nTrademarks As Integer, ByVal nModel As Integer, ByVal nSeats As Integer, ByVal nMovement As Integer, ByVal nTypeperson As Integer, ByVal Typepremiun As Integer, ByVal nTypeCalculate As Integer, ByVal nPremium As Double) As Boolean
        On Error GoTo ErrorHandler

        If pblnHeader Then
            InsPostMSO8500 = True
        Else
            If sAction = "Add" Then
                InsPostMSO8500 = AddMSO8500(nUsercode, nCurrency, dEffecdate, nBranch, nProduct, nGroupAuto, nClass, nTrademarks, nModel, nSeats, nMovement, nTypeperson, Typepremiun, nTypeCalculate, nPremium, CStr(1))
            ElseIf sAction = "Update" Then
                InsPostMSO8500 = UpdateMSO8500(nUsercode, nCurrency, dEffecdate, nBranch, nProduct, nGroupAuto, nClass, nTrademarks, nModel, nSeats, nMovement, nTypeperson, Typepremiun, nTypeCalculate, nPremium, CStr(2))
            ElseIf sAction = "Del" Then
                InsPostMSO8500 = DeleteMSO8500(nUsercode, nCurrency, dEffecdate, nBranch, nProduct, nGroupAuto, nClass, nTrademarks, nModel, nSeats, nMovement, nTypeperson, Typepremiun, nTypeCalculate, nPremium, CStr(3))
            End If
        End If

        Exit Function
ErrorHandler:
        If Err.Number Then
            InsPostMSO8500 = False
        End If
    End Function

    '**%Objective: Validation of the data for the page of the headed one.
    '**%Parameters:
    '**%    Pending   -
    '%Objetivo: Validación de los datos para la página del encabezado.
    '%Parámetros:
    '%    Pendiente -
    Public Function InsValMSO009_k(ByVal sCodispl As String, ByVal nMainAction As Integer, ByVal sAction As String, ByVal nCurrency As Short, ByVal dEffecdate As Date, ByVal nBranch As Short, ByVal nProduct As Short) As String
        Dim lclsErrors As eFunctions.Errors

        On Error GoTo ErrorHandler

        lclsErrors = New eFunctions.Errors

        '+ SE VALIDA QUE EL RAMO ESTE LLENO
        If (nBranch = 0 Or nBranch = eRemoteDB.Constants.intNull) Then
            Call lclsErrors.ErrorMessage(sCodispl, 9064)
        End If

        '+ SE VALIDA QUE EL PRODUCTO ESTE LLENO
        If (nProduct = 0 Or nProduct = eRemoteDB.Constants.intNull) Then
            Call lclsErrors.ErrorMessage(sCodispl, 70109)
        End If

        '+ SE VALIDA QUE LA MONEDA ESTE LLENA
        If (nCurrency = 0 Or nCurrency = eRemoteDB.Constants.intNull) Then
            Call lclsErrors.ErrorMessage(sCodispl, 1351)
        End If

        '+ SE VALIDA QUE LA FECHA ESTE LLENA
        If dEffecdate = dtmNull Then
            Call lclsErrors.ErrorMessage(sCodispl, 4003)
        End If

        '+ SE VALIDA QUE SI LAACCIÓN ES DIFERENTE DE CONSULTA LA FECHA DEBE SER
        '+ POSTERIOR A LA FECHA DEL COMPUTADOR
        If nMainAction <> 401 And dEffecdate <= Today And dEffecdate <> dtmNull Then
            Call lclsErrors.ErrorMessage(sCodispl, 10868)
        End If

        '+ Verifica si hay registro que no se pueden modificar
        If nMainAction = 302 And dEffecdate <> dtmNull Then
            If dEffecdate <= Me.Find_LastDate_Soat(nBranch, nProduct, nCurrency, dEffecdate) Then
                Call lclsErrors.ErrorMessage(sCodispl, 91004, , , , , "(" & Me.Find_LastDate_Soat(nBranch, nProduct, nCurrency, dEffecdate) & ")")
            End If
        End If


        InsValMSO009_k = lclsErrors.Confirm

        lclsErrors = Nothing

        Exit Function
ErrorHandler:
        If Err.Number Then
            InsValMSO009_k = InsValMSO009_k & Err.Description
        End If
    End Function

    '**%Objective: Validation of the data for the page details.
    '**%Parameters:
    '**%    Pending   -
    '%Objetivo: Validación de los datos para la página detalle.
    '%Parámetros:
    '%    Pendiente -
    Public Function InsValMSO009(ByVal sCodispl As String, ByVal nMainAction As Integer, ByVal sAction As String, ByVal nCurrency As Short, ByVal dEffecdate As Date, ByVal nBranch As Short, ByVal nProduct As Short, ByVal nGroupVeh As Short, ByVal nClass As Integer, ByVal nTrademarks As Integer, ByVal nMovement As Integer, ByVal nTypepremiun As Double, ByVal nPremium As Double, ByVal nTypeperson As Integer, ByVal nTypeCalculate As Integer) As String
        Dim lclsErrors As eFunctions.Errors

        On Error GoTo ErrorHandler

        lclsErrors = New eFunctions.Errors

        If (nGroupVeh = 0 Or nGroupVeh = eRemoteDB.Constants.intNull) Then
            Call lclsErrors.ErrorMessage(sCodispl, 91003)
        End If

        If (nClass = 0 Or nClass = eRemoteDB.Constants.intNull) Then
            Call lclsErrors.ErrorMessage(sCodispl, 91002)
        End If

        If (nTrademarks = 0 Or nTrademarks = eRemoteDB.Constants.intNull) Then
            Call lclsErrors.ErrorMessage(sCodispl, 10013)
        End If

        If (nMovement = 0 Or nMovement = eRemoteDB.Constants.intNull) Then
            Call lclsErrors.ErrorMessage(sCodispl, 8518)
        End If

        If sAction = "Add" And IsExist(nCurrency, dEffecdate, nBranch, nProduct, nGroupVeh, nVehType, nMovement) Then
            Call lclsErrors.ErrorMessage(sCodispl, 7148)
        End If

        If (nTypeCalculate = 0 Or nTypeCalculate = eRemoteDB.Constants.intNull) Then
            Call lclsErrors.ErrorMessage(sCodispl, 8509)
        End If

        If (nPremium = 0 Or nPremium = eRemoteDB.Constants.intNull) Then
            Call lclsErrors.ErrorMessage(sCodispl, 3689)
        End If

        InsValMSO009 = lclsErrors.Confirm
        lclsErrors = Nothing

        Exit Function
ErrorHandler:
        If Err.Number Then
            InsValMSO009 = InsValMSO009 & Err.Description
        End If
    End Function

    '**%Objective: Pass of the information introduced towards the layers of rules of business and access of data.
    '**%Parameters:
    '**%    Pending   -
    '%Objetivo: Se encarga de llevar la información introducida hacia las capas de reglas de negocio y acceso de datos.
    '%Parámetros:
    '%    Pendiente -
    Public Function InsPostMSO009(ByVal pblnHeader As Boolean, ByVal sCodispl As String, ByVal nMainAction As Integer, ByVal sAction As String, ByVal nUsercode As Integer, ByVal nCurrency As Integer, ByVal dEffecdate As Date, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nGroupAuto As Integer, ByVal nClass As Integer, ByVal nTrademarks As Integer, ByVal nModel As Integer, ByVal nSeats As Integer, ByVal nMovement As Integer, ByVal nTypeperson As Integer, ByVal Typepremiun As Integer, ByVal nTypeCalculate As Integer, ByVal nPremium As Double) As Boolean
        On Error GoTo ErrorHandler

        If pblnHeader Then
            InsPostMSO009 = True
        Else
            If sAction = "Add" Then
                InsPostMSO009 = AddMSO009(nUsercode, nCurrency, dEffecdate, nBranch, nProduct, nGroupAuto, nClass, nTrademarks, nModel, nSeats, nMovement, nTypeperson, Typepremiun, nTypeCalculate, nPremium, CStr(1))
            ElseIf sAction = "Update" Then
                InsPostMSO009 = UpdateMSO009(nUsercode, nCurrency, dEffecdate, nBranch, nProduct, nGroupAuto, nClass, nTrademarks, nModel, nSeats, nMovement, nTypeperson, Typepremiun, nTypeCalculate, nPremium, CStr(2))
            ElseIf sAction = "Del" Then
                InsPostMSO009 = DeleteMSO009(nUsercode, nCurrency, dEffecdate, nBranch, nProduct, nGroupAuto, nClass, nTrademarks, nModel, nSeats, nMovement, nTypeperson, Typepremiun, nTypeCalculate, nPremium, CStr(3))
            End If
        End If

        Exit Function
ErrorHandler:
        If Err.Number Then
            InsPostMSO009 = False
        End If
    End Function

    '**%Objective: Function that makes the search in the table 'Tar_Soat'.
    '**%Parameters:
    '%Objetivo: Función que realiza la busqueda en la tabla 'Tar_Soat'.
    '%Parámetros:
    Public Function Find(ByVal nCurrency As Short, ByVal dEffecdate As Date, ByVal nBranch As Short, ByVal nProduct As Short) As Boolean
        Dim lclsLocateTar_Soat As eRemoteDB.Execute

        On Error GoTo ErrorHandler

        lclsLocateTar_Soat = New eRemoteDB.Execute

        '+ Define all parameters for the stored procedures 'insudb.reatab_quotint'. Generated on 12/13/2004 1:38:43 PM
        With lclsLocateTar_Soat
            .StoredProcedure = "reaTar_SOAT_a"
            .Parameters.Add("ncurrency", nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecDate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If .Run(True) Then
                Do While Not .EOF
                    Me.dNullDate = .FieldToClass("dNullDate")
                    If Me.dNullDate <> dtmNull Then
                        Find = True
                        Exit Do
                    Else
                        Find = False
                    End If
                    .RNext()
                Loop
                .RCloseRec()
            End If
        End With
        lclsLocateTar_Soat = Nothing

        Exit Function
ErrorHandler:
        lclsLocateTar_Soat = Nothing
        If Err.Number Then
            Find = False
        End If
    End Function

    '**%Objective: Function that makes the search in the table 'Tar_Soat'.
    'Find_LastDate: Función que realiza la busqueda en la tabla 'Tar_Soat'
    Public Function Find_LastDate(ByVal nBranch As Short, ByVal nProduct As Short, ByVal nCurrency As Short, ByVal dEffecdate As Date) As Date
        Dim lrecTar_SOAT As eRemoteDB.Execute

        On Error GoTo ErrorHandler

        lrecTar_SOAT = New eRemoteDB.Execute

        With lrecTar_SOAT
            .StoredProcedure = "reatar_soat_lastdate"
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("NCURRENCY", nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dNullDate", dtmNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If .Run(False) Then
                Find_LastDate = .Parameters("dNullDate").Value
            Else
                Find_LastDate = dtmNull
            End If
        End With

        lrecTar_SOAT = Nothing

        Exit Function
ErrorHandler:
        If Err.Number Then
            Find_LastDate = dtmNull
        End If
    End Function
    Public Function Find_LastDate_Soat(ByVal nBranch As Short, ByVal nProduct As Short, ByVal nCurrency As Short, ByVal dEffecdate As Date) As Date
        Dim lrecTar_SOAT As eRemoteDB.Execute

        On Error GoTo ErrorHandler

        lrecTar_SOAT = New eRemoteDB.Execute

        With lrecTar_SOAT
            .StoredProcedure = "REATAR_LASTDATE_SOAT"
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("NCURRENCY", nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dNullDate", dtmNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If .Run(False) Then
                Find_LastDate_Soat = .Parameters("dNullDate").Value
            Else
                Find_LastDate_Soat = dtmNull
            End If
        End With

        lrecTar_SOAT = Nothing

        Exit Function
ErrorHandler:
        If Err.Number Then
            Find_LastDate_Soat = dtmNull
        End If
    End Function
End Class






