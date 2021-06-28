Option Strict Off
Option Explicit On
Public Class Cases_win
	'%-------------------------------------------------------%'
	'% $Workfile:: Cases_win.cls                            $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 1:34p                                $%'
	'% $Revision:: 17                                       $%'
	'%-------------------------------------------------------%'
	
	Public nClaim As Double
	Public nCase_num As Integer
	Public nDeman_type As Integer
	Public sV_conclaim As String
	Public sV_winclaim As String
	Public nUsercode As Integer
	Public sContent As String
	Public sCodispl As String
	Public IndexTab As Integer
	Private mblnChargeArr As Boolean
	Public sDescript As String
	Public sRequire As String
	Public sShort_des As String
	Public sCodisp As String
	
	'**- Definition of the estructure that contein the descriptions of each frame
	'-Definición de la estructura que contiene las descripciones de cada frame
	
	Private Structure typDesFrame
		Dim sCodisp As String
		Dim sCodispl As String
		Dim sDescript As String
		Dim sShortDes As String
		Dim sRequired As String
		Dim sContent As String
		Dim nModules As Short
		Dim nWindowTy As Short
	End Structure
	
	'**- Define the variable taht contain the description of each frame of the actualization secuence of cases (SI0099)
	'-Se define la variable que contiene las descripciones de cada frame de la secuencia actualizacion de casos (SI0099)
	
	Private mudtDesFrame(5) As typDesFrame
	
	'**-Defined the variable that contein the image that is going to be associate in the secuence
	'- Se define la variable que contiene la imagen a asociar a la página en la secuencia
	
	Private mintPageImage As eFunctions.Sequence.etypeImageSequence
	
	
	'**-Defined the variable for the handle of the pages security
	'- Se define la variable para el manejo de la seguridad de las páginas
	
	Private mclsSecurSche As eSecurity.Secur_sche
	'**%Find: It searches the table Cases_win
	'%Find: Busca en la tabla Cases_win
	Public Function Find(ByVal llngClaim As Double, ByVal lintCase_num As Integer, ByVal lintDeman_type As Integer, Optional ByVal lblnFind As Boolean = False) As Boolean
		Dim lrecreaCases_win2 As eRemoteDB.Execute
		
		On Error GoTo Find_Err
		
		lrecreaCases_win2 = New eRemoteDB.Execute
		
		If llngClaim <> nClaim Or lintCase_num <> nCase_num Or lintDeman_type <> nDeman_type Or lblnFind Then
			'**+ Parameters definition for the stored procedure 'insudb.reaCases_win2'
			'+ Definición de parámetros para stored procedure 'insudb.reaCases_win2'
			'**+ Data read on 01/19/2001 10.31.38
			'+ Información leída el 19/01/2001 10.31.38
			With lrecreaCases_win2
				.StoredProcedure = "reaCases_win2"
				.Parameters.Add("nClaim", llngClaim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nCase_num", lintCase_num, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nDeman_type", lintDeman_type, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				If .Run Then
					nClaim = llngClaim
					nCase_num = lintCase_num
					nDeman_type = lintDeman_type
					sV_conclaim = .FieldToClass("sV_conclaim")
					sV_winclaim = .FieldToClass("sV_winclaim")
					Find = True
					.RCloseRec()
				Else
					Find = False
				End If
			End With
		Else
			Find = True
		End If
		lrecreaCases_win2 = Nothing
		
Find_Err: 
		If Err.Number Then
			Find = False
		End If
		On Error GoTo 0
	End Function
	
	'**%Update: Updates the Cases_win table
	'%Update: Actualiza la tabla Cases_win
	Public Function Update() As Boolean
		Dim lrecinsCases_win As eRemoteDB.Execute
		lrecinsCases_win = New eRemoteDB.Execute
		
		On Error GoTo Update_Err
		'**Parameters definition for the stored procedure 'insudb.insCases_win'
		'Definición de parámetros para stored procedure 'insudb.insCases_win'
		'**Data read on 01/17/2001 15.33.22
		'Información leída el 17/01/2001 15.33.22
		
		With lrecinsCases_win
			.StoredProcedure = "insCases_win"
			.Parameters.Add("nClaim", nClaim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCase_num", nCase_num, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDeman_type", nDeman_type, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sV_conclaim", sV_conclaim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 30, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sV_winclaim", sV_winclaim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 180, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Update = .Run(False)
		End With
		lrecinsCases_win = Nothing
		
Update_Err: 
		If Err.Number Then
			Update = False
		End If
		On Error GoTo 0
	End Function
	'**%Add_Cases_win: This routine makes the actualization of the windows sequence of claims
	'**%The routine receive the frame code and the condition where it has to be updateed in the sequence
	'**%for example: 1.- empty and 2.- filled
	'%Add_Cases_win: Esta rutina realiza la actualización de la secuencia de ventanas de siniestros
	'%La rutina recibe el código del frame y el estado en el cual este debe ser
	'%actualizado en la secuencia, es decir, 1.- Sin Contenido y 2.-Con Contenido
	Public Function Add_Cases_win(ByVal lintClaim As Double, ByVal lintCase_num As Integer, ByVal lintDeman_type As Integer, ByVal lstrCodispl As String, ByVal lstrContent As String, ByVal lintUsercode As Integer, Optional ByVal bNotLoadTab As Boolean = True) As Boolean
		Dim lrecinsCases_win As eRemoteDB.Execute
		Dim lclsCases_win As Cases_win
		Dim llngCount As Integer
		Dim lstrV_conclaim As String
		Dim lstrV_winclaim As String
		Dim lintitem As Integer
		Dim lintFolder As Integer
		Dim llngTop As Integer
		Dim lstrAuxCodispl As String
		
		On Error GoTo Add_Cases_win_Err
		
		If bNotLoadTab Then
			lrecinsCases_win = New eRemoteDB.Execute
			With lrecinsCases_win
				.StoredProcedure = "insAddCases_win"
				.Parameters.Add("nClaim", lintClaim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nCase_num", lintCase_num, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nDeman_type", lintDeman_type, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("sCodispl", lstrCodispl, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 8, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("sContent", lstrContent, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				Add_Cases_win = .Run(False)
			End With
		Else
			lstrV_conclaim = String.Empty
			lstrV_winclaim = String.Empty
			
			If Find_Item(lstrCodispl, True) Then
				Refresh_Content = lstrContent
			End If
			
			For llngCount = 0 To CountItem
				Call Item(llngCount)
				lstrV_conclaim = Mid(lstrV_conclaim, 1, llngCount) & Me.sContent
				lstrV_winclaim = Mid(lstrV_winclaim, 1, llngCount * 8) & Me.sCodispl
			Next llngCount
			
			If Trim(lstrV_winclaim) <> String.Empty And Trim(lstrV_conclaim) <> String.Empty Then
				'**+ Policy win is updated
				'+ Se actualiza Policy_Win
				lclsCases_win = New Cases_win
				With lclsCases_win
					.nClaim = lintClaim
					.nCase_num = lintCase_num
					.nDeman_type = lintDeman_type
					.sV_conclaim = lstrV_conclaim
					.sV_winclaim = lstrV_winclaim
					.nUsercode = lintUsercode
					Add_Cases_win = .Update
				End With
			End If
		End If
		
Add_Cases_win_Err: 
		If Err.Number Then
			Add_Cases_win = False
		End If
		
		On Error GoTo 0
		lrecinsCases_win = Nothing
		lclsCases_win = Nothing
	End Function
	
	'**%Find_Item: Search the location of the array as a codispl
	'%Find_Item: Busca la posicion del arreglo dado un codispl
	Public Function Find_Item(ByVal lstrCodispl As String, Optional ByVal llbnLoad As Boolean = False) As Boolean
		For IndexTab = 0 To CountItem
			If Trim(mudtDesFrame(IndexTab).sCodispl) = lstrCodispl Then
				Find_Item = True
				If llbnLoad Then Item(IndexTab)
				Exit For
			End If
		Next IndexTab
	End Function
	
	'**%Property that is in charge to refresh the value of the field sContent in the array
	'%Propìedad que se encarga de refrescar el valor del campo sContent en el arreglo
	WriteOnly Property Refresh_Content() As String
		Set(ByVal Value As String)
			Dim lintIndex As Integer
			
			For lintIndex = 0 To CountItem
				If sCodispl = mudtDesFrame(lintIndex).sCodispl Then
					mudtDesFrame(lintIndex).sContent = Value
					Exit For
				End If
			Next lintIndex
		End Set
	End Property
	
	'**%Refresh_Require: This property is in charge of updating the content of the field "Required" of the array (mudtDesFrame)
	'%Refresh_Require: Esta propiedad se encarga de actualizar el contenido de el campo "Required" del arreglo (mudtDesframe)
	WriteOnly Property Refresh_Require() As String
		Set(ByVal Value As String)
			Dim lintIndex As Integer
			
			For lintIndex = 0 To CountItem
				If Trim(sCodispl) = Trim(mudtDesFrame(lintIndex).sCodispl) Then
					mudtDesFrame(lintIndex).sRequired = Value
					Exit For
				End If
			Next lintIndex
		End Set
	End Property
	'**%CountItem:  this property returns the record number in a defined type
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
	
	'**%Item: loads according to the elements position in the public properties
	'%Item: carga según una posición los elementos en las propiedades públicas
	Public Function Item(ByVal intIndex As Integer) As Boolean
		Dim nIndex As Byte
		
		Item = False
		If intIndex <= UBound(mudtDesFrame) Then
			With mudtDesFrame(intIndex)
				sCodisp = .sCodisp
				sCodispl = .sCodispl
				For nIndex = 1 To 8 - Len(sCodisp)
					sCodisp = sCodisp & " "
				Next 
				sContent = .sContent
				sDescript = .sDescript
				sRequire = .sRequired
				sShort_des = .sShortDes
			End With
			Item = True
		End If
	End Function
	
	'**%Class_Initialize: Controls the class opening
	'% Class_Initialize: se controla la apertura de la clase
	Private Sub Class_Initialize_Renamed()
        Call InitializeArray()
        mblnChargeArr = False
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
	
	'**%LoadTabsClaim: arm the sequence in HTML code
	'%LoadTabsClaim: arma la secuencia en código HTML
	Public Function LoadTabs(ByVal sTransaction As String, ByVal nClaim As Double, ByVal nCase_num As Integer, ByVal nDeman_type As Integer, ByVal nBeneftype As Integer, ByVal sBrancht As String, ByVal sUserSchema As String, ByVal nUsercode As Integer, Optional ByVal bCreateTab As Boolean = True) As String
		Dim lrecCases_win As eRemoteDB.Execute
		Dim lclsSequence As eFunctions.Sequence
		Dim lintAction As Integer
		Dim lstrHTMLCode As String
		Dim lstrInfWin As String
		Dim larrWindows() As String
		Dim larrCodispl() As String
		Dim lvntWindows As Object
		Dim lintIndex As Short
		Dim lintIndexTot As Short
		
		On Error GoTo LoadTabs_Err
		
		lrecCases_win = New eRemoteDB.Execute
		
		lstrHTMLCode = String.Empty
		
		If sTransaction = CStr(Claim_win.eClaimTransac.clngClaimQuery) Then
			lintAction = eFunctions.Menues.TypeActions.clngActionQuery
		Else
			lintAction = eFunctions.Menues.TypeActions.clngActionInput
		End If
		
		With lrecCases_win
			.StoredProcedure = "getLoadTabsCases"
			.Parameters.Add("nClaim", nClaim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCase_num", nCase_num, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDeman_type", nDeman_type, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBeneftype", nBeneftype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sBrancht", sBrancht, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sLoadTabs", String.Empty, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 4000, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Run(False)
			lstrInfWin = .Parameters("sLoadTabs").Value
		End With
		
		lintIndexTot = -1
		
		If lstrInfWin <> String.Empty Then
			'+ Si existe información para procesar
			larrWindows = Microsoft.VisualBasic.Split(lstrInfWin, "||")
			'+ Se tratan cada una de las ventanas
			For	Each lvntWindows In larrWindows
				If lvntWindows <> String.Empty Then
					lvntWindows = lvntWindows + "|"
					larrCodispl = Microsoft.VisualBasic.Split(lvntWindows, "|")
					lintIndexTot = lintIndexTot + 1
					With mudtDesFrame(lintIndexTot)
						.sCodisp = larrCodispl(0)
						.sCodispl = larrCodispl(1)
						.sDescript = larrCodispl(2)
						.sShortDes = larrCodispl(3)
						.sRequired = larrCodispl(7)
						.sContent = larrCodispl(6)
						.nModules = CShort(larrCodispl(4))
						.nWindowTy = CShort(larrCodispl(5))
					End With
				End If
			Next lvntWindows
		End If
		
		If lintIndexTot >= 0 Then
			mblnChargeArr = True
			If bCreateTab Then
				lclsSequence = New eFunctions.Sequence
				
				lstrHTMLCode = lclsSequence.makeTable
				
				For lintIndex = 0 To lintIndexTot
					'**+Assign values to the public variables
					'+ Se asignan los valores a las variables públicas
					Call Item(lintIndex)
					
					If sCodispl <> String.Empty Then
						'**+ Search the image to put in the links
						'+ Se busca la imagen a colocar en los links
						Call SecurityFrame(sUserSchema)
						
						lstrHTMLCode = lstrHTMLCode & lclsSequence.makeRow(mudtDesFrame(lintIndex).sCodisp, mudtDesFrame(lintIndex).sCodispl, lintAction, mudtDesFrame(lintIndex).sShortDes, mintPageImage,  ,  ,  ,  ,  ,  , mudtDesFrame(lintIndex).sDescript, mudtDesFrame(lintIndex).nModules, mudtDesFrame(lintIndex).nWindowTy)
					End If
				Next 
				lstrHTMLCode = lstrHTMLCode & lclsSequence.closeTable()
			End If
		End If
		LoadTabs = lstrHTMLCode
		
		mclsSecurSche = Nothing
		
		Exit Function
LoadTabs_Err: 
		LoadTabs = "LoadTabs: " & Err.Description
		On Error GoTo 0
		lclsSequence = Nothing
	End Function
	
	'**% insDriveError: Routine to capture the error data (if it ocurrs)
	'% insDriveError: Rutina para capturar los datos del error (en caso que ocurra)
	Public Function insDriveError(ByVal lstrMessage As String) As Boolean
		Dim sErrorDescript As Object
		Dim nErrNumber As Object
		
		With Err
			sErrorDescript = lstrMessage & ": " & Err.Description
			nErrNumber = Err.Number
			.Clear()
		End With
		On Error GoTo 0
	End Function
	
	'**% SecurityFrame: validates that the page is valid for the scheme/user
	'% SecurityFrame: valida que la página sea valida para el esquema/usuario
	Private Function SecurityFrame(ByVal sSchema As String) As Boolean
		Dim lstrCodispl As String
		
		On Error GoTo SecurityFrame_Err
		
		If mclsSecurSche Is Nothing Then
			mclsSecurSche = New eSecurity.Secur_sche
		End If
		
		lstrCodispl = Trim(sCodispl)
		With mclsSecurSche
			If Not .valTransAccess(sSchema, sCodispl, "2") Then
				If sContent = "2" Then
					mintPageImage = eFunctions.Sequence.etypeImageSequence.eDeniedOK
				Else
					If sRequire = "1" Then
						mintPageImage = eFunctions.Sequence.etypeImageSequence.eDeniedReq
					Else
						mintPageImage = eFunctions.Sequence.etypeImageSequence.eDeniedS
					End If
				End If
			Else
				If sContent = "1" Then
					If sRequire = "1" Then
						mintPageImage = eFunctions.Sequence.etypeImageSequence.eRequired
					Else
						mintPageImage = eFunctions.Sequence.etypeImageSequence.eEmpty
					End If
				Else
					mintPageImage = eFunctions.Sequence.etypeImageSequence.eOK
				End If
			End If
		End With
		
SecurityFrame_Err: 
		If Err.Number Then
			SecurityFrame = False
		End If
		On Error GoTo 0
	End Function
	
	'**% InsValSI099: Validates that the page is valid for the scheme/user
	'% InsValSI099: valida que la página sea valida para el esquema/usuario
	Public Function InsValSI099(ByVal sCodispl As String, ByVal nClaim As Double, ByVal nCase_num As Integer, ByVal nDeman_type As Integer) As String
		Dim lclsClaim_case As Claim_case
		Dim lerrTime As eFunctions.Errors
		
		On Error GoTo InsValSI099_Err
		
		lerrTime = New eFunctions.Errors
		lclsClaim_case = New Claim_case
		
		With lclsClaim_case
			.nClaim = nClaim
			.nCase_num = nCase_num
			.nDeman_type = nDeman_type
			If Not .bFullCase Then
				Call lerrTime.ErrorMessage(sCodispl, 705003)
			End If
		End With
		
		InsValSI099 = lerrTime.Confirm
		
		lerrTime = Nothing
		lclsClaim_case = Nothing
		
InsValSI099_Err: 
		If Err.Number Then
			InsValSI099 = InsValSI099 & Err.Description
		End If
		On Error GoTo 0
	End Function
	
	'**% InsPostSI099: This function is incharge to update the status of the case that is
	'**%being updated or modified
	'%InsPostSI099: Esta funcion se encarga de actualizar el estado del caso que se
	'%esta actualizando o modificando
	Private Function InsPostSI099(ByVal pintAction As Boolean, ByVal nClaim As Double, ByVal nCase_num As Integer, ByVal nDeman_type As Integer) As Boolean
		Dim lclsClaim_case As Claim_case
		
		If pintAction <> eFunctions.Menues.TypeActions.clngActionQuery Then
			lclsClaim_case = New Claim_case
			With lclsClaim_case
				If .Find(nClaim, nCase_num, nDeman_type) Then
					.sStaReserve = IIf(.sStaReserve = "6", "2", .sStaReserve)
					InsPostSI099 = .UpdatesStareserve(.nClaim, .nDeman_type, .nCase_num, .sStaReserve)
				Else
					InsPostSI099 = False
				End If
			End With
			lclsClaim_case = Nothing
		Else
			InsPostSI099 = True
		End If
	End Function
	
	'**% FullCases: Read the table Cases_win the info of the windows.
	'**%            Check the windows that are required and if it finds some lack of info  then It will return 0, or return 1
	'% FullCases: Lee la tabla Cases_win la información de las ventanas.
	'%            Evalua las ventanas que son requeridas, y si alguna le falta información,
	'%            devuelve 0, sino, devuelve 1
	Public Function valFullCases(ByVal nClaim As Double, ByVal nCase_num As Integer, ByVal nDeman_type As Integer) As Integer
		Dim lstrCodispl As String
		Dim lstrContent As String
		Dim llngCount As Integer
		Dim llngPos As Integer
        Dim lbytNunFrames As Byte
        Dim lobjClaim As New eClaim.Claim
        Dim lobjProduct As New eProduct.Product
        Dim dSearchEffecdate As Date = Date.Today
        Dim bValidate As Boolean

        If lobjClaim.Find(nClaim) Then
            If Not lobjProduct.Find(lobjClaim.nBranch, lobjClaim.nProduct, dSearchEffecdate) Then
                Throw New Exception("El producto no existe (" & lobjClaim.nBranch & "," & lobjClaim.nProduct & "," & dSearchEffecdate & ")")
            End If
        Else
            Throw New Exception("El siniestro no existe (" & nClaim & ")")
        End If

        valFullCases = 1

        If Find(nClaim, nCase_num, nDeman_type) Then
            llngPos = 1
            If sV_conclaim <> String.Empty Then
                lbytNunFrames = Len(Trim(sV_conclaim)) - 1

                For llngCount = 0 To lbytNunFrames
                    bValidate = False
                    lstrCodispl = Trim(Mid(sV_winclaim, llngPos, 8))
                    lstrContent = Trim(Mid(sV_conclaim, llngCount + 1, 1))
                    'Se excluyo la SI028 de esta condicion, ya que solo aplicaria para atencion medica
                    Select Case lobjProduct.sBrancht
                        Case eProduct.Product.pmBrancht.pmSegurosProvisionales
                            If lstrCodispl = "SI018" Then
                                If lstrContent = "1" Then
                                    valFullCases = 0
                                    Exit For
                                End If
                            End If
                            Exit For
                        Case Else
                            If lstrCodispl = "SI018" Or lstrCodispl = "SI019" Or lstrCodispl = "SI024" Or lstrCodispl = "SI070" Then
                                If lstrContent = "1" Then
                                    valFullCases = 0
                                    Exit For
                                End If
                            End If
                    End Select

                    llngPos = llngPos + 8
                Next llngCount
            End If
        Else
            valFullCases = 0
        End If

    End Function
	
	'**% InitializeArray: Runs the array of the windows values
	'% InitializeArray: inicializa el arreglo de valores de la ventana
	Private Sub InitializeArray()
		Dim bytIndex As Byte
		For bytIndex = 0 To 5
			mudtDesFrame(bytIndex).sCodisp = String.Empty
			mudtDesFrame(bytIndex).sCodispl = String.Empty
			mudtDesFrame(bytIndex).sContent = String.Empty
			mudtDesFrame(bytIndex).sDescript = String.Empty
			mudtDesFrame(bytIndex).sRequired = String.Empty
			mudtDesFrame(bytIndex).sShortDes = String.Empty
		Next 
	End Sub
End Class






