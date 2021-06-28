Imports Microsoft.VisualBasic

Imports System.Globalization
Imports eNetFrameWork
Imports eFunctions
Imports System.Data
Imports eFunctions.Values
Imports eRemoteDB.Parameter
Imports eProduct
Imports ePolicy

Public Class ctrlFidelidad
    Inherits System.Web.UI.UserControl

    Public mObjValues As New eFunctions.Values

    Public mObjGridFidelity As eFunctions.Grid

    Public mObjPuntualFidelity As Object

    Public resxValues As IEnumerable(Of DictionaryEntry) = eFunctions.Values.GetResxValue("MU700")

    Public Sub insDefineHeader_Fidelity()
        mObjGridFidelity = New eFunctions.Grid

        mObjGridFidelity.sArrayName = "marrArray_Fidelity"

        mObjGridFidelity.sCodisplPage = Request.QueryString.Item("sCodispl")

        '+Se definen todas las columnas del Grid
        With mObjGridFidelity.Columns

            '.AddTextColumn(Id:=0, Title:=resxValues.FindDictionaryValue("SCLIENT_Fidelity_Caption"), Length:=14, FieldName:="SCLIENT_Fidelity", DefValue:="", isRequired:=True, Alias_Renamed:=resxValues.FindDictionaryValue("SCLIENT_Fidelity_ToolTip"), OnChange:="InputOnChange(this)", Disabled:=False, TabIndex:=0)
            .AddClientColumn(Id:=0, Title:=resxValues.FindDictionaryValue("SCLIENT_Fidelity_Caption"), FieldName:="SCLIENT_Fidelity", DefValue:="", isRequired:=True, Alias_Renamed:=resxValues.FindDictionaryValue("SCLIENT_Fidelity_ToolTip"), OnChange:="insShowDefValue_Fidelity()", Disabled:=False, TabIndex:=0, bAllowInvalid:=True)
            .AddTextColumn(Id:=0, Title:=resxValues.FindDictionaryValue("SFIRSTNAME_Fidelity_Caption"), FieldName:="SFIRSTNAME_Fidelity", Length:=20, DefValue:=" ", isRequired:=True, Alias_Renamed:=resxValues.FindDictionaryValue("SFIRSTNAME_Fidelity_ToolTip"), OnChange:="InputOnChange(this)", Disabled:=False)
            .AddTextColumn(Id:=0, Title:=resxValues.FindDictionaryValue("SMIDDLENAME_Fidelity_Caption"), FieldName:="SMIDDLENAME_Fidelity", Length:=20, DefValue:="", isRequired:=True, Alias_Renamed:=resxValues.FindDictionaryValue("SMIDDLENAME_Fidelity_ToolTip"), OnChange:="InputOnChange(this)", Disabled:=False)
            .AddTextColumn(Id:=0, Title:=resxValues.FindDictionaryValue("SLASTNAME_Fidelity_Caption"), FieldName:="SLASTNAME_Fidelity", Length:=20, DefValue:="", isRequired:=True, Alias_Renamed:=resxValues.FindDictionaryValue("SLASTNAME_Fidelity_ToolTip"), OnChange:="InputOnChange(this)", Disabled:=False)
            .AddTextColumn(Id:=0, Title:=resxValues.FindDictionaryValue("SLASTNAME2_Fidelity_Caption"), FieldName:="SLASTNAME2_Fidelity", Length:=20, DefValue:="", isRequired:=True, Alias_Renamed:=resxValues.FindDictionaryValue("SLASTNAME2_Fidelity_ToolTip"), OnChange:="InputOnChange(this)", Disabled:=False)
            .AddPossiblesColumn(Id:=0, Title:=resxValues.FindDictionaryValue("NPOSITION_Fidelity_Caption"), FieldName:="NPOSITION_Fidelity", TableName:="TABTABLE283", ValuesType:=eValuesType.clngWindowType, DefValue:="", NeedParam:=False, ComboSize:="1", OnChange:="InputOnChangeNpos(this)", Disabled:=False, MaxLength:=5, Alias_Renamed:=resxValues.FindDictionaryValue("NPOSITION_Fidelity_ToolTip"), CodeType:=eTypeCode.eNumeric, bAllowInvalid:=False, ShowDescript:=True, Descript:="", NotCache:=False, KeyField:="")
            .AddNumericColumn(Id:=0, Title:=resxValues.FindDictionaryValue("NSALARY_Fidelity_Caption"), FieldName:="NSALARY_Fidelity", Length:=18, DefValue:="", isRequired:=True, Alias_Renamed:=resxValues.FindDictionaryValue("NSALARY_Fidelity_ToolTip"), ShowThousand:=False, DecimalPlaces:=6, OnChange:=" InputOnChange(this)", Disabled:=False, bAllowNegativ:=False)
            .AddNumericColumn(Id:=0, Title:=resxValues.FindDictionaryValue("NFACTOR_Fidelity_Caption"), FieldName:="NFACTOR_Fidelity", Length:=6, DefValue:="", isRequired:=True, Alias_Renamed:=resxValues.FindDictionaryValue("NFACTOR_Fidelity_ToolTip"), ShowThousand:=False, DecimalPlaces:=3, OnChange:="InputOnChange(this)", Disabled:=True, bAllowNegativ:=False)
            .AddNumericColumn(Id:=0, Title:=resxValues.FindDictionaryValue("NVALUE_Fidelity_Caption"), FieldName:="NVALUE_Fidelity", Length:=18, DefValue:="", isRequired:=True, Alias_Renamed:=resxValues.FindDictionaryValue("NVALUE_Fidelity_ToolTip"), ShowThousand:=False, DecimalPlaces:=6, OnChange:="InputOnChange(this)", Disabled:=False, bAllowNegativ:=False)
            .AddHiddenColumn("NTYPE_Fidelity", "6")

        End With

        With mObjGridFidelity
            .nMainAction = CShort(IIf(Request.QueryString.Item("nMainAction") = "", 0, Request.QueryString.Item("nMainAction")))
            .Codispl = Request.QueryString.Item("sCodispl")
            .Codisp = "MU700_k"
            .Top = 100
            .Height = 300
            .Width = 650
            .WidthDelete = 480
            .bOnlyForQuery = Request.QueryString.Item("nMainAction") = "401"
            .ActionQuery = (Request.QueryString.Item("nMainAction") = "401" Or IsNothing(Request.QueryString.Item("nMainAction")))
            .Columns("Sel").GridVisible = Not .ActionQuery
            .Columns("SCLIENT_Fidelity").EditRecord = True
            If Request.QueryString.Item("Action") = "Update" Then
                .Columns("SCLIENT_Fidelity").Disabled = Request.QueryString.Item("Action") = "Update"

            End If
            .Columns("NPOSITION_Fidelity").Parameters.ReturnValue("NFACTOR", , , True)

            If Request.QueryString.Item("Reload") = "1" Then
                .sReloadIndex = Request.QueryString.Item("ReloadIndex")
            End If

            .AddButton = True
            .DeleteButton = True
            .Columns("Sel").GridVisible = .DeleteButton
            .sEditRecordParam = "&gridName=Fidelity" & "&nFI_POLICYTYPE=' + self.document.forms[0].tcnFI_POLICYTYPE.value + '"
            .sDelRecordParam = "gridName=Fidelity" & _
                              "&SCERTYPE=" & Session("SCERTYPE") & _
                              "&cbeBranch=" & Session("nBranch") & _
                              "&valProduct=" & Session("nProduct") & _
                              "&NPOLICY=" & Session("NPOLICY") & _
                              "&NCERTIF=" & Session("NCERTIF") & _
                              "&DEFFECTDATE=" & Session("DEFFECDATE") & _
                              "&SCLIENT_Fidelity=' + marrArray_Fidelity[lintIndex].SCLIENT_Fidelity + '" & _
                              "&NTYPE_Fidelity=6"

        End With
    End Sub
    Public Sub insPreMU700_Fidelity()
        With Request
            Dim rdb As New eRemoteDB.Execute(True)
           
            rdb.SQL = "SELECT  CLIENT.SCLIENT, CLIENT.SFIRSTNAME, CLIENT.SMIDDLE_NAME, CLIENT.SLASTNAME, CLIENT.SLASTNAME2, FIDELITY.SCERTYPE, FIDELITY.NBRANCH, FIDELITY.NPRODUCT, FIDELITY.NPOLICY, FIDELITY.NCERTIF, FIDELITY.SCLIENAME, FIDELITY.NPOSITION, FIDELITY.NSALARY, DECODE(NVL(FIDELITY.NFACTOR,0),0,0, FIDELITY.NFACTOR) NFACTOR, FIDELITY.NVALUE, FIDELITY.NUSERCODE, FIDELITY.DEFFECDATE, FIDELITY.DCOMPDATE, FIDELITY.DNULLDATE FROM insudb.FIDELITY FIDELITY Inner Join insudb.CLIENT CLIENT ON FIDELITY.SCLIENT = CLIENT.SCLIENT WHERE  " & _
              " SCERTYPE = '" & Session("scertype") & "'" & _
              " AND NBRANCH = " & Session("nbranch") & _
              " AND NPRODUCT =  " & Session("nproduct") & _
              " AND NPOLICY = " & Session("nPolicy") & _
              " AND NCERTIF = " & Session("nCertif") & _
                " AND DEFFECDATE      <= 	STDATE('" & Convert.ToDateTime(mObjValues.StringToType(Session("DEFFECDATE"), eTypeData.etdDate)).ToString("yyyyMMdd") & "')" & _
                " AND (DNULLDATE IS NULL " & _
                "OR DNULLDATE >  	STDATE('" & Convert.ToDateTime(mObjValues.StringToType(Session("DEFFECDATE"), eTypeData.etdDate)).ToString("yyyyMMdd") & "') ) "


            With mObjGridFidelity
                If rdb.Run(True) Then
                    Do While Not rdb.EOF
                        .Columns("SCLIENT_Fidelity").DefValue = rdb.FieldToClass("SCLIENT")
                        .Columns("SFIRSTNAME_Fidelity").DefValue = rdb.FieldToClass("SFIRSTNAME")
                        .Columns("SMIDDLENAME_Fidelity").DefValue = rdb.FieldToClass("SMIDDLE_NAME")
                        .Columns("SLASTNAME_Fidelity").DefValue = rdb.FieldToClass("SLASTNAME")
                        .Columns("SLASTNAME2_Fidelity").DefValue = rdb.FieldToClass("SLASTNAME2")

                        .Columns("NPOSITION_Fidelity").DefValue = rdb.FieldToClass("NPOSITION")
                        .Columns("NSALARY_Fidelity").DefValue = rdb.FieldToClass("NSALARY")
                        .Columns("NFACTOR_Fidelity").DefValue = rdb.FieldToClass("NFACTOR")
                        .Columns("NVALUE_Fidelity").DefValue = rdb.FieldToClass("NVALUE")

                        Response.Write(.DoRow)
                        rdb.RNext()
                    Loop
                    rdb.RCloseRec()
                End If
                Response.Write(.closeTable())
            End With
        End With
    End Sub
    Public Sub insPreMU700Upd_Fidelity()
        With Request
            If .QueryString.Item("Action") = "Del" Then
                Dim lblnPost As Boolean
                Dim lstrMessage As String = String.Empty
                Dim rdb As New eRemoteDB.Execute(True)


                If String.IsNullOrEmpty(lstrMessage) Then
                    If mObjValues.StringToType(.QueryString.Item("dEffecdate"), eFunctions.Values.eTypeData.etdDate) = mObjValues.StringToType(.QueryString.Item("dEffecdateCurrent"), eFunctions.Values.eTypeData.etdDate) Then

                        rdb = New eRemoteDB.Execute(True)
                        rdb.SQL = "DELETE FROM INSUDB.FIDELITY WHERE SCERTYPE = :SCERTYPE AND NBRANCH = :NBRANCH AND NPRODUCT = :NPRODUCT AND NPOLICY = :NPOLICY AND NCERTIF = :NCERTIF AND SCLIENT = :SCLIENT AND DEFFECDATE = :DEFFECDATE "

                        rdb.Parameters.Add("SCERTYPE", .QueryString.Item("SCERTYPE"), eRmtDataDir.rdbParamInput, eRmtDataType.rdbCharFixedLength, 1, 0, 0, eRmtDataAttrib.rdbParamNullable)
                        rdb.Parameters.Add("NBRANCH", mObjValues.StringToType(.QueryString.Item("cbeBranch"), eFunctions.Values.eTypeData.etdLong), eRmtDataDir.rdbParamInput, eRmtDataType.rdbNumeric, 5, 0, 5, eRmtDataAttrib.rdbParamNullable)
                        rdb.Parameters.Add("NPRODUCT", mObjValues.StringToType(.QueryString.Item("valProduct"), eFunctions.Values.eTypeData.etdLong), eRmtDataDir.rdbParamInput, eRmtDataType.rdbNumeric, 5, 0, 5, eRmtDataAttrib.rdbParamNullable)
                        rdb.Parameters.Add("NPOLICY", mObjValues.StringToType(.QueryString.Item("NPOLICY"), eFunctions.Values.eTypeData.etdLong), eRmtDataDir.rdbParamInput, eRmtDataType.rdbNumeric, 10, 0, 10, eRmtDataAttrib.rdbParamNullable)
                        rdb.Parameters.Add("NCERTIF", mObjValues.StringToType(.QueryString.Item("NCERTIF"), eFunctions.Values.eTypeData.etdLong), eRmtDataDir.rdbParamInput, eRmtDataType.rdbNumeric, 5, 0, 5, eRmtDataAttrib.rdbParamNullable)
                        rdb.Parameters.Add("SCLIENT", .QueryString.Item("SCLIENT_Fidelity"), eRmtDataDir.rdbParamInput, eRmtDataType.rdbVarchar, 14, 0, 0, eRmtDataAttrib.rdbParamNullable)
                        rdb.Parameters.Add("DEFFECDATE", mObjValues.StringToType(.QueryString.Item("DEFFECtDATE"), eFunctions.Values.eTypeData.etdDate), eRmtDataDir.rdbParamInput, eRmtDataType.rdbDate, 7, 0, 0, eRmtDataAttrib.rdbParamNullable)


                        lblnPost = rdb.Run(False)
                    Else

                        rdb = New eRemoteDB.Execute(True)
                        rdb.SQL = "UPDATE INSUDB.FIDELITY SET DNULLDATE = :DNULLDATE WHERE SCERTYPE = :SCERTYPE AND NBRANCH = :NBRANCH AND NPRODUCT = :NPRODUCT AND NPOLICY = :NPOLICY AND NCERTIF = :NCERTIF AND SCLIENT = :SCLIENT AND DEFFECDATE = :DEFFECDATE"

                        rdb.Parameters.Add("DNULLDATE", mObjValues.StringToType(.QueryString.Item("dEffectdate"), eFunctions.Values.eTypeData.etdDate), eRmtDataDir.rdbParamInput, eRmtDataType.rdbDate, 7, 0, 0, eRmtDataAttrib.rdbParamNullable)
                        rdb.Parameters.Add("SCERTYPE", .QueryString.Item("SCERTYPE"), eRmtDataDir.rdbParamInput, eRmtDataType.rdbCharFixedLength, 1, 0, 0, eRmtDataAttrib.rdbParamNullable)
                        rdb.Parameters.Add("NBRANCH", mObjValues.StringToType(.QueryString.Item("cbeBranch"), eFunctions.Values.eTypeData.etdLong), eRmtDataDir.rdbParamInput, eRmtDataType.rdbNumeric, 5, 0, 5, eRmtDataAttrib.rdbParamNullable)
                        rdb.Parameters.Add("NPRODUCT", mObjValues.StringToType(.QueryString.Item("valProduct"), eFunctions.Values.eTypeData.etdLong), eRmtDataDir.rdbParamInput, eRmtDataType.rdbNumeric, 5, 0, 5, eRmtDataAttrib.rdbParamNullable)
                        rdb.Parameters.Add("NPOLICY", mObjValues.StringToType(.QueryString.Item("NPOLICY"), eFunctions.Values.eTypeData.etdLong), eRmtDataDir.rdbParamInput, eRmtDataType.rdbNumeric, 10, 0, 10, eRmtDataAttrib.rdbParamNullable)
                        rdb.Parameters.Add("NCERTIF", mObjValues.StringToType(.QueryString.Item("NCERTIF"), eFunctions.Values.eTypeData.etdLong), eRmtDataDir.rdbParamInput, eRmtDataType.rdbNumeric, 5, 0, 5, eRmtDataAttrib.rdbParamNullable)
                        rdb.Parameters.Add("SCLIENT", .QueryString.Item("SCLIENT_Fidelity"), eRmtDataDir.rdbParamInput, eRmtDataType.rdbVarchar, 14, 0, 0, eRmtDataAttrib.rdbParamNullable)
                        rdb.Parameters.Add("DEFFECDATE", mObjValues.StringToType(.QueryString.Item("dEffecdateCurrent"), eFunctions.Values.eTypeData.etdDate), eRmtDataDir.rdbParamInput, eRmtDataType.rdbDate, 7, 0, 0, eRmtDataAttrib.rdbParamNullable)


                        lblnPost = rdb.Run(False)
                    End If

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
            'Response.Write(mObjGridFidelity.DoFormUpd(.QueryString.Item("Action"), "valPolicySeq.aspx", .QueryString.Item("sCodispl"), .QueryString.Item("nMainAction"), mObjGridFidelity.ActionQuery, CShort(.QueryString.Item("Index")), , "&fromDelete=1"))
            Response.Write(mObjGridFidelity.DoFormUpd(.QueryString.Item("Action"), "valPolicySeq.aspx", .QueryString.Item("sCodispl"), .QueryString.Item("nMainAction"), mObjGridFidelity.ActionQuery, CShort(.QueryString.Item("Index")), "&fromDelete=1"))
        End With
    End Sub

    Public Sub insPreMU700_PuntualValue(ByVal objPreParameters As Object)
        mObjPuntualFidelity = objPreParameters
    End Sub

End Class
