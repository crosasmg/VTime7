<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eGeneral" %>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">

'^Begin Header Block VisualTimer Utility 1.1 31/3/03 17.17.03
Dim mobjNetFrameWork As eNetFrameWork.Layout
'~End Header Block VisualTimer Utility

'- Objeto para el manejo de las funciones generales de carga de valores
    Dim mobjValues As New eFunctions.Values
Dim lclsGeneral As eGeneral.GeneralFunction
Dim mstrMessage As String

Dim ldtmDate As Date
Dim ldtmDateAux As String
Dim lstrMonth As String
Dim lstrDay As String
Dim lstrYear As String
Dim lstrFormat As String


</script>
<%Response.Expires = -1
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage("calendar")
    
Response.Write("" & vbCrLf)
Response.Write("<SCRIPT>" & vbCrLf)
    Response.Write("    var mintWidthCalendar = '")
    Response.Write(GetLocalResourceObject("WidthCalendar"))
    Response.Write("';" & vbCrLf)
    
    Response.Write("    var mintHeightCalendar = '")
    Response.Write(GetLocalResourceObject("HeightCalendar"))
    Response.Write("';" & vbCrLf)
    
    Response.Write("    var mintWidthCalendarDirecSearch = '")
    Response.Write(GetLocalResourceObject("WidthCalendarDirecSearch"))
    Response.Write("';" & vbCrLf)
    
    Response.Write("    var mintHeightCalendarDirecSearch = '")
    Response.Write(GetLocalResourceObject("HeightCalendarDirecSearch"))
    Response.Write("';" & vbCrLf)
    

Response.Write("" & vbCrLf)
Response.Write("</" & "SCRIPT>")
    

lclsGeneral = New eGeneral.GeneralFunction

mstrMessage = lclsGeneral.insLoadMessage(1959)

mobjValues = New eFunctions.Values
%>
<SCRIPT LANGUAGE="JavaScript" SRC="../Scripts/ValPopup.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="../Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT>
//- Variable para el control de versiones
    document.VssVersion="$$Revision: 3 $|$$Date: 19/05/04 11:47 $|$$Author: Nvaplat60 $"

    var mdtmDate, mstrFieldName=""
