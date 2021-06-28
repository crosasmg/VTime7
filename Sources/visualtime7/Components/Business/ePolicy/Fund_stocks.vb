Option Strict Off
Option Explicit On
Public Class Fund_stocks
	Implements System.Collections.IEnumerable
	'**+Objective: Collection that supports the class: Fund_stock
	'**+Version: $$Revision: $
	'+Objetivo: Colección que le da soporte a la clase: Fund_stock
	'+Version: $$Revision: $
	'%-------------------------------------------------------%'
	'% $Workfile::                                          $%'
	'% $Author::                                            $%'
	'% $Date::                                              $%'
	'% $Revision::                                          $%'
	'%-------------------------------------------------------%'
	
	'**-Objective:
	'-Objetivo:
	Private lintBranch As Integer
	
	'**-Objective:
	'-Objetivo:
	Private lintProduct As Integer
	
	'**-Objective: Date which from the record is valid.
	'-Objetivo: Fecha de efecto del registro.
	Private ldtmEffecdate As Date
	
	'**-Objective: Code of the investment fund
	'-Objetivo: Código del fondo de inversión
	Private lintFunds As Integer
	
	'**-Objective:
	'-Objetivo:
	Private nStatInstanc As Fund_inv.eStatusInstance_f
	
	'**-Objective:
	'-Objetivo:
	Private mCol As Collection
	
	'**%Objective: Adds the fields to the collection of nominal values
	'**%Parameters:
	'**%  nStatInstanc -
	'**%  nValue       -
	'**%  nGanancy     -
	'**%  nBuyCost     -
	'**%  nSellCost    -
	'**%  nProduct     -
	'**%  nBranch      -
	'**%  nUnits       - Number of investment  units
	'**%  dEffecdate   - Date which from the record is valid.
	'**%  nNum_mov     - Number identifying the stock movement
	'**%  nMove_type   - Type of movement (Purchase/Sale)  Sole values as per table 415
	'**%  nFunds       - Code of the investment fund
	'**%  sMove_type   - Type of movement (Purchase/Sale)  Sole values as per table 415
	'**%  sFunds       - Code of the investment fund
	'%Objetivo: Agrega los campos a la colección de valores nominales
	'%Parámetros:
	'%    nStatInstanc -
	'%    nValue       -
	'%    nGanancy     -
	'%    nBuyCost     -
	'%    nSellCost    -
	'%    nProduct     -
	'%    nBranch      -
	'%    nUnits       - Cantidad de unidades
	'%    dEffecdate   - Fecha de efecto del registro.
	'%    nNum_mov     - Número que identifica el movimiento
	'%    nMove_type   - Tipo de movimiento de compra/venta.   Valores únicos según tabla 415.
	'%    nFunds       - Código del fondo de inversión
	'%    sMove_type   - Tipo de movimiento de compra/venta.   Valores únicos según tabla 415.
	'%    sFunds       - Código del fondo de inversión
    Public Function Add(ByVal nStatInstanc As Fund_inv.eStatusInstance_f, ByVal nValue As Double, ByVal nGanancy As Double, ByVal nBuyCost As Double, ByVal nSellCost As Double, ByVal nProduct As Integer, ByVal nBranch As Integer, ByVal nUnits As Double, ByVal dEffecdate As Date, ByVal nNum_mov As Integer, ByVal nMove_type As Integer, ByVal nFunds As Integer, Optional ByVal sMove_type As String = "", Optional ByVal sFunds As String = "") As Fund_stock
        Dim objNewMember As ePolicy.Fund_stock

        On Error GoTo ErrorHandler
        objNewMember = New ePolicy.Fund_stock

        If mCol Is Nothing Then
            mCol = New Collection
        End If

        With objNewMember
            .nStatInstanc = nStatInstanc
            .nValue = nValue
            .nGanancy = nGanancy
            .nBuyCost = nBuyCost
            .nSellCost = nSellCost
            .nProduct = nProduct
            .nBranch = nBranch
            .nUnits = nUnits
            .dEffecdate = dEffecdate
            .nNum_mov = nNum_mov
            .nMove_type = nMove_type
            .nFunds = nFunds
            .sMove_type = sMove_type
            .sFunds = sFunds
        End With

        mCol.Add(objNewMember, "FUND_STK" & nFunds & nMove_type & nNum_mov)

        '**- Returns the created object
        '- Retorna el objeto creado

        Add = objNewMember

        'UPGRADE_NOTE: Object objNewMember may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        objNewMember = Nothing

        Exit Function
