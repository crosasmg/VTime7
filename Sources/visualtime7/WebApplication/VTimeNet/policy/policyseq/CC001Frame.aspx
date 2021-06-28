<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="ePolicy" %>
<script language="VB" runat="Server">

'**-Objetive: Object for the handling of LOG
'-Objetivo: Objeto para el manejo de LOG
Dim mobjNetFrameWork As eNetFrameWork.Layout

'**-Objetive: The Object to handling the load values general functions is defined
'-Objetivo: Objeto para el manejo de las funciones generales de carga de valores        
Dim mobjValues As eFunctions.Values

'**-Objetive: Definition of the object to handle the grid and its properties
'-Objetivo: Se define la variable para el manejo del Grid de la ventana
Dim mobjGrid As eFunctions.Grid


'%insDefineHeader: Se definen las columnas del grid
'------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'------------------------------------------------------------------------------
	mobjGrid = New eFunctions.Grid
	
	'+Se definen todas las columnas del Grid
	With mobjGrid.Columns
            Call .AddHiddenColumn("tctWarrnumber", "1")
            Call .AddPossiblesColumn(17631, GetLocalResourceObject("cbeRoleColumnCaption"), "cbeRole", "table12", eFunctions.Values.eValuesType.clngComboType, , , , , , "insChangeRole(this);", , , GetLocalResourceObject("cbeRoleColumnToolTip"))
  
            'Call .AddTextColumn(17631, GetLocalResourceObject("cbeRoleColumnCaption"), "cbeRole", 50, "", , GetLocalResourceObject("cbeRoleColumnToolTip"), , , , True)
            Call .AddClientColumn(17630, GetLocalResourceObject("tctClieNameColumnCaption"), "tctClieName", "", , GetLocalResourceObject("tctClieNameColumnToolTip"), , True)
            '  Call .AddTextColumn(17630, GetLocalResourceObject("tctClieNameColumnCaption"), "tctClieName", 50, "", , GetLocalResourceObject("tctClieNameColumnToolTip"), , , , True)
            Call .AddPossiblesColumn(17625, GetLocalResourceObject("valTypewarrantyColumnCaption"), "valTypewarranty", "Table201", eFunctions.Values.eValuesType.clngWindowType, , , , , , "insValueItem(this);", , 4, GetLocalResourceObject("valTypewarrantyColumnToolTip"), eFunctions.Values.eTypeCode.eNumeric)
            Call .AddTextColumn(17626, GetLocalResourceObject("tctDocwarrantyColumnCaption"), "tctDocwarranty", 30, "", False, GetLocalResourceObject("tctDocwarrantyColumnToolTip"), , , , False)
            Call .AddPossiblesColumn(17627, GetLocalResourceObject("valCurrency_wrrColumnCaption"), "valCurrency_wrr", "Table11", 1, , , , , , , , 4, GetLocalResourceObject("valCurrency_wrrColumnToolTip"), eFunctions.Values.eTypeCode.eNumeric)
            Call .AddPossiblesColumn(17627, GetLocalResourceObject("valStatusbondCaption"), "valStatusbond", "TABLE5720", 1, , , , , , , , 4, GetLocalResourceObject("valStatusbondToolTip"), eFunctions.Values.eTypeCode.eNumeric)
            Call .AddNumericColumn(17628, GetLocalResourceObject("tcnCapacityColumnCaption"), "tcnCapacity", 18, "", False, GetLocalResourceObject("tcnCapacityColumnToolTip"), True, 6, , , , False)
            Call .AddDateColumn(17629, GetLocalResourceObject("tcdMaturityColumnCaption"), "tcdMaturity", "", False, GetLocalResourceObject("tcdMaturityColumnToolTip"))
            
		If Request.QueryString.Item("Type") = "PopUp" Then
			Call .AddButtonColumn(17629, GetLocalResourceObject("SCA2-TColumnCaption"), "SCA2-T", 0, True, False)
            Else                
                Call .AddButtonColumn(17629, GetLocalResourceObject("SCA2-TColumnCaption"), "SCA2-T", 0, True, True)
            End If
	End With
	
        With mobjGrid
                  	
            .nMainAction = CShort(IIf(Request.QueryString.Item("nMainAction") = "", 0, Request.QueryString.Item("nMainAction")))
            .Codispl = "CC001"
            .Codisp = "CC001"
            .Top = 100
            .Height = 350
            .Width = 550
            .ActionQuery = mobjValues.ActionQuery
            .bOnlyForQuery = .ActionQuery
            .Columns("Sel").GridVisible = Not .ActionQuery
            .Columns("valTypewarranty").EditRecord = True
            .Columns("cbeRole").TypeList = 1
		    .Columns("cbeRole").List = "6,8"
		
            .sEditRecordParam = "sCodisp=CC001" & "&nInsmodality=" & Request.QueryString.Item("valinsmodality") & "&nGuar_type=" & Request.QueryString.Item("tcnguar_type") & "&sContracnum=" & Request.QueryString.Item("tctcontracnum") & "&dContracdat=" & Request.QueryString.Item("tcdcontracdat") & "&nTime_eject=" & Request.QueryString.Item("tcntime_eject") & "&nCredcau=" & Request.QueryString.Item("tcncredcau") & "&nCurrency=" & Request.QueryString.Item("cbeCurrency") & "&nIndemper=" & Request.QueryString.Item("tcnindemper") & "&nMoraallow=" & Request.QueryString.Item("tcnmoraallow") & "&nTransmon1=" & Request.QueryString.Item("tcntransmon1") & "&nTransmon2=" & Request.QueryString.Item("tcntransmon2") & "&nIndper1=" & Request.QueryString.Item("tcnindper1") & "&nIndper2=" & Request.QueryString.Item("tcnindper2")
		
		
            .sDelRecordParam = "nWarrnumber='+ marrArray[lintIndex].tctWarrnumber + '"
		
            If Request.QueryString.Item("Reload") = "1" Then
                .sReloadIndex = Request.QueryString.Item("ReloadIndex")
            End If
        End With
