<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eProduct" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values
Dim mobjMenu As eFunctions.Menues
Dim mclsProduct As eProduct.Product


</script>
<%Response.Expires = -1

mobjValues = New eFunctions.Values
mobjMenu = New eFunctions.Menues
mclsProduct = New eProduct.Product

mobjValues.ActionQuery = Session("bQuery")

Call mclsProduct.FindProduct_li(mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToDate(Session("dEffecdate")))

mobjValues.sCodisplPage = "dp021"
%>
<HTML>
<HEAD>
    <META NAME = "GENERATOR" CONTENT = "Microsoft Visual Studio 6.0">
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>




<%With Response
	.Write(mobjValues.StyleSheet())
	.Write(mobjValues.ShowWindowsName("DP021"))
	.Write("<SCRIPT>var nMainAction=304</SCRIPT>")
	.Write(mobjMenu.setZone(2, Request.QueryString.Item("sCodispl"), "DP021.aspx"))
End With
mobjMenu = Nothing
%>
<SCRIPT LANGUAGE="JavaScript">
//- Variable para el control de versiones
	document.VssVersion="$$Revision: 3 $|$$Date: 13/02/06 19:46 $|$$Author: Clobos $"

//% insLockedFondPerm: habilita/deshabilita los campos dependiendo si admite o no 
//%					   fondos de inversión
//-------------------------------------------------------------------------------------------
function insLockedFondPerm(Field){
//-------------------------------------------------------------------------------------------
    with (self.document.forms[0]){
        tcnUlsmaxqu.disabled = (Field.value==0 ||
                                Field.value=='')?
                                true:
                                false;
        tcnUlrmaxqu.disabled = (Field.value==0 ||
                                Field.value=='')?
                                true:
                                false;
        if(Field.value==0 ||
           Field.value==''){
            cbeUlswiper.disabled = true;
            tcnUlsschar.disabled = true;
            tcnUlscharg.disabled = true;
            cbeUlredper.disabled = true;
            tcnUlrschar.disabled = true;
            tcnUlrcharg.disabled = true;

            tcnUlsmaxqu.value = '';
            cbeUlswiper.value = '';
            tcnUlsschar.value = '0';
            tcnUlscharg.value = '0,00';
            tcnUlrmaxqu.value = '';
            cbeUlredper.value = '';
            tcnUlrschar.value = '0';
            tcnUlrcharg.value = '0,00';
        }
    }
}
//% insLockedPerm: funciòn que se encarga de habilitar los campos si se modifica "Permitidos" 
//-------------------------------------------------------------------------------------------
function insLockedPerm(Field){
//-------------------------------------------------------------------------------------------
    var lblnDisabled = (Field.value==0 || Field.value=='')

    with (self.document.forms[0]){
        cbeULswMaxper.disabled = lblnDisabled;
        tcnULmmsw.disabled = lblnDisabled;
        tcnUlswmqt.disabled = lblnDisabled;
        tcnUlscharg.disabled = lblnDisabled;
        cbeULswmqtper.disabled = lblnDisabled;
        tcnULswchPerc.disabled = lblnDisabled;
        cbeUlswiper.disabled = lblnDisabled;
        tcnUlsschar.disabled = lblnDisabled;
        
        if(Field.value==0 ||
           Field.value==''){
            cbeULswMaxper.value = '';
            tcnULmmsw.value = '0';
            tcnUlswmqt.value = '0';
            tcnUlscharg.value = '0,00';
            cbeULswmqtper.value = '';
            tcnULswchPerc.value = '0';
            cbeUlswiper.value = '';
            tcnUlsschar.value = '0';
            
        }
    }
}
//% insLockedPermda: funciòn que se encarga de habilitar los campos si se modifica "Permitida" 
//-------------------------------------------------------------------------------------------
function insLockedPermRd(Field){
//-------------------------------------------------------------------------------------------
    var lblnDisabled = (Field.value==0 || Field.value=='')
    
    with (self.document.forms[0]){
        cbeULrdMaxper.disabled  = lblnDisabled;
        tcnULmmrd.disabled      = lblnDisabled;
        tcnUlrdmqt.disabled     = lblnDisabled;
        tcnUlrcharg.disabled    = lblnDisabled;
        cbeULrdmqtper.disabled  = lblnDisabled;
        tcnULrdchPerc.disabled  = lblnDisabled;
        cbeUlredper.disabled    = lblnDisabled;
        tcnUlrschar.disabled    = lblnDisabled;

        if(lblnDisabled){
            cbeULrdMaxper.value = '';
            tcnULmmrd.value     = '0';
            tcnUlrdmqt.value    = '0';
            tcnUlrcharg.value   = '0,00';
            cbeULrdmqtper.value = '';
            tcnULrdchPerc.value = '0';
            cbeUlredper.value   = '';
            tcnUlrschar.value   = '0';
        }    
    }
}
</SCRIPT>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="post" ID="FORM" NAME="frmDP021" ACTION="ValProdLifeSeq.aspx?sZone=1">
    <TABLE WIDTH="100%">
        <TR>
            <TD><LABEL ID=14779><%= GetLocalResourceObject("cbeCurrencyCaption") %></LABEL></TD>
            <TD><%=mobjValues.PossiblesValues("cbeCurrency", "Table11", eFunctions.Values.eValuesType.clngComboType, CStr(mclsProduct.nCurrency),  ,  ,  ,  ,  ,  , True,  , GetLocalResourceObject("cbeCurrencyToolTip"))%></TD>
            <TD>&nbsp;</TD>
            <TD><LABEL ID=14781><%= GetLocalResourceObject("tcnUlfmaxquCaption") %></LABEL></TD>
            <TD><%=mobjValues.NumericControl("tcnUlfmaxqu", 5, CStr(mclsProduct.nUlfmaxqu),  , GetLocalResourceObject("tcnUlfmaxquToolTip"),  , 0,  ,  ,  , "insLockedFondPerm(this)")%></TD>
        </TR>
        <TR>
            <TD><LABEL ID=14784><%= GetLocalResourceObject("cbeInfTypeCaption") %></LABEL></TD>
            <TD><%=mobjValues.PossiblesValues("cbeInfType", "Table5675", eFunctions.Values.eValuesType.clngComboType, CStr(mclsProduct.nInfType),  ,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbeInfTypeToolTip"))%></TD>
            <TD>&nbsp;</TD>
            <TD COLSPAN="2"><%=mobjValues.CheckControl("chkUlfchani", GetLocalResourceObject("chkUlfchaniCaption"), mclsProduct.sUlfchani,  ,  , mclsProduct.nProdclas = 2)%></TD>
        </TR>
        <TR>
            <TD><LABEL ID=LABEL1><%= GetLocalResourceObject("cbeTyperateproyCaption")%></LABEL></TD>
            <TD><%= mobjValues.PossiblesValues("cbeTyperateproy", "Table8300", eFunctions.Values.eValuesType.clngComboType, CStr(mclsProduct.nType_Rateproy), , , , , , , , , GetLocalResourceObject("cbeTyperateproyToolTip"))%></TD>
        </TR>
    </TABLE>
    <TABLE  WIDTH="100%">
        <TR>
            <TD COLSPAN="5" CLASS="HighLighted">
                <LABEL ID=100181><A NAME="Switches"><%= GetLocalResourceObject("AnchorSwitchesCaption") %></A></LABEL>
             </TD>
        </TR>
        <TR>
            <TD COLSPAN="5" CLASS="Horline"></TD>
        </TR>  
        <TR>
            <TD><LABEL ID=14780><%= GetLocalResourceObject("tcnUlsmaxquCaption") %></LABEL></TD>
            <TD><%=mobjValues.NumericControl("tcnUlsmaxqu", 5, CStr(mclsProduct.nUlsmaxqu),  , GetLocalResourceObject("tcnUlsmaxquToolTip"),  , 0,  ,  ,  , "insLockedPerm(this)")%></TD>
            <TD WIDTH=5%>&nbsp;</TD>
            <TD><LABEL ID=14788><%= GetLocalResourceObject("cbeULswMaxperCaption") %></LABEL></TD>
            <TD><%=mobjValues.PossiblesValues("cbeULswMaxper", "Table5644", 1, CStr(mclsProduct.nULswmaxper),  ,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbeULswMaxperToolTip"))%></TD>
        </TR>

        <TR>
            <TD WIDTH=5% COLSPAN="5">&nbsp;</TD>
        </TR>

        <TR>
            <TD COLSPAN="2" CLASS="HighLighted">
                <LABEL ID=0><A NAME="PerCambiosSw"><%= GetLocalResourceObject("AnchorPerCambiosSwCaption") %></A></LABEL>
            </TD>
            <TD WIDTH=5%>&nbsp;</TD>
            <TD COLSPAN="2" CLASS="HighLighted">
                <LABEL ID=0><A NAME="RecargosSw"><%= GetLocalResourceObject("AnchorRecargosSwCaption") %></A></LABEL>
            </TD>
        </TR>

        <TR>
            <TD COLSPAN="2" CLASS="Horline"></TD>
            <TD WIDTH=5%></TD>
            <TD COLSPAN="2" CLASS="Horline"></TD>
        </TR>

        <TR>
            <TD><LABEL ID=0 ><%= GetLocalResourceObject("tcnULmmswCaption") %></LABEL></TD>
            <TD><%=mobjValues.NumericControl("tcnULmmsw", 5, CStr(mclsProduct.nULmmsw),  , GetLocalResourceObject("tcnULmmswToolTip"),  , 0,  ,  ,  , "")%></TD>
            <TD>&nbsp;</TD>
            <TD><LABEL ID=14782><%= GetLocalResourceObject("tcnUlschargCaption") %></LABEL></TD>
            <TD><%=mobjValues.NumericControl("tcnUlscharg", 18, CStr(mclsProduct.nUlscharg),  , GetLocalResourceObject("tcnUlschargToolTip"), True, 6,  ,  ,  ,  , mclsProduct.nUlsmaxqu < 1)%></TD>
        </TR>
        <TR>
            <TD><LABEL ID=14787><%= GetLocalResourceObject("tcnUlswmqtCaption") %></LABEL></TD>
            <TD><%=mobjValues.NumericControl("tcnUlswmqt", 5, CStr(mclsProduct.nULSwmqt),  , GetLocalResourceObject("tcnUlswmqtToolTip"),  , 0,  ,  ,  ,  , mclsProduct.nUlsmaxqu < 1)%></TD>
            <TD>&nbsp;</TD>
            <TD><LABEL ID=14787><%= GetLocalResourceObject("tcnULswchPercCaption") %></LABEL></TD>
            <TD><%=mobjValues.NumericControl("tcnULswchPerc", 7, CStr(mclsProduct.nULswchPerc),  , GetLocalResourceObject("tcnULswchPercToolTip"),  ,4)%></TD>
        </TR>
        <TR>
            <TD><LABEL ID=0><%= GetLocalResourceObject("cbeULswmqtperCaption") %></LABEL></TD>
            <TD><%=mobjValues.PossiblesValues("cbeULswmqtper", "Table5644", 1, CStr(mclsProduct.nULswmqtper),  ,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbeULswmqtperToolTip"))%></TD>
            <TD>&nbsp;</TD>
            <TD><LABEL ID=0><%= GetLocalResourceObject("cbeUlswiperCaption") %></LABEL></TD>
            <TD><%=mobjValues.PossiblesValues("cbeUlswiper", "Table36", 1, CStr(mclsProduct.nUlswiper),  ,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbeUlswiperToolTip"))%></TD>
        </TR>
        <TR>
            <TD colspan= 3>&nbsp;</TD>
            <TD><LABEL ID=14787><%= GetLocalResourceObject("tcnUlsscharCaption") %></LABEL></TD>
            <TD><%=mobjValues.NumericControl("tcnUlsschar", 5, CStr(mclsProduct.nUlsschar),  , GetLocalResourceObject("tcnUlsscharToolTip"),  , 0,  ,  ,  ,  , mclsProduct.nUlsmaxqu < 1)%></TD>
        </TR>

        <TR>
            <TD WIDTH=5% COLSPAN="5">&nbsp;</TD>
        </TR>

        <TR>
            <TD COLSPAN="5" CLASS="HighLighted"><LABEL ID=100182><A NAME="Redirecciones"><%= GetLocalResourceObject("AnchorRedireccionesCaption") %></A></LABEL></TD>
        </TR>
        <TR>
            <TD COLSPAN="5" CLASS="Horline"></TD>
        </TR>  
        <TR>
            <TD><LABEL ID=14784><%= GetLocalResourceObject("tcnUlrmaxquCaption") %></LABEL></TD>
            <TD><%=mobjValues.NumericControl("tcnUlrmaxqu", 5, CStr(mclsProduct.nUlrmaxqu),  , GetLocalResourceObject("tcnUlrmaxquToolTip"),  , 0,  ,  ,  , "insLockedPermRd(this)")%></TD>
            <TD>&nbsp;</TD>
            <TD><LABEL ID=14788><%= GetLocalResourceObject("cbeULswMaxperCaption") %></LABEL></TD>
            <TD><%=mobjValues.PossiblesValues("cbeULrdMaxper", "Table5644", 1, CStr(mclsProduct.nULrdmaxper),  ,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbeULrdMaxperToolTip"))%></TD>
        </TR>

        <TR>
            <TD WIDTH=5% COLSPAN="5">&nbsp;</TD>
        </TR>

        <TR>
            <TD COLSPAN="2" CLASS="HighLighted">
                <LABEL ID=0><A NAME="PerCambiosRd"><%= GetLocalResourceObject("AnchorPerCambiosRdCaption") %></A></LABEL>
            </TD>
            <TD WIDTH=5%>&nbsp;</TD>
            <TD COLSPAN="2" CLASS="HighLighted">
                <LABEL ID=0><A NAME="RecargosRD"><%= GetLocalResourceObject("AnchorRecargosRDCaption") %></A></LABEL>
            </TD>
        </TR>
        <TR>
            <TD COLSPAN="2" CLASS="Horline"></TD>
            <TD WIDTH=5%></TD>
            <TD COLSPAN="2" CLASS="Horline"></TD>
        </TR>


        <TR>
            <TD><LABEL ID=0 ><%= GetLocalResourceObject("tcnULmmswCaption") %></LABEL></TD>
            <TD><%=mobjValues.NumericControl("tcnULmmrd", 5, CStr(mclsProduct.nULmmrd),  , GetLocalResourceObject("tcnULmmrdToolTip"),  , 0,  ,  ,  , "")%></TD>
            <TD>&nbsp;</TD>
            <TD><LABEL ID=14786><%= GetLocalResourceObject("tcnUlrchargCaption") %></LABEL></TD>
            <TD><%=mobjValues.NumericControl("tcnUlrcharg", 18, CStr(mclsProduct.nUlrcharg),  , GetLocalResourceObject("tcnUlrchargToolTip"), True, 6,  ,  ,  ,  , mclsProduct.nUlrmaxqu < 1)%></TD>
        </TR>

        <TR>
            <TD><LABEL ID=14787><%= GetLocalResourceObject("tcnUlswmqtCaption") %></LABEL></TD>
            <TD><%=mobjValues.NumericControl("tcnUlrdmqt", 5, CStr(mclsProduct.nULrdmqt),  , GetLocalResourceObject("tcnUlrdmqtToolTip"),  , 0,  ,  ,  ,  , mclsProduct.nUlsmaxqu < 1)%></TD>
            <TD>&nbsp;</TD>
            <TD><LABEL ID=14787><%= GetLocalResourceObject("tcnULswchPercCaption") %></LABEL></TD>
            <TD><%= mobjValues.NumericControl("tcnULrdchPerc", 7, CStr(mclsProduct.nUlrdchperc), , GetLocalResourceObject("tcnULrdchPercToolTip"), , 4)%></TD>
        </TR>

        <TR>
            <TD><LABEL ID=0><%= GetLocalResourceObject("cbeULswmqtperCaption") %></LABEL></TD>
            <TD><%=mobjValues.PossiblesValues("cbeULrdmqtper", "Table5644", 1, CStr(mclsProduct.nULrdmqtper),  ,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbeULrdmqtperToolTip"))%></TD>
            <TD>&nbsp;</TD>
            <TD><LABEL ID=0><%= GetLocalResourceObject("cbeUlswiperCaption") %></LABEL></TD>
            <TD><%=mobjValues.PossiblesValues("cbeUlredper", "Table36", 1, CStr(mclsProduct.nUlredper),  ,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbeUlredperToolTip"))%></TD>
        </TR>
        <TR>
            <TD colspan= 3>&nbsp;</TD>
            <TD><LABEL ID=14785><%= GetLocalResourceObject("tcnUlrscharCaption") %></LABEL></TD>
            <TD><%=mobjValues.NumericControl("tcnUlrschar", 5, CStr(mclsProduct.nUlrschar),  , GetLocalResourceObject("tcnUlrscharToolTip"),  , 0,  ,  ,  ,  , mclsProduct.nUlrmaxqu < 1)%></TD>
        </TR>            
	</TABLE>
</FORM>
</BODY>
</HTML>