ErrorHandler:
        'UPGRADE_NOTE: Object objNewMember may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        objNewMember = Nothing
        'UPGRADE_NOTE: Object mCol may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        mCol = Nothing
        'UPGRADE_NOTE: Object Add may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        Add = Nothing
    End Function
	
	'**%Objective: Permits reading all the cash flows of the stocks in a specific fund.
	'**%Parameters:
	'**%  nBranch1   -
	'**%  nProduct1  -
	'**%  dOperDate1 -
	'**%  nFunds1    -
	'%Objetivo: Permite leer todos los movimientos de stock de un fondo especifico.
	'%Parámetros:
	'%    nBranch1   -
	'%    nProduct1  -
	'%    dOperDate1 -
	'%    nFunds1    -
	Public Function Find_All_SpecificFund(ByVal nBranch1 As Integer, ByVal nProduct1 As Integer, ByVal dOperDate1 As Date, ByVal nFunds1 As Integer) As Boolean
		Dim lintPos As Integer
		Dim lrecreaFund_stocks As eRemoteDB.Execute
		
		On Error GoTo ErrorHandler
		Find_All_SpecificFund = False
		
		lrecreaFund_stocks = New eRemoteDB.Execute
		
		With lrecreaFund_stocks
			.StoredProcedure = "reaFund_stocks"
			
			.Parameters.Add("nBranch", nBranch1, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct1, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dOperdate", dOperDate1, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nFunds", nFunds1, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run Then
				Find_All_SpecificFund = True
				
				Do While Not .EOF
					Call Add(nStatInstanc = Fund_inv.eStatusInstance_f.eftExist_f, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, .FieldToClass("nBuy_Cost"), .FieldToClass("nSell_Cost"), .FieldToClass("nProduct"), .FieldToClass("nBranch"), .FieldToClass("nUnits"), .FieldToClass("dEffecDate"), .FieldToClass("nNum_mov"), .FieldToClass("nMove_type"), .FieldToClass("nFunds"))
					.RNext()
				Loop 
				
				.RCloseRec()
			End If
		End With
		
		'UPGRADE_NOTE: Object lrecreaFund_stocks may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaFund_stocks = Nothing
		
		Exit Function
ErrorHandler: 
		'UPGRADE_NOTE: Object lrecreaFund_stocks may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaFund_stocks = Nothing
		Find_All_SpecificFund = False
	End Function
	
	'**%Objective: Reads the transactions of a fund stock for a given date
	'**%Parameters:
	'**%  nFund     -
	'**%  dOperDate -
	'%Objetivo: Permite leer los movimientos de el stock del fondo a una fecha dada
	'%Parámetros:
	'%    nFund     -
	'%    dOperDate -
	Public Function Find_UnitsAvailable(ByVal nFund As Integer, ByVal dOperDate As Date) As Boolean
		Dim lrecreaFund_stock As eRemoteDB.Execute
		
		On Error GoTo ErrorHandler
		lrecreaFund_stock = New eRemoteDB.Execute
		
		Find_UnitsAvailable = False
		
		With lrecreaFund_stock
			.StoredProcedure = "reaFund_stock"
			
			.Parameters.Add("nFund", nFund, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dOperdate", dOperDate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run Then
				Find_UnitsAvailable = True
				
				Do While Not .EOF
					Call Add(nStatInstanc = Fund_inv.eStatusInstance_f.eftExist_f, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, .FieldToClass("nUnits"), eRemoteDB.Constants.dtmNull, .FieldToClass("nNum_mov"), .FieldToClass("nMove_type"), nFund)
					.RNext()
				Loop 
				.RCloseRec()
			End If
		End With
		
		'UPGRADE_NOTE: Object lrecreaFund_stock may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaFund_stock = Nothing
		
		Exit Function
ErrorHandler: 
		'UPGRADE_NOTE: Object lrecreaFund_stock may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaFund_stock = Nothing
		Find_UnitsAvailable = False
	End Function
	
	'**%Objective: Reads the transactions of a fund stock for a given date
	'**%Parameters:
	'**%  nFund     -
	'**%  dOperDate -
	'%Objetivo: Permite leer los movimientos de el stock del fondo a una fecha dada
	'%Parámetros:
	'%    nFund     -
	'%    dOperDate -
	Public Function Find_AllUnits(ByVal nFund As Integer, ByVal dOperDate As Date) As Boolean
		
		Dim lrecreaFund_stock As eRemoteDB.Execute
		
		On Error GoTo ErrorHandler
		lrecreaFund_stock = New eRemoteDB.Execute
		
		Find_AllUnits = False
		With lrecreaFund_stock
			.StoredProcedure = "reaFund_stock_1"
			
			.Parameters.Add("nFund", nFund, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dOperdate", dOperDate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run Then
				Find_AllUnits = True
				
				Do While Not .EOF
					Call Add(nStatInstanc = Fund_inv.eStatusInstance_f.eftExist_f, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, .FieldToClass("nUnits"), .FieldToClass("dEffecdate"), .FieldToClass("nNum_mov"), .FieldToClass("nMove_type"), nFund, .FieldToClass("sMove_type"))
					.RNext()
				Loop 
				.RCloseRec()
			End If
		End With
		
		'UPGRADE_NOTE: Object lrecreaFund_stock may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaFund_stock = Nothing
		
		Exit Function
ErrorHandler: 
		'UPGRADE_NOTE: Object lrecreaFund_stock may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaFund_stock = Nothing
		Find_AllUnits = False
	End Function
	
	'**%Objective: Reads the transactions of a fund stock for a given date
	'**%Parameters:
	'**%  dOperDate -
	'**%  nFund     -
	'%Objetivo: Permite leer los movimientos de el stock del fondo a una fecha dada
	'%Parámetros:
	'%    dOperDate -
	'%    nFund     -
	Public Function Find_AllTrans(ByVal dOperDate As Date, Optional ByVal nFund As Integer = eRemoteDB.Constants.intNull) As Boolean
		
		Dim lrecreaFund_stock As eRemoteDB.Execute
		
		On Error GoTo ErrorHandler
		lrecreaFund_stock = New eRemoteDB.Execute
		
		Find_AllTrans = False
		
		With lrecreaFund_stock
			.StoredProcedure = "reaFund_stock_2"
			
			.Parameters.Add("dOperdate", dOperDate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nFund", nFund, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run Then
				Find_AllTrans = True
				
				Do While Not .EOF
					Call Add(nStatInstanc = Fund_inv.eStatusInstance_f.eftExist_f, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, .FieldToClass("nUnits"), eRemoteDB.Constants.dtmNull, .FieldToClass("nNum_mov"), .FieldToClass("nMove_type"), .FieldToClass("nFunds"), String.Empty, .FieldToClass("sFunds"))
					.RNext()
				Loop 
				.RCloseRec()
			End If
		End With
		
		'UPGRADE_NOTE: Object lrecreaFund_stock may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaFund_stock = Nothing
		
		Exit Function
ErrorHandler: 
		'UPGRADE_NOTE: Object lrecreaFund_stock may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaFund_stock = Nothing
		Find_AllTrans = False
	End Function
	
	'**%Objective: Use when making reference to an element of the collection
	'**%           vntIndexKey contains the index or the password of the collection,
	'**%Parameters:
	'**%  vntIndexKey -
	'%Objetivo: Se usa al hacer referencia a un elemento de la colección
	'%          vntIndexKey contiene el índice o la clave de la colección,
	'%Parámetros:
	'%    vntIndexKey -
	Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As Fund_stock
		Get
			On Error GoTo ErrorHandler
			Item = mCol.Item(vntIndexKey)
			
			Exit Property
ErrorHandler: 
			'UPGRADE_NOTE: Object Item may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
			Item = Nothing
		End Get
	End Property
	
	'**%Objective: Returns the number of elements that the collection has
	'%Objetivo: Devuelve el número de elementos que posee la colección
	Public ReadOnly Property Count() As Integer
		Get
			On Error GoTo ErrorHandler
			Count = mCol.Count()
			
			Exit Property
ErrorHandler: 
			Count = 0
		End Get
	End Property
	
	'**%Objective: Enumerates the collection for use in a For Each...Next loop
	'%Objetivo: Permite enumerar la colección para utilizarla en un ciclo For Each... Next
	'UPGRADE_NOTE: NewEnum property was commented out. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="B3FC1610-34F3-43F5-86B7-16C984F0E88E"'
	'Public ReadOnly Property NewEnum() As stdole.IUnknown
		'Get
			'On Error GoTo ErrorHandler
			'NewEnum = mCol._NewEnum
			'
			'Exit Property
'ErrorHandler: '
			''UPGRADE_NOTE: Object NewEnum may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
			'NewEnum = Nothing
		'End Get
	'End Property
	
	Public Function GetEnumerator() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
		'UPGRADE_TODO: Uncomment and change the following line to return the collection enumerator. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="95F9AAD0-1319-4921-95F0-B9D3C4FF7F1C"'
		GetEnumerator = mCol.GetEnumerator
	End Function
	
	'**%Objective: Deletes an element from the collection
	'**%Parameters:
	'**%  vntIndexKey -
	'%Objetivo: Elimina un elemento de la colección
	'%Parámetros:
	'%    vntIndexKey -
	Public Sub Remove(ByRef vntIndexKey As Object)
		On Error GoTo ErrorHandler
		mCol.Remove(vntIndexKey)
		
		Exit Sub
ErrorHandler: 
		
	End Sub
	
	'**%Objective: Controls the creation of an instance of the collection
	'%Objetivo: Controla la creación de una instancia de la colección
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		On Error GoTo ErrorHandler
		mCol = New Collection
		
		Exit Sub
ErrorHandler: 
		'UPGRADE_NOTE: Object mCol may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		mCol = Nothing
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
	
	'**%Objective: Controls the destruction of an instance of the collection
	'%Objetivo: Controla la destrucción de una instancia de la colección
	'UPGRADE_NOTE: Class_Terminate was upgraded to Class_Terminate_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Terminate_Renamed()
		On Error GoTo ErrorHandler
		
		Exit Sub
ErrorHandler: 
		
	End Sub
	Protected Overrides Sub Finalize()
		Class_Terminate_Renamed()
		MyBase.Finalize()
	End Sub
End Class






