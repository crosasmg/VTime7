Option Strict Off
Option Explicit On
Public Class Tmp_val669s
	Implements System.Collections.IEnumerable
	'%-------------------------------------------------------%'
	'% $Workfile:: Tmp_val669s.cls                          $%'
	'% $Author:: Nvaplat41                                  $%'
	'% $Date:: 9/10/03 19.02                                $%'
	'% $Revision:: 18                                       $%'
	'%-------------------------------------------------------%'
	
	'local variable to hold collection
	Private mCol As Collection
	
	'+ Tipo de ilustracion
	Public Enum eIllustType
		eIllustAddPrem = 1 ' Con prima adicional
		eIllustPaySuspen = 2 ' Con suspensión de pagos
		eIllustCurrData = 3 ' Datos actuales
		eIllustPayPlan = 4 ' Plan de pago
		eIllustProjPrem = 5 ' prima proyectada
	End Enum
	
	'%Add: Agrega un nuevo registro a la colección
	Public Function Add(ByRef objClass As Tmp_val669) As Tmp_val669
		If objClass Is Nothing Then
			objClass = New Tmp_val669
		End If
		With objClass
			mCol.Add(objClass, .sKey & .sCertype & .nBranch & .nProduct & .nPolicy & .nCertif & .nYear)
		End With
		
		'Return the object created
		Add = objClass
	End Function
	
	'% Find_Projectlife: Lee la tabla que guarda la ilustración del valor póliza
	Public Function Find_Projectlife(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date) As Boolean
		Dim lrecReaTmp_val669 As eRemoteDB.Execute
		Dim lclsTmp_val669 As Tmp_val669
		
		On Error GoTo Find_Projectlife_Err
		'+ Definición de store procedure REATMP_VAL669 al 04-09-2002 10:42:00
		lrecReaTmp_val669 = New eRemoteDB.Execute
		With lrecReaTmp_val669
			.StoredProcedure = "ReaProjectlife_a"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				Find_Projectlife = True
				Do While Not .EOF
					lclsTmp_val669 = New Tmp_val669
					lclsTmp_val669.sCertype = .FieldToClass("sCertype")
					lclsTmp_val669.nBranch = .FieldToClass("nBranch")
					lclsTmp_val669.nProduct = .FieldToClass("nProduct")
					lclsTmp_val669.nPolicy = .FieldToClass("nPolicy")
					lclsTmp_val669.nCertif = .FieldToClass("nCertif")
					lclsTmp_val669.dEffecdate = .FieldToClass("dEffecdate")
					lclsTmp_val669.nYear = .FieldToClass("nYear")
					lclsTmp_val669.nAge_reinsu = .FieldToClass("nAge_reinsu")
					lclsTmp_val669.nAmodepacum = .FieldToClass("nAmodepacum")
					lclsTmp_val669.nValpolig = .FieldToClass("nValpolig")
					lclsTmp_val669.nValsurig = .FieldToClass("nValsurig")
					lclsTmp_val669.nProdeathig = .FieldToClass("nProdeathig")
					lclsTmp_val669.nValpolimg = .FieldToClass("nValpolimg")
					lclsTmp_val669.nValsurimg = .FieldToClass("nValsurimg")
					lclsTmp_val669.nProdeathimg = .FieldToClass("nProdeathimg")
					Call Add(lclsTmp_val669)
					'UPGRADE_NOTE: Object lclsTmp_val669 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
					lclsTmp_val669 = Nothing
					.RNext()
				Loop 
				.RCloseRec()
			End If
		End With
		
