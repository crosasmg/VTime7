<%@ Page Language="VB" Explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon"
    EnableViewState="false" %>

<%@ Import Namespace="eNetFrameWork" %>
<%@ Import Namespace="eFunctions" %>
<%@ Import Namespace="eClaim" %>
<%@ Import Namespace="ePolicy" %>
<%@ Import Namespace="eGeneral" %>
<script language="VB" runat="Server">
    '^Begin Header Block VisualTimer Utility 1.1 7/4/03 11.35.15	
    Dim mobjNetFrameWork As eNetFrameWork.Layout

    Dim mobjValues As eFunctions.Values
    Dim mobjMenu As eFunctions.Menues
    Dim mobjGrid As eFunctions.Grid
    Dim mobjProf_ord As eClaim.Prof_ord
    Dim mobjProf_ords As eClaim.Prof_ords
    Dim mobjClaim_his As eClaim.Claim_his
    Dim blnbutom As Boolean
    Dim mstrFirstCase As String
    Dim lclsClaimCases As eClaim.Claim_cases
     Dim mstrCase() As String
    '- Contador    
    Dim lintCount As Integer


    '+ insDefineHeader: Define las columnas de la Grid
    '---------------------------------------------------------------------------------------------
    Private Sub insDefineHeader()
        Dim clngProviderGarage As Object
        '---------------------------------------------------------------------------------------------
        Dim lclsPolicy As ePolicy.Policy
        mobjGrid.sSessionID = Session.SessionID
        mobjGrid.nUsercode = Session("nUsercode")
        '~End Body Block VisualTimer Utility
        '----------------------------------------------------------------------------------------------
        Dim lstrCase() As Object
        Dim lintCase_num As Object
        Dim lintDeman_type As Object
        Dim lclsClaimCases As eClaim.Claim_cases
        Dim lblnFind As Object
	
        lclsClaimCases = New eClaim.Claim_cases
        If mstrFirstCase <> vbNullString Then
            lstrCase = mstrFirstCase.Split("/")
            lintCase_num = lstrCase(0)
            lintDeman_type = lstrCase(1)
            Session("nCase_num_629") = lintCase_num
            Session("nDeman_type_629") = lintDeman_type
        End If
	
        mobjGrid.sCodisplPage = "si011"
        Call mobjGrid.SetWindowParameters(Request.QueryString("sCodispl"), Request.QueryString("sWindowDescript"), Request.QueryString("nWindowTy"))
	
        '+ Se definen las columnas del grid    
        With mobjGrid.Columns
		
            Call .AddPossiblesColumn(0, "Caso", "cbeCase", "tabClaim_cases", eFunctions.Values.eValuesType.clngComboType, , True, , , , "ChangeCase(this)", True, , "Caso al que se asocia la orden de servicio profesional")
            mobjGrid.Columns("cbeCase").Parameters.Add("nClaim", Session("nClaim"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		
            If Request.QueryString("Type") <> "PopUp" Then
                Call .AddNumericColumn(0, "Caso", "tcnCase", 4, CStr(0), , "Caso al que se asocia la orden de servicio profesional", False, 0)
            End If
            
            .AddPossiblesColumn(40603, "Cobertura", "valCover", "Tabcl_cover", eFunctions.Values.eValuesType.clngWindowType, CStr(eRemoteDB.Constants.intNull), True, , , , "ChangeValues_cover('Cover');", False, 5, "Cobertura a la cual se encuentra asociado el cliente como beneficiario.")
		
            Call .AddNumericColumn(0, "Orden", "tcnOrder", 10, "", , "Número que identifica la orden de servicio profesional", , , , , , Request.QueryString("Action") = "Update")
		
            Call .AddPossiblesColumn(0, "Tipo", "cbeType", "Table7100", eFunctions.Values.eValuesType.clngComboType, , , , , , , , , "Indica el tipo de orden de servicio")
            lclsPolicy = New ePolicy.Policy
            If lclsPolicy.Find_TabNameB(CInt(Session("nBranch"))) Then
                If lclsPolicy.sTabname = "FIRE" Then
                    mobjGrid.Columns("cbeType").TypeList = Values.ecbeTypeList.Inclution
                    mobjGrid.Columns("cbeType").List = "5,9,10,11,12,13"
                End If
            End If
            lclsPolicy = Nothing

            If Request.QueryString("Type") <> "PopUp" And CDbl(Session("sBrancht")) = 3 Then
                Call .AddCheckColumn(0, "Detalle", "chkDetail", "")
            End If
		
            Call .AddPossiblesColumn(0, "Titular orden de servicio", "valProvider", "tabclaimbenef", eFunctions.Values.eValuesType.clngWindowType, CStr(0), True, , , , "ChangeStatus(this);", True, , "Proveedor titular de la orden de servicio profesional")
		
            If Request.QueryString("Type") = "PopUp" Then
                With mobjGrid.Columns("valProvider")
                    .Parameters.Add("nClaim", Session("nClaim"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Parameters.Add("nCase_num", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Parameters.Add("nDeman_type", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Parameters.Add("nBene_type", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Parameters.Add("nTypeProv", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Parameters.Add("sBene_type", "9,10,12", eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 20, 0, , eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                End With
            End If
            Call .AddDateColumn(0, "Fecha de asignación", "tcdAssignDate", CStr(Today), , "Fecha en que se asigna la orden de servicio")
            Call .AddDateColumn(40324, "Fecha planificación", "dDateDesing", "", , "Fecha en la que se planifica o espera realizar el servicio")
            Call .AddTextColumn(40322, "Hora planificación", "dHourDesing", 5, "00:00", , "Hora en la que se planifica o espera realizar el servicio", , , "insFormatTime(this.value);")
		
            If CDbl(Session("sBrancht")) = 3 Then
                Call mobjGrid.Columns.AddPossiblesColumn(0, "Taller", "valWorksh", "TabClaimbenef", eFunctions.Values.eValuesType.clngWindowType, "", True, , , , "ChangeStatus(this);", True, , "Taller donde se encuentra en reparación el vehículo")
                If Request.QueryString("Type") = "PopUp" Then
                    With mobjGrid.Columns("valWorksh")
                        .Parameters.Add("nClaim", Session("nClaim"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                        .Parameters.Add("nCase_num", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                        .Parameters.Add("nDeman_type", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                        .Parameters.Add("nBene_type","0", eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                        .Parameters.Add("nTypeProv", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                        .Parameters.Add("sBene_type", "10,66", eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 20, 0, , eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    End With
                End If
                '+ Lee cl_cover para saber si existe deducible
                Call mobjGrid.Columns.AddCheckColumn(0, "Pago deducible en taller", "chkWsDeduc", vbNullString, , "1", , True, GetLocalResourceObject("valWorkshColumnToolTip"))
            End If
            Call .AddPossiblesColumn(0, "Estado", "cbeState", "Table215", eFunctions.Values.eValuesType.clngComboType, , , , , , , True, , "Estado en el que se encuentra la orden de servicio")
            Call mobjGrid.Columns.AddButtonColumn(0, "Notas", "SCA2-J", eRemoteDB.Constants.intNull, , True, , , , , "btnNotenum")
            
            Call .AddPossiblesColumn(0, "Inspector asociado", "valInspector", "TabTab_provider", eFunctions.Values.eValuesType.clngWindowType, CStr(0), True, , , , "ChangeStatus(this);", , , "Corresponde al inspector asociado a la orden de servicio.")
		
            If Request.QueryString("Type") = "PopUp" Then
                With mobjGrid.Columns("valInspector")
                    .Parameters.Add("nBranch", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Parameters.Add("nTypeProv", "8", eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                End With
            End If
            
            Call .AddHiddenColumn("dOldDateDesing", "")
            Call .AddHiddenColumn("dOldHourDesing", "")
            Call .AddHiddenColumn("nTransac", "")
		
            Call .AddHiddenColumn("nCaseNumber", CStr(0))
            Call .AddHiddenColumn("nDemandantType", CStr(0))
            Call .AddHiddenColumn("sCase", CStr(0))
            Call .AddHiddenColumn("tcnOrder_Aux", CStr(0))
            Call .AddHiddenColumn("tcnIndex", CStr(0))
            Call .AddHiddenColumn("tcnType_Aux", CStr(0))
            Call .AddHiddenColumn("tctStatus_Aux", vbNullString)
            Call .AddHiddenColumn("sParam", "")
            
            Call .AddHiddenColumn("hddCover", "")
            Call .AddHiddenColumn("hddModulec", "")
        End With
        
        With mobjGrid.Columns("valCover").Parameters
            .Add("nClaim", Session("nClaim"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Add("nCase_num",  eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Add("nDeman_type",  eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .ReturnValue("nModulec", , , True)
            .ReturnValue("nCover", , , True)
        End With
	
        '+ Se definen las propiedades generales del grid
        With mobjGrid
            .Codispl = "SI011"
            .DeleteButton = blnbutom
            .AddButton = blnbutom
            .MoveRecordScript = "ChangeCase(document.forms[0].cbeCase);"
            .Columns("cbeCase").GridVisible = False
            .Width = 450
            .Height = 460
            .WidthDelete = 460
            .Top = 200
            .Columns("Sel").OnClick = "ChangeValues(""Find_ProfSoon"",this)"
		
            .sDelRecordParam = "' + marrArray[lintIndex].sParam + '"
		
            If CStr(Session("sBrancht")) = "3" Then
                .MoveRecordScript = "DisabledCombo(" & Session("nTransaction") & ", self.document.forms[0].elements['tcnOrder'].value, self.document.forms[0].elements['cbeType'].value);"
            Else
                .MoveRecordScript = "DisabledCombo(" & Session("nTransaction") & ", self.document.forms[0].elements['tcnOrder'].value);"
            End If
		
            If Request.QueryString("Type") = "PopUp" Then
                .Columns("btnNotenum").bQuery = False
            Else
                .Columns("btnNotenum").bQuery = True
            End If
		
            If Request.QueryString("Reload") = "1" Then
                .sReloadIndex = Request.QueryString("ReloadIndex")
            End If
        End With
    End Sub

    '-------------------------------------------------------------------------------------------
    Private Sub insPreSI011()
    '-------------------------------------------------------------------------------------------
        Dim clngProviderProfessional As Object
        Dim clngProviderGarage As Object
    
        Dim lintIndex As Integer
        Dim sKey As Object
        Dim lclsClaim As eClaim.ClaimBenef
        Dim TypeProvider As Object
	
        lclsClaim = New eClaim.ClaimBenef
	
        lintIndex = 0
	
        If mobjProf_ords.Find(mobjValues.StringToType(CStr(Session("nClaim")), eFunctions.Values.eTypeData.etdDouble)) Then
            For lintCount = 1 To mobjProf_ords.Count
                mobjProf_ord = mobjProf_ords.Item(lintCount)
			
                With mobjGrid
                    .Columns("cbeCase").DefValue = mobjProf_ord.sCase
                    .Columns("tcnCase").DefValue = CStr(mobjProf_ord.nCase_Num)
                    .Columns("tcnCase").EditRecord = True
                    .Columns("tcnOrder").DefValue = CStr(mobjProf_ord.nServ_Order)
                    
                    
                    With mobjGrid.Columns("valCover")
                        .Parameters.Add("nClaim", Session("nClaim"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                        .Parameters.Add("nCase_num", mobjProf_ord.nCase_Num, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                        .Parameters.Add("nDeman_type", mobjProf_ord.nDeman_Type, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                   End With
                    
                    .Columns("valCover").DefValue = CStr(mobjProf_ord.nCover)
                    .Columns("hddCover").DefValue = CStr(mobjProf_ord.nCover)
                    .Columns("hddModulec").DefValue = CStr(mobjProf_ord.nModulec)
				
                    If CStr(Session("sBrancht")) = "3" Then
                        .Columns("cbeType").DefValue = CStr(mobjProf_ord.nOrdertype)
                    End If
				
                    If CDbl(Session("sBrancht")) = 3 Then
                        If mobjProf_ord.nOrdertype <> 4 Then
                            .Columns("chkDetail").Disabled = True
                        Else
                            .Columns("chkDetail").Disabled = False
                        End If
                        .Columns("chkWsDeduc").DefValue = mobjProf_ord.sWsdeduc
                        If mobjProf_ord.sWsdeduc = "1" Then
                            .Columns("chkWsDeduc").Checked = 1
                            .Columns("chkWsDeduc").DefValue = CStr(1)
                        Else
                            .Columns("chkWsDeduc").Checked = 2
                            .Columns("chkWsDeduc").DefValue = CStr(2)
                        End If
                    End If
				
                    With mobjGrid.Columns("valProvider")
                        .Parameters.Add("nClaim", Session("nClaim"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                        .Parameters.Add("nCase_num", mobjProf_ord.nCase_Num, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                        .Parameters.Add("nDeman_type", mobjProf_ord.nDeman_Type, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                        .Parameters.Add("nBene_type", eClaim.Claim_case.eClaimRole.clngClaimRProfessional, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                        .Parameters.Add("nTypeProv", clngProviderProfessional, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                        .Parameters.Add("sBene_type", "9,10,12", eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 20, 0, , eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    End With
                    .Columns("valProvider").DefValue = CStr(mobjProf_ord.nProvider)
                    .Columns("valProvider").Descript = mobjProf_ord.sProviderName
                    .Columns("valProvider").EditRecord = True
				
                    
                    With mobjGrid.Columns("valInspector")
                        .Parameters.Add("nBranch", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                        .Parameters.Add("nTypeProv", "8", eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    End With
                    .Columns("valInspector").DefValue = CStr(mobjProf_ord.nInspector)
                    '.Columns("valProvider").Descript = mobjProf_ord.sProviderName
                    '.Columns("valInspector").EditRecord = True
                    
                    
                    '+ Se obtiene el número de transacción correspondiente                
                    .Columns("nTransac").DefValue = CStr(mobjProf_ord.nTransac)
				    '+ Se obtiene el número del taller asociado.     
                    If CDbl(Session("sBrancht")) = 3 Then
                        With mobjGrid.Columns("valWorksh")
	                        .Parameters.Add("nClaim", Session("nClaim"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	                        .Parameters.Add("nCase_num", mobjProf_ord.nCase_Num, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	                        .Parameters.Add("nDeman_type", mobjProf_ord.nDeman_type, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                            .Parameters.Add("nBene_type", "0", eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	                        .Parameters.Add("nTypeProv", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	                        .Parameters.Add("sBene_type", "10,66", eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0,  , eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                        End With
                        .Columns("valWorksh").DefValue = CStr(mobjProf_ord.nWorksh)
                        .Columns("valWorksh").Descript = mobjProf_ord.sWorksh
                        TypeProvider = clngProviderGarage
                    Else
                        TypeProvider = clngProviderProfessional
                    End If
				
                    '+ Se obtiene la fecha de la orden y se hace una copia sobre un control hidden para
                    '+ efectos de validación.                
                    .Columns("dDateDesing").DefValue = CStr(mobjProf_ord.dFec_prog)
                    .Columns("dOldDateDesing").DefValue = CStr(mobjProf_ord.dFec_prog)
                    .Columns("tcdAssignDate").DefValue = CStr(mobjProf_ord.dAssigndate)
				
                    '+ Se obtiene la hora de la orden y se hace una copia sobre un control hidden para
                    '+ efectos de validación.                
                    .Columns("dHourDesing").DefValue = mobjProf_ord.sTime_prog
                    .Columns("dOldHourDesing").DefValue = mobjProf_ord.sTime_prog
                    .Columns("btnNoteNum").nNotenum = mobjValues.StringToType(CStr(mobjProf_ord.nNoteorder), eFunctions.Values.eTypeData.etdDouble)
                    .Columns("cbeState").DefValue = CStr(mobjProf_ord.nStatus_ord)
                    .Columns("tcnOrder_Aux").DefValue = CStr(mobjProf_ord.nServ_Order)
                    .Columns("nCaseNumber").DefValue = CStr(mobjProf_ord.nCase_Num)
                    .Columns("nDemandantType").DefValue = CStr(mobjProf_ord.nDeman_Type)
                    .Columns("sCase").DefValue = mobjProf_ord.nCase_Num & "/" & mobjProf_ord.nDeman_Type & "/" & mobjProf_ord.sClient
                    .Columns("tcnType_Aux").DefValue = CStr(mobjProf_ord.nOrdertype)
                    .Columns("tctStatus_Aux").DefValue = CStr(mobjProf_ord.nStatus_ord)
                    .Columns("btnNotenum").nNotenum = mobjProf_ord.nNoteorder
				
                    .Columns("sParam").DefValue = "nMovement=" & mobjProf_ord.nCase_Num & "&sAction=Delete" & "&nClaim=" & Session("nClaim") & "&nCase_num=" & mobjProf_ord.nCase_Num & "&nDeman_type=" & mobjProf_ord.nDeman_Type & "&nServ_Order=" & mobjProf_ord.nServ_Order & "&dFec_prog=" & mobjProf_ord.dFec_prog & "&nProvider=" & mobjProf_ord.nProvider & "&nStatus_ord=" & mobjProf_ord.nStatus_ord & "&sTime_prog=" & mobjProf_ord.sTime_prog & "&nWorksh=" & mobjProf_ord.nWorksh & "&nOrderType=" & mobjProf_ord.nOrdertype & "&nNoteorder=" & mobjProf_ord.nNoteorder & "&nUsercode=" & Session("nUsercode") & "&sWsDeduc=" & mobjProf_ord.sWsdeduc & "&dEffecdate=" & CStr(Today) & "&nBranch=" & Session("nBranch") & "&nProduct=" & Session("nProduct") & "&nPolicy=" & Session("nPolicy") & "&nCertif=" & Session("nCertif")
				
                    lintIndex = lintCount - 1
				
                    If CDbl(Session("sBrancht")) = 3 Then
                        .Columns("chkDetail").OnClick = "OpenPageSI774(this.checked," & Session("nClaim") & ", " & lintIndex & ")"
                    End If
				
                    .sEditRecordParam = "&nTransac=" & CStr(mobjProf_ord.nTransac) & "&nCaseNum=" & CStr(mobjProf_ord.nCase_Num) & "&nDemanType=" & CStr(mobjProf_ord.nDeman_Type) & "&nStatus_ord=" & mobjProf_ord.nStatus_ord & "&nServ_Order=" & mobjProf_ord.nServ_Order 
                    Response.Write(.DoRow)
                End With
            Next
        End If
        Response.Write(mobjGrid.closeTable)
        lintIndex = Nothing
        lclsClaim = Nothing
    End Sub

    '-------------------------------------------------------------------------------------------
    Private Sub insPreSI011Upd()
    '-------------------------------------------------------------------------------------------
        Dim lclsClaim As Object
        Dim lclsErrors As eGeneral.GeneralFunction
        Dim lstrMessage As String
        Dim lclsQuot_parts As eClaim.Quot_parts
        Dim lclsProf_ord As eClaim.Prof_ord
	
        mobjGrid.Columns("cbeCase").Disabled = False
	
        With Response
            .Write(mobjGrid.DoFormUpd(Request.QueryString("Action"), "valClaimSeq.aspx", Request.QueryString("sCodispl"), Request.QueryString("nMainAction"), CBool(Session("bQuery")), Request.QueryString("Index")))
            .Write(mobjValues.HiddenControl("tcnCaseNum", Request.QueryString("nCasenum")))
            .Write(mobjValues.HiddenControl("tcnDemanType", Request.QueryString("nDemanType")))
            .Write(mobjValues.HiddenControl("tctsClient", CStr(eRemoteDB.Constants.strNull)))
            If Request.QueryString("Action") <> "Del" And Request.QueryString("Action") <> "Delete" Then
                .Write("<script>ChangeCase(self.document.forms[0].cbeCase);</" & "Script>")
            End If
        End With
        If Request.QueryString("Action") = "Add" Then
            '+ Por defecto se coloca el estado como pendiente por asignar 
            Response.Write("<script>document.forms[0].cbeState.value=1;</" & "Script>")
        End If
	
        If Request.QueryString("Action") = "Update" Then
            Response.Write("<script>DisableAll();</" & "Script>")
            Response.Write("<script>self.document.forms[0].tcnNotenum.value = top.opener.marrArray[CurrentIndex].btnNotenum</" & "Script>")
            '+ Si la transacción se trata de MODIFICACIÓN DE UN SINIESTRO (Session("nTransaction") = 4) y 
            '+ la acción de la ventana POPUP es actualizar (UPDATE) y el tipo de orden de servicio es distinto de COTIZACIÓN DE REPUESTOS,
            '+ se habilita el campo ESTADO (cbeState) de lo contrario se inhabilita - ACM - 11/03/2003
            If CDbl(Session("nTransaction")) = 4 Then
                If CStr(Session("sBrancht")) = "3" Then
                    Response.Write("<script>DisabledCombo(" & Session("nTransaction") & ", self.document.forms[0].elements['tcnOrder'].value, self.document.forms[0].elements['cbeType'].value);</" & "Script>")
                Else
                    Response.Write("<script>DisabledCombo(" & Session("nTransaction") & ", self.document.forms[0].elements['tcnOrder'].value);</" & "Script>")
                End If
            End If
        End If
	
	
        If Request.QueryString("Action") = "Del" Or Request.QueryString("Action") = "Delete" Then
            lclsQuot_parts = New eClaim.Quot_parts
            If Not lclsQuot_parts.Find_exists(mobjValues.StringToType(Request.QueryString("nServiceOrder"), eFunctions.Values.eTypeData.etdDouble)) Then
                lclsProf_ord = New eClaim.Prof_ord
                Call lclsProf_ord.insPostSI011(mobjValues.StringToType(Request.QueryString("nServ_order"), eFunctions.Values.eTypeData.etdDouble), Request.QueryString("Action"), mobjValues.StringToType(CStr(Session("nClaim")), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString("nCase_Num"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString("nDeman_Type"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString("nServ_order"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString("dFec_prog"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Request.QueryString("nProvider"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString("nStatus_ord"), eFunctions.Values.eTypeData.etdDouble, True), Request.QueryString("sTime_prog"), mobjValues.StringToType(Request.QueryString("nWorksh"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Request.QueryString("nOrderType"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Request.QueryString("nNoteOrder"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(CStr(Session("nUsercode")), eFunctions.Values.eTypeData.etdDouble), Request.QueryString("sWsDeduc"), mobjValues.StringToType(CStr(Session("dEffecdate")), eFunctions.Values.eTypeData.etdDate), CInt(Session("nBranch")), CInt(Session("nProduct")), CDbl(Session("nPolicy")), CInt(Session("nCertif")), eRemoteDB.Constants.dtmNull , 0 , 0 , 0)
                lclsQuot_parts = Nothing
            Else
                lclsErrors = New eGeneral.GeneralFunction
                lstrMessage = lclsErrors.insLoadMessage(100011)
                Response.Write("<script>alert('" & lstrMessage & "')</" & "Script>")
                lclsErrors = Nothing
            End If
        End If
        lclsProf_ord = Nothing
    End Sub

</script>
<%Response.Expires = -1441
    mobjNetFrameWork = New eNetFrameWork.Layout
    mobjNetFrameWork.sSessionID = Session.SessionID
    mobjNetFrameWork.nUsercode = Session("nUsercode")
    Call mobjNetFrameWork.BeginPage("si011")
    '- Existe deducible (1: existe, 2:no existe)

    mobjValues = New eFunctions.Values
    mobjMenu = New eFunctions.Menues
    mobjGrid = New eFunctions.Grid
    mobjProf_ord = New eClaim.Prof_ord
    mobjProf_ords = New eClaim.Prof_ords
    mobjClaim_his = New eClaim.Claim_his


    '- Se establece el estado del tipo de acción.
    mobjValues.ActionQuery = Session("bQuery")
    mobjGrid.ActionQuery = Session("bQuery")

    mobjValues.sCodisplPage = "si011"
%>
<script type="text/javascript" src="/VTimeNet/Scripts/Constantes.js"></script>
<script type="text/javascript" src="/VTimeNet/Scripts/GenFunctions.js"></script>
<script>   
//ChangeValues: Cambia y asigna los valores según la opción seleccionada.
//-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------
function ChangeValues(Option, Field){
//-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    switch(Option){
		case "Find_ProfSoon":
			insDefValues('Find_ProfSoon', 'nLength=' + marrArray.length + '&nIndex=' + Field.value + '&nProf_ord=' + marrArray[Field.value].tcnOrder );
			break;
        
    }
}

    
    
//% insFormatTime: Da formato a la hora introducida por el usuario    
//--------------------------------------------------------------------------------------------
function insFormatTime(Field){
//--------------------------------------------------------------------------------------------
	var lstrTime="";
	var lstrTimeAUX="";
	var lstrString="";
    var lstrHour="";
    var lstrMin="";

	lstrTime = Field;
    lstrHour = lstrTime.substr(0,2);
    lstrMin = lstrTime.substr(3,2);
    lstrTime = lstrHour + lstrMin;
//+ Se pregunta por el valor mayor a 2400 que equivale a que el usuario halla introducido
//+ 24:00, si es mayor a este valor se blanquea el campo y se sale de la función - ACM - 21/05/2001
	if(lstrTime>2400){
		self.document.forms[0].elements["dHourDesing"].value = "00:00";
		return(0);
	}

	lstrTime = Field;
//+ Si la longitud de la hora es menor a 4 dígitos, se toma el primer dígito y se le suman 12
//+ para obtener la hora en formato militar, luego se le añaden los 2 puntos (:) entre las
//+ horas y los minutos - ACM - 21/05/2001
	if(lstrTime.length<4){
//+ Si la longitud del valor introducido es 1, 2 ó 3 se llena con ceros a la derecha para
//+ obtener la longitud ideal y luego formatear la hora en formato militar (24 horas) - ACM - 21/05/2001
		if(lstrTime.length<4 && lstrTime.length==1)
			lstrTime = "0" + lstrTime + "00";

		if(lstrTime.length<4 && lstrTime.length==2)
			lstrTime = lstrTime + "00";
			
		if(lstrTime.length<4 && lstrTime.length==3)
			lstrTime = lstrTime + "0";

//+ Se extrae el valor del primer dígito del valor del campo y se verifica que éste no sea
//+ ni 1 ni cero para concatenarle un cero a la izquierda - ACM - 21/05/2001
		lstrTimeAUX = lstrTime.substr(0, 1);
		lstrTimeAUX = lstrTime.substr(0, 2);
		lstrString = lstrTimeAUX + ":" + lstrTime.substr(2, lstrTime.length);
	}
//+ Si la longitud del campo es igual a 4, se procede a tomar las 2 primera posiciones, concatenar
//+ los 2 puntos (:) y luego se concatenan los valores restantes - ACM - 21/05/2001
	else
	{
		if(lstrTime.length==4)
		{
			lstrTimeAUX = lstrTime.substr(0, 2);
			lstrString = lstrTimeAUX + ":" + lstrTime.substr(2, lstrTime.length);
		}
	}
	if(lstrString!="")
		self.document.forms[0].elements["dHourDesing"].value = lstrString;
}

//% OpenPageSI774: Hace el llamado a la SI774 una vez que se presiona el checkbox denominado "DETALLE" - ACM - 19/06/2002
//------------------------------------------------------------------------------------------------------------------------------
function OpenPageSI774(blnChecked, nClaimNumber, nIndex){
//------------------------------------------------------------------------------------------------------------------------------
	if(blnChecked && nClaimNumber>0)	
		ShowPopUp('/VTimeNet/Common/secWHeader.aspx?sModule=Claim&sProject=Claim&sCodispl=SI774_K&sOriginalForm=SI011&nClaim='+nClaimNumber+
			                                        '&nCaseNumber='+marrArray[nIndex].nCaseNumber+
			                                        '&nDemandantType='+marrArray[nIndex].nDemandantType+
			                                        '&sCase='+marrArray[nIndex].sCase+
			                                        '&nService_Order='+marrArray[nIndex].tcnOrder_Aux+ 
			                                        '&nTypeOrder='+marrArray[nIndex].tcnType_Aux+ 
			                                        '&sStateOrder='+marrArray[nIndex].tctStatus_Aux +
			                                        '&nHeight=210', 'Claim', 900, 600, 'yes', 'yes', 0, 0);
}

//% DisableAll: Desactiva los campos de la PopUp en caso de que el estado de la orden de servicio sea distinto de 
//              "Pendiente por asignar"
//------------------------------------------------------------------------------------------------------------------------------
function DisableAll(){
//------------------------------------------------------------------------------------------------------------------------------
	with (self.document.forms[0])
	{
	 if (cbeState.value == "2")
		{
			cbeCase.disabled = true;
			dDateDesing.disabled = false;
			dHourDesing.disabled = false;
			valProvider.disabled = false;
			btnvalProvider.disabled = false;
             
			<%If CStr(Session("sBrancht")) = "3" Then%>
				valWorksh.disabled = false;
				btnvalWorksh.disabled = false;
				chkWsDeduc.disabled = false;
			<%End If%>			
			<%If CStr(Session("sBrancht")) = "3" Then%>
				cbeType.disabled = false;
			<%End If%>	
			cbeState.disabled = true;	
			btnNotenum.disabled = false;
		}
		else
		{
			cbeCase.disabled = true;
			valProvider.disabled = true;
			btnvalProvider.disabled = true;
			dDateDesing.disabled = true;
			dHourDesing.disabled = true;
            tcdAssignDate.disabled = true ;
			<%If CDbl(Session("sBrancht")) = 3 Then%>
				valWorksh.disabled = true;
				btnvalWorksh.disabled = true;
				chkWsDeduc.disabled = true;
			<%End If%>
			<%If CStr(Session("sBrancht")) = "3" Then%>
				cbeType.disabled = true;
			<%End If%>	
			cbeState.disabled = true;	
			btnNotenum.disabled = true;
			chkContinue.disabled = true;
			chkContinue.checked = false;
		}
	}
}
//%	ChangeCase: Ejecuta la busqueda con un nuevo caso
//-------------------------------------------------------------------------------------------
function ChangeCase(objCase){
//-------------------------------------------------------------------------------------------
	var strParams; 
    var sCasenum;
    var sDeman_type;
    var sCase;
            
    sCasenum = new String;
    sDeman_type = new String;
    sCase = new String(objCase.value);        

    if (sCase != 0) 
    {         
// Se obtiene el número de caso nCase_num 
        sCasenum = sCase.substr(0, sCase.length - (sCase.length - sCase.indexOf('/')));
// Se obtiene el número de nDeman_type    
        sDeman_type = sCase.substr(sCase.indexOf('/') + 1, (sCase.indexOf('/', sCase.indexOf('/') + 1) - sCase.indexOf('/')) - 1);
// Se obtiene el número de sCliente 
        sClient = sCase.substr(sCase.lastIndexOf('/')+1)  
 
		with (self.document.forms[0]) 
		{ 
			valProvider.Parameters.Param2.sValue = sCasenum; 
			valProvider.Parameters.Param3.sValue = sDeman_type; 
           // valInspector.Parameters.Param2.sValue = sCasenum; 
		//	valInspector.Parameters.Param3.sValue = sDeman_type; 
            valCover.Parameters.Param2.sValue = sCasenum; 
          	valCover.Parameters.Param3.sValue = sDeman_type; 
            tcnCaseNum.value = sCasenum ; 
			tcnDemanType.value = sDeman_type; 
			tctsClient.value = sClient; 
			btnvalProvider.disabled=false; 
			valProvider.disabled=false; 
			<%If CDbl(Session("sBrancht")) = 3 Then%> 
				valWorksh.Parameters.Param2.sValue = sCasenum; 
				valWorksh.Parameters.Param3.sValue = sDeman_type; 

     			valWorksh.disabled = false; 
     			btnvalWorksh.disabled = false; 

                <%	If Request.QueryString("Action") = "Add" Then%>
				        strParams = "nClaim=" + <%=Session("nClaim")%> +  
					                "&nCase_num=" + sCasenum +  
						            "&nDeman_type=" + sDeman_type +
                                    "&sFromSI011=1";

				        insDefValues('WsDeduc',strParams,'/VTimeNet/claim/claimseq'); 
  			    <%	End If%> 
     		<%End If%> 
		} 
   } 
   else
   {
		with (self.document.forms[0])
		{
			valProvider.Parameters.Param2.sValue = 0;
			valProvider.Parameters.Param3.sValue = 0;
            // valInspector.Parameters.Param2.sValue = 0;
			// valInspector.Parameters.Param3.sValue = 0;	
            valCover.Parameters.Param2.sValue = 0;
            valCover.Parameters.Param3.sValue = 0;			
			tcnCaseNum.value = 0;
			tcnDemanType.value = 0;	
			tctsClient.value = 0;
			valProvider.disabled = true;
			<%If CDbl(Session("sBrancht")) = 3 Then%>
				valWorksh.Parameters.Param2.sValue = 0;
				valWorksh.Parameters.Param3.sValue = 0;		
				btnvalProvider.disabled = true;						
     			valWorksh.disabled = true;
     			btnvalWorksh.disabled = true;
                valWorksh.value='';
                UpdateDiv('valWorkshDesc','');
     		<%End If%>
		}
   }
}

//%	ChangeType: Activa o desactiva el campo pago deducible en taller dependiendo del Tipo
//-------------------------------------------------------------------------------------------
function ChangeType(Field){
//-------------------------------------------------------------------------------------------
//		with (self.document.forms[0])
//		{
//			if (Field.value == "1"){
//		 		chkWsDeduc.disabled = false;
//			 	chkWsDeduc.checked = true;
//			}
//			else
//			{
//			 	chkWsDeduc.checked = true;
//			 	chkWsDeduc.disabled = false;
//			}
//		}
} 

//% ChangeStatus: Cambia automaticamente el estado de la orden.
//-------------------------------------------------------------------------------------------
function ChangeStatus(Field){
//-------------------------------------------------------------------------------------------
	with (self.document.forms[0])
	{
		if (valProvider.value == "")
			cbeState.value=1
		else
         {
          	<%If  Request.QueryString("Action") <> "Update" Then%>
				 cbeState.value=2;		
     		<%End If%>
          	cbeState.disabled = true;	
            
         }		
	}
}

//% DisabledCombo: Habilita - deshabilita el campo "cbeState" dependiendo de la transacción 
//%                global de la secuencia de siniestros y del tipo de orden - ACM - 11/03/2003
//--------------------------------------------------------------------------------------------
function DisabledCombo(nTransaction, nOrder, nType)
//--------------------------------------------------------------------------------------------
{
	if(nTransaction==4)
	{
		if(nType>0 && nType!=4)
			if(nOrder !="" && nOrder>0)
				self.document.forms[0].elements['cbeState'].disabled = false
			else
				self.document.forms[0].elements['cbeState'].disabled = true;
		else
			if(nOrder !="" && nOrder>0)
				self.document.forms[0].elements['cbeState'].disabled = false;
	}
}


//% insCallAlert: Mensaje de advertencia que el siniestro no tiene "Inicio de reserva"
//--------------------------------------------------------------------------------------------
function insCallAlert()
//--------------------------------------------------------------------------------------------
{
		alert('Adv: El siniestro no tiene provision ');
}


//ChangeValues: Cambia y asigna los valores según la opción seleccionada.
//-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------
function ChangeValues_cover(Option){
//-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------
 
		    top.fraFolder.document.forms[0].hddModulec.value = top.fraFolder.document.forms[0].valCover_nModulec.value;
            top.fraFolder.document.forms[0].hddCover.value = top.fraFolder.document.forms[0].valCover_nCover.value;
}



</script>
<html>
<head>
    <% 
        if  Request.QueryString("sCase_num") = vbNullString Then
        lclsClaimCases = New eClaim.Claim_cases
        If lclsClaimCases.Find(CDbl(Session("nClaim"))) Then
            mstrFirstCase = CStr(lclsClaimCases.Item(1).nCase_num) & "/" & CStr(lclsClaimCases.Item(1).nDeman_type) & "/" & lclsClaimCases.Item(1).sClient & "/" & lclsClaimCases.Item(1).nId
            'UPGRADE_NOTE: Object lclsClaimCases may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
            lclsClaimCases = Nothing
        End If
        Else
        mstrFirstCase = Request.QueryString("sCase_num")
        End If

        If mstrFirstCase <> vbNullString Then
            mstrCase = mstrFirstCase.Split("/")
            Session("nCase_num_11") = mstrCase(0)
            Session("nDeman_type_11") = mstrCase(1)
        End If
        'Response.Write("<SCRIPT>top.frames['fraSequence'].document.location='/VTimeNet/Claim/ClaimSeq/Sequence.aspx?nAction=" & Request.QueryString("nMainAction") & "'</" & "SCRIPT>")
        
        With Response
            .Write("<script>var nMainAction = " & Request.QueryString("nMainAction") & "</script>")
            If Request.QueryString("Type") <> "PopUp" Then
                .Write(mobjMenu.setZone(2, Request.QueryString("sCodispl"), Request.QueryString("sWindowDescript"), Request.QueryString("nWindowTy")))
                mobjMenu = Nothing
            End If
            .Write(mobjValues.StyleSheet() & vbCrLf)
        End With%>
    <meta name="GENERATOR" content="Microsoft Visual Studio 6.0">
    <%=mobjValues.StyleSheet()%>
</head>
<body onunload="closeWindows();">
    <%=mobjValues.ShowWindowsName(Request.QueryString("sCodispl"), Request.QueryString("sWindowDescript"))%>
    <form method="POST" id="FORM" name="frmSI011" action="valClaimSeq.aspx?mode=1">
    <%

        blnbutom = True

        If Not mobjClaim_his.FindMovReserv(CDbl(Session("nClaim"))) Then
            Response.Write("<script>insCallAlert(); </script>")
            blnbutom = False
        End If

        Call insDefineHeader()
        If Request.QueryString("Type") <> "PopUp" Then
            Call insPreSI011()
        Else
            Call insPreSI011Upd()
        End If

        mobjValues = Nothing
        mobjGrid = Nothing
        mobjClaim_his = Nothing
        mobjMenu = Nothing
        mobjProf_ord = Nothing
        mobjProf_ords = Nothing

    %>
    </form>
</body>
</html>
<%
    Call mobjNetFrameWork.FinishPage("si011")
    mobjNetFrameWork = Nothing
%>
