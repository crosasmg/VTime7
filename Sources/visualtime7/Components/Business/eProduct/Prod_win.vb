Option Strict Off
Option Explicit On
Public Class Prod_win
	'%-------------------------------------------------------%'
	'% $Workfile:: Prod_win.cls                             $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 12:36p                               $%'
	'% $Revision:: 12                                       $%'
	'%-------------------------------------------------------%'
	
	'- Estructura de tabla prod_win al 06-13-2002 11:31:39
	'-     Property                Type         DBType   Size Scale  Prec  Null
	Public nBranch As Integer ' NUMBER     22   0     5    N
	Public nProduct As Integer ' NUMBER     22   0     5    N
	Public dEffecdate As Date ' DATE       7    0     0    N
	Public dCompdate As Date ' DATE       7    0     0    S
	Public dNulldate As Date ' DATE       7    0     0    S
	Public nUsercode As Integer ' NUMBER     22   0     5    S
	Public sV_conprodu As String ' CHAR       50   0     0    S
	Public sV_winprodu As String ' CHAR       200  0     0    S
	Public sV_winprodu1 As String ' CHAR       200  0     0    S
	
	'+ Variables auxiliares
	Public sCodispl As String
	Public sContent As String
	Public sRequired As String
	
	'- Variable para saber si se cargó o no el arreglo con la información de las ventanas
	
	Private mblnChargeArr As Boolean
	
	'-Se define el tipo según los valores necesarios para trabajar con la secuencia
	
	Private Structure typProductSeq
		Dim sCodisp As String
		Dim sCodispl As String
		Dim sContent As String
		Dim sDescript As String
		Dim sRequired As String
		Dim sShortDes As String
	End Structure
	
	'-Se define la variable que contiene las descripciones de cada frame de la secuencia
	
	Private mudtDesFrame() As typProductSeq
	
	'- Constante para el número posible de frames. Longitud del campo sV_conprodu - 1
	
	Private Const CN_FRAMESNUM As Integer = 49
	
	'- Se define la variable que contiene la imagen a asociar a la página en la secuencia
	
	Private mintPageImage As eFunctions.Sequence.etypeImageSequence
	
	'- Clase y colección para el manejo de la tabla Tab_winpro
	Private mclsTab_winpro As Tab_winpro
	Private mcolTab_winpro As Tab_winpros
	
	'% LoadTabs: Esta función es la encarga de cargar la secuencia de ventanas a mostrar en la
	'%           secuencia.
	Public Function LoadTabs(ByVal bQuery As Boolean, ByVal dEffecdate As Date, ByVal sBrancht As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nUsercode As Integer, ByVal sTypeCompany As String) As String
		Dim lintAction As Integer
		Dim lblnExist As Boolean
		Dim lstrHTMLCode As String
		Dim lstrBrancht As String
		
		'- Contador utilizado para el número de folder en la colocación de la descripción
		Dim llngCount As Integer
		
		Dim lclsProduct As Product
		Dim lclsSequence As eFunctions.Sequence
		Dim lclsQuery As eRemoteDB.Query
		
		lclsSequence = New eFunctions.Sequence
		lintAction = IIf(bQuery, eFunctions.Menues.TypeActions.clngActionQuery, eFunctions.Menues.TypeActions.clngActionUpdate)
		lblnExist = insReaProd_win(nBranch, nProduct, dEffecdate)
		lstrHTMLCode = lclsSequence.makeTable
		If bQuery Then
			lclsQuery = New eRemoteDB.Query
			
			mintPageImage = eFunctions.Sequence.etypeImageSequence.eOK
			
			llngCount = 0
			
			Do While llngCount <= CountItem
				If mudtDesFrame(llngCount).sCodispl <> "DP046" Then
					If mudtDesFrame(llngCount).sContent = "2" Then
						With lclsQuery
							If .OpenQuery("Windows", "sCodisp, sCodispl, sShort_des", "sCodispl='" & mudtDesFrame(llngCount).sCodispl & "'") Then
								lstrHTMLCode = lstrHTMLCode & lclsSequence.makeRow(.FieldToClass("sCodisp"), .FieldToClass("sCodispl"), lintAction, .FieldToClass("sShort_des"), mintPageImage)
							End If
						End With
					End If
				End If
				
				llngCount = llngCount + 1
			Loop 
		Else
			lclsProduct = New Product
			mcolTab_winpro = New Tab_winpros
			
			lstrBrancht = IIf(lclsProduct.FindProdMasterActive(nBranch, nProduct), lclsProduct.sBrancht, "2")
			
			If mcolTab_winpro.Find(lstrBrancht, 0) Then
				If Not lblnExist Then
					Call insInitializeArray()
				End If
				
				For	Each mclsTab_winpro In mcolTab_winpro
					
					'+ Se busca la imagen a colocar en los links
					
					Call setImage(lblnExist)
					
					If mclsTab_winpro.sDescript <> String.Empty Then
						If mclsTab_winpro.sCodispl <> "DP046" Then
							lstrHTMLCode = lstrHTMLCode & lclsSequence.makeRow(mclsTab_winpro.sCodisp, mclsTab_winpro.sCodispl, lintAction, mclsTab_winpro.sShort_des, mintPageImage)
						End If
					End If
				Next mclsTab_winpro
				
				If Not bQuery Then
					Call Add_Prod_win(nBranch, nProduct, dEffecdate, "", "", nUsercode, lintAction, False)
				End If
			End If
		End If
		
		LoadTabs = lstrHTMLCode & lclsSequence.closeTable()
		
		'UPGRADE_NOTE: Object lclsProduct may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsProduct = Nothing
		'UPGRADE_NOTE: Object lclsSequence may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsSequence = Nothing
		'UPGRADE_NOTE: Object mcolTab_winpro may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		mcolTab_winpro = Nothing
		'UPGRADE_NOTE: Object lclsQuery may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsQuery = Nothing
	End Function
	
	'**%LoadTabsProd: charges the data in the sequence windows
	'%LoadTabsProd: carga los datos de las ventanas de la secuencia
	Public Function LoadTabsProd(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal dEffecdate As Date) As Boolean
		
		On Error GoTo LoadTabsProd_Err
		
		Dim lintIndex As Integer
		Dim sBranchtype As String
		
		Dim lclsProduct As eProduct.Product
		Dim lclsTab_winpro As eProduct.Tab_winpro
		Dim lclsTab_winpros As eProduct.Tab_winpros
		
		lclsProduct = New eProduct.Product
		lclsTab_winpro = New eProduct.Tab_winpro
		lclsTab_winpros = New eProduct.Tab_winpros
		
		LoadTabsProd = True
		
		'**+ The system reads from the Prod_win
		'+ Se lee de Prod_win
		Call insReaProd_win(nBranch, nProduct, dEffecdate)
		
		'**+Determinates the branch type
		'+ Se determina el type de ramo
		sBranchtype = IIf(lclsProduct.FindProdMasterActive(nBranch, nProduct), lclsProduct.sBrancht, "2")
		
		'**+The system reads from the Tab_winpro
		'+ Se lee de Tab_winpro
		lintIndex = 0
		If lclsTab_winpros.Find(sBranchtype, 0) Then
			For	Each lclsTab_winpro In lclsTab_winpros
				mudtDesFrame(lintIndex).sCodispl = lclsTab_winpro.sCodispl
				mudtDesFrame(lintIndex).sRequired = lclsTab_winpro.sRequire
				
				lintIndex = lintIndex + 1
			Next lclsTab_winpro
		End If
		
		'UPGRADE_NOTE: Object lclsProduct may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsProduct = Nothing
		'UPGRADE_NOTE: Object lclsTab_winpro may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsTab_winpro = Nothing
		'UPGRADE_NOTE: Object lclsTab_winpros may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsTab_winpros = Nothing
		
