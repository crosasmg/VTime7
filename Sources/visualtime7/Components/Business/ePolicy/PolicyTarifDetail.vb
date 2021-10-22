Public Class PolicyTarifDetail
    '%-------------------------------------------------------%'
    '% $Workfile:: PolicyTarifDetail.cls                           $%'
    '% $Author:: DMendoza                                  $%'
    '% $Date:: 22/09/21 17.00                               $%'
    '% $Revision:: 1                                       $%'
    '%-------------------------------------------------------%'

    '-
    '- Estructura de tabla POLICY_TARIF_DETAIL al 22-09-2021 17:00:00
    '-     Property                   Type         DBType   Size Scale  Prec  Null
    Public sCertype As String ' CHAR       1    0     0    N
    Public nBranch As Integer ' NUMBER     22   0     5    N
    Public nProduct As Integer ' NUMBER     22   0     5    N
    Public nPolicy As Double ' NUMBER     22   0     10   N
    Public nCertif As Integer ' NUMBER     22   0     10   N
    Public nIdPolicyTarif As Integer ' NUMBER     22   0     5    N
    Public nIdTable As Integer ' NUMBER     22   0     5    N
    Public nRow As Integer ' NUMBER     22   0     5    N
    Public nIdTarifDetail As Integer ' NUMBER     22   0     5    N
    Public nMonth As Integer ' NUMBER     22   0     5    N
    Public nPercentReturn As Double ' NUMBER     22   0     5    N
    Public nAmountReturn As Double ' NUMBER     22   0     5    N
    Public nUsercode As Integer ' NUMBER     22   0     5    S
    Public dEffecdate As Date ' DATE       7    0     0    N
    Public dNulldate As Date ' DATE       7    0     0    S
    Public dCompdate As Date ' DATE       7    0     0    S

    '-Propiedades auxiliares
    Private Structure udtPolicyTarifDetail
        Dim sCertype As String
        Dim nBranch As Integer
        Dim nProduct As Integer
        Dim nPolicy As Double
        Dim nCertif As Double
        Dim nIdPolicyTarif As Integer
        Dim nIdTable As Integer
        Dim nRow As Integer
        Dim nIdTarifDetail As Integer
        Dim nMonth As Integer
        Dim nPercentReturn As Decimal
        Dim nAmountReturn As Decimal
        Dim nUsercode As Integer
        Dim dEffecdate As Date
        Dim dNulldate As Date
        Dim dCompdate As Date
    End Structure

    Private arrPolicy_Tarif() As udtPolicyTarifDetail

    '-Variable que indica si el arreglo contiene información
    Private mblnChargeArr As Boolean

    '*CountCurrenPol: propiedad que indica el número de monedas qe se encuentra en determinado
    '*momento en el arreglo de la clase
    Public ReadOnly Property CountPolicyTarif() As Integer
        Get
            If mblnChargeArr Then
                CountPolicyTarif = UBound(arrPolicy_Tarif)
            Else
                CountPolicyTarif = -1
            End If
        End Get
    End Property

    '%Val_Curren_pol: Función que busca una información de una moneda en el arreglo de la clase dado
    '%un indice de busqueda...
    Public Function Val_Policy_Tarif(ByVal intIndex As Integer) As Boolean
        If mblnChargeArr Then
            If intIndex <= UBound(arrPolicy_Tarif) Then
                With arrPolicy_Tarif(intIndex)
                    sCertype = .sCertype
                    nBranch = .nBranch
                    nProduct = .nProduct
                    nPolicy = .nPolicy
                    nCertif = .nCertif
                    nIdPolicyTarif = .nIdPolicyTarif
                    nIdTable = .nIdTable
                    nRow = .nRow
                    nIdTarifDetail = .nIdTarifDetail
                    nMonth = .nMonth
                    nPercentReturn = .nPercentReturn
                    nAmountReturn = .nAmountReturn
                    nUsercode = .nUsercode
                    dEffecdate = .dEffecdate
                    dNulldate = .dNulldate
                    dCompdate = .dCompdate
                End With
                Val_Policy_Tarif = True
            End If
        End If
    End Function

    '%LoadCurrency: Devuelve las monedas asociadas al producto y las monedas ingresadas
    '%en la tabla Curren_pol
    Public Function LoadTarifDetail(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double) As Boolean
        Dim lintCount As Integer
        Dim lrecreaPolicy_Tarif_Detail As eRemoteDB.Execute

        On Error GoTo reaPolicy_Tarif_Detail_Err

        LoadTarifDetail = False

        If Me.nPolicy <> nPolicy Or Me.nBranch <> nBranch Or Me.nProduct <> nProduct Or Me.sCertype <> sCertype Or Me.nCertif <> nCertif Then

            '+Definición de parámetros para stored procedure 'rearrCurren_pol_tmp'
            lrecreaPolicy_Tarif_Detail = New eRemoteDB.Execute
            With lrecreaPolicy_Tarif_Detail
                .StoredProcedure = "REAPOLICY_TARIF_DETAIL"
                .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                If .Run Then
                    LoadTarifDetail = True
                    ReDim arrPolicy_Tarif(150)
                    lintCount = 0
                    Do While Not .EOF
                        With arrPolicy_Tarif(lintCount)
                            .nMonth = lrecreaPolicy_Tarif_Detail.FieldToClass("NMONTH")
                            .nPercentReturn = lrecreaPolicy_Tarif_Detail.FieldToClass("NPERCENT_RETURN")
                            .nAmountReturn = lrecreaPolicy_Tarif_Detail.FieldToClass("NAMOUNT_RETURN")
                            mblnChargeArr = True
                        End With
                        .RNext()
                        lintCount = lintCount + 1
                    Loop
                    .RCloseRec()
                    ReDim Preserve arrPolicy_Tarif(lintCount - 1)
                End If
                If LoadTarifDetail Then
                    Me.nPolicy = nPolicy
                    Me.nBranch = nBranch
                    Me.nProduct = nProduct
                    Me.sCertype = sCertype
                    Me.nCertif = nCertif
                End If
            End With
        Else
            LoadTarifDetail = True
        End If

reaPolicy_Tarif_Detail_Err:
        If Err.Number Then
            LoadTarifDetail = False
        End If

        'UPGRADE_NOTE: Object lrecreaPolicy_Tarif_Detail may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecreaPolicy_Tarif_Detail = Nothing
        On Error GoTo 0

    End Function

End Class
