Option Strict Off
Option Explicit On
Public Class Layout
	
	Public sSessionID As String
	
	Public nUsercode As Integer
	
	'**%Objective:
	'**%Parameters:
	'**%    sCodispl - Code of the window (logical code).
	'%Objetivo: Ejecuta acciones genericas al momento de comenzar a procesar la pagina ASP.
	'%          Este metodo puede devolver código HTML o JavaScript que se puede ser utlizado en la pagina procesada.
	'%Parámetros:
	'%      sCodispl - Código identificativo de la ventana (lógico).
	Public Function BeginPage(ByVal sCodispl As String) As String
		On Error GoTo ErrorHandler
		
#If LOG Then
		'UPGRADE_NOTE: #If #EndIf block was not upgraded because the expression LOG did not evaluate to True or was not evaluated. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="27EE2C3C-05AF-4C04-B2AF-657B4FB6B5FC"'
		Call eRemotedb.FileSupport.AddBufferToFile(sSessionID & "|Begin|Page|" & sCodispl, sSessionID)
#End If
		
		Exit Function
ErrorHandler: 
		'UPGRADE_WARNING: Array has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
		ProcError("Layout.BeginPage(sCodispl)", New Object(){sCodispl})
	End Function
	
	'**%Objective:
	'**%Parameters:
	'**%    sCodispl - Code of the window (logical code).
	'%Objetivo: Ejecuta acciones genericas antes de que se haga el proceso de preparacion de los datos a ser mostrados por la pagina.
	'%          Este metodo puede devolver código HTML o JavaScript que se puede ser utlizado en la pagina procesada.
	'%Parámetros:
	'%      sCodispl - Código identificativo de la ventana (lógico).
	Public Function BeforePre(ByVal sCodispl As String) As String
		On Error GoTo ErrorHandler
		
#If LOG Then
		'UPGRADE_NOTE: #If #EndIf block was not upgraded because the expression LOG did not evaluate to True or was not evaluated. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="27EE2C3C-05AF-4C04-B2AF-657B4FB6B5FC"'
		Call eRemotedb.FileSupport.AddBufferToFile(sSessionID & "|BeforePre|Process|" & sCodispl, sSessionID)
#End If
		
		Exit Function
ErrorHandler: 
		'UPGRADE_WARNING: Array has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
		ProcError("Layout.BeforePre(sCodispl)", New Object(){sCodispl})
	End Function
	
	'**%Objective:
	'**%Parameters:
	'**%    sCodispl - Code of the window (logical code).
	'%Objetivo: Ejecuta acciones genericas despues que se haga el proceso de preparacion de los datos a ser mostrados por la pagina.
	'%          Este metodo puede devolver código HTML o JavaScript que se puede ser utlizado en la pagina procesada.
	'%Parámetros:
	'%      sCodispl - Código identificativo de la ventana (lógico).
	Public Function AfterPre(ByVal sCodispl As String) As String
		On Error GoTo ErrorHandler
		
#If LOG Then
		'UPGRADE_NOTE: #If #EndIf block was not upgraded because the expression LOG did not evaluate to True or was not evaluated. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="27EE2C3C-05AF-4C04-B2AF-657B4FB6B5FC"'
		Call eRemotedb.FileSupport.AddBufferToFile(sSessionID & "|AfterPre|Process|" & sCodispl, sSessionID)
#End If
		
		Exit Function
ErrorHandler: 
		'UPGRADE_WARNING: Array has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
		ProcError("Layout.AfterPre(sCodispl)", New Object(){sCodispl})
	End Function
	
	'**%Objective:
	'**%Parameters:
	'**%    sCodispl - Code of the window (logical code).
	'%Objetivo: Ejecuta acciones genericas antes de que sea realizada las validaciones de la pagina procesada.
	'%          Este metodo puede devolver código HTML o JavaScript que se puede ser utlizado en la pagina procesada.
	'%Parámetros:
	'%      sCodispl - Código identificativo de la ventana (lógico).
	Public Function BeforeValidate(ByVal sCodispl As String) As String
		On Error GoTo ErrorHandler
		
#If LOG Then
		'UPGRADE_NOTE: #If #EndIf block was not upgraded because the expression LOG did not evaluate to True or was not evaluated. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="27EE2C3C-05AF-4C04-B2AF-657B4FB6B5FC"'
		Call eRemotedb.FileSupport.AddBufferToFile(sSessionID & "|BeforeValidate|Process|" & sCodispl, sSessionID)
#End If
		
		Exit Function
ErrorHandler: 
		'UPGRADE_WARNING: Array has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
		ProcError("Layout.BeforeValidate(sCodispl)", New Object(){sCodispl})
	End Function
	
	'**%Objective:
	'**%Parameters:
	'**%    sCodispl - Code of the window (logical code).
	'%Objetivo: Ejecuta acciones genericas despues de que sea realizada las validaciones de la pagina procesada.
	'%          Este metodo puede devolver código HTML o JavaScript que se puede ser utlizado en la pagina procesada.
	'%Parámetros:
	'%      sCodispl - Código identificativo de la ventana (lógico).
	Public Function AfterValidate(ByVal sCodispl As String) As String
		On Error GoTo ErrorHandler
		
#If LOG Then
		'UPGRADE_NOTE: #If #EndIf block was not upgraded because the expression LOG did not evaluate to True or was not evaluated. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="27EE2C3C-05AF-4C04-B2AF-657B4FB6B5FC"'
		Call eRemotedb.FileSupport.AddBufferToFile(sSessionID & "|AfterValidate|Process|" & sCodispl, sSessionID)
