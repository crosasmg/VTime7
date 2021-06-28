Option Strict Off
Option Explicit On
Public Class Cur_Allow
	'%-------------------------------------------------------%'
	'% $Workfile:: Cur_Allow.cls                            $%'
	'% $Author:: Clobos                                     $%'
	'% $Date:: 30/01/06 18:04                               $%'
	'% $Revision:: 3                                        $%'
	'%-------------------------------------------------------%'
	
	'**-Properties of the table on November 03,2000
	'**-The key fields corresponds to nBranch, nProduct, nCurrency
	'-Propiedades de la tabla al 03/11/2000
	'-Los campos llaves corresponden a nBranch , nProduct, nCurrency
	
	'  Column_name                         Type       Computed   Length      Prec  Scale Nullable      TrimTrailingBlanks     FixedLenNullInSource
	'  ----------------------------------- ---------- ---------- ----------- ----- ----- ------------- ---------------------- --------------------
	Public nBranch As Integer 'smallint  no         2           5     0     no            (n/a)                  (n/a)
	Public nProduct As Integer 'smallint  no         2           5     0     no            (n/a)                  (n/a)
	Public nCurrency As Integer 'smallint  no         2           5     0     no            (n/a)                  (n/a)
	Public dCompdate As Date 'datetime  no         8                       yes           (n/a)                  (n/a)
	Public sDefaulti As String 'char      no         1                       yes           yes                    yes
	Public nUsercode As Integer 'smallint  no         2           5     0     yes           (n/a)                  (n/a)
	
	'**-Auxiliary properties
	'-Propiedades auxiliares
	Public dEffecdate As Date
	Public sDescript As String
	Public nExchange As Double
	Public nCodigInt As Double
	
	Private Structure udtCur_allow
		Dim nCurrency As Integer
		Dim sDefaulti As Integer
		Dim nExchange As Double
		Dim Existe As Integer
		Dim sDescript As String
	End Structure
	
	Private arrCur_allow() As udtCur_allow
	
	'**%Find: Function that charges the information of the allowed currencies of the "Cur_allow" table
	'**%in the arrengement of the class.
	'%Find: Función que carga la información de las monedas permitidas de la tabla "Cur_allow"
	'%en el arreglo de la clase
	Public Function Find(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal dEffecdate As Date) As Boolean
		Dim lrecreaCur_allow_tmp As eRemoteDB.Execute
		Dim lintCount As Integer
		
		On Error GoTo Find_Err
		If Me.nBranch <> nBranch Or Me.nProduct <> nProduct Or Me.dEffecdate <> dEffecdate Then
			
			'**+Parameter definition for stored procedure 'insudb.reaCur_allow_tmp'
			'**+Information read on November 05, 1999  08:58:52 a.m.
			'+Definición de parámetros para stored procedure 'insudb.reaCur_allow_tmp'
			'+Información leída el 05/11/1999 08:58:52 AM
			lintCount = 0
			lrecreaCur_allow_tmp = New eRemoteDB.Execute
			With lrecreaCur_allow_tmp
				.StoredProcedure = "reaCur_allow_tmp"
				.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				If .Run Then
					Find = True
					ReDim arrCur_allow(100)
					Do While Not .EOF
						arrCur_allow(lintCount).nCurrency = .FieldToClass("nCurrency")
						arrCur_allow(lintCount).sDescript = .FieldToClass("sDescript")
						arrCur_allow(lintCount).sDefaulti = .FieldToClass("sDefaulti")
						arrCur_allow(lintCount).nExchange = .FieldToClass("nExchange")
						lintCount = lintCount + 1
						.RNext()
					Loop 
					.RCloseRec()
					ReDim Preserve arrCur_allow(lintCount - 1)
					Me.nBranch = nBranch
					Me.nProduct = nProduct
					Me.dEffecdate = dEffecdate
				Else
					ReDim Preserve arrCur_allow(lintCount)
				End If
			End With
		Else
			Find = True
		End If
		
