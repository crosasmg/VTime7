Option Strict Off
Option Explicit On
Public Class Agreement_pol
    '%-------------------------------------------------------%'
    '% $Workfile:: Agreement_pol.cls                           $%'
    '% $Author:: Clobos                                     $%'
    '% $Date:: 5-04-06 21:53                                $%'
    '% $Revision:: 2                                        $%'
    '%-------------------------------------------------------%'

    '**-Properties according the table in the system on 11/23/2000
    '-Propiedades según la tabla en el sistema el 26/03/2002
    '-La llave primaria corresponde a sCertype , nBranch, nProduct, nPolicy, nCertif, sClient, dEffecdate, nId


    'Column_name               Type                        Computed   Length Prec  Scale Nullable TrimTrailingBlanks  FixedLenNullInSource
    '------------------------  -------------------------   --------   ------ ----- ----- -------- ------------------  --------------------
    Public sCertype As String 'char       no         1      no    no       no
    Public nBranch As Integer 'smallint   no         2      5     0        no    (n/a)               (n/a)
    Public nProduct As Integer 'smallint   no         2      5     0        no    (n/a)               (n/a)
    Public nPolicy As Double 'int        no         4      10    0        no    (n/a)               (n/a)
    Public nCertif As Double 'int        no         4      10    0        no    (n/a)               (n/a)
    Public sClient As String 'char       no
    Public sDigit As String 'char       no
    Public dEffecdate As Date 'datetime   no         8      no                   (n/a)               (n/a)
    Public nCod_Agree As Double 'decimal    no         5      5     2        yes   (n/a)               (n/a)
    Public nUsercode As Integer 'smallint   no         2      5     0        yes   (n/a)               (n/a)
    '-Nombre del Cliente
    Public sCliename As String
    Public sAction_aux As String

    '**%Find: Function that returns TRUE to make the reading of the records in the 'Agreement_pol' table
    '%Find: Función que retorna VERDADERO realizar la lectura de registros en la tabla 'Agreement_pol'
    Public Function Find(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal sClient As String, ByVal dEffecdate As Date, ByVal nCod_Agree As Integer) As Boolean
        Dim lrecReaAgreement_pol As eRemoteDB.Execute

        On Error GoTo Find_Err

        lrecReaAgreement_pol = New eRemoteDB.Execute

        '**+Parameters definition to stored procedure ' insudb.reaAgreement_pol'
        '**+Data read on 11/23/2000 3:52:14 p.m.
        '+Definición de parámetros para stored procedure 'insudb.reaAgreement_pol'
        '+Información leída el 23/11/2000 3:52:14 p.m.

        With lrecReaAgreement_pol
            .StoredProcedure = "insAgreement_polPKG.reaAgreement_pol"
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCod_Agree", nCod_Agree, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If .Run Then
                sClient = .FieldToClass("sClient")
                sCliename = .FieldToClass("sCliename")
                sDigit = .FieldToClass("sDigit")
                nCod_Agree = .FieldToClass("nCod_Agree")
                Find = True
                .RCloseRec()
            End If
        End With

