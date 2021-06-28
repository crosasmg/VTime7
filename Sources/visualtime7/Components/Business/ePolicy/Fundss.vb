Option Strict Off
Option Explicit On
Public Class Fundss
	Implements System.Collections.IEnumerable
	'**+Objective: Collection that supports the class: Fundss
	'**+Version: $$Revision: 14 $
	'+Objetivo: Colección que le da soporte a la clase: Fundss
	'+Version: $$Revision: 14 $
	'%-------------------------------------------------------%'
	'% $Workfile:: Fundss.cls                               $%'
	'% $Author:: Gazuaje                                    $%'
	'% $Date:: 3/07/06 7:41p                                $%'
	'% $Revision:: 14                                       $%'
	'%-------------------------------------------------------%'
	
	'**-Objective:
	'-Objetivo:
	Private mCol As Collection
	
	'**-Objective:
	'-Objetivo:
	Public CurrentFunds As Funds
	Public bUpdateFound As Boolean
	
	'**%Objective: Adds the fields to the collection of nominal values
	'%Objetivo: Agrega los campos a la colección de valores nominales
	Public Function Add(ByVal nBranch As Integer, ByVal nFunds As Integer, ByVal nProduct As Integer, ByVal dEffecdate As Date, ByVal nBuy_cost As Double, ByVal dNulldate As Object, ByVal nPartic_min As Double, ByVal nParticip As Double, ByVal nSell_cost As Double, ByVal sActivFound As String, ByVal sDescript As String, ByVal nOrigin As Integer, ByVal sOrigin As String, ByVal nIntProy As Double, ByVal nIntProyVarMax As Double, Optional ByVal nIntProyVarCle As Double = 0, Optional ByVal sVigen As String = "") As Funds
		Dim objNewMember As Funds
		
		On Error GoTo ErrorHandler
		objNewMember = New Funds
		
		With objNewMember
			.nBranch = nBranch
			.nFunds = nFunds
			.nProduct = nProduct
			.dEffecdate = dEffecdate
			.nBuy_cost = nBuy_cost
			.dNulldate = dNulldate
			.nPartic_min = nPartic_min
			.nParticip = nParticip
			.nSell_cost = nSell_cost
			.sActivFound = sActivFound
			.nOrigin = nOrigin
			.sDescript = sDescript
			.sOrigin = sOrigin
			.nIntProy = nIntProy
			.nIntProyVarMax = nIntProyVarMax
			.nIntProyVarCle = nIntProyVarCle
			.sVigen = sVigen
		End With
		
		mCol.Add(objNewMember, "Funds" & CStr(nFunds) & "|" & CStr(nOrigin))
		Add = objNewMember
		
		objNewMember = Nothing
		
		Exit Function
