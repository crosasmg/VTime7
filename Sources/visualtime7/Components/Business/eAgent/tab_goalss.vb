Option Strict Off
Option Explicit On
Public Class tab_goalss
	Implements System.Collections.IEnumerable
	'%-------------------------------------------------------%'
	'% $Workfile:: tab_goalss.cls                           $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 1:41p                                $%'
	'% $Revision:: 3                                        $%'
	'%-------------------------------------------------------%'
	
	Private mcoltab_goals As Collection
	
	'%Add: Añade una nueva instancia de la clase "tab_goals" a la colección
	Public Function Add(ByVal nCode As Double, ByVal sDescript As String, ByVal sShort_des As String, ByVal sStatRegt As String, Optional ByRef sKey As String = "") As tab_goals
		Dim lclstab_goals As tab_goals
		
		lclstab_goals = New tab_goals
		
		With lclstab_goals
			.nCode = nCode
			.sDescript = sDescript
			.sShort_des = sShort_des
			.sStatRegt = sStatRegt
		End With
		
		'set the properties passed into the method
		If sKey = String.Empty Then
			mcoltab_goals.Add(lclstab_goals)
		Else
			mcoltab_goals.Add(lclstab_goals, sKey)
		End If
		
		'return the object created
		Add = lclstab_goals
		'UPGRADE_NOTE: Object lclstab_goals may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclstab_goals = Nothing
	End Function
	
	'Find: Función que realiza la busqueda en la tabla 'tab_goals'
	Public Function Find() As Boolean
		Dim lclstab_goals As eRemoteDB.Execute
		
		lclstab_goals = New eRemoteDB.Execute
		
		'+ Define all parameters for the stored procedures 'insudb.reatab_goals'. Generated on 17/01/2002 09:51:46 a.m.
		With lclstab_goals
			.StoredProcedure = "reatab_goals_a"
			
			If .Run(True) Then
				Do While Not .EOF
					Call Add(.FieldToClass("nCode"), .FieldToClass("sDescript"), .FieldToClass("sshort_des"), .FieldToClass("sStatRegt"))
					.RNext()
				Loop 
				Find = True
				.RCloseRec()
				
			Else
				Find = False
			End If
		End With
		'UPGRADE_NOTE: Object lclstab_goals may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclstab_goals = Nothing
	End Function
	
	Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As tab_goals
		Get
			Item = mcoltab_goals.Item(vntIndexKey)
		End Get
	End Property
	
	Public ReadOnly Property Count() As Integer
		Get
			Count = mcoltab_goals.Count()
		End Get
	End Property
	
	'UPGRADE_NOTE: NewEnum property was commented out. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="B3FC1610-34F3-43F5-86B7-16C984F0E88E"'
	'Public ReadOnly Property NewEnum() As stdole.IUnknown
		'Get
			'NewEnum = mcoltab_goals._NewEnum
		'End Get
	'End Property
	
	Public Function GetEnumerator() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
		'UPGRADE_TODO: Uncomment and change the following line to return the collection enumerator. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="95F9AAD0-1319-4921-95F0-B9D3C4FF7F1C"'
		GetEnumerator = mcoltab_goals.GetEnumerator
	End Function
	
	Public Sub Remove(ByRef vntIndexKey As Object)
		mcoltab_goals.Remove(vntIndexKey)
	End Sub
	
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		mcoltab_goals = New Collection
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
	
	'UPGRADE_NOTE: Class_Terminate was upgraded to Class_Terminate_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Terminate_Renamed()
		'UPGRADE_NOTE: Object mcoltab_goals may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		mcoltab_goals = Nothing
	End Sub
	Protected Overrides Sub Finalize()
		Class_Terminate_Renamed()
		MyBase.Finalize()
	End Sub
End Class






