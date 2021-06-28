Option Strict Off
Option Explicit On
Public Class TRCMs
    Implements System.Collections.IEnumerable
    '%-------------------------------------------------------%'
    '% $Workfile:: AccidentPersons.cls                         $%'
    '% $Author:: Nvaplat41                                  $%'
    '% $Date:: 9/10/03 19.01                                $%'
    '% $Revision:: 10                                       $%'
    '%-------------------------------------------------------%'
    'local variable to hold collection
    Private mCol As Collection

    Public Function Add(ByVal objClass As TRCM) As TRCM
        'create a new object
        If objClass Is Nothing Then
            objClass = New TRCM
        End If

        With objClass
            mCol.Add(objClass)
        End With

        'return the object created
        Add = objClass
        'UPGRADE_NOTE: Object objClass may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        objClass = Nothing

    End Function
    '%Find: Lee los datos de la tabla para la transacción VI665
    Public Function Find(ByVal sCertype As String, ByVal nBranch As Double, ByVal nProduct As Double, ByVal nCertif As Double, ByVal nPolicy As Double, ByVal dEffecdate As Date) As Boolean
        Dim lrecTRCM As eRemoteDB.Execute
        Dim lclsTRCM As ePolicy.TRCM
        On Error GoTo Find_Err

        '        '+Definición de parámetros para stored procedure 'ReaActiv_Group_a'
        '        '+Información leída el 02/02/2002
        lrecTRCM = New eRemoteDB.Execute
        With lrecTRCM
            .StoredProcedure = "ReaTRCM"
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If .Run(True) Then
                Do While Not .EOF
                    Find = True
                    lclsTRCM = New TRCM
                    lclsTRCM.dInitialdate_work = .FieldToClass("dInitialdate_work")
                    lclsTRCM.dEnddate_work = .FieldToClass("dEnddate_work")
                    lclsTRCM.dNulldate = .FieldToClass("dNulldate")
                    lclsTRCM.dInitialdate_em = .FieldToClass("dInitialdate_em")
                    lclsTRCM.dEnddate_em = .FieldToClass("dEnddate_em")
                    lclsTRCM.dInitialdate_m = .FieldToClass("dInitialdate_m")
                    lclsTRCM.dEnddate_m = .FieldToClass("dEnddate_m")
                    lclsTRCM.sWorkname = .FieldToClass("sWorkname")
                    lclsTRCM.nWorktype = .FieldToClass("nWorktype")
                    lclsTRCM.sDesc_work = .FieldToClass("sDesc_work")
                    lclsTRCM.nGroup = .FieldToClass("nGroup")
                    lclsTRCM.nSituation = .FieldToClass("nSituation")
                    Call Add(lclsTRCM)
                    lclsTRCM = Nothing
                    .RNext()
                Loop
                ''UPGRADE_NOTE: Object lclsActiv_Group may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
                .RCloseRec()
            Else
                Find = False
            End If
        End With
        lclsTRCM = Nothing
        lrecTRCM = Nothing
Find_Err:
        If Err.Number Then
            Find = False
        End If
        'UPGRADE_NOTE: Object lrecReaActiv_Group_a may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
  
        On Error GoTo 0
    End Function

    Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As TRCM
        Get
            'used when referencing an element in the collection
            'vntIndexKey contains either the Index or Key to the collection,
            'this is why it is declared as a Variant
            'Syntax: Set foo = x.Item(xyz) or Set foo = x.Item(5)
            Item = mCol.Item(vntIndexKey)
        End Get
    End Property

    Public ReadOnly Property Count() As Integer
        Get
            'used when retrieving the number of elements in the
            'collection. Syntax: Debug.Print x.Count
            Count = mCol.Count()
        End Get
    End Property

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


    Public Sub Remove(ByRef vntIndexKey As Object)
        'used when removing an element from the collection
        'vntIndexKey contains either the Index or Key, which is why
        'it is declared as a Variant
        'Syntax: x.Remove(xyz)

        mCol.Remove(vntIndexKey)
    End Sub


    'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
    Private Sub Class_Initialize_Renamed()
        'creates the collection when this class is created
        mCol = New Collection
    End Sub
    Public Sub New()
        MyBase.New()
        Class_Initialize_Renamed()
    End Sub

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
End Class
