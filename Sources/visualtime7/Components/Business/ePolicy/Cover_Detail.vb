Option Strict Off
Option Explicit On
'UPGRADE_NOTE: Property was upgraded to Cover_Detail. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
Public Class Cover_Detail
    '%-------------------------------------------------------%'
    '% $Workfile:: Property.cls                             $%'
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
    Public nCode_good As Integer 'smallint   no          2       5     0     yes               (n/a)                               (n/a)
    Public nCapital As Double 'decimal    no          9      12     0     yes               (n/a)                               (n/a)
    Public sDescript As String 'char       no        200                   yes               no                                  yes
    Public dNulldate As Date 'datetime   no          8                   yes               (n/a)                               (n/a)
    Public nPremium As Double 'decimal    no          9      10     2     yes               (n/a)                               (n/a)
    Public nUsercode As Integer 'smallint   no          2       5     0     yes               (n/a)                               (n/a)
    Public nRate As Double 'decimal    no          5       4     2     yes               (n/a)                               (n/a)
    Public nCurrency As Integer 'smallint   no          2       5     0     yes               (n/a)                               (n/a)
    '- Se define la variable para almacenar si la transacción esta con o sin contenido
    Public sContent As String
    Public nModulec As Integer
    Public ntype As Integer
    Public nCover As Integer

    '**% Add: insert an insuranced good in the policy.
    '% Add: Inserta un bien asegurado dentro de la póliza
    Public Function Add() As Boolean
        Dim lreccreProperty As eRemoteDB.Execute

        lreccreProperty = New eRemoteDB.Execute

        On Error GoTo Add_err

        '**+ Parameter definition for stored procedure 'insudb.creProperty'
        '+ Definición de parámetros para stored procedure 'insudb.creProperty'
        With lreccreProperty
            .StoredProcedure = "CRECOVER_DETAILS"
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nModulec", nModulec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 4, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCover", nCover, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 4, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nType", ntype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCode_good", nCode_good, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCapital", nCapital, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nRate", nRate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 4, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sDescript", sDescript, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 200, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCurrency", nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPremium", nPremium, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
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
    Public Function Delete(ByVal sAction As String) As Boolean
        '-----------------------------------------------------------
        Dim lrecdelProperty As eRemoteDB.Execute

        lrecdelProperty = New eRemoteDB.Execute

        On Error GoTo Delete_err

        '**+ Parameter definition for stored procedure 'insudb.delProperty'
        '+ Definición de parámetros para stored procedure 'insudb.delProperty'
        '**+ Information read on November 08, 2000   09:44:52 a.m.
        '+ Información leída el 08/11/2000 09:44:52 AM
        With lrecdelProperty
            .StoredProcedure = "DELCOVER_DETAILS"
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nModulec", nModulec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 4, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCover", nCover, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 4, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nType", ntype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCode_good", nCode_good, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sAction", sAction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            Delete = .Run(False)
        End With

Delete_err:
        If Err.Number Then
            Delete = False
        End If
        'UPGRADE_NOTE: Object lrecdelProperty may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecdelProperty = Nothing
    End Function

    '*** FindPropertyID: Restores the correlative code assigned to a new good.
    '* FindPropertyID: Devuelve el código correlativo asignado al nuevo Bien
    Public ReadOnly Property FindPropertyID(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, Optional ByVal lblnFind As Boolean = False) As Integer
        Get
            Dim lrecreaProperty_ID As eRemoteDB.Execute

            lrecreaProperty_ID = New eRemoteDB.Execute

            On Error GoTo FindPropertyID_Err

            '**+ Parameter definition for stored procedure 'insudb.reaProperty_ID'
            '+ Definición de parámetros para stored procedure 'insudb.reaProperty_ID'
            '**+ Information read on November08,2000  10:16:56 a.m.
            '+ Información leída el 08/11/2000 10:16:56 AM
            With lrecreaProperty_ID
                .StoredProcedure = "reaProperty_ID"
                .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

                If .Run Then
                    If (.FieldToClass("nId") = eRemoteDB.Constants.intNull) Then
                        FindPropertyID = 1
                    Else
                        FindPropertyID = .FieldToClass("nId") + 1
                    End If
                    .RCloseRec()
                End If
            End With

FindPropertyID_Err:
            If Err.Number Then
                FindPropertyID = False
            End If
            'UPGRADE_NOTE: Object lrecreaProperty_ID may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
            lrecreaProperty_ID = Nothing
        End Get
    End Property

    '**% Update: Updates the insuranced goods in a policy.
    '% Update: Actualiza los bienes asegurados dentro de la póliza
    '-------------------------------------------------------------
    Public Function Update() As Boolean
        '-------------------------------------------------------------
        Dim lrecinsProperty As eRemoteDB.Execute

        lrecinsProperty = New eRemoteDB.Execute

        On Error GoTo Update_Err

        '**+ Parameter definition for stored procedure 'insudb.insProperty'
        '+ Definición de parámetros para stored procedure 'insudb.insProperty'
        '**+ Information read on November 08,2000  10:07:20 a.m.
        '+ Información leída el 08/11/2000 10:07:20 AM
        With lrecinsProperty
            .StoredProcedure = "INSCOVER_DETAILS"
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nModulec", nModulec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 4, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCover", nCover, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 4, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nType", ntype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCode_good", nCode_good, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCapital", nCapital, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nRate", nRate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 4, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sDescript", sDescript, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 200, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCurrency", nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPremium", nPremium, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            Update = .Run(False)
        End With

