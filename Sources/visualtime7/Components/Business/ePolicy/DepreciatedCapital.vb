Option Strict Off
Option Explicit On
Public Class DepreciatedCapital
    '%-------------------------------------------------------%'
    '% $Workfile:: DepreciatedCapital.cls                           $%'
    '% $Author:: Nvaplat7                                   $%'
    '% $Date:: 9/08/03 1:06p                                $%'
    '% $Revision:: 28                                       $%'
    '%-------------------------------------------------------%'

    '+ Propiedades según la tabla en el sistema el 20/06/2015
    '+ El campo llave corresponde a nBranch nProduct dEffecdate nDepreciatedCapital.

    '+ Column_name        Type                 Length Prec  Scale Nullable TrimTrailingBlanks FixedLenNullInSource
    '+ ------------------ -------------------- ------ ----- ----- -------- ------------------ --------------------
    Public sCertype As String ' CHAR           1              No
    Public nBranch As Integer ' NUMBER        22     5      0 No
    Public nProduct As Integer ' NUMBER        22     5      0 No
    Public nPolicy As Double ' NUMBER        22    10      0 No
    Public nCertif As Double ' NUMBER        22    10      0 No
    Public nGroup_insu As Double
    Public nModulec As Double
    Public nCover As Double
    Public dEffecdate As Date ' DATE           7              No
    Public nCapital As Double ' NUMBER        22    18      6 Yes
    Public dNulldate As Date ' DATE           7              Yes
    Public dExpirdat As Date ' DATE           7              Yes
    Public dCompdate As Date
    Public nUsercode As Integer
    Public dStartdate As Date
    Public nEndorsementValue As Double
   
    Public Function Find(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal nGroup_insu As Integer, ByVal nModulec As Double, ByVal nCover As Double, ByVal dEffecdate As Date, ByVal dStartdate As Date, Optional ByVal lblnFind As Boolean = False) As Boolean
        Dim lrecreaDepreciatedCapital As eRemoteDB.Execute

        On Error GoTo Find_Err

        lrecreaDepreciatedCapital = New eRemoteDB.Execute

        '+ Definición de parámetros para stored procedure 'insudb.reaType_amend'
        '+ Información leída el 11/10/01 09:25:55 AM
        With lrecreaDepreciatedCapital
            .StoredProcedure = "reaDepreciatedCapital"
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nGroup_insu", nGroup_insu, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nModulec", nModulec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCover", nCover, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dStartdate", dStartdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If .Run Then
                Me.sCertype = .FieldToClass("sCertype")
                Me.nBranch = .FieldToClass("nBranch")
                Me.nProduct = .FieldToClass("nProduct")
                Me.nPolicy = .FieldToClass("nPolicy")
                Me.nCertif = .FieldToClass("nCertif")
                Me.nGroup_insu = .FieldToClass("nGroup_insu")
                Me.nModulec = .FieldToClass("nModulec")
                Me.nCover = .FieldToClass("nCover")
                Me.dNulldate = .FieldToClass("dNulldate")
                Me.dEffecdate = .FieldToClass("dEffecdate")
                Me.dStartdate = .FieldToClass("dStartdate")
                Me.nCapital = .FieldToClass("nCapital")
                Me.dExpirdat = .FieldToClass("dExpirdat")
                Me.nEndorsementValue = .FieldToClass("nEndorsementValue")
                .RCloseRec()
                Find = True
            Else
                Find = False
                Me.nCapital = 0
            End If
        End With
        'UPGRADE_NOTE: Object lrecreaType_amend may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecreaDepreciatedCapital = Nothing
Find_Err:
        If Err.Number Then
            Find = False
        End If
        On Error GoTo 0
    End Function

    Public Function CalculateDepreciatedCapitalByCoverage(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal nGroup_insu As Integer, ByVal dEffecdate As Date, ByVal sRoucapit As String, ByVal nCapital As Double, ByVal nType_amend As Double, ByVal dStartdate As Date, ByVal nTransaction As Integer, Optional ByVal lblnFind As Boolean = False) As Boolean
        Dim lrecreaDepreciatedCapital As eRemoteDB.Execute
        Dim nModulecAux As Double = 0
        Dim nCoverAux As Double = 0
        On Error GoTo Find_Err

        lrecreaDepreciatedCapital = New eRemoteDB.Execute

        '+ Definición de parámetros para stored procedure 'insudb.reaType_amend'
        '+ Información leída el 11/10/01 09:25:55 AM
        With lrecreaDepreciatedCapital
            .StoredProcedure = "reaGen_CoverByRoutine"
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nGroup_insu", nGroup_insu, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sRoucapit", sRoucapit, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbChar, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If .Run Then
                nModulecAux = .FieldToClass("nModulec")
                nCoverAux = .FieldToClass("nCover")
                Me.Find(sCertype, nBranch, nProduct, nPolicy, nCertif, nGroup_insu, nModulecAux, nCoverAux, dEffecdate, dEffecdate)
                'If Me.nCapital <> nCapital Then
                Me.CalDepreciatedCapital(sCertype, nBranch, nProduct, nPolicy, nCertif, nGroup_insu, nModulecAux, nCoverAux, dEffecdate, nCapital, nUsercode, nType_amend, dStartdate, nTransaction)
                'End If
                .RCloseRec()
                CalculateDepreciatedCapitalByCoverage = True
            Else
                CalculateDepreciatedCapitalByCoverage = False
                Me.nCapital = 0
            End If
        End With
        'UPGRADE_NOTE: Object lrecreaType_amend may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecreaDepreciatedCapital = Nothing
