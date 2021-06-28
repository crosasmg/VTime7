Option Strict Off
Option Explicit On
Public Class Branches
	'%-------------------------------------------------------%'
	'% $Workfile:: Branches.cls                             $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 12:35p                               $%'
	'% $Revision:: 13                                       $%'
	'%-------------------------------------------------------%'
	
	'+  Column_name            Type                         Computed     Length      Prec  Scale Nullable      TrimTrailingBlanks    FixedLenNullInSource
	Public nBranch As Integer 'smallint      no           2           5     0     no            (n/a)                 (n/a)
	Public sDescript As String 'char          no           30                      yes           yes                   yes
	Public sShort_des As String 'char          no           12                      yes           yes                   yes
	Public sStatregt As String 'char          no           1                       yes           yes                   yes
	Public sTabname As String 'char          no           20                      yes           no                    yes
	Public nUsercode As Integer 'smallint      no           2           5     0     no            (n/a)                 (n/a)
	
	'**- Define the auxiliary variables
	'- Se definen las propiedades auxiliares.
	Public sBrancht As String
	
	Public nStatusInstance As Integer
	
	'**% ADD: This method is in charge of adding new records to the table "table10".  It returns TRUE or FALSE
	'**% depending on whether the stored procedure executed correctly.
	'% ADD: Este método se encarga de agregar nuevos registros a la tabla "table10". Devolviendo verdadero o
	'% falso dependiendo de si el Stored procedure se ejecutó correctamente.
	Public Function Add() As Boolean
		Dim lrecCreTable10 As eRemoteDB.Execute
		
		lrecCreTable10 = New eRemoteDB.Execute
		
		On Error GoTo Add_err
		
		With lrecCreTable10
			.StoredProcedure = "creTable10"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sDescript", sDescript, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 30, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sShort_des", sShort_des, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sTabname", sTabname, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sStatregt", sStatregt, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			Add = .Run(False)
		End With
		
		'UPGRADE_NOTE: Object lrecCreTable10 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecCreTable10 = Nothing
		
Add_err: 
		If Err.Number Then
			Add = False
		End If
		On Error GoTo 0
	End Function
	
	'**% Update: This method is in charge of updating records in the table "Table10".  It returns TRUE or FALSE
	'**% depending on whether the stored procedure executed correctly.
	'% Update: Este método se encarga de actualizar registros en la tabla "Table10". Devolviendo verdadero o
	'% falso dependiendo de si el Stored procedure se ejecutó correctamente.
	Public Function Update() As Boolean
		Dim lrecUpdTable10 As eRemoteDB.Execute
		
		lrecUpdTable10 = New eRemoteDB.Execute
		
		On Error GoTo Update_Err
		
		With lrecUpdTable10
			.StoredProcedure = "updTable10"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sDescript", sDescript, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 30, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			lrecUpdTable10.Parameters.Add("sShort_des", sShort_des, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			lrecUpdTable10.Parameters.Add("sTabname", sTabname, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			lrecUpdTable10.Parameters.Add("sStatregt", sStatregt, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			lrecUpdTable10.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			Update = .Run(False)
		End With
		
		'UPGRADE_NOTE: Object lrecUpdTable10 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecUpdTable10 = Nothing
		
Update_Err: 
		If Err.Number Then
			Update = False
		End If
		On Error GoTo 0
	End Function
	
	'**%Delete: This method is in charge of Deleting records in the table "table10".  It returns TRUE or FALSE
	'**%depending on whether the stored procedure executed correctly.
	'%Delete: Este método se encarga de eliminar registros en la tabla "table10". Devolviendo verdadero o
	'%falso dependiendo de si el Stored procedure se ejecutó correctamente.
	Public Function Delete() As Boolean
		Dim lrecDelTable10 As eRemoteDB.Execute
		
		lrecDelTable10 = New eRemoteDB.Execute
		
		On Error GoTo Delete_Err
		
		'**+ Delete the record from the 'tab_name_b' table.
		'+ Eliminar el registro de la tabla 'tab_name_b'
		With lrecDelTable10
			.StoredProcedure = "delTab_name_b"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			Delete = .Run(False)
			
			If Delete Then
				
				'**+ Delete the record of the 'table10' table
				'+ Eliminar el registro de la tabla 'table10'
				.StoredProcedure = "delTable10"
				.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				
				Delete = .Run(False)
			End If
		End With
		
		'UPGRADE_NOTE: Object lrecDelTable10 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecDelTable10 = Nothing
		
Delete_Err: 
		If Err.Number Then
			Delete = False
		End If
		
		On Error GoTo 0
	End Function
	
	'**% Find: Routine to update the rows that have been modified in the commercial line of
	'**% business table "table10"
	'% Find: Rutina para actualizar las filas que han sido modificadas en la tabla de ramos
	'% comerciales "table10"
	Public Function Find(ByVal nBranch As Integer) As Boolean
		Dim lrecTable10 As New eRemoteDB.Execute
		
		On Error GoTo Find_Err
		
		With lrecTable10
			.StoredProcedure = "reaTable10_v"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run Then
				Find = True
				.RCloseRec()
			Else
				Find = False
			End If
		End With
		
		'UPGRADE_NOTE: Object lrecTable10 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecTable10 = Nothing
		
