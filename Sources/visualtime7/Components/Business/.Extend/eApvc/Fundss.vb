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
	Public Function Add(ByVal nBranch As Integer, ByVal nFunds As Integer, ByVal nProduct As Integer, ByVal dEffecdate As Date, ByVal nBuy_cost As Double, ByVal dNulldate As Object, ByVal nPartic_min As Double, ByVal nParticip As Double, ByVal nSell_cost As Double, ByVal sActivFound As String, ByVal sDescript As String, ByVal nOrigin As Integer, ByVal sOrigin As String, ByVal nIntProy As Double, ByVal nIntProyVarMax As Double, Optional ByVal nIntProyVarCle As Double = 0) As Funds
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
		End With
		
		mCol.Add(objNewMember, "Funds" & CStr(nFunds) & "|" & CStr(nOrigin))
		Add = objNewMember
		
		'UPGRADE_NOTE: Object objNewMember may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		objNewMember = Nothing
		
		Exit Function
ErrorHandler: 
		'UPGRADE_NOTE: Object objNewMember may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		objNewMember = Nothing
		'UPGRADE_NOTE: Object Add may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		Add = Nothing
	End Function
	
	'**%Objective: Reads all actives funds related to an specific line of business - Product
	'%Objetivo: Lee todos los fondos activos asociados a un Ramo - Producto
	Public Function Find(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal dEffecdate As Date) As Boolean
		Dim lrecreaFunds As Object
		Dim lclsFunds As Funds
		
		On Error GoTo ErrorHandler
		lrecreaFunds = eRemoteDB.NetHelper.CreateClassInstance("eRemoteDB.Execute")
		lclsFunds = New Funds
		
		Find = True
		
		With lrecreaFunds
			.StoredProcedure = "reaFunds_apvc"
			
			.Parameters.Add("nBranch", nBranch, defination.eRmtDataDir.rdbParamInput, defination.eRmtDataType.rdbInteger, 22, 0, 10, defination.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, defination.eRmtDataDir.rdbParamInput, defination.eRmtDataType.rdbInteger, 22, 0, 10, defination.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, defination.eRmtDataDir.rdbParamInput, defination.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, defination.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sCertype", String.Empty, defination.eRmtDataDir.rdbParamInput, defination.eRmtDataType.rdbVarChar, 1, 0, 0, defination.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", 0, defination.eRmtDataDir.rdbParamInput, defination.eRmtDataType.rdbDouble, 22, 0, 10, defination.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", 0, defination.eRmtDataDir.rdbParamInput, defination.eRmtDataType.rdbDouble, 22, 0, 10, defination.eRmtDataAttrib.rdbParamNullable)
			'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
			.Parameters.Add("nOrigin", System.DBNull.Value, defination.eRmtDataDir.rdbParamInput, defination.eRmtDataType.rdbDouble, 22, 0, 10, defination.eRmtDataAttrib.rdbParamNullable)
			
			If .Run Then
				Do While Not .EOF
					lclsFunds = Add(.FieldToClass("nBranch"), .FieldToClass("nFunds"), .FieldToClass("nProduct"), .FieldToClass("dEffecdate"), .FieldToClass("nBuy_cost"), .FieldToClass("dNulldate"), .FieldToClass("nPartic_min"), .FieldToClass("nParticip"), .FieldToClass("nSell_cost"), "2", .FieldToClass("sDescript"), .FieldToClass("nOrigin"), .FieldToClass("sOrigin"), .FieldToClass("nIntProy"), .FieldToClass("nIntProyVarMax"), .FieldToClass("nIntProyVarCle"))
					.RNext()
				Loop 
				
				.RCloseRec()
			End If
		End With
		
		'UPGRADE_NOTE: Object lrecreaFunds may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaFunds = Nothing
		'UPGRADE_NOTE: Object lclsFunds may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsFunds = Nothing
		
		Exit Function
ErrorHandler: 
		'UPGRADE_NOTE: Object lrecreaFunds may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaFunds = Nothing
		'UPGRADE_NOTE: Object lclsFunds may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsFunds = Nothing
		
		Find = False
	End Function
	
	Public Function CountFundsAccount(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date) As Short
		
		Dim lrecreaFunds As Object
		
		
		'**- The variable to store the participation share of the policy is defined
		'- Se define la variable que permitira conocer el porcentaje de participación de la póliza
		
		Dim lintCount As Integer
		
		lintCount = 0
		
		On Error GoTo CountFundsAccount_err
		
		lrecreaFunds = eRemoteDB.NetHelper.CreateClassInstance("eRemoteDB.Execute")
		
		With lrecreaFunds
			.StoredProcedure = "TABFUNDSACCOUNTPKG.TABFUNDSACCOUNT"
			.Parameters.Add("sShownum", "2", defination.eRmtDataDir.rdbParamInput, defination.eRmtDataType.rdbVarChar, 1, 0, 0, defination.eRmtDataAttrib.rdbParamNullable)
			'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
			.Parameters.Add("sCondition", System.DBNull.Value, defination.eRmtDataDir.rdbParamInput, defination.eRmtDataType.rdbVarChar, 30, 0, 0, defination.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, defination.eRmtDataDir.rdbParamInput, defination.eRmtDataType.rdbInteger, 22, 0, 10, defination.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, defination.eRmtDataDir.rdbParamInput, defination.eRmtDataType.rdbInteger, 22, 0, 10, defination.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, defination.eRmtDataDir.rdbParamInput, defination.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, defination.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sCertype", sCertype, defination.eRmtDataDir.rdbParamInput, defination.eRmtDataType.rdbVarChar, 1, 0, 0, defination.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, defination.eRmtDataDir.rdbParamInput, defination.eRmtDataType.rdbInteger, 22, 0, 10, defination.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, defination.eRmtDataDir.rdbParamInput, defination.eRmtDataType.rdbInteger, 22, 0, 10, defination.eRmtDataAttrib.rdbParamNullable)
			
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
		'UPGRADE_NOTE: Object lrecreaFunds may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaFunds = Nothing
	End Function
	
	
	'**%Objective: This function gets the information of the line of business/product actives funds
	'**%           those funds will be associated to the dealing policy in the window VI006
	'%Objetivo: Permite obtener la información de los fondos activos pertenecientes a un Ramo-Producto
	'%          que posterirmente serán asociados a la poliza en tratamineto por medio de la ventana VI006
	Public Function Find_FundstoPol(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, Optional ByVal sSche_code As String = "", Optional ByVal sCodispl As String = "", Optional ByVal nOrigin As Integer = 0) As Boolean
		
		Dim lrecreaFunds As Object
		Dim lclsFunds As Funds
		Dim lclsFunds_pol As Object
		Dim lcolFunds_Pol As Object
		Dim lclsSecurSche As Object
		
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
		
		On Error GoTo Find_FundstoPol_err
		
		lrecreaFunds = eRemoteDB.NetHelper.CreateClassInstance("eRemoteDB.Execute")
		lclsFunds = New Funds
		lclsFunds_pol = eRemoteDB.NetHelper.CreateClassInstance("ePolicy.Funds_Pol")
		lcolFunds_Pol = eRemoteDB.NetHelper.CreateClassInstance("ePolicy.Funds_pols")
		lclsSecurSche = eRemoteDB.NetHelper.CreateClassInstance("eSecurity.Secur_sche")
		
		lintSelected = 2
		
		With lrecreaFunds
			.StoredProcedure = "reaFunds_apvc"
			.Parameters.Add("nBranch", nBranch, defination.eRmtDataDir.rdbParamInput, defination.eRmtDataType.rdbInteger, 22, 0, 10, defination.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, defination.eRmtDataDir.rdbParamInput, defination.eRmtDataType.rdbInteger, 22, 0, 10, defination.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, defination.eRmtDataDir.rdbParamInput, defination.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, defination.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sCertype", sCertype, defination.eRmtDataDir.rdbParamInput, defination.eRmtDataType.rdbVarChar, 1, 0, 0, defination.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, defination.eRmtDataDir.rdbParamInput, defination.eRmtDataType.rdbDouble, 22, 0, 10, defination.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, defination.eRmtDataDir.rdbParamInput, defination.eRmtDataType.rdbDouble, 22, 0, 10, defination.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nOrigin", nOrigin, defination.eRmtDataDir.rdbParamInput, defination.eRmtDataType.rdbDouble, 22, 0, 10, defination.eRmtDataAttrib.rdbParamNullable)
			
			If .Run Then
				lblnFindCol = lcolFunds_Pol.Find(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate, defination.eConstNull.NumNull)
				
				Do While Not .EOF
					lintParticip = .FieldToClass("nParticip")
					lintproy = .FieldToClass("nIntProy")
					lintproyvar = .FieldToClass("nIntProyVarMax")
					lstrActivFound = "2"
					
					If lblnFindCol Then
						lintSelected = IIf(lcolFunds_Pol.FindItem(.FieldToClass("nFunds"), lintParticip, .FieldToClass("nOrigin"), lintproy, lintproyvar), 1, 2)
						lstrActivFound = lcolFunds_Pol.sActivFound
					End If
					
					lclsFunds = Add(.FieldToClass("nBranch"), .FieldToClass("nFunds"), .FieldToClass("nProduct"), .FieldToClass("dEffecdate"), .FieldToClass("nBuy_cost"), .FieldToClass("dNulldate"), .FieldToClass("nPartic_min"), lintParticip, .FieldToClass("nSell_cost"), lstrActivFound, .FieldToClass("sDescript"), .FieldToClass("nOrigin"), .FieldToClass("sOrigin"), lintproy, lintproyvar)
					
					lclsFunds.nSelected = lintSelected
					
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
		'UPGRADE_NOTE: Object lrecreaFunds may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaFunds = Nothing
		'UPGRADE_NOTE: Object lclsFunds may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsFunds = Nothing
		'UPGRADE_NOTE: Object lclsFunds_pol may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsFunds_pol = Nothing
		'UPGRADE_NOTE: Object lcolFunds_Pol may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lcolFunds_Pol = Nothing
		'UPGRADE_NOTE: Object lclsSecurSche may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsSecurSche = Nothing
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
			'UPGRADE_NOTE: Object CurrentFunds may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
			CurrentFunds = Nothing
		Else
			FindItem = True
		End If
		
		On Error GoTo ErrorHandler
		
		Exit Function
ErrorHandler: 
		'UPGRADE_NOTE: Object CurrentFunds may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		CurrentFunds = Nothing
		
		FindItem = False
	End Function
	
	'%Objetivo: Permite obtener la información de los fondos activos pertenecientes a un Ramo-Producto
	'%          que posterirmente serán asociados a la poliza en tratamineto por medio de la ventana VI006
	Public Function Find_FundstoPolMat(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, Optional ByVal sSche_code As String = "", Optional ByVal sCodispl As String = "") As Boolean
		
		Dim lrecreaFunds As Object
		Dim lclsFunds As Funds
		Dim lclsFunds_pol As Funds_CO_P
		Dim lcolFunds_Pol As Funds_CO_Ps
		Dim lclsSecurSche As Object
		
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
		
		On Error GoTo Find_FundstoPol_err
		
		lrecreaFunds = eRemoteDB.NetHelper.CreateClassInstance("eRemoteDB.Execute")
		lclsFunds = New Funds
		lclsFunds_pol = New Funds_CO_P
		lcolFunds_Pol = New Funds_CO_Ps
		lclsSecurSche = eRemoteDB.NetHelper.CreateClassInstance("eSecurity.Secur_sche")
		
		lintSelected = 2
		
		With lrecreaFunds
			.StoredProcedure = "REAFUNDS_POLMAT"
			.Parameters.Add("nBranch", nBranch, defination.eRmtDataDir.rdbParamInput, defination.eRmtDataType.rdbInteger, 22, 0, 10, defination.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, defination.eRmtDataDir.rdbParamInput, defination.eRmtDataType.rdbInteger, 22, 0, 10, defination.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, defination.eRmtDataDir.rdbParamInput, defination.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, defination.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sCertype", sCertype, defination.eRmtDataDir.rdbParamInput, defination.eRmtDataType.rdbVarChar, 1, 0, 0, defination.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, defination.eRmtDataDir.rdbParamInput, defination.eRmtDataType.rdbDouble, 22, 0, 10, defination.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, defination.eRmtDataDir.rdbParamInput, defination.eRmtDataType.rdbDouble, 22, 0, 10, defination.eRmtDataAttrib.rdbParamNullable)
			
			If .Run Then
				lblnFindCol = lcolFunds_Pol.Find(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate, defination.eConstNull.NumNull)
				
				Do While Not .EOF
					lintParticip = .FieldToClass("nParticip")
					lintproy = .FieldToClass("nIntProy")
					lintproyvar = .FieldToClass("nIntProyVarMax")
					lstrActivFound = "2"
					
					If lblnFindCol Then
						lintSelected = IIf(lcolFunds_Pol.FindItem(.FieldToClass("nFunds"), lintParticip, .FieldToClass("nOrigin"), lintproy, lintproyvar), 1, 2)
						lstrActivFound = CStr(lcolFunds_Pol.sActivFound)
					End If
					
					lclsFunds = Add(.FieldToClass("nBranch"), .FieldToClass("nFunds"), .FieldToClass("nProduct"), .FieldToClass("dEffecdate"), .FieldToClass("nBuy_cost"), .FieldToClass("dNulldate"), .FieldToClass("nPartic_min"), lintParticip, .FieldToClass("nSell_cost"), lstrActivFound, .FieldToClass("sDescript"), .FieldToClass("nOrigin"), .FieldToClass("sOrigin"), lintproy, lintproyvar)
					
					lclsFunds.nSelected = lintSelected
					
					.RNext()
				Loop 
				
				Find_FundstoPolMat = True
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
			Find_FundstoPolMat = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecreaFunds may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaFunds = Nothing
		'UPGRADE_NOTE: Object lclsFunds may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsFunds = Nothing
		'UPGRADE_NOTE: Object lclsFunds_pol may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsFunds_pol = Nothing
		'UPGRADE_NOTE: Object lcolFunds_Pol may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lcolFunds_Pol = Nothing
		'UPGRADE_NOTE: Object lclsSecurSche may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsSecurSche = Nothing
	End Function
	
	
	'**%Objective: Use when making reference to an element of the collection
	'%Objetivo: Se usa al hacer referencia a un elemento de la colección
	Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As Funds
		Get
			On Error GoTo ErrorHandler
			Item = mCol.Item(vntIndexKey)
			
			Exit Property
ErrorHandler: 
			'UPGRADE_NOTE: Object Item may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
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
			''UPGRADE_NOTE: Object NewEnum may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
			'NewEnum = Nothing
		'End Get
	'End Property
	
	Public Function GetEnumerator() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
		'UPGRADE_TODO: Uncomment and change the following line to return the collection enumerator. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="95F9AAD0-1319-4921-95F0-B9D3C4FF7F1C"'
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
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		On Error GoTo ErrorHandler
		mCol = New Collection
		'UPGRADE_NOTE: Object CurrentFunds may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		CurrentFunds = Nothing
		
		Exit Sub
ErrorHandler: 
		'UPGRADE_NOTE: Object mCol may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		mCol = Nothing
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
	
	'**%Objective: Controls the destruction of an instance of the collection
	'%Objetivo: Controla la destrucción de una instancia de la colección
	'UPGRADE_NOTE: Class_Terminate was upgraded to Class_Terminate_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Terminate_Renamed()
		On Error GoTo ErrorHandler
		
ErrorHandler: 
		
	End Sub
	Protected Overrides Sub Finalize()
		Class_Terminate_Renamed()
		MyBase.Finalize()
	End Sub
End Class






