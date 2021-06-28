Option Strict Off
Option Explicit On
Public Class Tar_am_basprods
	Implements System.Collections.IEnumerable
	'%-------------------------------------------------------%'
	'% $Workfile:: Tar_am_basprods.cls                      $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 1:38p                                $%'
	'% $Revision:: 6                                        $%'
	'%-------------------------------------------------------%'
	
	'- Variables locales para la colección
	Private mCol As Collection
	
	'%Add: Agrega un elemento a la colección
	Public Function Add(ByRef lclsTar_am_basprod As Tar_am_basprod) As Tar_am_basprod
		'+ Se establecen las propiedades que se transfieren al método
		With lclsTar_am_basprod
            mCol.Add(lclsTar_am_basprod, "AM" & .nTariff & .nModulec & .nCover & .dEffecdate & .dNulldate & .nBenef_type & .sDefaulti & .nDed_amount & .nLimit & .sChanges)
		End With
		
		'Retorna el objeto creado
		Add = lclsTar_am_basprod
		'UPGRADE_NOTE: Object lclsTar_am_basprod may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsTar_am_basprod = Nothing
	End Function
	
	'%Find: Permite consultar las tarifas de Atención médica de un producto
    Public Function Find(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal dEffecdate As Date, Optional ByVal bFind As Boolean = False, _
                         Optional ByVal nModulec As Integer = 0, Optional ByVal nCover As Integer = 0) As Boolean
        Dim lrecreaTar_am_basprod As eRemoteDB.Execute
        Dim lclsTar_am_basprod As Tar_am_basprod

        On Error GoTo reaTar_am_basprod_Err

        lrecreaTar_am_basprod = New eRemoteDB.Execute

        With lrecreaTar_am_basprod
            .StoredProcedure = "reaTar_am_basprod"
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            .Parameters.Add("nModulec", nModulec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCover", nCover, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            If .Run Then
                Do While Not .EOF
                    lclsTar_am_basprod = New Tar_am_basprod
                    With lclsTar_am_basprod
                        .nTariff = lrecreaTar_am_basprod.FieldToClass("nTariff")
                        .dEffecdate = lrecreaTar_am_basprod.FieldToClass("dEffecdate")
                        .dNulldate = lrecreaTar_am_basprod.FieldToClass("dNulldate")
                        .nBenef_type = lrecreaTar_am_basprod.FieldToClass("nBenef_type")
                        .sDefaulti = lrecreaTar_am_basprod.FieldToClass("sDefaulti")
                        .nDed_amount = lrecreaTar_am_basprod.FieldToClass("nDed_amount")
                        .nLimit = lrecreaTar_am_basprod.FieldToClass("nLimit")
                        .sChanges = lrecreaTar_am_basprod.FieldToClass("sChanges")

                        .nModulec = lrecreaTar_am_basprod.FieldToClass("nModulec")
                        .nCover = lrecreaTar_am_basprod.FieldToClass("nCover")
                        .sDescript = lrecreaTar_am_basprod.FieldToClass("sDescript")

                    End With
                    Call Add(lclsTar_am_basprod)
                    'UPGRADE_NOTE: Object lclsTar_am_basprod may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
                    lclsTar_am_basprod = Nothing
                    .RNext()
                Loop

                Find = True

                .RCloseRec()
            End If
        End With

reaTar_am_basprod_Err:
        If Err.Number Then
            Find = False
        End If
        'UPGRADE_NOTE: Object lrecreaTar_am_basprod may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecreaTar_am_basprod = Nothing
        On Error GoTo 0
    End Function
	
	'%Item: Devuelve un elemento de la colección (segun índice)
	Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As Tar_am_basprod
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






