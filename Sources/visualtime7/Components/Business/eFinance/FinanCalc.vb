Option Strict Off
Option Explicit On
Public Class FinanCalc
	'%-------------------------------------------------------%'
	'% $Workfile:: FinanCalc.cls                            $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 1:25p                                $%'
	'% $Revision:: 4                                        $%'
	'%-------------------------------------------------------%'
	
	'- Enumerated type definition. This is going to be used for the calculation type
	'- Se define el tipo enumerado para el tipo de Calculo
	Public Enum Calc
		efnInitial = 1
		efnDraft = 2
	End Enum
	
	'- Public variables
	'- Variables públicas
	Public Amount_fi As Double
	Public nInterest As Double
	Public nQ_draft As Integer
	Public FrecuencyDraft As Integer
	Public AmountInitial As Double
	Public AmountQDraft As Double
	Private sInsval As String
	'% insValFI010: This method validates the page "FI010" as described in the functional specifications
	'% InsValFI010: Este metodo se encarga de realizar las validaciones descritas en el funcional
	'%              de la ventana "FI010"
	Public Function insValFI010(ByVal nInitial As Double, ByVal nAmountQDra As Double, ByVal nQ_draft As Double, ByVal nAmount_fi As Double, ByVal nFrequency As Integer, ByVal nInterest As Double) As String
		Dim lclsErrors As eFunctions.Errors
		Dim bCalculate As Boolean
		Dim nResult As Double
		
		On Error GoTo insValFI010_Err
		
		lclsErrors = New eFunctions.Errors
		
		'+ The Down Payment or the amount of each draft must exist
		'+ Validación que la cuota inicial o el monto de cada cuota exista
		If ((nInitial = 0 Or nInitial = eRemoteDB.Constants.intNull) And (nAmountQDra = 0 Or nAmountQDra = eRemoteDB.Constants.intNull)) Or (nInitial <> 0 And nInitial <> eRemoteDB.Constants.intNull And nAmountQDra <> 0 And nAmountQDra <> eRemoteDB.Constants.intNull) Then
			Call lclsErrors.ErrorMessage("FI010", 21023)
		End If
		
		'+ Draft quantity validations
		'+ Validación del campo cantidad de cuotas
		If nQ_draft = 0 Or nQ_draft = eRemoteDB.Constants.intNull Then
			Call lclsErrors.ErrorMessage("FI010", 21011)
		End If
		
		'+ The financing amount has to be filled
		'+ Validación que el monto a financiar no se encuentre vacio
		If nAmount_fi = 0 Or nAmount_fi = eRemoteDB.Constants.intNull Then
			Call lclsErrors.ErrorMessage("FI010", 21020)
		End If
		
		'+ The down payment (if it's filled) has to be minor than the financing amount
		'+ Validación que el campo de cuota inicial si está lleno sea menor que el importe a financiar
		If nInitial <> 0 And nInitial <> eRemoteDB.Constants.intNull Then
			If nAmount_fi <= nInitial Then
				Call lclsErrors.ErrorMessage("FI010", 21021)
			End If
		End If
		
		'+ The amount of each draft cannot be greater than the financing amount
		'+ Validación que si el campo Importe de los giros está lleno sea menor que el importe a financiar
		If nAmountQDra <> 0 And nAmountQDra <> eRemoteDB.Constants.intNull Then
			If nAmount_fi <= nAmountQDra Then
				Call lclsErrors.ErrorMessage("FI010", 21022)
			End If
		End If
		
		'+ When the calculation button is pushed the system makes this validations
		'+ Validaciones cuando presiona el boton del calcular
		If Trim(lclsErrors.Confirm) = String.Empty Then
			If nAmountQDra = 0 Or nAmountQDra = eRemoteDB.Constants.intNull Then
				nResult = insCalculate(Calc.efnDraft, nInterest, nAmount_fi, nInitial, nAmountQDra, nFrequency, nQ_draft)
				If nResult = 0 Then
					Call lclsErrors.ErrorMessage("FI010", 21021)
				End If
				Me.AmountQDraft = CDbl(Format(nResult, "############0.00"))
				Me.AmountInitial = nInitial
			Else
				nResult = insCalculate(Calc.efnInitial, nInterest, nAmount_fi, nInitial, nAmountQDra, nFrequency, nQ_draft)
				If nResult = 0 Then
					Call lclsErrors.ErrorMessage("FI010", 21022)
				End If
				Me.AmountInitial = CDbl(Format(nResult, "############0.00"))
				Me.AmountQDraft = nAmountQDra
			End If
		End If
		
		insValFI010 = lclsErrors.Confirm
		
