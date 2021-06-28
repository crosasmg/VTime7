Option Strict Off
Option Explicit On
Public Class Propertys
	Implements System.Collections.IEnumerable
	'%-------------------------------------------------------%'
	'% $Workfile:: Propertys.cls                            $%'
	'% $Author:: Nvaplat41                                  $%'
	'% $Date:: 9/10/03 19.01                                $%'
	'% $Revision:: 9                                        $%'
	'%-------------------------------------------------------%'
	
	Private mCol As Collection
	
	'**- Auxiliary variables
	'- Variables auxiliares
	'**- Define the variables that will be used for the search
	'- Se definen las variables que se van a utilizar para la busqueda
	Private mstrCertype As String
	Private mintBranch As Integer
	Private mintProduct As Integer
	Private mlngPolicy As Integer
	Private mlngCertif As Integer
	Private mdtmEffecdate As Date
	
	'**% Add: Adds a new instance of the Property class to the collection
	'% Add: A�ade una nueva instancia de la clase Property a la colecci�n
    Public Function Add(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal nId As Integer, ByVal dEffecdate As Date, ByVal nCode_good As Integer, ByVal nCapital As Double, ByVal sDescript As String, ByVal sFrandedi As String, ByVal nDiscount As Double, ByVal nLost_capit As Double, ByVal nNotenum As Integer, ByVal nFixamount As Double, ByVal dNulldate As Date, ByVal nMaxamount As Double, ByVal nPremium As Double, ByVal nMinamount As Double, ByVal nRateProp As Double, ByVal nUsercode As Integer, ByVal nRate As Double, ByVal nCurrency As Integer, ByVal nServ_order As Double) As Property_Renamed
        '**- Define the variable that will contain the instance to be added.
        '- Se define la variable que contendra la instancia a a�adir
        Dim objNewMember As Property_Renamed

        objNewMember = New Property_Renamed

        With objNewMember
            .sCertype = sCertype
            .nBranch = nBranch
            .nProduct = nProduct
            .nPolicy = nPolicy
            .nCertif = nCertif
            .nId = nId
            .dEffecdate = dEffecdate
            .nCode_good = nCode_good
            .nCapital = nCapital
            .sDescript = sDescript
            .sFrandedi = sFrandedi
            .nDiscount = nDiscount
            .nLost_capit = nLost_capit
            .nNotenum = nNotenum
            .nFixamount = nFixamount
            .dNulldate = dNulldate
            .nMaxamount = nMaxamount
            .nPremium = nPremium
            .nMinamount = nMinamount
            .nRateProp = nRateProp
            .nUsercode = nUsercode
            .nRate = nRate
            .nCurrency = nCurrency
            .nServ_order = nServ_order
        End With

        mCol.Add(objNewMember)

        '**+ Return the created object.
        '+ Retorna el objeto creado

        Add = objNewMember
        'UPGRADE_NOTE: Object objNewMember may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        objNewMember = Nothing
    End Function
	
	'**% Find: This method fills the collection with records from the table "Property" returning TRUE or FALSE
	'**% depending on the existence of the records
	'% Find: Este metodo carga la coleccion de elementos de la tabla "Property" devolviendo Verdadero o
	'% falso, dependiendo de la existencia de los registros.
	'------------------------------------------------------------
    Public Function Find(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, Optional ByVal lblnFind As Boolean = False) As Boolean
        '------------------------------------------------------------
        '**- Declare the variable that determines the result of the function (Tru/False)
        '- Se declara la variable que determina el resultado de la funcion (True/False)
        Static lblnRead As Boolean

        '**- Variable definition lrecreaProperty.
        '- Se define la variable lrecreaProperty
        Dim lrecreaProperty As eRemoteDB.Execute

        lrecreaProperty = New eRemoteDB.Execute

        On Error GoTo Find_Err

        If mstrCertype <> sCertype Or mintBranch <> nBranch Or mintProduct <> nProduct Or mlngPolicy <> nPolicy Or mlngCertif <> nCertif Or mdtmEffecdate <> dEffecdate Or lblnFind Then

            mstrCertype = sCertype
            mintBranch = nBranch
            mintProduct = nProduct
            mlngPolicy = nPolicy
            mlngCertif = nCertif
            mdtmEffecdate = dEffecdate

            '**+ Parameter definition for stored procedure 'insudb.reaProperty'
            '+ Definici�n de par�metros para stored procedure 'insudb.reaProperty'
            '**+ Information read on November 07,2000  10:01:39 a.m.
            '+ Informaci�n le�da el 07/11/2000 10:01:39 AM
            With lrecreaProperty
                .StoredProcedure = "reaProperty"
                .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

                If .Run Then
                    Do While Not .EOF
                        Call Add(.FieldToClass("sCertype"), .FieldToClass("nBranch"), .FieldToClass("nProduct"), .FieldToClass("nPolicy"), .FieldToClass("nCertif"), .FieldToClass("nId"), .FieldToClass("dEffecdate"), .FieldToClass("nCode_good"), .FieldToClass("nCapital"), .FieldToClass("sDescript"), .FieldToClass("sFrandedi"), .FieldToClass("nDiscount"), .FieldToClass("nLost_capit"), .FieldToClass("nNotenum"), .FieldToClass("nFixamount"), .FieldToClass("dNulldate"), .FieldToClass("nMaxamount"), .FieldToClass("nPremium"), .FieldToClass("nMinamount"), .FieldToClass("nRateprop"), .FieldToClass("nUsercode"), .FieldToClass("nRate"), .FieldToClass("nCurrency"), .FieldToClass("nServ_order"))
                        .RNext()
                    Loop

                    .RCloseRec()
                    lblnRead = True
                Else
                    lblnRead = False
                End If
            End With
        End If

        Find = lblnRead
        'UPGRADE_NOTE: Object lrecreaProperty may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecreaProperty = Nothing
Find_Err:
        If Err.Number Then
            Find = False
        End If
        'UPGRADE_NOTE: Object lrecreaProperty may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecreaProperty = Nothing
    End Function
	
	'**% Item: Restores an element of the collection (according to index)
	'% Item: Devuelve un elemento de la colecci�n (segun �ndice)
	Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As Property_Renamed
		Get
			Item = mCol.Item(vntIndexKey)
		End Get
	End Property
	
	'**% Count: Restores the number of elements that the collection owns.
	'% Count: Devuelve el n�mero de elementos que posee la colecci�n
	Public ReadOnly Property Count() As Integer
		Get
			Count = mCol.Count()
		End Get
	End Property
	
	'**% NewEnum: enumerate the collection for using it in a cycle For Each...Next
	'% NewEnum: Permite enumerar la colecci�n para utilizarla en un ciclo For Each... Next
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
	
	'**% Remove: Removes an element from the collection.
	'% Remove: Elimina un elemento de la colecci�n
	'---------------------------------------------
	Public Sub Remove(ByRef vntIndexKey As Object)
		'---------------------------------------------
		mCol.Remove(vntIndexKey)
	End Sub
	
	'**% Class_Initialize: controls the creation of an instance of the collection.
	'% Class_Initialize: Controla la creaci�n de una instancia de la colecci�n
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		mCol = New Collection
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
	
	'**% Class_Terminate: controls the destruction of an instance of the collection.
	'% Class_Terminate: Controla la destrucci�n de una instancia de la colecci�n
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






