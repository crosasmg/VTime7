<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eProduct" %>
<script language="VB" runat="Server">
'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values
Dim mobjMenu As eFunctions.Menues
Dim mclsGen_cover As eProduct.Gen_cover


'%insPreDP052:función que realiza el llenado de cada uno de los campos de la transacción
'%en caso de existir previamente el registro en la tabla Gen_cover.
'--------------------------------------------------------------------------------------------
Private Sub insPreDP052()
	'--------------------------------------------------------------------------------------------
	Call mclsGen_cover.insPreDP052(mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nModulec"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nCover"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate))
End Sub

</script>
<%Response.Expires = -1
mobjValues = New eFunctions.Values
mobjMenu = New eFunctions.Menues
mclsGen_cover = New eProduct.Gen_cover
mobjValues.ActionQuery = Session("bQuery")
Call insPreDP052()

mobjValues.sCodisplPage = "dp052"
%>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<HTML>
<HEAD>
    <META NAME = "GENERATOR" CONTENT = "Microsoft Visual Studio 6.0">


    <%With Response
	.Write(mobjValues.StyleSheet())
	.Write(mobjValues.WindowsTitle("DP052"))
	.Write(mobjValues.ShowWindowsName("DP052"))
	.Write(mobjMenu.setZone(2, "DP052", "DP052.aspx"))
End With
mobjMenu = Nothing%>
<SCRIPT>
//- Variable para el control de versiones
	document.VssVersion="$$Revision: 2 $|$$Date: 15/10/03 17:04 $|$$Author: Nvaplat61 $"

