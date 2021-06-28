Option Strict Off
Option Explicit On
<System.Runtime.InteropServices.ProgId("obs_proposal_NET.obs_proposal")> Public Class obs_proposal
	'%-------------------------------------------------------%'
	'% $Workfile:: Obs_proposal.cls                         $%'
	'% $Author:: Nvaplat41                                  $%'
	'% $Date:: 9/10/03 19.01                                $%'
	'% $Revision:: 8                                        $%'
	'%-------------------------------------------------------%'
	
	'+ Definición de la tabla en el sistema al 08/11/2001
	'+ Los campos llave corresponden a sCertype, nBranch, nProduct, nPolicy, nCertif, nId
	
	'+ Column_Name                      Type                           Length  Prec  Scale  Nullable
	'---------------------------------- ------------------------------ ------ ------ -----  --------
	Public sCertype As String ' CHAR           1                   No
	Public nBranch As Integer ' NUMBER        22     5      0      No
	Public nProduct As Integer ' NUMBER        22     5      0      No
	Public nPolicy As Double ' NUMBER        22    10      0      No
	Public nCertif As Double ' NUMBER        22    10      0      No
	Public nId As Integer ' NUMBER        22     5      0      No
	Public nObservation As Integer ' NUMBER        22     5      0      No
	Public nNotenum As Integer ' NUMBER        22    10      0      Yes
	Public nUsercode As Integer ' NUMBER        22     5      0      No
    Public dCompdate As Date
	
	'% insvalCA748: se validan los datos para las observaciones de una propuesta
	Public Function insvalCA748(ByVal nObservation As Integer) As String
		Dim lclsErrors As eFunctions.Errors
		
		On Error GoTo insvalCA748_err
		
		lclsErrors = New eFunctions.Errors
		
		If nObservation = eRemoteDB.Constants.intNull Then
			Call lclsErrors.ErrorMessage("CA748", 1012,  , eFunctions.Errors.TextAlign.LeftAling, "Observación:")
		End If
		
		insvalCA748 = lclsErrors.Confirm
		
insvalCA748_err: 
		If Err.Number Then
			insvalCA748 = CStr(False)
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
	End Function
	
	'% insPostCA748: actualiza los datos de las observaciones de una propuesta
	Public Function insPostCA748(ByVal sAction As String, ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, ByVal nId As Integer, ByVal nObservation As Integer, ByVal nNotenum As Integer, ByVal nUsercode As Integer) As Boolean
		Dim lblnFind As Boolean
		Dim lclsPolicyWin As Policy_Win
		Dim lcolObs_proposal As Obs_proposals
		
		On Error GoTo insPostCA748_err
		
		lclsPolicyWin = New Policy_Win
		lcolObs_proposal = New Obs_proposals
		
		With Me
			.sCertype = sCertype
			.nBranch = nBranch
			.nProduct = nProduct
			.nPolicy = nPolicy
			.nCertif = nCertif
			.nId = nId
			.nObservation = nObservation
			.nNotenum = nNotenum
			.nUsercode = nUsercode
			
			Select Case sAction
				Case "Add"
					insPostCA748 = .Update(1)
				Case "Update"
					insPostCA748 = .Update(2)
				Case "Del"
					insPostCA748 = .Update(3)
			End Select
			
			'+ Se coloca la ventana con/sin contenido
			lblnFind = lcolObs_proposal.Find(sCertype, nBranch, nProduct, nPolicy, nCertif)
			Call lclsPolicyWin.Add_PolicyWin(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate, nUsercode, "CA748", IIf(lblnFind, "2", "1"))
		End With
		
insPostCA748_err: 
		If Err.Number Then
			insPostCA748 = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lclsPolicyWin may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsPolicyWin = Nothing
		'UPGRADE_NOTE: Object lcolObs_proposal may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lcolObs_proposal = Nothing
	End Function
	
	'* Add: se agrega nuevos registros a la tabla
	Public Function Add() As Boolean
		Add = Update(1)
	End Function
	
	'* Delete: se eliminan los registros de la tabla
	Public Function Delete() As Boolean
		Delete = Update(3)
	End Function
	
	'* Update: se actualizan los campos de la tabla
	Public Function Update(ByVal nAction As Integer) As Boolean
		Dim lrecinsupdObs_proposal As eRemoteDB.Execute
		
		On Error GoTo insupdObs_proposal_Err
		
		lrecinsupdObs_proposal = New eRemoteDB.Execute
		
		'+ Definición de parámetros para stored procedure 'insupdobs_proposal'
		'+ Información leída el 08/11/2001
		
		With lrecinsupdObs_proposal
			.StoredProcedure = "insupdObs_proposal"
			.Parameters.Add("nAction", nAction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nId", nId, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nObservation", nObservation, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nNotenum", nNotenum, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Update = .Run(False)
		End With
		
insupdObs_proposal_Err: 
		If Err.Number Then
			Update = False
		End If
		'UPGRADE_NOTE: Object lrecinsupdObs_proposal may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecinsupdObs_proposal = Nothing
		On Error GoTo 0
	End Function
	
	'* Class_Initialize: se controla el acceso a la clase
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		Call ClearFields()
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
	
	'% ClearFields: se inicializan las propiedades de la clase
	Private Sub ClearFields()
		sCertype = String.Empty
		nBranch = eRemoteDB.Constants.intNull
		nProduct = eRemoteDB.Constants.intNull
		nPolicy = eRemoteDB.Constants.intNull
		nCertif = eRemoteDB.Constants.intNull
		nId = eRemoteDB.Constants.intNull
		nObservation = eRemoteDB.Constants.intNull
		nNotenum = eRemoteDB.Constants.intNull
		nUsercode = eRemoteDB.Constants.intNull
	End Sub
End Class






