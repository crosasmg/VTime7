Option Strict Off
Option Explicit On
Public Class Tab_comlif
	'%-------------------------------------------------------%'
	'% $Workfile:: Tab_comlif.cls                           $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 1:41p                                $%'
	'% $Revision:: 8                                        $%'
	'%-------------------------------------------------------%'
	
	'**+ Properties according to the table in the system on April 24,2001
	'+ Propiedades según la tabla en el sistema el 24/04/2001
	'**+ The key field correspond to nComTabli.
	'+ El campo llave corresponde a nComtabli.
	
	'+ Column_name         Type                 Length      Prec  Scale Nullable TrimTrailingBlanks FixedLenNullInSource
	'+ ------------------- -------------------- ----------- ----- ----- -------- ------------------ --------------------
	Public nComtabli As Integer 'smallint 2           5     0     no       (n/a)              (n/a)
	Public nUsercode As Integer 'smallint 2           5     0     yes      (n/a)              (n/a)
	Public sDescript As String 'char     30                      yes      no                 yes
	Public sShort_des As String 'char     12                      yes      no                 yes
	Public sStatregt As String 'char     1                       yes      no                 yes
	'%Update: Esta función se encarga de agregar la información en tratamiento de la
	'%tabla principal para la transacción.
	Public Function insPostMAG002_K(ByVal nAction As Integer, ByVal nComtabli As Integer, ByVal nUsercode As Integer, ByVal sDescript As String, ByVal sShort_des As String, ByVal sStatregt As String) As Boolean
		
		On Error GoTo insPostMAG002_K_err
		
		Me.nComtabli = nComtabli
		Me.nUsercode = nUsercode
		Me.sDescript = sDescript
		Me.sShort_des = sShort_des
		
		
		If nAction = 301 Then
			Me.sStatregt = "2"
			insPostMAG002_K = Add()
		Else
			Me.sStatregt = sStatregt
			insPostMAG002_K = Update()
		End If
		
		
insPostMAG002_K_err: 
		If Err.Number Then
			insPostMAG002_K = False
		End If
		
	End Function
	
	
	
	'%Add: Esta función se encarga de agregar la información en tratamiento de la
	'%tabla principal para la transacción.
	Public Function Add() As Boolean
		Dim lrecTab_comlif As eRemoteDB.Execute
		
		On Error GoTo Add_Err
		
		lrecTab_comlif = New eRemoteDB.Execute
		
		'Definición de parámetros para stored procedure 'insudb.insDet_comlif'
		'Información leída el 24/04/2001 02:44:47 p.m.
		
		With lrecTab_comlif
			.StoredProcedure = "creTab_comlif"
			.Parameters.Add("nComtabli", nComtabli, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sDescript", sDescript, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 30, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sShort_des", sShort_des, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sStatregt", sStatregt, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			Add = .Run(False)
			
		End With
		
Add_Err: 
		If Err.Number Then
			Add = False
		End If
		'UPGRADE_NOTE: Object lrecTab_comlif may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecTab_comlif = Nothing
		On Error GoTo 0
	End Function
	
	'%Update: Esta función se encarga de agregar la información en tratamiento de la
	'%tabla principal para la transacción.
	Public Function Update() As Boolean
		Dim lrecTab_comlif As eRemoteDB.Execute
		
		On Error GoTo Update_Err
		
		lrecTab_comlif = New eRemoteDB.Execute
		
		'Definición de parámetros para stored procedure 'insudb.insDet_comlif'
		'Información leída el 24/04/2001 02:44:47 p.m.
		
		With lrecTab_comlif
			.StoredProcedure = "UPDTAB_COMLIF_STAT"
			.Parameters.Add("nComtabli", nComtabli, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sStatregt", sStatregt, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			Update = .Run(False)
			
		End With
		
Update_Err: 
		If Err.Number Then
			Update = False
		End If
		'UPGRADE_NOTE: Object lrecTab_comlif may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecTab_comlif = Nothing
		On Error GoTo 0
	End Function
	
	
	'%Find: Lee los datos de la tabla
	Public Function Find() As Boolean
		Dim lrecreaTab_comlif As eRemoteDB.Execute
		
		On Error GoTo lrecreaTab_comlif_Err
		
		lrecreaTab_comlif = New eRemoteDB.Execute
		
		'+Definición de parámetros para stored procedure 'reaAgreement_al_o'
		'+Información leída el 19/10/01
		With lrecreaTab_comlif
			.StoredProcedure = "REATAB_COMLIF_A"
			.Parameters.Add("nContabli", Me.nComtabli, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				Find = True
				sDescript = .FieldToClass("sDescript")
				sStatregt = .FieldToClass("sStatregt")
			End If
		End With
		
lrecreaTab_comlif_Err: 
		If Err.Number Then
			Find = False
		End If
		'UPGRADE_NOTE: Object lrecreaTab_comlif may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaTab_comlif = Nothing
		On Error GoTo 0
		
	End Function
	
	
	'*** Class_Initialize: controls the opening of the class
	'* Class_Initialize: se controla la apertura de la clase
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		'nUsercode = GetSetting("TIME", "GLOBALS", "USERCODE", 0)
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
End Class