End Sub
'%insPreCC001Frame. Se crea los datos del grid
'------------------------------------------------------------------------------
Private Sub insPreCC001Frame()
	'------------------------------------------------------------------------------    	
	Dim lcolWarrantys As ePolicy.Warrantys
	Dim lclsWarranty As Object
	
	lcolWarrantys = New ePolicy.Warrantys
	With mobjGrid
		If lcolWarrantys.Find(Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), Session("dEffecdate")) Then
			For	Each lclsWarranty In lcolWarrantys
				.Columns("tctWarrnumber").DefValue = lclsWarranty.nWarrnumber
				.Columns("valTypewarranty").DefValue = lclsWarranty.nTypewarranty
				.Columns("tctDocwarranty").DefValue = lclsWarranty.sDocwarranty
                .Columns("valCurrency_wrr").DefValue = lclsWarranty.nCurrency
                .Columns("valStatusbond").DefValue = lclsWarranty.nBondStatus
                .Columns("tcnCapacity").DefValue = lclsWarranty.nCapacity
				.Columns("btnNoteNum").nNotenum = lclsWarranty.nNotenum
                .Columns("tcdMaturity").DefValue = lclsWarranty.dMaturity
                .Columns("tctClieName").DefValue = lclsWarranty.sCliename
                .Columns("cbeRole").DefValue = lclsWarranty.sDescrole
                    Response.Write(mobjGrid.DoRow())
                    
			Next lclsWarranty
		End If
	End With
	
	Response.Write(mobjGrid.CloseTable())
	Response.Write(mobjValues.BeginPageButton)
	
	lclsWarranty = Nothing
	lcolWarrantys = Nothing
End Sub


