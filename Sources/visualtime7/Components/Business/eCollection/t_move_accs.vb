Option Strict Off
Option Explicit On
Public Class t_Move_Accs
	Implements System.Collections.IEnumerable
	'%-------------------------------------------------------%'
	'% $Workfile:: t_Move_Accs.cls                          $%'
	'% $Author:: Nvaplat40                                  $%'
	'% $Date:: 10/02/04 5:33p                               $%'
	'% $Revision:: 24                                       $%'
	'%-------------------------------------------------------%'
	
	'local variable to hold collection
	Private mCol As Collection
	
	Public nCount As Integer
	Public nPaidAmount As Double
	Public nTotalAmount As Double
	Public nTotalAmountGen As Double
	Public sTable5008 As String
	
	'***Add: Add element of collection
	'*Add: Agrega un elemento a la colección.
	Public Function Add(ByVal objClass As T_Move_Acc) As T_Move_Acc
		
		If objClass Is Nothing Then
            objClass = New T_Move_Acc
		End If
		mCol.Add(objClass)
		Add = objClass
		
	End Function
	
	'***Item: Returns an element of the collection (according to the index)
	'*Item: Devuelve un elemento de la colección (segun índice)
	Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As T_Move_Acc
		Get
			'used when referencing an element in the collection
			'vntIndexKey contains either the Index or Key to the collection,
			'this is why it is declared as a Variant
			'Syntax: Set foo = x.Item(xyz) or Set foo = x.Item(5)
			Item = mCol.Item(vntIndexKey)
		End Get
	End Property
	
	'***Count: Returns the number of elements that the collection has
	'*Count: Devuelve el número de elementos que posee la colección
	Public ReadOnly Property Count() As Integer
		Get
			'used when retrieving the number of elements in the
			'collection. Syntax: Debug.Print x.Count
			Count = mCol.Count()
		End Get
	End Property
	
	'***NewEnum: Enumerates the collection for use in a For Each...Next loop
	'*NewEnum: Permite enumerar la colección para utilizarla en un ciclo For Each... Next
	'UPGRADE_NOTE: NewEnum property was commented out. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="B3FC1610-34F3-43F5-86B7-16C984F0E88E"'
	'Public ReadOnly Property NewEnum() As stdole.IUnknown
		'Get
			'this property allows you to enumerate
			'this collection with the For...Each syntax
			'NewEnum = mCol._NewEnum
		'End Get
	'End Property
	
	Public Function GetEnumerator() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
		'UPGRADE_TODO: Uncomment and change the following line to return the collection enumerator. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="95F9AAD0-1319-4921-95F0-B9D3C4FF7F1C"'
		GetEnumerator = mCol.GetEnumerator
	End Function
	
	'**%findCO012: This routine reads the table of collection movements
	'%findCO012: Se lee de la tabla de movimientos de cobranzas
	Public Function findCO012(ByVal nAction As ColformRef.TypeActionsSeqColl, ByVal sWinType As String, ByVal nBordereaux As Double, ByVal sStatus As String, ByVal dCollect As Date, ByVal sRel_Type As String, ByVal nCod_Agree As Integer, ByVal dValueDate As Date, ByVal sRelOrigi As String) As Boolean
		'**-Variable definition. lrecCO012t_Move_Acc. It will be used as a cursor
		'-Se define la variable lrecCO012t_Move_Acc que se utilizará como cursor.
		Dim lclsT_Move_Acc As T_Move_Acc
		Dim lrecinsReaCO013Move_Acc As eRemoteDB.Execute
		Dim llngIndex As Integer
		
		On Error GoTo findCO012_Err
		
		lrecinsReaCO013Move_Acc = New eRemoteDB.Execute
		
		With lrecinsReaCO013Move_Acc
			'+ Si es consulta y la relación está completa se muestra la información de las tablas fijas
			If nAction = ColformRef.TypeActionsSeqColl.cstrQuery And sStatus = "1" Then
				.StoredProcedure = "insReaCO012Move_AccF"
			Else
				.StoredProcedure = "insReaCO012Move_AccT"
			End If
			
			.Parameters.Add("sWinType", IIf(sWinType = String.Empty, "Normal", sWinType), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 10, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBordereaux", nBordereaux, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dCollect", dCollect, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dValuedate", dValueDate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sRel_Type", sRel_Type, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sRelOrigi", sRelOrigi, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCod_agree", nCod_Agree, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run Then
				findCO012 = True
				Do While Not .EOF
					'+ Tratamiento especial del primer registro (Información general de la página)
					If .FieldToClass("sClient") = String.Empty Then
						nPaidAmount = .FieldToClass("nPaidAmount", 0)
						nTotalAmount = .FieldToClass("nTotalAmount", 0)
						nTotalAmountGen = .FieldToClass("nTotalGen", 0)
						sTable5008 = .FieldToClass("sTable5008")
					Else
						llngIndex = llngIndex + 1
						lclsT_Move_Acc = New T_Move_Acc
						lclsT_Move_Acc.sClient = .FieldToClass("sClient")
						lclsT_Move_Acc.sCliename = .FieldToClass("sCliename")
						lclsT_Move_Acc.sDigit = .FieldToClass("sDigit")
						lclsT_Move_Acc.nCredit = .FieldToClass("nCredit")
						lclsT_Move_Acc.nCurrency = .FieldToClass("nCurrency")
						lclsT_Move_Acc.sCurrency = .FieldToClass("sCurrency")
						lclsT_Move_Acc.nExchange = .FieldToClass("nExchange")
						lclsT_Move_Acc.nSequence = .FieldToClass("nSequence")
						lclsT_Move_Acc.nType_Move = .FieldToClass("nType_Move")
						
						Call Add(lclsT_Move_Acc)
						'UPGRADE_NOTE: Object lclsT_Move_Acc may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
						lclsT_Move_Acc = Nothing
					End If
					.RNext()
				Loop 
			End If
		End With
		
		nCount = llngIndex
		
findCO012_Err: 
		If Err.Number Then
			findCO012 = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecinsReaCO013Move_Acc may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecinsReaCO013Move_Acc = Nothing
		'UPGRADE_NOTE: Object lclsT_Move_Acc may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsT_Move_Acc = Nothing
	End Function
	
	'**%Remove: Deletes an element from the collection
	'%Remove: Elimina un elemento de la colección
	Public Sub Remove(ByRef vntIndexKey As Object)
		'used when removing an element from the collection
		'vntIndexKey contains either the Index or Key, which is why
		'it is declared as a Variant
		'Syntax: x.Remove(xyz)
		
		
		mCol.Remove(vntIndexKey)
	End Sub
	
	'**%Class_Initialize: Controls the creation of an instance of the collection
	'%Class_Initialize: Controla la creación de una instancia de la colección
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		'creates the collection when this class is created
		mCol = New Collection
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
	
	'**%Class_Terminate: Controls the destruction of an instance of the collection
	'%Class_Terminate: Controla la destrucción de una instancia de la colección
	'UPGRADE_NOTE: Class_Terminate was upgraded to Class_Terminate_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Terminate_Renamed()
		'destroys collection when this class is terminated
		'UPGRADE_NOTE: Object mCol may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		mCol = Nothing
	End Sub
	Protected Overrides Sub Finalize()
		Class_Terminate_Renamed()
		MyBase.Finalize()
	End Sub
	
	
	'% ReaPremiumOptions: Ejecuta del Stored Procedure "reaOpt_premiu" y
	'%                    devuelve el límite máximo establecido en las opciones de instalación
	'%                    del módulo de cobranzas - ACM - 30/08/2001
	
	'**% ReaPremiumOptions: Executes the Stored Procedure named "reaOpt_premiu" and returns
	'**%                    the maximum limit set on the install options of the Collection module - ACM - Aug-30-2001
	Public Function ReaPremiumOptions() As Double
		Dim lrecreaOpt_premiu As New eRemoteDB.Execute
		
		On Error GoTo ReaPremiumOptions_err
		
		With lrecreaOpt_premiu
			.StoredProcedure = "reaOpt_premiu"
			If .Run Then
				ReaPremiumOptions = .FieldToClass("nUpper_lim")
			Else
				ReaPremiumOptions = eRemoteDB.Constants.intNull
			End If
		End With
		'+ Manejo de errores
		'**+ Error handle
ReaPremiumOptions_err: 
		If Err.Number Then
			ReaPremiumOptions = eRemoteDB.Constants.intNull
		End If
		
		'UPGRADE_NOTE: Object lrecreaOpt_premiu may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaOpt_premiu = Nothing
		
	End Function
End Class






