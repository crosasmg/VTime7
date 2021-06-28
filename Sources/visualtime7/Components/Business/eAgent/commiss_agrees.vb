Option Strict Off
Option Explicit On
Public Class commiss_agrees
	Implements System.Collections.IEnumerable
	'%-------------------------------------------------------%'
	'% $Workfile:: commiss_agrees.cls                       $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 1:41p                                $%'
	'% $Revision:: 5                                        $%'
	'%-------------------------------------------------------%'
	
	Private mcolcommiss_agree As Collection
	
	'% Add: Añade a la clase los registros encontrados en la colección
	Public Function Add(ByVal sClient As String, ByVal nAgreement As Integer, ByVal dInit_Date As Date, ByVal dEnd_Date As Date, ByVal nPerc_Comm As Double, Optional ByRef sKey As String = "") As commiss_agree
		Dim lclscommiss_agree As commiss_agree
		
		lclscommiss_agree = New commiss_agree
		
		With lclscommiss_agree
			.sClient = sClient
			.nAgreement = nAgreement
			.dInit_Date = dInit_Date
			.dEnd_Date = dEnd_Date
			.nPerc_Comm = nPerc_Comm
		End With
		
		'+ Asignación de valores a las propiedades pasadas dentro del método
		If sKey = String.Empty Then
			mcolcommiss_agree.Add(lclscommiss_agree)
		Else
			mcolcommiss_agree.Add(lclscommiss_agree, sKey)
		End If
		
		'+ Se retorna el objeto creado
		Add = lclscommiss_agree
		'UPGRADE_NOTE: Object lclscommiss_agree may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclscommiss_agree = Nothing
	End Function
	
	'Find: Función que realiza la busqueda en la tabla 'commiss_agree'
	Public Function Find() As Boolean
		Dim lclscommiss_agree As eRemoteDB.Execute
		
		lclscommiss_agree = New eRemoteDB.Execute
		
		'+ Definición de parámetros para el stored procedure 'insudb.reacommiss_agree'. Generado el 12/11/2001 05:32:25 p.m.
		With lclscommiss_agree
			.StoredProcedure = "reacommiss_agree_a"
			
			If .Run(True) Then
				Do While Not .EOF
					Call Add(.FieldToClass("sClient"), .FieldToClass("nAgreement"), .FieldToClass("dInit_Date"), .FieldToClass("dEnd_Date"), .FieldToClass("nPerc_Comm"))
					.RNext()
				Loop 
				Find = True
				.RCloseRec()
				
			Else
				Find = False
			End If
		End With
		'UPGRADE_NOTE: Object lclscommiss_agree may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclscommiss_agree = Nothing
	End Function
	
	Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As commiss_agree
		Get
			Item = mcolcommiss_agree.Item(vntIndexKey)
		End Get
	End Property
	
	Public ReadOnly Property Count() As Integer
		Get
			Count = mcolcommiss_agree.Count()
		End Get
	End Property
	
	'UPGRADE_NOTE: NewEnum property was commented out. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="B3FC1610-34F3-43F5-86B7-16C984F0E88E"'
	'Public ReadOnly Property NewEnum() As stdole.IUnknown
		'Get
			'NewEnum = mcolcommiss_agree._NewEnum
		'End Get
	'End Property
	
	Public Function GetEnumerator() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
		'UPGRADE_TODO: Uncomment and change the following line to return the collection enumerator. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="95F9AAD0-1319-4921-95F0-B9D3C4FF7F1C"'
		GetEnumerator = mcolcommiss_agree.GetEnumerator
	End Function
	
	Public Sub Remove(ByRef vntIndexKey As Object)
		mcolcommiss_agree.Remove(vntIndexKey)
	End Sub
	
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		mcolcommiss_agree = New Collection
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
	
	'UPGRADE_NOTE: Class_Terminate was upgraded to Class_Terminate_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Terminate_Renamed()
		'UPGRADE_NOTE: Object mcolcommiss_agree may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		mcolcommiss_agree = Nothing
	End Sub
	Protected Overrides Sub Finalize()
		Class_Terminate_Renamed()
		MyBase.Finalize()
	End Sub
End Class