ErrorHandler: 
		objNewMember = Nothing
		Add = Nothing
	End Function
	
	'**%Objective: Reads all actives funds related to an specific line of business - Product
	'%Objetivo: Lee todos los fondos activos asociados a un Ramo - Producto
	Public Function Find(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal dEffecdate As Date) As Boolean
		Dim lrecreaFunds As eRemoteDB.Execute
		Dim lclsFunds As Funds
		
		On Error GoTo ErrorHandler
		lrecreaFunds = New eRemoteDB.Execute
		lclsFunds = New Funds
		
		Find = True
		
		With lrecreaFunds
			.StoredProcedure = "reaFunds"
			
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
			.Parameters.Add("nOrigin", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sVigen", String.Empty, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run Then
				Do While Not .EOF
					lclsFunds = Add(.FieldToClass("nBranch"), .FieldToClass("nFunds"), .FieldToClass("nProduct"), .FieldToClass("dEffecdate"), .FieldToClass("nBuy_cost"), .FieldToClass("dNulldate"), .FieldToClass("nPartic_min"), .FieldToClass("nParticip"), .FieldToClass("nSell_cost"), "2", .FieldToClass("sDescript"), .FieldToClass("nOrigin"), .FieldToClass("sOrigin"), .FieldToClass("nIntProy"), .FieldToClass("nIntProyVarMax"), .FieldToClass("nIntProyVarCle"), .FieldToClass("sVigen"))
					.RNext()
				Loop 
				
				.RCloseRec()
			End If
		End With
		
		lrecreaFunds = Nothing
		lclsFunds = Nothing
		
		Exit Function
ErrorHandler: 
		lrecreaFunds = Nothing
		lclsFunds = Nothing
		
		Find = False
	End Function
	
	
	'**%Objective: Reads the default selected funds defined by ranking MDO8004
	'%Objetivo: Lee la opción de fondo predeterminado por el ranking MDP8004
	Public Function FindDefaultOption(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nFunds As Integer) As Boolean
		Dim lrecreaFunds As eRemoteDB.Execute
		
		On Error GoTo ErrorHandler
		lrecreaFunds = New eRemoteDB.Execute
		
		FindDefaultOption = False
		
		With lrecreaFunds
			.StoredProcedure = "INSMDP8004PKG.REAFUNDSRANKINGDEFAULT"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nFunds", nFunds, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				If .FieldToClass("sDefault") = "1" Then
					FindDefaultOption = True
				End If
				.RCloseRec()
			End If
		End With
		
		lrecreaFunds = Nothing
		
		Exit Function
ErrorHandler: 
		lrecreaFunds = Nothing
		FindDefaultOption = False
	End Function
	
	
	'**%Objective: This function gets the information of the line of business/product actives funds
	'**%           those funds will be associated to the dealing policy in the window VI006
	'%Objetivo: Permite obtener la información de los fondos activos pertenecientes a un Ramo-Producto
	'%          que posterirmente serán asociados a la poliza en tratamineto por medio de la ventana VI006
	Public Function Find_FundstoPol(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, Optional ByVal sSche_code As String = "", Optional ByVal sCodispl As String = "", Optional ByVal nOrigin As Integer = 0, Optional ByVal nTransaction As Integer = 0, Optional ByVal nFirstLoad As Integer = 0) As Boolean
		
		Dim lrecreaFunds As eRemoteDB.Execute
		Dim lclsFunds As Funds
		Dim lclsFunds_pol As Funds_Pol
		Dim lcolFunds_Pol As Funds_pols
		Dim lclsSecurSche As eSecurity.Secur_sche
		
		'**- The variable to determines if the investment fund is related to the the policy is defined
		'- Se define la variable que permitira conocer la existencia de fondos de inversión
		'- asociados a la póliza
		
		Dim lblnFindCol As Boolean
		
		'**- The variable to store the participation share of the policy is defined
		'- Se define la variable que permitira conocer el porcentaje de participación de la póliza
		
		Dim lintParticip As Integer
		Dim lintproy As Double
		Dim lintproyvar As Double
		
		'**- The variable to store the relation between the fund and the policy is defined
		'- Se define la variable que permitira almacenar la asociación fondo-póliza
		
		Dim lintSelected As Integer
		Dim lstrActivFound As String
        Dim lstrVigen As String = ""
        Dim lblnAmendent As Boolean
		Dim lblnFundPol_NoActiv As Boolean
		
		On Error GoTo Find_FundstoPol_err
		
		lrecreaFunds = New eRemoteDB.Execute
		lclsFunds = New Funds
		lclsFunds_pol = New Funds_Pol
		lcolFunds_Pol = New Funds_pols
		lclsSecurSche = New eSecurity.Secur_sche
		
		lintSelected = 2
		
		'+Si se trata de Cotización,  Propuesta o Emisión,  se deben ocultar los fondos que se encuentren según producto restringido para la venta
        If nTransaction = Constantes.PolTransac.clngRecuperation Or nTransaction = Constantes.PolTransac.clngPolicyIssue Or nTransaction = Constantes.PolTransac.clngCertifIssue Or nTransaction = Constantes.PolTransac.clngPolicyQuotation Or nTransaction = Constantes.PolTransac.clngCertifQuotation Or nTransaction = Constantes.PolTransac.clngPolicyProposal Or nTransaction = Constantes.PolTransac.clngCertifProposal Then
            lstrVigen = "2"
        End If

        '+Si se trata de Cotización,  Propuesta de Endoso/Rehabilitación, o Endoso directo,
        '+se deben ocultar los fondos que se encuentren según producto restringido para la venta y no pertenezcan actualmente a la póliza.
        If nTransaction = Constantes.PolTransac.clngPolicyQuotAmendent Or nTransaction = Constantes.PolTransac.clngCertifQuotAmendent Or nTransaction = Constantes.PolTransac.clngPolicyPropAmendent Or nTransaction = Constantes.PolTransac.clngCertifPropAmendent Or nTransaction = Constantes.PolTransac.clngPolicyAmendment Or nTransaction = Constantes.PolTransac.clngCertifAmendment Then
            lblnAmendent = True
        End If
		
		With lrecreaFunds
			.StoredProcedure = "reaFundsVI006"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nOrigin", nOrigin, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sVigen", lstrVigen, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run Then
				lblnFindCol = lcolFunds_Pol.Find(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate, eRemoteDB.Constants.intNull)
				
				Do While Not .EOF
					lintParticip = .FieldToClass("nParticip")
					lintproy = .FieldToClass("nIntProy")
					lintproyvar = .FieldToClass("nIntProyVarMax")
					lstrActivFound = "2"
					
					If lblnFindCol Then
						lintSelected = IIf(lcolFunds_Pol.FindItem(.FieldToClass("nFunds"), lintParticip, .FieldToClass("nOrigin"), lintproy, lintproyvar), 1, 2)
						lblnFundPol_NoActiv = lclsFunds_pol.ValFundsPol_NoVigen(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate)
						lstrActivFound = CStr(lcolFunds_Pol.sActivFound)
					Else
						lintSelected = 2
					End If
					
					'+ Se verifica si el fondo ha sido marcado como predeterminado por el ranking
					If nFirstLoad = 1 And lstrVigen = "2" And lintSelected = 2 Then
						If FindDefaultOption(nBranch, nProduct, .FieldToClass("nFunds")) Then
							lintSelected = 1
						End If
					End If
					
					
					If Not lblnAmendent Or .FieldToClass("sVigen") = "2" Or (.FieldToClass("sVigen") = "1" And lblnFundPol_NoActiv And sCodispl <> "VI006") Or lintSelected = 1 Then
						lclsFunds = Add(.FieldToClass("nBranch"), .FieldToClass("nFunds"), .FieldToClass("nProduct"), .FieldToClass("dEffecdate"), .FieldToClass("nBuy_cost"), .FieldToClass("dNulldate"), .FieldToClass("nPartic_min"), lintParticip, .FieldToClass("nSell_cost"), lstrActivFound, .FieldToClass("sDescript"), .FieldToClass("nOrigin"), .FieldToClass("sOrigin"), lintproy, lintproyvar, .FieldToClass("nIntProyVarCle"), .FieldToClass("sVigen"))
						lclsFunds.nSelected = lintSelected
					End If
					.RNext()
				Loop 
				
				Find_FundstoPol = True
				.RCloseRec()
			End If
		End With
		
		bUpdateFound = True
		
		If sSche_code <> String.Empty Then
			If Not lclsSecurSche.valTransAccess(sSche_code, sCodispl, "1") Then
				bUpdateFound = False
			End If
		End If
		
Find_FundstoPol_err: 
		If Err.Number Then
			Find_FundstoPol = False
		End If
		On Error GoTo 0
		lrecreaFunds = Nothing
		lclsFunds = Nothing
		lclsFunds_pol = Nothing
		lcolFunds_Pol = Nothing
		lclsSecurSche = Nothing
	End Function
	
	'**%Objective: Verifies when a policy has, at least, one fund associated
	'%Objetivo: Verificar que una póliza tiene al algún fondo registrado
	Public Function PolicyHasAnyFund(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double) As Boolean
		
		Dim lrecreaFunds As eRemoteDB.Execute
		Dim lintExists As Double
		
		lrecreaFunds = New eRemoteDB.Execute
		
		With lrecreaFunds
			.StoredProcedure = "reaFundsVI006Exists"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run Then
				If .FieldToClass("nExists") = 1 Then
					PolicyHasAnyFund = True
				Else
					PolicyHasAnyFund = False
				End If
			End If
		End With
		
PolicyHasAnyFund_err: 
		If Err.Number Then
			PolicyHasAnyFund = False
		End If
		On Error GoTo 0
		lrecreaFunds = Nothing
	End Function
	
	
	Public Function CountFundsAccount(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date) As Short
		
		Dim lrecreaFunds As eRemoteDB.Execute
		
		
		'**- The variable to store the participation share of the policy is defined
		'- Se define la variable que permitira conocer el porcentaje de participación de la póliza
		
		Dim lintCount As Integer
		
		lintCount = 0
		
		On Error GoTo CountFundsAccount_err
		
		lrecreaFunds = New eRemoteDB.Execute
		
		With lrecreaFunds
			.StoredProcedure = "TABFUNDSACCOUNTPKG.TABFUNDSACCOUNT"
			.Parameters.Add("sShownum", "2", eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
			.Parameters.Add("sCondition", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 30, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run Then
				
				Do While Not .EOF
					lintCount = lintCount + 1
					.RNext()
				Loop 
				CountFundsAccount = lintCount
				.RCloseRec()
			End If
		End With
		
		
CountFundsAccount_err: 
		If Err.Number Then
			CountFundsAccount = 0
		End If
		On Error GoTo 0
		lrecreaFunds = Nothing
	End Function
	'**%Objective: Allows to determines if the fund in treatment is associated to the policy
	'**%           or not. If this is positive obtain the participation of the same (VI006)
	'%Objetivo: Permite determinar si el fondo en tratamiento se encuentra o no
	'%          asociado a la póliza. De resultar afirmativo obtiene la participación del mismo(VI006)
	Public Function FindItem(ByVal nFund As Integer, ByVal nOrigin As Integer, ByVal nIntProy As Double, ByVal nIntProyVar As Double) As Boolean
		On Error GoTo ErrorHandler
		On Error Resume Next
		
		CurrentFunds = mCol.Item("Funds" & CStr(nFund) & "|" & CStr(nOrigin))
		
		If Err.Number Then
			FindItem = False
			CurrentFunds = Nothing
		Else
			FindItem = True
		End If
		
		On Error GoTo ErrorHandler
		
		Exit Function
ErrorHandler: 
		CurrentFunds = Nothing
		
		FindItem = False
	End Function
	
	'**%Objective: Use when making reference to an element of the collection
	'%Objetivo: Se usa al hacer referencia a un elemento de la colección
	Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As Funds
		Get
			On Error GoTo ErrorHandler
			Item = mCol.Item(vntIndexKey)
			
			Exit Property
ErrorHandler: 
			Item = Nothing
		End Get
	End Property
	
	'**%Objective: Returns the number of elements that the collection has
	'%Objetivo: Devuelve el número de elementos que posee la colección
	Public ReadOnly Property Count() As Integer
		Get
			On Error GoTo ErrorHandler
			Count = mCol.Count()
			
			Exit Property
ErrorHandler: 
			Count = 0
		End Get
	End Property
	
	'**%Objective: Enumerates the collection for use in a For Each...Next loop
	'%Objetivo: Permite enumerar la colección para utilizarla en un ciclo For Each... Next
	'UPGRADE_NOTE: NewEnum property was commented out. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="B3FC1610-34F3-43F5-86B7-16C984F0E88E"'
	'Public ReadOnly Property NewEnum() As stdole.IUnknown
		'Get
			'On Error GoTo ErrorHandler
			'NewEnum = mCol._NewEnum
			'
			'Exit Property
'ErrorHandler: '
			'NewEnum = Nothing
		'End Get
	'End Property
	
	Public Function GetEnumerator() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
				GetEnumerator = mCol.GetEnumerator
	End Function
	
	'**%Objective: Deletes an element from the collection
	'%Objetivo: Elimina un elemento de la colección
	Public Sub Remove(ByRef vntIndexKey As Object)
		On Error GoTo ErrorHandler
		mCol.Remove(vntIndexKey)
		
		Exit Sub
ErrorHandler: 
		
	End Sub
	
	'**%Objective: Controls the creation of an instance of the collection
	'%Objetivo: Controla la creación de una instancia de la colección
	Private Sub Class_Initialize_Renamed()
		On Error GoTo ErrorHandler
		mCol = New Collection
		CurrentFunds = Nothing
		
		Exit Sub
ErrorHandler: 
		mCol = Nothing
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
	
	'**%Objective: Controls the destruction of an instance of the collection
	'%Objetivo: Controla la destrucción de una instancia de la colección
	Private Sub Class_Terminate_Renamed()
		On Error GoTo ErrorHandler
		
ErrorHandler: 
		
	End Sub
	Protected Overrides Sub Finalize()
		Class_Terminate_Renamed()
		MyBase.Finalize()
	End Sub
End Class






