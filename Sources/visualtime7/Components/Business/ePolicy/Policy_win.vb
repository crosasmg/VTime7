Option Strict Off
Option Explicit On
Public Class Policy_Win
	'%-------------------------------------------------------%'
	'% $Workfile:: Policy_Win.cls                           $%'
	'% $Author:: Nvaplat22                                  $%'
	'% $Date:: 15/10/04 3:45p                               $%'
	'% $Revision:: 125                                      $%'
	'%-------------------------------------------------------%'
	
	'+ Propiedades de la tabla Policy_Win en el sistema el 06/11/2000
	'+ Los campos llave corresponden a sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate
	
	'+ Column_name              Type                 Computed Length Prec  Scale Nullable TrimTrailingBlanks  FixedLenNullInSource
	'+ ------------------------ -------------------- -------- ------ ----- ----- -------- ------------------  --------------------
	Public sCertype As String 'char     no       1                  no       no                  no
	Public nPolicy As Double 'int      no       4      10    0     no       (n/a)               (n/a)
	Public nCertif As Double 'int      no       4      10    0     no       (n/a)               (n/a)
	Public sV_conpolic As String 'char     no       30                 yes      no                  yes
	Public sV_winpolic As String 'char     no       240                yes      no                  yes
	Public sV_recpolic As String 'char     no       30                 yes      no                  yes
	
	'+ Propiedades de la tabla Sequen_pol en el sistema el 19/01/2000
	'+ Los campos llave corresponden a nBranch, nProduct, sBussityp, nTratypep, sPolitype, sCompon, nSequence, dEffecdate
	
	'+ Column_name              Type                 Computed Length Prec  Scale Nullable TrimTrailingBlanks  FixedLenNullInSource
	'+ ------------------------ -------------------- -------- ------ ----- ----- -------- ------------------  --------------------
	Public nBranch As Integer 'smallint no       2      5     0     no       (n/a)               (n/a)
	Public nProduct As Integer 'smallint no       2      5     0     no       (n/a)               (n/a)
	Public sBussityp As String 'char     no       1                  no       yes                 no
	Public nTratypep As Integer 'Long  no       2      5     0     no       yes                 no
	Public sPolitype As String 'char     no       1                  no       yes                 no
	Public sCompon As String 'char     no       1                  no       yes                 no
	Public Nsequence As Integer 'smallint no       2      5     0     no       (n/a)               (n/a)
	Public dEffecdate As Date 'datetime no       8                  no       (n/a)               (n/a)
	Public sCodispl As String 'char     no       8                  no       yes                 no
	Public dNulldate As Date 'datetime no       8                  yes      (n/a)               (n/a)
	Public sRequire As String 'char     no       1                  yes      yes                 yes
	Public nUsercode As Integer 'smallint no       2      5     0     yes      (n/a)               (n/a)
	
	'+ Propiedades auxiliares
	Public sCodisp As String
	Public sContent As String
	Public sAutomatic As String
	Private mintModules As Integer
	Private mintExist As Integer
	Private sMassiveErrors As String
	
	Public sDescript As String 'char     no       40                 yes      yes                 yes
	Public sShort_des As String 'char     no       12                 yes      yes                 yes
	Public nWindowty As Integer 'smallint no       2      5     0     yes      (n/a)               (n/a)
	
	'- Arreglo que almacena la información de policy_seq
	Private Structure typPolicySeq
		'UPGRADE_WARNING: Fixed-length string size must fit in the buffer. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="3C1E4426-0B80-443E-B943-0627CD55D48B"'
		<VBFixedString(8),System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.ByValArray,SizeConst:=8)> Public sCodisp() As Char
		'UPGRADE_WARNING: Fixed-length string size must fit in the buffer. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="3C1E4426-0B80-443E-B943-0627CD55D48B"'
		<VBFixedString(8),System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.ByValArray,SizeConst:=8)> Public sCodispl() As Char
		'UPGRADE_WARNING: Fixed-length string size must fit in the buffer. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="3C1E4426-0B80-443E-B943-0627CD55D48B"'
		<VBFixedString(40),System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.ByValArray,SizeConst:=40)> Public sDescript() As Char
		'UPGRADE_WARNING: Fixed-length string size must fit in the buffer. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="3C1E4426-0B80-443E-B943-0627CD55D48B"'
		<VBFixedString(1),System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.ByValArray,SizeConst:=1)> Public sRequire() As Char
		Dim sBussityp As String
		Dim nTratypep As Integer
		Dim sPolitype As String
		Dim sCompon As String
		Dim Nsequence As Integer
		Dim dEffecdate As Date
		Dim dNulldate As Date
		Dim sShort_des As String
		Dim nWindowty As Integer
		'UPGRADE_WARNING: Fixed-length string size must fit in the buffer. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="3C1E4426-0B80-443E-B943-0627CD55D48B"'
		<VBFixedString(1),System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.ByValArray,SizeConst:=1)> Public sContent() As Char
		Dim blnMatching As Boolean
		Dim nModules As Integer
		'UPGRADE_WARNING: Fixed-length string size must fit in the buffer. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="3C1E4426-0B80-443E-B943-0627CD55D48B"'
		<VBFixedString(1),System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.ByValArray,SizeConst:=1)> Public sAutomatic() As Char
	End Structure
	
	'- Se definen las constantes para definir el tipo de imagen a mostrar
	Private Const CN_DENIEDREQ As String = "DENIEDREQ"
	Private Const CN_DENIEDOK As String = "DENIEDOK"
	Private Const CN_DENIEDS As String = "DENIEDS"
	Private Const CN_REQUIRED As String = "REQUIRED"
	Private Const CN_OK As String = "OK"
	
	Private ludtPolicySeq() As typPolicySeq
	
	'- Variable que indica si el arreglo contiene información
	Private mblnChargeArr As Boolean
	
	'- Variable que contiene la información para las ventanas a mostrar en la página
	Private mstrHTMLShorDes As String
	Private mstrHTMLImage As eFunctions.Sequence.etypeImageSequence
	
	'- Se define la variable pblnLocalCur para indicar que el producto tiene definido la moneda local
	'- como moneda permitida para la emisión de póliza
	Public pblnLocalCur As Boolean
	
	Public Index As Integer
	Private msecTime As eSecurity.Secur_sche
	Private mstrColinvot As String
	
	Public mstrCodisplReq As String
	
	Private mcolChild As Collection
	
	'% insConcatMessage: Función que devuelve un string, resultado de la concatenación de dos cadenas.
	Public Function insConcatMessage(ByVal lstrString As String, ByVal lintError As Integer, Optional ByVal nCertif As Double = 0, Optional ByVal sCertype As String = "") As String
		Dim lstrStringA As String
		Dim lstrStringB As String
		Dim lstrStringC As String
		Dim lobjGeneral As eGeneral.GeneralFunction
		
		lobjGeneral = New eGeneral.GeneralFunction
		
		lstrStringC = String.Empty
		
		If nCertif <> 0 Then
			If lintError = 3901 Then
				If sCertype = "2" Then
					lstrStringC = " el certificado."
				ElseIf sCertype = "3" Then 
					lstrStringC = " la cotización del certificado."
				ElseIf sCertype = "1" Then 
					lstrStringC = " la solicitud del certificado."
				End If
			End If
		Else
			If lintError = 3901 Then
				If sCertype = "2" Then
					lstrStringC = " la póliza."
				ElseIf sCertype = "3" Then 
					lstrStringC = " la cotización."
				ElseIf sCertype = "1" Then 
					lstrStringC = " la solicitud."
				End If
			End If
		End If
		
		lstrStringA = Trim(lstrString)
		
		'+ Esta función se transladó a eGeneral.GeneralFunction
		lstrStringB = lobjGeneral.insLoadMessage(lintError)
		
		If lstrStringA = String.Empty Then
			If Trim(lstrStringC) = String.Empty Then
				insConcatMessage = "- " & lstrStringB & "."
			Else
				insConcatMessage = "- " & lstrStringB & " " & Trim(lstrStringC)
			End If
		Else
			If Trim(lstrStringC) = String.Empty Then
				insConcatMessage = lstrStringA & Chr(13) & Chr(10) & "- " & lstrStringB & "."
			Else
				insConcatMessage = lstrStringA & Chr(13) & Chr(10) & "- " & lstrStringB & " " & Trim(lstrStringC)
			End If
		End If
		
		'UPGRADE_NOTE: Object lobjGeneral may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lobjGeneral = Nothing
	End Function
	
	'% Find: busca los datos de las transacciones de la secuencia
	Public Function Find(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, Optional ByVal bFind As Boolean = False) As Boolean
		Dim lrecreaPolicy_Win As eRemoteDB.Execute
		
		On Error GoTo Find_Err
		If Me.sCertype <> sCertype Or Me.nBranch <> nBranch Or Me.nProduct <> nProduct Or Me.nPolicy <> nPolicy Or Me.nCertif <> nCertif Or Me.dEffecdate <> dEffecdate Or bFind Then
			
			'+ Definición de parámetros para stored procedure 'ReaPolicy_Win'
			lrecreaPolicy_Win = New eRemoteDB.Execute
			With lrecreaPolicy_Win
				.StoredProcedure = "ReaPolicy_Win"
				.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				If .Run Then
					Find = True
					Me.sCertype = sCertype
					Me.nBranch = nBranch
					Me.nProduct = nProduct
					Me.nPolicy = nPolicy
					Me.nCertif = nCertif
					Me.dEffecdate = dEffecdate
					sV_conpolic = .FieldToClass("sV_conpolic")
					sV_winpolic = .FieldToClass("sV_winpolic")
					.RCloseRec()
				End If
			End With
		Else
			Find = True
		End If
		
