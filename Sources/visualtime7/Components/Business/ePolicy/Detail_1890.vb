Public Class Detail_1890
    Implements System.Collections.IEnumerable
    '%-------------------------------------------------------%'
    '% $Workfile:: Detail_1890.cls                           $%'
    '% $Author:: jalvear                                     $%'
    '% $Date:: 25/02/19 6:13p                                $%'
    '% $Revision:: 1                                         $%'
    '%-------------------------------------------------------%'

    '+ Estructura de tabla INSUDB.TMP_GIL1890 
    '+-----------------------------------------------------------
    Public nBranch As Integer
    Public nPolicy As Integer
    Public dDate_amount As Date
    Public dDate_surr As Date
    Public sInvestor As String
    Public sFirstname As String
    Public sClient As String
    Public sClient_1 As String 'SCLIENT+"-"+SDIGIT
    Public sDigit As String
    Public sPaper As String
    Public nAmount_orig As Double
    Public nVal_fact_uf As Double
    Public nAmount_local As Double
    Public nAmount_bal As Double
    Public nSurramo_orig As Double
    Public nSurramo_local As Double
    Public nAmount_adjust As Double
    Public nInt_surr_pos As Double
    Public nInt_surr_neg As Double
    Public nInt_foreing As Double
    Public nTax_foreing As Double
    Public nExchange1 As Double
    Public sType As String
    Public nYear As Integer
    Public nRectif As Integer
    Public nId As Integer
    Public sLastname As String
    Public sLastname2 As String
    Public nProduct As Integer
    Public nCertif As Integer
    Public nExchange2 As Double
    Public nCurrency As Double
    Public nReceipt As Integer
    Public nOrigin As Integer
    Public dCompdate As Date
    Public nUsercode As Integer
    Public sError As String
    Public nRemnumber As Integer
    Public sCompany As String
    Public sCompanyname As String
    Public sLegalname As String
    Public sDigit_comp As String
    Public sDescadd As String
    Public nMunicipality As Integer
    Public nCompany As Integer
    Public sMassive As String
    Public sYearmonth As String
    Public sYearmonth_r As String
    Public Cliname As String
    Public dStartdate As Date
    Public dExpirdat As Date
    Public int_rent_pag As Double
    Public int_rent_pag_pos_neg As Double

    '- Variable de la colection
    Public mCol As New Collection
    Public List_Detail1890 As New List(Of Detail_1890)

    Public Function FindCertSeven_massive(ByVal sKey As String, ByVal sMassive As String, ByVal nYear As String, ByVal dCompdate As String, ByVal nRectif As String, ByVal sClient As String) As Boolean

        Dim lrecreaCertseven As New eRemoteDB.Execute
        Dim lclsDetail1890 As Detail_1890

        '+ definicion del store procedure
        With lrecreaCertseven
            .StoredProcedure = "REAVIL1890"
            '.Parameters.Add("sKey", sKey, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sMassive_1", sMassive, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nYear_1", nYear, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 4, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nRectif_1", nRectif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sClient_1", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dCompdate_1", dCompdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar,, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If .Run Then
                FindCertSeven_massive = True
                Do While Not .EOF
                    lclsDetail1890 = New Detail_1890
                    lclsDetail1890.sCompany = IIf(IsDBNull(.FieldToClass("sCompany")) Or .FieldToClass("sCompany") = "-32768", "", .FieldToClass("sCompany"))
                    lclsDetail1890.sCompanyname = IIf(IsDBNull(.FieldToClass("sCompanyname")) Or .FieldToClass("sCompanyname") = "-32768", "", .FieldToClass("sCompanyname"))
                    lclsDetail1890.sLegalname = IIf(IsDBNull(.FieldToClass("sLegalname")) Or .FieldToClass("sLegalname") = "-32768", "", .FieldToClass("sLegalname"))
                    lclsDetail1890.sDigit_comp = IIf(IsDBNull(.FieldToClass("sDigit_comp")) Or .FieldToClass("sDigit_comp") = "-32768", "", .FieldToClass("sDigit_comp"))
                    lclsDetail1890.sDescadd = IIf(IsDBNull(.FieldToClass("sDescadd")) Or .FieldToClass("sDescadd") = "-32768", "", .FieldToClass("sDescadd"))
                    lclsDetail1890.nPolicy = IIf(IsDBNull(.FieldToClass("nPolicy")) Or .FieldToClass("nPolicy") = -32768, 0, .FieldToClass("nPolicy"))
                    lclsDetail1890.sClient_1 = IIf(IsDBNull(.FieldToClass("sClient_1")) Or .FieldToClass("sClient_1") = "-32768", "", .FieldToClass("sClient_1"))
                    lclsDetail1890.sClient = IIf(IsDBNull(.FieldToClass("sClient")) Or .FieldToClass("sClient") = "-32768", "", .FieldToClass("sClient"))
                    lclsDetail1890.Cliname = IIf(IsDBNull(.FieldToClass("Cliname")) Or .FieldToClass("Cliname") = "-32768", "", .FieldToClass("Cliname"))
                    lclsDetail1890.dStartdate = IIf(IsDBNull(.FieldToClass("dStartdate")), "01/01/1900", .FieldToClass("dStartdate"))
                    lclsDetail1890.dExpirdat = IIf(IsDBNull(.FieldToClass("dExpirdat")), "01/01/1900", .FieldToClass("dExpirdat"))
                    lclsDetail1890.sYearmonth = IIf(IsDBNull(.FieldToClass("sYearmonth")) Or .FieldToClass("sYearmonth") = "-32768", "", .FieldToClass("sYearmonth"))
                    lclsDetail1890.sYearmonth_r = IIf(IsDBNull(.FieldToClass("sYearmonth_r")) Or .FieldToClass("sYearmonth_r") = "-32768", "", .FieldToClass("sYearmonth_r"))
                    lclsDetail1890.nVal_fact_uf = IIf(IsDBNull(.FieldToClass("nVal_fact_uf")) Or .FieldToClass("nVal_fact_uf") = -32768, 0, .FieldToClass("nVal_fact_uf"))
                    lclsDetail1890.nAmount_local = IIf(IsDBNull(.FieldToClass("nAmount_local")) Or .FieldToClass("nAmount_local") = -32768, 0, .FieldToClass("nAmount_local"))
                    lclsDetail1890.nSurramo_local = IIf(IsDBNull(.FieldToClass("nSurramo_local")) Or .FieldToClass("nSurramo_local") = -32768, 0, .FieldToClass("nSurramo_local"))
                    lclsDetail1890.nInt_surr_pos = IIf(IsDBNull(.FieldToClass("nInt_surr_pos")) Or .FieldToClass("nInt_surr_pos") = -32768, 0, .FieldToClass("nInt_surr_pos"))
                    lclsDetail1890.nInt_surr_neg = IIf(IsDBNull(.FieldToClass("nInt_surr_neg")) Or .FieldToClass("nInt_surr_neg") = -32768, 0, .FieldToClass("nInt_surr_neg"))
                    lclsDetail1890.int_rent_pag = IIf(IsDBNull(.FieldToClass("int_rent_pag")) Or .FieldToClass("int_rent_pag") = -32768, 0, .FieldToClass("int_rent_pag"))
                    lclsDetail1890.int_rent_pag_pos_neg = IIf(IsDBNull(.FieldToClass("int_rent_pag_pos_neg")) Or .FieldToClass("int_rent_pag_pos_neg") = -32768, 0, .FieldToClass("int_rent_pag_pos_neg"))
                    lclsDetail1890.nId = IIf(IsDBNull(.FieldToClass("nId")) Or .FieldToClass("nId") = -32768, -1, .FieldToClass("nId"))
                    List_Detail1890.Add(lclsDetail1890)
                    .RNext()
                Loop
                .RCloseRec()
            Else
                .RCloseRec()
            End If
        End With

