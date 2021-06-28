Option Strict Off
Option Explicit On
Public Class Funds_pols
	Implements System.Collections.IEnumerable
	'**+Objective: Collection that supports the class: Funds_pols
	'**+Version: $$Revision: $
	'+Objetivo: Colección que le da soporte a la clase: Funds_pols
	'+Version: $$Revision: $
	'%-------------------------------------------------------%'
	'% $Workfile::                                          $%'
	'% $Author::                                            $%'
	'% $Date::                                              $%'
	'% $Revision::                                          $%'
	'%-------------------------------------------------------%'
	
	'**-Objective:
	'-Objetivo:
	Private mCol As Collection
	'I - GIT - CRHP
	Public nParticip As Double
	Public sActivFound As Double
	Public nBuysTot As Double
	Public nSellsTot As Double
	
	'F - GIT - CRHP
	
	'**%Objective: Adds the fields to the collection of nominal values
	'%Objetivo: Agrega los campos a la colección de valores nominales
	Public Function Add(ByRef objNewMember As Funds_Pol) As Funds_Pol
		On Error GoTo Add_err
		
		If mCol Is Nothing Then
			mCol = New Collection
		End If
		mCol.Add(objNewMember)
		Add = objNewMember
		
Add_err: 
		On Error GoTo 0
		'UPGRADE_NOTE: Object Add may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		Add = Nothing
	End Function
	
	'**%Objective: Reads all the active funds associated to a policy
	'%Objetivo: Lee todos los fondos activos asociados a una póliza
	Public Function Find(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, ByVal nOrigin As Integer) As Boolean
		Dim lrecreaFunds_pol As eRemoteDB.Execute
		Dim lclsFundPol As ePolicy.Funds_Pol
		
		On Error GoTo Find_Err
		
		lrecreaFunds_pol = New eRemoteDB.Execute
		
		Find = True
		
		With lrecreaFunds_pol
			.StoredProcedure = "reaFunds_pol"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nOrigin", nOrigin, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Find = .Run
			If Find Then
				Do While Not .EOF
					lclsFundPol = New ePolicy.Funds_Pol
					lclsFundPol.nFunds = .FieldToClass("nFunds")
					lclsFundPol.dEffecdate = .FieldToClass("dEffecdate")
					lclsFundPol.dNulldate = .FieldToClass("dNulldate")
					lclsFundPol.nParticip = .FieldToClass("nParticip")
					lclsFundPol.sDescript = .FieldToClass("sDescript")
					lclsFundPol.sReaddress = .FieldToClass("sReaddress")
					lclsFundPol.nQuan_avail = .FieldToClass("nQuan_avail")
					lclsFundPol.nAmount = .FieldToClass("nAmount")
					lclsFundPol.nBuy_cost = .FieldToClass("nBuy_cost")
					lclsFundPol.nSell_cost = .FieldToClass("nSell_cost")
					lclsFundPol.sActivFound = .FieldToClass("sActivFound")
					lclsFundPol.nOrigin = .FieldToClass("nOrigin")
					lclsFundPol.nIntProy = .FieldToClass("nIntProy")
					lclsFundPol.nIntProyVar = .FieldToClass("nIntProyVar")
					lclsFundPol.nIntProyVarCle = .FieldToClass("nIntProyVarCle")
					Call Add(lclsFundPol)
					'UPGRADE_NOTE: Object lclsFundPol may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
					lclsFundPol = Nothing
					.RNext()
				Loop 
				.RCloseRec()
			End If
		End With
		
