<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eBranches" %>
<%@ Import namespace="ePolicy" %>
<%@ Import namespace="eProduct" %>
<script language="VB" runat="Server"> 
'^Begin Header Block VisualTimer Utility 1.1 31/3/03 19.42.02
Dim mobjNetFrameWork As eNetFrameWork.Layout
 
'- Objeto para el manejo de las funciones generales de carga de valores  	
Dim mobjValues As eFunctions.Values
Dim mobjMenu As eFunctions.Menues
Dim mobjGrid As eFunctions.Grid
Dim mclsTar_am_basprod As eBranches.Tar_am_basprod
Dim mclsTar_am_bas As eBranches.Tar_am_bas
Dim mclsTar_am_pol As eBranches.Tar_am_pol
Dim mclsCover As ePolicy.Cover
Dim Exists_Reg As String
Dim mblnGroup As Boolean
Dim mblnChkDefaulti As Boolean
Dim mblnFound As Boolean


Dim mintTariffChange As Object
Dim mintGroupChange As Object
Dim mintRoleChange As Object

Dim mintTariff As Object
Dim mintGroup As Object
Dim mintRole As Object

Dim mintModulec As Object
Dim mintCover As Object

Dim mintModulecChange As Object
Dim mintCoverChange As Object

Dim lclsGroups As ePolicy.Groups


'%insDefineHeader: defines header of grid to showed in the page of the active and inactive 
'% modules in the system
'-------------------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'-------------------------------------------------------------------------------------------
	Dim lstrQuery As String
	If Request.QueryString.Item("Type") <> "PopUp" Then
		If mclsCover.insPreAM002(Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif")) Then
			mblnFound = True
		Else
			mblnFound = False
		End If
	End If
	
	With mobjGrid
		Call .Columns.AddNumericColumn(0, GetLocalResourceObject("tcnAgeInitColumnCaption"), "tcnAgeInit", 5, "", True, GetLocalResourceObject("tcnAgeInitColumnToolTip"),  , 0,  ,  ,  , Request.QueryString.Item("Action") <> "Add")
		Call .Columns.AddNumericColumn(0, GetLocalResourceObject("tcnAgeEndColumnCaption"), "tcnAgeEnd", 5, "", True, GetLocalResourceObject("tcnAgeEndColumnToolTip"),  , 0,  ,  ,  , Request.QueryString.Item("Action") <> "Add")
		Call .Columns.AddPossiblesColumn(0, GetLocalResourceObject("cbeGroupCompColumnCaption"), "cbeGroupComp", "Table268", eFunctions.Values.eValuesType.clngComboType,  ,  ,  ,  ,  ,  , Request.QueryString.Item("Action") <> "Add",  , GetLocalResourceObject("cbeGroupCompColumnCaption"))
            Call .Columns.AddNumericColumn(0, GetLocalResourceObject("tcnCapitalColumnCaption"), "tcnCapital", 18, "", True, GetLocalResourceObject("tcnCapitalColumnToolTip"), True, 6)
            Call .Columns.AddNumericColumn(0, GetLocalResourceObject("tcnPremiumColumnCaption"), "tcnPremium", 18, "", True, GetLocalResourceObject("tcnPremiumColumnCaption"), True, 6, , , , mclsTar_am_basprod.sChanges = "2" Or Not (Session("sPolitype") = 1 Or (Session("sPolitype") = 2 And Session("ncertif") = 0)) And (Request.QueryString.Item("Action") <> "Add"))
            Call .Columns.AddNumericColumn(0, GetLocalResourceObject("tcnGroupDedColumnCaption"), "tcnGroupDed", 18, "", True, GetLocalResourceObject("tcnGroupDedColumnToolTip"), True, 6)
		
		Call .Columns.AddHiddenColumn("tcnTariff", mintTariff)
		Call .Columns.AddHiddenColumn("tcnGroup", mintGroup)
		Call .Columns.AddHiddenColumn("tcnRole", mintRole)
		Call .Columns.AddHiddenColumn("tcnModulec", mintModulec)
		Call .Columns.AddHiddenColumn("tcnCover", mintCover)

		.UpdContent = True
		.Columns("cbeGroupComp").EditRecord = (Session("nCertif") <= 0)
		.Columns("Sel").GridVisible = (Session("nCertif") <= 0)
		.Codispl = "AM002"
		.Width = 320
		.Height = 320
		.WidthDelete = 320
		.AddButton = ((mintTariff <> vbNullString And mintRole <> vbNullString) And Session("nCertif") <= 0) And mblnFound
		
		.DeleteButton = (Session("nCertif") <= 0)
		
		.nMainAction = CShort(IIf(Request.QueryString.Item("nMainAction") ="", 0, Request.QueryString.Item("nMainAction")))
            .ActionQuery = (CDbl(IIf(Request.QueryString.Item("nMainAction") = "", 0, Request.QueryString.Item("nMainAction"))) = 401) Or Session("bQuery")
		
		lstrQuery = "nTariff=' + self.document.forms[0].cbeTariff.value + '" & "&nBenef_type=' + self.document.forms[0].tcnBenefType_H.value + '" & "&nLimit=' + self.document.forms[0].tcnLimit_H.value + '" & "&nDeduc_amount=' + self.document.forms[0].tcnDed_amount_H.value + '" & "&nRole=' + self.document.forms[0].cbeRole.value + '" & "&bGroup=' + self.document.forms[0].lblnGroup.value  + '" & "&nModulec=' + self.document.forms[0].cbeModulec.value + '" & "&nCover=' + self.document.forms[0].valCover.value + '" & "&sWait_type=' + self.document.forms[0].cbeWait_type.value + '" & "&nWait_quan=' + self.document.forms[0].tcnWait_quan.value + '"
		
		'If mblnGroup Then
		If mblnFound Then
			lstrQuery = lstrQuery & "&nGroup=' + self.document.forms[0].cbeGroup.value + '"
		Else
			lstrQuery = lstrQuery & "&nGroup=0"
		End If
		If mblnChkDefaulti Then
			lstrQuery = lstrQuery & "&sDefaulti= ' + self.document.forms[0].chkDefaulti.value + '"
		End If
		
		.sEditRecordParam = lstrQuery
		
		lstrQuery = "nTariff=' + self.document.forms[0].cbeTariff.value + '" & "&nBenef_type=' + self.document.forms[0].tcnBenefType_H.value + '" & "&nLimit=' + self.document.forms[0].tcnLimit_H.value + '" & "&nDeduc_amount=' + self.document.forms[0].tcnDed_amount_H.value + '" & "&nRole=' + self.document.forms[0].cbeRole.value + '" & "&nAgeInit=' + marrArray[lintIndex].tcnAgeInit  + '" & "&nAgeEnd=' + marrArray[lintIndex].tcnAgeEnd  + '" & "&nGroupComp=' + marrArray[lintIndex].cbeGroupComp  + '" & "&nPremium=' + marrArray[lintIndex].tcnPremium  + '" & "&bGroup=' + self.document.forms[0].lblnGroup.value  + '" & "&nGroupDed=' + marrArray[lintIndex].tcnGroupDed  + '" & "&nModulec=' + self.document.forms[0].cbeModulec.value + '" & "&nCover=' + self.document.forms[0].valCover.value + '"
		
		
		'If mblnGroup Then
		If mblnFound Then
			lstrQuery = lstrQuery & "&nGroup=' + self.document.forms[0].cbeGroup.value + '"
		Else
			lstrQuery = lstrQuery & "&nGroup=0"
		End If
		
		If mblnChkDefaulti Then
			lstrQuery = lstrQuery & "&sDefaulti= ' + self.document.forms[0].chkDefaulti.value + '"
		End If
		
		.sDelRecordParam = lstrQuery
		
		If Request.QueryString.Item("Reload") = "1" Then
			.sReloadIndex = Request.QueryString.Item("ReloadIndex")
		End If
	End With
End Sub

'% insPreAM002Upd: Carga los valores de la página AM002
'-------------------------------------------------------------------------------------------
Private Sub insPreAM002Upd()
	Dim lstrContent As String
	'-------------------------------------------------------------------------------------------
	Dim lclsValPolicySeq As ePolicy.ValPolicySeq
	If Request.QueryString.Item("Action") = "Del" Then
		lclsValPolicySeq = New ePolicy.ValPolicySeq
		With Request
			Call lclsValPolicySeq.insPostAM002Upd(Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), mobjValues.StringToType(.QueryString.Item("nTariff"), eFunctions.Values.eTypeData.etdDouble), Session("sDefaulti"), mobjValues.StringToType(.QueryString.Item("nGroup"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nRole"), eFunctions.Values.eTypeData.etdDouble), Session("dEffecdate"), mobjValues.StringToType(.QueryString.Item("nAgeInit"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nAgeEnd"), eFunctions.Values.eTypeData.etdDouble), mobjValues.TypeToString(.QueryString("nGroupComp"), eFunctions.Values.eTypeData.etdDouble), Session("dNullDate"), mobjValues.StringToType(.QueryString.Item("nPremium"), eFunctions.Values.eTypeData.etdDouble), Session("nUsercode"), Session("nTransaction"), "Del", mobjValues.StringToType("", eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nModulec"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nCover"), eFunctions.Values.eTypeData.etdDouble))
		End With
		lstrContent = lclsValPolicySeq.sContent
		lclsValPolicySeq = Nothing
		Response.Write(mobjValues.ConfirmDelete)
	End If
	
        Response.Write(mobjValues.HiddenControl("hddWait_type", Request.QueryString.Item("sWait_type")))
        Response.Write(mobjValues.HiddenControl("hddWait_quan", Request.QueryString.Item("nWait_quan")))
        Response.Write(mobjGrid.DoFormUpd(Request.QueryString.Item("Action"), "valPolicySeq.aspx", "AM002", Request.QueryString.Item("nMainAction"), Session("bQuery"), CShort(Request.QueryString.Item("Index")), lstrContent))
