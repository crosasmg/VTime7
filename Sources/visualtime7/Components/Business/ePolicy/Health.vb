Option Strict Off
Option Explicit On
Public Class Health
	'- Se deifne la colección pública para cargar las tarifas por "default" para la AM002
	
	Public oColAM002 As Collection
	
	Public nTariffDefault As Integer
	Public nProduct As Integer
	Public nBranch As Integer
	Public sCertype As String
	Public nPolicy As Double
	Public nCertif As Double
	Public dEffecdate As Date
	Public nCapital As Double
	Public dExpirdat As Date
	Public sClient As String
	Public nGroup_comp As Integer
	Public dIssuedat As Date
	Public nNullcode As Integer
	Public dNulldate As Date
	Public nPremium As Double
	Public dStartdate As Date
	Public nTariff As Integer
	Public nUsercode As Integer
	Public nTransactio As Integer
	Public nGroup As Integer
	Public nSituation As Integer
	
	'%insPreAM002: realiza la acción inicial de la ventana AM002
	Public Function insPreAM002(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date) As Boolean
		Dim lclsTar_Am_Bas As eBranches.Tar_am_bas
		Dim lclsTar_Am_BasProd As eBranches.Tar_am_basprod
		Dim lclsTar_am_basProds As eBranches.Tar_am_basprods
		Dim lblnFind As Boolean
		
		On Error GoTo insPreAM002_Err
		
		lclsTar_Am_Bas = New eBranches.Tar_am_bas
		lclsTar_Am_BasProd = New eBranches.Tar_am_basprod
		lclsTar_am_basProds = New eBranches.Tar_am_basprods
		
		With lclsTar_Am_BasProd
			If lclsTar_am_basProds.Find(nBranch, nProduct, dEffecdate) Then
				lblnFind = lclsTar_Am_Bas.Load(sCertype, nBranch, nProduct, nPolicy, dEffecdate)
				For	Each lclsTar_Am_BasProd In lclsTar_am_basProds
					If lblnFind Then
						If .nTariff <> lclsTar_Am_Bas.nTariff Then
							oColAM002.Add(lclsTar_Am_BasProd)
						End If
					Else
						oColAM002.Add(lclsTar_Am_Bas)
					End If
				Next lclsTar_Am_BasProd
			End If
		End With
		
		'UPGRADE_NOTE: Object lclsTar_Am_Bas may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsTar_Am_Bas = Nothing
		'UPGRADE_NOTE: Object lclsTar_Am_BasProd may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsTar_Am_BasProd = Nothing
		
