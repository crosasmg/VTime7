Imports Microsoft.VisualBasic

Imports System.Globalization
Imports eNetFrameWork
Imports eFunctions
Imports System.Data
Imports eFunctions.Values
Imports eRemoteDB.Parameter
Imports eProduct
Imports ePolicy


Public Class ctrlEquiposElectronicos
    Inherits System.Web.UI.UserControl

    Public mObjValues As New eFunctions.Values

    Public mObjGridEquipElect As eFunctions.Grid

    Public resxValues As IEnumerable(Of DictionaryEntry) = eFunctions.Values.GetResxValue("MU700")

    Public Sub insDefineHeader_EquipElec()
        mObjGridEquipElect = New eFunctions.Grid
        mObjGridEquipElect.sArrayName = "marrArray_EquipElect"

        With mObjGridEquipElect.Columns

            .AddPossiblesColumn(Id:=0, Title:=resxValues.FindDictionaryValue("NSECTION_EquipElect_Caption"), FieldName:="NSECTION_EquipElect", TableName:="TABLE7220", ValuesType:=eValuesType.clngWindowType, DefValue:="", NeedParam:=False, ComboSize:="1", OnChange:="InputOnChange(this)", Disabled:=False, MaxLength:=5, Alias_Renamed:=resxValues.FindDictionaryValue("NSECTION_EquipElect_ToolTip"), CodeType:=eTypeCode.eNumeric, bAllowInvalid:=False, ShowDescript:=True, Descript:="", NotCache:=False, KeyField:="")
            .AddPossiblesColumn(Id:=0, Title:=resxValues.FindDictionaryValue("NTYPE_EquipElect_Caption"), FieldName:="NTYPE_EquipElect", TableName:="TABLE7211", ValuesType:=eValuesType.clngWindowType, DefValue:="", NeedParam:=False, ComboSize:="1", OnChange:="InputOnChange(this)", Disabled:=False, MaxLength:=5, Alias_Renamed:=resxValues.FindDictionaryValue("NTYPE_EquipElect_ToolTip"), CodeType:=eTypeCode.eNumeric, bAllowInvalid:=False, ShowDescript:=True, Descript:="", NotCache:=False, KeyField:="")
            .AddTextColumn(Id:=0, Title:=resxValues.FindDictionaryValue("SDESCRIPTION_EquipElect_Caption"), FieldName:="SDESCRIPTION_EquipElect", Length:=30, DefValue:="", isRequired:=False, Alias_Renamed:=resxValues.FindDictionaryValue("SDESCRIPTION_EquipElect_ToolTip"), OnChange:="InputOnChange(this)", Disabled:=False)
            .AddNumericColumn(Id:=0, Title:=resxValues.FindDictionaryValue("NCAPITAL_EquipElect_Caption"), FieldName:="NCAPITAL_EquipElect", Length:=18, DefValue:="", isRequired:=False, Alias_Renamed:=resxValues.FindDictionaryValue("NCAPITAL_EquipElect_ToolTip"), ShowThousand:=True, DecimalPlaces:=2, OnChange:="InputOnChangeEquipElect(this)", Disabled:=False, bAllowNegativ:=False)
            .AddNumericColumn(Id:=0, Title:=resxValues.FindDictionaryValue("NRATE_EquipElect_Caption"), FieldName:="NRATE_EquipElect", Length:=9, DefValue:="", isRequired:=False, Alias_Renamed:=resxValues.FindDictionaryValue("NRATE_EquipElect_ToolTip"), ShowThousand:=True, DecimalPlaces:=6, OnChange:="InputOnChangeEquipElect(this)", Disabled:=False, bAllowNegativ:=False)
            .AddNumericColumn(Id:=0, Title:=resxValues.FindDictionaryValue("NPREMIUM_EquipElect_Caption"), FieldName:="NPREMIUM_EquipElect", Length:=18, DefValue:="", isRequired:=False, Alias_Renamed:=resxValues.FindDictionaryValue("NPREMIUM_EquipElect_ToolTip"), ShowThousand:=True, DecimalPlaces:=6, OnChange:="InputOnChangeEquipElect(this)", Disabled:=True, bAllowNegativ:=False)

            .AddHiddenColumn("nConsec_EquipElect", "0")
            '.AddNumericColumn(Id:=0, Title:="", FieldName:="nConsec_EquipElect", Length:=18, DefValue:="0", isRequired:=False, Alias_Renamed:=resxValues.FindDictionaryValue("NCAPITAL_EquipElect_ToolTip"), ShowThousand:=False, DecimalPlaces:=2, OnChange:="InputOnChange(this)", Disabled:=False, bAllowNegativ:=False)

        End With

        With mObjGridEquipElect
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
            .Columns("NSECTION_EquipElect").EditRecord = True
            '.Columns("NTYPE_EquipElect").Disabled = Request.QueryString.Item("Action") = "Update"
            '.Columns("DEFFECTDATE").Disabled = Request.QueryString.Item("Action") = "Update"
                    	 	'.sDelRecordParam = "NCONSEC_EquipElect=' + marrArray[lintIndex].NCONSEC_EquipElect + '" & "&SCERTYPE=' + marrArray[lintIndex].SCERTYPE + '" & "&cbeBranch=' + marrArray[lintIndex].cbeBranch + '" & "&valProduct=' + marrArray[lintIndex].valProduct + '" & "&NPOLICY=' + marrArray[lintIndex].NPOLICY + '" & "&NCERTIF=' + marrArray[lintIndex].NCERTIF + '" & "&DEFFECTDATE=' + marrArray[lintIndex].DEFFECTDATE + '"

            If Request.QueryString.Item("Reload") = "1" Then
                .sReloadIndex = Request.QueryString.Item("ReloadIndex")
            End If

            .AddButton = True
            .DeleteButton = True
            .Columns("Sel").GridVisible = .DeleteButton
            .sEditRecordParam = "&gridName=EquipElect" 

            .sDelRecordParam = "gridName=EquipElect" & _
                              "&SCERTYPE=" & Session("SCERTYPE") & _
                              "&cbeBranch=" & session("nBranch") & _ 
                              "&valProduct=" & session("nProduct") & _
                              "&NPOLICY=" & session("NPOLICY") & _
                              "&NCERTIF=" & session("NCERTIF") & _
                              "&DEFFECTDATE=" & session("DEFFECDATE") & _
                              "&NCONSEC_EquipElect=' + marrArray_EquipElect[lintIndex].nConsec_EquipElect + '" & _
                              "&NTYPE_EquipElect=3"

        End With

    End Sub
    Public Sub insPreMU700_EquipElect()
        With Request
            Dim rdb As New eRemoteDB.Execute(True)

            rdb.SQL = "SELECT  MULTIRISK_DET.NCONSEC, MULTIRISK_DET.SCERTYPE, MULTIRISK_DET.NBRANCH, MULTIRISK_DET.NPRODUCT, MULTIRISK_DET.NPOLICY, MULTIRISK_DET.NCERTIF, MULTIRISK_DET.NTYPE, MULTIRISK_DET.DEFFECTDATE, MULTIRISK_DET.NSECTION, MULTIRISK_DET.SDESCRIPTION, MULTIRISK_DET.NCAPITAL, MULTIRISK_DET.DCOMPDATE, MULTIRISK_DET.NUSERCODE, MULTIRISK_DET.DNULLDATE, MULTIRISK_DET.NELEMENT_TYPE, MULTIRISK_DET.NRATE, MULTIRISK_DET.NPREMIUM FROM insudb.MULTIRISK_DET MULTIRISK_DET WHERE " & _
            "SCERTYPE = '" & Session("scertype") & "'" & _
            " AND NBRANCH = " & Session("nbranch") & _
            " AND NPRODUCT =  " & Session("nproduct") & _
            " AND NPOLICY = " & Session("nPolicy") & _
            " AND NCERTIF = " & Session("nCertif") & _
            " AND NTYPE = 3 " & _
            " AND DEFFECTDATE      <= 	STDATE('" & Convert.ToDateTime(mObjValues.StringToType(Session("DEFFECDATE"), eTypeData.etdDate)).ToString("yyyyMMdd") & "')" & _
            " AND (DNULLDATE IS NULL " & _
            "OR DNULLDATE >  	STDATE('" & Convert.ToDateTime(mObjValues.StringToType(Session("DEFFECDATE"), eTypeData.etdDate)).ToString("yyyyMMdd") & "') ) "


            With mObjGridEquipElect
                If rdb.Run(True) Then
                    
                    Do While Not rdb.EOF
                        .Columns("nConsec_EquipElect").DefValue = rdb.FieldToClass("NCONSEC")
                        .Columns("NTYPE_EquipElect").DefValue = rdb.FieldToClass("NELEMENT_TYPE")
                        .Columns("NSECTION_EquipElect").DefValue = rdb.FieldToClass("NSECTION")
                        .Columns("SDESCRIPTION_EquipElect").DefValue = rdb.FieldToClass("SDESCRIPTION")
                        .Columns("NCAPITAL_EquipElect").DefValue = rdb.FieldToClass("NCAPITAL")
                        .Columns("NRATE_EquipElect").DefValue = rdb.FieldToClass("NRATE")
                        .Columns("NPREMIUM_EquipElect").DefValue = rdb.FieldToClass("NPREMIUM")
                        Response.Write(.DoRow)
                        rdb.RNext()
                    Loop
                    rdb.RCloseRec()
                End If

                Response.Write(.closeTable())
            End With
        End With
    End Sub
    Public Sub insPreMU700Upd_EquipElect()
        With Request
            If .QueryString.Item("Action") = "Del" Then
                Dim lblnPost As Boolean
                Dim lstrMessage As String = String.Empty
                Dim rdb As New eRemoteDB.Execute(True)


                If String.IsNullOrEmpty(lstrMessage) Then
                    rdb = New eRemoteDB.Execute(True)
                    rdb.SQL = "DELETE FROM INSUDB.MULTIRISK_DET WHERE NCONSEC = :NCONSEC AND SCERTYPE = :SCERTYPE AND NBRANCH = :NBRANCH AND NPRODUCT = :NPRODUCT AND NPOLICY = :NPOLICY AND NCERTIF = :NCERTIF AND NTYPE = :NTYPE AND DEFFECTDATE = :DEFFECTDATE "

                    rdb.Parameters.Add("NCONSEC", mObjValues.StringToType(.QueryString.Item("NCONSEC_EquipElect"), eFunctions.Values.eTypeData.etdLong), eRmtDataDir.rdbParamInput, eRmtDataType.rdbNumeric, 5, 0, 5, eRmtDataAttrib.rdbParamNullable)
                    rdb.Parameters.Add("SCERTYPE", .QueryString.Item("SCERTYPE"), eRmtDataDir.rdbParamInput, eRmtDataType.rdbCharFixedLength, 1, 0, 0, eRmtDataAttrib.rdbParamNullable)
                    rdb.Parameters.Add("NBRANCH", mObjValues.StringToType(.QueryString.Item("cbeBranch"), eFunctions.Values.eTypeData.etdLong), eRmtDataDir.rdbParamInput, eRmtDataType.rdbNumeric, 5, 0, 5, eRmtDataAttrib.rdbParamNullable)
                    rdb.Parameters.Add("NPRODUCT", mObjValues.StringToType(.QueryString.Item("valProduct"), eFunctions.Values.eTypeData.etdLong), eRmtDataDir.rdbParamInput, eRmtDataType.rdbNumeric, 5, 0, 5, eRmtDataAttrib.rdbParamNullable)
                    rdb.Parameters.Add("NPOLICY", mObjValues.StringToType(.QueryString.Item("NPOLICY"), eFunctions.Values.eTypeData.etdLong), eRmtDataDir.rdbParamInput, eRmtDataType.rdbNumeric, 10, 0, 10, eRmtDataAttrib.rdbParamNullable)
                    rdb.Parameters.Add("NCERTIF", mObjValues.StringToType(.QueryString.Item("NCERTIF"), eFunctions.Values.eTypeData.etdLong), eRmtDataDir.rdbParamInput, eRmtDataType.rdbNumeric, 10, 0, 10, eRmtDataAttrib.rdbParamNullable)
                    rdb.Parameters.Add("NTYPE", 3, eRmtDataDir.rdbParamInput, eRmtDataType.rdbNumeric, 5, 0, 5, eRmtDataAttrib.rdbParamNullable)
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
            'Response.Write(mObjGridEquipElect.DoFormUpd(.QueryString.Item("Action"), "valPolicySeq.aspx", .QueryString.Item("sCodispl"), .QueryString.Item("nMainAction"), mObjGridEquipElect.ActionQuery, CShort(.QueryString.Item("Index")),,"&fromDelete=1"))
            Response.Write(mObjGridEquipElect.DoFormUpd(.QueryString.Item("Action"), "valPolicySeq.aspx", .QueryString.Item("sCodispl"), .QueryString.Item("nMainAction"), mObjGridEquipElect.ActionQuery, CShort(.QueryString.Item("Index")), "&fromDelete=1"))
        End With
    End Sub

End Class