Find_Err: 
		If Err.Number Then
			Find = False
		End If
		On Error GoTo 0
	End Function

    '**% insPostDP001: This method updates the database (as described in the functional specifications)
    '**% for the page "DP001"
    '% insPostDP001: Este metodo se encarga de realizar el impacto en la base de datos (descrito en las
    '% especificaciones funcionales)de la ventana "DP001"
    Public Function insPostDP001(ByVal MainAction As String, ByVal nBranch As Integer, ByVal sDescript As String, ByVal sShort_des As String, ByVal sTabname As String, ByVal sStatregt As String, ByVal nUsercode As Integer, Optional ByVal nInsur_Area As Integer = 0) As Boolean
        On Error GoTo insPostDP001_Err
        Dim lsclValues_cache As eFunctions.Values
        lsclValues_cache = New eFunctions.Values

        insPostDP001 = True

        With Me
            .nBranch = nBranch
            .sDescript = sDescript
            .sShort_des = sShort_des
            .sTabname = sTabname
            .sStatregt = sStatregt
            .nUsercode = nUsercode
        End With

        Select Case MainAction

            '**+If the selected option is Add
            '+Si la opción seleccionada es Registrar.
            Case "Add"
                insPostDP001 = Add()

                '**+If the selected option is Modify
                '+Si la opción seleccionada es Modificar
            Case "Update"
                insPostDP001 = Update()

                '**+If the selected option is Delete
                '+Si la opción seleccionada es Eliminar
            Case "Delete"
                insPostDP001 = Delete()
        End Select

        'Se llama a método para borrar caché luego de crear nuevo ramo comercial
        Call lsclValues_cache.DelCache(2, "Table10",  , nInsur_Area)

insPostDP001_Err:
        If Err.Number Then
            insPostDP001 = False
        End If
        On Error GoTo 0
    End Function

    '**% insValDP001_K: This method validates the header section of the page "DP001_K" as described in the
    '**% functional specifications
    '% InsValDP001_K: Este metodo se encarga de realizar las validaciones del encabezado (Header)
    '% descritas en el funcional de la ventana "DP001_K"
    Public Function insValDP001_k(ByVal sCodispl As String, ByVal sAction As String, Optional ByVal nBranch As Integer = 0, Optional ByVal sDescript As String = "", Optional ByVal sShort_des As String = "", Optional ByVal sTabname As String = "", Optional ByVal sStatregt As String = "") As String
		Dim lclsBranches As eProduct.Branches
		Dim lobjErrors As eFunctions.Errors
		
		On Error GoTo insValDP001_k_Err
		lobjErrors = New eFunctions.Errors
		'**+ Validate the field "Code"
		'+ Se valida el campo "Código"
		If sAction = "Add" Then
			If nBranch = eRemoteDB.Constants.intNull Then
				Call lobjErrors.ErrorMessage(sCodispl, 1022)
			Else
				If Find(nBranch) Then
					Call lobjErrors.ErrorMessage(sCodispl, 11021)
				End If
			End If
		End If
		
		'**+ Validate the description
		'+ Valida la descripcion.
		'UPGRADE_WARNING: IsEmpty was upgraded to IsNothing and has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
		'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
		If IsDbNull(sDescript) Or IsNothing(sDescript) Or Trim(sDescript) = String.Empty Then
			Call lobjErrors.ErrorMessage(sCodispl, 10010)
		End If
		
		'**+ Validate the short description.
		'+ Valida la descripcion corta.
		'UPGRADE_WARNING: IsEmpty was upgraded to IsNothing and has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
		'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
		If IsDbNull(sShort_des) Or IsNothing(sShort_des) Or Trim(sShort_des) = String.Empty Then
			Call lobjErrors.ErrorMessage(sCodispl, 10011)
		End If
		
		'**+ Validate the Particular Data table.
		'+ Valida la tabla de Datos particulares.
		'UPGRADE_WARNING: IsEmpty was upgraded to IsNothing and has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
		'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
		If IsDbNull(sTabname) Or IsNothing(sTabname) Or Trim(sTabname) = String.Empty Then
			Call lobjErrors.ErrorMessage(sCodispl, 10158)
		Else
			lclsBranches = New eProduct.Branches
			If Not lclsBranches.valExistTable(sTabname) Then
				Call lobjErrors.ErrorMessage(sCodispl, 3341)
			End If
		End If
		
		'**+ Validate the record status.
		'+ Valida el estado del registro.
		'UPGRADE_WARNING: IsEmpty was upgraded to IsNothing and has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
		'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
		If IsDbNull(sStatregt) Or IsNothing(sStatregt) Or Trim(sStatregt) = String.Empty Or Trim(sStatregt) = "0" Then
			Call lobjErrors.ErrorMessage(sCodispl, 1016)
		End If
		
		insValDP001_k = lobjErrors.Confirm
		
