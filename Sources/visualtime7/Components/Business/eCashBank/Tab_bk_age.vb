Option Strict Off
Option Explicit On
Public Class Tab_bk_age
	'%-------------------------------------------------------%'
	'% $Workfile:: Tab_bk_age.cls                           $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 1:35p                                $%'
	'% $Revision:: 6                                        $%'
	'%-------------------------------------------------------%'
	
	'**-Properties according to the table in the system on 02/20/2001
	'**-The key fields of the table correspond to: nBank_code and nBk_agency.
	'-Propiedades según la tabla en el sistema al 20/02/2001.
	'-Los campos llave de la tabla corresponden a: nBank_code y nBk_agency.
	
	'   Column_name                   Type     Computed  Length      Prec  Scale Nullable  TrimTrailingBlanks  FixedLenNullInSource
	Public nBank_code As Integer 'int         no       4           10    0     no            (n/a)                (n/a)
	Public nBk_agency As Integer 'int         no       4           10    0     no            (n/a)                (n/a)
	Public sDescript As String 'char        no      30                       yes           no                   yes
	Public sShort_des As String 'char        no      12                       yes           no                   yes
	Public sStatregt As String 'char        no       1                       yes           no                   yes
	Public nUsercode As Integer 'smallint    no       2           5     0     yes           (n/a)                (n/a)
	Public sN_Aba As String 'char        no      20                       yes           no                   yes
	
	'**%Find: This method returns TRUE or FALSE depending if the records exists in the table "Tab_bk_age"
	'%Find: Este metodo retorna VERDADERO o FALSO dependiendo de la existencia o no de registros en la
	'%tabla "Tab_bk_age"
	Public Function Find(ByVal nBank_code As Integer, ByVal nBk_agency As Integer, Optional ByVal lblnFind As Boolean = False) As Boolean
		
		'**- Declares the variable that determines the function result (True/False)
		'- Se declara la variable que determina el resultado de la funcion (True/False)
		
		Static lblnRead As Boolean
		
		'**- Defines the variable lrecreaTab_bk_age
		'- Se define la variable lrecreaTab_bk_age
		
		Dim lrecreaTab_bk_age As eRemoteDB.Execute
		lrecreaTab_bk_age = New eRemoteDB.Execute
		
		On Error GoTo Find_Err
		
		If Me.nBank_code <> nBank_code Or Me.nBk_agency <> nBk_agency Or lblnFind Then
			
			Me.nBank_code = nBank_code
			Me.nBk_agency = nBk_agency
			
			'**+ Parameters definiton for the stored procedure 'insudb.reaTab_bk_age'
			'**+ Data of 02/20/2001 09:30:04 a.m.
			'+ Definición de parámetros para stored procedure 'insudb.reaTab_bk_age'
			'+ Información leída el 20/02/2001 09:30:04 a.m.
			
			With lrecreaTab_bk_age
				.StoredProcedure = "reaTab_bk_age"
				.Parameters.Add("nBank_code", nBank_code, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nBk_agency", nBk_agency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				
				If .Run Then
					Me.nBank_code = .FieldToClass("nBank_code")
					Me.nBk_agency = .FieldToClass("nBk_agency")
					sDescript = .FieldToClass("sDescript")
					sShort_des = .FieldToClass("sShort_des")
					sStatregt = .FieldToClass("sStatregt")
					sN_Aba = .FieldToClass("sN_Aba")
					
					lblnRead = True
					.RCloseRec()
				Else
					lblnRead = False
				End If
			End With
		End If
		Find = lblnRead
		'UPGRADE_NOTE: Object lrecreaTab_bk_age may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaTab_bk_age = Nothing
		
Find_Err: 
		If Err.Number Then
			Find = False
		End If
		On Error GoTo 0
	End Function

    '% Add: permite añadir un registro en la tabla tab_bk_age
    Public Function Add(Optional ByRef nBank_code As Object = eRemoteDB.Constants.intNull, Optional ByRef nBk_agency As Object = eRemoteDB.Constants.intNull, Optional ByRef sDescript As Object = "", Optional ByRef sShort_des As Object = "", Optional ByRef sStatregt As Object = "", Optional ByRef nUsercode As Object = eRemoteDB.Constants.intNull) As Object
        Dim lreccreTab_bk_age As eRemoteDB.Execute

        lreccreTab_bk_age = New eRemoteDB.Execute

        With Me
            If nBank_code <> eRemoteDB.Constants.intNull Then
                .nBank_code = nBank_code
            End If

            If nBk_agency <> eRemoteDB.Constants.intNull Then
                .nBk_agency = nBk_agency
            End If

            If sDescript <> String.Empty Then
                .sDescript = sDescript
            End If

            If sShort_des <> String.Empty Then
                .sShort_des = sShort_des
            End If

            If sStatregt <> String.Empty Then
                .sStatregt = sStatregt
            End If

            If nUsercode <> eRemoteDB.Constants.intNull Then
                .nUsercode = nUsercode
            End If
        End With

        'Definición de parámetros para stored procedure 'insudb.creTab_bk_age'
        'Información leída el 17/09/2001 3:13:45 PM

        With lreccreTab_bk_age
            .StoredProcedure = "creTab_bk_age"

            .Parameters.Add("nBank_code", Me.nBank_code, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBk_agency", Me.nBk_agency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sDescript", Me.sDescript, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 30, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sShort_des", Me.sShort_des, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sStatregt", Me.sStatregt, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUsercode", Me.nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If .Run(False) Then
                Add = True
            End If
        End With
        Return lreccreTab_bk_age
        'UPGRADE_NOTE: Object lreccreTab_bk_age may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lreccreTab_bk_age = Nothing
    End Function

    '% Update: permite actualizar un registro en la tabla tab_bk_age
    Public Function Update(Optional ByVal nBank_code As Object = eRemoteDB.Constants.intNull, Optional ByVal nBk_agency As Object = eRemoteDB.Constants.intNull, Optional ByVal sDescript As Object = "", Optional ByVal sShort_des As Object = "", Optional ByVal sStatregt As Object = "", Optional ByVal nUsercode As Object = eRemoteDB.Constants.intNull) As Object
		Dim lrecupdTab_bk_age As eRemoteDB.Execute
		
		lrecupdTab_bk_age = New eRemoteDB.Execute
		
		With Me
			If nBank_code <> eRemoteDB.Constants.intNull Then
				.nBank_code = nBank_code
			End If
			
			If nBk_agency <> eRemoteDB.Constants.intNull Then
				.nBk_agency = nBk_agency
			End If
			
			If sDescript <> String.Empty Then
				.sDescript = sDescript
			End If
			
			If sShort_des <> String.Empty Then
				.sShort_des = sShort_des
			End If
			
			If sStatregt <> String.Empty Then
				.sStatregt = sStatregt
			End If
			
			If nUsercode <> eRemoteDB.Constants.intNull Then
				.nUsercode = nUsercode
			End If
		End With

        'Definición de parámetros para stored procedure 'insudb.updTab_bk_age'
        'Información leída el 17/09/2001 3:36:47 PM

        With lrecupdTab_bk_age
            .StoredProcedure = "updTab_bk_age"

            .Parameters.Add("nBank_code", Me.nBank_code, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBk_agency", Me.nBk_agency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sDescript", Me.sDescript, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 30, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sShort_des", Me.sShort_des, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sStatregt", Me.sStatregt, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUsercode", Me.nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If .Run(False) Then
                Update = True
            End If
        End With
        Return lrecupdTab_bk_age
        'UPGRADE_NOTE: Object lrecupdTab_bk_age may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecupdTab_bk_age = Nothing
    End Function
	
	'% Delete: permite eliminar un registro en la tabla tab_bk_age
	Public Function Delete(Optional ByVal nBank_code As Object = eRemoteDB.Constants.intNull, Optional ByVal nBk_agency As Object = eRemoteDB.Constants.intNull) As Object
		Dim lrecdelTab_bk_age As eRemoteDB.Execute
		
		lrecdelTab_bk_age = New eRemoteDB.Execute
		
		With Me
			If nBank_code <> eRemoteDB.Constants.intNull Then
				.nBank_code = nBank_code
			End If
			
			If nBk_agency <> eRemoteDB.Constants.intNull Then
				.nBk_agency = nBk_agency
			End If
		End With

        'Definición de parámetros para stored procedure 'insudb.delTab_bk_age'
        'Información leída el 17/09/2001 3:40:30 PM

        With lrecdelTab_bk_age
            .StoredProcedure = "delTab_bk_age"
            .Parameters.Add("nBank_code", Me.nBank_code, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBk_agency", Me.nBk_agency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If .Run(False) Then
                Delete = True
            End If
        End With
        Return lrecdelTab_bk_age
        'UPGRADE_NOTE: Object lrecdelTab_bk_age may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecdelTab_bk_age = Nothing
    End Function
	
	'% valMS003_K: Realiza las validaciones del header de la ventana MS003 "Agencias bancarias"
	Public Function valMS003_K(ByVal nAction As Integer, ByVal nBank_code As Integer) As String
		Dim lobjErrors As eFunctions.Errors
		
		lobjErrors = New eFunctions.Errors
		
		'+ Validación del banco
		
		If nBank_code = 0 Then
			lobjErrors.ErrorMessage("MS003", 7004)
		End If
		
		valMS003_K = lobjErrors.Confirm
		
		'UPGRADE_NOTE: Object lobjErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lobjErrors = Nothing
	End Function
	
	'% valMS003: Realiza las validaciones del folder de la ventana MS003 "Agencias bacarias"
	Public Function valMS003(ByVal sAction As String, ByVal nBank_code As Integer, ByVal nBk_agency As Integer, ByVal sDescript As String, ByVal sShort_des As String, ByVal sStatregt As String) As String
		Dim lobjErrors As eFunctions.Errors
		
		lobjErrors = New eFunctions.Errors
		
		'+ Validación de la agencia bancaria
		
		If nBk_agency = 0 Then
			lobjErrors.ErrorMessage("MS003", 1080)
		Else
			If sAction = "Add" Then
				If Find(nBank_code, nBk_agency) Then
					lobjErrors.ErrorMessage("MS003", 10004)
				End If
			End If
		End If
		
		'+ Validación de la descripción completa
		
		If sDescript = String.Empty Then
			lobjErrors.ErrorMessage("MS003", 10005)
		End If
		
		'+ Validación de la descripción abreviada
		
		If sShort_des = String.Empty Then
			lobjErrors.ErrorMessage("MS003", 10006)
		End If
		
		'+ Validación del estado
		
		If sStatregt = "0" Then
			lobjErrors.ErrorMessage("MS003", 10826)
		End If
		
		valMS003 = lobjErrors.Confirm
		
		'UPGRADE_NOTE: Object lobjErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lobjErrors = Nothing
	End Function
	
	'% insPostMS003. Esta funcion se encarga de realizar la actualización de la ventana MS003
	Public Function insPostMS003(Optional ByVal sAction As String = "", Optional ByVal nBank_code As Integer = eRemoteDB.Constants.intNull, Optional ByVal nBk_agency As Integer = eRemoteDB.Constants.intNull, Optional ByVal sDescript As String = "", Optional ByVal sShort_des As String = "", Optional ByVal sStatregt As String = "", Optional ByVal nUsercode As Integer = eRemoteDB.Constants.intNull) As Boolean
		Select Case sAction
			Case "Add"
				insPostMS003 = Add(nBank_code, nBk_agency, sDescript, sShort_des, sStatregt, nUsercode)
			Case "Update"
				insPostMS003 = Update(nBank_code, nBk_agency, sDescript, sShort_des, sStatregt, nUsercode)
			Case "Del"
				insPostMS003 = Delete(nBank_code, nBk_agency)
		End Select
	End Function
End Class






