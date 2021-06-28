<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eProduct" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values
Dim mobjGen_cover As eProduct.Gen_cover
Dim mobjPrecov_apl As eProduct.Precov_apl
Dim mobjTab_modul As eProduct.Tab_modul
Dim mobjMenu As eFunctions.Menues
Dim lblnDisabled As Boolean
Dim lintCover_in As Object
Dim lsRoupremi As String
Dim lintPremifix As Object
Dim lintPremirat As Object
Dim lintCoverapl As Object


'% insPreDP035: Ejecuta las rutinas necesarias para cargar información en los controles
'--------------------------------------------------------------------------------------
Private Sub insPreDP035()
	'--------------------------------------------------------------------------------------
	Dim lintCount As Integer
	Dim lobjGrid As eFunctions.Grid
	
	lobjGrid = New eFunctions.Grid
	
	lobjGrid.sCodisplPage = "dp035"
	
	With lobjGrid
		With .Columns
			.AddTextColumn(100124, GetLocalResourceObject("tcnCapitalColumnCaption"), "tcnCapital", 35, vbNullString)
			.AddHiddenColumn("tcnCapitalCode", vbNullString)
			.AddHiddenColumn("tcnSelected", CStr(0))
		End With
		.ActionQuery = Session("bQuery")
		.Codisp = "DP035"
		.Codispl = "DP035"
		.DeleteButton = False
		.AddButton = False
		.Columns("Sel").OnClick = "insSelected(this,1);"
	End With
	
	'+ Cargando la información del grid de capitales básicos asociados a un producto
	With lobjGrid
		For lintCount = 1 To mobjPrecov_apl.CountRate
			If mobjPrecov_apl.ItemRate(lintCount) Then
				.Columns("tcnCapitalCode").DefValue = CStr(mobjPrecov_apl.nSumins_co)
				.Columns("tcnCapital").DefValue = mobjPrecov_apl.sDescript
				.Columns("Sel").Checked = CShort(mobjPrecov_apl.sSel)
				.Columns("tcnSelected").DefValue = mobjPrecov_apl.sSel
			End If
			Response.Write(.DoRow)
		Next 
		Response.Write(.closeTable)
	End With
	
End Sub

'% insPreDP035Tarif: Ejecuta las rutinas necesarias para cargar información de tarifas
'--------------------------------------------------------------------------------------
Private Sub insPreDP035Tarif()
	'--------------------------------------------------------------------------------------
	Dim lintCount As Integer
	Dim lobjGridTarif As eFunctions.Grid
	
	lobjGridTarif = New eFunctions.Grid
	
	'+Se define cabecera de grid
	With lobjGridTarif
		With .Columns
			.AddTextColumn(100124, GetLocalResourceObject("tcnTarifCapitalColumnCaption"), "tcnTarifCapital", 35, vbNullString)
			.AddHiddenColumn("tcnTarifCapitalCode", vbNullString)
			.AddHiddenColumn("tcnTarifSel", CStr(0))
		End With
		.ActionQuery = Session("bQuery")
		.Codisp = "DP035"
		.Codispl = "DP035"
		.DeleteButton = False
		.AddButton = False
		.Columns("Sel").OnClick = "insSelected(this, 2);"
	End With
	
	'+Cargando la información del grid de capitales básicos asociados a un producto
	With lobjGridTarif
		For lintCount = 1 To mobjPrecov_apl.CountTarif
			If mobjPrecov_apl.ItemTarif(lintCount) Then
				.Columns("tcnTarifCapitalCode").DefValue = CStr(mobjPrecov_apl.nSumins_co)
				.Columns("tcnTarifCapital").DefValue = mobjPrecov_apl.sDescript
				.Columns("Sel").Checked = CShort(mobjPrecov_apl.sSel)
				.Columns("tcnTarifSel").DefValue = mobjPrecov_apl.sSel
			End If
			Response.Write(.DoRow)
		Next 
		Response.Write(.closeTable)
	End With
End Sub

</script>
<%Response.Expires = -1

mobjValues = New eFunctions.Values
mobjGen_cover = New eProduct.Gen_cover
mobjPrecov_apl = New eProduct.Precov_apl
mobjTab_modul = New eProduct.Tab_modul

lblnDisabled = False

If mobjValues.StringToType(Session("nModulec"), eFunctions.Values.eTypeData.etdDouble, True) > 0 Then
	Call mobjTab_modul.Find(Session("nBranch"), Session("nProduct"), Session("nModulec"), Session("dEffecdate"))
	
	If mobjTab_modul.styp_rat = "1" Then
		lblnDisabled = True
	End If
End If

mobjTab_modul = Nothing

