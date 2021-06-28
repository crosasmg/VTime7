Option Strict Off
Option Explicit On
Public Class Beneficiar
    '%-------------------------------------------------------%'
    '% $Workfile:: Beneficiar.cls                           $%'
    '% $Author:: Clobos                                     $%'
    '% $Date:: 5-04-06 21:53                                $%'
    '% $Revision:: 2                                        $%'
    '%-------------------------------------------------------%'

    '**-Properties according the table in the system on 11/23/2000
    '-Propiedades seg�n la tabla en el sistema el 26/03/2002
    '-La llave primaria corresponde a sCertype , nBranch, nProduct, nPolicy, nCertif, sClient, dEffecdate, nId


    'Column_name               Type                        Computed   Length Prec  Scale Nullable TrimTrailingBlanks  FixedLenNullInSource
    '------------------------  -------------------------   --------   ------ ----- ----- -------- ------------------  --------------------
    Public sCertype As String 'char       no         1      no    no       no
    Public nBranch As Integer 'smallint   no         2      5     0        no    (n/a)               (n/a)
    Public nProduct As Integer 'smallint   no         2      5     0        no    (n/a)               (n/a)
    Public nPolicy As Double 'int        no         4      10    0        no    (n/a)               (n/a)
    Public nCertif As Double 'int        no         4      10    0        no    (n/a)               (n/a)
    Public sClient As String 'char       no
    Public dEffecdate As Date 'datetime   no         8      no                   (n/a)               (n/a)
    Public nParticip As Double 'decimal    no         5      5     2        yes   (n/a)               (n/a)
    Public nRelation As Integer 'smallint   no         2      5     0        no    (n/a)               (n/a)
    Public nUsercode As Integer 'smallint   no         2      5     0        yes   (n/a)               (n/a)
    Public nModulec As Integer 'smallint   no         2      5     0        yes   (n/a)               (n/a)
    Public nCover As Integer 'smallint   no         2      5     0        yes   (n/a)               (n/a)
    Public dDatedecla As Date 'datetime   no         8                     yes   (n/a)               (n/a)
    Public sIrrevoc As String 'char       no         1                     yes   (n/a)               (n/a)
    Public sConting As String 'char       no         1                     yes   (n/a)               (n/a)
    Public sDesign As String

    '**-Auxilliary properties
    '-Propiedades auxiliares

    '**-Clients name
    '-Nombre del Cliente
    Public sCliename As String

    '**%Find: Function that returns TRUE to make the reading of the records in the 'Beneficiar' table
    '%Find: Funci�n que retorna VERDADERO realizar la lectura de registros en la tabla 'Beneficiar'
    Public Function Find(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal sClient As String, ByVal dEffecdate As Date, ByVal nModulec As Integer, ByVal nCover As Integer) As Boolean
        Dim lrecReaBeneficiar As eRemoteDB.Execute

        On Error GoTo Find_Err

        lrecReaBeneficiar = New eRemoteDB.Execute

        '**+Parameters definition to stored procedure ' insudb.reaBeneficiar'
        '**+Data read on 11/23/2000 3:52:14 p.m.
        '+Definici�n de par�metros para stored procedure 'insudb.reaBeneficiar'
        '+Informaci�n le�da el 23/11/2000 3:52:14 p.m.

        With lrecReaBeneficiar
            .StoredProcedure = "reaBeneficiar"
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nModulec", IIf(nModulec = eRemoteDB.Constants.intNull, 0, nModulec), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCover", IIf(nCover = eRemoteDB.Constants.intNull, 0, nCover), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If .Run Then
                nParticip = .FieldToClass("nParticip")
                nRelation = .FieldToClass("nRelation")
                dDatedecla = .FieldToClass("dDatedecla")
                sIrrevoc = .FieldToClass("sIrrevoc")
                sCliename = .FieldToClass("sCliename")
                Find = True
                .RCloseRec()
            End If
        End With

Find_Err:
        If Err.Number Then
            Find = False
        End If
        On Error GoTo 0
        'UPGRADE_NOTE: Object lrecReaBeneficiar may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecReaBeneficiar = Nothing
    End Function

    '**%ADD: Function that returns TRUE when it inserts a record in the 'Beneficiar' table
    '%ADD: Funci�n que retorna VERDADERO al insertar un registro en la tabla 'Beneficiar'
    Public Function Add() As Boolean
        Dim lreccreBeneficiar As eRemoteDB.Execute

        On Error GoTo Add_err

        lreccreBeneficiar = New eRemoteDB.Execute

        '**+Parameters definition to stored procedure ' insudb.creBeneficiar'
        '**+Data read on 11/23/2000 1:44:31 p.m.
        '+Definici�n de par�metros para stored procedure 'insudb.creBeneficiar'
        '+Informaci�n le�da el 23/11/2000 1:44:31 p.m.
        Call sdefaulDesign()
        With lreccreBeneficiar
            .StoredProcedure = "creBeneficiar"
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nModulec", IIf(nModulec = eRemoteDB.Constants.intNull, 0, nModulec), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCover", IIf(nCover = eRemoteDB.Constants.intNull, 0, nCover), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nRelation", nRelation, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nParticip", nParticip, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dDatedecla", dDatedecla, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sIrrevoc", sIrrevoc, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sConting", sConting, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sDesign", sDesign, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            Add = .Run(False)
        End With

