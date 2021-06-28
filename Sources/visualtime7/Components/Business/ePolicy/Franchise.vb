Option Strict Off
Option Explicit On
Public Class Franchise
	'%-------------------------------------------------------%'
	'% $Workfile:: Franchise.cls                            $%'
	'% $Author:: Nvaplat41                                  $%'
	'% $Date:: 9/10/03 19.01                                $%'
	'% $Revision:: 19                                       $%'
	'%-------------------------------------------------------%'
	
	'+ Estructura de tabla franchise al 08-22-2002 09:32:34
	'+     Property                    Type         DBType   Size Scale  Prec  Null
	'+-----------------------------------------------------------------------------
	Public sCertype As String ' CHAR       1    0     0    N
	Public nBranch As Integer ' NUMBER     22   0     5    N
	Public nProduct As Integer ' NUMBER     22   0     5    N
	Public nPolicy As Double ' NUMBER     22   0     10   N
	Public nCertif As Double ' NUMBER     22   0     10   N
	Public dEffecdate As Date ' DATE       7    0     0    N
	Public nCaren_quan As Integer ' NUMBER     22   0     5    S
	Public sCaren_type As String ' CHAR       1    0     0    N
	Public dCompdate As Date ' DATE       7    0     0    S
	Public nDisc_Amoun As Double ' NUMBER     22   2     8    S
	Public nDiscount As Double ' NUMBER     22   2     4    S
	Public nFixamount As Double ' NUMBER     22   0     12   S
	Public sFrandedi As String ' CHAR       1    0     0    S
	Public nMaxamount As Double ' NUMBER     22   0     12   S
	Public nMinamount As Double ' NUMBER     22   0     12   S
	Public dNulldate As Date ' DATE       7    0     0    S
	Public nRate As Double ' NUMBER     22   2     4    S
	Public nUsercode As Integer ' NUMBER     22   0     5    S
	Public nWait_quan As Integer ' NUMBER     22   0     5    S
	Public sWait_type As String ' CHAR       1    0     0    S
	Public sFrancApl As String ' CHAR       1    0     0    S
	Public nCurrency As Integer ' NUMBER     22   0     5    S
	Public nSeq As Double
	Public nDed_Type As Integer
	Public nCover As Integer
	Public nPay_Concep As Integer
	Public nLevel As Integer
	Public nRole As Integer
	Public nOrder As Integer
	Public nModulec As Integer
	Public nGroup As Integer
	Public nCountGroup As Double
	
	Public sFrancAplDis As String
	Public sMessage As String
	
	Public bError As Boolean
	Public bFindGroup As Boolean
	
	Public nError As Integer
	
	'- Variables Auxiliares
	
	Public sProcess As String
	
	'% Exist_nSeq: Verifica si ya está registrado el número de deducible
	Public Function Exist_nSeq(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal nSeq As Double, ByVal nGroup As Integer) As Boolean
		
		Static lblnRead As Boolean
		
		Dim nExist As Integer
		
		'- Se define la variable lrecreaFranchise
		
		Dim lrecReaFranchise As eRemoteDB.Execute
		
		On Error GoTo Find_Err
		
		lrecReaFranchise = New eRemoteDB.Execute
		
		'+ Definición de parámetros para stored procedure 'insudb.reaFranchise_Exist_nSeq'
		
		With lrecReaFranchise
			.StoredProcedure = "ReaFranchise_Exist_nSeq"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nSeq", nSeq, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nExist", nExist, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nGroup", nGroup, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run(False) Then
				If .Parameters("nExist").Value = 1 Then
					lblnRead = True
				Else
					lblnRead = False
				End If
				.RCloseRec()
			Else
				lblnRead = False
			End If
		End With
		
		Exist_nSeq = lblnRead
		
Find_Err: 
		If Err.Number Then
			Exist_nSeq = False
		End If
		On Error GoTo 0
		
		'UPGRADE_NOTE: Object lrecReaFranchise may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecReaFranchise = Nothing
	End Function
	
	'% Exist_Certif: Verifica si ya está registrado un deducible con Nivel Certificado
	Public Function Exist_Certif(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Integer, ByVal nCertif As Integer, ByVal nGroup As Integer) As Boolean
		
		Static lblnRead As Boolean
		
		Dim nExist As Integer
		
		'- Se define la variable lrecreaFranchise
		
		Dim lrecReaFranchise As eRemoteDB.Execute
		
		On Error GoTo Find_Err
		
		lrecReaFranchise = New eRemoteDB.Execute
		
		'+ Definición de parámetros para stored procedure 'insudb.reaFranchise_Exist_Policy'
		
		With lrecReaFranchise
			.StoredProcedure = "ReaFranchise_Exist_Certif"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nExist", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nGroup", nGroup, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run(False) Then
				If .Parameters("nExist").Value = 1 Then
					lblnRead = True
				Else
					lblnRead = False
				End If
				.RCloseRec()
			Else
				lblnRead = False
			End If
		End With
		
		Exist_Certif = lblnRead
		
