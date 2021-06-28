Option Strict Off
Option Explicit On
Public Class FinanceWin
	'%-------------------------------------------------------%'
	'% $Workfile:: FinanceWin.cls                           $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 3/05/04 3:56p                                $%'
	'% $Revision:: 10                                       $%'
	'%-------------------------------------------------------%'
	'**-Defines the constant that holds the quantity of frames in the sequence
	'- Se define la constante que define el número de frames de la secuencia
	
	Const MAX_FRAME As Short = 5
	
	Public Enum eImageTabs
		eitNone
		eitRequired
		eitOK
	End Enum
	
	Public Enum eContent
		ecContent
		ecWithOutContent
	End Enum
	
	Private Structure udtInfoWin
		Dim sCodisp As String
		Dim sCodispl As String
		Dim sShort_des As String
		Dim bRequired As Boolean
		Dim bContent As Boolean
		Dim nWindowty As Integer
		Dim bDefaulti As Boolean
		Dim bVisible_ As Boolean
	End Structure
	
	Private aInfoWin(MAX_FRAME) As udtInfoWin
	Private mintIndex As Integer
	Private mefAction As financeCO.eFinanceTransac
	Private mintFreq As Integer
	
	'**+ Properties according the table in the system 08/23/1999
	'+ Propiedades según la tabla en el sistema 23/08/1999
	
	'   Column_name                           Type                           Length Prec  Scale Nullable                            TrimTrailingBlanks                  FixedLenNullInSource
	'   ------------------------------------  ------------------------------ ------ ----- ----- ----------------------------------- ----------------------------------- -----------------------------------
	
	Public dCompdate As Date 'datetime                       8                  yes                                 (n/a)                               (n/a)
	Public nContrat As Double 'int                            4      10    0     no                                  (n/a)                               (n/a)
	Public nUsercode As Integer 'smallint                       2      5     0     yes                                 (n/a)                               (n/a)
	Public sV_concontr As String 'char                           30                 yes                                 yes                                 yes
	Public sV_wincontr As String 'char                           240                yes                                 yes                                 yes
	
	Public sCodispl As String
	Public sShort_des As String
	Public bRequired As Boolean
	Public bContent As Boolean
	Public nWindowty As Integer
	Public bDefaulti As Boolean
	
	'**- Properties of the table Tab_winfin
	'- Propiedades de la tabla Tab_winfin
	'- Column_name                      Type                           Length Prec  Scale Nullable                            TrimTrailingBlanks                  FixedLenNullInSource
	'------------------------------     ------------------------------ ------ ----- ----- ----------------------------------- ----------------------------------- -----------------------------------
	Public nTratypec As Integer 'smallint                       2      5     0     no                                  (n/a)                               (n/a)
	Public nSequence As Integer 'smallint                       2      5     0     no                                  (n/a)                               (n/a)
	
	Public sDefaulti As String 'char                           1                  yes                                 yes                                 yes
	Public sRequire As String 'char                           1                  yes                                 yes                                 yes
	
	Public lclsTab_Win As Tab_winFin
	Public lcolTab_Win As Tab_winFins
	
	'%Add_Finan_win: Updates the sequence of the frames of the financing module, (1.- Not filled 2.- Filled)
	'%Add_Finan_win: actualiza la secuencia de los frame de financiamiento, es decir, 1.- Sin Contenido y 2.-Con Contenido
	Public Function Add_Finan_win(ByVal nContrat As Double, ByVal dEffecdate As Date, ByVal sCodispl As String, ByVal sContent As String, ByVal nUsercode As Integer, ByVal nAction As Integer, Optional ByVal bNotLoadTab As Boolean = True) As Boolean
		Dim llngCount As Integer
		Dim llngTop As Integer
		Dim lstrV_wincontr As String
		Dim lstrV_concontr As String
		Dim lstrAuxCodispl As String
		
		
		lstrV_wincontr = String.Empty
		lstrV_concontr = String.Empty
		
		If bNotLoadTab Then
			If FindFinanc_win(nAction, nContrat) Then
				Do While Len(sV_wincontr) Mod 8 <> 0
					sV_wincontr = sV_wincontr & " "
				Loop 
				
				lstrV_wincontr = sV_wincontr
				lstrV_concontr = sV_concontr
				
				'**+ Updates the value of sV_concontr with the new information
				'+ Se modifica el valor de sV_conprodu con el nuevo contenido
				llngTop = Len(Trim(sV_concontr)) - 1
				For llngCount = 0 To llngTop
					lstrAuxCodispl = Trim(Mid(lstrV_wincontr, llngCount * 8 + 1, 8))
					If lstrAuxCodispl = sCodispl Then
						lstrV_concontr = Mid(lstrV_concontr, 1, llngCount) & sContent & Mid(lstrV_concontr, llngCount + 2)
						Exit For
					End If
				Next llngCount
			End If
		Else
			If FindFinanc_win(nAction, nContrat) Then
				lstrV_wincontr = sV_wincontr
				lstrV_concontr = sV_concontr
				If lstrV_concontr = String.Empty Then
					lcolTab_Win = New Tab_winFins
					Call lcolTab_Win.Find(nAction, 0)
					Call makeDataSequence()
					lstrV_wincontr = sV_wincontr
					lstrV_concontr = sV_concontr
				End If
			Else
				lcolTab_Win = New Tab_winFins
				Call lcolTab_Win.Find(nAction, 0)
				Call makeDataSequence()
				lstrV_wincontr = sV_wincontr
				lstrV_concontr = sV_concontr
			End If
		End If
		Me.nContrat = nContrat
		Me.nUsercode = nUsercode
		Me.sV_concontr = lstrV_concontr
		Me.sV_wincontr = lstrV_wincontr
		Add_Finan_win = insFinanceWin
		
