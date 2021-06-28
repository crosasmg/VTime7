Option Strict Off
Option Explicit On
Public Class Life_load
	'**+Objective: Class that supports the table Life_load
	'**+           it's content is: Loadings for life products  A record per every load associated with a life product
	'**+Version: $$Revision: 5 $
	'+Objetivo: Clase que le da soporte a la tabla Life_load
	'+          cuyo contenido es: Cargos de vida  Un registro por cada cargo asociado a un producto de vida
	'+Version: $$Revision: 5 $
	
	'**-Objective: Code of the Line of Business. The possible values as per table 10.
	'-Objetivo: Código del ramo comercial. Valores posibles según tabla 10.
	Public nBranch As Integer
	
	'**-Objective: Code of the product.
	'-Objetivo: Código del producto.
	Public nProduct As Integer
	
	'**-Objective: Code of Current account Load
	'-Objetivo: Código del cargo a cuenta corriente
	Public nLoad_cod As Integer
	
	'**-Objective: Date which from the record is valid.
	'-Objetivo: Fecha de efecto del registro.
	Public dEffecdate As Date
	
	'**-Objective: Complete name of the loading or charge.
	'-Objetivo: Descripción completa del cargo
	Public sDescript As String
	
	'**-Objective: Type of load  Sole values as per table 7996
	'-Objetivo: Tipo de cargo  Valores únios según tabla 7996
	Public nLoad_type As Integer
	
	'**-Objective: Amount of administrative expenses to be charged to the insured
	'-Objetivo: Importe de gastos administrativos a cargo del asegurado
	Public nLoadAmo As Double
	
	'**-Objective: Percentage of administrative expenses to be charged to the insured
	'-Objetivo: Porcentaje de gastos administrativos a cargo del asegurado
	Public nloadRate As Double
	
	'**-Objective: Date when the record is cancelled.
	'-Objetivo: Fecha de anulación del registro.
	Public dNulldate As Date
	
	'**-Objective: General status of the record. Sole values as per table 26.
	'-Objetivo: Estado general del registro. Valores únicos según tabla 26.
	Public sStatregt As String
	
	'**-Objective: Code of the user creating or updating the record.
	'-Objetivo: Código del usuario que crea o actualiza el registro.
	Public nUsercode As Integer
	
	'**-Objective: Abbreviated description of the charge
	'-Objetivo: Descripción abreviada del cargo
	Public sShort_des As String
	
	'**-Objective: Payment frecuency of the premium. Sole values as per table 36.
	'-Objetivo: Frecuencia de pago de la prima. Valores únicos según tabla 36.
	Public nPayFreq As Integer 'smallint 2      5    0     yes      (n/a)              (n/a)
	
	'**-Objective: Code of the routine to be used to calculate the cost
	'-Objetivo: Rutina de cálculo del cargo
	Public sRoutine As String 'char     12                yes      yes                yes
	
	'**-Objective: Indicates the charge has to be applied previous to the investment. Sole values:    1 - Affirmative    2 - Negative.
	'-Objetivo: Indicador de cargo a ser aplicado previo a realizar la inversión. Valores únicos:    1 - Afirmativo    2 - Negativo
	Public sPreInv As String 'char     1                 yes      yes                yes
	
	'**-Objective: Type of current account movement Sole values as per table 401
	'-Objetivo: Tipo de movimiento de cuenta corriente Valores únicos según tabla 401
	Public nType_Move As Integer 'smallint 2      5    0     yes      (n/a)              (n/a)
	
	'- [APV2] HAD 1021 – Cambios en la lógica de descuento de los costos coberturas. DBLANCO 03-09-2003
	'- Objetivo: Indicador de si la primera vez que se aplica el cargo, el mismo debe ser prorrateado desde la fecha de emisión hasta el fin de mes.
	Public sFirst_cost_pro As String
	
	'- Objetivo: Código del módulo de cobertura
	Public nModulec As Integer
	
	'- Objetivo: Código de la cobertura asociada al cargo
	Public nCover As Integer
	
	'+ [APV2] HAD 1023. DP064 - Cargos
	'- Indicador de cargo afecto a impuesto
	Public sTaxin As String
	'- Propiedad auxiliar. Valor del indicador "suma para impuesto" asociado
	'-  a la cobertura indicada
	Public sAddTaxin As String
	Public nMonthi As Integer
	Public nMonthe As Integer
	Public nAply As Integer
	Public nOriAply As Integer
	Public sRetro As String
	Public sInstallind As String
	'-Permite indicar que el cargo afecta la prima básica (CA014)
	Public sPremBas As String
	
	'- Objetivo: Código del fondo de inversión sobre el que aplica el cargo
	Public nFunds As Integer
	
	
	'**- Variable that contains the status of the instance
	'- Variable que contiene el estado de la instancia
	
	'**-Objective:
	'-Objetivo:
	Public nStatusInstance As Integer
    Public sFirst_apply As String
    Public nIndex_table As Integer


    Public nMinimumAmount As Double
    Public nMaximumAmount As Double

	'**- Declare the defined type to wich the arrengement that will contain the
	'**- data brought from the table will be associated
	'- Se declara el tipo definido al que se le asociará el arreglo que contendrá
	'- los datos traídos de la tabla
	
	Private Structure typLife_load
		Dim nStatusInstance As Integer
		Dim nBranch As Integer
		Dim nProduct As Integer
		Dim nLoad_cod As Integer
		Dim dEffecdate As Date
		Dim nLoad_type As Integer
		Dim sDescript As String
		Dim nLoadAmo As Double
        Dim nloadRate As Double
        Dim nMinimumAmount As Double
        Dim nMaximumAmount As Double
		Dim dNulldate As Date
		'UPGRADE_WARNING: Fixed-length string size must fit in the buffer. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="3C1E4426-0B80-443E-B943-0627CD55D48B"'
		<VBFixedString(1),System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.ByValArray,SizeConst:=1)> Public sStatregt() As Char
		'UPGRADE_WARNING: Fixed-length string size must fit in the buffer. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="3C1E4426-0B80-443E-B943-0627CD55D48B"'
		<VBFixedString(12),System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.ByValArray,SizeConst:=12)> Public sShort_des() As Char
		Dim nPayFreq As Integer
		'UPGRADE_WARNING: Fixed-length string size must fit in the buffer. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="3C1E4426-0B80-443E-B943-0627CD55D48B"'
		<VBFixedString(12),System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.ByValArray,SizeConst:=12)> Public sRoutine() As Char
		'UPGRADE_WARNING: Fixed-length string size must fit in the buffer. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="3C1E4426-0B80-443E-B943-0627CD55D48B"'
		<VBFixedString(1),System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.ByValArray,SizeConst:=1)> Public sPreInv() As Char
		Dim nType_Move As Integer
		Dim nMonthi As Integer
		Dim nMonthe As Integer
		Dim nAply As Integer
	End Structure
	
	'**-Objective:
	'-Objetivo:
	Private ludtLife_load() As typLife_load
	
	'**-Objective:
	'-Objetivo:
	Private lintCount As Integer
	
	'% Find: realiza la lectura de los datos de la tabla
	Public Function Find(ByVal nBranch1 As Integer, ByVal nProduct1 As Integer, ByVal nLoad_cod1 As Integer, ByVal nMonthi As Integer, ByVal dEffecDate1 As Date, Optional ByVal sStatregt As String = "", Optional ByVal lblnFind As Boolean = False) As Boolean
		On Error GoTo ErrorHandler
		Find = False
		Dim lrecreaLife_load As eRemoteDB.Execute
		If nBranch1 <> nBranch Or nProduct1 <> nProduct Or nLoad_cod1 <> nLoad_cod Or dEffecDate1 <> dEffecdate Or lblnFind Then
			
			
			lrecreaLife_load = New eRemoteDB.Execute
			
			With lrecreaLife_load
				.StoredProcedure = "reaLife_load"
				.Parameters.Add("nBranch", nBranch1, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nProduct", nProduct1, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nLoad_cod", nLoad_cod1, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nMonthi", nMonthi, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("dEffecdate", dEffecDate1, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				
				If sStatregt = String.Empty Then
					'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
					.Parameters.Add("sStatregt", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				Else
					.Parameters.Add("sStatregt", sStatregt, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				End If
				If .Run Then
					nBranch = .FieldToClass("nBranch")
					nProduct = .FieldToClass("nProduct")
					nLoad_cod = .FieldToClass("nLoad_cod")
					dEffecdate = .FieldToClass("dEffecDate")
					nLoad_type = .FieldToClass("nLoad_type")
					sDescript = .FieldToClass("sDescript")
					nLoadAmo = .FieldToClass("nLoadAmo")
                    nloadRate = .FieldToClass("nLoadRate")
					dNulldate = .FieldToClass("dNullDate")
					sStatregt = .FieldToClass("sStatregt")
					sShort_des = .FieldToClass("sShort_des")
					nPayFreq = .FieldToClass("nPayFreq")
					sRoutine = .FieldToClass("sRoutine")
					sPremBas = .FieldToClass("sPremBas")
					
					sPreInv = .FieldToClass("sPreInv")
					nType_Move = .FieldToClass("nType_move")
					
					'+ [APV2] HAD 1021 – Cambios en la lógica de descuento de los costos coberturas. DBLANCO 03-09-2003
					sFirst_cost_pro = .FieldToClass("sFirst_cost_pro")
					nModulec = .FieldToClass("nModulec")
					nCover = .FieldToClass("nCover")
					
					'+ [APV2] HAD 1023. DP064 - Cargos
					sTaxin = .FieldToClass("sTaxin")
					nMonthi = .FieldToClass("nMonthi")
					nMonthe = .FieldToClass("nMonthe")
					nAply = .FieldToClass("nAply")

                    sFirst_apply = .FieldToClass("sFirst_apply")
                    nIndex_table = .FieldToClass("nIndex_table")

                    nMinimumAmount = .FieldToClass("nMinimumAmount")
                    nMaximumAmount = .FieldToClass("nMaximumAmount")


					Find = True
					.RCloseRec()
				End If
			End With
			'UPGRADE_NOTE: Object lrecreaLife_load may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
			lrecreaLife_load = Nothing
			
		Else
			Find = True
		End If
		
		Exit Function
ErrorHandler: 
		'UPGRADE_NOTE: Object lrecreaLife_load may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaLife_load = Nothing
		Find = False
	End Function
	
	'**%Objective: This function is in charge of adding/updating the information in treatment of the
	'**%           main table for the transaction.
	'%Objetivo: Esta función se encarga de agregar/actualizar la información en tratamiento de la
	'%           tabla principal para la transacción.
	Public Function Update() As Boolean
		On Error GoTo ErrorHandler
		Update = False
		
		Dim lrecupdLife_load As eRemoteDB.Execute
		
		lrecupdLife_load = New eRemoteDB.Execute
		
		With lrecupdLife_load
			.StoredProcedure = "insLife_load"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nLoad_cod", nLoad_cod, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nLoad_type", nLoad_type, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sDescript", sDescript, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 30, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nLoadAmo", nLoadAmo, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nLoadRate", nloadRate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 5, 8, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dNulldate", dNulldate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sStatregt", sStatregt, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sShort_des", sShort_des, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPayFreq", nPayFreq, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sRoutine", sRoutine, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sPreInv", sPreInv, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nType_move", nType_Move, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sFirst_cost_pro", sFirst_cost_pro, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nModulec", nModulec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCover", nCover, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sTaxin", sTaxin, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nMonthi", nMonthi, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nMonthe", nMonthe, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAply", nAply, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nOriAply", nOriAply, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sRetro", sRetro, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sPremBas", sPremBas, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nFunds", nFunds, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sInstallind", sInstallind, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sFirst_apply", sFirst_apply, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nIndex_table", nIndex_table, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nMinimumAmount", nMinimumAmount, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nMaximumAmount", nMaximumAmount, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

			Update = .Run(False)
			
		End With
		'UPGRADE_NOTE: Object lrecupdLife_load may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecupdLife_load = Nothing
		
		Exit Function
ErrorHandler: 
		'UPGRADE_NOTE: Object lrecupdLife_load may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecupdLife_load = Nothing
		Update = False
	End Function
	
	'**%Objective: creates a product life change
	'%Objetivo:  Crea un cargo de producto de vida
	Public Function Add() As Boolean
		
		Dim lreccreLife_load As eRemoteDB.Execute
		
		On Error GoTo ErrorHandler
		Add = False
		
		lreccreLife_load = New eRemoteDB.Execute
		
		With lreccreLife_load
			.StoredProcedure = "creLife_load"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nLoad_cod", nLoad_cod, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nLoad_type", nLoad_type, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sDescript", sDescript, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 30, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nLoadAmo", nLoadAmo, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nLoadRate", nloadRate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 4, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			'UPGRADE_WARNING: IsEmpty was upgraded to IsNothing and has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
			If IsNothing(dNulldate) Then
				'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
				.Parameters.Add("dNulldate", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Else
				.Parameters.Add("dNulldate", dNulldate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			End If
			.Parameters.Add("sStatregt", sStatregt, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sShort_des", sShort_des, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPayFreq", nPayFreq, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sRoutine", sRoutine, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sPreInv", sPreInv, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nType_move", nType_Move, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nMonthi", nMonthi, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nMonthe", nMonthe, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAply", nAply, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            .Parameters.Add("nMinimumAmount", nMinimumAmount, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nMaximumAmount", nMaximumAmount, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Add = .Run(False)
			
		End With
		'UPGRADE_NOTE: Object lreccreLife_load may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lreccreLife_load = Nothing
		
		Exit Function
ErrorHandler: 
		'UPGRADE_NOTE: Object lreccreLife_load may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lreccreLife_load = Nothing
		Add = False
	End Function
	
	'**%Objective: deletes a life product charge
	'%Objetivo:  elimina un cargo de producto de vida
	Public Function Delete() As Boolean
		On Error GoTo ErrorHandler
		Delete = False
		
		Dim lrecdelLife_load As eRemoteDB.Execute
		
		lrecdelLife_load = New eRemoteDB.Execute
		
		With lrecdelLife_load
			.StoredProcedure = "insDelLife_load"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nLoad_cod", nLoad_cod, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nMonthi", nMonthi, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			Delete = .Run(False)
			
		End With
		'UPGRADE_NOTE: Object lrecdelLife_load may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecdelLife_load = Nothing
		
		Exit Function
ErrorHandler: 
		'UPGRADE_NOTE: Object lrecdelLife_load may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecdelLife_load = Nothing
		Delete = False
	End Function
	
	'**%Objective: This function is in charge of validate the introduced data in the
	'**%           detail zone for form.
	'**%Parameters:
	'**%    sCodispl   -
	'**%    sAction    -
	'**%    nSeleted   -
	'**%    nBranch    - Code of the Line of Business. The possible values as per table 10.
	'**%    nProduct   - Code of the product.
	'**%    nLoad_cod  - Code of Current account Load
	'**%    dEffecdate - Date which from the record is valid.
	'**%    sDescript  - Complete name of the loading or charge.
	'**%    nLoad_type - Type of load  Sole values as per table 7996
	'**%    nLoadAmo   - Amount of administrative expenses to be charged to the insured
	'**%    nloadRate  - Percentage of administrative expenses to be charged to the insured
	'**%    sStatregt  - General status of the record. Sole values as per table 26.
	'**%    sShort_des - Abbreviated description of the charge
	'**%    nPayFreq   - Payment frecuency of the premium. Sole values as per table 36.
	'**%    sRoutine   - Code of the routine to be used to calculate the cost
	'**%    sPreInv    - Indicates the charge has to be applied previous to the investment. Sole values:    1 - Affirmative    2 - Negative.
	'**%    nType_move - Type of current account movement Sole values as per table 401
	'%Objetivo: Esta función se encarga de validar los datos introducidos en la zona de
	'%          detalle para forma.
	'%Parámetros:
	'%      sCodispl   -
	'%      sAction    -
	'%      nSeleted   -
	'%      nBranch    - Código del ramo comercial. Valores posibles según tabla 10.
	'%      nProduct   - Código del producto.
	'%      nLoad_cod  - Código del cargo a cuenta corriente
	'%      dEffecdate - Fecha de efecto del registro.
	'%      sDescript  - Descripción completa del cargo
	'%      nLoad_type - Tipo de cargo  Valores únios según tabla 7996
	'%      nLoadAmo   - Importe de gastos administrativos a cargo del asegurado
	'%      nloadRate  - Porcentaje de gastos administrativos a cargo del asegurado
	'%      sStatregt  - Estado general del registro. Valores únicos según tabla 26.
	'%      sShort_des - Descripción abreviada del cargo
	'%      nPayFreq   - Frecuencia de pago de la prima. Valores únicos según tabla 36.
	'%      sRoutine   - Rutina de cálculo del cargo
	'%      sPreInv    - Indicador de cargo a ser aplicado previo a realizar la inversión. Valores únicos:    1 - Afirmativo    2 - Negativo
	'%      nType_move - Tipo de movimiento de cuenta corriente Valores únicos según tabla 401
    Public Function insValDP064(ByVal sCodispl As String, ByVal sAction As String, Optional ByVal nSeleted As Integer = 0, Optional ByVal nBranch As Integer = 0, Optional ByVal nProduct As Integer = 0, Optional ByVal nLoad_cod As Integer = 0, Optional ByVal dEffecdate As Date = #12:00:00 AM#, Optional ByVal sDescript As String = "", Optional ByVal nLoad_type As Integer = 0, Optional ByVal nLoadAmo As Double = 0, Optional ByVal nloadRate As Double = 0, Optional ByVal sStatregt As String = "", Optional ByVal sShort_des As String = "", Optional ByVal nPayFreq As Integer = 0, Optional ByVal sRoutine As String = "", Optional ByVal sPreInv As String = "", Optional ByVal nType_Move As Integer = 0, Optional ByVal nModulec As Integer = 0, Optional ByVal nCover As Integer = 0, Optional ByVal sExist_Modul As String = "", Optional ByVal nMonthi As Integer = 0, Optional ByVal nMonthe As Integer = 0, Optional ByVal nAply As Integer = 0, Optional ByVal nOriAply As Integer = 0, Optional ByVal nFunds As Integer = 0, Optional ByVal nMinimumAmount As Double = 0, Optional ByVal nMaximumAmount As Double = 0) As String

        '**- Variable definition lclsErrors for the error of the window sending
        '- Se define la variable lclsErrors para el envío de errores de la ventana

        Dim lclsErrors As eFunctions.Errors
        Dim lclsValField As eFunctions.valField
        Dim lclsProduct As Product

        On Error GoTo ErrorHandler
        lclsErrors = New eFunctions.Errors
        lclsValField = New eFunctions.valField
        lclsProduct = New Product

        '**+ The product type must be <> to conventional (line of business of non traditional life)
        '+ El tipo de producto debe ser <> a convencional (ramo de vida no tradicional)

        '    If Trim$(lclsProduct_li.nProdClas) = 1 Then
        '        If lerrTime.ErrorMessage("DP064", 38002) Then
        '            insValDP064 = False
        '        End If
        '    End If

        '**+ Validation of the "COde" field.
        '+Validacion del campo "Código".
        If nLoad_cod = eRemoteDB.Constants.intNull Or nLoad_cod = 0 Then

            Call lclsErrors.ErrorMessage(sCodispl, 12157)

            '**+ If the code is not full, none of the errors either
            '+ Si el código no está lleno, ninguno de los otros tampoco
            If sDescript <> String.Empty Or (nLoad_type <> eRemoteDB.Constants.intNull And nLoad_type <> 0) Or (nLoadAmo <> eRemoteDB.Constants.intNull And nLoadAmo <> 0) Or (nloadRate <> eRemoteDB.Constants.intNull And nloadRate <> 0) Or sStatregt <> String.Empty Or sShort_des <> String.Empty Or (nPayFreq <> eRemoteDB.Constants.intNull And nPayFreq <> 0) Or sRoutine <> String.Empty Or sPreInv <> String.Empty Or (nType_Move <> eRemoteDB.Constants.intNull And nType_Move <> 0) Then

                Call lclsErrors.ErrorMessage(sCodispl, 1084)
            End If
        Else
            '**+ The code can not be repeated in the life_load table
            '+ El código no puede estar repetido dentro de la tabla life_load
            If sAction = "Add" And Find(nBranch, nProduct, nLoad_cod, nMonthi, dEffecdate, "1") Then
                Call lclsErrors.ErrorMessage(sCodispl, 10004)
            End If

            '**+ If the code is full, the type field too
            '+ Si el código está lleno, el campo tipo también
            If nLoad_type = eRemoteDB.Constants.intNull Or nLoad_type = 0 Then
                Call lclsErrors.ErrorMessage(sCodispl, 11334)
            End If

            '**+ If the code is full, the description field too
            '+ Si el código está lleno, el campo descripción también
            If sDescript = String.Empty Then
                Call lclsErrors.ErrorMessage(sCodispl, 11299)
            End If

            '**+ If the code is full, the descriptio field cuts too
            '+ Si el código está lleno, el campo descripción corta también
            If sShort_des = String.Empty Then
                Call lclsErrors.ErrorMessage(sCodispl, 11300)
            End If

            '**+ If the code is full, the field movement type of Current Account too
            '+ Si el código está lleno, el campo tipo de movimiento de Cta. Cte. también
            If nType_Move = eRemoteDB.Constants.intNull Or nType_Move = 0 Then
                Call lclsErrors.ErrorMessage(sCodispl, 66152)
            End If

            '**+ If the code is full, the field record status too
            '+ Si el código está lleno, el campo estado del registro también
            If sStatregt = String.Empty Then
                Call lclsErrors.ErrorMessage(sCodispl, 1922)
            End If

            '+ Se aplican validaciones relacionadas con el modulo y
            '+la cobertura solo si el tipo de cargo es "Costo-cobertura"
            If nLoad_type = 1 Then
                If sExist_Modul = "1" Then
                    If nModulec = eRemoteDB.Constants.intNull Then
                        Call lclsErrors.ErrorMessage(sCodispl, 11296)
                    End If
                End If
                If nCover = eRemoteDB.Constants.intNull Then
                    Call lclsErrors.ErrorMessage(sCodispl, 3552)
                End If
            End If

            If sRoutine = String.Empty Then
                If nAply = eRemoteDB.Constants.intNull Then
                    Call lclsErrors.ErrorMessage(sCodispl, 60485)
                End If
            End If

            If nMonthi = eRemoteDB.Constants.intNull Then
                Call lclsErrors.ErrorMessage(sCodispl, 56015)
            Else
                If nMonthe < nMonthi And nMonthe <> eRemoteDB.Constants.intNull Then
                    Call lclsErrors.ErrorMessage(sCodispl, 56017)
                End If
            End If

            '+ Si el producto no es APV, se valida el campo origen sobre el cual aplica
            If lclsProduct.FindProduct_li(nBranch, nProduct, dEffecdate) Then
                If nOriAply = eRemoteDB.Constants.intNull And lclsProduct.sApv <> "1" Then
                    Call lclsErrors.ErrorMessage(sCodispl, 55676, , eFunctions.Errors.TextAlign.RigthAling, " sobre el cual aplica el cargo")
                End If
            End If

            '+ Si se ha indicado que el Cargo/Costo aplica sobre un fondo en particular,
            '+ se valida que se introduzca dicho fondo.
            If nAply <> eRemoteDB.Constants.intNull And nAply = 11 And nFunds = eRemoteDB.Constants.intNull Then
                Call lclsErrors.ErrorMessage(sCodispl, 56058)
            End If

        End If

        insValDP064 = lclsErrors.Confirm

ErrorHandler:
        'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsErrors = Nothing
        'UPGRADE_NOTE: Object lclsValField may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsValField = Nothing
        On Error GoTo 0
    End Function
	
	'**%Objective:
	'**%Parameters:
	'**%    sAction    -
	'**%    nSeleted   -
	'**%    nBranch    - Code of the Line of Business. The possible values as per table 10.
	'**%    nProduct   - Code of the product.
	'**%    nLoad_cod  - Code of Current account Load
	'**%    dEffecdate - Date which from the record is valid.
	'**%    sDescript  - Complete name of the loading or charge.
	'**%    nLoad_type - Type of load  Sole values as per table 7996
	'**%    nLoadAmo   - Amount of administrative expenses to be charged to the insured
	'**%    nloadRate  - Percentage of administrative expenses to be charged to the insured
	'**%    sStatregt  - General status of the record. Sole values as per table 26.
	'**%    sShort_des - Abbreviated description of the charge
	'**%    nPayFreq   - Payment frecuency of the premium. Sole values as per table 36.
	'**%    sRoutine   - Code of the routine to be used to calculate the cost
	'**%    sPreInv    - Indicates the charge has to be applied previous to the investment. Sole values:    1 - Affirmative    2 - Negative.
	'**%    nType_move - Type of current account movement Sole values as per table 401
	'**%    nUsercode  - Code of the user creating or updating the record.
	'%Objetivo:
	'%Parámetros:
	'%      sAction    -
	'%      nSeleted   -
	'%      nBranch    - Código del ramo comercial. Valores posibles según tabla 10.
	'%      nProduct   - Código del producto.
	'%      nLoad_cod  - Código del cargo a cuenta corriente
	'%      dEffecdate - Fecha de efecto del registro.
	'%      sDescript  - Descripción completa del cargo
	'%      nLoad_type - Tipo de cargo  Valores únios según tabla 7996
	'%      nLoadAmo   - Importe de gastos administrativos a cargo del asegurado
	'%      nloadRate  - Porcentaje de gastos administrativos a cargo del asegurado
	'%      sStatregt  - Estado general del registro. Valores únicos según tabla 26.
	'%      sShort_des - Descripción abreviada del cargo
	'%      nPayFreq   - Frecuencia de pago de la prima. Valores únicos según tabla 36.
	'%      sRoutine   - Rutina de cálculo del cargo
	'%      sPreInv    - Indicador de cargo a ser aplicado previo a realizar la inversión. Valores únicos:    1 - Afirmativo    2 - Negativo
	'%      nType_move - Tipo de movimiento de cuenta corriente Valores únicos según tabla 401
	'%      nUsercode  - Código del usuario que crea o actualiza el registro.
    Public Function insPostDP064(ByVal sAction As String, ByVal nSeleted As Integer, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nLoad_cod As Integer, ByVal dEffecdate As Date, ByVal sDescript As String, ByVal nLoad_type As Integer, ByVal nLoadAmo As Double, ByVal nloadRate As Double, ByVal sStatregt As String, ByVal sShort_des As String, ByVal nPayFreq As Integer, ByVal sRoutine As String, ByVal sPreInv As String, ByVal nType_Move As Integer, ByVal nUsercode As Integer, ByVal sFirst_cost_pro As String, ByVal nModulec As Integer, ByVal nCover As Integer, ByVal sTaxin As String, ByVal nMonthi As Integer, ByVal nMonthe As Integer, ByVal nAply As Integer, ByVal nOriAply As Integer, ByVal sRetro As String, ByVal sPremBas As String, ByVal nFunds As Integer, ByVal sInstallind As String, ByVal sFirst_apply As String, ByVal nIndex_table As Integer, ByVal nMinimumAmount As Double, ByVal nMaximumAmount As Double) As Boolean
        On Error GoTo ErrorHandler

        With Me
            .nBranch = nBranch
            .nProduct = nProduct
            .nLoad_cod = nLoad_cod
            .dEffecdate = dEffecdate
            .sDescript = sDescript
            .nLoad_type = nLoad_type
            .nLoadAmo = nLoadAmo
            .nloadRate = nloadRate

            If sAction = "Add" Then
                .sStatregt = "1"
            Else
                .sStatregt = sStatregt
            End If

            .sShort_des = sShort_des
            .nPayFreq = nPayFreq
            .sRoutine = sRoutine
            .sPreInv = IIf(sPreInv <> "1", "2", sPreInv)
            .nType_Move = nType_Move
            .nUsercode = nUsercode
            .sFirst_cost_pro = IIf(sFirst_cost_pro = String.Empty, "2", sFirst_cost_pro)
            .nModulec = IIf(nModulec = eRemoteDB.Constants.intNull, 0, nModulec)
            .nCover = nCover
            .sTaxin = IIf(sTaxin = String.Empty, "2", sTaxin)
            .nMonthi = nMonthi
            .nMonthe = nMonthe
            .nAply = nAply
            .nOriAply = nOriAply
            .sRetro = sRetro
            .sPremBas = IIf(sPremBas = String.Empty, "2", sPremBas)
            .nFunds = nFunds
            .sInstallind = sInstallind
            .sFirst_apply = sFirst_apply
            .nIndex_table = nIndex_table
            .nMinimumAmount = nMinimumAmount
            .nMaximumAmount = nMaximumAmount

        End With

        insPostDP064 = True

        Select Case sAction

            '**+ If the selected option is Record
            '+Si la opción seleccionada es Registrar

            Case "Add"
                insPostDP064 = Update()

                '**+ If the selected option is Modify
                '+Si la opción seleccionada es Modificar

            Case "Update"
                insPostDP064 = Update()

                '**+ If the selected option is Delete
                '+Si la opción seleccionada es Eliminar

            Case "Del"
                insPostDP064 = Delete()

        End Select

        Exit Function
ErrorHandler:
        insPostDP064 = False
    End Function
	
	'**%Objective:
	'%Objetivo:
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		dNulldate = CDate(Nothing)
		nLoadAmo = 0
        nloadRate = 0
        nMinimumAmount = 0
        nMaximumAmount = 0

	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
End Class






