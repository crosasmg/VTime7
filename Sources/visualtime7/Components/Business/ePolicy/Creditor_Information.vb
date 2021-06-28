Option Strict Off
Option Explicit On
'UPGRADE_NOTE: Property was upgraded to Property_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
<System.Runtime.InteropServices.ProgId("Property_Renamed_NET.Property_Renamed")> Public Class Creditor_information
    '%-------------------------------------------------------%'
    '% $Workfile:: Creditor_information.cls                             $%'
    '% $Author:: Nvaplat41                                  $%'
    '% $Date:: 9/10/03 19.01                                $%'
    '% $Revision:: 33                                       $%'
    '%-------------------------------------------------------%'

    '**- Properties according to the table in the system on November 06,2000
    '- Propiedades según la tabla en el sistema al 06/11/2000
    '**- The key fields of the table corresponds to: sCertype, nBranch, nProduct, nPolicy, nCertif, nId, dEffecdate
    '- Los campos llave de la tabla corresponden a: sCertype, nBranch, nProduct, nPolicy, nCertif, nId, dEffecdate

    '- Column_name                     Type    Computed    Length   Prec  Scale Nullable      TrimTrailingBlanks                  FixedLenNullInSource
    '- -------------------------------------------------------------------------------------------------------------------------------- ------------
    Public sCertype As String 'char       no          1                   no                no                                  no
    Public nBranch As Integer 'smallint   no          2       5     0     no                (n/a)                               (n/a)
    Public nProduct As Integer 'smallint   no          2       5     0     no                (n/a)                               (n/a)
    Public nPolicy As Double 'int        no          4      10     0     no                (n/a)                               (n/a)
    Public nCertif As Double 'int        no          4      10     0     no                (n/a)                               (n/a)
    Public dEffecdate As Date 'datetime   no          8                   no                (n/a)                               (n/a)
    Public dNulldate As Date 'datetime   no          8                   yes               (n/a)                               (n/a)
    Public nUsercode As Integer 'smallint   no          2       5     0     yes               (n/a)                               (n/a)
    Public nModulec As Integer
    Public nCover As Integer
    Public nTransaction As Integer
    Public nConsecutive As Integer
    Public ndetail_item As Integer
    Public nEndorsementvalue As Double
    Public nCurrency As Integer
    Public sContent As String
    Public nType As Integer
    Public dEffecendorsementdate As Date
    Public dExpirendorsementdate As Date

    '**% Add: insert an insuranced good in the policy.
    '% Add: Inserta un bien asegurado dentro de la póliza
    Public Function Add() As Boolean
        Dim lreccreProperty As eRemoteDB.Execute

        lreccreProperty = New eRemoteDB.Execute

        On Error GoTo Add_err

        '**+ Parameter definition for stored procedure 'insudb.creCreditor_information'
        '+ Definición de parámetros para stored procedure 'insudb.creCreditor_information'
        With lreccreProperty
            .StoredProcedure = "creCreditor_information"
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCurrency", nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nModulec", nModulec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCover", nCover, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nTransaction", nTransaction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nConsecutive", nConsecutive, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("ndetail_item", ndetail_item, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nType", nType, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nEndorsementvalue", nEndorsementvalue, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            Add = .Run(False)
        End With

Add_err:
        If Err.Number Then
            Add = False
        End If
        'UPGRADE_NOTE: Object lreccreProperty may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lreccreProperty = Nothing
    End Function

    '**% Delete: Deletes an insuranced good in the policy.
    '% Delete: Elimina un bien asegurado dentro de la póliza
    '-----------------------------------------------------------
    Public Function Delete() As Boolean
        '-----------------------------------------------------------
        Dim lrecdelCreditor_information As eRemoteDB.Execute

        lrecdelCreditor_information = New eRemoteDB.Execute

        On Error GoTo Delete_err

        '**+ Parameter definition for stored procedure 'insudb.delProperty'
        '+ Definición de parámetros para stored procedure 'insudb.delProperty'
        '**+ Information read on November 08, 2000   09:44:52 a.m.
        '+ Información leída el 08/11/2000 09:44:52 AM
        With lrecdelCreditor_information
            .StoredProcedure = "delCreditor_information"
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nTransaction", nTransaction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nConsecutive", nConsecutive, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            Delete = .Run(False)
        End With

