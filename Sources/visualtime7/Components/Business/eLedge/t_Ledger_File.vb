Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Public Class t_Ledger_File
	'%-------------------------------------------------------%'
	'% $Workfile:: t_Ledger_File.cls                        $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 1:18p                                $%'
	'% $Revision:: 4                                        $%'
	'%-------------------------------------------------------%'
	
	'**- The properties of the class are defined
	'-   Se definen las propiedades de la clase
	
	'**- Column_name                                                                                                                      Type                                                                                                                             Computed                            Length      Prec  Scale  Nullable                            TrimTrailingBlanks                  FixedLenNullInSource                Collation
	'-   Nombre de la columna                                                                                                             Tipo                                                                                                                             Computed                            Longitud    Prec  Escala Admite nulos                        TrimTrailingBlanks                  FixedLenNullInSource                Collation
	Public sKey As String '                                                                                                     char                                                                                                                             no                                  30                      no                                  yes                                 no                                  SQL_Latin1_General_CP1_CI_AS
	Public nConsec As Integer '                                                                                                     int                                                                                                                              no                                  4           10    0     no                                  (n/a)                               (n/a)                               NULL
	Public dEffecdate As Date '                                                                                                     datetime                                                                                                                         no                                  8                       no                                  (n/a)                               (n/a)                               NULL
	Public sSub_nr As String '                                                                                                     char                                                                                                                             no                                  10                      yes                                 yes                                 yes                                 SQL_Latin1_General_CP1_CI_AS
	Public sAccount As String '                                                                                                     char                                                                                                                             no                                  20                      yes                                 yes                                 yes                                 SQL_Latin1_General_CP1_CI_AS
	Public sInv_nr As String '                                                                                                     char                                                                                                                             no                                  10                      yes                                 yes                                 yes                                 SQL_Latin1_General_CP1_CI_AS
	Public sDescript As String '                                                                                                     char                                                                                                                             no                                  40                      yes                                 yes                                 yes                                 SQL_Latin1_General_CP1_CI_AS
	Public sVat_code As String '                                                                                                     char                                                                                                                             no                                  1                       yes                                 yes                                 yes                                 SQL_Latin1_General_CP1_CI_AS
	Public sCost_code As String '                                                                                                     char                                                                                                                             no                                  10                      yes                                 yes                                 yes                                 SQL_Latin1_General_CP1_CI_AS
	Public sCur_code As String '                                                                                                     char                                                                                                                             no                                  3                       yes                                 yes                                 yes                                 SQL_Latin1_General_CP1_CI_AS
	Public nAmount As Double '                                                                                                     decimal                                                                                                                          no                                  9           14    2     yes                                 (n/a)                               (n/a)                               NULL
	
	'**%insHWtxt: The HW txt file is performed
	'%  insHWtxt: Se genera el archivo txt de HW
	Private Function insHWtxt(ByVal lintBranch As Integer, ByVal lintProduct As Integer, ByVal lintType_process As Integer, ByVal lintUsercode As Integer) As Boolean
		
		'**- The variables to be use in the process are defined
		'-   Las variables usadas en el proceso son definidas
		
		Dim lrecHW As eRemoteDB.Execute
		Dim FileSave As Object
        Dim LineSave As Object = New Object
        Dim sLine As String
		Dim lstrNamePrev As String
        Dim lstrSub_nr As String
        Dim lstrAccount As String
        Dim lstrInv_nr As String
        Dim lstrDescript As String
        Dim lstrCost_code As String
		
		On Error GoTo insHWtxt_err
		
		insHWtxt = False
		
		lrecHW = New eRemoteDB.Execute
		
		sKey = Trim(CStr(lintUsercode)) & "HW" & Trim(CStr(VB.Day(Today))) & Trim(CStr(Month(Today))) & Trim(CStr(Year(Today))) & Trim(CStr(Hour(TimeOfDay))) & Trim(CStr(Minute(TimeOfDay))) & Trim(CStr(Second(TimeOfDay)))
		
		With lrecHW
			.StoredProcedure = "insHWtxt"
			
			.Parameters.Add("sKey", sKey, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 30, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", lintBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", lintProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nType_process", lintType_process, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", lintUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run(True) Then
				lstrNamePrev = String.Empty
				
				While Not .EOF
					If Trim(.FieldToClass("sFileName")) <> Trim(lstrNamePrev) Then
						'UPGRADE_NOTE: Object FileSave may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
						FileSave = Nothing
						FileSave = CreateObject("Scripting.FileSystemObject")
						
						If FileSave.FileExists(.FieldToClass("sFileName")) Then
							FileSave.DeleteFile(.FieldToClass("sFileName"))
						End If
						
						LineSave = FileSave.CreateTextFile(.FieldToClass("sFileName"))
						
						lstrNamePrev = Trim(.FieldToClass("sFileName"))
					End If
					
                    lstrSub_nr = .FieldToClass("sSub_nr")
                    lstrAccount = Mid(.FieldToClass("sAccount"), 1, 10)
                    lstrInv_nr = .FieldToClass("sInv_nr")
                    lstrDescript = .FieldToClass("sDescript")
                    lstrCost_code = .FieldToClass("sCost_code")
					
                    sLine = CStr(Format(VB.Day(.FieldToClass("dEffecdate")), "00")) & "/" & CStr(Format(Month(.FieldToClass("dEffecdate")), "00")) & "/" & CStr(Format(Year(.FieldToClass("dEffecdate")), "0000")) & lstrSub_nr & lstrAccount & lstrInv_nr & lstrDescript & " " & lstrCost_code & .FieldToClass("sCur_code") & .FieldToClass("nAmount") & ";"
					
					LineSave.WriteLine(sLine)
					
					.RNext()
				End While
				
				.RCloseRec()
				
				insHWtxt = True
			End If
		End With
		
		'UPGRADE_NOTE: Object FileSave may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		FileSave = Nothing
		'UPGRADE_NOTE: Object lrecHW may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecHW = Nothing
		
insHWtxt_err: 
		If Err.Number Then
			insHWtxt = False
		End If
		
		On Error GoTo 0
	End Function
	
	'**%insKO/FZPtxt: The KO_FZP txt file is performed
	'%  insKO/FZPtxt: Se genera el archivo txt de KO/FZP
	Private Function insKO_FZPtxt(ByVal lintBranch As Integer, ByVal lintProduct As Integer, ByVal lintType_process As Integer, ByVal lintUsercode As Integer) As Boolean
		
		'**- The variables to be use in the process are defined
		'-   Las variables usadas en el proceso son definidas
		
		Dim lrecKO_FZP As eRemoteDB.Execute
		Dim FileSave As Object
        Dim LineSave As Object = New Object
        Dim sLine As String
		Dim lstrNamePrev As String
        Dim lstrSub_nr As String
        Dim lstrAccount As String
        Dim lstrInv_nr As String
        Dim lstrDescript As String
        Dim lstrCost_code As String
		
		On Error GoTo insKO_FZPtxt_err
		
		insKO_FZPtxt = False
		
		lrecKO_FZP = New eRemoteDB.Execute
		
		sKey = Trim(CStr(lintUsercode)) & "KO" & Trim(CStr(VB.Day(Today))) & Trim(CStr(Month(Today))) & Trim(CStr(Year(Today))) & Trim(CStr(Hour(TimeOfDay))) & Trim(CStr(Minute(TimeOfDay))) & Trim(CStr(Second(TimeOfDay)))
		
		With lrecKO_FZP
			.StoredProcedure = "insKO_FZPtxt"
			
			.Parameters.Add("sKey", sKey, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 30, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", lintBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", lintProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nType_process", lintType_process, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", lintUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run(True) Then
				lstrNamePrev = String.Empty
				
				While Not .EOF
					If Trim(.FieldToClass("sFileName")) <> Trim(lstrNamePrev) Then
						'UPGRADE_NOTE: Object FileSave may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
						FileSave = Nothing
						FileSave = CreateObject("Scripting.FileSystemObject")
						
						If FileSave.FileExists(.FieldToClass("sFileName")) Then
							FileSave.DeleteFile(.FieldToClass("sFileName"))
						End If
						
						LineSave = FileSave.CreateTextFile(.FieldToClass("sFileName"))
						
						lstrNamePrev = Trim(.FieldToClass("sFileName"))
					End If
					
                    lstrSub_nr = .FieldToClass("sSub_nr")
                    lstrAccount = Mid(.FieldToClass("sAccount"), 1, 10)
                    lstrInv_nr = .FieldToClass("sInv_nr")
                    lstrDescript = .FieldToClass("sDescript")
                    lstrCost_code = .FieldToClass("sCost_code")
					
                    sLine = CStr(Format(VB.Day(.FieldToClass("dEffecdate")), "00")) & "/" & CStr(Format(Month(.FieldToClass("dEffecdate")), "00")) & "/" & CStr(Format(Year(.FieldToClass("dEffecdate")), "0000")) & lstrSub_nr & lstrAccount & lstrInv_nr & lstrDescript & " " & lstrCost_code & .FieldToClass("sCur_code") & .FieldToClass("nAmount") & ";"
					
					LineSave.WriteLine(sLine)
					
					.RNext()
				End While
				
				.RCloseRec()
				
				insKO_FZPtxt = True
			End If
		End With
		
		'UPGRADE_NOTE: Object FileSave may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		FileSave = Nothing
		'UPGRADE_NOTE: Object lrecKO_FZP may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecKO_FZP = Nothing
		
insKO_FZPtxt_err: 
		If Err.Number Then
			insKO_FZPtxt = False
		End If
		
		On Error GoTo 0
	End Function
	
	'**%insORVtxt: The ORV txt file is performed
	'%  insORVtxt: Se genera el archivo txt de ORV
	Private Function insORVtxt(ByVal lintBranch As Integer, ByVal lintProduct As Integer, ByVal lintType_process As Integer, ByVal lintUsercode As Integer) As Boolean
		
		'**- The variables to be use in the process are defined
		'-   Las variables usadas en el proceso son definidas
		
		Dim lrecORV As eRemoteDB.Execute
		Dim FileSave As Object
        Dim LineSave As Object = New Object
        Dim sLine As String
		Dim lstrNamePrev As String
        Dim lstrSub_nr As String
        Dim lstrAccount As String
        Dim lstrInv_nr As String
        Dim lstrDescript As String
        Dim lstrCost_code As String
		
		On Error GoTo insORVtxt_err
		
		insORVtxt = False
		
		lrecORV = New eRemoteDB.Execute
		
		sKey = Trim(CStr(lintUsercode)) & "ORV" & Trim(CStr(VB.Day(Today))) & Trim(CStr(Month(Today))) & Trim(CStr(Year(Today))) & Trim(CStr(Hour(TimeOfDay))) & Trim(CStr(Minute(TimeOfDay))) & Trim(CStr(Second(TimeOfDay)))
		
		With lrecORV
			.StoredProcedure = "insORVtxt"
			
			.Parameters.Add("sKey", sKey, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 30, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", lintBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", lintProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nType_process", lintType_process, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", lintUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run(True) Then
				lstrNamePrev = String.Empty
				
				While Not .EOF
					If Trim(.FieldToClass("sFileName")) <> Trim(lstrNamePrev) Then
						'UPGRADE_NOTE: Object FileSave may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
						FileSave = Nothing
						FileSave = CreateObject("Scripting.FileSystemObject")
						
						If FileSave.FileExists(.FieldToClass("sFileName")) Then
							FileSave.DeleteFile(.FieldToClass("sFileName"))
						End If
						
						LineSave = FileSave.CreateTextFile(.FieldToClass("sFileName"))
						
						lstrNamePrev = Trim(.FieldToClass("sFileName"))
					End If
					
                    lstrSub_nr = .FieldToClass("sSub_nr")
                    lstrAccount = Mid(.FieldToClass("sAccount"), 1, 10)
                    lstrInv_nr = .FieldToClass("sInv_nr")
                    lstrDescript = .FieldToClass("sDescript")
                    lstrCost_code = .FieldToClass("sCost_code")
					
                    sLine = CStr(Format(VB.Day(.FieldToClass("dEffecdate")), "00")) & "/" & CStr(Format(Month(.FieldToClass("dEffecdate")), "00")) & "/" & CStr(Format(Year(.FieldToClass("dEffecdate")), "0000")) & lstrSub_nr & lstrAccount & lstrInv_nr & lstrDescript & " " & lstrCost_code & .FieldToClass("sCur_code") & .FieldToClass("nAmount") & ";"
					
					LineSave.WriteLine(sLine)
					
					.RNext()
				End While
				
				.RCloseRec()
				
				insORVtxt = True
			End If
		End With
		
		'UPGRADE_NOTE: Object FileSave may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		FileSave = Nothing
		'UPGRADE_NOTE: Object lrecORV may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecORV = Nothing
		
insORVtxt_err: 
		If Err.Number Then
			insORVtxt = False
		End If
		
		On Error GoTo 0
	End Function
	
	'**%insPostCPL637: This function is in charge of generating the txt files
	'%  insPostCPL637: Esta función se encarga de generar los archivos txt
	Public Function insPostCPL637(ByVal lintBranch As Integer, ByVal lintProduct As Integer, ByVal lintTypeFile As Integer, ByVal lintType_process As Integer, ByVal lintUsercode As Integer) As Boolean
		On Error GoTo insPostCPL637_err
		
		insPostCPL637 = True
		
		Select Case lintTypeFile
			
			'**+ ORV File
			'+   Archivo ORV
			
			Case 1
				insPostCPL637 = insORVtxt(lintBranch, lintProduct, lintType_process, lintUsercode)
				
				'**+ HW File
				'+   Archivo HW
				
			Case 2
				insPostCPL637 = insHWtxt(lintBranch, lintProduct, lintType_process, lintUsercode)
				
				'**+ KO/FZP File
				'+   Archivo KO/FZP
				
			Case 3
				insPostCPL637 = insKO_FZPtxt(lintBranch, lintProduct, lintType_process, lintUsercode)
		End Select
		
insPostCPL637_err: 
		If Err.Number Then
			insPostCPL637 = False
		End If
		
		On Error GoTo 0
	End Function
	
	'**% insValCPL637: Page validation routine.
	'%   insValMCP001: Rutina de validación de la ventana.
	Public Function insValCPL637(ByVal sCodispl As String, ByVal lintBranch As Integer, ByVal lintProduct As Integer, ByVal lintTypeFile As Integer, ByVal lintType_process As Integer) As String
		Dim lclsErrors As eFunctions.Errors
		
		On Error GoTo insValCPL637_Err
		
		lclsErrors = New eFunctions.Errors
		
		insValCPL637 = String.Empty
		
		'**+ Branch field validation
		'+   Validación del campo ramo
		
		If lintBranch = 0 Or lintBranch = eRemoteDB.Constants.intNull Then
			Call lclsErrors.ErrorMessage(sCodispl, 9064)
		End If
		
		'**+ Product field validation
		'+   Validación del campo product
		
		If lintProduct = 0 Or lintProduct = eRemoteDB.Constants.intNull Then
			Call lclsErrors.ErrorMessage(sCodispl, 1014)
		End If
		
		'**+ file type field validation
		'+   Validación del campo tipo de archivo
		
		If lintTypeFile = 0 Or lintTypeFile = eRemoteDB.Constants.intNull Then
			Call lclsErrors.ErrorMessage(sCodispl, 98011)
		End If
		
		'**+ Process type field validation
		'+   Validación del campo tipo de proceso
		
		If lintType_process = 0 Or lintType_process = eRemoteDB.Constants.intNull Then
			Call lclsErrors.ErrorMessage(sCodispl, 1113)
		End If
		
		insValCPL637 = lclsErrors.Confirm
		
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
		
insValCPL637_Err: 
		If Err.Number Then
			insValCPL637 = insValCPL637 & Err.Description
		End If
		
		On Error GoTo 0
	End Function
End Class






