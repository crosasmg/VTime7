Option Strict Off
Option Explicit On
Option Compare Text
Public Class ProdLifeSeq
    '%-------------------------------------------------------%'
    '% $Workfile:: ProdLifeSeq.cls                          $%'
    '% $Author:: Gazuaje                                    $%'
    '% $Date:: 3/07/06 7:53p                                $%'
    '% $Revision:: 16                                       $%'
    '%-------------------------------------------------------%'

    '- Constante para el número posible de frames en la subsecuencia de características de vida.
    Private Const CN_FRAMESNUMPRODLIFESEQ As Integer = 18

    '- Se define la variable que contiene la imagen a asociar a la página en la secuencia
    Private mintPageImage As eFunctions.Sequence.etypeImageSequence

    '- Se define la constante para los codispl en la subsecuencia de cobertura (Cob. VIDA)
    Private Const CN_WINDOWSPRODLIFESEQ As String = "DP043A  DP043B  DP043D  DP020   DP026   DP025   DP024   DP021   MVI7002 DP044   DP047   DP7000  DP7001  DP607A  DP607C  DP607D  DP8005  DP8006  "

    '- Se definen las siguientes variables para el manejo de la ventana DP043A (Información de anticipos)
    Private mblnExistRou As Boolean
    Private mintAnlifint As Integer
    Private mintPayinter As Integer
    Private mdblInterest As Double
    Private mstrRouadvan As String

    Private mlngQmepLoans As Integer
    Private mlngQmmLoans As Integer
    Private mlngQmyLoans As Integer
    Private mdblAminLoans As Double
    Private mdblAmaxLoans As Double
    Private msglPervsLoans As Single
    Private msglPercTol As Single
    Private msglTaxes As Single
    Private mstrRouInterest As String
    Private mlngBill_item As Integer
    Private mlngOrigin_Loan As Integer

    Public nClaim_pres As Integer

    '- Se definen las siguientes variables para el manejo de la ventana DP014 (pagos de primas)
    Private mdblPayiniti As Double
    Private mdblAnnualap As Double
    Private mstrPeriodic As String
    Private mintDedufreq As Integer
    Private mstrPerunifa As String
    Private mdblPerrevfa As Double
    Private mdblPermulti As Double
    Private mdblPernunmi As Double
    Private mdblPernumai As Double
    Private mstrNoPeriod As String
    Private mstrNpeunifa As String
    Private mstrRevaltyp As String

    '- Objeto para obtener la información de product_li
    Public mclsProduct_li As Product

    '- Objeto para obtener la información de Durinsu_prod
    Public mcolDurinsu_prod As Durinsu_prods

    '- Objeto para obtener la información de Durpay_prod
    Public mcolDurpay_prod As Durpay_prods

    '- Objeto para validar si se busca información en la tabla Durinsu_prod
    Public bFindDurinsu As Boolean

    '- Objeto para validar si se busca información en la tabla Durinsu_prod
    Public bFindPayinsu As Boolean

    '- Objeto para validar que las ventanas de la subsecuencia tengan información
    Public bWithInformation As Boolean

    '- Variable para daber si se debe enviar mensaje al usuario
    Public mblnError As Boolean

    '- Numero de meses de valor-poliza negativo para cancelar la poliza [APV2] - ACM - 25/08/2003
    Public nQmonVPN As Integer


    '% LoadTabsLifeCover: Esta función es la encarga de carga la información necesaria para cada
    '%                    pestaña que será mostrada para coberturas de vida
    Public Function LoadTabsProdLifeSeq(ByVal bQuery As Boolean, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal dEffecdate As Date, ByVal sBrancht As String, Optional ByVal bOnlyValid As Boolean = False, Optional ByVal objProduct_li As Product = Nothing, Optional ByVal nModulec As Integer = 0) As String
        Dim lclsProduct_li As Product
        Dim lclsQuery As eRemoteDB.Query
        Dim lclsSequence As eFunctions.Sequence = New eFunctions.Sequence
        Dim lclsEffect_dats As Effect_dats
        Dim lclsFunds As Object
        Dim lclsSurr_retention As Object
        Dim lcolLoad_Surr As Load_surrs
        Dim lcolTab_ActiveLife As Tab_ActiveLife
        Dim lcolPlan_IntWar As Plan_IntWar
        Dim lcolTab_ord_origin As Object
        Dim lclsObject As Object
        Dim lcolSurr_percent As Surr_percents

        Dim llngCount As Integer
        Dim llngAux As Integer
        Dim lvntRequireField As Object
        Dim lstrHTMLCode As String = ""
        Dim lintAction As Integer
        Dim lblnValid As Boolean

        '-Se define la variable lstrCodispl en la cual se almacena el código de la ventana
        '-extraído de la constante cstrWindows

        Dim lstrCodispl As String

        On Error GoTo LoadTabsProdLifeSeq_err

        If objProduct_li Is Nothing Then
            lclsProduct_li = New Product
            lblnValid = lclsProduct_li.FindProduct_li(nBranch, nProduct, dEffecdate)
        Else
            lclsProduct_li = objProduct_li
            lblnValid = True
        End If
        lclsQuery = New eRemoteDB.Query
        If Not bOnlyValid Then
            lclsSequence = New eFunctions.Sequence
        End If

        If lblnValid Then
            If Not bOnlyValid Then
                lstrHTMLCode = lclsSequence.makeTable
            End If
            llngAux = 1
            lintAction = IIf(bQuery, eFunctions.Menues.TypeActions.clngActionQuery, eFunctions.Menues.TypeActions.clngActionUpdate)
            With lclsProduct_li
                For llngCount = 1 To CN_FRAMESNUMPRODLIFESEQ

                    '+ Se extrae el código de la ventana
                    lstrCodispl = Trim(Mid(CN_WINDOWSPRODLIFESEQ, llngAux, 8))
                    llngAux = llngAux + 8

                    '+ Si el tipo de producto NO es UNIT LINK
                    '+ [APV2] Inclusión de la ventana Reglas de capitalización (DP7001) DBLANCO 08-08-2003

                    If .nProdClas <> 4 And (lstrCodispl = "DP7000" Or lstrCodispl = "DP7001") Then
                        lblnValid = False
                    End If

                    '+ Si el tipo de producto es Convencional o Seguro de crédito
                    If (.nProdClas = 1 Or .nProdClas = 7) And (lstrCodispl = "DP021" Or lstrCodispl = "DP044" Or lstrCodispl = "DP047") Then
                        lblnValid = False
                    End If

                    If (.nProdClas = 1 Or .nProdClas = 5 Or .nProdClas = 7) And lstrCodispl = "DP024" Then
                        lblnValid = False
                    End If


                    If lblnValid Then
                        If Not bOnlyValid Then
                            Call lclsQuery.OpenQuery("Windows", "sCodisp, sCodispl, sShort_des", "sCodispl='" & lstrCodispl & "'")
                        End If

                        Select Case lstrCodispl

                            '+ Se obtiene por cada transacción un campo (requerido) de la misma para identificar
                            '+ si tiene o no contenido

                            '+ ahorros garantizados
                            Case "DP8005"
                                lclsObject = eRemoteDB.NetHelper.CreateClassInstance("eBranches.Guar_saving_prod")

                                'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
                                lvntRequireField = IIf(lclsObject.Find(nBranch, nProduct, dEffecdate), "1", System.DBNull.Value)

                                'UPGRADE_NOTE: Object lclsObject may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
                                lclsObject = Nothing


                                '+ Porcentaje de retención de rescate
                            Case "DP7000"
                                lclsSurr_retention = eRemoteDB.NetHelper.CreateClassInstance("eProduct.Surr_retentions")

                                'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
                                lvntRequireField = IIf(lclsSurr_retention.Find(nBranch, nProduct, dEffecdate), "1", System.DBNull.Value)

                                'UPGRADE_NOTE: Object lclsSurr_retention may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
                                lclsSurr_retention = Nothing

                                '+ Información de anticipos
                            Case "DP043A"
                                lvntRequireField = .sRouadvan
                                If lvntRequireField = String.Empty Then
                                    lvntRequireField = .nInterest
                                    If lvntRequireField = eRemoteDB.Constants.intNull Then
                                        lvntRequireField = .nAnlifint
                                        If lvntRequireField = eRemoteDB.Constants.intNull Then
                                            lvntRequireField = .nPayinter
                                        End If
                                    End If
                                End If

                                '+ Información de rescate
                            Case "DP043B"
                                lvntRequireField = .sRousurre

                                '+ Datos para fondos de inversiones
                            Case "DP021"
                                lvntRequireField = .nCurrency
                                If lvntRequireField = eRemoteDB.Constants.intNull Then
                                    lvntRequireField = .nUlfmaxqu
                                    If lvntRequireField = eRemoteDB.Constants.intNull Then
                                        lvntRequireField = .sUlfchani
                                        If lvntRequireField = String.Empty Then
                                            lvntRequireField = .nUlsmaxqu
                                            If lvntRequireField = eRemoteDB.Constants.intNull Then
                                                lvntRequireField = .nUlswiper
                                                'If lvntRequireField = String.Empty Then
                                                If lvntRequireField = eRemoteDB.Constants.intNull Then
                                                    lvntRequireField = .nUlsschar
                                                    If lvntRequireField = eRemoteDB.Constants.intNull Then
                                                        lvntRequireField = .nUlscharg
                                                        If lvntRequireField = eRemoteDB.Constants.intNull Then
                                                            lvntRequireField = .nUlrmaxqu
                                                            If lvntRequireField = eRemoteDB.Constants.intNull Then
                                                                lvntRequireField = .nUlredper
                                                                'If lvntRequireField = String.Empty Then
                                                                If lvntRequireField = eRemoteDB.Constants.intNull Then
                                                                    lvntRequireField = .nUlrschar
                                                                    If lvntRequireField = eRemoteDB.Constants.intNull Then
                                                                        lvntRequireField = .nUlrcharg
                                                                    End If
                                                                End If
                                                            End If
                                                        End If
                                                    End If
                                                End If
                                            End If
                                        End If
                                    End If
                                End If

                                '+ Saldado / Prorrogado
                            Case "DP043D"
                                lvntRequireField = .sRoureduc
                                If lvntRequireField = String.Empty Then
                                    lvntRequireField = .sRoureddc
                                End If

                                '+ Beneficios de inversiones
                            Case "DP020"
                                lvntRequireField = .nBenefitr
                                If lvntRequireField = eRemoteDB.Constants.intNull Then
                                    lvntRequireField = .nBenefapl
                                    If lvntRequireField = eRemoteDB.Constants.intNull Then
                                        lvntRequireField = .nBenefexc
                                        If lvntRequireField = eRemoteDB.Constants.intNull Then
                                            lvntRequireField = .nBenexcra
                                            If lvntRequireField = eRemoteDB.Constants.intNull Then
                                                lvntRequireField = .sBenres
                                            End If
                                        End If
                                    End If
                                End If

                                '+ Consideraciones especiales sobre edades
                            Case "DP026"
                                lvntRequireField = .nSuagemax
                                If lvntRequireField <> eRemoteDB.Constants.intNull Then
                                    lvntRequireField = .nSuagemin
                                    If lvntRequireField <> eRemoteDB.Constants.intNull Then
                                        lvntRequireField = .nReagemax
                                    End If
                                End If

                                '+ Opciones de pago de siniestros
                            Case "DP025"
                                lvntRequireField = .sClallpre
                                If lvntRequireField <> String.Empty Then
                                    lvntRequireField = .sClnoprei
                                    If lvntRequireField <> String.Empty Then
                                        lvntRequireField = .sClpaypri
                                        If lvntRequireField <> String.Empty Then
                                            lvntRequireField = .sClsimpai
                                            If lvntRequireField <> String.Empty Then
                                                lvntRequireField = .sClsurrei
                                                If lvntRequireField <> String.Empty Then
                                                    lvntRequireField = .sCltransi
                                                End If
                                            End If
                                        End If
                                    End If
                                End If

                                '+ Opciones de pago de primas

                            Case "DP024"
                                lvntRequireField = .nPayiniti

                                If lvntRequireField <> eRemoteDB.Constants.intNull Then
                                    lvntRequireField = .sPeriodic

                                    If lvntRequireField = "1" Then
                                        lvntRequireField = .sNpeunifa

                                        If lvntRequireField <> String.Empty Then
                                            lvntRequireField = .sPerunifa
                                            If lvntRequireField = "1" Then
                                                lvntRequireField = .nPermulti
                                            Else
                                                lvntRequireField = .nPernunmi
                                            End If
                                        End If
                                    Else
                                        lvntRequireField = .sNpeunifa

                                        If lvntRequireField = "1" Then
                                            lvntRequireField = .nNpemulti
                                        Else
                                            lvntRequireField = .nNpenunmi
                                        End If
                                    End If
                                End If

                                '+ Cuentas Origen asociados al producto
                            Case "MVI7002"
                                lcolTab_ord_origin = eRemoteDB.NetHelper.CreateClassInstance("eBranches.Tab_Ord_Origins")
                                '+ Se busca si se tiene alguna Cuenta Origen al producto
                                'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
                                lvntRequireField = IIf(lcolTab_ord_origin.Find(nBranch, nProduct), "1", System.DBNull.Value)

                                '+ Fondos asociados al plan
                            Case "DP044"
                                lclsFunds = eRemoteDB.NetHelper.CreateClassInstance("ePolicy.Funds")
                                '+ Se busca si se tiene algún fondo asociado al producto
                                'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
                                lvntRequireField = IIf(lclsFunds.Find(nBranch, nProduct, dEffecdate), "1", System.DBNull.Value)

                                '+ Fecha efectiva del aporte
                            Case "DP047"
                                lclsEffect_dats = New Effect_dats
                                '+ Se busca si se tiene alguna fecha asociada al producto
                                'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
                                lvntRequireField = IIf(lclsEffect_dats.Find(nBranch, nProduct, dEffecdate), "1", System.DBNull.Value)
                                '+ Condiciones generales de los planes
                            Case "DP607A"
                                lcolTab_ActiveLife = New Tab_ActiveLife
                                '+ Se busca si se tiene algun cargo asociada al producto
                                'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
                                lvntRequireField = IIf(lcolTab_ActiveLife.FindDP607A(nBranch, nProduct, dEffecdate), "1", System.DBNull.Value)
                                '+ Cargos por rescate
                            Case "DP607C"
                                lcolLoad_Surr = New Load_surrs
                                '+ Se busca si se tiene algun cargo asociada al producto
                                'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
                                lvntRequireField = IIf(lcolLoad_Surr.Find(nBranch, nProduct, nModulec, dEffecdate), "1", System.DBNull.Value)
                                '+ Rentabilidad por plan
                            Case "DP607D"
                                lcolPlan_IntWar = New Plan_IntWar
                                '+ Se busca si se tiene algun cargo asociada al producto
                                'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
                                lvntRequireField = IIf(lcolPlan_IntWar.Find_All(nBranch, nProduct, dEffecdate), "1", System.DBNull.Value)

                                '+ [APV2] Reglas de Capitalización DBLANCO 12-08-2003
                            Case "DP7001"
                                'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
                                lvntRequireField = IIf(.nSaving_pct >= 0, 1, System.DBNull.Value)

                                '+ Porcentajes de Valor Póliza permitido Rescatar
                            Case "DP8006"
                                lcolSurr_percent = New Surr_percents

                                'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
                                lvntRequireField = IIf(lcolSurr_percent.Find(nBranch, nProduct, dEffecdate), "1", System.DBNull.Value)

                                'UPGRADE_NOTE: Object lcolSurr_percent may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
                                lcolSurr_percent = Nothing

                            Case Else
                                'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
                                lvntRequireField = System.DBNull.Value
                        End Select

                        '+ Se asigna la imagen asociada a la página asociada al Codispl

                        If (IsNumeric(lvntRequireField) AndAlso lvntRequireField = eRemoteDB.Constants.intNull) Or
                            (TypeName(lvntRequireField) = "String" AndAlso lvntRequireField = String.Empty) Or
                            (TypeName(lvntRequireField) = "Date" AndAlso lvntRequireField = eRemoteDB.Constants.dtmNull) Or
                            IsDBNull(lvntRequireField) Then


                            'If lvntRequireField = eRemoteDB.Constants.intNull Or lvntRequireField = eRemoteDB.Constants.dtmNull Or lvntRequireField = String.Empty Then
                            If lstrCodispl = "DP025" Or lstrCodispl = "DP026" Or lstrCodispl = "DP024" Then
                                mintPageImage = eFunctions.Sequence.etypeImageSequence.eRequired
                            Else
                                mintPageImage = eFunctions.Sequence.etypeImageSequence.eEmpty
                            End If
                        Else
                            '+Ventanas con contenido
                            'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
                            If IsDBNull(lvntRequireField) Then
                                mintPageImage = eFunctions.Sequence.etypeImageSequence.eEmpty
                            Else
                                mintPageImage = eFunctions.Sequence.etypeImageSequence.eOK
                            End If
                        End If

                        If bOnlyValid Then
                            bWithInformation = Not mintPageImage = eFunctions.Sequence.etypeImageSequence.eRequired
                            If Not bWithInformation Then
                                Exit For
                            End If
                        Else
                            If Not bQuery Or (bQuery And mintPageImage = eFunctions.Sequence.etypeImageSequence.eOK) Then
                                lstrHTMLCode = lstrHTMLCode & lclsSequence.makeRow(lclsQuery.FieldToClass("sCodisp"), lstrCodispl, lintAction, lclsQuery.FieldToClass("sShort_des"), mintPageImage)
                            End If
                        End If
                    End If
                    lblnValid = True
                Next llngCount
            End With
            If Not bOnlyValid Then
                LoadTabsProdLifeSeq = lstrHTMLCode & lclsSequence.closeTable()
            End If
        End If