insPreAM002_Err: 
		If Err.Number Then
			insPreAM002 = False
			'UPGRADE_NOTE: Object lclsTar_Am_Bas may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
			lclsTar_Am_Bas = Nothing
			'UPGRADE_NOTE: Object lclsTar_Am_BasProd may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
			lclsTar_Am_BasProd = Nothing
		End If
	End Function
	
	'% insReaTariffDefault : obtiene la tarifa a trabajar por "default"
    Public Function insReaTariffDefault(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, ByVal nTransaction As Integer) As Boolean
        Dim lintPos As Integer
        Dim lblnDefault As Boolean
        Dim lintTariff As Integer
        Dim lclsTar_Am_BasProd As eBranches.Tar_am_basprod
        Dim lclsTar_Am_Bas As eBranches.Tar_am_bas
        Dim lclsHealth As Health

        lclsTar_Am_BasProd = New eBranches.Tar_am_basprod
        lclsTar_Am_Bas = New eBranches.Tar_am_bas
        lclsHealth = New Health

        lintPos = 0
        lblnDefault = False

        lclsHealth.Find(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate)

        insReaTariffDefault = True

        '+ Se leen las tarifas asociadas al producto

        With lclsTar_Am_BasProd
            If .Load(nBranch, nProduct, dEffecdate) Then
                Do While .Item(lintPos) And Not lblnDefault
                    If .sDefaulti = "1" Then
                        lblnDefault = True
                    Else
                        lintPos = lintPos + 1
                    End If
                Loop
            End If
        End With

        lblnDefault = False
        lintPos = 0

        '+ Se leen las tarifas asociadas a la póliza

        With lclsTar_Am_Bas
            If .Load(sCertype, nBranch, nProduct, nPolicy, dEffecdate, True) Then

                Do While .Item(lintPos) And Not lblnDefault
                    If .sDefaulti = "1" Then
                        lblnDefault = True
                    Else
                        lintPos = lintPos + 1
                    End If
                Loop

                '+ Si la acción trata sobre un certificado, se lee la tarifa asociada al mismo en la tabla de datos particulares

                If nTransaction = Constantes.PolTransac.clngTempCertifAmendment Or nTransaction = Constantes.PolTransac.clngCertifIssue Or nTransaction = Constantes.PolTransac.clngCertifAmendment Then

                    '+ Se asocia a la tarifa por default aquella traída de la tabla de datos particulares para el certificado

                    lintTariff = IIf(lclsHealth.nTariff <> eRemoteDB.Constants.intNull, lclsHealth.nTariff, 0)

                    '+ Se evalúa si la tarifa del certificado existe en la tabla de datos particulares para asociarla y sino, se asocia la de la póliza o la del producto

                    If lintTariff <> 0 Then
                        nTariffDefault = lclsHealth.nTariff
                    Else
                        If lblnDefault Then
                            nTariffDefault = .nTariff
                        Else
                            nTariffDefault = lclsTar_Am_BasProd.nTariff
                        End If
                    End If
                Else

                    '+ En caso de tratarse de una póliza matriz o individual (certif = 0) se evalúa si se asociará la tarifa del producto o la de la póliza previamente guardada en la tabla

                    If lblnDefault Then
                        nTariffDefault = .nTariff
                    Else
                        nTariffDefault = lclsTar_Am_BasProd.nTariff
                    End If
                End If
            Else

                '+ Si no se han guardado tarifas previas para la póliza se asocian la del producto

                nTariffDefault = lclsTar_Am_BasProd.nTariff
            End If
        End With

        'UPGRADE_NOTE: Object lclsTar_Am_BasProd may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsTar_Am_BasProd = Nothing
        'UPGRADE_NOTE: Object lclsTar_Am_Bas may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsTar_Am_Bas = Nothing
        'UPGRADE_NOTE: Object lclsHealth may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsHealth = Nothing
    End Function
	
	'%Find: busca en la tabla el registro activo para atención médica asociado a una póliza
    Public Function Find(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date) As Boolean
        Dim lrecreaHealth As eRemoteDB.Execute

        On Error GoTo Find_Err

        lrecreaHealth = New eRemoteDB.Execute

        '+ Definición de parámetros para stored procedure 'insudb.reaHealth'
        '+ Información leída el 11/01/2002 05:05:07 p.m.

        With lrecreaHealth
            .StoredProcedure = "reaHealth"
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If .Run Then
                Find = True
                Do While Not .EOF
                    nProduct = .FieldToClass("nProduct")
                    nBranch = .FieldToClass("nBranch")
                    sCertype = .FieldToClass("sCertype")
                    nPolicy = .FieldToClass("nPolicy")
                    nCertif = .FieldToClass("nCertif")
                    dEffecdate = .FieldToClass("dEffecdate")
                    nCapital = .FieldToClass("nCapital")
                    dExpirdat = .FieldToClass("dExpirdat")
                    sClient = .FieldToClass("sClient")
                    nGroup_comp = .FieldToClass("nGroup_comp")
                    dIssuedat = .FieldToClass("dIssuedat")
                    nNullcode = .FieldToClass("nNullcode")
                    dNulldate = .FieldToClass("dNulldate")
                    nPremium = .FieldToClass("nPremium")
                    dStartdate = .FieldToClass("dStartdate")
                    nTariff = .FieldToClass("nTariff")
                    nUsercode = .FieldToClass("nUsercode")
                    nTransactio = .FieldToClass("nTransactio")
                    nGroup = .FieldToClass("nGroup")
                    nSituation = .FieldToClass("nSituation")
                    .RNext()
                Loop
                .RCloseRec()
            Else
                Find = False
            End If
        End With
        'UPGRADE_NOTE: Object lrecreaHealth may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecreaHealth = Nothing

Find_Err:
        If Err.Number Then
            Find = False
        End If
    End Function
End Class






