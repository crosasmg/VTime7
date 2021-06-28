<%@ Page Language="VB" Explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon"
    EnableViewState="false" %>

<%@ Import Namespace="eNetFrameWork" %>
<%@ Import Namespace="eFunctions" %>
<%@ Import Namespace="ePolicy" %>
<script language="VB" runat="Server">
    '^Begin Header Block VisualTimer Utility 1.1 31/3/03 19.42.04
    Dim mobjNetFrameWork As eNetFrameWork.Layout
    Dim mintCommission As String
    Dim mblnCurrentComm As Object
    Dim mobjGrid As eFunctions.Grid
    Dim mobjValues As eFunctions.Values
    Dim mobjMenu As eFunctions.Menues
    Dim mobjGrid_det As eFunctions.Grid

    Dim lintGroup As New Integer

    '% insDefineHeader: se definen las propiedades del grid
    '--------------------------------------------------------------------------------------------
    Private Sub insDefineHeader()
        '--------------------------------------------------------------------------------------------
        mobjGrid = New eFunctions.Grid
        '^Begin Body Block VisualTimer Utility 1.1 31/3/03 19.42.04
        mobjGrid.sSessionID = Session.SessionID
        mobjGrid.nUsercode = Session("nUsercode")
        '~End Body Block VisualTimer Utility
        Dim lcolCertificat As New ePolicy.Certificat
        
        lcolCertificat.Find(Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif") )

        mobjGrid.sArrayName = "marrArray"

        mobjGrid.sCodisplPage = Request.QueryString.Item("sCodispl")
        Call mobjGrid.SetWindowParameters(Request.QueryString.Item("sCodispl"), Request.QueryString.Item("sWindowDescript"), Request.QueryString.Item("nWindowTy"))

        lintGroup = mobjValues.StringToType(Request.QueryString.Item("nGroup"), eFunctions.Values.eTypeData.etdDouble)

        '+ Se definen las columnas del grid
        With mobjGrid.Columns
            .AddPossiblesColumn(0, GetLocalResourceObject("cbeModulecColumnCaption"), "cbeModulec", "TABMODULES_CA060", eFunctions.Values.eValuesType.clngWindowType, vbNullString, True, , , , "insChangeModulec(this)", Request.QueryString.Item("Action") = "Update", , GetLocalResourceObject("cbeModulecColumnToolTip"))
            .AddPossiblesColumn(0, GetLocalResourceObject("cbeCoverColumnCaption"), "cbeCover", "TABGEN_COVER2", eFunctions.Values.eValuesType.clngWindowType, vbNullString, True, , , , , Request.QueryString.Item("Action") = "Update", , GetLocalResourceObject("cbeCoverColumnToolTip"))
            
            Call .AddPossiblesColumn(0, GetLocalResourceObject("cbeCurrencyColumnCaption"), "cbeCurrency", "tabCurren_pol", eFunctions.Values.eValuesType.clngComboType,"1" , True, , , , , , , GetLocalResourceObject("cbeCurrencyColumnToolTip"))
            mobjGrid.Columns("cbeCurrency").Parameters.Add("sCertype", Session("sCertype"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            mobjGrid.Columns("cbeCurrency").Parameters.Add("nBranch", Session("nBranch"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            mobjGrid.Columns("cbeCurrency").Parameters.Add("nProduct", Session("nProduct"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            mobjGrid.Columns("cbeCurrency").Parameters.Add("nPolicy", Session("nPolicy"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            mobjGrid.Columns("cbeCurrency").Parameters.Add("nCertif", Session("nCertif"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            mobjGrid.Columns("cbeCurrency").Parameters.Add("dEffecdate", Session("dEffecdate"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            Call .AddNumericColumn(0, GetLocalResourceObject("tcnEndorsementValueCaption"), "tcnEndorsementValue", 18, , , GetLocalResourceObject("tcnEndorsementValueToolTip"), True, 6)
            .AddHiddenColumn("hddConsecutive", "0")
            .AddHiddenColumn("hddgridID", "1")

        End With

        '+ Se definen las propiedades generales del grid
        With mobjGrid
            .Codispl = "CA061"
            .ActionQuery = mobjValues.ActionQuery
            .Columns("Sel").GridVisible = Not .ActionQuery
            
            
            
            mobjGrid.Columns("cbeModulec").Parameters.Add("sCertype", Session("sCertype"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            mobjGrid.Columns("cbeModulec").Parameters.Add("nBranch", Session("nBranch"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            mobjGrid.Columns("cbeModulec").Parameters.Add("nProduct", Session("nProduct"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            mobjGrid.Columns("cbeModulec").Parameters.Add("nPolicy", Session("nPolicy"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            mobjGrid.Columns("cbeModulec").Parameters.Add("nCertif", Session("nCertif"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            mobjGrid.Columns("cbeModulec").Parameters.Add("NGROUP_INSU", lcolCertificat.nGroup, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            mobjGrid.Columns("cbeModulec").Parameters.Add("DPROCESS", mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Columns("cbeCover").Parameters.Add("nBranch", mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Columns("cbeCover").Parameters.Add("nProduct", mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Columns("cbeCover").Parameters.Add("nCover", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Columns("cbeCover").Parameters.Add("dEffecdate", mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Columns("cbeCover").Parameters.Add("nModulec", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)


            ' .sDelRecordParam = "nIntermed='+ marrArray[lintIndex].valIntermed + '" & "&nRole='+ marrArray[lintIndex].cbeRole + '"


            .sDelRecordParam = "sIsFACOB=1&nConsecutive='+ marrArray[lintIndex].hddConsecutive   + '"
            .Columns("Sel").GridVisible = Not .ActionQuery
            .Columns("tcnEndorsementValue").EditRecord = True
            .Top = 50
            .Height = 420
            .Width = 400
            .nMainAction = CShort(IIf(Request.QueryString.Item("nMainAction") = "", 0, Request.QueryString.Item("nMainAction")))
            .UpdContent = True
            .DeleteButton = True
            .WidthDelete = 500
            If Request.QueryString.Item("Reload") = "1" Then
                .sReloadIndex = Request.QueryString.Item("ReloadIndex")
            End If
        End With
    End Sub



    '% insDefineHeader: se definen las propiedades del grid
    '--------------------------------------------------------------------------------------------
    Private Sub insDefineHeader_det()
        '--------------------------------------------------------------------------------------------
        mobjGrid_det = New eFunctions.Grid
        '^Begin Body Block VisualTimer Utility 1.1 31/3/03 19.42.04
        mobjGrid_det.sSessionID = Session.SessionID
        mobjGrid_det.nUsercode = Session("nUsercode")
        '~End Body Block VisualTimer Utility
        mobjGrid_det.sArrayName = "marrArray_det"

        mobjGrid_det.sCodisplPage = Request.QueryString.Item("sCodispl")
        Call mobjGrid_det.SetWindowParameters(Request.QueryString.Item("sCodispl"), Request.QueryString.Item("sWindowDescript"), Request.QueryString.Item("nWindowTy"))

        '+ Se definen las columnas del grid
        With mobjGrid_det.Columns

            Call .AddPossiblesColumn(0, GetLocalResourceObject("cbeDetail_ItemCaption"), "cbeDetail_Item", "TabDetail_Item", eFunctions.Values.eValuesType.clngWindowType,, True, , ,  , "insChangeDetail(this)", , , GetLocalResourceObject("cbeDetail_ItemToolTip"))
            mobjGrid_det.Columns("cbeDetail_Item").Parameters.Add("sCertype", Session("sCertype"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            mobjGrid_det.Columns("cbeDetail_Item").Parameters.Add("nBranch", Session("nBranch"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            mobjGrid_det.Columns("cbeDetail_Item").Parameters.Add("nProduct", Session("nProduct"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            mobjGrid_det.Columns("cbeDetail_Item").Parameters.Add("nPolicy", Session("nPolicy"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            mobjGrid_det.Columns("cbeDetail_Item").Parameters.Add("nCertif", Session("nCertif"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            mobjGrid_det.Columns("cbeDetail_Item").Parameters.Add("dEffecdate", Session("dEffecdate"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            mobjGrid_det.Columns("cbeDetail_Item").Parameters.Add("nType", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            mobjGrid_det.Columns("cbeDetail_Item").Parameters.ReturnValue("nType", , , True)
              mobjGrid_det.Columns("cbeDetail_Item").Parameters.ReturnValue("nCapital", , , True)
            Call .AddHiddenColumn("hddType", "")
              Call .AddHiddenColumn("hddCapital", "")
            Call .AddPossiblesColumn(0, GetLocalResourceObject("cbeCurrencyColumnCaption"), "cbeCurrency", "tabCurren_pol", eFunctions.Values.eValuesType.clngComboType, "1", True, , , , , , , GetLocalResourceObject("cbeCurrencyColumnToolTip"))
            mobjGrid_det.Columns("cbeCurrency").Parameters.Add("sCertype", Session("sCertype"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            mobjGrid_det.Columns("cbeCurrency").Parameters.Add("nBranch", Session("nBranch"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            mobjGrid_det.Columns("cbeCurrency").Parameters.Add("nProduct", Session("nProduct"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            mobjGrid_det.Columns("cbeCurrency").Parameters.Add("nPolicy", Session("nPolicy"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            mobjGrid_det.Columns("cbeCurrency").Parameters.Add("nCertif", Session("nCertif"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            mobjGrid_det.Columns("cbeCurrency").Parameters.Add("dEffecdate", Session("dEffecdate"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            Call .AddNumericColumn(0, GetLocalResourceObject("tcnEndorsementValueCaption"), "tcnEndorsementValue", 18, , , GetLocalResourceObject("tcnEndorsementValueToolTip"), True, 6)
            .AddHiddenColumn("hddConsecutive", "0")
            .AddHiddenColumn("hddgridID", "2")

        End With

        '+ Se definen las propiedades generales del grid
        With mobjGrid_det
            .Codispl = "CA061"
            .ActionQuery = mobjValues.ActionQuery
            .Columns("Sel").GridVisible = Not .ActionQuery


            .Columns("tcnEndorsementValue").EditRecord = True
            .sDelRecordParam = "sIsFACOB=1&nConsecutive='+ marrArray_det[lintIndex].hddConsecutive   + '"

            '  .sDelRecordParam = "nIntermed='+ marrArray[lintIndex].valIntermed + '" & "&nRole='+ marrArray[lintIndex].cbeRole + '"
            .Top = 50
            .Height = 420
            .Width = 400
            .nMainAction = CShort(IIf(Request.QueryString.Item("nMainAction") = "", 0, Request.QueryString.Item("nMainAction")))
            .UpdContent = True
            .DeleteButton = True
            .WidthDelete = 500
            If Request.QueryString.Item("Reload") = "1" Then
                .sReloadIndex = Request.QueryString.Item("ReloadIndex")
            End If
            Call .Splits_Renamed.AddSplit(0, GetLocalResourceObject("6ColumnCaption"), 6)
            Call .Splits_Renamed.AddSplit(0, GetLocalResourceObject("5ColumnCaption"), 5)
        End With
    End Sub


    '% insPreCA061: se realiza el manejo del grid
    '-------------------------------------------------------------------------------------------- 
    Private Sub insPreCA061()
        Dim QueryString() As String
        '-------------------------------------------------------------------------------------------- 

        Dim lcolCreditor_information As ePolicy.Creditor_information
        Dim lcolCreditor_information_det As ePolicy.Creditor_information
        Dim lcolsCreditor_information As New ePolicy.Creditor_informations
        Dim lcolsCreditor_information_det As New ePolicy.Creditor_informations
        Dim lcolCreditor_information_aux As New ePolicy.Creditor_information

        Dim lclsPolicy As ePolicy.Policy
        Dim lclsAddress As eGeneralForm.Address
        Dim mstrKeyAddress As String
        Dim mlngBranch As Object
        Dim mlngProduct As Object
        Dim mlngPolicy As Object
        Dim mlngProponum As Object
        Dim mlngCertif As Object
        Dim mlngClaim As Object
        Dim mlngCase_num As Object
        Dim mstrBrancht As Object
        Dim mintDeman_type As Object
        Dim mstrDescadd As String
        Dim tcdEffecdate As New Date
        Dim mintTotal As Double
        Dim mstrText As String
        Dim hddIniDate As New Date
        Dim hddEndDate As new Date
        Dim lclsAuto As ePolicy.Automobile
        lclsAuto = New ePolicy.Automobile
        lclsPolicy = New ePolicy.Policy
        Dim mobjQuery As New eRemoteDB.Query
        
        

        mlngBranch = Session("nBranch")
        mlngProduct = Session("nProduct")
        mlngPolicy = Session("nPolicy")
        mlngProponum = Session("nPolicy")
        mlngCertif = Session("nCertif")
        mlngClaim = eRemoteDB.Constants.intNull
        mlngCase_num = eRemoteDB.Constants.intNull
        mstrBrancht = Session("sBrancht")
        mintDeman_type = eRemoteDB.Constants.intNull
        mstrKeyAddress = "12" & mlngBranch & mlngProduct & mlngPolicy & mlngCertif
        If Request.QueryString("tcdEffecdate") = vbNullString Then
            If CStr(Session("dEffecdate")) = vbNullString Then
                tcdEffecdate = Today
            Else
                tcdEffecdate = Session("dEffecdate")
            End If
        Else
            tcdEffecdate = Request.QueryString("tcdEffecdate")
        End If

        'If Session("nBranch") = 6 then
        '    If lclsAuto.Find(Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), Session("dEffecdate")) Then

        '        If mobjQuery.OpenQuery("TABLE7042", "NVEHBRAND", "TRIM(UPPER(SDESCRIPT)) = '" & lclsAuto.sVehmodel.ToUpper.Trim() & "'") Then
        '            lclsAuto.nVehBrand = mobjValues.StringToType(mobjQuery.FieldToClass("NVEHBRAND"), Values.eTypeData.etdInteger)
        '        End If


        '        mstrText = "MARCA " &   lclsAuto.nVehBrand & "-" & "AÑO " & lclsAuto.nYear & "-" & "PLACA " & lclsAuto.sRegist & "-" &
        '                   "MODELO " & lclsAuto.sVehModel & "-" & "COLOR: " & lclsAuto.sColor & "-" & "MOTOR: " & lclsAuto.sMotor & "-" & "CHASIS: " & lclsAuto.sChassis

        '    End If
        ' End If


        With lclsPolicy
            If .Find(Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy")) Then
                lcolCreditor_information = New ePolicy.Creditor_information
                lcolCreditor_information_det = New ePolicy.Creditor_information

                lclsAddress = New eGeneralForm.Address
                If lclsAddress.Find(mstrKeyAddress, 8, tcdEffecdate) Then
                    mstrDescadd = Mid(lclsAddress.sDescadd, 1, 30)
                Else
                    mstrDescadd = vbNullString
                End If
                lclsAddress = Nothing
                
                


                Response.Write("" & vbCrLf)
                Response.Write("		<TABLE WIDTH=""100%"">" & vbCrLf)
                Response.Write("		    <TR>" & vbCrLf)
                Response.Write("		        <TD COLSPAN=""2""  WIDTH=40%><LABEL ID=40950>" & GetLocalResourceObject("AnchorCaption") & "</LABEL></TD>" & vbCrLf)
                Response.Write("		        <TD WIDTH=""10%"">&nbsp;</TD>" & vbCrLf)
                Response.Write("		    </TR>" & vbCrLf)
                Response.Write("			<TR>" & vbCrLf)
                Response.Write("				<TD><LABEL>" & GetLocalResourceObject("tcdIniDateCaption") & "</LABEL></TD>" & vbCrLf)
                Response.Write("				<TD>")
                Response.Write(mobjValues.DateControl("tcdIniDate",  Request.QueryString.Item("dIniDate") , , GetLocalResourceObject("tcdIniDateToolTip"), , , , , False))

                Response.Write("" & vbCrLf)
                Response.Write("				</TD>" & vbCrLf)
                Response.Write("				<TD><LABEL>" & GetLocalResourceObject("tcdEndDateCaption") & "</LABEL></TD>" & vbCrLf)
                Response.Write("				<TD COLSPAN=""2"">")

                Response.Write(mobjValues.DateControl("tcdEndDate",    Request.QueryString.Item("dEndDate") , , GetLocalResourceObject("tcdEndDateToolTip"), , , , , False))

                Response.Write("</TD>" & vbCrLf)
                Response.Write("			</TR>" & vbCrLf)
                Response.Write("			<TR>" & vbCrLf)
                Response.Write("				<TD><LABEL>" & GetLocalResourceObject("tcdEffecdateCaption") & "</LABEL></TD>" & vbCrLf)
                Response.Write("				<TD>")
                If Request.QueryString.Item("nMainAction") = 401 Then
                    mobjValues.mblnActionQuery = False
                End If
                Response.Write(mobjValues.DateControl("tcdEffecdate", mobjValues.StringToType(tcdEffecdate, eFunctions.Values.eTypeData.etdDate), , GetLocalResourceObject("tcdEffecdateToolTip"), , , , IIf(Request.QueryString.Item("nMainAction") = 401, "insReload()", ""), Request.QueryString.Item("nMainAction") = 304))

                If Request.QueryString.Item("nMainAction") = 401 Then
                    mobjValues.mblnActionQuery = False
                    mobjValues.Parameters.Add("sCertype", Session("sCertype"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    mobjValues.Parameters.Add("nBranch", Session("nBranch"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    mobjValues.Parameters.Add("nProduct", Session("nProduct"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    mobjValues.Parameters.Add("nPolicy", Session("nPolicy"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    mobjValues.Parameters.Add("nCertif", Session("nCertif"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

                    Response.Write(mobjValues.PossiblesValues("tcdEffecdate2", "TABCRED_DATE", eFunctions.Values.eValuesType.clngWindowType, Convert.ToDateTime(mobjValues.StringToType(Today, eFunctions.Values.eTypeData.etdDate)).ToString("dd/MM/yyyy"), True, , , , , "setDate()", , 10, GetLocalResourceObject("tcdEffecdateToolTip"), Values.eTypeCode.eString, 16))

                    Response.Write("<script>")
                    Response.Write(" $('#tcdEffecdate2Desc').css('display', 'none'); ")
                    Response.Write(" $('input[name=tcdEffecdate]').prependTo($('input[name=tcdEffecdate2]').parent()); ")
                    Response.Write(" $('input[name=tcdEffecdate2]').css('visibility', 'hidden'); ")
                    Response.Write(" $('input[name=tcdEffecdate2]').css('position', 'absolute'); ")
                    Response.Write(" $('input[name=tcdEffecdate2]').css('left', '0px'); ")
                    Response.Write(" $('input[name=tcdEffecdate2]').css('top', '0px'); ")
                    Response.Write("</" & "script>")
                End If
                If Request.QueryString.Item("nMainAction") = 401 Then
                    mobjValues.mblnActionQuery = True
                End If
                Response.Write("" & vbCrLf)
                Response.Write("				</TD>" & vbCrLf)

                Response.Write("				<TD><LABEL>" & GetLocalResourceObject("tcnEndorsementValueCaption") & "</LABEL></TD>" & vbCrLf)
                Response.Write("				<TD COLSPAN=""3"">")

                Response.Write(mobjValues.NumericControl("tcnEndorsementValue", 8, "", , , True, 2, , , , , True))

                Response.Write("</TD>" & vbCrLf)

                Response.Write("	    </TABLE>" & vbCrLf)


                Response.Write("		<TABLE WIDTH=""100%"">" & vbCrLf)

                Response.Write("			<TR>" & vbCrLf)
                Response.Write("				<TD COLSPAN=""3""><LABEL>" & GetLocalResourceObject("tctAddressCaption") & "</LABEL></TD>" & vbCrLf)
                Response.Write("				<TD COLSPAN=""3"">")


                Response.Write(mobjValues.TextAreaControl("tctAddress", 2, 150, mstrDescadd, , GetLocalResourceObject("tctAddressToolTip"), , True))

                Response.Write("</TD>" & vbCrLf)
                Response.Write("			</TR>" & vbCrLf)

                Response.Write("	    </TABLE>" & vbCrLf)


                Response.Write("	    <BR>")
                Response.Write(mobjValues.ShowWindowsName(Request.QueryString.Item("sCodispl"), GetLocalResourceObject("colum1")))
                Response.Write("	    <BR>")

                mintTotal = 0

                '   mobjGrid.sEditRecordParam = "nCommityp=' + " & mintCommission & " + '" & "&nPercent=' + self.document.forms[0].tcnPercentCF.value + '" & "&sInd_Comm=' +  self.document.forms[0].hddInd_Comm.value  + '" & "&sConColl=' +  self.document.forms[0].hddConColl.value  + '"
                mobjGrid.sEditRecordParam = "sIsFACOB=1&nConsecutive=' + self.document.forms[0].hddConsecutive  + '" & "&dIniDate=' + self.document.forms[0].tcdIniDate.value + '"  & "&dEndDate=' + self.document.forms[0].tcdEndDate.value + '"

                If lcolsCreditor_information.Find(Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), tcdEffecdate, 1) Then
                    For Each lcolCreditor_information In lcolsCreditor_information
                        With mobjGrid
                            .Columns("cbeModulec").DefValue = lcolCreditor_information.nModulec
                            .Columns("cbeCover").DefValue = lcolCreditor_information.nCover
                            .Columns("cbeCurrency").DefValue = lcolCreditor_information.nCurrency
                            .Columns("tcnEndorsementValue").DefValue = lcolCreditor_information.nEndorsementvalue
                            .Columns("cbeCover").Parameters.Add("nModulec", lcolCreditor_information.nModulec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                            .Columns("hddConsecutive").DefValue = lcolCreditor_information.nConsecutive
                            'mstrText = mstrText & "- '" & CStr(lcolCreditor_information.ndetail_item) & "' - '" & CStr(lcolCreditor_information.nEndorsementvalue) & "'"
                            mintTotal = mintTotal + lcolCreditor_information.nEndorsementvalue
                            Response.Write(.DoRow)
                        End With
                    Next lcolCreditor_information
                End If
                Response.Write(mobjGrid.closeTable())
                
                
            
                lcolCreditor_information = Nothing

                mobjGrid_det.sEditRecordParam = "sIsFACOB=2&nConsecutive=' +self.document.forms[0].hddConsecutive  + '"


                If lcolsCreditor_information_det.Find(Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), tcdEffecdate, 2, True) Then
                    For Each lcolCreditor_information_det In lcolsCreditor_information_det
                        With mobjGrid_det
                            If lcolCreditor_information_det.ndetail_item > 0 Then
                                .Columns("cbeDetail_Item").DefValue = lcolCreditor_information_det.ndetail_item
                                .Columns("hddType").DefValue = CStr(lcolCreditor_information_det.nType)
                                .Columns("cbeDetail_Item").Parameters.Add("nType", lcolCreditor_information_det.nType, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                                .Columns("cbeCurrency").DefValue = lcolCreditor_information_det.nCurrency
                                .Columns("tcnEndorsementValue").DefValue = lcolCreditor_information_det.nEndorsementvalue
                                .Columns("hddConsecutive").DefValue = lcolCreditor_information_det.nConsecutive
                                mintTotal = mintTotal + lcolCreditor_information_det.nEndorsementvalue
                                ' mstrText = mstrText & "-" & CStr(lcolCreditor_information_det.ndetail_item) & "-" & CStr(lcolCreditor_information_det.nEndorsementvalue)
                           
                             
                                Response.Write(.DoRow)
                            End If
                            hddIniDate = lcolCreditor_information_det.dEffecendorsementdate
                            hddEndDate = lcolCreditor_information_det.dExpirendorsementdate
                            
                        End With
                    Next lcolCreditor_information_det
                End If
                Response.Write(mobjGrid_det.closeTable())
                
                Response.Write(mobjValues.HiddenControl("hddAmount", mintTotal))
                Response.Write(mobjValues.HiddenControl("hddIniDate", IIf(hddIniDate = eRemoteDB.Constants.dtmNull, Request.QueryString.Item("dIniDate"), hddIniDate)))
                Response.Write(mobjValues.HiddenControl("hddEndDate", IIf(hddEndDate = eRemoteDB.Constants.dtmNull,   Request.QueryString.Item("dEndDate"), hddEndDate)))
              
                Response.Write("	    <BR>")
                Response.Write(mobjValues.ShowWindowsName(Request.QueryString.Item("sCodispl"), GetLocalResourceObject("colum2")))
                Response.Write("	    <BR>")


                Response.Write("		<TABLE WIDTH=""100%"">" & vbCrLf)

                Response.Write("			<TR>" & vbCrLf)
                Response.Write("				<TD COLSPAN=""3""><LABEL>" & GetLocalResourceObject("tctTextCaption") & "</LABEL></TD>" & vbCrLf)
                Response.Write("				<TD COLSPAN=""3"">")



                mstrText = CStr(lcolCreditor_information_aux.Getextvalue(Session("sCertype"), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("npolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nCertif"), eFunctions.Values.eTypeData.etdDouble), tcdEffecdate))

                Response.Write(mobjValues.TextAreaControl("tctText", 2, 150, mstrText, , GetLocalResourceObject("tctTextToolTip")))

                Response.Write("</TD>" & vbCrLf)
                Response.Write("			</TR>" & vbCrLf)
                Response.Write("	    </TABLE>" & vbCrLf)



                '  Response.Write("top.frames['fraHeader'].UpdateDiv('lblTotCobDev','" & mobjValues.TypeToString(System.Math.Round(.nTotalAmount, 6), eFunctions.Values.eTypeData.etdDouble, True, 2) & "');")



                Response.Write("" & vbCrLf)
                Response.Write("<script>    " & vbCrLf)
                Response.Write("InvokeSetHeader(" & mintTotal & " );")
                Response.Write("</" & "script>        ")

                lcolCreditor_information = Nothing
                lcolCreditor_information_det = Nothing
                lcolCreditor_information_aux = Nothing

            End If
        End With
        lclsPolicy = Nothing
    End Sub
    '% insPreCA061Upd: Se realiza el manejo de la ventana PopUp asociada al grid
    '--------------------------------------------------------------------------------------------
    Private Sub insPreCA061Upd()
        '--------------------------------------------------------------------------------------------
        Dim lclsCreditor_information As ePolicy.Creditor_information
        Dim lstrContent As String
        lstrContent = vbNullString
        With Request
            If Request.QueryString.Item("Action") = "Del" Then
                lclsCreditor_information = New ePolicy.Creditor_information
                Response.Write(mobjValues.ConfirmDelete())
                If lclsCreditor_information.insPostCA061(Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), Session("dEffecdate"), Session("nUsercode"), Request.QueryString("Action"), .Form.Item("cbeCurrency"), mobjValues.StringToType(.QueryString.Item("cbeModulec"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("cbeCover"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nTransaction"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nConsecutive"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("tcnDetail_Item"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("hddType"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("tcnEndorsementValue"), eFunctions.Values.eTypeData.etdDouble)) Then
                    lstrContent = lclsCreditor_information.sContent
                End If
            End If
            If Request.QueryString.Item("sIsFACOB") = "1" Then
                Response.Write(mobjGrid.DoFormUpd(.QueryString.Item("Action"), "valPolicySeq.aspx", "CA061", .QueryString.Item("nMainAction"), mobjValues.ActionQuery, CShort(.QueryString.Item("Index"))))
            ElseIf Request.QueryString.Item("sIsFACOB") = "2" Then
                Response.Write(mobjGrid_det.DoFormUpd(.QueryString.Item("Action"), "valPolicySeq.aspx", "CA061", .QueryString.Item("nMainAction"), mobjValues.ActionQuery, CShort(.QueryString.Item("Index"))))
            End If

        End With
        lclsCreditor_information = Nothing
    End Sub

</script>
<%Response.Expires = -1441
    mobjNetFrameWork = New eNetFrameWork.Layout
    mobjNetFrameWork.sSessionID = Session.SessionID
    mobjNetFrameWork.nUsercode = Session("nUsercode")
    Call mobjNetFrameWork.BeginPage("CA061")
    '~End Header Block VisualTimer Utility
    Response.CacheControl = "private"
    mobjValues = New eFunctions.Values
    '^Begin Body Block VisualTimer Utility 1.1 31/3/03 19.42.04
    mobjValues.sSessionID = Session.SessionID
    mobjValues.nUsercode = Session("nUsercode")
    '~End Body Block VisualTimer Utility

    mobjValues.sCodisplPage = Request.QueryString.Item("sCodispl")
    mobjMenu = New eFunctions.Menues
    '^Begin Body Block VisualTimer Utility 1.1 31/3/03 19.42.04
    mobjMenu.sSessionID = Session.SessionID
    mobjMenu.nUsercode = Session("nUsercode")
    '~End Body Block VisualTimer Utility
    mobjValues.ActionQuery = Session("bQuery")
%>
<script type="text/javascript">
  
    //%	ReloadPage: recarga la página en caso de cambiar el tipo de comisión o el porcentaje de
    //%				comisión fija
    //-------------------------------------------------------------------------------------------
    function ReloadPage() {
        //-------------------------------------------------------------------------------------------

        //var lstrLocation = self.document.location.href;
       // lstrLocation = lstrLocation.replace(/&nCommityp.*/, "");
       // lstrLocation = lstrLocation.replace(/&ReloadAction.*/, "");
       // lstrLocation = lstrLocation + "&nCommityp=" + self.document.forms[0].cbeType.value + "&nPercent=" + self.document.forms[0].tcnPercentCF.value + "&sChangeCom=1";
       // self.document.location.href = lstrLocation;
    }

  

              //%insChangeModulec: se controla el cambio de valor del campo "Módulo"
    //--------------------------------------------------------------------------------------------------
    function insChangeModulec(Field){
    //--------------------------------------------------------------------------------------------------
        with (self.document.forms[0]) {
             if (Field.value==""){
                  cbeCover.Parameters.Param5.sValue = 0;}
            else
             {   cbeCover.Parameters.Param5.sValue = Field.value;}

	            <%
                If Request.QueryString.Item("Action") <> "Update" Then
	                %>
        	     
		                cbeCover.value="";
		                UpdateDiv("cbeCoverDesc","");
                <%End If%>
	    }
    }

    
              //%insChangeModulec: se controla el cambio de valor del campo "Módulo"
    //--------------------------------------------------------------------------------------------------
    function insChangeDetail(Field){
    //--------------------------------------------------------------------------------------------------
        with (self.document.forms[0]) {
               hddType.value  =  cbeDetail_Item_nType.value;
               tcnEndorsementValue.value  =     cbeDetail_Item_nCapital.value;
        }
    }


               //%insChangeModulec: se controla el cambio de valor del campo "Módulo"
    //--------------------------------------------------------------------------------------------------
    function InvokeSetHeader(nTotal ){
    //--------------------------------------------------------------------------------------------------
        with (self.document.forms[0]) {
            tcnEndorsementValue.value  = hddAmount.value ; 
            tcdIniDate.value  = hddIniDate.value ; 
            tcdEndDate.value  = hddEndDate.value ; 

               }
    }

    function setDate()
    {
        self.document.forms[0].tcdEffecdate.value = self.document.forms[0].tcdEffecdate2.value;
        self.document.forms[0].tcdEffecdate.onchange();
    }

    function insReload()
    {
        UpdateDiv('lblWaitProcess', '<MARQUEE>Procesando, por favor espere...</MARQUEE>', '');
        var lstrUrl = '';
        lstrUrl = document.location.href;
        lstrUrl = lstrUrl.replace(/&tcdEffecdate=.*/,'') + "&tcdEffecdate=" + self.document.forms[0].tcdEffecdate.value;
        document.location.href = lstrUrl;
         self.parent.top.frames['fraFolder'].resValues.marqueeMessage = ''; 
    }
     

</script>
<html>
<head>
    <meta name="GENERATOR" content="Microsoft Visual Studio 6.0">
    <script type="text/javascript" src="/VTimeNet/Scripts/GenFunctions.js"></script>
    <%
        With Response
            .Write(mobjValues.StyleSheet())
            If Request.QueryString.Item("Type") <> "PopUp" Then
                .Write(mobjMenu.setZone(2, "CA061", Request.QueryString.Item("sWindowDescript"), CShort(Request.QueryString.Item("nWindowTy"))))
                .Write("<script>var nMainAction=top.frames['fraSequence'].plngMainAction</script>")
            End If
        End With
        mobjMenu = Nothing
    %>
</head>
<body onunload="closeWindows();">
    <form method="POST" id="CA061" name="frmCA061" action="valPolicySeq.aspx?bAll=True">
    <%
        Response.Write(mobjValues.ShowWindowsName(Request.QueryString.Item("sCodispl"), Request.QueryString.Item("sWindowDescript")))
        Call insDefineHeader()
        Call insDefineHeader_det()
        If Request.QueryString.Item("Type") = "PopUp" Then
            Call insPreCA061Upd()
        Else
            Call insPreCA061()
        End If
        mobjGrid = Nothing
        mobjValues = Nothing
    %>
    </form>
</body>
</html>
<%'^Begin Footer Block VisualTimer Utility 1.1 31/3/03 19.42.04
    Call mobjNetFrameWork.FinishPage("CA061")
    mobjNetFrameWork = Nothing
    '^End Footer Block VisualTimer%>