insValDP001_k_Err: 
		If Err.Number Then
			insValDP001_k = insValDP001_k & Err.Description
		End If
		'UPGRADE_NOTE: Object lobjErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lobjErrors = Nothing
		'UPGRADE_NOTE: Object lclsBranches may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsBranches = Nothing
		On Error GoTo 0
	End Function
	
	'**% valExistTable: This function validates the existance of the Particular Data table of the line
	'**% of business in the data base.
	'% valExistTable: Esta función valida la existencia de la tabla de Datos particulares del ramo
	'% en la base de datos.
	Function valExistTable(ByRef lstrTabname As String) As Boolean
		Dim lrecTable As New eRemoteDB.Execute
		
		On Error GoTo valExistTable_Err
		
		With lrecTable
			.StoredProcedure = "reaObjects"
			.Parameters.Add("sTabname", lstrTabname, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run Then
				.RCloseRec()
				valExistTable = True
			Else
				valExistTable = False
			End If
		End With
		
valExistTable_Err: 
		If Err.Number Then
			valExistTable = False
		End If
		On Error GoTo 0
	End Function
	
	'**% valExistAssoPolicy: Validate that the line of business has no associated policies.
	'% valExistAssoPolicy: Se valida que el ramo no tenga pólizas asociadas.
	Function valExistAssoPolicy(ByRef nBranch As Integer) As Boolean
		Dim lrecPolicy As New eRemoteDB.Execute
		
		On Error GoTo valExistAssoPolicy_Err
		
		With lrecPolicy
			.SQL = "SELECT dCompdate FROM " & .Owner & ".Policy WHERE nBranch= " & nBranch
			
			If .Run Then
				.RCloseRec()
				valExistAssoPolicy = True
			Else
				valExistAssoPolicy = False
			End If
		End With
		
valExistAssoPolicy_Err: 
		If Err.Number Then
			valExistAssoPolicy = False
		End If
		On Error GoTo 0
	End Function
	
	'**% valExistProduct: This method validates that the line of business in process
	'**% has no associated products to execute the delete of the same.
	'% valExistProduct: Esta funcion se encarga de validar que el ramo en tratamiento no tenga ningun
	'% producto asociado para realizar la eliminacion del mismo.
	Public Function valExistProduc(ByRef nBranch As Integer) As Boolean
		Dim lrecProdmaster As eRemoteDB.Execute
		
		lrecProdmaster = New eRemoteDB.Execute
		
		On Error GoTo valExistProduc_err
		
		valExistProduc = False
		
		With lrecProdmaster
			.StoredProcedure = "reaProdMasterA"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			valExistProduc = .Run
			.RCloseRec()
		End With
		
		'UPGRADE_NOTE: Object lrecProdmaster may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecProdmaster = Nothing
		
valExistProduc_err: 
		If Err.Number Then
			valExistProduc = False
		End If
		On Error GoTo 0
	End Function
	
	'% insVerifyBranch: Rutina para verificar la existencia del registro en la tabla Maestro de productos
	Public Function insVerifyBranch(ByVal nBranch As Integer, ByVal sBrancht As String) As Boolean
		Dim lrecreaProdmasterB As eRemoteDB.Execute
		
		lrecreaProdmasterB = New eRemoteDB.Execute
		
		insVerifyBranch = False
		
		On Error GoTo insVerifyBranch_Err
		
		'+ Definición de parámetros para stored procedure 'insudb.reaProdmasterB'
		'+ Información leída el 05/11/2001 04:40:42 p.m.
		
		With lrecreaProdmasterB
			.StoredProcedure = "reaProdmasterBPKG.reaProdmasterB"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sBrancht", sBrancht, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 10, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				Me.sBrancht = .FieldToClass("sBrancht")
				insVerifyBranch = True
			End If
		End With
		'UPGRADE_NOTE: Object lrecreaProdmasterB may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaProdmasterB = Nothing
		
insVerifyBranch_Err: 
		If Err.Number Then
			insVerifyBranch = False
		End If
	End Function
	
	'%valExistsTab_name_b: Valida si el recibo ingresado esta en convenio de cobranzas.
	Public Function valExistsTab_name_b(ByVal nBranch As Integer, ByVal sTabname As String) As Boolean
		Dim lrecPremium As eRemoteDB.Execute
		Dim llngExists As Integer
		
		On Error GoTo valExistsTab_name_b_Err
		
		lrecPremium = New eRemoteDB.Execute
		
		With lrecPremium
			.StoredProcedure = "valExistsTab_name_b"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sTabName", sTabname, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 15, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nExists", llngExists, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run(False) Then
				If .Parameters.Item("nExists").Value = 1 Then
					valExistsTab_name_b = True
				End If
			End If
		End With
		
valExistsTab_name_b_Err: 
		If Err.Number Then
			valExistsTab_name_b = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecPremium may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecPremium = Nothing
	End Function
End Class






