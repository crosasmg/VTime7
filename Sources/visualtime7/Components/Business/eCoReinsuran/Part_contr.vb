Option Strict Off
Option Explicit On
Public Class Part_contr
	'%-------------------------------------------------------%'
	'% $Workfile:: Part_contr.cls                           $%'
	'% $Author:: Clobos                                     $%'
	'% $Date:: 17-05-06 23:30                               $%'
	'% $Revision:: 4                                        $%'
	'%-------------------------------------------------------%'
	'**+ Properties according the table in the system on 06/05/2001
	'+ Propiedades según la tabla en el sistema el 05/06/2001
	
	'   Column_name                 Type              Computed  Length  Prec  Scale  Nullable  TrimTrailingBlanks  FixedLenNullInSource
	Public nType_rel As Integer 'smallint        no       2       5     0     no             (n/a)               (n/a)
	Public nNumber As Integer 'smallint        no       2       5     0     no             (n/a)               (n/a)
	Public nType As Integer 'smallint        no       2       5     0     no             (n/a)               (n/a)
	Public nBranch As Integer 'smallint        no       2       5     0     no             (n/a)               (n/a)
	Public nCompany As Integer 'smallint        no       2       5     0     no             (n/a)               (n/a)
	Public nArr_perd As Integer 'smallint        no       2       5     0     yes            (n/a)               (n/a)
	Public nClasific As Integer
	Public dEffecdate As Date 'datetime        no       8                   no             (n/a)               (n/a)
	Public nComision As Double 'decimal         no       5       4     2     yes            (n/a)               (n/a)
	Public dCompdate As Date 'datetime        no       8                   no             (n/a)               (n/a)
	Public nCorredor As Integer 'int             no       4       10    0     yes            (n/a)               (n/a)
	Public dNulldate As Date 'datetime        no       8                   yes            (n/a)               (n/a)
	Public nPr_inout As Double 'decimal         no       5       4     2     yes            (n/a)               (n/a)
	Public nCl_inout As Double 'decimal         no       5       4     2     yes            (n/a)               (n/a)
	Public nRate As Double 'decimal         no       5       4     2     yes            (n/a)               (n/a)
	Public nRate_bene As Double 'decimal         no       5       4     2     yes            (n/a)               (n/a)
	Public nShare As Double 'decimal         no       5       5     2     yes            (n/a)               (n/a)
	Public nUsercode As Integer 'smallint        no       2       5     0     yes            (n/a)               (n/a)
	Public nCessprfix As Double 'decimal         no       5       5     2     yes            (n/a)               (n/a)
	Public nCessrate As Double 'decimal         no       5       5     2     yes            (n/a)               (n/a)
	Public sRoucess As String
    Public sRouProfit As String
    Public nAmountProfit As Double
    Public sFreqProfit As String
	
	'**+ Auxiliaries properties
	'+ Propiedades auxiliares
	
	Public nSel As Integer
	Public sClient As String
	Public sCliename As String
	
	Private Structure udtPart_contr
		Dim nSel As Integer
		Dim sClient As String
		Dim sCliename As String
		Dim nCompany As Integer
		Dim nClasific As Integer
		Dim nType_rel As Integer
		Dim nShare As Double
		Dim nRate As Double
		Dim nComision As Double
		Dim nArr_perd As Integer
		Dim nRate_bene As Double
		Dim nPr_inout As Double
		Dim nCl_inout As Double
		Dim nCessprfix As Double
		Dim nCessrate As Double
        Dim sRoucess As String
        Dim sRouProfit As String
        Dim nAmountProfit As Double
        Dim sFreqProfit As String
	End Structure
	
	Private arrPart_contr() As udtPart_contr
	'**% Find: This function is in charge to make the read of the Part_contr table
	'%Find: Función que realiza la lectura de la tabla Part_contr
	Public Function Find(ByVal sCodispl As String, ByVal nNumber As Integer, ByVal nType As Integer, ByVal nBranch As Integer, ByVal dEffecdate As Date, Optional ByVal nType_rel As Integer = 0) As Boolean
		Dim lrecreaPart_contr As eRemoteDB.Execute
		Dim lclsValues As eFunctions.Values
		Dim lintCount As Integer
		
		On Error GoTo Find_Err
		
		lrecreaPart_contr = New eRemoteDB.Execute
		lclsValues = New eFunctions.Values
		
		Find = True
		'**+ Parameters definition to stored procedure 'insudb.reaPart_contr'
		'+ Definición de parámetros para stored procedure 'insudb.reaPart_contr'
		'**+ Data read on 06/05/2001 12:02:48 p.m.
		'+ Información leída el 05/06/2001 12:02:48 p.m.
		
		With lrecreaPart_contr
			.StoredProcedure = "reaPart_contr"
			If sCodispl <> String.Empty Then
				.Parameters.Add("nType_rel", IIf((sCodispl = "CR301_K" Or sCodispl = "CR301_k"), 1, 2), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Else
				.Parameters.Add("nType_rel", nType_rel, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			End If
			.Parameters.Add("nNumber", nNumber, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nType", nType, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Find = .Run(True)
			If Find Then
				ReDim arrPart_contr(50)
				lintCount = 0
				Do While Not .EOF
					arrPart_contr(lintCount).nSel = CInt("3")
					arrPart_contr(lintCount).sClient = .FieldToClass("sClient")
					arrPart_contr(lintCount).sCliename = .FieldToClass("sCliename")
					arrPart_contr(lintCount).nCompany = lclsValues.StringToType(.FieldToClass("nCompany"), eFunctions.Values.eTypeData.etdInteger)
					arrPart_contr(lintCount).nClasific = lclsValues.StringToType(.FieldToClass("nClasific"), eFunctions.Values.eTypeData.etdInteger)
					arrPart_contr(lintCount).nShare = lclsValues.StringToType(.FieldToClass("nShare"), eFunctions.Values.eTypeData.etdDouble)
					arrPart_contr(lintCount).nRate = lclsValues.StringToType(.FieldToClass("nRate"), eFunctions.Values.eTypeData.etdDouble)
					arrPart_contr(lintCount).nComision = lclsValues.StringToType(.FieldToClass("nComision"), eFunctions.Values.eTypeData.etdDouble)
					arrPart_contr(lintCount).nArr_perd = lclsValues.StringToType(.FieldToClass("nArr_perd"), eFunctions.Values.eTypeData.etdInteger)
					arrPart_contr(lintCount).nRate_bene = lclsValues.StringToType(.FieldToClass("nRate_bene"), eFunctions.Values.eTypeData.etdDouble)
					arrPart_contr(lintCount).nPr_inout = lclsValues.StringToType(.FieldToClass("nPr_inout"), eFunctions.Values.eTypeData.etdDouble)
					arrPart_contr(lintCount).nCl_inout = lclsValues.StringToType(.FieldToClass("nCl_inout"), eFunctions.Values.eTypeData.etdDouble)
					arrPart_contr(lintCount).nType_rel = nType_rel
					
					arrPart_contr(lintCount).nCessprfix = lclsValues.StringToType(.FieldToClass("nCessprfix"), eFunctions.Values.eTypeData.etdDouble)
					arrPart_contr(lintCount).nCessrate = lclsValues.StringToType(.FieldToClass("nCessrate"), eFunctions.Values.eTypeData.etdDouble)
					arrPart_contr(lintCount).sRoucess = .FieldToClass("sRoucess")

                    arrPart_contr(lintCount).nAmountProfit = lclsValues.StringToType(.FieldToClass("nAmountProfit"), eFunctions.Values.eTypeData.etdDouble)
                    arrPart_contr(lintCount).sRouProfit = .FieldToClass("sRouProfit")
                    arrPart_contr(lintCount).sFreqProfit = .FieldToClass("sFreqProfit")

					lintCount = lintCount + 1
					.RNext()
				Loop 
				.RCloseRec()
				ReDim Preserve arrPart_contr(lintCount)
			Else
				Find = False
			End If
		End With
		
		'UPGRADE_NOTE: Object lrecreaPart_contr may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaPart_contr = Nothing
		
Find_Err: 
		If Err.Number Then
			Find = False
		End If
		On Error GoTo 0
	End Function
	
	'**% insValCR07: This function is in charge  to make the corresponding validates to the CR307 form.
	'%insValCR307:En esta funcion se realizan las validaciones correspondientes a la forma CR307.
	Public Function insValCR307(ByVal sCodispl As String, ByVal sCodispl_CR As String, ByVal sWindowsType As String, ByVal nNumber As Integer, ByVal nType As Integer, ByVal dEffecdate As Date, ByVal nBranch As Integer, ByVal nSel As Integer, ByVal nCompany As Integer, ByVal nShare As Double, ByVal nRate As Double, ByVal nComision As Double, ByVal nArr_perd As Integer, ByVal nRate_bene As Double, ByVal nPr_inout As Double, ByVal nCl_inout As Double, ByVal nCessrate As Double, ByVal nCessprfix As Double, ByVal sRoucess As String, ByVal sIndCessprcov As String, ByVal sIndCesscia As String) As String
		Dim lclsErrors As eFunctions.Errors
		Dim lintCount As Integer
		Dim lintCompanyAUX As Integer
		Dim ldblTotalPercent As Double
		
		lclsErrors = New eFunctions.Errors
		
		On Error GoTo insValCR307_Err
		
		lintCount = 0
		
		If nSel <> 2 Then
			
			'+Se acumulan todos los porcentajes de participación.
			If sWindowsType = "Normal" Then
				If Find(sCodispl_CR, nNumber, nType, nBranch, dEffecdate) Then
					lintCompanyAUX = 0
					For lintCount = 0 To Me.Count - 1
						If ItemCR307(lintCount) Then
							ldblTotalPercent = ldblTotalPercent + IIf(nSel <> 2, Me.nShare, 0)
						End If
					Next 
				End If
			End If
			
			'+Validación campo compañía
			If nCompany <> 0 And nCompany <> eRemoteDB.Constants.intNull Then
				
				'+Validación del Compañía (No puede ser la Compañía Lider "1")
				'+Se deja como advertencia que se esta usando la compañia lider en el reaseguro
				'                If reaOpt_system_v(nCompany) Then
				'                    Call lclsErrors.ErrorMessage(sCodispl, 6094)
				'                    Call lclsErrors.ErrorMessage(sCodispl, 100119)
				'                End If
				
				'+Validación de los campos Traspaso/Primas
				If ((nPr_inout <> 0 And nPr_inout <> eRemoteDB.Constants.intNull) And (nCl_inout = 0 Or nCl_inout = eRemoteDB.Constants.intNull)) Or ((nPr_inout = 0 Or nPr_inout = eRemoteDB.Constants.intNull) And (nCl_inout <> 0 And nCl_inout <> eRemoteDB.Constants.intNull)) Then
					Call lclsErrors.ErrorMessage(sCodispl, 6106)
				End If
			Else
				If Not sWindowsType = "Normal" Then
					Call lclsErrors.ErrorMessage(sCodispl, 1084)
				End If
			End If
		End If
		
		'+Validación de la suma de todos los porcentajes de las compañías participantes debe ser 100%.
		If sWindowsType = "Normal" Then
			If ldblTotalPercent <> 100 And ldblTotalPercent <> 0 Then
				Call lclsErrors.ErrorMessage(sCodispl, 6050)
			End If
		End If
		
		If sWindowsType = "PopUp" And nSel <> 2 Then
			If Find(sCodispl_CR, nNumber, nType, nBranch, dEffecdate) Then
				lintCompanyAUX = nCompany
				For lintCount = 0 To Me.Count - 1
					If ItemCR307(lintCount) Then
						If lintCompanyAUX = Me.nCompany Then
							Call lclsErrors.ErrorMessage(sCodispl, 1927)
							Exit For
						End If
					End If
				Next 
			End If
		End If
		
		'+Validación del campo Participación en contrato
		If sWindowsType = "PopUp" Then
			If nShare = 0 Or nShare = eRemoteDB.Constants.intNull Then
				Call lclsErrors.ErrorMessage(sCodispl, 6121)
			Else
				If nShare > 100 Then
					Call lclsErrors.ErrorMessage(sCodispl, 6122)
				End If
			End If
		End If

        'PRY-REASEGUROS VT - EVITA QUE SE ASIGNE UNA CIA DE REASEGURO EN CONTRATO  - RAOD - INI
        '+Validación del ingreso de algun tipo de calculo de cesion cuando solo es por cía

        'If sWindowsType = "PopUp" Then
        '	If (sIndCesscia = "1") And (sIndCessprcov = String.Empty Or sIndCessprcov = "") Then

        '		If (nCessrate = 0 Or nCessrate = eRemoteDB.Constants.intNull) And (nCessprfix = 0 Or nCessprfix = eRemoteDB.Constants.intNull) And (sRoucess = "" Or sRoucess = String.Empty) Then
        '			Call lclsErrors.ErrorMessage(sCodispl, 300005)
        '		End If
        '	End If
        'End If
        'PRY-REASEGUROS VT - EVITA QUE SE ASIGNE UNA CIA DE REASEGURO EN CONTRATO    - RAOD - FIN
        insValCR307 = lclsErrors.Confirm
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
		
insValCR307_Err: 
		If Err.Number Then
			insValCR307 = insValCR307 & Err.Description
		End If
		On Error GoTo 0
	End Function
	'**% reaOpt_system_v: This function is in charge to verify if exist the leader company
	'%reaOpt_system_v: función que verifica si la companía lider existe
	Function reaOpt_system_v(ByRef lintCompany As Integer) As Boolean
		Dim lrecreaOpt_System As eRemoteDB.Execute
		
		lrecreaOpt_System = New eRemoteDB.Execute
		
		On Error GoTo reaOpt_system_v_Err
		
		reaOpt_system_v = False
		'**+ Parameters definition to stored procedure 'insudb.reaOpt_System'
		'+ Definición de parámetros para stored procedure 'insudb.reaOpt_System'
		'**+ Data read on 06/05/2001 02:23:42 p.m.
		'+ Información leída el 05/06/2001 02:23:40 p.m.
		
		With lrecreaOpt_System
			.StoredProcedure = "reaOpt_System"
			If .Run Then
				If .FieldToClass("nCompany") = lintCompany Then
					reaOpt_system_v = True
				End If
			End If
		End With
		
		'UPGRADE_NOTE: Object lrecreaOpt_System may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaOpt_System = Nothing
		
reaOpt_system_v_Err: 
		If Err.Number Then
			reaOpt_system_v = False
		End If
	End Function
	
	Public ReadOnly Property Count() As Integer
		Get
			Count = UBound(arrPart_contr)
		End Get
	End Property
	
	Public Function ItemCR307(ByVal lintIndex As Integer) As Boolean
		If lintIndex <= UBound(arrPart_contr) Then
			With arrPart_contr(lintIndex)
				nSel = .nSel
				sClient = .sClient
				sCliename = .sCliename
				nCompany = .nCompany
				nClasific = .nClasific
				nType_rel = .nType_rel
				nShare = .nShare
				nRate = .nRate
				nComision = .nComision
				nArr_perd = .nArr_perd
				nRate_bene = .nRate_bene
				nPr_inout = .nPr_inout
				nCl_inout = .nCl_inout
				nCessprfix = .nCessprfix
				nCessrate = .nCessrate
                sRoucess = .sRoucess
                sRouProfit = .sRouProfit
                nAmountProfit = .nAmountProfit
                sFreqProfit = .sFreqProfit
			End With
			ItemCR307 = True
		Else
			ItemCR307 = False
		End If
	End Function
	'**% Update: This routine is in charge to update the data of the  participants companies
	'%Update: Esta rutina se encarga de actualizar los datos de las compañías participantes
	'%en el contrato de reaseguro
	Public Function Update(ByRef lintAction As Object) As Boolean
		Dim lrecinsPart_contr As eRemoteDB.Execute
		Dim lrecDelPart_contr As eRemoteDB.Execute
		
		lrecinsPart_contr = New eRemoteDB.Execute
		lrecDelPart_contr = New eRemoteDB.Execute
		
		Update = True
		
		On Error GoTo Update_Err
		
		'**+ Parameters definition to stored procedure 'insudb.insPart_contr'
		'+ Definición de parámetros para stored procedure 'insudb.insPart_contr'
		'**+ Data read on 06/06/2001 05:29:16 p.m.
		'+ Información leída el 06/06/2001 05:29:16 p.m.
		
		If lintAction = 3 Then
			With lrecinsPart_contr
				.StoredProcedure = "insPart_contr"
				.Parameters.Add("nType_rel", nType_rel, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nNumber", nNumber, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nType", nType, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nCompany", nCompany, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nArr_perd", nArr_perd, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nComision", nComision, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 4, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nPr_InOut", nPr_inout, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 4, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nCl_InOut", nCl_inout, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 4, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nRate", nRate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 4, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nRate_bene", nRate_bene, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 4, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nShare", nShare, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nCessrate", nCessrate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 4, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nCessprfix", nCessprfix, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("sRoucess", sRoucess, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("sRouProfit", sRouProfit, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nAmountProfit", nAmountProfit, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 18, 6, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("sFreqProfit", sFreqProfit, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				Update = .Run(False)
			End With
			'**+ If the action is delete
			'+Si la acción es eliminar
		ElseIf lintAction = 2 Then 
			
			'**+ Parameters definition to stored procedure 'insudb.DelPart_contr'
			'+ Definición de parámetros para stored procedure 'insudb.DelPart_contr'
			'**+ Data read on 06/06/2001 1:48:30 p.m.
			'+ Información leída el 06/06/2001 05:48:30 p.m.
			
			With lrecDelPart_contr
				.StoredProcedure = "DelPart_contr"
				.Parameters.Add("nType_rel", nType_rel, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nNumber", nNumber, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nType", nType, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nCompany", nCompany, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				Update = .Run(False)
			End With
		End If
		'UPGRADE_NOTE: Object lrecinsPart_contr may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecinsPart_contr = Nothing
		'UPGRADE_NOTE: Object lrecDelPart_contr may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecDelPart_contr = Nothing
		
Update_Err: 
		If Err.Number Then
			Update = False
		End If
		On Error GoTo 0
	End Function
	'**% insPostCR307: This function is in charge to make the updates in the
	'**% different involves tables
	'%insPostCR307: Esta función se encarga de realizar las actualizaciones en las
	'%diferentes tablas involucradas
    Public Function insPostCR307(ByVal sCodispl As String, ByVal sCodispl_CR As String, ByVal nSel As Integer, ByVal nUsercode As Integer, ByVal dEffecdate As Date, ByVal nNumber As Integer, ByVal nContraType As Integer, ByVal nBranch As Integer, ByVal nCompany As Integer, ByVal nShare As Double, ByVal nRate As Double, ByVal nComision As Double, ByVal nArr_perd As Integer, ByVal nRate_bene As Double, ByVal nPr_inout As Double, ByVal nCl_inout As Double, ByVal nCessrate As Double, ByVal nCessprfix As Double, ByVal sRoucess As String, ByVal sRouProfit As String, ByVal nAmountProfit As Double, ByVal sFreqProfit As String) As Boolean

        On Error GoTo insPostCR307_Err

        insPostCR307 = True
        '**+ If the selected option is Consult
        '+Si la opción seleccionada es Consultar

        With Me
            If UCase(sCodispl_CR) = "CR301_K" Then
                .nType_rel = 1
            ElseIf sCodispl_CR = "CR304_K" Then
                .nType_rel = 2
            End If
            .dEffecdate = dEffecdate
            .nBranch = nBranch
            .nNumber = nNumber
            .nType = nContraType
            .nCompany = nCompany
            .nShare = nShare
            .nRate = nRate
            .nComision = nComision
            .nArr_perd = nArr_perd
            .nRate_bene = nRate_bene
            .nPr_inout = nPr_inout
            .nCl_inout = nCl_inout
            .nCessprfix = nCessprfix
            .nCessrate = nCessrate
            .sRoucess = sRoucess
            .nUsercode = nUsercode
            .sRouProfit = sRouProfit
            .nAmountProfit = nAmountProfit
            .sFreqProfit = sFreqProfit
            If nCompany <> 0 And nCompany <> eRemoteDB.Constants.intNull Then
                If nSel = 3 Then
                    insPostCR307 = .Update(3)
                ElseIf nSel = 2 Then
                    insPostCR307 = .Update(2)
                End If
            End If
        End With


insPostCR307_Err:
        If Err.Number Then
            insPostCR307 = False
        End If
    End Function
End Class