//% PutYear : Construye una fecha a partir de los parámetros.
//----------------------------------------------------------------------
function PutYear(sDateDay,sDateMonth,sDateYear) {
//----------------------------------------------------------------------
    var lintYear = 0; lintMonth = 0; lintDay = 0;
    lintYear += parseFloat(sDateYear);
    lintDay += parseFloat(sDateDay);
    lintMonth += parseFloat(sDateMonth);
    mdtmDate = new Date(lintYear,lintMonth - 1,lintDay)
}
//% compute : Obtiene la fecha según formato del sistema.
//----------------------------------------------------------------------
function compute(value) {
//----------------------------------------------------------------------
	var lintYear
	var lstrDate=""
	var lstrValue="<%=mobjValues.msUserDateFormat%>"
	
	lintYear = mdtmDate.getYear()
	
	if (lintYear < 1000)
		lintYear +=1900
		
	if (lintYear < 1000)
		lintYear += 1000
		
	lstrValue = lstrValue.replace("YYYY",lintYear) 

	lstrDate = ""
	if (mdtmDate.getMonth() + 1 < 10) 
		lstrDate += "0"

	lstrDate += (mdtmDate.getMonth() + 1) 
	lstrValue = lstrValue.replace("MM", lstrDate) 	
	
	lstrDate = ""
	
    if (parseFloat(value) < 10)
		lstrDate += "0"
		
    lstrDate += parseFloat(value);
    lstrValue = lstrValue.replace("DD",lstrDate)
    
	opener.document.forms[0].elements[mstrFieldName].value = lstrValue
	opener.$("#" + mstrFieldName).change();
	self.close()
}
//% insBackNext: Obtiene la fecha dependiendo de la candidad de días a sumar o restar dependiendo de los parámetros indicados.
//----------------------------------------------------------------------
function insBackNext(lintAction,lintQuant,lintDays){
//----------------------------------------------------------------------
    var lstrMonth = '', lstrLocation = ''
    var lintMonth = 0,  lintLength=0
    lstrLocation +=  document.location.href
    if (lstrLocation.search("MonthAdd=.*")>=0) {
	 	lstrMonth=lstrLocation.substr(lstrLocation.search("MonthAdd=.*"),lstrLocation.length - lstrLocation.search("MonthAdd=.*"))
	 	if(lstrMonth.search("&")<0)
	 	   lintLength = lstrMonth.length
	 	else
	 	   lintLength = lstrMonth.search("&")
	 	lstrMonth=lstrMonth.substr(0,lintLength)
	 	lstrMonth=lstrMonth.substr((lstrMonth.search("=")+1),lstrMonth.length -(lstrMonth.search("=")+1))
	 }
	 else
	    lstrMonth = "0"
	 lstrLocation=lstrLocation.replace(/&MonthAdd=.*/,"")
	 lintMonth = parseFloat(lstrMonth)
	 if (typeof(lintQuant)=='undefined'){
	     if (lintAction == 0)
	         lintMonth--
	     else 
	         lintMonth++
	 }
	 else
	     lintMonth += lintQuant
	 lstrMonth='&MonthAdd=' + lintMonth
	 lstrLocation+=lstrMonth
	 if (typeof(lintDays)!='undefined')
	     lstrLocation+='&DaysAdd=' + lintDays	 
	 document.location.href=lstrLocation
}
//% insReSize: Redimensiona el tamaño de la ventana.
//----------------------------------------------------------------------
function insReSize(Field){
//----------------------------------------------------------------------
    if (Field.checked){
        window.resizeTo(mintWidthCalendarDirecSearch,mintHeightCalendarDirecSearch)
        ShowDiv('DivDirect', 'show')
    }
    else{
        window.resizeTo(mintWidthCalendar,mintHeightCalendar)
        ShowDiv('DivDirect', 'hide')
    }    
}
//% insEnabledControls: Habilita o deshabilita los controles de la ventana.
//----------------------------------------------------------------------
function insEnabledControls(Field){
//----------------------------------------------------------------------
   with (document.forms[0]){
       tcnYears.disabled = (Field.value==3)
       tcnMonths.disabled = (Field.value==3)
       tcnDays.disabled =(Field.value==3)
       tcdDate.disabled =(Field.value!=3)
   }
}
//% insDirectAccess: Obtiene la fecha que se seleccionó de la ventana.
//----------------------------------------------------------------------
function insDirectAccess(){
//----------------------------------------------------------------------
    var lintMonths=0;
    var lintDays=0;
    var lintSing=1;
    var lstrLocation='';
    if (document.forms[0].optType[2].checked){
        if (document.forms[0].tcdDate.value=='')
			alert('Err. 1959: <%=mstrMessage%>');
        else{
            lstrLocation+=document.location.href
            lstrLocation=lstrLocation.replace(/CurDate=.*&/,"CurDate=" + document.forms[0].tcdDate.value + "&")
            document.location.href=lstrLocation
        }
    }
    else{
        lintSing = (document.forms[0].optType[0].checked?1:-1)
        lintMonths+=parseFloat((document.forms[0].tcnYears.value==''?0:document.forms[0].tcnYears.value)) * 12
        lintMonths+=parseFloat((document.forms[0].tcnMonths.value==''?0:document.forms[0].tcnMonths.value))
        lintMonths*=lintSing
        lintDays+=parseFloat((document.forms[0].tcnDays.value==''?0:document.forms[0].tcnDays.value))
        lintDays*=lintSing
        insBackNext(0,lintMonths,lintDays)
    }
}
</SCRIPT>
<HTML>
<HEAD>
	<META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>


<%
With Response
	.Write(mobjValues.StyleSheet())
	.Write(mobjValues.WindowsTitle("GE100"))
End With
%>
</HEAD>
<BODY ONUNLOAD="closeWindows()" ONLOAD="window.focus()">
<FORM NAME=frmDate>
<%
    lstrFormat = mobjValues.msUserDateFormat

