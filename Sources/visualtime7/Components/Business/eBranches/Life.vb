Option Strict Off
Option Explicit On
Public Class Life
	'%-------------------------------------------------------%'
	'% $Workfile:: Life.cls                                 $%'
	'% $Author:: Nvaplat41                                  $%'
	'% $Date:: 10/10/03 17.34                               $%'
	'% $Revision:: 14                                       $%'
	'%-------------------------------------------------------%'
	
	'+ Propiedades según la tabla en el sistema el 05/11/2001
	
	'   Column_name                  Type            Computed   Length  Prec  Scale  Nullable  TrimTrailingBlanks  FixedLenNullInSource
	'----------------------  ---------------------   ---------  ------  ----- ------ --------  ------------------  --------------------
	Public sCertype As String 'char       no         1                     no           no                  no
	Public nProduct As Integer 'number     no         2      5     0        no          (n/a)               (n/a)
	Public nBranch As Integer 'number     no         2      5     0        no          (n/a)               (n/a)
	Public nPolicy As Double 'number     no         4      10    0        no          (n/a)               (n/a)
	Public nCertif As Double 'number     no         4      10    0        no          (n/a)               (n/a)
	Public dEffecdate As Date 'datetime   no         8                     no          (n/a)               (n/a)
	Public nAge As Integer 'number     no         2      5     0        yes         (n/a)               (n/a)
	Public sClient As String 'char       no         14                    yes          no                  yes
	Public nAge_limit As Integer 'number     no         2      5     0        yes         (n/a)               (n/a)
	Public nAge_reinsu As Integer 'number     no         2      5     0        yes         (n/a)               (n/a)
	Public sAmorti_way As String 'char       no         1                     yes          no                  yes
	Public nCapital As Double 'number     no         9      18    6        yes         (n/a)               (n/a)
	Public nCapital_ca As Double 'number     no         9      18    6        yes         (n/a)               (n/a)
	Public dCompdate As Date 'datetime   no         8                     yes         (n/a)               (n/a)
	Public nEnd_num As Integer 'number     no         4      10    0        yes         (n/a)               (n/a)
	Public nEnt_right As Integer 'number     no         4      10    0        yes         (n/a)               (n/a)
	Public nExa_amount As Double 'decimal    no         9      10    0        yes         (n/a)               (n/a)
	Public nExam_type As Integer 'number     no         2      5     0        yes         (n/a)               (n/a)
	Public dExpirdat As Date 'datetime   no         8                     yes         (n/a)               (n/a)
	Public nTypdurins As Integer 'number     no         4      5     0        yes         (n/a)               (n/a)
	Public nInit_num As Integer 'number     no         4      5     0        yes         (n/a)               (n/a)
	Public nInsur_time As Integer 'number     no         2      5     0        yes         (n/a)               (n/a)
	Public dIssuedat As Date 'datetime   no         8                     yes         (n/a)               (n/a)
	Public sLoan_numbe As String 'char       no         10                    yes          no                  yes
	Public nNullcode As Integer 'number     no         2      5     0        yes         (n/a)               (n/a)
	Public dNulldate As Date 'datetime   no         8                     yes         (n/a)               (n/a)
	Public nPay_time As Integer 'number     no         2      5     0        yes         (n/a)               (n/a)
	Public sPduraind As String 'char       no         1                     yes          no                  yes
	Public nPermulti As Double 'decimal    no         9      10    2        yes         (n/a)               (n/a)
	Public nPernumai As Double 'decimal    no         9      10    2        yes         (n/a)               (n/a)
	Public nPernunmi As Double 'decimal    no         9      10    2        yes         (n/a)               (n/a)
	Public nPremium As Double 'decimal    no         9      10    2        yes         (n/a)               (n/a)
	Public nPremium_ca As Double 'decimal    no         9      10    2        yes         (n/a)               (n/a)
	Public nReceipt As Double 'number     no         4      10    0        yes         (n/a)               (n/a)
	Public nSald_amoun As Double 'decimal    no         9      18    6        yes         (n/a)               (n/a)
	Public sSald_prog As String 'char       no         1                     yes          no                  yes
	Public dStartDate As Date 'datetime   no         8                     yes         (n/a)               (n/a)
	Public nTitles_sub As Integer 'number     no         2      5     0        yes         (n/a)               (n/a)
	Public nUsercode As Integer 'number     no         2      5     0        yes         (n/a)               (n/a)
	Public nWar_int_ex As Double 'decimal    no         5      4     2        yes         (n/a)               (n/a)
	Public nWar_intere As Double 'decimal    no         5      4     2        yes         (n/a)               (n/a)
	Public nXprem_time As Integer 'number     no         2      5     0        yes         (n/a)               (n/a)
	Public nYears_old As Integer 'number     no         2      5     0        yes         (n/a)               (n/a)
	Public nTransactio As Integer 'number     no         2      5     0        yes         (n/a)               (n/a)
	Public dProg_date As Date 'datetime   no         8                     yes         (n/a)               (n/a)
	Public nRentamount As Double 'decimal    no         9      18    6        yes         (n/a)               (n/a)
	Public nCurrrent As Integer 'number     no         2      5     0        yes         (n/a)               (n/a)
	Public sCreditnum As String 'char       no        20                     yes          no                  yes
	Public nCred_Pro As Integer 'number     no         2      5     0        yes         (n/a)               (n/a)
	Public dInit_Cre As Date 'datetime   no         8                     yes         (n/a)               (n/a)
	Public dEnd_Cre As Date 'datetime   no         8                     yes         (n/a)               (n/a)
	Public nCurren_Cre As Integer 'number     no         2      5     0        yes         (n/a)               (n/a)
	Public nAmount_Cre As Double 'decimal    no         9      18    6        yes         (n/a)               (n/a)
	Public nAmount_Act As Double 'decimal    no         9      18    6        yes         (n/a)               (n/a)
	Public nCalcapital As Integer 'number     no         2      5     0        yes         (n/a)               (n/a)
	Public nTyppremium As Integer 'number     no         2      5     0        yes         (n/a)               (n/a)
	Public nCount_Insu As Integer 'number     no         2      5     0        yes         (n/a)               (n/a)
	Public nGroup_comp As Integer 'number     no         2      5     0        yes         (n/a)               (n/a)
	Public nTariff As Integer 'number     no         2      5     0        yes         (n/a)               (n/a)
	Public nGroup As Integer 'number     no         2      5     0        yes         (n/a)               (n/a)
	Public nSituation As Integer 'number     no         2      5     0        yes         (n/a)               (n/a)
	Public sAccnum As String 'char       no        20                     yes          no                  yes
	Public nCapitalmax As Double 'decimal    no         9      18    6        yes         (n/a)               (n/a)
	Public nPerc_Cap As Double 'decimal    no         9      9     6        yes         (n/a)               (n/a)
	Public sIduraind As String 'char       no         1                     yes          no                  yes
	Public nLegamount As Double 'decimal    no         9      12    3        yes         (n/a)               (n/a)
	Public nTypDurpay As Integer 'number     no         2      5     0        yes         (n/a)               (n/a)
	Public dDate_pay As Date 'datetime   no         8                     yes         (n/a)               (n/a)
	
	Public sDesBranch As String
	Public sDesProduct As String
	Public sPolitype As String
	Public nPayfreq As Integer
	Public sCliename As String
	
	Private Structure udtPolLife
		Dim nBranch As Integer
		Dim sDesBranch As String
		Dim sDesProduct As String
		Dim sPolitype As String
		Dim dEffecdate As Date
		Dim dExpirdat As Date
		Dim nPayfreq As Integer
		Dim nAge As Integer
		Dim nAge_reinsu As Integer
		Dim nCapital As Double
		Dim nPremium As Double
		Dim nCertif As Double
		Dim nPolicy As Double
		Dim sCliename As String
		Dim sClient As String
		Dim nProduct As Integer
	End Structure
	
	Private arrPolLife() As udtPolLife
	
	Public ReadOnly Property Count() As Integer
		Get
			Count = UBound(arrPolLife)
		End Get
	End Property
	
	Public Function ItemPolLife(ByVal lintindex As Integer) As Boolean
		If lintindex <= UBound(arrPolLife) Then
			With arrPolLife(lintindex)
				nBranch = .nBranch
				sDesBranch = .sDesBranch
				sDesProduct = .sDesProduct
				sPolitype = .sPolitype
				dEffecdate = .dEffecdate
				dExpirdat = .dExpirdat
				nPayfreq = .nPayfreq
				nAge = .nAge
				nAge_reinsu = .nAge_reinsu
				nCapital = .nCapital
				nPremium = .nPremium
				nCertif = .nCertif
				nPolicy = .nPolicy
				sCliename = .sCliename
				sClient = .sClient
				nProduct = .nProduct
			End With
			ItemPolLife = True
		Else
			ItemPolLife = False
		End If
	End Function
	
	'%Find_VIC005_k:Esta función se encarga de vaciar la información encontrada, al ejecutar el
	'%              select preparado, en el arreglo correspondiente al grid y en el arreglo que
	'%              posee toda la información que se va a mostrar en los objetos.
	Public Function Find_VIC005_k(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal sPolitype As String, ByVal dEffecdate As Date, ByVal dExpirdat As Date, ByVal nPayfreq As Integer, ByVal nAge As Integer, ByVal nAge_reinsu As Integer, ByVal nCapital As Double, ByVal nPremium As Double) As Boolean
		Dim lrecreaLife As eRemoteDB.Execute
		Dim lintCount As Integer
		
		On Error GoTo Find_VIC005_k_Err
		
		lrecreaLife = New eRemoteDB.Execute
		
		Find_VIC005_k = False
		
		With lrecreaLife
			.StoredProcedure = "reaPolicyLife"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sPolitype", sPolitype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dExpirdat", dExpirdat, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPayfreq", nPayfreq, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAge", nAge, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAge_reinsu", nAge_reinsu, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCapital", nCapital, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPremium", nPremium, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				Find_VIC005_k = True
				lintCount = 0
				ReDim arrPolLife(1000)
				Do While Not .EOF
					'+Se vacia la información en el arreglo que contiene toda la información que se mostrará en los objetos
					arrPolLife(lintCount).nBranch = .FieldToClass("nBranch")
					arrPolLife(lintCount).sDesBranch = .FieldToClass("sDesBranch")
					arrPolLife(lintCount).sDesProduct = .FieldToClass("sDesProduct")
					arrPolLife(lintCount).sPolitype = .FieldToClass("sPolitype")
					arrPolLife(lintCount).dEffecdate = .FieldToClass("dStartdate")
					arrPolLife(lintCount).dExpirdat = .FieldToClass("dExpirdat")
					arrPolLife(lintCount).nPayfreq = .FieldToClass("nPayFreq")
					arrPolLife(lintCount).nAge = .FieldToClass("nAge")
					arrPolLife(lintCount).nAge_reinsu = .FieldToClass("nAge_reinsu")
					arrPolLife(lintCount).nCapital = .FieldToClass("nCapital")
					arrPolLife(lintCount).nPremium = .FieldToClass("nPremium")
					arrPolLife(lintCount).nCertif = .FieldToClass("nCertif")
					arrPolLife(lintCount).nPolicy = .FieldToClass("nPolicy")
					arrPolLife(lintCount).sCliename = .FieldToClass("sClieName")
					arrPolLife(lintCount).sClient = .FieldToClass("sClient")
					arrPolLife(lintCount).nProduct = .FieldToClass("nProduct")
					lintCount = lintCount + 1
					.RNext()
				Loop 
				.RCloseRec()
				'+Se reajusta el tamaño del Grid a la cantidad de datos a mostrar
				ReDim Preserve arrPolLife(lintCount)
			Else
				Find_VIC005_k = False
			End If
		End With
		