mobjValues.ActionQuery = Session("bQuery")

Call mobjPrecov_apl.insPreDP035(Session("nBranch"), Session("nProduct"), Session("dEffecdate"), Session("nModulec"), Session("nCover"), Session("nUsercode"))


Call mobjGen_cover.Find(Session("nBranch"), Session("nProduct"), Session("nModulec"), Session("nCover"), Session("dEffecdate"))

If mobjGen_cover.nCoverapl <> 0 And mobjGen_cover.nCoverapl <> eRemoteDB.Constants.intNull Then
	mobjPrecov_apl.nOwnCapital = 2
End If

If Not lblnDisabled Then
	lintCover_in = mobjGen_cover.nCover_in
	lsRoupremi = mobjGen_cover.sRoupremi
	lintPremifix = mobjGen_cover.nPremifix
	lintPremirat = mobjGen_cover.nPremirat
	lintCoverapl = mobjGen_cover.nCoverapl
End If

mobjValues.sCodisplPage = "dp035"
%>
<HTML>
<HEAD>
    <META NAME = "GENERATOR" Content = "Microsoft Visual Studio 6.0">
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>


<%
mobjMenu = New eFunctions.Menues
With Response
	.Write(mobjValues.StyleSheet())
	.Write(mobjValues.WindowsTitle("DP035"))
	.Write(mobjMenu.setZone(2, "DP035", "DP035.aspx"))
End With
mobjMenu = Nothing
%>

<SCRIPT>
//+ Variable para el control de versiones
       document.VssVersion="$$Revision: 3 $|$$Date: 27/04/04 9:49 $"
       
//% insDisabledField: Desabilita o habilita campos según condición
//---------------------------------------
function insDisabledField(Field, nValue){
//---------------------------------------
	switch(nValue)
	{
		case 1:
		{
			if(Field.checked)
				self.document.forms[0].elements["tcnRatepreadd"].disabled = false
			else
				self.document.forms[0].elements["tcnRatepreadd"].disabled = true;
				self.document.forms[0].elements["tcnRatepreadd"].value = 0;
			break;
		}
		case 2:
		{
			if(Field.checked)
				self.document.forms[0].elements["tcnRatepresub"].disabled = false
			else
				self.document.forms[0].elements["tcnRatepresub"].disabled = true;
				self.document.forms[0].elements["tcnRatepresub"].value = 0;
			break;
		}
	}
}

//% insSelected: Desabilita o habilita campos ocultos de grilla
//--------------------------
function insSelected(Field, nSection){
//--------------------------
    if(nSection==1)
	    with (self.document.forms[0]){
	    	if (typeof(tcnSelected.length) == 'undefined')
	    		tcnSelected.value = (Field.checked?1:0)
	    	else
	    		tcnSelected[Field.value].value = (Field.checked?1:0)
	    }
	else
	    with (self.document.forms[0]){
	    	if (typeof(tcnTarifSel.length) == 'undefined')
	    		tcnTarifSel.value = (Field.checked?1:0)
	    	else
	    		tcnTarifSel[Field.value].value = (Field.checked?1:0)
	    }	
}
</SCRIPT>
</HEAD>

<BODY ONUNLOAD="closeWindows();">
	<%=mobjValues.ShowWindowsName(Request.QueryString.Item("sCodispl"))%>
	<FORM METHOD="post" ID="FORM" NAME="DP035" ACTION="valCoverSeq.aspx?sZone=2">
	    <BR>
	    <A NAME="BeginPage"></A>
	    <P ALIGN="Center">
			<LABEL ID=100118><A HREF="#Tasa aplica sobre"> <%= GetLocalResourceObject("AnchorTasa aplica sobreCaption") %></A></LABEL><LABEL ID=0> | </LABEL>
			<LABEL ID=100119><A HREF="#Condiciones"> <%= GetLocalResourceObject("AnchorCondicionesCaption") %></A></LABEL><LABEL ID=0> | </LABEL>
			<LABEL ID=100120><A HREF="#Cambios"> <%= GetLocalResourceObject("AnchorCambiosCaption") %></A></LABEL>
			<LABEL ID=0><A HREF="#Tarifa"><%= GetLocalResourceObject("AnchorTarifaCaption") %></A></LABEL><LABEL ID=0> | </LABEL>
	    </P>

	    <TABLE WIDTH="100%">
	        <TR>
	            <TD WIDTH="15%"><LABEL ID=14467><%= GetLocalResourceObject("valCover_inCaption") %></LABEL></TD>