Find_Err: 
		If Err.Number Then
			Find = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecreaCur_allow_tmp may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaCur_allow_tmp = Nothing
	End Function
	'**%insValDP055: Validates the data of the page "DP055" - Allows currencies for the policy
	'%insValDP055: Valida los datos de la página "DP055" - Monedas permitidas para la póliza
	Public Function insValDP055(ByVal sCodispl As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal dEffecdate As Date, ByVal nSel As Integer, ByVal nPresel As Integer, ByVal sSel As String, ByVal sCodigint As String) As String
		
		On Error GoTo insValDP055_err
		
		Dim lclsProduct As eProduct.Product
		Dim lclsErrors As eFunctions.Errors
		
		lclsProduct = New eProduct.Product
		lclsErrors = New eFunctions.Errors
		
		'**+Obtain the product's data.
		'+Se obtienen los datos del producto.
		Call lclsProduct.Find(nBranch, nProduct, dEffecdate)
		
		'**+Verifies that currencies, that not are predefined for the product, are not selected
		'+Verifica que no existan seleccionadas mas monedas que las predefinidas para el producto.
		'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
		If Not IsDbNull(lclsProduct.nQmaxcurr) Then
			If (nPresel) > lclsProduct.nQmaxcurr Then
				Call lclsErrors.ErrorMessage(sCodispl, 11208,  ,  ,  , True)
			End If
		End If
		
		'**+Verifies that the local currency is selected.
		'+Verifica que la moneda local esté seleccionada
		If (lclsProduct.sStyle_comm = "2" Or lclsProduct.sStyle_prem = "2" Or lclsProduct.sStyle_tax = "2") And Not insValLocalCurrDP055(sSel, sCodigint) Then
			Call lclsErrors.ErrorMessage(sCodispl, 11385)
		End If
		
		'**+Verifies that there is at least one selected element.
		'+Verifica que exista por lo menos un elemento seleccionado.
		If nSel <= 0 Then
			Call lclsErrors.ErrorMessage(sCodispl, 12125)
		End If
		
		insValDP055 = lclsErrors.Confirm
		
		'UPGRADE_NOTE: Object lclsProduct may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsProduct = Nothing
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
		
insValDP055_err: 
		If Err.Number Then
			insValDP055 = "insValDP055" & " " & Err.Description
		End If
		On Error GoTo 0
		
	End Function
	
	'**%insPostDP055: This method updates the database (as described in the functional specifications)
	'**%for the page "DP055"
	'%insPostDP055: Este metodo se encarga de realizar el impacto en la base de datos (descrito en las
	'%especificaciones funcionales)de la ventana "DP055"
	Public Function insPostDP055(ByVal sCodispl As String, ByVal nAction As Integer, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal dEffecdate As Date, ByVal nUsercode As Integer, ByVal sSel As String, ByVal sCurrency As String, ByVal sDefaulti As String, ByVal sCodigint As String) As Boolean
		On Error GoTo insPostDP055_err
		If nAction <> eFunctions.Menues.TypeActions.clngActionQuery Then
			insPostDP055 = insUpdDP055(sCodispl, nBranch, nProduct, dEffecdate, nUsercode, sSel, sCurrency, sDefaulti, sCodigint)
		Else
			insPostDP055 = True
		End If
		