Find_Err: 
		If Err.Number Then
			Find = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecreaFunds_pol may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaFunds_pol = Nothing
	End Function
	
	'**%Objective: Allows to determines if the fund in treatment is associated to the policy
	'**%           or not. If this is positive obtain the participation of the same (VI006)
	'%Objetivo: Permite determinar si el fondo en tratamiento se encuentra o no
    '%          asociado a la póliza. De resultar afirmativo obtiene la participación del mismo(VI006)
    'OJO BYREF
    Public Function FindItem(ByVal nFunds As Integer, ByRef nParticip As Integer, ByVal nOrigin As Integer, ByRef nIntProy As Double, ByRef nIntProyVar As Double) As Boolean
        Dim lclsFunds_pol As Funds_Pol

        On Error GoTo FindItem_Err

        lclsFunds_pol = New Funds_Pol

        For Each lclsFunds_pol In mCol
            With lclsFunds_pol
                If .nFunds = nFunds And .nOrigin = nOrigin Then
                    nParticip = .nParticip
                    nIntProy = .nIntProy
                    nIntProyVar = .nIntProyVar
                    Me.sActivFound = IIf(.sActivFound = "1", 1, 2)
                    FindItem = True
                    Exit For
                Else
                    Me.sActivFound = CDbl("2")
                End If
            End With
        Next lclsFunds_pol


FindItem_Err:
        If Err.Number Then
            FindItem = False
        End If
        On Error GoTo 0
        'UPGRADE_NOTE: Object lclsFunds_pol may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsFunds_pol = Nothing
    End Function
	
	'**%Objective: Use when making reference to an element of the collection
	'**%           vntIndexKey contains the index or the password of the collection,
	'%Objetivo: Se usa al hacer referencia a un elemento de la colección
	'%          vntIndexKey contiene el índice o la clave de la colección,
	Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As Funds_Pol
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
		'UPGRADE_NOTE: Object mCol may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		mCol = Nothing
		
		Exit Sub
ErrorHandler: 
		
	End Sub
	Protected Overrides Sub Finalize()
		Class_Terminate_Renamed()
		MyBase.Finalize()
	End Sub
	
	'**%Objective: Reads all the active funds associated to a policy
	'%Objetivo: Lee todos los fondos activos asociados a una póliza
	Public Function Find_VI010(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, ByVal nOrigin As Integer) As Boolean
		Dim lrecreaFunds_pol As eRemoteDB.Execute
		Dim lclsFundPol As ePolicy.Funds_Pol
		
		On Error GoTo ErrorHandler
		
		lrecreaFunds_pol = New eRemoteDB.Execute
		
		Find_VI010 = True
		
		With lrecreaFunds_pol
			.StoredProcedure = "reaFunds_pol_VI010"
			
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nOrigin", nOrigin, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			Find_VI010 = .Run
			
			If Find_VI010 Then
				Do While Not .EOF
					lclsFundPol = New ePolicy.Funds_Pol
					lclsFundPol.nFunds = .FieldToClass("nFunds")
					lclsFundPol.dEffecdate = .FieldToClass("dEffecdate")
					lclsFundPol.dNulldate = .FieldToClass("dNulldate")
					lclsFundPol.nParticip = .FieldToClass("nParticip")
					lclsFundPol.sDescript = .FieldToClass("sDescript")
					lclsFundPol.sReaddress = .FieldToClass("sReaddress")
					lclsFundPol.nQuan_avail = .FieldToClass("nQuan_avail")
					lclsFundPol.nAmount = .FieldToClass("nAmount")
					lclsFundPol.nBuy_cost = .FieldToClass("nBuy_cost")
					lclsFundPol.nSell_cost = .FieldToClass("nSell_cost")
					lclsFundPol.sActivFound = .FieldToClass("sActivFound")
					lclsFundPol.nIntProy = .FieldToClass("nIntProy")
					lclsFundPol.nIntProyVar = .FieldToClass("nIntProyVar")
					lclsFundPol.nUnitsChange = .FieldToClass("nUnitsChange")
					lclsFundPol.sSel = .FieldToClass("sSel")
					lclsFundPol.sVigen = .FieldToClass("sVigen")
					Call Add(lclsFundPol)
					'UPGRADE_NOTE: Object lclsFundPol may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
					lclsFundPol = Nothing
					.RNext()
				Loop 
				
				.RCloseRec()
			End If
		End With
		
		'UPGRADE_NOTE: Object lrecreaFunds_pol may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaFunds_pol = Nothing
		
		Exit Function
