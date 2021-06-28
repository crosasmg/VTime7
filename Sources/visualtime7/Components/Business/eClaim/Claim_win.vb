Option Strict Off
Option Explicit On
Public Class Claim_win
	'%-------------------------------------------------------%'
	'% $Workfile:: Claim_win.cls                            $%'
	'% $Author:: Nvaplat28                                  $%'
	'% $Date:: 1/07/05 9:51a                                $%'
	'% $Revision:: 28                                       $%'
	'%-------------------------------------------------------%'
	'%-------------------------------------------------------%'
	'% $Workfile:: Claim_win.cls                            $%'
	'% $Author:: Nvaplat28                                  $%'
	'% $Date:: 1/07/05 9:51a                                $%'
	'% $Revision:: 28                                       $%'
	'%-------------------------------------------------------%'
	
	'-Variables públicas a la clase
	
	'   Column_name                            Type            Computed     Length      Prec  Scale Nullable      TrimTrailingBlanks     fixedLenNullInSource
	Public sRequire As String 'char           no           1                       yes           yes                    yes
	Public sCodisp As String 'char           no           8                       yes           yes                    yes
	Public sDescript As String 'char           no           40                      yes           yes                    yes
	Public sShort_des As String 'char           no           12                      yes           yes                    yes
	Public nWindowTy As Integer 'smallint       no           2           5     0     yes           (n/a)                  (n/a)
	Public sContent As String
	Public sShow As String
	Public nUserCode As Integer
	Public nClaim As Double
	Public sV_conclaim As String
	Public sV_winclaim As String
	
	Public sCodispl As String
	Private mblnChargeArr As Boolean
	Public IndexTab As Integer
	Public bPolVigency As Boolean
	Public sMessage As String
	Public sMessage1 As String
	Public nUpdCl_cover As Short
	
	
	'+ Variables auxiliares
	
	'-Se definen las constantes globales para el manejo de las opciones del menú de acciones
	
	Public Enum eClaimTransac
		clngClaimIssue = 1 '+ Emision de Siniestro
		clngRecovery = 2 '+ Recobro de siniestro
		clngApproval = 3 '+ Aprobación
		clngClaimAmendment = 4 '+ Modificar siniestro
		clngClaimQuery = 5 '+ Consultar Siniestro
		clngClaimRecovery = 6 '+ Recuperar siniestro
		clngClaimCancellation = 7 '+ Cancelar Siniestro
		clngClaimRever = 8 '+ Reverso  de Siniestro
		clngClaimPayme = 9 '+ Registro de Pago
		clngPaymeQuery = 10 '+ Consulta de Pago
		clngClaimRelease = 11 '+ Finiquito
		clngRequeDoc = 12 '+ Recaudos
		clngServiceProf = 13 '+ Servicios Profesionales
		clngLetterReq = 14 '+ Carta Aval
		clngClaimRejection = 15 '+ Rechazo de Siniestros
		clngClaimReopening = 16 '+ Reapertura de Siniestros
		clngCaratula = 17 '+ Reapertura de Siniestros
	End Enum
	
	'-Se define el tipo según los valores necesarios para trabajar desde el programa
	
	Private Structure typClaimSeq
		Dim sCodisp As String
		Dim sCodispl As String
		Dim sContent As String
		Dim sDescript As String
		Dim sRequired As String
		Dim sShortDes As String
		Dim nWindowTy As Integer
		Dim sShow As String
		Dim nModules As Integer
	End Structure
	
	Private mintModules As Integer
	
	'-Se define la variable que contiene las descripciones de cada frame de la secuencia Principal
	
	Private mudtDesFrame() As typClaimSeq
	
	'- Se definen las constantes para definir el tipo de imagen a mostrar
	Private Const CN_DENIEDREQ As String = "DENIEDREQ"
	Private Const CN_DENIEDOK As String = "DENIEDOK"
	Private Const CN_DENIEDS As String = "DENIEDS"
	Private Const CN_REQUIRED As String = "REQUIRED"
	Private Const CN_OK As String = "OK"
	
	'- Se define la variable que contiene la imagen a asociar a la página en la secuencia
	Private mintPageImage As eFunctions.Sequence.etypeImageSequence
	
	'- Se define la variable para el manejo de la seguridad de las páginas
	Private mclsSecurSche As eSecurity.Secur_sche
	
	'% Determina si la sequencia de la póliza se encuentra completa.
	Public Function insValSequence(ByVal nTransaction As Integer, ByVal nClaim As Double, ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Integer, ByVal bPolicyVigency As Boolean, Optional ByVal nNotenum As Integer = 0) As Boolean
		
		Dim nPolicyVigency As Short
		Dim lrecinsValseq_claim As eRemoteDB.Execute
		
		On Error GoTo insValseq_claim_Err
		
		lrecinsValseq_claim = New eRemoteDB.Execute
		
		If bPolicyVigency Then
			nPolicyVigency = 1
		Else
			nPolicyVigency = 0
		End If
		
		'+ Definición de store procedure insValseq_claim al 07-14-2003 18:25:11
		
		With lrecinsValseq_claim
			.StoredProcedure = "insValseq_claim"
			.Parameters.Add("nClaim", nClaim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTransaction", nTransaction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nContent", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sBrancht", String.Empty, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicyvig", nPolicyVigency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nNotenum", nNotenum, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sTextMessage", " ", eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 200, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUpdCl_cover", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Run(False)
			insValSequence = .Parameters("nContent").Value = 1
			Me.sMessage1 = Trim(.Parameters.Item("sTextMessage").Value)
			Me.nUpdCl_cover = .Parameters.Item("nUpdCl_cover").Value
			
		End With
		
insValseq_claim_Err: 
		If Err.Number Then
			insValSequence = False
		End If
		On Error GoTo 0
		lrecinsValseq_claim = Nothing
	End Function
	
	'%insConcatMessage: Función que devuelve un string, resultado de la concatenación de dos cadenas.
	Public Function insConcatMessage(ByVal lstrString As String, ByVal lintError As Integer) As String
		Dim lstrStringA As String
		Dim lstrStringB As String
		
		Dim lobjGeneral As eGeneral.GeneralFunction
		lobjGeneral = New eGeneral.GeneralFunction
		
		lstrStringA = Trim(lstrString)
		lstrStringB = Trim(lobjGeneral.insLoadMessage(lintError))
		If lstrStringA = String.Empty Then
			insConcatMessage = "- " & lstrStringB & "."
		Else
			insConcatMessage = lstrStringA & Chr(13) & Chr(10) & "- " & lstrStringB & "."
		End If
		
		lobjGeneral = Nothing
	End Function
	
	'%LoadTabsClaim: carga los datos de las ventanas de la secuencia
	Public Function LoadTabsClaim(ByVal nTransaction As Integer, ByVal nClaim As Double, ByVal sbrancht As String, ByVal sBussityp As String, Optional ByVal bPoliVigency As Boolean = False) As Boolean
		Dim recreaTab_WinCla As eRemoteDB.Execute
		Dim intIndex As Integer
		
		On Error GoTo LoadTabsClaim_Err
		
		recreaTab_WinCla = New eRemoteDB.Execute
		
		'+ Definición de parámetros para stored procedure 'insudb.reaTab_WinCla'
		'+ Información leída el 10/02/2000 15:35:36
		
		LoadTabsClaim = True
		With recreaTab_WinCla
			.StoredProcedure = "reaTab_WinCla"
			.Parameters.Add("nClaim", nClaim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTratypec", nTransaction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sBrancht", sbrancht, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sBussityp", sBussityp, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				intIndex = 0
				mblnChargeArr = True
				ReDim mudtDesFrame(50)
				While Not .EOF
					mudtDesFrame(intIndex).sCodisp = .FieldToClass("sCodisp", String.Empty)
					mudtDesFrame(intIndex).sCodispl = .FieldToClass("sCodispl", String.Empty)
					mudtDesFrame(intIndex).sContent = IIf(.FieldToClass("sContent") = String.Empty, "1", .FieldToClass("sContent", String.Empty))
					mudtDesFrame(intIndex).sDescript = .FieldToClass("sDescript", String.Empty)
					If .FieldToClass("sCodispl", String.Empty) = "SI007" And bPoliVigency Then
						mudtDesFrame(intIndex).sRequired = "2"
					Else
						If mudtDesFrame(intIndex).sContent = "3" Then
							mudtDesFrame(intIndex).sContent = "1"
							mudtDesFrame(intIndex).sRequired = "1"
						Else
							mudtDesFrame(intIndex).sRequired = .FieldToClass("sRequired", String.Empty)
						End If
					End If
					mudtDesFrame(intIndex).sShortDes = .FieldToClass("sShortDes", String.Empty)
					mudtDesFrame(intIndex).nWindowTy = .FieldToClass("nWindowTy", 0)
					mudtDesFrame(intIndex).sShow = .FieldToClass("sShow", "2")
					mudtDesFrame(intIndex).nModules = .FieldToClass("nModules")
					intIndex = intIndex + 1
					.RNext()
				End While
				ReDim Preserve mudtDesFrame(intIndex - 1)
				.RCloseRec()
			Else
				mblnChargeArr = False
				LoadTabsClaim = False
			End If
		End With
		
LoadTabsClaim_Err: 
		If Err.Number Then
			LoadTabsClaim = False
		End If
		On Error GoTo 0
		recreaTab_WinCla = Nothing
	End Function
	
	
	'%Find_Item: Busca la poscision del arreglo dado un codispl
	Public Function Find_Item(ByVal lstrCodispl As String, Optional ByVal llbnLoad As Boolean = False) As Boolean
		For IndexTab = 0 To CountItem
			If Trim(mudtDesFrame(IndexTab).sCodispl) = lstrCodispl Then
				Find_Item = True
				If llbnLoad Then Item(IndexTab)
				Exit For
			End If
		Next IndexTab
	End Function
	
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
				nWindowTy = .nWindowTy
				sShow = .sShow
				mintModules = .nModules
			End With
			Item = True
		End If
	End Function
	
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
	
	'%Propìedad que se encarga de refrescar el valor del campo sRequire en el arreglo
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

    '%ChangeContentWin: cambia el estado del contenido en una ventana y en la BD actualizando las propiedades de la clase
    Public Function ChangeContentWin(ByVal sCodispl As String) As Object

        Find_Item(sCodispl, True)
        mudtDesFrame(IndexTab).sRequired = CStr(1)
        sRequire = mudtDesFrame(IndexTab).sRequired
        Return sRequire
    End Function

    '% Class_Initialize: se controla la apertura de la clase
    Private Sub Class_Initialize_Renamed()
		Dim bytIndex As Byte
		
		ReDim mudtDesFrame(50)
		For bytIndex = 0 To 50
			mudtDesFrame(bytIndex).sCodisp = String.Empty
			mudtDesFrame(bytIndex).sCodispl = String.Empty
			mudtDesFrame(bytIndex).sContent = String.Empty
			mudtDesFrame(bytIndex).sShow = String.Empty
			mudtDesFrame(bytIndex).sDescript = String.Empty
			mudtDesFrame(bytIndex).sRequired = String.Empty
			mudtDesFrame(bytIndex).sShortDes = String.Empty
			mudtDesFrame(bytIndex).nWindowTy = 0
		Next 
		mblnChargeArr = False
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
	
	'%LoadTabsClaim: arma la secuencia en código HTML
    Public Function LoadTabs(ByVal sTransaction As String, ByVal sClaim As String, ByVal sbrancht As String, ByVal sBussityp As String, ByVal sUserSchema As String, ByVal nUserCode As String, Optional ByVal bPolicyVigency As Boolean = False, Optional nRecoveryTransac As Integer = eRemoteDB.Constants.intNull) As String
        Dim lclsValues As eFunctions.Values
        Dim lclsSequence As eFunctions.Sequence
        Dim lintCountWindows As Integer
        Dim lintCount As Integer
        Dim lintAction As Integer
        Dim lstrHTMLCode As String
        Dim lblnFind As Boolean

        On Error GoTo LoadTabs_Err

        lclsSequence = New eFunctions.Sequence
        lclsValues = New eFunctions.Values

        lstrHTMLCode = String.Empty

        Me.bPolVigency = bPolicyVigency
        lblnFind = Find(CDbl(sClaim))
        If LoadTabsClaim(lclsValues.StringToType(sTransaction, eFunctions.Values.eTypeData.etdInteger), lclsValues.StringToType(sClaim, eFunctions.Values.eTypeData.etdDouble), sbrancht, sBussityp, Me.bPolVigency) Then
            lintCountWindows = 0

            If sTransaction = CStr(eClaimTransac.clngClaimQuery) Then
                lintAction = eFunctions.Menues.TypeActions.clngActionQuery
            Else
                lintAction = eFunctions.Menues.TypeActions.clngActionInput
            End If

            lstrHTMLCode = lclsSequence.makeTable("DMESIN", "Siniestros")
            lintCount = CountItem
            Do While lintCountWindows <= lintCount

                '+ Se asignan los valores a las variables públicas

                Call Item(lintCountWindows)

                '+ Se busca la imagen a colocar en los links
                Call SecurityFrame(sUserSchema)

                '+ Se realiza actualización de paginas para tramite de recobro
                If sTransaction = CStr(eClaimTransac.clngRecovery) Then
                    Call RecoveryFrame(CDbl(sClaim), nRecoveryTransac)
                End If

                If sDescript <> String.Empty Then
                    lstrHTMLCode = lstrHTMLCode & lclsSequence.makeRow(sCodisp, sCodispl, lintAction, sShort_des, mintPageImage, , , , , , , sDescript, mintModules, nWindowTy)
                End If

                '+ Se mueve al siguiente registro encontrado
                lintCountWindows = lintCountWindows + 1
            Loop
            lstrHTMLCode = lstrHTMLCode & lclsSequence.closeTable()
        End If

        LoadTabs = lstrHTMLCode

        If sTransaction = CStr(eClaimTransac.clngClaimIssue) Or Not lblnFind Then
            Call Add_Claim_win(lclsValues.StringToType(sClaim, eFunctions.Values.eTypeData.etdDouble), String.Empty, String.Empty, CInt(nUserCode), False)
        End If

LoadTabs_Err:
        If Err.Number Then
            LoadTabs = "LoadTabs: " & Err.Description
        End If
        On Error GoTo 0
        lclsSequence = Nothing
        lclsValues = Nothing
    End Function
	
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
				If sContent = "1" And lstrCodispl = "SI007" And Not Me.bPolVigency Then
					mintPageImage = eFunctions.Sequence.etypeImageSequence.eRequired
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
			End If
		End With
		
SecurityFrame_Err: 
		If Err.Number Then
			SecurityFrame = False
		End If
		On Error GoTo 0
		If Not mclsSecurSche Is Nothing Then
			mclsSecurSche = Nothing
		End If
	End Function
	
	'% RecoveryFrame: valida que la página para recobro sean valida para el esquema/usuario
    Private Function RecoveryFrame(ByVal nClaim As Double, Optional ByVal nRecoveryTransac As Integer = eRemoteDB.Constants.intNull) As Boolean
        Dim lclsRecover As Recover

        On Error GoTo RecoveryFrame_Err

        lclsRecover = New Recover

        If sCodispl = "SI012" Then
            If lclsRecover.Find(nClaim, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, nRecoveryTransac) Then
                mintPageImage = eFunctions.Sequence.etypeImageSequence.eOK
            Else
                mintPageImage = eFunctions.Sequence.etypeImageSequence.eRequired
            End If
        Else
            If sCodispl = "SI013" Then
                mintPageImage = eFunctions.Sequence.etypeImageSequence.eRequired
                 If lclsRecover.Find(nClaim, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, nRecoveryTransac) Then
                    mintPageImage = eFunctions.Sequence.etypeImageSequence.eOK
                Else
                    mintPageImage = eFunctions.Sequence.etypeImageSequence.eRequired
                End If
            End If
        End If

RecoveryFrame_Err:
        If Err.Number Then
            RecoveryFrame = False
        End If
        On Error GoTo 0
        lclsRecover = Nothing
    End Function
	
	'%Update:
	Public Function Update() As Boolean
		Dim lrecinsClaim_win As eRemoteDB.Execute
		
		On Error GoTo Update_Err
		
		lrecinsClaim_win = New eRemoteDB.Execute
		
		With lrecinsClaim_win
			.StoredProcedure = "insClaim_win"
			.Parameters.Add("nClaim", nClaim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sV_conclaim", sV_conclaim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 30, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sV_winclaim", sV_winclaim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 240, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUserCode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Update = .Run(False)
		End With
		
Update_Err: 
		If Err.Number Then
			Update = False
		End If
		On Error GoTo 0
		lrecinsClaim_win = Nothing
	End Function
	
	'% Find:
	Public Function Find(ByVal nClaim As Double) As Boolean
		Dim lrecreaClaim_win As eRemoteDB.Execute
		Dim lintIndex As Integer
		Dim lintAux As Integer
		Dim lstrCodispl As String
		
		On Error GoTo Find_Err
		
		lrecreaClaim_win = New eRemoteDB.Execute
		
		'+ Definición de parámetros para stored procedure 'insudb.reaClaim_win'
		'+ Información leída el 19/01/2001 5:26:00 PM
		
		With lrecreaClaim_win
			.StoredProcedure = "reaClaim_win"
			.Parameters.Add("nClaim", nClaim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				Find = True
				sV_conclaim = .FieldToClass("sV_conclaim")
				sV_winclaim = .FieldToClass("sV_winclaim")
				.RCloseRec()
			Else
				Find = False
			End If
		End With
		
		If Find Then
			lintIndex = 0
			lintAux = 1
			mblnChargeArr = True
			Do While lintIndex < Len(sV_conclaim)
				lstrCodispl = Trim(Mid(sV_winclaim, lintAux, 8))
				mudtDesFrame(lintIndex).sCodispl = lstrCodispl
				mudtDesFrame(lintIndex).sContent = Mid(sV_conclaim, lintIndex + 1, 1)
				
				lintAux = lintAux + 8
				lintIndex = lintIndex + 1
			Loop 
		End If
		
Find_Err: 
		If Err.Number Then
			Find = False
		End If
		On Error GoTo 0
		lrecreaClaim_win = Nothing
	End Function
	
	'% Add_Claim_win: Esta rutina realiza la actualización de la secuencia de ventanas de siniestros
	'%                La rutina recibe el código del frame y el estado en el cual este debe ser
	'%                actualizado en la secuencia, es decir, 1.- Sin Contenido y 2.-Con Contenido
	Public Function Add_Claim_win(ByVal nClaim As Double, ByVal sCodispl As String, ByVal sContent As String, ByVal nUserCode As Integer, Optional ByVal bNotLoadTab As Boolean = True) As Boolean
		Dim llngCount As Integer
		Dim lstrV_conclaim As string
		Dim lstrV_winclaim As string
		Dim lclsRemote As eRemoteDB.Execute
		
		On Error GoTo Add_Claim_win_Err
		
		If bNotLoadTab Then
			'+ Se actualiza la secuencia de ventanas
			lclsRemote = New eRemoteDB.Execute
			With lclsRemote
				.StoredProcedure = "insAddClaim_Win"
				.Parameters.Add("nClaim", nClaim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("sCodispl", sCodispl, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 8, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("sContent", sContent, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nUserCode", nUserCode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				Add_Claim_win = .Run(False)
			End With
		Else
            lstrV_conclaim = String.Empty
            lstrV_winclaim = String.Empty
			
			If Find_Item(sCodispl, True) Then
				Refresh_Content = sContent
			End If
			
			For llngCount = 0 To CountItem
				Call Item(llngCount)
                lstrV_conclaim = Mid(lstrV_conclaim, 1, llngCount) & Me.sContent
                'lstrV_winclaim = Mid(lstrV_winclaim, 1, llngCount * 8) & Me.sCodispl
                lstrV_winclaim = Mid(lstrV_winclaim, 1, llngCount * 8) & Me.sCodispl.PadRight(8, " ")
			Next llngCount
			
            If Trim(lstrV_winclaim) <> String.Empty And Trim(lstrV_conclaim) <> String.Empty Then
                '+ Se actualiza Claim_win
                With Me
                    .nClaim = nClaim
                    .sV_conclaim = lstrV_conclaim
                    .sV_winclaim = lstrV_winclaim
                    .nUserCode = nUserCode
                    Add_Claim_win = Update()
                End With
            End If
		End If
		
Add_Claim_win_Err: 
		If Err.Number Then
			Add_Claim_win = False
		End If
		On Error GoTo 0
		lclsRemote = Nothing
	End Function
	
	'%ConcatMessage: Función que concatena el message de error.
	Public Function ConcatMessage(ByVal lintError As Integer) As String
        sMessage = sMessage & "||" & CStr(lintError)
        Return sMessage
    End Function
	
	'%ConcatMessage: Función que concatena el message de error.
	Public Function PrintMessage() As String
		Dim lrecMessage As eRemoteDB.Execute
		
		'+Definición de parámetros para stored procedure 'InsSi008pkg.InsValSi008_KUpd'
		'+Información leída el 24/04/2003
		On Error GoTo PrintMessage_Err
		lrecMessage = New eRemoteDB.Execute
		With lrecMessage
			.StoredProcedure = "insMessage"
			.Parameters.Add("lstrMessage", sMessage, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 400, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("lstrResult", String.Empty, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 4000, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Run(False)
			PrintMessage = sMessage1 & .Parameters("lstrResult").Value
			
		End With
PrintMessage_Err: 
		If Err.Number Then
			PrintMessage = "PrintMessage: " & Err.Description
		End If
		On Error GoTo 0
		lrecMessage = Nothing
	End Function
End Class