Update_Err:
        If Err.Number Then
            Update = False
        End If
        'UPGRADE_NOTE: Object lrecinsProperty may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecinsProperty = Nothing
    End Function

    '% Find: Devuelve un registro de la tabla property
    Public Function Find(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal nModulec As Integer, ByVal nCover As Integer, ByVal nType As Integer, ByVal nCode_good As Integer, ByVal dEffecdate As Date) As Boolean
        Dim lrecreaProperty As eRemoteDB.Execute

        lrecreaProperty = New eRemoteDB.Execute

        On Error GoTo Find_Err

        '**+ Parameter definition for stored procedure 'insudb.reaProperty_ID'
        '+ Definición de parámetros para stored procedure 'insudb.reaProperty_ID'
        '**+ Information read on November08,2000  10:16:56 a.m.
        '+ Información leída el 08/11/2000 10:16:56 AM
        With lrecreaProperty
            .StoredProcedure = "reacover_details_1"
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nModulec", nModulec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 4, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCover", nCover, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 4, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nType", nType, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCode_good", nCode_good, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            If .Run Then
                Find = True
                Me.sCertype = .FieldToClass("sCertype")
                Me.nBranch = .FieldToClass("nBranch")
                Me.nProduct = .FieldToClass("nProduct")
                Me.nPolicy = .FieldToClass("nPolicy")
                Me.nCertif = .FieldToClass("nCertif")
                Me.dEffecdate = .FieldToClass("dEffecdate")
                Me.nCode_good = .FieldToClass("nCode_good")
                
            Else
                Find = False
            End If
        End With

Find_Err:
        If Err.Number Then
            Find = False
        End If
        'UPGRADE_NOTE: Object lrecreaProperty may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecreaProperty = Nothing
    End Function

    '% insValCA060: Valida la información general del bien a asegurar
    Public Function insValCA060(ByVal sCodispl As String, ByVal sAction As String, ByVal nModulec As Integer, ByVal nCover As Integer, ByVal nType As Integer, ByVal nCode_good As Integer, ByVal sDescript As String, ByVal nCurrency As Integer, ByVal nRate As Double, ByVal nPremium As Double, ByVal nCapital As Double, ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Integer, ByVal nCertif As Integer, ByVal dEffecdate As Date) As String
        Dim lobjErrors As eFunctions.Errors
        Dim lclsCover_Detail As Cover_Detail
        Dim lclsProf_ord As Object
        Dim intCount As Integer

        lobjErrors = New eFunctions.Errors
        lclsCover_Detail = New Cover_Detail

        insValCA060 = CStr(True)
        On Error GoTo insValCA060_Err

        If sAction = "Add" Then
            If lclsCover_Detail.Find(sCertype, nBranch, nProduct, nPolicy, nCertif, nModulec, nCover, nType, nCode_good, dEffecdate) Then
                Call lobjErrors.ErrorMessage(sCodispl, 10004)
            End If
        End If

        If ((nCover = eRemoteDB.Constants.intNull Or nCover = 0)) Then
            Call lobjErrors.ErrorMessage(sCodispl, 55665, , , "  Cobertura")
        End If

        If ((nPremium = eRemoteDB.Constants.intNull Or nPremium = 0)) Then
            Call lobjErrors.ErrorMessage(sCodispl, 55665, , , " Prima ")
        End If



        If ((nType = eRemoteDB.Constants.intNull Or nType = 0)) Then
            Call lobjErrors.ErrorMessage(sCodispl, 55665, , , " Tipo de desglose")
        End If

        '+ Descripción de bien, vacía
        If Trim(sDescript) = "" Then
            Call lobjErrors.ErrorMessage(sCodispl, 55665, , , " Descripción")
            insValCA060 = CStr(False)
        End If

        '+ Tasa inválida
        If nRate <= 0 Or nRate = eRemoteDB.Constants.intNull Then
            Call lobjErrors.ErrorMessage(sCodispl, 55665, , , " Tasa")
            insValCA060 = CStr(False)
        End If


        '+ Moneda inválida
        If nCurrency <= 0 Or nCurrency = eRemoteDB.Constants.intNull Then
            Call lobjErrors.ErrorMessage(sCodispl, 55665, , , " Moneda")
            insValCA060 = CStr(False)
        End If

        '+ Capital inválido
        If nCapital <= 0 Or nCapital = eRemoteDB.Constants.intNull Then
            Call lobjErrors.ErrorMessage(sCodispl, 55665, , , " Capital")
            insValCA060 = CStr(False)
        End If




        insValCA060 = lobjErrors.Confirm

        'UPGRADE_NOTE: Object lobjErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lobjErrors = Nothing
        'UPGRADE_NOTE: Object lclsTab_goods may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsCover_Detail = Nothing
        'UPGRADE_NOTE: Object lclsProf_ord may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsProf_ord = Nothing

