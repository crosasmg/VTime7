<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eProduct" %>
<%@ Import namespace="eRemoteDB" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values
Dim mobjGrid As eFunctions.Grid
Dim mobjMenu As eFunctions.Menues
Dim mclsTab_reqexc As eProduct.Tab_reqexc
Dim mintDefReq As Object

    
'% insDefineHeader : Configura los datos del grid
'---------------------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'---------------------------------------------------------------------------------------------
	'	mobjGrid.ActionQuery = mclsTab_reqexc.bError
	If Not Session("bQuery") Then
		mobjGrid.ActionQuery = mclsTab_reqexc.bError
	End If
	'+ Se definen las columnas del grid    
	With mobjGrid.Columns
		Call .AddPossiblesColumn(41353, GetLocalResourceObject("tctRelationColumnCaption"), "tctRelation", "Table73", eFunctions.Values.eValuesType.clngComboType,  ,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("tctRelationColumnToolTip"))
		'+ Elemento a relacionar
		Call .AddPossiblesColumn(41354, GetLocalResourceObject("tctType1ColumnCaption"), "tctType1", "Table72", eFunctions.Values.eValuesType.clngComboType,  ,  ,  ,  ,  , "insChangeType(this,1)", Request.QueryString.Item("Action") = "Update",  , GetLocalResourceObject("tctType1ColumnToolTip"))
		
		If Request.QueryString.Item("Type") <> "PopUp" Then
			Call .AddTextColumn(0, GetLocalResourceObject("tctElement1_1ColumnCaption"), "tctElement1_1", 30, "",  , GetLocalResourceObject("tctElement1_1ColumnCaption"))
			Call .AddHiddenColumn("tctElement1", CStr(0))
		Else
			Call .AddPossiblesColumn(41355, GetLocalResourceObject("tctElement1ColumnCaption"), "tctElement1", getTabname(Mid(mclsTab_reqexc.sReqExcList, 1, 1)), eFunctions.Values.eValuesType.clngWindowType,  , True,  ,  ,  , "SetValue(this);", True, 4, GetLocalResourceObject("tctElement1ColumnToolTip"))
		End If
		
		If CStr(Session("sBrancht")) = "1" Or CStr(Session("sBrancht")) = "2" Then
                Call .AddPossiblesColumn(0, GetLocalResourceObject("valRole1ColumnCaption"), "valRole1", "TABTAB_COVROL3_1", eFunctions.Values.eValuesType.clngWindowType, "0", True, , , , , True, , GetLocalResourceObject("valRole1ColumnToolTip"))
            End If
		
            '+ Elemento relacionado
            Call .AddPossiblesColumn(41356, GetLocalResourceObject("tctType2ColumnCaption"), "tctType2", "Table72", eFunctions.Values.eValuesType.clngComboType, , , , , , "insChangeType(this,2)", Request.QueryString.Item("Action") = "Update", , GetLocalResourceObject("tctType2ColumnToolTip"))
		
            If Request.QueryString.Item("Type") <> "PopUp" Then
                Call .AddTextColumn(0, GetLocalResourceObject("tctElement2_2ColumnCaption"), "tctElement2_2", 30, "", , GetLocalResourceObject("tctElement2_2ColumnCaption"))
                Call .AddHiddenColumn("tctElement2", CStr(0))
            Else
                Call .AddPossiblesColumn(41357, GetLocalResourceObject("tctElement2ColumnCaption"), "tctElement2", getTabname(Mid(mclsTab_reqexc.sReqExcList, 1, 1)), eFunctions.Values.eValuesType.clngWindowType, , True, , , , "SetValue(this);", True, 4, GetLocalResourceObject("tctElement2ColumnToolTip"))
            End If
		
            If CStr(Session("sBrancht")) = "1" Or CStr(Session("sBrancht")) = "2" Then
                Call .AddPossiblesColumn(0, GetLocalResourceObject("valRole2ColumnCaption"), "valRole2", "TabTab_CovRol3_1", eFunctions.Values.eValuesType.clngWindowType, "0", True, , , , , True, , GetLocalResourceObject("valRole2ColumnToolTip"))
            End If
		
            Call .AddHiddenColumn("hddModulec1_1", "0")
		
        End With
	
        '+ Se definen las propiedades generales del grid
        With mobjGrid
            .Codispl = "DP038"
            .Width = 290
            .Height = 320
		
            .sEditRecordParam = "nDefReq=" & mintDefReq
		
            .nMainAction = CShort(IIf(Request.QueryString.Item("nMainAction") = "", 0, Request.QueryString.Item("nMainAction")))
		
            .Columns("tctElement1").Parameters.Add("nBranch", mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Columns("tctElement1").Parameters.Add("nProduct", mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Columns("tctElement1").Parameters.Add("dEffecdate", mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            
            If CStr(Session("sBrancht")) = 8 Then
                .Columns("tctElement1").Parameters.Add("nCover", mobjValues.StringToType(Session("nConver"), eFunctions.Values.eTypeData.etdInteger), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Columns("tctElement1").Parameters.Add("nModulec", mobjValues.StringToType(Session("nModulec"), eFunctions.Values.eTypeData.etdInteger), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            End If
            .Columns("tctElement2").Parameters.Add("nBranch", mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Columns("tctElement2").Parameters.Add("nProduct", mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Columns("tctElement2").Parameters.Add("dEffecdate", mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            
            If CStr(Session("sBrancht")) = 8 Then
                .Columns("tctElement2").Parameters.Add("nCover", mobjValues.StringToType(Session("nConver"), eFunctions.Values.eTypeData.etdInteger), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Columns("tctElement2").Parameters.Add("nModulec", mobjValues.StringToType(Session("nModulec"), eFunctions.Values.eTypeData.etdInteger), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            End If
            
            .Columns("tctType1").TypeList = 1
            .Columns("tctType2").TypeList = 1
            .Columns("tctType2").List = mclsTab_reqexc.sReqExcList
            .Columns("tctType1").List = mclsTab_reqexc.sReqExcList
		
            If CStr(Session("sBrancht")) = "1" Or CStr(Session("sBrancht")) = "2" Then
                .Columns("valRole1").Parameters.Add("nBranch", mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Columns("valRole1").Parameters.Add("nProduct", mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Columns("valRole1").Parameters.Add("nCover", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Columns("valRole1").Parameters.Add("dEffecdate", mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Columns("valRole1").Parameters.Add("nModulec", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
                .Columns("valRole2").Parameters.Add("nBranch", mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Columns("valRole2").Parameters.Add("nProduct", mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Columns("valRole2").Parameters.Add("nCover", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Columns("valRole2").Parameters.Add("dEffecdate", mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Columns("valRole2").Parameters.Add("nModulec", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            End If
		
            Call .Splits_Renamed.AddSplit(0, vbNullString, 1)
		
            If CStr(Session("sBrancht")) = "1" Or CStr(Session("sBrancht")) = "2" Then 'Vida
                Call .Splits_Renamed.AddSplit(0, GetLocalResourceObject("2ColumnCaption"), 3)
                Call .Splits_Renamed.AddSplit(0, GetLocalResourceObject("3ColumnCaption"), 3)
            Else
                Call .Splits_Renamed.AddSplit(0, GetLocalResourceObject("2ColumnCaption"), 2)
                Call .Splits_Renamed.AddSplit(0, GetLocalResourceObject("2ColumnCaption"), 2)
            End If
		
            If Request.QueryString.Item("Reload") = "1" Then
                .sReloadIndex = Request.QueryString.Item("ReloadIndex")
            End If
        End With
    End Sub

    '% insPreDP038 : Carga los datos que corresponden al grid.
    '---------------------------------------------------------------------------------------------
    Private Function insPreDP038() As Object
        '---------------------------------------------------------------------------------------------
        Dim lclsQuery As eRemoteDB.Query
        lclsQuery = New eRemoteDB.Query
	
        Dim lintCount As Byte
        Dim lcolTab_reqexcs As eProduct.Tab_reqexcs
        lcolTab_reqexcs = New eProduct.Tab_reqexcs
	
        lintCount = 0
        If mclsTab_reqexc.bError Then
            With Response
                .Write(mobjGrid.closeTable)
                .Write(mobjValues.BeginPageButton)
            End With
            Response.Write(mclsTab_reqexc.sError)
        Else
            If lcolTab_reqexcs.FindDP038(mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), Session("sBrancht"), mintDefReq) Then
                With mobjGrid
                    For Each mclsTab_reqexc In lcolTab_reqexcs
                        .Columns("tctRelation").DefValue = mclsTab_reqexc.sRelation
                        If CStr(Session("sBrancht")) = "1" Or CStr(Session("sBrancht")) = "2" Then
                            .Columns("valRole1").Parameters.Add("nCover", mclsTab_reqexc.nCode1, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                            .Columns("valRole1").Parameters.Add("nModulec", mclsTab_reqexc.nModulec1, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                            .Columns("valRole1").DefValue = CStr(mclsTab_reqexc.nRole1)
                            .Columns("valRole2").Parameters.Add("nCover", mclsTab_reqexc.nCode2, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                            .Columns("valRole2").Parameters.Add("nModulec", mclsTab_reqexc.nModulec2, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                            .Columns("valRole2").DefValue = CStr(mclsTab_reqexc.nRole2)
                        End If
                        .Columns("tctRelation").EditRecord = True
                        .sDelRecordParam = "sType1=' + marrArray[lintIndex].tctType1 + '&sElement1=' + marrArray[lintIndex].tctElement1 + '&sRole1=' + marrArray[lintIndex].valRole1 + '&sType2=' + marrArray[lintIndex].tctType2 + '&sElement2=' + marrArray[lintIndex].tctElement2 + '&sRole2=' + marrArray[lintIndex].valRole2 + '&sRelation=' + marrArray[lintIndex].tctRelation +  '" & "&nDefReq= " & mintDefReq
					
                        '+ Obtiene los valores posibles del elemento según el tipo
                        .Columns("tctType1").DefValue = mclsTab_reqexc.sType1
                        Select Case mclsTab_reqexc.sType1
                            Case CStr(1)
                                .Columns("tctElement1").TableName = "TabTab_modul"
                            Case CStr(2)
							
                                If CStr(Session("sBrancht")) = "1" Or CStr(Session("sBrancht")) = "2" Then
                                    .Columns("tctElement1").TableName = "TabLife_cover_des"
                                Else
                                    .Columns("tctElement1").TableName = "TabGen_cover_des"
                                End If
                            Case CStr(3)
                                .Columns("tctElement1").TableName = "TabDisco_exprc_re"
							
                            Case CStr(4)
                                .Columns("tctElement1").TableName = "TabTab_clause_a"
                                
                            Case CStr(6)
                                .Columns("tctElement1").TableName = "TABTAB_FIGROL"
                                
                        End Select
                        .Columns("tctElement1_1").DefValue = mclsTab_reqexc.sDesReqExc1
                        .Columns("tctElement1").DefValue = CStr(mclsTab_reqexc.nCode1)
					
					
                        '+ Obtiene los valores posibles del elemento según el tipo
                        .Columns("tctType2").DefValue = mclsTab_reqexc.sType2
                        Select Case mclsTab_reqexc.sType2
                            Case CStr(1)
                                .Columns("tctElement2").TableName = "TabTab_modul"
                            Case CStr(2)
							
                                If CStr(Session("sBrancht")) = "1" Or CStr(Session("sBrancht")) = "2" Then
                                    .Columns("tctElement2").TableName = "TabLife_cover_des"
                                Else
                                    .Columns("tctElement2").TableName = "TabGen_cover_des"
                                End If
                            Case CStr(3)
                                .Columns("tctElement2").TableName = "TabDisco_exprc_re"
                            Case CStr(4)
                                .Columns("tctElement2").TableName = "TabTab_clause_a"
                            Case CStr(6)
                                .Columns("tctElement1").TableName = "TABTAB_FIGROL"
                        End Select
                        .Columns("tctElement2_2").DefValue = mclsTab_reqexc.sDesReqExc2
                        .Columns("tctElement2").DefValue = CStr(mclsTab_reqexc.nCode2)
					
                        '	                .sDelRecordParam = "sType1=' + marrArray[lintIndex].tctType1 + '&sElement1=' + marrArray[lintIndex].tctElement1 + '&sType2=' + marrArray[lintIndex].tctType2 + '&sElement2=' + marrArray[lintIndex].tctElement2 + '&sRelation=' + marrArray[lintIndex].tctRelation + tctElement1.sTabName = mstrTab_tables +'"
                        '					.sEditRecordParam = "&nMainAction=" & Request.QueryString("nMainAction") '					                      & tctElement1.sTabName = mstrTab_tables; 
					
                        '					If Request.QueryString("nDefReq") = "" Then
                        '						.sEditRecordParam = "nDefReq=1"
                        '					Else
                        '						.sEditRecordParam = "nDefReq=" & Request.QueryString("nDefReq")
                        '					End If
					
                        Response.Write(mobjGrid.DoRow())
                    Next mclsTab_reqexc
                End With
            End If
            With Response
                .Write(mobjGrid.closeTable)
                .Write(mobjValues.BeginPageButton)
            End With
		
            If lclsQuery.OpenQuery("Tab_Modul", "nModulec", "nBranch=" & mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble) & "AND nProduct=" & mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble) & "AND ROWNUM  <= 1") Then
                Response.Write(mobjValues.HiddenControl("hddModulec", lclsQuery.FieldToClass("nModulec")))
            Else
                Response.Write(mobjValues.HiddenControl("hddModulec", CStr(0)))
            End If
        End If
	
        mclsTab_reqexc = Nothing
        lcolTab_reqexcs = Nothing
    End Function

'% insPreDP038Upd: Se muestra la ventana Popup para efecto de actualización del Grid
'--------------------------------------------------------------------------------------------
Private Sub insPreDP038Upd()
	'--------------------------------------------------------------------------------------------
	With Request
		If Request.QueryString.Item("Action") = "Del" Then
			Response.Write(mobjValues.ConfirmDelete)
			
			'+ Si el tipo de producto es diferente a "Vida" y "Vida Colectivo", se pasan los parámetros correspondientes
			'+ a los roles siempre y cuando el tipo de elemento sea "Cobertura"
			If CStr(Session("sBrancht")) = "1" Or CStr(Session("sBrancht")) = "2" Then
				If mclsTab_reqexc.insPostDP038("DP038", CInt(.QueryString.Item("nMainAction")), .QueryString.Item("Action"), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.QueryString.Item("sRelation"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("sType1"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("sElement1"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("sRole1"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("sType2"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("sElement2"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("sRole2"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nDefReq"), eFunctions.Values.eTypeData.etdLong)) Then
					Response.Write("<SCRIPT>top.opener.top.frames['fraSequence'].document.location='/VTimeNet/Product/ProductSeq/Sequence.aspx?nAction=" & Request.QueryString.Item("nMainAction") & "&nOpener=" & Request.QueryString.Item("sCodispl") & "&sGoToNext=NO" & "';</" & "Script>")
				End If
			Else
				If mclsTab_reqexc.insPostDP038("DP038", CInt(.QueryString.Item("nMainAction")), .QueryString.Item("Action"), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.QueryString.Item("sRelation"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("sType1"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("sElement1"), eFunctions.Values.eTypeData.etdDouble), 0, mobjValues.StringToType(.QueryString.Item("sType2"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("sElement2"), eFunctions.Values.eTypeData.etdDouble), 0, mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nDefReq"), eFunctions.Values.eTypeData.etdLong)) Then
					Response.Write("<SCRIPT>top.opener.top.frames['fraSequence'].document.location='/VTimeNet/Product/ProductSeq/Sequence.aspx?nAction=" & Request.QueryString.Item("nMainAction") & "&nOpener=" & Request.QueryString.Item("sCodispl") & "&sGoToNext=NO" & "';</" & "Script>")
				End If
			End If
		End If
		Response.Write(mobjGrid.DoFormUpd(.QueryString.Item("Action"), "valProductSeq.aspx", "DP038", .QueryString.Item("nMainAction"), mobjGrid.ActionQuery, CShort(.QueryString.Item("Index"))))
		
		If .QueryString.Item("Action") = "Update" Or .QueryString.Item("Action") = "Add" Then
			
Response.Write("" & vbCrLf)
Response.Write("	          <SCRIPT>" & vbCrLf)
Response.Write("				with(self.document.forms[0]){" & vbCrLf)
Response.Write("					getTabname(tctType1.value)" & vbCrLf)
Response.Write("					tctElement1.sTabName  = mstrTab_tables;" & vbCrLf)
Response.Write("" & vbCrLf)
Response.Write("					getTabname(tctType2.value)" & vbCrLf)
Response.Write("					tctElement2.sTabName = mstrTab_tables;" & vbCrLf)
Response.Write("					" & vbCrLf)
Response.Write("					hddModulec1_1.value   = top.opener.document.forms[0].elements[""hddModulec""].value" & vbCrLf)
Response.Write("					" & vbCrLf)
Response.Write("				}" & vbCrLf)
Response.Write("              </" & "SCRIPT>				" & vbCrLf)
Response.Write("          ")

			
		End If
		
	End With
End Sub

'% getTabname: se asigna el valor del tab_tables asociado al elemento a relacionar
'-------------------------------------------------------------------------------------------
Function getTabname(ByRef nValue As Object) As String
	'-------------------------------------------------------------------------------------------		
	
	Select Case nValue.ToString.Trim
		Case "1"
			getTabname = "TabTab_modul"
		Case "3"
			getTabname = "TabDisco_exprc_re"
		Case "4"
			getTabname = "TabTab_clause_a"
		Case "5"
			getTabname = "TabTar_am_BasProd_1"
       Case "6"
                getTabname = "TABTAB_FIGROL"
        Case "2"
                If CStr(Session("sBrancht")) = "1" Or CStr(Session("sBrancht")) = "2" Then
                    getTabname = "TabLife_cover_des"
                Else
                    getTabname = "TabGen_cover_des"
                End If
        End Select
End Function

</script>
<%Response.Expires = -1

mobjValues = New eFunctions.Values
mobjGrid = New eFunctions.Grid
mclsTab_reqexc = New eProduct.Tab_reqexc

mobjGrid.sCodisplPage = "DP038"
mobjValues.sCodisplPage = "DP038"

mobjGrid.ActionQuery = Session("bQuery")

If IsNothing(Request.QueryString.Item("nDefReq")) Then
	mintDefReq = 1
Else
	mintDefReq = Request.QueryString.Item("nDefReq")
End If

%>
<HTML>
<HEAD>
    <META NAME = "GENERATOR" CONTENT = "Microsoft Visual Studio 6.0">

<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT>
	var mstrBrancht = '<%=Session("sBrancht")%>';

//- Variable para el control de versiones
	document.VssVersion="$$Revision: 2 $|$$Date: 15/10/03 17:21 $|$$Author: Nvaplat61 $"

	var mstrTab_tables = '';

//% getTabname: se asigna el valor del tab_tables asociado al elemento a relacionar
//-------------------------------------------------------------------------------------------
function getTabname(nValue){
//-------------------------------------------------------------------------------------------    
    switch(nValue){
        case '1':
            mstrTab_tables = 'TabTab_modul';
            break;

        case '3':
            mstrTab_tables = 'TabDisco_exprc_re';
            break;

        case '4':
            mstrTab_tables = 'TabTab_clause_a';
            break;
        
        case '5':
            mstrTab_tables = 'TabTar_am_BasProd_1';
            break;
        case '6':
            mstrTab_tables = 'TABTAB_FIGROL';
            break;
            
        case '2':
            if (mstrBrancht=='1' ||
                mstrBrancht=='2')
                mstrTab_tables = 'TabLife_cover_des';
            else
                mstrTab_tables = 'TabGen_cover_des';
    }
    if(mstrTab_tables!='')
		mblnTabtables=true
}

//% insChangeType: Actualiza los valores posibles de los elementos a relacionar
//-------------------------------------------------------------------------------------------
function insChangeType(Field, nIndex){
//-------------------------------------------------------------------------------------------

    var nBranch = '<%=Session("nBranch")%>';
	var nProduct = '<%=Session("nProduct")%>';
	var dEffecdate = '<%=Session("dEffecdate")%>';
	var Action = '<%=Request.QueryString.Item("Action")%>';
	var nCover = "0";
	var nModulec = "0";

	getTabname(Field.value)

	with(self.document.forms[0]){

		if (nIndex==1){
			tctElement1.sTabName = mstrTab_tables;
			tctElement1.value = '';
			
			tctElement1.Parameters.Param1.sValue = nBranch;
			tctElement1.Parameters.Param2.sValue = nProduct;
			tctElement1.Parameters.Param3.sValue = dEffecdate;

//			if (mstrTab_tables =='TABTAB_FIGROL') {
//			    tctElement1.Parameters.Param4.sValue = nCover;

//			    tctElement1.Parameters.Param5.sValue = nModulec;
//			}
			if (Field.value == "0"){
				tctElement1.disabled = true;
				btntctElement1.disabled = true;
				UpdateDiv('tctElement1Desc', '');
			}
			else{
				tctElement1.disabled = false;
				btntctElement1.disabled = false;
				UpdateDiv('tctElement1Desc', '');
			}	

			if (Action == "Update"){
				tctElement1.disabled = true;
				btntctElement1.disabled = true;				
			}
		}
		else{
			tctElement2.sTabName = mstrTab_tables;
			tctElement2.value = '';			

			tctElement2.Parameters.Param1.sValue = nBranch;
			tctElement2.Parameters.Param2.sValue = nProduct;
			tctElement2.Parameters.Param3.sValue = dEffecdate;

//			if (mstrTab_tables == 'TABTAB_FIGROL') {
//			    tctElement2.Parameters.Param4.sValue = nCover;
//			    tctElement2.Parameters.Param5.sValue = nModulec;
//			}
            
			if (Field.value == "0"){
				tctElement2.disabled = true;
				btntctElement2.disabled = true;
				UpdateDiv('tctElement2Desc', '');
			}
			else{
				tctElement2.disabled = false;
				btntctElement2.disabled = false;
				UpdateDiv('tctElement2Desc', '');
			}	

			if (Action == "Update"){
				tctElement2.disabled = true;
				btntctElement2.disabled = true;				
			}            
		}
		
//- El campo "Rol" sólo se habilita si el elemento corresponde a "Cobertura".

        if (mstrBrancht=='1' ||
            mstrBrancht == '2' ) {
	    	if(Field.name == 'tctType1'){
	    		if(Field.value == 2){
	    			valRole1.disabled = false;
	    			btnvalRole1.disabled = false;
	    			valRole1.value='';
	    		}			
	    		else {
	    			valRole1.disabled = true;
	    			btnvalRole1.disabled = true;
	    			valRole1.value='0';
	    			UpdateDiv('valRole1Desc','');
	    		}
	    	}
	    	else {
	    		if(Field.name == 'tctType2'){
	    			if(Field.value == 2 ){
	    				valRole2.disabled = false;
	    				btnvalRole2.disabled = false;
	    				valRole2.value='';
	    			}
	    			else {
	    				valRole2.disabled = true;
	    				btnvalRole2.disabled = true;
	    				valRole2.value='0';
	    				UpdateDiv('valRole2Desc','');
	    			}
	    		}
	    	}
	    }
	}
}

//% SetValue: Se obtiene el nro. de la cobertura para buscar los roles asociados
//----------------------------------------------------------------------------------------
function SetValue(Field){
//----------------------------------------------------------------------------------------
	if (mstrBrancht=='1' ||
        mstrBrancht == '2' || mstrBrancht == '8') {
	    if(Field.name == 'tctElement1'){
	        if(Field.value != 0 && Field.value != '')
	        	self.document.forms[0].valRole1.Parameters.Param3.sValue = Field.value;
	        	self.document.forms[0].valRole1.Parameters.Param5.sValue = self.document.forms[0].hddModulec1_1.value;
	    }
	    else {
	        if(Field.name == 'tctElement2'){
	            if(Field.value != 0 && Field.value != '')
	            	self.document.forms[0].valRole2.Parameters.Param3.sValue = Field.value;	
	            	self.document.forms[0].valRole2.Parameters.Param5.sValue = self.document.forms[0].hddModulec1_1.value;
            }	        	
	    }	
    }	    
}

//+ Se recarga la página para que muestre los requisitos/exclusiones de siniestros o de cartera
//----------------------------------------------------------------------------------------------------------------------
function insChangeTypeReq(nDefReq){
//----------------------------------------------------------------------------------------------------------------------
	var lstrstring = '';
	if (nDefReq != '<%=mintDefReq%>'){
		lstrstring += document.location;
		lstrstring = lstrstring.replace(/&nDefReq=.*/, "");
		lstrstring = lstrstring + "&nDefReq="+nDefReq;
		document.location.href = lstrstring;
	}
}

//+ Setea los campos cuando la definición es a nivel de siniestros
//----------------------------------------------------------------------------------------------------------------------
function insSetDefClaim(){
//----------------------------------------------------------------------------------------------------------------------
    var Action     = '<%=Request.QueryString.Item("Action")%>'
    with(self.document.forms[0]){
        if (Action=='Add'){
	        tctType1.value = 2;
			insChangeType(tctType1,1);
			tctType1.disabled = true;
			tctType2.value = 2;
			insChangeType(tctType2,2);
			tctType2.disabled = true;

        }

       if (Action != 'Del'){
            //Se oculta el rol ya que para siniestros no es necesario    
            document.getElementsByTagName("TR")[6].style.display = 'none';
            //Se oculta el rol ya que para siniestros no es necesario        
            document.getElementsByTagName("TR")[14].style.display = 'none';
       }
    }
}

</SCRIPT>
<%
With Response
	.Write("<SCRIPT>var nMainAction = " & Request.QueryString.Item("nMainAction") & "</SCRIPT>")
	.Write(mobjValues.StyleSheet())
	.Write(mobjValues.WindowsTitle("DP038"))
	If Request.QueryString.Item("Type") <> "PopUp" Then
		mobjMenu = New eFunctions.Menues
		.Write(mobjMenu.setZone(2, "DP038", "DP038.aspx"))
		mobjMenu = Nothing
	End If
End With
%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="post" ID="FORM" NAME="frmDP038" ACTION="valProductSeq.aspx?sZone=2&nMainAction=<%=Request.QueryString.Item("nMainAction")%>">
<%Response.Write(mobjValues.ShowWindowsName(Request.QueryString.Item("sCodispl")))
If Request.QueryString.Item("Type") <> "PopUp" Then%>
		<BR>
		<TABLE WIDTH="50%" ALIGN="CENTER"> 
			<TR>
				<TD COLSPAN="2" CLASS="HighLighted"><LABEL ID=100166><A NAME="Definición"><%= GetLocalResourceObject("AnchorDefiniciónCaption") %></A></LABEL></TD>
			</TR>   
			<TR>
				<TD COLSPAN="2" CLASS="HORLINE"></TD>
			</TR>   
			<TR>
			    <%	If mintDefReq = "1" Then%>
					<TD><%=mobjValues.OptionControl(0, "optDefReq", GetLocalResourceObject("optDefReq_1Caption"), CStr(1), "1", "insChangeTypeReq(this.value)",  , 1)%></TD>
					<TD><%=mobjValues.OptionControl(0, "optDefReq", GetLocalResourceObject("optDefReq_2Caption"),  , "2", "insChangeTypeReq(this.value)",  , 2)%></TD>
				<%	Else%>
					<TD><%=mobjValues.OptionControl(0, "optDefReq", GetLocalResourceObject("optDefReq_1Caption"),  , "1", "insChangeTypeReq(this.value)",  , 1)%></TD>
					<TD><%=mobjValues.OptionControl(0, "optDefReq", GetLocalResourceObject("optDefReq_2Caption"), CStr(1), "2", "insChangeTypeReq(this.value)",  , 2)%></TD>
				<%	End If%>
			</TR>
		</TABLE>
		<BR>
<%Else
	Response.Write(mobjValues.HiddenControl("hddDefReq", mintDefReq))
End If

With mobjValues
	Call mclsTab_reqexc.insPreDP038(.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), .StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), .StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), Session("sBrancht"))
End With
Call insDefineHeader()
If Request.QueryString.Item("Type") = "PopUp" Then
	Call insPreDP038Upd()
Else
	Call insPreDP038()
End If

If Request.QueryString.Item("Type") = "PopUp" And mintDefReq = 2 Then
	Response.Write("<SCRIPT>insSetDefClaim();</SCRIPT>")
End If

mobjValues = Nothing
mobjGrid = Nothing
mclsTab_reqexc = Nothing

%>
</FORM>
</BODY>
</HTML>





