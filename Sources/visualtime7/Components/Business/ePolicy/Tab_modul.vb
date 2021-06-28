Option Strict Off
Option Explicit On
Public Class Tab_modul
	'%-------------------------------------------------------%'
	'% $Workfile:: Tab_modul.cls                            $%'
	'% $Author:: Nvaplat41                                  $%'
	'% $Date:: 9/10/03 19.02                                $%'
	'% $Revision:: 10                                       $%'
	'%-------------------------------------------------------%'
	'- Propiedades según la tabla en el sistema al 12/12/2000.
	'- Los campos llave de la tabla corresponden a: nBranch, nProduct, nModulec y dEffecdate.
	
	'   Column_name                   Type      Computed  Length Prec  Scale Nullable   TrimTrailingBlanks   FixedLenNullInSource
	Public nBranch As Integer 'smallint     no       2      5     0     no             (n/a)                 (n/a)
	Public nModulec As Integer 'smallint     no       2      5     0     no             (n/a)                 (n/a)
	Public nProduct As Integer 'smallint     no       2      5     0     no             (n/a)                 (n/a)
	Public dEffecdate As Date 'datetime     no       8                  no             (n/a)                 (n/a)
	Public sChanallo As String 'char         no       1                  yes            no                    yes
	Public sDefaulti As String 'char         no       1                  yes            no                    yes
	Public sDescript As String 'char         no      30                  yes            no                    yes
	Public dNulldate As Date 'datetime     no       8                  yes            (n/a)                 (n/a)
	Public sRequire As String 'char         no       1                  yes            no                    yes
	Public sShort_des As String 'char         no      12                  yes            no                    yes
	Public nUsercode As Integer 'smallint     no       2      5     0     yes            (n/a)                 (n/a)
	
	Private Structure udtTab_modul
		Dim sDescript As String
		Dim nModulec As Integer
		Dim sDefaulti As String
		Dim sRequire As String
		Dim nExist As Integer
	End Structure
	
	Private Structure udtModulesProduct
		Dim nBranch As Integer
		Dim nModulec As Integer
		Dim nProduct As Integer
		Dim dEffecdate As Date
		Dim sChanallo As Date
		Dim sDefaulti As String
		Dim sDescript As String
		Dim dNulldate As Date
		Dim sRequire As String
		Dim sShort_des As String
	End Structure
	
	'- Arreglo para la carga de Módulos
	Private marrTab_modul() As udtTab_modul
	
	'- Indica si el arreglo de módulos se cargó o no
	Private mblnCharge As Boolean
	
	'- Variables auxiliares
	Public nExist As Integer
	Public nPolicy As Double
	Public nCertif As Double
	Public sCertype As String
	Public nGroup_insu As Integer
	Public sChangei As String
	Public nCurrency As Integer
	
	'% Count: Devuelve el número de módulos que se encuentran en el arreglo
	Public ReadOnly Property Count() As Integer
		Get
			Count = UBound(marrTab_modul)
		End Get
	End Property
	
	'% LoadModuleData: Devuelve información de los módulos de una póliza
	Public Function LoadModuleData(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal dEffecdate As Date, ByVal sTyp_modul As String, ByVal nGroup As Integer) As Boolean
		Dim lrecreaTab_modul2 As eRemoteDB.Execute
		Dim lintIndex As Integer
		
		On Error GoTo LoadModuleData_Err
		
		lrecreaTab_modul2 = New eRemoteDB.Execute
		
		With lrecreaTab_modul2
			.StoredProcedure = "reaTab_modul2"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sTyp_modul", sTyp_modul, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nGroup", nGroup, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run Then
				lintIndex = -1
				LoadModuleData = True
				mblnCharge = True
				ReDim marrTab_modul(100)
				
				Do While Not .EOF
					lintIndex = lintIndex + 1
					marrTab_modul(lintIndex).sDescript = .FieldToClass("sDescript")
					marrTab_modul(lintIndex).nModulec = .FieldToClass("nModulec")
					marrTab_modul(lintIndex).sDefaulti = .FieldToClass("sDefaulti")
					marrTab_modul(lintIndex).sRequire = .FieldToClass("sRequire")
					marrTab_modul(lintIndex).nExist = .FieldToClass("Exist", 0)
					.RNext()
				Loop 
				
				.RCloseRec()
				ReDim Preserve marrTab_modul(lintIndex)
			Else
				LoadModuleData = False
				mblnCharge = False
			End If
		End With
		
LoadModuleData_Err: 
		If Err.Number Then
			LoadModuleData = False
			mblnCharge = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecreaTab_modul2 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaTab_modul2 = Nothing
	End Function
	
	'% Item: Carga en las variables de la clase la información de un módulo
	Public Function Item(ByVal lintIndex As Integer) As Boolean
		If mblnCharge Then
			If lintIndex <= UBound(marrTab_modul) Then
				With marrTab_modul(lintIndex)
					sDescript = .sDescript
					nModulec = .nModulec
					sDefaulti = .sDefaulti
					sRequire = .sRequire
					nExist = .nExist
				End With
				Item = True
			Else
				Item = False
			End If
		End If
	End Function
	
	'% Find_Modules: Permite cargar los modulos de una Poliza
	Public Function Find_Modules(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date) As Collection
		Dim lrecReaModules_a As eRemoteDB.Execute
		Dim lclsTab_Modul As Tab_modul
		
		On Error GoTo Find_Modules_Err
		
		lrecReaModules_a = New eRemoteDB.Execute
		Find_Modules = New Collection
		
		With lrecReaModules_a
			.StoredProcedure = "ReaModules_a"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				Do While Not .EOF
					lclsTab_Modul = New Tab_modul
					lclsTab_Modul.sCertype = .FieldToClass("sCertype")
					lclsTab_Modul.nBranch = .FieldToClass("nBranch")
					lclsTab_Modul.nProduct = .FieldToClass("nProduct")
					lclsTab_Modul.nPolicy = .FieldToClass("nPolicy")
					lclsTab_Modul.nCertif = .FieldToClass("nCertif")
					lclsTab_Modul.nGroup_insu = .FieldToClass("nGroup_insu")
					lclsTab_Modul.nModulec = .FieldToClass("nModulec")
					lclsTab_Modul.dEffecdate = .FieldToClass("dEffecdate")
					lclsTab_Modul.dNulldate = .FieldToClass("dNulldate")
					lclsTab_Modul.sChangei = .FieldToClass("sChangei")
					lclsTab_Modul.nCurrency = .FieldToClass("nCurrency")
					Find_Modules.Add(lclsTab_Modul)
					'UPGRADE_NOTE: Object lclsTab_Modul may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
					lclsTab_Modul = Nothing
					.RNext()
				Loop 
				.RCloseRec()
			End If
		End With
		
Find_Modules_Err: 
		If Err.Number Then
			'UPGRADE_NOTE: Object Find_Modules may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
			Find_Modules = Nothing
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lclsTab_Modul may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsTab_Modul = Nothing
		'UPGRADE_NOTE: Object lrecReaModules_a may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecReaModules_a = Nothing
	End Function
End Class






