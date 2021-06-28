Option Strict Off
Option Explicit On
Public Class Credit
	
	'**-Properties according the table in the system on 12/03/1999
	'**-The key fields are nProduct, nBranch, sCertype, nPolicy, nCertif, dEffecdate
	'-Propiedades según la tabla en el sistema 03/12/1999
	'-los campos llaves corresponden a nProduct, nBranch, sCertype, nPolicy, nCertif, dEffecdate
	
	'   Column_name                    Type                    Computed Length Prec Scale Nullable TrimTrailingBlanks FixedLenNullInSource
	'   -----------------------------  ----------------------- -------- ------ ---- ----- -------- ------------------ --------------------
	Public sCertype As String 'char        no       1                 no       yes                no
	Public nProduct As Integer 'smallint    no       2      5    0     no       (n/a)              (n/a)
	Public nBranch As Integer 'smallint    no       2      5    0     no       (n/a)              (n/a)
	Public nPolicy As Double 'int         no       4      10   0     no       (n/a)              (n/a)
	Public nCertif As Double 'int         no       4      10   0     no       (n/a)              (n/a)
	Public dEffecdate As Date 'datetime    no       8                 no       (n/a)              (n/a)
	Public NCAPITAL As Double 'decimal     no       9      12   0     yes      (n/a)              (n/a)
	Public SCLIENT As String 'char       no       14                yes      yes                yes
	Public dContracdat As Date 'datetime    no       8                 yes      (n/a)              (n/a)
	Public sContracnum As String 'char        no       10                yes      yes                yes
	Public nDwuell_num As Integer 'smallint    no       2      5    0     yes      (n/a)              (n/a)
	Public nExe_num As Integer 'smallint    no       2      5    0     yes      (n/a)              (n/a)
	Public DEXPIRDAT As Date 'datetime    no       8                 yes      (n/a)              (n/a)
	Public nGuar_kind As Integer 'smallint    no       2      5    0     yes      (n/a)              (n/a)
	Public sGuar_type As String 'char        no       1                 yes      yes                yes
	Public DISSUEDAT As Date 'datetime    no       8                 yes      (n/a)              (n/a)
	Public nNullcode As Integer 'smallint    no       2      5    0     yes      (n/a)              (n/a)
	Public dNulldate As Date 'datetime    no       8                 yes      (n/a)              (n/a)
	Public nPay_amount As Double 'decimal     no       9      10   2     yes      (n/a)              (n/a)
	Public nPay_quanti As Integer 'smallint    no       2      5    0     yes      (n/a)              (n/a)
	Public NPREMIUM As Double 'decimal     no       9      10   2     yes      (n/a)              (n/a)
	Public dStartdate As Date 'datetime    no       8                 yes      (n/a)              (n/a)
	Public dTerm_date As Date 'datetime    no       8                 yes      (n/a)              (n/a)
	Public nTime_eject As Integer 'smallint    no       2      5    0     yes      (n/a)              (n/a)
	Public nTime_unit As Integer 'smallint    no       2      5    0     yes      (n/a)              (n/a)
	Public NTRANSACTIO As Integer 'int         no       4      10   0     yes      (n/a)              (n/a)
	Public nMinPremium As Double
	Public nPercentPremium As Double
	Public nLimitRequest As Double
	Public nLimitCurrent As Double
	Public NMATERIA As Integer
	Public nUsercode As Integer 'smallint    no       2      5    0     yes      (n/a)              (n/a)
	Public nMaxCapital As Double
	Public nClassClient As Integer
	Public nAjustType As Integer
	Public nRate As Double
	Public nLimitNoPayroll As Double
	Public nCapInsured As Double
	Public nAge As Integer
	Public nLimitRequest_max As Double
	Public nLimitCurrent_max As Double
	Public ninsmodality As Integer 
	Public nguar_type As Integer 
    Public ncredcau As Double
	Public nindemper As Integer 
	Public nmoraallow As Integer 
	Public ntransmon1 As Integer 
	Public ntransmon2 As Integer 
	Public nindper1 As Integer 
    Public nindper2 As Integer    
    Public sFollowUp As Char
    Public sContractObject As String
    Public nBondstatus As Integer
    Public sInsurSector As String


	'**%Objective: Validation of the data for the page details.
	'**%Parameters:
	'**%     scertype         -  type of registry
	'**%     nbranch          -  branch
	'**%     nproduct         -  product
	'**%     npolicy          -  i number of poliza
	'**%     ncertif          -  i number of certificate
	'**%     deffecdate       -  date of effect of the registry
	'%Objetivo: Validación de los datos para la página detalle.
	'%Parámetros:
	'%     scertype        -   tipo de registro
	'%     nbranch         -   ramo
	'%     nproduct        -   producto
	'%     npolicy         -   numero de poliza
	'%     ncertif         -   numero de certificado
	'%     deffecdate      -   fecha de efecto del registro
    Private Function ValTransCC001(ByVal sCertype As String, ByVal nBranch As Short, ByVal nProduct As Short, ByVal nPolicy As Integer, ByVal nCertif As Integer, ByVal dEffecdate As Date, ByVal nAmount As Double) As String

        Dim lclsValtrans As eRemoteDB.Execute

        lclsValtrans = New eRemoteDB.Execute

        '+ Define all parameters for the stored procedures 'insudb.valCreditExist'. Generated on 21/07/2004 03:31:12 p.m.
        With lclsValtrans
            .StoredProcedure = "valtransCC001"
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nAmount", nAmount, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("serrorlist", " ", eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 100, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            If .Run(False) Then
                ValTransCC001 = .Parameters("serrorlist").Value
            Else
                ValTransCC001 = String.Empty
            End If
        End With

        lclsValtrans = Nothing

        Exit Function
    End Function


	'**%Objective: Validation of the data for the page details.
	'**%Parameters:
	'**%     sCodispl         -  Logical code that identifies the transaction.
	'**%     nMainAction      -  Action being executed on the transaction.
	'**%     sAction          -  Action begin executed on the grid of the transaction
	'**%     scertype         -  type of registry
	'**%     nbranch          -  branch
	'**%     nproduct         -  product
	'**%     npolicy          -  i number of poliza
	'**%     ncertif          -  i number of certificate
	'**%     deffecdate       -  date of effect of the registry
	'**%     ninsmodality     -  Insurance modality.
	'**%     scontracnum      -  Number of the Bond or House Purchase Treaty.
	'**%     dcontracdat      -  Date of the treaty
	'**%     ntime_unit       -  Unit in which the Execution Time is expessed.
	'**%     dterm_date       -  Date when the Construction Works are completed or when the House is delivered.
	'**%     ntime_eject      -  Execution Time.
	'**%     ncredcau         -  Credit amount
	'**%     nindemper        -  Percentage of indemnity of claim
	'**%     nmoraallow       -  Number of months permitted for delayed payment
	'**%     ntransmon1       -  Number of months passed for the payment of 1st part of claim
	'**%     ntransmon2       -  Number of months passed for the payment of 2do part of claim
	'**%     nindper1         -  Percentage of indemnification, once passed the first period
	'**%     nindper2         -  Percentage of indemnification, once passed the second period
	'%Objetivo: Validación de los datos para la página detalle.
	'%Parámetros:
	'%     sCodispl        -   Código lógico que identifica la transacción.
	'%     nMainAction     -   Acción que se ejecuta sobre la transacción.
	'%     sAction         -   Acción que se ejecuta sobre el grid de la transacción.
	'%     scertype        -   tipo de registro
	'%     nbranch         -   ramo
	'%     nproduct        -   producto
	'%     npolicy         -   numero de poliza
	'%     ncertif         -   numero de certificado
	'%     deffecdate      -   fecha de efecto del registro
	'%     ninsmodality    -   Modalidad del seguro.
	'%     scontracnum     -   Numero del contrato de fianza o de compra de vivienda.
	'%     dcontracdat     -   Fecha del contrato.
	'%     ntime_unit      -   Unidad del tiempo de ejecución.
	'%     dterm_date      -   Fecha de terminacion de la obra o de entrega de la vivienda.
	'%     ntime_eject     -   Tiempo de ejecución.
	'%     ncredcau        -   Importe del crédito o caución
	'%     nindemper       -   Porcentaje de indemnización de siniestros
	'%     nmoraallow      -   Cantidad de meses de mora permitido
	'%     ntransmon1      -   Cantidad de meses transcurridos para pago de 1ra parte de siniestros
	'%     ntransmon2      -   Cantidad de meses transcurridos para pago de 2da parte de siniestros
	'%     nindper1        -   Porcentaje de indemnización, una vez transcurrido el primer período
	'%     nindper2        -   Porcentaje de indemnización, una vez transcurrido el segundo período
    Public Function InsValCC001(ByVal sCodispl As String, ByVal nMainAction As Integer, ByVal sAction As String, ByVal sCertype As String, ByVal nBranch As Short, ByVal nProduct As Short, ByVal nPolicy As Integer, ByVal nCertif As Integer, ByVal dEffecdate As Date, ByVal ninsmodality As Integer, ByVal nguar_type As Integer, ByVal scontracnum As String, ByVal dcontracdat As Date, ByVal ntime_unit As Integer, ByVal dterm_date As Date, ByVal ntime_eject As Integer, ByVal ncredcau As Double, ByVal nCurrency As Integer, ByVal nStatusBond As Integer, ByVal nindemper As Double, ByVal nmoraallow As Integer, ByVal ntransmon1 As Short, ByVal ntransmon2 As Short, ByVal nindper1 As Double, ByVal nindper2 As Double) As String
        Dim lclsErrors As eFunctions.Errors = Nothing
        Dim sErrosList As String = String.Empty
        Dim sTime_unit As String = String.Empty

        lclsErrors = New eFunctions.Errors

        sErrosList = ValTransCC001(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate, ncredcau)

        If Len(sErrosList) > 0 Then
            Call lclsErrors.ErrorMessage(sCodispl, , , , , , sErrosList)
        End If

        If scontracnum = "" Then
            Call lclsErrors.ErrorMessage(sCodispl, 3357)
        End If

        If ntime_unit = 0 Or ntime_unit = eRemoteDB.Constants.intNull Then

            Call lclsErrors.ErrorMessage(sCodispl, 3360)
        End If

        If ntime_eject = 0 Or ntime_eject = eRemoteDB.Constants.intNull Then

            Call lclsErrors.ErrorMessage(sCodispl, 3359)
        End If

        If dcontracdat = eRemoteDB.Constants.dtmNull Then
            Call lclsErrors.ErrorMessage(sCodispl, 3358)
        End If

        If dterm_date = eRemoteDB.Constants.dtmNull Then
            Call lclsErrors.ErrorMessage(sCodispl, 3370)
        Else
            If dterm_date < dcontracdat Then
                Call lclsErrors.ErrorMessage(sCodispl, 3371)
            End If
        End If

        If ncredcau = eRemoteDB.Constants.intNull Then
            Call lclsErrors.ErrorMessage(sCodispl, 95001)
        End If

        If nindemper = eRemoteDB.Constants.intNull Then
            Call lclsErrors.ErrorMessage(sCodispl, 95002)
        Else
            If nindemper > 100 Then
                Call lclsErrors.ErrorMessage(sCodispl, 95003)
            End If
        End If

        If nmoraallow = eRemoteDB.Constants.intNull Then
            Call lclsErrors.ErrorMessage(sCodispl, 95004)
        Else
            If ntime_unit <> eRemoteDB.Constants.intNull Then
                Select Case ntime_unit
                    Case 1
                        sTime_unit = "d"
                    Case 2
                        sTime_unit = "m"
                    Case 3
                        sTime_unit = "yyyy"
                End Select
                If nmoraallow > DateDiff(sTime_unit, dcontracdat, dterm_date) Then
                    Call lclsErrors.ErrorMessage(sCodispl, 95005)
                End If
            End If
        End If

        If ntransmon1 = eRemoteDB.Constants.intNull Or ntransmon2 = eRemoteDB.Constants.intNull Then
            Call lclsErrors.ErrorMessage(sCodispl, 95006)
        Else
            If nindper1 < 100 And ntransmon2 < ntransmon1 Then
                Call lclsErrors.ErrorMessage(sCodispl, 95007)
            End If
        End If

        If nindper1 = eRemoteDB.Constants.intNull Or nindper2 = eRemoteDB.Constants.intNull Then
            Call lclsErrors.ErrorMessage(sCodispl, 95008)
        Else
            If (nindper1 + nindper2) <> 100 Then
                Call lclsErrors.ErrorMessage(sCodispl, 95009)
            End If
        End If

        InsValCC001 = lclsErrors.Confirm

        lclsErrors = Nothing

        Exit Function
    End Function

	
	'**%Objective: Updates a registry to the table "Credit" using the key for this table.
	'**%Parameters:
	'**%     nusercode        -  código del usuario
	'**%     scertype         -  type of registry
	'**%     nbranch          -  branch
	'**%     nproduct         -  product
	'**%     npolicy          -  i number of poliza
	'**%     ncertif          -  i number of certificate
	'**%     deffecdate       -  date of effect of the registry
	'**%     ninsmodality     -  Insurance modality.
	'**%     scontracnum      -  Number of the Bond or House Purchase Treaty.
	'**%     dcontracdat      -  Date of the treaty
	'**%     ntime_unit       -  Unit in which the Execution Time is expessed.
	'**%     dterm_date       -  Date when the Construction Works are completed or when the House is delivered.
	'**%     ntime_eject      -  Execution Time.
	'**%     ncredcau         -  Credit amount
	'**%     nindemper        -  Percentage of indemnity of claim
	'**%     nmoraallow       -  Number of months permitted for delayed payment
	'**%     ntransmon1       -  Number of months passed for the payment of 1st part of claim
	'**%     ntransmon2       -  Number of months passed for the payment of 2do part of claim
	'**%     nindper1         -  Percentage of indemnification, once passed the first period
	'**%     nindper2         -  Percentage of indemnification, once passed the second period
	'%Objetivo: Actualiza un registro a la tabla "Credit" usando la clave para dicha tabla.
	'%Parámetros:
	'%     nusercode       -   código del usuario
	'%     scertype        -   tipo de registro
	'%     nbranch         -   ramo
	'%     nproduct        -   producto
	'%     npolicy         -   numero de poliza
	'%     ncertif         -   numero de certificado
	'%     deffecdate      -   fecha de efecto del registro
	'%     ninsmodality    -   Modalidad del seguro.
	'%     scontracnum     -   Numero del contrato de fianza o de compra de vivienda.
	'%     dcontracdat     -   Fecha del contrato.
	'%     ntime_unit      -   Unidad del tiempo de ejecución.
	'%     dterm_date      -   Fecha de terminacion de la obra o de entrega de la vivienda.
	'%     ntime_eject     -   Tiempo de ejecución.
	'%     ncredcau        -   Importe del crédito o caución
	'%     nindemper       -   Porcentaje de indemnización de siniestros
	'%     nmoraallow      -   Cantidad de meses de mora permitido
	'%     ntransmon1      -   Cantidad de meses transcurridos para pago de 1ra parte de siniestros
	'%     ntransmon2      -   Cantidad de meses transcurridos para pago de 2da parte de siniestros
	'%     nindper1        -   Porcentaje de indemnización, una vez transcurrido el primer período
	'%     nindper2        -   Porcentaje de indemnización, una vez transcurrido el segundo período
    Private Function Update(ByVal nUsercode As Integer, ByVal sCertype As String, ByVal nBranch As Short, ByVal nProduct As Short, ByVal nPolicy As Integer, ByVal nCertif As Integer, ByVal dEffecdate As Date, ByVal ninsmodality As Integer, ByVal nguar_type As Integer, ByVal scontracnum As String, ByVal dcontracdat As Date, ByVal ntime_unit As Integer, ByVal dterm_date As Date, ByVal ntime_eject As Integer, ByVal ncredcau As Double, ByVal nindemper As Double, ByVal nmoraallow As Integer, ByVal ntransmon1 As Short, ByVal ntransmon2 As Short, ByVal nindper1 As Double, ByVal nindper2 As Double, ByVal sFollowUp As Char, ByVal sContractObject As String, ByVal nBondstatus As Integer, ByVal sInsurSector As String) As Boolean
        Dim lclsCredit As eRemoteDB.Execute

        lclsCredit = New eRemoteDB.Execute

        '+ Define all parameters for the stored procedures 'insudb.updCredit'. Generated on 21/07/2004 03:31:12 p.m.
        With lclsCredit
            .StoredProcedure = "insupdCredit"
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("ninsmodality", ninsmodality, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nguar_type", IIf(nguar_type = 0, eRemoteDB.Constants.intNull, nguar_type), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("scontracnum", scontracnum, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 50, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dcontracdat", dcontracdat, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("ntime_unit", ntime_unit, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dterm_date", dterm_date, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("ntime_eject", ntime_eject, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("ncredcau", ncredcau, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 0, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nindemper", nindemper, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 0, 2, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nmoraallow", nmoraallow, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("ntransmon1", ntransmon1, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("ntransmon2", ntransmon2, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nindper1", nindper1, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 0, 2, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nindper2", nindper2, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 0, 2, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sfollowup", sFollowUp, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sContractObject", sContractObject, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbChar, 3000, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBondstatus", nBondstatus, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sInsurSector", sInsurSector, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            Update = .Run(False)
        End With

        lclsCredit = Nothing

        Exit Function
    End Function
	
	'**%depending on whether the stored procedure executed correctly.
	'%Update: Este método se encarga de actualizar registros en la tabla "Credit". Devolviendo verdadero o
	'%falso dependiendo de si el Stored procedure se ejecutó correctamente.
	Public Function insPostCT001(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, ByVal nLimitRequest As Double, ByVal nLimitCurrent As Double, ByVal nPercentPremium As Double, ByVal nMinPremium As Double, ByVal NMATERIA As Integer, ByVal nClassClient As Integer, ByVal nAjustType As Integer, ByVal nRate As Double, ByVal nLimitNoPayroll As Double, ByVal nAge As Integer) As Boolean
		Dim lrecupdCredit As eRemoteDB.Execute
		
		lrecupdCredit = New eRemoteDB.Execute
		
		'**+Stored procedure parameters definition 'insudb.updCredit'
		'**+Data of 12/07/1999 15:27:04
		'+Definición de parámetros para stored procedure 'insudb.updCredit'
		'+Información leída el 7/12/1999 15:27:04
		
		With lrecupdCredit
			.StoredProcedure = "INSUPDCREDIT_CT001"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nMinPremium", nMinPremium, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPercentPremium", nPercentPremium, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nLimitRequest", nLimitRequest, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nLimitCurrent", nLimitCurrent, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nMateria", NMATERIA, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nClassClient", nClassClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAjustType", nAjustType, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nRate", nRate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 9, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nLimitNoPayroll", nLimitNoPayroll, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAge", nAge, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			insPostCT001 = .Run(False)
		End With
		'UPGRADE_NOTE: Object lrecupdCredit may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecupdCredit = Nothing
	End Function
	'%Find_CT001: Se realiza la lectura de las tabla CREDIT para la transaccion de Datos particulares CT001
	Public Function Find_CT001(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, ByVal nAction As Short) As Boolean
		Dim lrecReaCredit As eRemoteDB.Execute
		
		lrecReaCredit = New eRemoteDB.Execute
		
		With lrecReaCredit
			.StoredProcedure = "REACREDIT_CT001"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAction", nAction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				Find_CT001 = True
				nMinPremium = .FieldToClass("nMinPremium")
				nPercentPremium = .FieldToClass("nPercentPremium")
				nLimitRequest = .FieldToClass("nLimitRequest")
				nLimitCurrent = .FieldToClass("nLimitCurrent")
				NMATERIA = .FieldToClass("nMateria")
				nClassClient = .FieldToClass("nClassClient")
				nAjustType = .FieldToClass("nAjustType")
				nCapInsured = .FieldToClass("nCapInsured")
				nRate = .FieldToClass("nRate")
				nLimitNoPayroll = .FieldToClass("nLimitNoPayRoll")
				nAge = .FieldToClass("nAge")
			End If
		End With
		'UPGRADE_NOTE: Object lrecReaCredit may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecReaCredit = Nothing
		
	End Function
	'%insvalCT001: Se realizan las validaciones de la transaccion CT001.
	Public Function insvalCT001(ByVal sCodispl As String, ByVal nLimitRequest As Double, ByVal nLimitCurrent As Double, ByVal nPercentPremium As Double, ByVal nMinPremium As Double, ByVal NMATERIA As Integer, ByVal sPolitype As String, ByVal nCertif As Double, ByVal nClassClient As Short, ByVal nAjustType As Integer, ByVal nRate As Double, ByVal nLimitNoPayroll As Object, ByVal sCertype As String, ByVal nBranch As Short, ByVal nProduct As Short, ByVal nPolicy As Double, ByVal dEffecdate As Date, ByVal nAge As Integer) As String
		Dim lclsErrors As eFunctions.Errors
		Dim lclsCurren_pol As Curren_pol
		
		Dim NCURRENCY As Integer
		
		On Error GoTo insValCT001_err
		
		lclsErrors = New eFunctions.Errors
		
		'+Se valida que el limite solicitado tenga valor
		
		If nLimitRequest <= 0 Then
			Call lclsErrors.ErrorMessage(sCodispl, 9000001)
		End If
		
		'+Se debe indicar el importe otorgado
		
		If nLimitCurrent <= 0 Then
			Call lclsErrors.ErrorMessage(sCodispl, 9000002)
		End If
		
		'+Porcentaje debe estar lleno
		
		If nPercentPremium <= 0 And nMinPremium <= 0 Then
			Call lclsErrors.ErrorMessage(sCodispl, 9000003)
		End If
		
		'+Se valida que el campo Materia se indique solo si la poliza es individual o si se trata de un certificado
		
		If NMATERIA <= 0 And (CDbl(sPolitype) = 1 Or nCertif > 0) Then
			Call lclsErrors.ErrorMessage(sCodispl, 9000004)
		End If
		
		'+Se valida que el campo Clasificacion del deudor se indique solo si la poliza es individual o si se trata de un certificado
		
		If nClassClient <= 0 And (CDbl(sPolitype) = 1 Or nCertif > 0) Then
			Call lclsErrors.ErrorMessage(sCodispl, 9000007)
		End If
		
		'+Se valida que el campo Tipo de ajuste se indique solo si la poliza es individual o si se trata de un certificado
		
		If nAjustType <= 0 And nCertif = 0 Then
			Call lclsErrors.ErrorMessage(sCodispl, 9000006)
		End If
		
		'+Se valida que el campo tasa se indique solo si la poliza es individual o si se trata de un certificado
		
		If nRate <= 0 And (CDbl(sPolitype) = 1 Or nCertif > 0) Then
			Call lclsErrors.ErrorMessage(sCodispl, 2042)
		End If
		
		'+Se valida el campo limite de credito deudores innominados.
		
		If nLimitNoPayroll <= 0 Then
			If (CDbl(sPolitype) = 1 Or nCertif > 0) Then
				
				'+Campo obligatorio solo si la clase del cliente es "innominado"
				If nClassClient = 2 Then
					Call lclsErrors.ErrorMessage(sCodispl, 9000018)
				End If
			Else
				
				'+Si es poliza matriz el campo es obligatorio
				If nProduct = 1 Then
					Call lclsErrors.ErrorMessage(sCodispl, 9000017)
				End If
			End If
		Else
			
			'+Si se trata de un certificado, se valida que el importe indicado no supere el monto indicado en la poliza matriz
			
			If nCertif > 0 Then
				If ValLimitNoPayRoll(sCertype, nBranch, nProduct, nPolicy, dEffecdate, nLimitNoPayroll) Then
					Call lclsErrors.ErrorMessage(sCodispl, 9000019)
				End If
			End If
		End If
		
		'+Se validan limite solicitado y limite otorgado contra totales disponibles de la poliza matriz.
		If nCertif > 0 Then
			
			'+SUMAR DE IMPORTES OTORGADOS POR CLASIFICACION DEL DEUDOR (NOMINADO O INNOMINADO
			If nProduct = 1 Then
				If ReaCredit_Limit(sCertype, nBranch, nProduct, nPolicy, nCertif, nClassClient, dEffecdate) Then
					
					'+VALIDAR CONTRA EL IMPORTE CORRESPONDIENTE SEGUN CLASIFICACION DEL DEUDOR
					
					If nLimitCurrent > nLimitCurrent_max Then
						Call lclsErrors.ErrorMessage(sCodispl, 9000025,  ,  , "Venta anual.")
					End If
					
				End If
			End If
			'+Se valida el importe de credito contra los contratos posibles de reaseguro
			
			If nLimitCurrent > 0 Then
				lclsCurren_pol = New Curren_pol
				If lclsCurren_pol.findCurrency(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate) <> String.Empty Then
					If Not Find_AmountExcess(nBranch, nProduct, nLimitCurrent, lclsCurren_pol.NCURRENCY, dEffecdate) Then
						Call lclsErrors.ErrorMessage(sCodispl, 9000028)
					End If
				End If
			End If
			
		End If
		
		
		'+Se valida el plazo del credito
		If nAge <= 0 Then
			Call lclsErrors.ErrorMessage(sCodispl, 9000029)
		Else
			If nCertif > 0 Then
				If Find_CT001(sCertype, nBranch, nProduct, nPolicy, 0, dEffecdate, 1) Then
					If Me.nAge < nAge Then
						Call lclsErrors.ErrorMessage(sCodispl, 9000031)
					End If
				End If
			End If
		End If
		insvalCT001 = lclsErrors.Confirm
		
insValCT001_err: 
		If Err.Number Then
			insvalCT001 = "insValCT001: " & Err.Description
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
		
	End Function
	
	'%ValLimitNoPayRoll: Esta funcion valida que el limite indicado para deudor innominado no exceda el valor de la matriz
	Public Function ReaCredit_Limit(ByVal sCertype As String, ByVal nBranch As Short, ByVal nProduct As Short, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal nClassClient As Double, ByVal dEffecdate As Date) As Boolean
		Dim lrecReaCredit As eRemoteDB.Execute
		
		lrecReaCredit = New eRemoteDB.Execute
		
		With lrecReaCredit
			.StoredProcedure = "REACREDIT_LIMIT"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nClassClient", nClassClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				ReaCredit_Limit = True
				Me.nLimitRequest_max = .FieldToClass("nLimitRequest")
				Me.nLimitCurrent_max = .FieldToClass("nLimitCurrent")
			End If
		End With
		'UPGRADE_NOTE: Object lrecReaCredit may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecReaCredit = Nothing
		
	End Function
	
	'%ValLimitNoPayRoll: Esta funcion valida que el limite indicado para deudor innominado no exceda el valor de la matriz
	Public Function ValLimitNoPayRoll(ByVal sCertype As String, ByVal nBranch As Short, ByVal nProduct As Short, ByVal nPolicy As Double, ByVal dEffecdate As Date, ByVal nLimitNoPayroll As Double) As Boolean
		Dim lclsCredit As Credit
		
		lclsCredit = New Credit
		
		ValLimitNoPayRoll = False
		
		If lclsCredit.Find_CT001(sCertype, nBranch, nProduct, nPolicy, 0, dEffecdate, 0) Then
			If lclsCredit.nLimitNoPayroll < nLimitNoPayroll Then
				ValLimitNoPayRoll = True
			End If
		End If
		'UPGRADE_NOTE: Object lclsCredit may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsCredit = Nothing
		
	End Function
	
	'% Find_AmountExcess: Busca el monto en exceso del contrato de reaseguro
	Public Function Find_AmountExcess(ByVal nBranch As Double, ByVal nProduct As Double, ByVal nAmount As Integer, ByVal NCURRENCY As Integer, ByVal dEffecdate As Date) As Boolean
		Dim lrecFind_AmountExcess As eRemoteDB.Execute
		Dim ParamExchange As Integer
		Dim nReserve_aux As Double
		Dim lblnFind As Boolean
		
		ParamExchange = VariantType.Null
		
		On Error GoTo Find_AmountExcess_Err
		
		lrecFind_AmountExcess = New eRemoteDB.Execute
		Find_AmountExcess = True
		'+ Definición de store procedure insPostsi007 al 07-18-2003 13:43:59
		
		With lrecFind_AmountExcess
			.StoredProcedure = "Reaexcess_credit"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAmount", nAmount, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCurrency", NCURRENCY, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Find_AmountExcess = .Run
		End With
		
Find_AmountExcess_Err: 
		If Err.Number Then
			Find_AmountExcess = False
		End If
		'UPGRADE_NOTE: Object lrecFind_AmountExcess may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecFind_AmountExcess = Nothing
		On Error GoTo 0
	End Function

	'**%Objective: It prepares the page according to conditions you specify
	'**%Parameters:
	'**%     scertype         -  type of registry
	'**%     nbranch          -  branch
	'**%     nproduct         -  product
	'**%     npolicy          -  i number of poliza
	'**%     ncertif          -  i number of certificate
	'**%     deffecdate       -  date of effect of the registry
	'**%     nusercode        -  User Code
	'**%     nTransaction     - Type of transaction made with the Poliza
	'%Objetivo: Prepara la página según condiciones especificas
	'%Parámetros:
	'%     scertype        -   tipo de registro
	'%     nbranch         -   ramo
	'%     nproduct        -   producto
	'%     npolicy         -   numero de poliza
	'%     ncertif         -   numero de certificado
	'%     deffecdate      -   fecha de efecto del registro
	'%     nusercode       -   código del usuario
	'%     nTransaction    -   Tipo de transacción realizada con la Poliza
	Public Function insPreCC001(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, ByVal nUsercode As Integer, ByVal nTransaction As Integer) As Boolean
		Dim lclsWarranty As ePolicy.Warranty
		
		lclsWarranty = New ePolicy.Warranty
		'+ Si se trata de una 1)emisión, 3)recuperación, 18) Re-Emisión de póliza
		If nTransaction = 1 Or nTransaction = 3 Or nTransaction = 18 Then
			
			insPreCC001 = lclsWarranty.Delete(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate, eRemoteDB.Constants.intNull, nUsercode, "1")
		End If
		
		lclsWarranty = Nothing
		
		Exit Function
	End Function

	'**%Objective: Pass of the information introduced towards the layers of rules of business and access of data.
	'**%Parameters:
	'**%     nHeader          -  Indicator of the zone (Header or detail).
	'**%     sCodispl         -  Logical code that identifies the transaction.
	'**%     nMainAction      -  Action being executed on the transaction.
	'**%     sAction          -  Action begin executed on the grid of the transaction
	'**%     nusercode        -  código del usuario
	'**%     scertype         -  type of registry
	'**%     nbranch          -  branch
	'**%     nproduct         -  product
	'**%     npolicy          -  i number of poliza
	'**%     ncertif          -  i number of certificate
	'**%     deffecdate       -  date of effect of the registry
	'**%     ninsmodality     -  Insurance modality.
	'**%     scontracnum      -  Number of the Bond or House Purchase Treaty.
	'**%     dcontracdat      -  Date of the treaty
	'**%     ntime_unit       -  Unit in which the Execution Time is expessed.
	'**%     dterm_date       -  Date when the Construction Works are completed or when the House is delivered.
	'**%     ntime_eject      -  Execution Time.
	'**%     ncredcau         -  Credit amount
	'**%     nindemper        -  Percentage of indemnity of claim
	'**%     nmoraallow       -  Number of months permitted for delayed payment
	'**%     ntransmon1       -  Number of months passed for the payment of 1st part of claim
	'**%     ntransmon2       -  Number of months passed for the payment of 2do part of claim
	'**%     nindper1         -  Percentage of indemnification, once passed the first period
	'**%     nindper2         -  Percentage of indemnification, once passed the second period
	'%Objetivo: Se encarga de llevar la información introducida hacia las capas de reglas de negocio y acceso de datos.
	'%Parámetros:
	'%     nHeader         -   Indicador de zona de encabezado o detalle
	'%     sCodispl        -   Código lógico que identifica la transacción.
	'%     nMainAction     -   Acción que se ejecuta sobre la transacción.
	'%     sAction         -   Acción que se ejecuta sobre el grid de la transacción.
	'%     nusercode       -   código del usuario
	'%     scertype        -   tipo de registro
	'%     nbranch         -   ramo
	'%     nproduct        -   producto
	'%     npolicy         -   numero de poliza
	'%     ncertif         -   numero de certificado
	'%     deffecdate      -   fecha de efecto del registro
	'%     ninsmodality    -   Modalidad del seguro.
	'%     scontracnum     -   Numero del contrato de fianza o de compra de vivienda.
	'%     dcontracdat     -   Fecha del contrato.
	'%     ntime_unit      -   Unidad del tiempo de ejecución.
	'%     dterm_date      -   Fecha de terminacion de la obra o de entrega de la vivienda.
	'%     ntime_eject     -   Tiempo de ejecución.
	'%     ncredcau        -   Importe del crédito o caución
	'%     nindemper       -   Porcentaje de indemnización de siniestros
	'%     nmoraallow      -   Cantidad de meses de mora permitido
	'%     ntransmon1      -   Cantidad de meses transcurridos para pago de 1ra parte de siniestros
	'%     ntransmon2      -   Cantidad de meses transcurridos para pago de 2da parte de siniestros
	'%     nindper1        -   Porcentaje de indemnización, una vez transcurrido el primer período
	'%     nindper2        -   Porcentaje de indemnización, una vez transcurrido el segundo período
    Public Function InsPostCC001(ByVal nHeader As Boolean, ByVal sCodispl As String, ByVal nMainAction As Integer, ByVal sAction As String, ByVal nUsercode As Integer, ByVal sCertype As String, ByVal nBranch As Short, ByVal nProduct As Short, ByVal nPolicy As Integer, ByVal nCertif As Integer, ByVal dEffecdate As Date, ByVal ninsmodality As Integer, ByVal nguar_type As Integer, ByVal scontracnum As String, ByVal dcontracdat As Date, ByVal ntime_unit As Integer, ByVal dterm_date As Date, ByVal ntime_eject As Integer, ByVal ncredcau As Double, ByVal nCurrency As Integer, ByVal nindemper As Double, ByVal nmoraallow As Integer, ByVal ntransmon1 As Short, ByVal ntransmon2 As Short, ByVal nindper1 As Double, ByVal nindper2 As Double, ByVal sFollowUp As Char, ByVal sContractObject As String, ByVal nBondstatus As Integer, ByVal sInsurSector As Integer) As Boolean

        Dim lclsPolicyWin As ePolicy.Policy_Win

        InsPostCC001 = Update(nUsercode, sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate, ninsmodality, nguar_type, scontracnum, dcontracdat, ntime_unit, dterm_date, ntime_eject, ncredcau, nindemper, nmoraallow, ntransmon1, ntransmon2, nindper1, nindper2, sFollowUp, sContractObject, nBondstatus, sInsurSector)

        '+ Si la modalidad del seguro es Crédito se eliminan siempre todos los registros en la tabla Warranty
        '++ If the modality of the insurance is Credit eliminate always all the registries in the Warranty table
        If ninsmodality = 1 Then

            Call insPreCC001(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate, nUsercode, 1)

        End If

        If InsPostCC001 Then
            lclsPolicyWin = New ePolicy.Policy_Win
            Call lclsPolicyWin.Add_PolicyWin(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate, nUsercode, "CC001", "2")
            Call lclsPolicyWin.Add_PolicyWin(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate, nUsercode, "CA014", "1")
            lclsPolicyWin = Nothing
        End If

        Exit Function
    End Function


End Class






