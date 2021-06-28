Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Imports System.Xml.Serialization
Imports System.IO
Imports System.Globalization

Public Class AccountStatement

    Public sKey As String

    Public CartolaGenerateFile As Boolean = True
    Public RootStatement As String

    Public Shared Function GenerateStatement(clientId As String,
                                             startingDate As Date,
                                             endingDate As Date) As String
        Dim instance As New AccountStatement With {.CartolaGenerateFile = False}
        If instance.insPostAGL621_K(nTyp_acco:=intNull,
                                    sType_Acc:="",
                                    sClient:=clientId,
                                    nIntermed:=intNull,
                                    ninterm_typ := intNull,
                                    nCurrency:=intNull,
                                    dOperDateIni:=startingDate,
                                    dOperDateEnd:=endingDate,
                                    nUsercode:=6329) Then

        End If

        Return instance.RootStatement
    End Function
	'%insValAGL621_K: Función que realiza la validacion de los datos introducidos en la sección de Encabezado
	Public Function insValAGL621_K(ByVal sCodispl As String, ByVal nTyp_acco As Integer, ByVal sType_Acc As String, ByVal sClient As String, ByVal nIntermed As Integer, ByVal nCurrency As Integer, ByVal dOperDateIni As Date, ByVal dOperDateEnd As Date, ByVal nUsercode As Integer) As String
		
		'+ dEffeclastProc : Fecha del último proceso
		Dim lclsErrors As eFunctions.Errors
		
		On Error GoTo insValAGL621_K_Err
		
		lclsErrors = New eFunctions.Errors
		
		'+ Debe indicar la fecha desde
		If dOperDateIni = dtmNull Then
			Call lclsErrors.ErrorMessage(sCodispl, 60217)
		End If

		'+ Debe indicar la fecha hasta
		If dOperDateEnd = dtmNull Then
			Call lclsErrors.ErrorMessage(sCodispl, 60218)
		End If
		
		'+ Si el proceso fue ejecutado para el rango de fechas se envía la advertencia
		If dOperDateIni <> dtmNull And dOperDateEnd <> dtmNull Then
			If dOperDateIni > dOperDateEnd Then
				Call lclsErrors.ErrorMessage(sCodispl, 60205)
			End If
		End If

		insValAGL621_K = lclsErrors.Confirm
		
