Option Strict Off
Option Explicit On
Public Class Disco_exprs
	Implements System.Collections.IEnumerable
	'%-------------------------------------------------------%'
	'% $Workfile:: Disco_exprs.cls                          $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 12:35p                               $%'
	'% $Revision:: 9                                        $%'
	'%-------------------------------------------------------%'
	
	'- Variable local para la declaración
	Private mCol As Collection
	
	'% Add: Añade una nueva instancia de la clase "Disco_expr" a la colección
	Public Function Add(ByRef nBranch As Integer, ByRef nProduct As Integer, ByRef nDisexprc As Integer, ByRef dEffecdate As Date, ByRef nBill_item As Integer, ByRef nBranch_rei As Integer, ByRef sChanallo As String, ByRef nBranch_est As Integer, ByRef sCommissi_i As String, ByRef nBranch_led As Integer, ByRef nCurrency As Integer, ByRef sDefaulti As String, ByRef sDescript As String, ByRef sDevoallo As String, ByRef sEdperapl As String, ByRef dNulldate As Date, ByRef nOrder_apl As Integer, ByRef sProrate As String, ByRef sRequire As String, ByRef sRoutine As String, ByRef sShort_des As String, ByRef nAmelevel As Integer, ByRef sDisexpri As String, ByRef sStatregt As String, ByRef nNotenum As Integer, ByRef nDisexpra As Double, ByRef nDisexmin As Double, ByRef nDisexmax As Double, ByRef nDisexAddper As Double, ByRef nDisexSubper As Double) As Disco_expr
		'+Se crea un nuevo objeto
		Dim objNewMember As Disco_expr
		
		objNewMember = New Disco_expr
		With objNewMember
			.nBranch = nBranch
			.nProduct = nProduct
			.nDisexprc = nDisexprc
			.dEffecdate = dEffecdate
			.nBill_item = nBill_item
			.nBranch_rei = nBranch_rei
			.sChanallo = sChanallo
			.nBranch_est = nBranch_est
			.sCommissi_i = sCommissi_i
			.nBranch_led = nBranch_led
			.nCurrency = nCurrency
			.sDefaulti = sDefaulti
			.sDescript = sDescript
			.sDevoallo = sDevoallo
			.sEdperapl = sEdperapl
			.dNulldate = dNulldate
			.nOrder_apl = nOrder_apl
			.sProrate = sProrate
			.sRequire = sRequire
			.sRoutine = sRoutine
			.sShort_des = sShort_des
			.nAmelevel = nAmelevel
			.sDisexpri = sDisexpri
			.sStatregt = sStatregt
			.nNotenum = nNotenum
			.nDisexpra = nDisexpra
			.nDisexmin = nDisexmin
			.nDisexmax = nDisexmax
			.nDisexAddper = nDisexAddper
			.nDisexSubper = nDisexSubper
		End With
		
		mCol.Add(objNewMember, "DE" & nBranch & nProduct & nDisexprc & dEffecdate)
		
		'+Se retorna el objeto creado
		Add = objNewMember
		'UPGRADE_NOTE: Object objNewMember may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		objNewMember = Nothing
	End Function
	
	'% Find: Realiza la búsqueda de los recargos y descuentos del producto
	Public Function Find(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nDisexprc As Integer, ByVal dEffecdate As Date) As Boolean
		Dim lclsDisco_expr As eRemoteDB.Execute
		
		On Error GoTo Find_Err
		
		lclsDisco_expr = New eRemoteDB.Execute
		
		With lclsDisco_expr
			.StoredProcedure = "reaDisco_expr"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDisexprc", nDisexprc, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecDate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run() Then
				'UPGRADE_NOTE: Object mCol may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
				mCol = Nothing
				mCol = New Collection
				Find = True
				Do While Not .EOF
					Call Add(.FieldToClass("nBranch"), .FieldToClass("nProduct"), .FieldToClass("nDisexprc"), .FieldToClass("dEffecdate"), .FieldToClass("nBill_item"), .FieldToClass("nBranch_rei"), .FieldToClass("sChanallo"), .FieldToClass("nBranch_est"), .FieldToClass("sCommissi_i"), .FieldToClass("nBranch_led"), .FieldToClass("nCurrency"), .FieldToClass("sDefaulti"), .FieldToClass("sDescript"), .FieldToClass("sDevoallo"), .FieldToClass("sEdperapl"), .FieldToClass("dNulldate"), .FieldToClass("nOrder_apl"), .FieldToClass("sProrate"), .FieldToClass("sRequire"), .FieldToClass("sRoutine"), .FieldToClass("sShort_des"), .FieldToClass("nAmelevel"), .FieldToClass("sDisexpri"), .FieldToClass("sStatregt"), .FieldToClass("nNotenum"), .FieldToClass("nDisexpra2"), .FieldToClass("nDisexmin2"), .FieldToClass("nDisexmax2"), .FieldToClass("nDisexAddper2"), .FieldToClass("nDisexSubper2"))
					.RNext()
				Loop 
				.RCloseRec()
			End If
		End With
		
Find_Err: 
		If Err.Number Then
			Find = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lclsDisco_expr may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsDisco_expr = Nothing
	End Function
	
	'% MakeDiscNumber: calcula el número del recargo a tratar en la página
	Public Function MakeDiscNumber(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal dEffecdate As Date) As Integer
		Dim lintMaxNumber As Integer
		Dim lclsDisco_expr As Disco_expr
		
		On Error GoTo MakeDiscNumber_Err
		
		lclsDisco_expr = New Disco_expr
		
		lintMaxNumber = 1
		
		If Find(nBranch, nProduct, eRemoteDB.Constants.intNull, dEffecdate) Then
			For	Each lclsDisco_expr In mCol
				If lclsDisco_expr.nDisexprc > lintMaxNumber Then
					lintMaxNumber = lclsDisco_expr.nDisexprc
				End If
			Next lclsDisco_expr
			
			'+ El nuevo número será el número de caso más alto asociado al siniestro + 1
			lintMaxNumber = lintMaxNumber + 1
		End If
		
		MakeDiscNumber = lintMaxNumber
		
MakeDiscNumber_Err: 
		If Err.Number Then
			MakeDiscNumber = 0
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lclsDisco_expr may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsDisco_expr = Nothing
	End Function
	
	'* Item: Devuelve un elemento de la colección (segun índice)
	Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As Disco_expr
		Get
			Item = mCol.Item(vntIndexKey)
		End Get
	End Property
	
	'% Count: Devuelve el número de elementos que posee la colección
	Public ReadOnly Property Count() As Integer
		Get
			Count = mCol.Count()
		End Get
	End Property
	
	'* NewEnum: Permite enumerar la colección para utilizarla en un ciclo For Each... Next
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
	
	'% Remove: Elimina un elemento de la colección
	Public Sub Remove(ByRef vntIndexKey As Object)
		mCol.Remove(vntIndexKey)
	End Sub
	
	'* Class_Initialize: Controla la creación de una instancia de la colección
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		mCol = New Collection
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
	
	'* Class_Terminate: Controla la destrucción de una instancia de la colección
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