insProd_win: 
		If Err.Number Then
			Add_Finan_win = False
		End If
		On Error GoTo 0
	End Function
	
	
	'**% LoadWin: Loads the sequence of windows to the contract
	'% LoadWin: se carga la secuencia de ventanas para el contrato
	Public Function LoadTabs(ByVal nTransaction As financeCO.eFinanceTransac, ByVal nContrat As Double, ByVal dEffecdate As Date, ByVal nUsercode As Integer, ByVal sSche_code As String) As String
		Dim lclsSequence As eFunctions.Sequence
		Dim lclsFinanceCO As financeCO
		Dim lclsTab_Win As Tab_winFin

        Dim lstrHTMLCode As String = ""
        Dim nAction As Integer
		
		lclsSequence = New eFunctions.Sequence
		lclsFinanceCO = New financeCO
		lclsTab_Win = New Tab_winFin
		
		
		Me.nContrat = nContrat
		Me.nUsercode = nUsercode
		
		nAction = IIf(nTransaction = financeCO.eFinanceTransac.eftQuerycontrat, eFunctions.Menues.TypeActions.clngActionQuery, eFunctions.Menues.TypeActions.clngActionUpdate)
		
		If lclsFinanceCO.Find(nContrat, dEffecdate) Then
			lstrHTMLCode = lclsSequence.makeTable
			
			If insLoadWindows(nTransaction, lclsFinanceCO.nFrequency) Then
				Call FindFinanc_win(nTransaction, nContrat)
				For mintIndex = 0 To MAX_FRAME
					If aInfoWin(mintIndex).sCodispl <> String.Empty Then
						bRequired = aInfoWin(mintIndex).bRequired
						bContent = aInfoWin(mintIndex).bContent
						If aInfoWin(mintIndex).bVisible_ Then
							lstrHTMLCode = lstrHTMLCode & lclsSequence.makeRow(aInfoWin(mintIndex).sCodisp, aInfoWin(mintIndex).sCodispl, nAction, aInfoWin(mintIndex).sShort_des, TabsImage(aInfoWin(mintIndex).sCodispl, sSche_code))
						End If
					Else
						Exit For
					End If
				Next mintIndex
			End If
			lstrHTMLCode = lstrHTMLCode & lclsSequence.closeTable
		End If
		
		Call Add_Finan_win(nContrat, dEffecdate, "", "", nUsercode, nTransaction, False)
		
		LoadTabs = lstrHTMLCode
	End Function
	
	'**% makeDataSequence: Filles the fields associated to the sequence of the Financ_win
	'% makeDataSequence: se llenan de valor los campos asociados a la secuencia en Financ_win
	Private Sub makeDataSequence()
		sV_concontr = ""
		sV_wincontr = ""
		'**+Creates the sequence to the financing
		'+ Se crea la secuencia para el Financiamiento
		For	Each lclsTab_Win In lcolTab_Win
			sV_concontr = sV_concontr & "1"
			sV_wincontr = sV_wincontr & lclsTab_Win.sCodispl & New String(" ", 8 - Len(lclsTab_Win.sCodispl))
		Next lclsTab_Win
	End Sub
	
	'**% insLoadWindows: Loads the sequence of windows to a contract number
	'% insLoadWindows: Carga la secuencia de ventanas para un nro. de contrato
	Private Function insLoadWindows(ByVal nTransaction As financeCO.eFinanceTransac, ByVal nFrequency As Integer) As Boolean
		Dim lclsQuery As eRemoteDB.Query
		lclsQuery = New eRemoteDB.Query
		
		mintIndex = -1
		
		insLoadWindows = False
		
		With lclsQuery
			If .OpenQuery("Tab_WinFin, Windows", "Tab_WinFin.sCodispl , Tab_WinFin.sDefaulti, Tab_WinFin.sRequire, Windows.sShort_des, Windows.nWindowty, Windows.sCodisp", "nTratypec =  " & CStr(nTransaction) & " and Tab_WinFin.sCodispl = Windows.sCodispl", "Tab_WinFin.nSequence") Then
				mefAction = nTransaction
				
				Do While Not .EndQuery
					mintIndex = mintIndex + 1
					aInfoWin(mintIndex).sCodisp = .FieldToClass("sCodisp")
					aInfoWin(mintIndex).sCodispl = .FieldToClass("sCodispl")
					aInfoWin(mintIndex).sShort_des = .FieldToClass("sShort_des")
					aInfoWin(mintIndex).bRequired = .FieldToClass("sRequire") = "1"
					aInfoWin(mintIndex).bContent = False
					aInfoWin(mintIndex).nWindowty = .FieldToClass("nWindowty")
					aInfoWin(mintIndex).bDefaulti = .FieldToClass("sDefaulti") = "1"
					aInfoWin(mintIndex).bVisible_ = True
					
					
					If nFrequency = financeCO.eFrequency.efNot_Stand Then
						'**+If the frequency of the drafts is "Not uniform", the system doesn't load the automatic drafts window
						'+ Si la frequencia de los giros es "No uniforme", no debe cargarse la ventana de Giros Automáticos
						If aInfoWin(mintIndex).sCodispl = "FI004" Then
							aInfoWin(mintIndex).bVisible_ = False
							aInfoWin(mintIndex).bRequired = False
						End If
						
						If aInfoWin(mintIndex).sCodispl = "FI011" Then
							aInfoWin(mintIndex).bVisible_ = True
							aInfoWin(mintIndex).bRequired = True
						End If
					Else
						'**+If the frequency of the drafts is "Uniform", the system doesn't load the manual drafts window
						'+ Si la frequencia de los giros es diferente a "No uniforme", no debe cargarse la ventana de Giros Manuales
						If aInfoWin(mintIndex).sCodispl = "FI011" Then
							aInfoWin(mintIndex).bVisible_ = False
							aInfoWin(mintIndex).bRequired = False
						End If
					End If
					
					.NextRecord()
				Loop 
				If mintIndex < MAX_FRAME Then
					aInfoWin(mintIndex + 1).sCodispl = String.Empty
				End If
				
				lclsQuery.CloseQuery()
				insLoadWindows = True
			End If
		End With
	End Function
	
	'**%'**insFinanceWin: This method validates the header section of the page "XXXXXX" as described in the
	'**%functional specifications
	Private Function insFinanceWin() As Boolean
		
		Dim lrecinsFinanc_win As eRemoteDB.Execute
		
		On Error GoTo lrecinsFinanc_win_err
		
		lrecinsFinanc_win = New eRemoteDB.Execute
		
		'**+Stored procedure parameters definition 'insudb.insFinanc_win'
		'**+Data of 08/24/1999 02:44:42 PM
		'+ Definición de parámetros para stored procedure 'insudb.insFinanc_win'
		'+ Información leída el 24/08/1999 02:44:42 PM
		
		With lrecinsFinanc_win
			.StoredProcedure = "insFinanc_win"
			.Parameters.Add("nContrat", nContrat, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sV_concontr", sV_concontr, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 30, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sV_wincontr", sV_wincontr, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 240, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			insFinanceWin = .Run(False)
		End With
		
lrecinsFinanc_win_err: 
		If Err.Number Then
			insFinanceWin = False
		End If
		
		'UPGRADE_NOTE: Object lrecinsFinanc_win may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecinsFinanc_win = Nothing
		On Error GoTo 0
	End Function
	'% FindFinanc_win: Searches for the data associated to the sequence for a given contract
	'% FindFinanc_win: busca los datos asociados a la secuencia para un contrato dado
	Public Function FindFinanc_win(ByVal nTransaction As financeCO.eFinanceTransac, ByVal nContrat As Double) As Boolean
		Dim lrecreaFinanc_win As eRemoteDB.Execute
		Dim intIndex As Integer
		Dim lstrContent As String
		
		On Error GoTo FindFinanc_winErr
		
		lrecreaFinanc_win = New eRemoteDB.Execute
		
		'**+Stored procedure parameters definition 'insudb.reaFinanc_win'
		'**+Data of 08/24/1999 02:05:20 PM
		'+ Definición de parámetros para stored procedure 'insudb.reaFinanc_win'
		'+ Información leída el 24/08/1999 02:05:20 PM
		
		With lrecreaFinanc_win
			.StoredProcedure = "reaFinanc_win"
			.Parameters.Add("nContrat", nContrat, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
				If Not IsDbNull(.FieldToClass("sV_concontr")) Then
					sV_wincontr = .FieldToClass("sV_wincontr")
					sV_concontr = .FieldToClass("sV_concontr")
				End If
				.RCloseRec()
				
				sV_wincontr = Left(sV_wincontr & Space(240), 240)
				sV_concontr = Left(sV_concontr & Space(30), 30)
				
				For intIndex = 1 To MAX_FRAME * 8 Step 8
					If FindCodispl(Trim(Mid(sV_wincontr, intIndex, 8))) Then
						lstrContent = Mid(sV_concontr, Int(intIndex / 8 + 1), 1)
						aInfoWin(mintIndex).bContent = lstrContent = "2"
						'+Solo si no es requerida por omision se hace segunda verificación
						If Not aInfoWin(mintIndex).bRequired Then
							aInfoWin(mintIndex).bRequired = lstrContent = "3"
						End If
						If nTransaction = financeCO.eFinanceTransac.eftQuerycontrat And Not aInfoWin(mintIndex).bContent Then
							aInfoWin(mintIndex).bVisible_ = False
						End If
					End If
				Next intIndex
				FindFinanc_win = True
			Else
				FindFinanc_win = False
			End If
		End With
		
FindFinanc_winErr: 
		If Err.Number Then
			FindFinanc_win = False
		End If
		'UPGRADE_NOTE: Object lrecreaFinanc_win may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaFinanc_win = Nothing
	End Function
	'* Property Get TabsImage: Assings the corresponding image to the tab of the sequence
	'* Property Get TabsImage: se asigna la imagen de la pestaña de la secuencia
	Public ReadOnly Property TabsImage(ByVal sCodispl As String, ByVal sSche_code As String) As eFunctions.Sequence.etypeImageSequence
		Get
			Dim lclsSecurity As eSecurity.Secur_sche
			Dim lblnValid As Boolean
			
			lclsSecurity = New eSecurity.Secur_sche
			
			lblnValid = True
			With lclsSecurity
				If Not .valTransAccess(sSche_code, sCodispl, "1") Then
					lblnValid = False
				End If
			End With
			If lblnValid Then
				If bRequired Then
					TabsImage = eFunctions.Sequence.etypeImageSequence.eRequired
				Else
					TabsImage = eFunctions.Sequence.etypeImageSequence.eEmpty
				End If
			Else
				If bRequired Then
					TabsImage = eFunctions.Sequence.etypeImageSequence.eDeniedReq
				Else
					TabsImage = eFunctions.Sequence.etypeImageSequence.eDeniedS
				End If
			End If
			
			If bContent Then
				TabsImage = IIf(lblnValid, eFunctions.Sequence.etypeImageSequence.eOK, eFunctions.Sequence.etypeImageSequence.eDeniedOK)
			End If
			'UPGRADE_NOTE: Object lclsSecurity may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
			lclsSecurity = Nothing
		End Get
	End Property
	
	'**% FindCodispl: Verifies if the "codispl" is in the array
	'% FindCodispl: verifica si el codispl se encuentra en el arreglo
	Private Function FindCodispl(ByVal Codispl As String) As Boolean
		For mintIndex = 0 To MAX_FRAME
			If Trim(aInfoWin(mintIndex).sCodispl) = Codispl Then
				FindCodispl = True
				Exit For
			End If
		Next mintIndex
	End Function
	
	'**% IsPageRequired: Verifies if the contrat has required windows without content
	'% IsPageRequired: Verifica si el contrato tiene ventanas requeridas sin contenido
	Public Function IsPageRequired(ByVal nContrat As Double, ByVal dEffecdate As Date, ByVal nTransaction As financeCO.eFinanceTransac) As Boolean
		Dim lclsFinace_co As financeCO
		
		On Error GoTo IsPageRequired_Err
		lclsFinace_co = New financeCO
		IsPageRequired = False
		
		If lclsFinace_co.Find(nContrat, dEffecdate) Then
			If insLoadWindows(nTransaction, lclsFinace_co.nFrequency) Then
				Call FindFinanc_win(nTransaction, nContrat)
				For mintIndex = 0 To MAX_FRAME
					If aInfoWin(mintIndex).bRequired And Not aInfoWin(mintIndex).bContent Then
						IsPageRequired = True
						Exit For
					End If
				Next mintIndex
			End If
		End If
IsPageRequired_Err: 
		If Err.Number Then
			IsPageRequired = True
		End If
		'UPGRADE_NOTE: Object lclsFinace_co may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsFinace_co = Nothing
		On Error GoTo 0
	End Function
	
	'**% insConcatMessage: Return the string resulted of the concatenation of two strings
	'% insConcatMessage: Función que devuelve un string, resultado de la concatenación de dos cadenas.
	Public Function insConcatMessage(ByVal lstrString1 As String, ByVal lintError As Integer) As String
		Dim lstrStringA As String
		Dim lstrStringB As String
		Dim lobjGeneral As eGeneral.GeneralFunction
		
		lobjGeneral = New eGeneral.GeneralFunction
		
		lstrStringA = Trim(lstrString1)
		lstrStringB = Trim(lobjGeneral.insLoadMessage(lintError))
		
		If lstrStringA = String.Empty Then
			insConcatMessage = "- " & lstrStringB & "."
		Else
			insConcatMessage = Chr(13) & Chr(10) & "- " & lstrStringB & "."
		End If
		
		'UPGRADE_NOTE: Object lobjGeneral may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lobjGeneral = Nothing
	End Function
End Class