ErrorHandler: 
		'UPGRADE_NOTE: Object lrecreaFunds_pol may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaFunds_pol = Nothing
		Find_VI010 = False
	End Function
	
	'**%Objective: Reads all the active funds associated to a policy
	'%Objetivo: Lee todos los fondos activos asociados a una póliza
	Public Function Find_VI016(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, ByVal nOrigin As Integer) As Boolean
		Dim lrecreaFunds_pol As eRemoteDB.Execute
		Dim lclsFundPol As ePolicy.Funds_Pol
		
		On Error GoTo ErrorHandler
		
		lrecreaFunds_pol = New eRemoteDB.Execute
		
		Find_VI016 = True
		
		With lrecreaFunds_pol
			.StoredProcedure = "reaFunds_pol_VI016"
			
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nOrigin", nOrigin, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			Find_VI016 = .Run
			
			If Find_VI016 Then
				Do While Not .EOF
					lclsFundPol = New ePolicy.Funds_Pol
					lclsFundPol.nFunds = .FieldToClass("nFunds")
					lclsFundPol.dEffecdate = .FieldToClass("dEffecdate")
					lclsFundPol.dNulldate = .FieldToClass("dNulldate")
					lclsFundPol.nParticip = .FieldToClass("nParticip")
					lclsFundPol.sDescript = .FieldToClass("sDescript")
					lclsFundPol.sShort_des = .FieldToClass("sShort_des")
					lclsFundPol.sReaddress = .FieldToClass("sReaddress")
					lclsFundPol.nQuan_avail = .FieldToClass("nQuan_avail")
					lclsFundPol.nAmount = .FieldToClass("nAmount")
					lclsFundPol.nBuy_cost = .FieldToClass("nBuy_cost")
					lclsFundPol.nSell_cost = .FieldToClass("nSell_cost")
					lclsFundPol.sActivFound = .FieldToClass("sActivFound")
					lclsFundPol.nIntProy = .FieldToClass("nIntProy")
					lclsFundPol.nIntProyVar = .FieldToClass("nIntProyVar")
					lclsFundPol.nUnitsChange = .FieldToClass("nUnitsChange")
					lclsFundPol.sSel = .FieldToClass("sSel")
					lclsFundPol.sVigen = .FieldToClass("sVigen")
					lclsFundPol.nTyp_Profitworker = .FieldToClass("nTyp_Profitworker")
					lclsFundPol.nAvailtobuy = .FieldToClass("nAvailtobuy")
					Call Add(lclsFundPol)
					'UPGRADE_NOTE: Object lclsFundPol may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
					lclsFundPol = Nothing
					.RNext()
				Loop 
				
				.RCloseRec()
			End If
		End With
		
		'UPGRADE_NOTE: Object lrecreaFunds_pol may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaFunds_pol = Nothing
		
		Exit Function
ErrorHandler: 
		'UPGRADE_NOTE: Object lrecreaFunds_pol may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaFunds_pol = Nothing
		Find_VI016 = False
	End Function
	
	Public Function Find_Request_VI010(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, ByVal nSwitchOrigin As Integer) As Boolean
		Dim lrecreaFunds_pol As eRemoteDB.Execute
		Dim lclsFundPol As ePolicy.Funds_Pol
		
		On Error GoTo ErrorHandler
		
		lrecreaFunds_pol = New eRemoteDB.Execute
		
		Find_Request_VI010 = True
		
		With lrecreaFunds_pol
			.StoredProcedure = "reaRequest_VI010"
			
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nSwitchOrigin", nSwitchOrigin, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			Find_Request_VI010 = .Run
			
			If Find_Request_VI010 Then
				Do While Not .EOF
					lclsFundPol = New ePolicy.Funds_Pol
					lclsFundPol.sBranch = .FieldToClass("sBranch")
					lclsFundPol.sProduct = .FieldToClass("sProduct")
					lclsFundPol.nBranch = .FieldToClass("nBranch")
					lclsFundPol.nProduct = .FieldToClass("nProduct")
					lclsFundPol.nPolicy = .FieldToClass("nPolicy")
					lclsFundPol.nCertif = .FieldToClass("nCertif")
					lclsFundPol.dEffecdate = .FieldToClass("dEffecdate")
					lclsFundPol.nBuysTot = .FieldToClass("nBuysTot")
					lclsFundPol.nSellsTot = .FieldToClass("nSellsTot")
					
					Call Add(lclsFundPol)
					'UPGRADE_NOTE: Object lclsFundPol may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
					lclsFundPol = Nothing
					.RNext()
				Loop 
				
				.RCloseRec()
			End If
		End With
		
		'UPGRADE_NOTE: Object lrecreaFunds_pol may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaFunds_pol = Nothing
		
		Exit Function
