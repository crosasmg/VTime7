Option Strict Off
Option Explicit On
Public Class Tab_modul
	'%-------------------------------------------------------%'
	'% $Workfile:: Tab_modul.cls                            $%'
	'% $Author:: Nvaplat15                                  $%'
	'% $Date:: 10/11/04 15.13                               $%'
	'% $Revision:: 20                                       $%'
	'%-------------------------------------------------------%'
	
	'**- Properties accourding to the tablñe in the system on 11/30/2000
	'- Propiedades según la tabla en el sistema al 30/11/2000.
	
	'+ Column_name           Type
	'------------------------------------------------------
	Public nBranch As Integer 'smallint
	Public nModulec As Integer 'smallint
	Public nProduct As Integer 'smallint
	Public dEffecdate As Date 'datetime
	Public sChanallo As String 'char
	Public sDefaulti As String 'char
	Public sDescript As String 'char
	Public sRequire As String 'char
	Public sShort_des As String 'char
	Public nUsercode As Integer 'smallint
	Public sCondSVS As String
	Public nPremirat As Double
	Public nChPreLev As Double
	Public nRatePreAdd As Double
	Public nRatePreSub As Double
	Public sChangetyp As String
	Public styp_rat As String
	Public sVigen As String 'char
	
	'- Variable para identificar si el módulo se encuentra asociado en otras tablas
	Public sExists As String
	
	'%Find: Lee los datos de un registro de la tabla
	Public Function Find(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nModulec As Integer, ByVal dEffecdate As Date) As Object
		Dim lrecReaTab_modul_o As eRemoteDB.Execute
		
		On Error GoTo Find_Err
		
		lrecReaTab_modul_o = New eRemoteDB.Execute
		
		'+Definición de parámetros para stored procedure 'ReaTab_modul_o'
		'+Información leída el 12/11/01
		With lrecReaTab_modul_o
			.StoredProcedure = "ReaTab_modul_o"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nModulec", nModulec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				Find = True
				nBranch = .FieldToClass("nBranch")
				nModulec = .FieldToClass("nModulec")
				nProduct = .FieldToClass("nProduct")
				dEffecdate = .FieldToClass("dEffecdate")
				sChanallo = .FieldToClass("sChanallo")
				sDefaulti = .FieldToClass("sDefaulti")
				sDescript = .FieldToClass("sDescript")
				sRequire = .FieldToClass("sRequire")
				sShort_des = .FieldToClass("sShort_des")
				nUsercode = .FieldToClass("nUsercode")
				nPremirat = .FieldToClass("npremirat")
				nChPreLev = .FieldToClass("nchprelev")
				nRatePreAdd = .FieldToClass("nratepreadd")
				nRatePreSub = .FieldToClass("nratepresub")
				sChangetyp = .FieldToClass("schangetyp")
				styp_rat = .FieldToClass("styp_rat")
				sVigen = .FieldToClass("sVigen")
				.RCloseRec()
			End If
		End With
		