insValCA060_Err:
        If Err.Number Then
            insValCA060 = insValCA060 & Err.Description
        End If
        On Error GoTo 0

    End Function

    '% insPostCA060: Se realiza la actualización de los datos de los bienes asegurables y el
    '%               estado de la forma (PolicyWin)
    Public Function insPostCA060(ByVal nTransaction As Integer, ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, ByVal nUsercode As Integer, ByVal sAction As String, ByVal nModulec As Integer, ByVal nCover As Integer, ByVal nType As Integer, ByVal nCode_good As Integer, ByVal sDescript As String, ByVal nCurrency As Integer, ByVal nRate As Double, ByVal nPremium As Double, ByVal nCapital As Double) As Boolean
        Dim lclsPolicy_Win As ePolicy.Policy_Win
        Dim lclsCover_Details As Cover_Details

        On Error GoTo insPostCA060_Err
        Select Case nTransaction
            '+ Consulta de: póliza, certificados, cotización, solicitud
            Case 8, 9, 10, 11
            Case Else
                If insUpdTdbCA060(nTransaction, sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate, nUsercode, sAction, nModulec, nCover, nType, nCode_good, sDescript, nRate, nPremium, nCapital, nCurrency) Then

                    insPostCA060 = True
                    lclsCover_Details = New ePolicy.Cover_Details
                    If lclsCover_Details.Find(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate) Then
                        Me.sContent = "2"
                    Else
                        Me.sContent = "1"
                    End If
                    lclsPolicy_Win = New ePolicy.Policy_Win
                    Call lclsPolicy_Win.Add_PolicyWin(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate, nUsercode, "CA060", Me.sContent)
                    Call lclsPolicy_Win.Add_PolicyWin(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate, nUsercode, "CA014", "3")
                    If nBranch = 21 Then
                        Call lclsPolicy_Win.Add_PolicyWin(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate, nUsercode, "MU700", "3")
                        Call lclsPolicy_Win.Add_PolicyWin(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate, nUsercode, "CA009", "3")
                    End If

                Else
                    insPostCA060 = False
                End If
        End Select

insPostCA060_Err:
        If Err.Number Then
            insPostCA060 = False
        End If
        lclsCover_Details = New ePolicy.Cover_Details
        'UPGRADE_NOTE: Object lclsPolicy_Win may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsPolicy_Win = Nothing
        On Error GoTo 0
    End Function

    '% insUpdTdbCA060: Actualiza los datos de los bienes asegurables
    Private Function insUpdTdbCA060(ByVal nTransaction As Integer, ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, ByVal nUsercode As Integer, ByVal sAction As String, ByVal nModulec As Integer, ByVal nCover As Integer, ByVal nType As Integer, ByVal nCode_good As Integer, ByVal sDescript As String, ByVal nRate As Double, ByVal nPremium As Double, ByVal nCapital As Double, ByVal nCurrency As Integer) As Boolean
        Dim lclsProdMaster As eProduct.Product
        Dim lclsAuto As Automobile

        lclsProdMaster = New eProduct.Product
        lclsAuto = New Automobile

        With Me
            .sCertype = sCertype
            .nBranch = nBranch
            .nProduct = nProduct
            .nPolicy = nPolicy
            .nCertif = nCertif
            .dEffecdate = dEffecdate
            .nUsercode = nUsercode


            Select Case sAction

                '+ Registro nuevo
                Case "Add"
                    .nCode_good = nCode_good
                    .nCapital = nCapital
                    .nRate = nRate
                    .sDescript = Trim(sDescript)
                    .nCurrency = nCurrency
                    .nPremium = nPremium
                    .nCover = nCover
                    .nModulec = nModulec
                    .ntype = nType
                    .nCapital = nCapital
                    insUpdTdbCA060 = .Add

                    '+ Eliminación de registro
                Case "Del"
                    .nCode_good = nCode_good
                    .nCover = nCover
                    .nModulec = nModulec
                    .ntype = nType
                    Select Case nTransaction
                        Case 1, 3, 4, 5, 6, 7

                            '+ Eliminación de registro : 1 - Por rechazo de siniestro
                            insUpdTdbCA060 = .Delete("1")
                        Case Else

                            '+ Eliminación de registro : 2 - Por anulación
                            insUpdTdbCA060 = .Delete("2")
                    End Select

                    '+ Actualización de registro
                Case "Update"
                    .nCode_good = nCode_good
                    .nCapital = nCapital
                    .nRate = nRate
                    .sDescript = Trim(sDescript)
                    .nCurrency = nCurrency
                    .nPremium = nPremium
                    .nCover = nCover
                    .nModulec = nModulec
                    .ntype = nType
                    .nCapital = nCapital
                    insUpdTdbCA060 = .Update
            End Select

        End With

        'UPGRADE_NOTE: Object lclsAuto may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsAuto = Nothing
        'UPGRADE_NOTE: Object lclsProdMaster may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsProdMaster = Nothing

    End Function
End Class