Add_err:
        If Err.Number Then
            Add = False
        End If
        On Error GoTo 0
        'UPGRADE_NOTE: Object lreccreBeneficiar may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lreccreBeneficiar = Nothing
    End Function

    '**%Delete: Function that returns TRUE when it eliminates a record from the 'Beneficiar' table
    '%Delete: Funci�n que retorna VERDADERO al eliminar un registro en la tabla 'Beneficiar'
    Public Function Delete() As Boolean
        Dim lrecdelBeneficiar As eRemoteDB.Execute

        On Error GoTo Delete_err

        lrecdelBeneficiar = New eRemoteDB.Execute

        '**+Parameters definition to stored procedure ' insudb.delBeneficiar'
        '**+Data read on 11/23/2000 3:25:44 p.m.
        '+Definici�n de par�metros para stored procedure 'insudb.delBeneficiar'
        '+Informaci�n le�da el 23/11/2000 3:25:44 p.m.

        With lrecdelBeneficiar
            .StoredProcedure = "delBeneficiar"
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nModulec", IIf(nModulec = eRemoteDB.Constants.intNull, 0, nModulec), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCover", IIf(nCover = eRemoteDB.Constants.intNull, 0, nCover), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            Delete = .Run(False)
        End With

Delete_err:
        If Err.Number Then
            Delete = False
        End If
        On Error GoTo 0
        'UPGRADE_NOTE: Object lrecdelBeneficiar may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecdelBeneficiar = Nothing
    End Function

    '**%Update: Function that returns TRUE when it updates a record in the ' Beneficiar' table
    '%Update: Funci�n que retorna VERDADERO al actualizar un registro en la tabla 'Beneficiar'
    Public Function Update() As Boolean
        Dim lrecupdBeneficiar As eRemoteDB.Execute

        On Error GoTo Update_Err

        lrecupdBeneficiar = New eRemoteDB.Execute

        '**+Parameters definition to stored procedure ' insudb.updBeneficiar'
        '**+Data read on 11/23/2000 3:34:03 p.m.
        '+Definici�n de par�metros para stored procedure 'insudb.updBeneficiar'
        '+Informaci�n le�da el 23/11/2000 3:34:03 p.m.
        Call sdefaulDesign()
        With lrecupdBeneficiar
            .StoredProcedure = "updBeneficiar"
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nModulec", IIf(nModulec = eRemoteDB.Constants.intNull, 0, nModulec), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCover", IIf(nCover = eRemoteDB.Constants.intNull, 0, nCover), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nRelation", nRelation, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nParticip", nParticip, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dDatedecla", dDatedecla, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sIrrevoc", sIrrevoc, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sConting", sConting, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sDesign", sDesign, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            Update = .Run(False)
        End With

Update_Err:
        If Err.Number Then
            Update = False
        End If
        On Error GoTo 0
        'UPGRADE_NOTE: Object lrecupdBeneficiar may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecupdBeneficiar = Nothing
    End Function

    '**%valExist: Validates if there is beneficiary in the policy
    '%valExist: Valida si existen beneficiarios en la p�liza
    Public Function valExist(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, ByVal sExist As String) As Boolean
        '**-Variable definition lrecreaBeneficiarCA033
        '-Se define la variable lrecreaBeneficiarCA033

        Dim lrecreaBeneficiarCA033 As eRemoteDB.Execute
        lrecreaBeneficiarCA033 = New eRemoteDB.Execute
        '**+Parameters definition to stored procedure 'insudb.reaBeneficiarCA033'
        '**+Data read on 01/23/2001 12:00:33
        '+Definici�n de par�metros para stored procedure 'insudb.reaBeneficiarCA033'
        '+Informaci�n le�da el 23/01/2001 12:00:33

        On Error GoTo valExist_Err

        With lrecreaBeneficiarCA033
            .StoredProcedure = "reaBeneficiarCA033"
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecDate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sExist", sExist, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            If .Run(False) Then
                sExist = .Parameters.Item("sExist").Value
                If sExist = "1" Then
                    valExist = True
                End If
            Else
                valExist = False
            End If
        End With
        'UPGRADE_NOTE: Object lrecreaBeneficiarCA033 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecreaBeneficiarCA033 = Nothing

