Option Strict Off
Option Explicit On
Public Class Main
	'%-------------------------------------------------------%'
	'% $Workfile:: Main.cls                                 $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 5/11/03 5:46p                                $%'
	'% $Revision:: 9                                        $%'
	'%-------------------------------------------------------%'
	
	Public sSche_code As String
	
	Private mstrCachePath As String
	
	
	
	
	'% Makemenu: Construye el menú del módulo de errores
	Public Function Makemenu(ByVal sCodMen As String) As String
		Dim lobjValues As eFunctions.Values
		Dim lclsWindows As eSecurity.Windows
		Dim lcolWindows As eSecurity.Windowss
		Dim lclsGrid As eFunctions.Grid
		Dim lclsSecur_sche As eSecurity.Secur_sche
		Dim lclsGeneral As eGeneral.GeneralFunction
		Dim lintSecurLev As Short
        Dim lstrMenu As String
        Dim lstrDeniedMess As String = String.Empty
		
        If Not IsIDEMode() Then
        End If
		lclsGrid = New eFunctions.Grid
		
		'+Código javascript del insGoto()
        lstrMenu = "<SCRIPT>" & vbCrLf & "function insGoTo(RefUrl){" & vbCrLf & "  open('/VTimeNet/Common/GoTo.aspx?sCodispl=' + RefUrl, 'ErrorsWindow'," & vbCrLf & "       'toolbar=no,resizable=yes,location=no,directories=no,' + " & vbCrLf & "       'status=yes,menubar=no,copyhistory=no,width=750,height=450,left=20,top=20');" & vbCrLf & "  top.close();" & vbCrLf & "}" & vbCrLf & "</SCRIPT>" & vbCrLf
		
		'+Se inicializa grid
		With lclsGrid
			Call .Columns.AddTextColumn(0, C_DESCRIPT, "tctDescript", 40, "")
			
			.AddButton = False
			.DeleteButton = False
			.Columns("Sel").GridVisible = False
		End With
		
		'+Antes de cargar las transacciones, se verifica una vez si se debe validar cada
		'+una de dichas transascciones
		lclsSecur_sche = New eSecurity.Secur_sche
		
		'+Si no encuentra esquema, se indica por omision que realice validaciones,
		'+para evitar que ingrese como supervisor
		'+(aunque de todas formas no existiran registros en Levels)
		If Not lclsSecur_sche.Find(sSche_code, True) Then
			lintSecurLev = 2
		Else
			lintSecurLev = lclsSecur_sche.nSecurlev
		End If
		
		lclsSecur_sche = Nothing
		
		'+Si se requiere validar se carga mensaje de transaccion denegada por si se requiere
		If lintSecurLev = 2 Then
			'+ Se obtiene el mensage de: "Transacción no permitida para su esquema"
			lclsGeneral = New eGeneral.GeneralFunction
			lstrDeniedMess = lclsGeneral.insLoadMessage(12103)
			lclsGeneral = Nothing
		End If
		
		'+Se buscan transacciones de módulo de errores
		'+y con ellas se crean las filas del grid
		lcolWindows = New eSecurity.Windowss
		If lcolWindows.FindCodMen(sCodMen, lintSecurLev, sSche_code) Then
			For	Each lclsWindows In lcolWindows
				With lclsGrid
					.Columns("tctDescript").DefValue = lclsWindows.sDescript
					If lclsWindows.nIndPermitted = 1 Then
						.Columns("tctDescript").HRefScript = "insGoTo('" & lclsWindows.sCodispl & "');"
					Else
						.Columns("tctDescript").HRefScript = "alert('" & lstrDeniedMess & "');"
					End If
					
					lstrMenu = lstrMenu & .DoRow
				End With
			Next lclsWindows
		End If
		lcolWindows = Nothing
		
		lobjValues = New eFunctions.Values
		lstrMenu = lstrMenu & lclsGrid.closeTable & "<TABLE WIDTH=100%><TR><TD></TD></TR>" & vbCrLf & "<TR><TD CLASS=""HORLINE""></TD></TR>" & vbCrLf & "<TR><TD ALIGN=""RIGHT"">" & lobjValues.ButtonAcceptCancel( ,  , False,  , eFunctions.Values.eButtonsToShow.OnlyCancel) & "</TD></TR></TABLE>"
		lobjValues = Nothing
		
		lclsGrid = Nothing
		'Call SaveBufferToFile(lstrFilename, lstrMenu)
		'End If
		
		Makemenu = lstrMenu
		
		lclsGrid = Nothing
		lclsSecur_sche = Nothing
		lclsGeneral = Nothing
		lcolWindows = Nothing
		lobjValues = Nothing
		
		Exit Function
	End Function
End Class











