Option Strict Off
Option Explicit On
Public Class Durinsu_prod
	'%-------------------------------------------------------%'
	'% $Workfile:: Durinsu_prod.cls                         $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 12:35p                               $%'
	'% $Revision:: 14                                       $%'
	'%-------------------------------------------------------%'
	
	'*+Properties according to the table 'Durinsu_prod' in the system 10/07/2002
	'+ Propiedades según la tabla 'Durinsu_prod' en el sistema 10/07/2002
	
	'Column_name                 Type                  Nulldeable
	'---------------------   ------------------------ ---------------
	Public nBranch As Integer 'Number(5)       No
	Public nProduct As Integer 'Number(5)       No
	Public dEffecdate As Date 'Date            No
	Public nIdurafix As Integer 'Number          Yes
	Public nusercode As Integer 'Number(5)       No
	Public nTypdurins As Integer 'Number
	Public nMinDurIns As Short 'Number(3)       Yes
	
	
	'% InsUpdDurinsu_prod: se actualizan los datos asociados a la duración del seguro
	Private Function InsUpdDurinsu_prod(ByVal nAction As Integer) As Boolean
		Dim lrecinsupdDurinsu_prod As eRemoteDB.Execute
		
		On Error GoTo insupdDurinsu_prod_Err
		
		lrecinsupdDurinsu_prod = New eRemoteDB.Execute
		
		With lrecinsupdDurinsu_prod
			.StoredProcedure = "InsUpdDurinsu_prod"
			.Parameters.Add("nAction", nAction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nIdurafix", nIdurafix, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nusercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTypdurins", nTypdurins, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nMinDurIns", nMinDurIns, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			InsUpdDurinsu_prod = .Run(False)
		End With
		
insupdDurinsu_prod_Err: 
		If Err.Number Then
			InsUpdDurinsu_prod = False
		End If
		'UPGRADE_NOTE: Object lrecinsupdDurinsu_prod may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecinsupdDurinsu_prod = Nothing
		On Error GoTo 0
	End Function
	
	'%Add: Crea un registro en la tabla
	Public Function Add() As Boolean
		Add = InsUpdDurinsu_prod(1)
	End Function
	
	'%Update: Actualiza un registro en la tabla
	Public Function Update() As Boolean
		Update = InsUpdDurinsu_prod(2)
	End Function
	
	'%Delete: Borra un registro en la tabla
	Public Function Delete() As Boolean
		Delete = InsUpdDurinsu_prod(3)
	End Function
	
	'%DeleteAll: Borra todos los registros de la tabla
	Public Function DeleteAll(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal dEffecdate As Date) As Boolean
		Me.nBranch = nBranch
		Me.nProduct = nProduct
		Me.dEffecdate = dEffecdate
		
		DeleteAll = InsUpdDurinsu_prod(4)
	End Function
	
	'%Find: Lee los datos de la tabla
	Public Function Find(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nIdurafix As Integer, ByVal dEffecdate As Date, ByVal nTypdurins As Integer, Optional ByVal lblnFind As Boolean = False) As Boolean
		Dim lrecReaDurinsu_prod As eRemoteDB.Execute
		
		On Error GoTo Find_Err
		
		If Me.nBranch <> nBranch Or Me.nProduct <> nProduct Or Me.nIdurafix <> nIdurafix Or Me.dEffecdate <> dEffecdate Or Me.nTypdurins <> nTypdurins Or lblnFind Then
			
			lrecReaDurinsu_prod = New eRemoteDB.Execute
			
			'+Definición de parámetros para stored procedure 'reaDurinsu_prod'
			With lrecReaDurinsu_prod
				.StoredProcedure = "reaDurinsu_prod"
				.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nIdurafix", nIdurafix, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nTypdurins", nTypdurins, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				If .Run Then
					Me.nBranch = nBranch
					Me.nProduct = nProduct
					Me.nIdurafix = nIdurafix
					Me.dEffecdate = dEffecdate
					Me.nTypdurins = nTypdurins
					Me.nMinDurIns = .FieldToClass("nMinDurIns")
					Find = True
					.RCloseRec()
				End If
			End With
		End If
		
Find_Err: 
		If Err.Number Then
			Find = False
		End If
		'UPGRADE_NOTE: Object lrecReaDurinsu_prod may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecReaDurinsu_prod = Nothing
		On Error GoTo 0
	End Function
	
	'% InsValDP043UPD: Validaciones de la transacción
	Public Function InsValDP043UPD(ByVal sCodispl As String, ByVal sAction As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nIdurafix As Integer, ByVal sIdurvari As String, ByVal dEffecdate As Date, ByVal nTypdurins As Integer) As String
		Dim lclsErrors As eFunctions.Errors
		
		On Error GoTo InsValDP043UPD_Err
		
		lclsErrors = New eFunctions.Errors
		
		With lclsErrors
			'+ Validación del campo <Seguro-tiempo-fija-cantidad>
			If sIdurvari = "2" Then
				'+ El campo Seguro - Tiempo - Tipo de duración del seguro, debe estar lleno
				If nTypdurins = eRemoteDB.Constants.intNull Then
					Call lclsErrors.ErrorMessage("DP043", 1012,  , eFunctions.Errors.TextAlign.LeftAling, "Tipo de duración del seguro:")
				End If
				
				If nIdurafix = eRemoteDB.Constants.intNull Then
					.ErrorMessage("DP043", 11180)
					
					'+ Se valida que no exista el registro en la tabla
				ElseIf sAction = "Add" Then 
					If Find(nBranch, nProduct, nIdurafix, dEffecdate, nTypdurins, True) Then
						.ErrorMessage(sCodispl, 60481)
					End If
				End If
				
				'+ Se valida que la edad indicada no sea mayor a 130
				If nIdurafix > 130 And nTypdurins = 2 Then
					.ErrorMessage("DP043", 11413)
				End If
			End If
			InsValDP043UPD = .Confirm
		End With
		
InsValDP043UPD_Err: 
		If Err.Number Then
			InsValDP043UPD = "InsValDP043UPD: " & Err.Description
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
	End Function
	
	'% InsPostDP043UPD: Se actualizan los datos asociados a la duración del seguro
	Public Function InsPostDP043UPD(ByVal sAction As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nIdurafix As Integer, ByVal nTypdurins As Integer, ByVal nMinDurIns As Short, ByVal dEffecdate As Date, ByVal nusercode As Integer) As Boolean
		On Error GoTo InsPostDP043UPD_Err
		
		With Me
			.nBranch = nBranch
			.nProduct = nProduct
			.nIdurafix = nIdurafix
			.dEffecdate = dEffecdate
			.nusercode = nusercode
			.nTypdurins = nTypdurins
			.nMinDurIns = nMinDurIns
		End With
		
		Select Case sAction
			Case "Add"
				InsPostDP043UPD = Add
			Case "Update"
				InsPostDP043UPD = Update
			Case "Del"
				InsPostDP043UPD = Delete
		End Select
		
InsPostDP043UPD_Err: 
		If Err.Number Then
			InsPostDP043UPD = False
		End If
		On Error GoTo 0
	End Function
	
	'%Class_Initialize: Inicializa las propiedades cuando se instancia la clase
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		nBranch = eRemoteDB.Constants.intNull
		nProduct = eRemoteDB.Constants.intNull
		nIdurafix = eRemoteDB.Constants.intNull
		dEffecdate = eRemoteDB.Constants.dtmNull
		nusercode = eRemoteDB.Constants.intNull
		nTypdurins = eRemoteDB.Constants.intNull
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
End Class






