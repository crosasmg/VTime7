Option Strict Off
Option Explicit On
'Imports System.Resources

Imports eFunctions.Extensions

Public Class TRCM
    '%-------------------------------------------------------%'
    '% $Workfile:: Activ_Group.cls                          $%'
    '% $Author:: Nvaplat41                                  $%'
    '% $Date:: 9/10/03 19.01                                $%'
    '% $Revision:: 26                                       $%'
    '%-------------------------------------------------------%'

    '+ Definición de la tabla AccidentPerson tomada el 02/02/2002 12:19
    '+ Column_Name                                      Type     Length  Prec  Scale Nullable
    ' ------------------------------------------------- -------- ------- ----- ------ --------
    Public sCertype As String ' CHAR           1              No
    Public nBranch As Double ' NUMBER        22     5      0 No
    Public nProduct As Double ' NUMBER        22     5      0 No
    Public nPolicy As Double ' NUMBER        22    10      0 No
    Public nCertif As Double ' NUMBER        22     5      0 No
    Public dInitialdate_work As Date '
    Public dEffecdate As Date
    Public dEnddate_work As Date
    Public dNulldate As Date
    Public dCompdate As Date
    Public nUsercode As Integer
    Public dInitialdate_m As Date
    Public dEnddate_m As Date
    Public nBeneficiarnotes As String
    Public sWorkname As String
    Public nWorktype As Integer
    Public dInitialdate_em As Date
    Public dEnddate_em As Date
    Public dBirthdat As Date
    Public sDesc_work As String
    Public nGroup As Double
    Public nSituation As Double
    Public nNumberInsured As Double


    ' - Variable que guarda la transacción que se está ejecutando
    Public nPercent_group As Double
    Public nTransaction As Integer

    Public resxValues As IEnumerable(Of DictionaryEntry) = eFunctions.Values.GetResxValue("CM001")


    Public Function insValCM001(ByVal sCodispl As String, ByVal sWorkname As String, ByVal nWorktype As Integer, ByVal dInitialdate_work As Date, ByVal dEnddate_work As Date) As String
        '- Se define el objeto para el manejo de la clase Product
        Dim lobjErrors As eFunctions.Errors
        Dim resxValues As IEnumerable(Of DictionaryEntry) = eFunctions.Values.GetResxValue("CM001")
        On Error GoTo insValCM001_Err
        lobjErrors = New eFunctions.Errors

        If sWorkname = eRemoteDB.Constants.strNull Or sWorkname = "" Then
            Call lobjErrors.ErrorMessage(sCodispl, 55665, , eFunctions.Errors.TextAlign.LeftAling, resxValues.FindDictionaryValue("tctWorknameCaption"))
        End If

        If nWorktype = eRemoteDB.Constants.dblNull Or nWorktype = 0 Then
            Call lobjErrors.ErrorMessage(sCodispl, 55665, , eFunctions.Errors.TextAlign.LeftAling, resxValues.FindDictionaryValue("cbeTypeWorkCaption"))
        End If

        If dInitialdate_work = eRemoteDB.Constants.dtmNull Or dInitialdate_work = "#12:00:00 AM#" Then
            Call lobjErrors.ErrorMessage(sCodispl, 55665, , eFunctions.Errors.TextAlign.RigthAling, resxValues.FindDictionaryValue("DateInitdate_work"))
        End If

        If dEnddate_work = eRemoteDB.Constants.dtmNull Or dEnddate_work = "#12:00:00 AM#" Then
            Call lobjErrors.ErrorMessage(sCodispl, 55665, , eFunctions.Errors.TextAlign.RigthAling, resxValues.FindDictionaryValue("DateEnddate_work"))
        ElseIf dEnddate_work <= dInitialdate_work Then
            Call lobjErrors.ErrorMessage(sCodispl, 55665, , eFunctions.Errors.TextAlign.RigthAling, resxValues.FindDictionaryValue("DateEnddate_work"))
        End If

        insValCM001 = lobjErrors.Confirm

