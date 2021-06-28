Option Strict Off
Option Explicit On
Public Class Activ_Group
	'%-------------------------------------------------------%'
	'% $Workfile:: Activ_Group.cls                          $%'
	'% $Author:: Nvaplat41                                  $%'
	'% $Date:: 9/10/03 19.01                                $%'
	'% $Revision:: 26                                       $%'
	'%-------------------------------------------------------%'
	
	'+ Definición de la tabla ACTIV_GROUP tomada el 02/02/2002 12:19
	'+ Column_Name                                      Type     Length  Prec  Scale Nullable
	' ------------------------------------------------- -------- ------- ----- ------ --------
	Public sCertype As String ' CHAR           1              No
	Public nBranch As Integer ' NUMBER        22     5      0 No
	Public nProduct As Integer ' NUMBER        22     5      0 No
	Public nPolicy As Double ' NUMBER        22    10      0 No
	Public nGroup As Integer ' NUMBER        22     5      0 No
	Public nSpeciality As Integer ' NUMBER        22    10      0 No
	Public dEffecdate As Date ' DATE           7              No
	Public nPercent As Single ' NUMBER        22     5      2 Yes
	Public dNulldate As Date ' DATE           7              Yes
	Public dCompdate As Date ' DATE           7              No
	Public nUsercode As Integer ' NUMBER        22     5      0 No
	
	' - Variable que guarda la transacción que se está ejecutando
	Public nPercent_group As Double
	Public nTransaction As Integer
	
	'%Add: Crea un registro en la tabla
	Public Function Add() As Boolean
		Add = InsUpdActiv_Group(1, nTransaction)
	End Function
	
	'%Update: Actualiza los datos de la tabla
	Public Function Update() As Boolean
		Update = InsUpdActiv_Group(2, nTransaction)
	End Function
	
	'%Delete: Borra los datos de la tabla
	Public Function Delete() As Boolean
		Delete = InsUpdActiv_Group(3, nTransaction)
	End Function
	'%Find: Lee los datos de la tabla
	Public Function Find(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nGroup As Integer, ByVal nSpeciality As Integer, ByVal dEffecdate As Date, Optional ByVal lblnFind As Boolean = False) As Boolean
		Dim lrecreaActiv_Group As eRemoteDB.Execute
		
		On Error GoTo Find_Err
		
		If lblnFind Or Me.sCertype <> sCertype Or Me.nBranch <> nBranch Or Me.nProduct <> nProduct Or Me.nPolicy <> nPolicy Or Me.nGroup <> nGroup Or Me.nSpeciality <> nSpeciality Or Me.dEffecdate <> dEffecdate Then
			lrecreaActiv_Group = New eRemoteDB.Execute
			
			'+Definición de parámetros para stored procedure 'ReaActiv_Group'
			'+Información leída el 02/02/2002
			With lrecreaActiv_Group
				.StoredProcedure = "ReaActiv_Group"
				.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nGroup", nGroup, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nSpeciality", nSpeciality, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				If .Run Then
					Find = True
					Me.sCertype = .FieldToClass("sCertype")
					Me.nBranch = .FieldToClass("nBranch")
					Me.nProduct = .FieldToClass("nProduct")
					Me.nPolicy = .FieldToClass("nPolicy")
					Me.nGroup = .FieldToClass("nGroup")
					Me.nSpeciality = .FieldToClass("nSpeciality")
					Me.dEffecdate = .FieldToClass("dEffecdate")
					nPercent = .FieldToClass("nPercent")
					dNulldate = .FieldToClass("dNulldate")
					.RCloseRec()
				End If
			End With
		End If