Find_Err:
        If Err.Number Then
            CalculateDepreciatedCapitalByCoverage = False
        End If
        On Error GoTo 0
    End Function

    Public Function GetCapitalByRoutine(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal nGroup_insu As Integer, ByVal dEffecdate As Date, ByVal sRoucapit As String, Optional ByVal lblnFind As Boolean = False) As Boolean
        Dim lrecreaDepreciatedCapital As eRemoteDB.Execute
        Dim nModulecAux As Double = 0
        Dim nCoverAux As Double = 0
        On Error GoTo Find_Err

        lrecreaDepreciatedCapital = New eRemoteDB.Execute

        '+ Definición de parámetros para stored procedure 'insudb.reaType_amend'
        '+ Información leída el 11/10/01 09:25:55 AM
        With lrecreaDepreciatedCapital
            .StoredProcedure = "reaGen_CoverByRoutine"
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nGroup_insu", nGroup_insu, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sRoucapit", sRoucapit, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbChar, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If .Run Then
                nModulecAux = .FieldToClass("nModulec")
                nCoverAux = .FieldToClass("nCover")
                Me.Find(sCertype, nBranch, nProduct, nPolicy, nCertif, nGroup_insu, nModulecAux, nCoverAux, dEffecdate, dEffecdate)
                .RCloseRec()
                GetCapitalByRoutine = True
            Else
                GetCapitalByRoutine = False
                Me.nCapital = 0
            End If
        End With
        'UPGRADE_NOTE: Object lrecreaType_amend may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecreaDepreciatedCapital = Nothing
Find_Err:
        If Err.Number Then
            GetCapitalByRoutine = False
        End If
        On Error GoTo 0
    End Function

    '% Update: Esta función se encarga de actualizar información en la tabla principal de la clase.
    Public Function Update(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal nGroup_insu As Integer, ByVal nModulec As Double, ByVal nCover As Double, ByVal dEffecdate As Date, ByVal nCapital As Double, ByVal nUsercode As Integer, ByVal dStartdate As Date, ByVal dExpirdat As Date, ByVal nType_amend As Double, ByVal nEndorsementValue As Double) As Boolean
        Dim lrecupdDepreciatedCapital As eRemoteDB.Execute

        On Error GoTo Update_Err

        lrecupdDepreciatedCapital = New eRemoteDB.Execute

        '+ Definición de parámetros para stored procedure 'insudb.updDepreciatedCapital'
        '+ Información leída el 24/09/01 03:51:44 p.m.
        With lrecupdDepreciatedCapital
            .StoredProcedure = "insupdDepreciatedCapital"
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nGroup_insu", nGroup_insu, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nModulec", nModulec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCover", nCover, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dStartdate", dStartdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dExpirdat", dExpirdat, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCapital", nCapital, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nType_amend", nType_amend, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nEndorsementValue", nEndorsementValue, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            Update = .Run(False)
        End With

Update_Err:
        If Err.Number Then
            Update = False
        End If
        lrecupdDepreciatedCapital = Nothing
        On Error GoTo 0
    End Function

    Public Function Add(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal nGroup_insu As Integer, ByVal nModulec As Double, ByVal nCover As Double, ByVal dEffecdate As Date, ByVal nCapital As Integer, ByVal nUsercode As Integer, ByVal dStartdate As Date, ByVal dExpirdat As Date) As Boolean
        Dim lrecupdDepreciatedCapital As eRemoteDB.Execute

        On Error GoTo Add_Err

        lrecupdDepreciatedCapital = New eRemoteDB.Execute

        '+ Definición de parámetros para stored procedure 'insudb.updDepreciatedCapital'
        '+ Información leída el 24/09/01 03:51:44 p.m.
        With lrecupdDepreciatedCapital
            .StoredProcedure = "creDepreciatedCapital"
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nGroup_insu", nGroup_insu, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nModulec", nModulec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCover", nCover, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dStartdate", dStartdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dExpirdat", dExpirdat, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCapital", nCapital, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            Add = .Run(False)
        End With