Delete_err:
        If Err.Number Then
            Delete = False
        End If
        'UPGRADE_NOTE: Object lrecdelProperty may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecdelCreditor_information = Nothing
    End Function

    '*** FindPropertyID: Restores the correlative code assigned to a new good.
    '* FindPropertyID: Devuelve el código correlativo asignado al nuevo Bien
    Public ReadOnly Property FindCreditor_informationID(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, Optional ByVal lblnFind As Boolean = False) As Integer
        Get
            Dim lrecreaCreditor_information_ID As eRemoteDB.Execute

            lrecreaCreditor_information_ID = New eRemoteDB.Execute

            On Error GoTo FindPropertyID_Err

            '**+ Parameter definition for stored procedure 'insudb.reaProperty_ID'
            '+ Definición de parámetros para stored procedure 'insudb.reaProperty_ID'
            '**+ Information read on November08,2000  10:16:56 a.m.
            '+ Información leída el 08/11/2000 10:16:56 AM
            With lrecreaCreditor_information_ID
                .StoredProcedure = "reaCreditor_information_ID"
                .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

                If .Run Then
                    If (.FieldToClass("nId") = eRemoteDB.Constants.intNull) Then
                        FindCreditor_informationID = 1
                    Else
                        FindCreditor_informationID = .FieldToClass("nConsecutive") + 1
                    End If
                    .RCloseRec()
                End If
            End With

FindPropertyID_Err:
            If Err.Number Then
                FindCreditor_informationID = False
            End If
            'UPGRADE_NOTE: Object lrecreaProperty_ID may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
            FindCreditor_informationID = Nothing
        End Get
    End Property

    '**% Update: Updates the insuranced goods in a policy.
    '% Update: Actualiza los bienes asegurados dentro de la póliza
    '-------------------------------------------------------------
    Public Function Update() As Boolean
        '-------------------------------------------------------------
        Dim lrecinsCreditor_information As eRemoteDB.Execute

        lrecinsCreditor_information = New eRemoteDB.Execute

        On Error GoTo Update_Err

        '**+ Parameter definition for stored procedure 'insudb.insProperty'
        '+ Definición de parámetros para stored procedure 'insudb.insProperty'
        '**+ Information read on November 08,2000  10:07:20 a.m.
        '+ Información leída el 08/11/2000 10:07:20 AM
        With lrecinsCreditor_information
            .StoredProcedure = "insCreditor_information"
             .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCurrency", nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nModulec", nModulec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCover", nCover, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nTransaction", nTransaction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nConsecutive", nConsecutive, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("ndetail_item", ndetail_item, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nType", nType, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nEndorsementvalue", nEndorsementvalue, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            Update = .Run(False)
        End With

Update_Err:
        If Err.Number Then
            Update = False
        End If
        'UPGRADE_NOTE: Object lrecinsProperty may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecinsCreditor_information = Nothing
    End Function

    '% Find: Devuelve un registro de la tabla property
    Public Function Find(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date) As Boolean
        Dim lrecreaCreditor_information As eRemoteDB.Execute

        lrecreaCreditor_information = New eRemoteDB.Execute

        On Error GoTo Find_Err

        '**+ Parameter definition for stored procedure 'insudb.reaProperty_ID'
        '+ Definición de parámetros para stored procedure 'insudb.reaProperty_ID'
        '**+ Information read on November08,2000  10:16:56 a.m.
        '+ Información leída el 08/11/2000 10:16:56 AM
        With lrecreaCreditor_information
            .StoredProcedure = "reaCreditor_information_1"
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            If .Run Then
                Find = True
                Me.sCertype = .FieldToClass("sCertype")
                Me.nBranch = .FieldToClass("nBranch")
                Me.nProduct = .FieldToClass("nProduct")
                Me.nPolicy = .FieldToClass("nPolicy")
                Me.nCertif = .FieldToClass("nCertif")
                Me.dEffecdate = .FieldToClass("dEffecdate")
                Me.nUsercode = .FieldToClass("nUsercode")
                Me.nCurrency = .FieldToClass("nCurrency")
            Else
                Find = False
            End If
        End With

