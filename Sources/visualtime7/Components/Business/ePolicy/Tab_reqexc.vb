Option Strict Off
Option Explicit On
Public Class Tab_reqexc
	'-Esta clase es para hacer las validaciones de requesitos y exclusiones
	'%-------------------------------------------------------%'
	'% $Workfile:: Tab_reqexc.cls                           $%'
	'% $Author:: Nvaplat41                                  $%'
	'% $Date:: 9/10/03 19.02                                $%'
	'% $Revision:: 14                                       $%'
	'%-------------------------------------------------------%'
	
	'-Variable para verificar que alguno de los elementos está, cuando existe una relación del
	'-tipo "Alguno"
	Private bIsInSomeone As Boolean
	Private mclsTables As eFunctions.Tables
	Private mcolTab_reqexc As eProduct.Tab_reqexcs
	Private mstrElements As String
	Private mstrRoles As String
	
	
	'%InsValTab_Reqexc: Valida los requisitos y exclusiones en las ventanas de cartera
	Public Function InsValTab_Reqexc(ByVal sCodispl As String, ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, ByVal sType1 As eProduct.Tab_reqexc.ReqexclType, ByVal sCodes As String, ByVal sSel1 As String, ByRef lclsErrors As eFunctions.Errors, Optional ByVal sBrancht As String = "", Optional ByVal sRoles As String = "") As Boolean
		Dim lintTo As Integer
		Dim lintIndex As Integer
		Dim lstrCodes As String
		Dim lstrRoles As String
		Dim lstrReaTCover As String
		
		mcolTab_reqexc = New eProduct.Tab_reqexcs
		InsValTab_Reqexc = True
		'-Se obtiene los requisitos y exclusiones del tipo de elemento en tratamiento
		If mcolTab_reqexc.Find_by_type(nBranch, nProduct, dEffecdate, sType1) Then
			
			'+Depende del elemento a validar se busca los elementos "padres"
			'+Ejemplo: Si estoy en coberturas, primero valido los módulos
			'+Ejemplo: Si estoy en recargos, primero módulo y coberturas
			Select Case sType1
				Case eProduct.Tab_reqexc.ReqexclType.cstrModule
					lintTo = 1
					
				Case eProduct.Tab_reqexc.ReqexclType.cstrCover
					lintTo = 2
					
				Case eProduct.Tab_reqexc.ReqexclType.cstrDiscExpr
					lintTo = 3
					
				Case eProduct.Tab_reqexc.ReqexclType.cstrClause
					lintTo = 4
			End Select
			
			'+Si estoy en la CA014 y el ramo técnico es Vida se lee la información de TCOVER
			If sCodispl = "CA014" Then
				If sBrancht = CStr(eProduct.Product.pmBrancht.pmlife) Or sBrancht = CStr(eProduct.Product.pmBrancht.pmNotTraditionalLife) Then
					lstrReaTCover = "1"
				End If
			End If
			
			For lintIndex = 1 To lintTo
				'+Se obtiene los códigos de los elementos seleccionados
				Call GetCodes(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate, sType1, lintIndex, sCodes, sBrancht, sRoles)
				
				lstrCodes = mstrElements
				lstrRoles = mstrRoles
				If lstrCodes > String.Empty Then
					'+Se llama el procedimiento que recorre los elementos
					Call InsValReq_Exc(sCodispl, sType1, lintIndex, sCodes, lstrCodes, sSel1, sRoles, lstrRoles, lclsErrors)
				End If
			Next 
		End If
	End Function
	
	'%InsValReq_Exc: Valida los requisitos y exclusiones por tipo de elemento
	Private Function InsValReq_Exc(ByVal sCodispl As String, ByVal sType1 As eProduct.Tab_reqexc.ReqexclType, ByVal sType2 As eProduct.Tab_reqexc.ReqexclType, ByVal sCodes1 As String, ByVal sCodes2 As String, ByVal sSels1 As String, ByVal sRoles1 As String, ByVal sRoles2 As String, ByRef lclsErrors As eFunctions.Errors) As Boolean
		Dim lintPos As Integer
		Dim lstrCode As String
		Dim lstrCodes As String
		Dim lstrRole As String
		Dim lstrRoles As String
		
		'+Se recorre la cadena que contiene los código para obtener c/u
		lstrCodes = Trim(sCodes1)
		lstrRoles = Trim(sRoles1)
		While Len(lstrCodes) > 0
			'+Se busca el código de cada elemento
			lintPos = InStr(1, lstrCodes, ",")
			If lintPos > 0 Then
				lstrCode = Mid(lstrCodes, 1, lintPos - 1)
				lstrCodes = Mid(lstrCodes, lintPos + 1)
			Else
				lstrCode = lstrCodes
				lstrCodes = String.Empty
			End If
			
			'+Se busca la figura asociada al elemento
			lintPos = InStr(1, lstrRoles, ",")
			If lintPos > 0 Then
				lstrRole = Mid(lstrRoles, 1, lintPos - 1)
				lstrRoles = Mid(lstrRoles, lintPos + 1)
			Else
				lstrRole = lstrRoles
				lstrRoles = String.Empty
			End If
			
			If lstrRole = String.Empty Then
				lstrRole = "0"
			End If
			
			'+Se llama al procedimiento que valida cada elemento
			Call InsVal_Element(sCodispl, CInt(lstrCode), sSels1, sCodes1, sCodes2, sType1, CStr(sType2), CInt(lstrRole), sRoles1, sRoles2, lclsErrors)
		End While
		
	End Function
	
	'%InsVal_Element: Valida el elemento de la cadena
	Private Function InsVal_Element(ByVal sCodispl As String, ByVal nCode As Integer, ByVal sSel As String, ByVal sCodes1 As String, ByVal sCodes2 As String, ByVal sType1 As eProduct.Tab_reqexc.ReqexclType, ByVal sType2 As String, ByVal nRole As Integer, ByVal sRoles1 As String, ByVal sRoles2 As String, ByRef lclsErrors As eFunctions.Errors) As Boolean
		Dim lclsTab_reqexc As eProduct.Tab_reqexc
        Dim lblnFound As Boolean = False
        Dim lstrMessage As String = String.Empty
        Dim lstrRelation As String = String.Empty
        Dim lblnError As Boolean = False
        Dim lstrSomeOne As String = String.Empty
        Dim llngCode As String = String.Empty
        Dim llngCode2 As String = String.Empty
        Dim llngRole As String = String.Empty
        Dim llngRole2 As String = String.Empty
        Dim lstrDescriptype1 As String = String.Empty
        Dim lstrDescriptype2 As String = String.Empty
        Dim lblnFind As Boolean = False
        Dim lstrDesRole As String = String.Empty
        Dim lstrDesRole2 As String = String.Empty
		
		For	Each lclsTab_reqexc In mcolTab_reqexc
			lblnError = False
			lblnFind = False
			'+Si la relación es inversa se busca el código pasado como parámetro
			'+Si el código tratado corresponde al código leído
			If (nCode = lclsTab_reqexc.nCode1 And nRole = lclsTab_reqexc.nRole1) Then
				
				llngCode = CStr(lclsTab_reqexc.nCode2)
				llngRole = CStr(lclsTab_reqexc.nRole2)
				'+Si corresponde al mismo tipo
				If CStr(sType1) = sType2 Then
					'+Se busca el elemento en la cadena si la relación no es inversa
					lblnFind = lclsTab_reqexc.sInvrel = "0"
					If lblnFind Then
						lblnFind = Find_Code(nCode, sCodes2, nRole, sRoles2, sSel)
					End If
				Else
					'+Se busca el elemento en la cadena si la relación es inversa
					lblnFind = lclsTab_reqexc.sInvrel = "1"
					If lblnFind Then
						lblnFind = Find_Code(lclsTab_reqexc.nCode2, sCodes2, lclsTab_reqexc.nRole2, sRoles2, "1")
						llngCode = CStr(nCode)
						llngRole = CStr(nRole)
					End If
				End If
			End If
			
			If lblnFind Then
				lblnFound = Find_Code(CInt(llngCode), sCodes1, CInt(llngRole), sRoles1, sSel)
				Select Case lclsTab_reqexc.sRelation
					Case CStr(eProduct.Tab_reqexc.RelationType.cstrReq)
						If Not lblnFound Then
							lblnError = True
							lstrRelation = "incluir "
						End If
						
					Case CStr(eProduct.Tab_reqexc.RelationType.cstrExc)
						If lblnFound Then
							lblnError = True
							lstrRelation = "excluir "
						End If
						
					Case CStr(eProduct.Tab_reqexc.RelationType.cstrSome)
						If Not bIsInSomeone Then
							If lblnFound Then
								bIsInSomeone = True
								lstrSomeOne = String.Empty
							Else
								If mclsTables Is Nothing Then
									mclsTables = New eFunctions.Tables
								End If
								If mclsTables.GetDescription("Table72", sType2) Then
									lstrDescriptype2 = mclsTables.Descript
								End If
								
								If lstrSomeOne = String.Empty Then
									lstrSomeOne = "incluir " & lstrDescriptype2 & " " & lclsTab_reqexc.nCode2
								Else
									lstrSomeOne = lstrSomeOne & " o " & lstrDescriptype2 & " " & lclsTab_reqexc.nCode2
								End If
							End If
						End If
				End Select
				
				If lblnError Then
					If mclsTables Is Nothing Then
						mclsTables = New eFunctions.Tables
					End If
					If lclsTab_reqexc.sInvrel = "1" Then
						llngCode = CStr(lclsTab_reqexc.nCode2)
						llngCode2 = CStr(nCode)
						lclsTab_reqexc.nCode2 = nCode
						llngRole = CStr(lclsTab_reqexc.nRole2)
						llngRole2 = CStr(nRole)
						lclsTab_reqexc.nRole2 = nRole
						
						If mclsTables.GetDescription("Table72", sType2) Then
							lstrDescriptype1 = mclsTables.Descript
						End If
						If mclsTables.GetDescription("Table72", CStr(sType1)) Then
							lstrDescriptype2 = mclsTables.Descript
						End If
					Else
						llngCode = CStr(nCode)
						llngCode2 = CStr(lclsTab_reqexc.nCode2)
						llngRole = CStr(nRole)
						llngRole2 = CStr(lclsTab_reqexc.nRole2)
						If mclsTables.GetDescription("Table72", CStr(sType1)) Then
							lstrDescriptype1 = mclsTables.Descript
						End If
						If mclsTables.GetDescription("Table72", sType2) Then
							lstrDescriptype2 = mclsTables.Descript
						End If
					End If
					
					'+Se busca la descripción de la figura en tratamiento
					lstrDesRole = String.Empty
					lstrDesRole2 = String.Empty
					If CDbl(llngRole) > 0 Then
						If mclsTables.GetDescription("Table12", llngRole) Then
							lstrDesRole = mclsTables.Descript & "-"
						End If
					End If
					
					If CDbl(llngRole2) > 0 Then
						If mclsTables.GetDescription("Table12", llngRole2) Then
							lstrDesRole2 = mclsTables.Descript & "-"
						End If
					End If
					
					lstrMessage = lstrDesRole & lstrDescriptype1 & " " & llngCode & ", debe " & lstrRelation & lstrDesRole2 & lstrDescriptype2 & " " & llngCode2
					
					lclsErrors.ErrorMessage(sCodispl, 3715,  , eFunctions.Errors.TextAlign.RigthAling, lstrMessage)
				End If
			End If
		Next lclsTab_reqexc
		If lstrSomeOne > String.Empty Then
			If mclsTables.GetDescription("Table72", CStr(sType1)) Then
				lstrSomeOne = mclsTables.Descript & " " & nCode & " debe " & lstrSomeOne
			End If
			lclsErrors.ErrorMessage(sCodispl, 3715,  , eFunctions.Errors.TextAlign.RigthAling, lstrSomeOne)
		End If
		'UPGRADE_NOTE: Object lclsTab_reqexc may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsTab_reqexc = Nothing
	End Function
	
	'%Find_Code: Busca el código dado dentro de la cadena
	Private Function Find_Code(ByVal nCode As Integer, ByVal sCodes As String, ByVal nRole As Integer, ByVal sRoles As String, ByVal sSel As String) As Boolean
		Dim lstrCode As String
		Dim lstrCodes As String
		Dim lstrRole As String
		Dim lstrRoles As String
		Dim lstrSels As String
        Dim lstrSel As String = ""
        Dim lintPos As Integer
		Dim lblnFound As Boolean
		
		lstrCodes = Trim(sCodes)
		lstrSels = Trim(sSel)
		lstrRoles = Trim(sRoles)
		While Len(lstrCodes) > 0 And Not lblnFound
			'+Se busca el código indicado dentro de la cadena
			lintPos = InStr(1, lstrCodes, ",")
			If lintPos > 0 Then
				lstrCode = Mid(lstrCodes, 1, lintPos - 1)
				lstrCodes = Mid(lstrCodes, lintPos + 1)
			Else
				lstrCode = lstrCodes
				lstrCodes = String.Empty
			End If
			
			'+Se busca la figura asociada al código
			lintPos = InStr(1, lstrRoles, ",")
			If lintPos > 0 Then
				lstrRole = Mid(lstrRoles, 1, lintPos - 1)
				lstrRoles = Mid(lstrRoles, lintPos + 1)
			Else
				lstrRole = lstrRoles
				lstrRoles = String.Empty
			End If
			
			If lstrRole = String.Empty Then
				lstrRole = "0"
			End If
			
			'+Se busca el indicador de selección
			lintPos = InStr(1, lstrSels, ",")
			If lintPos > 0 Then
				lstrSel = Mid(lstrSels, 1, lintPos - 1)
				lstrSels = Mid(lstrSels, lintPos + 1)
			Else
				lstrSel = lstrSels
			End If
			lblnFound = CDbl(Trim(lstrCode)) = nCode And CDbl(Trim(lstrRole)) = nRole
		End While
		Find_Code = lblnFound And Trim(lstrSel) = "1"
	End Function
	
	'%GetCodes: Obtiene los códigos de los elementos a evaluar a evaluar
	Private Sub GetCodes(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, ByVal sType1 As eProduct.Tab_reqexc.ReqexclType, ByVal sType2 As eProduct.Tab_reqexc.ReqexclType, ByVal sCodes As String, ByVal sBrancht As String, ByVal sRoles As String)
		If sType1 = sType2 Then
			mstrElements = sCodes
			mstrRoles = sRoles
		Else
			Call GetElementsSel(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate, sType2, sBrancht)
		End If
	End Sub
	
	'%GetElementsSel: Obtiene los elementos que existen dado su tipo
	Private Sub GetElementsSel(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, ByVal nReqexc As eProduct.Tab_reqexc.ReqexclType, ByVal sBrancht As String)
		Dim lrecreaElements_sel As eRemoteDB.Execute
		
		On Error GoTo GetElementsSel_Err
		'+ Definición de store procedure reaElements_sel al 09-05-2002 21:57:52
		lrecreaElements_sel = New eRemoteDB.Execute
		With lrecreaElements_sel
			.StoredProcedure = "ReaElements_Sel"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nReqexc", nReqexc, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sBrancht", sBrancht, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sElement", String.Empty, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 2000, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sRoles", String.Empty, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 2000, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				mstrElements = .Parameters("sElement").Value
				mstrRoles = .Parameters("sRoles").Value
			End If
		End With
		
GetElementsSel_Err: 
		If Err.Number Then
			mstrElements = String.Empty
			mstrRoles = String.Empty
		End If
		'UPGRADE_NOTE: Object lrecreaElements_sel may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaElements_sel = Nothing
		On Error GoTo 0
	End Sub
	
	'%Class_Terminate: Se ejecuta cuando se destruye la clase
	'UPGRADE_NOTE: Class_Terminate was upgraded to Class_Terminate_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Terminate_Renamed()
		'UPGRADE_NOTE: Object mcolTab_reqexc may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		mcolTab_reqexc = Nothing
		'UPGRADE_NOTE: Object mclsTables may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		mclsTables = Nothing
	End Sub
	Protected Overrides Sub Finalize()
		Class_Terminate_Renamed()
		MyBase.Finalize()
	End Sub
End Class






