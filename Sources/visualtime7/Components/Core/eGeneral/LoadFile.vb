Option Strict Off
Option Explicit On
Public Class LoadFile
	Private Declare Sub keybd_event Lib "USER32" (ByVal bVk As Byte, ByVal bScan As Byte, ByVal dwFlags As Integer, ByVal dwExtraInfo As Integer)
	Const VK_CONTROL As Integer = &H11
	Const KEYEVENTF_KEYUP As Integer = &H2
	Const VK_ESCAPE As Integer = &H1B
	Const ATTR_NORMAL As Short = 0
	Const ATTR_READONLY As Short = 1
	Const ATTR_HIDDEN As Short = 2
	Const ATTR_SYSTEM As Short = 4
	Const ATTR_VOLUME As Short = 8
	Const ATTR_DIRECTORY As Short = 16
	Const ATTR_ARCHIVE As Short = 32
	Const FO_MOVE As Short = 1
	Const FO_COPY As Short = 2
	Const FO_DELETE As Short = 3
	Const FO_RENAME As Short = 4
	
	Const FOF_MULTIDESTFILES As Integer = &H1
	Const FOF_SILENT As Integer = &H4
	Const FOF_RENAMEONCOLLISION As Integer = &H8
	Const FOF_NOCONFIRMATION As Integer = &H10
	Const FOF_WANTMAPPINGHANDLE As Integer = &H20
	Const FOF_ALLOWUNDO As Integer = &H40
	Const FOF_FILESONLY As Integer = &H80
	Const FOF_SIMPLEPROGRESS As Integer = &H100
	Const FOF_NOCONFIRMMKDIR As Integer = &H200
	
	Private Structure SHFILEOPSTRUCT
		Dim hwnd As Integer
		Dim wFunc As Integer
		Dim pFrom As String
		Dim pTo As String
		Dim fFlags As Integer
		Dim fAnyOperationsAborted As Boolean
		Dim hNameMappings As Integer
		Dim lpszProgressTitle As String
	End Structure
	
	Private Declare Function ShellExecute Lib "shell32.dll"  Alias "ShellExecuteA"(ByVal hwnd As Integer, ByVal lpOperation As String, ByVal lpFile As String, ByVal lpParameters As String, ByVal lpDirectory As String, ByVal nShowCmd As Integer) As Integer
	'UPGRADE_WARNING: Structure SHFILEOPSTRUCT may require marshalling attributes to be passed as an argument in this Declare statement. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="C429C3A5-5D47-4CD9-8F51-74A1616405DC"'
	Private Declare Function SHFileOperation Lib "shell32.dll"  Alias "SHFileOperationA"(ByRef lpFileOp As SHFILEOPSTRUCT) As Integer
	Private Declare Function RegOpenKeyEx Lib "advapi32"  Alias "RegOpenKeyExA"(ByVal HKey As Integer, ByVal lpSubKey As String, ByVal ulOptions As Integer, ByVal samDesired As Integer, ByRef phkResult As Integer) As Integer
	Private Declare Function RegQueryValueEx Lib "advapi32"  Alias "RegQueryValueExA"(ByVal HKey As Integer, ByVal lpValueName As String, ByVal lpReserved As Integer, ByRef lpType As Integer, ByVal lpData As String, ByRef lpcbData As Integer) As Integer
	Private Declare Function RegCloseKey Lib "advapi32" (ByVal HKey As Integer) As Integer
	Private Declare Function SHFormatDrive Lib "shell32" (ByVal hwnd As Integer, ByVal Drive As Integer, ByVal fmtID As Integer, ByVal options As Integer) As Integer
	Private Declare Function GetDriveType Lib "kernel32"  Alias "GetDriveTypeA"(ByVal nDrive As String) As Integer
	Dim AlarmTime As Object 'search (btnAll_Click() has its own)
	Dim UserDate As Date
	
	'%Load: Copia archivos desde una ruta especifica al servidor
	Public Function Load(ByVal sFilePath As String) As Boolean
        Dim chkShowFile As Object = New Object
        Dim chkConfirmMkDir As Object = New Object
        Dim chkConfirmOp As Object = New Object
        Dim chkRename As Object = New Object
        Dim chkShowDlg As Object = New Object
        Dim chkUndo As Object = New Object
        Dim lclsRegistry As eFunctions.Values
        Dim lobjFileOp As SHFILEOPSTRUCT = New SHFILEOPSTRUCT
        Dim lstrFileName As String
		
		On Error GoTo Load_err
		
		lclsRegistry = New eFunctions.Values
		
		lobjFileOp.hwnd = 0
		lobjFileOp.wFunc = FO_COPY
		
		ChDrive(sFilePath)
		
		lobjFileOp.pFrom = sFilePath & Chr(0)
		
		lstrFileName = UCase(lclsRegistry.insGetSetting("LOADFILE", String.Empty, "CONFIG"))
		
		'UPGRADE_NOTE: Object lclsRegistry may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsRegistry = Nothing
		lobjFileOp.pTo = lstrFileName & Chr(0)
		
		If chkUndo = 1 Then
			lobjFileOp.fFlags = lobjFileOp.fFlags Or FOF_ALLOWUNDO
		End If
		If chkShowDlg = 0 Then
			lobjFileOp.fFlags = lobjFileOp.fFlags Or FOF_SILENT
		End If
		If chkRename = 1 Then
			lobjFileOp.fFlags = lobjFileOp.fFlags Or FOF_RENAMEONCOLLISION
		End If
		If chkConfirmOp = 0 Then
			lobjFileOp.fFlags = lobjFileOp.fFlags Or FOF_NOCONFIRMATION
		End If
		If chkConfirmMkDir = 0 Then
			lobjFileOp.fFlags = lobjFileOp.fFlags Or FOF_NOCONFIRMMKDIR
		End If
		If chkShowFile = 0 Then
			lobjFileOp.fFlags = lobjFileOp.fFlags Or FOF_SIMPLEPROGRESS
			
		End If
		
		If SHFileOperation(lobjFileOp) <> 0 Then
			Load = False
		End If
		
