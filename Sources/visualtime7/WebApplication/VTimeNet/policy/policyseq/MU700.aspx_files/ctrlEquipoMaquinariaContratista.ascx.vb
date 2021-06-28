Imports Microsoft.VisualBasic

Imports System.Globalization
Imports eNetFrameWork
Imports eFunctions
Imports System.Data
Imports eFunctions.Values
Imports eRemoteDB.Parameter
Imports eProduct
Imports ePolicy

Public Class ctrlEquipoMaquinariaContratista
    Inherits System.Web.UI.UserControl

    Public mObjValues As New eFunctions.Values

    Public mObjGridEquipMaquiContr As eFunctions.Grid

    Public resxValues As IEnumerable(Of DictionaryEntry) = eFunctions.Values.GetResxValue("MU700")

    Public Sub insDefineHeader_EquipMaquiContr()
        mObjGridEquipMaquiContr = New eFunctions.Grid
        mObjGridEquipMaquiContr.sArrayName = "marrArray_EquipMaquiContr"

        With mObjGridEquipMaquiContr.Columns

            .AddTextColumn(Id:=0, Title:=resxValues.FindDictionaryValue("STRADEMARK_EquipMaquiContr_Caption"), FieldName:="STRADEMARK_EquipMaquiContr", Length:=30, DefValue:="", isRequired:=False, Alias_Renamed:=resxValues.FindDictionaryValue("STRADEMARK_EquipMaquiContr_ToolTip"), OnChange:="InputOnChange(this)", Disabled:=False)
            .AddTextColumn(Id:=0, Title:=resxValues.FindDictionaryValue("SMODEL_EquipMaquiContr_Caption"), FieldName:="SMODEL_EquipMaquiContr", Length:=30, DefValue:="", isRequired:=False, Alias_Renamed:=resxValues.FindDictionaryValue("SMODEL_EquipMaquiContr_ToolTip"), OnChange:="InputOnChange(this)", Disabled:=False)

            .AddNumericColumn(Id:=0, Title:=resxValues.FindDictionaryValue("NYEAR_EquipMaquiContr_Caption"), FieldName:="NYEAR_EquipMaquiContr", Length:=4, DefValue:="", isRequired:=False, Alias_Renamed:=resxValues.FindDictionaryValue("NYEAR_EquipMaquiContr_ToolTip"), ShowThousand:=False, DecimalPlaces:=0, OnChange:="InputOnChange(this)", Disabled:=False, bAllowNegativ:=False)

            .AddTextColumn(Id:=0, Title:=resxValues.FindDictionaryValue("SORIGIN_EquipMaquiContr_Caption"), FieldName:="SORIGIN_EquipMaquiContr", Length:=30, DefValue:="", isRequired:=False, Alias_Renamed:=resxValues.FindDictionaryValue("SORIGIN_EquipMaquiContr_ToolTip"), OnChange:="InputOnChange(this)", Disabled:=False)
            .AddTextColumn(Id:=0, Title:=resxValues.FindDictionaryValue("SSERIALNUMBER_EquipMaquiContr_Caption"), FieldName:="SSERIALNUMBER_EquipMaquiContr", Length:=30, DefValue:="", isRequired:=False, Alias_Renamed:=resxValues.FindDictionaryValue("SSERIALNUMBER_EquipMaquiContr_ToolTip"), OnChange:="InputOnChange(this)", Disabled:=False)
            .AddNumericColumn(Id:=0, Title:=resxValues.FindDictionaryValue("NCAPITAL_EquipMaquiContr_Caption"), FieldName:="NCAPITAL_EquipMaquiContr", Length:=18, DefValue:="", isRequired:=False, Alias_Renamed:=resxValues.FindDictionaryValue("NCAPITAL_EquipMaquiContr_ToolTip"), ShowThousand:=True, DecimalPlaces:=2, OnChange:="InputOnChangeMaquiContract(this)", Disabled:=False, bAllowNegativ:=False)

            .AddNumericColumn(Id:=0, Title:=resxValues.FindDictionaryValue("NRATE_EquipMaquiContr_Caption"), FieldName:="NRATE_EquipMaquiContr", Length:=9, DefValue:="", isRequired:=False, Alias_Renamed:=resxValues.FindDictionaryValue("NRATE_EquipMaquiContr_ToolTip"), ShowThousand:=True, DecimalPlaces:=6, OnChange:="InputOnChangeMaquiContract(this)", Disabled:=False, bAllowNegativ:=False)

            .AddNumericColumn(Id:=0, Title:=resxValues.FindDictionaryValue("NPREMIUM_EquipMaquiContr_Caption"), FieldName:="NPREMIUM_EquipMaquiContr", Length:=18, DefValue:="", isRequired:=False, Alias_Renamed:=resxValues.FindDictionaryValue("NPREMIUM_EquipMaquiContr_ToolTip"), ShowThousand:=True, DecimalPlaces:=6, OnChange:="InputOnChange(this)", Disabled:=True, bAllowNegativ:=False)
            .AddHiddenColumn("NTYPE_EquipMaquiContr", "5")
            .AddHiddenColumn("nConsec_EquipMaquiContr", "0")

        End With

        With mObjGridEquipMaquiContr
            .nMainAction = CShort(IIf(Request.QueryString.Item("nMainAction") = "", 0, Request.QueryString.Item("nMainAction")))
            .Codispl = Request.QueryString.Item("sCodispl")
            .Codisp = "MU700"
            .Top = 100
            .Height = 320
            .Width = 450
            .WidthDelete = 480
            .bOnlyForQuery = Request.QueryString.Item("nMainAction") = "401"
            .ActionQuery = (Request.QueryString.Item("nMainAction") = "401" Or IsNothing(Request.QueryString.Item("nMainAction")))
            .Columns("Sel").GridVisible = Not .ActionQuery
            .Columns("STRADEMARK_EquipMaquiContr").EditRecord = True
            '.Columns("NTYPE_RotMaqui").Disabled = Request.QueryString.Item("Action") = "Update"
            '.Columns("DEFFECTDATE").Disabled = Request.QueryString.Item("Action") = "Update"
            '        	 	.sDelRecordParam = "NCONSEC_RotMaqui=' + marrArray[lintIndex].NCONSEC_RotMaqui + '" & "&SCERTYPE=' + marrArray[lintIndex].SCERTYPE + '" & "&cbeBranch=' + marrArray[lintIndex].cbeBranch + '" & "&valProduct=' + marrArray[lintIndex].valProduct + '" & "&NPOLICY=' + marrArray[lintIndex].NPOLICY + '" & "&NCERTIF=' + marrArray[lintIndex].NCERTIF + '" & "&DEFFECTDATE=' + marrArray[lintIndex].DEFFECTDATE + '"

            If Request.QueryString.Item("Reload") = "1" Then
                .sReloadIndex = Request.QueryString.Item("ReloadIndex")
            End If

            .AddButton = True
            .DeleteButton = True
            .Columns("Sel").GridVisible = .DeleteButton

            .sEditRecordParam = "&gridName=EquipMaquiContr"

            .sDelRecordParam = "gridName=EquipMaquiContr" & _
                              "&SCERTYPE=" & Session("SCERTYPE") & _
                              "&cbeBranch=" & Session("nBranch") & _
                              "&valProduct=" & Session("nProduct") & _
                              "&NPOLICY=" & Session("NPOLICY") & _
                              "&NCERTIF=" & Session("NCERTIF") & _
                              "&DEFFECTDATE=" & Session("DEFFECDATE") & _
                              "&NCONSEC_EquipMaquiContr=' + marrArray_EquipMaquiContr[lintIndex].nConsec_EquipMaquiContr + '" & _
                              "&NTYPE_EquipMaquiContr=5"
            

        End With
    End Sub
    Public Sub insPreMU700_EquipMaquiContr()
        With Request
            Dim rdb As New eRemoteDB.Execute(True)

            rdb.SQL = "SELECT  MULTIRISK_DET.NCONSEC, MULTIRISK_DET.SCERTYPE, MULTIRISK_DET.NBRANCH, MULTIRISK_DET.NPRODUCT, MULTIRISK_DET.NPOLICY, MULTIRISK_DET.NCERTIF, MULTIRISK_DET.NTYPE, MULTIRISK_DET.DEFFECTDATE, MULTIRISK_DET.NSECTION, MULTIRISK_DET.SDESCRIPTION, MULTIRISK_DET.NCAPITAL, MULTIRISK_DET.STRADEMARK, MULTIRISK_DET.SMODEL, MULTIRISK_DET.NYEAR, MULTIRISK_DET.SORIGIN, MULTIRISK_DET.SSERIALNUMBER, MULTIRISK_DET.NRATE, MULTIRISK_DET.NPREMIUM, MULTIRISK_DET.DCOMPDATE, MULTIRISK_DET.NUSERCODE, MULTIRISK_DET.NELEMENT_TYPE, MULTIRISK_DET.DNULLDATE FROM insudb.MULTIRISK_DET MULTIRISK_DET WHERE " & _
            " SCERTYPE = '" & Session("scertype") & "'" & _
            " AND NBRANCH = " & Session("nbranch") & _
            " AND NPRODUCT =  " & Session("nproduct") & _
            " AND NPOLICY = " & Session("nPolicy") & _
            " AND NCERTIF = " & Session("nCertif") & _
            " AND NTYPE = 5 " & _
            " AND DEFFECTDATE      <= 	STDATE('" & Convert.ToDateTime(mObjValues.StringToType(Session("DEFFECDATE"), eTypeData.etdDate)).ToString("yyyyMMdd") & "')" & _
            " AND (DNULLDATE IS NULL " & _
            "OR DNULLDATE >  	STDATE('" & Convert.ToDateTime(mObjValues.StringToType(Session("DEFFECDATE"), eTypeData.etdDate)).ToString("yyyyMMdd") & "') ) "



            With mObjGridEquipMaquiContr
                If rdb.Run(True) Then
                    Do While Not rdb.EOF
                        .Columns("STRADEMARK_EquipMaquiContr").DefValue = rdb.FieldToClass("STRADEMARK")
                        .Columns("SMODEL_EquipMaquiContr").DefValue = rdb.FieldToClass("SMODEL")
                        .Columns("NYEAR_EquipMaquiContr").DefValue = rdb.FieldToClass("NYEAR")
                        .Columns("SORIGIN_EquipMaquiContr").DefValue = rdb.FieldToClass("SORIGIN")
                        .Columns("SSERIALNUMBER_EquipMaquiContr").DefValue = rdb.FieldToClass("SSERIALNUMBER")
                        .Columns("NRATE_EquipMaquiContr").DefValue = rdb.FieldToClass("NRATE")
                        .Columns("NPREMIUM_EquipMaquiContr").DefValue = rdb.FieldToClass("NPREMIUM")
                        .Columns("nConsec_EquipMaquiContr").DefValue = rdb.FieldToClass("NCONSEC")
                        .Columns("NCAPITAL_EquipMaquiContr").DefValue = rdb.FieldToClass("NCAPITAL")
                        Response.Write(.DoRow)
                        rdb.RNext()
                    Loop
                    rdb.RCloseRec()
                End If
                Response.Write(.closeTable())
            End With
        End With
    End Sub
    Public Sub insPreMU700Upd_EquipMaquiContr()
        With Request
            If .QueryString.Item("Action") = "Del" Then
                Dim lblnPost As Boolean
                Dim lstrMessage As String = String.Empty
                Dim rdb As New eRemoteDB.Execute(True)


                If String.IsNullOrEmpty(lstrMessage) Then
                    rdb = New eRemoteDB.Execute(True)
                    rdb.SQL = "DELETE FROM INSUDB.MULTIRISK_DET WHERE NCONSEC = :NCONSEC AND SCERTYPE = :SCERTYPE AND NBRANCH = :NBRANCH AND NPRODUCT = :NPRODUCT AND NPOLICY = :NPOLICY AND NCERTIF = :NCERTIF AND NTYPE = :NTYPE AND DEFFECTDATE = :DEFFECTDATE "

                    rdb.Parameters.Add("NCONSEC", mObjValues.StringToType(.QueryString.Item("NCONSEC_EquipMaquiContr"), eFunctions.Values.eTypeData.etdLong), eRmtDataDir.rdbParamInput, eRmtDataType.rdbNumeric, 5, 0, 5, eRmtDataAttrib.rdbParamNullable)
                    rdb.Parameters.Add("SCERTYPE", .QueryString.Item("SCERTYPE"), eRmtDataDir.rdbParamInput, eRmtDataType.rdbCharFixedLength, 1, 0, 0, eRmtDataAttrib.rdbParamNullable)
                    rdb.Parameters.Add("NBRANCH", mObjValues.StringToType(.QueryString.Item("cbeBranch"), eFunctions.Values.eTypeData.etdLong), eRmtDataDir.rdbParamInput, eRmtDataType.rdbNumeric, 5, 0, 5, eRmtDataAttrib.rdbParamNullable)
                    rdb.Parameters.Add("NPRODUCT", mObjValues.StringToType(.QueryString.Item("valProduct"), eFunctions.Values.eTypeData.etdLong), eRmtDataDir.rdbParamInput, eRmtDataType.rdbNumeric, 5, 0, 5, eRmtDataAttrib.rdbParamNullable)
                    rdb.Parameters.Add("NPOLICY", mObjValues.StringToType(.QueryString.Item("NPOLICY"), eFunctions.Values.eTypeData.etdLong), eRmtDataDir.rdbParamInput, eRmtDataType.rdbNumeric, 10, 0, 10, eRmtDataAttrib.rdbParamNullable)
                    rdb.Parameters.Add("NCERTIF", mObjValues.StringToType(.QueryString.Item("NCERTIF"), eFunctions.Values.eTypeData.etdLong), eRmtDataDir.rdbParamInput, eRmtDataType.rdbNumeric, 10, 0, 10, eRmtDataAttrib.rdbParamNullable)
                    rdb.Parameters.Add("NTYPE", mObjValues.StringToType(.QueryString.Item("NTYPE_EquipMaquiContr"), eFunctions.Values.eTypeData.etdLong), eRmtDataDir.rdbParamInput, eRmtDataType.rdbNumeric, 5, 0, 5, eRmtDataAttrib.rdbParamNullable)
                    rdb.Parameters.Add("DEFFECTDATE", mObjValues.StringToType(.QueryString.Item("DEFFECTDATE"), eFunctions.Values.eTypeData.etdDate), eRmtDataDir.rdbParamInput, eRmtDataType.rdbDate, 7, 0, 0, eRmtDataAttrib.rdbParamNullable)


                    lblnPost = rdb.Run(False)
                    If lblnPost Then
                       Dim rdbPolicy_win As new eRemoteDB.Execute()
                        rdbPolicy_win.StoredProcedure = "INSUPDPOLICY_WIN_ARR"
                        rdbPolicy_win.Parameters.Add("SCERTYPE", .QueryString.Item("SCERTYPE"), eRmtDataDir.rdbParamInput, eRmtDataType.rdbCharFixedLength, 1, 0, 0, eRmtDataAttrib.rdbParamNullable)
                        rdbPolicy_win.Parameters.Add("NBRANCH", mObjValues.StringToType(.QueryString.Item("cbeBranch"), eFunctions.Values.eTypeData.etdLong), eRmtDataDir.rdbParamInput, eRmtDataType.rdbNumeric, 5, 0, 5, eRmtDataAttrib.rdbParamNullable)
                        rdbPolicy_win.Parameters.Add("NPRODUCT", mObjValues.StringToType(.QueryString.Item("valProduct"), eFunctions.Values.eTypeData.etdLong), eRmtDataDir.rdbParamInput, eRmtDataType.rdbNumeric, 5, 0, 5, eRmtDataAttrib.rdbParamNullable)
                        rdbPolicy_win.Parameters.Add("NPOLICY", mObjValues.StringToType(.QueryString.Item("NPOLICY"), eFunctions.Values.eTypeData.etdLong), eRmtDataDir.rdbParamInput, eRmtDataType.rdbNumeric, 10, 0, 10, eRmtDataAttrib.rdbParamNullable)
                        rdbPolicy_win.Parameters.Add("NCERTIF", mObjValues.StringToType(.QueryString.Item("NCERTIF"), eFunctions.Values.eTypeData.etdLong), eRmtDataDir.rdbParamInput, eRmtDataType.rdbNumeric, 5, 0, 5, eRmtDataAttrib.rdbParamNullable)
                        rdbPolicy_win.Parameters.Add("DEFFECDATE", mObjValues.StringToType(.QueryString.Item("DEFFECtDATE"), eFunctions.Values.eTypeData.etdDate), eRmtDataDir.rdbParamInput, eRmtDataType.rdbDate, 7, 0, 0, eRmtDataAttrib.rdbParamNullable)
                        rdbPolicy_win.Parameters.Add("SWINDOW", "MU700|1", eRmtDataDir.rdbParamInput, eRmtDataType.rdbVarchar, 200, 0, 0, eRmtDataAttrib.rdbParamNullable)
                        rdbPolicy_win.Parameters.Add("NUSERCODE", mObjValues.StringToType(Session("nUserCode"), eFunctions.Values.eTypeData.etdLong), eRmtDataDir.rdbParamInput, eRmtDataType.rdbNumeric, 5, 0, 5, eRmtDataAttrib.rdbParamNullable)

                        rdbPolicy_win.Run(False)

                    End If
                    Response.Write(mObjValues.ConfirmDelete())
                Else
                    Response.Write(lstrMessage)
                End If
            End If
            Response.Write("<script>top.opener.top.frames['fraSequence'].document.location='/VTimeNet/Policy/PolicySeq/Sequence.aspx?nAction=" & Request.QueryString.Item("nMainAction") & "&nOpener=" & .QueryString.Item("sCodispl") & "&sGoToNext=NO" & "';</" & "Script>")
            'Response.Write(mObjGridEquipMaquiContr.DoFormUpd(.QueryString.Item("Action"), "valPolicySeq.aspx", .QueryString.Item("sCodispl"), .QueryString.Item("nMainAction"), mObjGridEquipMaquiContr.ActionQuery, CShort(.QueryString.Item("Index")),,"&fromDelete=1"))
            Response.Write(mObjGridEquipMaquiContr.DoFormUpd(.QueryString.Item("Action"), "valPolicySeq.aspx", .QueryString.Item("sCodispl"), .QueryString.Item("nMainAction"), mObjGridEquipMaquiContr.ActionQuery, CShort(.QueryString.Item("Index")), "&fromDelete=1"))
        End With
    End Sub

End Class