Find_Err:
        If Err.Number Then
            Find = False
        End If
        'UPGRADE_NOTE: Object lrecreaProperty may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecreaCreditor_information = Nothing
    End Function

    '    '% insValCA061: Valida la información general del bien a asegurar
    '    Public Function insValCA061(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, ByVal nUsercode As Integer, ByVal sAction As String, ByVal nCurrency As Integer, ByVal nModulec As Integer, ByVal nCover As Integer, ByVal nTransaction As Integer, ByVal nConsecutive As Integer, ByVal nDetail_item As Integer, ByVal nType As Integer, ByVal nEndorsementvalue As Double) As String
    '        Dim lobjErrors As eFunctions.Errors
    '        Dim lclsTab_goods As Tab_goods
    '        Dim lclsProperty As ePolicy.Property_Renamed
    '        Dim lcolPropertys As ePolicy.Propertys
    '        Dim lclsAuto As ePolicy.Automobile
    '        Dim lclsProf_ord As Object
    '        Dim intCount As Integer
    '        Dim nCapitalAseg As Double
    '        Dim nSumValorBienes As Double

    '        lobjErrors = New eFunctions.Errors
    '        lclsTab_goods = New Tab_goods
    '        lclsProperty = New ePolicy.Property_Renamed
    '        lcolPropertys = New ePolicy.Propertys
    '        lclsAuto = New ePolicy.Automobile

    '        lclsProf_ord = eRemoteDB.NetHelper.CreateClassInstance("eClaim.Prof_ord")

    '        insValCA061 = CStr(True)
    '        On Error GoTo insValCA061_Err


    '        '+ Moneda inválida
    '        If nCurrency <= 0 Then
    '            Call lobjErrors.ErrorMessage("CA061", 1351)
    '            insValCA061 = CStr(False)
    '        End If



    '        insValCA061 = lobjErrors.Confirm

    '        'UPGRADE_NOTE: Object lobjErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
    '        lobjErrors = Nothing
    '        'UPGRADE_NOTE: Object lclsTab_goods may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
    '        lclsTab_goods = Nothing
    '        'UPGRADE_NOTE: Object lclsProf_ord may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
    '        lclsProf_ord = Nothing

    'insValCA061_Err:
    '        If Err.Number Then
    '            insValCA061 = insValCA061 & Err.Description
    '        End If
    '        On Error GoTo 0

    '    End Function

    '% insPostCA061: Se realiza la actualización de los datos de los bienes asegurables y el
    '%               estado de la forma (PolicyWin)
    Public Function insPostCA061(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, ByVal nUsercode As Integer, ByVal sAction As String, ByVal nCurrency As Integer, ByVal nModulec As Integer, ByVal nCover As Integer, ByVal nTransaction As Integer, ByVal nConsecutive As Integer, ByVal nDetail_item As Integer, ByVal nType As Integer, ByVal nEndorsementvalue As Double) As Boolean
        Dim lclsPolicy_Win As ePolicy.Policy_Win
        Dim lclsCreditor_informations As Creditor_information

        On Error GoTo insPostCA061_Err
        Select Case nTransaction
            '+ Consulta de: póliza, certificados, cotización, solicitud
            Case 8, 9, 10, 11
            Case Else
                If insUpdTdbCA061(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate, nUsercode, sAction, nCurrency, nModulec, nCover, nTransaction, nConsecutive, nDetail_item, nType, nEndorsementvalue) Then
                    insPostCA061 = True
                    lclsCreditor_informations = New ePolicy.Creditor_information
                    If lclsCreditor_informations.Find(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate) Then
                        Me.sContent = "2"
                    Else
                        Me.sContent = "1"
                    End If
                    lclsPolicy_Win = New ePolicy.Policy_Win
                    Call lclsPolicy_Win.Add_PolicyWin(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate, nUsercode, "CA061", Me.sContent)
                    'Call lclsPolicy_Win.Add_PolicyWin(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate, nUsercode, "CA014", "3")
                    'If nBranch = 21 Then
                    '    Call lclsPolicy_Win.Add_PolicyWin(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate, nUsercode, "MU700", "3")
                    '    Call lclsPolicy_Win.Add_PolicyWin(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate, nUsercode, "CA009", "3")
                    'End If

                Else
                    insPostCA061 = False
                End If
        End Select