LoadTabsProdLifeSeq_err:
        If Err.Number Then
            LoadTabsProdLifeSeq = "LoadTabsProdLifeSeq: " & Err.Description
        End If

        'UPGRADE_NOTE: Object lclsQuery may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsQuery = Nothing
        'UPGRADE_NOTE: Object lclsSequence may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsSequence = Nothing
        'UPGRADE_NOTE: Object lclsFunds may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsFunds = Nothing
        'UPGRADE_NOTE: Object lclsEffect_dats may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsEffect_dats = Nothing
        'UPGRADE_NOTE: Object lclsSurr_retention may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsSurr_retention = Nothing
        'UPGRADE_NOTE: Object lclsProduct_li may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsProduct_li = Nothing
        'UPGRADE_NOTE: Object lcolTab_ActiveLife may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lcolTab_ActiveLife = Nothing
        'UPGRADE_NOTE: Object lcolPlan_IntWar may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lcolPlan_IntWar = Nothing
        'UPGRADE_NOTE: Object lcolTab_ord_origin may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lcolTab_ord_origin = Nothing

        On Error GoTo 0
    End Function

    '% insValDP043: Valida los campos de la página DP043 (Características de vida)
    Public Function insValDP043(ByVal sCodispl As String, ByVal nProdClas As Integer, ByVal nCurrency As Integer, ByVal sIdurvari As String, ByVal nIdurafix As Integer, ByVal sIPayvari As String, ByVal nPdurCount As Integer, ByVal nTypDurins As Integer, Optional ByVal nQmonVPN As Integer = 0) As String
        Dim lobjErrors As eFunctions.Errors

        On Error GoTo insValDP043_Err

        lobjErrors = New eFunctions.Errors

        '+ Validación del campo "Clase de producto"
        If nProdClas = eRemoteDB.Constants.intNull Or nProdClas = 0 Then
            Call lobjErrors.ErrorMessage("DP043", 11442)
        End If

        '+ Validación del campo "Moneda"
        If nCurrency = eRemoteDB.Constants.intNull Or nCurrency = 0 Then
            Call lobjErrors.ErrorMessage("DP043", 10827)
        End If

        '+ Validación del campo <Seguro-tiempo-fija-cantidad>
        If sIdurvari = "2" Then
            If nIdurafix = eRemoteDB.Constants.intNull Then
                Call lobjErrors.ErrorMessage("DP043", 11180)

                '+ Se valida que la edad indicada no sea mayor a 130
            ElseIf nIdurafix > 130 And nTypDurins = 2 Then
                Call lobjErrors.ErrorMessage("DP043", 11413)
            End If
        End If

        '+ Si se indicó que el tiempo de los pagos es fija, deben indicarse los tiempos
        If sIPayvari = "2" Then
            If nPdurCount = eRemoteDB.Constants.intNull Then
                Call lobjErrors.ErrorMessage("DP043", 11191)
            End If
        End If

        '+ Si el producto es UNIT LINKED (nProdClass = 4), el campo "nQMonVPN" debe estar lleno
        '+ APV2 - ACM - 25/08/2003
        If nProdClas = 4 And nQmonVPN <= 0 Then
            Call lobjErrors.ErrorMessage(sCodispl, 70181)
        End If

        insValDP043 = lobjErrors.Confirm

