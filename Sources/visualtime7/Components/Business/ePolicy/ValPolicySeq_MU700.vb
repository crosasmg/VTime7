Imports eFunctions.Extensions
Imports eRemoteDB.Parameter
Imports eFunctions
Public Class ValPolicySeq_MU700

    Dim resxValues As IEnumerable(Of DictionaryEntry) = eFunctions.Values.GetResxValue("MU700")
    Dim lclsErrors As New eFunctions.Errors
    Dim mobjValues As New eFunctions.Values

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <remarks></remarks>
    Public Function insValMU700(ByVal SCERTYPE As String, ByVal NPRODUCT As Long, ByVal NBRANCH As Long,
                                ByVal NPOLICY As Long, ByVal NCERTIF As Long, ByVal DEFFECDATE As Date,
                                ByVal NCAPITAL As Double, ByVal DEXPIRDAT As Date, ByVal DISSUEDAT As Date,
                                ByVal NNULLCODE As Long, ByVal DNULLDATE As Date, ByVal NPREMIUM As Double,
                                ByVal DSTARTDATE As Date, ByVal NUSERCODE As Long, ByVal NTRANSACTIO As Long,
                                ByVal NSITUATION As Long, ByVal NGROUP As Long, ByVal SCLIENT As String,
                                ByVal NCONSTCAT As Long, ByVal NCODKIND As Long, ByVal NPAYFREQ As Long,
                                ByVal NSISMICZONE As Long, ByVal NFI_POLICYTYPE As Integer, ByVal NINSURTYPE As Integer,
                                ByVal NNUMBEROFEMPLOYEES As Long, ByVal NINSURED As Double, ByVal NTHEFTCAPITAL As Double,
                                ByVal NSECURITYMEN As Integer, ByVal NAREA As Double, ByVal SIND_FIDELITY As String,
                                ByVal SIND_ELECTRONIC As String, ByVal SIND_MACHINE As String, ByVal SIND_CONTRACTOR As String,
                                ByVal sRequieredSections As String, ByVal NMONEY_TRANSIT As Double, ByVal NMONEY_PERMANENCE As Double) As String

        'Valores de la variable sRequieredSections 
        ' 1 - Identificacion del riesgo
        ' 2 - Informacion particular de robo
        ' 3 - Equipo electrónico
        ' 4 - Rotura de maquinaria
        ' 5 - Equipo y maquina de contrantistas
        ' 6 - Fidelidad privada
        ' 7 - Dinero y valores

        Dim result As String = String.Empty

        If sRequieredSections.Split(",").Contains("1") Then ' 1 - Identificacion del riesgo
            'Identificacion del riesgo - Giro de negocio (Debe estar lleno)
            If NCODKIND = eRemoteDB.Constants.intNull Then
                lclsErrors.ErrorMessage("MU700", 94022)
            End If

            'Identificacion del riesgo - Zona del riesgo (Debe estar lleno)
            If NSISMICZONE = eRemoteDB.Constants.intNull Then
                lclsErrors.ErrorMessage("MU700", 1135)
            End If

            'Identificacion del riesgo - Categoria de la construcción (Debe estar lleno)
            If NCONSTCAT = eRemoteDB.Constants.intNull Then
                lclsErrors.ErrorMessage("MU700", 94098)
            End If
        End If

        If sRequieredSections.Split(",").Contains("2") Then ' 2 - Informacion particular de robo
            'Informacion particular robo - Valor asegurado primer riesgo
            'Informacion particular robo - % Asegurado primer riesgo
            If NTHEFTCAPITAL = eRemoteDB.Constants.intNull And NINSURED = eRemoteDB.Constants.intNull Then
                lclsErrors.ErrorMessage("MU700", 55665, , eFunctions.Errors.TextAlign.LeftAling, resxValues.FindDictionaryValue("NINSURED_MultiRisk_Caption"))
                lclsErrors.ErrorMessage("MU700", 55665, , eFunctions.Errors.TextAlign.LeftAling, resxValues.FindDictionaryValue("NTHEFTCAPITAL_MultiRisk_Caption"))
            End If

            ''Informacion particular robo - Número de vigilantes
            'If NSECURITYMEN = eRemoteDB.Constants.intNull Then
            '    lclsErrors.ErrorMessage("MU700", 55665, , eFunctions.Errors.TextAlign.LeftAling, resxValues.FindDictionaryValue("NSECURITYMEN_MultiRisk_Caption"))
            'End If

            ''Informacion particular robo - Area de vigilancia (metros cuadrados)
            'If NAREA = eRemoteDB.Constants.intNull Then
            '    lclsErrors.ErrorMessage("MU700", 55665, , eFunctions.Errors.TextAlign.LeftAling, resxValues.FindDictionaryValue("NAREA_MultiRisk_Caption"))
            'End If
        End If

        If sRequieredSections.Split(",").Contains("3") Then ' 3 - Equipo electrónico
            If SIND_ELECTRONIC = "2" Then
                lclsErrors.ErrorMessage("MU700", 55665, , eFunctions.Errors.TextAlign.LeftAling, resxValues.FindDictionaryValue("EquipElect_Title"))
            End If
        End If

        If sRequieredSections.Split(",").Contains("4") Then ' 4 - Rotura de maquinaria
            If SIND_MACHINE = "2" Then
                lclsErrors.ErrorMessage("MU700", 55665, , eFunctions.Errors.TextAlign.LeftAling, resxValues.FindDictionaryValue("RotMaqui_Title"))
            End If
        End If

        If sRequieredSections.Split(",").Contains("5") Then ' 5 - Equipo y maquina de contrantistas
            If SIND_CONTRACTOR = "2" Then
                lclsErrors.ErrorMessage("MU700", 55665, , eFunctions.Errors.TextAlign.LeftAling, resxValues.FindDictionaryValue("EquipMaquiContr_Title"))
            End If
        End If

        If sRequieredSections.Split(",").Contains("6") Then ' 6 - Fidelidad privada


            'Fidelidad privada - Tipo de póliza (Debe estar lleno)
            If NFI_POLICYTYPE = eRemoteDB.Constants.intNull Then
                lclsErrors.ErrorMessage("MU700", 55665, , eFunctions.Errors.TextAlign.LeftAling, resxValues.FindDictionaryValue("NFI_POLICYTYPE_Fidelity_Caption"))
            End If

            If SIND_FIDELITY = "2" And NFI_POLICYTYPE = 2 Then 'Si el tipo de poliza es Blanket se valida que tenga registros el grid
                lclsErrors.ErrorMessage("MU700", 55665, , eFunctions.Errors.TextAlign.LeftAling, resxValues.FindDictionaryValue("Fidelity_Title"))
            End If

            'Fidelidad privada - Tipo de seguro (Debe estar lleno)
            If NINSURTYPE = eRemoteDB.Constants.intNull Then
                lclsErrors.ErrorMessage("MU700", 55665, , eFunctions.Errors.TextAlign.LeftAling, resxValues.FindDictionaryValue("NINSURTYPE_Fidelity_Caption"))
            End If
        End If

        If sRequieredSections.Split(",").Contains("7") Then 'Dinero y valores

            If NMONEY_TRANSIT = eRemoteDB.Constants.intNull Then
                lclsErrors.ErrorMessage("MU700", 55665, , eFunctions.Errors.TextAlign.LeftAling, resxValues.FindDictionaryValue("NMONEYTRANSIT_MoneyValues_Caption"))
            End If

            If NMONEY_PERMANENCE = eRemoteDB.Constants.intNull Then
                lclsErrors.ErrorMessage("MU700", 55665, , eFunctions.Errors.TextAlign.LeftAling, resxValues.FindDictionaryValue("NMONEYPERMANENCE_MoneyValues_Caption"))
            End If

        End If


        result = lclsErrors.Confirm

        Return result

    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <remarks></remarks>
    Public Function insValMU700Upd_EquipElect(ByVal sCodispl As String, ByVal nType As Integer, ByVal nSection As Integer, ByVal sDescript As String, ByVal nValorAsegurado As Double, ByVal nRate As Double, ByVal nPremium As Double) As String

        Dim result As String = String.Empty

        If nType = eRemoteDB.Constants.intNull OrElse nType = 0 Then
            lclsErrors.ErrorMessage(sCodispl, 55665, , eFunctions.Errors.TextAlign.LeftAling, resxValues.FindDictionaryValue("NTYPE_EquipElect_Caption"))
        End If

        If nSection = eRemoteDB.Constants.intNull OrElse nSection = 0 Then
            lclsErrors.ErrorMessage(sCodispl, 55665, , eFunctions.Errors.TextAlign.LeftAling, resxValues.FindDictionaryValue("NSECTION_EquipElect_Caption"))
        End If

        If String.IsNullOrEmpty(sDescript) Then
            lclsErrors.ErrorMessage(sCodispl, 55665, , eFunctions.Errors.TextAlign.LeftAling, resxValues.FindDictionaryValue("SDESCRIPTION_EquipElect_Caption"))
        End If

        If nValorAsegurado = eRemoteDB.Constants.intNull OrElse nValorAsegurado = 0 Then
            lclsErrors.ErrorMessage(sCodispl, 55665, , eFunctions.Errors.TextAlign.LeftAling, resxValues.FindDictionaryValue("NCAPITAL_EquipElect_Caption"))
        End If
        If nRate = eRemoteDB.Constants.intNull OrElse nValorAsegurado = 0 Then
            lclsErrors.ErrorMessage(sCodispl, 55665, , eFunctions.Errors.TextAlign.LeftAling, resxValues.FindDictionaryValue("NRATE_EquipElect_Caption"))
        End If
        If nPremium = eRemoteDB.Constants.intNull OrElse nValorAsegurado = 0 Then
            lclsErrors.ErrorMessage(sCodispl, 55665, , eFunctions.Errors.TextAlign.LeftAling, resxValues.FindDictionaryValue("NPREMIUM_EquipElect_Caption"))
        End If

        result = lclsErrors.Confirm

        Return result

    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <remarks></remarks>
    Public Function insPostMU700Upd(ByVal sAction As String,
                                    ByVal sGridName As String,
                                    ByVal sCerType As String,
                                    ByVal nBranch As Integer,
                                    ByVal nProduct As Integer,
                                    ByVal nPolicy As Long,
                                    ByVal nCertif As Long,
                                    ByVal nType As Integer,
                                    ByVal dEffecdate As Date,
                                    ByVal nUserCode As Integer,
                                    Optional ByVal nConsec As Integer = eRemoteDB.Constants.intNull, Optional ByVal nElement_Type As Integer = eRemoteDB.Constants.intNull,
                                    Optional ByVal nSection As Integer = eRemoteDB.Constants.intNull,
                                    Optional ByVal sDescription As String = "",
                                    Optional ByVal nCapital As Double = eRemoteDB.Constants.intNull,
                                    Optional ByVal sTradeMark As String = "",
                                    Optional ByVal sModel As String = "",
                                    Optional ByVal nYear As Integer = eRemoteDB.Constants.intNull,
                                    Optional ByVal sOrigin As String = "",
                                    Optional ByVal sSerialNumber As String = "",
                                    Optional ByVal nRate As Double = eRemoteDB.Constants.intNull,
                                    Optional ByVal nPremium As Double = eRemoteDB.Constants.intNull,
                                    Optional ByVal sClient As String = "",
                                    Optional ByVal sDigit As String = "",
                                    Optional ByVal sFirstName As String = "",
                                    Optional ByVal sMiddel_Name As String = "",
                                    Optional ByVal sLastName As String = "",
                                    Optional ByVal sLastName2 As String = "",
                                    Optional ByVal nPosition As Integer = eRemoteDB.Constants.intNull,
                                    Optional ByVal nSalary As Double = eRemoteDB.Constants.intNull,
                                    Optional ByVal nFactor As Double = eRemoteDB.Constants.intNull,
                                    Optional ByVal nValue As Double = eRemoteDB.Constants.intNull) As Boolean

        Dim lblnPost As Boolean = False


        Dim lrecinsPostMU7000_EquipElect As New eRemoteDB.Execute




        Try

            With lrecinsPostMU7000_EquipElect
                .StoredProcedure = "insPostMU700Upd"

                .Parameters.Add("sAction", sAction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 10, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("sGridName", sGridName, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 10, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

                .Parameters.Add("nConsec", nConsec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 5, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

                .Parameters.Add("sCertype", sCerType, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 5, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 5, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nType", nType, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 5, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("NELEMENT_TYPE", nElement_Type, eRmtDataDir.rdbParamInput, eRmtDataType.rdbNumeric, 5, 0, 5, eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("NSECTION", nSection, eRmtDataDir.rdbParamInput, eRmtDataType.rdbNumeric, 5, 0, 5, eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("SDESCRIPTION", sDescription, eRmtDataDir.rdbParamInput, eRmtDataType.rdbCharFixedLength, 30, 0, 0, eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("NCAPITAL", nCapital, eRmtDataDir.rdbParamInput, eRmtDataType.rdbNumeric, 18, 2, 18, eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nUsercode", nUserCode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 5, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

                .Parameters.Add("sTradeMark", sTradeMark, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 30, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("sModel", sModel, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 30, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nYear", nYear, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 4, 0, 4, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("sOrigin", sOrigin, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 30, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("sSerialNumber", sSerialNumber, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 30, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nRate", nRate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 15, 0, 6, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nPremium", nPremium, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

                .Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("sDigit", sDigit, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("sFirstName", sFirstName, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 63, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("sMiddleName", sMiddel_Name, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 63, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("sLastName", sLastName, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 63, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("sLastName2", sLastName2, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 63, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nPosition", nPosition, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 5, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nSalary", nSalary, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nFactor", nFactor, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 9, 0, 3, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nValue", nValue, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

                Return .Run(False)


            End With

        Catch ex As Exception
            Return False

        End Try

    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <remarks></remarks>
    Public Function insValMU700Upd_RotMaqui(ByVal sCodispl As String,
                                             ByVal sTradeMark As String,
                                             ByVal sModel As String,
                                             ByVal nYear As Long,
                                             ByVal sOrigin As String,
                                             ByVal sSerialNumber As String,
                                             ByVal nCapital As Double,
                                             ByVal nRate As Double,
                                             ByVal nPremium As Double) As String


        Dim result As String = String.Empty


        If String.IsNullOrEmpty(sTradeMark) Then
            lclsErrors.ErrorMessage(sCodispl, 55665, , eFunctions.Errors.TextAlign.LeftAling, resxValues.FindDictionaryValue("STRADEMARK_RotMaqui_Caption"))
        End If

        If String.IsNullOrEmpty(sModel) Then
            lclsErrors.ErrorMessage(sCodispl, 55665, , eFunctions.Errors.TextAlign.LeftAling, resxValues.FindDictionaryValue("SMODEL_RotMaqui_Caption"))
        End If

        If nYear = eRemoteDB.Constants.intNull OrElse nYear = 0 Then
            lclsErrors.ErrorMessage(sCodispl, 55665, , eFunctions.Errors.TextAlign.LeftAling, resxValues.FindDictionaryValue("NYEAR_RotMaqui_Caption"))
        End If

        If String.IsNullOrEmpty(sOrigin) Then
            lclsErrors.ErrorMessage(sCodispl, 55665, , eFunctions.Errors.TextAlign.LeftAling, resxValues.FindDictionaryValue("SORIGIN_RotMaqui_Caption"))
        End If

        If String.IsNullOrEmpty(sSerialNumber) Then
            lclsErrors.ErrorMessage(sCodispl, 55665, , eFunctions.Errors.TextAlign.LeftAling, resxValues.FindDictionaryValue("SSERIALNUMBER_RotMaqui_Caption"))
        End If

        If nCapital = eRemoteDB.Constants.intNull OrElse nCapital = 0 Then
            lclsErrors.ErrorMessage(sCodispl, 55665, , eFunctions.Errors.TextAlign.LeftAling, resxValues.FindDictionaryValue("NCAPITAL_RotMaqui_Caption"))
        End If


        If nRate = eRemoteDB.Constants.intNull OrElse nRate = 0 Then
            lclsErrors.ErrorMessage(sCodispl, 55665, , eFunctions.Errors.TextAlign.LeftAling, resxValues.FindDictionaryValue("NRATE_RotMaqui_Caption"))
        End If

        If nPremium = eRemoteDB.Constants.intNull OrElse nPremium = 0 Then
            lclsErrors.ErrorMessage(sCodispl, 55665, , eFunctions.Errors.TextAlign.LeftAling, resxValues.FindDictionaryValue("NPREMIUM_RotMaqui_Caption"))
        End If

        result = lclsErrors.Confirm
        Return result

    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <remarks></remarks>
    Public Function insValMU700Upd_EquipMaquiContr(ByVal sCodispl As String,
                                                     ByVal sTradeMark As String,
                                                     ByVal sModel As String,
                                                     ByVal nYear As Long,
                                                     ByVal sOrigin As String,
                                                     ByVal sSerialNumber As String,
                                                     ByVal nCapital As Double,
                                                     ByVal nRate As Double,
                                                     ByVal nPremium As Double) As String

        Dim result As String = String.Empty


        If String.IsNullOrEmpty(sTradeMark) Then
            lclsErrors.ErrorMessage(sCodispl, 55665, , eFunctions.Errors.TextAlign.LeftAling, resxValues.FindDictionaryValue("STRADEMARK_EquipMaquiContr_Caption"))
        End If

        If String.IsNullOrEmpty(sModel) Then
            lclsErrors.ErrorMessage(sCodispl, 55665, , eFunctions.Errors.TextAlign.LeftAling, resxValues.FindDictionaryValue("SMODEL_EquipMaquiContr_Caption"))
        End If

        If nYear = eRemoteDB.Constants.intNull OrElse nYear = 0 Then
            lclsErrors.ErrorMessage(sCodispl, 55665, , eFunctions.Errors.TextAlign.LeftAling, resxValues.FindDictionaryValue("NYEAR_EquipMaquiContr_Caption"))
        End If

        If String.IsNullOrEmpty(sOrigin) Then
            lclsErrors.ErrorMessage(sCodispl, 55665, , eFunctions.Errors.TextAlign.LeftAling, resxValues.FindDictionaryValue("SORIGIN_EquipMaquiContr_Caption"))
        End If

        If String.IsNullOrEmpty(sSerialNumber) Then
            lclsErrors.ErrorMessage(sCodispl, 55665, , eFunctions.Errors.TextAlign.LeftAling, resxValues.FindDictionaryValue("SSERIALNUMBER_EquipMaquiContr_Caption"))
        End If

        If nCapital = eRemoteDB.Constants.intNull OrElse nCapital = 0 Then
            lclsErrors.ErrorMessage(sCodispl, 55665, , eFunctions.Errors.TextAlign.LeftAling, resxValues.FindDictionaryValue("NCAPITAL_EquipMaquiContr_Caption"))
        End If


        If nRate = eRemoteDB.Constants.intNull OrElse nRate = 0 Then
            lclsErrors.ErrorMessage(sCodispl, 55665, , eFunctions.Errors.TextAlign.LeftAling, resxValues.FindDictionaryValue("NRATE_EquipMaquiContr_Caption"))
        End If

        If nPremium = eRemoteDB.Constants.intNull OrElse nPremium = 0 Then
            lclsErrors.ErrorMessage(sCodispl, 55665, , eFunctions.Errors.TextAlign.LeftAling, resxValues.FindDictionaryValue("NPREMIUM_EquipMaquiContr_Caption"))
        End If

        result = lclsErrors.Confirm
        Return result

    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <remarks></remarks>
    Public Function insValMU700Upd_Fidelity(ByVal sCodispl As String,
                                       ByVal sClient_Fidelity As String,
                                       ByVal SCLIENT_Fidelity_Digit As String,
                                       ByVal SFIRSTNAME_Fidelity As String,
                                       ByVal SMIDDLENAME_Fidelity As String,
                                       ByVal SLASTNAME_Fidelity As String,
                                       ByVal SLASTNAME2_Fidelity As String,
                                       ByVal nPosition As Long,
                                       ByVal nSalary As Double,
                                       ByVal nFactor As Double,
                                       ByVal nValue As Double,
                                       ByVal nFI_POLICYTYPE As Double) As String

        Dim result As String = String.Empty
        Dim lstrCLiDocuments As New eClient.CliDocuments
        Dim sValidateFormat As String
        sValidateFormat = "0"


        If String.IsNullOrEmpty(sClient_Fidelity) Then
            lclsErrors.ErrorMessage(sCodispl, 55665, , eFunctions.Errors.TextAlign.LeftAling, resxValues.FindDictionaryValue("SCLIENT_Fidelity_Caption"))
        End If

        If String.IsNullOrEmpty(SCLIENT_Fidelity_Digit) Then
            lclsErrors.ErrorMessage(sCodispl, 55665, , eFunctions.Errors.TextAlign.LeftAling, resxValues.FindDictionaryValue("SCLIENT_Fidelity_Digit_Caption"))
        End If

        If nFI_POLICYTYPE <> 1 Then
            If String.IsNullOrEmpty(SFIRSTNAME_Fidelity) Then
                lclsErrors.ErrorMessage(sCodispl, 55665, , eFunctions.Errors.TextAlign.LeftAling, resxValues.FindDictionaryValue("SFIRSTNAME_Fidelity_Caption"))
            End If


            If String.IsNullOrEmpty(SLASTNAME_Fidelity) Then
                lclsErrors.ErrorMessage(sCodispl, 55665, , eFunctions.Errors.TextAlign.LeftAling, resxValues.FindDictionaryValue("SLASTNAME_Fidelity_Caption"))
            End If

           

            If nPosition = eRemoteDB.Constants.intNull OrElse nPosition = 0 Then
                lclsErrors.ErrorMessage(sCodispl, 55665, , eFunctions.Errors.TextAlign.LeftAling, resxValues.FindDictionaryValue("NPOSITION_Fidelity_Caption"))
            End If

            If nSalary = eRemoteDB.Constants.intNull OrElse nSalary = 0 Then
                lclsErrors.ErrorMessage(sCodispl, 55665, , eFunctions.Errors.TextAlign.LeftAling, resxValues.FindDictionaryValue("NSALARY_Fidelity_Caption"))
            End If

            If nFactor = eRemoteDB.Constants.intNull OrElse nFactor = 0 Then
                lclsErrors.ErrorMessage(sCodispl, 55665, , eFunctions.Errors.TextAlign.LeftAling, resxValues.FindDictionaryValue("NFACTOR_Fidelity_Caption"))
            End If

            If nValue = eRemoteDB.Constants.intNull Then
                lclsErrors.ErrorMessage(sCodispl, 55665, , eFunctions.Errors.TextAlign.LeftAling, resxValues.FindDictionaryValue("NVALUE_Fidelity_Caption"))
            End If
        End If


        result = lclsErrors.Confirm
        Return result

    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <remarks></remarks>
    Public Function insPostMU700(ByVal SCERTYPE As String, ByVal NPRODUCT As Long, ByVal NBRANCH As Long,
                            ByVal NPOLICY As Long, ByVal NCERTIF As Long, ByVal DEFFECDATE As Date,
                            ByVal NCAPITAL As Double, ByVal DEXPIRDAT As Date, ByVal DISSUEDAT As Date,
                            ByVal NNULLCODE As Long, ByVal DNULLDATE As Date, ByVal NPREMIUM As Double,
                            ByVal DSTARTDATE As Date, ByVal NUSERCODE As Long, ByVal NTRANSACTIO As Long,
                            ByVal NSITUATION As Long, ByVal NGROUP As Long, ByVal SCLIENT As String,
                            ByVal NCONSTCAT As Long, ByVal NCODKIND As Long, ByVal NPAYFREQ As Long,
                            ByVal NSISMICZONE As Long, ByVal NFI_POLICYTYPE As Integer, ByVal NINSURTYPE As Integer,
                            ByVal NNUMBEROFEMPLOYEES As Long, ByVal NINSURED As Double, ByVal NTHEFTCAPITAL As Double,
                            ByVal NSECURITYMEN As Integer, ByVal NAREA As Double, ByVal SIND_FIDELITY As String,
                            ByVal SIND_ELECTRONIC As String, ByVal SIND_MACHINE As String, ByVal SIND_CONTRACTOR As String,
                            ByVal NMONEY_TRANSIT As Double, ByVal NMONEY_PERMANENCE As Double,
                            ByVal NARTICLE As Integer,
                            Optional ByVal SRISKDESCRIPTION As String = "") As Boolean

        Dim lblnPost As Boolean = False
        Dim lclsPolicyWin As ePolicy.Policy_Win



        Dim lrecinsPostMU7000 As New eRemoteDB.Execute

        Try

            With lrecinsPostMU7000
                .StoredProcedure = "insPostMU700"

                .Parameters.Add("sCertype", SCERTYPE, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nProduct", NPRODUCT, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 5, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nBranch", NBRANCH, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 5, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nPolicy", NPOLICY, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nCertif", NCERTIF, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("dEffecdate", DEFFECDATE, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("NCAPITAL", NCAPITAL, eRmtDataDir.rdbParamInput, eRmtDataType.rdbNumeric, 18, 2, 18, eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("dExpirDat", DEXPIRDAT, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("dIssueDat", DISSUEDAT, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("NNULLCODE", NNULLCODE, eRmtDataDir.rdbParamInput, eRmtDataType.rdbNumeric, 5, 0, 5, eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("DNULLDATE", DNULLDATE, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("NPREMIUM", NPREMIUM, eRmtDataDir.rdbParamInput, eRmtDataType.rdbNumeric, 18, 2, 18, eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("dStartDate", DSTARTDATE, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nUsercode", NUSERCODE, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 5, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("NTRANSACTIO", NTRANSACTIO, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 5, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("NSITUATION", NSITUATION, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 5, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("NGROUP", NGROUP, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 5, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("sClient", SCLIENT, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("NCONSTCAT", NCONSTCAT, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 5, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("NCODKIND", NCODKIND, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 5, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("NPAYFREQ", NPAYFREQ, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 5, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("NSISMICZONE", NSISMICZONE, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 5, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("NFI_POLICYTYPE", NFI_POLICYTYPE, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 5, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("NINSURTYPE", NINSURTYPE, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 5, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("NNUMBEROFEMPLOYEES", NNUMBEROFEMPLOYEES, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 5, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("NINSURED", NINSURED, eRmtDataDir.rdbParamInput, eRmtDataType.rdbNumeric, 18, 2, 18, eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("NTHEFTCAPITAL", NTHEFTCAPITAL, eRmtDataDir.rdbParamInput, eRmtDataType.rdbNumeric, 18, 2, 18, eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("NSECURITYMEN", NSECURITYMEN, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 5, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("NAREA", NAREA, eRmtDataDir.rdbParamInput, eRmtDataType.rdbNumeric, 8, 2, 8, eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("SIND_FIDELITY", SIND_FIDELITY, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("SIND_ELECTRONIC", SIND_ELECTRONIC, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("SIND_MACHINE", SIND_MACHINE, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("SIND_CONTRACTOR", SIND_CONTRACTOR, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("NMONEY_TRANSIT", NMONEY_TRANSIT, eRmtDataDir.rdbParamInput, eRmtDataType.rdbNumeric, 18, 2, 18, eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("NMONEY_PERMANENCE", NMONEY_PERMANENCE, eRmtDataDir.rdbParamInput, eRmtDataType.rdbNumeric, 18, 2, 18, eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("NARTICLE", NARTICLE, eRmtDataDir.rdbParamInput, eRmtDataType.rdbNumeric, 18, 2, 18, eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("SRISKDESCRIPTION", SRISKDESCRIPTION, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 600, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)



                lblnPost = .Run(False)

            End With


            If lblnPost Then
                lclsPolicyWin = New ePolicy.Policy_Win
                With lclsPolicyWin
                    lblnPost = .Add_PolicyWin(SCERTYPE, NBRANCH, NPRODUCT, NPOLICY, NCERTIF, DEFFECDATE, NUSERCODE, "MU700", "2")
                    Call lclsPolicyWin.Add_PolicyWin(SCERTYPE, NBRANCH, NPRODUCT, NPOLICY, NCERTIF, DEFFECDATE, NUSERCODE, "CA014", "3")
                End With
            End If

            Return lblnPost

        Catch ex As Exception
            Return False

        End Try



    End Function

    Public Function ValMassiveCharge_MultiRisk_Det(ByVal sbFileinText As String, ByVal nType As Integer) As String

        Dim lines As String() = sbFileinText.Split("|")

        'Constantes de multi riesgo
        'se elimino Const IDX_NELEMENT_TYPE As Integer = 11 al no estar siendo usada
        'se elimino Const IDX_NPREMIUM As Integer = 10 al no estar siendo usada 
        Const IDX_NTYPE As Integer = 0
        Const IDX_NSECTION As Integer = 1
        Const IDX_SDESCRIPT As Integer = 2
        Const IDX_NCAPITAL As Integer = 3
        Const IDX_STRADEMARK As Integer = 4
        Const IDX_SMODEL As Integer = 5
        Const IDX_NYEAR As Integer = 6
        Const IDX_SORIGIN As Integer = 7
        Const IDX_SSERIAL As Integer = 8
        Const IDX_NRATE As Integer = 9



        'Constantes de Fidelidad
        'se elimino Const IDX_NVALUE As Integer = 9 al no esta siendo usada
        Const IDX_SCODIGO_EMPLEADO As Integer = 0
        Const IDX_SDIGIT As Integer = 1
        Const IDX_SFIRSTNAME As Integer = 2
        Const IDX_SMIDDLE_NAME As Integer = 3
        Const IDX_SLASTNAME As Integer = 4
        Const IDX_SLASTNAME2 As Integer = 5
        Const IDX_NPOSITION As Integer = 6
        Const IDX_NSALARY As Integer = 7
        Const IDX_NFACTOR As Integer = 8


        Dim idxCount As Integer = 0
        Dim mColClientFidel As New Microsoft.VisualBasic.Collection()
        Dim sIdClient As String = ""


        For Each line In lines
            idxCount += 1
            If line.Trim() = "" Then
                Exit For
            End If

            Dim fields As String() = line.Split(";")

            If nType <> 6 AndAlso (String.IsNullOrEmpty(fields(IDX_NTYPE)) OrElse Not IsNumeric(fields(IDX_NTYPE))) Then
                lclsErrors.ErrorMessage("MU700", 55665, , eFunctions.Errors.TextAlign.LeftAling, "(" & idxCount.ToString() & ") " & resxValues.FindDictionaryValue("NTYPE_EquipElect_Caption"))
            Else

                If nType = 6 OrElse (nType = Convert.ToInt32(fields(IDX_NTYPE))) Then

                    Select Case nType 'fields(IDX_NTYPE)
                        Case Is = 1   'Identificacion del riesgo     
                        Case Is = 2   'Información particular de robo
                        Case Is = 3   'Equipo electrónico            

                            If String.IsNullOrEmpty(fields(IDX_NSECTION)) OrElse Not IsNumeric(fields(IDX_NSECTION)) Then
                                lclsErrors.ErrorMessage("MU700", 55665, , eFunctions.Errors.TextAlign.LeftAling, "(" & idxCount.ToString() & ") " & resxValues.FindDictionaryValue("NSECTION_EquipElect_Caption"))
                            End If

                            If String.IsNullOrEmpty(fields(IDX_SDESCRIPT)) Then
                                lclsErrors.ErrorMessage("MU700", 55665, , eFunctions.Errors.TextAlign.LeftAling, resxValues.FindDictionaryValue("SDESCRIPTION_EquipElect_Caption"))
                            End If

                            If String.IsNullOrEmpty(fields(IDX_NCAPITAL)) OrElse Not IsNumeric(fields(IDX_NCAPITAL)) Then
                                lclsErrors.ErrorMessage("MU700", 55665, , eFunctions.Errors.TextAlign.LeftAling, "(" & idxCount.ToString() & ") " & resxValues.FindDictionaryValue("NCAPITAL_EquipElect_Caption"))
                            End If

                        Case Is = 4   'Rotura de maquinaria          

                            If String.IsNullOrEmpty(fields(IDX_STRADEMARK)) Then
                                lclsErrors.ErrorMessage("MU700", 55665, , eFunctions.Errors.TextAlign.LeftAling, "(" & idxCount.ToString() & ") " & resxValues.FindDictionaryValue("STRADEMARK_RotMaqui_Caption"))
                            End If

                            If String.IsNullOrEmpty(IDX_SMODEL) Then
                                lclsErrors.ErrorMessage("MU700", 55665, , eFunctions.Errors.TextAlign.LeftAling, "(" & idxCount.ToString() & ") " & resxValues.FindDictionaryValue("SMODEL_RotMaqui_Caption"))
                            End If

                            If String.IsNullOrEmpty(fields(IDX_NYEAR)) OrElse Not IsNumeric(fields(IDX_NYEAR)) Then
                                lclsErrors.ErrorMessage("MU700", 55665, , eFunctions.Errors.TextAlign.LeftAling, "(" & idxCount.ToString() & ") " & resxValues.FindDictionaryValue("NYEAR_RotMaqui_Caption"))
                            End If

                            If String.IsNullOrEmpty(IDX_SORIGIN) Then
                                lclsErrors.ErrorMessage("MU700", 55665, , eFunctions.Errors.TextAlign.LeftAling, "(" & idxCount.ToString() & ") " & resxValues.FindDictionaryValue("SORIGIN_RotMaqui_Caption"))
                            End If

                            If String.IsNullOrEmpty(IDX_SSERIAL) Then
                                lclsErrors.ErrorMessage("MU700", 55665, , eFunctions.Errors.TextAlign.LeftAling, "(" & idxCount.ToString() & ") " & resxValues.FindDictionaryValue("SSERIALNUMBER_RotMaqui_Caption"))
                            End If

                            If String.IsNullOrEmpty(fields(IDX_NCAPITAL)) OrElse Not IsNumeric(fields(IDX_NCAPITAL)) Then
                                lclsErrors.ErrorMessage("MU700", 55665, , eFunctions.Errors.TextAlign.LeftAling, "(" & idxCount.ToString() & ") " & resxValues.FindDictionaryValue("NCAPITAL_RotMaqui_Caption"))
                            End If

                            If String.IsNullOrEmpty(fields(IDX_NRATE)) OrElse Not IsNumeric(fields(IDX_NRATE)) Then
                                lclsErrors.ErrorMessage("MU700", 55665, , eFunctions.Errors.TextAlign.LeftAling, "(" & idxCount.ToString() & ") " & resxValues.FindDictionaryValue("NRATE_RotMaqui_Caption"))
                            End If

                        Case Is = 5   'Equipo maquinaria contratista 

                            If String.IsNullOrEmpty(fields(IDX_STRADEMARK)) Then
                                lclsErrors.ErrorMessage("MU700", 55665, , eFunctions.Errors.TextAlign.LeftAling, "(" & idxCount.ToString() & ") " & resxValues.FindDictionaryValue("STRADEMARK_EquipMaquiContr_Caption"))
                            End If

                            If String.IsNullOrEmpty(IDX_SMODEL) Then
                                lclsErrors.ErrorMessage("MU700", 55665, , eFunctions.Errors.TextAlign.LeftAling, "(" & idxCount.ToString() & ") " & resxValues.FindDictionaryValue("SMODEL_EquipMaquiContr_Caption"))
                            End If

                            If String.IsNullOrEmpty(fields(IDX_NYEAR)) OrElse Not IsNumeric(fields(IDX_NYEAR)) Then
                                lclsErrors.ErrorMessage("MU700", 55665, , eFunctions.Errors.TextAlign.LeftAling, "(" & idxCount.ToString() & ") " & resxValues.FindDictionaryValue("NYEAR_EquipMaquiContr_Caption"))
                            End If

                            If String.IsNullOrEmpty(IDX_SORIGIN) Then
                                lclsErrors.ErrorMessage("MU700", 55665, , eFunctions.Errors.TextAlign.LeftAling, "(" & idxCount.ToString() & ") " & resxValues.FindDictionaryValue("SORIGIN_EquipMaquiContr_Caption"))
                            End If

                            If String.IsNullOrEmpty(IDX_SSERIAL) Then
                                lclsErrors.ErrorMessage("MU700", 55665, , eFunctions.Errors.TextAlign.LeftAling, "(" & idxCount.ToString() & ") " & resxValues.FindDictionaryValue("SSERIALNUMBER_EquipMaquiContr_Caption"))
                            End If

                            If String.IsNullOrEmpty(fields(IDX_NCAPITAL)) OrElse Not IsNumeric(fields(IDX_NCAPITAL)) Then
                                lclsErrors.ErrorMessage("MU700", 55665, , eFunctions.Errors.TextAlign.LeftAling, "(" & idxCount.ToString() & ") " & resxValues.FindDictionaryValue("NCAPITAL_EquipMaquiContr_Caption"))
                            End If

                            If String.IsNullOrEmpty(fields(IDX_NRATE)) OrElse Not IsNumeric(fields(IDX_NRATE)) Then
                                lclsErrors.ErrorMessage("MU700", 55665, , eFunctions.Errors.TextAlign.LeftAling, "(" & idxCount.ToString() & ") " & resxValues.FindDictionaryValue("NRATE_EquipMaquiContr_Caption"))
                            End If

                        Case Is = 6   'Fidelidad privada 

                            If String.IsNullOrEmpty(fields(IDX_SCODIGO_EMPLEADO)) Then
                                lclsErrors.ErrorMessage("MU700", 55665, , eFunctions.Errors.TextAlign.LeftAling, "(" & idxCount.ToString() & ") " & resxValues.FindDictionaryValue("SCLIENT_Fidelity_Caption"))
                            Else
                                If Not mColClientFidel.Contains(fields(IDX_SCODIGO_EMPLEADO)) Then
                                    mColClientFidel.Add(sIdClient, fields(IDX_SCODIGO_EMPLEADO))
                                Else
                                    lclsErrors.ErrorMessage("MU700", 38010, , eFunctions.Errors.TextAlign.LeftAling, "(" & idxCount.ToString() & ") " & resxValues.FindDictionaryValue("SCLIENT_Fidelity_Caption"))
                                End If
                            End If

                            If String.IsNullOrEmpty(IDX_SDIGIT) Then
                                lclsErrors.ErrorMessage("MU700", 55665, , eFunctions.Errors.TextAlign.LeftAling, "(" & idxCount.ToString() & ") " & resxValues.FindDictionaryValue("SDIGIT_Fidelity_Caption"))
                            End If

                            If String.IsNullOrEmpty(fields(IDX_SFIRSTNAME)) OrElse Not IsNumeric(fields(IDX_NPOSITION)) Then
                                lclsErrors.ErrorMessage("MU700", 55665, , eFunctions.Errors.TextAlign.LeftAling, "(" & idxCount.ToString() & ") " & resxValues.FindDictionaryValue("SFIRSTNAME_Fidelity_Caption"))
                            End If

                            If String.IsNullOrEmpty(fields(IDX_SMIDDLE_NAME)) OrElse Not IsNumeric(fields(IDX_NSALARY)) Then
                                lclsErrors.ErrorMessage("MU700", 55665, , eFunctions.Errors.TextAlign.LeftAling, "(" & idxCount.ToString() & ") " & resxValues.FindDictionaryValue("SMIDDLENAME_Fidelity_Caption"))
                            End If

                            If String.IsNullOrEmpty(fields(IDX_SLASTNAME)) OrElse Not IsNumeric(fields(IDX_NFACTOR)) Then
                                lclsErrors.ErrorMessage("MU700", 55665, , eFunctions.Errors.TextAlign.LeftAling, "(" & idxCount.ToString() & ") " & resxValues.FindDictionaryValue("SLASTNAME_Fidelity_Caption"))
                            End If

                            If String.IsNullOrEmpty(IDX_SLASTNAME2) Then
                                lclsErrors.ErrorMessage("MU700", 55665, , eFunctions.Errors.TextAlign.LeftAling, "(" & idxCount.ToString() & ") " & resxValues.FindDictionaryValue("SLASTNAME2_Fidelity_Caption"))
                            End If

                            If String.IsNullOrEmpty(fields(IDX_NPOSITION)) OrElse Not IsNumeric(fields(IDX_NPOSITION)) Then
                                lclsErrors.ErrorMessage("MU700", 55665, , eFunctions.Errors.TextAlign.LeftAling, "(" & idxCount.ToString() & ") " & resxValues.FindDictionaryValue("NPOSITION_Fidelity_Caption"))
                            End If

                            If String.IsNullOrEmpty(fields(IDX_NSALARY)) OrElse Not IsNumeric(fields(IDX_NSALARY)) Then
                                lclsErrors.ErrorMessage("MU700", 55665, , eFunctions.Errors.TextAlign.LeftAling, "(" & idxCount.ToString() & ") " & resxValues.FindDictionaryValue("NSALARY_Fidelity_Caption"))
                            End If

                            If String.IsNullOrEmpty(fields(IDX_NFACTOR)) OrElse Not IsNumeric(fields(IDX_NFACTOR)) Then
                                lclsErrors.ErrorMessage("MU700", 55665, , eFunctions.Errors.TextAlign.LeftAling, "(" & idxCount.ToString() & ") " & resxValues.FindDictionaryValue("NFACTOR_Fidelity_Caption"))
                            End If

                            If String.IsNullOrEmpty(fields(IDX_NFACTOR)) OrElse Not IsNumeric(fields(IDX_NFACTOR)) Then
                                lclsErrors.ErrorMessage("MU700", 55665, , eFunctions.Errors.TextAlign.LeftAling, "(" & idxCount.ToString() & ") " & resxValues.FindDictionaryValue("NVALUE_Fidelity_Caption"))
                            End If

                        Case Is = 7  'Dinero y Valores              
                    End Select
                Else

                End If
            End If

        Next

        ValMassiveCharge_MultiRisk_Det = lclsErrors.Confirm

    End Function

End Class
