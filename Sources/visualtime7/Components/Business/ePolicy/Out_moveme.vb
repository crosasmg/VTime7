Option Strict Off
Option Explicit On
Public Class Out_moveme
    '%-------------------------------------------------------%'
    '% $Workfile:: Out_moveme.cls                           $%'
    '% $Author:: Nvaplat41                                  $%'
    '% $Date:: 9/10/03 19.01                                $%'
    '% $Revision:: 40                                       $%'
    '%-------------------------------------------------------%'

    '**+ Properties according the table in the system 11/17/2000
    '**+ The key fields are sCertype, nBranch, nProduct, nPolicy, nCertif, nMovnumbe y nDigit.
    '+ Propiedades según la tabla en el sistema al 17/11/2000.
    '+ Los campos llave de la tabla corresponden a: sCertype, nBranch, nProduct, nPolicy, nCertif, nMovnumbe y nDigit.
    '   Column_name                    Type     Computed  Length  Prec  Scale Nullable     TrimTrailingBlanks     FixedLenNullInSource
    Public sCertype As String 'char       no        1                   no              no                       no
    Public nBranch As Integer 'smallint   no        2       5     0     no              (n/a)                    (n/a)
    Public nProduct As Integer 'smallint   no        2       5     0     no              (n/a)                    (n/a)
    Public nPolicy As Double 'int        no        4      10     0     no              (n/a)                    (n/a)
    Public nCertif As Double 'int        no        4      10     0     no              (n/a)                    (n/a)
    Public nMovnumbe As Integer 'int        no        4      10     0     no              (n/a)                    (n/a)
    Public nDigit As Integer 'smallint   no        2       5     0     no              (n/a)                    (n/a)
    Public nCapital As Double 'decimal    no        9      12     0     yes             (n/a)                    (n/a)
    Public nCurrency As Integer 'smallint   no        2       5     0     yes             (n/a)                    (n/a)
    Public nExchange As Double 'decimal    no        9      10     6     yes             (n/a)                    (n/a)
    Public dExpirdat As Date 'datetime   no        8                   yes             (n/a)                    (n/a)
    Public nPremium As Double 'decimal    no        9      10     2     yes             (n/a)                    (n/a)
    Public sStatus_mov As String 'char       no        1                   yes             no                       yes
    Public nTaxamou As Double 'decimal    no        9      10     2     yes             (n/a)                    (n/a)
    Public nTratypei As Integer 'smallint   no        2       5     0     yes             (n/a)                    (n/a)
    Public nUsercode As Integer 'smallint   no        2       5     0     yes             (n/a)                    (n/a)
    Public nYear_month As Integer 'int        no        4      10     0     yes             (n/a)                    (n/a)
    Public sZone As Integer 'smallint   no        2       5     0     yes             (n/a)                    (n/a)
    Public nProvince As Integer 'smallint   no        2       5     0     yes             (n/a)                    (n/a)
    Public dStartdate As Date 'datetime   no        8                   yes             (n/a)                    (n/a)
    '**- Auxiliary variables
    '- Variables auxialiares

    Public nCommi_rate As Double
    Public sType_detai As String
    Public nCommision As Double
    Public nPremAnual As Double
    Public sDescript As String
    Public nBill_item As Integer
    Public nAmountAf As Double
    Public nAmountEx As Double
    Public mdblPremium As Double
    Public dEffecdate As Date
    Public nSel As Integer
    Public nReceipt As Double

    '**- Array definition. This array will hold the premium invoices
    '- Arreglo para la carga de recibos
    Private Structure udtOut_moveme
        Dim dStartdate As Date
        Dim dExpirDate As Date
        Dim nCurrency As Integer
        Dim nCommi_rate As Double
        Dim sType_detai As String
        Dim nCommision As Double
        Dim nPremium As Double
        Dim nPremAnual As Double
        Dim sDescript As String
        Dim nBill_item As Integer
        Dim nAmountAf As Double
        Dim nAmountEx As Double
    End Structure

    Private marrReceipts() As udtOut_moveme

    '**- Variable definition. This variable will be used to know if the premium invoices array was loaded
    '- Indica si el arreglo de recibos se cargo o no
    Private mblnCharge As Boolean
    Private lstrTratypei As String
    Private ldtmEffecdate As Date
    Private ldtmNulldate As Date
    Private mlngReceipt As Integer
    Private mintBranch As Integer
    Private mintProduct As Integer
    Private mlngPolicy As Integer
    Private mlngCertif As Integer
    Private mintInsur_area As Integer
    Private mlngWay_Pay As Integer
    Private mIntId_bill As Integer
    Private mlngrel_Idbill As Integer
    Private mstrAddtax As String
    Private mstrClient As String


    '**% CountReceipts: Returns the quantity of premium invoices in the array
    '% CountReceipts: Devuelve el número de recibos que se encuentran en el arreglo
    Public ReadOnly Property CountReceipts() As Integer
        Get

            If mblnCharge Then
                CountReceipts = UBound(marrReceipts)
            Else
                CountReceipts = -1
            End If
        End Get
    End Property

    '**% LoadReceipts: Returns the billing items of the issued premium invoices
    '% LoadReceipts: Devuelve los conceptos de facturación de los recibos emitidos
    Public Function LoadReceipts(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, ByVal nTratypei As Integer, ByVal nMovnumbe As Integer) As Boolean

        '**- Variable definition. lrecreaout_moveme_a
        '- Se define la variable lrecreaout_moveme_a

        Dim lrecreaout_moveme_a As eRemoteDB.Execute
        Dim llngIndex As Integer

        On Error GoTo LoadReceipts_Err

        lrecreaout_moveme_a = New eRemoteDB.Execute

        '**+Stored procedure parameters definition 'insudb.reaCountReceiptPend'
        '**+Data of 11/17/2000 14:25:23
        '+ Definición de parámetros para stored procedure 'insudb.reaout_moveme_a'
        '+ Información leída el 17/11/2000 14:25:23

        With lrecreaout_moveme_a
            .StoredProcedure = "reaout_moveme_a"
            .Parameters.Add("scertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nTratypei", nTratypei, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nMovNumbe", nMovnumbe, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            If .Run Then
                llngIndex = -1
                LoadReceipts = True
                mblnCharge = True
                ReDim marrReceipts(100)

                Do While Not .EOF
                    llngIndex = llngIndex + 1
                    marrReceipts(llngIndex).dExpirDate = .FieldToClass("dExpirDate")
                    marrReceipts(llngIndex).dStartdate = .FieldToClass("dStartDate")
                    marrReceipts(llngIndex).nBill_item = .FieldToClass("nBill_item")
                    marrReceipts(llngIndex).nCommi_rate = .FieldToClass("nCommi_rate")
                    marrReceipts(llngIndex).nCommision = .FieldToClass("nCommision")
                    marrReceipts(llngIndex).nCurrency = .FieldToClass("nCurrency")
                    marrReceipts(llngIndex).nPremAnual = .FieldToClass("nPremAnual")
                    marrReceipts(llngIndex).nPremium = .FieldToClass("nPremium")
                    marrReceipts(llngIndex).sDescript = .FieldToClass("sDescript")
                    marrReceipts(llngIndex).nAmountAf = .FieldToClass("nAmountAf")
                    marrReceipts(llngIndex).nAmountEx = .FieldToClass("nAmountEx")
                    .RNext()
                Loop

                .RCloseRec()
                ReDim Preserve marrReceipts(llngIndex)
            Else
                LoadReceipts = False
                mblnCharge = False
            End If
        End With

        'UPGRADE_NOTE: Object lrecreaout_moveme_a may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecreaout_moveme_a = Nothing

LoadReceipts_Err:
        If Err.Number Then
            LoadReceipts = False
        End If

        On Error GoTo 0

    End Function

    '**% ReceiptItem: Loads the information of a premium invoice in the class variables
    '% ReceiptItem: Carga en las variables de la clase la información de un recibo
    Public Function ReceiptItem(ByVal llngIndex As Integer) As Boolean

        If mblnCharge Then
            If llngIndex <= UBound(marrReceipts) Then
                With marrReceipts(llngIndex)
                    dExpirdat = .dExpirDate
                    dStartdate = .dStartdate
                    nBill_item = .nBill_item
                    nCommi_rate = .nCommi_rate
                    nCommision = .nCommision
                    nCurrency = .nCurrency
                    nPremAnual = .nPremAnual
                    nPremium = .nPremium
                    sDescript = .sDescript
                    sType_detai = .sType_detai
                    nAmountAf = .nAmountAf
                    nAmountEx = .nAmountEx
                End With

                ReceiptItem = True
            Else
                ReceiptItem = False
            End If
        End If
    End Function

    '% insValCA036A: valida los datos de la página
    Public Function insValCA036A(ByVal sCodispl As String, ByVal nRecordSelected As Integer) As String
        Dim lclsErrors As eFunctions.Errors

        On Error GoTo insValCA036A_Err

        lclsErrors = New eFunctions.Errors

        With lclsErrors
            If nRecordSelected = 0 Then
                Call .ErrorMessage(sCodispl, 1047)
            End If

            insValCA036A = .Confirm
        End With

insValCA036A_Err:
        If Err.Number Then
            insValCA036A = "insValCA036A: " & Err.Description
        End If
        On Error GoTo 0
        'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsErrors = Nothing
    End Function

    '% insValCA036: valida los datos de la página
    Public Function insValCA036(ByVal sCodispl As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal sClient As String, ByVal sColinvot As String, ByVal nCurrency As Integer, ByVal dDateStart As Date, ByVal dDateEnd As Date, ByVal dLedgerDate As Date, ByVal nCompany As Integer) As String
        Dim lclsErrors As eFunctions.Errors
        Dim lclsLed_compan As eLedge.Led_compan
        Dim lclsctrol_date As eGeneral.Ctrol_date
        Dim lclsRoles As ePolicy.Roles
        Dim lclsPolicy As ePolicy.Policy
        Dim Bdate As Boolean

        On Error GoTo insValCA036_Err

        lclsErrors = New eFunctions.Errors
        lclsLed_compan = New eLedge.Led_compan
        lclsctrol_date = New eGeneral.Ctrol_date
        lclsPolicy = New Policy
        Bdate = False

        With lclsErrors
            '+ Validaciones del campo Cliente
            If sClient = String.Empty Then
                '+ Si la facturación de la póliza no es por situación de riesgo, debe estar lleno
                If sColinvot <> "3" Then
                    Call .ErrorMessage(sCodispl, 3859)
                End If
            Else
                lclsPolicy.Find("2", nBranch, nProduct, nPolicy)
                If dDateStart < lclsPolicy.dStartdate And dDateStart <> eRemoteDB.Constants.dtmNull Then
                    '+ Fecha consultada debe ser posterior a la emision de la poliza
                    Call .ErrorMessage(sCodispl, 3925, , , ". (Fecha Desde)")
                Else
                    lclsRoles = New ePolicy.Roles
                    If dDateStart = eRemoteDB.Constants.dtmNull Then
                        dDateStart = DateTime.Now.ToString("dd-MM-yyyy")
                        Bdate = True
                    Else
                        dDateStart = dDateStart
                    End If
                    If Not lclsRoles.valExistsRoles("2", nBranch, nProduct, nPolicy, 0, eRemoteDB.Constants.intNull, sClient, dDateStart) Then
                        '+ Debe corresponder a un cliente de la póliza
                        Call .ErrorMessage(sCodispl, 4025)
                    End If
                End If
            End If

            '+ Validaciones del campo Moneda
            If nCurrency = eRemoteDB.Constants.intNull Then
                '+ Debe estar lleno
                Call .ErrorMessage(sCodispl, 1351)
            Else
                If Bdate Then
                    dDateStart = eRemoteDB.Constants.dtmNull
                End If
                '+ La póliza debe tener movimientos a facturar
                If Not insReaOut_moveme(nBranch, nProduct, nPolicy, dDateStart, dDateEnd, nCurrency) Then
                    Call .ErrorMessage(sCodispl, 38035)
                End If
            End If

            '+ Validaciones del campo Fecha de movimientos Desde - Hasta
            If dDateStart <> eRemoteDB.Constants.dtmNull Then
                If dDateEnd <> eRemoteDB.Constants.dtmNull Then
                    If dDateEnd < dDateStart Then
                        Call .ErrorMessage(sCodispl, 1132)
                    End If
                End If
            End If

            '+ Validaciones del campo Contabilización
            If dLedgerDate = eRemoteDB.Constants.dtmNull Then
                Call .ErrorMessage(sCodispl, 7056)
            Else
                '+ Debe ser posterior al último proceso de asientos automáticos
                If lclsctrol_date.Find(1) Then
                    If dLedgerDate <= lclsctrol_date.dEffecdate Then
                        Call .ErrorMessage(sCodispl, 1008)
                    End If
                End If
                '+ Debe ser posterior o igual al inicio del período contable en vigor
                If lclsLed_compan.Find_Date_Init(nCompany) Then
                    If lclsLed_compan.dDate_init <> eRemoteDB.Constants.dtmNull Then
                        If dLedgerDate < lclsLed_compan.dDate_init Then
                            Call .ErrorMessage(sCodispl, 1006)
                        End If
                    End If
                End If
            End If

            insValCA036 = .Confirm
        End With

insValCA036_Err:
        If Err.Number Then
            insValCA036 = "insValCA036: " & Err.Description
        End If
        On Error GoTo 0
        'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsErrors = Nothing
        'UPGRADE_NOTE: Object lclsLed_compan may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsLed_compan = Nothing
        'UPGRADE_NOTE: Object lclsctrol_date may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsctrol_date = Nothing
        'UPGRADE_NOTE: Object lclsRoles may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsRoles = Nothing
    End Function

    '**%insValCA036_K: The header information of the page CA036 - Billing of group policies are validated.
    '% insValCA036_K: Permite validar los datos del encabezado de la página CA036 - Facturación de colectivos.
    Public Function insValCA036_K(ByVal sCodispl As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double) As String
        Dim lclsErrors As eFunctions.Errors
        Dim lclsValues As eFunctions.Values
        Dim lclsPolicy As ePolicy.Policy
        Dim lclsCertificat As ePolicy.Certificat
        Dim lblnValid As Boolean

        On Error GoTo insValCA036_K_Err

        lclsErrors = New eFunctions.Errors
        lclsValues = New eFunctions.Values
        lclsPolicy = New ePolicy.Policy
        lclsCertificat = New ePolicy.Certificat

        lblnValid = True

        With lclsErrors
            '**+ Validations on the field "Branch".
            '+ Validaciones sobre el campo "Ramo".
            If nBranch = eRemoteDB.Constants.intNull Then
                '+ Debe estar lleno
                Call .ErrorMessage(sCodispl, 1022)
                lblnValid = False
            End If

            '**+ Validations on the field "Code of product".
            '+ Validaciones sobre el campo "Código del producto".

            If nProduct = eRemoteDB.Constants.intNull Then
                '+ Debe estar lleno
                Call lclsErrors.ErrorMessage(sCodispl, 1014)
            Else
                If lblnValid Then
                    '+ Debe ser un producto válido
                    lclsValues.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    If Not lclsValues.IsValid("tabProdmaster1", CStr(nProduct), True) Then
                        Call lclsErrors.ErrorMessage(sCodispl, 9066)
                        lblnValid = False
                    End If
                End If
            End If

            '**+ Validations on the field "Policy".
            '+ Validaciones sobre el campo "Póliza".

            If nPolicy = eRemoteDB.Constants.intNull Then
                '+ Debe estar lleno
                Call lclsErrors.ErrorMessage(sCodispl, 3003)
            Else
                If lblnValid Then
                    If Not lclsPolicy.FindPolicybyPolicy("2", nPolicy, nBranch, nProduct) Then
                        Call lclsErrors.ErrorMessage(sCodispl, 3001)
                    Else
                        If lclsPolicy.sPolitype = "1" Then
                            Call lclsErrors.ErrorMessage(sCodispl, 3109)
                        Else
                            '+ La póliza no puede estar anulada
                            If lclsPolicy.nNullcode <> eRemoteDB.Constants.intNull Then
                                Call lclsErrors.ErrorMessage(sCodispl, 3098)
                            End If

                            If lclsPolicy.sStatus_pol = "3" Then
                                '+ La póliza no puede estar en captura incompleta
                                Call lclsErrors.ErrorMessage(sCodispl, 3720)
                            End If

                            '+ Validaciones de generación de recibo. (1-vencida / 2-anticipada)
                            Call lclsCertificat.Find("2", nBranch, nProduct, nPolicy, 0, True)
                            If lclsPolicy.sReceipt_ind = "1" And Today < lclsCertificat.dNextReceip Then
                                Call lclsErrors.ErrorMessage(sCodispl, 100139)
                            End If
                        End If
                    End If
                End If
            End If
            insValCA036_K = .Confirm
        End With

insValCA036_K_Err:
        If Err.Number Then
            insValCA036_K = "insValCA036_K: " & Err.Description
        End If
        On Error GoTo 0
        'UPGRADE_NOTE: Object lclsValues may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsValues = Nothing
        'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsErrors = Nothing
        'UPGRADE_NOTE: Object lclsPolicy may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsPolicy = Nothing
    End Function

    '% insProcessCAL036: Esta rutina se encarga de realizar el proceso de selección y creación
    '% de los diferentes registros en las tablas involucradas, realiza el proceso de facturación de
    '% colectivos.
    Public Function insProcessCAL036(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal nCurrency As Integer, ByVal sTypeMov As String, ByVal nYear As Integer, ByVal nMonth As Integer, ByVal nSituation As Integer, ByVal nGroup As Integer, ByVal nTratypei As Integer, ByVal sClient As String, ByVal dLedgerDat As Date, ByVal nUsercode As Integer, ByVal dStartdate As Date, ByVal dEndDate As Date) As Boolean
        Dim lrecOut_moveme As eRemoteDB.Execute

        lrecOut_moveme = New eRemoteDB.Execute

        On Error GoTo insProcessCAL036_Err

        With lrecOut_moveme
            .StoredProcedure = "insCal036"
            .Parameters.Add("sTypemov", sTypeMov, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sCertype", "2", eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCurrency", nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nYear", nYear, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nMonth", nMonth, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nSituation", nSituation, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nGroup", nGroup, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nTratypei", nTratypei, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dLedgerdat", dLedgerDat, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCommit", 1, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dStartdate", dStartdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEnddate", dEndDate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            insProcessCAL036 = .Run(False)
        End With

insProcessCAL036_Err:
        If Err.Number Then
            insProcessCAL036 = False
        End If
        On Error GoTo 0
        'UPGRADE_NOTE: Object lrecOut_moveme may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecOut_moveme = Nothing
    End Function

    '**%LoadTabs: Constructs the sequence in HTML code
    '%LoadTabs: Arma la secuencia en código HTML.
    Public Function LoadTabs(ByVal nMainAction As Integer, ByVal sUserSchema As String, ByVal nCurrency As Integer, ByVal nTypemov As Integer, Optional ByVal sSel As String = "", Optional ByVal nCertif As Double = 0, Optional ByVal nYear As Integer = 0, Optional ByVal nMonth As Integer = 0, Optional ByVal nMoveType As Integer = 0, Optional ByVal nSituation As Integer = 0, Optional ByVal nGroup As Integer = 0) As String
        Dim lclsSecurSche As eSecurity.Secur_sche
        Dim mintPageImage As eFunctions.Sequence.etypeImageSequence
        Dim lrecWindows As eRemoteDB.Query
        Dim lclsSequence As eFunctions.Sequence

        Dim lstrHTMLCode As String
        Dim lstrCodispl As String
        Dim lstrCodisp As String = ""
        Dim lstrShort_desc As String = ""
        Dim lblnContent As Boolean
        Dim lblnRequired As Boolean
        Dim lstrSequence As String
        Dim lintCountWindows As Object

        On Error GoTo LoadTabs_err

        lclsSequence = New eFunctions.Sequence
        lrecWindows = New eRemoteDB.Query
        lclsSecurSche = New eSecurity.Secur_sche

        lstrHTMLCode = String.Empty

        lstrHTMLCode = lclsSequence.makeTable

        '+ Si el tipo de movimiento es "Manual" = 2 se carga el frame
        '+ CA036A - Movimientos pendientes por facturar.

        '+ Si el tipo de movimiento es "Según condición" = 3 se carga el
        '+ fram CA039 - Condición de selección de movimientos.

        '+ Si el tipo de movimiemto es diferente a los anteriores se carga
        '+ el frame CA036 - Selección de movimientos.

        If nTypemov = 2 Then
            lstrSequence = "CA036   CA036A  "
        ElseIf nTypemov = 3 Then
            lstrSequence = "CA036   CA039   "
        Else
            lstrSequence = "CA036   "
        End If

        lintCountWindows = 1
        lstrCodispl = Mid(lstrSequence, lintCountWindows, 8)

        Do While Trim(lstrCodispl) <> String.Empty
            lblnRequired = False
            lblnContent = False

            '**+ CA036 - Selection of items.
            '+ CA036 - Selección de movimientos.

            If Trim(lstrCodispl) = "CA036" Then
                If nCurrency = eRemoteDB.Constants.intNull Or nCurrency = 0 Then
                    lblnContent = False
                    lblnRequired = True
                Else
                    lblnContent = True
                    lblnRequired = False
                End If
            End If

            '**+ CA036A - Pending for billing entries.
            '+ CA036A - Movimientos pendientes por facturar.

            If Trim(lstrCodispl) = "CA036A" Then
                If sSel = String.Empty Then
                    lblnContent = False
                    lblnRequired = True
                Else
                    lblnContent = True
                    lblnRequired = False
                End If
            End If

            '**+CA039 - Condition for selection of items.
            '+ CA039 - Condición de selección de movimientos.

            If Trim(lstrCodispl) = "CA039" Then
                If ((nCertif <> eRemoteDB.Constants.intNull And nCertif <> 0) Or (nYear <> eRemoteDB.Constants.intNull And nYear <> 0) Or (nMonth <> eRemoteDB.Constants.intNull And nMonth <> 0) Or (nMoveType <> eRemoteDB.Constants.intNull And nMoveType <> 0) Or (nSituation <> eRemoteDB.Constants.intNull And nSituation <> 0) Or (nGroup <> eRemoteDB.Constants.intNull And nGroup <> 0)) Then
                    lblnContent = True
                    lblnRequired = False
                Else
                    lblnContent = False
                    lblnRequired = True
                End If
            End If

            '**+ The values are assigned to the description variables.
            '+ Se asignan los valores a las variables de descripción.

            If lrecWindows.OpenQuery("Windows", "sCodisp, sShort_des", "scodispl='" & Trim(lstrCodispl) & "'") Then
                lstrCodisp = lrecWindows.FieldToClass("sCodisp")
                lstrShort_desc = lrecWindows.FieldToClass("sShort_des")

                lrecWindows.CloseQuery()
            End If

            '**+Search the image to put in the links.
            '+Se busca la imagen a colocar en los links.

            With lclsSecurSche
                If Not .valTransAccess(sUserSchema, lstrCodisp, "1") Then
                    If lblnContent Then
                        mintPageImage = eFunctions.Sequence.etypeImageSequence.eDeniedOK
                    Else
                        If lblnRequired Then
                            mintPageImage = eFunctions.Sequence.etypeImageSequence.eDeniedReq
                        Else
                            mintPageImage = eFunctions.Sequence.etypeImageSequence.eDeniedS
                        End If
                    End If
                Else
                    If Not lblnContent Then
                        If lblnRequired Then
                            mintPageImage = eFunctions.Sequence.etypeImageSequence.eRequired
                        Else
                            mintPageImage = eFunctions.Sequence.etypeImageSequence.eEmpty
                        End If
                    Else
                        mintPageImage = eFunctions.Sequence.etypeImageSequence.eOK
                    End If
                End If
            End With

            lstrHTMLCode = lstrHTMLCode & lclsSequence.makeRow(lstrCodisp, lstrCodispl, nMainAction, lstrShort_desc, mintPageImage)
            '**+Move to the following record that has been found
            '+Se mueve al siguiente registro encontrado

            lintCountWindows = lintCountWindows + 8
            lstrCodispl = Mid(lstrSequence, lintCountWindows, 8)
        Loop

        lstrHTMLCode = lstrHTMLCode & lclsSequence.closeTable()

        LoadTabs = lstrHTMLCode

        'UPGRADE_NOTE: Object lclsSecurSche may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsSecurSche = Nothing
        'UPGRADE_NOTE: Object lrecWindows may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecWindows = Nothing
        'UPGRADE_NOTE: Object lclsSequence may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsSequence = Nothing

        Exit Function
LoadTabs_err:
        LoadTabs = "LoadTabs: " & Err.Description

        On Error GoTo 0
    End Function

    '**%insReaOut_moveme: Permited verify the exist movements for Billing of the policies.
    '%insReaOut_moveme: Permite verificar si existen movimientos por facturar para la póliza en tratamiento.
    Public Function insReaOut_moveme(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal dDateStart As Date, ByVal dDateEnd As Date, ByVal nCurrency As Integer) As Boolean
        Dim lrecReaOut_moveme_CA036A_a As eRemoteDB.Execute

        On Error GoTo insReaOut_moveme_Err

        lrecReaOut_moveme_CA036A_a = New eRemoteDB.Execute

        insReaOut_moveme = False

        '**+ Definition of parameters for stored procedure 'insudbreaOut_moveme_CA036A_a
        '**+ read Information 26/05/2000 11:36:49.

        '+ Definición de parámetros para stored procedure 'insudb.reaOut_moveme_CA036A_a'
        '+ Información leída el 26/05/2000 11:36:49

        With lrecReaOut_moveme_CA036A_a
            .StoredProcedure = "reaOut_moveme_CA036A_a"

            .Parameters.Add("sCertype", "2", eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dStartdate", dDateStart, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dExpirdat", dDateEnd, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCurrency", nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            If .Run Then
                insReaOut_moveme = True

                Do While Not (.EOF)
                    If .FieldToClass("nTratypei", 0) = 7 Or .FieldToClass("nTratypei", 0) = 1 Then
                        mdblPremium = mdblPremium + .FieldToClass("nPremium", 0)
                    End If

                    .RNext()
                Loop
            End If
        End With

        'UPGRADE_NOTE: Object lrecReaOut_moveme_CA036A_a may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecReaOut_moveme_CA036A_a = Nothing

insReaOut_moveme_Err:
        If Err.Number Then
            insReaOut_moveme = False
        End If

        On Error GoTo 0

    End Function

    '% updOut_movemeCA036A: Este método se encarga de actualizar los registros de la tabla Out_moveme
    '%                      con el estado 4 - Seleccionado para facturar.
    Private Function updOut_movemeCA036A(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal sCertif As String, ByVal sMovnumbe As String, ByVal sDigit As String, ByVal sSelected As String, ByVal nUsercode As Integer) As Boolean
        Dim lrecUpdOut_moveme As eRemoteDB.Execute

        On Error GoTo updOut_movemeCA036A_Err

        lrecUpdOut_moveme = New eRemoteDB.Execute

        With lrecUpdOut_moveme
            .StoredProcedure = "updOut_movemeCA036A"
            .Parameters.Add("sCertype", "2", eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sCertif", sCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 2000, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sMovnumbe", sMovnumbe, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 2000, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sDigit", sDigit, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 2000, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sSel", sSelected, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 2000, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            updOut_movemeCA036A = .Run(False)
        End With

updOut_movemeCA036A_Err:
        If Err.Number Then
            updOut_movemeCA036A = False
        End If
        On Error GoTo 0
        'UPGRADE_NOTE: Object lrecUpdOut_moveme may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecUpdOut_moveme = Nothing
    End Function

    '**%insValCA039: The information of frame CA039 - Condition of selection of movements.
    '% insValCA039: Permite validar los datos del frame CA039 - Condición de selección de movimientos
    Public Function insValCA039(ByVal sCodispl As String, ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal nYear As Integer, ByVal nMonth As Integer, ByVal nMovType As Integer, ByVal nSituation As Integer, ByVal nGroup As Integer) As String
        Dim lclsErrors As eFunctions.Errors
        Dim lclsCertificat As ePolicy.Certificat

        On Error GoTo insValCA039_Err

        lclsErrors = New eFunctions.Errors

        '**+ Been worth that at least one of the fields has value.
        '+ Se valida que por lo menos uno de los campos tenga valor.
        If nCertif = eRemoteDB.Constants.intNull And nYear = eRemoteDB.Constants.intNull And nMonth = eRemoteDB.Constants.intNull And nMovType = eRemoteDB.Constants.intNull And nSituation = eRemoteDB.Constants.intNull And nGroup = eRemoteDB.Constants.intNull Then
            Call lclsErrors.ErrorMessage(sCodispl, 3143)
        Else

            '**+ Been worth the introduced rank of date.
            '+Se valida año-mes ingresado.
            '+Se ingresan ambos o ninguno
            If ((nYear <> eRemoteDB.Constants.intNull) Xor (nMonth <> eRemoteDB.Constants.intNull)) Then
                Call lclsErrors.ErrorMessage(sCodispl, 3829)
            Else
                '+Aqui ya se sabe que si ingreso información en año
                '+tambien lo hizo en mes
                If (nYear <> eRemoteDB.Constants.intNull) Then
                    '+Año lógico
                    If (nYear < 1900 Or nYear > 4000) Then
                        Call lclsErrors.ErrorMessage(sCodispl, 1023)
                    Else
                        '+Mes lógico
                        If (nMonth < 1 Or nMonth > 12) Then
                            Call lclsErrors.ErrorMessage(sCodispl, 1023)
                        End If
                    End If
                End If
            End If
        End If

        '**+ Validations of the field "Certificado".
        '+ Validaciones del campo "Certificado".
        If nCertif <> eRemoteDB.Constants.intNull Then
            lclsCertificat = New ePolicy.Certificat
            If Not lclsCertificat.Find(sCertype, nBranch, nProduct, nPolicy, nCertif, True) Then
                Call lclsErrors.ErrorMessage(sCodispl, 3010)
            Else
                If lclsCertificat.sStatusva = "2" Or lclsCertificat.sStatusva = "3" Then
                    Call lclsErrors.ErrorMessage(sCodispl, 750044)
                End If
            End If
            'UPGRADE_NOTE: Object lclsCertificat may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
            lclsCertificat = Nothing
        End If

        insValCA039 = lclsErrors.Confirm

        'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsErrors = Nothing
        'UPGRADE_NOTE: Object lclsCertificat may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsCertificat = Nothing

insValCA039_Err:
        If Err.Number Then
            insValCA039 = insValCA039 & Err.Description
        End If

        On Error GoTo 0
    End Function

    '% inspostCA036A: se actualizan los datos seleccionados en la página
    Public Function inspostCA036A(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal sSelected As String, ByVal sCertif As String, ByVal sMovnumbe As String, ByVal sDigit As String, ByVal nUsercode As Integer) As Boolean
        On Error GoTo inspostCA036A_err

        inspostCA036A = updOut_movemeCA036A(nBranch, nProduct, nPolicy, sCertif, sMovnumbe, sDigit, sSelected, nUsercode)

inspostCA036A_err:
        If Err.Number Then
            inspostCA036A = False
        End If
        On Error GoTo 0
    End Function

    '% Find_CA050: Lee el numero del movimiento que se debe actualizar en policy_his
    Public Function Find_CA050(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double) As Boolean
        Dim lreaout_moveme_ca050 As eRemoteDB.Execute
        Dim nMovnumbe As Object = New Object
        On Error GoTo Find_CA050_Err

        lreaout_moveme_ca050 = New eRemoteDB.Execute

        With lreaout_moveme_ca050
            .StoredProcedure = "INSREAOUT_MOVEME_CA050"
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nMovnumbe", nMovnumbe, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If .Run(False) Then
                Me.nMovnumbe = .Parameters("nMovnumbe").Value
                Find_CA050 = True
            End If
        End With

Find_CA050_Err:
        If Err.Number Then
            Find_CA050 = False
        End If
        'UPGRADE_NOTE: Object lreaout_moveme_ca050 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lreaout_moveme_ca050 = Nothing
        On Error GoTo 0
    End Function

    '% Find_CA050: Lee el numero del movimiento que se debe actualizar en policy_his
    Public Function Find_Receipt(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal nMovnumbe As Double) As Boolean
        Dim lFind_Receipt As eRemoteDB.Execute
        Dim nReceipt As Object = New Object
        On Error GoTo Find_CA050_Err

        lFind_Receipt = New eRemoteDB.Execute

        With lFind_Receipt
            .StoredProcedure = "INSREAOUT_MOVEME_RECEIPT"
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nMovnumbe", nMovnumbe, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nReceipt", nReceipt, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If .Run(False) Then
                Me.nReceipt = .Parameters("nReceipt").Value
                Find_Receipt = True
            End If
        End With

Find_CA050_Err:
        If Err.Number Then
            Find_Receipt = False
        End If
        'UPGRADE_NOTE: Object lreaout_moveme_ca050 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lFind_Receipt = Nothing
        On Error GoTo 0
    End Function
End Class






