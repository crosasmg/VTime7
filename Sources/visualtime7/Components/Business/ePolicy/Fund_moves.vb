Option Strict Off
Option Explicit On
Public Class Fund_moves
	Implements System.Collections.IEnumerable
	'%-------------------------------------------------------%'
	'% $Workfile:: Fund_moves.cls                           $%'
	'% $Author:: Nvaplat41                                  $%'
	'% $Date:: 9/10/03 19.01                                $%'
	'% $Revision:: 11                                       $%'
	'%-------------------------------------------------------%'
	
	'**- local variable to hold collection
	'- Variable local para contener colección
	
	Private mCol As Collection
	
	'**% Add: Adds the object to the collection
	'% Add: Agrega el objeto a la colección
    Public Function Add(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal nFunds As Integer, ByVal nAmount As Double, ByVal nCurrency As Integer, ByVal nType_Move As Integer, ByVal nUnits As Double, ByVal TotInver As Double, ByVal nRem_number As Integer, ByVal dOperDate As Date, Optional ByVal sBranch As String = "", Optional ByVal sProduct As String = "", Optional ByVal sEntry As String = "", Optional ByVal sClient As String = "", Optional ByVal sCliename As String = "", Optional ByVal nUnit_Balance As Double = 0, Optional ByVal nInstitution As Integer = 0, Optional ByVal sInstitution As String = "", Optional ByVal nOrigin As Integer = 0, Optional ByVal sOrigin As String = "", Optional ByVal dDate_Origin As Date = eRemoteDB.Constants.dtmNull) As Fund_move
        Dim objNewMember As Fund_move

        objNewMember = New Fund_move

        With objNewMember
            .sCertype = "2"
            .nRem_number = nRem_number
            .nBranch = nBranch
            .nProduct = nProduct
            .nPolicy = nPolicy
            .nCurrency = nCurrency
            .nCertif = nCertif
            .nFunds = nFunds
            .nType_Move = nType_Move
            .nUnits = nUnits
            .nAmount = nAmount
            .TotValue = TotInver
            .dOperDate = dOperDate
            .sBranch = sBranch
            .sProduct = sProduct
            .sEntry = sEntry
            .sClient = sClient
            .sCliename = sCliename
            .nUnit_Balance = nUnit_Balance
            .nInstitution = nInstitution
            .sInstitution = sInstitution
            .nOrigin = nOrigin
            .sOrigin = sOrigin
            .dDate_Origin = dDate_Origin
        End With

        mCol.Add(objNewMember, nBranch & nProduct & nPolicy & nCertif & nFunds & nOrigin & nRem_number & dOperDate)

        Add = objNewMember
        'UPGRADE_NOTE: Object objNewMember may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        objNewMember = Nothing
    End Function
	
	'**% Find: This routine searches for the cash flows of a fund
	'% Find: Esta función se encarga de buscar todos los Movimientos de Un Fondo
    Public Function Find(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dOperDate As Date, ByVal nFunds As Integer) As Boolean
        On Error GoTo Find_Err

        Find = False

        Dim lrecreaFund_Units As eRemoteDB.Execute

        lrecreaFund_Units = New eRemoteDB.Execute

        With lrecreaFund_Units
            .StoredProcedure = "reaFund_Units"

            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dOperdate", dOperDate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nFunds", nFunds, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            If .Run Then
                Do While Not .EOF
                    Call Add(.FieldToClass("nBranch"), .FieldToClass("nProduct"), .FieldToClass("nPolicy"), .FieldToClass("nCertif"), .FieldToClass("nFunds"), .FieldToClass("nAmount"), .FieldToClass("nCurrency"), .FieldToClass("nType_move"), .FieldToClass("nUnits"), .FieldToClass("TotInver"), .FieldToClass("nRem_number"), .FieldToClass("dOperDate"))
                    .RNext()
                Loop

                .RCloseRec()
                Find = True
            End If
        End With

        'UPGRADE_NOTE: Object lrecreaFund_Units may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecreaFund_Units = Nothing