LoadTabsProd_Err: 
		If Err.Number Then
			LoadTabsProd = False
		End If
		On Error GoTo 0
	End Function
	
	'**% Determines if the product designer sequence is or not completed.
	'% Determina si la sequencia del diseñador del producto está o no completa.
	Public Function insValSequence(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal dEffecdate As Date) As Boolean
		'**- Counter
		'- Contador.
		Dim lintIndex As Integer
		
		insValSequence = True
		
		'**+ Obtains all the data of each window that belongs to the sequence
		'+ Obtiene todos los datos de cada ventana que pertenece a la sequencia.
		If LoadTabsProd(nBranch, nProduct, dEffecdate) Then
			lintIndex = 0
			Do 
				'**+ Verifies if the window is required. 1=Yes; 2=No
				'+ Verifica si la ventana es requerida. 1=Si; 2=No
				If mudtDesFrame(lintIndex).sRequired = "1" Then
					'**+ Verify if the window is required. 1=No; 2=Yes
					'+ Verifica si la ventana tiene o no contenido. 1=No; 2=Sí
					If mudtDesFrame(lintIndex).sContent = "1" Then
						insValSequence = False
					End If
				End If
				lintIndex = lintIndex + 1
			Loop Until mudtDesFrame(lintIndex).sCodispl = String.Empty Or Not insValSequence
			
		End If
	End Function
	
	'%Find_Item: Busca la posicion del arreglo dado un codispl
	Public Function Find_Item(ByVal sCodispl As String, Optional ByVal llbnLoad As Boolean = False) As Boolean
		Dim lintIndex As Integer
		
		For lintIndex = 0 To CountItem
			If Trim(mudtDesFrame(lintIndex).sCodispl) = sCodispl Then
				Find_Item = True
				If llbnLoad Then
					Call Item(lintIndex)
				End If
				Exit For
			End If
		Next lintIndex
	End Function
	
	'% Item: Función que carga la información del arreglo en la clase dada una posición
	Public Function Item(ByVal lintIndex As Integer) As Boolean
		Item = False
		'+ Si el arreglo de la clase contiene informacion se carga el combo
		If mblnChargeArr Then
			If lintIndex <= UBound(mudtDesFrame) Then
				With mudtDesFrame(lintIndex)
					sCodispl = .sCodispl
					sContent = .sContent
					sRequired = .sRequired
				End With
				Item = True
			End If
		End If
	End Function
	
	
	'% Required:
	Private Function Required(ByVal sCodispl As String) As Integer
		Dim Index As Object
		Required = 2
		For Index = 0 To UBound(mudtDesFrame)
			If mudtDesFrame(Index).sCodispl = sCodispl And mudtDesFrame(Index).sContent = "2" Then
				Required = 1
				Exit For
			End If
		Next Index
	End Function
	
	'% insReaProd_win: se carga los datos de Prod_win
	Public Function insReaProd_win(ByRef nBranch As Object, ByRef nProduct As Object, ByRef dEffecdate As Object) As Boolean
		Dim lintCount As Integer
		Dim lintPos As Integer
		
		If Find(nBranch, nProduct, dEffecdate) Then
			insReaProd_win = True
			lintPos = 1
			
			ReDim mudtDesFrame(CN_FRAMESNUM)
			
			Do While Len(sV_winprodu) Mod 8 <> 0
				sV_winprodu = sV_winprodu & " "
			Loop 
			
			sV_winprodu = sV_winprodu & sV_winprodu1
			
			If Trim(sV_conprodu) <> String.Empty Then
				For lintCount = 0 To Len(sV_conprodu)
					mudtDesFrame(lintCount).sContent = Mid(sV_conprodu, lintCount + 1, 1)
					mudtDesFrame(lintCount).sCodispl = Trim(Mid(sV_winprodu, lintPos, 8))
					
					lintPos = lintPos + 8
				Next lintCount
			Else
				insReaProd_win = False
			End If
		Else
			insReaProd_win = False
		End If
		mblnChargeArr = insReaProd_win
	End Function
	
	'% Find: se buscan los datos asociados al producto
	Public Function Find(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal dEffecdate As Date) As Boolean
		Dim lrecreaProd_win As eRemoteDB.Execute
		
		On Error GoTo Find_Err
		
		lrecreaProd_win = New eRemoteDB.Execute
		
		'+ Definición de parámetros para stored procedure 'insudb.reaProd_win'
		'+ Información leída el 27/03/2001 03:10:17 p.m.
		
		With lrecreaProd_win
			.StoredProcedure = "reaProd_win"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				Find = True
				Me.nBranch = nBranch
				Me.nProduct = nProduct
				Me.dEffecdate = dEffecdate
				dCompdate = .FieldToClass("dCompdate")
				dNulldate = .FieldToClass("dNulldate")
				sV_conprodu = .FieldToClass("sV_conprodu")
				sV_winprodu = .FieldToClass("sV_winprodu")
				sV_winprodu1 = .FieldToClass("sV_winprodu1")
				.RCloseRec()
			End If
		End With
		'UPGRADE_NOTE: Object lrecreaProd_win may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaProd_win = Nothing
		
