Option Strict Off
Option Explicit On
Public Class Detailsallowed
    '%-------------------------------------------------------%'
    '% $Workfile:: Detailsallowed.cls                            $%'
    '% $Author:: Nvaplat7                                   $%'
    '% $Date:: 9/08/03 1:06p                                $%'
    '% $Revision:: 11                                       $%'
    '%-------------------------------------------------------%'

    '**- Properties according to the table in the system on November 08,2000.
    '- Propiedades según la tabla en el sistema al 08/11/2000.
    '**- The key fields corresponds to:  sCertype, nBranch, nProduct, nPolicy, nCertif, nId and dEffecdate.
    '- Los campos llave de la tabla corresponden a: sCertype, nBranch, nProduct, nPolicy, nCertif, nId y dEffecdate.

    '   Column_name                    Type     Computed     Length  Prec  Scale Nullable   TrimTrailingBlanks    FixedLenNullInSource
    Public nBranch As Integer 'smallint    no          2      5     0     no              (n/a)                  (n/a)
    Public nProduct As Integer 'smallint    no          2      5     0     no              (n/a)                  (n/a)
    Public nCode_good As Integer 'smallint    no          2      5     0     no              (n/a)                  (n/a)
    Public sDescript As String 'char        no         30                  yes             no                     yes
    Public nType As Integer 'smallint    no          2      5     0     no              (n/a)                  (n/a)
    Public sAddCapital As String


    Public nUsercode As Integer 'smallint    no          2      5     0     yes             (n/a)                  (n/a)
    
    Public sSelected As String
    Public sExist As String
    Public dEffecdate As Date

    Public sDescript_Good As String
    Public sDescript_Type As String


    '**- Indicator that the the user can decrease the values shown in the system,
    '**- during the treatment of the policy,
    '- Indicador de si el usuario puede disminuir, durante el tratamiento de la póliza,
    '- los valores mostrados por el sistema

    Private mstrAction As String

  


    '**% Delete: Delete an insured good of the Insured Goods table (Detailsallowed)
    '% Delete: Elimina un bien asegurado de la tabla de Bienes Asegurados (Detailsallowed)
    Public Function Delete() As Boolean

        Dim lrecdelDetailsallowed As eRemoteDB.Execute

        On Error GoTo Delete_err

        lrecdelDetailsallowed = New eRemoteDB.Execute

        '**+ Parameter definitiof for stored procedure 'insudb.delDetailsallowed'
        '+ Definición de parámetros para stored procedure 'insudb.delDetailsallowed'
        '**+ Information read on May 02,2001  02:53:27 p.m.
        '+ Información leída el 02/05/2001 02:53:27 p.m.

        With lrecdelDetailsallowed
            .StoredProcedure = "delDetailsallowed"
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCode_good", nCode_good, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nType", nType, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            Delete = .Run(False)
        End With

Delete_err:
        If Err.Number Then
            Delete = False
        End If
        'UPGRADE_NOTE: Object lrecdelDetailsallowed may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecdelDetailsallowed = Nothing
        On Error GoTo 0
    End Function

    '**% Update: Updates an insured good of the Insured Goods table (Detailsallowed)
    '% Update: Actualiza un bien asegurado de la tabla de Bienes Asegurados (Detailsallowed)
    Public Function Update() As Boolean

        Dim lrecinsDetailsallowed As eRemoteDB.Execute

        On Error GoTo Update_Err

        lrecinsDetailsallowed = New eRemoteDB.Execute

        '**+ Parameter definition for stored procedure 'insudb.insDetailsallowed'
        '+ Definición de parámetros para stored procedure 'insudb.insDetailsallowed'
        '**+ Information read on May 02,2001  03:05:35 p.m.
        '+ Información leída el 02/05/2001 03:05:35 p.m.

        With lrecinsDetailsallowed
            .StoredProcedure = "insDetailsallowed"
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCode_good", nCode_good, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sDescript", sDescript, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 30, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nType", nType, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sAddCapital", sAddCapital, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
          


            Update = .Run(False)
        End With
        'UPGRADE_NOTE: Object lrecinsDetailsallowed may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecinsDetailsallowed = Nothing