Find_Err:
        If Err.Number Then
            Find = False
        End If
    End Function
	
	'**% Find_units: This routine obtains the activity of an investment fund
	'% Find_units: Esta función se encarga de buscar la actividades de un fondo
	Public Function Find_units(ByVal nFunds As Integer, ByVal dOperDate As Date) As Boolean
		
		Dim lrecreaFund_Units As eRemoteDB.Execute
		
		lrecreaFund_Units = New eRemoteDB.Execute
		
		On Error GoTo Find_Units_Err
		
		Find_units = False
		
		'**+Stored procedure parameters definition 'insudb.reaFund_Units'
		'**+Data of 08/16/2002 05:17:48 PM
		'+ Definición de parámetros para stored procedure 'insudb.reaFund_Units'
		'+ Información leída el 08/16/2002 05:17:48 PM
		
		With lrecreaFund_Units
			.StoredProcedure = "reaMove_fund"
			
			.Parameters.Add("nFunds", nFunds, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dOperdate", dOperDate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run Then
				Do While Not .EOF
					Call Add(.FieldToClass("nBranch"), .FieldToClass("nProduct"), .FieldToClass("nPolicy"), .FieldToClass("nCertif"), .FieldToClass("nFunds"), eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, .FieldToClass("nType_move"), .FieldToClass("nUnits"), eRemoteDB.Constants.intNull, .FieldToClass("nRem_number"), .FieldToClass("dOperDate"), .FieldToClass("sBranch"), .FieldToClass("sProduct"), .FieldToClass("sEntry"), .FieldToClass("sClient"), .FieldToClass("sCliename"), .FieldToClass("nUnit_balance"), .FieldToClass("nInstitution"), .FieldToClass("sInstitution"), .FieldToClass("nOrigin"), .FieldToClass("sOrigin"), .FieldToClass("dDate_origin"))
					.RNext()
				Loop 
				.RCloseRec()
				Find_units = True
			End If
		End With
		
		'UPGRADE_NOTE: Object lrecreaFund_Units may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaFund_Units = Nothing
		
Find_Units_Err: 
		If Err.Number Then
			Find_units = False
		End If
	End Function
	
	'*** Item: Use when making reference to an element of the collection
	'*** vntIndexKey contains the index or the password of the collection,
	'*** and that is why it is declared as a variant
	'*** Syntax: Set foo = x.Item(xyz) or Set foo = x.Item(5)
	'* Item: Se usa al hacer referencia a un elemento de la colección
	'* vntIndexKey contiene el índice o la clave de la colección,
	'* por lo que se declara como un Variant
	'* Syntax: Set foo = x.Item(xyz) or Set foo = x.Item(5)
	Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As Fund_move
		Get
			Item = mCol.Item(vntIndexKey)
		End Get
	End Property
	
	'*** Count: Returns the number of elements that the collection has
	'* Count: Devuelve el número de elementos que posee la colección
	Public ReadOnly Property Count() As Integer
		Get
			
			'**+ Used when obtaining the number of elemnets of the
			'**+ collection. Sytnax: Debug.print x.Count
			'+ Se usa al obtener el número de elementos de la
			'+ colección. Sintaxis: Debug.Print x.Count
			
			Count = mCol.Count()
		End Get
	End Property
	
	'*** NewEnum: Enumerates the collection for use in a For Each...Next loop
	'* NewEnum: Permite enumerar la colección para utilizarla en un ciclo For Each... Next
	'UPGRADE_NOTE: NewEnum property was commented out. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="B3FC1610-34F3-43F5-86B7-16C984F0E88E"'
	'Public ReadOnly Property NewEnum() As stdole.IUnknown
		'Get
			'
			'** This property allows to enumerate
			'** this collection with the syntax For...Each
			'+ Esta propiedad permite enumerar
			'+ esta colección con la sintaxis For...Each
			'
			'NewEnum = mCol._NewEnum
		'End Get
	'End Property
	
	Public Function GetEnumerator() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
		'UPGRADE_TODO: Uncomment and change the following line to return the collection enumerator. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="95F9AAD0-1319-4921-95F0-B9D3C4FF7F1C"'
		GetEnumerator = mCol.GetEnumerator
	End Function
	
	'**% Remove: Deletes an element from the collection
	'% Remove: Elimina un elemento de la colección
	Public Sub Remove(ByRef vntIndexKey As Object)
		
		'**+ Used when taking an element from the collection
		'**+ vntIndexKey contains the index or the password, and
		'**+ that is why it is declared as a variant
		'**+ Syntax: x.Remove (xyz)
		'+ Se usa al quitar un elemento de la colección
		'+ vntIndexKey contiene el índice o la clave, por lo que se
		'+ declara como un Variant
		'+ Sintaxis: x.Remove(xyz)
		
		mCol.Remove(vntIndexKey)
	End Sub
	
	'**% Class_Initialize: Controls the creation of an instance of the collection
	'% Class_Initialize: Controla la creación de una instancia de la colección
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		
		'**+ Creates the collection when the class is created
		'+ Crea la colección cuando se crea la clase
		
		mCol = New Collection
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
	
	'**% Class_Terminate: Controls the destruction of an instance of the collection
	'% Class_Terminate: Controla la destrucción de una instancia de la colección
	'UPGRADE_NOTE: Class_Terminate was upgraded to Class_Terminate_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Terminate_Renamed()
		
		'**+ Deletes the collection when the class is finished
		'+ Destruye la colección cuando se termina la clase
		
		'UPGRADE_NOTE: Object mCol may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		mCol = Nothing
	End Sub
	Protected Overrides Sub Finalize()
		Class_Terminate_Renamed()
		MyBase.Finalize()
	End Sub
End Class






