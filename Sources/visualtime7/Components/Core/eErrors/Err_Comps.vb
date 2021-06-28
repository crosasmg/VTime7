Option Strict Off
Option Explicit On
Option Compare Text
Public Class Err_Comps
	Implements System.Collections.IEnumerable
	
	'-Variable local de coleccion
	Private mCol As Collection
	
	
	
	'%Add: Agrega un nuevo registro a la colección
	Public Function Add(ByVal objClass As Err_Comp) As Err_Comp
		If Not IsIDEMode Then
		End If
		
		If objClass Is Nothing Then
			objClass = New Err_Comp
		End If
		
		With objClass
			mCol.Add(objClass, "CP" & .nErrorNum & .nId)
		End With
		
		'+Retorna objeto creado
		Add = objClass
		
		Exit Function
	End Function
	
	'%Item: Retorna item especificado
	Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As Err_Comp
		Get
			If Not IsIDEMode Then
			End If
			
			Item = mCol.Item(vntIndexKey)
			
			Exit Property
		End Get
	End Property
	
	'%Count: Cantidad de registros en coleccion
	Public ReadOnly Property Count() As Integer
		Get
			If Not IsIDEMode Then
			End If
			
			Count = mCol.Count()
			
			Exit Property
		End Get
	End Property
	
	'%NewEnum: Permite recorrer coleccion
	'UPGRADE_NOTE: NewEnum property was commented out. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="B3FC1610-34F3-43F5-86B7-16C984F0E88E"'
	'Public ReadOnly Property NewEnum() As stdole.IUnknown
		'Get
			'If Not IsIDEMode Then
			'End If
			'
			'NewEnum = mCol._NewEnum
			'
			'Exit Property
'ErrorHandler: '
			'ProcError("Err_Comps.NewEnum()")
		'End Get
	'End Property
	
	Public Function GetEnumerator() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
				GetEnumerator = mCol.GetEnumerator
	End Function
	
	'%Remove: Quita un elemento de la coleccion
	Public Sub Remove(ByRef vntIndexKey As Object)
		If Not IsIDEMode Then
		End If
		
		mCol.Remove(vntIndexKey)
		
		Exit Sub
	End Sub
	
	'%Find: Lee los datos de la tabla
	Public Function Find(ByVal nErrorNum As Integer, Optional ByVal lblnFind As Boolean = False) As Boolean
		Dim lrecreaErr_comp_err As eRemoteDB.Execute
		Dim lclsErrComp As Err_Comp
		
		If Not IsIDEMode Then
		End If
		lrecreaErr_comp_err = New eRemoteDB.Execute
		
		'+
		'+ Definición de store procedure reaErr_comp_err al 10-04-2002 13:46:50
		'+
		With lrecreaErr_comp_err
			.bErr_Module = True
			.StoredProcedure = "reaErr_comp_err"
			.Parameters.Add("nErrorNum", nErrorNum, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nId", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run Then
				Find = True
				Do While Not .EOF
					lclsErrComp = New Err_Comp
					lclsErrComp.nSeq = .FieldToClass("nSeq")
					lclsErrComp.nErrorNum = nErrorNum
					lclsErrComp.nId = .FieldToClass("nId")
					lclsErrComp.nCompType = .FieldToClass("nComptype")
					lclsErrComp.sCompName = .FieldToClass("sCompname")
					lclsErrComp.sCompPath = .FieldToClass("sComppath")
					lclsErrComp.nCompVers = .FieldToClass("nCompvers")
					lclsErrComp.dToQC = .FieldToClass("dToQC")
					lclsErrComp.dToQA = .FieldToClass("dToQA")
					lclsErrComp.nUsercode = .FieldToClass("nUsercode")
					Call Add(lclsErrComp)
					lclsErrComp = Nothing
					.RNext()
				Loop 
				.RCloseRec()
			Else
				Find = False
			End If
		End With
		
		lrecreaErr_comp_err = Nothing
		lclsErrComp = Nothing
		
		Exit Function
	End Function
	
	'%Class_Initialize: Carga valores iniciales de clase
	Private Sub Class_Initialize_Renamed()
		If Not IsIDEMode Then
		End If
		
		mCol = New Collection
		
		Exit Sub
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
	
	'%Class_Terminate: Libera objetos creados
	Private Sub Class_Terminate_Renamed()
		If Not IsIDEMode Then
		End If
		
		mCol = Nothing
		
		Exit Sub
	End Sub
	Protected Overrides Sub Finalize()
		Class_Terminate_Renamed()
		MyBase.Finalize()
	End Sub
End Class











