<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eMargin" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'- Objeto para el manejo de las zonas de la página    
Dim mobjMenu As eFunctions.Menues

'- Objeto para el manejo de los datos a mostrar en la página
Dim mclsMargin_master As eMargin.Margin_master


</script>
<%Response.Expires = -1

mobjMenu = New eFunctions.Menues
mobjValues = New eFunctions.Values
mobjValues.sCodisplPage = "MGS001"
If Request.QueryString.Item("sReload") <> "MGS001" Then
	Session("nIdtable") = ""
End If
Session("sReload") = ""
%>
<HTML>
<HEAD>
	<META NAME="GENERATOR" CONTENT="Personalización VTime">
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/valFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tMenu.js"></SCRIPT>




<SCRIPT>
//- Variable para el control de versiones
    document.VssVersion="$$Revision: 22 $|$$Date: 18/12/03 16:59 $|$$Author: Nvaplat15 $"

//% insChangeFields: se controla el cambio de valor de los campos
//-------------------------------------------------------------------------------------------
function insChangeFields(Field, sOption){
//-------------------------------------------------------------------------------------------
	var lstrHREF = self.document.location.href;
	with(self.document.forms[0]){
		switch(sOption){
			case 'cbeTableTyp':
//+ Se valida el valor del campo "Tipo de tabla".  En caso de ser "Siniestros" o "Pasivo",
//+ se reacarga la página
//				if (Field.value==5 || 
//				    Field.value==2 || 
//			       (Field.value!=5 && 
//			        '<%=Request.QueryString.Item("nTableTyp")%>'=='5') ||
//				   (Field.value!=2 && 
//				   '<%=Request.QueryString.Item("nTableTyp")%>'=='2')){
					lstrHREF = lstrHREF.replace(/&dInitDate.*/,'') + 
					           '&dInitDate=' + tcdInitDate.value + 
					           '&dEndDate=' + tcdEndDate.value + 
					           '&nInsur_area=' + '<%=Session("nInsur_area")%>' + 
					           '&nTableTyp=' + cbeTableTyp.value + 
					           '&nSource=' + cbeSource.value + 
					           '&nClaimClass=' + cbeClaimClass.value +
					           '&sReload=MGS001';
					self.document.location.href = lstrHREF;
				    
//				}
				break;
		}
	}
}
//% BlankDate: blanquea los campos de fecha que indican el período
//-------------------------------------------------------------------------------------------
function BlankDate(){
//-------------------------------------------------------------------------------------------
    with (document.forms[0]) {
		tcdInitDate.value='';
		tcdEndDate.value='';
	    
	}
}



//% insStateZone: habilita los campos de la forma
//-------------------------------------------------------------------------------------------
function insStateZone(nAction, nOrig){
//-------------------------------------------------------------------------------------------
    var lstrHREF = self.document.location.href;
    var lblndisabled = (nAction==301 || nAction==401 || nAction == 302)?false:true;

    if(top.frames["fraSequence"].plngMainAction==301 &&
       '<%=Request.QueryString.Item("nMainAction")%>'!=''){
		lstrHREF = lstrHREF.replace(/&dInitDate.*/,'') + '&sReload=MGS001';
		self.document.location.href = lstrHREF;
	}
	else{
	    if (typeof(nOrig) == 'undefined')
		BlankDate();
		with (document.forms[0]) {
			if (nAction == 302 ||
			    nAction==401){ 
                if (typeof(nOrig) == 'undefined'){
                btnPeriodValues.disabled=lblndisabled;
				cbeTableTyp.disabled=!lblndisabled;
				cbeSource.disabled=!lblndisabled;
				cbeClaimClass.disabled=!lblndisabled;
				tcdInitDate.disabled=!lblndisabled;
				btn_tcdInitDate.disabled=!lblndisabled;			    
				tcdEndDate.disabled=!lblndisabled;
				btn_tcdEndDate.disabled=!lblndisabled;
				}
			}
			else{
			    btnPeriodValues.disabled=!lblndisabled;
			    cbeTableTyp.disabled=lblndisabled;
				cbeSource.disabled=lblndisabled;
				cbeClaimClass.disabled=lblndisabled;
				tcdInitDate.disabled=lblndisabled;
				btn_tcdInitDate.disabled=lblndisabled;
				tcdEndDate.disabled=lblndisabled;
				btn_tcdEndDate.disabled=lblndisabled;
			}
		}
	}
}

//% insShowMSG003: Se muestran los períodos generados
//-------------------------------------------------------------------------------------------
function insShowMSG003(){
//-------------------------------------------------------------------------------------------
	
	
	with(self.document.forms[0]){
		ShowPopUp("MGS003.aspx?nInsur_area=" + '<%=Session("nInsur_area")%>' + '&nMainAction=' + top.frames['fraSequence'].plngMainAction, "MGS003", 320, 420, "no"); 
	        
	}  
}

//% insCancel: Ejecuta rutinas necesarias en el momento de cancelar la página
//-------------------------------------------------------------------------------------------
function insCancel(){
//-------------------------------------------------------------------------------------------
   return true
}
</SCRIPT>
<%
With Response
	.Write(mobjValues.StyleSheet())
	.Write(mobjMenu.MakeMenu("MGS001", "MGS001_K.aspx", 1, Request.QueryString.Item("sWindowDescript"), Session("sDesMultiCompany"), Session("sSche_code")))
End With
%>    
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<%
mclsMargin_master = New eMargin.Margin_master