insPostCA061_Err:
        If Err.Number Then
            insPostCA061 = False
        End If
        lclsCreditor_informations = New ePolicy.Creditor_information
        'UPGRADE_NOTE: Object lclsPolicy_Win may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsPolicy_Win = Nothing
        On Error GoTo 0
    End Function

    '% insUpdTdbCA061: Actualiza los datos de los bienes asegurables
    Private Function insUpdTdbCA061(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, ByVal nUsercode As Integer, ByVal sAction As String, ByVal nCurrency As Integer, ByVal nModulec As Integer, ByVal nCover As Integer, ByVal nTransaction As Integer, ByVal nConsecutive As Integer, ByVal ndetail_item As Integer, ByVal nType As Integer, ByVal nEndorsementvalue As Double) As Boolean
        Dim lclsProdMaster As eProduct.Product

        With Me
            .sCertype = sCertype
            .nBranch = nBranch
            .nProduct = nProduct
            .nPolicy = nPolicy
            .nCertif = nCertif
            .dEffecdate = dEffecdate
            .nModulec = nModulec
            .nCover = nCover
            .nTransaction = nTransaction
            .nConsecutive = nConsecutive
            .ndetail_item = ndetail_item
            .nEndorsementvalue = nEndorsementvalue
            .nUsercode = nUsercode
            .nCurrency = nCurrency
            .nType = nType

            Select Case sAction

                '+ Registro nuevo
                Case "Add"

                    insUpdTdbCA061 = .Add

                    '+ Eliminación de registro
                Case "Del"

                    insUpdTdbCA061 = .Delete
                    '+ Actualización de registro
                Case "Update"

                    insUpdTdbCA061 = .Update
            End Select

        End With

        'UPGRADE_NOTE: Object lclsProdMaster may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsProdMaster = Nothing

    End Function

    '**% Update: Updates the insuranced goods in a policy.
    '% Update: Actualiza los bienes asegurados dentro de la póliza
    '-------------------------------------------------------------
    Public Function insPostCA061_k(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, ByVal nTransaction As Integer, ByVal dExpirendorsementdate As Date, ByVal dEffecendorsementdate As Date, ByVal nEndorsementValue As Double, ByVal sText As String, ByVal nUsercode As Integer) As Boolean

        '-------------------------------------------------------------
        Dim lrecinsCreditor_information As eRemoteDB.Execute
        Dim lclsCreditor_informations As New Creditor_information
        lrecinsCreditor_information = New eRemoteDB.Execute
        Dim lclsPolicy_Win As ePolicy.Policy_Win

        On Error GoTo InsPostCA061_k_Err

        '**+ Parameter definition for stored procedure 'insudb.insProperty'
        '+ Definición de parámetros para stored procedure 'insudb.insProperty'
        '**+ Information read on November 08,2000  10:07:20 a.m.
        '+ Información leída el 08/11/2000 10:07:20 AM
        With lrecinsCreditor_information
            .StoredProcedure = "insEndorsement_text"
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nTransaction", nTransaction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dExpirendorsementdate", dExpirendorsementdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecendorsementdate", dEffecendorsementdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nEndorsementValue", nEndorsementValue, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sText", sText, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 300, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            insPostCA061_k = .Run(False)

            If lclsCreditor_informations.Find(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate) Then
                Me.sContent = "2"
            Else
                Me.sContent = "1"
            End If
            lclsPolicy_Win = New ePolicy.Policy_Win
            Call lclsPolicy_Win.Add_PolicyWin(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate, nUsercode, "CA061", Me.sContent)
            '     Call lclsPolicy_Win.Add_PolicyWin(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate, nUsercode, "CA014", "3")
            'If nBranch = 21 Then
            '    Call lclsPolicy_Win.Add_PolicyWin(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate, nUsercode, "MU700", "3")
            '    Call lclsPolicy_Win.Add_PolicyWin(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate, nUsercode, "CA009", "3")
            'End If

        End With

InsPostCA061_k_Err:
        If Err.Number Then
            insPostCA061_k = False
        End If
        'UPGRADE_NOTE: Object lrecinsProperty may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecinsCreditor_information = Nothing
    End Function




    '% insValCA061: Valida la información general del bien a asegurar
    Public Function Getextvalue(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date) As String
        Dim lreccreProperty As eRemoteDB.Execute

        lreccreProperty = New eRemoteDB.Execute

        On Error GoTo Add_err

        '**+ Parameter definition for stored procedure 'insudb.creCreditor_information'
        '+ Definición de parámetros para stored procedure 'insudb.creCreditor_information'
        With lreccreProperty
            .StoredProcedure = "GetTextvalue_BenefAcree"
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sTextvalue", String.Empty, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 3000, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            .Run(False)
            Getextvalue = Trim(.Parameters("sTextvalue").Value)
        End With


Add_err:
        If Err.Number Then
            Getextvalue = String.Empty
        End If
        'UPGRADE_NOTE: Object lreccreProperty may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lreccreProperty = Nothing

    End Function


    '%InsValAU001: Realiza la validación de los campos a actualizar
    'en la ventana de datos particulares del automovil (AU001)
    Public Function insValCA061(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, ByVal nUsercode As Integer, ByVal sAction As String, ByVal nCurrency As Integer, ByVal nModulec As Integer, ByVal nCover As Integer, ByVal nTransaction As Integer, ByVal nConsecutive As Integer, ByVal nDetail_item As Integer, ByVal nType As Integer, ByVal nEndorsementvalue As Double, ByVal ngridID As Integer) As String
        Dim lclsErrors As eFunctions.Errors
        On Error GoTo InsValAU001_Err
        lclsErrors = New eFunctions.Errors
        Dim lstrErrors As String

        '+Validaciones que se realizan el la BD
        lstrErrors = insValCA061DB(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate, nUsercode, sAction, nCurrency, nModulec, nCover, nTransaction, nConsecutive, nDetail_item, nType, nEndorsementvalue, ngridID)

        Call lclsErrors.ErrorMessage("CA061", , , , , , lstrErrors)

        insValCA061 = lclsErrors.Confirm


