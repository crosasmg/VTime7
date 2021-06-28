Option Strict Off
Option Explicit On
Public Class Contrnpro
	'%-------------------------------------------------------%'
	'% $Workfile:: Contrnpro.cls                            $%'
	'% $Author:: Vvera                                      $%'
	'% $Date:: 28/03/06 22:05                               $%'
	'% $Revision:: 2                                        $%'
	'%-------------------------------------------------------%'
	
	'+ Estructura de tabla contrnpro al 24-09-2002
	'+     Property                Type         DBType   Size Scale  Prec  Null
	'+-------------------------------------------------------------------------
	Public nNumber As Integer 'NOT NULL NUMBER(5)
	Public nType As Integer 'NOT NULL NUMBER(5)
	Public nBranch As Integer 'NOT NULL NUMBER(5)
	Public dEffecdate As Date 'NOT NULL DATE
	Public nAmount As Double 'Number(12)
	Public nType_rel As Integer 'NOT NULL NUMBER(5)
	Public nCession As Double 'Number(10)
	Public nClausule As Double 'Number(4, 2)
	Public dCompdate As Date 'Date
	Public nDeducible As Double 'Number(12)
	Public sDescript As String 'Char(30)
	Public nExcess As Double 'Number(12)
	Public nMax_even As Double 'Number(12)
	Public dNulldate As Date 'Date
	Public nNumber_rep As Integer 'Number(5)
	Public nPorc_rep As Double 'Number(4, 2)
	Public nPrem_dep As Double 'Number(10, 2)
	Public nPrem_fij As Double 'Number(10, 2)
	Public nPrem_min As Double 'Number(10, 2)
    Public nRate_fij As Double 'Number(6, 4)
    Public nRate_max As Double 'Number(6, 4)
    Public nRate_min As Double 'Number(6, 4)
	Public sReinsuran As String 'Char(1)
	Public nRetention As Double 'Number(12)
	Public nUsercode As Integer 'Number(5)
	Public nCurr_pay As Integer 'NOT NULL NUMBER(5)
	Public nInterest As Double 'Number(4, 2)
	Public nFreqpay As Integer 'Number(5)
	Public nNextmonthpa As Integer 'Number(5)
	Public nNextyearpa As Integer 'Number(5)
	Public nFreqct As Integer 'Number(5)
	Public nNextmonthc As Integer 'Number(5)
	Public nNextyearc As Integer 'Number(5)
	Public sRouCessPR As String 'Char(12)
	Public sRouCessCL As String 'Char(12)
    Public sAgreementPays As String 'Char(1)

    Public nLifeNum As Integer
    Public nSpcPriority As Integer
    Public nSpcLimit As Integer
    
	Public sRounetret As String
	Public nRate_nrifv As Double
	
	
	Public nYear As Integer
	Public nMonth As Integer
    Public sCuenTecn As String

    Public nMaxRespEven As Double
    Public nNumberRepEven As Integer
    Public sProrateRep As String
    Public nProc_pay As Double
    Public nPremium_deveng As Double
    Public nEpi As Double
    Public nTax As Double
    Public nClaimadj As Double
    Public nCapitalref As Double
    Public nPorc_pay As Double

    '+ Propiedades auxiliares
    Public dContrDate As Date
    Public nCurrency As Integer
    Private mvarLastModify As Date
    '*LastModifyDate: Esta propiedad se encarga de la fecha de última modificación del contrato
    Public ReadOnly Property LastModifyDate() As Date
        Get
            LastModifyDate = mvarLastModify
        End Get
    End Property

    '%Find: Se realiza la lectura para verificar la existencia del código del contrato proporcional
    Public Function Find(ByVal nNumber As Integer, ByVal nType As Integer, ByVal nBranch As Integer, ByVal dEffecdate As Date, Optional ByVal lblnFind As Boolean = False) As Boolean
        Dim lrecreaContrnpro As eRemoteDB.Execute

        lrecreaContrnpro = New eRemoteDB.Execute

        On Error GoTo Find_Err

        '+ Definición de parámetros para stored procedure 'insudb.reaContrnpro'
        '+ Información leída el 28/05/2001 04:15:42 p.m.

        With lrecreaContrnpro
            .StoredProcedure = "reaContrnpro"

            If nNumber > 0 Then
                .Parameters.Add("nNumber", nNumber, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            Else
                'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
                .Parameters.Add("nNumber", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            End If

            If nType > 0 Then
                .Parameters.Add("nType", nType, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            Else
                'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
                .Parameters.Add("nType", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            End If

            If nBranch > 0 Then
                .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            Else
                'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
                .Parameters.Add("nBranch", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            End If

            .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            If .Run Then
                Me.nNumber = .FieldToClass("nNumber")
                Me.nType = .FieldToClass("nType")
                Me.nBranch = .FieldToClass("nBranch")
                Me.dEffecdate = .FieldToClass("dEffecdate")
                Me.nAmount = .FieldToClass("nAmount")
                Me.nType_rel = .FieldToClass("nType_rel")
                Me.nCession = .FieldToClass("nCession")
                Me.nClausule = .FieldToClass("nClausule")
                Me.dCompdate = .FieldToClass("dCompdate")
                Me.nDeducible = .FieldToClass("nDeducible")
                Me.sDescript = .FieldToClass("sDescript")
                Me.nExcess = .FieldToClass("nExcess")
                Me.nMax_even = .FieldToClass("nMax_even")
                Me.dNulldate = .FieldToClass("dNulldate")
                Me.nNumber_rep = .FieldToClass("nNumber_rep")
                Me.nPorc_rep = .FieldToClass("nPorc_rep")
                Me.nPrem_dep = .FieldToClass("nPrem_dep")
                Me.nPrem_fij = .FieldToClass("nPrem_fij")
                Me.nPrem_min = .FieldToClass("nPrem_min")
                Me.nRate_fij = .FieldToClass("nRate_fij")
                Me.nRate_max = .FieldToClass("nRate_max")
                Me.nRate_min = .FieldToClass("nRate_min")
                Me.sReinsuran = .FieldToClass("sReinsuran")
                Me.nRetention = .FieldToClass("nRetention")
                Me.nUsercode = .FieldToClass("nUsercode")
                Me.nCurr_pay = IIf(.FieldToClass("nCurr_pay") <= 0, 1, .FieldToClass("nCurr_pay"))
                Me.nInterest = .FieldToClass("nInterest")
                Me.nFreqpay = IIf(.FieldToClass("nFreqpay") <= 0, 1, .FieldToClass("nFreqpay"))
                Me.nNextmonthpa = .FieldToClass("nNextmonthpa")
                Me.nNextyearpa = .FieldToClass("nNextyearpa")
                Me.nFreqct = IIf(.FieldToClass("nFreqct") <= 0, 1, .FieldToClass("nFreqct"))
                Me.nNextmonthc = .FieldToClass("nNextmonthc")
                Me.nNextyearc = .FieldToClass("nNextyearc")
                Me.sRouCessPR = .FieldToClass("sRouCessPR")
                Me.sRouCessCL = .FieldToClass("sRouCessCL")
                Me.sAgreementPays = .FieldToClass("sAgreementPays")
                Me.nRate_nrifv = .FieldToClass("nRate_nrifv")
                Me.sRounetret = .FieldToClass("sRounetret")
                Me.nMaxRespEven = .FieldToClass("nMaxRespEven")
                Me.nNumberRepEven = .FieldToClass("nNumberRepEven")
                Me.sProrateRep = .FieldToClass("sProrateRep")
                Me.nPorc_pay = .FieldToClass("nPorc_pay")
                Me.nPremium_deveng = .FieldToClass("npremium_deveng")
                Me.nEpi = .FieldToClass("nepi")
                Me.nTax = .FieldToClass("ntax")
                Me.nClaimadj = .FieldToClass("nclaimadj")
                Me.nCapitalref = .FieldToClass("ncapitalref")

                Me.nLifeNum = .FieldToClass("nLifeNum")
                Me.nSpcpriority = .FieldToClass("nSpcpriority")
                Me.nSpclimit = .FieldToClass("nSpclimit")

                Find = True
            Else
                Find = False
            End If
        End With
        'UPGRADE_NOTE: Object lrecreaContrnpro may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecreaContrnpro = Nothing

Find_Err:
        If Err.Number Then
            Find = False
        End If
    End Function

    '%insContrnPro: Creación de un registro en el archivo de los contratos de reaseguro no propocionales
    Public Function insContrnPro(ByVal sCodispl As String, Optional ByRef lblnUpdate As Boolean = False) As Boolean
        Dim lrecreaContrnpro As eRemoteDB.Execute
        Dim lblnFirstTime As Boolean

        lrecreaContrnpro = New eRemoteDB.Execute

        On Error GoTo insContrnPro_Err

        '+ Definición de parámetros para stored procedure 'insudb.reaContrnpro'
        '+ Información leída el 28/05/2001 04:40:08 p.m.

        lblnFirstTime = False

        With lrecreaContrnpro
            .StoredProcedure = "insContrnpro"
            '+Si se trata de los datos del contrato a nivel de cabecera
            If sCodispl = "CR304_K" Then

                If lblnUpdate Then
                    lblnFirstTime = False
                Else
                    lblnFirstTime = True
                End If

                '+ Se pasan los valores a los parámetros de la clave de la tabla.

                .Parameters.Add("nNumber", nNumber, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nType", nType, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("dEffecdate", dContrDate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nType_rel", nType_rel, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

                '+ La primera vez crea el registro con las variables claves y las demás con null.
                If lblnFirstTime Then
                    'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
                    .Parameters.Add("nAmount", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
                    .Parameters.Add("nCession", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
                    .Parameters.Add("nClausule", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 4, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
                    .Parameters.Add("nDeducible", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
                    .Parameters.Add("sDescript", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 30, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
                    .Parameters.Add("nExcess", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
                    .Parameters.Add("nMax_even", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
                    .Parameters.Add("dNulldate", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
                    .Parameters.Add("nNumber_rep", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
                    .Parameters.Add("nPorc_rep", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 4, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
                    .Parameters.Add("nPrem_dep", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
                    .Parameters.Add("nPrem_fij", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
                    .Parameters.Add("nPrem_min", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
                    .Parameters.Add("nRate_fij", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 4, 6, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
                    .Parameters.Add("nRate_max", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 4, 6, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
                    .Parameters.Add("nRate_min", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 4, 6, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
                    .Parameters.Add("sReinsuran", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
                    .Parameters.Add("nRetention", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Parameters.Add("nCurr_pay", Me.nCurr_pay, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Parameters.Add("nInterest", Me.nInterest, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 4, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Parameters.Add("nFreqpay", Me.nFreqpay, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Parameters.Add("nNextmonthpa", Me.nNextmonthpa, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Parameters.Add("nNextyearpa", Me.nNextyearpa, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Parameters.Add("nFreqct", Me.nFreqct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Parameters.Add("nNextmonthc", Me.nNextmonthc, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Parameters.Add("nNextyearc", Me.nNextyearc, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Parameters.Add("sRouCessPR", Me.sRouCessPR, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Parameters.Add("sRouCessCL", Me.sRouCessCL, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Parameters.Add("sAgreementPays", Me.sAgreementPays, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Parameters.Add("nRate_nrifv", Me.nRate_nrifv, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 4, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Parameters.Add("sRounetret", Me.sRounetret, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Parameters.Add("nMaxRespEven", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Parameters.Add("nNumberRepEven", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Parameters.Add("sProrateRep", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Parameters.Add("nporc_pay", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Parameters.Add("npremium_deveng", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Parameters.Add("nEpi", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Parameters.Add("nTax", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Parameters.Add("nClaimadj", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 9, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Parameters.Add("nCapitalref", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)


                    .Parameters.Add("nLifeNum", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Parameters.Add("nSpcPriority", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Parameters.Add("nSpclimit", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)


                Else
                    '+ En este caso (no la primera vez) se actualiza la tabla con lo que tenga el recorset de trabajo.

                    .Parameters.Add("nAmount", Me.nAmount, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Parameters.Add("nCession", Me.nCession, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Parameters.Add("nClausule", Me.nClausule, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 4, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Parameters.Add("nDeducible", Me.nDeducible, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Parameters.Add("sDescript", Me.sDescript, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 30, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Parameters.Add("nExcess", Me.nExcess, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Parameters.Add("nMax_even", Me.nMax_even, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Parameters.Add("dNulldate", Me.dNulldate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Parameters.Add("nNumber_rep", Me.nNumber_rep, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Parameters.Add("nPorc_rep", Me.nPorc_rep, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 4, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Parameters.Add("nPrem_dep", Me.nPrem_dep, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Parameters.Add("nPrem_fij", Me.nPrem_fij, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Parameters.Add("nPrem_min", Me.nPrem_min, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Parameters.Add("nRate_fij", Me.nRate_fij, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 4, 6, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Parameters.Add("nRate_max", Me.nRate_max, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 4, 6, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Parameters.Add("nRate_min", Me.nRate_min, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 4, 6, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Parameters.Add("sReinsuran", Me.sReinsuran, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Parameters.Add("nRetention", Me.nRetention, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Parameters.Add("nCurr_pay", Me.nCurr_pay, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Parameters.Add("nInterest", Me.nInterest, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 4, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Parameters.Add("nFreqpay", Me.nFreqpay, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Parameters.Add("nNextmonthpa", Me.nNextmonthpa, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Parameters.Add("nNextyearpa", Me.nNextyearpa, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Parameters.Add("nFreqct", Me.nFreqct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Parameters.Add("nNextmonthc", Me.nNextmonthc, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Parameters.Add("nNextyearc", Me.nNextyearc, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Parameters.Add("sRouCessPR", Me.sRouCessPR, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Parameters.Add("sRouCessCL", Me.sRouCessCL, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Parameters.Add("sAgreementPays", Me.sAgreementPays, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Parameters.Add("nRate_nrifv", Me.nRate_nrifv, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 4, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Parameters.Add("sRounetret", Me.sRounetret, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Parameters.Add("nMaxRespEven", Me.nMaxRespEven, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Parameters.Add("nNumberRepEven", Me.nNumberRepEven, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Parameters.Add("sProrateRep", Me.sProrateRep, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Parameters.Add("nporc_pay", Me.nPorc_pay, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Parameters.Add("npremium_deveng", Me.nPremium_deveng, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Parameters.Add("nEpi", Me.nEpi, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Parameters.Add("nTax", Me.nTax, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Parameters.Add("nClaimadj", Me.nClaimadj, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 9, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Parameters.Add("nCapitalref", Me.nCapitalref, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

                    .Parameters.Add("nLifeNum", Me.nLifeNum, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Parameters.Add("nSpcPriority", Me.nSpcpriority, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Parameters.Add("nSpclimit", Me.nSpclimit, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

                End If
            Else
                .Parameters.Add("nNumber", Me.nNumber, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nType", Me.nType, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nBranch", Me.nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("dEffecdate", Me.dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nType_rel", Me.nType_rel, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            End If

            If Not lblnFirstTime Then
                '+Si se trata del frame de límites
                If sCodispl = "CR304" Then
                    .Parameters.Add("nAmount", nAmount, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Parameters.Add("nCession", Me.nCession, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Parameters.Add("nClausule", Me.nClausule, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 4, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Parameters.Add("nDeducible", nDeducible, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Parameters.Add("sDescript", sDescript, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 30, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Parameters.Add("nExcess", nExcess, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Parameters.Add("nMax_even", nMax_even, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Parameters.Add("dNulldate", Me.dNulldate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Parameters.Add("nNumber_rep", nNumber_rep, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Parameters.Add("nPorc_rep", nPorc_rep, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 4, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Parameters.Add("nPrem_dep", Me.nPrem_dep, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Parameters.Add("nPrem_fij", Me.nPrem_fij, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Parameters.Add("nPrem_min", Me.nPrem_min, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Parameters.Add("nRate_fij", Me.nRate_fij, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 4, 6, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Parameters.Add("nRate_max", Me.nRate_max, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 4, 6, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Parameters.Add("nRate_min", Me.nRate_min, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 4, 6, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Parameters.Add("sReinsuran", Me.sReinsuran, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Parameters.Add("nRetention", nRetention, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Parameters.Add("nCurr_pay", Me.nCurr_pay, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Parameters.Add("nInterest", Me.nInterest, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 4, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Parameters.Add("nFreqpay", Me.nFreqpay, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Parameters.Add("nNextmonthpa", Me.nNextmonthpa, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Parameters.Add("nNextyearpa", Me.nNextyearpa, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Parameters.Add("nFreqct", Me.nFreqct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Parameters.Add("nNextmonthc", Me.nNextmonthc, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Parameters.Add("nNextyearc", Me.nNextyearc, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Parameters.Add("sRouCessPR", Me.sRouCessPR, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Parameters.Add("sRouCessCL", Me.sRouCessCL, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Parameters.Add("sAgreementPays", Me.sAgreementPays, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Parameters.Add("nRate_nrifv", Me.nRate_nrifv, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 4, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Parameters.Add("sRounetret", Me.sRounetret, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Parameters.Add("nMaxRespEven", Me.nMaxRespEven, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Parameters.Add("nNumberRepEven", Me.nNumberRepEven, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Parameters.Add("sProrateRep", Me.sProrateRep, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Parameters.Add("nporc_pay", Me.nPorc_pay, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Parameters.Add("npremium_deveng", Me.nPremium_deveng, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Parameters.Add("nEpi", Me.nEpi, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Parameters.Add("nTax", Me.nTax, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Parameters.Add("nClaimadj", Me.nClaimadj, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 9, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Parameters.Add("nCapitalref", Me.nCapitalref, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

                    .Parameters.Add("nLifeNum", Me.nLifeNum, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Parameters.Add("nSpcPriority", Me.nSpcpriority, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Parameters.Add("nSpclimit", Me.nSpclimit, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

                End If

                '+Si se trata del fgrama de tasas y primas
                If sCodispl = "CR305" Then
                    .Parameters.Add("nAmount", Me.nAmount, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Parameters.Add("nCession", Me.nCession, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Parameters.Add("nClausule", Me.nClausule, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 4, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Parameters.Add("nDeducible", Me.nDeducible, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Parameters.Add("sDescript", Me.sDescript, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 30, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Parameters.Add("nExcess", Me.nExcess, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Parameters.Add("nMax_even", Me.nMax_even, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Parameters.Add("dNulldate", eRemoteDB.Constants.dtmNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Parameters.Add("nNumber_rep", Me.nNumber_rep, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Parameters.Add("nPorc_rep", Me.nPorc_rep, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 4, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Parameters.Add("nPrem_dep", Me.nPrem_dep, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Parameters.Add("nPrem_fij", Me.nPrem_fij, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Parameters.Add("nPrem_min", Me.nPrem_min, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Parameters.Add("nRate_fij", Me.nRate_fij, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 4, 6, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Parameters.Add("nRate_max", Me.nRate_max, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 4, 6, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Parameters.Add("nRate_min", Me.nRate_min, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 4, 6, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Parameters.Add("sReinsuran", Me.sReinsuran, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Parameters.Add("nRetention", Me.nRetention, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Parameters.Add("nCurr_pay", Me.nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Parameters.Add("nInterest", Me.nInterest, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 4, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Parameters.Add("nFreqpay", Me.nFreqpay, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Parameters.Add("nNextmonthpa", Me.nNextmonthpa, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Parameters.Add("nNextyearpa", Me.nNextyearpa, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Parameters.Add("nFreqct", Me.nFreqct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Parameters.Add("nNextmonthc", Me.nNextmonthc, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Parameters.Add("nNextyearc", Me.nNextyearc, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Parameters.Add("sRouCessPR", Me.sRouCessPR, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Parameters.Add("sRouCessCL", Me.sRouCessCL, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Parameters.Add("sAgreementPays", Me.sAgreementPays, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Parameters.Add("nRate_nrifv", Me.nRate_nrifv, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 4, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Parameters.Add("sRounetret", Me.sRounetret, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Parameters.Add("nMaxRespEven", Me.nMaxRespEven, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Parameters.Add("nNumberRepEven", Me.nNumberRepEven, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Parameters.Add("sProrateRep", Me.sProrateRep, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Parameters.Add("nporc_pay", Me.nPorc_pay, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Parameters.Add("npremium_deveng", Me.nPremium_deveng, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Parameters.Add("nEpi", Me.nEpi, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Parameters.Add("nTax", Me.nTax, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Parameters.Add("nClaimadj", Me.nClaimadj, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 9, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Parameters.Add("nCapitalref", Me.nCapitalref, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Parameters.Add("nLifeNum", Me.nLifeNum, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Parameters.Add("nSpcPriority", Me.nSpcpriority, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Parameters.Add("nSpclimit", Me.nSpclimit, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

                End If
            End If
            insContrnPro = .Run(False)
        End With

        'UPGRADE_NOTE: Object lrecreaContrnpro may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecreaContrnpro = Nothing

insContrnPro_Err:
        If Err.Number Then
            insContrnPro = False
        End If
    End Function

    '%insValCR304_k: Esta función se encarga de validar los datos introducidos en la forma CR304_k (Header).
    Public Function insValCR304_k(ByVal sCodispl As String, ByVal nAction As Integer, ByVal dEffecdate As Date, ByVal nNumber As Integer, ByVal nContraType As Integer, ByVal nBranch As Integer) As String
        Dim lclsContrmaster As eCoReinsuran.Contrmaster
        Dim lclsErrors As eFunctions.Errors
        Dim lintReten As Integer

        On Error GoTo insValCR304_k_Err

        lclsContrmaster = New eCoReinsuran.Contrmaster
        lclsErrors = New eFunctions.Errors

        '+Validacion de la fecha del contrato

        If dEffecdate = eRemoteDB.Constants.dtmNull Then
            Call lclsErrors.ErrorMessage(sCodispl, 1103)
        End If

        '+Si se trata del campo que indica el código del contrato

        If nNumber = 0 Or nNumber = eRemoteDB.Constants.intNull Then
            '+Se valida que el código del contrato este lleno
            Call lclsErrors.ErrorMessage(sCodispl, 6015)
        End If

        '+Validacion del tipo del contrato

        If nAction = eFunctions.Menues.TypeActions.clngActionadd Then
            If nContraType = eRemoteDB.Constants.intNull Or nContraType = 0 Then
                Call lclsErrors.ErrorMessage(sCodispl, 6018)
            End If
        End If

        '+Validacion del ramo

        If nAction = eFunctions.Menues.TypeActions.clngActionadd Then
            If nBranch = 0 Or nBranch = eRemoteDB.Constants.intNull Then
                Call lclsErrors.ErrorMessage(sCodispl, 1022)
            End If
        End If

        '+Si se trata del campo que indica el código del contrato
        If nNumber <> eRemoteDB.Constants.intNull And nNumber <> 0 Then
            '+Si la acción es consulta se valida que el contrato este en el archivo de contratos
            If nAction = eFunctions.Menues.TypeActions.clngActionQuery Then
                If Not lclsContrmaster.Find(Me.nType_rel, nNumber, 0, 0, dEffecdate) Then
                    Call lclsErrors.ErrorMessage(sCodispl, 6090)
                Else
                    Me.nCurrency = lclsContrmaster.CodeCurrency
                    If Not Find(nNumber, 0, 0, dEffecdate) Then
                        Call lclsErrors.ErrorMessage(sCodispl, 6090)
                    End If
                End If
            End If

            '+Si la acción es registrar se valida que el contrato no este en el archivo de contratos
            If nAction = eFunctions.Menues.TypeActions.clngActionadd Then
                If lclsContrmaster.Find_Num(nNumber) Then
                    Call lclsErrors.ErrorMessage(sCodispl, 6023)
                End If

                If Find(nNumber, nContraType, nBranch, dEffecdate) Then
                    Call lclsErrors.ErrorMessage(sCodispl, 6092)
                End If
            End If
        End If

        '+Si se trata del campo que indica el código del contrato
        If nNumber <> eRemoteDB.Constants.intNull And nNumber <> 0 Then
            '+Si la acción es Modificar se valida que el contrato este en el archivo de contratos
            If nAction = eFunctions.Menues.TypeActions.clngActionUpdate Then
                If Not lclsContrmaster.Find(Me.nType_rel, nNumber, 0, 0, dEffecdate) Then
                    Call lclsErrors.ErrorMessage(sCodispl, 6090)
                Else
                    Me.nCurrency = lclsContrmaster.CodeCurrency
                    If Not Find(nNumber, 0, 0, dEffecdate) Then
                        Call lclsErrors.ErrorMessage(sCodispl, 6090)
                    End If
                End If
            End If
        End If

        '+Si la acción es modificar se valida que la fecha de modificación sea mayor o igual a la de última
        '+modificación
        If nAction = eFunctions.Menues.TypeActions.clngActionUpdate Then
            If insValLastnModify(nNumber, nContraType, nBranch) Then
                If dEffecdate < Me.LastModifyDate Then
                    Call lclsErrors.ErrorMessage(sCodispl, 1021, , eFunctions.Errors.TextAlign.RigthAling, " - " & Me.LastModifyDate)
                End If
            End If
        End If

        insValCR304_k = lclsErrors.Confirm
        'UPGRADE_NOTE: Object lclsContrmaster may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsContrmaster = Nothing
        'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsErrors = Nothing


insValCR304_k_Err:
        If Err.Number Then
            insValCR304_k = insValCR304_k & Err.Description
        End If
    End Function
    '%insPostCR304_k: Esta función se encarga de validar los datos introducidos en la zona de
    '%cabecera.
    Public Function insPostCR304_k(ByVal sCodispl As String, ByVal nAction As String, ByVal nNumber As Integer, ByVal dStartdate As Date, ByVal nContraType As Integer, ByVal nBranch As Integer, ByVal nUsercode As Integer) As Boolean
        Dim lclsContrnpro As eCoReinsuran.Contrnpro
        Dim lclsContrmaster As eCoReinsuran.Contrmaster

        On Error GoTo insPostCR304_k_Err

        lclsContrnpro = New eCoReinsuran.Contrnpro
        lclsContrmaster = New eCoReinsuran.Contrmaster

        insPostCR304_k = True

        '+Se inicializan los valores de la llave del contrato

        With lclsContrmaster
            .nType_rel = nType_rel
            .nNumber = nNumber
            .nType = nContraType
            .nBranch = nBranch
            .nUsercode = nUsercode
            .dStartdate = dStartdate
        End With

        With Me
            .nNumber = nNumber
            .dContrDate = dStartdate
            .dEffecdate = dStartdate
            .nUsercode = nUsercode

            If Not .Find(nNumber, nContraType, nBranch, dEffecdate) Then
                .nFreqct = 1
                .nFreqpay = 1
                .nCurr_pay = 1
            Else
                .dEffecdate = dStartdate
            End If



            If nAction = CStr(eFunctions.Menues.TypeActions.clngActionadd) Then
                .nType = nContraType
                .dEffecdate = dStartdate
                .nBranch = nBranch
            End If

            Select Case nAction
                '+Si la opción seleccionada es Registrar

                Case CStr(eFunctions.Menues.TypeActions.clngActionadd)
                    If lclsContrmaster.creContrMaster Then
                        If .insContrnPro("CR304_K") Then
                            insPostCR304_k = True
                        End If
                    Else
                        insPostCR304_k = False
                    End If

                    '+Si la opción seleccionada es Consulta

                Case CStr(eFunctions.Menues.TypeActions.clngActionQuery)
                    insPostCR304_k = .Find(nNumber, 0, 0, dStartdate, True)

                    If insPostCR304_k Then

                        nContraType = .nType
                        dEffecdate = .dEffecdate
                        nBranch = .nBranch

                        .nType = nContraType
                        .dEffecdate = dStartdate
                        .nBranch = nBranch
                    End If
                Case CStr(eFunctions.Menues.TypeActions.clngActionUpdate)
                    insPostCR304_k = .insContrnPro("CR304_K", True)
            End Select
        End With

insPostCR304_k_Err:
        If Err.Number Then
            insPostCR304_k = False
        End If
    End Function
    '%insValCR304:En esta funcion se realizan las validaciones correspondientes a la forma CR304 (Folder).
    Public Function insValCR304(ByVal sCodispl As String, ByVal nContraType As Integer, ByVal nCurrency As Integer, ByVal sDescript As String, ByVal nRetention As Double, ByVal nAmount As Double, ByVal nExcess As Double, ByVal nNumber_rep As Integer, ByVal nPorc_rep As Double, ByVal nMax_even As Double, ByVal nCurrencyPayment As Integer, ByVal dEffecdate As Date, ByVal dEndDate As Date, ByVal nNumberRepEven As Integer, ByVal nLifeNum As Integer, ByVal nSpcPriority As Integer, ByVal nSpcLimit As Integer) As String
        Dim lclsErrors As eFunctions.Errors
        Dim lclsValField As eFunctions.valField

        lclsErrors = New eFunctions.Errors
        lclsValField = New eFunctions.valField
        lclsValField.objErr = lclsErrors

        On Error GoTo insValCR304_Err

        '+Se realiza la validación del campo fecha de efecto
        If dEndDate <> eRemoteDB.Constants.dtmNull Then
            If dEndDate <= dEffecdate Then
                Call lclsErrors.ErrorMessage("CR304", 60823)
            End If
        End If
        '+Se realiza la validación del campo moneda

        If nCurrency = eRemoteDB.Constants.intNull Or nCurrency = 0 Then
            Call lclsErrors.ErrorMessage(sCodispl, 1351)
        End If

        If nCurrencyPayment <= 0 Then
            Call lclsErrors.ErrorMessage(sCodispl, 1351)
        End If

        '+Se realiza la validación del campo descripción

        If sDescript = String.Empty Then
            Call lclsErrors.ErrorMessage(sCodispl, 6123)
        End If

        '+Se realiza la validación del campo Exceso de
        If nExcess = eRemoteDB.Constants.intNull Or nExcess = 0 Then
            Call lclsErrors.ErrorMessage(sCodispl, 6060)
        Else

            '+Se realiza la validación del campo límite de indemnización
            If nRetention = eRemoteDB.Constants.intNull Or nRetention = 0 Then
                Call lclsErrors.ErrorMessage(sCodispl, 6124)
            Else

                '+ Si este campo está lleno y el "límite de indemnización" está lleno,
                '+ el contenido de este campo debe ser inferior al campo "Límite de indemnización"
                If nExcess >= nRetention Then
                    Call lclsErrors.ErrorMessage(sCodispl, 6036)
                End If
            End If
        End If

        '+Se realiza la validación del campo Reposición-número

        If nAmount <= 0 Then
            If nNumber_rep > 0 Then
                Call lclsErrors.ErrorMessage(sCodispl, 6065, , , " - Número de reposiciones")
            End If
        End If

        '+Se realiza la validación del campo Reposiciones-%Sobre prima

        If nNumber_rep <= 0 Then
            If nPorc_rep > 0 Then
                Call lclsErrors.ErrorMessage(sCodispl, 6125)
            End If
        End If

        If (nRetention <> 0 And nRetention <> eRemoteDB.Constants.intNull) And (nMax_even <> 0 And nMax_even <> eRemoteDB.Constants.intNull) Then
            If nRetention < nMax_even Then
                Call lclsErrors.ErrorMessage(sCodispl, 6145)
            End If
        End If

        '+Se realiza la validación del campo Reposición-por evento

        If nAmount <= 0 Then
            If nNumberRepEven > 0 Then
                Call lclsErrors.ErrorMessage(sCodispl, 6065, , , " - Número Por evento de reposiciones")
            End If
        End If

        '+Se realiza la validación de los importes para condiciones especiales
        If nSpcPriority > 0 Then
            If nSpcLimit > 0 Then
                '+El límite debe ser mayor a la prioridad.
                If nSpcLimit <= nSpcPriority Then
                    Call lclsErrors.ErrorMessage(sCodispl, 90500)
                End If
            Else
                '+Si la prioridad esta lleno el límite debe estar lleno
                Call lclsErrors.ErrorMessage(sCodispl, 90501)
            End If
        Else
            '+Si la prioridad no tiene valor el límite tampoco debe estar indicado.
            If nSpcLimit > 0 Then
                Call lclsErrors.ErrorMessage(sCodispl, 90502)
            End If
        End If
        insValCR304 = lclsErrors.Confirm

insValCR304_Err:
        If Err.Number Then
            insValCR304 = insValCR304 & Err.Description
        End If
        'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsErrors = Nothing
        'UPGRADE_NOTE: Object lclsValField may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsValField = Nothing
        On Error GoTo 0
    End Function

    '%insPostCR304: Esta función se encarga de realizar las actualizaciones en las
    '%diferentes tablas involucradas
    Public Function insPostCR304(ByVal sCodispl As String, ByVal nAction As Integer, ByVal nUsercode As Integer, ByVal dEffecdate As Date, ByVal nNumber As Integer, ByVal nContraType As Integer, ByVal nBranch As Integer, ByVal nCurrency As Integer, ByVal sDescript As String, ByVal nRetention As Double, ByVal nAmount As Double, ByVal nExcess As Double, ByVal nNumber_rep As Integer, ByVal nPorc_rep As Double, ByVal nMax_even As Double, ByVal nDeducible As Double, ByVal nCurr_pay As Integer, ByVal nInterest As Double, ByVal nFreqpay As Integer, ByVal nNextmonthpa As Integer, ByVal nNextyearpa As Integer, ByVal nFreqct As Integer, ByVal nNextmonthc As Integer, ByVal nNextyearc As Integer, ByVal sRouCessCL As String, ByVal dEndDate As Date, ByVal nMaxRespEven As Double, ByVal nNumberRepEven As Integer, ByVal sProrateRep As String, ByVal nLifeNum As Integer, ByVal nsPcPriority As Integer, ByVal nSpcLimit As Integer) As Boolean
        Dim lclsContrmaster As eCoReinsuran.Contrmaster
        Dim lclsContrnpro As eCoReinsuran.Contrnpro
        Dim NewMonth As Integer
        Dim NewYear As Integer

        lclsContrmaster = New eCoReinsuran.Contrmaster
        lclsContrnpro = New eCoReinsuran.Contrnpro

        On Error GoTo insPostCR304_Err

        insPostCR304 = True

        '+Si la opción seleccionada es Consultar
        If nAction = eFunctions.Menues.TypeActions.clngActionadd Or nAction = eFunctions.Menues.TypeActions.clngActionUpdate Then

            With Me
                Call .Find(nNumber, nContraType, nBranch, dEffecdate, True)
                .nRetention = nRetention
                .nCurrency = nCurrency
                .dEffecdate = dEffecdate
                .nBranch = nBranch
                .nNumber = nNumber
                .dEffecdate = dEffecdate
                .nType = nContraType
                .sDescript = sDescript
                .nDeducible = IIf(nDeducible = eRemoteDB.Constants.intNull, 0, nDeducible)
                .nAmount = IIf(nAmount = eRemoteDB.Constants.intNull, 0, nAmount)
                .nExcess = IIf(nExcess = eRemoteDB.Constants.intNull, 0, nExcess)
                .nMax_even = IIf(nMax_even = eRemoteDB.Constants.intNull, 0, nMax_even)
                .nPorc_rep = IIf(nPorc_rep = eRemoteDB.Constants.intNull, 0, nPorc_rep)
                .nNumber_rep = IIf(nNumber_rep = eRemoteDB.Constants.intNull, 0, nNumber_rep)
                .nUsercode = nUsercode
                .sRouCessCL = sRouCessCL
                .nInterest = nInterest
                .nCurr_pay = nCurr_pay
                .nMaxRespEven = IIf(nMaxRespEven = eRemoteDB.Constants.intNull, 0, nMaxRespEven)
                .nNumberRepEven = IIf(nNumberRepEven = eRemoteDB.Constants.intNull, 0, nNumberRepEven)
                .sProrateRep = sProrateRep
                .nLifeNum = nLifeNum
                .nSpcPriority = nsPcPriority
                .nSpcLimit = nSpcLimit


                '+ Se calcula el mes y el año de la próxima generación de cuenta técnica según pa periodicidad indicada
                Select Case nAction
                    '+ Si la acción es registrar:
                    Case 301
                        NewYear = Year(dEffecdate)
                        Select Case nFreqct
                            Case 1
                                NewMonth = Month(dEffecdate) + 12
                            Case 2
                                NewMonth = Month(dEffecdate) + 6
                            Case 3
                                NewMonth = Month(dEffecdate) + 3
                            Case 4
                                NewMonth = Month(dEffecdate) + 1
                        End Select
                        If NewMonth > 12 Then
                            NewYear = NewYear + 1
                            NewMonth = NewMonth - 12
                        End If
                        .nNextmonthc = NewMonth
                        .nNextyearc = NewYear
                        '+ Si la acción es actualizar y el usuario cambió la periodicidad:
                    Case 302
                        NewYear = Year(dEffecdate)
                        If .nFreqct <> nFreqct Then
                            Select Case nFreqct
                                Case 1
                                    NewMonth = Month(dEffecdate) + 12
                                Case 2
                                    NewMonth = Month(dEffecdate) + 6
                                Case 3
                                    NewMonth = Month(dEffecdate) + 3
                                Case 4
                                    NewMonth = Month(dEffecdate) + 1
                            End Select
                            If NewMonth > 12 Then
                                NewYear = NewYear + 1
                                NewMonth = NewMonth - 12
                            End If
                            .nNextmonthc = NewMonth
                            .nNextyearc = NewYear
                        End If
                End Select
                '+ Se calcula el mes y el año de la próxima generación de orden de pago según pa periodicidad indicada
                Select Case nAction
                    '+ Si la acción es registrar:
                    Case 301
                        NewYear = Year(dEffecdate)
                        Select Case nFreqpay
                            Case 1
                                NewMonth = Month(dEffecdate) + 12
                            Case 2
                                NewMonth = Month(dEffecdate) + 6
                            Case 3
                                NewMonth = Month(dEffecdate) + 3
                            Case 4
                                NewMonth = Month(dEffecdate) + 1
                        End Select
                        If NewMonth > 12 Then
                            NewYear = NewYear + 1
                            NewMonth = NewMonth - 12
                        End If
                        .nNextmonthpa = NewMonth
                        .nNextyearpa = NewYear
                        '+ Si la acción es actualizar y el usuario cambió la periodicidad:
                    Case 302
                        If .nFreqpay <> nFreqpay Then
                            NewYear = Year(dEffecdate)
                            Select Case nFreqct
                                Case 1
                                    NewMonth = Month(dEffecdate) + 12
                                Case 2
                                    NewMonth = Month(dEffecdate) + 6
                                Case 3
                                    NewMonth = Month(dEffecdate) + 3
                                Case 4
                                    NewMonth = Month(dEffecdate) + 1
                            End Select
                            If NewMonth > 12 Then
                                NewYear = NewYear + 1
                                NewMonth = NewMonth - 12
                            End If
                            .nNextmonthpa = NewMonth
                            .nNextyearpa = NewYear
                        End If
                End Select
                .nFreqct = IIf(nFreqct <= 0, 1, nFreqct)
                .nFreqpay = IIf(nFreqpay <= 0, 1, nFreqpay)
            End With

            With lclsContrmaster
                .nType_rel = 2
                .nCurrency = nCurrency
                .nBranch = nBranch
                .nNumber = nNumber
                .nType = nContraType
                .nUsercode = nUsercode
                .dExpirdate = dEndDate
                .nCurr_pay = nCurr_pay

                insPostCR304 = insContrnPro("CR304")
                insPostCR304 = .updContrMasterCurrency()
                insPostCR304 = .updContrMasterExpirdat(nNumber, dEndDate, nUsercode)
            End With
        End If
        'UPGRADE_NOTE: Object lclsContrmaster may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsContrmaster = Nothing
        'UPGRADE_NOTE: Object lclsContrnpro may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsContrnpro = Nothing

insPostCR304_Err:
        If Err.Number Then
            insPostCR304 = False
        End If
    End Function
    'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
    Private Sub Class_Initialize_Renamed()
        '+Se indica que el código de la moneda por defecto
        nCurrency = 1

        '+Se indica que el tipo de contratos a procesar son los proporcionales
        nType_rel = 2

    End Sub
    Public Sub New()
        MyBase.New()
        Class_Initialize_Renamed()
    End Sub

    '%insValLastnModify: Se realiza la validación de la fecha de última modificación del contrato
    Public Function insValLastnModify(ByVal nNumber As Integer, ByVal nType As Integer, ByVal nBranch As Integer) As Boolean
        Dim lrecreaContrnpro_effecdate As eRemoteDB.Execute

        lrecreaContrnpro_effecdate = New eRemoteDB.Execute

        On Error GoTo insValLastnModify_Err

        '+ Definición de parámetros para stored procedure 'insudb.reaContrnpro_effecdate'
        ' Información leída el 07/06/2001 10:30:09 a.m.

        With lrecreaContrnpro_effecdate
            .StoredProcedure = "reaContrnpro_effecdate"
            .Parameters.Add("nNumber", nNumber, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nType", nType, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If .Run Then
                mvarLastModify = .FieldToClass("LastDate")
                insValLastnModify = True
            Else
                insValLastnModify = False
            End If
        End With
        'UPGRADE_NOTE: Object lrecreaContrnpro_effecdate may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecreaContrnpro_effecdate = Nothing

insValLastnModify_Err:
        If Err.Number Then
            insValLastnModify = False
        End If
    End Function

    '%insValCR305:En esta funcion se realizan las validaciones correspondientes a la forma 'CR305'
    Public Function insValCR305(ByVal sCodispl As String, ByVal nNumber As Integer, ByVal nType As Integer, ByVal nBranch As Integer, ByVal dEffecdate As Date, ByVal nPrem_dep As Double, ByVal nRate_max As Double, ByVal nRate_min As Double, ByVal nRate_fij As Double, ByVal nPrem_fij As Double, ByVal nPrem_min As Double, ByVal nYear As Integer, ByVal nMonth As Integer, ByVal nCession As Double, ByVal sWindowsType As String, Optional ByVal nRate_nrifv As Double = 0, Optional ByVal sRounetret As String = "", Optional ByVal sIndNrifv As String = "") As String
        Dim lclsErrors As eFunctions.Errors
        Dim lclsContrmaster As eCoReinsuran.Contrmaster

        lclsErrors = New eFunctions.Errors
        lclsContrmaster = New eCoReinsuran.Contrmaster

        On Error GoTo insValCR305_Err

        If sWindowsType <> "PopUp" Then
            '+Se realiza la validación del campo depósito de prima
            '+Se realiza la validación del campo prima-fija
            If nPrem_dep <> 0 Or nPrem_dep <> eRemoteDB.Constants.intNull Then
                If nPrem_fij > 0 Then
                    If nPrem_dep > nPrem_fij Or nPrem_fij < nPrem_dep Then
                        Call lclsErrors.ErrorMessage(sCodispl, 6062)
                    End If
                End If
            End If

            '+Se realiza la validación del campo tasa máxima
            If nRate_max > 0 Then
                If nRate_min > 0 Then
                    If nRate_max < nRate_min Then
                        Call lclsErrors.ErrorMessage(sCodispl, 6016)
                    End If
                End If
            End If

            '+Se realiza la validación del campo prima-minima
            If nPrem_min > 0 Then
                If (nRate_fij = 0 Or nRate_fij = eRemoteDB.Constants.intNull) And (nRate_min = 0 Or nRate_min = eRemoteDB.Constants.intNull) And (nRate_max = 0 Or nRate_max = eRemoteDB.Constants.intNull) Then
                    Call lclsErrors.ErrorMessage(sCodispl, 6089)
                End If
            End If

            '+Se realiza la validación para el ingreso de monto o rutina para manejo de NRIFV
            If sIndNrifv <> String.Empty Then
                If (nRate_nrifv = eRemoteDB.Constants.intNull Or nRate_nrifv = 0) And (sRounetret = String.Empty Or sRounetret = "") Then
                    Call lclsErrors.ErrorMessage(sCodispl, 300000)
                End If
            End If

        Else
            '+Se realiza la validación del campo año
            If nYear = 0 Or nYear = eRemoteDB.Constants.intNull Then
                Call lclsErrors.ErrorMessage(sCodispl, 60338)
            Else
                If lclsContrmaster.Find(2, nNumber, nType, nBranch, dEffecdate) Then
                    If nYear < CDbl(Right(CStr(lclsContrmaster.dStartdate), 4)) Then
                        Call lclsErrors.ErrorMessage(sCodispl, 60339)
                    End If
                End If
            End If

            '+Se realiza la validación del campo mes
            If nMonth = 0 Or nMonth = eRemoteDB.Constants.intNull Then
                Call lclsErrors.ErrorMessage(sCodispl, 60340)
            Else
                If nMonth < 1 Or nMonth > 12 Then
                    Call lclsErrors.ErrorMessage(sCodispl, 1115)
                End If
            End If

            '+Se realiza la validación del campo cesion a pagar
            If nCession = 0 Or nCession = eRemoteDB.Constants.intNull Then
                Call lclsErrors.ErrorMessage(sCodispl, 60341)
            End If
        End If

        insValCR305 = lclsErrors.Confirm

insValCR305_Err:
        If Err.Number Then
            insValCR305 = insValCR305 & Err.Description
        End If
        On Error GoTo 0

        'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsErrors = Nothing
        'UPGRADE_NOTE: Object lclsContrmaster may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsContrmaster = Nothing
    End Function

    '%insPostCR305: Esta función se encarga de realizar las actualizaciones en las
    '%diferentes tablas involucradas
    Public Function insPostCR305(ByVal sCodispl As String, ByVal nAction As Integer, ByVal nUsercode As Integer, ByVal dEffecdate As Date, ByVal nNumber As Integer, ByVal nContraType As Integer, ByVal nBranch As Integer, ByVal sReinsuran As String, ByVal nPrem_dep As Double, ByVal nPrem_fij As Double, ByVal nRate_max As Double, ByVal nRate_min As Double, ByVal nRate_fij As Double, ByVal nPrem_min As Double, ByVal nYear As Integer, ByVal nMonth As Integer, ByVal nCession As Double, ByVal sWindowsType As String, ByVal sAgreementPays As Integer, Optional ByVal sRoutine As String = "", Optional ByVal nRate_nrifv As Double = 0, Optional ByVal sRounetret As String = "", Optional ByVal nPorc_pay As Double = 0, Optional ByVal nPremium_deveng As Double = 0, Optional ByVal nEpi As Double = 0, Optional ByVal nTax As Double = 0, Optional ByVal nClaimadj As Double = 0, Optional ByVal nCapitalref As Double = 0) As Boolean
        Dim lclsContrnpro As eCoReinsuran.Contrnpro


        lclsContrnpro = New eCoReinsuran.Contrnpro

        On Error GoTo insPostCR305_Err

        insPostCR305 = True

        If sWindowsType <> "PopUp" Then
            '+Si la opción seleccionada es Consultar
            With Me
                Call Find(nNumber, nContraType, nBranch, dEffecdate)
                .dEffecdate = dEffecdate
                .nBranch = nBranch
                .nNumber = nNumber
                .nType = nContraType
                .sReinsuran = IIf(sReinsuran = "1", "1", "2")
                .nPrem_dep = nPrem_dep
                .nPrem_fij = nPrem_fij
                .nRate_max = nRate_max
                .nRate_min = nRate_min
                .nRate_fij = nRate_fij
                .nPrem_min = nPrem_min
                .nUsercode = nUsercode
                .sRounetret = sRounetret
                .nRate_nrifv = nRate_nrifv
                .sAgreementPays = IIf(sAgreementPays = CDbl("1"), "1", "2")
                .sRouCessPR = sRoutine
                .nPorc_pay = nPorc_pay
                .nPremium_deveng = nPremium_deveng
                .nEpi = nEpi
                .nTax = nTax
                .nClaimadj = nClaimadj
                .nCapitalref = nCapitalref
                insPostCR305 = .insContrnPro("CR305")
            End With
        Else
            With Me
                .nNumber = nNumber
                .nBranch = nBranch
                .nType = nContraType
                .dEffecdate = dEffecdate
                .nYear = nYear
                .nMonth = nMonth
                .nAmount = nCession
                .sCuenTecn = "2"
                .nUsercode = nUsercode
                insPostCR305 = .insContrnp_Pays(nAction)
            End With
        End If
        'UPGRADE_NOTE: Object lclsContrnpro may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsContrnpro = Nothing

insPostCR305_Err:
        If Err.Number Then
            insPostCR305 = False
        End If
    End Function
	
	'%insContrnPro: Creación de un registro en el archivo de los contratos de reaseguro no propocionales
	Public Function insContrnp_Pays(ByVal nAction As Integer) As Boolean
		Dim lrecreaContrnp_pay As eRemoteDB.Execute
		
		lrecreaContrnp_pay = New eRemoteDB.Execute
		
		On Error GoTo insContrnp_pay_Err
		
		'+ Definición de parámetros para stored procedure 'insudb.reaContrnpro'
		'+ Información leída el 28/05/2001 04:40:08 p.m.
		
		With lrecreaContrnp_pay
			.StoredProcedure = "insupdContrnp_pay"
			
			.Parameters.Add("nNumber", nNumber, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nType", nType, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nYear", Me.nYear, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nMonth", Me.nMonth, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAmount", Me.nAmount, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sCuenTecn", Me.sCuenTecn, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAction", nAction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			insContrnp_Pays = .Run(False)
		End With
		
		'UPGRADE_NOTE: Object lrecreaContrnp_pay may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaContrnp_pay = Nothing
		
insContrnp_pay_Err: 
		If Err.Number Then
			insContrnp_Pays = False
		End If
		On Error GoTo 0
	End Function
End Class