insValDP043_Err:
        If Err.Number Then
            insValDP043 = "insValDP043: " & Err.Description
        End If
        'UPGRADE_NOTE: Object lobjErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lobjErrors = Nothing
    End Function
    '% insPostDP043: Realiza el llamado a la rutina que actualiza la tabla Product_li,
    '%               para la página DP043 (Características de vida)
    Public Function insPostDP043(ByVal sCodispl As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal dEffecdate As Date, ByVal nUsercode As Integer, ByVal nProdClas As Integer, ByVal nCurrency As Integer, ByVal sMorcapii As String, ByVal sAssociai As String, ByVal sAssototal As String, ByVal sPremiumtype As String, ByVal nTypDurins As Integer, ByVal sIdurvari As String, ByVal nIdurafix As Integer, ByVal nMinrent As Double, ByVal nMaxrent As Double, ByVal nTypDurpay As Integer, ByVal sPdurvari As String, Optional ByVal sRoutine_C As String = "", Optional ByVal sRoutinsu As String = "", Optional ByVal sRoutpay As String = "", Optional ByVal nQmonVPN As Integer = 0, Optional ByVal sApv As String = "", Optional ByVal nBmg As Integer = 0, Optional ByVal sRoutinevpn As String = "", Optional ByVal sNo_Holidays As String = "") As Boolean
        Dim lclsProduct As eProduct.Product
        Dim lclsProd_win As eProduct.Prod_win

        On Error GoTo insPostDP043_Err

        lclsProduct = New eProduct.Product
        lclsProd_win = New eProduct.Prod_win

        insPostDP043 = True

        With lclsProduct
            If .FindProduct_li(nBranch, nProduct, dEffecdate) Then
                .nBranch = nBranch
                .nProduct = nProduct
                .dEffecdate = dEffecdate
                .nUsercode = nUsercode
                .nCurrency = nCurrency
                .nProdClas = nProdClas
                .sMorcapii = sMorcapii
                .nMinrent = nMinrent
                .nMaxrent = nMaxrent
                .sAssociai = IIf(sAssociai = "1", "1", "2")
                .sAssototal = IIf(sAssociai = String.Empty, String.Empty, IIf(sAssototal = "1", "1", "2"))
                .sPremiumtype = IIf(sPremiumtype = "1", "1", "2")
                .nTypdurins = nTypDurins
                .nTypdurpay = nTypDurpay
                .sPdurvari = sPdurvari

                If nTypDurins = 5 Or nTypDurins = 6 Then
                    .sIdurvari = String.Empty
                    .nIdurafix = eRemoteDB.Constants.intNull
                Else
                    If sIdurvari = "1" Then
                        .sIdurvari = "1"
                        .nIdurafix = eRemoteDB.Constants.intNull
                    Else
                        .sIdurvari = "2"
                        .nIdurafix = nIdurafix
                    End If
                End If
                .sRoutine_C = UCase(sRoutine_C)
                .sRoutinsu = UCase(sRoutinsu)
                .sRoutpay = UCase(sRoutpay)
                .nQmonVPN = nQmonVPN
                .sApv = IIf(sApv = "1", "1", "2")
                .nBmg = nBmg
                .sRoutinevpn = UCase(sRoutinevpn)
                .sNo_Holidays = sNo_Holidays

                insPostDP043 = .insProdLifeSeq

                If insPostDP043 Then
                    '+ Se actualiza la secuencia de ventana del producto con la transacción enviada como parámetro
                    Call lclsProd_win.Add_Prod_win(nBranch, nProduct, dEffecdate, "DP043", "2", nUsercode)
                End If
            End If
        End With

insPostDP043_Err:
        If Err.Number Then
            insPostDP043 = False
        End If

        'UPGRADE_NOTE: Object lclsProd_win may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsProd_win = Nothing
        'UPGRADE_NOTE: Object lclsProduct may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsProduct = Nothing
    End Function

    '% insPreDP043: se valida la carga de los datos en la página
    Public Function insPreDP043(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal dEffecdate As Date, Optional ByVal sReloadAction As String = "", Optional ByVal nProdClas As Integer = 0, Optional ByVal nCurrency As Integer = 0, Optional ByVal sMorcapii As String = "", Optional ByVal nMinrent As Double = 0, Optional ByVal sRoutine_C As String = "", Optional ByVal nMaxrent As Double = 0, Optional ByVal sAssociai As String = "", Optional ByVal nTypDurins As Integer = 0, Optional ByVal nTypDurpay As Integer = 0, Optional ByVal sRoutinsu As String = "", Optional ByVal sAssototal As String = "", Optional ByVal sPremiumtype As String = "", Optional ByVal sIdurvari As String = "", Optional ByVal sPdurvari As String = "", Optional ByVal bWithInformation As Boolean = False, Optional ByVal sAntIdurvari As String = "", Optional ByVal sRoutpay As String = "", Optional ByVal sApv As String = "", Optional ByVal sNo_Holidays As String = "") As Boolean
        Dim lclsDurinsu_prod As Durinsu_prod
        Dim lclsDurpay_prod As Durpay_prod

        On Error GoTo insPreDP043_Err

        If mclsProduct_li Is Nothing Then
            mclsProduct_li = New Product
        End If

        '+ Si no se está recargando la página
        With mclsProduct_li
            If sReloadAction = String.Empty Then
                insPreDP043 = .FindProduct_li(nBranch, nProduct, dEffecdate)
                If insPreDP043 Then
                    Call LoadTabsProdLifeSeq(True, nBranch, nProduct, dEffecdate, CStr(mclsProduct_li.sBrancht), True, mclsProduct_li)
                End If
            Else
                insPreDP043 = True
                .nProdClas = nProdClas
                .nCurrency = nCurrency
                .sMorcapii = sMorcapii
                .nMinrent = nMinrent
                .nMaxrent = nMaxrent
                .sRoutine_C = sRoutine_C
                .sAssociai = sAssociai
                .nTypdurins = nTypDurins
                .sRoutinsu = sRoutinsu
                .sAssototal = sAssototal
                .sPremiumtype = sPremiumtype
                .sIdurvari = sIdurvari
                .nTypdurpay = nTypDurpay
                .sPdurvari = sPdurvari
                .sRoutpay = sRoutpay
                .sApv = sApv
                .sNo_Holidays = sNo_Holidays

                Me.bWithInformation = bWithInformation
            End If

            '+ Si el tipo de duración es 1-Edad alcanzada, 2-Años,  7-Años/Edad alcanzada, 8-Meses, 9-Dias
            If .nTypdurins = 1 Or .nTypdurins = 2 Or .nTypdurins = 8 Or .nTypdurins = 9 Or .nTypdurins = 7 Then
                '+ Si la duración del seguro es fija se habilita el grid de duracion
                bFindDurinsu = .sIdurvari = "2"
            End If

            '+ Si el tipo de duración de los pagos es 1-Edad alcanzada, 2-Años,  7-Años/Edad alcanzada, 8-Meses, 9-Dias
            If .nTypdurpay = 1 Or .nTypdurpay = 2 Or .nTypdurpay = 8 Or .nTypdurpay = 9 Or .nTypdurpay = 7 Then
                '+ Si la duración de los pagos del seguro es fija se habilita el grid de pagos
                bFindPayinsu = .sPdurvari = "2"
            End If
        End With

        If bFindDurinsu Then
            '+ Se obtiene la información sobre la duración del seguro
            If mcolDurinsu_prod Is Nothing Then
                mcolDurinsu_prod = New Durinsu_prods
            End If
            Call mcolDurinsu_prod.Find(nBranch, nProduct, dEffecdate)
        Else
            If sAntIdurvari <> sIdurvari Then
                '+ Si se tenía duración del seguro fija, y se cambia a variable se elimina la información
                '+ sobre la duración y pagos del seguro
                If sAntIdurvari = "2" And sIdurvari = "1" Then
                    lclsDurinsu_prod = New Durinsu_prod
                    Call lclsDurinsu_prod.DeleteAll(nBranch, nProduct, dEffecdate)
                End If
            End If
        End If

        If bFindPayinsu Then
            '+ Se obtiene la información sobre la duración de los pagos
            If mcolDurpay_prod Is Nothing Then
                mcolDurpay_prod = New Durpay_prods
            End If
            mcolDurpay_prod.Find(nBranch, nProduct, dEffecdate)
        Else
            '+ Se elimina la información sobre la duración de los pagos
            lclsDurpay_prod = New Durpay_prod
            Call lclsDurpay_prod.DeleteAll(nBranch, nProduct, dEffecdate)
        End If

        '+ Si se tenía duración del seguro variable, y se cambia a fija se envía mensaje nro. 55972
        If sAntIdurvari = "1" And sIdurvari = "2" Then
            mblnError = True
        End If

insPreDP043_Err:
        If Err.Number Then
            insPreDP043 = False
        End If
        'UPGRADE_NOTE: Object lclsDurpay_prod may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsDurpay_prod = Nothing
        'UPGRADE_NOTE: Object lclsDurinsu_prod may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsDurinsu_prod = Nothing
        On Error GoTo 0
    End Function

    '% insPreDP043A:Permite obtener la información de necesaria para el manejo de la ventana
    Public Function insPreDP043A(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal dEffecdate As Date) As Boolean
        Dim lclsProduct As Product

        lclsProduct = New Product

        On Error GoTo insPreDP043A_Err

        With lclsProduct
            If .FindProduct_li(nBranch, nProduct, dEffecdate) Then
                mblnExistRou = IIf(.sRouadvan <> String.Empty, True, False)
                mstrRouadvan = .sRouadvan
                mintAnlifint = .nAnlifint
                mintPayinter = .nPayinter
                mdblInterest = .nInterest

                mlngQmepLoans = .nQmeploans
                mlngQmmLoans = .nQmmloans
                mlngQmyLoans = .nQmyloans
                mdblAminLoans = .nAminloans
                mdblAmaxLoans = .nAmaxloans
                msglPervsLoans = .nPervsloans
                msglPercTol = .nPerctol
                msglTaxes = .nTaxes
                mstrRouInterest = .sRouinterest
                mlngBill_item = .nBill_item
                mlngOrigin_Loan = .nOrigin_loan

                insPreDP043A = True
            End If
        End With

        'UPGRADE_NOTE: Object lclsProduct may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsProduct = Nothing

