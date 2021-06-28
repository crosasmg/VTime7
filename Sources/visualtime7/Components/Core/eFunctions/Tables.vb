Option Strict Off
Option Explicit On
Public Class Tables
	'%-------------------------------------------------------%'
	'% $Workfile:: Tables.cls                               $%'
	'% $Author:: Fmendoza                                   $%'
	'% $Date:: 23-05-06 19:28                               $%'
	'% $Revision:: 2                                        $%'
	'%-------------------------------------------------------%'
	
	'-Nombre de campo desripcion
	Public Descript As String
	
	'-Condicion de busqueda
	Public Condition As String
	
	Public Enum sTypeServer
        sSQLServer65 = 1
        sSQLServer7 = 2
        sOracle = 3
        sInformix = 4
        sDB2 = 5
	End Enum
	
	Public Enum eRmtDataAttrib
		rdbParamSigned = 16
		rdbParamNullable = 64
		rdbParamLong = 128
	End Enum
	
	'-Objetos para cosulta en base de datos remota y local
	Private recTableRemote As eRemoteDB.Execute
	
	'-Parametros pasados a la consulta
	Private mParameters As Parameters
	
	'-Campos de busqueda y descripcion
	Private mstrKeyField As String
	Private mstrDesField As String
	
	'-Orden de busqueda
	Private meOrder As Values.ecbeOrder
	
	
	'-Se define la variable que indica si la transacción se ejecuta en modo consulta
	Public ActionQuery As Boolean
	
	'-Variable que guarda el número de sesión
	Public sSessionID As String
	
	'-Código del usuario
	Public nUsercode As Integer
	
	'**% Class_Initialize: controls the access to the class.
	'% Class_Initialize: se controla el acceso a la clase
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		TypeOrder = Values.ecbeOrder.Descript
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
	
	'%Class_Terminate: termino de uso de objeto
	'UPGRADE_NOTE: Class_Terminate was upgraded to Class_Terminate_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Terminate_Renamed()
		'UPGRADE_NOTE: Object mParameters may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		mParameters = Nothing
		'UPGRADE_NOTE: Object recTableRemote may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		recTableRemote = Nothing
	End Sub
	Protected Overrides Sub Finalize()
		Class_Terminate_Renamed()
		MyBase.Finalize()
	End Sub
	
	
	'%Parameters:Asigna parametros a usar en consultas
	
	'%Parameters:Recupera parametros de consultas
	Public Property Parameters() As Parameters
		Get
			If mParameters Is Nothing Then
				mParameters = New Parameters
			End If
			
			Parameters = mParameters
		End Get
		Set(ByVal Value As Parameters)
			mParameters = Value
		End Set
	End Property
	
	'%DescriptField:Retorna el nombre de campo de descripcion usado en la consulta
	Public ReadOnly Property DescriptField() As String
		Get
			If mstrDesField = String.Empty Then
				DescriptField = "sDescript"
			Else
				DescriptField = mstrDesField
			End If
		End Get
	End Property
	
	'*** TypeOrder: indicates the order in which the combo box values are going to be charged.
	'* TypeOrder: indica el orden en que se van a cargar los valores de los combos
	
	'**% TypeOrder: indicates the order in which the combo box values are going to be charged.
	'% TypeOrder: indica el orden en que se van a cargar los valores de los combos
	Public Property TypeOrder() As Values.ecbeOrder
		Get
			TypeOrder = meOrder
		End Get
		Set(ByVal Value As Values.ecbeOrder)
			meOrder = Value
		End Set
	End Property
	
	'%KeyField:Retorna el nombre de campo de clave usado en la consulta
	Public ReadOnly Property KeyField() As String
		Get
			Dim intIndex As Short
			
			If mstrKeyField = String.Empty Then
				KeyField = "nCodigInt"
			Else
				KeyField = mstrKeyField
			End If
			
			intIndex = InStr(mstrKeyField, ".")
			If intIndex > 0 Then
				KeyField = Mid(mstrKeyField, intIndex + 1)
			End If
		End Get
	End Property
	
	'%EOF: Indica si se llegó al final de los registros
	'UPGRADE_NOTE: EOF was upgraded to EOF. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Public ReadOnly Property EOF() As Boolean
		Get
			EOF = recTableRemote.EOF
		End Get
	End Property
	
	'%Fields:Retorna los campos de la base de datos
	Public Function Fields(ByVal strField As String) As Object
		Dim lvntFieldValue As Object
		Dim strNewField As String
		
		strNewField = strField
		lvntFieldValue = recTableRemote.FieldToClass(strNewField)
		Fields = lvntFieldValue
	End Function
	
	'**% NextRecord: it moves to the next available record in the table
	Public Sub NextRecord()
		recTableRemote.RNext()
	End Sub
	
	'**% closetable: it closes the table bieng displayed
	Public Sub closeTable()
		recTableRemote.RCloseRec()
	End Sub
	
	'**% reaTable. This function is in charge of initialize the query values.
	'%reaTable. Esta funcion se encarga de inicializar los valores de consulta
	Public Function reaTable(ByVal Table As String, Optional ByVal Code As Object = "", Optional ByVal sKeyField As String = "") As Boolean
		Dim clsTabTables As TabTables
		Dim blnTable As Boolean
        Dim strSQL As String = ""
        Dim lintCount As Short
		Dim lstrTypeOrder As String
		Dim blnExtend As Boolean
		Dim blnString As Boolean

		Descript = String.Empty
		blnTable = False
		
		'UPGRADE_WARNING: TypeName has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
		blnString = (TypeName(Code) = "String")
		
		'**+ Change Tables
		'+Cambio TABLAS
		
		Code = Replace(Code, "'", "|")
		
		If Mid(Code, 1, 1) = "," Then
			Code = "0"
		End If
		
		'**+ Verifies the case if it is about a generic table
		'+Se verifica el caso que se trata de una tabla generica
		blnTable = (UCase(Left(Table, 5)) = "TABLE")
		If blnTable Then
			If Not IsNumeric(Mid(Table, 6)) Then
				blnTable = False
			End If
		End If
		
		If blnTable Then
			mstrDesField = "sDescript"
			If meOrder = Values.ecbeOrder.Code Or Condition <> String.Empty Or Code <> String.Empty Then
				If sKeyField = String.Empty Then
					mstrKeyField = SearchKeyField(Table)
				Else
					mstrKeyField = sKeyField
				End If
				blnExtend = True
			Else
				If sKeyField = String.Empty Then
					mstrKeyField = "nCodigInt"
				Else
					mstrKeyField = sKeyField
				End If
				
			End If
			lstrTypeOrder = IIf(meOrder = Values.ecbeOrder.Code, mstrKeyField, mstrDesField)
			
			If recTableRemote Is Nothing Then
				recTableRemote = New eRemoteDB.Execute
			End If
			
			If blnExtend Then
				strSQL = "SELECT * FROM " & Table
				If String.Empty & Code <> String.Empty Then
					strSQL = strSQL & " WHERE " & Table & "." & Trim(mstrKeyField) & " = " & IIf(IsNumeric(Code), CStr(Code), "'" & Code & "'")
					If Not ActionQuery Then
						strSQL = strSQL & " AND " & Table & ".sStatregt = '1'"
					End If
				ElseIf Condition = String.Empty Then 
					If Not ActionQuery Then
						strSQL = strSQL & " WHERE " & Table & ".sStatregt = '1'"
					End If
					strSQL = strSQL & " ORDER BY " & lstrTypeOrder
				Else
					strSQL = strSQL & " WHERE "
					If Not ActionQuery Then
						strSQL = strSQL & Table & ".sStatregt = '1' AND "
					End If
                    strSQL = strSQL & "Upper(" & Table & "." & mstrDesField & ") LIKE '" & UCase(Condition) & "' ORDER BY " & lstrTypeOrder
				End If
			Else
				strSQL = "SELECT * FROM " & Table
				If Not ActionQuery Then
					strSQL = strSQL & " WHERE " & Table & ".sStatregt = '1'"
				End If
				strSQL = strSQL & " ORDER BY " & lstrTypeOrder
			End If
			recTableRemote.SQL = strSQL
			
			
		Else
			
			clsTabTables = New TabTables
			With clsTabTables
				If .Load(Table) Then
					If recTableRemote Is Nothing Then
						recTableRemote = New eRemoteDB.Execute
					End If
					mstrKeyField = .sKey
					mstrDesField = .sDesc_item
					Descript = .sDescript
					If .sIndSp = "2" Then
						strSQL = .sDs_select
						If String.Empty & Code <> String.Empty Then
							If InStr(UCase(strSQL), "WHERE") > 0 Then
								strSQL = strSQL & " AND "
							Else
								strSQL = strSQL & " WHERE "
							End If
							
							If Mid(mstrKeyField, 1, 1) = "s" Then
								strSQL = strSQL & mstrKeyField & " = '" & Code & "'"
							Else
								strSQL = strSQL & mstrKeyField & " = " & Code
							End If
						ElseIf Condition > String.Empty Then 
							If InStr(UCase(strSQL), "WHERE") > 0 Then
								strSQL = strSQL & " AND "
							Else
								strSQL = strSQL & " WHERE "
							End If
							ChangeFilter(False)
                            strSQL = strSQL & "Upper(" & mstrDesField & ") LIKE '" & UCase(Condition) & "'"
						End If
						strSQL = strSQL & " ORDER BY " & mstrDesField
						recTableRemote.SQL = strSQL
						
					Else
						
                        'recTableRemote.StoredProcedure = Table & "PKG." & Table
                        recTableRemote.SQL = "begin " & Table & "PKG." & Table & "(:sShowNum, :sCondition "
                        recTableRemote.IsTabTablesSP = True
                        If Not mParameters Is Nothing Then
                            If mParameters.Count > 0 Then
                                For lintCount = 1 To mParameters.Count
                                    recTableRemote.SQL &= ", :" & mParameters(lintCount).Name
                                Next lintCount
                            End If
                        End If
                        recTableRemote.SQL &= ", :RC1); end;"

                        recTableRemote.Special = True
                        Try
                            recTableRemote.Parameters.Add("sShowNum", .sShowNum, Parameter.eRmtDataDir.rdbParamInput, Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRmtDataAttrib.rdbParamNullable)
                        Catch ex As Exception
                            recTableRemote.Parameters("sShowNum").Value = .sShowNum
                        End Try

                        '+Busqueda por código
                        If String.Empty & Code <> String.Empty Then
                            If Mid(mstrKeyField, 1, 1) = "s" Then
                                strSQL = strSQL & mstrKeyField & " = '" & Code & "' " & " OR " & mstrKeyField & " = UPPER('" & Code & "'  )"
                            Else
                                strSQL = strSQL & mstrKeyField & " = " & Code
                            End If
                            Try
                                recTableRemote.Parameters.Add("sCondition", strSQL, Parameter.eRmtDataDir.rdbParamInput, Parameter.eRmtDataType.rdbVarchar, 255, 0, 0, eRmtDataAttrib.rdbParamNullable)
                            Catch ex As Exception
                                recTableRemote.Parameters("sCondition").Value = strSQL
                            End Try

                            '+Busqueda por descripcion
                        ElseIf Condition > String.Empty Then
                            ChangeFilter(False)
                            strSQL = strSQL & "Upper(" & mstrDesField & ") LIKE '" & UCase(Condition) & "'"
                            Try
                                recTableRemote.Parameters.Add("sCondition", strSQL, Parameter.eRmtDataDir.rdbParamInput, Parameter.eRmtDataType.rdbVarchar, 255, 0, 0, eRmtDataAttrib.rdbParamNullable)
                            Catch ex As Exception
                                recTableRemote.Parameters("sCondition").Value = strSQL
                            End Try

                        Else
                            'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
                            Try
                                recTableRemote.Parameters.Add("sCondition", System.DBNull.Value, Parameter.eRmtDataDir.rdbParamInput, Parameter.eRmtDataType.rdbVarchar, 255, 0, 0, eRmtDataAttrib.rdbParamNullable)
                            Catch ex As Exception
                                recTableRemote.Parameters("sCondition").Value = System.DBNull.Value
                            End Try

                        End If

                    End If
                    reaTable = True
                Else
                    Err.Raise(vbObjectError + 9999, Table & " no existe")
                End If
			End With
			'UPGRADE_NOTE: Object clsTabTables may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
			clsTabTables = Nothing
		End If
		
		If Not mParameters Is Nothing Then
			If mParameters.Count > 0 Then
				For lintCount = 1 To mParameters.Count
					recTableRemote.Parameters.Add(mParameters(lintCount).Name, mParameters(lintCount).Value, mParameters(lintCount).Direction, mParameters(lintCount).ParType, mParameters(lintCount).Size, mParameters(lintCount).NumericScale, mParameters(lintCount).Precision, mParameters(lintCount).Attributes, mParameters(lintCount).ParObject)
				Next lintCount
			End If
		End If
		
		reaTable = recTableRemote.Run
		
        If blnTable And Not blnExtend And reaTable Then
            With recTableRemote
                If sKeyField = String.Empty Then
                    mstrKeyField = .GetTablePrimaryKey
                End If
                If String.Empty & Code <> String.Empty Then
                    reaTable = False
                    Do While Not .EOF
                        If blnString Then
                            If CStr(.FieldToClass(mstrKeyField)) = Code Then
                                reaTable = True
                                Exit Do
                            End If
                        Else
                            If .FieldToClass(mstrKeyField) = Code Then
                                reaTable = True
                                Exit Do
                            End If
                        End If
                        .RNext()
                    Loop
                End If
            End With
        End If
		
		If reaTable Then
			If recTableRemote.Special Then
				If recTableRemote.EOF Then
					reaTable = False
				End If
			End If
		End If
	End Function
	
	'**%SearchKeyField
	'%SearchKeyField:Busca el campo llave
	Public Function SearchKeyField(ByVal Table As String) As String
		Dim lintIndex As Short
        Dim lstrField As String = ""
        Dim lrecRecordset As eRemoteDB.Execute
		
		lrecRecordset = New eRemoteDB.Execute
		With lrecRecordset
			Select Case .Server
				Case sTypeServer.sSQLServer65, sTypeServer.sSQLServer7
					.SQL = "SELECT * FROM " & .Owner & "." & Table & " (NOLOCK) WHERE 1 = 2"
				Case sTypeServer.sDB2
					.SQL = "SELECT * FROM " & .Owner & "." & Table & " WHERE 1 = 2"
				Case sTypeServer.sOracle
					.SQL = "SELECT * FROM " & Table & " WHERE 1 = 2"
			End Select
			.Special = True
			If .Run Then
				For lintIndex = 1 To .FieldsCount
					lstrField = .Item(lintIndex - 1)
					
					If InStr("sdescript_sshort_des_dcompdate_nusercode_sstatregt_scodigext", LCase(lstrField)) = 0 Then
						Exit For
					Else
						lstrField = String.Empty
					End If
				Next lintIndex
				
				SearchKeyField = lstrField
				.RCloseRec()
			Else
				SearchKeyField = String.Empty
			End If
		End With
		'UPGRADE_NOTE: Object lrecRecordset may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecRecordset = Nothing
	End Function
	
	'%ChangeFilter: Cambia comodines usados en filtros
	Private Sub ChangeFilter(ByVal blnLocal As Boolean)
		Dim intIndex As Short
		
		If blnLocal Then
			For intIndex = 1 To Len(Condition)
				If Mid(Condition, intIndex, 1) = "%" Then
					Mid(Condition, intIndex, 1) = "*"
				End If
			Next 
		Else
			For intIndex = 1 To Len(Condition)
				If Mid(Condition, intIndex, 1) = "*" Then
					Mid(Condition, intIndex, 1) = "%"
				End If
			Next 
		End If
	End Sub
	
	'%Description: Busca la descripción dada el código
	Public Function GetDescription(ByVal sTable As String, ByVal sCode As String) As Boolean
		Dim recTable As Tables
		Dim lstrDescript As String
		
		On Error GoTo GetDescription_Err
		recTable = New Tables
		If recTable.reaTable(sTable) Then
			With recTable
				While Not .EOF
                    lstrDescript = .Fields(.DescriptField)
                    If .Fields(.KeyField) = sCode Then
                        Descript = lstrDescript
                        GetDescription = True
                    End If
					.NextRecord()
				End While
				.closeTable()
			End With
			'+Cambio TABLAS
		End If
		'UPGRADE_NOTE: Object recTable may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		recTable = Nothing
		
GetDescription_Err: 
		If Err.Number Then
			GetDescription = False
		End If
		On Error GoTo 0
	End Function
End Class