insValCM001_Err:
        If Err.Number Then
            insValCM001 = "insValCM001: " & Err.Description
        End If
        'UPGRADE_NOTE: Object lobjErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lobjErrors = Nothing

        On Error GoTo 0
    End Function


    '%InsPostCM001Upd: Esta función realiza los cambios de BD según especificaciones funcionales
    '%                 de la transacción (CM001)
    Public Function InsPostCM001(ByVal sCertype As String, ByVal nBranch As Double, ByVal nProduct As Double, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, ByVal nGroup As Double, ByVal nSituation As Double, ByVal nTypeWork As Integer, ByVal sDesc_work As String, ByVal dInitialdate_work As Date, ByVal dEnddate_work As Date, ByVal dInitialdate_m As Date, ByVal dEnddate_m As Date, ByVal dInitialdate_em As Date, ByVal dEnddate_em As Date, ByVal nTransaction As Integer, ByVal nUsercode As Double) As Boolean
        Dim lclsPolicyWin As ePolicy.Policy_Win

        InsPostCM001 = Update(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate, nGroup, nSituation, nTypeWork, sDesc_work, dInitialdate_work, dEnddate_work, dInitialdate_m, dEnddate_m, dInitialdate_em, dEnddate_em, nTransaction, nUsercode)

        If InsPostCM001 Then
            lclsPolicyWin = New ePolicy.Policy_Win
            Call lclsPolicyWin.Add_PolicyWin(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate, nUsercode, "CM001", "2")
            lclsPolicyWin = Nothing
        End If

    End Function


    Private Function Update(ByVal sCertype As String, ByVal nBranch As Double, ByVal nProduct As Double, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, ByVal nGroup As Double, ByVal nSituation As Double, ByVal nTypeWork As Integer, ByVal sDesc_work As String, ByVal dInitialdate_work As Date, ByVal dEnddate_work As Date, ByVal dInitialdate_m As Date, ByVal dEnddate_m As Date, ByVal dInitialdate_em As Date, ByVal dEnddate_em As Date, ByVal nTransaction As Integer, ByVal nUsercode As Double) As Boolean
        Dim lclsTRCM As eRemoteDB.Execute

        lclsTRCM = New eRemoteDB.Execute


        With lclsTRCM

            .StoredProcedure = "Insupdtrcm"
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nGroup", nGroup, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nSituation", nSituation, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nTypeWork", nTypeWork, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sDesc_work", sDesc_work, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 300, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dInitialdate_work", dInitialdate_work, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEnddate_work", dEnddate_work, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dInitialdate_m", dInitialdate_m, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEnddate_m", dEnddate_m, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dInitialdate_em", dInitialdate_em, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEnddate_em", dEnddate_em, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nTransaction", nTransaction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            Update = .Run(False)
        End With

        If Err.Number Then
            Update = False
        End If
        On Error GoTo 0
        lclsTRCM = Nothing

    End Function
    '* Class_Initialize: se controla la apertura de la clase
    'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
    Private Sub Class_Initialize_Renamed()
        nUsercode = eRemoteDB.Constants.intNull
        sCertype = eRemoteDB.Constants.strNull
        nBranch = eRemoteDB.Constants.intNull
        nProduct = eRemoteDB.Constants.intNull
        nPolicy = eRemoteDB.Constants.intNull
        nCertif = eRemoteDB.Constants.intNull
        dEnddate_em = eRemoteDB.Constants.dtmNull
        dEffecdate = eRemoteDB.Constants.dtmNull
        sWorkname = eRemoteDB.Constants.strNull
        dNulldate = eRemoteDB.Constants.dtmNull
        dCompdate = eRemoteDB.Constants.dtmNull
        sDesc_work = eRemoteDB.Constants.strNull
        dInitialdate_em = eRemoteDB.Constants.dtmNull
        dInitialdate_m = eRemoteDB.Constants.dtmNull
        dEnddate_m = eRemoteDB.Constants.dtmNull
        dInitialdate_work = eRemoteDB.Constants.dtmNull
        dEnddate_work = eRemoteDB.Constants.dtmNull
        nGroup = eRemoteDB.Constants.dblNull
        nSituation = eRemoteDB.Constants.dblNull

    End Sub
    Public Sub New()
        MyBase.New()
        Class_Initialize_Renamed()
    End Sub
End Class