Find_Err: 
		If Err.Number Then
			Find = False
		End If
		'UPGRADE_NOTE: Object lrecreaPolicy_Win may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaPolicy_Win = Nothing
		On Error GoTo 0
	End Function
	
	'% Find_Codispl: verifica si el codispl se encuentra dentro de la secuencia de ventanas
	'%               de la póliza
	Public Function Find_Codispl(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, ByVal sCodispl As String) As Boolean
		Dim llngCount As Integer
		Dim lstrV_conpolic As String
		Dim lstrV_winpolic As String
		Dim llngTop As Integer
		Dim lstrAuxCodispl As String
		Dim lblnFound As Boolean
		
		On Error GoTo FindCodispl_err
		
		lblnFound = False
		
		If Find(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate) Then
			lstrV_winpolic = sV_winpolic
			lstrV_conpolic = sV_conpolic
			'+ Se modifica el valor de lstrV_conpolic con el nuevo contenido
			llngTop = Len(Trim(lstrV_conpolic)) - 1
			For llngCount = 0 To llngTop
				lstrAuxCodispl = Trim(Mid(lstrV_winpolic, llngCount * 8 + 1, 8))
				If lstrAuxCodispl = sCodispl Then
					lblnFound = True
					sContent = Mid(lstrV_conpolic, llngCount + 1, 1)
					Exit For
				End If
			Next llngCount
			If lblnFound Then
				Index = llngCount
			End If
			
		End If
		
		Find_Codispl = lblnFound
		
FindCodispl_err: 
		If Err.Number Then
			Find_Codispl = False
		End If
		On Error GoTo 0
	End Function
	
	'%bIsAmendment: Verifica si la transacción es alguna modificación
	Private ReadOnly Property bIsAmendment(ByVal nTransaction As Integer) As Boolean
		Get
			bIsAmendment = nTransaction = Constantes.PolTransac.clngPolicyAmendment Or nTransaction = Constantes.PolTransac.clngTempPolicyAmendment Or nTransaction = Constantes.PolTransac.clngCertifAmendment Or nTransaction = Constantes.PolTransac.clngTempCertifAmendment Or nTransaction = Constantes.PolTransac.clngPolicyQuotAmendent Or nTransaction = Constantes.PolTransac.clngCertifQuotAmendent Or nTransaction = Constantes.PolTransac.clngPolicyPropAmendent Or nTransaction = Constantes.PolTransac.clngCertifPropAmendent Or nTransaction = Constantes.PolTransac.clngQuotAmendConvertion Or nTransaction = Constantes.PolTransac.clngPropAmendConvertion Or nTransaction = Constantes.PolTransac.clngQuotRenewalConvertion Or nTransaction = Constantes.PolTransac.clngPropRenewalConvertion Or nTransaction = Constantes.PolTransac.clngQuotPropAmendentConvertion Or nTransaction = Constantes.PolTransac.clngQuotPropRenewalConvertion
		End Get
	End Property
	'% CountItem: propiedad que indica el número de registros que se encuentran en el arreglo
	Public ReadOnly Property MassiveErrors() As String
		Get
			MassiveErrors = sMassiveErrors
		End Get
	End Property
	
	'% CountItem: propiedad que indica el número de registros que se encuentran en el arreglo
	Public ReadOnly Property CountItem() As Integer
		Get
			If mblnChargeArr Then
				CountItem = UBound(ludtPolicySeq)
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
				If Trim(sCodispl) = Trim(ludtPolicySeq(lintIndex).sCodispl) Then
					ludtPolicySeq(lintIndex).sContent = Value
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
				If Trim(sCodispl) = Trim(ludtPolicySeq(lintIndex).sCodispl) Then
					ludtPolicySeq(lintIndex).sRequire = Value
					Exit For
				End If
			Next lintIndex
		End Set
	End Property
	
	'% Determina si la sequencia de la póliza se encuentra completa.
	Public Function insValSequence(ByVal sTransaction As String, ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, ByVal sTypeCompany As String, Optional ByVal sRequire_Win As String = "", Optional ByVal sBrancht As String = "", Optional ByVal sPolitype As String = "") As Boolean
		'- Indice que recorre los datos de un arreglo
		Dim lintIndex As Integer
		Dim lcolRoleses As Roleses
		Dim lintQRoles As Integer
		Dim lblnIsAmendment As Boolean
		
		'+ Se busca los roles asociados a la poliza
		If (Val(sBrancht) = eProduct.Product.pmBrancht.pmlife Or Val(sBrancht) = eProduct.Product.pmBrancht.pmNotTraditionalLife) And (sPolitype = "1" Or (sPolitype = "2" And nCertif > 0)) Then
			
			lcolRoleses = New Roleses
			If lcolRoleses.Find_Tab_Covrol(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate, 0) Then
				lintQRoles = lcolRoleses.Count
			End If
			'UPGRADE_NOTE: Object lcolRoleses may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
			lcolRoleses = Nothing
		End If
		
		'+ Obtiene todos los datos de cada ventana que pertenece a la sequencia.
		lblnIsAmendment = bIsAmendment(CInt(sTransaction))
		If Find_Sequen_Pol(sTransaction, sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate, sTypeCompany,  ,  ,  ,  ,  , lintQRoles, sRequire_Win) Then
			
			insValSequence = True
			For lintIndex = 0 To CountItem - 1
				If Item(lintIndex) Then
					'+Si la transacción es modificación se indica que la CA017 tiene contenido para
					'+que no se valide
					If sCodispl = "CA017" Then
						If lblnIsAmendment Then
							sContent = "2"
						End If
					End If
					'+ Si la ventana en selección es requerida y no tiene datos la función es interrumpida
					'+ inmediatamente e invalida.
					If (sCodispl = "CA014" And sBrancht <> "1") Or sCodispl <> "CA014" Then
						If sRequire = "1" Then
							If (sContent = "1" Or sContent = "3") Then
								insValSequence = False
								Exit For
							End If
						Else
							If sContent = "3" Then
								insValSequence = False
								Exit For
							End If
						End If
					End If
				End If
			Next lintIndex
		End If
	End Function
	
	
	'% Update: actualiza los datos de las transacciones de la secuencia
	Public Function Update() As Boolean
		Dim lrecupdPolicy_Win As eRemoteDB.Execute
		
		lrecupdPolicy_Win = New eRemoteDB.Execute
		
		On Error GoTo Update_Err
		
		'+ Definición de parámetros para stored procedure 'insudb.updPolicy_Win'
		'+ Información leída el 06/11/2000 03:51:42 p.m.
		
		With lrecupdPolicy_Win
			.StoredProcedure = "updPolicy_Win"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sV_conpolic", sV_conpolic, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 50, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Update = .Run(False)
		End With
		'UPGRADE_NOTE: Object lrecupdPolicy_Win may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecupdPolicy_Win = Nothing
		
Update_Err: 
		If Err.Number Then
			Update = False
		End If
		On Error GoTo 0
	End Function
	
	'% UpdatePolicy_Win: actualiza los datos de las transacciones de la secuencia
	Public Function UpdatePolicy_Win() As Boolean
		Dim lrecInsPolicy_Win As eRemoteDB.Execute
		
		On Error GoTo UpdatePolicy_Win_Err
		'+ Definición de parámetros para stored procedure 'InsPolicy_Win'
		lrecInsPolicy_Win = New eRemoteDB.Execute
		With lrecInsPolicy_Win
			.StoredProcedure = "InsPolicy_Win"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sV_conpolic", sV_conpolic, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 60, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sV_winpolic", sV_winpolic, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 480, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			UpdatePolicy_Win = .Run(False)
		End With
		