End Sub

'% insPreAM002: Hace la lectura de los campos a mostrar en pantalla
'-------------------------------------------------------------------------------------------
Private Sub insPreAM002()
        '-------------------------------------------------------------------------------------------
        Dim lintCount As Integer
        Dim lblnModul As Boolean
        Dim lclsProduct As eProduct.Product
        Dim lclsHealth As eBranches.Health
        Dim lclsPolicy As ePolicy.Policy
        
        lblnModul = True
	
        lclsProduct = New eProduct.Product
        lclsHealth = New eBranches.Health
        lclsPolicy = New ePolicy.Policy
        
        Call lclsPolicy.Find_TabNameB(Session("nBranch"))
        
        If lclsPolicy.sTabname = "HEALTH" Then
            Call lclsHealth.Find(Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), Session("dEffecdate"))
        
            If Request.QueryString.Item("sWait_type") <> vbNullString Then
                lclsHealth.sWait_type = Request.QueryString.Item("sWait_type")
                lclsHealth.nWait_quan = mobjValues.StringToType(Request.QueryString.Item("nWait_quan"), eFunctions.Values.eTypeData.etdInteger)
            End If
        End If
                
        If lclsProduct.IsModule(Session("nBranch"), Session("nProduct"), Session("dEffecdate")) Then
            lblnModul = False
        End If
	
        Response.Write("" & vbCrLf)
        Response.Write("	<TABLE WIDTH=""100%"">" & vbCrLf)
        Response.Write("		<TR>" & vbCrLf)
        Response.Write("			<TD><LABEL ID=0>" & GetLocalResourceObject("cbeTariffCaption") & "</LABEL></TD>" & vbCrLf)
        Response.Write("			<TD>" & vbCrLf)
        Response.Write("				")

	
        With mobjValues
            .Parameters.Add("nBranch", Session("nBranch"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", Session("nProduct"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", Session("dEffecdate"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            '+ Si se trata del tratamiento de un certificado se muestran las tarifas de la Poliza matriz (tar_am_bas) sino las del producto (tar_am_basprod).
            If (Session("sPolitype") = 1 Or (Session("sPolitype") = 2 And Session("ncertif") = 0)) Then
                .Parameters.Add("nPolicy", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("sCertype", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            Else
                .Parameters.Add("nPolicy", Session("nPolicy"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("sCertype", Session("sCertype"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            End If
        End With
        Response.Write(mobjValues.PossiblesValues("cbeTariff", "tabTar_am_basprod", eFunctions.Values.eValuesType.clngComboType, mintTariff, True, , , , , "insReload(this)", Session("nCertif") <> 0))
	
        Response.Write("" & vbCrLf)
        Response.Write("			</TD>" & vbCrLf)
        Response.Write("			<TD><LABEL ID=0>" & GetLocalResourceObject("cbeBenefTypeCaption") & "</LABEL></TD>" & vbCrLf)
        Response.Write("			<TD>")

        Response.Write(mobjValues.PossiblesValues("cbeBenefType", "Table270", eFunctions.Values.eValuesType.clngComboType, CStr(mclsTar_am_basprod.nBenef_type), , True))
        Response.Write("</TD>" & vbCrLf)
        Response.Write("			    ")

        Response.Write(mobjValues.HiddenControl("tcnBenefType_H", CStr(mclsTar_am_basprod.nBenef_type)))
        Response.Write("  " & vbCrLf)
        Response.Write("			")

	
        '+ Si la póliza es matriz
        If mblnChkDefaulti Then
            '+ Se verifica si hay alguna tarifa asignada por defecto
            lintCount = mclsTar_am_bas.getCountTar_am_bas(Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("dEffecdate"), "1")
            '+ Se obtiene los valores de la tarifa
            If mclsTar_am_bas.FindItem(mintTariff, mintGroup, mintRole) Then
                '+ Si la tarifa es por defecto
                Session("sDefaulti") = mclsTar_am_bas.sDefaulti
                If mclsTar_am_bas.sDefaulti = "1" Then
				
                    Response.Write("" & vbCrLf)
                    Response.Write("						<TD COLSPAN=""2"" ALIGN=""CENTER"">")


                    Response.Write(mobjValues.CheckControl("chkDefaulti", GetLocalResourceObject("chkDefaultiCaption"), "1", "1", "insChangeDefaulti(this)", lintCount <= 0, , GetLocalResourceObject("chkDefaultiToolTip")))


                    Response.Write("</TD>" & vbCrLf)
                    Response.Write("			")

                Else
				
                    Response.Write("" & vbCrLf)
                    Response.Write("						<TD COLSPAN=""2"" ALIGN=""CENTER"">")


                    Response.Write(mobjValues.CheckControl("chkDefaulti", GetLocalResourceObject("chkDefaultiCaption"), "2", "2", "insChangeDefaulti(this)", lintCount > 0, , GetLocalResourceObject("chkDefaultiToolTip")))


                    Response.Write("</TD>" & vbCrLf)
                    Response.Write("			")

                End If
            Else
                If lintCount = 0 Then
                    Session("sDefaulti") = mclsTar_am_basprod.sDefaulti
                    If mclsTar_am_basprod.sDefaulti = "1" Then
					
                        Response.Write("" & vbCrLf)
                        Response.Write("							<TD COLSPAN=""2"" ALIGN=""CENTER"">")


                        Response.Write(mobjValues.CheckControl("chkDefaulti", GetLocalResourceObject("chkDefaultiCaption"), "1", "1", "insChangeDefaulti(this)", lintCount > 0, , GetLocalResourceObject("chkDefaultiToolTip")))


                        Response.Write("</TD>" & vbCrLf)
                        Response.Write("			")

                    Else
                        Response.Write("" & vbCrLf)
                        Response.Write("							<TD COLSPAN=""2"" ALIGN=""CENTER"">")


                        Response.Write(mobjValues.CheckControl("chkDefaulti", GetLocalResourceObject("chkDefaultiCaption"), "", "2", "insChangeDefaulti(this)", lintCount > 0, , GetLocalResourceObject("chkDefaultiToolTip")))


                        Response.Write("</TD>" & vbCrLf)
                        Response.Write("			")

                    End If
                Else
                    Session("sDefaulti") = "2"
				
                    Response.Write("" & vbCrLf)
                    Response.Write("						<TD COLSPAN=""2"" ALIGN=""CENTER"">")


                    Response.Write(mobjValues.CheckControl("chkDefaulti", GetLocalResourceObject("chkDefaultiCaption"), "", "2", "insChangeDefaulti(this)", lintCount > 0))


                    Response.Write("</TD>" & vbCrLf)
                    Response.Write("			")

                End If
            End If
        Else
            Response.Write(mobjValues.HiddenControl("chkDefaulti", ""))
        End If
        Response.Write("" & vbCrLf)
        Response.Write("		</TR>" & vbCrLf)
        Response.Write("		<TR>" & vbCrLf)
        Response.Write("			<TD><LABEL ID=0></LABEL></TD>" & vbCrLf)
        Response.Write("			<TD>" & vbCrLf)
        Response.Write("			    ")

        'Response.Write mobjvalues.NumericControl("tcnLimit", 18, mclsTar_am_basprod.nLimit,,, True,6,true)
        Response.Write("</TD>" & vbCrLf)
        Response.Write("			    ")

        Response.Write(mobjValues.HiddenControl("tcnLimit", CStr(mclsTar_am_basprod.nLimit)))
        Response.Write("" & vbCrLf)
        Response.Write("			    ")

        Response.Write(mobjValues.HiddenControl("tcnLimit_H", CStr(mclsTar_am_basprod.nLimit)))
        Response.Write("" & vbCrLf)
        Response.Write("			<TD><LABEL ID=0></LABEL></TD>" & vbCrLf)
        Response.Write("			<TD>")

        'Response.Write mobjvalues.NumericControl("tcnDed_amount", 18, mclsTar_am_basprod.nDed_amount,,,True,6,True)
        Response.Write("</TD>" & vbCrLf)
        Response.Write("			    ")

        Response.Write(mobjValues.HiddenControl("tcnDed_amount", CStr(mclsTar_am_basprod.nDed_amount)))
        Response.Write("" & vbCrLf)
        Response.Write("			    ")

        Response.Write(mobjValues.HiddenControl("tcnDed_amount_H", CStr(mclsTar_am_basprod.nDed_amount)))
        Response.Write("" & vbCrLf)
        Response.Write("		</TR>" & vbCrLf)
        Response.Write("		<TR>" & vbCrLf)
        Response.Write("		" & vbCrLf)
        Response.Write("		")

        If mclsCover.sTyp_module = "3" And mblnGroup Then
            Response.Write("" & vbCrLf)
            Response.Write("			<TD><LABEL ID=0>" & GetLocalResourceObject("cbeGroupCaption") & "</LABEL></TD>" & vbCrLf)
            Response.Write("			<TD WIDTH=""30%"">" & vbCrLf)
            Response.Write("		")

            mobjValues.ActionQuery = False
            With mobjValues
                .Parameters.Add("sCertype", Session("sCertype"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nBranch", mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nProduct", mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nPolicy", mobjValues.StringToType(Session("nPolicy"), eFunctions.Values.eTypeData.etdDouble), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nCertif", mobjValues.StringToType(Session("nCertif"), eFunctions.Values.eTypeData.etdDouble), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            End With
            Response.Write(mobjValues.PossiblesValues("cbeGroup", "TABGROUPS_CERT", eFunctions.Values.eValuesType.clngWindowType, mintGroup, True, , , , , "insReload(this," & mintTariff & ",this.value," & mintRole & "," & mintModulec & "," & mintCover & ")", Not mblnFound And Session("nCertif") = 0))
            Response.Write(mobjValues.HiddenControl("tcnGroup_H", CStr(mclsTar_am_basprod.nGroup)))
            Response.Write(mobjValues.HiddenControl("lblnGroup", CStr(True)))
		
            Response.Write("</TD>" & vbCrLf)
            Response.Write("		")

        Else
            Response.Write(mobjValues.HiddenControl("lblnGroup", CStr(False)))
            Response.Write(mobjValues.HiddenControl("cbeGroup", "0"))
		
            Response.Write("" & vbCrLf)
            Response.Write("		")

        End If

        Response.Write("			<TD><LABEL ID=0>" & GetLocalResourceObject("cbeRoleCaption") & "</LABEL></TD>" & vbCrLf)
        Response.Write("				<TD>")

	
        With mobjValues
            .Parameters.Add("nBranch", Session("nBranch"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", Session("nProduct"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sPolitype", Session("sPolitype"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sCompon", "", eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
        End With
        Response.Write(mobjValues.PossiblesValues("cbeRole", "tabCliallopro", eFunctions.Values.eValuesType.clngComboType, mintRole, True, , , , , "insReload(this," & mintTariff & "," & mintGroup & ",this.value," & mintModulec & "," & mintCover & ")", Not mblnFound And Session("nCertif") = 0))
        Response.Write(mobjValues.HiddenControl("tcnRole_H", CStr(mclsTar_am_basprod.nRole)))
	
        Response.Write("" & vbCrLf)
        Response.Write("			</TD>" & vbCrLf)
        Response.Write("		</TR>			" & vbCrLf)
        Response.Write("		<TR>			" & vbCrLf)
        
        Response.Write("" & vbCrLf)
        Response.Write("		" & vbCrLf)
        Response.Write("			<TD><LABEL ID=0>" & GetLocalResourceObject("cbeModulecCaption") & "</LABEL></TD>" & vbCrLf)
        Response.Write("	        <TD>" & vbCrLf)
        Response.Write("		    ")

	
        With mobjValues
            Call .Parameters.Add("nBranch", Session("nBranch"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            Call .Parameters.Add("nProduct", Session("nProduct"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            Call .Parameters.Add("dEffecdate", Session("deffecdate"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            Response.Write(mobjValues.PossiblesValues("cbeModulec", "tabtab_modul", eFunctions.Values.eValuesType.clngComboType, mintModulec, True, , , , , "insReload(this," & mintTariff & "," & mintGroup & "," & mintRole & ",this.value," & mintCover & ")", lblnModul, , GetLocalResourceObject("cbeModulecToolTip")))
        End With
	
        Response.Write("" & vbCrLf)
        Response.Write("	        </TD>" & vbCrLf)
        Response.Write("	        <TD><LABEL ID=0>" & GetLocalResourceObject("valCoverCaption") & "</LABEL></TD>" & vbCrLf)
        Response.Write("            <TD>" & vbCrLf)
        Response.Write("		    ")

	
        With mobjValues
            Call .Parameters.Add("nBranch", Session("nBranch"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            Call .Parameters.Add("nProduct", Session("nProduct"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            Call .Parameters.Add("nModulec", mintModulec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            Call .Parameters.Add("dEffecdate", Session("deffecdate"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            Response.Write(mobjValues.PossiblesValues("valCover", "tablife_covmod", eFunctions.Values.eValuesType.clngWindowType, IIf(mintCover = 0, eRemoteDB.Constants.intNull, mintCover), True, , , , , "insReload(this," & mintTariff & "," & mintGroup & "," & mintRole & "," & mintModulec & ",this.value)", , , GetLocalResourceObject("valCoverToolTip"), , , , True))
        End With
	
        Response.Write("</TD>" & vbCrLf)
        Response.Write("		</TR>			" & vbCrLf)
        
        If lclsPolicy.sTabname = "HEALTH" Then
            Response.Write("		<TR>			" & vbCrLf)
            Response.Write("<TD COLSPAN=""4"" CLASS=""HighLighted""><LABEL ID=0><A NAME=""Wait"">" & GetLocalResourceObject("AnchorWaitCaption") & "</A></LABEL></TD>")
            Response.Write("            </TR>" & vbCrLf)
            Response.Write("            <TR>" & vbCrLf)
            Response.Write("                <TD COLSPAN=""4"" CLASS=""Horline""></TD>  " & vbCrLf)
            Response.Write("            </TR>" & vbCrLf)
            Response.Write("            <TR>" & vbCrLf)
            Response.Write("	        <TD><LABEL ID=0>" & GetLocalResourceObject("cbeWait_typeCaption") & "</LABEL></TD>" & vbCrLf)
            mobjValues.BlankPosition = False
            Response.Write("<TD>" & mobjValues.PossiblesValues("cbeWait_type", "Table52", eFunctions.Values.eValuesType.clngComboType, lclsHealth.sWait_type, , , , , , "insChangeWait_type(this);", , , GetLocalResourceObject("cbeWait_typeToolTip")) & "</TD>")
            Response.Write("	        <TD><LABEL ID=0>" & GetLocalResourceObject("tcnWait_quanCaption") & "</LABEL></TD>" & vbCrLf)
            Response.Write("<TD>" & mobjValues.NumericControl("tcnWait_quan", 4, lclsHealth.nWait_quan, , GetLocalResourceObject("tcnWait_quanToolTip")) & "</TD>")
            Response.Write("            </TR>" & vbCrLf)
        
            Response.Write("	</TABLE>")
        Else
            Response.Write(mobjValues.HiddenControl("cbeWait_type", 1))
            Response.Write(mobjValues.HiddenControl("tcnWait_quan", 0))
        End If
	
        insLoadGrid()
	
        Dim lobjError As eFunctions.Errors
        If Not mblnFound And Request.QueryString.Item("nTariff") = vbNullString And Request.QueryString.Item("nGroup") = vbNullString And Request.QueryString.Item("nRole") = vbNullString Then
            If mclsCover.nError > 0 Then
                lobjError = New eFunctions.Errors
                '^Begin Body Block VisualTimer Utility 1.1 31/3/03 19.42.03
                lobjError.sSessionID = Session.SessionID
                lobjError.nUsercode = Session("nUsercode")
                '~End Body Block VisualTimer Utility
                Response.Write(lobjError.ErrorMessage("AM002", mclsCover.nError, , , , True))
                lobjError = Nothing
            End If
		
        End If
        Response.Write(mobjValues.BeginPageButton)
	
        Exists_Reg = "3"
        If mclsTar_am_pol.ValExist_Tar_am_pol(Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("dEffecdate")) Then
            Exists_Reg = "2"
        End If
        Response.Write(mobjValues.UpdContent("AM002", "", Exists_Reg))
	
        If CStr(Session("nCertif")) = "0" Then
            '		Response.Write "<NOTSCRIPT>insReload(" & mintTariff & ")</" & "Script>"
            Response.Write("<SCRIPT>insChangeDefaulti(" & Session("sDefaulti") & ")</" & "Script>")
        End If
	
        mclsTar_am_basprod = Nothing
        mclsTar_am_bas = Nothing
        lclsProduct = Nothing
        lclsHealth = Nothing
    End Sub
'%insLoadGrid: define el grid según lo leído de las tablas involucradas
'%insLoadGrid: defines grid according to the read thing of the involved tables  
'-------------------------------------------------------------------------------------------
Private Sub insLoadGrid()
	'-------------------------------------------------------------------------------------------
        Dim lclsTar_am_pol As eBranches.Tar_am_pol
        Dim lclsTar_am_pol_def As eBranches.Tar_am_pol
	Dim lintIndex As Integer
	Dim lblnExist As Boolean
	'Response.Write "<NOTSCRIPT>alert('entra insLoadGrid');</" & "Script>"
	'+ Se instancian los objetos para poder cargar el grid de valores
	'+ The objects are instancian to be able to load grid of values  
	lclsTar_am_pol = New eBranches.Tar_am_pol

        lblnExist = False
        If lclsTar_am_pol.Load(Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("dEffecdate"), mintTariff, mintGroup, mintRole, mintModulec, mintCover) Then
            lblnExist = True
        End If

        Dim lclsTar_am_detProd As eBranches.Tar_am_detprod
        If Not lblnExist And Not Session("bQuery") Then
            '+ Solamente para el caso de Póliza matriz y póliza individual
            If Session("nCertif") <= 0 Then
                lclsTar_am_detProd = New eBranches.Tar_am_detprod
                '+ Se verifica si existe información por defecto (información del diseñador) para habilitar el botón.
                If lclsTar_am_detProd.valTar_am_detProd(Session("nBranch"), Session("nProduct"), mintTariff, Session("dEffecdate"), mintModulec, mintCover) Then
                    '+ E´l botón para cargar la infromación del producto se deshabilita.  Se cargará siempre la información del producto
                    'Response.Write(mobjValues.AnimatedButtonControl("btn_Apply", "/VTimeNet/images/FindPolicyOff.png", GetLocalResourceObject("btn_ApplyToolTip"), , "InitialValues()"))
                    lclsTar_am_pol_def = New eBranches.Tar_am_pol
                    Call lclsTar_am_pol_def.AddDefaultValue(Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), Session("dEffecdate"), mintTariff, mintGroup, mintRole, Session("sDefaulti"), Session("nUsercode"), mintModulec, mintCover)
                    If lclsTar_am_pol.Load(Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("dEffecdate"), mintTariff, mintGroup, mintRole, mintModulec, mintCover, True) Then
                        lblnExist = True
                    End If
                End If
            End If
            lclsTar_am_detProd = Nothing
        End If
        lclsTar_am_pol_def = Nothing
	
        If lblnExist Then
            With lclsTar_am_pol
                For lintIndex = 0 To .CountItem
                    If .Item(CShort(lintIndex)) Then
                        mobjGrid.Columns("tcnAgeInit").DefValue = CStr(.nAge_init)
                        mobjGrid.Columns("tcnAgeEnd").DefValue = CStr(.nAge_End)
                        mobjGrid.Columns("cbeGroupComp").DefValue = CStr(.nGroup_comp)
                        mobjGrid.Columns("tcnPremium").DefValue = CStr(.nPremium)
                        mobjGrid.Columns("tcnGroupDed").DefValue = CStr(.nGroupDed)
                        mobjGrid.Columns("tcnCapital").DefValue = CStr(.nCapital)
                        
                        Response.Write(mobjGrid.DoRow)
                    End If
                Next
            End With
        End If
	
        lclsTar_am_pol = Nothing
        Response.Write(mobjGrid.closeTable())
    End Sub

'% insReaInitial: Se encarga de inicializar las variables de trabajo
'-----------------------------------------------------------------------------------------
Private Sub insReaInitial()
	'-----------------------------------------------------------------------------------------
	
	If Request.QueryString.Item("nTariff") = vbNullString Then
		mintTariffChange = 0
	Else
		mintTariffChange = Request.QueryString.Item("nTariff")
	End If
	
	If Request.QueryString.Item("nGroup") = vbNullString Then
		mintGroupChange = 0
	Else
		mintGroupChange = Request.QueryString.Item("nGroup")
	End If
	
	If Request.QueryString.Item("nRole") = vbNullString Then
		mintRoleChange = 0
	Else
		mintRoleChange = Request.QueryString.Item("nRole")
	End If
	
	If Request.QueryString.Item("nModulec") = vbNullString Then
		mintModulecChange = 0
	Else
		mintModulecChange = Request.QueryString.Item("nModulec")
	End If
	
	If Request.QueryString.Item("nCover") = vbNullString Then
		mintCoverChange = 0
	Else
		mintCoverChange = Request.QueryString.Item("nCover")
	End If
	
	With Response
		.Write("<SCRIPT>")
		.Write("var mintTariffChange = " & CStr(mintTariffChange) & ";")
		.Write("var mintGroupChange = " & CStr(mintGroupChange) & ";")
		.Write("var mintRoleChange = " & CStr(mintRoleChange) & ";")
		.Write("var mintModulecChange = " & CStr(mintModulecChange) & ";")
		.Write("var mintCoverChange = " & CStr(mintCoverChange) & ";")
		.Write("</" & "Script>")
	End With
End Sub

'% insDefaultValues: Se encarga de mostrar la tarifa por defecto seleccionada
'-----------------------------------------------------------------------------------------
Private Sub insDefaultValues()
	'-----------------------------------------------------------------------------------------
	Dim lclsTar_am_bas As eBranches.Tar_am_bas
	Dim lclsLife As ePolicy.Life
	
	lclsTar_am_bas = New eBranches.Tar_am_bas
	lclsLife = New ePolicy.Life
	
	'+ Si no hay una tarifa seleccionada se muestra la que se definió por defecto.
	If Request.QueryString.Item("nTariff") = vbNullString Then
		If Session("nCertif") <> 0 Then
			Call lclsLife.Find(Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), Session("dEffecdate"), True)
			mintGroup = lclsLife.nGroup
		End If
		
		'+ Obtiene la información por defecto a mostrar
		If lclsTar_am_bas.FindDeftValues(Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), Session("dEffecdate"), True) Then
			If Session("nCertif") = 0 Then
				mintTariff = lclsTar_am_bas.nTariff
				mintGroup = lclsTar_am_bas.nGroup
				mintRole = lclsTar_am_bas.nRole
				mintModulec = lclsTar_am_bas.nModulec
				mintCover = lclsTar_am_bas.nCover
			Else
				mintTariff = lclsTar_am_bas.nTariff
				mintRole = lclsTar_am_bas.nRole
				mintModulec = lclsTar_am_bas.nModulec
				mintCover = lclsTar_am_bas.nCover
			End If
		Else
			mintTariff = Request.QueryString.Item("nTariff")
			mintGroup = Request.QueryString.Item("nGroup")
			mintRole = Request.QueryString.Item("nRole")
			mintModulec = Request.QueryString.Item("nModulec")
			mintCover = Request.QueryString.Item("nCover")
		End If
	Else
		mintTariff = Request.QueryString.Item("nTariff")
		mintGroup = Request.QueryString.Item("nGroup")
		mintRole = Request.QueryString.Item("nRole")
		mintModulec = Request.QueryString.Item("nModulec")
		mintCover = Request.QueryString.Item("nCover")
	End If
	If mintRole = vbNullString Then
		mintRole = 0
	End If
	
	If mintGroup = vbNullString Then
		mintGroup = 0
	End If
	
	If mintModulec = vbNullString Then
		mintModulec = 0
	End If
	
	If mintCover = vbNullString Then
		mintCover = 0
	End If
	
	lclsTar_am_bas = Nothing
	lclsLife = Nothing
End Sub

</script>
<%Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage("AM002")
'~End Header Block VisualTimer Utility
Response.CacheControl = "private"

mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 31/3/03 19.42.02
mobjValues.sSessionID = Session.SessionID
mobjValues.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjValues.sCodisplPage = "AM002"
mobjMenu = New eFunctions.Menues
'^Begin Body Block VisualTimer Utility 1.1 31/3/03 19.42.02
mobjMenu.sSessionID = Session.SessionID
mobjMenu.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility
mobjGrid = New eFunctions.Grid
'^Begin Body Block VisualTimer Utility 1.1 31/3/03 19.42.02
mobjGrid.sSessionID = Session.SessionID
mobjGrid.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility
mclsTar_am_basprod = New eBranches.Tar_am_basprod
mclsTar_am_bas = New eBranches.Tar_am_bas
    mclsTar_am_pol = New eBranches.Tar_am_pol
    mobjValues.ActionQuery = Session("bQuery")

%>
<HTML>
<HEAD>
	<META NAME = "GENERATOR" CONTENT = "Microsoft Visual Studio 6.0">
<SCRIPT LANGUAGE="JavaScript" SRC="../../Scripts/GenFunctions.js"></SCRIPT>

<SCRIPT>
//- Variable para el control de versiones
	document.VssVersion="$$Revision: 5 $|$$Date: 13/01/04 16:45 $|$$Author: Nvaplat15 $"
	var nMainAction = <%=Request.QueryString.Item("nMainAction")%>;

//%insReload: Se encarga de recargar la página al cambiar algún valor de cualquier combo de la página.
//-------------------------------------------------------------------------------------------
function insReload(Field,ltarif,lGroup,lRole,lModulec,lCover){
//-------------------------------------------------------------------------------------------
	var lstr_docloc = "";
	var lblnOk = false;
	with(document.forms[0])	{
		lstr_docloc = document.location.href;
		
		if (nMainAction == 401)
			lstr_docloc = lstr_docloc.replace(/&nTariff=[0-9].*/,'') + "&nTariff=" + ltarif
		else
			lstr_docloc = lstr_docloc.replace(/&nTariff=[0-9].*/,'') + "&nTariff=" + cbeTariff.value
			
		
		if (nMainAction == 401){
			if(lGroup!='undefined')
				lstr_docloc = lstr_docloc.replace(/&nGroup=[0-9].*/,'') + "&nGroup=" + lGroup;
	  		else
				lstr_docloc = lstr_docloc.replace(/&nGroup=[0-9].*/,'') + "&nGroup=0";
		}
		else {
			if(typeof(cbeGroup)!='undefined')
				lstr_docloc = lstr_docloc.replace(/&nGroup=[0-9].*/,'') + "&nGroup=" + cbeGroup.value;
	  		else
				lstr_docloc = lstr_docloc.replace(/&nGroup=[0-9].*/,'') + "&nGroup=0";
		}

		
		if (nMainAction == 401){
			lstr_docloc = lstr_docloc.replace(/&nRole=[0-9].*/,'') + "&nRole=" + lRole
		if (Field.value!='undefined'){
			if (Field.value!=mintGroupChange)
				lblnOk = true;
		}
		}
		else {
			lstr_docloc = lstr_docloc.replace(/&nRole=[0-9].*/,'') + "&nRole=" + cbeRole.value
		if(typeof(cbeGroup)!='undefined'){
			if (cbeGroup.value!=mintGroupChange)
				lblnOk = true;
		}
	    }

		
		if (nMainAction == 401){
			lstr_docloc = lstr_docloc.replace(/&nModulec=[0-9].*/,'') + "&nModulec=" + lModulec;
		if (Field.value!='undefined'){
		    if (Field.value!=mintModulecChange)
			    lblnOk = true;
		}			
		}    
		else {
			lstr_docloc = lstr_docloc.replace(/&nModulec=[0-9].*/,'') + "&nModulec=" + cbeModulec.value;
		    if(typeof(cbeModulec)!='undefined'){
		        if (cbeModulec.value!=mintModulecChange)
			        lblnOk = true;
		    }
		}
			

		if (nMainAction == 401){
			lstr_docloc = lstr_docloc.replace(/&nCover=[0-9].*/,'') + "&nCover=" + lCover;
		if (Field.value!='undefined'){
		    if (Field.value!=mintCoverChange)
			    lblnOk = true;
		}			
		}	
		else {
			lstr_docloc = lstr_docloc.replace(/&nCover=[0-9].*/,'') + "&nCover=" + valCover.value;
		    if(typeof(valCover)!='undefined'){
		        if (valCover.value!=mintCoverChange)
			    lblnOk = true;			
			}    
		}	

	
	if (lstr_docloc.search(/&nMainAction=[0-9]{2,3}&/,lstr_docloc) == -1)
		lstr_docloc = lstr_docloc.replace(/&nMainAction=[0-9].*/,'') + "&nMainAction=" + nMainAction
	else
		lstr_docloc = lstr_docloc.replace(/&nMainAction=[0-9]{2,3}&/,"&nMainAction=" + nMainAction + "&") 

//+ Si el campo tarifa y el campo Role tienen valor
		if (nMainAction == 401){
		  if (ltarif!=mintTariffChange ||
			lblnOk==true ||
			lRole!=mintRoleChange) {
			mintTariffChange = ltarif
			if(typeof(cbeGroup)!='undefined')
				mintGroupChange = Field.value
			mintRoleChange = lRole
			mintModulecChange = lModulec
			mintCoverChange = lCover
			
			if (chkDefaulti.checked)
				insChangeDefaulti(Field,ltarif,lGroup,lRole,lModulec,lCover);

			document.location.href = lstr_docloc;
		  }
		}
		else {
		    if (cbeTariff.value!=mintTariffChange ||
			lblnOk==true ||
			cbeRole.value!=mintRoleChange) {
			mintTariffChange = cbeTariff.value
			if(typeof(cbeGroup)!='undefined')
				mintGroupChange = cbeGroup.value
			mintRoleChange = cbeRole.value
			mintModulecChange = cbeModulec.value
			mintCoverChange = valCover.value

			document.location.href = lstr_docloc;

			if(typeof(chkDefaulti)!='undefined')
				if (chkDefaulti.checked)
					insChangeDefaulti('1')
		    }
		}  
	}
	
	with (document.forms[0]) {
	    if(Field.name == 'cbeModulec'){
	        if(typeof(cbeModulec)!='undefined'){
	            valCover.Parameters.Param3.sValue=cbeModulec.value;
	        }
        }
    }
}

//% InitialValues: se inicializa el grid de la transacción, con los datos definidos en el diseñador
//--------------------------------------------------------------------------------------------
function InitialValues(Field){
//--------------------------------------------------------------------------------------------
	var lstrQuery
	
	with (document.forms[0]) {
		lstrQuery = "nTariff=" + cbeTariff.value + "&nRole=" + cbeRole.value + "&nModulec=" + cbeModulec.value + "&nCover=" + valCover.value
		if(typeof(cbeGroup)!='undefined')
		    lstrQuery = lstrQuery + "&nGroup=" + cbeGroup.value
		else
			lstrQuery = lstrQuery + "&nGroup=0"
		if(typeof(chkDefaulti)!='undefined'){
			if (chkDefaulti.checked) 
				lstrQuery = lstrQuery + "&sDefaulti=1"
			insDefValues("Tar_am_pol", lstrQuery)
		}
	}
}

//% insChangeWait_type: Manejo de los campos del período de espera
//--------------------------------------------------------------------------------------------
function insChangeWait_type(Field){
//--------------------------------------------------------------------------------------------
    if(Field.value==1)
        self.document.forms[0].tcnWait_quan.value='';
}

//% insChangeDefaulti: Al cambiar el valor del campo por defecto
//--------------------------------------------------------------------------------------------
function insChangeDefaulti(Field,ltarif2,lGroup2,lRole2,lModulec2,lCover2){
//--------------------------------------------------------------------------------------------
	var lstrQuery


	with (document.forms[0]) {		
	  if (nMainAction == 401){	
		if (ltarif2>0) {
			lstrQuery = "nTariff=" + ltarif2 + "&nRole=" + lRole2 + "&nModulec=" + lModulec2 + "&nCover=" + lCover2
		
			if (chkDefaulti.checked) 
				lstrQuery= lstrQuery + "&sDefaulti=1"
			else
				lstrQuery= lstrQuery + "&sDefaulti=2"
				
			if(typeof(cbeGroup)!='undefined')
			    lstrQuery = lstrQuery + "&nGroup=" + Field.value
			else
				lstrQuery = lstrQuery + "&nGroup=0"
			insDefValues("Defaulti", lstrQuery)
		}
	  }
	  else {
	  	if (cbeTariff.value>0) {
			lstrQuery = "nTariff=" + cbeTariff.value + "&nRole=" + cbeRole.value + "&nModulec=" + cbeModulec.value + "&nCover=" + valCover.value
			if (chkDefaulti.checked) 
				lstrQuery= lstrQuery + "&sDefaulti=1"
			else
				lstrQuery= lstrQuery + "&sDefaulti=2"
				
			if(typeof(cbeGroup)!='undefined')
			    lstrQuery = lstrQuery + "&nGroup=" + cbeGroup.value
			else
				lstrQuery = lstrQuery + "&nGroup=0"

			insDefValues("Defaulti", lstrQuery)
		}
	  }
		chkDefaulti.value = (Field=='1'?'2':'1');
	}
}
</SCRIPT>	
	<%
With Response
	.Write(mobjValues.ShowWindowsName(Request.QueryString.Item("sCodispl"), Request.QueryString.Item("sWindowDescript")))
	.Write(mobjValues.StyleSheet())
	If Request.QueryString.Item("Type") <> "PopUp" Then
		.Write(mobjMenu.setZone(2, "AM002", Request.QueryString.Item("sWindowDescript"), CShort(Request.QueryString.Item("nWindowTy"))))
		'.Write "<NOTSCRIPT>var nMainAction=top.frames['fraSequence'].plngMainAction;</SCRIPT>"
	End If
End With
mobjMenu = Nothing
Call insReaInitial()
Call insDefaultValues()
%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" ACTION="valpolicyseq.aspx?nMainAction=<%=Request.QueryString.Item("nMainAction")%>" ID=AM002 NAME=AM002>
<%mclsTar_am_basprod.Load(Session("nBranch"), Session("nProduct"), Session("dEffecdate"))
mclsTar_am_basprod.FindItem(mintTariff, True)
mclsTar_am_bas.Load(Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("dEffecdate"), True)

mclsCover = New ePolicy.Cover
lclsGroups = New ePolicy.Groups
If lclsGroups.valGroupExist_a(Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate)) Then
	'Response.Write "<NOTSCRIPT>alert('entra en el valGroupExist_a');</script>"
	mblnGroup = True
Else
	mblnGroup = False
End If
lclsGroups = Nothing

'+ Si la póliza es matriz.		 	    
    If CStr(Session("sPolitype")) <> "1" And CStr(Session("nCertif")) = "0" Then
        'If CStr(Session("nCertif")) = "0" Then
        mblnChkDefaulti = True
    End If
    insDefineHeader()
    If Request.QueryString.Item("Type") <> "PopUp" Then
        insPreAM002()
    Else
        '	if Session("nMainAction") = "401" then
        '	insPreAM002
        '   End If
        insPreAM002Upd()
	
    End If
    mobjValues = Nothing
    mobjGrid = Nothing
    mclsTar_am_pol = Nothing
%>
</FORM>
</BODY>
</HTML>
<%'^Begin Footer Block VisualTimer Utility 1.1 31/3/03 19.42.02
Call mobjNetFrameWork.FinishPage("AM002")
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>




