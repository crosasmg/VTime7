Option Strict Off
Option Explicit On
Public Class opt_premiu
	'%-------------------------------------------------------%'
	'% $Workfile:: opt_premiu.cls                           $%'
	'% $Author:: Nvaplat40                                  $%'
	'% $Date:: 19/04/04 9:38a                               $%'
	'% $Revision:: 6                                        $%'
	'%-------------------------------------------------------%'
	
	'**Data of Opt_premiu15
	'Información de Opt_Premiu15
	Public dEffecdate As Date 'datetime                                                                                                                         no                                  8                       no                                  (n/a)                               (n/a)
	'Public dCompdate   a                   'datetime                                                                                                                         no                                  8                       yes                                 (n/a)                               (n/a)
	Public nUsercode As Integer 'smallint                                                                                                                         no                                  2           5     0     yes                                 (n/a)                               (n/a)
	Public nBank_acc As Integer 'smallint                                                                                                                         no                                  2           5     0     yes                                 (n/a)                               (n/a)
	Public nLower_lim As Double 'decimal                                                                                                                          no                                  9           10    2     yes                                 (n/a)                               (n/a)
	Public nUpper_lim As Double 'decimal                                                                                                                          no                                  9           10    2     yes                                 (n/a)                               (n/a)
	Public sParCollect As Double 'char                                                                                                                             no                                  1                       no                                  no                                  no
	Public sReqAmo As String 'char                                                                                                                             no                                  1                       yes                                 no                                  yes
	Public sTechAffect As String 'char                                                                                                                             no                                  1                       yes                                 no                                  yes
	Public nFixInt As Double 'decimal                                                                                                                          no                                  5           4     2     yes                                 (n/a)                               (n/a)
	Public nAmenLevel As Integer 'smallint                                                                                                                         no                                  2           5     0     yes                                 (n/a)                               (n/a)
	Public nPreReceipt As Integer 'smallint                                                                                                                         no                                  2           5     0     yes                                 (n/a)                               (n/a)
	Public nIntCalc As Integer 'smallint                                                                                                                         no                                  2           5     0     yes                                 (n/a)                               (n/a)
	Public nUpperInt As Double 'decimal                                                                                                                          no                                  5           4     2     yes                                 (n/a)                               (n/a)
	Public sMod_loLim As String 'char                                                                                                                             no                                  1                       yes                                 no                                  yes
	Public nLowerInt As Double 'decimal                                                                                                                          no                                  5           4     2     yes                                 (n/a)                               (n/a)
	Public sMod_upLim As String 'char                                                                                                                             no                                  1                       yes                                 no                                  yes
	Public nLower_lim_Agree As Double
	Public nUpper_lim_Agree As Double
	
    Public nUpperPercent As Integer
    Public nUpperPercentAgree As Integer
    Public nLowerPercent As Integer
    Public nLowerPercentAgree As Integer

    Public nUpperPercentAMO As Double
    Public nUpperPercentAgreeAMO As Double
    Public nLowerPercentAMO As Double
    Public nLowerPercentAgreeAMO As Double

    Public nTolerCurr As Integer
    Public ncodToler As Integer

	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		find()
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
	
	
	'% find
	'--------------------------------------------
	Public Function find() As Boolean
		'--------------------------------------------
		Dim lobjOptPremiu As eRemoteDB.Execute
		lobjOptPremiu = New eRemoteDB.Execute
		With lobjOptPremiu
			.StoredProcedure = "reaOpt_Premiu"
			If .Run Then
				nBank_acc = .FieldToClass("nAcc_bank")
                nLower_lim = .FieldToClass("nLower_lim", 0)
                nUpper_lim = .FieldToClass("nUpper_lim", 0)
				sParCollect = .FieldToClass("sParCollect")
				sReqAmo = .FieldToClass("sReqAmo")
				sTechAffect = .FieldToClass("sTechAffect")
				nFixInt = .FieldToClass("nFixInt")
				nAmenLevel = .FieldToClass("nAmenLevel")
				nPreReceipt = .FieldToClass("nPreReceipt")
				nIntCalc = .FieldToClass("nIntCalc")
				nUpperInt = .FieldToClass("nUpperInt")
				sMod_loLim = .FieldToClass("sMod_loLim")
				nLowerInt = .FieldToClass("nLowerInt")
				sMod_upLim = .FieldToClass("sMod_upLim")
				nLower_lim_Agree = .FieldToClass("nLower_lim_Agree")
                nUpper_lim_Agree = .FieldToClass("nUpper_lim_Agree")

                nUpperPercent = .FieldToClass("nUpperPercent", 0)
                nUpperPercentAgree = .FieldToClass("nUpperPercentAgree", 0)
                nLowerPercent = .FieldToClass("nLowerPercent", 0)
                nLowerPercentAgree = .FieldToClass("nLowerPercentAgree", 0)
                nUpperPercentAMO = .FieldToClass("nUpperPercentAMO", 0)
                nUpperPercentAgreeAMO = .FieldToClass("nUpperPercentAgreeAMO", 0)
                nLowerPercentAMO = .FieldToClass("nLowerPercentAMO", 0)
                nLowerPercentAgreeAMO = .FieldToClass("nLowerPercentAgreeAMO", 0)

                nTolerCurr = .FieldToClass("nTolerCurr", 4)
                nCodToler = .FieldToClass("nCodToler", 1)

				.RCloseRec()
			End If
		End With
		'UPGRADE_NOTE: Object lobjOptPremiu may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lobjOptPremiu = Nothing
		
    End Function

    Public Function GETLOWER_LIMEXC(ByVal DVALUEDATE As Date, ByVal NCURENCYDES As Integer, ByVal NAMOUNT As Double, Optional ByVal nCodToler As Integer = 0) As Double

        Dim nLower_lim As Double
        Dim nPercentAMO As Double
        Dim mobjExchange As Object

        '/*+ TRAE LOS VALORES DEL MARGE DE TOLERANCIA */
        Call Me.find()

        If nCodToler = 0 Then
            nCodToler = 1
        Else
            nCodToler = Me.ncodToler
        End If

        '/*+ SI SE DEFINIO UN MONTO FIJO */
        If nCodToler = 1 Then

            '/*+ SI SE DEFINIO UN MONTO FIJO */
            If Me.nLower_lim <> 0 Then
                '/*+ SI LA MONEDA DE DESTINO ES DISTINTA AL LA QUE ESTA DEFINADA EL MARGE Y ESTE ES MAYOR O HACE UNA CONVERSION  */
                If NCURENCYDES <> Me.nTolerCurr Then
                    mobjExchange = New eGeneral.Exchange

                    Call mobjExchange.Convert(0, NAMOUNT, Me.nTolerCurr, NCURENCYDES, DVALUEDATE, 0)

                    nLower_lim = mobjExchange.pdblResult
                    mobjExchange = Nothing
                Else
                    nLower_lim = Me.nLower_lim
                End If
            End If
        End If

        '/*+ SI SE DEFINIO UN PORCENTAJE DEL MONTO DEL PARAMETRO*/
        If nCodToler = 2 Then
            If Me.nLowerPercent <> 0 Then
                nPercentAMO = (NAMOUNT * Me.nLowerPercent) / 100

                If nPercentAMO > Me.nLowerPercentAMO And Me.nLowerPercentAMO <> 0 Then
                    nLower_lim = Me.nLowerPercentAMO
                Else
                    nLower_lim = nPercentAMO
                End If
            End If
        End If

        GETLOWER_LIMEXC = nLower_lim

    End Function

    Public Function GETUPPER_LIMEXC(ByVal DVALUEDATE As Date, ByVal NCURENCYDES As Integer, ByVal NAMOUNT As Double, Optional ByVal nCodToler As Integer = 0) As Double

        Dim nUpper_lim As Double
        Dim nPercentAMO As Double
        Dim mobjExchange As Object

        '/*+ TRAE LOS VALORES DEL MARGE DE TOLERANCIA */
        Call Me.find()


        If nCodToler = 0 Then
            nCodToler = 1
        Else
            nCodToler = Me.ncodToler
        End If

        '/*+ SI SE DEFINIO UN MONTO FIJO */
        If nCodToler = 1 Then
            If Me.nUpper_lim <> 0 Then
                '/*+ SI LA MONEDA DE DESTINO ES DISTINTA AL LA QUE ESTA DEFINADA EL MARGE Y ESTE ES MAYOR O HACE UNA CONVERSION  */
                If NCURENCYDES <> Me.nTolerCurr Then
                    mobjExchange = New eGeneral.Exchange

                    Call mobjExchange.Convert(0, NAMOUNT, Me.nTolerCurr, NCURENCYDES, DVALUEDATE, 0)

                    nUpper_lim = mobjExchange.pdblResult

                    mobjExchange = Nothing
                Else
                    nUpper_lim = Me.nUpper_lim
                End If
            End If
        End If

        '/*+ SI SE DEFINIO UN PORCENTAJE DEL MONTO DEL PARAMETRO*/
        If nCodToler = 2 Then
            If Me.nUpperPercent <> 0 Then
                nPercentAMO = (NAMOUNT * Me.nUpperPercent) / 100

                If nPercentAMO > Me.nUpperPercentAMO And Me.nUpperPercentAMO <> 0 Then
                    nUpper_lim = Me.nUpperPercentAMO
                Else
                    nUpper_lim = nPercentAMO
                End If
            End If
        End If

        GETUPPER_LIMEXC = nUpper_lim

    End Function

End Class