insPreDP043A_Err:
        If Err.Number Then
            insPreDP043A = False
        End If
        On Error GoTo 0
    End Function

    '%DefaultValueDP043A:Esta función se encarga de realizar la habilitación o des-habilitación de los
    '%campos de la ventana DP043A.
    Public Function DefaultValueDP043A(ByVal sField As String) As Object
        Dim lstrReturnValue As Object = New Object

        Select Case sField

            Case "tctRouAdvan"
                lstrReturnValue = mstrRouadvan

            Case "cbeAnlifint.disabled", "cbePayInter.disabled", "cbePayInter.disabled", "tcnInterest.disabled", "tcnQMEPLoans.disabled", "tcnQMMLoans.disabled", "tcnQMYLoans.disabled", "tcnAMinLoans.disabled", "tcnAMaxLoans.disabled", "tcnPerVSLoans.disabled", "tcnPercTol.disabled", "tcnTaxes.disabled", "tctRouInterest.disabled", "cbeBill_item.disabled"

                lstrReturnValue = Not (mblnExistRou)

            Case "cbeAnlifint"
                lstrReturnValue = IIf(mstrRouadvan <> String.Empty, mintAnlifint, String.Empty)

            Case "cbePayInter"
                lstrReturnValue = IIf(mstrRouadvan <> String.Empty, mintPayinter, String.Empty)

            Case "tcnInterest"
                lstrReturnValue = mdblInterest

            Case "tcnQMEPLoans"
                lstrReturnValue = mlngQmepLoans

            Case "tcnQMMLoans"
                lstrReturnValue = mlngQmmLoans

            Case "tcnQMYLoans"
                lstrReturnValue = mlngQmyLoans

            Case "tcnAMinLoans"
                lstrReturnValue = mdblAminLoans

            Case "tcnAMaxLoans"
                lstrReturnValue = mdblAmaxLoans

            Case "tcnPerVSLoans"
                lstrReturnValue = msglPervsLoans

            Case "tcnPercTol"
                lstrReturnValue = msglPercTol

            Case "tcnTaxes"
                lstrReturnValue = msglTaxes

            Case "tctRouInterest"
                lstrReturnValue = mstrRouInterest

            Case "cbeBill_item"
                lstrReturnValue = mlngBill_item
            Case "cbeOrigin_Loan"
                lstrReturnValue = mlngOrigin_Loan
        End Select

        DefaultValueDP043A = lstrReturnValue
    End Function

    '%insValDP043A: Verifica los datos del frame de Información de anticipos
    Public Function insValDP043A(ByVal sCodispl As String, ByVal sRouadvan As String, ByVal nAnlifint As Integer, ByVal nPayinter As Integer, ByVal nQmeploans As Integer, ByVal nQmmloans As Integer, ByVal nQmyloans As Integer, ByVal nAminloans As Double, ByVal nAmaxloans As Double, ByVal nPervsloans As Single, ByVal nPerctol As Single, ByVal nTaxes As Single, ByVal sRouinterest As String, ByVal nBill_item As Integer) As String
        Dim lclsErrors As eFunctions.Errors

        lclsErrors = New eFunctions.Errors

        On Error GoTo insValDP043A_Err

        If sRouadvan <> String.Empty Then

            '+ Validación del campo <Intereses-tipo>
            If nAnlifint = eRemoteDB.Constants.intNull Then
                Call lclsErrors.ErrorMessage(sCodispl, 1012,  , eFunctions.Errors.TextAlign.LeftAling, "Anticipos - Tipo de intereses:")
            End If

            '+ Validación del campo <Intereses-cobro en>
            If nPayinter = eRemoteDB.Constants.intNull Or nPayinter = 0 Then
                Call lclsErrors.ErrorMessage(sCodispl, 1012,  , eFunctions.Errors.TextAlign.LeftAling, "Anticipos - Cobro en:")
            End If

            If nQmeploans = eRemoteDB.Constants.intNull Then
                Call lclsErrors.ErrorMessage(sCodispl, 60143,  , eFunctions.Errors.TextAlign.LeftAling)
            End If

            If nQmmloans = eRemoteDB.Constants.intNull Then
                Call lclsErrors.ErrorMessage(sCodispl, 60144,  , eFunctions.Errors.TextAlign.LeftAling)
            End If

            If nQmyloans = eRemoteDB.Constants.intNull Then
                Call lclsErrors.ErrorMessage(sCodispl, 60145,  , eFunctions.Errors.TextAlign.LeftAling)
            End If

            If nAminloans = eRemoteDB.Constants.intNull Then
                Call lclsErrors.ErrorMessage(sCodispl, 60146,  , eFunctions.Errors.TextAlign.LeftAling)
            End If

            If nAmaxloans = eRemoteDB.Constants.intNull Then
                Call lclsErrors.ErrorMessage(sCodispl, 60147,  , eFunctions.Errors.TextAlign.LeftAling)
            End If

            If nPervsloans = eRemoteDB.Constants.intNull Then
                Call lclsErrors.ErrorMessage(sCodispl, 60148,  , eFunctions.Errors.TextAlign.LeftAling)
            End If

            If nTaxes = eRemoteDB.Constants.intNull Then
                Call lclsErrors.ErrorMessage(sCodispl, 60149,  , eFunctions.Errors.TextAlign.LeftAling)
            End If

            If sRouinterest = String.Empty Then
                Call lclsErrors.ErrorMessage(sCodispl, 60150,  , eFunctions.Errors.TextAlign.LeftAling)
            End If

            If nBill_item = eRemoteDB.Constants.intNull Then
                Call lclsErrors.ErrorMessage(sCodispl, 60151,  , eFunctions.Errors.TextAlign.LeftAling)
            End If

        End If

        insValDP043A = lclsErrors.Confirm

insValDP043A_Err:
        If Err.Number Then
            insValDP043A = "insValDP043A: " & Err.Description
        End If
        On Error GoTo 0
        'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsErrors = Nothing
    End Function

    '% insPostDP043A: En esta rutina se realiza la asignación de los valores de
    '%                la ventana a las propiedades correspondientes del método que
    '%                realiza el mantenimiento de la historia en la estructura 'Life_cover'.
    Public Function insPostDP043A(ByVal sCodispl As String, ByVal sRouadvan As String, ByVal nInterest As Double, ByVal nAnlifint As Integer, ByVal nPayinter As Integer, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal dEffecdate As Date, ByVal nUsercode As Integer, ByVal nQmeploans As Integer, ByVal nQmmloans As Integer, ByVal nQmyloans As Integer, ByVal nAminloans As Double, ByVal nAmaxloans As Double, ByVal nPervsloans As Single, ByVal nPerctol As Single, ByVal nTaxes As Single, ByVal sRouinterest As String, ByVal nBill_item As Integer, ByVal nOrigin_loan As Integer) As Boolean
        Dim lclsProduct As Product
        Dim lclsProd_win As Prod_win

        lclsProduct = New Product
        lclsProd_win = New Prod_win

        On Error GoTo insPostDP043A_Err

        With lclsProduct
            If .FindProduct_li(nBranch, nProduct, dEffecdate) Then
                .sRouadvan = sRouadvan
                .nInterest = nInterest
                .nAnlifint = nAnlifint
                .nPayinter = nPayinter
                .nUsercode = nUsercode
                .dEffecdate = dEffecdate

                .nQmeploans = nQmeploans
                .nQmmloans = nQmmloans
                .nQmyloans = nQmyloans
                .nAminloans = nAminloans
                .nAmaxloans = nAmaxloans
                .nPervsloans = nPervsloans
                .nPerctol = nPerctol
                .nTaxes = nTaxes
                .sRouinterest = sRouinterest
                .nBill_item = nBill_item
                .nOrigin_loan = nOrigin_loan

                If .insProdLifeSeq Then
                    Call lclsProd_win.Add_Prod_win(nBranch, nProduct, dEffecdate, sCodispl, "2", nUsercode)
                    insPostDP043A = True
                End If
            End If
        End With

insPostDP043A_Err:
        If Err.Number Then
            insPostDP043A = False
        End If
        On Error GoTo 0
        'UPGRADE_NOTE: Object lclsProduct may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsProduct = Nothing
        'UPGRADE_NOTE: Object lclsProd_win may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsProd_win = Nothing
    End Function

    '% insValDP026: Verifica los datos del frame consideraciones sobre edades
    Public Function insValDP026(ByVal sCodispl As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal dEffecdate As Date, ByVal nSuagemin As Integer, ByVal nSuagemax As Integer, ByVal nReagemax As Integer, ByVal nYearminw As Integer, ByVal nYearmors As Integer, ByVal nYearmins As Integer, ByVal nSmoke As Double, ByVal nNSmoke As Double) As String
        Dim lclsProduct_li As eProduct.Product
        Dim lclsErrors As eFunctions.Errors

        On Error GoTo insValDP026_Err

        lclsProduct_li = New eProduct.Product
        lclsErrors = New eFunctions.Errors

        With lclsErrors
            Call lclsProduct_li.FindProduct_li(nBranch, nProduct, dEffecdate)

            '+ Validación del campo <Contratación - mínimo>
            If nSuagemin = eRemoteDB.Constants.intNull Then
                Call lclsErrors.ErrorMessage("DP026", 11445)
            Else
                If nSuagemin > 130 Then
                    Call lclsErrors.ErrorMessage("DP026", 11413,  , eFunctions.Errors.TextAlign.LeftAling, "Edad mínima:")
                Else
                    With lclsProduct_li
                        If .nTypdurins = 1 And .sIdurvari = "2" And nSuagemin >= .nIdurafix Then
                            Call lclsErrors.ErrorMessage("DP026", 11262)
                        End If
                    End With
                End If
            End If

            '+ Validación del campo <Contratación - máximo>
            If nSuagemax = eRemoteDB.Constants.intNull Then
                Call lclsErrors.ErrorMessage("DP026", 11444)
            ElseIf nSuagemax <= nSuagemin Then
                Call lclsErrors.ErrorMessage("DP026", 11145,  , eFunctions.Errors.TextAlign.LeftAling, "Edad máxima:")
            ElseIf nSuagemax > 130 Then
                Call lclsErrors.ErrorMessage("DP026", 11413,  , eFunctions.Errors.TextAlign.LeftAling, "Edad máxima:")
            End If

            With lclsProduct_li
                If .nTypdurins = 1 And .sIdurvari = "2" And nSuagemax >= .nIdurafix Then
                    Call lclsErrors.ErrorMessage("DP026", 11263)
                End If
            End With

            '+ Validación del campo <Renovación>
            If nReagemax = eRemoteDB.Constants.intNull Then
                Call lclsErrors.ErrorMessage("DP026", 11443)
            Else
                If nReagemax <= nSuagemin Then
                    Call lclsErrors.ErrorMessage("DP026", 11145,  , eFunctions.Errors.TextAlign.LeftAling, "Renovación:")
                Else
                    If nReagemax > 130 Then
                        Call lclsErrors.ErrorMessage("DP026", 11413,  , eFunctions.Errors.TextAlign.LeftAling, "Renovación:")
                    End If
                End If

                With lclsProduct_li
                    If .nTypdurins = 1 And .sIdurvari = "2" And nReagemax >= .nIdurafix Then
                        Call lclsErrors.ErrorMessage("DP026", 11264)
                    End If
                End With
            End If

            '+ Validación del campo <Años a restar a mujeres>
            If nYearminw > 130 Then
                Call lclsErrors.ErrorMessage("DP026", 11413,  , eFunctions.Errors.TextAlign.LeftAling, "Años a restar a mujeres:")
            End If

            '+ Validación del campo <Años a sumar a fumadores>
            If nYearmors > 130 Then
                Call lclsErrors.ErrorMessage("DP026", 11413,  , eFunctions.Errors.TextAlign.LeftAling, "Años a sumar a fumadores:")
            End If

            '+ Validación del campo <Años a restar a no fumadores>
            If nYearmins > 130 Then
                Call lclsErrors.ErrorMessage("DP026", 11413,  , eFunctions.Errors.TextAlign.LeftAling, "Años a restar a no fumadores:")
            End If

            '+ Validación del campo de fumadores
            '+ Si se indico coeficiente a aplicar a fumadores, el campo
            '+ Años a sumar a fumadores debe estar vacio
            If nSmoke <> eRemoteDB.Constants.intNull And nYearmors <> eRemoteDB.Constants.intNull Then
                Call lclsErrors.ErrorMessage("DP026", 11418)
            End If

            '+ Validación del campo de NO fumadores
            '+ Si se indico coeficiente a aplicar a NO fumadores, el campo
            '+ Años a restar a no fumadores debe estar vacio
            If nNSmoke <> eRemoteDB.Constants.intNull And nYearmins <> eRemoteDB.Constants.intNull Then
                Call lclsErrors.ErrorMessage("DP026", 11419)
            End If

            insValDP026 = .Confirm
        End With