Add_Err:
        If Err.Number Then
            Add = False
        End If
        lrecupdDepreciatedCapital = Nothing
        On Error GoTo 0
    End Function

    '% Update: Esta función se encarga de actualizar información en la tabla principal de la clase.
    Public Function CalDepreciatedCapital(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal nGroup_insu As Integer, ByVal nModulec As Double, ByVal nCover As Double, ByVal dEffecdate As Date, ByVal nCapital As Integer, ByVal nUsercode As Integer, ByVal nType_amend As Double, ByVal dStartdate As Date, ByVal nTransaction As Double) As Boolean
        Dim lrecupdDepreciatedCapital As eRemoteDB.Execute

        On Error GoTo CalDepreciatedCapital_Err

        lrecupdDepreciatedCapital = New eRemoteDB.Execute

        '+ Definición de parámetros para stored procedure 'insudb.updDepreciatedCapital'
        '+ Información leída el 24/09/01 03:51:44 p.m.
        With lrecupdDepreciatedCapital
            .StoredProcedure = "insCalDepreciatedCapitalTable"
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nGroup_insu", nGroup_insu, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nModulec", nModulec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCover", nCover, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dStartdate", dStartdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCapital", nCapital, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nType_amend", nType_amend, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCaller", 1, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nEndorsementvalue", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nTransaction", nTransaction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            CalDepreciatedCapital = .Run(False)
        End With

CalDepreciatedCapital_Err:
        If Err.Number Then
            CalDepreciatedCapital = False
        End If
        lrecupdDepreciatedCapital = Nothing
        On Error GoTo 0
    End Function

    '%insValCA054: Esta función se encarga de validar los datos introducidos en la zona de detalle para
    '%forma.
    Public Function insValCA054Upd(ByVal sCodispl As String, ByVal nDepreciatedCapital As Double, ByVal nInitialCapital As Double, ByVal nEndorsementValue As Double) As String
        '- Se define el objeto lclsDepreciatedCapital, el manejo de la libreria de Endosos por Ramo Producto
        Dim lclsDepreciatedCapital As ePolicy.DepreciatedCapital

        '- Se define la variable lclserrors para el envío de errores de la ventana
        Dim lclsErrors As eFunctions.Errors

        On Error GoTo insValCA054_Err
        lclsErrors = New eFunctions.Errors
        With lclsErrors
            '+ Validación del campo: Capital.
            If nDepreciatedCapital <= 0 Or nDepreciatedCapital = eRemoteDB.Constants.dblNull Then
                Call .ErrorMessage(sCodispl, 90000501)
            Else
                If nInitialCapital < nDepreciatedCapital Then
                    Call .ErrorMessage(sCodispl, 90000502)
                End If
            End If

            If nEndorsementValue > nDepreciatedCapital Then
                Call .ErrorMessage(sCodispl, 900062)
            End If

            insValCA054Upd = .Confirm
        End With

insValCA054_Err:
        If Err.Number Then
            insValCA054Upd = lclsErrors.Confirm & Err.Description
        End If
        On Error GoTo 0
        lclsDepreciatedCapital = Nothing
        lclsErrors = Nothing
    End Function

    '% InsPostCA054: Esta función se encarga de crear/actualizar los registros
    '% correspondientes en la tabla de DepreciatedCapital
    Public Function insPostCA054(ByVal sAction As String, ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal nGroup_insu As Integer, ByVal nModulec As Double, ByVal nCover As Double, ByVal dEffecdate As Date, ByVal dStartdate As Date, ByVal dExpirdat As Date, ByVal nCapital As Double, ByVal nUsercode As Integer, ByVal nType_amend As Double, ByVal nEndorsementValue As Double) As Boolean
        On Error GoTo InsPostCA054_err
        Me.sCertype = sCertype
        Me.nBranch = nBranch
        Me.nProduct = nProduct
        Me.nPolicy = nPolicy
        Me.nCertif = nCertif
        Me.nGroup_insu = nGroup_insu
        Me.nModulec = nModulec
        Me.nCover = nCover
        Me.dEffecdate = dEffecdate
        Me.nCapital = nCapital
        Me.dExpirdat = dExpirdat
        Me.dStartdate = dStartdate
        Me.nEndorsementValue = nEndorsementValue
        insPostCA054 = True

        insPostCA054 = Update(sCertype, nBranch, nProduct, nPolicy, nCertif, nGroup_insu, nModulec, nCover, dEffecdate, nCapital, nUsercode, dStartdate, dExpirdat, nType_amend, nEndorsementValue)

InsPostCA054_err:
        If Err.Number Then
            insPostCA054 = False
        End If
        On Error GoTo 0
    End Function


    Public Sub New()
        MyBase.New()
    End Sub
End Class