insValFI010_Err: 
		If Err.Number Then
			insValFI010 = insValFI010 & Err.Description
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
	End Function
	
	'% insCalculate: This function makes the calculation of the down payment amount or the drafts amount
	'% insCalculate: Función que se encarga de hacer los calculos del giro o de la cuota inicial
	Public Function insCalculate(ByVal whoCal As Integer, ByVal nInterest As Double, ByVal nAmount_fi As Double, ByVal nInitial As Double, ByVal nAmountQDra As Double, ByVal nFrequency As Double, ByVal nQ_draft As Double) As Double
		Dim ldblInterest As Double
		Dim ldblResult As Double
		
		ldblInterest = inscalInterest(nInterest, nFrequency)
		
		If whoCal = Calc.efnInitial Then
			
			'+ If there is no interest, the system calculates the down payment
			'+ Se calcula la cuota inicial si no hay intereses
			If nInterest = 0 Then
				ldblResult = nAmount_fi - (nAmountQDra * nQ_draft)
			Else
				
				'+ Calculation of the down payment if there is interest
				'+ Se calcula en la cuota inicial si hay intereses
				ldblResult = nAmount_fi - (nAmountQDra * (1 - (1 / ((1 + ldblInterest) ^ nQ_draft))) / ldblInterest)
			End If
		Else
			
			'+ The amount of the drafts is calculated
			'+ Se calcula el monto de los giros
			If nQ_draft = 0 Then
				ldblResult = ((nAmount_fi - nInitial) * ldblInterest)
			Else
				ldblResult = ((nAmount_fi - nInitial) * ldblInterest) / (1 - (1 / ((1 + ldblInterest) ^ nQ_draft)))
			End If
		End If
		
		'+ If the result is negative the function returns 0
		'+ Retorna el valor 0 si el resultado es negativo
		If ldblResult <= 0 Then
			insCalculate = 0
		Else
			insCalculate = ldblResult
		End If
	End Function
	
	'% inscalInterest: This function calculates the insteres according to the period of time
	'% inscalInterest: Esta función se encarga de calcular el Interes segun el tiempo
	Private Function inscalInterest(ByVal nInterest As Double, ByVal FrecuencyDraft As Integer) As Double
		Dim lintFrecuency As Integer
		
		'+ The system calculates the interest according  to the frequency of expiration date of the drafts
		'+ Se procede a calcular el interes segun la frecuencia de los giros
		
		'+ If the frequency of expire date of the drafts is not unifom the system takes the anual rate
		'+ Si es no uniforme se toma el interes Anual
		Select Case FrecuencyDraft
			Case financeCO.eFrequency.efNot_Stand
				
				'+ The system takes the whole year
				'+ Se toma El año completo
				lintFrecuency = 1
			Case financeCO.eFrequency.efMonthly
				
				'+The system takes 12 months
				'+ Se toman los doce meses
				lintFrecuency = 12
			Case financeCO.eFrequency.efQuarterly
				
				'+ The system takes the four trimester of the year
				'+ Se toman los cuatro trimestres de año
				lintFrecuency = 4
		End Select
		
		If FrecuencyDraft = 0 Then
			lintFrecuency = 1
		End If
		
		inscalInterest = (nInterest / lintFrecuency) / 100
		
	End Function
End Class