InsValAU001_Err:
        If Err.Number Then
            insValCA061 = "insValCA061: " & Err.Description
        End If
        'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsErrors = Nothing
        On Error GoTo 0
    End Function


    '%InsValAU001DB: Llamado del procedure de la validación de los campos a actualizar en la
    '                ventana de datos particulares del automovil (AU001)
    Public Function insValCA061DB(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, ByVal nUsercode As Integer, ByVal sAction As String, ByVal nCurrency As Integer, ByVal nModulec As Integer, ByVal nCover As Integer, ByVal nTransaction As Integer, ByVal nConsecutive As Integer, ByVal nDetail_item As Integer, ByVal nType As Integer, ByVal nEndorsementvalue As Double, ByVal ngridID As Integer) As String
        Dim lrecinsValCA061 As eRemoteDB.Execute

        On Error GoTo insValCA061_Err

        lrecinsValCA061 = New eRemoteDB.Execute

        With lrecinsValCA061
            .StoredProcedure = "INSVALCA061"
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCurrency", nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nModulec", nModulec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCover", nCover, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nTransaction", nTransaction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nConsecutive", nConsecutive, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("ndetail_item", nDetail_item, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nType", nType, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nEndorsementvalue", nEndorsementvalue, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("ngridID", ngridID, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("Arrayerrors", String.Empty, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 4000, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If .Run(False) Then
                insValCA061DB = .Parameters("Arrayerrors").Value
            End If
        End With

insValCA061_Err:
        If Err.Number Then
            insValCA061DB = "InsValAU001DB: " & Err.Description
        End If
        'UPGRADE_NOTE: Object lrecInsValAU001 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecinsValCA061 = Nothing
        On Error GoTo 0
    End Function

    '% insValCA061: Valida la información general del bien a asegurar
    Public Function insValCA061_k(ByVal sText As String, ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, ByVal dEffecendorsementdate As Date, ByVal dExpirendorsementdate As Date) As String
        Dim lobjErrors As eFunctions.Errors
        Dim lclsCertificat As ePolicy.Certificat = New ePolicy.Certificat
        lobjErrors = New eFunctions.Errors

        insValCA061_k = CStr(True)
        On Error GoTo insValCA061_Err

        lclsCertificat.Find(sCertype, nBranch, nProduct, nPolicy, nCertif)
   

        '+ Si la fecha Fin 
        If dExpirendorsementdate = eRemoteDB.Constants.dtmNull Then
            Call lobjErrors.ErrorMessage("CA061", 60218)
        End If


        '+ Si la fecha Fin 
        If dEffecendorsementdate = eRemoteDB.Constants.dtmNull Then
            Call lobjErrors.ErrorMessage("CA061", 60217)
        End If

        '+ La fecha de endoso debe estar comprendida dentro de la vigencia de la póliza
        If dEffecendorsementdate < lclsCertificat.dDate_Origi Or dEffecendorsementdate > lclsCertificat.dExpirdat Then
            Call lobjErrors.ErrorMessage("CA061", 55541)
        End If

        '+ La fecha de endoso debe estar comprendida dentro de la vigencia de la póliza
        If dExpirendorsementdate < lclsCertificat.dDate_Origi Or dExpirendorsementdate > lclsCertificat.dExpirdat Then
            Call lobjErrors.ErrorMessage("CA061", 55541)
        End If


        '+ La fecha de endoso debe estar comprendida dentro de la vigencia de la póliza
        If dExpirendorsementdate < dEffecendorsementdate Then
            Call lobjErrors.ErrorMessage("CA061", 12120)
        End If



        '+ Moneda inválida
        If sText = String.Empty Then
            Call lobjErrors.ErrorMessage("CA061", 55665, , 1, "Texto a imprimir en el endoso")
            insValCA061_k = CStr(False)
        End If

        insValCA061_k = lobjErrors.Confirm

        'UPGRADE_NOTE: Object lobjErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lobjErrors = Nothing
insValCA061_Err:
        If Err.Number Then
            insValCA061_k = Err.Description
        End If
        On Error GoTo 0

    End Function

End Class