insValDP026_Err:
        If Err.Number Then
            insValDP026 = "insValDP026: " & Err.Description
        End If
        On Error GoTo 0
        'UPGRADE_NOTE: Object lclsProduct_li may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsProduct_li = Nothing
        'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsErrors = Nothing
    End Function

    '% insPostDP026: Realiza el llamado a la rutina que actualiza la tabla Product_li (Edades)
    Public Function insPostDP026(ByVal sCodispl As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal dEffecdate As Date, ByVal nUsercode As Integer, ByVal nSuagemin As Integer, ByVal nSuagemax As Integer, ByVal nReagemax As Integer, ByVal nYearminw As Integer, ByVal nYearmors As Integer, ByVal nYearmins As Integer, ByVal nSmoke As Double, ByVal nNSmoke As Double) As Boolean
        Dim lclsProduct As eProduct.Product

        On Error GoTo insPostDP026_Err

        lclsProduct = New eProduct.Product

        insPostDP026 = True

        With lclsProduct
            If .FindProduct_li(nBranch, nProduct, dEffecdate) Then
                .dEffecdate = dEffecdate
                .nBranch = nBranch
                .nProduct = nProduct
                .nUsercode = nUsercode
                .nSuagemin = nSuagemin
                .nSuagemax = nSuagemax
                .nReagemax = nReagemax
                .nYearminw = nYearminw
                .nYearmors = nYearmors
                .nYearmins = nYearmins
                .nTaxsmoke = nSmoke
                .nTaxnsmoke = nNSmoke

                insPostDP026 = .insProdLifeSeq
            End If
        End With

insPostDP026_Err:
        If Err.Number Then
            insPostDP026 = False
        End If
        On Error GoTo 0
        'UPGRADE_NOTE: Object lclsProduct may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsProduct = Nothing
    End Function

    '%insValDP025: Rutina de validación del frame 'DP025'
    Public Function insValDP025(ByVal nClaim_pres As Integer, ByVal nClsimpai As Integer, ByVal nClnoprei As Integer, ByVal nClsurrei As Integer, ByVal nClallpre As Integer, ByVal nClpaypri As Integer, ByVal nCltransi As Integer, ByVal sClannpei As String, ByVal sCllifeai As String, ByVal nClaim_Notice As Integer, ByVal nClaim_Pay As Integer) As String

        Dim lclsErrors As eFunctions.Errors
        On Error GoTo insValDP025_err
        lclsErrors = New eFunctions.Errors

        '+ Validación de la fecha de prescripción
        If nClaim_pres <> eRemoteDB.Constants.intNull Then
            If nClaim_pres < 0 Or nClaim_pres > 365 Then
                Call lclsErrors.ErrorMessage("DP025", 11333,  , eFunctions.Errors.TextAlign.LeftAling, "Entrega max. documentos:")
            End If
        End If

        '+ Validación de dias de denuncio
        If nClaim_Notice <> eRemoteDB.Constants.intNull Then
            If nClaim_Notice <= 0 Then
                Call lclsErrors.ErrorMessage("DP025", 55726)
            End If
        End If

        '+ Validación de dias de plazo para liquidar
        If nClaim_Pay <> eRemoteDB.Constants.intNull Then
            If nClaim_Pay <= 0 Then
                Call lclsErrors.ErrorMessage("DP025", 55844)
            End If
        End If

        '+ Validación del frame <tipos permitidos>
        If nClsimpai = eRemoteDB.Constants.intNull And nClnoprei = eRemoteDB.Constants.intNull And nClsurrei = eRemoteDB.Constants.intNull And nClallpre = eRemoteDB.Constants.intNull And nClpaypri = eRemoteDB.Constants.intNull And nCltransi = eRemoteDB.Constants.intNull And sClannpei = String.Empty And sCllifeai = String.Empty Then
            Call lclsErrors.ErrorMessage("DP025", 11406)
        End If

        insValDP025 = lclsErrors.Confirm

insValDP025_err:
        If Err.Number Then
            insValDP025 = insValDP025 & Err.Description
        End If
        On Error GoTo 0
        'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsErrors = Nothing
    End Function

    '% insPostDP025: se actualizan los datos de la ventana
    Public Function insPostDP025(ByVal sCodispl As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal dEffecdate As Date, ByVal nClsimpai As Integer, ByVal nClnoprei As Integer, ByVal nClsurrei As Integer, ByVal nClallpre As Integer, ByVal nClpaypri As Integer, ByVal nCltransi As Integer, ByVal sCllifeai As String, ByVal sClannpei As String, ByVal nClaim_pres As Integer, ByVal nUsercode As Integer, ByVal nClaim_Notice As Integer, ByVal nClaim_Pay As Integer, ByVal nIndCl_Pay As Integer) As Boolean
        Dim lclsProduct As Product
        Dim lclsProd_win As Prod_win

        On Error GoTo insPostDP025_Err

        lclsProduct = New Product
        lclsProd_win = New Prod_win

        With lclsProduct

            '+ Si cambió el valor del número de días para la prescripción, se actualiza la tabla product
            '+ o los otros dias (nClaim_Notice, nClaim_Pay)
            If .Find(nBranch, nProduct, dEffecdate) Then

                '            nClaim_pres = .nClaim_pres

                .nUsercode = nUsercode

                If nClaim_pres <> .nClaim_pres Or nClaim_Notice <> .nClaim_Notice Or nClaim_Pay <> .nClaim_Pay Then
                    .nClaim_pres = nClaim_pres
                    .nClaim_Notice = nClaim_Notice
                    .nClaim_Pay = nClaim_Pay
                    Call .UpdateProduct()
                End If
                Call lclsProd_win.Add_Prod_win(nBranch, nProduct, dEffecdate, "DP043", "2", nUsercode)
            End If

            If .FindProduct_li(nBranch, nProduct, dEffecdate) Then


                '-  Pagos simples
                If nClsimpai = eRemoteDB.Constants.intNull Then
                    .sClsimpai = "2"
                Else
                    .sClsimpai = CStr(nClsimpai)
                End If

                '- Liberación de primas
                If nClnoprei = eRemoteDB.Constants.intNull Then
                    .sClnoprei = "2"
                Else
                    .sClnoprei = CStr(nClnoprei)
                End If

                '- Valor de rescate
                If nClsurrei = eRemoteDB.Constants.intNull Then
                    .sClsurrei = "2"
                Else
                    .sClsurrei = CStr(nClsurrei)
                End If

                '- Devolución de todas las primas
                If nClallpre = eRemoteDB.Constants.intNull Then
                    .sClallpre = "2"
                Else
                    .sClallpre = CStr(nClallpre)
                End If

                '-Indicador de de monto minimo, para aprobacion de ordenes de pago de siniestros
                If nIndCl_Pay = eRemoteDB.Constants.intNull Then
                    .sIndCl_Pay = "2"
                Else
                    .sIndCl_Pay = CStr(nIndCl_Pay)
                End If

                '- Devolución de primas pagadas
                If nClpaypri = eRemoteDB.Constants.intNull Then
                    .sClpaypri = "2"
                Else
                    .sClpaypri = CStr(nClpaypri)
                End If

                '- Transferencia de bonos a los beneficiarios
                If nCltransi = eRemoteDB.Constants.intNull Then
                    .sCltransi = "2"
                Else
                    .sCltransi = CStr(nCltransi)
                End If

                .sCllifeai = sCllifeai
                .sClannpei = sClannpei

                '- Transferencia de bonos a los beneficiarios
                .nClaim_pres = nClaim_pres

                .dEffecdate = dEffecdate

                If .insProdLifeSeq Then
                    Call lclsProd_win.Add_Prod_win(nBranch, nProduct, dEffecdate, sCodispl, "2", nUsercode)
                    insPostDP025 = True
                End If
            End If
        End With

insPostDP025_Err:
        If Err.Number Then
            insPostDP025 = False
        End If
        On Error GoTo 0
        'UPGRADE_NOTE: Object lclsProduct may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsProduct = Nothing
        'UPGRADE_NOTE: Object lclsProd_win may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsProd_win = Nothing
    End Function

    '% insValDP043B: Verifica los datos del frame de Información de Rescates
    Public Function insValDP043B(ByVal sCodispl As String, Optional ByVal sRousurre As String = "", Optional ByVal sSurrenpi As String = "", Optional ByVal sSurrenti As String = "", Optional ByVal nSurrfreq As Double = 0, Optional ByVal nQmepsurr As Integer = 0, Optional ByVal nQmmsurr As Integer = 0, Optional ByVal nQmysurr As Integer = 0, Optional ByVal nAminsurr As Double = 0, Optional ByVal nAmaxsurr As Double = 0, Optional ByVal nPervssurr As Double = 0, Optional ByVal nCapminsurr As Double = 0) As String
        Dim lclsErrors As eFunctions.Errors

        On Error GoTo insValDP043B_Err

        lclsErrors = New eFunctions.Errors

        sSurrenpi = IIf(sSurrenpi = String.Empty, "2", sSurrenpi)
        sSurrenti = IIf(sSurrenti = String.Empty, "2", sSurrenti)

        '+ Validación del campo "Rescates parciales-frecuencia"
        If Trim(sRousurre) <> String.Empty Then
            If sSurrenpi = "1" Then
                If nSurrfreq = 0 Or nSurrfreq = eRemoteDB.Constants.intNull Then
                    Call lclsErrors.ErrorMessage(sCodispl, 1012,  , eFunctions.Errors.TextAlign.LeftAling, "Frecuencia:")
                End If

                '+ Validación de rescates parciales
                '+ Máximo por mes
                If nQmmsurr <= 0 Then
                    Call lclsErrors.ErrorMessage(sCodispl, 60153)
                End If

                '+ Máximo por año
                If nQmysurr <= 0 Then
                    Call lclsErrors.ErrorMessage(sCodispl, 60154)
                End If

                '+ Mínimo de rescate
                If nAminsurr <= 0 Then
                    Call lclsErrors.ErrorMessage(sCodispl, 60155)
                End If

                '+ Máximo de rescate
                If nAmaxsurr <= 0 Then
                    Call lclsErrors.ErrorMessage(sCodispl, 60156)
                End If

                '+ Capital mínimo
                If nCapminsurr <= 0 Then
                    Call lclsErrors.ErrorMessage(sCodispl, 60157)
                End If

                '+ (%) sobre el valor del rescate
                If nPervssurr <= 0 Then
                    Call lclsErrors.ErrorMessage(sCodispl, 60148)
                End If

                '+ Validación de los tipos de rescates permitidos
            ElseIf sSurrenti = "2" Then
                Call lclsErrors.ErrorMessage(sCodispl, 11057)
            End If

            '+ Validación de los meses mínimos de vigencia.
            If nQmepsurr = eRemoteDB.Constants.intNull Then
                Call lclsErrors.ErrorMessage(sCodispl, 60143)
            End If
        End If

        insValDP043B = lclsErrors.Confirm

        'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsErrors = Nothing