ErrorHandler: 
		'UPGRADE_NOTE: Object lrecreaFunds_pol may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaFunds_pol = Nothing
		Find_Request_VI010 = False
	End Function
	
	'**%Objective: Reads all the active funds associated to a policy
	'%Objetivo: Lee todos los fondos activos asociados a una póliza
	Public Function Find_policy(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal sEffecDate As String) As Boolean
		Dim lrecreaFunds_pol As eRemoteDB.Execute
		
		On Error GoTo Find_Policy_Err
		
		lrecreaFunds_pol = New eRemoteDB.Execute
		
		Find_policy = True
		
		With lrecreaFunds_pol
			.StoredProcedure = "reaCertifunds"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sEffecdate", sEffecDate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 10, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Find_policy = .Run
			If Find_policy Then
				Do While Not .EOF
					Call Add_funds(.FieldToClass("sPortafol"), .FieldToClass("nParticip"), .FieldToClass("sDescript"), .FieldToClass("nIntProy"), .FieldToClass("nIntProyVarCle"))
					.RNext()
				Loop 
				.RCloseRec()
			End If
		End With
		
Find_Policy_Err: 
		If Err.Number Then
			Find_policy = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecreaFunds_pol may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaFunds_pol = Nothing
	End Function
	
	
	'**%Objective: Adds the fields to the collection of nominal values
	'%Objetivo: Agrega los campos a la colección de valores nominales
    Public Function Add_funds(ByVal sPortafol As String, ByVal nParticip As Double, ByVal sDescript As String, ByVal nIntProy As Double, Optional ByVal nIntProyVarCle As Double = 0) As Funds_Pol
        Dim objNewMember As Funds_Pol

        On Error GoTo Add_funds_err

        objNewMember = New Funds_Pol

        If mCol Is Nothing Then
            mCol = New Collection
        End If
        '**+ Establishes the properties that transfers to the method
        '+ Se establecen las propiedades que se transfieren al método
        With objNewMember
            .sPortafol = sPortafol
            .nParticip = nParticip
            .sDescript = sDescript
            .nIntProy = nIntProy
            .nIntProyVarCle = nIntProyVarCle

        End With
        mCol.Add(objNewMember)
        Add_funds = objNewMember

Add_funds_err:
        On Error GoTo 0
        'UPGRADE_NOTE: Object objNewMember may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        objNewMember = Nothing
        '    Set mCol = Nothing
        'UPGRADE_NOTE: Object Add_funds may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        Add_funds = Nothing
    End Function
	
	Public Function Find_Request_VI016(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, ByVal nSwitchOrigin As Integer, ByVal nType As Integer) As Boolean
		Dim lrecreaFunds_pol As eRemoteDB.Execute
		On Error GoTo ErrorHandler
		
		lrecreaFunds_pol = New eRemoteDB.Execute
		
		Find_Request_VI016 = True
		
		With lrecreaFunds_pol
			.StoredProcedure = "reaRequest_VI016"
			
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nSwitchOrigin", nSwitchOrigin, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTyp_profitworker", nType, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			Find_Request_VI016 = .Run
			If Find_Request_VI016 Then
				nBuysTot = .FieldToClass("nBuysTot")
				nSellsTot = .FieldToClass("nSellsTot")
				.RCloseRec()
			End If
		End With
		
		'UPGRADE_NOTE: Object lrecreaFunds_pol may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaFunds_pol = Nothing
		
		Exit Function
ErrorHandler: 
		'UPGRADE_NOTE: Object lrecreaFunds_pol may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaFunds_pol = Nothing
		Find_Request_VI016 = False
	End Function
End Class