//%insLockedPercent:Permite verificar el valor del check para habilitar o deshabilitar
//%el campo correspondiente al porcentaje(disminucion,aumento)
//--------------------------------------------------------------------------------------------
function insLockedPercent(Field,nField){
//--------------------------------------------------------------------------------------------
    with(document.forms[0].elements){
        if(nField==1){
            Field.checked?tcnCapitalAddCh.disabled=false:tcnCapitalAddCh.disabled=true
            tcnCapitalAddCh.value=0
        }
        else{
            Field.checked?tcnCapitalSubCh.disabled=false:tcnCapitalSubCh.disabled=true
			tcnCapitalSubCh.value=0
		}
    }
}
//%Esta rutina se encarga de desplegar los valores correspondientes
//%a las condiciones de cálculo de prima
//--------------------------------------------------------------------------------------------
function insDefaultValue(sAddsuini,sAddreini,sAddtaxin,sRevalapl,sCacalrei){
//--------------------------------------------------------------------------------------------
    with(document.forms[0].elements){
	    switch(sAddsuini){
	        case 1:
                optCapital[0].checked=true
                optCapital[1].checked=false
                optCapital[2].checked=false
                break
			case 2:
				optCapital[0].checked=false
				optCapital[1].checked=false
				optCapital[2].checked=true
				break
			case 3:
				optCapital[0].checked=false
				optCapital[1].checked=true
				optCapital[2].checked=false
				break
			default:	
				optCapital[2].checked=true
        }
	    if(optReinsu[0].disabled)
			sAddreini=2
	    switch(sAddreini){
	        case 1:
                optReinsu[0].checked=true
                optReinsu[1].checked=false
                optReinsu[2].checked=false
                break
			case 2:
				optReinsu[0].checked=false
				optReinsu[1].checked=false
				optReinsu[2].checked=true
				break
			case 3:
				optReinsu[0].checked=false
				optReinsu[1].checked=true
				optReinsu[2].checked=false
				break
			default:
				optReinsu[2].checked=true
        }
	    switch(sAddtaxin){
	        case 1:
                optTax[0].checked=true
                optTax[1].checked=false
                optTax[2].checked=false
                break
			case 2:
				optTax[0].checked=false
				optTax[1].checked=false
				optTax[2].checked=true
				break
			case 3:
				optTax[0].checked=false
				optTax[1].checked=true
				optTax[2].checked=false
				break
			default:
				optTax[2].checked=true
        }
    }
}
</SCRIPT>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="post" ID="FORM" NAME="DP052" ACTION="valCoverSeq.aspx?mode=2&sCacalili=<%=mclsGen_cover.sCacalili%>">
    <TABLE WIDTH="100%">
        <TR>
            <TD COLSPAN="2" CLASS="HighLighted"><LABEL ID=100145>Condiciones</TD>
			<TD WIDTH=10%> &nbsp;</TD>
            <TD COLSPAN="2" CLASS="HighLighted"><LABEL ID=100146>Suma para</TD>
        </TR>
        <TR>
            <TD COLSPAN="2" CLASS="HorLine"></TD>
			<TD></TD>
            <TD COLSPAN="2" CLASS="HorLine"></TD>
        </TR>
        <TR>
			<TD COLSPAN="3"><%=mobjValues.CheckControl("chkIndex", GetLocalResourceObject("chkIndexCaption"), mclsGen_cover.sCacalrei, "1",  ,  , 1, GetLocalResourceObject("chkIndexToolTip"))%></TD>
		    <TD COLSPAN="2"><LABEL ID=14570><%= GetLocalResourceObject("AnchorCaption") %></LABEL></TD>
		</TR>
        <TR>
			<TD><LABEL ID=14574><%= GetLocalResourceObject("tcnCapitalminCaption") %></LABEL></TD>
			<TD COLSPAN="2"><%=mobjValues.NumericControl("tcnCapitalmin", 18, CStr(mclsGen_cover.nCacalmin),  , GetLocalResourceObject("tcnCapitalminToolTip"), True, 6,  ,  ,  ,  ,  , 2)%></TD>
			<TD>&nbsp;</TD>
			<TD><%=mobjValues.OptionControl(100148, "optCapital", GetLocalResourceObject("optCapital_CStr1Caption"),  , CStr(1),  ,  , 4, GetLocalResourceObject("optCapital_CStr1ToolTip"))%></TD>
		</TR>
		<TR>
			<TD><LABEL ID=14573><%= GetLocalResourceObject("tcnCapitalmaxCaption") %></LABEL></TD>
		    <TD COLSPAN="3"><%=mobjValues.NumericControl("tcnCapitalmax", 18, CStr(mclsGen_cover.nCacalmax),  , GetLocalResourceObject("tcnCapitalmaxToolTip"), True, 6,  ,  ,  ,  ,  , 3)%></TD>
		    <TD><%=mobjValues.OptionControl(100149, "optCapital", GetLocalResourceObject("optCapital_CStr3Caption"),  , CStr(3),  ,  , 5, GetLocalResourceObject("optCapital_CStr3ToolTip"))%></TD>
		</TR>
		<TR>
			<TD COLSPAN="4">&nbsp;</TD>
		    <TD><%=mobjValues.OptionControl(100150, "optCapital", GetLocalResourceObject("optCapital_CStr2Caption"),  , CStr(2),  ,  , 6, GetLocalResourceObject("optCapital_CStr2ToolTip"))%></TD>
		</TR>
		<TR>
			<TD COLSPAN="3">&nbsp;</TD>
			<TD><LABEL ID=14576><%= GetLocalResourceObject("Anchor2Caption") %></LABEL></TD>
		</TR>
        <TR>
            <TD COLSPAN="2" CLASS="HighLighted"><LABEL ID=100397>Cambios</TD>
            <TD COLSPAN="2">&nbsp;</TD>
			<TD><%=mobjValues.OptionControl(100152, "optReinsu", GetLocalResourceObject("optReinsu_CStr1Caption"),  , CStr(1),  , mclsGen_cover.nBranch_rei = eRemoteDB.Constants.intNull, 7, GetLocalResourceObject("optReinsu_CStr1ToolTip"))%></TD>
        </TR>
        <TR>
            <TD COLSPAN="2" CLASS="HorLine"></TD>
            <TD COLSPAN="3"></TD>
        </TR>
        <TR>
         	<TD><%=mobjValues.CheckControl("chkCapitalAddCh", GetLocalResourceObject("chkCapitalAddChCaption"), mclsGen_cover.DefaultValueCapital(mclsGen_cover.sCh_typ_cap, "AddCh"), "1", "insLockedPercent(this,1)",  , 10, GetLocalResourceObject("chkCapitalAddChToolTip"))%></TD>
		    <TD COLSPAN="3"><LABEL><%= GetLocalResourceObject("tcnCapitalAddChCaption") %> &nbsp;</LABEL>
				<%=mobjValues.NumericControl("tcnCapitalAddCh", 4, CStr(mclsGen_cover.nRateCapAdd),  , GetLocalResourceObject("tcnCapitalAddChToolTip"), True, 2,  ,  ,  ,  , mclsGen_cover.DefaultValueCapital(mclsGen_cover.sCh_typ_cap, "AddCh") <> "1", 12)%>
			</TD>
		    <TD><%=mobjValues.OptionControl(100153, "optReinsu", GetLocalResourceObject("optReinsu_CStr3Caption"),  , CStr(3),  , mclsGen_cover.nBranch_rei = eRemoteDB.Constants.intNull, 8, GetLocalResourceObject("optReinsu_CStr3ToolTip"))%></TD>
        </TR>
		<TR>
			<TD><%=mobjValues.CheckControl("chkCapitalSubCh", GetLocalResourceObject("chkCapitalSubChCaption"), mclsGen_cover.DefaultValueCapital(mclsGen_cover.sCh_typ_cap, "SubCh"), "1", "insLockedPercent(this,2)",  , 11, GetLocalResourceObject("chkCapitalSubChToolTip"))%></TD>
			<TD COLSPAN="3"><LABEL><%= GetLocalResourceObject("tcnCapitalAddChCaption") %> &nbsp;</LABEL>
				<%=mobjValues.NumericControl("tcnCapitalSubCh", 4, CStr(mclsGen_cover.nRateCapSub),  , GetLocalResourceObject("tcnCapitalSubChToolTip"), True, 2,  ,  ,  ,  , mclsGen_cover.DefaultValueCapital(mclsGen_cover.sCh_typ_cap, "SubCh") <> "1", 13)%>
			</TD>
            <TD><%=mobjValues.OptionControl(100151, "optReinsu", GetLocalResourceObject("optReinsu_CStr2Caption"),  , CStr(2),  , mclsGen_cover.nBranch_rei = eRemoteDB.Constants.intNull, 9, GetLocalResourceObject("optReinsu_CStr2ToolTip"))%></TD>
		</TR>
        <TR>
			<TD COLSPAN="3">&nbsp;</TD>
		    <TD COLSPAN="2"><LABEL ID=14577><%= GetLocalResourceObject("Anchor3Caption") %></LABEL></TD>
		</TR>
		<TR>
			<TD><LABEL ID=14572><%= GetLocalResourceObject("tcnCapitalLevCaption") %></LABEL></TD>
		    <TD><%=mobjValues.NumericControl("tcnCapitalLev", 5, CStr(mclsGen_cover.nChCapLev),  , GetLocalResourceObject("tcnCapitalLevToolTip"),  ,  ,  ,  ,  ,  ,  , 14)%></TD>
			<TD COLSPAN="2">&nbsp;</TD>
		    <TD><%=mobjValues.OptionControl(100155, "optTax", GetLocalResourceObject("optTax_CStr1Caption"),  , CStr(1),  ,  , 15, GetLocalResourceObject("optTax_CStr1ToolTip"))%></TD>
        </TR>
		<TR>
			<TD COLSPAN="4">&nbsp;</TD>
		    <TD><%=mobjValues.OptionControl(100156, "optTax", GetLocalResourceObject("optTax_CStr3Caption"),  , CStr(3),  ,  , 16, GetLocalResourceObject("optTax_CStr3ToolTip"))%></TD>
        </TR>
		<TR>
			<TD COLSPAN="4">&nbsp;</TD>
		    <TD><%=mobjValues.OptionControl(100154, "optTax", GetLocalResourceObject("optTax_CStr2Caption"),  , CStr(2),  ,  , 17, GetLocalResourceObject("optTax_CStr2ToolTip"))%></TD>
        </TR>
    </TABLE>
</FORM>
</BODY>
</HTML>
<%
If Session("bQuery") <> True Then
	Response.Write("<SCRIPT>insDefaultValue(" & mclsGen_cover.sAddsuini & "," & mclsGen_cover.sAddreini & "," & mclsGen_cover.sAddtaxin & "," & mclsGen_cover.sAddtaxin & "," & mclsGen_cover.sRevalapl & "," & mclsGen_cover.sCacalrei & ")</SCRIPT>")
End If
%>
<%
mobjValues = Nothing
mclsGen_cover = Nothing
%>





