Option Strict Off
Option Explicit On
Public Class TaxSituat
	'%-------------------------------------------------------%'
	'% $Workfile:: TaxSituat.cls                            $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 1:30p                                $%'
	'% $Revision:: 7                                        $%'
	'%-------------------------------------------------------%'
	
	'--------------------------------VARIABLES LOCALES-----------------------------'
	
	'- Situaci�n ante el impuesto a las gcias
	Private mintProfitSituat As Integer
	'- Indicador de no retener Gcia porque presento constancia
	Private mblnProfitNoReten As Boolean
	'- Indicador adherido al r�gimen operativo
	Private mblnOperative As Boolean
	'- Indicador ajuste por inflaci�n
	Private mblnAdjust As Boolean
	'- N�mero anterior de insc. ingresos brutos
	Private mstrNetIncPreviusNumber As String
	'- N�mero de insc. ingresos brutos
	Private mstrNetIncNewNumber As String
	'- Provincias en las que actua como agente de percepcion
	Private mintProvinceAgentPercep As Integer
	'- Provincia en las que est� inscripto en ingresos brutos
	Private mintProvinceInsc As Integer
	'- Indicador convenio multilateral
	Private mblnMultilatAgree As Boolean
	'- Numero de convenio multilateral
	Private mintMultilatAgreeArticle As Integer
	'- Provincias en las que est� exento en ingresos brutos
	Private mintProvinceExempt As Integer
	'- Nro de autonomos
	Private mintSijp_peop As Integer
	'- Nro de sociedades
	Private mintSijp_comp As Integer
	'- Situaci�n ante el IVA
	Private mintIvaSituat As Integer
	'- Indicador agente de retencion
	Private mblnAgent_ret As Boolean
	'- Indicador regimen promocion iva
	Private mblnIvaProm As Boolean
	'- Porcentaje dto promocion iva
	Private mdblPercentIvaProm As Double
	'- Fecha inicio promocion
	Private mdtmDateIvaPromFrom As Date
	'- Fecha fin promocion
	Private mdtmDateIvaPromTo As Date
	'- Indicador constancia exento en IVA
	Private mblnConstanceIvaExempt As Boolean
	'- Situaci�n ante el impuesto municipal
	Private mintTaxCitySituat As Integer
	'- N�mero de inscripci�n impuesto municipal
	Private mintTaxCityNumber As Integer
	'- Situaci�n sellado
	Private mintStampSituat As Integer
	
	'------------------------------------INTERFAZ----------------------------------'
	
	'% ProfitSituat: Situaci�n ante el impuesto a las gcias
	'-----------------------------------------------------------
	
	'% ProfitSituat: Situaci�n ante el impuesto a las gcias
	'-----------------------------------------------------------
	Public Property ProfitSituat() As Integer
		Get
			'-----------------------------------------------------------
			ProfitSituat = mintProfitSituat
		End Get
		Set(ByVal Value As Integer)
			'-----------------------------------------------------------
			mintProfitSituat = Value
		End Set
	End Property
	
	'% ProfitNoReten: Indicador de no retener Gcia porque presento constancia
	'-----------------------------------------------------------
	
	'% ProfitNoReten: Indicador de no retener Gcia porque presento constancia
	'-----------------------------------------------------------
	Public Property ProfitNoReten() As Boolean
		Get
			'-----------------------------------------------------------
			ProfitNoReten = mblnProfitNoReten
		End Get
		Set(ByVal Value As Boolean)
			'-----------------------------------------------------------
			mblnProfitNoReten = Value
		End Set
	End Property
	
	'% OperativeRegime: Indicador adherido al r�gimen operativo
	'-----------------------------------------------------------
	
	'% OperativeRegime: Indicador adherido al r�gimen operativo
	'-----------------------------------------------------------
	Public Property OperativeRegime() As Boolean
		Get
			'-----------------------------------------------------------
			OperativeRegime = mblnOperative
		End Get
		Set(ByVal Value As Boolean)
			'-----------------------------------------------------------
			mblnOperative = Value
		End Set
	End Property
	
	'% Adjust: Indicador ajuste por inflaci�n
	'-----------------------------------------------------------
	
	'% Adjust: Indicador ajuste por inflaci�n
	'-----------------------------------------------------------
	Public Property Adjust() As Boolean
		Get
			'-----------------------------------------------------------
			Adjust = mblnAdjust
		End Get
		Set(ByVal Value As Boolean)
			'-----------------------------------------------------------
			mblnAdjust = Value
		End Set
	End Property
	
	'% NetIncNewNumber: N�mero de insc. ingresos brutos
	'-----------------------------------------------------------
	
	'% NetIncNewNumber: N�mero de insc. ingresos brutos
	'-----------------------------------------------------------
	Public Property NetIncNewNumber() As String
		Get
			'-----------------------------------------------------------
			NetIncNewNumber = mstrNetIncNewNumber
		End Get
		Set(ByVal Value As String)
			'-----------------------------------------------------------
			mstrNetIncNewNumber = Value
		End Set
	End Property
	
	'% NetIncPreviusNumber: N�mero anterior de insc. ingresos brutos
	'-----------------------------------------------------------
	
	'% NetIncPreviusNumber: N�mero anterior de insc. ingresos brutos
	'-----------------------------------------------------------
	Public Property NetIncPreviusNumber() As String
		Get
			'-----------------------------------------------------------
			NetIncPreviusNumber = mstrNetIncPreviusNumber
		End Get
		Set(ByVal Value As String)
			'-----------------------------------------------------------
			mstrNetIncPreviusNumber = Value
		End Set
	End Property
	
	'% ProvinceAgentPercep: Provincias en las que actua como agente de percepcion
	'-----------------------------------------------------------
	
	'% ProvinceAgentPercep: Provincias en las que actua como agente de percepcion
	'-----------------------------------------------------------
	Public Property ProvinceAgentPercep() As Integer
		Get
			'-----------------------------------------------------------
			ProvinceAgentPercep = mintProvinceAgentPercep
		End Get
		Set(ByVal Value As Integer)
			'-----------------------------------------------------------
			mintProvinceAgentPercep = Value
		End Set
	End Property
	
	'% ProvinceInsc: Provincia en las que est� inscripto en ingresos brutos
	'-----------------------------------------------------------
	
	'% ProvinceInsc: Provincia en las que est� inscripto en ingresos brutos
	'-----------------------------------------------------------
	Public Property ProvinceInsc() As Integer
		Get
			'-----------------------------------------------------------
			ProvinceInsc = mintProvinceInsc
		End Get
		Set(ByVal Value As Integer)
			'-----------------------------------------------------------
			mintProvinceInsc = Value
		End Set
	End Property
	
	'% MultilatAgree: Indicador convenio multilateral
	'-----------------------------------------------------------
	
	'% MultilatAgree: Indicador convenio multilateral
	'-----------------------------------------------------------
	Public Property MultilatAgree() As Boolean
		Get
			'-----------------------------------------------------------
			MultilatAgree = mblnMultilatAgree
		End Get
		Set(ByVal Value As Boolean)
			'-----------------------------------------------------------
			mblnMultilatAgree = Value
		End Set
	End Property
	
	'% ProvinceExempt: Provincias en las que est� exento en ingresos brutos
	'-----------------------------------------------------------
	
	'% ProvinceExempt: Provincias en las que est� exento en ingresos brutos
	'-----------------------------------------------------------
	Public Property ProvinceExempt() As Integer
		Get
			'-----------------------------------------------------------
			ProvinceExempt = mintProvinceExempt
		End Get
		Set(ByVal Value As Integer)
			'-----------------------------------------------------------
			mintProvinceExempt = Value
		End Set
	End Property
	
	'% Sijp_peop: Nro de autonomos
	'-----------------------------------------------------------
	
	'% Sijp_peop: Nro de autonomos
	'-----------------------------------------------------------
	Public Property Sijp_peop() As Integer
		Get
			'-----------------------------------------------------------
			Sijp_peop = mintSijp_peop
		End Get
		Set(ByVal Value As Integer)
			'-----------------------------------------------------------
			mintSijp_peop = Value
		End Set
	End Property
	
	'% Sijp_comp: Nro de sociedades
	'-----------------------------------------------------------
	
	'% Sijp_comp: Nro de sociedades
	'-----------------------------------------------------------
	Public Property Sijp_comp() As Integer
		Get
			'-----------------------------------------------------------
			Sijp_comp = mintSijp_comp
		End Get
		Set(ByVal Value As Integer)
			'-----------------------------------------------------------
			mintSijp_comp = Value
		End Set
	End Property
	
	'% IvaSituat: Situaci�n ante el IVA
	'-----------------------------------------------------------
	
	'% IvaSituat: Situaci�n ante el IVA
	'-----------------------------------------------------------
	Public Property IvaSituat() As Integer
		Get
			'-----------------------------------------------------------
			IvaSituat = mintIvaSituat
		End Get
		Set(ByVal Value As Integer)
			'-----------------------------------------------------------
			mintIvaSituat = Value
		End Set
	End Property
	
	'% Agent_ret: Indicador agente de retencion
	'-----------------------------------------------------------
	
	'% Agent_ret: Indicador agente de retencion
	'-----------------------------------------------------------
	Public Property Agent_ret() As Boolean
		Get
			'-----------------------------------------------------------
			Agent_ret = mblnAgent_ret
		End Get
		Set(ByVal Value As Boolean)
			'-----------------------------------------------------------
			mblnAgent_ret = Value
		End Set
	End Property
	
	'% IvaProm: Indicador regimen promocion iva
	'-----------------------------------------------------------
	
	'% IvaProm: Indicador regimen promocion iva
	'-----------------------------------------------------------
	Public Property IvaProm() As Boolean
		Get
			'-----------------------------------------------------------
			IvaProm = mblnIvaProm
		End Get
		Set(ByVal Value As Boolean)
			'-----------------------------------------------------------
			mblnIvaProm = Value
		End Set
	End Property
	
	'% PercentIvaProm: Porcentaje dto promocion iva
	'-----------------------------------------------------------
	
	'% PercentIvaProm: Porcentaje dto promocion iva
	'-----------------------------------------------------------
	Public Property PercentIvaProm() As Double
		Get
			'-----------------------------------------------------------
			PercentIvaProm = mdblPercentIvaProm
		End Get
		Set(ByVal Value As Double)
			'-----------------------------------------------------------
			mdblPercentIvaProm = Value
		End Set
	End Property
	
	'% DateIvaPromFrom: Fecha inicio promocion
	'-----------------------------------------------------------
	
	'% DateIvaPromFrom: Fecha inicio promocion
	'-----------------------------------------------------------
	Public Property DateIvaPromFrom() As Date
		Get
			'-----------------------------------------------------------
			DateIvaPromFrom = mdtmDateIvaPromFrom
		End Get
		Set(ByVal Value As Date)
			'-----------------------------------------------------------
			mdtmDateIvaPromFrom = Value
		End Set
	End Property
	
	'% DateIvaPromTo: Fecha fin promocion
	'-----------------------------------------------------------
	
	'% DateIvaPromTo: Fecha fin promocion
	'-----------------------------------------------------------
	Public Property DateIvaPromTo() As Date
		Get
			'-----------------------------------------------------------
			DateIvaPromTo = mdtmDateIvaPromTo
		End Get
		Set(ByVal Value As Date)
			'-----------------------------------------------------------
			mdtmDateIvaPromTo = Value
		End Set
	End Property
	
	'% ConstanceIvaExempt: Indicador constancia exento en IVA
	'-----------------------------------------------------------
	
	'% ConstanceIvaExempt: Indicador constancia exento en IVA
	'-----------------------------------------------------------
	Public Property ConstanceIvaExempt() As Boolean
		Get
			'-----------------------------------------------------------
			ConstanceIvaExempt = mblnConstanceIvaExempt
		End Get
		Set(ByVal Value As Boolean)
			'-----------------------------------------------------------
			mblnConstanceIvaExempt = Value
		End Set
	End Property
	
	'% TaxCitySituat: Situaci�n ante el impuesto municipal
	'-----------------------------------------------------------
	
	'% TaxCitySituat: Situaci�n ante el impuesto municipal
	'-----------------------------------------------------------
	Public Property TaxCitySituat() As Integer
		Get
			'-----------------------------------------------------------
			TaxCitySituat = mintTaxCitySituat
		End Get
		Set(ByVal Value As Integer)
			'-----------------------------------------------------------
			mintTaxCitySituat = Value
		End Set
	End Property
	
	'% TaxCityNumber: N�mero de inscripci�n impuesto municipal
	'-----------------------------------------------------------
	
	'% TaxCityNumber: N�mero de inscripci�n impuesto municipal
	'-----------------------------------------------------------
	Public Property TaxCityNumber() As Integer
		Get
			'-----------------------------------------------------------
			TaxCityNumber = mintTaxCityNumber
		End Get
		Set(ByVal Value As Integer)
			'-----------------------------------------------------------
			mintTaxCityNumber = Value
		End Set
	End Property
	
	'% StampSituat: Situaci�n sellado
	'-----------------------------------------------------------
	
	'% StampSituat: Situaci�n sellado
	'-----------------------------------------------------------
	Public Property StampSituat() As Integer
		Get
			'-----------------------------------------------------------
			StampSituat = mintStampSituat
		End Get
		Set(ByVal Value As Integer)
			'-----------------------------------------------------------
			mintStampSituat = Value
		End Set
	End Property
	
	'% ReadTaxSituat: Esta funci�n valida que el cliente tenga informaci�n impositiva registrada
	'%                y recupera la misma
	Public Function ReadTaxSituat(ByVal sClient As String) As Boolean
		Dim lrecTaxSituat As eRemoteDB.Execute
		
		lrecTaxSituat = New eRemoteDB.Execute
		
		On Error GoTo ReadTaxSituat_err
		
		ReadTaxSituat = True
		
		'+Si se encuentra un c�digo de cliente v�lido se procede a leer la tabla de clientes
		
		If Trim(sClient) <> "" Then
			lrecTaxSituat.StoredProcedure = "reaTaxSituat"
			lrecTaxSituat.Parameters.Add("sClient", sClient)
			If lrecTaxSituat.Run Then
				
				With lrecTaxSituat
					'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
					If Not IsDbNull(.FieldToClass("nProfi_tax")) Then
						mintProfitSituat = .FieldToClass("nProfi_tax")
					Else
						mintProfitSituat = 0
					End If
					If .FieldToClass("sNo_reten") = "1" Then
						mblnProfitNoReten = True
					Else
						mblnProfitNoReten = False
					End If
					If .FieldToClass("sOperative") = "1" Then
						mblnOperative = True
					Else
						mblnOperative = False
					End If
					If .FieldToClass("sAdjust") = "1" Then
						mblnAdjust = True
					Else
						mblnAdjust = False
					End If
					'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
					If Not IsDbNull(.FieldToClass("sPrevious")) Then
						mstrNetIncPreviusNumber = .FieldToClass("sPrevious")
					Else
						mstrNetIncPreviusNumber = CStr(0)
					End If
					'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
					If Not IsDbNull(.FieldToClass("sNew")) Then
						mstrNetIncNewNumber = .FieldToClass("sNew")
					Else
						mstrNetIncNewNumber = CStr(0)
					End If
					'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
					If Not IsDbNull(.FieldToClass("nAgent_inc")) Then
						mintProvinceAgentPercep = .FieldToClass("nAgent_inc")
					Else
						mintProvinceAgentPercep = 0
					End If
					'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
					If Not IsDbNull(.FieldToClass("nProvin_in")) Then
						mintProvinceInsc = .FieldToClass("nProvin_in")
					Else
						mintProvinceInsc = 0
					End If
					If .FieldToClass("sMutilat") = "1" Then
						mblnMultilatAgree = True
					Else
						mblnMultilatAgree = False
					End If
					'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
					If Not IsDbNull(.FieldToClass("nArticle")) Then
						mintMultilatAgreeArticle = .FieldToClass("nArticle")
					Else
						mintMultilatAgreeArticle = 0
					End If
					'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
					If Not IsDbNull(.FieldToClass("nProvi_out")) Then
						mintProvinceExempt = .FieldToClass("nProvi_out")
					Else
						mintProvinceExempt = 0
					End If
					'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
					If Not IsDbNull(.FieldToClass("nSijp_peop")) Then
						mintSijp_peop = .FieldToClass("nSijp_peop")
					Else
						mintSijp_peop = 0
					End If
					'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
					If Not IsDbNull(.FieldToClass("nSijp_comp")) Then
						mintSijp_comp = .FieldToClass("nSijp_comp")
					Else
						mintSijp_comp = 0
					End If
					'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
					If Not IsDbNull(.FieldToClass("nIva")) Then
						mintIvaSituat = .FieldToClass("nIva")
					Else
						mintIvaSituat = 0
					End If
					If .FieldToClass("sAgent_ret") = "1" Then
						mblnAgent_ret = True
					Else
						mblnAgent_ret = False
					End If
					If .FieldToClass("sProm_iva") = "1" Then
						mblnIvaProm = True
					Else
						mblnIvaProm = False
					End If
					'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
					If Not IsDbNull(.FieldToClass("dProm_from")) Then
						mdtmDateIvaPromFrom = .FieldToClass("dProm_from")
					Else
						mdtmDateIvaPromFrom = System.Date.FromOADate(0)
					End If
					'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
					If Not IsDbNull(.FieldToClass("dProm_to")) Then
						mdtmDateIvaPromTo = .FieldToClass("dProm_to")
					Else
						mdtmDateIvaPromTo = System.Date.FromOADate(0)
					End If
					If .FieldToClass("sConstance") = "1" Then
						mblnConstanceIvaExempt = True
					Else
						mblnConstanceIvaExempt = False
					End If
					'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
					If Not IsDbNull(.FieldToClass("nInspect")) Then
						mintTaxCitySituat = .FieldToClass("nInspect")
					Else
						mintTaxCitySituat = 0
					End If
					'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
					If Not IsDbNull(.FieldToClass("nReg_num")) Then
						mintTaxCityNumber = .FieldToClass("nReg_num")
					Else
						mintTaxCityNumber = 0
					End If
					'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
					If Not IsDbNull(.FieldToClass("nSell_tax")) Then
						mintStampSituat = .FieldToClass("nSell_tax")
					Else
						mintStampSituat = 0
					End If
				End With
				lrecTaxSituat.RCloseRec()
			Else
				ReadTaxSituat = False
			End If
		Else
			ReadTaxSituat = False
		End If
		
		'UPGRADE_NOTE: Object lrecTaxSituat may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecTaxSituat = Nothing
		
ReadTaxSituat_err: 
		If Err.Number Then
			ReadTaxSituat = False
		End If
		
		On Error GoTo 0
		
	End Function
End Class