Find_Err: 
		If Err.Number Then
			Find = False
		End If
		'UPGRADE_NOTE: Object lrecreaActiv_Group may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaActiv_Group = Nothing
		On Error GoTo 0
	End Function
	
	'%insValVI665: Esta función se encarga de validar la fecha del
	'%Proceso de Actividades del grupo asegurado
	Public Function insValVI665(ByVal sCodispl As String, ByVal sAction As String, ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nGroup As Integer, ByVal nSpeciality As Integer, ByVal dEffecdate As Date, ByVal nPercent As Double) As String
		'- Se define el objeto para el manejo de la clase Product
		Dim lobjErrors As eFunctions.Errors
		Dim lclsActiv_Group As ePolicy.Activ_Group
		Dim lclsGroups As ePolicy.Groups
		
		On Error GoTo insValVI665_Err
		lobjErrors = New eFunctions.Errors
		'+Validación campo Actividad, debe estar lleno
		If nSpeciality = eRemoteDB.Constants.intNull Or nSpeciality = 0 Then
			Call lobjErrors.ErrorMessage(sCodispl, 8002)
		End If
		
		'+Validación del Campo % del grupo
		If nPercent = eRemoteDB.Constants.intNull Or nPercent = 0 Then
			Call lobjErrors.ErrorMessage(sCodispl, 55665,  ,  , "% del grupo")
		End If
		
		'+ Validaciónes
		lclsGroups = New ePolicy.Groups
		With lclsGroups
			If .valGroupExist(sCertype, nBranch, nProduct, nPolicy, dEffecdate) Then
				If nGroup = eRemoteDB.Constants.intNull Or nGroup = 0 Then
					Call lobjErrors.ErrorMessage(sCodispl, 10152)
				End If
			End If
		End With
		
		
		'+ La actividad no puede estar repetida para el mismo grupo asegurado
		lclsActiv_Group = New ePolicy.Activ_Group
		lclsActiv_Group.sCertype = sCertype
		lclsActiv_Group.nBranch = nBranch
		lclsActiv_Group.nProduct = nProduct
		lclsActiv_Group.nPolicy = nPolicy
		lclsActiv_Group.nGroup = nGroup
		lclsActiv_Group.nSpeciality = nSpeciality
		lclsActiv_Group.dEffecdate = dEffecdate
		If sAction = "Add" Then
			If lclsActiv_Group.Activ_GroupExist Then
				Call lobjErrors.ErrorMessage(sCodispl, 55664)
			End If
		End If
		
		'+ el porcentaje de los grupos no puede exceder del 100%
		nPercent_group = 0
		
		If Activ_Group_Rate(sCertype, nBranch, nProduct, nPolicy, nGroup, nSpeciality, dEffecdate) + nPercent > 100 Then
			Call lobjErrors.ErrorMessage(sCodispl, 55776)
		End If
		
		insValVI665 = lobjErrors.Confirm
		
insValVI665_Err: 
		If Err.Number Then
			insValVI665 = "insValVI665: " & Err.Description
		End If
		'UPGRADE_NOTE: Object lobjErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lobjErrors = Nothing
		'UPGRADE_NOTE: Object lclsActiv_Group may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsActiv_Group = Nothing
		'UPGRADE_NOTE: Object lclsGroups may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsGroups = Nothing
		On Error GoTo 0
	End Function
	'%InsPostVI665Upd: Esta función realiza los cambios de BD según especificaciones funcionales
	'%                 de la transacción (VI665)
	Public Function InsPostVI665Upd(ByVal sAction As String, ByVal nTransaction As Integer, ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal nGroup As Integer, ByVal nSpeciality As Integer, ByVal dEffecdate As Date, ByVal nPercent As Double, ByVal nUsercode As Integer) As Boolean
		Dim lintAction As Integer
		Dim lcolActiv_groups As Activ_Groups
		Dim lclsPolicy_Win As Policy_Win
		
		On Error GoTo InsPostVI665Upd_Err
		With Me
			.sCertype = sCertype
			.nBranch = nBranch
			.nProduct = nProduct
			.nPolicy = nPolicy
			.nGroup = nGroup
			.nSpeciality = nSpeciality
			.dEffecdate = dEffecdate
			.nTransaction = nTransaction
			.nPercent = nPercent
			.nUsercode = nUsercode
			
			If sAction = "Del" Then
				lintAction = 3
				
			Else
				If sAction = "Add" Then
					lintAction = 1
				Else
					lintAction = 2
				End If
			End If
			
			Select Case lintAction
				Case 1
					'+ Se crea el registro
					InsPostVI665Upd = .Add
					
					'+ Se modifica el registro
				Case 2
					InsPostVI665Upd = .Update
					
					'+ Se elimina el registro
				Case 3
					InsPostVI665Upd = .Delete
					If InsPostVI665Upd Then
						lcolActiv_groups = New Activ_Groups
						If Not lcolActiv_groups.Find(sCertype, nBranch, nProduct, nPolicy, dEffecdate) Then
							lclsPolicy_Win = New Policy_Win
							Call lclsPolicy_Win.Add_PolicyWin(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate, nUsercode, "VI665", "1")
							'UPGRADE_NOTE: Object lclsPolicy_Win may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
							lclsPolicy_Win = Nothing
						End If
						'UPGRADE_NOTE: Object lcolActiv_groups may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
						lcolActiv_groups = Nothing
					End If
			End Select
		End With
		