Find_Err:
        If Err.Number Then
            Find = False
        End If
        On Error GoTo 0
        'UPGRADE_NOTE: Object lrecReaAgreement_pol may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecReaAgreement_pol = Nothing
    End Function

    '**%ADD: Function that returns TRUE when it inserts a record in the 'Agreement_pol' table
    '%ADD: Función que retorna VERDADERO al insertar un registro en la tabla 'Agreement_pol'
    Public Function insAgreement_pol() As Boolean
        Dim lreccreAgreement_pol As eRemoteDB.Execute

        On Error GoTo insAgreement_pol_err

        lreccreAgreement_pol = New eRemoteDB.Execute

        '**+Parameters definition to stored procedure ' insudb.creAgreement_pol'
        '**+Data read on 11/23/2000 1:44:31 p.m.
        '+Definición de parámetros para stored procedure 'insudb.creAgreement_pol'
        '+Información leída el 23/11/2000 1:44:31 p.m.

        With lreccreAgreement_pol
            .StoredProcedure = "insAgreement_polPKG.insAgreement_pol"
            .Parameters.Add("sAction", sAction_aux, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCod_Agree", nCod_Agree, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            insAgreement_pol = .Run(False)
        End With

insAgreement_pol_err:
        If Err.Number Then
            insAgreement_pol = False
        End If
        On Error GoTo 0
        'UPGRADE_NOTE: Object lreccreAgreement_pol may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lreccreAgreement_pol = Nothing
    End Function


    '% insValCA002: Realiza la validación de los campos a actualizar en la ventana CA002
    Public Function insValCA002(ByVal sAction As String, ByVal sWindowType As String, ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal sClient As String, ByVal dEffecdate As Date, ByVal nCod_Agree As Integer, Optional ByVal nUsercode As Integer = 0) As String
        Dim lclsErrors As eFunctions.Errors
        Dim lcolAgreement_pol As Agreement_pols
        Dim lclsPolicyWin As ePolicy.Policy_Win

        On Error GoTo insValCA002_Err

        lclsErrors = New eFunctions.Errors
        lcolAgreement_pol = New Agreement_pols
        lclsPolicyWin = New ePolicy.Policy_Win

        If sWindowType = "PopUp" Then
            '+ La combinación Agreement_polio-cobertura debe ser única
            If sAction = "Add" Then

                If nCod_Agree = eRemoteDB.Constants.intNull Then
                    Call lclsErrors.ErrorMessage("CA002", 55004)
                End If
                If sClient = vbNullString Then
                    Call lclsErrors.ErrorMessage("CA002", 55769)
                End If

                If insvalOtherRows(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate, sClient, nCod_Agree) Then
                    Call lclsErrors.ErrorMessage("CA002", 55790)
                End If
            End If

            Dim mclsPolicy As ePolicy.Policy
            mclsPolicy = New ePolicy.Policy

            If mclsPolicy.Find(sCertype, nBranch, nProduct, nPolicy) Then
                Call lcolAgreement_pol.Find(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate)

                '+ Generacion por poliza, no puede tener mas de un convenio
                If mclsPolicy.sColinvot = "1" Then
                    If lcolAgreement_pol.Count >= 1 Then
                        Call lclsErrors.ErrorMessage("CA002", 7)
                    End If
                End If
            End If
            mclsPolicy = Nothing

            insValCA002 = lclsErrors.Confirm

        Else
            If Not lcolAgreement_pol.Find(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate) Then
                '+ Se debe indicar por lo menos un Agreement_polio
                Call lclsErrors.ErrorMessage("CA002", 3957)
            End If

            insValCA002 = lclsErrors.Confirm

            If insValCA002 = String.Empty Then
                '+ Si no existen errores se actualiza la ventana con contenido
                Call lclsPolicyWin.Add_PolicyWin(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate, nUsercode, "CA002", "2")
            End If
        End If

insValCA002_Err:
        If Err.Number Then
            insValCA002 = "insValCA002: " & Err.Description
        End If
        On Error GoTo 0
        'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsErrors = Nothing
        'UPGRADE_NOTE: Object lcolAgreement_pol may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lcolAgreement_pol = Nothing
        'UPGRADE_NOTE: Object lclsPolicyWin may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsPolicyWin = Nothing
    End Function

    '% insPostCA002: Se realiza la actualización de los datos en la ventana CA002
    Public Function insPostCA002(ByVal sAction As String, ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal sClient As String, ByVal dEffecdate As Date, ByVal nCod_Agree As Integer, ByVal nUsercode As Integer) As Boolean
        '- Declaración de los objetos a ser utilizados
        Dim lcolAgreement_pols As ePolicy.Agreement_pols
        Dim lclsPolicyWin As ePolicy.Policy_Win
        Dim lblnUpdPw As Boolean

        On Error GoTo insPostCA002_Err

        mstrContent = String.Empty

        '+ Creación de las instancias de los objetos a ser utilizados
        With Me
            .sCertype = sCertype
            .nBranch = nBranch
            .nProduct = nProduct
            .nPolicy = nPolicy
            .nCertif = nCertif
            .sClient = sClient
            .dEffecdate = dEffecdate
            .nCod_Agree = nCod_Agree
            .nUsercode = nUsercode

            Select Case sAction
                Case "Add"
                    sAction_aux = "1"
                    insPostCA002 = .insAgreement_pol
                Case "Update"
                    sAction_aux = "2"
                    insPostCA002 = .insAgreement_pol
                Case "Del"
                    sAction_aux = "3"
                    insPostCA002 = .insAgreement_pol
                    If insPostCA002 Then
                        '+ Se llama a la función FIND de la colección Agreement_pols para saber si hay o no registros
                        lcolAgreement_pols = New ePolicy.Agreement_pols
                        Call lcolAgreement_pols.Find(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate)

                        If lcolAgreement_pols.Count = 0 Then
                            mstrContent = "1"
                            lblnUpdPw = True
                        End If
                    End If
            End Select
        End With

        If lblnUpdPw Then
            '+ Si no hay registros en la colección Agreement_polS se coloca la ventana SIN CONTENIDO
            lclsPolicyWin = New ePolicy.Policy_Win
            Call lclsPolicyWin.Add_PolicyWin(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate, nUsercode, "CA002", mstrContent)
        End If

insPostCA002_Err:
        If Err.Number Then
            insPostCA002 = False
        End If
        'UPGRADE_NOTE: Object lclsPolicyWin may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsPolicyWin = Nothing
        'UPGRADE_NOTE: Object lcolAgreement_pols may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lcolAgreement_pols = Nothing
        On Error GoTo 0
    End Function

    '% insvalOtherRows: Se realiza la actualización de los datos en la ventana CA002
    Private Function insvalOtherRows(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, ByVal sClient As String, ByVal nCod_Agree As Integer) As Boolean
        Dim lcolAgreement_pol As Agreement_pols
        Dim lclsAgreement_pol As Agreement_pol

        On Error GoTo insvalOtherRows_err

        lcolAgreement_pol = New Agreement_pols

        If lcolAgreement_pol.Find(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate) Then
            For Each lclsAgreement_pol In lcolAgreement_pol
                If lclsAgreement_pol.sClient = sClient And lclsAgreement_pol.nCod_Agree = nCod_Agree Then
                    insvalOtherRows = True
                End If
            Next lclsAgreement_pol
        End If

insvalOtherRows_err:
        If Err.Number Then
            insvalOtherRows = False
        End If
        On Error GoTo 0
        'UPGRADE_NOTE: Object lcolAgreement_pol may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lcolAgreement_pol = Nothing
        'UPGRADE_NOTE: Object lclsAgreement_pol may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsAgreement_pol = Nothing
    End Function

    '*sContent: Obtiene el indicador de contenido de la transacción
    Public ReadOnly Property sContent() As String
        Get
            sContent = mstrContent
        End Get
    End Property
End Class






