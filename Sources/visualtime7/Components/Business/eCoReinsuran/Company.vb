Option Strict Off
Option Explicit On
Public Class Company
	'%-------------------------------------------------------%'
	'% $Workfile:: Company.cls                              $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 1:28p                                $%'
	'% $Revision:: 7                                        $%'
	'%-------------------------------------------------------%'
	
	'- Propiedades según la tabla en el sistema el 07/06/2001
	'   Column_name               Type                 Computed  Length  Prec  Scale  Nullable  TrimTrailingBlanks  FixedLenNullInSource
	Public nCompany As Integer 'smallint        no       5      5     0        no            (n/a)               (n/a)
	Public sAccount As String 'char            no       25                    yes            no                  yes
	Public sClient As String 'char            no       14                    yes            no                  yes
	Public sBankname As String 'char            no       30                    yes            no                  yes
	Public sClaimcon As String 'char            no       1                     yes            no                  yes
	Public dCompdate As Date 'datetime        no       8                     yes           (n/a)               (n/a)
	Public nCountry As Integer 'smallint        no       2      5     0        yes           (n/a)               (n/a)
	Public dInpdate As Date 'datetime        no       8                     yes           (n/a)               (n/a)
	Public sStatregt As String 'char            no       1                     yes            no                  yes
	Public nTaxrate As Double 'decimal         no       5      4     2        yes           (n/a)               (n/a)
	Public sType As String 'char            no       1                     yes            no                  yes
	Public nUsercode As Integer 'smallint        no       5      5     0        yes           (n/a)               (n/a)
	Public sNational As String 'char            no       1                     yes            no                  yes
	Public nClasific As Integer 'NUMBER     22   0     5    S
	
	'- Propiedades auxiliares
	Public nSel As Integer
	Public sCliename As String
	
	Private Structure udtCompany
		Dim nSel As Integer
		Dim nCompany As Integer
		Dim sCliename As String
		Dim sClient As String
		Dim sType As String
	End Structure
	
	Private arrCompany() As udtCompany
	
	Public ReadOnly Property Count() As Integer
		Get
			Count = UBound(arrCompany)
		End Get
	End Property
	Public Function ItemCompany(ByVal lintIndex As Integer) As Boolean
		If lintIndex <= UBound(arrCompany) Then
			With arrCompany(lintIndex)
				nSel = .nSel
				nCompany = .nCompany
				sCliename = .sCliename
				sClient = .sClient
				sType = .sType
			End With
			ItemCompany = True
		Else
			ItemCompany = False
		End If
	End Function
	
	'%Find: Esta rutina se encarga de realizar la lectura sobre la tabla Company al presionar
	'%la acción consultar.
	Public Function Find(ByRef lstrQuery As String) As Boolean
		Dim lrecreaCompany_a As eRemoteDB.Execute
		Dim lclsValues As eFunctions.Values
		Dim lintCount As Integer
		
		lrecreaCompany_a = New eRemoteDB.Execute
		lclsValues = New eFunctions.Values
		
		On Error GoTo Find_Err
		
		Find = True
		
		'+ Se prepara y ejecuta el "StoredProcedure" de consulta de los mensajes de error
		With lrecreaCompany_a
			If Trim(lstrQuery) = String.Empty Then
				.StoredProcedure = "reaCompany_a"
			Else
				.Sql = lstrQuery
			End If
			
			Find = .Run
			If Find Then
				ReDim arrCompany(50)
				lintCount = 0
				Do While Not .EOF
					arrCompany(lintCount).nSel = 1
					arrCompany(lintCount).nCompany = lclsValues.StringToType(.FieldToClass("nCompany"), eFunctions.Values.eTypeData.etdInteger)
					arrCompany(lintCount).sCliename = .FieldToClass("sCliename")
					arrCompany(lintCount).sClient = .FieldToClass("sClient")
					arrCompany(lintCount).sType = .FieldToClass("sType")
					lintCount = lintCount + 1
					.RNext()
				Loop 
				.RCloseRec()
				ReDim Preserve arrCompany(lintCount)
			Else
				Find = False
			End If
		End With
		'UPGRADE_NOTE: Object lrecreaCompany_a may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaCompany_a = Nothing
		