#End If
		
		Exit Function
ErrorHandler: 
		'UPGRADE_WARNING: Array has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
		ProcError("Layout.AfterValidate(sCodispl)", New Object(){sCodispl})
	End Function
	
	'**%Objective:
	'**%Parameters:
	'**%    sCodispl - Code of the window (logical code).
	'%Objetivo: Ejecuta acciones genericas antes de que sea realizado el posteo de informaciónde la pagina procesada.
	'%          Este metodo puede devolver código HTML o JavaScript que se puede ser utlizado en la pagina procesada.
	'%Parámetros:
	'%      sCodispl - Código identificativo de la ventana (lógico).
	Public Function BeforePost(ByVal sCodispl As String) As String
		On Error GoTo ErrorHandler
		
#If LOG Then
		'UPGRADE_NOTE: #If #EndIf block was not upgraded because the expression LOG did not evaluate to True or was not evaluated. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="27EE2C3C-05AF-4C04-B2AF-657B4FB6B5FC"'
		Call eRemotedb.FileSupport.AddBufferToFile(sSessionID & "|BeforePost|Process|" & sCodispl, sSessionID)
#End If
		
		Exit Function
ErrorHandler: 
		'UPGRADE_WARNING: Array has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
		ProcError("Layout.BeforePost(sCodispl)", New Object(){sCodispl})
	End Function
	
	'**%Objective:
	'**%Parameters:
	'**%    sCodispl - Code of the window (logical code).
	'%Objetivo: Ejecuta acciones genericas despues de que sea realizado el posteo de informaciónde la pagina procesada.
	'%          Este metodo puede devolver código HTML o JavaScript que se puede ser utlizado en la pagina procesada.
	'%Parámetros:
	'%      sCodispl - Código identificativo de la ventana (lógico).
	Public Function AfterPost(ByVal sCodispl As String) As String
		On Error GoTo ErrorHandler
		
#If LOG Then
		'UPGRADE_NOTE: #If #EndIf block was not upgraded because the expression LOG did not evaluate to True or was not evaluated. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="27EE2C3C-05AF-4C04-B2AF-657B4FB6B5FC"'
		Call eRemotedb.FileSupport.AddBufferToFile(sSessionID & "|AfterPost|Process|" & sCodispl, sSessionID)
#End If
		
		Exit Function
ErrorHandler: 
		'UPGRADE_WARNING: Array has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
		ProcError("Layout.AfterPost(sCodispl)", New Object(){sCodispl})
	End Function
	
	'**%Objective:
	'**%Parameters:
	'**%    sCodispl - Code of the window (logical code).
	'%Objetivo: Ejecuta acciones genericas al finalizar el proceso de la pagina ASP.
	'%          Este metodo puede devolver código HTML o JavaScript que se puede ser utlizado en la pagina procesada.
	'%Parámetros:
	'%      sCodispl - Código identificativo de la ventana (lógico).
	Public Function FinishPage(ByVal sCodispl As String) As String
		On Error GoTo ErrorHandler
		
#If LOG Then
		'UPGRADE_NOTE: #If #EndIf block was not upgraded because the expression LOG did not evaluate to True or was not evaluated. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="27EE2C3C-05AF-4C04-B2AF-657B4FB6B5FC"'
		Call eRemotedb.FileSupport.AddBufferToFile(sSessionID & "|Finish|Page|" & sCodispl, sSessionID)
#End If
		
		Exit Function
ErrorHandler: 
		'UPGRADE_WARNING: Array has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
		ProcError("Layout.FinishPage(sCodispl)", New Object(){sCodispl})
	End Function
	
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		sSessionID = "000000000"
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
	
	
	'**%Objective:
	'**%Parameters:
	'**%      sDescript - .
	'%Objetivo:
	'%Parámetros:
	'%      sDescript - .
	Public Function BeginProcess(ByVal sDescript As String) As String
		On Error GoTo ErrorHandler
		
#If LOG Then
		'UPGRADE_NOTE: #If #EndIf block was not upgraded because the expression LOG did not evaluate to True or was not evaluated. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="27EE2C3C-05AF-4C04-B2AF-657B4FB6B5FC"'
		Call eRemotedb.FileSupport.AddBufferToFile(sSessionID & "|Begin|Process|" & sDescript, sSessionID)
#End If
		
		Exit Function
ErrorHandler: 
		'UPGRADE_WARNING: Array has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
		ProcError("Layout.BeginProcess(sDescript)", New Object(){sDescript})
	End Function
	
	'**%Parameters:
	'**%      sDescript - .
	'%Objetivo:
	'%Parámetros:
	'%      sDescript - .
	Public Function FinishProcess(ByVal sDescript As String) As String
		On Error GoTo ErrorHandler
		
#If LOG Then
		'UPGRADE_NOTE: #If #EndIf block was not upgraded because the expression LOG did not evaluate to True or was not evaluated. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="27EE2C3C-05AF-4C04-B2AF-657B4FB6B5FC"'
		Call eRemotedb.FileSupport.AddBufferToFile(sSessionID & "|Finish|Process|" & sDescript, sSessionID)
#End If
		
		Exit Function
ErrorHandler: 
		'UPGRADE_WARNING: Array has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
		ProcError("Layout.FinishProcess(sDescript)", New Object(){sDescript})
	End Function
End Class






