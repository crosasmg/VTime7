Option Strict Off
Option Explicit On
Public Class Buy_ord
	'- Definición de variables a ser usadas en la transacción. Estructura de la tabla Buy_ord
	
	' Name                                     ' Null?    Type
	' ----------------------------------------- -------- ----------------------------
	Public nServ_Order As Double 'NOT NULL NUMBER(10)
	Public dOrd_date As Date 'NOT NULL DATE
	Public sClient As String 'NOT NULL CHAR(14)
	Public nId As Integer 'NOT NULL NUMBER(5)
	Public nQuantity_Parts As Integer 'NOT NULL NUMBER(5)
	Public nAuto_part As Integer 'NOT NULL NUMBER(5)
	Public sOriginal As String 'Char(1)
	Public nAmount_Part As Double 'Number(14, 2)
	Public nUsercode As Integer 'NOT NULL NUMBER(5)
	Public dCompdate As Date 'NOT NULL DATE
	
	'- Variables auxiliares para el manejo de la clase
	
	Public blnCharge As Boolean
	Public nTotalSpare_Amount As Double
	Public sSel As String
	Public sAddress As String
	Public nProvider As Integer
	Public sCity As String
	Public sState As String
	Public sPhone As String
	Public sClientName As String
	Public nMunicipality As Integer
	Public nProvince As Integer
	Public nLocal As Integer
	Public sName_Cont As String
	Public sAdd_Contact As String
	Public sPhone_Cont As String
	
	'- Tipo registro
	Private Structure udtBuy_Orders
		Dim nServ_Order As Double
		Dim dOrd_date As Date
		Dim sClient As String
		Dim nId As Integer
		Dim nQuantity_Parts As Integer
		Dim nAuto_part As Integer
		Dim sOriginal As String
		Dim nAmount_Part As Double
		Dim nUsercode As Integer
		Dim dCompdate As Date
		Dim sSel As String
	End Structure
	
	'- Arreglo
	Private arrBuy_Orders() As udtBuy_Orders
	'
	
	'% FindSI776: Localiza los registros dentro de la tabla Buy_ord (si está registrada la orden)
	'%            o en Quot_parts si la orden es nueva - ACM - 11/07/2002
	Public Function FindSI776(ByVal nService_Order As Integer, ByVal nId As Integer, ByVal dOrder_date As Date) As Boolean
		Dim recReaBuy_Ord As New eRemoteDB.Execute
		Dim lintCount As Integer
		
		On Error GoTo FindSI776_err
		
		With recReaBuy_Ord
			.StoredProcedure = "ReaBuy_Ord"
			.Parameters.Add("nService_Order", nService_Order, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nID", nId, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dOrder_date", dOrder_date, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(True) Then
				ReDim arrBuy_Orders(50)
				Do While Not .EOF
					lintCount = lintCount + 1
					arrBuy_Orders(lintCount).nServ_Order = .FieldToClass("nServ_Order")
					arrBuy_Orders(lintCount).dOrd_date = .FieldToClass("dOrd_date")
					arrBuy_Orders(lintCount).sClient = .FieldToClass("sClient")
					arrBuy_Orders(lintCount).nId = .FieldToClass("nID")
					arrBuy_Orders(lintCount).nQuantity_Parts = .FieldToClass("nQuantity_Parts")
					arrBuy_Orders(lintCount).nAuto_part = .FieldToClass("nAuto_part")
					arrBuy_Orders(lintCount).sOriginal = .FieldToClass("sOriginal")
					arrBuy_Orders(lintCount).nAmount_Part = .FieldToClass("nAmount_Part")
					arrBuy_Orders(lintCount).nUsercode = .FieldToClass("nUsercode")
					arrBuy_Orders(lintCount).dCompdate = .FieldToClass("dCompdate")
					.RNext()
				Loop 
				ReDim Preserve arrBuy_Orders(lintCount)
				FindSI776 = True
				.RCloseRec()
			Else
				FindSI776 = False
			End If
		End With
		
		blnCharge = FindSI776
		
FindSI776_err: 
		If Err.Number Then
			FindSI776 = False
		End If
		
		recReaBuy_Ord = Nothing
		
		On Error GoTo 0
		
	End Function
	
	'% insValSI776: Valida los valores ingresados a los campos de la ventana.
	'%              nFolder = 1: Se validan los campos puntuales del encabezado de la ventana
	'%              nFolder = 2: Se validan los campos puntuales del cuerpo de la ventana
	Public Function insValSI776(ByVal nFolder As Integer, Optional ByVal dEffecdate As Date = #12:00:00 AM#, Optional ByVal nClaimNumber As Double = 0, Optional ByVal nCaseNumber As Integer = 0, Optional ByVal nDeman_type As Integer = 0, Optional ByVal nService_Order As Integer = 0, Optional ByVal nType_Order As Integer = 0, Optional ByVal sRUT As String = "", Optional ByVal sName_Cont As String = "", Optional ByVal sAdd_Contact As String = "") As String
		Dim lclsProf_ord As eClaim.Prof_ord
		Dim lclsErrors As eFunctions.Errors
		Dim lclsClaim As eClaim.Claim
		Dim lclsPolicy As ePolicy.Policy
		Dim lstrSep As String
        Dim lstrError As String = ""

        lclsErrors = New eFunctions.Errors
		lclsClaim = New eClaim.Claim
		
		On Error GoTo insValSI776_err
		
		lstrSep = "||"
		
		Select Case nFolder
			'+ Validaciones correspondientes a los campos que se encuentran en el
			'+ encabezado de la ventana - ACM - 11/07/2002
			Case 1
				'+ La fecha debe estar llena
				'UPGRADE_WARNING: IsEmpty was upgraded to IsNothing and has a new behavior. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
				If IsNothing(dEffecdate) Then
					'Call lclsErrors.ErrorMessage("SI776", 1103)
					lstrError = lstrError & lstrSep & "1103"
				End If
				
				'+ El número del siniestro debe tener valor
				If nClaimNumber <= 0 Then
					'Call lclsErrors.ErrorMessage("SI776", 4006)
					lstrError = lstrError & lstrSep & "4006"
				Else
					'+ Si el número del siniestro tiene valor, el siniestro debe estar registrado
					If Not lclsClaim.Find(nClaimNumber) Then
						'Call lclsErrors.ErrorMessage("SI776", 4005)
						lstrError = lstrError & lstrSep & "4005"
					End If
				End If
				
				'+ El número del caso debe tener valor
				If nCaseNumber <= 0 Then
					'Call lclsErrors.ErrorMessage("SI776", 4310)
					lstrError = lstrError & lstrSep & "4310"
				End If
				
				'+ El número de la orden debe tener valor
				If nService_Order <= 0 Then
					'Call lclsErrors.ErrorMessage("SI776", 4055)
					lstrError = lstrError & lstrSep & "4055"
				Else
					lclsProf_ord = New eClaim.Prof_ord
					'+ Si el número de la orden tiene valor, debe estar aprobada (nStatus_order = 8)
					If lclsProf_ord.Find_nServ(nService_Order) Then
						If lclsProf_ord.nStatus_ord <> 8 Then
							'Call lclsErrors.ErrorMessage("SI776", 55768)
							lstrError = lstrError & lstrSep & "55768"
						End If
						'+ Si el número de la orden tiene valor, debe corresponder a "Cotización de repuestos" (nOrdertype = 4)
						lclsPolicy = New ePolicy.Policy
						lclsPolicy.Find_TabNameB(lclsClaim.nBranch)
						If lclsPolicy.sTabname = "FIRE" Then
							If lclsProf_ord.nOrdertype <> 5 Then
								'Call lclsErrors.ErrorMessage("SI776", 56105)
								lstrError = lstrError & lstrSep & "56105"
							End If
						Else
							If lclsProf_ord.nOrdertype <> 4 And lclsProf_ord.nOrdertype <> 7 Then
								'Call lclsErrors.ErrorMessage("SI776", 55760)
								lstrError = lstrError & lstrSep & "55760"
							End If
						End If
						'+ Verificar que la orden este asociada al siniestro-caso-demandante
						If Not lclsProf_ord.ValProf_ord(nClaimNumber, nCaseNumber, nDeman_type, nService_Order, True) Then
							'Call lclsErrors.ErrorMessage("SI776", 55755)
							lstrError = lstrError & lstrSep & "55755"
						End If
					Else
						'Call lclsErrors.ErrorMessage("SI776", 60522)
						lstrError = lstrError & lstrSep & "60522"
					End If
				End If
				
				'+ Validaciones de campos puntuales del cuerpo de la ventana - ACM - 11/07/2002
			Case 2
				
				'+ El RUC del proveedor debe estar lleno
				If sRUT = String.Empty Then
					'Call lclsErrors.ErrorMessage("SI776", 55769)
					lstrError = lstrError & lstrSep & "55769"
				End If
				
				'+ El Nombre del contacto debe estar lleno
				If sName_Cont = String.Empty Then
					'Call lclsErrors.ErrorMessage("SI776", 55770)
					lstrError = lstrError & lstrSep & "55770"
				End If
				
				'+ La dirección del contato debe estar llena
				If sAdd_Contact = String.Empty Then
					'Call lclsErrors.ErrorMessage("SI776", 55771)
					lstrError = lstrError & lstrSep & "55771"
				End If
		End Select
		
		'insValSI776 = lclsErrors.Confirm
		If lstrError <> String.Empty Then
			lstrError = Mid(lstrError, 3)
			lclsErrors.ErrorMessage("SI776",  ,  ,  ,  ,  , lstrError)
			insValSI776 = lclsErrors.Confirm
		End If
		
insValSI776_err: 
		If Err.Number Then
			insValSI776 = "insvalSI776: " & Err.Description
		End If
		lclsProf_ord = Nothing
		lclsClaim = Nothing
		lclsErrors = Nothing
		lclsPolicy = Nothing
		On Error GoTo 0
	End Function
	
	'% CountSI776: Propiedad que retorna la cantidad de elementos añadidos al
	'%             arreglo - ACM - 18/07/2002
	Public ReadOnly Property CountSI776() As Integer
		Get
			CountSI776 = UBound(arrBuy_Orders)
		End Get
	End Property
	
	'% ItemSI776: Retorna a las propiedades de la clase los valores almacenados en
	'%            determinada propiedad del arreglo - ACM - 18/07/2002
	Public Function ItemSI776(ByVal nIndex As Integer) As Boolean
		ItemSI776 = False
		
		On Error GoTo ItemSI776_err
		
		If blnCharge Then
			If nIndex <= UBound(arrBuy_Orders) Then
				With arrBuy_Orders(nIndex)
					Me.nServ_Order = .nServ_Order
					Me.dOrd_date = .dOrd_date
					Me.sClient = .sClient
					Me.nId = .nId
					Me.nQuantity_Parts = .nQuantity_Parts
					Me.nAuto_part = .nAuto_part
					Me.sOriginal = .sOriginal
					Me.nAmount_Part = .nAmount_Part
					Me.nUsercode = .nUsercode
					Me.dCompdate = .dCompdate
					Me.sSel = .sSel
					Me.nTotalSpare_Amount = (Me.nQuantity_Parts * Me.nAmount_Part)
				End With
				ItemSI776 = True
			Else
				ItemSI776 = False
			End If
		End If
		
ItemSI776_err: 
		If Err.Number Then
			ItemSI776 = False
		End If
		
		On Error GoTo 0
		
	End Function
	
	Public Function Find_ServiceOrder(ByVal nClaim As Double, ByVal nCase_Number As Integer, ByVal nDemandant_Type As Integer, ByVal nService_Order As Integer, ByVal dQuot_date As Date) As Boolean
        Dim lintCount As Object = New Object
        Dim lrecReaSpare_Parts As New eRemoteDB.Execute
		Dim lintCont As Integer
		
		On Error GoTo Find_ServiceOrder_err
		
		With lrecReaSpare_Parts
			.StoredProcedure = "ReaSpare_Parts"
			.Parameters.Add("nService_Order", nService_Order, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dQuot_date", dQuot_date, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(True) Then
				ReDim arrBuy_Orders(50)
				Do While Not .EOF
					lintCount = lintCount + 1
					arrBuy_Orders(lintCount).nServ_Order = .FieldToClass("nServ_Order")
					arrBuy_Orders(lintCount).dOrd_date = .FieldToClass("dQuot_date")
					arrBuy_Orders(lintCount).sClient = .FieldToClass("sClient")
					arrBuy_Orders(lintCount).nId = .FieldToClass("nID")
					arrBuy_Orders(lintCount).nQuantity_Parts = .FieldToClass("nQuantity_Parts")
					arrBuy_Orders(lintCount).nAuto_part = .FieldToClass("nAuto_parts")
					arrBuy_Orders(lintCount).sOriginal = .FieldToClass("sOriginal")
					arrBuy_Orders(lintCount).nAmount_Part = .FieldToClass("nAmount_Part")
					arrBuy_Orders(lintCount).nUsercode = .FieldToClass("nUsercode")
					arrBuy_Orders(lintCount).dCompdate = .FieldToClass("dCompdate")
					arrBuy_Orders(lintCount).sSel = .FieldToClass("sSel")
					.RNext()
				Loop 
				ReDim Preserve arrBuy_Orders(lintCount)
				Find_ServiceOrder = True
				.RCloseRec()
			Else
				Find_ServiceOrder = False
			End If
		End With
		
		blnCharge = Find_ServiceOrder
		
		If blnCharge Then
			Call LocateProvider(nService_Order)
		End If
		
Find_ServiceOrder_err: 
		If Err.Number Then
			Find_ServiceOrder = False
		End If
		
		lrecReaSpare_Parts = Nothing
		
		On Error GoTo 0
		
	End Function
	
	'% LocateProvider: Localiza los datos del proveedor dada una orden de servicio - ACM - 19/07/2002
	Public Sub LocateProvider(ByVal nServiceOrder As Object)
		Const sRecType As String = "1"
		Dim lclsProfessional_Orders As eClaim.Prof_ord = New eClaim.Prof_ord
		Dim lclsAddress As New eGeneralForm.Address
		Dim lclsPhones As New eGeneralForm.Phone
		Dim lclsClient As New eClient.Client
		Dim lstrKeyAddress As String
		
		Dim lclsTab_locat As New eGeneralForm.Tab_locat
		If lclsProfessional_Orders.Find_nServ(nServiceOrder) Then
			Me.sClient = lclsProfessional_Orders.sClient
			If lclsClient.Find(Me.sClient) Then
				Me.sClientName = lclsClient.sCliename
			End If
			Me.nProvider = lclsProfessional_Orders.nProvider
			lstrKeyAddress = sRecType & Me.sClient
			If lclsAddress.Find(lstrKeyAddress, 2, Today) Then
				Me.sAddress = lclsAddress.sStreet & " " & lclsAddress.sStreet1
				Me.sCity = lclsAddress.sZone
				Me.sState = lclsAddress.sprovi_des
			End If
			
			If lclsPhones.Find(lstrKeyAddress, 1, 2, Today) Then
				Me.sPhone = lclsPhones.sPhone
			End If
			
			Me.sName_Cont = lclsProfessional_Orders.sName_Cont
			Me.sAdd_Contact = lclsProfessional_Orders.sAdd_Contact
			Me.nMunicipality = lclsProfessional_Orders.nMunicipality
			Me.sPhone_Cont = lclsProfessional_Orders.sPhone_Cont
			If Me.nMunicipality <> 0 Then
				If lclsTab_locat.Find_by_municipality(Me.nMunicipality) Then
					Me.nLocal = lclsTab_locat.nLocal
					Me.nProvince = lclsTab_locat.nProvince
				End If
			End If
		End If
		
		lclsProfessional_Orders = Nothing
		lclsAddress = Nothing
		lclsPhones = Nothing
		lclsClient = Nothing
		lclsTab_locat = Nothing
		
	End Sub
	
	'% LocateTax: Localiza en la tabla Tax_fixval el valor del impuesto a manejar en la ventana - ACM - 22/07/2002
	Public Function LocateTax() As Double
		Dim lcolFixVals As New eAgent.Tax_fixvals
		Dim lclsFixVal As Object
		
		On Error GoTo LocateTax_err
		
		If lcolFixVals.Find(Today) Then
			For	Each lclsFixVal In lcolFixVals
				If lclsFixVal.sTypeTax = "1" Then 'Impuesto
					LocateTax = lclsFixVal.nPercent
					Exit For
				End If
			Next lclsFixVal
		Else
			LocateTax = 0
		End If
		
LocateTax_err: 
		If Err.Number Then
			LocateTax = 0
		End If
		
		On Error GoTo 0
		
		lcolFixVals = Nothing
		lclsFixVal = Nothing
		
	End Function

    '% insPostSI776: Ejecuta la actualización de registros sobre las tablas Buy_ord, Prof_ord y
    '%               Client, Address en caso de ser necesario - ACM - 23/07/2002
    Public Function insPostSI776(ByVal nClaimNumber As Double, ByVal nCaseNumber As Integer, ByVal nDemandant_Type As Integer, ByVal nServiceOrder As Integer, ByVal dOrderDate As String, ByVal sClientCode As String, ByVal nId As String, ByVal nQuantity_Parts As String, ByVal nAuto_part As String, ByVal sOriginal As String, ByVal nAmount_Order As String, ByVal nUsercode As Integer, Optional ByVal sName_Cont As String = "", Optional ByVal sPhone_Cont As String = "", Optional ByVal sAdd_Contact As String = "", Optional ByVal nMunicipality As Integer = 0, Optional ByVal nIVA As Double = 0, Optional ByVal nSendCost As Double = 0, Optional ByVal nFreightage As Double = 0, Optional ByVal nTransac As Integer = 0, Optional ByVal bFromSI774 As Boolean = False) As Boolean
        Dim lrecInsUpdate_Buy_Ord As New eRemoteDB.Execute
        Dim lclsProf_ord As Prof_ord
        Dim nServ_Order As Double

        On Error GoTo insPostSI776_err

        lclsProf_ord = New Prof_ord

        If lclsProf_ord.Find_nServ(nServiceOrder) Then

            lclsProf_ord.nTransac = 0
            lclsProf_ord.nServ_Order = 0
            lclsProf_ord.nStatus_ord = 3 '+ Realizada
            lclsProf_ord.nOrdertype = 1 '+ Orden de compra de repuestos
            lclsProf_ord.nAction = 1
            lclsProf_ord.sName_Cont = sName_Cont
            lclsProf_ord.sPhone_Cont = sPhone_Cont
            lclsProf_ord.sAdd_Contact = sAdd_Contact
            'lclsProf_ord.nMunicipality = nMunicipality
            'lclsProf_ord.nIVA = nIVA
            lclsProf_ord.nAmount = nAmount_Order
            lclsProf_ord.nSendCost = nSendCost
            lclsProf_ord.nFreightage = nFreightage
            lclsProf_ord.nQuotpart_order = nServiceOrder
            If bFromSI774 Then
                insPostSI776 = True
                lclsProf_ord.nServ_Order = nServiceOrder
            Else
                insPostSI776 = lclsProf_ord.Update_ProfOrdGeneric()
            End If
            Me.nServ_Order = lclsProf_ord.nServ_Order
                If insPostSI776 Then
                    With lrecInsUpdate_Buy_Ord
                        .StoredProcedure = "InsUpdate_Buy_Ord"
                        .Parameters.Add("nClaimNumber", nClaimNumber, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 10, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                        .Parameters.Add("nCaseNumber", nCaseNumber, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                        .Parameters.Add("nDemandant_Type", nDemandant_Type, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                        .Parameters.Add("nService_Order", lclsProf_ord.nServ_Order, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                        .Parameters.Add("dOrder_date", dOrderDate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 2500, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                        .Parameters.Add("sClientCode", sClientCode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 2500, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                        .Parameters.Add("nID", nId, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 2500, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                        .Parameters.Add("nQuantity_parts", nQuantity_Parts, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 250, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                        .Parameters.Add("nAuto_part", nAuto_part, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 2500, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                        .Parameters.Add("sOriginal", sOriginal, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 2500, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                        .Parameters.Add("nAmount", nAmount_Order, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 2500, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                        .Parameters.Add("nUserCode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                        .Parameters.Add("nTransac", nTransac, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 5, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                        .Parameters.Add("nServiceOrder", nServiceOrder, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 5, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

                        insPostSI776 = .Run(False)
                    End With
                End If
            End If

insPostSI776_err:
        If Err.Number Then
            insPostSI776 = False
        End If
        On Error GoTo 0
        lclsProf_ord = Nothing
    End Function

    '% CreateClientInformation: Crea la información básica del cliente en la tabla Client - ACM - 25/07/2002
    Public Function CreateClientInformation(ByVal sClientCode As String, ByVal sClientName As String, ByVal nUsercode As Integer) As Boolean
		Dim lclsClient As New eClient.Client
		Dim lclsClaim As eClaim.Claim = New eClaim.Claim
		
		On Error GoTo CreateClientInformation_err
		
		With lclsClient
			If Not .Find(sClientCode) Then
				.sClient = sClientCode
				.nUsercode = nUsercode
				.nPerson_typ = IIf(.nPerson_typ <> CDbl("2"), "1", .nPerson_typ)
				.nIncapacity = eRemoteDB.Constants.intNull
				.nArea = eRemoteDB.Constants.intNull
				.nIncap_cod = eRemoteDB.Constants.intNull
				.nNationality = eRemoteDB.Constants.intNull
				.nHealth_org = eRemoteDB.Constants.intNull
				.nAfp = eRemoteDB.Constants.intNull
				.nInvoicing = eRemoteDB.Constants.intNull
				.nLimitdriv = eRemoteDB.Constants.intNull
				.nDisability = eRemoteDB.Constants.intNull
				.nTypDriver = eRemoteDB.Constants.intNull
				.nHouse_type = eRemoteDB.Constants.intNull
				.sDigit = lclsClaim.CalcDigit(sClientCode)
				.sLastName = String.Empty
				.sLastName2 = String.Empty
				.sFirstName = sClientName
				.sCliename = sClientName
				Call .AddClient()
			End If
		End With
		
		lclsClient = Nothing
		
CreateClientInformation_err: 
		If Err.Number Then
			lclsClient = Nothing
			lclsClaim = Nothing
		End If
		
	End Function
End Class






