Option Strict Off
Option Explicit On
Public Class Sport
	'%-------------------------------------------------------%'
	'% $Workfile:: Sport.cls                                $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 1:30p                                $%'
	'% $Revision:: 7                                        $%'
	'%-------------------------------------------------------%'
	
	'+
	'+ Estructura de tabla insudb.sport al 02-19-2002 12:41:24
	'+     Property                Type         DBType   Size Scale  Prec  Null
	'+-------------------------------------------------------------------------
	Public nSport As Integer ' NUMBER     22   0     5    N
	Public sClient As String ' CHAR       14   0     0    N
	Public dCompdate As Date ' DATE       7    0     0    N
	Public nUsercode As Integer ' NUMBER     22   0     5    N
	Public sSel As String ' CHAR       1
	Public sDescript As String ' CHAR       40
	
	'+ Cadena con todos los deportes seleccionados
	Public sSport_cad As String
	
	'% InsUpdSport: Se encarga de actualizar la tabla Sport
	Private Function InsUpdSport() As Boolean
		
		'-Se define variable para realizar operaciones a la BD
		Dim lrecinsUpdsport As eRemoteDB.Execute
		
		On Error GoTo insUpdsport_Err
		lrecinsUpdsport = New eRemoteDB.Execute
		
		With lrecinsUpdsport
			.StoredProcedure = "insUpdsport"
			.Parameters.Add("sSport_cad", sSport_cad, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 200, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			InsUpdSport = .Run(False)
		End With
		
insUpdsport_Err: 
		If Err.Number Then
			InsUpdSport = False
		End If
		'UPGRADE_NOTE: Object lrecinsUpdsport may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecinsUpdsport = Nothing
		On Error GoTo 0
	End Function
	
	'% InsPostBC007S: Ejecuta el post de la transacción
	'%                Tabla de control de prima mínima(BC007S)
	Public Function InsPostBC007S(ByVal sSport_cad As String, ByVal sClient As String, ByVal nUsercode As Integer) As Boolean
		
		On Error GoTo InsPostBC007S_Err
		
		With Me
			.sSport_cad = sSport_cad
			.sClient = sClient
			.nUsercode = nUsercode
		End With
		
		InsPostBC007S = InsUpdSport
		
InsPostBC007S_Err: 
		If Err.Number Then
			InsPostBC007S = False
		End If
		On Error GoTo 0
	End Function
	
	'% Class_Initialize: Inicializa las propiedades cuando se instancia la clase
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		nSport = eRemoteDB.Constants.intNull
		sClient = String.Empty
		dCompdate = eRemoteDB.Constants.dtmNull
		nUsercode = eRemoteDB.Constants.intNull
		sSport_cad = String.Empty
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
End Class






