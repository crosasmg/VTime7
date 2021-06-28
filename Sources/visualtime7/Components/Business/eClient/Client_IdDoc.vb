Option Strict Off
Option Explicit On
Public Class Client_IdDoc
	
	'%-------------------------------------------------------%'
	'% $Workfile:: Client_IdDoc.cls                         $%'
	'% $Author:: Fmendoza                                   $%'
	'% $Date:: 3/02/06 16:52                                $%'
	'% $Revision:: 2                                        $%'
	'%-------------------------------------------------------%'
	
	
	'+
	'+ Estructura de tabla Client_IdDoc al 02-01-2006 18:31:01
	'+     Property                Type         DBType   Size Scale  Prec  Null
	'+-------------------------------------------------------------------------
	Public sClient As String ' CHAR       14   0     0    N
	Public nIddoc_type As Integer ' NUMBER     22   0     5    N
	Public sIddoc As String ' VARCHAR2   30   0     0    N
	Public sIddoc_digit As String ' CHAR       1    0     0    S
	Public nUsercode As Integer ' NUMBER     22   0     5    N
	
	
	'%InsUpdClient_IdDoc: Se encarga de actualizar la tabla Client_IdDoc
	Private Function InsUpdClient_IdDoc(ByVal nAction As Short) As Boolean
		Dim lrecins_row As eRemoteDB.Execute
		On Error GoTo ins_row_Err
		
		lrecins_row = New eRemoteDB.Execute
		
		'+
		'+ Definición de store procedure ins_row al 02-02-2006 15:29:41
		'+
		With lrecins_row
			.StoredProcedure = "client_IdDoc_DMLpkg.ins_row"
			.Parameters.Add("p_naction", nAction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 38, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("p_sclient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("p_niddoc_type", nIddoc_type, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("p_siddoc", sIddoc, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 30, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("p_siddoc_digit", sIddoc_digit, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("p_nusercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			InsUpdClient_IdDoc = .Run(False)
		End With
		
ins_row_Err: 
		If Err.Number Then
			InsUpdClient_IdDoc = False
		End If
		'UPGRADE_NOTE: Object lrecins_row may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecins_row = Nothing
		On Error GoTo 0
		
	End Function
	
	'%Add: Crea un registro en la tabla
	Public Function Add() As Boolean
		Add = InsUpdClient_IdDoc(1)
	End Function
	
	'%Update: Actualiza un registro en la tabla
	Public Function Update() As Boolean
		Update = InsUpdClient_IdDoc(2)
	End Function
	
	'%Delete: Borra un registro en la tabla
	Public Function Delete() As Boolean
		Delete = InsUpdClient_IdDoc(3)
	End Function
	
	'%Find: Lee los datos de la tabla
	Public Function Find(ByVal sClient As String, ByVal nIddoc_type As Integer, Optional ByVal lblnFind As Boolean = False) As Boolean
		
		Dim lrecrea_cur_pk As eRemoteDB.Execute
		Dim lclsClient_IdDoc As Client_IdDoc
		
		On Error GoTo rea_cur_pk_Err
		
		lrecrea_cur_pk = New eRemoteDB.Execute
		
		'+
		'+ Definición de store procedure rea_cur_pk al 02-02-2006 13:56:23
		'+
		With lrecrea_cur_pk
			.StoredProcedure = "Client_IdDoc_SQLpkg.rea_cur_pk"
			.Parameters.Add("p_sclient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("p_niddoc_type", nIddoc_type, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run(True) Then
				Find = True
				Me.sClient = .FieldToClass("sClient")
				Me.nIddoc_type = .FieldToClass("nIddoc_type")
				Me.sIddoc = .FieldToClass("sIddoc")
				Me.sIddoc_digit = .FieldToClass("sIddoc_digit")
				Me.nUsercode = .FieldToClass("nUsercode")
			Else
				Find = False
			End If
		End With
		
rea_cur_pk_Err: 
		If Err.Number Then
			Find = False
		End If
		'UPGRADE_NOTE: Object lrecrea_cur_pk may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecrea_cur_pk = Nothing
		On Error GoTo 0
	End Function
	
	
	'%Class_Initialize: Inicializa las propiedades cuando se instancia la clase
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		sClient = ""
		nIddoc_type = eRemoteDB.Constants.intNull
		sIddoc = ""
		sIddoc_digit = ""
		nUsercode = eRemoteDB.Constants.intNull
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
End Class






