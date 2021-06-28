Option Strict Off
Option Explicit On
Public Class Aviat_marit

    Public scertype As String
    Public nbranch As Integer
    Public nproduct As Integer
    Public npolicy As Integer
    Public ncertif As Integer
    Public deffecdate As Date
    Public ngroup As Integer
    Public nsituation As Integer
    Public nparticularclas As Integer
    Public sname As String
    Public sbrand As String
    Public smodel As String
    Public sseries As String
    Public nyear As Integer
    Public sorigin As String
    Public sregistrationnumber As String
    Public scapacity As String
    Public ntakeoff_maxwei As Integer
    Public sairportbase As String
    Public sgeographical As String
    Public nuse As Integer
    Public snavigationcertificate As String
    Public nqualificationship As Integer
    Public sportdeparture As String
    Public sportarrival As String
    Public sdimensions As String
    Public saddicionaltext As String
    Public nseatnumber As Integer
    Public ncrewnumber As Integer
    Public npassengersnumber As Integer
    Public nnibranumber As Integer
    Public nusercode As Integer
    Public ncapital As Double

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
    Public Function insValAV001_SH010(ByVal sCodispl As String, ByVal nparticularclas As Integer, ByVal sbrand As String, ByVal smodel As String, ByVal nyear As String, ByVal sregistrationnumber As String, ByVal ncapital As Integer, ByVal saddicionaltext As String,
                                      Optional ByVal ntakeoff_maxwei As Integer = 0, Optional ByVal sgeographical As String = "", Optional ByVal nuse As Integer = 0, Optional ByVal nseatnumber As Integer = 0, Optional ByVal ncrewnumber As Integer = 0, Optional ByVal npassengersnumber As Integer = 0, Optional ByVal nnibranumber As Integer = 0,
                                      Optional ByVal sname As String = "", Optional ByVal sseries As String = "", Optional ByVal sorigin As String = "", Optional ByVal snavigationcertificate As String = "", Optional ByVal nqualificationship As Integer = 0, Optional ByVal sportdeparture As String = "", Optional ByVal sportarrival As String = "", Optional ByVal sdimensions As String = "") As String
        Dim lclsValtrans As New eRemoteDB.Execute
        Dim lclsErrors As eFunctions.Errors
        Dim lstrErrorAll As String = ""


        '+ Define all parameters for the stored procedures 'insudb.valCreditExist'. Generated on 21/07/2004 03:31:12 p.m.
        With lclsValtrans
            .StoredProcedure = "insValAV001_SH010"
            .Parameters.Add("sCodispl", sCodispl, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 50, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nparticularclas", nparticularclas, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sbrand", sbrand, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 50, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("smodel", smodel, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 50, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nyear", nyear, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sregistrationnumber", sregistrationnumber, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 50, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("ntakeoff_maxwei", ntakeoff_maxwei, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sgeographical", sgeographical, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 50, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nuse", nuse, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("ncapital", ncapital, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("saddicionaltext", saddicionaltext, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 500, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nseatnumber", nseatnumber, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("ncrewnumber", ncrewnumber, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("npassengersnumber", npassengersnumber, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nnibranumber", nnibranumber, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sname", sname, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 50, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sseries", sseries, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 50, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sorigin", sorigin, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 50, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("snavigationcertificate", snavigationcertificate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 50, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nqualificationship", nqualificationship, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sportdeparture", sportdeparture, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 50, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sportarrival", sportarrival, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 50, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sdimensions", sdimensions, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 50, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("arrayerrors", " ", eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 4000, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            If .Run(False) Then
                lstrErrorAll = .Parameters("arrayerrors").Value
            End If

            lclsErrors = New eFunctions.Errors

            With lclsErrors
                If Len(lstrErrorAll) > 0 Then
                    If sCodispl = "AV001" Then
                        Call .ErrorMessage("AV001", , , , , , lstrErrorAll)
                    Else
                        Call .ErrorMessage("SH010", , , , , , lstrErrorAll)
                    End If
                End If
                insValAV001_SH010 = .Confirm
            End With
        End With


insvalCA004_Err:
        If Err.Number Then
            insValAV001_SH010 = "insValAV001_SH010: " & Err.Description
        End If
        On Error GoTo 0
        lclsErrors = Nothing
        lclsValtrans = Nothing
    End Function

    Public Function InsPostAV001_SH010(ByVal sCodispl As String, ByVal sCertype As String, ByVal nBranch As Short, ByVal nProduct As Short, ByVal nPolicy As Integer, ByVal nCertif As Integer, ByVal dEffecdate As Date, ByVal nGroup As Integer, ByVal nSituation As Integer, ByVal nparticularclas As Integer,
                                 ByVal sbrand As String, ByVal smodel As String, ByVal nyear As Integer, ByVal sregistrationnumber As String, ByVal ncapital As Double, ByVal saddicionaltext As String, ByVal nUsercode As Integer,
                                 Optional ByVal ntakeoff_maxwei As Integer = 0, Optional ByVal sairportbase As String = "", Optional ByVal sgeographical As String = "", Optional ByVal nuse As Integer = 0, Optional ByVal nseatnumber As Integer = 0, Optional ByVal ncrewnumber As Integer = 0, Optional ByVal npassengersnumber As Integer = 0, Optional ByVal nnibranumber As Integer = 0,
                                 Optional ByVal sname As String = "", Optional ByVal sseries As String = "", Optional ByVal sorigin As String = "", Optional ByVal snavigationcertificate As String = "", Optional ByVal nqualificationship As Integer = 0, Optional ByVal sportdeparture As String = "", Optional ByVal sportarrival As String = "", Optional ByVal sdimensions As String = "") As Boolean

        Dim lclsPolicyWin As ePolicy.Policy_Win

        InsPostAV001_SH010 = Update(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate, nGroup, nSituation, nparticularclas, sbrand, smodel, sseries, nyear, sorigin, sregistrationnumber, ntakeoff_maxwei, sairportbase, sgeographical, nuse, ncapital, saddicionaltext, nseatnumber, ncrewnumber, npassengersnumber, nnibranumber, sname, snavigationcertificate, nqualificationship, sportdeparture, sportarrival, sdimensions, nUsercode)

        If InsPostAV001_SH010 Then
            lclsPolicyWin = New ePolicy.Policy_Win
            If sCodispl = "AV001" Then
                Call lclsPolicyWin.Add_PolicyWin(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate, nUsercode, "AV001", "2")
            Else
                Call lclsPolicyWin.Add_PolicyWin(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate, nUsercode, "SH010", "2")
            End If
            lclsPolicyWin = Nothing
        End If

    End Function

    Private Function Update(ByVal sCertype As String, ByVal nBranch As Short, ByVal nProduct As Short, ByVal nPolicy As Integer, ByVal nCertif As Integer, ByVal dEffecdate As Date, ByVal nGroup As Integer, ByVal nSituation As Integer, ByVal nparticularclas As Integer, ByVal sbrand As String, ByVal smodel As String, ByVal sSeries As String, ByVal nyear As Integer, ByVal sOrigin As String, ByVal sregistrationnumber As String, ByVal ntakeoff_maxwei As Integer, ByVal sAirportbase As String, ByVal sgeographical As String, ByVal nuse As Integer, ByVal ncapital As Double, ByVal saddicionaltext As String, ByVal nseatnumber As Integer, ByVal ncrewnumber As Integer, ByVal npassengersnumber As Integer, ByVal nnibranumber As Integer, ByVal sname As String, ByVal snavigationcertificate As String, ByVal nqualificationship As Integer, ByVal sportdeparture As String, ByVal sportarrival As String, ByVal sdimensions As String, ByVal nUsercode As Integer) As Boolean
        Dim lclsAviat_marit As eRemoteDB.Execute

        lclsAviat_marit = New eRemoteDB.Execute

        '+ Define all parameters for the stored procedures 'insudb.updCredit'. Generated on 21/07/2004 03:31:12 p.m.
        With lclsAviat_marit

            .StoredProcedure = "insUpdAviat_marit"
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nGroup", nGroup, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nSituation", nSituation, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nparticularclas", nparticularclas, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sbrand", sbrand, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 50, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("smodel", smodel, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 50, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sSeries", sSeries, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 50, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nyear", nyear, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 50, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sOrigin", sOrigin, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 50, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sregistrationnumber", sregistrationnumber, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 50, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("ntakeoff_maxwei", ntakeoff_maxwei, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sAirportbase", sAirportbase, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 50, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sgeographical", sgeographical, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 50, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nuse", nuse, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("ncapital", ncapital, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("saddicionaltext", saddicionaltext, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 500, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nseatnumber", nseatnumber, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("ncrewnumber", ncrewnumber, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("npassengersnumber", npassengersnumber, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nnibranumber", nnibranumber, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sname", sname, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 50, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("snavigationcertificate", snavigationcertificate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 50, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nqualificationship", nqualificationship, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sportdeparture", sportdeparture, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 50, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sportarrival", sportarrival, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 50, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sdimensions", sdimensions, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 50, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            Update = .Run(False)
        End With

insvalCA004_Err:
        If Err.Number Then
            Update = False
        End If
        On Error GoTo 0
        lclsAviat_marit = Nothing

    End Function

End Class