'% insPreCC001Upd. Se define esta funcion para contruir el contenido de la 
'%                     ventana UPD de los archivos de datos particulares
'------------------------------------------------------------------------------
Private Sub insPreCC001Upd()
	'------------------------------------------------------------------------------
	Dim lclsWarranty As ePolicy.Warranty
	
	With Request
		If .QueryString.Item("Action") = "Del" Then
			Response.Write(mobjValues.ConfirmDelete())
			lclsWarranty = New ePolicy.Warranty
			If lclsWarranty.InsPostCC001(False, .QueryString.Item("sCodispl"), CInt(.QueryString.Item("nMainAction")), .QueryString.Item("Action"), Session("nUsercode"), Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), Session("dEffecdate"), CInt(.QueryString.Item("nWarrnumber")), 0, "", 0, 0, 0, CStr(0), eRemoteDB.Constants.dtmNull , String.Empty) Then
				
				Response.Write(mobjValues.ConfirmDelete())
				
			End If
			
		End If
		Response.Write(mobjGrid.DoFormUpd(.QueryString.Item("Action"), "valpolicyseq.aspx", .QueryString.Item("sCodispl"), .QueryString.Item("nMainAction"), mobjValues.ActionQuery, CShort(.QueryString.Item("Index"))))
	End With
	lclsWarranty = Nothing
End Sub

</script>
<%Response.Expires = -1441

mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.BeginPage(Request.QueryString.Item("sCodispl"))

mobjValues = New eFunctions.Values
With mobjValues
	.sSessionID = Session.SessionID
	.sCodisplPage = Request.QueryString.Item("sCodispl")
	.ActionQuery = (Request.QueryString.Item("nMainAction") = CStr(eFunctions.Menues.TypeActions.clngActionQuery))
End With
%>
<html>
<HEAD>
    <%=mobjValues.StyleSheet()%>

<script type="text/javascript" src="/VTimeNet/Scripts/GenFunctions.js"></script>
<%
If Request.QueryString.Item("Type") <> "PopUp" Then
	Response.Write("<script>var nMainAction=top.frames['fraSequence'].plngMainAction;")
    Response.Write("self.parent.top.frames['fraHeader'].UpdateDiv('lblWaitProcess', '');</script>")
End If
%>	
<script>



    //**%Objetive: This function disturbs in two variables the value selected in the age range.
    //%Objetivo: Esta función descompone en dos variables el valor seleccionado en el rango de edad.
    //-------------------------------------------------------------------------------------------
    function insValueItem(sItem) {

        with (self.document.forms[0]) {

            switch (sItem.name) {

                case "valTypewarranty":

                    if (valTypewarranty.value == 0) {
                        tctDocwarranty.value = '';
                        valCurrency_wrr.value = 0;
                        tcnCapacity.value = '';
                    }
                    break;
            }
        }
    }


    //**%Objetive: This function disturbs in two variables the value selected in the age range.
    //%Objetivo: Esta función descompone en dos variables el valor seleccionado en el rango de edad.
    //-------------------------------------------------------------------------------------------
    function insChangeRole(sItem) {

      with (self.document.forms[0]) {

          lstrQueryString = 'nRole=' + sItem.value;
           insDefValues("RoleWarranty", lstrQueryString, '/VTimeNet/Policy/PolicySeq');
          }
     }
 
</script>    
    
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" ID="FORM" NAME="<%=Request.QueryString.Item("sCodispl")%>" ACTION="ValPolicySeq.aspx?sCodispl=VI900&sIFrame=1&nMainAction=<%=Request.QueryString.Item("nMainAction")%>">
	<TABLE WIDTH="100%">	
		<TR>
            <TD COLSPAN="5" CLASS="HighLighted"><LABEL ID=17630><A NAME="Contragarantías"><%= GetLocalResourceObject("AnchorContragarantíasCaption") %></A></LABEL></TD>
        </TR>        
        <TR>
			<TD COLSPAN="5" CLASS="Horline"></TD>
        </TR>
	</TABLE>
<%
Call insDefineHeader()
If Request.QueryString.Item("Type") <> "PopUp" Then
	Call insPreCC001Frame()
	
Else
	Call insPreCC001Upd()
	
End If
mobjGrid = Nothing
mobjValues = Nothing

mobjNetFrameWork.FinishPage(Request.QueryString.Item("sCodispl"))
mobjNetFrameWork = Nothing
%>

</FORM>
</body>
</html>






