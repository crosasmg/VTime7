Option Strict Off
Option Explicit On
Public Class Tar_prem_SOAPs
	Implements System.Collections.IEnumerable
	'%-------------------------------------------------------%'
	'% $Workfile:: Tar_prem_SOAPs.cls                        $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 1:38p                                $%'
	'% $Revision:: 8                                        $%'
	'%-------------------------------------------------------%'
	
	'- Local variable to hold collection
	Private mCol As Collection
	
	'- Variables para almacenar los datos de la búsqueda
    Private mdtmEffecdate As Date

	'% Add: Añade una nueva instancia de la clase Tar_prem_SOAP a la colección
	Public Function Add(ByRef objTar_prem_SOAP As Tar_prem_SOAP) As Tar_prem_SOAP
		With objTar_prem_SOAP
            mCol.Add(objTar_prem_SOAP, "MT" & .nVehType & .dEffecdate)
		End With
		
		Add = objTar_prem_SOAP

        objTar_prem_SOAP = Nothing
	End Function
	
    '% Find: Lee las tarifas de prima de SOAP
    Public Function Find(ByVal dEffecdate As Date, Optional ByRef bFind As Boolean = False) As Boolean
        Dim lrecTar_prem_SOAP As eRemoteDB.Execute
        Dim lclsTar_prem_SOAP As Tar_prem_SOAP

        If bFind Or dEffecdate <> mdtmEffecdate Then

            lrecTar_prem_SOAP = New eRemoteDB.Execute

            With lrecTar_prem_SOAP
                .StoredProcedure = "reaTar_prem_SOAP_a"
                .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                If .Run Then
                    Do While Not .EOF
                        lclsTar_prem_SOAP = New Tar_prem_SOAP
                        lclsTar_prem_SOAP.nVehType = .FieldToClass("nVehType")
                        lclsTar_prem_SOAP.dEffecdate = dEffecdate
                        lclsTar_prem_SOAP.nPremium = .FieldToClass("nPremium")
                        Call Add(lclsTar_prem_SOAP)
                        lclsTar_prem_SOAP = Nothing
                        .RNext()
                    Loop
                    mdtmEffecdate = dEffecdate
                    Find = True
                End If
            End With
        End If
    End Function
	
	'* Item: Devuelve un elemento de la colección (segun índice)
	Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As Tar_prem_SOAP
		Get
			Item = mCol.Item(vntIndexKey)
		End Get
	End Property
	
	'* Count: Devuelve el número de elementos que posee la colección
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
	
	'* Class_Initialize: se controla la creación de la instancia del objeto
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		mCol = New Collection
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
	
	'* Class_Terminate: se controla la destrucción de la instancia del objeto
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






