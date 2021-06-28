Option Strict Off
Option Explicit On
Imports System.Web
Public Class Projectvuls
	Implements System.Collections.IEnumerable
	'%-------------------------------------------------------%'
	'% $Workfile:: Projectvuls.cls                         $%'
	'% $Author:: Pmanzur                                    $%'
	'% $Date:: 28/04/06 12:22p                              $%'
	'% $Revision:: 17                                       $%'
	'%-------------------------------------------------------%'
	
	'local variable to hold collection
	Private mCol As Collection
	Public sKey As String
	Private Structure udtArray
		Dim sField As String
	End Structure
	Public mlngIndex As Integer
	Private marray() As udtArray
	
	
	
	'%Add: Agrega un nuevo registro a la colección
	Public Function Add(ByRef objClass As Projectvul) As Projectvul
		If objClass Is Nothing Then
			objClass = New Projectvul
		End If
		
		With objClass
			mCol.Add(objClass, .sCertype & .nBranch & .nProduct & .nPolicy & .nCertif & .nYear & .dEffecdate.ToString("yyyyMMdd"))
		End With
		
		'Return the object created
		Add = objClass
	End Function
	
	'%Find: Lee los planes de pago para los aportes de la póliza
	Public Function Find(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date) As Boolean
		Dim lrecreaProjectvul As eRemoteDB.Execute
		Dim lclsProjectvul As Projectvul
		
		On Error GoTo Find_Err
		
		lrecreaProjectvul = New eRemoteDB.Execute
		
		'+ Definición de store procedure reaProjectvul_a al 04-03-2002 12:38:04
		With lrecreaProjectvul
			.StoredProcedure = "reaProjectvul"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				Find = True
				Do While Not .EOF
					lclsProjectvul = New Projectvul
					lclsProjectvul.sCertype = sCertype
					lclsProjectvul.nBranch = nBranch
					lclsProjectvul.nProduct = nProduct
					lclsProjectvul.nPolicy = nPolicy
					lclsProjectvul.nCertif = nCertif
					lclsProjectvul.dEffecdate = .FieldToClass("dEffecdate")
					lclsProjectvul.nYear = .FieldToClass("nYear")
					lclsProjectvul.nAge = .FieldToClass("nAge")
					lclsProjectvul.nCurrency = .FieldToClass("nCurrency")
					lclsProjectvul.nVp = .FieldToClass("nVp")
					lclsProjectvul.nPremium = .FieldToClass("nPremium")
					lclsProjectvul.nSurrAmount = .FieldToClass("nSurramount")
					lclsProjectvul.nSurramountl = .FieldToClass("nSurramountl")
					lclsProjectvul.nCapital = .FieldToClass("nCapital")
					lclsProjectvul.nUsercode = .FieldToClass("nUsercode")
					lclsProjectvul.nPremium2 = .FieldToClass("nPremium2")
					lclsProjectvul.nCapital2 = .FieldToClass("nCapital2")
					lclsProjectvul.nSurramount2 = .FieldToClass("nSurramount2")
					lclsProjectvul.nVp2 = .FieldToClass("nVp2")
					lclsProjectvul.nVp_npremium = .FieldToClass("nVp_npremium")
					lclsProjectvul.nVp_saving = .FieldToClass("nVp_saving")
					lclsProjectvul.nVp2_npremium = .FieldToClass("nVp2_npremium")
					lclsProjectvul.nVp2_saving = .FieldToClass("nVp2_saving")
					Call Add(lclsProjectvul)
					'UPGRADE_NOTE: Object lclsProjectvul may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
					lclsProjectvul = Nothing
					.RNext()
				Loop 
				.RCloseRec()
			End If
		End With
		