<%
With mobjValues
	.Parameters.Add("nBranch", .StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	.Parameters.Add("nProduct", .StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	.Parameters.Add("nCover", .StringToType(Session("nCover"), eFunctions.Values.eTypeData.etdDouble), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	.Parameters.Add("dEffecdate", .StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	.Parameters.Add("nModulec", .StringToType(Session("nModulec"), eFunctions.Values.eTypeData.etdDouble), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
End With
%>
				<TD WIDTH="15%"><%=mobjValues.PossiblesValues("valCover_in", "TabGen_cover2", eFunctions.Values.eValuesType.clngWindowType, lintCover_in, True,  ,  ,  ,  ,  , lblnDisabled)%></TD>
	            <TD WIDTH="15%"><LABEL ID=14473><%= GetLocalResourceObject("tctRoupremiCaption") %></LABEL></TD>
	            <TD WIDTH="15%"><%=mobjValues.TextControl("tctRoupremi", 15, lsRoupremi,  , GetLocalResourceObject("tctRoupremiToolTip"),  ,  ,  ,  , lblnDisabled)%></TD>
			</TR>
			<TR>
	            <TD WIDTH="15%"><LABEL ID=14470><%= GetLocalResourceObject("tcnPremiFixCaption") %></LABEL></TD>
	            <TD WIDTH="15%"><%=mobjValues.NumericControl("tcnPremiFix", 18, lintPremifix,  , GetLocalResourceObject("tcnPremiFixToolTip"), True, 6, 0,  ,  ,  , lblnDisabled)%></TD>
	            <TD WIDTH="15%"><LABEL ID=14476><%= GetLocalResourceObject("tcnPremiratCaption") %></LABEL></TD>
	            <TD WIDTH="15%"><%=mobjValues.NumericControl("tcnPremirat", 9, lintPremirat,  , GetLocalResourceObject("tcnPremiratToolTip"), True, 6,  ,  ,  ,  , lblnDisabled)%></TD>
			</TR>
			<TR>    
				<TD WIDTH="15%"><LABEL ID="0"><%= GetLocalResourceObject("valid_tableCaption") %></LABEL></TD>
				<TD COLSPAN="2"><%=mobjValues.PossiblesValues("valid_table", "table5800", eFunctions.Values.eValuesType.clngWindowType, CStr(mobjPrecov_apl.nId_table),  ,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("valid_tableToolTip"))%></TD>
			</TR>
		</TABLE>

		<TABLE WIDTH="100%">
			<TR>
				 <TD COLSPAN="2" CLASS="HighLighted"><LABEL ID=100121><A NAME="Tasa aplica sobre"><%= GetLocalResourceObject("AnchorTasa aplica sobre2Caption") %></A></LABEL></TD>
			</TR>	
			<TR>
				<TD COLSPAN="2" CLASS="HorLine"></TD>
			</TR>
			<TR>
	            <TD WIDTH="35%"><%=mobjValues.CheckControl("chkOwnCapital", GetLocalResourceObject("chkOwnCapitalCaption"), CStr(mobjPrecov_apl.nOwnCapital), CStr(1),  ,  ,  , GetLocalResourceObject("chkOwnCapitalToolTip"))%></TD>
	            <TD WIDTH="65%">
					<%Call insPreDP035()%>
				</TD>
			</TR>
		</TABLE>

		<TABLE WIDTH="100%">
	        <TR>
	            <TD WIDTH="20%"><LABEL ID=14468><%= GetLocalResourceObject("valCoveraplCaption") %></LABEL></TD>
<%
With mobjValues
	.Parameters.Add("nBranch", Session("nBranch"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	.Parameters.Add("nProduct", Session("nProduct"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	.Parameters.Add("nCover", Session("nCover"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	.Parameters.Add("dEffecdate", Session("dEffecdate"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	.Parameters.Add("nModulec", Session("nModulec"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
End With
%>
	            <TD><%=mobjValues.PossiblesValues("valCoverapl", "TabGen_cover2", eFunctions.Values.eValuesType.clngWindowType, lintCoverapl, True,  ,  ,  ,  ,  , lblnDisabled)%></TD>
			</TR>
		</TABLE>

		<TABLE WIDTH="100%">
			<TR>
			<TD WIDTH="50%" COLSPAN="2" CLASS="HighLighted"><LABEL ID=100122><A NAME="Condiciones"><%= GetLocalResourceObject("AnchorCondiciones2Caption") %></A></LABEL></TD>
			<TD WIDTH="50%" COLSPAN="3" CLASS="HighLighted"><LABEL ID=100123><A NAME="Cambios"><%= GetLocalResourceObject("AnchorCambios2Caption") %></A></LABEL></TD>
			</TR>		
			<TR>
				<TD COLSPAN="2" CLASS="HorLine"></TD>
				<TD COLSPAN="3" CLASS="HorLine"></TD>
			</TR>		
			
			<TR>
				<TD><LABEL ID=14475><%= GetLocalResourceObject("tcnPremiMinCaption") %></LABEL></TD>

				<TD><%=mobjValues.NumericControl("tcnPremiMin", 18, CStr(mobjGen_cover.nPremiMin),  , GetLocalResourceObject("tcnPremiMinToolTip"),  , 6)%></TD>

				<TD WIDTH="15%"><%=mobjValues.CheckControl("chkPremiumAdd", GetLocalResourceObject("chkPremiumAddCaption"), CStr(mobjPrecov_apl.nchkPremiumAdd),  , "insDisabledField(this, 1);",  ,  , GetLocalResourceObject("chkPremiumAddToolTip"))%></TD>

				<TD WIDTH="5%"><LABEL ID=14469><%= GetLocalResourceObject("tcnRatepreaddCaption") %></LABEL></TD>

				<TD><%=mobjValues.NumericControl("tcnRatepreadd", 6, CStr(mobjGen_cover.nRatepreadd),  , GetLocalResourceObject("tcnRatepreaddToolTip"),  ,  ,  ,  ,  ,  , mobjPrecov_apl.nchkPremiumAdd <> 1, 2)%></TD>

			</TR>
			<TR>
			    <TD><LABEL ID=14472><%= GetLocalResourceObject("tcnPremiMaxCaption") %></LABEL></TD>
			    
			    <TD><%=mobjValues.NumericControl("tcnPremiMax", 18, CStr(mobjGen_cover.nPremiMax),  , GetLocalResourceObject("tcnPremiMaxToolTip"),  , 6)%></TD>
			    
				<TD WIDTH="15%"><%=mobjValues.CheckControl("chkPremiumSub", GetLocalResourceObject("chkPremiumSubCaption"), CStr(mobjPrecov_apl.nchkPremiumSub),  , "insDisabledField(this, 2);",  ,  , GetLocalResourceObject("chkPremiumSubToolTip"))%></TD>
				
				<TD WIDTH="5%"><LABEL ID=14474><%= GetLocalResourceObject("tcnRatepresubCaption") %></LABEL></TD>
				
				<TD><%=mobjValues.NumericControl("tcnRatepresub", 6, CStr(mobjGen_cover.nRatepresub),  , GetLocalResourceObject("tcnRatepresubToolTip"),  ,  ,  ,  ,  ,  , mobjPrecov_apl.nchkPremiumSub <> 1, 2)%></TD>
				
			</TR>
			<TR>
	            <TD><LABEL ID=14476> <%= GetLocalResourceObject("tcnApply_PercCaption") %></LABEL></TD>

	            <TD WIDTH="15%"><%=mobjValues.NumericControl("tcnApply_Perc", 5, CStr(mobjGen_cover.nApply_Perc),  , GetLocalResourceObject("tcnApply_PercToolTip"), True, 2)%></TD>
				<TD WIDTH="25%"><LABEL ID=14471><%= GetLocalResourceObject("tcnCheprelevCaption") %></LABEL></TD>
				<TD></TD>
				<TD><%=mobjValues.NumericControl("tcnCheprelev", 5, CStr(mobjPrecov_apl.nPremiumLev),  , GetLocalResourceObject("tcnCheprelevToolTip"),  , 0)%></TD>
			</TR>
			<TR>

	            <TD><LABEL ID=14476> <%= GetLocalResourceObject("tctsRou_verifyCaption") %></LABEL></TD>
            <TD><%=mobjValues.TextControl("tctsRou_verify", 12, mobjGen_cover.sRou_verify,  , GetLocalResourceObject("tctsRou_verifyToolTip"))%></TD>
			</TR>
		</TABLE>
		
		<TABLE WIDTH="100%">
			<TR>
				 <TD COLSPAN="2" CLASS="HighLighted"><LABEL ID=0><A NAME="Tarifa"><%= GetLocalResourceObject("AnchorTarifa2Caption") %></A></LABEL></TD>
			</TR>	
			<TR>
				<TD COLSPAN="2" CLASS="HorLine"></TD>
			</TR>
			<TR>
			    <TD WIDTH="65%"><%Call insPreDP035Tarif()%></TD>
			</TR>
		</TABLE>
		
	    <P ALIGN="Center"><A HREF="#BeginPage"><%= GetLocalResourceObject("AnchorBeginPageCaption") %></A></P>
	</FORM>
</BODY>
</HTML>
<%
'+ Se eliminan de la memoria las instancias creadas - ACM - 30/04/2001
mobjValues = Nothing
mobjGen_cover = Nothing
mobjPrecov_apl = Nothing
%>




