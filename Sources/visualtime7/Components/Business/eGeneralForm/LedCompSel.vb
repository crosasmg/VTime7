Option Strict Off
Option Explicit On
Public Class LedCompSel
	
	Private mobjGrid As eFunctions.Grid
	
	'**% insDefineHeader: define the structure of the grid that contains the accounting companies of the system.
	'% insDefineHeader: Define la estructura del grid que contiene las compañías contables del sistema
	Private Function insDefineHeader() As Boolean
		
		'**+ Define all the columns of the Grid.
		'+ Se definen todas las columnas del Grid
		
		With mobjGrid
			With .Columns
				Call .AddTextColumn(0, "Descripción", "tctDescript", 15, "",  , "Nombre de la compañía contable")
				Call .AddHiddenColumn("tcnLed_compan", CStr(0))
			End With
			
			.DeleteButton = False
			.AddButton = False
			.bOnlyForQuery = True
			.Top = 70
			.Codispl = "CP099"
			.Columns("tctDescript").EditRecord = False
			.Columns("Sel").GridVisible = False
		End With
	End Function
	
	'**% LoadLedCompInfo: restores the information that will contain the grid of the accounting companies
	'% LoadLedCompInfo: Devuelve la información que contendrá el grid de compañías contables
	Public Function LoadLedCompInfo(ByVal FieldName As String, Optional ByVal OnChange As String = "") As String
		Dim lobjValues As eFunctions.Values
		Dim lcolLed_compans As Object
		Dim lclsLed_compan As Object
        Dim lstrOnChange As String = ""

        lobjValues = New eFunctions.Values
		lcolLed_compans = eRemoteDB.NetHelper.CreateClassInstance("eLedge.Led_compans")
		
		Call insDefineHeader()
		
		LoadLedCompInfo = "<DIV ID=""Scroll"" style=""width:280;height:150;overflow:auto; outset gray"">"
		
		If OnChange <> String.Empty OrElse OnChange <>String.Empty Then
			lstrOnChange = "opener." & OnChange & ";"
		End If
		
		If lcolLed_compans.Find() Then
			For	Each lclsLed_compan In lcolLed_compans
				With mobjGrid
					.Columns("tcnLed_compan").DefValue = lclsLed_compan.nLed_compan
					.Columns("tctDescript").DefValue = lclsLed_compan.sDescript
					.Columns("tctDescript").HRefScript = "opener.document.forms[0].tcn" & FieldName & ".value=" & lclsLed_compan.nLed_compan & ";" & "UpdateDiv('" & FieldName & "Desc','" & lclsLed_compan.sDescript & "','PopUp');" & lstrOnChange & "window.close();"
					LoadLedCompInfo = LoadLedCompInfo & .DoRow
				End With
			Next lclsLed_compan
		End If
		
		'**+ Call the CloseTable property to give as ended the creation of the table (Grid)
		'+ Se llama a la propiedad CloseTable, para dar por finalizada la creación de la tabla (Grid)
		
		LoadLedCompInfo = LoadLedCompInfo & mobjGrid.closeTable & "</DIV><HR><P ALIGN=""RIGHT"">" & lobjValues.ButtonAcceptCancel( ,  , False,  , eFunctions.Values.eButtonsToShow.OnlyCancel) & "</P>"
		
		'UPGRADE_NOTE: Object lobjValues may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lobjValues = Nothing
		'UPGRADE_NOTE: Object lclsLed_compan may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsLed_compan = Nothing
		'UPGRADE_NOTE: Object lcolLed_compans may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lcolLed_compans = Nothing
	End Function
	
	'**%Class_Initialize: Controls the creation of an instance of the collection
	'%Class_Initialize: Controla la creación de una instancia de la colección
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		
		mobjGrid = New eFunctions.Grid
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
	
	'**%Class_Terminate: Controls the destruction of an instance of the collection
	'%Class_Terminate: Controla la destrucción de una instancia de la colección
	'UPGRADE_NOTE: Class_Terminate was upgraded to Class_Terminate_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Terminate_Renamed()
		
		'UPGRADE_NOTE: Object mobjGrid may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		mobjGrid = Nothing
	End Sub
	Protected Overrides Sub Finalize()
		Class_Terminate_Renamed()
		MyBase.Finalize()
	End Sub
End Class