Find_Err: 
		If Err.Number Then
			Find = False
		End If
		'UPGRADE_NOTE: Object lrecReaTab_modul_o may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecReaTab_modul_o = Nothing
		On Error GoTo 0
	End Function
	
	'**% insValDP032: Makes the validation of the fields that has to be updated in the frame (window) DP032
	'% insValDP032: Realiza la validación de los campos a actualizar en el frame (ventana) DP032
	'%              (Módulos de un producto)
	Public Function insValDP032(ByVal sWindowType As String, ByVal sAction As String, ByVal nCountRecord As Integer, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nModulec As Integer, ByVal sDescript As String, ByVal sShort_des As String, ByVal dEffecdate As Date, ByVal sRequire As String, ByVal sDefaulti As String, ByVal sChanallo As String, ByVal nPremirat As Double, ByVal nChPreLev As Double, ByVal nRatePreAdd As Double, ByVal nRatePreSub As Double, ByVal sChangetyp As String, ByVal styp_rat As String) As String
		Dim lclsErrors As eFunctions.Errors
		Dim lclsTab_modul As Tab_modul
		
		On Error GoTo insValDP032_Err
		
		lclsErrors = New eFunctions.Errors
		
		With lclsErrors
			If sWindowType = "PopUp" Then
				'+ Si el código del módulo está vacío, ninguno de los campos de la línea puede estar lleno
				If nModulec = eRemoteDB.Constants.intNull Then
					Call .ErrorMessage("DP032", 12165)
					If sDescript <> String.Empty Or sShort_des <> String.Empty Or sRequire <> String.Empty Or sDefaulti <> String.Empty Or sChanallo <> String.Empty Then
						Call .ErrorMessage("DP032", 1084)
					End If
				Else
					'+ Si la línea contiene un nuevo módulo, éste no debe estar registrada en el archivo
					'+ de módulos de un producto
					If sAction <> "Update" Then
						lclsTab_modul = New Tab_modul
						If lclsTab_modul.Find(nBranch, nProduct, nModulec, dEffecdate) Then
							Call .ErrorMessage("DP032", 11116)
						End If
					End If
					
					'+ La descripción debe estar llena
					If sDescript = String.Empty Then
						Call .ErrorMessage("DP032", 10010)
					End If
					
					'+ La descripción abreviada debe estar llena
					If sShort_des = String.Empty Then
						Call .ErrorMessage("DP032", 10011)
					End If
					If styp_rat = "1" And nPremirat <= 0 Then
						Call .ErrorMessage("DP032", 2042)
					End If
				End If
			Else
				'+ Debe existir por lo menos un registro
				If nCountRecord = 0 Then
					Call .ErrorMessage("DP032", 1928)
				End If
			End If
			
			insValDP032 = .Confirm
		End With
		
insValDP032_Err: 
		If Err.Number Then
			insValDP032 = "insValDP032: " & Err.Description
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
		'UPGRADE_NOTE: Object lclsTab_modul may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsTab_modul = Nothing
	End Function
	
	'**% insPostDP032: This routine has the objetive of update in the class the values, which
	'**%               are going to be use to update the respective table
	'% insPostDP032: Esta rutina tiene la finalidad de actualizar en la clase los valores con los
	'%               cuales se hará la actualización en las tablas respectivas
	Public Function insPostDP032(ByVal sAction As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nModulec As Integer, ByVal dEffecdate As Date, ByVal sChanallo As String, ByVal sDefaulti As String, ByVal sRequire As String, ByVal sDescript As String, ByVal sShort_des As String, ByVal nUsercode As Integer, ByVal sCondSVS As String, ByVal nPremirat As Double, ByVal nChPreLev As Double, ByVal nRatePreAdd As Double, ByVal nRatePreSub As Double, ByVal sChangetyp As String, ByVal styp_rat As String, ByVal sVigen As String) As Boolean
		Dim nType As Integer
		Dim lclsProd_win As eProduct.Prod_win
		Dim lcolTab_moduls As eProduct.Tab_moduls
		
		On Error GoTo insPostDP032_err
		
		lclsProd_win = New eProduct.Prod_win
		lcolTab_moduls = New eProduct.Tab_moduls
		
		If sAction = "Update" Or sAction = "Add" Then
			nType = 1
		Else
			nType = 2
		End If
		
		sChanallo = IIf(sChanallo = String.Empty, "2", "1")
		sDefaulti = IIf(sDefaulti = String.Empty, "2", "1")
		styp_rat = IIf(styp_rat = String.Empty, "2", "1")
		sRequire = IIf(sRequire = String.Empty, "2", "1")
		sVigen = IIf(sVigen = String.Empty, "2", "1")
		
		insPostDP032 = Update(nType, nBranch, nProduct, nModulec, dEffecdate, sChanallo, sDefaulti, sRequire, sDescript, sShort_des, nUsercode, sCondSVS, nPremirat, nChPreLev, nRatePreAdd, nRatePreSub, sChangetyp, styp_rat, sVigen)
		
		If insPostDP032 Then
			If lcolTab_moduls.Find(nBranch, nProduct, dEffecdate) Then
				insPostDP032 = lclsProd_win.Add_Prod_win(nBranch, nProduct, dEffecdate, "DP032", "2", nUsercode)
			Else
				insPostDP032 = lclsProd_win.Add_Prod_win(nBranch, nProduct, dEffecdate, "DP032", "1", nUsercode)
			End If
			insPostDP032 = lclsProd_win.Add_Prod_win(nBranch, nProduct, dEffecdate, "DP033", "3", nUsercode)
		End If
		