Find_Err: 
		If Err.Number Then
			Find = False
		End If
		On Error GoTo 0
	End Function
	
	'**% setImage: assined the corresponding image to the page
	'% setImage: asigna la imagen correspondiente a la página
	Private Sub setImage(ByVal bExist As Boolean)
		Dim lintCount As Integer
		
		'**+ If the system find the information in the Prod_win table
		'+Si se encontró la información en la tabla Prod_win
		If bExist Then
			mintPageImage = eFunctions.Sequence.etypeImageSequence.eEmpty
			For lintCount = 0 To CountItem
				If mudtDesFrame(lintCount).sCodispl = mclsTab_winpro.sCodispl Then
					If mudtDesFrame(lintCount).sContent = "1" Then
						If mclsTab_winpro.sRequire = "1" Then
							mintPageImage = eFunctions.Sequence.etypeImageSequence.eRequired
						End If
						If mclsTab_winpro.sCodispl = "DP004" Then
							If Required("DP042") = 1 Then
								mintPageImage = eFunctions.Sequence.etypeImageSequence.eRequired
							End If
						End If
					ElseIf mudtDesFrame(lintCount).sContent = "2" Then 
						mintPageImage = eFunctions.Sequence.etypeImageSequence.eOK
					ElseIf mudtDesFrame(lintCount).sContent = "3" Then 
						mintPageImage = eFunctions.Sequence.etypeImageSequence.eRequired
					Else
						If mclsTab_winpro.sRequire = "1" Then
							mintPageImage = eFunctions.Sequence.etypeImageSequence.eRequired
						End If
					End If
					Exit For
				End If
			Next lintCount
		Else
			mintPageImage = IIf(mclsTab_winpro.sRequire = "1", eFunctions.Sequence.etypeImageSequence.eRequired, eFunctions.Sequence.etypeImageSequence.eEmpty)
			If mclsTab_winpro.sCodispl = "DP004" Then
				mclsTab_winpro.sRequire = "2"
				mintPageImage = IIf(mclsTab_winpro.sRequire = "1", eFunctions.Sequence.etypeImageSequence.eRequired, eFunctions.Sequence.etypeImageSequence.eEmpty)
			End If
		End If
	End Sub
	
	'**%CountItem: returns the records number in a defined type
	'%CountItem: devuelve el número de registros en el tipo definido
	Public ReadOnly Property CountItem() As Integer
		Get
			If mblnChargeArr Then
				CountItem = UBound(mudtDesFrame)
			Else
				CountItem = -1
			End If
		End Get
	End Property
	
	'**%insInitializeArray:initialize the array
	'% insInitializeArray: inicializa el arreglo
	Private Sub insInitializeArray()
		Dim lbytCount As Byte
		ReDim mudtDesFrame(CN_FRAMESNUM)
		For lbytCount = 0 To CN_FRAMESNUM
			With mudtDesFrame(lbytCount)
				.sCodisp = String.Empty
				.sCodispl = String.Empty
				.sContent = String.Empty
				.sDescript = String.Empty
				.sRequired = String.Empty
				.sShortDes = String.Empty
			End With
		Next lbytCount
	End Sub
	
	'%insProd_win: Esta rutina realiza la actualización de la secuencia de ventanas del diseñador
	'%de productos. La rutina recibe el código del frame y el estado en el cual este debe ser
	'%actualizado en la secuencia, es decir, 1.- Sin Contenido y 2.-Con Contenido
	Public Function Add_Prod_win(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal dEffecdate As Date, ByVal sCodispl As String, ByVal sContent As String, ByVal nUsercode As Integer, Optional ByVal nAction As Integer = 0, Optional ByVal bNotLoadTab As Boolean = True) As Boolean
		Dim llngCount As Integer
		Dim llngTop As Integer
		Dim lstrV_conProduc As String
		Dim lstrV_winProduc As String
		Dim lstrAuxCodispl As String
		
		lstrV_conProduc = String.Empty
		lstrV_winProduc = String.Empty
		
		If bNotLoadTab Then
			If Find(nBranch, nProduct, dEffecdate) Then
				Do While Len(sV_winprodu) Mod 8 <> 0
					sV_winprodu = sV_winprodu & " "
				Loop 
				
				lstrV_conProduc = sV_conprodu
				lstrV_winProduc = sV_winprodu & sV_winprodu1
				
				'+ Se modifica el valor de sV_conprodu con el nuevo contenido
				llngTop = Len(Trim(sV_conprodu)) - 1
				For llngCount = 0 To llngTop
					lstrAuxCodispl = Trim(Mid(lstrV_winProduc, llngCount * 8 + 1, 8))
					If lstrAuxCodispl = sCodispl Then
						lstrV_conProduc = Mid(lstrV_conProduc, 1, llngCount) & sContent & Mid(lstrV_conProduc, llngCount + 2)
						Exit For
					End If
				Next llngCount
			End If
		Else
			Call insConstructSequence(nBranch, nProduct, dEffecdate)
			lstrV_conProduc = sV_conprodu
			lstrV_winProduc = sV_winprodu
		End If
		
		Me.nBranch = nBranch
		Me.nProduct = nProduct
		Me.dEffecdate = dEffecdate
		Me.sV_conprodu = lstrV_conProduc
		Me.sV_winprodu = Mid(lstrV_winProduc, 1, 200)
		Me.sV_winprodu1 = Mid(lstrV_winProduc, 201, 300)
		Me.nUsercode = nUsercode
		Add_Prod_win = Update
		
