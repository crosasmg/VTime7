Option Strict Off
Option Explicit On
Public Class Client
	'%-------------------------------------------------------%'
	'% $Workfile:: Client.cls                               $%'
	'% $Author:: Lpizarro                                   $%'
	'% $Date:: 19/03/06 16:59                               $%'
	'% $Revision:: 3                                        $%'
	'%-------------------------------------------------------%'
	
	Enum eClientType
		ctCompany = 2
		ctPerson = 1
	End Enum
	
	Private Structure StructClientTyp
		'UPGRADE_WARNING: Fixed-length string size must fit in the buffer. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="3C1E4426-0B80-443E-B943-0627CD55D48B"'
		<VBFixedString(1),System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.ByValArray,SizeConst:=1)> Public sType() As Char
		Dim bProvision As Boolean
		Dim bNatural As Boolean
	End Structure
	
	Enum eDeathBlock
		Blockeade = 1
		Death = 2
		BlockDeath = 3
	End Enum
	
	Enum eType
		cstrInsurance = 1
		cstrReinsurance = 2
		cstrBrokerOrBrokerageFirm = 3
		cstrInsuranceReinsurance = 4
	End Enum
	
	Private aClientType() As StructClientTyp
	
	'+ Cantidad de paginas en la secuencia de documentos del cliente
	Private Const CN_PAGNUM As Integer = 2
	
	'- Se define la constante para los codispl en la subsecuencia de documentos del cliente
	Private Const CN_WINDOWS As String = "BC802   BC803   "
	
	'+Propiedades según la tabla en el sistema 11/01/2000
	Public sClient As String 'char      14                 no                                  yes                                 no
	Public sAccount_in As String 'char      1                  yes                                 yes                                 yes
	Public nSpeciality As Integer 'int       4      10    0     yes                                 (n/a)                               (n/a)
	Public dAprobdate As Date 'datetime  8                  yes                                 (n/a)                               (n/a)
	Public dBirthdat As Date 'datetime  8                  yes                                 (n/a)                               (n/a)
	Public sBlockade As String 'char      1                  yes                                 yes                                 yes
	Public nCivilsta As Integer 'smallint  2      5     0     yes                                 (n/a)                               (n/a)
	Public nClass As Integer 'smallint  2      5     0     yes                                 (n/a)                               (n/a)
	Public sCliename As String 'char      40                 yes                                 yes                                 yes
	Public sFirstName As String 'char      20                 yes                                 yes                                 yes
	Public sLastName As String 'char      20                 yes                                 yes                                 yes
	Public dDeathdat As Date 'datetime  8                  yes                                 (n/a)                               (n/a)
	Public dDriverdat As Date 'datetime  8                  yes                                 (n/a)                               (n/a)
	Public nHeight As Double 'decimal   3      4     2     yes                                 (n/a)                               (n/a)
	Public nHouse_type As Integer 'char      1                  yes                                 yes                                 yes
	Public dInpdate As Date 'datetime  8                  yes                                 (n/a)                               (n/a)
	Public nComp_Type As Integer
	Public sLicense As String 'char      10                 yes                                 yes                                 yes
	Public nNotenum As Integer 'int       4      10    0     yes                                 (n/a)                               (n/a)
	Public nOffice As Integer 'smallint  2      5     0     yes                                 (n/a)                               (n/a)
	Public nQ_cars As Integer 'smallint  2      5     0     yes                                 (n/a)                               (n/a)
	Public nQ_child As Integer 'smallint  2      5     0     yes                                 (n/a)                               (n/a)
	Public nRate As Double 'decimal   5      9     6     yes                                 (n/a)                               (n/a)
	Public sTax_code As String 'char      12                 yes                                 yes                                 yes
	Public sSexclien As String 'char      1                  yes                                 yes                                 yes
	Public sSmoking As String 'char      1                  yes                                 yes                                 yes
	Public sCuit As String 'char      14                 yes                                 yes                                 yes
	Public nTitle As Integer 'smallint  2      5     0     yes                                 (n/a)                               (n/a)
	Public nUsercode As Integer 'NUMBER    22   0     5    N
	Public nWeight As Double 'decimal   4      5     2     yes                                 (n/a)                               (n/a)
	Public sAuto_char As String 'char      1                  yes                                 yes                                 yes
	Public sCredit_card As String 'char      1                  yes                                 yes                                 yes
	Public nEconomic_l As Integer 'smallint  2      5     0     yes                                 (n/a)                               (n/a)
	Public nEmpl_qua As Integer 'int       4      10    0     yes                                 (n/a)                               (n/a)
	Public nInvoicing As Double 'decimal   7      13    0     yes                                 (n/a)                               (n/a)
	Public nImageNum As Integer 'int       4      10    0     yes                                 (n/a)                               (n/a)
	Public nPerson_typ As Integer 'smallint  2      5     0     yes
	Public nArea As Integer ' NUMBER   22     0     5     yes
	Public dDrivexpdat As Date ' DATE     7      0     0     yes
	Public nTypDriver As Integer ' NUMBER   22     0     5     yes
	Public nDisability As Integer ' NUMBER   22     0     5     yes
	Public nLimitdriv As Integer ' NUMBER   22     0     5     yes
	Public sLegalname As String ' VARCHAR2 60     0     0     yes
	Public sLastname2 As String ' CHAR     20     0     0     yes
	Public nHealth_org As Integer ' NUMBER   22     0     5     yes
	Public nAfp As Integer ' NUMBER   22     0     5     yes
	Public dWedd As Date ' DATE     7      0     0     yes
	Public sBill_ind As String ' CHAR     1      0     0     yes
	Public nIncapacity As Integer ' NUMBER   22     0     5     yes
	Public dIncapacity As Date ' DATE     7      0     0     yes
	Public nIncap_cod As Integer ' NUMBER   22     0     5     yes
	Public nNationality As Integer ' NUMBER   22     0     5     yes
    Public sDigit As String ' char     1
    Public sPEP As String ' char     1
    Public sUsPerson As String
    Public sCRS As String

    '- Variables auxiliares

    Public bProvision As Boolean
	Public bNatural As Boolean
	Public sCodClientType As String
	Public nAge As Short
	
	Private bFind As Boolean
	
	'- Variables para el manjo de la secuencia
	Private nVal_nev As Integer
	Private nVal_doc As Integer
	
	'- Variable que se añade por cambios referentes a APV2 - ACM - 17/09/2003
	Public dRetirement As Date
	
	'+ [APV2] Certificado 24 Movimiento Anual cuentas de APV por RUT
	'- Fecha a partir de la cual el tabajador es Independiente
	Public dIndependant As Date
	
	'- Fecha a partir de la cual el trabajador es Dependiente
    Public dDependant As Date
    Public sFatca As String

    '+ Campos añadidos para soportar el Giro de Negocio
    Public sComplCod As String
    '+ Campos para Giro de Negocio
    Public nBusinessty As Short
    Public nCommergrp As Short
    Public nCodkind As Short

    Public nTypeCompany As Short ' TODO falta el campo en l a tabla y la moficacion para que se grabe

	'% Find: Función que realiza la busqueda en la tabla client dado un coigo de cliente....
	Public Function Find(ByVal sClient As String, Optional ByVal bFind As Boolean = False) As Boolean
        Dim lrecreaClient As eRemoteDB.Execute
        Dim lclsBusinessFun As Object
		
		On Error GoTo Find_Err
		
		If sClient <> Me.sClient Or bFind Then
			
            lrecreaClient = New eRemoteDB.Execute
            lclsBusinessFun = eRemoteDB.NetHelper.CreateClassInstance("eGeneral.Business_Functs")

			With lrecreaClient
				.StoredProcedure = "reaClient"
				.Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				If .Run Then
					Me.sClient = sClient
					sAccount_in = .FieldToClass("sAccount_in")
					nInvoicing = .FieldToClass("nInvoicing")
					nSpeciality = .FieldToClass("nSpeciality")
					nCivilsta = .FieldToClass("nCivilsta")
					dAprobdate = .FieldToClass("dAprobdate")
					dBirthdat = .FieldToClass("dBirthdat")
					sBlockade = .FieldToClass("sBlockade")
					nClass = .FieldToClass("nClass")
					sCliename = .FieldToClass("sCliename")
					sFirstName = .FieldToClass("sFirstname")
					sLastName = .FieldToClass("sLastname")
					dDeathdat = .FieldToClass("dDeathdat")
					dDriverdat = .FieldToClass("dDriverdat")
					nHeight = .FieldToClass("nHeight")
					dInpdate = .FieldToClass("dInpdate")
					nHouse_type = .FieldToClass("nHouse_type")
					sLicense = .FieldToClass("sLicense")
					nNotenum = .FieldToClass("nNotenum")
					nOffice = .FieldToClass("nOffice")
					nQ_cars = .FieldToClass("nQ_cars")
					nQ_child = .FieldToClass("nQ_child")
					nRate = .FieldToClass("nRate")
					sTax_code = .FieldToClass("sTax_code")
					sSexclien = .FieldToClass("sSexclien")
					sSmoking = .FieldToClass("sSmoking")
					sCuit = .FieldToClass("sCuit")
					nTitle = .FieldToClass("nTitle")
					nUsercode = .FieldToClass("nUsercode")
					nWeight = .FieldToClass("nWeight")
					sAuto_char = .FieldToClass("sAuto_char")
					sCredit_card = .FieldToClass("sCredit_card")
					nEconomic_l = .FieldToClass("nEconomic_l")
					nEmpl_qua = .FieldToClass("nEmpl_qua")
					nImageNum = .FieldToClass("nImagenum")
					nPerson_typ = .FieldToClass("nPerson_typ")
					nArea = .FieldToClass("nArea")
					dDrivexpdat = .FieldToClass("dDrivexpdat")
					nTypDriver = .FieldToClass("nTypdriver")
					nDisability = .FieldToClass("nDisability")
					nLimitdriv = .FieldToClass("nLimitdriv")
					sLegalname = .FieldToClass("sLegalname")
					sLastname2 = .FieldToClass("sLastname2")
					nHealth_org = .FieldToClass("nHealth_org")
					nAfp = .FieldToClass("nAfp")
					dWedd = .FieldToClass("dWedd")
					sBill_ind = .FieldToClass("sBill_ind")
					nIncapacity = .FieldToClass("nIncapacity")
					dIncapacity = .FieldToClass("dIncapacity")
					nIncap_cod = .FieldToClass("nIncap_cod")
					nNationality = .FieldToClass("nNationality")
					sDigit = .FieldToClass("sDigit")
					bNatural = IIf(nPerson_typ = 2, False, True)
					dRetirement = .FieldToClass("dRetirement")
					nAge = .FieldToClass("nAge")
					nComp_Type = .FieldToClass("nComp_Type")
					
					'+ [APV2] Certificado 24 Movimiento Anual cuentas de APV por RUT
					dIndependant = .FieldToClass("dIndependant")
                    dDependant = .FieldToClass("dDependant")
                    sFatca = .FieldToClass("sFatca")
                    sPEP = .FieldToClass("sClientpep")
                    sUsPerson = .FieldToClass("sUsPerson")
                    sCRS = .FieldToClass("sCRS")

                    '+ Recupera información del Giro de Negocio
                    'sComplCod = .FieldToClass("sComplCod")
                    If Me.sComplCod <> String.Empty Then
                        Me.nBusinessty = lclsBusinessFun.getBusinessty(Me.sComplCod)
                        Me.nBusinessty = lclsBusinessFun.getCommergrp(Me.sComplCod)
                        Me.nCodkind = lclsBusinessFun.getCodkind(Me.sComplCod)
                    End If
					.RCloseRec()
					Find = True
				Else
					Find = False
				End If
            End With
            lclsBusinessFun = Nothing
		Else
			Find = True
		End If
		
