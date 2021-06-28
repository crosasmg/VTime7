<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="ePolicy" %>
<script language="VB" runat="Server">
'^Begin Header Block VisualTimer Utility 1.1 31/3/03 19.42.02
Dim mobjNetFrameWork As eNetFrameWork.Layout

'- Objeto para el manejo de las funciones generales de carga de valores  
Dim mobjValues As eFunctions.Values
Dim mobjMenu As eFunctions.Menues
Dim mobjGrid As eFunctions.Grid
Dim mintTariff As Object
Dim mstrInsured As String
Dim mstrOptExc As String
Dim mstrType_exc As String



'%insDefineHeader: define el header del grid a mostrara en la página de los módulos activos e inactivos en el sistema
'--------------------------------------------------------------------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'--------------------------------------------------------------------------------------------------------------------------------------------
        Dim EfectoCause As Short
        Dim lblnModul As Boolean
        Dim lblnDisabledPreExists As Boolean
        If mstrType_exc = "2" Then
            EfectoCause = 3
        Else
            EfectoCause = 0
        End If
	
        Dim lclsProduct As eProduct.Product
        lclsProduct = New eProduct.Product
        lblnModul = True
        lblnDisabledPreExists = False
        If lclsProduct.IsModule(Session("nBranch"), Session("nProduct"), Session("dEffecdate")) Then
            lblnModul = False
        End If
        
        If (Session("sPoliType") = "1" Or Session("nCertif") > 0) And Session("sBrancht") = "7" Then
            Dim lclsHealth As eBranches.Health
            lclsHealth = New eBranches.Health
            Dim lintDaysCount As Integer
            If lclsHealth.Find(Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), Session("dEffecdate")) Then
                If lclsHealth.sWait_type <> "1" Then
                    Dim lclsPolicy As ePolicy.Policy
                    lclsPolicy = New ePolicy.Policy
                    If lclsPolicy.Find(Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy")) Then
                        lintDaysCount = lclsHealth.nWait_quan
                        '+ Si el plazo de espera es Horas 
                        If lclsHealth.sWait_type = "2" Then
                            lintDaysCount = DateDiff(DateInterval.Day, lclsPolicy.dStartdate, DateAdd(DateInterval.Hour, lclsHealth.nWait_quan, lclsPolicy.dStartdate))
                        End If
                        '+ Si el plazo de espera es Meses
                        If lclsHealth.sWait_type = "4" Then
                            lintDaysCount = DateDiff(DateInterval.Day, lclsPolicy.dStartdate, DateAdd(DateInterval.Month, lclsHealth.nWait_quan, lclsPolicy.dStartdate))
                        End If
                        '+ Se debe obtener el Plazo de espera registrado en la póliza matriz y llevarlo a días.                        
                        '+ Para obtener la fecha límite de elegibilidad se debe sumar el número de días del Plazo de espera a la fecha 
                        '+ de inicio de vigencia de la póliza matriz.
                        '+ Si la fecha de inicio del certificado en tratamiento no es mayor que la fecha límite de elegibilidad se debe deshabilitar 
                        '+ el check de pre-existencias en la parte repetitiva de esta ventana.
                        If (lclsPolicy.dStartdate < lclsPolicy.dStartdate.AddDays(lintDaysCount)) Then
                            lblnDisabledPreExists = True
                        End If
                    End If
                    lclsPolicy = Nothing
                End If
                lclsHealth = Nothing
            End If
        End If
        
        With mobjGrid.Columns
            .AddCheckColumn(0, GetLocalResourceObject("optType_exc_1Caption"), "chkExclud", vbNullString, 1, , "insChangeCheck(""chkExclud"")", Request.QueryString.Item("Action") = "Update", GetLocalResourceObject("optType_exc_1ToolTip"))
            .AddCheckColumn(0, GetLocalResourceObject("optType_exc_2Caption"), "chkPreExist", vbNullString, , , "insChangeCheck(""chkPreExist"")", Request.QueryString.Item("Action") = "Update" Or lblnDisabledPreExists, GetLocalResourceObject("optType_exc_2ToolTip"))
            If Request.QueryString.Item("Type") = "PopUp" And Request.QueryString.Item("Action") = "Add" Then
                .AddPossiblesColumn(0, GetLocalResourceObject("cbeIllnessColumnCaption"), "cbeIllness", "TabTab_am_ill", eFunctions.Values.eValuesType.clngWindowType, , True, , , , , Request.QueryString.Item("Action") = "Update", 8, GetLocalResourceObject("cbeIllnessColumnToolTip"))
            Else
                .AddPossiblesColumn(0, GetLocalResourceObject("cbeIllnessColumnCaption"), "cbeIllness", "Tab_am_ill", eFunctions.Values.eValuesType.clngWindowType, , False, , , , , Request.QueryString.Item("Action") = "Update", 8, GetLocalResourceObject("cbeIllnessColumnToolTip"))
            End If
            .AddPossiblesColumn(0, GetLocalResourceObject("cbeModulecCaption"), "cbeModulec", "tabtab_modul", eFunctions.Values.eValuesType.clngComboType, , True, , , , "self.document.forms[0].valCover.Parameters.Param3.sValue=this.value;", lblnModul Or Request.QueryString.Item("Action") = "Update", 4, GetLocalResourceObject("cbeModulecToolTip"))
            .AddPossiblesColumn(0, GetLocalResourceObject("valCoverCaption"), "valCover", "tablife_covmod", eFunctions.Values.eValuesType.clngWindowType, , True, , , , , Request.QueryString.Item("Action") = "Update", 4, GetLocalResourceObject("valCoverToolTip"))
            .AddPossiblesColumn(0, GetLocalResourceObject("cbeExc_CodeColumnCaption"), "cbeExc_Code", "Table271", eFunctions.Values.eValuesType.clngWindowType, EfectoCause, , , , , , Request.QueryString.Item("Action") = "Update")
            .AddDateColumn(0, GetLocalResourceObject("tcdDateIniColumnCaption"), "tcdDateIni", CStr(Today), , GetLocalResourceObject("tcdDateIniColumnToolTip"), , , , Request.QueryString.Item("Action") = "Update")
            .AddDateColumn(0, GetLocalResourceObject("tcdDateEndColumnCaption"), "tcdDateEnd", , , GetLocalResourceObject("tcdDateEndColumnToolTip"), , , , mstrType_exc = "2")
		
            .AddHiddenColumn("hddnId", vbNullString)
            .AddHiddenColumn("sParam", vbNullString)
        End With
	'+ Se definen las propiedades generales del grid
	
        If Request.QueryString.Item("Type") = "PopUp" And Request.QueryString.Item("Action") = "Add" Then
            With mobjGrid.Columns("cbeIllness")
                .Parameters.Add("nBranch", Session("nBranch"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nProduct", Session("nProduct"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nPolicy", Session("nPolicy"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nCertif", Session("nCertif"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("dEffecdate", Session("dEffecdate"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("sClient", Request.QueryString.Item("sInsured"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            End With
        End If
        
        With mobjGrid.Columns("cbeModulec")
            Call .Parameters.Add("nBranch", Session("nBranch"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            Call .Parameters.Add("nProduct", Session("nProduct"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            Call .Parameters.Add("dEffecdate", Session("deffecdate"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
        End With
        
        With mobjGrid.Columns("valCover")
            Call .Parameters.Add("nBranch", Session("nBranch"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            Call .Parameters.Add("nProduct", Session("nProduct"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            Call .Parameters.Add("nModulec", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            Call .Parameters.Add("dEffecdate", Session("deffecdate"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
        End With

        With mobjGrid
            .Codispl = "AM006"
            .Width = 430
            .Height = 370
            .WidthDelete = 450
            .UpdContent = True
            
            If mintTariff = vbNullString Then
                mintTariff = 0
            End If

            .AddButton = True

            .ActionQuery = mobjValues.ActionQuery
            .nMainAction = CShort(IIf(Request.QueryString.Item("nMainAction") = "", 0, Request.QueryString.Item("nMainAction")))
            .ActionQuery = CDbl(IIf(Request.QueryString.Item("nMainAction") = "", 0, Request.QueryString.Item("nMainAction"))) = 401
            .bOnlyForQuery = mobjValues.ActionQuery
            .Columns("cbeIllness").EditRecord = True
		
            .sDelRecordParam = "' + marrArray[lintIndex].sParam + '"
		
            .sEditRecordParam = "nTariff=" & mintTariff & "&sInsured=' + document.forms[0].valInsured.value + '" & "&sTypeExclu=' + (document.forms[0].optTypeExclu[0].checked==true?'1':'2') + '" & "&sOptExc=" & mstrOptExc & "&sOptType_exc=" & mstrType_exc
            
            If Request.QueryString.Item("Reload") = "1" Then
                .sReloadIndex = Request.QueryString.Item("ReloadIndex")
            End If
        End With
    End Sub

'% insPreAM006Upd: Se muetra la ventana Popup para efecto de actualización del Gird
'--------------------------------------------------------------------------------------------
Private Sub insPreAM006Upd()
	'--------------------------------------------------------------------------------------------
	Dim lclsTab_am_exc As ePolicy.Tab_am_exc
	Dim lstrContent As String
	
	Response.Write(mobjValues.HiddenControl("hddsClient", Request.QueryString.Item("sInsured")))
	Response.Write(mobjValues.HiddenControl("hddTypeExclu", Request.QueryString.Item("sTypeExclu")))
	Response.Write(mobjValues.HiddenControl("hddOptType_exc", Request.QueryString.Item("sOptType_exc")))
		
	If Request.QueryString.Item("Action") = "Del" Then
		lclsTab_am_exc = New ePolicy.Tab_am_exc
		Response.Write(mobjValues.ConfirmDelete)
		If CStr(Session("sPolitype")) = "2" And Session("ncertif") = 0 Then
			If mintTariff = vbNullString Then
				mintTariff = 0
			End If
		End If
            Call lclsTab_am_exc.InsPostAM006Upd("Delete", Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), mintTariff, Request.QueryString.Item("nIllness"), Request.QueryString.Item("sInsured"), mobjValues.StringToType(Request.QueryString.Item("nId"), eFunctions.Values.eTypeData.etdDouble), Session("nUsercode"), Session("dEffecdate"), Session("nTransaction"), Request.QueryString.Item("sOptType_exc"), , Session("dNulldate"))
		lstrContent = lclsTab_am_exc.sContent
		lclsTab_am_exc = Nothing
	End If
	With Request
		Response.Write(mobjGrid.DoFormUpd(.QueryString.Item("Action"), "valPolicySeq.aspx", .QueryString.Item("sCodispl"), .QueryString.Item("nMainAction"), mobjValues.ActionQuery, CShort(.QueryString.Item("Index")), lstrContent))
	End With
End Sub

'% insPreAM006: hace la lectura de los campos a mostrar en pantalla
'----------------------------------------------------------------------------
Private Sub insPreAM006()
	'----------------------------------------------------------------------------
	Dim lstrQueryString As String
	Dim lstrChecked As String
	Dim lstrCheckedExc1 As String
	Dim lstrCheckedExc2 As String
	
Response.Write("" & vbCrLf)
Response.Write("    <TABLE WIDTH=""100%"">" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("			")
	
	If Request.QueryString.Item("sOptType_exc") = vbNullString Then
		lstrCheckedExc1 = "1"
		lstrCheckedExc2 = "2"
	Else
		If Request.QueryString.Item("sOptType_exc") = "1" Then
			lstrCheckedExc1 = "1"
			lstrCheckedExc2 = "2"
		Else
			lstrCheckedExc2 = "1"
			lstrCheckedExc1 = "2"
		End If
	End If
        
        '+ Las opciones de Exclusion/Pre-existencia ya no se muestran de la zona puntual, se pasan a la zona de detalle.
        If 1 = 1 Then
            Response.Write("            <TD colspan=""2""></TD>" & vbCrLf)
        Else
            Response.Write("        " & vbCrLf)
            Response.Write("            <TD>")


            Response.Write(mobjValues.OptionControl(0, "optType_exc", GetLocalResourceObject("optType_exc_1Caption"), lstrCheckedExc1, "1", "insChangeValue(""optType_exc"")", , , GetLocalResourceObject("optType_exc_1ToolTip")))


            Response.Write("</TD>" & vbCrLf)
            Response.Write("            <TD>")


            Response.Write(mobjValues.OptionControl(0, "optType_exc", GetLocalResourceObject("optType_exc_2Caption"), lstrCheckedExc2, "2", "insChangeValue(""optType_exc"")", , , GetLocalResourceObject("optType_exc_2ToolTip")))


            Response.Write("</TD>" & vbCrLf)
        End If
        Response.Write("            <TD WIDTH=""25%"" COLSPAN=""1"" CLASS=""HIGHLIGHTED""><LABEL>" & GetLocalResourceObject("AnchorCaption") & "</LABEL></TD>            " & vbCrLf)
        Response.Write("        </TR>" & vbCrLf)
        Response.Write("        <TR>" & vbCrLf)
        Response.Write("            <TD></TD>" & vbCrLf)
        Response.Write("            <TD></TD>" & vbCrLf)
        Response.Write("            <TD COLSPAN=""1"" CLASS=""HORLINE""></TD>" & vbCrLf)
        Response.Write("        </TR>" & vbCrLf)
        Response.Write("        <TR>" & vbCrLf)
        Response.Write("        <TD Width=""10%""><LABEL ID=0>" & GetLocalResourceObject("cbeTariffCaption") & "</LABEL></TD>" & vbCrLf)
        Response.Write("        <TD>" & vbCrLf)
        Response.Write("            ")

	
        With mobjValues
            .Parameters.Add("nBranch", Session("nBranch"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", Session("nProduct"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", Session("dEffecdate"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            '+ Si se trata del tratamiento de un certificado se muestran las tarifas de la Poliza matriz (tar_am_bas) sino las del producto (tar_am_basprod).
            If Session("nTransaction") <> eCollection.Premium.PolTransac.clngTempCertifAmendment And Session("nTransaction") <> eCollection.Premium.PolTransac.clngCertifIssue And Session("nTransaction") <> eCollection.Premium.PolTransac.clngCertifAmendment Then
                .Parameters.Add("nPolicy", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("sCertype", vbNullString, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            Else
                .Parameters.Add("nPolicy", Session("nPolicy"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("sCertype", Session("sCertype"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            End If
        End With
        Response.Write(mobjValues.PossiblesValues("cbeTariff", "tabTar_am_basprod", eFunctions.Values.eValuesType.clngComboType, mintTariff, True, , , , , "insChangeValue(""Tariff"")", True))
	
        Response.Write("" & vbCrLf)
        Response.Write("        </TD>" & vbCrLf)
        
        If Request.QueryString.Item("sTypeExclu") = "1" Or _
           Request.QueryString.Item("sTypeExclu") = vbNullString Then
            lstrChecked = "1"
        Else
            lstrChecked = "2"
        End If
        
        '+ Si la póliza es individual
        If CStr(Session("sPolitype")) = "1" Then
		
            Response.Write("<TD>")
                
            Response.Write(mobjValues.OptionControl(0, "optTypeExclu", GetLocalResourceObject("optTypeExclu_1Caption"), lstrChecked, "1", "insChangeValue(""OptExc"")", , , GetLocalResourceObject("optTypeExclu_1ToolTip")))


            Response.Write("</TD>")

		
            '+ Si la póliza es matriz
        Else
            If Session("nCertif") = 0 Then
			
                Response.Write("<TD>")


                Response.Write(mobjValues.OptionControl(0, "optTypeExclu", GetLocalResourceObject("optTypeExclu_1Caption"), lstrChecked, "1", "insChangeValue(""OptExc"")", , , GetLocalResourceObject("optTypeExclu_1ToolTip")))


                Response.Write("</TD>")

			
            Else
                '+ Si se trata de un certificado
			
                Response.Write("<TD>")


                Response.Write(mobjValues.OptionControl(0, "optTypeExclu", GetLocalResourceObject("optTypeExclu_1Caption"), lstrChecked, "1", "insChangeValue(""OptExc"")", , , GetLocalResourceObject("optTypeExclu_1ToolTip")))


                Response.Write("</TD>")

			
            End If
        End If
        
        Response.Write("" & vbCrLf)
        Response.Write("        <TR>" & vbCrLf)
        Response.Write("            <TD><LABEL ID=0>" & GetLocalResourceObject("valInsuredCaption") & "</LABEL></TD>" & vbCrLf)
        Response.Write("            <TD>" & vbCrLf)
        Response.Write("            ")

	
        lstrQueryString = "&sCertype=" & Session("sCertype") & "&nBranch=" & Session("nBranch") & "&nProduct=" & Session("nProduct") & "&nPolicy=" & Session("nPolicy") & "&nCertif=" & Session("nCertif") & "&dEffecdate=" & Session("dEffecdate")
        Response.Write(mobjValues.ClientControl("valInsured", mstrInsured, , GetLocalResourceObject("valInsuredToolTip"), "insChangeValue(""Insured"");", Not ((CStr(Session("sPoliType")) = "1" Or Session("nCertif") > 0) And (Request.QueryString.Item("sTypeExclu") = "2")), , , , , , eFunctions.Values.eTypeClient.SearchClientPolicy, , , , lstrQueryString))
	
        Response.Write("" & vbCrLf)
        Response.Write("            </TD>" & vbCrLf)
        Response.Write("			")

	
        If Request.QueryString.Item("sTypeExclu") = "2" Then
            lstrChecked = "1"
        Else
            lstrChecked = "2"
        End If
	
        Response.Write("" & vbCrLf)
        Response.Write("			<TD>")


        Response.Write(mobjValues.OptionControl(0, "optTypeExclu", GetLocalResourceObject("optTypeExclu_2Caption"), lstrChecked, "2", "insChangeValue(""OptExc"")", Not (CStr(Session("sPoliType")) = "1" Or Session("nCertif") > 0), , GetLocalResourceObject("optTypeExclu_2ToolTip")))


        Response.Write("</TD>" & vbCrLf)

                
        Response.Write("        </TR>" & vbCrLf)
        Response.Write("    </TABLE>" & vbCrLf)
        Response.Write("    ")

        insDefineGrid()
        Response.Write("")

    End Sub
'%insDefineGrid: define el grid según lo leído de las tablas incolucradas
'----------------------------------------------------------------------------------------------
Private Sub insDefineGrid()
	'----------------------------------------------------------------------------------------------
	Dim lcolTab_Am_Excs As ePolicy.Tab_am_excs
	Dim lclsTab_am_exc As ePolicy.Tab_am_exc
	Dim lblnExist As Boolean
	Dim llngIndex As Integer
        Dim sClient_aux As String
        Dim lstrType_exc As String
        
	lcolTab_Am_Excs = New ePolicy.Tab_am_excs
	llngIndex = 0
	If CStr(Session("sPolitype")) = "2" And Session("ncertif") = 0 Then
		mintTariff = 0
		sClient_aux = ""
	End If
	
	If Not (CStr(Session("sPolitype")) = "2" And Session("ncertif") = 0) Then
		If CDbl(Request.QueryString.Item("sTypeExclu")) = 2 Or IsNothing(Request.QueryString.Item("sTypeExclu")) Then
			If IsNothing(Request.QueryString.Item("sInsured")) Then
                    sClient_aux = vbNullString
			Else
				sClient_aux = Request.QueryString.Item("sInsured")
			End If
		Else
			sClient_aux = Request.QueryString.Item("sInsured")
		End If
	End If
	
        If lcolTab_Am_Excs.Find(Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), eRemoteDB.Constants.intNull, Session("dEffecdate"), sClient_aux, vbNullString) Then
		
            lblnExist = True
            mobjGrid.sEditRecordParam = mobjGrid.sEditRecordParam & "&nCount=" & lcolTab_Am_Excs.Count
            For llngIndex = 1 To lcolTab_Am_Excs.Count
                With mobjGrid
                    .Columns("tcdDateIni").DefValue = CStr(lcolTab_Am_Excs.Item(llngIndex).dInit_date)
                    .Columns("tcdDateEnd").DefValue = CStr(lcolTab_Am_Excs.Item(llngIndex).dEnd_date)
                    .Columns("cbeExc_Code").DefValue = CStr(lcolTab_Am_Excs.Item(llngIndex).nExc_code)
                    .Columns("cbeIllness").DefValue = lcolTab_Am_Excs.Item(llngIndex).sIllness
                    .Columns("hddnId").DefValue = CStr(lcolTab_Am_Excs.Item(llngIndex).nId)
                    .Columns("chkExclud").Disabled = True
                    .Columns("chkPreExist").Disabled = True
                    lstrType_exc = "1"
                    If lcolTab_Am_Excs.Item(llngIndex).sType_exc = "1" Then
                        .Columns("chkExclud").Checked = "1"
                        .Columns("chkPreExist").Checked = "2"
                        lstrType_exc = "1"
                    End If
                    If lcolTab_Am_Excs.Item(llngIndex).sType_exc = "2" Then
                        .Columns("chkExclud").Checked = "2"
                        .Columns("chkPreExist").Checked = "1"
                        lstrType_exc = "2"
                    End If
                    
                    .Columns("cbeModulec").Parameters.Add("nBranch", Session("nBranch"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Columns("cbeModulec").Parameters.Add("nProduct", Session("nProduct"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Columns("cbeModulec").Parameters.Add("dEffecdate", Session("dEffecdate"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				
                    .Columns("valCover").Parameters.Add("nBranch", Session("nBranch"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Columns("valCover").Parameters.Add("nProduct", Session("nProduct"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Columns("valCover").Parameters.Add("nModulec", lcolTab_Am_Excs.Item(llngIndex).nModulec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Columns("valCover").Parameters.Add("dEffecdate", Session("deffecdate"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				                    
                    .Columns("cbeModulec").DefValue = CStr(lcolTab_Am_Excs.Item(llngIndex).nModulec)
                    .Columns("valCover").DefValue = CStr(lcolTab_Am_Excs.Item(llngIndex).nCover)
                    
                    If CStr(Session("sPolitype")) = "2" And Session("ncertif") = 0 Then
                        If mintTariff = vbNullString Then
                            mintTariff = 0
                        End If
                    End If
                    .Columns("sParam").DefValue = "nTariff=" & mintTariff & "&sInsured=" & mstrInsured & "&dDateIni=" & lcolTab_Am_Excs.Item(llngIndex).dInit_date & "&dDateEnd=" & lcolTab_Am_Excs.Item(llngIndex).dEnd_date & "&nExc_Code=" & lcolTab_Am_Excs.Item(llngIndex).nExc_code & "&nIllness=" & lcolTab_Am_Excs.Item(llngIndex).sIllness & "&nId=" & lcolTab_Am_Excs.Item(llngIndex).nId & "&sOptType_exc=" & lstrType_exc
                    Response.Write(.DoRow)
                End With
            Next
        End If
        Response.Write(mobjGrid.closeTable())
        If IsNothing(Request.QueryString.Item("sTypeExclu")) Then
            If CStr(Session("sPolitype")) = "2" And Session("ncertif") = 0 Then
			
                Response.Write("" & vbCrLf)
                Response.Write("		    <SCRIPT>")
                Response.Write("		    <SCRIPT>  self.document.forms[0].optTypeExclu[0].checked = true;" & vbCrLf)
                Response.Write("		              self.document.forms[0].optTypeExclu[1].checked = false;" & vbCrLf)
                Response.Write("		              self.document.forms[0].valInsured.value = """";" & vbCrLf)
                Response.Write("		              self.document.forms[0].btnvalInsured.disabled = true;" & vbCrLf)
                Response.Write("		              self.document.forms[0].valInsured.disabled = true;" & vbCrLf)
                Response.Write("		              self.document.forms[0].cbeTariff.value = ""0"";" & vbCrLf)
                Response.Write("		              self.document.forms[0].cbeTariff.disabled = true; </" & "SCRIPT>" & vbCrLf)
                Response.Write("		")

            Else
                Response.Write("" & vbCrLf)
                Response.Write("		    <SCRIPT>")
                Response.Write("		    <SCRIPT>  self.document.forms[0].optTypeExclu[0].checked = false;" & vbCrLf)
                Response.Write("		             self.document.forms[0].optTypeExclu[1].checked = true;" & vbCrLf)
                Response.Write("		             self.document.forms[0].btnvalInsured.disabled = false;" & vbCrLf)
                Response.Write("		             self.document.forms[0].valInsured.disabled = false; </" & "SCRIPT>" & vbCrLf)
                Response.Write("		")

            End If
        End If
        If CStr(Session("sPolitype")) = "2" And Session("ncertif") = 0 Then
            If mintTariff = vbNullString Then
                mintTariff = 0
            End If
        End If
        If Not lblnExist And mintTariff > 0 And Not Session("bQuery") Then
            '+ Solamente para el caso de Póliza matriz y póliza individual
            If Session("nCertif") > 0 Then
                '+ Se verifica si existe información para la tarifa, cliente en tratamiento (Póliza matriz -> Certificado=0).
                lclsTab_am_exc = New ePolicy.Tab_am_exc
                If CStr(Session("sPolitype")) = "2" And Session("ncertif") = 0 Then
                    If mintTariff = vbNullString Then
                        mintTariff = 0
                    End If
                End If
                '            If lclsTab_am_exc.IsExist(Session("sCertype"), Session("nBranch"), Session("nProduct"), '                                      Session("nPolicy"), 0, mintTariff, '                                      Session("dEffecdate"), mstrInsured) Then
                '                Response.Write mobjValues.AnimatedButtonControl("btn_Apply","/VTimeNet/images/FindPolicyOff.png", GetLocalResourceObject("btn_ApplyToolTip"),,"InitialValues()")
                '            End If
            Else
                '+ Se verifica si existe información por defecto (información del diseñador) para habilitar el botón.
                If CStr(Session("sPolitype")) = "2" And Session("ncertif") = 0 Then
                    If mintTariff = vbNullString Then
                        mintTariff = 0
                    End If
                End If
                '            Set lclsTab_Am_ExcProd = Server.CreateObject("eBranches.Tab_Am_ExcProd")
                '            If lclsTab_am_excProd.valTab_am_excProd(Session("nBranch"), Session("nProduct"), mintTariff, '                                                    Session("dEffecdate")) Then
                '                Response.Write mobjValues.AnimatedButtonControl("btn_Apply","/VTimeNet/images/FindPolicyOff.png", GetLocalResourceObject("btn_ApplyToolTip"),,"InitialValues()")
                '            End If
            End If
        End If
        lcolTab_Am_Excs = Nothing
        lclsTab_am_exc = Nothing
End Sub

'% insDefaultValues: Se encarga de mostrar la tarifa por defecto seleccionada
'-----------------------------------------------------------------------------------------
Private Sub insDefaultValues()
	'-----------------------------------------------------------------------------------------
	Dim lclsTab_am_exc As ePolicy.Tab_am_exc
	
	lclsTab_am_exc = New ePolicy.Tab_am_exc
	'+ Si no hay una tarifa seleccionada se muestra la que se definió por defecto.
	If Request.QueryString.Item("nTariff") = vbNullString Then
		'+ Obtiene la información por defecto a mostrar
		If lclsTab_am_exc.FindDeftValues(Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), Session("dEffecdate")) Then
			mintTariff = lclsTab_am_exc.nTariff
			mstrInsured = Trim(lclsTab_am_exc.sClient)
		Else
			mintTariff = Request.QueryString.Item("nTariff")
			If CStr(Session("sPolitype")) = "2" And Session("ncertif") = 0 Then
				If mintTariff = vbNullString Then
					mintTariff = 0
				End If
			End If
			mstrInsured = Trim(lclsTab_am_exc.sClient)
		End If
	Else
		mintTariff = mobjValues.StringToType(Request.QueryString.Item("nTariff"), eFunctions.Values.eTypeData.etdDouble)
		If CStr(Session("sPolitype")) = "2" And Session("ncertif") = 0 Then
			If mintTariff = vbNullString Then
				mintTariff = 0
			End If
		End If
		mstrInsured = Request.QueryString.Item("sInsured")
		mstrOptExc = Request.QueryString.Item("sOptExc")
	End If
	
	If mstrInsured = vbNullString Then
		mstrOptExc = "1"
	Else
		mstrOptExc = "2"
	End If
	If (Request.QueryString.Item("sOptType_exc") = vbNullString) Then
		mstrType_exc = "1"
	Else
		mstrType_exc = Request.QueryString.Item("sOptType_exc")
	End If
	
	
	lclsTab_am_exc = Nothing
End Sub

'% insReaInitial: Se encarga de inicializar las variables de trabajo
'-----------------------------------------------------------------------------------------
Private Sub insReaInitial()
	'-----------------------------------------------------------------------------------------
	'    If Request.QueryString("nTariff") = vbNullString Then
	'        mintTariffChange = 0
	'    Else
	'        mintTariffChange = Request.QueryString("nTariff")
	'    End If
	'    If Trim(Request.QueryString("sInsured")) = vbNullString Then
	'        mstrInsuredChange = 0
	'    Else
	'        mstrInsuredChange = Request.QueryString("sInsured")
	'    End If
	'    If Request.QueryString("sOptExc") = vbNullString Then
	'        mstrOptExcChange = 0
	'    Else
	'        mstrOptExcChange = Request.QueryString("sOptExc")
	'    End If
	'    With Response
	'        .Write "<NOTSCRIPT>"
	'        .Write "var mintTariffChange = " & CStr(mintTariffChange) & ";"
	'        .Write "var mstrInsuredChange = " & CStr(mstrInsuredChange) & ";"
	'        .Write "var mstrOptExcChange = " & CStr(mstrOptExcChange) & ";"
	'        .Write "</" & "Script>"
	'    End With
End Sub

</script>
<%Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage("AM006")
'~End Header Block VisualTimer Utility
Response.CacheControl = "private"

mobjMenu = New eFunctions.Menues
'^Begin Body Block VisualTimer Utility 1.1 31/3/03 19.42.02
mobjMenu.sSessionID = Session.SessionID
mobjMenu.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility
mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 31/3/03 19.42.02
mobjValues.sSessionID = Session.SessionID
mobjValues.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjValues.sCodisplPage = Request.QueryString.Item("sCodispl")
mobjGrid = New eFunctions.Grid
'^Begin Body Block VisualTimer Utility 1.1 31/3/03 19.42.02
mobjGrid.sSessionID = Session.SessionID
mobjGrid.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjGrid.sCodisplPage = Request.QueryString.Item("sCodispl")
Call mobjGrid.SetWindowParameters(Request.QueryString.Item("sCodispl"), Request.QueryString.Item("sWindowDescript"), Request.QueryString.Item("nWindowTy"))

    mobjValues.ActionQuery = Session("bQuery")

%>
<HTML>
<HEAD>
<META NAME = "GENERATOR" CONTENT = "Microsoft Visual Studio 6.0">
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/Constantes.js"></SCRIPT>




<%
With Response
	.Write(mobjValues.ShowWindowsName(Request.QueryString.Item("sCodispl"), Request.QueryString.Item("sWindowDescript")))
	.Write(mobjValues.StyleSheet())
	If Request.QueryString.Item("Type") <> "PopUp" Then
		.Write(mobjMenu.setZone(2, "AM006", "AM006" & Request.QueryString.Item("sWindowDescript"), CShort(Request.QueryString.Item("nWindowTy"))))
		.Write("<SCRIPT>var nMainAction=top.frames['fraSequence'].plngMainAction;</SCRIPT>")
	End If
End With
mobjMenu = Nothing
Call insReaInitial()
Call insDefaultValues()
%>
<SCRIPT>
//- Variable para el control de versiones
    document.VssVersion="$$Revision: 3 $|$$Date: 15/10/03 16:48 $|$$Author: Nvaplat61 $"

//%insChangeTariff: Se recarga la página cuando se cambia el valor del campo asegurado.
//-------------------------------------------------------------------------------------------
function insChangeValue(Field){
//-------------------------------------------------------------------------------------------
    var lstrOptExc = (document.forms[0].optTypeExclu[0].checked == true ? '1' : '2');
    //var lstrOptType_exc = (document.forms[0].optType_exc[0].checked == true ? '1' : '2');
    var lstrOptType_exc = '1';

    with (document.forms[0]) {
        switch (Field){
//+ Campo tarifa.
            case "Tariff":
                getNewQuery (cbeTariff.value, valInsured.value, lstrOptExc, lstrOptType_exc);
                break;
//+ Campo Asegurado.
            case "Insured":
                if (cbeTariff.value>0){
                    valInsured.value = InsValuesCero(valInsured);
                    getNewQuery (cbeTariff.value, valInsured.value, lstrOptExc,lstrOptType_exc);
                }
                break;

//+ Campo tipo de exclusión.
            case "OptExc":
//+ Si el campo tarifa tiene valor
                if (cbeTariff.value>0){
                    if (optTypeExclu[0].checked==true){
						valInsured.value="";
						valInsured.disabled=true;
						valInsured_Digit.disabled=true;
						valInsured_Digit.value="";
						UpdateDiv("valInsured_Name","")
						btnvalInsured.disabled=true;
                        getNewQuery (cbeTariff.value, "", "1", lstrOptType_exc);
					}
					else{
//						valInsured.value="";
//						valInsured_Digit.value="";
						valInsured.disabled=false;
						valInsured_Digit.disabled=false;
						UpdateDiv("valInsured_Name","")
						btnvalInsured.disabled=false;
                        getNewQuery (cbeTariff.value, valInsured.value, "2", lstrOptType_exc);
					}
				}
                else{
                    //+ Si el campo tarifa no tiene valor se habilita o deshabilita el campo Asegurado
                    if (optTypeExclu[0].checked==true){
                          valInsured.value="";
                          valInsured.disabled=true;
						  valInsured_Digit.disabled=true;
						  valInsured_Digit.value="";
						  UpdateDiv("valInsured_Name","")
                          btnvalInsured.disabled=true;
                          getNewQuery (cbeTariff.value, "", "1",lstrOptType_exc);
                    }
                    else{
//                        valInsured.value="";
//						  valInsured_Digit.value="";
                          valInsured.disabled=false;
						  valInsured_Digit.disabled=false;
						  UpdateDiv("valInsured_Name","")
                          btnvalInsured.disabled=false;
                          getNewQuery (cbeTariff.value, valInsured.value, "2", lstrOptType_exc);
                    }
                }
                break;

//+ Campo tarifa.
            case "optType_exc": 
                 if (lstrOptType_exc==2) 
                 {
                    cbeTariff.value=0;
                    cbeTariff.disabled=true;
                 }
                 else                 
                 {
                    cbeTariff.disabled=false;
                 }                                                              
                 getNewQuery (cbeTariff.value, valInsured.value, lstrOptExc, lstrOptType_exc);
                 break;

            default:
                break;
        }
    }
}

//%insChangeCheck: Se evalua el valor de los checks de exclusion y pre-existencia
//-------------------------------------------------------------------------------------------
function insChangeCheck(Field) {
    //-------------------------------------------------------------------------------------------

    with (document.forms[0]) {
        switch (Field) {
            //+ Exclusión. 
            case "chkExclud":
                chkPreExist.checked = (chkExclud.checked ? false : true);
                break;

            //+ Pre-existencia. 
            case "chkPreExist":
                chkExclud.checked = (chkPreExist.checked ? false : true);
                break;

            default:
                break;
        }
    }
}

//%getNewQuery: Se recarga la página cuando se cambia el valor del campo asegurado.
//-------------------------------------------------------------------------------------------
function getNewQuery(Tariff, Insured, OptExc, OptType_exc){
//-------------------------------------------------------------------------------------------
    var lstrstring = "";
    with (document.forms[0]) {
        lstrstring += document.location;
        lstrstring = lstrstring.replace(/&Reload=.*/, "");
        lstrstring = lstrstring.replace(/&sOptExc=.*/, "");
        lstrstring = lstrstring.replace(/&sInsured=.*/, "");
        lstrstring = lstrstring.replace(/&nTariff=.*/, "");
        lstrstring = lstrstring.replace(/&sOptType_exc=.*/, "");        
        lstrstring = lstrstring + "&nTariff=" + Tariff + "&sInsured=" + Insured + "&sOptExc=" + OptExc + "&sTypeExclu=" + OptExc + "&sOptType_exc=" + OptType_exc;
        document.location = lstrstring;
    }
}

//% InitialValues: se inicializa el grid de la transacción, con los datos definidos en el diseñador
//--------------------------------------------------------------------------------------------
function InitialValues(){
//--------------------------------------------------------------------------------------------
    var lstrQuery
    with (document.forms[0]) {
        lstrQuery = "nTariff=" + cbeTariff.value + "&sInsured=" + valInsured.value
        insDefValues("Tab_am_exc", lstrQuery)
    }
}
</SCRIPT>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" ACTION="valPolicySeq.aspx?nMainAction=<%=Request.QueryString.Item("nMainAction")%>">
<%
insDefineHeader()
If Request.QueryString.Item("Type") = "PopUp" Then
	insPreAM006Upd()
Else
	insPreAM006()
End If
mobjValues = Nothing
mobjGrid = Nothing
%>
</FORM>
</BODY>
</HTML>
<%'^Begin Footer Block VisualTimer Utility 1.1 31/3/03 19.42.02
Call mobjNetFrameWork.FinishPage("AM006")
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>