insValAGL621_K_Err: 
		If Err.Number Then
			insValAGL621_K = "insValAGL621_K: " & Err.Description
		End If
		On Error GoTo 0
		lclsErrors = Nothing
	End Function

    'insPostAGL621_K: Método que realiza el proceso de extraccion de datos para la cartola
    Public Function insPostAGL621_K(ByVal nTyp_acco As Integer, ByVal sType_Acc As String, ByVal sClient As String, ByVal nIntermed As Integer, ByVal nInterm_Typ As Integer, ByVal nCurrency As Integer, ByVal dOperDateIni As Date, ByVal dOperDateEnd As Date, ByVal nUsercode As Integer) As Boolean

        Dim linsPostAGL621 As eRemoteDB.Execute

        linsPostAGL621 = New eRemoteDB.Execute

        With linsPostAGL621
            .StoredProcedure = "insAccountStatement"
            .Parameters.Add("nTyp_Acco", nTyp_acco, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sType_Acc", sType_Acc, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nIntermed", nIntermed, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nInterTyp", nInterm_Typ, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCurrency", nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dOperDateIni", dOperDateIni, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dOperDateEnd", dOperDateEnd, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sKey", "", eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            If .Run(False) Then
                insPostAGL621_K = True
                sKey = .Parameters("sKey").Value
                FillAccountHolders(sKey, dOperDateEnd)
            Else
                insPostAGL621_K = False
            End If
        End With


    End Function

    'FillAccountHolders: Método que realiza el proceso de extraccion de datos para la cartola
    Public Function FillAccountHolders(ByVal sKey As String, ByVal dOperDateEnd As Date) As Boolean

        Dim lAccountHolders As eRemoteDB.Execute

        lAccountHolders = New eRemoteDB.Execute


        With lAccountHolders
            .StoredProcedure = "ReaTemp_AccountHolder"
            .Parameters.Add("sKey", sKey, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            If .Run(True) Then
                FillAccountHolders = True

                Dim clsConfig = New eRemoteDB.VisualTimeConfig
                Dim strPathFile = clsConfig.LoadSetting("LoadFile", "C:\VISUALTIMENET\Temp\TFiles\", "Paths") 
                clsConfig = Nothing


                Dim serializer As New XmlSerializer(GetType(root))
                Dim objRoot As root
                Dim xmlName As String 

                Do While Not .EOF

                    objRoot = new root

                    FillDataList(objRoot)

                    objRoot.item = New List(Of item)

                    Dim item As New item With {.data = New data}

                    item.fileName = .FieldToClass("sClient")
                    item.language = "Español"

                    Dim objAccountHolder As New accountHolder

                    objAccountHolder.clientId = .FieldToClass("sClient")
                    objAccountHolder.name = .FieldToClass("sCliename")
                    objAccountHolder.KeyToAddressRecord = .FieldToClass("sKeyAddress")
                    If .FieldToClass("nRecowner") <> intNull Then
                        objAccountHolder.RecordOwner = .FieldToClass("nRecowner")
                    End If

                    objAccountHolder.address = .FieldToClass("sDescAdd")
                    objAccountHolder.AddressFirstLine = .FieldToClass("sStreet")
                    objAccountHolder.AddressSecondLine = .FieldToClass("sStreet1")
                    objAccountHolder.population = .FieldToClass("sPopulation")
                    If .FieldToClass("nMunicipality") <> intNull Then
                        objAccountHolder.municipality = .FieldToClass("nMunicipality")
                    End If
                    If .FieldToClass("nLocal") <> intNull Then
                        objAccountHolder.city = .FieldToClass("nLocal")
                    End If

                    If .FieldToClass("nProvince") <> intNull Then
                        objAccountHolder.state = .FieldToClass("nProvince")
                    End If

                    objAccountHolder.departmentNumber = .FieldToClass("sDepartment")
                    objAccountHolder.build = .FieldToClass("sBuild")
                    If .FieldToClass("nCountry") <> intNull Then
                        objAccountHolder.country = .FieldToClass("nCountry")
                    End If

                    If .FieldToClass("nZip_Code") <> intNull Then
                        objAccountHolder.country = .FieldToClass("nZip_Code")
                    End If
                    objAccountHolder.email = .FieldToClass("sE_Mail")

                    item.data.accountHolder = objAccountHolder

                    objRoot.item.Add(item)

                    FillAccountHolderPhones(sKey, item.data.accountHolder.clientId, objRoot)
                    FillConsumer(sKey, item.data.accountHolder.clientId, objRoot)
                    FillstatementInfo(sKey, item.data.accountHolder.clientId, objRoot, dOperDateEnd)
                    FillTransaction(sKey, item.data.accountHolder.clientId, objRoot)
                    FillOpremium(sKey, item.data.accountHolder.clientId, objRoot)
                    FillRewardsInfo(sKey, item.data.accountHolder.clientId, objRoot)

                    If CartolaGenerateFile Then
                        xmlName = strPathFile & .FieldToClass("scliename").ToString & "_" & .FieldToClass("sCurrency") & ".xml"
                        Dim w As New System.IO.StreamWriter(xmlName)
                        serializer.Serialize(w, objRoot)
                        w.Close()
                    Else
                        Using writer As New StringWriter(CultureInfo.InvariantCulture)
                            serializer.Serialize(writer, objRoot)
                            RootStatement = writer.ToString()
                        End Using
                    End If

                    .RNext()
                Loop
                .RCloseRec()
            Else
                FillAccountHolders = False
            End If
        End With

    End Function

    Public Function FillAccountHolderPhones(ByVal sKey As String, ByVal sClient As String, ByRef objRoot As root) As Boolean

        Dim lAccountHolderPhones As eRemoteDB.Execute

        lAccountHolderPhones = New eRemoteDB.Execute


        With lAccountHolderPhones
            .StoredProcedure = "ReaTemp_AccountHolderPhones"
            .Parameters.Add("sKey", sKey, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            If .Run(True) Then
                FillAccountHolderPhones = True

                Dim currentAccountHolder As accountHolder = (From item In objRoot.item Where item.data.accountHolder.clientId = sClient Select item.data.accountHolder).LastOrDefault

                currentAccountHolder.phones = New List(Of phone)

                Do While Not .EOF

                    Dim objAccountHolderPhone As New phone

                    objAccountHolderPhone.KeyToPhoneRecord = .FieldToClass("sKeyAddress")
                    objAccountHolderPhone.KeyPhone = .FieldToClass("nKeyPhones")
                    objAccountHolderPhone.RecordOwner = .FieldToClass("nRecowner")
                    objAccountHolderPhone.telephoneType = .FieldToClass("nPhone_Type")
                    objAccountHolderPhone.phoneNumber = .FieldToClass("sPhone")
                    objAccountHolderPhone.areaCode = .FieldToClass("nArea_Code")
                    If .FieldToClass("nExtens1") <> intNull Then
                        objAccountHolderPhone.Extension1 = .FieldToClass("nExtens1")
                    End If
                    If .FieldToClass("nExtens2") <> intNull Then
                        objAccountHolderPhone.Extension1 = .FieldToClass("nExtens2")
                    End If
                    objAccountHolderPhone.Order = .FieldToClass("nOrder")

                    currentAccountHolder.phones.Add(objAccountHolderPhone)

                    .RNext()
                Loop
                .RCloseRec()
            Else
                FillAccountHolderPhones = False

                Dim currentAccountHolder As accountHolder = (From item In objRoot.item Where item.data.accountHolder.clientId = sClient Select item.data.accountHolder).LastOrDefault

                currentAccountHolder.phones = New List(Of phone)

                Dim objAccountHolderPhone As New phone

                objAccountHolderPhone.KeyToPhoneRecord = ""
                objAccountHolderPhone.KeyPhone = 0
                objAccountHolderPhone.RecordOwner = 0
                objAccountHolderPhone.telephoneType = 0
                objAccountHolderPhone.phoneNumber = ""
                objAccountHolderPhone.areaCode = 0
                objAccountHolderPhone.Extension1 = 0
                objAccountHolderPhone.Extension2 = 0
                objAccountHolderPhone.Order = 0

                currentAccountHolder.phones.Add(objAccountHolderPhone)

            End If
        End With

    End Function

    ''' <summary>
    ''' Método que realiza el proceso de extraccion de datos para la cartola
    ''' </summary>
    ''' <param name="sKey"></param>
    ''' <param name="sClient"></param>
    ''' <param name="objRoot"></param>
    ''' <param name="dOperDateEnd"></param>
    Public Function FillstatementInfo(ByVal sKey As String, ByVal sClient As String, ByRef objRoot As root, ByVal dOperDateEnd As Date) As Boolean

        Dim lstatementInfo As eRemoteDB.Execute

        lstatementInfo = New eRemoteDB.Execute


        With lstatementInfo
            .StoredProcedure = "ReaTemp_StatementInfo"
            .Parameters.Add("sKey", sKey, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            If .Run(True) Then

                FillstatementInfo = True

                Dim currentData As data = (From item In objRoot.item Where item.data.accountHolder.clientId = sClient Select item.data).LastOrDefault

                currentData.statementInfo = New statementInfo
                currentData.statementInfo.accountNo = .FieldToClass("sAccount")
                currentData.statementInfo.currency = .FieldToClass("ncurrency")
                currentData.statementInfo.statementDate = dOperDateEnd
                If .FieldToClass("nNewCommisions") <> intNull Then
                    currentData.statementInfo.newCommisions = .FieldToClass("nNewCommisions")
                End If
                If .FieldToClass("nCommisionsRefounds") <> intNull Then
                    currentData.statementInfo.commisionsRefounds = .FieldToClass("nCommisionsRefounds")
                End If
                If .FieldToClass("nPaymentsAndCredits") <> intNull Then
                    currentData.statementInfo.paymentsAndCredits = .FieldToClass("nPaymentsAndCredits")
                End If
                If .FieldToClass("nBalance") <> intNull Then
                    currentData.statementInfo.closingBalance = .FieldToClass("nBalance")
                End If
            Else
                FillstatementInfo = False
            End If
        End With

    End Function

    ''' <summary>
    ''' Método que realiza el proceso de extraccion de datos para la cartola
    ''' </summary>
    ''' <param name="sKey"></param>
    ''' <param name="sClient"></param>
    ''' <param name="objRoot"></param>
    Public Function FillTransaction(ByVal sKey As String, ByVal sClient As String, ByRef objRoot As root) As Boolean

        Dim lTransactions As eRemoteDB.Execute

        lTransactions = New eRemoteDB.Execute


        With lTransactions
            .StoredProcedure = "ReaTemp_Transaction"
            .Parameters.Add("sKey", sKey, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            If .Run(True) Then

                FillTransaction = True

                Dim currentData As data = (From item In objRoot.item Where item.data.accountHolder.clientId = sClient Select item.data).LastOrDefault

                currentData.transactions = New List(Of transaction)

                Do While Not .EOF

                    Dim itemTransaction As New transaction

                    itemTransaction.id = .FieldToClass("nId")
                    itemTransaction.date = .FieldToClass("dDate")

                    itemTransaction.category = .FieldToClass("nCategory")
                    itemTransaction.lineofbusiness = .FieldToClass("nBranch")
                    itemTransaction.product = .FieldToClass("nProduct")
                    itemTransaction.amount = .FieldToClass("nAmount")

                    itemTransaction.clientId = .FieldToClass("sClientId")
                    itemTransaction.name = .FieldToClass("sName")
                    itemTransaction.phone = .FieldToClass("sPhone")
                    itemTransaction.completeaddress = .FieldToClass("sCompleteAddress")


                    itemTransaction.number = .FieldToClass("nNumber")
                    itemTransaction.dateBegin = .FieldToClass("dDateBegin")
                    itemTransaction.dateEnd = .FieldToClass("dDateEnd")
                    itemTransaction.insuredCapital = .FieldToClass("nInsuredCapital")
                    itemTransaction.details = .FieldToClass("sDetails")

                    itemTransaction.billId = .FieldToClass("nBillId")
                    itemTransaction.issueDate = .FieldToClass("dIssueDate")
                    itemTransaction.endingdate = .FieldToClass("dExpirdat")
                    itemTransaction.paymentDate = .FieldToClass("dPaymentDate")

                    itemTransaction.share = .FieldToClass("nShare")
                    itemTransaction.description = .FieldToClass("sDescript")


                    currentData.transactions.Add(itemTransaction)

                    .RNext()
                Loop
                .RCloseRec()
                For Each item As transaction In currentData.transactions
                    item.billItem = FillTransactionBillItem(sKey, sClient, item.billId, objRoot)
                Next
            Else
                FillTransaction = False
            End If
        End With


    End Function

    Public Function FillTransactionBillItem(ByVal sKey As String, ByVal sClient As String, ByVal billId As String, ByRef objRoot As root) As List(Of billItem)
        Dim result As List(Of billItem) = Nothing
        With New eRemoteDB.Execute
            .StoredProcedure = "ReaTemp_BillItem"
            .Parameters.Add("sKey", sKey, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nReceipt", billId, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If .Run(True) Then
                result = New List(Of billItem)
                Do While Not .EOF

                    Dim objBillItem As New billItem

                    objBillItem.BillingItem = .FieldToClass("nBillingItem")
                    objBillItem.Description = .FieldToClass("sBillingItemDescript")
                    objBillItem.TypeofDetailRecord = .FieldToClass("sTypeofDetailRecord")
                    objBillItem.DetailItemCode = .FieldToClass("nDetailItemCode")

                    'objBillItem.PremiumAmountSpecified = True 

                    'objBillItem.CommisionAmountSpecified = .FieldToClass("sDetails")
                    objBillItem.CommissionPercentage = .FieldToClass("CommissionPercentage")
                    objBillItem.CommisionAmount = .FieldToClass("nCommisionAmount")
                    objBillItem.PremiumAmount = .FieldToClass("nPremiumAmount")

                    result.Add(objBillItem)

                    'Dim serializer3 As New XmlSerializer(GetType(billItem))
                    'Using writer As New StringWriter(CultureInfo.InvariantCulture)
                    '    serializer3.Serialize(writer, objBillItem)
                    '    RootStatement = writer.ToString()
                    'End Using

                    '                   objBillItem.CommissionDetail = FillTransactionBillItemCommissionDetail(sKey, sClient, billId, objBillItem.DetailItemCode, objRoot)

                    .RNext()
                Loop
                .RCloseRec()
            End If
        End With
        Return result
    End Function

    Public Function FillTransactionBillItemCommissionDetail(ByVal sKey As String, ByVal sClient As String, ByVal billId As String, ByVal DetailItemCode As String, ByRef objRoot As root) As List(Of CommissionDetail)
        Dim result As List(Of CommissionDetail) = Nothing
        With New eRemoteDB.Execute
            '.StoredProcedure = "ReaTemp_AccountHolderPhones"
            '.Parameters.Add("sKey", sKey, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            '.Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            'If .Run(True) Then
            If 1 = 1 Then
                result = New List(Of CommissionDetail)

                'Do While Not .EOF

                Dim objCommissionDetail As New CommissionDetail

                objCommissionDetail.CodeofProducer = String.Empty
                objCommissionDetail.Hierarchylevel = 0
                objCommissionDetail.HierarchylevelSpecified = True
                objCommissionDetail.Type = String.Empty
                objCommissionDetail.CommissionPercentage = 0
                objCommissionDetail.CommissionPercentageSpecified = True
                objCommissionDetail.Commission = 0

                result.Add(objCommissionDetail)

                '    .RNext()
                'Loop
                '.RCloseRec()

            End If
        End With
        Return result
    End Function

    'FillOpremium: Método que realiza el proceso de extraccion de datos para la cartola
    Public Function FillOpremium(ByVal sKey As String, ByVal sClient As String, ByRef objRoot As root) As Boolean

        Dim lOpremiums As eRemoteDB.Execute

        lOpremiums = New eRemoteDB.Execute

        With lOpremiums
            .StoredProcedure = "ReaTemp_OPremiums"
            .Parameters.Add("sKey", sKey, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            If .Run(True) Then

                FillOpremium = True

                Dim currentData As data = (From item In objRoot.item Where item.data.accountHolder.clientId = sClient Select item.data).LastOrDefault

                currentData.opremiums = New List(Of opremium)

                Do While Not .EOF

                    Dim itemOpremium As New opremium

                    itemOpremium.id = .FieldToClass("nId")
                    itemOpremium.date = .FieldToClass("dDate")
                    itemOpremium.name = .FieldToClass("sName")
                    itemOpremium.category = .FieldToClass("nCategory")
                    itemOpremium.product = .FieldToClass("nProduct")
                    itemOpremium.commision = .FieldToClass("nCommision")
                    itemOpremium.clientId = .FieldToClass("sClientId")
                    itemOpremium.number = .FieldToClass("nNumber")
                    itemOpremium.dateBegin = .FieldToClass("dDateBegin")
                    itemOpremium.dateEnd = .FieldToClass("dDateEnd")
                    itemOpremium.insuredCapital = .FieldToClass("nInsuredCapital")
                    itemOpremium.details = .FieldToClass("sDetails")
                    itemOpremium.billId = .FieldToClass("nBillId")
                    itemOpremium.issueDate = .FieldToClass("dIssueDate")
                    itemOpremium.paymentDate = .FieldToClass("dPaymentDate")

                    currentData.opremiums.Add(itemOpremium)

                    FillOpremiumBillItem(sKey, sClient, itemOpremium.billId, objRoot)

                    .RNext()
                Loop
                .RCloseRec()
            Else
                FillOpremium = False
            End If
        End With

    End Function

    Public Function FillOpremiumBillItem(ByVal sKey As String, ByVal sClient As String, ByVal billId As String, ByRef objRoot As root) As Boolean

        Dim lOpremiumBillItems As eRemoteDB.Execute

        lOpremiumBillItems = New eRemoteDB.Execute

        With lOpremiumBillItems
            .StoredProcedure = "ReaTemp_BillItem"
            .Parameters.Add("sKey", sKey, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nReceipt", billId, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            If .Run(True) Then
                FillOpremiumBillItem = True

                Dim currentData As data = (From item In objRoot.item Where item.data.accountHolder.clientId = sClient Select item.data).LastOrDefault

                Dim currentOpremium As opremium = (From opremium In currentData.opremiums Where opremium.billId = billId Select opremium).LastOrDefault

                currentOpremium.billItem = New List(Of billItem)

                Do While Not .EOF

                    Dim objBillItem As New billItem

                    objBillItem.BillingItem = .FieldToClass("nBillingItem")
                    objBillItem.TypeofDetailRecord = .FieldToClass("sTypeofDetailRecord")
                    objBillItem.DetailItemCode = .FieldToClass("nDetailItemCode")
                    objBillItem.PremiumAmount = .FieldToClass("nPremiumAmount")
                    'objBillItem.PremiumAmountSpecified = True 
                    objBillItem.CommisionAmount = .FieldToClass("nCommisionAmount")
                    'objBillItem.CommisionAmountSpecified = .FieldToClass("sDetails")

                    currentOpremium.billItem.Add(objBillItem)

                    FillOpremiumBillItemCommissionDetail(sKey, sClient, billId, objBillItem.DetailItemCode, objRoot)

                    .RNext()
                Loop
                .RCloseRec()
            Else
                FillOpremiumBillItem = False
            End If
        End With

    End Function

    Public Function FillOpremiumBillItemCommissionDetail(ByVal sKey As String, ByVal sClient As String, ByVal billId As String, ByVal DetailItemCode As String, ByRef objRoot As root) As Boolean

        Dim lBillItemCommissionDetails As eRemoteDB.Execute

        lBillItemCommissionDetails = New eRemoteDB.Execute


        With lBillItemCommissionDetails
            '.StoredProcedure = "ReaTemp_AccountHolderPhones"
            '.Parameters.Add("sKey", sKey, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            '.Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            'If .Run(True) Then
            If 1 = 1 Then

                FillOpremiumBillItemCommissionDetail = True

                Dim currentData As data = (From item In objRoot.item Where item.data.accountHolder.clientId = sClient Select item.data).LastOrDefault

                Dim currentOpremium As opremium = (From opremium In currentData.opremiums Where opremium.billId = billId Select opremium).LastOrDefault

                Dim currentBillItem As billItem = (From billItem In currentOpremium.billItem Where billItem.DetailItemCode = DetailItemCode Select billItem).LastOrDefault

                currentBillItem.CommissionDetail = New List(Of CommissionDetail)

                'Do While Not .EOF

                Dim objCommissionDetail As New CommissionDetail

                objCommissionDetail.CodeofProducer = String.Empty
                objCommissionDetail.Hierarchylevel = 0
                objCommissionDetail.HierarchylevelSpecified = True
                objCommissionDetail.Type = String.Empty
                objCommissionDetail.CommissionPercentage = 0
                objCommissionDetail.CommissionPercentageSpecified = True
                objCommissionDetail.Commission = 0

                currentBillItem.CommissionDetail.Add(objCommissionDetail)

                '    .RNext()
                'Loop
                '.RCloseRec()
            Else
                FillOpremiumBillItemCommissionDetail = False
            End If
        End With


    End Function

    'FillAccountHolders: Método que realiza el proceso de extraccion de datos para la cartola
    Public Function FillRewardsInfo(ByVal sKey As String, ByVal sclient As String, ByRef objRoot As root) As Boolean

        Dim lRewardsInfo As eRemoteDB.Execute

        lRewardsInfo = New eRemoteDB.Execute


        'With lRewardsInfo
        '    .StoredProcedure = "ReaTemp_AccountHolder"
        '    .Parameters.Add("sKey", sKey, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

        '    If .Run(True) Then
        If 1 = 1 Then
            FillRewardsInfo = True

            Dim currentData As data = (From item In objRoot.item Where item.data.accountHolder.clientId = sclient Select item.data).LastOrDefault

            currentData.rewardsInfo = New List(Of rewardInfo)

            For i As Integer = 1 To 4
                Dim itemRewardsInfo As New rewardInfo

                If i = 1 Then
                    itemRewardsInfo.contest = "El Productor del año"
                    itemRewardsInfo.position = "3"
                    itemRewardsInfo.tickets = "No aplica"
                End If

                If i = 2 Then
                    itemRewardsInfo.contest = "Vida Dorada"
                    itemRewardsInfo.position = "6"
                    itemRewardsInfo.tickets = "No aplica"
                End If

                If i = 3 Then
                    itemRewardsInfo.contest = "Corre Seguro"
                    itemRewardsInfo.position = "No aplica"
                    itemRewardsInfo.tickets = "24"
                End If

                If i = 4 Then
                    itemRewardsInfo.contest = "A su Salud"
                    itemRewardsInfo.position = "2"
                    itemRewardsInfo.tickets = "No aplica"
                End If

                currentData.rewardsInfo.Add(itemRewardsInfo)
            Next
        Else
            FillRewardsInfo = False
        End If
        'End With

    End Function
    'Public Function FillPoints(ByVal sKey As String, ByVal sClient As String, ByRef objRoot As root) As Boolean

    '    Dim lPoints As eRemoteDB.Execute

    '    lPoints = New eRemoteDB.Execute

    '    Try

    '    With lPoints
    '        '.StoredProcedure = "ReaTemp_AccountHolderPhones"
    '        '.Parameters.Add("sKey", sKey, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
    '        '.Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

    '        'If .Run(True) Then
    '        If 1 = 1

    '            FillPoints = True

    '            Dim currentData As Data = (From item In objRoot.item Where item.data.accountHolder.clientId = sClient Select item.data).LastOrDefault

    '            currentData.rewardsInfo.points = New List(Of bonusPoints) 

    '            'Do While Not .EOF

    '                Dim objBonusPoint As New bonusPoints 

    '                objBonusPoint.type = 1
    '                objBonusPoint.Value = 1

    '                currentData.rewardsInfo.points.Add(objBonusPoint)

    '            '    .RNext()
    '            'Loop
    '            '.RCloseRec()
    '        Else
    '            FillPoints = False
    '        End If
    '    End With

    '    Catch ex As Exception
    '        If Err.Number Then
    '            FillPoints = False
    '        End If
    '        lPoints = Nothing
    '    End Try
    'End Function

    'Public Function FillHistory(ByVal sKey As String, ByVal sClient As String, ByRef objRoot As root) As Boolean

    '    Dim lHistory As eRemoteDB.Execute

    '    lHistory = New eRemoteDB.Execute

    '    Try

    '    With lHistory
    '        '.StoredProcedure = "ReaTemp_AccountHolderPhones"
    '        '.Parameters.Add("sKey", sKey, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
    '        '.Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

    '        'If .Run(True) Then
    '        If 1 = 1

    '            FillHistory = True

    '            Dim currentData As Data = (From item In objRoot.item Where item.data.accountHolder.clientId = sClient Select item.data).LastOrDefault

    '            currentData.rewardsInfo.history = New List(Of reward) 

    '            'Do While Not .EOF

    '                Dim objreward As New reward 

    '                objreward.currentTotal = String.Empty  
    '                objreward.month = "Agosto"
    '                objreward.previousTotal = 0

    '                currentData.rewardsInfo.history.Add(objreward)

    '            '    .RNext()
    '            'Loop
    '            '.RCloseRec()
    '        Else
    '            FillHistory = False
    '        End If
    '    End With

    '    Catch ex As Exception
    '        If Err.Number Then
    '            FillHistory = False
    '        End If
    '        lHistory = Nothing
    '    End Try
    'End Function

    'FillDataList: Método que realiza el proceso de extraccion de datos para la cartola
    Public Function FillDataList(ByRef objRoot As root) As Boolean
        Dim Table As New eFunctions.Tables


        FillDataList = True


        Dim currentRoot As root = objRoot


        currentRoot.dataList = New dataList
        
        'Region - CountryList - Table66
        'Ciudad - ProvinceList - TabProvince
        'Comuna - MunicipalityList - TabMunicipality

        'Region - ProvinceList - TabProvince   select * from province 
        'Ciudad - select * from tab_locat where nprovince = 4 order by sdescript
        'Zone   - MunicipalityList - TabMunicipality select * from Municipality where nlocal = 35
        

        currentRoot.dataList.CountryList = LoadLookUp("Table66", "nCountry", "sDescript")

        'Region
        currentRoot.dataList.ProvinceList = LoadLookUp("Tab_Province", "nProvince", "sDescript")

        'City
        currentRoot.dataList.CityList = New List(Of LookUp)
        With New eRemoteDB.Query
            If .OpenQuery("tab_locat", "nLocal,sDescript,nProvince") Then
                Do While Not .EndQuery
                    Dim itemTable As New LookUp

                    itemTable.Code = .FieldToClass("nLocal")
                    itemTable.Description = .FieldToClass("sDescript")
                    itemTable.ParentId = .FieldToClass("nProvince")

                    currentRoot.dataList.CityList.Add(itemTable)
                    .NextRecord()
                Loop

            End If
        End With
        'Zone
        'currentRoot.dataList.MunicipalityList = LoadLookUp("TabMunicipality", "nMunicipality", "sDescript")
        currentRoot.dataList.MunicipalityList = New List(Of LookUp)
        With New eRemoteDB.Query
            If .OpenQuery("Municipality", "nMunicipality,sDescript,nLocal") Then
                Do While Not .EndQuery
                    Dim itemTable As New LookUp

                    itemTable.Code = .FieldToClass("nMunicipality")
                    itemTable.Description = .FieldToClass("sDescript")
                    itemTable.ParentId = .FieldToClass("nLocal")

                    currentRoot.dataList.MunicipalityList.Add(itemTable)
                    .NextRecord()
                Loop

            End If
        End With




        currentRoot.dataList.TelephoneTypeList = LoadLookUp("Table564", "nPhone_Type", "sShort_des")
        '       currentRoot.dataList.LineOfBusinessList = LoadLookUp("Table10", "nBranch", "sDescript")

        currentRoot.dataList.PaymentFrequencyList = LoadLookUp("Table36", "nPayFreq", "sDescript")
        currentRoot.dataList.Currency = LoadLookUp("Table11", "nCodigInt", "sDescript")

        currentRoot.dataList.SexList = New List(Of LookUpAlphaNumeric)
        With New eFunctions.Tables
            If (.reaTable("Table18")) Then
                Do While Not .EOF
                    Dim itemTable As New LookUpAlphaNumeric

                    itemTable.Code = .Fields("sSexClien")
                    itemTable.Description = .Fields("sShort_des")

                    currentRoot.dataList.SexList.Add(itemTable)
                    .NextRecord()
                Loop

            End If
        End With

        currentRoot.dataList.LineOfBusinessList = New List(Of LookUpLineOfBusiness)
        With New eFunctions.Tables
            If (.reaTable("Table10")) Then
                Do While Not .EOF
                    Dim itemTable As New LookUpLineOfBusiness

                    itemTable.Code = .Fields("nBranch")
                    itemTable.Description = .Fields("sShort_des")
                    itemTable.LineOfBusinestechnical = "0"

                    currentRoot.dataList.LineOfBusinessList.Add(itemTable)
                    .NextRecord()
                Loop
            End If
        End With


        currentRoot.dataList.ProductList = New List(Of LookUpProduct)
        With New eFunctions.Tables
            .Parameters.Add("nBranch", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If (.reaTable("TabProdMaster5")) Then
                Do While Not .EOF
                    Dim itemTable As New LookUpProduct

                    itemTable.ParentId = .Fields("nBranch")
                    itemTable.Code = .Fields("nProduct")
                    itemTable.Description = .Fields("sShort_Des")
                    itemTable.LineOfBusinestechnical = .Fields("sBrancht")

                    currentRoot.dataList.ProductList.Add(itemTable)
                    .NextRecord()
                Loop
            End If
        End With

        If Not IsNothing(currentRoot.dataList.LineOfBusinessList) AndAlso
           Not IsNothing(currentRoot.dataList.ProductList) Then
            For Each branch As LookUpLineOfBusiness In currentRoot.dataList.LineOfBusinessList
                For Each product As LookUpProduct In currentRoot.dataList.ProductList
                    If product.ParentId = branch.Code Then
                        branch.LineOfBusinestechnical = product.LineOfBusinestechnical
                        Exit For
                    End If
                Next
            Next
        End If

        currentRoot.dataList.VehiclesList = New List(Of LookUpAlphaNumeric)
        With New eFunctions.Tables
            .Parameters.Add("sStatRegt", "1", eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If (.reaTable("TabTab_Au_Veh")) Then
                ' Do While Not .EOF
                Dim itemTable As New LookUpAlphaNumeric

                itemTable.Code = .Fields("sVehCode")
                itemTable.Description = .Fields("sDescript")

                currentRoot.dataList.VehiclesList.Add(itemTable)
                '    .NextRecord   
                'Loop
            End If
        End With



    End Function


    Private Function LoadLookUp(tableName As String, codeField As String, descriptField As String) As List(Of LookUp)
        Dim result As New List(Of LookUp)
        Dim itemTable As LookUp

        Try
            With New eFunctions.Tables
                If .reaTable(tableName) Then
                    Do While Not .EOF
                        itemTable = New LookUp
                        itemTable.Code = .Fields(codeField)
                        itemTable.Description = .Fields(descriptField)

                        result.Add(itemTable)
                        .NextRecord()
                    Loop
                End If
            End With
        Catch ex As Exception
            Throw New Exception(String.Format("LoadLookUp('{0}','{1}','{2}')", tableName, codeField, descriptField), ex)
        End Try
        Return result
    End Function

    'FillConsumer: Método que realiza el proceso de extraccion de datos para la cartola
    Public Function FillConsumer(ByVal sKey As String, ByVal sClient As String, ByRef objRoot As root) As Boolean



        FillConsumer = True

        Dim currentItem As item = (From item In objRoot.item Where item.data.accountHolder.clientId = sClient Select item).LastOrDefault

        currentItem.consumerInformation = New ConsumerInformation With {.CompanyId = 1,
                                                                       .Version = EnumVersion.LatCombined,
                                                                       .UserInitials = "nsoler",
                                                                       .UserPassword = "6329255"}

        'currentItem.consumerInformation.Security = New SecurityInformation With {.BranchOffice = 1,
        '                                                                        .CompanyType = "0",
        '                                                                        .Schema = "EASE1",
        '                                                                        .SecurityLevel = 1,
        '                                                                        .Usercode = 1}


    End Function

End Class
