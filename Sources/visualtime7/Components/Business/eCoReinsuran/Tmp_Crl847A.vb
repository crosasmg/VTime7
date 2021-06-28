Option Strict Off
Option Explicit On
Public Class Tmp_Crl847A
	'%-------------------------------------------------------%'
	'% $Workfile:: Tmp_Crl847A.cls                          $%'
	'% $Author:: Nvaplat17                                  $%'
	'% $Date:: 7/01/04 10:51                                $%'
	'% $Revision:: 3                                        $%'
	'%-------------------------------------------------------%'
	'+
	'+ Estructura de tabla insudb.Tmp_Crl847A al 04-25-2002 17:52:20
	'+         Property                Type         DBType   Size Scale  Prec  Null
	'+-----------------------------------------------------------------------------
	Public sCod_cumulo As String ' CHAR       12   0     0    S
	Public nVal_max_ces_uf As Double ' NUMBER     22   0     5    N
	Public sKey As String
	
	
	'%InsUpdTmp_Crl847A: Se encarga de actualizar la tabla Tmp_Crl847A
	Private Function InsUpdTmp_Crl847A(ByVal nAction As Integer) As Boolean
		Dim lrecinsUpdTmp_Crl847A As eRemoteDB.Execute
		
		On Error GoTo insUpdTmp_Crl847A_Err
		
		lrecinsUpdTmp_Crl847A = New eRemoteDB.Execute
		
		'+
		'+ Definición de store procedure insUpdTmp_Crl847A al 04-25-2002 17:55:43
		'+
		With lrecinsUpdTmp_Crl847A
			.StoredProcedure = "insUpdTmp_Crl847A"
			.Parameters.Add("nAction", nAction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sCod_cumulo", sCod_cumulo, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nVal_max_ces_uf", nVal_max_ces_uf, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sKey", sKey, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			InsUpdTmp_Crl847A = .Run(False)
		End With
		
insUpdTmp_Crl847A_Err: 
		If Err.Number Then
			InsUpdTmp_Crl847A = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecinsUpdTmp_Crl847A may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecinsUpdTmp_Crl847A = Nothing
		
	End Function
	
	'%Add: Crea un registro en la tabla
	Public Function Add() As Boolean
		Add = InsUpdTmp_Crl847A(1)
	End Function
	
	'%Update: Actualiza un registro en la tabla
	Public Function Update() As Boolean
		Update = InsUpdTmp_Crl847A(2)
	End Function
	
	'%Delete: Borra un registro en la tabla
	Public Function Delete() As Boolean
		Delete = InsUpdTmp_Crl847A(3)
	End Function
	
	'%Find: Lee los datos de la tabla
	'Public Function Find(ByVal nServ_Order As Double) As Boolean
	'    Dim lrecreaTmp_Crl847A As eRemotedb.Execute
	'   Dim lclsreaTmp_Crl847A As Tmp_Crl847A
	'
	'   On Error GoTo reaTmp_Crl847A_Err
	'
	'   Set lrecreaTmp_Crl847A = New eRemotedb.Execute
	'
	'+
	'+ Definición de store procedure reaTmp_Crl847A al 04-25-2002 17:54:39
	'+
	'    With lrecreaTmp_Crl847A
	'       .StoredProcedure = "reaTmp_Crl847A"
	'      .Parameters.Add "nServ_order", nServ_Order, rdbParamInput, rdbDouble, 22, 0, 10, rdbParamNullable
	'
	'       If .Run(True) Then
	'          Find = True
	'         nServ_Order = nServ_Order
	'        nCardinal = .FieldToClass("nCardinal")
	'       sDescript = .FieldToClass("sDescript")
	'      sMat_divid = .FieldToClass("sMat_divid")
	'     nDistant = .FieldToClass("nDistant")
	'Else
	'   Find = False
	'        End If
	'   End With
	'
	'reaTmp_Crl847A_Err:
	'   If err Then
	'      Find = False
	' End If
	'On Error GoTo 0
	'    Set lrecreaTmp_Crl847A = Nothing
	
	'End Function
	
	'%InsPostCRL847_1: Ejecuta el post de la transacción
	'%               Tabla de control de prima mínima(CRL847_1)
	Public Function InsPostCRL847_1(ByVal sAction As String, ByVal sCod_cumulo As String, ByVal nVal_max_ces_uf As Double, ByVal sKey As String) As Boolean
		
		On Error GoTo InsPostCRL847_1_Err
		
		With Me
			.sCod_cumulo = sCod_cumulo
			.nVal_max_ces_uf = nVal_max_ces_uf
			.sKey = sKey
		End With
		
		Select Case sAction
			Case "Add"
				InsPostCRL847_1 = Add
			Case "Update"
				InsPostCRL847_1 = Update
			Case "Del"
				InsPostCRL847_1 = Delete
		End Select
		
InsPostCRL847_1_Err: 
		If Err.Number Then
			InsPostCRL847_1 = False
		End If
		On Error GoTo 0
	End Function
	
	'%Class_Initialize: Inicializa las propiedades cuando se instancia la clase
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		sCod_cumulo = String.Empty
		nVal_max_ces_uf = eRemoteDB.Constants.intNull
		sKey = String.Empty
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
End Class






