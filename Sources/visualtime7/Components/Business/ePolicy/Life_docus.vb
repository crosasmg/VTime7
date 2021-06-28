Option Strict Off
Option Explicit On
Public Class Life_docus
	Implements System.Collections.IEnumerable
	'%-------------------------------------------------------%'
	'% $Workfile:: Life_docus.cls                           $%'
	'% $Author:: Nvaplat28                                  $%'
	'% $Date:: 4/10/04 12.42                                $%'
	'% $Revision:: 17                                       $%'
	'%-------------------------------------------------------%'
	
	'- Variable que indica si se eliminan los documentos requeridos a un cliente por modificación en la CA014
	Public sDel_docu As String
	
	'-Variable local para el manejo de la coleccion
	Private mCol As Collection
	
	'% Add: Agrega un objeto a la colección
	Public Function Add(ByRef objClass As Life_docu) As Life_docu
		If objClass Is Nothing Then
			objClass = New Life_docu
		End If
		
		mCol.Add(objClass)
		
		Add = objClass
	End Function
	
	'%Find: Lee los datos de la tabla
	Public Function Find(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, ByVal nUsercode As Integer, ByVal sKey As String, ByVal nTransaction As Integer, ByVal sExecute As String) As Boolean
		Dim lrecInsPrevi021 As eRemoteDB.Execute
		Dim lclsLife_docu As Life_docu
		
		On Error GoTo InsPrevi021_Err
		
		'+ Definición de store procedure InsPrevi021 al 08-27-2002 17:05:13
		lrecInsPrevi021 = New eRemoteDB.Execute
		With lrecInsPrevi021
			.StoredProcedure = "InsPrevi021"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sKey", sKey, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTransaction", nTransaction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sExecute", sExecute, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sDel_docu", sDel_docu, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCommit", 1, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("NoSequence", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProdclas", eRemoteDB.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sDocutyp", "", eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sPolitype", "", eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If .Run Then
                Find = True
                Do While Not .EOF
                    lclsLife_docu = New Life_docu
                    lclsLife_docu.sKey = .FieldToClass("sKey")
                    lclsLife_docu.nExist = .FieldToClass("nExist")
                    lclsLife_docu.sDescript = .FieldToClass("sDescript")
                    lclsLife_docu.nCrthecni = .FieldToClass("nCrthecni")
                    lclsLife_docu.dRecep_date = .FieldToClass("dRecep_date")
                    lclsLife_docu.nStat_docReq = .FieldToClass("nStat_docreq")
                    lclsLife_docu.nModulec = .FieldToClass("nModulec")
                    lclsLife_docu.nCover = .FieldToClass("nCover")
                    lclsLife_docu.nRole = .FieldToClass("nRole")
                    lclsLife_docu.sClient = .FieldToClass("sClient")
                    lclsLife_docu.dDate_to = .FieldToClass("dDate_to")
                    lclsLife_docu.dDatefree = .FieldToClass("dDatefree")
                    lclsLife_docu.nEval = .FieldToClass("nEval")
                    lclsLife_docu.dDatevig = .FieldToClass("dDatevig")
                    lclsLife_docu.nNotenum = .FieldToClass("nNotenum")
                    lclsLife_docu.nCumul = .FieldToClass("nCumul")
                    lclsLife_docu.nStatusdoc = .FieldToClass("nStatusdoc")
                    lclsLife_docu.dDocreq = .FieldToClass("dDocreq")
                    lclsLife_docu.dDocrec = .FieldToClass("dDocrec")
                    lclsLife_docu.dExpirdat = .FieldToClass("dExpirdat")
                    lclsLife_docu.nNotenum_cli = .FieldToClass("nNotenum_cli")
                    lclsLife_docu.nEval_master = .FieldToClass("nEval_master")
                    lclsLife_docu.nId = .FieldToClass("nId")
                    lclsLife_docu.sRequest = IIf(.FieldToClass("sRequest") = "1", "1", "2")
                    lclsLife_docu.nEval_Gen = .FieldToClass("nEval_Gen")
                    lclsLife_docu.nStatus_eval = .FieldToClass("nStatus_eval")
                    Call Add(lclsLife_docu)
                    'UPGRADE_NOTE: Object lclsLife_docu may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
                    lclsLife_docu = Nothing
                    .RNext()
                Loop
                .RCloseRec()
            End If
		End With
		
InsPrevi021_Err: 
		If Err.Number Then
			Find = False
		End If
		'UPGRADE_NOTE: Object lrecInsPrevi021 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecInsPrevi021 = Nothing
		On Error GoTo 0
	End Function
	
	'% Item: Esta propiedad es usada cuando se hace referencia a un elemento de la colección.
	Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As Life_docu
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