If Not IsNothing(Request.QueryString.Item("FieldName")) Then
	Response.Write("<SCRIPT> mstrFieldName =""" & Request.QueryString.Item("FieldName") & """</SCRIPT>")
End If
If IsNothing(Request.QueryString.Item("CurDate")) Then
	With Response
		ldtmDate = Today
		.Write("<SCRIPT> mdtmDate = new Date() </SCRIPT>")
	End With
Else
	With Response
		ldtmDate = mobjValues.StringToType(Request.QueryString.Item("CurDate"), eFunctions.Values.eTypeData.etdDate)
		.Write("<SCRIPT> var mdtmDate;</SCRIPT>")
	End With
End If

If Request.QueryString.Item("MonthAdd") <> vbNullString Then
	ldtmDate = mobjValues.TypeToString(DateAdd(Microsoft.VisualBasic.DateInterval.Month, CInt(Request.QueryString.Item("MonthAdd")), ldtmDate), eFunctions.Values.eTypeData.etdDate)
End If

If Request.QueryString.Item("DaysAdd") <> vbNullString Then
	ldtmDate = mobjValues.TypeToString(DateAdd(Microsoft.VisualBasic.DateInterval.Day, CInt(Request.QueryString.Item("DaysAdd")), ldtmDate), eFunctions.Values.eTypeData.etdDate)
End If

ldtmDateAux = mobjValues.TypeToString(ldtmDate, eFunctions.Values.eTypeData.etdDate)

lstrYear = Mid(ldtmDateAux, InStr(CStr(lstrFormat), "Y"), 4)
lstrMonth = Mid(ldtmDateAux, InStr(CStr(lstrFormat), "M"), 2)
lstrDay = Mid(ldtmDateAux, InStr(CStr(lstrFormat), "D"), 2)

With Response
	.Write("<SCRIPT>PutYear(""" & lstrDay & """,""" & lstrMonth & """,""" & lstrYear & """)</SCRIPT>")
	.Write(mobjValues.CreateCalendar(mobjValues.StringToType(ldtmDateAux, eFunctions.Values.eTypeData.etdDate)))
End With
%>
<%=mobjValues.CheckControl("chkDirect", GetLocalResourceObject("chkDirectCaption"), CStr(False),  , "insReSize(this)")%>
	<DIV ID="DivDirect">
	    <TABLE COLS=4>
	        <TR>
	            <TD><%=mobjValues.OptionControl(40517, "optType", GetLocalResourceObject("optType_CStr1Caption"), CStr(1), CStr(1), "insEnabledControls(this)")%></TD>
	            <TD WIDTH=15pcx>&nbsp;</TD>
	            <TD><LABEL ID=40513><%= GetLocalResourceObject("AnchorCaption") %></LABEL></TD><TD><%=mobjValues.NumericControl("tcnDays", 5, vbNullString, False, "Cantidad de días a agregar/disminuir a la fecha",  ,  ,  ,  ,  ,  , True)%></TD>
	        </TR>
	        <TR>
	            <TD><%=mobjValues.OptionControl(40518, "optType", GetLocalResourceObject("optType_CStr2Caption"), CStr(2), CStr(2), "insEnabledControls(this)")%></TD>
	            <TD WIDTH=15pcx>&nbsp;</TD>
	            <TD><LABEL ID=40514><%= GetLocalResourceObject("Anchor2Caption") %></LABEL></TD><TD><%=mobjValues.NumericControl("tcnMonths", 3, vbNullString, False, "Cantidad de meses a agregar/disminuir a la fecha en proceso",  ,  ,  ,  ,  ,  , True)%></TD>
	        </TR>
	        <TR>
	            <TD><%=mobjValues.OptionControl(40519, "optType", GetLocalResourceObject("optType_CStr3Caption"), CStr(2), CStr(3), "insEnabledControls(this)")%></TD>
	            <TD WIDTH=15pcx>&nbsp;</TD>
	            <TD><LABEL ID=40515><%= GetLocalResourceObject("btnApplyCaption") %></LABEL></TD><TD><%=mobjValues.NumericControl("tcnYears", 3, vbNullString, False, "Cantidad de años a agregar/disminuir a la fecha en proceso",  ,  ,  ,  ,  ,  , True)%></TD>
	        </TR>
	        <TR>
	            <TD><%=mobjValues.AnimatedButtonControl("btnApply", "/VTimeNet/Images/FindPolicyOff.png", GetLocalResourceObject("btnApplyToolTip"),  , "insDirectAccess()")%></TD>
	            <TD WIDTH=15pcx>&nbsp;</TD>
	            <TD><LABEL ID=40516><%= GetLocalResourceObject("Anchor3Caption") %></LABEL></TD><TD><INPUT TYPE="TEXT" NAME="tcdDate" SIZE="10" VALUE="" MAXLENGTH="10" TABINDEX=0 ONBLUR='tcdDate.IsReq=0;tcdDate.Alias="Fecha en la que debe ubicarse el calendario";if(ValDate(tcdDate)){}' NOTAB DISABLED ONFOCUS="ChangeFocus(this)"></TD>
	        </TR>
	    </TABLE>
	</DIV>
	<TABLE WIDTH=100%>
		<TR>
			<TD COLSPAN="3" CLASS="HORLINE"></TD>
		</TR>
		<TR>
			<TD WIDTH=5%><%=mobjValues.ButtonAbout("GE100")%></TD>
			<TD WIDTH=5%><%=mobjValues.ButtonHelp("GE100")%></TD>
			<TD ALIGN="RIGHT"><%=mobjValues.ButtonAcceptCancel( ,  ,  ,  , eFunctions.Values.eButtonsToShow.OnlyCancel)%></TD>
		</TR>
	</TABLE>
</FORM>
<%
mobjValues = Nothing
lclsGeneral = Nothing

%>
</BODY>
</HTML>
<script type="text/javascript">
    $(document).ready(function () {
        insReSize(document.forms[0].chkDirect);
        $("[name=cboMonth]").change(function () { insBackNext(0, this.value - document.forms[0].tcnCurMonth.value); });
        $("[name=cboYear]").change(function () { insBackNext(0, this.value * 12); });
        insEnabledControls(document.forms[0].optType[0]);

    });
</script>
<%'^Begin Footer Block VisualTimer Utility 1.1 31/3/03 17.17.03
Call mobjNetFrameWork.FinishPage("calendar")
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>