UpdatePolicy_Win_Err: 
		If Err.Number Then
			UpdatePolicy_Win = False
		End If
		'UPGRADE_NOTE: Object lrecInsPolicy_Win may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecInsPolicy_Win = Nothing
		On Error GoTo 0
	End Function
	
	'%GetQChildren: Obtiene la cantidad de hijos de una carpeta
	Private Function GetQChildren(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, ByVal sCodispl As String, ByVal IsLife As Boolean) As Boolean
		Dim lcolRoles As Roleses
		Dim lclsRoles As Roles
		
		On Error GoTo GetQChildren_Err
		'UPGRADE_NOTE: Object mcolChild may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		mcolChild = Nothing
		mcolChild = New Collection
		GetQChildren = True
		If sCodispl = "CA014" Then
			If IsLife Then
				lcolRoles = New Roleses
				If lcolRoles.Find_Tab_Covrol(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate, 0) Then
					
					For	Each lclsRoles In lcolRoles
						mcolChild.Add(sCodispl & lclsRoles.nCoverPos, sCodispl & lclsRoles.nCoverPos)
					Next lclsRoles
				End If
				'UPGRADE_NOTE: Object lcolRoles may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
				lcolRoles = Nothing
				'UPGRADE_NOTE: Object lclsRoles may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
				lclsRoles = Nothing
			Else
				mcolChild.Add(sCodispl, sCodispl)
			End If
		Else
			mcolChild.Add(sCodispl, sCodispl)
		End If
		
