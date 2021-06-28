Option Strict Off
Option Explicit On
Public Class TabGen
	
	'**- Define the properties of the class.
	'- Se definen las propiedades de la clase
	'- Column_name             Type
	'- ------------------      ---------------------
	Public nStatusInstance As Integer
	Public key As String
	Public sDescript As String
	Public sShort_des As String
	Public sStatregt As String
	Public sValorAdic As String
	Public sColumna As String
	Public sColumns As String
	Public nUsercode As Integer
	Public dCompdate As Date
	Public sTableNew As String
    Public nFieldLength As Long
    Public nFieldLengthDesc As Long
    Public nFieldLengthShortDesc As Long
	
	'**- Auxiliary Properties
	'- Propiedades auxiliares
	
	Public sCaption As String
	Public nNumErr As Integer

    '%ADD: Este método se encarga de leer si hay alguna columna adicional a las normales de Table
    ' para la tabla "TableXXX" y a que tableXXX esta asociada esta nueva columna.
    Public Function ReaTable_NameXXX(ByVal sTable As String, Optional sOrigin As String = "1") As Boolean
        Dim lrecReaTableXXX As eRemoteDB.Execute
        Dim sKeyName As String
        Dim sTableOwner As String

        lrecReaTableXXX = New eRemoteDB.Execute

        On Error GoTo ReaTable_err

        sKeyName = insSearchKeyValues(sTable)
        sTableOwner = IIf(sOrigin = "1", "INSUDB", "INSUDBGEN")

        With lrecReaTableXXX
            .StoredProcedure = "ReaTable_Name_XXX"
            '.Parameters.Add("sOwner", lrecReaTableXXX.Owner, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 15, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sOwner", sTableOwner, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 15, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            '.Parameters.Add("sOwner", "insudb", eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 15, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sTable", sTable, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 15, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sClave", sKeyName, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 15, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            ReaTable_NameXXX = .Run
            If ReaTable_NameXXX Then
                sValorAdic = .FieldToClass("sTabla")
                sColumna = .FieldToClass("sColumna")
                .RCloseRec()
                ReaTable_NameXXX = True
            End If

        End With

        'UPGRADE_NOTE: Object lrecReaTableXXX may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecReaTableXXX = Nothing

ReaTable_err:
        If Err.Number Then
            ReaTable_NameXXX = False
        End If
        On Error GoTo 0
    End Function


    '**%ADD: This method is in charge of adding new records to the table "TableXXX".  It returns TRUE or FALSE
    '**%depending on whether the stored procedure executed correctly.
    '%ADD: Este método se encarga de agregar nuevos registros a la tabla "TableXXX". Devolviendo verdadero o
    '%falso dependiendo de si el Stored procedure se ejecutó correctamente.
    Public Function Add(ByVal sTable As String, ByVal sKeyDescript As String, Optional ByVal sColumns As String = "", Optional sOrigin As String = "1") As Boolean
        Dim lrecinsTableXXX As eRemoteDB.Execute
        Dim sTableOwner As String

        lrecinsTableXXX = New eRemoteDB.Execute
        sTableOwner = IIf(sOrigin = "1", "INSUDB", "INSUDBGEN")

        On Error GoTo Add_err

        With lrecinsTableXXX
            .StoredProcedure = "insTableXXX"

            .Parameters.Add("nAction", 1, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sOwner", sTableOwner, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 15, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUserCode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sTable", sTable, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 15, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sClave", sKeyDescript, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 15, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sDescript", sDescript, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 250, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sShort_des", sShort_des, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 30, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sStatregt", sStatregt, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sClaveValue", key, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 10, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sValorAdic", sValorAdic, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 10, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sColumns", sColumns, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 10, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            Add = .Run(False)
        End With

        'UPGRADE_NOTE: Object lrecinsTableXXX may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecinsTableXXX = Nothing

Add_err:
        If Err.Number Then
            Add = False
        End If
        On Error GoTo 0
    End Function

    '**%Update: This method is in charge of updating records in the table "TableXXX".  It returns TRUE or FALSE
    '**%depending on whether the stored procedure executed correctly.
    '%Update: Este método se encarga de actualizar registros en la tabla "TableXXX". Devolviendo verdadero o
    '%falso dependiendo de si el Stored procedure se ejecutó correctamente.
    Public Function Update(ByVal sTable As String, ByVal sKeyDescript As String, Optional ByVal sColumns As String = "", Optional sOrigin As String = "1") As Boolean
        Dim lrecinsTableXXX As eRemoteDB.Execute
        Dim sTableOwner As String

        lrecinsTableXXX = New eRemoteDB.Execute
        sTableOwner = IIf(sOrigin = "1", "INSUDB", "INSUDBGEN")

        On Error GoTo Update_err

        With lrecinsTableXXX
            .StoredProcedure = "insTableXXX"

            .Parameters.Add("nAction", 2, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sOwner", sTableOwner, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 15, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUserCode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sTable", sTable, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 15, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sClave", sKeyDescript, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 15, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sDescript", sDescript, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 250, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sShort_des", sShort_des, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 30, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sStatregt", sStatregt, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sClaveValue", key, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 10, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sValorAdic", sValorAdic, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 10, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sColumns", sColumns, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 10, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            Update = .Run(False)
        End With

        'UPGRADE_NOTE: Object lrecinsTableXXX may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecinsTableXXX = Nothing

Update_err:
        If Err.Number Then
            Update = False
        End If
        On Error GoTo 0
    End Function

    '**%Delete: This method is in charge of Deleteing records in the table "TableXXX".  It returns TRUE or FALSE
    '**%depending on whether the stored procedure executed correctly.
    '%Delete: Este método se encarga de eliminar registros en la tabla "TableXXX". Devolviendo verdadero o
    '%falso dependiendo de si el Stored procedure se ejecutó correctamente.
    Public Function Delete(ByVal sTable As String, ByVal sKeyDescript As String, Optional ByVal sColumns As Object = Nothing, Optional sOrigin As String = "1") As Boolean
        Dim lrecinsTableXXX As eRemoteDB.Execute
        Dim sTableOwner As String

        lrecinsTableXXX = New eRemoteDB.Execute
        sTableOwner = IIf(sOrigin = "1", "INSUDB", "INSUDBGEN")

        On Error GoTo Delete_err

        With lrecinsTableXXX
            .StoredProcedure = "insTableXXX"
            .Parameters.Add("nAction", 3, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sOwner", sTableOwner, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 15, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUserCode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sTable", sTable, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 15, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sClave", sKeyDescript, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 15, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sDescript", sDescript, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 250, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sShort_des", sShort_des, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 30, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sStatregt", sStatregt, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sClaveValue", key, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 10, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sValorAdic", sValorAdic, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 10, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sColumns", sColumns, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 10, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            Delete = .Run(False)
        End With

Delete_err:
        If Err.Number Then
            Delete = False
        End If
        'UPGRADE_NOTE: Object lrecinsTableXXX may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecinsTableXXX = Nothing
        On Error GoTo 0
    End Function

    '**%Find: This method returns TRUE or FALSE depending if the records exists in the table "TableXXX"
    '%Find: Este metodo retorna VERDADERO o FALSO dependiendo de la existencia o no de registros en la
    '%tabla "TableXXX"
    Public Function Find(ByVal sTable As String, ByVal key As String) As Boolean
		Dim lrecReaTableXXX As eRemoteDB.Execute
		Dim sKeyName As String
		
		lrecReaTableXXX = New eRemoteDB.Execute
		
		On Error GoTo Find_err
		
		sKeyName = insSearchKeyValues(sTable)
		
		Find = False
		
		With lrecReaTableXXX
			'        .SQL = "SELECT * FROM " & .Owner & "." & sTable & " WHERE "
			.SQL = "SELECT * FROM " & sTable & " WHERE "
			
			If Trim(sKeyName) <> String.Empty Then
				If Mid(sKeyName, 1, 1) = "n" Then
					.SQL = .SQL & sKeyName & " = " & key
				Else
					.SQL = .SQL & sKeyName & " = '" & key & "'"
				End If
			Else
				.SQL = .SQL & "1 = 1"
			End If
			
			If .Run Then
				key = .FieldToClass(sKeyName)
				sDescript = .FieldToClass("sDescript")
				sShort_des = .FieldToClass("sShort_des")
				sStatregt = .FieldToClass("sStatregt")
				nUsercode = .FieldToClass("nUsercode")
				dCompdate = .FieldToClass("dCompDate")
				
				Find = True
				
				.RCloseRec()
			End If
		End With
		
		'UPGRADE_NOTE: Object lrecReaTableXXX may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecReaTableXXX = Nothing
		
Find_err: 
		If Err.Number Then
			Find = False
		End If
		On Error GoTo 0
	End Function
	
	'**% reaWindow: This routine is in charge to read data in the windows table.
	'%reaWindow: Esta rutina se encarga de leer los datos de la tabla windows
    Public Function reaWindow(ByVal sCodispl As String) As String
        Dim lrecWindows As eRemoteDB.Execute

        lrecWindows = New eRemoteDB.Execute

        On Error GoTo reaWindow_err

        nNumErr = 0

        reaWindow = String.Empty

        With lrecWindows
            .StoredProcedure = "reaWindows"

            .Parameters.Add("sCodispl", sCodispl, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 8, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            If .Run Then
                sCaption = Trim(.FieldToClass("sDescript")) & " - (" & Trim(.FieldToClass("sPseudo")) & ")"

                'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
                If IsDBNull(.FieldToClass("nG_identi")) Then
                    nNumErr = 90000
                Else
                    reaWindow = "table" & Trim(.FieldToClass("nG_identi"))
                End If

                .RCloseRec()
            Else
                nNumErr = 1073
            End If
        End With

        'UPGRADE_NOTE: Object lrecWindows may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecWindows = Nothing
reaWindow_err:
        If Err.Number Then
            reaWindow = CStr(False)
        End If
        On Error GoTo 0
    End Function
	
	'**% insSearchKeyValues: Makes the search of key field in a generic table.
	'%insSearchKeyValues: Realiza la busqueda del campo clave en una tabla generica.
	Public Function insSearchKeyValues(ByRef sTablename As String) As String

        Dim lrecRecordset As eRemoteDB.Execute

        lrecRecordset = New eRemoteDB.Execute
        insSearchKeyValues = lrecRecordset.GetTablePrimaryKeyInfo(sTablename, nFieldLength, nFieldLengthDesc, nFieldLengthShortDesc)
        lrecRecordset = Nothing

insSearchKeyValues_err: 
		If Err.Number Then
			insSearchKeyValues = CStr(False)
		End If
		On Error GoTo 0
	End Function

    '**%insPostMA1000. This method updates the database (as described in the functional specifications)
    '**%for the page "MA1000"
    '%insPostMA1000: Este metodo se encarga de realizar el impacto en la base de datos (descrito en las
    '%especificaciones funcionales)de la ventana "MA1000"
    Public Function insPostMA1000(ByVal sCodispl As String, ByVal sMainAction As String, ByVal sKey As String, ByVal sDescript As String, ByVal sShort_des As String, ByVal sStatregt As String, ByVal nUsercode As Integer, Optional ByVal sKeyColumns As String = "", Optional sOrigin As String = "1", Optional ByVal nInsur_Area As Integer = 0) As Boolean
        Dim sKeyDescript As String
        Dim sTable As String
        Dim sColumns As String = ""
        Dim lsclValues_cache As eFunctions.Values
        Dim lsclTabGen As TabGen

        On Error GoTo insPostMA1000_err

        insPostMA1000 = True

        sTable = reaWindow(sCodispl)
        sKeyDescript = insSearchKeyValues(sTable)

        lsclTabGen = New TabGen
        If lsclTabGen.ReaTable_NameXXX(sTable, sOrigin) Then
            sColumns = lsclTabGen.sColumna
        End If


        With Me
            .key = sKey
            .sDescript = sDescript
            .sShort_des = sShort_des
            .sStatregt = sStatregt
            .nUsercode = nUsercode
            .sValorAdic = sKeyColumns
        End With

        Select Case sMainAction

            '**+ If the selected option is Register.
            '+Si la opción seleccionada es Registrar

            Case "Add"
                insPostMA1000 = Add(sTable, sKeyDescript, sColumns, sOrigin)

                '**+ If the selected option is Modify
                '+Si la opción seleccionada es Modificar

            Case "Update"
                insPostMA1000 = Update(sTable, sKeyDescript, sColumns, sOrigin)

                '**+ If the selected option is Delete.
                '+Si la opción seleccionada es Eliminar

            Case "Delete"
                insPostMA1000 = Delete(sTable, sKeyDescript, sColumns, sOrigin)
        End Select

        If insPostMA1000 Then
            lsclValues_cache = New eFunctions.Values
            Call lsclValues_cache.DelCache(2, sTable, , nInsur_Area)
        End If

insPostMA1000_err:
        If Err.Number Then
            insPostMA1000 = False
        End If
        'UPGRADE_NOTE: Object lsclValues_cache may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lsclValues_cache = Nothing
        On Error GoTo 0
    End Function

    '**%insValMA1000_k: This method validates the header section of the page "MA1000_k" as described in the
    '**%functional specifications
    '%InsValXXX_K: Este metodo se encarga de realizar las validaciones del encabezado (Header)
    '%descritas en el funcional de la ventana "MA1000_k"
    Public Function insValMA1000_k(ByVal sCodispl As String, ByVal sAction As String, Optional ByVal key As String = "", Optional ByVal sDescript As String = "", Optional ByVal sShort_des As String = "", Optional ByVal sStatregt As String = "") As String
		Dim lobjValues As eFunctions.Values
		Dim lobjErrors As eFunctions.Errors
		Dim sTable As String
		
		'**- Define the variables that will receipt the parameters to make the numeric calculation.
		'- Se definen las variables que recibirán a los parámetros para poder realizar cálculos numéricos
		
		Dim llngKey As Double
		
		lobjValues = New eFunctions.Values
		lobjErrors = New eFunctions.Errors
		
		insValMA1000_k = String.Empty
		
		On Error GoTo insValMA1000_k_Err
		
		llngKey = lobjValues.StringToType(key, eFunctions.Values.eTypeData.etdDouble)
		
		'**+ Validate the field Code.
		'+ Se valida el campo Código
		
		If sAction = "Add" Then
			If llngKey = CDbl("0") Or CDbl(llngKey) = intNull Then
				Call lobjErrors.ErrorMessage(sCodispl, 1012,  , eFunctions.Errors.TextAlign.RigthAling, "(Código)")
			Else
				sTable = reaWindow(sCodispl)
				
				If Find(sTable, CStr(llngKey)) Then
					Call lobjErrors.ErrorMessage(sCodispl, 12101)
				End If
			End If
		End If
		
		'**+ Validate the description
		'+ Valida la descripcion
		
		'UPGRADE_WARNING: IsEmpty was upgraded to IsNothing and has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
		'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
		If IsDbNull(sDescript) Or IsNothing(sDescript) Or Trim(sDescript) = String.Empty Then
			Call lobjErrors.ErrorMessage(sCodispl, 10005)
		End If
		
		'**+ Validates the short description
		'+ Valida la descripcion corta
		
		'UPGRADE_WARNING: IsEmpty was upgraded to IsNothing and has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
		'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
		If IsDbNull(sShort_des) Or IsNothing(sShort_des) Or Trim(sShort_des) = String.Empty Then
			Call lobjErrors.ErrorMessage(sCodispl, 10006)
		End If
		
		'**+ Validates the registration status.
		'+ Valida el estado del registro
		
		'UPGRADE_WARNING: IsEmpty was upgraded to IsNothing and has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
		'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
		If IsDbNull(sStatregt) Or IsNothing(sStatregt) Or Trim(sStatregt) = String.Empty Or Trim(sStatregt) = "0" Then
			Call lobjErrors.ErrorMessage(sCodispl, 9089)
		End If
		
		insValMA1000_k = lobjErrors.Confirm
		
		'UPGRADE_NOTE: Object lobjErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lobjErrors = Nothing
		'UPGRADE_NOTE: Object lobjValues may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lobjValues = Nothing
		
insValMA1000_k_Err: 
		If Err.Number Then
			insValMA1000_k = insValMA1000_k & Err.Description
		End If
		On Error GoTo 0
	End Function
	
	'%InsValTables: Este metodo se encarga de realizar las validaciones generales para
	' las tablas de mantenimiento
	Public Function InsValTables(ByVal sTable As String, ByVal nCode As Integer) As Boolean
		
		Dim lrecinsFindnspeciality As eRemoteDB.Execute
		Dim lrecTables As eRemoteDB.Execute
		
		Select Case sTable
			
			'+Si la tabla es TABLE16 se busca por persona natural
			Case "table16"
				On Error GoTo InsValTables_Err
				
				lrecinsFindnspeciality = New eRemoteDB.Execute
				
				'+ Definición de store procedure insFindnspeciality al 04-16-2002 15:56:08
				With lrecinsFindnspeciality
					.StoredProcedure = "insFindnspeciality"
					.Parameters.Add("nSpeciality", nCode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Parameters.Add("nPerson_typ", 1, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					
					If .Run(True) Then
						InsValTables = True
					Else
						InsValTables = False
					End If
				End With
				
				'+Si la tabla es TABLE215 se busca estado de ordenes de servicios profesionales
			Case "table215"
				On Error GoTo InsValTables_Err
				lrecTables = New eRemoteDB.Execute
				With lrecTables
					.StoredProcedure = "insFindnStatusprof_ord"
					.Parameters.Add("nStatus_ord", nCode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Parameters.Add("nExists", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Run(False)
					InsValTables = (.Parameters("nExists").Value = 1)
				End With
				
				'+Si la tabla es TABLE417 se busca por persona jurídica
			Case "table417"
				On Error GoTo InsValTables_Err
				
				lrecinsFindnspeciality = New eRemoteDB.Execute
				
				'+ Definición de store procedure insFindnspeciality al 04-16-2002 15:56:08
				With lrecinsFindnspeciality
					.StoredProcedure = "insFindnspeciality"
					.Parameters.Add("nSpeciality", nCode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Parameters.Add("nPerson_typ", 2, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					
					If .Run(True) Then
						InsValTables = True
					Else
						InsValTables = False
					End If
				End With
				
		End Select
		
InsValTables_Err: 
		If Err.Number Then
			InsValTables = False
		End If
		'UPGRADE_NOTE: Object lrecinsFindnspeciality may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecinsFindnspeciality = Nothing
		On Error GoTo 0
	End Function
	
	'%DelValTables: Este método se encarga de validar que el código a eliminar de la table
	'%              no tenga dependencias con registros de otras tables
	Public Function DelValTables(ByVal sTable As String, ByVal nCode As Integer) As Boolean
		Dim nCont As Integer
		
		Dim lrecValTablesXXX As eRemoteDB.Execute
		
		On Error GoTo DelValTables_Err
		
		lrecValTablesXXX = New eRemoteDB.Execute
		
		With lrecValTablesXXX
			.StoredProcedure = "ValTableXXX"
			.Parameters.Add("sOwner", lrecValTablesXXX.Owner, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 15, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sTable", sTable, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 15, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sClaveValue", nCode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 10, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCont", nCont, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			DelValTables = .Run(False)
			
			nCont = .Parameters("nCont").Value
			
			If nCont > 0 Then
				DelValTables = True
			Else
				DelValTables = False
			End If
		End With
		
DelValTables_Err: 
		If Err.Number Then
			DelValTables = False
		End If
		'UPGRADE_NOTE: Object lrecValTablesXXX may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecValTablesXXX = Nothing
		On Error GoTo 0
	End Function

    '% FindNextValue: Rescata el siguente valor de la tabla
    Public Function FindNextValue(ByVal sTable As String, ByVal sKey As String, Optional ByVal lblnFind As Boolean = False, Optional sOrigin As String = "1") As String
        Dim lclsTabGen As TabGen = New TabGen
        Dim lclsTabGens As TabGens
        Dim lintIndex As Integer
        Dim lblnOut As Boolean

        On Error GoTo FindNextCompany_err

        lclsTabGens = New TabGens
        If lclsTabGens.Find(sTable, sOrigin) Then
            For Each lclsTabGen In lclsTabGens
                If lblnOut Then
                    Exit For
                End If
                lintIndex = lintIndex + 1
                If lclsTabGen.key = sKey Then
                    lblnOut = True
                End If
            Next lclsTabGen
            If lclsTabGens.Count = lintIndex Then
                lclsTabGen = lclsTabGens(1)
            End If
            FindNextValue = lclsTabGen.key
        End If

FindNextCompany_err:
        If Err.Number Then
            FindNextValue = sKey
        End If
        On Error GoTo 0
        'UPGRADE_NOTE: Object lclsTabGen may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsTabGen = Nothing
        'UPGRADE_NOTE: Object lclsTabGens may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsTabGens = Nothing
    End Function
End Class






