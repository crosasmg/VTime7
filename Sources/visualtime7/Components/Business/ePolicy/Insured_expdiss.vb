Option Strict Off
Option Explicit On
Public Class Insured_expdiss
	Implements System.Collections.IEnumerable
	'%-------------------------------------------------------%'
	'% $Workfile:: Insured_expdiss.cls                      $%'
	'% $Author:: Nvaplat41                                  $%'
	'% $Date:: 9/10/03 19.01                                $%'
	'% $Revision:: 16                                       $%'
	'%-------------------------------------------------------%'
	'+ variable local de la colección
	Private mCol As Collection
	
	'%Add: Agrega datos a la colección
	Public Function Add(ByVal objClass As Insured_expdis) As Insured_expdis
		'+ crea un nuevo objeto
		If objClass Is Nothing Then
			objClass = New Insured_expdis
		End If
		
		With objClass
			mCol.Add(objClass, .sCertype & .nBranch & .nProduct & .nPolicy & .nCertif & .sClient & .nDisexprc & .nModulec & .nCover & .dEffecdate.ToString("yyyyMMdd"))
		End With
		
		'+ retorna el objeto creado
		Add = objClass
		'UPGRADE_NOTE: Object objClass may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		objClass = Nothing
		
	End Function
	
	'%Find: Lee los datos de la tabla para la transacción VI681
	Public Function Find(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal sClient As String, ByVal dEffecdate As Date) As Boolean
		Dim lrecReaInsured_expdis_a As eRemoteDB.Execute
		Dim lclsinsured_expdis As Insured_expdis
		lrecReaInsured_expdis_a = New eRemoteDB.Execute
		
		On Error GoTo Find_Err
		
		With lrecReaInsured_expdis_a
			.StoredProcedure = "ReaInsured_expdis_a"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				Find = True
				Do While Not .EOF
					lclsinsured_expdis = New Insured_expdis
					lclsinsured_expdis.sSel = .FieldToClass("sSel")
					lclsinsured_expdis.sCertype = .FieldToClass("sCertype")
					lclsinsured_expdis.nBranch = .FieldToClass("nBranch")
					lclsinsured_expdis.nProduct = .FieldToClass("nProduct")
					lclsinsured_expdis.nPolicy = .FieldToClass("nPolicy")
					lclsinsured_expdis.nCertif = .FieldToClass("nCertif")
					lclsinsured_expdis.sClient = .FieldToClass("sClient")
					lclsinsured_expdis.nDisexprc = .FieldToClass("nDisexprc")
					lclsinsured_expdis.nModulec = .FieldToClass("nModulec")
					lclsinsured_expdis.nCover = .FieldToClass("nCover")
					lclsinsured_expdis.dEffecdate = .FieldToClass("dEffecdate")
					lclsinsured_expdis.sDisexpri = .FieldToClass("sDisexpri")
					lclsinsured_expdis.sUnit = .FieldToClass("sUnit")
					lclsinsured_expdis.nRate = .FieldToClass("nRate")
					lclsinsured_expdis.nAmount = .FieldToClass("nAmount")
					lclsinsured_expdis.sPerm_temp = .FieldToClass("sPerm_Temp")
					lclsinsured_expdis.dDate_fr = .FieldToClass("dDate_Fr")
					lclsinsured_expdis.dDate_to = .FieldToClass("dDate_to")
					lclsinsured_expdis.nAge = .FieldToClass("nAge")
					lclsinsured_expdis.nNotenum = .FieldToClass("nNotenum")
					lclsinsured_expdis.nCause = .FieldToClass("nCause")
					lclsinsured_expdis.sAgree = .FieldToClass("sAgree")
					lclsinsured_expdis.sCoverBase = .FieldToClass("sBaseCover")
                    lclsinsured_expdis.sInitdate_Calc = .FieldToClass("sInitdate_Calc")
                    lclsinsured_expdis.nActivity = .FieldToClass("nActivity")
                    lclsinsured_expdis.nSport = .FieldToClass("nSport")
			
					Call Add(lclsinsured_expdis)
					.RNext()
					'UPGRADE_NOTE: Object lclsinsured_expdis may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
					lclsinsured_expdis = Nothing
				Loop 
				.RCloseRec()
			End If
		End With
		
Find_Err: 
		If Err.Number Then
			Find = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecReaInsured_expdis_a may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecReaInsured_expdis_a = Nothing
	End Function
	
	'% Item: Esta propiedad es usada cuando se hace referencia a un elemento de la colección.
	Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As Insured_expdis
		Get
			Item = mCol.Item(vntIndexKey)
		End Get
	End Property
	
	'% Count: Retorna la contidad de elementos existentes en la colección.
	Public ReadOnly Property Count() As Integer
		Get
			Count = mCol.Count()
		End Get
	End Property
	
	'% NewEnum: Esta propiedad permite enumerar los elementos de la colección por medio de un "For...Each".
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
	
	'% Remove: Permite eliminar un elemento de la colección.
	Public Sub Remove(ByRef vntIndexKey As Object)
		mCol.Remove(vntIndexKey)
	End Sub
	
	'% Class_Initialize: Crea la colección cuando se crea esta clase.
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		mCol = New Collection
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
	
	'% Class_Terminate: Destruye la colección cuando se termina esta clase.
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






