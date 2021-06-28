Option Strict Off
Option Explicit On
Public Class Auto_dbs
	Implements System.Collections.IEnumerable
	'-Se define La variable lstrTab_name_b utilizada para almacenar el nombre de la tabla de Datos
	'-particulares de Auto.
	
	Private lstrTab_name_b As String
	
	'variable local para contener colección
	Private mCol As Collection

    '%Add: Añade una nueva instancia de la clase a la colección
    Public Sub Add(ByRef objClass As Auto_db)
        mCol.Add(objClass)
    End Sub

    '%Item: Devuelve un elemento de la colección (segun índice)
    Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As Auto_db
		Get
			Item = mCol.Item(vntIndexKey)
		End Get
	End Property
	
	'%Count: Devuelve el número de elementos que posee la colección
	Public ReadOnly Property Count() As Integer
		Get
			Count = mCol.Count()
		End Get
	End Property
	
	'%NewEnum: Permite enumerar la colección para utilizarla en un ciclo For Each... Next
	'UPGRADE_NOTE: NewEnum property was commented out. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="B3FC1610-34F3-43F5-86B7-16C984F0E88E"'
	'Public ReadOnly Property NewEnum() As stdole.IUnknown
		'Get
			'NewEnum = mCol._NewEnum
		'End Get
	'End Property
	
	Public Function GetEnumerator() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
		'UPGRADE_TODO: Uncomment and change the following line to return the collection enumerator. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="95F9AAD0-1319-4921-95F0-B9D3C4FF7F1C"'
		GetEnumerator = mCol.GetEnumerator
	End Function
	
	'%Remove: Elimina un elemento de la colección
	Public Sub Remove(ByRef vntIndexKey As Object)
		mCol.Remove(vntIndexKey)
	End Sub
	
	'%Class_Initialize: Controla la creación de una instancia de la colección
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		mCol = New Collection
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
	
	'%Class_Terminate: Controla la destrucción de una instancia de la colección
	'UPGRADE_NOTE: Class_Terminate was upgraded to Class_Terminate_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Terminate_Renamed()
		'UPGRADE_NOTE: Object mCol may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		mCol = Nothing
	End Sub
	Protected Overrides Sub Finalize()
		Class_Terminate_Renamed()
		MyBase.Finalize()
	End Sub
	
	'%Find_AUC001_K: Esta función se encarga de de buscar la colección de datos de acuerdo
	'%a las condiciones que el usuario
	Public Function Find_AUC001_K(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPayfreq As Integer, ByVal nCapital As Double, ByVal nPremium As Double, ByVal dEffecdate As Date, ByVal dNulldate As Date, ByVal sPolitype As String, ByVal sLicense_ty As String, ByVal sRegist As String, ByVal sMotor As String, ByVal sChassis As String, ByVal sColor As String, ByVal nVehBrand As Integer, ByVal sVehmodel As String, ByVal nVehType As Integer, ByVal nAutoZone As Integer) As Boolean
		Dim llclsAuto_db As Object
		Dim lrecrea_auc001 As eRemoteDB.Execute
		Dim lclsAuto_db As Auto_db
		
		On Error GoTo rea_auc001_Err
		
		lrecrea_auc001 = New eRemoteDB.Execute
		
		'+
		'+ Definición de store procedure rea_auc001 al 10-22-2002 11:10:47
		'+
		With lrecrea_auc001
			.StoredProcedure = "rea_auc001"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPayfreq", nPayfreq, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCapital", nCapital, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPremium", nPremium, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dNulldate", dNulldate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sPolitype", sPolitype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sLicense_ty", sLicense_ty, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sRegist", sRegist, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 10, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sMotor", sMotor, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 40, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sChassis", sChassis, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 40, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sColor", sColor, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 15, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nVehbrand", nVehBrand, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sVehmodel", sVehmodel, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nVehtype", nVehType, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAutozone", nAutoZone, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run Then
				Find_AUC001_K = True
				Do While Not .EOF
					lclsAuto_db = New Auto_db
					lclsAuto_db.nProduct = .FieldToClass("nProduct")
					lclsAuto_db.dEffecdate = .FieldToClass("dEffecdate")
					lclsAuto_db.dExpirdat = .FieldToClass("dExpirdat")
					lclsAuto_db.nBranch = .FieldToClass("nBranch")
					lclsAuto_db.nCapital = .FieldToClass("nCapital")
					lclsAuto_db.nCertif = .FieldToClass("nCertif")
					lclsAuto_db.nPayfreq = .FieldToClass("nPayfreq")
					lclsAuto_db.nPolicy = .FieldToClass("nPolicy")
					lclsAuto_db.nPremium = .FieldToClass("nPremium")
					lclsAuto_db.nVehBrand = .FieldToClass("nVehBrand")
					lclsAuto_db.nVehType = .FieldToClass("nVehType")
					lclsAuto_db.nAutoZone = .FieldToClass("nAutoZone")
					lclsAuto_db.sBranchName = .FieldToClass("sBranchName")
					lclsAuto_db.sChassis = .FieldToClass("sChassis")
					lclsAuto_db.sCliename = .FieldToClass("sCliename")
					lclsAuto_db.sColor = .FieldToClass("sColor")
					lclsAuto_db.sDescript = .FieldToClass("sDescript")
					lclsAuto_db.sLicense_ty = .FieldToClass("sLicense_ty")
					lclsAuto_db.sMotor = .FieldToClass("sMotor")
					lclsAuto_db.sPolitype = .FieldToClass("sPolitype")
					lclsAuto_db.sRegist = .FieldToClass("sRegist")
					lclsAuto_db.sVehmodel = .FieldToClass("sVehmodel")
					
					Call Add(lclsAuto_db)
					'UPGRADE_NOTE: Object llclsAuto_db may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
					llclsAuto_db = Nothing
					.RNext()
				Loop 
				.RCloseRec()
			Else
				Find_AUC001_K = False
			End If
		End With
		
rea_auc001_Err: 
		If Err.Number Then
			Find_AUC001_K = False
		End If
		'UPGRADE_NOTE: Object lrecrea_auc001 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecrea_auc001 = Nothing
		On Error GoTo 0
	End Function
End Class