Call mclsMargin_master.Find_list(mobjValues.StringToType(Session("nInsur_area"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("dInitdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Request.QueryString.Item("dEnddate"), eFunctions.Values.eTypeData.etdDate), Request.QueryString.Item("nTableTyp"), CInt(Request.QueryString.Item("nTableTyp")))
%>
<FORM METHOD="POST" ID="FORM" NAME="MGS001" ACTION="valMarginTra.aspx?sMode=1">
    <BR><BR><BR>
    <TABLE WIDTH="100%">
        
        <TR>
            <TD><LABEL ID=0></LABEL></TD>
            <TD></TD>
			<TD WIDTH="5%">&nbsp;</TD>
            <TD COLSPAN="2" CLASS="HighLighted"><LABEL ID=0><%= GetLocalResourceObject("AnchorCaption") %></LABEL></TD>
        </TR>
        <TR>
            <TD COLSPAN="3"></TD>
            <TD COLSPAN="2" CLASS="HorLine"></TD>
        </TR>
        <TR>
            <TD><LABEL ID=0><%= GetLocalResourceObject("cbeTableTypCaption") %></LABEL></TD>
            <TD><%mobjValues.BlankPosition = False
If mclsMargin_master.sTabletyp <> vbNullString Then
	mobjValues.TypeList = 1
	mobjValues.List = mclsMargin_master.sTabletyp
End If
Response.Write(mobjValues.PossiblesValues("cbeTableTyp", "Table5607", eFunctions.Values.eValuesType.clngComboType, Request.QueryString.Item("nTableTyp"),  ,  ,  ,  ,  , "insChangeFields(this, ""cbeTableTyp"")", mclsMargin_master.sTabletyp = vbNullString,  , GetLocalResourceObject("cbeTableTypToolTip"),  , 5))
%>
			</TD>
            <TD>&nbsp;</TD>
            <TD><LABEL ID=0><%= GetLocalResourceObject("tcdInitDateCaption") %></LABEL></TD>
            <TD><%Response.Write(mobjValues.DateControl("tcdInitDate", Request.QueryString.Item("dInitDate"),  , GetLocalResourceObject("tcdInitDateToolTip"),  ,  ,  ,  , True, 2))
Response.Write("&nbsp;")
Response.Write(mobjValues.AnimatedButtonControl("btnPeriodValues", "/VTimeNet/images/btn_ValuesOff.png", GetLocalResourceObject("btnPeriodValuesToolTip"),  , "insShowMSG003()", CStr(Session("nInsur_area")) = vbNullString, 3))
%>
			</TD>
        </TR>
        <TR>
            <TD><LABEL ID=0><%= GetLocalResourceObject("cbeSourceCaption") %></LABEL></TD>
            <TD><%mobjValues.BlankPosition = False
If mclsMargin_master.sSource <> vbNullString Then
	mobjValues.TypeList = 1
	mobjValues.List = mclsMargin_master.sSource
Else
	mobjValues.TypeList = 1
	mobjValues.List = "1,2,3"
End If
Response.Write(mobjValues.PossiblesValues("cbeSource", "Table5608", eFunctions.Values.eValuesType.clngComboType, Request.QueryString.Item("nSource"),  ,  ,  ,  ,  ,  , mclsMargin_master.sSource = vbNullString,  , GetLocalResourceObject("cbeSourceToolTip"),  , 6))
%>
		    </TD>
            <TD>&nbsp;</TD>
			<TD><LABEL ID=0><%= GetLocalResourceObject("tcdEndDateCaption") %></LABEL></TD>
            <TD><%=mobjValues.DateControl("tcdEndDate", Request.QueryString.Item("dEndDate"),  , GetLocalResourceObject("tcdEndDateToolTip"),  ,  ,  ,  , True, 4)%></TD>
		</TR>
		<%If Request.QueryString.Item("nTableTyp") = "2" Or (Request.QueryString.Item("sReload") = "MGS003" And mclsMargin_master.bClaimclass) Then%>
				<TR>
				    <TD><LABEL ID=0><%= GetLocalResourceObject("cbeClaimClassCaption") %></LABEL></TD>
				    <TD><%	mobjValues.BlankPosition = False
	If mclsMargin_master.sClaimclass <> vbNullString Then
		mobjValues.TypeList = 1
		mobjValues.List = mclsMargin_master.sClaimclass
	End If
	Response.Write(mobjValues.PossiblesValues("cbeClaimClass", "Table5609", eFunctions.Values.eValuesType.clngComboType,  ,  ,  ,  ,  ,  ,  , mclsMargin_master.sClaimclass = vbNullString,  , GetLocalResourceObject("cbeClaimClassToolTip"),  , 7))
	%>
					</TD>
					<TD COLSPAN="3">&nbsp;</TD>
				</TR>
        <%	
Else
	Response.Write(mobjValues.HiddenControl("cbeClaimClass", Request.QueryString.Item("nClaimClass")))
End If
%>
    </TABLE>
	<%=mobjValues.HiddenControl("hddIdTable", Request.QueryString.Item("nIdTable"))%>
  
</FORM>
</BODY>
</HTML>
<%
If Request.QueryString.Item("sReload") = "MGS001" Then
	Response.Write("<SCRIPT>insStateZone(top.fraSequence.plngMainAction, 69);</SCRIPT>")
End If

mobjMenu = Nothing
mobjValues = Nothing
mclsMargin_master = Nothing
%>