insPostDP032_err: 
		If Err.Number Then
			insPostDP032 = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lclsProd_win may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsProd_win = Nothing
		'UPGRADE_NOTE: Object lcolTab_moduls may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lcolTab_moduls = Nothing
	End Function
	'**%Update: Updates records in the table "Tab_modul".
	'%Update: Este método se encarga de actualizar registros en la tabla "Tab_modul".
	Private Function Update(ByVal nType As Integer, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nModulec As Integer, ByVal dEffecdate As Date, ByVal sChanallo As String, ByVal sDefaulti As String, ByVal sRequire As String, ByVal sDescript As String, ByVal sShort_des As String, ByVal nUsercode As Integer, ByVal sCondSVS As String, ByVal nPremirat As Double, ByVal nChPreLev As Double, ByVal nRatePreAdd As Double, ByVal nRatePreSub As Double, ByVal sChangetyp As String, ByVal styp_rat As String, ByVal sVigen As String) As Boolean
		'- Se define la variable lclsTab_modul
		Dim lrecTab_modul As eRemoteDB.Execute
		
		On Error GoTo Update_Err
		
		lrecTab_modul = New eRemoteDB.Execute
		
		'+ Definición de parámetros para stored procedure 'insudb.insTab_modul'
		'+ Información leída el 17/04/2001 02:23:02 p.m.
		
		With lrecTab_modul
			.StoredProcedure = "insTab_modul"
			.Parameters.Add("nType", nType, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nModulec", nModulec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sChanallo", sChanallo, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sDefaulti", sDefaulti, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sRequire", sRequire, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sDescript", sDescript, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 30, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sShort_des", sShort_des, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sCondSVS", sCondSVS, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 30, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("npremirat", nPremirat, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 9, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nchprelev", nChPreLev, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nratepreadd", nRatePreAdd, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 6, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nratepresub", nRatePreSub, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 6, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("schangetyp", sChangetyp, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("styp_rat", styp_rat, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sVigen", sVigen, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			
			Update = .Run(False)
		End With
		
Update_Err: 
		If Err.Number Then
			Update = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecTab_modul may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecTab_modul = Nothing
	End Function
	'% valModul_Cover: Se verifica que el módulo no tenga coberturas asociadas
	Public Function valModul_Cover(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nModulec As Integer, ByVal dEffecdate As Date, ByVal sBrancht As String) As Boolean
		Dim lcolGen_covers As Gen_covers
		Dim lcolLife_covers As Life_covers
		
		On Error GoTo valModul_Cover_Err
		
        'If sBrancht = CStr(Product.pmBrancht.pmlife) Or sBrancht = CStr(Product.pmBrancht.pmNotTraditionalLife) Then
        If (sBrancht = CStr(Product.pmBrancht.pmlife) Or sBrancht = CStr(Product.pmBrancht.pmNotTraditionalLife) Or sBrancht = CStr(Product.pmBrancht.pmMedicalAtention)) Then

            lcolLife_covers = New Life_covers
            valModul_Cover = lcolLife_covers.Find(nBranch, nProduct, nModulec, dEffecdate)
        Else
            lcolGen_covers = New Gen_covers
            valModul_Cover = lcolGen_covers.valModuleGen_cover(nBranch, nProduct, nModulec, dEffecdate)
        End If

valModul_Cover_Err:
        If Err.Number Then
            valModul_Cover = False
        End If
        On Error GoTo 0
        'UPGRADE_NOTE: Object lcolGen_covers may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lcolGen_covers = Nothing
        'UPGRADE_NOTE: Object lcolLife_covers may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lcolLife_covers = Nothing
	End Function
End Class