Find_Err: 
		If Err.Number Then
			Exist_Certif = False
		End If
		On Error GoTo 0
		
		'UPGRADE_NOTE: Object lrecReaFranchise may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecReaFranchise = Nothing
	End Function
	
	
	
	'% Exist_Policy: Verifica si ya está registrado un deducible con Nivel Póliza
	Public Function Exist_Policy(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nGroup As Integer) As Boolean
		
		Static lblnRead As Boolean
		
		Dim nExist As Integer
		
		'- Se define la variable lrecreaFranchise
		
		Dim lrecReaFranchise As eRemoteDB.Execute
		
		On Error GoTo Find_Err
		
		lrecReaFranchise = New eRemoteDB.Execute
		
		'+ Definición de parámetros para stored procedure 'insudb.reaFranchise_Exist_Policy'
		
		With lrecReaFranchise
			.StoredProcedure = "ReaFranchise_Exist_Policy"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nExist", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nGroup", nGroup, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				If .Parameters("nExist").Value = 1 Then
					lblnRead = True
				Else
					lblnRead = False
				End If
				.RCloseRec()
			Else
				lblnRead = False
			End If
		End With
		
		Exist_Policy = lblnRead
		
Find_Err: 
		If Err.Number Then
			Exist_Policy = False
		End If
		On Error GoTo 0
		
		'UPGRADE_NOTE: Object lrecReaFranchise may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecReaFranchise = Nothing
	End Function
	
	'% Exist_nOrder: Verifica si ya está registrado el orden del deducible
	Public Function Exist_nOrder(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal nOrder As Double, ByVal nGroup As Integer) As Boolean
		
		Static lblnRead As Boolean
		
		Dim nExist As Integer
		
		'- Se define la variable lrecreaFranchise
		
		Dim lrecReaFranchise As eRemoteDB.Execute
		
		On Error GoTo Find_Err
		
		lrecReaFranchise = New eRemoteDB.Execute
		
		'+ Definición de parámetros para stored procedure 'insudb.reaFranchise_nOrder'
		
		With lrecReaFranchise
			.StoredProcedure = "ReaFranchise_Exist_nOrder"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nOrder", nOrder, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nExist", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nGroup", nGroup, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run(False) Then
				If .Parameters("nExist").Value = 1 Then
					lblnRead = True
				Else
					lblnRead = False
				End If
				.RCloseRec()
			Else
				lblnRead = False
			End If
		End With
		
		Exist_nOrder = lblnRead
		
Find_Err: 
		If Err.Number Then
			Exist_nOrder = False
		End If
		On Error GoTo 0
		
		'UPGRADE_NOTE: Object lrecReaFranchise may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecReaFranchise = Nothing
	End Function
	
	'% Find: Devuelve información de un registro de la tabla Franchise
	Public Function Find(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal nSeq As Double, ByVal dEffecdate As Date, Optional ByVal lblnFind As Boolean = False) As Boolean
		
		Static lblnRead As Boolean
		
		'- Se define la variable lrecreaFranchise
		
		Dim lrecReaFranchise As eRemoteDB.Execute
		
		On Error GoTo Find_Err
		
		lrecReaFranchise = New eRemoteDB.Execute
		
		If Me.sCertype <> sCertype Or Me.nBranch <> nBranch Or Me.nProduct <> nProduct Or Me.nPolicy <> nPolicy Or Me.nCertif <> nCertif Or Me.nSeq <> nSeq Or Me.dEffecdate <> dEffecdate Or lblnFind Then
			
			Me.sCertype = sCertype
			Me.nBranch = nBranch
			Me.nProduct = nProduct
			Me.nPolicy = nPolicy
			Me.nCertif = nCertif
			Me.nSeq = nSeq
			Me.dEffecdate = dEffecdate
			
			'+ Definición de parámetros para stored procedure 'insudb.reaFranchise_nSeq'
			
			With lrecReaFranchise
				.StoredProcedure = "reaFranchise_nSeq"
				.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nSeq", nSeq, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				
				If .Run Then
					sCertype = .FieldToClass("sCertype")
					nBranch = .FieldToClass("nBranch")
					nProduct = .FieldToClass("nProduct")
					nPolicy = .FieldToClass("nPolicy")
					nCertif = .FieldToClass("nCertif")
					dEffecdate = .FieldToClass("dEffecdate")
					nFixamount = .FieldToClass("nFixamount")
					sFrandedi = .FieldToClass("sFrandedi")
					nMaxamount = .FieldToClass("nMaxamount")
					nMinamount = .FieldToClass("nMinamount")
					nDiscount = .FieldToClass("nDiscount")
					nDisc_Amoun = .FieldToClass("nDisc_amoun")
					nRate = .FieldToClass("nRate")
					sFrancApl = .FieldToClass("sFrancapl")
					nCurrency = .FieldToClass("ncurrency")
					nSeq = .FieldToClass("nSeq")
					nDed_Type = .FieldToClass("nDed_Type")
					nCover = .FieldToClass("nCover")
					nPay_Concep = .FieldToClass("nPay_Concep")
					nLevel = .FieldToClass("nLevel")
					nRole = .FieldToClass("nRole")
					nOrder = .FieldToClass("nOrder")
					nModulec = .FieldToClass("nModulec")
					
					.RCloseRec()
					lblnRead = True
				Else
					lblnRead = False
				End If
			End With
		End If
		
		Find = lblnRead
		