valExist_Err:
        If Err.Number Then
            valExist = False
        End If
        On Error GoTo 0
    End Function

    '% insValCA023: Realiza la validaci�n de los campos a actualizar en la ventana CA023
    Public Function insValCA023(ByVal sAction As String, ByVal sCodispl As String, ByVal sWindowType As String, ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal sClient As String, ByVal dEffecdate As Date, ByVal nModulec As Integer, ByVal nCover As Integer, Optional ByVal nParticip As Double = 0, Optional ByVal nRelation As Integer = 0, Optional ByVal sPolitype As String = "", Optional ByVal nUsercode As Integer = 0) As String
        Dim lclsErrors As eFunctions.Errors
        Dim lcolBeneficiar As Beneficiars
        Dim lclsPolicyWin As ePolicy.Policy_Win
        Dim lclsRoles As Roles
        Dim lclsClient As eClient.Client

        On Error GoTo insValCA023_Err

        lclsErrors = New eFunctions.Errors
        lcolBeneficiar = New Beneficiars
        lclsRoles = New Roles
        lclsPolicyWin = New ePolicy.Policy_Win

        If sWindowType = "PopUp" Then
            '+ Validaciones del campo "% capital participaci�n"
            If nParticip = eRemoteDB.Constants.intNull Or nParticip = 0 Then
                Call lclsErrors.ErrorMessage(sCodispl, 3082)
            End If
            '+ Validaciones del campo "Parentesco"
            If nRelation = eRemoteDB.Constants.intNull Or nRelation = 0 Then
                Call lclsErrors.ErrorMessage(sCodispl, 3085)
            End If
            '+ Validaciones del campo "Cliente"
            If sClient = String.Empty Then
                '+ Debe estar lleno
                Call lclsErrors.ErrorMessage(sCodispl, 2001)
            Else
                '+ No debe corresponder al titular de la p�liza*
                If lclsRoles.Find(sCertype, nBranch, nProduct, nPolicy, nCertif, IIf(sPolitype = "1", 2, 1), sClient, dEffecdate) Then
                    Call lclsErrors.ErrorMessage(sCodispl, 10191)

                Else
                    lclsClient = New eClient.Client
                    If Not lclsClient.Find(sClient) Then
                        Call lclsErrors.ErrorMessage(sCodispl, 7050)
                    End If
                    'UPGRADE_NOTE: Object lclsClient may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
                    lclsClient = Nothing
                End If

            End If
            '+ La combinaci�n beneficiario-cobertura debe ser �nica
            If sAction = "Add" Then
                If insvalOtherCover(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate, sClient, nModulec, nCover) Then
                    Call lclsErrors.ErrorMessage(sCodispl, 55790)
                End If
            End If
            If nModulec <> eRemoteDB.Constants.intNull Then
                If nCover = eRemoteDB.Constants.intNull Then
                    '+ Si se indica el m�dulo, debe indicarse la cobertura
                    Call lclsErrors.ErrorMessage(sCodispl, 11163)
                End If
            End If

            insValCA023 = lclsErrors.Confirm

        Else
            If Not lcolBeneficiar.Find(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate) Then
                '+ Se debe indicar por lo menos un beneficiario
                Call lclsErrors.ErrorMessage(sCodispl, 3957)
            Else
                If lcolBeneficiar.nDuplicate = 2 Then
                    Call lclsErrors.ErrorMessage(sCodispl, 978990)
                End If

                '+ El % de participaci�n debe ser el 100%
                If (lcolBeneficiar.nTotalParticip = 0) Then
                    '+ El % de participaci�n debe ser el 100% designado  
                    If (lcolBeneficiar.nTotalParticipDesign <> 0 And lcolBeneficiar.nTotalParticipDesign <> 100) Then
                        Call lclsErrors.ErrorMessage(sCodispl, 3070, , , "(Designado)")
                    End If
                    '+ El % de participaci�n debe ser el 100% Contingente  
                    If (lcolBeneficiar.nTotalParticipCont <> 0 And lcolBeneficiar.nTotalParticipCont <> 100) Then
                        Call lclsErrors.ErrorMessage(sCodispl, 3070, , , "(Contingente)")
                    End If
                Else
                    If lcolBeneficiar.nTotalParticip <> 100 Then
                        Call lclsErrors.ErrorMessage(sCodispl, 3070)
                    End If

                End If

            End If

            insValCA023 = lclsErrors.Confirm

            If insValCA023 = String.Empty Then
                '+ Si no existen errores se actualiza la ventana con contenido
                Call lclsPolicyWin.Add_PolicyWin(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate, nUsercode, "CA023", "2")
            End If
        End If

