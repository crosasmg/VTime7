Option Strict Off
Option Explicit On
Public Class VPolicyQuerys
	Implements System.Collections.IEnumerable
	'%-------------------------------------------------------%'
	'% $Workfile:: VPolicyQuerys.cls                        $%'
	'% $Author:: Nvaplat41                                  $%'
	'% $Date:: 9/10/03 19.02                                $%'
	'% $Revision:: 5                                        $%'
	'%-------------------------------------------------------%'
	
	Private mCol As Collection
	
	'+ Variables utilizadas para realizar la búsqueda de los datos.  Se manejan las acciones
	'+ "Anterior" y "Próximo"
	Public nCompany_First As Integer
	Public nBranch_First As Integer
	Public nProduct_First As Integer
	Public nPolicy_First As Double
	Public nCertif_First As Double
	Public sCompany As String
	Public sBranch As String
	Public sProduct As String
	Public sPolicy As String
	Public sCertif As String
	Public nElement As Integer

	'% Find: se buscan los datos de la cotización/propuesta/póliza
	Private Function Find(ByVal nCompany As Integer, ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal sClientC As String, ByVal sClientA As String, ByVal dStartdate As Date, ByVal sStatus_pol As String, ByVal dExpirdat As Date, ByVal sPolitype As String, ByVal sInitials As String, ByVal sAccesswo As String, ByVal nCompany_First As Integer, ByVal nBranch_First As Integer, ByVal nProduct_First As Integer, ByVal nPolicy_First As Double, ByVal nCertif_First As Double, ByVal sCompany As String, ByVal sBranch As String, ByVal sProduct As String, ByVal sPolicy As String, ByVal sCertif As String, ByVal nElement As Integer) As Boolean
		Dim lclsRemote As eRemoteDB.Execute
		Dim lclsVPolicyQuery As VPolicyQuery
		
		On Error GoTo Find_Err
		
		lclsRemote = New eRemoteDB.Execute
		
		With lclsRemote
			.StoredProcedure = "reaPossiblePolicy"
			.Parameters.Add("nCompany", IIf(nCompany = 0, eRemoteDB.Constants.intNull, nCompany), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sClientC", sClientC, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sClientA", sClientA, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dStartdate", dStartdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sStatus_pol", sStatus_pol, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dExpirdat", dExpirdat, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sPolitype", sPolitype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCompany_First", nCompany_First, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch_First", nBranch_First, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct_First", nProduct_First, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy_First", nPolicy_First, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif_First", nCertif_First, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sCompany", sCompany, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 4000, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sBranch", sBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 4000, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sProduct", sProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 4000, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sPolicy", sPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 4000, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sCertif", sCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 4000, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nElement", nElement, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sInitials", sInitials, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sAccesswo", sAccesswo, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run() Then
				Do While Not .EOF
					lclsVPolicyQuery = New VPolicyQuery
					lclsVPolicyQuery.nCompany = .FieldToClass("nCompany")
					lclsVPolicyQuery.nBranch = .FieldToClass("nBranch")
					lclsVPolicyQuery.nProduct = .FieldToClass("nProduct")
					lclsVPolicyQuery.nPolicy = .FieldToClass("nPolicy")
					lclsVPolicyQuery.nCertif = .FieldToClass("nCertif")
					lclsVPolicyQuery.sStatusva = .FieldToClass("sStatusva")
					lclsVPolicyQuery.dStartdate = .FieldToClass("dStartdate")
					lclsVPolicyQuery.dExpirdat = .FieldToClass("dExpirdat")
					lclsVPolicyQuery.nDigit = .FieldToClass("nDigit")
					If lclsVPolicyQuery.nDigit = eRemoteDB.Constants.intNull Then
						lclsVPolicyQuery.nDigit = 0
					End If
					lclsVPolicyQuery.sPolitype = .FieldToClass("sPolitype")
					lclsVPolicyQuery.sClientC = .FieldToClass("sClientC")
					lclsVPolicyQuery.sClientA = .FieldToClass("sClientA")
					Call Add(lclsVPolicyQuery)
					.RNext()
				Loop 
				Find = True
			End If
		End With
		
Find_Err: 
		If Err.Number Then
			Find = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lclsRemote may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsRemote = Nothing
		'UPGRADE_NOTE: Object lclsVPolicyQuery may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsVPolicyQuery = Nothing
	End Function
	
	'% Find_by_role: se buscan los datos de las pólizas asociadas a un asegurado
	Public Function Find_by_role(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal sClient As String, ByVal dEffecdate As Date) As Boolean
		Dim lclsRemote As eRemoteDB.Execute
		Dim lclsVPolicyQuery As VPolicyQuery
		
		On Error GoTo Find_Err
		
		lclsRemote = New eRemoteDB.Execute
		
		With lclsRemote
			.StoredProcedure = "reaPolicyByRole"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run() Then
				Do While Not .EOF
					lclsVPolicyQuery = New VPolicyQuery
					lclsVPolicyQuery.nBranch = .FieldToClass("nBranch")
					lclsVPolicyQuery.nProduct = .FieldToClass("nProduct")
					lclsVPolicyQuery.nPolicy = .FieldToClass("nPolicy")
					lclsVPolicyQuery.nCertif = .FieldToClass("nCertif")
					lclsVPolicyQuery.dDate_Origi = .FieldToClass("dDate_origi")
					lclsVPolicyQuery.dExpirdat = .FieldToClass("dExpirdat")
					lclsVPolicyQuery.sClient = .FieldToClass("sClient")
					Call Add(lclsVPolicyQuery)
					.RNext()
				Loop 
				Find_by_role = True
			End If
		End With
		
Find_Err: 
		If Err.Number Then
			Find_by_role = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lclsRemote may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsRemote = Nothing
		'UPGRADE_NOTE: Object lclsVPolicyQuery may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsVPolicyQuery = Nothing
	End Function

		'% Find_by_role: se buscan los datos de las pólizas asociadas a un asegurado
	Public Function Find_by_regist(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal sRegist As String, ByVal sDigit As String, ByVal dEffecdate As Date) As Boolean
		Dim lclsRemote As eRemoteDB.Execute
		Dim lclsVPolicyQuery As VPolicyQuery
		
		On Error GoTo Find_Err
		
		lclsRemote = New eRemoteDB.Execute
		
		With lclsRemote
			.StoredProcedure = "reaPolicyByRegist"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sRegist", sRegist, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 10, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sDigit", sDigit, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run() Then
				Do While Not .EOF
					lclsVPolicyQuery = New VPolicyQuery
					lclsVPolicyQuery.nBranch = .FieldToClass("nBranch")
					lclsVPolicyQuery.nProduct = .FieldToClass("nProduct")
					lclsVPolicyQuery.nPolicy = .FieldToClass("nPolicy")
					lclsVPolicyQuery.nCertif = .FieldToClass("nCertif")
					lclsVPolicyQuery.dDate_Origi = .FieldToClass("dDate_origi")
					lclsVPolicyQuery.dExpirdat = .FieldToClass("dExpirdat")
					lclsVPolicyQuery.sClient = .FieldToClass("sClient")
                    lclsVPolicyQuery.sClientA = .FieldToClass("sRut")
                    lclsVPolicyQuery.sRegist = .FieldToClass("sRegist")
                    lclsVPolicyQuery.sAutoDigit = .FieldToClass("sDigit")

                    Add(lclsVPolicyQuery)
					.RNext()
				Loop 
				Find_by_regist = True
			End If
		End With
		
Find_Err: 
		If Err.Number Then
			Find_by_regist = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lclsRemote may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsRemote = Nothing
		'UPGRADE_NOTE: Object lclsVPolicyQuery may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsVPolicyQuery = Nothing
	End Function

	
	'% inspreGE010: se preparan los datos para la búsqueda de pólizas
	Public Function inspreGE010(ByVal nCompany As Integer, ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal sClientC As String, ByVal sClientA As String, ByVal dStartdate As Date, ByVal sStatus_pol As String, ByVal dExpirdat As Date, ByVal sPolitype As String, ByVal sInitials As String, ByVal sAccesswo As String, ByVal nCompany_First As Integer, ByVal nBranch_First As Integer, ByVal nProduct_First As Integer, ByVal nPolicy_First As Double, ByVal nCertif_First As Double, ByVal sCompany As String, ByVal sBranch As String, ByVal sProduct As String, ByVal sPolicy As String, ByVal sCertif As String, ByVal nElement As Integer, ByVal sDirection As String) As Boolean
		Dim lintColCount As Short
		
		On Error GoTo inspreGE010_err
		
		Me.sCompany = sCompany
		Me.sBranch = sBranch
		Me.sProduct = sProduct
		Me.sPolicy = sPolicy
		Me.sCertif = sCertif
		Me.nElement = IIf(nElement = eRemoteDB.Constants.intNull, 0, nElement)
		
		If sDirection = "Back" Then
			'+ Si se necesitan leer los registros anteriores
			nCompany_First = eRemoteDB.Constants.intNull
			nBranch_First = eRemoteDB.Constants.intNull
			nProduct_First = eRemoteDB.Constants.intNull
			nPolicy_First = eRemoteDB.Constants.intNull
			nCertif_First = eRemoteDB.Constants.intNull
		End If
		
		sStatus_pol = IIf(sStatus_pol = "0", String.Empty, sStatus_pol)
		
		'+ Se realiza la lectura de los datos con los parámetros de búsqueda
		If Find(nCompany, sCertype, nBranch, nProduct, nPolicy, nCertif, sClientC, sClientA, dStartdate, sStatus_pol, dExpirdat, sPolitype, sInitials, sAccesswo, nCompany_First, nBranch_First, nProduct_First, nPolicy_First, nCertif_First, sCompany, sBranch, sProduct, sPolicy, sCertif, nElement) Then
			lintColCount = mCol.Count()
			With mCol.Item(lintColCount)
				Me.nCompany_First = .nCompany
				Me.nBranch_First = .nBranch
				Me.nProduct_First = .nProduct
				Me.nPolicy_First = .nPolicy
				Me.nCertif_First = .nCertif
			End With
			
			If sDirection = "Next" Or sDirection = String.Empty Then
				With mCol.Item(1)
					Me.sCompany = IIf(Me.sCompany = String.Empty, .nCompany, Me.sCompany & ", " & .nCompany)
					Me.sBranch = IIf(Me.sBranch = String.Empty, .nBranch, Me.sBranch & ", " & .nBranch)
					Me.sProduct = IIf(Me.sProduct = String.Empty, .nProduct, Me.sProduct & ", " & .nProduct)
					Me.sPolicy = IIf(Me.sPolicy = String.Empty, .nPolicy, Me.sPolicy & ", " & .nPolicy)
					Me.sCertif = IIf(Me.sCertif = String.Empty, .nCertif, Me.sCertif & ", " & .nCertif)
					Me.nElement = Me.nElement + 1
				End With
			Else
				With Me
					.sCompany = Mid(Me.sCompany, 1, InStrRev(Me.sCompany, ",", Len(Me.sCompany)) - 1)
					.sBranch = Mid(Me.sBranch, 1, InStrRev(Me.sBranch, ",", Len(Me.sBranch)) - 1)
					.sProduct = Mid(Me.sProduct, 1, InStrRev(Me.sProduct, ",", Len(Me.sProduct)) - 1)
					.sPolicy = Mid(Me.sPolicy, 1, InStrRev(Me.sPolicy, ",", Len(Me.sPolicy)) - 1)
					.sCertif = Mid(Me.sCertif, 1, InStrRev(Me.sCertif, ",", Len(Me.sCertif)) - 1)
					.nElement = Me.nElement - 1
				End With
			End If
			inspreGE010 = True
		End If
		
inspreGE010_err: 
		If Err.Number Then
			inspreGE010 = False
		End If
		On Error GoTo 0
	End Function
	
	'% Add: se agrega un elemento a la colección
	Private Function Add(ByRef lobjVPolicyQuery As VPolicyQuery) As VPolicyQuery
		mCol.Add(lobjVPolicyQuery)
		Add = lobjVPolicyQuery
		'UPGRADE_NOTE: Object lobjVPolicyQuery may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lobjVPolicyQuery = Nothing
	End Function
	
	'* Item: toma un elemento de la colección
	Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As VPolicyQuery
		Get
			Item = mCol.Item(vntIndexKey)
		End Get
	End Property
	
	'* Count: Indica el número de elementos de la colección
	Public ReadOnly Property Count() As Integer
		Get
			Count = mCol.Count()
		End Get
	End Property
	
	'* NewEnum: enumera los elementos de la colección
	'UPGRADE_NOTE: NewEnum property was commented out. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="B3FC1610-34F3-43F5-86B7-16C984F0E88E"'
	'Public ReadOnly Property NewEnum() As stdole.IUnknown
		'Get
			'NewEnum = mCol._NewEnum
		'End Get
	'End Property
	
	Public Function GetEnumerator() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
		'UPGRADE_TODO: Uncomment and change the following line to return the collection enumerator. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="95F9AAD0-1319-4921-95F0-B9D3C4FF7F1C"'
		GetEnumerator = mCol.GetEnumerator
	End Function
	
	'* Remove: elimina un elemento de la colección
	Public Sub Remove(ByRef vntIndexKey As Object)
		mCol.Remove(vntIndexKey)
	End Sub
	
	'* Class_Initialize: controla la apertura de la colección
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		mCol = New Collection
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
	
	'* Class_Terminate: controla el fin de la colección
	'UPGRADE_NOTE: Class_Terminate was upgraded to Class_Terminate_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Terminate_Renamed()
		'UPGRADE_NOTE: Object mCol may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		mCol = Nothing
	End Sub
	Protected Overrides Sub Finalize()
		Class_Terminate_Renamed()
		MyBase.Finalize()
	End Sub
End Class