insPostDP055_err: 
		If Err.Number Then
			insPostDP055 = False
		End If
		On Error GoTo 0
		
	End Function
	
	'**%insUpdDP055: function that runs each one of the records of Tdfgrid, making the specific update
	'**%in the table "Cur_allow"
	'%insUpdDP055: función que recorre c/u de los registros del Tdbgrid, realizando las actualizaciones
	'%específicas en la tabla Cur_allow
	Private Function insUpdDP055(ByVal sCodispl As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal dEffecdate As Date, ByVal nUsercode As Integer, ByVal sSel As String, ByVal sCurrency As String, ByVal sDefaulti As String, ByVal sCodigint As String) As Boolean
		
		On Error GoTo insUpdDP055_err
		
		Dim lblnContinue As Boolean
		
		'**-Separators index (,)
		'-Inidice de separadores (,)
		Dim nStartCur As Integer
		Dim nStartDef As Integer
		Dim nStartSel As Integer
		Dim nStartCod As Integer
		
		'**-Obtained values
		'-Valores obtenidos.
		Dim nAuxCur As Integer
		Dim sAuxDef As String
		Dim sAuxSel As String
		Dim nAuxCod As Integer
		
		'**-Objects to handle the database.
		'-Objetos para el manejo de base de datos.
		Dim lrecinsCur_allow As eRemoteDB.Execute
		Dim lrecdelCur_allow As eRemoteDB.Execute
		Dim lclsProd_win As eProduct.Prod_win
		
		lrecdelCur_allow = New eRemoteDB.Execute
		lrecinsCur_allow = New eRemoteDB.Execute
		lclsProd_win = New eProduct.Prod_win
		
		'**+Indicates the continuity of the process.
		'+Indica la continuidad del proceso
		lblnContinue = True
		insUpdDP055 = True
		
		Do While lblnContinue
			
			'**+locate the position of the next separator.
			'+Se ubica la posición del próximo separador
			nStartCur = InStr(sCurrency, ",")
			nStartDef = InStr(sDefaulti, ",")
			nStartSel = InStr(sSel, ",")
			nStartCod = InStr(sCodigint, ",")
			
			'**+Obtain the values of the parameters.
			'+Se obtienen los valores de los parámetros
			If lblnContinue Then
				If nStartCur > 0 Or nStartDef > 0 Or nStartSel > 0 Or nStartCod > 0 Then
					nAuxCur = CInt(Mid(sCurrency, 1, nStartCur - 1))
					sAuxDef = Trim(Mid(sDefaulti, 1, nStartDef - 1))
					sAuxSel = Trim(Mid(sSel, 1, nStartSel - 1))
					nAuxCod = CInt(Mid(sCodigint, 1, nStartCod - 1))
					
					sCurrency = Right(sCurrency, Len(sCurrency) - nStartCur)
					sDefaulti = Right(sDefaulti, Len(sDefaulti) - nStartDef)
					sSel = Right(sSel, Len(sSel) - nStartSel)
					sCodigint = Right(sCodigint, Len(sCodigint) - nStartCod)
				Else
					nAuxCur = CInt(sCurrency)
					sAuxDef = sDefaulti
					sAuxSel = sSel
					nAuxCod = CInt(sCodigint)
					lblnContinue = False
				End If
				
				If sAuxSel = "1" Then
					'**+Parameter definition for stored procedure 'insudb.insCur_allow'
					'**+Information read on April 10,20001  10:46:14 a.m.
					'+Definición de parámetros para stored procedure 'insudb.insCur_allow'
					'+Información leída el 10/04/2001 10:46:16 a.m.
					
					With lrecinsCur_allow
						.StoredProcedure = "insCur_allow"
						.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
						.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
						.Parameters.Add("nCurrency", nAuxCod, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
						.Parameters.Add("sDefaulti", sAuxDef, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
						.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
						.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
						If Not .Run(False) Then
							insUpdDP055 = False
						End If
					End With
					
				ElseIf sAuxSel = String.Empty And nAuxCur > 0 Then 
					
					'**+Parameter definition for stored procedure 'insudb.delCur_allow'
					'**+Information read on April 10,2001  10:50:09 a.m.
					'+Definición de parámetros para stored procedure 'insudb.delCur_allow'
					'+Información leída el 10/04/2001 10:50:09 a.m.
					
					With lrecdelCur_allow
						.StoredProcedure = "delCur_allow"
						.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
						.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
						.Parameters.Add("nCurrency", nAuxCur, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
						.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
						.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
						If Not .Run(False) Then
							insUpdDP055 = False
						End If
					End With
				End If
			End If
		Loop 
		
		If insUpdDP055 Then
			Call lclsProd_win.Add_Prod_win(nBranch, nProduct, dEffecdate, sCodispl, "2", nUsercode)
		Else
			Call lclsProd_win.Add_Prod_win(nBranch, nProduct, dEffecdate, sCodispl, "1", nUsercode)
		End If
		
		'UPGRADE_NOTE: Object lrecinsCur_allow may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecinsCur_allow = Nothing
		'UPGRADE_NOTE: Object lrecdelCur_allow may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecdelCur_allow = Nothing
		'UPGRADE_NOTE: Object lclsProd_win may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsProd_win = Nothing
		
insUpdDP055_err: 
		If Err.Number Then
			insUpdDP055 = False
		End If
		On Error GoTo 0
	End Function
	
	'**%insValLocalCurrDP055: Function that reads the xarray that contains the selected currencies to
	'**%validate the selection of the local currency.
	'%insValLocalCurrDP055: Función que recorre el xarray que contiene las monedas seleccionadas para
	'%validar la selección de la moneda local
	Private Function insValLocalCurrDP055(ByVal sSel As String, ByVal sCodigint As String) As Boolean
		
		Dim lblnContinue As Boolean
		
		'**-Index of run for both character chains.
		'-Indices de recorido para ambas cadenas de caracteres.
		Dim intStartSel As Integer
		Dim intStartCod As Integer
		
        lblnContinue = True
		insValLocalCurrDP055 = False
		Do While lblnContinue
			
			'**+Locate the position of the next separator.
			'+Se ubica la posición del próximo separador
			intStartSel = InStr(sSel, ",")
			intStartCod = InStr(sCodigint, ",")
			
			'**+Evaluate the next element...
			'+Se evalua el próximo elemento...
			If intStartSel > 0 And intStartCod > 0 Then
				If Mid(sCodigint, 1, intStartCod - 1) = "1" Then
					lblnContinue = False
					If Trim(Mid(sSel, 1, intStartSel - 1)) = "1" Then
						insValLocalCurrDP055 = True
					End If
				End If
				'**+Evaluate the last element
				'+Se evalúa el último elemento
			Else
				If sCodigint = "1" And sSel = "1" Then
					insValLocalCurrDP055 = True
				End If
				lblnContinue = False
			End If
			
			'**+The processed values are detached from the chain
			'+Se desprende de la cadena los valores ya procesados.
			sSel = Trim(Right(sSel, Len(sSel) - intStartSel))
			sCodigint = Trim(Right(sCodigint, Len(sCodigint) - intStartCod))
		Loop 
		
	End Function
	
	'**%insStateDP055: Configures the initial status of the page.
	'%insStateDP055: Configura el estado inicial de la página.
	Public Function insStateDP055(ByVal sCodispl As String, ByVal nAction As Integer, ByVal objProduct As eProduct.Product) As String
		'**-Validate that there is information in the page DP005
		'-Se valida que exista información en la DP005
		Dim lclsErrors As eFunctions.Errors
		lclsErrors = New eFunctions.Errors
		
		insStateDP055 = String.Empty
		If nAction <> eFunctions.Menues.TypeActions.clngActionQuery Then
			If objProduct.sPolitype = String.Empty Then
				insStateDP055 = lclsErrors.ErrorMessage(sCodispl, 11386,  ,  ,  , True)
			End If
		End If
		
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
	End Function
	
	
	'**%Charge_combo: Function that loads the information of the array of the class to a combo box...
	'%Charge_Combo: Función que carga la información del arreglo de la clase a un combo...
	Public Function Charge_Combo(ByRef lobjValues As Object, Optional ByRef lblnLocal As Boolean = False) As Boolean
		Dim lintIndex As Integer
		
		Charge_Combo = False
		lobjValues.Clear()
		lintIndex = 0
		
		'**+If the variable lblnLocal comes in False, load all the permitted currencies for product.
		'+Si la variable lblnLocal viene en falso se cargan todas las monedas permitidas del producto
		If Not (lblnLocal) Then
			Do While lintIndex <= UBound(arrCur_allow)
				lobjValues.AddItem(arrCur_allow(lintIndex).sDescript)
				lobjValues.ItemData(lobjValues.NewIndex) = arrCur_allow(lintIndex).nCurrency
				lintIndex = lintIndex + 1
				Charge_Combo = True
			Loop 
		Else
			
			'**+If the variable lblnLocal comes in True, load just the local currency
			'+Si la variable lblnLocal viene en true se carga solo la moneda local
			Do While lintIndex <= UBound(arrCur_allow)
				'If arrCur_allow(lintIndex).sDefaulti = cintYes Then
				If arrCur_allow(lintIndex).sDefaulti = 1 Then
					lobjValues.AddItem(arrCur_allow(lintIndex).sDescript)
					lobjValues.ItemData(lobjValues.NewIndex) = arrCur_allow(lintIndex).nCurrency
					lintIndex = lintIndex + 1
					Charge_Combo = True
					Exit Do
				End If
			Loop 
		End If
		lobjValues.ListIndex = 0
	End Function
	
	'**%Val_Cur_allow: Function that searches an information of a currency in the array of the class
	'**%given a search index...
	'%Val_Cur_allow: Función que busca una información de una moneda en el arreglo de la clase dado
	'%un indice de busqueda...
	Public Function Val_Cur_Allow(ByVal intIndex As Object) As Boolean
		If intIndex <= UBound(arrCur_allow) Then
			With arrCur_allow(intIndex)
				nCurrency = .nCurrency
				sDefaulti = CStr(.sDefaulti)
				sDescript = .sDescript
				nExchange = .nExchange
			End With
			Val_Cur_Allow = True
		End If
	End Function
	
	'**%delItem_Array: function that deletes a currency form the array, depending on the array's position as a parameter
	'**%the same is used for making the maching between the policy's currencies and the product's currencies for the
	'**%special handle of the currencies of a certificate...
	'%delItem_Array: función que borra una moneda del arreglo dependiendo la posición del arreglo que sea enviada como parametro
	'%la misma es usada para realizar el maching entre las monedas de la poliza matriz y las monedas del producto
	'%para el manejo especial de las monedas de un certificado...
	Public Function delItem_Array(ByVal lintIndex As Integer) As Boolean
		If Not lintIndex = UBound(arrCur_allow) Then
			Do While lintIndex < UBound(arrCur_allow)
				arrCur_allow(lintIndex).Existe = arrCur_allow(lintIndex + 1).Existe
				arrCur_allow(lintIndex).nCurrency = arrCur_allow(lintIndex + 1).nCurrency
				arrCur_allow(lintIndex).nExchange = arrCur_allow(lintIndex + 1).nExchange
				arrCur_allow(lintIndex).sDefaulti = arrCur_allow(lintIndex + 1).sDefaulti
				arrCur_allow(lintIndex).sDescript = arrCur_allow(lintIndex + 1).sDescript
				lintIndex = lintIndex + 1
			Loop 
		End If
		
		ReDim Preserve arrCur_allow(UBound(arrCur_allow) - 1)
	End Function
End Class