Find_Err: 
		If Err.Number Then
			Find = False
		End If
		On Error GoTo 0
		
		'UPGRADE_NOTE: Object lrecReaFranchise may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecReaFranchise = Nothing
	End Function
	
	'% Update: Se actualiza un registro en la tabla Franchise
	Public Function Update() As Boolean
		
		'- Se define la variable lrecinsFranchise
		
		Dim lrecinsFranchise As eRemoteDB.Execute
		
		On Error GoTo Update_Err
		
		lrecinsFranchise = New eRemoteDB.Execute
		
		'+ Definición de parámetros para stored procedure 'insFranchise'
		'+ Información leída el 02/01/2001 9:28:56
		
		With lrecinsFranchise
			.StoredProcedure = "insFranchise"
			.Parameters.Add("sProcess", sProcess, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nFixamount", nFixamount, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nMaxamount", nMaxamount, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nMinamount", nMinamount, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nRate", nRate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 4, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sFrancApl", sFrancApl, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCurrency", nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sFrandedi", sFrandedi, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nDiscount", nDiscount, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 4, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDisc_amoun", nDisc_Amoun, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

			Update = .Run(False)
		End With
		
Update_Err: 
		If Err.Number Then
			Update = False
		End If
		On Error GoTo 0
		
		'UPGRADE_NOTE: Object lrecinsFranchise may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecinsFranchise = Nothing
	End Function
	
	
	
	'% Update_CA960: Se actualiza un registro en la tabla Franchise
	Public Function Update_CA960(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, ByVal nFixamount As Double, ByVal nMaxamount As Double, ByVal nMinamount As Double, ByVal nRate As Double, ByVal nUsercode As Integer, ByVal sFrancApl As String, ByVal nCurrency As Integer, ByVal nSeq As Double, ByVal nDed_Type As Integer, ByVal nCover As Integer, ByVal nPay_Concep As Integer, ByVal nLevel As Integer, ByVal nRole As Integer, ByVal nOrder As Integer, ByVal nModulec As Integer, ByVal nGroup As Integer) As Boolean
		
		'- Se define la variable lrecinsFranchise
		
		Dim lrecinsFranchise As eRemoteDB.Execute
		
		On Error GoTo Update_Err
		
		lrecinsFranchise = New eRemoteDB.Execute
		
		'+ Definición de parámetros para stored procedure 'insFranchise'
		'+ Información leída el 02/01/2001 9:28:56
		
		With lrecinsFranchise
			.StoredProcedure = "insFranchise_CA960"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nFixamount", nFixamount, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nMaxamount", nMaxamount, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nMinamount", nMinamount, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nRate", nRate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 4, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sFrancApl", sFrancApl, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCurrency", nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nSeq", nSeq, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDed_Type", nDed_Type, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCover", nCover, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPay_Concep", nPay_Concep, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nLevel", nLevel, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nRole", nRole, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nOrder", nOrder, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nModulec", nModulec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nGroup", nGroup, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			Update_CA960 = .Run(False)
		End With
		
Update_Err: 
		If Err.Number Then
			Update_CA960 = False
		End If
		On Error GoTo 0
		
		'UPGRADE_NOTE: Object lrecinsFranchise may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecinsFranchise = Nothing
	End Function
	
	'% insPreCA015: Esta función consulta la franquicia/deducible asociado a una póliza o certificado
	Public Function insPreCA015(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date) As Boolean
		
		insPreCA015 = True
		sFrancAplDis = String.Empty
		On Error GoTo insPreCA015_Err
		
		If Not Find(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate.ToOADate, System.Date.FromOADate(True)) Then
			sFrandedi = "1"
			InitValues()
			GetProductGen(nBranch, nProduct, dEffecdate)
			insPreCA015 = False
		Else
			'+ Se encontró Franquicia/Deducible asignado a la Póliza.
			'+ Se llenan los campos Tipo de Franquicia/Deducible y Aplica Sobre
			If sFrandedi <> String.Empty Then
				'+ Se selecciona la opción respectiva en el campo Tipo de Franquicia/Deducible
				If sFrandedi = "1" Then
					'+ Se selecciona la opción "No Aplica" en el campo "Aplica Sobre" y se inicializan y bloquean los campos
					InitValues()
				End If
			Else
				'+ Se selecciona la opción "No Tiene" en Tipo de Franquicia/Deducible
				sFrandedi = "1"
				InitValues()
			End If
		End If
		
insPreCA015_Err: 
		If Err.Number Then
			insPreCA015 = False
		End If
		
		On Error GoTo 0
	End Function
	
	'%InitValues: Este procedimiento es invocado cada vez que el usuario selecciona una de las
	'%opciones del campo Tipo de Franquicia/Deducible asociado a la Póliza
	Private Sub InitValues()
		'+Si el usuario señala que "No Tiene" Franquicia/Deducible, entonces se inicializan los campos de la ventana
		sFrancApl = "1"
		nCurrency = 0
		nDiscount = 0
		nDisc_Amoun = 0
		nRate = 0
		nFixamount = 0
		nMinamount = 0
		nMaxamount = 0
	End Sub
	
	'%-------------------------------------------------------------------------------------------
	Private Sub GetProductGen(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal dEffecdate As Date)
		'%-------------------------------------------------------------------------------------------
		
		Dim lclsProduct_ge As eProduct.Product_ge
		lclsProduct_ge = New eProduct.Product_ge
		
		'+ Se leen los valores de Franquicia/Deducible introducidos en el Diseñador de Productos
		
		With lclsProduct_ge
			If Not .Find(nBranch, nProduct, dEffecdate) Then
				'+ No se encontró información de Franquicia/Deducible en el Diseñador de Productos
				'+ Se inicializan los campos de la ventana, ya que no existe información en Product_ge ni en Franchise.
				sFrandedi = "1"
				InitValues()
			Else
				'+ Se encontró información de Franquicia/Deducible en el Diseñador de Productos
				'+ Se carga la información de Franquicia/Deducible del Diseñador de Productos, ya que
				'+ no existía información en Franchise.
				'+ Se llenan los campos "Tipo de Franquicia/Deducible" y "Aplica sobre"
				If Not (.sFrantype = String.Empty) Then
					sFrandedi = .sFrantype
					If sFrandedi = "1" Then
						InitValues()
					Else
						'+ Se llena el campo "Aplica Sobre"
						If .sFrancApl <> String.Empty Then
							sFrancApl = .sFrancApl
							sFrancAplDis = .sFrancApl
						Else
							sFrancApl = String.Empty
						End If
					End If
				Else
					sFrandedi = "1"
					'+ Se llena el campo "Aplica Sobre"
					If .sFrancApl <> String.Empty Then
						sFrancAplDis = .sFrancApl
					End If
					InitValues()
				End If
				'+ Se llena el campo "Moneda"
				If .nCurrency <> eRemoteDB.Constants.intNull Then
					nCurrency = CInt(.nCurrency)
				Else
					nCurrency = eRemoteDB.Constants.intNull
				End If
				'+ Se llenan los campos Porcentaje e Importe Fijo de Descuento
				nDiscount = eRemoteDB.Constants.intNull
				nDisc_Amoun = eRemoteDB.Constants.intNull
				'+ Se llenan los campos "Porcentaje" e "Importe Fijo" de Franquicia/Deducible
				If .nFrancrat <> eRemoteDB.Constants.intNull Then
					If .nFrancrat <> 0 Then
						nRate = CDbl(.nFrancrat)
						nFixamount = eRemoteDB.Constants.intNull
					Else
						nRate = eRemoteDB.Constants.intNull
						
						If Not (.nFrancFix = eRemoteDB.Constants.intNull) Then
							nFixamount = CDbl(.nFrancFix)
						Else
							nFixamount = eRemoteDB.Constants.intNull
						End If
					End If
				Else
					nRate = eRemoteDB.Constants.intNull
					
					If .nFrancFix <> eRemoteDB.Constants.intNull Then
						nFixamount = CDbl(.nFrancFix)
					Else
						nFixamount = eRemoteDB.Constants.intNull
					End If
				End If
				'+ Se llena el campo Mínimo
				If .nFrancMin <> eRemoteDB.Constants.intNull And nRate <> eRemoteDB.Constants.intNull Then
					nMinamount = CDbl(.nFrancMin)
				Else
					nMinamount = eRemoteDB.Constants.intNull
				End If
				'+ Se llena el campo Máximo
				If .nFrancMax <> eRemoteDB.Constants.intNull And nRate <> eRemoteDB.Constants.intNull Then
					nMaxamount = CDbl(.nFrancMax)
				Else
					nMaxamount = eRemoteDB.Constants.intNull
				End If
			End If
		End With
		
		'UPGRADE_NOTE: Object lclsProduct_ge may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsProduct_ge = Nothing
		
	End Sub
	
    Public Function GetFranchiseType(ByVal nIndex As Integer) As String
        GetFranchiseType = IIf(CStr(nIndex) = sFrandedi, 1, 2)
    End Function
	
	'%insValCA015: Esta función realiza las validaciones de la ventana CA015
	Public Function insValCA015(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, ByVal sTransaction As String, ByVal sTypeCompany As String, ByVal sFranchiseType As String, ByVal sFrancApl As String, ByVal nCurrency As Integer, ByVal nDiscount As Double, ByVal nDisc_Amoun As Double, ByVal nRate As Double, ByVal nFixamount As Double, ByVal nMinamount As Double, ByVal nMaxamount As Double) As String
		Dim lclsPolicyWin As ePolicy.Policy_Win
		Dim lclsErrors As eFunctions.Errors
		Dim llngIndex As Integer
		
		On Error GoTo insValCA015_Err
		
		lclsPolicyWin = New ePolicy.Policy_Win
		lclsErrors = New eFunctions.Errors
		
		'+No se puede indicar "NO Tiene" si la ventana está requerida en la secuencia de Pólizas
		'+en tratamiento
		If sFranchiseType = "1" Then
			If lclsPolicyWin.Find_Sequen_Pol(sTransaction, sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate, sTypeCompany, "CA015") Then
				If lclsPolicyWin.sRequire = "1" Then
					Call lclsErrors.ErrorMessage("CA015", 3800)
				End If
			End If
		End If
		
		
		'+Si alguno de los campos correspondientes a importes está lleno, el campo moneda
		'+debe estar lleno
		If ((nDisc_Amoun <> eRemoteDB.Constants.intNull And nDisc_Amoun <> 0) Or (nFixamount <> eRemoteDB.Constants.intNull And nFixamount <> 0) Or (nMinamount <> eRemoteDB.Constants.intNull And nMinamount <> 0) Or (nMaxamount <> eRemoteDB.Constants.intNull And nMaxamount <> 0)) And nCurrency <= 0 Then
			Call lclsErrors.ErrorMessage("CA015", 1351)
		End If
		
		
		'+Si este campo está lleno y el valor de "Tipo" es igual a "No Tiene" o el campo "Importe Fijo de Franquicia/Deducible"
		'+está lleno, su valor debe ser igual a "No Aplica"
		If sFrancApl <> String.Empty Then
			If sFranchiseType = "1" Or nFixamount <> eRemoteDB.Constants.intNull Then
				If sFrancApl <> "1" Then
					Call lclsErrors.ErrorMessage("CA015", 11378)
				End If
			End If
		End If
		
		
		'+Sólo puede estar lleno uno sólo de estos dos campos
		If (nDiscount <> eRemoteDB.Constants.intNull And nDiscount <> 0) And (nDisc_Amoun <> eRemoteDB.Constants.intNull And nDisc_Amoun <> 0) Then
			Call lclsErrors.ErrorMessage("CA015", 3801)
		End If
		
		
		'+Validación del campo % e Importe fijo de Franquicia/Deducible
		'+Puede estar lleno sólo uno de estos dos campos
		If nRate <> eRemoteDB.Constants.intNull And nRate <> 0 Then
			If nFixamount <> eRemoteDB.Constants.intNull And nFixamount <> 0 Then
				Call lclsErrors.ErrorMessage("CA015", 3046)
			End If
		Else
			If (nFixamount = eRemoteDB.Constants.intNull Or nFixamount = 0) And sFranchiseType <> "1" Then
				'+Debe estar lleno al menos uno de los dos campos
				Call lclsErrors.ErrorMessage("CA015", 3802)
			End If
		End If
		
		
		'+Validación del campo Condiciones-Máximo
		'+Si el campo está lleno, debe ser superior al importe mínimo de Franquicia/Deducible
		If nMaxamount <> eRemoteDB.Constants.intNull And nMinamount <> eRemoteDB.Constants.intNull And nMaxamount <> 0 And nMinamount <> 0 Then
			If nMaxamount <= nMinamount Then
				Call lclsErrors.ErrorMessage("CA015", 3803)
			End If
		End If
		
		insValCA015 = lclsErrors.Confirm
		
insValCA015_Err: 
		If Err.Number Then
			insValCA015 = insValCA015 & Err.Description
		End If
		
		'UPGRADE_NOTE: Object lclsPolicyWin may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsPolicyWin = Nothing
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
		
		On Error GoTo 0
	End Function
	
	'% insPostCA015: Esta función registra, modifica o elimina, según el caso, la franquicia/deducible
	'% asociada a una póliza o certificado
	Public Function insPostCA015(ByVal sCodispl As String, ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, ByVal nUsercode As Integer, ByVal sTransaction As String, ByVal sTypeCompany As String, ByVal sFranchiseType As String, ByVal sFrancApl As String, ByVal nCurrency As Integer, ByVal nDiscount As Double, ByVal nDisc_Amoun As Double, ByVal nRate As Double, ByVal nFixamount As Double, ByVal nMinamount As Double, ByVal nMaxamount As Double, ByVal sPolitype As String) As Boolean
		
		Dim lclsFranchise As ePolicy.Franchise
		Dim lclsPolicyWin As ePolicy.Policy_Win
		Dim llngIndexCA014 As Integer
		Dim llngIndexCA017 As Integer
		Dim bChange As Boolean
		
		On Error GoTo insPostCA015_Err
		lclsFranchise = New ePolicy.Franchise
		lclsPolicyWin = New ePolicy.Policy_Win
		
		bChange = False
		
		'+ Se verifica si existieron cambios en el descuento
		Call Find(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate.ToOADate, System.Date.FromOADate(True))
		If Me.nDiscount <> nDiscount Or Me.nDisc_Amoun <> nDisc_Amoun Then
			bChange = True
		End If
		
		'+ Se llena el parámetro sProcess
		'+ Si el proceso en tratamiento es: Emisión, Recuperación, cotización o solicitud
		If sTransaction = CStr(Constantes.PolTransac.clngPolicyIssue) Or sTransaction = CStr(Constantes.PolTransac.clngCertifIssue) Or sTransaction = CStr(Constantes.PolTransac.clngRecuperation) Or sTransaction = CStr(Constantes.PolTransac.clngPolicyQuotation) Or sTransaction = CStr(Constantes.PolTransac.clngCertifQuotation) Or sTransaction = CStr(Constantes.PolTransac.clngPolicyProposal) Or sTransaction = CStr(Constantes.PolTransac.clngCertifProposal) Then
			lclsFranchise.sProcess = "1"
			
			'+ Si el proceso en tratamiento es: Modificación
		ElseIf sTransaction = CStr(Constantes.PolTransac.clngPolicyAmendment) Or sTransaction = CStr(Constantes.PolTransac.clngTempPolicyAmendment) Or sTransaction = CStr(Constantes.PolTransac.clngCertifAmendment) Or sTransaction = CStr(Constantes.PolTransac.clngTempCertifAmendment) Then 
			lclsFranchise.sProcess = "2"
		Else
			lclsFranchise.sProcess = "0"
		End If
		
		With lclsFranchise
			.sCertype = sCertype
			.nBranch = nBranch
			.nProduct = nProduct
			.nPolicy = nPolicy
			.nCertif = nCertif
			.dEffecdate = dEffecdate
			.sFrandedi = sFranchiseType
			.sFrancApl = sFrancApl
			.nCurrency = nCurrency
			.nDiscount = nDiscount
			.nDisc_Amoun = nDisc_Amoun
			.nRate = nRate
			.nFixamount = nFixamount
			.nMinamount = nMinamount
			.nMaxamount = nMaxamount
			.nUsercode = nUsercode
			insPostCA015 = .Update
		End With
		
		If insPostCA015 Then
			'+ Se cambia el estado de la ventana a 'con contenido'
			If lclsPolicyWin.Add_PolicyWin(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate, nUsercode, sCodispl, "2") Then
				'+ Si se realizó un cambio al descuento
				If bChange Then
					'+ Si es una póliza matriz
					If sPolitype <> "1" And nCertif = 0 Then
						'+ Se verifica si la ventana de coberturas (CA014_A) tiene contenido
						Call lclsPolicyWin.Find_Codispl(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate, "CA014_A")
					Else
						'+ Si es individual o un certificado
						'+ Se verifica si la ventana de coberturas (CA014) tiene contenido
						Call lclsPolicyWin.Find_Codispl(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate, "CA014")
					End If
					If lclsPolicyWin.sContent = "2" Then
						'+ Se verifica que la ventana de información del recibo está conmtenida en
						'+ la secuencia de la póliza
						If lclsPolicyWin.Find_Codispl(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate, "CA017") Then
							'+ Se deja la ventana de información del recibo como requerida
							Call lclsPolicyWin.Add_PolicyWin(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate, nUsercode, "CA017", "3")
						End If
					End If
				End If
			End If
		End If
		
insPostCA015_Err: 
		If Err.Number Then
			sMessage = CStr(Err.Number) & "+" & Err.Description
			insPostCA015 = False
		End If
		
		On Error GoTo 0
		
		'UPGRADE_NOTE: Object lclsFranchise may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsFranchise = Nothing
		'UPGRADE_NOTE: Object lclsPolicyWin may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsPolicyWin = Nothing
	End Function
	
	'% Delete: Elimina un registro puntual en la pantalla CA960
	Public Function DeleteFranchise(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, ByVal nSeq As Integer, ByVal nGroup As Integer, ByVal nUsercode As Integer) As Boolean
		Dim lrecdelFranchise As eRemoteDB.Execute
		Dim lclsPolicyWin As Policy_Win
		Dim lcolFranchise As Franchises
		
		lrecdelFranchise = New eRemoteDB.Execute
		
		On Error GoTo Delete_err
		'**+ Parameter definition for stored procedure 'insudb.delSituation'
		'+Definición de parámetros para stored procedure 'insudb.delSituation'
		'**+ Information read on November 13, 2000  10:38:48 a.m.
		'+Información leída el 13/11/2000 10:38:48 a.m.
		
		With lrecdelFranchise
			.StoredProcedure = "delFranchise"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nSeq", nSeq, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nGroup", nGroup, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			
			If .Run(False) Then
				DeleteFranchise = True
				lcolFranchise = New Franchises
				If Not lcolFranchise.Find(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate, nGroup) Then
					lclsPolicyWin = New Policy_Win
					lclsPolicyWin.Add_PolicyWin(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate, nUsercode, "CA960", "1")
				End If
			End If
			
		End With
		
Delete_err: 
		If Err.Number Then
			DeleteFranchise = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecdelFranchise may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecdelFranchise = Nothing
		'UPGRADE_NOTE: Object lcolFranchise may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lcolFranchise = Nothing
		'UPGRADE_NOTE: Object lclsPolicyWin may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsPolicyWin = Nothing
	End Function
	
	'% insPreCA960: Esta función consulta la franquicia/deducible asociado a una póliza o certificado
	Public Function insPreCA960(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, ByVal nGroup As Integer) As Boolean
		
		Dim lcolFranchises As ePolicy.Franchises
		Dim lclsPolicy As ePolicy.Policy
		Dim lclsGroups As ePolicy.Groups
		
		lcolFranchises = New ePolicy.Franchises
		lclsPolicy = New ePolicy.Policy
		
		insPreCA960 = True
		On Error GoTo insPreCA960_Err
		
		Call lcolFranchises.Find(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate, nGroup)
		
		With lclsPolicy
			If .Find(sCertype, nBranch, nProduct, nPolicy) Then
				If .sPolitype <> "1" And nCertif = 0 Then
					'+ Si las coberturas son por certificado
					If .sTyp_module = "4" Or .sTyp_module = "1" Then
						If .sTyp_module = "4" Then
							Me.nError = 3932
							Me.bError = True
						End If
						insPreCA960 = False
					Else
						'+ Si la especificación es por grupo
						If .sTyp_module = "3" Then
							lclsGroups = New ePolicy.Groups
							Me.nCountGroup = lclsGroups.getCountGroups(sCertype, nBranch, nProduct, nPolicy)
							'+ Si existen grupos asociados
							If Me.nCountGroup > 0 Then
								Me.bFindGroup = True
							Else
								'+ Si no existen
								insPreCA960 = False
								'+ 3309: Grupo asegurado, no está registrado en la póliza
								Me.nError = 3887
								Me.bError = True
							End If
							'UPGRADE_NOTE: Object lclsGroups may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
							lclsGroups = Nothing
						End If
					End If
				Else
					insPreCA960 = False
				End If
			End If
		End With
		
insPreCA960_Err: 
		If Err.Number Then
			insPreCA960 = False
		End If
		
		On Error GoTo 0
		
		'UPGRADE_NOTE: Object lcolFranchises may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lcolFranchises = Nothing
	End Function
	
	'% insPostCA960: Esta función registra, modifica o elimina, según el caso, la franquicia/deducible
	'% asociada a una póliza o certificado
	Public Function insPostCA960(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, ByVal nFixamount As Double, ByVal nMaxamount As Double, ByVal nMinamount As Double, ByVal nRate As Double, ByVal nUsercode As Integer, ByVal sFrancApl As String, ByVal nCurrency As Integer, ByVal nSeq As Double, ByVal nDed_Type As Integer, ByVal nCover As Integer, ByVal nPay_Concep As Integer, ByVal nLevel As Integer, ByVal nRole As Integer, ByVal nOrder As Integer, ByVal nModulec As Integer, ByVal nGroup As Integer) As String
		
		Dim lclsPolicyWin As ePolicy.Policy_Win
		Dim bChange As Boolean
		
		On Error GoTo insPostCA960_Err
		lclsPolicyWin = New ePolicy.Policy_Win
		
		bChange = False
		
		insPostCA960 = CStr(Update_CA960(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate, nFixamount, nMaxamount, nMinamount, nRate, nUsercode, sFrancApl, nCurrency, nSeq, nDed_Type, nCover, nPay_Concep, nLevel, nRole, nOrder, nModulec, nGroup))
		
		If CBool(insPostCA960) Then
			'+ Se cambia el estado de la ventana a 'con contenido'
			Call lclsPolicyWin.Add_PolicyWin(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate, nUsercode, "CA960", "2")
		End If
		
insPostCA960_Err: 
		If Err.Number Then
			sMessage = CStr(Err.Number) & "+" & Err.Description
			insPostCA960 = CStr(False)
		End If
		
		On Error GoTo 0
		
		'UPGRADE_NOTE: Object lclsPolicyWin may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsPolicyWin = Nothing
	End Function
	
	'%insValCA960: Esta función realiza las validaciones de la ventana CA960
	Public Function insValCA960(ByVal sActions As String, ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, ByVal nFixamount As Double, ByVal nMaxamount As Double, ByVal nMinamount As Double, ByVal nRate As Double, ByVal nUsercode As Integer, ByVal sFrancApl As String, ByVal nCurrency As Integer, ByVal nSeq As Double, ByVal nDed_Type As Integer, ByVal nCover As Integer, ByVal nPay_Concep As Integer, ByVal nLevel As Integer, ByVal nRole As Integer, ByVal nOrder As Integer, ByVal nModulec As Integer, ByVal nGroup As Integer) As String
		Dim lclsPolicyWin As ePolicy.Policy_Win
		Dim lclsErrors As eFunctions.Errors
		Dim llngIndex As Integer
		Dim lexist As Object
		
		On Error GoTo insValCA960_Err
		
		lclsPolicyWin = New ePolicy.Policy_Win
		lclsErrors = New eFunctions.Errors
		
		'+ El número de deducible debe ser indicado
		If nSeq = eRemoteDB.Constants.intNull Or nSeq = 0 Then
			Call lclsErrors.ErrorMessage("CA960", 1012,  ,  , " - Nro Deducible")
		End If
		
		'+ Verificar si ya está registrado del dedudible
		If sActions <> "Update" Then
			If Exist_nSeq(sCertype, nBranch, nProduct, nPolicy, nCertif, nSeq, nGroup) Then
				Call lclsErrors.ErrorMessage("CA960", 3949)
			End If
		End If
		
		'+ El Nivel es de entrada Obligatoria
		If nLevel = eRemoteDB.Constants.intNull Or nLevel = 0 Then
			Call lclsErrors.ErrorMessage("CA960", 1012,  ,  , " - Nivel")
		End If
		
		'+ El Aplica es de entrada Obligatoria
		If sFrancApl = String.Empty Or sFrancApl = "0" Then
			Call lclsErrors.ErrorMessage("CA960", 1012,  ,  , " - Aplica")
		End If
		
		'+ El Tipo es de entrada Obligatoria
		If nDed_Type = eRemoteDB.Constants.intNull Or nDed_Type = 0 Then
			Call lclsErrors.ErrorMessage("CA960", 1012,  ,  , " - Tipo")
		End If
		
		'+Si alguno de los campos correspondientes a importes está lleno, el campo moneda
		'+debe estar lleno
		If ((nDisc_Amoun <> eRemoteDB.Constants.intNull And nDisc_Amoun <> 0) Or (nFixamount <> eRemoteDB.Constants.intNull And nFixamount <> 0) Or (nMinamount <> eRemoteDB.Constants.intNull And nMinamount <> 0) Or (nMaxamount <> eRemoteDB.Constants.intNull And nMaxamount <> 0)) And nCurrency <= 0 Then
			Call lclsErrors.ErrorMessage("CA960", 1351)
		End If
		
		
		'+Si este campo está lleno y el valor de "Tipo" es igual a "No Tiene" o el campo "Importe Fijo de Franquicia/Deducible"
		'+está lleno, su valor debe ser igual a "No Aplica"
		If sFrancApl <> String.Empty Then
			If nFixamount <> eRemoteDB.Constants.intNull Then
				If sFrancApl <> "1" Then
					Call lclsErrors.ErrorMessage("CA960", 11378)
				End If
			End If
		End If
		
		
		'+Sólo puede estar lleno uno sólo de estos dos campos
		If (nDiscount <> eRemoteDB.Constants.intNull And nDiscount <> 0) And (nDisc_Amoun <> eRemoteDB.Constants.intNull And nDisc_Amoun <> 0) Then
			Call lclsErrors.ErrorMessage("CA960", 3801)
		End If
		
		
		'+Validación del campo % e Importe fijo de Franquicia/Deducible
		'+Puede estar lleno sólo uno de estos dos campos
		If nRate <> eRemoteDB.Constants.intNull And nRate <> 0 Then
			If nFixamount <> eRemoteDB.Constants.intNull And nFixamount <> 0 Then
				Call lclsErrors.ErrorMessage("CA960", 3046)
			End If
		Else
			If (nFixamount = eRemoteDB.Constants.intNull Or nFixamount = 0) And sFrancApl <> "1" Then
				'+Debe estar lleno al menos uno de los dos campos
				Call lclsErrors.ErrorMessage("CA960", 3802)
			End If
		End If
		
		
		'+Validación del campo Condiciones-Máximo
		'+Si el campo está lleno, debe ser superior al importe mínimo de Franquicia/Deducible
		If nMaxamount <> eRemoteDB.Constants.intNull And nMinamount <> eRemoteDB.Constants.intNull And nMaxamount <> 0 And nMinamount <> 0 Then
			If nMaxamount <= nMinamount Then
				Call lclsErrors.ErrorMessage("CA960", 3803)
			End If
		End If
		
		'+ El orden debe ser indicado
		If nOrder = eRemoteDB.Constants.intNull Or nOrder = 0 Then
			Call lclsErrors.ErrorMessage("CA960", 1012,  ,  , " - Orden")
		End If
		
		'+ Verificar si ya está registrado el orden del dedudible
		If sActions <> "Update" Then
			If Exist_nOrder(sCertype, nBranch, nProduct, nPolicy, nCertif, nOrder, nGroup) Then
				Call lclsErrors.ErrorMessage("CA960", 60484)
			End If
		End If
		
		
		'+ Verificar si ya está registrada un deducible por poliza
		If nLevel = 1 Then
			'+ El Nivel de tipo póliza solo puede estar en la matriz
			If nCertif <> 0 Then
				Call lclsErrors.ErrorMessage("CA960", 100121)
			Else
				If sActions <> "Update" Then
					If Exist_Policy(sCertype, nBranch, nProduct, nPolicy, nGroup) Then
						Call lclsErrors.ErrorMessage("CA960", 100120)
					End If
				End If
			End If
		End If
		
		'+ Verificar si ya está registrada un deducible por
		If nLevel = 2 Then
			If sActions <> "Update" Then
				If Exist_Certif(sCertype, nBranch, nProduct, nPolicy, nCertif, nGroup) Then
					Call lclsErrors.ErrorMessage("CA960", 100122)
				End If
			End If
		End If
		
		insValCA960 = lclsErrors.Confirm
		
insValCA960_Err: 
		If Err.Number Then
			insValCA960 = insValCA960 & Err.Description
		End If
		
		'UPGRADE_NOTE: Object lclsPolicyWin may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsPolicyWin = Nothing
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
		
		On Error GoTo 0
	End Function
End Class