insValCA023_Err:
        If Err.Number Then
            insValCA023 = "insValCA023: " & Err.Description
        End If
        On Error GoTo 0
        'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsErrors = Nothing
        'UPGRADE_NOTE: Object lcolBeneficiar may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lcolBeneficiar = Nothing
        'UPGRADE_NOTE: Object lclsRoles may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsRoles = Nothing
        'UPGRADE_NOTE: Object lclsPolicyWin may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsPolicyWin = Nothing
    End Function

    '% insPostCA023: Se realiza la actualizaci�n de los datos en la ventana CA023
    Public Function insPostCA023(ByVal sAction As String, ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double,
                                 ByVal sClient As String, ByVal nParticip As Double, ByVal nRelation As Integer, ByVal dEffecdate As Date, ByVal nModulec As Integer, ByVal nCover As Integer,
                                 ByVal nUsercode As Integer, ByVal dDatedecla As Date, ByVal sIrrevoc As String, ByVal sConting As String, Optional ByVal ncount As Integer = 0,
                                 Optional ByVal sDesign As String = "2") As Boolean
        '- Declaraci�n de los objetos a ser utilizados
        Dim lcolBeneficiars As ePolicy.Beneficiars
        Dim lclsPolicyWin As ePolicy.Policy_Win
        Dim lblnUpdPw As Boolean

        On Error GoTo insPostCA023_Err

        mstrContent = String.Empty

        '+ Creaci�n de las instancias de los objetos a ser utilizados
        With Me
            .sCertype = sCertype
            .nBranch = nBranch
            .nProduct = nProduct
            .nPolicy = nPolicy
            .nCertif = nCertif
            .sClient = sClient
            .dEffecdate = dEffecdate
            .nParticip = nParticip
            .nRelation = nRelation
            .nUsercode = nUsercode
            .nModulec = nModulec
            .nCover = nCover
            .dDatedecla = dDatedecla
            .sIrrevoc = sIrrevoc
            .sConting = sConting
            .sDesign = sDesign

            Select Case sAction
                Case "Add"
                    insPostCA023 = .Add
                Case "Update"
                    insPostCA023 = .Update
                Case "Del"
                    insPostCA023 = .Delete
                    If insPostCA023 Then
                        '+ Se llama a la funci�n FIND de la colecci�n Beneficiars para saber si hay o no registros
                        lcolBeneficiars = New ePolicy.Beneficiars
                        Call lcolBeneficiars.Find(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate)

                        If lcolBeneficiars.Count = 0 Then
                            mstrContent = "1"
                            lblnUpdPw = True
                        End If
                    End If
            End Select
        End With

        If lblnUpdPw Then
            '+ Si no hay registros en la colecci�n BENEFICIARS se coloca la ventana SIN CONTENIDO
            lclsPolicyWin = New ePolicy.Policy_Win
            Call lclsPolicyWin.Add_PolicyWin(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate, nUsercode, "CA023", mstrContent)
        End If

insPostCA023_Err:
        If Err.Number Then
            insPostCA023 = False
        End If
        'UPGRADE_NOTE: Object lclsPolicyWin may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsPolicyWin = Nothing
        'UPGRADE_NOTE: Object lcolBeneficiars may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lcolBeneficiars = Nothing
        On Error GoTo 0
    End Function

    '% insPostCA023: Se realiza la actualizaci�n de los datos en la ventana CA023
    Private Function insvalOtherCover(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, ByVal sClient As String, ByVal nModulec As Integer, ByVal nCover As Integer) As Boolean
        Dim lcolBeneficiar As Beneficiars
        Dim lclsBeneficiar As Beneficiar

        On Error GoTo insvalOtherCover_err

        lcolBeneficiar = New Beneficiars

        If lcolBeneficiar.Find(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate) Then
            For Each lclsBeneficiar In lcolBeneficiar
                If lclsBeneficiar.sClient = sClient And lclsBeneficiar.nModulec = nModulec And lclsBeneficiar.nCover = nCover Then
                    insvalOtherCover = True
                End If
            Next lclsBeneficiar
        End If

insvalOtherCover_err:
        If Err.Number Then
            insvalOtherCover = False
        End If
        On Error GoTo 0
        'UPGRADE_NOTE: Object lcolBeneficiar may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lcolBeneficiar = Nothing
        'UPGRADE_NOTE: Object lclsBeneficiar may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsBeneficiar = Nothing
    End Function

    '*sContent: Obtiene el indicador de contenido de la transacci�n
    Public ReadOnly Property sContent() As String
        Get
            sContent = mstrContent
        End Get
    End Property
    Private Sub sdefaulDesign()
        If sConting = "2" And sDesign = "2" Then
            sDesign = "1"
        End If
    End Sub
End Class