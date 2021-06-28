Option Strict Off
Option Explicit On
Public Class ActiveLife
	'%-------------------------------------------------------%'
	'% $Workfile:: ActiveLife.cls                           $%'
	'% $Author:: Nvaplat41                                  $%'
	'% $Date:: 10/10/03 17.34                               $%'
	'% $Revision:: 13                                       $%'
	'%-------------------------------------------------------%'
	
	'Column_Name                                 Type      Length  Prec  Scale Nullable
	'------------------------- --------------- - -------- ------- ----- ------ --------
	Public nBranch As Integer ' NUMBER        22     5      0 No
	Public nProduct As Integer ' NUMBER        22     5      0 No
	Public nPolicy As Double ' NUMBER        22    10      0 No
	Public nCertif As Double ' NUMBER        22    10      0 No
	Public nOption As Integer ' NUMBER        22     5      0 No
	Public nCapitaldeath As Double ' NUMBER        22    18      6 No
	Public nPremdeal As Double ' NUMBER        22    10      2 No
	Public nAgreement As Integer ' NUMBER        22     5      0 Yes
	Public nIntproject As Double ' NUMBER        22     4      2 No
	Public nTypeinvest As Integer ' NUMBER        22     5      0 No
	Public nWarminint As Double ' NUMBER        22     4      2 No
	Public sStatusva As String ' CHAR           1              Yes
	Public nRole As Integer ' NUMBER        22     5      0 No
	Public sClient As String ' CHAR          14              No
	Public nModulec As Integer ' NUMBER        22              Yes
	Public sCertype As String ' CHAR           1              No
	Public nPayfreq As Integer ' NUMBER        22     5      0 Yes
	
	'-Variables auxiliares
	Public sDescbranch As String ' CHAR          30              Yes
	Public sDescproduct As String ' CHAR          30              Yes
	Public sCliename As String ' CHAR          40              Yes
	Public sDigit As String
	
	'-Tipo registro de la consulta a realizar
	Private Structure udtPolActiveLife
		Dim nBranch As Integer
		Dim nProduct As Integer
		Dim nPolicy As Integer
		Dim nCertif As Integer
		Dim nOption As Integer
		Dim nCapitaldeath As Double
		Dim nPremdeal As Double
		Dim nAgreement As Integer
		Dim nIntproject As Double
		Dim nTypeinvest As Integer
		Dim nWarminint As Double
		'UPGRADE_WARNING: Fixed-length string size must fit in the buffer. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="3C1E4426-0B80-443E-B943-0627CD55D48B"'
		<VBFixedString(1),System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.ByValArray,SizeConst:=1)> Public sStatusva() As Char
		Dim nRole As Integer
		'UPGRADE_WARNING: Fixed-length string size must fit in the buffer. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="3C1E4426-0B80-443E-B943-0627CD55D48B"'
		<VBFixedString(14),System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.ByValArray,SizeConst:=14)> Public sClient() As Char
		Dim nModulec As Integer
		'UPGRADE_WARNING: Fixed-length string size must fit in the buffer. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="3C1E4426-0B80-443E-B943-0627CD55D48B"'
		<VBFixedString(1),System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.ByValArray,SizeConst:=1)> Public sCertype() As Char
		Dim nPayfreq As Integer
		'UPGRADE_WARNING: Fixed-length string size must fit in the buffer. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="3C1E4426-0B80-443E-B943-0627CD55D48B"'
		<VBFixedString(30),System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.ByValArray,SizeConst:=30)> Public sDescbranch() As Char
		'UPGRADE_WARNING: Fixed-length string size must fit in the buffer. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="3C1E4426-0B80-443E-B943-0627CD55D48B"'
		<VBFixedString(30),System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.ByValArray,SizeConst:=30)> Public sDescproduct() As Char
		'UPGRADE_WARNING: Fixed-length string size must fit in the buffer. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="3C1E4426-0B80-443E-B943-0627CD55D48B"'
		<VBFixedString(40),System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.ByValArray,SizeConst:=40)> Public sCliename() As Char
		Dim sDigit As String
	End Structure
	
	Private arrPolActiveLife() As udtPolActiveLife
	
	'%Count: Obtiene la cantidad de registros que hay en el arreglo
	Public ReadOnly Property Count() As Integer
		Get
			Count = UBound(arrPolActiveLife)
		End Get
	End Property
	
	'%Item: Obtiene los datos de la posición del arreglo según el índice dado
	Public Function Item(ByVal nIndex As Object) As Boolean
		If nIndex <= UBound(arrPolActiveLife) Then
			With arrPolActiveLife(nIndex)
				nBranch = .nBranch
				nProduct = .nProduct
				nPolicy = .nPolicy
				nCertif = .nCertif
				nOption = .nOption
				nCapitaldeath = .nCapitaldeath
				nPremdeal = .nPremdeal
				nAgreement = .nAgreement
				nIntproject = .nIntproject
				nTypeinvest = .nTypeinvest
				nWarminint = .nWarminint
				sStatusva = .sStatusva
				nRole = .nRole
				sClient = .sClient
				nModulec = .nModulec
				sCertype = .sCertype
				nPayfreq = .nPayfreq
				sDescbranch = .sDescbranch
				sDescproduct = .sDescproduct
				sCliename = .sCliename
				sDigit = .sDigit
			End With
			Item = True
		End If
	End Function
	
	'%Find_VAC631: Busca los registros según la condición dada
	Public Function Find_VAC631(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal nOption As Integer, ByVal nCapitaldeath As Double, ByVal nPremdeal As Double, ByVal nAgreement As Integer, ByVal nIntproject As Double, ByVal nTypeinvest As Integer, ByVal nWarminint As Double, ByVal sStatusva As String, ByVal nRole As Integer, ByVal sClient As String, ByVal nModulec As Integer, ByVal nPayfreq As Integer) As Boolean
		Dim lrecReaVActiveLife As eRemoteDB.Execute
		Dim lintCount As Integer
		Dim llngMaxRecord As Integer
		
		On Error GoTo Find_VAC631_Err
		
		lrecReaVActiveLife = New eRemoteDB.Execute
		
		'+Definición de parámetros para Stored Procedure 'ReaVActiveLife'
		'+Información leída el 21/01/2002
		With lrecReaVActiveLife
			.StoredProcedure = "ReaVActiveLife"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nOption", nOption, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCapitaldeath", nCapitaldeath, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPremdeal", nPremdeal, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAgreement", nAgreement, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nIntproject", nIntproject, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 4, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTypeinvest", nTypeinvest, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nWarminint", nWarminint, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 4, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sStatusva", sStatusva, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nRole", nRole, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nModulec", nModulec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPayfreq", nPayfreq, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				Find_VAC631 = True
				lintCount = 0
				llngMaxRecord = 50
				ReDim arrPolActiveLife(llngMaxRecord)
				Do While Not .EOF
					If lintCount = llngMaxRecord Then
						llngMaxRecord = 2 * llngMaxRecord
						ReDim Preserve arrPolActiveLife(llngMaxRecord)
					End If
					lintCount = lintCount + 1
					arrPolActiveLife(lintCount).nBranch = .FieldToClass("nBranch")
					arrPolActiveLife(lintCount).nProduct = .FieldToClass("nProduct")
					arrPolActiveLife(lintCount).nPolicy = .FieldToClass("nPolicy")
					arrPolActiveLife(lintCount).nCertif = .FieldToClass("nCertif")
					arrPolActiveLife(lintCount).nOption = .FieldToClass("nOption")
					arrPolActiveLife(lintCount).nCapitaldeath = .FieldToClass("nCapitaldeath")
					arrPolActiveLife(lintCount).nPremdeal = .FieldToClass("nPremdeal")
					arrPolActiveLife(lintCount).nAgreement = .FieldToClass("nAgreement")
					arrPolActiveLife(lintCount).nIntproject = .FieldToClass("nIntproject")
					arrPolActiveLife(lintCount).nTypeinvest = .FieldToClass("nTypeinvest")
					arrPolActiveLife(lintCount).nWarminint = .FieldToClass("nWarminint")
					arrPolActiveLife(lintCount).sStatusva = .FieldToClass("sStatusva")
					arrPolActiveLife(lintCount).nRole = .FieldToClass("nRole")
					arrPolActiveLife(lintCount).sClient = .FieldToClass("sClient")
					arrPolActiveLife(lintCount).nModulec = .FieldToClass("nModulec")
					arrPolActiveLife(lintCount).sCertype = .FieldToClass("sCertype")
					arrPolActiveLife(lintCount).nPayfreq = .FieldToClass("nPayfreq")
					arrPolActiveLife(lintCount).sDescbranch = .FieldToClass("sDescbranch")
					arrPolActiveLife(lintCount).sDescproduct = .FieldToClass("sDescproduct")
					arrPolActiveLife(lintCount).sCliename = .FieldToClass("sCliename")
					arrPolActiveLife(lintCount).sDigit = .FieldToClass("sDigit")
					.RNext()
				Loop 
				.RCloseRec()
				ReDim Preserve arrPolActiveLife(lintCount)
			End If
		End With
