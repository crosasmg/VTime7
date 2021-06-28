Option Strict Off
Option Explicit On
Public Class excess_maints
	Implements System.Collections.IEnumerable
	'%-------------------------------------------------------%'
	'% $Workfile:: excess_maints.cls                        $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 1:41p                                $%'
	'% $Revision:: 7                                        $%'
	'%-------------------------------------------------------%'
	
	Private mcolexcess_maint As Collection
	
	'%Add: Añade una nueva instancia de la clase "excess_maint" a la colección
	Public Function Add(ByVal nInterTyp As Integer, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nType_hist As Integer, ByVal nDet_transac As Integer, ByVal nInitRange As Double, ByVal nEndRange As Double, ByVal nPercent As Double, ByVal nAmount As Double, Optional ByRef sKey As String = "") As excess_maint
		Dim lclsexcess_maint As excess_maint
		
		lclsexcess_maint = New excess_maint
		
		With lclsexcess_maint
			.nInterTyp = nInterTyp
			.nBranch = nBranch
			.nProduct = nProduct
			.nType_hist = nType_hist
			.nDet_transac = nDet_transac
			.nInitRange = nInitRange
			.nEndRange = nEndRange
			.nPercent = nPercent
			.nAmount = nAmount
		End With
		
		'set the properties passed into the method
		If sKey = String.Empty Then
			mcolexcess_maint.Add(lclsexcess_maint)
		Else
			mcolexcess_maint.Add(lclsexcess_maint, sKey)
		End If
		
		'return the object created
		Add = lclsexcess_maint
		'UPGRADE_NOTE: Object lclsexcess_maint may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsexcess_maint = Nothing
	End Function
	
	'Find: Función que realiza la busqueda en la tabla 'excess_maint'
	Public Function Find(ByVal lintIntertyp As Integer, ByVal lintBranch As Integer, ByVal lintProduct As Integer) As Boolean
		Dim lclsexcess_maint As eRemoteDB.Execute
		
		lclsexcess_maint = New eRemoteDB.Execute
		
		'+ Define all parameters for the stored procedures 'insudb.reaexcess_maint'. Generated on 19/12/2001 02:52:37 p.m.
		With lclsexcess_maint
			.StoredProcedure = "reaexcess_maint_a"
			.Parameters.Add("nIntertyp", lintIntertyp, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", lintBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", lintProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(True) Then
				Do While Not .EOF
					Call Add(.FieldToClass("nIntertyp"), .FieldToClass("nBranch"), .FieldToClass("nProduct"), .FieldToClass("nType_hist"), .FieldToClass("nDet_transac"), .FieldToClass("nInitRange"), .FieldToClass("nEndRange"), .FieldToClass("nPercent"), .FieldToClass("nAmount"))
					.RNext()
				Loop 
				Find = True
				.RCloseRec()
				
			Else
				Find = False
			End If
		End With
		'UPGRADE_NOTE: Object lclsexcess_maint may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsexcess_maint = Nothing
	End Function
	
	Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As excess_maint
		Get
			Item = mcolexcess_maint.Item(vntIndexKey)
		End Get
	End Property
	
	Public ReadOnly Property Count() As Integer
		Get
			Count = mcolexcess_maint.Count()
		End Get
	End Property
	
	'UPGRADE_NOTE: NewEnum property was commented out. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="B3FC1610-34F3-43F5-86B7-16C984F0E88E"'
	'Public ReadOnly Property NewEnum() As stdole.IUnknown
		'Get
			'NewEnum = mcolexcess_maint._NewEnum
		'End Get
	'End Property
	
	Public Function GetEnumerator() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
		'UPGRADE_TODO: Uncomment and change the following line to return the collection enumerator. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="95F9AAD0-1319-4921-95F0-B9D3C4FF7F1C"'
		GetEnumerator = mcolexcess_maint.GetEnumerator
	End Function
	
	Public Sub Remove(ByRef vntIndexKey As Object)
		mcolexcess_maint.Remove(vntIndexKey)
	End Sub
	
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		mcolexcess_maint = New Collection
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
	
	'UPGRADE_NOTE: Class_Terminate was upgraded to Class_Terminate_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Terminate_Renamed()
		'UPGRADE_NOTE: Object mcolexcess_maint may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		mcolexcess_maint = Nothing
	End Sub
	Protected Overrides Sub Finalize()
		Class_Terminate_Renamed()
		MyBase.Finalize()
	End Sub
End Class