Load_err: 
		If Err.Number Then
			Load = False
		End If
		On Error GoTo 0
		
	End Function
	
	'%UnLoad: Copia archivos desde el Servidor a una ruta especifica
	Public Function UnLoad(ByVal sFilePathServer As String, ByVal sFilePath As String) As Boolean
        Dim chkShowFile As Object = New Object
        Dim chkConfirmMkDir As Object = New Object
        Dim chkConfirmOp As Object = New Object
        Dim chkRename As Object = New Object
        Dim chkShowDlg As Object = New Object
        Dim chkUndo As Object = New Object

        Dim lobjFileOp As SHFILEOPSTRUCT = New SHFILEOPSTRUCT
        Dim lstrFileName As String
		
		On Error GoTo UnLoad_err
		
		lobjFileOp.hwnd = 0
		lobjFileOp.wFunc = FO_COPY
		
		ChDrive(sFilePathServer)
		
		lobjFileOp.pFrom = sFilePathServer & Chr(0)
		lobjFileOp.pTo = sFilePath & Chr(0)
		
		If chkUndo = 1 Then
			lobjFileOp.fFlags = lobjFileOp.fFlags Or FOF_ALLOWUNDO
		End If
		If chkShowDlg = 0 Then
			lobjFileOp.fFlags = lobjFileOp.fFlags Or FOF_SILENT
		End If
		If chkRename = 1 Then
			lobjFileOp.fFlags = lobjFileOp.fFlags Or FOF_RENAMEONCOLLISION
		End If
		If chkConfirmOp = 0 Then
			lobjFileOp.fFlags = lobjFileOp.fFlags Or FOF_NOCONFIRMATION
		End If
		If chkConfirmMkDir = 0 Then
			lobjFileOp.fFlags = lobjFileOp.fFlags Or FOF_NOCONFIRMMKDIR
		End If
		If chkShowFile = 0 Then
			lobjFileOp.fFlags = lobjFileOp.fFlags Or FOF_SIMPLEPROGRESS
			
		End If
		
		If SHFileOperation(lobjFileOp) <> 0 Then
			UnLoad = False
		End If
		
UnLoad_err: 
		If Err.Number Then
			UnLoad = False
		End If
		On Error GoTo 0
		
	End Function
End Class






