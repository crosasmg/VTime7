Option Strict Off
Option Explicit On
Public Class TmpReportMasives
	Implements System.Collections.IEnumerable
	'%-------------------------------------------------------%'
	'% $Workfile:: TCovers.cls                              $%'
	'% $Author:: Jsarabia                                   $%'
	'% $Date:: 7-08-09 12:23                                $%'
	'% $Revision:: 3                                        $%'
	'%-------------------------------------------------------%'
	
	'- Tipo de ramo: 1)Vida 2)No vida
    Public Enum eBranchTypes
        clngLife = 1
        clngNoLife = 2
    End Enum
	
    Public objtCover As TmpReportMasive
	
	Private mCol As Collection
	
    Public mcolTCovers As TmpReportMasive
	
	'- Variables auxiliares
	Public nCoverWindow As Integer
	Public nLegAmount As Double
	
	'- Variables para manejo de errores.
	Public nError As Integer
	Public bError As Boolean
	
	'-Variable que indica si existe información en la tabla de tratamiento de coberturas
	'-(COVER, COVER_CO_P, COVER_CO_G)
	Public bDataFound As Boolean
	
	'% Add: Añade una nueva instancia de la clase TCover a la colección
    Public Function Add(ByRef objClass As TmpReportMasive) As TmpReportMasive
        If objClass Is Nothing Then
            objClass = New TmpReportMasive
        End If

        With objClass
            mCol.Add(objClass)
        End With

        'retorna el elemento creado
        Add = objClass
    End Function

	
	'%Find: Obtiene los datos de la tabla TCover
    Public Function Find(ByVal sKey As String, Optional ByVal bIsLife As Boolean = False) As Boolean
        Dim lrecreatcover As eRemoteDB.Execute
        Dim lclsTmpReportMasives As TmpReportMasive

        On Error GoTo Find_Err
        lrecreatcover = New eRemoteDB.Execute
        mCol = New Collection
        With lrecreatcover
            .StoredProcedure = "REA_TMP_REPORT_MASIVE"
            .Parameters.Add("sKey", sKey, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If .Run Then
                Find = True
                Do While Not .EOF
                    lclsTmpReportMasives = New TmpReportMasive
                    lclsTmpReportMasives.sKey = .FieldToClass("sKey")
                    lclsTmpReportMasives.nId = .FieldToClass("nId")
                    lclsTmpReportMasives.sCertype = .FieldToClass("sCertype")
                    lclsTmpReportMasives.nBranch = .FieldToClass("nBranch")
                    lclsTmpReportMasives.nProduct = .FieldToClass("nProduct")
                    lclsTmpReportMasives.nPolicy = .FieldToClass("nPolicy")
                    lclsTmpReportMasives.nCertif = .FieldToClass("nCertif")
                    lclsTmpReportMasives.dDate_origi = .FieldToClass("dDate_origi")
                    lclsTmpReportMasives.dStartdate = .FieldToClass("dStartdate")
                    lclsTmpReportMasives.nType_amend = .FieldToClass("nType_amend")
                    lclsTmpReportMasives.nStatus = .FieldToClass("nStatus")
                    lclsTmpReportMasives.dDate_printer = .FieldToClass("dDate_printer")
                    lclsTmpReportMasives.sExecutiontype = .FieldToClass("sExecutiontype")
                    lclsTmpReportMasives.sClient = .FieldToClass("sClient")
                    lclsTmpReportMasives.dAprobdate = .FieldToClass("dAprobdate")
                    lclsTmpReportMasives.sTypereport = .FieldToClass("sTypereport")
                    lclsTmpReportMasives.nFolionum = .FieldToClass("nFolionum")
                    Call Add(lclsTmpReportMasives)
                    .RNext()
                Loop
                .RCloseRec()
            Else
                Find = False
            End If
        End With

Find_Err:
        If Err.Number Then
            Find = False
        End If
        On Error GoTo 0
        'UPGRADE_NOTE: Object lrecreatcover may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecreatcover = Nothing
    End Function
	
	'%FindCoverPolicy. Esta rutina se encarga de realizar el cálculo de las coberturas
	'%de la póliza/certificado.
 	
	'%InsHereditCover_p_g: Hereda las condiciones de asegurabilidad de una poliza a otra

	'%sKey. Esta propiedad se encarga de devolver la llave de lectura del registro de coberturas
	Public ReadOnly Property sKey(ByVal nUsercode As Integer, ByVal nSessionId As String) As String
		Get
			sKey = "Cov" & CStr(nSessionId) & "-" & CStr(nUsercode)
		End Get
	End Property
	
	'* Item: Devuelve un elemento de la colección (segun índice)
	'-----------------------------------------------------------
    Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As TmpReportMasive
        Get
            '-----------------------------------------------------------
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
	
	'%Delete. Esta funcion se encarga de eliminar los registros de la tabla tCovers
    Public Function Delete(ByVal sKey As String, ByVal nId As Long) As Boolean
        Dim lrecdeltCover As eRemoteDB.Execute

        On Error GoTo Delete_err
        lrecdeltCover = New eRemoteDB.Execute

        With lrecdeltCover
            .StoredProcedure = "DEL_TMP_REPORT_MASIVE"
            .Parameters.Add("sKey", sKey, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nId", nId, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Run(False)
        End With

Delete_err:
        If Err.Number Then
            Delete = False
        End If
        On Error GoTo 0
        'UPGRADE_NOTE: Object lrecdeltCover may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecdeltCover = Nothing
    End Function
	
	'%FindItem: Busca un elemento dentro de la colección dado el código de la cobertura
	Public Function FindItem(ByVal nCover As Integer) As Boolean
		Dim lintIndex As Integer
		FindItem = False
		'UPGRADE_NOTE: Object objtCover may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		objtCover = Nothing
		For lintIndex = 1 To mCol.Count()
			If mCol.Item(lintIndex).nCover = nCover Then
				objtCover = mCol.Item(lintIndex)
				FindItem = True
				Exit For
			End If
		Next 
	End Function
	
	'% Remove: Elimina un elemento de la colección
	'---------------------------------------------
	Public Sub Remove(ByRef vntIndexKey As Object)
		'---------------------------------------------
		mCol.Remove(vntIndexKey)
	End Sub
	
	'% Class_Initialize: Controla la creación de una instancia de la colección
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		mCol = New Collection
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
	
	'% Class_Terminate: Controla la destrucción de una instancia de la colección
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