Update_Err:
        If Err.Number Then
            Update = False
        End If
        On Error GoTo 0
    End Function

    '*** Class_Initialize: controls the creation of each instance of the class
    '* Class_Initialize: Se controla la creación de cada instancia de la clase
    'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
    Private Sub Class_Initialize_Renamed()

        nUsercode = Int(CDbl(GetSetting("TIME", "GLOBALS", "USERCODE", CStr(0))))
    End Sub
    Public Sub New()
        MyBase.New()
        Class_Initialize_Renamed()
    End Sub

    '**%insValDP102: Validates the page "DP102" as described in the functional specifications
    '%InsValDP102: Este metodo se encarga de realizar las validaciones descritas en el funcional
    '%de la ventana "DP102"
    Public Function insValDP102(ByVal sCodispl As String, Optional ByVal nBranch As Integer = 0, Optional ByVal nProduct As Integer = 0, Optional ByVal nType As Integer = 0, Optional ByVal nCode_good As Integer = 0, Optional ByVal sDescript As String = "", Optional ByVal sAddCapital As String = "", Optional ByVal dEffecdate As Date = #12:00:00 AM#, Optional ByVal sWin_type As String = "", Optional ByVal sAction As String = "") As String


        Dim lclsErrors As eFunctions.Errors
        Dim lclsvalfield As eFunctions.valField
        Dim lcolDetailsallowedses As ePolicy.Detailsallowedses

        On Error GoTo insValDP102_Err

        lclsErrors = New eFunctions.Errors
        lclsvalfield = New eFunctions.valField
        lcolDetailsallowedses = New ePolicy.Detailsallowedses

        lclsvalfield.objErr = lclsErrors

        If sWin_type <> "PopUp" Then
            If Not lcolDetailsallowedses.Find(nBranch, nProduct, dEffecdate) Then
                Call lclsErrors.ErrorMessage(sCodispl, 1928)
            End If
        End If

        If sWin_type = "PopUp" Then
            If sAction = "Add" Then
                If lcolDetailsallowedses.Find_Dup(nBranch, nProduct, nCode_good, nType, dEffecdate) Then
                    Call lclsErrors.ErrorMessage(sCodispl, 10004)
                End If
            End If

            If (nCode_good = eRemoteDB.Constants.intNull Or nCode_good = 0) Then
                Call lclsErrors.ErrorMessage(sCodispl, 1012, , , "Código")
            End If


            If (nType = eRemoteDB.Constants.intNull Or nType = 0) Then
                Call lclsErrors.ErrorMessage(sCodispl, 1012, , , "Tipo de desglose")
            End If

            If sDescript = String.Empty Then
                Call lclsErrors.ErrorMessage(sCodispl, 10005)
            End If



        End If

        insValDP102 = lclsErrors.Confirm

        'UPGRADE_NOTE: Object lcolDetailsallowedses may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lcolDetailsallowedses = Nothing
        'UPGRADE_NOTE: Object lclsvalfield may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsvalfield = Nothing
        'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsErrors = Nothing

insValDP102_Err:
        If Err.Number Then
            insValDP102 = insValDP102 & Err.Description
        End If
        On Error GoTo 0
    End Function

    '**% insPostDP102: Validate all the introduced data in the specific content zone for "Frame"
    '% insPostDP102: Valida los datos introducidos en la zona de contenido para "frame" especifico
    Public Function insPostDP102(ByVal sAction As String, ByVal nMainAction As Integer, Optional ByVal nBranch As Integer = 0, Optional ByVal nProduct As Integer = 0, Optional ByVal nType As Integer = 0, Optional ByVal nCode_good As Integer = 0, Optional ByVal sDescript As String = "", Optional ByVal sAddCapital As String = "", Optional ByVal nUserCode As Integer = 0, Optional ByVal dEffecdate As Date = #12:00:00 AM#) As Boolean

        Dim lcolDetailsallowedses As ePolicy.Detailsallowedses
        Dim lclsProd_win As eProduct.Prod_win

        lcolDetailsallowedses = New ePolicy.Detailsallowedses
        lclsProd_win = New eProduct.Prod_win

        '**+ This assignation is for use the incoming information in all
        '**+ the routines called in insPostDP102, without having to pass it as a parameter.
        '+ Esta asignación es para utilizar la información entrante en todas
        '+ las rutinas llamadas dentro de insPostDP102, sin tener que pasarla como parámetro

        With Me
            .nBranch = nBranch
            .nProduct = nProduct
            .dEffecdate = dEffecdate
            .nCode_good = nCode_good
            .sDescript = sDescript
            .nType = nType
            .sAddCapital = sAddCapital
            .nUsercode = nUserCode

            mstrAction = sAction

            If nMainAction <> eFunctions.Menues.TypeActions.clngActionQuery Then
                If insCreDetailsAllowed() Then
                    insPostDP102 = True

                    If lcolDetailsallowedses.Find(CShort(.nBranch), CShort(.nProduct), dEffecdate, ) Then
                        Call lclsProd_win.Add_Prod_win(.nBranch, .nProduct, .dEffecdate, "DP102", "2", .nUsercode)
                    Else
                        Call lclsProd_win.Add_Prod_win(.nBranch, .nProduct, .dEffecdate, "DP102", "1", .nUsercode)
                    End If
                End If
            Else
                insPostDP102 = True
            End If
        End With

        'UPGRADE_NOTE: Object lclsProd_win may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsProd_win = Nothing
        'UPGRADE_NOTE: Object lcolDetailsallowedses may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lcolDetailsallowedses = Nothing
    End Function
    '**% insCreTab_goods: Associate all the selected actions for the user to a transaction
    '% insCreTab_goods: Asocia todas las acciones seleccionadas por el usuario a una transacción
    Private Function insCreDetailsAllowed() As Boolean

        Dim lstrChange_typ As String

        On Error GoTo insCreTab_goods_Err

        insCreDetailsAllowed = True

        If (mstrAction = "Add" Or mstrAction = "Update") And Not (Me.nCode_good = eRemoteDB.Constants.intNull) Then

            If Not Me.Update Then
                insCreDetailsAllowed = False
            End If
        ElseIf mstrAction = "Del" Then
            If Not Me.Delete Then
                insCreDetailsAllowed = False
            End If
        End If

insCreTab_goods_Err:
        If Err.Number Then
            insCreDetailsAllowed = False
        End If
        On Error GoTo 0
    End Function
End Class




