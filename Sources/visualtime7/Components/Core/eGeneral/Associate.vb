Option Strict Off
Option Explicit On
Public Class Associate
	'%-------------------------------------------------------%'
	'% $Workfile:: Associate.cls                            $%'
	'% $Author:: Nvaplat18                                  $%'
	'% $Date:: 14/11/03 13.00                               $%'
	'% $Revision:: 10                                       $%'
	'%-------------------------------------------------------%'
	
	Private mobjGrid As eFunctions.Grid
	
	'% insDefineHeader: Define las características del grid
	Private Function insDefineHeader() As Boolean
		With mobjGrid
			.AddButton = False
			.DeleteButton = False
			.bCheckVisible = False
			
			Call .Columns.AddTextColumn(0, "Consultas", "tctDescript", 40, String.Empty,  , "Consultas asociadas al campo en tratamiento")
			
			.Columns("Sel").GridVisible = False
		End With
	End Function
	
	'% Makemenu: Construye el Menu del Modulo de Errores
	Public Function MakeMenu(ByVal nKeynum As Integer, ByVal sStringQuery As String) As String
		Dim lobjValues As eFunctions.Values
		Dim lcolInquiry_as As Inquiry_ass
		Dim lclsInquiry_as As Inquiry_as
		Dim lclsQuery As eRemoteDB.Query
		
		mobjGrid = New eFunctions.Grid
		lcolInquiry_as = New Inquiry_ass
		lclsInquiry_as = New Inquiry_as
		lobjValues = New eFunctions.Values
		lclsQuery = New eRemoteDB.Query
		
		MakeMenu = insGoTo()
		
		MakeMenu = MakeMenu & "<DIV ID=""Scroll"" STYLE=""width:300;height:170;overflow:auto;outset gray"">"
		
		Call insDefineHeader()
		
		With lclsQuery
			If .OpenQuery("Windows", "sDescript", "sCodispl='GE777'") Then
				MakeMenu = MakeMenu & "<TITLE>" & .FieldToClass("sDescript") & " (" & lobjValues.getMessage(nKeynum, "Table1014") & ")</TITLE>"
				.CloseQuery()
			End If
		End With
		
		If lcolInquiry_as.Find(nKeynum) Then
			For	Each lclsInquiry_as In lcolInquiry_as
				With mobjGrid
					.Columns("tctDescript").DefValue = lclsInquiry_as.sDescript
					.Columns("tctDescript").HRefScript = "insGoTo('/VTimeNet/Common/GoTo.aspx?sCodispl=" & lclsInquiry_as.sCodispl & "&" & Replace(sStringQuery, "!", "&") & "');"
					MakeMenu = MakeMenu & .DoRow
				End With
			Next lclsInquiry_as
		End If
		MakeMenu = MakeMenu & mobjGrid.closeTable & "</DIV>" & "<HR><TABLE WIDTH=""100%""><TR>" & "<TD WIDTH=""5%"">" & lobjValues.ButtonAbout("GE777") & "</TD>" & "<TD WIDTH=""5%"">" & lobjValues.ButtonHelp("GE777") & "</TD>" & "<TD ALIGN=""Right"" >" & lobjValues.ButtonAcceptCancel( ,  , False,  , eFunctions.Values.eButtonsToShow.OnlyCancel) & "</TD>" & "</TR></TABLE>"
		
		'UPGRADE_NOTE: Object mobjGrid may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		mobjGrid = Nothing
		'UPGRADE_NOTE: Object lcolInquiry_as may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lcolInquiry_as = Nothing
		'UPGRADE_NOTE: Object lclsInquiry_as may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsInquiry_as = Nothing
		'UPGRADE_NOTE: Object lobjValues may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lobjValues = Nothing
		'UPGRADE_NOTE: Object lclsQuery may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsQuery = Nothing
	End Function
	
	'% insGoTo: se agrega el codigo JAVASCRIPT para levantar una nueva ventana
	Private Function insGoTo() As String
		insGoTo = "<SCRIPT>" & "function insGoTo(RefUrl){" & "open(RefUrl, ""ErrorsWindow""," & """toolbar=no,resizable=yes,location=no,directories=no,status=yes,menubar=no,copyhistory=no,width=750,height=450,left=20,top=20"");" & "top.close()" & "}" & "</SCRIPT>"
	End Function
End Class






