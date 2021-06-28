Option Strict Off
Option Explicit On
Public Class Tar_am_detprods
	Implements System.Collections.IEnumerable
	'%-------------------------------------------------------%'
	'% $Workfile:: Tar_am_detprods.cls                      $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 1:38p                                $%'
	'% $Revision:: 8                                        $%'
	'%-------------------------------------------------------%'
	
	'- Variables locales para la colección
	Private mCol As Collection
	
	'%Add: Añade una nueva instancia de la clase a la colección
    Public Function Add(ByRef nPremium As Double, ByRef dNulldate As Date, ByRef nAge_end As Integer, _
                        ByRef dEffecdate As Date, ByRef nAge_init As Integer, ByRef nGroup_comp As Integer, _
                        ByRef nTariff As Integer, ByRef nModulec As Integer, ByRef nCover As Integer) As Tar_am_detprod
        '+ Se crea un nuevo objeto
        Dim objNewMember As Tar_am_detprod
        objNewMember = New Tar_am_detprod


        With objNewMember
            .nPremium = nPremium
            .dNulldate = dNulldate
            .nAge_end = nAge_end
            .dEffecdate = dEffecdate
            .nAge_init = nAge_init
            .nGroup_comp = nGroup_comp
            .nTariff = nTariff

            .nModulec = nModulec
            .nCover = nCover

        End With

        mCol.Add(objNewMember)


        '+ Se retorna el objeto creado
        Add = objNewMember
        'UPGRADE_NOTE: Object objNewMember may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        objNewMember = Nothing


    End Function
	
	'%Item: Devuelve un elemento de la colección (segun índice)
	Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As Tar_am_detprod
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
	
	'%Find: Permite consultar el detalle de una tarifa de Atención médica
    Public Function Find(ByVal lintBranch As Integer, ByVal lintProduct As Integer, ByVal lintTariff As Integer, _
                         ByVal ldtmEffecdate As Date, Optional ByVal lblnFind As Boolean = False, _
                         Optional ByVal nModulec As Integer = 0, Optional ByVal nCover As Integer = 0) As Boolean

        Dim lrecreaTar_am_detprod As eRemoteDB.Execute
        Dim lintPos As Integer

        On Error GoTo reaTar_am_detprod_Err

        lrecreaTar_am_detprod = New eRemoteDB.Execute

        '+Definición de parámetros para stored procedure 'insudb.reaTar_am_detprod'
        '+Información leída el 17/01/2000 15:31:06

        With lrecreaTar_am_detprod
            .StoredProcedure = "reaTar_am_detprod"
            .Parameters.Add("nBranch", lintBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", lintProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nTariff", lintTariff, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", ldtmEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            .Parameters.Add("nModulec", nModulec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCover", nCover, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)


            If .Run Then
                Do While Not .EOF
                    Call Add(.FieldToClass("nPremium"), .FieldToClass("dNulldate"), .FieldToClass("nAge_end"), _
                             .FieldToClass("dEffecdate"), .FieldToClass("nAge_Init"), .FieldToClass("nGroup_comp"), _
                             .FieldToClass("nTariff"), .FieldToClass("nModulec"), .FieldToClass("nCover"))
                    .RNext()
                Loop

                Find = True

                .RCloseRec()
            End If
        End With

reaTar_am_detprod_Err:
        If Err.Number Then
            Find = False
        End If
        'UPGRADE_NOTE: Object lrecreaTar_am_detprod may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecreaTar_am_detprod = Nothing
        On Error GoTo 0

    End Function
End Class