Find_Err: 
		If Err.Number Then
			Find = False
		End If
		'UPGRADE_NOTE: Object lrecreaProjectvul may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaProjectvul = Nothing
		On Error GoTo 0
	End Function
	
	'* Item: Devuelve un elemento de la colección (segun índice)
	Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As Projectvul
		Get
			Item = mCol.Item(vntIndexKey)
		End Get
	End Property
	
	'* Count: Devuelve el número de elementos que posee la colección
	Public ReadOnly Property Count() As Integer
		Get
			Count = mCol.Count()
		End Get
	End Property
	
	'* NewEnum: Permite enumerar la colección para utilizarla en un ciclo For Each... Next
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
	
	'% Remove: Elimina un elemento de la colección
	Public Sub Remove(ByRef vntIndexKey As Object)
		mCol.Remove(vntIndexKey)
	End Sub
	
	'% Class_Initialize: Controla la creación de una instancia de la colección
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		mCol = New Collection
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
	
	'% Class_Terminate: Controla la destrucción de una instancia de la colección
	'UPGRADE_NOTE: Class_Terminate was upgraded to Class_Terminate_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Terminate_Renamed()
		'UPGRADE_NOTE: Object mCol may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		mCol = Nothing
	End Sub
	Protected Overrides Sub Finalize()
		Class_Terminate_Renamed()
		MyBase.Finalize()
	End Sub
	
	'%InsShowIlustration: Muestra la ilustración de la poliza/certificado
	Public Function InsShowIlustrationVul(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, ByVal nUsercode As Integer, Optional ByVal nOption As Integer = eRemoteDB.Constants.intNull, Optional ByVal nIntwarr As Double = eRemoteDB.Constants.intNull, Optional ByVal nIntwarrSaving As Double = eRemoteDB.Constants.intNull, Optional ByVal nVp_initial As Double = eRemoteDB.Constants.intNull, Optional ByVal dBirthdate As Date = eRemoteDB.Constants.dtmNull, Optional ByVal dEffecdate_to As Date = eRemoteDB.Constants.dtmNull, Optional ByVal nPremDeal_anu As Double = eRemoteDB.Constants.intNull, Optional ByVal bReadTable As Boolean = True, Optional ByVal bQuery As Boolean = False, Optional ByVal nIntwarr2 As Double = eRemoteDB.Constants.intNull, Optional ByVal nIntwarrSaving2 As Double = eRemoteDB.Constants.intNull) As Boolean
		If bQuery Then
			InsShowIlustrationVul = Me.Find(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate)
		Else
			InsShowIlustrationVul = InsCalValuePolIlustrationVul(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate, nUsercode, nOption, nIntwarr, nVp_initial, dBirthdate, dEffecdate_to, nPremDeal_anu, bReadTable, nIntwarrSaving, nIntwarr2, nIntwarrSaving2)
		End If
	End Function
	
	'%InsCalValuePolIlustrationVul: Llama al procedimiento que cálcula la ilustración del
	'%                              valor póliza
	Public Function InsCalValuePolIlustrationVul(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, ByVal nUsercode As Integer, ByVal nOption As Integer, ByVal nIntwarr As Double, ByVal nVp_initial As Double, ByVal dBirthdate As Date, ByVal dEffecdate_to As Date, ByVal nPremDeal_anu As Double, Optional ByVal bReadTable As Boolean = True, Optional ByVal nIntwarrSaving As Double = 0, Optional ByVal nIntwarr2 As Double = 0, Optional ByVal nIntwarrSaving2 As Double = 0) As Boolean
		Dim lrecInsCalIllustrationVul As eRemoteDB.Execute
		Dim ncount As Object
		Dim lclsPer_deposit As Per_deposit
		
		On Error GoTo InsCalIllustrationVul_Err
		lrecInsCalIllustrationVul = New eRemoteDB.Execute
		lclsPer_deposit = New Per_deposit
		
		'+ Definición de store procedure InsCalIllustration al 04-09-2002 13:29:16
		With lrecInsCalIllustrationVul
			If sCertype = "3" Then
				.StoredProcedure = "update_chrg_vil7002pkg.InsCalIlustration"
			Else
				'+ Si existe registros en per_deposit, la prima convenida se envía en nulo
				If lclsPer_deposit.Count(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate) > 0 Then
					nPremDeal_anu = eRemoteDB.Constants.intNull
				End If
				.StoredProcedure = "update_chrg_vil7002pkg.InsCalIlustration"
			End If
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sKey", sKey, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nOption", nOption, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nVp_initial", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nIntwarr", nIntwarr, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 8, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nIntwarrSaving", nIntwarrSaving, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 8, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dBirthdate", dBirthdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate_to", dEffecdate_to, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPremdeal_anu", nPremDeal_anu, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			'If sCertype = "3" Then
			.Parameters.Add("nIntwarr2", nIntwarr2, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 8, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nIntwarrSaving2", nIntwarrSaving2, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 8, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			'End If
			InsCalValuePolIlustrationVul = .Run(False)
			sKey = .Parameters("sKey").Value
		End With
		
		If InsCalValuePolIlustrationVul Then
			If bReadTable Then
				InsCalValuePolIlustrationVul = Me.Find(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate)
			End If
		End If
		
InsCalIllustrationVul_Err: 
		If Err.Number Then
			InsCalValuePolIlustrationVul = False
		End If
		'UPGRADE_NOTE: Object lrecInsCalIllustrationVul may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecInsCalIllustrationVul = Nothing
		'UPGRADE_NOTE: Object lclsPer_deposit may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsPer_deposit = Nothing
		On Error GoTo 0
	End Function
	
	'InsCalQuestion: Llama al procedimiento que cálcula la ilustración para responder las consultas
	'%                              asociadas
    Public Function InsCalQuestion(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal nUsercode As Integer, ByVal nType As Integer, ByVal nIntwarr As Double, ByVal nIntwarrsav As Double, ByVal nValueCon As Double, ByVal nValueEx As Double, ByVal nValuePol As Double, ByVal nAge As Integer, ByVal nAmountini As Double, ByVal nAgePay As Integer, ByVal nSurrAmount As Double, ByVal nCapital As Double, ByVal nValue As Double) As Double
        Dim lrecInsCalIllustrationVul As eRemoteDB.Execute

        On Error GoTo InsCalIllustrationVul_Err
        lrecInsCalIllustrationVul = New eRemoteDB.Execute

        '+ Definición de store procedure InsCalIllustration al 04-09-2002 13:29:16
        With lrecInsCalIllustrationVul
            .StoredProcedure = "update_chrg_vil7002pkg.InsCalQuestion"
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nType", nType, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nIntwarr", nIntwarr, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 8, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nIntwarrSav", nIntwarrsav, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 8, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nValueCon", nValueCon, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nValueEx", nValueEx, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nValuePol", nValuePol, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nAge", nAge, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nAmountini", nAmountini, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nAgePay", nAgePay, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nSurrAmount", nSurrAmount, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCapital", nCapital, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nValue", nValue, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nRet", nValuePol, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            .Run(False)

            InsCalQuestion = .Parameters("nRet").Value
            nSurrAmount = .Parameters("nSurrAmount").Value
            nCapital = .Parameters("nCapital").Value
            nValue = .Parameters("nValue").Value

        End With

InsCalIllustrationVul_Err:
        If Err.Number Then
            InsCalQuestion = 0
        End If
        'UPGRADE_NOTE: Object lrecInsCalIllustrationVul may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecInsCalIllustrationVul = Nothing
        On Error GoTo 0
    End Function
	
	
	'MakeVI1410: Muestra la ilustracion
    Public Function MakeVI1410(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, ByVal sKey As String, Optional ByVal nYear As Integer = 0) As String
        Dim lrecreaProjectvul As eRemoteDB.Execute
        Dim mobjGrid As eFunctions.Grid
        Dim llngIndex As Integer
        Dim llngCount As Integer
        Dim lintyear As Integer
        Dim strResult As String = ""

        Dim sField As String
        Try

            mobjGrid = New eFunctions.Grid
            lrecreaProjectvul = New eRemoteDB.Execute

            '+ Definición de store procedure reaProjectvul_a al 04-03-2002 12:38:04
            With lrecreaProjectvul
                .StoredProcedure = "INSVI1410PKG.INSREAVI1410_HEADER"
                .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("sKey", sKey, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nYear", nYear, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                If .Run Then
                    llngIndex = 1
                    Do While Not .EOF
                        llngCount = .FieldToClass("nCount")
                        sField = "Control" & llngIndex

                        If Not IsNothing(HttpContext.GetLocalResourceObject("/VTimeNet/Common/showilustrationVul.aspx", .FieldToClass("sValue"))) Then
                            Call mobjGrid.Columns.AddTextColumn(0, HttpContext.GetLocalResourceObject("/VTimeNet/Common/showilustrationVul.aspx", .FieldToClass("sValue")), sField, 30, "", , , , , , True)
                        Else
                            Call mobjGrid.Columns.AddTextColumn(0, .FieldToClass("sValue"), sField, 30, "", , , , , , True)
                        End If


                        llngIndex = llngIndex + 1
                        .RNext()
                    Loop
                    If nYear = 0 Then
                        Call mobjGrid.Columns.AddAnimatedColumn(0, HttpContext.GetLocalResourceObject("/VTimeNet/Common/showilustrationVul.aspx", "tcnMonthlyProjectionColumnCaption"), "btnCalc", "/VTimeNet/images/batchStat06.png", HttpContext.GetLocalResourceObject("/VTimeNet/Common/showilustrationVul.aspx", "tcnMonthlyIllustrationpolicyvalueColumnCaption"))
                    End If
                    mlngIndex = llngCount
                    .RCloseRec()
                End If
            End With
            'UPGRADE_NOTE: Object lrecreaProjectvul may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
            lrecreaProjectvul = Nothing

            '+ Se definen las propiedades generales del grid
            With mobjGrid
                .Codispl = "GI1403"
                .DeleteButton = False
                .AddButton = False
                .Top = 50
                .Height = 430
                .Width = 400
                .Columns("Sel").GridVisible = False
                .bOnlyForQuery = True
                .Splits_Renamed.AddSplit(0, "", 2)
            End With

            If nYear > 0 Then
                ' no lleva split
            Else

                lrecreaProjectvul = New eRemoteDB.Execute
                '+ Definición de store procedure reaProjectvul_a al 04-03-2002 12:38:04
                With lrecreaProjectvul
                    .StoredProcedure = "INSVI1410PKG.INSREAVI1410_SPLIT"
                    .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Parameters.Add("sKey", sKey, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    If .Run Then
                        Do While Not .EOF
                            mobjGrid.Splits_Renamed.AddSplit(0, .FieldToClass("sOrigin"), .FieldToClass("sPlit"))
                            .RNext()
                        Loop
                        .RCloseRec()
                    End If
                End With
                'UPGRADE_NOTE: Object lrecreaProjectvul may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
                lrecreaProjectvul = Nothing
                mobjGrid.Splits_Renamed.AddSplit(0, HttpContext.GetLocalResourceObject("/VTimeNet/Common/showilustrationVul.aspx", "tcnTotalColumnCaption"), 3)
            End If
            lrecreaProjectvul = New eRemoteDB.Execute
            '+ Definición de store procedure reaProjectvul_a al 04-03-2002 12:38:04
            With lrecreaProjectvul
                .StoredProcedure = "INSVI1410PKG.INSREAVI1410_FOLDER"
                .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("sKey", sKey, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                If .Run Then
                    llngIndex = 1
                    lintyear = 1
                    Do While Not .EOF
                        If llngIndex = mlngIndex + 1 Then
                            If nYear = 0 Then
                                mobjGrid.Columns("btnCalc").HRefScript = "ShowPopUp('/VTimeNet/Common/ShowIlustrationVul_month.aspx?sCodispl=VI1410&nYear=" & lintyear & "','ValuePolIlustration1',750,500,'yes','yes',30,30)"
                                lintyear = lintyear + 1
                            End If
                            strResult = strResult & mobjGrid.DoRow
                            llngIndex = 1
                        End If
                        mobjGrid.Columns("Control" & llngIndex).DefValue = .FieldToClass("sValue")
                        llngIndex = llngIndex + 1
                        .RNext()
                    Loop
                    If nYear = 0 Then
                        mobjGrid.Columns("btnCalc").HRefScript = "ShowPopUp('/VTimeNet/Common/ShowIlustrationVul_month.aspx?sCodispl=VI1410&nYear=" & lintyear & "','ValuePolIlustration1',750,500,'yes','yes',30,30)"
                        lintyear = lintyear + 1
                    End If
                    strResult = strResult & mobjGrid.DoRow
                    strResult = strResult & mobjGrid.closeTable
                    .RCloseRec()
                End If
            End With
            'UPGRADE_NOTE: Object lrecreaProjectvul may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
            lrecreaProjectvul = Nothing
            Return strResult
        Catch ex As Exception
            MakeVI1410 = Err.Description
            'UPGRADE_NOTE: Object lrecreaProjectvul may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
            lrecreaProjectvul = Nothing
        Finally
        End Try
    End Function
End Class