insProd_win: 
		If Err.Number Then
			Add_Prod_win = False
		End If
		On Error GoTo 0
		
	End Function
	
	'% Update: actualiza la tabla
	Public Function Update() As Boolean
		Dim lrecinsProd_win As eRemoteDB.Execute
		
		On Error GoTo Update_Err
		
		lrecinsProd_win = New eRemoteDB.Execute
		
		'+ Definición de parámetros para stored procedure 'insudb.insProd_win'
		'+ Información leída el 28/03/2001 11:44:09 a.m.
		
		With lrecinsProd_win
			.StoredProcedure = "insProd_win"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dProductDate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sV_conprodu", sV_conprodu, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 50, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sV_winprodu", sV_winprodu, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 200, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sV_winprodu1", sV_winprodu1, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 200, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Update = .Run(False)
		End With
		'UPGRADE_NOTE: Object lrecinsProd_win may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecinsProd_win = Nothing
		
Update_Err: 
		If Err.Number Then
			Update = False
		End If
		On Error GoTo 0
	End Function
	
	'**% makeDataSequence: fill the associated fields values to the sequence in Prod_win
	'% makeDataSequence: se llenan de valor los campos asociados a la secuencia en Prod_win
	Private Sub makeDataSequence()
		'**+Create the sequence for the product
		'+ Se crea la secuencia para el producto
		For	Each mclsTab_winpro In mcolTab_winpro
			sV_conprodu = sV_conprodu & "1"
			sV_winprodu = sV_winprodu & mclsTab_winpro.sCodispl & New String(" ", 8 - Len(mclsTab_winpro.sCodispl))
		Next mclsTab_winpro
	End Sub
	
	'% insConstructSequence: se cargan los datos de la secuencia definida en Tab_winpro
	Private Sub insConstructSequence(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal dEffecdate As Date)
		Dim lstrBrancht As String
        Dim lstrNewSequence As String = ""
        Dim lstrNewContent As String = ""
        Dim lintCount As Integer
		Dim lclsProduct As eProduct.Product
		Dim lclsTab_winpro As Tab_winpro
		Dim lcolTab_winpro As Tab_winpros
		
		On Error GoTo insConstructSequence_err
		
		lclsProduct = New eProduct.Product
		lcolTab_winpro = New Tab_winpros
		
		If insReaProd_win(nBranch, nProduct, dEffecdate) Then
			lintCount = 0
			
			lstrBrancht = IIf(lclsProduct.FindProdMasterActive(nBranch, nProduct), lclsProduct.sBrancht, "2")
			
			If lcolTab_winpro.Find(lstrBrancht, 0) Then
				lintCount = lintCount + 1
				For	Each lclsTab_winpro In lcolTab_winpro
					lstrNewSequence = lstrNewSequence & lclsTab_winpro.sCodispl & New String(" ", 8 - Len(lclsTab_winpro.sCodispl))
					lstrNewContent = IIf(Find_Item(lclsTab_winpro.sCodispl, True), lstrNewContent & sContent, lstrNewContent & "1")
				Next lclsTab_winpro
			End If
			sV_conprodu = lstrNewContent
			sV_winprodu = lstrNewSequence
		Else
			Call makeDataSequence()
		End If
		
insConstructSequence_err: 
		If Err.Number Then
			On Error GoTo 0
		End If
		
		'UPGRADE_NOTE: Object lcolTab_winpro may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lcolTab_winpro = Nothing
		'UPGRADE_NOTE: Object lclsProduct may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsProduct = Nothing
		'UPGRADE_NOTE: Object lclsTab_winpro may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsTab_winpro = Nothing
	End Sub
End Class






