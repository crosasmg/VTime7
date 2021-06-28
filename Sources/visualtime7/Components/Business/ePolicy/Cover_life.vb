Option Strict Off
Option Explicit On
Public Class Cover_life
	'%-------------------------------------------------------%'
	'% $Workfile:: Cover_life.cls                           $%'
	'% $Author:: Nvaplat41                                  $%'
	'% $Date:: 9/10/03 19.01                                $%'
	'% $Revision:: 8                                        $%'
	'%-------------------------------------------------------%'
	
	'**-Propertied according the table in the system on 03/19/2001
	'-Propiedades según la tabla en el sistema el 19/03/2001
	
	'  Column_name              Type                       Computed  Length  Prec  Scale  Nullable  TrimTrailingBlanks  FixedLenNullInSource
	'  -----------------------  -------------------------  --------  ------  ----  -----  --------  ------------------  ------------------------
	Public sCertype As String ' char      no        1                    no        no                  no
	Public nBranch As Integer ' smallint  no        2       5     0      no        (n/a)               (n/a)
	Public nProduct As Integer ' smallint  no        2       5     0      no        (n/a)               (n/a)
	Public nPolicy As Double ' int       no        4       10    0      no        (n/a)               (n/a)
	Public nCertif As Double ' int       no        4       10    0      no        (n/a)               (n/a)
	Public nGroup_insu As Integer ' smallint  no        2       5     0      no        (n/a)               (n/a)
	Public nModulec As Integer ' smallint  no        2       5     0      no        (n/a)               (n/a)
	Public nCover As Integer ' smallint  no        2       5     0      no        (n/a)               (n/a)
	Public dEffecdate As Date ' datetime  no        8                    no        (n/a)               (n/a)
	Public nAge As Integer ' smallint  no        2       5     0      yes       (n/a)               (n/a)
	Public nCapital As Double ' decimal   no        9       12    0      yes       (n/a)               (n/a)
	Public dCompdate As Date ' datetime  no        8                    yes       (n/a)               (n/a)
	Public dNulldate As Date ' datetime  no        8                    yes       (n/a)               (n/a)
	Public nPremium As Double ' decimal   no        9       10    2      yes       (n/a)               (n/a)
	Public nType As Integer ' smallint  no        2       5     0      yes       (n/a)               (n/a)
	Public nUsercode As Integer ' smallint  no        2       5     0      yes       (n/a)               (n/a)
	Public nTransac As Integer ' int       no        4       10    0      yes       (n/a)               (n/a)
	
	'**%Find: This method returns TRUE or FALSE depending if the records exists in the table "Cover_life"
	'%Find: Este metodo retorna VERDADERO o FALSO dependiendo de la existencia o no de registros en la
	'%tabla "Cover_life"
    Public Function Find(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal nCover As Integer, Optional ByVal lblnFind As Boolean = False) As Boolean

        Dim lrecreaCover_Life As eRemoteDB.Execute

        lrecreaCover_Life = New eRemoteDB.Execute

        On Error GoTo Find_Err

        '**+Stored procedure parameters definition 'insudb.reaCover_Life'
        '**+Data of 03/19/2001 12:19:07 p.m.
        '+Definición de parámetros para stored procedure 'insudb.reaCover_Life'
        '+Información leída el 19/03/2001 12:19:07 p.m.

        With lrecreaCover_Life
            .StoredProcedure = "reaCover_Life"
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCover", nCover, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nType", 1, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If .Run Then
                sCertype = .FieldToClass("sCertype")
                nBranch = .FieldToClass("nBranch")
                nProduct = .FieldToClass("nProduct")
                nPolicy = .FieldToClass("nPolicy")
                nCertif = .FieldToClass("nCertif")
                nGroup_insu = .FieldToClass("nGroup_insu")
                nModulec = .FieldToClass("nModulec")
                nCover = .FieldToClass("nCover")
                dEffecdate = .FieldToClass("dEffecdate")
                nAge = .FieldToClass("nAge")
                nCapital = .FieldToClass("nCapital")
                dCompdate = .FieldToClass("dCompdate")
                dNulldate = .FieldToClass("dNulldate")
                nPremium = .FieldToClass("nPremium")
                nType = .FieldToClass("nType")
                nUsercode = .FieldToClass("nUsercode")
                nTransac = .FieldToClass("nTransac")
                .RCloseRec()
            End If
        End With
        'UPGRADE_NOTE: Object lrecreaCover_Life may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecreaCover_Life = Nothing

Find_Err:
        If Err.Number Then
            Find = False
        End If
        On Error GoTo 0
    End Function
	
	'**%insCreCover_life: This function adds a record in the "aditional information of coverages" table associated
	'**%to each coverage that has suffered a partial rescue
	'%insCreCover_life: Esta función se encarga de agregar un registro en la tabla de
	'%información adicional de la cobertura asociado a cada cobertura a la que
	'%se le ha realizado un rescate parcial.
    Public Function insCreCover_life(ByVal lintCover As Integer, ByVal ldblCapital As Double, ByVal ldtmEffecdate As Date) As Boolean

        Dim lreccreCover_life As eRemoteDB.Execute

        lreccreCover_life = New eRemoteDB.Execute

        On Error GoTo insCreCover_life_Err

        '**+Stored procedure parameters definition 'insudb.creCover_life'
        '**+Data of 01/21/2000 10:24:56
        '+Definición de parámetros para stored procedure 'insudb.creCover_life'
        '+Información leída el 21/01/2000 10:24:56

        With lreccreCover_life
            .StoredProcedure = "creCover_life"
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCover", lintCover, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", ldtmEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCapital", ldblCapital, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nType", 1, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If .Run(False) Then
                insCreCover_life = True
            End If
        End With
        'UPGRADE_NOTE: Object lreccreCover_life may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lreccreCover_life = Nothing

insCreCover_life_Err:
        If Err.Number Then
            insCreCover_life = False
        End If
        On Error GoTo 0
    End Function
End Class






