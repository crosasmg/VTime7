Option Strict Off
Option Explicit On
Public Class Sequen_pols
	Implements System.Collections.IEnumerable
	'%-------------------------------------------------------%'
	'% $Workfile:: Sequen_pols.cls                          $%'
	'% $Author:: Nvaplat18                                  $%'
	'% $Date:: 6/10/03 17.23                                $%'
	'% $Revision:: 12                                       $%'
	'%-------------------------------------------------------%'
	
	Private mCol As Collection
	
	'- Variables auxiliares
	
	Private mintBranch As Integer
	Private mintProduct As Integer
	
	'% Add: Añade una nueva instancia de la clase Sequen_pol a la colección
	Public Function Add(ByRef objElement As Sequen_pol) As Sequen_pol
		mCol.Add(objElement)
		Add = objElement
		'UPGRADE_NOTE: Object objElement may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		objElement = Nothing
	End Function
	
	'% Find: Devuelve la información de los clientes permitidos del producto en tratamiento
	Public Function Find(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal sBussityp As String, ByVal nTratypep As Integer, ByVal sPolitype As String, ByVal sCompon As String, ByVal dEffecdate As Date, ByVal sCodispl As String, Optional ByVal lblnFind As Boolean = False, Optional ByVal nType_Amend As Short = 0) As Boolean
		Dim lrecreaSequen_pol As eRemoteDB.Execute
		Dim lclsSequen_pol As eProduct.Sequen_pol
		
		On Error GoTo Find_Err
		
		If mintBranch <> nBranch Or mintProduct <> nProduct Or lblnFind Then
			
			lrecreaSequen_pol = New eRemoteDB.Execute
			With lrecreaSequen_pol
				.StoredProcedure = "reaSequen_pol"
				.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("sBussityp", sBussityp, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nTratypep", nTratypep, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("sPolitype", sPolitype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("sCompon", sCompon, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("sCodispl", sCodispl, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 8, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nType_Amend", IIf(nType_Amend = -32768, 0, nType_Amend), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				
				If .Run Then
					mintBranch = nBranch
					mintProduct = nProduct
					Do While Not .EOF
						lclsSequen_pol = New eProduct.Sequen_pol
						lclsSequen_pol.nBranch = .FieldToClass("nBranch")
						lclsSequen_pol.nProduct = .FieldToClass("nProduct")
						lclsSequen_pol.sBussityp = .FieldToClass("sBussityp")
						lclsSequen_pol.nTratypep = .FieldToClass("nTratypep")
						lclsSequen_pol.sPolitype = .FieldToClass("sPolitype")
						lclsSequen_pol.sCompon = .FieldToClass("sCompon")
						lclsSequen_pol.Nsequence = .FieldToClass("nSequence")
						lclsSequen_pol.dEffecdate = .FieldToClass("dEffecdate")
						lclsSequen_pol.sCodispl = .FieldToClass("sCodispl")
						lclsSequen_pol.sRequire = .FieldToClass("sRequire")
						lclsSequen_pol.nUsercode = .FieldToClass("nUsercode")
						lclsSequen_pol.nType_Amend = .FieldToClass("nType_Amend")
						Call Add(lclsSequen_pol)
						
						'UPGRADE_NOTE: Object lclsSequen_pol may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
						lclsSequen_pol = Nothing
						.RNext()
					Loop 
					
					.RCloseRec()
					Find = True
				End If
			End With
		Else
			Find = True
		End If
		
Find_Err: 
		If Err.Number Then
			Find = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecreaSequen_pol may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaSequen_pol = Nothing
		'UPGRADE_NOTE: Object lclsSequen_pol may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsSequen_pol = Nothing
	End Function
	
	'% Find_Tab_winpol: Devuelve la información de los clientes permitidos del producto
	'%                  en tratamiento
	Public Function Find_Tab_winpol(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal sBussityp As String, ByVal nTratypep As Integer, ByVal sPolitype As String, ByVal sCompon As String, ByVal dEffecdate As Date, Optional ByVal lblnFind As Boolean = False, Optional ByVal sBrancht As String = "", Optional ByVal nType_Amend As Short = 0) As Boolean
		Dim lrecreaTab_winpol As eRemoteDB.Execute
		Dim lclsSequen_pol As eProduct.Sequen_pol
		
		On Error GoTo Find_Tab_winpol_err
		
		lrecreaTab_winpol = New eRemoteDB.Execute
		
		If mintBranch <> nBranch Or mintProduct <> nProduct Or lblnFind Then
			
			With lrecreaTab_winpol
				.StoredProcedure = "reaTab_winpol"
				.Parameters.Add("sBussityp", sBussityp, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("sCompon", sCompon, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nTratypep", nTratypep, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("sPolitype", sPolitype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("sBrancht", sBrancht, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nType_Amend", IIf(nType_Amend = -32768, 0, nType_Amend), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				
				If .Run Then
					mintBranch = nBranch
					mintProduct = nProduct
					Do While Not .EOF
						lclsSequen_pol = New eProduct.Sequen_pol
						lclsSequen_pol.sBussityp = .FieldToClass("sBussityp")
						lclsSequen_pol.nTratypep = .FieldToClass("nTratypep")
						lclsSequen_pol.sPolitype = .FieldToClass("sPolitype")
						lclsSequen_pol.sCompon = .FieldToClass("sCompon")
						lclsSequen_pol.Nsequence = .FieldToClass("nSequence")
						lclsSequen_pol.sCodispl = .FieldToClass("sCodispl")
						lclsSequen_pol.sRequire = .FieldToClass("sRequire")
						lclsSequen_pol.sDescript = .FieldToClass("sDescript")
						lclsSequen_pol.sRequirePol = .FieldToClass("RequirePol")
						lclsSequen_pol.Codispl_Exist = .FieldToClass("Codispl_Exist")
						lclsSequen_pol.sAutomatic = .FieldToClass("sAutomatic")
						lclsSequen_pol.nType_Amend = .FieldToClass("nType_Amend")
						
						Call Add(lclsSequen_pol)
						'UPGRADE_NOTE: Object lclsSequen_pol may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
						lclsSequen_pol = Nothing
						.RNext()
					Loop 
					
					.RCloseRec()
					Find_Tab_winpol = True
				End If
			End With
		Else
			Find_Tab_winpol = True
		End If
		
Find_Tab_winpol_err: 
		If Err.Number Then
			Find_Tab_winpol = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecreaTab_winpol may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaTab_winpol = Nothing
		'UPGRADE_NOTE: Object lclsSequen_pol may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsSequen_pol = Nothing
	End Function
	
	'* Item: Devuelve un elemento de la colección (segun índice)
	Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As Sequen_pol
		Get
			Item = mCol.Item(vntIndexKey)
		End Get
	End Property
	
	'* Count: Devuelve el número de elementos que posee la colección
	Public ReadOnly Property Count() As Integer
		Get
			Count = mCol.Count
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
End Class