Find_Err:
        If Err.Number Then
            FindCertSeven_massive = False
        End If
        On Error GoTo 0
    End Function
    Public Function Add(ByRef objClass As Detail_1890) As Detail_1890
        If objClass Is Nothing Then
            objClass = New Detail_1890
        End If

        With objClass
            mCol.Add(objClass)
        End With
        '+ Retorna objeto creado
        Add = objClass
    End Function
    '% Class_Initialize: se controla el acceso a la clase
    'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
    Private Sub Class_Initialize_Renamed()
        Call ClearFields()
    End Sub
    Public Sub New()
        MyBase.New()
        Class_Initialize_Renamed()
    End Sub

    '% ClearFields: se inicializa el valor de las variables de la clase
    Private Sub ClearFields()
        nBranch = eRemoteDB.Constants.intNull
        sYearmonth = String.Empty
        sYearmonth_r = String.Empty
        nPolicy = eRemoteDB.Constants.intNull
        dDate_amount = eRemoteDB.Constants.dtmNull
        dDate_surr = eRemoteDB.Constants.dtmNull
        sInvestor = String.Empty
        sFirstname = String.Empty
        sClient = String.Empty
        sClient_1 = String.Empty
        sDigit = String.Empty
        sPaper = String.Empty
        nAmount_orig = eRemoteDB.Constants.dblNull
        nVal_fact_uf = eRemoteDB.Constants.dblNull
        nAmount_local = eRemoteDB.Constants.dblNull
        nAmount_bal = eRemoteDB.Constants.dblNull
        nSurramo_orig = eRemoteDB.Constants.dblNull
        nSurramo_local = eRemoteDB.Constants.dblNull
        nAmount_adjust = eRemoteDB.Constants.dblNull
        nInt_surr_pos = eRemoteDB.Constants.dblNull
        nInt_surr_neg = eRemoteDB.Constants.dblNull
        nInt_foreing = eRemoteDB.Constants.dblNull
        nTax_foreing = eRemoteDB.Constants.dblNull
        nExchange1 = eRemoteDB.Constants.dblNull
        sType = String.Empty
        nYear = eRemoteDB.Constants.intNull
        nRectif = eRemoteDB.Constants.intNull
        nId = eRemoteDB.Constants.intNull
        sLastname = String.Empty
        sLastname2 = String.Empty
        nProduct = eRemoteDB.Constants.intNull
        nCertif = eRemoteDB.Constants.intNull
        nExchange2 = eRemoteDB.Constants.dblNull
        nCurrency = eRemoteDB.Constants.intNull
        nReceipt = eRemoteDB.Constants.intNull
        nOrigin = eRemoteDB.Constants.intNull
        dCompdate = eRemoteDB.Constants.dtmNull
        nUsercode = eRemoteDB.Constants.intNull
        sError = String.Empty
        nRemnumber = eRemoteDB.Constants.intNull
        sDescadd = String.Empty
        sCompany = String.Empty
        sCompanyname = String.Empty
        sLegalname = String.Empty
        sDigit_comp = String.Empty
        nMunicipality = eRemoteDB.Constants.intNull
        nCompany = eRemoteDB.Constants.intNull
        sMassive = String.Empty
        Cliname = String.Empty
        dStartdate = eRemoteDB.Constants.dtmNull
        dExpirdat = eRemoteDB.Constants.dtmNull
        int_rent_pag = eRemoteDB.Constants.dblNull
        int_rent_pag_pos_neg = eRemoteDB.Constants.dblNull

    End Sub

    Public Function GetEnumerator() As IEnumerator Implements IEnumerable.GetEnumerator
        Throw New NotImplementedException()
    End Function
End Class
