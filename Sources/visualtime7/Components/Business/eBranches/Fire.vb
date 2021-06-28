Option Strict Off
Option Explicit On
Public Class Fire
	'%-------------------------------------------------------%'
	'% $Workfile:: Fire.cls                                 $%'
	'% $Author:: Nvaplat41                                  $%'
	'% $Date:: 10/10/03 17.34                               $%'
	'% $Revision:: 7                                        $%'
	'%-------------------------------------------------------%'
	
	'**-Properties according to the table in the system on Octube 17,2001.
	'*-Propiedades según la tabla en el sistema el 17/10/2001
	'Column_name                 Type                   Computed   Length   Prec  Scale Nullable    TrimTrailingBlanks    FixedLenNullInSource
	'---------------------   ------------------------   --------   -------  ----  ----- --------    -------------------   --------------------
	Public sCertype As String '   char        no  1                   no  no  no
	Public nBranch As Integer '   smallint    no  2   5   0   no  (n/a)   (n/a)
	Public nProduct As Integer '   smallint    no  2   5   0   no  (n/a)   (n/a)
	Public nPolicy As Double '   int         no  4   10  0   no  (n/a)   (n/a)
	Public nCertif As Double '   int         no  4   10  0   no  (n/a)   (n/a)
	Public dEffecdate As Date '   datetime    no  8                   no  (n/a)   (n/a)
	Public nCapital As Double '   decimal     no  9   12  0   yes (n/a)   (n/a)
	Public nSpCombType As Integer '   smallint    no  2   5   0   yes (n/a)   (n/a)
	Public nArticle As Integer '   smallint    no  2   5   0   yes (n/a)   (n/a)
	Public nBuildType As Integer '   smallint    no  2   5   0   yes (n/a)   (n/a)
	Public nActivityType As Integer '   smallint    no  2   5   0   yes (n/a)   (n/a)
	Public nFamily As Integer '   smallint    no  2   5   0   yes (n/a)   (n/a)
	Public nSeismicZone As Integer '   smallint    no  2   5   0   yes (n/a)   (n/a)
	Public nSideCloseType As Integer '   smallint    no  2   5   0   yes (n/a)   (n/a)
	Public nCl_risk As Integer '   smallint    no  2   5   0   yes (n/a)   (n/a)
	Public nRoofType As Integer '   smallint    no  2   5   0   yes (n/a)   (n/a)
	Public sClient As String '   datetime    no  8                   yes (n/a)   (n/a)
	Public sDecla_type As String '   char        no  14                  yes no  yes
	Public nDep_prem As Double '   char        no  1                   yes no  yes
	Public dExpirdat As Date '   decimal     no  9   10  2   yes (n/a)   (n/a)
	Public sFactu_freq As String '   datetime    no  8                   yes (n/a)   (n/a)
	Public nDetailArt As Integer '   char        no  1                   yes no  yes
	Public nFloor_quan As Integer '   smallint    no  2   5   0   yes (n/a)   (n/a)
	Public dIssuedat As Date '   smallint    no  2   5   0   yes (n/a)   (n/a)
	Public sLocat_risk As String '   datetime    no  8                   yes (n/a)   (n/a)
	Public nNullcode As Integer '   char        no  1                   yes no  yes
	Public dNulldate As Date '   smallint    no  2   5   0   yes (n/a)   (n/a)
	Public sPopulat_ty As String '   datetime    no  8                   yes (n/a)   (n/a)
	Public nPremium As Double '   char        no  1                   yes no  yes
	Public nProm_rate As Double '   decimal     no  9   10  2   yes (n/a)   (n/a)
	Public dStartDate As Date '   decimal     no  5   4   2   yes (n/a)   (n/a)
	Public nUsercode As Integer '   datetime    no  8                   yes (n/a)   (n/a)
	Public nIndPeriod As Integer '   smallint    no  2   5   0   yes (n/a)   (n/a)
	Public nConstcat As Integer '   smallint    no  2   5   0   yes (n/a)   (n/a)
	Public nTransactio As Integer '   smallint    no  2   5   0   yes (n/a)   (n/a)
	Public nActivityCat As Integer '   smallint    no  2   5   0   yes (n/a)   (n/a)
	Public nAnt_prem As Double '   smallint    no  2   5   0   yes (n/a)   (n/a)
	Public sDecla_freq As String '   decimal     no  5   3   2   yes (n/a)   (n/a)
	Public nStatusInstance As Integer '   char        no  1                   yes no  yes
	
	Public sCliename As String '   char        no  40                  yes yes yes SQL_Latin1_General_CP1_CI_AS
	
	Public sBranchName As String '   char    no  30                  yes no  yes
	
	Public sDescript As String '   char    no  30                  yes no  yes
	
	Public sPolitype As String '   char        no  1                   yes no  yes SQL_Latin1_General_CP1_CI_AS
	
	Public nPayfreq As Integer '   smallint    no  2   5       0       yes (n/a)   (n/a)   NULL
	
	Public pexeConstruct As eRemoteDB.ConstructSelect
	
	
	'%Find. Este metodo se encarga de realizar la busqueda de los datos correspondientes para la
	'%tabla "Fire". Devolviendo verdadero o falso dependiendo de la existencia o no de los datos
	Public Function FindReapolicyFire(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double) As Boolean
		
		Dim lrecinsReapolicyFire As eRemoteDB.Execute
		
		lrecinsReapolicyFire = New eRemoteDB.Execute
		
		'Definición de parámetros para stored procedure 'insudb.insReapolicyFire'
		'Información leída el 17/10/2001 03:00:31 p.m.
		
		With lrecinsReapolicyFire
			.StoredProcedure = "insReapolicyFire"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				nBranch = .FieldToClass("nBranch")
				nProduct = .FieldToClass("nProduct")
				nCertif = .FieldToClass("ncertif")
				sPolitype = .FieldToClass("sPolitype")
				nPayfreq = .FieldToClass("nPayfreq")
				dEffecdate = .FieldToClass("dEffecdate")
				dExpirdat = .FieldToClass("dExpirdat")
				nCapital = .FieldToClass("nCapital")
				nPremium = .FieldToClass("nPremium")
				nArticle = .FieldToClass("nArticle")
				nDetailArt = .FieldToClass("nDetailArt")
				nActivityCat = .FieldToClass("nActivityCat")
				nConstcat = .FieldToClass("nConstCat")
				nFloor_quan = .FieldToClass("nFloor_quan")
				nSpCombType = .FieldToClass("nSpCombType")
				nSideCloseType = .FieldToClass("nSideCloseType")
				nIndPeriod = .FieldToClass("nIndPeriod")
				nRoofType = .FieldToClass("nRoofType")
				FindReapolicyFire = True
				
				.RCloseRec()
			End If
		End With
		'UPGRADE_NOTE: Object lrecinsReapolicyFire may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecinsReapolicyFire = Nothing
		
		
	End Function
	
	'%insVal_INC001_K: Esta función se encarga de validar los datos introducidos en la zona de detalle para
	'%forma.
	Public Function insVal_INC001_K(ByVal sCodispl As String, ByVal sAction As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPayfreq As Integer, ByVal nCapital As Double, ByVal nPremium As Double, ByVal dEffecdate As Date, ByVal dNulldate As Date, ByVal nTypePolicy As Integer, ByVal nArticle As Integer, ByVal nDetailArt As Integer, ByVal nActivityCat As Integer, ByVal nConstcat As Integer, ByVal nFloor_quan As Integer, ByVal nSpCombType As Integer, ByVal nSideCloseType As Integer, ByVal nIndPeriod As Integer, ByVal nRoofType As Integer) As String
		Dim lerrTime As eFunctions.Errors
		Dim lvalField As eFunctions.valField
		Dim lobjValues As eFunctions.Values
		
		
		Dim lintindex As Integer
		Dim lintIniPos As Integer
		
		Dim lstrWhere As String
		Dim lstrCondition As String
		Dim lstrValue As String
		
		'-Se define la variable lstrBrancht utilizada para almacenar el valor del ramo técnico.
		
		Dim lstrBrancht As String
		
		
		'-Se define el objeto lobjValProd para poder hacer uso de la función insValProdmaster
		
		Dim lobjValProd As eProduct.Product
		
		
		lvalField = New eFunctions.valField
		lobjValues = New eFunctions.Values
		lerrTime = New eFunctions.Errors
		
		lobjValProd = New eProduct.Product
		On Error GoTo insVal_INC001_K_Err
		
		
		
		'+Validación del campo Tipo de pago.
		
		If nPayfreq > 0 Then
			If Not pexeConstruct.WhereClause("Pol.nPayfreq", eRemoteDB.ConstructSelect.eTypeValue.TypCNumeric, Str(nPayfreq), eRemoteDB.ConstructSelect.eWordConnection.eAnd) Then
				Call lerrTime.ErrorMessage(sCodispl, 1948)
			End If
		End If
		
		
		'+Validación del campo Capital.
		
		If nCapital > 0 Then
			lstrValue = Str(nCapital)
			lintIniPos = 0
			If Not IsNumeric(Mid(Trim(lstrValue), 1, 1)) Then
				lintIniPos = 1
				If Not IsNumeric(Mid(Trim(lstrValue), 2, 2)) Then
					lintIniPos = 2
				End If
			End If
			
			'+Se valida que se haya colocado un número después de los comodines.
			
			If Trim(Mid(lstrValue, 1 + lintIniPos)) <> String.Empty Then
				With lvalField
					.Max = 999999999999#
					.Min = 0
					.ValFormat = "##########"
                    .Descript = eFunctions.Values.GetMessage(826)
                End With
				If lvalField.ValNumber(Mid(lstrValue, 1 + lintIniPos),  , eFunctions.valField.eTypeValField.ValAll) Then
					
					'Se verifica que el número introducido sea un entero.
					
					If (CDbl(Mid(lstrValue, 1 + lintIniPos)) <> lvalField.Value) And (lvalField.Value <> String.Empty) Then
						Call lerrTime.ErrorMessage(sCodispl, 1952)
					Else
						If Not pexeConstruct.WhereClause("Fire.nCapital", eRemoteDB.ConstructSelect.eTypeValue.TypCNumeric, Str(nCapital), eRemoteDB.ConstructSelect.eWordConnection.eAnd) Then
							Call lerrTime.ErrorMessage(sCodispl, 1948)
						End If
					End If
				End If
			Else
				Call lerrTime.ErrorMessage(sCodispl, 1953)
			End If
		End If
		
		'+Validación de la prima
		
		If nPremium > 0 Then
			lstrValue = Str(nPremium)
			lintIniPos = 0
			If Not IsNumeric(Mid(Trim(lstrValue), 1, 1)) Then
				lintIniPos = 1
				If Not IsNumeric(Mid(Trim(lstrValue), 2, 2)) Then
					lintIniPos = 2
				End If
			End If
			
			'+Se verifica que haya un número después de los comodines
			
			If Trim(Mid(lstrValue, 1 + lintIniPos)) <> String.Empty Then
				With lvalField
					.Max = 99999999.99
					.Min = 0
					.ValFormat = "########.##"
                    .Descript = eFunctions.Values.GetMessage(827)
                End With
				If lvalField.ValNumber(Trim(Mid(lstrValue, 1 + lintIniPos)),  , eFunctions.valField.eTypeValField.ValAll) Then
					If Not pexeConstruct.WhereClause("Fire.nPremium", eRemoteDB.ConstructSelect.eTypeValue.TypCNumeric, Str(nPremium), eRemoteDB.ConstructSelect.eWordConnection.eAnd) Then
						Call lerrTime.ErrorMessage(sCodispl, 1948)
					End If
				End If
			Else
				Call lerrTime.ErrorMessage(sCodispl, 1953)
			End If
		End If
		
		'+Validación del campo Fecha de Efecto.
		If dEffecdate <> dtmNull Then
            lvalField.Descript = eFunctions.Values.GetMessage(828)
            If lvalField.ValDate(dEffecdate,  , eFunctions.valField.eTypeValField.onlyvalid) Then
				If Not pexeConstruct.WhereClause("Fire.dEffecdate", eRemoteDB.ConstructSelect.eTypeValue.TypCDate, "<=" & Trim(CStr(dEffecdate)), eRemoteDB.ConstructSelect.eWordConnection.eAnd) Then
					Call lerrTime.ErrorMessage(sCodispl, 1948)
				Else
					If Not pexeConstruct.WhereClause("Fire.dNulldate", eRemoteDB.ConstructSelect.eTypeValue.TypCDate, ">" & Trim(CStr(dEffecdate)), eRemoteDB.ConstructSelect.eWordConnection.eAnd, "(", "OR Fire.dNulldate IS NULL)") Then
						Call lerrTime.ErrorMessage(sCodispl, 1948)
					End If
				End If
			End If
		End If
		
		'+Validación del campo Fecha de Vencimiento.
		
		If dNulldate <> dtmNull Then
            lvalField.Descript = eFunctions.Values.GetMessage(829)
            If lvalField.ValDate(dNulldate,  , eFunctions.valField.eTypeValField.onlyvalid) Then
				If Not pexeConstruct.WhereClause("Fire.dExpirdat", eRemoteDB.ConstructSelect.eTypeValue.TypCDate, "=" & Trim(CStr(dNulldate)), eRemoteDB.ConstructSelect.eWordConnection.eAnd) Then
					Call lerrTime.ErrorMessage(sCodispl, 1948)
				End If
			End If
		End If
		
		'+Validación del campo Tipo de Póliza.
		
		If Not pexeConstruct.WhereClause("Pol.sPolitype", eRemoteDB.ConstructSelect.eTypeValue.TypCString, Trim(Str(nTypePolicy)), eRemoteDB.ConstructSelect.eWordConnection.eAnd) Then
			Call lerrTime.ErrorMessage(sCodispl, 1948)
		End If
		
		'+Campo Actividad
		
		If Not pexeConstruct.WhereClause("Fire.nArticle", eRemoteDB.ConstructSelect.eTypeValue.TypCNumeric, Trim(Str(nArticle)), eRemoteDB.ConstructSelect.eWordConnection.eAnd) Then
			Call lerrTime.ErrorMessage(sCodispl, 1948)
		End If
		
		'+ Campo detalle
		
		If Not pexeConstruct.WhereClause("Fire.nDetailArt", eRemoteDB.ConstructSelect.eTypeValue.TypCNumeric, Trim(Str(nDetailArt)), eRemoteDB.ConstructSelect.eWordConnection.eAnd) Then
			Call lerrTime.ErrorMessage(sCodispl, 1948)
		End If
		
		'+ Campo Categoría de actividad
		
		If Not pexeConstruct.WhereClause("Fire.nActivityCat", eRemoteDB.ConstructSelect.eTypeValue.TypCNumeric, Trim(Str(nActivityCat)), eRemoteDB.ConstructSelect.eWordConnection.eAnd) Then
			Call lerrTime.ErrorMessage(sCodispl, 1948)
		End If
		
		
		'+ Campo categoría de construcción
		
		If Not pexeConstruct.WhereClause("Fire.nConstCat", eRemoteDB.ConstructSelect.eTypeValue.TypCNumeric, Trim(Str(nConstcat)), eRemoteDB.ConstructSelect.eWordConnection.eAnd) Then
			Call lerrTime.ErrorMessage(sCodispl, 1948)
		End If
		
		
		'+ Campo numéro de plantas
		
		If Not pexeConstruct.WhereClause("Fire.nFloor_quan", eRemoteDB.ConstructSelect.eTypeValue.TypCNumeric, Trim(Str(nFloor_quan)), eRemoteDB.ConstructSelect.eWordConnection.eAnd) Then
			Call lerrTime.ErrorMessage(sCodispl, 1948)
		End If
		
		
		'+ Capo tipo de riesgo
		
		If Not pexeConstruct.WhereClause("Fire.nSpCombType", eRemoteDB.ConstructSelect.eTypeValue.TypCNumeric, Trim(Str(nSpCombType)), eRemoteDB.ConstructSelect.eWordConnection.eAnd) Then
			Call lerrTime.ErrorMessage(sCodispl, 1948)
		End If
		
		'+ Campo tipo de cerramiento
		If Not pexeConstruct.WhereClause("Fire.nSideCloseType", eRemoteDB.ConstructSelect.eTypeValue.TypCNumeric, Trim(Str(nSideCloseType)), eRemoteDB.ConstructSelect.eWordConnection.eAnd) Then
			Call lerrTime.ErrorMessage(sCodispl, 1948)
		End If
		
		'+ Campo Periodo de indemnización
		If Not pexeConstruct.WhereClause("Fire.nIndPeriod", eRemoteDB.ConstructSelect.eTypeValue.TypCNumeric, Trim(Str(nIndPeriod)), eRemoteDB.ConstructSelect.eWordConnection.eAnd) Then
			Call lerrTime.ErrorMessage(sCodispl, 1948)
		End If
		
		'+ Campo tipo de techo
		
		If Not pexeConstruct.WhereClause("Fire.nRoofType", eRemoteDB.ConstructSelect.eTypeValue.TypCNumeric, Trim(Str(nRoofType)), eRemoteDB.ConstructSelect.eWordConnection.eAnd) Then
			Call lerrTime.ErrorMessage(sCodispl, 1948)
		End If
		insVal_INC001_K = lerrTime.Confirm
		
insVal_INC001_K_Err: 
		If Err.Number Then
			insVal_INC001_K = insVal_INC001_K & Err.Description
		End If
		'UPGRADE_NOTE: Object lerrTime may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lerrTime = Nothing
		'UPGRADE_NOTE: Object pexeConstruct may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		pexeConstruct = Nothing
		'UPGRADE_NOTE: Object lobjValues may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lobjValues = Nothing
		On Error GoTo 0
	End Function
	
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		pexeConstruct = New eRemoteDB.ConstructSelect
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
	
	'UPGRADE_NOTE: Class_Terminate was upgraded to Class_Terminate_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Terminate_Renamed()
		'UPGRADE_NOTE: Object pexeConstruct may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		pexeConstruct = Nothing
	End Sub
	Protected Overrides Sub Finalize()
		Class_Terminate_Renamed()
		MyBase.Finalize()
	End Sub
End Class