insValDP043B_Err:
        If Err.Number Then
            insValDP043B = insValDP043B & Err.Description
        End If
        On Error GoTo 0
    End Function

    '% insPostDP043B: Valida los datos introducidos en la zona de contenido para "frame" especifico
    Public Function insPostDP043B(ByVal sCodispl As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal dEffecdate As Date, ByVal nUsercode As Integer, ByVal sRousurre As String, ByVal sSurrenpi As String, ByVal sSurrenti As String, ByVal nSurrfreq As Double, ByVal nSurcashv As Double, ByVal nCharge As Double, ByVal nChargeamo As Double, ByVal nQmepsurr As Integer, ByVal nQmmsurr As Integer, ByVal nQmysurr As Integer, ByVal nAminsurr As Double, ByVal nAmaxsurr As Double, ByVal nPervssurr As Double, ByVal nCapminsurr As Double, ByVal nMaxchargsurr As Double, ByVal nOrigin_surr As Integer, ByVal sRoutineSurr As String, ByVal sApplyRouSurr As String, ByVal nQMMPSurr As Integer, ByVal nBalminsurr As Double) As Boolean
        Dim lclsProduct As eProduct.Product
        Dim lclsProd_win As eProduct.Prod_win

        On Error GoTo insPostDP043B_Err

        lclsProduct = New eProduct.Product
        lclsProd_win = New eProduct.Prod_win

        With lclsProduct
            If .FindProduct_li(nBranch, nProduct, dEffecdate) Then
                .sRousurre = sRousurre
                .sSurrenpi = IIf(sSurrenpi = String.Empty, "2", sSurrenpi)
                .sSurrenti = IIf(sSurrenti = String.Empty, "2", sSurrenti)
                .nSurrfreq = nSurrfreq
                .nSurcashv = IIf(nSurcashv = eRemoteDB.Constants.intNull, 0, nSurcashv)
                .nCharge = IIf(nCharge = eRemoteDB.Constants.intNull, 0, nCharge)
                .nChargeamo = IIf(nChargeamo = eRemoteDB.Constants.intNull, 0, nChargeamo)
                .nQmepsurr = nQmepsurr
                .nQmmsurr = nQmmsurr
                .nQmysurr = nQmysurr
                .nAminsurr = nAminsurr
                .nAmaxsurr = nAmaxsurr
                .nPervssurr = nPervssurr
                .nCapminsurr = nCapminsurr
                .nMaxchargsurr = nMaxchargsurr
                .nOrigin_surr = nOrigin_surr
                .sRoutineSurr = sRoutineSurr
                .sApplyRouSurr = IIf(sApplyRouSurr = String.Empty, "2", sApplyRouSurr)
                .nQMMPSurr = nQMMPSurr
                .nBalminsurr = nBalminsurr

                If .insProdLifeSeq Then
                    Call lclsProd_win.Add_Prod_win(nBranch, nProduct, dEffecdate, sCodispl, "2", nUsercode)
                    insPostDP043B = True
                End If
            End If
        End With

        'UPGRADE_NOTE: Object lclsProd_win may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsProd_win = Nothing
        'UPGRADE_NOTE: Object lclsProduct may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsProduct = Nothing

insPostDP043B_Err:
        If Err.Number Then
            insPostDP043B = False
        End If
        On Error GoTo 0
    End Function

    '% insValDP020: Verifica y valida los datos de la forma 'Beneficios de opciones de pagos de primas'
    Public Function insValDP020(ByVal sCodispl As String, ByVal nBenefitr As Double, ByVal nBenefapl As Integer) As String
        Dim lclsErrors As eFunctions.Errors

        On Error GoTo insValDP020_Err

        lclsErrors = New eFunctions.Errors

        ' + Validación del campo "Aplicación"
        If nBenefitr <> eRemoteDB.Constants.intNull And nBenefapl = eRemoteDB.Constants.intNull Then
            '+ Si se indicó el "% mínimo garantizado", debe indicarse la forma de aplicación del mismo
            Call lclsErrors.ErrorMessage(sCodispl, 11446)
        End If

        insValDP020 = lclsErrors.Confirm

insValDP020_Err:
        If Err.Number Then
            insValDP020 = "insValDP020: " & Err.Description
        End If
        On Error GoTo 0
        'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsErrors = Nothing
    End Function

    '% insPostDP020: se actualizan los datos en la tabla (Product_li)
    Public Function insPostDP020(ByVal sCodispl As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal dEffecdate As Date, ByVal nUsercode As Integer, ByVal nBenefitr As Double, ByVal nBenefexc As Double, ByVal nBenexcra As Double, ByVal nBenefapl As Integer, ByVal sBenres As String) As Boolean
        Dim lclsProduct As eProduct.Product

        On Error GoTo insPostDP020_Err

        lclsProduct = New eProduct.Product

        With lclsProduct
            If .FindProduct_li(nBranch, nProduct, dEffecdate) Then
                .dEffecdate = dEffecdate
                .nBranch = nBranch
                .nProduct = nProduct
                .nUsercode = nUsercode
                .nBenefitr = nBenefitr
                .nBenefapl = nBenefapl
                .nBenefexc = nBenefexc
                .nBenexcra = nBenexcra
                .sBenres = sBenres

                insPostDP020 = .insProdLifeSeq
            End If
        End With

insPostDP020_Err:
        If Err.Number Then
            insPostDP020 = False
        End If
        On Error GoTo 0
        'UPGRADE_NOTE: Object lclsProduct may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsProduct = Nothing
    End Function

    '% insvalDP044: Verifica los datos del frame de Fondos de inversión al plan
    Public Function insValDP044(ByVal sCodispl As String, ByVal sAction As String, ByVal sWindowType As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal dEffecdate As Date, ByVal nFund As Integer, ByVal nPartic_min As Double, ByVal nParticip As Double, ByVal nBuy_cost As Double, ByVal nSell_cost As Double, ByVal nCountAdd As Integer, ByVal nTotAdd As Integer, ByVal nUpdate As Integer) As String
        Dim lclsErrors As eFunctions.Errors
        Dim lclsValTime As eFunctions.valField
        Dim lclsValues As eFunctions.Values
        Dim lclsFunds_pol As Object
        Dim lclsFunds As Object
        Dim lintIndex As Integer

        On Error GoTo insValDP044_Err

        lclsErrors = New eFunctions.Errors
        lclsValTime = New eFunctions.valField
        lclsValues = New eFunctions.Values
        lclsFunds_pol = eRemoteDB.NetHelper.CreateClassInstance("ePolicy.Funds_Pol")
        lclsFunds = eRemoteDB.NetHelper.CreateClassInstance("ePolicy.Funds")
        lclsValTime.objErr = lclsErrors

        If sAction = "Delete" Then
            '+ Si existen pólizas asociadas no se puede anular
            If lclsFunds_pol.FindPolFund(nBranch, nProduct, nFund, dEffecdate) Then
                Call lclsErrors.ErrorMessage(sCodispl, 11241)
            End If
        Else
            If lclsFunds.Find(nBranch, nProduct, dEffecdate) Then
                With lclsFunds
                    For lintIndex = 0 To .CountVI010
                        If .Item(lintIndex) Then
                            If .nFunds = nFund Then
                                Exit For
                            Else
                                If lclsFunds.CountVI010 = nTotAdd Then
                                    '+ No se puede exceder el maximo de fondo permitido definido para el producto
                                    Call lclsErrors.ErrorMessage(sCodispl, 11284)
                                    Exit For
                                End If
                            End If
                        End If
                    Next lintIndex
                End With
            End If

            '+ Validación del fondo
            If nFund = 0 Or nFund = eRemoteDB.Constants.intNull Then
                Call lclsErrors.ErrorMessage(sCodispl, 17001)
            End If

            '+ Validación del "% Mínimo".

            If Fix(nPartic_min) <> eRemoteDB.Constants.intNull Then
                lclsValTime.ValFormat = "##0.##"
                lclsValTime.ErrRange = 1938
                lclsValTime.Max = 100
                lclsValTime.Min = 1
                lclsValTime.Descript = "Porcentaje mínimo"
                Call lclsValTime.ValNumber(nPartic_min)
            Else
                Call lclsErrors.ErrorMessage(sCodispl, 11238)
            End If

            '+ Validación del "% de inversión ".

            If Fix(nParticip) <> eRemoteDB.Constants.intNull Then
                lclsValTime.ValFormat = "##0.##"
                lclsValTime.ErrRange = 1938
                lclsValTime.Max = 100
                lclsValTime.Min = 1.0#
                lclsValTime.Descript = "Porcentaje de inversión"
                If lclsValTime.ValNumber(nParticip) Then
                    If nPartic_min > nParticip Then
                        Call lclsErrors.ErrorMessage(sCodispl, 17004)
                    End If
                End If
            Else
                Call lclsErrors.ErrorMessage(sCodispl, 11236)
            End If

            '+ Validación del "Costo de compra".

            If Fix(nBuy_cost) <> eRemoteDB.Constants.intNull Then
                lclsValTime.ErrRange = 1938
                lclsValTime.ValFormat = "##0.##"
                lclsValTime.Max = 100
                lclsValTime.Min = 0
                lclsValTime.Descript = "Costo de compra"
                Call lclsValTime.ValNumber(nBuy_cost)
            Else
                Call lclsErrors.ErrorMessage(sCodispl, 11268)
            End If

            '+ Validación del "Costo de venta".

            If Fix(nSell_cost) <> eRemoteDB.Constants.intNull Then
                lclsValTime.ValFormat = "##0.##"
                lclsValTime.ErrRange = 1938
                lclsValTime.Max = 100
                lclsValTime.Min = 0
                lclsValTime.Descript = "Costo de venta"
                Call lclsValTime.ValNumber(nSell_cost)
            Else
                Call lclsErrors.ErrorMessage(sCodispl, 17003)
            End If
        End If

        insValDP044 = lclsErrors.Confirm

        'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsErrors = Nothing
        'UPGRADE_NOTE: Object lclsValTime may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsValTime = Nothing
        'UPGRADE_NOTE: Object lclsValues may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsValues = Nothing
        'UPGRADE_NOTE: Object lclsFunds_pol may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsFunds_pol = Nothing
        'UPGRADE_NOTE: Object lclsFunds may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsFunds = Nothing