InsPostVI665Upd_Err: 
		If Err.Number Then
			InsPostVI665Upd = False
		End If
		On Error GoTo 0
	End Function
	
	'%InsUpdActiv_Group: Realiza la actualización de la tabla
	Private Function InsUpdActiv_Group(ByVal nAction As Integer, ByVal nTransaction As Integer) As Boolean
		Dim lrecInsUpdActiv_Group As eRemoteDB.Execute
		
		On Error GoTo InsUpdActiv_Group_Err
		
		lrecInsUpdActiv_Group = New eRemoteDB.Execute
		
		'+Definición de parámetros para stored procedure 'InsUpdActiv_Group'
		'+Información leída el 02/02/2002
		With lrecInsUpdActiv_Group
			.StoredProcedure = "InsUpdActiv_Group"
			.Parameters.Add("nAction", nAction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nGroup", nGroup, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nSpeciality", nSpeciality, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPercent", nPercent, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			InsUpdActiv_Group = .Run(False)
		End With
		
InsUpdActiv_Group_Err: 
		If Err.Number Then
			InsUpdActiv_Group = False
		End If
		'UPGRADE_NOTE: Object lrecInsUpdActiv_Group may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecInsUpdActiv_Group = Nothing
		On Error GoTo 0
	End Function
	'%Activ_GroupExist. Esta propiedad indica la existencia o no
	'% de la actividad para el mismo grupo asegurado  (Activ_Group)
	Public ReadOnly Property Activ_GroupExist() As Boolean
		Get
			Dim lobjActiv_Group As eRemoteDB.Execute
			lobjActiv_Group = New eRemoteDB.Execute
			With lobjActiv_Group
				.StoredProcedure = "ReaActiv_Group"
				.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nGroup", nGroup, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nSpeciality", nSpeciality, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				Activ_GroupExist = .Run
			End With
			'UPGRADE_NOTE: Object lobjActiv_Group may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
			lobjActiv_Group = Nothing
		End Get
	End Property
	
	'%Activ_Groups_Rate. Esta propiedad indica la existencia o no
	'% de la actividad para el mismo grupo asegurado  (Activ_Group)
	Public Function Activ_Group_Rate(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nGroup As Integer, ByVal nSpeciality As Integer, ByVal dEffecdate As Date) As Double
		Dim lobjActiv_Group As eRemoteDB.Execute
		On Error GoTo Activ_Group_Rate_Err
		lobjActiv_Group = New eRemoteDB.Execute
		
		With lobjActiv_Group
			.StoredProcedure = "InsActiv_Group_Rate"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nGroup", nGroup, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nSpeciality", nSpeciality, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
			.Parameters.Add("nPercent_group", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				Activ_Group_Rate = .Parameters.Item("nPercent_group").Value
			End If
			
		End With
		
Activ_Group_Rate_Err: 
		If Err.Number Then
			Activ_Group_Rate = 0
		End If
		'UPGRADE_NOTE: Object lobjActiv_Group may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lobjActiv_Group = Nothing
		On Error GoTo 0
	End Function
	
	
	'* Class_Initialize: se controla la apertura de la clase
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		nUsercode = eRemoteDB.Constants.intNull
		sCertype = String.Empty
		nBranch = eRemoteDB.Constants.intNull
		nProduct = eRemoteDB.Constants.intNull
		nPolicy = eRemoteDB.Constants.intNull
		nGroup = eRemoteDB.Constants.intNull
		nSpeciality = eRemoteDB.Constants.intNull
		dEffecdate = eRemoteDB.Constants.dtmNull
		nPercent = eRemoteDB.Constants.intNull
		dNulldate = eRemoteDB.Constants.dtmNull
		dCompdate = eRemoteDB.Constants.dtmNull
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
End Class