Find_Err: 
		If Err.Number Then
			Find = False
		End If
		
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecreaClient may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaClient = Nothing
		
	End Function
	
	'% LoadClientType: Procedimiento que carga los tipo de clientes existentes en la base de datos a un arreglo ...
	Public Sub LoadClientType()
		Dim recSQL As eRemoteDB.Query
		
		recSQL = New eRemoteDB.Query
		Dim intCount As Integer
		With recSQL
			If .OpenQuery("Client_typ", "sType, sProvision, sNatural", "sStatregt = '1'") Then
				
				ReDim aClientType(100)
				intCount = 0
				Do While Not .EndQuery
					aClientType(intCount).sType = .FieldToClass("sType")
					aClientType(intCount).bProvision = (.FieldToClass("sProvision") = "1")
					aClientType(intCount).bNatural = (.FieldToClass("sNatural") = "1")
					.NextRecord()
					intCount = intCount + 1
				Loop 
				.CloseQuery()
				ReDim Preserve aClientType(intCount - 1)
			End If
		End With
		
		'UPGRADE_NOTE: Object recSQL may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		recSQL = Nothing
		
	End Sub

    '% ValidClientType: Función que valida la structura del código del cliente...
    Public Function ValidClientType(ByVal sTypeVal As String) As Boolean

        If nPerson_typ = 2 Then
            bNatural = False
        Else
            bNatural = True
        End If
        ValidClientType = True

    End Function

    '% GetNewClientCode: Busca un código del cliente
    Public Function GetNewClientCode() As String
		Dim lrecreaClientCodeNew As eRemoteDB.Execute
		Dim lstrClient As String
		
		lrecreaClientCodeNew = New eRemoteDB.Execute
		
		lstrClient = "0"
		
		With lrecreaClientCodeNew
			.StoredProcedure = "reaClientCodeNew"
			.Parameters.Add("sClient", lstrClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Run(False)
			GetNewClientCode = Trim(.Parameters("sClient").Value)
		End With
		
		'UPGRADE_NOTE: Object lrecreaClientCodeNew may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaClientCodeNew = Nothing
		
	End Function
	
	'% GetRUT: Esta función valida el código del cliente
	Public Function GetRUT(ByVal sClient As String) As String
		Dim llngSummary As Integer
		Dim llngFactor As Integer
		Dim llngIndex As Integer
		Dim llngRUT As Integer
		
		GetRUT = ""
		llngFactor = 2
		llngSummary = 0
		
		For llngIndex = Len(sClient) To 1 Step -1
			If llngFactor = 8 Then
				llngFactor = 2
			End If
			llngSummary = llngSummary + CDbl(Mid(sClient, llngIndex, 1)) * llngFactor
			llngFactor = llngFactor + 1
		Next llngIndex
		
		llngRUT = llngSummary Mod 11
		llngRUT = 11 - llngRUT
		Select Case llngRUT
			Case 11
				GetRUT = "0"
			Case 10
				GetRUT = "K"
			Case Is < 10
				GetRUT = CStr(llngRUT)
		End Select
	End Function
	
	'% UpdateBC001J: Funcion que realiza la actualización de los campos de la tabla client dependiendo del código de cliente
    Public Function UpdateBC001J(ByVal sClient As String, ByVal dInpdate As Date, ByVal sCliename As String, ByVal sLegalname As String, ByVal nSpeciality As Integer, ByVal sCredit_card As String, ByVal dBirthdat As Date, ByVal nEmpl_qua As Integer, ByVal nInvoicing As Integer, ByVal sBill_ind As String, ByVal sBlockade As String, ByVal nComp_Type As Integer, ByVal sPEP As String, ByVal sUsperson As String) As Boolean
        Dim lrecupdClientBC001J As eRemoteDB.Execute

        lrecupdClientBC001J = New eRemoteDB.Execute
        Dim lrecClient As Client
        Dim lclsClientWin As ClientWin
        Dim lclsClientSf As Client_SF

        On Error GoTo UpdateBC001J_Err
        lrecClient = New Client
        lrecClient.Find(sClient, True)
        If lrecClient.sPEP <> sPEP Or lrecClient.sUsPerson <> sUsperson Then

            'Se registra en la tabla CLIENT_SF
            lclsClientSf = New Client_SF
            lclsClientSf.Find(sClient, Today)
            If lrecClient.sPEP <> sPEP Then
                lclsClientSf.UpdClient_SF(2)
            End If
            If lrecClient.sUsPerson <> sUsperson Then
                lclsClientSf.UpdClient_SF(3)
            End If

            lclsClientWin = New ClientWin
            Call lclsClientWin.insUpdClient_win(sClient, "BC007P", "1", , , nUsercode)
            'UPGRADE_NOTE: Object lclsClientWin may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
            lclsClientWin = Nothing
        End If
        lrecClient = Nothing

        lrecupdClientBC001J = New eRemoteDB.Execute

        On Error GoTo UpdateBC001J_Err

        UpdateBC001J = False

        With lrecupdClientBC001J
            .StoredProcedure = "updClientBC001J"
            .Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            'UPGRADE_WARNING: IsEmpty was upgraded to IsNothing and has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
            If IsNothing(Trim(CStr(dInpdate))) Then
                'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
                .Parameters.Add("dInpdate", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            Else
                .Parameters.Add("dInpdate", dInpdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            End If
            .Parameters.Add("sCliename", sCliename, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 60, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sLegalname", sLegalname, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 60, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nSpeciality", nSpeciality, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sCredit_card", sCredit_card, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            'UPGRADE_WARNING: IsEmpty was upgraded to IsNothing and has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
            If IsNothing(Trim(CStr(dBirthdat))) Then
                'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
                .Parameters.Add("dBirthdat", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            Else
                .Parameters.Add("dBirthdat", dBirthdat, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            End If
            .Parameters.Add("nEmpl_qua", nEmpl_qua, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nInvoicing", IIf(nInvoicing = 0, eRemoteDB.Constants.intNull, nInvoicing), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sBill_ind", sBill_ind, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sBlockade", sBlockade, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nComp_Type", nComp_Type, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sPEP", sPEP, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sUsPerson", sUsperson, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            UpdateBC001J = .Run(False)
        End With

        'UPGRADE_NOTE: Object lrecupdClientBC001J may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecupdClientBC001J = Nothing

UpdateBC001J_Err:
        If Err.Number Then
            UpdateBC001J = False
        End If
    End Function

    '% UpdateBC001N: Funcion que realiza la actualización de los campos de la tabla client
    '%               dependiendo del código de cliente
    Public Function UpdateBC001N(ByVal sClient As String, ByVal dInpdate As Date, ByVal sCuit As String, ByVal sLastName As String, ByVal sFirstName As String, ByVal nCivilsta As Integer, ByVal sSexclien As String, ByVal nTitle As Integer, ByVal nNationality As Integer, ByVal nSpeciality As Integer, ByVal dBirthdat As Date, ByVal dDriverdat As Date, ByVal sLicense As String, ByVal sCredit_card As String, ByVal sBlockade As String, ByVal dDeathdat As Date, ByVal sLastname2 As String, ByVal nArea As Integer, ByVal dDrivexpdat As Date, ByVal nTypDriver As Integer, ByVal nLimitdriv As Integer, ByVal nHealth_org As Integer, ByVal nAfp As Integer, ByVal dWedd As Date, ByVal sBill_ind As String, ByVal dRetirement As Date, ByVal dIndependant As Date, ByVal dDependant As Date,
                                 ByVal sSmoking As String, ByVal sFatca As String, ByVal sPEP As String, ByVal sUsPerson As String, ByVal nUsercode As Integer, ByVal sCRS As String) As Boolean
        Dim lrecupdClientBC001N As eRemoteDB.Execute
        Dim org As Integer
        Dim lrecClient As Client
        Dim lclsClientWin As ClientWin
        Dim lclsClientSf As Client_SF

        On Error GoTo UpdateBC001N_err
        lrecClient = New Client
        lrecClient.Find(sClient, True)
        If lrecClient.sPEP <> sPEP Or lrecClient.sUsPerson <> sUsPerson Or lrecClient.sCRS <> sCRS Then

            'Se registra en la tabla CLIENT_SF
            lclsClientSf = New Client_SF
            lclsClientSf.Find(sClient, Today)
            If lrecClient.sPEP <> sPEP Then
                lclsClientSf.UpdClient_SF(2)
            End If
            If lrecClient.sUsPerson <> sUsPerson Then
                lclsClientSf.UpdClient_SF(3)
            End If
            If lrecClient.sCRS <> sCRS Then
                lclsClientSf.UpdClient_SF(3)
            End If

            lclsClientWin = New ClientWin
            Call lclsClientWin.insUpdClient_win(sClient, "BC007P", "1", , , nUsercode)
            'UPGRADE_NOTE: Object lclsClientWin may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
            lclsClientWin = Nothing
        End If

        lrecClient = Nothing


        lrecupdClientBC001N = New eRemoteDB.Execute


        UpdateBC001N = False

        With lrecupdClientBC001N
            .StoredProcedure = "updClientBC001N"
            .Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dInpdate", dInpdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sCuit", sCuit, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sLastname", sLastName, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sFirstname", sFirstName, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCivilsta", nCivilsta, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
            .Parameters.Add("sSexclien", IIf(sSexclien = "0", System.DBNull.Value, sSexclien), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nTitle", nTitle, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nNationality", nNationality, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nSpeciality", nSpeciality, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dBirthdat", dBirthdat, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dDriverdat", dDriverdat, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sLicense", sLicense, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 10, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sCredit_card", sCredit_card, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sBlockade", sBlockade, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dDeathdat", dDeathdat, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sLastname2", sLastname2, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nArea", nArea, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dDrivexpdat", dDrivexpdat, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nTypdriver", nTypDriver, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nLimitdriv", nLimitdriv, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nHealth_org", nHealth_org, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nAfp", nAfp, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dWedd", dWedd, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sBill_ind", sBill_ind, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            '+ [APV2] Certificado 24 Movimiento Anual cuentas de APV por RUT
            .Parameters.Add("dRetirement", dRetirement, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dIndependant", dIndependant, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dDependant", dDependant, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sSmoking", sSmoking, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sfatca", sFatca, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sPEP", sPEP, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sUsPerson", sUsPerson, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sCRS", sCRS, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            UpdateBC001N = .Run(False)
        End With

        'UPGRADE_NOTE: Object lrecupdClientBC001N may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecupdClientBC001N = Nothing

UpdateBC001N_err:
        If Err.Number Then
            UpdateBC001N = False
        End If

        On Error GoTo 0
    End Function

    '% UpdateBC007M: Actualiza la tabla de clientes
    Public Function UpdateBC007M(ByVal sClient As String, ByVal nEconomic_l As Integer, ByVal nHouse_type As Integer, ByVal nQ_child As Integer, ByVal nQ_cars As Integer, ByVal nClass As Integer) As Boolean
		
		Dim lrecupdClientBC007M As eRemoteDB.Execute
		
		On Error GoTo UpdateBC007M_err
		lrecupdClientBC007M = New eRemoteDB.Execute
		
		UpdateBC007M = False
		
		With lrecupdClientBC007M
			.StoredProcedure = "updClientBC007M"
			.Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nEconomic_l", nEconomic_l, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nHouse_type", nHouse_type, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nClass", nClass, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nQ_child", nQ_child, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nq_cars", nQ_cars, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			UpdateBC007M = .Run(False)
		End With
		
		'UPGRADE_NOTE: Object lrecupdClientBC007M may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecupdClientBC007M = Nothing
		
UpdateBC007M_err: 
		If Err.Number Then
			UpdateBC007M = False
		End If
    End Function


    '% UpdateBC007P: Actualiza la tabla de clientes_sf
    Public Function UpdateBC007P(ByVal sClient As String, ByVal sDigit As String, ByVal ntypepop As Integer, ByVal dGrantDate As Date, ByVal dEndDate As Date) As Boolean

        Dim lrecupdClientBC007P As eRemoteDB.Execute

        On Error GoTo UpdateBC007P_err
        lrecupdClientBC007P = New eRemoteDB.Execute

        UpdateBC007P = False

        With lrecupdClientBC007P
            .StoredProcedure = "updClientBC007P"
            .Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sDigit", sDigit, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("ntypepop", ntypepop, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dGrantDate", dGrantDate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEndDate", dEndDate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            UpdateBC007P = .Run(False)
        End With

        'UPGRADE_NOTE: Object lrecupdClientBC007P may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecupdClientBC007P = Nothing

UpdateBC007P_err:
        If Err.Number Then
            UpdateBC007P = False
        End If
    End Function
	
	'% UpdateBC007S: Función que actualiza la tabla de clientes
	Public Function UpdateBC007S(ByVal sClient As String, ByVal nWeight As Double, ByVal nHeight As Double) As Boolean
		Dim lrecupdClientBC007S As eRemoteDB.Execute
		
		On Error GoTo UpdateBC007S_err
		lrecupdClientBC007S = New eRemoteDB.Execute
		
		UpdateBC007S = False
		With lrecupdClientBC007S
			.StoredProcedure = "updClientBC007S"
			.Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nWeight", nWeight, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nHeight", nHeight, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 4, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			UpdateBC007S = .Run(False)
		End With
		
		'UPGRADE_NOTE: Object lrecupdClientBC007S may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecupdClientBC007S = Nothing
		
UpdateBC007S_err: 
		If Err.Number Then
			UpdateBC007S = False
		End If
	End Function
	
	'% UpdateBC801: Función que actualiza la tabla de clientes con campos de invalidez
	Public Function UpdateBC801(ByVal sClient As String, ByVal nDisability As Integer, ByVal nIncapacity As Integer, ByVal dIncapacity As Date, ByVal nIncap_cod As Integer, ByVal nUsercode As Integer) As Boolean
		Dim lrecupdClientBC801 As eRemoteDB.Execute
		
		On Error GoTo UpdateBC801_err
		
		lrecupdClientBC801 = New eRemoteDB.Execute
		
		UpdateBC801 = False
		With lrecupdClientBC801
			.StoredProcedure = "updClientBC801"
			.Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDisability", nDisability, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nIncapacity", nIncapacity, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dIncapacity", dIncapacity, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nIncap_cod", nIncap_cod, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			UpdateBC801 = .Run(False)
		End With
		
UpdateBC801_err: 
		If Err.Number Then
			UpdateBC801 = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecupdClientBC801 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecupdClientBC801 = Nothing
	End Function
	
	'% UpdateBC003_k: Función que actualiza la tabla de clientes con campos de invalidez
	Public Function UpdateBC003_k(ByVal sClient As String, ByVal nPerson_typ As Integer, ByVal nUsercode As Integer) As Boolean
		Dim lrecupdClientBC003_k As eRemoteDB.Execute
		
		On Error GoTo UpdateBC003_k_err
		
		lrecupdClientBC003_k = New eRemoteDB.Execute
		
		UpdateBC003_k = False
		With lrecupdClientBC003_k
			.StoredProcedure = "updClientBC003_k"
			.Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPerson_typ", nPerson_typ, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			UpdateBC003_k = .Run(False)
		End With
		
UpdateBC003_k_err: 
		If Err.Number Then
			UpdateBC003_k = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecupdClientBC003_k may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecupdClientBC003_k = Nothing
	End Function
	'% UpdateNoteNum: Actualiza el numero de nota
	Public Function UpdateNoteNum(ByVal nNumnote As Integer) As Boolean
		Dim lrecupdClientNote As eRemoteDB.Execute
		
		lrecupdClientNote = New eRemoteDB.Execute
		
		UpdateNoteNum = False
		With lrecupdClientNote
			.StoredProcedure = "updClientNote"
			.Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nNotenum", nNumnote, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				UpdateNoteNum = True
				nNotenum = nNumnote
			End If
		End With
		
		'UPGRADE_NOTE: Object lrecupdClientNote may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecupdClientNote = Nothing
		
	End Function
	
	'% UpdateImageNum: Actualiza el numero de la imagen
	Public Function UpdateImageNum(ByVal nNumimage As Integer) As Boolean
		Dim lrecupdClientImage As eRemoteDB.Execute
		
		lrecupdClientImage = New eRemoteDB.Execute
		
		UpdateImageNum = False
		With lrecupdClientImage
			.StoredProcedure = "updClientImage"
			.Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nImageNum", nNumimage, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				UpdateImageNum = True
				nImageNum = nNumimage
			End If
		End With
		
		'UPGRADE_NOTE: Object lrecupdClientImage may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecupdClientImage = Nothing
		
	End Function
	
	'% ClearFields: Funcion que limpia las variables de la clase y le asigna un constante que indica
	'%              que la variable semeja un valor null
	Public Function ClearFields() As Boolean
		ClearFields = True
		sClient = String.Empty
		sAccount_in = String.Empty
		nSpeciality = eRemoteDB.Constants.intNull
		dAprobdate = eRemoteDB.Constants.dtmNull
		dBirthdat = eRemoteDB.Constants.dtmNull
		sBlockade = String.Empty
		nCivilsta = eRemoteDB.Constants.intNull
		nClass = eRemoteDB.Constants.intNull
		sCliename = String.Empty
		sFirstName = String.Empty
		sLastName = String.Empty
		dDeathdat = eRemoteDB.Constants.dtmNull
		dDriverdat = eRemoteDB.Constants.dtmNull
		nHeight = eRemoteDB.Constants.intNull
		nHouse_type = eRemoteDB.Constants.intNull
		dInpdate = eRemoteDB.Constants.dtmNull
		sLicense = String.Empty
		nNationality = eRemoteDB.Constants.intNull
		nNotenum = eRemoteDB.Constants.intNull
		nOffice = eRemoteDB.Constants.intNull
		nQ_cars = eRemoteDB.Constants.intNull
		nQ_child = eRemoteDB.Constants.intNull
		nRate = eRemoteDB.Constants.intNull
		sTax_code = String.Empty
		sSexclien = String.Empty
		sSmoking = String.Empty
        sCuit = String.Empty
        nTitle = eRemoteDB.Constants.intNull
		nWeight = eRemoteDB.Constants.intNull
		sAuto_char = String.Empty
		sCredit_card = String.Empty
		nEconomic_l = eRemoteDB.Constants.intNull
		nEmpl_qua = eRemoteDB.Constants.intNull
		nInvoicing = eRemoteDB.Constants.intNull
		nImageNum = eRemoteDB.Constants.intNull
		sLastname2 = String.Empty
		nArea = eRemoteDB.Constants.intNull
		dDrivexpdat = eRemoteDB.Constants.dtmNull
		nTypDriver = eRemoteDB.Constants.intNull
		nLimitdriv = eRemoteDB.Constants.intNull
		nHealth_org = eRemoteDB.Constants.intNull
		nAfp = eRemoteDB.Constants.intNull
		dWedd = eRemoteDB.Constants.dtmNull
		sBill_ind = String.Empty
		dIndependant = eRemoteDB.Constants.dtmNull
		dDependant = eRemoteDB.Constants.dtmNull
        sLegalname = String.Empty
    End Function
	
	'% Add: Inserta un cliente en la tabla client
	Public Function Add(ByVal sClientCode As String) As Boolean
		Dim lreccreClientCode As eRemoteDB.Execute
		
		lreccreClientCode = New eRemoteDB.Execute
		
		With lreccreClientCode
			.StoredProcedure = "creClientCode"
			.Parameters.Add("sClient", sClientCode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPerson_typ", IIf(nPerson_typ <> 2, 1, nPerson_typ), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sDigit", sDigit, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Add = .Run(False)
		End With
		'UPGRADE_NOTE: Object lreccreClientCode may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lreccreClientCode = Nothing
		If Add Then
			ClearFields()
			sClient = sClientCode
		End If
	End Function
	
	'% ValidateClientStruc: Esta función valida el código del cliente
	Public Function ValidateClientStruc(ByVal sClientCode As String) As Boolean
		ValidateClientStruc = True
		
		If Not IsNumeric(sClientCode) Then
			ValidateClientStruc = False
		Else
			sClient = sClientCode
		End If
	End Function
	
	'% AddressClient: Esta función valida que el cliente tenga al menos una direccion asociada.
	Public Function AddressClient(ByVal sClient As String) As Boolean
		Dim lrecreaAddres_Cli As eRemoteDB.Execute
		
		lrecreaAddres_Cli = New eRemoteDB.Execute
		
		With lrecreaAddres_Cli
			.StoredProcedure = "reaAddres_Cli"
			.Parameters.Add("nRecowner", 2, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				AddressClient = True
				.RCloseRec()
			End If
		End With
		'UPGRADE_NOTE: Object lrecreaAddres_Cli may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaAddres_Cli = Nothing
	End Function
	
	'% ReadClientTaxSituat: Esta función valida que el cliente tenga información impositiva registrada
	'%                      y recupera la misma
	Public Function ReadClientTaxSituat(ByVal sClient As String) As Boolean
		Dim lclsTaxSituat As TaxSituat
		
		lclsTaxSituat = New TaxSituat
		
		If lclsTaxSituat.ReadTaxSituat(sClient) Then
			ReadClientTaxSituat = True
		Else
			ReadClientTaxSituat = False
		End If
		'UPGRADE_NOTE: Object lclsTaxSituat may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsTaxSituat = Nothing
	End Function
	
	'%Class_Initialize: Inicialización de las variables públicas
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		Call ClearFields()
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
	
	'%ExpandCode: Esta function se encarga de tomar el codigo de cliente (Pasado como parametro)
	'%            y expandirlo para la estructura definida
	Public Function ExpandCode(ByVal sClientCode As String) As String
		ExpandCode = String.Empty
		
		If sClientCode <> String.Empty Then
			ExpandCode = New String("0", 14 - Len(sClientCode)) & sClientCode
		End If
	End Function
	
	'% AddClient: Agrega un nuevo cliente a la tabla Client
	Public Function AddClient() As Boolean
		Dim lreccreClient As eRemoteDB.Execute
		
		lreccreClient = New eRemoteDB.Execute
		
		With lreccreClient
			.StoredProcedure = "creClient"
			.Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sAccount_in", sAccount_in, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nInvoicing", nInvoicing, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nSpeciality", nSpeciality, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCivilsta", nCivilsta, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dAprobdate", dAprobdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dBirthdat", dBirthdat, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sBlockade", sBlockade, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nClass", nClass, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sCliename", sCliename, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 63, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sFirstname", sFirstName, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sLastname", sLastName, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dDeathdat", dDeathdat, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dDriverdat", dDriverdat, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nHeight", nHeight, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 4, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dInpdate", dInpdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nHouse_type", nHouse_type, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sLicense", sLicense, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 10, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nNotenum", nNotenum, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nOffice", nOffice, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nQ_cars", nQ_cars, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nQ_child", nQ_child, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nRate", nRate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 9, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sTax_code", sTax_code, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sSexclien", sSexclien, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sSmoking", sSmoking, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sCuit", sCuit, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTitle", nTitle, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nWeight", nWeight, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sAuto_char", sAuto_char, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sCredit_card", sCredit_card, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nEconomic_l", nEconomic_l, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nEmpl_qua", nEmpl_qua, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nImagenum", nImageNum, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPerson_typ", nPerson_typ, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nArea", nArea, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dDrivexpdat", dDrivexpdat, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTypdriver", nTypDriver, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDisability", nDisability, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nLimitdriv", nLimitdriv, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sLegalname", sLegalname, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 60, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sLastname2", sLastname2, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nHealth_org", nHealth_org, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAfp", nAfp, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dWedd", dWedd, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sBill_ind", sBill_ind, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nIncapacity", nIncapacity, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dIncapacity", dIncapacity, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nIncap_cod", nIncap_cod, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nNationality", nNationality, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sDigit", sDigit, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			AddClient = .Run(False)
		End With
		
		'UPGRADE_NOTE: Object lreccreClient may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lreccreClient = Nothing
	End Function
	
	'% Delete: Elimina un cliente de la tabla Client
	Public Function Delete() As Boolean
		
		'- Se define la variable lrecdelClient
		Dim lrecdelClient As eRemoteDB.Execute
		
		lrecdelClient = New eRemoteDB.Execute
		On Error GoTo Delete_Err
		
		With lrecdelClient
			.StoredProcedure = "delClient"
			.Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Delete = .Run(False)
		End With
		
		'UPGRADE_NOTE: Object lrecdelClient may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecdelClient = Nothing
		
Delete_Err: 
		If Err.Number Then
			Delete = False
		End If
		On Error GoTo 0
	End Function
	
	'% Delete: Elimina un cliente de todas las tablas asociadas
	Public Function Delete_All() As Boolean
		
		'- Se define la variable lrecdelClient
		Dim lrecdelClient As eRemoteDB.Execute
		
		lrecdelClient = New eRemoteDB.Execute
		On Error GoTo Delete_All_Err
		
		With lrecdelClient
			.StoredProcedure = "delClient_All"
			.Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Delete_All = .Run(False)
		End With
		
		'UPGRADE_NOTE: Object lrecdelClient may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecdelClient = Nothing
		
Delete_All_Err: 
		If Err.Number Then
			Delete_All = False
		End If
		On Error GoTo 0
	End Function
	
	
	'% FindClientName: Devuelve el nombre de un cliente dado un código de cliente
	Public Function FindClientName(ByVal sClientCode As String, Optional ByVal bFind As Boolean = False) As Boolean
		
		'- Se declara la variable que determina el resultado de la funcion (True/False)
		Static lblnRead As Boolean
		
		'- Se define la variable lrecreaClientName
		Dim lrecreaClientName As eRemoteDB.Execute
		lrecreaClientName = New eRemoteDB.Execute
		
		On Error GoTo FindClientName_Err
		
		If sClient <> sClientCode Or bFind Then
			sClient = sClientCode
			
			With lrecreaClientName
				.StoredProcedure = "reaClientName"
				.Parameters.Add("sClient", sClientCode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				
				If .Run Then
					sClient = .FieldToClass("sClient")
					sCliename = .FieldToClass("sCliename")
					sFirstName = .FieldToClass("sFirstname")
					sLastName = .FieldToClass("sLastname")
					sLastname2 = .FieldToClass("sLastName2")
					
					lblnRead = True
					.RCloseRec()
				Else
					lblnRead = False
				End If
			End With
		End If
		FindClientName = lblnRead
		'UPGRADE_NOTE: Object lrecreaClientName may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaClientName = Nothing
		
FindClientName_Err: 
		If Err.Number Then
			FindClientName = False
		End If
		On Error GoTo 0
	End Function
	
	'% Find_CA033: Permite buascar la descripción de un cliente y/o intermediario
	Public Function Find_CA033(ByVal sClient As String, ByVal sIntermed As String) As Collection
		
		Dim lclsClient As Object
		Dim lrecreaClientCA033 As eRemoteDB.Execute
		
		lrecreaClientCA033 = New eRemoteDB.Execute
		
		Find_CA033 = New Collection
		
		With lrecreaClientCA033
			.StoredProcedure = "reaClientCA033"
			.Parameters.Add("sClient1", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 40, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sClient2", sIntermed, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 40, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				Do While Not .EOF
					lclsClient = eRemoteDB.NetHelper.CreateClassInstance("eClient.Client")
					lclsClient.sClient = .FieldToClass("sClient")
					lclsClient.sCliename = .FieldToClass("sCliename")
					Find_CA033.Add(lclsClient)
					'UPGRADE_NOTE: Object lclsClient may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
					lclsClient = Nothing
					.RNext()
				Loop 
				.RCloseRec()
			End If
		End With
		'UPGRADE_NOTE: Object lrecreaClientCA033 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaClientCA033 = Nothing
	End Function
	
	'%insValBCC001: Esta rutina se encarga de hacer la validación de los campos del grid cuando
	'%              es introducida una consulta.
	Public Function insValBCC001(ByVal nPerson_typ As Integer, ByVal sClient As String, ByVal sCliename As String, ByVal tctLastname As String, ByVal tctLastname2 As String, ByVal dBirthdat As Date, ByVal sSexclien As String) As String
		
		Dim lclsErrors As eFunctions.Errors
		
		On Error GoTo insvalBCC001_Err
		lclsErrors = New eFunctions.Errors
		
		If sClient = String.Empty And sCliename = String.Empty And tctLastname = String.Empty And tctLastname2 = String.Empty Then
			Call lclsErrors.ErrorMessage("BCC001", 1068)
        End If

        Dim rx As New System.Text.RegularExpressions.Regex("^[A-Z0-9 a-z %]*$")
        If Not rx.IsMatch(sClient) OrElse
           Not rx.IsMatch(sCliename) OrElse
           Not rx.IsMatch(tctLastname) OrElse
           Not rx.IsMatch(tctLastname2) Then

            Call lclsErrors.ErrorMessage("BCC001", 1948)

        End If

        insValBCC001 = lclsErrors.Confirm

insvalBCC001_Err:
        If Err.Number Then
            insValBCC001 = insValBCC001 & Err.Description
        End If
        On Error GoTo 0

        'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsErrors = Nothing
    End Function

    '% insPreBC001: se valida la carga de los datos en la página
    Public Function insPreBC001(ByVal sClient As String, Optional ByVal sReloadAction As String = "", Optional ByVal dInpdate As Date = #12:00:00 AM#, Optional ByVal sCliename As String = "", Optional ByVal nOcupat As Integer = 0, Optional ByVal dBirthDate As Date = #12:00:00 AM#, Optional ByVal sBlockadeJ As String = "", Optional ByVal sLegalname As String = "", Optional ByVal nEmpl_qua As Integer = 0, Optional ByVal nInvoicing As Integer = 0, Optional ByVal sBill_ind As String = "", Optional ByVal nComp_Type As Integer = 0, Optional ByVal sUsPerson As String = "", Optional ByVal sPEP As String = "") As Boolean
        Dim lclsOpt_system As eGeneral.Opt_system

        On Error GoTo insPreBC001_Err

        '+ Si no se está recargando la página

        If sReloadAction = String.Empty Then
            If Find(sClient) Then
                insPreBC001 = True
            End If
        Else
            insPreBC001 = True
            Me.dInpdate = dInpdate
            Me.sCliename = sCliename
            Me.nSpeciality = nOcupat
            Me.dBirthdat = dBirthDate
            Me.sBlockade = sBlockadeJ
            Me.sLegalname = sLegalname
            Me.nEmpl_qua = nEmpl_qua
            Me.nInvoicing = nInvoicing
            Me.sBill_ind = sBill_ind
            Me.nComp_Type = nComp_Type
            Me.sUsPerson = sUsPerson
            Me.sPEP = sPEP
        End If

insPreBC001_Err:
        If Err.Number Then
            insPreBC001 = False
        End If
    End Function

    '%insValBC006_k: Esta función se encarga de validar los datos introducidos
    Public Function insValBC006_k(ByVal sCodispl As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal npolicy As Double, ByVal ncertif As Double) As String
		Dim lclsErrors As eFunctions.Errors
		Dim lclsClient As eClient.Client
		Dim lobjPolicy As Object
		Dim lobjCertificat As Object
		Dim gstrTypeCompany As eType
		On Error GoTo insValBC006_k_Err
		lclsErrors = New eFunctions.Errors
		lclsClient = New eClient.Client
		lobjPolicy = eRemoteDB.NetHelper.CreateClassInstance("ePolicy.Policy")
		lobjCertificat = eRemoteDB.NetHelper.CreateClassInstance("ePolicy.Certificat")
		'+Se valida el campo Ramo
		If nBranch = eRemoteDB.Constants.intNull Then
			Call lclsErrors.ErrorMessage(sCodispl, 1022)
		End If
		'+Se valida el campo Producto
		If nProduct = eRemoteDB.Constants.intNull Then
			Call lclsErrors.ErrorMessage(sCodispl, 3635)
		End If
		'+ Se valida que el campo Póliza este lleno
		If npolicy = eRemoteDB.Constants.intNull Then
			Call lclsErrors.ErrorMessage(sCodispl, 3003)
		Else
			If lobjPolicy.ValExistPolicyRec(nBranch, nProduct, npolicy, IIf(gstrTypeCompany = eType.cstrBrokerOrBrokerageFirm, "C", String.Empty)) Then
				If lobjPolicy.nNullcode <> eRemoteDB.Constants.intNull Then
					Call lclsErrors.ErrorMessage(sCodispl, 3098)
				End If
				If lobjPolicy.sStatus_pol = "3" Then
					Call lclsErrors.ErrorMessage(sCodispl, 3720)
					'+Si se trata de una póliza colectiva o multilocalidad
				Else
					If lobjPolicy.sPolitype <> "1" Then
						If ncertif <> eRemoteDB.Constants.intNull Then
							If Not lobjCertificat.Find("2", nBranch, nProduct, npolicy, ncertif) Then
								Call lclsErrors.ErrorMessage(sCodispl, 3010)
							Else
								If lobjCertificat.nNullcode <> eRemoteDB.Constants.intNull Then
									Call lclsErrors.ErrorMessage(sCodispl, 3099)
								End If
								If lobjCertificat.sStatusva = "3" Then
									Call lclsErrors.ErrorMessage(sCodispl, 3883)
								End If
							End If
						Else
							Call lclsErrors.ErrorMessage(sCodispl, 3006)
						End If
					End If
				End If
			Else
				Call lclsErrors.ErrorMessage(sCodispl, 3001)
			End If
		End If
		insValBC006_k = lclsErrors.Confirm
		
insValBC006_k_Err: 
		If Err.Number Then
			insValBC006_k = insValBC006_k & Err.Description
		End If
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
		'UPGRADE_NOTE: Object lclsClient may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsClient = Nothing
		'UPGRADE_NOTE: Object lobjPolicy may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lobjPolicy = Nothing
		'UPGRADE_NOTE: Object lobjCertificat may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lobjCertificat = Nothing
		
		On Error GoTo 0
	End Function
	
	'% insPostBC006: Esta función se encaga de validar todos los datos introducidos
	Public Function insPostBC006(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal npolicy As Double, ByVal ncertif As Double, ByVal sClient As String, ByVal nRole As Integer, ByVal nUser As Integer, ByVal sClientCode As String, ByVal nSelection As Integer) As Boolean
		On Error GoTo insPostBC006_Err
		
		insPostBC006 = Me.insChangeClient(nBranch, nProduct, npolicy, ncertif, sClient, nRole, nUser, sClientCode, nSelection)
		
insPostBC006_Err: 
		If Err.Number Then
			insPostBC006 = False
		End If
		On Error GoTo 0
	End Function
	
	'% insChangeClient: Esta rutina realiza la distribución de actualizaciones a relizar a los
	'%                  diferentes archivos que contienen el cliente que se desea cambiar.
	Public Function insChangeClient(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal npolicy As Double, ByVal ncertif As Double, ByVal sClient As String, ByVal nRole As Integer, ByVal nUser As Integer, ByVal sClientCode As String, ByVal nSelection As Integer) As Boolean
        'Dim llngCount As Integer
        Dim lstrHolder As String = ""
        Dim lclsProduct As eProduct.Product
		Dim lclsRoles As Object
		Dim lclsPolicy As Object
		Dim lclsCertificat As Object
		Dim lclsPremium As Object
		Dim lclsPolicy_his As Object
		Dim lblnResult As Boolean
		lclsProduct = New eProduct.Product
		lclsRoles = eRemoteDB.NetHelper.CreateClassInstance("ePolicy.Roles")
		lclsPolicy = eRemoteDB.NetHelper.CreateClassInstance("ePolicy.Policy")
		lclsCertificat = eRemoteDB.NetHelper.CreateClassInstance("ePolicy.Certificat")
		lclsPremium = eRemoteDB.NetHelper.CreateClassInstance("eCollection.Premium")
		lclsPolicy_his = eRemoteDB.NetHelper.CreateClassInstance("ePolicy.Policy_his")
		
		lblnResult = False
		
		If lclsProduct.Find(nBranch, nProduct, Today) Then
			If lclsProduct.sHolder = "1" Then
				lstrHolder = CStr(2)
			Else
				lstrHolder = CStr(1)
			End If
		End If
		If nSelection = 1 Then
			'+ Actualización de Roles
			'+ Aqui primero Find y si consigue Update sino Add
			With lclsRoles
				.sCertype = "2"
				.nBranch = nBranch
				.nProduct = nProduct
				.npolicy = npolicy
				.ncertif = ncertif
				.sClient = sClient
				.nRole = nRole
				.dEffecdate = Today
				.nUsercode = nUser
				.nIntermed = eRemoteDB.Constants.intNull
				lblnResult = .UpdateClientCode(sClientCode, nRole)
			End With
			If lblnResult Then
				If nRole = CDbl(lstrHolder) Then
					With lclsPolicy_his
						.sCertype = "2"
						.nBranch = nBranch
						.nProduct = nProduct
						.npolicy = npolicy
						.ncertif = ncertif
						.nCurrency = eRemoteDB.Constants.intNull
						.dEffecdate = Today
						.dNulldate = eRemoteDB.Constants.dtmNull
						.sNull_move = String.Empty
						.nReceipt = eRemoteDB.Constants.intNull
						.nTransactio = eRemoteDB.Constants.intNull
						.nType = 52
						.nUsercode = nUser
						.nClaim = eRemoteDB.Constants.intNull
						.nMovement = eRemoteDB.Constants.intNull
						.dLedgerdat = eRemoteDB.Constants.dtmNull
						lblnResult = .Update
					End With
					If lblnResult Then
						'+ Actualización de cliente en la póliza
						lblnResult = lclsPolicy.UpdateClientPolicy("2", nBranch, nProduct, npolicy, sClientCode, nUser)
					End If
					'+ Actualización de cliente en el certificado
					If lblnResult Then
						lblnResult = lclsCertificat.UpdateClientCertificat("2", nBranch, nProduct, npolicy, ncertif, sClientCode, nUser)
					End If
					If lblnResult Then
						With lclsPremium
							.sCertype = "2"
							.nBranch = nBranch
							.nProduct = nProduct
							.npolicy = npolicy
							.sClient = sClientCode
							.nUsercode = nUser
							lblnResult = .UpdateClientPremium
						End With
					End If
				End If
			End If
		End If
		'UPGRADE_NOTE: Object lclsPolicy_his may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsPolicy_his = Nothing
		'UPGRADE_NOTE: Object lclsPremium may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsPremium = Nothing
		'UPGRADE_NOTE: Object lclsCertificat may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsCertificat = Nothing
		'UPGRADE_NOTE: Object lclsPolicy may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsPolicy = Nothing
		'UPGRADE_NOTE: Object lclsRoles may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsRoles = Nothing
		'UPGRADE_NOTE: Object lclsProduct may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsProduct = Nothing
		
		insChangeClient = lblnResult
		
	End Function
	
	'% insValBC006: Esta función se encarga de validar los datos introducidos
	Public Function insValBC006(ByVal sCodispl As String, ByVal nSelection As Integer, ByVal sClient As String, ByVal sOldClient As String) As String
		Dim lclsErrors As eFunctions.Errors
		Dim lvclTime As eClient.ValClient
		Dim lclsClient As eClient.Client
		Dim ludtDeathBlock As eDeathBlock
		On Error GoTo insValBC006_Err
		lclsErrors = New eFunctions.Errors
		lvclTime = New eClient.ValClient
		lclsClient = New eClient.Client
		
		'+Se valida que se haya seleccionado un elemento en el arreglo
		If nSelection = 0 Then
			Call lclsErrors.ErrorMessage(sCodispl, 2790)
		End If
		'+Caso de no haber introducido el código del nuevo cliente
		If Not lvclTime.Validate(Trim(sClient), eFunctions.Menues.TypeActions.clngActionUpdate) Then
			Select Case lvclTime.Status
				Case valClient.eTypeValClientErr.StructInvalid
					Call lclsErrors.ErrorMessage(sCodispl, 2012)
				Case valClient.eTypeValClientErr.FieldEmpty
					Call lclsErrors.ErrorMessage(sCodispl, 2001)
				Case valClient.eTypeValClientErr.TypeNotFound
					If Trim(sClient) <> String.Empty Then
						Call lclsErrors.ErrorMessage(sCodispl, 2013)
					End If
			End Select
		Else
			ludtDeathBlock = lclsClient.ValBlockDeath(sClient)
			If ludtDeathBlock = eDeathBlock.Death Then
				Call lclsErrors.ErrorMessage(sCodispl, 2051)
			End If
			If ludtDeathBlock = eDeathBlock.Blockeade Then
				Call lclsErrors.ErrorMessage(sCodispl, 2052)
			End If
			If ludtDeathBlock = eDeathBlock.BlockDeath Then
				Call lclsErrors.ErrorMessage(sCodispl, 2076)
			End If
			If sOldClient = sClient And nSelection = 1 Then
				Call lclsErrors.ErrorMessage(sCodispl, 2205)
			End If
		End If
		insValBC006 = lclsErrors.Confirm
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
		'UPGRADE_NOTE: Object lvclTime may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lvclTime = Nothing
		'UPGRADE_NOTE: Object lclsClient may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsClient = Nothing
insValBC006_Err: 
		If Err.Number Then
			insValBC006 = insValBC006 & Err.Description
		End If
		On Error GoTo 0
	End Function
	'% insValBC668_K: Esta función se encarga de validar los datos introducidos
	Public Function insValBC668_K(ByVal nSelection As Integer, ByVal sClient As String, ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal npolicy As Double, ByVal ncertif As Double) As String
		Dim lclsErrors As eFunctions.Errors
		Dim lclsCertificat As Object
		Dim lclsPolicy As Object
		
		On Error GoTo insValBC668_K_Err
		
		lclsErrors = New eFunctions.Errors
		lclsCertificat = eRemoteDB.NetHelper.CreateClassInstance("ePolicy.Certificat")
		lclsPolicy = eRemoteDB.NetHelper.CreateClassInstance("ePolicy.Policy")
		
		'+Valida que el rut tenga valor
		If sClient = String.Empty Then
			Call lclsErrors.ErrorMessage("BC668_K", 2001)
		Else
			If Not Find(sClient) Then
				Call lclsErrors.ErrorMessage("BC668_K", 2044)
			End If
		End If
		
		'+Si se indica ramo el producto y  la poliza de tener valor
		If nBranch <> eRemoteDB.Constants.intNull Then
			If nProduct = eRemoteDB.Constants.intNull Then
				Call lclsErrors.ErrorMessage("BC668_K", 1014)
			End If
			If npolicy = eRemoteDB.Constants.intNull Then
				Call lclsErrors.ErrorMessage("BC668_K", 55623)
			Else
				If sCertype = "2" Then
					Call lclsPolicy.Find(sCertype, nBranch, nProduct, npolicy)
					If (lclsPolicy.sStatus_pol <> 1 And lclsPolicy.sStatus_pol <> 4 And lclsPolicy.sStatus_pol <> 5) Then
						Call lclsErrors.ErrorMessage("BC668_K", 55958)
					End If
				Else
					Call lclsCertificat.Find(sCertype, nBranch, nProduct, npolicy, ncertif)
					If lclsCertificat.nStatquota <> 1 Then
						Call lclsErrors.ErrorMessage("BC668_K", 55958)
					End If
				End If
			End If
		End If
		
		'+Si se indica certificado debe pertenecer a la poliza
		If npolicy <> eRemoteDB.Constants.intNull And ncertif <> eRemoteDB.Constants.intNull Then
			If Not lclsCertificat.Find(sCertype, nBranch, nProduct, npolicy, ncertif) Then
				Call lclsErrors.ErrorMessage("BC668_K", 55624)
			End If
		End If
		
		insValBC668_K = lclsErrors.Confirm
		
insValBC668_K_Err: 
		If Err.Number Then
			insValBC668_K = insValBC668_K & Err.Description
		End If
		
		On Error GoTo 0
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
	End Function
	
	'% ValBlockDeath: Función que valida que el cliente no esté bloqueado o muerto
	Public Function ValBlockDeath(ByVal strCodClient As String) As eDeathBlock
		
		Dim lclsClient As eClient.Client
		
		lclsClient = New eClient.Client
		
		ValBlockDeath = True
		
		With lclsClient
			If .Find(strCodClient) Then
				'+Esta muerto
				If Not (.dDeathdat = eRemoteDB.Constants.dtmNull) Then
					ValBlockDeath = eDeathBlock.Death
				End If
				'+Esta bloqueado
				If CStr(.sBlockade) = "1" Then
					ValBlockDeath = eDeathBlock.Blockeade
				End If
				'+Todas las anteriores
				If Not (.dDeathdat = eRemoteDB.Constants.dtmNull) And CStr(.sBlockade) = "1" Then
					ValBlockDeath = eDeathBlock.BlockDeath
				End If
			End If
		End With
		
		'UPGRADE_NOTE: Object lclsClient may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsClient = Nothing
	End Function
	
	'% LoadTabsDocument: Esta función es la encarga de carga la información necesaria para cada
	'%                   pestaña que sera mostrada para la secuencia de documentos del cliente.
	Public Function LoadTabsDocument(ByVal sClient As String, ByVal bQuery As Boolean) As String
		Dim lclsQuery As eRemoteDB.Query
		Dim lclsEval_master As eval_masters
		
		Dim lclsSequence As eFunctions.Sequence
		Dim lintPageImage As eFunctions.Sequence.etypeImageSequence
		
		
		'- Se define la variable que indica la existencia de las ventanas de la secuencia
		Dim lintCount As Integer
		Dim lintAux As Integer
		Dim lstrHTMLCode As String
		Dim lintAction As Integer
		
		Dim lvntAux As Object
		
		'- Se define la variable lstrCodispl en la cual se almacena el código de la ventana
		'  extraído de la constante cstrWindows
		Dim lstrCodispl As String
		
		On Error GoTo LoadTabsDocument_err
		
		lclsQuery = New eRemoteDB.Query
		lclsSequence = New eFunctions.Sequence
		
		
		lintAction = IIf(bQuery, eFunctions.Menues.TypeActions.clngActionQuery, eFunctions.Menues.TypeActions.clngActionUpdate)
		
		lintAux = 1
		
		lstrHTMLCode = lclsSequence.makeTable
		
		'+Se lee los documentos del cliente
		Call Find_Eval_Doc(sClient)
		
		For lintCount = 1 To CN_PAGNUM
			
			'+Se extrae el código de la ventana
			
			lstrCodispl = Trim(Mid(CN_WINDOWS, lintAux, 8))
			lintAux = lintAux + 8
			
			Call lclsQuery.OpenQuery("Windows", "sCodisp, sCodispl, sShort_des", "sCodispl='" & lstrCodispl & "'")
			
			'+ Se obtiene por cada transacción un campo (requerido) de la misma
			'+ para identificar si tiene o no contenido
			Select Case lstrCodispl
				
				'+Campo obligatorio para la transacción (BC803)
				Case "BC802"
					lvntAux = nVal_nev
					'+Campo obligatorio para la transacción (BC803)
				Case "BC803"
					lvntAux = nVal_doc
				Case Else
					'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
					lvntAux = System.DBNull.Value
			End Select
			
			lintPageImage = eFunctions.Sequence.etypeImageSequence.eEmpty
			
			'+ Se asigna la imagen asociada a la página asociada al Codispl
			
			'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
            If IsDBNull(lvntAux) OrElse lvntAux = eRemoteDB.Constants.intNull Then
                '+Ventanas sin contenido
                lintPageImage = eFunctions.Sequence.etypeImageSequence.eEmpty
            Else
                '+Ventanas con contenido
                lintPageImage = eFunctions.Sequence.etypeImageSequence.eOK
            End If
			
			lstrHTMLCode = lstrHTMLCode & lclsSequence.makeRow(lclsQuery.FieldToClass("sCodisp"), lstrCodispl, lintAction, lclsQuery.FieldToClass("sShort_des"), lintPageImage)
		Next lintCount
		
		LoadTabsDocument = lstrHTMLCode & lclsSequence.closeTable()
		
		'UPGRADE_NOTE: Object lclsQuery may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsQuery = Nothing
		'Set lclsGen_cover = Nothing
		'UPGRADE_NOTE: Object lclsSequence may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsSequence = Nothing
		
LoadTabsDocument_err: 
		If Err.Number Then
			LoadTabsDocument = "LoadTabsDocument: " & Err.Description
		End If
		On Error GoTo 0
	End Function
	
	'% Find_Eval_Doc: Esta función es la encarga de buscar si existe informacion en las tablas.
	'%                eval_master - doc_req_cli
	Public Function Find_Eval_Doc(ByVal sClient As String) As Object
		
		Dim lrecreaVal_eval_doc As eRemoteDB.Execute
		Dim lclsreaVal_eval_doc As Client
		
		On Error GoTo reaVal_eval_doc_Err
		lrecreaVal_eval_doc = New eRemoteDB.Execute
		
		With lrecreaVal_eval_doc
			.StoredProcedure = "reaVal_eval_doc"
			.Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run(True) Then
				Find_Eval_Doc = True
				nVal_nev = .FieldToClass("L_val_nev", eRemoteDB.Constants.intNull)
				nVal_doc = .FieldToClass("L_val_doc", eRemoteDB.Constants.intNull)
			Else
				Find_Eval_Doc = False
			End If
		End With
		
reaVal_eval_doc_Err: 
		If Err.Number Then
			Find_Eval_Doc = False
		End If
		'UPGRADE_NOTE: Object lrecreaVal_eval_doc may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaVal_eval_doc = Nothing
		On Error GoTo 0
	End Function

    '% Find_Eval_Cliente: Esta función valida datos del cliente 
    Public Function Validate_Client(ByVal sClient As String, ByVal nAction As Integer, _
                                    ByVal nUsercode As String, ByVal dEffecdate As String, _
                                    ByRef sMessajeRet As String) As Boolean

        Dim lrecValidate_Client As eRemoteDB.Execute

        On Error GoTo Validate_Client_Err
        lrecValidate_Client = New eRemoteDB.Execute
        Validate_Client = False

        With lrecValidate_Client
            .StoredProcedure = "INSVALCLIENTUSER"
            .Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nAction", nAction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("SMESSAJE", sMessajeRet, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 200, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            If .Run(False) Then
                Validate_Client = True
                sMessajeRet = Trim(.Parameters("SMESSAJE").Value)
            Else
                Validate_Client = False
            End If
        End With

Validate_Client_Err:
        If Err.Number Then
            Validate_Client = False
        End If
        lrecValidate_Client = Nothing
        On Error GoTo 0
    End Function

    Public Function Find_GM_Siniestrado(ByVal nClaim As Double) As Boolean
        Dim lrecinsReaCover_a As eRemoteDB.Execute
        'Dim lclsLife_Claim As Life_claim
        Dim lclsCl_client As Client
        Dim a As Object

        Dim lblnIncapacity As Boolean

        On Error GoTo Find_SI007_GM_Err

        Find_GM_Siniestrado = False
        lrecinsReaCover_a = New eRemoteDB.Execute

        With lrecinsReaCover_a
            .StoredProcedure = "INSREASINIESTRADO"
            .Parameters.Add("nClaim", nClaim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            '.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            '.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            '.Parameters.Add("nModulec", nModulec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            '.Parameters.Add("nCover", nCover, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)



            If .Run Then
                Find_GM_Siniestrado = True
                lclsCl_client = New Client
                With lclsCl_client

                    sClient = lrecinsReaCover_a.FieldToClass("sClient")
                    dBirthdat = lrecinsReaCover_a.FieldToClass("DBIRTHDAT")
                    sCliename = lrecinsReaCover_a.FieldToClass("SCLIENAME")

                End With

                .RCloseRec()
            End If
        End With

        lrecinsReaCover_a = Nothing
        'lclsLife_Claim = Nothing
        'lclsCl_cover = Nothing

Find_SI007_GM_Err:
        If Err.Number Then
            Find_GM_Siniestrado = False
        End If
        On Error GoTo 0
    End Function
    Public Function Find_GM_Reclamante(ByVal nClaim As Double) As Boolean
        Dim lrecinsReaCover_a As eRemoteDB.Execute
        'Dim lclsLife_Claim As Life_claim
        Dim lclsCl_client As Client
        Dim a As Object

        Dim lblnIncapacity As Boolean

        On Error GoTo Find_SI007_GM_Err

        Find_GM_Reclamante = False
        lrecinsReaCover_a = New eRemoteDB.Execute

        With lrecinsReaCover_a
            .StoredProcedure = "INSREARECLAMANTE"
            .Parameters.Add("nClaim", nClaim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            If .Run Then
                Find_GM_Reclamante = True
                lclsCl_client = New Client
                'With lclsCl_client

                sClient = lrecinsReaCover_a.FieldToClass("sClient")
                sSmoking = lrecinsReaCover_a.FieldToClass("sDescript")
                sCliename = lrecinsReaCover_a.FieldToClass("SCLIENAME")

                ' End With

                .RCloseRec()
            End If
        End With

        lrecinsReaCover_a = Nothing
        'lclsLife_Claim = Nothing
        'lclsCl_cover = Nothing

Find_SI007_GM_Err:
        If Err.Number Then
            Find_GM_Reclamante = False
        End If
        On Error GoTo 0
    End Function


    Public Function Find_GM_Beneficiario(ByVal nClaim As Double) As Boolean
        Dim lrecinsReaCover_a As eRemoteDB.Execute
        Dim lclsCl_client As Client
        Dim a As Object

        Dim lblnIncapacity As Boolean

        On Error GoTo Find_SI007_GM_Err

        Find_GM_Beneficiario = False
        lrecinsReaCover_a = New eRemoteDB.Execute

        With lrecinsReaCover_a
            .StoredProcedure = "INSREABENEFICIARIO"
            .Parameters.Add("nClaim", nClaim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            If .Run Then
                Find_GM_Beneficiario = True
                lclsCl_client = New Client
                'With lclsCl_client

                sClient = lrecinsReaCover_a.FieldToClass("sClient")
                sSmoking = lrecinsReaCover_a.FieldToClass("sDescript")
                sCliename = lrecinsReaCover_a.FieldToClass("SCLIENAME")

                ' End With

                .RCloseRec()
            End If
        End With

        lrecinsReaCover_a = Nothing
        'lclsLife_Claim = Nothing
        'lclsCl_cover = Nothing

Find_SI007_GM_Err:
        If Err.Number Then
            Find_GM_Beneficiario = False
        End If
        On Error GoTo 0
    End Function


    '% Update_UserAmend: Actualiza el usuario que está modificando la póliza
    Public Function Update_ClientPEP() As Boolean
        Dim lrecUpdate_ClientPEP As eRemoteDB.Execute

        lrecUpdate_ClientPEP = New eRemoteDB.Execute

        On Error GoTo Update_UserAmend_Err

        '+ Definición de parámetros para stored procedure 'insudb.Update_ClientPEP'
        '+ Información leída el 06/11/2000 02:37:39 p.m.

        With lrecUpdate_ClientPEP
            .StoredProcedure = "UPDCLIENTPEPBC007P"
            .Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sPEP", sPEP, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            Update_ClientPEP = .Run(False)
        End With

        lrecUpdate_ClientPEP = Nothing

Update_UserAmend_Err:
        If Err.Number Then
            Update_ClientPEP = False
        End If
        On Error GoTo 0
    End Function

End Class






