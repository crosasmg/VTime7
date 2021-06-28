Option Strict Off
Option Explicit On
'UPGRADE_NOTE: Error was upgraded to ErrorTyp. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
Public Class ErrorTyp
	'+
	'+ Estructura de tabla insudb.t_err_interface al 20-08-2004
	'+     Property                Type
	'+----------------------------------------
	Public sKey As String
	Public nRow As Integer
	Public nSeq As Integer
	Public nError As Integer
	Public sDescript As String
	
	'%Class_Initialize: Inicializa las propiedades cuando se instancia la clase
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		
		sKey = strNull
		nRow = numNull
		nSeq = numNull
		nError = numNull
		sDescript = strNull
		
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
End Class






