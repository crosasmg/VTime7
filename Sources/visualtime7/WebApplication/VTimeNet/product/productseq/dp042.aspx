<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eProduct" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores

Dim mobjValues As eFunctions.Values
Dim mobjMenu As eFunctions.Menues
Dim mobjGrid As eFunctions.Grid
Dim mintCount As Integer
Dim mintCountAUX As Object

Dim lclsProduct As eProduct.Product


'%insDefineHeader: Se definen las columnas de la grilla	
'----------------------------------------------------------------------------------------------
Private Sub insDefineHeaderDP042()
	'----------------------------------------------------------------------------------------------
	With mobjGrid.Columns
		Call .AddPossiblesColumn(41379, GetLocalResourceObject("cboClientRolColumnCaption"), "cboClientRol", "Table12", eFunctions.Values.eValuesType.clngComboType, CStr(0), False,  ,  ,  ,  , True, 5, GetLocalResourceObject("cboClientRolColumnToolTip"))
		Call .AddCheckColumn(41381, GetLocalResourceObject("chkObligatoriaColumnCaption"), "chkObligatoria", "",  ,  , "insRequiredValue(this);", True, GetLocalResourceObject("chkObligatoriaColumnToolTip"))
		Call .AddCheckColumn(41382, GetLocalResourceObject("chkDefaultColumnCaption"), "chkDefault", "",  ,  , "insDefaultValue(this);", True, GetLocalResourceObject("chkDefaultColumnToolTip"))
		Call .AddNumericColumn(41380, GetLocalResourceObject("tcnMaxValueColumnCaption"), "tcnMaxValue", 5, CStr(0), True, GetLocalResourceObject("tcnMaxValueColumnToolTip"), False, 0,  ,  , "insErrorMessage(this.value);")
		Call .AddCheckColumn(0, GetLocalResourceObject("chkOptionalQuoColumnCaption"), "chkOptionalQuo", "",  ,  , "insOptional(this);", True, GetLocalResourceObject("chkOptionalQuoColumnToolTip"))
		Call .AddHiddenColumn("tctPoliType", "")
		Call .AddHiddenColumn("sRequired", "")
		Call .AddHiddenColumn("sDefault", "")
		Call .AddHiddenColumn("tcnChecked", "")
		Call .AddHiddenColumn("sOptionalQuo", "")
		Call .AddHiddenColumn("tcnIndividual", "")
		Call .AddHiddenColumn("tcnColectiva", "")
		Call .AddHiddenColumn("tcnMultilocalidad", "")
		Call .AddHiddenColumn("tcnCounter", "")
		Call .AddHiddenColumn("tctCompon", "")
		Call .AddHiddenColumn("tctInitialSelection", "")
		Call .AddHiddenColumn("tcnMaxValue2", CStr(0))
		Call .AddHiddenColumn("tcnBranch", "")
		Call .AddHiddenColumn("tcnProduct", "")
		Call .AddHiddenColumn("tcdEffecdate", "")
		Call .AddHiddenColumn("tcnUserCode", "")
		Call .AddHiddenColumn("tcnClientRol", "")
		Call .AddHiddenColumn("tctPolicyType", "")
		Call .AddHiddenColumn("tctComponent", "")
		Call .AddHiddenColumn("tctAction", "")
		Call .AddHiddenColumn("tcnAction", CStr(0))
		Call .AddHiddenColumn("sParam", vbNullString)
		Call .AddHiddenColumn("hddHolder", CStr(2))
	End With
	
	With mobjGrid
		.Codispl = "DP042"
		.Codisp = "DP042"
		.Height = 280
		.Width = 350
		.Columns("cboClientRol").EditRecord = True
		.DeleteButton = False
		.AddButton = False
		.Columns("Sel").OnClick = "insSelected(this); MarkRecord(this);"
		.sDelRecordParam = "' + marrArray[lintIndex].sParam + '"
		.MoveRecordScript = "insIncreaseValue(self.document.forms[0].elements[""tcnMaxValue""].value);"
		
		If Request.QueryString.Item("Reload") = "1" Then
			.sReloadIndex = Request.QueryString.Item("ReloadIndex")
		End If
	End With
End Sub

'% Se obtienen los clientes permitidos en una póliza para un producto.
'----------------------------------------------------------------------------------------------
Private Sub insPreDP042()
	'----------------------------------------------------------------------------------------------
	Dim lclsProduct As eProduct.Product
	Dim lcolCliallopro As eProduct.Cliallopros
	Dim lintRequired As Integer
	Dim lintDefault As Integer
	Dim lintInd As Object
	Dim lintCol As Object
	Dim lintMul As Object
	Dim lstrHolder As String
	Dim lclsErrors As eFunctions.Errors
	Dim lblnValidate As Boolean
	Dim lclsProductWin As eProduct.Prod_win
	Dim lstrCompon As String
	
	lstrHolder = vbNullString
	
	lclsProductWin = New eProduct.Prod_win
	lclsProduct = New eProduct.Product
	lcolCliallopro = New eProduct.Cliallopros
	
	Call lclsProduct.Find(mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate))
	
	Select Case Request.QueryString.Item("sPoliType")
		Case "1"
			lintInd = 1
			lintCol = 2
			lintMul = 2
		Case "2"
			lintInd = 2
			lintCol = 1
			lintMul = 2
		Case "3"
			lintInd = 2
			lintCol = 2
			lintMul = 1
	End Select
	
        lstrHolder = IIf(lclsProduct.sHolder = "3", "25", lclsProduct.sHolder)
	
	