insValDP044_Err:
        If Err.Number Then
            insValDP044 = insValDP044 & Err.Description
        End If
        On Error GoTo 0
    End Function

    '
    '% insvalDP044: Verifica los datos del frame de Fondos de inversión al plan
    Public Function insValMsvDP044(ByVal sCodispl As String, ByVal nCountAdd As Integer, ByVal nTotAdd As Integer, ByVal nTotParticip As Double, ByVal nTotCtas As Integer) As String

        Dim lclsErrors As eFunctions.Errors
        Dim lclsValTime As eFunctions.valField

        On Error GoTo insValMsvDP044_Err

        lclsErrors = New eFunctions.Errors
        lclsValTime = New eFunctions.valField

        '- nCountAdd:    Cantidad de Registros que esta agregando
        '- Debe haber asociado al menos un fondo al producto
        If nCountAdd = 0 Then
            Call lclsErrors.ErrorMessage(sCodispl, 17002)
        Else
            '+ La sumatoria de las participaciones debe ser igual al 100%
            '- nTotAdd:      Cantidad de registros que permite añadir
            '- nTotParticip: Total de la sumatoria de las participaciones
            '            If nTotParticip <> 100 * nTotCtas Then
            '                Call lclsErrors.ErrorMessage(sCodispl, 3070)
            '            End If
        End If
        insValMsvDP044 = lclsErrors.Confirm

        'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsErrors = Nothing
        'UPGRADE_NOTE: Object lclsValTime may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsValTime = Nothing

insValMsvDP044_Err:
        If Err.Number Then
            insValMsvDP044 = insValMsvDP044 & Err.Description
        End If
        On Error GoTo 0
    End Function
    '% insPostDP044: permite actualizar los datos del frame DP044
    Public Function insPostDP044(ByVal sAction As String, ByVal nExist As Integer, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal dEffecdate As Date, ByVal nUsercode As Integer, ByVal nFunds As Integer, ByVal nBuy_cost As Double, ByVal dNulldate As Date, ByVal nPartic_min As Double, ByVal nParticip As Double, ByVal nSell_cost As Double, ByVal nOrigin As Integer, ByVal nIntProy As Double, ByVal nIntProyVarMax As Double, Optional ByVal nIntProyVarCle As Double = 0, Optional ByVal sVigen As String = "") As Boolean
        Dim lclsFund As Object

        On Error GoTo insPostDP044_Err

        lclsFund = eRemoteDB.NetHelper.CreateClassInstance("ePolicy.Funds")

        With lclsFund
            If sAction = "Add" Or sAction = "Update" Then
                .nBranch = nBranch
                .nProduct = nProduct
                .dEffecdate = dEffecdate
                .nFunds = nFunds
                .nBuy_cost = nBuy_cost
                'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
                .dNulldate = System.DBNull.Value
                .nPartic_min = nPartic_min
                .nParticip = nParticip
                .nSell_cost = nSell_cost
                .nUsercode = nUsercode
                .nOrigin = nOrigin
                .nIntProy = nIntProy
                .nIntProyVarMax = nIntProyVarMax
                .nIntProyVarCle = nIntProyVarCle
                .sVigen = sVigen
                If nExist = 0 Then
                    insPostDP044 = .Add
                Else
                    insPostDP044 = .Update
                End If
            Else
                .nBranch = nBranch
                .nProduct = nProduct
                .dEffecdate = dEffecdate
                .nFunds = nFunds
                .nOrigin = nOrigin
                insPostDP044 = .Delete
            End If
        End With

        'UPGRADE_NOTE: Object lclsFund may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsFund = Nothing
insPostDP044_Err:
        If Err.Number Then
            insPostDP044 = False
        End If
        On Error GoTo 0
    End Function

    '% insValDP021: Verifica los datos del frame de Datos para fondos de inversión
    Public Function insValDP021(ByVal sCodispl As String, ByVal nUlsschar As Double, ByVal nUlsmaxqu As Double, ByVal nUlscharg As Double, ByVal nUlrschar As Double, ByVal nUlrmaxqu As Double, ByVal nUlrcharg As Double) As String
        Dim lclsErrors As eFunctions.Errors

        On Error GoTo insValDP021_Err

        lclsErrors = New eFunctions.Errors

        With lclsErrors
            '+ Validación del campo <Switches - Recargo>
            If nUlsschar < nUlsmaxqu And nUlscharg = eRemoteDB.Constants.dblNull Then
                Call lclsErrors.ErrorMessage(sCodispl, 11060, , eFunctions.Errors.TextAlign.LeftAling, "Switches - Recargo:")
            End If

            '+ Validación del campo <Redirecciones - Recargo>
            If nUlrschar < nUlrmaxqu And nUlrcharg = eRemoteDB.Constants.dblNull Then
                Call lclsErrors.ErrorMessage(sCodispl, 11063, , eFunctions.Errors.TextAlign.LeftAling, "Redirecciones - Recargo:")
            End If

            insValDP021 = .Confirm
        End With

insValDP021_Err:
        If Err.Number Then
            insValDP021 = "insValDP021: " & Err.Description
        End If
        On Error GoTo 0
        'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsErrors = Nothing
    End Function

    '% insPostDP021: Realiza el llamado a la rutina que actualiza la tabla Product_li (Beneficios)
    Public Function insPostDP021(ByVal dEffecdate As Date, ByVal nBranch As Integer, ByVal nProduct As Integer,
                                 ByVal nUsercode As Integer, ByVal nCurrency As Integer, ByVal nUlfmaxqu As Integer,
                                 ByVal sUlfchani As String, ByVal nUlsmaxqu As Integer, ByVal nUlswiper As Integer,
                                 ByVal nULswmaxper As Integer, ByVal nUlsschar As Integer, ByVal nUlscharg As Double,
                                 ByVal nULswchPerc As Double, ByVal nULmmsw As Integer, ByVal nULSwmqt As Integer,
                                 ByVal nULswmqtper As Integer, ByVal nUlrmaxqu As Integer, ByVal nUlredper As Integer,
                                 ByVal nULrdmaxper As Integer, ByVal nUlrschar As Integer, ByVal nUlrcharg As Double,
                                 ByVal nUlrdchperc As Double, ByVal nULmmrd As Integer, ByVal nULrdmqt As Integer,
                                 ByVal nULrdmqtper As Integer, ByVal nInfType As Integer, ByVal nType_rateproy As Integer) As Boolean
        Dim lclsProduct As eProduct.Product

        On Error GoTo insPostDP021_Err

        lclsProduct = New eProduct.Product

        insPostDP021 = True

        With lclsProduct
            If .FindProduct_li(nBranch, nProduct, dEffecdate) Then
                .dEffecdate = dEffecdate
                .nBranch = nBranch
                .nProduct = nProduct
                .nUsercode = nUsercode

                .nCurrency = nCurrency
                .nUlfmaxqu = nUlfmaxqu
                .sUlfchani = IIf(sUlfchani = String.Empty, "2", sUlfchani)
                .nUlsmaxqu = nUlsmaxqu
                .nUlswiper = nUlswiper
                .nULswmaxper = nULswmaxper
                .nUlsschar = nUlsschar
                .nUlscharg = nUlscharg
                .nULswchPerc = nULswchPerc
                .nULmmsw = nULmmsw
                .nULSwmqt = nULSwmqt
                .nULswmqtper = nULswmqtper
                .nUlrmaxqu = nUlrmaxqu
                .nUlredper = nUlredper
                .nULrdmaxper = nULrdmaxper
                .nUlrschar = nUlrschar
                .nUlrcharg = nUlrcharg
                .nUlrdchperc = nUlrdchperc
                .nULmmrd = nULmmrd
                .nULrdmqt = nULrdmqt
                .nULrdmqtper = nULrdmqtper
                .nInfType = nInfType
                .nType_Rateproy = nType_rateproy

                insPostDP021 = .insProdLifeSeq
            End If
        End With

