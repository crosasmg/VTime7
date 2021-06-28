Option Strict Off
Option Explicit On
Public Class Null_condis
	Implements System.Collections.IEnumerable
	'- Variables locales para la colección
	Private mCol As Collection
	
	'% Add: Añade una nueva instancia de la clase Null_condi a la colección
	Public Function Add(ByRef nBranch As Integer, ByRef nProduct As Integer, ByRef nNullcode As Integer, ByRef dEffecdate As Date, ByRef nAmelevel As Integer, ByRef dCompdate As Date, ByRef sRegtypen As String, ByRef sReturn_ind As String, ByRef nReturn_rat As Double, ByRef sStatregt As String, ByRef nUsercode As Integer, ByRef dNulldate As Date, ByRef sNotrehab As String, ByRef sReaAuto As String, ByVal sRoutine_Pay As String, ByVal nRetraction As Integer) As Null_condi
		'+ Se crea un nuevo objeto
		Dim objNewMember As Null_condi
		
		'+ Se setean las propiedades
		objNewMember = New Null_condi
		With objNewMember
			.nBranch = nBranch
			.nProduct = nProduct
			.nNullcode = nNullcode
			.dEffecdate = dEffecdate
			.nAmelevel = nAmelevel
			.dCompdate = dCompdate
			.sRegtypen = sRegtypen
			.sReturn_ind = sReturn_ind
			.nReturn_rat = nReturn_rat
			.sStatregt = sStatregt
			.nUsercode = nUsercode
			.dNulldate = dNulldate
			.sNotrehab = sNotrehab
			.sReaAuto = sReaAuto
            .sRoutine_Pay = sRoutine_Pay
            .nRetraction = nRetraction 

		End With
		
		mCol.Add(objNewMember)
		
		'+ Se retorna el objeto creado
		Add = objNewMember
		'UPGRADE_NOTE: Object objNewMember may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		objNewMember = Nothing
	End Function
	
	'% Find: Devuelve la información de los campos requeridos en la emisión
	Public Function Find(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal dEffecdate As Date, ByVal sRegtypen As String) As Boolean
		
		Dim lrecreaNull_CondiDP061 As eRemoteDB.Execute
		Dim lclsNull_condi As Null_condi
		
		On Error GoTo Find_Err
		
		lrecreaNull_CondiDP061 = New eRemoteDB.Execute
		lclsNull_condi = New Null_condi
		
		'+ Definición de parámetros para stored procedure 'insudb.reaNull_CondiDP061'
		'+ Información leída el 18/04/2001 03:43:15 p.m.
		Find = False
		
		With lrecreaNull_CondiDP061
			.StoredProcedure = "reaNull_CondiDP061"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sRegtypen", sRegtypen, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				Find = True
				Do While Not .EOF
					lclsNull_condi = Add(nBranch, nProduct, .FieldToClass("nNullcode"), eRemoteDB.Constants.dtmNull, .FieldToClass("nAmelevel"), eRemoteDB.Constants.dtmNull, .FieldToClass("sRegtypen"), .FieldToClass("sReturn_ind"), .FieldToClass("nReturn_rat"), .FieldToClass("sStatregt"), eRemoteDB.Constants.intNull, .FieldToClass("dNulldate"), .FieldToClass("sNotRehab"), .FieldToClass("sReaAuto"), .FieldToClass("sRoutine_Pay"), .FieldToClass("nRetraction"))
					.RNext()
				Loop 
				.RCloseRec()
			End If
		End With
		
		'UPGRADE_NOTE: Object lrecreaNull_CondiDP061 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaNull_CondiDP061 = Nothing
		'UPGRADE_NOTE: Object lclsNull_condi may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsNull_condi = Nothing
		
Find_Err: 
		If Err.Number Then
			Find = False
		End If
		
		On Error GoTo 0
		
	End Function
	
	'%Item: Devuelve un elemento de la colección (segun índice)
	Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As Null_condi
		Get
			Item = mCol.Item(vntIndexKey)
			
		End Get
	End Property
	
	'%Count: Devuelve el número de elementos que posee la colección
	Public ReadOnly Property Count() As Integer
		Get
			Count = mCol.Count()
			
		End Get
	End Property
	
	'%NewEnum: Permite enumerar la colección para utilizarla en un ciclo For Each... Next
	'UPGRADE_NOTE: NewEnum property was commented out. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="B3FC1610-34F3-43F5-86B7-16C984F0E88E"'
	'Public ReadOnly Property NewEnum() As stdole.IUnknown
		'Get
			'NewEnum = mCol._NewEnum
			'
		'End Get
	'End Property
	
	Public Function GetEnumerator() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
		'UPGRADE_TODO: Uncomment and change the following line to return the collection enumerator. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="95F9AAD0-1319-4921-95F0-B9D3C4FF7F1C"'
		GetEnumerator = mCol.GetEnumerator
	End Function
	
	'%Remove: Elimina un elemento de la colección
	Public Sub Remove(ByRef vntIndexKey As Object)
		mCol.Remove(vntIndexKey)
		
	End Sub
	
	'%Class_Initialize: Controla la creación de una instancia de la colección
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		mCol = New Collection
		
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
	
	'%Class_Terminate: Controla la destrucción de una instancia de la colección
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