Find_VAC631_Err: 
		If Err.Number Then
			Find_VAC631 = False
		End If
		'UPGRADE_NOTE: Object lrecReaVActiveLife may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecReaVActiveLife = Nothing
		On Error GoTo 0
	End Function
	
	'insvalVAC631: Busca los registros según la condición dada
	Public Function insvalVAC631(ByVal sCodispl As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal sClient As String, ByVal sStatusva As String, ByVal nCurrency As Integer, ByVal nModulec As Integer, ByVal nOption As Integer, ByVal nPayfreq As Integer, ByVal nCapitaldeath As Double, ByVal nPremdeal As Double, ByVal nAgreement As Integer, ByVal nTypeinvest As Integer, ByVal nIntproject As Double, ByVal nWarminint As Double) As String
		Dim lclsErrors As eFunctions.Errors
		
		On Error GoTo insvalVAC631_Err
		
		lclsErrors = New eFunctions.Errors
		
		With lclsErrors
			'+ Se valida el campo Ramo
			If nBranch = eRemoteDB.Constants.intNull Or nBranch = 0 Then
				.ErrorMessage(sCodispl, 9064)
			End If
			'+ Se valida el campo Producto
			If nProduct = eRemoteDB.Constants.intNull Or nProduct = 0 Then
				.ErrorMessage(sCodispl, 11009)
			End If
			'+ Debe existir otra condición de busqueda
			If (nPolicy = eRemoteDB.Constants.intNull Or nPolicy = 0) And (nCertif = eRemoteDB.Constants.intNull Or nCertif = 0) And (sClient = String.Empty) And (sStatusva = String.Empty Or sStatusva = "0") And (nCurrency = eRemoteDB.Constants.intNull Or nCurrency = 0) And (nModulec = eRemoteDB.Constants.intNull Or nModulec = 0) And (nOption = eRemoteDB.Constants.intNull Or nOption = 0) And (nPayfreq = eRemoteDB.Constants.intNull Or nPayfreq = 0) And (nCapitaldeath = eRemoteDB.Constants.intNull Or nCapitaldeath = 0) And (nPremdeal = eRemoteDB.Constants.intNull Or nPremdeal = 0) And (nAgreement = eRemoteDB.Constants.intNull Or nAgreement = 0) And (nTypeinvest = eRemoteDB.Constants.intNull Or nTypeinvest = 0) And (nIntproject = eRemoteDB.Constants.intNull Or nIntproject = 0) And (nWarminint = eRemoteDB.Constants.intNull Or nWarminint = 0) Then
				.ErrorMessage(sCodispl, 99022)
			End If
			
			insvalVAC631 = .Confirm
		End With
		
insvalVAC631_Err: 
		If Err.Number Then
			insvalVAC631 = "insvalVAC631: " & Err.Description
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
	End Function
	
	'%Class_Initialize: Se ejecuta cuando se instancia el objeto
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		nBranch = eRemoteDB.Constants.intNull
		nProduct = eRemoteDB.Constants.intNull
		nPolicy = eRemoteDB.Constants.intNull
		nCertif = eRemoteDB.Constants.intNull
		nOption = eRemoteDB.Constants.intNull
		nCapitaldeath = eRemoteDB.Constants.intNull
		nPremdeal = eRemoteDB.Constants.intNull
		nAgreement = eRemoteDB.Constants.intNull
		nIntproject = eRemoteDB.Constants.intNull
		nTypeinvest = eRemoteDB.Constants.intNull
		nWarminint = eRemoteDB.Constants.intNull
		sStatusva = String.Empty
		nRole = eRemoteDB.Constants.intNull
		sClient = String.Empty
		nModulec = eRemoteDB.Constants.intNull
		sCertype = String.Empty
		nPayfreq = eRemoteDB.Constants.intNull
		sDescbranch = String.Empty
		sDescproduct = String.Empty
		sCliename = String.Empty
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
End Class