insPostDP021_Err:
        If Err.Number Then
            insPostDP021 = False
        End If
        On Error GoTo 0
        'UPGRADE_NOTE: Object lclsProduct may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsProduct = Nothing
    End Function



    '% insValDP024: Se realizan las validaciones de la ventana
    Public Function insValDP024(ByVal sCodispl As String, ByVal nPayiniti As Double, ByVal nPerrevfa As Double, ByVal nPermulti As Double, ByVal nPernunmi As Double, ByVal nPernumai As Double, ByVal nNoPermulti As Double, ByVal nNoPernunmi As Double, ByVal nNoPernumai As Double, ByVal sPeriodic As String, ByVal sPerunifa As String, ByVal sNoperiod As String, ByVal sNpeunifa As String, ByVal nDedufreq As Integer, ByVal sRevaltyp As String) As String
        Dim lclsErrors As eFunctions.Errors

        On Error GoTo insValDP024_Err

        lclsErrors = New eFunctions.Errors

        With lclsErrors
            '+ Debe indicarse algún tipo de pago (Periódico - No periódico)
            If sPeriodic = String.Empty And sNoperiod = String.Empty Then
                Call lclsErrors.ErrorMessage(sCodispl, 12091,  , eFunctions.Errors.TextAlign.LeftAling, "Pagos periódicos - Pagos no periódicos:")
            End If

            '+ El campo "Pago inicial" debe estar lleno
            If nPayiniti = eRemoteDB.Constants.intNull Then
                Call lclsErrors.ErrorMessage(sCodispl, 1012,  , eFunctions.Errors.TextAlign.LeftAling, "Pago inicial:")
            End If

            '+ Si se seleccionó "Pagos periódicos"

            If sPeriodic = "1" Then
                '+ El campo "Frecuencia" debe estar lleno
                If nDedufreq = eRemoteDB.Constants.intNull Then
                    Call lclsErrors.ErrorMessage(sCodispl, 1012,  , eFunctions.Errors.TextAlign.LeftAling, "Pagos periódicos - Frecuencia:")
                End If

                '+ El campo "Múltiplo de" debe estar lleno, si se seleccionó "Monto uniforme"
                If sPerunifa = "1" And nPermulti = eRemoteDB.Constants.intNull Then
                    Call lclsErrors.ErrorMessage(sCodispl, 1012,  , eFunctions.Errors.TextAlign.LeftAling, "Pagos periódicos - Múltiplo de:")
                End If

                '+ El campo "Mínimo" debe estar lleno
                If sPerunifa = String.Empty And nPernunmi = eRemoteDB.Constants.intNull Then
                    Call lclsErrors.ErrorMessage(sCodispl, 1012,  , eFunctions.Errors.TextAlign.LeftAling, "Pagos periódicos - Mínimo:")
                End If

                '+ Si el campo "Máximo" está lleno, debe ser superior o igual al campo "Mínimo"
                If sPerunifa = String.Empty And nPernunmi <> eRemoteDB.Constants.intNull And nPernumai <> eRemoteDB.Constants.intNull Then
                    If nPernumai < nPernunmi Then
                        Call lclsErrors.ErrorMessage(sCodispl, 11133,  , eFunctions.Errors.TextAlign.LeftAling, "Pagos periódicos - Máximo:")
                    End If
                End If

                '+ Si el tipo de revalorización es "Factor fijo", el factor debe estar lleno
                If sRevaltyp = "3" And nPerrevfa = eRemoteDB.Constants.intNull Then
                    Call lclsErrors.ErrorMessage(sCodispl, 1012,  , eFunctions.Errors.TextAlign.LeftAling, "Pagos periódicos - Revalorización - Factor:")
                End If
            End If

            '+ Si se seleccionó "Pagos no periódicos"

            If sNoperiod = "1" Then
                '+ El campo "Múltiplo de" debe estar lleno, si se seleccionó "Monto uniforme"
                If sNpeunifa = "1" And nNoPermulti = eRemoteDB.Constants.intNull Then
                    Call lclsErrors.ErrorMessage(sCodispl, 1012,  , eFunctions.Errors.TextAlign.LeftAling, "Pagos no periódicos - Múltiplo de:")
                End If

                '+ El campo "Mínimo" debe estar lleno, si no se seleccionó "Monto uniforme"
                If sNpeunifa = String.Empty And nNoPernunmi = eRemoteDB.Constants.intNull Then
                    Call lclsErrors.ErrorMessage(sCodispl, 1012,  , eFunctions.Errors.TextAlign.LeftAling, "Pagos no periódicos - Mínimo:")
                End If

                '+ Si el campo "Máximo" está lleno, debe ser superior o igual al campo "Mínimo"
                If sNpeunifa = String.Empty And nNoPernunmi <> eRemoteDB.Constants.intNull And nNoPernumai <> eRemoteDB.Constants.intNull Then
                    If nNoPernumai < nNoPernunmi Then
                        Call lclsErrors.ErrorMessage(sCodispl, 11133,  , eFunctions.Errors.TextAlign.LeftAling, "Pagos no periódicos - Máximo:")
                    End If
                End If
            End If

            insValDP024 = .Confirm
        End With

insValDP024_Err:
        If Err.Number Then
            insValDP024 = "insValDP024: " & Err.Description
        End If
        On Error GoTo 0
        'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsErrors = Nothing
    End Function

    '% insPostDP024: Se actualizan los campos de la ventana
    Public Function insPostDP024(ByVal sCodispl As String, ByVal nPayiniti As Double, ByVal nAnnualap As Double, ByVal nPerrevfa As Double, ByVal nPermulti As Double, ByVal nPernunmi As Double, ByVal nPernumai As Double, ByVal nNoPermulti As Double, ByVal nNoPernunmi As Double, ByVal nNoPernumai As Double, ByVal sPeriodic As String, ByVal sPerunifa As String, ByVal sNoperiod As String, ByVal sNpeunifa As String, ByVal nDedufreq As Integer, ByVal sRevaltyp As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal dEffecdate As Date, ByVal nUsercode As Integer) As Boolean
        Dim lclsProduct As Product

        On Error GoTo insPostDP024_Err

        lclsProduct = New Product

        With lclsProduct
            If .FindProduct_li(nBranch, nProduct, dEffecdate) Then
                .nPayiniti = nPayiniti
                .nAnnualap = nAnnualap
                .nPerrevfa = nPerrevfa
                .sPeriodic = IIf(sPeriodic <> "1", "2", sPeriodic)
                .sPerunifa = IIf(sPerunifa <> "1", "2", sPerunifa)
                .sNoperiod = IIf(sNoperiod <> "1", "2", sNoperiod)
                .sNpeunifa = IIf(sNpeunifa <> "1", "2", sNpeunifa)
                .nDedufreq = IIf(nDedufreq <> 0, nDedufreq, eRemoteDB.Constants.intNull)
                .sRevaltyp = IIf(sRevaltyp <> "0", sRevaltyp, String.Empty)
                .nPermulti = nPermulti
                .nPernunmi = nPernunmi
                .nPernumai = nPernumai
                .nNpemulti = nNoPermulti
                .nNpenunmi = nNoPernunmi
                .nNpenumai = nNoPernumai
                .nUsercode = nUsercode
                .dEffecdate = dEffecdate

                insPostDP024 = .insProdLifeSeq
            End If
        End With

insPostDP024_Err:
        If Err.Number Then
            insPostDP024 = False
        End If
        On Error GoTo 0
        'UPGRADE_NOTE: Object lclsProduct may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsProduct = Nothing
    End Function

    '% insPostDP043D: Realiza el llamado a la rutina que actualiza la tabla Product_li (Saldado/Prorrogado)
    Public Function insPostDP043D(ByVal sCodispl As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal dEffecdate As Date, ByVal nUsercode As Integer, ByVal sRoureduc As String, ByVal sRoureddc As String) As Boolean
        Dim lclsProduct As eProduct.Product
        Dim lclsProd_win As eProduct.Prod_win

        lclsProduct = New eProduct.Product
        lclsProd_win = New eProduct.Prod_win

        On Error GoTo insPostDP043D_Err

        insPostDP043D = True

        With lclsProduct
            If .FindProduct_li(nBranch, nProduct, dEffecdate) Then
                .dEffecdate = dEffecdate
                .nBranch = nBranch
                .nProduct = nProduct
                .nUsercode = nUsercode
                .sRoureduc = IIf(sRoureduc <> String.Empty, sRoureduc, String.Empty)
                .sRoureddc = IIf(sRoureddc <> String.Empty, sRoureddc, String.Empty)
                insPostDP043D = .insProdLifeSeq

                If insPostDP043D Then
                    '+ Se actualiza la secuencia de ventana del producto con la transacción enviada como parámetro
                    Call lclsProd_win.Add_Prod_win(nBranch, nProduct, dEffecdate, "DP043D", "2", nUsercode)
                End If
            End If
        End With

        'UPGRADE_NOTE: Object lclsProduct may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsProduct = Nothing
        'UPGRADE_NOTE: Object lclsProd_win may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsProd_win = Nothing

insPostDP043D_Err:
        If Err.Number Then
            insPostDP043D = False
        End If
        On Error GoTo 0
    End Function

    '% [APV2] Inclusión de la ventana Reglas de capitalización (DP7001) DBLANCO 11-08-2003
    '% insValDP7001: Validación de la ventana que maneja las Reglas de Capitalización
    Public Function insValDP7001(ByVal sCodispl As String, ByVal nSaving_pct As Short, ByVal nIndex_table As Short, ByVal nWarrn_table As Short, ByVal sAccount_mirror As String, ByVal nwarrn_table_mirror As Short) As String

        Dim lclsErrors As eFunctions.Errors

        On Error GoTo insValDP7001_Err

        lclsErrors = New eFunctions.Errors

        With lclsErrors

            If nSaving_pct > 0 Then

                If nSaving_pct > 100 Then
                    Call .ErrorMessage(sCodispl, 70152)
                End If

                If nIndex_table <= 0 Then
                    Call .ErrorMessage(sCodispl, 70144)
                End If

                If nWarrn_table <= 0 Then
                    Call .ErrorMessage(sCodispl, 70145)
                End If

                If sAccount_mirror = "1" And nwarrn_table_mirror <= 0 Then
                    Call .ErrorMessage(sCodispl, 70145,  , eFunctions.Errors.TextAlign.RigthAling, " para la cuenta espejo")
                End If
            End If

            insValDP7001 = .Confirm
        End With

        'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsErrors = Nothing

insValDP7001_Err:
        If Err.Number Then
            insValDP7001 = insValDP7001 & Err.Description
        End If
        On Error GoTo 0
    End Function

    '% [APV2] Inclusión de la ventana Reglas de capitalización (DP7001) DBLANCO 11-08-2003
    '% insPostDP7001: Actualización de los campos de la ventana que maneja las Reglas de
    '% Capitalización
    Public Function insPostDP7001(ByVal sCodispl As String, ByVal nBranch As Short, ByVal nProduct As Short, ByVal dEffecdate As Date, ByVal nSaving_pct As Short, ByVal nIndex_table As Short, ByVal nWarrn_table As Short, ByVal sS_allwchng As String, ByVal sIx_allwchng As String, ByVal sW_allwchng As String, ByVal nUsercode As Short, ByVal sAccount_mirror As String, ByVal nwarrn_table_mirror As Short) As Boolean

        Dim lclsProduct As Product
        Dim lclsProd_win As Prod_win

        On Error GoTo insPostDP7001_Err

        lclsProduct = New Product
        lclsProd_win = New Prod_win

        With lclsProduct
            If .FindProduct_li(nBranch, nProduct, dEffecdate) Then
                .nSaving_pct = nSaving_pct
                .nIndex_table = nIndex_table
                .nWarrn_table = nWarrn_table
                .sS_allwchng = IIf(sS_allwchng = String.Empty, "2", sS_allwchng)
                .sIx_allwchng = IIf(sIx_allwchng = String.Empty, "2", sIx_allwchng)
                .sW_allwchng = IIf(sW_allwchng = String.Empty, "2", sW_allwchng)
                .dEffecdate = dEffecdate
                .nUsercode = nUsercode
                .sAccount_mirror = IIf(sAccount_mirror = String.Empty, "2", sAccount_mirror)
                .nwarrn_table_mirror = nwarrn_table_mirror
                insPostDP7001 = .insProdLifeSeq
            End If
        End With

        'UPGRADE_NOTE: Object lclsProduct may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsProduct = Nothing
        'UPGRADE_NOTE: Object lclsProd_win may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsProd_win = Nothing

insPostDP7001_Err:
        If Err.Number Then
            insPostDP7001 = False
        End If
        On Error GoTo 0
    End Function

    '* Class_Terminate: Controla la destrucción de la clase
    'UPGRADE_NOTE: Class_Terminate was upgraded to Class_Terminate_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
    Private Sub Class_Terminate_Renamed()
        'UPGRADE_NOTE: Object mclsProduct_li may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        mclsProduct_li = Nothing
        'UPGRADE_NOTE: Object mcolDurinsu_prod may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        mcolDurinsu_prod = Nothing
        'UPGRADE_NOTE: Object mcolDurpay_prod may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        mcolDurpay_prod = Nothing
    End Sub
    Protected Overrides Sub Finalize()
        Class_Terminate_Renamed()
        MyBase.Finalize()
    End Sub
End Class