Find_Projectlife_Err: 
		If Err.Number Then
			Find_Projectlife = False
		End If
		'UPGRADE_NOTE: Object lrecReaTmp_val669 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecReaTmp_val669 = Nothing
		On Error GoTo 0
	End Function
	
	'% Find: Lee la tabla que guarda la ilustración del valor póliza
	Public Function Find(ByVal sKey As String) As Boolean
		Dim lrecReaTmp_val669 As eRemoteDB.Execute
		Dim lclsTmp_val669 As Tmp_val669
		
		On Error GoTo Find_Err
		'+ Definición de store procedure REATMP_VAL669 al 04-09-2002 10:42:00
		lrecReaTmp_val669 = New eRemoteDB.Execute
		With lrecReaTmp_val669
			.StoredProcedure = "ReaTmp_val669"
			.Parameters.Add("sKey", sKey, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				Find = True
				Do While Not .EOF
					lclsTmp_val669 = New Tmp_val669
					lclsTmp_val669.sCertype = .FieldToClass("sCertype")
					lclsTmp_val669.nBranch = .FieldToClass("nBranch")
					lclsTmp_val669.nProduct = .FieldToClass("nProduct")
					lclsTmp_val669.nPolicy = .FieldToClass("nPolicy")
					lclsTmp_val669.nCertif = .FieldToClass("nCertif")
					lclsTmp_val669.dEffecdate = .FieldToClass("dEffecdate")
					lclsTmp_val669.nYear = .FieldToClass("nYear")
					lclsTmp_val669.nAge_reinsu = .FieldToClass("nAge_reinsu")
					lclsTmp_val669.nAmodepacum = .FieldToClass("nAmodepacum")
					lclsTmp_val669.nValpolig = .FieldToClass("nValpolig")
					lclsTmp_val669.nValsurig = .FieldToClass("nValsurig")
					lclsTmp_val669.nProdeathig = .FieldToClass("nProdeathig")
					lclsTmp_val669.nValpolimg = .FieldToClass("nValpolimg")
					lclsTmp_val669.nValsurimg = .FieldToClass("nValsurimg")
					lclsTmp_val669.nProdeathimg = .FieldToClass("nProdeathimg")
					Call Add(lclsTmp_val669)
					'UPGRADE_NOTE: Object lclsTmp_val669 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
					lclsTmp_val669 = Nothing
					.RNext()
				Loop 
				.RCloseRec()
			End If
		End With
		
Find_Err: 
		If Err.Number Then
			Find = False
		End If
		'UPGRADE_NOTE: Object lrecReaTmp_val669 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecReaTmp_val669 = Nothing
		On Error GoTo 0
	End Function
	
	'%InsCalValuePolIlustration: Llama al procedimiento que cálcula la ilustración del
	'%                           valor póliza
	Public Function InsCalValuePolIlustration(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, ByVal nIllustType As eIllustType, ByVal nUsercode As Integer, ByVal nSessionId As String, Optional ByVal nProjRent As Double = eRemoteDB.Constants.intNull, Optional ByVal nAddpremium As Double = eRemoteDB.Constants.intNull, Optional ByVal nSurrMonth As Integer = eRemoteDB.Constants.intNull, Optional ByVal nSurrYear As Integer = eRemoteDB.Constants.intNull, Optional ByVal nSurrAmount As Double = eRemoteDB.Constants.intNull, Optional ByVal nPremdeal As Double = eRemoteDB.Constants.intNull, Optional ByVal sPremdeal As String = "", Optional ByVal bReadTable As Boolean = True, Optional ByVal nYear_end As Integer = 0) As Boolean
		Dim lrecInsCalIllustration As eRemoteDB.Execute
		Dim lstrKey As String
		
		On Error GoTo InsCalIllustration_Err
		lrecInsCalIllustration = New eRemoteDB.Execute
		lstrKey = sKey(nUsercode, nSessionId)
		If sPremdeal <> "1" Then
			nPremdeal = eRemoteDB.Constants.intNull
		End If
		
		'+ Definición de store procedure InsCalIllustration al 04-09-2002 13:29:16
		With lrecInsCalIllustration
			.StoredProcedure = "InsCalIllustration"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nIllusttype", nIllustType, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sKey", lstrKey, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProjrent", nProjRent, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 4, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAddpremium", nAddpremium, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nSurrmonth", nSurrMonth, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nSurryear", nSurrYear, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nSurramount", nSurrAmount, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nValpolig", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sCre_rec", "1", eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPremdeal", nPremdeal, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nModulec", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nYear_end", nYear_end, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			InsCalValuePolIlustration = .Run(False)
		End With
		
		If InsCalValuePolIlustration Then
			If bReadTable Then
				InsCalValuePolIlustration = Find(lstrKey)
			End If
		End If
		
InsCalIllustration_Err: 
		If Err.Number Then
			InsCalValuePolIlustration = False
		End If
		'UPGRADE_NOTE: Object lrecInsCalIllustration may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecInsCalIllustration = Nothing
		On Error GoTo 0
	End Function
	
	'%sKey. Esta propiedad se encarga de devolver la llave de lectura del registro de coberturas
	Public ReadOnly Property sKey(ByVal nUsercode As Integer, ByVal nSessionId As String) As String
		Get
			sKey = "TMP" & CStr(nSessionId) & "-" & CStr(nUsercode)
		End Get
	End Property
	'* Item: Devuelve un elemento de la colección (segun índice)
	Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As Tmp_val669
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
	
	'%InsShowIlustration: Muestra la ilustración de la poliza/certificado
	Public Function InsShowIlustration(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, ByVal nIllustType As eIllustType, ByVal nUsercode As Integer, ByVal nSessionId As String, Optional ByVal nProjRent As Double = eRemoteDB.Constants.intNull, Optional ByVal nAddpremium As Double = eRemoteDB.Constants.intNull, Optional ByVal nSurrMonth As Integer = eRemoteDB.Constants.intNull, Optional ByVal nSurrYear As Integer = eRemoteDB.Constants.intNull, Optional ByVal nSurrAmount As Double = eRemoteDB.Constants.intNull, Optional ByVal nPremdeal As Double = eRemoteDB.Constants.intNull, Optional ByVal sPremdeal As String = "", Optional ByVal bReadTable As Boolean = True, Optional ByVal bQuery As Boolean = False, Optional ByVal nYear_end As Integer = 0) As Boolean
		If bQuery Then
			InsShowIlustration = Find_Projectlife(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate)
		Else
			InsShowIlustration = InsCalValuePolIlustration(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate, nIllustType, nUsercode, nSessionId, nProjRent, nAddpremium, nSurrMonth, nSurrYear, nSurrAmount, nPremdeal, sPremdeal, bReadTable, nYear_end)
		End If
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
End Class