Find_VIC005_k_Err: 
		If Err.Number Then
			Find_VIC005_k = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecreaLife may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaLife = Nothing
	End Function
	
	'%insValVIC005: Esta función se encarga de validar los datos introducidos en la zona de
	'%              detalle para la forma.
	Public Function insValVIC005(ByVal sCodispl As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal sPolitype As String, ByVal dEffecdate As Date, ByVal dExpirdat As Date, ByVal nPayfreq As Integer, ByVal nAge As Integer, ByVal nAge_reinsu As Integer, ByVal nCapital As Double, ByVal nPremium As Double) As String
		
		On Error GoTo insValVIC005_Err
		
		Dim lclsErrors As eFunctions.Errors
		Dim lclsProductBran As Object
		Dim lclsProductProd As Object
		Dim lintindex As Integer
		'-Se define la variable lstrBrancht utilizada para almacenar el valor del ramo técnico.
		Dim lstrBrancht As String
		
		lclsErrors = New eFunctions.Errors
		lclsProductBran = eRemoteDB.NetHelper.CreateClassInstance("eProduct.Branches")
		lclsProductProd = eRemoteDB.NetHelper.CreateClassInstance("eProduct.Product")
		
		'+Validación del campo Ramo.
		If nBranch = 0 Or nBranch = eRemoteDB.Constants.intNull Then
			Call lclsErrors.ErrorMessage(sCodispl, 1022)
		Else
			If nBranch <> 0 And nBranch <> eRemoteDB.Constants.intNull Then
				If Not lclsProductBran.insVerifyBranch(nBranch, "('1','2')") Then
					Call lclsErrors.ErrorMessage(sCodispl, 3967)
				End If
			End If
		End If
		
		'+Validación del campo Producto.
		If nProduct <> 0 And nProduct <> eRemoteDB.Constants.intNull Then
			Call lclsProductProd.insValProdMaster(nBranch, nProduct)
			lstrBrancht = lclsProductProd.sBrancht
			If lstrBrancht <> "1" And lstrBrancht <> "2" Then
				Call lclsErrors.ErrorMessage(sCodispl, 13000)
			End If
		End If
		
		insValVIC005 = lclsErrors.Confirm
		
insValVIC005_Err: 
		If Err.Number Then
			insValVIC005 = insValVIC005 & Err.Description
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
		'UPGRADE_NOTE: Object lclsProductBran may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsProductBran = Nothing
		'UPGRADE_NOTE: Object lclsProductProd may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsProductProd = Nothing
	End Function
End Class