Find_Err: 
		If Err.Number Then
			Find = False
		End If
	End Function
	
	'% insPreparedQuery: Esta rutina prepara la instrucción que de debe ejecutar según los datos
	'% puestos por el usuario para la condición.
	Public Function insPreparedQuery(ByVal nCompany As String, ByVal sCliename As String, ByVal sType As String, Optional ByRef intFirstRecord As Integer = 0, Optional ByRef intLastRecord As Integer = 0) As Boolean
		
		Dim lclsSelect As eRemoteDB.ConstructSelect
		Dim lblnWhereInd As Boolean
		Dim lstrQuery As String
		Dim lstrDate As String
		
		lclsSelect = New eRemoteDB.ConstructSelect
		
		On Error GoTo insPreparedQuery_Err
		
		With lclsSelect
			'.Owner "Insudb"
			.SelectClause("Cli.sClient sClient, Comp.nCompany nCompany, Comp.sType sType, Cli.sCliename") ', T219.sDescript"
			.NameFatherTable("Company", "Comp")
			.RelationsTables(eRemoteDB.ConstructSelect.eRelationTables.RelLeft, "Client", "Cli", "Comp.sClient = Cli.sClient")
			.RelationsTables(eRemoteDB.ConstructSelect.eRelationTables.RelLeft, "Table219", "T219", "Comp.sType = T219.nCodigInt")
			
			If Trim(nCompany) <> String.Empty Then
				If Mid(nCompany, 1, 1) = "<" Or Mid(nCompany, 1, 1) = ">" Or Mid(nCompany, 1, 1) = "=" Then
					lstrDate = Mid(nCompany, 2)
					.WhereClause("Comp.nCompany" & Mid(nCompany, 1, 1), eRemoteDB.ConstructSelect.eTypeValue.TypCNumeric, lstrDate)
				Else
					.WhereClause("Comp.nCompany ", eRemoteDB.ConstructSelect.eTypeValue.TypCNumeric, nCompany)
				End If
			Else
				.WhereClause("Comp.nCompany >", eRemoteDB.ConstructSelect.eTypeValue.TypCNumeric, "0")
			End If
			
			If Trim(sCliename) <> String.Empty Then
				.WhereClause("Cli.sCliename", eRemoteDB.ConstructSelect.eTypeValue.TypCString, sCliename, eRemoteDB.ConstructSelect.eWordConnection.eAnd)
			End If
			
			If Trim(sType) <> String.Empty Then
				If Mid(sType, 1, 1) = "<" Or Mid(sType, 1, 1) = ">" Or Mid(sType, 1, 1) = "=" Then
					lstrDate = Mid(sType, 2)
					.WhereClause("Comp.sType" & Mid(sType, 1, 1), eRemoteDB.ConstructSelect.eTypeValue.TypCString, lstrDate, eRemoteDB.ConstructSelect.eWordConnection.eAnd)
				Else
					lstrDate = sType
					.WhereClause("Comp.sType", eRemoteDB.ConstructSelect.eTypeValue.TypCString, lstrDate, eRemoteDB.ConstructSelect.eWordConnection.eAnd)
				End If
			End If
			
			.OrderBy("Order by nCompany")
		End With
		
		'+ Se ejecuta la rutina que lee la compañia desde la tabla 'Company'
		If Find(lclsSelect.Answer) Then
			insPreparedQuery = True
		Else
			insPreparedQuery = False
		End If
		
insPreparedQuery_Err: 
		If Err.Number Then
			insPreparedQuery = False
		End If
	End Function
	
	
	'**%FindReinsuPolicy: This function verifies if policies have been issued to the reinsurance contract.
	'%FindReinsuPolicy:Esta función permite verificar si ya se han emitido pólizas al contrato de reaseguro.
	Public Function Find_CompanySystem() As Boolean
		Dim lrecCompany As New eRemoteDB.Execute
		
		lrecCompany = New eRemoteDB.Execute
		
		On Error GoTo Find_CompanySystem_Err
		
		With lrecCompany
			
			.StoredProcedure = "Find_CompanySystem"
			If .Run Then
				nCompany = .FieldToClass("nCompany")
				.RCloseRec()
				Find_CompanySystem = True
			Else
				Find_CompanySystem = False
			End If
			
		End With
		
Find_CompanySystem_Err: 
		If Err.Number Then
			Find_CompanySystem = False
		End If
	End Function
	
	
	Public Function Find_ClasificCompany(ByVal nCompany As Integer) As Boolean
		Dim lrecCompany As New eRemoteDB.Execute
		
		lrecCompany = New eRemoteDB.Execute
		
		On Error GoTo Find_ClasificCompany_Err
		
		With lrecCompany
			
			.StoredProcedure = "reaCompany"
			.Parameters.Add("nCompany", nCompany, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				Me.nCompany = .FieldToClass("nCompany")
				nClasific = .FieldToClass("nClasific")
				.RCloseRec()
				Find_ClasificCompany = True
			Else
				Find_ClasificCompany = False
			End If
			
		End With
		
Find_ClasificCompany_Err: 
		If Err.Number Then
			Find_ClasificCompany = False
		End If
	End Function
End Class






