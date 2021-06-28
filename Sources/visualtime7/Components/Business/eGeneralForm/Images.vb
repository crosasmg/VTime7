Option Strict Off
Option Explicit On
Public Class Images
	
	'- Propiedades según la tabla en el sistema el 07/06/2000.
	'- El campo llave corresponde a nImagenum y nConsec
	
	'Column_name                      Type                 Computed Length      Prec  Scale Nullable TrimTrailingBlanks FixedLenNullInSource
	'-------------------------------- -------------------- -------- ----------- ----- ----- -------- ------------------ --------------------
	Public nImagenum As Integer 'int      no       4           10    0     no       (n/a)              (n/a)
	Public nConsec As Integer 'smallint no       2           5     0     no       (n/a)              (n/a)
	Public sDescript As String 'char     no       60                      yes      yes                yes
	Public iImage As Object 'image    no       16                      yes      (n/a)              (n/a)
	Public dCompdate As Date 'datetime no       8                       yes      (n/a)              (n/a)
	Public dNulldate As Date 'datetime no       8                       yes      (n/a)              (n/a)
	Public nRectype As Integer 'smallint no       2           5     0     no       (n/a)              (n/a)
	Public nUsercode As Integer 'smallint no       2           5     0     no       (n/a)              (n/a)
	
	'- Propiedades auxiliares
	
	'- Dirección física de la imagen
	Public sSource As String
	
	'- Numero original de la imagen de la instancia en tratamiento
	Public nOldImagenum As Integer
	
	'- Tipo de Registro al que pertenecen las imágenes.
	Public Enum eTypeImages
		'-Imagen de siniestro
		clngClaimImage = 1
		'-Imagen de cliente
		clngClientImage = 2
		'-Imagen de orden de servicio
		clngProf_ordImage = 3
		'-Imagen de Propuestas de Siniestros
		clngFireBudgetImage = 4
	End Enum
	
	Private mlngTypeImage As eTypeImages
	
	Public sClient As String
	Public sCliename As String
	
	'-Se define la variable para indicar el estado de cada instancia en la colección
	Public nStatusInstance As Integer
	
	
	Public sSessionID As String
	
	'%Find_Image: realiza la lectura de los datos directamente en la tabla
	Public Function Find_Image(ByVal Imagenum As Integer, ByVal Consec As Integer) As Boolean
		Dim lrecreaImage As eRemoteDB.Execute
		
		lrecreaImage = New eRemoteDB.Execute
		
		On Error GoTo Find_Image_Err
		
		'+ Definición de parámetros para stored procedure 'insudb.reaImage'
		'+ Información leída el 30/10/2000 11:45:28 a.m.
		
		With lrecreaImage
			.StoredProcedure = "reaImage"
			.Parameters.Add("nImagenum", Imagenum, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nConsec", Consec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				Find_Image = True
				nImagenum = Imagenum
				nConsec = Consec
				sDescript = .FieldToClass("sDescript", numNull)
				dCompdate = .FieldToClass("dCompdate", dtmNull)
				dNulldate = .FieldToClass("dNulldate", dtmNull)
				nRectype = .FieldToClass("nRectype", numNull)
				nUsercode = .FieldToClass("nUsercode", numNull)
				iImage = .FieldToClass("iImage", strNull)
				.RCloseRec()
			Else
				Find_Image = False
			End If
		End With
		'UPGRADE_NOTE: Object lrecreaImage may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaImage = Nothing
		
Find_Image_Err: 
		If Err.Number Then
			Find_Image = False
		End If
		On Error GoTo 0
	End Function
	
	'% Find: Este metodo retorna VERDADERO o FALSO dependiendo de la existencia o no de registros en la
	'% tabla "IMAGES"
	Public Function Find(ByVal Imagenum As Integer, ByVal Consec As Integer, ByRef objControl As Object, Optional ByVal lblnFind As Boolean = False) As Boolean
		Dim lexeTime As eRemoteDB.Images
		
		lexeTime = New eRemoteDB.Images
		
		On Error GoTo Find_err
		
		'+ Definición de parámetros para stored procedure 'insudb.reaImages_o'
		'+ Información leída el 24/01/2000 02:08:08 PM
		
		With lexeTime
			If .FindImage(Imagenum, Consec) Then
				Find = True
				nImagenum = Imagenum
				nConsec = Consec
				sDescript = .sDescript
				dCompdate = .dCompdate
				dNulldate = .dNulldate
				nRectype = .nRectype
				nUsercode = .nUsercode
				
				iImage = .iImage
				If Not objControl Is Nothing Then
					objControl.Picture = iImage
				End If
			Else
				Find = False
			End If
		End With
		
		'UPGRADE_NOTE: Object lexeTime may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lexeTime = Nothing
		
Find_err: 
		If Err.Number Then
			Find = False
		End If
		On Error GoTo 0
	End Function
	
	'% Add_WithOutImage: Permite añadir un registro (sin la imagen)
	Public Function Add_WithOutImage() As Boolean
		Dim lreccreImage As eRemoteDB.Execute
		
		lreccreImage = New eRemoteDB.Execute
		
		On Error GoTo Add_WithOutImage_Err
		
		'+ Definición de parámetros para stored procedure 'insudb.creImage'
		'+ Información leída el 31/10/2000 05:22:40 p.m.
		With lreccreImage
			.StoredProcedure = "creImage"
			.Parameters.Add("nImageNum", nImagenum, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nConsec", nConsec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sDescript", sDescript, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 60, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dNulldate", dNulldate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nRectype", nRectype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Add_WithOutImage = .Run(False)
		End With
		'UPGRADE_NOTE: Object lreccreImage may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lreccreImage = Nothing
		
Add_WithOutImage_Err: 
		If Err.Number Then
			Add_WithOutImage = False
		End If
		On Error GoTo 0
	End Function
	
	'% Add: Este método se encarga de agregar nuevos registros a la tabla "IMAGE". Devolviendo verdadero o
	'% falso dependiendo de si el Stored procedure se ejecutó correctamente.
	Public Function Add() As Boolean
		Dim lexeTime As eRemoteDB.Images
		Dim lclsNumerator As Object
		Dim lclsClient As Object
		
		lexeTime = New eRemoteDB.Images
		lclsNumerator = eRemoteDB.NetHelper.CreateClassInstance("eGeneral.GeneralFunction")
		lclsClient = eRemoteDB.NetHelper.CreateClassInstance("eClient.Client")
		
		On Error GoTo Add_err
		
		If nImagenum = 0 Then
			nImagenum = lclsNumerator.Find_Numerator(23, 0)
		End If
		
		With lexeTime
			If .AddImage(nImagenum, nConsec, nRectype, nUsercode, sDescript, sSource, dNulldate) Then
				Add = True
				With lclsClient
					.sClient = sClient
					.nUsercode = nUsercode
					Call .UpdateImageNum(nImagenum)
				End With
			End If
		End With
		'UPGRADE_NOTE: Object lexeTime may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lexeTime = Nothing
		'UPGRADE_NOTE: Object lclsNumerator may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsNumerator = Nothing
		'UPGRADE_NOTE: Object lclsClient may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsClient = Nothing
		
Add_err: 
		If Err.Number Then
			Add = False
		End If
		On Error GoTo 0
	End Function
	
	'% Update_WithOutImage: Permite modificar un registro (sin la imagen)
	Public Function Update_WithOutImage() As Boolean
		Dim lrecupdImage As eRemoteDB.Execute
		
		lrecupdImage = New eRemoteDB.Execute
		
		On Error GoTo Update_WithOutImage_Err
		
		'+ Definición de parámetros para stored procedure 'insudb.updImage'
		'+ Información leída el 31/10/2000 05:41:07 p.m.
		
		With lrecupdImage
			.StoredProcedure = "updImage"
			.Parameters.Add("nImageNum", nImagenum, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nConsec", nConsec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sDescript", sDescript, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 60, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dNulldate", dNulldate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Update_WithOutImage = .Run(False)
		End With
		'UPGRADE_NOTE: Object lrecupdImage may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecupdImage = Nothing
		
Update_WithOutImage_Err: 
		If Err.Number Then
			Update_WithOutImage = False
		End If
		On Error GoTo 0
	End Function
	
	'% Update: Este método se encarga de actualizar registros en la tabla "IMAGE". Devolviendo verdadero o
	'% falso dependiendo de si el Stored procedure se ejecutó correctamente.
	Public Function Update() As Boolean
		Dim lrecupdImage As eRemoteDB.Images
		
		lrecupdImage = New eRemoteDB.Images
		
		On Error GoTo Update_err
		
		'+ Definición de parámetros para stored procedure
		'+ Información leída el 07/06/2000 02:06:08 PM
		
		With lrecupdImage
			Update = .UpdateImage(nImagenum, nConsec, nRectype, nUsercode, sDescript, sSource, dNulldate)
		End With
		'UPGRADE_NOTE: Object lrecupdImage may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecupdImage = Nothing
		
Update_err: 
		If Err.Number Then
			Update = False
		End If
		On Error GoTo 0
	End Function
	
	'% Delete: Este método se encarga de eliminar registros en la tabla "Images". Devolviendo verdadero o
	'% falso dependiendo de si el Stored procedure se ejecutó correctamente.
	Public Function Delete() As Boolean
		Dim lrecdelImages As eRemoteDB.Execute
		
		lrecdelImages = New eRemoteDB.Execute
		
		On Error GoTo Delete_err
		
		'+ Definición de parámetros para stored procedure 'delImages'
		'+ Información leída el 09/06/2000 01:00:38 PM
		With lrecdelImages
			.StoredProcedure = "delImages"
			.Parameters.Add("nImagenum", nImagenum, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nConsec", nConsec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Delete = .Run(False)
		End With
		'UPGRADE_NOTE: Object lrecdelImages may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecdelImages = Nothing
		
Delete_err: 
		If Err.Number Then
			Delete = False
		End If
		On Error GoTo 0
	End Function
	
	'% getImageKey: se toman los datos de acuerdo al codispl de la forma
	Public Function getImageKey(ByVal sCodispl As String, ByVal sKey As Object) As Boolean
		Dim lobjImage As Object
		
		'+ Asignacion del tipo de registro
		Select Case sCodispl
			
			'+ Imagen del siniestro
			Case "SCA10-1"
				lobjImage = eRemoteDB.NetHelper.CreateClassInstance("eClaim.Claim")
				If lobjImage.Find(sKey) Then
					nImagenum = lobjImage.nImagenum
				End If
				nRectype = eTypeImages.clngClaimImage
				
				'+ Imagen del cliente
			Case "SCA10-2"
				lobjImage = eRemoteDB.NetHelper.CreateClassInstance("eClient.Client")
				If lobjImage.Find(sKey) Then
					nImagenum = lobjImage.nImagenum
				End If
				nRectype = eTypeImages.clngClientImage

				'+ Imagen del cliente en siniestros
			Case "SCA10-20"
				lobjImage = eRemoteDB.NetHelper.CreateClassInstance("eClient.Client")
				If lobjImage.Find(sKey) Then
					nImagenum = lobjImage.nImagenum
				End If
				nRectype = eTypeImages.clngClientImage
				
				'+ Imagen de Propuestas de siniestros
			Case "SCA10-3"
				nImagenum = sKey
				nRectype = eTypeImages.clngFireBudgetImage
				
				'+ Imagen de la orden de servicio
			Case "SCA593"
				lobjImage = eRemoteDB.NetHelper.CreateClassInstance("eClaim.Prof_ord")
				If lobjImage.Find_nServ(sKey) Then
					nImagenum = lobjImage.nImagenum
				End If
				nRectype = eTypeImages.clngProf_ordImage
			Case Else
				nRectype = 0
		End Select
		'UPGRADE_NOTE: Object lobjImage may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lobjImage = Nothing
	End Function
	
	'% Update_Image: actualiza la imagen en la tabla
	Public Function Update_Image(ByVal nImagenum As Integer, ByVal nConsec As Integer, ByVal sSource As String) As Boolean
		Dim lrecupdImage As eRemoteDB.Execute
		
		On Error GoTo Update_Image_err
		
		lrecupdImage = New eRemoteDB.Execute
		
		'+ Definición de parámetros para actualizar el campo iImage de la tabla Images
		'+ Información leída el 07/06/2000 02:06:08 PM
		With lrecupdImage
			.SQL = "Update Images set iImage = '" & sSource & "' Where nImagenum = " & nImagenum & " and nConsec = " & nConsec
			Update_Image = .Run(False)
		End With
		'UPGRADE_NOTE: Object lrecupdImage may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecupdImage = Nothing
		
Update_Image_err: 
		If Err.Number Then
			Update_Image = False
		End If
		On Error GoTo 0
	End Function
	
	'% BuildConnectionString: contruye el string de connección de la base de datos
	Public Function BuildConnectionString() As String
		Dim lclsRegistry As eFunctions.Values
        Dim lstrLogin As String = ""
        Dim lstrPassWord As String = ""
        Dim lstrString As String = String.Empty
		
		'- Variable para identificar el tipo de servidor al cual se conecta
		Dim lServer As eFunctions.Tables.sTypeServer
		
		Call insGetLoginPsw(lstrLogin, lstrPassWord)
		
		lclsRegistry = New eFunctions.Values
		lServer = CShort(lclsRegistry.insGetSetting("Server", CStr(eFunctions.Tables.sTypeServer.sSQLServer7)))
		
		'+ Se arma el String de conección, dependiendo del tipo de servidor asociado
		Select Case lServer
			Case eFunctions.Tables.sTypeServer.sSQLServer65, eFunctions.Tables.sTypeServer.sSQLServer7
				lstrString = "Provider=" & lclsRegistry.insGetSetting("Provider", "SQLOLEDB.1") & ";Server=" & lclsRegistry.insGetSetting("ServerName", "Cadillacs") & ";Database=" & lclsRegistry.insGetSetting("Database", "Insudb") & ";uid=" & lstrLogin & ";pwd=" & lstrPassWord
			Case eFunctions.Tables.sTypeServer.sOracle
				lstrString = "Provider=MSDAORA.1" & ";Data Source=" & lclsRegistry.insGetSetting("Provider", "Time") & ";uid=" & lstrLogin & ";pwd=" & lstrPassWord
			Case eFunctions.Tables.sTypeServer.sInformix
			Case eFunctions.Tables.sTypeServer.sDB2
		End Select
		'UPGRADE_NOTE: Object lclsRegistry may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsRegistry = Nothing
		BuildConnectionString = lstrString
	End Function
	
	'% insGetLoginPsw: toma los valores del usuario para la sesión
	Private Sub insGetLoginPsw(ByRef Login As String, ByRef PassWord As String)
        Dim objContext As New eRemoteDB.ASPSupport
		Dim lclsgenHandPass As genHandPass
		
		On Error Resume Next
		
		lclsgenHandPass = New genHandPass

        Login = objContext.GetASPSessionValue("sInitials")
		
        PassWord = lclsgenHandPass.StrDecode(objContext.GetASPSessionValue("sAccessWo"))
		On Error GoTo 0

	End Sub
	
	'% EmptyImageFolder: Borra archivos de directorio de imagenes
	Public Sub EmptyImageFolder()
		Dim lstrFile As Object
		Dim lstrSourceFile As Object
		Dim lstrSourceDir As Object
		Dim lstrSource As Object
		
		lstrSource = Me.sSource
		
		lstrSourceFile = Mid(lstrSource, InStrRev(lstrSource, "\") + 1)
		
		lstrSourceDir = Mid(lstrSource, 1, InStrRev(lstrSource, "\"))
		
		'UPGRADE_WARNING: Dir has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
		lstrFile = Dir(lstrSourceDir, FileAttribute.Archive)
		
		On Error Resume Next
		While lstrFile <> String.Empty
			If lstrFile <> lstrSourceFile Then
				Kill(lstrSourceDir & lstrFile)
			End If
			'UPGRADE_WARNING: Dir has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
			lstrFile = Dir()
		End While
		On Error GoTo 0
	End Sub
	
	'% Class_Initialize: se controla la apertura de la clase
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		nImagenum = numNull
		nConsec = numNull
		sDescript = String.Empty
		dNulldate = dtmNull
		nRectype = numNull
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
End Class






