Option Strict Off
Option Explicit On
Public Class Led_compans
	Implements System.Collections.IEnumerable
	'%-------------------------------------------------------%'
	'% $Workfile:: Led_compans.cls                          $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 1:18p                                $%'
	'% $Revision:: 4                                        $%'
	'%-------------------------------------------------------%'
	
	'**+Collection importes from the Win32 version on 05/24/2001.
	'+ Colección importada de la versión Win32 el día 24/05/2001.
	
	Private mCol As Collection
	
	'**% Add: add a new instance to the Led_compan class to the collection
	'% Add: Añade una nueva instancia de la clase Led_compan a la colección
	Public Function Add(ByVal nStatusInstance As Led_compan.eStatusInstance, ByVal dCompan_dat As Date, ByVal dDate_end As Date, ByVal dDate_init As Date, ByVal nCurrency As Integer, ByVal nLed_compan As Integer, ByVal nVoucher As Integer, ByVal sAccount_gp As String, ByVal sAccount_bg As String, ByVal sBal_actu As String, ByVal sClose_mont As String, ByVal sStatregt As String, ByVal sStruct_uni As String, ByVal sStructure As String, ByVal nYear As Integer, ByVal dIniLedDat As Date, ByVal dEndLedDat As Date, Optional ByVal sDescript As String = "") As Led_compan
		
		Dim objNewMember As Led_compan
		objNewMember = New Led_compan
		
		With objNewMember
			.nStatusInstance = nStatusInstance
			.nLed_compan = nLed_compan
			.sAccount_bg = sAccount_bg
			.sAccount_gp = sAccount_gp
			.sBal_actu = sBal_actu
			.sClose_mont = sClose_mont
			.sStatregt = sStatregt
			.sStruct_uni = sStruct_uni
			.sStructure = sStructure
			.dCompan_dat = dCompan_dat
			.dDate_end = dDate_end
			.dDate_init = dDate_init
			.nVoucher = nVoucher
			.nCurrency = nCurrency
			.nYear = nYear
			.dIniLedDat = dIniLedDat
			.dEndLedDat = dEndLedDat
			.sDescript = sDescript
		End With
		
		'set the properties passed into the method
		
		mCol.Add(objNewMember, "LC" & nLed_compan)
		
		'return the object created
		Add = objNewMember
		'UPGRADE_NOTE: Object objNewMember may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		objNewMember = Nothing
		
	End Function
	
	'**%Find: Returns TRUE or FALSE if the records exists in the table "XXXXXX"
	'%Find: Este metodo retorna VERDADERO o FALSO dependiendo de la existencia o no de registros en la
	'%tabla "XXXXXX"
	Public Function Find(Optional ByVal lblnFind As Boolean = False) As Boolean
		
		Dim lrecreaLed_compan_All As eRemoteDB.Execute
		
		On Error GoTo Find_Err
		
		lrecreaLed_compan_All = New eRemoteDB.Execute
		
		'**+Condition that will permit to control the execution of the method in case any info exist in the collection
		'+ Condición que permitira controlar la ejecución del metodo en caso de que exista información
		'+ en la colección
		
		If mCol.Count() > 0 And Not lblnFind Then
			Find = True
		Else
			
			'**+Parameters definition for the stored procedure 'insudb.reaLed_compan_All'
			'**+Data read on 06/05/2000 09:27:16 p.m.
			'+ Definición de parámetros para stored procedure 'insudb.reaLed_compan_All'
			'+ Información leída el 05/09/2000 09:27:16 p.m.
			
			With lrecreaLed_compan_All
				.StoredProcedure = "reaLed_compan_All"
				If .Run Then
					Do While Not .EOF
						Call Add(Led_compan.eStatusInstance.eftQuery, .FieldToClass("dCompan_dat"), .FieldToClass("dDate_end"), .FieldToClass("dDate_init"), .FieldToClass("nCurrency"), .FieldToClass("nLed_compan"), .FieldToClass("nVoucher"), .FieldToClass("sAccount_gp"), .FieldToClass("sAccount_bg"), .FieldToClass("sBal_actu"), .FieldToClass("sClose_mont"), .FieldToClass("sStatregt"), .FieldToClass("sStruct_uni"), .FieldToClass("sStructure"), .FieldToClass("nYear"), .FieldToClass("dIniLedDat"), .FieldToClass("dEndLedDat"), .FieldToClass("sDescript"))
						.RNext()
					Loop 
					.RCloseRec()
					Find = True
				Else
					Find = False
				End If
			End With
		End If
		'UPGRADE_NOTE: Object lrecreaLed_compan_All may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaLed_compan_All = Nothing
		
Find_Err: 
		If Err.Number Then
			Find = False
		End If
		On Error GoTo 0
	End Function
	
	'*** Item: return an element of the collection (accourding to the index)
	'* Item: Devuelve un elemento de la colección (segun índice)
	Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As Led_compan
		Get
			
			Item = mCol.Item(vntIndexKey)
		End Get
	End Property
	
	'*** Count: return the elements number that the collection has
	'* Count: Devuelve el número de elementos que posee la colección
	Public ReadOnly Property Count() As Integer
		Get
			
			Count = mCol.Count()
		End Get
	End Property
	
	'*** NewEnum: Permit the enumerate the collection to used it in one cicle For Each...Next
	'* NewEnum: Permite enumerar la colección para utilizarla en un ciclo For Each... Next
	'UPGRADE_NOTE: NewEnum property was commented out. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="B3FC1610-34F3-43F5-86B7-16C984F0E88E"'
	'Public ReadOnly Property NewEnum() As stdole.IUnknown
		'Get
			'
			'NewEnum = mCol._NewEnum
		'End Get
	'End Property
	
	Public Function GetEnumerator() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
		'UPGRADE_TODO: Uncomment and change the following line to return the collection enumerator. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="95F9AAD0-1319-4921-95F0-B9D3C4FF7F1C"'
		GetEnumerator = mCol.GetEnumerator
	End Function
	
	'**% Remove: delete one element of the collection
	'% Remove: Elimina un elemento de la colección
	'---------------------------------------------
	Public Sub Remove(ByRef vntIndexKey As Object)
		'---------------------------------------------
		
		mCol.Remove(vntIndexKey)
	End Sub
	
	'**% Class_Initialize: Control the creation of a collection instance
	'% Class_Initialize: Controla la creación de una instancia de la colección
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		
		mCol = New Collection
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
	
	'**% Class_Terminate: Control the destruction of a collection instance
	'% Class_Terminate: Controla la destrucción de una instancia de la colección
	'UPGRADE_NOTE: Class_Terminate was upgraded to Class_Terminate_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Terminate_Renamed()
		
		'UPGRADE_NOTE: Object mCol may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		mCol = Nothing
	End Sub
	Protected Overrides Sub Finalize()
		Class_Terminate_Renamed()
		MyBase.Finalize()
	End Sub
End Class