GetQChildren_Err: 
		If Err.Number Then
			GetQChildren = False
		End If
	End Function
	
	'%Metodo que se encarga de grabar las vemtanas con contenido en Policy_Win
	Public Function Add_PolicyWin(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, ByVal nUsercode As Integer, ByVal sCodispl As String, ByVal sContent As String, Optional ByVal bNotLoadTab As Boolean = True, Optional ByVal bIndex As Boolean = False, Optional ByVal bIsLife As Boolean = False, Optional ByVal bAddWindows As Boolean = True) As Boolean
		Dim lrecupdPolicywin As eRemoteDB.Execute
		Dim llngCount As Integer
        Dim lstrV_conpolic As String
        Dim lstrV_winpolic As String
		Dim lstrContentAnt As String
		
		Dim llngIndex As Integer
		
		On Error GoTo Add_PolicyWin_Err
		
		Add_PolicyWin = True
		
		sCodispl = Trim(sCodispl)
		'+ los posibles valores de sContent son:
		'+      1 -  Sin Contenido
		'+      2 -  Con Contenido
		'+      3 -  Sin Contenido y Requerida para la poliza/certificado
		'+      4 -  Sin Contenido y No requerida para la poliza/certificado
		'+      5 -  Con Contenido y Requerida para la poliza/certificado
		'+      6 -  Con Contenido y No requerida para la poliza/certificado
		If bNotLoadTab Then
			'+ Definición de store procedure ReaDisc_xprem_count al 07-04-2002 17:51:02
			lrecupdPolicywin = New eRemoteDB.Execute
			With lrecupdPolicywin
				.StoredProcedure = "InsAddPolicy_Win"
				.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("sCodispl", sCodispl, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 8, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nUserCode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("sContent", sContent, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nIsLife", IIf(bIsLife, 1, 2), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nAddWindows", IIf(bAddWindows, 1, 2), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nCommit", 1, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				If .Run(False) Then
					mstrContent = .Parameters("sContent").Value
				End If
			End With
		Else
            lstrV_conpolic = String.Empty
            lstrV_winpolic = String.Empty
			
			If Find_Item(sCodispl, True) Then
				Refresh_Content = sContent
			End If
			
			For llngCount = 0 To CountItem
				Call Item(llngCount)
                lstrV_conpolic = Mid(lstrV_conpolic, 1, llngCount) & Me.sContent
                lstrV_winpolic = Mid(lstrV_winpolic, 1, llngCount * 8) & Me.sCodispl.PadRight(8, " ")
			Next llngCount
			
            If Trim(lstrV_winpolic) <> String.Empty And Trim(lstrV_conpolic) <> String.Empty Then
                '+ Se actualiza Policy_Win
                With Me
                    .sCertype = sCertype
                    .nBranch = nBranch
                    .nProduct = nProduct
                    .nPolicy = nPolicy
                    .nCertif = nCertif
                    .dEffecdate = dEffecdate
                    .sV_conpolic = lstrV_conpolic
                    .sV_winpolic = lstrV_winpolic
                    .nUsercode = nUsercode
                    Add_PolicyWin = UpdatePolicy_Win()
                End With
            End If
		End If
		
Add_PolicyWin_Err: 
		If Err.Number Then
			Add_PolicyWin = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecupdPolicywin may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecupdPolicywin = Nothing
	End Function
	
	'% LoadTabs: carga la secuencia de ventanas para el módulo de Póliza
	Public Function LoadTabs(ByVal nTransaction As Integer, ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, ByVal nUsercode As Integer, ByVal sPoltype As String, ByVal sBussityp As String, ByVal sTypeCompany As String, Optional ByVal sOpener As String = "", Optional ByVal sBrancht As String = "", Optional ByVal sSche_Code As String = "", Optional ByVal nType_amend As Integer = eRemoteDB.Constants.intNull) As String

        Dim lclsProduct As eProduct.Product = New eProduct.Product
        Dim lclsSequence As eFunctions.Sequence
        Dim lcolRoleses As Roleses = New Roleses
        Dim lintCountWindows As Integer
		Dim lstrCompon As String
		Dim lintAction As Integer
		Dim lstrHTMLCode As String
		Dim lintIndexCover As Integer
		Dim lblnFoundRoles As Boolean
		Dim lintQRoles As Integer
		Dim lblnLife As Boolean
        Dim lstrTratypep As String = ""
        Dim lintCountItem As Short
		Dim lblnContinue As Boolean
		
		On Error GoTo LoadTabs_err
		
		
		'+ Se asigna la accion a tomar
		Select Case nTransaction
			Case Constantes.PolTransac.clngPolicyQuery, Constantes.PolTransac.clngCertifQuery, Constantes.PolTransac.clngQuotationQuery, Constantes.PolTransac.clngProposalQuery, Constantes.PolTransac.clngQuotAmendentQuery, Constantes.PolTransac.clngPropAmendentQuery, Constantes.PolTransac.clngQuotRenewalQuery, Constantes.PolTransac.clngPropRenewalQuery
				lintAction = eFunctions.Menues.TypeActions.clngActionQuery
			Case Else
				lintAction = eFunctions.Menues.TypeActions.clngActionInput
		End Select
		
		If sBrancht = String.Empty Then
			lclsProduct = New eProduct.Product
			Call lclsProduct.Find(nBranch, nProduct, dEffecdate)
			sBrancht = CStr(lclsProduct.sBrancht)
		End If
		
		'+Se inicializa la variable que corresponde a la moneda local
		pblnLocalCur = False
		
		'+ Se realiza la selección de la transacción
		'+ Cambio segun Hoja 421
		Select Case nTransaction
            Case CDec("1"), CDec("2"), CDec("3"), CDec("18"), CDec("19"), 45
                lstrTratypep = "1"
			Case CDec("12"), CDec("13"), CDec("14"), CDec("15")
				lstrTratypep = "2"
			Case CDec("8"), CDec("9"), CDec("10"), CDec("11"), CDec("44")
				lstrTratypep = "3"
				'+Declaraciones
			Case CDec("21")
				lstrTratypep = "4"
			Case CDec("4"), CDec("5"), CDec("24"), CDec("25"), CDec("39"), CDec("28"), CDec("29"), CDec("41")
				lstrTratypep = "6"
			Case CDec("6"), CDec("7"), CDec("26"), CDec("27"), CDec("40"), CDec("30"), CDec("31"), CDec("42"), CDec("34"), CDec("43"), CDec("23")
				lstrTratypep = "7"
		End Select
		
		'+ Se asigna valor a la variable que contiene el componente de la transacción
		'+ (1. Póliza - 2. Certificado)
		
		If sPoltype = "1" Then
			lstrCompon = "1"
		Else
			If nTransaction = CDbl("21") And sBrancht = "8" Then
				lstrCompon = "1"
			Else
				Select Case nTransaction
					Case CDec("1"), CDec("4"), CDec("6"), CDec("8"), CDec("12"), CDec("13"), CDec("16"), CDec("17"), CDec("18")
						lstrCompon = "1"
					Case Else
						If (nTransaction = Constantes.PolTransac.clngRecuperation Or nTransaction = Constantes.PolTransac.clngPropQuotConvertion) And nCertif = 0 Then
							lstrCompon = "1"
						Else
							If nCertif = 0 Then
								lstrCompon = "1"
							Else
								lstrCompon = "2"
							End If
						End If
				End Select
			End If
		End If
		
		'+ Se busca los roles asociados a la poliza
		If (sBrancht = CStr(eProduct.Product.pmBrancht.pmlife) Or sBrancht = CStr(eProduct.Product.pmBrancht.pmNotTraditionalLife)) And (sPoltype = "1" Or (sPoltype = "2" And nCertif > 0)) Then
			
			lblnLife = True
			
			lcolRoleses = New Roleses
			lblnFoundRoles = lcolRoleses.Find_Tab_Covrol(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate, 0)
			lintQRoles = lcolRoleses.Count
		End If
		
		lclsSequence = New eFunctions.Sequence
		lstrHTMLCode = lclsSequence.makeTable("DMECAR", "Pólizas")
		
		'+ Se buscan las transacciones asociadas a la póliza
		If Find_Sequen_Pol(CStr(nTransaction), sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate, sTypeCompany, String.Empty, sBussityp, CInt(lstrTratypep), sPoltype, lstrCompon, lintQRoles, String.Empty, nUsercode, nType_amend) Then
			lintCountItem = CountItem
			Do While lintCountWindows <= lintCountItem
				'+ Se asignan los valores a las variables públicas
				Call Item(lintCountWindows)
				
				'+ Se filtra la creación de la ventana, dependiendo de la transacción
				If Not ((nTransaction = Constantes.PolTransac.clngPolicyQuotAmendent Or nTransaction = Constantes.PolTransac.clngCertifQuotAmendent Or nTransaction = Constantes.PolTransac.clngPolicyPropAmendent Or nTransaction = Constantes.PolTransac.clngCertifPropAmendent Or nTransaction = Constantes.PolTransac.clngPropAmendConvertion Or nTransaction = Constantes.PolTransac.clngProprehabilitate) And (sCodispl = "CA041")) And Not ((nTransaction = Constantes.PolTransac.clngPolicyProposal Or nTransaction = Constantes.PolTransac.clngCertifProposal Or nTransaction = Constantes.PolTransac.clngPolicyQuotation Or nTransaction = Constantes.PolTransac.clngCertifQuotation Or nTransaction = Constantes.PolTransac.clngPolicyQuotRenewal Or nTransaction = Constantes.PolTransac.clngCertifQuotRenewal Or nTransaction = Constantes.PolTransac.clngPolicyPropRenewal Or nTransaction = 23 Or nTransaction = Constantes.PolTransac.clngCertifPropRenewal) And (sCodispl = "CA028A" Or sCodispl = "CA027A" Or sCodispl = "CA017B")) Then
					'+ Se crea la pestaña
					If Create_tab_Policy(lintCountWindows, nBranch, nProduct, nPolicy, CInt(lstrTratypep), sCertype, nCertif, dEffecdate, nUsercode, CStr(nTransaction), sSche_Code, lblnLife) Then
						'+ Si se trata de una consulta y tiene contenido o es cualquier transacción diferente a consultar
						'+ Falta validar las demas consultas 10,11,31,32,33,34 segu hoja 421
						If (lstrTratypep = "3" And (sContent = "2" Or sContent = "5" Or sContent = "6")) Or (nTransaction = CDbl("10") And (sContent = "2" Or sContent = "5" Or sContent = "6")) Or (nTransaction = CDbl("11") And (sContent = "2" Or sContent = "5" Or sContent = "6")) Or (nTransaction = CDbl("39") And (sContent = "2" Or sContent = "5" Or sContent = "6")) Or (nTransaction = CDbl("40") And (sContent = "2" Or sContent = "5" Or sContent = "6")) Or (nTransaction = CDbl("41") And (sContent = "2" Or sContent = "5" Or sContent = "6")) Or (nTransaction = CDbl("42") And (sContent = "2" Or sContent = "5" Or sContent = "6")) Or (nTransaction = CDbl("44") And (sContent = "2" Or sContent = "5" Or sContent = "6")) Or (lstrTratypep <> "3") Or (lblnLife And sCodispl = "CA014") Then
							
							lblnContinue = True
							
							If sCodispl = "CA017" And lstrTratypep = "7" Then
								Select Case nTransaction
									Case CDec("26"), CDec("27"), CDec("30"), CDec("31"), CDec("34"), CDec("40"), CDec("42")
										lblnContinue = False
								End Select
							End If
							
							If lblnContinue Then
								
								If lblnLife And InStr(1, sCodispl, "CA014") > 0 Then
									
									If sCodispl = "CA014" Then
										lstrHTMLCode = lstrHTMLCode & lclsSequence.makeRow(sCodisp, sCodispl, lintAction, mstrHTMLShorDes, mstrHTMLImage,  , True,  , lintQRoles > 0,  ,  , sDescript, mintModules, nWindowty)
									Else
										If lblnFoundRoles Then
											lintIndexCover = lintIndexCover + 1
											
											lstrHTMLCode = lstrHTMLCode & lclsSequence.makeRow(sCodisp, "CA014", lintAction, lcolRoleses.Item(lintIndexCover).sDescRole & "-" & lcolRoleses.Item(lintIndexCover).sClient, mstrHTMLImage, "CA014",  , "&nRole=" & lcolRoleses.Item(lintIndexCover).nRole & "&sClient=" & lcolRoleses.Item(lintIndexCover).sClient & "&nIndexCover=" & lcolRoleses.Item(lintIndexCover).nCoverPos,  , sRequire, lcolRoleses.Item(lintIndexCover).nCoverPos, sDescript, mintModules, nWindowty)
										End If
									End If
								Else
									If Not lblnLife Or (lblnLife And InStr(1, sCodispl, "CA014") = 0) Then
										lstrHTMLCode = lstrHTMLCode & lclsSequence.makeRow(sCodisp, sCodispl, lintAction, mstrHTMLShorDes, mstrHTMLImage,  ,  ,  ,  , sRequire,  , sDescript, mintModules, nWindowty)
									End If
								End If
							End If
						End If
						'+ Se mueve al siguiente registro encontrado
					End If
				End If
				lintCountWindows = lintCountWindows + 1
			Loop 
		Else
			'+Si la acción se trata de una declaración
			If lstrTratypep = "4" Then
				If Sequen_Declaration(lclsProduct.sWin_declar, nUsercode, sSche_Code) Then
					lstrHTMLCode = lstrHTMLCode & lclsSequence.makeRow(sCodisp, sCodispl, lintAction, mstrHTMLShorDes, eFunctions.Sequence.etypeImageSequence.eRequired,  ,  ,  ,  , sRequire,  , sDescript, mintModules)
				End If
			End If
		End If
		
		'+Se cambia el tipo de transacción, para que se realize una emisión de certificado,
		'+si el proceso a tratar es declaración
		
		nTransaction = IIf(nTransaction = 21, 2, nTransaction)
		
		'+En el caso de que se este realizando una emisión de pólizas o reemision de la misma,
		'+se realiza el llamado a la funcion  insPolicy
		If sPoltype = "3" And nCertif = 0 And (nTransaction = Constantes.PolTransac.clngPolicyIssue Or nTransaction = Constantes.PolTransac.clngRecuperation Or nTransaction = Constantes.PolTransac.clngPolicyReissue) Then
			Call FindTabIndex(String.Empty, sPoltype, sCertype, nBranch, nProduct, nPolicy)
		End If
		
		'       nTransaction <> "3" And _
		'
		If (mintExist = 2 Or (sOpener = "CA025" And lblnLife)) And lintAction <> eFunctions.Menues.TypeActions.clngActionQuery Then
			Call Add_PolicyWin(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate, nUsercode, "CA001_K", "1", False)
		End If
		
		lstrHTMLCode = lstrHTMLCode & lclsSequence.closeTable
		LoadTabs = lstrHTMLCode
		
LoadTabs_err: 
		If Err.Number Then
			LoadTabs = "LoadTabs: " & Err.Description
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lclsProduct may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsProduct = Nothing
		'UPGRADE_NOTE: Object lcolRoleses may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lcolRoleses = Nothing
		'UPGRADE_NOTE: Object lclsSequence may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsSequence = Nothing
		
	End Function
	
	'% Find_Sequen_Pol: realiza la lectura de la secuencia de Póliza
	'% 11/01/2001 - Se anexó el parámetro opcional sCodispl que permite actualizar de manera inmediata
	'% las variables públicas con los datos de una ventana particular. En caso de no definir ningún Codispl
	'% la función se comporta como lo haría normalmente.
	Public Function Find_Sequen_Pol(ByVal sTransaction As String, ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, ByVal sTypeCompany As String, Optional ByVal sCodispl As String = "", Optional ByVal sBussityp As String = "", Optional ByVal nTratypep As Integer = 0, Optional ByVal sPolitype As String = "", Optional ByVal sCompon As String = "", Optional ByVal nQRoles As Integer = 0, Optional ByVal sRequire_Win As String = "", Optional ByVal nUsercode As Integer = eRemoteDB.Constants.intNull, Optional ByVal nType_amend As Integer = eRemoteDB.Constants.intNull) As Boolean
		'+ En caso de definir un codispl, lblnFind indica si el item ha sido ubicado o no
		Dim lblnFind As Boolean
		Dim lintCount As Integer
		Dim lintIndex As Integer
		
		'+ En caso de deifnir un codispl, se almacena el indice que corresponde al item de interés.
		Dim lintIndexRequired As Integer
		Dim lrecreaTab_Winpol As eRemoteDB.Execute
		Dim lclsPolicy As ePolicy.Policy
		Dim lclsCertificat As ePolicy.Certificat
		
		On Error GoTo Find_Sequen_Pol_Err
		If sBussityp = String.Empty Or nTratypep = 0 Or sPolitype = String.Empty Or sCompon = String.Empty Then
			lclsPolicy = New ePolicy.Policy
			With lclsPolicy
				If .Find(sCertype, nBranch, nProduct, nPolicy) Then
					sBussityp = .sBussityp
					sPolitype = .sPolitype
					mstrColinvot = .sColinvot
					
					If sPolitype = "1" Then
						sCompon = "1"
					Else
						If sTransaction = "21" Then
							sCompon = "1"
						Else
							Select Case sTransaction
								Case "1", "4", "6", "8", "12", "13", "16", "17", "18"
									sCompon = "1"
								Case Else
									If (sTransaction = CStr(Constantes.PolTransac.clngRecuperation) Or sTransaction = CStr(Constantes.PolTransac.clngPropQuotConvertion)) And nCertif = 0 Then
										sCompon = "1"
									Else
										sCompon = "2"
									End If
							End Select
						End If
					End If
					
					'+ Se realiza la selección de la transacción
					Select Case sTransaction
						Case "1", "2", "3", "18", "19", "16", "17"
							nTratypep = CInt("1")
						Case "12", "13", "14", "15", "33", "34", "35", "36"
							nTratypep = CInt("2")
						Case "8", "9"
							nTratypep = CInt("3")
							'+ Declaraciones
						Case "21"
							nTratypep = CInt("4")
						Case "4", "5", "10", "24", "25", "39", "28", "29", "41"
							nTratypep = CInt("6")
						Case "6", "7", "11", "26", "27", "40", "30", "31", "42", "23"
							nTratypep = CInt("7")
					End Select
				End If
			End With
			'UPGRADE_NOTE: Object lclsPolicy may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
			lclsPolicy = Nothing
		End If
		
		'+ Definición de parámetros para stored procedure 'insudb.reaTab_WinPOL'
		'+ Información leída el 28/02/2000 01:47:14 PM
		lrecreaTab_Winpol = New eRemoteDB.Execute
		With lrecreaTab_Winpol
			.StoredProcedure = "reawin_sequen_Pol"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sBussityp", sBussityp, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTratypep", nTratypep, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sPolitype", sPolitype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sCompon", sCompon, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sTypeCompany", sTypeCompany, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nQRoles", nQRoles, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nType_amend", nType_amend, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run() Then
				mintExist = .FieldToClass("nExist")
				If CountItem <> -1 Then
					ReDim Preserve ludtPolicySeq(60)
				Else
					ReDim ludtPolicySeq(60)
					For lintIndex = 0 To 60
						ludtPolicySeq(lintIndex).sCodispl = String.Empty
					Next lintIndex
				End If
				
				lintCount = 0
				
				lblnFind = False
				
				'+ Se carga el arreglo con los datos de la tabla
				Do While Not .EOF
					ludtPolicySeq(lintCount).sCodisp = .FieldToClass("sCodisp")
					ludtPolicySeq(lintCount).sCodispl = .FieldToClass("sCodispl")
					ludtPolicySeq(lintCount).sDescript = .FieldToClass("sDescript")
					If Trim(.FieldToClass("sCodisp")) = "CA021" Then
						If sRequire_Win = "1" Then
							ludtPolicySeq(lintCount).sRequire = sRequire_Win
							ludtPolicySeq(lintCount).sContent = sRequire_Win
						Else
							ludtPolicySeq(lintCount).sRequire = .FieldToClass("sRequired")
							ludtPolicySeq(lintCount).sContent = .FieldToClass("sContent")
						End If
					Else
						ludtPolicySeq(lintCount).sRequire = .FieldToClass("sRequired")
						ludtPolicySeq(lintCount).sContent = .FieldToClass("sContent")
					End If
					
					ludtPolicySeq(lintCount).sShort_des = .FieldToClass("sShortDes")
					ludtPolicySeq(lintCount).nWindowty = .FieldToClass("nWindowTy")
					
					If Trim(ludtPolicySeq(lintCount).sCodispl) = "CA003" Then
						If (nTratypep <> 3 And (sTransaction <> "10" And sTransaction <> "11" And sTransaction <> "39" And sTransaction <> "40" And sTransaction <> "41" And sTransaction <> "42")) Then
							
							lclsCertificat = New ePolicy.Certificat
							Call lclsCertificat.Find(sCertype, nBranch, nProduct, nPolicy, nCertif)
							If (lclsCertificat.nWay_pay = Constantes.eWayPay.clngPayByPAC Or lclsCertificat.nWay_pay = Constantes.eWayPay.clngPayByTransBank) Then
								If lclsCertificat.sDirind = "1" Then
									ludtPolicySeq(lintCount).sRequire = "0"
								ElseIf lclsCertificat.sDirind = "2" Then 
									ludtPolicySeq(lintCount).sRequire = "1"
								End If
							Else
								ludtPolicySeq(lintCount).sRequire = "0"
							End If
							'UPGRADE_NOTE: Object lclsCertificat may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
							lclsCertificat = Nothing
						End If
					End If
					ludtPolicySeq(lintCount).nModules = .FieldToClass("nModules")
					
					If Not Trim(sCodispl) = String.Empty And Trim(ludtPolicySeq(lintCount).sCodispl) = sCodispl And Not lblnFind Then
						lblnFind = True
						lintIndexRequired = lintCount
					End If
					.RNext()
					lintCount = lintCount + 1
				Loop 
				.RCloseRec()
				ReDim Preserve ludtPolicySeq(lintCount - 1)
				mblnChargeArr = True
				Find_Sequen_Pol = True
				
				'+ En caso de definir un codispl se ubica se actualizan las variables públicas de la clase
				'+ con el indice obtenido.
				If Not Trim(sCodispl) = String.Empty Then
					If Item(lintIndexRequired) Then
					End If
				End If
			End If
		End With
		
Find_Sequen_Pol_Err: 
		If Err.Number Then
			Find_Sequen_Pol = False
		End If
		'UPGRADE_NOTE: Object lrecreaTab_Winpol may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaTab_Winpol = Nothing
		On Error GoTo 0
	End Function
	
	'% Item: Función que carga la información del arreglo en la clase dada una posición
	Public Function Item(ByVal lintIndex As Integer) As Boolean
		'+ Si el arreglo de la clase contiene informacion se carga el combo
		If mblnChargeArr Then
			If lintIndex <= UBound(ludtPolicySeq) Then
				With ludtPolicySeq(lintIndex)
					sCodisp = Trim(.sCodisp)
					sCodispl = Trim(.sCodispl)
					sDescript = Trim(.sDescript)
					sRequire = Trim(.sRequire)
					sShort_des = Trim(.sShort_des)
					nWindowty = .nWindowty
					sContent = Trim(.sContent)
					mintModules = .nModules
					sAutomatic = .sAutomatic
				End With
				Item = True
			End If
		End If
	End Function
	
	'% Item: Función que carga la información del arreglo en la clase dado un codispl particular
	Public Function Find_ItemCodispl(ByVal lstrCodispl As String) As Boolean
		Dim lintIndex As Integer
		
		Find_ItemCodispl = False
		
		If Not Trim(lstrCodispl) = String.Empty Then
			'+ Si el arreglo de la clase contiene informacion se carga el combo
			If mblnChargeArr Then
				For lintIndex = 0 To UBound(ludtPolicySeq)
					With ludtPolicySeq(lintIndex)
						If Item(lintIndex) Then
							If .sCodispl = lstrCodispl Then
								Find_ItemCodispl = True
								Exit For
							End If
						End If
					End With
				Next lintIndex
			End If
		End If
	End Function
	
	
	'% Funcion que se encarga del manejo de los tab de las ventanas
	Private Function Create_tab_Policy(ByVal ncount As Integer, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nTratypep As Integer, ByVal sCertype As String, ByVal nCertif As Double, ByVal dEffecdate As Date, ByVal nUsercode As Integer, ByVal sTransaction As String, ByVal sSche_Code As String, Optional ByVal bLife As Boolean = False) As Boolean
		Create_tab_Policy = True
		If (nTratypep <> 3 And (sTransaction <> "10" And sTransaction <> "11" And sTransaction <> "39" And sTransaction <> "40" And sTransaction <> "41" And sTransaction <> "42")) Then
			
			'+ Asocia la descripción de la pestaña
			Call Create_Tab()
			
			'+ Asocia la imagen de la pestaña
			If mintExist = 1 Then
				Call Load_Ca041_Cont(nBranch, nProduct, dEffecdate)
			Else
				'+Si no se encontró la información en la tabla 'Policy_Win'
				Call Load_ca041_NotCont(nBranch, nProduct, nPolicy, nCertif, sCertype, dEffecdate, nUsercode)
			End If
			Call SecurityFrame_Class(sCodispl, nUsercode, sSche_Code)
			
		Else
			Create_tab_Policy = False
			'+ Si se trata de consulta de pólizas, se asocia la descripción y la imagen de la pestaña
			'+ (Sólo si tiene contenido la ventana)
			If (sContent = "2" Or sContent = "5" Or sContent = "6") Or (sCodispl = "CA014" And bLife) Then
				If Create_Tab_Query(sCodispl, sCertype) Then
					Create_tab_Policy = True
					Call SecurityFrame_Class(sCodispl, nUsercode, sSche_Code)
				End If
			End If
		End If
	End Function
	
	'% Create_Tab: se asocia la descripción de la ventana en la secuencia
	Private Sub Create_Tab()
		mstrHTMLShorDes = sShort_des
	End Sub
	
	'%Create_tab_Query: Crea las pestañas para una consulta
	Private Function Create_Tab_Query(ByVal lstrCodispl As String, ByVal lstrCertype As String) As Boolean
		lstrCodispl = Trim(lstrCodispl)
		If (lstrCodispl <> "CA047" Or (lstrCodispl = "CA047" And lstrCertype <> "2")) And lstrCodispl <> "CA017" Then
			If Find_Item(lstrCodispl) Then
				mstrHTMLShorDes = Trim(sShort_des)
				mstrHTMLImage = IIf((sContent = "2" Or sContent = "5" Or sContent = "6"), eFunctions.Sequence.etypeImageSequence.eOK, eFunctions.Sequence.etypeImageSequence.eEmpty)
				Create_Tab_Query = True
			End If
		End If
	End Function
	
	'% se encarga del manejo de las imagenes de los tab para la ventana de monedas cuando existe informacion en Policy_Win
	Private Function Load_Ca041_Cont(ByVal llngBranch As Integer, ByVal llngProduct As Integer, ByVal ldtmEffecdate As Date) As Boolean
		Dim lclsCurren_pol As Curren_pol
        Dim lstrRequired As String = ""
        Dim lblnChecked As Boolean
		
		'+Si se trata de la ventana de Selección de Monedas
		If Trim(sCodispl) = "CA041" Then
			'+Si la ventana no tiene contenido y no es requerida
			If sContent <> "2" And sRequire <> "1" Then
				lclsCurren_pol = New Curren_pol
				'+Si la moneda local no está definida en el producto, se pone la ventana requerida automáticamente
				If Not lclsCurren_pol.valCurLocal(llngBranch, llngProduct, ldtmEffecdate) Then
					mstrHTMLImage = lclsCurren_pol.mstrImage
				End If
				'UPGRADE_NOTE: Object lclsCurren_pol may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
				lclsCurren_pol = Nothing
			Else
				If sContent = "2" Then
					mstrHTMLImage = eFunctions.Sequence.etypeImageSequence.eOK
				Else
					If sRequire = "1" Then
						mstrHTMLImage = eFunctions.Sequence.etypeImageSequence.eRequired
					Else
						mstrHTMLImage = eFunctions.Sequence.etypeImageSequence.eEmpty
					End If
				End If
			End If
		Else
			
			'+ Los posibles valores de sContent son:
			'+      1 -  Sin Contenido
			'+      2 -  Con Contenido
			'+      3 -  Sin Contenido y Requerida para la poliza/certificado
			'+      4 -  Sin Contenido y No requerida para la poliza/certificado
			'+      5 -  Con Contenido y Requerida para la poliza/certificado
			'+      6 -  Con Contenido y No requerida para la poliza/certificado
			Select Case sContent
				Case "1"
					lstrRequired = sRequire
					lblnChecked = False
					
				Case "2"
					lstrRequired = sRequire
					lblnChecked = True
					
				Case "3"
					lstrRequired = "1"
					lblnChecked = False
					
				Case "4"
					lstrRequired = "2"
					lblnChecked = False
					
				Case "5"
					lstrRequired = "1"
					lblnChecked = True
					
				Case "6"
					lstrRequired = "2"
					lblnChecked = True
			End Select
			
			If lblnChecked Then
				mstrHTMLImage = eFunctions.Sequence.etypeImageSequence.eOK
			Else
				If lstrRequired = "1" Then
					mstrHTMLImage = eFunctions.Sequence.etypeImageSequence.eRequired
				Else
					mstrHTMLImage = eFunctions.Sequence.etypeImageSequence.eEmpty
				End If
			End If
		End If
	End Function
	
	'%se encarga del manejo de las imagenes de los tab para la ventana de monedas y la ca022 y no existe informacion en Policy_Win
	Private Function Load_ca041_NotCont(ByVal llngBranch As Integer, ByVal llngProduct As Integer, ByVal llngPolicy As Double, ByVal llngCertif As Double, ByVal lstrCertype As String, ByVal ldtmEffecdate As Date, ByVal lintUser As Integer) As Boolean
		Dim lblnAutoFill As Boolean
		Dim lclsCurren_pol As Curren_pol
		Dim lclsPolicy As Policy
		
		'+Si se trata de la ventana de Selección de Monedas
		If Trim(sCodispl) = "CA041" Then
			lclsCurren_pol = New ePolicy.Curren_pol
			
			'+ Se llena automáticamente la información de la ventana
			With lclsCurren_pol
				.nUsercode = lintUser
				If llngCertif <> 0 Then
					If .Find(llngPolicy, llngBranch, llngProduct, lstrCertype, 0, ldtmEffecdate) Then
						.Val_Curren_pol(0)
						.nCertif = llngCertif
						If .Add Then
							mstrHTMLImage = .mstrImage
							lblnAutoFill = .mblnAutoFill
						End If
					Else
						If .valInitCurren(llngBranch, llngProduct, llngPolicy, llngCertif, lstrCertype, ldtmEffecdate) Then
							mstrHTMLImage = .mstrImage
							lblnAutoFill = .mblnAutoFill
						Else
							'+ Si la moneda local no está definida en el producto, se pone la ventana requerida automáticamente
							If .valCurLocal(llngBranch, llngProduct, ldtmEffecdate) Then
								.IsLocal = True
								pblnLocalCur = True
							End If
							mstrHTMLImage = .mstrImage
							lblnAutoFill = .mblnAutoFill
						End If
						
					End If
				Else
					If .valInitCurren(llngBranch, llngProduct, llngPolicy, llngCertif, lstrCertype, ldtmEffecdate) Then
						mstrHTMLImage = .mstrImage
						lblnAutoFill = .mblnAutoFill
					Else
						'+ Si la moneda local no está definida en el producto, se pone la ventana requerida automáticamente
						If .valCurLocal(llngBranch, llngProduct, ldtmEffecdate) Then
							.IsLocal = True
							pblnLocalCur = True
						End If
						mstrHTMLImage = .mstrImage
						lblnAutoFill = .mblnAutoFill
					End If
				End If
			End With
			
			'+ Si se trata de la ventana de cláusulas
		ElseIf Trim(sCodispl) = "CA022" Then 
			If Add_AutoCa0022 Then
				mstrHTMLImage = eFunctions.Sequence.etypeImageSequence.eOK
				lblnAutoFill = True
			ElseIf sRequire = "1" Then 
				mstrHTMLImage = eFunctions.Sequence.etypeImageSequence.eRequired
			Else
				mstrHTMLImage = eFunctions.Sequence.etypeImageSequence.eEmpty
			End If
			'+ Si se trata de la ventana Asegurados por coberturas (pólizas innominadas)
		ElseIf Trim(sCodispl) = "VI811" Then 
			lclsPolicy = New ePolicy.Policy
			With lclsPolicy
				If .Find(lstrCertype, llngBranch, llngProduct, llngPolicy) Then
					If .sNopayroll = "1" Then
						mstrHTMLImage = eFunctions.Sequence.etypeImageSequence.eRequired
						Refresh_Require = "1"
						Refresh_Content = "3"
					Else
						mstrHTMLImage = eFunctions.Sequence.etypeImageSequence.eEmpty
					End If
				End If
			End With
		ElseIf sRequire = "1" Then 
			mstrHTMLImage = eFunctions.Sequence.etypeImageSequence.eRequired
		Else
			mstrHTMLImage = eFunctions.Sequence.etypeImageSequence.eEmpty
		End If
		If Trim(sCodispl) <> "VI811" Then
			Refresh_Content = IIf(lblnAutoFill, "2", "1")
		End If
		
		'UPGRADE_NOTE: Object lclsCurren_pol may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsCurren_pol = Nothing
		'UPGRADE_NOTE: Object lclsPolicy may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsPolicy = Nothing
	End Function
	
	'% Add_AutoCa0022: Graba las cláusulas preseleccionadas en la póliza
	Public Function Add_AutoCa0022() As Boolean
		Dim lrecInsAutoCA022 As eRemoteDB.Execute
		
		lrecInsAutoCA022 = New eRemoteDB.Execute
		
		'Definición de parámetros para stored procedure 'insudb.InsAutoCA022'
		'Información leída el 30/06/1999 01:51:23 PM
		
		With lrecInsAutoCA022
			.StoredProcedure = "InsAutoCA022"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecDate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUserCode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nResult", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				'+ Si se grabaron valores por defecto la función devuelve true
				Add_AutoCa0022 = CBool(.Parameters("nResult").Value)
			End If
		End With
		
		'UPGRADE_NOTE: Object lrecInsAutoCA022 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecInsAutoCA022 = Nothing
	End Function
	
	'%SecurityFrame: valida que el frame sea valido para el esquema/usuario
	Private Function SecurityFrame_Class(ByVal sCodispl As String, ByVal nUsercode As Integer, ByVal sSche_Code As String) As String
		On Error GoTo SecurityFrame_Class_Err
		
		sCodispl = Trim(sCodispl)
		'If msecTime Is Nothing Then
		'   Set msecTime = New eSecurity.Secur_sche
		'End If
		'With msecTime
		'    If .Reload(Levels, sSche_Code) Then
		'        If .ItemLevels(sSche_Code, Window, sCodispl) Then
		'            If Find_Item(Trim$(sCodispl), True) Then
		'                If Trim$(sRequire) = "1" Then
		'                     mstrHTMLImage = etypeImageSequence.eDeniedReq
		'                Else
		'                    If Trim(sContent) = "2" Then
		'                         mstrHTMLImage = etypeImageSequence.eDeniedOK
		'                    Else
		'                         mstrHTMLImage = etypeImageSequence.eDeniedS
		'                    End If
		'                End If
		'            End If
		'        End If
		'    End If
		'End With
		
SecurityFrame_Class_Err: 
		If Err.Number Then
			SecurityFrame_Class = "SecurityFrame_Class: " & Err.Description
		End If
		On Error GoTo 0
	End Function
	
	'%Find_Item: Busca la poscision del arreglo dado un codispl
	Public Function Find_Item(ByVal sCodispl As String, Optional ByVal bLoad As Boolean = False) As Boolean
		Dim llngIndex As Integer
		Dim llngTop As Integer
		llngTop = CountItem
		For llngIndex = 0 To llngTop
			If Trim(ludtPolicySeq(llngIndex).sCodispl) = Trim(sCodispl) Then
				If bLoad Then
					Call Item(llngIndex)
				End If
				Find_Item = True
				Exit For
			End If
		Next llngIndex
	End Function
	
	'% Sequen_Declaration: Funcion que carga la secuencia de ventanas correspondientes a una declaracion
	Private Function Sequen_Declaration(ByVal sWin_declar As String, ByVal nUsercode As Integer, ByVal sSche_Code As String) As Boolean
        Dim lstrCodispl As String = ""
        Dim lrecReaWindows As eRemoteDB.Execute
		
		On Error GoTo Sequen_Declaration_Err
		lrecReaWindows = New eRemoteDB.Execute
		ReDim ludtPolicySeq(5)
		
		'+Si la póliza es declarativa y no tiene secuencia asociada, se busca en los datos
		'+del producto, la ventana asociada para las declaraciones
		'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
		If Not IsDbNull(sWin_declar) And Trim(sWin_declar) <> String.Empty Then
			
			'+ Definición de parámetros para stored procedure 'insudb.reaWindows'
			With lrecReaWindows
				.StoredProcedure = "reaWindows"
				.Parameters.Add("sCodispl", sWin_declar)
				If .Run Then
					
					lstrCodispl = sWin_declar & New String(" ", 8 - Len(sWin_declar))
					
					mstrHTMLShorDes = Trim(.FieldToClass("sShort_des"))
					mstrHTMLImage = eFunctions.Sequence.etypeImageSequence.eRequired
					sCodisp = Trim(.FieldToClass("sCodisp"))
					sCodispl = Trim(lstrCodispl)
					
					Sequen_Declaration = True
					
					'+Se llena la variable que contienen las descripciones de los frames
					ludtPolicySeq(0).sCodispl = Trim(lstrCodispl)
					ludtPolicySeq(0).sDescript = Trim(.FieldToClass("sDescript"))
					ludtPolicySeq(0).sRequire = "1"
					
					.RCloseRec()
					
				End If
			End With
			'UPGRADE_NOTE: Object lrecReaWindows may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
			lrecReaWindows = Nothing
		End If
		
		Call SecurityFrame_Class(lstrCodispl, nUsercode, sSche_Code)
		
Sequen_Declaration_Err: 
		If Err.Number Then
			Sequen_Declaration = False
		End If
		On Error GoTo 0
	End Function
	
	'% FindTabIndex: Esta rutina se encarga de buscar el índice de Tabfolder que corresponda
	'%               con una transacción específica.
	Public Function FindTabIndex(Optional ByVal lstrCodispl As String = "", Optional ByVal lstrPolitype As String = "", Optional ByVal sCertype As String = "", Optional ByVal nBranch As Integer = 0, Optional ByVal nProduct As Integer = 0, Optional ByVal nPolicy As Double = 0) As Integer
		Dim llngIndex As Integer
		Dim lblnContinue As Boolean
		Dim llbnIndic2 As Boolean
		Dim llbnIndic1 As Boolean
		
		On Error GoTo FindTabIndex_Err
		
		Dim lclsPolicy As ePolicy.Policy
		lclsPolicy = New ePolicy.Policy
		
		llngIndex = 1
		lblnContinue = True
		lstrCodispl = Trim(lstrCodispl)
		Do While llngIndex <= CountItem And lblnContinue
			Call Item(llngIndex)
			If lstrCodispl <> String.Empty Then
				If lstrCodispl = sCodispl Then
					FindTabIndex = llngIndex
					lblnContinue = False
				End If
			Else
				If sCodispl = "CA006" Then llbnIndic1 = True
				If sCodispl = "CA017" Then llbnIndic2 = True
			End If
			llngIndex = llngIndex + 1
		Loop 
		
		If lblnContinue Then
			FindTabIndex = 0
		End If
		
		If lstrCodispl = String.Empty Then
			If Not llbnIndic1 And llbnIndic2 And lstrPolitype = "3" Then
				'+El tipo de facturacion se coloca "Por Póliza" Para la pólizas declarativas de transporte,
				'+Siempre y cuando el tipo de poliza sea multilocalidad, y la ventana de datos del colectivo
				'+no este presente en la secuencia
				With lclsPolicy
					If .Find(sCertype, nBranch, nProduct, nPolicy) Then
						.sColinvot = "1"
						.Add()
					End If
				End With
			End If
		End If
		
		'UPGRADE_NOTE: Object lclsPolicy may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsPolicy = Nothing
		
FindTabIndex_Err: 
		If Err.Number Then
			FindTabIndex = 0
		End If
		On Error GoTo 0
	End Function
	
	'UPGRADE_NOTE: Class_Terminate was upgraded to Class_Terminate_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Terminate_Renamed()
		'UPGRADE_NOTE: Object msecTime may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		msecTime = Nothing
	End Sub
	Protected Overrides Sub Finalize()
		Class_Terminate_Renamed()
		MyBase.Finalize()
	End Sub
	
	'% getCodisplArr: Devuelve un string con todas las ventanas de una póliza a tratar
	Public Function getCodisplArr(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date) As String
		Dim llngCount As Integer
		Dim lstrV_conpolic As String
		Dim lstrV_winpolic As String
		Dim llngTop As Integer
        Dim lstrAuxCodispl As String = ""
        Dim lblnFound As Boolean
		
		On Error GoTo FindCodispl_err
		
		lblnFound = False
		
		If Find(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate) Then
			lstrV_winpolic = sV_winpolic
			lstrV_conpolic = sV_conpolic
			'+ Se modifica el valor de lstrV_conpolic con el nuevo contenido
			llngTop = Len(Trim(lstrV_conpolic)) - 1
			For llngCount = 0 To llngTop
				If llngCount = 0 Then
					lstrAuxCodispl = "|"
				End If
				
				lstrAuxCodispl = lstrAuxCodispl & Trim(Mid(lstrV_winpolic, llngCount * 8 + 1, 8)) & "|"
			Next llngCount
		End If
		
		getCodisplArr = lstrAuxCodispl
		
FindCodispl_err: 
		If Err.Number Then
			getCodisplArr = String.Empty
		End If
		On Error GoTo 0
	End Function
	
	'% getRemoveCodispl: Remueve un string (sCodispl) de una cadena (contenido de ventana a procesar)
	Public Function getRemoveCodispl(ByVal sString As String, ByVal sStringDel As String, Optional ByVal sDelimit As String = "") As String
		Dim llngPos As Integer
        Dim lblnDelimit As Boolean
        getRemoveCodispl = String.Empty

		If sString <> String.Empty Then
			'+ Se verifica si la cadena posee delimitador (es el mismo que el pasado como parámetro).
			'+ Aquí el delimitador es considerado al principio y al final de cada elemento.
			lblnDelimit = (Mid(sString, 1, 1) = sDelimit)
			
			If lblnDelimit Then
				sStringDel = "|" & sStringDel & "|"
				llngPos = InStr(1, sString, sStringDel)
			Else
				llngPos = InStr(1, sString, sStringDel)
			End If
			
			'+ Si se consiguió el patron
			If llngPos > 0 Then
				'+ Si es el primer elemento
				If llngPos = 1 Then
					getRemoveCodispl = Mid(sString, llngPos + Len(sStringDel), Len(sString))
					If getRemoveCodispl <> String.Empty Then
						If lblnDelimit Then
							getRemoveCodispl = "|" & getRemoveCodispl
						End If
					End If
				Else
					getRemoveCodispl = Mid(sString, 1, llngPos) & Mid(sString, llngPos + Len(sStringDel), Len(sString))
					
				End If
			End If
		End If
	End Function
	
	'% getCodisplNotContent: Obtiene el código de un string cuyo formado es: |1:CA022|
	Public Function getCodisplNotContent(ByVal sString As String, ByVal sDelimit As String) As String
		Dim llngPos As Integer
        getCodisplNotContent = String.Empty
		If sString <> String.Empty Then
			llngPos = InStr(1, sString, sDelimit)
			
			If llngPos Then
				getCodisplNotContent = Mid(sString, llngPos + 1, Len(sString) - 1)
			Else
				getCodisplNotContent = sString
			End If
		End If
	End Function
	
	'%Add_PolicyWinArr: Metodo que se encarga de grabar varias ventanas con contenido en Policy_Win según formato de cadena pasado como parámetro "|1:CA025|2:CA004|"
	Public Function Add_PolicyWinArr(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, ByVal nUsercode As Integer, ByVal sCodisplAll As String, Optional ByVal bNotLoadTab As Boolean = True) As Boolean
		Dim lclsSetting As eFunctions.Values
		Dim larrCodispl() As String
		Dim lvntCodispl As Object
        Dim lstrCodispl As String = String.Empty
        Dim lstrContent As String = String.Empty
		Dim llngPos As Integer
		
		Add_PolicyWinArr = True
		
		'+ Si existen ventanas para procesar
		If sCodisplAll <> String.Empty Then
			
			larrCodispl = Microsoft.VisualBasic.Split(Mid(sCodisplAll, 2, Len(sCodisplAll) - 2), "|")
			
			'+ Se tratan cada una de las ventanas
			For	Each lvntCodispl In larrCodispl
				If lvntCodispl <> String.Empty Then
					llngPos = InStr(1, lvntCodispl, ":")
					
					If llngPos Then
						lstrCodispl = Mid(lvntCodispl, llngPos + 1, Len(lvntCodispl) - 1)
						lstrContent = Mid(lvntCodispl, 1, 1)
					Else
						lstrCodispl = lvntCodispl
						lstrContent = "1"
					End If
				End If
				If lstrCodispl <> String.Empty Then
					Call Add_PolicyWin(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate, nUsercode, lstrCodispl, lstrContent)
				End If
			Next lvntCodispl
		End If
		
Add_PolicyWinArr_Err: 
		Dim strFileName As String
		Dim lblnDebug As Boolean
		Dim lngFile As Integer
		If Err.Number Then
			
			
			lclsSetting = New eFunctions.Values
			
			lblnDebug = (lclsSetting.insGetSetting("Active", "2", "Debug") = "1")
			strFileName = lclsSetting.insGetSetting("LogFile", "D:\PerformanceDebug", "Debug")
			'UPGRADE_NOTE: Object lclsSetting may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
			lclsSetting = Nothing
			
			
			If lblnDebug Then
				strFileName = strFileName & "Method"
				
				strFileName = strFileName & nUsercode & ".log"
				
				lngFile = FreeFile
				FileOpen(lngFile, strFileName, OpenMode.Append)
				PrintLine(lngFile, Err.Source & "." & "Add_PolicyWinArr -> Error[" & Err.Number & "]: """ & Err.Description & """")
				FileClose(lngFile)
				
			End If
			Add_PolicyWinArr = False
		End If
		On Error GoTo 0
	End Function
	
	'%InsupdRequiredWin: Actualiza el registro correpondiente en policywin para colocar el
	'                    estado correcto en la tabla
	Public Function InsupdRequiredWin(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, ByVal sCodispl As String, ByVal nUsercode As Integer, ByVal sConten As String) As Boolean
		Dim lrecupdPolicywin As eRemoteDB.Execute
		
		On Error GoTo InsupdRequiredWin_Err
		'+ Definición de store procedure ReaDisc_xprem_count al 07-04-2002 17:51:02
		lrecupdPolicywin = New eRemoteDB.Execute
		With lrecupdPolicywin
			.StoredProcedure = "InsupdPolicy_Win"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sCodispl", sCodispl, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 8, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUserCode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sConten", sConten, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCommit", 1, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			InsupdRequiredWin = .Run(False)
		End With
		
InsupdRequiredWin_Err: 
		If Err.Number Then
			InsupdRequiredWin = False
		End If
		'UPGRADE_NOTE: Object lrecupdPolicywin may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecupdPolicywin = Nothing
		On Error GoTo 0
	End Function
	
	'%InsGetAutomaticWindows: Obtiene las ventanas que se llenan automáticamente
	Public Function InsGetAutomaticWindows(ByVal sCodispl As String, ByVal nTransaction As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal dEffecdate As Date, ByVal sPolitype As String, ByVal sBussityp As String) As Boolean
		Dim lrecReaSequen_pol_Automatic As eRemoteDB.Execute
		Dim lintCount As Short
		Dim lstrCompon As String
		Dim llngTratypep As Integer
		Dim lintTop As Short
		
		On Error GoTo ReaSequen_pol_Automatic_Err
		lintTop = 50
		If sPolitype = "1" Then
			lstrCompon = "1"
		Else
			If CDbl(nTransaction) = 21 Then
				lstrCompon = "1"
			Else
				Select Case nTransaction
					Case CStr(1), CStr(4), CStr(6), CStr(8), CStr(12), CStr(13), CStr(16), CStr(17), CStr(18)
						lstrCompon = "1"
					Case Else
						If (nTransaction = CStr(Constantes.PolTransac.clngRecuperation) Or nTransaction = CStr(Constantes.PolTransac.clngPropQuotConvertion)) And nCertif = 0 Then
							lstrCompon = "1"
						Else
							lstrCompon = "2"
						End If
				End Select
			End If
		End If
		
		'+ Se realiza la selección de la transacción
		Select Case nTransaction
			Case CStr(1), CStr(2), CStr(3), CStr(18), CStr(19)
				llngTratypep = 1
			Case CStr(12), CStr(13), CStr(14), CStr(15), CStr(33), CStr(34), CStr(35), CStr(36)
				llngTratypep = 2
			Case CStr(8), CStr(9)
				llngTratypep = 3
				'+ Declaraciones
			Case CStr(21)
				llngTratypep = 4
			Case CStr(4), CStr(5), CStr(10), CStr(24), CStr(25), CStr(39), CStr(28), CStr(29), CStr(41)
				llngTratypep = 6
			Case CStr(6), CStr(7), CStr(11), CStr(26), CStr(27), CStr(40), CStr(30), CStr(31), CStr(42)
				llngTratypep = 7
		End Select
		
		'+Definición de parámetros para stored procedure 'ReaSequen_pol_Automatic'
		'+Información leída el 17/04/2003
		lrecReaSequen_pol_Automatic = New eRemoteDB.Execute
		With lrecReaSequen_pol_Automatic
			.StoredProcedure = "ReaSequen_pol_Automatic"
			.Parameters.Add("sCodispl", sCodispl, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 8, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTratypep", llngTratypep, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sBussityp", sBussityp, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sPolitype", sPolitype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sCompon", lstrCompon, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			ReDim ludtPolicySeq(lintTop)
			If .Run Then
				lintCount = 0
				Do While Not .EOF
					ludtPolicySeq(lintCount).sCodispl = .FieldToClass("sCodispl")
					ludtPolicySeq(lintCount).sAutomatic = .FieldToClass("sAutomatic")
					ludtPolicySeq(lintCount).sRequire = .FieldToClass("sRequire")
					.RNext()
					lintCount = lintCount + 1
					If lintCount > lintTop Then
						lintTop = lintTop + 10
						ReDim Preserve ludtPolicySeq(lintTop)
					End If
				Loop 
				.RCloseRec()
				ReDim Preserve ludtPolicySeq(lintCount - 1)
				mblnChargeArr = True
				InsGetAutomaticWindows = True
			End If
		End With
		
ReaSequen_pol_Automatic_Err: 
		If Err.Number Then
			InsGetAutomaticWindows = False
		End If
		'UPGRADE_NOTE: Object lrecReaSequen_pol_Automatic may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecReaSequen_pol_Automatic = Nothing
		On Error GoTo 0
	End Function
End Class