Response.Write("" & vbCrLf)
Response.Write("    <TABLE WIDTH=""100%"">		" & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("			<TD WIDTH=""100%"" COLSPAN=""6"" CLASS=""HighLighted""><LABEL ID=41362><A NAME=""Tipo de póliza"">" & GetLocalResourceObject("AnchorTipo de pólizaCaption") & "</A></LABEL></TD>" & vbCrLf)
Response.Write("		</TR>" & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("			<TD COLSPAN=""6"" CLASS=""HORLINE""></TD>		" & vbCrLf)
Response.Write("		</TR>		" & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("			<TD WIDTH=""25%"">")


Response.Write(mobjValues.OptionControl(41363, "OptPolicyType", GetLocalResourceObject("OptPolicyType_CStr1Caption"), lintInd, CStr(1), "insEnabledPolicyType(this.checked, 1);", lclsProduct.sIndivind <> "1",  , GetLocalResourceObject("OptPolicyType_CStr1ToolTip")))


Response.Write("</TD>" & vbCrLf)
Response.Write("			<TD>")


Response.Write(mobjValues.OptionControl(41365, "OptPolicyType", GetLocalResourceObject("OptPolicyType_CStr2Caption"), lintCol, CStr(2), "insEnabledPolicyType(this.checked, 2);", lclsProduct.sGroupind <> "1",  , GetLocalResourceObject("OptPolicyType_CStr2ToolTip")))


Response.Write("</TD>" & vbCrLf)
Response.Write("            <TD> &nbsp; </TD>" & vbCrLf)
Response.Write("			<TD>")


Response.Write(mobjValues.OptionControl(41367, "OptPolicyType", GetLocalResourceObject("OptPolicyType_CStr3Caption"), lintMul, CStr(3), "insEnabledPolicyType(this.checked, 3);", lclsProduct.sMultiind <> "1",  , GetLocalResourceObject("OptPolicyType_CStr3ToolTip")))


Response.Write("</TD>" & vbCrLf)
Response.Write("            <TD> &nbsp; </TD>" & vbCrLf)
Response.Write("		</TR>" & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("            <TD> &nbsp; </TD>" & vbCrLf)
Response.Write("            <TD> &nbsp; </TD>" & vbCrLf)
Response.Write("		")

	
	If Request.QueryString.Item("sComponent") = "1" And Request.QueryString.Item("sPoliType") = "2" Then
		
Response.Write("" & vbCrLf)
Response.Write("  			<TD>")


Response.Write(mobjValues.OptionControl(41368, "OptGroupindMaster", GetLocalResourceObject("OptGroupindMaster_CStr1Caption"), CStr(1), CStr(1), "insEnabledPolicyType(this.checked, 2, 1);", lclsProduct.sGroupind <> "1" Or Request.QueryString.Item("sPoliType") = "1",  , GetLocalResourceObject("OptGroupindMaster_CStr1ToolTip")))


Response.Write("</TD>" & vbCrLf)
Response.Write("        ")

		
	Else
		
Response.Write("" & vbCrLf)
Response.Write("        	<TD>")


Response.Write(mobjValues.OptionControl(41368, "OptGroupindMaster", GetLocalResourceObject("OptGroupindMaster_CStr1Caption"),  , CStr(1), "insEnabledPolicyType(this.checked, 2, 1);", lclsProduct.sGroupind <> "1" Or Request.QueryString.Item("sPoliType") = "1",  , GetLocalResourceObject("OptGroupindMaster_CStr1ToolTip")))


Response.Write("</TD>" & vbCrLf)
Response.Write("		")

		
	End If
	
Response.Write("" & vbCrLf)
Response.Write("            <TD> &nbsp; </TD>" & vbCrLf)
Response.Write("		")

	
	If Request.QueryString.Item("sComponent") = "1" And Request.QueryString.Item("sPoliType") = "3" Then
		
Response.Write("" & vbCrLf)
Response.Write("			<TD>")


Response.Write(mobjValues.OptionControl(41369, "OptMultindMaster", GetLocalResourceObject("OptMultindMaster_CStr1Caption"), CStr(1), CStr(1), "insEnabledPolicyType(this.checked, 3, 1);", lclsProduct.sMultiind <> "1" Or Request.QueryString.Item("sPoliType") = "1",  , GetLocalResourceObject("OptMultindMaster_CStr1ToolTip")))


Response.Write("</TD>" & vbCrLf)
Response.Write("		")

		
	Else
		
Response.Write("" & vbCrLf)
Response.Write("			<TD>")


Response.Write(mobjValues.OptionControl(41369, "OptMultindMaster", GetLocalResourceObject("OptMultindMaster_CStr1Caption"),  , CStr(1), "insEnabledPolicyType(this.checked, 3, 1);", lclsProduct.sMultiind <> "1" Or Request.QueryString.Item("sPoliType") = "1",  , GetLocalResourceObject("OptMultindMaster_CStr1ToolTip")))


Response.Write("</TD>" & vbCrLf)
Response.Write("		")

		
	End If
	
Response.Write("" & vbCrLf)
Response.Write("		</TR>" & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("			<TD> &nbsp; </TD>" & vbCrLf)
Response.Write("			<TD> &nbsp; </TD>" & vbCrLf)
Response.Write("		")

	
	If Request.QueryString.Item("sComponent") = "2" And Request.QueryString.Item("sPoliType") = "2" Then
		
Response.Write("" & vbCrLf)
Response.Write("			<TD WIDTH=""26%"">")


Response.Write(mobjValues.OptionControl(41370, "OptGroupindMaster", GetLocalResourceObject("OptGroupindMaster_CStr2Caption"), CStr(1), CStr(2), "insEnabledPolicyType(this.checked, 2, 2);", lclsProduct.sGroupind <> "1" Or Request.QueryString.Item("sPoliType") = "1",  , GetLocalResourceObject("OptGroupindMaster_CStr2ToolTip")))


Response.Write("</TD>" & vbCrLf)
Response.Write("		")

		
	Else
		
Response.Write("" & vbCrLf)
Response.Write("			<TD WIDTH=""26%"">")


Response.Write(mobjValues.OptionControl(41370, "OptGroupindMaster", GetLocalResourceObject("OptGroupindMaster_CStr2Caption"),  , CStr(2), "insEnabledPolicyType(this.checked, 2, 2);", lclsProduct.sGroupind <> "1" Or Request.QueryString.Item("sPoliType") = "1",  , GetLocalResourceObject("OptGroupindMaster_CStr2ToolTip")))


Response.Write("</TD>" & vbCrLf)
Response.Write("		")

		
	End If
	
Response.Write("" & vbCrLf)
Response.Write("            <TD> &nbsp; </TD>" & vbCrLf)
Response.Write("		")

	
	If Request.QueryString.Item("sComponent") = "2" And Request.QueryString.Item("sPoliType") = "3" Then
		
Response.Write("" & vbCrLf)
Response.Write("			<TD WIDTH=""26%"">")


Response.Write(mobjValues.OptionControl(41371, "OptMultindMaster", GetLocalResourceObject("OptMultindMaster_CStr2Caption"), CStr(1), CStr(2), "insEnabledPolicyType(this.checked, 3, 2);", lclsProduct.sMultiind <> "1" Or Request.QueryString.Item("sPoliType") = "1",  , GetLocalResourceObject("OptMultindMaster_CStr2ToolTip")))


Response.Write("</TD>" & vbCrLf)
Response.Write("		")

		
	Else
		
Response.Write("" & vbCrLf)
Response.Write("			<TD WIDTH=""26%"">")


Response.Write(mobjValues.OptionControl(41371, "OptMultindMaster", GetLocalResourceObject("OptMultindMaster_CStr2Caption"),  , CStr(2), "insEnabledPolicyType(this.checked, 3, 2);", lclsProduct.sMultiind <> "1" Or Request.QueryString.Item("sPoliType") = "1",  , GetLocalResourceObject("OptMultindMaster_CStr2ToolTip")))


Response.Write("</TD>" & vbCrLf)
Response.Write("		")

		
	End If
	
Response.Write("" & vbCrLf)
Response.Write("		</TR>" & vbCrLf)
Response.Write("		" & vbCrLf)
Response.Write("	</TABLE>")

	
	lblnValidate = False
	
	lclsProduct = New eProduct.Product
	
	Call lclsProductWin.insReaProd_win(mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate))
	
	'+ Validación 11386: Para poder mostrar información en esta ventana, debe existir información
	'+					 grabada en la ventana "respuestas automáticas en la emisión" - ACM - 14/06/2001
	
	Call lclsProductWin.Find_Item("DP003", True)
	
	If Request.QueryString.Item("sPoliType") <> vbNullString Then
		If Not lclsProductWin.sContent = "2" Then
			lclsErrors = New eFunctions.Errors
			Response.Write(lclsErrors.ErrorMessage("DP042", 11349,  ,  ,  , True))
			lclsErrors = Nothing
			lblnValidate = False
		Else
			lblnValidate = True
		End If
	End If
	
	'+ Validación 11349: Para poder mostrar información en esta ventana, debe existir información
	'+					 grabada de los tipos de pólizas permitidas para el producto en la ventana de
	'+					 "información general del producto" - ACM - 14/06/2001
	
	Call lclsProductWin.Find_Item("DP005", True)
	
	If Request.QueryString.Item("sPoliType") <> vbNullString Then
		If Not lclsProductWin.sContent = "2" Then
			lclsErrors = New eFunctions.Errors
			Response.Write(lclsErrors.ErrorMessage("DP042", 11386,  ,  ,  , True))
			lclsErrors = Nothing
			lblnValidate = False
		Else
			lblnValidate = True
		End If
	End If
	
	If lblnValidate = True Then
		lstrCompon = "1"
		If Request.QueryString.Item("sComponent") <> vbNullString Then
			lstrCompon = Request.QueryString.Item("sComponent")
		End If
		Call lcolCliallopro.Find(mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), Request.QueryString.Item("sPoliType"), lstrCompon)
		
		For mintCount = 1 To lcolCliallopro.Count
			lintDefault = 0
			lintRequired = 0
			
			'+ Se asignan a las variables lintRequired y lintDefault los valores de lclsProduct.sRequired y
			'+ lclsProduct.sDefaulti respectivamente - ACM - 16/04/2001					
			lintRequired = mobjValues.StringToType(lcolCliallopro.Item(mintCount).sRequire, eFunctions.Values.eTypeData.etdDouble)
			lintDefault = mobjValues.StringToType(lcolCliallopro.Item(mintCount).sDefaulti, eFunctions.Values.eTypeData.etdDouble)
			
			With mobjGrid
				.Columns("Sel").Checked = CShort(lcolCliallopro.Item(mintCount).sSel)
				If lcolCliallopro.Item(mintCount).sRequire = "1" Then
					.Columns("chkObligatoria").Checked = 1
				Else
					.Columns("chkObligatoria").Checked = 2
				End If
				
				If lcolCliallopro.Item(mintCount).sDefaulti = "1" Then
					.Columns("chkDefault").Checked = 1
				Else
					.Columns("chkDefault").Checked = 2
				End If
				
				If lcolCliallopro.Item(mintCount).sOptionalQuo = "1" Then
					.Columns("chkOptionalQuo").Checked = 1
				Else
					.Columns("chkOptionalQuo").Checked = 2
				End If
				
				Select Case Request.QueryString.Item("sPoliType")
					Case "1"
						
						.Columns("tcnIndividual").DefValue = CStr(1)
						.Columns("tcnColectiva").DefValue = CStr(2)
						.Columns("tcnMultilocalidad").DefValue = CStr(2)
					Case "2"
						
						.Columns("tcnIndividual").DefValue = CStr(2)
						.Columns("tcnColectiva").DefValue = CStr(1)
						.Columns("tcnMultilocalidad").DefValue = CStr(2)
					Case "3"
						
						.Columns("tcnIndividual").DefValue = CStr(2)
						.Columns("tcnColectiva").DefValue = CStr(2)
						.Columns("tcnMultilocalidad").DefValue = CStr(1)
				End Select
				
				.Columns("cboClientRol").DefValue = CStr(lcolCliallopro.Item(mintCount).nCodigint)
				.Columns("tcnMaxValue").DefValue = CStr(lcolCliallopro.Item(mintCount).nMax_role)
				.Columns("tctPoliType").DefValue = Request.QueryString.Item("sPoliType")
				.Columns("sRequired").DefValue = lcolCliallopro.Item(mintCount).sRequire
				.Columns("sDefault").DefValue = lcolCliallopro.Item(mintCount).sDefaulti
				.Columns("tcnChecked").DefValue = CStr(0)
				.Columns("sOptionalQuo").DefValue = lcolCliallopro.Item(mintCount).sOptionalQuo
				.Columns("tcnIndividual").DefValue = CStr(2)
				.Columns("tcnColectiva").DefValue = CStr(2)
				.Columns("tcnMultilocalidad").DefValue = CStr(2)
				.Columns("tcnCounter").DefValue = CStr(mintCount - 1)
				.Columns("tctCompon").DefValue = Request.QueryString.Item("sComponent")
				.Columns("tctInitialSelection").DefValue = CStr(lcolCliallopro.Item(mintCount).nSelected)
				.Columns("tcnBranch").DefValue = Session("nBranch")
				.Columns("tcnProduct").DefValue = Session("nProduct")
				.Columns("tcdEffecdate").DefValue = Session("dEffecdate")
				.Columns("tcnUserCode").DefValue = Session("nUsercode")
				.Columns("tcnClientRol").DefValue = CStr(lcolCliallopro.Item(mintCount).nCodigint)
				
				If Request.QueryString.Item("sPoliType") = vbNullString Then
					.Columns("tctPolicyType").DefValue = "1"
				Else
					.Columns("tctPolicyType").DefValue = Request.QueryString.Item("sPoliType")
				End If
				
				If Request.QueryString.Item("sComponent") = vbNullString Then
					.Columns("tctComponent").DefValue = "1"
				Else
					.Columns("tctComponent").DefValue = Request.QueryString.Item("sComponent")
				End If
				
				.Columns("tctAction").DefValue = "Update"
				.Columns("tcnAction").DefValue = CStr(302)
				'+ Se asigna el valor por defecto para la póliza individual al cliente que es
				'+ el titular del recibo.						
				If lstrHolder <> vbNullString And mobjValues.StringToType(lstrHolder, eFunctions.Values.eTypeData.etdDouble) = lcolCliallopro.Item(mintCount).nCodigint Then
					Select Case Request.QueryString.Item("sPoliType")
						Case "1"
							.Columns("Sel").Checked = 1
							.Columns("Sel").Disabled = True
							.Columns("chkObligatoria").Disabled = True
							.Columns("chkDefault").Disabled = True
							.Columns("chkObligatoria").Checked = lintRequired
							.Columns("chkDefault").Checked = lintDefault
                                .Columns("hddHolder").DefValue = CStr(1)
                            Case "2"
                                If (Request.QueryString.Item("sComponent") = "1" And _
                                   (lstrHolder = "1" Or lstrHolder = "25")) Or _
                                   (Request.QueryString.Item("sComponent") = "2" And _
                                   lstrHolder = "2") Then
                                    .Columns("Sel").Checked = 1
                                    .Columns("Sel").Disabled = True
                                    .Columns("chkObligatoria").Disabled = True
                                    .Columns("chkDefault").Disabled = True
                                    .Columns("chkObligatoria").Checked = lintRequired
                                    .Columns("chkDefault").Checked = lintDefault
                                    .Columns("hddHolder").DefValue = CStr(1)
                                End If
                        End Select
					
					If lcolCliallopro.Item(mintCount).nMax_role = 0 And .Columns("Sel").Checked = 1 Then
						.Columns("tcnMaxValue").DefValue = CStr(1)
					Else
						.Columns("tcnMaxValue").DefValue = CStr(lcolCliallopro.Item(mintCount).nMax_role)
					End If
					
					.Columns("chkObligatoria").Disabled = True
					.Columns("chkDefault").Disabled = True
				Else
					.Columns("Sel").Disabled = False
					.Columns("hddHolder").DefValue = CStr(2)
				End If
				
				'+ Se asigna cero (0) a tcnMaxValue2 para que en caso de que el registro no exista 
				'+ físicamente en la tabla sea agregado - ACM - 20/06/2001
				If lcolCliallopro.Item(mintCount).nMax_role > 0 Then
					.Columns("tcnMaxValue2").DefValue = CStr(lcolCliallopro.Item(mintCount).nMax_role)
				Else
					.Columns("tcnMaxValue2").DefValue = CStr(0)
				End If
				
				.Columns("sParam").DefValue = "&nBranch=" & Session("nBranch") & "&nProduct=" & Session("nProduct") & "&dEffecdate=" & Session("dEffecdate") & "&sCompon=" & Request.QueryString.Item("sComponent") & "&sRequired=" & lintRequired & "&sDefault=" & lintDefault & "&nMaxvalue=" & lcolCliallopro.Item(mintCount).nMax_role & "&nUserCode=" & Session("nUsercode") & "&nClientRol=" & lcolCliallopro.Item(mintCount).nCodigint & "&nChecked=0"
			End With
			
			Response.Write(mobjGrid.DoRow())
		Next 
	End If
	
	Response.Write(mobjGrid.CloseTable())
End Sub

'----------------------------------------------------------------------------------------------
Private Sub insPreDP042Upd()
	Dim i As Double
	'----------------------------------------------------------------------------------------------
	Dim lclsPost As eProduct.Product
	Dim lintAction As Object
	Dim lblnPost As Boolean
	Dim lstrAction As Object
	
	Select Case Request.QueryString.Item("Action")
		Case "Del", "Delete"
			lintAction = 302
			'+ Muestra el mensaje para eliminar registros
			
			Response.Write(mobjValues.ConfirmDelete())
			
			lclsPost = New eProduct.Product
			
			With Request
				If .QueryString.Item("nChecked") = "2" And (.QueryString.Item("nChecked") <> vbNullString Or .QueryString.Item("nChecked") <> "0") Then
					lblnPost = lclsPost.insPostDP042(mobjValues.StringToType(lintAction, eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), .QueryString.Item("sPoliType"), .QueryString.Item("sComponent"), mobjValues.StringToType(.QueryString.Item("nClientRol"), eFunctions.Values.eTypeData.etdDouble), vbNullString, vbNullString, mobjValues.StringToType(.QueryString.Item("nMaxValue"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nUsercode"), eFunctions.Values.eTypeData.etdDouble), .QueryString.Item("nChecked"), "2", 1, "2")
				End If
				
				With Request
					Response.Write(mobjGrid.DoFormUpd(.QueryString.Item("Action"), "valProductSeq.aspx", "DP042", lintAction, mobjValues.actionQuery, CShort(.QueryString.Item("Index"))))
				End With
			End With
			
			lclsPost = Nothing
			
		Case "Add", "Update"
			Response.Write(mobjGrid.DoFormUpd(Request.QueryString.Item("Action"), "valProductSeq.aspx", "DP042", Request.QueryString.Item("nMainAction"), mobjValues.actionQuery, CShort(Request.QueryString.Item("Index"))))
			
			
Response.Write("" & vbCrLf)
Response.Write("<SCRIPT>" & vbCrLf)
Response.Write("			" & vbCrLf)
Response.Write("//+ Debido a que el QueryString de la ventana PopUp está errado al tratarse de pólizas" & vbCrLf)
Response.Write("//+ distintas a INDIVIDUAL, se reconstruye el QueryString de la ventana PopUp, asignándole" & vbCrLf)
Response.Write("//+ correctamente los valores del tipo de Póliza (sPoliType) y del subtipo de Pólizas" & vbCrLf)
Response.Write("//+ (sComponent) - ACM - 13/06/2001" & vbCrLf)
Response.Write("" & vbCrLf)
Response.Write("	var lstrLocation="""";" & vbCrLf)
Response.Write("	var lstrPoliType="""";" & vbCrLf)
Response.Write("	var lstrComponent="""";" & vbCrLf)
Response.Write("" & vbCrLf)
Response.Write("	if(top.opener.document.forms[0].elements[""tctPolicyType""][CurrentIndex].value != '")


Response.Write(Request.QueryString.Item("sPoliType"))


Response.Write("' ||" & vbCrLf)
Response.Write("	   top.opener.document.forms[0].elements[""tctComponent""][CurrentIndex].value != self.document.forms[0].elements[""tctComponent""].value)" & vbCrLf)
Response.Write("		{" & vbCrLf)
Response.Write("			lstrPoliType = top.opener.document.forms[0].elements[""tctPolicyType""][CurrentIndex].value;" & vbCrLf)
Response.Write("			lstrComponent = top.opener.document.forms[0].elements[""tctComponent""][CurrentIndex].value;" & vbCrLf)
Response.Write("			lstrLocation += self.document.location;" & vbCrLf)
Response.Write("			lstrLocation = lstrLocation.replace(/&sPoliType.*/, """");" & vbCrLf)
Response.Write("			lstrLocation = lstrLocation.replace(/&sComponent.*/, """");" & vbCrLf)
Response.Write("			lstrLocation = lstrLocation + ""&sPoliType="" + lstrPoliType;" & vbCrLf)
Response.Write("			lstrLocation = lstrLocation + ""&sComponent="" + lstrComponent;" & vbCrLf)
Response.Write("  			self.document.location = lstrLocation;" & vbCrLf)
Response.Write("		}" & vbCrLf)
Response.Write("  " & vbCrLf)
Response.Write("	if(self.document.forms[0].elements[""chkObligatoria""].disabled=true)" & vbCrLf)
Response.Write("		self.document.forms[0].elements[""chkObligatoria""].disabled=false;" & vbCrLf)
Response.Write("  " & vbCrLf)
Response.Write("	if(self.document.forms[0].elements[""chkDefault""].disabled=true)" & vbCrLf)
Response.Write("	    self.document.forms[0].elements[""chkDefault""].disabled=false;" & vbCrLf)
Response.Write("	" & vbCrLf)
Response.Write("	if(self.document.forms[0].elements[""chkOptionalQuo""].disabled=true)" & vbCrLf)
Response.Write("	    self.document.forms[0].elements[""chkOptionalQuo""].disabled=false;" & vbCrLf)
Response.Write("	    	" & vbCrLf)
Response.Write("	if(self.document.forms[0].elements[""tcnMaxValue""].value <= 0 ||" & vbCrLf)
Response.Write("	   top.opener.document.forms[0].elements[""tcnMaxValue2""][CurrentIndex].value <= 0)" & vbCrLf)
Response.Write("	    self.document.forms[0].elements[""tcnMaxValue""].value = 1;" & vbCrLf)
Response.Write("	    " & vbCrLf)
Response.Write("	if (self.document.forms[0].elements[""hddHolder""].value == 1){  " & vbCrLf)
Response.Write("		self.document.forms[0].elements[""chkObligatoria""].disabled=true;" & vbCrLf)
Response.Write("		self.document.forms[0].elements[""chkDefault""].disabled=true;" & vbCrLf)
Response.Write("	}	" & vbCrLf)
Response.Write("				    " & vbCrLf)
Response.Write("</" & "SCRIPT>")

			
			'+ Cuando se recarga la POPUP, en ese instante no se ha recargado la ventana MADRE, por lo cual
			'+ se produce un error ya que la POPUP hace referencia a objetos y campos que no se han recargado,
			'+ por esta razón es que se construyó este bloque, para retardar la recarga de la POPUP y dar tiempo
			'+ a que se refresque la ventana madre - ACM - 29/06/2001
			i = 0
			While i < 10000
				i = i + 1
			End While
			
			i = Nothing
	End Select
End Sub

</script>
<%
Response.Expires = -1

mobjValues = New eFunctions.Values
mobjGrid = New eFunctions.Grid

mobjGrid.sCodisplPage = "DP042"
mobjValues.sCodisplPage = "DP042"

mobjGrid.actionQuery = Session("bQuery")
%>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT>

//- Variable para el control de versiones
    document.VssVersion="$$Revision: 2 $|$$Date: 15/10/03 17:21 $|$$Author: Nvaplat61 $"

//% insRequiredValue: Llama a la función insDefaultValue cuando el campo 'obligatorio' cambia
//-------------------------------------------------------------------------------------------
function insRequiredValue(Field){
//-------------------------------------------------------------------------------------------
	with (self.document.forms[0]){
		sRequired.value = (Field.checked?1:2)
		if(Field.checked){
			chkDefault.checked = true;
			insDefaultValue(chkDefault);
		}
	}
}
//% insRequiredValue: Llama a la función insDefaultValue cuando el campo 'obligatorio' cambia
//-------------------------------------------------------------------------------------------
function insOptional(Field){
//-------------------------------------------------------------------------------------------
	with (self.document.forms[0]){
		sOptionalQuo.value = (Field.checked?1:2);
	}
}
//% insDefaultValue: Cambia el estado del check 'Defecto' cuando es de tipo 'obligatorio'
//-------------------------------------------------------------------------------------------
function insDefaultValue(Field){
//-------------------------------------------------------------------------------------------
	document.forms[0].elements["sDefault"].value = (Field.checked?1:2);
}

//% Verifica si está seleccionado el registro
//-------------------------------------------------------------------------------------------
function insSelected(Field){
//-------------------------------------------------------------------------------------------
	var lstrLocation="";
	
    with (document.forms[0])
    {
		if (elements["tcnChecked"].length==-1)
		{
		    elements["tcnChecked"].value=(Field.checked?1:2)
		}
		else
		{
		    elements["tcnChecked"][Field.value].value=(Field.checked?1:2)
		}
		
		if(self.document.forms[0].elements["tcnChecked"][Field.value].value==1)
		{
			self.document.forms[0].elements["tctAction"][Field.value].value = "Add";
			EditRecord(Field.value,nMainAction);
			Field.checked = false;
			return(0);
		}
		
		if(self.document.forms[0].elements["tcnChecked"][Field.value].checked==false)
		{
		    elements["chkObligatoria"][Field.value].checked=false;
		    elements["chkDefault"][Field.value].checked=false;
		    lstrLocation += document.location.href;
		    lstrLocation = lstrLocation + self.document.forms[0].elements["sParam"][Field.value].value;
			lstrLocation = lstrLocation.replace(/&nChecked.*/, "");
			lstrLocation = lstrLocation + "&nChecked=2";
			EditRecord(Field.value,302, 'Del', lstrLocation);
			return(0);
		}
	}
}		
//% RechargePage: Recarga la página
//-------------------------------------------------------------------------------------------
function RechargePage(Valor){
//-------------------------------------------------------------------------------------------
	var lstrLocation="";
	lstrLocation += document.location.href
	lstrLocation = lstrLocation.replace(/&sPoliType.*/, "")
	lstrLocation = lstrLocation + "&sPoliType=" + Valor
	document.location.href = lstrLocation
}

//% Valida que el valor del campo 'número máximo' sea mayor a 0
//-------------------------------------------------------------------------------------------
function insErrorMessage(Field){
//-------------------------------------------------------------------------------------------
	if(Field<=0 || isNaN(Field))
	{
		alert("55868: " + "El valor de este campo debe ser mayor a cero (0)");
		self.document.forms[0].elements["tcnMaxValue"].value = "1";
	}
}
</SCRIPT>
<SCRIPT>
//% insEnabledPolicyType: Ingresa los valores de los check de los campos tipos de póliza
//-------------------------------------------------------------------------------------------
function insEnabledPolicyType(FieldChecked, FieldValue, nComponent){
//-------------------------------------------------------------------------------------------
	var lstrLocation="";
	var lintComponent=0;
	
//+ El parámetro FieldChecked indica si el RadioButton está o no seleccionado
//+ El parámetro FieldValue indica el valor del RadioButton seleccionado
	
	if(nComponent=="" || isNaN(nComponent))
		lintComponent = 1
	else
		lintComponent = nComponent
	
	if(typeof(FieldChecked=='undefined'))
		FieldChecked = true;
	
	if(FieldChecked)
	{
		switch(FieldValue)
		{
		
//+ FieldValue = 1: Pólizas Individuales

			case 1:
			{
				document.forms[0].elements["OptMultindMaster"][0].checked = false;
				document.forms[0].elements["OptMultindMaster"][1].checked = false;

				document.forms[0].elements["OptGroupindMaster"][0].checked = false;
				document.forms[0].elements["OptGroupindMaster"][1].checked = false;
				break;
			}

//+ FieldValue = 2: Pólizas Colectivas

			case 2:
			{
				switch(lintComponent)
				{

//+ Póliza matriz de un colectivo

					case 1:
					{
						document.forms[0].elements["OptGroupindMaster"][0].disabled = false;
						document.forms[0].elements["OptGroupindMaster"][1].disabled = false;
						document.forms[0].elements["OptGroupindMaster"][0].checked = true;
						break;
					}

//+ Certificados de un colectivo

					case 2:
					{
						document.forms[0].elements["OptGroupindMaster"][0].disabled = false;
						document.forms[0].elements["OptGroupindMaster"][1].disabled = false;
						document.forms[0].elements["OptGroupindMaster"][1].checked = true;
						break;
					}

				}
				
				document.forms[0].elements["OptMultindMaster"][0].disabled = true;
				document.forms[0].elements["OptMultindMaster"][1].disabled = true;
				document.forms[0].elements["OptMultindMaster"][0].checked = false;
				document.forms[0].elements["OptMultindMaster"][1].checked = false;
				break;
			}

//+ FieldValue = 3: Pólizas Multilocalidad

			case 3:
			{
				
				switch(lintComponent)
				{

//+ Póliza matriz de Multilocalidad

					case 1:
					{
						document.forms[0].elements["OptMultindMaster"][0].disabled = false;
						document.forms[0].elements["OptMultindMaster"][1].disabled = false;
						document.forms[0].elements["OptMultindMaster"][0].checked = true;
						break;
					}

//+ Certificados de Multilocalidad

					case 2:
					{
						document.forms[0].elements["OptMultindMaster"][0].disabled = false;
						document.forms[0].elements["OptMultindMaster"][1].disabled = false;
						document.forms[0].elements["OptMultindMaster"][1].checked = true;
						break;
					}
				}

				document.forms[0].elements["OptGroupindMaster"][0].disabled = true;
				document.forms[0].elements["OptGroupindMaster"][1].disabled = true;
				document.forms[0].elements["OptGroupindMaster"][0].checked = false;
				document.forms[0].elements["OptGroupindMaster"][1].checked = false;
				break;
			}
		}
	}
	if(document.forms[0].elements["OptPolicyType"][2].disabled)
	{
		document.forms[0].elements["OptPolicyType"][0].disabled = false
		document.forms[0].elements["OptPolicyType"][1].disabled = false
		document.forms[0].elements["OptPolicyType"][2].disabled = true
	}
	else
	{
		document.forms[0].elements["OptPolicyType"][0].disabled = false
		document.forms[0].elements["OptPolicyType"][1].disabled = true
		document.forms[0].elements["OptPolicyType"][2].disabled = false
	}

//+ Se sustiyuye el valor del parámetro sPoliType por el valor que tenga el RadioButton
//+ seleccionado 

	lstrLocation += document.location.href;
	lstrLocation = lstrLocation.replace(/&sPoliType.*/, "");
	lstrLocation = lstrLocation.replace(/&sComponent.*/, "");
//+ Se sustituye el valor del parámetro RELOAD por blanco ("") para que la ventana POPUP
//+ no sea recargada una vez que se accione cualquier Radio Button 
	lstrLocation = lstrLocation.replace(/&Reload.*/, "");
	lstrLocation = lstrLocation + "&sPoliType=" + FieldValue;
	lstrLocation = lstrLocation + "&sComponent=" + lintComponent;
	document.location.href = lstrLocation;	
}

//% insIncreaseValue: Inclementa el valor del campo tcnMaxValue
//-------------------------------------------------------------------------------------------
function insIncreaseValue(FieldValue){
//-------------------------------------------------------------------------------------------
	var lintValue=0;
	
	if(FieldValue==0 || isNaN(FieldValue))
	{
		lintValue = lintValue + 1;
		self.document.forms[0].elements["tcnMaxValue"].value = lintValue;
	}
}
</SCRIPT>

<HTML>
<HEAD>


<%
mobjMenu = New eFunctions.Menues

With Response
	.Write(mobjValues.StyleSheet())
	.Write(mobjValues.WindowsTitle("DP042"))
	
	If Request.QueryString.Item("Type") <> "PopUp" Then
		.Write(mobjMenu.setZone(2, "DP042", "DP042.aspx"))
		.Write("<SCRIPT>var nMainAction=304;</SCRIPT>")
	End If
End With

mobjMenu = Nothing

'+ Se invoca a la función RechargePage si y sólo si el parámetro "sPoliType" del QueryString
'+ es igual a blanco o no existe. El valor que se pasa entre los paréntesis corresponde a 
'+ Pólizas Individuales (sPolitype = 1) - ACM - 16/04/2001

If Request.QueryString.Item("sPoliType") = vbNullString And Request.QueryString.Item("sComponent") = vbNullString Then
	Response.Write("<SCRIPT>RechargePage(1)</SCRIPT>")
End If
%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="post" ID="FORM" NAME="frmDP042" ACTION="valProductSeq.aspx?nMainAction=<%=Request.QueryString.Item("nMainAction")%>">
<%
Response.Write(mobjValues.ShowWindowsName(Request.QueryString.Item("sCodispl")))
Call insDefineHeaderDP042()

If Request.QueryString.Item("Type") <> "PopUp" Then
	Call insPreDP042()
Else
	Call insPreDP042Upd()
End If
%>
<%lclsProduct = New eProduct.Product

Call lclsProduct.Find(Session("nBranch"), Session("nProduct"), Session("dEffecdate"))


Select Case lclsProduct.sPoliType
	
	Case "1"
		
		If Request.QueryString.Item("sComponent") = vbNullString And Request.QueryString.Item("Type") <> "PopUp" Then
			Response.Write("<SCRIPT>insEnabledPolicyType(true, 1, """");</SCRIPT>")
		End If
		
	Case "2"
		If Request.QueryString.Item("sComponent") = vbNullString And Request.QueryString.Item("Type") <> "PopUp" Then
			Response.Write("<SCRIPT>insEnabledPolicyType(true, 2, """");</SCRIPT>")
		End If
	Case Else
		If Request.QueryString.Item("sComponent") = vbNullString And Request.QueryString.Item("Type") <> "PopUp" Then
			Response.Write("<SCRIPT>insEnabledPolicyType(true, 1, """");</SCRIPT>")
		End If
		
End Select

%>
</FORM>
</BODY>
</HTML>




