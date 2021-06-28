Option Strict Off
Option Explicit On
Public Class Tab_docus
	Implements System.Collections.IEnumerable
	'%-------------------------------------------------------%'
	'% $Workfile:: Tab_docus.cls                            $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 1:34p                                $%'
	'% $Revision:: 5                                        $%'
	'%-------------------------------------------------------%'
	
	'**-Local variable for the collection handle
	'- Variable local para el manejo de la coleccion
	Private mCol As Collection
	
	'**%Add: adds a new element to the collection
	'% Add: añade un nuevo elemento a la colección
	Public Function Add(ByVal sClaimpay As String, ByVal nUsercode As Integer, ByVal sStatregt As String, ByVal sShort_des As String, ByVal sDescript As String, ByVal nDoc_code As Integer, ByVal nDays_presc As Integer, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nModulec As Integer, ByVal nCover As Integer, ByVal nCauscod As Integer, Optional ByVal sKey As String = "") As Tab_docu
		
		Dim objNewMember As Tab_docu
		objNewMember = New Tab_docu
		
		On Error GoTo Add_err
		
		With objNewMember
			.sClaimpay = sClaimpay
			.nUsercode = nUsercode
			.sStatregt = sStatregt
			.nDays_presc = nDays_presc
			.sShort_des = sShort_des
			.sDescript = sDescript
			.dCompdate = Today
			.nDoc_code = nDoc_code
			.nBranch = nBranch
			.nProduct = nProduct
			.nModulec = nModulec
			.nCover = nCover
			.nCauscod = nCauscod
		End With
		
		If Len(sKey) = 0 Then
			mCol.Add(objNewMember)
		Else
			mCol.Add(objNewMember, "TD" & nBranch & nProduct & nModulec & nCover & nCauscod & nDoc_code)
		End If
		
		Add = objNewMember
		objNewMember = Nothing
		
Add_err: 
		If Err.Number Then
            Add = Nothing
		End If
		On Error GoTo 0
	End Function
	
	Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As Tab_docu
		Get
			Item = mCol.Item(vntIndexKey)
		End Get
	End Property
	
	Public ReadOnly Property Count() As Integer
		Get
			Count = mCol.Count()
		End Get
	End Property
	
	'UPGRADE_NOTE: NewEnum property was commented out. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="B3FC1610-34F3-43F5-86B7-16C984F0E88E"'
	'Public ReadOnly Property NewEnum() As stdole.IUnknown
		'Get
			'NewEnum = mCol._NewEnum
		'End Get
	'End Property
	
	Public Function GetEnumerator() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
				GetEnumerator = mCol.GetEnumerator
	End Function
	
	Public Sub Remove(ByRef vntIndexKey As Object)
		mCol.Remove(vntIndexKey)
	End Sub
	
	Private Sub Class_Initialize_Renamed()
		mCol = New Collection
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
	
	Private Sub Class_Terminate_Renamed()
		mCol = Nothing
	End Sub
	Protected Overrides Sub Finalize()
		Class_Terminate_Renamed()
		MyBase.Finalize()
	End Sub
	
	'Find: Esta función realiza la lectura y carga de la información de las causas de siniestro en el Tdbgrid.
	Public Function Find(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nModulec As Integer, ByVal nCover As Integer, ByVal nCauscod As Integer) As Boolean
		Dim lrecTab_docu As eRemoteDB.Execute
		
		On Error GoTo Find_Err
		
		lrecTab_docu = New eRemoteDB.Execute
		
		With lrecTab_docu
			.StoredProcedure = "reaTab_docu_a"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nModulec", nModulec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCover", nCover, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCauscod", nCauscod, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run Then
				Do While Not .EOF
					Call Add(.FieldToClass("sClaimPay"), eRemoteDB.Constants.intNull, .FieldToClass("sStatregt"), .FieldToClass("sShort_des"), .FieldToClass("sDescript"), .FieldToClass("nDoc_code"), .FieldToClass("nDays_presc"), nBranch, nProduct, nModulec, nCover, nCauscod)
					.RNext()
				Loop 
				.RCloseRec()
				Find = True
			Else
				Find = False
			End If
		End With
		lrecTab_docu = Nothing
		
Find_Err: 
		If Err.Number Then
			Find = False
		End If
		On Error GoTo 0
		
	End Function
End Class






