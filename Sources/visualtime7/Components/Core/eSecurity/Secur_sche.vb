Option Strict Off
Option Explicit On
Public Class Secur_sche
	'%-------------------------------------------------------%'
	'% $Workfile:: Secur_sche.cls                           $%'
	'% $Author:: Mvazquez                                   $%'
	'% $Date:: 14/03/06 19:35                               $%'
	'% $Revision:: 3                                        $%'
	'%-------------------------------------------------------%'
	
	Const Actived As Short = 1
	
	'   Column_name                     Type         Computed   Length      Prec  Scale Nullable    TrimTrailingBlanks   FixedLenNullInSource
	'   ------------------------------- ------------ ---------- ----------- ----- ----- ----------- -------------------- -----------------------------------
	Public sSche_code As String 'char        no         6                       no          no                   no
	Public nAccesof As Integer 'smallint    no         2           5     0     yes         (n/a)                (n/a)
	Public dDate_from As Date 'datetime    no         8                       yes         (n/a)                (n/a)
	Public dDate_to As Date 'datetime    no         8                       yes         (n/a)                (n/a)
	Public nInd_curren As Integer 'smallint    no         2           5     0     yes         (n/a)                (n/a)
	Public nInd_limits As Integer 'smallint    no         2           5     0     yes         (n/a)                (n/a)
	Public sLongdesc As String 'char        no         30                      yes         no                   yes
	Public nSecurlev As Integer 'smallint    no         2           5     0     yes         (n/a)                (n/a)
	Public sShortdes As String 'char        no         12                      yes         no                   yes
	Public sStatregt As String 'char        no         1                       yes         no                   yes
	Public sTime1_from As String 'char        no         5                       yes         no                   yes
	Public sTime1_to As String 'char        no         5                       yes         no                   yes
	Public sTime2_from As String 'char        no         5                       yes         no                   yes
	Public sTime2_to As String 'char        no         5                       yes         no                   yes
	Public sTimeq1_fro As String 'char        no         5                       yes         no                   yes
	Public sTimeq1_to As String 'char        no         5                       yes         no                   yes
	Public sTimeq2_fro As String 'char        no         5                       yes         no                   yes
	Public sTimeq2_to As String 'char        no         5                       yes         no                   yes
	Public sUsequery As String 'char        no         1                       yes         no                   yes
	Public nInd_conce As Integer 'smallint    no         2           5     0     yes         (n/a)                (n/a)
	Public nUsercode As Integer 'smallint    no         2           5     0     yes         (n/a)                (n/a)
	Public nDuration As Integer
	Public nDaysAdv As Integer
	
	Public sScheCode As String
	Public OldSchema As String
	
	
	'**-Levels
	'-Niveles
	Public sCode_mt As String
	Public nAmelevel As Integer
	Public nInqlevel As Integer
	Public sSupervis As String
	Public sPermitted As String
	Public sInd_Type As String
	Public sDescCode_mt As String
	
	'**-Limits
	'-Limites
	Public nCurrency As Integer
	Public nBranch As Integer
	Public nProduct As Integer
	Public nClaim_dec As Double
	Public nClaim_pay As Double
	Public nIssuelimit As Double
	Public sStatregtLim As String
	
	'**-Office
	'-Oficina
	Public nOffice As Integer
	Public sInd_inqu As String
	Public sInd_upda As String
	Public sStatregtOff As String
	Public sDesOffice As String
	
	'**-Currency
	'-Moneda
	Public nCurrencyCur As Integer
	Public sStatregtCur As String
	
	'**-Concept
	'-Concepto
	
	Public nConcept As Integer
	Public sStatregtCon As String
	Public nValidOffice As Byte
	
	'-Niveles por transacción
	Public sCodispl As String
	Public nTransac As Integer
	Public sDesc_tx As String
	
	Public sWithInformation As String
	'**-Properties definition to used in SG014 - Currency approve in a scheme.
	'-Se definen las propiedades utilizadas en SG014 - Monedas autorizadas en un esquema.
	
	Public nSel As Integer
	
	'-Solo premisos para consultar
	Public mblnOnlyQuery As Boolean
	
	'**-Table indicators that state if it is about all of the default values or if it's based on the list
	'**-that was loaded in the related table
	'**-If a change that impacts the references, please put it in this enumerated list List=2 and All=1
	'-Indicadores de tabla para decir si se trata de todos los valores por defecto o si es según la lista
	'-cargada en la tabla relacionada
	'-Si se llega a hacer un cambio que implique referencias favor colocar en esta lista enumerada List=2 y All=1
	
	Public Enum eTypeList
		All = 1
		List = 2
	End Enum
	
	Public Enum eTypeCode
		Module_Renamed = 1
		Window = 2
	End Enum
	
	'**-Type of tables associated to the security module
	'-Tipo de tablas asociadas al módulo de seguridad
	
	Public Enum eTypeTable
		Levels
		Off_acc
		Schema_Cur
		Schema_pcon
		Limits
	End Enum
	
	Public Enum eTypeLimit
		nClaim_d
		nClaim_p
		nIssuelim
	End Enum
	
	'**-Defined type for the security levels of the transactions or modules
	'-Tipo defindo para los niveles de seguridad de las transacciones o módulos
	
	Private Structure udtLevels
		Dim sInd_Type As String
		Dim sCode_mt As String
		Dim nAmelevel As Integer
		Dim nInqlevel As Integer
		Dim sSupervis As String
		Dim sPermitted As String
		Dim sDescCode_mt As String
	End Structure
	
	'**-Defined type for the security levels of the branch office
	'-Tipo defindo para los niveles de seguridad de las sucursales
	
	Private Structure udtOficce
		Dim nOffice As Integer
		Dim sInd_inqu As String
		Dim sInd_upda As String
		Dim sStatregt As String
		Dim sDesOffice As String
	End Structure
	
	'**-Defined type for the security levels of the declaration limits
	'-Tipo defindo para los niveles de seguridad de los límites de declaración
	
	Private Structure udtLimits
		Dim nCurrency As Integer
		Dim nBranch As Integer
		Dim nClaim_d As Double
		Dim nClaim_p As Double
		Dim nIssuelim As Double
		Dim sStatregt As String
		Dim nProduct As Integer
	End Structure
	
	'**-Defined type for the security levels of the currencies allowed
	'-Tipo defindo para los niveles de seguridad de las monedas permitidas
	
	Private Structure udtCurrency
		Dim nCurrency As Integer
		Dim sStatregt As String
	End Structure
	
	'**-Defined type for the security levels of the payment concepts allowed
	'-Tipo defindo para los niveles de seguridad de los conceptos de pagos permitidos
	
	Private Structure udtSche_pcon
		Dim nConcept As Integer
		Dim sStatregt As String
	End Structure
	
	'**-Definition of the structure that contains the types of existance limits
	'-Definición de la estructura que contiene los tipos de límites existentes.
	
	Enum eTypeLimits
		'**-Limit of the policy suscription
		'-Límite de la suscripción de la póliza
		clngLimitsPolicySus = 1
		'**-Limit of the claim declaration
		'-Límite de declaración del siniestro.
		clngLimitsClaimDec = 2
		'**-Limit of the claim payment.
		'-Límite de pago del siniestro.
		clngLimitsClaimPay = 3
	End Enum
	
	'**-Dimensional arrangement according to the defined type by the user for each table
	'-Arreglos dimensionar según los tipos definidos por el usuario para cada tabla
	
	Private arrLevels() As udtLevels
	Private arrOffice() As udtOficce
	Private arrLimits() As udtLimits
	Private arrCurrency() As udtCurrency
	Private arrSche_Pcon() As udtSche_pcon
	
	
	Private bFind As Boolean
	'**-The auxiliaries properties are defined to be used in the page SG013 - General information of the scheme
	'-Se definen las propiedades auxiliares a ser utilizadas en la ventana SG013 - Información general del esquema.
	
	Private mstrSche_code As String
	'**-The auxiliaries properties are defined to be used in the page SG013_k - Security scheme.
	'-Se deifnen las propiedades auxiliares a ser utilizadas en la ventana SG013_k - Esquema de seguridad.
	
	
	
	
	
	'**%Find: This method returns TRUE or FALSE depending if the records exists in the table "Secur_sche"
	'%Find: Este metodo retorna VERDADERO o FALSO dependiendo de la existencia o no de registros en la
	'%tabla "Secur_sche"
	Public Function Find(ByVal sSche_code As String, Optional ByVal ReadAll As Boolean = False) As Boolean
		Dim lrecvalSchema As eRemoteDB.Execute
		
		On Error GoTo ErrorHandler
		
		If Me.sSche_code <> sSche_code Then
			
			'+En caso de que el 'Schema' ya este cargado en la session entonces, el mismo se carga al DLL y no se ejecuta
			'+ el StoredProcedure.
            If Not ReadAll AndAlso Not eRemoteDB.ServiceEnviroment.isServiceConsumer Then
                If GetSchemaSession(sSche_code) Then
                    Find = True
                    Exit Function
                End If
            End If
			'+Definición de parámetros para stored procedure 'insudb.valSchema'
			'+Información leída el 25/06/1999 09:47:49 AM
			lrecvalSchema = New eRemoteDB.Execute
			With lrecvalSchema
				.StoredProcedure = "valSchema"
				.Parameters.Add("sSche_Code", sSche_code, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 6, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				If .Run Then
					dDate_from = .FieldToClass("dDate_from")
					dDate_to = .FieldToClass("dDate_to")
					nAccesof = .FieldToClass("nAccesof")
					nInd_conce = .FieldToClass("nInd_conce")
					nInd_curren = .FieldToClass("nInd_curren")
					nInd_limits = .FieldToClass("nInd_limits")
					nSecurlev = .FieldToClass("nSecurlev")
					sStatregt = .FieldToClass("sStatregt")
					sTime1_from = .FieldToClass("sTime1_from")
					sTime1_to = .FieldToClass("sTime1_to")
					sTime2_from = .FieldToClass("sTime2_from")
					sTime2_to = .FieldToClass("sTime2_to")
					sTimeq1_to = .FieldToClass("sTimeq1_to")
					sTimeq1_fro = .FieldToClass("sTimeq1_fro")
					sTimeq2_fro = .FieldToClass("sTimeq2_fro")
					sTimeq2_to = .FieldToClass("sTimeq2_to")
					sUsequery = .FieldToClass("sUsequery")
					nDuration = .FieldToClass("nDuration")
					nDaysAdv = .FieldToClass("nDaysAdv")
					If ReadAll Then
						If nInd_limits = 2 Then
							FindLimits(sSche_code)
						End If
						If nInd_curren = 2 Then
							FindSchema_cur(sSche_code)
						End If
						If nAccesof = 2 Then
							FindOff_acc(sSche_code)
						End If
						If nInd_conce = 2 Then
							FindSche_pcon(sSche_code)
						End If
						If nSecurlev = 2 Then
							FindLevels(sSche_code)
						End If
					End If
					.RCloseRec()
					Me.sSche_code = sSche_code
					Find = True
				End If
			End With

            If Not eRemoteDB.ServiceEnviroment.isServiceConsumer Then
                '+Una vez que se encuentra toda la información del 'Schema' la misma es almacenda en la Session.
                Dim session As New eRemoteDB.ASPSupport
                session.SetASPSessionValue("sXMLSchema", XMLStream_SecurSche(, True))
            End If
        Else
            Find = True
        End If
        lrecvalSchema = Nothing

        Exit Function
ErrorHandler:
        lrecvalSchema = Nothing
        ProcError("Secur_sche.Find(sSche_code,ReadAll)", New Object() {sSche_code, ReadAll})
    End Function
	
	'**%FindOtherScheParts_Err: reads the associated information with part of a security schema
	'**%according to the indications that are loaded from that part of the schema
	'%FindOtherScheParts_Err: lee la información asociada con una parte de un esquema de
	'%seguridad según se indique cargando los datos de dicha parte del esquema
	Private Function FindOtherScheParts(ByVal sSche_code As String, ByVal NameTable As eTypeTable) As Boolean
		Dim lstrScheCode As String
		
		On Error GoTo FindOtherScheParts_Err
		
		FindOtherScheParts = Find(sSche_code)
		
		If FindOtherScheParts Then
			lstrScheCode = sSche_code
		Else
			Exit Function
		End If
		
		If OldSchema <> lstrScheCode Then
			OldSchema = lstrScheCode
			Select Case NameTable
				Case eTypeTable.Levels
					bFind = FindLevels(lstrScheCode)
				Case eTypeTable.Limits
					bFind = FindLimits(lstrScheCode)
				Case eTypeTable.Off_acc
					bFind = FindOff_acc(lstrScheCode)
				Case eTypeTable.Schema_Cur
					bFind = FindSchema_cur(lstrScheCode)
				Case eTypeTable.Schema_pcon
					bFind = FindSche_pcon(lstrScheCode)
			End Select
		End If
		FindOtherScheParts = bFind
		
FindOtherScheParts_Err: 
		If Err.Number Then
			FindOtherScheParts = False
		End If
		On Error GoTo 0
	End Function
	
	'**%FindLevels: Reads the related information with the security levels
	'**%to restrict to the user from the use of the certain transactions
	'%FindLevels: lee la información relacionada con los niveles de seguridad
	'%para restringir al usuario sobre el uso de ciertas transacciones
	Public Function FindLevels(Optional ByRef sSche_code As String = "", Optional ByVal nRow As Short = eRemoteDB.Constants.intNull) As Boolean
		Dim lrecreaLevels As eRemoteDB.Execute
		Dim lstrScheCode As String
		Dim lbytIndex As Byte
		
		On Error GoTo FindLevels_Err
		
		lstrScheCode = IIf(sSche_code > String.Empty, sSche_code, sScheCode)
		
		lrecreaLevels = New eRemoteDB.Execute
		
		FindLevels = True
		
		'**+Parameters definiton to stored procedure 'insudb.reaLevels'
		'**+Data read on 06/25/1999 09:50:49 AM
		'+Definición de parámetros para stored procedure 'insudb.reaLevels'
		'+Información leída el 25/06/1999 09:50:49 AM
		
		With lrecreaLevels
			.StoredProcedure = "reaLevels_SG002"
			.Parameters.Add("sSche_code", lstrScheCode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 6, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nRow", nRow, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				ReDim Preserve arrLevels(50)
				lbytIndex = 0
				While Not .EOF
					arrLevels(lbytIndex).nAmelevel = .FieldToClass("nAmelevel", 0)
					arrLevels(lbytIndex).nInqlevel = .FieldToClass("nInqlevel", 0)
					arrLevels(lbytIndex).sCode_mt = .FieldToClass("sCode_mt", String.Empty)
					arrLevels(lbytIndex).sInd_Type = .FieldToClass("sInd_type", String.Empty)
					arrLevels(lbytIndex).sPermitted = .FieldToClass("sPermitted", String.Empty)
					arrLevels(lbytIndex).sSupervis = .FieldToClass("sSupervis", String.Empty)
					arrLevels(lbytIndex).sDescCode_mt = .FieldToClass("sDescCode_mt", String.Empty)
					.RNext()
					lbytIndex = lbytIndex + 1
				End While
				.RCloseRec()
				ReDim Preserve arrLevels(lbytIndex - 1)
			Else
				FindLevels = False
			End If
		End With
		'UPGRADE_NOTE: Object lrecreaLevels may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaLevels = Nothing
		
FindLevels_Err: 
		If Err.Number Then
			FindLevels = False
		End If
		On Error GoTo 0
	End Function
	
	'**%FindLimits: Reads the information related with the security levels to restrict
	'**%the user against the use of the declarations and issues amounts
	'%FindLimits: lee la información relacionada con los niveles de seguridad para restringir
	'%al usuario sobre el uso de montos de declaraciones y emisiones
	Private Function FindLimits(Optional ByRef sSche_code As String = "") As Boolean
		Dim lrecreaLimitsSG003 As eRemoteDB.Execute
		Dim lstrScheCode As String
		Dim lbytIndex As Byte
		lrecreaLimitsSG003 = New eRemoteDB.Execute
		
		On Error GoTo FindLimits_Err
		
		lstrScheCode = IIf(sSche_code > String.Empty, sSche_code, sScheCode)
		
		FindLimits = True
		
		'**+Parameters defintion to stored procedure ' insudb.reaLimitSG003'
		'**+Data read on 01/21/2000 15:07:58
		'+Definición de parámetros para stored procedure 'insudb.reaLimitsSG003'
		'+Información leída el 21/01/2000 15:07:58
		
		With lrecreaLimitsSG003
			.StoredProcedure = "reaLimitsSG003"
			.Parameters.Add("sSche_code", lstrScheCode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 6, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				ReDim Preserve arrLimits(50)
				lbytIndex = 0
				While Not .EOF
					arrLimits(lbytIndex).nBranch = .FieldToClass("nBranch")
					arrLimits(lbytIndex).nClaim_d = .FieldToClass("nClaim_d")
					arrLimits(lbytIndex).nClaim_p = .FieldToClass("nClaim_p")
					arrLimits(lbytIndex).nCurrency = .FieldToClass("nCurrency")
					arrLimits(lbytIndex).nIssuelim = .FieldToClass("nIssuelim")
					arrLimits(lbytIndex).sStatregt = .FieldToClass("sStatregt")
					arrLimits(lbytIndex).nProduct = .FieldToClass("nProduct")
					.RNext()
					lbytIndex = lbytIndex + 1
				End While
				.RCloseRec()
				ReDim Preserve arrLimits(lbytIndex - 1)
			Else
				FindLimits = False
			End If
		End With
		'UPGRADE_NOTE: Object lrecreaLimitsSG003 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaLimitsSG003 = Nothing
		
FindLimits_Err: 
		If Err.Number Then
			FindLimits = False
		End If
		On Error GoTo 0
	End Function
	
	'**%FindOff_acc: Reads the related information to the security levels
	'**%to restrict the user against the use of certain branch offices
	'%FindOff_acc: lee la información relacionada con los niveles de seguridad
	'%para restringir al usuario sobre el uso de ciertas sucursales
	Private Function FindOff_acc(Optional ByRef sSche_code As String = "") As Boolean
		Dim lrecreaOff_accSG017 As eRemoteDB.Execute
		Dim lstrScheCode As String
		Dim lbytIndex As Byte
		
		On Error GoTo FindOff_acc_Err
		
		lstrScheCode = IIf(sSche_code > String.Empty, sSche_code, sScheCode)
		lrecreaOff_accSG017 = New eRemoteDB.Execute
		
		FindOff_acc = True
		
		'**+Parameters Definition to stored procedure 'insudb.reaOff_accSG017'
		'**+Data read on 01/21/2000 14:45:22
		'+Definición de parámetros para stored procedure 'insudb.reaOff_accSG017'
		'+Información leída el 21/01/2000 14:45:22
		
		With lrecreaOff_accSG017
			.StoredProcedure = "reaOff_accSG017"
			.Parameters.Add("sSche_code", lstrScheCode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 6, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				ReDim Preserve arrOffice(50)
				lbytIndex = 0
				While Not .EOF
					arrOffice(lbytIndex).nOffice = .FieldToClass("nOffice", 0)
					arrOffice(lbytIndex).sInd_inqu = .FieldToClass("sInd_inqu", String.Empty)
					arrOffice(lbytIndex).sInd_upda = .FieldToClass("sInd_upda", String.Empty)
					arrOffice(lbytIndex).sStatregt = .FieldToClass("sStatregt", String.Empty)
					arrOffice(lbytIndex).sDesOffice = .FieldToClass("sDescript", String.Empty)
					
					.RNext()
					lbytIndex = lbytIndex + 1
				End While
				.RCloseRec()
				ReDim Preserve arrOffice(lbytIndex - 1)
			Else
				FindOff_acc = False
			End If
		End With
		'UPGRADE_NOTE: Object lrecreaOff_accSG017 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaOff_accSG017 = Nothing
		
FindOff_acc_Err: 
		If Err.Number Then
			FindOff_acc = False
		End If
		On Error GoTo 0
	End Function
	
	'**%FindSchema_cur: Reads the related information with the security levels to restrict
	'**%the user against the use of certain currencies
	'%FindSchema_cur: lee la información relacionada con los niveles de seguridad para restringir
	'%al usuario sobre el uso de ciertas monedas
	Private Function FindSchema_cur(Optional ByRef sSche_code As String = "") As Boolean
		Dim lrecreaSchema_curSG014 As eRemoteDB.Execute
		Dim lstrScheCode As String
		Dim lbytIndex As Byte
		
		On Error GoTo FindSchema_cur_Err
		
		lstrScheCode = IIf(sSche_code > String.Empty, sSche_code, sScheCode)
		lrecreaSchema_curSG014 = New eRemoteDB.Execute
		FindSchema_cur = True
		
		'**+Parameters definition to stored procedure 'insudb.reaSchema_curSG014'
		'**+Data read on 01/21/2000 15:34:52
		'+Definición de parámetros para stored procedure 'insudb.reaSchema_curSG014'
		'+Información leída el 21/01/2000 15:34:52
		
		With lrecreaSchema_curSG014
			.StoredProcedure = "reaSchema_curSG014"
			.Parameters.Add("sSche_code", lstrScheCode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 6, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAction", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				ReDim Preserve arrCurrency(50)
				lbytIndex = 0
				While Not .EOF
					arrCurrency(lbytIndex).nCurrency = .FieldToClass("nCurrency", 0)
					arrCurrency(lbytIndex).sStatregt = .FieldToClass("sStatregt", String.Empty)
					.RNext()
					lbytIndex = lbytIndex + 1
				End While
				.RCloseRec()
				ReDim Preserve arrCurrency(lbytIndex - 1)
			Else
				FindSchema_cur = False
			End If
		End With
		'UPGRADE_NOTE: Object lrecreaSchema_curSG014 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaSchema_curSG014 = Nothing
		
FindSchema_cur_Err: 
		If Err.Number Then
			FindSchema_cur = False
		End If
		On Error GoTo 0
	End Function
	
	'**%FindSchema_pcon: Reads the related information with the security levels
	'**%to restrict the user from the use of certain payment concepts
	'%FindSche_pcon: lee la información relacionada con los niveles de seguridad
	'%para restringir al usuario sobre el uso de ciertos conceptos de pagos
	Private Function FindSche_pcon(Optional ByRef sSche_code As String = "") As Boolean
		Dim lrecreaSche_pconSG100 As eRemoteDB.Execute
		Dim lstrScheCode As String
		Dim lbytIndex As Byte
		
		On Error GoTo FindSche_pcon_Err
		
		lstrScheCode = IIf(sSche_code > String.Empty, sSche_code, sScheCode)
		lrecreaSche_pconSG100 = New eRemoteDB.Execute
		FindSche_pcon = True
		
		'**+Parameters defintion to stored procedure 'insudb.reaSche_pconSG100'
		'**+Data read on 01/21/2000 15:42:50
		'+Definición de parámetros para stored procedure 'insudb.reaSche_pconSG100'
		'+Información leída el 21/01/2000 15:42:50
		
		With lrecreaSche_pconSG100
			.StoredProcedure = "reaSche_pconSG100"
			.Parameters.Add("sSche_code", lstrScheCode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 6, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAction", 302, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				ReDim Preserve arrSche_Pcon(50)
				lbytIndex = 0
				While Not .EOF
					arrSche_Pcon(lbytIndex).nConcept = .FieldToClass("nConcept", 0)
					arrSche_Pcon(lbytIndex).sStatregt = .FieldToClass("sStatregt", String.Empty)
					.RNext()
					lbytIndex = lbytIndex + 1
				End While
				.RCloseRec()
				ReDim Preserve arrSche_Pcon(lbytIndex - 1)
			Else
				FindSche_pcon = False
			End If
		End With
		'UPGRADE_NOTE: Object lrecreaSche_pconSG100 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaSche_pconSG100 = Nothing
		
FindSche_pcon_Err: 
		If Err.Number Then
			FindSche_pcon = False
		End If
		On Error GoTo 0
	End Function
	
	'**%Reload: Executes the reload to load new data from a specific table
	'%Reload: realiza el reload par acargar de nuevo los datos de una tabla específica
	Public Function Reload(ByVal NameTable As eTypeTable, Optional ByVal sSche_code As String = "") As Boolean
		Reload = FindOtherScheParts(sSche_code, NameTable)
	End Function
	
	'************************************************************************************************************************
	'****************************** Functions for restores the values since the arrangement *********************************
	'****************************** Funciones para devolver los valores desde el arreglo ************************************
	'************************************************************************************************************************
	
	'**%ItemLevels: Function that gets information of a level from the array
	'%ItemLevels: Función que busca una información de un nivel en el arreglo
	Public Function ItemLevels(ByVal sSche_code As String, ByVal sInd_Type As eTypeCode, ByVal sCode_mt As String) As Boolean
		Dim lintIndex As Integer
		
		ItemLevels = False
		If Find(sSche_code) Then
			If nSecurlev <> 1 Then
				For lintIndex = 0 To UBound(arrLevels)
					If arrLevels(lintIndex).sInd_Type = CStr(sInd_Type) And arrLevels(lintIndex).sCode_mt = sCode_mt Then
						sCode_mt = arrLevels(lintIndex).sCode_mt
						nAmelevel = arrLevels(lintIndex).nAmelevel
						nInqlevel = arrLevels(lintIndex).nInqlevel
						sInd_Type = CShort(arrLevels(lintIndex).sInd_Type)
						sSupervis = arrLevels(lintIndex).sSupervis
						sPermitted = arrLevels(lintIndex).sPermitted
						sDescCode_mt = arrLevels(lintIndex).sDescCode_mt
						ItemLevels = True
						Exit For
					End If
				Next 
			End If
		End If
	End Function
	
	'**%ItemLimits: Function that gets information of a limit from the array
	'%ItemLimits: Función que busca una información de un límite en el arreglo
	Public Function ItemLimits(ByVal sSche_code As String, ByVal nCurrency As Integer, ByVal nBranch As Integer, ByVal nProduct As Integer) As Boolean
		Dim lbytIndex As Byte
		
		On Error GoTo 0
		
		ItemLimits = False
		If Find(sSche_code) Then
			If nInd_limits <> 1 Then
				On Error GoTo ItemLimits_err
				For lbytIndex = 0 To UBound(arrLimits)
					If arrLimits(lbytIndex).nCurrency = nCurrency And arrLimits(lbytIndex).nBranch = nBranch And arrLimits(lbytIndex).nProduct = nProduct Then
						nCurrency = arrLimits(lbytIndex).nCurrency
						nBranch = arrLimits(lbytIndex).nBranch
						nClaim_dec = arrLimits(lbytIndex).nClaim_d
						nClaim_pay = arrLimits(lbytIndex).nClaim_p
						nIssuelimit = arrLimits(lbytIndex).nIssuelim
						sStatregtLim = arrLimits(lbytIndex).sStatregt
						nProduct = arrLimits(lbytIndex).nProduct
						ItemLimits = True
						Exit For
					End If
				Next 
			End If
		End If
		
ItemLimits_err: 
		If Err.Number Then
			ItemLimits = True
		End If
		On Error GoTo 0
	End Function
	
	'**%FindOffice: Finds an element from an array by it's position
	'%FindOffice: Permite encontrar un elemento del arreglo por su posición
	Public Function FindOffice(ByRef lintIndex As Integer) As Boolean
		If lintIndex <= UBound(arrOffice) Then
			FindOffice = True
			nOffice = arrOffice(lintIndex).nOffice
			sInd_inqu = arrOffice(lintIndex).sInd_inqu
			sInd_upda = arrOffice(lintIndex).sInd_upda
			sStatregtOff = arrOffice(lintIndex).sStatregt
			sDesOffice = arrOffice(lintIndex).sDesOffice
		End If
	End Function
	
	'**%valLimits: This function validates if the user the exceeds the existing limits
	'**%according to the security scheme
	'%valLimits: Esta función se encarga de validar si el usuario se excede en los Límites
	'%existentes de acuerdo al esquema de seguridad.
	Public Function valLimits(ByRef llngTypeLimits As eTypeLimits, ByRef lstrSche_code As String, ByRef lintBranch As Integer, ByRef lintCurrency As Integer, ByRef lcurAmount As Decimal, ByRef lintProduct As Integer) As Boolean
		
		'**+The user has authorization to exceed the limit if:
		'**+The user has the supervisor level or,
		'**+If the user has the total limit indicator or,
		'**+If the reserve amount of the cover is null
		'+Si el usuario tiene autorización para excederse en el límite; según:
		'+Si el usuario tiene nivel de Supervisor o,
		'+Si el usuario tiene indicador de límite total o,
		'+Si el monto de la reserva de la cobertura es nula
		
		If sSupervis = "1" Or nInd_limits = 1 Or lcurAmount = 0 Then
			valLimits = True
		Else
			'**+In case there is no special privileges, the limits assinged to the user
			'**+according to the assigned security scheme are verified
			'+En caso de que no tenga privilegios especiales, se verifica los limites
			'+asignados al usuario según el esquema de seguridad asignado
			
			Select Case llngTypeLimits
				'**+Subscription limit of the policy
				'+Límite de suscripción de la póliza.
				
				Case eTypeLimits.clngLimitsPolicySus
					If ItemLimits(lstrSche_code, lintCurrency, lintBranch, lintProduct) Then
						If eTypeLimit.nIssuelim = eRemoteDB.Constants.intNull Or eTypeLimit.nIssuelim >= lcurAmount Then
							valLimits = True
						End If
					End If
					'**+Declaration limit of the claim
					'+Límite de declaración del siniestro.
					
				Case eTypeLimits.clngLimitsClaimDec
					If ItemLimits(lstrSche_code, lintCurrency, lintBranch, lintProduct) Then
						If eTypeLimit.nClaim_d = eRemoteDB.Constants.intNull Or eTypeLimit.nClaim_d >= lcurAmount Then
							valLimits = True
						End If
					End If
					'**+Payment limit of the claim
					'+Límite de pago del siniestro.
					
				Case eTypeLimits.clngLimitsClaimPay
					If ItemLimits(lstrSche_code, lintCurrency, lintBranch, lintProduct) Then
						If nClaim_pay = eRemoteDB.Constants.intNull Or nClaim_pay >= lcurAmount Then
							valLimits = True
						End If
					End If
			End Select
		End If
	End Function
	
	'**%valOffice: This function verifies that the indicated office is
	'**%authorized in the indicated security schema
	'%valOffice : Esta función se encarga de verificar que la oficina indicada se encuentre
	'%autorizada en el esquema de seguridad indicado
	Public Function valOffice(ByVal sSche_code As String, ByVal lintOffice As Integer, ByVal lintAction As Integer) As Boolean
		Dim lintAccesOf As Integer
		
		If Find(sSche_code) Then
			lintAccesOf = nAccesof
		End If
		
		If lintAccesOf = 1 Then
			valOffice = True
		Else
			If lintAction = 1 Then
				If ItemOffice(sSche_code, lintOffice) Then
					valOffice = sInd_inqu = "1"
				End If
			ElseIf lintAction = 2 Then 
				If ItemOffice(sSche_code, lintOffice) Then
					valOffice = sInd_upda = "1"
				End If
			End If
		End If
	End Function
	'**%valCurrency: This function verifies that the indicated currency is
	'**%authorized in the indicated security schema
	'%valCurrency : Esta función se encarga de verificar que la moneda indicada se encuentre
	'%autorizada en el esquema de seguridad indicado
	Public Function valCurrency(ByVal sSche_code As String, ByVal lintCurrency As Integer) As Boolean
		Dim lintInd_Curren As Integer
		
		If Find(sSche_code) Then
			lintInd_Curren = nInd_curren
		End If
		
		If lintInd_Curren = 1 Then
			valCurrency = True
		Else
			If FindSchema_cur(sSche_code) Then
				valCurrency = ItemCurrency(sSche_code, lintCurrency, True)
			End If
		End If
	End Function
	
	'**%ItemOffice: Function that gets information of an office in the array
	'%ItemOffice: Función que busca una información de una oficina en el arreglo
	Public Function ItemOffice(ByVal sSche_code As String, ByVal nOffice As Integer) As Boolean
		Dim lbytIndex As Byte
		
		ItemOffice = False
		If Find(sSche_code) Then
			If nAccesof <> 1 Then
				For lbytIndex = 0 To UBound(arrOffice)
					If arrOffice(lbytIndex).nOffice = nOffice Then
						nOffice = arrOffice(lbytIndex).nOffice
						sInd_inqu = arrOffice(lbytIndex).sInd_inqu
						sInd_upda = arrOffice(lbytIndex).sInd_upda
						sStatregtOff = arrOffice(lbytIndex).sStatregt
						sDesOffice = arrOffice(lbytIndex).sDesOffice
						ItemOffice = True
						Exit For
					End If
				Next 
			End If
		End If
	End Function
	
	'**%ItemCurrency: Function that gets information of a currency in the array
	'%ItemCurrency: Función que busca una información de una moneda en el arreglo
	Public Function ItemCurrency(ByVal sSche_code As String, ByVal nCurrency As Integer, Optional ByVal VerifyStatus As Boolean = True) As Boolean
		Dim lbytIndex As Byte
		
		ItemCurrency = False
		If Find(sSche_code) Then
			If nInd_curren <> 1 Then
				For lbytIndex = 0 To UBound(arrCurrency)
					If arrCurrency(lbytIndex).nCurrency = nCurrency Then
						If VerifyStatus Then
							If arrCurrency(lbytIndex).sStatregt = CStr(Actived) Then
								ItemCurrency = True
								nCurrencyCur = arrCurrency(lbytIndex).nCurrency
								sStatregtCur = arrCurrency(lbytIndex).sStatregt
							Else
								ItemCurrency = False
							End If
						Else
							nCurrencyCur = arrCurrency(lbytIndex).nCurrency
							sStatregtCur = arrCurrency(lbytIndex).sStatregt
							ItemCurrency = True
						End If
					End If
				Next 
			End If
		End If
	End Function
	
	'**%ItemConcepts: This function gets information of a concept in the array
	'%ItemConcepts: Función que busca una información de un concepto en el arreglo
	Public Function ItemConcepts(ByVal sSche_code As String, ByVal nConcept As Integer, Optional ByVal VerifyStatus As Boolean = True) As Boolean
		Dim lbytIndex As Byte
		
		ItemConcepts = False
		If Find(sSche_code) Then
			If nInd_conce <> 1 Then
				For lbytIndex = 0 To UBound(arrSche_Pcon)
					If arrSche_Pcon(lbytIndex).nConcept = nConcept Then
						If VerifyStatus Then
							If CDbl(arrSche_Pcon(lbytIndex).sStatregt) = Actived Then
								ItemConcepts = True
								nConcept = arrSche_Pcon(lbytIndex).nConcept
								sStatregtCon = arrSche_Pcon(lbytIndex).sStatregt
							Else
								ItemConcepts = False
							End If
						Else
							ItemConcepts = True
							nConcept = arrSche_Pcon(lbytIndex).nConcept
							sStatregtCon = arrSche_Pcon(lbytIndex).sStatregt
						End If
					End If
				Next 
			End If
		End If
	End Function
	
	'**%Class_Initialize: Controls the creation of an instance of the class
	'%Class_Initialize: Controla la creación de una instancia de la clase
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		sCode_mt = String.Empty
		nAmelevel = eRemoteDB.Constants.intNull
		nInqlevel = eRemoteDB.Constants.intNull
		sSupervis = String.Empty
		sPermitted = String.Empty
		sInd_Type = String.Empty
		nCurrency = eRemoteDB.Constants.intNull
		nBranch = eRemoteDB.Constants.intNull
		nClaim_dec = eRemoteDB.Constants.intNull
		nClaim_pay = eRemoteDB.Constants.intNull
		nIssuelimit = eRemoteDB.Constants.intNull
		sStatregtLim = String.Empty
		nOffice = eRemoteDB.Constants.intNull
		sInd_inqu = String.Empty
		sInd_upda = String.Empty
		sStatregtOff = String.Empty
		nCurrencyCur = eRemoteDB.Constants.intNull
		sStatregtCur = String.Empty
		nConcept = eRemoteDB.Constants.intNull
		sStatregtCon = String.Empty
		sSche_code = String.Empty
		nProduct = eRemoteDB.Constants.intNull
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
	
	'**%valCurrency_Schema: Validates if a currency is allowed according to the security schema that executes it
	'%valCurrency_Schema: Valida si una moneda esta permitida segun el esquema de seguridad que ejecuta
	Public Function valCurrency_Schema(ByVal nUsercode As Integer, ByVal nCurrency As Integer) As Boolean
		'**-The lrecValCurrency_Schema variable is defined
		'-Se define la variable lrecValCurrency_Schema
		Dim lrecValCurrency_Schema As eRemoteDB.Execute
		
		'**+Parameters definition to stored procedure 'insudb.ValCurrency_Schema'
		'**+Data read on 02/25/2001 17:24:12
		'+Definición de parámetros para stored procedure 'insudb.ValCurrency_Schema'
		'+Información leída el 05/02/2001 17:24:12
		On Error GoTo valCurrency_Schema_Err
		lrecValCurrency_Schema = New eRemoteDB.Execute
		With lrecValCurrency_Schema
			.StoredProcedure = "ValCurrency_Schema"
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCurrency", nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nValidCurrency", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				valCurrency_Schema = .Parameters("nValidCurrency").Value = 1
			End If
		End With
		
		'UPGRADE_NOTE: Object lrecValCurrency_Schema may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecValCurrency_Schema = Nothing
		
valCurrency_Schema_Err: 
		If Err.Number Then
			valCurrency_Schema = False
		End If
		On Error GoTo 0
	End Function
	
	'**%valOffice_Schema: Validates if the zone/branch office is allowed according the security schema of a user
	'%valOffice_Schema: Valida si una zona/sucursal esta permitida segun el esquema de seguridad de un usuario
	Public Function valOffice_Schema(ByVal intUsercode As Integer, ByVal intOffice As Integer) As Boolean
		'**-The lrecValOffice_Schema variable is defined
		'-Se define la variable lrecValOffice_Schema
		Dim lrecValOffice_Schema As eRemoteDB.Execute
		Dim lintValOffice As Short
		
		On Error GoTo valOffice_Schema_Err
		
		lrecValOffice_Schema = New eRemoteDB.Execute
		
		With lrecValOffice_Schema
			.StoredProcedure = "ValOffice_Schema"
			.Parameters.Add("nUsercode", intUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nOffice", intOffice, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nValOffice", lintValOffice, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Run(False)
			valOffice_Schema = .Parameters("nValOffice").Value = 1
			nValidOffice = valOffice_Schema
		End With
		
valOffice_Schema_Err: 
		If Err.Number Then
			valOffice_Schema = False
		End If
		'UPGRADE_NOTE: Object lrecValOffice_Schema may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecValOffice_Schema = Nothing
		On Error GoTo 0
	End Function
	
	'**%valExistSecur_sche: Validates the existence of the security schema
	'*valExistSecur_sche: Valida la existencia del esquema de seguridad.
	Public Function valExistSecur_sche(ByRef lstrShema As String) As Boolean
		Dim lrecSecur_sche As New eRemoteDB.Execute
		
		valExistSecur_sche = False
		
		On Error GoTo valExistSecur_sche_Err
		
		With lrecSecur_sche
			.StoredProcedure = "valSecur_sche"
			
			.Parameters.Add("sSche_code", lstrShema, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 6, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run Then
				If .FieldToClass("lCount") > 0 Then
					valExistSecur_sche = True
				End If
			End If
		End With
		
		'UPGRADE_NOTE: Object lrecSecur_sche may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecSecur_sche = Nothing
		
valExistSecur_sche_Err: 
		If Err.Number Then
			valExistSecur_sche = False
		End If
		
		On Error GoTo 0
	End Function
	
	'**%insValSG013_K: This method validates the header section of the page "SG013_K" as described in the
	'**%functional specifications
	'%InsValSG013_K: Este metodo se encarga de realizar las validaciones del encabezado (Header)
	'%descritas en el funcional de la ventana "SG013_K"
	Public Function insValSG013_k(ByVal sCodispl As String, ByVal nAction As String, ByVal sSche_code As String) As String
		Dim lobjErrors As eFunctions.Errors
		Dim lclsUser As eSecurity.User
		
		lobjErrors = New eFunctions.Errors
		lclsUser = New eSecurity.User
		
		insValSG013_k = String.Empty
		
		On Error GoTo insValSG013_k_Err
		'**+Validates the "schema" field
		'+ e valida el campo "Esquema".
		
		'UPGRADE_WARNING: IsEmpty was upgraded to IsNothing and has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
		'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
		If IsDbNull(sSche_code) Or IsNothing(sSche_code) Or Trim(sSche_code) = String.Empty Or Trim(sSche_code) = "0" Then
			Call lobjErrors.ErrorMessage(sCodispl, 12076)
		Else
			If Not lclsUser.reaScheCode_v(sSche_code) Then
				If nAction <> CStr(eFunctions.Menues.TypeActions.clngActionadd) Then
					Call lobjErrors.ErrorMessage(sCodispl, 12077)
				End If
			Else
				If nAction = CStr(eFunctions.Menues.TypeActions.clngActionadd) Then
					Call lobjErrors.ErrorMessage(sCodispl, 12089)
				End If
			End If
		End If
		
		insValSG013_k = lobjErrors.Confirm
		
		'UPGRADE_NOTE: Object lobjErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lobjErrors = Nothing
		
insValSG013_k_Err: 
		If Err.Number Then
			insValSG013_k = insValSG013_k & Err.Description
		End If
		
		On Error GoTo 0
	End Function
	
	'**%insPostSG013_K: This method updates the database (as described in the functional specifications)
	'**%for the page "SG013_K"
	'%insPostSG013_K: Este metodo se encarga de realizar el impacto en la base de datos (descrito en las
	'%especificaciones funcionales)de la ventana "SG013_K"
	Public Function insPostSG013_k(ByVal nAction As Integer, ByVal lstrSche_code As String, ByVal lintUsercode As Integer) As Boolean
		insPostSG013_k = True
		
		On Error GoTo insPostSG013_k_Err
		
		sSche_code = lstrSche_code
		nUsercode = lintUsercode
		'**+If the selected option is "Add".
		'+Si la opción seleccionada es "Registrar".
		
		Select Case nAction
			Case eFunctions.Menues.TypeActions.clngActionadd
				insPostSG013_k = creCodeSchema
				
				If insPostSG013_k Then
					insPostSG013_k = InsUpdIndic(sSche_code, nUsercode)
				End If
		End Select
		
insPostSG013_k_Err: 
		If Err.Number Then
			insPostSG013_k = False
		End If
		
		On Error GoTo 0
	End Function
	
	'**%creCodeSchema: Adds the data to the security schema table
	'%creCodeSchema: Se encarga de crear la información en la tabla de esquema de seguridad.
	Public Function creCodeSchema() As Boolean
		Dim lrecSchema As New eRemoteDB.Execute
		
		On Error GoTo creCodeSchema_Err
		
		With lrecSchema
			.StoredProcedure = "creCodeSche"
			
			.Parameters.Add("sSche_code", sSche_code, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 6, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			creCodeSchema = .Run(False)
		End With
		
		'UPGRADE_NOTE: Object lrecSchema may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecSchema = Nothing
		
creCodeSchema_Err: 
		If Err.Number Then
			creCodeSchema = False
		End If
		
		On Error GoTo 0
	End Function
	
	'**%insDelSchema: Deletes the records from the table "Secur_Sche"
	'%insDelSchema: Elimina los registros de la tabla "Secur_Sche"
	Public Function insDelSchema(ByVal sSche_code As String) As Boolean
		Dim lrecRecordset As New eRemoteDB.Execute
		
		On Error GoTo insDelSchema_Err
		
		With lrecRecordset
			.StoredProcedure = "delSecur_sche"
			.Parameters.Add("sSche_code", sSche_code, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 6, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			insDelSchema = .Run(False)
		End With
		
		'UPGRADE_NOTE: Object lrecRecordset may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecRecordset = Nothing
		
insDelSchema_Err: 
		If Err.Number Then
			insDelSchema = False
		End If
		
		On Error GoTo 0
	End Function
	
	'**%reaSchema: Read the general data for the schema
	'%reaSchema: Lee los datos generales para el Esquema
	Public Function reaSchema(ByVal sSche_code As String, Optional ByVal lblnFind As Boolean = True) As Boolean
		Dim lrecReaSchema As eRemoteDB.Execute
		
		On Error GoTo reaSchema_Err
		
		reaSchema = True
		
		If mstrSche_code <> sSche_code Or lblnFind Then
			lrecReaSchema = New eRemoteDB.Execute
			
			'**+Parameters defintion to stored procedure 'insudb.reaSecurSche'
			'**+Data read on 06/25/1999 09:47:49 AM
			'+Definición de parámetros para stored procedure 'insudb.reaSecurSche'
			'+Información leída el 25/06/1999 09:47:49 AM
			
			With lrecReaSchema
				.StoredProcedure = "reaSecurSche"
				.Parameters.Add("strSchema", sSche_code, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 6, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				
				If .Run Then
					sSche_code = .FieldToClass("sSche_code")
					nAccesof = .FieldToClass("nAccesof")
					dDate_from = .FieldToClass("dDate_from")
					dDate_to = .FieldToClass("dDate_to")
					nInd_curren = .FieldToClass("nInd_curren")
					nInd_limits = .FieldToClass("nInd_limits")
					sLongdesc = .FieldToClass("sLongdesc")
					nSecurlev = .FieldToClass("nSecurlev")
					sShortdes = .FieldToClass("sShortdes")
					sStatregt = .FieldToClass("sStatregt")
					sTime1_from = .FieldToClass("sTime1_from")
					sTime1_to = .FieldToClass("sTime1_to")
					sTime2_from = .FieldToClass("sTime2_from")
					sTime2_to = .FieldToClass("sTime2_to")
					sTimeq1_fro = .FieldToClass("sTimeq1_fro")
					sTimeq1_to = .FieldToClass("sTimeq1_to")
					sTimeq2_fro = .FieldToClass("sTimeq2_fro")
					sTimeq2_to = .FieldToClass("sTimeq2_to")
					sUsequery = .FieldToClass("sUsequery")
					nInd_conce = .FieldToClass("nInd_conce")
					nUsercode = .FieldToClass("nUsercode")
					nDuration = .FieldToClass("nDuration")
					nDaysAdv = .FieldToClass("nDaysAdv")
					
					.RCloseRec()
				Else
					reaSchema = False
					
					mstrSche_code = sSche_code
				End If
			End With
			
			'UPGRADE_NOTE: Object lrecReaSchema may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
			lrecReaSchema = Nothing
		End If
		
reaSchema_Err: 
		If Err.Number Then
			reaSchema = False
		End If
		
		On Error GoTo 0
	End Function
	
	'**%LoadTabs: Constructs the sequence in HTML code
	'%LoadTabs: Arma la secuencia en código HTML.
	Public Function LoadTabs(ByVal nMainAction As Integer, ByVal sSche_code As String, ByVal sUserSchema As String) As String
		Dim lrecValSequence As eRemoteDB.Execute
		Dim lclsSequence As eFunctions.Sequence
		Dim lstrHTMLCode As String
		
		On Error GoTo LoadTabs_Err
		
		lclsSequence = New eFunctions.Sequence
		
		lstrHTMLCode = lclsSequence.makeTable
		
		lrecValSequence = New eRemoteDB.Execute
		With lrecValSequence
			.StoredProcedure = "InsValScheSeq"
			.Parameters.Add("sSche_code", sSche_code, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 6, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sSche_code_usr", sUserSchema, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 6, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				While Not .EOF
					lstrHTMLCode = lstrHTMLCode & lclsSequence.makeRow(.FieldToClass("sCodisp"), .FieldToClass("sCodispl"), nMainAction, .FieldToClass("sShort_des"), .FieldToClass("nStatus"),  ,  ,  ,  ,  ,  , .FieldToClass("sDescript"), .FieldToClass("nModules"), .FieldToClass("nWindowty"))
					.RNext()
				End While
				.RCloseRec()
			End If
		End With
		'UPGRADE_NOTE: Object lrecValSequence may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecValSequence = Nothing
		
		lstrHTMLCode = lstrHTMLCode & lclsSequence.closeTable()
		
		LoadTabs = lstrHTMLCode
		
LoadTabs_Err: 
		If Err.Number Then
			LoadTabs = "LoadTabs: " & Err.Description
		End If
		'UPGRADE_NOTE: Object lrecValSequence may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecValSequence = Nothing
		On Error GoTo 0
	End Function
	
	'**%insValRequired: Verifies which windows in the sequence have contents
	'%insValRequired: Se encarga de verificar que ventanas tienen contenido dentro de la secuencia.
	Public Function insValRequired(ByVal sSche_code As String) As Boolean
		Dim lrecValRequired As New eRemoteDB.Execute
		
		insValRequired = False
		
		With lrecValRequired
			.StoredProcedure = "insValRequired"
			.Parameters.Add("sSche_code", sSche_code, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 6, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run Then
				insValRequired = True
				sWithInformation = .FieldToClass("WithInformation")
			End If
		End With
		
		'UPGRADE_NOTE: Object lrecValRequired may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecValRequired = Nothing
	End Function
	
	'**%insValSG013: This method validates the page "SG013" as described in the functional specifications
	'%InsValSG013: Este metodo se encarga de realizar las validaciones descritas en el funcional
	'%de la ventana "SG013"
	Public Function insValSG013(ByVal sCodispl As String, ByVal sAction As String, ByVal sSchemaDes As String, ByVal sShort_des As String, ByVal dEffecdate As Date, ByVal dNulldate As Date, ByVal sTimeFrom1 As String, ByVal sTimeTo1 As String, ByVal sTimeFrom2 As String, ByVal sTimeTo2 As String, ByVal sPermission As String, ByVal sTimeFromQ1 As String, ByVal sTimeToQ1 As String, ByVal sTimeFromQ2 As String, ByVal sTimeToQ2 As String, ByVal lintDuration As Integer, ByVal lintDaysAdv As Integer) As String
		Dim lerrTime As eFunctions.Errors
		Dim lclsValField As eFunctions.valField
		
		On Error GoTo insValSG013_Err
		
		lerrTime = New eFunctions.Errors
		lclsValField = New eFunctions.valField
		
		If CDbl(sAction) <> 303 Then
			'**+Validates the "Description"
			'+Se realizan las validaciones de la "Descripción".
			
			'UPGRADE_WARNING: IsEmpty was upgraded to IsNothing and has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
			'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
			If IsDbNull(sSchemaDes) Or IsNothing(sSchemaDes) Or Trim(sSchemaDes) = String.Empty Or Trim(sSchemaDes) = "0" Then
				Call lerrTime.ErrorMessage(sCodispl, 12080)
			End If
			'**+Validates the "Abreviated description"
			'+Se realizan las validaciones de la "Descripción abreviada".
			
			'UPGRADE_WARNING: IsEmpty was upgraded to IsNothing and has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
			'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
			If IsDbNull(sShort_des) Or IsNothing(sShort_des) Or Trim(sShort_des) = String.Empty Or Trim(sShort_des) = "0" Then
				Call lerrTime.ErrorMessage(sCodispl, 12019)
			End If
			'**+Validates the field "Valid period - date since"
			'+Validaciones sobre el campo "Período de validez - Fecha Desde".
			
			If dEffecdate = eRemoteDB.Constants.dtmNull Then
				Call lerrTime.ErrorMessage(sCodispl, 12159)
			Else
				lclsValField.objErr = lerrTime
				
				If Not lclsValField.ValDate(dEffecdate) Then
					Call lerrTime.ErrorMessage(sCodispl, 1001)
				End If
			End If
			'**+validates the field "Valid Period - Date until"
			'+Validaciones sobre el campo "Período de validez - Fecha Hasta".
			
			If dNulldate <> eRemoteDB.Constants.dtmNull Then
				lclsValField.objErr = lerrTime
				
				If Not lclsValField.ValDate(dNulldate) Then
					Call lerrTime.ErrorMessage(sCodispl, 1001)
				Else
					If sAction <> CStr(eFunctions.Menues.TypeActions.clngActionQuery) Then
						If CDate(dEffecdate) >= CDate(dNulldate) Then
							Call lerrTime.ErrorMessage(sCodispl, 12172)
						End If
					End If
				End If
			End If
			'**+Validates the field "scheduled time work - Since 1 "
			'+Se realizan las validaciones del "Horario de trabajo - Desde 1".
			
			'UPGRADE_WARNING: IsEmpty was upgraded to IsNothing and has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
			'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
			If IsDbNull(sTimeFrom1) Or IsNothing(sTimeFrom1) Or Trim(sTimeFrom1) = String.Empty Then
				Call lerrTime.ErrorMessage(sCodispl, 12160)
			Else
				If Not IsDate(sTimeFrom1) Or Len(sTimeFrom1) < 5 Then
					Call lerrTime.ErrorMessage(sCodispl, 99124,  , eFunctions.Errors.TextAlign.LeftAling, "Horario de trabajo 'desde' primer turno ")
				End If
			End If
			'**+Validates the "Work hour - Until 1 "
			'+Se realizan las validaciones de la "Hora de trabajo - Hasta 1".
			
			If sTimeFrom1 <> String.Empty And sTimeTo1 = String.Empty Then
				Call lerrTime.ErrorMessage(sCodispl, 12170)
			Else
				'UPGRADE_WARNING: IsEmpty was upgraded to IsNothing and has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
				If (Not IsDate(sTimeTo1) Or Len(sTimeTo1) < 5 Or IsNothing(sTimeTo1)) And (Not IsNothing(sTimeFrom1) Or IsDate(sTimeFrom1)) Then
					Call lerrTime.ErrorMessage(sCodispl, 99124,  , eFunctions.Errors.TextAlign.LeftAling, "Horario de trabajo 'hasta' primer turno ")
				Else
					If sTimeTo1 <> String.Empty And sTimeFrom1 = String.Empty Then
						Call lerrTime.ErrorMessage(sCodispl, 12161)
					Else
						'UPGRADE_WARNING: IsEmpty was upgraded to IsNothing and has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
						'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
						If Not IsDbNull(sTimeFrom1) And Not IsNothing(sTimeFrom1) And Trim(sTimeFrom1) <> String.Empty And IsDate(sTimeFrom1) And Len(sTimeFrom1) = 5 Then
							If sTimeTo1 <= sTimeFrom1 Then
								Call lerrTime.ErrorMessage(sCodispl, 12084)
							End If
						End If
					End If
				End If
			End If
			'**+Validates the "Scheduled time work - since 2" field
			'+Se realizan las validaciones del campo "Horario de trabajo - Desde 2".
			
			'UPGRADE_WARNING: IsEmpty was upgraded to IsNothing and has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
			'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
			If Not IsDbNull(sTimeFrom2) And Not IsNothing(sTimeFrom2) And Trim(sTimeFrom2) <> String.Empty Then
				If Not IsDate(sTimeFrom2) Or Len(sTimeFrom2) < 5 Then
					Call lerrTime.ErrorMessage(sCodispl, 99124,  , eFunctions.Errors.TextAlign.LeftAling, "Horario de trabajo 'desde' segundo turno ")
				Else
					'UPGRADE_WARNING: IsEmpty was upgraded to IsNothing and has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
					'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
					If Not IsDbNull(sTimeTo1) And Not IsNothing(sTimeTo1) And Trim(sTimeTo1) <> String.Empty And IsDate(sTimeTo1) And Len(sTimeTo1) = 5 Then
						If sTimeFrom2 <= sTimeTo1 Then
							Call lerrTime.ErrorMessage(sCodispl, 12085)
						End If
					End If
				End If
			End If
			'**+Validates the field " Schedule time work - Until 2"
			'+Se realizan las validaciones del campo "Horario de trabajo - Hasta 2".
			
			'UPGRADE_WARNING: IsEmpty was upgraded to IsNothing and has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
			'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
			If Not IsDbNull(sTimeTo2) And Not IsNothing(sTimeTo2) And Trim(sTimeTo2) <> String.Empty Then
				'UPGRADE_WARNING: IsEmpty was upgraded to IsNothing and has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
				If (Not IsDate(sTimeTo2) Or Len(sTimeTo2) < 5 Or IsNothing(sTimeTo2)) And (Not IsNothing(sTimeFrom2) Or IsDate(sTimeFrom2)) Then
					Call lerrTime.ErrorMessage(sCodispl, 99124,  , eFunctions.Errors.TextAlign.LeftAling, "(Horario de trabajo 'hasta' segundo turno ")
				Else
					If sTimeTo2 <= sTimeFrom2 Then
						Call lerrTime.ErrorMessage(sCodispl, 12084)
					End If
				End If
			End If
			'**+Validates the field "Condition use - Schedule time since 1"
			'+Se realizan las validaciones del campo "Uso de condición - Horario Desde 1"
			
			If sPermission = "1" Then
				'UPGRADE_WARNING: IsEmpty was upgraded to IsNothing and has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
				'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
				If IsDbNull(sTimeFromQ1) Or IsNothing(sTimeFromQ1) Or Trim(sTimeFromQ1) = String.Empty Then
					Call lerrTime.ErrorMessage(sCodispl, 12162)
				Else
					If Not IsDate(sTimeFromQ1) Or Len(sTimeFromQ1) < 5 Then
						Call lerrTime.ErrorMessage(sCodispl, 99124,  , eFunctions.Errors.TextAlign.LeftAling, "(Uso de condición - Horario desde 1) ")
					End If
				End If
				'**+Validates the field "Condition use - Schedule time until 1"
				'+Se realizan las validaciones del campo "Uso de condición - Horario Hasta 1".
				If sTimeFromQ1 <> String.Empty And sTimeToQ1 = String.Empty Then
					Call lerrTime.ErrorMessage(sCodispl, 12171)
				Else
					If (Not IsDate(sTimeToQ1) Or Len(sTimeToQ1) < 5) And (sTimeToQ1 <> String.Empty) Then
						Call lerrTime.ErrorMessage(sCodispl, 99124,  , eFunctions.Errors.TextAlign.LeftAling, "(Uso de condición - Horario hasta 1) ")
					Else
						If Trim(sTimeFromQ1) = String.Empty And Trim(sTimeFromQ1) <> String.Empty Then
							Call lerrTime.ErrorMessage(sCodispl, 12161)
						Else
							'UPGRADE_WARNING: IsEmpty was upgraded to IsNothing and has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
							'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
							If Not IsDbNull(sTimeFromQ1) And Not IsNothing(sTimeFromQ1) And Trim(sTimeFromQ1) <> String.Empty And IsDate(sTimeFromQ1) And Len(sTimeFromQ1) = 5 Then
								If sTimeToQ1 <= sTimeFromQ1 Then
									Call lerrTime.ErrorMessage(sCodispl, 12084)
								End If
							End If
						End If
					End If
				End If
				'**+Validates the field "Condition use - Schedule time since 2"
				'+Se realizan las validaciones del campo "Uso de condición - Horario Desde 2".
				
				'UPGRADE_WARNING: IsEmpty was upgraded to IsNothing and has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
				'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
				If Not IsDbNull(sTimeFromQ2) And Not IsNothing(sTimeFromQ2) And Trim(sTimeFromQ2) <> String.Empty Then
					If Not IsDate(sTimeFromQ2) Or Len(sTimeFromQ2) < 5 Then
						Call lerrTime.ErrorMessage(sCodispl, 99124,  , eFunctions.Errors.TextAlign.LeftAling, "(Uso de condición - Horario desde 2) ")
					Else
						'UPGRADE_WARNING: IsEmpty was upgraded to IsNothing and has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
						'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
						If Not IsDbNull(sTimeToQ1) And Not IsNothing(sTimeToQ1) And Trim(sTimeToQ1) <> String.Empty And IsDate(sTimeToQ1) And Len(sTimeToQ1) = 5 Then
							If sTimeFromQ2 <= sTimeToQ1 Then
								Call lerrTime.ErrorMessage(sCodispl, 12085)
							End If
						End If
					End If
				End If
				'**+Validates the field "Condition use - Schedule until 2"
				'+Se realizan las validaciones del campo "Uso de condición - Horario Hasta 2".
				
				'UPGRADE_WARNING: IsEmpty was upgraded to IsNothing and has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
				'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
				If Not IsDbNull(sTimeToQ2) And Not IsNothing(sTimeToQ2) And Trim(sTimeToQ2) <> String.Empty Then
					If Not IsDate(sTimeToQ2) Or Len(sTimeToQ2) < 5 Then
						Call lerrTime.ErrorMessage(sCodispl, 99124,  , eFunctions.Errors.TextAlign.LeftAling, "(Uso de condición - Horario hasta 2) ")
					Else
						If sTimeToQ2 <= sTimeFromQ2 Then
							Call lerrTime.ErrorMessage(sCodispl, 12084)
						End If
					End If
				End If
			End If
			If lintDuration > 0 And lintDaysAdv <= 0 Then
				Call lerrTime.ErrorMessage(sCodispl, 12182)
			ElseIf lintDuration <= 0 And lintDaysAdv > 0 Then 
				Call lerrTime.ErrorMessage(sCodispl, 12184)
			ElseIf lintDuration > 0 And lintDaysAdv > 0 Then 
				If lintDaysAdv > lintDuration Then
					Call lerrTime.ErrorMessage(sCodispl, 12186)
				End If
			End If
			
		End If
		
		insValSG013 = lerrTime.Confirm
		
		'UPGRADE_NOTE: Object lerrTime may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lerrTime = Nothing
		
insValSG013_Err: 
		If Err.Number Then
			insValSG013 = insValSG013 & Err.Description
		End If
		
		On Error GoTo 0
	End Function
	
	'**%upSecur_sche: This mehtod adds data to the security schema table
	'%updSecur_sche: Se encarga de crear la información en la tabla de esquema de seguridad .
	Public Function updSecur_sche() As Boolean
		Dim lrecUpdSecur_sche As eRemoteDB.Execute
		
		lrecUpdSecur_sche = New eRemoteDB.Execute
		
		With lrecUpdSecur_sche
			.StoredProcedure = "updSecur_sche"
			
			.Parameters.Add("sSche_code", sSche_code, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 6, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAccesof", nAccesof, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dDate_from", dDate_from, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dDate_to", dDate_to, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nInd_curren", nInd_curren, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nInd_limits", nInd_limits, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nInd_conce", nInd_conce, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sLongdesc", sLongdesc, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 30, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nSecurlev", nSecurlev, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sShortdes", sShortdes, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sStatregt", sStatregt, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sTime1_from", sTime1_from, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 5, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sTime1_to", sTime1_to, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 5, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sTime2_from", sTime2_from, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 5, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sTime2_to", sTime2_to, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 5, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sTimeq1_fro", sTimeq1_fro, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 5, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sTimeq1_to", sTimeq1_to, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 5, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sTimeq2_fro", sTimeq2_fro, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 5, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sTimeq2_to", sTimeq2_to, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 5, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sUsequery", sUsequery, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDuration", nDuration, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDaysAdv", nDaysAdv, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			updSecur_sche = .Run(False)
		End With
		
		'UPGRADE_NOTE: Object lrecUpdSecur_sche may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecUpdSecur_sche = Nothing
	End Function
	
	'**%inspostSG013: This method calls the event that updates the "Secur_sche" table - Security schema
	'%insPostSG013: Esta función se encarga de realizar el llamado al evento que actualiza la
	'%tabla Secur_sche - Esquema de seguridad.
    Public Function insPostSG013(ByVal nAction As Integer, ByVal lstrSche_code As String, ByVal lstrSchemaDes As String, ByVal lstrShort_des As String, ByVal ldtmEffecdate As Date, ByVal ldtmNulldate As Date, ByVal lstrTimeFrom1 As String, ByVal lstrTimeTo1 As String, ByVal lstrTimeFrom2 As String, ByVal lstrTimeTo2 As String, ByVal lstrPermission As String, ByVal lstrTimeFromQ1 As String, ByVal lstrTimeToQ1 As String, ByVal lstrTimeFromQ2 As String, ByVal lstrTimeToQ2 As String, ByVal lstrStatregt As String, ByVal lintUsercode As Integer, Optional ByVal lintDuration As Integer = 0, Optional ByVal lintDaysAdv As Integer = 0) As Boolean
        insPostSG013 = True

        On Error GoTo insPostSG013_Err

        sSche_code = lstrSche_code
        nAccesof = 2
        dDate_from = ldtmEffecdate
        dDate_to = ldtmNulldate
        nInd_curren = 1
        nInd_limits = 1
        nInd_conce = 1

        sLongdesc = lstrSchemaDes
        nSecurlev = 1
        sShortdes = lstrShort_des

        If nAction = eFunctions.Menues.TypeActions.clngActionadd Then
            sStatregt = "1"
        Else
            sStatregt = lstrStatregt
        End If

        sTime1_from = lstrTimeFrom1
        sTime1_to = lstrTimeTo1
        sTime2_from = lstrTimeFrom2
        sTime2_to = lstrTimeTo2

        If lstrPermission = "1" Then
            sTimeq1_fro = lstrTimeFromQ1
            sTimeq1_to = lstrTimeToQ1
            sTimeq2_fro = lstrTimeFromQ2
            sTimeq2_to = lstrTimeToQ2
        Else
            sTimeq1_fro = String.Empty
            sTimeq1_to = String.Empty
            sTimeq2_fro = String.Empty
            sTimeq2_to = String.Empty
        End If

        If lstrPermission = String.Empty Then
            sUsequery = "2"
        Else
            sUsequery = lstrPermission
        End If

        nUsercode = lintUsercode
        nDuration = lintDuration
        nDaysAdv = lintDaysAdv

        '**+If the selected option is "Add" or "Update"
        '+Si la opción seleccionada es Registrar o Actualizar.

        Select Case nAction
            Case eFunctions.Menues.TypeActions.clngActionadd, eFunctions.Menues.TypeActions.clngActionUpdate
                insPostSG013 = updSecur_sche()
            Case 303
                insPostSG013 = insDelSchema(lstrSche_code)
        End Select

insPostSG013_Err:
        If Err.Number Then
            insPostSG013 = False
        End If

        On Error GoTo 0
    End Function
	
	'**%insDelSchema_cur: Deletes the information of the actions in the "Schema_cur" table
	'%insDelSchema_Cur: Elimina la Información de las acciones en la tabla "Schema_cur"
	Public Function insDelSchema_Cur(ByVal sSche_code As String) As Boolean
		Dim lrecRecordset As New eRemoteDB.Execute
		
		On Error GoTo insDelSchema_Cur_Err
		
		insDelSchema_Cur = False
		
		With lrecRecordset
			.StoredProcedure = "delSchema_cur"
			.Parameters.Add("sSche_code", sSche_code, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 6, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run(False) Then
				insDelSchema_Cur = True
			End If
		End With
		
insDelSchema_Cur_Err: 
		If Err.Number Then
			insDelSchema_Cur = False
		End If
		
		On Error GoTo 0
	End Function
	
	'**%insCreSchema_Cur: Adds the information of the authorized currencies for a schema
	'%insCreSchema_Cur: Registra la Información de las monedas autorizadas para un esquema.
	Public Function insCreSchema_Cur(ByVal sSche_code As String, ByVal nCurrency As Integer, ByVal nUsercode As Integer) As Boolean
		Dim lrecRecordset As New eRemoteDB.Execute
		
		On Error GoTo insCreSchema_Cur_Err
		
		insCreSchema_Cur = False
		
		With lrecRecordset
			.StoredProcedure = "creSchema_cur"
			
			.Parameters.Add("sSche_code", sSche_code, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 6, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCurrency", nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sStatregt", "1", eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run(False) Then
				insCreSchema_Cur = True
			End If
		End With
		
insCreSchema_Cur_Err: 
		If Err.Number Then
			insCreSchema_Cur = False
		End If
		
		On Error GoTo 0
	End Function
	
	'**%insDelLimitsCur: Deletes the information about the limits of subscription and claims.
	'%insDelLimitsCur: Elimina la Información de los límites de suscripción y siniestros.
	Public Function insDelLimitsCur(ByVal sSche_code As String) As Boolean
		Dim lrecRecordset As New eRemoteDB.Execute
		
		On Error GoTo insDelLimitsCur_Err
		
		insDelLimitsCur = False
		
		With lrecRecordset
			.StoredProcedure = "delLimitsCur"
			
			.Parameters.Add("sSche_code", sSche_code, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 6, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run(False) Then
				insDelLimitsCur = True
			End If
		End With
		
insDelLimitsCur_Err: 
		If Err.Number Then
			insDelLimitsCur = False
		End If
		
		On Error GoTo 0
	End Function
	
	'**%insCreLimits: Adds the information of the Suscription limits and claims
	'%insCreLimits: Registra la Información de los límites de suscripción de siniestros.
	Public Function insCreLimits(ByVal sSche_code As String, ByVal nCurrency As Integer, ByVal nBranch As Integer, ByVal nClaim_d As Double, ByVal nClaim_p As Double, ByVal nIssuelim As Double, ByVal nUsercode As Integer, ByVal nProduct As Integer) As Boolean
		Dim lrecCreLimits As New eRemoteDB.Execute
		
		On Error GoTo insCreLimits_Err
		
		insCreLimits = False
		
		With lrecCreLimits
			.StoredProcedure = "crelimits"
			
			.Parameters.Add("sSche_code", sSche_code, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 6, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCurrency", nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nClaim_d", nClaim_d, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 14, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nClaim_p", nClaim_p, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 14, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nIssuelim", nIssuelim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 12, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sStatregt", "1", eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run(False) Then
				insCreLimits = True
			End If
		End With
		
insCreLimits_Err: 
		If Err.Number Then
			insCreLimits = False
		End If
		
		On Error GoTo 0
	End Function
	
	'**%insDElLimits: Deletes the information of the limits in the "LIMITS" table
	'%insDelLimits: Elimina la Información de los límites en la tabla "LIMITS"
	Public Function insDelLimits(ByVal sSche_code As String) As Boolean
		Dim lrecDelLimits As New eRemoteDB.Execute
		
		On Error GoTo insDelLimits_Err
		
		insDelLimits = False
		
		With lrecDelLimits
			.StoredProcedure = "delLimits_1"
			
			.Parameters.Add("sSche_code", sSche_code, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 6, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run(False) Then
				insDelLimits = True
			End If
		End With
		
insDelLimits_Err: 
		If Err.Number Then
			insDelLimits = False
		End If
		
		On Error GoTo 0
	End Function
	
	'**%insValSG003: This method validates the page "SG003" as described in the functional specifications
	'%InsValSG003: Este metodo se encarga de realizar las validaciones descritas en el funcional
	'%de la ventana "SG003"
	Public Function insValSG003(ByVal sCodispl As String, ByVal sAction As String, ByVal sSche_code As String, ByVal nCurrency As Integer, ByVal nBranch As Integer, ByVal nProduct As Integer) As String
		Dim lerrTime As eFunctions.Errors
		
		On Error GoTo insValSG003_Err
		
		lerrTime = New eFunctions.Errors
		
		If sAction = "Add" Then
			'**+Validates the field "Currency"
			'+Se realizan las validaciones del campo "Moneda".
			
			If nCurrency = 0 Or nCurrency = eRemoteDB.Constants.intNull Then
				Call lerrTime.ErrorMessage(sCodispl, 750024)
			End If
			'**+Validates the field "Branch"
			'+Se realizan las validaciones del campo "Ramo".
			
			If (nBranch = 0 Or nBranch = eRemoteDB.Constants.intNull) And (nCurrency > 0) Then
				Call lerrTime.ErrorMessage(sCodispl, 1022)
			End If
			'**+The existence of duplicates in the table "Limits" is validated
			'+Se valida la existencia de duplicados en la Tabla Limits.
			
			If (nCurrency <> 0 And nCurrency <> eRemoteDB.Constants.intNull) And (nBranch <> 0 And nBranch <> eRemoteDB.Constants.intNull) Then
				If insValDupLimits(sSche_code, nCurrency, nBranch, nProduct) Then
					Call lerrTime.ErrorMessage(sCodispl, 12101)
				End If
			End If
		End If
		
		insValSG003 = lerrTime.Confirm
		
		'UPGRADE_NOTE: Object lerrTime may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lerrTime = Nothing
		
insValSG003_Err: 
		If Err.Number Then
			insValSG003 = insValSG003 & Err.Description
		End If
		
		On Error GoTo 0
	End Function
	
	'**%insValDupLimits: Verifies if a duplicate record exists in the table "Limits" - Suscription
	'**%Limits and claims for the indicated scheme.
	'%insValDupLimits: Permite verificar si existe un registro duplicado en la tabla Limits - Límites
	'%de suscripción y siniestros para el esquema indicado.
	Public Function insValDupLimits(ByVal sSche_code As String, ByVal nCurrency As Integer, ByVal nBranch As Integer, ByVal nProduct As Integer) As Boolean
		Dim lrecValLimits As New eRemoteDB.Execute
		
		On Error GoTo insValDupLimits_Err
		
		insValDupLimits = False
		
		With lrecValLimits
			.StoredProcedure = "insValDupLimits"
			
			.Parameters.Add("sSche_code", sSche_code, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 6, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCurrency", nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run Then
				insValDupLimits = True
			End If
		End With
		
insValDupLimits_Err: 
		If Err.Number Then
			insValDupLimits = False
		End If
		
		On Error GoTo 0
	End Function
	
	'**%InsUpdInd:This method updates the table "Secur_sche" to modify the value of the field "nIndValue"
	'%InsUpdInd: Este metodo actualiza la tabla "Secur_sche" para modificar el valor del campo "nIndValue"
	Public Function InsUpdInd(ByVal sSche_code As String, ByVal pIndName As String, ByVal nIndValue As Integer, ByVal nUsercode As Integer) As Boolean
		Dim lrecRecordset As New eRemoteDB.Execute
		
		On Error GoTo insUpdInd_err
		
		With lrecRecordset
			.StoredProcedure = "insUpdSecur_sche"
			
			.Parameters.Add("sSche_code", sSche_code, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 6, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sIndName", pIndName, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 15, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nIndValue", nIndValue, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			InsUpdInd = .Run(False)
		End With
		
insUpdInd_err: 
		If Err.Number Then
			InsUpdInd = False
		End If
		
		On Error GoTo 0
	End Function
	
	'**%InsUpdIndic: Modifies the indicators of the general information of the schema, Currencies,
	'**%Suscription limits, Access to Branch offices, security levels and payment solicitation concepts
	'%InsUpdIndic: Permite modificar los indicadores de Información general del esquema, Monedas,
	'%Límites de suscripción, acceso a sucursales, niveles de seguridad y conceptos de solicitud
	'%de pago.
	Public Function InsUpdIndic(ByVal sSche_code As String, ByVal nUsercode As Integer) As Boolean
		Dim lrecRecordset As New eRemoteDB.Execute
		
		On Error GoTo InsUpdIndic_Err
		
		With lrecRecordset
			.StoredProcedure = "updSecur_sche1"
			
			.Parameters.Add("sSche_code", sSche_code, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 6, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			InsUpdIndic = .Run(False)
		End With
		
InsUpdIndic_Err: 
		If Err.Number Then
			InsUpdIndic = False
		End If
		
		On Error GoTo 0
	End Function
	
	'**%insDelLimits_2: Deletes the limits information from the table "LIMITS" for the file(s) indicated
	'%insDelLimits_2:Elimina la Información de los límites en el archivo LIMITS para el o los registros
	'%indicados.
	Public Function insDelLimits_2(ByVal sSche_code As String, ByVal nCurrency As Integer, ByVal nBranch As Integer, ByVal nProduct As Integer) As Boolean
		Dim lrecDelLimits As New eRemoteDB.Execute
		
		On Error GoTo insDelLimits_2_Err
		
		insDelLimits_2 = False
		
		With lrecDelLimits
			.StoredProcedure = "delLimits_2"
			
			.Parameters.Add("sSche_code", sSche_code, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 6, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCurrency", nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run(False) Then
				insDelLimits_2 = True
			End If
		End With
		
insDelLimits_2_Err: 
		If Err.Number Then
			insDelLimits_2 = False
		End If
		
		On Error GoTo 0
	End Function
	
	'**%insUpdLimits: Modifies the Subscription fields "nIssuelim", claim declaration
	'**%"nClaim_d" and indicated Claim payment "nClaim_p"
	'%insUpdLimits: Permite modificar los campos Suscripción "nIssuelim", Declaración de siniestros
	'%"nClaim_d" y Pago de siniestros "nClaim_p" indicados.
	Public Function insUpdLimits(ByVal sSche_code As String, ByVal nCurrency As Integer, ByVal nBranch As Integer, ByVal nIssuelim As Double, ByVal nClaim_d As Double, ByVal nClaim_p As Double, ByVal nUsercode As Double, ByVal nProduct As Integer) As Boolean
		Dim lrecUpdLimits As New eRemoteDB.Execute
		
		On Error GoTo insUpdLimits_Err
		
		insUpdLimits = False
		
		With lrecUpdLimits
			.StoredProcedure = "updLimits_1"
			
			.Parameters.Add("sSche_code", sSche_code, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 6, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCurrency", nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nIssuelim", nIssuelim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 12, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nClaim_d", nClaim_d, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 14, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nClaim_p", nClaim_p, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 14, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run(False) Then
				insUpdLimits = True
			End If
		End With
		
insUpdLimits_Err: 
		If Err.Number Then
			insUpdLimits = False
		End If
		
		On Error GoTo 0
	End Function
	
	'**%insPostSG003: This function calls the events that updates the
	'**%table "Limits" - Suscription Limits and Claims depending of the action
	'%insPostSG003: Esta función se encarga de realizar el llamado a los eventos que actualizan la
	'%tabla "Limits" - Límites de suscripción y siniestros dependiendo de la acción.
	Public Function insPostSG003(ByVal sAction As String, ByVal lstrSche_code As String, ByVal lintCurrency As Integer, ByVal lintBranch As Integer, ByVal ldblClaim_d As Double, ByVal ldblClaim_p As Double, ByVal ldblIssuelim As Double, ByVal lintUsercode As Integer, ByVal lintProduct As Integer) As Boolean
		insPostSG003 = True
		
		On Error GoTo insPostSG003_Err
		'**+If the select option is Add
		'+Si la opción seleccionada es Registrar.
		
		Select Case sAction
			Case "Add"
				insPostSG003 = insCreLimits(lstrSche_code, lintCurrency, lintBranch, ldblClaim_d, ldblClaim_p, ldblIssuelim, lintUsercode, lintProduct)
				
			Case "Update"
				insPostSG003 = insUpdLimits(lstrSche_code, lintCurrency, lintBranch, ldblIssuelim, ldblClaim_d, ldblClaim_p, lintUsercode, lintProduct)
		End Select
		
insPostSG003_Err: 
		If Err.Number Then
			insPostSG003 = False
		End If
		
		On Error GoTo 0
	End Function
	
	'**%insCreOff_acc: Adds the Information of the accesses to a Branch office
	'%insCreOff_acc: Registra la Información de los accesos a sucursales.
	Public Function insCreOff_acc(ByVal sSche_code As String, ByVal nOffice As Integer, ByVal sInd_inqu As String, ByVal sInd_upda As String, ByVal nUsercode As Integer) As Boolean
		Dim lrecCreOff_acc As New eRemoteDB.Execute
		
		On Error GoTo insCreOff_acc_Err
		
		insCreOff_acc = False
		
		With lrecCreOff_acc
			.StoredProcedure = "creOff_acc"
			
			.Parameters.Add("sSche_code", sSche_code, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 6, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nOffice", nOffice, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sInd_inqu", sInd_inqu, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sInd_upda", sInd_upda, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sStatregt", "1", eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run(False) Then
				insCreOff_acc = True
			End If
		End With
		
insCreOff_acc_Err: 
		If Err.Number Then
			insCreOff_acc = False
		End If
		
		On Error GoTo 0
	End Function
	
	'**%insDElOff_acc: Deletes the information of the accesses to a branch office - "Off_acc"
	'%insDelOff_acc: Elimina la Información de los accesos a sucursales - "Off_acc"
	Public Function insDelOff_acc(ByVal sSche_code As String) As Boolean
		Dim lrecDelOff_acc As New eRemoteDB.Execute
		
		On Error GoTo insDelOff_acc_Err
		
		insDelOff_acc = False
		
		With lrecDelOff_acc
			.StoredProcedure = "delOff_acc"
			
			.Parameters.Add("sSche_code", sSche_code, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 6, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run(False) Then
				insDelOff_acc = True
			End If
		End With
		
insDelOff_acc_Err: 
		If Err.Number Then
			insDelOff_acc = False
		End If
		
		On Error GoTo 0
	End Function
	
	'**%insDelLevels: Deletes the information of the accesses to a Branch offices - "Off_acc"
	'%insDelLevels: Elimina la Información de los accesos a sucursales - "Off_acc"
	Public Function insDelLevels(ByVal sSche_code As String, ByVal sCode_mt As String, ByVal nUsercode As Integer) As Boolean
		Dim lrecDelLevels As eRemoteDB.Execute
		Dim lsclValues_cache As eFunctions.Values
		
		On Error GoTo insDelLevels_Err
		
		lrecDelLevels = New eRemoteDB.Execute
		insDelLevels = False
		
		With lrecDelLevels
			.StoredProcedure = "delLevels"
			.Parameters.Add("sSche_code", sSche_code, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 6, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sCode_mt", sCode_mt, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 8, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				insDelLevels = True
			End If
		End With
		
		If insDelLevels Then
			lsclValues_cache = New eFunctions.Values
			Call lsclValues_cache.DelCache(1, sSche_code)
		End If
		
insDelLevels_Err: 
		If Err.Number Then
			insDelLevels = False
		End If
		'UPGRADE_NOTE: Object lsclValues_cache may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lsclValues_cache = Nothing
		'UPGRADE_NOTE: Object lrecDelLevels may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecDelLevels = Nothing
		On Error GoTo 0
	End Function
	
	'**%insCreLevels: This method adds the data in the table "Levels" - Security levels
	'%insCreLevels: Se encarga de crear la información en la tabla "Levels" - Niveles de seguridad.
	Public Function insCreLevels(ByVal sSche_code As String, ByVal sInd_Type As String, ByVal sCode_mt As String, ByVal nAmelevel As Integer, ByVal nInqlevel As Integer, ByVal sSupervis As String, ByVal sPermitted As String, ByVal nUsercode As Integer) As Boolean
		Dim lrecCreLevels As New eRemoteDB.Execute
		
		On Error GoTo insCreLevels_Err
		
		With lrecCreLevels
			.StoredProcedure = "insCreLevels"
			
			.Parameters.Add("sSche_code", sSche_code, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 6, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sInd_type", sInd_Type, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sCode_mt", sCode_mt, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 8, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAmelevel", nAmelevel, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nInqlevel", nInqlevel, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sSupervis", sSupervis, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sPermitted", sPermitted, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			insCreLevels = .Run(False)
		End With
		
		'UPGRADE_NOTE: Object lrecCreLevels may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecCreLevels = Nothing
		
insCreLevels_Err: 
		If Err.Number Then
			insCreLevels = False
		End If
		
		On Error GoTo 0
	End Function
	
	'**%insValSG002: This method validates the page "SG002" as described in the functional specifications
	'%InsValSG002: Este metodo se encarga de realizar las validaciones descritas en el funcional
	'%de la ventana "SG002"
	Public Function insValSG002(ByVal sCodispl As String, ByVal sAction As String, ByVal sSche_code As String, ByVal sInd_Type As String, ByVal sModTransac As String) As String
		Dim lerrTime As eFunctions.Errors
		Dim lclsWindows As eSecurity.Windows = New eSecurity.Windows
		
		On Error GoTo insValSG002_Err
		
		lerrTime = New eFunctions.Errors
		
		If sAction = "Add" Then
			'**+Validates the field "type"
			'+Se realizan las validaciones del campo "Tipo".
			
			If Val(sInd_Type) = 0 Then
				Call lerrTime.ErrorMessage(sCodispl, 1012,  , eFunctions.Errors.TextAlign.LeftAling, "Tipo: ")
			Else
				'**+Validates the field "Module"
				'+Se realizan las validaciones del campo "Módulo".
				If Trim(sModTransac) = "0" Or Trim(sModTransac) = "" Then
					If Val(sInd_Type) = 1 Then '+Modulo
						Call lerrTime.ErrorMessage(sCodispl, 56163)
					ElseIf Val(sInd_Type) = 2 Then  '+Transaccion
						Call lerrTime.ErrorMessage(sCodispl, 12060)
					End If
				Else
					If insValDupLevels(sSche_code, sInd_Type, sModTransac) Then
						Call lerrTime.ErrorMessage(sCodispl, 12101)
					Else
						If Val(sInd_Type) = 2 Then '+Transaccion
							If Not lclsWindows.InsValWindows(sModTransac) Then
								Call lerrTime.ErrorMessage(sCodispl, 12014)
							End If
						End If
					End If
				End If
				
			End If
		End If
		
		insValSG002 = lerrTime.Confirm
		
		'UPGRADE_NOTE: Object lerrTime may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lerrTime = Nothing
		
insValSG002_Err: 
		If Err.Number Then
			insValSG002 = insValSG002 & Err.Description
		End If
		
		On Error GoTo 0
	End Function
	
	'**%insValDupLevels: Verify if a duplicate record exists in the table "Levels" - Security levels
	'%insValDupLevels: Permite verificar si existe un registro duplicado en la tabla Levels - Niveles
	'%de seguridad.
	Public Function insValDupLevels(ByVal sSche_code As String, ByVal sInd_Type As String, ByVal sModTransac As String) As Boolean
		Dim lrecValLevels As New eRemoteDB.Execute
		
		On Error GoTo insValDupLevels_Err
		
		insValDupLevels = False
		
		With lrecValLevels
			.StoredProcedure = "insValDupLevels"
			
			.Parameters.Add("sSche_code", sSche_code, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 6, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sInd_type", sInd_Type, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sCode_mt", sModTransac, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 8, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nExists", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run(False) Then
				insValDupLevels = .Parameters("nExists").Value = 1
			End If
		End With
		
insValDupLevels_Err: 
		If Err.Number Then
			insValDupLevels = False
		End If
		
		On Error GoTo 0
	End Function
	
	'**%insPostSG002: This method calls the events that update the table "Levels" -
	'**%Security levels depending of the action
	'%insPostSG002: Esta función se encarga de realizar el llamado a los eventos que actualizan la
	'%tabla Levels - Niveles de seguridad dependiendo de la acción.
	Public Function insPostSG002(ByVal sAction As String, ByVal lstrSche_code As String, ByVal lstrInd_type As String, ByVal lstrModTransac As String, ByVal lintAmelevel As Integer, ByVal lintInqlevel As Integer, ByVal lstrSupervis As String, ByVal lstrPermitted As String, ByVal lintUsercode As Integer) As Boolean
		insPostSG002 = True
		Dim lsclValues_cache As eFunctions.Values
		
		On Error GoTo insPostSG002_Err
		
		If lstrSupervis <> "1" Then
			lstrSupervis = "2"
		End If
		
		If lstrPermitted <> "1" Then
			lstrPermitted = "2"
		End If
		'**+If the select option is Record
		'+Si la opción seleccionada es Registrar.
		
		Select Case sAction
			Case "Add"
				insPostSG002 = insCreLevels(lstrSche_code, lstrInd_type, lstrModTransac, lintAmelevel, lintInqlevel, lstrSupervis, lstrPermitted, lintUsercode)
				
			Case "Update"
				insPostSG002 = insUpdLevels(lstrSche_code, lstrInd_type, lstrModTransac, lintAmelevel, lintInqlevel, lstrSupervis, lstrPermitted, lintUsercode)
		End Select
		
		If insPostSG002 Then
			lsclValues_cache = New eFunctions.Values
			Call lsclValues_cache.DelCache(1, lstrSche_code)
		End If
		
insPostSG002_Err: 
		If Err.Number Then
			insPostSG002 = False
		End If
		'UPGRADE_NOTE: Object lsclValues_cache may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lsclValues_cache = Nothing
		On Error GoTo 0
	End Function
	
	'**%insUpLevels: Modifies the field "Supervisor", Update level, inquiry level and permitted
	'%insUpdLevels: Permite modificar los campos Supervisor, Nivel de actualización, Nivel de
	'%consulta y Permitido.
	Public Function insUpdLevels(ByVal sSche_code As String, ByVal sInd_Type As String, ByVal sModTransac As String, ByVal nAmelevel As Integer, ByVal nInqlevel As Integer, ByVal sSupervis As String, ByVal sPermitted As String, ByVal nUsercode As Double) As Boolean
		Dim lrecUpdLevels As New eRemoteDB.Execute
		
		On Error GoTo insUpdLevels_Err
		
		insUpdLevels = False
		
		With lrecUpdLevels
			.StoredProcedure = "updLevels_1"
			
			.Parameters.Add("sSche_code", sSche_code, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 6, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sInd_type", sInd_Type, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sCode_mt", sModTransac, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 8, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAmelevel", nAmelevel, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nInqLevel", nInqlevel, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sSupervis", sSupervis, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sPermitted", sPermitted, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run(False) Then
				insUpdLevels = True
			End If
		End With
		
insUpdLevels_Err: 
		If Err.Number Then
			insUpdLevels = False
		End If
		
		On Error GoTo 0
	End Function
	
	'**%insReaSecurLevel: This method searches to see if all levels are associated to a
	'**%schema in process
	'%insReaSecurLevel: Función que permite buscar si todos los niveles están asociados a un
	'%esquema en tratamiento.
	Public Function insReaSecurLevel(ByRef lstrSchemaSecur As String) As Boolean
		Dim lrecSecurLevel As New eRemoteDB.Execute
		
		On Error GoTo insReaSecurLevel_Err
		
		insReaSecurLevel = True
		
		With lrecSecurLevel
			.StoredProcedure = "reaSecurLevel"
			
			.Parameters.Add("Schema_secur", lstrSchemaSecur, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 6, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If Not .Run Then
				insReaSecurLevel = False
			Else
				'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
				If .FieldToClass("nSecurlev") = 2 Or IsDbNull(.FieldToClass("nSecurlev")) Then
					insReaSecurLevel = False
				End If
				
				.RCloseRec()
			End If
		End With
		
		'UPGRADE_NOTE: Object lrecSecurLevel may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecSecurLevel = Nothing
		
insReaSecurLevel_Err: 
		If Err.Number Then
			insReaSecurLevel = False
		End If
		
		On Error GoTo 0
	End Function
	
	'**%insDelSche_pcon: Deletes the information from the table "Sche_pcon" - Application form concepts
	'**%of the authorized payment
	'%insDelSche_pcon: Elimina la Información del archivo de Sche_pcon - Conceptos de solicitud de
	'%pagos autorizados.
	Public Function insDelSche_pcon(ByVal sSche_code As String) As Boolean
		Dim lrecDelSche_pcon As New eRemoteDB.Execute
		
		On Error GoTo insDelSche_pcon_Err
		
		With lrecDelSche_pcon
			.StoredProcedure = "delSche_pcon"
			.Parameters.Add("sSche_code", sSche_code, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 6, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			insDelSche_pcon = .Run(False)
		End With
		
insDelSche_pcon_Err: 
		If Err.Number Then
			insDelSche_pcon = False
		End If
		
		On Error GoTo 0
	End Function
	
	'**%insCreSche_pcon: Adds the information of the application form concepts of authorized payments
	'%insCreSche_pcon: Registra la Información de los conceptos de solicitud de pagos autorizados.
	Public Function insCreSche_pcon(ByVal sSche_code As String, ByVal nConcept As Integer, ByVal nUsercode As Integer) As Boolean
		Dim lrecCreSche_pcon As New eRemoteDB.Execute
		
		On Error GoTo insCreSche_pcon_Err
		
		insCreSche_pcon = False
		
		With lrecCreSche_pcon
			.StoredProcedure = "creSche_pcon"
			
			.Parameters.Add("sSche_code", sSche_code, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 6, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nConcept", nConcept, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sStatregt", "1", eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run(False) Then
				insCreSche_pcon = True
			End If
		End With
		
insCreSche_pcon_Err: 
		If Err.Number Then
			insCreSche_pcon = False
		End If
		
		On Error GoTo 0
	End Function
	
	'**%insRealLevels: This method validates if the schema has update level for a module or transaction
	'%insReaLevels:Esta funcion se encarga de validar si el esquema tiene nivel de actualizacion para un modulo o transaccion
	Public Function insReaLevels_v(ByVal lstrScheCode As String, ByVal sInd_Type As String, ByVal sCode_mt As String) As Boolean
		Dim lrecRecordset As New eRemoteDB.Execute
		
		On Error GoTo insReaLevels_v_err
		
		With lrecRecordset
			.StoredProcedure = "reaLevels"
			
			.Parameters.Add("sSche_code", lstrScheCode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 6, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sInd_Type", sInd_Type, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sCode_mt", sCode_mt, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 8, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				insReaLevels_v = True
				nAmelevel = .FieldToClass("nAmelevel")
				nInqlevel = .FieldToClass("nInqlevel")
				sSupervis = .FieldToClass("sSupervis")
				nUsercode = .FieldToClass("nUsercode")
				sPermitted = .FieldToClass("sPermitted")
			Else
				insReaLevels_v = False
			End If
		End With
		
insReaLevels_v_err: 
		If Err.Number Then
			insReaLevels_v = False
		End If
		
		On Error GoTo 0
	End Function
	
	'**%insUpdSecur_scheStatregt: Update the table "Secur_sche" the status of the "active" schema
	'%insUpdSecur_scheStatregt: Permite actualizar la tabla Secur_sche el estado del esquema en "Activo".
	Public Function insUpdSecur_scheStatregt(ByVal sSche_code As String, ByVal sStatregt As String, ByVal nUsercode As Integer) As Boolean
		Dim lrecUpdSecur_sche As New eRemoteDB.Execute
		
		On Error GoTo insUpdSecur_scheStatregt_Err
		
		insUpdSecur_scheStatregt = False
		
		With lrecUpdSecur_sche
			.StoredProcedure = "updSecur_scheStatregt"
			
			.Parameters.Add("sSche_code", sSche_code, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 6, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sStatregt", sStatregt, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run(False) Then
				insUpdSecur_scheStatregt = True
			End If
		End With
		
insUpdSecur_scheStatregt_Err: 
		If Err.Number Then
			insUpdSecur_scheStatregt = False
		End If
		
		On Error GoTo 0
	End Function
	
	'**% valTransAcess: This method validates if a transaction complies with the security schema assiciated to the user
	'%valTransAccess: Esta función se encarga de validar si una determinada transacción cumple con el esquema
	'%de seguridad asociado al usuario.
	Public Function valTransAccess(ByVal lstrSche_code As String, ByVal lstrCode_mt As String, ByVal lstrInd_type As String) As Boolean
		Dim lintAccess As Integer
        Dim lstrSessionSche_code As String
        Dim session As New eRemoteDB.ASPSupport

		On Error GoTo valTransAccess_Err
		
		If lstrSche_code = String.Empty Then
            lstrSessionSche_code = session.GetASPSessionValue("sSche_code")
		Else
			lstrSessionSche_code = lstrSche_code
		End If
		
		Dim lrecvalTransac As eRemoteDB.Execute
		If Find(lstrSessionSche_code) Then
			If nSecurlev = 1 Then
				valTransAccess = True
			Else
				lrecvalTransac = New eRemoteDB.Execute
				
				With lrecvalTransac
					.StoredProcedure = "valTransac"
					.Parameters.Add("sSche_Code", lstrSessionSche_code, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 6, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Parameters.Add("sCode_mt", lstrCode_mt, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 8, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Parameters.Add("sInd_type", lstrInd_type, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Parameters.Add("nStatRoot", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Parameters.Add("nIndAccess", lintAccess, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Run(False)
					valTransAccess = (.Parameters("nIndAccess").Value = 1 Or .Parameters("nIndAccess").Value = 3)
					
					If .Parameters("nIndAccess").Value = 3 Then
						mblnOnlyQuery = True
					End If
				End With
			End If
		End If
		
valTransAccess_Err: 
		If Err.Number Then
			valTransAccess = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecvalTransac may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecvalTransac = Nothing
	End Function
	
	'**%insreaSecur_sche: Updates the table "Secur_sche" the status of the "active" schema
	'%insReaSecur_sche: Permite actualizar la tabla Secur_sche el estado del esquema en "Activo".
	Public Function insReaSecur_sche(ByVal sSche_code As String) As Boolean
		Dim lrecReaSecur_sche As New eRemoteDB.Execute
		
		On Error GoTo insReaSecur_sche_Err
		
		If sSche_code = mstrSche_code And mstrSche_code <> String.Empty Then
			insReaSecur_sche = True
		Else
			insReaSecur_sche = False
			
			With lrecReaSecur_sche
				.StoredProcedure = "reaSecur_sche"
				
				.Parameters.Add("sSche_code", sSche_code, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 6, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				
				If .Run Then
					insReaSecur_sche = True
					nSecurlev = .FieldToClass("nSecurlev")
					mstrSche_code = sSche_code
				End If
			End With
		End If
		
insReaSecur_sche_Err: 
		If Err.Number Then
			insReaSecur_sche = False
		End If
		
		On Error GoTo 0
	End Function
	
	'%valSecur_sche: Verifica si el esquema está permitido
	Public Function valSecur_sche(ByVal sSche_code As String) As Boolean
		Dim lrecReaSecur_sche As New eRemoteDB.Execute
		
		On Error GoTo valSecur_sche_Err
		
		If sSche_code = mstrSche_code And mstrSche_code <> String.Empty Then
			valSecur_sche = True
		Else
			valSecur_sche = False
			
			With lrecReaSecur_sche
				.StoredProcedure = "reaSecur_sche"
				
				.Parameters.Add("sSche_code", sSche_code, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 6, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				
				If .Run Then
					valSecur_sche = True
					nSecurlev = .FieldToClass("nSecurlev")
					mstrSche_code = sSche_code
				End If
			End With
		End If
		
valSecur_sche_Err: 
		If Err.Number Then
			valSecur_sche = False
		End If
		
		On Error GoTo 0
	End Function
	
	
	
	
	
	'@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
	'@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
	'@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
	'@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
	'@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
	'@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
	
	'**%Objective:
	'**%Parameters:
	'**%  nLevel       -
	'**%  bGetChildren -
	'%Objetivo:
	'%Parámetros:
	'%    nLevel       -
	'%    bGetChildren -
	Public Function XMLStream_SecurSche(Optional ByVal nLevel As Integer = 1, Optional ByVal bGetChildren As Boolean = True) As String
		Dim intLevelElement As Integer
        Dim strChildren As String = ""

        On Error GoTo ErrorHandler
		intLevelElement = nLevel + 1
		bXMLHandledAsAttribute = True
		bXMLIsCompress = True
		
		XMLStream_SecurSche = BuildXMLElement("sSche_code", sSche_code, intLevelElement) & BuildXMLElement("nAccesof", nAccesof, intLevelElement) & BuildXMLElement("dDate_from", dDate_from, intLevelElement) & BuildXMLElement("dDate_to", dDate_to, intLevelElement) & BuildXMLElement("nInd_curren", nInd_curren, intLevelElement) & BuildXMLElement("nInd_limits", nInd_limits, intLevelElement) & BuildXMLElement("sLongdesc", sLongdesc, intLevelElement) & BuildXMLElement("nSecurlev", nSecurlev, intLevelElement) & BuildXMLElement("sShortdes", sShortdes, intLevelElement) & BuildXMLElement("sStatregt", sStatregt, intLevelElement) & BuildXMLElement("sTime1_from", sTime1_from, intLevelElement) & BuildXMLElement("sTime1_to", sTime1_to, intLevelElement) & BuildXMLElement("sTime2_from", sTime2_from, intLevelElement) & BuildXMLElement("sTime2_to", sTime2_to, intLevelElement) & BuildXMLElement("sTimeq1_fro", sTimeq1_fro, intLevelElement) & BuildXMLElement("sTimeq1_to", sTimeq1_to, intLevelElement) & BuildXMLElement("sTimeq2_fro", sTimeq2_fro, intLevelElement) & BuildXMLElement("sTimeq2_to", sTimeq2_to, intLevelElement) & BuildXMLElement("sUsequery", sUsequery, intLevelElement) & BuildXMLElement("nInd_conce", nInd_conce, intLevelElement)
		If bGetChildren Then
			strChildren = XMLStream_SchemaCur(intLevelElement, bGetChildren) & XMLStream_SchePcon(intLevelElement, bGetChildren) & XMLStream_OffAcc(intLevelElement, bGetChildren) & XMLStream_Limits(intLevelElement, bGetChildren) & XMLStream_Levels(intLevelElement, bGetChildren)
		End If
		XMLStream_SecurSche = BuildXMLEntity("SecurSche", BuildXMLEntity("SecurSche", strChildren, nLevel,  , XMLStream_SecurSche), nLevel - 1, True)
		bXMLHandledAsAttribute = False
		bXMLIsCompress = False
		
		Exit Function
ErrorHandler: 
		'UPGRADE_WARNING: Array has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
		ProcError("Secur_sche.XMLStream_SecurSche(nLevel,bGetChildren)", New Object(){nLevel, bGetChildren})
	End Function
	
	'**%Objective:
	'**%Parameters:
	'**%  nLevel       -
	'**%  bGetChildren -
	'%Objetivo:
	'%Parámetros:
	'%    nLevel       -
	'%    bGetChildren -
	Private Function XMLStream_SchemaCur(Optional ByVal nLevel As Integer = 1, Optional ByVal bGetChildren As Boolean = True) As String
		Dim intLevelElement As Integer
		Dim intIndex As Integer
		Dim intCount As Integer
        Dim strAttribute As String = ""
        Dim strXMLStream As String
		
		On Error GoTo ErrorHandler
		nLevel = nLevel + 1
		intLevelElement = nLevel + 1
		XMLStream_SchemaCur = String.Empty
		On Error Resume Next
		intCount = UBound(arrCurrency)
		If Err.Number Then intCount = -1
		On Error GoTo ErrorHandler
		
		For intIndex = 0 To intCount
			strXMLStream = BuildXMLElement("nCurrency", arrCurrency(intIndex).nCurrency, intLevelElement) & BuildXMLElement("sStatregt", arrCurrency(intIndex).sStatregt, intLevelElement)
			
			If bXMLHandledAsAttribute Then
				strAttribute = strXMLStream
				strXMLStream = String.Empty
			End If
			XMLStream_SchemaCur = XMLStream_SchemaCur & BuildXMLEntity("SchemaCur", strXMLStream, nLevel,  , strAttribute)
			
		Next intIndex
		If XMLStream_SchemaCur > String.Empty Then
			XMLStream_SchemaCur = BuildXMLEntity("SchemaCur", XMLStream_SchemaCur, nLevel - 1, True)
		End If
		
		Exit Function
ErrorHandler: 
		'UPGRADE_WARNING: Array has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
		ProcError("Secur_sche.XMLStream_SchemaCur(nLevel,bGetChildren)", New Object(){nLevel, bGetChildren})
	End Function
	
	'**%Objective:
	'**%Parameters:
	'**%  nLevel       -
	'**%  bGetChildren -
	'%Objetivo:
	'%Parámetros:
	'%    nLevel       -
	'%    bGetChildren -
	Private Function XMLStream_SchePcon(Optional ByVal nLevel As Integer = 1, Optional ByVal bGetChildren As Boolean = True) As String
		Dim intLevelElement As Integer
		Dim intIndex As Integer
		Dim intCount As Integer
        Dim strAttribute As String = ""
        Dim strXMLStream As String
		
		On Error GoTo ErrorHandler
		nLevel = nLevel + 1
		intLevelElement = nLevel + 1
		XMLStream_SchePcon = String.Empty
		On Error Resume Next
		intCount = UBound(arrSche_Pcon)
		If Err.Number Then intCount = -1
		On Error GoTo ErrorHandler
		
		For intIndex = 0 To intCount
			strXMLStream = BuildXMLElement("nConcept", arrSche_Pcon(intIndex).nConcept, intLevelElement) & BuildXMLElement("sStatregt", arrSche_Pcon(intIndex).sStatregt, intLevelElement)
			If bXMLHandledAsAttribute Then
				strAttribute = strXMLStream
				strXMLStream = String.Empty
			End If
			XMLStream_SchePcon = XMLStream_SchePcon & BuildXMLEntity("SchePcon", strXMLStream, nLevel,  , strAttribute)
		Next intIndex
		If XMLStream_SchePcon > String.Empty Then
			XMLStream_SchePcon = BuildXMLEntity("SchePcon", XMLStream_SchePcon, nLevel - 1, True)
		End If
		
		Exit Function
ErrorHandler: 
		'UPGRADE_WARNING: Array has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
		ProcError("Secur_sche.XMLStream_SchePcon(nLevel,bGetChildren)", New Object(){nLevel, bGetChildren})
	End Function
	
	'**%Objective:
	'**%Parameters:
	'**%  nLevel       -
	'**%  bGetChildren -
	'%Objetivo:
	'%Parámetros:
	'%    nLevel       -
	'%    bGetChildren -
	Private Function XMLStream_OffAcc(Optional ByVal nLevel As Integer = 1, Optional ByVal bGetChildren As Boolean = True) As String
		Dim intLevelElement As Integer
		Dim intIndex As Integer
		Dim intCount As Integer
        Dim strAttribute As String = ""
        Dim strXMLStream As String
		
		On Error GoTo ErrorHandler
		nLevel = nLevel + 1
		intLevelElement = nLevel + 1
		XMLStream_OffAcc = String.Empty
		On Error Resume Next
		intCount = UBound(arrOffice)
		If Err.Number Then intCount = -1
		On Error GoTo ErrorHandler
		
		For intIndex = 0 To intCount
			strXMLStream = BuildXMLElement("nOffice", arrOffice(intIndex).nOffice, intLevelElement) & BuildXMLElement("sDesOffice", arrOffice(intIndex).sDesOffice, intLevelElement) & BuildXMLElement("sInd_inqu", arrOffice(intIndex).sInd_inqu, intLevelElement) & BuildXMLElement("sInd_upda", arrOffice(intIndex).sInd_upda, intLevelElement) & BuildXMLElement("sStatregt", arrOffice(intIndex).sStatregt, intLevelElement)
			If bXMLHandledAsAttribute Then
				strAttribute = strXMLStream
				strXMLStream = String.Empty
			End If
			
			XMLStream_OffAcc = XMLStream_OffAcc & BuildXMLEntity("OffAcc", strXMLStream, nLevel,  , strAttribute)
		Next intIndex
		If XMLStream_OffAcc > String.Empty Then
			XMLStream_OffAcc = BuildXMLEntity("OffAcc", XMLStream_OffAcc, nLevel - 1, True)
		End If
		
		Exit Function
ErrorHandler: 
		'UPGRADE_WARNING: Array has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
		ProcError("Secur_sche.XMLStream_OffAcc(nLevel,bGetChildren)", New Object(){nLevel, bGetChildren})
	End Function
	
	'**%Objective:
	'**%Parameters:
	'**%  nLevel       -
	'**%  bGetChildren -
	'%Objetivo:
	'%Parámetros:
	'%    nLevel       -
	'%    bGetChildren -
	Private Function XMLStream_Limits(Optional ByVal nLevel As Integer = 1, Optional ByVal bGetChildren As Boolean = True) As String
		Dim intLevelElement As Integer
		Dim intIndex As Integer
		Dim intCount As Integer
        Dim strAttribute As String = ""
        Dim strXMLStream As String
		
		On Error GoTo ErrorHandler
		nLevel = nLevel + 1
		intLevelElement = nLevel + 1
		XMLStream_Limits = String.Empty
		On Error Resume Next
		intCount = UBound(arrLimits)
		If Err.Number Then intCount = -1
		On Error GoTo ErrorHandler
		
		For intIndex = 0 To intCount
			strXMLStream = BuildXMLElement("nCurrency", arrLimits(intIndex).nCurrency, intLevelElement) & BuildXMLElement("nBranch", arrLimits(intIndex).nBranch, intLevelElement) & BuildXMLElement("nClaim_d", arrLimits(intIndex).nClaim_d, intLevelElement) & BuildXMLElement("nClaim_p", arrLimits(intIndex).nClaim_p, intLevelElement) & BuildXMLElement("nIssuelim", arrLimits(intIndex).nIssuelim, intLevelElement) & BuildXMLElement("sStatregt", arrLimits(intIndex).sStatregt, intLevelElement) & BuildXMLElement("nProduct", arrLimits(intIndex).nProduct, intLevelElement)
			If bXMLHandledAsAttribute Then
				strAttribute = strXMLStream
				strXMLStream = String.Empty
			End If
			
			XMLStream_Limits = XMLStream_Limits & BuildXMLEntity("Limit", strXMLStream, nLevel,  , strAttribute)
		Next intIndex
		If XMLStream_Limits > String.Empty Then
			XMLStream_Limits = BuildXMLEntity("Limit", XMLStream_Limits, nLevel - 1, True)
		End If
		
		Exit Function
ErrorHandler: 
		'UPGRADE_WARNING: Array has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
		ProcError("Secur_sche.XMLStream_Limits(nLevel,bGetChildren)", New Object(){nLevel, bGetChildren})
	End Function
	
	'**%Objective:
	'**%Parameters:
	'**%  nLevel       -
	'**%  bGetChildren -
	'%Objetivo:
	'%Parámetros:
	'%    nLevel       -
	'%    bGetChildren -
	Private Function XMLStream_Levels(Optional ByVal nLevel As Integer = 1, Optional ByVal bGetChildren As Boolean = True) As String
		Dim intLevelElement As Integer
		Dim intIndex As Integer
		Dim intCount As Integer
        Dim strAttribute As String = ""
        Dim strXMLStream As String
		
		On Error GoTo ErrorHandler
		nLevel = nLevel + 1
		intLevelElement = nLevel + 1
		XMLStream_Levels = String.Empty
		On Error Resume Next
		intCount = UBound(arrLevels)
		If Err.Number Then intCount = -1
		On Error GoTo ErrorHandler
		
		For intIndex = 0 To intCount
			
			strXMLStream = BuildXMLElement("sInd_type", arrLevels(intIndex).sInd_Type, intLevelElement) & BuildXMLElement("sCode_mt", arrLevels(intIndex).sCode_mt, intLevelElement) & BuildXMLElement("nAmelevel", arrLevels(intIndex).nAmelevel, intLevelElement) & BuildXMLElement("sSupervis", arrLevels(intIndex).sSupervis, intLevelElement) & BuildXMLElement("nInqlevel", arrLevels(intIndex).nInqlevel, intLevelElement) & BuildXMLElement("sPermitted", arrLevels(intIndex).sPermitted, intLevelElement) & BuildXMLElement("sDescCode_mt", arrLevels(intIndex).sDescCode_mt, intLevelElement)
			If bXMLHandledAsAttribute Then
				strAttribute = strXMLStream
				strXMLStream = String.Empty
			End If
			
			XMLStream_Levels = XMLStream_Levels & BuildXMLEntity("Level", strXMLStream, nLevel,  , strAttribute)
		Next intIndex
		If XMLStream_Levels > String.Empty Then
			XMLStream_Levels = BuildXMLEntity("Level", XMLStream_Levels, nLevel - 1, True)
		End If
		
		Exit Function
ErrorHandler: 
		'UPGRADE_WARNING: Array has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
		ProcError("Secur_sche.XMLStream_Levels(nLevel,bGetChildren)", New Object(){nLevel, bGetChildren})
	End Function
	
	'**%Objective:
	'**%Parameters:
	'**%  oChild -
	'%Objetivo:
	'%Parámetros:
	'%    oChild -
    Private Function ProcXMLDOMNodeList_SecurSche(ByRef oChild As Xml.XmlNodeList) As Boolean
        Dim intIndex As Integer
        Dim intCount As Integer

        On Error GoTo ErrorHandler
        intCount = oChild.Count - 1
        For intIndex = 0 To intCount

            With oChild(intIndex)
                sSche_code = XMLGetValue(oChild(intIndex), "sSche_code", XMLSupport.eXMLGetValueType.exvString)

                nAccesof = XMLGetValue(oChild(intIndex), "nAccesof", XMLSupport.eXMLGetValueType.exvInteger)
                dDate_from = XMLGetValue(oChild(intIndex), "dDate_from", XMLSupport.eXMLGetValueType.exvDate)
                dDate_to = XMLGetValue(oChild(intIndex), "dDate_to", XMLSupport.eXMLGetValueType.exvDate)
                nInd_curren = XMLGetValue(oChild(intIndex), "nInd_curren", XMLSupport.eXMLGetValueType.exvInteger)
                nInd_limits = XMLGetValue(oChild(intIndex), "nInd_limits", XMLSupport.eXMLGetValueType.exvInteger)
                sLongdesc = XMLGetValue(oChild(intIndex), "sLongdesc", XMLSupport.eXMLGetValueType.exvString)
                nSecurlev = XMLGetValue(oChild(intIndex), "nSecurlev", XMLSupport.eXMLGetValueType.exvInteger)
                sShortdes = XMLGetValue(oChild(intIndex), "sShortdes", XMLSupport.eXMLGetValueType.exvString)
                sStatregt = XMLGetValue(oChild(intIndex), "sStatregt", XMLSupport.eXMLGetValueType.exvString)
                sTime1_from = XMLGetValue(oChild(intIndex), "sTime1_from", XMLSupport.eXMLGetValueType.exvString)
                sTime1_to = XMLGetValue(oChild(intIndex), "sTime1_to", XMLSupport.eXMLGetValueType.exvString)
                sTime2_from = XMLGetValue(oChild(intIndex), "sTime2_from", XMLSupport.eXMLGetValueType.exvString)
                sTime2_to = XMLGetValue(oChild(intIndex), "sTime2_to", XMLSupport.eXMLGetValueType.exvString)
                sTimeq1_fro = XMLGetValue(oChild(intIndex), "sTimeq1_fro", XMLSupport.eXMLGetValueType.exvString)
                sTimeq1_to = XMLGetValue(oChild(intIndex), "sTimeq1_to", XMLSupport.eXMLGetValueType.exvString)
                sTimeq2_fro = XMLGetValue(oChild(intIndex), "sTimeq2_fro", XMLSupport.eXMLGetValueType.exvString)
                sTimeq2_to = XMLGetValue(oChild(intIndex), "sTimeq2_to", XMLSupport.eXMLGetValueType.exvString)
                sUsequery = XMLGetValue(oChild(intIndex), "sUsequery", XMLSupport.eXMLGetValueType.exvString)
                nInd_conce = XMLGetValue(oChild(intIndex), "nInd_conce", XMLSupport.eXMLGetValueType.exvInteger)

                Call ProcXMLDOMNodeList_SchemaCur(.selectNodes("SchemaCurs/SchemaCur"))
                Call ProcXMLDOMNodeList_SchePcon(.selectNodes("SchePcons/SchePcon"))
                Call ProcXMLDOMNodeList_OffAcc(.selectNodes("OffAccs/OffAcc"))
                Call ProcXMLDOMNodeList_Limits(.selectNodes("Limits/Limit"))
                Call ProcXMLDOMNodeList_Levels(.selectNodes("Levels/Level"))
            End With

        Next intIndex
        'UPGRADE_NOTE: Object oChild may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        oChild = Nothing

        Exit Function
ErrorHandler:
        'UPGRADE_WARNING: Array has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
        ProcError("Secur_sche.ProcXMLDOMNodeList_SecurSche(oChild)", New Object() {oChild})
    End Function
	
	'**%Objective:
	'**%Parameters:
	'**%  oChild -
	'%Objetivo:
	'%Parámetros:
	'%    oChild -
    Private Function ProcXMLDOMNodeList_SchemaCur(ByRef oChild As Xml.XmlNodeList) As Boolean
        Dim intIndex As Integer
        Dim intCount As Integer

        On Error GoTo ErrorHandler
        intCount = oChild.Count - 1
        If intCount >= 0 Then
            ReDim Preserve arrCurrency(intCount)
            For intIndex = 0 To intCount
                With oChild.Item(intIndex)
                    arrCurrency(intIndex).nCurrency = XMLGetValue(oChild.Item(intIndex), "nCurrency", XMLSupport.eXMLGetValueType.exvInteger)
                    arrCurrency(intIndex).sStatregt = XMLGetValue(oChild.Item(intIndex), "sStatregt", XMLSupport.eXMLGetValueType.exvString)
                End With
            Next intIndex
        End If
        'UPGRADE_NOTE: Object oChild may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        oChild = Nothing

        Exit Function
ErrorHandler:
        'UPGRADE_WARNING: Array has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
        ProcError("Secur_sche.ProcXMLDOMNodeList_SchemaCur(oChild)", New Object() {oChild})
    End Function
	
	'**%Objective:
	'**%Parameters:
	'**%  oChild -
	'%Objetivo:
	'%Parámetros:
	'%    oChild -
    Private Function ProcXMLDOMNodeList_SchePcon(ByRef oChild As Xml.XmlNodeList) As Boolean
        Dim intIndex As Integer
        Dim intCount As Integer

        On Error GoTo ErrorHandler
        intCount = oChild.Count - 1
        If intCount >= 0 Then
            ReDim Preserve arrSche_Pcon(intCount)
            For intIndex = 0 To intCount
                With oChild.Item(intIndex)
                    arrSche_Pcon(intIndex).nConcept = XMLGetValue(oChild.Item(intIndex), "nConcept", XMLSupport.eXMLGetValueType.exvInteger)
                    arrSche_Pcon(intIndex).sStatregt = XMLGetValue(oChild.Item(intIndex), "sStatregt", XMLSupport.eXMLGetValueType.exvString)
                End With
            Next intIndex
        End If
        'UPGRADE_NOTE: Object oChild may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        oChild = Nothing

        Exit Function
ErrorHandler:
        'UPGRADE_WARNING: Array has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
        ProcError("Secur_sche.ProcXMLDOMNodeList_SchePcon(oChild)", New Object() {oChild})
    End Function
	
	'**%Objective:
	'**%Parameters:
	'**%  oChild -
	'%Objetivo:
	'%Parámetros:
	'%    oChild -
    Private Function ProcXMLDOMNodeList_OffAcc(ByRef oChild As Xml.XmlNodeList) As Boolean
        Dim intIndex As Integer
        Dim intCount As Integer

        On Error GoTo ErrorHandler
        intCount = oChild.Count - 1
        If intCount >= 0 Then
            ReDim Preserve arrOffice(intCount)
            For intIndex = 0 To intCount
                With oChild.Item(intIndex)
                    arrOffice(intIndex).nOffice = XMLGetValue(oChild.Item(intIndex), "nOffice", XMLSupport.eXMLGetValueType.exvLong)
                    arrOffice(intIndex).sDesOffice = XMLGetValue(oChild.Item(intIndex), "sDesOffice", XMLSupport.eXMLGetValueType.exvString)
                    arrOffice(intIndex).sInd_inqu = XMLGetValue(oChild.Item(intIndex), "sInd_inqu", XMLSupport.eXMLGetValueType.exvString)
                    arrOffice(intIndex).sInd_upda = XMLGetValue(oChild.Item(intIndex), "sInd_upda", XMLSupport.eXMLGetValueType.exvString)
                    arrOffice(intIndex).sStatregt = XMLGetValue(oChild.Item(intIndex), "sStatregt", XMLSupport.eXMLGetValueType.exvString)
                End With
            Next intIndex
        End If
        'UPGRADE_NOTE: Object oChild may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        oChild = Nothing

        Exit Function
ErrorHandler:
        'UPGRADE_WARNING: Array has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
        ProcError("Secur_sche.ProcXMLDOMNodeList_OffAcc(oChild)", New Object() {oChild})
    End Function
	
	'**%Objective:
	'**%Parameters:
	'**%  oChild -
	'%Objetivo:
	'%Parámetros:
	'%    oChild -
    Private Function ProcXMLDOMNodeList_Limits(ByRef oChild As Xml.XmlNodeList) As Boolean
        Dim intIndex As Integer
        Dim intCount As Integer

        On Error GoTo ErrorHandler
        intCount = oChild.Count - 1
        If intCount >= 0 Then
            ReDim Preserve arrLimits(intCount)
            For intIndex = 0 To intCount
                With oChild.Item(intIndex)
                    arrLimits(intIndex).nCurrency = XMLGetValue(oChild.Item(intIndex), "nCurrency", XMLSupport.eXMLGetValueType.exvInteger)
                    arrLimits(intIndex).nBranch = XMLGetValue(oChild.Item(intIndex), "nBranch", XMLSupport.eXMLGetValueType.exvInteger)
                    arrLimits(intIndex).nClaim_d = XMLGetValue(oChild.Item(intIndex), "nClaim_d", XMLSupport.eXMLGetValueType.exvDecimal)
                    arrLimits(intIndex).nClaim_p = XMLGetValue(oChild.Item(intIndex), "nClaim_p", XMLSupport.eXMLGetValueType.exvDecimal)
                    arrLimits(intIndex).nIssuelim = XMLGetValue(oChild.Item(intIndex), "nIssuelim", XMLSupport.eXMLGetValueType.exvDecimal)
                    arrLimits(intIndex).sStatregt = XMLGetValue(oChild.Item(intIndex), "sStatregt", XMLSupport.eXMLGetValueType.exvString)
                    arrLimits(intIndex).nProduct = XMLGetValue(oChild.Item(intIndex), "nProduct", XMLSupport.eXMLGetValueType.exvInteger)
                End With
            Next intIndex
        End If
        'UPGRADE_NOTE: Object oChild may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        oChild = Nothing

        Exit Function
ErrorHandler:
        'UPGRADE_WARNING: Array has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
        ProcError("Secur_sche.ProcXMLDOMNodeList_Limits(oChild)", New Object() {oChild})
    End Function
	
	'**%Objective:
	'**%Parameters:
	'**%  oChild -
	'%Objetivo:
	'%Parámetros:
	'%    oChild -
    Private Function ProcXMLDOMNodeList_Levels(ByRef oChild As Xml.XmlNodeList) As Boolean
        Dim intCount As Integer
        Dim intIndex As Integer

        On Error GoTo ErrorHandler

        intCount = oChild.Count - 1
        If intCount >= 0 Then
            ReDim Preserve arrLevels(intCount)
            For intIndex = 0 To intCount
                With oChild.Item(intIndex)
                    arrLevels(intIndex).sInd_Type = XMLGetValue(oChild.Item(intIndex), "sInd_type", XMLSupport.eXMLGetValueType.exvString)
                    arrLevels(intIndex).sCode_mt = XMLGetValue(oChild.Item(intIndex), "sCode_mt", XMLSupport.eXMLGetValueType.exvString)
                    arrLevels(intIndex).nAmelevel = XMLGetValue(oChild.Item(intIndex), "nAmelevel", XMLSupport.eXMLGetValueType.exvInteger)
                    arrLevels(intIndex).sSupervis = XMLGetValue(oChild.Item(intIndex), "sSupervis", XMLSupport.eXMLGetValueType.exvString)
                    arrLevels(intIndex).nInqlevel = XMLGetValue(oChild.Item(intIndex), "nInqlevel", XMLSupport.eXMLGetValueType.exvInteger)
                    arrLevels(intIndex).sPermitted = XMLGetValue(oChild.Item(intIndex), "sPermitted", XMLSupport.eXMLGetValueType.exvString)
                    arrLevels(intIndex).sDescCode_mt = XMLGetValue(oChild.Item(intIndex), "sDescCode_mt", XMLSupport.eXMLGetValueType.exvString)
                End With
            Next intIndex
        End If
        'UPGRADE_NOTE: Object oChild may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        oChild = Nothing

        Exit Function
ErrorHandler:
        'UPGRADE_WARNING: Array has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
        ProcError("Secur_sche.ProcXMLDOMNodeList_Levels(oChild)", New Object() {oChild})
    End Function
	
	'**%Objective:
	'**%Parameters:
	'**%  sXMLStream -
	'%Objetivo:
	'%Parámetros:
	'%    sXMLStream -
	Public Sub ProcXMLStream(ByVal sXMLStream As String)
        Dim objXML As New Xml.XmlDocument
		
		On Error GoTo ErrorHandler

		With objXML
            .LoadXml(sXMLStream)

            bXMLHandledAsAttribute = XMLGetValue(.DocumentElement.SelectSingleNode("/SecurSches"), "_HandledAsAttribute", XMLSupport.eXMLGetValueType.exvBoolean, True)
            bXMLIsCompress = XMLGetValue(.DocumentElement.SelectSingleNode("/SecurSches"), "_IsCompress", XMLSupport.eXMLGetValueType.exvBoolean, True)

            ProcXMLDOMNodeList_SecurSche(.DocumentElement.SelectNodes("/SecurSches/SecurSche"))
        End With
		
		'UPGRADE_NOTE: Object objXML may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		objXML = Nothing
		
		Exit Sub
ErrorHandler: 
		'UPGRADE_NOTE: Object objXML may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		objXML = Nothing
		'UPGRADE_WARNING: Array has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
		ProcError("Secur_sche.ProcXMLStream(sXMLStream)", New Object(){sXMLStream})
	End Sub
	
	'**%Objective:
	'**%Parameters:
	'**%  sSchemaCode -
	'%Objetivo: '%NOERROR%
	'%Parámetros:
	'%    sSchemaCode -
	Private Function GetSchemaSession(ByVal sSchemaCode As String) As Boolean
		Dim strXMLSchema As String
        Dim session As New eRemoteDB.ASPSupport

		On Error GoTo ErrorHandler
		GetSchemaSession = False
        strXMLSchema = session.GetASPSessionValue("sXMLSchema")
		If Trim(strXMLSchema) > String.Empty Then
			Call ProcXMLStream(strXMLSchema)
			GetSchemaSession = (sSche_code = sSchemaCode)
		End If

		Exit Function
ErrorHandler: 
        ProcError("Secur_sche.GetSchemaSession(sSchemaCode)", New Object() {sSchemaCode})
	End Function
	
	'%Sche_Transac:Devuelve las operaciones validas al esquema indicado
	Public Function Sche_Transac(ByVal sSchemaCode As String, ByVal sCodispl As String) As String
		Dim mobjvalues As eFunctions.Values
        Dim lrecSche_transac As New eRemoteDB.Execute
        Dim strResult As String = ""


        mobjvalues = New eFunctions.Values
		lrecSche_transac = New eRemoteDB.Execute
        Try

            With lrecSche_transac
                .StoredProcedure = "TABSCHE_TRANSAC"
                .Parameters.Add("sSche_code", sSchemaCode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 6, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("sCodispl", sCodispl, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 8, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

                If .Run Then
                    Do While Not .EOF
                        If strResult > String.Empty Then
                            strResult = strResult & "," & .FieldToClass("nTransac") & "|" & .FieldToClass("sDescript")
                        Else
                            strResult = .FieldToClass("nTransac") & "|" & .FieldToClass("sDescript")
                        End If
                        .RNext()
                    Loop
                    .RCloseRec()
                End If
            End With
            Return strResult
        Catch ex As Exception
            Return vbEmpty
        Finally
            mobjvalues = Nothing
            lrecSche_transac = Nothing
        End Try
    End Function
	
	'**%insValSG020: This method validates the page "SG020" as described in the functional specifications
	'%InsValSG020: Este metodo se encarga de realizar las validaciones descritas en el funcional
	'%de la ventana "SG020"
	Public Function insValSG020(ByVal sCodispl As String, ByVal sAction As String, ByVal sSche_code As String, ByVal sCodisplTx As String, ByVal nTransac As Integer) As String
		Dim lerrTime As eFunctions.Errors
		
		On Error GoTo insValSG020_Err
		
		lerrTime = New eFunctions.Errors
		
		
		If sCodisplTx = "" Or sCodisplTx = String.Empty Then
			Call lerrTime.ErrorMessage(sCodispl, 12014)
		End If
		
		If nTransac = eRemoteDB.Constants.intNull Or nTransac = 0 Then
			Call lerrTime.ErrorMessage(sCodispl, 12159)
		End If
		
		If valExistSche_Transac(sSche_code, sCodisplTx, nTransac) Then
			Call lerrTime.ErrorMessage(sCodispl, 10284)
		End If
		
		insValSG020 = lerrTime.Confirm
		
		'UPGRADE_NOTE: Object lerrTime may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lerrTime = Nothing
		
insValSG020_Err: 
		If Err.Number Then
			insValSG020 = insValSG020 & Err.Description
		End If
		
		On Error GoTo 0
	End Function
	
	Public Function insPostSG020(ByVal sAction As String, ByVal sSche_code As String, ByVal sCodispl As String, ByVal nTransac As Integer, ByVal nUsercode As Integer) As Boolean
		Dim lrecUpdSche_Transac As New eRemoteDB.Execute
		
		On Error GoTo insPostSG020_Err
		
		insPostSG020 = False
		
		With lrecUpdSche_Transac
			.StoredProcedure = "Sg020PKG.insUpdSche_Transac"
			.Parameters.Add("sAction", sAction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 10, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sSche_code", sSche_code, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 6, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sCodispl", sCodispl, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 8, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTransac", nTransac, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUserCode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				insPostSG020 = True
			End If
		End With
		
insPostSG020_Err: 
		If Err.Number Then
			insPostSG020 = False
		End If
		
		On Error GoTo 0
	End Function
	
	Public Function valExistSche_Transac(ByVal sSche_code As String, ByVal sCodispl As String, ByVal nTransac As Integer) As Boolean
		Dim lrecvalExistSche_Transac As New eRemoteDB.Execute
		
		On Error GoTo valExistSche_Transac_Err
		
		valExistSche_Transac = False
		
		With lrecvalExistSche_Transac
			.StoredProcedure = "Sg020PKG.insValExistSche_Transac"
			.Parameters.Add("sSche_code", sSche_code, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 6, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sCodispl", sCodispl, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 8, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTransac", nTransac, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nExists", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run(False) Then
				If .Parameters("nExists").Value = 1 Then
					valExistSche_Transac = True
				Else
					valExistSche_Transac = False
				End If
			End If
		End With
		
valExistSche_Transac_Err: 
		If Err.Number Then
			valExistSche_Transac = False
		End If
		
		On Error GoTo 0
	End Function
	
	
	'%GetLevelsByTransac: Obtiene los valores de actualización y consulta para una transacción del esquema de un usuario en la tabla "Levels" - Niveles de seguridad.
	Public Function GetLevelsByTransac(ByVal sSche_code As String, ByVal sInd_Type As String, ByVal sCode_mt As String) As Boolean
		Dim lrecGetLevelsByTransac As New eRemoteDB.Execute
		
		On Error GoTo GetLevelsByTransac_Err
		
		GetLevelsByTransac = False
		
		With lrecGetLevelsByTransac
			.StoredProcedure = "ValActionLevel"
			
			.Parameters.Add("sSche_code", sSche_code, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 6, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sInd_type", sInd_Type, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sCode_mt", sCode_mt, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 8, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nInqlevel", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAmelevel", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run(False) Then
				GetLevelsByTransac = True
				Me.nInqlevel = .Parameters("nInqlevel").Value
				Me.nAmelevel = .Parameters("nAmelevel").Value
			End If
		End With
		
		'UPGRADE_NOTE: Object lrecGetLevelsByTransac may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecGetLevelsByTransac = Nothing
		
GetLevelsByTransac_Err: 
		If Err.Number Then
			GetLevelsByTransac = False
		End If
		
		On Error GoTo 0
	End Function
End Class






