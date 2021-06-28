Imports Microsoft.VisualBasic

Imports System.Globalization
Imports eNetFrameWork
Imports eFunctions
Imports System.Data
Imports eFunctions.Values
Imports eRemoteDB.Parameter

Public Class ctrlRoturaMaquinaria
    Inherits System.Web.UI.UserControl

    Public mObjValues As New eFunctions.Values

    Public mObjGridRotMaqui As eFunctions.Grid

    Public resxValues As IEnumerable(Of DictionaryEntry) = eFunctions.Values.GetResxValue("MU700")

    #Region "Rotura de maquinaria"
    Public Sub insDefineHeader_RotMaqui()

        mObjGridRotMaqui = New eFunctions.Grid
        mObjGridRotMaqui.sArrayName = "marrArray_RotMaqui"
        With mObjGridRotMaqui.Columns

            .AddTextColumn(Id:=0, Title:=resxValues.FindDictionaryValue("STRADEMARK_RotMaqui_Caption"), FieldName:="STRADEMARK_RotMaqui", Length:=30, DefValue:="", isRequired:=False, Alias_Renamed:=resxValues.FindDictionaryValue("STRADEMARK_RotMaqui_ToolTip"), OnChange:="InputOnChange(this)", Disabled:=False)
            .AddTextColumn(Id:=0, Title:=resxValues.FindDictionaryValue("SMODEL_RotMaqui_Caption"), FieldName:="SMODEL_RotMaqui", Length:=30, DefValue:="", isRequired:=False, Alias_Renamed:=resxValues.FindDictionaryValue("SMODEL_RotMaqui_ToolTip"), OnChange:="InputOnChange(this)", Disabled:=False)

            .AddNumericColumn(Id:=0, Title:=resxValues.FindDictionaryValue("NYEAR_RotMaqui_Caption"), FieldName:="NYEAR_RotMaqui", Length:=4, DefValue:="",isRequired:=False, Alias_Renamed:=resxValues.FindDictionaryValue("NYEAR_RotMaqui_ToolTip"), ShowThousand:=False, DecimalPlaces:=0,OnChange:="InputOnChange(this)", Disabled:=False, bAllowNegativ:=False)

            .AddTextColumn(Id:=0, Title:=resxValues.FindDictionaryValue("SORIGIN_RotMaqui_Caption"), FieldName:="SORIGIN_RotMaqui", Length:=30, DefValue:="", isRequired:=False, Alias_Renamed:=resxValues.FindDictionaryValue("SORIGIN_RotMaqui_ToolTip"), OnChange:="InputOnChange(this)", Disabled:=False)
            .AddTextColumn(Id:=0, Title:=resxValues.FindDictionaryValue("SSERIALNUMBER_RotMaqui_Caption"), FieldName:="SSERIALNUMBER_RotMaqui", Length:=30, DefValue:="", isRequired:=False, Alias_Renamed:=resxValues.FindDictionaryValue("SSERIALNUMBER_RotMaqui_ToolTip"), OnChange:="InputOnChange(this)", Disabled:=False)
            .AddNumericColumn(Id:=0, Title:=resxValues.FindDictionaryValue("NCAPITAL_RotMaqui_Caption"), FieldName:="NCAPITAL_RotMaqui", Length:=18, DefValue:="", isRequired:=False, Alias_Renamed:=resxValues.FindDictionaryValue("NCAPITAL_RotMaqui_ToolTip"), ShowThousand:=True, DecimalPlaces:=2, OnChange:="InputOnChangeRotMaqui(this)", Disabled:=False, bAllowNegativ:=False)

            .AddNumericColumn(Id:=0, Title:=resxValues.FindDictionaryValue("NRATE_RotMaqui_Caption"), FieldName:="NRATE_RotMaqui", Length:=9, DefValue:="",isRequired:=False, Alias_Renamed:=resxValues.FindDictionaryValue("NRATE_RotMaqui_ToolTip"), ShowThousand:=True, DecimalPlaces:=6,OnChange:="InputOnChangeRotMaqui(this)", Disabled:=False, bAllowNegativ:=False)
            .AddNumericColumn(Id:=0, Title:=resxValues.FindDictionaryValue("NPREMIUM_RotMaqui_Caption"), FieldName:="NPREMIUM_RotMaqui", Length:=18, DefValue:="",isRequired:=False, Alias_Renamed:=resxValues.FindDictionaryValue("NPREMIUM_RotMaqui_ToolTip"), ShowThousand:=True, DecimalPlaces:=6,OnChange:="InputOnChange(this)", Disabled:=True, bAllowNegativ:=False)
            .AddHiddenColumn("nConsec_RotMaqui", "0")
            .AddHiddenColumn("NTYPE_RotMaqui", "4")
        End With

        With mObjGridRotMaqui
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
            .Columns("STRADEMARK_RotMaqui").EditRecord = True
            '.Columns("NTYPE_RotMaqui").Disabled = Request.QueryString.Item("Action") = "Update"
            '.Columns("DEFFECTDATE").Disabled = Request.QueryString.Item("Action") = "Update"
            '        	 	.sDelRecordParam = "NCONSEC_RotMaqui=' + marrArray[lintIndex].NCONSEC_RotMaqui + '" & "&SCERTYPE=' + marrArray[lintIndex].SCERTYPE + '" & "&cbeBranch=' + marrArray[lintIndex].cbeBranch + '" & "&valProduct=' + marrArray[lintIndex].valProduct + '" & "&NPOLICY=' + marrArray[lintIndex].NPOLICY + '" & "&NCERTIF=' + marrArray[lintIndex].NCERTIF + '" & "&DEFFECTDATE=' + marrArray[lintIndex].DEFFECTDATE + '"

            If Request.QueryString.Item("Reload") = "1" Then
                .sReloadIndex = Request.QueryString.Item("ReloadIndex")
            End If

            .AddButton = True
            .DeleteButton = True
            .Columns("Sel").GridVisible = .DeleteButton

            .sEditRecordParam = "&gridName=RotMaqui"

            .sDelRecordParam = "gridName=RotMaqui" & _
                              "&SCERTYPE=" & Session("SCERTYPE") & _
                              "&cbeBranch=" & session("nBranch") & _ 
                              "&valProduct=" & session("nProduct") & _
                              "&NPOLICY=" & session("NPOLICY") & _
                              "&NCERTIF=" & session("NCERTIF") & _
                              "&DEFFECTDATE=" & session("DEFFECDATE") & _
                              "&NCONSEC_RotMaqui=' + marrArray_RotMaqui[lintIndex].nConsec_RotMaqui + '" & _
                              "&NTYPE_RotMaqui=4" 

        End With


    End Sub
    Public Sub insPreMU700_RotMaqui()
      With Request                    
          Dim rdb As New eRemoteDB.Execute(True)
               
            rdb.SQL = "SELECT  MULTIRISK_DET.NCONSEC, MULTIRISK_DET.SCERTYPE, MULTIRISK_DET.NBRANCH, MULTIRISK_DET.NPRODUCT, MULTIRISK_DET.NPOLICY, MULTIRISK_DET.NCERTIF, MULTIRISK_DET.NTYPE, MULTIRISK_DET.DEFFECTDATE, MULTIRISK_DET.NSECTION, MULTIRISK_DET.SDESCRIPTION, MULTIRISK_DET.NCAPITAL, MULTIRISK_DET.STRADEMARK, MULTIRISK_DET.SMODEL, MULTIRISK_DET.NYEAR, MULTIRISK_DET.SORIGIN, MULTIRISK_DET.SSERIALNUMBER, MULTIRISK_DET.NRATE, MULTIRISK_DET.NPREMIUM, MULTIRISK_DET.DCOMPDATE, MULTIRISK_DET.NUSERCODE, MULTIRISK_DET.NELEMENT_TYPE, MULTIRISK_DET.DNULLDATE,MULTIRISK_DET.NTYPE FROM insudb.MULTIRISK_DET MULTIRISK_DET WHERE " & _
              " SCERTYPE = '" & Session("scertype") & "'" & _
              " AND NBRANCH = " & Session("nbranch") & _
              " AND NPRODUCT =  " & Session("nproduct") & _
              " AND NPOLICY = " & Session("nPolicy") & _
              " AND NCERTIF = " & Session("nCertif") & _
              " AND NTYPE = 4 " & _
                " AND DEFFECTDATE      <= 	STDATE('" & Convert.ToDateTime(mObjValues.StringToType(Session("DEFFECDATE"), eTypeData.etdDate)).ToString("yyyyMMdd") & "')" & _
                " AND (DNULLDATE IS NULL " & _
                "OR DNULLDATE >  	STDATE('" & Convert.ToDateTime(mObjValues.StringToType(Session("DEFFECDATE"), eTypeData.etdDate)).ToString("yyyyMMdd") & "') ) "

 
          
          With mObjGridRotMaqui
            If rdb.Run(True) Then
               Do While Not rdb.EOF
                    .Columns("STRADEMARK_RotMaqui").DefValue = rdb.FieldToClass("STRADEMARK") 
                    .Columns("SMODEL_RotMaqui").DefValue = rdb.FieldToClass("SMODEL") 
                    .Columns("NYEAR_RotMaqui").DefValue = rdb.FieldToClass("NYEAR") 
                    .Columns("SORIGIN_RotMaqui").DefValue = rdb.FieldToClass("SORIGIN") 
                    .Columns("SSERIALNUMBER_RotMaqui").DefValue = rdb.FieldToClass("SSERIALNUMBER") 
                    .Columns("NRATE_RotMaqui").DefValue = rdb.FieldToClass("NRATE") 
                    .Columns("NPREMIUM_RotMaqui").DefValue = rdb.FieldToClass("NPREMIUM") 
                    .Columns("NCONSEC_RotMaqui").DefValue = rdb.FieldToClass("NCONSEC")
                    .Columns("NTYPE_RotMaqui").DefValue = 4
                    .Columns("NCAPITAL_RotMaqui").DefValue = rdb.FieldToClass("NCAPITAL")
                 Response.Write(.DoRow)
                 rdb.RNext()
               Loop                 
               rdb.RCloseRec()              
            End If            
            Response.Write(.CloseTable())        
          End With
       End With
    End Sub
    Public Sub insPreMU700Upd_RotMaqui()
              With Request
            If .QueryString.Item("Action") = "Del" Then
                Dim lblnPost As Boolean
                Dim lstrMessage As String = String.Empty
                Dim rdb As New eRemoteDB.Execute(True)


                If String.IsNullOrEmpty(lstrMessage) Then
                    rdb = New eRemoteDB.Execute(True)
                    rdb.SQL = "DELETE FROM INSUDB.MULTIRISK_DET WHERE NCONSEC = :NCONSEC AND SCERTYPE = :SCERTYPE AND NBRANCH = :NBRANCH AND NPRODUCT = :NPRODUCT AND NPOLICY = :NPOLICY AND NCERTIF = :NCERTIF AND NTYPE = :NTYPE AND DEFFECTDATE = :DEFFECTDATE "

                    rdb.Parameters.Add("NCONSEC", mObjValues.StringToType(.QueryString.Item("NCONSEC_RotMaqui"), eFunctions.Values.eTypeData.etdLong), eRmtDataDir.rdbParamInput, eRmtDataType.rdbNumeric, 5, 0, 5, eRmtDataAttrib.rdbParamNullable)
                    rdb.Parameters.Add("SCERTYPE", .QueryString.Item("SCERTYPE"), eRmtDataDir.rdbParamInput, eRmtDataType.rdbCharFixedLength, 1, 0, 0, eRmtDataAttrib.rdbParamNullable)
                    rdb.Parameters.Add("NBRANCH", mObjValues.StringToType(.QueryString.Item("cbeBranch"), eFunctions.Values.eTypeData.etdLong), eRmtDataDir.rdbParamInput, eRmtDataType.rdbNumeric, 5, 0, 5, eRmtDataAttrib.rdbParamNullable)
                    rdb.Parameters.Add("NPRODUCT", mObjValues.StringToType(.QueryString.Item("valProduct"), eFunctions.Values.eTypeData.etdLong), eRmtDataDir.rdbParamInput, eRmtDataType.rdbNumeric, 5, 0, 5, eRmtDataAttrib.rdbParamNullable)
                    rdb.Parameters.Add("NPOLICY", mObjValues.StringToType(.QueryString.Item("NPOLICY"), eFunctions.Values.eTypeData.etdLong), eRmtDataDir.rdbParamInput, eRmtDataType.rdbNumeric, 10, 0, 10, eRmtDataAttrib.rdbParamNullable)
                    rdb.Parameters.Add("NCERTIF", mObjValues.StringToType(.QueryString.Item("NCERTIF"), eFunctions.Values.eTypeData.etdLong), eRmtDataDir.rdbParamInput, eRmtDataType.rdbNumeric, 10, 0, 10, eRmtDataAttrib.rdbParamNullable)
                    rdb.Parameters.Add("NTYPE", mObjValues.StringToType(.QueryString.Item("NTYPE_RotMaqui"), eFunctions.Values.eTypeData.etdLong), eRmtDataDir.rdbParamInput, eRmtDataType.rdbNumeric, 5, 0, 5, eRmtDataAttrib.rdbParamNullable)
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
            Response.Write(mObjGridRotMaqui.DoFormUpd(.QueryString.Item("Action"), "valPolicySeq.aspx", .QueryString.Item("sCodispl"), .QueryString.Item("nMainAction"), mObjGridRotMaqui.ActionQuery, CShort(.QueryString.Item("Index"))))
        End With


    End Sub
#End Region


End Class
