Option Strict Off
Option Explicit On
Public Class TabSeismicFloodZone
    '**-Defined the principal properties of the correspondent class to the TabSeismicFloodZone table (11/13/2001)
    '**-Column_name
    '-Se definen las propiedades principales de la clase correspondientes a la tabla TabSeismicFloodZone (13/11/2001)
    '-Column_name                               Type                                                                                                                             Computed                            Length      Prec  Scale Nullable                            TrimTrailingBlanks                  FixedLenNullInSource
    '------------------------------------------ -------------------------------------------------------------------------------------------------------------------------------- ----------------------------------- ----------- ----- ----- ----------------------------------- ----------------------------------- -----------------------------------
    Public nZip_Code As Double
    Public nGeographicZone1 As Long
    Public nGeographicZone2 As Long
    Public nGeographicZone3 As Long
    Public nSeismicZone As Integer
    Public nDeduSeismicZone As Double
    Public nCoasSeismicZone As Double
    Public nZoneType As Integer
    Public nDeduZoneType As Double
    Public nCoasZoneType As Double
    Public dCompdate As Date
    Public sStatRegt As String
    Public nUsercode As Integer

    '**%Function Find: Find the TabSeismicFloodZone.
    '%Function Find: Busca en la tabla TabSeismicFloodZone.
    Public Function Find(ByVal nZip_Code As Double) As Boolean
        Dim lrecreaTabSeismicFloodZone As eRemoteDB.Execute
        Static lblnRead As Boolean

        lrecreaTabSeismicFloodZone = New eRemoteDB.Execute
        With lrecreaTabSeismicFloodZone
            .StoredProcedure = "reaTabSeismicFloodZone"
            .Parameters.Add("nZip_Code", nZip_Code, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            If .Run Then
                nZip_Code = .FieldToClass("nZip_Code")
                nGeographicZone1 = .FieldToClass("nGeographicZone1")
                nGeographicZone2 = .FieldToClass("nGeographicZone2")
                nGeographicZone3 = .FieldToClass("nGeographicZone3")
                nSeismicZone = .FieldToClass("nSeismicZone")
                nDeduSeismicZone = .FieldToClass("nDeduSeismicZone")
                nCoasSeismicZone = .FieldToClass("nCoasSeismicZone")
                nZoneType = .FieldToClass("nZoneType")
                nDeduZoneType = .FieldToClass("nDeduZoneType")
                nCoasZoneType = .FieldToClass("nCoasZoneType")
                sStatRegt = .FieldToClass("sStatRegt")

                lblnRead = True
                .RCloseRec()
            Else
                lblnRead = False
            End If
        End With

        lrecreaTabSeismicFloodZone = Nothing

        Find = lblnRead

        lrecreaTabSeismicFloodZone = Nothing

        Exit Function
    End Function

    '**%Function Find_Pol: Find the TabSeismicFloodZone.
    '%Function Find_Pol: Busca en la tabla TabSeismicFloodZone.
    Public Function Find_Pol(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, _
                               ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date) As Boolean
        Dim lrecreaTabSeismicFloodZone As eRemoteDB.Execute
        Static lblnRead As Boolean

        lrecreaTabSeismicFloodZone = New eRemoteDB.Execute
        With lrecreaTabSeismicFloodZone
            .StoredProcedure = "reaTabSeismicFloodZone_Pol"
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If .Run Then
                nZip_Code = .FieldToClass("nZip_Code")
                nGeographicZone1 = .FieldToClass("nGeographicZone1")
                nGeographicZone2 = .FieldToClass("nGeographicZone2")
                nGeographicZone3 = .FieldToClass("nGeographicZone3")
                nSeismicZone = .FieldToClass("nSeismicZone")
                nDeduSeismicZone = .FieldToClass("nDeduSeismicZone")
                nCoasSeismicZone = .FieldToClass("nCoasSeismicZone")
                nZoneType = .FieldToClass("nZoneType")
                nDeduZoneType = .FieldToClass("nDeduZoneType")
                nCoasZoneType = .FieldToClass("nCoasZoneType")
                sStatRegt = .FieldToClass("sStatRegt")

                lblnRead = True
                .RCloseRec()
            Else
                lblnRead = False
            End If
        End With

        lrecreaTabSeismicFloodZone = Nothing

        Find_Pol = lblnRead

        lrecreaTabSeismicFloodZone = Nothing

        Exit Function
    End Function

    '**%Add: This function add new register to the TabSeismicFloodZone table
    '%Add: Esta función agrega registros a la tabla TabSeismicFloodZone
    Public Function Add() As Boolean
        Dim lreccreTabSeismicFloodZone As eRemoteDB.Execute

        lreccreTabSeismicFloodZone = New eRemoteDB.Execute
        '**Parameters definition for stored procedure 'insudb.creClaim_caus'
        'Definición de parámetros para stored procedure 'insudb.creClaim_caus'
        '**Infoemation read on October 04 of 2001 06:23:31 p.m.
        'Información leída el 04/10/2001 06:23:31 p.m.

        With lreccreTabSeismicFloodZone
            .StoredProcedure = "creTabSeismicFloodZone"
            .Parameters.Add("nZip_Code", nZip_Code, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nGeographicZone1", nGeographicZone1, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nGeographicZone2", nGeographicZone2, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nGeographicZone3", nGeographicZone3, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nSeismicZone", nSeismicZone, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nDeduSeismicZone", nDeduSeismicZone, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 9, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCoasSeismicZone", nCoasSeismicZone, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 9, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nZoneType", nZoneType, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nDeduZoneType", nDeduZoneType, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 9, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCoasZoneType", nCoasZoneType, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 9, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sStatregt", sStatRegt, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            Add = .Run(False)

        End With

        lreccreTabSeismicFloodZone = Nothing

        Exit Function
    End Function

    '**%Update: This function update data of the TabSeismicFloodZone table
    '%Update: Esta función actualiza registros en la tabla TabSeismicFloodZone
    Public Function Update() As Boolean
        Dim lrecupdTabSeismicFloodZone As eRemoteDB.Execute

        lrecupdTabSeismicFloodZone = New eRemoteDB.Execute

        '**Parameters definition for stored procedure 'insudb.updClaim_caus'
        'Definición de parámetros para stored procedure 'insudb.updClaim_caus'
        '**Infoemation read on October 04 of 2001 06:48:22 p.m.
        'Información leída el 04/10/2001 06:48:22 p.m.

        With lrecupdTabSeismicFloodZone
            .StoredProcedure = "updTabSeismicFloodZone"
            .Parameters.Add("nZip_Code", nZip_Code, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nGeographicZone1", nGeographicZone1, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nGeographicZone2", nGeographicZone2, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nGeographicZone3", nGeographicZone3, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nSeismicZone", nSeismicZone, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nDeduSeismicZone", nDeduSeismicZone, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 9, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCoasSeismicZone", nCoasSeismicZone, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 9, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nZoneType", nZoneType, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nDeduZoneType", nDeduZoneType, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 9, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCoasZoneType", nCoasZoneType, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 9, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sStatregt", sStatRegt, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            Update = .Run(False)

        End With

        lrecupdTabSeismicFloodZone = Nothing

        Exit Function
    End Function

    '**%Delete: This function remove registers of the TabSeismicFloodZone table
    '%Delete: Esta función elimina registros de la tabla TabSeismicFloodZone
    Public Function Delete() As Boolean
        Dim lrecdelTabSeismicFloodZone As eRemoteDB.Execute


        lrecdelTabSeismicFloodZone = New eRemoteDB.Execute

        '**Parameters definition for stored procedure 'insudb.delClaim_caus'
        'Definición de parámetros para stored procedure 'insudb.delClaim_caus'
        '**Infoemation read on October 04 of 2001 06:52:26 p.m.
        'Información leída el 04/10/2001 06:52:26 p.m.

        With lrecdelTabSeismicFloodZone
            .StoredProcedure = "delTabSeismicFloodZone"
            .Parameters.Add("nZip_Code", nZip_Code, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            Delete = .Run(False)

        End With
        lrecdelTabSeismicFloodZone = Nothing

        Exit Function
    End Function

    '**%valExistTabSeismicFloodZone: This function validate if there are damages related to a specific branch
    '%valExistTabSeismicFloodZone: Valida la existencia de daños asociadas a un ramo el cual es pasado como parámetro.
    Public Function valExistTabSeismicFloodZone(ByVal nZip_Code As Double) As Boolean
        Dim lrecTabSeismicFloodZone As eRemoteDB.Execute


        valExistTabSeismicFloodZone = False

        lrecTabSeismicFloodZone = New eRemoteDB.Execute

        With lrecTabSeismicFloodZone
            .StoredProcedure = "valTabSeismicFloodZone_a"
            .Parameters.Add("nZip_Code", nZip_Code, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            If .Run(True) Then
                If .FieldToClass("lCount") > 0 Then
                    valExistTabSeismicFloodZone = True
                End If
                .RCloseRec()
            End If
        End With

        lrecTabSeismicFloodZone = Nothing

        Exit Function
    End Function

    '**%insValMHO001: This function perform validations over the fields of the folder
    '%insValMHO001: Esta función se encarga de validar los datos introducidos en la zona de detalle
    Public Function insValMHO001(ByVal sCodispl As String, ByVal sAction As String, ByVal nZip_Code As Integer, _
                                 ByVal nGeographicZone1 As Integer, ByVal nGeographicZone2 As Integer, ByVal nGeographicZone3 As Integer, _
                                 ByVal nSeismicZone As Integer, ByVal nDeduSeismicZone As Double, ByVal nCoasSeismicZone As Double, _
                                 ByVal nZoneType As Integer, ByVal nDeduZoneType As Double, ByVal nCoasZoneType As Double, _
                                 ByVal sStatRegt As String, ByVal nUsercode As Integer) As String

        Dim lclsErrors As eFunctions.Errors
        Dim lclsTabSeismicFloodZone As eBranches.TabSeismicFloodZone

        
        lclsErrors = New eFunctions.Errors
        lclsTabSeismicFloodZone = New eBranches.TabSeismicFloodZone

        '**+Validations related to column: zip code
        '+ Se valida la columna: Código postal.
        If nZip_Code <= 0 Then
            Call lclsErrors.ErrorMessage(sCodispl, 1037)
        Else
            If sAction = "Add" And lclsTabSeismicFloodZone.Find(nZip_Code) Then
                Call lclsErrors.ErrorMessage(sCodispl, 90196)
            End If
        End If

        '**+Validations related to column: 
        '+ Se valida la columna: 
        If nGeographicZone1 <= 0 Then
            Call lclsErrors.ErrorMessage(sCodispl, 12004)
        End If

        '**+Validations related to column: 
        '+ Se valida la columna: 
        If nGeographicZone2 <= 0 Then
            Call lclsErrors.ErrorMessage(sCodispl, 10100)
        End If

        '**+Validations related to column: 
        '+ Se valida la columna: 
        If nGeographicZone3 <= 0 Then
            Call lclsErrors.ErrorMessage(sCodispl, 90070)
        End If

        '**+Validations related to column: 
        '+ Se valida la columna: 
        If nSeismicZone <= 0 Then
            Call lclsErrors.ErrorMessage(sCodispl, 767046)
        End If

        '**+Validations related to column: 
        '+ Se valida la columna: 
        If nDeduSeismicZone <= 0 Then
            Call lclsErrors.ErrorMessage(sCodispl, 767047)
        End If

        '**+Validations related to column: 
        '+ Se valida la columna: 
        If nCoasSeismicZone <= 0 Then
            Call lclsErrors.ErrorMessage(sCodispl, 767048)
        End If

        '**+Validations related to column: 
        '+ Se valida la columna: 
        If nZoneType <= 0 Then
            Call lclsErrors.ErrorMessage(sCodispl, 767049)
        End If

        '**+Validations related to column: 
        '+ Se valida la columna: 
        If nDeduZoneType <= 0 Then
            Call lclsErrors.ErrorMessage(sCodispl, 767050)
        End If

        '**+Validations related to column: 
        '+ Se valida la columna: 
        If nCoasZoneType <= 0 Then
            Call lclsErrors.ErrorMessage(sCodispl, 767051)
        End If

        '**+Validations related to column: Status.
        '+ Se valida la columna: Estado.
        If sStatRegt = "0" Or sStatRegt = strNull Then
            Call lclsErrors.ErrorMessage(sCodispl, 9089)
        End If

        insValMHO001 = lclsErrors.Confirm

        lclsErrors = Nothing
        lclsTabSeismicFloodZone = Nothing

        Exit Function
    End Function

    '*** insPostMHO001: create/update corresponding data in the TabSeismicFloodZone table
    '*insPostMHO001: Esta función se encarga de crear/actualizar los registros
    '*correspondientes en la tabla TabSeismicFloodZone
    Public Function insPostMHO001(ByVal sAction As String, ByVal nZip_Code As Integer, ByVal nGeographicZone1 As Integer, _
                                  ByVal nGeographicZone2 As Integer, ByVal nGeographicZone3 As Integer, ByVal nSeismicZone As Integer, _
                                  ByVal nDeduSeismicZone As Double, ByVal nCoasSeismicZone As Double, ByVal nZoneType As Integer, _
                                  ByVal nDeduZoneType As Double, ByVal nCoasZoneType As Double, ByVal sStatRegt As String, _
                                  ByVal nUsercode As Integer) As Boolean


        Me.nZip_Code = nZip_Code
        Me.nGeographicZone1 = nGeographicZone1
        Me.nGeographicZone2 = nGeographicZone2
        Me.nGeographicZone3 = nGeographicZone3
        Me.nSeismicZone = nSeismicZone
        Me.nDeduSeismicZone = nDeduSeismicZone
        Me.nCoasSeismicZone = nCoasSeismicZone
        Me.nZoneType = nZoneType
        Me.nDeduZoneType = nDeduZoneType
        Me.nCoasZoneType = nCoasZoneType
        Me.sStatRegt = sStatRegt
        Me.nUsercode = nUsercode


        insPostMHO001 = True

        Select Case sAction

            '**+ If the selected option exists
            '+Si la opción seleccionada es Registrar

            Case "Add"
                insPostMHO001 = Add()

                '**+  If the selected option is Modify
                '+Si la opción seleccionada es Modificar

            Case "Update"
                insPostMHO001 = Update()

                '**+ If the selected option is Delete
                '+Si la opción seleccionada es Eliminar

            Case "Del"
                insPostMHO001 = Delete()
        End Select

        Exit Function
    End Function
End Class





