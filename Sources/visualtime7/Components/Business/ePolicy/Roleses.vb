Option Strict Off
Option Explicit On
Public Class Roleses
	Implements System.Collections.IEnumerable
	'%-------------------------------------------------------%'
	'% $Workfile:: Roleses.cls                              $%'
	'% $Author:: Nvaplat41                                  $%'
	'% $Date:: 9/10/03 19.01                                $%'
	'% $Revision:: 40                                       $%'
	'%-------------------------------------------------------%'
	
	'- Local variable to hold collection
	Private mCol As Collection
	'- Variable para indicar si ha ocurrido algun cambio a nivel del cliente
	Public nChange As Short
	'- Variable para indicar si la póliza es inominada.  Si se trata del certificado de la póliza,
	'- se realiza un tratamiento especial sobre la CA025
	Public bNopayroll As Boolean
	'- Variable para indicar si es la primera vez para la póliza que se ejecuta la CA025
	Public bFirst As Boolean
	
	'% Add: Agrega un objeto a la colección
	Public Function Add(ByRef objClass As Roles) As Roles
		If objClass Is Nothing Then
			objClass = New Roles
		End If

        With objClass
            mCol.Add(objClass, .sCertype & .nBranch & .nProduct & .nPolicy & .nCertif & .nRole & .sClient & .dEffecdate.ToString("yyyyMMdd"))
        End With
        Return objClass
    End Function
	
	'% Find: Lee los datos de la tabla para la transacción CA025(Join con Cliallopro)
	    Public Function Find(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, ByVal sPolitype As String, ByVal sCompon As String, ByVal nTransaction As Integer) As Boolean
		Dim lrecReaRoles_a As eRemoteDB.Execute
		Dim lclsRoles As Roles
		
		On Error GoTo Find_Err
		
		lrecReaRoles_a = New eRemoteDB.Execute
		
		With lrecReaRoles_a
			.StoredProcedure = "ReaRoles_a"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sPolitype", sPolitype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sCompon", sCompon, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nChange", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sNopayroll", String.Empty, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		        .Parameters.Add("nTransaction", nTransaction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				Find = True
				Do While Not .EOF
					lclsRoles = New Roles
					lclsRoles.sSel = .FieldToClass("sSel")
					lclsRoles.sCertype = .FieldToClass("sCertype")
					lclsRoles.nBranch = .FieldToClass("nBranch")
					lclsRoles.nProduct = .FieldToClass("nProduct")
					lclsRoles.nPolicy = .FieldToClass("nPolicy")
					lclsRoles.nCertif = .FieldToClass("nCertif")
					lclsRoles.nRole = .FieldToClass("nRole")
					lclsRoles.sDesT12 = .FieldToClass("sDescript_T12")
					lclsRoles.sClient = .FieldToClass("sClient")
					lclsRoles.sCliename = .FieldToClass("sCliename")
					lclsRoles.dEffecdate = .FieldToClass("dEffecdate")
					lclsRoles.dNulldate = .FieldToClass("dNulldate")
					lclsRoles.nIntermed = .FieldToClass("nIntermed")
					lclsRoles.dBirthdate = .FieldToClass("dBirthdate")
					lclsRoles.sSexclien = .FieldToClass("sSexclien")
					lclsRoles.sDesT18 = .FieldToClass("sDescript_T18")
					lclsRoles.sSmoking = .FieldToClass("sSmoking")
					lclsRoles.nTyperisk = .FieldToClass("nTyperisk")
					lclsRoles.sVIP = .FieldToClass("sVip")
                    lclsRoles.sItem = .FieldToClass("sItem")
					lclsRoles.nStatusrol = .FieldToClass("nStatusrol")
					lclsRoles.sDesT5561 = .FieldToClass("sDescript_T5561")
					lclsRoles.nRating = .FieldToClass("nRating")
					lclsRoles.sPolitype = .FieldToClass("sPolitype")
					lclsRoles.sCompon = .FieldToClass("sCompon")
					lclsRoles.sDefaulti = .FieldToClass("sDefaulti")
					lclsRoles.sRequire = .FieldToClass("sRequire")
					lclsRoles.nMax_role = .FieldToClass("nMax_role")
					lclsRoles.nTypename = .FieldToClass("nTypename")
					lclsRoles.sDesT5592 = .FieldToClass("sDescript_T5592")
					lclsRoles.sDigit = .FieldToClass("sDigit")
					lclsRoles.nPerson_typ = .FieldToClass("nPerson_typ")
					lclsRoles.nCoverPos = .FieldToClass("nCoverPos")
					lclsRoles.dContinue = .FieldToClass("dContinue")
					lclsRoles.nContrat_Pay = .FieldToClass("nContrat_Pay")
					lclsRoles.sContinued = .FieldToClass("sContinued")
					lclsRoles.sPrintName = .FieldToClass("sPrintName")
					lclsRoles.sReqAddress = .FieldToClass("sReqAddress")
					Call Add(lclsRoles)
					.RNext()
					'UPGRADE_NOTE: Object lclsRoles may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
					lclsRoles = Nothing
				Loop 
				.RCloseRec()
			End If
		End With
		
Find_Err: 
		If Err.Number Then
			Find = False
		End If
		'UPGRADE_NOTE: Object lrecReaRoles_a may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecReaRoles_a = Nothing
		On Error GoTo 0
	End Function
	
	'% InsRoles_Ca025: Lee los datos de la tabla para la transacción CA025
	Private Function InsRoles_Ca025(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, ByVal nUsercode As Integer, ByVal sPolitype As String, ByVal sCompon As String, ByVal nTransaction As Integer) As Boolean
		Dim lrecReaRoles_a As eRemoteDB.Execute
		Dim lclsRoles As Roles
		Dim lstrNopayroll As String
		Dim lblnFirsTime As Boolean
		
		On Error GoTo InsRoles_Ca025_Err
		lrecReaRoles_a = New eRemoteDB.Execute
		With lrecReaRoles_a
			.StoredProcedure = "InsRoles_Ca025"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nChange", nChange, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sNopayroll", "2", eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sPolitype", sPolitype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sCompon", sCompon, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	                .Parameters.Add("nTransaction", nTransaction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				InsRoles_Ca025 = True
				lblnFirsTime = True
				Do While Not .EOF
					lclsRoles = New Roles
					lclsRoles.sSel = .FieldToClass("sSel")
					lclsRoles.sCertype = .FieldToClass("sCertype")
					lclsRoles.nBranch = .FieldToClass("nBranch")
					lclsRoles.nProduct = .FieldToClass("nProduct")
					lclsRoles.nPolicy = .FieldToClass("nPolicy")
					lclsRoles.nCertif = .FieldToClass("nCertif")
					lclsRoles.nRole = .FieldToClass("nRole")
					lclsRoles.sDesT12 = .FieldToClass("sDescript_T12")
					lclsRoles.sClient = .FieldToClass("sClient")
					lclsRoles.sCliename = .FieldToClass("sCliename")
					lclsRoles.dEffecdate = .FieldToClass("dEffecdate")
					lclsRoles.dNulldate = .FieldToClass("dNulldate")
					lclsRoles.nIntermed = .FieldToClass("nIntermed")
					lclsRoles.dBirthdate = .FieldToClass("dBirthdate")
					lclsRoles.sSexclien = .FieldToClass("sSexclien")
					lclsRoles.sDesT18 = .FieldToClass("sDescript_T18")
					lclsRoles.sSmoking = .FieldToClass("sSmoking")
					lclsRoles.nTyperisk = .FieldToClass("nTyperisk")
					lclsRoles.sVIP = .FieldToClass("sVip")
                    lclsRoles.sItem = .FieldToClass("sItem")
					lclsRoles.nStatusrol = .FieldToClass("nStatusrol")
					lclsRoles.sDesT5561 = .FieldToClass("sDescript_T5561")
					lclsRoles.nRating = .FieldToClass("nRating")
					lclsRoles.sPolitype = .FieldToClass("sPolitype")
					lclsRoles.sCompon = .FieldToClass("sCompon")
					lclsRoles.sDefaulti = .FieldToClass("sDefaulti")
					lclsRoles.sRequire = .FieldToClass("sRequire")
					lclsRoles.nMax_role = .FieldToClass("nMax_role")
					lclsRoles.nTypename = .FieldToClass("nTypename")
					lclsRoles.sDesT5592 = .FieldToClass("sDescript_T5592")
					lclsRoles.sDigit = .FieldToClass("sDigit")
					lclsRoles.nPerson_typ = .FieldToClass("nPerson_typ")
					lclsRoles.nCoverPos = .FieldToClass("nCoverPos")
					lclsRoles.dContinue = .FieldToClass("dContinue")
					lclsRoles.nContrat_Pay = .FieldToClass("nContrat_Pay")
					lclsRoles.sContinued = .FieldToClass("sContinued")
					lclsRoles.sPrintName = .FieldToClass("sPrintName")
		                        lclsRoles.sReqAddress = .FieldToClass("sReqAddress")
					Call Add(lclsRoles)
					If lblnFirsTime Then
						lblnFirsTime = False
						'lstrNopayroll = Trim$(.Parameters("sNopayroll").Value)
						lstrNopayroll = Trim(.FieldToClass("sNoPayRoll", "2"))
						nChange = .Parameters("nChange").Value
						bFirst = lstrNopayroll <> "3"
						bNopayroll = lstrNopayroll = "1" Or lstrNopayroll = "3"
					End If
					.RNext()
					'UPGRADE_NOTE: Object lclsRoles may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
					lclsRoles = Nothing
				Loop 
				.RCloseRec()
			End If
		End With
		
InsRoles_Ca025_Err: 
		If Err.Number Then
			InsRoles_Ca025 = False
		End If
		'UPGRADE_NOTE: Object lrecReaRoles_a may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecReaRoles_a = Nothing
		'UPGRADE_NOTE: Object lclsRoles may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsRoles = Nothing
		On Error GoTo 0
	End Function
	
	
	'% Find_by_Policy: Lee los datos de la tabla para la póliza
	Public Function Find_by_Policy(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal sClient As String, ByVal dEffecdate As Date, Optional ByVal nRole As Integer = eRemoteDB.Constants.intNull, Optional ByVal nTypeList As Short = eRemoteDB.Constants.intNull, Optional ByVal sRole As String = "", Optional ByVal bCalInsuAge As Boolean = False) As Boolean
		Dim lrecreaRoles As eRemoteDB.Execute
		Dim lclsRoles As Roles
		
		On Error GoTo Find_by_Policy_Err
		
		lrecreaRoles = New eRemoteDB.Execute
		
		With lrecreaRoles
			.StoredProcedure = "ReaRoles_by_Policy"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nRole", nRole, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTypelist", nTypeList, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sRole", sRole, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 225, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				Find_by_Policy = True
				Do While Not .EOF
					lclsRoles = New Roles
					lclsRoles.sCertype = .FieldToClass("sCertype")
					lclsRoles.nBranch = .FieldToClass("nBranch")
					lclsRoles.nProduct = .FieldToClass("nProduct")
					lclsRoles.nPolicy = .FieldToClass("nPolicy")
					lclsRoles.nCertif = .FieldToClass("nCertif")
					lclsRoles.nRole = .FieldToClass("nRole")
					lclsRoles.sClient = .FieldToClass("sClient")
					lclsRoles.dEffecdate = .FieldToClass("dEffecdate")
					lclsRoles.dNulldate = .FieldToClass("dNulldate")
					lclsRoles.nIntermed = .FieldToClass("nIntermed")
					lclsRoles.dBirthdate = .FieldToClass("dBirthdate")
					lclsRoles.sSexclien = .FieldToClass("sSexclien")
					lclsRoles.sSmoking = .FieldToClass("sSmoking")
					lclsRoles.nTyperisk = .FieldToClass("nTyperisk")
					lclsRoles.sVIP = .FieldToClass("sVIP")
                    lclsRoles.sItem = .FieldToClass("sItem")
					lclsRoles.nStatusrol = .FieldToClass("nStatusrol")
					lclsRoles.nRating = .FieldToClass("nRating")
					lclsRoles.nTypename = .FieldToClass("nTypename")
					lclsRoles.sCliename = .FieldToClass("sCliename")
					lclsRoles.sDigit = .FieldToClass("sDigit")
					lclsRoles.sDesT12 = .FieldToClass("sDes12")
					lclsRoles.nPerson_typ = .FieldToClass("nPerson_typ")
					lclsRoles.dContinue = .FieldToClass("dContinue")
					lclsRoles.nContrat_Pay = .FieldToClass("nContrat_pay")
					lclsRoles.sContinued = .FieldToClass("sContinued")
					lclsRoles.sPrintName = .FieldToClass("sPrintName")
					If bCalInsuAge Then
						'+Se calcula la edad real y la edad actuarial de un cliente para bCalInsuAge verdadero
						Call lclsRoles.CalInsuAge(lclsRoles.nBranch, lclsRoles.nProduct, lclsRoles.dEffecdate, lclsRoles.dBirthdate, lclsRoles.sSexclien, lclsRoles.sSmoking)
					End If
					Call Add(lclsRoles)
					.RNext()
					'UPGRADE_NOTE: Object lclsRoles may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
					lclsRoles = Nothing
				Loop 
				.RCloseRec()
			End If
		End With
		
Find_by_Policy_Err: 
		If Err.Number Then
			Find_by_Policy = False
		End If
		'UPGRADE_NOTE: Object lrecreaRoles may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaRoles = Nothing
		On Error GoTo 0
	End Function
	
	'% Find_Tab_Covrol: Obtiene las figuras asociados a la póliza definidos en la tabla
	'%                  de figuras por coberturas(TAB_COVROL)
	Public Function Find_Tab_Covrol(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, ByVal nModulec As Short) As Boolean
		Dim lrecReaRoles_Tab_covrol As eRemoteDB.Execute
		Dim lclsRoles As Roles
		
		On Error GoTo Find_Tab_Covrol_Err
		
		lrecReaRoles_Tab_covrol = New eRemoteDB.Execute
		
		With lrecReaRoles_Tab_covrol
			.StoredProcedure = "ReaRoles_Tab_covrol"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				Find_Tab_Covrol = True
				Do While Not .EOF
					lclsRoles = New Roles
					lclsRoles.nRole = .FieldToClass("nRole")
					lclsRoles.sDescRole = .FieldToClass("sDescrole")
					lclsRoles.sClient = .FieldToClass("sClient")
					lclsRoles.nCoverPos = .FieldToClass("nCoverPos")
					Call Add(lclsRoles)
					.RNext()
					'UPGRADE_NOTE: Object lclsRoles may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
					lclsRoles = Nothing
				Loop 
				.RCloseRec()
			End If
		End With
		
Find_Tab_Covrol_Err: 
		If Err.Number Then
			Find_Tab_Covrol = False
		End If
		'UPGRADE_NOTE: Object lrecReaRoles_Tab_covrol may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecReaRoles_Tab_covrol = Nothing
		On Error GoTo 0
	End Function
	
	'%InsPreCA025: Obtiene la información a mostrar en la CA025
	Public Function InsPreCA025(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, ByVal sPolitype As String, ByVal sCompon As String, ByVal nTransaction As Integer, ByVal nUsercode As Integer, ByVal sBrancht As String) As Boolean
		Dim lblnQuery As Boolean
		
		On Error GoTo InsPreCA025_Err
		lblnQuery = nTransaction = Constantes.PolTransac.clngPolicyQuery Or nTransaction = Constantes.PolTransac.clngCertifQuery Or nTransaction = Constantes.PolTransac.clngQuotationQuery Or nTransaction = Constantes.PolTransac.clngProposalQuery Or nTransaction = Constantes.PolTransac.clngQuotAmendentQuery Or nTransaction = Constantes.PolTransac.clngPropAmendentQuery Or nTransaction = Constantes.PolTransac.clngQuotRenewalQuery Or nTransaction = Constantes.PolTransac.clngPropRenewalQuery
		
		If lblnQuery Then
			InsPreCA025 = Find_by_Policy(sCertype, nBranch, nProduct, nPolicy, nCertif, String.Empty, dEffecdate)
		ElseIf nTransaction = Constantes.PolTransac.clngPolicyIssue Or nTransaction = Constantes.PolTransac.clngCertifIssue Or nTransaction = Constantes.PolTransac.clngRecuperation Or nTransaction = Constantes.PolTransac.clngPolicyQuotation Or nTransaction = Constantes.PolTransac.clngCertifQuotation Or nTransaction = Constantes.PolTransac.clngPolicyProposal Or nTransaction = Constantes.PolTransac.clngCertifProposal Or nTransaction = Constantes.PolTransac.clngPolicyReissue Or nTransaction = Constantes.PolTransac.clngCertifReissue Then 
			
			
            InsPreCA025 = InsRoles_Ca025(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate, nUsercode, sPolitype, sCompon, nTransaction)
			
		Else
	            InsPreCA025 = Find(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate, sPolitype, sCompon, nTransaction)
		End If
		
InsPreCA025_Err: 
		If Err.Number Then
			InsPreCA025 = False
		End If
		On Error GoTo 0
	End Function
	
	'% Item: Esta propiedad es usada cuando se hace referencia a un elemento de la colección.
	Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As Roles
		Get
			Item = mCol.Item(vntIndexKey)
		End Get
	End Property
	
	'% Count: Retorna la contidad de elementos existentes en la colección.
	Public ReadOnly Property Count() As Integer
		Get
			Count = mCol.Count()
		End Get
	End Property
	
	'% NewEnum: Esta propiedad permite enumerar los elementos de la colección por medio de un "For...Each".
	'UPGRADE_NOTE: NewEnum property was commented out. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="B3FC1610-34F3-43F5-86B7-16C984F0E88E"'
	'Public ReadOnly Property NewEnum() As stdole.IUnknown
		'Get
			'NewEnum = mCol._NewEnum
		'End Get
	'End Property
	
	Public Function GetEnumerator() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
		'UPGRADE_TODO: Uncomment and change the following line to return the collection enumerator. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="95F9AAD0-1319-4921-95F0-B9D3C4FF7F1C"'
		GetEnumerator = mCol.GetEnumerator
	End Function
	
	'% Remove: Permite eliminar un elemento de la colección.
	Public Sub Remove(ByRef vntIndexKey As Object)
		mCol.Remove(vntIndexKey)
	End Sub
	
	'% Class_Initialize: Crea la colección cuando se crea esta clase.
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		mCol = New Collection
		bNopayroll = False
		bFirst = False
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
	
	'% Class_Terminate: Destruye la colección cuando se termina esta clase.
	'UPGRADE_NOTE: Class_Terminate was upgraded to Class_Terminate_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Terminate_Renamed()
		'UPGRADE_NOTE: Object mCol may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		mCol = Nothing
	End Sub
	Protected Overrides Sub Finalize()
		Class_Terminate_Renamed()
		MyBase.Finalize()
	End Sub
End Class






